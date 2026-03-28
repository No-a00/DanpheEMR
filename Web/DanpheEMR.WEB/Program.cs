using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Auth;
using DanpheEMR.Core.Interfaces.Base;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Patients;
using DanpheEMR.Infrastructure.Data;
using DanpheEMR.Infrastructure.Services;
using DanpheEMR.WEB.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// 1. CẤU HÌNH HỆ THỐNG CỐT LÕI (DATABASE & AUTH)


// Đăng ký DbContext & Chỉ định nơi chứa Migrations (Sửa lỗi Migration Assembly)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("DanpheEMR.DataAccess")
    ));

// Đăng ký JwtOptions từ appsettings.json
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtOptions>(jwtSection);
var jwtOptions = jwtSection.Get<JwtOptions>();

// Cấu hình Xác thực JWT (BẮT BUỘC để API hiểu Token)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };
    });

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddControllersWithViews();


// 2. KHU VỰC CQRS, AUTO-MAPPER & VALIDATION


// Lấy Assembly của Application làm gốc

var applicationAssembly = typeof(Result).Assembly;

// Các dòng dưới giữ nguyên
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
builder.Services.AddValidatorsFromAssembly(applicationAssembly);

// Đăng ký AutoMapper (Sửa lỗi IMapper không tìm thấy)
builder.Services.AddAutoMapper(applicationAssembly);


// 3. DEPENDENCY INJECTION (UOW & REPOSITORY)


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Tự động đăng ký các Repositories bằng Reflection (Cực xịn!)
var repositoryAssembly = typeof(PatientRepository).Assembly;
var repositoryTypes = repositoryAssembly.GetTypes()
    .Where(type => type.IsClass && !type.IsAbstract && type.Name.EndsWith("Repository"));

foreach (var repoType in repositoryTypes)
{
    var interfaceType = repoType.GetInterfaces().FirstOrDefault(i => i.Name == $"I{repoType.Name}");
    if (interfaceType != null) builder.Services.AddScoped(interfaceType, repoType);
}

builder.Services.AddMemoryCache();


// 4. KÍCH HOẠT VÀ CẤU HÌNH PIPELINE (MIDDLEWARE)


var app = builder.Build();

// Tự động Migration và Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        await RoleSeeder.SeedDataAsync(context); // Đảm bảo class này đã tồn tại
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Lỗi Seeding]: {ex.Message}");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// THỨ TỰ PHẢI LÀ: Auth -> Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();