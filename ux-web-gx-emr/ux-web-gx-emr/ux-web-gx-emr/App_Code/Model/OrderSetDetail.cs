using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for OrderSetDetail
/// </summary>
public class OrderSetDetail
{
    public string order_set_id { get; set; }
    public string order_set_name { get; set; }
    public Int64? doctor_id { get; set; }
    public string order_set_detail_id { get; set; }
    public long? organizationId { get; set; }
    public Int64? item_id { get; set; }
    public string item_code { get; set; }
    public string item_name { get; set; }
    public string quantity { get; set; }
    public Int64? uomId { get; set; }
    public string uom_code { get; set; }
    public Int64? frequency_id { get; set; }
    public string frequency_code { get; set; }
    public string dosage_id { get; set; }
    public string dose_text { get; set; }
    public Int64? dose_uom_id { get; set; }
    public string dose_uom { get; set; }
    public Int64? administration_route_id { get; set; }
    public string administration_route_code { get; set; }
    public Int64? Iteration { get; set; }
    public string remarks { get; set; }
    public string activeIngredientsName { get; set; }
    public Int64? uom1Id { get; set; }
    public string uom1_code { get; set; }
    public Int64? uom2Id { get; set; }
    public string uom2_code { get; set; }
    public string uom2Ratio { get; set; }
    public Int64? uom3Id { get; set; }
    public string uom3_code { get; set; }
    public string uom3Ratio { get; set; }
    public Int64? uom4Id { get; set; }
    public string uom4_code { get; set; }
    public string uom4Ratio { get; set; }
    public Int64? uom5Id { get; set; }
    public string uom5_code { get; set; }
    public string uom5Ratio { get; set; }
    public string formularium { get; set; }
    public bool isDoseTextDetail { get; set; }
}

public class ResultOrderSetDetail
{
    private List<OrderSetDetail> lists = new List<OrderSetDetail>();
    [JsonProperty("data")]
    public List<OrderSetDetail> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}


public class DrugsDetail
{
    public string order_set_id { get; set; }
    public string order_set_name { get; set; }
    public Int64? doctor_id { get; set; }
    public string order_set_detail_id { get; set; }
    public Int64? item_id { get; set; }
    public string item_name { get; set; }
    public Decimal? quantity { get; set; }
    public Int64? uomId { get; set; }
    public string uom_code { get; set; }
    public Int64? frequency_id { get; set; }
    public string frequency_code { get; set; }
    public Int64? dosage_id { get; set; }
    public string dose_code { get; set; }
    public string dose_text { get; set; }
    public Int64? AdministrationRouteId { get; set; }
    public string administrationRouteCode { get; set; }
    public Int64? iteration { get; set; }
    public string remarks { get; set; }
    public string compound_id { get; set; }
    public string activeIngredientsName { get; set; }
    public Int64? uom1Id { get; set; }
    public string uom1_code { get; set; }
    public Int64? uom2Id { get; set; }
    public string uom2_code { get; set; }
    public Int64? uom3Id { get; set; }
    public string uom3_code { get; set; }
    public Int64? uom4Id { get; set; }
    public string uom4_code { get; set; }
    public Int64? uom5Id { get; set; }
    public string uom5_code { get; set; }
}

public class ResultDrugsDetail
{
    private List<DrugsDetail> lists = new List<DrugsDetail>();
    [JsonProperty("data")]
    public List<DrugsDetail> list { get { return lists; } }
}


public class CompoundDetail
{
    public Nullable<Guid> order_set_id { get; set; }
    public string order_set_name { get; set; }
    public Int64 doctor_id { get; set; }
    public Decimal compound_quantity { get; set; }
    public Int64 compound_uom_id { get; set; }
    public string compound_uom_code { get; set; }
    public Decimal compound_dosage_id { get; set; }
    public string compound_dose_text { get; set; }
    public Int64 compound_dose_uom_id { get; set; }
    public string compound_dose_uom { get; set; }
    public Int64 compound_frequency_id { get; set; }
    public string compound_frequency_code { get; set; }
    public Nullable<Int64> compound_administration_route_id { get; set; }
    public string compound_administration_route_code { get; set; }
    public Nullable<int> compound_iteration { get; set; }
    public string compound_remarks { get; set; }
    public bool IsDoseTextHeader { get; set; }
    //Detail
    public Nullable<Guid> order_set_detail_id { get; set; }
    public Nullable<Int64> OrganizationId { get; set; }
    public Int64 item_id { get; set; }
    public string item_code { get; set; }
    public string item_name { get; set; }
    public string quantity { get; set; }
    public Int64 UomId { get; set; }
    public string uom_code { get; set; }
    public Int64 frequency_id { get; set; }
    public string frequency_code { get; set; }
    public Decimal dosage_id { get; set; }
    public string dose_text { get; set; }
    public Int64 dose_uom_id { get; set; }
    public string dose_uom { get; set; }
    public Nullable<Int64> administration_route_id { get; set; }
    public string administration_route_code { get; set; }
    public Nullable<int> iteration { get; set; }
    public string remarks { get; set; }
    public string ActiveIngredientsName { get; set; }
    public int item_sequence { get; set; }
    public string compound_note { get; set; } 
    public bool IsDoseTextDetail { get; set; }
}

public class ResultCompoundDetail
{
    private List<CompoundDetail> lists = new List<CompoundDetail>();
    [JsonProperty("data")]
    public List<CompoundDetail> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class LaboratoryInsert
{
    public String OrderSetId { get; set; }
    public String OrderSetName { get; set; }
    public Int64 DoctorId { get; set; }
    public List<CpoeTrans> model { get; set; }
}

public class ResultLaboratoryInsert
{
    private List<LaboratoryInsert> lists = new List<LaboratoryInsert>();
    [JsonProperty("data")]
    public List<LaboratoryInsert> list { get { return lists; } }
}