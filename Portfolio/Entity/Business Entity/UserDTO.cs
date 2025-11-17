using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Business_Entity
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }

        // Initialize lists so they NEVER cause ModelState errors
        public List<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();
        public List<ExperienceDTO> Experiences { get; set; } = new List<ExperienceDTO>();
        public List<EducationDTO> Educations { get; set; } = new List<EducationDTO>();
    }

}
