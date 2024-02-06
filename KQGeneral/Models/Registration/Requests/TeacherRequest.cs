namespace KQGeneral.Models.Registration.Requests
{
    public class TeacherRequest : IRequest
    {
        public string EndPoint { get; set; }
        public int MithamId { get; set; }
        public int Registration_Type { get; set; }
        
    }
}
