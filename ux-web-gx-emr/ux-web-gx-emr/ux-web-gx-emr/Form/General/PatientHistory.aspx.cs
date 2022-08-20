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
using static PatientHistoryEncounter;
using static MedicalHistory;
using System.Data;
using System.Text;
using Microsoft.Ajax.Utilities;
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Drawing;

public partial class Form_General_PatientHistory : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public List<Dose> listdoseUom = new List<Dose>();
    public List<physicalExm> eye = new List<physicalExm>();
    public List<physicalExm> move = new List<physicalExm>();
    public List<physicalExm> verbal = new List<physicalExm>();
    public List<PatientAdmissionType> patientType = new List<PatientAdmissionType>();

    public string setENG = "none";
    public string setIND = "none";
    public string isBahasa = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        if (Session[Helper.SessionLanguage] == null)
            Session[Helper.SessionLanguage] = 1;

        if (Session[Helper.SessionLanguage].ToString() == "1")
        {
            setENG = "";
            setIND = "none";
            isBahasa = "ENG";
            HFisBahasa.Value = "ENG";
        }
        else if (Session[Helper.SessionLanguage].ToString() == "2")
        {
            setENG = "none";
            setIND = "";
            isBahasa = "IND";
            HFisBahasa.Value = "IND";
        }
        else
        {
            setENG = "";
            setIND = "none";
            isBahasa = "ENG";
            HFisBahasa.Value = "ENG";
        }

        //set bahasa
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "bahasa", "switchBahasa();", true);

        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            Session[Helper.SessionPreviousRowIndex] = null;

            string localIP = GetLocalIPAddress();
            //string localIP = "10.83.254.38"; //hardcode
            //emr_data.Src = "http://" + localIP + "/viewer/form/PharmacyPatientHistory?OrganizationId=" + Helper.organizationId.ToString() + "&PatientId=" + Request.QueryString["idPatient"] + "&PrintBy=" + Session[Helper.SessionUserFullName];

            string baseURLhttp = "http://" + localIP + "/viewer";
            string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_Viewer"]; //"https://gtn-devws-01.siloamhospitals.com:2123"; //nanti akan ambil dari registry

            //emr_data.Src = baseURLhttps + "/form/PharmacyPatientHistory?OrganizationId=" + Helper.organizationId.ToString() + "&PatientId=" + Request.QueryString["idPatient"] + "&PrintBy=" + Session[Helper.SessionUserFullName];
            emr_data.Src = baseURLhttps + "/form/formviewer/patienthistory/PH_Mr.aspx?OrganizationId=" + Helper.organizationId.ToString() + "&PatientId=" + Request.QueryString["idPatient"] + "&PrintBy=" + Session[Helper.SessionUserFullName] + "&DoctorId=" + Helper.GetDoctorID(this); ;

            if (Request.QueryString["EncounterId"] == null)
            {
                Response.Redirect("~/Form/General/Login.aspx", true);
                Context.ApplicationInstance.CompleteRequest();
            }
            if (Helper.GetLoginUser(this) == null)
            {
                Response.Redirect("~/Form/General/Login.aspx", true);
                Context.ApplicationInstance.CompleteRequest();
            }
            var test1 = Helper.GetDoctorID(this);
            if (Helper.GetDoctorID(this) == "")
            {
                Response.Redirect("~/Form/General/Login.aspx", true);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
            // ------------------------------------------------ Fill Master Data ------------------------------------------------- //

            /* ----------------------------------------- Date Search Emr --------------------------------------------------*/
            DateTextboxStart_emr.Text = DateTime.Now.AddMonths(-6).ToString("dd MMM yyyy");
            DateTextboxEnd_emr.Text = DateTime.Now.ToString("dd MMM yyyy");
            DateTextboxStart_emr.Attributes.Add("ReadOnly", "ReadOnly");
            DateTextboxEnd_emr.Attributes.Add("ReadOnly", "ReadOnly");
            /* ----------------------------------------- Date Search Emr --------------------------------------------------*/

            /* ----------------------------------------- Date Search Hope Emr --------------------------------------------------*/
            DateTextboxStart_hopeEmr.Text = DateTime.Now.AddMonths(-24).ToString("dd MMM yyyy");
            DateTextboxEnd_hopeEmr.Text = DateTime.Now.ToString("dd MMM yyyy");
            DateTextboxStart_hopeEmr.Attributes.Add("ReadOnly", "ReadOnly");
            DateTextboxEnd_hopeEmr.Attributes.Add("ReadOnly", "ReadOnly");
            /* ----------------------------------------- Date Search Hope Emr --------------------------------------------------*/

            /* ----------------------------------------- Date Search Other Unit Emr --------------------------------------------------*/
            DateTextboxStart_other.Text = DateTime.Now.AddMonths(-6).ToString("dd MMM yyyy");
            DateTextboxEnd_other.Text = DateTime.Now.ToString("dd MMM yyyy");
            DateTextboxStart_other.Attributes.Add("ReadOnly", "ReadOnly");
            DateTextboxEnd_other.Attributes.Add("ReadOnly", "ReadOnly");
            /* ----------------------------------------- Date Search Other Unit Emr --------------------------------------------------*/

            /* ----------------------------------------- Date Search Scanned MR --------------------------------------------------*/
            DateTextboxStart_scanned.Text = DateTime.Now.AddMonths(-6).ToString("dd MMM yyyy");
            DateTextboxEnd_scanned.Text = DateTime.Now.ToString("dd MMM yyyy");
            DateTextboxStart_scanned.Attributes.Add("ReadOnly", "ReadOnly");
            DateTextboxEnd_scanned.Attributes.Add("ReadOnly", "ReadOnly");
            /* ----------------------------------------- Date Search Scanned MR --------------------------------------------------*/
            
            HyperLink test = Master.FindControl("PatientHistoryLink") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");

            // ------------------------------------------------------------ Electronic MR --------------------------------------------------------------
            eye = new List<physicalExm>();
            Session.Remove(Helper.ViewStatePatientHistoryEye);
            eye.Add(new physicalExm { idph = 1, name = "None" });
            eye.Add(new physicalExm { idph = 2, name = "To Pressure" });
            eye.Add(new physicalExm { idph = 3, name = "To Sound" });
            eye.Add(new physicalExm { idph = 4, name = "Spontaneus" });
            Session[Helper.ViewStatePatientHistoryEye] = eye;

            move = new List<physicalExm>();
            Session.Remove(Helper.ViewStatePatientHistoryMove);
            move.Add(new physicalExm { idph = 1, name = "None" });
            move.Add(new physicalExm { idph = 2, name = "Extension" });
            move.Add(new physicalExm { idph = 3, name = "Flexion to pain stumulus" });
            move.Add(new physicalExm { idph = 4, name = "Withdrawns from pain" });
            move.Add(new physicalExm { idph = 5, name = "Localizes to pain stimulus" });
            move.Add(new physicalExm { idph = 6, name = "Obey Commands" });
            move.Add(new physicalExm { idph = 1, name = "None" });
            Session[Helper.ViewStatePatientHistoryMove] = move;

            verbal = new List<physicalExm>();
            Session.Remove(Helper.ViewStatePatientHistoryVerbal);
            verbal.Add(new physicalExm { idph = 1, name = "None" });
            verbal.Add(new physicalExm { idph = 2, name = "Incomprehensible sounds" });
            verbal.Add(new physicalExm { idph = 3, name = "Inappropriate words" });
            verbal.Add(new physicalExm { idph = 4, name = "Confused" });
            verbal.Add(new physicalExm { idph = 5, name = "Orientated" });
            Session[Helper.ViewStatePatientHistoryVerbal] = verbal;
            // ------------------------------------------------------------ Electronic MR --------------------------------------------------------------

            // --------------------------------------------------------------- Hope MR -----------------------------------------------------------------
            patientType = new List<PatientAdmissionType>();
            Session.Remove(Helper.ViewStatePatientHistoryPatientType);
            patientType.Add(new PatientAdmissionType { admissionTypeId = 1, admissionTypeName = "OPD" });
            patientType.Add(new PatientAdmissionType { admissionTypeId = 2, admissionTypeName = "IPD" });
            patientType.Add(new PatientAdmissionType { admissionTypeId = 3, admissionTypeName = "ED" });
            patientType.Add(new PatientAdmissionType { admissionTypeId = 4, admissionTypeName = "Checkup" });
            Session[Helper.ViewStatePatientHistoryPatientType] = patientType;
            // --------------------------------------------------------------- Hope MR -----------------------------------------------------------------

            // ------------------------------------------------ Fill Master Data ------------------------------------------------- //

            Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], "", Request.QueryString["PageSoapId"], Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
                hfPatientId.Value = Request.QueryString["idPatient"];
                hfEncounterId.Value = Request.QueryString["EncounterId"];
                hfAdmissionId.Value = Request.QueryString["AdmissionId"];
                hfPageSoapId.Value = Request.QueryString["PageSoapId"];

                getHeader();
                //getEncounter();

                /*Dose UOM */
                try
                {
                    //Log.Debug(LogConfig.LogStart("getDose"));
                    var doseUomData = clsOrderSet.getDose();
                    var JsondoseUom = JsonConvert.DeserializeObject<ResultDose>(doseUomData.Result.ToString());

                    //string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "Page_Load", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", doseUomData.Result.ToString()));
                    //Log.Debug(LogConfig.LogEnd("getDose", JsondoseUom.Status, JsondoseUom.Message)); 

                    listdoseUom = JsondoseUom.list;
                    Session[Helper.ViewStatePatientHistoryDoseUOM] = listdoseUom;
                }
                catch (Exception ex)
                {
                    string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "Page_Load", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                    //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
                }

                // load all data
                try
                {
                    Dictionary<string, string> logParam = new Dictionary<string, string>
                    {
                        { "Organization_ID", Helper.organizationId.ToString() },
                        { "Name", "HOPE_MR" }
                    };
                    //Log.Debug(LogConfig.LogStart("getStatusMR", logParam));
                    var organizationSettingData = clsPatientHistory.getStatusMR(Helper.organizationId, "HOPE_MR");
                    var OrganizationSetting = JsonConvert.DeserializeObject<ResultOrganizationSetting>(organizationSettingData.Result.ToString());
                    //string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "Page_Load", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", organizationSettingData.Result.ToString()));
                    //Log.Debug(LogConfig.LogEnd("getStatusMR", OrganizationSetting.Status, OrganizationSetting.Message));

                    if (OrganizationSetting.list.setting_value == "TRUE")
                    {
                        //getHopeEmrData();
                    }
                    else
                    {
                        btn_hope_emr.Style.Add("display", "none");
                    }
                }
                catch (Exception ex)
                {
                    string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "Page_Load", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                    //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
                }

                try
                {

                    Dictionary<string, string> logParam = new Dictionary<string, string>
                    {
                        { "Organization_ID", Helper.organizationId.ToString() },
                        { "Name", "SCANNED_MR" }
                    };
                    //Log.Debug(LogConfig.LogStart("getStatusMR", logParam));
                    var organizationSettingData = clsPatientHistory.getStatusMR(Helper.organizationId, "SCANNED_MR");
                    var OrganizationSetting = JsonConvert.DeserializeObject<ResultOrganizationSetting>(organizationSettingData.Result.ToString());
                    //string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "Page_Load", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", organizationSettingData.Result.ToString()));
                    //Log.Debug(LogConfig.LogEnd("getStatusMR", OrganizationSetting.Status, OrganizationSetting.Message));

                    if (OrganizationSetting.list.setting_value == "TRUE")
                    {
                        //getScannedData();
                    }
                    else
                    {
                        btn_scanned_emr.Style.Add("display", "none");
                    }

                }
                catch (Exception ex)
                {
                    string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "Page_Load", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                    //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
                }

                try
                {
                    Dictionary<string, string> logParam = new Dictionary<string, string>
                    {
                        { "Organization_ID", Helper.organizationId.ToString() },
                        { "Name", "OTHERUNIT_MR" }
                    };
                    //Log.Debug(LogConfig.LogStart("getStatusMR", logParam));
                    var organizationSettingData = clsPatientHistory.getStatusMR(Helper.organizationId, "OTHERUNIT_MR");
                    var OrganizationSetting = JsonConvert.DeserializeObject<ResultOrganizationSetting>(organizationSettingData.Result.ToString());
                    //string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "Page_Load", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", organizationSettingData.Result.ToString()));
                    // Log.Debug(LogConfig.LogEnd("getStatusMR", OrganizationSetting.Status, OrganizationSetting.Message));

                    if (OrganizationSetting.list.setting_value == "TRUE")
                    {
                        //getOtherUnitData(2);
                    }
                    else
                    {
                        btn_other_emr.Style.Add("display","none");
                        img_noData_other_mr.Style.Add("display", "");
                    }

                }
                catch (Exception ex)
                {
                    string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "Page_Load", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                    //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
                }
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "Page_Load", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

            //Log.Info(LogConfig.LogEnd());
        }
    }

    void getHopeEmrData()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            //Log.Info(LogConfig.LogStart());

            GridView gvw_test = new GridView();
            StringBuilder innerHtmlHopeEmr = new StringBuilder();
            List<PatientHistoryHOPEemr> dataHopeEMR = new List<PatientHistoryHOPEemr>();

            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Organization_ID", Helper.organizationId.ToString() },
                { "Patient_ID", hfPatientId.Value },
                { "Date_Start", DateTextboxStart_hopeEmr.Text },
                { "Date_End", DateTextboxEnd_hopeEmr.Text }
            };
           // Log.Debug(LogConfig.LogStart("getHOPEemrData", logParam));
            var varResult = clsPatientHistory.getHOPEemrData(Helper.organizationId, hfPatientId.Value, DateTime.Parse(DateTextboxStart_hopeEmr.Text), DateTime.Parse(DateTextboxEnd_hopeEmr.Text));
            var JsongetPatientHistoryHOPEemr = JsonConvert.DeserializeObject<ResultPatientHistoryHOPEemr>(varResult.Result.ToString());

            //Log.Debug(LogConfig.LogEnd("getHOPEemrData", JsongetPatientHistoryHOPEemr.Status, JsongetPatientHistoryHOPEemr.Message));

            if (JsongetPatientHistoryHOPEemr.list.Count != 0)
            {
                dataHopeEMR = JsongetPatientHistoryHOPEemr.list;
                Session[Helper.ViewStatePatientHistoryHOPEemr] = dataHopeEMR;
                //innerHtmlHopeEmr = 
                    loadDataHopeEmr(dataHopeEMR);
                status_hopeEmr.Value = "Not empty";
            }
            else
            {
                gvHopeMRList.DataSource = null;
                gvHopeMRList.DataBind();
                innerHtmlHopeEmr.Append("");
                status_hopeEmr.Value = "empty";
            }
            //hope__emr.InnerHtml = innerHtmlHopeEmr.ToString();

            //Log.Info(LogConfig.LogEnd());
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getHopeEmrData", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getHopeEmrData", StartTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ""));
            //LogLibrary.Error("getHopeEmrData", hfPatientId.Value.ToString() + " " + hfEncounterId.Value, ex.Message.ToString());
        }
    }

    void getHeader()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
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

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getHeader", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", varResult.Result.ToString()));
            //Log.Debug(LogConfig.LogEnd("GetPatientHeader", JsongetPatientHistory.Status, JsongetPatientHistory.Message));

            PatientHeader header = JsongetPatientHistory.Data;
            PatientCard.initializevalue(header);
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getHeader", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
    }

    void getEncounter()
    {
        Session.Remove(Helper.ViewStateEncounterData);
        Session.Remove(Helper.ViewStatePageData);
        Session.Remove(Helper.ViewStatePatientHistoryInner);

        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            //Log.Info(LogConfig.LogStart());
            GridView gvw_test = new GridView();

            //Log.Debug(LogConfig.LogStart("getEncounterPatientHistory", LogConfig.LogParam("Patient_ID", hfPatientId.Value)));
            var varResult = clsPatientHistory.getEncounterPatientHistory(hfPatientId.Value);
            var JsongetPatientHistoryEncounter = JsonConvert.DeserializeObject<ResultPatientHistoryEncounter>(varResult.Result.ToString());

            //Log.Debug(LogConfig.LogEnd("getEncounterPatientHistory", JsongetPatientHistoryEncounter.Status, JsongetPatientHistoryEncounter.Message));

            List<PatientHistoryEncounter> encounterData = new List<PatientHistoryEncounter>();
            List<PatientHistoryEncounter> encounterAllData = new List<PatientHistoryEncounter>();
            encounterAllData = JsongetPatientHistoryEncounter.list.FindAll(x => x.organizationId.Equals(Helper.organizationId));
            Session[Helper.ViewStateEncounterData] = (List<PatientHistoryEncounter>)encounterAllData.ToList();

            if (DateTextboxStart_emr.Text != "")
            {
                var tempData = encounterAllData.FindAll(x => DateTime.Parse(x.admissionDate.ToString("dd MMM yyyy")) >= DateTime.Parse(DateTime.Now.AddMonths(-3).ToString("dd MMM yyyy")));
                var temp = tempData;
                if (DateTextboxEnd_emr.Text != "")
                {
                    tempData = temp.FindAll(x => DateTime.Parse(x.admissionDate.ToString("dd MMM yyyy")) <= DateTime.Parse(DateTextboxEnd_emr.Text.ToString()));
                }

                encounterData = tempData.ToList();
            }

            if (encounterAllData.Count == 1)
            {
                btn_load_more.Style.Add("display", "none");
            }

            if (encounterData.Count != 0)
            {
                if (encounterData.Count > 1)
                {
                    status_dataEmr.Value = "LOAD MORE";
                }
                Session[Helper.ViewStatePageData] = encounterData;
                Session.Remove(Helper.ViewStatePatientHistoryInner);
                StringBuilder patientHistory = loadDataPatientHistory(encounterData[0].organizationId, encounterData[0].patientId, encounterData[0].admissionId, encounterData[0].encounterId.ToString());

                Session[Helper.ViewStatePatientHistoryInner] = patientHistory;
                tblPatientHistory.InnerHtml = patientHistory.ToString();

                DataTable dt = Helper.ToDataTable(encounterAllData);
                gvw_test.DataSource = dt;
                gvw_test.DataBind();
                img_noData_emr.Style.Add("display", "none");
            }
            else
            {
                status_dataEmr.Value = "";
                btn_load_more.Style.Add("display", "none");
                img_noData_emr.Style.Add("display", "");
                status_dataEmr.Value = "empty";
            }

            //Log.Info(LogConfig.LogEnd());
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getEncounter", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getEncounter", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
    }

    void getOtherUnitData(int year)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); 
        //Log.Info(LogConfig.LogStart());
        Session.Remove(Helper.ViewStateOtherUnitMR);
        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Patient_ID", hfPatientId.Value.ToString() },
                { "Organization_ID", Helper.organizationId.ToString() },
                { "Year", year.ToString() }
            };
            //Log.Debug(LogConfig.LogStart("getOtherUnitData", logParam));
            var varResult = clsPatientHistory.getOtherUnitData(Int64.Parse(hfPatientId.Value.ToString()), Helper.organizationId, year);
            var JsongetOtherUnitData = JsonConvert.DeserializeObject<ResultOtherUnitMR>(varResult.Result.ToString());

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getOtherUnitData", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", varResult.Result.ToString()));
            //Log.Debug(LogConfig.LogEnd("getOtherUnitData", JsongetOtherUnitData.Status, JsongetOtherUnitData.Message));

            Session[Helper.ViewStateOtherUnitMR] = JsongetOtherUnitData.list;
            
            if (JsongetOtherUnitData != null)
            {
                List<OtherUnitMR> OtherUnitData = new List<OtherUnitMR>();
                if (DateTextboxStart_other.Text != "" || DateTextboxEnd_other.Text != "")
                {
                    var tempOtherUnitData = new List<OtherUnitMR>();

                    tempOtherUnitData = JsongetOtherUnitData.list.FindAll(x => x.AdmissionDate >= DateTime.Parse(DateTextboxStart_other.Text.ToString()));
                    OtherUnitData = tempOtherUnitData;

                    if (DateTextboxEnd_other.Text != "")
                    {
                        OtherUnitData = tempOtherUnitData.FindAll(x => x.AdmissionDate <= DateTime.Parse(DateTextboxEnd_other.Text.ToString()).AddDays(1));
                    }
                }
                else
                {
                    OtherUnitData = JsongetOtherUnitData.list;
                }
                
                StringBuilder otherUnitInnerHTML = new StringBuilder();
                if (OtherUnitData.Count != 0)
                {
                    List<string> unit_filter = new List<string>();
                    unit_filter.AddRange(OtherUnitData.Select(x => x.OrganizationCode).Distinct());
                    //var firstitem = ddl_unit_other_mr.Items[0];
                    ddl_unit_other_mr.Items.Clear();
                    ddl_unit_other_mr.DataSource = unit_filter;
                    ddl_unit_other_mr.DataBind();
                    //ddl_unit_other_mr.Items.Add(firstitem);

                    otherUnitInnerHTML = loadOtherUnitEMR(OtherUnitData);
                    other_unit_emr_data.InnerHtml = otherUnitInnerHTML.ToString();
                    status_other_unit_Emr.Value = "Not empty";
                }
                else
                {
                    status_other_unit_Emr.Value = "empty";
                }
            }
            else
            {
                status_other_unit_Emr.Value = "empty";
            }      
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getOtherUnitData", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    StringBuilder loadOtherUnitEMR(List<OtherUnitMR> OtherUnitData)
    {   
        HiddenField userFUllName = Master.FindControl("hfUserFullName") as HiddenField;

        StringBuilder otherUnitInnerHTML = new StringBuilder();
        string imagePath = ResolveClientUrl("~/Images/PatientHistory/ic_newtab_blue.svg");

        otherUnitInnerHTML.Append("<div style=\"border-radius: 7px; border: solid 2px lightgrey; margin:20px;\">");
        OtherUnitData = OtherUnitData.OrderByDescending(x => x.AdmissionDate).ToList();
        foreach (OtherUnitMR data in OtherUnitData)
        {
            otherUnitInnerHTML.Append("<div style=\"border-top: 1px solid #ddd;\">" +
                                    "<div style=\"margin-top: 10px;margin-bottom: 5px;\" class=\"container-fluid\"><a target=\"_blank\" href=\'http://" + data.LinkURL + "&PrintBy="+ Helper.GetLoginUser(this) + "\'  style=\"color: blue; text-decoration:underline; \"><span><img src=\"" + imagePath + "\" /></span>  <b>" + data.AdmissionDate.ToString("dd MMM yyyy") + "</b></a></div>" +
                                    "<div style=\"margin-bottom: 10px;color: darkgrey;\" class=\"container-fluid\">" + data.AdmissionTypeCode + " " + data.AdmissionNo + " " + data.DoctorName + "</div>" +
                                    "</div>");
        }
        otherUnitInnerHTML.Append("</div>");

        return otherUnitInnerHTML;
    }

    public static string GetLocalIPAddress()
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

    StringBuilder loadDataHopeEmr(List<PatientHistoryHOPEemr> data)
    {
        patientType = (List<PatientAdmissionType>) Session[Helper.ViewStatePatientHistoryPatientType];
        StringBuilder hopeInnerHtml = new StringBuilder();
        List<PatientHistoryHOPEemr> hopeEmrData = new List<PatientHistoryHOPEemr>();
        List<long> admissionId = data.Select(x => x.admissionId).Distinct().ToList();
        int i = 0;
        var localIPAdress = GetLocalIPAddress();
        string imagePath = ResolveClientUrl("~/Images/PatientHistory/ic_newtab_blue.svg");

        //hopeInnerHtml.Append("<div style=\"border-radius: 7px; border: solid 2px lightgrey; margin:20px;\">");

        hopeEmrData = new List<PatientHistoryHOPEemr>();
        for (i = 0; i < admissionId.Count; i++)
        {
            hopeEmrData.Add(data.FindAll(x => x.admissionId.Equals(admissionId[i])).First());

            var doctorAdmission = "";

            foreach (PatientHistoryHOPEemr datahope in hopeEmrData)
            {
                doctorAdmission = doctorAdmission + " " + datahope.entryUser;
            }          

            //localIPAdress = "10.83.254.38";
            var tempLink = "//" + localIPAdress + "/viewermrhope/Form/EMRHope/PatientHistoryDetail.aspx?orgid=" + Helper.organizationId + "&admid=" + admissionId[i];

            //tempLinkIframe.Value = tempLink.ToString();

            hopeEmrData[i].linkMRHOPE = tempLink.ToString();
            hopeEmrData[i].admTypePlusAdmId = patientType.Find(x => x.admissionTypeId == hopeEmrData[0].admissionTypeId).admissionTypeName + admissionId[i];

            //hopeInnerHtml.Append("<div style=\"border-top: 1px solid #ddd;\">" +
            //                    "<div style=\"margin-top: 10px;margin-bottom: 5px;\" class=\"container-fluid\"><a onclick=\'" + "klikHopeEmr(); return false;" + "\' href=\'" + "#" + "\'  style=\"color: blue; text-decoration:underline; \"><span><img src=\""+ imagePath + "\" /></span>  <b>" + hopeEmrData[0].admissionDate.ToString("dd MMM yyyy") + "</b></a></div>" +
            //                    "<div style=\"margin-bottom: 10px;color: darkgrey;\" class=\"container-fluid\">" + patientType.Find(x => x.admissionTypeId == hopeEmrData[0].admissionTypeId).admissionTypeName + " " + admissionId[i] + " " + doctorAdmission + "</div>" +
            //                    "</div>");
        }

        gvHopeMRList.DataSource = null;
        gvHopeMRList.DataSource = Helper.ToDataTable(hopeEmrData.ToList());
        gvHopeMRList.DataBind();
        //hopeInnerHtml.Append("</div>");

        return hopeInnerHtml;
    }

    StringBuilder loadDataPatientHistory(Int64 OrganizationId, Int64 PatientId, Int64 AdmissionId, String EncounterId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); 
        //Log.Info(LogConfig.LogStart());

        /*-------------------------------------------------- Get Laboratory -----------------------------------------------*/
        try
        {
            List<LaboratoryResult> listlaboratory = new List<LaboratoryResult>();
            //Log.Debug(LogConfig.LogStart("getLaboratoryResult", LogConfig.LogParam("Admission_ID", hfAdmissionId.Value)));
            var dataLaboratory = clsResult.getLaboratoryResult(hfAdmissionId.Value);
            var JsonLaboratory = JsonConvert.DeserializeObject<ResponseLaboratoryResult>(dataLaboratory.Result.ToString());

            //string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", PatientId.ToString(), "loadDataPatientHistory", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataLaboratory.Result.ToString()));
            //Log.Debug(LogConfig.LogEnd("getLaboratoryResult", JsonLaboratory.Status, JsonLaboratory.Message));

            if (JsonLaboratory.Data.Count != 0)
            {
                listlaboratory = new List<LaboratoryResult>();
                listlaboratory = JsonLaboratory.Data;

                StdLabResult.initializevalue(listlaboratory);
            }
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", PatientId.ToString(), "loadDataPatientHistory", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            return new StringBuilder();
        }

        try
        {
            GridView gvw_test = new GridView();

            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Organization_ID", OrganizationId.ToString() },
                { "Patient_ID", PatientId.ToString() },
                { "Admission_ID", AdmissionId.ToString() },
                { "Encounter_ID", EncounterId }
            };
            //Log.Debug(LogConfig.LogStart("getPatientHistoryData", logParam));
            var varResult = clsPatientHistory.getPatientHistoryData(OrganizationId, PatientId, AdmissionId, EncounterId);
            var JsongetPatientHistoryData = JsonConvert.DeserializeObject<ResultPatientHistoryEncounterData>(varResult.Result.ToString());

            //Log.Debug(LogConfig.LogEnd("getPatientHistoryData", JsongetPatientHistoryData.Status, JsongetPatientHistoryData.Message));

            StringBuilder patientHistory = new StringBuilder();
            eye = (List<physicalExm>) Session[Helper.ViewStatePatientHistoryEye];
            move = (List<physicalExm>)Session[Helper.ViewStatePatientHistoryMove];
            verbal = (List<physicalExm>) Session[Helper.ViewStatePatientHistoryVerbal];

            # region Data Patien History
            if (JsongetPatientHistoryData != null)
            {
                ResultPatientHistoryEncounterData patientHistoryData = JsongetPatientHistoryData;

                // ------------------------------------ Illness History ---------------------------------
                PatientHistoryHeader headerPatient = patientHistoryData.list.historyheader;
                // ------------------------------------ Illness History ---------------------------------
                List<PatientHistoryIllness> illnessHistory = patientHistoryData.list.historyillness;
                illnessHistory.OrderBy(x => x.type);
                List<String> illCategory = illnessHistory.DistinctBy(x => x.type).Select(x => x.type).ToList();

                // ------------------------------------ history physical exam ---------------------------------
                List<PatientHistoryPhysicalExam> physicalExams = patientHistoryData.list.historyphysicalexam;

                // ------------------------------------ history diagnosis ---------------------------------
                List<PatientHistoryDiagnosis> historyDiagnoses = patientHistoryData.list.historydiagnosis;

                // ------------------------------------ history planning ---------------------------------
                List<PatientHistoryPlanning> historyPlanning = patientHistoryData.list.historyplanning;

                // ------------------------------------ history prescription ---------------------------------
                List<PatientHistoryPrescription> historyPrescriptions = patientHistoryData.list.historyprescription;
                List<PatientHistoryPrescription> tmpDrugs = (from a in historyPrescriptions
                                                        where a.compoundName == "" &&
                                                        a.isConsumables == false &&
                                                        a.IsDoctor == 1 &&
                                                        a.IsAdditional == 0
                                                        orderby a.salesItemName
                                                        select a).ToList();

                List<PatientHistoryPrescription> tmpConsumableDoctor = (from a in historyPrescriptions
                                                                        where a.compoundName == "" &&
                                                                        a.isConsumables == true &&
                                                                        a.IsDoctor == 1 &&
                                                                        a.IsAdditional == 0
                                                                        orderby a.salesItemName
                                                                        select a).ToList(); 

                List<PatientHistoryPrescription> tmpCompound = historyPrescriptions.FindAll(x => x.compoundName != "").OrderBy(x => x.salesItemName).ToList();
                List<String> compoundName = tmpCompound.DistinctBy(x => x.compoundName).Select(x => x.compoundName).ToList();
                List<PatientHistoryPrescription> tmpAdditionalDoctor = (from a in historyPrescriptions
                                                                        where a.compoundName == "" &&
                                                                        a.isConsumables == false &&
                                                                        a.IsDoctor == 1 &&
                                                                        a.IsAdditional == 1
                                                                        orderby a.salesItemName
                                                                        select a).ToList();

                List<PatientHistoryPrescription> tmpPrescriptionPharmacist = (from a in historyPrescriptions
                                                                              where a.compoundName == "" &&
                                                                              a.isConsumables == false &&
                                                                              a.IsDoctor == 0 &&
                                                                              a.IsAdditional == 0
                                                                              orderby a.salesItemName
                                                                              select a).ToList();

                List<PatientHistoryPrescription> tmpConsumablePharmacist = (from a in historyPrescriptions
                                                                            where a.compoundName == "" &&
                                                                            a.isConsumables == true &&
                                                                            a.IsDoctor == 0 &&
                                                                            a.IsAdditional == 0
                                                                            orderby a.salesItemName
                                                                            select a).ToList();

                List<PatientHistoryPrescription> tmpAdditionalPharmacist = (from a in historyPrescriptions
                                                                            where a.compoundName == "" &&
                                                                            a.isConsumables == false &&
                                                                            a.IsDoctor == 0 &&
                                                                            a.IsAdditional == 1
                                                                            orderby a.salesItemName
                                                                            select a).ToList();

                //-------------------------------- history clinical --------------------------------------------
                string linkLaboratory = "javascript:modalLaboratory('" + AdmissionId + "')";
                string linkRadiology = "javascript:modalRadiology('" + AdmissionId + "')";

                List<PatientHistoryClinicalFinding> patientHistoryClinicals = patientHistoryData.list.historyclinical;
                var laboratoryData = "";
                if (patientHistoryClinicals[0].countData != 0)
                {
                    laboratoryData = "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top\"><a href=\"" + linkLaboratory + "\"><img src=\"../../Images/Result/ic_Lab.png\" />  <label style=\"color: blue; text-decoration:underline; \"><b>Laboratory Result</b></label></a></div>";
                }
                else
                {
                    laboratoryData = "";
                }

                var radiologyData = "";
                if (patientHistoryClinicals[1].countData != 0)
                {
                    radiologyData = "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top\"><a href=\"" + linkRadiology + "\"><img src=\"../../Images/Result/ic_Rad.png\" />  <label  style=\"color: orange; text-decoration:underline; \"><b>Radiology Result</b></label></a></div>";
                }
                else
                {
                    radiologyData = "";
                }

                var statusPregnancy = "";
                if (historyDiagnoses.Find(x => x.mappingName.Equals("PREGNANCY")).value == "True")
                {
                    if (isBahasa == "ENG")
                    {
                        statusPregnancy = "Yes";
                    }
                    else if (isBahasa == "IND")
                    {
                        statusPregnancy = "Ya";
                    }
                }
                else
                {
                    if (isBahasa == "ENG")
                    {
                        statusPregnancy = "No";
                    }
                    else if (isBahasa == "IND")
                    {
                        statusPregnancy = "Tidak";
                    }
                }

                var statusBreastFeeding = "";
                if (historyDiagnoses.Find(x => x.mappingName.Equals("BREASTFEEDING")).value == "True")
                {
                    if (isBahasa == "ENG")
                    {
                        statusBreastFeeding = "Yes";
                    }
                    else if (isBahasa == "IND")
                    {
                        statusBreastFeeding = "Ya";
                    }
                }
                else
                {
                    if (isBahasa == "ENG")
                    {
                        statusBreastFeeding = "No";
                    }
                    else if (isBahasa == "IND")
                    {
                        statusBreastFeeding = "Tidak";
                    }
                }

                //var imageResume = "/Images/PatientHistory/ic_Resume.png";

                string linkResume = "javascript:Preview(" + AdmissionId + ", /" + EncounterId + "/ );";

                patientHistory.Append("<div style=\"background-color:white; margin:20px; border-radius:7px; border: solid 2px lightgrey; \">" +
                                    "<table class=\"table table-divider\" style=\"margin-bottom: 0px;\">");
                patientHistory.Append("<tr>" +
                                        "<td colspan=\"2\" style=\"border-top:0px;\">" +
                                        "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                                            "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; font-size:15px;\">" +
                                                "<b>" + patientHistoryData.list.historyheader.admissionDate.ToString("dddd, dd MMMM yyyy") + " - " +
                                                patientHistoryData.list.historyheader.organizationCode + " - " + patientHistoryData.list.historyheader.admissionTypeName + " - " +
                                                patientHistoryData.list.historyheader.doctorName + "</b" +
                                            "</div>" +
                                        "</div>" +
                                        "</td>" +
                                      "</tr>");

                var tempChiefComplaint = historyDiagnoses.Find(x => x.mappingName == "PATIENT COMPLAINT").remarks.Split('\n').ToList();
                var notesChiefComplaint = "";
                if (tempChiefComplaint.Count != 0)
                {
                    for (int i = 0; i < tempChiefComplaint.Count; i++)
                    {
                        notesChiefComplaint = notesChiefComplaint + "<div>" + tempChiefComplaint[i] + "</div>";
                    }
                }
                else
                {
                    notesChiefComplaint = historyDiagnoses.Find(x => x.mappingName == "PATIENT COMPLAINT").remarks;
                }

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"> <b> <label style=\"display:" + setENG + "\"> Chief Complaint </label> <label style=\"display:" + setIND + "\"> Keluhan Utama </label> </b></td><td>" + notesChiefComplaint + "</td></tr>");

                var tempAnemsis = patientHistoryData.list.historyanamnesis.remarks.Split('\n').ToList();
                var notesAnemsis = "";
                if (tempAnemsis.Count != 0)
                {
                    for (int i = 0; i < tempAnemsis.Count; i++)
                    {
                        notesAnemsis = notesAnemsis + "<div>" + tempAnemsis[i] + "</div>";
                    }
                }
                else
                {
                    notesAnemsis = patientHistoryData.list.historyanamnesis.remarks;
                }

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"> <b>Anamnesis</b></td>" +
                                        "<td style=\"padding-top:0px; padding-bottom:0px;\">" +
                                        "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                                            "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px 8px 8px 0px;\">" +
                                                "<div>" + notesAnemsis + "</div>" +
                                            "</div>" +
                                            "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\">" +
                                                "<div><b> <label style=\"display:" + setENG + "\"> Pregnant </label> <label style=\"display:" + setIND + "\"> Hamil </label> </b></div><div>" + statusPregnancy + "</div>" +
                                            "</div>" +
                                            "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\">" +
                                                "<div><b> <label style=\"display:" + setENG + "\"> Breast Feeding </label> <label style=\"display:" + setIND + "\"> Menyusui </label></b></div><div>" + statusBreastFeeding + "</div>" +
                                            "</div>" +
                                        "</div>" +
                                        "</td></tr>");

                /*-------------------------------- Routine Medication --------------------------------*/
                var routineMedication = illnessHistory.FindAll(x => x.type.Equals("RoutineMedication"));
                var routineMedicationInner = "";
                routineMedicationInner = "<b> <label style=\"display:" + setENG + "\"> Routine Medication </label> <label style=\"display:" + setIND + "\"> Pengobatan Rutin </label></b><ul style=\"padding-left:15px;\">";
                if (routineMedication.Count > 1)
                {
                    foreach (PatientHistoryIllness data in routineMedication)
                    {
                        if (data.value.ToUpper() == "LAIN-LAIN")
                            routineMedicationInner = routineMedicationInner + "<li>" + data.remarks + "</li>";
                        else
                            routineMedicationInner = routineMedicationInner + "<li>" + data.value + "</li>";
                    }
                }
                else {
                    if (routineMedication.Count == 0)
                        routineMedicationInner = routineMedicationInner + "-";
                    else {
                        if (routineMedication[0].value == "" || routineMedication[0].remarks == "")
                        {
                            if (routineMedication[0].value.ToUpper() == "LAIN-LAIN")
                                routineMedicationInner = routineMedicationInner + "" + routineMedication[0].remarks + "";
                            else
                                routineMedicationInner = routineMedicationInner + "" + routineMedication[0].value + "";
                        }
                    }
                }

                routineMedicationInner = routineMedicationInner + "</ul>";

                /*-------------------------------- Drug Allergy --------------------------------*/
                var drugAlergy = illnessHistory.FindAll(x => x.type.Equals("DrugAllergy"));
                var drugAlergyInner = "";
                drugAlergyInner = "<b><label style=\"display:" + setENG + "\"> Drug Allergy  </label> <label style=\"display:" + setIND + "\"> Alergi Obat </label> </b> <br />";
                if (drugAlergy.Count > 1)
                {
                    drugAlergyInner = drugAlergyInner + "<ul style=\"padding-left:15px;\">";
                    foreach (PatientHistoryIllness data in drugAlergy)
                    {
                        if (data.value.ToUpper() == "LAIN-LAIN")
                            drugAlergyInner = drugAlergyInner + "<li>" + data.remarks + "</li>";
                        else
                            drugAlergyInner = drugAlergyInner + "<li>" + data.value + "</li>";
                    }
                    drugAlergyInner = drugAlergyInner + "</ul>";
                }
                else
                {
                    if (drugAlergy.Count == 0)
                        drugAlergyInner = drugAlergyInner + "-";
                    else
                    {
                        if (drugAlergy[0].value != "" || drugAlergy[0].remarks != "")
                        {
                            if (drugAlergy[0].value.ToUpper() == "LAIN-LAIN")
                                drugAlergyInner = drugAlergyInner + "" + drugAlergy[0].remarks + "";
                            else
                                drugAlergyInner = drugAlergyInner + "" + drugAlergy[0].value + "";
                        }
                    }
                }

                /*-------------------------------- Food Allergy --------------------------------*/
                var foodAlergy = illnessHistory.FindAll(x => x.type.Equals("FoodAllergy"));
                var foodAlergyInner = "";
                foodAlergyInner = "<b> <label style=\"display:" + setENG + "\"> Food Allergy </label> <label style=\"display:" + setIND + "\"> Alergi Makanan </label></b><ul style=\"padding-left:15px;\">";
                if (foodAlergy.Count > 1)
                {
                    foreach (PatientHistoryIllness data in foodAlergy)
                    {
                        if (data.value.ToUpper() == "LAIN-LAIN")
                            foodAlergyInner = foodAlergyInner + "<li>" + data.remarks + "</li>";
                        else
                            foodAlergyInner = foodAlergyInner + "<li>" + data.value + "</li>";
                    }
                }
                else
                {
                    if (foodAlergy.Count == 0)
                        foodAlergyInner = foodAlergyInner + "-";
                    else
                    {
                        if (foodAlergy[0].value != "" || foodAlergy[0].remarks != "")
                        {
                            if (foodAlergy[0].value.ToUpper() == "LAIN-LAIN")
                                foodAlergyInner = foodAlergyInner + "" + foodAlergy[0].remarks + "";
                            else
                                foodAlergyInner = foodAlergyInner + "" + foodAlergy[0].value + "";
                        }
                    }
                }

                foodAlergyInner = foodAlergyInner + "</ul>";

                /*-------------------------------- Surgery --------------------------------*/
                var sugeryHistory = illnessHistory.FindAll(x => x.type.Equals("Surgery"));
                var sugeryHistoryInner = "";
                sugeryHistoryInner = "<b> <label style=\"display:" + setENG + "\"> Surgery History </label> <label style=\"display:" + setIND + "\"> Riwayat Operasi </label></b><ul style=\"padding-left:15px;\">";
                if (sugeryHistory.Count > 1)
                {
                    foreach (PatientHistoryIllness data in sugeryHistory)
                    {
                        if (data.value.ToUpper() == "LAIN-LAIN")
                            sugeryHistoryInner = sugeryHistoryInner + "<li>" + data.remarks + "</li>";
                        else
                            sugeryHistoryInner = sugeryHistoryInner + "<li>" + data.value + "</li>";
                    }
                }
                else
                {
                    if (sugeryHistory.Count == 0)
                        sugeryHistoryInner = sugeryHistoryInner + "-";
                    else
                    {
                        if (sugeryHistory[0].value != "" || sugeryHistory[0].remarks != "")
                        {
                            if (sugeryHistory[0].value.ToUpper() == "LAIN-LAIN")
                                sugeryHistoryInner = sugeryHistoryInner + "" + sugeryHistory[0].remarks + "";
                            else
                                sugeryHistoryInner = sugeryHistoryInner + "" + sugeryHistory[0].value + "";
                        }
                    }
                }

                sugeryHistoryInner = sugeryHistoryInner + "</ul>";

                /*-------------------------------- Diseas History ( Personal Disease ) --------------------------------*/
                var diseasHistory = illnessHistory.FindAll(x => x.type.Equals("PersonalDisease"));
                var diseasHistoryInner = "";
                diseasHistoryInner = "<b> <label style=\"display:" + setENG + "\"> Disease History </label> <label style=\"display:" + setIND + "\"> Riwayat Penyakit </label></b><ul style=\"padding-left:15px;\">";
                if (diseasHistory.Count > 1)
                {
                    foreach (PatientHistoryIllness data in diseasHistory)
                    {
                        if (data.value.ToUpper() == "LAIN-LAIN")
                            diseasHistoryInner = diseasHistoryInner + "<li>" + data.remarks + "</li>";
                        else
                            diseasHistoryInner = diseasHistoryInner + "<li>" + data.value + "</li>";
                    }
                }
                else
                {
                    if (diseasHistory.Count == 0)
                        diseasHistoryInner = diseasHistoryInner + "-";
                    else
                    {
                        if (diseasHistory[0].value != "" || diseasHistory[0].remarks != "")
                        {
                            if (diseasHistory[0].value.ToUpper() == "LAIN-LAIN")
                                diseasHistoryInner = diseasHistoryInner + "" + diseasHistory[0].remarks + "";
                            else
                                diseasHistoryInner = diseasHistoryInner + "" + diseasHistory[0].value + "";
                        }
                    }
                }

                diseasHistoryInner = diseasHistoryInner + "</ul>";

                /*-------------------------------- Family Disease --------------------------------*/
                var familyDiseasHistory = illnessHistory.FindAll(x => x.type.Equals("FamilyDisease"));
                var familyDiseasHistoryInner = "";
                familyDiseasHistoryInner = "<b> <label style=\"display:" + setENG + "\"> Family Disease History </label> <label style=\"display:" + setIND + "\"> Riwayat Penyakit Keluarga </label> </b><ul style=\"padding-left:15px;\">";
                if (familyDiseasHistory.Count > 1)
                {
                    foreach (PatientHistoryIllness data in familyDiseasHistory)
                    {
                        if (data.value.ToUpper() == "LAIN-LAIN")
                            familyDiseasHistoryInner = familyDiseasHistoryInner + "<li>" + data.remarks + "</li>";
                        else
                            familyDiseasHistoryInner = familyDiseasHistoryInner + "<li>" + data.value + "</li>";
                    }
                }
                else
                {
                    if (familyDiseasHistory.Count == 0)
                        familyDiseasHistoryInner = familyDiseasHistoryInner + "-";
                    else
                    {
                        if (familyDiseasHistory[0].value == "" || familyDiseasHistory[0].remarks == "")
                        {
                            if (familyDiseasHistory[0].value.ToUpper() == "LAIN-LAIN")
                                familyDiseasHistoryInner = familyDiseasHistoryInner + "" + familyDiseasHistory[0].remarks + "";
                            else
                                familyDiseasHistoryInner = familyDiseasHistoryInner + "" + familyDiseasHistory[0].value + "";
                        }
                    }
                }

                familyDiseasHistoryInner = familyDiseasHistoryInner + "</ul>";

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\">" +
                                            "<div><b> <label style=\"display:" + setENG + "\"> Medication & Allergies </label> <label style=\"display:" + setIND + "\"> Pengobatan & Alergi </label></b></div>" +
                                        //"<div style=\"color: blue; text-decoration:underline; \"><span>Revision</span></div>" +
                                        "</td>" +
                                        "<td style=\"padding-top:0px; padding-bottom:0px;\">" +
                                            "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px 8px 8px 0px;\">" +
                                                    "<div>" + routineMedicationInner + "</div>" +
                                                "</div>" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\">" +
                                                    "<div>" + drugAlergyInner + "</div>" +
                                                "</div>" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\">" +
                                                    "<div>" + foodAlergyInner + "</div>" +
                                                "</div>" +
                                            "</div>" +
                                        "</td>" +
                                        "</tr>");
                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\">" +
                                            "<div><b> <label style=\"display:" + setENG + "\"> Illness History </label> <label style=\"display:" + setIND + "\"> Riwayat Penyakit </label></b></div>" +
                                        //"<div style=\"color: blue; text-decoration:underline; \"><span>Revision</span></div>" +
                                        "</td>" +
                                        "<td style=\"padding-top:0px; padding-bottom:0px;\">" +
                                            "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px 8px 8px 0px;\">" +
                                                    "<div>" + sugeryHistoryInner + "</div>" +
                                                "</div>" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\">" +
                                                    "<div>" + diseasHistoryInner + "</div>" +
                                                "</div>" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\">" +
                                                    "<div>" + familyDiseasHistoryInner + "</div>" +
                                                "</div>" +
                                            "</div>" +
                                        "</td></tr>");

                List<PatientHistoryDiagnosis> endemicArea = historyDiagnoses.FindAll(x => x.mappingId.Equals(Guid.Parse("6A10C1FA-7C43-4E7C-A855-EAEA815BCADE")));
                var contentEndemicArea = "";

                if (endemicArea.Count > 1)
                {
                    foreach (PatientHistoryDiagnosis data in endemicArea)
                    {
                        if (data.value.ToUpper() != "LAIN-LAIN")
                            contentEndemicArea = contentEndemicArea + "<li>" + data.remarks + "</li>";
                        else
                            contentEndemicArea = contentEndemicArea + "<li>" + data.value + "</li>";
                    }
                }
                else
                {
                    contentEndemicArea = "-";
                    if (endemicArea[0].value != "" || endemicArea[0].remarks != "")
                    {
                        if (endemicArea[0].value.ToUpper() != "LAIN-LAIN")
                            contentEndemicArea = endemicArea[0].remarks + "";
                        else
                            contentEndemicArea = endemicArea[0].value + "";
                    }
                }


                List<PatientHistoryDiagnosis> screeningEndemic = historyDiagnoses.FindAll(x => x.mappingId.Equals(Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6")));
                var contentScreeningEndemic = "";

                if (screeningEndemic.Count > 1)
                {
                    foreach (PatientHistoryDiagnosis data in screeningEndemic)
                    {
                        if (data.value.ToUpper() != "LAIN-LAIN")
                            contentScreeningEndemic = contentScreeningEndemic + "<li>" + data.remarks + "</li>";
                        else
                            contentScreeningEndemic = contentScreeningEndemic + "<li>" + data.value + "</li>";
                    }
                }
                else
                {
                    contentScreeningEndemic = "-";

                    if (screeningEndemic[0].value != "" || screeningEndemic[0].remarks != "")
                    {
                        if (screeningEndemic[0].value.ToUpper() != "LAIN-LAIN")
                            contentScreeningEndemic = screeningEndemic[0].remarks;
                        else
                            contentScreeningEndemic = screeningEndemic[0].value;
                    }
                }

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\">" +
                                            "<div><b> <label style=\"display:" + setENG + "\"> Endemic Area Visitation </label> <label style=\"display:" + setIND + "\"> Kunjungan ke Daerah Endemis </label></b></div>" +
                                        "</td>" +
                                        "<td style=\"padding-top:0px; padding-bottom:0px;\">" +
                                            "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px 8px 8px 0px; width:40%;\">" +
                                                    "<div><b> <label style=\"display:" + setENG + "\"> Have Been to Endemic Area </label> <label style=\"display:" + setIND + "\"> Kunjungan ke Daerah Endemis </label> </b></div>" +
                                                    "<div>" + contentEndemicArea + "</div>" +
                                                "</div>" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey; width:55%;\">" +
                                                    "<div><b> <label style=\"display:" + setENG + "\"> Screening Infectious Disease </label> <label style=\"display:" + setIND + "\"> Pemindaian Penyakit Menular </label></b></div>" +
                                                    "<div>" + contentScreeningEndemic + "</div>" +
                                                "</div>" +
                                            "</div>" +
                                        "</td></tr>");

                var nutrition = historyDiagnoses.Find(x => x.mappingId.Equals(Guid.Parse("82B114B2-303C-43EC-963B-851B19A11EEA")));
                var contentNutrition = "-";
                if (nutrition.value != "" || nutrition.remarks != "")
                    contentNutrition = nutrition.value + " " + nutrition.remarks;

                var fasting = historyDiagnoses.Find(x => x.mappingId.Equals(Guid.Parse("BB077100-EAAE-41E4-91DB-B2B10154EE48")));
                var contentFasting = "-";
                if (fasting.value != "" || fasting.remarks != "")
                    contentFasting = fasting.value + " " + fasting.remarks;

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\">" +
                                            "<div><b> <label style=\"display:" + setENG + "\"> Nutrition & Fasting </label> <label style=\"display:" + setIND + "\"> Nutrisi & Puasa </label></b></div>" +
                                        //"<div style=\"color: blue; text-decoration:underline; \"><span>Revision</span></div>" +
                                        "</td>" +
                                        "<td style=\"padding-top:0px; padding-bottom:0px;\">" +
                                            "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px 8px 8px 0px; width:40%;\">" +
                                                    "<div><b> <label style=\"display:" + setENG + "\"> Nutrition Problem </label> <label style=\"display:" + setIND + "\"> Masalah Nutrisi </label></b></div>" +
                                                    "<div>" + contentNutrition + "</div>" +
                                                "</div>" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey; width:55%;\">" +
                                                    "<div><b> <label style=\"display:" + setENG + "\"> Fasting </label> <label style=\"display:" + setIND + "\"> Puasa </label></b></div>" +
                                                    "<div>" + contentFasting + "</div>" +
                                                "</div>" +
                                            "</div>" +
                                        "</td></tr>");

                var tempChest = physicalExams.Find(x => x.mappingId == Guid.Parse("7218971c-e89f-4172-ae3c-b7fb855c1d6d")).remarks.Split('\n').ToList();
                var notesChest = "";
                if (tempChest.Count != 0)
                {
                    for (int i = 0; i < tempChest.Count; i++)
                    {
                        notesChest = notesChest + "<div>" + tempChest[i] + "</div>";
                    }
                }
                else
                {
                    notesChest = physicalExams.Find(x => x.mappingId == Guid.Parse("7218971c-e89f-4172-ae3c-b7fb855c1d6d")).remarks;
                }

                var tempChestString = "";
                if (notesChest != "")
                {
                    tempChestString = "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey;\"> <div>" + notesChest + "</div></div>" +
                    "</div>";
                }
                else
                {
                    tempChestString = "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey;\"> <div> - </div></div>" +
                    "</div>";
                }

                var total = 0;
                var eyeData = physicalExams.Find(a => a.mappingName == "GCS EYE").value;
                var moveData = physicalExams.Find(a => a.mappingName == "GCS MOVE").value;
                var verbalData = physicalExams.Find(a => a.mappingName == "GCS VERBAL").value;

                string eyeName = "";
                if (eyeData != "")
                {
                    eyeName = eye.Find(x => x.idph.ToString() == physicalExams.Find(a => a.mappingName == "GCS EYE").value).name;
                    total = total + int.Parse(eyeData);
                }

                string moveName = "";
                if (moveData != "")
                {
                    moveName = move.Find(x => x.idph.ToString() == physicalExams.Find(a => a.mappingName == "GCS MOVE").value).name;
                    total = total + int.Parse(moveData);
                }

                string verbalName = "";
                if (verbalData != "")
                {
                    if (verbalData == "T")
                    {
                        verbalName = "Tracheostomy";
                        total = 0;
                    }
                    else if (verbalData == "A")
                    {
                        verbalName = "Aphasia";
                        total = 0;
                    }
                    else
                    {
                        verbalName = verbal.Find(x => x.idph.ToString() == physicalExams.Find(a => a.mappingName == "GCS VERBAL").value).name;
                        total = total + int.Parse(verbalData);
                    }
                }
                var painScale = physicalExams.Find(x => x.mappingId.Equals(Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176")));
                var painScaleString = "-";
                if (painScale != null)
                {
                    painScaleString = painScale.value;
                }

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"><b> <label style=\"display:" + setENG + "\"> Physical Examination </label> <label style=\"display:" + setIND + "\"> Pemeriksaan Fisik </label></b></td>");
                patientHistory.Append("<td style=\"padding:0px;\">" +
                    "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px;\"><b>Eye(Mata): </b><asp:Label runat =\"server\" Text=\"\" Font-Bold =\"true\"/>" + physicalExams.Find(x => x.mappingName == "GCS EYE").value + ". " + eyeName + "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\"><b>Move(Motorik) : </b><asp:Label runat=\"server\" Font-Bold=\"true\" Text=\"\" />" + physicalExams.Find(x => x.mappingName == "GCS MOVE").value + ". " + moveName + "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\"><b>Verbal(Verbal) : </b><asp:Label runat =\"server\" Font-Bold=\"true\" Text=\"\" />" + physicalExams.Find(x => x.mappingName == "GCS VERBAL").value + ". " + verbalName + "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\"><b> <label style=\"display:" + setENG + "\"> Score: </label> <label style=\"display:" + setIND + "\"> Skor: </label>  </b><asp:Label runat =\"server\" Font-Bold=\"true\" Text=\"\" />" + total + "</div>" +
                    "</div>" +
                    "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey;\"><b> <label style=\"display:" + setENG + "\"> Pain Scale: </label> <label style=\"display:" + setIND + "\"> Skala Nyeri: </label> </b><asp:Label runat =\"server\" Text=\"\" Font-Bold =\"true\"/>" + painScaleString + "</div>" +
                    "</div>" +
                    "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey;\">" +
                            "<div><b> <label style=\"display:" + setENG + "\"> Blood pressure </label> <label style=\"display:" + setIND + "\"> Tekanan Darah </label></b></div><div>" + physicalExams.Find(x => x.mappingName == "BLOOD PRESSURE HIGH").value + "/" + physicalExams.Find(x => x.mappingName == "BLOOD PRESSURE LOW").value + " mmHg</div>" +
                        "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">" +
                            "<div><b> <label style=\"display:" + setENG + "\"> Pulse Rate </label> <label style=\"display:" + setIND + "\"> Nadi </label></b></div><div>" + physicalExams.Find(x => x.mappingName == "PULSE RATE").value + " X/mnt</div>" +
                        "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">" +
                            "<div><b> <label style=\"display:" + setENG + "\"> Respiratory Rate </label> <label style=\"display:" + setIND + "\"> Pernapasan </label></b></div><div>" + physicalExams.Find(x => x.mappingName == "RESPIRATORY RATE").value + " X/mnt</div>" +
                        "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">" +
                            "<div><b>SpO2</b></div><div>" + physicalExams.Find(x => x.mappingName == "SPO2").value + " %</div>" +
                        "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">" +
                            "<div><b> <label style=\"display:" + setENG + "\"> Temperature </label> <label style=\"display:" + setIND + "\"> Suhu </label></b></div><div>" + physicalExams.Find(x => x.mappingName == "TEMPERATURE").value + " &#176;C</div>" +
                        "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">" +
                            "<div><b> <label style=\"display:" + setENG + "\"> Weight </label> <label style=\"display:" + setIND + "\"> Berat </label></b></div><div>" + physicalExams.Find(x => x.mappingName == "WEIGHT").value + " kg</div>" +
                        "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">" +
                            "<div><b> <label style=\"display:" + setENG + "\"> Height </label> <label style=\"display:" + setIND + "\"> Tinggi </label></b></div><div>" + physicalExams.Find(x => x.mappingName == "HEIGHT").value + " cm</div>" +
                        "</div>" +
                    "</div>" +
                    "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey;\">  <div><b> <label style=\"display:" + setENG + "\"> Mental Status </label> <label style=\"display:" + setIND + "\"> Status Mental </label></b></div>  <div class=\"btn-group\" role=\"group\" style=\"vertical-align:top\"> " + physicalExams.Find(x => x.mappingName == "MENTAL STATUS").value + "</div>  </div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">  <div><b> <label style=\"display:" + setENG + "\"> Consciousness Level </label> <label style=\"display:" + setIND + "\"> Kesadaran </label></b></div>  <div class=\"btn-group\" role=\"group\" style=\"vertical-align:top\"> " + physicalExams.Find(x => x.mappingName == "CONSCIOUSNESS LEVEL").value + "</div>  </div>" +
                    "</div>" + tempChestString +
                    "</td></tr>");
                var diagnosis = historyDiagnoses.Find(x => x.mappingId.Equals(Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")));
                var tempDiagnosis = diagnosis.remarks.Split('\n').ToList();
                var notesDiagnosis = "";
                if (tempDiagnosis.Count != 0)
                {
                    for (int i = 0; i < tempDiagnosis.Count; i++)
                    {
                        notesDiagnosis = notesDiagnosis + "<div>" + tempDiagnosis[i] + "</div>";
                    }
                }
                else
                {
                    notesDiagnosis = diagnosis.remarks;
                }

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"><b> <label style=\"display:" + setENG + "\"> Diagnosis </label> <label style=\"display:" + setIND + "\"> Diagnosa </label></b></td>" +
                                    "<td>" + notesDiagnosis + "</td></tr>");

                var tempPlanProcedure = historyPlanning.Find(x => x.mappingId.Equals(Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))).remarks.Split('\n').ToList();
                var notesPlanProcedure = "";
                if (tempPlanProcedure.Count != 0)
                {
                    for (int i = 0; i < tempPlanProcedure.Count; i++)
                    {
                        notesPlanProcedure = notesPlanProcedure + "<div>" + tempPlanProcedure[i] + "</div>";
                    }
                }
                else
                {
                    notesPlanProcedure = historyPlanning.Find(x => x.mappingId.Equals(Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))).remarks;
                }
                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"><b> <label style=\"display:" + setENG + "\"> Planning & Procedure </label> <label style=\"display:" + setIND + "\"> Perencanaan & Tindakan </label></b></td><td>" + notesPlanProcedure + "</td></tr>");

                var tempProcedure = headerPatient.procedureNotes.Split('\n').ToList();
                var notesProcedure = "";
                if (tempProcedure.Count != 0)
                {
                    for (int i = 0; i < tempProcedure.Count; i++)
                    {
                        notesProcedure = notesProcedure + "<div>" + tempProcedure[i] + "</div>";
                    }
                }
                else
                {
                    notesProcedure = headerPatient.procedureNotes;
                }

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"><b> <label style=\"display:" + setENG + "\"> Procedure Notes </label> <label style=\"display:" + setIND + "\"> Catatan Tindakan </label></b></td><td>" + notesProcedure + "</td></tr>");

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"><b> <label style=\"display:" + setENG + "\"> Clinical Findings </label> <label style=\"display:" + setIND + "\"> Penemuan Klinis </label></b></td><td>" +
                    "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" + laboratoryData + " " + radiologyData + "</div>&nbsp;" +
                    "</td></tr>");
                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%; border-radius:0px 0px 0px 7px;\"><b> <label style=\"display:" + setENG + "\"> Prescription </label> <label style=\"display:" + setIND + "\"> Resep </label></b></td><td style=\"padding:0px;\">");

                if (tmpDrugs.Count != 0)
                {
                    var tempDoctorDrugs = tmpDrugs.FindAll(x => x.IsDoctor == 1);

                    /*---------------------------------------------------------- Doctor Prescription --------------------------------------------------*/
                    if (tempDoctorDrugs.Count != 0)
                    {
                        patientHistory.Append("<div><div style=\"padding:6px 6px 0px 8px;background-color:#b4e3fa;height:35px;font-family:Helvetica;font-size:14px;\"><b> <label style=\"display:" + setENG + "\"> DOCTOR Prescription </label> <label style=\"display:" + setIND + "\"> Resep DOKTER </label></b></div>");
                        patientHistory.Append("<table class=\"table table-striped table-condensed\" style=\"margin-bottom: 0px;\"><tr>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Item </label> <label style=\"display:" + setIND + "\"> Obat </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Qty </label> <label style=\"display:" + setIND + "\"> Jml </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> U.O.M </label> <label style=\"display:" + setIND + "\"> Unit </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Frequency </label> <label style=\"display:" + setIND + "\"> Frekuensi </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Dose </label> <label style=\"display:" + setIND + "\"> Dosis </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Dose UOM </label> <label style=\"display:" + setIND + "\"> Dosis Unit </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Instruction </label> <label style=\"display:" + setIND + "\"> Instruksi </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Route </label> <label style=\"display:" + setIND + "\"> Rute </label></b></td>" +
                                                "<td><b>Iter </b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Routine </label> <label style=\"display:" + setIND + "\"> Rutin </label></b></td>" +
                                                "</tr>");
                        foreach (PatientHistoryPrescription dataDrugs in tempDoctorDrugs)
                        {
                            var doseUom = "";
                            if (listdoseUom.Find(x => x.doseUomId.Equals(dataDrugs.dose_uom_id)) != null)
                                doseUom = listdoseUom.Find(x => x.doseUomId.Equals(dataDrugs.dose_uom_id)).name;

                            var routine = "";
                            if (dataDrugs.isRoutine)
                                routine = "Yes";
                            else
                                routine = "No";

                            patientHistory.Append("<tr>" +
                                                    "<td>" + dataDrugs.salesItemName + "</td>" +
                                                    "<td>" + dataDrugs.quantity + "</td>" +
                                                    "<td>" + dataDrugs.uom + "</td>" +
                                                    "<td>" + dataDrugs.frequency + "</td>" +
                                                    "<td>" + (Int64)dataDrugs.dose + "</td>" +
                                                    "<td>" + doseUom + "</td>" +
                                                    "<td>" + dataDrugs.instruction + "</td>" +
                                                    "<td>" + dataDrugs.route + "</td>" +
                                                    "<td>" + dataDrugs.iter + "</td>" +
                                                    "<td>" + routine + "</td>" +
                                                    "</tr>");
                        }
                        patientHistory.Append("</table></div>");                                               
                    }

                    /*---------------------------------------------------------- Doctor Prescription Notes --------------------------------------------------*/
                    var doctorNotes = "-";
                    if (historyPlanning.Find(x => x.mappingId.Equals(Guid.Parse("2DF0294D-F94E-4BA4-8BA1-F017BFB55D92"))).remarks != "")
                    {
                        patientHistory.Append("<div style=\"padding:6px 8px 6px 8px;font-family:Helvetica;background-color:#b4e3fa;margin-bottom: 15px;\">" +
                                            "<div style=\"font-size:14px;\"><b> <label style=\"display:" + setENG + "\"> DOCTOR Prescription Notes </label> <label style=\"display:" + setIND + "\"> Catatan Resep DOKTER </label></b></div>" +
                                            "<div style=\"font-size:12px;\">");

                        doctorNotes = historyPlanning.Find(x => x.mappingId.Equals(Guid.Parse("2DF0294D-F94E-4BA4-8BA1-F017BFB55D92"))).remarks;
                        var tempnotes = doctorNotes.Split('\n').ToList();
                        if (tempnotes.Count != 0)
                        {
                            doctorNotes = "";
                            for (int i = 0; i < tempnotes.Count; i++)
                            {
                                patientHistory.Append("<div>" + tempnotes[i] + "</div>");
                            }
                        }
                        else
                        {
                            patientHistory.Append(doctorNotes);
                        }

                        patientHistory.Append("</div>" +
                                                "</div>");
                    }

                    /*---------------------------------------------------------- Doctor Consumables --------------------------------------------------*/
                    if (tmpConsumableDoctor.Count != 0)
                    {
                        patientHistory.Append("<div style=\"margin-bottom: 15px;\"><div style=\"padding:6px 6px 0px 8px;background-color:#b4e3fa;height:35px;font-family:Helvetica;font-size:14px;\"><b> <label style=\"display:" + setENG + "\"> DOCTOR Consumables </label> <label style=\"display:" + setIND + "\"> Alat Kesehatan DOKTER </label></b></div>");
                        patientHistory.Append("<table class=\"table table-striped table-condensed\"><tr>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Item </label> <label style=\"display:" + setIND + "\"> Alat </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Qty </label> <label style=\"display:" + setIND + "\"> Jml </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> U.O.M </label> <label style=\"display:" + setIND + "\"> Unit </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Instruction </label> <label style=\"display:" + setIND + "\"> Instruksi </label></b></td>" +
                                                "</tr>");
                        foreach (PatientHistoryPrescription dataconsumable in tmpConsumableDoctor)
                        {
                            patientHistory.Append("<tr>" +
                                                    "<td>" + dataconsumable.salesItemName + "</td>" +
                                                    "<td>" + dataconsumable.quantity + "</td>" +
                                                    "<td>" + dataconsumable.uom + "</td>" +
                                                    "<td>" + dataconsumable.instruction + "</td>" +
                                                    "</tr>");
                        }
                        patientHistory.Append("</table></div>");
                    }

                    /*---------------------------------------------------------- Doctor Additional --------------------------------------------------*/
                    if (tmpAdditionalDoctor.Count != 0)
                    {
                        patientHistory.Append("<div><div style=\"padding:6px 6px 0px 8px;background-color:#b4e3fa;height:35px;font-family:Helvetica;font-size:14px;\"><b> <label style=\"display:" + setENG + "\"> DOCTOR Additional Prescription </label> <label style=\"display:" + setIND + "\"> Tambahan Resep DOKTER </label></b></div>");
                        patientHistory.Append("<table class=\"table table-striped table-condensed\" style=\"margin-bottom: 0px;\"><tr>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Item </label> <label style=\"display:" + setIND + "\"> Alat </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Qty </label> <label style=\"display:" + setIND + "\"> Jml </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> U.O.M </label> <label style=\"display:" + setIND + "\"> Unit </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Instruction </label> <label style=\"display:" + setIND + "\"> Instruksi </label></b></td>" +
                                                "</tr>");
                        foreach (PatientHistoryPrescription dataconsumable in tmpAdditionalDoctor)
                        {
                            patientHistory.Append("<tr>" +
                                                    "<td>" + dataconsumable.salesItemName + "</td>" +
                                                    "<td>" + dataconsumable.quantity + "</td>" +
                                                    "<td>" + dataconsumable.uom + "</td>" +
                                                    "<td>" + dataconsumable.instruction + "</td>" +
                                                    "</tr>");
                        }
                        patientHistory.Append("</table></div>");

                        /*---------------------------------------------------------- Doctor Additional Notes --------------------------------------------------*/
                        var doctorAdditionalNotes = "-";
                        if (historyPlanning.Find(x => x.mappingId.Equals(Guid.Parse("5E34AE60-1D72-4EFD-8440-C4442515AABE"))).remarks != "")
                        {
                            doctorAdditionalNotes = historyPlanning.Find(x => x.mappingId.Equals(Guid.Parse("5E34AE60-1D72-4EFD-8440-C4442515AABE"))).remarks;
                        }

                        patientHistory.Append("<div style=\"padding:6px 8px 6px 8px;font-family:Helvetica;background-color:#b4e3fa;margin-bottom: 15px;\">" +
                                                "<div style=\"font-size:14px;\"><b> <label style=\"display:" + setENG + "\"> DOCTOR Additional Notes </label> <label style=\"display:" + setIND + "\"> Catatan Tambahan DOKTER </label></b></div>");

                        patientHistory.Append("<div style=\"font-size:12px;\">");
                        var tempAddNotesDoctor = doctorAdditionalNotes.Split('\n').ToList();
                        var doctorAddNotes = "-";
                        if (tempAddNotesDoctor.Count != 0)
                        {
                            doctorAddNotes = "";
                            for (int i = 0; i < tempAddNotesDoctor.Count; i++)
                            {
                                patientHistory.Append("<div>" + tempAddNotesDoctor[i] + "</div>");
                            }
                        }
                        else
                        {
                            patientHistory.Append(doctorAddNotes);
                        }
                        patientHistory.Append("</div></div>");
                    }

                    /*---------------------------------------------------------- Pharmacist Prescription --------------------------------------------------*/
                    if (tmpPrescriptionPharmacist.Count != 0)
                    {
                        patientHistory.Append("<div><div style=\"padding:6px 6px 0px 8px;background-color:#dfe69b;height:35px;font-family:Helvetica;font-size:14px;\"><b> <label style=\"display:" + setENG + "\"> PHARMACIST Prescription </label> <label style=\"display:" + setIND + "\"> Resep FARMASIS </label></b></div>");
                        patientHistory.Append("<table class=\"table table-striped table-condensed\" style=\"margin-bottom: 0px;\"><tr>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Item </label> <label style=\"display:" + setIND + "\"> Obat </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Qty </label> <label style=\"display:" + setIND + "\"> Jml </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> U.O.M </label> <label style=\"display:" + setIND + "\"> Unit </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Frequency </label> <label style=\"display:" + setIND + "\"> Frekuensi </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Dose </label> <label style=\"display:" + setIND + "\"> Dosis </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Dose UOM </label> <label style=\"display:" + setIND + "\"> Dosis Unit </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Instruction </label> <label style=\"display:" + setIND + "\"> Instruksi </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Route </label> <label style=\"display:" + setIND + "\"> Rute </label></b></td>" +
                                                "<td><b>Iter </b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Routine </label> <label style=\"display:" + setIND + "\"> Rutin </label></b></td>" +
                                                "</tr>");
                        foreach (PatientHistoryPrescription dataDrugs in tmpPrescriptionPharmacist)
                        {
                            var doseUom = "";
                            if (listdoseUom.Find(x => x.doseUomId.Equals(dataDrugs.dose_uom_id)) != null)
                                doseUom = listdoseUom.Find(x => x.doseUomId.Equals(dataDrugs.dose_uom_id)).name;

                            var routine = "";
                            if (dataDrugs.isRoutine)
                                routine = "Yes";
                            else
                                routine = "No";

                            patientHistory.Append("<tr>" +
                                                    "<td>" + dataDrugs.salesItemName + "</td>" +
                                                    "<td>" + dataDrugs.quantity + "</td>" +
                                                    "<td>" + dataDrugs.uom + "</td>" +
                                                    "<td>" + dataDrugs.frequency + "</td>" +
                                                    "<td>" + (Int64)dataDrugs.dose + "</td>" +
                                                    "<td>" + doseUom + "</td>" +
                                                    "<td>" + dataDrugs.instruction + "</td>" +
                                                    "<td>" + dataDrugs.route + "</td>" +
                                                    "<td>" + dataDrugs.iter + "</td>" +
                                                    "<td>" + routine + "</td>" +
                                                    "</tr>");
                        }
                        patientHistory.Append("</table></div>");                       
                    }

                    /*---------------------------------------------------------- Pharmacist Prescription Notes --------------------------------------------------*/
                    var pharmacistNotes = "-";
                    if (headerPatient.PharmacyNotes != "")
                    {
                        pharmacistNotes = headerPatient.PharmacyNotes;

                        patientHistory.Append("<div style=\"padding:6px 8px 6px 8px;font-family:Helvetica;background-color:#dfe69b;margin-bottom: 15px;\">" +
                                            "<div style=\"font-size:14px;\"><b> <label style=\"display:" + setENG + "\"> PHARMACIST Prescription Notes </label> <label style=\"display:" + setIND + "\"> Catatan Resep FARMASIS </label></b></div>" +
                                            "<div style=\"font-size:12px;\">");

                        var tempnotes = pharmacistNotes.Split('\n').ToList();
                        if (tempnotes.Count != 0)
                        {
                            for (int i = 0; i < tempnotes.Count; i++)
                            {
                                patientHistory.Append("<div>" + tempnotes[i] + "</div>");
                            }
                        }
                        else
                        {
                            patientHistory.Append(pharmacistNotes);
                        }

                        patientHistory.Append("</div>" +
                                    "</div>");
                    }

                    /*---------------------------------------------------------- Pharmacist Consumables --------------------------------------------------*/
                    if (tmpConsumablePharmacist.Count != 0)
                    {
                        patientHistory.Append("<div style=\"margin-bottom: 15px;\"><div style=\"padding:6px 6px 0px 8px;background-color:#dfe69b;height:35px;font-family:Helvetica;font-size:14px;\"><b> <label style=\"display:" + setENG + "\"> PHARMACIST Consumables </label> <label style=\"display:" + setIND + "\"> Alat Kesehatan FARMASIS </label></b></div>");
                        patientHistory.Append("<table class=\"table table-striped table-condensed\"><tr>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Item </label> <label style=\"display:" + setIND + "\"> Alat </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Qty </label> <label style=\"display:" + setIND + "\"> Jml </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> U.O.M </label> <label style=\"display:" + setIND + "\"> Unit </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Instruction </label> <label style=\"display:" + setIND + "\"> Instruksi </label></b></td>" +
                                                "</tr>");
                        foreach (PatientHistoryPrescription dataconsumable in tmpConsumablePharmacist)
                        {
                            patientHistory.Append("<tr>" +
                                                    "<td>" + dataconsumable.salesItemName + "</td>" +
                                                    "<td>" + dataconsumable.quantity + "</td>" +
                                                    "<td>" + dataconsumable.uom + "</td>" +
                                                    "<td>" + dataconsumable.instruction + "</td>" +
                                                    "</tr>");
                        }
                        patientHistory.Append("</table></div>");
                    }

                    /*---------------------------------------------------------- Pharmacist Additional --------------------------------------------------*/
                    if (tmpAdditionalPharmacist.Count != 0)
                    {
                        patientHistory.Append("<div><div style=\"padding:6px 6px 0px 8px;background-color:#dfe69b;height:35px;font-family:Helvetica;font-size:14px;\"><b> <label style=\"display:" + setENG + "\"> PHARMACIST Additional Prescription </label> <label style=\"display:" + setIND + "\"> Tambahan Resep FARMASIS </label></b></div>");
                        patientHistory.Append("<table class=\"table table-striped table-condensed\" style=\"margin-bottom: 0px;\"><tr>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Item </label> <label style=\"display:" + setIND + "\"> Alat </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Qty </label> <label style=\"display:" + setIND + "\"> Jml </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> U.O.M </label> <label style=\"display:" + setIND + "\"> Unit </label></b></td>" +
                                                "<td><b> <label style=\"display:" + setENG + "\"> Instruction </label> <label style=\"display:" + setIND + "\"> Instruksi </label></b></td>" +
                                                "</tr>");
                        foreach (PatientHistoryPrescription dataconsumable in tmpAdditionalPharmacist)
                        {
                            patientHistory.Append("<tr>" +
                                                    "<td>" + dataconsumable.salesItemName + "</td>" +
                                                    "<td>" + dataconsumable.quantity + "</td>" +
                                                    "<td>" + dataconsumable.uom + "</td>" +
                                                    "<td>" + dataconsumable.instruction + "</td>" +
                                                    "</tr>");
                        }
                        patientHistory.Append("</table></div>");


                        /*---------------------------------------------------------- Pharmacist Additional Notes --------------------------------------------------*/
                        var pharmacistAdditionalNotes = "-";
                        if (headerPatient.AdditionalPharmacyNotes != "")
                        {
                            pharmacistAdditionalNotes = headerPatient.AdditionalPharmacyNotes;
                        }

                        patientHistory.Append("<div style=\"padding:6px 8px 6px 8px;font-family:Helvetica;background-color:#dfe69b;margin-bottom: 15px;\">" +
                                                "<div style=\"font-size:14px;\"><b> <label style=\"display:" + setENG + "\"> PHARMACIST Additional Notes </label> <label style=\"display:" + setIND + "\"> Catatan Tambahan FARMASIS </label></b></div>" +
                                                "<div style=\"font-size:12px;\">" + pharmacistAdditionalNotes + "</div>" +
                                            "</div>");
                    }
                }

                if (Helper.GetFlagCompound(this) == "TRUE")
                {
                    if (tmpCompound.Count != 0)
                    {
                        List<MedicalHistory> detailCompound = new List<MedicalHistory>();
                        patientHistory.Append("<div style=\"padding-left:8px;\"><b>Compound Prescription</b></div>");
                        patientHistory.Append("<table class=\"table table-striped table-condensed\"><tr>" +
                                                "<td><b>Item</b></td>" +
                                                "<td><b>Qty</b></td>" +
                                                "<td><b>U.O.M</b></td>" +
                                                "<td><b>Frequency</b></td>" +
                                                "<td><b>Dose & Instruction</b></td>" +
                                                "<td><b>Route</b></td>" +
                                                "<td><b>Iter</b></td>" +
                                                "<td><b>Routine</b></td>" +
                                                "</tr>");
                        foreach (String cmpName in compoundName)
                        {
                            var header = tmpCompound.Find(x => x.compoundName == cmpName && x.itemId == 0);

                            var routine = "";
                            if (header.isRoutine)
                                routine = "Yes";
                            else
                                routine = "No";

                            string link = "javascript:Open('" + cmpName + "')";

                            patientHistory.Append("<tr>" +
                                                    "<td><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline; \">" + cmpName + "</a></td>" +
                                                "<td>" + header.quantity + "</td>" +
                                                "<td>" + header.uom + "</td>" +
                                                "<td>" + header.frequency + "</td>" +
                                                "<td>" + header.instruction + "</td>" +
                                                "<td>" + header.route + "</td>" +
                                                "<td>" + header.iter + "</td>" +
                                                "<td>" + routine + "</td>" +
                                                "</tr>");

                            //----------------------------------------------------------------------------

                            var tmpDataCompound = tmpCompound.OrderBy(x => x.compoundName).ToList().FindAll(x => x.compoundName == cmpName && x.itemId != 0);
                            foreach (PatientHistoryPrescription dataCompound in tmpDataCompound)
                            {
                                detailCompound.Add(new MedicalHistory
                                {
                                    compoundName = dataCompound.compoundName,
                                    doseText = dataCompound.doseText,
                                    uom = dataCompound.uom,
                                    frequency = dataCompound.frequency,
                                    instruction = dataCompound.instruction,
                                    itemName = dataCompound.salesItemName,
                                    quantity = Int64.Parse(dataCompound.quantity),
                                    route = dataCompound.route,
                                    iter = dataCompound.iter.ToString()
                                });
                            }

                        }
                        patientHistory.Append("</table>");
                        Session.Remove(Helper.ViewStatePatientHistoryCompound);
                        Session[Helper.ViewStatePatientHistoryCompound] = detailCompound;
                    }
                }
                
                patientHistory.Append("</td></tr>");
                patientHistory.Append("</table></div>");
                //-------------------------------- String Patient History Data --------------------------------------------
            }
            #endregion

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", PatientId.ToString(), "loadDataPatientHistory", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));


            return patientHistory;
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", PatientId.ToString(), "loadDataPatientHistory", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            return new StringBuilder();
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnTanggalHopeMR_Click(object sender, EventArgs e)
    {
        if (Session[Helper.SessionPreviousRowIndex] != null)
        {
            var previousRowIndex = (int)Session[Helper.SessionPreviousRowIndex];
            GridViewRow PreviousRow = gvHopeMRList.Rows[previousRowIndex];
            PreviousRow.BackColor = Color.White;
        }

        GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
        row.BackColor = ColorTranslator.FromHtml("#CDD2DD");
        Session[Helper.SessionPreviousRowIndex] = row.RowIndex;

        string localIP = GetLocalIPAddress();
        string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_HopeMR"];

        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        HiddenField targetLink = (HiddenField)gvHopeMRList.Rows[selRowIndex].FindControl("hdnLinkHopeMR");
        myIframe.Src = targetLink.Value.Replace("//" + localIP + "/viewermrhope", baseURLhttps);

        img_noData_hope_emr.Style.Add("dsiplay", "none");
        myIframe.Style.Add("display", "block");
    }

    protected void btn_load_more_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            //Log.Info(LogConfig.LogStart());

            List<PatientHistoryEncounter> encounterData = (List<PatientHistoryEncounter>)Session[Helper.ViewStatePageData];
            var index = int.Parse(hf_load_encounter.Value) + 1;

            hf_load_encounter.Value = index.ToString();
            StringBuilder tmpString = (StringBuilder)Session[Helper.ViewStatePatientHistoryInner];

            StringBuilder patientHistory = loadDataPatientHistory(encounterData[index].organizationId, encounterData[index].patientId, encounterData[index].admissionId, encounterData[index].encounterId.ToString());

            tmpString = tmpString.Append(patientHistory);

            tblPatientHistory.InnerHtml = tmpString.ToString();

            if (index + 1 == encounterData.Count)
            {
                btn_load_more.Style.Add("display", "none");
            }
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value, "btn_load_more_Click", StartTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            //Log.Info(LogConfig.LogEnd());
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "btn_load_more_Click", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
    }

    protected void btnCompoundDetail_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            //Log.Info(LogConfig.LogStart());

            headerCompound.Text = "Header - " + hfCompoundName.Value;
            List<MedicalHistory> tempData = (List<MedicalHistory>)Session[Helper.ViewStatePatientHistoryCompound];
            List<MedicalHistory> detailCompoundData = tempData.FindAll(x => x.compoundName.Equals(hfCompoundName.Value) && x.itemId != 0);
            if (detailCompoundData.Count != 0)
            {
                DataTable dt = Helper.ToDataTable(detailCompoundData);
                gvw_detail_compound.DataSource = dt;
                gvw_detail_compound.DataBind();
            }

            //Log.Info(LogConfig.LogEnd());
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value, "btnCompoundDetail_Click", StartTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "btnCompoundDetail_Click", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        preview.PostBackUrl = "/Form/SOAP/PreviewTemplate/PrintSoap.aspx" + "?idPatient=" + hfPatientId.Value + "&AdmissionId=" + hfAdmissionId.Value + "&EncounterId=" + Guid.Parse(hfEncounterId.Value.Replace("/", ""));

        SoapPagePreview.initializevalue(Helper.organizationId, long.Parse(hfPatientId.Value), long.Parse(hfAdmissionId.Value), Guid.Parse(hfEncounterId.Value.Replace("/", "")));
    }

    void getScannedData()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Session.Remove(Helper.ViewStateScannedData);
       
        DataTable dt = new DataTable();
        dt = clsPatientHistory.getScannedData(hfPatientId.Value, 1);

        if (dt.Rows.Count != 0)
        {
            List<ScannedMR> tempDataTable = new List<ScannedMR>();
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ScannedMR data = new ScannedMR();
                    data.OrganizationId = Convert.ToInt64(dt.Rows[i]["OrganizationId"]);
                    data.PatientId = Convert.ToInt64(dt.Rows[i]["PatientId"]);
                    data.AdmissionId = Convert.ToInt64(dt.Rows[i]["AdmissionId"]);
                    data.AdmissionNo = Convert.ToString(dt.Rows[i]["AdmissionNo"]);
                    data.AdmissionDate = Convert.ToString(dt.Rows[i]["AdmissionDate"]);
                    data.MrNo = Convert.ToInt64(dt.Rows[i]["OrganizationId"]);
                    data.AdmissionType = Convert.ToString(dt.Rows[i]["AdmissionType"]);
                    data.FormTypeName = Convert.ToString(dt.Rows[i]["FormTypeName"]);
                    data.Path = Convert.ToString(dt.Rows[i]["Path"]);
                    data.DoctorName = Convert.ToString(dt.Rows[i]["DoctorName"]);

                    tempDataTable.Add(data);
                }
            }
            catch (Exception ex)
            {
                Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getScannedData", StartTime, "ERROR", MyUser.GetUsername(), "", "", ""));
                //LogLibrary.Error("loadCompoundDetail", hfPatientId.Value.ToString() + " " + hfEncounterId.Value, ex.Message.ToString());
            }

            var dateStart = DateTime.Parse(DateTextboxStart_scanned.Text.ToString());
            var dateEnd = DateTime.Parse(DateTextboxEnd_scanned.Text.ToString());

            //Session[Helper.ViewStateScannedData] = tempDataTable.FindAll(x => x.AdmissionDate != "").FindAll(x => Convert.ToDateTime(x.AdmissionDate) >= dateStart && Convert.ToDateTime(x.AdmissionDate) <= dateEnd);
            for (int i = 0; i < tempDataTable.Count; i++)
            {
                if (tempDataTable.ElementAt(i).DoctorName == "")
                {
                    tempDataTable.ElementAt(i).DoctorName = "-";
                }
            }
            Session[Helper.ViewStateScannedData] = tempDataTable;

            fillFilterFormScannedMR();
        }
        else
        {
            StringBuilder scannedMRInner = new StringBuilder();
            scannedMRInner.Append("No Data");
            opd_data.InnerHtml = scannedMRInner.ToString();
            ipd_data.InnerHtml = scannedMRInner.ToString();
            mcu_data.InnerHtml = scannedMRInner.ToString();
        }
        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getScannedData", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
    }

    void fillFilterFormScannedMR()
    {
        var dateStart = DateTime.Parse(DateTextboxStart_scanned.Text.ToString());
        var dateEnd = DateTime.Parse(DateTextboxEnd_scanned.Text.ToString());
        List<ScannedMR> tempDataTable = ((List<ScannedMR>)Session[Helper.ViewStateScannedData]).FindAll(x => x.AdmissionDate != "").FindAll(x => Convert.ToDateTime(x.AdmissionDate) >= dateStart && Convert.ToDateTime(x.AdmissionDate) <= dateEnd);

        StringBuilder form_filter = new StringBuilder();

        List<ScannedMR> dtScannedFilter = new List<ScannedMR>();
        dtScannedFilter = tempDataTable;
        conAll.Value = "0";
        DocAll.Value = "0";

        if (tempDataTable != null)
        {
            if (DateTextboxStart_scanned.Text != "")
            {
                dtScannedFilter = new List<ScannedMR>();
                //var dateTemp = DateTime.Parse(tempDataTable[0].AdmissionDate);
                //var dateStart = DateTime.Parse(DateTextboxStart_scanned.Text.ToString());
                //var dateEnd = DateTime.Parse(DateTextboxEnd_scanned.Text.ToString());

                dtScannedFilter = tempDataTable.FindAll(x => x.AdmissionDate != "").FindAll(x => Convert.ToDateTime(x.AdmissionDate) >= dateStart);
                var temp = dtScannedFilter;
                if (DateTextboxEnd_scanned.Text != "")
                {
                    dtScannedFilter = new List<ScannedMR>();
                    dtScannedFilter = temp.FindAll(x => x.AdmissionDate != "").FindAll(x => Convert.ToDateTime(x.AdmissionDate) <= dateEnd);
                }
            }
            else
            {
                if (DateTextboxEnd_scanned.Text != "")
                {
                    dtScannedFilter = tempDataTable.FindAll(x => DateTime.Parse(x.AdmissionDate) <= DateTime.Parse(DateTextboxEnd_scanned.Text.ToString()));
                }
            }

            if (selected_form_filter.Value != "")
            {
                List<string> formSelected = selected_form_filter.Value.Split(',').ToList();
                var temp = new List<ScannedMR>();
                if (dtScannedFilter.Count != 0)
                    temp = dtScannedFilter;
                else
                    temp = tempDataTable;

                dtScannedFilter = new List<ScannedMR>();

                foreach (string data in formSelected)
                {
                    List<string> formFlagSplit = data.Split('_').ToList();
                    String formName = "";
                    if (formFlagSplit.Count != 1)
                    {
                        for (int i = 0; i < formFlagSplit.Count; i++)
                        {
                            if (i != formFlagSplit.Count - 1)
                            {
                                if (formName == "")
                                    formName = formFlagSplit[i];
                                else
                                    formName = formName + " " + formFlagSplit[i];
                            }
                        }
                    }
                    else
                        formName = formFlagSplit[0];

                    dtScannedFilter.AddRange(temp.FindAll(x => x.FormTypeName == formName && x.AdmissionType == formFlagSplit[formFlagSplit.Count - 1]));
                }
            }

            if (selected_doctor_filter.Value != "")
            {
                List<string> doctorSelected = selected_doctor_filter.Value.Split(';').ToList();
                var temp = new List<ScannedMR>();
                if (dtScannedFilter.Count != 0)
                    temp = dtScannedFilter;
                else
                    temp = tempDataTable;

                dtScannedFilter = new List<ScannedMR>();

                foreach (string data in doctorSelected)
                {
                    List<string> doctorFlag = data.Split('_').ToList();
                    String doctorName = "";
                    if (doctorFlag.Count != 1)
                    {
                        for (int i = 0; i < doctorFlag.Count; i++)
                        {
                            if (i != doctorFlag.Count - 1)
                            {
                                if (doctorName == "")
                                    doctorName = doctorFlag[i];
                                else
                                    doctorName = doctorName + " " + doctorFlag[i];
                            }
                        }
                    }
                    else
                        doctorName = doctorFlag[0];

                    dtScannedFilter.AddRange(temp.FindAll(x => x.DoctorName == doctorName && x.AdmissionType == doctorFlag[doctorFlag.Count - 1]));
                }
            }

            //if (dtScannedFilter.Count != 0)
            //{

            opd_scanned_filter.InnerHtml = "";
            ipd_scanned_filter.InnerHtml = "";
            mcu_scanned_filter.InnerHtml = "";

            /*==================================================== Add Filter for DOCTOR Scanned EMR ====================================================*/


                List<string> formFlagDoctor = selected_doctor_filter.Value.Split(';').ToList();

                string selectAllDoctor = "javascript:selectAllDoctor();";
                form_filter.Append("<div class=\"pretty p-icon p-curve\">" +
                                "<input type=\"checkbox\" id=\"all_doctor_filter\" onclick=\"" + selectAllDoctor + "\" />" +
                                "<div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font-size: 12px\" > </label > </div >" +
                                "</div>" +
                                "<label for=\"all_doctor_filter\">Show All</label>");
                select_all_doctor.InnerHtml = form_filter.ToString();

                /*---------------------------------------- Filter OPD Doctor ------------------------------------------------*/
                form_filter = new StringBuilder();

                var opdDoctor = tempDataTable.FindAll(x => x.AdmissionType.Equals("OPD") || x.AdmissionType.Equals("ED") || x.AdmissionType.Equals("OTHER"));
                form_filter.Append("<ul style=\"list-style:none; list-style-type:none; padding-left:15px\">");

                hf_all_doctor.Value = "";
                foreach (String data in opdDoctor.DistinctBy(x => x.DoctorName).Select(x => x.DoctorName))
                {
                    string listSelectedForm = "javascript:doctorSelected('" + data.ToString() + "_OPD" + "');";
                    var idInput = data.ToString() + "_OPD";
                    var found = formFlagDoctor.Find(x => x == idInput);

                    //if (selected_form_filter.Value != "")
                    //    selected_form_filter.Value = selected_form_filter.Value + "," + idInput;
                    //else
                    //    selected_form_filter.Value = idInput;

                    if (found != null)
                        form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" checked =\"checked\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");
                    else
                        form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");

                    //if (hf_all_doctor.Value.Split(';').Length < dtScannedFilter.Count)
                    //{
                        if (hf_all_doctor.Value != "")
                            hf_all_doctor.Value = hf_all_doctor.Value + ";" + idInput;
                        else
                            hf_all_doctor.Value = idInput;
                    //}
                }

                form_filter.Append("</ul>");
                DocAll.Value = (0 + opdDoctor.DistinctBy(x => x.DoctorName).Count()).ToString();

                opd_doctor_filter.InnerHtml = form_filter.ToString();
                /*---------------------------------------- Filter OPD Doctor ------------------------------------------------*/

                /*---------------------------------------- Filter IPD Doctor ------------------------------------------------*/
                form_filter = new StringBuilder();

                var ipdDoctor = tempDataTable.FindAll(x => x.AdmissionType.Equals("IPD"));
                form_filter.Append("<ul style=\"list-style:none; list-style-type:none; padding-left:15px\">");

                foreach (String data in ipdDoctor.DistinctBy(x => x.DoctorName).Select(x => x.DoctorName))
                {
                    string listSelectedForm = "javascript:doctorSelected('" + data.ToString() + "_IPD" + "');";
                    var idInput = data.ToString() + "_IPD";
                    var found = formFlagDoctor.Find(x => x == idInput);

                    //if (selected_form_filter.Value != "")
                    //    selected_form_filter.Value = selected_form_filter.Value + "," + idInput;
                    //else
                    //    selected_form_filter.Value = idInput;

                    if (found != null)
                        form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" checked =\"checked\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");
                    else
                        form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");

                    //if (hf_all_doctor.Value.Split(';').Length < dtScannedFilter.Count)
                    //{
                        if (hf_all_doctor.Value != "")
                            hf_all_doctor.Value = hf_all_doctor.Value + ";" + idInput;
                        else
                            hf_all_doctor.Value = idInput;
                    //}

                }
                form_filter.Append("</ul>");
                DocAll.Value = (Int64.Parse(DocAll.Value) + ipdDoctor.DistinctBy(x => x.DoctorName).Count()).ToString();

                ipd_doctor_filter.InnerHtml = form_filter.ToString();
                /*---------------------------------------- Filter IPD Doctor ------------------------------------------------*/

                /*---------------------------------------- Filter MCU Doctor ------------------------------------------------*/
                form_filter = new StringBuilder();

                var mcuDoctor = tempDataTable.FindAll(x => x.AdmissionType.Equals("MCU"));
                form_filter.Append("<ul style=\"list-style:none; list-style-type:none; padding-left:15px\">");

                foreach (String data in mcuDoctor.DistinctBy(x => x.DoctorName).Select(x => x.DoctorName))
                {
                    string listSelectedForm = "javascript:doctorSelected('" + data.ToString() + "_MCU" + "');";
                    var idInput = data.ToString() + "_MCU";
                    var found = formFlagDoctor.Find(x => x == idInput);

                    //if (selected_form_filter.Value != "")
                    //    selected_form_filter.Value = selected_form_filter.Value + "," + idInput;
                    //else
                    //    selected_form_filter.Value = idInput;

                    if (found != null)
                        form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" checked =\"checked\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");
                    else
                        form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");

                    //if (hf_all_doctor.Value.Split(';').Length < dtScannedFilter.Count)
                    //{
                        if (hf_all_doctor.Value != "")
                            hf_all_doctor.Value = hf_all_doctor.Value + ";" + idInput;
                        else
                            hf_all_doctor.Value = idInput;
                    //}

                }
                form_filter.Append("</ul>");
                DocAll.Value = (Int64.Parse(DocAll.Value) + mcuDoctor.DistinctBy(x => x.DoctorName).Count()).ToString();

                mcu_doctor_filter.InnerHtml = form_filter.ToString();
                /*---------------------------------------- Filter IPD Doctor ------------------------------------------------*/
                /*==================================================== Add Filter for DOCTOR Scanned EMR ====================================================*/

                /*==================================================== Add Filter for FORM ====================================================*/
                opd_scanned_filter.InnerHtml = "";
                ipd_scanned_filter.InnerHtml = "";
                mcu_scanned_filter.InnerHtml = "";
                //List<string> allform = tempDataTable.Select(x => x.FormTypeName).ToList();

                List<string> formFlag = selected_form_filter.Value.Split(',').ToList();
                form_filter = new StringBuilder();

                string selectAll = "javascript:selectAllForm();";
                form_filter.Append("<div class=\"pretty p-icon p-curve\">" +
                                "<input type=\"checkbox\" id=\"all_form_filter\" onclick=\"" + selectAll + "\" />" +
                                "<div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font-size: 12px\" > </label > </div >" +
                                "</div>" +
                                "<label for=\"all_form_filter\">Show All</label>");
                select_all_form.InnerHtml = form_filter.ToString();
                /*---------------------------------------- Filter OPD Form ------------------------------------------------*/
                form_filter = new StringBuilder();

                var opd = tempDataTable.FindAll(x => x.AdmissionType.Equals("OPD") || x.AdmissionType.Equals("ED") || x.AdmissionType.Equals("OTHER"));
                form_filter.Append("<ul style=\"list-style:none; list-style-type:none; padding-left:15px\">");

                hf_all_form.Value = "";
                foreach (String data in opd.DistinctBy(x => x.FormTypeName).Select(x => x.FormTypeName))
                {
                    string listSelectedForm = "javascript:formSelected('" + data.ToString() + "_OPD" + "');";
                    var idInput = data.ToString() + "_OPD";
                    var found = formFlag.Find(x => x == idInput);

                    //if (selected_form_filter.Value != "")
                    //    selected_form_filter.Value = selected_form_filter.Value + "," + idInput;
                    //else
                    //    selected_form_filter.Value = idInput;

                    if (found != null)
                        form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" checked =\"checked\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");
                    else
                        form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");

                    //if (hf_all_form.Value.Split(',').Length < dtScannedFilter.Count)
                    //{
                        if (hf_all_form.Value != "")
                            hf_all_form.Value = hf_all_form.Value + "," + data.ToString() + "_OPD";
                        else
                            hf_all_form.Value = data.ToString() + "_OPD";
                    //}

                }
                form_filter.Append("</ul>");
                conAll.Value = (0 + opd.DistinctBy(x => x.FormTypeName).Count()).ToString();

                opd_scanned_filter.InnerHtml = form_filter.ToString();
                /*---------------------------------------- Filter OPD Form ------------------------------------------------*/


                /*---------------------------------------- Filter IPD Form ------------------------------------------------*/
                form_filter = new StringBuilder();
                var ipd = tempDataTable.FindAll(x => x.AdmissionType.Equals("IPD"));
                form_filter.Append("<ul style=\"list-style:none; list-style-type:none; padding-left:15px\">");

                foreach (String data in ipd.DistinctBy(x => x.FormTypeName).Select(x => x.FormTypeName))
                {
                    string listSelectedForm = "javascript:formSelected('" + data.ToString() + "_IPD" + "')";
                    var idInput = data.ToString() + "_IPD";
                    var found = formFlag.Find(x => x == idInput);

                    //if (selected_form_filter.Value != "")
                    //    selected_form_filter.Value = selected_form_filter.Value + "," + idInput;
                    //else
                    //    selected_form_filter.Value = idInput;

                    if (found != null)
                        form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" checked =\"checked\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");
                    else
                        form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");

                    //if (hf_all_form.Value.Split(',').Length < dtScannedFilter.Count)
                    //{
                        if (hf_all_form.Value != "")
                            hf_all_form.Value = hf_all_form.Value + "," + data.ToString() + "_IPD";
                        else
                            hf_all_form.Value = data.ToString() + "_IPD";
                    //}

                }
                form_filter.Append("</ul>");
                conAll.Value = (Int64.Parse(conAll.Value) + ipd.DistinctBy(x => x.FormTypeName).Count()).ToString();

                ipd_scanned_filter.InnerHtml = form_filter.ToString();
                /*---------------------------------------- Filter OPD Form ------------------------------------------------*/


                /*---------------------------------------- Filter MCU Form ------------------------------------------------*/
                form_filter = new StringBuilder();
                var mcu = tempDataTable.FindAll(x => x.AdmissionType.Equals("MCU"));
                form_filter.Append("<ul style=\"list-style:none; list-style-type:none; padding-left:15px\">");

                foreach (String data in mcu.DistinctBy(x => x.FormTypeName).Select(x => x.FormTypeName))
                {
                    string listSelectedForm = "javascript:formSelected('" + data.ToString() + "_MCU" + "')";
                    var idInput = data.ToString() + "_MCU";
                    var found = formFlag.Find(x => x == idInput);

                    //if (selected_form_filter.Value != "")
                    //    selected_form_filter.Value = selected_form_filter.Value + "," + idInput;
                    //else
                    //    selected_form_filter.Value = idInput;

                    if (found != null)
                        form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" checked =\"checked\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");
                    else
                        form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");

                    //if (hf_all_form.Value.Split(',').Length < dtScannedFilter.Count)
                    //{
                        if (hf_all_form.Value != "")
                            hf_all_form.Value = hf_all_form.Value + "," + data.ToString() + "_MCU";
                        else
                            hf_all_form.Value = data.ToString() + "_MCU";
                    //}
                }

                form_filter.Append("</ul>");

                conAll.Value = (Int64.Parse(conAll.Value) + mcu.DistinctBy(x => x.FormTypeName).Count()).ToString();

                mcu_scanned_filter.InnerHtml = form_filter.ToString();
                /*---------------------------------------- Filter MCU Form ------------------------------------------------*/
                /*==================================================== Add Filter for FORM ====================================================*/
            //}
            loadScannedMR(dtScannedFilter);
        }
        else
        {
            form_filter.Append("No Data");
            opd_data.InnerHtml = form_filter.ToString();
            ipd_data.InnerHtml = form_filter.ToString();
            mcu_data.InnerHtml = form_filter.ToString();
        }
    }

    void loadScannedMR(List<ScannedMR> dataScan) {
        
        StringBuilder scannedMRInner = new StringBuilder();
        //var dataScanned = dataScan.DistinctBy(a => new { a.AdmissionDate, a.FormTypeName}).ToList();

        /* ----------------------------------------------- Outpatient / Emergency ----------------------------------------------- */
        List<ScannedMR> opd = dataScan.FindAll(x => x.AdmissionType == "OPD" || x.AdmissionType == "ED" || x.AdmissionType == "OTHER").OrderByDescending(x => x.AdmissionDate).ToList();
        var dateAdmission = new DateTime();
        //selected_form_filter.Value = "";
        var localIP = GetLocalIPAddress();
        string imagePath = ResolveClientUrl("~/Images/PatientHistory/ic_newtab_blue.svg");

        if (opd.Count != 0)
        {
            scannedMRInner.Append("<div><table class=\"table table-striped table-condensed\">");
            foreach (ScannedMR data in opd)
            {
                if (data.AdmissionDate != "")
                {

                    string link = "http://" + data.Path.Replace("\\", "/").Replace(" ", "%20");
                    if (DateTime.Parse(data.AdmissionDate) == dateAdmission)
                    {
                        scannedMRInner.Append("<tr>" +
                                        "<td style=\"width:15%;\"></td>" +
                                        "<td style=\"color: blue;height: 25px; width:35%;\"><b>" + data.DoctorName + "</b></td>" +
                                        "<td style=\"color: blue;height: 25px; width:50%;\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline;\" target=\"_blank\"><span><img src=\""+ imagePath + "\" /></span><b>" + data.FormTypeName + "</b></a></td>" +
                                        "</tr>");
                    }
                    else
                    {
                        scannedMRInner.Append("<tr>" +
                                        "<td style=\"color: blue;height: 25px; width:15%;\">" + DateTime.Parse(data.AdmissionDate).ToString("dd MMM yyyy") + "</td>" +
                                        "<td style=\"color: blue;height: 25px; width:35%;\"><b>" + data.DoctorName + "</b></td>" +
                                        "<td style=\"color: blue;height: 25px; width:50%;\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline;\" target=\"_blank\"><span><img src=\"" + imagePath + "\" /></span><b>" + data.FormTypeName + "</b></a></td>" +
                                        "</tr>");
                    }
                    dateAdmission = DateTime.Parse(data.AdmissionDate);
                }
            }
            scannedMRInner.Append("</table></div>");
            opd_data.InnerHtml = scannedMRInner.ToString();
        }
        else
        {
            scannedMRInner.Append("No Data");
            opd_data.InnerHtml = scannedMRInner.ToString();
        }
        
        /* ----------------------------------------------- Outpatient / Emergency ----------------------------------------------- */

        /* ----------------------------------------------- Inpatient / Emergency ----------------------------------------------- */
        scannedMRInner = new StringBuilder();
        List<ScannedMR> ipd = dataScan.FindAll(x => x.AdmissionType == "IPD").OrderByDescending(x => x.AdmissionDate).ToList();
        if (ipd.Count != 0)
        {
            scannedMRInner.Append("<div><table class=\"table table-striped table-condensed\">");
            dateAdmission = new DateTime();
            foreach (ScannedMR data in ipd)
            {
                if (data.AdmissionDate != "")
                {
                    string link = "http://" + data.Path.Replace("\\", "/").Replace(" ", "%20");
                    if (DateTime.Parse(data.AdmissionDate) == dateAdmission)
                    {
                        scannedMRInner.Append("<tr>" +
                                        "<td style=\"width:15%;\"></td>" +
                                        "<td style=\"color: blue;height: 25px; width:35%;\"><b>" + data.DoctorName + "</b></td>" +
                                        "<td style=\"color: blue;height: 25px; width:50%;\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline;\" target=\"_blank\"><span><img src=\"" + imagePath + "\" /></span><b>" + data.FormTypeName + "</b></a></td>" +
                                        "</tr>");
                    }
                    else
                    {
                        scannedMRInner.Append("<tr>" +
                                        "<td style=\"color: blue;height: 25px; width:15%;\">" + DateTime.Parse(data.AdmissionDate).ToString("dd MMM yyyy") + "</td>" +
                                        "<td style=\"color: blue;height: 25px; width:35%;\"><b>" + data.DoctorName + "</b></td>" +
                                        "<td style=\"color: blue;height: 25px; width:50%;\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline;\" target=\"_blank\"><span><img src=\"" + imagePath + "\" /></span><b>" + data.FormTypeName + "</b></a></td>" +
                                        "</tr>");
                    }
                    dateAdmission = DateTime.Parse(data.AdmissionDate);
                }
            }
            scannedMRInner.Append("</table></div>");
            ipd_data.InnerHtml = scannedMRInner.ToString();
        }
        else
        {
            scannedMRInner.Append("No Data");
            ipd_data.InnerHtml = scannedMRInner.ToString();
        }
        /* ----------------------------------------------- Inpatient / Emergency ----------------------------------------------- */

        /* ----------------------------------------------- Medical Check Up / Emergency ----------------------------------------------- */
        scannedMRInner = new StringBuilder();
        List<ScannedMR> mcu = dataScan.FindAll(x => x.AdmissionType == "MCU").OrderByDescending(x => x.AdmissionDate).ToList();
        if (mcu.Count != 0)
        {
            scannedMRInner.Append("<div><table class=\"table table-striped table-condensed\">");
            dateAdmission = new DateTime();
            foreach (ScannedMR data in mcu)
            {
                if (data.AdmissionDate != "")
                {
                    string link = "http://" + data.Path.Replace("\\", "/").Replace(" ", "%20");
                    if (DateTime.Parse(data.AdmissionDate) == dateAdmission)
                    {
                        scannedMRInner.Append("<tr>" +
                                        "<td style=\"width:15%;\"></td>" +
                                        "<td style=\"color: blue;height: 25px; width:35%;\"><b>" + data.DoctorName + "</b></td>" +
                                        "<td style=\"color: blue;height: 25px; width:50%;\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline;\" target=\"_blank\"><span><img src=\"" + imagePath + "\" /></span><b>" + data.FormTypeName + "</b></a></td>" +
                                        "</tr>");
                    }
                    else
                    {
                        scannedMRInner.Append("<tr>" +
                                        "<td style=\"color: blue;height: 25px; width:15%;\">" + DateTime.Parse(data.AdmissionDate).ToString("dd MMM yyyy") + "</td>" +
                                        "<td style=\"color: blue;height: 25px; width:35%;\"><b>" + data.DoctorName + "</b></td>" +
                                        "<td style=\"color: blue;height: 25px; width:50%;\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline;\" target=\"_blank\"><span><img src=\"" + imagePath + "\" /></span><b>" + data.FormTypeName + "</b></a></td>" +
                                        "</tr>");
                    }
                    dateAdmission = DateTime.Parse(data.AdmissionDate);
                }
            }
            scannedMRInner.Append("</table></div>");
            mcu_data.InnerHtml = scannedMRInner.ToString();
        }
        else
        {
            scannedMRInner.Append("No Data");
            mcu_data.InnerHtml = scannedMRInner.ToString();
        }
        /* ----------------------------------------------- Medical Check Up / Emergency ----------------------------------------------- */
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); 
        //Log.Info(LogConfig.LogStart());
        var encounterData = (List<PatientHistoryEncounter>)Session[Helper.ViewStateEncounterData];
        string dtend = Request.Form[DateTextboxEnd_emr.UniqueID];
        DateTextboxEnd_emr.Text = dtend;
        dtend = Request.Form[DateTextboxStart_emr.UniqueID];
        DateTextboxStart_emr.Text = dtend;
        List<PatientHistoryEncounter> dataFilter = new List<PatientHistoryEncounter>();
        StringBuilder patientHistory = new StringBuilder();

        #region
        //------------------------------------------------------------ Filter by Date -----------------------------------------------------------------------------------------

        if (DateTextboxStart_emr.Text != "")
        {
            dataFilter = encounterData.FindAll(x => DateTime.Parse(x.admissionDate.ToString()) >= DateTime.Parse(DateTextboxStart_emr.Text.ToString()));
            var temp = dataFilter;
            if (DateTextboxEnd_emr.Text != "")
            {
                dataFilter = temp.FindAll(x => DateTime.Parse(x.admissionDate.ToString()) <= DateTime.Parse(DateTextboxEnd_emr.Text.ToString()));
            }    
        }
        else
        {
            if (DateTextboxEnd_emr.Text != "")
            {
                dataFilter = encounterData.FindAll(x => DateTime.Parse(x.admissionDate.ToString()) <= DateTime.Parse(DateTextboxEnd_emr.Text));
            }
            else
            {
                dataFilter = encounterData;
            }
        }
        //------------------------------------------------------------ Filter by Date -----------------------------------------------------------------------------------------
        #endregion

        Session[Helper.ViewStatePageData] = dataFilter;
        hf_load_encounter.Value = "0";
        btn_load_more.Style.Add("display", "none");

        if (dataFilter.Count != 0)
        {
            tblPatientHistory.Visible = true;
            img_noData_emr.Style.Add("display", "none");

            patientHistory = loadDataPatientHistory(dataFilter[0].organizationId, dataFilter[0].patientId, dataFilter[0].admissionId, dataFilter[0].encounterId.ToString());
            Session[Helper.ViewStatePatientHistoryInner] = patientHistory;
            tblPatientHistory.InnerHtml = patientHistory.ToString();


            if (dataFilter.Count == 1)
            {
                btn_load_more.Style.Add("display", "none");
                status_dataEmr.Value = "";
            }
            else
            {
                btn_load_more.Style.Add("display", "");
                status_dataEmr.Value = "LOAD MORE";
            }
        }
        else
        {
            patientHistory.Append("<div style=\"background-color:transparant; padding-left: 20px;padding-right: 20px;padding-top: 20px;padding-bottom: 20px; \"></div>");
            tblPatientHistory.InnerHtml = patientHistory.ToString();
            tblPatientHistory.Visible = false;
            img_noData_emr.Style.Add("display", "");
            status_dataEmr.Value = "empty";
        }
        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value, "btn_search_Click", StartTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btn_search_hopeEmr_Click(object sender, EventArgs e)
    {
        getHopeEmrData();
    }

    protected void src_doctorName_hopeEmr_TextChanged(object sender, EventArgs e)
    { 
        //List<PatientHistoryHOPEemr> dataHopeEMR = (List<PatientHistoryHOPEemr>)Session[Helper.ViewStatePatientHistoryHOPEemr];
        //StringBuilder innerHtmlHopeEmr = new StringBuilder();

        //if (src_doctorName_hopeEmr.Text != "")
        //{
        //    List<PatientHistoryHOPEemr> dataFilterHopeEmr = dataHopeEMR.Where(x => x.entryUser.ToUpper().Contains(src_doctorName_hopeEmr.Text.ToUpper())).ToList();
        //    innerHtmlHopeEmr = loadDataHopeEmr(dataFilterHopeEmr);
        //}
        //else
        //{
        //    innerHtmlHopeEmr = loadDataHopeEmr(dataHopeEMR);
        //}

        //hope__emr.InnerHtml = innerHtmlHopeEmr.ToString();
    }

    protected void btn_search_other_unit_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); 
        //Log.Info(LogConfig.LogStart());
        List<OtherUnitMR> otherUnitDataSession = (List<OtherUnitMR>)Session[Helper.ViewStateOtherUnitMR];

        if (otherUnitDataSession != null)
        {
            List<OtherUnitMR> OtherUnitData = new List<OtherUnitMR>();
            if (DateTextboxStart_other.Text != "" || DateTextboxEnd_other.Text != "")
            {
                var tempOtherUnitData = new List<OtherUnitMR>();

                tempOtherUnitData = otherUnitDataSession.FindAll(x => x.AdmissionDate >= DateTime.Parse(DateTextboxStart_other.Text.ToString()));
                OtherUnitData = tempOtherUnitData;

                if (DateTextboxEnd_other.Text != "")
                {
                    OtherUnitData = tempOtherUnitData.FindAll(x => x.AdmissionDate <= DateTime.Parse(DateTextboxEnd_other.Text.ToString()).AddDays(1));
                }
            }
            else
            {
                OtherUnitData = otherUnitDataSession;
            }

            if (ddl_unit_other_mr.SelectedValue != "")
            {
                var temp = OtherUnitData;
                OtherUnitData = new List<OtherUnitMR>();
                OtherUnitData.AddRange(temp.FindAll(x => x.OrganizationCode.Equals(ddl_unit_other_mr.SelectedValue)));
            }

            StringBuilder otherUnitInnerHTML = new StringBuilder();
            if (OtherUnitData.Count != 0)
            {
                other_unit_emr_data.Visible = true;
                otherUnitInnerHTML = loadOtherUnitEMR(OtherUnitData);
                other_unit_emr_data.InnerHtml = otherUnitInnerHTML.ToString();
                status_other_unit_Emr.Value = "Not empty";
                img_noData_other_mr.Style.Add("display", "none");
            }
            else
            {
                other_unit_emr_data.Visible = false;
                status_other_unit_Emr.Value = "empty";
                img_noData_other_mr.Style.Add("display", "");
            }
        }
        else
        {
            status_other_unit_Emr.Value = "empty";
            img_noData_other_mr.Style.Add("display", "");
        }
        List<string> unit_filter = new List<string>();
        unit_filter.AddRange(otherUnitDataSession.Select(x => x.OrganizationCode).Distinct());
        //var firstitem = ddl_unit_other_mr.Items[0];
        ddl_unit_other_mr.Items.Clear();
        //ddl_unit_other_mr.Items.Add(firstitem);

        foreach (string data in unit_filter)
        {
            ddl_unit_other_mr.Items.Insert(unit_filter.IndexOf(data), new ListItem { Text = data, Value = data });
        }

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value, "btn_search_other_unit_Click", StartTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btn_scanned_mr_Click(object sender, EventArgs e)
    {
        fillFilterFormScannedMR();
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "chechkAllFormType", "chechkAllFormType();", true); 
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "chechkAllDoctor", "chechkAllDoctor();", true);
    }

    protected void ddl_year_other_mr_TextChanged(object sender, EventArgs e)
    {
        getOtherUnitData(int.Parse(ddl_year_other_mr.SelectedValue));
    }

    //protected void txt_link_Click(object sender, EventArgs e)
    //{
    //string pdfPath = "\\10.85.129.54\d\SHARE\ArcMr\00020030\00020030;OPD;CATATAN PERKEMBANGAN TERINTEGRASI;20180904;.pdf";
    //Page.ClientScript.RegisterStartupScript(this.GetType(),"OpenWin", "<script type=text/javascript>window.open('" + pdfPath + "')</script>");   
    //}

    protected void LB_hopeEMR_Click(object sender, EventArgs e)
    {
        //if (Session[Helper.ViewStatePatientHistoryHOPEemr] == null)
        //{
            getHopeEmrData();
        //}
    }

    protected void LB_scannedEMR_Click(object sender, EventArgs e)
    {
        //if (Session[Helper.ViewStateScannedData] == null)
        //{
            getScannedData();
        //}
    }

    protected void LB_otherEMR_Click(object sender, EventArgs e)
    {
        //if (Session[Helper.ViewStateOtherUnitMR] == null)
        //{
            getOtherUnitData(2);
        //}
    }
}

