
using AutoMapper;
using IdentityModel.Client;
using KQApi.DbContexts;
using KQApi.Models;
using KQApi.Models.ProcessSharePoint.Entities;
using KQGeneral.Models.Registration;
using KQGeneral.Models.Registration.Requests;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Text;
using static IdentityModel.OidcConstants;

namespace KQApi.Services
{
    public class SQLBasateenRepository : ISQLBasateenRepository
    {
        private ILogger<SQLBasateenRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly CityInfoContext _context;
        private readonly HttpContent? httpContent;
        private readonly HttpClient httpClient;

        private readonly IBasateenRepository _SPRepo;
        public SQLBasateenRepository(
            CityInfoContext context,
            ILogger<SQLBasateenRepository> logger,
            IConfiguration configuration,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            _configuration = configuration;
            _context = context ?? throw new ArgumentNullException(nameof(context));             
    }

        public async Task<IEnumerable<School>> GetSchoolsAsynch()
        {

            //var listDataEndPpoint = string.Format(ListDataEndPpoint, basateenPointSite.value[0].id, schoolsListListId);
            //var response = httpClient.GetAsync(listDataEndPpoint).Result;
            //var schoolsData = response.Content.ReadAsStringAsync().Result;
            //var sharePointSchoolsData = JsonConvert.DeserializeObject<SqlSchool>(schoolsData);

            //var schoolsFields = sharePointSchoolsData.value.Select(v => v.fields);
            //var schools = _mapper.Map<IEnumerable<School>>(schoolsFields);

            //return schools;
            var sqlSchools = await _context.Schools.ToListAsync();
            var schools = _mapper.Map<IEnumerable<School>>(sqlSchools);
            return schools;
        }
        public async Task<IEnumerable<Teacher>> GetTeachersBySchoolIdAsynch(int schoolId, int year, int Registration_Type)
        {
            //var listDataEndPpoint = string.Format(ListDataEndPpoint, basateenPointSite.value[0].id, teachersListListId);
            //var response = httpClient.GetAsync(listDataEndPpoint).Result;
            //var teachersData = response.Content.ReadAsStringAsync().Result;
            //var sharePointTeachersData = JsonConvert.DeserializeObject<SqlTeacher>(teachersData);

            //var schoolsTeachersFields = sharePointTeachersData.value
            //    .Where(tch => (schoolId == -1 || tch.fields.MithamId == schoolId)
            //               && tch.fields.Active == "Yes"
            //               && tch.fields.Registration_Type == Registration_Type.ToString()
            //    ).Select(v => v.fields);
            //var spStudents = sharePointAllStdentsList;

            //var spStudentsRecords = spStudents.StudentsRecords.Where(sr=>sr.fields.Year == year);
            //var teachers = _mapper.Map<IEnumerable<Teacher>>(schoolsTeachersFields)
            //    .Select(t => {
            //        t.NumberOfRegisteredStudents = spStudentsRecords
            //                                       .Count(sr => sr.fields.TeacherId == t.TeacherId); 
            //        return t; });

            //return teachers;
            var sqlTeachers = await _context.VTeachers.ToListAsync();           
            var teachers = _mapper.Map<IEnumerable<Teacher>>(sqlTeachers);
            return teachers;
        }

        public async Task<IEnumerable<RegistrationType>> GetAvailableRegistrationTypes()
        {

            //var listDataEndPpoint = string.Format(ListDataEndPpoint, basateenPointSite.value[0].id, registrationTypeListListId);
            //var response = httpClient.GetAsync(listDataEndPpoint).Result;
            //var regTypesData = response.Content.ReadAsStringAsync().Result;
            //var sharePointRegTypesData = JsonConvert.DeserializeObject<SqlRegistrationType>(regTypesData);

            //var typesFields = sharePointRegTypesData.value.Select(v => v.fields);
            //var regTypes = _mapper.Map<IEnumerable<RegistrationType>>(typesFields);

            //return regTypes;
            var sqlRegTypes = await _context.RegistrationTypes.ToListAsync();
            var regTypes = _mapper.Map<IEnumerable<RegistrationType>>(sqlRegTypes);
            return regTypes;
        }

