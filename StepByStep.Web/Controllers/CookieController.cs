using System.Web;
using System.Web.Mvc;

namespace StepByStep.Web.Controllers
{
    public class CookieController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            HttpContext.Response.Cookies.Add(new HttpCookie("MYCOOKIE", "VALOR"));
            return View();
        }

        [HttpPost]
        public ActionResult Index(string mycookie)
        {
            return null;
        }
    }
}