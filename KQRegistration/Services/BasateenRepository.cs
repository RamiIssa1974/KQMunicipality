
using AutoMapper;
using IdentityModel.Client;
using KQApi.Models;
using KQApi.Models.ProcessSharePoint.Entities;
using KQGeneral.Models.Registration;
using KQGeneral.Models.Registration.Requests;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
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
    public class BasateenRepository : IBasateenRepository
    {
        private ILogger<BasateenRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly HttpContent? httpContent;
        private readonly HttpClient httpClient;
        private readonly string accessToken;
        private readonly string Tenant;
        private readonly string TokenEndPoint;
        private string SiteDataEndPoint;

        private string ListsEndPoint;
        private string BasateenListsEndPoint;
         
        private string ListDataEndPpoint;
        private string ListItemEndPoint;
        private string ListIDNFieldsEndPoint;
        private string ListUpdateItemEndPoint;

        private string? studentsListListId;
        private string? schoolsListListId;
        private string? teachersListListId;
        private string? registrationTypeListListId;
        private SharePointSiteData basateenPointSite;
        
        private SharePointLists basateenSharePointLists;
        

        private SharePointStudent _sharePointAllStdentsList;
        private SharePointStudent sharePointAllStdentsList
        {
            get
            {
                if (_sharePointAllStdentsList == null)
                {
                    ListIDNFieldsEndPoint = string.Format(ListIDNFieldsEndPoint, basateenPointSite.value[0].id, studentsListListId);
                    var response = httpClient.GetAsync(ListIDNFieldsEndPoint).Result;
                    var studentsListData = response.Content.ReadAsStringAsync().Result;
                    _sharePointAllStdentsList = JsonConvert.DeserializeObject<SharePointStudent>(studentsListData);

                }
                return _sharePointAllStdentsList;
            }            
        }

        private SharePointStudent _sharePointAllStdentsListForSqlSynch;
        private SharePointStudent sharePointAllStdentsListForSqlSynch
        {
            get
            {
                if (_sharePointAllStdentsListForSqlSynch == null)
                {
                    ListDataEndPpoint = string.Format(ListDataEndPpoint, basateenPointSite.value[0].id, studentsListListId);
                    var response = httpClient.GetAsync(ListDataEndPpoint).Result;
                    var studentsListData = response.Content.ReadAsStringAsync().Result;
                    _sharePointAllStdentsListForSqlSynch = JsonConvert.DeserializeObject<SharePointStudent>(studentsListData);

                }
                return _sharePointAllStdentsListForSqlSynch;
            }
        }

        public BasateenRepository(ILogger<BasateenRepository> logger,
            IConfiguration configuration, 
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            _configuration = configuration;

            var GrantType = _configuration["InfoSharePointOnline:GrantType"];
            var ClientId = _configuration["InfoSharePointOnline:ClientId"];
            var ClientSecret = _configuration["InfoSharePointOnline:ClientSecret"];
            var resource = _configuration["InfoSharePointOnline:resource"];

            SiteDataEndPoint = _configuration["InfoSharePointOnline:SiteDataEndPoint"];
            ListsEndPoint = _configuration["InfoSharePointOnline:ListsEndPoint"];
            ListDataEndPpoint = _configuration["InfoSharePointOnline:ListDataEndPpoint"];
            ListIDNFieldsEndPoint = _configuration["InfoSharePointOnline:ListIDNFieldsEndPoint"];
            ListItemEndPoint = _configuration["InfoSharePointOnline:ListItemEndPoint"];
            Tenant = _configuration["InfoSharePointOnline:Tenant"];
            TokenEndPoint = _configuration["InfoSharePointOnline:TokenEndPoint"];
            ListUpdateItemEndPoint  = _configuration["InfoSharePointOnline:ListUpdateItemEndPoint"];

            TokenEndPoint = string.Format(TokenEndPoint, Tenant);

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type",GrantType),
                new KeyValuePair<string, string>("client_id",ClientId),
                new KeyValuePair<string, string>("client_secret",ClientSecret),
                new KeyValuePair<string, string>("resource",resource),
                new KeyValuePair<string, string>("tenant",Tenant),
            };
            httpContent = new FormUrlEncodedContent(keyValues);
            httpClient = new HttpClient();
            accessToken = GetAccessToken();
            httpClient.SetBearerToken(accessToken);

            basateenPointSite = GetSharePointSiteData("basateen2022");
             

            BasateenListsEndPoint = string.Format(ListsEndPoint, basateenPointSite?.value[0].id);
             

            basateenSharePointLists = GetAllLists(BasateenListsEndPoint);
             
            schoolsListListId = GetListIdByListName("Schools", basateenSharePointLists);
            teachersListListId = GetListIdByListName("Teachers", basateenSharePointLists);
            studentsListListId = GetListIdByListName("StudentList", basateenSharePointLists);
            registrationTypeListListId = GetListIdByListName("Registration_type", basateenSharePointLists);
        }

        private SharePointLists? GetAllLists(string listsEndPoint)
        {
            var response = httpClient.GetAsync(listsEndPoint).Result;
            var listData = response.Content.ReadAsStringAsync().Result;
            var lists = JsonConvert.DeserializeObject<SharePointLists>(listData);
            return lists;
        }

        private SharePointSiteData? GetSharePointSiteData(string siteName)
        {
            var siteDataEndPoint = string.Format(SiteDataEndPoint, siteName);

            var response = httpClient.GetAsync(siteDataEndPoint).Result;
            var responseContent = response.Content.ReadAsStringAsync();
            var siteData = responseContent.Result;
            var sharePointSite = JsonConvert.DeserializeObject<SharePointSiteData>(siteData);
            if(sharePointSite == null || sharePointSite.value==null|| sharePointSite.value.Count==0) 
            {
                _logger.LogInformation($"Problem with share point getting data:Result{responseContent.Result}, Status Code:{(int)response.StatusCode}");
            }
            else
            {
                _logger.LogInformation($"Sharepoint connection success");
            }
            return sharePointSite;
        }

        private string? GetListIdByListName(string listName, SharePointLists sharePointLists)
        {
            var _studentsListListId = sharePointLists.Lists?.FirstOrDefault(obj => obj.name == listName)?.id;
            return _studentsListListId;
        }

        private string GetAccessToken()
        {
            var response = httpClient.PostAsync(TokenEndPoint, httpContent).Result;
            var token = response.Content.ReadAsStringAsync().Result;
            string accessToken = JsonConvert.DeserializeObject<AccessToken>(token).access_token;
            return accessToken;
        }
        public async Task<IEnumerable<School>> GetSchoolsAsynch()
        {             
            var listDataEndPpoint = string.Format(ListDataEndPpoint, basateenPointSite.value[0].id, schoolsListListId);
            var response = httpClient.GetAsync(listDataEndPpoint).Result;
            var schoolsData = response.Content.ReadAsStringAsync().Result;
            var sharePointSchoolsData = JsonConvert.DeserializeObject<SharePointSchool>(schoolsData);

            var schoolsFields = sharePointSchoolsData.value.Select(v => v.fields);
            var schools = _mapper.Map<IEnumerable<School>>(schoolsFields);

            return schools;
        }
        public async Task<IEnumerable<Teacher>> GetTeachersBySchoolIdAsynch(int schoolId, int year, int Registration_Type)
        {

            var listDataEndPpoint = string.Format(ListDataEndPpoint, basateenPointSite.value[0].id, teachersListListId);
            var response = httpClient.GetAsync(listDataEndPpoint).Result;
            var teachersData = response.Content.ReadAsStringAsync().Result;
            var sharePointTeachersData = JsonConvert.DeserializeObject<SharePointTeacher>(teachersData);

            var schoolsTeachersFields = sharePointTeachersData.value
                .Where(tch => (schoolId == -1 || tch.fields.MithamId == schoolId)
                           && tch.fields.Active == "Yes"
                           && tch.fields.Registration_Type == Registration_Type.ToString()
                ).Select(v => v.fields);
            var spStudents = sharePointAllStdentsList;

            var spStudentsRecords = spStudents.StudentsRecords.Where(sr => sr.fields.Year == year);
            var teachers = _mapper.Map<IEnumerable<Teacher>>(schoolsTeachersFields)
                .Select(t =>
                {
                    t.NumberOfRegisteredStudents = spStudentsRecords
                                                   .Count(sr => sr.fields.TeacherId == t.TeacherId);
                    return t;
                });

            return teachers;
        }
       
        public async Task<IEnumerable<RegistrationType>> GetAvailableRegistrationTypes()
        {
            var listDataEndPpoint = string.Format(ListDataEndPpoint, basateenPointSite.value[0].id, registrationTypeListListId);
            var response = httpClient.GetAsync(listDataEndPpoint).Result;
            var regTypesData = response.Content.ReadAsStringAsync().Result;
            var sharePointRegTypesData = JsonConvert.DeserializeObject<SharePointRegistrationType>(regTypesData);

            var typesFields = sharePointRegTypesData.value.Select(v => v.fields);
            var regTypes = _mapper.Map<IEnumerable<RegistrationType>>(typesFields);

            return regTypes;
        }
        public async Task<Student> GetStudentAsync(string studentIDN , DateTime birthDate, int year)
        {

            var shortStudentData = sharePointAllStdentsList.StudentsRecords
                            .FirstOrDefault(s =>
                                ((int)s.fields.IDNo).ToString() == studentIDN &&
                                s.fields.Year == year &&
                                s.fields.Birthdate.Date == birthDate.Date);
            if (shortStudentData is null) return null;
            var studentId = shortStudentData.id;
            ListItemEndPoint = string.Format(ListItemEndPoint, basateenPointSite.value[0].id, studentsListListId, studentId);
            var response = httpClient.GetAsync(ListItemEndPoint).Result;
            var studentData = response.Content.ReadAsStringAsync().Result;

            Student student;
            try
            {
                var sharePointStdentData = JsonConvert.DeserializeObject<StudentRecord>(studentData);
                var studentFields = sharePointStdentData.fields;

                student = _mapper.Map<Student>(studentFields);
            }
            catch (Exception ex)
            {

                throw;
            }
            return student;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="request.BirthDate">yyyy-MM-ddd fromat</param>
        /// <returns></returns>
        public async Task<string> UpdateStudentAsync(StudentRequest request)
        {

            var separator = '-';
            var birthDate_year = int.Parse(request.BirthDate.Split(separator)[0]);
            var birthDate_month = int.Parse(request.BirthDate.Split(separator)[1]);
            var birthDate_day = int.Parse(request.BirthDate.Split(separator)[2]);
            DateTime dBirthDate = new DateTime(birthDate_year, birthDate_month, birthDate_day);

            var dId = double.Parse(request.IDN);
            var spStudent = sharePointAllStdentsList
                            .StudentsRecords
                            .FirstOrDefault(s => s.fields.Year == request.Year
                                              && s.fields.IDNo == dId);
            var studentId = spStudent?.id;

            var teachers = await GetTeachersBySchoolIdAsynch(-1, request.Year, request.Registration_Type);
            var selectedteacher = teachers.FirstOrDefault(t => t.TeacherId == request.TeacherId);
            request.TeacherName = selectedteacher?.Name;

            var _listUpdateItemEndPoint = string.Format(ListUpdateItemEndPoint, basateenPointSite.value[0].id, studentsListListId, studentId);
            var content = GetStudentUpdateContent(request, spStudent);
            var aaa = await content.ReadAsStringAsync();
            var response = await httpClient.PatchAsync(_listUpdateItemEndPoint, content);
            response.EnsureSuccessStatusCode();

            var res = response.Content.ReadAsStringAsync().Result;


            return res;
        }

        private StringContent GetStudentUpdateContent(StudentRequest request,StudentRecord currentStudent)
        {
            return null;
            //var objectToConvert = new StudentFieldsRequest()
            //{
            //    School = request.SchoolName != null && request.SchoolName != currentStudent.fields.School ? request.SchoolName : null,
            //    SchoolId = request.SchoolId != null && request.SchoolId != currentStudent.fields.SchoolId ? request.SchoolId : null,
            //    Teacher = request.TeacherName != null && request.TeacherName != currentStudent.fields.Teacher ? request.TeacherName : null,
            //    TeacherId = request.TeacherId != null && request.TeacherId != currentStudent.fields.TeacherId ? request.TeacherId : null,
            //    Reason = request.Reason != null && request.Reason != currentStudent.fields.Reason ? request.Reason : null,
            //    Agree = request.Agree != null && request.Agree != currentStudent.fields.Agree ? request.Agree : null,
            //    AltSchool1 = request.FirstAlternativeSchool != null && request.FirstAlternativeSchool != currentStudent.fields.AltSchool1 ? request.FirstAlternativeSchool.Trim() : null,
            //    AltSchool2 = request.SecondAlternativeSchool != null && request.SecondAlternativeSchool != currentStudent.fields.AltSchool2 ? request.SecondAlternativeSchool.Trim() : null,
            //    AltTeacher1 = request.FirstAlternativeTeacher != null && request.FirstAlternativeTeacher != currentStudent.fields.AltTeacher1 ? request.FirstAlternativeTeacher.Trim() : null,
            //    AltTeacher2 = request.SecondAlternativeTeacher != null && request.SecondAlternativeTeacher != currentStudent.fields.AltTeacher2 ? request.SecondAlternativeTeacher.Trim() : null,
            //    reshoum_hetsonee_bdekaa = request.reshoum_hetsonee_bdekaa != null && request.reshoum_hetsonee_bdekaa != currentStudent.fields.reshoum_hetsonee_bdekaa ? request.reshoum_hetsonee_bdekaa.Trim() : null,                
            //    Confirm = null,
            //};
            
            //var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            ////var json = JsonConvert.SerializeObject(keyValues, settings);
            //var json = JsonConvert.SerializeObject(objectToConvert, settings);
            //var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            ////var httpContent = new FormUrlEncodedContent(keyValues);

            //return stringContent;
        }

        public async Task<IEnumerable<SqlStudent>> GetAllStudentsAsynch(int year)
        {
            var shortStudentData = sharePointAllStdentsListForSqlSynch.StudentsRecords
                .Where(s =>
                    s.fields.Year == year).Select(st=>st.fields);

            var students = _mapper.Map<IEnumerable<SqlStudent>>(shortStudentData);
             
            return students;
        }

        public void AddStudents(IEnumerable<SqlStudent> students)
        {
            throw new NotImplementedException();
        }
    }
}
