using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_Project.Data
{
    public class CV
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CvId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Skills { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
