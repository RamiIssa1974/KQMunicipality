using KQGeneral.Models.Registration;
using KQRegistrationWeb.Models;
using KQRegistrationWeb.ViewModels;
using KQRegistratrationWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KQRegistratrationWeb.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ISchoolsRepository _repository;

        public RegistrationController(ISchoolsRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("/")]
        public IActionResult Index()
        {
            //var registrationStartUrl = "http://98.71.251.11/Registration/Start";
            // var htmlStr = $"<div><a href=\"{registrationStartUrl}\">Registration Url</a></div>";
            //return base.Content(htmlStr, "txt/html");
            return RedirectToAction("Start");
        }
        [HttpGet("Registration/Start")]
        public async Task<IActionResult> Start()
        {            
            IEnumerable<RegistrationType> types = null;
            IEnumerable<RegistrationType> activeTypes = null;
            try
            {
                types = await _repository.GetAvailableRegistrationTypes();
                activeTypes = types?.Where(rt => rt.IsActiv).ToList();
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            
            return View(activeTypes);
        }
        [HttpPost("Registration/Start")]
        public async Task<IActionResult> Start(StartData startData)
        {
            var regTypes = await _repository.GetAvailableRegistrationTypes();
            var regType = regTypes.FirstOrDefault(rt => rt.Id == startData.RegistrationTypeId);
            var regTypeName = regType?.Name;
            var regYaear = regType != null ? regType.Year : DateTime.Now.Year;

            //ViewBag.PageTitle = string.Format("{0} لعام {1}/{2}", regTypeName, regYaear, regYaear + 1);

            //var searchData = new SearchData
            //{
            //    StartData = new StartData()
            //    {
            //        RegistrationTypeId = startData.RegistrationTypeId,
            //        RegistrationYear = startData.RegistrationYear
            //    }
            //};

            return Redirect("LoginPage/" + startData.RegistrationTypeId);
        }
        [HttpGet("Registration/LoginPage/{registrationTypeId}")]
        public async Task<IActionResult> LoginPage(int registrationTypeId)
        {
            var regTypes = await _repository.GetAvailableRegistrationTypes();
            var regType = regTypes.FirstOrDefault(rt => rt.Id == registrationTypeId);
            var regTypeName = regType?.Name;
            var regYaear = regType != null ? regType.Year : DateTime.Now.Year;

            //ViewBag.PageTitle = string.Format("{0} لعام {1}/{2}", regTypeName, regYaear, regYaear + 1);
            var startData = new StartData
            {
                RegistrationTypeId = registrationTypeId,
                RegistrationYear = regYaear,
            };
            return View(new SearchData
            {
                Year = regYaear,
                StartData = startData,
                RegistrationTypeId = registrationTypeId
            });
        }
        [HttpPost("Registration/PostLogin")]
        public async Task<IActionResult> PostLogin(SearchData searchData)
        {

            var student = await _repository.GetStudentByIdAndBirthDateAsync(searchData.StudentIDN, searchData.StudentBirthDate.Value, searchData.Year);
            if (student == null)
            {
                ModelState.AddModelError("Student not found!", "");
                searchData.ErrorSearchMessage = "لم يتم العثور على بيانات الطالب, الرجاء التحقق من رقم الهوية وتاريخ الميلاد اولا. ثم التوجه الى مركز الاتصالات لبلدية كفر قاسم: ";
                return View("LoginPage", searchData);
            }
            var allTeachers = await _repository.GetTeachersBySchoolIdAsynch(student.SchoolId, student.Year, searchData.RegistrationTypeId);
             
            RegisterPageVm openingPageVm = new RegisterPageVm()
            {
                //SearchData = search,
                Student = student,
                Teachers = allTeachers.Where(t => t.MithamId == student.SchoolId).ToList()
            };
            if (student.Agree)
            {
                var registrationDetails = new RegisterDetailsVm
                {
                    Confirm = student.Confirm,
                    RegistrationMessage = !student.Confirm ?
                    string.Format("تم تسجيل الطالب/ة {0} بنجاح.", student.Name) :
                    string.Format("تم تسجيل الطالب/ة {0} بنجاح والموافقه على الطلب.", student.Name),
                    StudentName = student.Name,
                    StudentBirthDate = student.BirthDate.ToString("yyyy-MM-dd"),
                    StudentIDN = student.IDN,
                    SchoolName = student.SchoolName,
                    TeacherName = student.TeacherName
                };
                return View("RegisterDetails", registrationDetails);
            }
            //return View("Register", openingPageVm);
            return Redirect($"Register/{student.IDN}/{student.BirthDate.ToString("yyyy-MM-dd")}/{student.Year}/{searchData.RegistrationTypeId}");
        }
        [HttpGet("Registration/Register/{studentIDN}/{studentBirthDate}/{year}/{RegistrationTypeId}")]
        public async Task<IActionResult> Register(string studentIDN, DateTime studentBirthDate, int year, int RegistrationTypeId)
        {
            var student = await _repository.GetStudentByIdAndBirthDateAsync(studentIDN, studentBirthDate, year);
            student.RegistrationTypeId = RegistrationTypeId;

            var allTeachers = await _repository.GetTeachersBySchoolIdAsynch(student.SchoolId, year, RegistrationTypeId);
            RegisterPageVm openingPageVm = new RegisterPageVm()
            {
                Student = student,
                Teachers = allTeachers.Where(t => t.MithamId == student.SchoolId).ToList(),                
            };
            return View(openingPageVm);
        }
        [HttpPost("Registration/ParentSelect")]
        public async Task<IActionResult> ParentSelect(RegisterPageVm parentSelect)
        {
            var teachers = await _repository.GetTeachersBySchoolIdAsynch(-1, parentSelect.Student.Year, parentSelect.Student.RegistrationTypeId);
            var student = await _repository.GetStudentByIdAndBirthDateAsync(parentSelect.Student.IDN, parentSelect.Student.BirthDate, parentSelect.Student.Year);
            var selectedTeacher = teachers.FirstOrDefault(t => t.TeacherId == parentSelect.RegistrationData.TeacherId);
            if (parentSelect.RegistrationData.SuggestionAcceptance == "accept")
            {
                student.Agree = true;
                student.TeacherName = selectedTeacher?.Name;
                student.TeacherId = parentSelect.RegistrationData.TeacherId;
                student.reshoum_hetsonee_bdekaa = DateTime.Now.ToString();
                _repository.SaveStudentDetails(student);
                return View("Success", "تم التسجيل بنجاح/הרישום בוצע בהצלחה");
            }
            else
            {
                //return RedirectToAction("Reject", parentSelect);
                return Redirect($"Reject/?studentIDN={student.IDN}&studentBirthDate={student.BirthDate.ToString("yyyy-MM-dd")}&year={student.Year}&Registration_Type={parentSelect.Student.RegistrationTypeId}");
            }
        }
        [HttpGet("Registration/Reject")]
        public async Task<IActionResult> Reject(string studentIDN, DateTime studentBirthDate, int year, int Registration_Type)
        {
            var schools = await _repository.GetSchoolsAsynch(Registration_Type);
            var teachers = await _repository.GetTeachersBySchoolIdAsynch(-1, year, Registration_Type);
            var student = await _repository.GetStudentByIdAndBirthDateAsync(studentIDN, studentBirthDate, year);
            student.RegistrationTypeId = Registration_Type;
            var rehectVm = new RejectPageVm()
            {
                Schools = schools.ToList(),
                Teachers = teachers.ToList(),
                Student = student,
                StudentIDN = student.Id
            };
            return View(rehectVm);
        }

        [HttpPost("Registration/PostReject")]
        public async Task<IActionResult> Reject(RejectData rejectData)
        {
            //TODO Save data to sharepoint
            var student = await _repository.GetStudentByIdAndBirthDateAsync(rejectData.StudentIDN, rejectData.StudentBirthDate, rejectData.Year);

            var schools = await _repository.GetSchoolsAsynch(rejectData.Registration_Type);
            var teachers = await _repository.GetTeachersBySchoolIdAsynch(-1, rejectData.Year, rejectData.Registration_Type);

            var selectedSchool1 = schools.FirstOrDefault(school => school.Id == rejectData.SelectedSchoolId1);
            var selectedSchool2 = schools.FirstOrDefault(school => school.Id == rejectData.SelectedSchoolId2);

            var selectedTeacher1 = teachers.FirstOrDefault(teacher => teacher.TeacherId == rejectData.SelectedTeacherId1);
            var selectedTeacher2 = teachers.FirstOrDefault(teacher => teacher.TeacherId == rejectData.SelectedTeacherId2);

            student.Agree = false;
            student.Reason = rejectData.Reason;
            student.FirstAlternativeSchool = selectedSchool1.Name;
            student.FirstAlternativeSchoolId = selectedSchool1.Id;
            student.SecondAlternativeSchool = selectedSchool2.Name;
            student.SecondAlternativeSchoolId = selectedSchool2.Id;

            student.FirstAlternativeTeacher = selectedTeacher1.Name;
            student.FirstAlternativeTeacherId = selectedTeacher1.TeacherId;
            student.SecondAlternativeTeacher = selectedTeacher2.Name;
            student.SecondAlternativeTeacherId = selectedTeacher2.TeacherId;
            student.reshoum_hetsonee_bdekaa = DateTime.Now.ToString();

            _repository.SaveStudentDetails(student);

            return View("Success", "تم تقديم الإعتراض بنجاح/ההשגה הוגשה בהצלחה");
        }
    }
}
