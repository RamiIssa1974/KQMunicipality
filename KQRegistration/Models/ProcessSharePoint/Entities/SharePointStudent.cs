using Newtonsoft.Json;

namespace KQApi.Models.ProcessSharePoint.Entities
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class SharePointStudent
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        [JsonProperty("value")]
        public List<StudentRecord> StudentsRecords { get; set; }
    }
    public class StudentRecord
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
        public StudentFields fields { get; set; }
    }
    public class ContentType
    {
        public string id { get; set; }
        public string name { get; set; }
    }



    public class StudentFields
    {

        [JsonProperty("@odata.etag")]
        public string odataetag { get; set; }
        public string Title { get; set; }
        public double IDNo { get; set; }
        public DateTime Birthdate { get; set; }
        public string Mphone { get; set; }
        public string Fphone { get; set; }
        public string School { get; set; }
        public double SchoolId { get; set; }
        public string Teacher { get; set; }
        public double TeacherId { get; set; }
        public string LinkTitleNoMenu { get; set; }
        public string LinkTitle { get; set; }
        public string Agree { get; set; }
        public string Confirm { get; set; }
        public string Registration_type_ID { get; set; }
        public double Year { get; set; }
        public string id { get; set; }
        public string ContentType { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public string AuthorLookupId { get; set; }
        public string EditorLookupId { get; set; }
        public string _UIVersionString { get; set; }
        public bool Attachments { get; set; }
        public string Edit { get; set; }
        public string ItemChildCount { get; set; }
        public string FolderChildCount { get; set; }
        public string _ComplianceFlags { get; set; }
        public string _ComplianceTag { get; set; }
        public string _ComplianceTagWrittenTime { get; set; }
        public string _ComplianceTagUserId { get; set; }
        public string AltSchool1 { get; set; }
        public string AltSchool2 { get; set; }
        public string AltTeacher1 { get; set; }
        public string AltTeacher2 { get; set; }
        public string Reason { get; set; }
        public string reshoum_hetsonee_bdekaa { get; set; }
    }
}
