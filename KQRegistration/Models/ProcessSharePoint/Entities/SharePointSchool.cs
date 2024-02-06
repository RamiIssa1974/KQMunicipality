using Newtonsoft.Json;

namespace KQApi.Models.ProcessSharePoint.Entities
{
     
    public class SharePointSchool
{
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        [JsonProperty("value")]
        public List<SchoolRecord> value { get; set; }
    }


    public class SchoolFields
    {
        [JsonProperty("@odata.etag")]
        public string odataetag { get; set; }
        public string id { get; set; }
        public string ContentType { get; set; }
        public string Title { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public string AuthorLookupId { get; set; }
        public string EditorLookupId { get; set; }
        public string _UIVersionString { get; set; }
        public bool Attachments { get; set; }
        public string Edit { get; set; }
        public string LinkTitleNoMenu { get; set; }
        public string LinkTitle { get; set; }
        public string ItemChildCount { get; set; }
        public string FolderChildCount { get; set; }
        public string _ComplianceFlags { get; set; }
        public string _ComplianceTag { get; set; }
        public string _ComplianceTagWrittenTime { get; set; }
        public string _ComplianceTagUserId { get; set; }
        public string MaxNumber { get; set; }
        public string Registration_Type_Schools { get; set; }
    }

    

    public class SchoolRecord
    {
        [JsonProperty("@odata.etag")]
        public string odataetag { get; set; }
        public DateTime createdDateTime { get; set; }
        public string eTag { get; set; }
        public string id { get; set; }
        public DateTime lastModifiedDateTime { get; set; }
        public string webUrl { get; set; }
        public CreatedBy createdBy { get; set; }
        public LastModifiedBy lastModifiedBy { get; set; }
        public ParentReference parentReference { get; set; }
        public ContentType contentType { get; set; }

        [JsonProperty("fields@odata.context")]
        public string fieldsodatacontext { get; set; }
        [JsonProperty("fields")]
        public SchoolFields fields { get; set; }
    }
}
