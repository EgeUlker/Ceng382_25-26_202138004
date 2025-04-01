var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();

// **Session Middleware ekleme**
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session süresi
    options.Cookie.HttpOnly = true; // Güvenlik için sadece HTTP üzerinden erişilebilir olmalı
    options.Cookie.IsEssential = true; // Session çerezlerinin gerekli olduğunu belirt
});

var app = builder.Build();

app.UseSession(); // **Session aktif hale getir!**
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
