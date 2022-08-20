using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LogMimsModel
/// </summary>
public class LogMimsModel
{
    public LogMimsHeaderModel LogHeader { set; get; }
    public List<LogMimsDetailModel> LogDetail { set; get; }
}

public class LogMimsHeaderModel
{
    public long LogMimsHeaderId { set; get; }
    public DateTime LogDate { set; get; }
    public long OrganizationId { set; get; }
    public string OrganizationName { set; get; }
    public string Modul { set; get; }
    public string UserName { set; get; }
    public string FullName { set; get; }
    public long AdmissionId { set; get; }
    public string AdmissionNo { set; get; }
    public string MrNo { set; get; }
    public string Action { set; get; }
    public string ContinueReason1 { set; get; }
    public string ContinueReason2 { set; get; }
    public string ContinueReasonOther { set; get; }
    public bool IsLatest { set; get; }
    public string CreatedBy { set; get; }
    public DateTime CreatedDate { set; get; }

}

public class LogMimsDetailModel
{
    public long LogMimsDetailId { set; get; }
    public long LogMimsHeaderId { set; get; }
    public string Interaction { set; get; }
    public string Severity { set; get; }
    public string Drug { set; get; }
    public string InteractingWith { set; get; }
    public bool IsLatest { set; get; }
    public string CreatedBy { set; get; }
    public DateTime CreatedDate { set; get; }
}