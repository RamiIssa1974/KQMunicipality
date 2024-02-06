using Newtonsoft.Json;

namespace KQApi.Models.ProcessSharePoint.Entities
{
    public class SqlTeacher
    {
        public string id { get; set; }
        public double TeacherId { get; set; }
        public double MithamId { get; set; }        
        public string Title { get; set; }
        public string TName { get; set; }
        public string MaxNumber { get; set; }
        public int NumberOfRegisteredStudents { get; set; }
        public string Active { get; set; }
        public string Registration_Type { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
    }
}