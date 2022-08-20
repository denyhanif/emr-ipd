using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    public static List<Frequency> listfrequency;
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Init(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;

            lbltime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");

            DataTable dt = (DataTable)Session[Helper.SessionListOrganization];
            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];

            if (dt != null)
            {
                Doctor doc = (Doctor)Session[Helper.SessionDoctor];

                if (doc.doctor_id != 0)
                {
                    hfUserID.Value = dt.Rows[0]["user_id"].ToString();
                    hfDoctorID.Value = doc.doctor_id.ToString();
                    Session[Helper.SessionDoctorIdLogin] = doc.doctor_id;
                    lblUsername.Text = doc.name.ToString();
                    ViewOrganizationSetting tempvaluecompound = orgsetting.Find(y => y.setting_name.ToUpper() == "IS_COMPOUND".ToUpper());
                    hfCompoundflag.Value = tempvaluecompound.setting_value;
                    hfUserFullName.Value = (String)Session[Helper.SessionUserFullName];
                    lblFullname.Text = (String)Session[Helper.SessionUserFullName];
                    hfHospitalID.Value = dt.Rows[0]["mobile_organization_id"].ToString();
                    hfOrgName.Value = dt.Rows[0]["organization_name"].ToString();
                }

                ddlOrganization.DataSource = dt;
                ddlOrganization.DataValueField = "hope_organization_id";
                ddlOrganization.DataTextField = "organization_name";
                ddlOrganization.DataBind();

                btnhelp.NavigateUrl = "http://" + Helper.GetLocalIPAddress().ToString() + ConfigurationManager.AppSettings["HelperPath"].ToString();

                //if (Session["itempres"] == null)
                //{
                //    long x = Int64.Parse(ddlOrganization.SelectedValue);
                //    var getMapItemDrug = clsPrescription.getItemPrescription(2, 1);
                //    var getMapJsonItemDrug = JsonConvert.DeserializeObject<ResultItem>(getMapItemDrug.Result.ToString());
                //    List<Item> listitem = getMapJsonItemDrug.list;
                //    Session["itempres"] = listitem;
                //}
                //var frequencyData = clsOrderSet.getFrequency();
                //var Jsonfrequency = JsonConvert.DeserializeObject<ResultFrequency>(frequencyData.Result.ToString());
                //listfrequency = Jsonfrequency.list;
                //DataTable dtfreq = Helper.ToDataTable(listfrequency);
                //Session["freq"] = dtfreq;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlOrganization.Items[i].Attributes.CssStyle.Add("Color", "Black");
                }

                if (Session[Helper.SessionLanguage] == null)
                {
                    Session[Helper.SessionLanguage] = Convert.ToInt32(ddlLanguage.SelectedValue);
                }
                else
                {
                    ddlLanguage.SelectedValue = Session[Helper.SessionLanguage].ToString();
                }

                if (Session[Helper.SessionOrganization] == null)
                {
                    Session[Helper.SessionOrganization] = Convert.ToInt32(ddlOrganization.SelectedValue);
                }
                else
                {
                    ddlOrganization.SelectedValue = Session[Helper.SessionOrganization].ToString();
                }

                DataRow[] result = dt.Select("hope_organization_id = " + Convert.ToInt32(ddlOrganization.SelectedValue));
                foreach (DataRow row in result)
                {
                    hfRoleID.Value = row["role_id"].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Form/General/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session[Helper.SessionListOrganization];
        if (!IsPostBack)
        {
            SetLinkWorklist();

            if (Session[Helper.SESSIONmarker] == null)
            {
                Session[Helper.SESSIONmarker] = Helper.setInitializeMarker();
            }

            if (dt != null)
            {
                DateTime birth = DateTime.Parse(dt.Rows[0]["birthday"].ToString());
                DateTime daynow = DateTime.Now;

                List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                string DOBmarker = markerlist.Find(x => x.key == "DOBmarker").value;

                if (birth.Day == daynow.Day && birth.Month == daynow.Month && DOBmarker == "unmarked")
                {
                    List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
                    if (orgsetting.Where(y => y.setting_name.ToLower() == "BIRTHDAY_DOC".ToLower()).Count() > 0)
                    {
                        if (orgsetting.Find(y => y.setting_name.ToUpper() == "BIRTHDAY_DOC".ToUpper()).setting_value == "TRUE")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notif", "$('#dataBirthday').modal('show');", addScriptTags: true);
                            LabelUserBirthday.Text = dt.Rows[0]["full_name"].ToString();
                            divconfetti.Visible = true;
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("~/Form/General/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
        detectPatientMenu();
    }

    //public class OTNotif
    //{
    //    public long doctorId { get; set; }
    //    public int total { get; set; }
    //}

    //public class ResultOTNotif
    //{
    //    private List<OTNotif> lists = new List<OTNotif>();
    //    [JsonProperty("data")]
    //    public List<OTNotif> list { get { return lists; } }
    //}

    protected void countOTNotif()
    {
        var json_notif = clsDoctor.GetCountNotifOT(ddlOrganization.SelectedValue.ToString(), hfDoctorID.Value);
        var data_notif = JsonConvert.DeserializeObject<dynamic>(json_notif.Result.ToString());
        try
        {
            if (data_notif != null)
            {
                var x = data_notif.Property("data").Value["total"].ToString();
                int data = int.Parse(x);

                if (data > 0)
                {
                    LabelNotifOT.Text = data.ToString();
                    LabelNotifOT.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());
        }
    }

    protected void detectPatientMenu()
    {
        //if (Session[Helper.SessionQSPatient] != null)
        if (Request.QueryString["idPatient"] != null)
        {
            Li1.Visible = false;
            Li2.Visible = true;
            Li3.Visible = true;
            Li4.Visible = false;
            Li5.Visible = true;
            Li6.Visible = true;
            Li7.Visible = true;
            LiCV.Visible = true;
            LiDiabisa.Visible = true;

            LiWorklistIPD.Visible = false;
            LiRecapIPD.Visible = false;
            LiRecapOPD.Visible = false;
            LiOT.Visible = false;
            
        }
        else
        {
            Li1.Visible = true;
            Li2.Visible = false;
            Li3.Visible = false;
            Li4.Visible = false;
            Li5.Visible = false;
            Li6.Visible = false;
            Li7.Visible = false;
            LiCV.Visible = false;
            LiDiabisa.Visible = false;

            LiWorklistIPD.Visible = true;
            LiRecapIPD.Visible = true;
            LiRecapOPD.Visible = true;
            LiOT.Visible = true;
            
        }

        if (Session[Helper.SessionOrganizationSetting] != null)
        {
            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Where(y => y.setting_name.ToUpper() == "USE_OTSCHEDULING".ToUpper()) != null)
            {
                if (orgsetting.Find(y => y.setting_name.ToUpper() == "USE_OTSCHEDULING".ToUpper()).setting_value == "FALSE")
                {
                    LiOT.Visible = false;
                }
            }

            if (orgsetting.Where(y => y.setting_name.ToUpper() == "USE_IPDDOCTOR".ToUpper()) != null)
            {
                if (orgsetting.Find(y => y.setting_name.ToUpper() == "USE_IPDDOCTOR".ToUpper()).setting_value == "FALSE")
                {
                    LiWorklistIPD.Visible = false;
                }
                else
                {
                    LiRecapIPD.Visible = false;
                }
            }
        }
    }

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    }

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        HttpContext.Current.Session[Helper.SessionLanguage] = ddlLanguage.SelectedValue;
        //Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);  
        Response.Redirect(Request.RawUrl);
    }

    protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        HttpContext.Current.Session[Helper.SessionOrganization] = ddlOrganization.SelectedValue;
        Session[Helper.SessionItemDrugPres] = null;

        var orgsetting = clsCommon.GetOrganizationSettingbyOrgId(long.Parse(ddlOrganization.SelectedValue.ToString()), long.Parse(hfDoctorID.Value.ToString()));
        var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());
        Session[Helper.SessionOrganizationSetting] = jsonorgsetting.list;

        //var getMapItemDrug = clsPrescription.getItemPrescription(2, 1);
        //var getMapJsonItemDrug = JsonConvert.DeserializeObject<ResultItem>(getMapItemDrug.Result.ToString());
        //List<Item> listitem = getMapJsonItemDrug.list;
        ////DataTable dtitem = Helper.ToDataTable(listitem);
        //Session["itempres"] = listitem;

        GetWorklistType();

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "DoctorID", hfDoctorID.Value.ToString(), "ddlOrganization_SelectedIndexChanged", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
    }

    protected void linkGoSomewhere_Click(object sender, EventArgs e)
    {
        GetWorklistType();
    }

    void GetWorklistType()
    {
        var WorklistType = clsCommon.GetSettingValue(Int64.Parse(MyUser.GetHopeOrgID()), "WORKLIST_TYPE").Result.ToString();

        if (WorklistType == "APPOINTMENT")
        {
            
            Response.Redirect("~/Form/General/WorklistAppointment.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        else if (WorklistType == "NON_APPOINTMENT")
        {
            
            Response.Redirect("~/Form/General/WorklistNonAppointment.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }

    void SetLinkWorklist()
    {
        string type = (string)Session[CstSession.SessionWorklistType];
        if (type == "APPOINTMENT")
        {
            linkGoSomewhere.NavigateUrl = "~/Form/General/WorklistAppointment.aspx";
        }
        else if (type == "NON_APPOINTMENT")
        {
            linkGoSomewhere.NavigateUrl = "~/Form/General/WorklistNonAppointment.aspx";
        }
    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Form/General/Login.aspx", false);
        Context.ApplicationInstance.CompleteRequest();
    }

    protected void ButtonRefreshNotifOT_Click(object sender, EventArgs e)
    {
        countOTNotif();
    }
}