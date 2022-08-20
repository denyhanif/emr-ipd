using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientData
/// </summary>
public class PatientData
{
    public long PatientId { get; set; }
    public string PatientName { get; set; }
    public string BirthDate { get; set; }
    public string Age { get; set; }
    public string ReligionName { get; set; }
    public short SexId { get; set; }
}

public class resultPatientData
{
    private PatientData lists = new PatientData();
    [JsonProperty("data")]
    public PatientData list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class resultPatientDataStatus
{
    private string status;
    [JsonProperty("status")]
    public string statusValue { get { return status; } }
}

public class PatientHealthInfoFlag
{
    public long patient_id { get; set; }
    public bool IsALG { get; set; }
    public bool IsTBC { get; set; }
    public bool IsHCS { get; set; }
    public bool IsHBS { get; set; }
    public bool IsHAD { get; set; }
    public bool IsPRT { get; set; }
    public bool IsRHN { get; set; }
    public bool IsMRS { get; set; }
    public bool IsCOVID { get; set; }
}

public class responsePatientHealthInfoFlag
{
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
    public PatientHealthInfoFlag Data { set; get; }
}

public class SingleConsent
{
    public long organization_id { get; set; }
    public long patient_id { get; set; }
    public long admission_id { get; set; }
    public Guid encounter_id { get; set; }
    public bool is_covidvac { get; set; }
}

public class responseSingleConsent
{
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
    public SingleConsent Data { set; get; }
}