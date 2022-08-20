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

public partial class Form_General_View_ViewDiabisa : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        if (!IsPostBack)
        {
            HyperLink test = Master.FindControl("DiabisaLink") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");

            GetDataIframe();
            Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], "", Request.QueryString["PageSoapId"], Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "UserID", MyUser.GetHopeUserID(), "Page_Load", StartTime, EndTime, "OK", MyUser.GetUsername() , "", "", ""));
        }
    }

    void GetDataIframe()
    {
        string configIPAdress = ConfigurationManager.AppSettings["urlViewerResult"];
        string patientid = Request.QueryString["idPatient"];
        string organizationid = MyUser.GetHopeOrgID();
        string doctorid = MyUser.GetHopeUserID();
        string doctorname = MyUser.GetFullname();

        string baseURLhttp = configIPAdress + "/diabisa";
        string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_Diabisa"]; //"https://gtn-devws-01.siloamhospitals.com:2130"; //nanti akan ambil dari registry
        string url = baseURLhttps + "/Pages/FormViewer/BloodGlucose?PtnId=" + patientid + "&OrgId=" + organizationid + "&DoctorId=" + doctorid + "&DoctorName=" + doctorname;

        IframeViewDiabisa.Src = url;
    }
}