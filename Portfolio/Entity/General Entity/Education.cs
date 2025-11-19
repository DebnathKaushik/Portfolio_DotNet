using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Common;

namespace Entity.General_Entity
{
    public class Education : IHasUserId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // for auto increment 
        public int EducationId { get; set; }

       // [Required]  // Must be provide this value in frontend 
        public string Institution { get; set; }

       // [Required]  // Must be provide this value in frontend
        public string Degree { get; set; }

       // [Required]  // Must be provide this value in frontend
        public string Year { get; set; }



        // Many to one ( Relationship with User classes )
        [ForeignKey("User")]
        public int UserId { get; set; }
        
    }
}
