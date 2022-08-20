using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using log4net;
using System.Reflection;

/// <summary>
/// Summary description for clsTemplateSet
/// </summary>
public class clsTemplateSet
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public static async Task<string> getAllTemplateSet(Int64 doctorId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient templateSet = new HttpClient();
            templateSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            templateSet.DefaultRequestHeaders.Accept.Clear();
            templateSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var task = Task.Run(async () =>
            {
                return await templateSet.GetAsync(string.Format($"/soaptemplate/" + doctorId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "getAllTemplateSet", StartTime, "OK", MyUser.GetUsername(), "/" + doctorId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "getAllTemplateSet", StartTime, "ERROR", MyUser.GetUsername(), "/" + doctorId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getSpesificTemplateSet(Int64 doctorId, Guid mappingId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient templateSet = new HttpClient();
            templateSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            templateSet.DefaultRequestHeaders.Accept.Clear();
            templateSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var task = Task.Run(async () =>
            {
                return await templateSet.GetAsync(string.Format($"/soaptemplate/" + doctorId + "/" + mappingId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "getAllTemplateSet", StartTime, "OK", MyUser.GetUsername(), "/" + doctorId.ToString() + "/" + mappingId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "getAllTemplateSet", StartTime, "ERROR", MyUser.GetUsername(), "/" + doctorId.ToString() + "/" + mappingId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }
}