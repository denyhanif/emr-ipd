using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for ListChecked
/// </summary>
public class ListChecked
{
    public Int64 id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public int isnew { get; set; }
    public int isdelete { get; set; }
    public int issubmit { get; set; }
    public int iscito { get; set; }
    public int ischeck { get; set; }
    public Guid encounter_ticket_id { get; set; }
}

public class ResultListChecked
{
    private List<ListChecked> lists = new List<ListChecked>();
    [JsonProperty("data")]
    public List<ListChecked> list { get { return lists; } }
}