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

   /* public class UserFullDetailsVM1
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }

        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string Description { get; set; }


        public int EducationId { get; set; }
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string Year { get; set; }

        public int ExperienceId { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }


    }
   */
}
