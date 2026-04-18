using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PracticeApi.Models; // Đảm bảo đúng tên namespace của bạn

var builder = WebApplication.CreateBuilder(args);

// 1. Kết nối Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PracticeDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 2. Kích hoạt MVC (QUAN TRỌNG: Phải là AddControllersWithViews, không phải AddControllers)
builder.Services.AddControllersWithViews();

// 3. Kích hoạt bảo vệ bằng Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
    });

builder.Services.AddSession(); // Nếu bạn cần sử dụng session


var app = builder.Build();

app.UseSession(); // Nếu bạn đã thêm session
// Các Middleware bắt buộc và phải ĐÚNG THỨ TỰ
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Bác bảo vệ xét thẻ
app.UseAuthorization();  // Bác bảo vệ xét quyền

// 4. Cấu hình định tuyến API (attribute-based)
app.MapControllers();

// 5. Cấu hình định tuyến MVC (Thay thế hoàn toàn cho app.MapControllers() của API cũ)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();