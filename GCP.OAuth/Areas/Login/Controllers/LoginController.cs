using Microsoft.AspNetCore.Mvc;

namespace GCP.OAuth.Areas.Login.Controllers
{
    [Area("Login")]
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
          public  ActionResult ValidLogin()
        {

            return null;
        }

    }
}
