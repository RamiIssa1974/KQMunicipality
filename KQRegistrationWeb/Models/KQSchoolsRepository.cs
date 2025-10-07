using KQGeneral.Models.Registration;
using KQGeneral.Models.Registration.Requests;
using KQRegistratrationWeb.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Net;
using KQGeneral.Models.Registration.Responses;

namespace KQRegistrationWeb.Models
{
    public class KQSchoolsRepository : ISchoolsRepository
    {
        private readonly IConfiguration _configuration;

        public KQSchoolsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        private async Task<ServerResponse> GetDataFromServer<T>(IRequest rtRequest)
        {
            var serverResponse = new ServerResponse();

            using var http = new HttpClient();

            // NEW: build the URL from ApiBaseUrl (env) + relative path
            var apiBase = Environment.GetEnvironmentVariable("ApiBaseUrl") ?? "http://kq-api:8080";
            var baseUri = new Uri(apiBase.TrimEnd('/') + "/");
            var requestUri = Uri.IsWellFormedUriString(rtRequest.EndPoint, UriKind.Absolute)
                ? new Uri(rtRequest.EndPoint)
                : new Uri(baseUri, rtRequest.EndPoint.TrimStart('/'));
            Console.WriteLine("[HTTP] calling: " + requestUri);  // <- so we can see it in docker logs

            HttpResponseMessage response = await http.GetAsync(requestUri);

            var statusCode = response.StatusCode;
            if (statusCode != HttpStatusCode.OK)
            {
                var responeContent = await response.Content.ReadAsStringAsync();
                serverResponse.ErrorCode = statusCode.ToString();
                serverResponse.ErrorMessage = responeContent;
                return serverResponse;
            }

            var data = await response.Content.ReadAsStringAsync();
            var regTypes = JsonConvert.DeserializeObject<T>(data);
            serverResponse.Data = regTypes;
            return serverResponse;
        }

        private async Task<T> SendUpdateRequest<T>(IRequest rtRequest)
        {
            using (var client = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(rtRequest);
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(rtRequest.EndPoint, content);
                var data = await response.Content.ReadAsStringAsync();
                var updatedItem = JsonConvert.DeserializeObject<T>(data);
                return updatedItem;
            }
        }
        public async Task<IEnumerable<RegistrationType>> GetAvailableRegistrationTypes()
        {
            var regEndPoint = _configuration["KQApiEndPoints:RegistrationTypes"];
            var request = new Request { EndPoint = regEndPoint };
            var apiRes = await GetDataFromServer<List<RegistrationType>?>(request);
            var res = ((List<RegistrationType>)apiRes.Data)?.Where(rt => rt.IsActiv).ToList();

            return res;
        }
        public async Task<IEnumerable<School>> GetSchoolsAsynch(int Registration_Type)
        {
            var regEndPoint = _configuration["KQApiEndPoints:Schools"];
            var request = new Request { EndPoint = regEndPoint };
            var apiRes = await GetDataFromServer<List<School>?>(request);

            var res = ((List<School>)apiRes.Data).ToList();
            return res.Where(sc => sc.Registration_Type_Schools == Registration_Type);
        }

        public async Task<Student> GetStudentByIdAndBirthDateAsync(string studentIDN, DateTime studentBirthDate, int year)
        {
            var regEndPoint = string.Format(_configuration["KQApiEndPoints:Student"], studentIDN, studentBirthDate.ToString("yyyy-MM-dd"), year); ;
            var request = new StudentRequest { EndPoint = regEndPoint, Year = year, IDN = studentIDN, BirthDate = studentBirthDate.ToString("yyyy-MM-dd") };
            var apiRes = await GetDataFromServer<Student>(request);
            var res = ((Student)apiRes.Data);
            return res;
        }

        public async Task<IEnumerable<Teacher>> GetTeachersBySchoolIdAsynch(int schoolId, int year, int Registration_Type)
        {
            var regEndPoint = string.Format(_configuration["KQApiEndPoints:Teachers"], schoolId, year, Registration_Type );
            var request = new TeacherRequest { EndPoint = regEndPoint, MithamId = schoolId, Registration_Type = Registration_Type };
            var apiRes = await GetDataFromServer<List<Teacher>?>(request);
            var res = ((List<Teacher>)apiRes.Data).ToList();
            var fRes = res?.Where(rt => request.MithamId == -1 || rt.MithamId == request.MithamId).ToList();
            return fRes;
        }
        public async Task<Student> SaveStudentDetails(Student student)
        {
            var regEndPoint = _configuration["KQApiEndPoints:StudentUpdate"];
            var request = new StudentRequest
            {
                EndPoint = regEndPoint,
                Year = student.Year,
                IDN = student.IDN,
                BirthDate = student.BirthDate.ToString("yyyy-MM-dd"),
                Agree = student.Agree ? "Yes" : "No",
                FirstAlternativeSchool = student.FirstAlternativeSchool,
                SecondAlternativeSchool = student.SecondAlternativeSchool,
                FirstAlternativeTeacher = student.FirstAlternativeTeacher,
                SecondAlternativeTeacher = student.SecondAlternativeTeacher,
                SchoolName = student.SchoolName,
                SchoolId = student.SchoolId,
                TeacherName = student.TeacherName,
                TeacherId = student.TeacherId,
                Reason = student.Reason,
                reshoum_hetsonee_bdekaa = student.reshoum_hetsonee_bdekaa,
                Registration_Type = student.RegistrationTypeId
            };
            var res = await SendUpdateRequest<Student?>(request);
            return res;
        }
    }
}