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
using static FirstAssesment;

/// <summary>
/// Summary description for clsFirstAssesment
/// </summary>
public class clsFirstAssesment
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public clsFirstAssesment()
    { }

    public static async Task<string> GetPageSpecialty(long doctorid, int orgid)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/pagespecialty/"+doctorid+"/"+orgid));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorid", doctorid.ToString(), "GetPageSpecialty", StartTime, "OK", MyUser.GetUsername(), "/" + doctorid.ToString() + "/" + orgid.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorid", doctorid.ToString(), "GetPageSpecialty", StartTime, "ERROR", MyUser.GetUsername(), "/" + doctorid.ToString() + "/" + orgid.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getFirstAssesment(string encounter_ticket_id, long patient_id, long admission_id, long organization_id, long doctor_id, string page_id)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/firstanalysis/" + encounter_ticket_id + "/" + patient_id + "/" + admission_id + "/" + organization_id + "/" + doctor_id + "/" + page_id));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", encounter_ticket_id.ToString(), "getFirstAssesment", StartTime, "OK", MyUser.GetUsername(), "/" + encounter_ticket_id + "/" + patient_id.ToString() + "/" + admission_id.ToString() + "/" + organization_id.ToString() + "/" + doctor_id.ToString() + "/" + page_id, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organiencounter_ticket_idzationId", encounter_ticket_id.ToString(), "getFirstAssesment", StartTime, "ERROR", MyUser.GetUsername(), "/" + encounter_ticket_id + "/" + patient_id.ToString() + "/" + admission_id.ToString() + "/" + organization_id.ToString() + "/" + doctor_id.ToString() + "/" + page_id, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> SaveAsDraftFA(FirstAnalysis fa)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(fa);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"/firstanalysis/"), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", fa.encounter_ticket_id.ToString(), "SaveAsDraftFA", StartTime, "OK", MyUser.GetUsername(), "", JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", fa.encounter_ticket_id.ToString(), "SaveAsDraftFA", StartTime, "ERROR", MyUser.GetUsername(), "", JsonString, ex.Message));
            return ex.Message;
        }
    }
}