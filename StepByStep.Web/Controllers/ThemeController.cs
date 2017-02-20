using System.Web;
using System.Web.Mvc;

namespace StepByStep.Web.Controllers
{
    public class ThemeController : Controller
    {
        [HttpGet]
        public ActionResult ChangeTheme(string theme)
        {
            var cookie = new HttpCookie("Theme", theme);
            Response.Cookies.Add(cookie);
            if (Request.UrlReferrer != null)
            {
                var returnUrl = Request.UrlReferrer.ToString();
                return new RedirectResult(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}