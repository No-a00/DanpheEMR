
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interfaces.Base;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Patients;
using DanpheEMR.Infrastructure.Data;
using DanpheEMR.Infrastructure.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Add DbContext with SQL Server provider
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// ====================================================================
// KHU VỰC ĐĂNG KÝ MEDIATR & FLUENT VALIDATION (CQRS)
// ====================================================================

// 1. Lấy Assembly chứa các Command/Handler của bạn (Ví dụ dùng CreatePrescriptionHandler làm mốc)
var applicationAssembly = typeof(DanpheEMR.Application.Features.EMR.Commands.CreatePrescription.CreatePrescriptionHandler).Assembly;

// 2. Đăng ký MediatR (Áp dụng cho thư viện MediatR bản mới v12+)
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(applicationAssembly);
});

// 3. Đăng ký FluentValidation (Tự động quét tất cả các file Validator)
// Yêu cầu cài đặt package: FluentValidation.DependencyInjectionExtensions
builder.Services.AddValidatorsFromAssembly(applicationAssembly);

// ====================================================================
// KHU VỰC ĐĂNG KÝ DEPENDENCY INJECTION (DI) CHO UOW & REPOSITORY
// ====================================================================

builder.Services.AddHttpContextAccessor();

// 2. Đăng ký CurrentUserService
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
// 1. Đăng ký UnitOfWork với vòng đời Scoped
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var repositoryAssembly = typeof(PatientRepository).Assembly;

// Tìm tất cả các class có tên kết thúc bằng chữ "Repository"
var repositoryTypes = repositoryAssembly.GetTypes()
    .Where(type => type.IsClass
                && !type.IsAbstract
                && type.Name.EndsWith("Repository"));

foreach (var repoType in repositoryTypes)
{
    // Tìm Interface tương ứng (Ví dụ: class PatientRepository thì tìm IPatientRepository)
    var interfaceType = repoType.GetInterfaces()
        .FirstOrDefault(i => i.Name == $"I{repoType.Name}");

    if (interfaceType != null)
    {
        // Tự động đăng ký vào hệ thống
        builder.Services.AddScoped(interfaceType, repoType);
    }
}

//
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();