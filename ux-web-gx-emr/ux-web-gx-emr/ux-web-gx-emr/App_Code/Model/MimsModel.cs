using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MimsModel
/// </summary>
public class MimsModel
{
    public Int64 organizationId { get; set; }
    public Int64 admissionId { get; set; }
    public string createdBy { get; set; }
    public List<Int64> salesItemIds { get; set; }
    public List<Int64> allergyIds { get; set; }
    public string gender { get; set; }
    public int monthOfPregnancy { get; set; }
    public int age { get; set; }
    public bool nursing { get; set; }
}

public class MimsSeverity
{
    public string name { get; set; }
    public int value { get; set; }
}

public class DrugsInteraction
{
    public Int64 salesItemId { get; set; }
    public string itemName { get; set; }
    public string cimsGuid { get; set; }
    public string cimsType { get; set; }
    public bool drugToDrugInteraction { get; set; }
    public bool drugToAllergyInteraction { get; set; }
    public bool drugToHealthInteraction { get; set; }
    public bool duplicateIngredient { get; set; }
    public bool duplicateTherapy { get; set; }
    public bool drugToPregnancyInteraction { get; set; }
    public bool drugToLactationInteraction { get; set; }
    public string drugToDrugSeverity { get; set; }
    public string drugToAllergySeverity { get; set; }
    public string drugToHealthSeverity { get; set; }
    public string duplicateIngredientSeverity { get; set; }
    public string duplicateTherapySeverity { get; set; }
    public string drugToPregnancySeverity { get; set; }
    public string drugToLactationSeverity { get; set; }
}

public class LogsInteraction
{
    public string interaction { get; set; }
    public string severity { get; set; }
    public string level { get; set; }
    public string prescribingDrug { get; set; }
    public string interactingDrug { get; set; }
}

public class MimsInteraction
{
    public string htmlResult { get; set; }
    public List<DrugsInteraction> drugsInteraction { get; set; }
    public string disclaimer { get; set; }
}
public class MimsInteractionWithLog
{
    public string htmlResult { get; set; }
    public List<DrugsInteraction> drugsInteraction { get; set; }
    public List<LogsInteraction> logsInteraction { get; set; }
    public string disclaimer { get; set; }
}

public class ResponseMimsInteraction
{
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
    public MimsInteractionWithLog Data { set; get; }
}

public class MimsModelWithDrugDetail : MimsModel
{
    public LogDrugHeaderModel LogHeader { set; get; }
    public List<LogDrugDetailModel> LogDetail { set; get; }
}

public class LogDrugHeaderModel
{
    public long LogDrugHeaderId { set; get; }
    public DateTime LogDate { set; get; }
    public long OrganizationId { set; get; }
    public string OrganizationName { set; get; }
    public string Modul { set; get; }
    public string UserName { set; get; }
    public string FullName { set; get; }
    public long AdmissionId { set; get; }
    public string AdmissionNo { set; get; }
    public string MrNo { set; get; }
    public bool IsLatest { set; get; }
    public string CreatedBy { set; get; }
    public DateTime CreatedDate { set; get; }

}

public class LogDrugDetailModel
{
    public long LogDrugDetailId { set; get; }
    public long LogDrugHeaderId { set; get; }
    public int Revision { set; get; }
    public bool IsAdditional { set; get; }
    public string ItemType { set; get; }
    public long ItemId { set; get; }
    public string ItemName { set; get; }
    public string Dose { set; get; }
    public string Frequency { set; get; }
    public string Route { set; get; }
    public string Instruction { set; get; }
    public string Qty { set; get; }
    public string Uom { set; get; }
    public int Iter { set; get; }
    public string Routine { set; get; }
    public bool IsLatest { set; get; }
    public string CreatedBy { set; get; }
    public DateTime CreatedDate { set; get; }
}