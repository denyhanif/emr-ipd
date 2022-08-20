using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReasonMimsModel
/// </summary>
public class ReasonMimsModel
{
    public long MimsReasonId { set; get; }
    public string Reason { set; get; }
    public bool IsActive { set; get; }
    public int Sequence { set; get; }
    public string CreatedBy { set; get; }
    public DateTime CreatedDate { set; get; }
    public string ModifiedBy { set; get; }
    public DateTime ModifiedDate { set; get; }
    public bool IsChecked { set; get; } = false;
}

public class ResponseReasonMimsModel
{
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
    public List<ReasonMimsModel> Data { set; get; }
}