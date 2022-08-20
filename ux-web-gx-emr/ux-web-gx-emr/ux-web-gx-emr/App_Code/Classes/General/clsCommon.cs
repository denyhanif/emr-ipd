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
/// Summary description for clsCommon
/// </summary>
public class clsCommon
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public static async Task<string> GetSettingValue(long OrganizationId, string SettingName)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpLogin.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
            //        ConfigurationManager.AppSettings["userSSO"].ToString(), ConfigurationManager.AppSettings["passSSO"].ToString()))));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/organizationsettingbyid/" + OrganizationId + "/" + SettingName));
            });

            var response = task.Result.Content.ReadAsStringAsync().Result;
            var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(response);
            var data = (JObject)Response.Property("data").Value;

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", OrganizationId.ToString(), "GetSettingValue", StartTime, "OK", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + SettingName, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return data.Property("setting_value").Value.ToString();
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", OrganizationId.ToString(), "GetSettingValue", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + SettingName, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetOrganizationSettingbyOrgId(long orgid, long userid)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpLogin.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
            //        ConfigurationManager.AppSettings["userSSO"].ToString(), ConfigurationManager.AppSettings["passSSO"].ToString()))));

            var task = Task.Run(async () =>
            {
                //return await httpLogin.GetAsync(string.Format($"/organizationsettingbyorgid/" + orgid));
                return await httpLogin.GetAsync(string.Format($"/organizationsetting/" + orgid + "/" + userid));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "userid", userid.ToString(), "GetOrganizationSettingbyOrgId", StartTime, "OK", MyUser.GetUsername(), "/" + orgid.ToString() + " / " + userid.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "userid", userid.ToString(), "GetOrganizationSettingbyOrgId", StartTime, "ERROR", MyUser.GetUsername(), "/" + orgid.ToString() + " / " + userid.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetOrganizationSettingbyOrgIdUserID(long orgid, long userid)
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
                return await httpLogin.GetAsync(string.Format($"/usersetting/" + orgid + "/" + userid));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "userid", userid.ToString(), "GetOrganizationSettingbyOrgIdUserID", StartTime, "OK", MyUser.GetUsername(), "/" + orgid.ToString() + " / " + userid.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "userid", userid.ToString(), "GetOrganizationSettingbyOrgIdUserID", StartTime, "ERROR", MyUser.GetUsername(), "/" + orgid.ToString() + " / " + userid.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> SetOrganizationSettingbyOrgIdUserID(List<ViewOrganizationSetting> og, long orgid, long userid)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(og);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"/usersetting/" + orgid + "/" + userid), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "userid", userid.ToString(), "SetOrganizationSettingbyOrgIdUserID", StartTime, "OK", MyUser.GetUsername(), "/" + orgid.ToString() + " / " + userid.ToString(), JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "userid", userid.ToString(), "SetOrganizationSettingbyOrgIdUserID", StartTime, "ERROR", MyUser.GetUsername(), "/" + orgid.ToString() + " / " + userid.ToString(), JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static string GetAge(DateTime BirthDate)
    {
        DateTime today = DateTime.Today;

        int months = today.Month - BirthDate.Month;
        int years = today.Year - BirthDate.Year;

        if (today.Day < BirthDate.Day)
        {
            months--;
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        int days = (today - BirthDate.AddMonths((years * 12) + months)).Days;

        return string.Format("{0}Y {1}M {2}D",
                             years,
                             months,
                             days);
    }

    public static double GetAgeTotalDays(DateTime BirthDate)
    {
        DateTime today = DateTime.Today;

        int months = today.Month - BirthDate.Month;
        int years = today.Year - BirthDate.Year;

        if (today.Day < BirthDate.Day)
        {
            months--;
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        int days = (today - BirthDate.AddMonths((years * 12) + months)).Days;

        TimeSpan objTimeSpan = today - BirthDate;

        return objTimeSpan.TotalDays;
    }

    public static int GetAgeDays(DateTime BirthDate)
    {
        DateTime today = DateTime.Today;

        int months = today.Month - BirthDate.Month;
        int years = today.Year - BirthDate.Year;

        if (today.Day < BirthDate.Day)
        {
            months--;
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        int days = (today - BirthDate.AddMonths((years * 12) + months)).Days;

        return days;
    }

    public static int GetAgeMonths(DateTime BirthDate)
    {
        DateTime today = DateTime.Today;

        int months = today.Month - BirthDate.Month;
        int years = today.Year - BirthDate.Year;
        if (today.Day < BirthDate.Day)
        {
            months--;
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        return months;
    }

    public static int GetAgeYears(DateTime BirthDate)
    {
        DateTime today = DateTime.Today;

        int months = today.Month - BirthDate.Month;
        int years = today.Year - BirthDate.Year;
        if (today.Day < BirthDate.Day)
        {
            months--;
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        return years;
    }

    public static async Task<string> GetPatientHeader(long PatientId, string TicketId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlExtension"].ToString());
            //httpLogin.BaseAddress = new Uri("http://10.83.1.45:5595");

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/patientheader/" + PatientId + "/" + TicketId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "GetPatientHeader", StartTime, "OK", MyUser.GetUsername(), "/" + PatientId.ToString() + "/" + TicketId, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "GetPatientHeader", StartTime, "ERROR", MyUser.GetUsername(), "/" + PatientId.ToString() + "/" + TicketId, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> SendEmail(Email model)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string JsonString = "";
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlEmailEngine"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpLogin.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
            //        ConfigurationManager.AppSettings["userSSO"].ToString(), ConfigurationManager.AppSettings["passSSO"].ToString()))));

            JsonString = JsonConvert.SerializeObject(model);
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

            var task = Task.Run(async () =>
            {
                return await httpLogin.PostAsync(string.Format($"/insertEmail/"), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "username", MyUser.GetUsername(), "SendEmail", StartTime, "OK", MyUser.GetUsername(), "", JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "username", MyUser.GetUsername(), "SendEmail", StartTime, "ERROR", MyUser.GetUsername(), "", JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetAllOrganization()
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
                return await httpLogin.GetAsync(string.Format($"/organization"));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "username", MyUser.GetUsername(), "GetAllOrganization", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "username", MyUser.GetUsername(), "GetAllOrganization", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> ChangePasswordUser(string username, string oldpass, string newpass, string modifiedby)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        ParamChangePass param = new ParamChangePass();
        param.user_name = username;
        param.old_pass = oldpass;
        param.new_pass = newpass;
        param.modified_by = modifiedby;

        var JsonString = JsonConvert.SerializeObject(param);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            //string apicentralums = "http://10.85.129.91:8500"; //untuk persiapan ganti uri base address
            HttpClient http_putuser = new HttpClient();
            http_putuser.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlUserManagement"].ToString());

            http_putuser.DefaultRequestHeaders.Accept.Clear();
            http_putuser.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http_putuser.PutAsync(string.Format($"/userupdatechangepassword"), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "username", username, "ChangePasswordUser", StartTime, "OK", MyUser.GetUsername(), "", JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "username", username, "ChangePasswordUser", StartTime, "ERROR", MyUser.GetUsername(), "", JsonString, ex.Message));
            return ex.Message;
        }
    }
}