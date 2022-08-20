using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static PatientHistory;

public partial class Form_General_View_ViewOTScheduling : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            HyperLink test = Master.FindControl("OTSchedulingLink") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");

            //if (Request.QueryString["idPatient"] == null)
            //{
            //    Response.Redirect("~/Form/General/Login.aspx", true);
            //    Context.ApplicationInstance.CompleteRequest();
            //}
            //if (Helper.GetLoginUser(this) == null)
            //{
            //    Response.Redirect("~/Form/General/Login.aspx", true);
            //    Context.ApplicationInstance.CompleteRequest();
            //}
            //if (Helper.GetDoctorID(this) == "")
            //{
            //    Response.Redirect("~/Form/General/Login.aspx", true);
            //    Context.ApplicationInstance.CompleteRequest();
            //}
            //else
            //{
            //    Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], "", Request.QueryString["PageSoapId"]);
            //    hfPatientId.Value = Request.QueryString["idPatient"];
            //    hfEncounterId.Value = Request.QueryString["EncounterId"];
            //    hfAdmissionId.Value = Request.QueryString["AdmissionId"];
            //    hfPageSoapId.Value = Request.QueryString["PageSoapId"];
            //}

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OTNotif", "RefreshCountNotifOT();", true);
            GetDataIframe();

            
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "UserID", MyUser.GetHopeUserID(), "Page_Load", StartTime, EndTime, "OK", MyUser.GetUsername()
                            , "", "", ""));
            //Log.Info(LogConfig.LogEnd());
        }
    }

    void GetDataIframe()
    {
        //PROD
        //API nya: 10.85.138.26:5700
        //UI nya: http://10.85.138.25/otscheduling/Form/View/Doctor.aspx?organization_id=2&user_id=2000000732&username=siloam.emr

        string configIPAdress = ConfigurationManager.AppSettings["urlViewerResult"];
        //string localIPAdress = "http://" + Helper.GetLocalIPAddress();

        string baseURLhttp = configIPAdress + "/otscheduling";
        string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_OTScheduling"]; //"https://gtn-devws-01.siloamhospitals.com:2126"; //nanti akan ambil dari registry
        string url = baseURLhttps + "/Form/View/Doctor.aspx?organization_id=" + Helper.organizationId + "&user_id=" + Helper.GetDoctorID(this) + "&username=" + Helper.GetLoginUser(this);
        //string url = configIPAdress + "/otscheduling/Form/View/Doctor.aspx?organization_id=" + Helper.organizationId + "&user_id=" + Helper.GetDoctorID(this) + "&username=" + Helper.GetLoginUser(this);
        //string url = "http://10.83.254.38/otscheduling/Form/View/Doctor.aspx?organization_id=2&user_id=2000000841&username=haryanto";

        IframeOTS.Src = url;
    }
}