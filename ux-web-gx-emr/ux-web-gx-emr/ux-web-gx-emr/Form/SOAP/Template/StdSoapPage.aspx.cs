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
using static PatientHistory;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Text;
using System.IO;
using static FirstAssesment;
using System.Web.UI.HtmlControls;

public partial class Form_SOAP_Template_StdSoapPage : System.Web.UI.Page
{
    protected static readonly ILog log = LogManager.GetLogger(typeof(Form_SOAP_Template_StdSoapPage));

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Session[Helper.SessionLabPathologyChecked] = null;
            //Session[Helper.SessionSurgerySubjective] = null;
            //Session[Helper.SessionAllergySubjective] = null;
            Session[Helper.Sessionradcheck] = null;
            HyperLink test = Master.FindControl("SOAPLink") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");

            if (Helper.GetLoginUser(this) == null)
            {
                Response.Redirect("~/Form/General/Login.aspx", true);
                Context.ApplicationInstance.CompleteRequest();
            }
            if (Request.QueryString["EncounterId"] == null || Helper.GetDoctorID(this) == "")
            {
                Response.Redirect("~/Form/General/Login.aspx", true);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], Request.QueryString["PagefaId"], Request.QueryString["PageSoapId"], Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
                hfPatientId.Value = Request.QueryString["idPatient"];
                hfEncounterId.Value = Request.QueryString["EncounterId"];
                hfAdmissionId.Value = Request.QueryString["AdmissionId"];
                hfPagefaId.Value = Request.QueryString["PagefaId"];
                hfPageSoapId.Value = Request.QueryString["PageSoapId"];

                log.Debug(LogLibrary.Logging("S", "GetPatientHeader", Helper.GetLoginUser(this), "(param:" + long.Parse(hfPatientId.Value) + "," + hfEncounterId.Value.ToString() + ")"));
                var varResult = clsCommon.GetPatientHeader(long.Parse(hfPatientId.Value), hfEncounterId.Value.ToString());
                ResultPatientHeader JsongetPatientHistory = JsonConvert.DeserializeObject<ResultPatientHeader>(varResult.Result.ToString());
                log.Debug(LogLibrary.Logging("E", "GetPatientHeader", Helper.GetLoginUser(this), JsongetPatientHistory.ToString()));

                hfheader.Value = varResult.Result.ToString();

                var varResultconsultation = clsSOAP.GetConsultationFee(long.Parse(Helper.GetDoctorID(this)), Helper.organizationId, long.Parse(hfAdmissionId.Value), Guid.Parse(hfEncounterId.Value));
                ResultConsultFee JsongetResultconsultation = JsonConvert.DeserializeObject<ResultConsultFee>(varResultconsultation.Result.ToString());

                var getsoap = clsSOAP.getSOAP(hfEncounterId.Value, long.Parse(hfPatientId.Value), long.Parse(hfAdmissionId.Value), Helper.organizationId, long.Parse(Helper.GetDoctorID(this)));
                ResultSOAP Jsongetsoap = JsonConvert.DeserializeObject<ResultSOAP>(getsoap.Result.ToString());
                hfsoapstring.Value = getsoap.Result.ToString();
                hfsoapcopystring.Value = "";

                var getcopysoap = clsSOAP.getCopySOAP(Helper.organizationId, hfPatientId.Value, "");
                ResultViewAdmissionCopySOAP Jsongetcopysoap = JsonConvert.DeserializeObject<ResultViewAdmissionCopySOAP>(getcopysoap.Result.ToString());

                var varallergy = clsPatientDetail.GetPatientHistory(long.Parse(hfPatientId.Value), hfEncounterId.Value);
                ResultPatientHistory Jsongetallergy = JsonConvert.DeserializeObject<ResultPatientHistory>(varallergy.Result.ToString());
                var PatientAllergy = Jsongetallergy.list.allergy;
                DataTable dtAllergy = Helper.ToDataTable(PatientAllergy);

                hfallergy.Value = varallergy.Result.ToString();

                DataTable pagedatadt;
                if (Session[Helper.Sessionpolidata] == null)
                {
                    List<PageSpecialty> listpagedata = new List<PageSpecialty>();
                    var pagedata = clsFirstAssesment.GetPageSpecialty(long.Parse(Helper.GetDoctorID(this)), 1);
                    //var pagedata = clsFirstAssesment.GetPageSpecialty(long.Parse(Helper.GetDoctorID(this)), Helper.organizationId);
                    var Jsonpagedata = JsonConvert.DeserializeObject<ResultPageSpecialty>(pagedata.Result.ToString());

                    listpagedata = Jsonpagedata.list;
                    pagedatadt = Helper.ToDataTable(listpagedata);
                    Session[Helper.Sessionpolidata] = pagedatadt;
                }
                else
                    pagedatadt = (DataTable)Session[Helper.Sessionpolidata];

                List<ConsultFeestring> consfee = new List<ConsultFeestring>();
                if (JsongetResultconsultation.list.Count > 0)
                {
                    foreach (var x in JsongetResultconsultation.list)
                    {
                        ConsultFeestring temp = new ConsultFeestring();
                        //x.consultation_fee = decimal.Parse(x.consultation_fee.ToString("#,##0"));
                        string formatfee = String.Format("{0:n0}", x.consultation_fee).Replace('.', ',');
                        temp.consultation_fee = x.consultation_fee.ToString();
                        temp.sales_item_id = x.sales_item_id;
                        temp.sales_item_name = x.sales_item_name;
                        temp.consulation_fee_name = x.sales_item_name + " ~ Rp " + formatfee;
                        consfee.Add(temp);
                    }
                }

                PatientHeader header = JsongetPatientHistory.header;

