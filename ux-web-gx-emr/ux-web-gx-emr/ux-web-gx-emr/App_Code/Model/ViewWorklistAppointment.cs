using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for WorklistNonAppointment
/// </summary>
public class ViewWorklistAppointment
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
    public string MobileNo { get; set; }
    public string Religion { get; set; }
    public long PayerId { get; set; }
    public string PayerName { get; set; }
    public Nullable<Int64> DoctorId { get; set; }
    public string DoctorName { get; set; }
    public Boolean IsVIP { get; set; }
    public Boolean IsNew { get; set; }
    public string AdmissionStatus { get; set; }
    public string Status { get; set; }
    public int Revision { get; set; }
    public Guid PageFA { get; set; }
    public Guid PageSOAP { get; set; }
    public int IsRecord { get; set; }
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
    public int IsCovidVac {get;set;}
    public string QueueNo { get; set; }
    public TimeSpan AppointmentFromTime { get; set; }
    public TimeSpan AppointmentToTime { get; set; }
    public bool IsWaitingList { get; set; }
    public bool IsYellow { get; set; }
    public short AppointmentNo { get; set; }
    public bool QueueStatus { get; set; }
    public string ConnStatus { get; set; }
    public string AppointmentType { get; set; }
    public Guid AppointmentHospitalId { get; set; }
    public Guid AppointmentAdmissionId { get; set; }
    public Guid AppointmentDoctorId { get; set; }
    public string ZoomLink { get; set; }
    public bool IsAIDO { get; set; }
    public Guid AppointmentId { get; set; }
	public bool isInfectious { get; set; }
	public string alertList { get; set; }

	public int TempIndex { get; set; }
    public string TempToday { get; set; }
    
}

public class ViewScheduleDoctor
{
    public Guid schedule_id { get; set; }
    public Guid hospital_id { get; set; }
    public Guid doctor_id { get; set; }
    public Guid room_mapping_id { get; set; }
    public string room_name { get; set; }
    public string from_time { get; set; }
    public string to_time { get; set; }
    public string from_to_time { get; set; }
    public string isPermanent { get; set; }
    public string isCurrentRoom { get; set; }
}

public class ViewAppointmentDoctor
{
    public List<ViewWorklistAppointment> ViewWorklistAppointments { get; set; }
    public List<ViewScheduleDoctor> ViewScheduleDoctor { get; set; }
}

public class ResultWorklistAppointment
{
    private ViewAppointmentDoctor lists = new ViewAppointmentDoctor();
    [JsonProperty("data")]
    public ViewAppointmentDoctor list { get { return lists; } }
}

public class ResponseWorklistAppointment
{
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
    public ViewAppointmentDoctor Data { set; get; }
}

public class ResultTotalTicketAppointment
{
    //private List<Doctor> lists = new List<Doctor>();
    [JsonProperty("data")]
    public string total { get; set; }
}


public class ViewRoom
{
    public Guid room_mapping_id { get; set; }
    public Int64 organization_id { get; set; }
    public string room_name { get; set; }
    public bool is_delete { get; set; }
}

public class ResultViewRoom
{
    private List<ViewRoom> lists = new List<ViewRoom>();
    [JsonProperty("data")]
    public List<ViewRoom> list { get { return lists; } }
}

public class ResponseViewRoom
{
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
    public List<ViewRoom> Data { set; get; }
}