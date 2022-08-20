using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using log4net;
using System.Reflection;

/// <summary>
/// Summary description for clsLogin
/// </summary>

public class userLogin
{
    public string user_name { get; set; }
    public string password { get; set; }
    public string application_id { get; set; }
}

public class userLoginQR
{
    public Guid application_id { get; set; }
    public Guid unique_code { get; set; }
}

public class clsLogin
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public static async Task<string> GetLogin(string UserName, string Password)
    {
        string JsonString = "";
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlUserManagement"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpLogin.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
            //        ConfigurationManager.AppSettings["userSSO"].ToString(), ConfigurationManager.AppSettings["passSSO"].ToString()))));

            string applicationId = ConfigurationManager.AppSettings["ApplicationId"].ToString();
            userLogin loginUser = new userLogin { user_name = UserName, password = Password, application_id = applicationId };

            JsonString = JsonConvert.SerializeObject(loginUser);
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

            var task = Task.Run(async () =>
            {
                return await httpLogin.PutAsync(string.Format($"/userselectloginbyuserpassappoapps/"), content);
            });

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", UserName, "GetLogin", StartTime, EndTime, "OK", UserName, "", JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", UserName, "GetLogin", StartTime, ErrorTime, "ERROR", UserName, "", JsonString, ex.Message));

            return ex.Message;
        }
    }

    //public static async Task<string> getPatientData(string AdmissionId)
    //{
    //    try
    //    {
    //        HttpClient httpLogin = new HttpClient();
    //        httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["HopeTemporary"].ToString());

    //        httpLogin.DefaultRequestHeaders.Accept.Clear();
    //        httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


    //        var task = Task.Run(async () =>
    //        {
    //            return await httpLogin.GetAsync(string.Format($"/admissionbyno/" + AdmissionId));
    //        });

    //        return task.Result.Content.ReadAsStringAsync().Result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return ex.Message;
    //    }
    //}

    #region QRCode Login
    /// <summary>
    /// Generate QR
    /// </summary>
    /// <returns>base64string QR</returns>
    /// 
    public static string GenerateQR(string computerName)
    {
        // used for logging 
        List<QRCodeLogin.QRCodeGenerate> data = new List<QRCodeLogin.QRCodeGenerate>();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            //configure http client
            HttpClient http_Page_login = new HttpClient();
            http_Page_login.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLUserManagement"].ToString());
            string AppId = ConfigurationManager.AppSettings["ApplicationId"].ToString();
            string ServerCode = ConfigurationManager.AppSettings["ServerCode"].ToString();
            var task = Task.Run(async () =>
            {
                return await http_Page_login.GetAsync(string.Format($"/generateqrlogin/" + AppId + "/" + ServerCode + "/" + computerName));
            });
            JObject taskResult = JObject.Parse(task.Result.Content.ReadAsStringAsync().Result);
            // create LOG
            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID(), string.Empty, string.Empty, "GenerateQR", StartTime, string.Empty,
                string.Empty, taskResult.SelectToken("status").ToString(), string.Empty, taskResult.SelectToken("status").ToString()));
            // return as string
            return taskResult.SelectToken("data").ToString();
        }
        catch (Exception exx)
        {
            // create log
            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID(), string.Empty, string.Empty, "GenerateQR", StartTime, 
                string.Empty, string.Empty, exx.Message, string.Empty, exx.Message));
            return exx.Message;
        }
    }

    // Check QR Code is it used by mobile or not
    public static string CheckIsLogin(Guid unique_code, string computerName)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            // configure http client and content
            HttpClient http_Page_login = new HttpClient();
            http_Page_login.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLUserManagement"].ToString());
            userLoginQR loginUser = new userLoginQR
            {
                application_id = Guid.Parse(ConfigurationManager.AppSettings["ApplicationId"].ToString()),
                unique_code = unique_code
            };
            string JsonString = JsonConvert.SerializeObject(loginUser);
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            var task = Task.Run(async () =>
            {
                return await http_Page_login.PostAsync(string.Format($"/userselectloginbyuserpassappoapps_qr"), content);
            });
            
            // result of http client
            JObject taskResult = JObject.Parse(task.Result.Content.ReadAsStringAsync().Result);
            return taskResult.ToString();
        }
        catch (Exception exx)
        {
            // create log
            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID(), string.Empty, string.Empty, "CheckIsLogin", 
                StartTime, string.Empty, string.Empty,exx.Message, string.Empty, exx.Message));
            return exx.Message;
        }
    }

    // Remove / Update QR Code Guid has been used
    public static string RemoveQrSecretId(Guid unique_code, string computerName)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            // configure http client
            HttpClient http_Page_login = new HttpClient();
            http_Page_login.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLUserManagement"].ToString());
            http_Page_login.DefaultRequestHeaders.Accept.Clear();
            http_Page_login.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            // set app id
            string AppId = ConfigurationManager.AppSettings["ApplicationId"].ToString();
            // configure post type
            var JsonString = JsonConvert.SerializeObject(unique_code);
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

            var task = Task.Run(async () =>
            {
                return await http_Page_login.PostAsync(string.Format($"/removeguidqr/"), content);
            });

            // create LOG
            JObject taskResult = JObject.Parse(task.Result.Content.ReadAsStringAsync().Result);
            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID(), string.Empty, string.Empty, "RemoveQrSecretId", StartTime, string.Empty,
                string.Empty, taskResult.SelectToken("status").ToString(), string.Empty, taskResult.SelectToken("status").ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception exx)
        {
            // create log
            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID(), string.Empty, string.Empty, "RemoveQrSecretId",
                StartTime, string.Empty, string.Empty, exx.Message, string.Empty, exx.Message));
            return exx.Message;
        }
    }
    #endregion

}