                PatientCard.initializevalue(header);

                if (Session[Helper.SessionItemDrugPres] == null)
                {
                    DataTable dt = clsSOAP.getItemPres(Helper.organizationId, header.AdmissionTypeId);
                    Session[Helper.SessionItemDrugPres] = dt;
                }

                ddlForm_Type.DataSource = pagedatadt;
                ddlForm_Type.DataTextField = "page_specialty_name";
                ddlForm_Type.DataValueField = "page_specialty_id";
                ddlForm_Type.DataBind();


                ddl_consultationfee.DataSource = Helper.ToDataTable(consfee);
                ddl_consultationfee.DataTextField = "consulation_fee_name";
                ddl_consultationfee.DataValueField = "sales_item_id";
                ddl_consultationfee.DataBind();

                if (Jsongetsoap.list.save_mode == 0)
                {
                    string[] totalfeeformat = ddl_consultationfee.SelectedItem.Text.Split(new string[] { " ~ Rp " }, StringSplitOptions.None);
                    string totalfee = totalfeeformat[1];
                    txttotalfee.Text = totalfee;
                }
                else
                {
                    //string a = String.Format("{0:n0}", Jsongetsoap.list.consultation_amount);
                    txtConsfee.Text = Jsongetsoap.list.consultation_item_name + " ~ Rp " + String.Format("{0:n0}", Jsongetsoap.list.consultation_amount).Replace('.', ',');
                    if (Jsongetsoap.list.discount_amount == Jsongetsoap.list.consultation_amount)
                    {
                        rbFree.Checked = true;
                    }
                    else if (Jsongetsoap.list.discount_amount == 0)
                    {
                        rbNormal.Checked = true;
                    }
                    else if (Jsongetsoap.list.discount_amount < Jsongetsoap.list.consultation_amount)
                    {
                        rbdisc.Checked = true;
                        txtdisc.Text = String.Format("{0:n0}", Jsongetsoap.list.discount_amount).Replace('.', ',');
                    }

                    txtnotes.Text = Jsongetsoap.list.procedure_notes;
                    var calc = Jsongetsoap.list.consultation_amount - Jsongetsoap.list.discount_amount;
                    lbltotalfeedisable.Text = String.Format("{0:n0}", calc).Replace('.', ',');

                    hfsavemode.Value = Jsongetsoap.list.save_mode.ToString();
                    hfitemid.Value = Jsongetsoap.list.consultation_item_id.ToString();
                    hfitemname.Value = Jsongetsoap.list.consultation_item_name;
                    hfconsfee.Value = Jsongetsoap.list.consultation_amount.ToString();
                    hfdiscamount.Value = Jsongetsoap.list.discount_amount.ToString();
                }

                if (hfPageSoapId.Value == "00000000-0000-0000-0000-000000000000")
                {
                    ddlForm_Type.SelectedValue = "7ccd0a7e-9001-48ff-8052-ed07cf5716bb";
                }
                else
                    ddlForm_Type.SelectedValue = hfPageSoapId.Value;

