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

        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Experience> Experiences { get; set; } = new List<Experience>();
        public List<Education> Educations { get; set; } = new List<Education>();
    }

}
