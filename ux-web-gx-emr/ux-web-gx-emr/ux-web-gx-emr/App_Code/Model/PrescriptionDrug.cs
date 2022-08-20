using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for PrescriptionDrug
/// </summary>
public class PrescriptionDrug
{
    public Nullable<Guid> prescription_id { get; set; }
    public Guid encounter_ticket_id { get; set; }
    public Nullable<Guid> origin_prescription_id { get; set; }
    public string prescription_no { get; set; }
    //Detail
    public Int64 item_id { get; set; }
    public string item_code { get; set; }
    public string item_name { get; set; }
    public Decimal quantity { get; set; }
    public Int64 UomId { get; set; }
    public string uom_code { get; set; }
    public Int64 frequency_id { get; set; }
    public string frequency_code { get; set; }
    public Int64 dosage_id { get; set; }
    public string dose_text { get; set; }
    public Nullable<Int64> AdministrationRouteId { get; set; }
    public string AdministrationRouteCode { get; set; }
    public Nullable<int> Iteration { get; set; }
    public string remarks { get; set; }
    public string ActiveIngredientsName { get; set; }
    public Boolean is_routine { get; set; }
    public Nullable<Int64> hope_admission_id { get; set; }
    public Nullable<Int64> hope_arinvoice_id { get; set; }
    public Nullable<Guid> compound_id { get; set; }
    //for populate data dropdown uom
    public Int64 Uom1Id { get; set; }
    public string uom1_code { get; set; }
    public Nullable<Int64> Uom2Id { get; set; }
    public string uom2_code { get; set; }
    public Nullable<Decimal> Uom2Ratio { get; set; }
    public Nullable<Int64> Uom3Id { get; set; }
    public string uom3_code { get; set; }
    public Nullable<Decimal> Uom3Ratio { get; set; }
    public Nullable<Int64> Uom4Id { get; set; }
    public string uom4_code { get; set; }
    public Nullable<Decimal> Uom4Ratio { get; set; }
    public Nullable<Int64> Uom5Id { get; set; }
    public string uom5_code { get; set; }
    public Nullable<Decimal> Uom5Ratio { get; set; }

    public Nullable<Guid> order_set_id { get; set; }
    public string order_set_name { get; set; }
    public Int64 doctor_id { get; set; }
    public Decimal compound_quantity { get; set; }
    public Int64 compound_uom_id { get; set; }
    public string compound_uom_code { get; set; }
    public Int64 compound_dosage_id { get; set; }
    public string compound_dose_text { get; set; }
    public Int64 compound_frequency_id { get; set; }
    public string compound_frequency_code { get; set; }
    public Nullable<Int64> compound_AdministrationRouteId { get; set; }
    public string compound_AdministrationRouteCode { get; set; }
    public Nullable<int> compound_Iteration { get; set; }
    public string compound_remarks { get; set; }
    //Detail
    public Nullable<Guid> order_set_detail_id { get; set; }
    public Nullable<Int64> OrganizationId { get; set; }

}

public class ResultPrescriptionDrug
{
    private List<PrescriptionDrug> lists = new List<PrescriptionDrug>();
    [JsonProperty("data")]
    public List<PrescriptionDrug> list { get { return lists; } }
}