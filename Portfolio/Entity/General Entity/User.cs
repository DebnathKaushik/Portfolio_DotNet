using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entity.General_Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // for auto increment 
        public int UserId { get; set; }

       // [Required]  // Must be provide this value in frontend 
        [Display(Name = "User Name")] // for display this "User Name" in UI
        public string UserName { get; set; }
        public int Age { get; set; }

        //[Required]  // Must be provide value in frontend 
        public string Email { get; set; }
        public string Bio { get; set; }


        // One to Many ( Relationship with other classes)
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
    }
}
