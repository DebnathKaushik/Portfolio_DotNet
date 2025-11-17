using Entity.Business_Entity;
using Entity.General_Entity;
using Manager.Services;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        // Dependency Injection
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // Get User Registration Page
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        // POST: Create User
        [HttpPost]
        public IActionResult Create(UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.CreateUser(model);

            return RedirectToAction("AddDetails", "UserDetails",
                new { userId = user.UserId, userName = user.UserName });
        }



        // For Show all User
        [HttpGet]
        public IActionResult Index()
        {
            var users = _userService.GetAllusers();
            return View(users);
        }
    }
}
