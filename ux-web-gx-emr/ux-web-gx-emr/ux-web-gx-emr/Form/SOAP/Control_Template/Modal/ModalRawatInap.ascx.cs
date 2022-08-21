using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static PatientHistory;
using static PatientReferralModel;

public partial class Form_SOAP_Control_Template_Modal_ModalRawatInap : System.Web.UI.UserControl
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    List<CpoeTrans> listChecked = new List<CpoeTrans>();
    List<OperationProcedure> listProcedureInpatientChecked = new List<OperationProcedure>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfPatientId.Value = Request.QueryString["idPatient"];
            hfEncounterId.Value = Request.QueryString["EncounterId"];
            var varResult = clsCommon.GetPatientHeader(long.Parse(hfPatientId.Value), hfEncounterId.Value.ToString());
            var JsongetPatientHistory = JsonConvert.DeserializeObject<ResponsePatientHeader>(varResult.Result.ToString());
            PatientHeader header = JsongetPatientHistory.Data;
            //PatientCard.initializevalue(header);
            var adm = header.AdmissionNo;
            
            HiddenField hfstatusId = (HiddenField)FindControl("hfstatusId");

            var hfstatudid = hfstatusId.Value;
            textbox_dokter.Text = header.DoctorName;
            GetAnestheticData();
            GetWardData();
            GetRecoveryRoom();
        }

        if (chck_BangsalLain.Checked)
        {
            cbl_ward.Enabled = false;
        }
        else
        {
            cbl_ward.Enabled = true;
        }

        for (int i = 0; i < cbl_ward.Items.Count; i++)
        {
            cbl_ward.Items[i].Attributes.Add("onclick", "MutExChkList(this)");
        }
        for (int i = 0; i < cbl_recoveryroom.Items.Count; i++)
        {
            cbl_recoveryroom.Items[i].Attributes.Add("onclick", "MutExChkListRecovery(this)");
        }
    }

    public void GetWardData()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            var getward = clsSOAP.getWardByOrg(Helper.organizationId);
            var jsongetward = JsonConvert.DeserializeObject<ResultWard>(getward.Result.ToString());
            List<Ward> warddata = jsongetward.data;
            DataTable dtward = Helper.ToDataTable(warddata);
            //rptward.DataSource = dtward;
            //rptward.DataBind();
            //cblist
            cbl_ward.DataSource = warddata;
            cbl_ward.DataValueField = "wardId";
            cbl_ward.DataTextField = "wardName";
            cbl_ward.DataBind();
        }
        catch( Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetWardData", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));


        }
        
    }

    public void GetRecoveryRoom()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            var getrecoveryroom = clsSOAP.getRecoveryRoomByOrg(Helper.organizationId);
            var jsonrecoveryroom = JsonConvert.DeserializeObject<ResultRecoveryRoom>(getrecoveryroom.Result.ToString());
            List<RecoveryRoom> recoverRoom = jsonrecoveryroom.data;
            cbl_recoveryroom.DataSource = recoverRoom;
            cbl_recoveryroom.DataTextField = "recovery_room_name";
            cbl_recoveryroom.DataValueField = "recovery_room_id";
            cbl_recoveryroom.DataBind();
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetRecoveryRoomData", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
        }

    }
    
    public void GetAnestheticData()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {  
            DataTable anestheticData = (DataTable)Session[Helper.SessionAnestheticInpatientData];
            var anestheticList = Helper.ToDataList<AnestesiData>(anestheticData);

            DropDownList ddl_anasteticmethod = (DropDownList)FindControl("ddl_anasteticmethod");
            foreach (AnestesiData anestesimethod in anestheticList)
            {
                ddl_anasteticmethod.Items.Add(new ListItem(anestesimethod.anesthetia_type_name, anestesimethod.anestesi_id.ToString()));
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetAnestheticData", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetAnestheticData", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
    }

    public void initializevalue(InpatientData inpatientData)
    {
        try 
        {
            if (inpatientData == null)
            {
                InpatientData inpatientDataa = new InpatientData();
            }
            else if (inpatientData.status_id != null)
            {
                HiddenField hf_statusId = (HiddenField)FindControl("hfstatusId");
                HiddenField hf_encounter = (HiddenField)FindControl("hfencounterId");
                HiddenField hf_operationScheduleId = (HiddenField)FindControl("hfoperationScheduleId");
                // HiddenField hf_operationScheduleAdditionalId = (HiddenField)FindControl("hfOperationScheduleAdditionalId");
                HiddenField hf_statusBooking = (HiddenField)FindControl("hfstatusBookingId");

                hf_statusId.Value = inpatientData.status_id.ToString();
                hf_encounter.Value = inpatientData.encounter_id.ToString();
                hf_operationScheduleId.Value = inpatientData.operation_schedule_id.ToString();

                hfOperationScheduleAdditionalId.Value = inpatientData.operation_schedule_additional_id.ToString();

                //hf_statusBooking.Value = inpatientData.operation_schedule_header.status_booking_id; 

                // diagnose di jadikan testing
                textbox_diagnosis.Text = inpatientData.diagnosis;

                string[] separateDate = inpatientData.admission_date.Split(' ');
                string splitDate = separateDate[0];
                string splitTime = separateDate[1];
                string timePM_AM = separateDate[2];
             
                txttglmasukrawat.Text = DateTime.Parse(splitDate.Trim()).ToString("dd MMMM yyyy", CultureInfo.InvariantCulture);
                txtwaktumasukrawat.Text = $"{splitTime.Substring(0, splitTime.Length-3)} {timePM_AM}";
 
                txtinstruksirawatinap.Text = inpatientData.instruction;
                txtremarks.Text = inpatientData.remarks;

                if (inpatientData.ward_id.ToString()=="0")
                {
                    txt_BangsalLain.Enabled = true;
                    chck_BangsalLain.Checked = true;
                    txt_BangsalLain.Text = inpatientData.ward_name;
                }
                else
                {
                    cbl_ward.SelectedValue = inpatientData.ward_id.ToString();
                    cbl_ward.SelectedItem.Text = inpatientData.ward_name;
                }
                
                

                if (inpatientData.estimation_day.ToString() == "<7 hari")
                {
                    chbx_lamarawat_kurangseminggu.Checked = true;
                }
                else if (inpatientData.estimation_day.ToString() == ">7 hari")
                {
                    chbx_lamarawat_kurangseminggu.Checked = true;
                }

                if (inpatientData.operation_schedule_id == Guid.Empty)
                {
                    chbx_tindakanoperasi_tidak.Checked = true;
                    chbx_tindakanoperasi_ya.Checked = false;
                    //divTindakanOperasi.Visible = false;
                    //UPtindakanOperasi.Update();
                }
                else
                {
                    chbx_tindakanoperasi_tidak.Checked = false;
                    chbx_tindakanoperasi_ya.Checked = true;
                    //divTindakanOperasi.Visible = true;
                    //UPtindakanOperasi.Update();
                }
                //txt_tglperkiraanoperasi.Text = DateTime.Parse(inpatientData.admission_date.ToString()).ToString("dd MMMM yyyy");
                txt_tglperkiraanoperasi.Text = inpatientData.admission_date;
                txtwaktuperkiraanoperasi.Text = inpatientData.operation_schedule_header.incision_time;

                int menit = 0;
                int jam = 0;
                //foreach (var a in inpatientData.operation_procedures)
                //{
                //    ddl_namaoperasi.SelectedItem.Text = a.procedure_name;
                //    jam = a.procedure_estimate_time / 60;
                //    menit = a.procedure_estimate_time % 60;
                //    txt_JamLamaOperasi.Text = jam.ToString();
                //    txt_MenitLamaOperasi.Text = menit.ToString();
                //}

                ddl_anasteticmethod.SelectedItem.Text = inpatientData.operation_schedule_header.anesthetia_type_name;

                if (!string.IsNullOrEmpty(inpatientData.tools_detail))
                {
                    txt_alat_ya.Text = inpatientData.tools_detail;
                    txt_alat_ya.Enabled = true;
                    chbx_alat_tidak.Checked = false;
                    chbx_alat_ya.Checked = true;
                }
                else
                {
                    txt_alat_ya.Enabled = false;
                    chbx_alat_ya.Checked = false;
                    chbx_alat_tidak.Checked = true;
                }


                if (inpatientData.category == 1)
                {
                    chck_Tabel1.Checked = true;

                }
                else if (inpatientData.category == 2)
                {
                    chck_Tabel2.Checked = true;
                }
                else if (inpatientData.category == 3)
                {
                    chck_Tabel3.Checked = true;
                }
                else if (inpatientData.category == 4)
                {
                    chck_Tabel4.Checked = true;
                }
                else if (inpatientData.category == 5)
                {
                    chck_Tabel5.Checked = true;
                }
                else if (inpatientData.category == 6)
                {
                    chck_Tabel6.Checked = true;
                }
                else if (inpatientData.category == 7)
                {
                    chck_Tabel7.Checked = true;
                }
                else
                {
                    txt_TabelLain.Text = inpatientData.category_detail;
                    txt_TabelLain.Enabled = true;
                    chck_TabelLain.Checked = true;
                }

                if(inpatientData.operation_schedule_header.recovery_room_id == 0)
                {
                    txt_OperasiTindakanLain.Text = inpatientData.recovery_room;
                    chck_OperasiTindakanLain.Checked = true;
                }
                else
                {
                   cbl_recoveryroom.SelectedValue = inpatientData.operation_schedule_header.recovery_room_id.ToString();
                }
               


                if (inpatientData.fasting_procedure == true)
                {
                    chbx_puasa_tidak.Checked = false;
                    chbx_puasa_ya.Checked = true;
                    txt_puasa_ya.Text = inpatientData.fasting_procedure_time.ToString();
                }
                else
                {
                    chbx_puasa_tidak.Checked = true;
                    chbx_puasa_ya.Checked = false;

                }


                txt_otherLab.Text = inpatientData.other_lab;
                txt_OtherRad.Text = inpatientData.other_rad;

                //labrad

                List<CpoeTrans> listlabstemp = new List<CpoeTrans>();
                List<CpoeTrans> listradtemp = new List<CpoeTrans>();

                if (inpatientData.lab_Rad_Additionals != null)
                {
                    foreach (var a in inpatientData.lab_Rad_Additionals)
                    {

                        if (a.is_rad == true)
                        {
                            CpoeTrans lisrad = new CpoeTrans();
                            lisrad.id = Convert.ToInt64(a.item_id);
                            if(a.is_active == true)
                            {
                                lisrad.isdelete = 0;
                            }
                            else if(a.is_active== false)
                            {
                                lisrad.isdelete = 1;
                            }
                            lisrad.name = a.item_name;
                            lisrad.type = a.item_type;
                            listradtemp.Add(lisrad);
                        }
                        else
                        {
                            CpoeTrans listlab = new CpoeTrans();
                            listlab.id = Convert.ToInt64(a.item_id);
                            listlab.name = a.item_name;
                            if (a.is_active == true)
                            {
                                listlab.isdelete = 0;
                            }
                            else if (a.is_active == false)
                            {
                                listlab.isdelete = 1;
                            }
                            listlab.type = a.item_type;
                            listlabstemp.Add(listlab);
                        }
                    }
                    Session[Helper.Sessionradcheck + hfguidadditional.Value] = listradtemp;
                    Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = listlabstemp;

                    DataTable dtrad = Helper.ToDataTable((List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value]);
                    gv_Rad.DataSource = dtrad;
                    gv_Rad.DataBind();

                    DataTable dtlab = Helper.ToDataTable((List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value]);
                    gv_Lab.DataSource = dtlab;
                    gv_Lab.DataBind();
                }
            }
        } catch(Exception ex)
        {
            var message = ex.Message;
        }
        //divTindakanOperasi.Visible = true;
        //UPtindakanOperasi.Update();

        Up_rawatinap.Update();

    }

    public void BtnAjaxSearchRAD_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            DataTable radSelect = new DataTable();

            radSelect = ((DataTable)Session[Helper.SessionItemAllCPOE]).Select("template_name = 'RAD' AND id = '" + hf_ItemSelectedRad.Value + "' AND name = '" + HF_ItemSelectedRad_name.Value + "' AND remarks = '" + HF_ItemSelectedRad_remarks.Value + "'").CopyToDataTable();

            List<CpoeTrans> listExcludeTrans = new List<CpoeTrans>();
            // List<CpoeTrans> listNotExist = new List<CpoeTrans>();
            List<CpoeTrans> listTempCpoeTrans;

            if (Session[Helper.Sessionradcheck + hfguidadditional.Value] == null)
            {
                listTempCpoeTrans = new List<CpoeTrans>();
            }
            else
            {
                listTempCpoeTrans = new List<CpoeTrans>();
                listTempCpoeTrans = (List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value];
            }

            CpoeTrans ct = new CpoeTrans();
            ct.id = long.Parse(radSelect.Rows[0]["id"].ToString());
            ct.name = radSelect.Rows[0]["name"].ToString();
            ct.type = radSelect.Rows[0]["type"].ToString();
            ct.remarks = radSelect.Rows[0]["remarks"].ToString();
            ct.isnew = int.Parse(radSelect.Rows[0]["isnew"].ToString());
            ct.iscito = int.Parse(radSelect.Rows[0]["iscito"].ToString());
            ct.issubmit = int.Parse(radSelect.Rows[0]["issubmit"].ToString());
            ct.isdelete = int.Parse(radSelect.Rows[0]["isdelete"].ToString());
            ct.ischeck = int.Parse(radSelect.Rows[0]["ischeck"].ToString());
            ct.IsSendHope = int.Parse(radSelect.Rows[0]["IsSendHope"].ToString());

            if (listTempCpoeTrans != null)
            {
                if (listTempCpoeTrans.Any(y => y.name == ct.name && y.isdelete == 0))
                {
                    listExcludeTrans.Add(ct);
                }
                else if (listTempCpoeTrans.Any(y => y.name == ct.name && y.isdelete == 1))
                {
                    listTempCpoeTrans.FirstOrDefault(z => z.name == ct.name).isdelete = 0;
                }
                else
                {
                    listTempCpoeTrans.Add(ct);
                }

                //else if (listTempCpoeTrans.Any(y => ct.ischeck == 0))
                //{
                //    listNotExist.Add(ct);
                //}
            }
            else
            {
                listTempCpoeTrans.Add(ct);
            }

            Session[Helper.Sessionradcheck + hfguidadditional.Value] = listTempCpoeTrans;

            listChecked = (List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value];
            if (listChecked != null)
            {
                if (Helper.ToDataTable(listChecked).Select("isdelete = 0 and ischeck <> 0").Count() > 0)
                {
                    List<CpoeTrans> listcheckedTemp = new List<CpoeTrans>();
                    foreach (var list in listChecked)
                    {
                        CpoeTrans temp = new CpoeTrans();
                        temp.id = list.id;
                        temp.ischeck = list.ischeck;
                        temp.iscito = list.iscito;
                        temp.isdelete = list.isdelete;
                        temp.isnew = list.isnew;
                        temp.issubmit = list.issubmit;

                        if (list.remarks != "")
                        {
                            temp.name = list.name + " (" + list.remarks + ")";
                        }
                        else
                        {
                            temp.name = list.name;
                        }

                        temp.type = temp.type;
                        temp.remarks = list.remarks;
                        temp.IsSendHope = list.IsSendHope;
                        listcheckedTemp.Add(temp);
                    }
                    DataTable dt = Helper.ToDataTable(listcheckedTemp).Select("isdelete = 0 and ischeck <> 0").CopyToDataTable();
                    gv_Rad.DataSource = dt;
                    gv_Rad.DataBind();
                }
                else
                {
                    gv_Rad.DataSource = null;
                    gv_Rad.DataBind();
                }
            }

            if (listExcludeTrans.Count() > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert(' Service Already Exist');", true);
            }

            txt_ItemRad.Text = "";
            up_DivRad.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnAjaxSearch_RadRawatInap_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnAjaxSearch_RadRawatInap_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

    }

    public void BtnAjaxSearchLAB_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            DataTable labSelect;
            labSelect = ((DataTable)Session[Helper.SessionItemAllCPOE]).Select("template_name = 'LAB' AND id = '" + hf_ItemSelectedLab.Value + "' AND type <> 'CitoLab'").CopyToDataTable();

            List<CpoeTrans> listExcludeTrans = new List<CpoeTrans>();
            // List<CpoeTrans> listNotExist = new List<CpoeTrans>();
            List<CpoeTrans> listTempCpoeTrans;

            if (Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] == null)
            {
                listTempCpoeTrans = new List<CpoeTrans>();
            }
            else
            {
                listTempCpoeTrans = new List<CpoeTrans>();
                listTempCpoeTrans = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];
            }

            CpoeTrans x = new CpoeTrans();
            x.id = long.Parse(labSelect.Rows[0]["id"].ToString());
            x.name = labSelect.Rows[0]["name"].ToString();
            x.type = labSelect.Rows[0]["type"].ToString();
            x.remarks = labSelect.Rows[0]["remarks"].ToString();
            x.isnew = int.Parse(labSelect.Rows[0]["isnew"].ToString());
            x.iscito = int.Parse(labSelect.Rows[0]["iscito"].ToString());
            x.issubmit = int.Parse(labSelect.Rows[0]["issubmit"].ToString());
            x.isdelete = int.Parse(labSelect.Rows[0]["isdelete"].ToString());
            x.ischeck = int.Parse(labSelect.Rows[0]["ischeck"].ToString());
            x.IsSendHope = int.Parse(labSelect.Rows[0]["IsSendHope"].ToString());

            if (listTempCpoeTrans != null)
            {
                if (listTempCpoeTrans.Any(y => y.name == x.name && y.isdelete == 0))
                {
                    listExcludeTrans.Add(x);
                }
                else if (listTempCpoeTrans.Any(y => y.name == x.name && y.isdelete == 1))
                {
                    listTempCpoeTrans.FirstOrDefault(z => z.name == x.name).isdelete = 0;
                }
                else
                {
                    listTempCpoeTrans.Add(x);
                }

                //else if (listTempCpoeTrans.Any(y => x.ischeck == 0))
                //{
                //    listNotExist.Add(x);
                //}
            }
            else
            {
                listTempCpoeTrans.Add(x);
            }

            Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = listTempCpoeTrans;

            listChecked = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];
            if (listChecked != null)
            {

                if (Helper.ToDataTable(listChecked).Select("isdelete = 0 and ischeck <> 0").Count() > 0)
                {
                    DataTable dt = Helper.ToDataTable(listChecked).Select("isdelete = 0 and ischeck <> 0").CopyToDataTable();
                    gv_Lab.DataSource = dt;
                    gv_Lab.DataBind();
                }
                else
                {
                    gv_Lab.DataSource = null;
                    gv_Lab.DataBind();
                }
            }

            if (listExcludeTrans.Count() > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert(' Service Already Exist');", true);
            }

            txt_ItemLab.Text = "";
            up_DivLab.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnAjaxSearch_LabRawatInap_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnAjaxSearch_LabRawatInap_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
    }

    public InpatientData getvalues()
    {
        InpatientData data = new InpatientData();

        try
        {
            HiddenField hf_admission = (HiddenField)FindControl("hfAdmissionId");
            HiddenField hf_statusId = (HiddenField)FindControl("hfstatusId");
            HiddenField hf_encounter = (HiddenField)FindControl("hfencounterId");
            HiddenField hf_doctorid = (HiddenField)FindControl("hfDoctorID");
            HiddenField hf_operationScheduleId = (HiddenField)FindControl("hfoperationScheduleId");
            // HiddenField hf_operationScheduleAdditionalId = (HiddenField)FindControl("hfOperationScheduleAdditionalId");

            // DropDownList ddl_namaoperasi = (DropDownList)FindControl("ddl_namaoperasi");

            DropDownList ddl_anasteticmethod = (DropDownList)FindControl("ddl_anasteticmethod");
            
            var statusid = hf_statusId.Value;
            var encounter = hf_encounter.Value;
            var operationalScheduleId = hf_operationScheduleId.Value;
            var operationScheduleIdhf = hf_operationScheduleId.Value;
            var perationScheduleAdditionalId = hfOperationScheduleAdditionalId.Value;
            var newOperationScheduleId = Guid.NewGuid();

            bool procedureOperation_Yes = chbx_tindakanoperasi_ya.Checked;
            bool procedureOperation_NO = chbx_tindakanoperasi_tidak.Checked;


            hfPatientId.Value = Request.QueryString["idPatient"];
            hfEncounterId.Value = Request.QueryString["EncounterId"];

            var varResult = clsCommon.GetPatientHeader(long.Parse(hfPatientId.Value), hfEncounterId.Value.ToString());
            var JsongetPatientHistory = JsonConvert.DeserializeObject<ResponsePatientHeader>(varResult.Result.ToString());
            PatientHeader header = JsongetPatientHistory.Data;


            ScriptManager.RegisterStartupScript(this, GetType(), "showalertqqq", "console('haiiii " + perationScheduleAdditionalId + " ');", true);


            data.status_id = string.IsNullOrEmpty(perationScheduleAdditionalId) || Int32.Parse(perationScheduleAdditionalId) == 0  ? 1 : 2;
            // data.status_id = perationScheduleAdditionalId == null ? 1 : 2;
            data.user_id = MyUser.GetHopeUserID().ToString();
            data.operation_schedule_additional_id = !string.IsNullOrEmpty(perationScheduleAdditionalId) || Int32.Parse(perationScheduleAdditionalId) != 0 ? Int32.Parse(perationScheduleAdditionalId) : 0;
            // data.operation_schedule_additional_id = perationScheduleAdditionalId == null ? Int32.Parse(perationScheduleAdditionalId) : 0;
            //data.operation_schedule_id = operationalScheduleId != null ? Guid.Parse(operationalScheduleId) : newOperationScheduleId;
            data.operation_schedule_id = operationalScheduleId == null ? Guid.Parse(operationalScheduleId) : newOperationScheduleId;
            data.encounter_id = header.EncounterId;
            data.patient_id = hfPatientId.Value;
            data.patientName = header.PatientName;
            
            // LAMA RAWAT
            if (chbx_lamarawat_kurangseminggu.Checked)
            {
                data.estimation_day = "<7 hari";
            }
            else if (chbx_lamarawat_lebihseminggu.Checked)
            {
                data.estimation_day = ">7 hari";
            }
            else
            {
                data.estimation_day = "";
            }
          
            // BANGSAL
            if (chck_BangsalLain.Checked)
            {
                data.ward_name = txt_BangsalLain.Text;
                data.ward_id = "0";
            }
            else if(cbl_ward.SelectedItem != null)
            {
                data.ward_name = cbl_ward.SelectedItem.Text;
                data.ward_id = cbl_ward.SelectedItem.Value;
            }
            else
            {
                data.ward_name = "";
                data.ward_id = "0";
            }
          
            if (chbx_tindakanoperasi_ya.Checked)
            {
               #region Tindakan Operasi 

                #region Alat
                if (chbx_alat_tidak.Checked)
                {
                    data.use_tools = false;
                    data.tools_detail = "";
                }
                else if (chbx_alat_ya.Checked)
                {
                    data.use_tools = true;
                    data.tools_detail = txt_alat_ya.Text;
                }
                else
                {
                    data.tools_detail = "";
                }
                #endregion
                
                #region TabelKategori
                if (chck_Tabel1.Checked)
                {
                    data.category = 1;
                    data.category_detail = " ";
                }
                else if (chck_Tabel2.Checked)
                {
                    data.category = 2;
                    data.category_detail = " ";
                }
                else if (chck_Tabel3.Checked)
                {
                    data.category = 3;
                    data.category_detail = " ";
                }
                else if (chck_Tabel4.Checked)
                {
                    data.category = 4;
                    data.category_detail = " ";
                }
                else if (chck_Tabel5.Checked)
                {
                    data.category = 5;
                    data.category_detail = " ";
                }
                else if (chck_Tabel6.Checked)
                {
                    data.category = 6;
                    data.category_detail = " ";
                }
                else if (chck_Tabel7.Checked)
                {
                    data.category = 7;
                    data.category_detail = " ";
                }
                else if (chck_TabelLain.Checked)
                {
                    data.category_detail = txt_TabelLain.Text;
                    data.category = 0;
                }
                
                else
                {
                    data.category_detail = " ";
                    data.category = null;
                }

                #endregion
                #region RecoferyRoom
                if (chck_OperasiTindakanLain.Checked)
                {
                    data.recovery_room = txt_OperasiTindakanLain.Text;
                }else if(cbl_recoveryroom.SelectedItem == null)
                {
                    data.recovery_room = "";
                }
                else
                {
                    data.recovery_room = cbl_recoveryroom.SelectedItem.Text;
                }
                




                #endregion
                #region Fasting
                if (chbx_puasa_tidak.Checked)
                {
                    data.fasting_procedure = false;
                    data.fasting_procedure_time = 0;
                }
                else if (chbx_puasa_ya.Checked)
                {
                    data.fasting_procedure = true;
                    data.fasting_procedure_time = Convert.ToInt32(txt_puasa_ya.Text);
                }

                #endregion
                data.other_rad = txt_OtherRad.Text;
                data.other_lab = txt_otherLab.Text;
                data.instruction = txtinstruksirawatinap.Text;
                data.remarks = txtremarks.Text;
                data.doctor_id = Helper.doctorid.ToString();
                data.doctor_name = textbox_dokter.Text;
                data.diagnosis = textbox_diagnosis.Text;
                data.admission_date =txttglmasukrawat.Text;
                data.patientName = header.PatientName;
                data.birthDate = header.BirthDate.ToString();
                data.umur = (DateTime.Now - header.BirthDate).ToString();
                data.sexId = header.Gender.ToString();
                data.seks = header.Gender == 1 ? "Male" : "Female"; ;
                data.localMrNo = header.MrNo;
                data.create_encounter = "";
                data.created_date = DateTime.Now.ToString();
                data.created_by = MyUser.GetHopeUserID();
                data.modified_by = MyUser.GetHopeUserID();
                data.is_pregnancy = false;
                data.is_edited = false;
                data.is_active = true;
                data.is_action = chbx_tindakanoperasi_ya.Checked ? true : false;

                data.modified_date = DateTime.Now.ToString();

                #region OperationScheduleHeader
                OperationScheduleHeader opsHeader = new OperationScheduleHeader();
                opsHeader.operation_schedule_id = data.operation_schedule_id;
                opsHeader.organization_id = Int16.Parse(Helper.organizationId.ToString());
                opsHeader.operation_schedule_date = txt_tglperkiraanoperasi.Text;
                opsHeader.incision_time = txtwaktuperkiraanoperasi.Text;
                opsHeader.is_infectious = false;
                opsHeader.is_cito = false;
                opsHeader.positioning_time = 0;
                opsHeader.anesthetia_type_name = ddl_anasteticmethod.SelectedItem.Text;
                opsHeader.anesthetia_user_id = Convert.ToInt32(ddl_anasteticmethod.SelectedValue);
                if (chck_OperasiTindakanLain.Checked == true)
                {
                    opsHeader.recovery_room = txt_OperasiTindakanLain.Text;
                    opsHeader.recovery_room_id = 0;
                } else if (cbl_recoveryroom.SelectedItem != null)
                {
                    opsHeader.recovery_room = data.recovery_room;
                    opsHeader.recovery_room_id = Convert.ToInt32(cbl_recoveryroom.SelectedValue);
                    
                }
                else
                {
                    opsHeader.recovery_room = "";
                    opsHeader.recovery_room_id = null;
                }

                //opsHeader.admitted_to_ward_date = DateTime.Parse(txttglmasukrawat.Text).ToString("dd MMMM yyyy");
                opsHeader.admitted_to_ward_date = txttglmasukrawat.Text;
                opsHeader.doctor_notes = "";
                opsHeader.is_confirmed = false;
                opsHeader.is_active = true;
                opsHeader.room_id = Guid.Empty;
                opsHeader.status_booking_id = "6";
                opsHeader.status_booking_name = "";
                opsHeader.patient_id = hfPatientId.Value;
                opsHeader.created_date = DateTime.Now.ToString();
                opsHeader.created_by = MyUser.GetHopeUserID();
                opsHeader.modified_date = DateTime.Now.ToString();
                opsHeader.modified_by = MyUser.GetHopeUserID();
                opsHeader.by_preop = false;
                opsHeader.is_from_opd = true;
                opsHeader.recovery_time =0;
                opsHeader.anesthetia_duration = 0;
                opsHeader.admission_no = header.AdmissionNo;
                opsHeader.ipd_admission_no = "";
                opsHeader.nurse_notes = "";
                opsHeader.note_update = 1;
                opsHeader.note_update_string = "";
                opsHeader.additional_note_update = "";
                opsHeader.note_delete = "";
                opsHeader.note_pembatalan = 1;
                opsHeader.additional_note_pembatalan = "";
                opsHeader.note_pembatalan_string = "";
                opsHeader.cancel_by = "";
                opsHeader.cancel_date = "";
                opsHeader.by_doctor = false;
                opsHeader.status_date = "";
                opsHeader.is_rujukan = false;
                opsHeader.report_rujukan = false;
                opsHeader.temp_patientname = header.PatientName ;
                opsHeader.temp_dob = header.BirthDate.ToString();
                opsHeader.temp_contactno = "0";

                data.operation_schedule_header = opsHeader;
                #endregion


                #region OperationProcedure
                List<OperationProcedure> operationProcedures = new List<OperationProcedure>();
                OperationProcedure operationProcedure = new OperationProcedure();
                operationProcedure.operation_procedure_id = data.operation_schedule_id.ToString();
                operationProcedure.operation_schedule_id = opsHeader.operation_schedule_id; //opsHeader.operation_schedule_id.ToString();


                
                operationProcedure.procedure_user_id = 0;
                operationProcedure.procedure_estimate_time = ((Int16.Parse(txt_JamLamaOperasi.Text) * 60) + Int16.Parse(txt_MenitLamaOperasi.Text));
                operationProcedure.asisten_operator_id = 0;
                operationProcedure.konsultan_operator_id = 0;
                operationProcedure.start_time = "";
                operationProcedure.is_active = true;
                operationProcedure.created_by = MyUser.GetHopeUserID();
                operationProcedure.created_date = DateTime.Now.ToString();
                operationProcedure.modified_by = MyUser.GetHopeUserID();
                operationProcedure.modified_date = DateTime.Now.ToString();
                operationProcedures.Add(operationProcedure);

                data.operation_procedures = operationProcedures;
                #endregion

                #region Labrad
                
                List<CpoeTrans> listrad = new List<CpoeTrans>();
                listrad = (List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value];
                List<CpoeTrans> listlab = new List<CpoeTrans>();
                listlab = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];

                if (listrad != null || listrad != null)
                {
                    List<CpoeTrans> labRadAddcpoe = new List<CpoeTrans>();
                    labRadAddcpoe.AddRange(listlab);
                    labRadAddcpoe.AddRange(listrad);

                    List<LabRadAdditional> listLabRad = new List<LabRadAdditional>();


                    for (int i = 0; i < labRadAddcpoe.Count; i++)
                    {
                        LabRadAdditional lab_Rad_Additionals = new LabRadAdditional();
                        lab_Rad_Additionals.lab_rad_additional_id = 0;
                        lab_Rad_Additionals.operation_schedule_additional_id = data.operation_schedule_additional_id;
        
                        lab_Rad_Additionals.item_id = (int)(labRadAddcpoe[i].id);
                        lab_Rad_Additionals.item_name = labRadAddcpoe[i].name;
                        lab_Rad_Additionals.item_type = labRadAddcpoe[i].type;
                        lab_Rad_Additionals.item_type_name = labRadAddcpoe[i].type;
                        lab_Rad_Additionals.created_by = MyUser.GetHopeUserID();
                        lab_Rad_Additionals.modified_by = MyUser.GetHopeUserID();
                        lab_Rad_Additionals.modified_date = DateTime.Now.ToString();
                        if(labRadAddcpoe[i].isdelete == 1)
                        {
                            lab_Rad_Additionals.is_active = false;
                        }
                        else
                        {
                            lab_Rad_Additionals.is_active = true;
                        }

                        if(labRadAddcpoe[i].type.ToString() == "ClinicLab" || labRadAddcpoe[i].type.ToString() == "MicroLab" || labRadAddcpoe[i].type.ToString() == "CitoLab"|| labRadAddcpoe[i].type.ToString() == "PanelLab"|| labRadAddcpoe[i].type.ToString() == "PatologiLab"|| labRadAddcpoe[i].type.ToString() == "MDCLab")
                        {
                            lab_Rad_Additionals.is_rad = false;
                        }
                        else
                        {
                            lab_Rad_Additionals.is_rad = true;
                        }
                            
                        lab_Rad_Additionals.modified_date = DateTime.Now.ToString();
                        lab_Rad_Additionals.modified_by = MyUser.GetHopeUserID();
                        lab_Rad_Additionals.created_date = DateTime.Now.ToString();
                        if(lab_Rad_Additionals.is_active == true)
                        {
                            listLabRad.Add(lab_Rad_Additionals);
                        }
                        
                    }
                    data.lab_Rad_Additionals = listLabRad;

                }

                #endregion
            }
            #endregion
           
            else if (chbx_tindakanoperasi_tidak.Checked)
            {
            #region Tanpa tindakan operasi

                // data.user_id = (MyUser.GetHopeUserID());
                //data.operation_schedule_additional_id = 0;
                // data.operation_schedule_id = Guid.Empty;
                data.encounter_id = header.EncounterId;
                data.use_tools = false;
                data.tools_detail = "";
                data.category = 0;
                data.category_detail = "";
                data.recovery_room = "";
                data.fasting_procedure = false;
                data.fasting_procedure_time = 0;
                data.other_rad = txt_OtherRad.Text;
                data.other_lab = txt_otherLab.Text;
                data.instruction = txtinstruksirawatinap.Text;
                data.remarks = txtremarks.Text;
                data.doctor_id = Helper.doctorid.ToString();
                data.doctor_name = textbox_dokter.Text;
                data.diagnosis = textbox_diagnosis.Text;
                //data.admission_date = DateTime.Parse(txttglmasukrawat.Text).ToString("dd MMMM yyyy");
                data.admission_date = txttglmasukrawat.Text;
                #region Bangsal
                if (chck_BangsalLain.Checked)
                {
                    data.ward_name = txt_BangsalLain.Text;
                    data.ward_id = "0";
                }
                
                if(cbl_ward.SelectedValue == "")
                {
                    data.ward_name = "";
                    data.ward_id = "";
                }
                else
                {
                    data.ward_name = cbl_ward.SelectedItem.Text;
                    data.ward_id = cbl_ward.SelectedItem.Value;
                }
                #endregion
                data.patient_id = (hfPatientId.Value);
                data.patientName = header.PatientName;
                data.birthDate = header.BirthDate.ToString();
                data.umur = (DateTime.Now - header.BirthDate).ToString();
                data.sexId = header.Gender.ToString();
                data.seks = header.Gender == 1 ? "Male" : "Female";
                data.localMrNo = header.MrNo;
                data.create_encounter = "";
                data.created_date = DateTime.Now.ToString();
                data.created_by = MyUser.GetUsername();
                data.modified_by = MyUser.GetUsername();
                data.is_pregnancy =false;
                data.is_edited = false;
                data.is_active = true;
                data.is_action = false;

                data.modified_date = DateTime.Now.ToString();
                #region OperationScheduleHeader
                OperationScheduleHeader opsHeader = new OperationScheduleHeader();
                opsHeader.operation_schedule_id = Guid.Empty;
                opsHeader.organization_id = Int16.Parse(Helper.organizationId.ToString());
                opsHeader.operation_schedule_date = "";
                opsHeader.incision_time = "";
                opsHeader.is_infectious = false;
                opsHeader.is_cito = false;
                opsHeader.positioning_time = 0;
                opsHeader.anesthetia_type_name = "";
                opsHeader.anesthetia_user_id = 0;
                opsHeader.recovery_room = "";
                opsHeader.status_booking_name = "";
                //DateTime.Parse(txttglmasukrawat.Text).ToString("dd MMMM yyyy");
                opsHeader.admitted_to_ward_date = txttglmasukrawat.Text;
                opsHeader.doctor_notes = "";
                opsHeader.is_confirmed = false;
                opsHeader.is_active = false;
                opsHeader.room_id = Guid.Empty;
                opsHeader.status_booking_id = "6";
                opsHeader.patient_id = hfPatientId.Value;
                opsHeader.created_date = DateTime.Now.ToString();
                opsHeader.created_by = MyUser.GetHopeUserID();
                opsHeader.modified_date = DateTime.Now.ToString();
                opsHeader.modified_by = MyUser.GetHopeUserID();
                opsHeader.by_preop = true;
                opsHeader.is_from_opd = true;
                opsHeader.recovery_time = 0;
                opsHeader.anesthetia_duration = 0;
                opsHeader.recovery_room_id = 0;
                opsHeader.admission_no = header.AdmissionNo;
                opsHeader.ipd_admission_no = "";
                opsHeader.nurse_notes = "";
                opsHeader.note_update = 1;
                opsHeader.note_update_string = "";
                opsHeader.additional_note_update = "";
                opsHeader.note_delete = "";
                opsHeader.note_pembatalan = 1;
                opsHeader.additional_note_pembatalan = "";
                opsHeader.note_pembatalan_string = "";
                opsHeader.cancel_by = "";
                opsHeader.cancel_date = "";
                opsHeader.by_doctor = false;
                opsHeader.status_date = "";
                opsHeader.is_rujukan = false;
                opsHeader.report_rujukan = false;
                opsHeader.temp_patientname = header.PatientName;
                opsHeader.temp_dob = header.BirthDate.ToString();
                opsHeader.temp_contactno = "+62";

                data.operation_schedule_header = opsHeader;
                #endregion

                #region OperationProcedure
                List<OperationProcedure> operationProcedures = new List<OperationProcedure>();
               

                data.operation_procedures = operationProcedures;
                #endregion

                #region Labrad
                List<LabRadAdditional> labrad = new List<LabRadAdditional>();

                data.lab_Rad_Additionals = labrad;
                #endregion
                

            }
            #endregion
        }
        catch (Exception e)
        {
            throw e;
        }
       
        return data;
    }

    public void BtnDeletelab_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
            HiddenField lab_id = (HiddenField)gv_Lab.Rows[selRowIndex].FindControl("hf_id_lab");

            List<CpoeTrans> listcheck = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];

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
                        else if (x.isnew == 1)
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

            if (Helper.ToDataTable(listcheck).Select("isdelete = 0").Count() > 0)
            {
                gv_Lab.DataSource = Helper.ToDataTable(listcheck).Select("isdelete = 0").CopyToDataTable();
                gv_Lab.DataBind();
            }
            else
            {
                gv_Lab.DataSource = null;
                gv_Lab.DataBind();
            }

            up_DivLab.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnDeletelab_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnDeletelab_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
    }
    public void BtnDeleteRad_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
            HiddenField rad_id = (HiddenField)gv_Rad.Rows[selRowIndex].FindControl("hf_id_rad");

            List<CpoeTrans> listcheck = (List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value];

            var item = new CpoeTrans();
            var flagitem = 0;

            foreach (var x in listcheck.Where(x => x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT"))
            {
                if (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT")
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

            if (Helper.ToDataTable(listcheck).Select("isdelete = 0").Count() > 0)
            {
                List<CpoeTrans> listcheckedshow = new List<CpoeTrans>();
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
                    listcheckedshow.Add(temp);
                }

                gv_Rad.DataSource = Helper.ToDataTable(listcheckedshow).Select("isdelete = 0").CopyToDataTable();
                gv_Rad.DataBind();
            }
            else
            {
                gv_Rad.DataSource = null;
                gv_Rad.DataBind();
            }

            up_DivRad.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnDeleteRad_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnDeleteRad_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
    }

    public void JamOperasiValidation(bool txt)
    {
        if (txt == true)
        {
            TextBox txt_JamLamaOperasi = (TextBox)FindControl("txt_JamLamaOperasi");
            txt_JamLamaOperasi.Style.Add("border", "1px solid red");
           
        }
        else
        {
            TextBox txt_JamLamaOperasi = (TextBox)FindControl("txt_JamLamaOperasi");
            txt_JamLamaOperasi.Style.Add("border", "1px solid #76767C");
        }

    }

    public void MenitOperasiValidation(bool txt)
    {
        if (txt == true)
        {
            TextBox txt_MenitLamaOperasi = (TextBox)FindControl("txt_MenitLamaOperasi");
            txt_MenitLamaOperasi.Style.Add("border", "1px solid red");

        }
        else
        {
            TextBox txt_MenitLamaOperasi = (TextBox)FindControl("txt_MenitLamaOperasi");
            txt_MenitLamaOperasi.Style.Add("border", "1px solid #76767C");
        }

    }

    public void BtnAjaxSearchProcedure_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            DataTable procedureSelect = new DataTable();
            procedureSelect = ((DataTable)Session[Helper.SessionProcedureInpatientData]).Select("operationprocedure_id = '" + hf_ItemSelectedProcedure.Value + "' AND procedure_name = '" + hf_ItemSelectedProcedure_name.Value + "' AND diagnosis = '" + hf_ItemSelectedProcedure_diagnosis.Value + "'").CopyToDataTable();

            List<OperationProcedure> listExcludeProcedure = new List<OperationProcedure>();
            List<OperationProcedure> listProcedureTemp;

            if (Session[Helper.SessionProcedureInpatientChecked + hfguidadditional.Value] == null)
            {
                listProcedureTemp = new List<OperationProcedure>();
            }
            else
            {
                listProcedureTemp = new List<OperationProcedure>();
                listProcedureTemp = (List<OperationProcedure>)Session[Helper.SessionProcedureInpatientChecked + hfguidadditional.Value];
            }

            OperationProcedure op = new OperationProcedure();
            op.operation_procedure_id = procedureSelect.Rows[0]["operationprocedure_id"].ToString();
            op.operation_schedule_id = Guid.Empty;
            op.procedure_name = procedureSelect.Rows[0]["procedure_name"].ToString();
            op.procedure_user_id = 0;
            op.procedure_estimate_time = 0;
            op.procedure_name_id = Int32.Parse(procedureSelect.Rows[0]["operationprocedure_id"].ToString());
            op.asisten_operator_id = 0;
            op.konsultan_operator_id = 0;
            op.start_time = "";
            op.is_active = true;
            op.created_by = MyUser.GetHopeUserID();
            op.created_date = DateTime.Now.ToString();
            op.modified_by = MyUser.GetHopeUserID();
            op.modified_date = DateTime.Now.ToString();

            if (listProcedureTemp != null) {
                if (listProcedureTemp.Any(y => y.procedure_name == op.procedure_name && y.is_active == true))
                {
                    listExcludeProcedure.Add(op);
                }
                else if (listProcedureTemp.Any(y => y.procedure_name == op.procedure_name && y.is_active == false))
                {
                    listProcedureTemp.FirstOrDefault(z => z.procedure_name == op.procedure_name).is_active = true;
                }
                else
                {
                    listProcedureTemp.Add(op);
                }
            }
            else {
                listProcedureTemp.Add(op);
            }

            Session[Helper.SessionProcedureInpatientChecked + hfguidadditional.Value] = listProcedureTemp;
            listProcedureInpatientChecked = (List<OperationProcedure>)Session[Helper.SessionProcedureInpatientChecked + hfguidadditional.Value];

            if (listProcedureInpatientChecked != null)
            {
                if (Helper.ToDataTable(listProcedureInpatientChecked).Select("is_active = true").Count() > 0)
                {
                    List<OperationProcedure> listcheckedTemp = new List<OperationProcedure>();
                    foreach (var item in listProcedureInpatientChecked)
                    {
                        OperationProcedure temp = new OperationProcedure();
                        temp.operation_procedure_id = item.operation_procedure_id;
                        temp.operation_schedule_id = item.operation_schedule_id;
                        temp.procedure_name = item.procedure_name;
                        temp.procedure_user_id = item.procedure_user_id;
                        temp.procedure_estimate_time = item.procedure_estimate_time;
                        temp.procedure_name_id = item.procedure_name_id;
                        temp.asisten_operator_id = item.asisten_operator_id;
                        temp.konsultan_operator_id = item.konsultan_operator_id;
                        temp.start_time = item.start_time;
                        temp.is_active = item.is_active;
                        temp.created_by = item.created_by;
                        temp.created_date = item.created_date;
                        temp.modified_by = item.modified_by;
                        temp.modified_date = item.modified_date;

                        listcheckedTemp.Add(temp);
                    }

                    DataTable dt = Helper.ToDataTable(listcheckedTemp).Select("is_active = true").CopyToDataTable();
                    gv_procedure.DataSource = dt;
                    gv_procedure.DataBind();
                }
                else
                {
                    gv_procedure.DataSource = null;
                    gv_procedure.DataBind();
                }
            }

            if (listExcludeProcedure.Count() > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Procedure Already Exist');", true);
            }

            txt_ItemProcedure.Text = "";
            up_DivProcedure.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnAjaxSearchProcedure_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnAjaxSearchProcedure_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
    }

    public void BtnDeleteProcedure_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
            HiddenField procedureId = (HiddenField)gv_procedure.Rows[selRowIndex].FindControl("hf_id_procedure");

            List<OperationProcedure> listItemCheck = (List<OperationProcedure>)Session[Helper.SessionProcedureInpatientChecked + hfguidadditional.Value];

            var itemProcedure = new OperationProcedure();
            int flagItem = 0;

            foreach (var x in listItemCheck)
            {
                if (x.operation_procedure_id == procedureId.Value.ToString())
                {
                    if (x.is_active == true)
                    {
                        x.is_active = false;
                        itemProcedure = x;
                        flagItem = 1;
                    }
                }
            }

            if (flagItem == 1)
            {
                listItemCheck.Remove(itemProcedure);
                flagItem = 0;
            }

            Session[Helper.SessionProcedureInpatientChecked + hfguidadditional.Value] = listItemCheck;

            if (Helper.ToDataTable(listItemCheck).Select("is_active = true").Count() > 0)
            {
                List<OperationProcedure> listCheckedShow = new List<OperationProcedure>();

                foreach (var x in listItemCheck)
                {
                    OperationProcedure temp = new OperationProcedure();
                    temp.operation_procedure_id = x.operation_procedure_id;
                    temp.operation_schedule_id = x.operation_schedule_id;
                    temp.procedure_name = x.procedure_name;
                    temp.procedure_user_id = x.procedure_user_id;
                    temp.procedure_estimate_time = x.procedure_estimate_time;
                    temp.procedure_name_id = x.procedure_name_id;
                    temp.asisten_operator_id = x.asisten_operator_id;
                    temp.konsultan_operator_id = x.konsultan_operator_id;
                    temp.start_time = x.start_time;
                    temp.is_active = x.is_active;
                    temp.created_by = x.created_by;
                    temp.created_date = x.created_date;
                    temp.modified_by = x.modified_by;
                    temp.modified_date = x.modified_date;

                    listCheckedShow.Add(temp);
                }

                gv_procedure.DataSource = Helper.ToDataTable(listCheckedShow).Select("is_active = true").CopyToDataTable();
                gv_procedure.DataBind();

            }
            else
            {
                gv_procedure.DataSource = null;
                gv_procedure.DataBind();
            }

            up_DivProcedure.Update();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnDeleteProcedure_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "BtnDeleteProcedure_Click", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
    }
    
}

