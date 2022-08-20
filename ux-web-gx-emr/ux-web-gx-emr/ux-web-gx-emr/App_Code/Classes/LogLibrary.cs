using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LogLibrary
/// </summary>
public static class LogLibrary
{
    public static string Logging(string Status, string Function, string UserName, string Message)
    {
        if (Status == "S")
        {
            Status = "START";
        }
        else if (Status == "E")
        {
            Status = "END";
        }
        return Status + " - " + Function + " \n\t\t Username: " + UserName + " \n\t\t Message: " + Message;
    }

    public static string Error(string Function, string UserName, string Message)
    {
        return Function + " \n\t\t Username: " + UserName + " \n\t\t Message: " + Message;
    }


    public static string SaveLog(string OrgId, string ReferenceCode, string ReferenceNo, string method, string StartTime, string EndTime, string Type, string User, string QueryString, string JsonRequest, string Response)
    {
        string result = OrgId + ";" + ReferenceCode + ";" + ReferenceNo + ";" + method + ";" + StartTime + ";" + EndTime + ";" + User + ";" + Type + ";" + QueryString + ";" + JsonRequest + ";" + "";

        return result;
    }

    public static string SaveLogNew(string OrgId, string ReferenceCode, string ReferenceNo, string method, string StartTime, string Type, string User, string QueryString, string JsonRequest, string Response)
    {
        string result = OrgId + ";" + ReferenceCode + ";" + ReferenceNo + ";" + method + ";" + StartTime + ";" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ";" + User + ";" + Type + ";" + QueryString + ";" + JsonRequest + ";" + "";

        return result;
    }

    public static string SaveLogMims(string AdmNo, string MrNo, string PatientName, string Action)
    {
        //OrgId;Orgname;Modul;Username;Fullname;Specialization;Admno;MrNo;PatientName;Action;ActionDate;
        string Modul = "EMR Doctor";
        string result = MyUser.GetHopeOrgID() + ";" + MyUser.GetOrgName() + ";" + Modul + ";" + MyUser.GetUsername() + ";" + MyUser.GetFullname() + ";" + AdmNo + ";" + MrNo + ";" + PatientName + ";" + Action + ";" + DateTime.Now.ToString();

        return result;
    }
}