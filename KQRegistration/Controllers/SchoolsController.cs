using KQApi.Models;
using KQApi.Services;
using KQGeneral.Models.Registration;
using KQGeneral.Models.Registration.Requests;
using Microsoft.AspNetCore.Mvc;

namespace KQApi.Controllers
{
    [ApiController]
    [Route("api/schools")]
    public class SchoolsController : ControllerBase
    {
        private readonly ISQLBasateenRepository _basateenRepo;

       
        public SchoolsController(ISQLBasateenRepository basateenRepo)
        {
            _basateenRepo = basateenRepo;
            
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> Schools()
        {
             
            var schools = await _basateenRepo.GetSchoolsAsynch();
            return Ok(schools);
        }
        [HttpGet("teachers/{schoolId}/{year}/{Registration_Type}")]
        public async Task<ActionResult<IEnumerable<Teacher>>> Teachers(string schoolId,int year,int Registration_Type)
        {
            int schId;
            if (!int.TryParse(schoolId, out schId) || schId < -1 || schId==0)
            {
                return BadRequest($"Invalid School Id: {schoolId}.");
            }
            var teachers = await _basateenRepo.GetTeachersBySchoolIdAsynch(schId, year, Registration_Type);
            if (teachers == null || teachers.Count() == 0)
            {
                return BadRequest($"Invalid School Id, No teachers found matching SchoolId: {schId}.");
            }
            return Ok(teachers);
        }
        [HttpGet("students/{id}/{birthDate}/{year}")]
        public async Task<ActionResult<IEnumerable<Student>>> Students(string id, string birthDate, int year)
        {
            if (id is null|| birthDate == null || year<=0)
            {
                return BadRequest();
            }
            //id = !string.IsNullOrEmpty(id) ? id : "224146167";//TODO Rami Issa Remove this code
            //Expected format yyyy-MM-dd
            var separator = '-';
            var birthDate_year = int.Parse(birthDate.Split(separator)[0]);
            var birthDate_month = int.Parse(birthDate.Split(separator)[1]);
            var birthDate_day = int.Parse(birthDate.Split(separator)[2]);
            DateTime dBirthDate = new DateTime(birthDate_year, birthDate_month, birthDate_day);
            var student = await _basateenRepo.GetStudentAsync(id, dBirthDate, year);
            if(student == null) { return BadRequest("Invalid Student Details"); }
            return Ok(student);
        }

        [HttpPost("students")]
        public async Task<ActionResult<Student>> Students()
        {
            StudentRequest request = await Request.ReadFromJsonAsync<StudentRequest>();

            var updatedStudent = await _basateenRepo.UpdateStudentAsync(request);
            return Ok(updatedStudent);
        }

        [HttpGet("RegistrationTypes")]
        public async Task<ActionResult<IEnumerable<RegistrationType>>> GetAvailableRegistrationTypes( )
        {
            //id = !string.IsNullOrEmpty(id) ? id : "224146167";//TODO Rami Issa Remove this code
            var regTypes = await _basateenRepo.GetAvailableRegistrationTypes();
            
            return Ok(regTypes);
        }

       
    }
}
