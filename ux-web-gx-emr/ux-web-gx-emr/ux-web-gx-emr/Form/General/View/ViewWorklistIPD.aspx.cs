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

public partial class Form_General_View_ViewWorklistIPD : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            HyperLink test = Master.FindControl("WorklistIPDLink") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");

            GetDataIframe();
            //Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], "", Request.QueryString["PageSoapId"], Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);

            
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "UserID", MyUser.GetHopeUserID(), "Page_Load", StartTime, EndTime, "OK", MyUser.GetUsername()
                            , "", "", ""));
            //Log.Info(LogConfig.LogEnd());
        }
    }

    void GetDataIframe()
    {
        string configIPAdress = ConfigurationManager.AppSettings["urlViewerResult"];
        //string localIPAdress = "http://" + Helper.GetLocalIPAddress();
        string patientid = Request.QueryString["idPatient"];

        //string url = "http://10.83.254.38" + "/ipd-doctor/Form/General/PatientList.aspx?organization_id=" + Helper.organizationId + "&doctor_id=" + Helper.GetDoctorID(this) + "&is_rmo=false";
        //string url = localIPAdress + "/ipd-doctor/Form/General/PatientList.aspx?organization_id=" + Helper.organizationId + "&doctor_id=" + Helper.GetDoctorID(this) + "&is_rmo=false";

        string diencript = Helper.GetDoctorID(this); //Helper.Encrypt(Helper.GetDoctorID(this));

        string baseURLhttp = configIPAdress + "/ipd-doctor";
        string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_IPDDoctor"]; //"https://gtn-devws-01.siloamhospitals.com:2127"; //nanti akan ambil dari registry
        string url = baseURLhttps + "/Form/General/PatientList.aspx?organization_id=" + Helper.organizationId + "&doctor_id=" + diencript + "&user_type=1";

        IframeIPD.Src = url;
    }
}