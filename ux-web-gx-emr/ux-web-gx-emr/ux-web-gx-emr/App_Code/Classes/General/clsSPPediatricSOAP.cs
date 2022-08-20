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
using static SPPediatric;
using log4net;
using System.Reflection;

/// <summary>
/// Summary description for clsSOAP
/// </summary>
public class clsSPPediatricSOAP
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public clsSPPediatricSOAP()
    { 
        //
        // TODO: Add constructor logic here
        //
    }

    public static async Task<string> getSOAPPediatric(string encounter_ticket_id,long patient_id,long admission_id,long organization_id,long doctor_id)
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
                return await http.GetAsync(string.Format($"/SOAPPediatric/" + encounter_ticket_id + "/" + patient_id + "/" + admission_id + "/" + organization_id + "/" + doctor_id));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", encounter_ticket_id, "getSOAPPediatric", StartTime, "OK", MyUser.GetUsername(), "/" + encounter_ticket_id + "/" + patient_id.ToString() + "/" + admission_id.ToString() + "/" + organization_id.ToString() + "/" + doctor_id.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", encounter_ticket_id, "getSOAPPediatric", StartTime, "ERROR", MyUser.GetUsername(), "/" + encounter_ticket_id + "/" + patient_id.ToString() + "/" + admission_id.ToString() + "/" + organization_id.ToString() + "/" + doctor_id.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> SaveAsDraftSOAPPediatric(SOAPPediatric savesoap, string pageid, string appointmentid, string username)
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
                return await http.PostAsync(string.Format($"/SOAPPediatric/0/0/0/" + pageid+ "/saveasdraft/" + postDate + "/" + appointmentid + "/" + username), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SaveAsDraftSOAPPediatric", StartTime, "OK", MyUser.GetUsername(), "/" + pageid + "/saveasdraft/" + postDate + "/" + appointmentid + "/" + username, JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SaveAsDraftSOAPPediatric", StartTime, "ERROR", MyUser.GetUsername(), "/" + pageid + "/saveasdraft/" + postDate + "/" + appointmentid + "/" + username, JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> SubmitSOAPPediatric(SOAPPediatric savesoap,long itemid, double ConsultationAmount, double DiscountAmount,string ProcedureNotes,string pageid,string consname, string appointmentid, string username)
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
                return await http.PostAsync(string.Format($"/SOAPPediatric/" + itemid + "/" + ConsultationAmount + "/" + DiscountAmount +"/" + pageid + "/" + consname + "/" + postDate + "/" + appointmentid + "/" + username), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SubmitSOAPPediatric", StartTime, "OK", MyUser.GetUsername(), "/" + itemid.ToString() + "/" + ConsultationAmount.ToString() + "/" + DiscountAmount.ToString() + "/" + pageid + "/" + consname + "/" + postDate + "/" + appointmentid + "/" + username, JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SubmitSOAPPediatric", StartTime, "ERROR", MyUser.GetUsername(), "/" + itemid.ToString() + "/" + ConsultationAmount.ToString() + "/" + DiscountAmount.ToString() + "/" + pageid + "/" + consname + "/" + postDate + "/" + appointmentid + "/" + username, JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetCoordinat(int gender)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri("http://10.83.254.38:5500");
            //http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.GetAsync(string.Format($"/chartcoordinate/" + gender));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "gender", gender.ToString(), "GetCoordinat", StartTime, "OK", MyUser.GetUsername(), "/" + gender.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "gender", gender.ToString(), "GetCoordinat", StartTime, "ERROR", MyUser.GetUsername(), "/" + gender.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static DataTable GetCoordinatbySP(int gender)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        DataTable dtCord = new DataTable();
        bool sex = false; //Male

        if (gender == 2)
        {
            sex = true; //Female
        }
        try
        {
            String strConnString = ConfigurationManager.AppSettings["DB_Master"].ToString();
            SqlConnection conn = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetChartCoordinate";
            cmd.Parameters.Add("SexId", SqlDbType.Bit).Value = sex;
            cmd.Connection = conn;

            try
            {
                conn.Open();

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {                  
                    sda.Fill(dtCord);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "gender", gender.ToString(), "GetCoordinatbySP", StartTime, "OK", MyUser.GetUsername(), "/" + gender.ToString(), "", ""));
            return dtCord;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "gender", gender.ToString(), "GetCoordinatbySP", StartTime, "ERROR", MyUser.GetUsername(), "/" + gender.ToString(), "", ex.Message));
            throw ex;
        }
    }

    public static async Task<string> SubmitPediatricChart(List<PediatricChart> chart, long orgid, long ptnid, long admid, Guid encid, long doctorid)
    {


        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(chart);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"/soappediatricchart/" + orgid + "/" + ptnid + "/" + admid + "/" + encid + "/" + doctorid), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", encid.ToString(), "SubmitPediatricChart", StartTime, "OK", MyUser.GetUsername(), "/" + orgid.ToString() + "/" + ptnid.ToString() + "/" + admid.ToString() + "/" + encid.ToString() + "/" + doctorid.ToString(), JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", encid.ToString(), "SubmitPediatricChart", StartTime, "ERROR", MyUser.GetUsername(), "/" + orgid.ToString() + "/" + ptnid.ToString() + "/" + admid.ToString() + "/" + encid.ToString() + "/" + doctorid.ToString(), JsonString, ex.Message));
            return ex.Message;
        }
    }
}