using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for FirstAssesment
/// </summary>
public class SPDocument
{
    public class SOAPSDocument : SOAP
    {      
        public List<SOAPDocument> soap_document { get; set; }
    }

    public class SOAPDocument
    {
        public Guid image_id { get; set; }
        public long patient_id { get; set; }
        public long admission_id { get; set; }
        public string image_url { get; set; }
        public string image_type_value { get; set; }
        public string image_remark { get; set; }
        public string image_format { get; set; }
        public bool is_new { get; set; }
        public bool is_delete { get; set; }
        public string upload_date { get; set; }
    }

    public class ResultSOAPDocument
    {
        private SOAPSDocument lists = new SOAPSDocument();
        [JsonProperty("data")]
        public SOAPSDocument list { get { return lists; } }
    }
}