using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_SOAP_Template_StdSoapTeleconsultation : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            HyperLink test = Master.FindControl("SOAPLink") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");

            GetDataIframe();
            Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], "", Request.QueryString["PageSoapId"], Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "Page_Load", StartTime, "OK", MyUser.GetUsername()
                          , "", "", "")); 
            //Log.Info(LogConfig.LogEnd());
        }
    }

    void GetDataIframe()
    {
        
        //zoom link
        string zl = "null";

        var fl = Request.QueryString["DirectTele"];
        if (fl != null)
        {
            zl = Request.QueryString["ZoomLink"];
            if (zl != null && zl != "null")
            {
                Session[CstSession.SessionZoomLink] = zl;
            }
        }
        else
        {
            if (Session[CstSession.SessionZoomLink] != null)
            {
                zl = (string)Session[CstSession.SessionZoomLink];
            }
        }

        string configIPAdress = ConfigurationManager.AppSettings["BaseURL_EMR_SOAP"];
        string zlencript = zl == "null" ? zl : Helper.Encrypt(zl);

        //string url = "http://10.83.254.38/emr-soap/forms/pages/soapteleconsul.aspx?idPatient=27546&AdmissionId=2000006078874&EncounterId=44f9a8cc-9a2b-4429-9674-756e99fa8e8e&AppointmentId=94964b9a-5c07-4525-a6fd-227669db4022&idOrganization=2&idUser=baaefc24-8308-4b75-98dd-47a697f2afdc&idDoctor=2000000732";
        string url = configIPAdress + "/forms/pages/soapteleconsul.aspx?idPatient=" + Request.QueryString["idPatient"] + "&AdmissionId=" + Request.QueryString["AdmissionId"] + "&EncounterId=" + Request.QueryString["EncounterId"] + "&AppointmentId="+ Request.QueryString["AppointmentId"] + "&idOrganization=" + Helper.organizationId + "&idUser=" + Helper.GetUserID(this) + "&idDoctor=" + Helper.GetDoctorID(this) + "&userName=" + Helper.GetLoginUser(this) + "&orgName=" + Helper.GetOrgName(this) + "&hospitalId=" + Helper.GetHospitalID(this) + "&email=" + MyUser.GetEmail() + "&ZoomLink=" + zlencript;


        IframeTele.Src = url;
    }
}