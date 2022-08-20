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
/// Summary description for clsResult
/// </summary>
public class clsResult
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public clsResult()
    {
    }

    public static async Task<string> getLaboratoryResult(String admissionId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/labresult/" + admissionId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionId", admissionId.ToString(), "getLaboratoryResult", StartTime, "OK", MyUser.GetUsername(), "/" + admissionId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionId", admissionId.ToString(), "getLaboratoryResult", StartTime, "ERROR", MyUser.GetUsername(), "/" + admissionId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getLabByOno(String patientId, String admissionId, String ono)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/labresultono/" + patientId + "/" + admissionId + "/" + ono));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionId", admissionId.ToString(), "getLabByOno", StartTime, "OK", MyUser.GetUsername(), "/" + patientId + "/" + admissionId + "/" + ono, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionId", admissionId.ToString(), "getLabByOno", StartTime, "ERROR", MyUser.GetUsername(), "/" + patientId + "/" + admissionId + "/" + ono, "", ex.Message));
            return ex.Message;
        }
    }


    public static async Task<string> getOnoData(String patientID, String onoList)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/annuallabcompare/" + patientID + "/"+ onoList));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", patientID.ToString(), "getOnoData", StartTime, "OK", MyUser.GetUsername(), "/" + patientID.ToString() + "/" + onoList, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", patientID.ToString(), "getOnoData", StartTime, "ERROR", MyUser.GetUsername(), "/" + patientID.ToString() + "/" + onoList, "", ex.Message));
            return ex.Message;
        }
    }


    public static async Task<string> getLaboratoryByDate(String patientId, String year)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/labresult/" + patientId + "/" + year));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", patientId.ToString(), "getLaboratoryByDate", StartTime, "OK", MyUser.GetUsername(), "/" + patientId.ToString() + "/" + year, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", patientId.ToString(), "getLaboratoryByDate", StartTime, "ERROR", MyUser.GetUsername(), "/" + patientId.ToString() + "/" + year, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getRadResultAdmission(String patientId, Int64 year)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/annualradresult/" + patientId + "/" + year));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", patientId.ToString(), "getRadResultAdmission", StartTime, "OK", MyUser.GetUsername(), "/" + patientId + "/" + year.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", patientId.ToString(), "getRadResultAdmission", StartTime, "ERROR", MyUser.GetUsername(), "/" + patientId + "/" + year.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getRadResultAdmissionDetail(String admissionIdList)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/radresult/" + admissionIdList));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionIdList", admissionIdList.ToString(), "getRadResultAdmissionDetail", StartTime, "OK", MyUser.GetUsername(), "/" + admissionIdList, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionIdList", admissionIdList.ToString(), "getRadResultAdmissionDetail", StartTime, "ERROR", MyUser.GetUsername(), "/" + admissionIdList, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getSearchLaboratoryResult(String patientId, Int64 type, String value)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/labresult/" + patientId + "/" + type + "/" + value));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", patientId.ToString(), "getSearchLaboratoryResult", StartTime, "OK", MyUser.GetUsername(), "/" + patientId+ "/" + type.ToString() + "/" + value, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", patientId.ToString(), "getSearchLaboratoryResult", StartTime, "ERROR", MyUser.GetUsername(), "/" + patientId + "/" + type.ToString() + "/" + value, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getLabCompareItem(String PatientId, String Value)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/labcompare/" + PatientId + "/" + Value));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getLabCompareItem", StartTime, "OK", MyUser.GetUsername(), "/" + PatientId.ToString() + "/" + Value, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getLabCompareItem", StartTime, "ERROR", MyUser.GetUsername(), "/" + PatientId.ToString() + "/" + Value, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getHeaderLab(String PatientId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/labtestgroup/" + PatientId ));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getHeaderLab", StartTime, "OK", MyUser.GetUsername(), "/" + PatientId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getHeaderLab", StartTime, "ERROR", MyUser.GetUsername(), "/" + PatientId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }


    public static async Task<string> getlabCompareTestGroup(String PatientId, String TestGroup)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/labcomparetestgroup/" + PatientId + "/" + TestGroup));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getlabCompareTestGroup", StartTime, "OK", MyUser.GetUsername(), "/" + PatientId.ToString() + "/" + TestGroup, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getlabCompareTestGroup", StartTime, "ERROR", MyUser.GetUsername(), "/" + PatientId.ToString() + "/" + TestGroup, "", ex.Message));
            return ex.Message;
        }
    }

}