using Newtonsoft.Json;
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
using System.Data.SqlClient;
using System.Configuration;
using static PatientHistory;
using System.Web.Script.Serialization;
using System.Web.Services;

public partial class Form_SOAP_Control_Template_StdPlanning : System.Web.UI.UserControl
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    List<string> itemname = new List<string>();
    List<CpoeTrans> listchecked = new List<CpoeTrans>();
    List<Item> listitem = new List<Item>();
    List<PrescriptionDrug> listprescriptiondrug = new List<PrescriptionDrug>();

    List<UOM> listUOM = new List<UOM>();
    List<Frequency> listfrequency = new List<Frequency>();
    List<Dose> listdose = new List<Dose>();
    List<AdministrationRoute> listadministrationRoute = new List<AdministrationRoute>();
    List<OrderSet> listordersetheader, listordersetlab = new List<OrderSet>();
    List<FrequentDrug> listfrequentdrugs = new List<FrequentDrug>();
    
    public DataTable uomdt = new DataTable();
    public DataTable dosedt = new DataTable();
    public DataTable frequencydt = new DataTable();
    public DataTable routedt = new DataTable();
    public DataTable ordersetdt = new DataTable();
    public DataTable labsetdt = new DataTable();

    public string setENG = "none";
    public string setIND = "none";

    void ShowToastr(string message, string title, string type)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastr_message",
             String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        if (Session[Helper.SessionLanguage] == null)
            Session[Helper.SessionLanguage] = 1;

        if (Session[Helper.SessionLanguage].ToString() == "1")
        {
            //HiddenFieldENG.Value = "True";
            //HiddenFieldIND.Value = "False";
            setENG = "";
            setIND = "none";
            HFisBahasaSOAP_Planning.Value = "ENG";
        }
        else if (Session[Helper.SessionLanguage].ToString() == "2")
        {
            //HiddenFieldENG.Value = "False";
            //HiddenFieldIND.Value = "True";
            setENG = "none";
            setIND = "";
            HFisBahasaSOAP_Planning.Value = "IND";
        }
        else
        {
            //HiddenFieldENG.Value = "True";
            //HiddenFieldIND.Value = "False";
            setENG = "";
            setIND = "none";
            HFisBahasaSOAP_Planning.Value = "ENG";
        }

        //set bahasa
        ScriptManager.RegisterStartupScript(this, GetType(), "bahasa", "switchBahasaSOAP_Planning();", true);

        if (!IsPostBack)
        {
            // Set Date Diagnostic Attribute to readonly
            dp_diag.Attributes.Add("readonly", "readonly");
            dp_proc.Attributes.Add("readonly", "readonly");
            dp_labFutureOrder.Attributes.Add("readonly", "readonly");
            dp_radFutureOrder.Attributes.Add("readonly", "readonly");

            //Clear Button Future Order Date
            ClearButtonFutureOrderDate();

            //Log.Info(LogConfig.LogStart());

            HiddenFlagTabSet.Value = "lab";

            //var x = (this.Page as dynamic).tempcurrmedication;

            //List<Item> listitem = (List<Item>)Session["itempres"];
            //DataTable dtitem = Helper.ToDataTable(listitem);

            DataTable dtItem = (DataTable)Session[Helper.SessionItemDrugPres];

            if (dtItem.Rows[0]["ConnStatus"].ToString().ToLower() == "offline")
            {
                divwarningconnection.Visible = true;
                HF_connstatus.Value = "offline";
            }
            
            //chkformularium.Checked = true;
            //dibuka lagi jika menerapkan metode pencarian sebelumnya
            //gvw_data.DataSource = (dtItem.Select("Formularium = '" + hfPayerType.Value + "'").CopyToDataTable()).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
            //gvw_data.DataBind();
            //gvw_add_drugs.DataSource = (dtItem.Select("Formularium = '" + hfPayerType.Value + "'").CopyToDataTable()).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
            //gvw_add_drugs.DataBind();

            //}
            //else
            //{
            //    gvw_data.DataSource = (dtItem.Select("Formularium = '" + hfPayerType.Value + "'").CopyToDataTable()).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
            //    gvw_data.DataBind();
            //}

            if (hftakedate.Value == "1")
            {
                //chkformularium.Enabled = false;
                txtItemAdd.Enabled = false;
                txtItemAdd_AC.Enabled = false;
                DisableRowList(1);
                BtnCheckDrugInteraction.Enabled = false;
                //DisableRowList(2);
                DisableRowList(5);
                txtPresNotes.ReadOnly = true;

                LB_tambahracikanbaru.Enabled = false;
                LB_tambahracikanbaru.CssClass = "btn btn-default btn-sm disabled";
                DisableRowList(11);
            }

            if (hfadditional_take_date.Value == "1")
            {
                txtItemAddDrugs.Enabled = false;
                txtItemAdd_AC_additional.Enabled = false;
                DisableRowList(9);
                BtnCheckDrugInteraction_Add.Enabled = false;
                txtItemAddCons.Enabled = false;
                txtItemCons_AC_additional.Enabled = false;
                DisableRowList(10);
                txtAdditionalPresNotes.ReadOnly = true;

                LB_tambahracikanbaru_add.Enabled = false;
                LB_tambahracikanbaru_add.CssClass = "btn btn-default btn-sm disabled";
                DisableRowList(12);
            }

            if (Helper.GetFlagCompound(this.Parent.Page) == "FALSE")
            {
                dvRacikanShow.Visible = false;
                dvAdditionalRacikanShow.Visible = false;
            }
            else
            {
                dvRacikanShow.Visible = true;     
            }

            //for manual use
            //divcompound.Visible = false;
            //dvRacikanShow.Visible = false;
            //dvAdditionalRacikanShow.Visible = false;
            //end for manual use

            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Where(y => y.setting_name.ToUpper() == "MANDATORY_COMPOUND").Count() > 0)
            {
                HFmandatoryRacikan.Value = orgsetting.Find(y => y.setting_name.ToUpper() == "MANDATORY_COMPOUND").setting_value.ToString();
            }
            // config for future order based on organization setting
            // if use future order is true than future order show if not than hide
            if (orgsetting.Where(y => y.setting_name.ToUpper() == "USE_FUTUREORDER").Count() > 0)
            {
                if (bool.Parse(orgsetting.Find(y => y.setting_name.ToUpper() == "USE_FUTUREORDER").setting_value))
                {divBtnFutureOrderDiAGPROC.Style.Add("display", "flex");}
                else{divBtnFutureOrderDiAGPROC.Style.Add("display", "none");}
            }

            HFflagRacikan.Value = Helper.GetFlagCompound(this.Parent.Page);
            hfmandatoryFA.Value = orgsetting.Find(y => y.setting_name.ToUpper() == "MANDATORY_DOCTORFA".ToUpper()).setting_value;

            DataTable dtfreqtemp = (DataTable)Session[Helper.SessionFrequency];           
            DataTable dtroutetemp = (DataTable)Session[Helper.SessionRoute];            
            DataTable dtdosetemp = (DataTable)Session[Helper.Sessiondosage];
            DataTable dtuomtemp = (DataTable)Session[Helper.Sessionuom];

            if (dtfreqtemp == null)
            {
                var frequencyData = clsOrderSet.getFrequency();
                var Jsonfrequency = JsonConvert.DeserializeObject<ResultFrequency>(frequencyData.Result.ToString());
                listfrequency = Jsonfrequency.list;
                frequencydt = Helper.ToDataTable(listfrequency);
                DataView dv = frequencydt.DefaultView;
                dv.Sort = "name asc";
                frequencydt = dv.ToTable();
                Session[Helper.SessionFrequency] = frequencydt;
            }

            if (dtroutetemp == null)
            {
                var administrationRouteData = clsOrderSet.getAdministrationRoute();
                var JsonadministrationRoute = JsonConvert.DeserializeObject<ResultAdministrationRoute>(administrationRouteData.Result.ToString());
                listadministrationRoute = JsonadministrationRoute.list;
                routedt = Helper.ToDataTable(listadministrationRoute);
                DataView dvroute = routedt.DefaultView;
                dvroute.Sort = "name asc";
                routedt = dvroute.ToTable();
                Session[Helper.SessionRoute] = routedt;
            }

            if (dtdosetemp == null)
            {
                var dosage = clsSOAP.getDosage();
                var Jsondosage = JsonConvert.DeserializeObject<ResultDose>(dosage.Result.ToString());
                List<Dose> listdosage = Jsondosage.list;
                DataTable dosagedt = Helper.ToDataTable(listdosage);
                DataView dvdose = dosagedt.DefaultView;
                dvdose.Sort = "name asc";
                dosagedt = dvdose.ToTable();
                Session[Helper.Sessiondosage] = dosagedt;
            }

            if (dtuomtemp == null)
            {
                var uom = clsOrderSet.getUOM();
                var Jsonuom = JsonConvert.DeserializeObject<ResultUOM>(uom.Result.ToString());
                List<UOM> listuom = Jsonuom.list;
                DataTable uomdt = Helper.ToDataTable(listuom);
                DataView dvuom = uomdt.DefaultView;
                dvuom.Sort = "name asc";
                uomdt = dvuom.ToTable();
                Session[Helper.Sessionuom] = uomdt;
            }


            var getMap = clsCpoeMapping.GetMapping(Helper.organizationId);
            var getMapJson = JsonConvert.DeserializeObject<ResultMapping>(getMap.Result.ToString());
            Session[Helper.Sessionmaplab] = getMapJson.list;

            stdclinic.GetMappingClinicLab(getMapJson.list, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdmicro.GetMappingMicroLab(getMapJson.list, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdcito.GetMappingCitoLab(getMapJson.list, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdxray.GetMappingClinicLab(getMapJson.list, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdusg.GetMappingClinicLab(getMapJson.list, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdctrad.GetMappingClinicLab(getMapJson.list, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdmrihalf.GetMappingClinicLab(getMapJson.list, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdmrifull.GetMappingClinicLab(getMapJson.list, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdanatomi.GetMappingAnatomiLab(getMapJson.list, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdmdc.GetMappingMDC(getMapJson.list, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdpanel.GetMappingPanelLab(getMapJson.list, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdmdc.GetMappingMDC(getMapJson.list, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdpanel.SetIPlocal(Helper.GetLocalIPAddress());
            Label_updatestockdrug.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm");
            Label_updatestockdrugadd.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm");

            HF_Label_updatestockdrug.Value = DateTime.Now.ToString("dd MMM yyyy, HH:mm");

            if (chkformularium.Checked == true)
            {
                hfchkformularium.Value = "true";
            }
            else
            {
                hfchkformularium.Value = "false";
            }

            //Racikan
            loadDDlModalRacikan();

            Session[Helper.SessionMimsResultData] = null;

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "Page_Load", StartTime, EndTime, "OK", MyUser.GetUsername()
                          , "", "", ""));
            //Log.Info(LogConfig.LogEnd());
        }

        stdclinic.checkIfExist += new Form_CPOE_Control_Template_RptClinicalLab.customHandler(MyParentMethod);
        stdmicro.checkIfExistMicro += new Form_CPOE_Control_Template_RptMicroLab.customHandler(MyParentMethod);
        stdcito.checkIfExistCito += new Form_CPOE_Control_Template_RptCitoLab.customHandler(MyParentMethod);
        stdanatomi.checkIfExistAnatomi += new Form_CPOE_Control_Template_RptAnatomiLab.customHandler(MyParentMethod);
        stdmdc.checkIfExistMDC += new Form_CPOE_Control_Template_RptMDCLab.customHandler(MyParentMethod);
        stdpanel.checkIfExistPanel += new Form_CPOE_Control_Template_RptPanelLab.customHandler(MyParentMethod);

        stdxray.checkIfExist += new Form_CPOE_Control_Template_RptXrayRad.customHandler(MyParentMethodRadiology);
        stdusg.checkIfExistUSG += new Form_CPOE_Control_Template_RptUSGRad.customHandler(MyParentMethodRadiology);
        stdctrad.checkIfExist += new Form_CPOE_Control_Template_RptCTRad.customHandler(MyParentMethodRadiology);
        stdmrihalf.checkIfExist += new Form_CPOE_Control_Template_RptMRIhalfRad.customHandler(MyParentMethodRadiology);
        stdmrifull.checkIfExist += new Form_CPOE_Control_Template_RptMRIfullRad.customHandler(MyParentMethodRadiology);

        setTextAreaHight();

    }
    
    public void setTextAreaHight()
    {
        txtDocNurseNotes.Rows = txtDocNurseNotes.Text.Split('\n').Length;
        txtclinicaldiagnosis.Rows = txtclinicaldiagnosis.Text.Split('\n').Length;
        txtplanningotherLab.Rows = txtplanningotherLab.Text.Split('\n').Length;
        txtplanningotherRad.Rows = txtplanningotherRad.Text.Split('\n').Length;
        txtplanningotherLab_FutureOrder.Rows = txtplanningotherLab_FutureOrder.Text.Split('\n').Length;
        txtplanningotherRad_FutureOrder.Rows = txtplanningotherRad_FutureOrder.Text.Split('\n').Length;
        txtPresNotes.Rows = txtPresNotes.Text.Split('\n').Length;
        txtPharmacistNotes.Rows = txtPharmacistNotes.Text.Split('\n').Length;
        txtAdditionalPresNotes.Rows = txtPresNotes.Text.Split('\n').Length;
        txtAdditionalPharmacistNotes.Rows = txtPharmacistNotes.Text.Split('\n').Length;
    }

    public void ClearButtonFutureOrderDate()
	{
        //Clear Button Future Order Date
        if (dp_labFutureOrder.Text != "")
        {
            dp_labFutureOrderDelete.Style.Add("display", "block");
        }
        else
        {
            dp_labFutureOrderDelete.Style.Add("display", "none");
        }

        if (dp_radFutureOrder.Text != "")
        {
            dp_radFutureOrderDelete.Style.Add("display", "block");

        }
        else
        {
            dp_radFutureOrderDelete.Style.Add("display", "none");
        }

        if (dp_diag.Text != "")
        {
            dp_diagDelete.Style.Add("display", "block");

        }
        else
        {
            dp_diagDelete.Style.Add("display", "none");
        }

        if (dp_proc.Text != "")
        {
            dp_procDelete.Style.Add("display", "block");

        }
        else
        {
            dp_procDelete.Style.Add("display", "none");
        }
    }

    public void UpdateRoutineMedication(DataTable dt)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            if (dt != null)
            {
                if (dt.Rows[0]["medication"].ToString() == "Tidak Ada Pengobatan")
                {
                    rbpengobatan1.Checked = true;
                    rbpengobatan2.Checked = false;
                }
                else
                {
                    rbpengobatan1.Checked = false;
                    rbpengobatan2.Checked = true;
                }

                RepCurrentMedication.DataSource = dt;
                RepCurrentMedication.DataBind();
                noroutine.Style.Add("display", "none");

            }
            else
            {
                RepCurrentMedication.DataSource = null;
                RepCurrentMedication.DataBind();
                noroutine.Style.Add("display", "");

            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "UpdateRoutineMedication", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "UpdateRoutineMedication", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    public void UpdateDrugAllergy(DataTable dt)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        try
        {
            if (dt != null)
            {
                if (dt.Select("allergy_type = 1").Count() > 0)
                {
                    DataTable dtdrug = dt.Select("allergy_type = 1").CopyToDataTable();
                    if (dtdrug.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                    {
                        rbdrug1.Checked = true;
                        rbdrug2.Checked = false;
                    }
                    else
                    {
                        rbdrug1.Checked = false;
                        rbdrug2.Checked = true;
                    }

                    DrugAllergy.DataSource = dt.Select("allergy_type = 1").CopyToDataTable();
                    DrugAllergy.DataBind();
                    nodrugs.Style.Add("display", "none");
                    
                }
                else
                {
                    DrugAllergy.DataSource = null;
                    DrugAllergy.DataBind();
                    nodrugs.Style.Add("display", "");
                }
            }
            else
            {
                nodrugs.Style.Add("display", "");
                DrugAllergy.DataSource = null;
                DrugAllergy.DataBind();
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "UpdateDrugAllergy", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "UpdateDrugAllergy", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    public void UpdateFoodAllergy(DataTable dt)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            if (dt != null)
            {
                if (dt.Select("allergy_type = 2").Count() > 0)
                {
                    DataTable dtfood = dt.Select("allergy_type = 2").CopyToDataTable();
                    if (dtfood.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                    {
                        rbfood1.Checked = true;
                        rbfood2.Checked = false;
                    }
                    else
                    {
                        rbfood1.Checked = false;
                        rbfood2.Checked = true;
                    }
                    FoodAllergy.DataSource = dt.Select("allergy_type = 2").CopyToDataTable();
                    FoodAllergy.DataBind();
                    nofood.Style.Add("display", "none");
                }
                else
                {
                    nofood.Style.Add("display", "");
                    FoodAllergy.DataSource = null;
                    FoodAllergy.DataBind();
                }
            }
            else
            {
                nofood.Style.Add("display", "");
                FoodAllergy.DataSource = null;
                FoodAllergy.DataBind();
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "UpdateFoodAllergy", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "UpdateFoodAllergy", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    public void UpdateOtherAllergy(DataTable dt)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            if (dt != null)
            {
                if (dt.Select("allergy_type = 7").Count() > 0)
                {
                    DataTable dtother = dt.Select("allergy_type = 7").CopyToDataTable();
                    if (dtother.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                    {
                        rbother1.Checked = true;
                        rbother2.Checked = false;
                    }
                    else
                    {
                        rbother1.Checked = false;
                        rbother2.Checked = true;
                    }
                    OtherAllergy.DataSource = dt.Select("allergy_type = 7").CopyToDataTable();
                    OtherAllergy.DataBind();
                    noother.Style.Add("display", "none");
                }
                else
                {
                    noother.Style.Add("display", "");
                    OtherAllergy.DataSource = null;
                    OtherAllergy.DataBind();
                }
            }
            else
            {
                noother.Style.Add("display", "");
                OtherAllergy.DataSource = null;
                OtherAllergy.DataBind();
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "UpdateOtherAllergy", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "UpdateOtherAllergy", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    public void UpdateAllergyIsNo(string flag)
    {
        if (flag == "No")
        {
            rballergy1.Checked = true;
            rballergy2.Checked = false;
        }
        else if (flag == "Unknown")
        {
            rballergy1.Checked = false;
            rballergy2.Checked = false;
        }
        else if (flag == "Yes")
        {
            rballergy1.Checked = false;
            rballergy2.Checked = true;
        }
    }

    public void initializevalue(dynamic Jsongetsoap, PatientHeader header, DataTable dtAllergy, List<PatientRoutineMedication> CurrMedication, string guidadditional)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            Session[Helper.SessionDrugPres] = null;
            Session[Helper.SessionCompPres] = null;
            Session[Helper.SessionCompDetailPres] = null;
            Session[Helper.SessionCompPresHdn] = null;
            Session[Helper.SessionCompHeaderHdn] = null;
            Session[Helper.SessionConsumablesList] = null;
            Session[Helper.Sessionadditionalpres] = null;
            Session[Helper.SessionConsumablesListAdd] = null;
            List<Planning> listplanning = new List<Planning>();
            List<Subjective> listsubj = new List<Subjective>();
            listplanning = Jsongetsoap.list.planning;
            listsubj = Jsongetsoap.list.subjective;

            gvw_drug.DataSource = null;
            gvw_drug.DataBind();
            gvwAdditionalDrugs.DataSource = null;
            gvwAdditionalDrugs.DataBind();

            gvw_consumables.DataSource = null;
            gvw_consumables.DataBind();
            gvw_add_cons.DataSource = null;
            gvw_add_cons.DataBind();

            gvw_racikan_header.DataSource = null;
            gvw_racikan_header.DataBind();
            gvw_racikan_header_add.DataSource = null;
            gvw_racikan_header_add.DataBind();


            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Find(y => y.setting_name.ToUpper() == "CHECKED_FORMULARIUM".ToUpper()).setting_value == "TRUE")
            {
                chkformularium.Checked = true;
                hfPayerType.Value = "NORMAL";               
            }   
            else
                hfPayerType.Value = header.Formularium;

            hfguidadditional.Value = guidadditional;
            if (Jsongetsoap.list.take_date != null)
                hftakedate.Value = "1";
            else
                hftakedate.Value = "0";

            if (Jsongetsoap.list.verify_date != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdditional", "ShowAdditional();", true);
                hfverifydate.Value = "1";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideAdditional", "HideAdditional();", true);
                hfverifydate.Value = "0";
            }

            if (Jsongetsoap.list.additional_take_date != null)
            {
                hfadditional_take_date.Value = "1";
            }
            else
            {
                hfadditional_take_date.Value = "0";
            }

            //enable disable soap
            if (Jsongetsoap.list.mr_status == 1) //open status
            {
                divTopLABRAD.Style.Remove("transform");
                divblokLABRAD.Visible = false;

                if (Jsongetsoap.list.lab_process_date != null)
                {
                    divTopLAB.Style.Add("transform", "translate(0,0)");
                    divblokLAB.Visible = true;
                }
                else
                {
                    divTopLAB.Style.Remove("transform");
                    divblokLAB.Visible = false;
                }

                if (Jsongetsoap.list.rad_process_date != null)
                {
                    divTopRAD.Style.Add("transform", "translate(0,0)");
                    divblokRAD.Visible = true;
                }
                else
                {
                    divTopRAD.Style.Remove("transform");
                    divblokRAD.Visible = false;
                }

                //HFflagsoapisdisable_old.Value = "0"; //disable
            }
            else if (Jsongetsoap.list.mr_status == 2) //close status
            {
                divTopLABRAD.Style.Add("transform", "translate(0,0)");
                divblokLABRAD.Visible = true;

                //HFflagsoapisdisable_old.Value = "1"; //disable
            }

            //log.Debug(LogLibrary.Logging("S", "getOrderSetpanel", Helper.GetLoginUser(this.Parent.Page), "(param:" + long.Parse(Helper.GetDoctorID(this.Parent.Page)) + "," + Helper.organizationId + ")"));
            //var soappanellab = clsOrderSet.getOrderSet("0", 0,3);
            //var jsonsoappanellab = JsonConvert.DeserializeObject<ResultOrderSet>(soappanellab.Result.ToString());
            //log.Debug(LogLibrary.Logging("E", "getOrderSetpanel", Helper.GetLoginUser(this.Parent.Page), jsonsoappanellab.ToString()));
            //List<OrderSet> panelset = jsonsoappanellab.list;
            //gvw_panelset.DataSource = panelset;
            //gvw_panelset.DataBind();

            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", Helper.GetDoctorID(this.Parent.Page) },
                { "Organization_ID", Helper.organizationId.ToString() }
            };
            //Log.Debug(LogConfig.LogStart("GetSOAPAdditionalInfo", logParam));
            var soapadditional = clsSOAP.GetSOAPAdditionalInfo(long.Parse(Helper.GetDoctorID(this.Parent.Page)), Helper.organizationId);
            var jsonsoapadditional = JsonConvert.DeserializeObject<ResultSOAPAdditionalInfo>(soapadditional.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("GetSOAPAdditionalInfo", jsonsoapadditional.Status, jsonsoapadditional.Message));

            SOAPAdditionalInfo additional = jsonsoapadditional.list;
            
            //if (Helper.GetFlagCompound(this.Parent.Page) == "FALSE")
            //{
            //    List<OrderSet> listracikan = new List<OrderSet>();
            //    listordersetheader = additional.ordersetdrug;
            //    listracikan = additional.ordersetdrug.FindAll(r => r.set_name.Contains("(R)"));
            //    if (listracikan.Count > 0)
            //    {
            //        listordersetheader = (List<OrderSet>)additional.ordersetdrug.Except(listracikan);
            //    }
            //}
            //else
            //{
            //    listordersetheader = additional.ordersetdrug;
            //}
            listordersetheader = additional.ordersetdrug;
            ordersetdt = Helper.ToDataTable(listordersetheader);

            gvw_orderset.DataSource = ordersetdt;
            gvw_orderset.DataBind();

            listfrequentdrugs = additional.frequentdrug;
            DataTable dtfrequentdrugs = Helper.ToDataTable(listfrequentdrugs);
            //if (dtfrequentdrugs.Select("Formularium = '" + hfPayerType.Value + "'").Count() > 0)
            //{
            //    gvw_frequent_drugs.DataSource = dtfrequentdrugs.Select("Formularium = '" + hfPayerType.Value + "'").CopyToDataTable();
            //    gvw_frequent_drugs.DataBind();
            //}

            gvw_frequent_drugs.DataSource = dtfrequentdrugs;
            gvw_frequent_drugs.DataBind();

            listordersetlab = additional.ordersetlab;
            labsetdt = Helper.ToDataTable(listordersetlab);
            gvw_labset.DataSource = labsetdt;
            gvw_labset.DataBind();

            if (dtAllergy.Select("allergy_type = '1'").Count() > 0)
            {
                DataTable dtdrug = dtAllergy.Select("allergy_type = '1'").CopyToDataTable();
                if (dtdrug.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                {
                    rbdrug1.Checked = true;
                }
                else
                {
                    rbdrug2.Checked = true;
                }

                DrugAllergy.DataSource = dtAllergy.Select("allergy_type = '1'").CopyToDataTable();
                DrugAllergy.DataBind();
                nodrugs.Style.Add("display", "none");
                
            }
            else
            {
                DrugAllergy.DataSource = null;
                DrugAllergy.DataBind();
                nodrugs.Style.Add("display", "");

            }

            if (dtAllergy.Select("allergy_type = '2'").Count() > 0)
            {
                DataTable dtfood = dtAllergy.Select("allergy_type = '2'").CopyToDataTable();
                if (dtfood.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                {
                    rbfood1.Checked = true;
                }
                else
                {
                    rbfood2.Checked = true;
                }

                FoodAllergy.DataSource = dtAllergy.Select("allergy_type = '2'").CopyToDataTable();
                FoodAllergy.DataBind();
                nofood.Style.Add("display", "none");
            }
            else
            {
                FoodAllergy.DataSource = null;
                FoodAllergy.DataBind();
                nofood.Style.Add("display", "");
            }

            if (dtAllergy.Select("allergy_type = '7'").Count() > 0)
            {
                DataTable dtother = dtAllergy.Select("allergy_type = '7'").CopyToDataTable();
                if (dtother.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                {
                    rbother1.Checked = true;
                }
                else
                {
                    rbother2.Checked = true;
                }
                OtherAllergy.DataSource = dtAllergy.Select("allergy_type = '7'").CopyToDataTable();
                OtherAllergy.DataBind();
                noother.Style.Add("display", "none");
            }
            else
            {
                OtherAllergy.DataSource = null;
                OtherAllergy.DataBind();
                noother.Style.Add("display", "");
            }

            if (rbdrug1.Checked == true && rbfood1.Checked == true && rbother1.Checked == true)
            {
                UpdateAllergyIsNo("No");
            }
            else if (rbdrug1.Checked == false && rbfood1.Checked == false && rbother1.Checked == false && rbdrug2.Checked == false && rbfood2.Checked == false && rbother2.Checked == false)
            {
                UpdateAllergyIsNo("Unknown");
            }
            else if (rbdrug2.Checked == true || rbfood2.Checked == true || rbother2.Checked == true)
            {
                UpdateAllergyIsNo("Yes");
            }


            if (CurrMedication.Count() > 0)
            {
                DataTable dtpengobatan = Helper.ToDataTable(CurrMedication);
                dtpengobatan.Columns[1].ColumnName = "medication";
                if (dtpengobatan.Rows[0]["medication"].ToString() == "Tidak Ada Pengobatan")
                {
                    rbpengobatan1.Checked = true;
                }
                else
                {
                    rbpengobatan2.Checked = true;
                }
                RepCurrentMedication.DataSource = dtpengobatan;
                RepCurrentMedication.DataBind();
                noroutine.Style.Add("display", "none");
            }
            else
            {
                RepCurrentMedication.DataSource = null;
                RepCurrentMedication.DataBind();
                noroutine.Style.Add("display", "");
            }


            if (listsubj.Count > 0)
            {
                txtDocNurseNotes.Text = listsubj.Find(y => y.soap_mapping_id == Guid.Parse("bc0c06ae-7085-4e15-8e73-b3bb104a66f1")).remarks;
                txtDocNurseNotes.Rows = txtDocNurseNotes.Text.Split('\n').Length;
                txtNurseNotes.Text = listsubj.Find(y => y.soap_mapping_id == Guid.Parse("19a04437-100e-44ca-a514-42b908e0d657")).remarks;
                txtNurseNotes.Rows = txtNurseNotes.Text.Split('\n').Length;
            }

            if (listplanning.Count > 0)
            {
                foreach (Planning x in listplanning)
                {
                    //if (x.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
                    //{
                    //    txtPlanning.Text = x.remarks;
                    //}
                    if (x.soap_mapping_id == Guid.Parse("5B39A9B4-744B-4AD3-954F-386E32220ABE"))
                    {
                        txtplanningotherLab.Text = x.remarks;
                    }
                    else if (x.soap_mapping_id == Guid.Parse("61764B36-4BF4-4A03-917E-695E6929AFB3"))
                    {
                        txtplanningotherRad.Text = x.remarks;
                    }
                    else if (x.soap_mapping_id == Guid.Parse("E794E3D3-7860-4D52-9166-9A5DF3127E55"))
                    {
                        //REVISI OTHERS DATE
                        if (x.remarks != "")
                        {
                            txtplanningotherLab_FutureOrder.Text = x.remarks;
                            dp_labFutureOrder.Text = DateTime.Parse(x.value).ToString("dd MMM yyyy");
                            divFutureOrder.Style.Add("display", "block");
                        }
                    }
                    else if (x.soap_mapping_id == Guid.Parse("0EE8A241-73A8-49DB-8F4B-8733CDB92C8F"))
                    {
                        //REVISI OTHERS DATE
                        if (x.remarks != "")
                        {
                            txtplanningotherRad_FutureOrder.Text = x.remarks;
                            dp_radFutureOrder.Text = DateTime.Parse(x.value).ToString("dd MMM yyyy");
                            divFutureOrder.Style.Add("display", "block");
                        }
                    }
                    else if (x.soap_mapping_id == Guid.Parse("2df0294d-f94e-4ba4-8ba1-f017bfb55d92"))
                    {
                        txtPresNotes.Text = x.remarks;
                        txtPresNotes.Rows = txtPresNotes.Text.Split('\n').Length;
                    }
                    else if (x.soap_mapping_id == Guid.Parse("5e34ae60-1d72-4efd-8440-c4442515aabe"))
                    {
                        txtAdditionalPresNotes.Text = x.remarks;
                        txtAdditionalPresNotes.Rows = txtAdditionalPresNotes.Text.Split('\n').Length;
                    }
                    else if (x.soap_mapping_id == Guid.Parse("2A20FD9B-0911-4FBB-B8F0-84CE19C743E9"))
                    {
                        txtclinicaldiagnosis.Text = x.remarks;
                        txtclinicaldiagnosis.Rows = txtclinicaldiagnosis.Text.Split('\n').Length;
                    }
                    else if (x.soap_mapping_id == Guid.Parse("5A96F8FA-CCD9-4E75-AD96-148A36F06685"))
                    {
                        txtclinicaldiagnosis_FutureOrder.Text = x.remarks;
                        txtclinicaldiagnosis_FutureOrder.Rows = txtclinicaldiagnosis_FutureOrder.Text.Split('\n').Length;
                    }

                    // planning for diagnostic and procedure
                    else if (x.soap_mapping_id == Guid.Parse("35779378-FC19-41B5-8445-D6C6D358BDE5"))
                    {
                        // other diagnostic
                        txtPlanningOtherDiagnostic.Text = x.remarks;
                    }
                    else if (x.soap_mapping_id == Guid.Parse("E4565CFB-1E9E-47EC-A06E-F21240043289"))
                    {
                        // other procedure
                        txtPlanningOtherProcedure.Text = x.remarks;
                    }
                    else if (x.soap_mapping_id == Guid.Parse("DFA41B67-0EDC-45A8-BD69-5E6883FADEF2"))
                    {
                        // other future order diagnostic 
                        if (x.remarks != "")
                        {
                            txtplanningotherDiagnostic_FutureOrder.Text = x.remarks;
                            dp_diag.Text = DateTime.Parse(x.value).ToString("dd MMM yyyy");
                        }
                    }
                    else if (x.soap_mapping_id == Guid.Parse("68C2FF43-C93B-4FFB-84FF-C7BEBECA72C5"))
                    {
                        // other future order procedure 
                        if (x.remarks != "")
                        {
                            txtplanningotherProcedure_FutureOrder.Text = x.remarks;
                            dp_proc.Text = DateTime.Parse(x.value).ToString("dd MMM yyyy");
                        }
                    }
                }
            }
            txtPharmacistNotes.Text = Jsongetsoap.list.pharmacy_notes;
            txtAdditionalPharmacistNotes.Text = Jsongetsoap.list.additional_pharmacy_notes;

            txtPharmacistNotes.Rows = txtPharmacistNotes.Text.Split('\n').Length;
            txtAdditionalPharmacistNotes.Rows = txtAdditionalPharmacistNotes.Text.Split('\n').Length;

            List<CpoeTrans> listcpoetrans = new List<CpoeTrans>();
            listcpoetrans = Jsongetsoap.list.cpoe_trans;
            if (listcpoetrans.Count > 0)
            {
                var data = (
                                    from a in listcpoetrans
                                    where (a.type == "ClinicLab" || a.type == "CitoLab" || a.type == "MicroLab" || a.type == "PatologiLab" || a.type == "PanelLab" || a.type == "MDCLab") //&& a.isdelete == 0
                                    select a
                               ).Distinct().ToList();
                if (data.Count > 0)
                {
                    if(data.Where(x=> x.IsFutureOrder == false).ToList().Count()> 0)
					{
                    labempty.Style.Add("display", "none");
                    linklabbutton.Style.Add("display", "none");
                    btnEditLab.Visible = true;
                    btnResetLab.Visible = true;
                        HyperLinkSaveAsLab.Style.Add("display", "");
                    }
                    if( data.Where(x=> x.IsFutureOrder == true).ToList().Count() > 0)
					{
                        labempty_FutureOrder.Style.Add("display", "none");
                        linklabbutton_FutureOrder.Style.Add("display", "none");
                        btnEditLab_FutureOrder.Visible = true;
                        btnResetLab_FutureOrder.Visible = true;
                        HyperLinkSaveAsLab_FutureOrder.Style.Add("display", "");
                        divFutureOrder.Style.Add("display", "block");
                    }

                }
                Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = data;

                if (Helper.ToDataTable(data).Select("isdelete = 0").Count() > 0)
                {
                    if(data.Where(x => x.IsFutureOrder == false).ToList().Count() > 0)
					{
                        DataTable dt = Helper.ToDataTable(data).Select("isdelete = 0 AND IsFutureOrder = false").CopyToDataTable();
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else
                {
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
                    if (data.Where(x => x.IsFutureOrder == true).ToList().Count() > 0)
					{
                        DataTable dt = Helper.ToDataTable(data).Select("isdelete = 0 AND IsFutureOrder = true").CopyToDataTable();
                        Repeater1_FutureOrder.DataSource = dt;
                        Repeater1_FutureOrder.DataBind();
                        dp_labFutureOrder.Text = DateTime.Parse(dt.Rows[0]["FutureOrderDate"].ToString()).ToString("dd MMM yyyy");
                    }
                    else
                    {
                        Repeater1_FutureOrder.DataSource = null;
                        Repeater1_FutureOrder.DataBind();
                    }
                }
                else
                {
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();

                    Repeater1_FutureOrder.DataSource = null;
                    Repeater1_FutureOrder.DataBind();
                }


                var datarad = (
                                    from a in listcpoetrans
                                    where (a.type == "MRI1" || a.type == "MRI3" || a.type == "Radiology" || a.type == "USG" || a.type == "CT") //&& a.isdelete == 0
                                    select a
                               ).Distinct().ToList();
                if (datarad.Count > 0)
                {
                    if (datarad.Where(x => x.IsFutureOrder == false).ToList().Count() > 0)
					{
                    radempty.Style.Add("display", "none");
                    linkradbutton.Style.Add("display", "none");
                    btnEditRad.Visible = true;
                    btnResetRad.Visible = true;
                    divcitorad.Visible = true;
                    if (datarad.Where(y => y.iscito == 1 && y.isdelete == 0).Count() > 0)
                    {
                        chkcitorad.Checked = true;
                    }
                    else
                        chkcitorad.Checked = false;
                }
                    if (datarad.Where(x => x.IsFutureOrder == true).ToList().Count() > 0)
					{
                        radempty_FutureOrder.Style.Add("display", "none");
                        linkradbutton_FutureOrder.Style.Add("display", "none");
                        btnEditRad_FutureOrder.Visible = true;
                        btnResetRad_FutureOrder.Visible = true;
                        divcitorad_FutureOrder.Visible = true;
                        if (datarad.Where(y => y.iscito == 1 && y.isdelete == 0).Count() > 0)
                        {
                            chkcitorad.Checked = true;
                        }
                else
                    chkcitorad.Checked = false;
                        divFutureOrder.Style.Add("display", "block");
                    }

                }
				else
				{
                    chkcitorad.Checked = false;
                    chkcitorad_FutureOrder.Checked = false;
				}

                Session[Helper.Sessionradcheck + hfguidadditional.Value] = datarad;

                List<CpoeTrans> listcheckedshow = new List<CpoeTrans>();
                foreach (var list in datarad)
                {
                    CpoeTrans temp = new CpoeTrans();
                    temp.id = list.id;
                    temp.ischeck = list.ischeck;
                    temp.iscito = list.iscito;
                    temp.isdelete = list.isdelete;
                    temp.isnew = list.isnew;
                    temp.issubmit = list.issubmit;
                    if (list.remarks != "")
                        temp.name = list.name + " (" + list.remarks + ")";
                    else
                        temp.name = list.name;

                    temp.type = temp.type;
                    temp.remarks = list.remarks;
                    temp.IsSendHope = list.IsSendHope;
                    temp.IsFutureOrder = list.IsFutureOrder;
                    temp.FutureOrderDate = list.FutureOrderDate;
                    listcheckedshow.Add(temp);
                }

                if (Helper.ToDataTable(listcheckedshow).Select("isdelete = 0").Count() > 0)
                {
                    if (datarad.Where(x => x.IsFutureOrder == false).ToList().Count() > 0)
					{
                        DataTable dtrad = Helper.ToDataTable(listcheckedshow).Select("isdelete = 0 AND IsFutureOrder = false").CopyToDataTable();
                    rptRadiology.DataSource = dtrad;
                    rptRadiology.DataBind();
                }
                else
                {
                    rptRadiology.DataSource = null;
                    rptRadiology.DataBind();
                }

                    if (datarad.Where(x => x.IsFutureOrder == true).ToList().Count() > 0)
                    {
                        DataTable dtrad = Helper.ToDataTable(listcheckedshow).Select("isdelete = 0 AND IsFutureOrder = true").CopyToDataTable();
                        rptRadiology_FutureOrder.DataSource = dtrad;
                        rptRadiology_FutureOrder.DataBind();
                        dp_radFutureOrder.Text = DateTime.Parse(dtrad.Rows[0]["FutureOrderDate"].ToString()).ToString("dd MMM yyyy");
                    }
                    else
                    {
                        rptRadiology_FutureOrder.DataSource = null;
                        rptRadiology_FutureOrder.DataBind();
                    }


                }
                else
                {
                    rptRadiology.DataSource = null;
                    rptRadiology.DataBind();

                    rptRadiology_FutureOrder.DataSource = null;
                    rptRadiology_FutureOrder.DataBind();
                }
            }

            List<CpoeNotes> listcpoenotes = new List<CpoeNotes>();
            listcpoenotes = Jsongetsoap.list.cpoe_notes;
            if (listcpoenotes.Count > 0)
            {
                foreach (CpoeNotes temp in listcpoenotes)
                {
                    if (temp.notes_type == "ClinicLab")
                    {
                        stdclinic.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "MicroLab")
                    {
                        stdmicro.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "CitoLab")
                    {
                        stdcito.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "Xray")
                    {
                        stdxray.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "USG")
                    {
                        stdusg.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "MRI1")
                    {
                        stdmrihalf.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "MRI3")
                    {
                        stdmrifull.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "CT")
                    {
                        stdctrad.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "MDCLab")
                    {
                        stdmdc.InitializeNotes(temp);
                    }
                }
            }

			if (orgsetting.Find(y => y.setting_name.ToUpper() == "USE_FUTUREORDER".ToUpper()).setting_value == "FALSE")
            {
                divFutureOrder.Style.Add("display", "none");
                chk_isLabsetFO.Style.Add("display", "none");
                divBtnFutureOrder.Style.Add("display", "none");
            }

            if (Session[Helper.SessionDrugsConsumables] == null)
            {
                DataTable dt = clsSOAP.getItemConsumables(Helper.organizationId, header.AdmissionTypeId);
                Session[Helper.SessionDrugsConsumables] = dt;
            }

            //dibuka lagi jika menerapkan metode pencarian sebelumnya
            //gvw_cons.DataSource = ((DataTable)Session[Helper.SessionDrugsConsumables]).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable(); 
            //gvw_cons.DataBind();
            //gvw_item_cons_additional.DataSource = ((DataTable)Session[Helper.SessionDrugsConsumables]).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
            //gvw_item_cons_additional.DataBind();

            List<Prescription> listprescription, listpressave = new List<Prescription>();
            List<Prescription> listpresadditional = new List<Prescription>();
            List<CompoundHeaderSoap> listracikanheader = new List<CompoundHeaderSoap>();
            List<CompoundDetailSoap> listracikandetail = new List<CompoundDetailSoap>();

            listprescription = Jsongetsoap.list.prescription;
            listpresadditional = Jsongetsoap.list.additional_prescription;
            listracikanheader = Jsongetsoap.list.compound_header;
            listracikandetail = Jsongetsoap.list.compound_detail;

            foreach (var templist in listprescription)
            {
                
                //string a = templist.quantity.ToString().Substring(0, 3);
                string[] tempqty = templist.quantity.ToString().Split('.');

                if (tempqty.Length > 1)
                {
                    if (tempqty[1].Length == 3)
                    {
                        if (tempqty[1] == "000")
                        {
                            templist.quantity = decimal.Parse(tempqty[0]).ToString();
                        }
                        else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                        {
                            templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                        }
                        else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                        {
                            templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                        }
                    }
                }

                
                string[] tempdose = templist.dosage_id.ToString().Split('.');
                if (tempdose.Length > 1)
                {
                    if (tempdose[1].Length == 3)
                    {
                        if (tempdose[1] == "000")
                        {
                            templist.dosage_id = decimal.Parse(tempdose[0]).ToString();
                        }
                        else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                        {
                            templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                        }
                        else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                        {
                            templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                        }
                    }
                }
                
            }

            foreach (var templist in listpresadditional)
            {
                //string a = templist.quantity.ToString().Substring(0, 3);
                string[] tempqty = templist.quantity.ToString().Split('.');
                if (tempqty.Count() > 1)
                {
                    if (tempqty[1].Length == 3)
                    {
                        if (tempqty[1] == "000")
                        {
                            templist.quantity = decimal.Parse(tempqty[0]).ToString();
                        }
                        else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                        {
                            templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                        }
                        else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                        {
                            templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                        }
                    }
                }

                string[] tempdose = templist.dosage_id.ToString().Split('.');
                if (tempdose.Count() > 1)
                {
                    if (tempdose[1].Length == 3)
                    {
                        if (tempdose[1] == "000")
                        {
                            templist.dosage_id = decimal.Parse(tempdose[0]).ToString();
                        }
                        else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                        {
                            templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                        }
                        else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                        {
                            templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                        }
                    }
                }
            }

            foreach (var templist in listracikanheader)
            {
                //string a = templist.quantity.ToString().Substring(0, 3);
                string[] tempqty = templist.quantity.ToString().Split('.');
                if (tempqty.Count() > 1)
                {
                    if (tempqty[1].Length == 3)
                    {
                        if (tempqty[1] == "000")
                        {
                            templist.quantity = decimal.Parse(tempqty[0]).ToString();
                        }
                        else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                        {
                            templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                        }
                        else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                        {
                            templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                        }
                    }
                }

                string[] tempdose = templist.dose.ToString().Split('.');

                if (tempdose.Count() > 1)
                {
                    if (tempdose[1].Length == 3)
                    {
                        if (tempdose[1] == "000")
                        {
                            templist.dose = decimal.Parse(tempdose[0]).ToString();
                        }
                        else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                        {
                            templist.dose = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                        }
                        else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                        {
                            templist.dose = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                        }
                    }
                }
            }

            foreach (var templist in listracikandetail)
            {
                //string a = templist.quantity.ToString().Substring(0, 3);
                string[] tempqty = templist.quantity.ToString().Split('.');
                if (tempqty.Count() > 1)
                {
                    if (tempqty[1].Length == 3)
                    {
                        if (tempqty[1] == "000")
                        {
                            templist.quantity = decimal.Parse(tempqty[0]).ToString();
                        }
                        else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                        {
                            templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                        }
                        else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                        {
                            templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                        }
                    }
                }

                string[] tempdose = templist.dose.ToString().Split('.');

                if (tempdose.Count() > 1)
                {
                    if (tempdose[1].Length == 3)
                    {
                        if (tempdose[1] == "000")
                        {
                            templist.dose = decimal.Parse(tempdose[0]).ToString();
                        }
                        else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                        {
                            templist.dose = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                        }
                        else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                        {
                            templist.dose = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                        }
                    }
                }
            }

            if (listpresadditional.Count > 0)
            {
                if (Helper.ToDataTable(listpresadditional).Select("compound_id = '00000000-0000-0000-0000-000000000000' and is_delete = 0 and is_consumables = 0").Count() > 0)
                {
                    DataTable dtpresdrugadditional = Helper.ToDataTable(listpresadditional).Select("compound_id = '00000000-0000-0000-0000-000000000000' and is_delete = 0 and is_consumables = 0").CopyToDataTable();
                    Session[Helper.Sessionadditionalpres + hfguidadditional.Value] = dtpresdrugadditional;
                    gvwAdditionalDrugs.DataSource = dtpresdrugadditional;
                    gvwAdditionalDrugs.DataBind();
                }
                else
                {
                    Session[Helper.Sessionadditionalpres + hfguidadditional.Value] = null;
                    gvwAdditionalDrugs.DataSource = null;
                    gvwAdditionalDrugs.DataBind();
                }

                if (Helper.ToDataTable(listpresadditional).Select("is_consumables = 1").Count() > 0)
                {
                    DataTable dtconsumables = Helper.ToDataTable(listpresadditional).Select("is_consumables = 1").CopyToDataTable();
                    Session[Helper.SessionConsumablesListAdd + hfguidadditional.Value] = dtconsumables;
                    gvw_add_cons.DataSource = dtconsumables;
                    gvw_add_cons.DataBind();
                }
                else
                {
                    Session[Helper.SessionConsumablesListAdd + hfguidadditional.Value] = null;
                    gvw_add_cons.DataSource = null;
                    gvw_add_cons.DataBind();
                }
            }

            if (listprescription.Count > 0)
            {
                if (Helper.ToDataTable(listprescription).Select("compound_id = '00000000-0000-0000-0000-000000000000' and is_delete = 0 and is_consumables = 0").Count() > 0)
                {
                    DataTable dtpresdrug = Helper.ToDataTable(listprescription).Select("compound_id = '00000000-0000-0000-0000-000000000000' and is_delete = 0 and is_consumables = 0").CopyToDataTable();
                    Session[Helper.SessionDrugPres + hfguidadditional.Value] = dtpresdrug;
                    gvw_drug.DataSource = dtpresdrug;
                    gvw_drug.DataBind();

                    HyperLinkSaveOrderSet.Style.Add("display", "");
                }
                else
                {
                    Session[Helper.SessionDrugPres + hfguidadditional.Value] = null;
                    gvw_drug.DataSource = null;
                    gvw_drug.DataBind();

                    HyperLinkSaveOrderSet.Style.Add("display", "none");
                }


                //if (Helper.ToDataTable(listprescription).Select("item_id = 0").Count() > 0)
                //{
                //    DataTable dtcompdrug = Helper.ToDataTable(listprescription).Select("item_id = 0").CopyToDataTable();
                //    Session[Helper.SessionCompPres + hfguidadditional.Value] = dtcompdrug;
                //    gvw_comp.DataSource = dtcompdrug;
                //    gvw_comp.DataBind();
                //    //divcompound.Visible = true;
                //}
                //else
                //{
                //    Session[Helper.SessionCompPres + hfguidadditional.Value] = null;
                //    gvw_comp.DataSource = null;
                //    gvw_comp.DataBind();
                //    //divcompound.Visible = false;
                //}

                if (Helper.ToDataTable(listprescription).Select("is_consumables = 1").Count() > 0)
                {
                    DataTable dtconsumables = Helper.ToDataTable(listprescription).Select("is_consumables = 1").CopyToDataTable();
                    Session[Helper.SessionConsumablesList + hfguidadditional.Value] = dtconsumables;
                    gvw_consumables.DataSource = dtconsumables;
                    gvw_consumables.DataBind();
                }
                else

                {
                    Session[Helper.SessionConsumablesList + hfguidadditional.Value] = null;
                    gvw_consumables.DataSource = null;
                    gvw_consumables.DataBind();
                }

                //if (Helper.ToDataTable(listprescription).Select("compound_id <> '00000000-0000-0000-0000-000000000000' and item_id <> 0").Count() > 0)
                //{
                //    DataTable dtcompdetail = Helper.ToDataTable(listprescription).Select("compound_id <> '00000000-0000-0000-0000-000000000000' and item_id <> 0").CopyToDataTable();
                //    Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = dtcompdetail;
                //    gvw_compdetail.DataSource = dtcompdetail;
                //    gvw_compdetail.DataBind();
                //}
                //else
                //{
                //    Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = null;
                //    gvw_compdetail.DataSource = null;
                //    gvw_compdetail.DataBind();
                //}
            }

            if (listracikanheader.Count > 0)
            {
                //compound
                if (Helper.ToDataTable(listracikanheader.Where(y => y.is_additional == false).ToList()).Rows.Count > 0)
                {
                    DataTable dtracikan = Helper.ToDataTable(listracikanheader.Where(y => y.is_additional == false).ToList());
                    Session[Helper.SessionRacikanHeader + hfguidadditional.Value] = dtracikan;

                    if (listracikandetail.Count > 0)
                    {
                        DataTable dtracikandetail = Helper.ToDataTable(listracikandetail.Where(y => y.is_additional == false).ToList());
                        Session[Helper.SessionRacikanDetail + hfguidadditional.Value] = dtracikandetail;
                    }

                    gvw_racikan_header.DataSource = dtracikan;
                    gvw_racikan_header.DataBind(); 
                }
                else
                {
                    Session[Helper.SessionRacikanHeader + hfguidadditional.Value] = null;
                    gvw_racikan_header.DataSource = null;
                    gvw_racikan_header.DataBind();
                }

                //additional compound
                if (Helper.ToDataTable(listracikanheader.Where(y => y.is_additional == true).ToList()).Rows.Count > 0)
                {
                    DataTable dtracikan_add = Helper.ToDataTable(listracikanheader.Where(y => y.is_additional == true).ToList());
                    Session[Helper.SessionRacikanHeaderAdd + hfguidadditional.Value] = dtracikan_add;

                    if (listracikandetail.Where(y => y.is_additional == true).ToList().Count > 0)
                    {
                        DataTable dtracikandetail_add = Helper.ToDataTable(listracikandetail.Where(y => y.is_additional == true).ToList());
                        Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value] = dtracikandetail_add;
                    }

                    gvw_racikan_header_add.DataSource = dtracikan_add;
                    gvw_racikan_header_add.DataBind();
                }
                else
                {
                    Session[Helper.SessionRacikanHeaderAdd + hfguidadditional.Value] = null;
                    gvw_racikan_header_add.DataSource = null;
                    gvw_racikan_header_add.DataBind();
                }
            }


            #region setting
            if (orgsetting.Find(y => y.setting_name.ToUpper() == "EDIT_CPOE".ToUpper()).setting_value == "FALSE")
            {
                if (listcpoetrans.Count > 0)
                {
                    var data = (
                                from a in listcpoetrans
                                where (a.type == "ClinicLab" || a.type == "CitoLab" || a.type == "MicroLab" || a.type == "PatologiLab" || a.type == "PanelLab" || a.type == "MDCLab") //&& a.isdelete == 0
                                select a
                           ).Distinct().ToList();

                    if (data.Count > 0)
                    {
                        if (Jsongetsoap.list.save_mode == 1)
                        {
                            btnResetLab.Visible = false;
                            btnEditLab.Visible = false;
                            DisableRowList(6);
                        }
                    }

                    var datarad = (
                                from a in listcpoetrans
                                where (a.type == "MRI1" || a.type == "MRI3" || a.type == "Radiology" || a.type == "USG" || a.type == "CT") //&& a.isdelete == 0
                                select a
                           ).Distinct().ToList();
                    if (datarad.Count > 0)
                    {
                        if (Jsongetsoap.list.save_mode == 1)
                        {
                            btnEditRad.Visible = false;
                            btnResetRad.Visible = false;
                            chkcitorad.Enabled = false;
                        }
                    }
                }
            }

            if (orgsetting.Find(y => y.setting_name.ToUpper() == "EDIT_PRESCRIPTION".ToUpper()).setting_value == "FALSE")
            {
                if (listprescription.Count > 0)
                {
                    if (Jsongetsoap.list.save_mode == 1)
                    {
                        //chkformularium.Enabled = false;
                        txtItemAdd.Enabled = false;
                        txtItemAdd_AC.Enabled = false;
                        txtPresNotes.Enabled = false;
                        DisableRowList(1);
                        DisableRowList(5);
                        DisableRowList(7);
                        DisableRowList(8);

                        BtnCheckDrugInteraction.Enabled = false;
                    }
                }
            }

            #endregion

            //Clear Button Future Order Date
            ClearButtonFutureOrderDate();
            // Set Diagnostic and procedure 
            SetDiagProc();


            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "initializevalue", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "initializevalue", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    public void initializevaluecopy(dynamic soap, PatientHeader header, DataTable dtAllergy, List<CurrentMedication> CurrMedication,string guidadditional)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>();

            Session[Helper.SessionDrugPres] = null;
            Session[Helper.SessionCompPres] = null;
            Session[Helper.SessionCompDetailPres] = null;
            Session[Helper.SessionCompPresHdn] = null;
            Session[Helper.SessionCompHeaderHdn] = null;
            Session[Helper.SessionConsumablesList] = null;
            Session[Helper.Sessionadditionalpres] = null;
            Session[Helper.SessionConsumablesListAdd] = null;
            List<Planning> listplanning = new List<Planning>();
            List<Subjective> listsubj = new List<Subjective>();
            listplanning = soap.planning;
            listsubj = soap.subjective;

            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Find(y => y.setting_name.ToUpper() == "CHECKED_FORMULARIUM".ToUpper()).setting_value == "TRUE")
            {
                chkformularium.Checked = true;
                hfPayerType.Value = "NORMAL";
            }
            else
                hfPayerType.Value = header.Formularium;

            hfguidadditional.Value = guidadditional;
            if (soap.take_date != null)
                hftakedate.Value = "1";
            else
                hftakedate.Value = "0";

            logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", "0" },
                { "Is_Compound", "0" },
                { "Type", "3" }
            };
           // Log.Debug(LogConfig.LogStart("getOrderSet", logParam));
            var soappanellab = clsOrderSet.getOrderSet("0", 0, 3);
            var jsonsoappanellab = JsonConvert.DeserializeObject<ResultOrderSet>(soappanellab.Result.ToString());
           // Log.Debug(LogConfig.LogEnd("getOrderSet", jsonsoappanellab.Status, jsonsoappanellab.Message));
            
            List<OrderSet> panelset = jsonsoappanellab.list;
            gvw_panelset.DataSource = panelset;
            gvw_panelset.DataBind();

            logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", Helper.GetDoctorID(this.Parent.Page) },
                { "Organization_ID", Helper.organizationId.ToString() }
            };
           // Log.Debug(LogConfig.LogStart("GetSOAPAdditionalInfo", logParam));
            var soapadditional = clsSOAP.GetSOAPAdditionalInfo(long.Parse(Helper.GetDoctorID(this.Parent.Page)), Helper.organizationId);
            var jsonsoapadditional = JsonConvert.DeserializeObject<ResultSOAPAdditionalInfo>(soapadditional.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("GetSOAPAdditionalInfo", jsonsoapadditional.Status, jsonsoapadditional.Message));

            SOAPAdditionalInfo additional = jsonsoapadditional.list;

            //if (Helper.GetFlagCompound(this.Parent.Page) == "FALSE")
            //{
            //    List<OrderSet> listracikan = new List<OrderSet>();
            //    listordersetheader = additional.ordersetdrug;
            //    listracikan = additional.ordersetdrug.FindAll(r => r.set_name.Contains("(R)"));
            //    if (listracikan.Count > 0)
            //    {
            //        listordersetheader = (List<OrderSet>)additional.ordersetdrug.Except(listracikan);
            //    }
            //}
            //else
            //{
            //    listordersetheader = additional.ordersetdrug;
            //}
            listordersetheader = additional.ordersetdrug;
            ordersetdt = Helper.ToDataTable(listordersetheader);

            gvw_orderset.DataSource = ordersetdt;
            gvw_orderset.DataBind();

            listfrequentdrugs = additional.frequentdrug;
            DataTable dtfrequentdrugs = Helper.ToDataTable(listfrequentdrugs);
            //if (dtfrequentdrugs.Select("Formularium = '" + hfPayerType.Value + "'").Count() > 0)
            //{
            //    gvw_frequent_drugs.DataSource = dtfrequentdrugs.Select("Formularium = '" + hfPayerType.Value + "'").CopyToDataTable();
            //    gvw_frequent_drugs.DataBind();
            //}

            gvw_frequent_drugs.DataSource = dtfrequentdrugs;
            gvw_frequent_drugs.DataBind();

            listordersetlab = additional.ordersetlab;
            labsetdt = Helper.ToDataTable(listordersetlab);
            gvw_labset.DataSource = labsetdt;
            gvw_labset.DataBind();

            if (dtAllergy.Select("allergy_type = 'Drug'").Count() > 0)
            {
                DataTable dtdrug = dtAllergy.Select("allergy_type = 'Drug'").CopyToDataTable();
                if (dtdrug.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                {
                    rbdrug1.Checked = true;
                }
                else
                {
                    rbdrug2.Checked = true;
                }

                DrugAllergy.DataSource = dtAllergy.Select("allergy_type = 'Drug'").CopyToDataTable();
                DrugAllergy.DataBind();
                nodrugs.Style.Add("display", "none");
                
            }
            else
            {
                DrugAllergy.DataSource = null;
                DrugAllergy.DataBind();
                nodrugs.Style.Add("display", "");
            }

            if (dtAllergy.Select("allergy_type = 'Food'").Count() > 0)
            {
                DataTable dtfood = dtAllergy.Select("allergy_type = 'Food'").CopyToDataTable();
                if (dtfood.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                {
                    rbfood1.Checked = true;
                }
                else
                {
                    rbfood2.Checked = true;
                }
                FoodAllergy.DataSource = dtAllergy.Select("allergy_type = 'Food'").CopyToDataTable();
                FoodAllergy.DataBind();
                nofood.Style.Add("display", "none");
            }
            else
            {
                FoodAllergy.DataSource = null;
                FoodAllergy.DataBind();
                nofood.Style.Add("display", "");
            }

            if (dtAllergy.Select("allergy_type = 'Other'").Count() > 0)
            {
                DataTable dtother = dtAllergy.Select("allergy_type = 'Other'").CopyToDataTable();
                if (dtother.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                {
                    rbother1.Checked = true;
                }
                else
                {
                    rbother2.Checked = true;
                }
                OtherAllergy.DataSource = dtAllergy.Select("allergy_type = 'Other'").CopyToDataTable();
                OtherAllergy.DataBind();
                noother.Style.Add("display", "none");
            }
            else
            {
                OtherAllergy.DataSource = null;
                OtherAllergy.DataBind();
                noother.Style.Add("display", "");
            }

            if (rbdrug1.Checked == true && rbfood1.Checked == true && rbother1.Checked == true)
            {
                UpdateAllergyIsNo("No");
            }
            else if (rbdrug1.Checked == false && rbfood1.Checked == false && rbother1.Checked == false && rbdrug2.Checked == false && rbfood2.Checked == false && rbother2.Checked == false)
            {
                UpdateAllergyIsNo("Unknown");
            }
            else if (rbdrug2.Checked == true || rbfood2.Checked == true || rbother2.Checked == true)
            {
                UpdateAllergyIsNo("Yes");
            }

            if (CurrMedication.Count() > 0)
            {
                DataTable dtpengobatan = Helper.ToDataTable(CurrMedication);
                dtpengobatan.Columns[1].ColumnName = "medication";
                if (dtpengobatan.Rows[0]["medication"].ToString() == "Tidak Ada Pengobatan")
                {
                    rbpengobatan1.Checked = true;
                }
                else
                {
                    rbpengobatan2.Checked = true;
                }
                RepCurrentMedication.DataSource = dtpengobatan;
                RepCurrentMedication.DataBind();
                noroutine.Style.Add("display", "none");
            }
            else
            {
                RepCurrentMedication.DataSource = null;
                RepCurrentMedication.DataBind();
                noroutine.Style.Add("display", "none");
            }

            if (listsubj.Count > 0)
            {
                txtDocNurseNotes.Text = listsubj.Find(y => y.soap_mapping_id == Guid.Parse("bc0c06ae-7085-4e15-8e73-b3bb104a66f1")).remarks;
                txtDocNurseNotes.Rows = txtDocNurseNotes.Text.Split('\n').Length;
                txtNurseNotes.Text = listsubj.Find(y => y.soap_mapping_id == Guid.Parse("19a04437-100e-44ca-a514-42b908e0d657")).remarks;
                txtNurseNotes.Rows = txtNurseNotes.Text.Split('\n').Length;
            }

            if (listplanning.Count > 0)
            {
                foreach (Planning x in listplanning)
                {
                    //if (x.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
                    //{
                    //    txtPlanning.Text = x.remarks;
                    //}
                    if (x.soap_mapping_id == Guid.Parse("5B39A9B4-744B-4AD3-954F-386E32220ABE"))
                    {
                        txtplanningotherLab.Text = x.remarks;
                    }
                    else if (x.soap_mapping_id == Guid.Parse("61764B36-4BF4-4A03-917E-695E6929AFB3"))
                    {
                        txtplanningotherRad.Text = x.remarks;
                    }
                    else if (x.soap_mapping_id == Guid.Parse("E794E3D3-7860-4D52-9166-9A5DF3127E55"))
                    {
                        //REVISI OTHERS DATE
                        if (x.remarks != "")
                        {
                            txtplanningotherLab_FutureOrder.Text = x.remarks;
                            dp_labFutureOrder.Text = DateTime.Parse(x.value).ToString("dd MMM yyyy");
                            divFutureOrder.Style.Add("display", "block");
                        }
                    }
                    else if (x.soap_mapping_id == Guid.Parse("0EE8A241-73A8-49DB-8F4B-8733CDB92C8F"))
                    {
                        //REVISI OTHERS DATE
                        if (x.remarks != "")
                        {
                            txtplanningotherRad_FutureOrder.Text = x.remarks;
                            dp_radFutureOrder.Text = DateTime.Parse(x.value).ToString("dd MMM yyyy");
                            divFutureOrder.Style.Add("display", "block");
                        }
                    }
                    else if (x.soap_mapping_id == Guid.Parse("2df0294d-f94e-4ba4-8ba1-f017bfb55d92"))
                    {
                        txtPresNotes.Text = x.remarks;
                    }
                    else if (x.soap_mapping_id == Guid.Parse("2A20FD9B-0911-4FBB-B8F0-84CE19C743E9"))
                    {
                        txtclinicaldiagnosis.Text = x.remarks;
                    }
                }
            }
            txtPharmacistNotes.Text = soap.pharmacy_notes;

            txtPharmacistNotes.Rows = txtPharmacistNotes.Text.Split('\n').Length;

            List<CpoeTrans> listcpoetrans = new List<CpoeTrans>();
            listcpoetrans = soap.cpoe_trans;
            if (listcpoetrans.Count > 0)
            {
                var data = (
                                    from a in listcpoetrans
                                    where (a.type == "ClinicLab" || a.type == "CitoLab" || a.type == "MicroLab" || a.type == "PatologiLab" || a.type == "PanelLab" || a.type == "MDCLab") && a.isdelete == 0
                                    select a
                               ).Distinct().ToList();

                if (data.Count > 0)
                {
                    if (data.Where(x => x.IsFutureOrder == false).ToList().Count() > 0)
                    {
                    labempty.Style.Add("display", "none");
                    linklabbutton.Style.Add("display", "none");
                    btnEditLab.Visible = true;
                    btnResetLab.Visible = true;
                        HyperLinkSaveAsLab.Style.Add("display", "");
                }
                    if (data.Where(x => x.IsFutureOrder == true).ToList().Count() > 0)
                {
                        labempty_FutureOrder.Style.Add("display", "none");
                        linklabbutton_FutureOrder.Style.Add("display", "none");
                        btnEditLab_FutureOrder.Visible = true;
                        btnResetLab_FutureOrder.Visible = true;
                        HyperLinkSaveAsLab_FutureOrder.Style.Add("display", "");
                        divFutureOrder.Style.Add("display", "block");
                }

                }
                if (Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] != null)
                {
                    List<CpoeTrans> listtempcpoetrans = ((List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value]).Where(y => y.isdelete == 0).ToList();
                    List<CpoeTrans> listcpoecopy = new List<CpoeTrans>();
                    if (listtempcpoetrans.Count > 0)
                    {
                        if (data.Count > 0)
                        {
                            foreach (var x in listtempcpoetrans)
                            {
                                if (data.Where(y => y.id == x.id).Count() == 0)
                                {
                                    listcpoecopy.Add(x);
                                }
                            }
                            data.AddRange(listcpoecopy);
                        }
                        else
                        {
                            data = listtempcpoetrans;
                        }
                    }
                }
                Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = data;

                if (Helper.ToDataTable(data).Select("isdelete = 0").Count() > 0)
                {
                    if (data.Where(x => x.IsFutureOrder == false).ToList().Count() > 0)
                    {
                        DataTable dt = Helper.ToDataTable(data).Select("isdelete = 0 AND IsFutureOrder = false").CopyToDataTable();
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else
                {
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
                    if (data.Where(x => x.IsFutureOrder == true).ToList().Count() > 0)
                    {
                        DataTable dt = Helper.ToDataTable(data).Select("isdelete = 0 AND IsFutureOrder = true").CopyToDataTable();
                        Repeater1_FutureOrder.DataSource = dt;
                        Repeater1_FutureOrder.DataBind();
                    }
                    else
                    {
                        Repeater1_FutureOrder.DataSource = null;
                        Repeater1_FutureOrder.DataBind();
                    }
                }
                else
                {
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();

                    Repeater1_FutureOrder.DataSource = null;
                    Repeater1_FutureOrder.DataBind();
                }


                var datarad = (
                                    from a in listcpoetrans
                                    where (a.type == "MRI1" || a.type == "MRI3" || a.type == "Radiology" || a.type == "USG" || a.type == "CT") //&& a.isdelete == 0
                                    select a
                               ).Distinct().ToList();
                if (datarad.Count > 0)
                {
                    if (datarad.Where(x => x.IsFutureOrder == false).ToList().Count() > 0)
                    {
                    radempty.Style.Add("display", "none");
                    linkradbutton.Style.Add("display", "none");
                    btnEditRad.Visible = true;
                    btnResetRad.Visible = true;
                    divcitorad.Visible = true;
                    if (datarad.Where(y => y.iscito == 1 && y.isdelete == 0).Count() > 0)
                    {
                        chkcitorad.Checked = true;
                    }
                    else
                        chkcitorad.Checked = false;
                    }
                    if (datarad.Where(x => x.IsFutureOrder == true).ToList().Count() > 0)
                    {
                        radempty_FutureOrder.Style.Add("display", "none");
                        linkradbutton_FutureOrder.Style.Add("display", "none");
                        btnEditRad_FutureOrder.Visible = true;
                        btnResetRad_FutureOrder.Visible = true;
                        divcitorad_FutureOrder.Visible = true;
                        if (datarad.Where(y => y.iscito == 1 && y.isdelete == 0).Count() > 0)
                        {
                            chkcitorad.Checked = true;
                        }
                        else
                            chkcitorad.Checked = false;
                        divFutureOrder.Style.Add("display", "block");
                    }

                }
                else
                {
                    radempty.Style.Add("display", "");
                    linkradbutton.Style.Add("display", "");
                    btnEditRad.Visible = false;
                    btnResetRad.Visible = false;
                    divcitorad.Visible = false;
                    chkcitorad.Checked = false;

                    radempty_FutureOrder.Style.Add("display", "");
                    linkradbutton_FutureOrder.Style.Add("display", "");
                    btnEditRad_FutureOrder.Visible = false;
                    btnResetRad_FutureOrder.Visible = false;
                    divcitorad_FutureOrder.Visible = false;
                    chkcitorad_FutureOrder.Checked = false;
                }

                if (Session[Helper.Sessionradcheck + hfguidadditional.Value] != null)
                {
                    List<CpoeTrans> listtempcpoetransrad = ((List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value]).Where(y => y.isdelete == 0).ToList();
                    List<CpoeTrans> listcpoecopyrad = new List<CpoeTrans>();
                    if (listtempcpoetransrad.Count > 0)
                    {
                        if (datarad.Count > 0)
                        {
                            foreach (var x in listtempcpoetransrad)
                            {
                                if (datarad.Where(y => y.id == x.id).Count() == 0)
                                {
                                    listcpoecopyrad.Add(x);
                                }
                            }
                            datarad.AddRange(listcpoecopyrad);
                        }
                        else
                        {
                            datarad = listtempcpoetransrad;
                        }
                    }
                }
                Session[Helper.Sessionradcheck + hfguidadditional.Value] = datarad;

                List<CpoeTrans> listcheckedshow = new List<CpoeTrans>();
                foreach (var list in datarad)
                {
                    CpoeTrans temp = new CpoeTrans();
                    temp.id = list.id;
                    temp.ischeck = list.ischeck;
                    temp.iscito = list.iscito;
                    temp.isdelete = list.isdelete;
                    temp.isnew = list.isnew;
                    temp.issubmit = list.issubmit;
                    if (list.remarks != "")
                        temp.name = list.name + " (" + list.remarks + ")";
                    else
                        temp.name = list.name;

                    temp.type = temp.type;
                    temp.remarks = list.remarks;
                    temp.IsSendHope = list.IsSendHope;
                    temp.IsFutureOrder = list.IsFutureOrder;
                    temp.FutureOrderDate = list.FutureOrderDate;
                    listcheckedshow.Add(temp);
                }

                if (Helper.ToDataTable(listcheckedshow).Select("isdelete = 0").Count() > 0)
                {
                    if (datarad.Where(x => x.IsFutureOrder == false).ToList().Count() > 0)
                    {
                        DataTable dtrad = Helper.ToDataTable(listcheckedshow).Select("isdelete = 0 AND IsFutureOrder = false").CopyToDataTable();
                    rptRadiology.DataSource = dtrad;
                    rptRadiology.DataBind();
                }
                else
                {
                    rptRadiology.DataSource = null;
                    rptRadiology.DataBind();
                }

                    if (datarad.Where(x => x.IsFutureOrder == true).ToList().Count() > 0)
                    {
                        DataTable dtrad = Helper.ToDataTable(listcheckedshow).Select("isdelete = 0 AND IsFutureOrder = true").CopyToDataTable();
                        rptRadiology_FutureOrder.DataSource = dtrad;
                        rptRadiology_FutureOrder.DataBind();
                    }
                    else
                    {
                        rptRadiology_FutureOrder.DataSource = null;
                        rptRadiology_FutureOrder.DataBind();
                    }


                }
                else
                {
                    rptRadiology.DataSource = null;
                    rptRadiology.DataBind();

                    rptRadiology_FutureOrder.DataSource = null;
                    rptRadiology_FutureOrder.DataBind();
                }
            }

            List<CpoeNotes> listcpoenotes = new List<CpoeNotes>();
            listcpoenotes = soap.cpoe_notes;
            if (listcpoenotes.Count > 0)
            {
                foreach (CpoeNotes temp in listcpoenotes)
                {
                    if (temp.notes_type == "ClinicLab")
                    {
                        stdclinic.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "MicroLab")
                    {
                        stdmicro.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "CitoLab")
                    {
                        stdcito.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "Xray")
                    {
                        stdxray.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "USG")
                    {
                        stdusg.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "MRI1")
                    {
                        stdmrihalf.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "MRI3")
                    {
                        stdmrifull.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "CT")
                    {
                        stdctrad.InitializeNotes(temp);
                    }
                    else if (temp.notes_type == "MDCLab")
                    {
                        stdmdc.InitializeNotes(temp);
                    }
                }
            }

            if (Session[Helper.SessionDrugsConsumables] == null)
            {
                DataTable dt = clsSOAP.getItemConsumables(Helper.organizationId, header.AdmissionTypeId);
                Session[Helper.SessionDrugsConsumables] = dt;
            }

            //dibuka lagi jika menerapkan metode pencarian sebelumnya
            //gvw_cons.DataSource = ((DataTable)Session[Helper.SessionDrugsConsumables]).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
            //gvw_cons.DataBind();
            //gvw_item_cons_additional.DataSource = ((DataTable)Session[Helper.SessionDrugsConsumables]).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
            //gvw_item_cons_additional.DataBind();

            List<Prescription> listprescription, listpressave = new List<Prescription>();
            List<Prescription> tempexistdrugs = GetRowList(1);
            List<Prescription> tempexistconsumables = GetRowList(5);

            List<Prescription> tempdrugscopy = new List<Prescription>();


            listprescription = soap.prescription;
            if (tempexistdrugs.Count() > 0)
            {
                foreach (var x in tempexistdrugs)
                {
                    if (listprescription.Where(y => y.item_id == x.item_id).Count() == 0)
                    {
                        tempdrugscopy.Add(x);
                    }
                }
            }

            if (tempexistconsumables.Count() > 0)
            {
                foreach (var x in tempexistconsumables)
                {
                    if (listprescription.Where(y => y.item_id == x.item_id).Count() == 0)
                    {
                        tempdrugscopy.Add(x);
                    }
                }
            }
            listprescription.AddRange(tempdrugscopy);
            foreach (var templist in listprescription)
            {
                string[] tempqty = templist.quantity.ToString().Split('.');
                if (tempqty.Count() > 1)
                {
                    if (tempqty[1].Length == 3)
                    {
                        if (tempqty[1] == "000")
                        {
                            templist.quantity = decimal.Parse(tempqty[0]).ToString();
                        }
                        else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                        {
                            templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                        }
                        else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                        {
                            templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                        }
                    }
                }
                


                string[] tempdose = templist.dosage_id.ToString().Split('.');
                if (tempdose.Count() > 1)
                {
                    if (tempdose[1].Length == 3)
                    {
                        if (tempdose[1] == "000")
                        {
                            templist.dosage_id = decimal.Parse(tempdose[0]).ToString();
                        }
                        else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                        {
                            templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                        }
                        else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                        {
                            templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                        }
                    }
                }
            }
            
            if (listprescription.Count > 0)
            {
                if (Helper.ToDataTable(listprescription).Select("compound_id = '00000000-0000-0000-0000-000000000000' and is_delete = 0 and is_consumables = 0").Count() > 0)
                {
                    DataTable dtpresdrug = Helper.ToDataTable(listprescription).Select("compound_id = '00000000-0000-0000-0000-000000000000' and is_delete = 0 and is_consumables = 0").CopyToDataTable();
                    Session[Helper.SessionDrugPres + hfguidadditional.Value] = dtpresdrug;
                    gvw_drug.DataSource = dtpresdrug;
                    gvw_drug.DataBind();

                    HyperLinkSaveOrderSet.Style.Add("display", "");
                }
                else
                {
                    Session[Helper.SessionDrugPres + hfguidadditional.Value] = null;
                    gvw_drug.DataSource = null;
                    gvw_drug.DataBind();

                    HyperLinkSaveOrderSet.Style.Add("display", "none");
                }


                //if (Helper.ToDataTable(listprescription).Select("item_id = 0").Count() > 0)
                //{
                //    DataTable dtcompdrug = Helper.ToDataTable(listprescription).Select("item_id = 0").CopyToDataTable();
                //    Session[Helper.SessionCompPres + hfguidadditional.Value] = dtcompdrug;
                //    gvw_comp.DataSource = dtcompdrug;
                //    gvw_comp.DataBind();
                //}
                //else
                //{
                //    Session[Helper.SessionCompPres + hfguidadditional.Value] = null;
                //    gvw_comp.DataSource = null;
                //    gvw_comp.DataBind();
                //}

                if (Helper.ToDataTable(listprescription).Select("is_consumables = 1").Count() > 0)
                {
                    DataTable dtconsumables = Helper.ToDataTable(listprescription).Select("is_consumables = 1").CopyToDataTable();
                    Session[Helper.SessionConsumablesList + hfguidadditional.Value] = dtconsumables;
                    gvw_consumables.DataSource = dtconsumables;
                    gvw_consumables.DataBind();
                }
                else

                {
                    Session[Helper.SessionConsumablesList + hfguidadditional.Value] = null;
                    gvw_consumables.DataSource = null;
                    gvw_consumables.DataBind();
                }

                //if (Helper.ToDataTable(listprescription).Select("compound_id <> '00000000-0000-0000-0000-000000000000' and item_id <> 0").Count() > 0)
                //{
                //    DataTable dtcompdetail = Helper.ToDataTable(listprescription).Select("compound_id <> '00000000-0000-0000-0000-000000000000' and item_id <> 0").CopyToDataTable();
                //    Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = dtcompdetail;
                //    gvw_compdetail.DataSource = dtcompdetail;
                //    gvw_compdetail.DataBind();
                //}
                //else
                //{
                //    Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = null;
                //    gvw_compdetail.DataSource = null;
                //    gvw_compdetail.DataBind();
                //}

            }
            else
            {
                Session[Helper.SessionDrugPres + hfguidadditional.Value] = null;
                gvw_drug.DataSource = null;
                gvw_drug.DataBind();

                //Session[Helper.SessionCompPres + hfguidadditional.Value] = null;
                //gvw_comp.DataSource = null;
                //gvw_comp.DataBind();

                Session[Helper.SessionConsumablesList + hfguidadditional.Value] = null;
                gvw_consumables.DataSource = null;
                gvw_consumables.DataBind();

                //Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = null;
                //gvw_compdetail.DataSource = null;
                //gvw_compdetail.DataBind();
            }


            #region setting
            if (orgsetting.Find(y => y.setting_name.ToUpper() == "EDIT_CPOE".ToUpper()).setting_value == "FALSE")
            {
                if (listcpoetrans.Count > 0)
                {
                    var data = (
                                from a in listcpoetrans
                                where (a.type == "ClinicLab" || a.type == "CitoLab" || a.type == "MicroLab" || a.type == "PatologiLab" || a.type == "PanelLab" || a.type == "MDCLab") && a.isdelete == 0
                                select a
                           ).Distinct().ToList();

                    if (data.Count > 0)
                    {
                        if (soap.save_mode == 1)
                        {
                            btnResetLab.Visible = false;
                            btnEditLab.Visible = false;
                            DisableRowList(6);
                        }
                    }

                    var datarad = (
                                from a in listcpoetrans
                                where (a.type == "MRI1" || a.type == "MRI3" || a.type == "Radiology" || a.type == "USG" || a.type == "CT") //&& a.isdelete == 0
                                select a
                           ).Distinct().ToList();
                    if (datarad.Count > 0)
                    {
                        if (soap.save_mode == 1)
                        {
                            btnEditRad.Visible = false;
                            btnResetRad.Visible = false;
                            chkcitorad.Enabled = false;
                        }
                    }
                }
            }

            if (orgsetting.Find(y => y.setting_name.ToUpper() == "EDIT_PRESCRIPTION".ToUpper()).setting_value == "FALSE")
            {
                if (listprescription.Count > 0)
                {
                    if (soap.save_mode == 1)
                    {
                        //chkformularium.Enabled = false;
                        txtItemAdd.Enabled = false;
                        txtItemAdd_AC.Enabled = false;
                        txtPresNotes.Enabled = false;
                        DisableRowList(1);
                        DisableRowList(5);
                        DisableRowList(7);
                        DisableRowList(8);

                        BtnCheckDrugInteraction.Enabled = false;
                    }
                }
            }

            #endregion


            List<CpoeMapping> getMapJson = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
            //StdClinicControl.GetMappingClinicLab(getMapJson, hfguidadditional.Value);
            stdclinic.GetMappingClinicLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdmicro.GetMappingMicroLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdcito.GetMappingCitoLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdanatomi.GetMappingAnatomiLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdpanel.GetMappingPanelLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdxray.GetMappingClinicLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdusg.GetMappingClinicLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdctrad.GetMappingClinicLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdmrihalf.GetMappingClinicLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdmrifull.GetMappingClinicLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdmdc.GetMappingMDC(getMapJson, hfguidadditional.Value, HF_FlagFutureOrder.Value);

            //Clear Button Future Order Date
            ClearButtonFutureOrderDate();

            UpdatePanelDivLab.Update();
            UP_ContainerLab.Update();
            UpdatePanelDivRad.Update();
            UP_ContainerRad.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "initializevaluecopy", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "initializevaluecopy", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void drugsadditional_data_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField idItem = (HiddenField)e.Row.FindControl("item_id");

                DataTable dt = ((DataTable)Session[Helper.Sessionadditionalpres + hfguidadditional.Value]).Select("item_id = " + idItem.Value).CopyToDataTable();

                DataTable dtfreqtemp = (DataTable)Session[Helper.SessionFrequency];
                DataTable dtroutetemp = (DataTable)Session[Helper.SessionRoute];
                DataTable dtdosetemp = (DataTable)Session[Helper.Sessiondosage];
                if (dtfreqtemp == null)
                {
                    var frequencyData = clsOrderSet.getFrequency();
                    var Jsonfrequency = JsonConvert.DeserializeObject<ResultFrequency>(frequencyData.Result.ToString());
                    listfrequency = Jsonfrequency.list;
                    frequencydt = Helper.ToDataTable(listfrequency);
                    DataView dv = frequencydt.DefaultView;
                    dv.Sort = "name asc";
                    frequencydt = dv.ToTable();
                    Session[Helper.SessionFrequency] = frequencydt;
                }

                if (dtroutetemp == null)
                {
                    var administrationRouteData = clsOrderSet.getAdministrationRoute();
                    var JsonadministrationRoute = JsonConvert.DeserializeObject<ResultAdministrationRoute>(administrationRouteData.Result.ToString());
                    listadministrationRoute = JsonadministrationRoute.list;
                    routedt = Helper.ToDataTable(listadministrationRoute);
                    DataView dvroute = routedt.DefaultView;
                    dvroute.Sort = "name asc";
                    routedt = dvroute.ToTable();
                    Session[Helper.SessionRoute] = routedt;
                }
                if (dtdosetemp == null)
                {
                    var dosage = clsSOAP.getDosage();
                    var Jsondosage = JsonConvert.DeserializeObject<ResultDose>(dosage.Result.ToString());
                    List<Dose> listdosage = Jsondosage.list;
                    DataTable dosagedt = Helper.ToDataTable(listdosage);
                    DataView dvdose = dosagedt.DefaultView;
                    dvdose.Sort = "name asc";
                    dosagedt = dvdose.ToTable();
                    Session[Helper.Sessiondosage] = dosagedt;
                }


                DropDownList ddlfrequency_drugs = (DropDownList)e.Row.FindControl("frequency_code");
                ddlfrequency_drugs.DataSource = (DataTable)Session[Helper.SessionFrequency];
                ddlfrequency_drugs.DataTextField = "name";
                ddlfrequency_drugs.DataValueField = "administrationFrequencyId";
                ddlfrequency_drugs.DataBind();
                ddlfrequency_drugs.SelectedValue = dt.Rows[0]["frequency_id"].ToString();
                // ddlfrequency_drugs.ToolTip = dt.Rows[0]["frequency_code"].ToString();
                ddlfrequency_drugs.Items.Insert(0, new ListItem("- SELECT -", "0"));

                DropDownList ddlroute_drugs = (DropDownList)e.Row.FindControl("administrationRouteCode");
                ddlroute_drugs.DataSource = (DataTable)Session[Helper.SessionRoute];
                ddlroute_drugs.DataTextField = "name";
                ddlroute_drugs.DataValueField = "administration_route_id";
                ddlroute_drugs.DataBind();
                ddlroute_drugs.SelectedValue = dt.Rows[0]["administration_route_id"].ToString();
                //ddlroute_drugs.ToolTip = dt.Rows[0]["AdministrationRouteCode"].ToString();
                ddlroute_drugs.Items.Insert(0, new ListItem("- SELECT -", "0"));

                DropDownList ddldosage_drugs = (DropDownList)e.Row.FindControl("doseuom");
                ddldosage_drugs.DataSource = (DataTable)Session[Helper.Sessiondosage];
                ddldosage_drugs.DataTextField = "name";
                ddldosage_drugs.DataValueField = "doseUomId";
                ddldosage_drugs.DataBind();
                ddldosage_drugs.SelectedValue = dt.Rows[0]["dose_uom_id"].ToString();
                //ddlroute_drugs.ToolTip = dt.Rows[0]["AdministrationRouteCode"].ToString();
                ddldosage_drugs.Items.Insert(0, new ListItem("- SELECT -", "0"));

            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "drugsadditional_data_RowDataBound", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "drugsadditional_data_RowDataBound", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

    }

    protected void drugs_data_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField idItem = (HiddenField)e.Row.FindControl("item_id");

                DataTable dt = ((DataTable)Session[Helper.SessionDrugPres + hfguidadditional.Value]).Select("item_id = " + idItem.Value).CopyToDataTable();

                DataTable dtfreqtemp = (DataTable)Session[Helper.SessionFrequency];
                DataTable dtroutetemp = (DataTable)Session[Helper.SessionRoute];
                DataTable dtdosetemp = (DataTable)Session[Helper.Sessiondosage];
                if (dtfreqtemp == null)
                {
                    var frequencyData = clsOrderSet.getFrequency();
                    var Jsonfrequency = JsonConvert.DeserializeObject<ResultFrequency>(frequencyData.Result.ToString());
                    listfrequency = Jsonfrequency.list;
                    frequencydt = Helper.ToDataTable(listfrequency);
                    Session[Helper.SessionFrequency] = frequencydt;
                }

                if (dtroutetemp == null)
                {
                    var administrationRouteData = clsOrderSet.getAdministrationRoute();
                    var JsonadministrationRoute = JsonConvert.DeserializeObject<ResultAdministrationRoute>(administrationRouteData.Result.ToString());
                    listadministrationRoute = JsonadministrationRoute.list;
                    routedt = Helper.ToDataTable(listadministrationRoute);
                    Session[Helper.SessionRoute] = routedt;
                }
                if (dtdosetemp == null)
                {
                    var dosage = clsSOAP.getDosage();
                    var Jsondosage = JsonConvert.DeserializeObject<ResultDose>(dosage.Result.ToString());
                    List<Dose> listdosage = Jsondosage.list;
                    DataTable dosagedt = Helper.ToDataTable(listdosage);
                    Session[Helper.Sessiondosage] = dosagedt;
                }


                DropDownList ddlfrequency_drugs = (DropDownList)e.Row.FindControl("frequency_code");
                ddlfrequency_drugs.DataSource = (DataTable)Session[Helper.SessionFrequency];
                ddlfrequency_drugs.DataTextField = "name";
                ddlfrequency_drugs.DataValueField = "administrationFrequencyId";
                ddlfrequency_drugs.DataBind();
                ddlfrequency_drugs.SelectedValue = dt.Rows[0]["frequency_id"].ToString();
                // ddlfrequency_drugs.ToolTip = dt.Rows[0]["frequency_code"].ToString();
                ddlfrequency_drugs.Items.Insert(0, new ListItem("- SELECT -", "0"));

                DropDownList ddlroute_drugs = (DropDownList)e.Row.FindControl("administrationRouteCode");
                ddlroute_drugs.DataSource = (DataTable)Session[Helper.SessionRoute];
                ddlroute_drugs.DataTextField = "name";
                ddlroute_drugs.DataValueField = "administration_route_id";
                ddlroute_drugs.DataBind();
                ddlroute_drugs.SelectedValue = dt.Rows[0]["administration_route_id"].ToString();
                //ddlroute_drugs.ToolTip = dt.Rows[0]["AdministrationRouteCode"].ToString();
                ddlroute_drugs.Items.Insert(0, new ListItem("- SELECT -", "0"));

                DropDownList ddldosage_drugs = (DropDownList)e.Row.FindControl("doseuom");
                ddldosage_drugs.DataSource = (DataTable)Session[Helper.Sessiondosage];
                ddldosage_drugs.DataTextField = "name";
                ddldosage_drugs.DataValueField = "doseUomId";
                ddldosage_drugs.DataBind();
                ddldosage_drugs.SelectedValue = dt.Rows[0]["dose_uom_id"].ToString();
                //ddlroute_drugs.ToolTip = dt.Rows[0]["AdministrationRouteCode"].ToString();
                ddldosage_drugs.Items.Insert(0, new ListItem("- SELECT -", "0"));

            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "drugs_data_RowDataBound", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "drugs_data_RowDataBound", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
    }

    protected void comp_data_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField compound_id = (HiddenField)e.Row.FindControl("compound_comp_id");

                DataTable dtfreqtemp = (DataTable)Session[Helper.SessionFrequency];
                DataTable dtroutetemp = (DataTable)Session[Helper.SessionRoute];
                if (dtfreqtemp == null)
                {
                    var frequencyData = clsOrderSet.getFrequency();
                    var Jsonfrequency = JsonConvert.DeserializeObject<ResultFrequency>(frequencyData.Result.ToString());
                    listfrequency = Jsonfrequency.list;
                    frequencydt = Helper.ToDataTable(listfrequency);
                    Session[Helper.SessionFrequency] = frequencydt;
                }

                if (dtroutetemp == null)
                {
                    var administrationRouteData = clsOrderSet.getAdministrationRoute();
                    var JsonadministrationRoute = JsonConvert.DeserializeObject<ResultAdministrationRoute>(administrationRouteData.Result.ToString());
                    listadministrationRoute = JsonadministrationRoute.list;
                    routedt = Helper.ToDataTable(listadministrationRoute);
                    Session[Helper.SessionRoute] = routedt;
                }

                DataTable dt = ((DataTable)Session[Helper.SessionCompPres + hfguidadditional.Value]).Select("compound_id = '" + compound_id.Value + "'").CopyToDataTable();

                DropDownList ddlfrequency_drugs = (DropDownList)e.Row.FindControl("frequency_comp_code");
                ddlfrequency_drugs.DataSource = (DataTable)Session[Helper.SessionFrequency];
                ddlfrequency_drugs.DataTextField = "name";
                ddlfrequency_drugs.DataValueField = "administrationFrequencyId";
                ddlfrequency_drugs.DataBind();
                ddlfrequency_drugs.SelectedValue = dt.Rows[0]["frequency_id"].ToString();
                // ddlfrequency_drugs.ToolTip = dt.Rows[0]["frequency_code"].ToString();
                ddlfrequency_drugs.Items.Insert(0, new ListItem("-", "0"));

                DropDownList ddlroute_drugs = (DropDownList)e.Row.FindControl("administrationRouteCode_comp");
                ddlroute_drugs.DataSource = (DataTable)Session[Helper.SessionRoute];
                ddlroute_drugs.DataTextField = "name";
                ddlroute_drugs.DataValueField = "administration_route_id";
                ddlroute_drugs.DataBind();
                ddlroute_drugs.SelectedValue = dt.Rows[0]["administration_route_id"].ToString();
                //ddlroute_drugs.ToolTip = dt.Rows[0]["AdministrationRouteCode"].ToString();
                ddlroute_drugs.Items.Insert(0, new ListItem("-", "0"));
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "comp_data_RowDataBound", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "comp_data_RowDataBound", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

    }

    bool MyParentMethod(object sender)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            //StdClinicControl.checkIfExist += new Form_CPOE_Control_Template_StdClinicLabPage.customHandler(MyParentMethod);
            listchecked = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];
            if (listchecked != null)
            {
                labempty.Style.Add("display", "none");
                linklabbutton.Style.Add("display", "none");
                btnEditLab.Visible = true;
                btnResetLab.Visible = true;
                if (Helper.ToDataTable(listchecked).Select("isdelete = 0").Count() > 0)
                {
                    DataTable dt = Helper.ToDataTable(listchecked).Select("isdelete = 0").CopyToDataTable();
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();

                    HyperLinkSaveAsLab.Style.Add("display","");
                }
                else
                {
                    labempty.Style.Add("display", "");
                    linklabbutton.Style.Add("display", "");
                    btnEditLab.Visible = false;
                    btnResetLab.Visible = false;
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();

                    HyperLinkSaveAsLab.Style.Add("display", "none");
                }

            }
            else
            {
                labempty.Style.Add("display", "");
                linklabbutton.Style.Add("display", "");
                btnEditLab.Visible = false;
                btnResetLab.Visible = false;
                Repeater1.DataSource = null;
                Repeater1.DataBind();

                HyperLinkSaveAsLab.Style.Add("display", "none");
            }
            List<CpoeMapping> getMapJson = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
            stdclinic.GetMappingClinicLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdcito.GetMappingCitoLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrder.Value);

            UpdatePanelDivLab.Update();
            UP_ContainerLab.Update();

            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "loading", "hideloading();", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "MyParentMethod", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "MyParentMethod", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());

        return true;

    }

    bool MyParentMethodRadiology(object sender)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            //StdClinicControl.checkIfExist += new Form_CPOE_Control_Template_StdClinicLabPage.customHandler(MyParentMethod);
            listchecked = (List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value];

            if (listchecked != null)
            {
                List<CpoeTrans> listcheckedshow = new List<CpoeTrans>();
                foreach (var list in listchecked)
                {
                    CpoeTrans temp = new CpoeTrans();
                    temp.id = list.id;
                    temp.ischeck = list.ischeck;
                    temp.iscito = list.iscito;
                    temp.isdelete = list.isdelete;
                    temp.isnew = list.isnew;
                    temp.issubmit = list.issubmit;
                    if (list.remarks != "")
                        temp.name = list.name + " (" + list.remarks + ")";
                    else
                        temp.name = list.name;

                    temp.type = list.type;
                    temp.remarks = list.remarks;
                    listcheckedshow.Add(temp);
                }

                radempty.Style.Add("display", "none");
                linkradbutton.Style.Add("display", "none");
                btnEditRad.Visible = true;
                btnResetRad.Visible = true;
                divcitorad.Visible = true;
                if (Helper.ToDataTable(listcheckedshow).Select("isdelete = 0").Count() > 0)
                {
                    DataTable dt = Helper.ToDataTable(listcheckedshow).Select("isdelete = 0").CopyToDataTable();
                    rptRadiology.DataSource = dt;
                    rptRadiology.DataBind();
                    //if (listcheckedshow.Where(y => y.iscito == 1 && y.isdelete == 0).Count() > 0)
                    //{
                    //    chkcitorad.Checked = true;
                    //}
                    //else
                    //    chkcitorad.Checked = false;
                }
                else
                {
                    radempty.Style.Add("display", "");
                    linkradbutton.Style.Add("display", "");
                    btnEditRad.Visible = false;
                    btnResetRad.Visible = false;
                    divcitorad.Visible = false;
                    chkcitorad.Checked = false;
                    rptRadiology.DataSource = null;
                    rptRadiology.DataBind();
                }

            }
            else
            {
                radempty.Style.Add("display", "");
                linkradbutton.Style.Add("display", "");
                btnEditRad.Visible = false;
                btnResetRad.Visible = false;
                divcitorad.Visible = false;
                chkcitorad.Checked = false;
                rptRadiology.DataSource = null;
                rptRadiology.DataBind();
            }

            UpdatePanelDivRad.Update();
            UP_ContainerRad.Update();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "loading", "hideloading();", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "MyParentMethodRadiology", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "MyParentMethodRadiology", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());

        return true;

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            lblModalTitle.Text = "Laboratory";           
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnSubmit_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnSubmit_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //upModal.Update();
        //GetMappingClinicLab(2, "cliniclab");
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnRadiology_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            LblRadiology.Text = "Radiology";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalRad", "$('#modalRad').modal();", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnRadiology_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnRadiology_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);

            
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void formularium_onclik(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Find(y => y.setting_name.ToUpper() == "CHECKED_FORMULARIUM".ToUpper()).setting_value == "FALSE")
            {
                DataTable dtItem = (DataTable)Session[Helper.SessionItemDrugPres];
                if (chkformularium.Checked)
                {
                    chkformularium.Checked = true; hfchkformularium.Value = "true";

                    //dibuka lagi jika menerapkan metode pencarian sebelumnya
                    //gvw_data.DataSource = dtItem.Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
                    //gvw_data.DataBind();

                }
                else
                {
                    chkformularium.Checked = false; hfchkformularium.Value = "false";
                    //if (hfPayerType.Value != "NORMAL")
                    //{
                    //chkformularium.Checked = true;
                    //dibuka lagi jika menerapkan metode pencarian sebelumnya
                    //gvw_data.DataSource = (dtItem.Select("Formularium = '" + hfPayerType.Value + "'").CopyToDataTable()).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
                    //gvw_data.DataBind();
                    //}
                }
                upError.Update();
                txtSearchItem.Text = "";
            }
            UP_SearchDrug.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "formularium_onclik", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "formularium_onclik", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnFind_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        try
        {
            upError.Update();
            string a = txtSearchItem.Text;
            gvw_data.DataSource = null;
            if (a == "")
            {
                //listitem = (List<Item>)ViewState["item"];
                DataTable dtItem = (DataTable)Session[Helper.SessionItemDrugPres];
                if (chkformularium.Checked)
                {
                    chkformularium.Checked = true;
                    gvw_data.DataSource = dtItem.Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
                    gvw_data.DataBind();
                }
                else
                {
                    gvw_data.DataSource = (dtItem.Select("Formularium = '" + hfPayerType.Value + "'").CopyToDataTable()).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
                    gvw_data.DataBind();
                }
            }
            else
            {
                try
                {
                    DataTable dtItem = (DataTable)Session[Helper.SessionItemDrugPres];
                    if (chkformularium.Checked)
                    {
                        chkformularium.Checked = true;
                        gvw_data.DataSource = dtItem.Select("salesItemName like '%" + a + "%' or activeIngredientsName like '%" + a + "%'").CopyToDataTable();
                        gvw_data.DataBind();
                    }
                    else
                    {
                        gvw_data.DataSource = (dtItem.Select("Formularium = '" + hfPayerType.Value + "' and (salesItemName like '%" + a + "%' or activeIngredientsName like '%" + a + "%')").CopyToDataTable()).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
                        gvw_data.DataBind();
                    }
                    //gvw_data.DataSource = dtItem.Select("salesItemName like '%" + a + "%' or activeIngredientsName like '%" + a + "%'").CopyToDataTable();
                }
                catch
                {
                    gvw_data.DataSource = null;
                }
                gvw_data.DataBind();
            }
            //txtSearchItem.Focus();
            //upError.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnFind_click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnFind_click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnFindConsAdd_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            upConsAdd.Update();
            string keyword = txtSearchAddItemCons.Text;
            if (keyword == "")
            {
                //listitem = (List<Item>)ViewState["item"];
                DataTable dtItem = (DataTable)Session[Helper.SessionDrugsConsumables];
                gvw_item_cons_additional.DataSource = dtItem.Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable(); ;
                gvw_item_cons_additional.DataBind();
            }
            else
            {

                try
                {
                    DataTable dtItem = (DataTable)Session[Helper.SessionDrugsConsumables];
                    gvw_item_cons_additional.DataSource = dtItem.Select("salesItemName like '%" + keyword + "%' or activeIngredientsName like '%" + keyword + "%'").CopyToDataTable();
                }
                catch
                {
                    gvw_item_cons_additional.DataSource = null;
                }
                gvw_item_cons_additional.DataBind();
            }
            //txtSearchAddItemCons.Focus();
            upConsAdd.Update();
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnFindConsAdd_click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnFindConsAdd_click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnFindCons_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            upConsItem.Update();
            string keyword = txtSearchItemcons.Text;
            if (keyword == "")
            {
                //listitem = (List<Item>)ViewState["item"];
                DataTable dtItem = (DataTable)Session[Helper.SessionDrugsConsumables];
                gvw_cons.DataSource = dtItem.Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable(); ;
                gvw_cons.DataBind();
            }
            else
            {

                try
                {
                    DataTable dtItem = (DataTable)Session[Helper.SessionDrugsConsumables];
                    gvw_cons.DataSource = dtItem.Select("salesItemName like '%" + keyword + "%' or activeIngredientsName like '%" + keyword + "%'").CopyToDataTable();
                }
                catch
                {
                    gvw_cons.DataSource = null;
                }
                gvw_cons.DataBind();
            }
            //txtSearchItemcons.Focus();
            upConsItem.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnFindCons_click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnFindCons_click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnFindAdditionalDrugs_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            upAdditionalItem.Update();
            string a = txtSearchItemAddDrugs.Text;
            gvw_add_drugs.DataSource = null;
            if (a == "")
            {
                //listitem = (List<Item>)ViewState["item"];
                DataTable dtItem = (DataTable)Session[Helper.SessionItemDrugPres];
                if (chkformularium.Checked)
                {
                    chkformularium.Checked = true;
                    gvw_add_drugs.DataSource = dtItem.Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
                }
                else
                {
                    gvw_add_drugs.DataSource = (dtItem.Select("Formularium = '" + hfPayerType.Value + "'").CopyToDataTable()).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
                    gvw_add_drugs.DataBind();
                }
            }
            else
            {
                try
                {
                    DataTable dtItem = (DataTable)Session[Helper.SessionItemDrugPres];
                    if (chkformularium.Checked)
                    {
                        chkformularium.Checked = true;
                        gvw_add_drugs.DataSource = dtItem.Select("salesItemName like '%" + a + "%' or activeIngredientsName like '%" + a + "%'").CopyToDataTable();
                        gvw_add_drugs.DataBind();
                    }
                    else
                    {
                        gvw_add_drugs.DataSource = (dtItem.Select("Formularium = '" + hfPayerType.Value + "' and (salesItemName like '%" + a + "%' or activeIngredientsName like '%" + a + "%')").CopyToDataTable()).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
                        gvw_add_drugs.DataBind();
                    }
                    //gvw_data.DataSource = dtItem.Select("salesItemName like '%" + a + "%' or activeIngredientsName like '%" + a + "%'").CopyToDataTable();
                }
                catch(Exception ex)
                {
                    string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnFindAdditionalDrugs_click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
                    gvw_add_drugs.DataSource = null;

                }
                gvw_add_drugs.DataBind();

                string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnFindAdditionalDrugs_click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
            }
            //txtSearchItemAddDrugs.Focus();
            //upError.Update();
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnFindAdditionalDrugs_click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    //protected void btnFindcomp_click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        log.Info(LogLibrary.Logging("S", "btnFindcomp_click", Helper.GetLoginUser(this.Parent.Page), ""));
    //        string a = find_detail.Text;
    //        if (a == "")
    //        {
    //            //listitem = (List<Item>)ViewState["item"];
    //            DataTable dtItem = (DataTable)Session[Helper.SessionItemDrugPres];
    //            gvw_item_detail.DataSource = dtItem.Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable(); ;
    //            gvw_item_detail.DataBind();
    //        }
    //        else
    //        {

    //            try
    //            {
    //                DataTable dtItem = (DataTable)Session[Helper.SessionItemDrugPres];
    //                gvw_item_detail.DataSource = dtItem.Select("salesItemName like '%" + a + "%' or activeIngredientsName like '%" + a + "%'").CopyToDataTable();
    //            }
    //            catch
    //            {
    //                gvw_item_detail.DataSource = null;
    //            }
    //            gvw_item_detail.DataBind();
    //            //listitem = (List<Item>)ViewState["item"];
    //            //gvw_item_detail.DataBind();
    //        }
    //        log.Info(LogLibrary.Logging("E", "btnFindcomp_click", Helper.GetLoginUser(this.Parent.Page), "Finish btnFindcomp_click"));
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error(LogLibrary.Error("btnFindcomp_click", Helper.GetLoginUser(this.Parent.Page), ex.InnerException.Message));
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
    //    }
    //}

    public bool isMandatory()
    {
        if (txtclinicaldiagnosis.Text == "")
        {
            if (HFmandatoryCD.Value == "*")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "setmandatory", "isMandatoryField(); setFocusMandatory();", true);
                return false;
            }
        }

        return true;
    }

    public bool CheckQuantityPrescription(int type)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            string mandatory = "";
            if (orgsetting.Where(y => y.setting_name.ToLower() == "MANDATORY_PRESCRIPTION".ToLower()).Count() > 0)
            {
                mandatory = orgsetting.Find(y => y.setting_name.ToUpper() == "MANDATORY_PRESCRIPTION".ToUpper()).setting_value.ToString();
            }
                
            if (type == 1)
            {
                foreach (GridViewRow rows in gvw_drug.Rows)
                {
                    TextBox quantity = (TextBox)rows.FindControl("quantity");
                    TextBox dose = (TextBox)rows.FindControl("dosage_id");
                    DropDownList doseuom = (DropDownList)rows.FindControl("doseuom");
                    DropDownList frequency_code = (DropDownList)rows.FindControl("frequency_code");
                    DropDownList administrationRouteCode = (DropDownList)rows.FindControl("administrationRouteCode");
                    TextBox remarks = (TextBox)rows.FindControl("remarks");
                    CheckBox is_dosetext = (CheckBox)rows.FindControl("is_dosetext");
                    TextBox dosetext = (TextBox)rows.FindControl("dosetext");

                    if (mandatory == "FALSE")
                    {
                        if (quantity.Text == "")
                            return false;
                        else if (double.Parse(quantity.Text) < 0.000001)
                            return false;
                    }
                    if (is_dosetext.Checked == false)
                    {
                        if (mandatory.Contains("DOSE"))
                        {
                            if (quantity.Text == "")
                                return false;
                            else if (double.Parse(quantity.Text) < 0.000001)
                                return false;
                            else if (dose.Text == "")
                                return false;
                            else if (double.Parse(dose.Text) < 0.000001)
                                return false;
                        }
                        if (mandatory.Contains("DOSE_UOM"))
                        {
                            if (quantity.Text == "")
                                return false;
                            else if (double.Parse(quantity.Text) < 0.000001)
                                return false;
                            else if (doseuom.SelectedIndex == 0)
                                return false;
                        }
                    }
                    else if (is_dosetext.Checked == true)
                    {
                        if (mandatory.Contains("DOSE_TEXT"))
                        {
                            if (quantity.Text == "")
                                return false;
                            else if (double.Parse(quantity.Text) < 0.000001)
                                return false;
                            else if (dosetext.Text == "")
                                return false;
                        }
                    }
                    if (mandatory.Contains("FREQUENCY"))
                    {
                        if (quantity.Text == "")
                            return false;
                        else if (double.Parse(quantity.Text) < 0.000001)
                            return false;
                        else if (frequency_code.SelectedIndex == 0)
                            return false;
                    }
                    if (mandatory.Contains("ROUTE"))
                    {
                        if (quantity.Text == "")
                            return false;
                        else if (double.Parse(quantity.Text) < 0.000001)
                            return false;
                        else if (administrationRouteCode.SelectedIndex == 0)
                            return false;
                    }
                    if (mandatory.Contains("INSTRUCTION"))
                    {
                        if (quantity.Text == "")
                            return false;
                        else if (double.Parse(quantity.Text) < 0.000001)
                            return false;
                        else if (remarks.Text == "")
                            return false;
                    }
                    
                }
            }
            //else if (type == 2)
            //{
            //    foreach (GridViewRow rows in gvw_comp.Rows)
            //    {
            //        TextBox quantity = (TextBox)rows.FindControl("quantity_comp");

            //        //string a = quantity.Text;
            //        if (quantity.Text == "")
            //        {
            //            return false;
            //        }
            //        else if (double.Parse(quantity.Text) < 0.000001)
            //        {
            //            return false;
            //        }
            //    }
            //}
            //else if (type == 3)
            //{
            //    foreach (GridViewRow rows in gvw_compdetail.Rows)
            //    {
            //        Label quantity = (Label)rows.FindControl("quantity_compdtlhdn");
            //        //string a = quantity.Text;
            //        if (quantity.Text == "")
            //        {
            //            return false;
            //        }
            //        else if (double.Parse(quantity.Text) < 0.000001)
            //        {
            //            return false;
            //        }
            //    }
            //}
            else if (type == 4)
            {
                foreach (GridViewRow rows in gvw_consumables.Rows)
                {
                    TextBox quantity = (TextBox)rows.FindControl("quantity_cons");
                    TextBox remarks_cons = (TextBox)rows.FindControl("remarks_cons");
                    if (mandatory == "FALSE")
                    {
                        if (quantity.Text == "")
                        {
                            return false;
                        }
                        else if (double.Parse(quantity.Text) < 0.000001)
                        {
                            return false;
                        }
                    }
                    if (mandatory.Contains("INSTRUCTION"))
                    {
                        if (quantity.Text == "")
                        {
                            return false;
                        }
                        else if (double.Parse(quantity.Text) < 0.000001)
                        {
                            return false;
                        }
                        else if (remarks_cons.Text == "")
                            return false;
                    }
                    else
                    {
                        if (quantity.Text == "")
                        {
                            return false;
                        }
                        else if (double.Parse(quantity.Text) < 0.000001)
                        {
                            return false;
                        }
                    }
                    //string a = quantity.Text;
                    
                }
            }
            else if (type == 5)
            {
                foreach (GridViewRow rows in gvwAdditionalDrugs.Rows)
                {
                    TextBox quantity = (TextBox)rows.FindControl("quantity");
                    TextBox dose = (TextBox)rows.FindControl("dosage_id");
                    DropDownList doseuom = (DropDownList)rows.FindControl("doseuom");
                    DropDownList frequency_code = (DropDownList)rows.FindControl("frequency_code");
                    DropDownList administrationRouteCode = (DropDownList)rows.FindControl("administrationRouteCode");
                    TextBox remarks = (TextBox)rows.FindControl("remarks");
                    CheckBox is_dosetext = (CheckBox)rows.FindControl("is_dosetext");
                    TextBox dosetext = (TextBox)rows.FindControl("dosetext");

                    if (mandatory == "FALSE")
                    {
                        if (quantity.Text == "")
                            return false;
                        else if (double.Parse(quantity.Text) < 0.000001)
                            return false;
                    }
                    if (is_dosetext.Checked == false)
                    {
                        if (mandatory.Contains("DOSE"))
                        {
                            if (quantity.Text == "")
                                return false;
                            else if (double.Parse(quantity.Text) < 0.000001)
                                return false;
                            else if (dose.Text == "")
                                return false;
                            else if (double.Parse(dose.Text) < 0.000001)
                                return false;
                        }
                        if (mandatory.Contains("DOSE_UOM"))
                        {
                            if (quantity.Text == "")
                                return false;
                            else if (double.Parse(quantity.Text) < 0.000001)
                                return false;
                            else if (doseuom.SelectedIndex == 0)
                                return false;
                        }
                    }
                    else if (is_dosetext.Checked == true)
                    {
                        if (mandatory.Contains("DOSE_TEXT"))
                        {
                            if (quantity.Text == "")
                                return false;
                            else if (double.Parse(quantity.Text) < 0.000001)
                                return false;
                            else if (dosetext.Text == "")
                                return false;
                        }
                    }
                    if (mandatory.Contains("FREQUENCY"))
                    {
                        if (quantity.Text == "")
                            return false;
                        else if (double.Parse(quantity.Text) < 0.000001)
                            return false;
                        else if (frequency_code.SelectedIndex == 0)
                            return false;
                    }
                    if (mandatory.Contains("ROUTE"))
                    {
                        if (quantity.Text == "")
                            return false;
                        else if (double.Parse(quantity.Text) < 0.000001)
                            return false;
                        else if (administrationRouteCode.SelectedIndex == 0)
                            return false;
                    }
                    if (mandatory.Contains("INSTRUCTION"))
                    {
                        if (quantity.Text == "")
                            return false;
                        else if (double.Parse(quantity.Text) < 0.000001)
                            return false;
                        else if (remarks.Text == "")
                            return false;
                    }
                }
            }
            else if (type == 6)
            {
                foreach (GridViewRow rows in gvw_add_cons.Rows)
                {
                    TextBox quantity = (TextBox)rows.FindControl("quantity_cons");
                    TextBox remarks_cons = (TextBox)rows.FindControl("remarks_cons");
                    //string a = quantity.Text;
                    if (mandatory == "FALSE")
                    {
                        if (quantity.Text == "")
                        {
                            return false;
                        }
                        else if (double.Parse(quantity.Text) < 0.000001)
                        {
                            return false;
                        }
                    }
                    if (mandatory.Contains("INSTRUCTION"))
                    {
                        if (quantity.Text == "")
                        {
                            return false;
                        }
                        else if (double.Parse(quantity.Text) < 0.000001)
                        {
                            return false;
                        }
                        else if (remarks_cons.Text == "")
                            return false;
                    }
                    else
                    {
                        if (quantity.Text == "")
                        {
                            return false;
                        }
                        else if (double.Parse(quantity.Text) < 0.000001)
                        {
                            return false;
                        }
                    }
                }
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CheckQuantityPrescription", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CheckQuantityPrescription", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());

        return true;
    }

    protected void DisableRowList(int type)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            if (type == 1)//gvw_drugs
            {
                foreach (GridViewRow rows in gvw_drug.Rows)
                {
                    HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_id");
                    HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_no");
                    HiddenField item_id = (HiddenField)rows.FindControl("item_id");
                    Label item_name = (Label)rows.FindControl("item_name");
                    //HiddenField orderSetDetailId = (HiddenField)rows.FindControl("id_order_drugs_detail");
                    TextBox quantity = (TextBox)rows.FindControl("quantity");
                    HiddenField uom_id = (HiddenField)rows.FindControl("uom_id");
                    Label uom_code = (Label)rows.FindControl("uom_code");
                    DropDownList frequency_code = (DropDownList)rows.FindControl("frequency_code");
                    TextBox dosage_id = (TextBox)rows.FindControl("dosage_id");
                    HiddenField dose_text = (HiddenField)rows.FindControl("dose_text");
                    DropDownList doseuom = (DropDownList)rows.FindControl("doseuom");
                    TextBox remarks = (TextBox)rows.FindControl("remarks");
                    DropDownList administrationRouteCode = (DropDownList)rows.FindControl("administrationRouteCode");
                    TextBox iteration = (TextBox)rows.FindControl("iteration");
                    CheckBox is_routine = (CheckBox)rows.FindControl("is_routine");
                    HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables");
                    HiddenField compound_id = (HiddenField)rows.FindControl("compound_id");
                    HiddenField compound_name = (HiddenField)rows.FindControl("compound_name");
                    HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_id");
                    HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_id");
                    HiddenField is_delete = (HiddenField)rows.FindControl("is_delete");
                    LinkButton btndelete = (LinkButton)rows.FindControl("btndelete");
                    CheckBox is_dosetext = (CheckBox)rows.FindControl("is_dosetext");
                    TextBox dosetext = (TextBox)rows.FindControl("dosetext");

                    quantity.Enabled = false;
                    frequency_code.Enabled = false;
                    remarks.Enabled = false;
                    dosage_id.Enabled = false;
                    doseuom.Enabled = false;
                    administrationRouteCode.Enabled = false;
                    iteration.Enabled = false;
                    is_routine.Enabled = false;
                    btndelete.Enabled = false; btndelete.Visible = false;
                    rows.ForeColor = ColorTranslator.FromHtml("#969696");
                    is_dosetext.Enabled = false;
                    dosetext.Enabled = false;
                }
            }
            //else if (type == 2)//gvw_compound_header
            //{
            //    foreach (GridViewRow rows in gvw_comp.Rows)
            //    {
            //        HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_comp_id");
            //        HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_comp_no");
            //        HiddenField item_id = (HiddenField)rows.FindControl("item_comp_id");
            //        HiddenField item_name = (HiddenField)rows.FindControl("item_comp_name");
            //        //HiddenField orderSetDetailId = (HiddenField)rows.FindControl("id_order_drugs_detail");
            //        TextBox quantity = (TextBox)rows.FindControl("quantity_comp");
            //        HiddenField uom_id = (HiddenField)rows.FindControl("uom_comp_id");
            //        Label uom_code = (Label)rows.FindControl("uom_comp_code");
            //        DropDownList frequency_code = (DropDownList)rows.FindControl("frequency_comp_code");
            //        TextBox dosage_id = (TextBox)rows.FindControl("dosage_comp_id");
            //        TextBox dose_text = (TextBox)rows.FindControl("dose_comp_text");
            //        TextBox remarks = (TextBox)rows.FindControl("remarks_comp");
            //        DropDownList administrationRouteCode = (DropDownList)rows.FindControl("administrationRouteCode_comp");
            //        TextBox iteration = (TextBox)rows.FindControl("iteration_comp");
            //        CheckBox is_routine = (CheckBox)rows.FindControl("is_routine_comp");
            //        HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables_comp");
            //        HiddenField compound_id = (HiddenField)rows.FindControl("compound_comp_id");
            //        LinkButton compound_name = (LinkButton)rows.FindControl("compound_comp_name");
            //        HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_comp_id");
            //        HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_comp_id");
            //        HiddenField is_delete = (HiddenField)rows.FindControl("is_delete_comp");
            //        LinkButton btndelete = (LinkButton)rows.FindControl("btndelete");

            //        quantity.Enabled = false;
            //        frequency_code.Enabled = false;
            //        remarks.Enabled = false;
            //        administrationRouteCode.Enabled = false;
            //        iteration.Enabled = false;
            //        is_routine.Enabled = false;
            //        btndelete.Enabled = false; btndelete.Visible = false;
            //        rows.ForeColor = ColorTranslator.FromHtml("#969696");

            //    }
            //}
            //else if (type == 4)//modal_compound_detail
            //{
            //    item_search_detail.Enabled = false;
            //    foreach (GridViewRow rows in gvw_comp_detail.Rows)
            //    {
            //        HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_compdtl_id");
            //        HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_compdtl_no");
            //        HiddenField item_id = (HiddenField)rows.FindControl("item_compdtl_id");
            //        Label item_name = (Label)rows.FindControl("item_compdtl_name");
            //        //HiddenField orderSetDetailId = (HiddenField)rows.FindControl("id_order_drugs_detail");
            //        TextBox quantity = (TextBox)rows.FindControl("quantity_compdtl");
            //        HiddenField uom_id = (HiddenField)rows.FindControl("uom_compdtl_id");
            //        Label uom_code = (Label)rows.FindControl("uom_compdtl_code");
            //        HiddenField frequency_id = (HiddenField)rows.FindControl("frequency_compdtl_id");
            //        HiddenField frequency_code = (HiddenField)rows.FindControl("frequency_compdtl_code");
            //        TextBox dosage_id = (TextBox)rows.FindControl("dosage_compdtl_id");
            //        TextBox dose_text = (TextBox)rows.FindControl("dose_compdtl_text");
            //        TextBox remarks = (TextBox)rows.FindControl("remarks_compdtl");
            //        HiddenField administrationRoute_id = (HiddenField)rows.FindControl("administration_route_compdtl_id");
            //        HiddenField administrationRouteCode = (HiddenField)rows.FindControl("administration_route_compdtl_code");
            //        TextBox iteration = (TextBox)rows.FindControl("iteration_compdtl");
            //        HiddenField is_routine = (HiddenField)rows.FindControl("is_routine_compdtl");
            //        HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables_compdtl");
            //        HiddenField compound_id = (HiddenField)rows.FindControl("compound_compdtl_id");
            //        HiddenField compound_name = (HiddenField)rows.FindControl("compound_compdtl_name");
            //        HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_compdtl_id");
            //        HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_compdtl_id");
            //        HiddenField is_delete = (HiddenField)rows.FindControl("is_delete_compdtl");
            //        LinkButton btndelete = (LinkButton)rows.FindControl("btndelete");


            //        quantity.Enabled = false;
            //        remarks.Enabled = false;
            //        iteration.Enabled = false;
            //        btndelete.Enabled = false; btndelete.Visible = false;
            //        rows.ForeColor = ColorTranslator.FromHtml("#969696");
            //    }
            //}
            else if (type == 5)//gvw_consumables
            {
                txtitemcons.Enabled = false;
                txtItemCons_AC.Enabled = false;
                foreach (GridViewRow rows in gvw_consumables.Rows)
                {
                    HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_id_cons");
                    HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_no_cons");
                    HiddenField item_id = (HiddenField)rows.FindControl("itemId_cons");
                    Label item_name = (Label)rows.FindControl("item_name_cons");
                    TextBox quantity = (TextBox)rows.FindControl("quantity_cons");
                    HiddenField uom_id = (HiddenField)rows.FindControl("uom_id_cons");
                    Label uom_code = (Label)rows.FindControl("uom_code_cons");
                    HiddenField frequency_code = (HiddenField)rows.FindControl("frequency_code_cons");
                    HiddenField frequency_id = (HiddenField)rows.FindControl("frequency_id_cons");
                    HiddenField dosage_id = (HiddenField)rows.FindControl("dosage_id_cons");
                    HiddenField dose_text = (HiddenField)rows.FindControl("dose_text_cons");
                    TextBox remarks = (TextBox)rows.FindControl("remarks_cons");
                    HiddenField administrationRouteId = (HiddenField)rows.FindControl("administrationRouteId_cons");
                    HiddenField administrationRouteCode = (HiddenField)rows.FindControl("administrationRouteCode_cons");
                    HiddenField iteration = (HiddenField)rows.FindControl("iteration_cons");
                    HiddenField is_routine = (HiddenField)rows.FindControl("is_routine_cons");
                    HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables_cons");
                    HiddenField compound_id = (HiddenField)rows.FindControl("compound_id_cons");
                    HiddenField compound_name = (HiddenField)rows.FindControl("compound_name_cons");
                    HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_id_cons");
                    HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_id_cons");
                    HiddenField is_delete = (HiddenField)rows.FindControl("is_delete_cons");
                    LinkButton btndelete = (LinkButton)rows.FindControl("btndelete");

                    quantity.Enabled = false;
                    remarks.Enabled = false;
                    btndelete.Enabled = false; btndelete.Visible = false;
                    rows.ForeColor = ColorTranslator.FromHtml("#969696");
                }
            }
            else if (type == 6)
            {
                foreach (GridViewRow rows in gvw_labset.Rows)
                {
                    LinkButton setname_lab = (LinkButton)rows.FindControl("setname_lab");
                    setname_lab.Enabled = false;
                    setname_lab.Style.Add("cursor", "not-allowed");
                    rows.ForeColor = ColorTranslator.FromHtml("#969696");
                }
            }
            else if (type == 7)
            {
                foreach (GridViewRow rows in gvw_frequent_drugs.Rows)
                {
                    LinkButton frequentdrugs_name = (LinkButton)rows.FindControl("frequentdrugs_name");
                    frequentdrugs_name.Enabled = false;
                    frequentdrugs_name.Style.Add("cursor", "not-allowed");
                }
            }
            else if (type == 8)
            {
                foreach (GridViewRow rows in gvw_orderset.Rows)
                {
                    LinkButton setname = (LinkButton)rows.FindControl("setname");
                    setname.Enabled = false;
                    setname.Style.Add("cursor", "not-allowed");
                }
            }
            else if (type == 9)//gvwAdditionalDrugs
            {
                foreach (GridViewRow rows in gvwAdditionalDrugs.Rows)
                {
                    HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_id");
                    HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_no");
                    HiddenField item_id = (HiddenField)rows.FindControl("item_id");
                    Label item_name = (Label)rows.FindControl("item_name");
                    //HiddenField orderSetDetailId = (HiddenField)rows.FindControl("id_order_drugs_detail");
                    TextBox quantity = (TextBox)rows.FindControl("quantity");
                    HiddenField uom_id = (HiddenField)rows.FindControl("uom_id");
                    Label uom_code = (Label)rows.FindControl("uom_code");
                    DropDownList frequency_code = (DropDownList)rows.FindControl("frequency_code");
                    TextBox dosage_id = (TextBox)rows.FindControl("dosage_id");
                    HiddenField dose_text = (HiddenField)rows.FindControl("dose_text");
                    DropDownList doseuom = (DropDownList)rows.FindControl("doseuom");
                    TextBox remarks = (TextBox)rows.FindControl("remarks");
                    DropDownList administrationRouteCode = (DropDownList)rows.FindControl("administrationRouteCode");
                    TextBox iteration = (TextBox)rows.FindControl("iteration");
                    CheckBox is_routine = (CheckBox)rows.FindControl("is_routine");
                    HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables");
                    HiddenField compound_id = (HiddenField)rows.FindControl("compound_id");
                    HiddenField compound_name = (HiddenField)rows.FindControl("compound_name");
                    HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_id");
                    HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_id");
                    HiddenField is_delete = (HiddenField)rows.FindControl("is_delete");
                    LinkButton btndelete = (LinkButton)rows.FindControl("btndelete");
                    CheckBox is_dosetext = (CheckBox)rows.FindControl("is_dosetext");
                    TextBox dosetext = (TextBox)rows.FindControl("dosetext");

                    quantity.Enabled = false;
                    frequency_code.Enabled = false;
                    remarks.Enabled = false;
                    dosage_id.Enabled = false;
                    doseuom.Enabled = false;
                    administrationRouteCode.Enabled = false;
                    iteration.Enabled = false;
                    is_routine.Enabled = false;
                    btndelete.Enabled = false; btndelete.Visible = false;
                    rows.ForeColor = ColorTranslator.FromHtml("#969696");
                    is_dosetext.Enabled = false;
                    dosetext.Enabled = false;
                }
            }
            else if (type == 10)//gvw_add_cons
            {
                txtItemAddCons.Enabled = false;
                foreach (GridViewRow rows in gvw_add_cons.Rows)
                {
                    HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_id_cons");
                    HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_no_cons");
                    HiddenField item_id = (HiddenField)rows.FindControl("itemId_cons");
                    Label item_name = (Label)rows.FindControl("item_name_cons");
                    TextBox quantity = (TextBox)rows.FindControl("quantity_cons");
                    HiddenField uom_id = (HiddenField)rows.FindControl("uom_id_cons");
                    Label uom_code = (Label)rows.FindControl("uom_code_cons");
                    HiddenField frequency_code = (HiddenField)rows.FindControl("frequency_code_cons");
                    HiddenField frequency_id = (HiddenField)rows.FindControl("frequency_id_cons");
                    HiddenField dosage_id = (HiddenField)rows.FindControl("dosage_id_cons");
                    HiddenField dose_text = (HiddenField)rows.FindControl("dose_text_cons");
                    TextBox remarks = (TextBox)rows.FindControl("remarks_cons");
                    HiddenField administrationRouteId = (HiddenField)rows.FindControl("administrationRouteId_cons");
                    HiddenField administrationRouteCode = (HiddenField)rows.FindControl("administrationRouteCode_cons");
                    HiddenField iteration = (HiddenField)rows.FindControl("iteration_cons");
                    HiddenField is_routine = (HiddenField)rows.FindControl("is_routine_cons");
                    HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables_cons");
                    HiddenField compound_id = (HiddenField)rows.FindControl("compound_id_cons");
                    HiddenField compound_name = (HiddenField)rows.FindControl("compound_name_cons");
                    HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_id_cons");
                    HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_id_cons");
                    HiddenField is_delete = (HiddenField)rows.FindControl("is_delete_cons");
                    LinkButton btndelete = (LinkButton)rows.FindControl("btndelete");

                    quantity.Enabled = false;
                    remarks.Enabled = false;
                    btndelete.Enabled = false; btndelete.Visible = false;
                    rows.ForeColor = ColorTranslator.FromHtml("#969696");
                }
            }
            else if (type == 11)//gvw_racikan
            {
                foreach (GridViewRow rows in gvw_racikan_header.Rows)
                {
                    LinkButton btnedit = (LinkButton)rows.FindControl("btneditRacikanHeader");
                    LinkButton btndelete = (LinkButton)rows.FindControl("btndeleteRacikanHeader");

                    btnedit.Enabled = false; btnedit.Visible = false;
                    btndelete.Enabled = false; btndelete.Visible = false;
                    rows.ForeColor = ColorTranslator.FromHtml("#969696");
                }
            }
            else if (type == 12)//gvw_racikan_add
            {
                foreach (GridViewRow rows in gvw_racikan_header_add.Rows)
                {
                    LinkButton btnedit = (LinkButton)rows.FindControl("btneditRacikanHeader_add");
                    LinkButton btndelete = (LinkButton)rows.FindControl("btndeleteRacikanHeader_add");

                    btnedit.Enabled = false; btnedit.Visible = false;
                    btndelete.Enabled = false; btndelete.Visible = false;
                    rows.ForeColor = ColorTranslator.FromHtml("#969696");
                }
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "DisableRowList", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "DisableRowList", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            // Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }
    
    public void UncheckRoutine(List<PatientRoutineMedication> tempcurrmed)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            List<Prescription> datapresc = new List<Prescription>();
            datapresc = GetRowList(1);
            var temp = (from a in datapresc
                        join b in tempcurrmed
                        on a.item_id equals b.routine_sales_item_id into joined
                        from b in joined.DefaultIfEmpty()
                            //where b.medication == null
                        select new temproutine
                        {
                            medication = b == null ? "" : b.medication,
                            item_id = a.item_id
                        }).Distinct().ToList();

            //foreach (var xtemp in temp.Where(y => y.medication == ""))
            //{
            //    datapresc.Find(y => y.item_id == xtemp.item_id).is_routine = 0;
            //}

            (from a in datapresc
             join b in temp on a.item_id equals b.item_id
             where b.medication == ""
             select a
                     ).ToList().ForEach(x => x.is_routine = 0);

            gvw_drug.DataSource = Helper.ToDataTable(datapresc);
            gvw_drug.DataBind();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "UncheckRoutine", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));


        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "UncheckRoutine", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected List<Prescription> GetRowList(int type)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        List<Prescription> data = new List<Prescription>();
        try
        {
            
            if (type == 1)//gvw_drugs
            {
                int counter = 1;
                foreach (GridViewRow rows in gvw_drug.Rows)
                {
                    HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_id");
                    HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_no");
                    HiddenField item_id = (HiddenField)rows.FindControl("item_id");
                    Label item_name = (Label)rows.FindControl("item_name");
                    //HiddenField orderSetDetailId = (HiddenField)rows.FindControl("id_order_drugs_detail");
                    TextBox quantity = (TextBox)rows.FindControl("quantity");
                    HiddenField uom_id = (HiddenField)rows.FindControl("uom_id");
                    Label uom_code = (Label)rows.FindControl("uom_code");
                    DropDownList frequency_code = (DropDownList)rows.FindControl("frequency_code");
                    TextBox dosage_id = (TextBox)rows.FindControl("dosage_id");
                    DropDownList dosage = (DropDownList)rows.FindControl("doseuom");
                    HiddenField dose_text = (HiddenField)rows.FindControl("dose_text");
                    TextBox remarks = (TextBox)rows.FindControl("remarks");
                    DropDownList administrationRouteCode = (DropDownList)rows.FindControl("administrationRouteCode");
                    TextBox iteration = (TextBox)rows.FindControl("iteration");
                    CheckBox is_routine = (CheckBox)rows.FindControl("is_routine");
                    HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables");
                    HiddenField compound_id = (HiddenField)rows.FindControl("compound_id");
                    HiddenField compound_name = (HiddenField)rows.FindControl("compound_name");
                    HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_id");
                    HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_id");
                    HiddenField is_delete = (HiddenField)rows.FindControl("is_delete");
                    CheckBox is_dosetext = (CheckBox)rows.FindControl("is_dosetext");
                    TextBox dosetext = (TextBox)rows.FindControl("dosetext");

                    Prescription row = new Prescription();

                    row.item_sequence = counter;
                    counter = counter + 1;
                    row.prescription_id = Guid.Parse(prescription_id.Value);
                    row.prescription_no = prescription_no.Value;
                    row.item_id = Int64.Parse(item_id.Value);
                    row.item_name = item_name.Text;
                    if (quantity.Text == "")
                    {
                        row.quantity = "0";
                    }
                    else
                    {
                        //var decimaltemp = Decimal.Parse(quantity.Text);
                        //row.quantity = Convert.ToInt64(decimaltemp);

                        row.quantity = quantity.Text.ToString().Replace(",",".");
                    }
                    if (uom_code.Text.ToString() != "")
                    {
                        row.uom_id = Int64.Parse(uom_id.Value.ToString());
                        row.uom_code = uom_code.Text;
                    }
                    else if (uom_code.Text.ToString() == "")
                    {
                        row.uom_id = 0;
                        row.uom_code = "";
                    }
                    row.frequency_id = Int64.Parse(frequency_code.SelectedValue);
                    row.frequency_code = frequency_code.SelectedItem.Text;
                    if (dosage_id.Text.ToString() != "")
                    {
                        row.dosage_id = dosage_id.Text.ToString().Replace(",", ".");
                    }
                    else
                        row.dosage_id = "0";

                    row.dose_uom = dosage.SelectedItem.Text;
                    row.dose_uom_id = long.Parse(dosage.SelectedValue);
                    row.dose_text = dosetext.Text;
                    row.remarks = remarks.Text;
                    row.administration_route_id = Int64.Parse(administrationRouteCode.SelectedValue);
                    row.administration_route_code = administrationRouteCode.SelectedItem.Text;
                    if (iteration.Text == "")
                    {
                        row.iteration = 0;
                    }
                    else
                    {
                        row.iteration = int.Parse(iteration.Text);
                    }
                    if (is_routine.Checked)
                    {
                        row.is_routine = 1;
                    }
                    else
                    {
                        row.is_routine = 0;
                    }

                    row.is_consumables = int.Parse(is_consumables.Value);
                    row.compound_id = Guid.Parse(compound_id.Value.ToString());
                    row.compound_name = compound_name.Value;
                    row.origin_prescription_id = Guid.Parse(origin_prescription_id.Value);
                    row.hope_arinvoice_id = Int64.Parse(hope_arinvoice_id.Value);
                    row.is_delete = int.Parse(is_delete.Value);

                    if (is_dosetext.Checked)
                    {
                        row.IsDoseText = true;
                    }
                    else
                    {
                        row.IsDoseText = false;
                        row.dose_text = "";
                    }

                    row.is_iter = iteration.Enabled;

                    data.Add(row);
                }
            }
            //else if (type == 2)//gvw_compound_header
            //{
            //    foreach (GridViewRow rows in gvw_comp.Rows)
            //    {
            //        HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_comp_id");
            //        HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_comp_no");
            //        HiddenField item_id = (HiddenField)rows.FindControl("item_comp_id");
            //        HiddenField item_name = (HiddenField)rows.FindControl("item_comp_name");
            //        //HiddenField orderSetDetailId = (HiddenField)rows.FindControl("id_order_drugs_detail");
            //        TextBox quantity = (TextBox)rows.FindControl("quantity_comp");
            //        HiddenField uom_id = (HiddenField)rows.FindControl("uom_comp_id");
            //        Label uom_code = (Label)rows.FindControl("uom_comp_code");
            //        DropDownList frequency_code = (DropDownList)rows.FindControl("frequency_comp_code");
            //        TextBox dosage_id = (TextBox)rows.FindControl("dosage_comp_id");
            //        TextBox dose_text = (TextBox)rows.FindControl("dose_comp_text");
            //        TextBox remarks = (TextBox)rows.FindControl("remarks_comp");
            //        DropDownList administrationRouteCode = (DropDownList)rows.FindControl("administrationRouteCode_comp");
            //        TextBox iteration = (TextBox)rows.FindControl("iteration_comp");
            //        CheckBox is_routine = (CheckBox)rows.FindControl("is_routine_comp");
            //        HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables_comp");
            //        HiddenField compound_id = (HiddenField)rows.FindControl("compound_comp_id");
            //        LinkButton compound_name = (LinkButton)rows.FindControl("compound_comp_name");
            //        HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_comp_id");
            //        HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_comp_id");
            //        HiddenField is_delete = (HiddenField)rows.FindControl("is_delete_comp");

            //        Prescription row = new Prescription();

            //        row.prescription_id = Guid.Parse(prescription_id.Value);
            //        row.prescription_no = prescription_no.Value;
            //        row.item_id = Int64.Parse(item_id.Value);
            //        row.item_name = item_name.Value;
            //        if (quantity.Text == "")
            //        {
            //            row.quantity = "0";
            //        }
            //        else
            //        {
            //            //var decimaltemp = Decimal.Parse(quantity.Text);
            //            //row.quantity = Convert.ToInt64(decimaltemp);
            //            row.quantity = quantity.Text.ToString();
            //        }
            //        if (uom_code.Text.ToString() != "")
            //        {
            //            row.uom_id = Int64.Parse(uom_id.Value.ToString());
            //            row.uom_code = uom_code.Text;
            //        }
            //        else if (uom_code.Text.ToString() == "")
            //        {
            //            row.uom_id = 0;
            //            row.uom_code = "";
            //        }
            //        row.frequency_id = Int64.Parse(frequency_code.SelectedValue);
            //        row.frequency_code = frequency_code.SelectedItem.Text;
            //        if (dosage_id.Text.ToString() != "")
            //        {
            //            row.dosage_id = dosage_id.Text.ToString();
            //        }
            //        else
            //            row.dosage_id = "0";
            //        row.dose_text = dose_text.Text;
            //        row.remarks = remarks.Text;
            //        row.administration_route_id = Int64.Parse(administrationRouteCode.SelectedValue);
            //        row.administration_route_code = administrationRouteCode.SelectedItem.Text;
            //        if (iteration.Text == "")
            //        {
            //            row.iteration = 0;
            //        }
            //        else
            //        {
            //            row.iteration = int.Parse(iteration.Text);
            //        }
            //        if (is_routine.Checked)
            //        {
            //            row.is_routine = 1;
            //        }
            //        else
            //        {
            //            row.is_routine = 0;
            //        }

            //        row.is_consumables = int.Parse(is_consumables.Value);
            //        row.compound_id = Guid.Parse(compound_id.Value.ToString());
            //        row.compound_name = compound_name.Text;
            //        row.origin_prescription_id = Guid.Parse(origin_prescription_id.Value);
            //        row.hope_arinvoice_id = Int64.Parse(hope_arinvoice_id.Value);
            //        row.is_delete = int.Parse(is_delete.Value);

            //        data.Add(row);
            //    }
            //}
            //else if (type == 3)//gvw_compound_detail
            //{
            //    foreach (GridViewRow rows in gvw_compdetail.Rows)
            //    {
            //        HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_compdtlhdn_id");
            //        HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_compdtlhdn_no");
            //        HiddenField item_id = (HiddenField)rows.FindControl("item_compdtlhdn_id");
            //        Label item_name = (Label)rows.FindControl("item_compdtlhdn_name");
            //        //HiddenField orderSetDetailId = (HiddenField)rows.FindControl("id_order_drugs_detail");
            //        Label quantity = (Label)rows.FindControl("quantity_compdtlhdn");
            //        HiddenField uom_id = (HiddenField)rows.FindControl("uom_compdtlhdn_id");
            //        Label uom_code = (Label)rows.FindControl("uom_compdtlhdn_code");
            //        HiddenField frequency_id = (HiddenField)rows.FindControl("frequency_compdtlhdn_id");
            //        Label frequency_code = (Label)rows.FindControl("frequency_compdtlhdn_code");
            //        Label dosage_id = (Label)rows.FindControl("dosage_compdtlhdn_id");
            //        Label dose_text = (Label)rows.FindControl("dose_compdtlhdn_text");
            //        Label remarks = (Label)rows.FindControl("remarks_compdtlhdn");
            //        HiddenField administrationRoute_id = (HiddenField)rows.FindControl("administrationRouteid_compdtlhdn");
            //        Label administrationRouteCode = (Label)rows.FindControl("administrationRouteCode_compdtlhdn");
            //        Label iteration = (Label)rows.FindControl("iteration_compdtlhdn");
            //        CheckBox is_routine = (CheckBox)rows.FindControl("is_routine_compdtlhdn");
            //        HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables_compdtlhdn");
            //        HiddenField compound_id = (HiddenField)rows.FindControl("compound_compdtlhdn_id");
            //        Label compound_name = (Label)rows.FindControl("compound_compdtlhdn_name");
            //        HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_compdtlhdn_id");
            //        HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_compdtlhdn_id");
            //        HiddenField is_delete = (HiddenField)rows.FindControl("is_delete_compdtlhdn");

            //        Prescription row = new Prescription();

            //        row.prescription_id = Guid.Parse(prescription_id.Value);
            //        row.prescription_no = prescription_no.Value;
            //        row.item_id = Int64.Parse(item_id.Value);
            //        row.item_name = item_name.Text;
            //        if (quantity.Text == "")
            //        {
            //            row.quantity = "0";
            //        }
            //        else
            //        {
            //            //var decimaltemp = Decimal.Parse(quantity.Text);
            //            //row.quantity = Convert.ToInt64(decimaltemp);
            //            row.quantity = quantity.Text.ToString();
            //        }
            //        if (uom_code.Text.ToString() != "")
            //        {
            //            row.uom_id = Int64.Parse(uom_id.Value.ToString());
            //            row.uom_code = uom_code.Text;
            //        }
            //        else if (uom_code.Text.ToString() == "")
            //        {
            //            row.uom_id = 0;
            //            row.uom_code = "";
            //        }
            //        row.frequency_id = Int64.Parse(frequency_id.Value);
            //        row.frequency_code = frequency_code.Text;
            //        if (dosage_id.Text.ToString() != "")
            //        {
            //            row.dosage_id = dosage_id.Text.ToString();
            //        }
            //        else
            //            row.dosage_id = "0";
            //        row.dose_text = dose_text.Text;
            //        row.remarks = remarks.Text;
            //        row.administration_route_id = Int64.Parse(administrationRoute_id.Value);
            //        row.administration_route_code = administrationRouteCode.Text;
            //        if (iteration.Text == "")
            //        {
            //            row.iteration = 0;
            //        }
            //        else
            //        {
            //            row.iteration = int.Parse(iteration.Text);
            //        }
            //        if (is_routine.Checked)
            //        {
            //            row.is_routine = 1;
            //        }
            //        else
            //        {
            //            row.is_routine = 0;
            //        }

            //        row.is_consumables = int.Parse(is_consumables.Value);
            //        row.compound_id = Guid.Parse(compound_id.Value.ToString());
            //        row.compound_name = compound_name.Text;
            //        row.origin_prescription_id = Guid.Parse(origin_prescription_id.Value);
            //        row.hope_arinvoice_id = Int64.Parse(hope_arinvoice_id.Value);
            //        row.is_delete = int.Parse(is_delete.Value);

            //        data.Add(row);
            //    }
            //}
            //else if (type == 4)//modal_compound_detail
            //{
            //    foreach (GridViewRow rows in gvw_comp_detail.Rows)
            //    {
            //        HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_compdtl_id");
            //        HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_compdtl_no");
            //        HiddenField item_id = (HiddenField)rows.FindControl("item_compdtl_id");
            //        Label item_name = (Label)rows.FindControl("item_compdtl_name");
            //        //HiddenField orderSetDetailId = (HiddenField)rows.FindControl("id_order_drugs_detail");
            //        TextBox quantity = (TextBox)rows.FindControl("quantity_compdtl");
            //        HiddenField uom_id = (HiddenField)rows.FindControl("uom_compdtl_id");
            //        Label uom_code = (Label)rows.FindControl("uom_compdtl_code");
            //        HiddenField frequency_id = (HiddenField)rows.FindControl("frequency_compdtl_id");
            //        HiddenField frequency_code = (HiddenField)rows.FindControl("frequency_compdtl_code");
            //        TextBox dosage_id = (TextBox)rows.FindControl("dosage_compdtl_id");
            //        TextBox dose_text = (TextBox)rows.FindControl("dose_compdtl_text");
            //        TextBox remarks = (TextBox)rows.FindControl("remarks_compdtl");
            //        HiddenField administrationRoute_id = (HiddenField)rows.FindControl("administration_route_compdtl_id");
            //        HiddenField administrationRouteCode = (HiddenField)rows.FindControl("administration_route_compdtl_code");
            //        TextBox iteration = (TextBox)rows.FindControl("iteration_compdtl");
            //        HiddenField is_routine = (HiddenField)rows.FindControl("is_routine_compdtl");
            //        HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables_compdtl");
            //        HiddenField compound_id = (HiddenField)rows.FindControl("compound_compdtl_id");
            //        HiddenField compound_name = (HiddenField)rows.FindControl("compound_compdtl_name");
            //        HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_compdtl_id");
            //        HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_compdtl_id");
            //        HiddenField is_delete = (HiddenField)rows.FindControl("is_delete_compdtl");

            //        Prescription row = new Prescription();

            //        row.prescription_id = Guid.Parse(prescription_id.Value);
            //        row.prescription_no = prescription_no.Value;
            //        row.item_id = Int64.Parse(item_id.Value);
            //        row.item_name = item_name.Text;
            //        if (quantity.Text == "")
            //        {
            //            row.quantity = "0";
            //        }
            //        else
            //        {
            //            //var decimaltemp = Decimal.Parse(quantity.Text);
            //            //row.quantity = Convert.ToInt64(decimaltemp);
            //            row.quantity = quantity.Text.ToString();
            //        }
            //        if (uom_code.Text.ToString() != "")
            //        {
            //            row.uom_id = Int64.Parse(uom_id.Value.ToString());
            //            row.uom_code = uom_code.Text;
            //        }
            //        else if (uom_code.Text.ToString() == "")
            //        {
            //            row.uom_id = 0;
            //            row.uom_code = "";
            //        }
            //        row.frequency_id = Int64.Parse(frequency_id.Value);
            //        row.frequency_code = frequency_code.Value;
            //        if (dosage_id.Text.ToString() != "")
            //        {
            //            row.dosage_id = dosage_id.Text.ToString();
            //        }
            //        else
            //            row.dosage_id = "0";
            //        row.dose_text = dose_text.Text;
            //        row.remarks = remarks.Text;
            //        row.administration_route_id = Int64.Parse(administrationRoute_id.Value);
            //        row.administration_route_code = administrationRouteCode.Value;
            //        if (iteration.Text == "")
            //        {
            //            row.iteration = 0;
            //        }
            //        else
            //        {
            //            row.iteration = int.Parse(iteration.Text);
            //        }

            //        row.is_routine = int.Parse(is_routine.Value);

            //        row.is_consumables = int.Parse(is_consumables.Value);
            //        row.compound_id = Guid.Parse(compound_id.Value.ToString());
            //        row.compound_name = compound_name.Value;
            //        row.origin_prescription_id = Guid.Parse(origin_prescription_id.Value);
            //        row.hope_arinvoice_id = Int64.Parse(hope_arinvoice_id.Value);
            //        row.is_delete = int.Parse(is_delete.Value);

            //        data.Add(row);
            //    }
            //}
            else if (type == 5)//gvw_consumables
            {
                int countercons = 1;
                foreach (GridViewRow rows in gvw_consumables.Rows)
                {
                    HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_id_cons");
                    HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_no_cons");
                    HiddenField item_id = (HiddenField)rows.FindControl("itemId_cons");
                    Label item_name = (Label)rows.FindControl("item_name_cons");
                    TextBox quantity = (TextBox)rows.FindControl("quantity_cons");
                    HiddenField uom_id = (HiddenField)rows.FindControl("uom_id_cons");
                    Label uom_code = (Label)rows.FindControl("uom_code_cons");
                    HiddenField frequency_code = (HiddenField)rows.FindControl("frequency_code_cons");
                    HiddenField frequency_id = (HiddenField)rows.FindControl("frequency_id_cons");
                    HiddenField dosage_id = (HiddenField)rows.FindControl("dosage_id_cons");
                    HiddenField dose_text = (HiddenField)rows.FindControl("dose_text_cons");
                    TextBox remarks = (TextBox)rows.FindControl("remarks_cons");
                    HiddenField administrationRouteId = (HiddenField)rows.FindControl("administrationRouteId_cons");
                    HiddenField administrationRouteCode = (HiddenField)rows.FindControl("administrationRouteCode_cons");
                    HiddenField iteration = (HiddenField)rows.FindControl("iteration_cons");
                    HiddenField is_routine = (HiddenField)rows.FindControl("is_routine_cons");
                    HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables_cons");
                    HiddenField compound_id = (HiddenField)rows.FindControl("compound_id_cons");
                    HiddenField compound_name = (HiddenField)rows.FindControl("compound_name_cons");
                    HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_id_cons");
                    HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_id_cons");
                    HiddenField is_delete = (HiddenField)rows.FindControl("is_delete_cons");

                    Prescription row = new Prescription();

                    row.item_sequence = countercons;
                    countercons = countercons + 1;
                    row.prescription_id = Guid.Parse(prescription_id.Value);
                    row.prescription_no = prescription_no.Value;
                    row.item_id = Int64.Parse(item_id.Value);
                    row.item_name = item_name.Text;
                    if (quantity.Text == "")
                    {
                        row.quantity = "0";
                    }
                    else
                    {
                        //var decimaltemp = Decimal.Parse(quantity.Text);
                        //row.quantity = Convert.ToInt64(decimaltemp);
                        row.quantity = quantity.Text.ToString().Replace(",", ".");
                    }
                    if (uom_code.Text.ToString() != "")
                    {
                        row.uom_id = Int64.Parse(uom_id.Value.ToString());
                        row.uom_code = uom_code.Text;
                    }
                    else if (uom_code.Text.ToString() == "")
                    {
                        row.uom_id = 0;
                        row.uom_code = "";
                    }
                    row.frequency_id = Int64.Parse(frequency_id.Value);
                    row.frequency_code = frequency_code.Value;
                    if (dosage_id.Value.ToString() != "")
                    {
                        row.dosage_id = dosage_id.Value.ToString().Replace(",", ".");
                    }
                    else
                        row.dosage_id = "0";
                    row.dose_text = dose_text.Value;
                    row.dose_uom_id = 0;
                    row.dose_uom = "";
                    row.remarks = remarks.Text;
                    row.administration_route_id = Int64.Parse(administrationRouteId.Value);
                    row.administration_route_code = administrationRouteCode.Value;
                    if (iteration.Value == "")
                    {
                        row.iteration = 0;
                    }
                    else
                    {
                        row.iteration = int.Parse(iteration.Value);
                    }
                    row.is_routine = int.Parse(is_routine.Value);


                    row.is_consumables = int.Parse(is_consumables.Value);
                    row.compound_id = Guid.Parse(compound_id.Value.ToString());
                    row.compound_name = compound_name.Value;
                    row.origin_prescription_id = Guid.Parse(origin_prescription_id.Value);
                    row.hope_arinvoice_id = Int64.Parse(hope_arinvoice_id.Value);
                    row.is_delete = int.Parse(is_delete.Value);

                    data.Add(row);
                }
            }
            else if (type == 6)//gvwAdditionalDrugs
            {
                int countadd = 1;
                foreach (GridViewRow rows in gvwAdditionalDrugs.Rows)
                {
                    HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_id");
                    HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_no");
                    HiddenField item_id = (HiddenField)rows.FindControl("item_id");
                    Label item_name = (Label)rows.FindControl("item_name");
                    //HiddenField orderSetDetailId = (HiddenField)rows.FindControl("id_order_drugs_detail");
                    TextBox quantity = (TextBox)rows.FindControl("quantity");
                    HiddenField uom_id = (HiddenField)rows.FindControl("uom_id");
                    Label uom_code = (Label)rows.FindControl("uom_code");
                    DropDownList frequency_code = (DropDownList)rows.FindControl("frequency_code");
                    TextBox dosage_id = (TextBox)rows.FindControl("dosage_id");
                    DropDownList dosage = (DropDownList)rows.FindControl("doseuom");
                    HiddenField dose_text = (HiddenField)rows.FindControl("dose_text");
                    TextBox remarks = (TextBox)rows.FindControl("remarks");
                    DropDownList administrationRouteCode = (DropDownList)rows.FindControl("administrationRouteCode");
                    TextBox iteration = (TextBox)rows.FindControl("iteration");
                    CheckBox is_routine = (CheckBox)rows.FindControl("is_routine");
                    HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables");
                    HiddenField compound_id = (HiddenField)rows.FindControl("compound_id");
                    HiddenField compound_name = (HiddenField)rows.FindControl("compound_name");
                    HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_id");
                    HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_id");
                    HiddenField is_delete = (HiddenField)rows.FindControl("is_delete");
                    CheckBox is_dosetext = (CheckBox)rows.FindControl("is_dosetext");
                    TextBox dosetext = (TextBox)rows.FindControl("dosetext");

                    Prescription row = new Prescription();
                    row.item_sequence = countadd;
                    countadd = countadd + 1;
                    row.prescription_id = Guid.Parse(prescription_id.Value);
                    row.prescription_no = prescription_no.Value;
                    row.item_id = Int64.Parse(item_id.Value);
                    row.item_name = item_name.Text;
                    if (quantity.Text == "")
                    {
                        row.quantity = "0";
                    }
                    else
                    {
                        //var decimaltemp = Decimal.Parse(quantity.Text);
                        //row.quantity = Convert.ToInt64(decimaltemp);

                        row.quantity = quantity.Text.ToString().Replace(",", ".");
                    }
                    if (uom_code.Text.ToString() != "")
                    {
                        row.uom_id = Int64.Parse(uom_id.Value.ToString());
                        row.uom_code = uom_code.Text;
                    }
                    else if (uom_code.Text.ToString() == "")
                    {
                        row.uom_id = 0;
                        row.uom_code = "";
                    }
                    row.frequency_id = Int64.Parse(frequency_code.SelectedValue);
                    row.frequency_code = frequency_code.SelectedItem.Text;
                    if (dosage_id.Text.ToString() != "")
                    {
                        row.dosage_id = dosage_id.Text.ToString().Replace(",", ".");
                    }
                    else
                        row.dosage_id = "0";

                    row.dose_uom = dosage.SelectedItem.Text;
                    row.dose_uom_id = long.Parse(dosage.SelectedValue);
                    row.dose_text = dosetext.Text;
                    row.remarks = remarks.Text;
                    row.administration_route_id = Int64.Parse(administrationRouteCode.SelectedValue);
                    row.administration_route_code = administrationRouteCode.SelectedItem.Text;
                    if (iteration.Text == "")
                    {
                        row.iteration = 0;
                    }
                    else
                    {
                        row.iteration = int.Parse(iteration.Text);
                    }
                    if (is_routine.Checked)
                    {
                        row.is_routine = 1;
                    }
                    else
                    {
                        row.is_routine = 0;
                    }

                    row.is_consumables = int.Parse(is_consumables.Value);
                    row.compound_id = Guid.Parse(compound_id.Value.ToString());
                    row.compound_name = compound_name.Value;
                    row.origin_prescription_id = Guid.Parse(origin_prescription_id.Value);
                    row.hope_arinvoice_id = Int64.Parse(hope_arinvoice_id.Value);
                    row.is_delete = int.Parse(is_delete.Value);

                    if (is_dosetext.Checked)
                    {
                        row.IsDoseText = true;
                    }
                    else
                    {
                        row.IsDoseText = false;
                        row.dose_text = "";
                    }

                    data.Add(row);
                }
            }
            else if (type == 7)//gvw_add_cons
            {
                int countaddcons = 1;
                foreach (GridViewRow rows in gvw_add_cons.Rows)
                {
                    HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_id_cons");
                    HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_no_cons");
                    HiddenField item_id = (HiddenField)rows.FindControl("itemId_cons");
                    Label item_name = (Label)rows.FindControl("item_name_cons");
                    TextBox quantity = (TextBox)rows.FindControl("quantity_cons");
                    HiddenField uom_id = (HiddenField)rows.FindControl("uom_id_cons");
                    Label uom_code = (Label)rows.FindControl("uom_code_cons");
                    HiddenField frequency_code = (HiddenField)rows.FindControl("frequency_code_cons");
                    HiddenField frequency_id = (HiddenField)rows.FindControl("frequency_id_cons");
                    HiddenField dosage_id = (HiddenField)rows.FindControl("dosage_id_cons");
                    HiddenField dose_text = (HiddenField)rows.FindControl("dose_text_cons");
                    TextBox remarks = (TextBox)rows.FindControl("remarks_cons");
                    HiddenField administrationRouteId = (HiddenField)rows.FindControl("administrationRouteId_cons");
                    HiddenField administrationRouteCode = (HiddenField)rows.FindControl("administrationRouteCode_cons");
                    HiddenField iteration = (HiddenField)rows.FindControl("iteration_cons");
                    HiddenField is_routine = (HiddenField)rows.FindControl("is_routine_cons");
                    HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables_cons");
                    HiddenField compound_id = (HiddenField)rows.FindControl("compound_id_cons");
                    HiddenField compound_name = (HiddenField)rows.FindControl("compound_name_cons");
                    HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_id_cons");
                    HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_id_cons");
                    HiddenField is_delete = (HiddenField)rows.FindControl("is_delete_cons");

                    Prescription row = new Prescription();
                    row.item_sequence = countaddcons;
                    countaddcons = countaddcons + 1;
                    row.prescription_id = Guid.Parse(prescription_id.Value);
                    row.prescription_no = prescription_no.Value;
                    row.item_id = Int64.Parse(item_id.Value);
                    row.item_name = item_name.Text;
                    if (quantity.Text == "")
                    {
                        row.quantity = "0";
                    }
                    else
                    {
                        //var decimaltemp = Decimal.Parse(quantity.Text);
                        //row.quantity = Convert.ToInt64(decimaltemp);
                        row.quantity = quantity.Text.ToString().Replace(",", ".");
                    }
                    if (uom_code.Text.ToString() != "")
                    {
                        row.uom_id = Int64.Parse(uom_id.Value.ToString());
                        row.uom_code = uom_code.Text;
                    }
                    else if (uom_code.Text.ToString() == "")
                    {
                        row.uom_id = 0;
                        row.uom_code = "";
                    }
                    row.frequency_id = Int64.Parse(frequency_id.Value);
                    row.frequency_code = frequency_code.Value;
                    if (dosage_id.Value.ToString() != "")
                    {
                        row.dosage_id = dosage_id.Value.ToString().Replace(",", ".");
                    }
                    else
                        row.dosage_id = "0";
                    row.dose_text = dose_text.Value;
                    row.dose_uom_id = 0;
                    row.dose_uom = "";
                    row.remarks = remarks.Text;
                    row.administration_route_id = Int64.Parse(administrationRouteId.Value);
                    row.administration_route_code = administrationRouteCode.Value;
                    if (iteration.Value == "")
                    {
                        row.iteration = 0;
                    }
                    else
                    {
                        row.iteration = int.Parse(iteration.Value);
                    }
                    row.is_routine = int.Parse(is_routine.Value);


                    row.is_consumables = int.Parse(is_consumables.Value);
                    row.compound_id = Guid.Parse(compound_id.Value.ToString());
                    row.compound_name = compound_name.Value;
                    row.origin_prescription_id = Guid.Parse(origin_prescription_id.Value);
                    row.hope_arinvoice_id = Int64.Parse(hope_arinvoice_id.Value);
                    row.is_delete = int.Parse(is_delete.Value);

                    data.Add(row);
                }
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetRowList", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));



        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetRowList", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());

        return data;
    }

    public void copyprescriptionfromhope(List<PrescriptionHOPE> hope)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            if (hope.Count > 0)
            {
                List<Prescription> temphope = new List<Prescription>();
                List<Prescription> listitem = GetRowList(1);
                temphope = (from a in hope
                            where a.item_id != 0
                            select new Prescription
                            {
                                item_id = a.item_id,
                                administration_route_code = a.administration_route_code,
                                administration_route_id = a.administration_route_id,
                                compound_id = a.compound_id,
                                compound_name = a.compound_name,
                                dosage_id = a.dosage_id,
                                dose_text = a.dose_text,
                                dose_uom = a.dose_uom,
                                dose_uom_id = a.dose_uom_id,
                                frequency_code = a.frequency_code,
                                frequency_id = a.frequency_id,
                                hope_arinvoice_id = a.hope_aritem_id,
                                is_consumables = a.is_consumables,
                                is_delete = a.is_delete,
                                is_routine = a.is_routine,
                                item_name = a.item_name,
                                iteration = a.iteration,
                                organization_id = a.organization_id,
                                origin_prescription_id = a.origin_prescription_id,
                                prescription_id = a.prescription_id,
                                prescription_no = a.prescription_no,
                                quantity = a.quantity,
                                remarks = a.remarks,
                                uom_code = a.uom_code,
                                uom_id = a.uom_id
                            }).ToList();
                if (listitem.Count > 0)
                {
                    List<Prescription> temppresc = new List<Prescription>();
                    List<Prescription> tempexist = new List<Prescription>();
                    foreach (var x in temphope)
                    {
                        if (listitem.Where(y => y.item_id == x.item_id).Count() > 0)
                            tempexist.Add(x);
                        else
                            temppresc.Add(x);
                    }
                    listitem.AddRange(temppresc);
                }
                else
                {
                    listitem = temphope;
                }
                if (listitem != null)
                {
                    DataTable dttemp = Helper.ToDataTable(listitem);
                    Session[Helper.SessionDrugPres + hfguidadditional.Value] = dttemp;
                    gvw_drug.DataSource = dttemp;
                    gvw_drug.DataBind();
                }
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "copyprescriptionfromhope", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "copyprescriptionfromhope", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void frequentdrugs_onclick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            if (hfverifydate.Value == "1")
            {
                if (hfadditional_take_date.Value == "1")
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Additional Prescription Already Taken by Pharmacy');", true);
                    ShowToastr("Additional Prescription already taken by Pharmacy.", "Info!", "info");
                }
                else
                {
                    int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
                    //LinkButton salesItemName = (LinkButton)gvw_frequent_drugs.Rows[selRowIndex].FindControl("frequentdrugs_name");
                    Label salesItemName_lbl = (Label)gvw_frequent_drugs.Rows[selRowIndex].FindControl("Label_frequentdrugs_name");
                    HiddenField salesitemid = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hffrequentdrugs_id");
                    HiddenField hfUomId = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfuom_id");
                    HiddenField hfUomCode = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfuom_code");
                    HiddenField hfDose = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfDose");
                    HiddenField hfDoseText = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfDoseText");
                    HiddenField hfDoseUomId = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfDoseUomId");
                    HiddenField hfAdministrationFrequencyId = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfAdministrationFrequencyId");
                    HiddenField hfAdministrationRouteId = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfAdministrationRouteId");
                    HiddenField hfAdministrationInstruction = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfAdministrationInstruction");
                    HiddenField hfisIter = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfis_iter");

                    List<Prescription> data = GetRowList(6);

                    hfDose.Value = hfDose.Value.ToString().Replace(',', '.');
                    string[] tempqty = hfDose.Value.ToString().Split('.');
                    if (tempqty.Count() > 1)
                    {
                        if (tempqty[1].Length == 3)
                        {
                            if (tempqty[1] == "000")
                            {
                                hfDose.Value = decimal.Parse(tempqty[0]).ToString();
                            }
                            else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                            {
                                hfDose.Value = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                            }
                            else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                            {
                                hfDose.Value = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                            }
                        }
                    }

                    Prescription temp = new Prescription();
                    temp.prescription_id = Guid.Empty;
                    temp.prescription_no = "";
                    temp.item_id = long.Parse(salesitemid.Value);
                    temp.item_name = salesItemName_lbl.Text;
                    temp.quantity = "0";
                    temp.uom_id = long.Parse(hfUomId.Value);
                    temp.uom_code = hfUomCode.Value;
                    temp.frequency_id = long.Parse(hfAdministrationFrequencyId.Value);
                    temp.frequency_code = "";
                    temp.dosage_id = hfDose.Value;
                    temp.dose_text = hfDoseText.Value;
                    temp.dose_uom = "-";
                    temp.dose_uom_id = long.Parse(hfDoseUomId.Value);
                    temp.administration_route_id = long.Parse(hfAdministrationRouteId.Value);
                    temp.administration_route_code = "";
                    temp.iteration = 0;
                    temp.remarks = hfAdministrationInstruction.Value;
                    temp.is_routine = 0;
                    temp.is_consumables = 0;
                    temp.compound_id = Guid.Empty;
                    temp.compound_name = "";
                    temp.origin_prescription_id = Guid.Empty;
                    temp.hope_arinvoice_id = 0;
                    temp.is_delete = 0;
                    temp.is_iter = bool.Parse(hfisIter.Value);

                    if (temp != null)
                    {
                        DataTable dttemp = Helper.ToDataTable(data);
                        if (dttemp.Select("item_name = '" + temp.item_name + "'").Count() == 0)
                        {
                            data.Add(temp);
                            DataTable dta = Helper.ToDataTable(data);
                            Session[Helper.Sessionadditionalpres + hfguidadditional.Value] = dta;
                            gvwAdditionalDrugs.DataSource = dta;
                            gvwAdditionalDrugs.DataBind();
                        }
                        else
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);
                        //txtSearchItem.Text = "";
                    }
                }
            }
            else
            {
                if (hftakedate.Value == "1")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Prescription Already Taken by Pharmacy');", true);
                }
                else
                {
                    int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
                    //LinkButton salesItemName = (LinkButton)gvw_frequent_drugs.Rows[selRowIndex].FindControl("frequentdrugs_name");
                    Label salesItemName_lbl = (Label)gvw_frequent_drugs.Rows[selRowIndex].FindControl("Label_frequentdrugs_name");
                    HiddenField salesitemid = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hffrequentdrugs_id");
                    HiddenField hfUomId = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfuom_id");
                    HiddenField hfUomCode = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfuom_code");
                    HiddenField hfDose = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfDose");
                    HiddenField hfDoseText = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfDoseText");
                    HiddenField hfDoseUomId = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfDoseUomId");
                    HiddenField hfAdministrationFrequencyId = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfAdministrationFrequencyId");
                    HiddenField hfAdministrationRouteId = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfAdministrationRouteId");
                    HiddenField hfAdministrationInstruction = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfAdministrationInstruction");
                    HiddenField hfisIter = (HiddenField)gvw_frequent_drugs.Rows[selRowIndex].FindControl("hfis_iter");


                    List<Prescription> data = GetRowList(1);
                    string qty = "0";
                    List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
                    if (orgsetting.Where(y => y.setting_name.ToLower() == "DEFAULT_QTY".ToLower()).Count() > 0)
                        qty = orgsetting.Find(y => y.setting_name.ToUpper() == "DEFAULT_QTY".ToUpper()).setting_value;


                    hfDose.Value = hfDose.Value.ToString().Replace(',', '.');
                    string[] tempqty = hfDose.Value.ToString().Split('.');
                    if (tempqty.Count() > 1)
                    {
                        if (tempqty[1].Length == 3)
                        {
                            if (tempqty[1] == "000")
                            {
                                hfDose.Value = decimal.Parse(tempqty[0]).ToString();
                            }
                            else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                            {
                                hfDose.Value = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                            }
                            else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                            {
                                hfDose.Value = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                            }
                        }
                    }

                    Prescription temp = new Prescription();
                    temp.prescription_id = Guid.Empty;
                    temp.prescription_no = "";
                    temp.item_id = long.Parse(salesitemid.Value);
                    temp.item_name = salesItemName_lbl.Text;
                    temp.quantity = qty;
                    temp.uom_id = long.Parse(hfUomId.Value);
                    temp.uom_code = hfUomCode.Value;
                    temp.frequency_id = long.Parse(hfAdministrationFrequencyId.Value);
                    temp.frequency_code = "";
                    temp.dosage_id = hfDose.Value;
                    temp.dose_text = hfDoseText.Value;
                    temp.dose_uom = "-";
                    temp.dose_uom_id = long.Parse(hfDoseUomId.Value);
                    temp.administration_route_id = long.Parse(hfAdministrationRouteId.Value);
                    temp.administration_route_code = "";
                    temp.iteration = 0;
                    temp.remarks = hfAdministrationInstruction.Value;
                    temp.is_routine = 0;
                    temp.is_consumables = 0;
                    temp.compound_id = Guid.Empty;
                    temp.compound_name = "";
                    temp.origin_prescription_id = Guid.Empty;
                    temp.hope_arinvoice_id = 0;
                    temp.is_delete = 0;
                    temp.is_iter = bool.Parse(hfisIter.Value);

                    if (temp != null)
                    {
                        DataTable dttemp = Helper.ToDataTable(data);
                        if (dttemp.Select("item_name = '" + temp.item_name + "'").Count() == 0)
                        {
                            data.Add(temp);
                            DataTable dta = Helper.ToDataTable(data);
                            Session[Helper.SessionDrugPres + hfguidadditional.Value] = dta;
                            gvw_drug.DataSource = dta;
                            gvw_drug.DataBind();

                            HyperLinkSaveOrderSet.Style.Add("display", "");
                        }
                        else
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);
                        //txtSearchItem.Text = "";
                    }
                }
            }

            upSaveAsOrderSet.Update();
            upFormularium.Update();
            UpdatePanelListPrescription.Update();
            UpdatePanelListPrescriptionAdd.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "frequentdrugs_onclick", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "frequentdrugs_onclick", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void itemselectedConsAdd_onclick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton salesItemName = (LinkButton)gvw_item_cons_additional.Rows[selRowIndex].FindControl("salesItemName");
            Label salesitemid = (Label)gvw_item_cons_additional.Rows[selRowIndex].FindControl("salesitemid");
            HiddenField hfUomId = (HiddenField)gvw_item_cons_additional.Rows[selRowIndex].FindControl("hfUomId");
            HiddenField hfUomCode = (HiddenField)gvw_item_cons_additional.Rows[selRowIndex].FindControl("hfUomCode");

            List<Prescription> data = GetRowList(7);
            string qty = "0";
            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Where(y => y.setting_name.ToLower() == "DEFAULT_QTY".ToLower()).Count() > 0)
                qty = orgsetting.Find(y => y.setting_name.ToUpper() == "DEFAULT_QTY".ToUpper()).setting_value;

            Prescription temp = new Prescription();
            temp.prescription_id = Guid.Empty;
            temp.prescription_no = "";
            temp.item_id = long.Parse(salesitemid.Text);
            temp.item_name = salesItemName.Text;
            temp.quantity = qty;
            temp.uom_id = long.Parse(hfUomId.Value);
            temp.uom_code = hfUomCode.Value;
            temp.frequency_id = 0;
            temp.frequency_code = "";
            temp.dosage_id = "0";
            temp.dose_text = "";
            temp.administration_route_id = 0;
            temp.administration_route_code = "";
            temp.iteration = 0;
            temp.remarks = "";
            temp.is_routine = 0;
            temp.is_consumables = 1;
            temp.compound_id = Guid.Empty;
            temp.compound_name = "";
            temp.origin_prescription_id = Guid.Empty;
            temp.hope_arinvoice_id = 0;
            temp.is_delete = 0;

            if (temp != null)
            {
                DataTable dttemp = Helper.ToDataTable(data);
                if (dttemp.Select("item_name = '" + temp.item_name + "'").Count() == 0)
                {
                    data.Add(temp);
                    DataTable dta = Helper.ToDataTable(data);
                    Session[Helper.SessionConsumablesListAdd + hfguidadditional.Value] = dta;
                    gvw_add_cons.DataSource = dta;
                    gvw_add_cons.DataBind();
                    txtSearchItemcons.Text = "";
                }
                else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);

            }
            //txtItemAdd.Text = salesItemName.Text;
            //txtItemId.Text = salesitemid.Text;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "close", "loseBoxConsAdd()", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "itemselectedConsAdd_onclick", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "itemselectedConsAdd_onclick", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            // Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void itemselectedCons_onclick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton salesItemName = (LinkButton)gvw_cons.Rows[selRowIndex].FindControl("salesItemName");
            Label salesitemid = (Label)gvw_cons.Rows[selRowIndex].FindControl("salesitemid");
            HiddenField hfUomId = (HiddenField)gvw_cons.Rows[selRowIndex].FindControl("hfUomId");
            HiddenField hfUomCode = (HiddenField)gvw_cons.Rows[selRowIndex].FindControl("hfUomCode");
            string qty = "0";
            List<Prescription> data = GetRowList(5);
            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Where(y => y.setting_name.ToLower() == "DEFAULT_QTY".ToLower()).Count() > 0)
                qty = orgsetting.Find(y => y.setting_name.ToUpper() == "DEFAULT_QTY".ToUpper()).setting_value;

            Prescription temp = new Prescription();
            temp.prescription_id = Guid.Empty;
            temp.prescription_no = "";
            temp.item_id = long.Parse(salesitemid.Text);
            temp.item_name = salesItemName.Text;
            temp.quantity = qty;
            temp.uom_id = long.Parse(hfUomId.Value);
            temp.uom_code = hfUomCode.Value;
            temp.frequency_id = 0;
            temp.frequency_code = "";
            temp.dosage_id = "0";
            temp.dose_text = "";
            temp.administration_route_id = 0;
            temp.administration_route_code = "";
            temp.iteration = 0;
            temp.remarks = "";
            temp.is_routine = 0;
            temp.is_consumables = 1;
            temp.compound_id = Guid.Empty;
            temp.compound_name = "";
            temp.origin_prescription_id = Guid.Empty;
            temp.hope_arinvoice_id = 0;
            temp.is_delete = 0;

            if (temp != null)
            {
                DataTable dttemp = Helper.ToDataTable(data);
                if (dttemp.Select("item_name = '" + temp.item_name + "'").Count() == 0)
                {
                    data.Add(temp);
                    DataTable dta = Helper.ToDataTable(data);
                    Session[Helper.SessionConsumablesList + hfguidadditional.Value] = dta;
                    gvw_consumables.DataSource = dta;
                    gvw_consumables.DataBind();
                    txtSearchItemcons.Text = "";
                }
                else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);

            }
            //txtItemAdd.Text = salesItemName.Text;
            //txtItemId.Text = salesitemid.Text;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "close", "loseBoxCons()", true);


            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "itemselectedCons_onclick", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "itemselectedCons_onclick", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void itemselectedAdditionalDrug_onclick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton salesItemName = (LinkButton)gvw_add_drugs.Rows[selRowIndex].FindControl("salesItemName");
            Label salesitemid = (Label)gvw_add_drugs.Rows[selRowIndex].FindControl("salesitemid");
            HiddenField hfUomId = (HiddenField)gvw_add_drugs.Rows[selRowIndex].FindControl("hfUomId");
            HiddenField hfUomCode = (HiddenField)gvw_add_drugs.Rows[selRowIndex].FindControl("hfUomCode");
            HiddenField hfDose = (HiddenField)gvw_add_drugs.Rows[selRowIndex].FindControl("hfDose");
            HiddenField hfDoseText = (HiddenField)gvw_add_drugs.Rows[selRowIndex].FindControl("hfDoseText");
            HiddenField hfDoseUomId = (HiddenField)gvw_add_drugs.Rows[selRowIndex].FindControl("hfDoseUomId");
            HiddenField hfAdministrationFrequencyId = (HiddenField)gvw_add_drugs.Rows[selRowIndex].FindControl("hfAdministrationFrequencyId");
            HiddenField hfAdministrationRouteId = (HiddenField)gvw_add_drugs.Rows[selRowIndex].FindControl("hfAdministrationRouteId");
            HiddenField hfAdministrationInstruction = (HiddenField)gvw_add_drugs.Rows[selRowIndex].FindControl("hfAdministrationInstruction");

            List<PatientRoutineMedication> dataroutine = (List<PatientRoutineMedication>)Session[Helper.Sessionroutinemedication + hfguidadditional.Value];

            List<Prescription> data = GetRowList(6);
            string qty = "0";
            hfDose.Value = hfDose.Value.ToString().Replace(',', '.');
            string[] tempqty = hfDose.Value.ToString().Split('.');
            if (tempqty[1].Length == 3)
            {
                if (tempqty[1] == "000")
                {
                    hfDose.Value = decimal.Parse(tempqty[0]).ToString();
                }
                else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                {
                    hfDose.Value = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                }
                else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                {
                    hfDose.Value = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                }
            }
            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Where(y => y.setting_name.ToLower() == "DEFAULT_QTY".ToLower()).Count() > 0)
                qty = orgsetting.Find(y => y.setting_name.ToUpper() == "DEFAULT_QTY".ToUpper()).setting_value;
            
            Prescription temp = new Prescription();
            temp.prescription_id = Guid.Empty;
            temp.prescription_no = "";
            temp.item_id = long.Parse(salesitemid.Text);
            temp.item_name = salesItemName.Text;
            temp.quantity = qty;
            temp.uom_id = long.Parse(hfUomId.Value);
            temp.uom_code = hfUomCode.Value;
            temp.frequency_id = long.Parse(hfAdministrationFrequencyId.Value);
            temp.frequency_code = "";
            temp.dosage_id = hfDose.Value;
            temp.dose_uom = "-";
            temp.dose_uom_id = long.Parse(hfDoseUomId.Value);
            temp.dose_text = hfDoseText.Value;
            temp.administration_route_id = long.Parse(hfAdministrationRouteId.Value);
            temp.administration_route_code = "";
            temp.iteration = 0;
            temp.remarks = hfAdministrationInstruction.Value;
            if (dataroutine != null)
            {
                if (dataroutine.Any(y => y.routine_sales_item_id == long.Parse(salesitemid.Text)))
                    temp.is_routine = 1;
                else
                    temp.is_routine = 0;
            }
            else
                temp.is_routine = 0;
            
            temp.is_consumables = 0;
            temp.compound_id = Guid.Empty;
            temp.compound_name = "";
            temp.origin_prescription_id = Guid.Empty;
            temp.hope_arinvoice_id = 0;
            temp.is_delete = 0;

            if (temp != null)
            {
                DataTable dttemp = Helper.ToDataTable(data);
                if (dttemp.Select("item_name = '" + temp.item_name + "'").Count() == 0)
                {
                    data.Add(temp);
                    DataTable dta = Helper.ToDataTable(data);
                    Session[Helper.Sessionadditionalpres + hfguidadditional.Value] = dta;
                    gvwAdditionalDrugs.DataSource = dta;
                    gvwAdditionalDrugs.DataBind();
                    txtSearchItemAddDrugs.Text = "";
                }
                else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);

            }
            DataTable dtItem = (DataTable)Session[Helper.SessionItemDrugPres];
            gvw_add_drugs.DataSource = (dtItem.Select("Formularium = '" + hfPayerType.Value + "'").CopyToDataTable()).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
            gvw_add_drugs.DataBind();
            //txtItemAdd.Text = salesItemName.Text;
            //txtItemId.Text = salesitemid.Text;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "close", "loseBoxDrugAdd()", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "itemselectedAdditionalDrug_onclick", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));


        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "itemselectedAdditionalDrug_onclick", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void itemselected_onclick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton salesItemName = (LinkButton)gvw_data.Rows[selRowIndex].FindControl("salesItemName");
            Label salesitemid = (Label)gvw_data.Rows[selRowIndex].FindControl("salesitemid");
            HiddenField hfUomId = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfUomId");
            HiddenField hfUomCode = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfUomCode");
            HiddenField hfDose = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfDose");
            HiddenField hfDoseText = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfDoseText");
            HiddenField hfDoseUomId = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfDoseUomId");
            HiddenField hfAdministrationFrequencyId = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfAdministrationFrequencyId");
            HiddenField hfAdministrationRouteId = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfAdministrationRouteId");
            HiddenField hfAdministrationInstruction = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfAdministrationInstruction");
            
            List<PatientRoutineMedication> dataroutine = (List<PatientRoutineMedication>)Session[Helper.Sessionroutinemedication + hfguidadditional.Value];

            List<Prescription> data = GetRowList(1);
            string qty = "0";
            //string a = hfDose.Value.ToString().Substring(0, 3);
            hfDose.Value = hfDose.Value.ToString().Replace(',', '.');
            string[] tempqty = hfDose.Value.ToString().Split('.');
            if (tempqty[1].Length == 3)
            {
                if (tempqty[1] == "000")
                {
                    hfDose.Value = decimal.Parse(tempqty[0]).ToString();
                }
                else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                {
                    hfDose.Value = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                }
                else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                {
                    hfDose.Value = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                }
            }
            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Where(y => y.setting_name.ToLower() == "DEFAULT_QTY".ToLower()).Count() > 0)
                qty = orgsetting.Find(y => y.setting_name.ToUpper() == "DEFAULT_QTY".ToUpper()).setting_value;

            Prescription temp = new Prescription();
            temp.prescription_id = Guid.Empty;
            temp.prescription_no = "";
            temp.item_id = long.Parse(salesitemid.Text);
            temp.item_name = salesItemName.Text;
            temp.quantity = qty;
            temp.uom_id = long.Parse(hfUomId.Value);
            temp.uom_code = hfUomCode.Value;
            temp.frequency_id = long.Parse(hfAdministrationFrequencyId.Value);
            temp.frequency_code = "";
            temp.dosage_id = hfDose.Value;
            temp.dose_uom = "-";
            temp.dose_uom_id = long.Parse(hfDoseUomId.Value);
            temp.dose_text = hfDoseText.Value;
            temp.administration_route_id = long.Parse(hfAdministrationRouteId.Value);
            temp.administration_route_code = "";
            temp.iteration = 0;
            temp.remarks = hfAdministrationInstruction.Value;
            if (dataroutine != null)
            {
                if (dataroutine.Any(y => y.routine_sales_item_id == long.Parse(salesitemid.Text)))
                    temp.is_routine = 1;
                else
                    temp.is_routine = 0;
            }
            else
                temp.is_routine = 0;


            temp.is_consumables = 0;
            temp.compound_id = Guid.Empty;
            temp.compound_name = "";
            temp.origin_prescription_id = Guid.Empty;
            temp.hope_arinvoice_id = 0;
            temp.is_delete = 0;

            if (temp != null)
            {
                DataTable dttemp = Helper.ToDataTable(data);
                if (dttemp.Select("item_name = '" + temp.item_name + "'").Count() == 0)
                {
                    data.Add(temp);
                    DataTable dta = Helper.ToDataTable(data);
                    Session[Helper.SessionDrugPres + hfguidadditional.Value] = dta;
                    gvw_drug.DataSource = dta;
                    gvw_drug.DataBind();
                    txtSearchItem.Text = "";
                }
                else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);

            }
            DataTable dtItem = (DataTable)Session[Helper.SessionItemDrugPres];
            gvw_data.DataSource = (dtItem.Select("Formularium = '" + hfPayerType.Value + "'").CopyToDataTable()).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
            gvw_data.DataBind();
            //txtItemAdd.Text = salesItemName.Text;
            //txtItemId.Text = salesitemid.Text;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "close", "loseBoxDrug()", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "itemselected_onclick", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "itemselected_onclick", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }

        //Log.Info(LogConfig.LogEnd());
    }
    
    //protected void itemselecteddetail_onclick(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        log.Info(LogLibrary.Logging("S", "itemselecteddetail_onclick", Helper.GetLoginUser(this.Parent.Page), ""));
    //        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
    //        LinkButton salesItemName = (LinkButton)gvw_item_detail.Rows[selRowIndex].FindControl("salesItemName_detail");
    //        Label salesitemid = (Label)gvw_item_detail.Rows[selRowIndex].FindControl("salesitemid_detail");
    //        HiddenField hfUomId = (HiddenField)gvw_item_detail.Rows[selRowIndex].FindControl("hfUomId_detail");
    //        HiddenField hfUomCode = (HiddenField)gvw_item_detail.Rows[selRowIndex].FindControl("hfUomCode_detail");

    //        List<Prescription> data = GetRowList(4);

    //        Prescription temp = new Prescription();
    //        temp.prescription_id = Guid.Empty;
    //        temp.prescription_no = "";
    //        temp.item_id = long.Parse(salesitemid.Text);
    //        temp.item_name = salesItemName.Text;
    //        temp.quantity = "0";
    //        temp.uom_id = long.Parse(hfUomId.Value);
    //        temp.uom_code = hfUomCode.Value;
    //        temp.frequency_id = 0;
    //        temp.frequency_code = "";
    //        temp.dosage_id = "0";
    //        temp.dose_text = "";
    //        temp.administration_route_id = 0;
    //        temp.administration_route_code = "";
    //        temp.iteration = 0;
    //        temp.remarks = "";
    //        temp.is_routine = 0;
    //        temp.is_consumables = 0;
    //        temp.compound_id = data[0].compound_id;
    //        temp.compound_name = data[0].compound_name;
    //        temp.origin_prescription_id = Guid.Empty;
    //        temp.hope_arinvoice_id = 0;
    //        temp.is_delete = 0;

    //        if (temp != null)
    //        {
    //            DataTable dttemp = Helper.ToDataTable(data);
    //            if (dttemp.Select("item_name = '" + temp.item_name + "'").Count() == 0)
    //            {
    //                data.Add(temp);
    //                DataTable dta = Helper.ToDataTable(data);
    //                Session[Helper.SessionCompPresHdn + hfguidadditional.Value] = dta;
    //                gvw_comp_detail.DataSource = dta;
    //                gvw_comp_detail.DataBind();
    //                find_detail.Text = "";
    //            }
    //            else
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);
    //        }
    //        //txtItemAdd.Text = salesItemName.Text;
    //        //txtItemId.Text = salesitemid.Text;
    //        log.Info(LogLibrary.Logging("E", "itemselecteddetail_onclick", Helper.GetLoginUser(this.Parent.Page), "Finish itemselecteddetail_onclick"));
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error(LogLibrary.Error("itemselecteddetail_onclick", Helper.GetLoginUser(this.Parent.Page), ex.InnerException.Message));
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
    //    }
    //}
    
    //protected void CompDetail_onClick(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        log.Info(LogLibrary.Logging("S", "CompDetail_onClick", Helper.GetLoginUser(this.Parent.Page), ""));
    //        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
    //        LinkButton compound_comp_name = (LinkButton)gvw_comp.Rows[selRowIndex].FindControl("compound_comp_name");
    //        DataTable dtcompdetail = (DataTable)Session[Helper.SessionCompDetailPres + hfguidadditional.Value];

    //        RepeaterDrugsNotAvailable.DataSource = null;
    //        RepeaterDrugsNotAvailable.DataBind();
    //        itemex.Visible = false;

    //        DataTable dtItem = (DataTable)Session[Helper.SessionItemDrugPres];
    //        gvw_item_detail.DataSource = dtItem.Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
    //        gvw_item_detail.DataBind();
    //        Session[Helper.SessionCompHeaderHdn + hfguidadditional.Value] = null;
    //        Session[Helper.SessionCompPresHdn + hfguidadditional.Value] = dtcompdetail.Select("compound_name = '" + compound_comp_name.Text + "' and item_id not in ('0')").CopyToDataTable();
    //        gvw_comp_detail.DataSource = dtcompdetail.Select("compound_name = '" + compound_comp_name.Text + "' and item_id not in ('0')").CopyToDataTable();
    //        gvw_comp_detail.DataBind();

    //        //DataTable dtdrugsdetail = ViewState["item"] as DataTable;
    //        //gvw_item_detail.DataSource = dtdrugsdetail;
    //        //gvw_item_detail.DataBind();

    //        if (hftakedate.Value == "1")
    //        {
    //            DisableRowList(4);
    //        }
    //        log.Info(LogLibrary.Logging("E", "CompDetail_onClick", Helper.GetLoginUser(this.Parent.Page), "Finish CompDetail_onClick"));
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "$('#modalcompdetail').modal({backdrop: 'static', keyboard: false});", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error(LogLibrary.Error("CompDetail_onClick", Helper.GetLoginUser(this.Parent.Page), ex.InnerException.Message));
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
    //    }

    //}

    //protected void btnSaveCompDetail_onClick(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        log.Info(LogLibrary.Logging("S", "UpdateRoutineMedication", Helper.GetLoginUser(this.Parent.Page), ""));
    //        List<Prescription> data = GetRowList(4);
    //        if (data.Count() >= 2)
    //        {
    //            string compname = data[0].compound_name;
    //            if (Session[Helper.SessionCompHeaderHdn + hfguidadditional.Value] != null)
    //            {
    //                DataTable dtheadercomp = (DataTable)Session[Helper.SessionCompHeaderHdn + hfguidadditional.Value];
    //                Session[Helper.SessionCompHeaderHdn + hfguidadditional.Value] = null;
    //                if (Session[Helper.SessionCompPres + hfguidadditional.Value] != null)
    //                {
    //                    DataTable dtheader = (DataTable)Session[Helper.SessionCompPres + hfguidadditional.Value];
    //                    dtheader.Merge(dtheadercomp);
    //                    Session[Helper.SessionCompPres + hfguidadditional.Value] = dtheader;
    //                    gvw_comp.DataSource = dtheader;
    //                    gvw_comp.DataBind();
    //                }
    //                else
    //                {
    //                    Session[Helper.SessionCompPres + hfguidadditional.Value] = dtheadercomp;
    //                    gvw_comp.DataSource = dtheadercomp;
    //                    gvw_comp.DataBind();
    //                }

    //            }
    //            if (Session[Helper.SessionCompDetailPres + hfguidadditional.Value] != null)
    //            {
    //                DataTable dtdetail = (DataTable)Session[Helper.SessionCompDetailPres + hfguidadditional.Value];
    //                if (dtdetail.Select("compound_name not in ('" + compname + "')").Count() > 0)
    //                {
    //                    DataTable dttempdetail = dtdetail.Select("compound_name not in ('" + compname + "')").CopyToDataTable();
    //                    //for (int i = dtdetail.Rows.Count - 1; i >= 0; i--)
    //                    //{
    //                    //    DataRow dr = dtdetail.Rows[i];
    //                    //    if (dr["compound_name"].ToString() == compname)
    //                    //    {
    //                    //        dr.Delete();
    //                    //        dr.AcceptChanges();
    //                    //    }
    //                    //}

    //                    //DataTable dttempdetail = Helper.ToDataTable(data);
    //                    dttempdetail.Merge(Helper.ToDataTable(data));
    //                    gvw_compdetail.DataSource = dttempdetail;
    //                    gvw_compdetail.DataBind();
    //                    Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = dttempdetail;
    //                    //dta.Merge(Helper.ToDataTable(dataheader));
    //                }
    //                else
    //                {
    //                    Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = Helper.ToDataTable(data);
    //                    gvw_compdetail.DataSource = Helper.ToDataTable(data);
    //                    gvw_compdetail.DataBind();
    //                }
    //                //Session["compdetail"] = dtdetail;

    //            }
    //            else
    //            {
    //                Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = Session[Helper.SessionCompPresHdn + hfguidadditional.Value];
    //                gvw_compdetail.DataSource = Session[Helper.SessionCompPresHdn + hfguidadditional.Value];
    //                gvw_compdetail.DataBind();
    //            }
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "$('#modalcompdetail').modal('hide');", true);
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Compound should contains minimum 2 items');", true);
    //        }
    //        log.Info(LogLibrary.Logging("E", "btnSaveCompDetail_onClick", Helper.GetLoginUser(this.Parent.Page), "Finish btnSaveCompDetail_onClick"));
    //    }
    //    catch (Exception ex)
    //    {
    //        string a = ex.Message;
    //        log.Error(LogLibrary.Error("btnSaveCompDetail_onClick", Helper.GetLoginUser(this.Parent.Page), ex.InnerException.Message));
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
    //    }
    //}

    protected void btnDeleteConsAdd_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            List<Prescription> datadetail = GetRowList(7);
            DataTable dt = Helper.ToDataTable(datadetail);
            //DataTable dt = Session["presdrug"] as DataTable;
            dt.Rows[selRowIndex].SetField("is_delete", 1);
            //dt.Rows[selRowIndex].Delete();
            if (dt.Select("is_delete = 0").Count() > 0)
            {
                Session[Helper.SessionConsumablesListAdd + hfguidadditional.Value] = dt.Select("is_delete = 0").CopyToDataTable();
                gvw_add_cons.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
                gvw_add_cons.DataBind();
            }
            else
            {
                Session[Helper.SessionConsumablesListAdd + hfguidadditional.Value] = null;
                gvw_add_cons.DataSource = null;
                gvw_add_cons.DataBind();
            }


            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnDeleteConsAdd_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnDeleteConsAdd_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDeleteCons_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            List<Prescription> datadetail = GetRowList(5);
            DataTable dt = Helper.ToDataTable(datadetail);
            //DataTable dt = Session["presdrug"] as DataTable;
            dt.Rows[selRowIndex].SetField("is_delete", 1);
            //dt.Rows[selRowIndex].Delete();
            if (dt.Select("is_delete = 0").Count() > 0)
            {
                Session[Helper.SessionConsumablesList + hfguidadditional.Value] = dt.Select("is_delete = 0").CopyToDataTable();
                gvw_consumables.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
                gvw_consumables.DataBind();
            }
            else
            {
                Session[Helper.SessionConsumablesList + hfguidadditional.Value] = null;
                gvw_consumables.DataSource = null;
                gvw_consumables.DataBind();
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnDeleteCons_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnDeleteCons_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDeleteAdditionalDrugs_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            List<Prescription> datadetail = GetRowList(6);
            DataTable dt = Helper.ToDataTable(datadetail);
            //DataTable dt = Session["presdrug"] as DataTable;
            dt.Rows[selRowIndex].SetField("is_delete", 1);
            //dt.Rows[selRowIndex].Delete();
            if (dt.Select("is_delete = 0").Count() > 0)
            {
                Session[Helper.Sessionadditionalpres + hfguidadditional.Value] = dt.Select("is_delete = 0").CopyToDataTable();
                gvwAdditionalDrugs.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
                gvwAdditionalDrugs.DataBind();
            }
            else
            {
                Session[Helper.Sessionadditionalpres + hfguidadditional.Value] = null;
                gvwAdditionalDrugs.DataSource = null;
                gvwAdditionalDrugs.DataBind();
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnDeleteAdditionalDrugs_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnDeleteAdditionalDrugs_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            List<Prescription> datadetail = GetRowList(1);
            DataTable dt = Helper.ToDataTable(datadetail);
            //DataTable dt = Session["presdrug"] as DataTable;
            dt.Rows[selRowIndex].SetField("is_delete", 1);
            //dt.Rows[selRowIndex].Delete();
            if (dt.Select("is_delete = 0").Count() > 0)
            {
                Session[Helper.SessionDrugPres + hfguidadditional.Value] = dt.Select("is_delete = 0").CopyToDataTable();
                gvw_drug.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
                gvw_drug.DataBind();

                HyperLinkSaveOrderSet.Style.Add("display", "");
            }
            else
            {
                Session[Helper.SessionDrugPres + hfguidadditional.Value] = null;
                gvw_drug.DataSource = null;
                gvw_drug.DataBind();

                HyperLinkSaveOrderSet.Style.Add("display", "none");
            }

            upSaveAsOrderSet.Update();
            upFormularium.Update();


            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnDelete_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnDelete_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    //protected void btnDeleteComp_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        log.Info(LogLibrary.Logging("S", "btnDeleteComp_Click", Helper.GetLoginUser(this.Parent.Page), ""));
    //        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
    //        HiddenField presid = (HiddenField)gvw_comp_detail.Rows[selRowIndex].FindControl("prescription_compdtl_id");
    //        List<Prescription> datadetail = GetRowList(4);
    //        DataTable dt = Helper.ToDataTable(datadetail);
    //        if (dt.Select("is_delete = 0").Count() > 2)
    //        {
    //            if (Guid.Parse(presid.Value) == Guid.Empty)
    //            {
    //                dt.Rows[selRowIndex].Delete();
    //            }
    //            else
    //            {
    //                dt.Rows[selRowIndex].SetField("is_delete", 1);
    //            }
    //            //dt.Rows[selRowIndex].Delete();

    //            Session[Helper.SessionCompPresHdn + hfguidadditional.Value] = dt.Select("is_delete = 0").CopyToDataTable();
    //            gvw_comp_detail.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
    //            gvw_comp_detail.DataBind();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Compound should contains minimum 2 items');", true);
    //        }
    //        log.Info(LogLibrary.Logging("E", "btnDeleteComp_Click", Helper.GetLoginUser(this.Parent.Page), "Finish btnDeleteComp_Click"));
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error(LogLibrary.Error("btnDeleteComp_Click", Helper.GetLoginUser(this.Parent.Page), ex.InnerException.Message));
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
    //    }
    //}

    //protected void btnDeleteheadercomp_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        log.Info(LogLibrary.Logging("S", "btnDeleteheadercomp_Click", Helper.GetLoginUser(this.Parent.Page), ""));

    //        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
    //        HiddenField presid = (HiddenField)gvw_comp.Rows[selRowIndex].FindControl("prescription_comp_id");
    //        LinkButton compname = (LinkButton)gvw_comp.Rows[selRowIndex].FindControl("compound_comp_name");
    //        List<Prescription> dataheader = GetRowList(2);
    //        List<Prescription> datadetail = GetRowList(3);
    //        DataTable dtheader = Helper.ToDataTable(dataheader);
    //        DataTable dtdetail = Helper.ToDataTable(datadetail);
    //        if (dtheader.Select("compound_name not in('" + compname.Text + "')").Count() > 0)
    //        {
    //            DataTable dt = dtheader.Select("compound_name not in('" + compname.Text + "')").CopyToDataTable();
    //            Session[Helper.SessionCompPres + hfguidadditional.Value] = dt;
    //            gvw_comp.DataSource = dt;
    //            gvw_comp.DataBind();

    //            DataTable dtdetailcomp = dtdetail.Select("compound_name not in('" + compname.Text + "')").CopyToDataTable();
    //            Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = dtdetailcomp;
    //            gvw_compdetail.DataSource = dtdetailcomp;
    //            gvw_compdetail.DataBind();
    //        }
    //        else
    //        {
    //            Session[Helper.SessionCompPres + hfguidadditional.Value] = null;
    //            gvw_comp.DataSource = null;
    //            gvw_comp.DataBind();

    //            Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = null;
    //            gvw_compdetail.DataSource = null;
    //            gvw_compdetail.DataBind();

    //            //divcompound.Visible = false;
    //            //divcompound.Style.Add("display", "none");
    //        }
    //        log.Info(LogLibrary.Logging("E", "btnDeleteheadercomp_Click", Helper.GetLoginUser(this.Parent.Page), "Finish btnDeleteheadercomp_Click"));
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error(LogLibrary.Error("btnDeleteheadercomp_Click", Helper.GetLoginUser(this.Parent.Page), ex.InnerException.Message));
    //    }
        
    //}

    protected void checkkey(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        try
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalexdrugs", "alert('Order Set Compound already in use');", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "checkkey", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "checkkey", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnPanelSet_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        try
        {
            
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            //LinkButton setname = (LinkButton)gvw_panelset.Rows[selRowIndex].FindControl("setname_lab");
            Label setname_lbl = (Label)gvw_panelset.Rows[selRowIndex].FindControl("Label_setname");
            var getlabset = clsOrderSet.getOrdersetLab(setname_lbl.Text, Helper.organizationId, Helper.doctorid);
            var getJsonlabset = JsonConvert.DeserializeObject<ResultCpoeTrans>(getlabset.Result.ToString());

            List<CpoeTrans> listexcludetrans = new List<CpoeTrans>();
            List<CpoeTrans> listnotexist = new List<CpoeTrans>();
            //List<CpoeTrans> listtempcpoetrans = new List<CpoeTrans>();
            List<CpoeTrans> listtempcpoetrans;
            if (Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] == null)
            {
                listtempcpoetrans = new List<CpoeTrans>();
            }
            else
            {
                listtempcpoetrans = new List<CpoeTrans>();
                listtempcpoetrans = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];
            }

            if (listtempcpoetrans != null)
            {
                foreach (CpoeTrans x in getJsonlabset.list)
                {
                    if (listtempcpoetrans.Any(y => y.name == x.name))
                    {
                        listexcludetrans.Add(x);
                    }
                    else if (listtempcpoetrans.Any(y => x.ischeck == 0))
                    {
                        listnotexist.Add(x);
                    }
                    else
                        listtempcpoetrans.Add(x);
                }
            }
            else
            {
                foreach (CpoeTrans x in getJsonlabset.list)
                {
                    listtempcpoetrans.Add(x);
                }
            }

            Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = listtempcpoetrans;

            listchecked = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];
            if (listchecked != null)
            {
                labempty.Style.Add("display", "none");
                linklabbutton.Style.Add("display", "none");
                btnEditLab.Visible = true;
                btnResetLab.Visible = true;
                if (Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0").Count() > 0)
                {
                    DataTable dt = Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0").CopyToDataTable();
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else
                {
                    labempty.Style.Add("display", "");
                    linklabbutton.Style.Add("display", "");
                    btnEditLab.Visible = false;
                    btnResetLab.Visible = false;
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
                List<CpoeMapping> tempMap = new List<CpoeMapping>();
                tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
                //StdClinicControl.GetMappingClinicLab(tempMap, hfguidadditional.Value);
                stdmicro.GetMappingMicroLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdcito.GetMappingCitoLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdclinic.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdanatomi.GetMappingAnatomiLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdpanel.GetMappingPanelLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdmdc.GetMappingMDC(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            }

            if (listexcludetrans.Count() > 0 || listnotexist.Count() > 0)
            {
                if (listexcludetrans.Count() > 0)
                {
                    DataTable dtexclude = Helper.ToDataTable(listexcludetrans);
                    rptExist.DataSource = dtexclude;
                    rptExist.DataBind();
                    lblExist.Visible = true;
                }
                else
                {
                    rptExist.DataSource = null;
                    rptExist.DataBind();
                    lblExist.Visible = false;
                }
                if (listnotexist.Count() > 0)
                {
                    DataTable dtnotexist = Helper.ToDataTable(listnotexist);
                    rptNotExist.DataSource = dtnotexist;
                    rptNotExist.DataBind();
                    lblNotExist.Visible = true;
                }
                else
                {
                    rptNotExist.DataSource = null;
                    rptNotExist.DataBind();
                    lblNotExist.Visible = false;
                }

                updatepanelexistlabrad.Update();

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalexdrugs", "$('#modallab').modal();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modallab", "$('#modallab').modal();", true);
            }

            UpdatePanelDivLab.Update();
            UP_ContainerLab.Update();

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "labpanelonclick", "labpanelonclick();", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnPanelSet_onClick", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnPanelSet_onClick", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnLabSet_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        try
        {

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            //LinkButton setname = (LinkButton)gvw_labset.Rows[selRowIndex].FindControl("setname_lab");
            Label setname_lbl = (Label)gvw_labset.Rows[selRowIndex].FindControl("Label_setname");
            var getlabset = clsOrderSet.getOrdersetLab(setname_lbl.Text, Helper.organizationId, Helper.doctorid);
            var getJsonlabset = JsonConvert.DeserializeObject<ResultCpoeTrans>(getlabset.Result.ToString());

            List<CpoeTrans> listexcludetrans = new List<CpoeTrans>();
            List<CpoeTrans> listnotexist = new List<CpoeTrans>();
            //List<CpoeTrans> listtempcpoetrans = new List<CpoeTrans>();
            List<CpoeTrans> listtempcpoetrans;

            if (chk_isLabsetFO.Checked == true)
			{
				foreach  ( CpoeTrans x in getJsonlabset.list)
				{
                    x.IsFutureOrder = true;

                    x.FutureOrderDate = dp_labFutureOrder.Text != "" ? DateTime.Parse(dp_labFutureOrder.Text) : DateTime.Now; 
                }
                HF_LabsetFO.Value = "true";
			}
			else
			{
                foreach (CpoeTrans x in getJsonlabset.list)
                {
                    x.IsFutureOrder = false;
                    x.FutureOrderDate = DateTime.Now;
                }
                HF_LabsetFO.Value = "false";
			}

            var LabsetFOValue = bool.Parse(HF_LabsetFO.Value);

            if (Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] == null)
            {
                listtempcpoetrans = new List<CpoeTrans>();
            }
            else
            {
                listtempcpoetrans = new List<CpoeTrans>();
                listtempcpoetrans = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];
            }

            if (listtempcpoetrans != null)
            {
                foreach (CpoeTrans x in getJsonlabset.list)
                {
                    if (listtempcpoetrans.Any(y => y.name == x.name && y.isdelete == 0 && y.IsFutureOrder == LabsetFOValue))
                    {
                        listexcludetrans.Add(x);
                    }
                    else if (listtempcpoetrans.Any(y => y.name == x.name && y.isdelete == 1 && y.IsFutureOrder == LabsetFOValue))
                    {
                        listtempcpoetrans.FirstOrDefault(z => z.name == x.name).isdelete = 0;
                    }
                    else if (listtempcpoetrans.Any(y => x.ischeck == 0 && y.IsFutureOrder == LabsetFOValue))
                    {
                        listnotexist.Add(x);
                    }
                    else
                        listtempcpoetrans.Add(x);
                }
            }
            else
            {
                foreach (CpoeTrans x in getJsonlabset.list)
                {
                    listtempcpoetrans.Add(x);
                }
            }

            Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = listtempcpoetrans;

            listchecked = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];
            if (listchecked != null)
            {
                if(chk_isLabsetFO.Checked == true)
				{
                    labempty_FutureOrder.Style.Add("display", "none");
                    linklabbutton_FutureOrder.Style.Add("display", "none");
                    btnEditLab_FutureOrder.Visible = true;
                    btnResetLab_FutureOrder.Visible = true;
                    if (Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0 AND IsFutureOrder = true").Count() > 0)
                    {
                        DataTable dt = Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0 AND IsFutureOrder = true").CopyToDataTable();
                        Repeater1_FutureOrder.DataSource = dt;
                        Repeater1_FutureOrder.DataBind();
                    }
                    else
                    {
                        labempty_FutureOrder.Style.Add("display", "");
                        linklabbutton_FutureOrder.Style.Add("display", "");
                        btnEditLab_FutureOrder.Visible = false;
                        btnResetLab_FutureOrder.Visible = false;
                        Repeater1_FutureOrder.DataSource = null;
                        Repeater1_FutureOrder.DataBind();
                    }
                    List<CpoeMapping> tempMap = new List<CpoeMapping>();
                    tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
                    //StdClinicControl.GetMappingClinicLab(tempMap, hfguidadditional.Value);
                    stdmicro.GetMappingMicroLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdcito.GetMappingCitoLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdclinic.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdanatomi.GetMappingAnatomiLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdpanel.GetMappingPanelLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdmdc.GetMappingMDC(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                }
				else
				{
                labempty.Style.Add("display", "none");
                linklabbutton.Style.Add("display", "none");
                btnEditLab.Visible = true;
                btnResetLab.Visible = true;
                    if (Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0 AND IsFutureOrder = false").Count() > 0)
                {
                        DataTable dt = Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0 AND IsFutureOrder = false").CopyToDataTable();
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else
                {
                    labempty.Style.Add("display", "");
                    linklabbutton.Style.Add("display", "");
                    btnEditLab.Visible = false;
                    btnResetLab.Visible = false;
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
                List<CpoeMapping> tempMap = new List<CpoeMapping>();
                tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
                //StdClinicControl.GetMappingClinicLab(tempMap, hfguidadditional.Value);
                    stdmicro.GetMappingMicroLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdcito.GetMappingCitoLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdclinic.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdanatomi.GetMappingAnatomiLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdpanel.GetMappingPanelLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdmdc.GetMappingMDC(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                }


            }

            if (listexcludetrans.Count() > 0 || listnotexist.Count() > 0)
            {
                if (listexcludetrans.Count() > 0)
                {
                    DataTable dtexclude = Helper.ToDataTable(listexcludetrans);
                    rptExist.DataSource = dtexclude;
                    rptExist.DataBind();
                    lblExist.Visible = true;
                }
                else
                {
                    rptExist.DataSource = null;
                    rptExist.DataBind();
                    lblExist.Visible = false;
                }
                if (listnotexist.Count() > 0)
                {
                    DataTable dtnotexist = Helper.ToDataTable(listnotexist);
                    rptNotExist.DataSource = dtnotexist;
                    rptNotExist.DataBind();
                    lblNotExist.Visible = true;
                }
                else
                {
                    rptNotExist.DataSource = null;
                    rptNotExist.DataBind();
                    lblNotExist.Visible = false;
                }

                updatepanelexistlabrad.Update();

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalexdrugs", "$('#modallab').modal();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modallab", "$('#modallab').modal();", true);
                
            }

            UpdatePanelDivLab.Update();
            UpdatePanelDivLab_FutureOrder.Update();
            UP_ContainerLab.Update();


            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnLabSet_onClick", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnLabSet_onClick", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnResetLab_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            Button buttonReset = (Button)sender;
            string buttonResetID = buttonReset.ID;
            var listremove = new List<CpoeTrans>();

            List<CpoeTrans> listlab = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];
            var listcheck = (
                    from a in listlab
                    where ((a.type == "ClinicLab" || a.type == "CitoLab" || a.type == "MicroLab" || a.type == "PatologiLab" || a.type == "PanelLab" || a.type == "MDCLab")) //&& a.isnew == 0
                    select a
                ).Distinct().ToList();

            var listcheckNotFO = listcheck.Where(x => x.IsFutureOrder == false).Distinct().ToList();
            var listcheckFO = listcheck.Where(x => x.IsFutureOrder == true).Distinct().ToList();

            if (buttonResetID == "btnResetLab")
            {
                HF_FlagFutureOrder.Value = "false";
                foreach (var x in listcheckNotFO.Where(x => x.type == "ClinicLab" || x.type == "CitoLab" || x.type == "MicroLab" || x.type == "PatologiLab" || x.type == "PanelLab" || x.type == "MDCLab"))
            {
                if (x.type == "ClinicLab" || x.type == "CitoLab" || x.type == "MicroLab" || x.type == "PatologiLab" || x.type == "PanelLab" || x.type == "MDCLab")
                {
                    if (x.issubmit == 0)
                    {
                        if (x.isnew == 0)
                        {
                            x.isdelete = 1;
                        }
                        else if (x.isnew == 1)
                        {
                            listremove.Add(x);
                        }

                        //x.isdelete = 1;
                    }
                }
            }
            foreach (var r in listremove)
            {
                listcheck.Remove(r);
                    listcheckNotFO.Remove(r);
            }

            Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = listcheck;
            //stdclinic.resetlist();

            //var listcheck = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];

            List<CpoeMapping> tempMap = new List<CpoeMapping>();
            tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
            //StdClinicControl.GetMappingClinicLab(tempMap, hfguidadditional.Value);
                stdmicro.GetMappingMicroLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdcito.GetMappingCitoLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdclinic.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdanatomi.GetMappingAnatomiLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdpanel.GetMappingPanelLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdmdc.GetMappingMDC(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);

                if (Helper.ToDataTable(listcheckNotFO).Select("isdelete = 0").Count() > 0)
            {

                    Repeater1.DataSource = Helper.ToDataTable(listcheckNotFO).Select("isdelete = 0").CopyToDataTable();
                Repeater1.DataBind();


            }
            else
            {
                labempty.Style.Add("display", "");
                linklabbutton.Style.Add("display", "");
                btnEditLab.Visible = false;
                btnResetLab.Visible = false;

                Repeater1.DataSource = null;
                Repeater1.DataBind();
                    HyperLinkSaveAsLab.Style.Add("display", "none");

                }
                UpdatePanelDivLab.Update();
                UP_ContainerLab.Update();

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "setmandatory", "isMandatoryField();", true);
            }
            else if (buttonResetID == "btnResetLab_FutureOrder")
            {
                HF_FlagFutureOrder.Value = "true";
                foreach (var x in listcheckFO.Where(x => x.type == "ClinicLab" || x.type == "CitoLab" || x.type == "MicroLab" || x.type == "PatologiLab" || x.type == "PanelLab" || x.type == "MDCLab"))
                {
                    if (x.type == "ClinicLab" || x.type == "CitoLab" || x.type == "MicroLab" || x.type == "PatologiLab" || x.type == "PanelLab" || x.type == "MDCLab")
                    {
                        if (x.issubmit == 0)
                        {
                            if (x.isnew == 0)
                            {
                                x.isdelete = 1;
                            }
                            else if (x.isnew == 1)
                            {
                                listremove.Add(x);
                            }

                            //x.isdelete = 1;
                        }
                    }
                }
                foreach (var r in listremove)
                {
                    listcheck.Remove(r);
                    listcheckFO.Remove(r);
                }

                Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = listcheck;
                //stdclinic.resetlist();

                //var listcheck = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];

                List<CpoeMapping> tempMap = new List<CpoeMapping>();
                tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
                //StdClinicControl.GetMappingClinicLab(tempMap, hfguidadditional.Value);
                stdmicro.GetMappingMicroLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdcito.GetMappingCitoLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdclinic.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdanatomi.GetMappingAnatomiLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdpanel.GetMappingPanelLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                stdmdc.GetMappingMDC(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);

                if (Helper.ToDataTable(listcheckFO).Select("isdelete = 0").Count() > 0)
                {

                    Repeater1_FutureOrder.DataSource = Helper.ToDataTable(listcheckFO).Select("isdelete = 0").CopyToDataTable();
                    Repeater1_FutureOrder.DataBind();


            }
                else
                {
                    labempty_FutureOrder.Style.Add("display", "");
                    linklabbutton_FutureOrder.Style.Add("display", "");
                    btnEditLab_FutureOrder.Visible = false;
                    btnResetLab_FutureOrder.Visible = false;

                    Repeater1_FutureOrder.DataSource = null;
                    Repeater1_FutureOrder.DataBind();
                    HyperLinkSaveAsLab_FutureOrder.Style.Add("display", "none");

                }
                UpdatePanelDivLab_FutureOrder.Update();
            UP_ContainerLab.Update();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "setmandatory", "isMandatoryField();", true);
            }

            



            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnResetLab_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnResetLab_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnResetRad_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            var buttonResetID = ((Button)sender).ID;
            var listremove = new List<CpoeTrans>();

            List<CpoeTrans> listrad = (List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value];
            var listcheck = (
                    from a in listrad
                    where (a.type == "MRI1" || a.type == "MRI3" || a.type == "Radiology" || a.type == "USG" || a.type == "CT") //&& a.isnew == 0
                    select a
                ).Distinct().ToList();

            var flagFO = bool.Parse(HF_FlagFutureOrderRad.Value);
            if(buttonResetID == "btnResetRad")
			{
                HF_FlagFutureOrderRad.Value = "false";
                foreach (var x in listrad.Where(x => x.IsFutureOrder == false && (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT")))
            {
                    if (x.IsFutureOrder == false && (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT"))
                {
                    if (x.issubmit == 0)
                    {
                        if (x.isnew == 0)
                        {
                            x.isdelete = 1;
                        }
                        else if (x.isnew == 1)
                        {
                            listremove.Add(x);
                        }

                        //x.isdelete = 1;
                    }
                }
            }

            foreach (var r in listremove)
            {
                listcheck.Remove(r);
            }

            Session[Helper.Sessionradcheck + hfguidadditional.Value] = listcheck;
            List<CpoeMapping> tempMap = new List<CpoeMapping>();
            tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
            //stdclinic.GetMappingClinicLab(tempMap, hfguidadditional.Value);
            //StdClinicControl.GetMappingClinicLab(tempMap, hfguidadditional.Value);
            //stdmicro.GetMappingMicroLab(tempMap, hfguidadditional.Value);
            //stdcito.GetMappingCitoLab(tempMap, hfguidadditional.Value);
                stdxray.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                stdusg.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                stdctrad.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                stdmrihalf.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                stdmrifull.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);

                if (Helper.ToDataTable(listcheck).Select("isdelete = 0 AND IsFutureOrder = false").Count() > 0)
            {
                    rptRadiology.DataSource = Helper.ToDataTable(listcheck).Select("isdelete = 0 AND IsFutureOrder = false").CopyToDataTable();
                rptRadiology.DataBind();
                    if (listcheck.Where(y => y.iscito == 1 && y.isdelete == 0 && y.IsFutureOrder == flagFO).Count() > 0)
                {
                    chkcitorad.Checked = true;
                }
                else
                    chkcitorad.Checked = false;
            }
            else
            {
                radempty.Style.Add("display", "");
                linkradbutton.Style.Add("display", "");
                btnEditRad.Visible = false;
                btnResetRad.Visible = false;
                divcitorad.Visible = false;
                chkcitorad.Checked = false;
                rptRadiology.DataSource = null;
                rptRadiology.DataBind();

            }
            }
            else if(buttonResetID == "btnResetRad_FutureOrder")
			{
                HF_FlagFutureOrderRad.Value = "true";
                foreach (var x in listrad.Where(x => x.IsFutureOrder == true && (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT")))
                {
                    if (x.IsFutureOrder == true && (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT"))
                    {
                        if (x.issubmit == 0)
                        {
                            if (x.isnew == 0)
                            {
                                x.isdelete = 1;
                            }
                            else if (x.isnew == 1)
                            {
                                listremove.Add(x);
                            }

                            //x.isdelete = 1;
                        }
                    }
                }

                foreach (var r in listremove)
                {
                    listcheck.Remove(r);
                }

                Session[Helper.Sessionradcheck + hfguidadditional.Value] = listcheck;
                List<CpoeMapping> tempMap = new List<CpoeMapping>();
                tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
                //stdclinic.GetMappingClinicLab(tempMap, hfguidadditional.Value);
                //StdClinicControl.GetMappingClinicLab(tempMap, hfguidadditional.Value);
                //stdmicro.GetMappingMicroLab(tempMap, hfguidadditional.Value);
                //stdcito.GetMappingCitoLab(tempMap, hfguidadditional.Value);
                stdxray.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                stdusg.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                stdctrad.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                stdmrihalf.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                stdmrifull.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);

                if (Helper.ToDataTable(listcheck).Select("isdelete = 0 AND IsFutureOrder = true").Count() > 0)
                {
                    rptRadiology_FutureOrder.DataSource = Helper.ToDataTable(listcheck).Select("isdelete = 0 AND IsFutureOrder = true").CopyToDataTable();
                    rptRadiology_FutureOrder.DataBind();
                    if (listcheck.Where(y => y.iscito == 1 && y.isdelete == 0 && y.IsFutureOrder == flagFO).Count() > 0)
                    {
                        chkcitorad_FutureOrder.Checked = true;
                    }
                    else
                        chkcitorad_FutureOrder.Checked = false;
                }
                else
                {
                    radempty_FutureOrder.Style.Add("display", "");
                    linkradbutton_FutureOrder.Style.Add("display", "");
                    btnEditRad_FutureOrder.Visible = false;
                    btnResetRad_FutureOrder.Visible = false;
                    divcitorad_FutureOrder.Visible = false;
                    chkcitorad_FutureOrder.Checked = false;
                    rptRadiology_FutureOrder.DataSource = null;
                    rptRadiology_FutureOrder.DataBind();

                }
            }


            UpdatePanelDivRad.Update();
            UpdatePanelDivRad_FutureOrder.Update();
            UP_ContainerRad.Update();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "setmandatory", "isMandatoryField();", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnResetRad_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnResetRad_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void citorad_onclick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        List<CpoeTrans> listrad = (List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value];
        if (listrad.Count > 0)
        {
            if (chkcitorad.Checked)
            {

                foreach (var x in listrad.Where(x => x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT"))
                {
                    if (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT")
                    {
                        x.iscito = 1;
                    }
                }
                Session[Helper.Sessionradcheck + hfguidadditional.Value] = listrad;
            }
            else
            {

                foreach (var x in listrad.Where(x => x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT"))
                {
                    if (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT")
                    {
                        x.iscito = 0;
                    }
                }
                Session[Helper.Sessionradcheck + hfguidadditional.Value] = listrad;
            }
        }
        else
        {
            if (chkcitorad.Checked)
            {

                foreach (var x in listrad.Where(x => x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT"))
                {
                    if (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT")
                    {
                        x.iscito = 1;
                        x.isnew = 1;
                    }
                }
                Session[Helper.Sessionradcheck + hfguidadditional.Value] = listrad;
            }
            else
            {

                foreach (var x in listrad.Where(x => x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT"))
                {
                    if (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT")
                    {
                        x.iscito = 0;
                        x.isnew = 1;
                    }
                }
                Session[Helper.Sessionradcheck + hfguidadditional.Value] = listrad;
            }
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "citorad_onclick", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void  orderset_onclick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            bool is_itemissue = false;
            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Find(y => y.setting_name.ToUpper() == "SEND_DATA_ITEM_ISSUE".ToUpper()).setting_value == "TRUE")
            {
                is_itemissue = true;
            }

            if (hfverifydate.Value == "1")
            {
                if (hfadditional_take_date.Value == "1")
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Additional Prescription Already Taken by Pharmacy');", true);
                    ShowToastr("Additional Prescription already taken by Pharmacy.", "Info!", "info");
                }
                else
                {
                    string separator = "-(R)";

                    //List<PrescriptionDrug> listprescriptiondrugtemp, listprescriptioncomptemp,listdrugex = new List<PrescriptionDrug>();
                    List<Prescription> listprescriptiondrugtemp, listprescriptioncomptemp, listdrugex = new List<Prescription>();
                    List<Prescription> listdrugnotavail = new List<Prescription>();
                    List<Prescription> listdruguomcheck = new List<Prescription>();
                    int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
                    //LinkButton setname = (LinkButton)gvw_orderset.Rows[selRowIndex].FindControl("setname");
                    Label setname_lbl = (Label)gvw_orderset.Rows[selRowIndex].FindControl("Label_setname");


                    if (setname_lbl.Text.Contains("-(R)"))
                    {
                        //goto SKIP_ORDERRACIKAN_ADD; 

                        DataTable RacikanHeader_add = (DataTable)Session[Helper.SessionRacikanHeaderAdd + hfguidadditional.Value];
                        DataTable RacikanDetail_add = (DataTable)Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value];

                        if (RacikanHeader_add == null)
                        {
                            RacikanHeader_add = new DataTable();
                            RacikanHeader_add.Columns.Add("prescription_compound_header_id");
                            RacikanHeader_add.Columns.Add("compound_name");
                            RacikanHeader_add.Columns.Add("quantity");
                            RacikanHeader_add.Columns.Add("uom_id");
                            RacikanHeader_add.Columns.Add("uom_code");
                            RacikanHeader_add.Columns.Add("dose");
                            RacikanHeader_add.Columns.Add("dose_uom_id");
                            RacikanHeader_add.Columns.Add("dose_uom");
                            RacikanHeader_add.Columns.Add("administration_frequency_id");
                            RacikanHeader_add.Columns.Add("frequency_code");
                            RacikanHeader_add.Columns.Add("administration_route_id");
                            RacikanHeader_add.Columns.Add("administration_route_code");
                            RacikanHeader_add.Columns.Add("administration_instruction");
                            RacikanHeader_add.Columns.Add("compound_note");
                            RacikanHeader_add.Columns.Add("iter");
                            RacikanHeader_add.Columns.Add("is_additional");
                            RacikanHeader_add.Columns.Add("item_sequence");
                            RacikanHeader_add.Columns.Add("dose_text");
                            RacikanHeader_add.Columns.Add("IsDoseText");

                            RacikanDetail_add = new DataTable();
                            RacikanDetail_add.Columns.Add("prescription_compound_detail_id");
                            RacikanDetail_add.Columns.Add("prescription_compound_header_id");
                            RacikanDetail_add.Columns.Add("item_id");
                            RacikanDetail_add.Columns.Add("item_name");
                            RacikanDetail_add.Columns.Add("quantity");
                            RacikanDetail_add.Columns.Add("uom_id");
                            RacikanDetail_add.Columns.Add("uom_code");
                            RacikanDetail_add.Columns.Add("item_sequence");
                            RacikanDetail_add.Columns.Add("is_additional");
                            RacikanDetail_add.Columns.Add("organization_id");
                            RacikanDetail_add.Columns.Add("dose");
                            RacikanDetail_add.Columns.Add("dose_uom_id");
                            RacikanDetail_add.Columns.Add("dose_uom_code");
                            RacikanDetail_add.Columns.Add("dose_text");
                            RacikanDetail_add.Columns.Add("IsDoseText");
                        }

                        string[] tempordersetname = setname_lbl.Text.Split(new string[] { separator }, StringSplitOptions.None);
                        string ordersetname = tempordersetname[0];

                        int flagNamaRacikan = 0;
                        notifRacikan.Style.Add("display", "none");

                        for (int i = 0; i < RacikanHeader_add.Rows.Count; i++)
                        {
                            if (RacikanHeader_add.Rows[i]["compound_name"].ToString().ToLower() == ordersetname.ToLower())
                            {
                                flagNamaRacikan = 1;
                            }
                        }

                        if (flagNamaRacikan == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addcompoundorderset", "warningnotificationOption(); toastr.warning('Order Set Compound Name already exist! <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Add Compound Prescription Fail!');", true);
                        }
                        else
                        {

                            CompoundOrderSet OrderSetRacikan_add = new CompoundOrderSet();
                            var dataOrderSet_add = clsSOAP.getOrderSetRacikan(ordersetname, Helper.organizationId, Helper.doctorid, 0);
                            var JsonOrderSet_add = JsonConvert.DeserializeObject<ResultCompoundOrderSet>(dataOrderSet_add.Result.ToString());

                            OrderSetRacikan_add = JsonOrderSet_add.list;
                            CompoundHeaderSoap CHS = OrderSetRacikan_add.header;

                            string[] tempqty_add = CHS.quantity.ToString().Split('.');
                            if (tempqty_add.Count() > 1)
                            {
                                if (tempqty_add[1].Length == 3)
                                {
                                    if (tempqty_add[1] == "000")
                                    {
                                        CHS.quantity = decimal.Parse(tempqty_add[0]).ToString();
                                    }
                                    else if (tempqty_add[1].Substring(tempqty_add[1].Length - 2) == "00")
                                    {
                                        CHS.quantity = tempqty_add[0] + "." + tempqty_add[1].Substring(0, 1);
                                    }
                                    else if (tempqty_add[1].Substring(tempqty_add[1].Length - 1) == "0")
                                    {
                                        CHS.quantity = tempqty_add[0] + "." + tempqty_add[1].Substring(0, 2);
                                    }
                                }
                            }

                            string[] tempdose_add = CHS.dose.ToString().Split('.');
                            if (tempdose_add.Count() > 1)
                            {
                                if (tempdose_add[1].Length == 3)
                                {
                                    if (tempdose_add[1] == "000")
                                    {
                                        CHS.dose = decimal.Parse(tempdose_add[0]).ToString();
                                    }
                                    else if (tempdose_add[1].Substring(tempdose_add[1].Length - 2) == "00")
                                    {
                                        CHS.dose = tempdose_add[0] + "." + tempdose_add[1].Substring(0, 1);
                                    }
                                    else if (tempdose_add[1].Substring(tempdose_add[1].Length - 1) == "0")
                                    {
                                        CHS.dose = tempdose_add[0] + "." + tempdose_add[1].Substring(0, 2);
                                    }
                                }
                            }

                            DataRow tempHeader_add = RacikanHeader_add.NewRow();
                            tempHeader_add["prescription_compound_header_id"] = CHS.prescription_compound_header_id;
                            tempHeader_add["compound_name"] = CHS.compound_name;
                            tempHeader_add["quantity"] = CHS.quantity;
                            tempHeader_add["uom_id"] = CHS.uom_id;
                            tempHeader_add["uom_code"] = CHS.uom_code;
                            tempHeader_add["dose"] = CHS.dose;
                            tempHeader_add["dose_uom_id"] = CHS.dose_uom_id;
                            tempHeader_add["dose_uom"] = CHS.dose_uom;
                            tempHeader_add["administration_frequency_id"] = CHS.administration_frequency_id;
                            tempHeader_add["frequency_code"] = CHS.frequency_code;
                            tempHeader_add["administration_route_id"] = CHS.administration_route_id;
                            tempHeader_add["administration_route_code"] = CHS.administration_route_code;
                            tempHeader_add["administration_instruction"] = CHS.administration_instruction;
                            tempHeader_add["compound_note"] = CHS.compound_note;
                            tempHeader_add["iter"] = CHS.iter;
                            tempHeader_add["is_additional"] = true;

                            int item_seq = Convert.ToInt32(RacikanHeader_add.AsEnumerable().Max(row => row["item_sequence"]));
                            tempHeader_add["item_sequence"] = (short)(item_seq + 1);
                            tempHeader_add["dose_text"] = CHS.dose_text;
                            tempHeader_add["IsDoseText"] = CHS.IsDoseText;

                            RacikanHeader_add.Rows.Add(tempHeader_add);
                            Session[Helper.SessionRacikanHeaderAdd + hfguidadditional.Value] = RacikanHeader_add;

                            List<CompoundDetailSoap> listDataRacikanDetail_add = new List<CompoundDetailSoap>();
                            int countadd = 1;
                            foreach (CompoundDetailSoap grid in OrderSetRacikan_add.detail)
                            {
                                string[] tempgridqty = grid.quantity.ToString().Split('.');
                                if (tempgridqty.Count() > 1)
                                {
                                    if (tempgridqty[1].Length == 3)
                                    {
                                        if (tempgridqty[1] == "000")
                                        {
                                            grid.quantity = decimal.Parse(tempgridqty[0]).ToString();
                                        }
                                        else if (tempgridqty[1].Substring(tempgridqty[1].Length - 2) == "00")
                                        {
                                            grid.quantity = tempgridqty[0] + "." + tempgridqty[1].Substring(0, 1);
                                        }
                                        else if (tempgridqty[1].Substring(tempgridqty[1].Length - 1) == "0")
                                        {
                                            grid.quantity = tempgridqty[0] + "." + tempgridqty[1].Substring(0, 2);
                                        }
                                    }
                                }

                                string[] tempgriddose = grid.dose.ToString().Split('.');
                                if (tempgriddose.Count() > 1)
                                {
                                    if (tempgriddose[1].Length == 3)
                                    {
                                        if (tempgriddose[1] == "000")
                                        {
                                            grid.dose = decimal.Parse(tempgriddose[0]).ToString();
                                        }
                                        else if (tempgriddose[1].Substring(tempgriddose[1].Length - 2) == "00")
                                        {
                                            grid.dose = tempgriddose[0] + "." + tempgriddose[1].Substring(0, 1);
                                        }
                                        else if (tempgriddose[1].Substring(tempgriddose[1].Length - 1) == "0")
                                        {
                                            grid.dose = tempgriddose[0] + "." + tempgriddose[1].Substring(0, 2);
                                        }
                                    }
                                }

                                CompoundDetailSoap row = new CompoundDetailSoap();
                                row.item_sequence = (short)countadd;
                                countadd = countadd + 1;
                                row.prescription_compound_detail_id = grid.prescription_compound_detail_id;
                                row.prescription_compound_header_id = grid.prescription_compound_header_id;
                                row.item_id = grid.item_id;
                                row.item_name = grid.item_name;
                                row.quantity = grid.quantity;
                                row.uom_id = grid.uom_id;
                                row.uom_code = grid.uom_code;
                                row.is_additional = true;
                                row.organization_id = Helper.organizationId;
                                row.dose = grid.dose;
                                row.dose_text = grid.dose_text;
                                row.dose_uom_code = grid.dose_uom_code;
                                row.dose_uom_id = grid.dose_uom_id;
                                row.IsDoseText = grid.IsDoseText;

                                listDataRacikanDetail_add.Add(row);
                            }

                            DataTable tempDetail_add = Helper.ToDataTable(listDataRacikanDetail_add);

                            RacikanDetail_add = RacikanDetail_add.AsEnumerable().Union(tempDetail_add.AsEnumerable()).CopyToDataTable();
                            Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value] = RacikanDetail_add;

                            gvw_racikan_header_add.DataSource = RacikanHeader_add;
                            gvw_racikan_header_add.DataBind();

                        }

                        UpdatePanel_gvw_racikan_add.Update();

                        //SKIP_ORDERRACIKAN_ADD:
                        //int skipadd = 1;

                        //if (Session[Helper.SessionCompPres + hfguidadditional.Value] != null)
                        //{
                        //    if (((DataTable)Session[Helper.SessionCompPres + hfguidadditional.Value]).Select("compound_name = '" + ordersetname + "'").Count() > 0)
                        //    {
                        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalexdrugs", "alert('Order Set Compound already in use');", true);
                        //    }
                        //    else
                        //    {

                        //        var getItemdetailComp = clsPrescription.GetCompDetailPrescription(ordersetname, int.Parse(Helper.organizationId.ToString()));
                        //        var getJsonItemdetailComp = JsonConvert.DeserializeObject<ResultPrescription>(getItemdetailComp.Result.ToString());
                        //        listprescriptioncomptemp = getJsonItemdetailComp.list;

                        //        foreach (var templist in listprescriptioncomptemp)
                        //        {
                        //            string[] tempqty = templist.quantity.ToString().Split('.');
                        //            int a = tempqty.Count();
                        //            if (tempqty.Count() > 1)
                        //            {
                        //                if (tempqty[1].Length == 3)
                        //                {
                        //                    if (tempqty[1] == "000")
                        //                    {
                        //                        templist.quantity = decimal.Parse(tempqty[0]).ToString();
                        //                    }
                        //                    else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                        //                    {
                        //                        templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                        //                    }
                        //                    else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                        //                    {
                        //                        templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                        //                    }
                        //                }

                        //                string[] tempdose = templist.dosage_id.ToString().Split('.');

                        //                if (tempdose.Count() > 1)
                        //                {
                        //                    if (tempdose[1].Length == 3)
                        //                    {
                        //                        if (tempdose[1] == "000")
                        //                        {
                        //                            templist.dosage_id = decimal.Parse(tempdose[0]).ToString();
                        //                        }
                        //                        else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                        //                        {
                        //                            templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                        //                        }
                        //                        else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                        //                        {
                        //                            templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                        //                        }
                        //                    }
                        //                }

                        //            }
                        //        }
                        //        DataTable prescriptioncompdt = Helper.ToDataTable(listprescriptioncomptemp);

                        //        if (prescriptioncompdt.Select("organization_id = 0").Count() == 0)
                        //        {
                        //            DataTable dta = prescriptioncompdt.Select("item_id = 0").CopyToDataTable();
                        //            DataTable dtcompdetailhdn = prescriptioncompdt.Select("item_id <> 0").CopyToDataTable();
                        //            List<Prescription> dataheader = GetRowList(2);
                        //            List<Prescription> datadetail = GetRowList(3);

                        //            dta.Merge(Helper.ToDataTable(dataheader));
                        //            dtcompdetailhdn.Merge(Helper.ToDataTable(datadetail));

                        //            Session[Helper.SessionCompPres + hfguidadditional.Value] = dta;
                        //            gvw_comp.DataSource = dta;
                        //            gvw_comp.DataBind();

                        //            Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = dtcompdetailhdn;
                        //            gvw_compdetail.DataSource = dtcompdetailhdn;
                        //            gvw_compdetail.DataBind();
                        //        }
                        //        else
                        //        {
                        //            RepeaterDrugsNotAvailable.DataSource = null;
                        //            RepeaterDrugsNotAvailable.DataBind();
                        //            itemex.Visible = false;

                        //            DataTable dta = prescriptioncompdt.Select("item_id = 0").CopyToDataTable();
                        //            DataTable dtcompdetailhdn = prescriptioncompdt.Select("item_id <> 0 and organization_id <> 0").CopyToDataTable();
                        //            DataTable dtexdrugs = prescriptioncompdt.Select("item_id <> 0 and organization_id = 0").CopyToDataTable();
                        //            Session[Helper.SessionCompHeaderHdn + hfguidadditional.Value] = dta;
                        //            Session[Helper.SessionCompPresHdn + hfguidadditional.Value] = dtcompdetailhdn;
                        //            //DataTable dtcompdetailhdn = Helper.ToDataTable(datadetail);
                        //            if (prescriptioncompdt.Select("item_id <> 0 and organization_id = 0").Count() > 0)
                        //            {
                        //                RepeaterDrugsAlreadyExist.DataSource = dtexdrugs;
                        //                RepeaterDrugsAlreadyExist.DataBind();
                        //                itemex.Visible = true;
                        //            }

                        //            gvw_comp_detail.DataSource = dtcompdetailhdn;
                        //            gvw_comp_detail.DataBind();

                        //            //DataTable dtItem = (DataTable)Session["item"];
                        //            gvw_item_detail.DataSource = (DataTable)Session[Helper.SessionItemDrugPres];
                        //            gvw_item_detail.DataBind();

                        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "$('#modalcompdetail').modal({backdrop: 'static', keyboard: false});", true);
                        //        }

                        //    }
                        //}
                        //else
                        //{
                        //    var getItemdetailComp = clsPrescription.GetCompDetailPrescription(ordersetname, int.Parse(Helper.organizationId.ToString()));
                        //    var getJsonItemdetailComp = JsonConvert.DeserializeObject<ResultPrescription>(getItemdetailComp.Result.ToString());
                        //    listprescriptioncomptemp = getJsonItemdetailComp.list;

                        //    foreach (var templist in listprescriptioncomptemp)
                        //    {
                        //        string[] tempqty = templist.quantity.ToString().Split('.');
                        //        int a = tempqty.Count();
                        //        if (tempqty.Count() > 1)
                        //        {
                        //            if (tempqty[1].Length == 3)
                        //            {
                        //                if (tempqty[1] == "000")
                        //                {
                        //                    templist.quantity = decimal.Parse(tempqty[0]).ToString();
                        //                }
                        //                else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                        //                {
                        //                    templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                        //                }
                        //                else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                        //                {
                        //                    templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                        //                }
                        //            }

                        //            string[] tempdose = templist.dosage_id.ToString().Split('.');
                        //            if (tempdose.Count() > 1)
                        //            {
                        //                if (tempdose[1].Length == 3)
                        //                {
                        //                    if (tempdose[1] == "000")
                        //                    {
                        //                        templist.dosage_id = decimal.Parse(tempdose[0]).ToString();
                        //                    }
                        //                    else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                        //                    {
                        //                        templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                        //                    }
                        //                    else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                        //                    {
                        //                        templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }

                        //    DataTable prescriptioncompdt = Helper.ToDataTable(listprescriptioncomptemp);

                        //    if (prescriptioncompdt.Select("organization_id = 0").Count() == 0)
                        //    {
                        //        DataTable dta = prescriptioncompdt.Select("item_id = 0").CopyToDataTable();
                        //        DataTable dtcompdetailhdn = prescriptioncompdt.Select("item_id <> 0").CopyToDataTable();
                        //        List<Prescription> dataheader = GetRowList(2);
                        //        List<Prescription> datadetail = GetRowList(3);

                        //        dta.Merge(Helper.ToDataTable(dataheader));
                        //        dtcompdetailhdn.Merge(Helper.ToDataTable(datadetail));

                        //        Session[Helper.SessionCompPres + hfguidadditional.Value] = dta;
                        //        gvw_comp.DataSource = dta;
                        //        gvw_comp.DataBind();

                        //        Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = dtcompdetailhdn;
                        //        gvw_compdetail.DataSource = dtcompdetailhdn;
                        //        gvw_compdetail.DataBind();
                        //    }
                        //    else
                        //    {
                        //        RepeaterDrugsAlreadyExist.DataSource = null;
                        //        RepeaterDrugsAlreadyExist.DataBind();
                        //        itemex.Visible = false;

                        //        DataTable dta = prescriptioncompdt.Select("item_id = 0").CopyToDataTable();
                        //        DataTable dtcompdetailhdn = prescriptioncompdt.Select("item_id <> 0 and organization_id <> 0").CopyToDataTable();
                        //        DataTable dtexdrugs = prescriptioncompdt.Select("item_id <> 0 and organization_id = 0").CopyToDataTable();
                        //        Session[Helper.SessionCompHeaderHdn + hfguidadditional.Value] = dta;
                        //        Session[Helper.SessionCompPresHdn + hfguidadditional.Value] = dtcompdetailhdn;
                        //        //DataTable dtcompdetailhdn = Helper.ToDataTable(datadetail);
                        //        if (prescriptioncompdt.Select("item_id <> 0 and organization_id = 0").Count() > 0)
                        //        {
                        //            RepeaterDrugsAlreadyExist.DataSource = dtexdrugs;
                        //            RepeaterDrugsAlreadyExist.DataBind();
                        //            itemex.Visible = true;
                        //        }

                        //        gvw_comp_detail.DataSource = dtcompdetailhdn;
                        //        gvw_comp_detail.DataBind();

                        //        //DataTable dtItem = (DataTable)Session["item"];
                        //        gvw_item_detail.DataSource = ((DataTable)Session[Helper.SessionItemDrugPres]).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
                        //        gvw_item_detail.DataBind();

                        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "$('#modalcompdetail').modal({backdrop: 'static', keyboard: false});", true);
                        //    }
                        //}


                    }
                    else
                    {
                        
                        var getItemdetailDrug = clsPrescription.GetDrugDetailPrescription(setname_lbl.Text, Helper.organizationId, Helper.doctorid);
                        var getJsonItemdetailDrug = JsonConvert.DeserializeObject<ResultPrescription>(getItemdetailDrug.Result.ToString());
                        listprescriptiondrugtemp = getJsonItemdetailDrug.list;

                        foreach (var x in listprescriptiondrugtemp)
                        {
                            if (is_itemissue == true)
                            {
                                if (x.uom_id != x.uom_idori)
                                {
                                    Prescription p = new Prescription();
                                    p = x;
                                    listdruguomcheck.Add(p);
                                }
                            }
                        }

                        if (listdruguomcheck.Count != 0)
                        {
                            RepeaterDrugsUomChange.DataSource = listdruguomcheck;
                            RepeaterDrugsUomChange.DataBind();

                            dialogDrugsUomChange.Visible = true;
                            dialogDrugsUomChange.Attributes.Remove("style");
                            dialogDrugsUomChange.Attributes.Add("style", "position: fixed; top:25%; left:0; right:0; width: 40%; ");

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modaluomchangedrugs", "$('#modaluomchangedrugs').modal();", true);

                            UpdatePanelUomChange.Update();

                        }

                        List<Prescription> data = GetRowList(6);
                        foreach (var x in listprescriptiondrugtemp)
                        {
                            if (is_itemissue == true)
                            {
                                if (x.uom_id != x.uom_idori)
                                {
                                    x.uom_id = x.uom_idori;
                                    x.uom_code = x.uom_codeori;
                                    x.quantity = "0";
                                }
                            }

                            if (x.organization_id != 0)
                            {
                                if (x != null)
                                {
                                    DataTable dttemp = Helper.ToDataTable(data);
                                    if (dttemp.Select("item_name = '" + x.item_name + "'").Count() == 0)
                                    {
                                        data.Add(x);
                                        DataTable dta = Helper.ToDataTable(data);
                                        Session[Helper.Sessionadditionalpres + hfguidadditional.Value] = dta;
                                        gvwAdditionalDrugs.DataSource = dta;
                                        gvwAdditionalDrugs.DataBind();
                                        txtSearchItem.Text = "";
                                    }
                                    else
                                        listdrugex.Add(x);

                                }
                            }
                            else
                                listdrugnotavail.Add(x);
                        }

                        DataTable dtDrugex = Helper.ToDataTable(listdrugex);
                        DataTable dtDrugNotAvail = Helper.ToDataTable(listdrugnotavail);

                        if (dtDrugex.Rows.Count != 0 || dtDrugNotAvail.Rows.Count != 0)
                        {

                            //UpdatePanel1.Update();
                            RepeaterDrugsNotAvailable.DataSource = dtDrugNotAvail;
                            RepeaterDrugsNotAvailable.DataBind();
                            RepeaterDrugsAlreadyExist.DataSource = dtDrugex;
                            RepeaterDrugsAlreadyExist.DataBind();

                            if (dtDrugex.Rows.Count != 0 && dtDrugNotAvail.Rows.Count != 0)
                            {
                                dialogDrugsAlreadyExist.Visible = true;
                                dialogDrugsNotAvailable.Visible = true;

                                dialogDrugsNotAvailable.Attributes.Remove("style");
                                dialogDrugsNotAvailable.Attributes.Add("style", "position: fixed; top: 25%; left: 20%; width: 30%;;");

                                dialogDrugsAlreadyExist.Attributes.Remove("style");
                                dialogDrugsAlreadyExist.Attributes.Add("style", "position: fixed; top: 25%; left: 51%; width: 30%;");
                            }

                            else if (dtDrugex.Rows.Count != 0)
                            {
                                dialogDrugsAlreadyExist.Visible = true;
                                dialogDrugsNotAvailable.Visible = false;
                                dialogDrugsAlreadyExist.Attributes.Remove("style");
                                dialogDrugsAlreadyExist.Attributes.Add("style", "position: fixed; top: 25%; left:0; right:0;  width: 30%; ");
                            }

                            else if (dtDrugNotAvail.Rows.Count != 0)
                            {
                                dialogDrugsAlreadyExist.Visible = false;
                                dialogDrugsNotAvailable.Visible = true;
                                dialogDrugsNotAvailable.Attributes.Remove("style");
                                dialogDrugsNotAvailable.Attributes.Add("style", "position: fixed; top: 25%; left:0; right:0;  width: 30%; ");
                            }

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalexdrugs", "$('#modalexdrugs').modal();", true);

                            UpdatePanelExistDrugs.Update();
                            UpdatePanelNotavailDrugs.Update();

                        }

                        

                        upSaveAsOrderSet.Update();
                        upFormularium.Update();
                        UpdatePanelListPrescription.Update();
                        UpdatePanelListPrescriptionAdd.Update();
                    }
                }
            }
            else
            {
                if (hftakedate.Value == "1")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Prescription Already Taken by Pharmacy');", true);
                }
                else
                {
                    string separator = "-(R)";

                    //List<PrescriptionDrug> listprescriptiondrugtemp, listprescriptioncomptemp,listdrugex = new List<PrescriptionDrug>();
                    List<Prescription> listprescriptiondrugtemp, listprescriptioncomptemp, listdrugex = new List<Prescription>();
                    List<Prescription> listdrugnotavail = new List<Prescription>();
                    List<Prescription> listdruguomcheck = new List<Prescription>();
                    int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
                    //LinkButton setname = (LinkButton)gvw_orderset.Rows[selRowIndex].FindControl("setname");
                    Label setname_lbl = (Label)gvw_orderset.Rows[selRowIndex].FindControl("Label_setname");


                    if (setname_lbl.Text.Contains("-(R)"))
                    {
                        //goto SKIP_ORDERRACIKAN;

                        DataTable RacikanHeader = (DataTable)Session[Helper.SessionRacikanHeader + hfguidadditional.Value];
                        DataTable RacikanDetail = (DataTable)Session[Helper.SessionRacikanDetail + hfguidadditional.Value];

                        if (RacikanHeader == null)
                        {
                            RacikanHeader = new DataTable();
                            RacikanHeader.Columns.Add("prescription_compound_header_id");
                            RacikanHeader.Columns.Add("compound_name");
                            RacikanHeader.Columns.Add("quantity");
                            RacikanHeader.Columns.Add("uom_id");
                            RacikanHeader.Columns.Add("uom_code");
                            RacikanHeader.Columns.Add("dose");
                            RacikanHeader.Columns.Add("dose_uom_id");
                            RacikanHeader.Columns.Add("dose_uom");
                            RacikanHeader.Columns.Add("administration_frequency_id");
                            RacikanHeader.Columns.Add("frequency_code");
                            RacikanHeader.Columns.Add("administration_route_id");
                            RacikanHeader.Columns.Add("administration_route_code");
                            RacikanHeader.Columns.Add("administration_instruction");
                            RacikanHeader.Columns.Add("compound_note");
                            RacikanHeader.Columns.Add("iter");
                            RacikanHeader.Columns.Add("is_additional");
                            RacikanHeader.Columns.Add("item_sequence");
                            RacikanHeader.Columns.Add("dose_text");
                            RacikanHeader.Columns.Add("IsDoseText");

                            RacikanDetail = new DataTable();
                            RacikanDetail.Columns.Add("prescription_compound_detail_id");
                            RacikanDetail.Columns.Add("prescription_compound_header_id");
                            RacikanDetail.Columns.Add("item_id");
                            RacikanDetail.Columns.Add("item_name");
                            RacikanDetail.Columns.Add("quantity");
                            RacikanDetail.Columns.Add("uom_id");
                            RacikanDetail.Columns.Add("uom_code");
                            RacikanDetail.Columns.Add("item_sequence");
                            RacikanDetail.Columns.Add("is_additional");
                            RacikanDetail.Columns.Add("organization_id");
                            RacikanDetail.Columns.Add("dose");
                            RacikanDetail.Columns.Add("dose_uom_id");
                            RacikanDetail.Columns.Add("dose_uom_code");
                            RacikanDetail.Columns.Add("dose_text");
                            RacikanDetail.Columns.Add("IsDoseText");
                        }

                        string[] tempordersetname = setname_lbl.Text.Split(new string[] { separator }, StringSplitOptions.None);
                        string ordersetname = tempordersetname[0];

                        int flagNamaRacikan = 0;
                        notifRacikan.Style.Add("display", "none");

                        for (int i = 0; i < RacikanHeader.Rows.Count; i++)
                        {
                            if (RacikanHeader.Rows[i]["compound_name"].ToString().ToLower() == ordersetname.ToLower())
                            {
                                flagNamaRacikan = 1;
                            }
                        }

                        if (flagNamaRacikan == 1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addcompoundorderset", "warningnotificationOption(); toastr.warning('Order Set Compound Name already exist! <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Add Compound Prescription Fail!');", true);
                        }
                        else
                        {

                            CompoundOrderSet OrderSetRacikan = new CompoundOrderSet();

                            Dictionary<string, string> logParam = new Dictionary<string, string>
                            {
                                { "Orderset_Name", ordersetname },
                                { "Organization_ID", Helper.organizationId.ToString() },
                                { "Doctor_ID", Helper.doctorid.ToString() },
                                { "Is_Additional", "0" }
                            };
                            //Log.Debug(LogConfig.LogStart("getOrderSetRacikan", logParam));
                            var dataOrderSet = clsSOAP.getOrderSetRacikan(ordersetname, Helper.organizationId, Helper.doctorid, 0);
                            var JsonOrderSet = JsonConvert.DeserializeObject<ResultCompoundOrderSet>(dataOrderSet.Result.ToString());
                            //Log.Debug(LogConfig.LogEnd("getOrderSetRacikan", JsonOrderSet.Status, JsonOrderSet.Message));

                            OrderSetRacikan = JsonOrderSet.list;
                            CompoundHeaderSoap CHS = OrderSetRacikan.header;

                            string[] tempqty = CHS.quantity.ToString().Split('.');
                            if (tempqty.Count() > 1)
                            {
                                if (tempqty[1].Length == 3)
                                {
                                    if (tempqty[1] == "000")
                                    {
                                        CHS.quantity = decimal.Parse(tempqty[0]).ToString();
                                    }
                                    else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                                    {
                                        CHS.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                                    }
                                    else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                                    {
                                        CHS.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                                    }
                                }
                            }

                            string[] tempdose = CHS.dose.ToString().Split('.');
                            if (tempdose.Count() > 1)
                            {
                                if (tempdose[1].Length == 3)
                                {
                                    if (tempdose[1] == "000")
                                    {
                                        CHS.dose = decimal.Parse(tempdose[0]).ToString();
                                    }
                                    else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                                    {
                                        CHS.dose = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                                    }
                                    else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                                    {
                                        CHS.dose = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                                    }
                                }
                            }

                            DataRow tempHeader = RacikanHeader.NewRow();
                            tempHeader["prescription_compound_header_id"] = CHS.prescription_compound_header_id;
                            tempHeader["compound_name"] = CHS.compound_name;
                            tempHeader["quantity"] = CHS.quantity;
                            tempHeader["uom_id"] = CHS.uom_id;
                            tempHeader["uom_code"] = CHS.uom_code;
                            tempHeader["dose"] = CHS.dose;
                            tempHeader["dose_uom_id"] = CHS.dose_uom_id;
                            tempHeader["dose_uom"] = CHS.dose_uom;
                            tempHeader["administration_frequency_id"] = CHS.administration_frequency_id;
                            tempHeader["frequency_code"] = CHS.frequency_code;
                            tempHeader["administration_route_id"] = CHS.administration_route_id;
                            tempHeader["administration_route_code"] = CHS.administration_route_code;
                            tempHeader["administration_instruction"] = CHS.administration_instruction;
                            tempHeader["compound_note"] = CHS.compound_note;
                            tempHeader["iter"] = CHS.iter;
                            tempHeader["is_additional"] = false;

                            int item_seq = Convert.ToInt32(RacikanHeader.AsEnumerable().Max(row => row["item_sequence"]));
                            tempHeader["item_sequence"] = (short)(item_seq + 1);
                            tempHeader["dose_text"] = CHS.dose_text;
                            tempHeader["IsDoseText"] = CHS.IsDoseText;

                            RacikanHeader.Rows.Add(tempHeader);
                            Session[Helper.SessionRacikanHeader + hfguidadditional.Value] = RacikanHeader;

                            List<CompoundDetailSoap> listDataRacikanDetail = new List<CompoundDetailSoap>();
                            int countadd = 1;
                            foreach (CompoundDetailSoap grid in OrderSetRacikan.detail)
                            {
                                string[] tempgridqty = grid.quantity.ToString().Split('.');
                                if (tempgridqty.Count() > 1)
                                {
                                    if (tempgridqty[1].Length == 3)
                                    {
                                        if (tempgridqty[1] == "000")
                                        {
                                            grid.quantity = decimal.Parse(tempgridqty[0]).ToString();
                                        }
                                        else if (tempgridqty[1].Substring(tempgridqty[1].Length - 2) == "00")
                                        {
                                            grid.quantity = tempgridqty[0] + "." + tempgridqty[1].Substring(0, 1);
                                        }
                                        else if (tempgridqty[1].Substring(tempgridqty[1].Length - 1) == "0")
                                        {
                                            grid.quantity = tempgridqty[0] + "." + tempgridqty[1].Substring(0, 2);
                                        }
                                    }
                                }

                                string[] tempgriddose = grid.dose.ToString().Split('.');
                                if (tempgriddose.Count() > 1)
                                {
                                    if (tempgriddose[1].Length == 3)
                                    {
                                        if (tempgriddose[1] == "000")
                                        {
                                            grid.dose = decimal.Parse(tempgriddose[0]).ToString();
                                        }
                                        else if (tempgriddose[1].Substring(tempgriddose[1].Length - 2) == "00")
                                        {
                                            grid.dose = tempgriddose[0] + "." + tempgriddose[1].Substring(0, 1);
                                        }
                                        else if (tempgriddose[1].Substring(tempgriddose[1].Length - 1) == "0")
                                        {
                                            grid.dose = tempgriddose[0] + "." + tempgriddose[1].Substring(0, 2);
                                        }
                                    }
                                }

                                CompoundDetailSoap row = new CompoundDetailSoap();
                                row.item_sequence = (short)countadd;
                                countadd = countadd + 1;
                                row.prescription_compound_detail_id = grid.prescription_compound_detail_id;
                                row.prescription_compound_header_id = grid.prescription_compound_header_id;
                                row.item_id = grid.item_id;
                                row.item_name = grid.item_name;
                                row.quantity = grid.quantity;
                                row.uom_id = grid.uom_id;
                                row.uom_code = grid.uom_code;
                                row.is_additional = false;
                                row.organization_id = Helper.organizationId;
                                row.dose = grid.dose;
                                row.dose_text = grid.dose_text;
                                row.dose_uom_code = grid.dose_uom_code;
                                row.dose_uom_id = grid.dose_uom_id;
                                row.IsDoseText = grid.IsDoseText;

                                listDataRacikanDetail.Add(row);
                            }

                            DataTable tempDetail = Helper.ToDataTable(listDataRacikanDetail);

                            RacikanDetail = RacikanDetail.AsEnumerable().Union(tempDetail.AsEnumerable()).CopyToDataTable();
                            Session[Helper.SessionRacikanDetail + hfguidadditional.Value] = RacikanDetail;

                            gvw_racikan_header.DataSource = RacikanHeader;
                            gvw_racikan_header.DataBind();
                        }

                        UpdatePanel_gvw_racikan.Update();

                        //SKIP_ORDERRACIKAN:
                        //int skip = 1;

                        //if (Session[Helper.SessionCompPres + hfguidadditional.Value] != null)
                        //{
                        //   if (((DataTable)Session[Helper.SessionCompPres + hfguidadditional.Value]).Select("compound_name = '" + ordersetname + "'").Count() > 0)
                        //    {
                        //         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalexdrugs", "alert('Order Set Compound already in use');", true);
                        //    }
                        //    else
                        //    {

                        //        var getItemdetailComp = clsPrescription.GetCompDetailPrescription(ordersetname, int.Parse(Helper.organizationId.ToString()));
                        //        var getJsonItemdetailComp = JsonConvert.DeserializeObject<ResultPrescription>(getItemdetailComp.Result.ToString());
                        //        listprescriptioncomptemp = getJsonItemdetailComp.list;

                        //        foreach (var templist in listprescriptioncomptemp)
                        //        {
                        //            string[] tempqty = templist.quantity.ToString().Split('.');
                        //            int a = tempqty.Count();
                        //            if (tempqty.Count() > 1)
                        //            {
                        //                if (tempqty[1].Length == 3)
                        //                {
                        //                    if (tempqty[1] == "000")
                        //                    {
                        //                        templist.quantity = decimal.Parse(tempqty[0]).ToString();
                        //                    }
                        //                    else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                        //                    {
                        //                        templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                        //                    }
                        //                    else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                        //                    {
                        //                        templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                        //                    }
                        //                }

                        //                string[] tempdose = templist.dosage_id.ToString().Split('.');

                        //                if (tempdose.Count() > 1)
                        //                {
                        //                    if (tempdose[1].Length == 3)
                        //                    {
                        //                        if (tempdose[1] == "000")
                        //                        {
                        //                            templist.dosage_id = decimal.Parse(tempdose[0]).ToString();
                        //                        }
                        //                        else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                        //                        {
                        //                            templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                        //                        }
                        //                        else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                        //                        {
                        //                            templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                        //                        }
                        //                    }
                        //                }

                        //            }
                        //        }
                        //        DataTable prescriptioncompdt = Helper.ToDataTable(listprescriptioncomptemp);

                        //        if (prescriptioncompdt.Select("organization_id = 0").Count() == 0)
                        //        {
                        //            DataTable dta = prescriptioncompdt.Select("item_id = 0").CopyToDataTable();
                        //            DataTable dtcompdetailhdn = prescriptioncompdt.Select("item_id <> 0").CopyToDataTable();
                        //            List<Prescription> dataheader = GetRowList(2);
                        //            List<Prescription> datadetail = GetRowList(3);

                        //            dta.Merge(Helper.ToDataTable(dataheader));
                        //            dtcompdetailhdn.Merge(Helper.ToDataTable(datadetail));

                        //            Session[Helper.SessionCompPres + hfguidadditional.Value] = dta;
                        //            gvw_comp.DataSource = dta;
                        //            gvw_comp.DataBind();

                        //            Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = dtcompdetailhdn;
                        //            gvw_compdetail.DataSource = dtcompdetailhdn;
                        //            gvw_compdetail.DataBind();
                        //        }
                        //        else
                        //        {
                        //            RepeaterDrugsNotAvailable.DataSource = null;
                        //            RepeaterDrugsNotAvailable.DataBind();
                        //            itemex.Visible = false;

                        //            DataTable dta = prescriptioncompdt.Select("item_id = 0").CopyToDataTable();
                        //            DataTable dtcompdetailhdn = prescriptioncompdt.Select("item_id <> 0 and organization_id <> 0").CopyToDataTable();
                        //            DataTable dtexdrugs = prescriptioncompdt.Select("item_id <> 0 and organization_id = 0").CopyToDataTable();
                        //            Session[Helper.SessionCompHeaderHdn + hfguidadditional.Value] = dta;
                        //            Session[Helper.SessionCompPresHdn + hfguidadditional.Value] = dtcompdetailhdn;
                        //            //DataTable dtcompdetailhdn = Helper.ToDataTable(datadetail);
                        //            if (prescriptioncompdt.Select("item_id <> 0 and organization_id = 0").Count() > 0)
                        //            {
                        //                RepeaterDrugsAlreadyExist.DataSource = dtexdrugs;
                        //                RepeaterDrugsAlreadyExist.DataBind();
                        //                itemex.Visible = true;
                        //            }

                        //            gvw_comp_detail.DataSource = dtcompdetailhdn;
                        //            gvw_comp_detail.DataBind();

                        //            //DataTable dtItem = (DataTable)Session["item"];
                        //            gvw_item_detail.DataSource = (DataTable)Session[Helper.SessionItemDrugPres];
                        //            gvw_item_detail.DataBind();

                        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "$('#modalcompdetail').modal({backdrop: 'static', keyboard: false});", true);
                        //        }

                        //    }
                        //}
                        //else
                        //{
                        //    var getItemdetailComp = clsPrescription.GetCompDetailPrescription(ordersetname, int.Parse(Helper.organizationId.ToString()));
                        //    var getJsonItemdetailComp = JsonConvert.DeserializeObject<ResultPrescription>(getItemdetailComp.Result.ToString());
                        //    listprescriptioncomptemp = getJsonItemdetailComp.list;

                        //    foreach (var templist in listprescriptioncomptemp)
                        //    {
                        //        string[] tempqty = templist.quantity.ToString().Split('.');
                        //        int a = tempqty.Count();
                        //        if (tempqty.Count() > 1)
                        //        {
                        //            if (tempqty[1].Length == 3)
                        //            {
                        //                if (tempqty[1] == "000")
                        //                {
                        //                    templist.quantity = decimal.Parse(tempqty[0]).ToString();
                        //                }
                        //                else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                        //                {
                        //                    templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                        //                }
                        //                else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                        //                {
                        //                    templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                        //                }
                        //            }

                        //            string[] tempdose = templist.dosage_id.ToString().Split('.');
                        //            if (tempdose.Count() > 1)
                        //            {
                        //                if (tempdose[1].Length == 3)
                        //                {
                        //                    if (tempdose[1] == "000")
                        //                    {
                        //                        templist.dosage_id = decimal.Parse(tempdose[0]).ToString();
                        //                    }
                        //                    else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                        //                    {
                        //                        templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                        //                    }
                        //                    else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                        //                    {
                        //                        templist.dosage_id = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }

                        //    DataTable prescriptioncompdt = Helper.ToDataTable(listprescriptioncomptemp);

                        //    if (prescriptioncompdt.Select("organization_id = 0").Count() == 0)
                        //    {
                        //        DataTable dta = prescriptioncompdt.Select("item_id = 0").CopyToDataTable();
                        //        DataTable dtcompdetailhdn = prescriptioncompdt.Select("item_id <> 0").CopyToDataTable();
                        //        List<Prescription> dataheader = GetRowList(2);
                        //        List<Prescription> datadetail = GetRowList(3);

                        //        dta.Merge(Helper.ToDataTable(dataheader));
                        //        dtcompdetailhdn.Merge(Helper.ToDataTable(datadetail));

                        //        Session[Helper.SessionCompPres + hfguidadditional.Value] = dta;
                        //        gvw_comp.DataSource = dta;
                        //        gvw_comp.DataBind();

                        //        Session[Helper.SessionCompDetailPres + hfguidadditional.Value] = dtcompdetailhdn;
                        //        gvw_compdetail.DataSource = dtcompdetailhdn;
                        //        gvw_compdetail.DataBind();
                        //    }
                        //    else
                        //    {
                        //        RepeaterDrugsAlreadyExist.DataSource = null;
                        //        RepeaterDrugsAlreadyExist.DataBind();
                        //        itemex.Visible = false;

                        //        DataTable dta = prescriptioncompdt.Select("item_id = 0").CopyToDataTable();
                        //        DataTable dtcompdetailhdn = prescriptioncompdt.Select("item_id <> 0 and organization_id <> 0").CopyToDataTable();
                        //        DataTable dtexdrugs = prescriptioncompdt.Select("item_id <> 0 and organization_id = 0").CopyToDataTable();
                        //        Session[Helper.SessionCompHeaderHdn + hfguidadditional.Value] = dta;
                        //        Session[Helper.SessionCompPresHdn + hfguidadditional.Value] = dtcompdetailhdn;
                        //        //DataTable dtcompdetailhdn = Helper.ToDataTable(datadetail);
                        //        if (prescriptioncompdt.Select("item_id <> 0 and organization_id = 0").Count() > 0)
                        //        {
                        //            RepeaterDrugsAlreadyExist.DataSource = dtexdrugs;
                        //            RepeaterDrugsAlreadyExist.DataBind();
                        //            itemex.Visible = true;
                        //        }

                        //        gvw_comp_detail.DataSource = dtcompdetailhdn;
                        //        gvw_comp_detail.DataBind();

                        //        //DataTable dtItem = (DataTable)Session["item"];
                        //        gvw_item_detail.DataSource = ((DataTable)Session[Helper.SessionItemDrugPres]).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
                        //        gvw_item_detail.DataBind();

                        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "$('#modalcompdetail').modal({backdrop: 'static', keyboard: false});", true);
                        //    }
                        //}
                    }
                    else
                    {
                        Dictionary<string, string> logParam = new Dictionary<string, string>
                        {
                            { "Orderset_Name", setname_lbl.Text },
                            { "Organization_ID", Helper.organizationId.ToString() },
                            { "Doctor_ID", Helper.doctorid.ToString() }
                        };
                        //Log.Debug(LogConfig.LogStart("GetDrugDetailPrescription", logParam));
                        var getItemdetailDrug = clsPrescription.GetDrugDetailPrescription(setname_lbl.Text, Helper.organizationId, Helper.doctorid);
                        var getJsonItemdetailDrug = JsonConvert.DeserializeObject<ResultPrescription>(getItemdetailDrug.Result.ToString());
                        //Log.Debug(LogConfig.LogEnd("GetDrugDetailPrescription", getJsonItemdetailDrug.Status, getJsonItemdetailDrug.Message));

                        listprescriptiondrugtemp = getJsonItemdetailDrug.list;

                        foreach (var x in listprescriptiondrugtemp)
                        {
                            if (is_itemissue == true)
                            {
                                if (x.uom_id != x.uom_idori)
                                {
                                    Prescription p = new Prescription();
                                    p = x;
                                    listdruguomcheck.Add(p);
                                }
                            }
                        }

                        if (listdruguomcheck.Count != 0)
                        {
                            RepeaterDrugsUomChange.DataSource = listdruguomcheck;
                            RepeaterDrugsUomChange.DataBind();

                            dialogDrugsUomChange.Visible = true;
                            dialogDrugsUomChange.Attributes.Remove("style");
                            dialogDrugsUomChange.Attributes.Add("style", "position: fixed; top:25%; left:0; right:0; width: 40%; ");

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modaluomchangedrugs", "$('#modaluomchangedrugs').modal();", true);

                            UpdatePanelUomChange.Update();

                        }

                        List<Prescription> data = GetRowList(1);
                        foreach (var x in listprescriptiondrugtemp)
                        {
                            if (is_itemissue == true)
                            {
                                if (x.uom_id != x.uom_idori)
                                {
                                    x.uom_id = x.uom_idori;
                                    x.uom_code = x.uom_codeori;
                                    x.quantity = "0";
                                }
                            }

                            if (x.organization_id != 0)
                            {
                                if (x != null)
                                {
                                    DataTable dttemp = Helper.ToDataTable(data);
                                    if (dttemp.Select("item_name = '" + x.item_name + "'").Count() == 0)
                                    {
                                        data.Add(x);
                                        DataTable dta = Helper.ToDataTable(data);
                                        Session[Helper.SessionDrugPres + hfguidadditional.Value] = dta;
                                        gvw_drug.DataSource = dta;
                                        gvw_drug.DataBind();
                                        txtSearchItem.Text = "";
                                    }
                                    else
                                        listdrugex.Add(x);

                                }
                            }
                            else
                                listdrugnotavail.Add(x);

                            
                        }

                        DataTable dtDrugex = Helper.ToDataTable(listdrugex);
                        DataTable dtDrugNotAvail = Helper.ToDataTable(listdrugnotavail);

                        if (dtDrugex.Rows.Count != 0 || dtDrugNotAvail.Rows.Count !=0)
                        {

                            //UpdatePanel1.Update();
                            RepeaterDrugsAlreadyExist.DataSource = dtDrugex;
                            RepeaterDrugsAlreadyExist.DataBind();
                            RepeaterDrugsNotAvailable.DataSource = dtDrugNotAvail;
                            RepeaterDrugsNotAvailable.DataBind();

                            if (dtDrugex.Rows.Count != 0 && dtDrugNotAvail.Rows.Count != 0)
                            {
                                dialogDrugsAlreadyExist.Visible = true;
                                dialogDrugsNotAvailable.Visible = true;

                                dialogDrugsNotAvailable.Attributes.Remove("style");
                                dialogDrugsNotAvailable.Attributes.Add("style", "position: fixed; top: 25%; left: 20%; width: 30%;;");

                                dialogDrugsAlreadyExist.Attributes.Remove("style");
                                dialogDrugsAlreadyExist.Attributes.Add("style", "position: fixed; top: 25%; left: 51%; width: 30%;");

                            }

                            else if (dtDrugex.Rows.Count != 0)
                            {
                                dialogDrugsAlreadyExist.Visible = true;
                                dialogDrugsNotAvailable.Visible = false;
                                dialogDrugsAlreadyExist.Attributes.Remove("style");
                                dialogDrugsAlreadyExist.Attributes.Add("style", "position: fixed; top:25%; left:0; right:0; width:30%;");
                            }

                            else if (dtDrugNotAvail.Rows.Count != 0)
                            {
                                dialogDrugsAlreadyExist.Visible = false;
                                dialogDrugsNotAvailable.Visible = true;
                                dialogDrugsNotAvailable.Attributes.Remove("style");
                                dialogDrugsNotAvailable.Attributes.Add("style", "position: fixed; top:25%; left:0; right:0; width: 30%; ");
                            }

                            //dialogDrugsNotAvailable.Visible = false;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalexdrugs", "$('#modalexdrugs').modal();", true);

                            UpdatePanelExistDrugs.Update();
                            UpdatePanelNotavailDrugs.Update();
                        }

                        

                        upSaveAsOrderSet.Update();
                        upFormularium.Update();
                        UpdatePanelListPrescription.Update();
                        UpdatePanelListPrescriptionAdd.Update();
                    }
                }
            }


            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "orderset_onclick", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "orderset_onclick", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            // Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    public dynamic GetPlanningValues(dynamic SOA)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            foreach (var planning in SOA.planning)
            {
                //if (planning.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
                //{
                //    planning.remarks = txtPlanning.Text;
                //}
                if (planning.soap_mapping_id == Guid.Parse("5B39A9B4-744B-4AD3-954F-386E32220ABE"))
                {
                    planning.remarks = txtplanningotherLab.Text;
                }
                else if (planning.soap_mapping_id == Guid.Parse("61764B36-4BF4-4A03-917E-695E6929AFB3"))
                {
                    planning.remarks = txtplanningotherRad.Text;
                }
                else if (planning.soap_mapping_id == Guid.Parse("E794E3D3-7860-4D52-9166-9A5DF3127E55"))
                {
                    //REVISI OTHERS DATE
                    if(dp_labFutureOrder.Text != "")
					{
                        planning.value = (DateTime.Parse(dp_labFutureOrder.Text)).ToString("yyyy-MM-dd");
                        planning.remarks = txtplanningotherLab_FutureOrder.Text;
                    }
                }
                else if (planning.soap_mapping_id == Guid.Parse("0EE8A241-73A8-49DB-8F4B-8733CDB92C8F"))
                {
                    //REVISI OTHERS DATE
                    if(dp_radFutureOrder.Text != "")
					{
                        planning.value = (DateTime.Parse(dp_radFutureOrder.Text)).ToString("yyyy-MM-dd");
                        planning.remarks = txtplanningotherRad_FutureOrder.Text;
                    }

                }
                else if (planning.soap_mapping_id == Guid.Parse("2df0294d-f94e-4ba4-8ba1-f017bfb55d92"))
                {
                    planning.remarks = txtPresNotes.Text;
                }
                else if (planning.soap_mapping_id == Guid.Parse("5e34ae60-1d72-4efd-8440-c4442515aabe"))
                {
                    planning.remarks = txtAdditionalPresNotes.Text;
                }
                else if (planning.soap_mapping_id == Guid.Parse("2A20FD9B-0911-4FBB-B8F0-84CE19C743E9"))
                {
                    if (txtclinicaldiagnosis.Text.Length >= 400)
                    {
                        planning.remarks = txtclinicaldiagnosis.Text.Substring(0, 400);
                    }
                    else
                    {
                        planning.remarks = txtclinicaldiagnosis.Text;
                    }
                }
                else if (planning.soap_mapping_id == Guid.Parse("5A96F8FA-CCD9-4E75-AD96-148A36F06685"))
                {
                    if (txtclinicaldiagnosis_FutureOrder.Text.Length >= 400)
                    {
                        planning.remarks = txtclinicaldiagnosis_FutureOrder.Text.Substring(0, 400);
                    }
                    else
                    {
                        planning.remarks = txtclinicaldiagnosis_FutureOrder.Text;
                    }
                }
                // Diagnostic and procedure
                
                else if (planning.soap_mapping_id == Guid.Parse("35779378-FC19-41B5-8445-D6C6D358BDE5"))
                {
                    planning.remarks = txtPlanningOtherDiagnostic.Text;
                }
                else if (planning.soap_mapping_id == Guid.Parse("E4565CFB-1E9E-47EC-A06E-F21240043289"))
                {
                    planning.remarks = txtPlanningOtherProcedure.Text;
                }
                else if (planning.soap_mapping_id == Guid.Parse("DFA41B67-0EDC-45A8-BD69-5E6883FADEF2"))
                {
                    // diagnostic future order
                    if (dp_diag.Text != "")
                    {
                        planning.value = (DateTime.Parse(dp_diag.Text)).ToString("yyyy-MM-dd");
                        planning.remarks = txtplanningotherDiagnostic_FutureOrder.Text;
                    }
                }
                else if (planning.soap_mapping_id == Guid.Parse("68C2FF43-C93B-4FFB-84FF-C7BEBECA72C5"))
                {
                    // procedure future order
                    if (dp_proc.Text != "")
                    {
                        planning.value = (DateTime.Parse(dp_proc.Text)).ToString("yyyy-MM-dd");
                        planning.remarks = txtplanningotherProcedure_FutureOrder.Text;
                    }

                }

            }

            ////listobjective.Find(y => y.soap_mapping_id == Guid.Parse("AE3CA8C2-EAB0-41B6-9E3E-3ECB8071A9D0")).value
            //SOA.subjective.Find(y => y.soap_mapping_id == Guid.Parse("bc0c06ae-7085-4e15-8e73-b3bb104a66f1")).remarks = txtDocNurseNotes.Text;
            foreach (var subjective in SOA.subjective)
            {
                if (subjective.soap_mapping_id == Guid.Parse("bc0c06ae-7085-4e15-8e73-b3bb104a66f1"))
                {
                    subjective.remarks = txtDocNurseNotes.Text;
                }
            }

            if (Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] == null && Session[Helper.Sessionradcheck + hfguidadditional.Value] == null)
            {
                SOA.cpoe_trans.Clear();
            }
            else
            {
                List<CpoeTrans> datalab = new List<CpoeTrans>();
                List<CpoeTrans> datarad = new List<CpoeTrans>();
                List<CpoeTrans> datalabrad = new List<CpoeTrans>();

                if (Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] != null)
                {
                    datalab = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];

                    var duplicatedlab =
                            from p in datalab
                            group p by new { p.id, p.IsFutureOrder } into g
                            where g.Count() > 1
                            select g.Key;

                    if (duplicatedlab.Count() > 0)
                    {
                        datalab = datalab.GroupBy(i => i.id).Select(g => g.First()).ToList();
						
                    }

                    foreach (CpoeTrans x in datalab)
                    {
                        if (x.IsFutureOrder == true)
                        {
                            x.FutureOrderDate = dp_labFutureOrder.Text != "" ? DateTime.Parse(dp_labFutureOrder.Text) : DateTime.Now;
                        }
                    }
                }
                if (Session[Helper.Sessionradcheck + hfguidadditional.Value] != null)
                {
                    datarad = (List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value];

                    var duplicatedrad =
                            from p in datarad
                            group p by new { p.id, p.IsFutureOrder } into g
                            where g.Count() > 1
                            select g.Key;

                    if (duplicatedrad.Count() > 0)
                    {
                        datarad = datarad.GroupBy(i => i.id).Select(g => g.First()).ToList();
                        
                    }

                    foreach (CpoeTrans x in datarad)
                    {
                        if (x.IsFutureOrder == true)
                        {
                            x.FutureOrderDate = dp_radFutureOrder.Text != "" ? DateTime.Parse(dp_radFutureOrder.Text) : DateTime.Now;
                        }
                    }

                    if (chkcitorad.Checked)
                    {
                        datarad.Select(c => { c.iscito = 1; return c; }).ToList();
                    }
                    else
                        datarad.Select(c => { c.iscito = 0; return c; }).ToList();

                }
                datalabrad.AddRange(datalab);
                datalabrad.AddRange(datarad);
                SOA.cpoe_trans = datalabrad;
            }

            
            SOA.cpoe_notes = stdclinic.getvaluesnotes(SOA.cpoe_notes);
            SOA.cpoe_notes = stdmicro.getvaluesnotes(SOA.cpoe_notes);
            SOA.cpoe_notes = stdcito.getvaluesnotes(SOA.cpoe_notes);
            SOA.cpoe_notes = stdmdc.getvaluesnotes(SOA.cpoe_notes);

            SOA.cpoe_notes = stdctrad.getvaluesnotes(SOA.cpoe_notes);
            SOA.cpoe_notes = stdmrifull.getvaluesnotes(SOA.cpoe_notes);
            SOA.cpoe_notes = stdmrihalf.getvaluesnotes(SOA.cpoe_notes);
            SOA.cpoe_notes = stdusg.getvaluesnotes(SOA.cpoe_notes);
            SOA.cpoe_notes = stdxray.getvaluesnotes(SOA.cpoe_notes);

            List<Prescription> datadrugs = GetRowList(1);
            //List<Prescription> datacompheader = GetRowList(2);
            //List<Prescription> datacompdetail = GetRowList(3);
            List<Prescription> dataconsumables = GetRowList(5);

            //datadrugs.AddRange(datacompheader);
            //datadrugs.AddRange(datacompdetail);
            datadrugs.AddRange(dataconsumables);

            SOA.prescription = datadrugs;

            List<Prescription> data_additional_drugs = GetRowList(6);
            List<Prescription> data_additional_cons = GetRowList(7);
            data_additional_drugs.AddRange(data_additional_cons);

            SOA.additional_prescription = data_additional_drugs;

            DataTable dataRacikanHeader = (DataTable)Session[Helper.SessionRacikanHeader + hfguidadditional.Value];
            DataTable dataRacikanDetail = (DataTable)Session[Helper.SessionRacikanDetail + hfguidadditional.Value];
            DataTable dataRacikanHeader_add = (DataTable)Session[Helper.SessionRacikanHeaderAdd + hfguidadditional.Value];
            DataTable dataRacikanDetail_add = (DataTable)Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value];

            //if ((dataRacikanHeader == null || dataRacikanHeader.Rows.Count == 0) && (dataRacikanHeader_add == null || dataRacikanHeader_add.Rows.Count == 0))
            //{
            //    SOA.compound_header = new List<CompoundHeaderSoap>();
            //    SOA.compound_detail = new List<CompoundDetailSoap>();
            //}
            //else
            //{
                SOA.compound_header = new List<CompoundHeaderSoap>();
                SOA.compound_detail = new List<CompoundDetailSoap>();

                if (dataRacikanHeader != null && dataRacikanHeader.Rows.Count != 0)
                {
                    SOA.compound_header.AddRange(GetRowListRacikanHeader());
                    if (dataRacikanDetail != null && dataRacikanDetail.Rows.Count != 0)
                    {
                        SOA.compound_detail.AddRange(Helper.ToDataList<CompoundDetailSoap>(dataRacikanDetail));
                    }
                    
                }

                if (dataRacikanHeader_add != null && dataRacikanHeader_add.Rows.Count != 0)
                {
                    SOA.compound_header.AddRange(GetRowListRacikanHeaderAdditional());
                    if (dataRacikanDetail_add != null && dataRacikanDetail_add.Rows.Count != 0)
                    {
                        SOA.compound_detail.AddRange(Helper.ToDataList<CompoundDetailSoap>(dataRacikanDetail_add));
                    }
                }
            //}

            // Set Date Future Diagnostic and Future Procedure
            foreach (var item in SOA.procedure_diagnosis)
            {
                if(item.ProcedureItemTypeId == 4 && item.IsFutureOrder == true )
                {
                    item.FutureOrderDate = DateTime.Parse(dp_diag.Text);
                }

                if (item.ProcedureItemTypeId == 5 && item.IsFutureOrder == true)
                {
                    item.FutureOrderDate = DateTime.Parse(dp_proc.Text);
                }
            }

           


            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetPlanningValues", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetPlanningValues", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());

        return SOA;
    }


    //=======================================================TAMBAHAN=================================================================//

    protected void ButtonAjaxSearchDrug_Click(object sender, EventArgs e)
    {

        DataTable DrugSelect;
        DrugSelect = ((DataTable)Session[Helper.SessionItemDrugPres]).Select("salesItemId = '" + HF_ItemSelecteddrug.Value + "'").CopyToDataTable();

        List<PatientRoutineMedication> dataroutine = (List<PatientRoutineMedication>)Session[Helper.Sessionroutinemedication + hfguidadditional.Value];

        List<Prescription> data = GetRowList(1);
        string qty = "0";

        List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
        if (orgsetting.Where(y => y.setting_name.ToLower() == "DEFAULT_QTY".ToLower()).Count() > 0)
            qty = orgsetting.Find(y => y.setting_name.ToUpper() == "DEFAULT_QTY".ToUpper()).setting_value;

        if (DrugSelect.Rows.Count > 0)
        {
            for (int i = 0; i < DrugSelect.Rows.Count; i++)
            {
                Prescription temp = new Prescription();
                temp.prescription_id = Guid.Empty;
                temp.prescription_no = "";
                temp.item_id = long.Parse(DrugSelect.Rows[i]["SalesItemID"].ToString());
                temp.item_name = DrugSelect.Rows[i]["SalesItemName"].ToString();
                temp.quantity = qty;
                temp.uom_id = long.Parse(DrugSelect.Rows[i]["SalesUomId"].ToString());
                temp.uom_code = DrugSelect.Rows[i]["SalesUomCode"].ToString();
                temp.frequency_id = long.Parse(DrugSelect.Rows[i]["AdministrationFrequencyId"].ToString());
                temp.frequency_code = "";
                temp.dosage_id = DrugSelect.Rows[i]["Dose"].ToString();
                temp.is_iter = bool.Parse(DrugSelect.Rows[i]["IsIter"].ToString());

                temp.dosage_id = temp.dosage_id.ToString().Replace(',', '.');
                string[] tempqty = temp.dosage_id.ToString().Split('.');
                if (tempqty[1].Length == 3)
                {
                    if (tempqty[1] == "000")
                    {
                        temp.dosage_id = decimal.Parse(tempqty[0]).ToString();
                    }
                    else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                    {
                        temp.dosage_id = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                    }
                    else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                    {
                        temp.dosage_id = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                    }
                }

                temp.dose_uom = "-";
                temp.dose_uom_id = long.Parse(DrugSelect.Rows[i]["DoseUomId"].ToString());
                temp.dose_text = ""; // DrugSelect.Rows[i]["DoseText"].ToString();
                temp.administration_route_id = long.Parse(DrugSelect.Rows[i]["AdministrationRouteId"].ToString());
                temp.administration_route_code = "";
                temp.iteration = 0;
                temp.remarks = DrugSelect.Rows[i]["AdministrationInstruction"].ToString();
                if (dataroutine != null)
                {
                    if (dataroutine.Any(y => y.routine_sales_item_id == long.Parse(DrugSelect.Rows[i]["SalesItemID"].ToString())))
                        temp.is_routine = 1;
                    else
                        temp.is_routine = 0;
                }
                else
                    temp.is_routine = 0;

                temp.is_consumables = 0;
                temp.compound_id = Guid.Empty;
                temp.compound_name = "";
                temp.origin_prescription_id = Guid.Empty;
                temp.hope_arinvoice_id = 0;
                temp.is_delete = 0;
                temp.IsDoseText = false;

                if (temp != null)
                {
                    DataTable dttemp = Helper.ToDataTable(data);
                    if (dttemp.Select("item_name = '" + temp.item_name + "'").Count() == 0)
                    {
                        data.Add(temp);
                        DataTable dta = Helper.ToDataTable(data);
                        Session[Helper.SessionDrugPres + hfguidadditional.Value] = dta;
                        gvw_drug.DataSource = dta;
                        gvw_drug.DataBind();
                    }
                    else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);

                }
            }
            HyperLinkSaveOrderSet.Style.Add("display", "");
        }
        
        txtItemAdd_AC.Text = "";
        UpdatePanelListPrescription.Update();
        upSaveAsOrderSet.Update();
        upFormularium.Update();
    }

    protected void ButtonAjaxSearchDrug_add_Click(object sender, EventArgs e)
    {
        DataTable AddDrugSelect;
        AddDrugSelect = ((DataTable)Session[Helper.SessionItemDrugPres]).Select("salesItemId = '" + HF_ItemSelecteddrug_add.Value + "'").CopyToDataTable();

        List<PatientRoutineMedication> dataroutine = (List<PatientRoutineMedication>)Session[Helper.Sessionroutinemedication + hfguidadditional.Value];

        List<Prescription> data = GetRowList(6);
        string qty = "0";

        List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
        if (orgsetting.Where(y => y.setting_name.ToLower() == "DEFAULT_QTY".ToLower()).Count() > 0)
            qty = orgsetting.Find(y => y.setting_name.ToUpper() == "DEFAULT_QTY".ToUpper()).setting_value;

        if (AddDrugSelect.Rows.Count > 0)
        {
            for (int i = 0; i < AddDrugSelect.Rows.Count; i++)
            {
                Prescription temp = new Prescription();
                temp.prescription_id = Guid.Empty;
                temp.prescription_no = "";
                temp.item_id = long.Parse(AddDrugSelect.Rows[i]["SalesItemID"].ToString());
                temp.item_name = AddDrugSelect.Rows[i]["SalesItemName"].ToString();
                temp.quantity = qty;
                temp.uom_id = long.Parse(AddDrugSelect.Rows[i]["SalesUomId"].ToString());
                temp.uom_code = AddDrugSelect.Rows[i]["SalesUomCode"].ToString();
                temp.frequency_id = long.Parse(AddDrugSelect.Rows[i]["AdministrationFrequencyId"].ToString());
                temp.frequency_code = "";
                temp.dosage_id = AddDrugSelect.Rows[i]["Dose"].ToString();
                temp.is_iter = bool.Parse(AddDrugSelect.Rows[i]["IsIter"].ToString());

                temp.dosage_id = temp.dosage_id.ToString().Replace(',', '.');
                string[] tempqty = temp.dosage_id.ToString().Split('.');
                if (tempqty[1].Length == 3)
                {
                    if (tempqty[1] == "000")
                    {
                        temp.dosage_id = decimal.Parse(tempqty[0]).ToString();
                    }
                    else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                    {
                        temp.dosage_id = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                    }
                    else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                    {
                        temp.dosage_id = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                    }
                }

                temp.dose_uom = "-";
                temp.dose_uom_id = long.Parse(AddDrugSelect.Rows[i]["DoseUomId"].ToString());
                temp.dose_text = ""; // AddDrugSelect.Rows[i]["DoseText"].ToString();
                temp.administration_route_id = long.Parse(AddDrugSelect.Rows[i]["AdministrationRouteId"].ToString());
                temp.administration_route_code = "";
                temp.iteration = 0;
                temp.remarks = AddDrugSelect.Rows[i]["AdministrationInstruction"].ToString();
                if (dataroutine != null)
                {
                    if (dataroutine.Any(y => y.routine_sales_item_id == long.Parse(AddDrugSelect.Rows[i]["SalesItemID"].ToString())))
                        temp.is_routine = 1;
                    else
                        temp.is_routine = 0;
                }
                else
                    temp.is_routine = 0;

                temp.is_consumables = 0;
                temp.compound_id = Guid.Empty;
                temp.compound_name = "";
                temp.origin_prescription_id = Guid.Empty;
                temp.hope_arinvoice_id = 0;
                temp.is_delete = 0;
                temp.IsDoseText = false;

                if (temp != null)
                {
                    DataTable dttemp = Helper.ToDataTable(data);
                    if (dttemp.Select("item_name = '" + temp.item_name + "'").Count() == 0)
                    {
                        data.Add(temp);
                        DataTable dta = Helper.ToDataTable(data);
                        Session[Helper.Sessionadditionalpres + hfguidadditional.Value] = dta;
                        gvwAdditionalDrugs.DataSource = dta;
                        gvwAdditionalDrugs.DataBind();
                    }
                    else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);

                }
            }
        }

        txtItemAdd_AC_additional.Text = "";
        UpdatePanelListPrescriptionAdd.Update();
    }

    protected void ButtonAjaxSearchCons_Click(object sender, EventArgs e)
    {
        DataTable ConsSelect;
        ConsSelect = ((DataTable)Session[Helper.SessionDrugsConsumables]).Select("salesItemId = '" + HF_ItemSelectedcons.Value + "'").CopyToDataTable();

        List<Prescription> data = GetRowList(5);
        string qty = "0";

        List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
        if (orgsetting.Where(y => y.setting_name.ToLower() == "DEFAULT_QTY".ToLower()).Count() > 0)
            qty = orgsetting.Find(y => y.setting_name.ToUpper() == "DEFAULT_QTY".ToUpper()).setting_value;

        if (ConsSelect.Rows.Count > 0)
        {
            for (int i = 0; i < ConsSelect.Rows.Count; i++)
            {
                Prescription temp = new Prescription();

                temp.prescription_id = Guid.Empty;
                temp.prescription_no = "";
                temp.item_id = long.Parse(ConsSelect.Rows[i]["SalesItemID"].ToString());
                temp.item_name = ConsSelect.Rows[i]["SalesItemName"].ToString();
                temp.quantity = qty;
                temp.uom_id = long.Parse(ConsSelect.Rows[i]["SalesUomId"].ToString());
                temp.uom_code = ConsSelect.Rows[i]["SalesUomCode"].ToString();
                temp.frequency_id = 0;
                temp.frequency_code = "";
                temp.dosage_id = "0";
                temp.dose_text = "";
                temp.administration_route_id = 0;
                temp.administration_route_code = "";
                temp.iteration = 0;
                temp.remarks = "";
                temp.is_routine = 0;
                temp.is_consumables = 1;
                temp.compound_id = Guid.Empty;
                temp.compound_name = "";
                temp.origin_prescription_id = Guid.Empty;
                temp.hope_arinvoice_id = 0;
                temp.is_delete = 0; 

                if (temp != null)
                {
                    DataTable dttemp = Helper.ToDataTable(data);
                    if (dttemp.Select("item_name = '" + temp.item_name + "'").Count() == 0)
                    {
                        data.Add(temp);
                        DataTable dta = Helper.ToDataTable(data);
                        Session[Helper.SessionConsumablesList + hfguidadditional.Value] = dta;
                        gvw_consumables.DataSource = dta;
                        gvw_consumables.DataBind();
                    }
                    else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);

                }
            }
        }

        txtItemCons_AC.Text = "";
        UpdatePanelConsumable.Update();
    }

    protected void ButtonAjaxSearchCons_add_Click(object sender, EventArgs e)
    {
        DataTable ConsSelect;
        ConsSelect = ((DataTable)Session[Helper.SessionDrugsConsumables]).Select("salesItemId = '" + HF_ItemSelectedcons_add.Value + "'").CopyToDataTable();

        List<Prescription> data = GetRowList(7);
        string qty = "0";

        List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
        if (orgsetting.Where(y => y.setting_name.ToLower() == "DEFAULT_QTY".ToLower()).Count() > 0)
            qty = orgsetting.Find(y => y.setting_name.ToUpper() == "DEFAULT_QTY".ToUpper()).setting_value;

        if (ConsSelect.Rows.Count > 0)
        {
            for (int i = 0; i < ConsSelect.Rows.Count; i++)
            {
                Prescription temp = new Prescription();

                temp.prescription_id = Guid.Empty;
                temp.prescription_no = "";
                temp.item_id = long.Parse(ConsSelect.Rows[i]["SalesItemID"].ToString());
                temp.item_name = ConsSelect.Rows[i]["SalesItemName"].ToString();
                temp.quantity = qty;
                temp.uom_id = long.Parse(ConsSelect.Rows[i]["SalesUomId"].ToString());
                temp.uom_code = ConsSelect.Rows[i]["SalesUomCode"].ToString();
                temp.frequency_id = 0;
                temp.frequency_code = "";
                temp.dosage_id = "0";
                temp.dose_text = "";
                temp.administration_route_id = 0;
                temp.administration_route_code = "";
                temp.iteration = 0;
                temp.remarks = "";
                temp.is_routine = 0;
                temp.is_consumables = 1;
                temp.compound_id = Guid.Empty;
                temp.compound_name = "";
                temp.origin_prescription_id = Guid.Empty;
                temp.hope_arinvoice_id = 0;
                temp.is_delete = 0;

                if (temp != null)
                {
                    DataTable dttemp = Helper.ToDataTable(data);
                    if (dttemp.Select("item_name = '" + temp.item_name + "'").Count() == 0)
                    {
                        data.Add(temp);
                        DataTable dta = Helper.ToDataTable(data);
                        Session[Helper.SessionConsumablesListAdd + hfguidadditional.Value] = dta;
                        gvw_add_cons.DataSource = dta;
                        gvw_add_cons.DataBind();
                    }
                    else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);

                }
            }
        }

        txtItemCons_AC_additional.Text = "";
        UpdatePanelConsumableAdd.Update();
    }

    protected void ButtonAjaxSearchCPOE_LAB_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            DataTable LabSelect = new DataTable();
            var buttonSearchID_Lab = ((Button)sender).ID;
            if (buttonSearchID_Lab == "ButtonAjaxSearchCPOE_LAB")
            {
            LabSelect = ((DataTable)Session[Helper.SessionItemAllCPOE]).Select("template_name = 'LAB' AND id = '" + HF_ItemSelectedcpoelab.Value + "' AND type <> 'CitoLab'").CopyToDataTable();
            }
            else if (buttonSearchID_Lab == "ButtonAjaxSearchCPOE_LAB_FutureOrder")
            {
                LabSelect = ((DataTable)Session[Helper.SessionItemAllCPOE]).Select("template_name = 'LAB' AND id = '" + HF_ItemSelectedcpoelab_FutureOrder.Value + "' AND type <> 'CitoLab'").CopyToDataTable();
            }


            List<CpoeTrans> listexcludetrans = new List<CpoeTrans>();
            List<CpoeTrans> listnotexist = new List<CpoeTrans>();
            //List<CpoeTrans> listtempcpoetrans = new List<CpoeTrans>();
            List<CpoeTrans> listtempcpoetrans;

            bool flagFO;

            if (Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] == null)
            {
                listtempcpoetrans = new List<CpoeTrans>();
            }
            else
            {
                listtempcpoetrans = new List<CpoeTrans>();
                listtempcpoetrans = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];
            }

            CpoeTrans x = new CpoeTrans();
            x.id = long.Parse(LabSelect.Rows[0]["id"].ToString());
            x.name = LabSelect.Rows[0]["name"].ToString();
            x.type = LabSelect.Rows[0]["type"].ToString();
            x.remarks = LabSelect.Rows[0]["remarks"].ToString();
            x.isnew = int.Parse(LabSelect.Rows[0]["isnew"].ToString());
            x.iscito = int.Parse(LabSelect.Rows[0]["iscito"].ToString());
            x.issubmit = int.Parse(LabSelect.Rows[0]["issubmit"].ToString());
            x.isdelete = int.Parse(LabSelect.Rows[0]["isdelete"].ToString());
            x.ischeck = int.Parse(LabSelect.Rows[0]["ischeck"].ToString());
            x.IsSendHope = int.Parse(LabSelect.Rows[0]["IsSendHope"].ToString());


            if(buttonSearchID_Lab == "ButtonAjaxSearchCPOE_LAB")
			{
                HF_FlagFutureOrder.Value = "false";
                x.IsFutureOrder = false;
                x.FutureOrderDate = DateTime.Now;
            }
            else if(buttonSearchID_Lab == "ButtonAjaxSearchCPOE_LAB_FutureOrder")
			{
                HF_FlagFutureOrder.Value = "true";
                x.IsFutureOrder = true;
                if (dp_labFutureOrder.Text == "")
				{
                    x.FutureOrderDate = DateTime.Now;
				}
				else
				{
                    var dateFO = DateTime.Parse(dp_labFutureOrder.Text);
                    x.FutureOrderDate = dateFO;
                }
            }

            flagFO = bool.Parse(HF_FlagFutureOrder.Value);


            if (listtempcpoetrans != null)
            {
                if (listtempcpoetrans.Any(y => y.name == x.name && y.isdelete == 0 && y.IsFutureOrder == flagFO))
                {
                    listexcludetrans.Add(x);
                }
                else if (listtempcpoetrans.Any(y => y.name == x.name && y.isdelete == 1 && y.IsFutureOrder == flagFO))
                {
                    listtempcpoetrans.FirstOrDefault(z => z.name == x.name && z.IsFutureOrder == flagFO).isdelete = 0;
                }
                else if (listtempcpoetrans.Any(y => x.ischeck == 0 && y.IsFutureOrder== flagFO))
                {
                    listnotexist.Add(x);
                }
                else
                    listtempcpoetrans.Add(x);
            }
            else
            {
                listtempcpoetrans.Add(x);
            }

            Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = listtempcpoetrans;

            listchecked = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];
            if (listchecked != null)
            {

                if(buttonSearchID_Lab == "ButtonAjaxSearchCPOE_LAB")
				{
                labempty.Style.Add("display", "none");
                linklabbutton.Style.Add("display", "none");
                btnEditLab.Visible = true;
                btnResetLab.Visible = true;
                    if (Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0 AND IsFutureOrder = false").Count() > 0)
                {
                        DataTable dt = Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0 AND IsFutureOrder = false").CopyToDataTable();
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();

                    HyperLinkSaveAsLab.Style.Add("display", "");
                }
                else
                {
                    labempty.Style.Add("display", "");
                    linklabbutton.Style.Add("display", "");
                    btnEditLab.Visible = false;
                    btnResetLab.Visible = false;
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
                List<CpoeMapping> tempMap = new List<CpoeMapping>();
                tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
                //StdClinicControl.GetMappingClinicLab(tempMap, hfguidadditional.Value);
                    stdmicro.GetMappingMicroLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdcito.GetMappingCitoLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdclinic.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdanatomi.GetMappingAnatomiLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdpanel.GetMappingPanelLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdmdc.GetMappingMDC(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                }
                else if (buttonSearchID_Lab == "ButtonAjaxSearchCPOE_LAB_FutureOrder")
				{
                    labempty_FutureOrder.Style.Add("display", "none");
                    linklabbutton_FutureOrder.Style.Add("display", "none");
                    btnEditLab_FutureOrder.Visible = true;
                    btnResetLab_FutureOrder.Visible = true;
                    if (Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0 AND IsFutureOrder = true").Count() > 0)
                    {
                        DataTable dt = Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0 AND IsFutureOrder = true").CopyToDataTable();
                        Repeater1_FutureOrder.DataSource = dt;
                        Repeater1_FutureOrder.DataBind();

                        HyperLinkSaveAsLab_FutureOrder.Style.Add("display", "");
                    }
                    else
                    {
                        labempty_FutureOrder.Style.Add("display", "");
                        linklabbutton_FutureOrder.Style.Add("display", "");
                        btnEditLab_FutureOrder.Visible = false;
                        btnResetLab_FutureOrder.Visible = false;
                        Repeater1_FutureOrder.DataSource = null;
                        Repeater1_FutureOrder.DataBind();
                    }
                    List<CpoeMapping> tempMap = new List<CpoeMapping>();
                    tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
                    //StdClinicControl.GetMappingClinicLab(tempMap, hfguidadditional.Value);
                    stdmicro.GetMappingMicroLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdcito.GetMappingCitoLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdclinic.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdanatomi.GetMappingAnatomiLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdpanel.GetMappingPanelLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                    stdmdc.GetMappingMDC(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
                }


            }

            if (listexcludetrans.Count() > 0 || listnotexist.Count() > 0)
            {
                if (listexcludetrans.Count() > 0)
                {
                    DataTable dtexclude = Helper.ToDataTable(listexcludetrans);
                    rptExist.DataSource = dtexclude;
                    rptExist.DataBind();
                    lblExist.Visible = true;
                }
                else
                {
                    rptExist.DataSource = null;
                    rptExist.DataBind();
                    lblExist.Visible = false;
                }
                if (listnotexist.Count() > 0)
                {
                    DataTable dtnotexist = Helper.ToDataTable(listnotexist);
                    rptNotExist.DataSource = dtnotexist;
                    rptNotExist.DataBind();
                    lblNotExist.Visible = true;
                }
                else
                {
                    rptNotExist.DataSource = null;
                    rptNotExist.DataBind();
                    lblNotExist.Visible = false;
                }

                updatepanelexistlabrad.Update();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modallab", "$('#modallab').modal();", true);
            }

            txtItemCPOE_LAB.Text = "";
            txtItemCPOE_LAB_FutureOrder.Text = "";

            UpdatePanelDivLab.Update();
            UpdatePanelDivLab_FutureOrder.Update();
            UP_ContainerLab.Update();


            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonAjaxSearchCPOE_LAB_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonAjaxSearchCPOE_LAB_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonAjaxSearchCPOE_RAD_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            DataTable RadSelect = new DataTable();
            var buttonSearchID_Rad = ((Button)sender).ID;
            if(buttonSearchID_Rad == "ButtonAjaxSearchCPOE_RAD")
			{
            RadSelect = ((DataTable)Session[Helper.SessionItemAllCPOE]).Select("template_name = 'RAD' AND id = '" + HF_ItemSelectedcpoerad.Value + "' AND name = '" + HF_ItemSelectedcpoerad_name.Value + "' AND remarks = '" + HF_ItemSelectedcpoerad_remarks.Value + "'").CopyToDataTable();
			}
            else if(buttonSearchID_Rad == "ButtonAjaxSearchCPOE_RAD_FutureOrder")
			{
                RadSelect = ((DataTable)Session[Helper.SessionItemAllCPOE]).Select("template_name = 'RAD' AND id = '" + HF_ItemSelectedcpoerad_FutureOrder.Value + "' AND name = '" + HF_ItemSelectedcpoerad_name_FutureOrder.Value + "' AND remarks = '" + HF_ItemSelectedcpoerad_remarks_FutureOrder.Value + "'").CopyToDataTable();
            }

            List <CpoeTrans> listexcludetrans = new List<CpoeTrans>();
            List<CpoeTrans> listnotexist = new List<CpoeTrans>();
            //List<CpoeTrans> listtempcpoetrans = new List<CpoeTrans>();
            List<CpoeTrans> listtempcpoetrans;
            if (Session[Helper.Sessionradcheck + hfguidadditional.Value] == null)
            {
                listtempcpoetrans = new List<CpoeTrans>();
            }
            else
            {
                listtempcpoetrans = new List<CpoeTrans>();
                listtempcpoetrans = (List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value];
            }

            bool flagFO;

            CpoeTrans x = new CpoeTrans();
            x.id = long.Parse(RadSelect.Rows[0]["id"].ToString());
            x.name = RadSelect.Rows[0]["name"].ToString();
            x.type = RadSelect.Rows[0]["type"].ToString();
            x.remarks = RadSelect.Rows[0]["remarks"].ToString();
            x.isnew = int.Parse(RadSelect.Rows[0]["isnew"].ToString());
            x.iscito = int.Parse(RadSelect.Rows[0]["iscito"].ToString());
            x.issubmit = int.Parse(RadSelect.Rows[0]["issubmit"].ToString());
            x.isdelete = int.Parse(RadSelect.Rows[0]["isdelete"].ToString());
            x.ischeck = int.Parse(RadSelect.Rows[0]["ischeck"].ToString());
            x.IsSendHope = int.Parse(RadSelect.Rows[0]["IsSendHope"].ToString());

            if(buttonSearchID_Rad == "ButtonAjaxSearchCPOE_RAD")
			{
                HF_FlagFutureOrderRad.Value = "false";
                x.IsFutureOrder = false;
                x.FutureOrderDate = DateTime.Now;
			}
            else if (buttonSearchID_Rad == "ButtonAjaxSearchCPOE_RAD_FutureOrder")
			{
                HF_FlagFutureOrderRad.Value = "true";
                x.IsFutureOrder = true;

                if(dp_radFutureOrder.Text == "")
				{
                    x.FutureOrderDate = DateTime.Now;
				}
				else
				{

					var dateFO = DateTime.Parse(dp_labFutureOrder.Text);
					x.FutureOrderDate = dateFO;
                }
            }

            flagFO = bool.Parse(HF_FlagFutureOrderRad.Value);

            if (listtempcpoetrans != null)
            {
                if (listtempcpoetrans.Any(y => y.name == x.name && y.isdelete == 0 && y.IsFutureOrder == flagFO))
                {
                    listexcludetrans.Add(x);
                }
                else if (listtempcpoetrans.Any(y => y.name == x.name && y.isdelete == 1 && y.IsFutureOrder == flagFO))
                {
                    listtempcpoetrans.FirstOrDefault(z => z.name == x.name && z.IsFutureOrder == flagFO).isdelete = 0;
                }
                else if (listtempcpoetrans.Any(y => x.ischeck == 0 && y.IsFutureOrder == flagFO))
                {
                    listnotexist.Add(x);
                }
                else
                    listtempcpoetrans.Add(x);
            }
            else
            {
                listtempcpoetrans.Add(x);
            }

            Session[Helper.Sessionradcheck + hfguidadditional.Value] = listtempcpoetrans;

            listchecked = (List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value];
            if (listchecked != null)
            {
                if (buttonSearchID_Rad == "ButtonAjaxSearchCPOE_RAD")
                {
                radempty.Style.Add("display", "none");
                linkradbutton.Style.Add("display", "none");
                btnEditRad.Visible = true;
                btnResetRad.Visible = true;
                divcitorad.Visible = true;
                    if (Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0 and IsFutureOrder = false").Count() > 0)
                {
                    //DataTable dt = Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0").CopyToDataTable();
                    //rptRadiology.DataSource = dt;
                    //rptRadiology.DataBind();

                    List<CpoeTrans> listcheckedTemp = new List<CpoeTrans>();
                    foreach (var list in listchecked)
                    {
                        CpoeTrans temp = new CpoeTrans();
                        temp.id = list.id;
                        temp.ischeck = list.ischeck;
                        temp.iscito = list.iscito;
                        temp.isdelete = list.isdelete;
                        temp.isnew = list.isnew;
                        temp.issubmit = list.issubmit;
                        if (list.remarks != "")
                            temp.name = list.name + " (" + list.remarks + ")";
                        else
                            temp.name = list.name;

                        temp.type = temp.type;
                        temp.remarks = list.remarks;
                        temp.IsSendHope = list.IsSendHope;
                            temp.IsFutureOrder = list.IsFutureOrder;
                            temp.FutureOrderDate = list.FutureOrderDate;
                        listcheckedTemp.Add(temp);
                    }
                        DataTable dt = Helper.ToDataTable(listcheckedTemp).Select("isdelete = 0 and ischeck <> 0 and IsFutureOrder = false").CopyToDataTable();
                    rptRadiology.DataSource = dt;
                    rptRadiology.DataBind();
                }
                else
                {
                    radempty.Style.Add("display", "");
                    linkradbutton.Style.Add("display", "");
                    btnEditRad.Visible = false;
                    btnResetRad.Visible = false;
                    divcitorad.Visible = false;
                    rptRadiology.DataSource = null;
                    rptRadiology.DataBind();
                }
                List<CpoeMapping> tempMap = new List<CpoeMapping>();
                tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
                    stdxray.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                    stdusg.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                    stdctrad.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                    stdmrihalf.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                    stdmrifull.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                }
                else if (buttonSearchID_Rad == "ButtonAjaxSearchCPOE_RAD_FutureOrder")
                {
                    radempty_FutureOrder.Style.Add("display", "none");
                    linkradbutton_FutureOrder.Style.Add("display", "none");
                    btnEditRad_FutureOrder.Visible = true;
                    btnResetRad_FutureOrder.Visible = true;
                    divcitorad_FutureOrder.Visible = true;
                    if (Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0 and IsFutureOrder = true").Count() > 0)
                    {
                        //DataTable dt = Helper.ToDataTable(listchecked).Select("isdelete = 0 and ischeck <> 0").CopyToDataTable();
                        //rptRadiology.DataSource = dt;
                        //rptRadiology.DataBind();

                        List<CpoeTrans> listcheckedTemp = new List<CpoeTrans>();
                        foreach (var list in listchecked)
                        {
                            CpoeTrans temp = new CpoeTrans();
                            temp.id = list.id;
                            temp.ischeck = list.ischeck;
                            temp.iscito = list.iscito;
                            temp.isdelete = list.isdelete;
                            temp.isnew = list.isnew;
                            temp.issubmit = list.issubmit;
                            if (list.remarks != "")
                                temp.name = list.name + " (" + list.remarks + ")";
                            else
                                temp.name = list.name;

                            temp.type = temp.type;
                            temp.remarks = list.remarks;
                            temp.IsSendHope = list.IsSendHope;
                            temp.IsFutureOrder = list.IsFutureOrder;
                            temp.FutureOrderDate = list.FutureOrderDate;
                            listcheckedTemp.Add(temp);
                        }
                        DataTable dt = Helper.ToDataTable(listcheckedTemp).Select("isdelete = 0 and ischeck <> 0 and IsFutureOrder = true").CopyToDataTable();
                        rptRadiology_FutureOrder.DataSource = dt;
                        rptRadiology_FutureOrder.DataBind();
                    }
                    else
                    {
                        radempty_FutureOrder.Style.Add("display", "");
                        linkradbutton_FutureOrder.Style.Add("display", "");
                        btnEditRad_FutureOrder.Visible = false;
                        btnResetRad_FutureOrder.Visible = false;
                        divcitorad_FutureOrder.Visible = false;
                        rptRadiology_FutureOrder.DataSource = null;
                        rptRadiology_FutureOrder.DataBind();
                    }
                    List<CpoeMapping> tempMap = new List<CpoeMapping>();
                    tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
                    stdxray.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                    stdusg.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                    stdctrad.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                    stdmrihalf.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                    stdmrifull.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
                }
                
            }

            if (listexcludetrans.Count() > 0 || listnotexist.Count() > 0)
            {
                if (listexcludetrans.Count() > 0)
                {
                    DataTable dtexclude = Helper.ToDataTable(listexcludetrans);
                    rptExist.DataSource = dtexclude;
                    rptExist.DataBind();
                    lblExist.Visible = true;
                }
                else
                {
                    rptExist.DataSource = null;
                    rptExist.DataBind();
                    lblExist.Visible = false;
                }
                if (listnotexist.Count() > 0)
                {
                    DataTable dtnotexist = Helper.ToDataTable(listnotexist);
                    rptNotExist.DataSource = dtnotexist;
                    rptNotExist.DataBind();
                    lblNotExist.Visible = true;
                }
                else
                {
                    rptNotExist.DataSource = null;
                    rptNotExist.DataBind();
                    lblNotExist.Visible = false;
                }

                updatepanelexistlabrad.Update();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalrad", "$('#modallab').modal();", true);
            }

            txtItemCPOE_RAD.Text = "";
            txtItemCPOE_RAD_FutureOrder.Text = "";

            UpdatePanelDivRad.Update();
            UpdatePanelDivRad_FutureOrder.Update();
            UP_ContainerRad.Update();


            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonAjaxSearchCPOE_RAD_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonAjaxSearchCPOE_RAD_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btndeletelab_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            ImageButton buttonDelete = (ImageButton)sender;
            string buttonDeleteID = buttonDelete.ID;


            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

            HiddenField lab_id = new HiddenField();
            if(buttonDeleteID == "btndeletelab")
			{
                lab_id = (HiddenField)Repeater1.Rows[selRowIndex].FindControl("hf_id_lab");
			}
            else if(buttonDeleteID == "btndeletelab_FutureOrder")
			{
                lab_id = (HiddenField)Repeater1_FutureOrder.Rows[selRowIndex].FindControl("hf_id_lab_FutureOrder");

			}

            List<CpoeTrans> listcheck = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];

            //var item = listcheck.First(x => x.id == long.Parse(lab_id.Value.ToString()));
            //listcheck.Remove(item);

            var item = new CpoeTrans();
            var flagitem = 0;

            foreach (var x in listcheck.Where(x => x.type == "ClinicLab" || x.type == "CitoLab" || x.type == "MicroLab" || x.type == "PatologiLab" || x.type == "PanelLab" || x.type == "MDCLab"))
            {
                if (x.issubmit == 0)
                {
                    if (x.id == long.Parse(lab_id.Value.ToString()))
                    {
                        if (x.isnew == 0)
                        {
                            x.isdelete = 1;
                        }
                        else if(x.isnew == 1)
                        {
                            item = x;
                            flagitem = 1;
                        }
                    }
                }
            }
            if (flagitem == 1)
            {
                listcheck.Remove(item);
                flagitem = 0;
            }

            Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = listcheck;

            List<CpoeMapping> tempMap = new List<CpoeMapping>();
            tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
            stdmicro.GetMappingMicroLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdcito.GetMappingCitoLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdclinic.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdanatomi.GetMappingAnatomiLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdpanel.GetMappingPanelLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdmdc.GetMappingMDC(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);

            if (Helper.ToDataTable(listcheck).Select("isdelete = 0").Count() > 0)
            {
                if(buttonDeleteID == "btndeletelab") 
                {
                    var listcheckNotFO = listcheck.Where(x => x.IsFutureOrder == false).ToList();
                    if(Helper.ToDataTable(listcheckNotFO).Select("isdelete=0").Count() > 0)
					{
                        Repeater1.DataSource = Helper.ToDataTable(listcheckNotFO).Select("isdelete = 0").CopyToDataTable();
                        Repeater1.DataBind();
                        HyperLinkSaveAsLab.Style.Add("display", "");
					}
					else
					{
                        labempty.Style.Add("display", "");
                        linklabbutton.Style.Add("display", "");
                        btnEditLab.Visible = false;
                        btnResetLab.Visible = false;

                        Repeater1.DataSource = null;
                        Repeater1.DataBind();

                        HyperLinkSaveAsLab.Style.Add("display", "none");
                        UpdatePanelDivLab.Update();
                    }

                }
                else if(buttonDeleteID == "btndeletelab_FutureOrder")
				{
                    var listcheckFO = listcheck.Where(x => x.IsFutureOrder == true).ToList();
                    if (Helper.ToDataTable(listcheckFO).Select("isdelete = 0").Count() > 0)
					{
                        Repeater1_FutureOrder.DataSource = Helper.ToDataTable(listcheckFO).Select("isdelete = 0").CopyToDataTable();
                        Repeater1_FutureOrder.DataBind();
                        HyperLinkSaveAsLab_FutureOrder.Style.Add("display", "");
					}
					else
					{
                        labempty_FutureOrder.Style.Add("display", "");
                        linklabbutton_FutureOrder.Style.Add("display", "");
                        btnEditLab_FutureOrder.Visible = false;
                        btnResetLab_FutureOrder.Visible = false;

                        Repeater1_FutureOrder.DataSource = null;
                        Repeater1_FutureOrder.DataBind();

                        HyperLinkSaveAsLab_FutureOrder.Style.Add("display", "none");
                        UpdatePanelDivLab_FutureOrder.Update();
                    }
                }

            }
            else
            {
                if (buttonDeleteID == "btndeletelab")
                {
					labempty.Style.Add("display", "");
					linklabbutton.Style.Add("display", "");
					btnEditLab.Visible = false;
					btnResetLab.Visible = false;

					Repeater1.DataSource = null;
					Repeater1.DataBind();

					HyperLinkSaveAsLab.Style.Add("display", "none");
					UpdatePanelDivLab.Update();
				}
                else if(buttonDeleteID == "btndeletelab_FutureOrder")
				{
                    labempty_FutureOrder.Style.Add("display", "");
                    linklabbutton_FutureOrder.Style.Add("display", "");
                    btnEditLab_FutureOrder.Visible = false;
                    btnResetLab_FutureOrder.Visible = false;

                    Repeater1_FutureOrder.DataSource = null;
                    Repeater1_FutureOrder.DataBind();

                    HyperLinkSaveAsLab_FutureOrder.Style.Add("display", "none");
                    UpdatePanelDivLab_FutureOrder.Update();
                }
            }

            UP_ContainerLab.Update();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "setmandatory", "isMandatoryField();", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btndeletelab_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btndeletelab_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btndeleterad_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            var buttonDeleteID = ((ImageButton)sender).ID;
            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

            HiddenField rad_id = new HiddenField();
            var flagFO = bool.Parse(HF_FlagFutureOrderRad.Value);

            if(buttonDeleteID == "btndeleterad")
			{
                rad_id = (HiddenField)rptRadiology.Rows[selRowIndex].FindControl("hf_id_rad");
                flagFO = false;


            }
            else if (buttonDeleteID == "btndeleterad_FutureOrder")
			{
                rad_id = (HiddenField)rptRadiology_FutureOrder.Rows[selRowIndex].FindControl("hf_id_rad_FutureOrder");
                flagFO = true;
            }


            List<CpoeTrans> listcheck = (List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value];

            var item = new CpoeTrans();
            var flagitem = 0;

            foreach (var x in listcheck.Where(x => x.IsFutureOrder == flagFO && (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT")))
            {
                if (x.IsFutureOrder == flagFO && (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT"))
                {
                    if (x.issubmit == 0)
                    {
                        if (x.id == long.Parse(rad_id.Value.ToString()))
                        {
                            if (x.isnew == 0)
                            {
                                x.isdelete = 1;
                            }
                            else if (x.isnew == 1)
                            {
                                item = x;
                                flagitem = 1;
                            }
                        }
                    }
                
                }
            }
            if (flagitem == 1)
            {
                listcheck.Remove(item);
                flagitem = 0;
            }

            Session[Helper.Sessionradcheck + hfguidadditional.Value] = listcheck;
            List<CpoeMapping> tempMap = new List<CpoeMapping>();
            tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
            stdxray.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdusg.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdctrad.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdmrihalf.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
            stdmrifull.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);

            List<CpoeTrans> listcheckedshow = new List<CpoeTrans>();
            if(buttonDeleteID == "btndeleterad")
			{
                if (Helper.ToDataTable(listcheck).Select("isdelete = 0 AND IsFutureOrder = false").Count() > 0)
            {
                foreach (var list in listcheck)
                {
                    CpoeTrans temp = new CpoeTrans();
                    temp.id = list.id;
                    temp.ischeck = list.ischeck;
                    temp.iscito = list.iscito;
                    temp.isdelete = list.isdelete;
                    temp.isnew = list.isnew;
                    temp.issubmit = list.issubmit;
                    if (list.remarks != "")
                        temp.name = list.name + " (" + list.remarks + ")";
                    else
                        temp.name = list.name;

                    temp.type = list.type;
                    temp.remarks = list.remarks;
                    temp.IsSendHope = list.IsSendHope;
                        temp.IsFutureOrder = list.IsFutureOrder;
                        temp.FutureOrderDate = list.FutureOrderDate;
                    listcheckedshow.Add(temp);
                }

                    rptRadiology.DataSource = Helper.ToDataTable(listcheckedshow).Select("isdelete = 0 AND IsFutureOrder = false").CopyToDataTable();
                rptRadiology.DataBind();
                    if (listcheck.Where(y => y.iscito == 1 && y.isdelete == 0 && y.IsFutureOrder == flagFO).Count() > 0)
                {
                    chkcitorad.Checked = true;
                }
                else
                    chkcitorad.Checked = false;
            }
            else
            {
                radempty.Style.Add("display", "");
                linkradbutton.Style.Add("display", "");
                btnEditRad.Visible = false;
                btnResetRad.Visible = false;
                divcitorad.Visible = false;
                chkcitorad.Checked = false;
                rptRadiology.DataSource = null;
                rptRadiology.DataBind();

            }
            }
            else if(buttonDeleteID == "btndeleterad_FutureOrder")
			{
                if (Helper.ToDataTable(listcheck).Select("isdelete = 0 AND IsFutureOrder = true").Count() > 0)
                {
                    foreach (var list in listcheck)
                    {
                        CpoeTrans temp = new CpoeTrans();
                        temp.id = list.id;
                        temp.ischeck = list.ischeck;
                        temp.iscito = list.iscito;
                        temp.isdelete = list.isdelete;
                        temp.isnew = list.isnew;
                        temp.issubmit = list.issubmit;
                        if (list.remarks != "")
                            temp.name = list.name + " (" + list.remarks + ")";
                        else
                            temp.name = list.name;

                        temp.type = list.type;
                        temp.remarks = list.remarks;
                        temp.IsSendHope = list.IsSendHope;
                        temp.IsFutureOrder = list.IsFutureOrder;
                        temp.FutureOrderDate = list.FutureOrderDate;
                        listcheckedshow.Add(temp);
                    }

                    rptRadiology_FutureOrder.DataSource = Helper.ToDataTable(listcheckedshow).Select("isdelete = 0 AND IsFutureOrder = true").CopyToDataTable();
                    rptRadiology_FutureOrder.DataBind();
                    if (listcheck.Where(y => y.iscito == 1 && y.isdelete == 0 && y.IsFutureOrder == flagFO).Count() > 0)
                    {
                        chkcitorad_FutureOrder.Checked = true;
                    }
                    else
                        chkcitorad_FutureOrder.Checked = false;
                }
                else
                {
                    radempty_FutureOrder.Style.Add("display", "");
                    linkradbutton_FutureOrder.Style.Add("display", "");
                    btnEditRad_FutureOrder.Visible = false;
                    btnResetRad_FutureOrder.Visible = false;
                    divcitorad_FutureOrder.Visible = false;
                    chkcitorad_FutureOrder.Checked = false;
                    rptRadiology_FutureOrder.DataSource = null;
                    rptRadiology_FutureOrder.DataBind();

                }
            }

            UpdatePanelDivRad.Update();
            UpdatePanelDivRad_FutureOrder.Update();
            UP_ContainerRad.Update();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "setmandatory", "isMandatoryField();", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btndeleterad_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btndeleterad_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void LinkSaveAsOrderSet_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        Dictionary<string, string> logParam = new Dictionary<string, string>();

        List<Prescription> data_Drugs = GetRowList(1);
        long doctord = long.Parse(Helper.GetDoctorID(this.Parent.Page));

        logParam = new Dictionary<string, string>
        {
            { "Orderset_Name", TextBoxSaveAsOrderSetName.Text },
            { "Doctor_ID", doctord.ToString() }
        };
        //Log.Debug(LogConfig.LogStart("insertSaveAsOrderSet", logParam, LogConfig.JsonToString(data_Drugs)));
        var SaveAS = clsOrderSet.insertSaveAsOrderSet(TextBoxSaveAsOrderSetName.Text, doctord, data_Drugs);
        var JsongetSaveAS = (JObject)JsonConvert.DeserializeObject<dynamic>(SaveAS.Result);
        var Status = JsongetSaveAS.Property("status").Value.ToString();
        var Message = JsongetSaveAS.Property("data").Value.ToString();
        //Log.Debug(LogConfig.LogEnd("insertSaveAsOrderSet", Status, Message));

        if (Message.ToUpper() == "SUCCESS")
        {
            ShowToastr("Save as Order Set successful.", "Success", "Success");

            logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", Helper.GetDoctorID(this.Parent.Page) },
                { "Organization_ID", Helper.organizationId.ToString() }
            };
            //Log.Debug(LogConfig.LogStart("GetSOAPAdditionalInfo", logParam));
            var soapadditional = clsSOAP.GetSOAPAdditionalInfo(long.Parse(Helper.GetDoctorID(this.Parent.Page)), Helper.organizationId);
            var jsonsoapadditional = JsonConvert.DeserializeObject<ResultSOAPAdditionalInfo>(soapadditional.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("GetSOAPAdditionalInfo", jsonsoapadditional.Status, jsonsoapadditional.Message));
            SOAPAdditionalInfo additional = jsonsoapadditional.list;

            listordersetheader = additional.ordersetdrug;
            ordersetdt = Helper.ToDataTable(listordersetheader);

            gvw_orderset.DataSource = ordersetdt;
            gvw_orderset.DataBind();

            UpdatePanelOrderSet.Update();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "saveorderset", "warningnotificationOption(); toastr.warning('" + Message.ToLower() + " order set name <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Order Set Fail!');", true);
        }
        TextBoxSaveAsOrderSetName.Text = "";



        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "LinkSaveAsOrderSet_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void LinkSaveAsLab_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        Dictionary<string, string> logParam = new Dictionary<string, string>();

        List<CpoeTrans> data_lab = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];
        var data_lab_notFO = data_lab.Where(x => x.IsFutureOrder == false).Distinct().ToList();
        var data_lab_FO = data_lab.Where(x => x.IsFutureOrder == true).Distinct().ToList();
        long doctord = long.Parse(Helper.GetDoctorID(this.Parent.Page));

        string linkSaveAsLabID = ((LinkButton)sender).ID;

        if(linkSaveAsLabID == "LinkSaveAsLab")
		{
        logParam = new Dictionary<string, string>
        {
            { "Orderset_ID", Guid.Empty.ToString() },
            { "Orderset_Name", TextBoxSaveAsLabName.Text },
            { "Doctor_ID", doctord.ToString() }
        };
        //Log.Debug(LogConfig.LogStart("insertSaveAsOrderSetLab", logParam, LogConfig.JsonToString(data_lab)));
            var SaveAS = clsOrderSet.insertSaveAsOrderSetLab(Guid.Empty, TextBoxSaveAsLabName.Text, doctord, data_lab_notFO);
        var JsongetSaveAS = (JObject)JsonConvert.DeserializeObject<dynamic>(SaveAS.Result);
        var Status = JsongetSaveAS.Property("status").Value.ToString();
        var Message = JsongetSaveAS.Property("data").Value.ToString();
        //Log.Debug(LogConfig.LogEnd("insertSaveAsOrderSetLab", Status, Message));

        if (Message.ToUpper() == "SUCCESS")
        {
            ShowToastr("Save as Order Set successful.", "Success", "Success");

            logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", Helper.GetDoctorID(this.Parent.Page) },
                { "Organization_ID", Helper.organizationId.ToString() }
            };
            //Log.Debug(LogConfig.LogStart("GetSOAPAdditionalInfo", logParam));
            var soapadditional = clsSOAP.GetSOAPAdditionalInfo(long.Parse(Helper.GetDoctorID(this.Parent.Page)), Helper.organizationId);
            var jsonsoapadditional = JsonConvert.DeserializeObject<ResultSOAPAdditionalInfo>(soapadditional.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("GetSOAPAdditionalInfo", jsonsoapadditional.Status, jsonsoapadditional.Message));
            SOAPAdditionalInfo additional = jsonsoapadditional.list;

            listordersetlab = additional.ordersetlab;
            labsetdt = Helper.ToDataTable(listordersetlab);

            gvw_labset.DataSource = labsetdt;
            gvw_labset.DataBind();

            UpdatePanelLabSet.Update();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "saveorderset", "warningnotificationOption(); toastr.warning('" + Message.ToLower() + " order set name <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Order Set Fail!');", true);
        }
        TextBoxSaveAsLabName.Text = "";
        }
        else if(linkSaveAsLabID == "LinkSaveAsLab_FutureOrder")
		{
            logParam = new Dictionary<string, string>
            {
                { "Orderset_ID", Guid.Empty.ToString() },
                { "Orderset_Name", TextBoxSaveAsLabName_FutureOrder.Text },
                { "Doctor_ID", doctord.ToString() }
            };
            //Log.Debug(LogConfig.LogStart("insertSaveAsOrderSetLab", logParam, LogConfig.JsonToString(data_lab)));
            var SaveAS = clsOrderSet.insertSaveAsOrderSetLab(Guid.Empty, TextBoxSaveAsLabName_FutureOrder.Text, doctord, data_lab_FO);
            var JsongetSaveAS = (JObject)JsonConvert.DeserializeObject<dynamic>(SaveAS.Result);
            var Status = JsongetSaveAS.Property("status").Value.ToString();
            var Message = JsongetSaveAS.Property("data").Value.ToString();
            //Log.Debug(LogConfig.LogEnd("insertSaveAsOrderSetLab", Status, Message));

            if (Message.ToUpper() == "SUCCESS")
            {
                ShowToastr("Save as Order Set successful.", "Success", "Success");

                logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", Helper.GetDoctorID(this.Parent.Page) },
                { "Organization_ID", Helper.organizationId.ToString() }
            };
                //Log.Debug(LogConfig.LogStart("GetSOAPAdditionalInfo", logParam));
                var soapadditional = clsSOAP.GetSOAPAdditionalInfo(long.Parse(Helper.GetDoctorID(this.Parent.Page)), Helper.organizationId);
                var jsonsoapadditional = JsonConvert.DeserializeObject<ResultSOAPAdditionalInfo>(soapadditional.Result.ToString());
                //Log.Debug(LogConfig.LogEnd("GetSOAPAdditionalInfo", jsonsoapadditional.Status, jsonsoapadditional.Message));
                SOAPAdditionalInfo additional = jsonsoapadditional.list;

                listordersetlab = additional.ordersetlab;
                labsetdt = Helper.ToDataTable(listordersetlab);

                gvw_labset.DataSource = labsetdt;
                gvw_labset.DataBind();

                UpdatePanelLabSet.Update();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "saveorderset", "warningnotificationOption(); toastr.warning('" + Message.ToLower() + " order set name <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Order Set Fail!');", true);
            }
            TextBoxSaveAsLabName_FutureOrder.Text = "";
        }

        



        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "LinkSaveAsLab_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }


    



    //============================================ Modal Racikan ==================================================//

    void loadDDlModalRacikan()
    {
        DataTable dt_dosisunit = (DataTable)Session[Helper.Sessiondosage];
        inputddl_dosisunitRacikan.DataSource = dt_dosisunit;
        inputddl_dosisunitRacikan.DataTextField = "name";
        inputddl_dosisunitRacikan.DataValueField = "doseUomId";
        inputddl_dosisunitRacikan.DataBind();
        inputddl_dosisunitRacikan.Items.Insert(0, new ListItem("- SELECT -", "0"));
        inputddl_dosisunitRacikan.SelectedValue = dt_dosisunit.Rows[0]["doseUomId"].ToString();

        DataTable dt_frekuensi = (DataTable)Session[Helper.SessionFrequency];
        inputddl_frekuensiRacikan.DataSource = (DataTable)Session[Helper.SessionFrequency];
        inputddl_frekuensiRacikan.DataTextField = "name";
        inputddl_frekuensiRacikan.DataValueField = "administrationFrequencyId";
        inputddl_frekuensiRacikan.DataBind();
        inputddl_frekuensiRacikan.Items.Insert(0, new ListItem("- SELECT -", "0"));
        inputddl_frekuensiRacikan.SelectedValue = dt_frekuensi.Rows[0]["administrationFrequencyId"].ToString();

        DataTable dt_rute = (DataTable)Session[Helper.SessionRoute];
        inputddl_ruteRacikan.DataSource = dt_rute;
        inputddl_ruteRacikan.DataTextField = "name";
        inputddl_ruteRacikan.DataValueField = "administration_route_id";
        inputddl_ruteRacikan.DataBind();
        inputddl_ruteRacikan.Items.Insert(0, new ListItem("- SELECT -", "0"));
        inputddl_ruteRacikan.SelectedValue = dt_rute.Rows[0]["administration_route_id"].ToString();

        DataTable dt_unit = (DataTable)Session[Helper.Sessionuom];
        inputddl_unitRacikan.DataSource = dt_unit;
        inputddl_unitRacikan.DataTextField = "name";
        inputddl_unitRacikan.DataValueField = "uomId";
        inputddl_unitRacikan.DataBind();
        inputddl_unitRacikan.Items.Insert(0, new ListItem("- SELECT -", "0"));
        inputddl_unitRacikan.SelectedValue = dt_unit.Rows[0]["uomId"].ToString();
    }

    protected List<CompoundDetailSoap> GetRowListRacikanModal(string type)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        List<CompoundDetailSoap> data = new List<CompoundDetailSoap>();
        try
        {

            int countadd = 1;
            foreach (GridViewRow rows in input_gvw_racikan_detail.Rows)
            {
                HiddenField rac_detail_id = (HiddenField)rows.FindControl("HF_racikan_detail_id");
                HiddenField rac_header_id = (HiddenField)rows.FindControl("HF_racikan_header_id");
                HiddenField rac_item_id = (HiddenField)rows.FindControl("HF_racikan_item_id");
                HiddenField rac_uom_id = (HiddenField)rows.FindControl("HF_racikan_uom_id");
                Label rac_item_name = (Label)rows.FindControl("racikan_item_name");
                TextBox rac_qty = (TextBox)rows.FindControl("racikan_quantity");
                DropDownList rac_uom = (DropDownList)rows.FindControl("racikan_uom_code");
                TextBox rac_dose = (TextBox)rows.FindControl("racikan_dosage_id");
                DropDownList rac_dosageuom = (DropDownList)rows.FindControl("racikan_doseuom");
                CheckBox rac_is_dosetext = (CheckBox)rows.FindControl("racikan_is_dosetext");
                TextBox rac_dosetext = (TextBox)rows.FindControl("racikan_dosetext");

                CompoundDetailSoap row = new CompoundDetailSoap();
                row.item_sequence = (short)countadd;
                countadd = countadd + 1;
                row.prescription_compound_detail_id = Guid.Parse(rac_detail_id.Value);
                row.prescription_compound_header_id = Guid.Parse(rac_header_id.Value);
                row.item_id = Int64.Parse(rac_item_id.Value);
                row.item_name = rac_item_name.Text;
                if (rac_qty.Text == "")
                {
                    row.quantity = "0";
                }
                else
                {
                    row.quantity = rac_qty.Text.ToString().Replace(",", ".");
                }

                row.uom_id = Int64.Parse(rac_uom.SelectedValue.ToString());
                row.uom_code = rac_uom.SelectedItem.Text;
                row.is_additional = get_isadditionaltype(type);
                row.organization_id = Helper.organizationId;

                if (rac_dose.Text == "")
                {
                    row.dose = "0";
                }
                else
                {
                    row.dose = rac_dose.Text.ToString().Replace(",", ".");
                }
                row.dose_uom_id = long.Parse(rac_dosageuom.SelectedValue);
                row.dose_uom_code = rac_dosageuom.SelectedItem.Text;
                row.dose_text = rac_dosetext.Text;

                if (rac_is_dosetext.Checked)
                {
                    row.IsDoseText = true;
                }
                else
                {
                    row.IsDoseText = false;
                    row.dose_text = "";
                }

                data.Add(row);
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonAddKontrasepsi_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonAddKontrasepsi_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());

        return data;
    }

    protected void ButtonAjaxSearchRacikan_Click(object sender, EventArgs e)
    {
        DataTable DrugSelect;
        DrugSelect = ((DataTable)Session[Helper.SessionItemDrugPres]).Select("salesItemId = '" + HF_ItemSelectedracikan.Value + "'").CopyToDataTable();

        List<PatientRoutineMedication> dataroutine = (List<PatientRoutineMedication>)Session[Helper.Sessionroutinemedication + hfguidadditional.Value];

        List<CompoundDetailSoap> data = GetRowListRacikanModal(inputhidden_isadditionaltype.Value);
        string qty = "0";     

        List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
        if (orgsetting.Where(y => y.setting_name.ToLower() == "DEFAULT_QTY".ToLower()).Count() > 0)
            qty = orgsetting.Find(y => y.setting_name.ToUpper() == "DEFAULT_QTY".ToUpper()).setting_value;

        if (DrugSelect.Rows.Count > 0)
        {
            for (int i = 0; i < DrugSelect.Rows.Count; i++)
            {
                CompoundDetailSoap temp = new CompoundDetailSoap();
                temp.prescription_compound_detail_id = Guid.Empty;
                temp.prescription_compound_header_id = Guid.Parse(inputhidden_prescription_compound_header_id.Value.ToString());
                temp.item_id = long.Parse(DrugSelect.Rows[i]["SalesItemID"].ToString());
                temp.item_name = DrugSelect.Rows[i]["SalesItemName"].ToString();
                temp.quantity = qty;
                temp.uom_id = long.Parse(DrugSelect.Rows[i]["SalesUomId"].ToString());
                temp.uom_code = DrugSelect.Rows[i]["SalesUomCode"].ToString();
                temp.item_sequence = 0;
                temp.is_additional = get_isadditionaltype(inputhidden_isadditionaltype.Value);
                temp.organization_id = Helper.organizationId;

                temp.dose = DrugSelect.Rows[i]["Dose"].ToString();
                

                temp.dose = temp.dose.ToString().Replace(',', '.');
                string[] tempnumber = temp.dose.ToString().Split('.');
                if (tempnumber[1].Length == 3)
                {
                    if (tempnumber[1] == "000")
                    {
                        temp.dose = decimal.Parse(tempnumber[0]).ToString();
                    }
                    else if (tempnumber[1].Substring(tempnumber[1].Length - 2) == "00")
                    {
                        temp.dose = tempnumber[0] + "." + tempnumber[1].Substring(0, 1);
                    }
                    else if (tempnumber[1].Substring(tempnumber[1].Length - 1) == "0")
                    {
                        temp.dose = tempnumber[0] + "." + tempnumber[1].Substring(0, 2);
                    }
                }

                temp.dose_uom_code = "-";
                temp.dose_uom_id = long.Parse(DrugSelect.Rows[i]["DoseUomId"].ToString());
                temp.dose_text = "";
                temp.IsDoseText = false;

                if (temp != null)
                {
                    DataTable dttemp = Helper.ToDataTable(data);
                    if (dttemp.Select("item_name = '" + temp.item_name + "'").Count() == 0)
                    {
                        data.Add(temp);
                        DataTable dta = Helper.ToDataTable(data);
                        Session[Helper.SessionTempInputRacikanDetail + hfguidadditional.Value] = dta;
                        input_gvw_racikan_detail.DataSource = dta;
                        input_gvw_racikan_detail.DataBind();
                    }
                    else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);

                }
            }
        }

        txtinput_ItemRacikan_AC.Text = "";

        UpdatePanelmodalInputRacikan.Update();
        UpdatePanelModalbodyRacikan.Update();
    }

    protected void input_gvw_racikan_detail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField idItem = (HiddenField)e.Row.FindControl("HF_racikan_item_id");

                DataTable dtCurrent = ((DataTable)Session[Helper.SessionTempInputRacikanDetail + hfguidadditional.Value]).Select("item_id = " + idItem.Value).CopyToDataTable();

                DataTable dt_unit_detail = (DataTable)Session[Helper.Sessionuom];
                DropDownList ddluom_racikan = (DropDownList)e.Row.FindControl("racikan_uom_code");
                ddluom_racikan.DataSource = dt_unit_detail;
                ddluom_racikan.DataTextField = "name";
                ddluom_racikan.DataValueField = "uomId";
                ddluom_racikan.DataBind();
                ddluom_racikan.Items.Insert(0, new ListItem("-", "0"));
                ddluom_racikan.SelectedValue = dtCurrent.Rows[0]["uom_id"].ToString();

                DataTable dt_doseuom = (DataTable)Session[Helper.Sessiondosage];
                DropDownList ddldoseuom_racikan = (DropDownList)e.Row.FindControl("racikan_doseuom");
                ddldoseuom_racikan.DataSource = dt_doseuom;
                ddldoseuom_racikan.DataTextField = "name";
                ddldoseuom_racikan.DataValueField = "doseUomId";
                ddldoseuom_racikan.DataBind();
                ddldoseuom_racikan.Items.Insert(0, new ListItem("-", "0"));
                ddldoseuom_racikan.SelectedValue = dtCurrent.Rows[0]["dose_uom_id"].ToString();
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "input_gvw_racikan_detail_RowDataBound", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "input_gvw_racikan_detail_RowDataBound", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

    }

    protected void btndelete_inputRacikan_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            List<CompoundDetailSoap> datadetail = GetRowListRacikanModal(inputhidden_isadditionaltype.Value);
            DataTable dt = Helper.ToDataTable(datadetail);

            dt.Rows[selRowIndex].Delete();
            if (dt.Rows.Count > 0)
            {
                Session[Helper.SessionTempInputRacikanDetail + hfguidadditional.Value] = dt;
                input_gvw_racikan_detail.DataSource = dt;
                input_gvw_racikan_detail.DataBind();
            }
            else
            {
                Session[Helper.SessionTempInputRacikanDetail + hfguidadditional.Value] = null;
                input_gvw_racikan_detail.DataSource = null;
                input_gvw_racikan_detail.DataBind();
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btndelete_inputRacikan_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btndelete_inputRacikan_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }



        //Log.Info(LogConfig.LogEnd());
    }

    void resetInputRacikanHeader()
    {
        input_namaRacikan.Text = "";
        input_dosisRacikan.Text = "";
        inputddl_dosisunitRacikan.SelectedIndex = 0;
        inputddl_frekuensiRacikan.SelectedIndex = 0;
        inputddl_ruteRacikan.SelectedIndex = 0;
        input_instruksiRacikan.Text = "";
        input_jmlRacikan.Text = "";
        inputddl_unitRacikan.SelectedIndex = 0;
        input_iterRacikan.Text = "0";
        input_instruksiRacikan_note.Text = "";
        input_dosetext.Text = "";
        input_is_dosetext.Checked = false;

        //input_dvdose.Visible = true;
        //input_dosetext.Visible = false;
        input_dvdose.Style.Add("display", "");
        input_dosetext.Style.Add("display", "none");
        notifRacikan.Style.Add("display", "none");
    }

    public bool get_isadditionaltype(string type)
    {
        bool dataType = false;
        if (inputhidden_isadditionaltype.Value == "general")
        {
            dataType = false;
        }
        else if (inputhidden_isadditionaltype.Value == "additional")
        {
            dataType = true;
        }

        return dataType;
    }

    //============================================ Racikan ==================================================//

    protected void gvw_racikan_header_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string headerId = gvw_racikan_header.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvDetails = e.Row.FindControl("gvw_racikan_detail") as GridView;

            if (Session[Helper.SessionRacikanDetail + hfguidadditional.Value] != null)
            {
                DataRow[] dr = ((DataTable)Session[Helper.SessionRacikanDetail + hfguidadditional.Value]).Select("prescription_compound_header_id = '" + headerId + "'");

                if (dr.Length > 0)
                {
                    //DataTable dtDetail = ((DataTable)Session[Helper.SessionRacikanDetail + hfguidadditional.Value]).Select("prescription_compound_header_id = '" + headerId + "'").CopyToDataTable();
                    DataTable dtDetail = dr.CopyToDataTable();
                    gvDetails.DataSource = dtDetail;
                    gvDetails.DataBind();
                }
            }
        }
    }

    protected List<CompoundHeaderSoap> GetRowListRacikanHeader()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        List<CompoundHeaderSoap> data = new List<CompoundHeaderSoap>();
        try
        {

            int countRac = 1;
            foreach (GridViewRow rows in gvw_racikan_header.Rows)
            {
                HiddenField hidden_header_id = (HiddenField)rows.FindControl("HF_headerid_racikan");
                HiddenField hidden_uom_id = (HiddenField)rows.FindControl("HF_uomid_racikan");
                HiddenField hidden_doseuom_id = (HiddenField)rows.FindControl("HF_doseuomid_racikan");
                HiddenField hidden_freq_id = (HiddenField)rows.FindControl("HF_freqid_racikan");
                HiddenField hidden_route_id = (HiddenField)rows.FindControl("HF_routeid_racikan");
                HiddenField hidden_isDoseText = (HiddenField)rows.FindControl("HF_isdosetext_racikan");

                Label item_name = (Label)rows.FindControl("lbl_nama_racikan");
                Label item_dosis = (Label)rows.FindControl("lbl_dosis_racikan");
                Label item_dosisunit = (Label)rows.FindControl("lbl_dosisunit_racikan");
                Label item_frekuensi = (Label)rows.FindControl("lbl_frekuensi_racikan");
                Label item_rute = (Label)rows.FindControl("lbl_rute_racikan");
                Label item_instruksi = (Label)rows.FindControl("lbl_instruksi_racikan");
                Label item_jml = (Label)rows.FindControl("lbl_jml_racikan");
                Label item_unit = (Label)rows.FindControl("lbl_unit_racikan");
                Label item_iter = (Label)rows.FindControl("lbl_iter_racikan");
                Label item_dosistext = (Label)rows.FindControl("lbl_dosistext_racikan");
                HiddenField item_instruksinote = (HiddenField)rows.FindControl("HF_lbl_instruksi_racikan_farmasi");

                CompoundHeaderSoap row = new CompoundHeaderSoap();
                row.item_sequence = (short)countRac;
                countRac = countRac + 1;

                row.prescription_compound_header_id = Guid.Parse(hidden_header_id.Value);
                row.compound_name = item_name.Text;
                if (item_dosis.Text.ToString() != "")
                {
                    row.dose = item_dosis.Text.ToString().Replace(",", ".");
                }
                else
                    row.dose = "0";

                row.dose_uom = item_dosisunit.Text;
                row.dose_uom_id = long.Parse(hidden_doseuom_id.Value);
                row.administration_frequency_id = Int64.Parse(hidden_freq_id.Value);
                row.frequency_code = item_frekuensi.Text;
                row.administration_route_id = Int64.Parse(hidden_route_id.Value);
                row.administration_route_code = item_rute.Text;
                row.administration_instruction = item_instruksi.Text;
                if (item_jml.Text == "")
                {
                    row.quantity = "0";
                }
                else
                {
                    row.quantity = item_jml.Text.ToString().Replace(",", ".");
                }
                if (item_unit.Text.ToString() != "")
                {
                    row.uom_id = Int64.Parse(hidden_uom_id.Value.ToString());
                    row.uom_code = item_unit.Text;
                }
                else if (item_unit.Text.ToString() == "")
                {
                    row.uom_id = 0;
                    row.uom_code = "";
                }
                if (item_iter.Text == "")
                {
                    row.iter = 0;
                }
                else
                {
                    row.iter = int.Parse(item_iter.Text);
                }
                row.compound_note = item_instruksinote.Value;
                row.is_additional = false;
                row.dose_text = item_dosistext.Text;
                row.IsDoseText = bool.Parse(hidden_isDoseText.Value);
                if (row.IsDoseText == false)
                {
                    row.dose_text = "";
                }

                data.Add(row);
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetRowListRacikanHeader", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetRowListRacikanHeader", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());

        return data;
    }

    protected void LB_tambahracikanbaru_Click(object sender, EventArgs e)
    {
        BtnSave_Racikan.Visible = true;
        BtnUpdate_Racikan.Visible = false;
        BtnSave_Racikan_Add.Visible = false;
        BtnUpdate_Racikan_Add.Visible = false;
        if (HFisBahasaSOAP_Planning.Value == "ENG")
        {
            LabelHeaderModalRacikan.Text = "Add Compound";
        }
        else if(HFisBahasaSOAP_Planning.Value == "IND")
        {
            LabelHeaderModalRacikan.Text = "Tambah Racikan";
        }
        
        inputhidden_isadditionaltype.Value = "general";

        inputhidden_prescription_compound_header_id.Value = Guid.NewGuid().ToString();
        Session[Helper.SessionTempInputRacikanHeader + hfguidadditional.Value] = null;
        Session[Helper.SessionTempInputRacikanDetail + hfguidadditional.Value] = null;
        input_gvw_racikan_detail.DataSource = null;
        input_gvw_racikan_detail.DataBind();

        resetInputRacikanHeader();
        UpdatePanelmodalInputRacikan.Update();
        UpdatePanelModalbodyRacikan.Update();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddRacikan", "$('#modalInputRacikan').modal();", true);
    }

    protected void btneditRacikanHeader_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        resetInputRacikanHeader();
        BtnSave_Racikan.Visible = false;
        BtnUpdate_Racikan.Visible = true;
        BtnSave_Racikan_Add.Visible = false;
        BtnUpdate_Racikan_Add.Visible = false;
        if (HFisBahasaSOAP_Planning.Value == "ENG")
        {
            LabelHeaderModalRacikan.Text = "Edit Compound";
        }
        else if (HFisBahasaSOAP_Planning.Value == "IND")
        {
            LabelHeaderModalRacikan.Text = "Ubah Racikan";
        }
        inputhidden_isadditionaltype.Value = "general";

        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            HiddenField headerid = (HiddenField)gvw_racikan_header.Rows[selRowIndex].FindControl("HF_headerid_racikan");
            inputhidden_prescription_compound_header_id.Value = headerid.Value.ToString();

            List<CompoundHeaderSoap> dataheader = GetRowListRacikanHeader();
            DataTable dt_header = Helper.ToDataTable(dataheader);
            

            DataRow[] rowsheader;
            rowsheader = dt_header.Select("prescription_compound_header_id = '" + headerid.Value.ToString() + "'");
            input_namaRacikan.Text = rowsheader[0]["compound_name"].ToString();
            input_dosisRacikan.Text = rowsheader[0]["dose"].ToString();
            inputddl_dosisunitRacikan.SelectedValue = rowsheader[0]["dose_uom_id"].ToString();
            inputddl_frekuensiRacikan.SelectedValue = rowsheader[0]["administration_frequency_id"].ToString();
            inputddl_ruteRacikan.SelectedValue = rowsheader[0]["administration_route_id"].ToString();
            input_instruksiRacikan.Text = rowsheader[0]["administration_instruction"].ToString();
            input_jmlRacikan.Text = rowsheader[0]["quantity"].ToString();
            inputddl_unitRacikan.SelectedValue = rowsheader[0]["uom_id"].ToString();
            input_iterRacikan.Text = rowsheader[0]["iter"].ToString();
            input_instruksiRacikan_note.Text = rowsheader[0]["compound_note"].ToString();
            input_instruksiRacikan_note.Rows = input_instruksiRacikan_note.Text.Split('\n').Length;
            input_dosetext.Text = rowsheader[0]["dose_text"].ToString();
            if (rowsheader[0]["IsDoseText"].ToString().ToUpper() == "TRUE")
            {
                input_is_dosetext.Checked = true;
                //input_dvdose.Visible = false;
                //input_dosetext.Visible = true;
                input_dvdose.Style.Add("display", "none");
                input_dosetext.Style.Add("display", "");
            }
            else if (rowsheader[0]["IsDoseText"].ToString().ToUpper() == "FALSE")
            {
                input_is_dosetext.Checked = false;
                //input_dvdose.Visible = true;
                //input_dosetext.Visible = false;
                input_dvdose.Style.Add("display", "");
                input_dosetext.Style.Add("display", "none");
            }

            if (Session[Helper.SessionRacikanDetail + hfguidadditional.Value] != null)
            {
                DataTable dt_detail = (DataTable)Session[Helper.SessionRacikanDetail + hfguidadditional.Value];

                DataRow[] dr_det = dt_detail.Select("prescription_compound_header_id = '" + headerid.Value.ToString() + "'");
                if (dr_det.Length > 0)
                {
                    DataTable dt_detail_select = dt_detail.Select("prescription_compound_header_id = '" + headerid.Value.ToString() + "'").CopyToDataTable();
                    Session[Helper.SessionTempInputRacikanDetail + hfguidadditional.Value] = dt_detail_select;
                    input_gvw_racikan_detail.DataSource = dt_detail_select;
                    input_gvw_racikan_detail.DataBind();
                }
                else
                {
                    Session[Helper.SessionTempInputRacikanDetail + hfguidadditional.Value] = null;
                    input_gvw_racikan_detail.DataSource = null;
                    input_gvw_racikan_detail.DataBind();
                }
            }


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddRacikan", "$('#modalInputRacikan').modal();", true);
            UpdatePanelmodalInputRacikan.Update();
            UpdatePanelModalbodyRacikan.Update();


            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btneditRacikanHeader_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btneditRacikanHeader_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btndeleteRacikanHeader_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            HiddenField headerid = (HiddenField)gvw_racikan_header.Rows[selRowIndex].FindControl("HF_headerid_racikan");

            List<CompoundHeaderSoap> dataheader = GetRowListRacikanHeader();
            DataTable dt = Helper.ToDataTable(dataheader);
            DataTable dataRacikanDetail = (DataTable)Session[Helper.SessionRacikanDetail + hfguidadditional.Value];

            if (dataRacikanDetail != null && dataRacikanDetail.Rows.Count != 0)
            {
                DataRow[] rows;
                rows = dataRacikanDetail.Select("prescription_compound_header_id = '" + headerid.Value.ToString() + "'");
                foreach (DataRow row in rows)
                {
                    dataRacikanDetail.Rows.Remove(row);
                }
            }
            dt.Rows[selRowIndex].Delete();

            if (dt.Rows.Count > 0)
            {
                Session[Helper.SessionRacikanHeader + hfguidadditional.Value] = dt;
                Session[Helper.SessionRacikanDetail + hfguidadditional.Value] = dataRacikanDetail;
                gvw_racikan_header.DataSource = dt;
                gvw_racikan_header.DataBind();
            }
            else
            {
                Session[Helper.SessionRacikanHeader + hfguidadditional.Value] = null;
                Session[Helper.SessionRacikanDetail + hfguidadditional.Value] = dataRacikanDetail;
                gvw_racikan_header.DataSource = null;
                gvw_racikan_header.DataBind();
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btndeleteRacikanHeader_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btndeleteRacikanHeader_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void BtnSave_Racikan_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        DataTable RacikanHeader = (DataTable)Session[Helper.SessionRacikanHeader + hfguidadditional.Value];
        DataTable RacikanDetail = (DataTable)Session[Helper.SessionRacikanDetail + hfguidadditional.Value];

        if (RacikanHeader == null)
        {
            RacikanHeader = new DataTable();
            RacikanHeader.Columns.Add("prescription_compound_header_id");
            RacikanHeader.Columns.Add("compound_name");
            RacikanHeader.Columns.Add("quantity");
            RacikanHeader.Columns.Add("uom_id");
            RacikanHeader.Columns.Add("uom_code");
            RacikanHeader.Columns.Add("dose");
            RacikanHeader.Columns.Add("dose_uom_id");
            RacikanHeader.Columns.Add("dose_uom");
            RacikanHeader.Columns.Add("administration_frequency_id");
            RacikanHeader.Columns.Add("frequency_code");
            RacikanHeader.Columns.Add("administration_route_id");
            RacikanHeader.Columns.Add("administration_route_code");
            RacikanHeader.Columns.Add("administration_instruction");
            RacikanHeader.Columns.Add("compound_note");
            RacikanHeader.Columns.Add("iter");
            RacikanHeader.Columns.Add("is_additional");
            RacikanHeader.Columns.Add("item_sequence");
            RacikanHeader.Columns.Add("dose_text");
            RacikanHeader.Columns.Add("IsDoseText");

            //RacikanDetail = new DataTable();
            //RacikanDetail.Columns.Add("prescription_compound_detail_id");
            //RacikanDetail.Columns.Add("prescription_compound_header_id");
            //RacikanDetail.Columns.Add("item_id");
            //RacikanDetail.Columns.Add("item_name");
            //RacikanDetail.Columns.Add("quantity");
            //RacikanDetail.Columns.Add("uom_id");
            //RacikanDetail.Columns.Add("uom_code");
            //RacikanDetail.Columns.Add("item_sequence");
            //RacikanDetail.Columns.Add("is_additional");
            //RacikanDetail.Columns.Add("organization_id");
        }

        if (RacikanDetail == null)
        {
            RacikanDetail = new DataTable();
            RacikanDetail.Columns.Add("prescription_compound_detail_id");
            RacikanDetail.Columns.Add("prescription_compound_header_id");
            RacikanDetail.Columns.Add("item_id");
            RacikanDetail.Columns.Add("item_name");
            RacikanDetail.Columns.Add("quantity");
            RacikanDetail.Columns.Add("uom_id");
            RacikanDetail.Columns.Add("uom_code");
            RacikanDetail.Columns.Add("item_sequence");
            RacikanDetail.Columns.Add("is_additional");
            RacikanDetail.Columns.Add("organization_id");
            RacikanDetail.Columns.Add("dose");
            RacikanDetail.Columns.Add("dose_uom_id");
            RacikanDetail.Columns.Add("dose_uom_code");
            RacikanDetail.Columns.Add("dose_text");
            RacikanDetail.Columns.Add("IsDoseText");
        }

        int flagNamaRacikan = 0;
        notifRacikan.Style.Add("display", "none");

        for (int i = 0; i < RacikanHeader.Rows.Count; i++)
        {
            if (RacikanHeader.Rows[i]["compound_name"].ToString().ToLower() == input_namaRacikan.Text.ToLower())
            {
                flagNamaRacikan = 1;
            }
        }

        if (flagNamaRacikan == 1)
        {
            notifRacikan.Style.Add("display", "");
            LabelNotifRacikan.Text = "Nama Racikan tidak boleh sama!";
        }
        else
        {

            DataRow tempHeader = RacikanHeader.NewRow();
            tempHeader["prescription_compound_header_id"] = Guid.Parse(inputhidden_prescription_compound_header_id.Value);
            tempHeader["compound_name"] = input_namaRacikan.Text;
            tempHeader["quantity"] = input_jmlRacikan.Text;
            tempHeader["uom_id"] = long.Parse(inputddl_unitRacikan.SelectedValue.ToString());
            tempHeader["uom_code"] = inputddl_unitRacikan.SelectedItem.Text;
            tempHeader["dose"] = input_dosisRacikan.Text;
            tempHeader["dose_uom_id"] = long.Parse(inputddl_dosisunitRacikan.SelectedValue.ToString());
            tempHeader["dose_uom"] = inputddl_dosisunitRacikan.SelectedItem.Text;
            tempHeader["administration_frequency_id"] = long.Parse(inputddl_frekuensiRacikan.SelectedValue.ToString());
            tempHeader["frequency_code"] = inputddl_frekuensiRacikan.SelectedItem.Text;
            tempHeader["administration_route_id"] = long.Parse(inputddl_ruteRacikan.SelectedValue.ToString());
            tempHeader["administration_route_code"] = inputddl_ruteRacikan.SelectedItem.Text;
            tempHeader["administration_instruction"] = input_instruksiRacikan.Text;
            tempHeader["compound_note"] = input_instruksiRacikan_note.Text;
            tempHeader["iter"] = int.Parse(input_iterRacikan.Text.ToString());
            tempHeader["is_additional"] = false;

            int item_seq = Convert.ToInt32(RacikanHeader.AsEnumerable().Max(row => row["item_sequence"]));
            tempHeader["item_sequence"] = (short)(item_seq + 1);
            tempHeader["dose_text"] = input_dosetext.Text;
            tempHeader["IsDoseText"] = input_is_dosetext.Checked;

            RacikanHeader.Rows.Add(tempHeader);
            Session[Helper.SessionRacikanHeader + hfguidadditional.Value] = RacikanHeader;

            List<CompoundDetailSoap> listdet = GetRowListRacikanModal(inputhidden_isadditionaltype.Value);
            if (listdet.Count > 0)
            {
                DataTable tempDetail = Helper.ToDataTable(listdet);

                //List<DataRow> rowListRacikanDetail = RacikanDetail.Rows.Cast<DataRow>().ToList();
                //rowListRacikanDetail.AddRange(tempDetail.Rows.Cast<DataRow>());
                //DataTable unionTableDetail = rowListRacikanDetail.CopyToDataTable();
                //atau bawahnya
                RacikanDetail = RacikanDetail.AsEnumerable().Union(tempDetail.AsEnumerable()).CopyToDataTable();
                Session[Helper.SessionRacikanDetail + hfguidadditional.Value] = RacikanDetail;
            }

            gvw_racikan_header.DataSource = RacikanHeader;
            gvw_racikan_header.DataBind();

            UpdatePanel_gvw_racikan.Update();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseRacikan", "$('#modalInputRacikan').modal('hide');", true);
        }



        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnSave_Racikan_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void BtnUpdate_Racikan_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        DataTable RacikanHeader = (DataTable)Session[Helper.SessionRacikanHeader + hfguidadditional.Value];

        int flagNamaRacikan = 0;
        notifRacikan.Style.Add("display", "none");

        for (int i = 0; i < RacikanHeader.Rows.Count; i++)
        {
            if (RacikanHeader.Rows[i]["compound_name"].ToString().ToLower() == input_namaRacikan.Text.ToLower() && RacikanHeader.Rows[i]["prescription_compound_header_id"].ToString().ToLower() != inputhidden_prescription_compound_header_id.Value.ToLower())
            {
                flagNamaRacikan = 1;
            }
        }

        if (flagNamaRacikan == 1)
        {
            notifRacikan.Style.Add("display", "");
            LabelNotifRacikan.Text = "Nama Racikan tidak boleh sama!";
        }
        else
        {

            DataRow[] rowsheader;
            rowsheader = RacikanHeader.Select("prescription_compound_header_id = '" + inputhidden_prescription_compound_header_id.Value.ToString() + "'");
            rowsheader[0]["compound_name"] = input_namaRacikan.Text; ;
            rowsheader[0]["quantity"] = input_jmlRacikan.Text;
            rowsheader[0]["uom_id"] = inputddl_unitRacikan.SelectedValue.ToString();
            rowsheader[0]["uom_code"] = inputddl_unitRacikan.SelectedItem.Text;
            rowsheader[0]["dose"] = input_dosisRacikan.Text;
            rowsheader[0]["dose_uom_id"] = inputddl_dosisunitRacikan.SelectedValue.ToString();
            rowsheader[0]["dose_uom"] = inputddl_dosisunitRacikan.SelectedItem.Text;
            rowsheader[0]["administration_frequency_id"] = inputddl_frekuensiRacikan.SelectedValue.ToString();
            rowsheader[0]["frequency_code"] = inputddl_frekuensiRacikan.SelectedItem.Text;
            rowsheader[0]["administration_route_id"] = inputddl_ruteRacikan.SelectedValue.ToString();
            rowsheader[0]["administration_route_code"] = inputddl_ruteRacikan.SelectedItem.Text;
            rowsheader[0]["administration_instruction"] = input_instruksiRacikan.Text;
            rowsheader[0]["iter"] = input_iterRacikan.Text;
            rowsheader[0]["compound_note"] = input_instruksiRacikan_note.Text;
            rowsheader[0]["dose_text"] = input_dosetext.Text;
            rowsheader[0]["IsDoseText"] = input_is_dosetext.Checked;
            Session[Helper.SessionRacikanHeader + hfguidadditional.Value] = RacikanHeader;

            DataTable RacikanDetail = (DataTable)Session[Helper.SessionRacikanDetail + hfguidadditional.Value];
            if (RacikanDetail == null)
            {
                RacikanDetail = new DataTable();
                RacikanDetail.Columns.Add("prescription_compound_detail_id");
                RacikanDetail.Columns.Add("prescription_compound_header_id");
                RacikanDetail.Columns.Add("item_id");
                RacikanDetail.Columns.Add("item_name");
                RacikanDetail.Columns.Add("quantity");
                RacikanDetail.Columns.Add("uom_id");
                RacikanDetail.Columns.Add("uom_code");
                RacikanDetail.Columns.Add("item_sequence");
                RacikanDetail.Columns.Add("is_additional");
                RacikanDetail.Columns.Add("organization_id");
                RacikanDetail.Columns.Add("dose");
                RacikanDetail.Columns.Add("dose_uom_id");
                RacikanDetail.Columns.Add("dose_uom_code");
                RacikanDetail.Columns.Add("dose_text");
                RacikanDetail.Columns.Add("IsDoseText");
            }

            DataRow[] rowsdetail;
            rowsdetail = RacikanDetail.Select("prescription_compound_header_id = '" + inputhidden_prescription_compound_header_id.Value.ToString() + "'");
            if (rowsdetail.Length > 0)
            {
                foreach (DataRow row in rowsdetail)
                {
                    RacikanDetail.Rows.Remove(row);
                }
            }

            List<CompoundDetailSoap> listdet = GetRowListRacikanModal(inputhidden_isadditionaltype.Value);
            if (listdet.Count > 0)
            {
                DataTable tempDetail = Helper.ToDataTable(listdet);
                RacikanDetail = RacikanDetail.AsEnumerable().Union(tempDetail.AsEnumerable()).CopyToDataTable();   
            }

            Session[Helper.SessionRacikanDetail + hfguidadditional.Value] = RacikanDetail;
            gvw_racikan_header.DataSource = RacikanHeader;
            gvw_racikan_header.DataBind();

            UpdatePanel_gvw_racikan.Update();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseRacikan", "$('#modalInputRacikan').modal('hide');", true);


            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnUpdate_Racikan_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
            //Log.Info(LogConfig.LogEnd());
        }
    }

    //============================================ Additional Racikan ==================================================//

    protected void gvw_racikan_header_add_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string headerId = gvw_racikan_header_add.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvDetails_add = e.Row.FindControl("gvw_racikan_detail_add") as GridView;

            if (Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value] != null)
            {
                DataRow[] dr_add = ((DataTable)Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value]).Select("prescription_compound_header_id = '" + headerId + "'");

                if (dr_add.Length > 0)
                {
                    //DataTable dtDetail_add = ((DataTable)Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value]).Select("prescription_compound_header_id = '" + headerId + "'").CopyToDataTable();
                    DataTable dtDetail_add = dr_add.CopyToDataTable();
                    gvDetails_add.DataSource = dtDetail_add;
                    gvDetails_add.DataBind();
                }
            }
        }
    }

    protected List<CompoundHeaderSoap> GetRowListRacikanHeaderAdditional()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        List<CompoundHeaderSoap> data = new List<CompoundHeaderSoap>();
        try
        {

            int countRac = 1;
            foreach (GridViewRow rows in gvw_racikan_header_add.Rows)
            {
                HiddenField hidden_header_id = (HiddenField)rows.FindControl("HF_headerid_racikan");
                HiddenField hidden_uom_id = (HiddenField)rows.FindControl("HF_uomid_racikan");
                HiddenField hidden_doseuom_id = (HiddenField)rows.FindControl("HF_doseuomid_racikan");
                HiddenField hidden_freq_id = (HiddenField)rows.FindControl("HF_freqid_racikan");
                HiddenField hidden_route_id = (HiddenField)rows.FindControl("HF_routeid_racikan");
                HiddenField hidden_isDoseText = (HiddenField)rows.FindControl("HF_isdosetext_racikan");

                Label item_name = (Label)rows.FindControl("lbl_nama_racikan");
                Label item_dosis = (Label)rows.FindControl("lbl_dosis_racikan");
                Label item_dosisunit = (Label)rows.FindControl("lbl_dosisunit_racikan");
                Label item_frekuensi = (Label)rows.FindControl("lbl_frekuensi_racikan");
                Label item_rute = (Label)rows.FindControl("lbl_rute_racikan");
                Label item_instruksi = (Label)rows.FindControl("lbl_instruksi_racikan");
                Label item_jml = (Label)rows.FindControl("lbl_jml_racikan");
                Label item_unit = (Label)rows.FindControl("lbl_unit_racikan");
                Label item_iter = (Label)rows.FindControl("lbl_iter_racikan");
                Label item_dosistext = (Label)rows.FindControl("lbl_dosistext_racikan");
                HiddenField item_instruksinote = (HiddenField)rows.FindControl("HF_lbl_instruksi_racikan_farmasi");

                CompoundHeaderSoap row = new CompoundHeaderSoap();
                row.item_sequence = (short)countRac;
                countRac = countRac + 1;

                row.prescription_compound_header_id = Guid.Parse(hidden_header_id.Value);
                row.compound_name = item_name.Text;
                if (item_dosis.Text.ToString() != "")
                {
                    row.dose = item_dosis.Text.ToString().Replace(",", ".");
                }
                else
                    row.dose = "0";

                row.dose_uom = item_dosisunit.Text;
                row.dose_uom_id = long.Parse(hidden_doseuom_id.Value);
                row.administration_frequency_id = Int64.Parse(hidden_freq_id.Value);
                row.frequency_code = item_frekuensi.Text;
                row.administration_route_id = Int64.Parse(hidden_route_id.Value);
                row.administration_route_code = item_rute.Text;
                row.administration_instruction = item_instruksi.Text;
                if (item_jml.Text == "")
                {
                    row.quantity = "0";
                }
                else
                {
                    row.quantity = item_jml.Text.ToString().Replace(",", ".");
                }
                if (item_unit.Text.ToString() != "")
                {
                    row.uom_id = Int64.Parse(hidden_uom_id.Value.ToString());
                    row.uom_code = item_unit.Text;
                }
                else if (item_unit.Text.ToString() == "")
                {
                    row.uom_id = 0;
                    row.uom_code = "";
                }
                if (item_iter.Text == "")
                {
                    row.iter = 0;
                }
                else
                {
                    row.iter = int.Parse(item_iter.Text);
                }
                row.compound_note = item_instruksinote.Value;
                row.is_additional = true;
                row.dose_text = item_dosistext.Text;
                row.IsDoseText = bool.Parse(hidden_isDoseText.Value);
                if (row.IsDoseText == false)
                {
                    row.dose_text = "";
                }

                data.Add(row);
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetRowListRacikanHeaderAdditional", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetRowListRacikanHeaderAdditional", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());

        return data;
    }

    protected void LB_tambahracikanbaru_add_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        BtnSave_Racikan.Visible = false;
        BtnUpdate_Racikan.Visible = false;
        BtnSave_Racikan_Add.Visible = true;
        BtnUpdate_Racikan_Add.Visible = false;
        if (HFisBahasaSOAP_Planning.Value == "ENG")
        {
            LabelHeaderModalRacikan.Text = "Add Additional Compound";
        }
        else if (HFisBahasaSOAP_Planning.Value == "IND")
        {
            LabelHeaderModalRacikan.Text = "Tambah Racikan Tambahan";
        }
        inputhidden_isadditionaltype.Value = "additional";

        inputhidden_prescription_compound_header_id.Value = Guid.NewGuid().ToString();
        Session[Helper.SessionTempInputRacikanHeader + hfguidadditional.Value] = null;
        Session[Helper.SessionTempInputRacikanDetail + hfguidadditional.Value] = null;
        input_gvw_racikan_detail.DataSource = null;
        input_gvw_racikan_detail.DataBind();

        resetInputRacikanHeader();
        UpdatePanelmodalInputRacikan.Update();
        UpdatePanelModalbodyRacikan.Update();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddRacikan", "$('#modalInputRacikan').modal();", true);


        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "LB_tambahracikanbaru_add_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btneditRacikanHeader_add_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        resetInputRacikanHeader();
        BtnSave_Racikan.Visible = false;
        BtnUpdate_Racikan.Visible = false;
        BtnSave_Racikan_Add.Visible = false;
        BtnUpdate_Racikan_Add.Visible = true;
        if (HFisBahasaSOAP_Planning.Value == "ENG")
        {
            LabelHeaderModalRacikan.Text = "Edit Additional Compound";
        }
        else if (HFisBahasaSOAP_Planning.Value == "IND")
        {
            LabelHeaderModalRacikan.Text = "Ubah Racikan Tambahan";
        }
        inputhidden_isadditionaltype.Value = "additional";

        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            HiddenField headerid = (HiddenField)gvw_racikan_header_add.Rows[selRowIndex].FindControl("HF_headerid_racikan");
            inputhidden_prescription_compound_header_id.Value = headerid.Value.ToString();

            List<CompoundHeaderSoap> dataheader_add = GetRowListRacikanHeaderAdditional();
            DataTable dt_header = Helper.ToDataTable(dataheader_add);

            DataRow[] rowsheader;
            rowsheader = dt_header.Select("prescription_compound_header_id = '" + headerid.Value.ToString() + "'");
            input_namaRacikan.Text = rowsheader[0]["compound_name"].ToString();
            input_dosisRacikan.Text = rowsheader[0]["dose"].ToString();
            inputddl_dosisunitRacikan.SelectedValue = rowsheader[0]["dose_uom_id"].ToString();
            inputddl_frekuensiRacikan.SelectedValue = rowsheader[0]["administration_frequency_id"].ToString();
            inputddl_ruteRacikan.SelectedValue = rowsheader[0]["administration_route_id"].ToString();
            input_instruksiRacikan.Text = rowsheader[0]["administration_instruction"].ToString();
            input_jmlRacikan.Text = rowsheader[0]["quantity"].ToString();
            inputddl_unitRacikan.SelectedValue = rowsheader[0]["uom_id"].ToString();
            input_iterRacikan.Text = rowsheader[0]["iter"].ToString();
            input_instruksiRacikan_note.Text = rowsheader[0]["compound_note"].ToString();
            input_instruksiRacikan_note.Rows = input_instruksiRacikan_note.Text.Split('\n').Length;
            input_dosetext.Text = rowsheader[0]["dose_text"].ToString();
            if (rowsheader[0]["IsDoseText"].ToString().ToUpper() == "TRUE")
            {
                input_is_dosetext.Checked = true;
                //input_dvdose.Visible = false;
                //input_dosetext.Visible = true;
                input_dvdose.Style.Add("display", "none");
                input_dosetext.Style.Add("display", "");
            }
            else if (rowsheader[0]["IsDoseText"].ToString().ToUpper() == "FALSE")
            {
                input_is_dosetext.Checked = false;
                //input_dvdose.Visible = true;
                //input_dosetext.Visible = false;
                input_dvdose.Style.Add("display", "");
                input_dosetext.Style.Add("display", "none");
            }

            if (Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value] != null)
            {
                DataTable dt_detail = (DataTable)Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value];

                DataRow[] dr_det = dt_detail.Select("prescription_compound_header_id = '" + headerid.Value.ToString() + "'");
                if (dr_det.Length > 0)
                {
                    DataTable dt_detail_select = dt_detail.Select("prescription_compound_header_id = '" + headerid.Value.ToString() + "'").CopyToDataTable();
                    Session[Helper.SessionTempInputRacikanDetail + hfguidadditional.Value] = dt_detail_select;
                    input_gvw_racikan_detail.DataSource = dt_detail_select;
                    input_gvw_racikan_detail.DataBind();
                }
                else
                {
                    Session[Helper.SessionTempInputRacikanDetail + hfguidadditional.Value] = null;
                    input_gvw_racikan_detail.DataSource = null;
                    input_gvw_racikan_detail.DataBind();
                }
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddRacikan", "$('#modalInputRacikan').modal();", true);
            UpdatePanelmodalInputRacikan.Update();
            UpdatePanelModalbodyRacikan.Update();


            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btneditRacikanHeader_add_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btneditRacikanHeader_add_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btndeleteRacikanHeader_add_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            HiddenField headerid = (HiddenField)gvw_racikan_header_add.Rows[selRowIndex].FindControl("HF_headerid_racikan");

            List<CompoundHeaderSoap> dataheader_add = GetRowListRacikanHeaderAdditional();
            DataTable dt = Helper.ToDataTable(dataheader_add);
            DataTable dataRacikanDetail_add = (DataTable)Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value];

            if (dataRacikanDetail_add != null && dataRacikanDetail_add.Rows.Count != 0)
            {
                DataRow[] rows;
                rows = dataRacikanDetail_add.Select("prescription_compound_header_id = '" + headerid.Value.ToString() + "'");
                foreach (DataRow row in rows)
                {
                    dataRacikanDetail_add.Rows.Remove(row);
                }
            }
            dt.Rows[selRowIndex].Delete();

            if (dt.Rows.Count > 0)
            {
                Session[Helper.SessionRacikanHeaderAdd + hfguidadditional.Value] = dt;
                Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value] = dataRacikanDetail_add;
                gvw_racikan_header_add.DataSource = dt;
                gvw_racikan_header_add.DataBind();
            }
            else
            {
                Session[Helper.SessionRacikanHeaderAdd + hfguidadditional.Value] = null;
                Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value] = dataRacikanDetail_add;
                gvw_racikan_header_add.DataSource = null;
                gvw_racikan_header_add.DataBind();
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btndeleteRacikanHeader_add_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btndeleteRacikanHeader_add_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void BtnSave_Racikan_Add_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        DataTable RacikanHeader_add = (DataTable)Session[Helper.SessionRacikanHeaderAdd + hfguidadditional.Value];
        DataTable RacikanDetail_add = (DataTable)Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value];

        if (RacikanHeader_add == null)
        {
            RacikanHeader_add = new DataTable();
            RacikanHeader_add.Columns.Add("prescription_compound_header_id");
            RacikanHeader_add.Columns.Add("compound_name");
            RacikanHeader_add.Columns.Add("quantity");
            RacikanHeader_add.Columns.Add("uom_id");
            RacikanHeader_add.Columns.Add("uom_code");
            RacikanHeader_add.Columns.Add("dose");
            RacikanHeader_add.Columns.Add("dose_uom_id");
            RacikanHeader_add.Columns.Add("dose_uom");
            RacikanHeader_add.Columns.Add("administration_frequency_id");
            RacikanHeader_add.Columns.Add("frequency_code");
            RacikanHeader_add.Columns.Add("administration_route_id");
            RacikanHeader_add.Columns.Add("administration_route_code");
            RacikanHeader_add.Columns.Add("administration_instruction");
            RacikanHeader_add.Columns.Add("compound_note");
            RacikanHeader_add.Columns.Add("iter");
            RacikanHeader_add.Columns.Add("is_additional");
            RacikanHeader_add.Columns.Add("item_sequence");
            RacikanHeader_add.Columns.Add("dose_text");
            RacikanHeader_add.Columns.Add("IsDoseText");

            //RacikanDetail_add = new DataTable();
            //RacikanDetail_add.Columns.Add("prescription_compound_detail_id");
            //RacikanDetail_add.Columns.Add("prescription_compound_header_id");
            //RacikanDetail_add.Columns.Add("item_id");
            //RacikanDetail_add.Columns.Add("item_name");
            //RacikanDetail_add.Columns.Add("quantity");
            //RacikanDetail_add.Columns.Add("uom_id");
            //RacikanDetail_add.Columns.Add("uom_code");
            //RacikanDetail_add.Columns.Add("item_sequence");
            //RacikanDetail_add.Columns.Add("is_additional");
            //RacikanDetail_add.Columns.Add("organization_id");
        }

        if (RacikanDetail_add == null)
        {
            RacikanDetail_add = new DataTable();
            RacikanDetail_add.Columns.Add("prescription_compound_detail_id");
            RacikanDetail_add.Columns.Add("prescription_compound_header_id");
            RacikanDetail_add.Columns.Add("item_id");
            RacikanDetail_add.Columns.Add("item_name");
            RacikanDetail_add.Columns.Add("quantity");
            RacikanDetail_add.Columns.Add("uom_id");
            RacikanDetail_add.Columns.Add("uom_code");
            RacikanDetail_add.Columns.Add("item_sequence");
            RacikanDetail_add.Columns.Add("is_additional");
            RacikanDetail_add.Columns.Add("organization_id");
            RacikanDetail_add.Columns.Add("dose");
            RacikanDetail_add.Columns.Add("dose_uom_id");
            RacikanDetail_add.Columns.Add("dose_uom_code");
            RacikanDetail_add.Columns.Add("dose_text");
            RacikanDetail_add.Columns.Add("IsDoseText");
        }

        int flagNamaRacikan = 0;
        notifRacikan.Style.Add("display", "none");

        for (int i = 0; i < RacikanHeader_add.Rows.Count; i++)
        {
            if (RacikanHeader_add.Rows[i]["compound_name"].ToString().ToLower() == input_namaRacikan.Text.ToLower())
            {
                flagNamaRacikan = 1;
            }
        }

        if (flagNamaRacikan == 1)
        {
            notifRacikan.Style.Add("display", "");
            LabelNotifRacikan.Text = "Nama Racikan tidak boleh sama!";
        }
        else
        {

            DataRow tempHeader_add = RacikanHeader_add.NewRow();
            tempHeader_add["prescription_compound_header_id"] = Guid.Parse(inputhidden_prescription_compound_header_id.Value);
            tempHeader_add["compound_name"] = input_namaRacikan.Text;
            tempHeader_add["quantity"] = input_jmlRacikan.Text;
            tempHeader_add["uom_id"] = long.Parse(inputddl_unitRacikan.SelectedValue.ToString());
            tempHeader_add["uom_code"] = inputddl_unitRacikan.SelectedItem.Text;
            tempHeader_add["dose"] = input_dosisRacikan.Text;
            tempHeader_add["dose_uom_id"] = long.Parse(inputddl_dosisunitRacikan.SelectedValue.ToString());
            tempHeader_add["dose_uom"] = inputddl_dosisunitRacikan.SelectedItem.Text;
            tempHeader_add["administration_frequency_id"] = long.Parse(inputddl_frekuensiRacikan.SelectedValue.ToString());
            tempHeader_add["frequency_code"] = inputddl_frekuensiRacikan.SelectedItem.Text;
            tempHeader_add["administration_route_id"] = long.Parse(inputddl_ruteRacikan.SelectedValue.ToString());
            tempHeader_add["administration_route_code"] = inputddl_ruteRacikan.SelectedItem.Text;
            tempHeader_add["administration_instruction"] = input_instruksiRacikan.Text;
            tempHeader_add["compound_note"] = input_instruksiRacikan_note.Text;
            tempHeader_add["iter"] = int.Parse(input_iterRacikan.Text.ToString());
            tempHeader_add["is_additional"] = true;

            int item_seq = Convert.ToInt32(RacikanHeader_add.AsEnumerable().Max(row => row["item_sequence"]));
            tempHeader_add["item_sequence"] = (short)(item_seq + 1);
            tempHeader_add["dose_text"] = input_dosetext.Text;
            tempHeader_add["IsDoseText"] = input_is_dosetext.Checked;

            RacikanHeader_add.Rows.Add(tempHeader_add);
            Session[Helper.SessionRacikanHeaderAdd + hfguidadditional.Value] = RacikanHeader_add;

            List<CompoundDetailSoap> listdet_add = GetRowListRacikanModal(inputhidden_isadditionaltype.Value);
            if (listdet_add.Count > 0)
            {
                DataTable tempDetail_add = Helper.ToDataTable(listdet_add);

                //List<DataRow> rowListRacikanDetail_add = RacikanDetail_add.Rows.Cast<DataRow>().ToList();
                //rowListRacikanDetail_add.AddRange(tempDetail_add.Rows.Cast<DataRow>());
                //DataTable unionTableDetail_add = rowListRacikanDetail_add.CopyToDataTable();
                //atau bawahnya
                RacikanDetail_add = RacikanDetail_add.AsEnumerable().Union(tempDetail_add.AsEnumerable()).CopyToDataTable();
                Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value] = RacikanDetail_add;
            }

            gvw_racikan_header_add.DataSource = RacikanHeader_add;
            gvw_racikan_header_add.DataBind();

            UpdatePanel_gvw_racikan_add.Update();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseRacikan", "$('#modalInputRacikan').modal('hide');", true);


            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnSave_Racikan_Add_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
            //Log.Info(LogConfig.LogEnd());
        }
    }

    protected void BtnUpdate_Racikan_Add_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        DataTable RacikanHeader_add = (DataTable)Session[Helper.SessionRacikanHeaderAdd + hfguidadditional.Value];

        DataRow[] rowsheader;
        rowsheader = RacikanHeader_add.Select("prescription_compound_header_id = '" + inputhidden_prescription_compound_header_id.Value.ToString() + "'");
        rowsheader[0]["compound_name"] = input_namaRacikan.Text; ;
        rowsheader[0]["quantity"] = input_jmlRacikan.Text;
        rowsheader[0]["uom_id"] = inputddl_unitRacikan.SelectedValue.ToString();
        rowsheader[0]["uom_code"] = inputddl_unitRacikan.SelectedItem.Text;
        rowsheader[0]["dose"] = input_dosisRacikan.Text;
        rowsheader[0]["dose_uom_id"] = inputddl_dosisunitRacikan.SelectedValue.ToString();
        rowsheader[0]["dose_uom"] = inputddl_dosisunitRacikan.SelectedItem.Text;
        rowsheader[0]["administration_frequency_id"] = inputddl_frekuensiRacikan.SelectedValue.ToString();
        rowsheader[0]["frequency_code"] = inputddl_frekuensiRacikan.SelectedItem.Text;
        rowsheader[0]["administration_route_id"] = inputddl_ruteRacikan.SelectedValue.ToString();
        rowsheader[0]["administration_route_code"] = inputddl_ruteRacikan.SelectedItem.Text;
        rowsheader[0]["administration_instruction"] = input_instruksiRacikan.Text;
        rowsheader[0]["iter"] = input_iterRacikan.Text;
        rowsheader[0]["compound_note"] = input_instruksiRacikan_note.Text;
        rowsheader[0]["dose_text"] = input_dosetext.Text;
        rowsheader[0]["IsDoseText"] = input_is_dosetext.Checked;
        Session[Helper.SessionRacikanHeader + hfguidadditional.Value] = RacikanHeader_add;

        DataTable RacikanDetail_add = (DataTable)Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value];
        if (RacikanDetail_add == null)
        {
            RacikanDetail_add = new DataTable();
            RacikanDetail_add.Columns.Add("prescription_compound_detail_id");
            RacikanDetail_add.Columns.Add("prescription_compound_header_id");
            RacikanDetail_add.Columns.Add("item_id");
            RacikanDetail_add.Columns.Add("item_name");
            RacikanDetail_add.Columns.Add("quantity");
            RacikanDetail_add.Columns.Add("uom_id");
            RacikanDetail_add.Columns.Add("uom_code");
            RacikanDetail_add.Columns.Add("item_sequence");
            RacikanDetail_add.Columns.Add("is_additional");
            RacikanDetail_add.Columns.Add("organization_id");
            RacikanDetail_add.Columns.Add("dose");
            RacikanDetail_add.Columns.Add("dose_uom_id");
            RacikanDetail_add.Columns.Add("dose_uom_code");
            RacikanDetail_add.Columns.Add("dose_text");
            RacikanDetail_add.Columns.Add("IsDoseText");
        }

        DataRow[] rowsdetail;
        rowsdetail = RacikanDetail_add.Select("prescription_compound_header_id = '" + inputhidden_prescription_compound_header_id.Value.ToString() + "'");
        if (rowsdetail.Length > 0)
        {
            foreach (DataRow row in rowsdetail)
            {
                RacikanDetail_add.Rows.Remove(row);
            }
        }

        List<CompoundDetailSoap> listdet_add = GetRowListRacikanModal(inputhidden_isadditionaltype.Value);
        if (listdet_add.Count > 0)
        {
            DataTable tempDetail_add = Helper.ToDataTable(listdet_add);
            RacikanDetail_add = RacikanDetail_add.AsEnumerable().Union(tempDetail_add.AsEnumerable()).CopyToDataTable();
        }
        Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value] = RacikanDetail_add;

        gvw_racikan_header_add.DataSource = RacikanHeader_add;
        gvw_racikan_header_add.DataBind();

        UpdatePanel_gvw_racikan_add.Update();

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseRacikan", "$('#modalInputRacikan').modal('hide');", true);


        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnUpdate_Racikan_Add_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void LinkSaveAsOrderSetRacikan_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        Dictionary<string, string> logParam = new Dictionary<string, string>();

        try
        {

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            HiddenField headerid = (HiddenField)gvw_racikan_header.Rows[selRowIndex].FindControl("HF_headerid_racikan");
            TextBox headerRacikanName = (TextBox)gvw_racikan_header.Rows[selRowIndex].FindControl("TextBoxSaveAsOrderSetNameRacikan");
            Label headerlblRacikanName = (Label)gvw_racikan_header.Rows[selRowIndex].FindControl("lbl_nama_racikan");
            inputhidden_prescription_compound_header_id.Value = headerid.Value.ToString();

            if (headerRacikanName.Text != "")
            {
                if (Session[Helper.SessionRacikanDetail + hfguidadditional.Value] == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "detailEmpty", "alert('For Order Set, Detail Can not be Empty!');", true);
                }
                else
                {
                    CompoundOrderSet compoundOS = new CompoundOrderSet();

                    List<CompoundHeaderSoap> dataheader = GetRowListRacikanHeader();
                    CompoundHeaderSoap headerOS = dataheader.Where(head => head.prescription_compound_header_id == Guid.Parse(headerid.Value.ToString())).First();
                    headerOS.compound_name = headerRacikanName.Text;
                    compoundOS.header = headerOS;

                    DataTable dt_detail = (DataTable)Session[Helper.SessionRacikanDetail + hfguidadditional.Value];
                    DataRow[] dr_detail = dt_detail.Select("prescription_compound_header_id = '" + headerid.Value.ToString() + "'");
                    if (dr_detail.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "detailEmpty", "alert('For Order Set, Detail Can not be Empty!');", true);
                    }
                    else
                    {
                        DataTable dt_detail_select = dt_detail.Select("prescription_compound_header_id = '" + headerid.Value.ToString() + "'").CopyToDataTable();
                        compoundOS.detail = Helper.ToDataList<CompoundDetailSoap>(dt_detail_select);


                        long doctorid = long.Parse(Helper.GetDoctorID(this.Parent.Page));

                        logParam = new Dictionary<string, string>
                        {
                            { "Doctor_ID", doctorid.ToString() }
                        };
                        //Log.Debug(LogConfig.LogStart("insertSaveAsOrderSetRacikan", logParam, LogConfig.JsonToString(compoundOS)));
                        var SaveAS = clsOrderSet.insertSaveAsOrderSetRacikan(compoundOS, doctorid);
                        var JsongetSaveAS = (JObject)JsonConvert.DeserializeObject<dynamic>(SaveAS.Result);
                        var Status = JsongetSaveAS.Property("status").Value.ToString();
                        var Message = JsongetSaveAS.Property("data").Value.ToString();
                        //Log.Debug(LogConfig.LogEnd("insertSaveAsOrderSetRacikan", Status, Message));

                        if (Message.ToUpper() == "SUCCESS")
                        {
                            ShowToastr("Save as Order Set successful.", "Success", "Success");

                            logParam = new Dictionary<string, string>
                            {
                                { "Doctor_ID", Helper.GetDoctorID(this.Parent.Page) },
                                { "Organization_ID", Helper.organizationId.ToString() }
                            };
                            //Log.Debug(LogConfig.LogStart("GetSOAPAdditionalInfo", logParam));
                            var soapadditional = clsSOAP.GetSOAPAdditionalInfo(long.Parse(Helper.GetDoctorID(this.Parent.Page)), Helper.organizationId);
                            var jsonsoapadditional = JsonConvert.DeserializeObject<ResultSOAPAdditionalInfo>(soapadditional.Result.ToString());
                            //Log.Debug(LogConfig.LogEnd("GetSOAPAdditionalInfo", jsonsoapadditional.Status, jsonsoapadditional.Message));

                            SOAPAdditionalInfo additional = jsonsoapadditional.list;

                            listordersetheader = additional.ordersetdrug;
                            ordersetdt = Helper.ToDataTable(listordersetheader);

                            gvw_orderset.DataSource = ordersetdt;
                            gvw_orderset.DataBind();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "saveorderset", "warningnotificationOption(); toastr.warning('" + Message.ToLower() + " order set name <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Order Set Fail!');", true);
                        }
                    }
                }
            }
            else
            {
                headerRacikanName.Text = headerlblRacikanName.Text;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "textEmpty", "alert('Order Set Name Can not be Empty!');", true);
            }

            UpdatePanelOrderSet.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "LinkSaveAsOrderSetRacikan_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "LinkSaveAsOrderSetRacikan_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void LinkSaveAsOrderSetRacikanAdd_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        Dictionary<string, string> logParam = new Dictionary<string, string>();

        try
        {

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            HiddenField headerid = (HiddenField)gvw_racikan_header_add.Rows[selRowIndex].FindControl("HF_headerid_racikan");
            TextBox headerRacikanName = (TextBox)gvw_racikan_header_add.Rows[selRowIndex].FindControl("TextBoxSaveAsOrderSetNameRacikanAdd");
            Label headerlblRacikanName = (Label)gvw_racikan_header_add.Rows[selRowIndex].FindControl("lbl_nama_racikan");
            inputhidden_prescription_compound_header_id.Value = headerid.Value.ToString();

            if (headerRacikanName.Text != "")
            {
                if (Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value] == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "detailEmpty", "alert('For Order Set, Detail Can not be Empty!');", true);
                }
                else
                {

                    CompoundOrderSet compoundOS = new CompoundOrderSet();

                    List<CompoundHeaderSoap> dataheader_add = GetRowListRacikanHeaderAdditional();
                    CompoundHeaderSoap headerOS = dataheader_add.Where(head => head.prescription_compound_header_id == Guid.Parse(headerid.Value.ToString())).First();
                    headerOS.compound_name = headerRacikanName.Text;
                    compoundOS.header = headerOS;

                    DataTable dt_detail_add = (DataTable)Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value];
                    DataRow[] dr_detail_add = dt_detail_add.Select("prescription_compound_header_id = '" + headerid.Value.ToString() + "'");
                    if (dr_detail_add.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "detailEmpty", "alert('For Order Set, Detail Can not be Empty!');", true);
                    }
                    else
                    {
                        DataTable dt_detail_select_add = dt_detail_add.Select("prescription_compound_header_id = '" + headerid.Value.ToString() + "'").CopyToDataTable();
                        compoundOS.detail = Helper.ToDataList<CompoundDetailSoap>(dt_detail_select_add);


                        long doctorid = long.Parse(Helper.GetDoctorID(this.Parent.Page));

                        logParam = new Dictionary<string, string>
                        {
                            { "Doctor_ID", doctorid.ToString() }
                        };
                        //Log.Debug(LogConfig.LogStart("insertSaveAsOrderSetRacikan", logParam, LogConfig.JsonToString(compoundOS)));
                        var SaveAS = clsOrderSet.insertSaveAsOrderSetRacikan(compoundOS, doctorid);
                        var JsongetSaveAS = (JObject)JsonConvert.DeserializeObject<dynamic>(SaveAS.Result);
                        var Status = JsongetSaveAS.Property("status").Value.ToString();
                        var Message = JsongetSaveAS.Property("data").Value.ToString();
                        //Log.Debug(LogConfig.LogEnd("insertSaveAsOrderSetRacikan", Status, Message));

                        if (Message.ToUpper() == "SUCCESS")
                        {
                            ShowToastr("Save as Order Set successful.", "Success", "Success");

                            logParam = new Dictionary<string, string>
                            {
                                { "Doctor_ID", Helper.GetDoctorID(this.Parent.Page) },
                                { "Organization_ID", Helper.organizationId.ToString() }
                            };
                            //Log.Debug(LogConfig.LogStart("GetSOAPAdditionalInfo", logParam));
                            var soapadditional = clsSOAP.GetSOAPAdditionalInfo(long.Parse(Helper.GetDoctorID(this.Parent.Page)), Helper.organizationId);
                            var jsonsoapadditional = JsonConvert.DeserializeObject<ResultSOAPAdditionalInfo>(soapadditional.Result.ToString());
                            //Log.Debug(LogConfig.LogEnd("GetSOAPAdditionalInfo", jsonsoapadditional.Status, jsonsoapadditional.Message));
                            SOAPAdditionalInfo additional = jsonsoapadditional.list;

                            listordersetheader = additional.ordersetdrug;
                            ordersetdt = Helper.ToDataTable(listordersetheader);

                            gvw_orderset.DataSource = ordersetdt;
                            gvw_orderset.DataBind();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "saveorderset", "warningnotificationOption(); toastr.warning('" + Message.ToLower() + " order set name <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Order Set Fail!');", true);
                        }
                    }
                }
            }
            else
            {
                headerRacikanName.Text = headerlblRacikanName.Text;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "textEmpty", "alert('Order Set Name Can not be Empty!');", true);
            }

            UpdatePanelOrderSet.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "LinkSaveAsOrderSetRacikanAdd_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "LinkSaveAsOrderSetRacikanAdd_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }


    protected void linklabbutton_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        TabContainer1.Visible = true;
        TabContainer1.ActiveTabIndex = 0;
        HF_FlagFutureOrder.Value = "false";
        List<CpoeMapping> tempMap = new List<CpoeMapping>();
        tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
        stdclinic.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
        stdmdc.GetMappingMDC(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
        stdmicro.GetMappingMicroLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
        stdpanel.GetMappingPanelLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
        stdanatomi.GetMappingAnatomiLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
        stdcito.GetMappingCitoLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
        UP_ContainerLab.Update();

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "HF_FlagFutureOrder", HF_FlagFutureOrder.Value.ToString(), "linklabbutton_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
    }

    protected void linklabbutton_FutureOrder_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        TabContainer1.Visible = true;
        TabContainer1.ActiveTabIndex = 0;
        HF_FlagFutureOrder.Value = "true";
        List<CpoeMapping> tempMap = new List<CpoeMapping>();
        tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
        stdclinic.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
        stdmdc.GetMappingMDC(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
        stdmicro.GetMappingMicroLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
        stdpanel.GetMappingPanelLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
        stdanatomi.GetMappingAnatomiLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
        stdcito.GetMappingCitoLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrder.Value);
        UP_ContainerLab.Update();

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "HF_FlagFutureOrder", HF_FlagFutureOrder.Value.ToString(), "linklabbutton_FutureOrder_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
    }

    protected void btnCloseLab_Click(object sender, EventArgs e)
    {
        TabContainer1.Visible = false;
        UP_ContainerLab.Update();
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "loading", "hideloading();", true);
    }

    protected void linkradbutton_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        TabContainer2.Visible = true;
        TabContainer2.ActiveTabIndex = 0;
        HF_FlagFutureOrderRad.Value = "false";

        List<CpoeMapping> tempMap = new List<CpoeMapping>();
        tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
        stdxray.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
        stdusg.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
        stdctrad.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
        stdmrihalf.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
        stdmrifull.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
        UP_ContainerRad.Update();

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "HF_FlagFutureOrder", HF_FlagFutureOrder.Value.ToString(), "linkradbutton_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
    }

    protected void linkradbutton_FutureOrder_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        TabContainer2.Visible = true;
        TabContainer2.ActiveTabIndex = 0;
        HF_FlagFutureOrderRad.Value = "true";

        List<CpoeMapping> tempMap = new List<CpoeMapping>();
        tempMap = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
        stdxray.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
        stdusg.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
        stdctrad.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
        stdmrihalf.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
        stdmrifull.GetMappingClinicLab(tempMap, hfguidadditional.Value, HF_FlagFutureOrderRad.Value);
        UP_ContainerRad.Update();

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "HF_FlagFutureOrder", HF_FlagFutureOrder.Value.ToString(), "linkradbutton_FutureOrder_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
    }

    protected void btnCloseRad_Click(object sender, EventArgs e)
    {
        TabContainer2.Visible = false;
        UP_ContainerRad.Update();
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "loading", "hideloading();", true);
    }

    protected void is_dosetext_CheckedChanged(object sender, EventArgs e)
    {
        //int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        List<Prescription> dataDrug = GetRowList(1);
        DataTable dt = Helper.ToDataTable(dataDrug);

        Session[Helper.SessionDrugPres + hfguidadditional.Value] = dt;
        gvw_drug.DataSource = dt;
        gvw_drug.DataBind();

    }

    protected void is_dosetext_add_CheckedChanged(object sender, EventArgs e)
    {
        //int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        List<Prescription> dataDrug = GetRowList(6);
        DataTable dt = Helper.ToDataTable(dataDrug);

        Session[Helper.Sessionadditionalpres + hfguidadditional.Value] = dt;
        gvwAdditionalDrugs.DataSource = dt;
        gvwAdditionalDrugs.DataBind();

    }

    protected void racikan_is_dosetext_CheckedChanged(object sender, EventArgs e)
    {
        //int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        List<CompoundDetailSoap> data = GetRowListRacikanModal(inputhidden_isadditionaltype.Value);
        DataTable dt = Helper.ToDataTable(data);

        Session[Helper.SessionTempInputRacikanDetail + hfguidadditional.Value] = dt;
        input_gvw_racikan_detail.DataSource = dt;
        input_gvw_racikan_detail.DataBind();
    }

    protected void input_is_dosetext_CheckedChanged(object sender, EventArgs e)
    {
        if (input_is_dosetext.Checked == true)
        {
            //input_dvdose.Visible = false;
            //input_dosetext.Visible = true;
            input_dvdose.Style.Add("display", "none");
            input_dosetext.Style.Add("display", "");
        }
        else if (input_is_dosetext.Checked == false)
        {
            //input_dvdose.Visible = true;
            //input_dosetext.Visible = false;
            input_dvdose.Style.Add("display", "");
            input_dosetext.Style.Add("display", "none");
        }
    }

    public void UpdateListPrescription()
    {
        upSaveAsOrderSet.Update();
        upFormularium.Update();
        UpdatePanelListPrescription.Update();
        UpdatePanelListPrescriptionAdd.Update();

        UpdatePanel_gvw_racikan.Update();
        UpdatePanel_gvw_racikan_add.Update();

        UpdatePanelConsumable.Update();
        UpdatePanelConsumableAdd.Update();
    }

    public void UpdateListAllergy()
    {
        UpdatePanelAllergyPlanning.Update();
    }

    public void UpdateListRoutine()
    {
        UpdatePanelRoutinePlanning.Update();
    }

    public void MyParentMethodLab()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            //StdClinicControl.checkIfExist += new Form_CPOE_Control_Template_StdClinicLabPage.customHandler(MyParentMethod);
            listchecked = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];
            if (listchecked != null)
            {
                if(HF_FlagFutureOrder.Value == "false")
				{

                    if (Helper.ToDataTable(listchecked).Select("isdelete = 0 AND IsFutureOrder = false").Count() > 0)
                    {
                labempty.Style.Add("display", "none");
                linklabbutton.Style.Add("display", "none");
                btnEditLab.Visible = true;
                btnResetLab.Visible = true;
                        DataTable dt = Helper.ToDataTable(listchecked).Select("isdelete = 0 AND IsFutureOrder = false").CopyToDataTable();
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();

                    HyperLinkSaveAsLab.Style.Add("display", "");
                }
                else
                {
                    labempty.Style.Add("display", "");
                    linklabbutton.Style.Add("display", "");
                    btnEditLab.Visible = false;
                    btnResetLab.Visible = false;
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();

                    HyperLinkSaveAsLab.Style.Add("display", "none");
                }
				}
                else if(HF_FlagFutureOrder.Value == "true")
				{

                    if (Helper.ToDataTable(listchecked).Select("isdelete = 0 AND IsFutureOrder = true").Count() > 0)
                    {
                        labempty_FutureOrder.Style.Add("display", "none");
                        linklabbutton_FutureOrder.Style.Add("display", "none");
                        btnEditLab_FutureOrder.Visible = true;
                        btnResetLab_FutureOrder.Visible = true;
                        DataTable dt = Helper.ToDataTable(listchecked).Select("isdelete = 0 AND IsFutureOrder = true").CopyToDataTable();
                        Repeater1_FutureOrder.DataSource = dt;
                        Repeater1_FutureOrder.DataBind();

                        HyperLinkSaveAsLab_FutureOrder.Style.Add("display", "");
                    }
                    else
                    {
                        labempty_FutureOrder.Style.Add("display", "");
                        linklabbutton_FutureOrder.Style.Add("display", "");
                        btnEditLab_FutureOrder.Visible = false;
                        btnResetLab_FutureOrder.Visible = false;
                        Repeater1_FutureOrder.DataSource = null;
                        Repeater1_FutureOrder.DataBind();

                        HyperLinkSaveAsLab_FutureOrder.Style.Add("display", "none");
                    }
                }

            }
            else
            {
                labempty.Style.Add("display", "");
                linklabbutton.Style.Add("display", "");
                btnEditLab.Visible = false;
                btnResetLab.Visible = false;
                Repeater1.DataSource = null;
                Repeater1.DataBind();

                HyperLinkSaveAsLab.Style.Add("display", "none");

                labempty_FutureOrder.Style.Add("display", "");
                linklabbutton_FutureOrder.Style.Add("display", "");
                btnEditLab_FutureOrder.Visible = false;
                btnResetLab_FutureOrder.Visible = false;
                Repeater1_FutureOrder.DataSource = null;
                Repeater1_FutureOrder.DataBind();

                HyperLinkSaveAsLab_FutureOrder.Style.Add("display", "none");
            }
            List<CpoeMapping> getMapJson = (List<CpoeMapping>)Session[Helper.Sessionmaplab];
            stdclinic.GetMappingClinicLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrder.Value);
            stdcito.GetMappingCitoLab(getMapJson, hfguidadditional.Value, HF_FlagFutureOrder.Value);

            UpdatePanelDivLab.Update();
            UpdatePanelDivLab_FutureOrder.Update();
            UP_ContainerLab.Update();

            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "loading", "hideloading();", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "MyParentMethodLab", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "MyParentMethodLab", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnGetValueCPOE_Lab(object sender, EventArgs e)
    {
        string objectcpoe = hfbuilderobject.Value;
        if (objectcpoe.Length > 0)
        {
            objectcpoe = objectcpoe.Remove(objectcpoe.Length - 1, 1);
            objectcpoe = "[" + objectcpoe + "]";
            List<CpoeTrans> tempcurrmed = new JavaScriptSerializer().Deserialize<List<CpoeTrans>>(objectcpoe);

            var duplicatedlab =
                            from p in tempcurrmed
                            group p by new { p.id, p.IsFutureOrder } into g
                            where g.Count() > 1
                            select g.Key;

            if (duplicatedlab.Count() > 0)
            {
                tempcurrmed = tempcurrmed.GroupBy(i => i.id).Select(g => g.First()).ToList();
            }

            //Button chk = (Button)sender;
            Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = tempcurrmed;
            //checkIfExist(sender);
            MyParentMethodLab();
        }
        else
        {
            //Button chk = (Button)sender;
            Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = null;
            //checkIfExist(sender);
            MyParentMethodLab();
        }
        TabContainer1.Visible = false;
        HF_FlagTabLab.Value = "";

        //stdclinic.Visible = true;
        //HF_FlagTabClinical.Value = "open";

        stdmicro.Visible = false;
        HF_FlagTabMicrobiology.Value = "";

        stdcito.Visible = false;
        HF_FlagTabCITO.Value = "";

        stdanatomi.Visible = false;
        HF_FlagTabAnatomical.Value = "";

        stdmdc.Visible = false;
        HF_FlagTabMDC.Value = "";

        stdpanel.Visible = false;
        HF_FlagTabPanel.Value = "";

        UP_ContainerLab.Update();
    }

    public void MyParentMethodRad()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            //StdClinicControl.checkIfExist += new Form_CPOE_Control_Template_StdClinicLabPage.customHandler(MyParentMethod);
            listchecked = (List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value];

            if (listchecked != null)
            {
                List<CpoeTrans> listcheckedshow = new List<CpoeTrans>();
                foreach (var list in listchecked)
                {
                    CpoeTrans temp = new CpoeTrans();
                    temp.id = list.id;
                    temp.ischeck = list.ischeck;
                    temp.iscito = list.iscito;
                    temp.isdelete = list.isdelete;
                    temp.isnew = list.isnew;
                    temp.issubmit = list.issubmit;
                    if (list.remarks != "")
                        temp.name = list.name + " (" + list.remarks + ")";
                    else
                        temp.name = list.name;

                    temp.type = list.type;
                    temp.remarks = list.remarks;
                    temp.IsSendHope = list.IsSendHope;
                    temp.IsFutureOrder = list.IsFutureOrder;
                    temp.FutureOrderDate = list.FutureOrderDate;
                    listcheckedshow.Add(temp);
                }

                if ( HF_FlagFutureOrderRad.Value == "false")
				{
                radempty.Style.Add("display", "none");
                linkradbutton.Style.Add("display", "none");
                btnEditRad.Visible = true;
                btnResetRad.Visible = true;
                divcitorad.Visible = true;
                    if (Helper.ToDataTable(listcheckedshow).Select("isdelete = 0 AND IsFutureOrder = false").Count() > 0)
                {
                        DataTable dt = Helper.ToDataTable(listcheckedshow).Select("isdelete = 0 AND IsFutureOrder = false").CopyToDataTable();
                    rptRadiology.DataSource = dt;
                    rptRadiology.DataBind();
                    //if (listcheckedshow.Where(y => y.iscito == 1 && y.isdelete == 0).Count() > 0)
                    //{
                    //    chkcitorad.Checked = true;
                    //}
                    //else
                    //    chkcitorad.Checked = false;
                }
                else
                {
                    radempty.Style.Add("display", "");
                    linkradbutton.Style.Add("display", "");
                    btnEditRad.Visible = false;
                    btnResetRad.Visible = false;
                    divcitorad.Visible = false;
                    chkcitorad.Checked = false;
                    rptRadiology.DataSource = null;
                    rptRadiology.DataBind();
                }
                } 
                else if(HF_FlagFutureOrderRad.Value == "true")
				{
                    radempty_FutureOrder.Style.Add("display", "none");
                    linkradbutton_FutureOrder.Style.Add("display", "none");
                    btnEditRad_FutureOrder.Visible = true;
                    btnResetRad_FutureOrder.Visible = true;
                    divcitorad_FutureOrder.Visible = true;
                    if (Helper.ToDataTable(listcheckedshow).Select("isdelete = 0 AND IsFutureOrder = true").Count() > 0)
                    {
                        DataTable dt = Helper.ToDataTable(listcheckedshow).Select("isdelete = 0 AND IsFutureOrder = true").CopyToDataTable();
                        rptRadiology_FutureOrder.DataSource = dt;
                        rptRadiology_FutureOrder.DataBind();
                        //if (listcheckedshow.Where(y => y.iscito == 1 && y.isdelete == 0).Count() > 0)
                        //{
                        //    chkcitorad.Checked = true;
                        //}
                        //else
                        //    chkcitorad.Checked = false;
                    }
                    else
                    {
                        radempty_FutureOrder.Style.Add("display", "");
                        linkradbutton_FutureOrder.Style.Add("display", "");
                        btnEditRad_FutureOrder.Visible = false;
                        btnResetRad_FutureOrder.Visible = false;
                        divcitorad_FutureOrder.Visible = false;
                        chkcitorad_FutureOrder.Checked = false;
                        rptRadiology_FutureOrder.DataSource = null;
                        rptRadiology_FutureOrder.DataBind();
                    }
                }

                

            }
            else
            {
                radempty.Style.Add("display", "");
                linkradbutton.Style.Add("display", "");
                btnEditRad.Visible = false;
                btnResetRad.Visible = false;
                divcitorad.Visible = false;
                chkcitorad.Checked = false;
                rptRadiology.DataSource = null;
                rptRadiology.DataBind();

                radempty_FutureOrder.Style.Add("display", "");
                linkradbutton_FutureOrder.Style.Add("display", "");
                btnEditRad_FutureOrder.Visible = false;
                btnResetRad_FutureOrder.Visible = false;
                divcitorad_FutureOrder.Visible = false;
                chkcitorad_FutureOrder.Checked = false;
                rptRadiology_FutureOrder.DataSource = null;
                rptRadiology_FutureOrder.DataBind();
            }

            UpdatePanelDivRad.Update();
            UpdatePanelDivRad_FutureOrder.Update();
            UP_ContainerRad.Update();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "loading", "hideloading();", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "MyParentMethodRad", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "MyParentMethodRad", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnGetValueCPOE_Rad(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        string objectcpoe = hfbuilderobjectradiology.Value;
        if (objectcpoe.Length > 0)
        {
            objectcpoe = objectcpoe.Remove(objectcpoe.Length - 1, 1);
            objectcpoe = "[" + objectcpoe + "]";
            List<CpoeTrans> tempcurrmed = new JavaScriptSerializer().Deserialize<List<CpoeTrans>>(objectcpoe);

            var duplicatedlab =
                            from p in tempcurrmed
                            group p by new { p.id, p.IsFutureOrder } into g
                            where g.Count() > 1
                            select g.Key;

            if (duplicatedlab.Count() > 0)
            {
                tempcurrmed = tempcurrmed.GroupBy(i => i.id).Select(g => g.First()).ToList();
            }


            //Button chk = (Button)sender;
            Session[Helper.Sessionradcheck + hfguidadditional.Value] = tempcurrmed;
            //checkIfExist(sender);
            MyParentMethodRad();
        }
        else
        {
            //Button chk = (Button)sender;
            Session[Helper.Sessionradcheck + hfguidadditional.Value] = null;
            //checkIfExist(sender);
            MyParentMethodRad();
        }

        TabContainer2.Visible = false;
        HF_FlagTabRad.Value = "";

        //stdxray.Visible = true;
        //HF_FlagTabXray.Value = "open";

        stdusg.Visible = false;
        HF_FlagTabUSG.Value = "";

        stdctrad.Visible = false;
        HF_FlagTabCT.Value = "";

        stdmrihalf.Visible = false;
        HF_FlagTabMRI1.Value = "";

        stdmrifull.Visible = false;
        HF_FlagTabMRI3.Value = "";

        UP_ContainerRad.Update();



        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnGetValueCPOE_Rad", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonChooseTabLab_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        if (HF_FlagTabLab.Value == "Clinical")
        {
            stdclinic.Visible = true;
            HF_FlagTabClinical.Value = "open";
        }
        else if (HF_FlagTabLab.Value == "Microbiology")
        {
            stdmicro.Visible = true;
            HF_FlagTabMicrobiology.Value = "open";
        }
        else if (HF_FlagTabLab.Value == "CITO")
        {
            stdcito.Visible = true;
            HF_FlagTabCITO.Value = "open";
        }
        else if (HF_FlagTabLab.Value == "Anatomical")
        {
            stdanatomi.Visible = true;
            HF_FlagTabAnatomical.Value = "open";
        }
        else if (HF_FlagTabLab.Value == "MDC")
        {
            stdmdc.Visible = true;
            HF_FlagTabMDC.Value = "open";
        }
        else if (HF_FlagTabLab.Value == "Panel")
        {
            stdpanel.Visible = true;
            HF_FlagTabPanel.Value = "open";
        }

        UP_ContainerLab.Update();



        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonChooseTabLab_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }


    protected void ButtonChooseTabRad_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        if (HF_FlagTabRad.Value == "Xray")
        {
            stdxray.Visible = true;
            HF_FlagTabXray.Value = "open";
        }
        else if (HF_FlagTabRad.Value == "USG")
        {
            stdusg.Visible = true;
            HF_FlagTabUSG.Value = "open";
        }
        else if (HF_FlagTabRad.Value == "CT")
        {
            stdctrad.Visible = true;
            HF_FlagTabCT.Value = "open";
        }
        else if (HF_FlagTabRad.Value == "MRI1")
        {
            stdmrihalf.Visible = true;
            HF_FlagTabMRI1.Value = "open";
        }
        else if (HF_FlagTabRad.Value == "MRI3")
        {
            stdmrifull.Visible = true;
            HF_FlagTabMRI3.Value = "open";
        }

        UP_ContainerRad.Update();



        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonChooseTabRad_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());

        //Other method
        //TabContainer2.Visible = true;
        ////UP_ContainerLab.Update();

        //if (HF_FlagTabRad.Value == "All")
        //{
        //    XRay.Visible = true;
        //    USG.Visible = true;
        //    CT.Visible = true;
        //    MRI1.Visible = true;
        //    MRI3.Visible = true;
        //}
        //else if (HF_FlagTabRad.Value == "Xray")
        //{
        //    XRay.Visible = true;
        //    USG.Visible = false;
        //    CT.Visible = false;
        //    MRI1.Visible = false;
        //    MRI3.Visible = false;
        //}
        //else if (HF_FlagTabRad.Value == "USG")
        //{
        //    XRay.Visible = false;
        //    USG.Visible = true;
        //    CT.Visible = false;
        //    MRI1.Visible = false;
        //    MRI3.Visible = false;
        //}
        //else if (HF_FlagTabRad.Value == "CT")
        //{
        //    XRay.Visible = false;
        //    USG.Visible = false;
        //    CT.Visible = true;
        //    MRI1.Visible = false;
        //    MRI3.Visible = false;
        //}
        //else if (HF_FlagTabRad.Value == "MRI1")
        //{
        //    XRay.Visible = false;
        //    USG.Visible = false;
        //    CT.Visible = false;
        //    MRI1.Visible = true;
        //    MRI3.Visible = false;
        //}
        //else if (HF_FlagTabRad.Value == "MRI3")
        //{
        //    XRay.Visible = false;
        //    USG.Visible = false;
        //    CT.Visible = false;
        //    MRI1.Visible = false;
        //    MRI3.Visible = true;
        //}
    }

    public string GenerateSeverityColor(DrugsInteraction di)
    {
        int level = 0;
        if (di.drugToAllergyInteraction == true && di.drugToAllergySeverity != null && di.drugToAllergySeverity != "")
        {
            int x = int.Parse(di.drugToAllergySeverity.Split(',')[1]);
            if (x > level)
            {
                level = x;
            }
        }
        if (di.drugToDrugInteraction == true && di.drugToDrugSeverity != null && di.drugToDrugSeverity != "")
        {
            int x = int.Parse(di.drugToDrugSeverity.Split(',')[1]);
            if (x > level)
            {
                level = x;
            }
        }
        if (di.drugToHealthInteraction == true && di.drugToHealthSeverity != null && di.drugToHealthSeverity != "")
        {
            int x = int.Parse(di.drugToHealthSeverity.Split(',')[1]);
            if (x > level)
            {
                level = x;
            }
        }
        if (di.drugToLactationInteraction == true && di.drugToLactationSeverity != null && di.drugToLactationSeverity != "")
        {
            int x = int.Parse(di.drugToLactationSeverity.Split(',')[1]);
            if (x > level)
            {
                level = x;
            }
        }
        if (di.drugToPregnancyInteraction == true && di.drugToPregnancySeverity != null && di.drugToPregnancySeverity != "")
        {
            int x = int.Parse(di.drugToPregnancySeverity.Split(',')[1]);
            if (x > level)
            {
                level = x;
            }
        }
        if (di.duplicateIngredient == true && di.duplicateIngredientSeverity != null && di.duplicateIngredientSeverity != "")
        {
            int x = int.Parse(di.duplicateIngredientSeverity.Split(',')[1]);
            if (x > level)
            {
                level = x;
            }
        }
        if (di.duplicateTherapy == true && di.duplicateTherapySeverity != null && di.duplicateTherapySeverity != "")
        {
            int x = int.Parse(di.duplicateTherapySeverity.Split(',')[1]);
            if (x > level)
            {
                level = x;
            }
        }

        string severitycolor = "";
        if (level <= 2)
        {
            severitycolor = "m-warn-icon-ylw";
        }
        else if (level == 3)
        {
            severitycolor = "m-warn-icon-org";
        }
        else if (level >= 4)
        {
            severitycolor = "m-warn-icon-red";
        }

        return severitycolor;

    }


    //GAK DIPAKAI LAGI
    /*
    public void CheckDrugInteraction(bool flagisadditional)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        string rspn_status = "", rspn_message = "";

        try
        {
            PatientHeader ptndata = (PatientHeader)Session[CstSession.SessionPatientHeader];

            MimsModel mimsdata = new MimsModel();

            mimsdata.organizationId = long.Parse(MyUser.GetHopeOrgID());
            mimsdata.admissionId = long.Parse(Request.QueryString["AdmissionId"].ToString());
            mimsdata.createdBy = MyUser.GetUsername();
            mimsdata.gender = ptndata.Gender == 1 ? "M" : "F";
            mimsdata.monthOfPregnancy = 0;
            mimsdata.age = clsCommon.GetAgeYears(ptndata.BirthDate);
            mimsdata.nursing = true;
            mimsdata.salesItemIds = new List<Int64>();
            mimsdata.allergyIds = new List<Int64>();

            List<Prescription> drugdata = new List<Prescription>();
            DataTable dtcmpdet = new DataTable();

            if (flagisadditional == false)
            {
                drugdata = GetRowList(1);
                dtcmpdet = (DataTable)Session[Helper.SessionRacikanDetail + hfguidadditional.Value];
            }
            else if (flagisadditional == true)
            {
                drugdata = GetRowList(6);
                dtcmpdet = (DataTable)Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value];
            }

            if (drugdata.Count > 0)
            {
                foreach (Prescription p in drugdata)
                {
                    mimsdata.salesItemIds.Add(p.item_id);
                }
            }

            if (dtcmpdet != null)
            {
                for (int i = 0; i < dtcmpdet.Rows.Count; i++)
                {
                    mimsdata.salesItemIds.Add(long.Parse(dtcmpdet.Rows[i]["item_id"].ToString()));
                }
            }

            if (mimsdata.salesItemIds.Count > 0)
            {

                var PostResponse = clsMims.CheckDrugInteraction(mimsdata);
                var PostJson = JsonConvert.DeserializeObject<ResponseMimsInteraction>(PostResponse.Result);
                rspn_status = PostJson.Status.Replace(@"'", "");
                rspn_message = PostJson.Message.Replace(@"'", ""); ;

                if (rspn_status == "Success")
                {
                    if (PostJson.Data.drugsInteraction.Count > 0)
                    {
                        MimsInteractionWithLog datainter = PostJson.Data;
                        //lblMimsHtmlResult.Text = datainter.htmlResult;
                        Session[Helper.SessionMimsResultData] = datainter;

                        foreach (Prescription pp in drugdata)
                        {
                            pp.cims_result = "fa fa-check-circle m-safe-icon";
                            foreach (DrugsInteraction di in datainter.drugsInteraction)
                            {
                                if (pp.item_id == di.salesItemId)
                                {
                                    if (di.drugToAllergyInteraction == true || di.drugToDrugInteraction == true || di.drugToHealthInteraction == true || di.drugToLactationInteraction == true || di.drugToPregnancyInteraction == true || di.duplicateIngredient == true || di.duplicateTherapy == true)
                                    {
                                        string severity = GenerateSeverityColor(di);
                                        pp.cims_result = "fa fa-warning " + severity;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MimsInteractionWithLog datainter = PostJson.Data;
                        //lblMimsHtmlResult.Text = datainter.htmlResult;
                        Session[Helper.SessionMimsResultData] = datainter;

                        foreach (Prescription pp in drugdata)
                        {
                            pp.cims_result = "fa fa-check-circle m-safe-icon";
                        }
                    }

                    //drugdata[0].cims_result = "fa fa-warning m-warn-icon-red";

                    DataTable dta = Helper.ToDataTable(drugdata);
                    if (flagisadditional == false)
                    {
                        Session[Helper.SessionDrugPres + hfguidadditional.Value] = dta;
                        gvw_drug.DataSource = dta;
                        gvw_drug.DataBind();
                        UpdatePanelListPrescription.Update();
                    }
                    else if (flagisadditional == true)
                    {
                        Session[Helper.Sessionadditionalpres + hfguidadditional.Value] = dta;
                        gvwAdditionalDrugs.DataSource = dta;
                        gvwAdditionalDrugs.DataBind();
                        UpdatePanelListPrescriptionAdd.Update();
                    }

                }
                else if (rspn_status == "Fail")
                {
                    ShowToastr(rspn_message, rspn_status, "Error");
                    //ScriptManager.RegisterStartupScript(Page, GetType(), "alertmims", "alert('Fail : " + rspn_message + "')", true);
                }
            }
            else
            {
                ShowToastr("Please Add Drugs First", "Warning", "Warning");
                //ScriptManager.RegisterStartupScript(Page, GetType(), "alertdrugs", "alert('Please Add Drugs First')", true);
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CheckDrugInteraction", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CheckDrugInteraction", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            throw ex;
        }

        //Log.Info(LogConfig.LogEnd());
    }
    */

    public List<LogDrugDetailModel> GetDetailDrugForMims()
    {
        List<LogDrugDetailModel> alldata = new List<LogDrugDetailModel>();
        
        //DRUG
        foreach (GridViewRow rows in gvw_drug.Rows)
        {
            HiddenField item_id = (HiddenField)rows.FindControl("item_id");
            Label item_name = (Label)rows.FindControl("item_name");
            TextBox quantity = (TextBox)rows.FindControl("quantity");
            Label uom_code = (Label)rows.FindControl("uom_code");
            DropDownList frequency_code = (DropDownList)rows.FindControl("frequency_code");
            TextBox dosage_id = (TextBox)rows.FindControl("dosage_id");
            DropDownList dosage = (DropDownList)rows.FindControl("doseuom");
            HiddenField dose_text = (HiddenField)rows.FindControl("dose_text");
            TextBox remarks = (TextBox)rows.FindControl("remarks");
            DropDownList administrationRouteCode = (DropDownList)rows.FindControl("administrationRouteCode");
            TextBox iteration = (TextBox)rows.FindControl("iteration");
            CheckBox is_routine = (CheckBox)rows.FindControl("is_routine");        
            CheckBox is_dosetext = (CheckBox)rows.FindControl("is_dosetext");
            TextBox dosetext = (TextBox)rows.FindControl("dosetext");

            LogDrugDetailModel row = new LogDrugDetailModel();

            row.LogDrugDetailId = 0;
            row.LogDrugHeaderId = 0;
            row.Revision = 0;
            row.IsAdditional = false;
            row.ItemType = "Drug";
            row.ItemId = Int64.Parse(item_id.Value);
            row.ItemName = item_name.Text;
            row.Dose = dosage_id.Text.ToString() == "" ? "0" : dosage_id.Text.ToString().Replace(",", ".") + " " + dosage.SelectedItem.Text;
            row.Frequency = frequency_code.SelectedItem.Text;
            row.Route = administrationRouteCode.SelectedItem.Text;
            row.Instruction = remarks.Text;
            row.Qty = quantity.Text.ToString() == "" ? "0" : quantity.Text.ToString().Replace(",", ".");
            row.Uom = uom_code.Text;
            row.Iter = iteration.Text == "" ? 0 : int.Parse(iteration.Text);
            row.Routine = is_routine.Checked.ToString();
            row.IsLatest = true;
            row.CreatedBy = MyUser.GetUsername();
            row.CreatedDate = DateTime.Now;

            alldata.Add(row);
        }

        //ADD DRUG
        foreach (GridViewRow rows in gvwAdditionalDrugs.Rows)
        {
            HiddenField item_id = (HiddenField)rows.FindControl("item_id");
            Label item_name = (Label)rows.FindControl("item_name");
            TextBox quantity = (TextBox)rows.FindControl("quantity");
            
            Label uom_code = (Label)rows.FindControl("uom_code");
            DropDownList frequency_code = (DropDownList)rows.FindControl("frequency_code");
            TextBox dosage_id = (TextBox)rows.FindControl("dosage_id");
            DropDownList dosage = (DropDownList)rows.FindControl("doseuom");
            HiddenField dose_text = (HiddenField)rows.FindControl("dose_text");
            TextBox remarks = (TextBox)rows.FindControl("remarks");
            DropDownList administrationRouteCode = (DropDownList)rows.FindControl("administrationRouteCode");
            TextBox iteration = (TextBox)rows.FindControl("iteration");
            CheckBox is_routine = (CheckBox)rows.FindControl("is_routine");      
            CheckBox is_dosetext = (CheckBox)rows.FindControl("is_dosetext");
            TextBox dosetext = (TextBox)rows.FindControl("dosetext");

            LogDrugDetailModel row = new LogDrugDetailModel();

            row.LogDrugDetailId = 0;
            row.LogDrugHeaderId = 0;
            row.Revision = 0;
            row.IsAdditional = true;
            row.ItemType = "Drug Additional";
            row.ItemId = Int64.Parse(item_id.Value);
            row.ItemName = item_name.Text;
            row.Dose = dosage_id.Text.ToString() == "" ? "0" : dosage_id.Text.ToString().Replace(",", ".") + " " + dosage.SelectedItem.Text;
            row.Frequency = frequency_code.SelectedItem.Text;
            row.Route = administrationRouteCode.SelectedItem.Text;
            row.Instruction = remarks.Text;
            row.Qty = quantity.Text.ToString() == "" ? "0" : quantity.Text.ToString().Replace(",", ".");
            row.Uom = uom_code.Text;
            row.Iter = iteration.Text == "" ? 0 : int.Parse(iteration.Text);
            row.Routine = is_routine.Checked.ToString();
            row.IsLatest = true;
            row.CreatedBy = MyUser.GetUsername();
            row.CreatedDate = DateTime.Now;

            alldata.Add(row);
        }

        DataTable dataRacikanHeader = (DataTable)Session[Helper.SessionRacikanHeader + hfguidadditional.Value];
        DataTable dataRacikanDetail = (DataTable)Session[Helper.SessionRacikanDetail + hfguidadditional.Value];
        DataTable dataRacikanHeader_add = (DataTable)Session[Helper.SessionRacikanHeaderAdd + hfguidadditional.Value];
        DataTable dataRacikanDetail_add = (DataTable)Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value];

        if (dataRacikanHeader != null && dataRacikanHeader.Rows.Count != 0)
        {
            //Compound Header
            foreach (GridViewRow rows in gvw_racikan_header.Rows)
            {
                HiddenField hidden_header_id = (HiddenField)rows.FindControl("HF_headerid_racikan");          
                HiddenField hidden_isDoseText = (HiddenField)rows.FindControl("HF_isdosetext_racikan");

                Label item_name = (Label)rows.FindControl("lbl_nama_racikan");
                Label item_dosis = (Label)rows.FindControl("lbl_dosis_racikan");
                Label item_dosisunit = (Label)rows.FindControl("lbl_dosisunit_racikan");
                Label item_frekuensi = (Label)rows.FindControl("lbl_frekuensi_racikan");
                Label item_rute = (Label)rows.FindControl("lbl_rute_racikan");
                Label item_instruksi = (Label)rows.FindControl("lbl_instruksi_racikan");
                Label item_jml = (Label)rows.FindControl("lbl_jml_racikan");
                Label item_unit = (Label)rows.FindControl("lbl_unit_racikan");
                Label item_iter = (Label)rows.FindControl("lbl_iter_racikan");
                Label item_dosistext = (Label)rows.FindControl("lbl_dosistext_racikan");

                LogDrugDetailModel row = new LogDrugDetailModel();

                row.LogDrugDetailId = 0;
                row.LogDrugHeaderId = 0;
                row.Revision = 0;
                row.IsAdditional = false;
                row.ItemType = "Compound Header";
                row.ItemId = 0;
                row.ItemName = item_name.Text;
                row.Dose = item_dosis.Text.ToString() == "" ? "0" : item_dosis.Text.ToString().Replace(",", ".") + " " + item_dosisunit.Text;
                row.Frequency = item_frekuensi.Text;
                row.Route = item_rute.Text;
                row.Instruction = item_instruksi.Text;
                row.Qty = item_jml.Text.ToString() == "" ? "0" : item_jml.Text.ToString().Replace(",", ".");
                row.Uom = item_unit.Text;
                row.Iter = item_iter.Text == "" ? 0 : int.Parse(item_iter.Text);
                row.Routine = "";
                row.IsLatest = true;
                row.CreatedBy = MyUser.GetUsername();
                row.CreatedDate = DateTime.Now;

                alldata.Add(row);
            }

            if (dataRacikanDetail != null && dataRacikanDetail.Rows.Count != 0)
            {
                List<CompoundDetailSoap> racdet =  Helper.ToDataList<CompoundDetailSoap>(dataRacikanDetail);
                foreach (CompoundDetailSoap rowsdet in racdet)
                {
                    LogDrugDetailModel row = new LogDrugDetailModel();

                    row.LogDrugDetailId = 0;
                    row.LogDrugHeaderId = 0;
                    row.Revision = 0;
                    row.IsAdditional = false;
                    row.ItemType = "Compound Detail";
                    row.ItemId = rowsdet.item_id;
                    row.ItemName = rowsdet.item_name;
                    row.Dose = rowsdet.dose == "" ? "0" : rowsdet.dose.Replace(",", ".") + " " + rowsdet.dose_uom_code;
                    row.Frequency = "";
                    row.Route = "";
                    row.Instruction = "";
                    row.Qty = rowsdet.quantity.ToString() == "" ? "0" : rowsdet.quantity.ToString().Replace(",", ".");
                    row.Uom = rowsdet.uom_code;
                    row.Iter = 0;
                    row.Routine = "";
                    row.IsLatest = true;
                    row.CreatedBy = MyUser.GetUsername();
                    row.CreatedDate = DateTime.Now;

                    alldata.Add(row);

                }
            }

        }

        if (dataRacikanHeader_add != null && dataRacikanHeader_add.Rows.Count != 0)
        {
            //Compound Header Additional
            foreach (GridViewRow rows in gvw_racikan_header_add.Rows)
            {
                HiddenField hidden_header_id = (HiddenField)rows.FindControl("HF_headerid_racikan");
                HiddenField hidden_isDoseText = (HiddenField)rows.FindControl("HF_isdosetext_racikan");

                Label item_name = (Label)rows.FindControl("lbl_nama_racikan");
                Label item_dosis = (Label)rows.FindControl("lbl_dosis_racikan");
                Label item_dosisunit = (Label)rows.FindControl("lbl_dosisunit_racikan");
                Label item_frekuensi = (Label)rows.FindControl("lbl_frekuensi_racikan");
                Label item_rute = (Label)rows.FindControl("lbl_rute_racikan");
                Label item_instruksi = (Label)rows.FindControl("lbl_instruksi_racikan");
                Label item_jml = (Label)rows.FindControl("lbl_jml_racikan");
                Label item_unit = (Label)rows.FindControl("lbl_unit_racikan");
                Label item_iter = (Label)rows.FindControl("lbl_iter_racikan");
                Label item_dosistext = (Label)rows.FindControl("lbl_dosistext_racikan");

                LogDrugDetailModel row = new LogDrugDetailModel();

                row.LogDrugDetailId = 0;
                row.LogDrugHeaderId = 0;
                row.Revision = 0;
                row.IsAdditional = true;
                row.ItemType = "Compound Header Additional";
                row.ItemId = 0;
                row.ItemName = item_name.Text;
                row.Dose = item_dosis.Text.ToString() == "" ? "0" : item_dosis.Text.ToString().Replace(",", ".") + " " + item_dosisunit.Text;
                row.Frequency = item_frekuensi.Text;
                row.Route = item_rute.Text;
                row.Instruction = item_instruksi.Text;
                row.Qty = item_jml.Text.ToString() == "" ? "0" : item_jml.Text.ToString().Replace(",", ".");
                row.Uom = item_unit.Text;
                row.Iter = item_iter.Text == "" ? 0 : int.Parse(item_iter.Text);
                row.Routine = "";
                row.IsLatest = true;
                row.CreatedBy = MyUser.GetUsername();
                row.CreatedDate = DateTime.Now;

                alldata.Add(row);
            }
            if (dataRacikanDetail_add != null && dataRacikanDetail_add.Rows.Count != 0)
            {
                List<CompoundDetailSoap> racdet = Helper.ToDataList<CompoundDetailSoap>(dataRacikanDetail_add);
                foreach (CompoundDetailSoap rowsdet in racdet)
                {
                    LogDrugDetailModel row = new LogDrugDetailModel();

                    row.LogDrugDetailId = 0;
                    row.LogDrugHeaderId = 0;
                    row.Revision = 0;
                    row.IsAdditional = true;
                    row.ItemType = "Compound Detail Additional";
                    row.ItemId = rowsdet.item_id;
                    row.ItemName = rowsdet.item_name;
                    row.Dose = rowsdet.dose == "" ? "0" : rowsdet.dose.Replace(",", ".") + " " + rowsdet.dose_uom_code;
                    row.Frequency = "";
                    row.Route = "";
                    row.Instruction = "";
                    row.Qty = rowsdet.quantity.ToString() == "" ? "0" : rowsdet.quantity.ToString().Replace(",", ".");
                    row.Uom = rowsdet.uom_code;
                    row.Iter = 0;
                    row.Routine = "";
                    row.IsLatest = true;
                    row.CreatedBy = MyUser.GetUsername();
                    row.CreatedDate = DateTime.Now;

                    alldata.Add(row);

                }
            }
        }

        return alldata;
    }

    public bool CheckDrugInteractionFunction(bool flagisadditional, string admNo, string mrNo)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        bool result = false;
        string rspn_status = "", rspn_message = "";

        try
        {
            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Find(y => y.setting_name.ToUpper() == "USE_MIMS".ToUpper()).setting_value == "TRUE")
            {

                PatientHeader ptndata = (PatientHeader)Session[CstSession.SessionPatientHeader];

                MimsModelWithDrugDetail mimsdata = new MimsModelWithDrugDetail();

                mimsdata.organizationId = long.Parse(MyUser.GetHopeOrgID());
                mimsdata.admissionId = long.Parse(Request.QueryString["AdmissionId"].ToString());
                mimsdata.createdBy = MyUser.GetUsername();
                mimsdata.gender = ptndata.Gender == 1 ? "M" : "F";
                mimsdata.monthOfPregnancy = 0;
                mimsdata.age = clsCommon.GetAgeYears(ptndata.BirthDate);
                mimsdata.nursing = true;
                mimsdata.salesItemIds = new List<Int64>();
                mimsdata.allergyIds = new List<Int64>();

                List<Prescription> drugdata = new List<Prescription>();
                List<Prescription> drugdata_add = new List<Prescription>();
                DataTable dtcmpdet = new DataTable();
                DataTable dtcmpdet_add = new DataTable();
                List<Int64> ListIdAllergy = (List<Int64>)Session[CstSession.SessionListAllergy_HI];
                List<Int64> ListIdRoutine = (List<Int64>)Session[CstSession.SessionListRoutine_HI];

                //if (flagisadditional == false)
                //{
                    drugdata = GetRowList(1);
                    dtcmpdet = (DataTable)Session[Helper.SessionRacikanDetail + hfguidadditional.Value];
                //}
                //else if (flagisadditional == true)
                //{
                    drugdata_add = GetRowList(6);
                    dtcmpdet_add = (DataTable)Session[Helper.SessionRacikanDetailAdd + hfguidadditional.Value];
                //}

                if (drugdata.Count > 0)
                {
                    foreach (Prescription p in drugdata)
                    {
                        mimsdata.salesItemIds.Add(p.item_id);
                    }
                }

                if (drugdata_add.Count > 0)
                {
                    foreach (Prescription p in drugdata_add)
                    {
                        mimsdata.salesItemIds.Add(p.item_id);
                    }
                }

                if (dtcmpdet != null)
                {
                    for (int i = 0; i < dtcmpdet.Rows.Count; i++)
                    {
                        mimsdata.salesItemIds.Add(long.Parse(dtcmpdet.Rows[i]["item_id"].ToString()));
                    }
                }

                if (dtcmpdet_add != null)
                {
                    for (int i = 0; i < dtcmpdet_add.Rows.Count; i++)
                    {
                        mimsdata.salesItemIds.Add(long.Parse(dtcmpdet_add.Rows[i]["item_id"].ToString()));
                    }
                }

                if (ListIdAllergy != null)
                {
                    foreach (Int64 a in ListIdAllergy)
                    {
                        mimsdata.allergyIds.Add(a);
                    }
                }

                //if (ListIdRoutine != null)
                //{
                //    List<Int64> rselected = new List<long>();
                //    rselected = ListIdRoutine.Except(mimsdata.salesItemIds).ToList();
                //    foreach (Int64 b in rselected)
                //    {
                //        mimsdata.salesItemIds.Add(b);
                //    }
                //}

                //For Drug Log
                mimsdata.LogHeader = new LogDrugHeaderModel();
                mimsdata.LogDetail = new List<LogDrugDetailModel>();

                mimsdata.LogHeader.LogDrugHeaderId = 0;
                mimsdata.LogHeader.LogDate = DateTime.Now;
                mimsdata.LogHeader.OrganizationId = long.Parse(MyUser.GetHopeOrgID());
                mimsdata.LogHeader.OrganizationName = MyUser.GetOrgName();
                mimsdata.LogHeader.Modul = "DOCTOR";
                mimsdata.LogHeader.UserName = MyUser.GetUsername();
                mimsdata.LogHeader.FullName = MyUser.GetFullname();
                mimsdata.LogHeader.AdmissionId = long.Parse(Request.QueryString["AdmissionId"].ToString());
                mimsdata.LogHeader.AdmissionNo = admNo;
                mimsdata.LogHeader.MrNo = mrNo;            
                mimsdata.LogHeader.IsLatest = true;
                mimsdata.LogHeader.CreatedBy = MyUser.GetUsername();
                mimsdata.LogHeader.CreatedDate = DateTime.Now;

                mimsdata.LogDetail = GetDetailDrugForMims();

                if (mimsdata.salesItemIds.Count > 0)
                {
                    var PostResponse = clsMims.CheckDrugInteraction(mimsdata);
                    var PostJson = JsonConvert.DeserializeObject<ResponseMimsInteraction>(PostResponse.Result);
                    rspn_status = PostJson.Status.Replace(@"'", "");
                    rspn_message = PostJson.Message.Replace(@"'", ""); ;

                    if (rspn_status == "Success")
                    {
                        if (PostJson.Data.drugsInteraction.Count > 0)
                        {
                            MimsInteractionWithLog datainter = PostJson.Data;
                            //lblMimsHtmlResult.Text = datainter.htmlResult;
                            Session[Helper.SessionMimsResultData] = datainter;

                            //foreach (Prescription pp in drugdata)
                            //{
                            //    pp.cims_result = "fa fa-check-circle m-safe-icon";
                            //    foreach (DrugsInteraction di in datainter.drugsInteraction)
                            //    {
                            //        if (pp.item_id == di.salesItemId)
                            //        {
                            //            if (di.drugToAllergyInteraction == true || di.drugToDrugInteraction == true || di.drugToHealthInteraction == true || di.drugToLactationInteraction == true || di.drugToPregnancyInteraction == true || di.duplicateIngredient == true || di.duplicateTherapy == true)
                            //            {
                            //                string severity = GenerateSeverityColor(di);
                            //                pp.cims_result = "fa fa-warning " + severity;

                            //                result = true;
                            //            }
                            //        }
                            //    }
                            //}

                            foreach (Int64 pp in mimsdata.salesItemIds)
                            {
                                foreach (DrugsInteraction di in datainter.drugsInteraction)
                                {
                                    if (pp == di.salesItemId)
                                    {
                                        if (di.drugToAllergyInteraction == true || di.drugToDrugInteraction == true || di.drugToHealthInteraction == true || di.drugToLactationInteraction == true || di.drugToPregnancyInteraction == true || di.duplicateIngredient == true || di.duplicateTherapy == true)
                                        {
                                            result = true;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MimsInteractionWithLog datainter = PostJson.Data;
                            //lblMimsHtmlResult.Text = datainter.htmlResult;
                            Session[Helper.SessionMimsResultData] = datainter;

                            foreach (Prescription pp in drugdata)
                            {
                                pp.cims_result = "fa fa-check-circle m-safe-icon";
                            }
                        }

                        //drugdata[0].cims_result = "fa fa-warning m-warn-icon-red";

                        //DataTable dta = Helper.ToDataTable(drugdata);
                        //if (flagisadditional == false)
                        //{
                        //    Session[Helper.SessionDrugPres + hfguidadditional.Value] = dta;
                        //    gvw_drug.DataSource = dta;
                        //    gvw_drug.DataBind();
                        //    UpdatePanelListPrescription.Update();
                        //}
                        //else if (flagisadditional == true)
                        //{
                        //    Session[Helper.Sessionadditionalpres + hfguidadditional.Value] = dta;
                        //    gvwAdditionalDrugs.DataSource = dta;
                        //    gvwAdditionalDrugs.DataBind();
                        //    UpdatePanelListPrescriptionAdd.Update();
                        //}

                    }
                    else if (rspn_status == "Fail")
                    {
                        result = false;
                        Session[Helper.SessionMimsResultData] = null;

                        //ShowToastr(rspn_message, rspn_status, "Error");
                        //ScriptManager.RegisterStartupScript(Page, GetType(), "alertmims", "alert('Fail : " + rspn_message + "')", true);
                    }

                    //GET REASON MIMS LOG
                    GetMimsReasonMaster();
                }
                //else
                //{
                //    ShowToastr("Please Add Drugs First", "Warning", "Warning");
                //    //ScriptManager.RegisterStartupScript(Page, GetType(), "alertdrugs", "alert('Please Add Drugs First')", true);
                //}

                string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CheckDrugInteractionFunction", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
            }

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "CheckDrugInteractionFunction", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            throw ex;
        }

        //Log.Info(LogConfig.LogEnd());

        return result;
    }

    protected void BtnCheckDrugInteraction_Click(object sender, EventArgs e)
    {
        //CheckDrugInteraction(false);
    }

    protected void BtnCheckDrugInteraction_Add_Click(object sender, EventArgs e)
    {
        //CheckDrugInteraction(true);
    }

    protected void GetMimsReasonMaster()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            if (Session[CstSession.SessionReasonMasterMims] == null)
            {
                var reason = clsMims.GetReasonMims();
                var JsonReason = JsonConvert.DeserializeObject<ResponseReasonMimsModel>(reason.Result.ToString());
                List<ReasonMimsModel> listReason = JsonReason.Data;

                if (listReason.Count > 0)
                {
                    RepeaterChkReason.DataSource = listReason;
                    RepeaterChkReason.DataBind();
                    Session[CstSession.SessionReasonMasterMims] = listReason;
                }
                else
                {
                    RepeaterChkReason.DataSource = null;
                    RepeaterChkReason.DataBind();
                }
            }
            else
            {
                List<ReasonMimsModel> listReason = (List<ReasonMimsModel>)Session[CstSession.SessionReasonMasterMims];
                RepeaterChkReason.DataSource = listReason;
                RepeaterChkReason.DataBind();
            }

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "", "", "GetMimsReasonMaster", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "", "", "GetMimsReasonMaster", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            throw ex;
        }
    }

    //HEALTH RECORD

    public void InitSOAPMedicationAllergy(PatientHealthInfo dataHI, PatientHealthInfo data)
    {
        int isempty_drug = 0, isempty_food = 0, isempty_other = 0, isempty_routine = 0;

        
        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.drugs && x.is_active == true && x.is_waiting_delete == false).ToList());
        DrugAllergy_HI.DataSource = dataHI.list_info;
        DrugAllergy_HI.DataBind();
        if (dataHI.list_info.Count > 0)
        {
            isempty_drug = 2;
            nodrugs.Style.Add("display", "none");
        }
        else { nodrugs.Style.Add("display", ""); }

        List<InfoHealth> tempdrug = new List<InfoHealth>();
        tempdrug.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.drugs && x.is_active == false && x.is_waiting_delete == false).ToList());
        if (isempty_drug != 2 && tempdrug.Count > 0)
        {
            isempty_drug = 1;
        }
        else
        {
            List<InfoHealth> tempdrug2 = new List<InfoHealth>();
            tempdrug2.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.drugs && x.is_active == true && x.is_waiting_delete == true).ToList());
            if (isempty_drug != 2 && isempty_drug != 1 && tempdrug2.Count > 0)
            {
                //decision value
                isempty_drug = 1; //1=No, 0=Uncentang
            }
        }

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.foods && x.is_active == true && x.is_waiting_delete == false).ToList());
        FoodAllergy_HI.DataSource = dataHI.list_info;
        FoodAllergy_HI.DataBind();
        if (dataHI.list_info.Count > 0)
        {
            isempty_food = 2;
            nofood.Style.Add("display", "none");
        }
        else { nofood.Style.Add("display", ""); }

        List<InfoHealth> tempfood = new List<InfoHealth>();
        tempfood.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.foods && x.is_active == false && x.is_waiting_delete == false).ToList());
        if (isempty_food != 2 && tempfood.Count > 0)
        {
            isempty_food = 1;
        }
        else
        {
            List<InfoHealth> tempfood2 = new List<InfoHealth>();
            tempfood2.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.foods && x.is_active == true && x.is_waiting_delete == true).ToList());
            if (isempty_food != 2 && isempty_food != 1 && tempfood2.Count > 0)
            {
                isempty_food = 1;
            }
        }

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.others && x.is_active == true && x.is_waiting_delete == false).ToList());
        OtherAllergy_HI.DataSource = dataHI.list_info;
        OtherAllergy_HI.DataBind();
        if (dataHI.list_info.Count > 0)
        {
            isempty_other = 2;
            noother.Style.Add("display", "none");
        }
        else { noother.Style.Add("display", ""); }

        List<InfoHealth> tempother = new List<InfoHealth>();
        tempother.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.others && x.is_active == false && x.is_waiting_delete == false).ToList());
        if (isempty_other != 2 && tempother.Count > 0)
        {
            isempty_other = 1;
        }
        else
        {
            List<InfoHealth> tempother2 = new List<InfoHealth>();
            tempother2.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.others && x.is_active == true && x.is_waiting_delete == true).ToList());
            if (isempty_other != 2 && isempty_other != 1 && tempother2.Count > 0)
            {
                isempty_other = 1;
            }
        }

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.medications && x.is_active == true && x.is_waiting_delete == false).ToList());
        RepCurrentMedication_HI.DataSource = dataHI.list_info;
        RepCurrentMedication_HI.DataBind();
        if (dataHI.list_info.Count > 0)
        {
            isempty_routine = 2;
            noroutine.Style.Add("display", "none");
        }
        else { noroutine.Style.Add("display", ""); }

        List<InfoHealth> temproutine = new List<InfoHealth>();
        temproutine.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.medications && x.is_active == false && x.is_waiting_delete == false).ToList());
        if (isempty_routine != 2 && temproutine.Count > 0)
        {
            isempty_routine = 1;
        }
        else
        {
            List<InfoHealth> temproutine2 = new List<InfoHealth>();
            temproutine2.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.medications && x.is_active == true && x.is_waiting_delete == true).ToList());
            if (isempty_routine != 2 && isempty_routine != 1 && temproutine2.Count > 0)
            {
                isempty_routine = 1;
            }
        }

        if (isempty_drug == 0 && isempty_food == 0 && isempty_other == 0)
        {
            rballergy1.Checked = false;
            rballergy2.Checked = false;
        }
        else if (isempty_drug == 1 && isempty_food == 1 && isempty_other == 1)
        {
            rballergy2.Checked = false;
            rballergy1.Checked = true;
        }
        else if (isempty_drug == 2 || isempty_food == 2 || isempty_other == 2)
        {
            rballergy1.Checked = false;
            rballergy2.Checked = true;
        }

        if (isempty_routine == 0)
        {
            rbpengobatan1.Checked = false;
            rbpengobatan2.Checked = false;
        }
        else if (isempty_routine == 1)
        {
            rbpengobatan2.Checked = false;
            rbpengobatan1.Checked = true;
        }
        else
        {
            rbpengobatan1.Checked = false;
            rbpengobatan2.Checked = true;
        }
    }

    public void UpdateMedication_HI(List<InfoHealth> medication)
    {
        try
        {
            //log.Info(LogLibrary.Logging("S", "UpdateMedication_HI", Helper.GetLoginUser(this.Parent.Page), ""));

            RepCurrentMedication_HI.DataSource = medication;
            RepCurrentMedication_HI.DataBind();
            if (medication.Count > 0) { noroutine.Style.Add("display", "none"); } else { noroutine.Style.Add("display", ""); }

            UpdatePanelRoutinePlanning.Update();

            //log.Info(LogLibrary.Logging("E", "UpdateMedication_HI", Helper.GetLoginUser(this.Parent.Page), "Finish UpdateMedication_HI"));
        }
        catch (Exception ex)
        {
            //log.Error(LogLibrary.Error("UpdateMedication_HI", Helper.GetLoginUser(this.Parent.Page), ex.InnerException.Message));
        }
    }

    public void UpdateDrugAllergy_HI(List<InfoHealth> alldrug)
    {
        try
        {
            //log.Info(LogLibrary.Logging("S", "UpdateDrugAllergy_HI", Helper.GetLoginUser(this.Parent.Page), ""));

            DrugAllergy_HI.DataSource = alldrug;
            DrugAllergy_HI.DataBind();
            if (alldrug.Count > 0) { nodrugs.Style.Add("display", "none"); } else { nodrugs.Style.Add("display", ""); }

            UpdatePanelAllergyPlanning.Update();

            //log.Info(LogLibrary.Logging("E", "UpdateDrugAllergy_HI", Helper.GetLoginUser(this.Parent.Page), "Finish UpdateDrugAllergy_HI"));
        }
        catch (Exception ex)
        {
            //log.Error(LogLibrary.Error("UpdateDrugAllergy_HI", Helper.GetLoginUser(this.Parent.Page), ex.InnerException.Message));
        }
    }

    public void UpdateFoodAllergy_HI(List<InfoHealth> allfood)
    {
        try
        {
            //log.Info(LogLibrary.Logging("S", "UpdateFoodAllergy_HI", Helper.GetLoginUser(this.Parent.Page), ""));

            FoodAllergy_HI.DataSource = allfood;
            FoodAllergy_HI.DataBind();
            if (allfood.Count > 0) { nofood.Style.Add("display", "none"); } else { nofood.Style.Add("display", ""); }

            UpdatePanelAllergyPlanning.Update();

            //log.Info(LogLibrary.Logging("E", "UpdateFoodAllergy_HI", Helper.GetLoginUser(this.Parent.Page), "Finish UpdateFoodAllergy_HI"));
        }
        catch (Exception ex)
        {
            //log.Error(LogLibrary.Error("UpdateFoodAllergy_HI", Helper.GetLoginUser(this.Parent.Page), ex.InnerException.Message));
        }
    }

    public void UpdateOtherAllergy_HI(List<InfoHealth> allother)
    {
        try
        {
            //log.Info(LogLibrary.Logging("S", "UpdateOtherAllergy_HI", Helper.GetLoginUser(this.Parent.Page), ""));

            OtherAllergy_HI.DataSource = allother;
            OtherAllergy_HI.DataBind();
            if (allother.Count > 0) { noother.Style.Add("display", "none"); } else { noother.Style.Add("display", ""); }

            UpdatePanelAllergyPlanning.Update();

            //log.Info(LogLibrary.Logging("E", "UpdateOtherAllergy_HI", Helper.GetLoginUser(this.Parent.Page), "Finish UpdateOtherAllergy_HI"));
        }
        catch (Exception ex)
        {
            //log.Error(LogLibrary.Error("UpdateOtherAllergy_HI", Helper.GetLoginUser(this.Parent.Page), ex.InnerException.Message));
        }
    }


    public void UpdateSOAPMedicationAllergy(PatientHealthInfo data)
    {
        int isempty_drug = 1, isempty_food = 1, isempty_other = 1, isempty_routine = 1;

        PatientHealthInfo dataHI = new PatientHealthInfo();

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.drugs && x.is_active == true && x.is_waiting_delete == false).ToList());
        DrugAllergy_HI.DataSource = dataHI.list_info;
        DrugAllergy_HI.DataBind();
        if (dataHI.list_info.Count > 0)
        {
            isempty_drug = 2;
            nodrugs.Style.Add("display", "none");
        }
        else { nodrugs.Style.Add("display", ""); }

        List<InfoHealth> tempdrug = new List<InfoHealth>();
        tempdrug.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.drugs && x.is_active == false && x.is_waiting_delete == false).ToList());
        if (isempty_drug != 2 && tempdrug.Count > 0)
        {
            isempty_drug = 1;
        }
        else
        {
            List<InfoHealth> tempdrug2 = new List<InfoHealth>();
            tempdrug2.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.drugs && x.is_active == true && x.is_waiting_delete == true).ToList());
            if (isempty_drug != 2 && isempty_drug != 1 && tempdrug2.Count > 0)
            {
                //decision value
                isempty_drug = 1; //1=No, 0=Uncentang
            }
        }

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.foods && x.is_active == true && x.is_waiting_delete == false).ToList());
        FoodAllergy_HI.DataSource = dataHI.list_info;
        FoodAllergy_HI.DataBind();
        if (dataHI.list_info.Count > 0)
        {
            isempty_food = 2;
            nofood.Style.Add("display", "none");
        }
        else { nofood.Style.Add("display", ""); }

        List<InfoHealth> tempfood = new List<InfoHealth>();
        tempfood.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.foods && x.is_active == false && x.is_waiting_delete == false).ToList());
        if (isempty_food != 2 && tempfood.Count > 0)
        {
            isempty_food = 1;
        }
        else
        {
            List<InfoHealth> tempfood2 = new List<InfoHealth>();
            tempfood2.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.foods && x.is_active == true && x.is_waiting_delete == true).ToList());
            if (isempty_food != 2 && isempty_food != 1 && tempfood2.Count > 0)
            {
                isempty_food = 1;
            }
        }

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.others && x.is_active == true && x.is_waiting_delete == false).ToList());
        OtherAllergy_HI.DataSource = dataHI.list_info;
        OtherAllergy_HI.DataBind();
        if (dataHI.list_info.Count > 0)
        {
            isempty_other = 2;
            noother.Style.Add("display", "none");
        }
        else { noother.Style.Add("display", ""); }

        List<InfoHealth> tempother = new List<InfoHealth>();
        tempother.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.others && x.is_active == false && x.is_waiting_delete == false).ToList());
        if (isempty_other != 2 && tempother.Count > 0)
        {
            isempty_other = 1;
        }
        else
        {
            List<InfoHealth> tempother2 = new List<InfoHealth>();
            tempother2.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.others && x.is_active == true && x.is_waiting_delete == true).ToList());
            if (isempty_other != 2 && isempty_other != 1 && tempother2.Count > 0)
            {
                isempty_other = 1;
            }
        }

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.medications && x.is_active == true && x.is_waiting_delete == false).ToList());
        RepCurrentMedication_HI.DataSource = dataHI.list_info;
        RepCurrentMedication_HI.DataBind();
        if (dataHI.list_info.Count > 0)
        {
            isempty_routine = 2;
            noroutine.Style.Add("display", "none");
        }
        else { noroutine.Style.Add("display", ""); }

        List<InfoHealth> temproutine = new List<InfoHealth>();
        temproutine.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.medications && x.is_active == false && x.is_waiting_delete == false).ToList());
        if (isempty_routine != 2 && temproutine.Count > 0)
        {
            isempty_routine = 1;
        }
        else
        {
            List<InfoHealth> temproutine2 = new List<InfoHealth>();
            temproutine2.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.medications && x.is_active == true && x.is_waiting_delete == true).ToList());
            if (isempty_routine != 2 && isempty_routine != 1 && temproutine2.Count > 0)
            {
                isempty_routine = 1;
            }
        }

        if (isempty_drug == 0 && isempty_food == 0 && isempty_other == 0)
        {
            rballergy1.Checked = false;
            rballergy2.Checked = false;
        }
        else if (isempty_drug == 1 && isempty_food == 1 && isempty_other == 1)
        {
            if (HF_ForceNo_MA_Allergy.Value != "true" && HF_ForceNo_MA_Routine.Value != "true")
            {
                rballergy2.Checked = false;
                rballergy1.Checked = true;
            }
        }
        else if (isempty_drug == 2 || isempty_food == 2 || isempty_other == 2)
        {
            rballergy1.Checked = false;
            rballergy2.Checked = true;
        }

        if (isempty_routine == 0)
        {
            rbpengobatan1.Checked = false;
            rbpengobatan2.Checked = false;
        }
        else if (isempty_routine == 1)
        {
            if (HF_ForceNo_MA_Allergy.Value != "true" && HF_ForceNo_MA_Routine.Value != "true")
            {
                rbpengobatan2.Checked = false;
                rbpengobatan1.Checked = true;
            }
        }
        else
        {
            rbpengobatan1.Checked = false;
            rbpengobatan2.Checked = true;
        }

        HF_ForceNo_MA_Allergy.Value = "";
        HF_ForceNo_MA_Routine.Value = "";

        UpdatePanelAllergyPlanning.Update();
        UpdatePanelRoutinePlanning.Update();
    }


    #region Diagnostic dan Procedure

    // Search Ajax Diagnostic and Procedure Non Modal event
    protected void ButtonAjaxSearchDiagProcNonModal_Click(object sender, EventArgs e)
    {
        // set start action time
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            // cast sender
            Button btn = (Button)sender;
            // prepare session Diagnostic and procedure 
            List<ProcedureDiagnosis> sessionData = (List<ProcedureDiagnosis>)Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value];
            // define component based on button id click
            string grvLabelControlName = string.Empty, grvHiddenItemId = string.Empty;
            GridView gridview = null;
            Label labelEmpty = null;
            Button btnReset = null;
            TextBox txtItem = null;
            Repeater rptExist = rptExistDiagProc;
            UpdatePanel updatePanelExist = updatepanelexistdiagproc, updatePanelDiv = null, updatePanelSearch = null;

            // set hidden field is future order or not
            HiddenField hiddenField = null;
            int procedureItemTypeId = 0;
            bool isFutureOrder = false;
            DateTime? futureOrderDate = null;

            if (btn.ID == "ButtonAjaxSearchDiagnosticNonModal")
            {
                // set label
                grvLabelControlName = "lblItemDiagnosticName";
                grvHiddenItemId = "hf_id_submitdiagnostic";
                // set component
                gridview = GridView_DiagnosticList;
                labelEmpty = labempty_Diagnostic;
                btnReset = btnResetDiagnostic;
                txtItem = txtItem_DIAG;
                updatePanelDiv = UpdatePanelDivDiagnostic;
                updatePanelSearch = UpdatePanelSearchBoxDiagnostic;
                hiddenField = HF_ItemSelectedDiagnosticNonModal;
                procedureItemTypeId = 4;
                futureOrderDate = DateTime.Now;
            }
            else if (btn.ID == "ButtonAjaxSearchDiagnosticFutureOrderNonModal")
            {
                // set label
                grvLabelControlName = "lblItemDiagnosticFutureOrderName";
                grvHiddenItemId = "hf_id_submitdiagnostic_futureorder";
                // set component
                gridview = GridView_DiagnosticList_FutureOrder;
                labelEmpty = labempty_Diagnostic_FutureOrder;
                btnReset = btnResetDiagnosticFutureOrder;
                txtItem = txtItem_DIAG_FutureOrder;
                updatePanelDiv = UpdatePanelDivDiagnosticFutureOrder;
                updatePanelSearch = UpdatePanelSearchBoxDiagnosticFutureOrder;
                hiddenField = HF_ItemSelectedDiagFutureOrderNonModal;
                procedureItemTypeId = 4;
                isFutureOrder = true;
                if (dp_diag.Text == string.Empty)
                {
                    futureOrderDate = DateTime.Now;
                }
                else { futureOrderDate = DateTime.Parse(dp_diag.Text); }

            }
            else if (btn.ID == "ButtonAjaxSearchProcedureNonModal")
            {
                // set label
                grvLabelControlName = "lblItemProcedureName";
                grvHiddenItemId = "hf_id_submitprocedure";
                // set component
                gridview = GridView_ProcedureList;
                labelEmpty = labempty_Procedure;
                btnReset = btnResetProcedure;
                txtItem = txtItem_PROC;
                updatePanelDiv = UpdatePanelDivProcedure;
                updatePanelSearch = UpdatePanelSearchBoxProcedure;
                hiddenField = HF_ItemSelectedProcedureNonModal;
                procedureItemTypeId = 5;
                futureOrderDate = DateTime.Now;
            }
            else if (btn.ID == "ButtonAjaxSearchProcedureFutureOrderNonModal")
            {
                //set label
                grvLabelControlName = "lblItemProcedureFutureOrderName";
                grvHiddenItemId = "hf_id_submitprocedure_futureorder";
                // set component
                gridview = GridView_ProcedureList_FutureOrder;
                labelEmpty = labempty_Procedure_FutureOrder;
                btnReset = btnResetProcedureFutureOrder;
                txtItem = txtItem_PROC_FutureOrder;
                updatePanelDiv = UpdatePanelDivProcedureFutureOrder;
                updatePanelSearch = UpdatePanelSearchBoxProcedureFutureOrder;
                hiddenField = HF_ItemSelectedProcedureFutureOrderNonModal;
                procedureItemTypeId = 5;
                isFutureOrder = true;
                if (dp_proc.Text == string.Empty)
                {
                    futureOrderDate = DateTime.Now;
                }
                else { futureOrderDate = DateTime.Parse(dp_proc.Text); }
            }

            // get and set data selected
            DataTable DiagProcSelect;
            DiagProcSelect = ((DataTable)Session[Helper.SessionItemDiagProc]).Select("SalesItemId = '" + hiddenField.Value + "'").CopyToDataTable();
            // make sure selected item not empty
            if (DiagProcSelect != null)
            {
                // create new list model
                ProcedureDiagnosis dataModel = new ProcedureDiagnosis
                {
                    EncounterProcedureId = Guid.Empty,
                    ProcedureItemId = long.Parse(DiagProcSelect.Rows[0]["SalesItemId"].ToString()),
                    ProcedureItemName = DiagProcSelect.Rows[0]["SalesItemName"].ToString(),
                    ProcedureItemTypeId = long.Parse(DiagProcSelect.Rows[0]["SalesItemTypeId"].ToString()),
                    IsFutureOrder = isFutureOrder,
                    FutureOrderDate = futureOrderDate
                };

                // make sure session is set
                if (sessionData == null) { sessionData = new List<ProcedureDiagnosis>(); }

                /// Update Date :  25 July 20222,
                /// Item on Grid can added multiple Item
                /// so, below function remark
                // set new datasource, and check if data has added before
                //if (Helper.ToDataTable(sessionData
                //    .Where(c => c.ProcedureItemId.Equals(dataModel.ProcedureItemId) && c.IsFutureOrder == dataModel.IsFutureOrder).ToList()).Rows.Count > 0)
                //{
                //    // show information to view
                //    List<ProcedureDiagnosis> dataExistsList = new List<ProcedureDiagnosis>() { dataModel };
                //    var dsExist = Helper.ToDataTable(dataExistsList);
                //    txtItem.Text = string.Empty;
                //    rptExist.DataSource = dsExist;
                //    rptExist.DataBind();
                //    updatePanelExist.Update();
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modaldiagnostic", "$('#modaldiagnostic').modal();", true);
                //    return;
                //}

                // add data to session on every select 
                sessionData.Add(dataModel);
                // filter data
                var ds = Helper.ToDataTable(sessionData.Where
                    (c => c.ProcedureItemTypeId.Equals(procedureItemTypeId) && c.IsFutureOrder == isFutureOrder).ToList());

                // check gridview diagnostic list is set or not
                if (gridview.Rows.Count.Equals(0))
                {
                    gridview.DataSource = ds;
                    gridview.DataBind();
                    labelEmpty.Style.Add("display", "none");
                    // Show reset label
                    btnReset.Visible = true;
                }
                else // append to gridview if data has been set before
                {
                    gridview.DataSource = ds;
                    gridview.DataBind();
                }

                // add data to session
                Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value] = sessionData;

                // clear text item and update data to ui
                txtItem.Text = string.Empty;
                updatePanelDiv.Update();
                updatePanelSearch.Update();
            }
        }
        catch (Exception ex)
        {
            // set error action time
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonAjaxSearchDiagProcNonModal_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
        }
    }

    // Reet diagnostic list and procedure list
    protected void btnResetDiagProc_Click(object sender, EventArgs e)
    {
        // set start action time
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            // get data
            List<ProcedureDiagnosis> sessionData = (List<ProcedureDiagnosis>)Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value];

            // get button sender
            Button btnReset = (Button)sender;

            // define control 
            GridView gridview = null;
            Label labelEmpty = null;
            UpdatePanel updatePanel = null;

            // set hidden field is future order or not
            HiddenField hiddenField = null;
            int procedureItemTypeId = 0;
            bool isFutureOrder = false;

            switch (btnReset.ID)
            {
                case "btnResetDiagnostic":
                    gridview = GridView_DiagnosticList;
                    labelEmpty = labempty_Diagnostic;
                    updatePanel = UpdatePanelDivDiagnostic;
                    hiddenField = HF_ItemSelectedDiagnosticNonModal;
                    procedureItemTypeId = 4;
                    break;
                case "btnResetDiagnosticFutureOrder":
                    gridview = GridView_DiagnosticList_FutureOrder;
                    labelEmpty = labempty_Diagnostic_FutureOrder;
                    updatePanel = UpdatePanelDivDiagnosticFutureOrder;
                    hiddenField = HF_ItemSelectedDiagFutureOrderNonModal;
                    procedureItemTypeId = 4;
                    isFutureOrder = true;
                    break;
                case "btnResetProcedure":
                    gridview = GridView_ProcedureList;
                    labelEmpty = labempty_Procedure;
                    updatePanel = UpdatePanelDivProcedure;
                    hiddenField = HF_ItemSelectedProcedureNonModal;
                    procedureItemTypeId = 5;
                    break;
                case "btnResetProcedureFutureOrder":
                    gridview = GridView_ProcedureList_FutureOrder;
                    labelEmpty = labempty_Procedure_FutureOrder;
                    updatePanel = UpdatePanelDivProcedureFutureOrder;
                    hiddenField = HF_ItemSelectedProcedureFutureOrderNonModal;
                    procedureItemTypeId = 5;
                    isFutureOrder = true;
                    break;
                default:
                    break;
            }

            // remove from session data
            var dataAfterRemove = sessionData.Except(sessionData.Where(c => c.ProcedureItemTypeId == procedureItemTypeId && c.IsFutureOrder == isFutureOrder).ToList()).ToList();
            // update session dataset
            Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value] = dataAfterRemove;

            // reset gridview
            gridview.DataSource = null;
            gridview.DataBind();
            labelEmpty.Style.Add("display", "");
            updatePanel.Update();
            // Hide reset label
            btnReset.Visible = false;
        }
        catch (Exception ex)
        {
            // set error action time
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnResetDiagProc_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
        }
    }

    // delete item diagnostic and procedure on list
    protected void btndeletediagproc_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            ImageButton btn = (ImageButton)sender;
            // get row position
            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

            // define component based on button id click
            string grvLabelControlName = string.Empty, grvHiddenItemId = string.Empty;
            GridView gridview = null;
            Label labelEmpty = null;
            Repeater rptExist = rptExistDiagProc;
            UpdatePanel updatePanelExist = updatepanelexistdiagproc, updatePanelDiv = null, updatePanelSearch = null;
            Button btnReset = null;
            string lab_id = string.Empty;
            bool isFutureOrder = false;
            int procedureItemTypeId = 0;
            TextBox dateTextbox = null;

            if (btn.ID == "btndeletediag")
            {
                // set label
                grvLabelControlName = "lblItemDiagnosticName";
                grvHiddenItemId = "hf_id_submitdiagnostic";
                // set component
                gridview = GridView_DiagnosticList;
                lab_id = gridview.Rows[selRowIndex].FindControl(grvHiddenItemId).ToString();
                updatePanelDiv = UpdatePanelDivDiagnostic;
                updatePanelSearch = UpdatePanelSearchBoxDiagnostic;
                labelEmpty = labempty_Diagnostic;
                procedureItemTypeId = 4;
                btnReset = btnResetDiagnostic;
            }
            else if (btn.ID == "btndeletediag_FutureOrder")
            {
                // set label
                grvLabelControlName = "lblItemDiagnosticFutureOrderName";
                grvHiddenItemId = "hf_id_submitdiagnostic_futureorder";
                // set component
                gridview = GridView_DiagnosticList_FutureOrder;
                lab_id = gridview.Rows[selRowIndex].FindControl(grvHiddenItemId).ToString();
                updatePanelDiv = UpdatePanelDivDiagnosticFutureOrder;
                updatePanelSearch = UpdatePanelSearchBoxDiagnosticFutureOrder;
                labelEmpty = labempty_Diagnostic_FutureOrder;
                btnReset = btnResetDiagnosticFutureOrder;
                procedureItemTypeId = 4;
                isFutureOrder = true;
                dateTextbox = dp_diag;
            }
            else if (btn.ID == "btndeleteproc")
            {
                // set label
                grvLabelControlName = "lblItemProcedureName";
                grvHiddenItemId = "hf_id_submitprocedure";
                // set component
                gridview = GridView_ProcedureList;
                lab_id = gridview.Rows[selRowIndex].FindControl(grvHiddenItemId).ToString();
               
                updatePanelDiv = UpdatePanelDivProcedure;
                updatePanelSearch = UpdatePanelSearchBoxProcedure;
                labelEmpty = labempty_Procedure;
                procedureItemTypeId = 5;
                btnReset = btnResetProcedure;
            }
            
            else if (btn.ID == "btndeleteproc_FutureOrder")
            {
                //set label
                grvLabelControlName = "lblItemProcedureFutureOrderName";
                grvHiddenItemId = "hf_id_submitprocedure_futureorder";
                // set component
                gridview = GridView_ProcedureList_FutureOrder;
                lab_id = gridview.Rows[selRowIndex].FindControl(grvHiddenItemId).ToString();
                updatePanelDiv = UpdatePanelDivProcedureFutureOrder;
                updatePanelSearch = UpdatePanelSearchBoxProcedureFutureOrder;
                labelEmpty = labempty_Procedure_FutureOrder;
                btnReset = btnResetProcedureFutureOrder;
                procedureItemTypeId = 5;
                isFutureOrder = true;
                dateTextbox = dp_proc;
            }

            // remove and rebind item to grid
            var row = gridview.Rows[selRowIndex];
            ProcedureDiagnosis prdExisting = new ProcedureDiagnosis()
            {
                EncounterProcedureId = Guid.Empty,
                ProcedureItemId = Int32.Parse(((HiddenField)row.Cells[1].FindControl(grvHiddenItemId)).Value),
                ProcedureItemName = ((Label)row.Cells[1].FindControl(grvLabelControlName)).Text,
                IsFutureOrder = isFutureOrder,
                FutureOrderDate = DateTime.Now,
            };

            // remove item based on user click
            List<ProcedureDiagnosis> sessionData = (List<ProcedureDiagnosis>)Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value];
            var dataAfterRemove =  sessionData.Except(sessionData
                .Where(c => c.ProcedureItemId == prdExisting.ProcedureItemId && c.IsFutureOrder == prdExisting.IsFutureOrder).Take(1).ToList()).ToList();
            // filter for existing data based on user procedureitemtypeid
            var tempDataExisting = dataAfterRemove.Where(c => c.ProcedureItemTypeId == procedureItemTypeId && c.IsFutureOrder == isFutureOrder).ToList();
            // check if grid is empty or not, if empth show label empty
            if (tempDataExisting.Count() == 0)
            {
                // show empty pic
                labelEmpty.Style.Add("display", "");
                // hide button reset
                btnReset.Visible = false;
                // clear date
                if (dateTextbox != null)
                {
                    dateTextbox.Text = string.Empty;
                }
            }
            // update data to session and update gridview for submit
            Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value] = dataAfterRemove;
            
            gridview.DataSource = Helper.ToDataTable(tempDataExisting);
            gridview.DataBind();
            // update panel
            updatePanelDiv.Update();
            updatePanelSearch.Update();

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btndeletediagproc_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
        }

    }

    /// <summary>
    /// Set Diagnostic and procedure on load
    /// </summary>
    private void SetDiagProc()
    {
        // set start action time
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            // get existing data
            List<ProcedureDiagnosis> sessionData = (List<ProcedureDiagnosis>)Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value];

            // set update panel
            UpdatePanel updatePanelExist = updatepanelexistdiagproc;
            UpdatePanel updatePanelDivDiagnostic = UpdatePanelDivDiagnostic, updatePanelDivDiagnosticFutureOrder = UpdatePanelDivDiagnosticFutureOrder;
            UpdatePanel updatePanelDivProcedure = UpdatePanelDivProcedure, updatePanelDivProcedureFutureOrder = UpdatePanelDivProcedureFutureOrder;
            UpdatePanel updatePanelSearchDiagnostic = UpdatePanelSearchBoxDiagnostic, updatePanelSearchDiagnosticFutureOrder = UpdatePanelSearchBoxDiagnosticFutureOrder;
            UpdatePanel updatePanelSearchProcedure = UpdatePanelSearchBoxProcedure, updatePanelSearchProcedureFutureOrder = UpdatePanelSearchBoxProcedureFutureOrder;

            // bind data to gridview
            GridView_DiagnosticList.DataSource = Helper.ToDataTable(sessionData.Where(i => i.ProcedureItemTypeId == 4 && i.IsFutureOrder == false).ToList());
            GridView_DiagnosticList.DataBind();
            GridView_ProcedureList.DataSource = Helper.ToDataTable(sessionData.Where(i => i.ProcedureItemTypeId == 5 && i.IsFutureOrder == false).ToList());
            GridView_ProcedureList.DataBind();

            // Future order bind
            if (sessionData.Where(i => i.ProcedureItemTypeId == 4 && i.IsFutureOrder == true).ToList().Count > 0)
            {
                var tempList = sessionData.Where(i => i.ProcedureItemTypeId == 4 && i.IsFutureOrder == true).ToList();
                dp_diag.Text = tempList[0].FutureOrderDate.HasValue ? ((DateTime)tempList[0].FutureOrderDate).ToString("dd MMM yyyy") : string.Empty;
                GridView_DiagnosticList_FutureOrder.DataSource = Helper.ToDataTable(tempList);
                GridView_DiagnosticList_FutureOrder.DataBind();

            }
            if (sessionData.Where(i => i.ProcedureItemTypeId == 5 && i.IsFutureOrder == true).ToList().Count > 0)
            {
                var tempList = sessionData.Where(i => i.ProcedureItemTypeId == 5 && i.IsFutureOrder == true).ToList();
                dp_proc.Text = tempList[0].FutureOrderDate.HasValue ? ((DateTime)tempList[0].FutureOrderDate).ToString("dd MMM yyyy") : string.Empty;
                GridView_ProcedureList_FutureOrder.DataSource = Helper.ToDataTable(tempList);
                GridView_ProcedureList_FutureOrder.DataBind();
            }

            // show reset button 
            btnResetDiagnostic.Visible = GridView_DiagnosticList.Rows.Count > 0 ? true : false;
            btnResetDiagnosticFutureOrder.Visible = GridView_DiagnosticList_FutureOrder.Rows.Count > 0 ? true : false;
            btnResetProcedure.Visible = GridView_ProcedureList.Rows.Count > 0 ? true : false;
            btnResetProcedureFutureOrder.Visible = GridView_ProcedureList_FutureOrder.Rows.Count > 0 ? true : false;

            // set label empty
            if (GridView_DiagnosticList.Rows.Count > 0) { labempty_Diagnostic.Style.Add("display", "none"); }
            if (GridView_DiagnosticList_FutureOrder.Rows.Count > 0) { labempty_Diagnostic_FutureOrder.Style.Add("display", "none"); }
            if (GridView_ProcedureList.Rows.Count > 0) { labempty_Procedure.Style.Add("display", "none"); }
            if (GridView_ProcedureList_FutureOrder.Rows.Count > 0) { labempty_Procedure_FutureOrder.Style.Add("display", "none"); }

            // set future order button
            if (sessionData.Where(i => i.ProcedureItemTypeId == 4 && i.IsFutureOrder == true).ToList().Count > 0 ||
                sessionData.Where(i => i.ProcedureItemTypeId == 5 && i.IsFutureOrder == true).ToList().Count > 0)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "CollapseDiagProc", "document.getElementById('MainContent_StdPlanning_divFutureOrderDiagProc').style.display='block';", true); }

            // update all panel diagnostic and procedure
            updatePanelDivDiagnostic.Update();
            updatePanelDivDiagnosticFutureOrder.Update();
            updatePanelDivProcedure.Update();
            updatePanelDivProcedureFutureOrder.Update();
            updatePanelSearchDiagnostic.Update();
            updatePanelSearchDiagnosticFutureOrder.Update();
            updatePanelSearchProcedure.Update();
            updatePanelSearchProcedureFutureOrder.Update();
            updatePanelExist.Update();
        }
        catch (Exception ex)
        {
            // set error action time
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "SetDiagProc", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
        }
    }

	#endregion

	

}
