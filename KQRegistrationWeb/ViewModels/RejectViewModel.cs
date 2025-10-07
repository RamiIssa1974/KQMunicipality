using KQGeneral.Models.Registration;

namespace KQRegistrationWeb.ViewModels
{
    public class RejectViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<School> Schools { get; set; }
    }
}
