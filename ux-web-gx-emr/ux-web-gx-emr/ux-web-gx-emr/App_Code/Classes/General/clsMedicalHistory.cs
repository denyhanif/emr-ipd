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
/// Summary description for clsMedicalHistory
/// </summary>
public class clsMedicalHistory
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public static async Task<string> getMedicalHistory (String patientId, Int64 type, String value)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/medicationhistory/" + patientId + "/" + type + "/" + value));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientId", patientId.ToString(), "getMedicalHistory", StartTime, "OK", MyUser.GetUsername(), "/" + patientId + "/" + type.ToString() + "/" + value, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientId", patientId.ToString(), "getMedicalHistory", StartTime, "ERROR", MyUser.GetUsername(), "/" + patientId + "/" + type.ToString() + "/" + value, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getMedicalHistoryNew(String patientId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/medicationhistory/" + patientId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientId", patientId.ToString(), "getMedicalHistoryNew", StartTime, "OK", MyUser.GetUsername(), "/" + patientId.ToString() , "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientId", patientId.ToString(), "getMedicalHistoryNew", StartTime, "ERROR", MyUser.GetUsername(), "/" + patientId.ToString() , "", ex.Message));
            return ex.Message;
        }
    }
}