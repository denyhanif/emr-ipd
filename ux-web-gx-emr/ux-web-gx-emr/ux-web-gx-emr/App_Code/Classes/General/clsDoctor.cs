using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using log4net;
using System.Reflection;

/// <summary>
/// Summary description for clsDoctor
/// </summary>
public class clsDoctor
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    public static async Task<string> GetDoctor(string user_id)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlFunctional"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpLogin.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
            //        ConfigurationManager.AppSettings["userSSO"].ToString(), ConfigurationManager.AppSettings["passSSO"].ToString()))));

            string applicationId = ConfigurationManager.AppSettings["ApplicationId"].ToString();

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/doctor/" + user_id));
            });

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "user_id", user_id, "GetDoctor", StartTime, EndTime, "OK", MyUser.GetUsername(), "/" + user_id, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "user_id", user_id, "GetDoctor", StartTime, ErrorTime, "ERROR", MyUser.GetUsername(), "/" + user_id, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetCountNotifOT(string org_id, string user_id)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); 
        try
        {
            HttpClient httpCL = new HttpClient();
            httpCL.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlSiloamOT"].ToString());

            httpCL.DefaultRequestHeaders.Accept.Clear();
            httpCL.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string applicationId = ConfigurationManager.AppSettings["ApplicationId"].ToString();

            var task = Task.Run(async () =>
            {
                return await httpCL.GetAsync(string.Format($"/notification/count/" + org_id + "/" + user_id));
            });

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "user_id", user_id, "GetCountNotifOT", StartTime, EndTime, "OK", MyUser.GetUsername(), "/" + org_id + "/" + user_id, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "user_id", user_id, "GetCountNotifOT", StartTime, ErrorTime, "ERROR", MyUser.GetUsername(), "/" + org_id + "/" + user_id, "", ex.Message));
            return ex.Message;
        }
    }
}