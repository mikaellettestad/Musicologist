using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreMVC_Lab1.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            return View("PageNotFound");
        }

        IActionResult PageNotFound()
        {
            return View();
        }
    }
}