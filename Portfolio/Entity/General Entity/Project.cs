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
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // for auto increment 
        public int ProjectId { get; set; }

        [Required]  // Must be provide this value in frontend 
        [Display(Name = "Project Title")] // for display this "User Name" in UI
        public string ProjectTitle { get; set; }
        public string Description { get; set; }



        // Many to one ( Relationship with User classes )
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
