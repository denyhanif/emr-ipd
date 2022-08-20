using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for LogConfig
/// </summary>
public class LogConfig
{
    public static string ParamDivider = "/";
    private static string spasi = "\n         ";

    #region START
    public static string LogStart()
    {
        string logmsg = "[START]";
        logmsg = logmsg + spasi + "Username   : " + MyUser.GetUsername();

        return logmsg;
    }

    public static string LogStart(string Info)
    {
        string logmsg = "[START]";
        logmsg = logmsg
            + spasi + "Username   : " + MyUser.GetUsername()
            + spasi + "Info       : " + Info;

        return logmsg;
    }

    public static string LogStart(string MethodApiName, string Param)
    {
        string logmsg = "[START]";
        logmsg = logmsg
            + spasi + "Username   : " + MyUser.GetUsername()
            + spasi + "Method API : " + MethodApiName
            + spasi + "Parameter  : " + Param;

        return logmsg;
    }

    public static string LogStart(string MethodApiName, string Param, string ParamJson)
    {
        string logmsg = "[START]";
        logmsg = logmsg
            + spasi + "Username   : " + MyUser.GetUsername()
            + spasi + "Method API : " + MethodApiName
            + spasi + "Parameter  : " + Param
            + spasi + "JSON       : " + ParamJson;

        return logmsg;
    }

    public static string LogStart(string MethodApiName, Dictionary<string, string> parameters)
    {
        string ParamList = "";
        if (parameters != null)
        {
            if (parameters.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var param in parameters)
                {
                    sb.Append(string.Format("{0}: {1},", param.Key.ToUpper(), param.Value));
                }

                ParamList += sb.ToString().TrimEnd(',');
            }
        }

        string logmsg = "[START]";
        logmsg = logmsg
            + spasi + "Username   : " + MyUser.GetUsername()
            + spasi + "Method API : " + MethodApiName
            + spasi + "Parameter  : " + ParamList;

        return logmsg;
    }

    public static string LogStart(string MethodApiName, Dictionary<string, string> parameters, string ParamJson)
    {
        string ParamList = "";
        if (parameters != null)
        {
            if (parameters.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var param in parameters)
                {
                    sb.Append(string.Format("{0}: {1},", param.Key.ToUpper(), param.Value));
                }

                ParamList += sb.ToString().TrimEnd(',');
            }
        }

        string logmsg = "[START]";
        logmsg = logmsg
            + spasi + "Username   : " + MyUser.GetUsername()
            + spasi + "Method API : " + MethodApiName
            + spasi + "Parameter  : " + ParamList
            + spasi + "JSON       : " + ParamJson;

        return logmsg;
    }
    #endregion

    #region END
    public static string LogEnd()
    {
        string logmsg = "[END]";
        logmsg = logmsg + spasi + "Username   : " + MyUser.GetUsername();

        return logmsg;
    }

    public static string LogEnd(string MethodApiName, string Status, string Message)
    {
        string logmsg = "[END]";
        logmsg = logmsg
            + spasi + "Username   : " + MyUser.GetUsername()
            + spasi + "Method API : " + MethodApiName
            + spasi + "Status     : " + Status
            + spasi + "Message    : " + Message;

        return logmsg;
    }
    #endregion

    #region ERROR
    public static string LogError()
    {
        string logmsg = "[ERROR]";
        logmsg = logmsg + spasi + "Username   : " + MyUser.GetUsername();

        return logmsg;
    }

    public static string LogError(string ExceptionMessage)
    {
        string logmsg = "[ERROR]";
        logmsg = logmsg
            + spasi + "Username   : " + MyUser.GetUsername()
            + spasi + "Exception  : " + ExceptionMessage;
        return logmsg;
    }
    #endregion

    public static string JsonToString(dynamic jsonobj)
    {
        var JsonString = JsonConvert.SerializeObject(jsonobj);
        return JsonString;
    }

    public static Dictionary<string, string> LogParam(string title, string value)
    {
        Dictionary<string, string> ParamList = new Dictionary<string, string>
        {
            { title, value }
        };

        return ParamList;
    }
}