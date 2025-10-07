using KQGeneral.Models.Registration;

namespace KQRegistrationWeb.ViewModels
{
    public class RejectPageVm
    {
        public int StudentIDN { get; set; }
        public Student Student { get; set; }
        public List<School> Schools { get; set; }
        public List<Teacher> Teachers { get; set; }


    }
}
