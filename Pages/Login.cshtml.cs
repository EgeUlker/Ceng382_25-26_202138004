using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab8.Models;
using System.Text.Json;

namespace lab8.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginInput LoginData { get; set; } = new LoginInput();
        
        public string ErrorMessage { get; set; } = string.Empty;

        private readonly IWebHostEnvironment _env;
        
        public LoginModel(IWebHostEnvironment env)
        {
            _env = env;
        }
        
        public void OnGet()
        {
            // Eğer TempData["Error"] var ise hata mesajını gösterebilirsiniz.
            if (TempData.ContainsKey("Error"))
            {
                ErrorMessage = TempData["Error"] as string ?? "";
            }
        }
        
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
                
            // users.json dosyasını oku
            var filePath = Path.Combine(_env.WebRootPath, "data", "users.json");
            if (!System.IO.File.Exists(filePath))
            {
                ErrorMessage = "User data file not found.";
                return Page();
            }
            
            var json = System.IO.File.ReadAllText(filePath);
            List<User>? users = JsonSerializer.Deserialize<List<User>>(json);
            if (users == null)
            {
                ErrorMessage = "User data is invalid.";
                return Page();
            }
            
            // Kullanıcı adı ve şifre eşleşen aktif kullanıcıyı bul
            var user = users.FirstOrDefault(u => u.Username == LoginData.Username 
                                               && u.Password == LoginData.Password
                                               && u.IsActive);
            if (user == null)
            {
                ErrorMessage = "Username or password is incorrect.";
                return Page();
            }
            
            // Basit token oluştur (GUID kullanıyoruz)
            string token = Guid.NewGuid().ToString();
            
            // Session'a giriş bilgilerini kaydet
            HttpContext.Session.SetString("username", user.Username);
            HttpContext.Session.SetString("token", token);
            HttpContext.Session.SetString("session_id", HttpContext.Session.Id);
            
            // Aynı değerleri Cookie’ye de kaydet (30 dakika süreyle, HttpOnly, Secure, SameSite)
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddMinutes(30),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append("username", user.Username, cookieOptions);
            Response.Cookies.Append("token", token, cookieOptions);
            Response.Cookies.Append("session_id", HttpContext.Session.Id, cookieOptions);
            
            // Başarılı girişte, Week7'de oluşturduğunuz tablo sayfasına yönlendir
            return RedirectToPage("/Index");
        }
        
        public class LoginInput
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}