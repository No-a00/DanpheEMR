// Thêm các using này để gọi được Interface và Class của UoW/Repo
// Thay đổi namespace nếu thực tế dự án của bạn đặt tên khác nhé!
using DanpheEMR.Core.Interface;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories;
using DanpheEMR.DataAccess.Repositories.Patients;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext with SQL Server provider
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ====================================================================
// KHU VỰC ĐĂNG KÝ DEPENDENCY INJECTION (DI) CHO UOW & REPOSITORY
// ====================================================================

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