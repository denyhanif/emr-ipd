using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for Item
/// </summary>
public class Item
{
    public Int64 salesItemId { get; set; }
    public Int64 salesItemTypeId { get; set; }
    public Int64 salesItemGroupId { get; set; }
    public string salesItemCode { get; set; }
    public string salesItemName { get; set; }
    public string alternateCode1 { get; set; }
    public string alternateName1 { get; set; }
    public Int64? totalQuantity { get; set; }
    public Int64? uom1Id { get; set; }
    public string uom1 { get; set; }
    public Int64? uom2Id { get; set; }
    public string uom2 { get; set; }
    public Int64? uom3Id { get; set; }
    public string uom3 { get; set; }
    public Int64? uom4Id { get; set; }
    public string uom4 { get; set; }
    public Int64? uom5Id { get; set; }
    public string uom5 { get; set; }
    public string administrationRoute { get; set; }
    public string activeIngredientsName { get; set; }
    public long salesUomId { get; set; }
    public String salesUomCode { get; set; }
}

public class ResultItem
{
    private List<Item> lists = new List<Item>();
    [JsonProperty("data")]
    public List<Item> list { get { return lists; } }
}

public class gridItem
{
    public string salesItemName { get; set; }
    public Int64? salesItemId { get; set; }
    public string doze { get; set; }
    public Int64? dozeuomId { get; set; }
    public string dose_uom { get; set; }
    public Int64? frequencyId { get; set; }
    public string frequency { get; set; }
    public Int64? routeId { get; set; }
    public string route { get; set; }
    public string instruction { get; set; }
    public string quantity { get; set; }
    public Int64? uomId { get; set; }
    public string uom { get; set; }
    public Int64? iteration { get; set; }
    public string order_set_detail_id { get; set; }
    public String formularium { get; set; }
    public int item_sequence { get; set; }
    public string compound_note { get; set; }
    public bool IsDoseTextDetail { get; set; }
    public string dose_text { get; set; }
}

public class ResultgridItem
{
    private List<gridItem> lists = new List<gridItem>();
    [JsonProperty("data")]
    public List<gridItem> list { get { return lists; } }
}