using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KQGeneral.Models.Registration.Requests
{    
    public class StudentFieldsRequest
    {          
        public string? School { get; set; }
        public int? SchoolId { get; set; }
        public string? Teacher { get; set; }
        public int? TeacherId { get; set; }            
        public string? Agree { get; set; }
        public string? Confirm { get; set; }              
        public string? AltSchool1 { get; set; }
        public string? AltSchool2 { get; set; }
        public string? AltTeacher1 { get; set; }
        public string? AltTeacher2 { get; set; }
        public string? Reason { get; set; }
        public string reshoum_hetsonee_bdekaa { get; set; }
    }

}
