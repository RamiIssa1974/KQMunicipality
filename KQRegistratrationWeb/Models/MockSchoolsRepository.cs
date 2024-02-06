using KQGeneral.Models.Registration;
using Newtonsoft.Json;

namespace KQRegistratrationWeb.Models
{
    public class MockSchoolsRepository : ISchoolsRepository
    {
        public async Task<IEnumerable<School>> GetSchoolsAsynch()
        {
            return new List<School>()
            {
                new School(){ Id = 1,Name="ابن خلدون", MaximumCapacity=79},
                new School(){ Id = 2,Name="ابن رشد", MaximumCapacity=78},
                new School(){ Id = 3,Name="الزهراء", MaximumCapacity=71},
                new School(){ Id = 4,Name="العمرية", MaximumCapacity=49},
                new School(){ Id = 5,Name="الغزالي", MaximumCapacity=95},
                new School(){ Id = 6,Name="المنار", MaximumCapacity=42},
                new School(){ Id = 7,Name="زين", MaximumCapacity=118}
            };
        }

        public async Task<Student> GetStudentByIdAndBirthDateAsync(string studentIDN, DateTime studentBirthDate, int year)
        {
            var yasmine = new Student()
            {
                Year = 2024,
                Name = "يسمين عصام عيسى",
                Agree = false,
                Confirm = true,
                Gender = "بنت",
                BirthDate = new DateTime(2015, 12, 16),
                Id = 2,
                IDN = "76543210",
                SchoolName = "الغزالي",
                TeacherName = "عبير شريف"
            };
            return new Student()
            {
                Year = 2024,
                Name = "مريم عصام عيسى",
                Agree = true,
                Confirm = true,
                Gender = "بنت",
                BirthDate = new DateTime(2017, 05, 06),
                Id = 1,
                IDN = "012334567",
                SchoolName = "الغزالي",
                SchoolId = 5,
                TeacherName = "يسمين فريج",
                TeacherId = 5
            };
        }

        public async Task<IEnumerable<Teacher>> GetTeachersBySchoolIdAsynch(int schoolId, int year)
        {
            return new List<Teacher>()
            {
                 new Teacher(){ Id=1, IsActive=true, Name="נגאח עקילי", MithamId=1, MaximumCapacity=10, NumberOfRegisteredStudents=9 },
                 new Teacher(){ Id=2, IsActive=true, Name="מרפת עיסא", MithamId=2, MaximumCapacity=11, NumberOfRegisteredStudents=10 },
                 new Teacher(){ Id=3, IsActive=true, Name="עביר עאמר", MithamId=3, MaximumCapacity=12, NumberOfRegisteredStudents=11 },
                 new Teacher(){ Id=4, IsActive=true, Name="האלה עיסא", MithamId=4, MaximumCapacity=13, NumberOfRegisteredStudents=12 },
                 new Teacher(){ Id=5, IsActive=true, Name="يسمين فريج", MithamId=5, MaximumCapacity=14, NumberOfRegisteredStudents=13 },
                 new Teacher(){ Id=6, IsActive=true, Name="זינב טהה", MithamId=6, MaximumCapacity=15, NumberOfRegisteredStudents=14 },
                 new Teacher(){ Id=7, IsActive=true, Name="אשגאן עאמר", MithamId=7, MaximumCapacity=16, NumberOfRegisteredStudents=15 },
                 new Teacher(){ Id=7, IsActive=true, Name="לינה עודה", MithamId=5, MaximumCapacity=14, NumberOfRegisteredStudents=10 },
                 new Teacher(){ Id=8, IsActive=true, Name="פירוז גאבר", MithamId=5, MaximumCapacity=14, NumberOfRegisteredStudents=10 },
                 new Teacher(){ Id=9, IsActive=true, Name="אמל בדיר", MithamId=5, MaximumCapacity=14, NumberOfRegisteredStudents=14 },
            };
        }

        public async Task<IEnumerable<RegistrationType>> GetAvailableRegistrationTypes()
        {
            return new List<RegistrationType>
             {
                 new RegistrationType{ Id=1,Name="גנים/بساتين",IsActiv=true, Year=2024},
                 new RegistrationType{ Id=2,Name="כיתה א'/صفوف اوائل",IsActiv=false, Year=2024},
                 new RegistrationType{ Id=3,Name="חטיבות ביניים/اعداديات",IsActiv=false, Year=2024},
                 new RegistrationType{ Id=4,Name="תיכונים/ثانويات",IsActiv=false, Year=2024},
             }.Where(rt => rt.IsActiv);
        }

        public Task<Student> SaveStudentDetails(Student student)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<School>> GetSchoolsAsynch(int Registration_Type)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Teacher>> GetTeachersBySchoolIdAsynch(int schoolId, int year, int Registration_Type)
        {
            throw new NotImplementedException();
        }
    }
}