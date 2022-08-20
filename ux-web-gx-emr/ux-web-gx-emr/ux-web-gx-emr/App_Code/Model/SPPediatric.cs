using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for SPObgyn
/// </summary>
public class SPPediatric
{
    public class SOAPPediatric : SOAP
    {
        public List<PediatricData> pediatric_data { get; set; }
        public List<PediatricChart> pediatric_chart { get; set; }
    }

    public class ResultSOAPPediatric
    {
        private SOAPPediatric lists = new SOAPPediatric();
        [JsonProperty("data")]
        public SOAPPediatric list { get { return lists; } set { lists = value; } }
        public string Company { set; get; }
        public string Status { set; get; }
        public int Code { set; get; }
        public string Message { set; get; }
    }

    public class PediatricData
    {
        public Guid pediatric_data_id { get; set; }
        public string pediatric_data_type { get; set; }
        public string value { get; set; }
        public string remarks { get; set; }
    }

    public class PediatricChart
    {
        public Guid chart_transaction_master_id { get; set; }
        public string chart_type { get; set; }
        public string age { get; set; }
        public decimal x { get; set; }
        public decimal y { get; set; }
        public string verdict_note { get; set; }
        public string age_type { get; set; }
        public bool isNew { get; set; }
    }

    //for kurva
    public class Chart
    {
        public string chart_type { get; set; }
        public int score { get; set; }
        public decimal x { get; set; }
        public decimal y { get; set; }
        public string age_type { get; set; }
    }

    public class ResultChart
    {
        private List<Chart> lists = new List<Chart>();
        [JsonProperty("data")]
        public List<Chart> list { get { return lists; } }
    }

    public class ChartData
    {
        public decimal x { get; set; }
        public decimal y { get; set; }
    }

}