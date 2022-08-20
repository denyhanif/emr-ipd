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
using System.Reflection;


/// <summary>
/// Summary description for clsOrderSet
/// </summary>
public class clsOrderSet
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public static async Task<string> getOrderSet(String doctorId, Int64 isCompound, Int64 type)
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
                return await orderSet.GetAsync(string.Format($"/ordersetheader/" + doctorId + "/" + isCompound + "/" + type));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", doctorId.ToString(), "getOrderSet", StartTime, "OK", MyUser.GetUsername(), "/" + doctorId + "/" + isCompound.ToString() + "/" + type.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", doctorId.ToString(), "getOrderSet", StartTime, "ERROR", MyUser.GetUsername(), "/" + doctorId + "/" + isCompound.ToString() + "/" + type.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> deleteDrugsOrderSet(List<OrderSet> delOrderSet)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(delOrderSet);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.PutAsync(string.Format($"/deleteorderset/"), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", MyUser.GetHopeUserID(), "deleteDrugsOrderSet", StartTime, "OK", MyUser.GetUsername(), "", JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", MyUser.GetHopeUserID(), "deleteDrugsOrderSet", StartTime, "ERROR", MyUser.GetUsername(), "", JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> deleteTemplateSOAP(Int64 doctorId, string mappingId, string templateName)
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
                return await orderSet.DeleteAsync(string.Format($"/soaptemplate/"+doctorId+"/"+mappingId+"/"+templateName));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", doctorId.ToString(), "deleteTemplateSOAP", StartTime, "OK", MyUser.GetUsername(), "/" + doctorId.ToString() + "/" + mappingId + "/" + templateName, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", doctorId.ToString(), "deleteTemplateSOAP", StartTime, "ERROR", MyUser.GetUsername(), "/" + doctorId.ToString() + "/" + mappingId + "/" + templateName, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> deleteCompoundOrderSet(List<OrderSet> delOrderSet)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(delOrderSet);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.PutAsync(string.Format($"/deleteordersetcompound/"), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", MyUser.GetHopeUserID(), "deleteCompoundOrderSet", StartTime, "OK", MyUser.GetUsername(), "", JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", MyUser.GetHopeUserID(), "deleteCompoundOrderSet", StartTime, "ERROR", MyUser.GetUsername(), "", JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getDrugbyOrderSetName(string orderSetName, long DoctorId)
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
                return await orderSet.GetAsync(string.Format($"/viewordersetbyname/" + orderSetName + "/" + DoctorId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "getDrugbyOrderSetName", StartTime, "OK", MyUser.GetUsername(), "/" + orderSetName + "/" + DoctorId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "getDrugbyOrderSetName", StartTime, "ERROR", MyUser.GetUsername(), "/" + orderSetName + "/" + DoctorId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getOrdersetLab(string orderSetName, long orgId, long DoctorId)
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
                return await orderSet.GetAsync(string.Format($"/viewordersetlabprescription/" + orderSetName + "/" + orgId + "/" + DoctorId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "getOrdersetLab", StartTime, "OK", MyUser.GetUsername(), "/" + orderSetName + "/" + orgId.ToString() + "/" + DoctorId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "getOrdersetLab", StartTime, "ERROR", MyUser.GetUsername(), "/" + orderSetName + "/" + orgId.ToString() + "/" + DoctorId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getDrugsbyLaboratorySetName(string orderSetName, string organizationId, long doctorid)
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
                return await orderSet.GetAsync(string.Format($"/viewordersetlabmapping/" + orderSetName + "/" + organizationId + "/" + doctorid));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", doctorid.ToString(), "getDrugsbyLaboratorySetName", StartTime, "OK", MyUser.GetUsername(), "/" + orderSetName + "/" + organizationId.ToString() + "/" + doctorid.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", doctorid.ToString(), "getDrugsbyLaboratorySetName", StartTime, "ERROR", MyUser.GetUsername(), "/" + orderSetName + "/" + organizationId.ToString() + "/" + doctorid.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getlistItemLaboratory(string organizationId)
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
                return await orderSet.GetAsync(string.Format($"/cpoemapping/" + organizationId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", organizationId.ToString(), "getlistItemLaboratory", StartTime, "OK", MyUser.GetUsername(), "/" + organizationId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", organizationId.ToString(), "getlistItemLaboratory", StartTime, "ERROR", MyUser.GetUsername(), "/" + organizationId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }


    public static async Task<string> getDose()
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

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "getDose", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "getDose", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getFrequency()
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
                return await orderSet.GetAsync(string.Format($"/frequency"));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "getFrequency", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "getFrequency", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getAdministrationRoute()
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
                return await orderSet.GetAsync(string.Format($"/administrationroute"));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "getAdministrationRoute", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "getAdministrationRoute", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getUOM()
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
                return await orderSet.GetAsync(string.Format($"/uom"));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "insertSaveAsOrderSetRacikan", StartTime, "OK", MyUser.GetUsername(), "", "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "insertSaveAsOrderSetRacikan", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> insertNewLabOrderSet(Guid OrderSetId, string OrderSetName, Int64 DoctorId, List<CpoeTrans> itemList)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(itemList);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.PostAsync(string.Format($"/ordersetlab/"+ OrderSetId + "/" + OrderSetName + "/" + DoctorId), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "insertNewLabOrderSet", StartTime, "OK", MyUser.GetUsername(), "/" + OrderSetId.ToString() + "/" + OrderSetName + "/" + DoctorId.ToString(), JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "insertNewLabOrderSet", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrderSetId.ToString() + "/" + OrderSetName + "/" + DoctorId.ToString(), JsonString, ex.Message));
            return ex.Message;
        }
    }


    public static async Task<string> insertNewTemplateSOAP(string mappingID, Int64 doctorID, SpesificTemplateSet data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(data);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.PostAsync(string.Format($"/soaptemplate/" + doctorID + "/" + mappingID), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", doctorID.ToString(), "insertNewTemplateSOAP", StartTime, "OK", MyUser.GetUsername(), "/" + doctorID + "/" + mappingID.ToString(), JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", doctorID.ToString(), "insertNewTemplateSOAP", StartTime, "ERROR", MyUser.GetUsername(), "/" + doctorID + "/" + mappingID.ToString(), JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> updateTemplateSOAP(string mappingID, Int64 doctorID, string oldName, SpesificTemplateSet data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(data);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.PutAsync(string.Format($"/soaptemplate/" + doctorID + "/" + mappingID + "/"+ oldName), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", doctorID.ToString(), "updateTemplateSOAP", StartTime, "OK", MyUser.GetUsername(), "/" + doctorID.ToString() + "/" + mappingID + "/" + oldName, JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", doctorID.ToString(), "updateTemplateSOAP", StartTime, "ERROR", MyUser.GetUsername(), "/" + doctorID.ToString() + "/" + mappingID + "/" + oldName, JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> insertNewOrderSet(List<InsertOrderSet> itemList)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(itemList);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.PostAsync(string.Format($"/ordersetnew/"+1), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "insertNewOrderSet", StartTime, "OK", MyUser.GetUsername(), "/1", JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "insertNewOrderSet", StartTime, "ERROR", MyUser.GetUsername(), "/1", JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getCompoundbyOrderSetName(string orderSetName, long DoctorId)
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
                return await orderSet.GetAsync(string.Format($"/viewcompoundbyname/" + orderSetName + "/" + DoctorId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "getCompoundbyOrderSetName", StartTime, "OK", MyUser.GetUsername(), "/" + orderSetName + "/" + DoctorId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "getCompoundbyOrderSetName", StartTime, "ERROR", MyUser.GetUsername(), "/" + orderSetName + "/" + DoctorId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getSearchItem(long organizationId, string itemId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHOPE"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/drug/" + organizationId + "/" + itemId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", organizationId.ToString(), "getSearchItem", StartTime, "OK", MyUser.GetUsername(), "/" + organizationId.ToString() + "/" + itemId, "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", organizationId.ToString(), "getSearchItem", StartTime, "ERROR", MyUser.GetUsername(), "/" + organizationId.ToString() + "/" + itemId, "", ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> getItem(long organizationId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHOPE"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/druglite/" + organizationId));
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", organizationId.ToString(), "getItem", StartTime, "OK", MyUser.GetUsername(), "/" + organizationId.ToString(), "", task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", organizationId.ToString(), "getItem", StartTime, "ERROR", MyUser.GetUsername(), "/" + organizationId.ToString(), "", ex.Message));
            return ex.Message;
        }
    }

    public static DataTable GetItemLite()
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
                cmd.CommandText = "spGetDrug";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("OrganizationId", Helper.organizationId));
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                conn.Close();
            }

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", Helper.organizationId.ToString(), "GetItemLite", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
            return dt;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "organizationId", Helper.organizationId.ToString(), "GetItemLite", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            throw ex;
        }
    }

    public static async Task<string> insertSaveAsOrderSet(string OrderSetName, Int64 DoctorId, List<Prescription> itemList)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(itemList);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient orderSetSOAP = new HttpClient();
            orderSetSOAP.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSetSOAP.DefaultRequestHeaders.Accept.Clear();
            orderSetSOAP.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSetSOAP.PostAsync(string.Format($"/ordersetprescription/" + OrderSetName + "/" + DoctorId), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "insertSaveAsOrderSet", StartTime, "OK", MyUser.GetUsername(), "/" + OrderSetName + "/" + DoctorId.ToString(), JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "insertSaveAsOrderSet", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrderSetName + "/" + DoctorId.ToString(), JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> insertSaveAsOrderSetLab(Guid OrderSetId, string OrderSetName, Int64 DoctorId, List<CpoeTrans> itemList)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(itemList);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient orderSetSOAP = new HttpClient();
            orderSetSOAP.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSetSOAP.DefaultRequestHeaders.Accept.Clear();
            orderSetSOAP.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSetSOAP.PostAsync(string.Format($"/ordersetlab/" + OrderSetId + "/" + OrderSetName + "/" + DoctorId), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "insertSaveAsOrderSetLab", StartTime, "OK", MyUser.GetUsername(), "/" + OrderSetId.ToString() + "/" + OrderSetName + "/" + DoctorId.ToString(), JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "insertSaveAsOrderSetLab", StartTime, "ERROR", MyUser.GetUsername(), "/" + OrderSetId.ToString() + "/" + OrderSetName + "/" + DoctorId.ToString(), JsonString, ex.Message));
            return ex.Message;
        }
    }

    public static async Task<string> insertSaveAsOrderSetRacikan(CompoundOrderSet Racikan, Int64 DoctorId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var JsonString = JsonConvert.SerializeObject(Racikan);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient orderSetSOAP = new HttpClient();
            orderSetSOAP.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSetSOAP.DefaultRequestHeaders.Accept.Clear();
            orderSetSOAP.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSetSOAP.PostAsync(string.Format($"/ordersetcompound/" + DoctorId), content);
            });

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "insertSaveAsOrderSetRacikan", StartTime, "OK", MyUser.GetUsername(), "/" + DoctorId.ToString(), JsonString, task.Result.Content.ReadAsStringAsync().Result.ToString()));
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "DoctorId", DoctorId.ToString(), "insertSaveAsOrderSetRacikan", StartTime, "ERROR", MyUser.GetUsername(), "/" + DoctorId.ToString(), JsonString, ex.Message));
            return ex.Message;
        }
    }
}