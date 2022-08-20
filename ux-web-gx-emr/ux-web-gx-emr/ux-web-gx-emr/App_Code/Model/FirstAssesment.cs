using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for FirstAssesment
/// </summary>
public class FirstAssesment
{
    public class PageSpecialty
    {
        public Guid page_specialty_id { get; set; }
        public string page_specialty_code { get; set; }
        public string page_specialty_name { get; set; }
        public Int64 specialty_group_id { get; set; }
        public Byte type { get; set; }
        public Boolean is_active { get; set; }
    }
    public class ResultPageSpecialty
    {
        private List<PageSpecialty> lists = new List<PageSpecialty>();
        [JsonProperty("data")]
        public List<PageSpecialty> list { get { return lists; } }
        public string Company { set; get; }
        public string Status { set; get; }
        public int Code { set; get; }
        public string Message { set; get; }
    }

    public class FirstAnalysis
    {
        public Guid encounter_ticket_id { get; set; }
        public Int64 organization_id { get; set; }
        public Int64 patient_id { get; set; }
        public Int64 admission_id { get; set; }
        public Int64 doctor_id { get; set; }
        public Guid page_id { get; set; }
        public List<SubjectiveFA> subjective_fa { get; set; }
        public List<ObjectiveFA> objective_fa { get; set; }
        public List<OthersFA> others_fa { get; set; }
        public List<PatientAllergyFA> patient_allergy_fa { get; set; }
        public List<PatientSurgeryFA> patient_surgery_fa { get; set; }
        public List<PatientDiseaseHistoryFA> patient_disease_fa { get; set; }
    }

    public class SubjectiveFA
    {
        public Guid subjective_id { get; set; }
        public Guid soap_mapping_id { get; set; }
        public string soap_mapping_name { get; set; }
        public string value { get; set; }
        public string remarks { get; set; }
    }

    public class ObjectiveFA
    {
        public Guid objective_id { get; set; }
        public Guid soap_mapping_id { get; set; }
        public string soap_mapping_name { get; set; }
        public string value { get; set; }
        public string remarks { get; set; }
    }

    public class OthersFA
    {
        public Guid others_id { get; set; }
        public Guid others_mapping_id { get; set; }
        public string others_mapping_name { get; set; }
        public string value { get; set; }
        public string remarks { get; set; }
    }

    public class PatientAllergyFA
    {
        public Guid patient_allergy_id { get; set; }
        public Int64 allergy_type { get; set; }
        public string allergy { get; set; }
        public string allergy_reaction { get; set; }
        public int is_delete { get; set; }
    }

    public class PatientSurgeryFA
    {
        public Guid patient_surgery_id { get; set; }
        public string surgery_type { get; set; }
        public DateTime surgery_date { get; set; }
        public int is_delete { get; set; }
    }

    public class PatientDiseaseHistoryFA
    {
        public Guid patient_disease_history_id { get; set; }
        public Int64 disease_history_type { get; set; }
        public string value { get; set; }
        public string remarks { get; set; }
        public int is_delete { get; set; }
    }

    public class ResultFirstAnalysis
    {
        private FirstAnalysis lists = new FirstAnalysis();
        [JsonProperty("data")]
        public FirstAnalysis list { get { return lists; } }
    }
}