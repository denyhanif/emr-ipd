using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LabList
/// </summary>
public class LabList
{
    public Int64 organizationId { get; set; }
    public string template_name { get; set; }
    public string type { get; set; }
    public string gridview_name { get; set; }
    public int item_sequence { get; set; }
    public Int64 item_id { get; set; }
    public string item_name { get; set; }
    public int is_checked { get; set; }
    public int issubmit { get; set; }
    public int iscito { get; set; }
    public bool is_leftright { get; set; }
    public string remarks { get; set; }
    public short fasting_flag { get; set; }
    public int IsSendHope { get; set; }
    public bool IsFutureOrder { get; set; }
    public DateTime? FutureOrderDate { get; set; }
}

public class ResultLabList
{
    private List<LabList> lists = new List<LabList>();
    [JsonProperty("data")]
    public List<LabList> list { get { return lists; } }
}