using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using static PatientHistory;
using static SPPediatric;

public partial class Form_SOAP_Control_Template_StdKurvaPertumbuhan : System.Web.UI.UserControl
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();

        if (!IsPostBack)
        {
        }

    }

    //sementara belum digunakan
    //public void initializevalue(dynamic Jsongetsoap, PatientHeader header, string guidadditional)
    //{

    //    hfguidadditional.Value = guidadditional;

    //    if (Jsongetsoap.list.pediatric_chart != null)
    //    {
    //        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = Jsongetsoap.list.pediatric_chart;
    //        Session[Helper.SessionDataDetailChart_Save + hfguidadditional.Value] = Jsongetsoap.list.pediatric_chart;
    //    }
    //    else
    //    {
    //        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = null;
    //        Session[Helper.SessionDataDetailChart_Save + hfguidadditional.Value] = null;
    //    }

    //    List<PediatricChart> ChartList = Jsongetsoap.list.pediatric_chart;
    //    if (ChartList.Count != 0)
    //    {
    //        List<PediatricChart> PBU_Data = ChartList.Where(x => x.chart_type == "PBU").ToList();
    //        RepeaterPBU.DataSource = Helper.ToDataTable(PBU_Data);
    //        RepeaterPBU.DataBind();
    //        List<ChartData> PBU_Chart = getXYchart(PBU_Data);
    //        HF_PBU_Data.Value = new JavaScriptSerializer().Serialize(PBU_Chart);


    //        List<PediatricChart> BBU_Data = ChartList.Where(x => x.chart_type == "BBU").ToList();
    //        RepeaterBBU.DataSource = Helper.ToDataTable(BBU_Data);
    //        RepeaterBBU.DataBind();
    //        List<ChartData> BBU_Chart = getXYchart(BBU_Data);
    //        HF_BBU_Data.Value = new JavaScriptSerializer().Serialize(BBU_Chart);

    //        List<PediatricChart> BBPB_Data = ChartList.Where(x => x.chart_type == "BBPB").ToList();
    //        RepeaterBBPB.DataSource = Helper.ToDataTable(BBPB_Data);
    //        RepeaterBBPB.DataBind();
    //        List<ChartData> BBPB_Chart = getXYchart(BBPB_Data);
    //        HF_BBPB_Data.Value = new JavaScriptSerializer().Serialize(BBPB_Chart);

    //        List<PediatricChart> LKU_Data = ChartList.Where(x => x.chart_type == "LKU").ToList();
    //        RepeaterLKU.DataSource = Helper.ToDataTable(LKU_Data);
    //        RepeaterLKU.DataBind();
    //        List<ChartData> LKU_Chart = getXYchart(LKU_Data);
    //        HF_LKU_Data.Value = new JavaScriptSerializer().Serialize(LKU_Chart);

    //        List<PediatricChart> BMI_Data = ChartList.Where(x => x.chart_type == "BMI").ToList();
    //        RepeaterBMI.DataSource = Helper.ToDataTable(BMI_Data);
    //        RepeaterBMI.DataBind();
    //        List<ChartData> BMI_Chart = getXYchart(BMI_Data);
    //        HF_BMI_Data.Value = new JavaScriptSerializer().Serialize(BMI_Chart);
    //    }
    //    else
    //    {
    //        RepeaterPBU.DataSource = null;
    //        RepeaterPBU.DataBind();
    //        List<ChartData> PBU_Chart = new List<ChartData>();
    //        HF_PBU_Data.Value = new JavaScriptSerializer().Serialize(PBU_Chart);

    //        RepeaterBBU.DataSource = null;
    //        RepeaterBBU.DataBind();
    //        List<ChartData> BBU_Chart = new List<ChartData>();
    //        HF_BBU_Data.Value = new JavaScriptSerializer().Serialize(BBU_Chart);

    //        RepeaterBBPB.DataSource = null;
    //        RepeaterBBPB.DataBind();
    //        List<ChartData> BBPB_Chart = new List<ChartData>();
    //        HF_BBPB_Data.Value = new JavaScriptSerializer().Serialize(BBPB_Chart);

    //        RepeaterLKU.DataSource = null;
    //        RepeaterLKU.DataBind();
    //        List<ChartData> LKU_Chart = new List<ChartData>();
    //        HF_LKU_Data.Value = new JavaScriptSerializer().Serialize(LKU_Chart);

    //        RepeaterBMI.DataSource = null;
    //        RepeaterBMI.DataBind();
    //        List<ChartData> BMI_Chart = new List<ChartData>();
    //        HF_BMI_Data.Value = new JavaScriptSerializer().Serialize(BMI_Chart);
    //    }

    //    LabelNamaPasienChart.Text = header.PatientName;
    //    LabelAgePasienChart.Text = clsCommon.GetAge(header.BirthDate);
    //    HFAgePasienDays.Value = clsCommon.GetAgeDays(header.BirthDate).ToString();
    //    HFAgePasienMonths.Value = clsCommon.GetAgeMonths(header.BirthDate).ToString();
    //    HFAgePasienYears.Value = clsCommon.GetAgeYears(header.BirthDate).ToString();
    //    HFGenderPasien.Value = header.Gender.ToString();

    //    if (header.Gender == 1)
    //    {
    //        imgSex.ImageUrl = "~/Images/Icon/ic_Male.svg";
    //    }
    //    else if (header.Gender == 2)
    //    {
    //        imgSex.ImageUrl = "~/Images/Icon/ic_Female.svg";
    //    }

    //    getCoordinatByChartType("PBU", header.Gender);
    //    getCoordinatByChartType("BBU", header.Gender);
    //    getCoordinatByChartType("BBPB", header.Gender);
    //    getCoordinatByChartType("LKU", header.Gender);
    //    getCoordinatByChartType("BMIU", header.Gender);


    //}

    public void initializevalue(PatientHeader header, string guidadditional, string ptnid, string encid, string admid, string docid)
    {

        hfguidadditional.Value = guidadditional;

        LabelNamaPasienChart.Text = header.PatientName;
        LabelAgePasienChart.Text = clsCommon.GetAge(header.BirthDate);
        HFAgePasienDays.Value = clsCommon.GetAgeDays(header.BirthDate).ToString();
        HFAgePasienMonths.Value = clsCommon.GetAgeMonths(header.BirthDate).ToString();
        HFAgePasienYears.Value = clsCommon.GetAgeYears(header.BirthDate).ToString();
        HFGenderPasien.Value = header.Gender.ToString();

        HF_PtnID.Value = ptnid;
        HF_EncID.Value = encid;
        HF_AdmID.Value = admid;
        HF_DocID.Value = docid;

        if (header.Gender == 1)
        {
            imgSex.ImageUrl = "~/Images/Icon/ic_Male.svg";

            LabelHeaderPBU.CssClass = "male-color";
            LabelHeaderBBU.CssClass = "male-color";
            LabelHeaderBBPB.CssClass = "male-color";
            LabelHeaderLKU.CssClass = "male-color";
            LabelHeaderBMI.CssClass = "male-color";
            LabelHeaderLLU.CssClass = "male-color";
        }
        else if (header.Gender == 2)
        {
            imgSex.ImageUrl = "~/Images/Icon/ic_Female.svg";

            LabelHeaderPBU.CssClass = "female-color";
            LabelHeaderBBU.CssClass = "female-color";
            LabelHeaderBBPB.CssClass = "female-color";
            LabelHeaderLKU.CssClass = "female-color";
            LabelHeaderBMI.CssClass = "female-color";
            LabelHeaderLLU.CssClass = "female-color";
        }

        HF_FlagChartTemplate.Value = "CLOSE";

        HF_PBU_MinMaxAge.Value = "0;168;7;Age(completed weeks or months)"; //0-6Month
        HF_BBU_MinMaxAge.Value = "0;168;7;Age(completed weeks or months)"; //0-6Month step 7 day legend
        HF_BBPB_MinMaxAge.Value = "0;672"; //0-2Years
        //HF_LKU_MinMaxAge.Value = "0;672;28;Age(completed months and years)"; //0-2Years
        HF_LKU_MinMaxAge.Value = "0;91;7;Age(weeks)"; //0-13Weeks
        HF_BMI_MinMaxAge.Value = "0;672;28;Age(completed months and years)"; //0-2Years
        HF_LLU_MinMaxAge.Value = "84;1680;28;Age(completed months and years)"; //3months-5Years

        HF_PBU_MinMaxY.Value = "40;75;5"; //40-75 step 5
        HF_BBU_MinMaxY.Value = "0;11;1"; //0-11 step 1
        HF_BBPB_MinMaxX.Value = "45;110;5;Length(cm)"; //45-110 step 4 legend
        HF_BBPB_MinMaxY.Value = "0;24;2"; //0-24 step 2
        //HF_LKU_MinMaxY.Value = "30;53;1"; //30-53 step 1
        HF_LKU_MinMaxY.Value = "30;45;1"; //30-45 step 1
        HF_BMI_MinMaxY.Value = "9;23;1"; //9-23 step 1
        HF_LLU_MinMaxY.Value = "10;23;1"; //10-23 step 1
  
    }

    public void LoadChart(List<PediatricChart> JsongetsoapListPediatric)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (JsongetsoapListPediatric != null)
        {
            Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = JsongetsoapListPediatric;
            //Session[Helper.SessionDataDetailChart_Save + hfguidadditional.Value] = JsongetsoapListPediatric;
        }
        else
        {
            Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = null;
            //Session[Helper.SessionDataDetailChart_Save + hfguidadditional.Value] = null;
        }


        List<PediatricChart> ChartList = JsongetsoapListPediatric;
        if (ChartList.Count != 0)
        {
            List<PediatricChart> PBU_Data = ChartList.Where(x => x.chart_type == "PBU").ToList().OrderBy(y => y.x).ToList();
            RepeaterPBU.DataSource = Helper.ToDataTable(PBU_Data);
            RepeaterPBU.DataBind();
            //List<ChartData> PBU_Chart = getXYchart(PBU_Data);
            //HF_PBU_Data.Value = new JavaScriptSerializer().Serialize(PBU_Chart);
            BindChartPBU(PBU_Data);

            List<PediatricChart> BBU_Data = ChartList.Where(x => x.chart_type == "BBU").ToList().OrderBy(y => y.x).ToList();
            RepeaterBBU.DataSource = Helper.ToDataTable(BBU_Data);
            RepeaterBBU.DataBind();
            //List<ChartData> BBU_Chart = getXYchart(BBU_Data);
            //HF_BBU_Data.Value = new JavaScriptSerializer().Serialize(BBU_Chart);
            BindChartBBU(BBU_Data);

            List<PediatricChart> BBPB_Data = ChartList.Where(x => x.chart_type == "BBPB").ToList();
            RepeaterBBPB.DataSource = Helper.ToDataTable(BBPB_Data);
            RepeaterBBPB.DataBind();
            //List<ChartData> BBPB_Chart = getXYchart(BBPB_Data);
            //HF_BBPB_Data.Value = new JavaScriptSerializer().Serialize(BBPB_Chart);
            BindChartBBPB(BBPB_Data);

            List<PediatricChart> LKU_Data = ChartList.Where(x => x.chart_type == "LKU").ToList().OrderBy(y => y.x).ToList();
            RepeaterLKU.DataSource = Helper.ToDataTable(LKU_Data);
            RepeaterLKU.DataBind();
            //List<ChartData> LKU_Chart = getXYchart(LKU_Data);
            //HF_LKU_Data.Value = new JavaScriptSerializer().Serialize(LKU_Chart);
            BindChartLKU(LKU_Data);

            List<PediatricChart> BMI_Data = ChartList.Where(x => x.chart_type == "BMI").ToList().OrderBy(y => y.x).ToList();
            RepeaterBMI.DataSource = Helper.ToDataTable(BMI_Data);
            RepeaterBMI.DataBind();
            //List<ChartData> BMI_Chart = getXYchart(BMI_Data);
            //HF_BMI_Data.Value = new JavaScriptSerializer().Serialize(BMI_Chart);
            BindChartBMI(BMI_Data);

            List<PediatricChart> LLU_Data = ChartList.Where(x => x.chart_type == "LLU").ToList().OrderBy(y => y.x).ToList();
            RepeaterLLU.DataSource = Helper.ToDataTable(LLU_Data);
            RepeaterLLU.DataBind();
            List<ChartData> LLU_Chart = getXYchart(LLU_Data);
            HF_LLU_Data.Value = new JavaScriptSerializer().Serialize(LLU_Chart);
            BindChartLLU(LLU_Data);
        }
        else
        {
            RepeaterPBU.DataSource = null;
            RepeaterPBU.DataBind();
            List<ChartData> PBU_Chart = new List<ChartData>();
            HF_PBU_Data.Value = new JavaScriptSerializer().Serialize(PBU_Chart);

            RepeaterBBU.DataSource = null;
            RepeaterBBU.DataBind();
            List<ChartData> BBU_Chart = new List<ChartData>();
            HF_BBU_Data.Value = new JavaScriptSerializer().Serialize(BBU_Chart);

            RepeaterBBPB.DataSource = null;
            RepeaterBBPB.DataBind();
            List<ChartData> BBPB_Chart = new List<ChartData>();
            HF_BBPB_Data.Value = new JavaScriptSerializer().Serialize(BBPB_Chart);

            RepeaterLKU.DataSource = null;
            RepeaterLKU.DataBind();
            List<ChartData> LKU_Chart = new List<ChartData>();
            HF_LKU_Data.Value = new JavaScriptSerializer().Serialize(LKU_Chart);

            RepeaterBMI.DataSource = null;
            RepeaterBMI.DataBind();
            List<ChartData> BMI_Chart = new List<ChartData>();
            HF_BMI_Data.Value = new JavaScriptSerializer().Serialize(BMI_Chart);

            RepeaterLLU.DataSource = null;
            RepeaterLLU.DataBind();
            List<ChartData> LLU_Chart = new List<ChartData>();
            HF_LLU_Data.Value = new JavaScriptSerializer().Serialize(LLU_Chart);
        }


        if (HF_FlagChartTemplate.Value != "OPEN")
        {
            getCoordinatByChartType("PBU", int.Parse(HFGenderPasien.Value), "hide", "day");
            getCoordinatByChartType("BBU", int.Parse(HFGenderPasien.Value), "hide", "day");
            getCoordinatByChartType("BBPB", int.Parse(HFGenderPasien.Value), "hide", "day");
            getCoordinatByChartType("LKU", int.Parse(HFGenderPasien.Value), "hide", "day");
            getCoordinatByChartType("BMIU", int.Parse(HFGenderPasien.Value), "hide", "day");
            getCoordinatByChartType("LLU", int.Parse(HFGenderPasien.Value), "hide", "day");

            HF_FlagChartTemplate.Value = "OPEN";
            UpdatePanelChartTemplate.Update();
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "LoadChart", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    public List<ChartData> getXYchart(List<PediatricChart> data)
    {
        List<ChartData> result = new List<ChartData>();

        foreach (PediatricChart c in data)
        {
            ChartData titik = new ChartData();
            titik.x = c.x;
            titik.y = c.y;

            result.Add(titik);
        }

        return result;
    }

    public List<PediatricChart> CloneList(List<PediatricChart> data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        // Log.Info(LogConfig.LogStart());

        List<PediatricChart> result = new List<PediatricChart>();

        foreach (PediatricChart c in data)
        {
            PediatricChart newdata = new PediatricChart();
            newdata.chart_transaction_master_id = c.chart_transaction_master_id;
            newdata.chart_type = c.chart_type;
            newdata.age = c.age;
            newdata.x = c.x;
            newdata.y = c.y;
            newdata.verdict_note = c.verdict_note;
            newdata.age_type = c.age_type;
            newdata.isNew = c.isNew;

            result.Add(newdata);
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CloneList", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
        return result;

    }

    public void UPChartTemplate()
    {
        if (HF_FlagChartTemplate.Value != "OPEN")
        {
            UpdatePanelChartTemplate.Update();
            UpdatePanelChartTemplatePBU.Update();
            UpdatePanelChartTemplateBBU.Update();
            UpdatePanelChartTemplateBBPB.Update();
            UpdatePanelChartTemplateLKU.Update();
            UpdatePanelChartTemplateBMI.Update();
            UpdatePanelChartTemplateLLU.Update();
        }
    }

    //public dynamic GetKurvaValues(dynamic DataKurva)
    //{
    //    try
    //    {
    //        //log.Info(LogLibrary.Logging("S", "GetKurvaValues", Helper.GetLoginUser(this.Parent.Page), ""));

    //        //DataTable dataAll = (DataTable)Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value];
    //        //DataKurva.vaccination = new List<Vaccination>();

    //        //if (dataAll != null && dataAll.Rows.Count != 0)
    //        //{
    //        //    //DataKurva.vaccination.AddRange(GetRowListVaccination());
    //        //}

    //    }
    //    catch (Exception ex)
    //    {
    //        //log.Error(LogLibrary.Error("GetKurvaValues", Helper.GetLoginUser(this.Parent.Page), ex.InnerException.Message));
    //    }

    //    return DataKurva;
    //}

    protected void ButtonSaveKurva_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        //List<PediatricChart> datadetail = GetRowListVaccination();
        //DataTable dt_alldataKurva = Helper.ToDataTable(datadetail);
        //Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] = dt_alldataKurva;
        //Session[Helper.SessionDataDetailVaksinasi_Save + hfguidadditional.Value] = dt_alldataKurva;

        List<PediatricChart> ListPediatricData = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];
        Session[Helper.SessionDataDetailChart_Save + hfguidadditional.Value] = ListPediatricData;

        foreach (PediatricChart PC in ListPediatricData)
        {
            if (PC.isNew == true)
            {
                PC.chart_transaction_master_id = Guid.Empty;
            }
        }

        Dictionary<string, string> logParam = new Dictionary<string, string>
        {
            { "Organization_ID", Helper.organizationId.ToString() },
            { "Patient_ID", HF_PtnID.Value.ToString() },
            { "Admission_ID", HF_AdmID.Value.ToString() },
            { "Encounter_ID", HF_EncID.Value.ToString() },
            { "Doctor_ID", HF_DocID.Value.ToString() }
        };
        //Log.Debug(LogConfig.LogStart("SubmitPediatricChart", logParam, LogConfig.JsonToString(ListPediatricData)));
        var resultChart = clsSPPediatricSOAP.SubmitPediatricChart(ListPediatricData, Helper.organizationId, long.Parse(HF_PtnID.Value), long.Parse(HF_AdmID.Value), Guid.Parse(HF_EncID.Value), long.Parse(HF_DocID.Value));
        var JsonChart = (JObject)JsonConvert.DeserializeObject<dynamic>(resultChart.Result);
        var Status = JsonChart.Property("status").Value.ToString();
        var Message = JsonChart.Property("message").Value.ToString();
        //Log.Debug(LogConfig.LogEnd("SubmitPediatricChart", Status, Message));

        if (Status != "Success")
        {
            LabelChartSaveFail.Visible = true;
            LabelChartSaveFail.ToolTip = Message;
            LabelChartSaveFail2.Visible = true;
            LabelChartSaveFail2.ToolTip = Message;
            foreach (PediatricChart PC in ListPediatricData)
            {
                if (PC.isNew == true)
                {
                    PC.chart_transaction_master_id = Guid.NewGuid();
                }
            }
        }
        else
        {
            LabelChartSaveFail.Visible = false;
            LabelChartSaveFail2.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalKurva", "HideKurva();", true);
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonSaveKurva_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonCancelKurva_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        //DataTable datainitial = (DataTable)Session[Helper.SessionDataDetailVaksinasi_Save + hfguidadditional.Value];
        //Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] = datainitial;

        //RepeaterKurvaDewasa.DataSource = (DataTable) Session[Helper.SessionVaccineDWS];
        //RepeaterKurvaDewasa.DataBind();
        //RepeaterKurvaAnak.DataSource = (DataTable)Session[Helper.SessionVaccineANK];
        //RepeaterKurvaAnak.DataBind();

        List<PediatricChart> JsongetsoapListPediatricInitial = (List<PediatricChart>)Session[Helper.SessionDataDetailChart_Save + hfguidadditional.Value];
        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = JsongetsoapListPediatricInitial;

        LabelChartSaveFail.Visible = false;
        LabelChartSaveFail2.Visible = false;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalKurva", "HideKurva();", true);

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonCancelKurva_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    //first method
    //public void getCoordinat()
    //{
    //    var GetC = clsSPPediatricSOAP.GetCoordinat(2);
    //    var JsonC = JsonConvert.DeserializeObject<ResultChart>(GetC.Result.ToString());
    //    List<Chart> CoordinatList = JsonC.list;

    //    List<ChartData> data0 = new List<ChartData>();
    //    string temp0 = "";
    //    List<ChartData> data2 = new List<ChartData>();
    //    string temp2 = "";
    //    List<ChartData> datamin2 = new List<ChartData>();
    //    string tempmin2 = "";
    //    List<ChartData> data3 = new List<ChartData>();
    //    string temp3 = "";
    //    List<ChartData> datamin3 = new List<ChartData>();
    //    string tempmin3 = "";

    //    foreach (var data in CoordinatList)
    //    {
    //        if (data.score == 0)
    //        {
    //            if (temp0 != data.y.ToString())
    //            {
    //                ChartData n = new ChartData();
    //                n.x = data.x;
    //                n.y = data.y;

    //                data0.Add(n);

    //                temp0 = data.y.ToString();
    //            }
    //        }

    //        if (data.score == 2)
    //        {
    //            if (temp2 != data.y.ToString())
    //            {
    //                ChartData n = new ChartData();
    //                n.x = data.x;
    //                n.y = data.y;

    //                data2.Add(n);

    //                temp2 = data.y.ToString();
    //            }
    //        }

    //        if (data.score == -2)
    //        {
    //            if (tempmin2 != data.y.ToString())
    //            {
    //                ChartData n = new ChartData();
    //                n.x = data.x;
    //                n.y = data.y;

    //                datamin2.Add(n);

    //                tempmin2 = data.y.ToString();
    //            }
    //        }

    //        if (data.score == 3)
    //        {
    //            if (temp3 != data.y.ToString())
    //            {
    //                ChartData n = new ChartData();
    //                n.x = data.x;
    //                n.y = data.y;

    //                data3.Add(n);

    //                temp3 = data.y.ToString();
    //            }
    //        }

    //        if (data.score == -3)
    //        {
    //            if (tempmin3 != data.y.ToString())
    //            {
    //                ChartData n = new ChartData();
    //                n.x = data.x;
    //                n.y = data.y;

    //                datamin3.Add(n);

    //                tempmin3 = data.y.ToString();
    //            }
    //        }
    //    }

    //    HF_BBU0.Value = new JavaScriptSerializer().Serialize(data0);
    //    HF_BBUplus2.Value = new JavaScriptSerializer().Serialize(data2);
    //    HF_BBUminus2.Value = new JavaScriptSerializer().Serialize(datamin2);
    //    HF_BBUplus3.Value = new JavaScriptSerializer().Serialize(data3);
    //    HF_BBUminus3.Value = new JavaScriptSerializer().Serialize(datamin3);
    //}

    public void getCoordinatByChartType(string type, int gender, string visibility, string agetype)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<ChartData> data0 = new List<ChartData>();
        List<ChartData> data1 = new List<ChartData>();
        List<ChartData> datamin1 = new List<ChartData>();
        List<ChartData> data2 = new List<ChartData>();
        List<ChartData> datamin2 = new List<ChartData>();
        List<ChartData> data3 = new List<ChartData>();
        List<ChartData> datamin3 = new List<ChartData>();

        if (visibility == "show")
        {
            //API Version
            //var GetC = clsSPPediatricSOAP.GetCoordinat(gender);
            //var JsonC = JsonConvert.DeserializeObject<ResultChart>(GetC.Result.ToString());
            //List<Chart> CoordinatList = JsonC.list.Where(x => x.chart_type == type).ToList();

            //List Version
            //CoordinatCalculation(CoordinatList, out data0, out data1, out datamin1, out data2, out datamin2, out data3, out datamin3);

            //SP Version
            //Log.Debug(LogConfig.LogStart("GetCoordinatbySP"));
            var ListC = clsSPPediatricSOAP.GetCoordinatbySP(gender);
            DataRow[] tempSearch = ListC.Select("chart_type ='" + type + "' AND age_type ='" + agetype + "'");
            if (tempSearch.Length > 0)
            {
                DataTable CoordinatList = ListC.Select("chart_type ='" + type + "' AND age_type ='" + agetype + "'").CopyToDataTable();

                //Datatable Version
                CoordinatCalculation2(CoordinatList, out data0, out data1, out datamin1, out data2, out datamin2, out data3, out datamin3);
            }
        }

        if (type == "PBU")
        {
            HF_PBU0.Value = new JavaScriptSerializer().Serialize(data0);
            HF_PBUplus2.Value = new JavaScriptSerializer().Serialize(data2);
            HF_PBUminus2.Value = new JavaScriptSerializer().Serialize(datamin2);
            HF_PBUplus3.Value = new JavaScriptSerializer().Serialize(data3);
            HF_PBUminus3.Value = new JavaScriptSerializer().Serialize(datamin3);
        }
        else if (type == "BBU")
        {
            HF_BBU0.Value = new JavaScriptSerializer().Serialize(data0);
            HF_BBUplus2.Value = new JavaScriptSerializer().Serialize(data2);
            HF_BBUminus2.Value = new JavaScriptSerializer().Serialize(datamin2);
            HF_BBUplus3.Value = new JavaScriptSerializer().Serialize(data3);
            HF_BBUminus3.Value = new JavaScriptSerializer().Serialize(datamin3);
        }
        else if (type == "BBPB" || type == "BBTB")
        {
            HF_BBPB0.Value = new JavaScriptSerializer().Serialize(data0);
            HF_BBPBplus1.Value = new JavaScriptSerializer().Serialize(data1);
            HF_BBPBminus1.Value = new JavaScriptSerializer().Serialize(datamin1);
            HF_BBPBplus2.Value = new JavaScriptSerializer().Serialize(data2);
            HF_BBPBminus2.Value = new JavaScriptSerializer().Serialize(datamin2);
            HF_BBPBplus3.Value = new JavaScriptSerializer().Serialize(data3);
            HF_BBPBminus3.Value = new JavaScriptSerializer().Serialize(datamin3);
        }
        else if (type == "LKU")
        {
            HF_LKU0.Value = new JavaScriptSerializer().Serialize(data0);
            HF_LKUplus1.Value = new JavaScriptSerializer().Serialize(data1);
            HF_LKUminus1.Value = new JavaScriptSerializer().Serialize(datamin1);
            HF_LKUplus2.Value = new JavaScriptSerializer().Serialize(data2);
            HF_LKUminus2.Value = new JavaScriptSerializer().Serialize(datamin2);
            HF_LKUplus3.Value = new JavaScriptSerializer().Serialize(data3);
            HF_LKUminus3.Value = new JavaScriptSerializer().Serialize(datamin3);
        }
        else if (type == "BMIU")
        {
            HF_BMI0.Value = new JavaScriptSerializer().Serialize(data0);
            HF_BMIplus1.Value = new JavaScriptSerializer().Serialize(data1);
            HF_BMIminus1.Value = new JavaScriptSerializer().Serialize(datamin1);
            HF_BMIplus2.Value = new JavaScriptSerializer().Serialize(data2);
            HF_BMIminus2.Value = new JavaScriptSerializer().Serialize(datamin2);
            HF_BMIplus3.Value = new JavaScriptSerializer().Serialize(data3);
            HF_BMIminus3.Value = new JavaScriptSerializer().Serialize(datamin3);
        }
        else if (type == "LLU")
        {
            HF_LLU0.Value = new JavaScriptSerializer().Serialize(data0);
            HF_LLUplus1.Value = new JavaScriptSerializer().Serialize(data1);
            HF_LLUminus1.Value = new JavaScriptSerializer().Serialize(datamin1);
            HF_LLUplus2.Value = new JavaScriptSerializer().Serialize(data2);
            HF_LLUminus2.Value = new JavaScriptSerializer().Serialize(datamin2);
            HF_LLUplus3.Value = new JavaScriptSerializer().Serialize(data3);
            HF_LLUminus3.Value = new JavaScriptSerializer().Serialize(datamin3);
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "getCoordinatByChartType", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    public void CoordinatCalculation(List<Chart> CoordinatList, out List<ChartData> data0, out List<ChartData> data1, out List<ChartData> datamin1, out List<ChartData> data2, out List<ChartData> datamin2, out List<ChartData> data3, out List<ChartData> datamin3)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        //string temp0 = "";
        //string temp1 = "";
        //string tempmin1 = "";
        //string temp2 = "";
        //string tempmin2 = "";
        //string temp3 = "";
        //string tempmin3 = "";

        data0 = new List<ChartData>();
        data1 = new List<ChartData>();
        datamin1 = new List<ChartData>();
        data2 = new List<ChartData>();
        datamin2 = new List<ChartData>();
        data3 = new List<ChartData>();
        datamin3 = new List<ChartData>();

        foreach (var data in CoordinatList)
        {
            if (data.score == 0)
            {
                //if (temp0 != data.y.ToString())
                //{
                    ChartData n = new ChartData();
                    n.x = data.x;
                    n.y = data.y;

                    data0.Add(n);

                    //temp0 = data.y.ToString();
                //}
            }

            if (data.score == 1)
            {
                //if (temp1 != data.y.ToString())
                //{
                    ChartData n = new ChartData();
                    n.x = data.x;
                    n.y = data.y;

                    data1.Add(n);

                    //temp1 = data.y.ToString();
                //}
            }

            if (data.score == -1)
            {
                //if (tempmin1 != data.y.ToString())
                //{
                    ChartData n = new ChartData();
                    n.x = data.x;
                    n.y = data.y;

                    datamin1.Add(n);

                    //tempmin1 = data.y.ToString();
                //}
            }

            if (data.score == 2)
            {
                //if (temp2 != data.y.ToString())
                //{
                    ChartData n = new ChartData();
                    n.x = data.x;
                    n.y = data.y;

                    data2.Add(n);

                    //temp2 = data.y.ToString();
                //}
            }

            if (data.score == -2)
            {
                //if (tempmin2 != data.y.ToString())
                //{
                    ChartData n = new ChartData();
                    n.x = data.x;
                    n.y = data.y;

                    datamin2.Add(n);

                    //tempmin2 = data.y.ToString();
                //}
            }

            if (data.score == 3)
            {
                //if (temp3 != data.y.ToString())
                //{
                    ChartData n = new ChartData();
                    n.x = data.x;
                    n.y = data.y;

                    data3.Add(n);

                    //temp3 = data.y.ToString();
                //}
            }

            if (data.score == -3)
            {
                //if (tempmin3 != data.y.ToString())
                //{
                    ChartData n = new ChartData();
                    n.x = data.x;
                    n.y = data.y;

                    datamin3.Add(n);

                    //tempmin3 = data.y.ToString();
                //}
            }
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CoordinatCalculation", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    public void CoordinatCalculation2(DataTable CoordinatList, out List<ChartData> data0, out List<ChartData> data1, out List<ChartData> datamin1, out List<ChartData> data2, out List<ChartData> datamin2, out List<ChartData> data3, out List<ChartData> datamin3)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        //string temp0 = "";
        //string temp1 = "";
        //string tempmin1 = "";
        //string temp2 = "";
        //string tempmin2 = "";
        //string temp3 = "";
        //string tempmin3 = "";

        data0 = new List<ChartData>();
        data1 = new List<ChartData>();
        datamin1 = new List<ChartData>();
        data2 = new List<ChartData>();
        datamin2 = new List<ChartData>();
        data3 = new List<ChartData>();
        datamin3 = new List<ChartData>();

        for(int i=0; i< CoordinatList.Rows.Count; i++)
        {
            if (CoordinatList.Rows[i]["score"].ToString() == "0")
            {
                //if (temp0 != data.y.ToString())
                //{
                ChartData n = new ChartData();
                n.x = decimal.Parse(CoordinatList.Rows[i]["x"].ToString());
                n.y = decimal.Parse(CoordinatList.Rows[i]["y"].ToString());

                data0.Add(n);

                //temp0 = data.y.ToString();
                //}
            }

            if (CoordinatList.Rows[i]["score"].ToString() == "1")
            {
                //if (temp1 != data.y.ToString())
                //{
                ChartData n = new ChartData();
                n.x = decimal.Parse(CoordinatList.Rows[i]["x"].ToString());
                n.y = decimal.Parse(CoordinatList.Rows[i]["y"].ToString());

                data1.Add(n);

                //temp1 = data.y.ToString();
                //}
            }

            if (CoordinatList.Rows[i]["score"].ToString() == "-1")
            {
                //if (tempmin1 != data.y.ToString())
                //{
                ChartData n = new ChartData();
                n.x = decimal.Parse(CoordinatList.Rows[i]["x"].ToString());
                n.y = decimal.Parse(CoordinatList.Rows[i]["y"].ToString());

                datamin1.Add(n);

                //tempmin1 = data.y.ToString();
                //}
            }

            if (CoordinatList.Rows[i]["score"].ToString() == "2")
            {
                //if (temp2 != data.y.ToString())
                //{
                ChartData n = new ChartData();
                n.x = decimal.Parse(CoordinatList.Rows[i]["x"].ToString());
                n.y = decimal.Parse(CoordinatList.Rows[i]["y"].ToString());

                data2.Add(n);

                //temp2 = data.y.ToString();
                //}
            }

            if (CoordinatList.Rows[i]["score"].ToString() == "-2")
            {
                //if (tempmin2 != data.y.ToString())
                //{
                ChartData n = new ChartData();
                n.x = decimal.Parse(CoordinatList.Rows[i]["x"].ToString());
                n.y = decimal.Parse(CoordinatList.Rows[i]["y"].ToString());

                datamin2.Add(n);

                //tempmin2 = data.y.ToString();
                //}
            }

            if (CoordinatList.Rows[i]["score"].ToString() == "3")
            {
                //if (temp3 != data.y.ToString())
                //{
                ChartData n = new ChartData();
                n.x = decimal.Parse(CoordinatList.Rows[i]["x"].ToString());
                n.y = decimal.Parse(CoordinatList.Rows[i]["y"].ToString());

                data3.Add(n);

                //temp3 = data.y.ToString();
                //}
            }

            if (CoordinatList.Rows[i]["score"].ToString() == "-3")
            {
                //if (tempmin3 != data.y.ToString())
                //{
                ChartData n = new ChartData();
                n.x = decimal.Parse(CoordinatList.Rows[i]["x"].ToString());
                n.y = decimal.Parse(CoordinatList.Rows[i]["y"].ToString());

                datamin3.Add(n);

                //tempmin3 = data.y.ToString();
                //}
            }
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CoordinatCalculation2", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    #region PBU Action

    protected void BindChartPBU(List<PediatricChart> PBU_Data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (DDLKurvaPBU.SelectedValue == "3")
        {
            List<PediatricChart> PBU_Data_inmonth = CloneList(PBU_Data);
            foreach (PediatricChart data in PBU_Data_inmonth)
            {
                data.x = data.x / 28;
            }

            List<ChartData> PBU_Chart = getXYchart(PBU_Data_inmonth);
            HF_PBU_Data.Value = new JavaScriptSerializer().Serialize(PBU_Chart);
        }
        else
        {
            List<ChartData> PBU_Chart = getXYchart(PBU_Data);
            HF_PBU_Data.Value = new JavaScriptSerializer().Serialize(PBU_Chart);
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BindChartPBU", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonSavePBULength_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        if (TxtAgePBUYear.Text == "")
        {
            TxtAgePBUYear.Text = "0";
        }
        if (TxtAgePBUMonth.Text == "")
        {
            TxtAgePBUMonth.Text = "0";
        }
        if (TxtAgePBUDay.Text == "")
        {
            TxtAgePBUDay.Text = "0";
        }
        if (TextBoxPBULength.Text == "")
        {
            TextBoxPBULength.Text = "0";
        }

        string age = "";
        decimal ageindays = 0;

        age = TxtAgePBUYear.Text + "Y " + TxtAgePBUMonth.Text + "M " + TxtAgePBUDay.Text + "D";
        ageindays = (decimal.Parse(TxtAgePBUYear.Text) * 336) + (decimal.Parse(TxtAgePBUMonth.Text) * 28) + decimal.Parse(TxtAgePBUDay.Text);

        PediatricChart n = new PediatricChart();
        n.chart_transaction_master_id = Guid.NewGuid();
        n.chart_type = "PBU";
        n.age_type = "DAY";
        n.age = age; //LabelAgePasienChart.Text;
        n.x = ageindays; //decimal.Parse(HFAgePasienDays.Value);
        n.y = decimal.Parse(TextBoxPBULength.Text.Replace(".",","), new System.Globalization.CultureInfo("id-ID"));
        n.verdict_note = "";
        n.isNew = true;
        ChartList.Add(n);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> PBU_Data = ChartList.Where(x => x.chart_type == "PBU").ToList().OrderBy(y => y.x).ToList();
            RepeaterPBU.DataSource = Helper.ToDataTable(PBU_Data);
            RepeaterPBU.DataBind();
            //List<ChartData> PBU_Chart = getXYchart(PBU_Data);
            //HF_PBU_Data.Value = new JavaScriptSerializer().Serialize(PBU_Chart);
            BindChartPBU(PBU_Data);
        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        divPBUData.Visible = false;
        resetPBU();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonSavePBULength_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonUpdatePBULength_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        if (TxtAgePBUYear.Text == "")
        {
            TxtAgePBUYear.Text = "0";
        }
        if (TxtAgePBUMonth.Text == "")
        {
            TxtAgePBUMonth.Text = "0";
        }
        if (TxtAgePBUDay.Text == "")
        {
            TxtAgePBUDay.Text = "0";
        }
        if (TextBoxPBULength.Text == "")
        {
            TextBoxPBULength.Text = "0";
        }

        string age = "";
        decimal ageindays = 0;

        age = TxtAgePBUYear.Text + "Y " + TxtAgePBUMonth.Text + "M " + TxtAgePBUDay.Text + "D";
        ageindays = (decimal.Parse(TxtAgePBUYear.Text) * 336) + (decimal.Parse(TxtAgePBUMonth.Text) * 28) + decimal.Parse(TxtAgePBUDay.Text);

        PediatricChart n = ChartList.FirstOrDefault(x => x.chart_transaction_master_id == Guid.Parse(HFTempPBUMasterId.Value));
        //n.chart_transaction_master_id = Guid.Empty;
        //n.chart_type = "PBU";
        n.age = age; //LabelAgePasienChart.Text;
        n.x = ageindays; //decimal.Parse(HFAgePasienDays.Value);
        n.y = decimal.Parse(TextBoxPBULength.Text.Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        n.verdict_note = "";
        //ChartList.Add(n);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> PBU_Data = ChartList.Where(x => x.chart_type == "PBU").ToList().OrderBy(y => y.x).ToList();
            RepeaterPBU.DataSource = Helper.ToDataTable(PBU_Data);
            RepeaterPBU.DataBind();
            //List<ChartData> PBU_Chart = getXYchart(PBU_Data);
            //HF_PBU_Data.Value = new JavaScriptSerializer().Serialize(PBU_Chart);
            BindChartPBU(PBU_Data);
        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        divPBUData.Visible = false;
        resetPBU();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonUpdatePBULength_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonAddPBU_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        divPBUData.Visible = true;
        TxtAgePBUDay.Text = HFAgePasienDays.Value;
        TxtAgePBUMonth.Text = HFAgePasienMonths.Value;
        TxtAgePBUYear.Text = HFAgePasienYears.Value;

        ButtonSavePBULength.Visible = true;
        ButtonUpdatePBULength.Visible = false;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonAddPBU_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonCancelPBU_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        divPBUData.Visible = false;
        resetPBU();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonCancelPBU_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void resetPBU()
    {
        HFTempPBUMasterId.Value = "";
        TextBoxPBULength.Text = "";
        TxtAgePBUYear.Text = "";
        TxtAgePBUMonth.Text = "";
        TxtAgePBUDay.Text = "";

    }

    protected void ButtonEditPBU_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int selIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;
        HiddenField HFPBUMasterId = (HiddenField)RepeaterPBU.Items[selIndex].FindControl("HFPBUMasterId");
        Label LabelPBUx = (Label)RepeaterPBU.Items[selIndex].FindControl("LabelPBUx");
        Label LabelPBUy = (Label)RepeaterPBU.Items[selIndex].FindControl("LabelPBUy");
        Label LabelPBUage = (Label)RepeaterPBU.Items[selIndex].FindControl("LabelPBUage");

        string[] arrage = LabelPBUage.Text.Split(' ');

        HFTempPBUMasterId.Value = HFPBUMasterId.Value;
        TextBoxPBULength.Text = LabelPBUy.Text;
        TxtAgePBUYear.Text = arrage[0].Remove(arrage[0].Length - 1, 1);
        TxtAgePBUMonth.Text = arrage[1].Remove(arrage[1].Length - 1, 1);
        TxtAgePBUDay.Text = arrage[2].Remove(arrage[2].Length - 1, 1);

        divPBUData.Visible = true;

        ButtonSavePBULength.Visible = false;
        ButtonUpdatePBULength.Visible = true;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonEditPBU_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonDeletePBU_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int selIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;
        HiddenField HFPBUMasterId = (HiddenField)RepeaterPBU.Items[selIndex].FindControl("HFPBUMasterId");

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        PediatricChart del = ChartList.Where(x => x.chart_transaction_master_id == Guid.Parse(HFPBUMasterId.Value)).SingleOrDefault();
        ChartList.Remove(del);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> PBU_Data = ChartList.Where(x => x.chart_type == "PBU").ToList().OrderBy(y => y.x).ToList();

            if (PBU_Data.Count != 0)
            {
                RepeaterPBU.DataSource = Helper.ToDataTable(PBU_Data);
                RepeaterPBU.DataBind();
                //List<ChartData> PBU_Chart = getXYchart(PBU_Data);
                //HF_PBU_Data.Value = new JavaScriptSerializer().Serialize(PBU_Chart);
                BindChartPBU(PBU_Data);
            }
            else
            {
                RepeaterPBU.DataSource = null;
                RepeaterPBU.DataBind();
                List<ChartData> PBU_Chart = new List<ChartData>();
                HF_PBU_Data.Value = new JavaScriptSerializer().Serialize(PBU_Chart);
            }
        }
        else
        {
            RepeaterPBU.DataSource = null;
            RepeaterPBU.DataBind();
            List<ChartData> PBU_Chart = new List<ChartData>();
            HF_PBU_Data.Value = new JavaScriptSerializer().Serialize(PBU_Chart);
        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonDeletePBU_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void DDLKurvaPBU_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];
        List<PediatricChart> PBU_Data = ChartList.Where(x => x.chart_type == "PBU").ToList().OrderBy(y => y.x).ToList();
        BindChartPBU(PBU_Data);

        if (DDLKurvaPBU.SelectedValue == "0")
        {
            HF_PBU_MinMaxAge.Value = "0;168;7;Age(completed weeks or months)"; //0-6Month
            HF_PBU_MinMaxY.Value = "40;75;5"; //40-75 step 5
        }
        else if (DDLKurvaPBU.SelectedValue == "1")
        {
            HF_PBU_MinMaxAge.Value = "0;672;28;Age(completed weeks or months)"; //0-2Year
            HF_PBU_MinMaxY.Value = "40;100;5"; //40-100 step 5
        }
        else if (DDLKurvaPBU.SelectedValue == "2")
        {
            HF_PBU_MinMaxAge.Value = "0;1680;28;Age(completed weeks or months)"; //0-5Year
            HF_PBU_MinMaxY.Value = "40;125;5"; //40-125 step 5
        }
        else if (DDLKurvaPBU.SelectedValue == "3")
        {
            HF_PBU_MinMaxAge.Value = "60;228;3;Age(completed months and years)"; //5-19Year step 3 month
            HF_PBU_MinMaxY.Value = "90;200;10"; //90-200 step 10
        }

        if (CheckBoxStandardValuePBU.Checked == true)
        {
            if (DDLKurvaPBU.SelectedValue == "3")
            {
                getCoordinatByChartType("PBU", int.Parse(HFGenderPasien.Value), "show", "month");
            }
            else
            {
                getCoordinatByChartType("PBU", int.Parse(HFGenderPasien.Value), "show", "day");
            }
            UpdatePanelChartTemplatePBU.Update();
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "DDLKurvaPBU_SelectedIndexChanged", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void CheckBoxStandardValuePBU_CheckedChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (CheckBoxStandardValuePBU.Checked == true)
        {
            if (DDLKurvaPBU.SelectedValue == "3")
            {
                getCoordinatByChartType("PBU", int.Parse(HFGenderPasien.Value), "show", "month");
            }
            else
            {
                getCoordinatByChartType("PBU", int.Parse(HFGenderPasien.Value), "show", "day");
            }

            //CheckBoxStandardValuePBU.Text = "  Hide Standard Value";
        }
        else if (CheckBoxStandardValuePBU.Checked == false)
        {
            if (DDLKurvaPBU.SelectedValue == "3")
            {
                getCoordinatByChartType("PBU", int.Parse(HFGenderPasien.Value), "hide", "month");
            }
            else
            {
                getCoordinatByChartType("PBU", int.Parse(HFGenderPasien.Value), "hide", "day");
            }

            //CheckBoxStandardValuePBU.Text = "  Show Standard Value";
        }

        UpdatePanelChartTemplatePBU.Update();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CheckBoxStandardValuePBU_CheckedChanged", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    #endregion

    #region BBU Action

    protected void BindChartBBU(List<PediatricChart> BBU_Data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (DDLKurvaBBU.SelectedValue == "3")
        {
            List<PediatricChart> BBU_Data_inmonth = CloneList(BBU_Data);
            foreach (PediatricChart data in BBU_Data_inmonth)
            {
                data.x = data.x / 28;
            }

            List<ChartData> BBU_Chart = getXYchart(BBU_Data_inmonth);
            HF_BBU_Data.Value = new JavaScriptSerializer().Serialize(BBU_Chart);
        }
        else
        {
            List<ChartData> BBU_Chart = getXYchart(BBU_Data);
            HF_BBU_Data.Value = new JavaScriptSerializer().Serialize(BBU_Chart);
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BindChartBBU", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonSaveBBUWeight_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        if (TxtAgeBBUYear.Text == "")
        {
            TxtAgeBBUYear.Text = "0";
        }
        if (TxtAgeBBUMonth.Text == "")
        {
            TxtAgeBBUMonth.Text = "0";
        }
        if (TxtAgeBBUDay.Text == "")
        {
            TxtAgeBBUDay.Text = "0";
        }
        if (TextBoxBBUWeight.Text == "")
        {
            TextBoxBBUWeight.Text = "0";
        }

        string age = "";
        decimal ageindays = 0;

        age = TxtAgeBBUYear.Text + "Y " + TxtAgeBBUMonth.Text + "M " + TxtAgeBBUDay.Text + "D";
        ageindays = (decimal.Parse(TxtAgeBBUYear.Text) * 336) + (decimal.Parse(TxtAgeBBUMonth.Text) * 28) + decimal.Parse(TxtAgeBBUDay.Text);

        PediatricChart n = new PediatricChart();
        n.chart_transaction_master_id = Guid.NewGuid();
        n.chart_type = "BBU";
        n.age_type = "DAY";
        n.age = age; //LabelAgePasienChart.Text;
        n.x = ageindays; //decimal.Parse(HFAgePasienDays.Value);
        n.y = decimal.Parse(TextBoxBBUWeight.Text.Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        n.verdict_note = "";
        n.isNew = true;
        ChartList.Add(n);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> BBU_Data = ChartList.Where(x => x.chart_type == "BBU").ToList().OrderBy(y => y.x).ToList();
            RepeaterBBU.DataSource = Helper.ToDataTable(BBU_Data);
            RepeaterBBU.DataBind();
            //List<ChartData> BBU_Chart = getXYchart(BBU_Data);
            //HF_BBU_Data.Value = new JavaScriptSerializer().Serialize(BBU_Chart);
            BindChartBBU(BBU_Data);


        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        divBBUData.Visible = false;
        resetBBU();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonSaveBBUWeight_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonUpdateBBUWeight_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        if (TxtAgeBBUYear.Text == "")
        {
            TxtAgeBBUYear.Text = "0";
        }
        if (TxtAgeBBUMonth.Text == "")
        {
            TxtAgeBBUMonth.Text = "0";
        }
        if (TxtAgeBBUDay.Text == "")
        {
            TxtAgeBBUDay.Text = "0";
        }
        if (TextBoxBBUWeight.Text == "")
        {
            TextBoxBBUWeight.Text = "0";
        }

        string age = "";
        decimal ageindays = 0;

        age = TxtAgeBBUYear.Text + "Y " + TxtAgeBBUMonth.Text + "M " + TxtAgeBBUDay.Text + "D";
        ageindays = (decimal.Parse(TxtAgeBBUYear.Text) * 336) + (decimal.Parse(TxtAgeBBUMonth.Text) * 28) + decimal.Parse(TxtAgeBBUDay.Text);

        PediatricChart n = ChartList.FirstOrDefault(x => x.chart_transaction_master_id == Guid.Parse(HFTempBBUMasterId.Value));
        //n.chart_transaction_master_id = Guid.Empty;
        //n.chart_type = "BBU";
        n.age = age; //LabelAgePasienChart.Text;
        n.x = ageindays; //decimal.Parse(HFAgePasienDays.Value);
        n.y = decimal.Parse(TextBoxBBUWeight.Text.Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        n.verdict_note = "";
        //ChartList.Add(n);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> BBU_Data = ChartList.Where(x => x.chart_type == "BBU").ToList().OrderBy(y => y.x).ToList();
            RepeaterBBU.DataSource = Helper.ToDataTable(BBU_Data);
            RepeaterBBU.DataBind();
            //List<ChartData> BBU_Chart = getXYchart(BBU_Data);
            //HF_BBU_Data.Value = new JavaScriptSerializer().Serialize(BBU_Chart);
            BindChartBBU(BBU_Data);
        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        divBBUData.Visible = false;
        resetBBU();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonUpdateBBUWeight_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonAddBBU_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        divBBUData.Visible = true;
        TxtAgeBBUDay.Text = HFAgePasienDays.Value;
        TxtAgeBBUMonth.Text = HFAgePasienMonths.Value;
        TxtAgeBBUYear.Text = HFAgePasienYears.Value;

        ButtonSaveBBUWeight.Visible = true;
        ButtonUpdateBBUWeight.Visible = false;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonAddBBU_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonCancelBBU_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        divBBUData.Visible = false;
        resetBBU();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonCancelBBU_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void resetBBU()
    {
        HFTempBBUMasterId.Value = "";
        TextBoxBBUWeight.Text = "";
        TxtAgeBBUYear.Text = "";
        TxtAgeBBUMonth.Text = "";
        TxtAgeBBUDay.Text = "";

    }

    protected void ButtonEditBBU_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int selIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;
        HiddenField HFBBUMasterId = (HiddenField)RepeaterBBU.Items[selIndex].FindControl("HFBBUMasterId");
        Label LabelBBUx = (Label)RepeaterBBU.Items[selIndex].FindControl("LabelBBUx");
        Label LabelBBUy = (Label)RepeaterBBU.Items[selIndex].FindControl("LabelBBUy");
        Label LabelBBUage = (Label)RepeaterBBU.Items[selIndex].FindControl("LabelBBUage");

        string[] arrage = LabelBBUage.Text.Split(' ');

        HFTempBBUMasterId.Value = HFBBUMasterId.Value;
        TextBoxBBUWeight.Text = LabelBBUy.Text;
        TxtAgeBBUYear.Text = arrage[0].Remove(arrage[0].Length - 1, 1);
        TxtAgeBBUMonth.Text = arrage[1].Remove(arrage[1].Length - 1, 1);
        TxtAgeBBUDay.Text = arrage[2].Remove(arrage[2].Length - 1, 1);

        divBBUData.Visible = true;

        ButtonSaveBBUWeight.Visible = false;
        ButtonUpdateBBUWeight.Visible = true;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonEditBBU_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonDeleteBBU_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int selIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;
        HiddenField HFBBUMasterId = (HiddenField)RepeaterBBU.Items[selIndex].FindControl("HFBBUMasterId");

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        PediatricChart del = ChartList.Where(x => x.chart_transaction_master_id == Guid.Parse(HFBBUMasterId.Value)).SingleOrDefault();
        ChartList.Remove(del);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> BBU_Data = ChartList.Where(x => x.chart_type == "BBU").ToList().OrderBy(y => y.x).ToList();

            if (BBU_Data.Count != 0)
            {
                RepeaterBBU.DataSource = Helper.ToDataTable(BBU_Data);
                RepeaterBBU.DataBind();
                //List<ChartData> BBU_Chart = getXYchart(BBU_Data);
                //HF_BBU_Data.Value = new JavaScriptSerializer().Serialize(BBU_Chart);
                BindChartBBU(BBU_Data);
            }
            else
            {
                RepeaterBBU.DataSource = null;
                RepeaterBBU.DataBind();
                List<ChartData> BBU_Chart = new List<ChartData>();
                HF_BBU_Data.Value = new JavaScriptSerializer().Serialize(BBU_Chart);
            }
        }
        else
        {
            RepeaterBBU.DataSource = null;
            RepeaterBBU.DataBind();
            List<ChartData> BBU_Chart = new List<ChartData>();
            HF_BBU_Data.Value = new JavaScriptSerializer().Serialize(BBU_Chart);
        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonDeleteBBU_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void DDLKurvaBBU_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];
        List<PediatricChart> BBU_Data = ChartList.Where(x => x.chart_type == "BBU").ToList().OrderBy(y => y.x).ToList();
        BindChartBBU(BBU_Data);

        if (DDLKurvaBBU.SelectedValue == "0")
        {
            HF_BBU_MinMaxAge.Value = "0;168;7;Age(completed weeks or months)"; //0-6Month step 7 days
            HF_BBU_MinMaxY.Value = "0;11;1"; //0-11 step 1
        }
        else if (DDLKurvaBBU.SelectedValue == "1")
        {
            HF_BBU_MinMaxAge.Value = "0;672;28;Age(completed weeks or months)"; //0-2Year step 7 days
            HF_BBU_MinMaxY.Value = "0;18;1"; //0-18 step 1
        }
        else if (DDLKurvaBBU.SelectedValue == "2")
        {
            HF_BBU_MinMaxAge.Value = "0;1680;28;Age(completed weeks or months)"; //0-5Year step 7 days
            HF_BBU_MinMaxY.Value = "0;30;2"; //0-30 step 2
        }
        else if (DDLKurvaBBU.SelectedValue == "3")
        {
            HF_BBU_MinMaxAge.Value = "60;120;3;Age(completed months and years)"; //5-10Year step 3 month
            HF_BBU_MinMaxY.Value = "0;60;5"; //0-60 step 5
        }

        if (CheckBoxStandardValueBBU.Checked == true)
        {
            if (DDLKurvaBBU.SelectedValue == "3")
            {
                getCoordinatByChartType("BBU", int.Parse(HFGenderPasien.Value), "show", "month");
            }
            else
            {
                getCoordinatByChartType("BBU", int.Parse(HFGenderPasien.Value), "show", "day");
            }

            UpdatePanelChartTemplateBBU.Update();
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "DDLKurvaBBU_SelectedIndexChanged", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void CheckBoxStandardValueBBU_CheckedChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (CheckBoxStandardValueBBU.Checked == true)
        {
            if (DDLKurvaBBU.SelectedValue == "3")
            {
                getCoordinatByChartType("BBU", int.Parse(HFGenderPasien.Value), "show", "month");
            }
            else
            {
                getCoordinatByChartType("BBU", int.Parse(HFGenderPasien.Value), "show", "day");
            }

            //CheckBoxStandardValueBBU.Text = "  Hide Standard Value";
        }
        else if (CheckBoxStandardValueBBU.Checked == false)
        {
            if (DDLKurvaBBU.SelectedValue == "3")
            {
                getCoordinatByChartType("BBU", int.Parse(HFGenderPasien.Value), "hide", "month");
            }
            else
            {
                getCoordinatByChartType("BBU", int.Parse(HFGenderPasien.Value), "hide", "day");
            }

            //CheckBoxStandardValueBBU.Text = "  Show Standard Value";
        }

        UpdatePanelChartTemplateBBU.Update();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CheckBoxStandardValueBBU_CheckedChanged", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    #endregion

    #region BBPB Action

    protected void BindChartBBPB(List<PediatricChart> BBPB_Data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (DDLKurvaBBPB.SelectedValue == "3")
        {
            List<PediatricChart> BBPB_Data_inmonth = CloneList(BBPB_Data);
            foreach (PediatricChart data in BBPB_Data_inmonth)
            {
                data.x = data.x / 28;
            }

            List<ChartData> BBPB_Chart = getXYchart(BBPB_Data_inmonth);
            HF_BBPB_Data.Value = new JavaScriptSerializer().Serialize(BBPB_Chart);
        }
        else
        {
            List<ChartData> BBPB_Chart = getXYchart(BBPB_Data);
            HF_BBPB_Data.Value = new JavaScriptSerializer().Serialize(BBPB_Chart);
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BindChartBBPB", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonSaveBBPBWeight_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        if (TxtAgeBBPBYear.Text == "")
        {
            TxtAgeBBPBYear.Text = "0";
        }
        if (TxtAgeBBPBMonth.Text == "")
        {
            TxtAgeBBPBMonth.Text = "0";
        }
        if (TxtAgeBBPBDay.Text == "")
        {
            TxtAgeBBPBDay.Text = "0";
        }
        if (TextBoxBBPBLength.Text == "")
        {
            TextBoxBBPBLength.Text = "0";
        }
        if (TextBoxBBPBWeight.Text == "")
        {
            TextBoxBBPBWeight.Text = "0";
        }

        string age = "";
        //decimal ageindays = 0;

        age = TxtAgeBBPBYear.Text + "Y " + TxtAgeBBPBMonth.Text + "M " + TxtAgeBBPBDay.Text + "D";
        //ageindays = (decimal.Parse(TxtAgeBBPBYear.Text) * 336) + (decimal.Parse(TxtAgeBBPBMonth.Text) * 28) + decimal.Parse(TxtAgeBBPBDay.Text);

        PediatricChart n = new PediatricChart();
        n.chart_transaction_master_id = Guid.NewGuid();
        n.chart_type = "BBPB";
        n.age_type = "DAY";
        n.age = age; //LabelAgePasienChart.Text;
        //n.x = ageindays; //decimal.Parse(HFAgePasienDays.Value);
        n.x = decimal.Parse(TextBoxBBPBLength.Text.Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        n.y = decimal.Parse(TextBoxBBPBWeight.Text.Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        n.verdict_note = "";
        n.isNew = true;
        ChartList.Add(n);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> BBPB_Data = ChartList.Where(x => x.chart_type == "BBPB").ToList();
            RepeaterBBPB.DataSource = Helper.ToDataTable(BBPB_Data);
            RepeaterBBPB.DataBind();
            //List<ChartData> BBPB_Chart = getXYchart(BBPB_Data);
            //HF_BBPB_Data.Value = new JavaScriptSerializer().Serialize(BBPB_Chart);
            BindChartBBPB(BBPB_Data);
        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        divBBPBData.Visible = false;
        resetBBPB();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonSaveBBPBWeight_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonUpdateBBPBWeight_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        if (TxtAgeBBPBYear.Text == "")
        {
            TxtAgeBBPBYear.Text = "0";
        }
        if (TxtAgeBBPBMonth.Text == "")
        {
            TxtAgeBBPBMonth.Text = "0";
        }
        if (TxtAgeBBPBDay.Text == "")
        {
            TxtAgeBBPBDay.Text = "0";
        }
        if (TextBoxBBPBLength.Text == "")
        {
            TextBoxBBPBLength.Text = "0";
        }
        if (TextBoxBBPBWeight.Text == "")
        {
            TextBoxBBPBWeight.Text = "0";
        }

        string age = "";
        //decimal ageindays = 0;

        age = TxtAgeBBPBYear.Text + "Y " + TxtAgeBBPBMonth.Text + "M " + TxtAgeBBPBDay.Text + "D";
        //ageindays = (decimal.Parse(TxtAgeBBPBYear.Text) * 336) + (decimal.Parse(TxtAgeBBPBMonth.Text) * 28) + decimal.Parse(TxtAgeBBPBDay.Text);

        PediatricChart n = ChartList.FirstOrDefault(x => x.chart_transaction_master_id == Guid.Parse(HFTempBBPBMasterId.Value));
        //n.chart_transaction_master_id = Guid.Empty;
        //n.chart_type = "BBPB";
        n.age = age; //LabelAgePasienChart.Text;
        //n.x = ageindays; //decimal.Parse(HFAgePasienDays.Value);
        n.x = decimal.Parse(TextBoxBBPBLength.Text.Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        n.y = decimal.Parse(TextBoxBBPBWeight.Text.Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        n.verdict_note = "";
        //ChartList.Add(n);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> BBPB_Data = ChartList.Where(x => x.chart_type == "BBPB").ToList();
            RepeaterBBPB.DataSource = Helper.ToDataTable(BBPB_Data);
            RepeaterBBPB.DataBind();
            //List<ChartData> BBPB_Chart = getXYchart(BBPB_Data);
            //HF_BBPB_Data.Value = new JavaScriptSerializer().Serialize(BBPB_Chart);
            BindChartBBPB(BBPB_Data);
        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        divBBPBData.Visible = false;
        resetBBPB();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonUpdateBBPBWeight_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonAddBBPB_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        divBBPBData.Visible = true;
        TxtAgeBBPBDay.Text = HFAgePasienDays.Value;
        TxtAgeBBPBMonth.Text = HFAgePasienMonths.Value;
        TxtAgeBBPBYear.Text = HFAgePasienYears.Value;

        ButtonSaveBBPBWeight.Visible = true;
        ButtonUpdateBBPBWeight.Visible = false;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonAddBBPB_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonCancelBBPB_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        divBBPBData.Visible = false;
        resetBBPB();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonCancelBBPB_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void resetBBPB()
    {
        HFTempBBPBMasterId.Value = "";
        TextBoxBBPBLength.Text = "";
        TextBoxBBPBWeight.Text = "";
        TxtAgeBBPBYear.Text = "";
        TxtAgeBBPBMonth.Text = "";
        TxtAgeBBPBDay.Text = "";

    }

    protected void ButtonEditBBPB_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int selIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;
        HiddenField HFBBPBMasterId = (HiddenField)RepeaterBBPB.Items[selIndex].FindControl("HFBBPBMasterId");
        Label LabelBBPBx = (Label)RepeaterBBPB.Items[selIndex].FindControl("LabelBBPBx");
        Label LabelBBPBy = (Label)RepeaterBBPB.Items[selIndex].FindControl("LabelBBPBy");
        Label LabelBBPBage = (Label)RepeaterBBPB.Items[selIndex].FindControl("LabelBBPBage");

        string[] arrage = LabelBBPBage.Text.Split(' ');

        HFTempBBPBMasterId.Value = HFBBPBMasterId.Value;
        TextBoxBBPBLength.Text = LabelBBPBx.Text;
        TextBoxBBPBWeight.Text = LabelBBPBy.Text;
        TxtAgeBBPBYear.Text = arrage[0].Remove(arrage[0].Length - 1, 1);
        TxtAgeBBPBMonth.Text = arrage[1].Remove(arrage[1].Length - 1, 1);
        TxtAgeBBPBDay.Text = arrage[2].Remove(arrage[2].Length - 1, 1);

        divBBPBData.Visible = true;

        ButtonSaveBBPBWeight.Visible = false;
        ButtonUpdateBBPBWeight.Visible = true;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonEditBBPB_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonDeleteBBPB_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int selIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;
        HiddenField HFBBPBMasterId = (HiddenField)RepeaterBBPB.Items[selIndex].FindControl("HFBBPBMasterId");

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        PediatricChart del = ChartList.Where(x => x.chart_transaction_master_id == Guid.Parse(HFBBPBMasterId.Value)).SingleOrDefault();
        ChartList.Remove(del);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> BBPB_Data = ChartList.Where(x => x.chart_type == "BBPB").ToList();

            if (BBPB_Data.Count != 0)
            {
                RepeaterBBPB.DataSource = Helper.ToDataTable(BBPB_Data);
                RepeaterBBPB.DataBind();
                //List<ChartData> BBPB_Chart = getXYchart(BBPB_Data);
                //HF_BBPB_Data.Value = new JavaScriptSerializer().Serialize(BBPB_Chart);
                BindChartBBPB(BBPB_Data);
            }
            else
            {
                RepeaterBBPB.DataSource = null;
                RepeaterBBPB.DataBind();
                List<ChartData> BBPB_Chart = new List<ChartData>();
                HF_BBPB_Data.Value = new JavaScriptSerializer().Serialize(BBPB_Chart);
            }
        }
        else
        {
            RepeaterBBPB.DataSource = null;
            RepeaterBBPB.DataBind();
            List<ChartData> BBPB_Chart = new List<ChartData>();
            HF_BBPB_Data.Value = new JavaScriptSerializer().Serialize(BBPB_Chart);
        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonDeleteBBPB_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void DDLKurvaBBPB_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (DDLKurvaBBPB.SelectedValue == "1")
        {
            LabelHeaderBBPB.Text = "BB/PB (Berat Badan Menurut Panjang Badan)";
            HF_BBPB_MinMaxAge.Value = "0;672"; //0-2Year
            HF_BBPB_MinMaxX.Value = "45;110;5;Length(cm)"; //45-110 step 4 legend
            HF_BBPB_MinMaxY.Value = "0;24;2"; //0-24 step 2
        }
        else if (DDLKurvaBBPB.SelectedValue == "2")
        {
            LabelHeaderBBPB.Text = "BB/TB (Berat Badan Menurut Tinggi Badan)";
            HF_BBPB_MinMaxAge.Value = "0;1680"; //0-5Year
            HF_BBPB_MinMaxX.Value = "65;120;5;Height(cm)"; //45-120 step 4 legend
            HF_BBPB_MinMaxY.Value = "0;34;2"; //0-34 step 2
        }

        if (CheckBoxStandardValueBBPB.Checked == true)
        {
            if (DDLKurvaBBPB.SelectedValue == "2")
            {
                getCoordinatByChartType("BBTB", int.Parse(HFGenderPasien.Value), "show", "day");
            }
            else
            {
                getCoordinatByChartType("BBPB", int.Parse(HFGenderPasien.Value), "show", "day");
            }

            UpdatePanelChartTemplateBBPB.Update();
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "DDLKurvaBBPB_SelectedIndexChanged", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void CheckBoxStandardValueBBPB_CheckedChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (CheckBoxStandardValueBBPB.Checked == true)
        {
            if (DDLKurvaBBPB.SelectedValue == "2")
            {
                getCoordinatByChartType("BBTB", int.Parse(HFGenderPasien.Value), "show", "day");
            }
            else
            {
                getCoordinatByChartType("BBPB", int.Parse(HFGenderPasien.Value), "show", "day");
            }

            //CheckBoxStandardValueBBPB.Text = "  Hide Standard Value";
        }
        else if (CheckBoxStandardValueBBPB.Checked == false)
        {
            if (DDLKurvaBBPB.SelectedValue == "2")
            {
                getCoordinatByChartType("BBTB", int.Parse(HFGenderPasien.Value), "hide", "day");
            }
            else
            {
                getCoordinatByChartType("BBPB", int.Parse(HFGenderPasien.Value), "hide", "day");
            }

            //CheckBoxStandardValueBBPB.Text = "  Show Standard Value";
        }

        UpdatePanelChartTemplateBBPB.Update();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CheckBoxStandardValueBBPB_CheckedChanged", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    #endregion

    #region LKU Action

    protected void BindChartLKU(List<PediatricChart> LKU_Data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (DDLKurvaLKU.SelectedValue == "3")
        {
            List<PediatricChart> LKU_Data_inmonth = CloneList(LKU_Data);
            foreach (PediatricChart data in LKU_Data_inmonth)
            {
                data.x = data.x / 28;
            }

            List<ChartData> LKU_Chart = getXYchart(LKU_Data_inmonth);
            HF_LKU_Data.Value = new JavaScriptSerializer().Serialize(LKU_Chart);
        }
        else
        {
            List<ChartData> LKU_Chart = getXYchart(LKU_Data);
            HF_LKU_Data.Value = new JavaScriptSerializer().Serialize(LKU_Chart);
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BindChartLKU", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonSaveLKUHeadCirc_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        if (TxtAgeLKUYear.Text == "")
        {
            TxtAgeLKUYear.Text = "0";
        }
        if (TxtAgeLKUMonth.Text == "")
        {
            TxtAgeLKUMonth.Text = "0";
        }
        if (TxtAgeLKUDay.Text == "")
        {
            TxtAgeLKUDay.Text = "0";
        }
        if (TextBoxLKUHeadCirc.Text == "")
        {
            TextBoxLKUHeadCirc.Text = "0";
        }

        string age = "";
        decimal ageindays = 0;

        age = TxtAgeLKUYear.Text + "Y " + TxtAgeLKUMonth.Text + "M " + TxtAgeLKUDay.Text + "D";
        ageindays = (decimal.Parse(TxtAgeLKUYear.Text) * 336) + (decimal.Parse(TxtAgeLKUMonth.Text) * 28) + decimal.Parse(TxtAgeLKUDay.Text);

        PediatricChart n = new PediatricChart();
        n.chart_transaction_master_id = Guid.NewGuid();
        n.chart_type = "LKU";
        n.age_type = "DAY";
        n.age = age; //LabelAgePasienChart.Text;
        n.x = ageindays; //decimal.Parse(HFAgePasienDays.Value);
        n.y = decimal.Parse(TextBoxLKUHeadCirc.Text.Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        n.verdict_note = "";
        n.isNew = true;
        ChartList.Add(n);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> LKU_Data = ChartList.Where(x => x.chart_type == "LKU").ToList().OrderBy(y => y.x).ToList();
            RepeaterLKU.DataSource = Helper.ToDataTable(LKU_Data);
            RepeaterLKU.DataBind();
            //List<ChartData> LKU_Chart = getXYchart(LKU_Data);
            //HF_LKU_Data.Value = new JavaScriptSerializer().Serialize(LKU_Chart);
            BindChartLKU(LKU_Data);


        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        divLKUData.Visible = false;
        resetLKU();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonSaveLKUHeadCirc_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonUpdateLKUHeadCirc_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        if (TxtAgeLKUYear.Text == "")
        {
            TxtAgeLKUYear.Text = "0";
        }
        if (TxtAgeLKUMonth.Text == "")
        {
            TxtAgeLKUMonth.Text = "0";
        }
        if (TxtAgeLKUDay.Text == "")
        {
            TxtAgeLKUDay.Text = "0";
        }
        if (TextBoxLKUHeadCirc.Text == "")
        {
            TextBoxLKUHeadCirc.Text = "0";
        }

        string age = "";
        decimal ageindays = 0;

        age = TxtAgeLKUYear.Text + "Y " + TxtAgeLKUMonth.Text + "M " + TxtAgeLKUDay.Text + "D";
        ageindays = (decimal.Parse(TxtAgeLKUYear.Text) * 336) + (decimal.Parse(TxtAgeLKUMonth.Text) * 28) + decimal.Parse(TxtAgeLKUDay.Text);

        PediatricChart n = ChartList.FirstOrDefault(x => x.chart_transaction_master_id == Guid.Parse(HFTempLKUMasterId.Value));
        //n.chart_transaction_master_id = Guid.Empty;
        //n.chart_type = "LKU";
        n.age = age; //LabelAgePasienChart.Text;
        n.x = ageindays; //decimal.Parse(HFAgePasienDays.Value);
        n.y = decimal.Parse(TextBoxLKUHeadCirc.Text.Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        n.verdict_note = "";
        //ChartList.Add(n);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> LKU_Data = ChartList.Where(x => x.chart_type == "LKU").ToList().OrderBy(y => y.x).ToList();
            RepeaterLKU.DataSource = Helper.ToDataTable(LKU_Data);
            RepeaterLKU.DataBind();
            //List<ChartData> LKU_Chart = getXYchart(LKU_Data);
            //HF_LKU_Data.Value = new JavaScriptSerializer().Serialize(LKU_Chart);
            BindChartLKU(LKU_Data);


        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        divLKUData.Visible = false;
        resetLKU();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonUpdateLKUHeadCirc_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonAddLKU_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        divLKUData.Visible = true;
        TxtAgeLKUDay.Text = HFAgePasienDays.Value;
        TxtAgeLKUMonth.Text = HFAgePasienMonths.Value;
        TxtAgeLKUYear.Text = HFAgePasienYears.Value;

        ButtonSaveLKUHeadCirc.Visible = true;
        ButtonUpdateLKUHeadCirc.Visible = false;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonAddLKU_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonCancelLKU_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        divLKUData.Visible = false;
        resetLKU();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonCancelLKU_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void resetLKU()
    {
        HFTempLKUMasterId.Value = "";
        TextBoxLKUHeadCirc.Text = "";
        TxtAgeLKUYear.Text = "";
        TxtAgeLKUMonth.Text = "";
        TxtAgeLKUDay.Text = "";

    }

    protected void ButtonEditLKU_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int selIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;
        HiddenField HFLKUMasterId = (HiddenField)RepeaterLKU.Items[selIndex].FindControl("HFLKUMasterId");
        Label LabelLKUx = (Label)RepeaterLKU.Items[selIndex].FindControl("LabelLKUx");
        Label LabelLKUy = (Label)RepeaterLKU.Items[selIndex].FindControl("LabelLKUy");
        Label LabelLKUage = (Label)RepeaterLKU.Items[selIndex].FindControl("LabelLKUage");

        string[] arrage = LabelLKUage.Text.Split(' ');

        HFTempLKUMasterId.Value = HFLKUMasterId.Value;
        TextBoxLKUHeadCirc.Text = LabelLKUy.Text;
        TxtAgeLKUYear.Text = arrage[0].Remove(arrage[0].Length - 1, 1);
        TxtAgeLKUMonth.Text = arrage[1].Remove(arrage[1].Length - 1, 1);
        TxtAgeLKUDay.Text = arrage[2].Remove(arrage[2].Length - 1, 1);

        divLKUData.Visible = true;

        ButtonSaveLKUHeadCirc.Visible = false;
        ButtonUpdateLKUHeadCirc.Visible = true;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonEditLKU_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonDeleteLKU_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int selIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;
        HiddenField HFLKUMasterId = (HiddenField)RepeaterLKU.Items[selIndex].FindControl("HFLKUMasterId");

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        PediatricChart del = ChartList.Where(x => x.chart_transaction_master_id == Guid.Parse(HFLKUMasterId.Value)).SingleOrDefault();
        ChartList.Remove(del);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> LKU_Data = ChartList.Where(x => x.chart_type == "LKU").ToList().OrderBy(y => y.x).ToList();

            if (LKU_Data.Count != 0)
            {
                RepeaterLKU.DataSource = Helper.ToDataTable(LKU_Data);
                RepeaterLKU.DataBind();
                //List<ChartData> LKU_Chart = getXYchart(LKU_Data);
                //HF_LKU_Data.Value = new JavaScriptSerializer().Serialize(LKU_Chart);
                BindChartLKU(LKU_Data);
            }
            else
            {
                RepeaterLKU.DataSource = null;
                RepeaterLKU.DataBind();
                List<ChartData> LKU_Chart = new List<ChartData>();
                HF_LKU_Data.Value = new JavaScriptSerializer().Serialize(LKU_Chart);
            }
        }
        else
        {
            RepeaterLKU.DataSource = null;
            RepeaterLKU.DataBind();
            List<ChartData> LKU_Chart = new List<ChartData>();
            HF_LKU_Data.Value = new JavaScriptSerializer().Serialize(LKU_Chart);
        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonDeleteLKU_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void DDLKurvaLKU_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (DDLKurvaLKU.SelectedValue == "0")
        {
            HF_LKU_MinMaxAge.Value = "0;91;7;Age(weeks)"; //0-2Years
            HF_LKU_MinMaxY.Value = "30;45;1"; //30-45 step 1
        }
        else if (DDLKurvaLKU.SelectedValue == "1")
        {
            HF_LKU_MinMaxAge.Value = "0;672;28;Age(completed months and years)"; //0-2Years
            HF_LKU_MinMaxY.Value = "30;53;1"; //30-53 step 1
        }
        else if (DDLKurvaLKU.SelectedValue == "2")
        {
            HF_LKU_MinMaxAge.Value = "0;1680;28;Age(completed months and years)"; //0-5Years
            HF_LKU_MinMaxY.Value = "30;56;2"; //30-56 step 2
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "DDLKurvaLKU_SelectedIndexChanged", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void CheckBoxStandardValueLKU_CheckedChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (CheckBoxStandardValueLKU.Checked == true)
        {
            if (DDLKurvaLKU.SelectedValue == "3")
            {
                getCoordinatByChartType("LKU", int.Parse(HFGenderPasien.Value), "show", "month");
            }
            else
            {
                getCoordinatByChartType("LKU", int.Parse(HFGenderPasien.Value), "show", "day");
            }

            //CheckBoxStandardValueLKU.Text = "  Hide Standard Value";
        }
        else if (CheckBoxStandardValueLKU.Checked == false)
        {
            if (DDLKurvaLKU.SelectedValue == "3")
            {
                getCoordinatByChartType("LKU", int.Parse(HFGenderPasien.Value), "hide", "month");
            }
            else
            {
                getCoordinatByChartType("LKU", int.Parse(HFGenderPasien.Value), "hide", "day");
            }

            //CheckBoxStandardValueLKU.Text = "  Show Standard Value";
        }

        UpdatePanelChartTemplateLKU.Update();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CheckBoxStandardValueLKU_CheckedChanged", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    #endregion

    #region BMI Action

    protected void BindChartBMI(List<PediatricChart> BMI_Data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (DDLKurvaBMI.SelectedValue == "3")
        {
            List<PediatricChart> BMI_Data_inmonth = CloneList(BMI_Data);
            foreach (PediatricChart data in BMI_Data_inmonth)
            {
                data.x = data.x / 28;
            }

            List<ChartData> BMI_Chart = getXYchart(BMI_Data_inmonth);
            HF_BMI_Data.Value = new JavaScriptSerializer().Serialize(BMI_Chart);
        }
        else
        {
            List<ChartData> BMI_Chart = getXYchart(BMI_Data);
            HF_BMI_Data.Value = new JavaScriptSerializer().Serialize(BMI_Chart);
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BindChartBMI", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonSaveBMIWeight_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        if (TxtAgeBMIYear.Text == "")
        {
            TxtAgeBMIYear.Text = "0";
        }
        if (TxtAgeBMIMonth.Text == "")
        {
            TxtAgeBMIMonth.Text = "0";
        }
        if (TxtAgeBMIDay.Text == "")
        {
            TxtAgeBMIDay.Text = "0";
        }
        if (TextBoxBMIWeight.Text == "")
        {
            TextBoxBMIWeight.Text = "0";
        }

        string age = "";
        decimal ageindays = 0;

        age = TxtAgeBMIYear.Text + "Y " + TxtAgeBMIMonth.Text + "M " + TxtAgeBMIDay.Text + "D";
        ageindays = (decimal.Parse(TxtAgeBMIYear.Text) * 336) + (decimal.Parse(TxtAgeBMIMonth.Text) * 28) + decimal.Parse(TxtAgeBMIDay.Text);

        //if (ageindays > 1680)
        //{
        //    ageindays = ageindays / 28; //convert day to month
        //}

        PediatricChart n = new PediatricChart();
        n.chart_transaction_master_id = Guid.NewGuid();
        n.chart_type = "BMI";
        n.age_type = "DAY";
        n.age = age; //LabelAgePasienChart.Text;
        n.x = ageindays; //decimal.Parse(HFAgePasienDays.Value);
        n.y = decimal.Parse(TextBoxBMIWeight.Text.Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        n.verdict_note = "";
        n.isNew = true;
        ChartList.Add(n);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> BMI_Data = ChartList.Where(x => x.chart_type == "BMI").ToList().OrderBy(y => y.x).ToList();
            RepeaterBMI.DataSource = Helper.ToDataTable(BMI_Data);
            RepeaterBMI.DataBind();
            //List<ChartData> BMI_Chart = getXYchart(BMI_Data);
            //HF_BMI_Data.Value = new JavaScriptSerializer().Serialize(BMI_Chart);
            BindChartBMI(BMI_Data);


        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        divBMIData.Visible = false;
        resetBMI();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonSaveBMIWeight_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonUpdateBMIWeight_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        if (TxtAgeBMIYear.Text == "")
        {
            TxtAgeBMIYear.Text = "0";
        }
        if (TxtAgeBMIMonth.Text == "")
        {
            TxtAgeBMIMonth.Text = "0";
        }
        if (TxtAgeBMIDay.Text == "")
        {
            TxtAgeBMIDay.Text = "0";
        }
        if (TextBoxBMIWeight.Text == "")
        {
            TextBoxBMIWeight.Text = "0";
        }

        string age = "";
        decimal ageindays = 0;

        age = TxtAgeBMIYear.Text + "Y " + TxtAgeBMIMonth.Text + "M " + TxtAgeBMIDay.Text + "D";
        ageindays = (decimal.Parse(TxtAgeBMIYear.Text) * 336) + (decimal.Parse(TxtAgeBMIMonth.Text) * 28) + decimal.Parse(TxtAgeBMIDay.Text);

        //if (ageindays > 1680)
        //{
        //    ageindays = ageindays / 28;
        //}

        PediatricChart n = ChartList.FirstOrDefault(x => x.chart_transaction_master_id == Guid.Parse(HFTempBMIMasterId.Value));
        //n.chart_transaction_master_id = Guid.Empty;
        //n.chart_type = "BMI";
        n.age = age; //LabelAgePasienChart.Text;
        n.x = ageindays; //decimal.Parse(HFAgePasienDays.Value);
        n.y = decimal.Parse(TextBoxBMIWeight.Text.Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        n.verdict_note = "";
        //ChartList.Add(n);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> BMI_Data = ChartList.Where(x => x.chart_type == "BMI").ToList().OrderBy(y => y.x).ToList();
            RepeaterBMI.DataSource = Helper.ToDataTable(BMI_Data);
            RepeaterBMI.DataBind();
            //List<ChartData> BMI_Chart = getXYchart(BMI_Data);
            //HF_BMI_Data.Value = new JavaScriptSerializer().Serialize(BMI_Chart);
            BindChartBMI(BMI_Data);


        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        divBMIData.Visible = false;
        resetBMI();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonUpdateBMIWeight_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonAddBMI_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        divBMIData.Visible = true;
        TxtAgeBMIDay.Text = HFAgePasienDays.Value;
        TxtAgeBMIMonth.Text = HFAgePasienMonths.Value;
        TxtAgeBMIYear.Text = HFAgePasienYears.Value;

        ButtonSaveBMIWeight.Visible = true;
        ButtonUpdateBMIWeight.Visible = false;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonAddBMI_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonCancelBMI_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        divBMIData.Visible = false;
        resetBMI();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonCancelBMI_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void resetBMI()
    {
        HFTempBMIMasterId.Value = "";
        TextBoxBMIWeight.Text = "";
        TxtAgeBMIYear.Text = "";
        TxtAgeBMIMonth.Text = "";
        TxtAgeBMIDay.Text = "";

    }

    protected void ButtonEditBMI_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int selIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;
        HiddenField HFBMIMasterId = (HiddenField)RepeaterBMI.Items[selIndex].FindControl("HFBMIMasterId");
        Label LabelBMIx = (Label)RepeaterBMI.Items[selIndex].FindControl("LabelBMIx");
        Label LabelBMIy = (Label)RepeaterBMI.Items[selIndex].FindControl("LabelBMIy");
        Label LabelBMIage = (Label)RepeaterBMI.Items[selIndex].FindControl("LabelBMIage");

        string[] arrage = LabelBMIage.Text.Split(' ');

        HFTempBMIMasterId.Value = HFBMIMasterId.Value;
        TextBoxBMIWeight.Text = LabelBMIy.Text;
        TxtAgeBMIYear.Text = arrage[0].Remove(arrage[0].Length - 1, 1);
        TxtAgeBMIMonth.Text = arrage[1].Remove(arrage[1].Length - 1, 1);
        TxtAgeBMIDay.Text = arrage[2].Remove(arrage[2].Length - 1, 1);

        divBMIData.Visible = true;

        ButtonSaveBMIWeight.Visible = false;
        ButtonUpdateBMIWeight.Visible = true;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonEditBMI_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonDeleteBMI_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int selIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;
        HiddenField HFBMIMasterId = (HiddenField)RepeaterBMI.Items[selIndex].FindControl("HFBMIMasterId");

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        PediatricChart del = ChartList.Where(x => x.chart_transaction_master_id == Guid.Parse(HFBMIMasterId.Value)).SingleOrDefault();
        ChartList.Remove(del);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> BMI_Data = ChartList.Where(x => x.chart_type == "BMI").ToList().OrderBy(y => y.x).ToList();

            if (BMI_Data.Count != 0)
            {
                RepeaterBMI.DataSource = Helper.ToDataTable(BMI_Data);
                RepeaterBMI.DataBind();
                //List<ChartData> BMI_Chart = getXYchart(BMI_Data);
                //HF_BMI_Data.Value = new JavaScriptSerializer().Serialize(BMI_Chart);
                BindChartBMI(BMI_Data);
            }
            else
            {
                RepeaterBMI.DataSource = null;
                RepeaterBMI.DataBind();
                List<ChartData> BMI_Chart = new List<ChartData>();
                HF_BMI_Data.Value = new JavaScriptSerializer().Serialize(BMI_Chart);
            }
        }
        else
        {
            RepeaterBMI.DataSource = null;
            RepeaterBMI.DataBind();
            List<ChartData> BMI_Chart = new List<ChartData>();
            HF_BMI_Data.Value = new JavaScriptSerializer().Serialize(BMI_Chart);
        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonDeleteBMI_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void DDLKurvaBMI_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];
        List<PediatricChart> BMI_Data = ChartList.Where(x => x.chart_type == "BMI").ToList().OrderBy(y => y.x).ToList();
        BindChartBMI(BMI_Data);

        if (DDLKurvaBMI.SelectedValue == "1")
        {
            HF_BMI_MinMaxAge.Value = "0;672;28;Age(completed months and years)"; //0-2Years
            HF_BMI_MinMaxY.Value = "9;23;1"; //9-23 step 1
        }
        else if (DDLKurvaBMI.SelectedValue == "2")
        {
            HF_BMI_MinMaxAge.Value = "0;1680;28;Age(completed months and years)"; //0-5Years
            HF_BMI_MinMaxY.Value = "9;23;1"; //9-23 step 1
        }
        else if (DDLKurvaBMI.SelectedValue == "3")
        {
            HF_BMI_MinMaxAge.Value = "60;228;3;Age(completed months and years)"; //5-19Years step 3 month
            HF_BMI_MinMaxY.Value = "10;36;2"; //10-36 step 2
        }

        if (CheckBoxStandardValueBMI.Checked == true)
        {
            if (DDLKurvaBMI.SelectedValue == "3")
            {
                getCoordinatByChartType("BMIU", int.Parse(HFGenderPasien.Value), "show", "month");
            }
            else
            {
                getCoordinatByChartType("BMIU", int.Parse(HFGenderPasien.Value), "show", "day");
            }
            UpdatePanelChartTemplateBMI.Update();
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "DDLKurvaBMI_SelectedIndexChanged", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void CheckBoxStandardValueBMI_CheckedChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (CheckBoxStandardValueBMI.Checked == true)
        {
            if (DDLKurvaBMI.SelectedValue == "3")
            {
                getCoordinatByChartType("BMIU", int.Parse(HFGenderPasien.Value), "show", "month");
            }
            else
            {
                getCoordinatByChartType("BMIU", int.Parse(HFGenderPasien.Value), "show", "day");
            }

            //CheckBoxStandardValueBMI.Text = "  Hide Standard Value";
        }
        else if (CheckBoxStandardValueBMI.Checked == false)
        {
            if (DDLKurvaBMI.SelectedValue == "3")
            {
                getCoordinatByChartType("BMIU", int.Parse(HFGenderPasien.Value), "hide", "month");
            }
            else
            {
                getCoordinatByChartType("BMIU", int.Parse(HFGenderPasien.Value), "hide", "day");
            }

            //CheckBoxStandardValueBMI.Text = "  Show Standard Value";
        }

        UpdatePanelChartTemplateBMI.Update();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CheckBoxStandardValueBMI_CheckedChanged", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    #endregion

    #region LLU Action

    protected void BindChartLLU(List<PediatricChart> LLU_Data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<ChartData> LLU_Chart = getXYchart(LLU_Data);
        HF_LLU_Data.Value = new JavaScriptSerializer().Serialize(LLU_Chart);

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BindChartLLU", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonSaveLLUWeight_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        if (TxtAgeLLUYear.Text == "")
        {
            TxtAgeLLUYear.Text = "0";
        }
        if (TxtAgeLLUMonth.Text == "")
        {
            TxtAgeLLUMonth.Text = "0";
        }
        if (TxtAgeLLUDay.Text == "")
        {
            TxtAgeLLUDay.Text = "0";
        }
        if (TextBoxLLUWeight.Text == "")
        {
            TextBoxLLUWeight.Text = "0";
        }

        string age = "";
        decimal ageindays = 0;

        age = TxtAgeLLUYear.Text + "Y " + TxtAgeLLUMonth.Text + "M " + TxtAgeLLUDay.Text + "D";
        ageindays = (decimal.Parse(TxtAgeLLUYear.Text) * 336) + (decimal.Parse(TxtAgeLLUMonth.Text) * 28) + decimal.Parse(TxtAgeLLUDay.Text);

        //if (ageindays > 1680)
        //{
        //    ageindays = ageindays / 28; //convert day to month
        //}

        PediatricChart n = new PediatricChart();
        n.chart_transaction_master_id = Guid.NewGuid();
        n.chart_type = "LLU";
        n.age_type = "DAY";
        n.age = age; //LabelAgePasienChart.Text;
        n.x = ageindays; //decimal.Parse(HFAgePasienDays.Value);
        n.y = decimal.Parse(TextBoxLLUWeight.Text.Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        n.verdict_note = "";
        n.isNew = true;
        ChartList.Add(n);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> LLU_Data = ChartList.Where(x => x.chart_type == "LLU").ToList().OrderBy(y => y.x).ToList();
            RepeaterLLU.DataSource = Helper.ToDataTable(LLU_Data);
            RepeaterLLU.DataBind();
            //List<ChartData> LLU_Chart = getXYchart(LLU_Data);
            //HF_LLU_Data.Value = new JavaScriptSerializer().Serialize(LLU_Chart);
            BindChartLLU(LLU_Data);


        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        divLLUData.Visible = false;
        resetLLU();

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonSaveLLUWeight_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonUpdateLLUWeight_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        if (TxtAgeLLUYear.Text == "")
        {
            TxtAgeLLUYear.Text = "0";
        }
        if (TxtAgeLLUMonth.Text == "")
        {
            TxtAgeLLUMonth.Text = "0";
        }
        if (TxtAgeLLUDay.Text == "")
        {
            TxtAgeLLUDay.Text = "0";
        }
        if (TextBoxLLUWeight.Text == "")
        {
            TextBoxLLUWeight.Text = "0";
        }

        string age = "";
        decimal ageindays = 0;

        age = TxtAgeLLUYear.Text + "Y " + TxtAgeLLUMonth.Text + "M " + TxtAgeLLUDay.Text + "D";
        ageindays = (decimal.Parse(TxtAgeLLUYear.Text) * 336) + (decimal.Parse(TxtAgeLLUMonth.Text) * 28) + decimal.Parse(TxtAgeLLUDay.Text);

        //if (ageindays > 1680)
        //{
        //    ageindays = ageindays / 28;
        //}

        PediatricChart n = ChartList.FirstOrDefault(x => x.chart_transaction_master_id == Guid.Parse(HFTempLLUMasterId.Value));
        //n.chart_transaction_master_id = Guid.Empty;
        //n.chart_type = "LLU";
        n.age = age; //LabelAgePasienChart.Text;
        n.x = ageindays; //decimal.Parse(HFAgePasienDays.Value);
        n.y = decimal.Parse(TextBoxLLUWeight.Text.Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        n.verdict_note = "";
        //ChartList.Add(n);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> LLU_Data = ChartList.Where(x => x.chart_type == "LLU").ToList().OrderBy(y => y.x).ToList();
            RepeaterLLU.DataSource = Helper.ToDataTable(LLU_Data);
            RepeaterLLU.DataBind();
            //List<ChartData> LLU_Chart = getXYchart(LLU_Data);
            //HF_LLU_Data.Value = new JavaScriptSerializer().Serialize(LLU_Chart);
            BindChartLLU(LLU_Data);


        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        divLLUData.Visible = false;
        resetLLU();

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonUpdateLLUWeight_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonAddLLU_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        divLLUData.Visible = true;
        TxtAgeLLUDay.Text = HFAgePasienDays.Value;
        TxtAgeLLUMonth.Text = HFAgePasienMonths.Value;
        TxtAgeLLUYear.Text = HFAgePasienYears.Value;

        ButtonSaveLLUWeight.Visible = true;
        ButtonUpdateLLUWeight.Visible = false;

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonAddLLU_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonCancelLLU_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        divLLUData.Visible = false;
        resetLLU();

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonCancelLLU_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void resetLLU()
    {
        HFTempLLUMasterId.Value = "";
        TextBoxLLUWeight.Text = "";
        TxtAgeLLUYear.Text = "";
        TxtAgeLLUMonth.Text = "";
        TxtAgeLLUDay.Text = "";

    }

    protected void ButtonEditLLU_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int selIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;
        HiddenField HFLLUMasterId = (HiddenField)RepeaterLLU.Items[selIndex].FindControl("HFLLUMasterId");
        Label LabelLLUx = (Label)RepeaterLLU.Items[selIndex].FindControl("LabelLLUx");
        Label LabelLLUy = (Label)RepeaterLLU.Items[selIndex].FindControl("LabelLLUy");
        Label LabelLLUage = (Label)RepeaterLLU.Items[selIndex].FindControl("LabelLLUage");

        string[] arrage = LabelLLUage.Text.Split(' ');

        HFTempLLUMasterId.Value = HFLLUMasterId.Value;
        TextBoxLLUWeight.Text = LabelLLUy.Text;
        TxtAgeLLUYear.Text = arrage[0].Remove(arrage[0].Length - 1, 1);
        TxtAgeLLUMonth.Text = arrage[1].Remove(arrage[1].Length - 1, 1);
        TxtAgeLLUDay.Text = arrage[2].Remove(arrage[2].Length - 1, 1);

        divLLUData.Visible = true;

        ButtonSaveLLUWeight.Visible = false;
        ButtonUpdateLLUWeight.Visible = true;

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonEditLLU_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonDeleteLLU_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int selIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;
        HiddenField HFLLUMasterId = (HiddenField)RepeaterLLU.Items[selIndex].FindControl("HFLLUMasterId");

        List<PediatricChart> ChartList = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];

        PediatricChart del = ChartList.Where(x => x.chart_transaction_master_id == Guid.Parse(HFLLUMasterId.Value)).SingleOrDefault();
        ChartList.Remove(del);

        if (ChartList.Count != 0)
        {
            List<PediatricChart> LLU_Data = ChartList.Where(x => x.chart_type == "LLU").ToList().OrderBy(y => y.x).ToList();

            if (LLU_Data.Count != 0)
            {
                RepeaterLLU.DataSource = Helper.ToDataTable(LLU_Data);
                RepeaterLLU.DataBind();
                //List<ChartData> LLU_Chart = getXYchart(LLU_Data);
                //HF_LLU_Data.Value = new JavaScriptSerializer().Serialize(LLU_Chart);
                BindChartLLU(LLU_Data);
            }
            else
            {
                RepeaterLLU.DataSource = null;
                RepeaterLLU.DataBind();
                List<ChartData> LLU_Chart = new List<ChartData>();
                HF_LLU_Data.Value = new JavaScriptSerializer().Serialize(LLU_Chart);
            }
        }
        else
        {
            RepeaterLLU.DataSource = null;
            RepeaterLLU.DataBind();
            List<ChartData> LLU_Chart = new List<ChartData>();
            HF_LLU_Data.Value = new JavaScriptSerializer().Serialize(LLU_Chart);
        }

        Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = ChartList;

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonDeleteLLU_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void CheckBoxStandardValueLLU_CheckedChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (CheckBoxStandardValueLLU.Checked == true)
        {
            if (DDLKurvaLLU.SelectedValue == "3")
            {
                getCoordinatByChartType("LLU", int.Parse(HFGenderPasien.Value), "show", "month");
            }
            else
            {
                getCoordinatByChartType("LLU", int.Parse(HFGenderPasien.Value), "show", "day");
            }

            //CheckBoxStandardValueLLU.Text = "  Hide Standard Value";
        }
        else if (CheckBoxStandardValueLLU.Checked == false)
        {
            if (DDLKurvaLLU.SelectedValue == "3")
            {
                getCoordinatByChartType("LLU", int.Parse(HFGenderPasien.Value), "hide", "month");
            }
            else
            {
                getCoordinatByChartType("LLU", int.Parse(HFGenderPasien.Value), "hide", "day");
            }

            //CheckBoxStandardValueLLU.Text = "  Show Standard Value";
        }

        UpdatePanelChartTemplateLLU.Update();

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CheckBoxStandardValueLLU_CheckedChanged", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    #endregion

    protected void CheckBoxStandardValue_CheckedChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (CheckBoxStandardValue.Checked == true)
        {
            getCoordinatByChartType("PBU", int.Parse(HFGenderPasien.Value), "show", "day");
            getCoordinatByChartType("BBU", int.Parse(HFGenderPasien.Value), "show", "day");
            getCoordinatByChartType("BBPB", int.Parse(HFGenderPasien.Value), "show", "day");
            getCoordinatByChartType("LKU", int.Parse(HFGenderPasien.Value), "show", "day");
            getCoordinatByChartType("BMIU", int.Parse(HFGenderPasien.Value), "show", "day");
            getCoordinatByChartType("LLU", int.Parse(HFGenderPasien.Value), "show", "day");

            //CheckBoxStandardValue.Text = "  Hide Standard Value";
        }
        else if (CheckBoxStandardValue.Checked == false)
        {
            getCoordinatByChartType("PBU", int.Parse(HFGenderPasien.Value), "hide", "day");
            getCoordinatByChartType("BBU", int.Parse(HFGenderPasien.Value), "hide", "day");
            getCoordinatByChartType("BBPB", int.Parse(HFGenderPasien.Value), "hide", "day");
            getCoordinatByChartType("LKU", int.Parse(HFGenderPasien.Value), "hide", "day");
            getCoordinatByChartType("BMIU", int.Parse(HFGenderPasien.Value), "hide", "day");
            getCoordinatByChartType("LLU", int.Parse(HFGenderPasien.Value), "hide", "day");

            //CheckBoxStandardValue.Text = "  Show Standard Value";
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CheckBoxStandardValue_CheckedChanged", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

}