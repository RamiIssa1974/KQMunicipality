using KQGeneral.Models.Registration;

namespace KQRegistratrationWeb.Models
{
    public interface ISchoolsRepository
    {
        Task<IEnumerable<School>> GetSchoolsAsynch(int Registration_Type);
        Task<IEnumerable<Teacher>> GetTeachersBySchoolIdAsynch(int schoolId, int year, int Registration_Type);
        Task<Student> GetStudentByIdAndBirthDateAsync(string studentIDN, DateTime studentBirthDate, int year);
        Task<IEnumerable<RegistrationType>> GetAvailableRegistrationTypes();

        Task<Student> SaveStudentDetails(Student student);
    }
}
