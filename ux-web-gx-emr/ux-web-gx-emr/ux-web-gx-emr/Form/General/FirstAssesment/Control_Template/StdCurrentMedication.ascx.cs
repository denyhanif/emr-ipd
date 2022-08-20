using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static FirstAssesment;

public partial class Form_General_Control_StdCurrentMedication : System.Web.UI.UserControl
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();

        if (!IsPostBack)
        {
        }
        CheckVisibleDiv();
    }

    public void initializevalue(List<SubjectiveFA> listsubjective, List<PatientDiseaseHistoryFA> listPatientDiseases, List<PatientSurgeryFA> listPatientSurgery, List<PatientAllergyFA> listPatientAllergy)
    {
        //listsubjective = Jsongetsoap.list.subjective;
        if (listsubjective.Count > 0)
        {
            foreach (SubjectiveFA x in listsubjective)
            {
                if (x.soap_mapping_id == Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6"))
                {//screening 
                    if (x.value.ToUpper() == "Temperature".ToUpper())
                        chkScreen1.Checked = true;
                    else if (x.value.ToUpper() == "Batuk darah".ToUpper())
                        chkScreen2.Checked = true;
                    else if (x.value.ToUpper() == "Berkeringat".ToUpper())
                        chkScreen3.Checked = true;
                    else if (x.value.ToUpper() == "Diare, mual & muntah".ToUpper())
                        chkScreen4.Checked = true;
                    else if (x.value.ToUpper() == "batuk".ToUpper())
                        chkScreen5.Checked = true;
                    else if (x.value.ToUpper() == "swollen".ToUpper())
                        chkScreen6.Checked = true;
                    else if (x.value.ToUpper() == "kemerahan".ToUpper())
                        chkScreen7.Checked = true;
                    else if (x.value.ToUpper() == "luka".ToUpper())
                        chkScreen8.Checked = true;
                }
                else if (x.soap_mapping_id == Guid.Parse("a41d51b7-9999-4045-b992-241f1fe679ca"))
                {//current medication
                    if (x.remarks != "")
                    {
                        txtPengobatan.Text = x.remarks;
                        rbPengobatan2.Checked = true;
                    }
                    else
                    {
                        rbPengobatan1.Checked = true;
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea"))
                {//nutrition
                    if (x.remarks != "")
                    {
                        txtNutrition.Text = x.remarks;
                        rbnutrisi2.Checked = true;
                    }
                    else
                    {
                        rbnutrisi1.Checked = true;
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48"))
                {//fasting
                    if (x.remarks != "")
                    {
                        txtFasting.Text = x.remarks;
                        rbpuasa2.Checked = true;
                    }
                    else
                    {
                        rbpuasa1.Checked = true;
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade"))
                {//endemic
                    if (x.remarks != "")
                    {
                        txtEndemic.Text = x.remarks;
                        rbkunjungan2.Checked = true;
                    }
                    else
                    {
                        rbkunjungan1.Checked = true;
                    }
                }
            }
        }

        int tempflagdisease = 0, tempflagdiseasefam = 0;
        if (listPatientDiseases.Count > 0)
        {
            foreach (PatientDiseaseHistoryFA x in listPatientDiseases)
            {
                if (x.disease_history_type == 1)
                {//patient disease
                    tempflagdisease = 1;
                    if (x.value == "Hypertension")
                        chkdisease1.Checked = true;
                    else if (x.value == "Stroke")
                        chkdisease2.Checked = true;
                    else if (x.value == "TBC")
                        chkdisease3.Checked = true;
                    else if (x.value == "Kidney")
                        chkdisease4.Checked = true;
                    else if (x.value == "Convulsive")
                        chkdisease5.Checked = true;
                    else if (x.value == "Heart")
                        chkdisease6.Checked = true;
                    else if (x.value == "Diabetes")
                        chkdisease7.Checked = true;
                    else if (x.value == "Asthma")
                        chkdisease8.Checked = true;
                    else if (x.value == "Hepatitis")
                        chkdisease9.Checked = true;
                    else if (x.value == "Cancer")
                        chkdisease10.Checked = true;
                    else if (x.value == "Lain-lain")
                        txtDisease.Text = x.remarks;
                }
                if (x.disease_history_type == 2)
                {//patient disease
                    tempflagdiseasefam = 1;
                    if (x.value == "Heart")
                        chkdiseasefam1.Checked = true;
                    else if (x.value == "Diabetes")
                        chkdiseasefam2.Checked = true;
                    else if (x.value == "Asthma")
                        chkdiseasefam3.Checked = true;
                    else if (x.value == "Hypertension")
                        chkdiseasefam4.Checked = true;
                    else if (x.value == "Cancer")
                        chkdiseasefam5.Checked = true;
                    else if (x.value == "Lain-lain")
                        txtDiseaseFam.Text = x.remarks;
                }
            }
            if (tempflagdisease == 1)
            {
                rbpribadi2.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript2", "ShowHideDiv2();", true);
            }
            else
            {
                rbpribadi1.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript2", "HideDiv2();", true);
            }
            if (tempflagdiseasefam == 1)
            {
                rbkeluarga2.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript3", "ShowHideDiv3();", true);
            }
            else
            {
                rbkeluarga1.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript3", "HideDiv3();", true);
            }
        }

        if (listPatientSurgery.Count > 0)
        {//patient surgery
            if (Helper.ToDataTable(listPatientSurgery).Select("is_delete = 0").Count() > 0)
            {
                DataTable dt = Helper.ToDataTable(listPatientSurgery);
                Session["CurrMedSurgery"] = dt;
                gvw_surgery.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
                gvw_surgery.DataBind();
            }
            rbOperas2.Checked = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript7", "ShowHideDiv7();", true);
        }
        else
        {
            rbOperasi.Checked = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript7", "HideDiv7();", true);
        }

        //int tempflagallergy = 0, tempflagfoods = 0;
        if (listPatientAllergy.Count > 0)
        {//patient allergy
            if (Helper.ToDataTable(listPatientAllergy).Select("is_delete = 0").Count() > 0)
            {
                DataTable dtAllergy = Helper.ToDataTable(listPatientAllergy);
                Session["CurrMedAllergy"] = dtAllergy;
                if (dtAllergy.Select("is_delete = 0 and allergy_type = 1").Count() > 0)
                {
                    gvw_allergy.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
                    gvw_allergy.DataBind();
                    rbdrug2.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript8", "ShowHideDiv8();", true);
                }
                else
                {
                    rbdrug1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript8", "HideDiv8();", true);
                }
                if (dtAllergy.Select("is_delete = 0 and allergy_type = 2").Count() > 0)
                {
                    gvw_foods.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 2").CopyToDataTable();
                    gvw_foods.DataBind();
                    rbfood2.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript9", "ShowHideDiv9();", true);
                }
                else
                {
                    rbfood1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript9", "HideDiv9();", true);
                }
            }
        }
        else
        {
            Session["CurrMedAllergy"] = null;
            rbdrug1.Checked = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript8", "HideDiv8();", true);
            rbfood1.Checked = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript9", "HideDiv9();", true);
        }
    }

    public void CheckVisibleDiv()
    {
        if (rbPengobatan2.Checked)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideDiv", "ShowHideDiv();", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDiv", "HideDiv();", true);

        if (rbOperas2.Checked)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideDiv7", "ShowHideDiv7();", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDiv7", "HideDiv7();", true);

        if (rbpribadi2.Checked)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideDiv2", "ShowHideDiv2();", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDiv2", "HideDiv2();", true);

        if (rbkeluarga2.Checked)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideDiv3", "ShowHideDiv3();", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDiv3", "HideDiv3();", true);

        if (rbkunjungan2.Checked)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideDiv4", "ShowHideDiv4();", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDiv4", "HideDiv4();", true);

        if (rbdrug2.Checked)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideDiv8", "ShowHideDiv8();", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDiv8", "HideDiv8();", true);

        if (rbfood2.Checked)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideDiv9", "ShowHideDiv9();", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDiv9", "HideDiv9();", true);

        if (rbnutrisi2.Checked)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideDiv5", "ShowHideDiv5();", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDiv5", "HideDiv5();", true);

        if (rbpuasa2.Checked)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideDiv6", "ShowHideDiv6();", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDiv6", "HideDiv6();", true);
    }

    protected void btnAddSurgery_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            DataTable dt = new DataTable();
            if (Session["CurrMedSurgery"] == null)
            {
                dt.Columns.Add("patient_surgery_id");
                dt.Columns.Add("surgery_type");
                dt.Columns.Add("surgery_date");
                dt.Columns.Add("is_delete");
                dt.Rows.Add(new Object[] { Guid.Empty, txtSurgeryName.Text, DateTime.Parse(txtSurgeryDate.Text), 0 });
            }
            else
            {
                dt = (DataTable)Session["CurrMedSurgery"];
                dt.Rows.Add(new Object[] { Guid.Empty, txtSurgeryName.Text, DateTime.Parse(txtSurgeryDate.Text), 0 });
            }

            Session["CurrMedSurgery"] = dt;
            gvw_surgery.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
            gvw_surgery.DataBind();
            CheckVisibleDiv();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "SurgeryName", txtSurgeryName.Text, "btnAddSurgery_onClick", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), txtSurgeryName.Text + "/" + txtSurgeryDate.Text, "", ""));
        }
        catch (Exception ex)
        {
            txtSurgeryDate.Text = "";
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "SurgeryName", txtSurgeryName.Text, "btnAddSurgery_onClick", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));


        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDeleteSurgery_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int selRowIndex = ((GridViewRow)(((Button)sender).Parent.Parent)).RowIndex;
        HiddenField patient_surgery_id = (HiddenField)gvw_surgery.Rows[selRowIndex].FindControl("patient_surgery_id");
        DataTable dt = ((DataTable)Session["CurrMedSurgery"]).Select("is_delete = 0").CopyToDataTable();
        dt.Rows[selRowIndex].SetField("is_delete", 1);

        if (dt.Select("is_delete = 0").Count() > 0)
        {
            Session["CurrMedSurgery"] = dt.Select("is_delete = 0").CopyToDataTable();
            gvw_surgery.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
            gvw_surgery.DataBind();
        }
        else
        {
            Session["CurrMedSurgery"] = null;
            gvw_surgery.DataSource = null;
            gvw_surgery.DataBind();
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript7", "ShowHideDiv7();", true);
        CheckVisibleDiv();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "patient_surgery_id", patient_surgery_id.Value, "btnDeleteSurgery_onClick", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), patient_surgery_id.Value, "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnAddAllergy_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        DataTable dt = new DataTable();
        if (Session["CurrMedAllergy"] == null)
        {
            dt.Columns.Add("patient_allergy_id");
            dt.Columns.Add("allergy_type");
            dt.Columns.Add("allergy");
            dt.Columns.Add("allergy_reaction");
            dt.Columns.Add("is_delete");
            dt.Rows.Add(new Object[] { Guid.Empty, 1, txtDrugsAllergy.Text, txtReactionAllergy.Text, 0 });
        }
        else
        {
            dt = (DataTable)Session["CurrMedAllergy"];
            dt.Rows.Add(new Object[] { Guid.Empty, 1, txtDrugsAllergy.Text, txtReactionAllergy.Text, 0 });
        }

        Session["CurrMedAllergy"] = dt;
        if (dt.Select("is_delete = 0 and allergy_type = 1").Count() > 0)
        {
            gvw_allergy.DataSource = dt.Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
            gvw_allergy.DataBind();
            rbdrug2.Checked = true;
        }
        else
        {
            rbdrug1.Checked = true;
        }
        CheckVisibleDiv();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DrugsAllergyName", txtDrugsAllergy.Text, "btnAddAllergy_onClick", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), txtDrugsAllergy.Text + "/" + txtReactionAllergy.Text, "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDeleteAllergy_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        DataTable dt;
        int selRowIndex = ((GridViewRow)(((Button)sender).Parent.Parent)).RowIndex;
        HiddenField patient_allergy_id = (HiddenField)gvw_allergy.Rows[selRowIndex].FindControl("patient_allergy_id");
        if (((DataTable)Session["CurrMedAllergy"]).Select("is_delete = 0 and allergy_type = 2").Count() > 0)
        {
            DataTable dttemp = ((DataTable)Session["CurrMedAllergy"]).Select("is_delete = 0 and allergy_type = 2").CopyToDataTable();
            dt = ((DataTable)Session["CurrMedAllergy"]).Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
            dt.Rows[selRowIndex].SetField("is_delete", 1);
            dt.Merge(dttemp);
        }
        else
        {
            dt = ((DataTable)Session["CurrMedAllergy"]).Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
            dt.Rows[selRowIndex].SetField("is_delete", 1);
        }

        if (dt.Select("is_delete = 0 and allergy_type = 1").Count() > 0)
        {
            Session["CurrMedAllergy"] = dt.Select("is_delete = 0").CopyToDataTable();
            gvw_allergy.DataSource = dt.Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
            gvw_allergy.DataBind();
        }
        else
        {
            if (dt.Select("is_delete = 0").Count() > 0)
            {
                Session["CurrMedAllergy"] = dt.Select("is_delete = 0").CopyToDataTable();
                gvw_allergy.DataSource = null;
                gvw_allergy.DataBind();
            }
            else
            {
                Session["CurrMedAllergy"] = null;
                gvw_allergy.DataSource = null;
                gvw_allergy.DataBind();
            }

        }
        CheckVisibleDiv();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "patient_allergy_id", patient_allergy_id.Value, "btnDeleteAllergy_onClick", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), patient_allergy_id.Value, "", ""));
       // Log.Info(LogConfig.LogEnd());
    }

    protected void btnAddFoodAllergy_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        DataTable dt = new DataTable();
        if (Session["CurrMedAllergy"] == null)
        {
            dt.Columns.Add("patient_allergy_id");
            dt.Columns.Add("allergy_type");
            dt.Columns.Add("allergy");
            dt.Columns.Add("allergy_reaction");
            dt.Columns.Add("is_delete");
            dt.Rows.Add(new Object[] { Guid.Empty, 2, txtDrugsFoods.Text, txtReactionFoods.Text, 0 });
        }
        else
        {
            dt = (DataTable)Session["CurrMedAllergy"];
            dt.Rows.Add(new Object[] { Guid.Empty, 2, txtDrugsFoods.Text, txtReactionFoods.Text, 0 });
        }

        Session["CurrMedAllergy"] = dt;
        if (dt.Select("is_delete = 0 and allergy_type = 2").Count() > 0)
        {
            gvw_foods.DataSource = dt.Select("is_delete = 0 and allergy_type = 2").CopyToDataTable();
            gvw_foods.DataBind();
            rbfood2.Checked = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript9", "ShowHideDiv9();", true);
        }
        else
        {
            rbfood1.Checked = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript9", "HideDiv9();", true);
        }
        CheckVisibleDiv();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "DrugsFoodsName", txtDrugsFoods.Text, "btnAddFoodAllergy_onClick", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), txtDrugsFoods.Text + "/" + txtReactionFoods.Text, "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDeleteFoods_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        DataTable dt;
        int selRowIndex = ((GridViewRow)(((Button)sender).Parent.Parent)).RowIndex;
        HiddenField patient_allergy_id = (HiddenField)gvw_foods.Rows[selRowIndex].FindControl("patient_allergy_id");
        if (((DataTable)Session["CurrMedAllergy"]).Select("is_delete = 0 and allergy_type = 1").Count() > 0)
        {
            DataTable dttemp = ((DataTable)Session["CurrMedAllergy"]).Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
            dt = ((DataTable)Session["CurrMedAllergy"]).Select("is_delete = 0 and allergy_type = 2").CopyToDataTable();
            dt.Rows[selRowIndex].SetField("is_delete", 1);
            dt.Merge(dttemp);
        }
        else
        {
             dt = ((DataTable)Session["CurrMedAllergy"]).Select("is_delete = 0 and allergy_type = 2").CopyToDataTable();
            dt.Rows[selRowIndex].SetField("is_delete", 1);
        }

        if (dt.Select("is_delete = 0 and allergy_type = 2").Count() > 0)
        {
            Session["CurrMedAllergy"] = dt.Select("is_delete = 0").CopyToDataTable();
            gvw_foods.DataSource = dt.Select("is_delete = 0 and allergy_type = 2").CopyToDataTable();
            gvw_foods.DataBind();
        }
        else
        {
            if (dt.Select("is_delete = 0").Count() > 0)
            {
                Session["CurrMedAllergy"] = dt.Select("is_delete = 0").CopyToDataTable();
                gvw_foods.DataSource = null;
                gvw_foods.DataBind();
            }
            else
            {
                Session["CurrMedAllergy"] = null;
                gvw_foods.DataSource = null;
                gvw_foods.DataBind();
            }
        }
        CheckVisibleDiv();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "patient_allergy_id", patient_allergy_id.Value, "btnDeleteFoods_onClick", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), patient_allergy_id.Value, "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected List<PatientSurgeryFA> GetRowList_PatientSurgery()
    {
        List<PatientSurgeryFA> data = new List<PatientSurgeryFA>();
        foreach (GridViewRow rows in gvw_surgery.Rows)
        {
            HiddenField patient_surgery_id = (HiddenField)rows.FindControl("patient_surgery_id");
            Label surgery_type = (Label)rows.FindControl("surgery_type");
            Label surgery_date = (Label)rows.FindControl("surgery_date");

            PatientSurgeryFA row = new PatientSurgeryFA();

            row.patient_surgery_id = Guid.Parse(patient_surgery_id.Value);
            row.surgery_type = surgery_type.Text;
            row.surgery_date = DateTime.Parse(surgery_date.Text);
            row.is_delete = 0;
            data.Add(row);
        }
        return data;
    }

    protected List<PatientAllergyFA> GetRowList_PatientAllergy(int type)
    {
        List<PatientAllergyFA> data = new List<PatientAllergyFA>();
        if (type == 1)
        {
            foreach (GridViewRow rows in gvw_allergy.Rows)
            {
                HiddenField patient_allergy_id = (HiddenField)rows.FindControl("patient_allergy_id");
                Label allergy = (Label)rows.FindControl("allergy");
                Label allergy_reaction = (Label)rows.FindControl("allergy_reaction");

                PatientAllergyFA row = new PatientAllergyFA();

                row.patient_allergy_id = Guid.Parse(patient_allergy_id.Value);
                row.allergy_type = 1;
                row.allergy = allergy.Text;
                row.allergy_reaction = allergy_reaction.Text;
                row.is_delete = 0;
                data.Add(row);
            }
        }
        else
        {
            foreach (GridViewRow rows in gvw_foods.Rows)
            {
                HiddenField patient_allergy_id = (HiddenField)rows.FindControl("patient_allergy_id");
                Label allergy = (Label)rows.FindControl("allergy");
                Label allergy_reaction = (Label)rows.FindControl("allergy_reaction");

                PatientAllergyFA row = new PatientAllergyFA();

                row.patient_allergy_id = Guid.Parse(patient_allergy_id.Value);
                row.allergy_type = 2;
                row.allergy = allergy.Text;
                row.allergy_reaction = allergy_reaction.Text;
                row.is_delete = 0;
                data.Add(row);
            }
        }
        return data;
    }

    public FirstAnalysis getvalues(FirstAnalysis fa)
    {
        fa.subjective_fa.RemoveAll(x => x.soap_mapping_id == Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6"));
        if (chkScreen1.Checked)
        {
            SubjectiveFA tempscreening = new SubjectiveFA();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "Temperature";
            tempscreening.remarks = chkScreen1.Text.ToString();
            fa.subjective_fa.Add(tempscreening);
        }
        if (chkScreen2.Checked)
        {
            SubjectiveFA tempscreening = new SubjectiveFA();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "Batuk darah";
            tempscreening.remarks = chkScreen2.Text.ToString();
            fa.subjective_fa.Add(tempscreening);
        }
        if (chkScreen3.Checked)
        {
            SubjectiveFA tempscreening = new SubjectiveFA();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "Berkeringat";
            tempscreening.remarks = chkScreen3.Text.ToString();
            fa.subjective_fa.Add(tempscreening);
        }
        if (chkScreen4.Checked)
        {
            SubjectiveFA tempscreening = new SubjectiveFA();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "Diare, mual & muntah";
            tempscreening.remarks = chkScreen4.Text.ToString();
            fa.subjective_fa.Add(tempscreening);
        }
        if (chkScreen5.Checked)
        {
            SubjectiveFA tempscreening = new SubjectiveFA();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "batuk";
            tempscreening.remarks = chkScreen5.Text.ToString();
            fa.subjective_fa.Add(tempscreening);
        }
        if (chkScreen6.Checked)
        {
            SubjectiveFA tempscreening = new SubjectiveFA();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "swollen";
            tempscreening.remarks = chkScreen6.Text.ToString();
            fa.subjective_fa.Add(tempscreening);
        }
        if (chkScreen7.Checked)
        {
            SubjectiveFA tempscreening = new SubjectiveFA();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "kemerahan";
            tempscreening.remarks = chkScreen7.Text.ToString();
            fa.subjective_fa.Add(tempscreening);
        }
        if (chkScreen8.Checked)
        {
            SubjectiveFA tempscreening = new SubjectiveFA();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "luka";
            tempscreening.remarks = chkScreen8.Text.ToString();
            fa.subjective_fa.Add(tempscreening);
        }

        foreach (SubjectiveFA subjective in fa.subjective_fa)
        {
            if (subjective.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade"))
            {//endemic
                if (rbkunjungan2.Checked)
                    subjective.remarks = txtEndemic.Text;
                else if (rbkunjungan2.Checked)
                    subjective.remarks = "";
            }
            if (subjective.soap_mapping_id == Guid.Parse("a41d51b7-9999-4045-b992-241f1fe679ca"))
            {//current medication
                if (rbPengobatan2.Checked)
                    subjective.remarks = txtPengobatan.Text;
                else if (rbPengobatan1.Checked)
                    subjective.remarks = "";
            }
            if (subjective.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea"))
            {//nutrition
                if (rbnutrisi2.Checked)
                    subjective.remarks = txtNutrition.Text;
                else if (rbnutrisi1.Checked)
                    subjective.remarks = "";
            }
            if (subjective.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48"))
            {//fasting
                if (rbpuasa2.Checked)
                    subjective.remarks = txtFasting.Text;
                else if (rbpuasa1.Checked)
                    subjective.remarks = "";
            }
        }

        if (rbOperas2.Checked)
        {
            fa.patient_surgery_fa = GetRowList_PatientSurgery();
        }
        else if (rbOperasi.Checked)
        {
            fa.patient_surgery_fa.Clear();
            Session[Helper.SessionSurgerySubjective] = null;
        }

        if (rbdrug2.Checked)
        {
            fa.patient_allergy_fa = GetRowList_PatientAllergy(1);
            if (rbfood2.Checked)
            {
                fa.patient_allergy_fa.AddRange(GetRowList_PatientAllergy(2));
            }
        }
        else
        {
            if (rbfood2.Checked)
            {
                fa.patient_allergy_fa = GetRowList_PatientAllergy(2);
            }
            else
            {
                Session[Helper.SessionAllergySubjective] = null;
                fa.patient_allergy_fa.Clear();
            }
        }

        List<PatientDiseaseHistoryFA> templistdisease = new List<PatientDiseaseHistoryFA>();
        if (rbpribadi2.Checked)
        {
            if (chkdisease1.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Hypertension";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease2.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Stroke";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease3.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "TBC";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease4.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Kidney";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease5.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Convulsive";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease6.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Heart";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease7.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Diabetes";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease8.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Asthma";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease9.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Hepatitis";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }

            PatientDiseaseHistoryFA temp_patienthistoryremark = new PatientDiseaseHistoryFA();
            temp_patienthistoryremark.patient_disease_history_id = Guid.Empty;
            temp_patienthistoryremark.value = "Lain-lain";
            temp_patienthistoryremark.remarks = txtDisease.Text;
            temp_patienthistoryremark.disease_history_type = 1;
            temp_patienthistoryremark.is_delete = 0;
            templistdisease.Add(temp_patienthistoryremark);

        }
        else if (rbpribadi1.Checked)
        {
            fa.patient_disease_fa.Clear();
        }

        if (rbkeluarga2.Checked)
        {
            if (chkdiseasefam1.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Heart";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 2;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdiseasefam2.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Diabetes";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 2;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdiseasefam3.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Asthma";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 2;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdiseasefam4.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Hypertension";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 2;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdiseasefam5.Checked)
            {
                PatientDiseaseHistoryFA temp_patienthistory = new PatientDiseaseHistoryFA();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Cancer";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 2;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            PatientDiseaseHistoryFA temp_patienthistoryremark = new PatientDiseaseHistoryFA();
            temp_patienthistoryremark.patient_disease_history_id = Guid.Empty;
            temp_patienthistoryremark.value = "Lain-lain";
            temp_patienthistoryremark.remarks = txtDiseaseFam.Text;
            temp_patienthistoryremark.disease_history_type = 2;
            temp_patienthistoryremark.is_delete = 0;
            templistdisease.Add(temp_patienthistoryremark);
        }
        fa.patient_disease_fa = templistdisease;

        return fa;
    }
}