using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using System.Globalization;
using Newtonsoft.Json;
using log4net;
using System.Reflection;
/// <summary>
/// Summary description for clsWorklistNonAppointment
/// </summary>
public class clsWorklistNonAppointment
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public clsWorklistNonAppointment()
    { }

    public static async Task<string> worklistdoctor(Int64 doctorId,Int64 orgId, Int64 admTypeId, DateTime datestart, DateTime datefinish, string search,int status)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            string a = datestart.ToString("yyyy-MM-dd");

            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlExtension"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpLogin.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
            //        ConfigurationManager.AppSettings["userSSO"].ToString(), ConfigurationManager.AppSettings["passSSO"].ToString()))));

            //string b = "/worklistdoctor/" + doctorId + "/" + admTypeId + "/" + orgId
            //         + "/" + datestart.ToString("yyyy-MM-dd") + "/" + datefinish.ToString("yyyy-MM-dd") + "?Search=" + search + "&Status=" + status;
            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/worklistdoctor/" + doctorId + "/" + orgId   + "/" + admTypeId
                     + "/" + datestart.ToString("yyyy-MM-dd") + "/" + datefinish.ToString("yyyy-MM-dd") + "?Search=" + search + "&Status=" + status
                    ));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "worklistdoctor", StartTime, "OK", MyUser.GetUsername(), "/" + doctorId.ToString() + "/" + orgId.ToString() + "/" + admTypeId.ToString()
                     + "/" + datestart.ToString("yyyy-MM-dd") + "/" + datefinish.ToString("yyyy-MM-dd") + "?Search=" + search + "&Status=" + status.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "worklistdoctor", StartTime, "ERROR", MyUser.GetUsername(), "/" + doctorId.ToString() + "/" + orgId.ToString() + "/" + admTypeId.ToString()
                     + "/" + datestart.ToString("yyyy-MM-dd") + "/" + datefinish.ToString("yyyy-MM-dd") + "?Search=" + search + "&Status=" + status.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> encounteractiveticketcount(Int64 doctorId, Int64 orgId, Int64 admTypeId, DateTime datestart, DateTime datefinish)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            string a = datestart.ToString("yyyy-MM-dd");

            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlExtension"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpLogin.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
            //        ConfigurationManager.AppSettings["userSSO"].ToString(), ConfigurationManager.AppSettings["passSSO"].ToString()))));

            //string b = "/worklistdoctor/" + doctorId + "/" + admTypeId + "/" + orgId
            //         + "/" + datestart.ToString("yyyy-MM-dd") + "/" + datefinish.ToString("yyyy-MM-dd") + "?Search=" + search + "&Status=" + status;
            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/encounteractiveticketcount/" + doctorId + "/" + orgId + "/" + admTypeId
                     + "/" + datestart.ToString("yyyy-MM-dd") + "/" + datefinish.ToString("yyyy-MM-dd") ));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "encounteractiveticketcount", StartTime, "OK", MyUser.GetUsername(), "/" + doctorId.ToString() + "/" + orgId.ToString() + "/" + admTypeId.ToString()
                     + "/" + datestart.ToString("yyyy-MM-dd") + "/" + datefinish.ToString("yyyy-MM-dd"), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "encounteractiveticketcount", StartTime, "ERROR", MyUser.GetUsername(), "/" + doctorId.ToString() + "/" + orgId.ToString() + "/" + admTypeId.ToString()
                     + "/" + datestart.ToString("yyyy-MM-dd") + "/" + datefinish.ToString("yyyy-MM-dd"), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> encountercancelticketcount(Int64 doctorId, Int64 orgId, Int64 admTypeId, DateTime datestart, DateTime datefinish)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            string a = datestart.ToString("yyyy-MM-dd");

            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlExtension"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpLogin.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
            //        ConfigurationManager.AppSettings["userSSO"].ToString(), ConfigurationManager.AppSettings["passSSO"].ToString()))));

            //string b = "/worklistdoctor/" + doctorId + "/" + admTypeId + "/" + orgId
            //         + "/" + datestart.ToString("yyyy-MM-dd") + "/" + datefinish.ToString("yyyy-MM-dd") + "?Search=" + search + "&Status=" + status;
            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/encountercancelticketcount/" + doctorId + "/" + orgId + "/" + admTypeId
                     + "/" + datestart.ToString("yyyy-MM-dd") + "/" + datefinish.ToString("yyyy-MM-dd")));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "encountercancelticketcount", StartTime, "OK", MyUser.GetUsername(), "/" + doctorId.ToString() + "/" + orgId.ToString() + "/" + admTypeId.ToString()
                     + "/" + datestart.ToString("yyyy-MM-dd") + "/" + datefinish.ToString("yyyy-MM-dd"), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "encountercancelticketcount", StartTime, "ERROR", MyUser.GetUsername(), "/" + doctorId.ToString() + "/" + orgId.ToString() + "/" + admTypeId.ToString()
                     + "/" + datestart.ToString("yyyy-MM-dd") + "/" + datefinish.ToString("yyyy-MM-dd"), "", ex.Message));
            return ex.Message;
        }
    }
}