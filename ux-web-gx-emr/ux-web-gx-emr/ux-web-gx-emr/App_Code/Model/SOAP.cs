using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for SOAP
/// </summary>


public class ResultSOAP
{
    private SOAP lists = new SOAP();
    [JsonProperty("data")]
    public SOAP list { get { return lists; } set { lists = value; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class ItemDrugs {
    public long SalesItemId { get; set; }
    public string SalesItemCode { get; set; }
    public string SalesItemName { get; set; }
    public string ActiveIngredientsName { get; set; }
    public long SalesUomId { get; set; }
    public string SalesUomCode { get; set; }
    public decimal Dose { get; set; }
    public string DoseText { get; set; }
    public long DoseUomId { get; set; }
    public long AdministrationFrequencyId { get; set; }
    public long AdministrationRouteId { get; set; }
    public string AdministrationInstruction { get; set; }
    public decimal TotalQuantity { get; set; }
    public string Formularium { get; set; }
    public string ConnStatus { get; set; }
    public bool IsIter { get; set; }
}

public class SOAP
{
    public Guid encounter_ticket_id { get; set; }
    public Int64 organization_id { get; set; }
    public Int64 patient_id { get; set; }
    public Int64 admission_id { get; set; }
    public Int64 doctor_id { get; set; }
    public int status_id { get; set; }
    public int save_mode { get; set; }
    public Nullable<DateTime> take_date { get; set; }
    public string pharmacy_notes { get; set; }
    public Nullable<DateTime> verify_date { get; set; }
    public Nullable<DateTime> additional_take_date { get; set; }
    public string additional_pharmacy_notes { get; set; }
    public Int64 consultation_item_id { get; set; }
    public string consultation_item_name { get; set; }
    public Decimal consultation_amount { get; set; }
    public Decimal discount_amount { get; set; }
    public Decimal total_amount { get; set; }
    public string procedure_notes { get; set; }
    public int is_invoice { get; set; }
    public List<Subjective> subjective { get; set; }
    public List<Objective> objective { get; set; }
    public List<Assessment> assessment { get; set; }
    public List<Planning> planning { get; set; }
    public List<CpoeTrans> cpoe_trans { get; set; }
    public List<CpoeNotes> cpoe_notes { get; set; }
    public List<Prescription> prescription { get; set; }
    public List<PatientAllergy> patient_allergy { get; set; }
    public List<PatientSurgery> patient_surgery { get; set; }
    public List<PatientDiseaseHistory> patient_disease { get; set; }
    public List<PatientRoutineMedication> patient_medication { get; set; }
    public List<Prescription> additional_prescription { get; set; }
    public string ModifiedDate { get; set; }
    public int mr_status { get; set; }
    public List<PatientProcedureHistory> patient_procedure { get; set; }
    public List<PatientSpecialNotification> patient_notification { get; set; }

    public List<CompoundHeaderSoap> compound_header { get; set; }
    public List<CompoundDetailSoap> compound_detail { get; set; }

    public List<Vaccination> vaccination { get; set; }

    public List<ProcedureDiagnosis> procedure_diagnosis { get; set; }

    public SingleQueueData sq_data { get; set; }

    public string BackupDate { get; set; }

    public Nullable<DateTime> lab_process_date { get; set; }
    public Nullable<DateTime> rad_process_date { get; set; }

    public List<ReferalData> referal { get; set; }

	public List<InfectiousDisease> infectious_disease { get; set; }
	public List<InfectiousAlert> infectious_alert { get; set; }
    public List<InfectiousMapping> infectious_mapping { get; set; }




}

public class InfectiousMapping
{
    public long infectious_disease_id { get; set; }
    public string infectious_disease_name { get; set; }
    public long alert_type_id { get; set; }
    public string alert_type_name { get; set; }
	public string mapping_remarks { get; set; }
}


public class InfectiousAlert
{
    public Guid patient_alert_id { get; set; }
    public long alert_type_id { get; set; }
    public string alert_type_name { get; set; }
    public bool is_new { get; set; }
    public bool is_delete { get; set; }
}

public class InfectiousDisease
{
    public long infectious_disease_id { get; set; }
    public string infectious_disease_name { get; set; }
    public long infectious_symptoms_id { get; set; }
    public string infectious_symptoms_name { get; set; }
    public Guid patient_symptoms_id { get; set; }
    public bool is_new { get; set; }
    public bool is_delete { get; set; }
}

public class Subjective
{
    public Guid subjective_id { get; set; }
    public Guid soap_mapping_id { get; set; }
    public string soap_mapping_name { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
}

public class Objective
{
    public Guid objective_id { get; set; }
    public Guid soap_mapping_id { get; set; }
    public string soap_mapping_name { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
}

public class Assessment
{
    public Guid assessment_id { get; set; }
    public Guid soap_mapping_id { get; set; }
    public string soap_mapping_name { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
}

public class Planning
{
    public Guid planning_id { get; set; }
    public Guid soap_mapping_id { get; set; }
    public string soap_mapping_name { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
}

public class Prescription
{
    public Guid prescription_id { get; set; }
    public string prescription_no { get; set; }
    public Int64 item_id { get; set; }
    public string item_name { get; set; }
    public string quantity { get; set; }
    public Int64 uom_id { get; set; }
    public string uom_code { get; set; }
    public Int64 frequency_id { get; set; }
    public string frequency_code { get; set; }
    public string dosage_id { get; set; }
    public string dose_text { get; set; }
    public long dose_uom_id { get; set; }
    public string dose_uom { get; set; }
    public Int64 administration_route_id { get; set; }
    public string administration_route_code { get; set; }
    public int iteration { get; set; }
	public bool is_iter { get; set; }
	public string remarks { get; set; }
    public int is_routine { get; set; }
    public int is_consumables { get; set; }
    public Guid compound_id { get; set; }
    public string compound_name { get; set; }
    public Guid origin_prescription_id { get; set; }
    public Int64 hope_arinvoice_id { get; set; }
    public int is_delete { get; set; }
    public Int64 organization_id { get; set; }
    public int item_sequence { get; set; }
    public bool IsDoseText {get;set;}
    public bool IsActive { get; set; }

    public string cims_result { get; set; }

    public Int64 uom_idori { get; set; } = 0;
    public string uom_codeori { get; set; } = "";
    public decimal uom_ratio { get; set; } = 0;
}
public class ResultPrescription
{
    private List<Prescription> lists = new List<Prescription>();
    [JsonProperty("data")]
    public List<Prescription> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}
public class ResultCpoeTrans
{
    private List<CpoeTrans> lists = new List<CpoeTrans>();
    [JsonProperty("data")]
    public List<CpoeTrans> list { get { return lists; } }
}

public class CpoeTrans
{
    public Int64 id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public int isnew { get; set; }
    public int iscito { get; set; }
    public int issubmit { get; set; }
    public int isdelete { get; set; }
    public int ischeck { get; set; }
    public string remarks { get; set; }
    public int IsSendHope { get; set; } = 0;
    public bool IsFutureOrder { get; set; }
    public DateTime? FutureOrderDate { get; set; }
}


public class CpoeNotes
{
    public Guid cpoe_notes_id { get; set; }
    public string notes_type { get; set; }
    public string notes { get; set; }
}

public class PatientAllergy
{
    public Guid patient_allergy_id { get; set; }
    public Int64 allergy_type { get; set; }
    public string allergy { get; set; }
    public string allergy_reaction { get; set; }
    public int is_delete { get; set; }
}

public class PatientSurgery
{
    public Guid patient_surgery_id { get; set; }
    public string surgery_type { get; set; }
    public DateTime surgery_date { get; set; }
    public int is_delete { get; set; }
}


public class FrequentDrug
{
    public long SalesItemId { get; set; }
    public string SalesItemName { get; set; }
    public long uom_id { get; set; }
    public string uom_code { get; set; }
    public Decimal Dose { get; set; }
    public string DoseText { get; set; }
    public long DoseUomId { get; set; }
    public long AdministrationFrequencyId { get; set; }
    public long AdministrationRouteId { get; set; }
    public string AdministrationInstruction { get; set; }
    public string Formularium { get; set; }
	public bool is_iter { get; set; }
}
public class ResultFrequentDrug
{
    private List<FrequentDrug> lists = new List<FrequentDrug>();
    [JsonProperty("data")]
    public List<FrequentDrug> list { get { return lists; } }
}
public class PatientDiseaseHistory
{
    public Guid patient_disease_history_id { get; set; }
    public Int64 disease_history_type { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
    public string status { get; set; }
    public int is_delete { get; set; }
}

public class ResultPatientDiseaseHistory
{
    private List<PatientDiseaseHistory> lists = new List<PatientDiseaseHistory>();
    [JsonProperty("data")]
    public List<PatientDiseaseHistory> list { get { return lists; } }
}

public class PatientRoutineMedication
{
    public Guid patient_routine_medication_id { get; set; }
    public string medication { get; set; }
    public int is_delete { get; set; }
    public long routine_sales_item_id { get; set; }
    public string routine_sales_item_code { get; set; }
}

public class ResultPatientRoutineMedication
{
    //private PatientRoutineMedication lists = new PatientRoutineMedication();
    //[JsonProperty("data")]
    public PatientRoutineMedication list { get; set; }
}


public class ConsultFee
{
    public Int64 sales_item_id { get; set; }
    public string sales_item_name { get; set; }
    public decimal consultation_fee { get; set; }
}

public class ConsultFeestring
{
    public Int64 sales_item_id { get; set; }
    public string sales_item_name { get; set; }
    public string consultation_fee { get; set; }
    public string consulation_fee_name { get; set; }

}
public class ResultConsultFee
{
    private List<ConsultFee> lists = new List<ConsultFee>();
    [JsonProperty("data")]
    public List<ConsultFee> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class ViewAdmissionCopySOAP
{
    public Guid EncounterId { get; set; }
    public long PatientId { get; set; }
    public long OrganizationId { get; set; }
    public long AdmissionId { get; set; }
    public string AdmissionNo { get; set; }
    public string AdmissionDate { get; set; }
    public long DoctorId { get; set; }
    public string DoctorName { get; set; }
    public long SpecialtyId { get; set; }
    public string SpecialtyName { get; set; }
}
public class ResultViewAdmissionCopySOAP
{
    private List<ViewAdmissionCopySOAP> lists = new List<ViewAdmissionCopySOAP>();
    [JsonProperty("data")]
    public List<ViewAdmissionCopySOAP> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class temproutine
{
    public long item_id { get; set; }
    public string medication { get; set; }
}

public class SOAPAdditionalInfo
{
    public List<FrequentDrug> frequentdrug { get; set; }
    public List<OrderSet> ordersetdrug { get; set; }
    public List<OrderSet> ordersetlab { get; set; }
}

public class ResultSOAPAdditionalInfo
{
    private SOAPAdditionalInfo lists = new SOAPAdditionalInfo();
    [JsonProperty("data")]
    public SOAPAdditionalInfo list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class PatientProcedureHistory
{
    public Guid procedure_history_id { get; set; }
    public string procedure_remarks { get; set; }
    public DateTime procedure_date { get; set; }
    public string doctor_name { get; set; }
    public int is_myself { get; set; }
}

public class PatientSpecialNotification
{
    public Guid special_notification_id { get; set; }
    public string notification { get; set; }
    public string doctor_name { get; set; }
    public DateTime start_date { get; set; }
    public DateTime end_date { get; set; }
    public bool is_checked { get; set; }
    public int is_myself { get; set; }
}

public class AdmissionPrescriptionHOPE
{
    public long AdmissionId { get; set; }
    public string AdmissionDate { get; set; }
    public string AdmissionNo { get; set; }
    public string DoctorName { get; set; }
}

public class ResultAdmissionPrescriptionHOPE
{
    private List<AdmissionPrescriptionHOPE> lists = new List<AdmissionPrescriptionHOPE>();
    [JsonProperty("data")]
    public List<AdmissionPrescriptionHOPE> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}


public class ResultViewMedicalEntryHOPE
{
    private ViewMedicalEntryHOPE lists = new ViewMedicalEntryHOPE();
    [JsonProperty("data")]
    public ViewMedicalEntryHOPE list { get { return lists; } }
}


public class ViewMedicalEntryHOPE
{
    public List<MedicalEntryHOPE> medicals { get; set; }
    public List<PrescriptionHOPE> prescriptions { get; set; }
}

public class MedicalEntryHOPE
{
    public short MedicalEntryTypeId { get; set; }
    public string Name { get; set; }
    public string EntryText { get; set; }
    public string entryuser { get; set; }
    public string entryusertype { get; set; }
}

public class PrescriptionHOPE
{
    public Guid prescription_id { get; set; }
    public string prescription_no { get; set; }
    public long item_id { get; set; }
    public string item_name { get; set; }
    public string quantity { get; set; }
    public long uom_id { get; set; }
    public string uom_code { get; set; }
    public long frequency_id { get; set; }
    public string frequency_code { get; set; }
    public string dosage_id { get; set; }
    public long dose_uom_id { get; set; }
    public string dose_uom { get; set; }
    public string dose_text { get; set; }
    public long administration_route_id { get; set; }
    public string administration_route_code { get; set; }
    public int iteration { get; set; }
    public string remarks { get; set; }
    public int is_routine { get; set; }
    public int is_consumables { get; set; }
    public Guid compound_id { get; set; }
    public string compound_name { get; set; }
    public Guid origin_prescription_id { get; set; }
    public long hope_aritem_id { get; set; }
    public int is_delete { get; set; }
    public long organization_id { get; set; }
}

public class CompoundHeaderSoap
{
    public Guid prescription_compound_header_id { get; set; }
    public string compound_name { get; set; }
    public string quantity { get; set; }
    public long uom_id { get; set; }
    public string uom_code { get; set; }
    public string dose { get; set; }
    public long dose_uom_id { get; set; }
    public string dose_uom { get; set; }
    public long administration_frequency_id { get; set; }
    public string frequency_code { get; set; }
    public long administration_route_id { get; set; }
    public string administration_route_code { get; set; }
    public string administration_instruction { get; set; }
    public string compound_note { get; set; }
    public int iter { get; set; }
    public bool is_additional { get; set; }
    public short item_sequence { get; set; }
    public string dose_text { get; set; }
    public bool IsDoseText { get; set; }
}

public class CompoundDetailSoap
{
    public Guid prescription_compound_detail_id { get; set; }
    public Guid prescription_compound_header_id { get; set; }
    public long item_id { get; set; }
    public string item_name { get; set; }
    public string quantity { get; set; }
    public long uom_id { get; set; }
    public string uom_code { get; set; }
    public short item_sequence { get; set; }
    public bool is_additional { get; set; }
    public long organization_id { get; set; }
    public string dose { get; set; }
    public long dose_uom_id { get; set; }
    public string dose_uom_code { get; set; }
    public string dose_text { get; set; }
    public bool IsDoseText { get; set; }
}

public class CompoundOrderSet
{
    public CompoundHeaderSoap header { get; set; }
    public List<CompoundDetailSoap> detail { get; set; }
}

public class ResultCompoundOrderSet
{
    private CompoundOrderSet lists = new CompoundOrderSet();
    [JsonProperty("data")]
    public CompoundOrderSet list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class DiagProcSubmit
{
    public string SalesItemId;
    public string SalesItemTypeId;
    public string SalesItemCode;
    public string SalesItemName;
}

public class ProcedureDiagnosis
{
    public Guid EncounterProcedureId { get; set; }
    public long ProcedureItemId { get; set; }
    public string ProcedureItemName { get; set; }
    public long ProcedureItemTypeId { get; set; }
    public int IsSendHope { get; set; }
    public bool IsFutureOrder { get; set; }
    public DateTime? FutureOrderDate { get; set; }
}

public class SingleQueueData
{
    public string QueueLineDrug { get; set; }
    public string QueueLineLab { get; set; }
    public string QueueLineRad { get; set; }
    public string UrlDetailDrug { get; set; }
    public string UrlDetailLab { get; set; }
    public string UrlDetailRad { get; set; }
    public string UrlDetailMedicalResume { get; set; }
    public string HospitalId { get; set; }
    public string UserId { get; set; }
    // procedure dan diagnostic
    public string UrlDetailProcedureDiagnostic { get; set; }
    public string UrlDetailShortSOAP { get; set; }
}

public class ReferalData
{
    public Guid referral_id { get; set; }
    public string referral_type { get; set; }
    public long referral_doctor_id { get; set; }
    public Guid referral_doctor_id_mysiloam { get; set; }
    public string referral_doctor_name { get; set; }
    public long speciality_id { get; set; }
    public Guid speciality_id_mysiloam { get; set; }
    public string speciality_name { get; set; }
    public string referral_remark { get; set; }
    public bool IsProcess { get; set; }
    public int is_new { get; set; }
    public int is_delete { get; set; }
    public string referral_target { get; set; }
    public string external_referral_to { get; set; }
    public string external_referral_place { get; set; }
    public string external_referral_date { get; set; }
    public string external_referral_time { get; set; }
    public string external_referral_reason { get; set; }
    public int is_editable { get; set; } 
    public string created_date { get; set; } 
    public string referal_status { get; set; }
}
public class ProcedureData
{
    public int operationprocedure_id { set; get; }
    public string procedure_name { set; get; }
    public string diagnosis { set; get; }
    public string site { set; get; }
    public string keyword_translation { set; get; }

}
public class AnestesiData
{
    public string anesthetia_type_name { set; get; }
    public int anesthetia_duration { set; get; }
    public int anestesi_id { set; get; }
}

public class ProcedureResponse
{
    public List<ProcedureData> procedure { set; get; }
    public List<AnestesiData> anestesi { set; get; }
}

public class ResultProcedureResponse
{
    private ProcedureResponse lists = new ProcedureResponse();
    [JsonProperty("data")]
    public ProcedureResponse list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }

}


