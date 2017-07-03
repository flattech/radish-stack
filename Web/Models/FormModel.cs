using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class NewsLetterModel
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class FormModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public string DateChoosen { get; set; }
        public string Gender { get; set; }
        public string Media { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
    }
}