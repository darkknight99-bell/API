using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class PointOfInterestForUpdateDTO
    {
        [Required(ErrorMessage ="This field is required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
    }
}
