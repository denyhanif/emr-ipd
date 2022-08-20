using log4net;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static PatientHistory;

//Session[Helper.ViewStateRadiologyResult]
public partial class Form_General_Result : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    List<LaboratoryResult> listlaboratory = new List<LaboratoryResult>();

    public string setENG = "none";
    public string setIND = "none";

    protected void Page_Load(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();

        if (Session[Helper.SessionLanguage] == null)
            Session[Helper.SessionLanguage] = 1;

        if (Session[Helper.SessionLanguage].ToString() == "1")
        {
            setENG = "";
            setIND = "none";
            HFisBahasa.Value = "ENG";
        }
        else if (Session[Helper.SessionLanguage].ToString() == "2")
        {
            setENG = "none";
            setIND = "";
            HFisBahasa.Value = "IND";
        }
        else
        {
            setENG = "";
            setIND = "none";
            HFisBahasa.Value = "ENG";
        }

        //set bahasa
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "bahasa", "switchBahasa();", true);

        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            HyperLink test = Master.FindControl("ResultLink") as HyperLink;
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
                Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], "", Request.QueryString["PageSoapId"], Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
                hfPatientId.Value = Request.QueryString["idPatient"];
                hfEncounterId.Value = Request.QueryString["EncounterId"];
                hfAdmissionId.Value = Request.QueryString["AdmissionId"];
                hfPageSoapId.Value = Request.QueryString["PageSoapId"];
                lblYear.Text = DateTime.Now.Year.ToString();
                btn_next_ono.Enabled = false;
                btn_next_ono.Style.Add("cursor", "not-allowed");

                getHeader();
                Session.Remove(Helper.ViewStateListLaboratory);
                getSearchLabHistory(1, "10");
                getRadResult(hfPatientId.Value.ToString(), DateTime.Now.Year);
                img_compare.PostBackUrl = String.Format("~/Form/General/Result/CompareLaboratory.aspx?idPatient={0}&EncounterId={1}&AdmissionId={2}&PageSoapId={3}", hfPatientId.Value, hfEncounterId.Value, hfAdmissionId.Value, hfPageSoapId.Value);
                MainView.ActiveViewIndex = 0;
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Page_Load", StartTime, EndTime, "OK", MyUser.GetUsername()
                          , "", "", ""));
            //Log.Info(LogConfig.LogEnd());
        }
    }

    void getHeader ()
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

    void getRadResult(String patientId, Int64 year)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        Session.Remove(Helper.ViewStateRadiologyResult);
        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Patient_ID", patientId },
                { "Year", year.ToString() }
            };
            //Log.Debug(LogConfig.LogStart("getRadResultAdmission", logParam));
            var dataRad = clsResult.getRadResultAdmission(patientId, year);
            var JsonRad = JsonConvert.DeserializeObject<ResponselaboratoryByWeek>(dataRad.Result);
            //Log.Debug(LogConfig.LogEnd("getRadResultAdmission", JsonRad.Status, JsonRad.Message));

            List<laboratoryByWeek> listRad = JsonRad.Data;
            Session[Helper.ViewStateRadiologyResult] = listRad;
            groupLaboratoryByWeek(listRad);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "getRadResult", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "getRadResult", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    void groupLaboratoryByWeek(List<laboratoryByWeek> data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<string> admissionList = hfListAdmission.Value.Split(',').ToList();
        //------------------------------------------ INSERT DATA TO MONTH BOX WEEK BY WEEK ------------------------------------------------
                             //------------------------------- Create Table --------------------------------

        StringBuilder result = new StringBuilder();
        result.Append("<table border=\"1\" style=\"width:100%;border-color:lightgray;border-style:solid;\" id=\"tbl_date_lab\"><tr>" +
            "<td style=\"width:100px\" colspan=\"4\"><b>January</b></td>" +
            "<td style=\"width:100px\" colspan=\"4\"><b>February</b></td>" +
            "<td style=\"width:100px\" colspan=\"4\"><b>March</b></td>" +
            "<td style=\"width:100px\" colspan=\"4\"><b>April</b></td>" +
            "<td style=\"width:100px\" colspan=\"4\"><b>May</b></td>" +
            "<td style=\"width:100px\" colspan=\"4\"><b>June</b></td>" +
            "<td style=\"width:100px\" colspan=\"4\"><b>July</b></td>" +
            "<td style=\"width:100px\" colspan=\"4\"><b>August</b></td>" +
            "<td style=\"width:100px\" colspan=\"4\"><b>September</b></td>" +
            "<td style=\"width:100px\" colspan=\"4\"><b>October</b></td>" +
            "<td style=\"width:100px\" colspan=\"4\"><b>November</b></td>" +
            "<td style=\"width:100px\" colspan=\"4\"><b>December</b></td></tr>");

        //------------------------------------------ INSERT DATA TO MONTH BOX WEEK BY WEEK ------------------------------------------------
        int count = 0;
        Boolean dateChooce = false;
        for (int k = 1; k <= 12; k++)
        {
            for (int j = 1; j <= 4; j++)
            {
                if (data.Find(x => x.requestMonth == k & x.requestWeek == j) != null)
                {
                    var admissionId = "";
                    //var date = "";
                    List<Int64?> temp = data.FindAll(x => x.requestMonth == k & x.requestWeek == j).DistinctBy(x => x.admissionId).Select(x => x.admissionId).ToList();
                    foreach (Int64? m in temp)
                    {
                        if (admissionList.FindAll(x => x.Contains(m.ToString())).Count != 0 && admissionList[0] != "")
                            dateChooce = true;

                        if (admissionId == "")
                            admissionId = m.ToString();
                        else {
                            admissionId = admissionId + "," + m.ToString();
                        }
                    }

                    var link = "javascript:addAdmissionList('" + admissionId + "'," + count + ")";
                    if (dateChooce)
                        result.Append("<td style=\"background-color:grey; height:40px; padding:0;\"><a style=\"display:inline-block;min-height:100%; width:100%; padding-top:11px\" href=\"" + link + ";\">" + j + "</a></td>");
                    else
                        result.Append("<td style=\"background-color:orange; height:40px; padding:0;\"><a style=\"display:inline-block;min-height:100%; width:100%; padding-top:11px\" href=\"" + link + ";\">" + j + "</a></td>");

                    count++;
                    dateChooce = false;
                }
                else
                {
                    result.Append("<td style=\"height:40px; padding:0;\">" + j + "</td>");
                    count++;
                }
            }
        }
        result.Append("</tr></table>");
        divDateRadiology.InnerHtml = result.ToString();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "groupLaboratoryByWeek", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());
    }

    void getSearchLabHistory(Int64 type, String value)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Patient_ID", hfPatientId.Value },
                { "Type", type.ToString() },
                { "Value", value }
            };
            //Log.Debug(LogConfig.LogStart("getSearchLaboratoryResult", logParam));
            var dataAdmission = clsResult.getSearchLaboratoryResult(hfPatientId.Value, type, value);
            var JsonAdmission = JsonConvert.DeserializeObject<admissionList>(dataAdmission.Result);
            //Log.Debug(LogConfig.LogEnd("getSearchLaboratoryResult", JsonAdmission.Status, JsonAdmission.Message));

            String listAdmission = JsonAdmission.data;
            var dataLaboratory = clsResult.getLaboratoryResult(listAdmission);
            var JsonLaboratory = JsonConvert.DeserializeObject<ResultLaboratoryResult>(dataLaboratory.Result.ToString());

            if (JsonLaboratory != null)
                listlaboratory = JsonLaboratory.list;
            else
                img_compare.Visible = false;

            Session[Helper.ViewStateListLaboratory] = listlaboratory;
            StdLabResult.initializevalue(listlaboratory);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "getSearchLabHistory", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

        }
        catch (Exception ex)
        {
            LogLibrary.Error("getSearchLaboratoryResult ", hfAdmissionId.Value, ex.Message.ToString());
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void labResult_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        // Log.Info(LogConfig.LogStart());

        labResult_div.Style.Add("background-color", "#d6dbff");
        radResult_div.Style.Add("background-color", "transparent");

        listlaboratory = (List<LaboratoryResult>) Session[Helper.ViewStateListLaboratory];
        StdLabResult.initializevalue(listlaboratory);
        MainView.ActiveViewIndex = 0;
        MainView.Visible = true;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "labResult_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());
    }

    protected void radResult_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        radResult_div.Style.Add("background-color", "#d6dbff");
        labResult_div.Style.Add("background-color", "transparent");

        MainView.ActiveViewIndex = 1;
        MainView.Visible = true;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "radResult_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());
    }

    protected void diagResult_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        MainView.ActiveViewIndex = 2;
        MainView.Visible = true;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "diagResult_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (txt_admissionNo_doctorName.Text.ToString() != "")
        {
            ddlEncounterMode.SelectedIndex = 0;
        }
        else
        {
            getSearchLabHistory(1, ddlEncounterMode.SelectedValue.ToString());
            txt_admissionNo_doctorName.Text = "";
            ddlEncounterMode.SelectedIndex = 0;     
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "btnSearch_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDetailRadiology_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            List<radiologyByWeek> listAdmissionDetail = new List<radiologyByWeek>();
            List<laboratoryByWeek> listRad = (List<laboratoryByWeek>)Session[Helper.ViewStateRadiologyResult];

            groupLaboratoryByWeek(listRad);

            //Log.Debug(LogConfig.LogStart("getRadResultAdmissionDetail", LogConfig.LogParam("Admission_List", hfListAdmission.Value)));
            var dataAdmissionDetail = clsResult.getRadResultAdmissionDetail(hfListAdmission.Value.ToString());
            var JsonAdmissionDetail = JsonConvert.DeserializeObject<ResponseRadiologyByWeek>(dataAdmissionDetail.Result);
            //Log.Debug(LogConfig.LogEnd("getRadResultAdmissionDetail", JsonAdmissionDetail.Status, JsonAdmissionDetail.Message));

            if (JsonAdmissionDetail != null)
                listAdmissionDetail = JsonAdmissionDetail.Data;

            StdRadResult.initializevalue(listAdmissionDetail);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "btnDetailRadiology_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "btnDetailRadiology_Click", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btn_prev_ono_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        var lbl_year = Int64.Parse(lblYear.Text);
        btn_next_ono.Enabled = true;
        btn_next_ono.Style.Add("cursor", "pointer");
        lblYear.Text = (lbl_year - 1).ToString();
        hfListAdmission.Value = "";
        getRadResult(hfPatientId.Value, Int64.Parse(lblYear.Text));
        StdRadResult.initializevalue(new List<radiologyByWeek>());

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "btn_prev_ono_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btn_next_ono_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        var lbl_year = Int64.Parse(lblYear.Text);
        if ((lbl_year + 1).ToString() == DateTime.Now.Year.ToString())
        {
            btn_next_ono.Enabled = false;
            btn_next_ono.Style.Add("cursor", "not-allowed");
        }
        else
        {
            btn_next_ono.Enabled = true;
            btn_next_ono.Style.Add("cursor", "pointer");
        }

        lblYear.Text = (lbl_year + 1).ToString();
        hfListAdmission.Value = "";
        getRadResult(hfPatientId.Value, Int64.Parse(lblYear.Text));
        StdRadResult.initializevalue(new List<radiologyByWeek>());

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", hfEncounterId.Value.ToString(), "btn_next_ono_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());
    }
}