using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Recap
/// </summary>
public class Recap
{
    public Int64 admission_id { get; set; }
    public string patient_name { get; set; }
    public string patient_age { get; set; }
    public Int64 patient_mr { get; set; }
    public string procedure_room { get; set; }
    public string room_class { get; set; }
    public string patient_diagnosis { get; set; }
    public string doctor_status { get; set; }
}

public class dataRecap
{
    private List<Recap> lists = new List<Recap>();
    [JsonProperty("data")]
    public List<Recap> list { get { return lists; } }
}

public class ResultRecap
{
    //private List<Recap> lists = new List<Recap>();
    //[JsonProperty("data")]
    //public List<Recap> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
    public dataRecap data { set; get; }
}

public class RecapOPD
{
    public Int64 admission_id { get; set; }
    public Int64 doctor_id { get; set; }
    public Int64? patient_id { get; set; }
    public string patient_name { get; set; }
    public string payer { get; set; }
}

public class dataRecapOPD
{
    private List<RecapOPD> lists = new List<RecapOPD>();
    [JsonProperty("data")]
    public List<RecapOPD> list { get { return lists; } }
}

public class ResultRecapOPD
{
    public dataRecapOPD data { set; get; }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}