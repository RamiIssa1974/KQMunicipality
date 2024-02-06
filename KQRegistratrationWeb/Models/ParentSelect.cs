using System.ComponentModel.DataAnnotations;

namespace KQRegistrationWeb.Models
{
    public class ParentSelect
    {
        public int SelectedTeacherId { get; set; }
        public bool Accept { get; set; }
        public string StudentIDN { get; set; }       
        public string StudentBirthDate { get; set; }
        public int Year { get; set; }

    }
}
