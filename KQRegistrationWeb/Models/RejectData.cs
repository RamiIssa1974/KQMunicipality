namespace KQRegistrationWeb.Models
{
    public class RejectData
    {
        public string StudentIDN { get; set; }
        public DateTime StudentBirthDate { get; set; }
        public int Year { get; set; }
        public string SelectedSchool1 { get; set; }
        public int SelectedSchoolId1 { get; set; }
        public string SelectedTeacher1 { get; set; }
        public int SelectedTeacherId1 { get; set; }
        public string SelectedSchool2 { get; set; }
        public int SelectedSchoolId2 { get; set; }
        public string SelectedTeacher2 { get; set; }
        public int SelectedTeacherId2 { get; set; }
        public string Reason { get; set; }
        public int Registration_Type { get; set; }
    }
}
