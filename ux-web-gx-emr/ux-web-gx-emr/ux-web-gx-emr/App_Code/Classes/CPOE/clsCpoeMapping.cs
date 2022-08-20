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
/// Summary description for clsCpoeMapping
/// </summary>
public class clsCpoeMapping
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    public clsCpoeMapping()
    { }

    public static async Task<string> GetMapping(Int64 organizationId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpMapping = new HttpClient();
            httpMapping.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            httpMapping.DefaultRequestHeaders.Accept.Clear();
            httpMapping.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var task = Task.Run(async () =>
            {
                return await httpMapping.GetAsync(string.Format($"/cpoemapping/" + organizationId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", organizationId.ToString(), "GetMapping", StartTime, "OK", MyUser.GetUsername(), "/" + organizationId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", organizationId.ToString(), "GetMapping", StartTime, "ERROR", MyUser.GetUsername(), "/" + organizationId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetCPOEList(string ticketId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            ticketId = "7A6060BB-F2CC-43D8-B3AD-1F4CCE320106";
            HttpClient httpMapping = new HttpClient();
            httpMapping.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            httpMapping.DefaultRequestHeaders.Accept.Clear();
            httpMapping.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var task = Task.Run(async () =>
            {
                return await httpMapping.GetAsync(string.Format($"/cpoelist/" + ticketId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "ticketId", ticketId.ToString(), "GetCPOEList", StartTime, "OK", MyUser.GetUsername(), "/" + ticketId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "ticketId", ticketId.ToString(), "GetCPOEList", StartTime, "ERROR", MyUser.GetUsername(), "/" + ticketId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }


    public static async Task<string> SaveAsDraft(List<ListChecked> listchecked)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(listchecked);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlIntegration"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"/cpoetransaction/"), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", MyUser.GetHopeOrgID().ToString(), "SaveAsDraft", StartTime, "OK", MyUser.GetUsername(), "", JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", MyUser.GetHopeOrgID().ToString(), "SaveAsDraft", StartTime, "ERROR", MyUser.GetUsername(), "", JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetAllItemCPOE(Int64 organizationId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpCPOE = new HttpClient();
            httpCPOE.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            httpCPOE.DefaultRequestHeaders.Accept.Clear();
            httpCPOE.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var task = Task.Run(async () =>
            {
                return await httpCPOE.GetAsync(string.Format($"/cpoeitem/" + organizationId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", organizationId.ToString(), "GetAllItemCPOE", StartTime, "OK", MyUser.GetUsername(), "/" + organizationId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", organizationId.ToString(), "GetAllItemCPOE", StartTime, "ERROR", MyUser.GetUsername(), "/" + organizationId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }
}