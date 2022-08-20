using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientHistory
/// </summary>
public class PatientHistory
{
    public class ResultPatientHistory
    {
        private ViewPatientHistory lists = new ViewPatientHistory();
        [JsonProperty("data")]
        public ViewPatientHistory list { get { return lists; } }
        public string Company { set; get; }
        public string Status { set; get; }
        public int Code { set; get; }
        public string Message { set; get; }
    }

    public class ViewPatientHistory
    {
        public PatientHeader header { get; set; }
        public List<PatientAllergy> allergy { get; set; }
        public List<CurrentMedication> currentmedication { get; set; }
        public List<LastMedication> lastmedication { get; set; }
        public List<MedicalHistory> medicalhistory { get; set; }
    }

    public class physicalExm
    {
        public int idph { get; set; }
        public string name { get; set; }
    }

    public class PatientHeader //EMR Extension
    {
        public Guid EncounterId { get; set; }
        public string PatientName { get; set; }
        public int Gender { get; set; }
        public string MrNo { get; set; }
        public string DoctorName { get; set; }
        public string AdmissionNo { get; set; }
        public DateTime BirthDate { get; set; }
        public string Religion { get; set; }
        public string PayerName { get; set; }
        public long PayerId { get; set; }
        public Int16 AdmissionTypeId { get; set; }
        public string PatientTypeName { get; set; }
        public string Formularium { get; set; }
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
        public Guid PageSOAP { get; set; }
        public Guid PageFA { get; set; }
        public string ChannelId { get; set; }
    }

    public class ResultPatientHeader
    {
        //private List<Doctor> lists = new List<Doctor>();
        [JsonProperty("data")]
        public PatientHeader header { get; set; }
    }

    public class ResponsePatientHeader
    {
        public string Company { set; get; }
        public string Status { set; get; }
        public int Code { set; get; }
        public string Message { set; get; }
        [JsonProperty("data")]
        public PatientHeader Data { set; get; }
    }

    public class PatientAllergy //EMR Transaction
    {
        public Guid patient_allergy_id { get; set; }
        public Int64 patient_id { get; set; }
        public string allergy_type { get; set; }
        public string allergy { get; set; }
    }

    public class LastMedication //EMR Transaction
    {
        public Guid encounter_ticket_id { get; set; }
        public string item_name { get; set; }
        public Boolean is_routine { get; set; }
        public DateTime admission_date { get; set; }
        public string doctor_name { get; set; }
    }

    public class MedicalHistory
    {
        public Guid encounter_ticket_id { get; set; }
        public Guid mapping_id { get; set; }
        public string mapping_name { get; set; }
        public string remarks { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string doctor_name { get; set; }
    }

    public class AdmissionHistory //Data Collection
    {
        public Int64 OrganizationId { get; set; }
        public string OrgCd { get; set; }
        public Int64 AdmissionId { get; set; }
        public string AdmissionNo { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int AdmissionMonth { get; set; }
        public int AdmissionWeek { get; set; }
        public string DoctorName { get; set; }
        public string Specialty { get; set; }
        public int isMyself { get; set; }
        public string LabSalesOrderNo { get; set; }
        public string RadSalesOrderNo { get; set; }
        public string Type { get; set; }
        public string Diagnosis { get; set; }
        public string connStatus { get; set; }
        public string encounterId { get; set; }
    }

    public class CurrentMedication
    {
        public long patient_id { get; set; }
        public string current_medication { get; set; }
    }

    public class ResultCurrentMedication
    {
        //private PatientRoutineMedication lists = new PatientRoutineMedication();
        //[JsonProperty("data")]
        public CurrentMedication list { get; set; }
    }

    public class ResponseAdmissionHistory
    {
        public string Company { set; get; }
        public string Status { set; get; }
        public int Code { set; get; }
        public string Message { set; get; }
        
        [JsonProperty("data")]
        public List<AdmissionHistory> list { set; get; }
    }

