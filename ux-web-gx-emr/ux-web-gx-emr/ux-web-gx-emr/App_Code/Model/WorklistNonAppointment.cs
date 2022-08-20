using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for WorklistNonAppointment
/// </summary>
public class WorklistNonAppointment
{
    public Guid EncounterId { get; set; }
    public Int64 OrganizationId { get; set; }
    public Int64 AdmissionId { get; set; }
    public string AdmissionNo { get; set; }
    public DateTime AdmissionDate { get; set; }
    public Int64 AdmissionTypeId { get; set; }
    public string AdmissionTypeName { get; set; }
    public Nullable<Int64> PatientId { get; set; }
    public string IsLab { get; set; }
    public string IsRad { get; set; }
    public string PatientName { get; set; }
    public string LocalMrNo { get; set; }
    public DateTime BirthDate { get; set; }
    public string Age { get; set; }
    public string Gender { get; set; }
    public string Religion { get; set; }
    public string PayerName { get; set; }
    public Nullable<Int64> DoctorId { get; set; }
    public string DoctorName { get; set; }
    public string Status { get; set; }
    public string AdmissionStatus { get; set; }
    public Boolean isVIP { get; set; }
    public string IsNew { get; set; }
    public int Revision { get; set; }
    public Guid pageFA { get; set; }
    public Guid pageSOAP { get; set; }
    public string ConnStatus { get; set; }
    public int IsTBC { get; set; }
    public int IsHCS { get; set; }
    public int IsHBS { get; set; }
    public int IsHAD { get; set; }
    public int IsPRT { get; set; }
    public int IsRHN { get; set; }
    public int IsMRS { get; set; }
    public int IsALG { get; set; }
    public int IsFAL { get; set; }
    public int IsCOVID { get; set; }
    public int IsCovidVac { get; set; }
    public string QueueNo { get; set; }
    public bool IsFinish { get; set; }
}

public class ResultWorklistNonAppointment
{
    private List<WorklistNonAppointment> lists = new List<WorklistNonAppointment>();
    [JsonProperty("data")]
    public List<WorklistNonAppointment> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class ResultTotalTicket
{
    //private List<Doctor> lists = new List<Doctor>();
    [JsonProperty("data")]
    public string total { get; set; }
}