                if (hfsavemode.Value == "0" || hfsavemode.Value == "")
                {
                    if (Jsongetcopysoap.list.Count() > 0)
                    {
                        DataTable dtcopy = Helper.ToDataTable(Jsongetcopysoap.list);
                        gvw_doctor.DataSource = dtcopy;
                        gvw_doctor.DataBind();
                    }
                    else
                    {
                        gvw_doctor.DataSource = null;
                        gvw_doctor.DataBind();
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hidecopy", "hidecopy();", true);


                Session[Helper.Sessionsoapmodel] = Jsongetsoap.list;
                

                StdSubjective.initializevalue(Jsongetsoap.list.subjective, Jsongetsoap.list.patient_disease, Jsongetsoap.list.patient_surgery, Jsongetsoap.list.patient_allergy, Jsongetsoap.list.patient_medication);
                StdObjective.initializevalue(Jsongetsoap.list.objective);
                //StdPlanning.initializevalue(Jsongetsoap, header, dtAllergy, Jsongetallergy.list.currentmedication);
                StdAssessment.initializevalue(Jsongetsoap.list.assessment);

                
                List<Subjective> listsubjective = Jsongetsoap.list.subjective;
                if (listsubjective.Count > 0)
                {
                    foreach (Subjective x in listsubjective)
                    {
                        if (x.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0"))
                        {//anamnesis
                            Anamnesis.Text = x.remarks;
                        }
                        else if (x.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf"))
                        {//patient complaint
                            if (x.remarks != "")
                            {
                                Complaint.Text = x.remarks;
                            }
                        }
                        else if (x.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923"))
                        {//pregnancy
                            if (x.value == "True")
                            {
                                chkpregnant.Checked = true;
                            }
                            else
                            {
                                chkpregnant.Checked = false;
                            }
                        }
                        else if (x.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937"))
                        {//breast feeding
                            if (x.value == "True")
                            {
                                chkbreastfeed.Checked = true;
                            }
                            else
                            {
                                chkbreastfeed.Checked = false;
                            }
                        }
                    }
                }

                List<Planning> listplanning = Jsongetsoap.list.planning;
                if (listplanning.Count > 0)
                {
                    foreach (Planning x in listplanning)
                    {
                        if (x.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
                        {
                            txtPlanning.Text = x.remarks;
                        }
                    }
                }

                preview.NavigateUrl = preview.NavigateUrl + "?idPatient=" + hfPatientId.Value.ToString() + "&AdmissionId=" + hfAdmissionId.Value.ToString() + "&EncounterId=" + hfEncounterId.Value.ToString();
                email.NavigateUrl = email.NavigateUrl + "?idPatient=" + hfPatientId.Value.ToString() + "&AdmissionId=" + hfAdmissionId.Value.ToString() + "&EncounterId=" + hfEncounterId.Value.ToString();

                log.Info(LogLibrary.Logging("E", "Page_Load", Helper.GetLoginUser(this), ""));

            }
        }
        StdSubjective.checkIfExist += new Form_SOAP_Control_Template_StdSubjective.customHandler(MyParentMethod);
        StdSubjective.checkIfExistAllergy += new Form_SOAP_Control_Template_StdSubjective.customHandler(MyParentMethodAllergy);
        StdSubjective.checkIfExistAllergyFood += new Form_SOAP_Control_Template_StdSubjective.customHandler(MyParentMethodAllergyFood);
    }

    bool MyParentMethod(object sender)
    {
        DataTable dtroutinemed = Session["routinemed"] as DataTable;
        Session.Remove("routinemed");

        if (Session["routinedeleted"] != null)
        {
            string itemdeleted = Session["routinedeleted"].ToString();
            Session.Remove("routinedeleted");
            //StdPlanning.UpdateRoutineMedication(dtroutinemed, itemdeleted);
        }

        return true;
    }

    bool MyParentMethodAllergy(object sender)
    {
        DataTable dtroutinemed = Session["routinemed"] as DataTable;
        Session.Remove("routinemed");
        StdPlanning.UpdateDrugAllergy(dtroutinemed);
        return true;
    }

    bool MyParentMethodAllergyFood(object sender)
    {
        DataTable dtroutinemed = Session["routinemed"] as DataTable;
        Session.Remove("routinemed");
        StdPlanning.UpdateFoodAllergy(dtroutinemed);
        return true;
    }

    protected void btnChoose_onClick(object sender, EventArgs e)
    {
        Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], Request.QueryString["pagefaid"], ddlForm_Type.SelectedValue, Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
        Helper.ResponseRedirectSOAP(Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], Request.QueryString["pagefaid"], ddlForm_Type.SelectedValue, Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
    }

    protected void btnPreview_click(object sender, EventArgs e)
    {
        SoapPagePreview.initializevalue(Helper.organizationId, long.Parse(hfPatientId.Value), long.Parse(hfAdmissionId.Value), Guid.Parse(hfEncounterId.Value));
    }

    protected void btnsave_click(object sender, EventArgs e)
    {
        //listchecked = (List<ListChecked>)Session[Helper.SessionLabPathologyChecked];
        try
        {
            bool qty_flag = StdPlanning.CheckQuantityPrescription(1);
            bool qtycomp_flag = StdPlanning.CheckQuantityPrescription(2);
            bool qtycomp_detail_flag = StdPlanning.CheckQuantityPrescription(3);
            bool qtycons_flag = StdPlanning.CheckQuantityPrescription(4);
            if (!qty_flag)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Drugs Qty');", true);
            }
            else if (!qtycomp_flag)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Compound Qty');", true);
            }
            else if (!qtycomp_detail_flag)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Compound Detail Qty');", true);
            }
            else if (!qtycons_flag)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Consumables Qty');", true);
            }
            else
            {
                SaveDraft_SOAP();
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }

    }

