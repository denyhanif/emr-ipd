using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Globalization;
using System.Threading.Tasks;

public partial class Form_General_ManageOrderSet : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public List<OrderSet> listDrugOrderSet = new List<OrderSet>();
    public List<OrderSet> listCompoundOrderSet = new List<OrderSet>();
    public List<OrderSet> listLaboratorySet = new List<OrderSet>();
    public List<TemplateSet> listTemplate_Set = new List<TemplateSet>();

    void setCompound()
    {
        if (Helper.GetFlagCompound(this) == "FALSE")
            Tab2.Visible = false;
        else
            Tab2.Visible = true;
    }

    void ShowToastr(string message, string title, string type)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastr_message",
             String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
    }

    public string setENG = "none";
    public string setIND = "none";

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

        string doctorid = Helper.GetDoctorID(this);

        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            if (Session[Helper.SESSIONmarker] == null)
            {
                Session[Helper.SESSIONmarker] = Helper.setInitializeMarker();
            }

            if (doctorid != "")
            {
                Session.Remove(Helper.SessionTemplateSOAP);
                //setCompound();
                getDataSet(doctorid);
                var orderSetType = (String) Session[Helper.ViewStateOrderSetType];

                if (orderSetType == "0" || orderSetType == null) { 
                    MainView.ActiveViewIndex = 0;
                    Tab1.CssClass = "Clicked";
                }else if (orderSetType == "1") { 
                    MainView.ActiveViewIndex = int.Parse(orderSetType);
                    Tab2.CssClass = "Clicked";
                }else if (orderSetType == "2") { 
                    MainView.ActiveViewIndex = int.Parse(orderSetType);
                    Tab3.CssClass = "Clicked";
                }else if (orderSetType == "3") { 
                    MainView.ActiveViewIndex = int.Parse(orderSetType);
                    Tab_S_cc.CssClass = "Clicked";
                }else if (orderSetType == "4") { 
                    MainView.ActiveViewIndex = int.Parse(orderSetType);
                    Tab_S_a.CssClass = "Clicked";
                }else if (orderSetType == "5") { 
                    MainView.ActiveViewIndex = int.Parse(orderSetType);
                    Tab_Objective.CssClass = "Clicked";
                }else if (orderSetType == "6") { 
                    MainView.ActiveViewIndex = int.Parse(orderSetType);
                    Tab_Analysis.CssClass = "Clicked";
                }else if (orderSetType == "7") { 
                    MainView.ActiveViewIndex = int.Parse(orderSetType);
                    Tab_Planning.CssClass = "Clicked";
                }

                Session.Remove(Helper.ViewStateOrderSetType);
            }

            List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];

            string SAVEORDERmarker = markerlist.Find(x => x.key == "SAVEORDERmarker").value;
            if (SAVEORDERmarker == "marked")
            {
                ShowToastr("The data has been saved.", "Success", "Success");
            }
            markerlist.Find(x => x.key == "SAVEORDERmarker").value = "unmarked";

            markerlist.Find(x => x.key == "DOBmarker").value = "marked";

            Session[Helper.SESSIONmarker] = markerlist;

            Log.Debug(LogLibrary.SaveLogNew(MyUser.GetHopeOrgID().ToString(), "Doctor_ID", MyUser.GetHopeUserID(), "Page_Load", StartTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            //Log.Info(LogConfig.LogEnd());
        }
    }

    protected void getDataSet(String doctorId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        DataTable dt;
        Task<String> dataOrderSet;
        ResultOrderSet JsonOrderSet;
        Dictionary<string, string> logParam = new Dictionary<string, string>();

        /*Set GridView For Drugs*/
        try
        {
            StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", doctorId },
                { "Is_Compound", "0" },
                { "Type", "1" }
            };
            //Log.Debug(LogConfig.LogStart("getOrderSet", logParam));
            dataOrderSet = clsOrderSet.getOrderSet(doctorId, 0, 1);
            JsonOrderSet = JsonConvert.DeserializeObject<ResultOrderSet>(dataOrderSet.Result.ToString());

            //Log.Debug(LogConfig.LogEnd("getOrderSet", JsonOrderSet.Status, JsonOrderSet.Message));

            listDrugOrderSet = JsonOrderSet.list;

            if (listDrugOrderSet.Count > 0)
            {
                dt = Helper.ToDataTable(listDrugOrderSet);
                drugsGrid.DataSource = dt;
                drugsGrid.DataBind();
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "Doctor_ID", doctorId, "getOrderSet", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataOrderSet.Result.ToString()));

        }
        catch (Exception ex){

            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "getOrderSet", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        /*Set GridView For Compund*/

        try {
            StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", doctorId },
                { "Is_Compound", "1" },
                { "Type", "1" }
            };
            //Log.Debug(LogConfig.LogStart("getOrderSet", logParam));
            dataOrderSet = clsOrderSet.getOrderSet(doctorId, 1, 1);
            JsonOrderSet = JsonConvert.DeserializeObject<ResultOrderSet>(dataOrderSet.Result.ToString());

            //Log.Debug(LogConfig.LogEnd("getOrderSet", JsonOrderSet.Status, JsonOrderSet.Message));

            listCompoundOrderSet = new List<OrderSet>();
            listCompoundOrderSet = JsonOrderSet.list;

            if (listCompoundOrderSet.Count > 0)
            {
                dt = Helper.ToDataTable(listCompoundOrderSet);
                compoundGrid.DataSource = dt;
                compoundGrid.DataBind();
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "Doctor_ID", doctorId, "getOrderSet", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataOrderSet.Result.ToString()));

        }
        catch (Exception ex){
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "getOrderSet", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        try {
            StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", doctorId },
                { "Is_Compound", "0" },
                { "Type", "2" }
            };
            //Log.Debug(LogConfig.LogStart("getOrderSet", logParam));
            dataOrderSet = clsOrderSet.getOrderSet(doctorId, 0, 2);
            JsonOrderSet = JsonConvert.DeserializeObject<ResultOrderSet>(dataOrderSet.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("getOrderSet", JsonOrderSet.Status, JsonOrderSet.Message));

            listLaboratorySet = new List<OrderSet>();
            listLaboratorySet = JsonOrderSet.list;

            if (listLaboratorySet.Count > 0)
            {
                dt = Helper.ToDataTable(listLaboratorySet);
                laboratoryGrid.DataSource = dt;
                laboratoryGrid.DataBind();
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "Doctor_ID", doctorId, "getOrderSet", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataOrderSet.Result.ToString()));

        }
        catch (Exception ex) {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "getOrderSet", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        Task<String> dataTemplateSet;
        ResultTemplateSet JsonTemplateSet;

        try
        {
            StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //Log.Debug(LogConfig.LogStart("getAllTemplateSet", LogConfig.LogParam("Doctor_Id", doctorId)));
            dataTemplateSet = clsTemplateSet.getAllTemplateSet(Int64.Parse(doctorId));
            JsonTemplateSet = JsonConvert.DeserializeObject<ResultTemplateSet>(dataTemplateSet.Result.ToString());
            
            //string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "Doctor_ID", doctorId, "getDataSet", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataTemplateSet.Result.ToString()));
            //Log.Debug(LogConfig.LogEnd("getAllTemplateSet", JsonTemplateSet.Status, JsonTemplateSet.Message));

            listTemplate_Set = new List<TemplateSet>();
            listTemplate_Set = JsonTemplateSet.list;
            Session[Helper.SessionTemplateSOAP] = listTemplate_Set;

            //Bind Data Subjective Chief Complaint
            var complaint = Helper.ToDataTable(listTemplate_Set).Select("soap_mapping_id = 'E851F782-8210-49EB-A074-F26C104F5DDF'");
            if (complaint.Count() != 0)
            {
                dt = complaint.CopyToDataTable(); //subjective chief complaint
                SubjectCCGrid.DataSource = dt;
                SubjectCCGrid.DataBind();
            }
            else
            {
                img_noData_S_cc.Style.Add("display", "block");
            }

            //Bind Data Subjective Anamnesis
            //code here
            var anamnesis = Helper.ToDataTable(listTemplate_Set).Select("soap_mapping_id = '2874A832-8503-4CAD-B5DD-535775E94AC0'");
            if (anamnesis.Count() > 0)
            {
                dt = anamnesis.CopyToDataTable(); //subjective chief complaint
                SubjectAGrid.DataSource = dt;
                SubjectAGrid.DataBind();
            }
            else
            {
                img_noData_S_a.Style.Add("display", "block");
            }

            //Bind Data Subjective Objective
            //code here
            var objective = Helper.ToDataTable(listTemplate_Set).Select("soap_mapping_id = '7218971C-E89F-4172-AE3C-B7FB855C1D6D'");
            if (objective.Count() > 0)
            {
                dt = objective.CopyToDataTable(); //subjective chief complaint
                ObjectiveGrid.DataSource = dt;
                ObjectiveGrid.DataBind();
            }
            else
            {
                img_noData_objective.Style.Add("display", "block");
            }

            //Bind Data Subjective Analysis
            //code here
            var assesment = Helper.ToDataTable(listTemplate_Set).Select("soap_mapping_id = 'D24D0881-7C06-4563-BF75-3A20B843DC47'");
            if (assesment.Count() != 0)
            {
                dt = assesment.CopyToDataTable(); //subjective chief complaint
                AnalysisGrid.DataSource = dt;
                AnalysisGrid.DataBind();
            }
            else
            {
                img_noData_analysis.Style.Add("display", "block");
            }

            //Bind Data Subjective Planning
            //code here
            var planning = Helper.ToDataTable(listTemplate_Set).Select("soap_mapping_id = '337A371F-BAF5-424A-BDC5-C320C277CAC6'");
            if (planning.Count() > 0)
            {
                dt = planning.CopyToDataTable(); //subjective chief complaint
                PlanningGrid.DataSource = dt;
                PlanningGrid.DataBind();
            }
            else
            {
                img_noData_planning.Style.Add("display", "block");
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "Doctor_ID", doctorId, "getAllTemplateSet", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "getAllTemplateSet", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
    }
    
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab_S_cc.CssClass = "Initial";
        Tab_S_a.CssClass = "Initial";
        Tab_Objective.CssClass = "Initial";
        Tab_Analysis.CssClass = "Initial";
        Tab_Planning.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        Tab3.CssClass = "Initial";
        Tab_S_cc.CssClass = "Initial";
        Tab_S_a.CssClass = "Initial";
        Tab_Objective.CssClass = "Initial";
        Tab_Analysis.CssClass = "Initial";
        Tab_Planning.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;
    }

    //test merging
    protected void Tab3_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Clicked";
        Tab_S_cc.CssClass = "Initial";
        Tab_S_a.CssClass = "Initial";
        Tab_Objective.CssClass = "Initial";
        Tab_Analysis.CssClass = "Initial";
        Tab_Planning.CssClass = "Initial";
        MainView.ActiveViewIndex = 2;
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)drugsGrid.HeaderRow.FindControl("chkAll");
        LinkButton btnDeleteAllChk = (LinkButton)drugsGrid.HeaderRow.FindControl("deletechkAll");
        Label Drug1 = (Label)drugsGrid.HeaderRow.FindControl("Drug1");
        Label Drug2 = (Label)drugsGrid.HeaderRow.FindControl("Drug2");
        Label Drug3 = (Label)drugsGrid.HeaderRow.FindControl("Drug3");
        if (chkAll.Checked == true)
        {
            foreach (GridViewRow grid in drugsGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Drug");
                chkItem.Checked = true;
            }
            btnDeleteAllChk.Visible = true;
            Drug1.Visible = false;
            Drug2.Visible = false;
            Drug3.Visible = false;
        }
        else
        {
            btnDeleteAllChk.Visible = false;
            Drug1.Visible = true;
            Drug2.Visible = true;
            Drug3.Visible = true;
            foreach (GridViewRow grid in drugsGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Drug");
                chkItem.Checked = false;
            }
        }
    }

    protected void chkDelete_Drug_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)drugsGrid.HeaderRow.FindControl("chkAll");
        LinkButton btnDeleteAllChk = (LinkButton)drugsGrid.HeaderRow.FindControl("deletechkAll");
        Label Drug1 = (Label)drugsGrid.HeaderRow.FindControl("Drug1");
        Label Drug2 = (Label)drugsGrid.HeaderRow.FindControl("Drug2");
        Label Drug3 = (Label)drugsGrid.HeaderRow.FindControl("Drug3");
        var count = 0;
        foreach (GridViewRow grid in drugsGrid.Rows)
        {
            CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Drug");
            if (chkItem.Checked)
            {
                btnDeleteAllChk.Visible = true;
                Drug1.Visible = false;
                Drug2.Visible = false;
                Drug3.Visible = false;
                count = count + 1;
            }
        }

        if (count == drugsGrid.Rows.Count)
        {
            chkAll.Checked = true;
        }
        else
        {
            if (count == 0)
            {
                btnDeleteAllChk.Visible = false;
                Drug1.Visible = true;
                Drug2.Visible = true;
                Drug3.Visible = true;
            }
            else
            {
                btnDeleteAllChk.Visible = true;
                Drug1.Visible = false;
                Drug2.Visible = false;
                Drug3.Visible = false;
            }
            chkAll.Checked = false;
        }
    }

    protected void btnDel_Drugs_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            List<OrderSet> deleteOrderSet = new List<OrderSet>();
            foreach (GridViewRow grid in drugsGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Drug");
                if (chkItem.Checked)
                {
                    TextBox txtId_Orderset = (TextBox)grid.FindControl("txtId_DrugOrderset");
                    Label txtItem_list = (Label)grid.FindControl("txtItem_list");
                    Label txtCreated_Date1 = (Label)grid.FindControl("txt_hf_create_date1");
                    HiddenField hfOrderSetName = (HiddenField)grid.FindControl("hfOrderSetName");

                    deleteOrderSet.Add(new OrderSet { id = txtId_Orderset.Text, created_date = txtCreated_Date1.Text, item_list = txtItem_list.Text, set_name = hfOrderSetName.Value });
                }
            }
            //Log.Debug(LogConfig.LogStart("deleteDrugsOrderSet", LogConfig.JsonToString(deleteOrderSet)));
            var result = clsOrderSet.deleteDrugsOrderSet(deleteOrderSet);
            var resultJson = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result);
            var Status = resultJson.Property("status").Value.ToString();
            var Message = resultJson.Property("message").Value.ToString();
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Drugs_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message.ToString()));
            //Log.Debug(LogConfig.LogEnd("deleteDrugsOrderSet", Status, Message));

            Session[Helper.ViewStateOrderSetType] = "0";

            Response.Redirect("~/Form/General/OrderSet/ManageOrderSet.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Drugs_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void chkAllCompound_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)compoundGrid.HeaderRow.FindControl("chkAllCompound");
        LinkButton btnDeleteAllChk = (LinkButton)compoundGrid.HeaderRow.FindControl("deletechkCompound");
        Label Compound1 = (Label)compoundGrid.HeaderRow.FindControl("Compound1");
        Label Compound2 = (Label)compoundGrid.HeaderRow.FindControl("Compound2");
        Label Compound3 = (Label)compoundGrid.HeaderRow.FindControl("Compound3");
        if (chkAll.Checked == true)
        {
            foreach (GridViewRow grid in compoundGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Compound");
                chkItem.Checked = true;
            }
            btnDeleteAllChk.Visible = true;
            Compound1.Visible = false;
            Compound2.Visible = false;
            Compound3.Visible = false;
        }
        else
        {
            btnDeleteAllChk.Visible = false;
            Compound1.Visible = true;
            Compound2.Visible = true;
            Compound3.Visible = true;
            foreach (GridViewRow grid in compoundGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Compound");
                chkItem.Checked = false;
            }
        }
    }

    protected void chkDelete_Compound_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)compoundGrid.HeaderRow.FindControl("chkAllCompound");
        LinkButton btnDeleteAllChk = (LinkButton)compoundGrid.HeaderRow.FindControl("deletechkCompound");
        Label Compound1 = (Label)compoundGrid.HeaderRow.FindControl("Compound1");
        Label Compound2 = (Label)compoundGrid.HeaderRow.FindControl("Compound2");
        Label Compound3 = (Label)compoundGrid.HeaderRow.FindControl("Compound3");
        var count = 0;
        foreach (GridViewRow grid in compoundGrid.Rows)
        {
            CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Compound");
            if (chkItem.Checked)
            {
                btnDeleteAllChk.Visible = true;
                Compound1.Visible = false;
                Compound2.Visible = false;
                Compound3.Visible = false;
                count = count + 1;
            }
        }

        if (count == compoundGrid.Rows.Count)
        {
            chkAll.Checked = true;
        }
        else
        {
            if (count == 0)
            {
                btnDeleteAllChk.Visible = false;
                Compound1.Visible = true;
                Compound2.Visible = true;
                Compound3.Visible = true;
            }
            chkAll.Checked = false;
        }
    }

    protected void btnDel_Compound_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            List<OrderSet> deleteOrderSet = new List<OrderSet>();
            foreach (GridViewRow grid in compoundGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Compound");
                if (chkItem.Checked)
                {
                    TextBox txtId_Orderset = (TextBox)grid.FindControl("txtId_CompoundOrderset");
                    Label txtItem_list = (Label)grid.FindControl("txtItem_Compoundlist");
                    Label txtCreated_Date2 = (Label)grid.FindControl("txt_hf_create_date2");
                    HiddenField hfOrderSetName = (HiddenField)grid.FindControl("hfCompoundSetName");

                    deleteOrderSet.Add(new OrderSet { id = txtId_Orderset.Text, created_date = txtCreated_Date2.Text, item_list = txtItem_list.Text, set_name = hfOrderSetName.Value });

                }
            }
            //Log.Debug(LogConfig.LogStart("deleteCompoundOrderSet", LogConfig.JsonToString(deleteOrderSet)));
            var result = clsOrderSet.deleteCompoundOrderSet(deleteOrderSet);
            var resultJson = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result);
            var Status = resultJson.Property("status").Value.ToString();
            var Message = resultJson.Property("message").Value.ToString();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Compound_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message.ToString()));
            //Log.Debug(LogConfig.LogEnd("deleteCompoundOrderSet", Status, Message));

            Session[Helper.ViewStateOrderSetType] = "1";

            Response.Redirect("~/Form/General/OrderSet/ManageOrderSet.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Compound_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    } 

    protected void chkAllLaboratory_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)laboratoryGrid.HeaderRow.FindControl("chkAllLaboratory");
        LinkButton btnDeleteAllChk = (LinkButton)laboratoryGrid.HeaderRow.FindControl("deletechkLaboratory");
        Label Laboratory1 = (Label)laboratoryGrid.HeaderRow.FindControl("Laboratory1");
        Label Laboratory2 = (Label)laboratoryGrid.HeaderRow.FindControl("Laboratory2");
        Label Laboratory3 = (Label)laboratoryGrid.HeaderRow.FindControl("Laboratory3");
        if (chkAll.Checked == true)
        {
            foreach (GridViewRow grid in laboratoryGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Laboratory");
                chkItem.Checked = true;
            }
            btnDeleteAllChk.Visible = true;
            Laboratory1.Visible = false;
            Laboratory2.Visible = false;
            Laboratory3.Visible = false;
        }
        else
        {
            btnDeleteAllChk.Visible = false;
            Laboratory1.Visible = true;
            Laboratory2.Visible = true;
            Laboratory3.Visible = true;
            foreach (GridViewRow grid in laboratoryGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Laboratory");
                chkItem.Checked = false;
            }
        }
    }

    protected void chkDelete_Laboratory_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)laboratoryGrid.HeaderRow.FindControl("chkAllLaboratory");
        LinkButton btnDeleteAllChk = (LinkButton)laboratoryGrid.HeaderRow.FindControl("deletechkLaboratory");
        Label Laboratory1 = (Label)laboratoryGrid.HeaderRow.FindControl("Laboratory1");
        Label Laboratory2 = (Label)laboratoryGrid.HeaderRow.FindControl("Laboratory2");
        Label Laboratory3 = (Label)laboratoryGrid.HeaderRow.FindControl("Laboratory3");
        var count = 0;
        foreach (GridViewRow grid in laboratoryGrid.Rows)
        {
            CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Laboratory");
            if (chkItem.Checked)
            {
                btnDeleteAllChk.Visible = true;
                Laboratory1.Visible = false;
                Laboratory2.Visible = false;
                Laboratory3.Visible = false;
                count = count + 1;
            }
        }

        if (count == laboratoryGrid.Rows.Count)
        {
            chkAll.Checked = true;
        }
        else
        {
            if (count == 0)
            {
                btnDeleteAllChk.Visible = false;
                Laboratory1.Visible = true;
                Laboratory2.Visible = true;
                Laboratory3.Visible = true;
            }
            chkAll.Checked = false;
        }
    }

    protected void btnDel_Laboratory_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            List<OrderSet> deleteOrderSet = new List<OrderSet>();
            foreach (GridViewRow grid in laboratoryGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Laboratory");
                if (chkItem.Checked)
                {
                    TextBox txtId_Orderset = (TextBox)grid.FindControl("txtId_LaboratoryOrderset");
                    Label txtItem_list = (Label)grid.FindControl("txtItem_Laboratorylist");
                    Label txtCreated_Date3 = (Label)grid.FindControl("txt_hf_create_date3");
                    HiddenField hfOrderSetName = (HiddenField)grid.FindControl("hfLaboratorySetName");

                    deleteOrderSet.Add(new OrderSet { id = txtId_Orderset.Text, created_date = txtCreated_Date3.Text, item_list = txtItem_list.Text, set_name = hfOrderSetName.Value });

                }
            }
            //Log.Debug(LogConfig.LogStart("deleteDrugsOrderSet", LogConfig.JsonToString(deleteOrderSet)));
            var result = clsOrderSet.deleteDrugsOrderSet(deleteOrderSet);
            var resultJson = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result);
            var Status = resultJson.Property("status").Value.ToString();
            var Message = resultJson.Property("message").Value.ToString();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Laboratory_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message.ToString()));
            //Log.Debug(LogConfig.LogEnd("deleteDrugsOrderSet", Status, Message));

            Session[Helper.ViewStateOrderSetType] = "2";

            Response.Redirect("~/Form/General/OrderSet/ManageOrderSet.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Laboratory_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }


    protected void Tab_S_cc_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab_S_cc.CssClass = "Clicked";
        Tab_S_a.CssClass = "Initial";
        Tab_Objective.CssClass = "Initial";
        Tab_Analysis.CssClass = "Initial";
        Tab_Planning.CssClass = "Initial";
        MainView.ActiveViewIndex = 3;
    } 

    protected void Tab_S_a_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab_S_cc.CssClass = "Initial";
        Tab_S_a.CssClass = "Clicked";
        Tab_Objective.CssClass = "Initial";
        Tab_Analysis.CssClass = "Initial";
        Tab_Planning.CssClass = "Initial";
        MainView.ActiveViewIndex = 4;
    }

    protected void Tab_Objective_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab_S_cc.CssClass = "Initial";
        Tab_S_a.CssClass = "Initial";
        Tab_Objective.CssClass = "Clicked";
        Tab_Analysis.CssClass = "Initial";
        Tab_Planning.CssClass = "Initial";
        MainView.ActiveViewIndex = 5;
    }

    protected void Tab_Analysis_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab_S_cc.CssClass = "Initial";
        Tab_S_a.CssClass = "Initial";
        Tab_Objective.CssClass = "Initial";
        Tab_Analysis.CssClass = "Clicked";
        Tab_Planning.CssClass = "Initial";
        MainView.ActiveViewIndex = 6;
    }

    protected void Tab_Planning_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        Tab_S_cc.CssClass = "Initial";
        Tab_S_a.CssClass = "Initial";
        Tab_Objective.CssClass = "Initial";
        Tab_Analysis.CssClass = "Initial";
        Tab_Planning.CssClass = "Clicked";
        MainView.ActiveViewIndex = 7;
    }

    protected void chkAllSubjectCC_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)SubjectCCGrid.HeaderRow.FindControl("chkAllSubjectCC");
        LinkButton btnDeleteAllChk = (LinkButton)SubjectCCGrid.HeaderRow.FindControl("deletechkSubjectCC");
        Label SubjectCC1 = (Label)SubjectCCGrid.HeaderRow.FindControl("SubjectCC1");
        Label SubjectCC2 = (Label)SubjectCCGrid.HeaderRow.FindControl("SubjectCC2");
        Label SubjectCC3 = (Label)SubjectCCGrid.HeaderRow.FindControl("SubjectCC3");
        if (chkAll.Checked == true)
        {
            foreach (GridViewRow grid in SubjectCCGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_SubjectCC");
                chkItem.Checked = true;
            }
            btnDeleteAllChk.Visible = true;
            SubjectCC1.Visible = false;
            SubjectCC2.Visible = false;
            SubjectCC3.Visible = false;
        }
        else
        {
            btnDeleteAllChk.Visible = false;
            SubjectCC1.Visible = true;
            SubjectCC2.Visible = true;
            SubjectCC3.Visible = true;
            foreach (GridViewRow grid in SubjectCCGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_SubjectCC");
                chkItem.Checked = false;
            }
        }
    }

    protected void chkDelete_SubjectCC_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)SubjectCCGrid.HeaderRow.FindControl("chkAllSubjectCC");
        LinkButton btnDeleteAllChk = (LinkButton)SubjectCCGrid.HeaderRow.FindControl("deletechkSubjectCC");
        Label SubjectCC1 = (Label)SubjectCCGrid.HeaderRow.FindControl("SubjectCC1");
        Label SubjectCC2 = (Label)SubjectCCGrid.HeaderRow.FindControl("SubjectCC2");
        Label SubjectCC3 = (Label)SubjectCCGrid.HeaderRow.FindControl("SubjectCC3");
        var count = 0;
        foreach (GridViewRow grid in SubjectCCGrid.Rows)
        {
            CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_SubjectCC");
            if (chkItem.Checked)
            {
                btnDeleteAllChk.Visible = true;
                SubjectCC1.Visible = false;
                SubjectCC2.Visible = false;
                SubjectCC3.Visible = false;
                count = count + 1;
            }
        }

        if (count == SubjectCCGrid.Rows.Count)
            chkAll.Checked = true;
        else
        {
            if (count == 0)
            {
                btnDeleteAllChk.Visible = false;
                SubjectCC1.Visible = true;
                SubjectCC2.Visible = true;
                SubjectCC3.Visible = true;
            }
            else
            {
                btnDeleteAllChk.Visible = true;
                SubjectCC1.Visible = false;
                SubjectCC2.Visible = false;
                SubjectCC3.Visible = false;
            }
            chkAll.Checked = false;
        }
    }

    protected void chkAllSubjectA_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)SubjectAGrid.HeaderRow.FindControl("chkAllSubjectA");
        LinkButton btnDeleteAllChk = (LinkButton)SubjectAGrid.HeaderRow.FindControl("deletechkSubjectA");
        Label SubjectA1 = (Label)SubjectAGrid.HeaderRow.FindControl("SubjectA1");
        Label SubjectA2 = (Label)SubjectAGrid.HeaderRow.FindControl("SubjectA2");
        Label SubjectA3 = (Label)SubjectAGrid.HeaderRow.FindControl("SubjectA3");
        if (chkAll.Checked == true)
        {
            foreach (GridViewRow grid in SubjectAGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_SubjectA");
                chkItem.Checked = true;
            }
            btnDeleteAllChk.Visible = true;
            SubjectA1.Visible = false;
            SubjectA2.Visible = false;
            SubjectA3.Visible = false;
        }
        else
        {
            btnDeleteAllChk.Visible = false;
            SubjectA1.Visible = true;
            SubjectA2.Visible = true;
            SubjectA3.Visible = true;
            foreach (GridViewRow grid in SubjectAGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_SubjectA");
                chkItem.Checked = false;
            }
        }
    }

    protected void chkDelete_SubjectA_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)SubjectAGrid.HeaderRow.FindControl("chkAllSubjectA");
        LinkButton btnDeleteAllChk = (LinkButton)SubjectAGrid.HeaderRow.FindControl("deletechkSubjectA");
        Label SubjectA1 = (Label)SubjectAGrid.HeaderRow.FindControl("SubjectA1");
        Label SubjectA2 = (Label)SubjectAGrid.HeaderRow.FindControl("SubjectA2");
        Label SubjectA3 = (Label)SubjectAGrid.HeaderRow.FindControl("SubjectA3");
        var count = 0;
        foreach (GridViewRow grid in SubjectAGrid.Rows)
        {
            CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_SubjectA");
            if (chkItem.Checked)
            {
                btnDeleteAllChk.Visible = true;
                SubjectA1.Visible = false;
                SubjectA2.Visible = false;
                SubjectA3.Visible = false;
                count = count + 1;
            }
        }

        if (count == SubjectAGrid.Rows.Count)
            chkAll.Checked = true;
        else
        {
            if (count == 0)
            {
                btnDeleteAllChk.Visible = false;
                SubjectA1.Visible = true;
                SubjectA2.Visible = true;
                SubjectA3.Visible = true;
            }
            else
            {
                btnDeleteAllChk.Visible = true;
                SubjectA1.Visible = false;
                SubjectA2.Visible = false;
                SubjectA3.Visible = false;
            }
            chkAll.Checked = false;
        }
    }

    protected void chkAllObjective_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)ObjectiveGrid.HeaderRow.FindControl("chkAllObjective");
        LinkButton btnDeleteAllChk = (LinkButton)ObjectiveGrid.HeaderRow.FindControl("deletechkObjective");
        Label Objective1 = (Label)ObjectiveGrid.HeaderRow.FindControl("Objective1");
        Label Objective2 = (Label)ObjectiveGrid.HeaderRow.FindControl("Objective2");
        Label Objective3 = (Label)ObjectiveGrid.HeaderRow.FindControl("Objective3");
        if (chkAll.Checked == true)
        {
            foreach (GridViewRow grid in ObjectiveGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Objective");
                chkItem.Checked = true;
            }
            btnDeleteAllChk.Visible = true;
            Objective1.Visible = false;
            Objective2.Visible = false;
            Objective3.Visible = false;
        }
        else
        {
            btnDeleteAllChk.Visible = false;
            Objective1.Visible = true;
            Objective2.Visible = true;
            Objective3.Visible = true;
            foreach (GridViewRow grid in ObjectiveGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Objective");
                chkItem.Checked = false;
            }
        }
    }

    protected void chkDelete_Objective_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)ObjectiveGrid.HeaderRow.FindControl("chkAllObjective");
        LinkButton btnDeleteAllChk = (LinkButton)ObjectiveGrid.HeaderRow.FindControl("deletechkObjective");
        Label Objective1 = (Label)ObjectiveGrid.HeaderRow.FindControl("Objective1");
        Label Objective2 = (Label)ObjectiveGrid.HeaderRow.FindControl("Objective2");
        Label Objective3 = (Label)ObjectiveGrid.HeaderRow.FindControl("Objective3");
        var count = 0;
        foreach (GridViewRow grid in ObjectiveGrid.Rows)
        {
            CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Objective");
            if (chkItem.Checked)
            {
                btnDeleteAllChk.Visible = true;
                Objective1.Visible = false;
                Objective2.Visible = false;
                Objective3.Visible = false;
                count = count + 1;
            }
        }

        if (count == ObjectiveGrid.Rows.Count)
            chkAll.Checked = true;
        else
        {
            if (count == 0)
            {
                btnDeleteAllChk.Visible = false;
                Objective1.Visible = true;
                Objective2.Visible = true;
                Objective3.Visible = true;
            }
            else
            {
                btnDeleteAllChk.Visible = true;
                Objective1.Visible = false;
                Objective2.Visible = false;
                Objective3.Visible = false;
            }
            chkAll.Checked = false;
        }
    }

    protected void chkAllAnalysis_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)AnalysisGrid.HeaderRow.FindControl("chkAllAnalysis");
        LinkButton btnDeleteAllChk = (LinkButton)AnalysisGrid.HeaderRow.FindControl("deletechkAnalysis");
        Label Analysis1 = (Label)AnalysisGrid.HeaderRow.FindControl("Analysis1");
        Label Analysis2 = (Label)AnalysisGrid.HeaderRow.FindControl("Analysis2");
        Label Analysis3 = (Label)AnalysisGrid.HeaderRow.FindControl("Analysis3");
        if (chkAll.Checked == true)
        {
            foreach (GridViewRow grid in AnalysisGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Analysis");
                chkItem.Checked = true;
            }
            btnDeleteAllChk.Visible = true;
            Analysis1.Visible = false;
            Analysis2.Visible = false;
            Analysis3.Visible = false;
        }
        else
        {
            btnDeleteAllChk.Visible = false;
            Analysis1.Visible = true;
            Analysis2.Visible = true;
            Analysis3.Visible = true;
            foreach (GridViewRow grid in AnalysisGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Analysis");
                chkItem.Checked = false;
            }
        }
    }

    protected void chkDelete_Analysis_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)AnalysisGrid.HeaderRow.FindControl("chkAllAnalysis");
        LinkButton btnDeleteAllChk = (LinkButton)AnalysisGrid.HeaderRow.FindControl("deletechkAnalysis");
        Label Analysis1 = (Label)AnalysisGrid.HeaderRow.FindControl("Analysis1");
        Label Analysis2 = (Label)AnalysisGrid.HeaderRow.FindControl("Analysis2");
        Label Analysis3 = (Label)AnalysisGrid.HeaderRow.FindControl("Analysis3");
        var count = 0;
        foreach (GridViewRow grid in AnalysisGrid.Rows)
        {
            CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Analysis");
            if (chkItem.Checked)
            {
                btnDeleteAllChk.Visible = true;
                Analysis1.Visible = false;
                Analysis2.Visible = false;
                Analysis3.Visible = false;
                count = count + 1;
            }
        }

        if (count == AnalysisGrid.Rows.Count)
            chkAll.Checked = true;
        else
        {
            if (count == 0)
            {
                btnDeleteAllChk.Visible = false;
                Analysis1.Visible = true;
                Analysis2.Visible = true;
                Analysis3.Visible = true;
            }
            else
            {
                btnDeleteAllChk.Visible = true;
                Analysis1.Visible = false;
                Analysis2.Visible = false;
                Analysis3.Visible = false;
            }
            chkAll.Checked = false;
        }
    }

    protected void chkAllPlanning_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)PlanningGrid.HeaderRow.FindControl("chkAllPlanning");
        LinkButton btnDeleteAllChk = (LinkButton)PlanningGrid.HeaderRow.FindControl("deletechkPlanning");
        Label Planning1 = (Label)PlanningGrid.HeaderRow.FindControl("Planning1");
        Label Planning2 = (Label)PlanningGrid.HeaderRow.FindControl("Planning2");
        Label Planning3 = (Label)PlanningGrid.HeaderRow.FindControl("Planning3");
        if (chkAll.Checked == true)
        {
            foreach (GridViewRow grid in PlanningGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Planning");
                chkItem.Checked = true;
            }
            btnDeleteAllChk.Visible = true;
            Planning1.Visible = false;
            Planning2.Visible = false;
            Planning3.Visible = false;
        }
        else
        {
            btnDeleteAllChk.Visible = false;
            Planning1.Visible = true;
            Planning2.Visible = true;
            Planning3.Visible = true;
            foreach (GridViewRow grid in PlanningGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Planning");
                chkItem.Checked = false;
            }
        }
    }

    protected void chkDelete_Planning_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)PlanningGrid.HeaderRow.FindControl("chkAllPlanning");
        LinkButton btnDeleteAllChk = (LinkButton)PlanningGrid.HeaderRow.FindControl("deletechkPlanning");
        Label Planning1 = (Label)PlanningGrid.HeaderRow.FindControl("Planning1");
        Label Planning2 = (Label)PlanningGrid.HeaderRow.FindControl("Planning2");
        Label Planning3 = (Label)PlanningGrid.HeaderRow.FindControl("Planning3");
        var count = 0;
        foreach (GridViewRow grid in PlanningGrid.Rows)
        {
            CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Planning");
            if (chkItem.Checked)
            {
                btnDeleteAllChk.Visible = true;
                Planning1.Visible = false;
                Planning2.Visible = false;
                Planning3.Visible = false;
                count = count + 1;
            }
        }

        if (count == PlanningGrid.Rows.Count)
            chkAll.Checked = true;
        else
        {
            if (count == 0)
            {
                btnDeleteAllChk.Visible = false;
                Planning1.Visible = true;
                Planning2.Visible = true;
                Planning3.Visible = true;
            }
            else
            {
                btnDeleteAllChk.Visible = true;
                Planning1.Visible = false;
                Planning2.Visible = false;
                Planning3.Visible = false;
            }
            chkAll.Checked = false;
        }
    }

    protected void btnDel_Subject_SS_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {

            string deleteOrderSet = "";
            foreach (GridViewRow grid in SubjectCCGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_SubjectCC");
                if (chkItem.Checked)
                {
                    HiddenField hfsubjectccName = (HiddenField)grid.FindControl("hfsubjectccName");
                    if (deleteOrderSet == "")
                        deleteOrderSet = hfsubjectccName.Value;
                    else
                        deleteOrderSet = deleteOrderSet + "," + hfsubjectccName.Value;
                }
            }
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", Helper.GetDoctorID(this).ToString() },
                { "Mapping_ID",  hfsubjectccMapping.Value },
                { "Template_Name", deleteOrderSet },
            };
            //Log.Debug(LogConfig.LogStart("deleteTemplateSOAP", logParam));
            var result = clsOrderSet.deleteTemplateSOAP(Int64.Parse(Helper.GetDoctorID(this)), hfsubjectccMapping.Value, deleteOrderSet);
            var resultJson = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result);
            var Status = resultJson.Property("status").Value.ToString();
            var Message = resultJson.Property("message").Value.ToString();
            
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Subject_SS_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message.ToString()));
            //Log.Debug(LogConfig.LogEnd("deleteTemplateSOAP", Status, Message));

            Session[Helper.ViewStateOrderSetType] = "3";

            Response.Redirect("~/Form/General/OrderSet/ManageOrderSet.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Subject_SS_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDel_Subject_A_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            string deleteOrderSet = "";
            foreach (GridViewRow grid in SubjectAGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_SubjectA");
                if (chkItem.Checked)
                {
                    HiddenField hfsubjectaName = (HiddenField)grid.FindControl("hfsubjectaName");
                    if (deleteOrderSet == "")
                        deleteOrderSet = hfsubjectaName.Value;
                    else
                        deleteOrderSet = deleteOrderSet + "," + hfsubjectaName.Value;
                }
            }
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", Helper.GetDoctorID(this).ToString() },
                { "Mapping_ID",  hfsubjectccMapping.Value },
                { "Template_Name", deleteOrderSet },
            };
            //Log.Debug(LogConfig.LogStart("deleteTemplateSOAP", logParam));
            var result = clsOrderSet.deleteTemplateSOAP(Int64.Parse(Helper.GetDoctorID(this)), hfsubjectaMapping.Value, deleteOrderSet);
            var resultJson = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result);
            var Status = resultJson.Property("status").Value.ToString();
            var Message = resultJson.Property("message").Value.ToString();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Subject_A_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message.ToString()));
            //Log.Debug(LogConfig.LogEnd("deleteTemplateSOAP", Status, Message));

            Session[Helper.ViewStateOrderSetType] = "4";

            Response.Redirect("~/Form/General/OrderSet/ManageOrderSet.aspx", false);
            Context.ApplicationInstance.CompleteRequest();

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Subject_A_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDel_Objective_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {

            string deleteOrderSet = "";
            foreach (GridViewRow grid in ObjectiveGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Objective");
                if (chkItem.Checked)
                {
                    HiddenField hfobjectiveName = (HiddenField)grid.FindControl("hfobjectiveName");
                    if (deleteOrderSet == "")
                        deleteOrderSet = hfobjectiveName.Value;
                    else
                        deleteOrderSet = deleteOrderSet + "," + hfobjectiveName.Value;
                }
            }
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", Helper.GetDoctorID(this).ToString() },
                { "Mapping_ID",  hfsubjectccMapping.Value },
                { "Template_Name", deleteOrderSet },
            };
            //Log.Debug(LogConfig.LogStart("deleteTemplateSOAP", logParam));
            var result = clsOrderSet.deleteTemplateSOAP(Int64.Parse(Helper.GetDoctorID(this)), hfObjectiveMapping.Value, deleteOrderSet);
            var resultJson = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result);
            var Status = resultJson.Property("status").Value.ToString();
            var Message = resultJson.Property("message").Value.ToString();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Objective_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message.ToString()));
            //Log.Debug(LogConfig.LogEnd("deleteTemplateSOAP", Status, Message));

            Session[Helper.ViewStateOrderSetType] = "5";

            Response.Redirect("~/Form/General/OrderSet/ManageOrderSet.aspx", false);
            Context.ApplicationInstance.CompleteRequest();

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Objective_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDel_Analysis_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {

            string deleteOrderSet = "";
            foreach (GridViewRow grid in AnalysisGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Analysis");
                if (chkItem.Checked)
                {
                    HiddenField hfanalysisName = (HiddenField)grid.FindControl("hfanalysisName");
                    if (deleteOrderSet == "")
                        deleteOrderSet = hfanalysisName.Value;
                    else
                        deleteOrderSet = deleteOrderSet + "," + hfanalysisName.Value;
                }
            }
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", Helper.GetDoctorID(this).ToString() },
                { "Mapping_ID",  hfsubjectccMapping.Value },
                { "Template_Name", deleteOrderSet },
            };
            //Log.Debug(LogConfig.LogStart("deleteTemplateSOAP", logParam));
            var result = clsOrderSet.deleteTemplateSOAP(Int64.Parse(Helper.GetDoctorID(this)), hfAnalysisMapping.Value, deleteOrderSet);
            var resultJson = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result);
            var Status = resultJson.Property("status").Value.ToString();
            var Message = resultJson.Property("message").Value.ToString();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Analysis_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message.ToString()));
            //Log.Debug(LogConfig.LogEnd("deleteTemplateSOAP", Status, Message));

            Session[Helper.ViewStateOrderSetType] = "6";

            Response.Redirect("~/Form/General/OrderSet/ManageOrderSet.aspx", false);
            Context.ApplicationInstance.CompleteRequest();

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Analysis_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDel_Planning_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            string deleteOrderSet = "";
            foreach (GridViewRow grid in PlanningGrid.Rows)
            {
                CheckBox chkItem = (CheckBox)grid.FindControl("chkDelete_Planning");
                if (chkItem.Checked)
                {
                    HiddenField hfplanningName = (HiddenField)grid.FindControl("hfplanningName");
                    if (deleteOrderSet == "")
                        deleteOrderSet = hfplanningName.Value;
                    else
                        deleteOrderSet = deleteOrderSet + "," + hfplanningName.Value;
                }
            }
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", Helper.GetDoctorID(this).ToString() },
                { "Mapping_ID",  hfsubjectccMapping.Value },
                { "Template_Name", deleteOrderSet },
            };
            //Log.Debug(LogConfig.LogStart("deleteTemplateSOAP", logParam));
            var result = clsOrderSet.deleteTemplateSOAP(Int64.Parse(Helper.GetDoctorID(this)), hfPlanning_Mapping.Value, deleteOrderSet);
            var resultJson = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result);
            var Status = resultJson.Property("status").Value.ToString();
            var Message = resultJson.Property("message").Value.ToString();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Planning_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message.ToString()));
            //Log.Debug(LogConfig.LogEnd("deleteTemplateSOAP", Status, Message));

            Session[Helper.ViewStateOrderSetType] = "7";

            Response.Redirect("~/Form/General/OrderSet/ManageOrderSet.aspx", false);
            Context.ApplicationInstance.CompleteRequest();

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DoctorID", Helper.GetDoctorID(this), "btnDel_Planning_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }
}