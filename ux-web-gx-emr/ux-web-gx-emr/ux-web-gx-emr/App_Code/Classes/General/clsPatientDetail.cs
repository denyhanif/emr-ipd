using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using log4net;
using System.Reflection;
/// <summary>
/// Summary description for clsPatientDetail
/// </summary>
public class clsPatientDetail
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public static async Task<string> GetPatientHistory(long PatientId, string EncounterId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlFunctional"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/patienthistory/" + PatientId + "/" + EncounterId + "/"));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "GetPatientHistory", StartTime, "OK", MyUser.GetUsername(), "/" + PatientId.ToString() + "/" + EncounterId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "GetPatientHistory", StartTime, "ERROR", MyUser.GetUsername(), "/" + PatientId.ToString() + "/" + EncounterId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetPatientDashboard(long organizationId,long PatientId,long admissionid, string EncounterId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/patientdashboard/" + organizationId + "/" + PatientId + "/" + admissionid + "/" + EncounterId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "GetPatientDashboard", StartTime, "OK", MyUser.GetUsername(), "/" + organizationId.ToString() + "/" + PatientId.ToString() + "/" + admissionid.ToString() + "/" + EncounterId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "GetPatientDashboard", StartTime, "ERROR", MyUser.GetUsername(), "/" + organizationId.ToString() + "/" + PatientId.ToString() + "/" + admissionid.ToString() + "/" + EncounterId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getPatientHistorySOAP(long OrganizationId, long PatientId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/patienthistorylitedashboard/" + OrganizationId + "/" + PatientId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getPatientHistorySOAP", StartTime, "OK", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getPatientHistorySOAP", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetAdmissionHistory(long PatientId, long doctorId, int Year)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/AdmissionHistory/" + PatientId + "/" + doctorId + "/" + Year + "/"));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "GetAdmissionHistory", StartTime, "OK", MyUser.GetUsername(), "/" + PatientId.ToString() + "/" + doctorId.ToString() + "/" + Year.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "GetAdmissionHistory", StartTime, "ERROR", MyUser.GetUsername(), "/" + PatientId.ToString() + "/" + doctorId.ToString() + "/" + Year.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getRevisionHistorySOAP(long OrganizationId, long PatientId, long AdmissionId, string EncounterId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.GetAsync(string.Format($"/soaplog/" + OrganizationId + "/" + PatientId + "/" + AdmissionId + "/" + EncounterId));
            });
            
            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getRevisionHistorySOAP", StartTime, "OK", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getRevisionHistorySOAP", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }
}