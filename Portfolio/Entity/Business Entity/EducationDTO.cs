using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Business_Entity
{
    public class EducationDTO
    {
        public int EducationId { get; set; }
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string Year { get; set; }

        // For related Data
        public int UserId { get; set; }
       // public string UserName { get; set; }
    }
}
