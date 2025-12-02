using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult ServerError()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StatusCode(int code)
        {
            if(code == 404)
            {
                ViewBag.Errorcode = code;
                return View("Notfound");
            }
            return View("Error");

            
        }
    }
}
