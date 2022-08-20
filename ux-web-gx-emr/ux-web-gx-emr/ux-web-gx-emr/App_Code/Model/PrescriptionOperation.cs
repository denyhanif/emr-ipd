using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PrescriptionOperation
/// </summary>
public class PrescriptionOperation
{
    public string prescription_operation { get; set; }
    public Nullable<Guid> prescription_id { get; set; }
    public Guid encounter_ticket_id { get; set; }
    public Nullable<Guid> origin_prescription_id { get; set; }
    public Nullable<Guid> compound_id { get; set; }
    public string prescription_no { get; set; }
    public Int64 item_id { get; set; }
    public Decimal quantity { get; set; }
    public Int64 uom_id { get; set; }
    public Int64 dosage_id { get; set; }
    public Int64 frequency_id { get; set; }
    public string dose_text { get; set; }
    public Nullable<Int64> AdministrationRouteId { get; set; }
    public Nullable<int> Iteration { get; set; }
    public Boolean is_routine { get; set; }
    public string remarks { get; set; }
    public Boolean is_consumables { get; set; }
    public string created_by { get; set; }
    public string modified_by { get; set; }
}