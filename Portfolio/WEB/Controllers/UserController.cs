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
                new { userId = user.UserId });
        }



        // For Show all User ( Get All User )
        [HttpGet]
        public IActionResult Index()
        {
            var users = _userService.GetAllusers();
            return View(users);
        }

        // For Search functionality
        [HttpPost]
        public IActionResult Index(string userName) 
        {
            var users = _userService.GetAllusers(); // keep all users for table

            if (string.IsNullOrEmpty(userName))
            {
                ViewBag.Error = "Please enter a username.";
                return View(users);
            }

            var foundUser = _userService.GetUserByUserName(userName);  // in repo Stored Procedure kora
            if (foundUser == null) 
            {
                ViewBag.Error = "User Not Found";
                return View(users);
            }

            return RedirectToAction("FullUserDetails", new { userId = foundUser.UserId });
        }




        // For Show Full User Details ( Stored Procedure type)
        [HttpGet]
        public IActionResult FullUserDetails(int userId) 
        { 
            var vm = _userService.GetUserFullDetails(userId);
            if( vm == null || vm.User == null )
            {
                return NotFound();
            }
            return View(vm);
        }


    }
}
