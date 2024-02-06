using KQGeneral.Models.Registration;
using Newtonsoft.Json;

namespace KQApi.Models.ProcessSharePoint.Entities
{

    public class SqlRegistrationType
    {
        public string id { get; set; }
        public string Title { get; set; }
        public string Type_name { get; set; }
        public bool IsActive { get; set; }
        public string Year { get; set; }        
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
    }
}