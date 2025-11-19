using Entity.General_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Business_Entity
{
    public class UserDetailsViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public List<ProjectDTO> Projects { get; set; } = new();
        public List<ExperienceDTO> Experiences { get; set; } = new();
        public List<EducationDTO> Educations { get; set; } = new();
    }

}
