using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientReferral
/// </summary>
public class PatientReferralModel
{

    public class PatientReferral
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
        public string OrderLab { get; set; }
        public string OrderRad { get; set; }
        public string DoctorReferral { get; set; }
        public string referral_type { get; set; }
        public string referral_remark { get; set; }
        public int IsSelf { get; set; }
        public string DoctorNameOri { get; set; } = "";
    }

    public class ResultPatientReferral
    {
        private List<PatientReferral> lists = new List<PatientReferral>();
        [JsonProperty("data")]
        public List<PatientReferral> list { get { return lists; } }
    }

    public class PatientReferralBalasan
    {
        public List<PatientReferral> referrals { get; set; }
        public List<PatientReferral> balasan { get; set; }
    }

    public class ResultPatientReferralBalasan
    {
        private PatientReferralBalasan lists = new PatientReferralBalasan();
        [JsonProperty("data")]
        public PatientReferralBalasan list { get { return lists; } }
    }

    public class Specialist
    {
        public Guid speciality_id { get; set; }
        public string speciality_name_en { get; set; }
        public string speciality_name { get; set; }
        public bool is_coe { get; set; }
        public bool is_delete { get; set; }
        public string image_url { get; set; }
        public string speciality_seo_key { get; set; }
        public long specialization_hope_id { get; set; }
}


    public class ResultSpecialist
    {
        public List<Specialist> data { set; get; }
        public string Company { set; get; }
        public string Status { set; get; }
        public int Code { set; get; }
        public string Message { set; get; }
    }

    public class DoctorOrg
    {
        public Guid doctor_id { get; set; }
        public Guid hospital_id { get; set; }
        public string quota_type_id { get; set; }
        public string effective_date { get; set; }
        public string created_name { get; set; }
         public string modified_name { get; set; }
        public int quota { get; set; }
        public int reservation { get; set; }
        public int walkin { get; set; }
        public Guid doctor_hospital_config_id { get; set; }
        public Guid doctor_hospital_id { get; set; }
        public bool is_internal { get; set; }
        public string doctor_type_name { get; set; }
        public string doctor_type_id { get; set; }
        public string consultation_type_id { get; set; }
        public long doctor_hope_id { get; set; }
        public Guid specialization_id { get; set; }
        public string name { get; set; }
        public Guid speciality_id { get; set; }
        public string specialization_name { get; set; }
        public string specialization_name_en { get; set; }
        public string hospital_name { get; set; }
    }


    public class ResultDoctorOrg
    {
        public List<DoctorOrg> data { set; get; }
        public string Company { set; get; }
        public string Status { set; get; }
        public int Code { set; get; }
        public string Message { set; get; }
    }
}