using AspNetCore.Reporting;
using Entity.Business_Entity;
using Entity.General_Entity;
using Manager.Services;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.EF;
using X.PagedList.Mvc.Core;



namespace WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly IWebHostEnvironment _env;


        // Dependency Injection
        public UserController(UserService userService, IWebHostEnvironment env)
        {
            _userService = userService;
            _env = env;
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



        // For Show all User with Pagination Functionality
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;

            var users = _userService.GetAllUserPagination(); // IQueryable<UserDTO>

            var pagedData = await users
                .OrderBy(u => u.UserId)
                .ToPagedListAsync(page, pageSize);  // ToPagedListAsync() --> method is called then query get executed.

            return View(pagedData); // model is IPagedList<UserDTO>
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

            var foundUser = _userService.SearchUserByUserName(userName);  // in repo Stored Procedure kora
            if (foundUser == null || foundUser.Count == 0) 
            {
                ViewBag.Error = "User Not Found";  // in frontend show Error Msg
                return View(users);
            }

            ViewBag.SearchResults = foundUser;
            return View(users);
        }




        // For Show Full User Details ( Stored Procedure type --> See Details button )
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

        // This is for RDLC Report Controller ----------------------------------------------
        [HttpGet]
        public IActionResult RDLCReport(int userId, string reportType) 
        {
            
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // 1. Get ViewModel data
            var details = _userService.GetUserFullDetails(userId);
            if (details == null) return NotFound();

            // 2. Get RDLC file path
            string rdlcFilePath = Path.Combine(_env.WebRootPath, "Report", "UserReport.rdlc");


            // 3. Prepare parameters (if you had any)
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "user", details.User.UserName }
            };

            // 4. Create LocalReport (AspNetCore.Reporting)
            LocalReport report = new LocalReport(rdlcFilePath);

            // 5. Add Data Sources (names must match RDLC dataset names)
            report.AddDataSource("UserDetails", new[] { details.User });
            report.AddDataSource("project", details.Projects);
            report.AddDataSource("Education", details.Educations);
            report.AddDataSource("Experience", details.Experiences);

            if(reportType == "pdf") // Click pdf button 
            {
                // 6. Render PDF
                var result = report.Execute(RenderType.Pdf, 1, parameters);

                // 7. Return PDF file
                return File(result.MainStream, "application/pdf", $"{details.User.UserName}_FullDetails.pdf");

            }
            else
            {
                // Render Excel
                var result = report.Execute(RenderType.Excel, 1, parameters);

                // Return Excel file
                return File(
                    result.MainStream,
                    "application/vnd.ms-excel",         // Excel 2003 MIME type
                    $"{details.User.UserName}_FullDetails.xls"   // <<< IMPORTANT
                );

            }




        }



       
      
        

    }   
}
