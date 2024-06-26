﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Globalization;
using static PatientHistory;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Text;
using System.IO;
using static FirstAssesment;
using System.Web.UI.HtmlControls;

public partial class Form_SOAP_Control_Template_StdSubjective : System.Web.UI.UserControl
{
    public delegate bool customHandler(object sender);
    public event customHandler checkIfExist;
    public event customHandler checkIfExistAllergy;
    public event customHandler checkIfExistAllergyFood;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { }
        CheckVisibleDiv();
        checkAccordionSubjective();
    }

    public SOAP GetSubjectiveValues(SOAP SOA)
    {
        SOA.subjective.RemoveAll(x => x.soap_mapping_id == Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6"));
        if (chkScreen1.Checked)
        {
            Subjective tempscreening = new Subjective();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "Temperature";
            tempscreening.remarks = "Temperature >38 C";
            SOA.subjective.Add(tempscreening);
        }
        if (chkScreen2.Checked)
        {
            Subjective tempscreening = new Subjective();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "Batuk darah";
            tempscreening.remarks = "Hemoptysis";
            SOA.subjective.Add(tempscreening);
        }
        if (chkScreen3.Checked)
        {
            Subjective tempscreening = new Subjective();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "Berkeringat";
            tempscreening.remarks = "Night Sweats";
            SOA.subjective.Add(tempscreening);
        }
        if (chkScreen4.Checked)
        {
            Subjective tempscreening = new Subjective();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "Diare, mual & muntah";
            tempscreening.remarks = "Diarrhea, nausea, vomit";
            SOA.subjective.Add(tempscreening);
        }
        if (chkScreen5.Checked)
        {
            Subjective tempscreening = new Subjective();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "batuk";
            tempscreening.remarks = "Cough more than 2 weeks";
            SOA.subjective.Add(tempscreening);
        }
        if (chkScreen6.Checked)
        {
            Subjective tempscreening = new Subjective();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "swollen";
            tempscreening.remarks = "Swollen neck gland/s";
            SOA.subjective.Add(tempscreening);
        }
        if (chkScreen7.Checked)
        {
            Subjective tempscreening = new Subjective();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "kemerahan";
            tempscreening.remarks = "Skin rash";
            SOA.subjective.Add(tempscreening);
        }
        if (chkScreen8.Checked)
        {
            Subjective tempscreening = new Subjective();
            tempscreening.subjective_id = Guid.Empty;
            tempscreening.soap_mapping_id = Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6");
            tempscreening.soap_mapping_name = "INFECTIOUS DISEASE SYMPTOM";
            tempscreening.value = "luka";
            tempscreening.remarks = "Open wounds and pus";
            SOA.subjective.Add(tempscreening);
        }

        foreach (var subjective in SOA.subjective)
        {
            if (subjective.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade"))
            {//endemic
                if (rbkunjungan2.Checked)
                    subjective.remarks = txtEndemic.Text;
                else if (rbkunjungan1.Checked)
                    subjective.remarks = "";
            }
            //if (subjective.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf"))
            //{//patientcomplaint
            //    subjective.remarks = Complaint.Text;
            //}
            //if (subjective.soap_mapping_id == Guid.Parse("a41d51b7-9999-4045-b992-241f1fe679ca"))
            //{//current medication
            //    if (rbPengobatan2.Checked)
            //        subjective.remarks = txtPengobatan.Text;
            //    else if (rbPengobatan1.Checked)
            //        subjective.remarks = "";
            //}
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
            //if (subjective.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0"))
            //{//anamnesis
            //    subjective.remarks = Anamnesis.Text;
            //}
        }
        if (rbOperas2.Checked)
        {
            SOA.patient_surgery = GetRowList_PatientSurgery();
        }
        else if (rbOperasi.Checked)
        {
            SOA.patient_surgery.Clear();
        }

        if (rbPengobatan2.Checked)
        {
            SOA.patient_medication = GetRowList_PatientRoutineMedication();
        }
        else if (rbPengobatan1.Checked)
        {
            SOA.patient_medication.Clear();
        }

        if (rbdrug2.Checked)
        {
            SOA.patient_allergy = GetRowList_PatientAllergy(1);
            if (rbfood2.Checked)
            {
                SOA.patient_allergy.AddRange(GetRowList_PatientAllergy(2));
            }
        }
        else
        {
            if (rbfood2.Checked)
            {
                SOA.patient_allergy = GetRowList_PatientAllergy(2);
            }
            else
            {
                SOA.patient_allergy.Clear();
            }
        }

        //PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
        List<PatientDiseaseHistory> templistdisease = new List<PatientDiseaseHistory>();
        if (rbpribadi2.Checked)
        {
            if (chkdisease1.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Hypertension";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease2.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Stroke";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease3.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "TBC";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease4.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Kidney";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease5.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Convulsive";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease6.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Heart";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease7.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Diabetes";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease8.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Asthma";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdisease9.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Hepatitis";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }

            PatientDiseaseHistory temp_patienthistoryremark = new PatientDiseaseHistory();
            temp_patienthistoryremark.patient_disease_history_id = Guid.Empty;
            temp_patienthistoryremark.value = "Lain-lain";
            temp_patienthistoryremark.remarks = txtDisease.Text;
            temp_patienthistoryremark.disease_history_type = 1;
            temp_patienthistoryremark.is_delete = 0;
            templistdisease.Add(temp_patienthistoryremark);

        }
        else if (rbpribadi1.Checked)
        {
            SOA.patient_disease.Clear();
        }

        if (rbkeluarga2.Checked)
        {
            if (chkdiseasefam1.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Heart";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 2;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdiseasefam2.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Diabetes";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 2;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdiseasefam3.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Asthma";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 2;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdiseasefam4.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Hypertension";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 2;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            if (chkdiseasefam5.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Cancer";
                temp_patienthistory.remarks = "";
                temp_patienthistory.disease_history_type = 2;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);
            }
            PatientDiseaseHistory temp_patienthistoryremark = new PatientDiseaseHistory();
            temp_patienthistoryremark.patient_disease_history_id = Guid.Empty;
            temp_patienthistoryremark.value = "Lain-lain";
            temp_patienthistoryremark.remarks = txtDiseaseFam.Text;
            temp_patienthistoryremark.disease_history_type = 2;
            temp_patienthistoryremark.is_delete = 0;
            templistdisease.Add(temp_patienthistoryremark);
        }
        SOA.patient_disease = templistdisease;
        return SOA;
    }

    protected List<PatientSurgery> GetRowList_PatientSurgery()
    {
        List<PatientSurgery> data = new List<PatientSurgery>();
        foreach (GridViewRow rows in gvw_surgery.Rows)
        {
            HiddenField patient_surgery_id = (HiddenField)rows.FindControl("patient_surgery_id");
            Label surgery_type = (Label)rows.FindControl("surgery_type");
            Label surgery_date = (Label)rows.FindControl("surgery_date");

            PatientSurgery row = new PatientSurgery();

            row.patient_surgery_id = Guid.Parse(patient_surgery_id.Value);
            row.surgery_type = surgery_type.Text;
            row.surgery_date = DateTime.Parse(surgery_date.Text);
            row.is_delete = 0;
            data.Add(row);
        }
        return data;
    }

    protected List<PatientRoutineMedication> GetRowList_PatientRoutineMedication()
    {
        List<PatientRoutineMedication> data = new List<PatientRoutineMedication>();
        foreach (GridViewRow rows in gvw_routinemed.Rows)
        {
            HiddenField patient_routine_medication_id = (HiddenField)rows.FindControl("patient_routine_medication_id");
            Label medication = (Label)rows.FindControl("medication");

            PatientRoutineMedication row = new PatientRoutineMedication();

            row.patient_routine_medication_id = Guid.Parse(patient_routine_medication_id.Value);
            row.medication = medication.Text;
            row.is_delete = 0;
            data.Add(row);
        }
        return data;
    }

    protected List<PatientAllergy> GetRowList_PatientAllergy(int type)
    {
        List<PatientAllergy> data = new List<PatientAllergy>();
        if (type == 1)
        {
            foreach (GridViewRow rows in gvw_allergy.Rows)
            {
                HiddenField patient_allergy_id = (HiddenField)rows.FindControl("patient_allergy_id");
                Label allergy = (Label)rows.FindControl("allergy");
                Label allergy_reaction = (Label)rows.FindControl("allergy_reaction");

                PatientAllergy row = new PatientAllergy();

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

                PatientAllergy row = new PatientAllergy();

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

    protected void btnAddRoutineMed_onClick(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        List<PatientRoutineMedication> data = GetRowList_PatientRoutineMedication();
        if (data == null)
        {
            dt.Columns.Add("patient_routine_medication_id");
            dt.Columns.Add("medication");
            dt.Columns.Add("is_delete");
            dt.Rows.Add(new Object[] { Guid.Empty, txtRoutineMed.Text, 0 });
            gvw_routinemed.DataSource = dt;
            gvw_routinemed.DataBind();

            DataTable dtroutinemed = dt;
            dtroutinemed.Columns["medication"].ColumnName = "current_medication";

            //Session["routinemed"] = dtroutinemed;
            //checkIfExist(sender);
        }
        else
        {
            DataTable dtRoutine = Helper.ToDataTable(data);
            dtRoutine.Rows.Add(new Object[] { Guid.Empty, txtRoutineMed.Text, 0 });
            gvw_routinemed.DataSource = dtRoutine;
            gvw_routinemed.DataBind();

            DataTable dtroutinemed = dtRoutine;
            dtroutinemed.Columns["medication"].ColumnName = "current_medication";

            //Session["routinemed"] = dtroutinemed;
            //checkIfExist(sender);
        }
        txtRoutineMed.Text = "";
        txtRoutineMed.Focus();
        CheckVisibleDiv();
    }

    protected void btnAddSurgery_onClick(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        List<PatientSurgery> data = GetRowList_PatientSurgery();
        if (data == null)
        {
            dt.Columns.Add("patient_surgery_id");
            dt.Columns.Add("surgery_type");
            dt.Columns.Add("surgery_date");
            dt.Columns.Add("is_delete");
            dt.Rows.Add(new Object[] { Guid.Empty, txtSurgeryName.Text, DateTime.Parse(txtSurgeryDate.Text), 0 });
            gvw_surgery.DataSource = dt;
            gvw_surgery.DataBind();
        }
        else
        {
            DataTable dtSurgery = Helper.ToDataTable(data);
            dtSurgery.Rows.Add(new Object[] { Guid.Empty, txtSurgeryName.Text, DateTime.Parse(txtSurgeryDate.Text), 0 });
            gvw_surgery.DataSource = dtSurgery;
            gvw_surgery.DataBind();
        }
        txtSurgeryName.Text = "";
        txtSurgeryName.Focus();
        CheckVisibleDiv();
    }

    public void CheckVisibleDiv()
    {
        if (rbPengobatan2.Checked)
            ScriptManager.RegisterStartupScript(upCurrMedication, upCurrMedication.GetType(), "alert1", "ShowHideDiv();", true);
        else
            ScriptManager.RegisterStartupScript(upCurrMedication, upCurrMedication.GetType(), "alert1", "HideDiv();", true);

        if (rbOperas2.Checked)
            ScriptManager.RegisterStartupScript(upSurgery, upSurgery.GetType(), "alert2", "ShowHideDiv7();", true);
        else
            ScriptManager.RegisterStartupScript(upSurgery, upSurgery.GetType(), "alert2", "HideDiv7();", true);

        if (rbpribadi2.Checked)
            ScriptManager.RegisterStartupScript(upPribadi, upPribadi.GetType(), "alert3", "ShowHideDiv2();", true);
        else
            ScriptManager.RegisterStartupScript(upPribadi, upPribadi.GetType(), "alert3", "HideDiv2();", true);

        if (rbkeluarga2.Checked)
            ScriptManager.RegisterStartupScript(upKeluarga, upKeluarga.GetType(), "alert4", "ShowHideDiv3();", true);
        else
            ScriptManager.RegisterStartupScript(upKeluarga, upKeluarga.GetType(), "alert4", "HideDiv3();", true);

        if (rbkunjungan2.Checked)
            ScriptManager.RegisterStartupScript(upEndemic, upEndemic.GetType(), "alert5", "ShowHideDiv4();", true);
        else
            ScriptManager.RegisterStartupScript(upEndemic, upEndemic.GetType(), "alert5", "HideDiv4();", true);

        if (rbnutrisi2.Checked)
            ScriptManager.RegisterStartupScript(upNutrition, upNutrition.GetType(), "alert8", "ShowHideDiv5();", true);
        else
            ScriptManager.RegisterStartupScript(upNutrition, upNutrition.GetType(), "alert8", "HideDiv5();", true);

        if (rbpuasa2.Checked)
            ScriptManager.RegisterStartupScript(upFasting, upFasting.GetType(), "alert9", "ShowHideDiv6();", true);
        else
            ScriptManager.RegisterStartupScript(upFasting, upFasting.GetType(), "alert9", "HideDiv6();", true);

        if (rbdrug2.Checked)
            ScriptManager.RegisterStartupScript(upAllergies, upAllergies.GetType(), "alert6", "ShowHideDiv8();", true);
        else
            ScriptManager.RegisterStartupScript(upAllergies, upAllergies.GetType(), "alert6", "HideDiv8();", true);

        if (rbfood2.Checked)
            ScriptManager.RegisterStartupScript(upFoods, upFoods.GetType(), "alert7", "ShowHideDiv9();", true);
        else
            ScriptManager.RegisterStartupScript(upFoods, upFoods.GetType(), "alert7", "HideDiv9();", true);

        checkAccordionSubjective();

        //ScriptManager.RegisterStartupScript(upFoods, upFoods.GetType(), "accordion1", "checkAccordion();", true);
        //string divstyle = collapseRiwayat.Attributes["class"].ToString();
        //if (divstyle == "panel-collapse collapse in")
        //{
        //    collapseRiwayat.Attributes.Add("class", "panel-collapse collapse in");
        //}
    }

    public void checkAccordionSubjective()
    {
        ScriptManager.RegisterStartupScript(upFoods, upFoods.GetType(), "accordion1", "checkAccordion();", true);
    }

    public void initializevalue(List<Subjective> listsubjective, List<PatientDiseaseHistory> listPatientDiseases, List<PatientSurgery> listPatientSurgery, List<PatientAllergy> listPatientAllergy, List<PatientRoutineMedication> listPatientRoutine)
    {
        //listsubjective = Jsongetsoap.list.subjective;
        if (listsubjective.Count > 0)
        {
            foreach (Subjective x in listsubjective)
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
                //else if (x.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0"))
                //{//anamnesis
                //    Anamnesis.Text = x.remarks;
                //}
                else if (x.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea"))
                {//nutrition
                    if (x.remarks != "")
                    {
                        txtNutrition.Text = x.remarks;
                        rbnutrisi2.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript5", "ShowHideDiv5();", true);
                    }
                    else
                    {
                        rbnutrisi1.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript5", "HideDiv5();", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48"))
                {//fasting
                    if (x.remarks != "")
                    {
                        txtFasting.Text = x.remarks;
                        rbpuasa2.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript6", "ShowHideDiv6();", true);
                    }
                    else
                    {
                        rbpuasa1.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript6", "HideDiv6();", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade"))
                {//endemic
                    if (x.remarks != "")
                    {
                        txtEndemic.Text = x.remarks;
                        rbkunjungan2.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript4", "ShowHideDiv4();", true);
                    }
                    else
                    {
                        rbkunjungan1.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript4", "HideDiv4();", true);
                    }
                }
                //else if (x.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf"))
                //{//patient complaint
                //    if (x.remarks != "")
                //    {
                //        Complaint.Text = x.remarks;
                //    }
                //}
            }
        }

        int tempflagdisease = 0, tempflagdiseasefam = 0;
        if (listPatientDiseases.Count > 0)
        {
            foreach (PatientDiseaseHistory x in listPatientDiseases)
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
        if (listPatientRoutine.Count > 0)
        {
            if (Helper.ToDataTable(listPatientRoutine).Select("is_delete = 0").Count() > 0)
            {
                DataTable dt = Helper.ToDataTable(listPatientRoutine);
                gvw_routinemed.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
                gvw_routinemed.DataBind();
            }
            rbPengobatan2.Checked = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "ShowHideDiv();", true);
        }
        else
        {
            gvw_routinemed.DataSource = null;
            gvw_routinemed.DataBind();
            rbPengobatan1.Checked = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript7", "HideDiv();", true);
        }

        if (listPatientSurgery.Count > 0)
        {//patient surgery
            if (Helper.ToDataTable(listPatientSurgery).Select("is_delete = 0").Count() > 0)
            {
                DataTable dt = Helper.ToDataTable(listPatientSurgery);
                gvw_surgery.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
                gvw_surgery.DataBind();
            }
            rbOperas2.Checked = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript7", "ShowHideDiv7();", true);
        }
        else
        {
            gvw_surgery.DataSource = null;
            gvw_surgery.DataBind();
            rbOperasi.Checked = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript7", "HideDiv7();", true);
        }

        int tempflagallergy = 0, tempflagfoods = 0;
        if (listPatientAllergy.Count > 0)
        {//patient allergy
            if (Helper.ToDataTable(listPatientAllergy).Select("is_delete = 0").Count() > 0)
            {
                DataTable dtAllergy = Helper.ToDataTable(listPatientAllergy);
                if (dtAllergy.Select("is_delete = 0 and allergy_type = 1").Count() > 0)
                {
                    gvw_allergy.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
                    gvw_allergy.DataBind();
                    rbdrug2.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript8", "ShowHideDiv8();", true);
                }
                else
                {
                    gvw_allergy.DataSource = null;
                    gvw_allergy.DataBind();
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
                    gvw_foods.DataSource = null;
                    gvw_foods.DataBind();
                    rbfood1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript9", "HideDiv9();", true);
                }
            }
        }
        else
        {
            gvw_allergy.DataSource = null;
            gvw_allergy.DataBind();
            gvw_foods.DataSource = null;
            gvw_foods.DataBind();
            rbdrug1.Checked = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript8", "HideDiv8();", true);
            rbfood1.Checked = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript9", "HideDiv9();", true);
        }
        CheckVisibleDiv();
    }

    protected void btnDeleteRoutineMed_onClick(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((Button)sender).Parent.Parent)).RowIndex;
        HiddenField patient_surgery_id = (HiddenField)gvw_routinemed.Rows[selRowIndex].FindControl("patient_routine_medication_id");
        Label item_name = (Label)gvw_routinemed.Rows[selRowIndex].FindControl("medication");
        List<PatientRoutineMedication> data = GetRowList_PatientRoutineMedication();
        DataTable dt = Helper.ToDataTable(data);
        dt.Rows[selRowIndex].SetField("is_delete", 1);
        if (dt.Select("is_delete = 0").Count() > 0)
        {
            gvw_routinemed.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
            gvw_routinemed.DataBind();

            DataTable dtroutinemed = dt.Select("is_delete = 0").CopyToDataTable();
            dtroutinemed.Columns["medication"].ColumnName = "current_medication";

            //Session["routinedeleted"] = item_name.Text;
            //Session["routinemed"] = dtroutinemed;
            //checkIfExist(sender);
        }
        else
        {
            gvw_routinemed.DataSource = null;
            gvw_routinemed.DataBind();

            //Session["routinedeleted"] = item_name.Text;
            //Session["routinemed"] = null;
            //checkIfExist(sender);
        }
        //CheckVisibleDiv();
    }

    protected void btnDeleteSurgery_onClick(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((Button)sender).Parent.Parent)).RowIndex;
        HiddenField patient_surgery_id = (HiddenField)gvw_surgery.Rows[selRowIndex].FindControl("patient_surgery_id");

        List<PatientSurgery> data = GetRowList_PatientSurgery();
        DataTable dt = Helper.ToDataTable(data);
        dt.Rows[selRowIndex].SetField("is_delete", 1);
        if (dt.Select("is_delete = 0").Count() > 0)
        {
            gvw_surgery.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
            gvw_surgery.DataBind();
        }
        else
        {
            gvw_surgery.DataSource = null;
            gvw_surgery.DataBind();
        }
        CheckVisibleDiv();
    }

    protected void btnDeleteAllergy_onClick(object sender, EventArgs e)
    {
        DataTable dt;
        int selRowIndex = ((GridViewRow)(((Button)sender).Parent.Parent)).RowIndex;
        HiddenField patient_allergy_id = (HiddenField)gvw_allergy.Rows[selRowIndex].FindControl("patient_allergy_id");

        List<PatientAllergy> data = GetRowList_PatientAllergy(1);
        dt = Helper.ToDataTable(data);
        dt.Rows[selRowIndex].SetField("is_delete", 1);
        if (dt.Select("is_delete = 0").Count() > 0)
        {
            gvw_allergy.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
            gvw_allergy.DataBind();
            
            //Session["routinemed"] = dt.Select("is_delete = 0").CopyToDataTable();
            //checkIfExistAllergy(sender);
        }
        else
        {
            gvw_allergy.DataSource = null;
            gvw_allergy.DataBind();

            //Session["routinemed"] = null;
            //checkIfExistAllergy(sender);
        }
        
        CheckVisibleDiv();
    }

    protected void btnDeleteFoods_onClick(object sender, EventArgs e)
    {
        DataTable dt;
        int selRowIndex = ((GridViewRow)(((Button)sender).Parent.Parent)).RowIndex;
        HiddenField patient_allergy_id = (HiddenField)gvw_foods.Rows[selRowIndex].FindControl("patient_allergy_id");

        List<PatientAllergy> data = GetRowList_PatientAllergy(2);
        dt = Helper.ToDataTable(data);
        dt.Rows[selRowIndex].SetField("is_delete", 1);
        if (dt.Select("is_delete = 0").Count() > 0)
        {
            gvw_foods.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
            gvw_foods.DataBind();

            //Session["routinemed"] = dt.Select("is_delete = 0").CopyToDataTable();
            //checkIfExistAllergyFood(sender);
        }
        else
        {
            gvw_foods.DataSource = null;
            gvw_foods.DataBind();

            //Session["routinemed"] = null;
            //checkIfExistAllergyFood(sender);
        }
        
        CheckVisibleDiv();
    }

    protected void btnAddAllergy_onClick(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        List<PatientAllergy> data = GetRowList_PatientAllergy(1);

        if (data == null)
        {
            dt.Columns.Add("patient_allergy_id");
            dt.Columns.Add("allergy_type");
            dt.Columns.Add("allergy");
            dt.Columns.Add("allergy_reaction");
            dt.Columns.Add("is_delete");
            dt.Rows.Add(new Object[] { Guid.Empty, 1, txtDrugsAllergy.Text, txtReactionAllergy.Text, 0 });
            gvw_allergy.DataSource = dt;
            gvw_allergy.DataBind();
            
            //Session["routinemed"] = dt;
            //checkIfExistAllergy(sender);
        }
        else
        {
            DataTable dtAllergy = Helper.ToDataTable(data);
            dtAllergy.Rows.Add(new Object[] { Guid.Empty, 1, txtDrugsAllergy.Text, txtReactionAllergy.Text, 0 });
            gvw_allergy.DataSource = dtAllergy;
            gvw_allergy.DataBind();
            
            //Session["routinemed"] = dtAllergy;
            //checkIfExistAllergy(sender);
        }
        txtDrugsAllergy.Focus();
        CheckVisibleDiv();
    }

    protected void btnAddFoodAllergy_onClick(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        List<PatientAllergy> data = GetRowList_PatientAllergy(2);

        if (data == null)
        {
            dt.Columns.Add("patient_allergy_id");
            dt.Columns.Add("allergy_type");
            dt.Columns.Add("allergy");
            dt.Columns.Add("allergy_reaction");
            dt.Columns.Add("is_delete");
            dt.Rows.Add(new Object[] { Guid.Empty, 2, txtDrugsFoods.Text, txtReactionFoods.Text, 0 });
            gvw_foods.DataSource = dt;
            gvw_foods.DataBind();

            //Session["routinemed"] = dt;
            //checkIfExistAllergyFood(sender);
        }
        else
        {
            DataTable dtAllergy = Helper.ToDataTable(data);
            dtAllergy.Rows.Add(new Object[] { Guid.Empty, 2, txtDrugsFoods.Text, txtReactionFoods.Text, 0 });
            gvw_foods.DataSource = dtAllergy;
            gvw_foods.DataBind();

            //Session["routinemed"] = dtAllergy;
            //checkIfExistAllergyFood(sender);
        }
        txtDrugsFoods.Focus();
        CheckVisibleDiv();
    }
    
//    protected void btnRoutineMedNone(object sender, EventArgs e)
//    {
//        Session["routinemed"] = null;
//        checkIfExist(sender);
//        CheckVisibleDiv();
//    }

//    protected void btnAllergyDrugNone_onClick(object sender, EventArgs e)
//    {
//        Session["routinemed"] = null;
//        checkIfExistAllergy(sender);
//        CheckVisibleDiv();
//    }

//    protected void btnFoodAllergyNone_onClick(object sender, EventArgs e)
//    {
//        Session["routinemed"] = null;
//        checkIfExistAllergyFood(sender);
//        CheckVisibleDiv();
//    }
    
//    protected void btnRoutineMedShow(object sender, EventArgs e)
//    {
//        List<PatientRoutineMedication> data = GetRowList_PatientRoutineMedication();
//        if (data == null)
//        {
//            Session["routinemed"] = null;
//            checkIfExist(sender);
//        }
//        else
//        {
//            DataTable dtRoutine = Helper.ToDataTable(data);

//            DataTable dtroutinemed = dtRoutine;
//            dtroutinemed.Columns["medication"].ColumnName = "current_medication";

//            Session["routinemed"] = dtroutinemed;
//            checkIfExist(sender);
//        }
//        CheckVisibleDiv();
//    }

//    protected void btnAllergyDrugShow_onClick(object sender, EventArgs e)
//    {
//        List<PatientAllergy> data = GetRowList_PatientAllergy(1);
        
//        if (data == null)
//        {
//            Session["routinemed"] = null;
//            checkIfExistAllergy(sender);
//        }
//        else
//        {
//            DataTable dtAllergy = Helper.ToDataTable(data);
            
//            Session["routinemed"] = dtAllergy;
//            checkIfExistAllergy(sender);
//        }
//        CheckVisibleDiv();
//    }

//    protected void btnFoodAllergyShow_onClick(object sender, EventArgs e)
//    {
//        List<PatientAllergy> data = GetRowList_PatientAllergy(2);

//        if (data == null)
//        {
//            Session["routinemed"] = null;
//            checkIfExistAllergyFood(sender);
//        }
//        else
//        {
//            DataTable dtAllergy = Helper.ToDataTable(data);

//            Session["routinemed"] = dtAllergy;
//            checkIfExistAllergyFood(sender);
//        }
//        CheckVisibleDiv();
//    }
}