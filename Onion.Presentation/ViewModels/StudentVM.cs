using System.ComponentModel.DataAnnotations;

namespace Onion.Presentation.ViewModels
{
    public class StudentVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int FacultyId { get; set; }
    }
}
