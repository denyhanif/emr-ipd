
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using static FirstAssesment;
using static SPDocument;
using static FirstAssesment;
using static PatientReferralModel;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Web.Services;
using static PatientHistory;

public partial class Form_SOAP_Control_Template_Modal_ModalReferal : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfPatientId.Value = Request.QueryString["idPatient"];
            hfEncounterId.Value = Request.QueryString["EncounterId"];
            //var varResult = clsCommon.GetPatientHeader(long.Parse(hfPatientId.Value), hfEncounterId.Value.ToString());
            //var JsongetPatientHistory = JsonConvert.DeserializeObject<ResponsePatientHeader>(varResult.Result.ToString());
            //PatientHeader header = JsongetPatientHistory.Data;
            //PatientCard.initializevalue(header);

            //form_rujukan_external.Visible = false;
            
        }
    }

    //protected void chkRBExternal_CheckedChanged(Object sender, EventArgs e)
    //{
    //    ((RadioButton)sender).Checked = true;
    //    foreach (RepeaterItem item in rptReferral.Items)
    //    {
    //        RadioButton RB_DokterExternal = (RadioButton)item.FindControl("RB_DokterExternal");

    //        if (RB_DokterExternal.Checked)
    //        {
    //            RadioButton RB_Internal = (RadioButton)item.FindControl("RB_Internal");
    //            RadioButton RB_Siloam = (RadioButton)item.FindControl("RB_Siloam");
    //            RB_Internal.Checked = false;
    //            RB_Siloam.Checked = false;


    //            Control form_rujukan_external = item.FindControl("form_rujukan_external");
    //            Control divDokterRujukan = item.FindControl("divDokterRujukan");
    //            Control divadddokter = item.FindControl("divadddokter");
    //            Control divjenisrujukan = item.FindControl("divjenisrujukan");
    //            Control btnDeleterujukan = item.FindControl("btnDeleterujukan");

    //            form_rujukan_external.Visible = true;
    //            divDokterRujukan.Visible = false;
    //            divadddokter.Visible = false;
    //            divBtnAddDokter.Visible = false;
    //            divjenisrujukan.Visible = false;
    //            btnDeleterujukan.Visible = false;
    //            //divjenisrujuk.Visible = false;

    //        }

    //    }
    //    UpdatePanelformrujukan.Update();
    //}

    //protected void chkRBSIloam_CheckedChanged(Object sender, EventArgs e)
    //{
    //    ((RadioButton)sender).Checked = true;
    //    foreach (RepeaterItem item in rptReferral.Items)
    //    {
    //        RadioButton RB_Siloam = (RadioButton)item.FindControl("RB_Siloam");
    //        var ii = RB_Siloam.Text;


    //        if (RB_Siloam.Checked)
    //        {
    //            RadioButton RB_Internal = (RadioButton)item.FindControl("RB_Internal");
    //            RadioButton RB_DokterExternal = (RadioButton)item.FindControl("RB_DokterExternal");

    //            RB_Internal.Checked = false;
    //            RB_DokterExternal.Checked = false;


    //            Control form_rujukan_external = item.FindControl("form_rujukan_external");
    //            Control divDokterRujukan = item.FindControl("divDokterRujukan");
    //            Control divadddokter = item.FindControl("divadddokter");
    //            Control divjenisrujukan = item.FindControl("divjenisrujukan");
    //            Control btnDeleterujukan = item.FindControl("btnDeleterujukan");



    //            form_rujukan_external.Visible = false;
    //            divDokterRujukan.Visible = true;
    //            divadddokter.Visible = true;
    //            divBtnAddDokter.Visible = true;
    //            divjenisrujukan.Visible = true;
    //            btnDeleterujukan.Visible = true;

    //        }

    //    }
    //    UpdatePanelformrujukan.Update();
    //}

    //protected void chkRBInternal_CheckedChanged(Object sender, EventArgs e)
    //{
    //    ((RadioButton)sender).Checked = true;
    //    foreach (RepeaterItem item in rptReferral.Items)
    //    {
    //        RadioButton RB_Internal = (RadioButton)item.FindControl("RB_Internal");

    //        if (RB_Internal.Checked)
    //        {

    //            RadioButton RB_DokterExternal = (RadioButton)item.FindControl("RB_DokterExternal");
    //            RadioButton RB_Siloam = (RadioButton)item.FindControl("RB_Siloam");

    //            RB_DokterExternal.Checked = false;
    //            RB_Siloam.Checked = false;


    //            Control form_rujukan_external = item.FindControl("form_rujukan_external");
    //            Control divDokterRujukan = item.FindControl("divDokterRujukan");
    //            Control divadddokter = item.FindControl("divadddokter");
    //            Control divjenisrujukan = item.FindControl("divjenisrujukan");
    //            Control btnDeleterujukan = item.FindControl("btnDeleterujukan");
    //            //Control divBtnAddDokter = item.FindControl("divBtnAddDokter");



    //            form_rujukan_external.Visible = false;
    //            divDokterRujukan.Visible = true;
    //            divadddokter.Visible = true;
    //            divBtnAddDokter.Visible = true;
    //            BtnAddDokter.Visible = true;
    //            divjenisrujukan.Visible = true;
    //            btnDeleterujukan.Visible = true;


    //        }

    //    }
    //    UpdatePanelformrujukan.Update();
    //}

    protected void RB_Konsul1x_CheckedChanged(Object sender, EventArgs e)
    {
        ((RadioButton)sender).Checked = true;
        foreach (RepeaterItem item in rptReferral.Items)
        {
            RadioButton RB_Konsul1x = (RadioButton)item.FindControl("RB_Konsul1x");

            if (RB_Konsul1x.Checked)
            {

                RadioButton RB_AlihRawat = (RadioButton)item.FindControl("RB_AlihRawat");
                RadioButton RB_RawatBersama = (RadioButton)item.FindControl("RB_RawatBersama");

                RB_AlihRawat.Checked = false;
                RB_RawatBersama.Checked = false;
            }

        }
        UpdatePanelformrujukan.Update();
    }

    protected void RB_AlihRawat_CheckedChanged(Object sender, EventArgs e)
    {
        ((RadioButton)sender).Checked = true;
        foreach (RepeaterItem item in rptReferral.Items)
        {
            RadioButton RB_AlihRawat = (RadioButton)item.FindControl("RB_AlihRawat");

            if (RB_AlihRawat.Checked)
            {

                RadioButton RB_Konsul1x = (RadioButton)item.FindControl("RB_Konsul1x");
                RadioButton RB_RawatBersama = (RadioButton)item.FindControl("RB_RawatBersama");

                RB_Konsul1x.Checked = false;
                RB_RawatBersama.Checked = false;
            }

        }
        UpdatePanelformrujukan.Update();
    }

    protected void RB_RawatBersama_CheckedChanged(Object sender, EventArgs e)
    {
        ((RadioButton)sender).Checked = true;
        foreach (RepeaterItem item in rptReferral.Items)
        {
            RadioButton RB_RawatBersama = (RadioButton)item.FindControl("RB_RawatBersama");

            if (RB_RawatBersama.Checked)
            {

                RadioButton RB_Konsul1x = (RadioButton)item.FindControl("RB_Konsul1x");
                RadioButton RB_AlihRawat = (RadioButton)item.FindControl("RB_AlihRawat");

                RB_Konsul1x.Checked = false;
                RB_AlihRawat.Checked = false;
            }

        }
        UpdatePanelformrujukan.Update();
    }

    protected void BtnAddDokter_Click(object sender, EventArgs e)
    {

        //TextBox txtreferal = FindControl.



        List<ReferalData> referalDatas = getvalues();
        ReferalData newdata = new ReferalData();
        newdata.referral_id = Guid.NewGuid();
        newdata.referral_type = "Konsultasi 1 Kali";
        newdata.referral_doctor_id = 0;
        newdata.referral_doctor_name = "";
        newdata.speciality_id = 0;
        newdata.referral_remark = "";
        newdata.IsProcess = false;
        newdata.is_new = 1;
        newdata.referal_status = "New";
        newdata.is_delete = 0;
        newdata.referral_target = "Internal";
        newdata.external_referral_to = "";
        newdata.external_referral_place = "";
        newdata.external_referral_date = "";
        newdata.external_referral_time = "00:00:00.000";
        newdata.external_referral_reason = "";
        newdata.is_editable = 1;
        newdata.created_date = DateTime.Now.ToString();

        DataTable dtreferals = new DataTable();
        dtreferals = Helper.ToDataTable(referalDatas);

        if (dtreferals.Select("speciality_id = 0").Count() > 0)
        {
            ShowToastr("Pilih Spesialis terlebih dahulu !", "Save Form Alert", "Warning");

            rptReferral.DataSource = Helper.ToDataTable(referalDatas);
            rptReferral.DataBind();

            initUI(referalDatas);
            UpdatePanelformrujukan.Update();
        }
        else
        {
            referalDatas.Add(newdata);
            rptReferral.DataSource = Helper.ToDataTable(referalDatas);
            rptReferral.DataBind();

            initUI(referalDatas);
            UpdatePanelformrujukan.Update();
        }

       

        
        
       

        
    }


    protected void BtnDeleteReferral_Click(object sender, EventArgs e)
    {
        try
        {
            int selRowIndex = ((RepeaterItem)(((LinkButton)sender).Parent.Parent)).ItemIndex;
            List<ReferalData> referalDatas = getvalues();
            DataTable dt = Helper.ToDataTable(referalDatas);// dt diconvert ke model?
            dt.Rows[selRowIndex].SetField("is_delete", 1);
            if (dt.Select("is_delete = 0").Count() > 0)
            {
                //Session[Helper.SessionConsumablesList + hfguidadditional.Value] = dt.Select("is_delete = 0").CopyToDataTable();
                rptReferral.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
                rptReferral.DataBind();

                initUI(referalDatas);
            }
            else
            {
                rptReferral.DataSource = null;
                rptReferral.DataBind();
            }
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }

    }

    protected void Ddl_DokterSpesialis_SelectedIndexChanged(object sender, EventArgs e)
    {

        //HiddenField hf_referral_id = (HiddenField)FindControl("referral_id");

        try
        {
            DropDownList sel = (DropDownList)sender;

            List<DoctorOrg> datadoctororgobj = (List<DoctorOrg>)Session[Helper.SessionDoctorByOrg];
            List<Specialist> spesilisobj = (List<Specialist>)Session[Helper.SessionSpeciality];
            var selecspesialis = spesilisobj.Where(x => x.specialization_hope_id == Convert.ToInt64(sel.SelectedValue)).FirstOrDefault();


            List<DoctorOrg> doctorOrgsnew = datadoctororgobj.Where(x => x.speciality_id == selecspesialis.speciality_id && x.consultation_type_id == "1").ToList();

            RepeaterItem ri = sel.NamingContainer as RepeaterItem;
            if (ri != null)
            {
                DropDownList Ddl_DokterRujukan = ri.FindControl("Ddl_DokterRujukan") as DropDownList;
                DropDownList Ddl_DokterSpesialis = ri.FindControl("Ddl_DokterSpesialis") as DropDownList;

                if (Ddl_DokterRujukan != null)
                {
                    Ddl_DokterRujukan.Enabled = true;
                    Ddl_DokterRujukan.Items.Clear();
                    Ddl_DokterRujukan.Items.Add(new ListItem("Any Doctor", "0"));
                    foreach (DoctorOrg docorg in doctorOrgsnew)
                    {
                        Ddl_DokterRujukan.Items.Add(new ListItem(docorg.name, docorg.doctor_hope_id.ToString()));
                    }

                    UpdatePanelformrujukan.Update();
                }
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }

    }

    protected void Ddl_Dokter_SelectIndexChange(object sender, EventArgs e)
    {
        try
        {
            List<ReferalData> referalDatas = getvalues();
            DropDownList sel = (DropDownList)sender;
            List<DoctorOrg> datadoctororgobj = (List<DoctorOrg>)Session[Helper.SessionDoctorByOrg];
            RepeaterItem ri = sel.NamingContainer as RepeaterItem;
            if (ri != null)
            {

                if (referalDatas.Where(x => x.referral_doctor_id == Convert.ToInt64(sel.SelectedValue)).ToList().Count > 1)
                {
                    DropDownList sell = (DropDownList)sender;
                    sell.SelectedValue = "0";// add toast
                    ShowToastr("Dokter tidak boleh sama", "Dokter rujukan tidak boleh sama", "Warning");
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert('Dokter tidak boleh sama');", true);
                }

            }


        }
        catch (Exception ex)
        {
            string message = ex.Message;
        }
        //ShowToastr("Dokter tidak boleh sama", "", "Danger");
    }

    void ShowToastr(string message, string title, string type)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastr_message",
             String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
    }
    public void initUI(List<ReferalData> referalDatas)
    {

        List<Specialist> dataspecialistobj = (List<Specialist>)Session[Helper.SessionSpeciality];
        List<DoctorOrg> datadoctororgobj = (List<DoctorOrg>)Session[Helper.SessionDoctorByOrg];

        if (dataspecialistobj == null)
        {
            var dataSpecialist = clsSOAP.getSpeciality();
            var SpecialistJson = JsonConvert.DeserializeObject<ResultSpecialist>(dataSpecialist.Result.ToString());
            dataspecialistobj = SpecialistJson.data;
            Session[Helper.SessionSpeciality] = dataspecialistobj;
        }

        if (datadoctororgobj == null)
        {
            DataTable dt = (DataTable)Session[Helper.SessionListOrganization];
            var dataDoctorOrg = clsSOAP.getDoctorByOrg(dt.Rows[0]["mobile_organization_id"].ToString());
            var DoctorOrgJson = JsonConvert.DeserializeObject<ResultDoctorOrg>(dataDoctorOrg.Result.ToString());
            datadoctororgobj = DoctorOrgJson.data;
            Session[Helper.SessionDoctorByOrg] = datadoctororgobj;
        }

        bool delVisible = rptReferral.Items.Count > 1;


        foreach (RepeaterItem item in rptReferral.Items)
        {

            LinkButton BtnDeleteReferral = (LinkButton)item.FindControl("BtnDeleteReferral");
            BtnDeleteReferral.Visible = delVisible;

            //RadioButton RB_Internal = (RadioButton)item.FindControl("RB_Internal");
            //RadioButton RB_DokterExternal = (RadioButton)item.FindControl("RB_DokterExternal");
            //RadioButton RB_Siloam = (RadioButton)item.FindControl("RB_Siloam");

            HiddenField refferal_id = (HiddenField)item.FindControl("referral_id");

            Control form_rujukan_external = item.FindControl("form_rujukan_external");
            Control divDokterRujukan = item.FindControl("divDokterRujukan");
            Control divadddokter = item.FindControl("divadddokter");
            Control divjenisrujukan = item.FindControl("divjenisrujukan");
            Control btnDeleterujukan = item.FindControl("btnDeleterujukan");
            form_rujukan_external.Visible = false;
            divDokterRujukan.Visible = true;
            divadddokter.Visible = true;
            divjenisrujukan.Visible = true;
            btnDeleterujukan.Visible = true;
          

            DropDownList Ddl_DokterSpesialis = (DropDownList)item.FindControl("Ddl_DokterSpesialis");
            Ddl_DokterSpesialis.Items.Add(new ListItem("Choose Spesialis", "0"));
            
            foreach (Specialist specialist in dataspecialistobj.OrderBy(x => x.speciality_name))
            {
                Ddl_DokterSpesialis.Items.Add(new ListItem(specialist.speciality_name, specialist.specialization_hope_id.ToString()));
            }

            //id model pr/ repeater
            ReferalData modelReferal = referalDatas.Where(x => x.referral_id == new Guid(refferal_id.Value)).FirstOrDefault();

            if (modelReferal.speciality_id > 0)
            {

                Specialist selectspesialis = dataspecialistobj.Where(x => x.specialization_hope_id == modelReferal.speciality_id).FirstOrDefault();
                Ddl_DokterSpesialis.SelectedValue = selectspesialis.specialization_hope_id.ToString();

                List<DoctorOrg> filteredDoctor = datadoctororgobj.Where(x => x.speciality_id == selectspesialis.speciality_id && x.consultation_type_id == "1").ToList();

                DropDownList Ddl_DokterRujukan = (DropDownList)item.FindControl("Ddl_DokterRujukan");
                Ddl_DokterRujukan.Items.Clear();
                Ddl_DokterRujukan.Items.Add(new ListItem("Any Doctor", "0"));

                foreach (DoctorOrg doctor in filteredDoctor)
                {
                    Ddl_DokterRujukan.Items.Add(new ListItem(doctor.name, doctor.doctor_hope_id.ToString()));
                }

                if (modelReferal.referral_doctor_id > 1)
                {
                    Ddl_DokterRujukan.SelectedValue = modelReferal.referral_doctor_id.ToString();
                }

                if(modelReferal.is_editable.ToString()  == "0")
                {
                    Ddl_DokterRujukan.Enabled = false;
                }
                else
                {
                    Ddl_DokterRujukan.Enabled = true;
                }

               
            }
            else
            {
                DropDownList Ddl_DokterRujukan = (DropDownList)item.FindControl("Ddl_DokterRujukan");
                Ddl_DokterRujukan.Items.Clear();
                Ddl_DokterRujukan.Items.Add(new ListItem("Any Doctor", "0"));
                
                    Ddl_DokterRujukan.Enabled = false;
                

            }

        }


    }

    public void initializevalue(List<ReferalData> referalDatas)
    {
        if (referalDatas == null)
        {
            referalDatas = new List<ReferalData>();
        }

        if (referalDatas.Count == 0)
        {
            referalDatas = new List<ReferalData>();
            ReferalData newdata = new ReferalData();
            newdata.referral_id = Guid.NewGuid();

            newdata.referral_type = "Konsultasi 1 Kali";
            newdata.referral_doctor_id = 0;
            newdata.referral_doctor_name = "";
            newdata.speciality_id = 0;
            newdata.speciality_name = "";
            newdata.referral_remark = "";
            newdata.IsProcess = false;
            newdata.is_new = 1;
            newdata.referal_status = "New";
            newdata.is_delete = 0;
            newdata.referral_target = "Internal";
            newdata.external_referral_to = "";
            newdata.external_referral_place = "";
            newdata.external_referral_date = DateTime.Now.ToString("dd.MM.yyy");
            newdata.external_referral_time = string.Empty;
            newdata.external_referral_reason = "";
            newdata.is_editable = 1;
            newdata.created_date = DateTime.Now.ToString();
            referalDatas.Add(newdata);
        }

        rptReferral.DataSource = Helper.ToDataTable(referalDatas);
        rptReferral.DataBind();

        initUI(referalDatas);
        UpdatePanelformrujukan.Update();
    }

    public List<ReferalData> getvalues()
    {
        List<ReferalData> referalDatas = new List<ReferalData>();

        foreach (RepeaterItem item in rptReferral.Items)
        {
            TextBox referral_remark = (TextBox)item.FindControl("TBreferral_remark");

            HiddenField referal_status = (HiddenField)item.FindControl("referal_status");
            TextBox referral_remark_to = (TextBox)item.FindControl("TBexternal_referral_to");
            TextBox referral_remark_place = (TextBox)item.FindControl("TBexternal_referral_place");
            TextBox referral_remark_date = (TextBox)item.FindControl("TBexternal_referral_date");
            TextBox referral_remark_time = (TextBox)item.FindControl("TBexternal_referral_time");
            TextBox referral_remark_reason = (TextBox)item.FindControl("TBexternal_referral_reason");
            HiddenField hf_referral_id = (HiddenField)item.FindControl("referral_id");

            HiddenField hf_is_new = (HiddenField)item.FindControl("is_new");
            HiddenField hf_is_editable = (HiddenField)item.FindControl("is_editable");
            HiddenField hf_is_delete = (HiddenField)item.FindControl("is_delete");





            HiddenField hf_referral_dokter_id = (HiddenField)item.FindControl("referral_doctor_id");
            HiddenField hf_speciality_id = (HiddenField)item.FindControl("speciality_id");
            DropDownList doctor_name = (DropDownList)item.FindControl("Ddl_DokterRujukan");
            DropDownList spesialis = (DropDownList)item.FindControl("Ddl_DokterSpesialis");
            DropDownList ddlreason = (DropDownList)item.FindControl("Ddl_DokterSpesialis");


            //RadioButton RB_Internal = (RadioButton)item.FindControl("RB_Internal");
            RadioButton RB_Siloam = (RadioButton)item.FindControl("RB_Siloam");
            RadioButton RB_DokterExternal = (RadioButton)item.FindControl("RB_DokterExternal");

            RadioButton RB_Konsul1x = (RadioButton)item.FindControl("RB_Konsul1x");
            RadioButton RB_AlihRawat = (RadioButton)item.FindControl("RB_AlihRawat");
            RadioButton RB_RawatBersama = (RadioButton)item.FindControl("RB_RawatBersama");

            // ini untuk populate dari UI
            ReferalData newdata = new ReferalData();
            newdata.referral_id = new Guid(hf_referral_id.Value);


            List<Specialist> spesilisobj = (List<Specialist>)Session[Helper.SessionSpeciality];

            var selecspesialis = spesilisobj.Where(x => x.specialization_hope_id == Int64.Parse(spesialis.SelectedValue)).FirstOrDefault();

            List<DoctorOrg> datadoctororgobj = (List<DoctorOrg>)Session[Helper.SessionDoctorByOrg];


            //cek spesialis
            //if (spesialis.SelectedValue.ToString()=="0")
            //{//specialization_hope_id
            //    ShowToastr("Pilih Spesialis terlebih dahulu notif dari get value asxreferl!", "Save Form Alert", "Warning");


            //}


            if (doctor_name.SelectedValue.ToString() == "0")
            {
                newdata.referral_doctor_id_mysiloam = Guid.Empty;
                
                //ShowToastr("Pilih Spesialis terlebih dahulu notif dari get value asxreferl!", "Save Form Alert", "Warning");
            }
            else
            if (doctor_name.SelectedValue.ToString() == MyUser.GetHopeUserID())
            {
                doctor_name.SelectedValue = "0";
                ShowToastr("Tidak boleh referal ke diri sendiri!", "Save Form Alert", "Warning");

            }
            else
            {
                var doctorOrgsnew = datadoctororgobj.Where(x => x.doctor_hope_id == Int64.Parse(doctor_name.SelectedValue)).FirstOrDefault();
                newdata.referral_doctor_id_mysiloam = doctorOrgsnew.doctor_id;
            }

            newdata.speciality_id_mysiloam = selecspesialis.speciality_id;

            if (RB_Konsul1x.Checked)
            {
                newdata.referral_type = "Konsultasi 1 Kali";
            }
            else if (RB_AlihRawat.Checked)
            {
                newdata.referral_type = "Alih Rawat";
            }
            else if (RB_RawatBersama.Checked)
            {
                newdata.referral_type = "Rawat Bersama";
            }

            if (!doctor_name.SelectedValue.ToString().Equals("0") )
            {

                newdata.referral_doctor_name = doctor_name.SelectedItem.Text;
                newdata.referral_doctor_id = Int64.Parse(doctor_name.SelectedValue.ToString());
            }
            else
            {

                newdata.referral_doctor_id = 0;
                newdata.referral_doctor_name = "Any Doctor";
            }

            newdata.referal_status = referal_status.Value;
            newdata.speciality_id = Int64.Parse(spesialis.SelectedValue);
            newdata.speciality_name = spesialis.SelectedItem.Text;
            newdata.referral_remark = referral_remark.Text;
            newdata.IsProcess = false;
            newdata.is_new = Convert.ToInt32(hf_is_new.Value);
            newdata.is_delete = Convert.ToInt32(hf_is_delete.Value);

            //if (RB_Internal.Checked)
            //{

            //}
            //else if (RB_Siloam.Checked)
            //{
            //    newdata.referral_target = "Siloam";
            //}
            //else if (RB_DokterExternal.Checked)
            //{
            //    newdata.referral_target = "External";
            //}
            newdata.referral_target = "Internal";
            newdata.external_referral_to = referral_remark_to.Text;
            newdata.external_referral_place = referral_remark_place.Text;
            newdata.external_referral_date = referral_remark_date.Text;
            newdata.external_referral_time = referral_remark_time.Text;
            newdata.external_referral_reason = referral_remark_reason.Text;
            //newdata.is_editable = 0;
            newdata.is_editable = Convert.ToInt32(hf_is_editable.Value);
            newdata.created_date = DateTime.Now.ToString();

            referalDatas.Add(newdata);
        }

        return referalDatas;
    }


    public void StylingValidation(bool txt)
    {
        if (txt == true)
        {
            foreach (RepeaterItem item in rptReferral.Items)
            {
                TextBox referral_remark = (TextBox)item.FindControl("TBreferral_remark");
                DropDownList doctor_name = (DropDownList)item.FindControl("Ddl_DokterRujukan");
                referral_remark.Style.Add("border", "1px solid red");
            }
        }
        else
        {
            foreach (RepeaterItem item in rptReferral.Items)
            {
                TextBox referral_remark = (TextBox)item.FindControl("TBreferral_remark");
                DropDownList doctor_name = (DropDownList)item.FindControl("Ddl_DokterRujukan");
                referral_remark.Style.Add("border", "1px solid #76767C");
            }
        }

        UpdatePanelformrujukan.Update();

    }

    public void DokterValidation(bool txt)
    {
        if (txt == true)
        {
            foreach (RepeaterItem item in rptReferral.Items)
            {

                DropDownList doctor_name = (DropDownList)item.FindControl("Ddl_DokterRujukan");
                doctor_name.Style.Add("border", "1px solid red");

            }
        }
        else
        {
            foreach (RepeaterItem item in rptReferral.Items)
            {
                DropDownList doctor_name = (DropDownList)item.FindControl("Ddl_DokterRujukan");
                doctor_name.Style.Add("border", "1px solid 76767C");
            }
        }

        UpdatePanelformrujukan.Update();
    }


    public void SpesialisValidation(bool txt)
    {
        if (txt == true)
        {
            foreach (RepeaterItem item in rptReferral.Items)
            {

                DropDownList spesialis_name = (DropDownList)item.FindControl("Ddl_DokterSpesialis");
                spesialis_name.Style.Add("border", "1px solid red");

            }
        }
        else
        {
            foreach (RepeaterItem item in rptReferral.Items)
            {
                DropDownList spesialis_name = (DropDownList)item.FindControl("Ddl_DokterSpesialis");
                spesialis_name.Style.Add("border", "1px solid 76767C");
            }
        }

        UpdatePanelformrujukan.Update();
    }



    public void UpdatePanel()
    {
        UpdatePanelformrujukan.Update();

    }

}
