using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientHistoryEncounter
/// </summary>

[Serializable]
public class PatientHistoryEncounter
{
    public Guid encounterId { get; set; }
    public Int64 organizationId { get; set; }
    public Int64 admissionId { get; set; }
    public Int64 patientId { get; set; }
    public Int64 doctorId { get; set; }
    public DateTime admissionDate { get; set; }

    public class ResultPatientHistoryEncounter
    {
        private List<PatientHistoryEncounter> lists = new List<PatientHistoryEncounter>();
        [JsonProperty("data")]
        public List<PatientHistoryEncounter> list { get { return lists; } }
        public string Company { set; get; }
        public string Status { set; get; }
        public int Code { set; get; }
        public string Message { set; get; }
    }
}

public class ResultPatientHistoryEncounterData
{
    private PatientHistoryEncounterData lists = new PatientHistoryEncounterData();
    [JsonProperty("data")]
    public PatientHistoryEncounterData list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class PatientHistoryEncounterData
{
    public PatientHistoryHeader historyheader { get; set; }
    public PatientHistoryAnamnesis historyanamnesis { get; set; }
    public List<PatientHistoryIllness> historyillness { get; set; }
    public List<PatientHistoryPhysicalExam> historyphysicalexam { get; set; }
    public List<PatientHistoryDiagnosis> historydiagnosis { get; set; }
    public List<PatientHistoryPlanning> historyplanning { get; set; }
    public List<PatientHistoryClinicalFinding> historyclinical { get; set; }
    public List<PatientHistoryPrescription> historyprescription { get; set; }

    public List<CompoundHeaderSoap> compound_header { get; set; }
    public List<CompoundDetailSoap> compound_detail { get; set; }
}

public class PatientHistoryHeader
{
    public DateTime admissionDate { get; set; }
    public string organizationCode { get; set; }
    public string admissionTypeName { get; set; }
    public string doctorName { get; set; }
    public string pageName { get; set; }
    public string PharmacyNotes { get; set; }
    public string AdditionalPharmacyNotes { get; set; }
    public string procedureNotes { get; set; }
}

public class PatientHistoryAnamnesis
{
    public Guid mappingId { get; set; }
    public string mappingName { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
    public int isRevision { get; set; }
}

public class PatientHistoryIllness
{
    public string value { get; set; }
    public string remarks { get; set; }
    public string type { get; set; }
}

public class PatientHistoryPhysicalExam
{
    public Guid mappingId { get; set; }
    public string mappingName { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
    public int isRevision { get; set; }
}

public class PatientHistoryDiagnosis
{
    public Guid mappingId { get; set; }
    public string mappingName { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
    public int isRevision { get; set; }
}

public class PatientHistoryPlanning
{
    public Guid mappingId { get; set; }
    public string mappingName { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
    public int isRevision { get; set; }
}

public class PatientHistoryClinicalFinding
{
    public string type { get; set; }
    public int countData { get; set; }
}

public class PatientHistoryPrescription
{
    public long itemId { get; set; }
    public string salesItemName { get; set; }
    public string quantity { get; set; }
    public string uom { get; set; }
    public string frequency { get; set; }
    public Decimal dose { get; set; }
    public long dose_uom_id { get; set; }
    public string dose_uom { get; set; }
    public string doseText { get; set; }
    public string instruction { get; set; }
    public string route { get; set; }
    public Boolean isRoutine { get; set; }
    public Boolean isConsumables { get; set; }
    public Guid compoundId { get; set; }
    public string compoundName { get; set; }
    public int iter { get; set; }
    public int IsDoctor { get; set; }
    public int IsAdditional { get; set; }
    public bool IsDoseText { get; set; }
}

public class PatientHistoryHOPEemr
{
    public long admissionId { get; set; }
    public string externalAdmissionId { get; set; }
    public long admissionTypeId { get; set; }
    public string admissionNo { get; set; }
    public DateTime admissionDate { get; set; }
    public string entryUser { get; set; }
    public string linkMRHOPE { get; set; }
    public string admTypePlusAdmId { get; set; }
}

public class ResultPatientHistoryHOPEemr
{
    private List<PatientHistoryHOPEemr> lists = new List<PatientHistoryHOPEemr>();
    [JsonProperty("data")]
    public List<PatientHistoryHOPEemr> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class PatientAdmissionType
{
    public long admissionTypeId { get; set; }
    public string admissionTypeName { get; set; }
}

public class OtherUnitMR
{
    public string OrganizationCode { get; set; }
    public string AdmissionNo { get; set; }
    public DateTime AdmissionDate { get; set; }
    public string AdmissionTypeCode { get; set; }
    public string DoctorName { get; set; }
    public string LinkURL { get; set; }
}

public class ResultOtherUnitMR
{
    private List<OtherUnitMR> lists = new List<OtherUnitMR>();
    [JsonProperty("data")]
    public List<OtherUnitMR> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}