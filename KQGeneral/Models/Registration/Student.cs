namespace KQGeneral.Models.Registration
{
    public class Student
    {        
        public int Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }

        public string IDN { get; set; }
        public DateTime BirthDate { get; set; }
        

        public string Telephone { get; set; }
        public string Gender { get; set; }
        public double IDFather { get; set; }
        public string Fathername { get; set; }
        public double IDMother { get; set; }
        public string MotherName { get; set; }

        public string SchoolName { get; set; }
        public int SchoolId { get; set; }
        public string TeacherName { get; set; }
        public int TeacherId { get; set; }

        public string FirstAlternativeSchool { get; set; }
        public string FirstAlternativeTeacher { get; set; }
        public string SecondAlternativeSchool { get; set; }
        public string SecondAlternativeTeacher { get; set; }

        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public bool Agree { get; set; }
        public bool Confirm { get; set; }

        public int RegistrationTypeId { get; set; }
        public string Reason { get; set; }
        public int FirstAlternativeSchoolId { get; set; }
        public int SecondAlternativeSchoolId { get; set; }
        public int FirstAlternativeTeacherId { get; set; }
        public int SecondAlternativeTeacherId { get; set; }
        public string reshoum_hetsonee_bdekaa { get; set; }
    }
}
