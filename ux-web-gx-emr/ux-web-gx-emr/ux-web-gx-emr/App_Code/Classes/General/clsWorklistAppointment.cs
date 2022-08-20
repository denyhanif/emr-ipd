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
/// Summary description for clsWorklistNonAppointment
/// </summary>
public class clsWorklistAppointment
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public clsWorklistAppointment()
    { }

    public static async Task<string> worklistdoctorappointment(Int64 doctorId,Int64 orgId, Int64 admTypeId, DateTime dateselect, string search,int status)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlExtension"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
           
            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/worklistdoctorappointment/" + doctorId + "/" + orgId   + "/" + admTypeId
                     + "/" + dateselect.ToString("yyyy-MM-dd") + "?Search=" + search + "&Status=" + status
                    ));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "worklistdoctorappointment", StartTime, "OK", MyUser.GetUsername(), "/" + doctorId.ToString() + "/" + orgId.ToString() + "/" + admTypeId.ToString()
                     + "/" + dateselect.ToString("yyyy-MM-dd") + "?Search=" + search + "&Status=" + status.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorId", doctorId.ToString(), "worklistdoctorappointment", StartTime, "ERROR", MyUser.GetUsername(), "/" + doctorId.ToString() + "/" + orgId.ToString() + "/" + admTypeId.ToString()
                     + "/" + dateselect.ToString("yyyy-MM-dd") + "?Search=" + search + "&Status=" + status.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> CallPatientMySiloam(Guid HospitalId, Guid DoctorID, Guid AdmissionID)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        CallModel data = new CallModel();
        data.admissionId = AdmissionID;
        data.source = "EMR - Doctor Call";
        data.userId = DoctorID;

        var JsonString = JsonConvert.SerializeObject(data);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
        string mysiloamurlcall = ConfigurationManager.AppSettings["BaseURL_MySiloam_Doctor"].ToString();

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseURL_MySiloam_Doctor"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"" + mysiloamurlcall + "/api/v2/appointments/call/hospital/" + HospitalId + "/doctor/" + DoctorID), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "AdmissionID", AdmissionID.ToString(), "CallPatientMySiloam", StartTime, "OK", MyUser.GetUsername(), "/call/hospital/" + HospitalId.ToString() + "/doctor/" + DoctorID.ToString(), JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "AdmissionID", AdmissionID.ToString(), "CallPatientMySiloam", StartTime, "ERROR", MyUser.GetUsername(), "/call/hospital/" + HospitalId.ToString() + "/doctor/" + DoctorID.ToString(), JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> FirstClickPatientMySiloam(Guid HospitalId, Guid DoctorID, Guid AdmissionID)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        CallModel data = new CallModel();
        data.admissionId = AdmissionID;
        data.source = "EMR - Attendance";
        data.userId = DoctorID;

        var JsonString = JsonConvert.SerializeObject(data);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
        string mysiloamurlcall = ConfigurationManager.AppSettings["BaseURL_MySiloam_Doctor"].ToString();

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseURL_MySiloam_Doctor"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"" + mysiloamurlcall + "/api/v2/appointments/call/hospital/" + HospitalId + "/doctor/" + DoctorID + "?logging=true"), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "AdmissionID", AdmissionID.ToString(), "FirstClickPatientMySiloam", StartTime, "OK", MyUser.GetUsername(), "/call/hospital/" + HospitalId.ToString() + "/doctor/" + DoctorID.ToString() + "?logging=true", JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "AdmissionID", AdmissionID.ToString(), "FirstClickPatientMySiloam", StartTime, "ERROR", MyUser.GetUsername(), "/call/hospital/" + HospitalId.ToString() + "/doctor/" + DoctorID.ToString() + "?logging=true", JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> FirstClickPatientTeleMySiloam(Guid HospitalId, Guid DoctorID, Guid AppointmentID)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        CallModelTele data = new CallModelTele();
        data.appointmentId = AppointmentID;
        data.source = "EMR - Attendance Tele";
        data.userId = DoctorID;
        data.statusId = "8";

        var JsonString = JsonConvert.SerializeObject(data);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
        string mysiloamurlcall = ConfigurationManager.AppSettings["BaseURL_MySiloam_Doctor"].ToString();

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseURL_MySiloam_Doctor"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"" + mysiloamurlcall + "/api/v2/appointments/tele/log/hospital/" + HospitalId + "/doctor/" + DoctorID), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "AppointmentID", AppointmentID.ToString(), "FirstClickPatientTeleMySiloam", StartTime, "OK", MyUser.GetUsername(), "/tele/log/hospital/" + HospitalId.ToString() + "/doctor/" + DoctorID.ToString(), JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "AppointmentID", AppointmentID.ToString(), "FirstClickPatientTeleMySiloam", StartTime, "ERROR", MyUser.GetUsername(), "/tele/log/hospital/" + HospitalId.ToString() + "/doctor/" + DoctorID.ToString(), JsonString, ex.Message));
            return ex.Message;
        }
    }

    public class CallModel
    {
        public Guid admissionId;
        public string source;
        public Guid userId;
    }

    public class CallModelTele
    {
        public Guid appointmentId;
        public string statusId;
        public string source;
        public Guid userId;
    }

    //public static async Task<string> GetScheduleMySiloam(Guid HospitalId, Guid DoctorID)
    //{
    //    string mysiloamurl = ConfigurationManager.AppSettings["BaseURL_SCHEDULE"].ToString();

    //    try
    //    {
    //        HttpClient http = new HttpClient();
    //        http.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseURL_SCHEDULE"].ToString());

    //        http.DefaultRequestHeaders.Accept.Clear();
    //        http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

    //        var task = Task.Run(async () =>
    //        {
    //            return await http.GetAsync(string.Format($"" + mysiloamurl + "/hospital/" + HospitalId + "/doctor/" + DoctorID + "?date=" + "2020-02-18"));
    //        });

    //        return task.Result.Content.ReadAsStringAsync().Result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return ex.Message;
    //    }
    //}

    public static async Task<string> GetRoom(Int64 orgid)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlExtension"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.GetAsync(string.Format($"getroom/" + orgid));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "OrganizationId", orgid.ToString(), "GetRoom", StartTime, "OK", MyUser.GetUsername(), "/" + orgid.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "OrganizationId", orgid.ToString(), "GetRoom", StartTime, "ERROR", MyUser.GetUsername(), "/" + orgid.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> CheckinRoom(Int64 OrgID, Int64 HopeDoctorID, string Date, Guid ScheduleID, Guid RoomMappingID, string isPermanent, Guid HospitalId, Guid DoctorID)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlExtension"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"CheckInRoom/" + OrgID + "/" + HopeDoctorID + "/" + Date + "/" + ScheduleID + "/" + RoomMappingID + "/" + isPermanent + "/" + HospitalId + "/" + DoctorID), null);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "ScheduleID", ScheduleID.ToString(), "CheckinRoom", StartTime, "OK", MyUser.GetUsername(), "/" + OrgID.ToString() + "/" + HopeDoctorID.ToString() + "/" + Date + "/" + ScheduleID.ToString() + "/" + RoomMappingID.ToString() + "/" + isPermanent + "/" + HospitalId.ToString() + "/" + DoctorID.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "ScheduleID", ScheduleID.ToString(), "CheckinRoom", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrgID.ToString() + "/" + HopeDoctorID.ToString() + "/" + Date + "/" + ScheduleID.ToString() + "/" + RoomMappingID.ToString() + "/" + isPermanent + "/" + HospitalId.ToString() + "/" + DoctorID.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> PostLogZoom(string EncID, long OrgID, long PtnID, long AdmID)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"logzoom/" + EncID + "/" + OrgID + "/" + PtnID + "/" + AdmID), null);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "EncounterID", EncID.ToString(), "PostLogZoom", StartTime, "OK", MyUser.GetUsername(), "/" + EncID + "/" + OrgID.ToString() + "/" + PtnID.ToString() + "/" + AdmID.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "EncounterID", EncID.ToString(), "PostLogZoom", StartTime, "ERROR", MyUser.GetUsername(), "/" + EncID + "/" + OrgID.ToString() + "/" + PtnID.ToString() + "/" + AdmID.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

}