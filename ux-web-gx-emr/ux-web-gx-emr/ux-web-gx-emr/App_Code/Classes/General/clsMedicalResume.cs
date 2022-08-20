using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using log4net;
using System.Reflection;
/// <summary>
/// Summary description for clsMedicalResume
/// </summary>
public class clsMedicalResume
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public static async Task<string> GetResume(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/medicalresume/" + OrganizationId + "/" + PatientId + "/" + AdmissionId + "/" + EncounterId));
            });


            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "EncounterId", EncounterId.ToString(), "GetResume", StartTime, "OK", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "EncounterId", EncounterId.ToString(), "GetResume", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }
}