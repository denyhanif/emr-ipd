using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static FirstAssesment;

public partial class Form_General_UserSetting : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            loadPoliData();

            resetHiddenField();
            getDataSetting();

            ddlForm_Type.SelectedValue = HF_pageselect.Value;
            ddlAdm_Type.SelectedValue = HF_admselect.Value;

            List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
            markerlist.Find(x => x.key == "DOBmarker").value = "marked";
            Session[Helper.SESSIONmarker] = markerlist;

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "", "", "Page_Load", StartTime, EndTime, "OK", MyUser.GetUsername()
                            , "", "", ""));
            //Log.Info(LogConfig.LogEnd());
        }
    }

    //fungsi untuk menampilkan toast via akses javascript
    void ShowToastr(string message, string title, string type)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastr_message",
            String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
    }

    void getDataSetting()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        
        Dictionary<string, string> logParam = new Dictionary<string, string>
        {
            { "Organization_ID", Helper.organizationId.ToString() },
            { "Doctor_ID", Helper.doctorid.ToString() }
        };
        //Log.Debug(LogConfig.LogStart("GetOrganizationSettingbyOrgIdUserID", logParam));
        var usersetting = clsCommon.GetOrganizationSettingbyOrgIdUserID(Helper.organizationId, Helper.doctorid);
        var jsonusersetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(usersetting.Result.ToString());

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "getDataSetting", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", usersetting.Result.ToString()));
        //Log.Debug(LogConfig.LogEnd("GetOrganizationSettingbyOrgIdUserID", jsonusersetting.Status, jsonusersetting.Message));
        Session[Helper.SessionUserSetting] = jsonusersetting.list;

        var get_showbirthday = jsonusersetting.list.Find(n => n.setting_name.ToUpper() == "BIRTHDAY_DOC".ToUpper());
        if (get_showbirthday != null)
        {
            HF_birthday.Value = get_showbirthday.setting_value.ToString().ToUpper();
            settingbirthday.Visible = true;
        }
        else
        {
            HF_birthday.Value = "";
            settingbirthday.Visible = false;
        }

        var get_showfa = jsonusersetting.list.Find(n => n.setting_name.ToUpper() == "SHOW_FA".ToUpper());
        if (get_showfa != null)
        {
            HF_nursesoap.Value = get_showfa.setting_value.ToString().ToUpper();
            settingpopupFA.Visible = true;
        }
        else
        {
            HF_nursesoap.Value = "";
            settingpopupFA.Visible = false;
        }

        var get_defaulttemplate = jsonusersetting.list.Find(n => n.setting_name.ToUpper() == "DEFAULT_SOAP_PAGE".ToUpper());
        if (get_defaulttemplate != null)
        {
            HF_pageselect.Value = get_defaulttemplate.setting_value.ToString().ToLower();
            settingsoaptemplate.Visible = true;
        }
        else
        {
            HF_pageselect.Value = "";
            settingsoaptemplate.Visible = false;
        }

        var get_defaultadmtype = jsonusersetting.list.Find(n => n.setting_name.ToUpper() == "DEFAULT_ADMISSION_TYPE".ToUpper());
        if (get_defaultadmtype != null)
        {
            HF_admselect.Value = get_defaultadmtype.setting_value.ToString().ToUpper();
            settingadmtype.Visible = true;
        }
        else
        {
            HF_admselect.Value = "";
            settingadmtype.Visible = false;
        }

        ScriptManager.RegisterStartupScript(Page, GetType(), "setValue", "getTheSetting();", true);
    }

    protected void ButtonSaveSetting_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        var dataSetting = (List<ViewOrganizationSetting>)Session[Helper.SessionUserSetting];

        var sett_showbirthday = dataSetting.Find(n => n.setting_name.ToUpper() == "BIRTHDAY_DOC".ToUpper());
        if (sett_showbirthday != null)
        {
            dataSetting.Find(n => n.setting_name.ToUpper() == "BIRTHDAY_DOC".ToUpper()).setting_value = HF_birthday.Value.ToUpper();
        }
        var sett_showfa = dataSetting.Find(n => n.setting_name.ToUpper() == "SHOW_FA".ToUpper());
        if (sett_showfa != null)
        {
            dataSetting.Find(n => n.setting_name.ToUpper() == "SHOW_FA".ToUpper()).setting_value = HF_nursesoap.Value.ToUpper();
        }
        var sett_defaulttemplate = dataSetting.Find(n => n.setting_name.ToUpper() == "DEFAULT_SOAP_PAGE".ToUpper());
        if (sett_defaulttemplate != null)
        {
            dataSetting.Find(n => n.setting_name.ToUpper() == "DEFAULT_SOAP_PAGE".ToUpper()).setting_value = ddlForm_Type.SelectedValue.ToUpper();
        }
        var sett_defaultadmtype = dataSetting.Find(n => n.setting_name.ToUpper() == "DEFAULT_ADMISSION_TYPE".ToUpper());
        if (sett_defaultadmtype != null)
        {
            dataSetting.Find(n => n.setting_name.ToUpper() == "DEFAULT_ADMISSION_TYPE".ToUpper()).setting_value = ddlAdm_Type.SelectedValue.ToUpper();

            if (Session[Helper.SessionLastKeywordAppointment] != null)
            {
                List<string> lk = (List<string>)Session[Helper.SessionLastKeywordAppointment];
                if (ddlAdm_Type.SelectedValue.ToUpper() == "OPD")
                {
                    lk[0] = "1";
                }
                else if (ddlAdm_Type.SelectedValue.ToUpper() == "ED")
                {
                    lk[0] = "3";
                }
                Session[Helper.SessionLastKeywordAppointment] = lk;
            }

            if (Session[Helper.SessionLastKeyword] != null)
            {
                List<string> lknon = (List<string>)Session[Helper.SessionLastKeyword];
                if (ddlAdm_Type.SelectedValue.ToUpper() == "OPD")
                {
                    lknon[0] = "1";
                }
                else if (ddlAdm_Type.SelectedValue.ToUpper() == "ED")
                {
                    lknon[0] = "3";
                }
                Session[Helper.SessionLastKeyword] = lknon;
            }
        }

        Dictionary<string, string> logParam = new Dictionary<string, string>
        {
            { "Organization_ID", Helper.organizationId.ToString() },
            { "Doctor_ID", Helper.doctorid.ToString() }
        };
        //Log.Debug(LogConfig.LogStart("SetOrganizationSettingbyOrgIdUserID", logParam, LogConfig.JsonToString(dataSetting)));
        var post_orgsetting = clsCommon.SetOrganizationSettingbyOrgIdUserID(dataSetting, Helper.organizationId, Helper.doctorid);
        var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(post_orgsetting.Result);
        var Status = Response.Property("status").Value.ToString();
        var Message = Response.Property("message").Value.ToString();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "ButtonSaveSetting_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", post_orgsetting.Result.ToString()));
        //Log.Debug(LogConfig.LogEnd("SetOrganizationSettingbyOrgIdUserID", Status, Message));

        if (Status == "Fail")
        {
            p_Add.Attributes.Remove("style");
            p_Add.Attributes.Add("style", "display:block; color:red;");
            p_Add.InnerText = "Update Setting Failed!";
            ShowToastr(Status + "! " + Message, "Save Failed", "error");
        }
        else
        {
            p_Add.Attributes.Remove("style");
            p_Add.Attributes.Add("style", "display:block; color:green;");
            p_Add.InnerText = "Save Changes Success!";
            ShowToastr(Status + "! " + Message, "Save Success", "success");
        }

        getDataSetting();

        logParam = new Dictionary<string, string>
        {
            { "Organization_ID", Helper.organizationId.ToString() },
            { "Doctor_ID", Helper.doctorid.ToString() }
        };
        //Log.Debug(LogConfig.LogStart("GetOrganizationSettingbyOrgId", logParam));
        var orgsetting = clsCommon.GetOrganizationSettingbyOrgId(Helper.organizationId, Helper.doctorid);
        var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());

        EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "ButtonSaveSetting_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", orgsetting.Result.ToString()));
        //Log.Debug(LogConfig.LogEnd("GetOrganizationSettingbyOrgId", jsonorgsetting.Status, jsonorgsetting.Message));
        Session[Helper.SessionOrganizationSetting] = jsonorgsetting.list;

        //Log.Info(LogConfig.LogEnd());
    }

    void resetHiddenField()
    {
        HF_birthday.Value = "";
        HF_nursesoap.Value = "";
        HF_pageselect.Value = "";
        HF_admselect.Value = "";
    }

    void loadPoliData()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        List<PageSpecialty> listpagedata = new List<PageSpecialty>();

        Dictionary<string, string> logParam = new Dictionary<string, string>
        {
            { "Organization_ID", "1" },
            { "Doctor_ID", Helper.GetDoctorID(this) }
        };
        //Log.Debug(LogConfig.LogStart("GetPageSpecialty", logParam));
        var pagedata = clsFirstAssesment.GetPageSpecialty(long.Parse(Helper.GetDoctorID(this)), 1);
        var Jsonpagedata = JsonConvert.DeserializeObject<ResultPageSpecialty>(pagedata.Result.ToString());
        
        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "loadPoliData", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", pagedata.Result.ToString()));
        //Log.Debug(LogConfig.LogEnd("GetPageSpecialty", Jsonpagedata.Status, Jsonpagedata.Message));

        listpagedata = Jsonpagedata.list;

        DataTable pagedatadt = Helper.ToDataTable(listpagedata);

        ddlForm_Type.DataSource = pagedatadt;
        ddlForm_Type.DataTextField = "page_specialty_name";
        ddlForm_Type.DataValueField = "page_specialty_id";
        ddlForm_Type.DataBind();
    }
}