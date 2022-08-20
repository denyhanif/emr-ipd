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
using log4net;
using System.Reflection;

/// <summary>
/// Summary description for clsPrescription
/// </summary>
public class clsPrescription
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public clsPrescription()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static async Task<string> getPrescription(string prescriptionNo)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlIntegration"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/prescriptionbyno/" + prescriptionNo));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "userID", MyUser.GetHopeUserID(), "getPrescription", StartTime, "OK", MyUser.GetUsername(), "/" + prescriptionNo.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "userID", MyUser.GetHopeUserID(), "getPrescription", StartTime, "ERROR", MyUser.GetUsername(), "/" + prescriptionNo.ToString(), "", ex.Message));
            return ex.Message;
        }
    }
    public static async Task<string> getItemPrescription(long organizationId,long AdmissionTypeId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHOPE"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/drugprescription/" + organizationId + "/" + AdmissionTypeId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", organizationId.ToString(), "getItemPrescription", StartTime, "OK", MyUser.GetUsername(), "/" + organizationId.ToString() + "/" + AdmissionTypeId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", organizationId.ToString(), "getItemPrescription", StartTime, "ERROR", MyUser.GetUsername(), "/" + organizationId.ToString() + "/" + AdmissionTypeId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }
    public static async Task<string> GetDrugPrescription(string ticketid)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            ticketid = "7A6060BB-F2CC-43D8-B3AD-1F4CCE320106";
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            httpclient.DefaultRequestHeaders.Accept.Clear();
            httpclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpclient.GetAsync(string.Format($"/prescriptiondrug/" + ticketid));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "ticketid", ticketid, "GetDrugPrescription", StartTime, "OK", MyUser.GetUsername(), "/" + ticketid, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "ticketid", ticketid, "GetDrugPrescription", StartTime, "ERROR", MyUser.GetUsername(), "/" + ticketid, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetCompoundPrescription(string ticketid)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            ticketid = "7A6060BB-F2CC-43D8-B3AD-1F4CCE320106";
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            httpclient.DefaultRequestHeaders.Accept.Clear();
            httpclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpclient.GetAsync(string.Format($"/prescriptioncompound/" + ticketid));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "ticketid", ticketid, "GetCompoundPrescription", StartTime, "OK", MyUser.GetUsername(), "/" + ticketid, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "ticketid", ticketid, "GetCompoundPrescription", StartTime, "ERROR", MyUser.GetUsername(), "/" + ticketid, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetDrugDetailPrescription(string name,long orgid,long DoctorId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            httpclient.DefaultRequestHeaders.Accept.Clear();
            httpclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpclient.GetAsync(string.Format($"/viewordersetprescription/" + name + "/" + orgid + "/" + DoctorId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "name", name.ToString(), "GetDrugDetailPrescription", StartTime, "OK", MyUser.GetUsername(), "/" + name + "/" + orgid.ToString() + "/" + DoctorId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "name", name.ToString(), "GetDrugDetailPrescription", StartTime, "ERROR", MyUser.GetUsername(), "/" + name + "/" + orgid.ToString() + "/" + DoctorId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetCompDetailPrescription(string name, int orgid)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            httpclient.DefaultRequestHeaders.Accept.Clear();
            httpclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpclient.GetAsync(string.Format($"/viewcompoundprescription/" + name + "/" + orgid));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "name", name.ToString(), "GetCompDetailPrescription", StartTime, "OK", MyUser.GetUsername(), "/" + name.ToString() + "/" + orgid.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "name", name.ToString(), "GetCompDetailPrescription", StartTime, "ERROR", MyUser.GetUsername(), "/" + name.ToString() + "/" + orgid.ToString(), "", ex.Message));
            return ex.Message;
        }
    }
    public static async Task<string> GetFrequentDrugs(string doctorId, Int64 OrganizationId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/frequentdrug/" + doctorId + "/" + OrganizationId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "GetFrequentDrugs", StartTime, "OK", MyUser.GetUsername(), "/" + doctorId + "/" + OrganizationId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "GetFrequentDrugs", StartTime, "ERROR", MyUser.GetUsername(), "/" + doctorId + "/" + OrganizationId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }
    public static async Task<string> SaveDrugPres(List<PrescriptionOperation> drugpres)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(drugpres);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlIntegration"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"/prescription/"), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "SaveDrugPres", StartTime, "OK", MyUser.GetUsername(), "", JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "SaveDrugPres", StartTime, "ERROR", MyUser.GetUsername(), "", JsonString, ex.Message));
            return ex.Message;
        }
    }
}