        public async Task<Student> GetStudentAsync(string studentIDN, DateTime birthDate, int year)
        {   
            //var aaa=_context.Students.FirstOrDefault();
            var sqlStudents =   _context.Students
                .FirstOrDefault(st=>  st.Year == year
                && ((int)st.IDNo).ToString() == studentIDN
                && st.Birthdate.Date == birthDate.Date
                );
            try
            {
                var student = _mapper.Map<Student>(sqlStudents);
                return student;
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
            
            return null;
             
            //var shortStudentData = sharePointAllStdentsList
            //                .StudentsRecords
            //                .FirstOrDefault(s =>  
            //                    ((int)s.fields.IDNo).ToString() == studentIDN &&
            //                    s.fields.Year == year&&
            //                    s.fields.Birthdate.Date == birthDate.Date );
            //if (shortStudentData is null) return null;
            //var studentId = shortStudentData.id;
            //ListItemEndPoint = string.Format(ListItemEndPoint, basateenPointSite.value[0].id, studentsListListId, studentId);
            //var response = httpClient.GetAsync(ListItemEndPoint).Result;
            //var studentData = response.Content.ReadAsStringAsync().Result;

            //Student student;
            //try
            //{
            //    var sharePointStdentData = JsonConvert.DeserializeObject<StudentRecord>(studentData);
            //    var studentFields = sharePointStdentData.fields;

            //    student = _mapper.Map<Student>(studentFields);
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}                        
            //return student;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="request.BirthDate">yyyy-MM-ddd fromat</param>
        /// <returns></returns>
        public async Task<string> UpdateStudentAsync(StudentRequest request)
        {
            try
            {
                var separator = '-';
                var birthDate_year = int.Parse(request.BirthDate.Split(separator)[0]);
                var birthDate_month = int.Parse(request.BirthDate.Split(separator)[1]);
                var birthDate_day = int.Parse(request.BirthDate.Split(separator)[2]);

                DateTime dBirthDate = new DateTime(birthDate_year, birthDate_month, birthDate_day);
                var studentToUpdate = await _context.Students
                    .FirstOrDefaultAsync(st => st.Year == request.Year
                    && ((int)st.IDNo).ToString() == request.IDN
                    && st.Birthdate.Date == dBirthDate.Date);
                if (studentToUpdate != null)
                {
                    if (request.SchoolName != null) studentToUpdate.School = request.SchoolName;
                    if (request.SchoolId != null) studentToUpdate.SchoolId = request.SchoolId;
                    if (request.TeacherName != null) studentToUpdate.Teacher = request.TeacherName;
                    if (request.TeacherId != null) studentToUpdate.TeacherId = request.TeacherId;

                    if (request.Reason != null) studentToUpdate.Reason = request.Reason;

                    if (request.Agree != null) studentToUpdate.Agree = request.Agree;

                    if (request.FirstAlternativeSchool != null) studentToUpdate.AltSchool1 = request.FirstAlternativeSchool;
                    if (request.SecondAlternativeSchool != null) studentToUpdate.AltSchool2 = request.SecondAlternativeSchool;

                    if (request.FirstAlternativeTeacher != null) studentToUpdate.AltTeacher1 = request.FirstAlternativeTeacher;

                    if (request.SecondAlternativeTeacher != null) studentToUpdate.AltTeacher2 = request.SecondAlternativeTeacher;

                    if (request.reshoum_hetsonee_bdekaa != null) studentToUpdate.reshoum_hetsonee_bdekaa = request.reshoum_hetsonee_bdekaa;
                    studentToUpdate.Confirm = "";
                    // Save the changes to the database
                    await _context.SaveChangesAsync();
                }
                return $"Student: {request.IDN} Updated successfully";
            }
            catch (Exception)
            {
                return $"Failed to Update Student:ID-{request.IDN}";
            }
        }

        
        public Task<IEnumerable<SqlStudent>> GetAllStudentsAsynch(int year)
        {
            throw new NotImplementedException();
        }

        public async void AddStudents(IEnumerable<SqlStudent> students)
        {
            try
            {
                students = students.Select(st =>
                {
                    st.Teacher = "Test Teacher";
                    st.TeacherId = 1;
                    return st;
                });
                _context.Students.AddRange(students);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                 var aaa=ex.Message;
            }
            
        }
    }
}
