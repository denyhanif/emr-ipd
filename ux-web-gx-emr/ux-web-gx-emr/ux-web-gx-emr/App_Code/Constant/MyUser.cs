using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MyUser
/// </summary>
public class MyUser
{
    //public static PharmacyCentralLoginModel Data { get; set; }

    //private static Login Data;
    public static void SetLoginSession(Login logindata)
    {
        //Data = logindata;
        HttpContext.Current.Session[CstSession.SessionLogin] = logindata;
    }

    //--------------------------------------------------------------

    public static Login LoginData()
    {
        Login Data = (Login)HttpContext.Current.Session[CstSession.SessionLogin];
        return Data;
    }

    public static string GetUsername()
    {
        Login Data = (Login)HttpContext.Current.Session[CstSession.SessionLogin];
        return Data == null ? "Guest" : Data.user_name;
    }

    public static string GetFullname()
    {
        Login Data = (Login)HttpContext.Current.Session[CstSession.SessionLogin];
        return Data.full_name;
    }

    public static string GetHopeUserID()
    {
        Login Data = (Login)HttpContext.Current.Session[CstSession.SessionLogin];
        return Data.hope_user_id.ToString();
    }

    public static string GetHopeOrgID()
    {
            if (HttpContext.Current.Session[CstSession.SessionLogin] != null)
            {
                Login Data = (Login)HttpContext.Current.Session[CstSession.SessionLogin];
                return Data == null ? "-1" : Data.hope_organization_id.ToString();
            }
            else
            {
                return "-1";
            }
    }

    public static string GetOrgName()
    {
        Login Data = (Login)HttpContext.Current.Session[CstSession.SessionLogin];
        return Data.organization_name;
    }

    public static string GetEmail()
    {
        Login Data = (Login)HttpContext.Current.Session[CstSession.SessionLogin];
        return Data.email;
    }

    public static string GetHopeOrgIDLog()
    {
        if (HttpContext.Current.Session[CstSession.SessionLogin] != null)
        {
            Login Data = (Login)HttpContext.Current.Session[CstSession.SessionLogin];
            if (Data == null)
            {
                return "-111";
            }
            else
            {
                string orgid = Data.hope_organization_id.ToString();
                if (orgid.Length == 1)
                {
                    orgid = "00" + orgid;
                }
                else if (orgid.Length == 2)
                {
                    orgid = "0" + orgid;
                }

                return orgid;
            }
        }
        else
        {
            return "-111";
        }
    }
}