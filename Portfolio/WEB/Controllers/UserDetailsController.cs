using AutoMapper;
using Entity.Business_Entity;
using Entity.General_Entity;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Manager.Services;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class UserDetailsController : Controller
    {
        private readonly UserService _userService;
        private readonly ProjectService _projectService;
        private readonly ExperienceService _experienceService;
        private readonly EducationService _educationService;
        private readonly IMapper _mapper;

        // Dependency Injection 
        public UserDetailsController(UserService userService, ProjectService projectService,ExperienceService experienceService, EducationService educationService, IMapper mapper)
        {
            _userService = userService;
            _projectService = projectService;
            _experienceService = experienceService;
            _educationService = educationService;
            _mapper = mapper;

        }

        // GET: User details page for a user ----------------------------------------------------------------
        [HttpGet]
        public IActionResult AddDetails(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null) return NotFound();

            

            var model = new UserDetailsViewModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Projects = new List<ProjectDTO> { new ProjectDTO() },
                Experiences = new List<ExperienceDTO> { new ExperienceDTO() },
                Educations = new List<EducationDTO> { new EducationDTO() }
            };
            // Pass the UserName to the view via ViewData for display only
            ViewData["UserName"] = user.UserName;
            return View(model);
        }


        // POST: User Details page ( for submit )-------------------------------------------------------------
        [HttpPost]
        public IActionResult AddDetails(UserDetailsViewModel model)
        {
            var user = _userService.GetUserById(model.UserId);
            if (user == null) return NotFound();

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Validation Failed!";
                ViewData["UserName"] = user.UserName; // keep display
                return View(model);
            }

            // Save Projects
            foreach (var project in model.Projects)
            {
                project.UserId = model.UserId;
                _projectService.CreateProject(project);
            }

            // Save Experiences
            foreach (var exp in model.Experiences)
            {
                exp.UserId = model.UserId;
                _experienceService.CreateExperience(exp);
            }

            // Save Educations
            foreach (var edu in model.Educations)
            {
                edu.UserId = model.UserId;
                _educationService.CreateEducation(edu);
            }

            return RedirectToAction("Index", "User");
        }



        // GET: Edit Details -----------------------------------------------------------------------
        [HttpGet]
        public IActionResult EditDetails(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null) return NotFound();

            var vm = new UserDetailsViewModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Projects = _projectService.GetProjectsByUserId(userId),
                Experiences = _experienceService.GetExperiencesByUserId(userId),
                Educations = _educationService.GetEducationsByUserId(userId)
            };

            // If any of the lists are empty, add one empty row so form shows
            if (!vm.Projects.Any()) vm.Projects.Add(new ProjectDTO());
            if (!vm.Experiences.Any()) vm.Experiences.Add(new ExperienceDTO());
            if (!vm.Educations.Any()) vm.Educations.Add(new EducationDTO());

            return View(vm);
        }

        // POST: Edit Details --------------------------------------------------------------------
        [HttpPost]
        public IActionResult EditDetails(UserDetailsViewModel model)
        {
            

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Validation Failed: " + string.Join(", ",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return View(model);
            }

            // Save/Update Projects
            foreach (var project in model.Projects)
            {
                project.UserId = model.UserId;

                if (project.ProjectId > 0)
                    _projectService.UpdateProject(project);
                else
                    _projectService.CreateProject(project);
            }

            // Save/Update Experiences
            foreach (var exp in model.Experiences)
            {
                exp.UserId = model.UserId;

                if (exp.ExperienceId > 0)
                    _experienceService.UpdateExperience(exp);
                else
                    _experienceService.CreateExperience(exp);
            }

            // Save/Update Educations
            foreach (var edu in model.Educations)
            {
                edu.UserId = model.UserId;

                if (edu.EducationId > 0)
                    _educationService.UpdateEducation(edu);
                else
                    _educationService.CreateEducation(edu);
            }

            TempData["Success"] = "Details Updated Successfully!";
            return RedirectToAction("Index", "User");
        }



        // GET : Delete -------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Delete(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null) return NotFound();

            return View(user);  // pass User object to view
        }


        // POST : Delete -----------------------------------------------------------------------

        [HttpPost]
        public IActionResult Delete(int userId , string choice)
        {
            if(choice != "Yes")  // User Clicks No 
            {
                return RedirectToAction("Index", "User");
            }

            var deleted = _userService.DeleteUser(userId);
            if (!deleted)
            {
                TempData["Error"] = "User could not be deleted!";
                return RedirectToAction("Index", "User");
            }

            TempData["Success"] = "User deleted successfully!";
            return RedirectToAction("Index", "User");


        }


        // For PDF UserDeatils --------------------------------------------
       public IActionResult ExportPdf(int userId)
        {
            var userDetails = _userService.GetUserFullDetails(userId);

            if(userDetails == null) return NotFound();

            using(var ms = new MemoryStream()) 
            {
                var document = new iTextSharp.text.Document(); // Create iTextSharp instance
                PdfWriter.GetInstance(document, ms);

                document.Open();

                // User Details
                document.Add(new Paragraph("User Full Details"));
                document.Add(new Paragraph("---------------------------------"));
                document.Add(new Paragraph($"Name: {userDetails.User.UserName}"));
                document.Add(new Paragraph($"Email: {userDetails.User.Email}"));
                document.Add(new Paragraph($"Age: {userDetails.User.Age}"));
                document.Add(new Paragraph($"Bio: {userDetails.User.Bio}"));
                document.Add(new Paragraph("\n"));

                // Projects
                document.Add(new Paragraph("Projects"));
                document.Add(new Paragraph("---------------------------------"));
                foreach(var p in userDetails.Projects)
                {
                    document.Add(new Paragraph($"Title: {p.ProjectTitle}"));
                    document.Add(new Paragraph($"Description: {p.Description}"));
                    document.Add(new Paragraph("\n"));
                }

                // Educations
                document.Add(new Paragraph("Educations"));
                document.Add(new Paragraph("---------------------------------"));
                foreach (var edu in userDetails.Educations)
                {
                    document.Add(new Paragraph($"Institution: {edu.Institution}"));
                    document.Add(new Paragraph($"Degree: {edu.Degree}"));
                    document.Add(new Paragraph($"Year: {edu.Year}"));
                    document.Add(new Paragraph("\n"));
                }

                // Experiences
                document.Add(new Paragraph("Experiences"));
                document.Add(new Paragraph("---------------------------------"));
                foreach (var exp in userDetails.Experiences)
                {
                    document.Add(new Paragraph($"CompanyName: {exp.CompanyName}"));
                    document.Add(new Paragraph($"Degree: {exp.Position}"));
                    document.Add(new Paragraph("\n"));
                }

                document.Close();

                return File(ms.ToArray(),"application/pdf",$"{userDetails.User.UserName}_details.pdf");


            }

        }
       


    }
}
