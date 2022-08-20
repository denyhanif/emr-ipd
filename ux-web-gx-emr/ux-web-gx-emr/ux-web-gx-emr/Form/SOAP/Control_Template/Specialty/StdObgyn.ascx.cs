using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SPObgyn;

public partial class Form_SOAP_Control_Template_Specialty_StdObgyn : System.Web.UI.UserControl
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
    }

    public void CheckVisibleDiv()
    {
        string templateid = Request.QueryString["PageSoapId"];
        if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
        {
            if (rbhaidtidakteratur.Checked)
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowHaid", "ShowDvHaid();", true);
            else
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideHaid", "HideDvHaid();", true);

            if (rbkontrasepsiyes.Checked)
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowKontrasepsi", "ShowDvKontrasepsi();", true);
            else
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideKontrasepsi", "HideDvKontrasepsi();", true);
        }
    }

    public void initializevalue(List<PregnancyData> listpregnantdata, List<PregnancyHistory> listpregnanthistory, List<Objective> listobjective)
    {
        List<PregnancyContraception> listkontrasepidata = new List<PregnancyContraception>();

        if (listpregnantdata.Count > 0)
        {

            foreach (PregnancyData x in listpregnantdata)
            {
                if (x.pregnancy_data_type.ToUpper() == "MENARCHE")
                {
                    txtMenarche.Text = x.value;
                }

                if (x.pregnancy_data_type.ToUpper() == "CONTRACEPTION")
                {
                    PregnancyContraception data = new PregnancyContraception();
                    data.pregnancy_data_id = x.pregnancy_data_id;
                    data.pregnancy_data_type = x.pregnancy_data_type;
                    data.value = x.value;
                    data.remarks = x.remarks;
                    data.status = x.status;
                    listkontrasepidata.Add(data);
                }
            }
        }

        if (listkontrasepidata.Count > 0)
        {
            DataTable dtkontrasepsi = Helper.ToDataTable(listkontrasepidata);
            if (dtkontrasepsi.Rows[0]["value"].ToString() == "Tidak Ada Kontrasepsi")
            {
                gvw_kontrasepsi.DataSource = null;
                gvw_kontrasepsi.DataBind();
                //set checkbox
                rbkontrasepsino.Checked = true;
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideKontrasepsi", "HideDvKontrasepsi();", true);
            }
            else
            {
                gvw_kontrasepsi.DataSource = dtkontrasepsi;
                gvw_kontrasepsi.DataBind();
                //set checkbox
                rbkontrasepsiyes.Checked = true;
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowKontrasepsi", "ShowDvKontrasepsi();", true);
            }
            Session[Helper.SessionObgynKontrasepsi] = dtkontrasepsi;
        }
        else
        {
            Session[Helper.SessionObgynKontrasepsi] = null;
            gvw_kontrasepsi.DataSource = null;
            gvw_kontrasepsi.DataBind();
            //set checkbox
            //rbkontrasepsino.Checked = true;
            ScriptManager.RegisterStartupScript(Page, GetType(), "HideKontrasepsi", "HideDvKontrasepsi();", true);
        }

        if (listpregnanthistory.Count > 0)
        {
            DataTable dtpregnanthistory = Helper.ToDataTable(listpregnanthistory);
            Session[Helper.SessionObgynPregnantHistory] = dtpregnanthistory;

            gvw_riwayathamil.DataSource = dtpregnanthistory;
            gvw_riwayathamil.DataBind();
        }
        else
        {
            Session[Helper.SessionObgynPregnantHistory] = null;

            gvw_riwayathamil.DataSource = null;
            gvw_riwayathamil.DataBind();
        }

        if (listobjective.Count > 0)
        {
            foreach (Objective x in listobjective)
            {
                if (x.soap_mapping_name.ToUpper() == "HAID")
                {
                    if (x.value == "Teratur")
                    {
                        rbhaidteratur.Checked = true;
                    }
                    else if (x.value == "Tidak Teratur")
                    {
                        rbhaidtidakteratur.Checked = true;
                        txtHaidTakTeratur.Text = x.remarks;
                    }
                }

                if (x.soap_mapping_name.ToUpper() == "HAID PROBLEM")
                {
                    txtKeluhanHaid.Text = x.value;
                }
            }
        }
        CheckVisibleDiv();
    }

    public SOAPObgyn GetObgynValues(SOAPObgyn faobgyn)
    {
        faobgyn.pregnancy_data.RemoveAll(x => x.pregnancy_data_type == "CONTRACEPTION");
        //for (int i = 0; i < faobgyn.pregnancy_data.Count; i++)
        //{
        //    if (faobgyn.pregnancy_data[i].pregnancy_data_type == "CONTRACEPTION")
        //    {
        //        faobgyn.pregnancy_data.RemoveAt(i);
        //        i--;
        //    }
        //}

        if (txtMenarche.Text != "")
        {
            string flagempty = "kosong";
            foreach (PregnancyData x in faobgyn.pregnancy_data)
            {
                if (x.pregnancy_data_type.ToUpper() == "MENARCHE")
                {
                    x.value = txtMenarche.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PregnancyData data = new PregnancyData();
                data.pregnancy_data_id = Guid.Empty;
                data.pregnancy_data_type = "MENARCHE";
                data.value = txtMenarche.Text;
                data.remarks = "";
                data.status = "";
                faobgyn.pregnancy_data.Add(data);
            }
        }

        if (rbkontrasepsiyes.Checked == true)
        {
            DataTable dtkontrasepsi = (DataTable)Session[Helper.SessionObgynKontrasepsi];
            if (dtkontrasepsi.Rows.Count > 0)
            {
                for (int j = 0; j < dtkontrasepsi.Rows.Count; j++)
                {
                    PregnancyData data = new PregnancyData();
                    data.pregnancy_data_id = Guid.Parse(dtkontrasepsi.Rows[j]["pregnancy_data_id"].ToString());
                    data.pregnancy_data_type = "CONTRACEPTION";
                    data.value = dtkontrasepsi.Rows[j]["value"].ToString();
                    data.remarks = dtkontrasepsi.Rows[j]["remarks"].ToString();
                    data.status = dtkontrasepsi.Rows[j]["status"].ToString();
                    faobgyn.pregnancy_data.Add(data);
                }
            }
        }
        else if (rbkontrasepsino.Checked == true)
        {
            PregnancyData data = new PregnancyData();
            data.pregnancy_data_id = Guid.Empty;
            data.pregnancy_data_type = "CONTRACEPTION";
            data.value = "Tidak Ada Kontrasepsi";
            data.remarks = "";
            data.status = "";
            faobgyn.pregnancy_data.Add(data);
        }

        List<PregnancyHistory> listhamil = GetRowListPregnantHistory();
        if (listhamil.Count > 0)
        {
            faobgyn.pregnancy_history = listhamil;
        }
        else
        {
            faobgyn.pregnancy_history = new List<PregnancyHistory>();
        }

        foreach (Objective x in faobgyn.objective)
        {
            if (x.soap_mapping_name.ToUpper() == "HAID")
            {
                if (rbhaidteratur.Checked == true)
                {
                    x.value = "Teratur";
                    x.remarks = "";
                }
                else if (rbhaidtidakteratur.Checked == true)
                {
                    x.value = "Tidak Teratur";
                    x.remarks = txtHaidTakTeratur.Text;
                }
            }

            if (x.soap_mapping_name.ToUpper() == "HAID PROBLEM")
            {
                x.value = txtKeluhanHaid.Text;
            }
        }

        return faobgyn;
    }

    protected void ButtonAddKontrasepsi_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            DataTable dt = new DataTable();
            if (Session[Helper.SessionObgynKontrasepsi] == null)
            {
                dt.Columns.Add("pregnancy_data_id");
                dt.Columns.Add("pregnancy_data_type");
                dt.Columns.Add("value");
                dt.Columns.Add("remarks");
                dt.Columns.Add("status");
                dt.Rows.Add(new Object[] { Guid.Empty, "CONTRACEPTION", txtJenisKontrasepsi.Text, txtSejakKontrasepsi.Text, txtHinggaKontrasepsi.Text });
            }
            else
            {
                dt = (DataTable)Session[Helper.SessionObgynKontrasepsi];
                DataRow dr;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["value"].ToString() == "Tidak Ada Kontrasepsi")
                    {
                        dr = dt.Rows[i];
                        dt.Rows.Remove(dr);
                    }
                }
                dt.Rows.Add(new Object[] { Guid.Empty, "CONTRACEPTION", txtJenisKontrasepsi.Text, txtSejakKontrasepsi.Text, txtHinggaKontrasepsi.Text });
            }

            Session[Helper.SessionObgynKontrasepsi] = dt;
            gvw_kontrasepsi.DataSource = dt;
            gvw_kontrasepsi.DataBind();
            CheckVisibleDiv();

            txtJenisKontrasepsi.Text = "";
            txtSejakKontrasepsi.Text = "";
            txtHinggaKontrasepsi.Text = "";

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null? Request.QueryString["EncounterId"].ToString():"", "ButtonAddKontrasepsi_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null? Request.QueryString["EncounterId"].ToString():"", "ButtonAddKontrasepsi_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            throw ex;
        }

        //Log.Info(LogConfig.LogEnd());

    }

    protected void ImageButtonDeleteKontrasepsi_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {

            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
            HiddenField kontrasepsi_id = (HiddenField)gvw_kontrasepsi.Rows[selRowIndex].FindControl("kontrasepsi_id");
            DataTable dt = (DataTable)Session[Helper.SessionObgynKontrasepsi];
            dt.Rows[selRowIndex].Delete();

            if (dt.Rows.Count > 0)
            {
                Session[Helper.SessionObgynKontrasepsi] = dt;
                gvw_kontrasepsi.DataSource = dt;
                gvw_kontrasepsi.DataBind();
            }
            else
            {
                Session[Helper.SessionObgynKontrasepsi] = null;
                gvw_kontrasepsi.DataSource = null;
                gvw_kontrasepsi.DataBind();
            }
            CheckVisibleDiv();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null? Request.QueryString["EncounterId"].ToString():"", "ImageButtonDeleteKontrasepsi_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null? Request.QueryString["EncounterId"].ToString():"", "ImageButtonDeleteKontrasepsi_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            throw ex;
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void gvw_riwayathamil_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField idItem = (HiddenField)e.Row.FindControl("HF_riwayathamilid");

            DataTable dt = ((DataTable)Session[Helper.SessionObgynPregnantHistory]).Select("pregnancy_history_id = '" + idItem.Value + "'").CopyToDataTable();
            DropDownList ddl_umur = (DropDownList)e.Row.FindControl("DDLUmur");
            ddl_umur.SelectedValue = dt.Rows[0]["age_type"].ToString();
            DropDownList ddl_jk = (DropDownList)e.Row.FindControl("DDLjk");
            ddl_jk.SelectedValue = dt.Rows[0]["child_sex"].ToString();
            DropDownList ddl_hidupmati = (DropDownList)e.Row.FindControl("DDLhidupmati");
            ddl_hidupmati.SelectedValue = dt.Rows[0]["labor_doa"].ToString();
        }
    }

    protected void ButtonAddKehamilan_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        DataTable dataku = new DataTable();

        if (Session[Helper.SessionObgynPregnantHistory] == null)
        {
            dataku.Columns.Add("pregnancy_history_id");
            dataku.Columns.Add("pregnancy_sequence");
            dataku.Columns.Add("child_age");
            dataku.Columns.Add("age_type");
            dataku.Columns.Add("child_sex");
            dataku.Columns.Add("bbl");
            dataku.Columns.Add("labor_type");
            dataku.Columns.Add("labor_helper");
            dataku.Columns.Add("labor_place");
            dataku.Columns.Add("labor_doa");
            dataku.Columns.Add("data_sequence");

            Session[Helper.SessionObgynPregnantHistory] = dataku;
        }

        //dataku = (DataTable)Session[Helper.SessionObgynPregnantHistory];
        dataku = Helper.ToDataTable(GetRowListPregnantHistory());

        DataRow temp_add = dataku.NewRow();
        temp_add["pregnancy_history_id"] = Guid.NewGuid();
        temp_add["pregnancy_sequence"] = 0;
        temp_add["child_age"] = 0;
        temp_add["age_type"] = "";
        temp_add["child_sex"] = 0;
        temp_add["bbl"] = 0;
        temp_add["labor_type"] = "";
        temp_add["labor_helper"] = "";
        temp_add["labor_place"] = "";
        temp_add["labor_doa"] = 0;
        temp_add["data_sequence"] = 0;

        dataku.Rows.Add(temp_add);

        if (dataku.Rows.Count > 0)
        {
            for (int i = 0; i < dataku.Rows.Count; i++)
            {
                dataku.Rows[i]["data_sequence"] = i + 1;
            }
        }

        Session[Helper.SessionObgynPregnantHistory] = dataku;

        gvw_riwayathamil.DataSource = dataku;
        gvw_riwayathamil.DataBind();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null? Request.QueryString["EncounterId"].ToString():"", "GetRowListPregnantHistory", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ImageButtonDeleteKehamilan_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {

            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
            HiddenField riwayathamilid = (HiddenField)gvw_riwayathamil.Rows[selRowIndex].FindControl("HF_riwayathamilid");
            DataTable dt = (DataTable)Session[Helper.SessionObgynPregnantHistory];
            dt.Rows[selRowIndex].Delete();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["data_sequence"] = i + 1;
                }

                Session[Helper.SessionObgynPregnantHistory] = dt;
                gvw_riwayathamil.DataSource = dt;
                gvw_riwayathamil.DataBind();
            }
            else
            {
                Session[Helper.SessionObgynPregnantHistory] = null;
                gvw_riwayathamil.DataSource = null;
                gvw_riwayathamil.DataBind();
            }
            CheckVisibleDiv();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null? Request.QueryString["EncounterId"].ToString():"", "ImageButtonDeleteKehamilan_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null? Request.QueryString["EncounterId"].ToString():"", "ImageButtonDeleteKehamilan_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            throw ex;
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected List<PregnancyHistory> GetRowListPregnantHistory()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<PregnancyHistory> data = new List<PregnancyHistory>();
        try
        {

            int countadd = 1;
            foreach (GridViewRow rows in gvw_riwayathamil.Rows)
            {
                HiddenField HF_riwayathamilid = (HiddenField)rows.FindControl("HF_riwayathamilid");
                TextBox TextBoxHamilKe = (TextBox)rows.FindControl("TextBoxHamilKe");
                TextBox TextBoxUmur = (TextBox)rows.FindControl("TextBoxUmur");
                TextBox TextBoxBBL = (TextBox)rows.FindControl("TextBoxBBL");
                TextBox TextBoxPersalinan = (TextBox)rows.FindControl("TextBoxPersalinan");
                TextBox TextBoxDitolong = (TextBox)rows.FindControl("TextBoxDitolong");
                TextBox TextBoxTempatLahir = (TextBox)rows.FindControl("TextBoxTempatLahir");
                DropDownList DDLUmur = (DropDownList)rows.FindControl("DDLUmur");
                DropDownList DDLjk = (DropDownList)rows.FindControl("DDLjk");
                DropDownList DDLhidupmati = (DropDownList)rows.FindControl("DDLhidupmati");

                PregnancyHistory row = new PregnancyHistory();
                countadd = countadd + 1;
                short hamilke = 0;
                if (TextBoxHamilKe.Text != "")
                {
                    hamilke = short.Parse(TextBoxHamilKe.Text);
                }
                //short umur = 0;
                //if (TextBoxUmur.Text != "")
                //{
                //    umur = short.Parse(TextBoxUmur.Text);
                //}
                //short bbl = 0;
                //if (TextBoxBBL.Text != "")
                //{
                //    bbl = short.Parse(TextBoxBBL.Text);
                //}
                row.pregnancy_history_id = Guid.Parse(HF_riwayathamilid.Value);
                row.pregnancy_sequence = hamilke;
                row.child_age = TextBoxUmur.Text; //umur;
                row.age_type = DDLUmur.SelectedValue.ToString();
                row.child_sex = short.Parse(DDLjk.SelectedValue.ToString());
                row.BBL = TextBoxBBL.Text; //bbl;
                row.labor_type = TextBoxPersalinan.Text;
                row.labor_helper = TextBoxDitolong.Text;
                row.labor_place = TextBoxTempatLahir.Text;
                row.labor_doa = short.Parse(DDLhidupmati.SelectedValue.ToString());
                row.data_sequence = (short)countadd;

                data.Add(row);
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null? Request.QueryString["EncounterId"].ToString():"", "ButtonAddKontrasepsi_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null? Request.QueryString["EncounterId"].ToString():"", "ButtonAddKontrasepsi_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());

        return data;
    }

    public delegate bool customHandler(object sender);
    public event customHandler submitObgynSender;
    protected void btnsubmitobgyn_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        submitObgynSender(sender);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalEditObgyn", "$('#modalEditObgyn').modal('hide');", true);

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null? Request.QueryString["EncounterId"].ToString():"", "btnsubmitobgyn_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());
    }


    public void UpdateModalObgyn()
    {
        UpdatePanelModalObgyn.Update();
    }
}