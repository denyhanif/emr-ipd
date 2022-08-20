using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using System.Globalization;
using Newtonsoft.Json;
using System.Text;
using log4net;
using System.Reflection;

/// <summary>
/// Summary description for clsRecap
/// </summary>
public class clsRecap
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public clsRecap()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static async Task<string> GetRecapIPD(Int64 hospitalID, Int64 DoctorID)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string mysiloamurl = ConfigurationManager.AppSettings["BaseURL_MySiloam_Doctor"].ToString();

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseURL_MySiloam_Doctor"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.GetAsync(string.Format($"" + mysiloamurl + "/api/v2/doctors/in-patients/hospital/" + hospitalID + "/doctor/" + DoctorID + "?isEmr=true"));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorID", DoctorID.ToString(), "GetRecapIPD", StartTime, "OK", MyUser.GetUsername(), "hospital/" + hospitalID.ToString() + "/doctor/" + DoctorID.ToString() + "?isEmr=true", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorID", DoctorID.ToString(), "GetRecapIPD", StartTime, "ERROR", MyUser.GetUsername(), "hospital/" + hospitalID.ToString() + "/doctor/" + DoctorID.ToString() + "?isEmr=true", "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetRecapOPD(Int64 hospitalID, Int64 DoctorID, string datefrom, string dateto, int limit, int offset, string name)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string paramname = "";
        if (name != "")
        {
            paramname = "&name=" + name;
        }

        string mysiloamurl = ConfigurationManager.AppSettings["BaseURL_MySiloam_Doctor"].ToString();

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseURL_MySiloam_Doctor"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.GetAsync(string.Format($"" + mysiloamurl + "/api/v2/doctors/out-patients/hospital/" + hospitalID + "/doctor/" + DoctorID + "?limit=" + limit + "&offset=" + offset + "&fromDate=" + datefrom + "&toDate=" + dateto + paramname + "&isEmr=true"));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorID", DoctorID.ToString(), "GetRecapOPD", StartTime, "OK", MyUser.GetUsername(), "hospital/" + hospitalID.ToString() + "/doctor/" + DoctorID.ToString() + "?limit=" + limit.ToString() + "&offset=" + offset.ToString() + "&fromDate=" + datefrom + "&toDate=" + dateto + paramname + "&isEmr=true","", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorID", DoctorID.ToString(), "GetRecapOPD", StartTime, "ERROR", MyUser.GetUsername(), "hospital/" + hospitalID.ToString() + "/doctor/" + DoctorID.ToString() + "?limit=" + limit.ToString() + "&offset=" + offset.ToString() + "&fromDate=" + datefrom + "&toDate=" + dateto + paramname + "&isEmr=true", "", ex.Message));
            return ex.Message;
        }
    }
}