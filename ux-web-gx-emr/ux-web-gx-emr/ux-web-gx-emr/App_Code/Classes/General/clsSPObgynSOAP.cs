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
using static SPObgyn;
using log4net;
using System.Reflection;

/// <summary>
/// Summary description for clsSOAP
/// </summary>
public class clsSPObgynSOAP
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public clsSPObgynSOAP()
    { 
        //
        // TODO: Add constructor logic here
        //
    }

    public static async Task<string> getSOAPObgyn(string encounter_ticket_id,long patient_id,long admission_id,long organization_id,long doctor_id)
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
                return await http.GetAsync(string.Format($"/SOAPObgyn/" + encounter_ticket_id + "/" + patient_id + "/" + admission_id + "/" + organization_id + "/" + doctor_id));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", encounter_ticket_id, "getSOAPObgyn", StartTime, "OK", MyUser.GetUsername(), "/" + encounter_ticket_id + "/" + patient_id.ToString() + "/" + admission_id.ToString() + "/" + organization_id.ToString() + "/" + doctor_id.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", encounter_ticket_id, "getSOAPObgyn", StartTime, "ERROR", MyUser.GetUsername(), "/" + encounter_ticket_id + "/" + patient_id.ToString() + "/" + admission_id.ToString() + "/" + organization_id.ToString() + "/" + doctor_id.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> SaveAsDraftSOAPObgyn(SOAPObgyn savesoap, string pageid, string appointmentid, string username)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //log.Info(LogLibrary.Logging("E", "btnSignIn_Click", txtUsername.Text.ToString(), HospitalList.Count().ToString()));

        var JsonString = JsonConvert.SerializeObject(savesoap);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
        var postDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"/SOAPObgyn/0/0/0/"+ pageid+ "/saveasdraft/" + postDate + "/" + appointmentid + "/" + username), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SaveAsDraftSOAPObgyn", StartTime, "OK", MyUser.GetUsername(), "/" + pageid + "/saveasdraft/" + postDate + "/" + appointmentid + "/" + username, JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SaveAsDraftSOAPObgyn", StartTime, "ERROR", MyUser.GetUsername(), "/" + pageid + "/saveasdraft/" + postDate + "/" + appointmentid + "/" + username, JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> SubmitSOAPObgyn(SOAPObgyn savesoap,long itemid, double ConsultationAmount, double DiscountAmount,string ProcedureNotes,string pageid,string consname, string appointmentid, string username)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //log.Info(LogLibrary.Logging("E", "btnSignIn_Click", txtUsername.Text.ToString(), HospitalList.Count().ToString()));
        if (ProcedureNotes == "")
        {
            ProcedureNotes = "-";
        }

        if (consname == "")
        {
            consname = "-";
        }

        consname = consname.Replace("/", " or ");

        var JsonString = JsonConvert.SerializeObject(savesoap);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
        var postDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"/SOAPObgyn/"+ itemid + "/" + ConsultationAmount + "/" + DiscountAmount +"/" + pageid + "/" + consname + "/" + postDate + "/" + appointmentid + "/" + username), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SubmitSOAPObgyn", StartTime, "OK", MyUser.GetUsername(), "/" + itemid.ToString() + "/" + ConsultationAmount.ToString() + "/" + DiscountAmount.ToString() + "/" + pageid + "/" + consname + "/" + postDate + "/" + appointmentid + "/" + username, JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SubmitSOAPObgyn", StartTime, "ERROR", MyUser.GetUsername(), "/" + itemid.ToString() + "/" + ConsultationAmount.ToString() + "/" + DiscountAmount.ToString() + "/" + pageid + "/" + consname + "/" + postDate + "/" + appointmentid + "/" + username, JsonString, ex.Message));
            return ex.Message;
        }
    }
}