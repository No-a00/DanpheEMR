using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Extensions;
using DanpheEMR.WEB.Authentication;
using DanpheEMR.WEB.DependencyInjection;
using DanpheEMR.WEB.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("DanpheEMR.DataAccess")
    ));

var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtOptions>(jwtSection); 
var jwtOptions = jwtSection.Get<JwtOptions>();

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

builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructureServices();


builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen(options =>
{
    // Dùng trực tiếp OpenApiInfo (IDE sẽ tự nhận nếu đã using đúng ở trên)
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DanpheEMR API",
        Version = "v1"
    });

    // Cấu hình ổ khóa Bearer
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Dán Token vào đây, không cần gõ chữ 'Bearer'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});




var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Lỗi Seeding]: {ex.Message}");
    }
}

//Middleware

//  Bắt mọi lỗi văng ra và trả về JSON chuẩn
app.UseMiddleware<GlobalExceptionMiddleware>();

// Gắn header bảo mật chống XSS, Clickjacking...
app.UseMiddleware<SecurityHeadersMiddleware>();

//if (!app.Environment.IsDevelopment())
//{
//    app.UseHsts();
//}


// Chỉ bật Swagger trong môi trường Phát triển (Development)
// Thêm dịch vụ Swagger vào Container


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DanpheEMR API V1");
        c.RoutePrefix = "swagger"; // Đường dẫn sẽ là localhost:xxxx/swagger
    });
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Xác thực & Phân quyền 
app.UseAuthentication();
app.UseAuthorization();


// Khởi tạo seeder
await app.ApplyMigrationsAndSeed();

// Ghi log thời gian chạy và User gọi API
// ID của User từ JWT Token!
app.UseMiddleware<RequestAuditMiddleware>();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();