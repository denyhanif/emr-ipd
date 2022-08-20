using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsHealthInfo
/// </summary>
public class clsHealthInfo
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    public static async Task<string> GetHealthInfo(long OrganizationId, long PatientId, int StatusId, string EncounterId)
    {
        try
        {
            HttpClient http_ = new HttpClient();

            http_.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHealthRecordAPI"].ToString()); // new Uri("http://10.83.254.38:5531");  // 

            http_.DefaultRequestHeaders.Accept.Clear();
            http_.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http_.GetAsync(string.Format($"/patienthealthinfo/" + OrganizationId + "/" + PatientId + "/" + StatusId + "/" + EncounterId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> GetStickerHealthInfo1(long PatientId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            HttpClient http_ = new HttpClient();

            //http_.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHealthRecordAPI"].ToString()); // new Uri("http://10.83.254.38:5531");  // 
            http_.BaseAddress = new Uri("http://10.85.129.54:5531");

            http_.DefaultRequestHeaders.Accept.Clear();
            http_.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http_.GetAsync(string.Format($"/flaghealthinfo/" + PatientId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "GetStickerHealthInfo", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "GetStickerHealthInfo", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetStickerHealthInfo2(long OrganizationId, long PatientId, long AdmissionId, string EncounterId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            HttpClient http_ = new HttpClient();

            //http_.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHealthRecordAPI"].ToString()); // new Uri("http://10.83.254.38:5531"); 
            http_.BaseAddress = new Uri("http://10.85.129.54:5521"); 

            http_.DefaultRequestHeaders.Accept.Clear();
            http_.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http_.GetAsync(string.Format($"/singleconsent/" + OrganizationId + "/" + PatientId + "/" + AdmissionId + "/" + EncounterId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "GetStickerVaccineCovid", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "GetStickerVaccineCovid", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            return ex.Message;
        }
    }
}