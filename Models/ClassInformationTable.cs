using System.ComponentModel.DataAnnotations;

namespace lab9.Models
{
    public class ClassInformationTable
    {
        public int Id { get; set; }
        
        [Required]
        public string ClassName { get; set; } = string.Empty;
        
        [Required]
        public string Instructor { get; set; } = string.Empty;
        
        public DateTime? StartDate { get; set; }
        
        public int? StudentCount { get; set; }
        
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}