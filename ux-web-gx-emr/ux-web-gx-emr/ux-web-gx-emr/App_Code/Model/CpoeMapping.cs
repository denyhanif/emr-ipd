using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CpoeMapping
/// </summary>

public class CpoeMapping
{
    public Int64 organizationId { get; set; }
    public string template_name { get; set; }
    public string type { get; set; }
    public string gridview_name { get; set; }
    public int item_sequence { get; set; }
    public Int64 item_id { get; set; }
    public string item_name { get; set; }
    public int is_checked { get; set; }
    public bool is_leftright { get; set; }
    public short fasting_flag { get; set; }
}

public class ResultMapping
{
    private List<CpoeMapping> lists = new List<CpoeMapping>();
    [JsonProperty("data")]
    public List<CpoeMapping> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class ViewItemCPOE
{
    public long id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string remarks { get; set; }
    public int isnew { get; set; }
    public int iscito { get; set; }
    public int issubmit { get; set; }
    public int isdelete { get; set; }
    public int ischeck { get; set; }
    public bool is_leftright { get; set; }
    public string template_name { get; set; }
    public int IsSendHope { get; set; } = 0;
    public bool IsFutureOrder { get; set; }
    public DateTime? FutureOrderDate { get; set; }
}

public class ResultViewItemCPOE
{
    private List<ViewItemCPOE> lists = new List<ViewItemCPOE>();
    [JsonProperty("data")]
    public List<ViewItemCPOE> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

