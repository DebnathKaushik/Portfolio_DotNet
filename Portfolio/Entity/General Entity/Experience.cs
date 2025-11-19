using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Entity.Common;

namespace Entity.General_Entity
{
    public class Experience : IHasUserId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // for auto increment 
        public int ExperienceId { get; set; }

        //[Required]  // Must be provide this value in frontend 
        [Display(Name = "Company Name")] // for display this "User Name" in UI
        public string CompanyName { get; set; }

        //[Required]  // Must be provide this value in frontend
        public string Position { get; set; }



        // Many to one ( Relationship with User classes )
        [ForeignKey("User")]
        public int UserId { get; set; }
       
    }
}
