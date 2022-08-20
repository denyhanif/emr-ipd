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

        if (!IsPostBack)
        {
            string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
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

    protected void btnChoose_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {

            if (HF_Form_Type.Value == "7ccd0a7e-9001-48ff-8052-ed07cf5716bb")
            {
                List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                markerlist.Find(x => x.key == "IsChooseTemplate").value = "true";
                Session[Helper.SESSIONmarker] = markerlist;
            }

            Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], Request.QueryString["pagefaid"], HF_Form_Type.Value, Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
            Helper.ResponseRedirectSOAP(Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], Request.QueryString["pagefaid"], HF_Form_Type.Value, Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);

            Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]] = null;

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnChoose_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnChoose_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    void GetDataIframe()
    {
        
        string configIPAdress = ConfigurationManager.AppSettings["BaseURL_EMR_SOAP"];

        //string url = "http://10.83.254.38/emr-soap/forms/pages/soapmain.aspx?idPatient=2000001998612&AdmissionId=2000006114946&EncounterId=2d569426-f25b-4029-9cc4-3879c047c96f&AppointmentId=00000000-0000-0000-0000-000000000000&idOrganization=2&idUser=baaefc24-8308-4b75-98dd-47a697f2afdc&idDoctor=2000000732&PageSoapId=7ccd0a7e-9001-48ff-8052-ed07cf5716bb&userName=siloam.emr";
        string url = configIPAdress + "/forms/pages/soapmain.aspx?idPatient=" + Request.QueryString["idPatient"] + "&AdmissionId=" + Request.QueryString["AdmissionId"] + "&EncounterId=" + Request.QueryString["EncounterId"] + "&PageSoapId="+ Request.QueryString["PageSoapId"] + "&AppointmentId=" + Request.QueryString["AppointmentId"] + "&idOrganization=" + Helper.organizationId + "&idUser=" + Helper.GetUserID(this) + "&idDoctor=" + Helper.GetDoctorID(this) + "&userName=" + Helper.GetLoginUser(this) + "&orgName=" + Helper.GetOrgName(this) + "&hospitalId=" + Helper.GetHospitalID(this) + "&IsTele=" + Request.QueryString["IsTele"];


        IframeTemplate.Src = url;
    }
}