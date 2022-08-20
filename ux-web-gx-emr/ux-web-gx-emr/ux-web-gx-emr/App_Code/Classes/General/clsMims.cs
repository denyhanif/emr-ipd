using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using log4net;
using System.Reflection;

/// <summary>
/// Summary description for clsMims
/// </summary>
public class clsMims
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public static async Task<string> CheckDrugInteraction(MimsModelWithDrugDetail data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(data);
        var httpContent = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient http_ = new HttpClient();
            http_.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMims"].ToString()); ////"http://10.83.254.38:5609"

            http_.DefaultRequestHeaders.Accept.Clear();
            http_.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http_.PostAsync(string.Format($"/cims-interaction-with-log-2"), httpContent);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionId", data.admissionId.ToString(), "CheckDrugInteraction", StartTime, "OK", MyUser.GetUsername(), "", JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionId", data.admissionId.ToString(), "CheckDrugInteraction", StartTime, "ERROR", MyUser.GetUsername(), "", JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> PostLogMims(LogMimsModel data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(data);
        var httpContent = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient http_ = new HttpClient();
            http_.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMims"].ToString()); 

            http_.DefaultRequestHeaders.Accept.Clear();
            http_.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http_.PostAsync(string.Format($"/log-mims"), httpContent);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionId", data.LogHeader.AdmissionId.ToString(), "PostLogMims", StartTime, "OK", MyUser.GetUsername(), "", JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionId", data.LogHeader.AdmissionId.ToString(), "PostLogMims", StartTime, "ERROR", MyUser.GetUsername(), "", JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetLogMims(long AdmissionId, long OrganizationId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            HttpClient http_ = new HttpClient();

            http_.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMims"].ToString()); 

            http_.DefaultRequestHeaders.Accept.Clear();
            http_.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http_.GetAsync(string.Format($"/log-mims/" + AdmissionId + "/" + OrganizationId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionId", AdmissionId.ToString(), "GetLogMims", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionId", AdmissionId.ToString(), "GetLogMims", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetReasonMims()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            HttpClient http_ = new HttpClient();

            http_.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMims"].ToString()); 

            http_.DefaultRequestHeaders.Accept.Clear();
            http_.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http_.GetAsync(string.Format($"/log-mims-reason"));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "", "", "GetReasonMims", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "", "", "GetReasonMims", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            return ex.Message;
        }
    }
}