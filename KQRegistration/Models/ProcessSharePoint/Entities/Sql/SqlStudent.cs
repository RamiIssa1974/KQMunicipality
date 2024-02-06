using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KQApi.Models.ProcessSharePoint.Entities
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class SqlStudent
    {
        public string id { get; set; }
        public string Title { get; set; }
        public double IDNo { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Mphone { get; set; }
        public string? Fphone { get; set; }
        public string School { get; set; }
        public double SchoolId { get; set; }
        public string Teacher { get; set; }
        public double TeacherId { get; set; }       
        public string? Agree { get; set; }
        public string? Confirm { get; set; }

        public double Year { get; set; }
         
        public DateTime? Modified { get; set; }
        public DateTime? Created { get; set; }

        public string? AltSchool1 { get; set; }
        public string? AltSchool2 { get; set; }
        public string? AltTeacher1 { get; set; }
        public string? AltTeacher2 { get; set; }
        [MaxLength(255)]
        public string? Reason { get; set; }
        public string Registration_type_ID { get; set; }
        public string? reshoum_hetsonee_bdekaa { get; set; }
    }
}
