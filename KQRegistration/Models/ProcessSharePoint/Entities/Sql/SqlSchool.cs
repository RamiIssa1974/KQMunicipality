using Newtonsoft.Json;

namespace KQApi.Models.ProcessSharePoint.Entities
{

    public class SqlSchool
    {
        public string id { get; set; }
        public string Title { get; set; }       
        
        public string MaxNumber { get; set; }
        public string Registration_Type_Schools { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }

    }
}