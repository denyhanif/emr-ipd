using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using log4net;
using System.Reflection;
/// <summary>
/// Summary description for clsPatientHistory
/// </summary>
public class clsPatientHistory
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public static async Task<string> getPatientData(string mrNO, long orgID)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient medicalClient = new HttpClient();

            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlExtension"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/patientdatabymr/" + mrNO + "/" + orgID));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "mrNO", mrNO.ToString(), "getPatientData", StartTime, "OK", MyUser.GetUsername(), "/" + mrNO + "/" + orgID.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "mrNO", mrNO.ToString(), "getPatientData", StartTime, "ERROR", MyUser.GetUsername(), "/" + mrNO + "/" + orgID.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getStatusMR(Int64 OrgId, string name)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/organizationsettingbyid/" + OrgId + "/" + name ));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "OrganizationId", OrgId.ToString(), "getStatusMR", StartTime, "OK", MyUser.GetUsername(), "/" + OrgId.ToString() + "/" + name, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "OrganizationId", OrgId.ToString(), "getStatusMR", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrgId.ToString() + "/" + name, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getEncounterPatientHistory(string patientId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlExtension"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/encounterhistory/" + patientId ));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientId", patientId.ToString(), "getEncounterPatientHistory", StartTime, "OK", MyUser.GetUsername(), "/" + patientId, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientId", patientId.ToString(), "getEncounterPatientHistory", StartTime, "ERROR", MyUser.GetUsername(), "/" + patientId, "", ex.Message));
            return ex.Message;
        }
    }

    public static DataTable getScannedData(string patientId, byte isActive)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        DataTable dt = new DataTable();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_HIS_External"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spGetScannedData";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("OrganizationId", Helper.organizationId));
                cmd.Parameters.Add(new SqlParameter("patientID", patientId));
                cmd.Parameters.Add(new SqlParameter("isActive", isActive));
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                conn.Close();
            }

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientID", patientId, "getScannedData", StartTime, "OK", MyUser.GetUsername(), "/" + Helper.organizationId.ToString() + "/" + patientId + "/" + isActive.ToString(), "", ""));
            return dt;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientID", patientId, "getScannedData", StartTime, "ERROR", MyUser.GetUsername(), "/" + Helper.organizationId.ToString() + "/" + patientId + "/" + isActive.ToString(), "", ex.Message));
            throw ex;
        }
    }

    public static async Task<string> getPatientHistoryData(Int64 OrganizationId, Int64 PatientId, Int64 AdmissionId, String EncounterId)
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
                return await medicalClient.GetAsync(string.Format($"/patienthistory/" + OrganizationId + "/"+PatientId+"/"+AdmissionId+"/"+EncounterId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientID", PatientId.ToString(), "getPatientHistoryData", StartTime, "OK", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientID", PatientId.ToString(), "getPatientHistoryData", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getHOPEemrData(Int64 OrganizationId, string PatientId, DateTime startDate, DateTime endDate)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var start = startDate.ToString("yyyy-MM-dd");
        var end = endDate.ToString("yyyy-MM-dd");
        try
        {
            HttpClient hopeClient = new HttpClient();
            hopeClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlExtension"].ToString());

            hopeClient.DefaultRequestHeaders.Accept.Clear();
            hopeClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await hopeClient.GetAsync(string.Format($"/listMRHope/" + OrganizationId + "/" + PatientId + "/" + startDate.ToString("yyyy-MM-dd") + "/" + endDate.ToString("yyyy-MM-dd")));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientID", PatientId.ToString(), "getHOPEemrData", StartTime, "OK", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + startDate.ToString("yyyy-MM-dd") + "/" + endDate.ToString("yyyy-MM-dd"), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientID", PatientId.ToString(), "getHOPEemrData", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + startDate.ToString("yyyy-MM-dd") + "/" + endDate.ToString("yyyy-MM-dd"), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getOtherUnitData(Int64 patientID, Int64 organizationId, int year)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlExtension"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/otherunitmr/" + patientID + "/" + organizationId + "/" + year ));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientID", patientID.ToString(), "getOtherUnitData", StartTime, "OK", MyUser.GetUsername(), "/" + patientID.ToString() + "/" + organizationId.ToString() + "/" + year.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "patientID", patientID.ToString(), "getOtherUnitData", StartTime, "ERROR", MyUser.GetUsername(), "/" + patientID.ToString() + "/" + organizationId.ToString() + "/" + year.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

}