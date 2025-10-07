using KQGeneral.Models.Registration;
using KQRegistrationWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace KQRegistrationWeb.ViewModels
{
    public class RegisterPageVm
    {
        public RegistrationData RegistrationData { get; set; }
        public string TypeName { get; set; }
        public Student Student { get; set; }
        public List<Teacher> Teachers { get; set; }
        public string ErrorSearchMessage { get; set; }
        public int parentSelect { get; set; }
    }
}
