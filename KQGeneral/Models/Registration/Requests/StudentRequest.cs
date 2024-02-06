namespace KQGeneral.Models.Registration.Requests
{
    public class StudentRequest : IRequest
    {
        public string reshoum_hetsonee_bdekaa{ get; set; }
        public string EndPoint { get; set; }
        public int Year { get; set; }
        public string IDN { get; set; }
        public string BirthDate { get; set; }
        public string Agree { get; set; }
        public string SchoolName { get; set; }
        public int SchoolId { get; set; }
        public string TeacherName { get; set; }
        public int TeacherId { get; set; }
        public string FirstAlternativeSchool { get; set; }
        public string FirstAlternativeTeacher { get; set; }
        public string SecondAlternativeSchool { get; set; }
        public string SecondAlternativeTeacher { get; set; }

        public string Reason { get; set; }
        public int Registration_Type { get; set; }
    }
}
