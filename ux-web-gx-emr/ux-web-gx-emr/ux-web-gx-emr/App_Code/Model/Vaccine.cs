using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Vaksin
/// </summary>
public class Vaccine
{
    public long vaccine_id { get; set; }
    public string vaccine_name { get; set; }
    public short vaccine_flag { get; set; }
    public bool is_active { get; set; }
}

public class ResultVaccine
{
    private List<Vaccine> lists = new List<Vaccine>();
    [JsonProperty("data")]
    public List<Vaccine> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class Vaccination
{
    public Guid vaccination_id { get; set; }
    public long vaccine_id { get; set; }
    //public long doctor_id { get; set; }
    public string doctor_name { get; set; }
    public short vaccination_sequence { get; set; }
    public string vaccination_date { get; set; }
    public string vaccination_age { get; set; }
    public string expiry_date { get; set; }
    public string no_lot { get; set; }
}