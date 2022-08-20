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

public partial class Form_General_Result_ResultLABRAD : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            HyperLink test = Master.FindControl("ResultLinkLabRad") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");

            if (Request.QueryString["idPatient"] == null)
            {
                Response.Redirect("~/Form/General/Login.aspx", true);
                Context.ApplicationInstance.CompleteRequest();
            }
            if (Helper.GetLoginUser(this) == null)
            {
                Response.Redirect("~/Form/General/Login.aspx", true);
                Context.ApplicationInstance.CompleteRequest();
            }
            if (Helper.GetDoctorID(this) == "")
            {
                Response.Redirect("~/Form/General/Login.aspx", true);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {

                Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], "", Request.QueryString["PageSoapId"], Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
                hfPatientId.Value = Request.QueryString["idPatient"];
                hfEncounterId.Value = Request.QueryString["EncounterId"];
                hfAdmissionId.Value = Request.QueryString["AdmissionId"];
                hfPageSoapId.Value = Request.QueryString["PageSoapId"];
                hfAppointmentId.Value = Request.QueryString["AppointmentId"];
                hfIsTele.Value = Request.QueryString["IsTele"];

                //lblYear.Text = DateTime.Now.Year.ToString();
                //btn_next_ono.Enabled = false;
                //btn_next_ono.Style.Add("cursor", "not-allowed");

                getHeader();
                getdataIframe();
                //Session.Remove(Helper.ViewStateListLaboratory);
                //getSearchLabHistory(1, "10");
                //getRadResult(hfPatientId.Value.ToString(), DateTime.Now.Year);
                img_compare.PostBackUrl = String.Format("~/Form/General/Result/CompareLaboratory.aspx?idPatient={0}&EncounterId={1}&AdmissionId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}", hfPatientId.Value, hfEncounterId.Value, hfAdmissionId.Value, hfPageSoapId.Value, hfAppointmentId.Value, hfIsTele.Value);
                //MainView.ActiveViewIndex = 0;

                
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Page_Load", StartTime, EndTime, "OK", MyUser.GetUsername()
                          , "", "", ""));
            //Log.Info(LogConfig.LogStart());
        }
    }

    void getHeader()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Patient_ID", hfPatientId.Value },
                { "Encounter_ID", hfEncounterId.Value }
            };
            //Log.Debug(LogConfig.LogStart("GetPatientHeader", logParam));
            var varResult = clsCommon.GetPatientHeader(long.Parse(hfPatientId.Value), hfEncounterId.Value.ToString());
            var JsongetPatientHistory = JsonConvert.DeserializeObject<ResponsePatientHeader>(varResult.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("GetPatientHeader", JsongetPatientHistory.Status, JsongetPatientHistory.Message));

            PatientHeader header = JsongetPatientHistory.Data;
            PatientCard.initializevalue(header);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "getHeader", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "getHeader", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    void getdataIframe()
    {
        string localIPAdress = ConfigurationManager.AppSettings["urlViewerResult"];
        string baseURLhttp = localIPAdress + "/viewerresult";
        string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_ViewerResult"]; //"https://gtn-devws-01.siloamhospitals.com:2124"; //nanti akan ambil dari registry

        //string url = localIPAdress + "/viewerresult/Form/result?idPatient=" + hfPatientId.Value;
        string url = baseURLhttps + "/Form/result?idPatient=" + hfPatientId.Value + "&idOrganization=" + Helper.organizationId;
        myLabRadIframe.Src = url;
    }
}