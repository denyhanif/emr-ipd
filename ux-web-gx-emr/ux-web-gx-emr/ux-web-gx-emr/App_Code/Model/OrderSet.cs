using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for OrderSet
/// </summary>
public class OrderSet
{
    public string id { get; set; }
    public string set_name { get; set; }
    public string item_list { get; set; }
    public string created_date { get; set; }
}

public class ResultOrderSet
{
    private List<OrderSet> lists = new List<OrderSet>();
    [JsonProperty("data")]
    public List<OrderSet> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}


public class InsertOrderSet
{
    //for check compound
    public Boolean is_compound { get; set; }
    //for search data header and search data
    public Nullable<Guid> order_set_id { get; set; }
    public Int64 doctor_id { get; set; }
    public string order_set_name { get; set; }
    //for edit data header
    public Decimal compound_quantity { get; set; }
    public Int64 compound_uom_id { get; set; }
    public Decimal compound_dosage_id { get; set; }
    public Int64 compound_dose_uom_id { get; set; }
    public string compound_dose_text { get; set; }
    public Int64 compound_frequency_id { get; set; }
    public Nullable<Int64> compound_administration_route_id { get; set; }
    public Nullable<int> compound_Iteration { get; set; }
    public string compound_remarks { get; set; }
    public bool IsDoseTextHeader { get; set; }
    //for insert/edit/delete data detail
    public Nullable<Guid> order_set_detail_id { get; set; }
    public Int64 item_id { get; set; }
    public string item_name { get; set; }
    public string quantity { get; set; }
    public Int64 uom_id { get; set; }
    public string dosage_id { get; set; }
    public Int64 dose_uom_id { get; set; }
    public string dose_text { get; set; }
    public Int64 frequency_id { get; set; }
    public Nullable<Int64> administration_route_id { get; set; }
    public Nullable<int> iteration { get; set; }
    public string remarks { get; set; }
    public string type { get; set; }
    public string created_by { get; set; }
    public string modified_by { get; set; }
    public int item_sequence { get; set; }
    public string compound_note { get; set; }
    public bool IsDoseTextDetail { get; set; }
}

public class ResultInsertOrderSet
{ 
    private List<InsertOrderSet> lists = new List<InsertOrderSet>();
    [JsonProperty("data")]
    public List<InsertOrderSet> list { get { return lists; } }
}


public class UOM
{

    public Int64? uomId { get; set; }
    public String code { get; set; }
    public String name { get; set; }

}

public class ResultUOM
{
    private List<UOM> lists = new List<UOM>();
    [JsonProperty("data")]
    public List<UOM> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}


public class Dose
{

    public Int64? doseUomId { get; set; }
    public String code { get; set; }
    public String name { get; set; }

}

public class ResultDose
{
    private List<Dose> lists = new List<Dose>();
    [JsonProperty("data")]
    public List<Dose> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class Frequency
{

    public Int64? administrationFrequencyId { get; set; }
    public String code { get; set; }
    public String name { get; set; }

}

public class ResultFrequency
{
    private List<Frequency> lists = new List<Frequency>();
    [JsonProperty("data")]
    public List<Frequency> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}


public class AdministrationRoute
{

    public Int64? administration_route_id { get; set; }
    public String code { get; set; }
    public String name { get; set; }
}

public class ResultAdministrationRoute
{
    private List<AdministrationRoute> lists = new List<AdministrationRoute>();
    [JsonProperty("data")]
    public List<AdministrationRoute> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class ItemLite
{

    public Int64? salesItemId { get; set; }
    public String salesItemName { get; set; }
    public String activeIngredientsName { get; set; }
    public long salesUomId { get; set; }
    public String salesUomCode { get; set; }
}

public class ResultItemLite
{
    private List<ItemLite> lists = new List<ItemLite>();
    [JsonProperty("data")]
    public List<ItemLite> list { get { return lists; } }
}