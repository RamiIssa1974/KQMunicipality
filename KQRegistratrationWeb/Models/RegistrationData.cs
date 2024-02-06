using System.ComponentModel.DataAnnotations;

namespace KQRegistrationWeb.Models
{
    /// <summary>
    /// TODO Rami Issa delete this class
    /// </summary>
    public class RegistrationData
    {
        [Required]
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        [Required]
        public string SuggestionAcceptance { get; set; }

    }
}
