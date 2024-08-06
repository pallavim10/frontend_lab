using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace WebApplication_Core.Models
{
    public class Staff
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        public string LastName { set; get; }
        [Required]
        public string FirstName { set; get; }
        [Required]
        public string Address { set; get; }
        [Required]
        public string Designation { set; get; }
        [Required]
        public string StaffNo { set; get; } 
    }
}
