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
using System.Data;
using System.Data.SqlClient;
using log4net;
using static SPDocument;
using System.Reflection;

/// <summary>
/// Summary description for clsSOAP
/// </summary>
public class clsSOAP
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public clsSOAP()
    {
        
        //
        // TODO: Add constructor logic here
        //
    }

    public static async Task<string> getDosage()
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
                return await orderSet.GetAsync(string.Format($"/dose"));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "getDosage", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "getDosage", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getSOAP(string encounter_ticket_id,long patient_id,long admission_id,long organization_id,long doctor_id)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/SOAP/" + encounter_ticket_id + "/" + patient_id + "/" + admission_id + "/" + organization_id + "/" + doctor_id));
            });

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", encounter_ticket_id, "getSOAP", StartTime, EndTime, "OK", MyUser.GetUsername(), "/" + encounter_ticket_id + "/" + patient_id + "/" + admission_id + "/" + organization_id + "/" + doctor_id, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "encounter_ticket_id", encounter_ticket_id, "getSOAP", StartTime, ErrorTime, "ERROR", MyUser.GetUsername(), "/" + encounter_ticket_id + "/" + patient_id + "/" + admission_id + "/" + organization_id + "/" + doctor_id, "", ex.Message));

            return ex.Message;
        }
    }

    public static async Task<string> getCopySOAP(long OrganizationId, string PatientId, string Search)
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
                return await httpLogin.GetAsync(string.Format($"/admissioncopysoap/" + OrganizationId + "/" + PatientId + "?Search=" + Search ));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId, "getCopySOAP", StartTime, "OK", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId + "?Search=" + Search, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId, "getCopySOAP", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId + "?Search=" + Search, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getCopySOAPDoctor(long OrganizationId, string PatientId, long DoctorId)
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
                return await httpLogin.GetAsync(string.Format($"/admissioncopysoapdoctor/" + OrganizationId + "/" + PatientId + "/" + DoctorId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId, "getCopySOAPDoctor", StartTime, "OK", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId + "/" + DoctorId, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId, "getCopySOAPDoctor", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId + "/" + DoctorId, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getCopyPrescription(string PatientId, long OrgId)
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
                return await httpLogin.GetAsync(string.Format($"/admissionprescriptionhope/" + PatientId + "/" + OrgId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getCopyPrescription", StartTime, "OK", MyUser.GetUsername(), "/" + PatientId.ToString() + "/" + OrgId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getCopyPrescription", StartTime, "ERROR", MyUser.GetUsername(), "/" + PatientId.ToString() + "/" + OrgId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetPrescriptionHope(long admissionid, long OrgId)
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
                return await httpLogin.GetAsync(string.Format($"/prescriptionhope/" + admissionid + "/" + OrgId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionid", admissionid.ToString(), "GetPrescriptionHope", StartTime, "OK", MyUser.GetUsername(), "/" + admissionid.ToString() + "/" + OrgId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admissionid", admissionid.ToString(), "GetPrescriptionHope", StartTime, "ERROR", MyUser.GetUsername(), "/" + admissionid.ToString() + "/" + OrgId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetSOAPAdditionalInfo(long doctorid, long organizationid)
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
                return await httpLogin.GetAsync(string.Format($"/soapadditionalinfo/" + doctorid + "/" + organizationid));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorid", doctorid.ToString(), "GetSOAPAdditionalInfo", StartTime, "OK", MyUser.GetUsername(), "/" + doctorid.ToString() + "/" + organizationid.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "doctorid", doctorid.ToString(), "GetSOAPAdditionalInfo", StartTime, "ERROR", MyUser.GetUsername(), "/" + doctorid.ToString() + "/" + organizationid.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> SaveAsDraftSOAP(SOAP savesoap, string pageid, string appointmentid, string username)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //log.Info(LogLibrary.Logging("E", "btnSignIn_Click", txtUsername.Text.ToString(), HospitalList.Count().ToString()));
        var JsonString = JsonConvert.SerializeObject(savesoap);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
        var postDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"/SOAP/0/0/0/"+ pageid+ "/saveasdraft/" + postDate + "/" + appointmentid + "/" + username), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SaveAsDraftSOAP", StartTime, "OK", MyUser.GetUsername(), "/" + pageid + "/saveasdraft/" + postDate + "/" + appointmentid + "/" + username, JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SaveAsDraftSOAP", StartTime, "ERROR", MyUser.GetUsername(), "/" + pageid + "/saveasdraft/" + postDate + "/" + appointmentid + "/" + username, JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> SubmitSOAP(SOAP savesoap,long itemid, double ConsultationAmount, double DiscountAmount,string ProcedureNotes,string pageid,string consname, string appointmentid, string username)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //log.Info(LogLibrary.Logging("E", "btnSignIn_Click", txtUsername.Text.ToString(), HospitalList.Count().ToString()));
        if (ProcedureNotes == "")
        {
            ProcedureNotes = "-";
        }

        if (consname == "")
        {
            consname = "-";
        }

        consname = consname.Replace("/", " or ");


        var JsonString = JsonConvert.SerializeObject(savesoap);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
        var postDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"/SOAP/"+ itemid + "/" + ConsultationAmount + "/" + DiscountAmount +"/" + pageid + "/" + consname + "/" + postDate + "/" + appointmentid + "/" + username), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SubmitSOAP", StartTime, "OK", MyUser.GetUsername(), "/" + itemid.ToString() + "/" + ConsultationAmount.ToString() + "/" + DiscountAmount.ToString() + "/" + pageid + "/" + consname + "/" + postDate + "/" + appointmentid + "/" + username, JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SubmitSOAP", StartTime, "ERROR", MyUser.GetUsername(), "/" + itemid.ToString() + "/" + ConsultationAmount.ToString() + "/" + DiscountAmount.ToString() + "/" + pageid + "/" + consname + "/" + postDate + "/" + appointmentid + "/" + username, JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static DataTable getItemPres(long orgId,Int16 admtypeid)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        DataTable dt = new DataTable();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_Emr"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spGetDrugPrescriptionNew";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("organizationid", orgId));
                cmd.Parameters.Add(new SqlParameter("admissiontypeid", admtypeid));
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                conn.Close();
            }
            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admtypeid", admtypeid.ToString(), "getItemPres", StartTime, "OK", MyUser.GetUsername(), "/" + orgId.ToString() + "/" + admtypeid.ToString(), "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admtypeid", admtypeid.ToString(), "getItemPres", StartTime, "ERROR", MyUser.GetUsername(), "/" + orgId.ToString() + "/" + admtypeid.ToString(), "", ex.Message));
            throw ex;
        }
        return dt;
    }

    public static DataTable getItemConsumables(long orgId, Int16 admtypeid)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        DataTable dt = new DataTable();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_Emr"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spGetConsumablePrescription";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("organizationid", orgId));
                cmd.Parameters.Add(new SqlParameter("admissiontypeid", admtypeid));
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                conn.Close();
            }
            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admtypeid", admtypeid.ToString(), "getDiseaseClassification", StartTime, "OK", MyUser.GetUsername(), "/" + orgId.ToString() + "/" + admtypeid.ToString(), "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "admtypeid", admtypeid.ToString(), "getDiseaseClassification", StartTime, "ERROR", MyUser.GetUsername(), "/" + orgId.ToString() + "/" + admtypeid.ToString(), "", ex.Message));
            throw ex;
        }
        return dt;
    }

    public static DataTable getDiseaseClassification(string search)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        DataTable dt = new DataTable();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_Emr"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spGetDiseaseClassification";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Search", search));
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                conn.Close();
            }
            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "userID", MyUser.GetHopeUserID(), "getDiseaseClassification", StartTime, "OK", MyUser.GetUsername(), "/" + search, "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "userID", MyUser.GetHopeUserID(), "getDiseaseClassification", StartTime, "ERROR", MyUser.GetUsername(), "/" + search, "", ex.Message));
            throw ex;
        }
        return dt;
    }

    public static async Task<string> GetConsultationFee(long DoctorId, long OrganizationId,long AdmissionId,Guid EncounterId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/consultationfee/" + DoctorId + "/" + OrganizationId + "/" + AdmissionId + "/" + EncounterId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "EncounterId", EncounterId.ToString(), "GetConsultationFee", StartTime, "OK", MyUser.GetUsername(), "/" + DoctorId.ToString() + "/" + OrganizationId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "EncounterId", EncounterId.ToString(), "GetConsultationFee", StartTime, "ERROR", MyUser.GetUsername(), "/" + DoctorId.ToString() + "/" + OrganizationId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getVaccine()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient vaccine = new HttpClient();
            vaccine.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            vaccine.DefaultRequestHeaders.Accept.Clear();
            vaccine.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await vaccine.GetAsync(string.Format($"/vaccine"));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserId", MyUser.GetHopeUserID(), "getVaccine", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserId", MyUser.GetHopeUserID(), "getVaccine", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getOrderSetRacikan(string ordersetname, long OrganizationId, long DoctorId,  int is_additional)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient vaccine = new HttpClient();
            vaccine.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            vaccine.DefaultRequestHeaders.Accept.Clear();
            vaccine.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await vaccine.GetAsync(string.Format($"/ordersetcompound/" + ordersetname + "/" + OrganizationId + "/" + DoctorId + "/" + is_additional));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "getOrderSetRacikan", StartTime, "OK", MyUser.GetUsername(), "/" + ordersetname + "/" + OrganizationId.ToString() + "/" + DoctorId.ToString() + "/" + is_additional.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "getOrderSetRacikan", StartTime, "ERROR", MyUser.GetUsername(), "/" + ordersetname + "/" + OrganizationId.ToString() + "/" + DoctorId.ToString() + "/" + is_additional.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static DataTable getDiagnosticProcedure(long orgId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        DataTable dt = new DataTable();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_Emr"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spGetDiagnosticProcedure";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("organizationid", orgId));
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                conn.Close();
            }
            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", orgId.ToString(), "getDiagnosticProcedure", StartTime, "OK", MyUser.GetUsername(), "/" + orgId.ToString(), "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", orgId.ToString(), "getDiagnosticProcedure", StartTime, "ERROR", MyUser.GetUsername(), "/" + orgId.ToString(), "", ex.Message));
            throw ex;
        }
        return dt;
    }


    public static async Task<string> getSOAPWDoc(string encounter_ticket_id, long patient_id, long admission_id, long organization_id, long doctor_id)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/SOAPWDOC/" + encounter_ticket_id + "/" + patient_id + "/" + admission_id + "/" + organization_id + "/" + doctor_id));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", encounter_ticket_id, "getSOAPWDoc", StartTime, "OK", MyUser.GetUsername(), "/" + encounter_ticket_id + "/" + patient_id.ToString() + "/" + admission_id.ToString() + "/" + organization_id.ToString() + "/" + doctor_id.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", encounter_ticket_id, "getSOAPWDoc", StartTime, "ERROR", MyUser.GetUsername(), "/" + encounter_ticket_id + "/" + patient_id.ToString() + "/" + admission_id.ToString() + "/" + organization_id.ToString() + "/" + doctor_id.ToString(), "", ex.Message));
            return ex.Message;
        }
    }


    public static async Task<string> GetFormTypeHOPE()
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
                return await httpLogin.GetAsync(string.Format($"/mrformtype/"));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "userID", MyUser.GetHopeUserID(), "GetFormTypeHOPE", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "userID", MyUser.GetHopeUserID(), "GetFormTypeHOPE", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> GetFormTypeHOPETemplate(long OrganizationId, string TemplateName)
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
                return await httpLogin.GetAsync(string.Format($"/mrformtypetemplate/" + OrganizationId + "/" + TemplateName));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "TemplateName", TemplateName, "GetFormTypeHOPETemplate", StartTime, "OK", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + TemplateName, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "TemplateName", TemplateName, "GetFormTypeHOPETemplate", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + TemplateName, "", ex.Message));
            return string.Empty;
        }
    }

    public static async Task<string> SaveAsDraftSOAPWDoc(SOAPSDocument savesoap, string pageid, string appointmentid, string username)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //log.Info(LogLibrary.Logging("E", "btnSignIn_Click", txtUsername.Text.ToString(), HospitalList.Count().ToString()));
        var JsonString = JsonConvert.SerializeObject(savesoap);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
        var postDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"/SOAPWDOC/0/0/0/" + pageid + "/saveasdraft/" + postDate + "/" + appointmentid + "/" + username), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SaveAsDraftSOAPWDoc", StartTime, "OK", MyUser.GetUsername(), "/" + pageid + "/saveasdraft/" + postDate + "/" + appointmentid + "/" + username, JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SaveAsDraftSOAPWDoc", StartTime, "ERROR", MyUser.GetUsername(), "/" + pageid + "/saveasdraft/" + postDate + "/" + appointmentid + "/" + username, JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> SubmitSOAPWDoc(SOAPSDocument savesoap, long itemid, double ConsultationAmount, double DiscountAmount, string ProcedureNotes, string pageid, string consname, string appointmentid, string username)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //log.Info(LogLibrary.Logging("E", "btnSignIn_Click", txtUsername.Text.ToString(), HospitalList.Count().ToString()));
        if (ProcedureNotes == "")
        {
            ProcedureNotes = "-";
        }

        if (consname == "")
        {
            consname = "-";
        }

        consname = consname.Replace("/", " or ");

        var JsonString = JsonConvert.SerializeObject(savesoap);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
        var postDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"/SOAPWDOC/" + itemid + "/" + ConsultationAmount + "/" + DiscountAmount + "/" + pageid + "/" + consname + "/" + postDate + "/" + appointmentid + "/" + username), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SubmitSOAPWDoc", StartTime, "OK", MyUser.GetUsername(), "/" + itemid.ToString() + "/" + ConsultationAmount.ToString() + "/" + DiscountAmount.ToString() + "/" + pageid + "/" + consname + "/" + postDate + "/" + appointmentid + "/" + username, JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", savesoap.encounter_ticket_id.ToString(), "SubmitSOAPWDoc", StartTime, "ERROR", MyUser.GetUsername(), "/" + itemid.ToString() + "/" + ConsultationAmount.ToString() + "/" + DiscountAmount.ToString() + "/" + pageid + "/" + consname + "/" + postDate + "/" + appointmentid + "/" + username, JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getPatientReferral(long OrganizationId, long PatientId, long AdmissionId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/patientreferral/" + OrganizationId + "/" + PatientId + "/" + AdmissionId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "patientreferral", StartTime, "OK", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "patientreferral", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getPatientReferralBalasan(long OrganizationId, long PatientId, long AdmissionId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/patientreferralbalasan/" + OrganizationId + "/" + PatientId + "/" + AdmissionId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getPatientReferralBalasan", StartTime, "OK", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "PatientId", PatientId.ToString(), "getPatientReferralBalasan", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getSpeciality()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            string mysiloamurl = ConfigurationManager.AppSettings["BaseURL_MySiloam_OPAdmin"].ToString();

            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseURL_MySiloam_OPAdmin"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"" + mysiloamurl + "/api/v2/generals/specialities"));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "getSpeciality", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "getSpeciality", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getDoctorByOrg(string hospitalId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            string mysiloamurl = ConfigurationManager.AppSettings["BaseURL_MySiloam_OPAdmin"].ToString();

            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseURL_MySiloam_OPAdmin"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"" + mysiloamurl + "/api/v2/doctors/hospital/" + hospitalId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "hospitalId", hospitalId, "getDoctorByOrg", StartTime, "OK", MyUser.GetUsername(), "/" + hospitalId, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "hospitalId", hospitalId, "getDoctorByOrg", StartTime, "ERROR", MyUser.GetUsername(), "/" + hospitalId, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getProcedure(long organizationId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            string mysiloamurl = ConfigurationManager.AppSettings["urlIPDOT"].ToString();

            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlIPDOT"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"" + mysiloamurl + "/procedureitem/list/" + organizationId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "hospitalId", organizationId.ToString(), "getDoctorByOrg", StartTime, "OK", MyUser.GetUsername(), "/" + organizationId, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "hospitalId", organizationId.ToString(), "getDoctorByOrg", StartTime, "ERROR", MyUser.GetUsername(), "/" + organizationId, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> syncReferal(long organization_id)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/autosyncreferral/" + organization_id));
            });

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "syncReferal", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "syncReferal", StartTime, "ERROR", MyUser.GetUsername(), "/" + "OrganizationId"+ "/" + organization_id.ToString(), "", ex.Message));

            return ex.Message;
        }
    }
    
    public static async Task<string> referralResume(long organization_id,long patient_id,long admission_id, string encounter_ticket_id)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/autosyncreferral/" + organization_id + patient_id + admission_id + encounter_ticket_id));
            });
                
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", encounter_ticket_id, "referralResume", StartTime, EndTime, "OK", MyUser.GetUsername(), "/" + encounter_ticket_id + "/" + patient_id + "/" + admission_id + "/" + organization_id + "/", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "encounter_ticket_id", encounter_ticket_id, "referralResume", StartTime, ErrorTime, "ERROR", MyUser.GetUsername(), "/" + encounter_ticket_id + "/" + patient_id + "/" + admission_id + "/" + organization_id + "/", "", ex.Message));

            return ex.Message;
        }
    }

    public static async Task<string> GetPatientReferral(long OrganizationId,long PatientId,long AdmissionId) 
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/autosyncreferral/" + OrganizationId + PatientId + AdmissionId ));
            });

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "getPatientreferral", StartTime, EndTime, "OK", MyUser.GetUsername(), "/" + OrganizationId + "/" + PatientId + "/" + AdmissionId + "/", "", "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "getPatientreferral", StartTime, ErrorTime, "ERROR", MyUser.GetUsername(), "/" + OrganizationId + "/" + PatientId + "/" + AdmissionId + "/", "", "", "", ex.Message));

            return ex.Message;
        }
    }

    public static async Task<string> CancelReferral()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlTransaction"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/cancelreferral"));
            });

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "CancelReferral", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "CancelReferral", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));

            return ex.Message;
        }
    }

    public static async Task<string> SaveInpatientData(InpatientData data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(data);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
        var postDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlIPDOT"].ToString());


            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"/OperationSchedule/additional"), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", data.encounter_id.ToString(), "SaveRawatInap", StartTime, "OK", MyUser.GetUsername(), "/" + "" + "/saveasdraft/" + postDate + "/" + "" + "/" + "", JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", data.encounter_id.ToString(), "SaveRawatInap", StartTime, "ERROR", MyUser.GetUsername(), "/" + "" + "/saveasdraft/" + postDate + "/" + "" + "/" + "", JsonString, ex.Message));
            return ex.Message;
        }
    }
   
    public static async Task<string> getRawatInap(long organizationId, string operation_schecule_id, long patient_id, string addmision_no,string encounter_id)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlIPDOT"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/OperationSchedule/additional/" + organizationId + "/" + operation_schecule_id + "/" + patient_id + "/" + addmision_no + "/" + encounter_id ));
            });

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "hospitalId", organizationId.ToString(), "getInpatientData", StartTime, "OK", MyUser.GetUsername(), "/" + organizationId, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "hospitalId", organizationId.ToString(), "getInpatientData", StartTime, "ERROR", MyUser.GetUsername(), "/" + organizationId, "", ex.Message));

            return ex.Message;
        }
    }

    public static async Task<string> getWardByOrg(long organizationId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            string mysiloamurl = ConfigurationManager.AppSettings["urlIPDOT"].ToString();

            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlIPDOT"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"" + mysiloamurl + "/ward/" + organizationId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "hospitalId", organizationId.ToString(), "getWardByOrg", StartTime, "OK", MyUser.GetUsername(), "/" + organizationId, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "hospitalId", organizationId.ToString(), "getWardByOrg", StartTime, "ERROR", MyUser.GetUsername(), "/" + organizationId, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getRecoveryRoomByOrg(long organizationId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            string mysiloamurl = ConfigurationManager.AppSettings["urlIPDOT"].ToString();

            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlIPDOT"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"" + mysiloamurl + "/recoveryroom/" + organizationId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "hospitalId", organizationId.ToString(), "RecoveryRoomByOrg", StartTime, "OK", MyUser.GetUsername(), "/" + organizationId, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "hospitalId", organizationId.ToString(), "RecoverRoomByOrg", StartTime, "ERROR", MyUser.GetUsername(), "/" + organizationId, "", ex.Message));
            return ex.Message;
        }
    }
    
    public static async Task<string> InpatientCancel(InpatientDeleteParam deleteParam)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var jsonString = JsonConvert.SerializeObject(deleteParam);
        var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
        var postDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlIPDOT"].ToString());


            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.PostAsync(string.Format($"/operationschedule/cancelpoc"), httpContent);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", deleteParam.encounter_id.ToString(), "CancelRawatInap", StartTime, "OK", MyUser.GetUsername(), "/" + "" + "/saveasdraft/" + postDate + "/" + "" + "/" + "", jsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "encounter_ticket_id", deleteParam.encounter_id.ToString(), "CancelRawatInap", StartTime, "ERROR", MyUser.GetUsername(), "/" + "" + "/saveasdraft/" + postDate + "/" + "" + "/" + "", jsonString, ex.Message));
            return ex.Message;
        }
    }


}