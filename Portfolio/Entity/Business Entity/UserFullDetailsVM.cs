using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Business_Entity
{
    public class UserFullDetailsVM
    {
        public UserDTO User { get; set; }

        public List<ProjectDTO> Projects { get; set; }
        public List<EducationDTO> Educations { get; set; }
        public List<ExperienceDTO> Experiences { get; set; }
    }
}
