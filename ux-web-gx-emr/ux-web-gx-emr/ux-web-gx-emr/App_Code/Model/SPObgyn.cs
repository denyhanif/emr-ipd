using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for SPObgyn
/// </summary>
public class SPObgyn
{
    public class SOAPObgyn : SOAP
    {
        public List<PregnancyData> pregnancy_data { get; set; }
        public List<PregnancyHistory> pregnancy_history { get; set; }
    }

    public class ResultSOAPObgyn
    {
        private SOAPObgyn lists = new SOAPObgyn();
        [JsonProperty("data")]
        public SOAPObgyn list { get { return lists; } set { lists = value; } }
        public string Company { set; get; }
        public string Status { set; get; }
        public int Code { set; get; }
        public string Message { set; get; }
    }

    public class PregnancyData
    {
        public Guid pregnancy_data_id { get; set; }
        public string pregnancy_data_type { get; set; }
        public string value { get; set; }
        public string remarks { get; set; }
        public string status { get; set; }
    }
    public class PregnancyHistory
    {
        public Guid pregnancy_history_id { get; set; }
        public short pregnancy_sequence { get; set; }
        public string child_age { get; set; }
        public string age_type { get; set; }
        public short child_sex { get; set; }
        public string BBL { get; set; }
        public string labor_type { get; set; }
        public string labor_helper { get; set; }
        public string labor_place { get; set; }
        public short labor_doa { get; set; }
        public short data_sequence { get; set; }
    }

    public class PregnancyContraception
    {
        public Guid pregnancy_data_id { get; set; }
        public string pregnancy_data_type { get; set; }
        public string value { get; set; }
        public string remarks { get; set; }
        public string status { get; set; }
    }
}