    public class ResultAdmissionHistory
    {
        private List<AdmissionHistory> lists = new List<AdmissionHistory>();
        [JsonProperty("data")]
        public List<AdmissionHistory> list { get { return lists; } }
    }

    public class ScannedMR
    {
        public Int64? OrganizationId { get; set; }
        public Int64? PatientId { get; set; }
        public Int64? AdmissionId { get; set; }
        public string AdmissionNo { get; set; }
        public string AdmissionDate { get; set; }
        public Int64? MrNo { get; set; }
        public string AdmissionType { get; set; }
        public string FormTypeName { get; set; }
        public string Path { get; set; }
        public string DoctorName { get; set; }

    }

    public class PatientHistoryLite
    {
        public long OrganizationId { get; set; }
        public string OrganizationCode { get; set; }
        public long PatientId { get; set; }
        public long AdmissionId { get; set; }
        public Guid EncounterId { get; set; }
        public string AdmissionNo { get; set; }
        public string AdmissionTypeName { get; set; }
        public string AdmissionDate { get; set; }
        public string DoctorName { get; set; }
        public string Subjective { get; set; }
        public string Objective { get; set; }
        public string Diagnosis { get; set; }
        public string PlanningProcedure { get; set; }
        public string Prescription { get; set; }
        public string IsLab { get; set; }
        public string IsRad { get; set; }
        public string ConnStatus { get; set; }
        public int CountData { get; set; }
        public string LocalMrNo { get; set; }
        public string PatientName { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Admission { get; set; }
        public int CheckPrint { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool IsEditPrescription { get; set; }
        public Guid PageSOAP { get; set; }
        public bool IsTeleconsultation { get; set; }
    }

    public class ResultPatientHistoryLite
    {
        private List<PatientHistoryLite> lists = new List<PatientHistoryLite>();
        [JsonProperty("data")]
        public List<PatientHistoryLite> list { get { return lists; } }
    }


    public class PatientDashboard
    {
        public ViewPatientHeader patientheader { get; set; }
        public List<ViewHealthInfo> patienthealthinfo { get; set; }
        public List<ViewNotification> patientnotification { get; set; }
        public List<ViewProcedure> patientprocedure { get; set; }
        public List<ViewProcedureResult> proceduresults { get; set; }
    }

    public class ResponsePatientDashboard
    {
        public string Company { set; get; }
        public string Status { set; get; }
        public int Code { set; get; }
        public string Message { set; get; }
        [JsonProperty("data")]
        public PatientDashboard Data { set; get; }
    }

    public class ResultPatientDashboard
    {
        private List<PatientDashboard> lists = new List<PatientDashboard>();
        [JsonProperty("data")]
        public List<PatientDashboard> list { get { return lists; } }
    }

    public class ViewPatientHeader
    {
        public Guid EncounterId { get; set; }
        public string PatientName { get; set; }
        public int Gender { get; set; }
        public string MrNo { get; set; }
        public string DoctorName { get; set; }
        public string AdmissionNo { get; set; }
        public DateTime BirthDate { get; set; }
        public string Religion { get; set; }
        public Int16 AdmissionTypeId { get; set; }
        public Int64 PayerId { get; set; }
        public string PayerName { get; set; }
        public string Formularium { get; set; }
        public int IsReferral { get; set; }
        public int IsBalasan { get; set; } = 0;
    }

    public class ViewHealthInfo
    {
        public int other_health_info_type { get; set; }
        public string other_health_info_value { get; set; }
        public string other_health_info_remarks { get; set; }
    }

    public class ViewNotification
    {
        public string notification { get; set; }
        public string doctor_name { get; set; }
        public string created_date { get; set; }
        public long doctor_id { get; set; }
    }

    public class ViewProcedure
    {
        public short procedure_type { get; set; }
        public string procedure_remarks { get; set; }
        public DateTime procedure_date { get; set; }
        public string doctor_name { get; set; }
    }

    public class ViewProcedureResult
    {
        public string admission { get; set; }
        public string doctor_name { get; set; }
        public string planning_remarks { get; set; }
    }

}