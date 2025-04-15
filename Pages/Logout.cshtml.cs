using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab8.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Session'ı temizle
            HttpContext.Session.Clear();

            // Girişle ilgili cookie’leri sil
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("token");
            Response.Cookies.Delete("session_id");

            return RedirectToPage("/Login");
        }
    }
}