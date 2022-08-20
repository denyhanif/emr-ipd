using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_General_PatientHistoryLite : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        
        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            hideHeader();
            if (Helper.GetUserID(this) == null)
            {
                Response.Redirect("~/Form/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                HyperLink test = Master.FindControl("PatientHistoryOutsideLink") as HyperLink;
                test.Style.Add("background-color", "#D6DBFF");
            }

            List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
            markerlist.Find(x => x.key == "DOBmarker").value = "marked";
            Session[Helper.SESSIONmarker] = markerlist;

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "UserID", MyUser.GetHopeUserID(), "Page_Load", StartTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            //Log.Info(LogConfig.LogEnd());
        }
    }

    protected void hideHeader()
    {
        lblNama.Visible = false;
        lblDOB.Visible = false;
        lblDOBJudul.Visible = false;
        lblAge.Visible = false;
        lblAgeJudul.Visible = false;
        lblReligion.Visible = false;
        lblReligionJudul.Visible = false;
        divLine.Visible = false;
    }

    protected void showHeader()
    {
        lblNama.Visible = true;
        lblDOB.Visible = true;
        lblDOBJudul.Visible = true;
        lblAge.Visible = true;
        lblAgeJudul.Visible = true;
        lblReligion.Visible = true;
        lblReligionJudul.Visible = true;
        divLine.Visible = true;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            hideHeader();
            string patientId = "";

            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "MR_no", txtMRno.Text },
                { "Organization_ID", Helper.organizationId.ToString() }
            };
            //Log.Debug(LogConfig.LogStart("getPatientData", logParam));
            var dataPatientId = clsPatientHistory.getPatientData(txtMRno.Text, Helper.organizationId);
            var patientIdOwned = JsonConvert.DeserializeObject<resultPatientData>(dataPatientId.Result.ToString());

            //Log.Debug(LogConfig.LogEnd("GetPatientHeader", patientIdOwned.Status, patientIdOwned.Message));

            lblNama.Text = patientIdOwned.list.PatientName;
            lblDOB.Text = patientIdOwned.list.BirthDate;
            lblAge.Text = patientIdOwned.list.Age;
            lblReligion.Text = patientIdOwned.list.ReligionName;
            if (patientIdOwned.list.SexId == 1)
            {
                ImageICMale.Visible = true;
                ImageICFemale.Visible = false;
            }
            else if (patientIdOwned.list.SexId == 2)
            {
                ImageICMale.Visible = false;
                ImageICFemale.Visible = true;
            }

            if (patientIdOwned.Status == "Fail")
            {
                divFrame.Visible = false;
                img_noData.Visible = true;
                no_patient_data.Visible = true;
                //searchSection.Attributes.Remove("style");
                //searchSection.Attributes.Add("style", "margin-top:-4% ; margin-right:-12%; position:absolute; width:100%; margin-bottom:10%");
            }

            else
            {
                showHeader();

                divFrame.Visible = true;
                //searchSection.Attributes.Remove("style");
                //searchSection.Attributes.Add("style", "margin-top:-7% ; margin-right:-12%; position:absolute; width:100%; margin-bottom:10%");
                patientId = patientIdOwned.list.PatientId.ToString();
                img_noData.Visible = false;
                no_patient_data.Visible = false;
                var localIPAdress = "";

                localIPAdress = GetLocalIPAddress();
                string baseURLhttp = "http://" + localIPAdress + "/viewer";
                string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_Viewer"]; //"https://gtn-devws-01.siloamhospitals.com:2123"; //nanti akan ambil dari registry

                //string url = "http://" + localIPAdress + "/viewer/form/PharmacyPatientHistory?OrganizationId=" + Helper.organizationId.ToString() + "&PatientId=" + patientId + "&PrintBy=" + Helper.GetUserFullname(this);
                //string url = "http://" + localIPAdress + "/viewer/Form/BigPatientHistory.aspx?idPatient=" + patientId + "&OrgId=" + Helper.organizationId.ToString() + "&PrintBy=" + Helper.GetUserFullname(this) + "&FlgCmpnd=true";
                string url = baseURLhttps + "/Form/BigPatientHistory.aspx?idPatient=" + patientId + "&OrgId=" + Helper.organizationId.ToString() + "&PrintBy=" + Helper.GetUserFullname(this) + "&FlgCmpnd=true";
                myIframe.Src = url;
            }
            updateBIG.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "MRno", txtMRno.Text.ToString(), "btnSearch_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "MRno", txtMRno.Text.ToString(), "btnSearch_Click", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            // Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}