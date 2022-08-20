using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static PatientHistory;
using static PatientReferralModel;
using log4net;
using System.Web.Script.Serialization;
using static SOAPRevisionHistory;
using System.Configuration;
using System.Web;

public partial class Form_General_PatientDashboard : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public string setENG = "none";
    public string setIND = "none";
    public string isBahasa = "";

    public int flag_responsive = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //
       
       

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
            try
            {
                flag_responsive = 0;

                if (Session[Helper.SESSIONmarker] == null)
                {
                    Session[Helper.SESSIONmarker] = Helper.setInitializeMarker();
                }

                HyperLink test = Master.FindControl("PatientDashboardLink") as HyperLink;
                test.Style.Add("background-color", "#D6DBFF");

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
                if (Helper.GetDoctorID(this) == "")
                {
                    Response.Redirect("~/Form/General/Login.aspx", true);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    //Log.Info(LogConfig.LogStart());

                    hfPatientId.Value = Request.QueryString["idPatient"];
                    hfEncounterId.Value = Request.QueryString["EncounterId"];
                    hfAdmissionId.Value = Request.QueryString["AdmissionId"];
                    hfPageSoapId.Value = Request.QueryString["PageSoapId"];


                    getHeader();

                    //Link Binder
                    //Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], "", Request.QueryString["PageSoapId"]);
                    if (Request.QueryString["IsTele"] == "True")
                    {
                        Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], "", Request.QueryString["PageSoapId"], Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
                    }
                    else
                    {
                        Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], "", hfPageSoapIdHeader.Value, Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
                    }

                    lblYear.Text = DateTime.Now.Year.ToString();
                    btnNext.Enabled = false;
                    btnNext.Style.Add("cursor", "not-allowed");

                    getAdmissionHistory(long.Parse(hfPatientId.Value));
                    getPatientData(long.Parse(Helper.GetDoctorID(this)), long.Parse(hfPatientId.Value), long.Parse(hfAdmissionId.Value), hfEncounterId.Value);
                    getPatientHistorySOAP(long.Parse(hfPatientId.Value));
                    //responsivedivempty();

                    List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                    markerlist.Find(x => x.key == "DOBmarker").value = "marked";
                    Session[Helper.SESSIONmarker] = markerlist;

                    //zoom link
                    var zl = Request.QueryString["ZoomLink"];
                    if (zl != null && zl != "null")
                    {
                        Session[CstSession.SessionZoomLink] = zl;
                    }

                    //ClickPatient();
                    Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value, "Page_Load", StartTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
                    //Log.Info(LogConfig.LogEnd());

                }
            }
            catch (Exception ex)
            {
                Session[CstSession.sessionerror] = ex;
                throw ex;
            }
        }
    }

    void getPatientHistorySOAP(long PatientId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        //Log.Debug(LogConfig.LogStart("getPatientHistorySOAP", LogConfig.LogParam("Patient_ID", PatientId.ToString())));
        var varResult = clsPatientDetail.getPatientHistorySOAP(Helper.organizationId, PatientId);
        var JsonResult = JsonConvert.DeserializeObject<ResultPatientHistoryLite>(varResult.Result.ToString());

        List<PatientHistoryLite> patienthistory = JsonResult.list;

        string labInactive = ResolveClientUrl("~/Images/Icon/ic_Lab_NotActive.svg");
        string labActive = ResolveClientUrl("~/Images/Icon/ic_Lab.svg");
        string radInactive = ResolveClientUrl("~/Images/Icon/ic_Rad_NotActive.svg");
        string radActive = ResolveClientUrl("~/Images/Icon/ic_Rad.svg");
        string patientInactive = ResolveClientUrl("~/Images/Icon/ic_History_NotActive.svg");
        string patientActive = ResolveClientUrl("~/Images/Icon/ic_History.svg");

        StringBuilder innerPatientHistorySOAP = new StringBuilder();
        string modallabresult = "";
        string modalradresult = "";
        string modalphresult = "";

        DataTable dt = Helper.ToDataTable(patienthistory);
        gvw_latestsoap.DataSource = dt;
        gvw_latestsoap.DataBind();

        if (patienthistory.Count != 0)
        {
            if (isBahasa == "ENG")
            {
                innerPatientHistorySOAP.Append("<div style=\"padding-left:0px; padding-right:0px;\"><div id=\"header_SOAP\" class=\"row table-sub-header-label\" style=\"border: 1px solid #f2f3f4; font-size: 14px; font-family: Helvetica;\">" +
                                                "<div id=\"date_header\" class=\"col-sm-2\" style=\"width:11%;\"><b>Date</b></div>" +
                                                "<div id=\"doctor_header\" class=\"col-sm-2\" style=\"width:11%;\"><b>Doctor</b></div>" +
                                                "<div id=\"header_s\" class=\"col-sm-1\" style=\"width:13%;\"><b>S</b></div>" +
                                                "<div id=\"header_o\" class=\"col-sm-1\" style=\"width:13%;\"><b>O</b></div>" +
                                                "<div id=\"header_a\" class=\"col-sm-1\" style=\"width:13%;\"><b>A</b></div>" +
                                                "<div id=\"header_p\" class=\"col-sm-1\" style=\"width:13%;\"><b>P</b></div>" +
                                                "<div id=\"header_resep\" class=\"col-sm-3\" style=\"width:26%;\"><b>Prescription</b></div>" +
                                            "</div> </div>");

                modallabresult = "Laboratory Result";
                modalradresult = "Radiology Result";
                modalphresult = "Patient History";
            }
            else if (isBahasa == "IND")
            {
                innerPatientHistorySOAP.Append("<div class=\"col-sm-12\" style=\"padding-left:0px; padding-right:0px;\"><div id=\"header_SOAP\" class=\"row table-sub-header-label\" style=\"border: 1px solid #f2f3f4; font-size: 14px; font-family: Helvetica;\">" +
                                                "<div id=\"date_header\" class=\"col-sm-2\" style=\"width:11%;\"><b>Tanggal</b></div>" +
                                                "<div id=\"doctor_header\" class=\"col-sm-2\" style=\"width:11%;\"><b>Dokter</b></div>" +
                                                "<div id=\"header_s\" class=\"col-sm-1\" style=\"width:13%;\"><b>S</b></div>" +
                                                "<div id=\"header_o\" class=\"col-sm-1\" style=\"width:13%;\"><b>O</b></div>" +
                                                "<div id=\"header_a\" class=\"col-sm-1\" style=\"width:13%;\"><b>A</b></div>" +
                                                "<div id=\"header_p\" class=\"col-sm-1\" style=\"width:13%;\"><b>P</b></div>" +
                                                "<div id=\"header_resep\" class=\"col-sm-3\" style=\"width:26%;\"><b>Resep</b></div>" +
                                            "</div> </div>");

                modallabresult = "Hasil Laboratorium";
                modalradresult = "Hasil Radiologi";
                modalphresult = "Riwayat Pasien";
            }

            //if (isBahasa == "ENG")
            //{
            //    innerPatientHistorySOAP.Append("<table style=\"padding-left:0px; padding-right:0px; border-color:#b9b9b9;\" border=\"1\" class=\"table-condensed table-fill-width\"><tr id=\"header_SOAP\" class=\"table-sub-header-label\" style=\"border: 1px solid #f2f3f4; font-size: 14px; font-family: Helvetica;\">" +
            //                                    "<td id=\"date_header\" style=\"width:11%;\"><b>Date</b></td>" +
            //                                    "<td id=\"doctor_header\" style=\"width:11%;\"><b>Doctor</b></td>" +
            //                                    "<td id=\"header_s\" style=\"width:13%;\"><b>S</b></td>" +
            //                                    "<td id=\"header_o\" style=\"width:13%;\"><b>O</b></td>" +
            //                                    "<td id=\"header_a\" style=\"width:13%;\"><b>A</b></td>" +
            //                                    "<td id=\"header_p\" style=\"width:13%;\"><b>P</b></td>" +
            //                                    "<td id=\"header_resep\" style=\"width:26%;\"><b>Prescription</b></td>" +
            //                                "</tr> </table>");

            //    modallabresult = "Laboratory Result";
            //    modalradresult = "Radiology Result";
            //    modalphresult = "Patient History";
            //}
            //else if (isBahasa == "IND")
            //{
            //    innerPatientHistorySOAP.Append("<div class=\"col-sm-12\" style=\"padding-left:0px; padding-right:0px;\"><div id=\"header_SOAP\" class=\"row table-sub-header-label\" style=\"border: 1px solid #f2f3f4; font-size: 14px; font-family: Helvetica;\">" +
            //                                    "<div id=\"date_header\" class=\"col-sm-2\" style=\"width:11%;\"><b>Tanggal</b></div>" +
            //                                    "<div id=\"doctor_header\" class=\"col-sm-2\" style=\"width:11%;\"><b>Dokter</b></div>" +
            //                                    "<div id=\"header_s\" class=\"col-sm-1\" style=\"width:13%;\"><b>S</b></div>" +
            //                                    "<div id=\"header_o\" class=\"col-sm-1\" style=\"width:13%;\"><b>O</b></div>" +
            //                                    "<div id=\"header_a\" class=\"col-sm-1\" style=\"width:13%;\"><b>A</b></div>" +
            //                                    "<div id=\"header_p\" class=\"col-sm-1\" style=\"width:13%;\"><b>P</b></div>" +
            //                                    "<div id=\"header_resep\" class=\"col-sm-3\" style=\"width:26%;\"><b>Resep</b></div>" +
            //                                "</div> </div>");

            //    modallabresult = "Hasil Laboratorium";
            //    modalradresult = "Hasil Radiologi";
            //    modalphresult = "Riwayat Pasien";
            //}

            innerPatientHistorySOAP.Append("<div class=\"col-sm-12\" style=\"padding-left:0px; padding-right:0px;\">");

            foreach (PatientHistoryLite data in patienthistory)
            {
                string linkLaboratory = "javascript:Lab(" + data.AdmissionId + ")";
                string linkRadiology = "javascript:Rad(" + data.AdmissionId + ")";
                string linkPatientHistory = "javascript:patientHistory(" + data.PatientId + "," + data.OrganizationId + ", " + data.AdmissionId + ", \"" + data.EncounterId + "\")";

                innerPatientHistorySOAP.Append("<div class=\"row\"> <div style=\"padding : 5px 0px 5px 0px; border: 1px solid #ddd; /*max-height: 97px;overflow-y: auto;*/ font-family: Helvetica; font-size: 12px;\" class=\"col-sm-12\">" +
                                                    "<div class=\"col-sm-2\" style=\"width:11%;\">" +
                                                        "<div style=\"font-size: 16px;\"><b>" + data.AdmissionDate + "</b></div>" +
                                                        "<div class=\"font-content-dashboard\">" + data.AdmissionNo + "</div><div>");

                if (data.IsLab == "1")
                {
                    innerPatientHistorySOAP.Append("<a target=\"_blank\" title=\"" + modallabresult + "\" href=\'" + linkLaboratory + "\'  style=\"color: blue; margin-right:5px; text-decoration:underline; \"><span><img src=\"" + labActive + "\" /></span></a>");
                }
                else if (data.IsLab != "1")
                {
                    innerPatientHistorySOAP.Append("<span style=\"margin-right:5px;\"><img src=\"" + labInactive + "\" title=\"" + modallabresult + "\"  /></span>");
                }

                if (data.IsRad == "1")
                {
                    innerPatientHistorySOAP.Append("<a target=\"_blank\" title=\"" + modalradresult + "\" href=\'" + linkRadiology + "\'  style=\"color: blue; margin-right:5px; text-decoration:underline; \"><span><img src=\"" + radActive + "\" /></span></a>");
                }
                else if (data.IsRad != "1")
                {
                    innerPatientHistorySOAP.Append("<span style=\"margin-right:5px;\"><img src=\"" + radInactive + "\" title=\"" + modalradresult + "\"  /></span>");
                }

                innerPatientHistorySOAP.Append("<a target=\"_blank\" title=\"" + modalphresult + "\" href=\'" + linkPatientHistory + "\'  style=\"color: blue; text-decoration:underline; \"><span><img src=\"" + patientActive + "\" /></span></a>");
                innerPatientHistorySOAP.Append("</div></div>" +
                                                    "<div class=\"col-sm-2 font-content-dashboard\" style=\"width:11%;\">" + data.DoctorName + "</div>");

                var subjectPatient = data.Subjective.Replace("\\n", "\n").Split('\n');
                innerPatientHistorySOAP.Append("<div style=\"overflow-wrap:break-word; width:13%;\" class=\"col-sm-1\">");
                for (int i = 0; i < subjectPatient.Length; i++)
                {
                    innerPatientHistorySOAP.Append(subjectPatient[i] + "<br />");
                }
                innerPatientHistorySOAP.Append("</div>");

                //"<div class=\"col-sm-1\">" + data.Subjective.Replace("\\n","<br />") + "</div>" +
                innerPatientHistorySOAP.Append("<div style=\"overflow-wrap:break-word; width:13%;\" class=\"col-sm-1 font-content-dashboard\">" + data.Objective + "</div>" +
                                               "<div style=\"overflow-wrap:break-word; width:13%;\" class=\"col-sm-1 font-content-dashboard\">" + data.Diagnosis + "</div>" +
                                               "<div style=\"overflow-wrap:break-word; width:13%;\" class=\"col-sm-1 font-content-dashboard\">" + data.PlanningProcedure + "</div>" +
                                               "<div class=\"col-sm-3 font-content-dashboard\" style=\"width:26%\">" + data.Prescription.Replace("\\n", "<br />") + "</div>" +
                                               //"<div class=\"col-sm-3 poppop\" style=\"width:26%\" data-content=\"" + data.Prescription.Replace("\\n", "<br />") + "\" rel=\"popover\" data-placement=\"left\" data-container=\"body\" data-html=\"true\" data-original-title=\"Detail Prescription\" data-trigger=\"click\">" + data.Prescription.Replace("\\n", "<br />") + "</div>" +
                                               "</div></div>");
            }

            innerPatientHistorySOAP.Append("</div>");

            divkosong_latestsoap.Style.Add("display", "none");
            divisi_latestsoap.Style.Add("display", "");
        }
        else
        {
            //innerPatientHistorySOAP.Append("<label>No Data</label>");
            divNoSoap.Style.Add("display", "");

            divkosong_latestsoap.Style.Add("display", "");
            divisi_latestsoap.Style.Add("display", "none");
        }

        SOAP_patientHistory.InnerHtml = innerPatientHistorySOAP.ToString();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", PatientId.ToString(), "getPatientHistorySOAP", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", varResult.Result.ToString()));

    }

    void getAdmissionHistory(long PatientId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); 
        //Log.Info(LogConfig.LogStart());

        try
        {
            divContentReport.InnerHtml = "";

            long DoctorId = long.Parse(Helper.GetDoctorID(this));
            string Year = lblYear.Text.ToString();

            StringBuilder result = new StringBuilder();
            //Generate Header
            if (isBahasa == "ENG")
            {
                result.Append("<table border=\"1\" style=\"width:100%;border-color:#b9b9b9;border-style:solid; border-top: none;\" class=\"font-content-dashboard\"><tr class=\"table-sub-header-label\" style=\"height:32px;\"><td style=\"width:100px\"><b>Specialities</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>January</b></td><td style=\"width:100px\"colspan=\"4\"><b>February</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>March</b></td><td style=\"width:100px\" colspan=\"4\"><b>April</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>May</b></td><td style=\"width:100px\" colspan=\"4\"><b>June</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>July</b></td><td style=\"width:100px\" colspan=\"4\"><b>August</b></td><td style=\"width:100px\" colspan = \"4\">" +
                              "<b>September</b></td><td style=\"width:100px\" colspan=\"4\"><b>October</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>November</b></td><td style=\"width:100px\" colspan=\"4\"><b>December</b></td></tr>");
            }
            else if (isBahasa == "IND")
            {
                result.Append("<table border=\"1\" style=\"width:100%;border-color:#b9b9b9;border-style:solid; border-top: none;\" class=\"font-content-dashboard\"><tr class=\"table-sub-header-label\" style=\"height:32px;\"><td style=\"width:100px\"><b>Spesialis</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>Januari</b></td><td style=\"width:100px\"colspan=\"4\"><b>Februari</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>Maret</b></td><td style=\"width:100px\" colspan=\"4\"><b>April</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>Mei</b></td><td style=\"width:100px\" colspan=\"4\"><b>Juni</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>Juli</b></td><td style=\"width:100px\" colspan=\"4\"><b>Agustus</b></td><td style=\"width:100px\" colspan = \"4\">" +
                              "<b>September</b></td><td style=\"width:100px\" colspan=\"4\"><b>Oktober</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>November</b></td><td style=\"width:100px\" colspan=\"4\"><b>Desember</b></td></tr>");
            }

            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Patient_ID", PatientId.ToString() },
                { "Doctor_ID", DoctorId.ToString() },
                { "Year", Year }
            };
            //Log.Debug(LogConfig.LogStart("GetAdmissionHistory", logParam));
            var varResult = clsPatientDetail.GetAdmissionHistory(PatientId, DoctorId, int.Parse(Year));
            var JsongetAdmissionHistory = JsonConvert.DeserializeObject<ResponseAdmissionHistory>(varResult.Result.ToString());

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", PatientId.ToString(), "getAdmissionHistory", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", varResult.Result.ToString()));
            //Log.Debug(LogConfig.LogEnd("GetAdmissionHistory", JsongetAdmissionHistory.Status, JsongetAdmissionHistory.Message));

            List<AdmissionHistory> DataAdmissionHistory = JsongetAdmissionHistory.list;

            if (DataAdmissionHistory != null)
            {
                DataTable Result = new DataTable();
                Result = Helper.ToDataTable(DataAdmissionHistory);

                if (Result.Rows.Count > 0)
                {
                    Labeladmishis.Style.Add("display", "none");

                    DataTable dtOPD = new DataTable();
                    DataTable dtIPD = new DataTable();
                    DataTable dtED = new DataTable();
                    DataTable dtProcedure = new DataTable();

                    if (Result.Select("Type = 'OPD'").Count() > 0)
                    {
                        dtOPD = Result.Select("Type = 'OPD'").CopyToDataTable();
                    }
                    if (Result.Select("Type = 'IPD'").Count() > 0)
                    {
                        dtIPD = Result.Select("Type = 'IPD'").CopyToDataTable();
                    }
                    if (Result.Select("Type = 'ED'").Count() > 0)
                    {
                        dtED = Result.Select("Type = 'ED'").CopyToDataTable();
                    }
                    if (Result.Select("Type = 'PROCEDURE'").Count() > 0)
                    {
                        dtProcedure = Result.Select("Type = 'PROCEDURE'").CopyToDataTable();
                    }

                    #region DUMMY
                    //dtOPD.Columns.Add("Type");
                    //dtOPD.Columns.Add("Specialty");
                    //dtOPD.Columns.Add("AdmissionNo");
                    //dtOPD.Columns.Add("AdmissionDate");
                    //dtOPD.Columns.Add("AdmissionId");
                    //dtOPD.Columns.Add("AdmissionMonth");
                    //dtOPD.Columns.Add("AdmissionWeek");
                    //dtOPD.Columns.Add("LabSalesOrderNo");
                    //dtOPD.Columns.Add("RadSalesOrderNo");
                    //dtOPD.Columns.Add("OrganizationId");
                    //dtOPD.Columns.Add("OrgCd");
                    //dtOPD.Columns.Add("DoctorName");
                    //dtOPD.Columns.Add("isMyself");

                    //dtOPD.Rows.Add(new Object[] { "OPD", "DENTIST", "OPA0000001", "31 January 2018", "12345", "1", "4", "123123", "234234", "2", "Dr.A", "0" });
                    //dtOPD.Rows.Add(new Object[] { "OPD", "DENTIST", "OPA01230001", "1 March 2018", "434", "3", "1", "2342", "123458765", "27", "Dr.ACCC", "0" });
                    //dtOPD.Rows.Add(new Object[] { "OPD", "GENERAL PRACTITIONER", "OPA0003453001", "1 January 2018", "356546", "1", "1", "-", "-", "4", "Dr.WWWW", "0" });
                    //dtOPD.Rows.Add(new Object[] { "OPD", "GENERAL PRACTITIONER", "OPA234450001", "2 December 2018", "353", "12", "1", "-", "-", "21", "Dr.ZZsklvndkjfvndkjfnksjdfksdjfnksjdfnskjdfnksjdfnZZ", "0" });
                    //dtOPD.Rows.Add(new Object[] { "OPD", "MEDICAL REHABILITATION", "OPA0045601", "12 December 2018", "2342344", "12", "2", "-", "-", "3", "Dr.B", "1" });
                    //dtOPD.Rows.Add(new Object[] { "OPD", "MEDICAL REHABILITATION", "OPA00456301", "13 December 2018", "22342344", "12", "2", "-", "23434", "2", "Dr.B", "1" });
                    #endregion

                    if (dtOPD.Rows.Count > 0)
                    {
                        //Generate OPD
                        result.Append("<tr style=\"background-color:#e67d0533\"><td style=\"text-aligh:left\"><b>OPD</b></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td></tr>");

                        DataTable GetSpecialty = dtOPD.DefaultView.ToTable(true, "Specialty");

                        //int numberr = 0;
                        for (int i = 0; i < GetSpecialty.Rows.Count; i++)
                        {
                            result.Append("<tr><td style=\"text-align:left;padding-left:5px; text-transform:capitalize;\">" + GetSpecialty.Rows[i]["Specialty"].ToString().ToLower() + "</td>");

                            DataTable LoopAdmission = dtOPD.Select("Specialty = '" + GetSpecialty.Rows[i]["Specialty"].ToString() + "'").CopyToDataTable();

                            for (int month = 1; month <= 12; month++)
                            {
                                if (LoopAdmission.Select("AdmissionMonth = " + month).Count() > 0)
                                {
                                    for (int week = 1; week <= 4; week++)
                                    {
                                        if (LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).Count() > 0)
                                        {
                                            DataTable LoopContent = LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).CopyToDataTable();

                                            var ListAdmission = "";

                                            int CheckColor = 0;
                                            //numberr++;

                                            for (int loopcontent = 0; loopcontent < LoopContent.Rows.Count; loopcontent++)
                                            {
                                                if (loopcontent != 0)
                                                {
                                                    ListAdmission += "|";
                                                }

                                                if (LoopContent.Rows[loopcontent]["isMyself"].ToString() == "1")
                                                {
                                                    CheckColor = 1;
                                                }

                                                ListAdmission += DateTime.Parse(LoopContent.Rows[loopcontent]["AdmissionDate"].ToString()).ToString("dd MMM yyyy") + "#";

                                                if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() != "-")
                                                {
                                                    ListAdmission += LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() + "#";
                                                }
                                                else if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() == "-")
                                                {
                                                    ListAdmission += "-#";
                                                }
                                                if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() != "-")
                                                {
                                                    ListAdmission += LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() + "#";
                                                }
                                                else if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() == "-")
                                                {
                                                    ListAdmission += "-#";
                                                }
                                                ListAdmission += LoopContent.Rows[loopcontent]["encounterId"].ToString() + "#"
                                                    + LoopContent.Rows[loopcontent]["AdmissionId"].ToString() + "#"
                                                    + LoopContent.Rows[loopcontent]["DoctorName"].ToString() + "#"
                                                    + LoopContent.Rows[loopcontent]["isMyself"].ToString() + "#"
                                                    + LoopContent.Rows[loopcontent]["Diagnosis"].ToString() + "#"
                                                    + LoopContent.Rows[loopcontent]["organizationId"].ToString();
                                            }

                                            string link = "javascript:Open('" + ListAdmission + "')";

                                            if (CheckColor == 0)
                                            {
                                                result.Append("<td style=\"width:20px;background-color:gray\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%; background-color:gray;border:none;margin:0 0 0 0\">&nbsp;</button>  </td>");
                                            }
                                            else
                                            {
                                                result.Append("<td style=\"width:20px;background-color:cornflowerblue\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%; background-color:cornflowerblue;border:none;margin:0 0 0 0\">&nbsp;</button> </td>");
                                            }

                                            //string link = "javascript:Open('" + ListAdmission + "','" + numberr + "')";
                                            //if (CheckColor == 0)
                                            //{
                                            //    result.Append("<td style=\"width:20px;background-color:gray\"><button type=\"button\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:gray;border:none;margin:0 0 0 0\">&nbsp;</button> <button id=\"mybtn" + numberr + "\" type=\"button\"  data-toggle=\"popover-x\" data-target=\"#myPopover" + numberr + "\" data-placement=\"top\">o</button> </td>");
                                            //}
                                            //else
                                            //{
                                            //    result.Append("<td style=\"width:20px;background-color:cornflowerblue\"><button type=\"button\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:cornflowerblue;border:none;margin:0 0 0 0\">&nbsp;</button> <button id=\"mybtn" + numberr + "\" type=\"button\" data-toggle=\"popover-x\" data-target=\"#myPopover" + numberr + "\" data-placement=\"top\">o</button> </td>");
                                            //}
                                        }
                                        else
                                        {
                                            result.Append("<td style=\"width:20px;\"></td>");
                                        }
                                    }
                                }
                                else
                                {
                                    result.Append("<td style=\"width:20px;\"></td>" +
                                                  "<td style=\"width:20px;\"></td>" +
                                                  "<td style=\"width:20px;\"></td>" +
                                                  "<td style=\"width:20px;\"></td>");
                                }
                            }

                            result.Append("</tr>");
                        }
                    }
                    if (dtIPD.Rows.Count > 0)
                    {
                        //Generate IPD
                        result.Append("<tr style=\"background-color:#e67d0533\"><td style=\"text-aligh:left\"><b>IPD</b></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td></tr>");

                        result.Append("<tr><td style=\"text-align:left;padding-left:5px; text-transform:capitalize;\"><b>-</b></td>");

                        DataTable LoopAdmission = dtIPD.Select().CopyToDataTable();

                        for (int month = 1; month <= 12; month++)
                        {
                            if (LoopAdmission.Select("AdmissionMonth = " + month).Count() > 0)
                            {
                                for (int week = 1; week <= 4; week++)
                                {
                                    if (LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).Count() > 0)
                                    {
                                        DataTable LoopContent = LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).CopyToDataTable();

                                        var ListAdmission = "";

                                        int CheckColor = 0;

                                        for (int loopcontent = 0; loopcontent < LoopContent.Rows.Count; loopcontent++)
                                        {
                                            if (loopcontent != 0)
                                            {
                                                ListAdmission += "|";
                                            }

                                            if (LoopContent.Rows[loopcontent]["isMyself"].ToString() == "1")
                                            {
                                                CheckColor = 1;
                                            }

                                            ListAdmission += LoopContent.Rows[loopcontent]["AdmissionDate"].ToString() + "#";

                                            if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() != "-")
                                            {
                                                ListAdmission += LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() + "#";
                                            }
                                            else if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() == "-")
                                            {
                                                ListAdmission += "-#";
                                            }
                                            if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() != "-")
                                            {
                                                ListAdmission += LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() + "#";
                                            }
                                            else if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() == "-")
                                            {
                                                ListAdmission += "-#";
                                            }
                                            ListAdmission += LoopContent.Rows[loopcontent]["encounterId"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["AdmissionId"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["DoctorName"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["isMyself"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["Diagnosis"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["organizationId"].ToString();
                                        }

                                        string link = "javascript:Open('" + ListAdmission + "')";

                                        if (CheckColor == 0)
                                        {
                                            result.Append("<td style=\"width:20px;background-color:gray\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:gray;border:none;margin:0 0 0 0\">&nbsp;</button></td>");
                                        }
                                        else
                                        {
                                            result.Append("<td style=\"width:20px;background-color:cornflowerblue\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:cornflowerblue;border:none;margin:0 0 0 0\">&nbsp;</button></td>");
                                        }
                                    }
                                    else
                                    {
                                        result.Append("<td style=\"width:20px;\"></td>");
                                    }
                                }
                            }
                            else
                            {
                                result.Append("<td style=\"width:20px;\"></td>" +
                                              "<td style=\"width:20px;\"></td>" +
                                              "<td style=\"width:20px;\"></td>" +
                                              "<td style=\"width:20px;\"></td>");
                            }
                        }

                        result.Append("</tr>");

                    }
                    if (dtED.Rows.Count > 0)
                    {
                        //Generate ED
                        result.Append("<tr style=\"background-color:#e67d0533\"><td style=\"text-aligh:left\"><b>ED</b></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td></tr>");

                        result.Append("<tr><td style=\"text-align:left;padding-left:5px; text-transform:capitalize;\"><b>-</b></td>");

                        DataTable LoopAdmission = dtED.Select().CopyToDataTable();

                        for (int month = 1; month <= 12; month++)
                        {
                            if (LoopAdmission.Select("AdmissionMonth = " + month).Count() > 0)
                            {
                                for (int week = 1; week <= 4; week++)
                                {
                                    if (LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).Count() > 0)
                                    {
                                        DataTable LoopContent = LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).CopyToDataTable();

                                        var ListAdmission = "";

                                        int CheckColor = 0;

                                        for (int loopcontent = 0; loopcontent < LoopContent.Rows.Count; loopcontent++)
                                        {
                                            if (loopcontent != 0)
                                            {
                                                ListAdmission += "|";
                                            }

                                            if (LoopContent.Rows[loopcontent]["isMyself"].ToString() == "1")
                                            {
                                                CheckColor = 1;
                                            }

                                            ListAdmission += LoopContent.Rows[loopcontent]["AdmissionDate"].ToString() + "#";

                                            if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() != "-")
                                            {
                                                ListAdmission += LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() + "#";
                                            }
                                            else if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() == "-")
                                            {
                                                ListAdmission += "-#";
                                            }
                                            if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() != "-")
                                            {
                                                ListAdmission += LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() + "#";
                                            }
                                            else if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() == "-")
                                            {
                                                ListAdmission += "-#";
                                            }
                                            ListAdmission += LoopContent.Rows[loopcontent]["encounterId"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["AdmissionId"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["DoctorName"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["isMyself"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["Diagnosis"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["organizationId"].ToString();
                                        }

                                        string link = "javascript:Open('" + ListAdmission + "')";

                                        if (CheckColor == 0)
                                        {
                                            result.Append("<td style=\"width:20px;background-color:gray\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:gray;border:none;margin:0 0 0 0\">&nbsp;</button></td>");
                                        }
                                        else
                                        {
                                            result.Append("<td style=\"width:20px;background-color:cornflowerblue\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:cornflowerblue;border:none;margin:0 0 0 0\">&nbsp;</button></td>");
                                        }
                                    }
                                    else
                                    {
                                        result.Append("<td style=\"width:20px;\"></td>");
                                    }
                                }
                            }
                            else
                            {
                                result.Append("<td style=\"width:20px;\"></td>" +
                                              "<td style=\"width:20px;\"></td>" +
                                              "<td style=\"width:20px;\"></td>" +
                                              "<td style=\"width:20px;\"></td>");
                            }
                        }

                        result.Append("</tr>");
                    }
                    if (dtProcedure.Rows.Count > 0)
                    {
                        //Generate Procedure
                        result.Append("<tr style=\"background-color:#e67d0533\"><td style=\"text-aligh:left\"><b>PROCEDURE</b></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                      "<td style=\"width:100px;\" colspan=\"4\"></td></tr>");

                        DataTable GetSpecialty = dtProcedure.DefaultView.ToTable(true, "Specialty");

                        for (int i = 0; i < GetSpecialty.Rows.Count; i++)
                        {
                            result.Append("<tr><td style=\"text-align:left;padding-left:5px; text-transform:capitalize;\">" + GetSpecialty.Rows[i]["Specialty"].ToString().ToLower() + "</td>");

                            DataTable LoopAdmission = dtProcedure.Select("Specialty = '" + GetSpecialty.Rows[i]["Specialty"].ToString() + "'").CopyToDataTable();

                            for (int month = 1; month <= 12; month++)
                            {
                                if (LoopAdmission.Select("AdmissionMonth = " + month).Count() > 0)
                                {
                                    for (int week = 1; week <= 4; week++)
                                    {
                                        if (LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).Count() > 0)
                                        {
                                            DataTable LoopContent = LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).CopyToDataTable();

                                            var ListAdmission = "";

                                            int CheckColor = 0;

                                            for (int loopcontent = 0; loopcontent < LoopContent.Rows.Count; loopcontent++)
                                            {
                                                if (loopcontent != 0)
                                                {
                                                    ListAdmission += "|";
                                                }

                                                if (LoopContent.Rows[loopcontent]["isMyself"].ToString() == "1")
                                                {
                                                    CheckColor = 1;
                                                }

                                                ListAdmission += LoopContent.Rows[loopcontent]["AdmissionDate"].ToString() + "#";

                                                if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() != "-")
                                                {
                                                    ListAdmission += LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() + "#";
                                                }
                                                else if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() == "-")
                                                {
                                                    ListAdmission += "-#";
                                                }
                                                if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() != "-")
                                                {
                                                    ListAdmission += LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() + "#";
                                                }
                                                else if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() == "-")
                                                {
                                                    ListAdmission += "-#";
                                                }
                                                ListAdmission += LoopContent.Rows[loopcontent]["encounterId"].ToString() + "#"
                                                    + LoopContent.Rows[loopcontent]["AdmissionId"].ToString() + "#"
                                                    + LoopContent.Rows[loopcontent]["DoctorName"].ToString() + "#"
                                                    + LoopContent.Rows[loopcontent]["isMyself"].ToString() + "#"
                                                    + LoopContent.Rows[loopcontent]["Diagnosis"].ToString() + "#"
                                                    + LoopContent.Rows[loopcontent]["organizationId"].ToString();
                                            }

                                            string link = "javascript:Open('" + ListAdmission + "')";

                                            if (CheckColor == 0)
                                            {
                                                result.Append("<td style=\"width:20px;background-color:gray\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:gray;border:none;margin:0 0 0 0\">&nbsp;</button></td>");
                                            }
                                            else
                                            {
                                                result.Append("<td style=\"width:20px;background-color:cornflowerblue\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:cornflowerblue;border:none;margin:0 0 0 0\">&nbsp;</button></td>");
                                            }
                                        }
                                        else
                                        {
                                            result.Append("<td style=\"width:20px;\"></td>");
                                        }
                                    }
                                }
                                else
                                {
                                    result.Append("<td style=\"width:20px;\"></td>" +
                                                  "<td style=\"width:20px;\"></td>" +
                                                  "<td style=\"width:20px;\"></td>" +
                                                  "<td style=\"width:20px;\"></td>");
                                }
                            }

                            result.Append("</tr>");
                        }
                    }
                }
                else
                {
                    Labeladmishis.Style.Add("display", "");
                }
            }
            result.Append("</table>");
            divContentReport.InnerHtml = result.ToString();
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", PatientId.ToString(), "getMedicalHistory", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", PatientId.ToString(), "getAdmissionHistory", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
        }
        //Log.Info(LogConfig.LogEnd());

    }

    void getPatientData(long doctorId, long PatientId, long admissionId, string EncounterId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); 
        //Log.Info(LogConfig.LogStart());

        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Organization_ID", Helper.organizationId.ToString() },
                { "Patient_ID", PatientId.ToString() },
                { "Admission_ID", admissionId.ToString() },
                { "Encounter_ID", EncounterId }
            };
            //Log.Debug(LogConfig.LogStart("GetPatientDashboard", logParam));
            var varResult = clsPatientDetail.GetPatientDashboard(Helper.organizationId, PatientId, admissionId, EncounterId);
            var JsongetMap = JsonConvert.DeserializeObject<ResponsePatientDashboard>(varResult.Result.ToString());

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", PatientId.ToString(), "getPatientData", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", varResult.Result.ToString()));
            //Log.Debug(LogConfig.LogEnd("GetPatientDashboard", JsongetMap.Status, JsongetMap.Message));

            PatientDashboard JsongetPatientHistory = JsongetMap.Data;
            //var objectdata = JsongetMap.Property("data").Value.ToString();
            //PatientDashboard JsongetPatientHistory = JsonConvert.DeserializeObject<PatientDashboard>(objectdata);

            var hasiltindakan = JsongetPatientHistory.proceduresults;
            if (hasiltindakan.Count > 0)
            {
                //DataTable dthasiltindakan = Helper.ToDataTable(hasiltindakan);
                gvw_hasiltindakan.DataSource = hasiltindakan;
                gvw_hasiltindakan.DataBind();
                divriwayattindakan.Style.Add("display", "");

                divkosong_procresult.Style.Add("display", "none");
                divisi_procresult.Style.Add("display", "");
            }
            else
            {
                divnotindakan.Style.Add("display", "");
                divriwayattindakan.Style.Add("display", "none");

                divkosong_procresult.Style.Add("display", "");
                divisi_procresult.Style.Add("display", "none");
                flag_responsive++;
            }

            int flagalergikosong = 0;

            var PatientAllergy = JsongetPatientHistory.patienthealthinfo;
            DataTable dtAllergy = Helper.ToDataTable(PatientAllergy);
            dtAllergy.Columns[1].ColumnName = "allergy";
            if (dtAllergy.Select("other_health_info_type = 1").Count() > 0)
            {
                DrugAllergy.DataSource = dtAllergy.Select("other_health_info_type = 1").CopyToDataTable();
                DrugAllergy.DataBind();
                Lblemptyaledrug.Style.Add("display", "none");

                flagalergikosong = flagalergikosong + 1;
            }
            else
            {
                DrugAllergy.DataSource = null;
                DrugAllergy.DataBind();
            }

            if (dtAllergy.Select("other_health_info_type = 2").Count() > 0)
            {
                FoodAllergy.DataSource = dtAllergy.Select("other_health_info_type = 2").CopyToDataTable();
                FoodAllergy.DataBind();
                Lblemptyalefood.Style.Add("display", "none");

                flagalergikosong = flagalergikosong + 1;
            }
            else
            {
                FoodAllergy.DataSource = null;
                FoodAllergy.DataBind();
            }

            if (dtAllergy.Select("other_health_info_type = 7").Count() > 0)
            {
                OtherAllergy.DataSource = dtAllergy.Select("other_health_info_type = 7").CopyToDataTable();
                OtherAllergy.DataBind();
                Lblemptyalelain.Style.Add("display", "none");

                flagalergikosong = flagalergikosong + 1;
            }
            else
            {
                OtherAllergy.DataSource = null;
                OtherAllergy.DataBind();
            }

            if (flagalergikosong == 0)
            {
                divkosong_allergy.Style.Add("display", "");
                divisi_allergy.Style.Add("display", "none");
                flag_responsive++;
            }
            else
            {
                divkosong_allergy.Style.Add("display", "none");
                divisi_allergy.Style.Add("display", "");
            }

            if (dtAllergy.Select("other_health_info_type = 3").Count() > 0)
            {
                DataTable data = dtAllergy.Select("other_health_info_type = 3").CopyToDataTable();
                //data.Columns[1].DataType = typeof(DateTime);
                gvw_surgery.DataSource = data;
                gvw_surgery.DataBind();
                lblemptysurgery.Style.Add("display", "none");
            }
            else
            {
                gvw_surgery.DataSource = null;
                gvw_surgery.DataBind();
            }

            List<ViewHealthInfo> CurrMedication = JsongetPatientHistory.patienthealthinfo.Where(y => y.other_health_info_type == 6).ToList();
            DataTable dtroutine = Helper.ToDataTable(CurrMedication);
            dtroutine.Columns[1].ColumnName = "current_medication";
            if (CurrMedication.Count() > 0)
            {
                RepCurrentMedication.DataSource = dtroutine;
                RepCurrentMedication.DataBind();
                lblemptyroutinemed.Style.Add("display", "none");

                divkosong_routinemed.Style.Add("display", "none");
                divisi_routinemed.Style.Add("display", "");
            }
            else
            {
                RepCurrentMedication.DataSource = null;
                RepCurrentMedication.DataBind();

                divkosong_routinemed.Style.Add("display", "");
                divisi_routinemed.Style.Add("display", "none");
                flag_responsive++;
            }

            List<ViewNotification> listreminder = JsongetPatientHistory.patientnotification;
            hfjsonreminder.Value = new JavaScriptSerializer().Serialize(listreminder);

            if (listreminder.Count() > 0)
            {
                gvw_reminder.DataSource = Helper.ToDataTable(listreminder);
                gvw_reminder.DataBind();
                lblemptyreminder.Style.Add("display", "none");

                divkosong_reminder.Style.Add("display", "none");
                divisi_reminder.Style.Add("display", "");
            }
            else
            {
                gvw_reminder.DataSource = null;
                gvw_reminder.DataBind();

                divkosong_reminder.Style.Add("display", "");
                divisi_reminder.Style.Add("display", "none");
                flag_responsive++;
            }

            List<ViewProcedure> listoutsideprocedure = JsongetPatientHistory.patientprocedure.Where(y => y.procedure_type == 1).ToList();
            List<ViewProcedure> listinsideprocedure = JsongetPatientHistory.patientprocedure.Where(y => y.procedure_type == 2).ToList();
            if (listoutsideprocedure.Count() > 0)
            {
                gvw_outsideprocedure.DataSource = Helper.ToDataTable(listoutsideprocedure);
                gvw_outsideprocedure.DataBind();
                lblemptyoutside.Style.Add("display", "none");
            }
            else
            {
                gvw_outsideprocedure.DataSource = null;
                gvw_outsideprocedure.DataBind();
            }

            if (listinsideprocedure.Count() > 0)
            {
                gvw_procedure.DataSource = Helper.ToDataTable(listinsideprocedure);
                gvw_procedure.DataBind();
                lblemptyinternal.Style.Add("display", "none");
            }
            else
            {
                gvw_procedure.DataSource = null;
                gvw_procedure.DataBind();
            }

            if (JsongetPatientHistory.patientheader.IsReferral > 0)
            {
                
                var varResultReferral = clsSOAP.getPatientReferral(Helper.organizationId, PatientId, admissionId);
                var JsongetMapReferral = JsonConvert.DeserializeObject<ResultPatientReferral>(varResultReferral.Result.ToString());

                List<PatientReferral> patientReferrals = JsongetMapReferral.list;
                ModalReferalList.initializevalue(patientReferrals);
                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalpatientreferral", "$('#modal-referal-list').modal('show');", true);
            }
            else if (JsongetPatientHistory.patientheader.IsBalasan > 0)
            {

                var varResultReferralBalasan = clsSOAP.getPatientReferralBalasan(Helper.organizationId, PatientId, admissionId);
                var JsongetMapReferralBalasan = JsonConvert.DeserializeObject<ResultPatientReferralBalasan>(varResultReferralBalasan.Result.ToString());

                PatientReferralBalasan patientReferralBalasans = JsongetMapReferralBalasan.list;
                ModalReferalListBalasan.initializevalue(patientReferralBalasans);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalpatientreferralbalasan", "$('#modal-referal-list-balasan').modal('show');", true);
            }

            //getHeader();

            //PatientHeader header = new PatientHeader();
            //header.AdmissionNo = JsongetPatientHistory.patientheader.AdmissionNo;
            //header.AdmissionTypeId = JsongetPatientHistory.patientheader.AdmissionTypeId;
            //header.BirthDate = JsongetPatientHistory.patientheader.BirthDate;
            //header.DoctorName = JsongetPatientHistory.patientheader.DoctorName;
            //header.EncounterId = JsongetPatientHistory.patientheader.EncounterId;
            //header.Formularium = JsongetPatientHistory.patientheader.Formularium;
            //header.Gender = JsongetPatientHistory.patientheader.Gender;
            //header.MrNo = JsongetPatientHistory.patientheader.MrNo;
            //header.PatientName = JsongetPatientHistory.patientheader.PatientName;
            //header.PayerId = JsongetPatientHistory.patientheader.PayerId;
            //header.PayerName = JsongetPatientHistory.patientheader.PayerName;
            //header.Religion = JsongetPatientHistory.patientheader.Religion;

            //PatientCard.initializevalue(header);

            //var Medication = JsongetPatientHistory.list[0].lastmedication;
            //if (Medication.Count > 0)
            //{
            //    admissionDate.Text = Medication[0].admission_date.ToString("dd MMM yyyy");
            //    lbldoctordrugpres.Text = Medication[0].doctor_name;
            //    LastMedication.DataSource = Helper.ToDataTable(Medication);
            //    LastMedication.DataBind();
            //    lblemptydrugpres.Style.Add("display", "none");
            //}
            //else
            //{
            //    admissionDate.Text = "-";
            //    lbldoctordrugpres.Visible = false;
            //    LastMedication.DataSource = null;
            //    LastMedication.DataBind();
            //}

            //DateTime MedHistoryDate = JsongetPatientHistory.list.medicalhistory.First().AdmissionDate;
            //if (MedHistoryDate == DateTime.MinValue)
            //{
            //    lblMedHistoryDate.Text = "-";
            //}
            //else
            //{
            //    lblMedHistoryDate.Text = MedHistoryDate.ToString("dd MMM yyyy");
            //}

            //lblPrimary.Text = (from a in JsongetPatientHistory.list.medicalhistory
            //                   where (a.mapping_id == Guid.Parse("D24D0881-7C06-4563-BF75-3A20B843DC47"))
            //                   select a.remarks == null ? "" : a.remarks).SingleOrDefault().ToString();
            //if (lblPrimary.Text != "")
            //{
            //    Lbldoctormedihis.Text = JsongetPatientHistory.list.medicalhistory.First().doctor_name;
            //    lblemptymedihis1.Style.Add("display", "none");
            //}
            //else
            //{
            //    Lbldoctormedihis.Visible = false;
            //}

            //lblProcedure.Text = (from a in JsongetPatientHistory.list.medicalhistory
            //                     where (a.mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
            //                     select a.remarks == null ? "" : a.remarks).SingleOrDefault().ToString();
            //if (lblProcedure.Text != "")
            //{
            //    lblemptymedihis2.Style.Add("display", "none");
            //}
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", PatientId.ToString(), "getMedicalHistory", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", PatientId.ToString(), "getPatientData", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
        }
        //Log.Info(LogConfig.LogEnd());
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
            var JsongetPatientHistorydata = JsonConvert.DeserializeObject<ResponsePatientHeader>(varResult.Result.ToString());

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getPatientData", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", varResult.Result.ToString()));
            //Log.Debug(LogConfig.LogEnd("GetPatientHeader", JsongetPatientHistorydata.Status, JsongetPatientHistorydata.Message));

            PatientHeader header = JsongetPatientHistorydata.Data;
            PatientCard.initializevalue(header);
            PatientCardRefModal.initializevalue(header);
            PatientCardRefModalBalasan.initializevalue(header);
            hfPageSoapIdHeader.Value = header.PageSOAP.ToString();
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getPatientData", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnFilterReminder_Click(object sender, EventArgs e)
    {
        if (chkreminder.Checked)
        {
            List<ViewNotification> tempreminder = new JavaScriptSerializer().Deserialize<List<ViewNotification>>(hfjsonreminder.Value);
            List<ViewNotification> reminderfilter = tempreminder.Where(y => y.doctor_id == long.Parse(Helper.GetDoctorID(this))).ToList();
            if (reminderfilter.Count() > 0)
            {
                gvw_reminder.DataSource = Helper.ToDataTable(reminderfilter);
                gvw_reminder.DataBind();
                lblemptyreminder.Style.Add("display", "none");
            }
            else
            {
                gvw_reminder.DataSource = null;
                gvw_reminder.DataBind();
            }
        }
        else
        {
            List<ViewNotification> tempreminder = new JavaScriptSerializer().Deserialize<List<ViewNotification>>(hfjsonreminder.Value);
            if (tempreminder.Count() > 0)
            {
                gvw_reminder.DataSource = Helper.ToDataTable(tempreminder);
                gvw_reminder.DataBind();
                lblemptyreminder.Style.Add("display", "none");
            }
            else
            {
                gvw_reminder.DataSource = null;
                gvw_reminder.DataBind();
            }
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        int existingYear = int.Parse(lblYear.Text.ToString());
        lblYear.Text = (existingYear + 1).ToString();
        if ((int.Parse(lblYear.Text.ToString())) == DateTime.Now.Year)
        {
            btnNext.Enabled = false;
            btnNext.Style.Add("cursor", "not-allowed");
        }
        getAdmissionHistory(long.Parse(hfPatientId.Value));
    }

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        int existingYear = int.Parse(lblYear.Text.ToString());
        lblYear.Text = (existingYear - 1).ToString();
        btnNext.Enabled = true;
        btnNext.Style.Add("cursor", "pointer");
        getAdmissionHistory(long.Parse(hfPatientId.Value));
    }

    protected void btnlabModal_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        string a = hfLab.Value.ToString();
        List<LaboratoryResult> listlaboratory = new List<LaboratoryResult>();

        //Log.Debug(LogConfig.LogStart("getLaboratoryResult", LogConfig.LogParam("Admission_ID", hfLab.Value)));
        var dataLaboratory = clsResult.getLaboratoryResult(hfLab.Value.ToString());
        var JsonLaboratory = JsonConvert.DeserializeObject<ResponseLaboratoryResult>(dataLaboratory.Result.ToString());

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "AdmissionId", a, "btnlabModal_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataLaboratory.Result.ToString()));
        //Log.Debug(LogConfig.LogEnd("getLaboratoryResult", JsonLaboratory.Status, JsonLaboratory.Message));
        listlaboratory = new List<LaboratoryResult>();
        listlaboratory = JsonLaboratory.Data;

        //StdLabResult.initializevalue(listlaboratory);
        StdLabResult.initializevalue(listlaboratory);
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        SoapPagePreview.initializevalue(long.Parse(EMROrganization.Value.ToString()), long.Parse(hfPatientId.Value), long.Parse(EMRAdmission.Value.ToString()), Guid.Parse(EMREncounter.Value.Replace("/", "")));
    }

    protected void btnRadDetail_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            List<radiologyByWeek> listAdmissionDetail = new List<radiologyByWeek>();

            //Log.Debug(LogConfig.LogStart("getRadResultAdmissionDetail", LogConfig.LogParam("Admission_ID", hfRad.Value)));
            var dataAdmissionDetail = clsResult.getRadResultAdmissionDetail(hfRad.Value.ToString());
            var JsonAdmissionDetail = JsonConvert.DeserializeObject<ResponseRadiologyByWeek>(dataAdmissionDetail.Result);
            
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "AdmissionId", hfRad.Value.ToString(), "btnRadDetail_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataAdmissionDetail.Result.ToString()));
            //Log.Debug(LogConfig.LogEnd("getRadResultAdmissionDetail", JsonAdmissionDetail.Status, JsonAdmissionDetail.Message));

            if (JsonAdmissionDetail != null)
                listAdmissionDetail = JsonAdmissionDetail.Data;

            StdRadResult.initializevalue(listAdmissionDetail);

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "AdmissionId", hfRad.Value.ToString(), "btnRadDetail_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            // Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btn_patientDetail_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            //var userID = Helper.GetLoginUser(this);
            //log.Info(LogLibrary.Logging("S", "getPatientDetail", Helper.GetLoginUser(this), ""));

            //log.Debug(LogLibrary.Logging("S", "getPatientHistoryData ", Helper.GetLoginUser(this), ""));
            //var varResult = clsPatientHistory.getPatientHistoryData(Int64.Parse(hf_organizationID.Value), Int64.Parse(hf_patientID.Value), Int64.Parse(hf_admissionID.Value), hf_encounterID.Value);
            //var JsongetPatientHistoryData = JsonConvert.DeserializeObject<ResultPatientHistoryEncounterData>(varResult.Result.ToString());
            //log.Debug(LogLibrary.Logging("E", "getPatientHistoryData", userID, JsonConvert.SerializeObject(JsongetPatientHistoryData)));

            //StdPatientHistory.setPatientHistory(userID, JsongetPatientHistoryData);

            var localIPAdress = Helper.GetLocalIPAddress();
            string baseURLhttp = "http://" + localIPAdress + "/viewer";
            string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_Viewer"]; //"https://gtn-devws-01.siloamhospitals.com:2123"; //nanti akan ambil dari registry
            string url = baseURLhttps + "/Form/FormViewer/MedicalResumePatient.aspx?org_id=" + hf_organizationID.Value + "&ptn_id=" + hf_patientID.Value + "&adm_id=" + hf_admissionID.Value + "&enc_id=" + hf_encounterID.Value + "&pagesoap_id=" + hf_pagesoapID.Value + "&headertype=2" + "&username=" + Helper.GetLoginUser(this);
            
            //string url = "http://" + localIPAdress + "/Viewer/Form/FormViewer/MedicalResumePatient.aspx?org_id=" + hf_organizationID.Value + "&ptn_id=" + hf_patientID.Value + "&adm_id=" + hf_admissionID.Value + "&enc_id=" + hf_encounterID.Value + "&pagesoap_id=" + hf_pagesoapID.Value + "&headertype=2" + "&username=" + Helper.GetLoginUser(this);
            //var localIPAdress = "localhost:62383";
            //string url = "http://" + localIPAdress + "/Form/FormViewer/MedicalResumePatient.aspx?org_id=" + hf_organizationID.Value + "&ptn_id=" + hf_patientID.Value + "&adm_id=" + hf_admissionID.Value + "&enc_id=" + hf_encounterID.Value + "&pagesoap_id=" + hf_pagesoapID.Value + "&headertype=2" + "&username=" + Helper.GetLoginUser(this);
            IframeMedicalResumePatient.Src = url;


        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hf_patientID.Value.ToString(), "btn_patientDetail_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
        }
        //Log.Info(LogConfig.LogEnd());
    }

    void responsivedivempty()
    {
        if (flag_responsive == 4)
        {
            divkosong_allergy.Attributes["class"] = "col-lg-3";
            divkosong_reminder.Attributes["class"] = "col-lg-3";
            divkosong_routinemed.Attributes["class"] = "col-lg-3";
            divkosong_procresult.Attributes["class"] = "col-lg-3";
        }
        else if (flag_responsive == 3)
        {
            divkosong_allergy.Attributes["class"] = "col-lg-4";
            divkosong_reminder.Attributes["class"] = "col-lg-4";
            divkosong_routinemed.Attributes["class"] = "col-lg-4";
            divkosong_procresult.Attributes["class"] = "col-lg-4";
        }
        else if (flag_responsive == 2)
        {
            divkosong_allergy.Attributes["class"] = "col-lg-6";
            divkosong_reminder.Attributes["class"] = "col-lg-6";
            divkosong_routinemed.Attributes["class"] = "col-lg-6";
            divkosong_procresult.Attributes["class"] = "col-lg-6";
        }
        else if (flag_responsive == 1)
        {
            divkosong_allergy.Attributes["class"] = "col-lg-12";
            divkosong_reminder.Attributes["class"] = "col-lg-12";
            divkosong_routinemed.Attributes["class"] = "col-lg-12";
            divkosong_procresult.Attributes["class"] = "col-lg-12";
        }
    }

    protected void btnrevisionModal_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            var userID = Helper.GetLoginUser(this);

            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Organization_ID", HFrevOrgID.Value },
                { "Patient_ID", HFrevPtnID.Value },
                { "Admission_ID", HFrevAdmID.Value },
                { "Encounter_ID", HFrevEncID.Value }
            };
            //Log.Debug(LogConfig.LogStart("getRevisionHistorySOAP", logParam));
            var varResult = clsPatientDetail.getRevisionHistorySOAP(Int64.Parse(HFrevOrgID.Value), Int64.Parse(HFrevPtnID.Value), Int64.Parse(HFrevAdmID.Value), HFrevEncID.Value);
            var JsongetRevisionHistoryData = JsonConvert.DeserializeObject<ResultSOAPLog>(varResult.Result.ToString());

            //Log.Debug(LogConfig.LogEnd("getRevisionHistorySOAP", JsongetRevisionHistoryData.Status, JsongetRevisionHistoryData.Message));

            SOAPLog SL = new SOAPLog();
            if (JsongetRevisionHistoryData != null)
            {
                SL = JsongetRevisionHistoryData.list;

                //List<long> headid = new List<long>();
                List<long> detailid = new List<long>();
                List<LogHeader> resultHeader = new List<LogHeader>();

                //foreach (LogHeader h in SL.Header)
                //{
                //    headid.Add(h.ID);
                //}

                foreach (LogSOAPData s in SL.SOAPData)
                {
                    detailid.Add(s.ID);
                }

                foreach (LogCPOE c in SL.CPOEData)
                {
                    detailid.Add(c.ID);
                }

                foreach (LogPrescription p in SL.PrescriptionData)
                {
                    detailid.Add(p.ID);
                }

                resultHeader = SL.Header.Where(r => detailid.Any(x => x == r.ID)).ToList();

                DataTable RevSoapData = Helper.ToDataTable(SL.SOAPData);
                Session[Helper.SessionRevSoap] = RevSoapData;

                DataTable RevCpoeData = Helper.ToDataTable(SL.CPOEData);
                Session[Helper.SessionRevCpoe] = RevCpoeData;

                DataTable RevPresData = Helper.ToDataTable(SL.PrescriptionData);
                Session[Helper.SessionRevPres] = RevPresData;

                //header is the last bind
                DataTable RevHeaderData = Helper.ToDataTable(resultHeader);
                RepeaterRevisionHeader.DataSource = RevHeaderData;
                RepeaterRevisionHeader.DataBind();

                string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", HFrevEncID.Value.ToString(), "btnrevisionModal_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

            }

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", HFrevEncID.Value.ToString(), "btnrevisionModal_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
        }
        //Log.Info(LogConfig.LogEnd());
    }

    public string getDatafromRow(DataTable dt, string mappingname, string tag)
    {
        DataRow[] drlabeldata = dt.Select("MappingName = '" + mappingname + "'");
        if (drlabeldata.Length > 0)
        {
            return drlabeldata[0][tag].ToString();
        }
        else
        {
            return "";
        }
    }

    protected void RepeaterRevisionHeader_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string headerId = (e.Item.FindControl("HF_REVHeaderID") as HiddenField).Value;

            Panel panelS = e.Item.FindControl("panelS") as Panel;
            Panel panelO = e.Item.FindControl("panelO") as Panel;
            Panel panelA = e.Item.FindControl("panelA") as Panel;
            Panel panelP = e.Item.FindControl("panelP") as Panel;
            Panel panelCPOE = e.Item.FindControl("panelCPOE") as Panel;
            Panel panelPRES = e.Item.FindControl("panelPRES") as Panel;

            Repeater rptDetailLabRad = e.Item.FindControl("RptLabRad") as Repeater;
            Repeater rptDetailDrugs = e.Item.FindControl("RptDrugs") as Repeater;
            Repeater rptDetailCons = e.Item.FindControl("RptCons") as Repeater;


            if (Session[Helper.SessionRevSoap] != null)
            {
                DataRow[] drS = ((DataTable)Session[Helper.SessionRevSoap]).Select("ID = '" + headerId + "' AND MappingType = 'S'");

                if (drS.Length > 0)
                {
                    DataTable dtSoapS = drS.CopyToDataTable();

                    //Label LabelComplaint = e.Item.FindControl("LabelComplaint") as Label;
                    //LabelComplaint.Text = "<b>Patient Complaint</b> <br/>" + getDatafromRow(dtSoapS, "PATIENT COMPLAINT", "Remarks") + "<br/>";

                    //Label LabelAnamnesis = e.Item.FindControl("LabelAnamnesis") as Label;
                    //LabelAnamnesis.Text = "<b>Anamnesis</b> <br/>" + getDatafromRow(dtSoapS, "ANAMNESIS", "Remarks") ;

                    Label LabelComplaint = e.Item.FindControl("LabelComplaint") as Label;
                    DataRow[] drlabelc = dtSoapS.Select("MappingName = 'PATIENT COMPLAINT'");
                    if (drlabelc.Length > 0)
                    {
                        LabelComplaint.Text = "<b>Patient Complaint</b> <br/>" + drlabelc[0]["Remarks"].ToString().Replace("\n","<br/>") + "<br/>";
                    }

                    Label LabelAnamnesis = e.Item.FindControl("LabelAnamnesis") as Label;
                    DataRow[] drlabela = dtSoapS.Select("MappingName = 'ANAMNESIS'");
                    if (drlabela.Length > 0)
                    {
                        LabelAnamnesis.Text = "<b>Anamnesis</b> <br/>" + drlabela[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/>";
                    }

                    Label LabelDoctorNotesToNurse = e.Item.FindControl("LabelDoctorNotesToNurse") as Label;
                    DataRow[] drlabeld = dtSoapS.Select("MappingName = 'DOCTOR NOTES NURSE'");
                    if (drlabeld.Length > 0)
                    {
                        LabelDoctorNotesToNurse.Text = "<b>Doctor Notes To Nurse</b> <br/>" + drlabeld[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/>";
                    }
                }
                else
                {
                    panelS.Visible = false;
                }

                DataRow[] drO = ((DataTable)Session[Helper.SessionRevSoap]).Select("ID = '" + headerId + "' AND MappingType = 'O'");

                if (drO.Length > 0)
                {
                    DataTable dtSoapO = drO.CopyToDataTable();

                    Label LabelOther = e.Item.FindControl("LabelOther") as Label;
                    DataRow[] drlabelother = dtSoapO.Select("MappingName = 'OTHERS'");
                    if (drlabelother.Length > 0)
                    {
                        LabelOther.Text = "<b>Others</b> <br/>" + drlabelother[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/><br/>";
                    }

                    Label LabelBloodPresH = e.Item.FindControl("LabelBloodPresH") as Label;
                    DataRow[] drlabelbpH = dtSoapO.Select("MappingName = 'BLOOD PRESSURE HIGH'");
                    if (drlabelbpH.Length > 0)
                    {
                        LabelBloodPresH.Text = "<b>Blood Pressure High</b> : " + drlabelbpH[0]["Value"].ToString() + " mmHg <br/>";
                    }
                    Label LabelBloodPresL = e.Item.FindControl("LabelBloodPresL") as Label;
                    DataRow[] drlabelbpL = dtSoapO.Select("MappingName = 'BLOOD PRESSURE LOW'");
                    if (drlabelbpL.Length > 0)
                    {
                        LabelBloodPresL.Text = "<b>Blood Pressure Low</b> : " + drlabelbpL[0]["Value"].ToString() + " mmHg <br/>";
                    }
                    Label LabelPulse = e.Item.FindControl("LabelPulse") as Label;
                    DataRow[] drlabelpr = dtSoapO.Select("MappingName = 'PULSE RATE'");
                    if (drlabelpr.Length > 0)
                    {
                        LabelPulse.Text = "<b>Pulse</b> : " + drlabelpr[0]["Value"].ToString() + " x/mnt <br/>";
                    }
                    Label LabelRespiratory = e.Item.FindControl("LabelRespiratory") as Label;
                    DataRow[] drlabelrr = dtSoapO.Select("MappingName = 'RESPIRATORY RATE'");
                    if (drlabelrr.Length > 0)
                    {
                        LabelRespiratory.Text = "<b>Respiratory Rate</b> : " + drlabelrr[0]["Value"].ToString() + " x/mnt <br/>";
                    }
                    Label LabelSPO2 = e.Item.FindControl("LabelSPO2") as Label;
                    DataRow[] drlabelsp = dtSoapO.Select("MappingName = 'SPO2'");
                    if (drlabelsp.Length > 0)
                    {
                        LabelSPO2.Text = "<b>SpO2</b> : " + drlabelsp[0]["Value"].ToString() + " % <br/>";
                    }
                    Label LabelTemp = e.Item.FindControl("LabelTemp") as Label;
                    DataRow[] drlabelt = dtSoapO.Select("MappingName = 'TEMPERATURE'");
                    if (drlabelt.Length > 0)
                    {
                        LabelTemp.Text = "<b>Temperature</b> : " + drlabelt[0]["Value"].ToString() + " °C <br/>";
                    }
                    Label LabelWeight = e.Item.FindControl("LabelWeight") as Label;
                    DataRow[] drlabelw = dtSoapO.Select("MappingName = 'WEIGHT'");
                    if (drlabelw.Length > 0)
                    {
                        LabelWeight.Text = "<b>Weight</b> : " + drlabelw[0]["Value"].ToString() + " kg <br/>";
                    }
                    Label LabelHeight = e.Item.FindControl("LabelHeight") as Label;
                    DataRow[] drlabelh = dtSoapO.Select("MappingName = 'HEIGHT'");
                    if (drlabelh.Length > 0)
                    {
                        LabelHeight.Text = "<b>Height</b> : " + drlabelh[0]["Value"].ToString() + " cm <br/>";
                    }
                    Label LabelHeadCir = e.Item.FindControl("LabelHeadCir") as Label;
                    DataRow[] drlabelhc = dtSoapO.Select("MappingName = 'LINGKAR KEPALA'");
                    if (drlabelhc.Length > 0)
                    {
                        LabelHeadCir.Text = "<b>Head Circumference</b> : " + drlabelhc[0]["Value"].ToString() + " cm <br/>";
                    }
                }
                else
                {
                    panelO.Visible = false;
                }

                DataRow[] drA = ((DataTable)Session[Helper.SessionRevSoap]).Select("ID = '" + headerId + "' AND MappingType = 'A'");

                if (drA.Length > 0)
                {
                    DataTable dtSoapA = drA.CopyToDataTable();

                    Label LabelPrimaryDiagnosis = e.Item.FindControl("LabelPrimaryDiagnosis") as Label;
                    DataRow[] drlabelpd = dtSoapA.Select("MappingName = 'PRIMARY DIAGNOSIS'");
                    if (drlabelpd.Length > 0)
                    {
                        LabelPrimaryDiagnosis.Text = "<b>Primary Diagnosis</b> <br/>" + drlabelpd[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/>";
                    }
                }
                else
                {
                    panelA.Visible = false;
                }

                DataRow[] drP = ((DataTable)Session[Helper.SessionRevSoap]).Select("ID = '" + headerId + "' AND MappingType = 'P'");

                if (drP.Length > 0)
                {
                    DataTable dtSoapP = drP.CopyToDataTable();

                    Label LabelPlanningProcedure = e.Item.FindControl("LabelPlanningProcedure") as Label;
                    DataRow[] drlabelpp = dtSoapP.Select("MappingName = 'PLANNING PROCEDURE'");
                    if (drlabelpp.Length > 0)
                    {
                        LabelPlanningProcedure.Text = "<b>Planning Procedure</b> <br/>" + drlabelpp[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/>";
                    }

                    Label LabelPlanningOthers = e.Item.FindControl("LabelPlanningOthers") as Label;
                    DataRow[] drlabelpo = dtSoapP.Select("MappingName = 'PLANNING OTHERS'");
                    if (drlabelpo.Length > 0)
                    {
                        LabelPlanningOthers.Text = "<b>Planning Others</b> <br/>" + drlabelpo[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/>";
                    }

                    Label LabelProcedureResult = e.Item.FindControl("LabelProcedureResult") as Label;
                    DataRow[] drlabelpr = dtSoapP.Select("MappingName = 'PROCEDURE RESULT'");
                    if (drlabelpr.Length > 0)
                    {
                        LabelProcedureResult.Text = "<b>Procedure Result</b> <br/>" + drlabelpr[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/>";
                    }
                }
                else
                {
                    panelP.Visible = false;
                }
            }

            if (Session[Helper.SessionRevCpoe] != null)
            {
                DataRow[] drLabRad = ((DataTable)Session[Helper.SessionRevCpoe]).Select("ID = '" + headerId + "'");

                if (drLabRad.Length > 0)
                {
                    DataTable dtSoapLabRad = drLabRad.CopyToDataTable();
                    rptDetailLabRad.DataSource = dtSoapLabRad;
                    rptDetailLabRad.DataBind();
                }
                else
                {
                    panelCPOE.Visible = false;
                }
            }

            if (Session[Helper.SessionRevPres] != null)
            {
                DataRow[] drDrugs = ((DataTable)Session[Helper.SessionRevPres]).Select("ID = '" + headerId + "' AND IsConsumables = 0");

                if (drDrugs.Length > 0)
                {
                    DataTable dtSoapDrug = drDrugs.CopyToDataTable();
                    rptDetailDrugs.DataSource = dtSoapDrug;
                    rptDetailDrugs.DataBind();
                }

                DataRow[] drCons = ((DataTable)Session[Helper.SessionRevPres]).Select("ID = '" + headerId + "' AND IsConsumables = 1");

                if (drCons.Length > 0)
                {
                    DataTable dtSoapCons = drCons.CopyToDataTable();
                    rptDetailCons.DataSource = dtSoapCons;
                    rptDetailCons.DataBind();
                }

                DataRow[] drPres = ((DataTable)Session[Helper.SessionRevPres]).Select("ID = '" + headerId + "'");

                if (drPres.Length == 0)
                {
                    panelPRES.Visible = false;
                }
            }
        }
    }

    public void ClickPatient()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            string isaido = Request.QueryString["IsTele"];
            string admid = Request.QueryString["ApptAdmID"];
            string orgid = Request.QueryString["ApptOrgID"];
            string docid = Request.QueryString["ApptDocID"];
            string apptid = Request.QueryString["AppointmentId"];

            if (isaido != null && admid != null && orgid != null && docid != null && apptid != null)
            {
                if (apptid != Guid.Empty.ToString())
                {
                    if (isaido.ToLower() == "false")
                    {
                        var CallAction = clsWorklistAppointment.FirstClickPatientMySiloam(Guid.Parse(orgid), Guid.Parse(docid), Guid.Parse(admid));
                        var JsonCall = (JObject)JsonConvert.DeserializeObject<dynamic>(CallAction.Result);
                        var Status = JsonCall.Property("status").Value.ToString().Replace(@"'", @"\'");
                        var Message = JsonCall.Property("message").Value.ToString().Replace(@"'", @"\'");

                    }
                    else if (isaido.ToLower() == "true")
                    {
                        var CallAction = clsWorklistAppointment.FirstClickPatientTeleMySiloam(Guid.Parse(orgid), Guid.Parse(docid), Guid.Parse(apptid));
                        var JsonCall = (JObject)JsonConvert.DeserializeObject<dynamic>(CallAction.Result);
                        var Status = JsonCall.Property("status").Value.ToString().Replace(@"'", @"\'");
                        var Message = JsonCall.Property("message").Value.ToString().Replace(@"'", @"\'");

                    }
                }
            }
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "ClickPatient", StartTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "ClickPatient", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            //return "ERROR" + ", " + ex.ToString();
            Session[CstSession.sessionerror] = ex;
        }
    }
}