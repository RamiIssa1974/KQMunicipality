using KQApi.Models.ProcessSharePoint.Entities;
using KQGeneral.Models.Registration;
using KQGeneral.Models.Registration.Requests;

namespace KQApi.Services
{
    public interface IBasateenRepository
    {
        Task<IEnumerable<SqlStudent>> GetAllStudentsAsynch(int year);
        Task<IEnumerable<School>> GetSchoolsAsynch();
        Task<IEnumerable<Teacher>> GetTeachersBySchoolIdAsynch(int schoolId, int year, int Registration_Type);
        Task<Student> GetStudentAsync(string studentIDN,DateTime birthDate, int year);
        Task <IEnumerable<RegistrationType>> GetAvailableRegistrationTypes();
        Task<string> UpdateStudentAsync(StudentRequest request);
        void AddStudents(IEnumerable<SqlStudent> students);
    }

    public interface ISQLBasateenRepository
    {
        Task<IEnumerable<SqlStudent>> GetAllStudentsAsynch(int year);
        Task<IEnumerable<School>> GetSchoolsAsynch();
        Task<IEnumerable<Teacher>> GetTeachersBySchoolIdAsynch(int schoolId, int year, int Registration_Type);
        Task<Student> GetStudentAsync(string studentIDN, DateTime birthDate, int year);
        Task<IEnumerable<RegistrationType>> GetAvailableRegistrationTypes();
        Task<string> UpdateStudentAsync(StudentRequest request);
        void AddStudents(IEnumerable<SqlStudent> students);
    }
}
