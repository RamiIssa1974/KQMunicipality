using Newtonsoft.Json;

namespace KQApi.Models.ProcessSharePoint.Entities
{
     
    public class SharePointLists
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        
        [JsonProperty("value")]
        public List<ListValue> Lists { get; set; }
    }

    public class CreatedBy
    {
        public User user { get; set; }
    }

    public class LastModifiedBy
    {
        public User user { get; set; }
    }

    public class List
    {
        public bool contentTypesEnabled { get; set; }
        public bool hidden { get; set; }
        public string template { get; set; }
    }

    public class ParentReference
    {
        public string siteId { get; set; }
    }

    
    

    public class ListValue
    {
        [JsonProperty("@odata.etag")]
        public string odataetag { get; set; } = string.Empty;
        public DateTime createdDateTime { get; set; }
        public string description { get; set; } = string.Empty;
        public string eTag { get; set; } = string.Empty;
        public string id { get; set; } = string.Empty;
        public DateTime lastModifiedDateTime { get; set; }
        public string name { get; set; } = string.Empty;
        public string webUrl { get; set; } = string.Empty;
        public string displayName { get; set; } = string.Empty;
        public CreatedBy createdBy { get; set; }
        public LastModifiedBy lastModifiedBy { get; set; } 
        public ParentReference parentReference { get; set; }
        public List list { get; set; }
    }
}