using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LaboratoryCompare
/// </summary>

[Serializable]
public class LaboratoryCompare
{
    public String ordeR_TESTNM { get; set; }
    public String disP_SEQ { get; set; }
    public String tesT_NM { get; set; }
    public String reF_RANGE { get; set; }
    public String unit { get; set; }
    public String onO_1 { get; set; }
    public String onO_2 { get; set; }
    public String onO_3 { get; set; }
    public String onO_4 { get; set; }
    public String onO_5 { get; set; }
    public Int64 isInfo { get; set; }
    public Int64 IsHeader { get; set; }
    public string ONO_F_1 { get; set; }
    public string ONO_F_2 { get; set; }
    public string ONO_F_3 { get; set; }
    public string ONO_F_4 { get; set; }
    public string ONO_F_5 { get; set; }
}

public class ResultLaboratoryCompare
{
    private List<LaboratoryCompare> lists = new List<LaboratoryCompare>();
    [JsonProperty("data")]
    public List<LaboratoryCompare> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class pagingData {
    public String ordeR_TESTNM { get; set; }
    public String tesT_NM { get; set; }
    public String reF_RANGE { get; set; }
    public String unit { get; set; }
    public String onO_1 { get; set; }
    public String onO_2 { get; set; }
    public String onO_3 { get; set; }
    public String onO_4 { get; set; }
    public String onO_5 { get; set; }
}


public class LaboratoryHeader
{
    public String tesT_GROUP { get; set; }
    public String tesT_NM { get; set; }
}

public class ResultLaboratoryHeader
{
    private List<LaboratoryHeader> lists = new List<LaboratoryHeader>();
    [JsonProperty("data")]
    public List<LaboratoryHeader> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}