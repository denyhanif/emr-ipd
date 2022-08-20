using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Configuration;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Reflection;
using System.Web.Services;


public partial class Form_General_Login : System.Web.UI.Page
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();

        if (!IsPostBack)
        {
            string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            //if (Session[Helper.SessionDoctor] != null)
            //{
            //    var WorklistType = clsCommon.GetSettingValue(Helper.organizationId, "WORKLIST_TYPE").Result.ToString();

            //    if (WorklistType == "APPOINTMENT")
            //    {
            //        Response.Redirect("~/Form/General/WorklistAppointment.aspx", false);
            //        Context.ApplicationInstance.CompleteRequest();
            //    }
            //    else if (WorklistType == "NON_APPOINTMENT")
            //    {
            //        Response.Redirect("~/Form/General/WorklistNonAppointment.aspx", false);
            //        Context.ApplicationInstance.CompleteRequest();
            //    }
            //}

            var registryflag = ConfigurationManager.AppSettings["registryflag"].ToString();

            if (registryflag == "1")
            {
                ConfigurationManager.AppSettings["urlUserManagement"] = SiloamConfig.Functions.GetValue("urlUserManagement").ToString();
                ConfigurationManager.AppSettings["urlTransaction"] = SiloamConfig.Functions.GetValue("urlTransaction").ToString();
                ConfigurationManager.AppSettings["urlMaster"] = SiloamConfig.Functions.GetValue("urlMaster").ToString();
                ConfigurationManager.AppSettings["urlIntegration"] = SiloamConfig.Functions.GetValue("urlIntegration").ToString();
                ConfigurationManager.AppSettings["urlHOPE"] = SiloamConfig.Functions.GetValue("urlHOPE").ToString();
                ConfigurationManager.AppSettings["urlHISDataCollection"] = SiloamConfig.Functions.GetValue("urlHISDataCollection").ToString();
                ConfigurationManager.AppSettings["urlFunctional"] = SiloamConfig.Functions.GetValue("urlFunctional").ToString();
                ConfigurationManager.AppSettings["urlExtension"] = SiloamConfig.Functions.GetValue("urlExtension").ToString();
                ConfigurationManager.AppSettings["urlEmailEngine"] = SiloamConfig.Functions.GetValue("urlEmailEngine").ToString();
                ConfigurationManager.AppSettings["DB_Emr"] = SiloamConfig.Functions.GetValue("DB_Emr").ToString();        
                ConfigurationManager.AppSettings["DB_HIS_External"] = SiloamConfig.Functions.GetValue("DB_HIS_External").ToString();
                ConfigurationManager.AppSettings["urlViewerResult"] = SiloamConfig.Functions.GetValue("urlViewerResult").ToString();
                ConfigurationManager.AppSettings["urlPharmacy"] = SiloamConfig.Functions.GetValue("urlPharmacy").ToString();
                ConfigurationManager.AppSettings["urlMims"] = SiloamConfig.Functions.GetValue("urlMims").ToString();

                ConfigurationManager.AppSettings["BaseURL_MySiloam_Doctor"] = SiloamConfig.Functions.GetValue("BaseURL_MySiloam_Doctor").ToString();
                ConfigurationManager.AppSettings["BaseURL_MySiloam_OPAdmin"] = SiloamConfig.Functions.GetValue("BaseURL_MySiloam_OPAdmin").ToString();
                ConfigurationManager.AppSettings["DB_Master"] = SiloamConfig.Functions.GetValue("DB_Master").ToString();
                ConfigurationManager.AppSettings["urlSiloamOT"] = SiloamConfig.Functions.GetValue("urlSiloamOT").ToString();
                ConfigurationManager.AppSettings["urlHealthRecordAPI"] = SiloamConfig.Functions.GetValue("urlHealthRecordAPI").ToString();

                ConfigurationManager.AppSettings["BaseURL_EMR_SOAP"] = SiloamConfig.Functions.GetValue("BaseURL_EMR_SOAP").ToString();
                ConfigurationManager.AppSettings["BaseURL_EMR_Viewer"] = SiloamConfig.Functions.GetValue("BaseURL_EMR_Viewer").ToString();
                ConfigurationManager.AppSettings["BaseURL_EMR_ViewerResult"] = SiloamConfig.Functions.GetValue("BaseURL_EMR_ViewerResult").ToString();
                ConfigurationManager.AppSettings["BaseURL_EMR_CovidVaccination"] = SiloamConfig.Functions.GetValue("BaseURL_EMR_CovidVaccination").ToString();
                ConfigurationManager.AppSettings["BaseURL_EMR_OTScheduling"] = SiloamConfig.Functions.GetValue("BaseURL_EMR_OTScheduling").ToString();
                ConfigurationManager.AppSettings["BaseURL_EMR_IPDDoctor"] = SiloamConfig.Functions.GetValue("BaseURL_EMR_IPDDoctor").ToString();
                ConfigurationManager.AppSettings["BaseURL_EMR_HopeMR"] = SiloamConfig.Functions.GetValue("BaseURL_EMR_HopeMR").ToString();
                ConfigurationManager.AppSettings["BaseURL_EMR_HealthRecord"] = SiloamConfig.Functions.GetValue("BaseURL_EMR_HealthRecord").ToString();
                //ConfigurationManager.AppSettings["BaseURL_EMR_Diabisa"] = SiloamConfig.Functions.GetValue("BaseURL_EMR_Diabisa").ToString();
                ConfigurationManager.AppSettings["urlIPDOT"] = SiloamConfig.Functions.GetValue("urlIPDOT").ToString();

                ///ConfigurationManager.AppSettings["BaseURL_EMR_Diabisa"] = SiloamConfig.Functions.GetValue("BaseURL_EMR_Diabisa").ToString();

                ConfigurationManager.AppSettings["UrlIPDOT"] = SiloamConfig.Functions.GetValue("UrlIPDOT").ToString();


            }

            if (Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"] == "clear")
                {
                    Session.Abandon();
                }

                Session[Helper.SessionLanguage] = null;
                Session[Helper.SessionListOrganization] = null;
                Session[Helper.SessionOrganization] = null;
                Session[Helper.SessionDoctor] = null;
                Session[Helper.SessionDrugsConsumables] = null;
                Session[Helper.SessionItemDrugPres] = null;
                Session[Helper.SessionDrugPres] = null;
                Session[Helper.SessionCompPres] = null;
                Session[Helper.SessionCompDetailPres] = null;      
            }

            //initialize marker
            //List<MarkerConfig> MARKERR = new List<MarkerConfig>();

            //MARKERR.Add(new MarkerConfig { key = "DOBmarker", value = "unmarked" });
            //MARKERR.Add(new MarkerConfig { key = "SAVESOAPmarker", value = "unmarked" });
            //MARKERR.Add(new MarkerConfig { key = "SUBMITSOAPmarker", value = "unmarked" });
            //MARKERR.Add(new MarkerConfig { key = "TAKENmarker", value = "unmarked" });
            //MARKERR.Add(new MarkerConfig { key = "SAVEORDERmarker", value = "unmarked" });

            //MARKERR.Add(new MarkerConfig { key = "HFflagsoapisdisable", value = "0" });
            //MARKERR.Add(new MarkerConfig { key = "HFflagdrugisdisable", value = "0" });
            //MARKERR.Add(new MarkerConfig { key = "HFflagadddrugisdisable", value = "0" });

            Session[Helper.SESSIONmarker] = Helper.setInitializeMarker();
            //Helper.DOBmarker = "unmarked";

            LabelVersion.Text = ConfigurationManager.AppSettings["AppVersion"].ToString();

            //Log.Info(LogConfig.LogEnd());
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "", "", "Page_Load", StartTime, EndTime, "OK", ""
                            , "", "",""));
        }
    }

    public string GetPathWorklist()
    {
        string pathh = "~/Form/General/WorklistNonAppointment.aspx";
        var WorklistType = clsCommon.GetSettingValue(Int64.Parse(MyUser.GetHopeOrgID()), "WORKLIST_TYPE").Result.ToString();
        Session[CstSession.SessionWorklistType] = WorklistType;

        if (WorklistType == "APPOINTMENT")
        {
            pathh = "~/Form/General/WorklistAppointment.aspx";          
        }
        else if (WorklistType == "NON_APPOINTMENT")
        {
            pathh = "~/Form/General/WorklistNonAppointment.aspx";
        }

        return pathh;
    }

    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        string Message = "";
        string Status = "";
        var finish = 0;

        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            //string clientipv6 = Dns.GetHostEntry(Dns.GetHostName()).AddressList.GetValue(0).ToString();
            
            //Log.Info(LogConfig.LogStart("- CLIENT-IP : " + GetUserIPv4()));

            btnSignIn.Enabled = false;
            string usernameLogin = txtUsername.Text.ToString().Replace('\\', '+');
            string passwordLogin = txtPassword.Text.ToString();

            //pengecekan pada username AD
            if (usernameLogin.Contains("+") == true)
            {
                var cekUserType = usernameLogin.Split('+');
                if (cekUserType[0].Length >= 12)
                {
                    passwordLogin = "12345678";
                }

                //login with AD manually
                try
                {
                    var UsernameData = usernameLogin.Split('+');
                    LdapConnection connection = new LdapConnection(UsernameData[0].ToString());
                    NetworkCredential credential = new NetworkCredential(UsernameData[1].ToString(), txtPassword.Text.ToString());
                    connection.Credential = credential;
                    connection.Bind();
                }
                catch (LdapException lexc)
                {
                    string error = lexc.ServerErrorMessage;
                    pError.InnerText = "Login AD Fail! " + error;
                    pError.Attributes.Remove("style");
                    pError.Attributes.Add("style", "display:block; color:red;");
                    goto FINISHH;
                }
                catch (Exception exc)
                {
                    pError.InnerText = "Login AD Fail! " + exc.ToString();
                    pError.Attributes.Remove("style");
                    pError.Attributes.Add("style", "display:block; color:red;");
                    goto FINISHH;
                }
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", usernameLogin, StartTime, EndTime, "OK", usernameLogin
            //        , usernameLogin, "", ""));
            //Log.Debug(LogConfig.LogStart("GetLogin", LogConfig.LogParam("username", usernameLogin)));
            List<Login> Login = new List<Login>();
            var GetLogin = clsLogin.GetLogin(usernameLogin, passwordLogin);
            var ListHospital = JsonConvert.DeserializeObject<ResultLogin>(GetLogin.Result.ToString());

            List<Login> HospitalList = new List<Login>();
            HospitalList = ListHospital.list;

            if (HospitalList.Count > 0)
            {
                Session[CstSession.SessionLoginTemp] = HospitalList;

                Doctor doc = new Doctor();
                doc.doctor_id = HospitalList[0].hope_user_id;
                doc.name = HospitalList[0].user_name;
                Session[Helper.SessionUserFullName] = HospitalList[0].full_name;
                Session[Helper.SessionDoctor] = doc;

                DataTable dt = Helper.ToDataTable(HospitalList);
                Session[Helper.SessionListOrganization] = dt;

                if (passwordLogin == "12345678")
                {
                    if (true)
                    {
                        LabelChangePassTitle.Text = "Silakan Ganti Password Default Anda.";
                        string localIP = Helper.GetLocalIPAddress();
                        //iframechangepass.Src = "http://" + localIP + "/viewer/Form/FormViewer/FormChangePassword?Username=" + usernameLogin;

                        string baseURLhttp = "http://" + localIP + "/viewer";
                        string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_Viewer"]; //"https://gtn-devws-01.siloamhospitals.com:2123"; //nanti akan ambil dari registry
                        iframechangepass.Src = baseURLhttps + "/Form/FormViewer/FormChangePassword?Username=" + usernameLogin;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalChangePass", "$('#modalChangePass').modal({backdrop: 'static', keyboard: false});", true);
                        goto FINISHH;
                    }
                }

                int flagexp = 0;
                var ResponseLogin = (JObject)JsonConvert.DeserializeObject<dynamic>(GetLogin.Result);
                string Pesan = ResponseLogin.Property("message").Value.ToString();
                if (Pesan.Contains("Password expired"))
                {
                    flagexp = 1;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertpassexpired", "alert('" + Pesan + "');", true);
                    Session["alertpassexpired"] = Pesan;
                }

                if (dt.Rows.Count > 1)
                {
                    dropdownOrg.DataSource = dt;
                    dropdownOrg.DataTextField = "organization_name";
                    dropdownOrg.DataValueField = "organization_id";
                    dropdownOrg.DataBind();
                    dropdownOrg.Items.Insert(0, new ListItem("Select Organization", "0"));

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalChooseOrg", "$('#modalChooseOrg').modal({backdrop: 'static', keyboard: false});", true);
                   
                }
                else
                {
                    if (Session[Helper.SessionDoctor] != null)
                    {
                        var orgsetting = clsCommon.GetOrganizationSettingbyOrgId(long.Parse(HospitalList[0].hope_organization_id.ToString()), long.Parse(HospitalList[0].hope_user_id.ToString()));
                        var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());
                        Session[Helper.SessionOrganizationSetting] = jsonorgsetting.list;
                    }
                    else
                    {
                        Message = "Login Error. Please Try Again!";
                        Status = "Fail";
                        pError.InnerText = Status + "! " + Message;
                        pError.Attributes.Remove("style");
                        pError.Attributes.Add("style", "display:block; color:red;");
                        txtUsername.BorderColor = Color.Red;
                        txtPassword.BorderColor = Color.Red;

                        //EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        //Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", usernameLogin, StartTime, EndTime, "Error", usernameLogin, usernameLogin, "", Message));
                        //Log.Info(LogConfig.LogEnd("GetLogin", Status, Message));
                    }

                    MyUser.SetLoginSession(ListHospital.list.FirstOrDefault());
                    if (flagexp == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect", "redirectlogin();", true);
                    }
                    else
                    {
                        Response.Redirect(GetPathWorklist(), false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }

                //Log.Info(LogConfig.LogEnd("GetLogin", Status, Message));
                EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", usernameLogin, "btnSignIn_Click", StartTime, EndTime, "OK", usernameLogin, usernameLogin, "", Message));
            }
            else
            {
                var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(GetLogin.Result);
                Status = Response.Property("status").Value.ToString();
                Message = Response.Property("message").Value.ToString();

                pError.InnerText = Status + "! " + Message;
                pError.Attributes.Remove("style");
                pError.Attributes.Add("style", "display:block; color:red;");
                txtUsername.BorderColor = Color.Red;
                txtPassword.BorderColor = Color.Red;

                if (Message.Contains("expired"))
                {
                    LabelChangePassTitle.Text = "Password Expired! Please update your password.";
                    string localIP = Helper.GetLocalIPAddress();
                    //iframechangepass.Src = "http://" + localIP + "/viewer/Form/FormViewer/FormChangePassword?Username=" + txtUsername.Text;

                    string baseURLhttp = "http://" + localIP + "/viewer";
                    string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_Viewer"]; //"https://gtn-devws-01.siloamhospitals.com:2123"; //nanti akan ambil dari registry
                    iframechangepass.Src = baseURLhttps + "/Form/FormViewer/FormChangePassword?Username=" + txtUsername.Text;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalChangePass", "$('#modalChangePass').modal({backdrop: 'static', keyboard: false}); toastr.warning('Password is Expired!', 'Warning');", true);
                }

                //Log.Info(LogConfig.LogEnd("GetLogin", Status, Message));
                EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", usernameLogin, "btnSignIn_Click", StartTime, EndTime, Status, usernameLogin, usernameLogin, "", Message));
            }

            btnSignIn.Enabled = true;
            
        }
        catch (Exception ex)
        {
            Message = "Login Error. Please Try Again!";
            Status = "Fail";
            pError.InnerText = Status + "! " + Message;
            pError.Attributes.Remove("style");
            pError.Attributes.Add("style", "display:block; color:red;");
            txtUsername.BorderColor = Color.Red;
            txtPassword.BorderColor = Color.Red;

            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", txtUsername.Text.ToString().Replace('\\', '+'), "btnSignIn_Click", StartTime, ErrorTime, "ERROR", txtUsername.Text.ToString().Replace('\\', '+'), txtUsername.Text.ToString().Replace('\\', '+'), "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            btnSignIn.Enabled = true;           
        }

    FINISHH:
        btnSignIn.Enabled = true;
        finish = 1;
    }

    //fungsi inisialisasi username AD secara otomatis saat checkbox dicentang
    protected void CheckBoxLoginAD_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBoxLoginAD.Checked)
        {
            txtUsername.Text = HttpContext.Current.User.Identity.Name.ToString();
            txtPassword.Enabled = false;
            txtUsername.Enabled = false;
        }
        else
        {
            txtUsername.Text = "";
            txtPassword.Enabled = true;
            txtUsername.Enabled = true;
        }
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        if (dropdownOrg.Text == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Select Organization First');", true);
        }
        else
        {
            //string clientipv6 = Dns.GetHostEntry(Dns.GetHostName()).AddressList.GetValue(0).ToString();
            // Log.Info(LogConfig.LogStart("- CLIENT-IP : " + GetUserIPv4()));
            string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            DataTable dt = (DataTable)Session[Helper.SessionListOrganization];
            DataTable dt_select = dt.Select("organization_id = " + dropdownOrg.SelectedValue).CopyToDataTable();
            Session[Helper.SessionListOrganization] = dt_select;

            if (Session[Helper.SessionDoctor] != null)
            {
                var orgsetting = clsCommon.GetOrganizationSettingbyOrgId(long.Parse(dt_select.Rows[0]["hope_organization_id"].ToString()), long.Parse(dt_select.Rows[0]["hope_user_id"].ToString()));
                var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());
                Session[Helper.SessionOrganizationSetting] = jsonorgsetting.list;
            }

            List<Login> HospitalList = (List<Login>)Session[CstSession.SessionLoginTemp];
            MyUser.SetLoginSession(HospitalList.Where(x => x.organization_id == long.Parse(dropdownOrg.SelectedValue)).FirstOrDefault());
            Response.Redirect(GetPathWorklist(), false);
            Context.ApplicationInstance.CompleteRequest();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalChooseOrg", "$('#modalChooseOrg').modal('hide');", true);
            //Log.Info(LogConfig.LogEnd());
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "CLIENT-IP", GetUserIPv4(), "btnContinue_Click", StartTime, EndTime, "OK", "", "", "", ""));
        }
    }

    private string GetUserIPv4()
    {
        string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipList))
        {
            return ipList.Split(',')[0];
        }

        return Request.ServerVariables["REMOTE_ADDR"];
    }

    protected void ButtonRedirect_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetPathWorklist(), false);
    }

    protected void btnSignInEncode_Click(object sender, EventArgs e)
    {
        var finish = 0;
        string Message = "";
        string Status = "";


        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            //string clientipv6 = Dns.GetHostEntry(Dns.GetHostName()).AddressList.GetValue(0).ToString();

            //Log.Info(LogConfig.LogStart("- CLIENT-IP : " + GetUserIPv4()));

            btnSignIn.Enabled = false;
            string usernameLogin = Base64Decode(hfTxtUsername.Value).ToString().Replace('\\', '+');
            string passwordLogin = Base64Decode(hfTxtPassword.Value).ToString();

            //pengecekan pada username AD
            if (usernameLogin.Contains("+") == true)
            {
                var cekUserType = usernameLogin.Split('+');
                if (cekUserType[0].Length >= 12)
                {
                    passwordLogin = "12345678";
                }

                //login with AD manually
                try
                {
                    var UsernameData = usernameLogin.Split('+');
                    LdapConnection connection = new LdapConnection(UsernameData[0].ToString());
                    NetworkCredential credential = new NetworkCredential(UsernameData[1].ToString(), txtPassword.Text.ToString());
                    connection.Credential = credential;
                    connection.Bind();
                }
                catch (LdapException lexc)
                {
                    string error = lexc.ServerErrorMessage;
                    pError.InnerText = "Login AD Fail! " + error;
                    pError.Attributes.Remove("style");
                    pError.Attributes.Add("style", "display:block; color:red;");
                    goto FINISHH;
                }
                catch (Exception exc)
                {
                    pError.InnerText = "Login AD Fail! " + exc.ToString();
                    pError.Attributes.Remove("style");
                    pError.Attributes.Add("style", "display:block; color:red;");
                    goto FINISHH;
                }
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", usernameLogin, StartTime, EndTime, "OK", usernameLogin
            //        , usernameLogin, "", ""));
            //Log.Debug(LogConfig.LogStart("GetLogin", LogConfig.LogParam("username", usernameLogin)));
            List<Login> Login = new List<Login>();
            var GetLogin = clsLogin.GetLogin(usernameLogin, passwordLogin);
            var ListHospital = JsonConvert.DeserializeObject<ResultLogin>(GetLogin.Result.ToString());

            List<Login> HospitalList = new List<Login>();
            HospitalList = ListHospital.list;

            if (HospitalList.Count > 0)
            {
                Session[CstSession.SessionLoginTemp] = HospitalList;

                Doctor doc = new Doctor();
                doc.doctor_id = HospitalList[0].hope_user_id;
                doc.name = HospitalList[0].user_name;
                Session[Helper.SessionUserFullName] = HospitalList[0].full_name;
                Session[Helper.SessionDoctor] = doc;

                DataTable dt = Helper.ToDataTable(HospitalList);
                Session[Helper.SessionListOrganization] = dt;

                if (passwordLogin == "12345678")
                {
                    if (true)
                    {
                        LabelChangePassTitle.Text = "Silakan Ganti Password Default Anda.";
                        string localIP = Helper.GetLocalIPAddress();
                        //iframechangepass.Src = "http://" + localIP + "/viewer/Form/FormViewer/FormChangePassword?Username=" + usernameLogin;

                        string baseURLhttp = "http://" + localIP + "/viewer";
                        string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_Viewer"]; //"https://gtn-devws-01.siloamhospitals.com:2123"; //nanti akan ambil dari registry
                        iframechangepass.Src = baseURLhttps + "/Form/FormViewer/FormChangePassword?Username=" + usernameLogin;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalChangePass", "$('#modalChangePass').modal({backdrop: 'static', keyboard: false});", true);
                        goto FINISHH;
                    }
                }

                int flagexp = 0;
                var ResponseLogin = (JObject)JsonConvert.DeserializeObject<dynamic>(GetLogin.Result);
                string Pesan = ResponseLogin.Property("message").Value.ToString();
                if (Pesan.Contains("Password expired"))
                {
                    flagexp = 1;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertpassexpired", "alert('" + Pesan + "');", true);
                    Session["alertpassexpired"] = Pesan;
                }

                if (dt.Rows.Count > 1)
                {
                    dropdownOrg.DataSource = dt;
                    dropdownOrg.DataTextField = "organization_name";
                    dropdownOrg.DataValueField = "organization_id";
                    dropdownOrg.DataBind();
                    dropdownOrg.Items.Insert(0, new ListItem("Select Organization", "0"));

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalChooseOrg", "$('#modalChooseOrg').modal({backdrop: 'static', keyboard: false});", true);

                }
                else
                {
                    if (Session[Helper.SessionDoctor] != null)
                    {
                        var orgsetting = clsCommon.GetOrganizationSettingbyOrgId(long.Parse(HospitalList[0].hope_organization_id.ToString()), long.Parse(HospitalList[0].hope_user_id.ToString()));
                        var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());
                        Session[Helper.SessionOrganizationSetting] = jsonorgsetting.list;
                    }
                    else
                    {
                        Message = "Login Error. Please Try Again!";
                        Status = "Fail";
                        pError.InnerText = Status + "! " + Message;
                        pError.Attributes.Remove("style");
                        pError.Attributes.Add("style", "display:block; color:red;");
                        txtUsername.BorderColor = Color.Red;
                        txtPassword.BorderColor = Color.Red;

                        //EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        //Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", usernameLogin, StartTime, EndTime, "Error", usernameLogin, usernameLogin, "", Message));
                        //Log.Info(LogConfig.LogEnd("GetLogin", Status, Message));
                    }

                    MyUser.SetLoginSession(ListHospital.list.FirstOrDefault());
                    if (flagexp == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect", "redirectlogin();", true);
                    }
                    else
                    {
                        Response.Redirect(GetPathWorklist(), false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }

                //Log.Info(LogConfig.LogEnd("GetLogin", Status, Message));
                EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", usernameLogin, "btnSignIn_Click", StartTime, EndTime, "OK", usernameLogin, usernameLogin, "", Message));
            }
            else
            {
                var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(GetLogin.Result);
                Status = Response.Property("status").Value.ToString();
                Message = Response.Property("message").Value.ToString();

                pError.InnerText = Status + "! " + Message;
                pError.Attributes.Remove("style");
                pError.Attributes.Add("style", "display:block; color:red;");
                txtUsername.BorderColor = Color.Red;
                txtPassword.BorderColor = Color.Red;

                if (Message.Contains("expired"))
                {
                    LabelChangePassTitle.Text = "Password Expired! Please update your password.";
                    string localIP = Helper.GetLocalIPAddress();
                    //iframechangepass.Src = "http://" + localIP + "/viewer/Form/FormViewer/FormChangePassword?Username=" + txtUsername.Text;

                    string baseURLhttp = "http://" + localIP + "/viewer";
                    string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_Viewer"]; //"https://gtn-devws-01.siloamhospitals.com:2123"; //nanti akan ambil dari registry
                    iframechangepass.Src = baseURLhttps + "/Form/FormViewer/FormChangePassword?Username=" + usernameLogin;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalChangePass", "$('#modalChangePass').modal({backdrop: 'static', keyboard: false}); toastr.warning('Password is Expired!', 'Warning');", true);
                }

                //Log.Info(LogConfig.LogEnd("GetLogin", Status, Message));
                EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", usernameLogin, "btnSignIn_Click", StartTime, EndTime, Status, usernameLogin, usernameLogin, "", Message));
            }

            btnSignIn.Enabled = true;

        }
        catch (Exception ex)
        {
            Message = "Login Error. Please Try Again!";
            Status = "Fail";
            pError.InnerText = Status + "! " + Message;
            pError.Attributes.Remove("style");
            pError.Attributes.Add("style", "display:block; color:red;");
            txtUsername.BorderColor = Color.Red;
            txtPassword.BorderColor = Color.Red;

            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", txtUsername.Text.ToString().Replace('\\', '+'), "btnSignIn_Click", StartTime, ErrorTime, "ERROR", txtUsername.Text.ToString().Replace('\\', '+'), txtUsername.Text.ToString().Replace('\\', '+'), "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            btnSignIn.Enabled = true;
        }

    FINISHH:
        btnSignIn.Enabled = true;
        finish = 1;
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    #region QR Code Login


    private static string GetUserIPv4V2()
    {
        string ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (!string.IsNullOrEmpty(ipList))
        {return ipList.Split(',')[0];}
        return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
    }

    // onclick - server side event on hidden button
    protected void btnSignInQR_Click(object sender, EventArgs e)
    {
        string Message = string.Empty, Status = string.Empty;
        int finish = 0;
        // start time process
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            // Get Hospital list from session
            List<Login> HospitalList = (List<Login>)Session["hospitalList"];
            if (HospitalList.Count > 0)
            {
                Session[CstSession.SessionLoginTemp] = HospitalList;

                Doctor doc = new Doctor();
                doc.doctor_id = HospitalList[0].hope_user_id;
                doc.name = HospitalList[0].user_name;
                Session[Helper.SessionUserFullName] = HospitalList[0].full_name;
                Session[Helper.SessionDoctor] = doc;

                DataTable dt = Helper.ToDataTable(HospitalList);
                Session[Helper.SessionListOrganization] = dt;

                if (dt.Rows.Count > 1)
                {
                    dropdownOrg.DataSource = dt;
                    dropdownOrg.DataTextField = "organization_name";
                    dropdownOrg.DataValueField = "organization_id";
                    dropdownOrg.DataBind();
                    dropdownOrg.Items.Insert(0, new ListItem("Select Organization", "0"));
                    // show modal
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalChooseOrg", "$('#modalChooseOrg').modal({backdrop: 'static', keyboard: false});", true);
                    // clear session
                    Session.Remove("hospitalList");
                }
                else
                {
                    if (Session[Helper.SessionDoctor] != null)
                    {
                        var orgsetting = clsCommon.GetOrganizationSettingbyOrgId(long.Parse(HospitalList[0].hope_organization_id.ToString()), long.Parse(HospitalList[0].hope_user_id.ToString()));
                        var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());
                        Session[Helper.SessionOrganizationSetting] = jsonorgsetting.list;
                        // clear session
                        Session.Remove("hospitalList");
                    }
                    else
                    {
                        Message = "Login Error. Please Try Again!";
                        Status = "Fail";
                        pError.InnerText = Status + "! " + Message;
                        pError.Attributes.Remove("style");
                        pError.Attributes.Add("style", "display:block; color:red;");
                        txtUsername.BorderColor = Color.Red;
                        txtPassword.BorderColor = Color.Red;
                    }

                    MyUser.SetLoginSession(HospitalList.FirstOrDefault());
                    Response.Redirect(GetPathWorklist(), false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", HospitalList[0].user_name, "btnSignInQR_Click", StartTime, EndTime, "OK", HospitalList[0].user_name, HospitalList[0].user_name, "", Message));
            }
            btnSignIn.Enabled = true;
        }
        catch (Exception ex)
        {
            Message = "Login Error. Please Try Again!";
            Status = "Fail";
            pError.InnerText = Status + "! " + Message;
            pError.Attributes.Remove("style");
            pError.Attributes.Add("style", "display:block; color:red;");
            txtUsername.BorderColor = Color.Red;
            txtPassword.BorderColor = Color.Red;

            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", txtUsername.Text.ToString().Replace('\\', '+'), "btnSignIn_Click", StartTime, ErrorTime, "ERROR", txtUsername.Text.ToString().Replace('\\', '+'), txtUsername.Text.ToString().Replace('\\', '+'), "", ex.Message));
            btnSignIn.Enabled = true;
        }
    FINISHH:
        btnSignIn.Enabled = true;
        finish = 1;
    }

    // Generate QR  
    [WebMethod]
    public static List<QRCodeLogin.QRCodeGenerate> GetQRCode()
    {
        try
        {
            // get ip name 
            string clientName = GetUserIPv4V2();
            // get client hostname
            //string clientIP = Dns.GetHostEntry(ip).HostName;

            // create QR Code
            string GenerateQr = clsLogin.GenerateQR(clientName);
            var dataGenerateQr = JsonConvert.DeserializeObject<List<QRCodeLogin.QRCodeGenerate>>(GenerateQr);
            // add qr code into session
            HttpContext.Current.Session["secret_key"] = dataGenerateQr[0].qr_guid;
            //return
            return dataGenerateQr;
        }
        catch (Exception exx)
        {
            //add error message and return error handler
            QRCodeLogin.QRCodeResponse res = new QRCodeLogin.QRCodeResponse
            {
                message_code = 0,
                message_text = exx.Message
            };
            List<QRCodeLogin.QRCodeResponse> resList = new List<QRCodeLogin.QRCodeResponse>();
            resList.Add(res);
            QRCodeLogin.QRCodeGenerate err = new QRCodeLogin.QRCodeGenerate();
            List<QRCodeLogin.QRCodeGenerate> errList = new List<QRCodeLogin.QRCodeGenerate>();
            err.QRCodeResponse = resList;
            errList.Add(err);
            // return error handler
            return errList;
        }
    }

    // check if in mobile is scan or not
    [WebMethod]
    public static QRCodeLogin.Result_check_login_qr CheckIsLogin()
    {
        // Get IP
        string clientName = GetUserIPv4V2();
        QRCodeLogin.Result_check_login_qr retVal = new QRCodeLogin.Result_check_login_qr();
        if (HttpContext.Current.Session["secret_key"] != null)
        {
            string secret_key = HttpContext.Current.Session["secret_key"].ToString();
            var resultAsync = clsLogin.CheckIsLogin(Guid.Parse(secret_key), clientName);
            var dataAsync = JsonConvert.DeserializeObject<QRCodeLogin.Result_check_login_qr>(resultAsync);

            if (dataAsync.status != "Fail")
            {
                // saving data to session
                HttpContext.Current.Session["hospitalList"] = dataAsync.list;
            }
            retVal = dataAsync;
        }
        return retVal;
    }

    // clear unique code when timeout 
    [WebMethod]
    public static void ClearSecretKey()
    {
        // Get IP
        string clientName = GetUserIPv4V2();
        if (HttpContext.Current.Session["secret_key"] != null)
        {
            string computerName = Environment.MachineName;
            string secretKey = HttpContext.Current.Session["secret_key"].ToString();
            var GetLogin = clsLogin.RemoveQrSecretId(Guid.Parse(secretKey), clientName);
            // remove specific session
            HttpContext.Current.Session.Remove("secret_key");
        }
    }



    
    #endregion
}