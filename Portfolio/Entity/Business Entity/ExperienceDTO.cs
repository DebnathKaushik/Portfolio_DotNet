using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Business_Entity
{
    public class ExperienceDTO
    {
        public int ExperienceId { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }

        // For related Data
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
