using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Business_Entity
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string Description { get; set; }

        // For related data 

        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
