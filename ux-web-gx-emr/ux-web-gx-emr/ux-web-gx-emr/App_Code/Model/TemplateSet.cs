using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TemplateSet
/// </summary>
public class TemplateSet
{
    public Guid soap_template_id { get; set; }
    public long doctor_id { get; set; }
    public Guid soap_mapping_id { get; set; }
    public string template_name { get; set; }
    public string template_value { get; set; }
    public string template_remarks { get; set; }
    public DateTime created_date { get; set; }
    public string created_by { get; set; }
    public Nullable<DateTime> modified_date { get; set; }
    public string modified_by { get; set; }
}

public class ResultTemplateSet
{
    private List<TemplateSet> lists = new List<TemplateSet>();
    [JsonProperty("data")]
    public List<TemplateSet> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class SpesificTemplateSet
{

    public string template_name { get; set; }
    public string template_value { get; set; }
    public string template_remarks { get; set; }
}

public class ResultSpesificTemplateSet
{
    private List<SpesificTemplateSet> lists = new List<SpesificTemplateSet>();
    [JsonProperty("data")]
    public List<SpesificTemplateSet> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class templateMappingID
{
    public string mappingName { get; set; }
    public string mappingID { get; set; }
}