    protected void btnSubmitDisable_click(object sender, EventArgs e)
    {
        try
        {
            bool qty_flag = StdPlanning.CheckQuantityPrescription(1);
            bool qtycomp_flag = StdPlanning.CheckQuantityPrescription(2);
            bool qtycomp_detail_flag = StdPlanning.CheckQuantityPrescription(3);
            bool qtycons_flag = StdPlanning.CheckQuantityPrescription(4);
            if (!qty_flag)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Drugs Qty');", true);
            }
            else if (!qtycomp_flag)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Compound Qty');", true);
            }
            else if (!qtycomp_detail_flag)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Compound Detail Qty');", true);
            }
            else if (!qtycons_flag)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Consumables Qty');", true);
            }
            else
            {
                Submit_SOAP_disable();
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Failed to Submit');", true);
        }
    }

    protected void btnSubmit_click(object sender, EventArgs e)
    {
        try
        {
            bool qty_flag = StdPlanning.CheckQuantityPrescription(1);
            bool qtycomp_flag = StdPlanning.CheckQuantityPrescription(2);
            bool qtycomp_detail_flag = StdPlanning.CheckQuantityPrescription(3);
            bool qtycons_flag = StdPlanning.CheckQuantityPrescription(4);
            if (!qty_flag)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Drugs Qty');", true);
            }
            else if (!qtycomp_flag)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Compound Qty');", true);
            }
            else if (!qtycomp_detail_flag)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Compound Detail Qty');", true);
            }
            else if (!qtycons_flag)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Consumables Qty');", true);
            }
            else
            {
                Submit_SOAP();
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
    }

    public void Submit_SOAP_disable()
    {
        log.Info(LogLibrary.Logging("S", "SaveDraft_SOAP", Helper.GetLoginUser(this), ""));

        SOAP soapmodel = (SOAP)Session[Helper.Sessionsoapmodel];
        soapmodel = StdSubjective.GetSubjectiveValues(soapmodel);
        soapmodel = StdPlanning.GetPlanningValues(soapmodel);
        soapmodel = StdObjective.GetObjectiveValues(soapmodel);
        soapmodel = StdAssessment.GetAssessmentValues(soapmodel);

        foreach (var subjective in soapmodel.subjective)
        {
            if (subjective.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf"))
            {//patientcomplaint
                subjective.remarks = Complaint.Text;
            }
            if (subjective.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0"))
            {//anamnesis
                subjective.remarks = Anamnesis.Text;
            }
            if (subjective.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923"))
            {//pregnancy
                if (chkpregnant.Checked)
                {
                    subjective.value = "True";
                }
                else
                {
                    subjective.value = "False";
                }
            }
            else if (subjective.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937"))
            {//breast feeding
                if (chkbreastfeed.Checked)
                {
                    subjective.value = "True";
                }
                else
                {
                    subjective.value = "False";
                }
            }
        }

        foreach (var planning in soapmodel.planning)
        {
            if (planning.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
            {
                planning.remarks = txtPlanning.Text;
            }
        }
        soapmodel.save_mode = 1;

        long itemid = long.Parse(hfitemid.Value);
        double ConsultationAmount = double.Parse(hfconsfee.Value);
        double discountamount = double.Parse(hfdiscamount.Value);


        log.Debug(LogLibrary.Logging("S", "SaveAsDraftSOAP", Helper.GetLoginUser(this), soapmodel.ToString()));
        string appointmentid = Request.QueryString["AppointmentId"];
        string username = Helper.GetLoginUser(this);
        var getMap = clsSOAP.SubmitSOAP(soapmodel, itemid, ConsultationAmount, discountamount, txtnotes.Text, hfPageSoapId.Value, hfitemname.Value, appointmentid, username);
        var JsongetMap = (JObject)JsonConvert.DeserializeObject<dynamic>(getMap.Result);
        var Status = JsongetMap.Property("status").Value.ToString();
        var Message = JsongetMap.Property("message").Value.ToString();

        if (Status == "Fail")
        {
            log.Info(LogLibrary.Logging("E", "SaveDraft_SOAP", Helper.GetLoginUser(this), "Failed Save SOAP: " + Message));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Failed to Save SOAP');", true);
        }
        else
        {
            Response.Redirect(Request.RawUrl);
        }
        log.Debug(LogLibrary.Logging("E", "SaveAsDraftSOAP", Helper.GetLoginUser(this), getMap.ToString()));

        //Response.Redirect(Request.RawUrl);

        log.Info(LogLibrary.Logging("E", "SaveDraft_SOAP", Helper.GetLoginUser(this), ""));
    }

    public void Submit_SOAP()
    {
        log.Info(LogLibrary.Logging("S", "SaveDraft_SOAP", Helper.GetLoginUser(this), ""));

        SOAP soapmodel = (SOAP)Session[Helper.Sessionsoapmodel];
        soapmodel = StdSubjective.GetSubjectiveValues(soapmodel);
        soapmodel = StdPlanning.GetPlanningValues(soapmodel);
        soapmodel = StdObjective.GetObjectiveValues(soapmodel);
        soapmodel = StdAssessment.GetAssessmentValues(soapmodel);

        foreach (var subjective in soapmodel.subjective)
        {
            if (subjective.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf"))
            {//patientcomplaint
                subjective.remarks = Complaint.Text;
            }
            if (subjective.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0"))
            {//anamnesis
                subjective.remarks = Anamnesis.Text;
            }
            if (subjective.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923"))
            {//pregnancy
                if (chkpregnant.Checked)
                {
                    subjective.value = "True";
                }
                else
                {
                    subjective.value = "False";
                }
            }
            else if (subjective.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937"))
            {//breast feeding
                if (chkbreastfeed.Checked)
                {
                    subjective.value = "True";
                }
                else
                {
                    subjective.value = "False";
                }
            }
        }

        foreach (var planning in soapmodel.planning)
        {
            if (planning.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
            {
                planning.remarks = txtPlanning.Text;
            }
        }
        soapmodel.save_mode = 1;

        long itemid = long.Parse(ddl_consultationfee.SelectedValue);

        string[] totalfeeformat = ddl_consultationfee.SelectedItem.Text.Split(new string[] { " ~ Rp " }, StringSplitOptions.None);
        string totalfee = totalfeeformat[1];

        double ConsultationAmount = double.Parse(totalfee.Replace(",", ""));
        double discountamount = 0;
        if (rbPrice2.Checked)
        {
            discountamount = ConsultationAmount;
        }
        else if (rbPrice3.Checked)
        {
            discountamount = double.Parse(txtDiscount.Text);
        }


        log.Debug(LogLibrary.Logging("S", "SaveAsDraftSOAP", Helper.GetLoginUser(this), soapmodel.ToString()));
        string appointmentid = Request.QueryString["AppointmentId"];
        string username = Helper.GetLoginUser(this);
        var getMap = clsSOAP.SubmitSOAP(soapmodel, itemid, ConsultationAmount, discountamount, txtProcedure.Text, hfPageSoapId.Value, totalfeeformat[0].ToString(), appointmentid, username);
        var JsongetMap = (JObject)JsonConvert.DeserializeObject<dynamic>(getMap.Result);
        var Status = JsongetMap.Property("status").Value.ToString();
        var Message = JsongetMap.Property("message").Value.ToString();

        if (Status == "Fail")
        {
            log.Info(LogLibrary.Logging("E", "SaveDraft_SOAP", Helper.GetLoginUser(this), "Failed Save SOAP: " + Message));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Failed to Save SOAP');", true);
        }
        else
        {
            Response.Redirect(Request.RawUrl);
        }


        log.Debug(LogLibrary.Logging("E", "SaveAsDraftSOAP", Helper.GetLoginUser(this), getMap.ToString()));

        //Response.Redirect(Request.RawUrl);

        log.Info(LogLibrary.Logging("E", "SaveDraft_SOAP", Helper.GetLoginUser(this), ""));
    }
    
    public void SaveDraft_SOAP()
    {
        log.Info(LogLibrary.Logging("S", "SaveDraft_SOAP", Helper.GetLoginUser(this), ""));
        SOAP soapmodel = (SOAP)Session[Helper.Sessionsoapmodel];
        soapmodel = StdSubjective.GetSubjectiveValues(soapmodel);
        soapmodel = StdPlanning.GetPlanningValues(soapmodel);
        soapmodel = StdObjective.GetObjectiveValues(soapmodel);
        soapmodel = StdAssessment.GetAssessmentValues(soapmodel);

        foreach (var subjective in soapmodel.subjective)
        {
            if (subjective.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf"))
            {//patientcomplaint
                subjective.remarks = Complaint.Text;
            }
            if (subjective.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0"))
            {//anamnesis
                subjective.remarks = Anamnesis.Text;
            }
            if (subjective.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923"))
            {//pregnancy
                if (chkpregnant.Checked)
                {
                    subjective.value = "True";
                }
                else
                {
                    subjective.value = "False";
                }
            }
            else if (subjective.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937"))
            {//breast feeding
                if (chkbreastfeed.Checked)
                {
                    subjective.value = "True";
                }
                else
                {
                    subjective.value = "False";
                }
            }
        }

        foreach (var planning in soapmodel.planning)
        {
            if (planning.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
            {
                planning.remarks = txtPlanning.Text;
            }
        }

        log.Debug(LogLibrary.Logging("S", "SaveAsDraftSOAP", Helper.GetLoginUser(this), soapmodel.ToString()));
        string appointmentid = Request.QueryString["AppointmentId"];
        string username = Helper.GetLoginUser(this);
        var getMap = clsSOAP.SaveAsDraftSOAP(soapmodel, hfPageSoapId.Value, appointmentid, username);

        var JsongetMap = (JObject)JsonConvert.DeserializeObject<dynamic>(getMap.Result);
        var Status = JsongetMap.Property("status").Value.ToString();
        var Message = JsongetMap.Property("message").Value.ToString();

        if (Status == "Fail")
        {
            log.Info(LogLibrary.Logging("E", "SaveDraft_SOAP", Helper.GetLoginUser(this), "Failed Save SOAP: "+Message));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Failed to Save SOAP');", true);
        }
        else
        {
            Response.Redirect(Request.RawUrl);
        }
        log.Debug(LogLibrary.Logging("E", "SaveAsDraftSOAP", Helper.GetLoginUser(this), getMap.ToString()));
        log.Info(LogLibrary.Logging("E", "SaveDraft_SOAP", Helper.GetLoginUser(this), ""));
    }

    protected void btngetitem_onClick(object sender, EventArgs e)
    {
        foreach (GridViewRow rows in gvw_doctor.Rows)
        {
            LinkButton linkcopy = (LinkButton)rows.FindControl("copyitem");
            linkcopy.Style.Add("color", "#000");
        }
        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        HiddenField hfcopyPatientId = (HiddenField)gvw_doctor.Rows[selRowIndex].FindControl("hfcopyPatientId");
        HiddenField hfcopyOrganizationId = (HiddenField)gvw_doctor.Rows[selRowIndex].FindControl("hfcopyOrganizationId");
        HiddenField hfcopyAdmissionId = (HiddenField)gvw_doctor.Rows[selRowIndex].FindControl("hfcopyAdmissionId");
        HiddenField hfcopyDoctorId = (HiddenField)gvw_doctor.Rows[selRowIndex].FindControl("hfcopyDoctorId");
        HiddenField hfcopyEncounterId = (HiddenField)gvw_doctor.Rows[selRowIndex].FindControl("hfcopyEncounterId");
        LinkButton linkpatientcopy = (LinkButton)gvw_doctor.Rows[selRowIndex].FindControl("copyitem");


        linkpatientcopy.Style.Add("color", "#4d9b35");

        var getsoap = clsSOAP.getSOAP(hfcopyEncounterId.Value, long.Parse(hfcopyPatientId.Value), long.Parse(hfcopyAdmissionId.Value),
        long.Parse(hfcopyOrganizationId.Value), long.Parse(hfcopyDoctorId.Value));
        ResultSOAP Jsongetsoap = JsonConvert.DeserializeObject<ResultSOAP>(getsoap.Result.ToString());
        hfsoapcopystring.Value = getsoap.Result.ToString();

        int flagsubjective = 0;
        chkSubjective.Enabled = true;
        chkSubjective.Checked = false;
        List<Subjective> listsubjective = Jsongetsoap.list.subjective;
        if (listsubjective.Count > 0)
        {
            foreach (Subjective x in listsubjective)
            {
                if (x.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0"))
                {//anamnesis
                    if (x.remarks != "")
                    {
                        lblAnamnesis.Text = x.remarks;
                        flagsubjective = 1;
                    }
                    else
                        lblAnamnesis.Text = "";
                }
                else if (x.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf"))
                {//patient complaint
                    if (x.remarks != "")
                    {
                        lblChiefComplaint.Text = x.remarks;
                        flagsubjective = 1;
                    }
                    else
                        lblChiefComplaint.Text = "";
                }
            }
        }

        if (flagsubjective == 0)
            chkSubjective.Enabled = false;
        else
            chkSubjective.Checked = true;

        int flagobjective = 0;
        chkObjective.Enabled = true;
        chkObjective.Checked = false;
        List<Objective> listobjective = Jsongetsoap.list.objective;
        if (listobjective.Count > 0)
        {
            foreach (Objective x in listobjective)
            {
                if (x.soap_mapping_id == Guid.Parse("7218971c-e89f-4172-ae3c-b7fb855c1d6d"))
                {//"Others"
                    if (x.remarks != "")
                    {
                        lblobjective.Text = x.remarks;
                        flagobjective = 1;
                    }
                    else
                        lblobjective.Text = "";
                }
            }
        }

        if (flagobjective == 0)
            chkObjective.Enabled = false;
        else
            chkObjective.Checked = true;

        int flagassessment = 0;
        chkAssessment.Enabled = true;
        chkAssessment.Checked = false;
        List<Assessment> listassessment = Jsongetsoap.list.assessment;
        if (listassessment.Count > 0)
        {
            foreach (Assessment x in listassessment)
            {
                if (x.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47"))
                {//"Primary"
                    if (x.remarks != "")
                    {
                        lblAssessment.Text = x.remarks;
                        flagassessment = 1;
                    }
                    else
                        lblAssessment.Text = "";
                }
            }
        }

        if (flagassessment == 0)
            chkAssessment.Enabled = false;
        else
            chkAssessment.Checked = true;

        int flagplanning = 0;
        chkPlanning.Enabled = true;
        chkPlanning.Checked = false;
        List<Planning> listplanning = Jsongetsoap.list.planning;
        if (listplanning.Count > 0)
        {
            foreach (Planning x in listplanning)
            {
                if (x.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
                {
                    if (x.remarks != "")
                    {
                        lblPlanning.Text = x.remarks;
                        flagplanning = 1;
                    }
                    else
                        lblPlanning.Text = "";
                }
            }
        }

        if (flagplanning == 0)
            chkPlanning.Enabled = false;
        else
            chkPlanning.Checked = true;


        int flaglab = 0;
        chkLab.Enabled = true;
        chkLab.Checked = false;

        int flagrad = 0;
        chkRad.Enabled = true;
        chkRad.Checked = false;
        List<CpoeTrans> listcpoetrans = new List<CpoeTrans>();
        listcpoetrans = Jsongetsoap.list.cpoe_trans;
        if (listcpoetrans.Count > 0)
        {
            var data = (
                                from a in listcpoetrans
                                where (a.type == "ClinicLab" || a.type == "CitoLab" || a.type == "MicroLab") && a.isdelete == 0
                                select a
                           ).Distinct().ToList();
            if (data.Count > 0)
            {
                flaglab = 1;
                DataTable dt = Helper.ToDataTable(data);
                rptLab.DataSource = dt;
                rptLab.DataBind();
            }
            else
            {
                rptLab.DataSource = null;
                rptLab.DataBind();
            }

            if (flaglab == 0)
                chkLab.Enabled = false;
            else
                chkLab.Checked = true;

            var datarad = (
                                from a in listcpoetrans
                                where (a.type == "MRI1" || a.type == "MRI3" || a.type == "Radiology" || a.type == "USG" || a.type == "CT") && a.isdelete == 0
                                select a
                           ).Distinct().ToList();

            if (datarad.Count == 0)
            {
                rptRad.DataSource = null;
                rptRad.DataBind();
            }
            else
            {
                flagrad = 1;
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
                    listcheckedshow.Add(temp);
                }
                DataTable dtrad = Helper.ToDataTable(listcheckedshow);
                rptRad.DataSource = dtrad;
                rptRad.DataBind();
            }

            if (flagrad == 0)
                chkRad.Enabled = false;
            else
                chkRad.Checked = true;
        }
        else
        {
            rptLab.DataSource = null;
            rptLab.DataBind();

            rptRad.DataSource = null;
            rptRad.DataBind();

            chkLab.Enabled = false;
            chkRad.Enabled = false;
        }

        List<Prescription> listprescription = new List<Prescription>();
        listprescription = Jsongetsoap.list.prescription;

        foreach (var templist in listprescription)
        {
            string a = templist.quantity.ToString().Substring(0, 3);
            string[] tempqty = templist.quantity.ToString().Split('.');
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


            string[] tempdose = templist.dosage_id.ToString().Split('.');

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
        int flagdrug = 0;
        int flagconsumables = 0;
        int flagcompound = 0;
        chkDrugs.Enabled = true;
        chkDrugs.Checked = false;
        chkConsumables.Enabled = true;
        chkConsumables.Checked = false;
        chkCompound.Enabled = true;
        chkCompound.Checked = false;

        if (listprescription.Count > 0)
        {
            if (Helper.ToDataTable(listprescription).Select("compound_id = '00000000-0000-0000-0000-000000000000' and is_delete = 0 and is_consumables = 0").Count() > 0)
            {
                flagdrug = 1;
                DataTable dtpresdrug = Helper.ToDataTable(listprescription).Select("compound_id = '00000000-0000-0000-0000-000000000000' and is_delete = 0 and is_consumables = 0").CopyToDataTable();
                rptDrugs.DataSource = dtpresdrug;
                rptDrugs.DataBind();
            }
            else
            {
                rptDrugs.DataSource = null;
                rptDrugs.DataBind();
            }

            if (flagdrug == 0)
                chkDrugs.Enabled = false;
            else
                chkDrugs.Checked = true;

            ViewState["listpres"] = listprescription;
            if (Helper.ToDataTable(listprescription).Select("item_id = 0").Count() > 0)
            {
                flagcompound = 1;
                DataTable dtcompdrug = Helper.ToDataTable(listprescription).Select("item_id = 0").CopyToDataTable();
                rptCompound.DataSource = dtcompdrug;
                rptCompound.DataBind();
            }
            else
            {
                rptCompound.DataSource = null;
                rptCompound.DataBind();
            }

            ViewState.Remove("listpres");

            if (flagcompound == 0)
                chkCompound.Enabled = false;
            else
                chkCompound.Checked = true;

            if (Helper.ToDataTable(listprescription).Select("is_consumables = 1").Count() > 0)
            {
                flagconsumables = 1;
                DataTable dtconsumables = Helper.ToDataTable(listprescription).Select("is_consumables = 1").CopyToDataTable();
                rptConsumables.DataSource = dtconsumables;
                rptConsumables.DataBind();
            }
            else
            {
                rptConsumables.DataSource = null;
                rptConsumables.DataBind();
            }

            if (flagconsumables == 0)
                chkConsumables.Enabled = false;
            else
                chkConsumables.Checked = true;
        }
        else
        {
            rptDrugs.DataSource = null;
            rptDrugs.DataBind();

            rptConsumables.DataSource = null;
            rptConsumables.DataBind();

            chkDrugs.Enabled = false;
            chkConsumables.Enabled = false;
        }
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var data = ((DataRowView)e.Item.DataItem).Row.ItemArray[18].ToString();
        List<Prescription> listpres = (List<Prescription>)ViewState["listpres"];
        DataTable dtcompdetail = Helper.ToDataTable(listpres).Select("compound_id <> '00000000-0000-0000-0000-000000000000' and item_id <> 0 and compound_name = '" + data + "'").CopyToDataTable();
        var repeater2 = (Repeater)e.Item.FindControl("rptCompDetail");
        repeater2.DataSource = dtcompdetail;
        repeater2.DataBind();
    }

    protected void txtsearchDoctor_onChange(object sender, EventArgs e)
    {
        var getcopysoap = clsSOAP.getCopySOAP(Helper.organizationId, hfPatientId.Value, txtSearchDoctor.Text);
        ResultViewAdmissionCopySOAP Jsongetcopysoap = JsonConvert.DeserializeObject<ResultViewAdmissionCopySOAP>(getcopysoap.Result.ToString());

        if (Jsongetcopysoap.list.Count() > 0)
        {
            DataTable dtcopy = Helper.ToDataTable(Jsongetcopysoap.list);
            gvw_doctor.DataSource = dtcopy;
            gvw_doctor.DataBind();
        }
        else
        {
            gvw_doctor.DataSource = null;
            gvw_doctor.DataBind();
        }
    }

    protected void btnCopySOAP_onClick(object sender, EventArgs e)
    {
        if (hfsoapcopystring.Value != "")
        {
            string rawsoap = hfsoapstring.Value;
            string copysoap = hfsoapcopystring.Value;

            ResultSOAP Jsonrawsoap = JsonConvert.DeserializeObject<ResultSOAP>(rawsoap);
            ResultSOAP Jsoncopysoap = JsonConvert.DeserializeObject<ResultSOAP>(copysoap);

            SOAP result = new SOAP();
            result.encounter_ticket_id = Jsonrawsoap.list.encounter_ticket_id;
            result.organization_id = Jsonrawsoap.list.organization_id;
            result.patient_id = Jsonrawsoap.list.patient_id;
            result.admission_id = Jsonrawsoap.list.admission_id;
            result.doctor_id = Jsonrawsoap.list.doctor_id;
            result.save_mode = Jsonrawsoap.list.save_mode;
            result.take_date = Jsonrawsoap.list.take_date;
            result.pharmacy_notes = Jsonrawsoap.list.pharmacy_notes;
            result.consultation_item_id = Jsonrawsoap.list.consultation_item_id;
            result.consultation_item_name = Jsonrawsoap.list.consultation_item_name;
            result.consultation_amount = Jsonrawsoap.list.consultation_amount;
            result.discount_amount = Jsonrawsoap.list.discount_amount;
            result.total_amount = Jsonrawsoap.list.total_amount;
            result.procedure_notes = Jsonrawsoap.list.procedure_notes;

            if (chkSubjective.Checked)
            {
                var subj = (
                    from raw in Jsonrawsoap.list.subjective
                    join copy in Jsoncopysoap.list.subjective on raw.soap_mapping_id equals copy.soap_mapping_id
                    select new Subjective
                    {
                        subjective_id = raw.subjective_id,
                        soap_mapping_id = raw.soap_mapping_id,
                        soap_mapping_name = raw.soap_mapping_name,
                        remarks = copy.remarks,
                        value = copy.value
                    }
                ).Distinct().ToList();
                result.subjective = subj;
            }
            else
                result.subjective = Jsonrawsoap.list.subjective;

            if (chkObjective.Checked)
            {
                var obj = (
                        from raw in Jsonrawsoap.list.objective
                        join copy in Jsoncopysoap.list.objective on raw.soap_mapping_id equals copy.soap_mapping_id
                        select new Objective
                        {
                            objective_id = raw.objective_id,
                            soap_mapping_id = raw.soap_mapping_id,
                            soap_mapping_name = raw.soap_mapping_name,
                            remarks = copy.remarks,
                            value = copy.value
                        }
                    ).Distinct().ToList();
                result.objective = obj;
            }
            else
                result.objective = Jsonrawsoap.list.objective;

            if (chkAssessment.Checked)
            {
                var assessment = (
                        from raw in Jsonrawsoap.list.assessment
                        join copy in Jsoncopysoap.list.assessment on raw.soap_mapping_id equals copy.soap_mapping_id
                        select new Assessment
                        {
                            assessment_id = raw.assessment_id,
                            soap_mapping_id = raw.soap_mapping_id,
                            soap_mapping_name = raw.soap_mapping_name,
                            remarks = copy.remarks,
                            value = copy.value
                        }
                    ).Distinct().ToList();
                result.assessment = assessment;
            }
            else
                result.assessment = Jsonrawsoap.list.assessment;

            if (chkPlanning.Checked)
            {
                var plan = (
                        from raw in Jsonrawsoap.list.planning
                        join copy in Jsoncopysoap.list.planning on raw.soap_mapping_id equals copy.soap_mapping_id
                        select new Planning
                        {
                            planning_id = raw.planning_id,
                            soap_mapping_id = raw.soap_mapping_id,
                            soap_mapping_name = raw.soap_mapping_name,
                            remarks = copy.remarks,
                            value = copy.value
                        }
                    ).Distinct().ToList();
                result.planning = plan;
            }
            else
                result.planning = Jsonrawsoap.list.planning;

            List<CpoeTrans> temp = new List<CpoeTrans>();
            if (chkLab.Checked)
            {
                foreach (var x in Jsoncopysoap.list.cpoe_trans)
                {
                    if (x.type == "ClinicLab" || x.type == "CitoLab" || x.type == "MicroLab")
                    {
                        x.isnew = 1;
                        x.issubmit = 0;
                        temp.Add(x);
                    }
                }
            }
            else
            {
                foreach (var x in Jsonrawsoap.list.cpoe_trans)
                {
                    if (x.type == "ClinicLab" || x.type == "CitoLab" || x.type == "MicroLab")
                    {
                        temp.Add(x);
                    }
                }
            }

            if (chkRad.Checked)
            {
                foreach (var x in Jsoncopysoap.list.cpoe_trans)
                {
                    if (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT")
                    {
                        x.isnew = 1;
                        x.issubmit = 0;
                        temp.Add(x);
                    }
                }
            }
            else
            {
                foreach (var x in Jsonrawsoap.list.cpoe_trans)
                {
                    if (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT")
                    {
                        temp.Add(x);
                    }
                }
            }
            result.cpoe_trans = temp;
            result.cpoe_notes = Jsonrawsoap.list.cpoe_notes;

            List<Prescription> drugstemp = new List<Prescription>();
            if (chkDrugs.Checked)
            {
                foreach (var x in Jsoncopysoap.list.prescription)
                {
                    if (x.compound_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_consumables == 0 && x.is_delete == 0)
                    {
                        x.prescription_id = Guid.Empty;
                        drugstemp.Add(x);
                    }
                }
            }
            else
            {
                foreach (var x in Jsonrawsoap.list.prescription)
                {
                    if (x.compound_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_consumables == 0 && x.is_delete == 0)
                    {
                        drugstemp.Add(x);
                    }
                }
            }

            if (chkCompound.Checked)
            {
                foreach (var x in Jsoncopysoap.list.prescription)
                {
                    if (x.compound_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_consumables == 0 && x.is_delete == 0)
                    {
                        x.prescription_id = Guid.Empty;
                        drugstemp.Add(x);
                    }
                }
            }
            else
            {
                foreach (var x in Jsonrawsoap.list.prescription)
                {
                    if (x.compound_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_consumables == 0 && x.is_delete == 0)
                    {
                        x.prescription_id = Guid.Empty;
                        drugstemp.Add(x);
                    }
                }
            }

            if (chkConsumables.Checked)
            {
                foreach (var x in Jsoncopysoap.list.prescription)
                {
                    if (x.is_consumables == 1)
                    {
                        x.prescription_id = Guid.Empty;
                        drugstemp.Add(x);
                    }
                }
            }
            else
            {
                foreach (var x in Jsonrawsoap.list.prescription)
                {
                    if (x.is_consumables == 1)
                    {
                        drugstemp.Add(x);
                    }
                }
            }

            result.prescription = drugstemp;


            result.patient_allergy = Jsonrawsoap.list.patient_allergy;
            result.patient_surgery = Jsonrawsoap.list.patient_surgery;
            result.patient_disease = Jsonrawsoap.list.patient_disease;
            result.patient_medication = Jsonrawsoap.list.patient_medication;


            Session[Helper.Sessionsoapmodel] = result;

            StdSubjective.initializevalue(result.subjective, result.patient_disease, result.patient_surgery, result.patient_allergy, result.patient_medication);
            StdObjective.initializevalue(result.objective);
            ResultPatientHeader JsongetPatientHistory = JsonConvert.DeserializeObject<ResultPatientHeader>(hfheader.Value);
            ResultPatientHistory Jsongetallergy = JsonConvert.DeserializeObject<ResultPatientHistory>(hfallergy.Value);

            DataTable dtAllergy = Helper.ToDataTable(Jsongetallergy.list.allergy);
            StdPlanning.initializevaluecopy(result, JsongetPatientHistory.header, dtAllergy, Jsongetallergy.list.currentmedication,"");
            StdAssessment.initializevalue(result.assessment);

            List<Subjective> listsubjective = result.subjective;
            if (listsubjective.Count > 0)
            {
                foreach (Subjective x in listsubjective)
                {
                    if (x.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0"))
                    {//anamnesis
                        Anamnesis.Text = x.remarks;
                    }
                    else if (x.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf"))
                    {//patient complaint
                        if (x.remarks != "")
                        {
                            Complaint.Text = x.remarks;
                        }
                    }
                    else if (x.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923"))
                    {//pregnancy
                        if (x.value == "True")
                        {
                            chkpregnant.Checked = true;
                        }
                        else
                        {
                            chkpregnant.Checked = false;
                        }
                    }
                    else if (x.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937"))
                    {//breast feeding
                        if (x.value == "True")
                        {
                            chkbreastfeed.Checked = true;
                        }
                        else
                        {
                            chkbreastfeed.Checked = false;
                        }
                    }
                }
            }

            List<Planning> listplanning = result.planning;
            if (listplanning.Count > 0)
            {
                foreach (Planning x in listplanning)
                {
                    if (x.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
                    {
                        txtPlanning.Text = x.remarks;
                    }
                }
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCopySOAP", "$('#modalCopySOAP').modal('hide');", true);
    }
    
}