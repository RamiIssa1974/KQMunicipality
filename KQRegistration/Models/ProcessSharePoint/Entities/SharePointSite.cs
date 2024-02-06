using Newtonsoft.Json;

namespace KQApi.Models.ProcessSharePoint.Entities
{   
    public class SharePointSiteData
{
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        [JsonProperty("value")]
        public List<SiteRecord> value { get; set; }
    }

    
    public class SiteCollection
    {
        public string hostname { get; set; }
    }

    public class SiteRecord
    {
        public DateTime createdDateTime { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public DateTime lastModifiedDateTime { get; set; }
        public string name { get; set; }
        public string webUrl { get; set; }
        public string displayName { get; set; }
        public SharePointSiteData root { get; set; }
        public SiteCollection siteCollection { get; set; }
    }
}