using AutoMapper;
using Entity.Business_Entity;
using Entity.General_Entity;
using Manager.Services;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class UserDetailsController : Controller
    {
        private readonly ProjectService _projectService;
        private readonly ExperienceService _experienceService;
        private readonly EducationService _educationService;
        private readonly IMapper _mapper;

        // Dependency Injection 
        public UserDetailsController(ProjectService projectService,ExperienceService experienceService, EducationService educationService, IMapper mapper)
        {
            _projectService = projectService;
            _experienceService = experienceService;
            _educationService = educationService;
            _mapper = mapper;

        }

        // GET: Add details page for a user
        [HttpGet]
        public IActionResult AddDetails(int userId, string userName) // This UserId comes from UserController create (method/route)
        {
            var model = new UserDetailsViewModel
            {
                UserId = userId,
                UserName = userName
            };

            // Optional: Initialize 1 empty row for each model to display in form
            model.Projects.Add(new Project());
            model.Experiences.Add(new Experience());
            model.Educations.Add(new Education());

            return View(model);
        }


        // Post
        [HttpPost]
        public IActionResult AddDetails(UserDetailsViewModel model)
        {

            // 1️⃣ Check ModelState errors
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                TempData["Error"] = "Validation Failed: " + string.Join(", ", errors);

                return View(model); // return back to the AddDetails page
            }

            // Save Projects
            foreach (var project in model.Projects)
            {
                 project.UserId = model.UserId;
                 var projectDTO = _mapper.Map<ProjectDTO>(project); // Convert to DTO
                _projectService.CreateProject(projectDTO);
            }

                // Save Experiences
                foreach (var exp in model.Experiences)
                {
                    exp.UserId = model.UserId;
                    var expDTO = _mapper.Map<ExperienceDTO>(exp);
                    _experienceService.CreateExperience(expDTO);
                }

                // Save Educations
                foreach (var edu in model.Educations)
                {
                    edu.UserId = model.UserId;
                    var eduDTO = _mapper.Map<EducationDTO>(edu);
                    _educationService.CreateEducation(eduDTO);
                }

                return RedirectToAction("Index", "User");
           
        }

    }
}
