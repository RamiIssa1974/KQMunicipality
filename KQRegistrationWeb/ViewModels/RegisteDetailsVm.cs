using KQGeneral.Models.Registration;

namespace KQRegistrationWeb.ViewModels
{
    public class RegisterDetailsVm
    {
        public string StudentIDN { get; set; }
        public string StudentName { get; set; }
        public string StudentBirthDate { get; set; }
        public string TeacherName { get; set; }
        public string SchoolName { get; set; }
        public string RegistrationMessage { get; set; }
        public bool Confirm { get; set; }
    }
}
