using KQApi.Models;
using KQApi.Services;
using KQGeneral.Models.Registration;
using KQGeneral.Models.Registration.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace KQApi.Controllers
{
    [ApiController]
    [Route("api/schools")]
    public class SPController : ControllerBase
    {
        private readonly IBasateenRepository _basateenRepo;
        private readonly ISQLBasateenRepository _sqlbasateenRepo;


        public SPController(IBasateenRepository basateenRepo,
                            ISQLBasateenRepository sqlbasateenRepo
            )
        {
            _basateenRepo = basateenRepo;
            _sqlbasateenRepo = sqlbasateenRepo;
        }
        
        [HttpGet("SpToDb/{year}/{Registration_Type}")]
        public async Task<ActionResult<IEnumerable<School>>> SpToDb(int year, int Registration_Type)
        {          
            var students = await _basateenRepo.GetAllStudentsAsynch(year);
            
            _sqlbasateenRepo.AddStudents(students);
            return Ok();
        }         
    }
}
