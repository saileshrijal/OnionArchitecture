using System.ComponentModel.DataAnnotations;

namespace Onion.Presentation.ViewModels
{
    public class FacultyVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
