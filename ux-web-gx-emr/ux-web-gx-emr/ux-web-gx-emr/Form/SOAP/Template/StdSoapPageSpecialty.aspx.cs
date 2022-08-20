using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using static PatientHistory;
using static FirstAssesment;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Web.Services;
using static SPObgyn;
using static SPPediatric;
using System.Configuration;
using System.Web;
using static PatientReferralModel;

public partial class Form_SOAP_Template_StdSoapPageSpecialty : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    //public static List<CurrentMedication> tempcurrmedication = new List<CurrentMedication>();

    public string setENG = "none";
    public string setIND = "none";

    void ShowToastr(string message, string title, string type)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastr_message",
             String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
    }

    //protected void Page_Unload(object sender, EventArgs e)
    //{
    //    AutoSaveSOAP();
    //}

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
            HFisBahasaSOAP.Value = "ENG";
        }
        else if (Session[Helper.SessionLanguage].ToString() == "2")
        {
            //HiddenFieldENG.Value = "False";
            //HiddenFieldIND.Value = "True";
            setENG = "none";
            setIND = "";
            HFisBahasaSOAP.Value = "IND";
        }
        else
        {
            //HiddenFieldENG.Value = "True";
            //HiddenFieldIND.Value = "False";
            setENG = "";
            setIND = "none";
            HFisBahasaSOAP.Value = "ENG";
        }

        //set bahasa
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "bahasa", "switchBahasaSOAP();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "bahasaPC", "switchBahasaPC();", true);

        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            if (Session[Helper.SESSIONmarker] == null)
            {
                Session[Helper.SESSIONmarker] = Helper.setInitializeMarker();
            }

            Session[Helper.SessionLabPathologyChecked] = null;
            Session[Helper.Sessionroutinemedication] = null;
            Session[Helper.Sessionradcheck] = null;
            Session[Helper.Sessionpolidata] = null;
            Session[Helper.Sessionsoapmodel] = null;
            HyperLink test = Master.FindControl("SOAPLink") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");

            Complaint.Focus();

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
                Dictionary<string, string> logParam = new Dictionary<string, string>();

                Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], Request.QueryString["PagefaId"], Request.QueryString["PageSoapId"], Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
                hfPatientId.Value = Request.QueryString["idPatient"];
                hfEncounterId.Value = Request.QueryString["EncounterId"];
                hfAdmissionId.Value = Request.QueryString["AdmissionId"];
                hfPageSoapId.Value = Request.QueryString["PageSoapId"];

                hfguidadditional.Value = Guid.NewGuid().ToString();

                string flagcompound = Helper.GetFlagCompound(this);

                logParam = new Dictionary<string, string>
                {
                    { "Patient_ID", hfPatientId.Value },
                    { "Encounter_ID", hfEncounterId.Value }
                };
                //log.debug(logconfig.LogStart("GetPatientHeader", logParam));
                var varResult = clsCommon.GetPatientHeader(long.Parse(hfPatientId.Value), hfEncounterId.Value.ToString());
                var JsongetPatientHistory = JsonConvert.DeserializeObject<ResponsePatientHeader>(varResult.Result.ToString());
                //log.debug(logconfig.LogEnd("GetRecapIPD", JsongetPatientHistory.Status, JsongetPatientHistory.Message));

                hfheader.Value = varResult.Result.ToString();

                //var getsoap = clsSOAP.getSOAP(hfEncounterId.Value, long.Parse(hfPatientId.Value), long.Parse(hfAdmissionId.Value), Helper.organizationId, long.Parse(Helper.GetDoctorID(this)));
                //ResultSOAP Jsongetsoap = JsonConvert.DeserializeObject<ResultSOAP>(getsoap.Result.ToString());
                var Jsongetsoap = MappingforGetdataSOAP();

                List<MarkerConfig> markerbackup = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                string BackupSOAPmarker = markerbackup.Find(x => x.key == "BACKUPSOAPmarker").value;
                //for backup data on session
                if (Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]] != null)
                {
                    if (BackupSOAPmarker == "false")
                    {
                        var tempSOAPOriginal = Jsongetsoap.list;
                        Jsongetsoap.list.BackupDate = Jsongetsoap.list.ModifiedDate;
                        //versi session
                        var tempSOAPBackup = MappingforGetdataSOAPSessionBAckup();
                        CompareSOAPView.initializevalue(tempSOAPOriginal, tempSOAPBackup);
                        
                        ////versi localstorage
                        ////ScriptManager.RegisterStartupScript(this, GetType(), "getlocal", "GetSOAPfromLocal();", true);
                        //if (hfsoapstringgetfromlocal.Value != "")
                        //{
                        //    SOAP tempSOAPBackupStorage = new JavaScriptSerializer().Deserialize<SOAP>(hfsoapstringgetfromlocal.Value);
                        //    CompareSOAPView.initializevalue(tempSOAPOriginal, tempSOAPBackupStorage);
                        //}
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openmodalcompare", "$('#modalCompareSOAP').modal('show');", true);
                    }
                    else if (BackupSOAPmarker == "true")
                    {
                        //versi session
                        var tempSOAPBackup = MappingforGetdataSOAPSessionBAckup();
                        Jsongetsoap.list = tempSOAPBackup;

                        ////versi localstorage
                        //SOAP tempSOAPBackupStorage = new JavaScriptSerializer().Deserialize<SOAP>(hfsoapstringgetfromlocal.Value);
                        //Jsongetsoap.list = tempSOAPBackupStorage;
                    }
                    markerbackup.Find(x => x.key == "BACKUPSOAPmarker").value = "false";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SetConfirmbackup", "var getbackup = false;", true);
                }

                if (Jsongetsoap.list.take_date != null)
                {
                    hftakedate.Value = Jsongetsoap.list.take_date.ToString();
                }
                if (Jsongetsoap.list.additional_take_date != null)
                {
                    hfadditionaltakedate.Value = Jsongetsoap.list.additional_take_date.ToString();
                }

                //reminder notes
                List<PatientSpecialNotification> reminderlist = Jsongetsoap.list.patient_notification;

                if (reminderlist != null && reminderlist.Count > 0)
                {
                    hdnremindernotes.Value = new JavaScriptSerializer().Serialize(reminderlist);

                    DataTable dtreminder = Helper.ToDataTable(reminderlist);
                    dtreminder.DefaultView.Sort = "start_date ASC";
                    dtreminder = dtreminder.DefaultView.ToTable();

                    gvw_remindernotes.DataSource = dtreminder;
                    gvw_remindernotes.DataBind();

                    divtablereminder.Style.Add("display", "");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScriptProcout", "ShowHideDivProcout();", true);
                }
                else
                {
                    hdnremindernotes.Value = "";

                    gvw_remindernotes.DataSource = null;
                    gvw_remindernotes.DataBind();

                    divtablereminder.Style.Add("display", "none");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScriptProcout", "HideDivProcout();", true);
                }

                //masih tanda tanya kenapa allergy ambil dari sini
                //logParam = new Dictionary<string, string>
                //{
                //    { "Patient_ID", hfPatientId.Value },
                //    { "Encounter_ID", hfEncounterId.Value }
                //};
                //log.debug(logconfig.LogStart("GetPatientHistory", logParam));
                //var varallergy = clsPatientDetail.GetPatientHistory(long.Parse(hfPatientId.Value), hfEncounterId.Value);
                //ResultPatientHistory Jsongetallergy = JsonConvert.DeserializeObject<ResultPatientHistory>(varallergy.Result.ToString());
                //log.debug(logconfig.LogEnd("GetPatientHistory", Jsongetallergy.Status, Jsongetallergy.Message));

                //DataTable vardiseaseclassification = clsSOAP.getDiseaseClassification("");
                //vardiseaseclassification.DefaultView.Sort = "DiseaseClassification";
                //dibuka lagi jika kembali ke mode pencarian lama
                //gvw_icd.DataSource = vardiseaseclassification;
                //gvw_icd.DataBind();


                //var PatientAllergy = Jsongetallergy.list.allergy;
                var PatientAllergy = Jsongetsoap.list.patient_allergy;
                DataTable dtAllergy = Helper.ToDataTable(PatientAllergy);

                //hfallergy.Value = varallergy.Result.ToString();
                //getrawat inap
                PatientHeader headeradm = JsongetPatientHistory.Data;

                try
                {
                    var getrawatinap = clsSOAP.getRawatInap(long.Parse(Helper.organizationId.ToString()), Guid.Empty.ToString(), long.Parse(hfPatientId.Value), headeradm.AdmissionNo, hfEncounterId.Value);
                    ResultInpatient jsoninpatient = JsonConvert.DeserializeObject<ResultInpatient>(getrawatinap.Result.ToString());
                    if (jsoninpatient.list.patient_id != null)
                    {

                        //filter dokter
                        DataTable dt = (DataTable)Session[Helper.SessionListOrganization];
                        List<DoctorOrg> datadoctororgobj = (List<DoctorOrg>)Session[Helper.SessionDoctorByOrg];
                        try
                        {

                            var getDoctorbyOrg = clsSOAP.getDoctorByOrg(dt.Rows[0]["mobile_organization_id"].ToString());
                            var DoctorOrgJson = JsonConvert.DeserializeObject<ResultDoctorOrg>(getDoctorbyOrg.Result.ToString());
                            datadoctororgobj = DoctorOrgJson.data;
                        }
                        catch (Exception ex)
                        {
                            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetFilterDokterSOAPInpatient", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
                        }

                        InpatientData inpatientData = (InpatientData)Session[Helper.SessionSOAPRawatInap + hfguidadditional.Value];
                        inpatientData = jsoninpatient.list;
                        Session[Helper.SessionSOAPRawatInap + hfguidadditional.Value] = inpatientData;


                        var templistdokter = datadoctororgobj.Where(x => x.doctor_hope_id == Convert.ToInt64(inpatientData.doctor_id)).ToList();
                        foreach (DoctorOrg a in templistdokter)
                        {
                            inpatientData.spesialis_dokter = a.specialization_name;
                        }

                        if (string.IsNullOrEmpty(inpatientData.operation_schedule_header.status_booking_name))
                        {
                            stickerinpatient.Visible = false;
                            lbl_rawatinap_status.Visible = false;
                        }
                        else
                        {
                            stickerinpatient.Visible = true;
                            lbl_rawatinap_status.Visible = true;
                        }

                        lnkModalrawatinap.Disabled = true;
                        lnkModalrawatinap.Style.Add("background-color", "#B9B9B9");
                        lnkModalrawatinap.Attributes.Remove("href");



                        lbl_rawatinap_dokter.Text = inpatientData.doctor_name;
                        lbl_rawatinap_spesialis.Text = inpatientData.spesialis_dokter;
                        //DateTime date = DateTime.ParseExact(inpatientData.operation_schedule_header.created_date, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        lbl_rawatinap_waktu.Text = inpatientData.created_date;
                        lbl_rawatinap_status.Text = inpatientData.operation_schedule_header.status_booking_name;


                    }
                }
                catch (Exception ex)
                {
                    string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetInpatientData", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
                }




                DataTable pagedatadt;
                if (Session[Helper.Sessionpolidata + hfguidadditional.Value] == null)
                {
                    List<PageSpecialty> listpagedata = new List<PageSpecialty>();

                    logParam = new Dictionary<string, string>
                    {
                        { "Doctor_ID", Helper.GetDoctorID(this) },
                        { "Org_ID", "1" }
                    };
                    //log.debug(logconfig.LogStart("GetPageSpecialty", logParam));
                    var pagedata = clsFirstAssesment.GetPageSpecialty(long.Parse(Helper.GetDoctorID(this)), 1);
                    //var pagedata = clsFirstAssesment.GetPageSpecialty(long.Parse(Helper.GetDoctorID(this)), Helper.organizationId);
                    var Jsonpagedata = JsonConvert.DeserializeObject<ResultPageSpecialty>(pagedata.Result.ToString());
                    //log.debug(logconfig.LogEnd("GetPageSpecialty", Jsonpagedata.Status, Jsonpagedata.Message));

                    //listpagedata = Jsonpagedata.list.Where(y => y.page_specialty_name == "GENERAL PRACTITIONER").ToList();
                    listpagedata = Jsonpagedata.list;

                    pagedatadt = Helper.ToDataTable(listpagedata);
                    Session[Helper.Sessionpolidata + hfguidadditional.Value] = pagedatadt;
                }
                else
                    pagedatadt = (DataTable)Session[Helper.Sessionpolidata + hfguidadditional.Value];


                PatientHeader header = JsongetPatientHistory.Data;

                PatientCard.initializevalue(header);
                PatientCardModal.initializevalue(header);
                PatientCardRefModal.initializevalue(header);
                PatientCardRawatinapModal.initializevalue(header);

                hfgender.Value = header.Gender.ToString();
                hfage.Value = clsCommon.GetAge(header.BirthDate).Substring(0, 2);

                if (header.Gender == 1)
                {
                    divpregnat.Visible = false;
                }
                else if (header.Gender == 2)
                {
                    divpregnat.Visible = true;
                }

                //get all item drugs
                //if (Session[Helper.SessionItemDrugPres] == null)
                //{

                logParam = new Dictionary<string, string>
                {
                    { "Organization_ID", Helper.organizationId.ToString() },
                    { "Admission_ID", header.AdmissionTypeId.ToString() }
                };
                //log.debug(logconfig.LogStart("getItemPres", logParam));
                DataTable dtitempres = clsSOAP.getItemPres(Helper.organizationId, header.AdmissionTypeId);
                Session[Helper.SessionItemDrugPres] = dtitempres;
                //log.debug(logconfig.LogEnd());
                //}

                //get all item cpoe
                //log.debug(logconfig.LogStart("GetAllItemCPOE", LogConfig.LogParam("Organization_ID", Helper.organizationId.ToString())));
                var itemcpoe = clsCpoeMapping.GetAllItemCPOE(Helper.organizationId);
                var Jsonitemcpoe = JsonConvert.DeserializeObject<ResultViewItemCPOE>(itemcpoe.Result.ToString());
                //log.debug(logconfig.LogEnd("GetAllItemCPOE", Jsonitemcpoe.Status, Jsonitemcpoe.Message));

                DataTable dtitemcpoe = Helper.ToDataTable(Jsonitemcpoe.list);
                Session[Helper.SessionItemAllCPOE] = dtitemcpoe;

                //get all item procedure/diagnostic
                //log.debug(logconfig.LogStart("getDiagnosticProcedure", LogConfig.LogParam("Organization_ID", Helper.organizationId.ToString())));
                DataTable dtitemdiagproc = clsSOAP.getDiagnosticProcedure(Helper.organizationId);
                Session[Helper.SessionItemDiagProc] = dtitemdiagproc;
                //log.debug(logconfig.LogEnd());

                //get spesific template
                logParam = new Dictionary<string, string>
                {
                    { "Doctor_ID", Helper.doctorid.ToString() },
                    { "Mapping_ID", "E851F782-8210-49EB-A074-F26C104F5DDF" }
                };
                //log.debug(logconfig.LogStart("getSpesificTemplateSet", logParam));
                var itemtemplateSCC = clsTemplateSet.getSpesificTemplateSet(Helper.doctorid, Guid.Parse("E851F782-8210-49EB-A074-F26C104F5DDF"));
                var JsontemplateSCC = JsonConvert.DeserializeObject<ResultSpesificTemplateSet>(itemtemplateSCC.Result.ToString());
                //log.debug(logconfig.LogEnd("getSpesificTemplateSet", JsontemplateSCC.Status, JsontemplateSCC.Message));

                if (JsontemplateSCC != null)
                {
                    DataTable dttemplateSCC = Helper.ToDataTable(JsontemplateSCC.list);
                    RepeaterSCC.DataSource = dttemplateSCC;
                    RepeaterSCC.DataBind();
                    HFcounteritemSCC.Value = dttemplateSCC.Rows.Count.ToString();
                }
                else
                {
                    RepeaterSCC.DataSource = null;
                    RepeaterSCC.DataBind();
                }

                logParam = new Dictionary<string, string>
                {
                    { "Doctor_ID", Helper.doctorid.ToString() },
                    { "Mapping_ID", "2874A832-8503-4CAD-B5DD-535775E94AC0" }
                };
                var itemtemplateSA = clsTemplateSet.getSpesificTemplateSet(Helper.doctorid, Guid.Parse("2874A832-8503-4CAD-B5DD-535775E94AC0"));
                var JsontemplateSA = JsonConvert.DeserializeObject<ResultSpesificTemplateSet>(itemtemplateSA.Result.ToString());
                //log.debug(logconfig.LogEnd("getSpesificTemplateSet", JsontemplateSA.Status, JsontemplateSA.Message));

                if (JsontemplateSA != null)
                {
                    DataTable dttemplateSA = Helper.ToDataTable(JsontemplateSA.list);
                    RepeaterSA.DataSource = dttemplateSA;
                    RepeaterSA.DataBind();
                    HFcounteritemSA.Value = dttemplateSA.Rows.Count.ToString();
                }
                else
                {
                    RepeaterSA.DataSource = null;
                    RepeaterSA.DataBind();
                }

                logParam = new Dictionary<string, string>
                {
                    { "Doctor_ID", Helper.doctorid.ToString() },
                    { "Mapping_ID", "7218971C-E89F-4172-AE3C-B7FB855C1D6D" }
                };
                //log.debug(logconfig.LogStart("getSpesificTemplateSet", logParam));
                var itemtemplateO = clsTemplateSet.getSpesificTemplateSet(Helper.doctorid, Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D"));
                var JsontemplateO = JsonConvert.DeserializeObject<ResultSpesificTemplateSet>(itemtemplateO.Result.ToString());
                //log.debug(logconfig.LogEnd("getSpesificTemplateSet", JsontemplateO.Status, JsontemplateO.Message));

                if (JsontemplateO != null)
                {
                    DataTable dttemplateO = Helper.ToDataTable(JsontemplateO.list);
                    RepeaterO.DataSource = dttemplateO;
                    RepeaterO.DataBind();
                    HFcounteritemO.Value = dttemplateO.Rows.Count.ToString();

                    var srcddlO = JsontemplateO.list.OrderBy(n => n.template_name).ToList();

                    ddlO.Items.Add(new ListItem("- select -", "-"));
                    ddlO.Items.Add(new ListItem("All Normal", "All Normal"));
                    for (int i = 0; i < JsontemplateO.list.Count(); i++)
                    {
                        ddlO.Items.Add(new ListItem(srcddlO[i].template_name, srcddlO[i].template_remarks));
                    }
                }
                else
                {
                    RepeaterO.DataSource = null;
                    RepeaterO.DataBind();

                    ddlO.Items.Add(new ListItem("- select -", "-"));
                    ddlO.Items.Add(new ListItem("All Normal", "All Normal"));
                }

                logParam = new Dictionary<string, string>
                {
                    { "Doctor_ID", Helper.doctorid.ToString() },
                    { "Mapping_ID", "D24D0881-7C06-4563-BF75-3A20B843DC47" }
                };
                //log.debug(logconfig.LogStart("getSpesificTemplateSet", logParam));
                var itemtemplateA = clsTemplateSet.getSpesificTemplateSet(Helper.doctorid, Guid.Parse("D24D0881-7C06-4563-BF75-3A20B843DC47"));
                var JsontemplateA = JsonConvert.DeserializeObject<ResultSpesificTemplateSet>(itemtemplateA.Result.ToString());
                //log.debug(logconfig.LogEnd("getSpesificTemplateSet", JsontemplateA.Status, JsontemplateA.Message));

                if (JsontemplateA != null)
                {
                    DataTable dttemplateA = Helper.ToDataTable(JsontemplateA.list);
                    RepeaterA.DataSource = dttemplateA;
                    RepeaterA.DataBind();
                    HFcounteritemA.Value = dttemplateA.Rows.Count.ToString();
                }
                else
                {
                    RepeaterA.DataSource = null;
                    RepeaterA.DataBind();
                }

                logParam = new Dictionary<string, string>
                {
                    { "Doctor_ID", Helper.doctorid.ToString() },
                    { "Mapping_ID", "337A371F-BAF5-424A-BDC5-C320C277CAC6" }
                };
                //log.debug(logconfig.LogStart("getSpesificTemplateSet", logParam));
                var itemtemplateP = clsTemplateSet.getSpesificTemplateSet(Helper.doctorid, Guid.Parse("337A371F-BAF5-424A-BDC5-C320C277CAC6"));
                var JsontemplateP = JsonConvert.DeserializeObject<ResultSpesificTemplateSet>(itemtemplateP.Result.ToString());
                //log.debug(logconfig.LogEnd("getSpesificTemplateSet", JsontemplateP.Status, JsontemplateP.Message));

                if (JsontemplateP != null)
                {
                    DataTable dttemplateP = Helper.ToDataTable(JsontemplateP.list);
                    RepeaterP.DataSource = dttemplateP;
                    RepeaterP.DataBind();
                    HFcounteritemP.Value = dttemplateP.Rows.Count.ToString();
                }
                else
                {
                    RepeaterP.DataSource = null;
                    RepeaterP.DataBind();
                }

                ddlForm_Type.DataSource = pagedatadt;
                ddlForm_Type.DataTextField = "page_specialty_name";
                ddlForm_Type.DataValueField = "page_specialty_id";
                ddlForm_Type.DataBind();
                ddlForm_Type.Items.Add(new ListItem("EMERGENCY SOAP", "711671b0-a2b3-4311-9b89-69c146fdae3a"));

                hfsavemode.Value = Jsongetsoap.list.save_mode.ToString();
                List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
                if (Jsongetsoap.list.save_mode == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SetConfirm", "var shouldsubmit = false;", true);

                    logParam = new Dictionary<string, string>
                    {
                        { "Doctor_ID", Helper.doctorid.ToString() },
                        { "Organization_ID", Helper.organizationId.ToString() },
                        { "Admission_ID", hfAdmissionId.Value },
                        { "Encounter_ID", hfEncounterId.Value }
                    };
                    //log.debug(logconfig.LogStart("GetConsultationFee", logParam));
                    var varResultconsultation = clsSOAP.GetConsultationFee(long.Parse(Helper.GetDoctorID(this)), Helper.organizationId, long.Parse(hfAdmissionId.Value), Guid.Parse(hfEncounterId.Value));
                    ResultConsultFee JsongetResultconsultation = JsonConvert.DeserializeObject<ResultConsultFee>(varResultconsultation.Result.ToString());
                    //log.debug(logconfig.LogEnd("GetConsultationFee", JsongetResultconsultation.Status, JsongetResultconsultation.Message));

                    logParam = new Dictionary<string, string>
                    {
                        { "Organization_ID", Helper.organizationId.ToString() },
                        { "Patient_ID", hfPatientId.Value },
                        { "Keyword", "" }
                    };
                    //log.debug(logconfig.LogStart("getCopySOAP", logParam));
                    var getcopysoap = clsSOAP.getCopySOAP(Helper.organizationId, hfPatientId.Value, "");
                    //var getcopysoap = clsSOAP.getCopySOAPDoctor(Helper.organizationId, hfPatientId.Value, long.Parse(MyUser.GetHopeUserID()));
                    ResultViewAdmissionCopySOAP Jsongetcopysoap = JsonConvert.DeserializeObject<ResultViewAdmissionCopySOAP>(getcopysoap.Result.ToString());
                    //log.debug(logconfig.LogEnd("getCopySOAP", Jsongetcopysoap.Status, Jsongetcopysoap.Message));


                    if (orgsetting.Find(y => y.setting_name.ToUpper() == "PRESCRIPTION_HOPE".ToUpper()).setting_value == "TRUE")
                    {
                        hfprescriptionHOPE.Value = "TRUE";
                        logParam = new Dictionary<string, string>
                        {
                            { "Patient_ID", hfPatientId.Value },
                            { "Organization_ID", "7" }
                        };
                        //log.debug(logconfig.LogStart("getCopyPrescription", logParam));
                        //var getcopyprescription = clsSOAP.getCopyPrescription(hfPatientId.Value, Helper.organizationId);
                        var getcopyprescription = clsSOAP.getCopyPrescription(hfPatientId.Value, 7);
                        ResultAdmissionPrescriptionHOPE Jsongetcopyprescription = JsonConvert.DeserializeObject<ResultAdmissionPrescriptionHOPE>(getcopyprescription.Result.ToString());
                        //log.debug(logconfig.LogEnd("getCopyPrescription", Jsongetcopyprescription.Status, Jsongetcopyprescription.Message));

                        if (Jsongetcopyprescription.list.Count() > 0)
                        {
                            DataTable dtcopypres = Helper.ToDataTable(Jsongetcopyprescription.list);
                            gvw_doctorhope.DataSource = dtcopypres;
                            gvw_doctorhope.DataBind();
                        }
                        else
                        {
                            gvw_doctorhope.DataSource = null;
                            gvw_doctorhope.DataBind();
                        }
                    }

                    if (Helper.GetFlagCompound(this) == "FALSE")
                        chkCompound.Enabled = false;
                    else
                        chkCompound.Enabled = true;

                    List<ConsultFeestring> consfee = new List<ConsultFeestring>();
                    if (JsongetResultconsultation.list.Count > 0)
                    {
                        foreach (var x in JsongetResultconsultation.list)
                        {
                            ConsultFeestring temp = new ConsultFeestring();
                            string formatfee = String.Format("{0:n0}", x.consultation_fee).Replace('.', ',');
                            temp.consultation_fee = x.consultation_fee.ToString();
                            temp.sales_item_id = x.sales_item_id;
                            temp.sales_item_name = x.sales_item_name;
                            temp.consulation_fee_name = x.sales_item_name + " ~ Rp " + formatfee;
                            consfee.Add(temp);
                        }
                    }

                    ddl_consultationfee.DataSource = Helper.ToDataTable(consfee);
                    ddl_consultationfee.DataTextField = "consulation_fee_name";
                    ddl_consultationfee.DataValueField = "sales_item_id";
                    ddl_consultationfee.DataBind();


                    string[] totalfeeformat = ddl_consultationfee.SelectedItem.Text.Split(new string[] { " ~ Rp " }, StringSplitOptions.None);
                    string totalfee = totalfeeformat[1];
                    txttotalfee.Text = totalfee;

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
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SetConfirm", "var shouldsubmit = true;", true);
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


                    hfitemid.Value = Jsongetsoap.list.consultation_item_id.ToString();
                    hfitemname.Value = Jsongetsoap.list.consultation_item_name;
                    hfconsfee.Value = Jsongetsoap.list.consultation_amount.ToString();
                    hfdiscamount.Value = Jsongetsoap.list.discount_amount.ToString();
                }

                List<ProcedureDiagnosis> datadiagproc = Jsongetsoap.list.procedure_diagnosis;
                /// this Grid data has been moved to outside of modal
                //DataTable diagproc_data = Helper.ToDataTable(datadiagproc);
                //Gvw_submitdiagnosticprocedure_dis.DataSource = diagproc_data;
                //Gvw_submitdiagnosticprocedure_dis.DataBind();
                //Gvw_submitdiagnosticprocedure.DataSource = diagproc_data;
                //Gvw_submitdiagnosticprocedure.DataBind();

                Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value] = datadiagproc;
                //if (datadiagproc.Count > 0)
                //{
                //    divdiagproc_en.Visible = true;
                //}
                //else
                //{
                //    divdiagproc_en.Visible = false;
                //}

                /// Modal diagnostik dan procedur sudah tidak melalui modal lagi
                //if (datadiagproc.Count > 0)
                //{
                //    divdiagproc_dis.Visible = true;
                //}
                //else
                //{
                //    divdiagproc_dis.Visible = false;
                //}

                if (hfPageSoapId.Value == "00000000-0000-0000-0000-000000000000")
                {
                    ddlForm_Type.SelectedValue = "7ccd0a7e-9001-48ff-8052-ed07cf5716bb";
                }
                else
                    ddlForm_Type.SelectedValue = hfPageSoapId.Value;


                ScriptManager.RegisterStartupScript(this, this.GetType(), "hidecopy", "hidecopy();", true);
                if (orgsetting.Find(y => y.setting_name.ToUpper() == "COPY_SOAP".ToUpper()).setting_value == "FALSE")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hidebtncopy", "hidecopysoapbutton();", true);
                }

                Session[Helper.Sessionsoapmodel + hfguidadditional.Value] = Jsongetsoap.list;
                
                //tempcurrmedication = Jsongetallergy.list.currentmedication;
                List<PatientRoutineMedication> temproutinemed = Jsongetsoap.list.patient_medication;
                Session[Helper.Sessionroutinemedication + hfguidadditional.Value] = temproutinemed;
                StdPlanning.initializevalue(Jsongetsoap, header, dtAllergy, temproutinemed, hfguidadditional.Value);
                StdImunisasi.initializevalue(Jsongetsoap, header, hfguidadditional.Value);
                if (Jsongetsoap.list.referal != null)
                {
                    for (int i = 0; i < Jsongetsoap.list.referal.Count; i++)
                    {
                        Jsongetsoap.list.referal[i].created_date = Jsongetsoap.list.ModifiedDate;
                    }
                    Session[Helper.SessionSOAPReferral + hfguidadditional.Value] = Jsongetsoap.list.referal;
                    //ModalReferal.initializevalue(Jsongetsoap.list.referal);
                }
                else
                {
                    Jsongetsoap.list.referal = new List<ReferalData>();
                    //ModalReferal.initializevalue(Jsongetsoap.list.referal);
                }

                //if (orgsetting.Find(y => y.setting_name.ToUpper() == "USE_REFERRAL".ToUpper()).setting_value == "TRUE")
                //{
                //    divaddbuttonreferal.Visible = true;
                //    //divaddbuttonrawatinap.Visible = true;
                //}
                //else
                //{
                //    divaddbuttonreferal.Visible = false;
                //    //divaddbuttonrawatinap.Visible = false;
                //}

                int flagtele = 0;
                if (header.ChannelId == "5" || header.ChannelId == "9")
                {
                    string[] settingpayertele = orgsetting.Find(y => y.setting_name.ToUpper() == "MYSILOAM_PAYER_ID".ToUpper()).setting_value.Split(',');
                    for (int i = 0; i < settingpayertele.Length; i++)
                    {
                        if (settingpayertele[i] == JsongetPatientHistory.Data.PayerId.ToString())
                        {
                            flagtele = 1;
                        }
                    }
                }
                else if (header.ChannelId == "18")
                {
                    string[] settingpayertele = orgsetting.Find(y => y.setting_name.ToUpper() == "AIDO_PAYER_ID".ToUpper()).setting_value.Split(',');
                    for (int i = 0; i < settingpayertele.Length; i++)
                    {
                        if (settingpayertele[i] == JsongetPatientHistory.Data.PayerId.ToString())
                        {
                            flagtele = 1;
                        }
                    }
                    //if (orgsetting.Find(y => y.setting_name.ToUpper() == "AIDO_PAYER_ID".ToUpper()).setting_value.Contains(JsongetPatientHistory.header.PayerId.ToString())) //AIDO

                }
                   
                if (flagtele == 1)
                {
                    rbPrice2.Enabled = false;
                    rbPrice3.Enabled = false;
                    //txtItemDiagProc_AC.Enabled = false;
                    //txtItemDiagProc_AC_Dis.Enabled = false;

                    Labelneed.Visible = true;
                    Labelnotneed.Visible = true;
                    divReferalButton.Visible = true;
                    Labelneed2.Visible = true;
                    Labelnotneed2.Visible = true;
                    divReferalButtonDisable.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HideAdditional", "HideAdditional(); HideDivRacikan();", true);
                }

                #region modalFA

                if (temproutinemed.Count() > 0)
                {
                    DataTable dtroutine = Helper.ToDataTable(temproutinemed);
                    if (dtroutine.Rows[0]["medication"].ToString() == "Tidak Ada Pengobatan")
                    {
                        hdnhistoryroutine.Value = new JavaScriptSerializer().Serialize(temproutinemed);

                        rptroutinemedication.DataSource = Helper.ToDataTable(temproutinemed);
                        rptroutinemedication.DataBind();
                        lblmodalnoroute.Style.Add("display", "none");
                        rbPengobatan1.Checked = true;
                        gvw_routinemed.DataSource = null;
                        gvw_routinemed.DataBind();
                    }
                    else
                    {
                        hdnhistoryroutine.Value = new JavaScriptSerializer().Serialize(temproutinemed);

                        rptroutinemedication.DataSource = Helper.ToDataTable(temproutinemed);
                        rptroutinemedication.DataBind();
                        lblmodalnoroute.Style.Add("display", "none");
                        rbPengobatan2.Checked = true;
                        gvw_routinemed.DataSource = Helper.ToDataTable(temproutinemed);
                        gvw_routinemed.DataBind();
                    }
                }
                else
                {
                    hdnhistoryroutine.Value = "";

                    rptroutinemedication.DataSource = null;
                    rptroutinemedication.DataBind();
                    lblmodalnoroute.Style.Add("display", "");
                    //rbPengobatan1.Checked = true;
                    gvw_routinemed.DataSource = null;
                    gvw_routinemed.DataBind();
                }

                if (rbPengobatan2.Checked)
                    hfenableroutine.Value = "1";
                else if (rbPengobatan1.Checked)
                    hfenableroutine.Value = "0";
                else
                    hfenableroutine.Value = "-1";

                List<PatientAllergy> listPatientAllergy = Jsongetsoap.list.patient_allergy;
                List<PatientAllergy> tempdrugs = (from a in listPatientAllergy
                                                  where a.allergy_type == 1
                                                  select a).ToList();
                List<PatientAllergy> tempfoods = (from a in listPatientAllergy
                                                  where a.allergy_type == 2
                                                  select a).ToList();
                List<PatientAllergy> tempothers = (from a in listPatientAllergy
                                                  where a.allergy_type == 7
                                                  select a).ToList();
                hdnhistorydrugallergies.Value = new JavaScriptSerializer().Serialize(tempdrugs);
                hdnhistoryfoodallergies.Value = new JavaScriptSerializer().Serialize(tempfoods);
                hdnhistoryotherallergies.Value = new JavaScriptSerializer().Serialize(tempothers);
                if (listPatientAllergy.Count > 0)
                {//patient allergy
                    if (Helper.ToDataTable(listPatientAllergy).Select("is_delete = 0").Count() > 0)
                    {
                        dtAllergy = Helper.ToDataTable(listPatientAllergy);
                        if (dtAllergy.Select("is_delete = 0 and allergy_type = 1").Count() > 0)
                        {
                            DataTable dtdrug = dtAllergy.Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
                            if (dtdrug.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                            {
                                rptdrugallergies.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
                                rptdrugallergies.DataBind();
                                gvw_allergy.DataSource = null;
                                gvw_allergy.DataBind();
                                lblmodalnodrug.Style.Add("display", "none");
                                rbdrug1.Checked = true;
                            }
                            else
                            {
                                rptdrugallergies.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
                                rptdrugallergies.DataBind();
                                gvw_allergy.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
                                gvw_allergy.DataBind();
                                lblmodalnodrug.Style.Add("display", "none");
                                rbdrug2.Checked = true;
                            }
                        }
                        else
                        {
                            rptdrugallergies.DataSource = null;
                            rptdrugallergies.DataBind();
                            gvw_allergy.DataSource = null;
                            gvw_allergy.DataBind();
                            lblmodalnodrug.Style.Add("display", "");
                            //rbdrug1.Checked = true;
                        }

                        if (dtAllergy.Select("is_delete = 0 and allergy_type = 2").Count() > 0)
                        {
                            DataTable dtfood = dtAllergy.Select("is_delete = 0 and allergy_type = 2").CopyToDataTable();
                            if (dtfood.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                            {
                                rptfoodallergies.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 2").CopyToDataTable();
                                rptfoodallergies.DataBind();
                                gvw_foods.DataSource = null;
                                gvw_foods.DataBind();
                                lblmodalnofood.Style.Add("display", "none");
                                rbfood1.Checked = true;
                            }
                            else
                            {
                                rptfoodallergies.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 2").CopyToDataTable();
                                rptfoodallergies.DataBind();
                                gvw_foods.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 2").CopyToDataTable();
                                gvw_foods.DataBind();
                                lblmodalnofood.Style.Add("display", "none");
                                rbfood2.Checked = true;
                            }
                        }
                        else
                        {
                            rptfoodallergies.DataSource = null;
                            rptfoodallergies.DataBind();
                            gvw_foods.DataSource = null;
                            gvw_foods.DataBind();
                            lblmodalnofood.Style.Add("display", "");
                            //rbfood1.Checked = true;
                        }

                        if (dtAllergy.Select("is_delete = 0 and allergy_type = 7").Count() > 0)
                        {
                            DataTable dtother = dtAllergy.Select("is_delete = 0 and allergy_type = 7").CopyToDataTable();
                            if (dtother.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                            {
                                rptotherallergies.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 7").CopyToDataTable();
                                rptotherallergies.DataBind();
                                gvw_others.DataSource = null;
                                gvw_others.DataBind();
                                lblmodalnoother.Style.Add("display", "none");
                                rbother1.Checked = true;
                            }
                            else
                            {
                                rptotherallergies.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 7").CopyToDataTable();
                                rptotherallergies.DataBind();
                                gvw_others.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 7").CopyToDataTable();
                                gvw_others.DataBind();
                                lblmodalnoother.Style.Add("display", "none");
                                rbother2.Checked = true;
                            }
                        }
                        else
                        {
                            rptotherallergies.DataSource = null;
                            rptotherallergies.DataBind();
                            gvw_others.DataSource = null;
                            gvw_others.DataBind();
                            lblmodalnoother.Style.Add("display", "");
                            //rbother1.Checked = true;
                        }
                    }
                }
                else
                {
                    gvw_allergy.DataSource = null;
                    gvw_allergy.DataBind();
                    gvw_foods.DataSource = null;
                    gvw_foods.DataBind();
                    gvw_others.DataSource = null;
                    gvw_others.DataBind();
                    //rbdrug1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript8", "HideDiv8();", true);
                    //rbfood1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript9", "HideDiv9();", true);
                    //rbother1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScriptOther", "HideDivOtherAllergy();", true);
                }

                List<PatientSurgery> surgery = Jsongetsoap.list.patient_surgery;

                if (surgery.Count > 0)
                {
                    hdnhistorysurgery.Value = new JavaScriptSerializer().Serialize(surgery);

                    DataTable dtoperasi = Helper.ToDataTable(surgery).Select("is_delete = 0").CopyToDataTable();
                    if (dtoperasi.Rows[0]["surgery_type"].ToString() == "Tidak Ada Operasi")
                    {
                        rptsurgery.DataSource = Helper.ToDataTable(surgery);
                        rptsurgery.DataBind();
                        lblmodalnosurgery.Style.Add("display", "none");

                        gvw_surgery.DataSource = null;
                        gvw_surgery.DataBind();
                        rbOperasi.Checked = true;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript7", "HideDiv7();", true);
                    }
                    else
                    {
                        if (Helper.ToDataTable(surgery).Select("is_delete = 0").Count() > 0)
                        {
                            rptsurgery.DataSource = Helper.ToDataTable(surgery);
                            rptsurgery.DataBind();
                            lblmodalnosurgery.Style.Add("display", "none");

                            DataTable dt = Helper.ToDataTable(surgery);
                            gvw_surgery.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
                            gvw_surgery.DataBind();
                        }
                        rbOperas2.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript7", "ShowHideDiv7();", true);
                    }
                }
                else
                {
                    hdnhistorysurgery.Value = "";
                    rptsurgery.DataSource = null;
                    rptsurgery.DataBind();
                    lblmodalnosurgery.Style.Add("display", "");

                    gvw_surgery.DataSource = null;
                    gvw_surgery.DataBind();
                    //rbOperasi.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript7", "HideDiv7();", true);
                }

                List<PatientProcedureHistory> procedureoutside = Jsongetsoap.list.patient_procedure;

                if (procedureoutside.Count > 0)
                {
                    hdnProcedureOutside.Value = new JavaScriptSerializer().Serialize(procedureoutside);
                    //if (Helper.ToDataTable(procedureoutside).Select("is_myself = 1").Count() > 0)
                    //{
                        rptprocout.DataSource = Helper.ToDataTable(procedureoutside);
                        rptprocout.DataBind();
                        lblmodalnoprocout.Style.Add("display", "none");

                        DataTable dt = Helper.ToDataTable(procedureoutside);
                        gvw_procout.DataSource = dt;
                        gvw_procout.DataBind();
                    //}
                    rbProcOut2.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScriptProcout", "ShowHideDivProcout();", true);

                    //lblmodalnoprocout.Style.Add("display", "none");
                    //rptprocout.Visible = true;
                }
                else
                {
                    hdnProcedureOutside.Value = "";
                    rptprocout.DataSource = null;
                    rptprocout.DataBind();
                    lblmodalnoprocout.Style.Add("display", "");

                    gvw_procout.DataSource = null;
                    gvw_procout.DataBind();
                    rbProcOut1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScriptProcout", "HideDivProcout();", true);

                    //lblmodalnoprocout.Style.Add("display", "");
                    //rptprocout.Visible = false;
                }

                int tempflagdisease = 0, tempflagdiseasefam = 0;
                int no_tempflagdisease = 0, no_tempflagdiseasefam = 0;

                List<PatientDiseaseHistory> listPatientDiseases = Jsongetsoap.list.patient_disease;              
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
                            {
                                chkdisease3.Checked = true;
                                if (x.status == "Tidak Diketahui")
                                {
                                    DDL_TBC.SelectedValue = "Tidak Diketahui";
                                    Button_TbcStickeroff.Visible = true;
                                    Button_TbcStickeron.Visible = false;
                                }
                                if (x.status == "Sudah Sembuh")
                                {
                                    DDL_TBC.SelectedValue = "Sudah Sembuh";
                                    Button_TbcStickeroff.Visible = true;
                                    Button_TbcStickeron.Visible = false;
                                }
                                if (x.status == "Belum Sembuh")
                                {
                                    DDL_TBC.SelectedValue = "Belum Sembuh";
                                    Button_TbcStickeroff.Visible = false;
                                    Button_TbcStickeron.Visible = true;
                                }
                            }
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
                            else if (x.value == "Hepatitis B")
                            {
                                chkdiseaseHepB.Checked = true;
                                if (x.status == "Tidak Diketahui")
                                {
                                    DDL_HepB.SelectedValue = "Tidak Diketahui";
                                    Button_HepBStickeroff.Visible = true;
                                    Button_HepBStickeron.Visible = false;
                                }
                                if (x.status == "Sudah Sembuh")
                                {
                                    DDL_HepB.SelectedValue = "Sudah Sembuh";
                                    Button_HepBStickeroff.Visible = true;
                                    Button_HepBStickeron.Visible = false;
                                }
                                if (x.status == "Belum Sembuh")
                                {
                                    DDL_HepB.SelectedValue = "Belum Sembuh";
                                    Button_HepBStickeroff.Visible = false;
                                    Button_HepBStickeron.Visible = true;
                                }
                            }
                            else if (x.value == "Hepatitis C")
                            {
                                chkdiseaseHepC.Checked = true;
                                if (x.status == "Tidak Diketahui")
                                {
                                    DDL_HepC.SelectedValue = "Tidak Diketahui";
                                    Button_HepCStickeroff.Visible = true;
                                    Button_HepCStickeron.Visible = false;
                                }
                                if (x.status == "Sudah Sembuh")
                                {
                                    DDL_HepC.SelectedValue = "Sudah Sembuh";
                                    Button_HepCStickeroff.Visible = true;
                                    Button_HepCStickeron.Visible = false;
                                }
                                if (x.status == "Belum Sembuh")
                                {
                                    DDL_HepC.SelectedValue = "Belum Sembuh";
                                    Button_HepCStickeroff.Visible = false;
                                    Button_HepCStickeron.Visible = true;
                                }
                            }
                            else if (x.value == "HAD")
                            {
                                CheckBoxHAD.Checked = true;
                                if (x.status == "Tidak Diketahui")
                                {
                                    Button_HadStickeroff.Visible = true;
                                    Button_HadStickeron.Visible = false;
                                }
                                if (x.status == "Sudah Sembuh")
                                {
                                    Button_HadStickeroff.Visible = true;
                                    Button_HadStickeron.Visible = false;
                                }
                                if (x.status == "Belum Sembuh")
                                {
                                    Button_HadStickeroff.Visible = false;
                                    Button_HadStickeron.Visible = true;
                                }
                            }
                            else if (x.value == "PRT")
                            {
                                CheckBoxPRT.Checked = true;
                                if (x.status == "Tidak Diketahui")
                                {
                                    Button_PrtStickeroff.Visible = true;
                                    Button_PrtStickeron.Visible = false;
                                }
                                if (x.status == "Sudah Sembuh")
                                {
                                    Button_PrtStickeroff.Visible = true;
                                    Button_PrtStickeron.Visible = false;
                                }
                                if (x.status == "Belum Sembuh")
                                {
                                    Button_PrtStickeroff.Visible = false;
                                    Button_PrtStickeron.Visible = true;
                                }
                            }
                            else if (x.value == "RHN")
                            {
                                CheckBoxRHN.Checked = true;
                                if (x.status == "Tidak Diketahui")
                                {
                                    Button_RhnStickeroff.Visible = true;
                                    Button_RhnStickeron.Visible = false;
                                }
                                if (x.status == "Sudah Sembuh")
                                {
                                    Button_RhnStickeroff.Visible = true;
                                    Button_RhnStickeron.Visible = false;
                                }
                                if (x.status == "Belum Sembuh")
                                {
                                    Button_RhnStickeroff.Visible = false;
                                    Button_RhnStickeron.Visible = true;
                                }
                            }
                            else if (x.value == "MRS")
                            {
                                CheckBoxMRS.Checked = true;
                                if (x.status == "Tidak Diketahui")
                                {
                                    Button_MrsStickeroff.Visible = true;
                                    Button_MrsStickeron.Visible = false;
                                }
                                if (x.status == "Sudah Sembuh")
                                {
                                    Button_MrsStickeroff.Visible = true;
                                    Button_MrsStickeron.Visible = false;
                                }
                                if (x.status == "Belum Sembuh")
                                {
                                    Button_MrsStickeroff.Visible = false;
                                    Button_MrsStickeron.Visible = true;
                                }
                            }
                            else if (x.value == "COVID")
                            {
                                CheckBoxCOVID.Checked = true;
                                if (x.status == "Tidak Diketahui")
                                {
                                    Button_CvStickeroff.Visible = true;
                                    Button_CvStickeron.Visible = false;
                                }
                                if (x.status == "Sudah Sembuh")
                                {
                                    Button_CvStickeroff.Visible = true;
                                    Button_CvStickeron.Visible = false;
                                }
                                if (x.status == "Belum Sembuh")
                                {
                                    Button_CvStickeroff.Visible = false;
                                    Button_CvStickeron.Visible = true;
                                }
                            }
                            else if (x.value == "Lain-lain")
                            {
                                txtDisease.Text = x.remarks;
                            }
                            else if (x.value == "Tidak Ada Riwayat")
                            {
                                no_tempflagdisease = 1;
                            }
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
                            else if (x.value == "Tidak Ada Riwayat")
                            {
                                no_tempflagdiseasefam = 1;
                            }
                        }
                    }
                    if (tempflagdisease == 1)
                    {
                        if (no_tempflagdisease == 1)
                        {
                            rbpribadi1.Checked = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript2", "HideDiv2();", true);
                        }
                        else
                        {
                            rbpribadi2.Checked = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript2", "ShowHideDiv2();", true);
                        }
                    }
                    else
                    {
                        //rbpribadi1.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript2", "HideDiv2();", true);
                    }

                    if (tempflagdiseasefam == 1)
                    {
                        if (no_tempflagdiseasefam == 1)
                        {
                            rbkeluarga1.Checked = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript3", "HideDiv3();", true);
                        }
                        else
                        {
                            rbkeluarga2.Checked = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript3", "ShowHideDiv3();", true);
                        }
                    }
                    else
                    {
                        //rbkeluarga1.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript3", "HideDiv3();", true);
                    }
                }
                List<PatientDiseaseHistory> tempdisease = Jsongetsoap.list.patient_disease;
                List<PatientDiseaseHistory> disease = tempdisease.Where(x => x.disease_history_type == 1).ToList();
                if (disease.Count > 0)
                    hdnDiseaseHistory.Value = new JavaScriptSerializer().Serialize(disease);
                else
                    hdnDiseaseHistory.Value = "";

                foreach (var x in disease.Where(x => x.value == "Lain-lain"))
                {
                    x.value = x.remarks;
                }

                if (disease.Count > 0)
                {
                    List<PatientDiseaseHistory> temprpt_disease = disease.FindAll(x => x.value != "HAD" && x.value != "PRT" && x.value != "RHN" && x.value != "MRS" && x.value != "COVID");
                    rptdisease.DataSource = Helper.ToDataTable(temprpt_disease);
                    rptdisease.DataBind();
                    lblmodalnodisease.Style.Add("display", "none");
                }
                else
                {
                    rptdisease.DataSource = null;
                    rptdisease.DataBind();
                    lblmodalnodisease.Style.Add("display", "");
                }

                setCheckBoxToggle();

                List<PatientDiseaseHistory> diseasefam = tempdisease.Where(x => x.disease_history_type == 2).ToList();

                if (diseasefam.Count > 0)
                    hdnFamilyDiseaseHistory.Value = new JavaScriptSerializer().Serialize(diseasefam);
                else
                    hdnFamilyDiseaseHistory.Value = "";

                foreach (var x in diseasefam.Where(x => x.value == "Lain-lain"))
                {
                    x.value = x.remarks;
                }

                if (diseasefam.Count > 0)
                {
                    
                    rptfamdisease.DataSource = Helper.ToDataTable(diseasefam);
                    rptfamdisease.DataBind();
                    lblmodalnofamdisease.Style.Add("display", "none");

                }
                else
                {
                    hdnFamilyDiseaseHistory.Value = "";
                    rptfamdisease.DataSource = null;
                    rptfamdisease.DataBind();
                    lblmodalnofamdisease.Style.Add("display", "");
                }

                HF_mapping_fa.Value = new JavaScriptSerializer().Serialize(Jsongetsoap.list.infectious_mapping);

                //List<Subjective> templistsubjective = Jsongetsoap.list.subjective.Where(x => x.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade")).ToList();
                //var templistsubjective = ((IEnumerable<dynamic>)Jsongetsoap.list.subjective).Where(x => x.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade")).ToList();
                List<Subjective> templistsubjective = ((List<Subjective>)Jsongetsoap.list.subjective).Where(x => x.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade")).ToList();
                if (templistsubjective[0].remarks != "")
                {
                    lblmodalendemic.Visible = true;
                    lblmodalnoendemic.Style.Add("display", "none");
                    lblmodalendemic.Text = "Yes ~ " + templistsubjective[0].remarks;

                    txtEndemic.Text = templistsubjective[0].remarks;
                    hdnEndemicArea.Value = txtEndemic.Text;
                    rbkunjungan2.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript4", "ShowHideDiv4();", true);
                }
                else
                {
                    lblmodalendemic.Visible = false;
                    lblmodalnoendemic.Style.Add("display", "");
                    hdnEndemicArea.Value = "";
                    rbkunjungan1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript4", "HideDiv4();", true);
                }

                List<InfectiousDisease> templistscreening = ((List<InfectiousDisease>)Jsongetsoap.list.infectious_disease).Where(x => x.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000")).ToList();
                if (templistscreening.Count > 0)
                {
                    hdnInfectiousDisease.Value = new JavaScriptSerializer().Serialize(templistscreening);
                    rptscreening.DataSource = Helper.ToDataTable(templistscreening);
                    rptscreening.DataBind();
                    lblmodalnoscreening.Style.Add("display", "none");

                }
                else
                {
                    hdnInfectiousDisease.Value = "";
                    rptscreening.DataSource = null;
                    rptscreening.DataBind();
                    lblmodalnoscreening.Style.Add("display", "");
                }

                String[] tempArrayAlert = ((List<InfectiousAlert>)Jsongetsoap.list.infectious_alert).Where(x => x.patient_alert_id != Guid.Parse("00000000-0000-0000-0000-000000000000")).Select(x => x.alert_type_name).ToArray();
                if (tempArrayAlert.Length > 0)
                {
                    divKewaspadaan2.Style.Add("display", "");
                    var infectiousAlert = String.Join(", ", tempArrayAlert);
                    listKewaspadaan2.InnerText = infectiousAlert;
                }
                else
                {
                    divKewaspadaan2.Style.Add("display", "none");
                }


                List<Subjective> tempTindakan = ((List<Subjective>)Jsongetsoap.list.subjective).Where(x => x.soap_mapping_id == Guid.Parse("f815de0e-e57a-4250-a2be-bdd27c1876ac")).ToList();
                if (tempTindakan.Count > 0)
                {
                    hdnTindakan.Value = new JavaScriptSerializer().Serialize(tempTindakan);
                }

                List<Subjective> tempDeleteReason = ((List<Subjective>)Jsongetsoap.list.subjective).Where(x => x.soap_mapping_id == Guid.Parse("0cbb6a1d-392b-4fed-8e8a-696bea3cbc32")).ToList();
                if (tempDeleteReason.Count > 0)
                {
                    hdnDeleteReason.Value = new JavaScriptSerializer().Serialize(tempDeleteReason);
                }

                List<InfectiousAlert> templistinfectiousalert = ((List<InfectiousAlert>)Jsongetsoap.list.infectious_alert).Where(x => x.patient_alert_id != Guid.Parse("00000000-0000-0000-0000-000000000000")).ToList();
                if (templistinfectiousalert.Count > 0)
                {
                    rptInfectiousAlert.DataSource = Helper.ToDataTable(templistinfectiousalert);
                    rptInfectiousAlert.DataBind();
                    lblmodalnoalert.Style.Add("display", "none");

                }
                else
                {

                    rptInfectiousAlert.DataSource = null;
                    rptInfectiousAlert.DataBind();
                    lblmodalnoalert.Style.Add("display", "");
                }

                //List<Subjective> templistnutrition = Jsongetsoap.list.subjective.Where(x => x.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).ToList();
                List<Subjective> templistnutrition = ((List<Subjective>)Jsongetsoap.list.subjective).Where(x => x.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).ToList();
                if (templistnutrition[0].remarks != "")
                {
                    lblnutrition.Visible = true;
                    lblmodalnonutrition.Style.Add("display", "none");
                    lblnutrition.Text = "Yes ~ " + templistnutrition[0].remarks;
                    hdnNutrition.Value = templistnutrition[0].remarks;

                    txtNutrition.Text = templistnutrition[0].remarks;
                    rbnutrisi2.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript5", "ShowHideDiv5();", true);
                }
                else
                {
                    lblnutrition.Visible = false;
                    lblmodalnonutrition.Style.Add("display", "");
                    hdnNutrition.Value = "";
                    rbnutrisi1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript5", "HideDiv5();", true);
                }

                //List<Subjective> templistfasting = Jsongetsoap.list.subjective.Where(x => x.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).ToList();
                List<Subjective> templistfasting = ((List<Subjective>)Jsongetsoap.list.subjective).Where(x => x.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).ToList();
                if (templistfasting[0].remarks != "")
                {
                    lblfasting.Visible = true;
                    lblmodalnofasting.Style.Add("display", "none");
                    lblfasting.Text = "Yes ~ " + templistfasting[0].remarks;
                    hdnFasting.Value = templistfasting[0].remarks;

                    txtFasting.Text = templistfasting[0].remarks;
                    rbpuasa2.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript6", "ShowHideDiv6();", true);
                }
                else
                {
                    lblfasting.Visible = false;
                    lblmodalnofasting.Style.Add("display", "");
                    hdnFasting.Value = "";
                    rbpuasa1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript6", "HideDiv6();", true);
                }
                
                List<Objective> listobjective = Jsongetsoap.list.objective;
                string eye = "", move = "", verbal = "";

                //List<Objective> tempobjeye = Jsongetsoap.list.objective.Where(y => y.soap_mapping_id == Guid.Parse("A9C5CD3C-1E02-4DB2-A047-2F1983D1315B")).ToList();
                List<Objective> tempobjeye = ((List<Objective>)Jsongetsoap.list.objective).Where(y => y.soap_mapping_id == Guid.Parse("A9C5CD3C-1E02-4DB2-A047-2F1983D1315B")).ToList();

                eye = tempobjeye[0].value;
                hdnEye.Value = eye;
                if (tempobjeye[0].value == "1")
                {
                    eye4.Checked = true;
                    lbleye.Text = "1. None";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjeye[0].value == "2")
                {
                    eye3.Checked = true;
                    lbleye.Text = "2. To Pressure";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjeye[0].value == "3")
                {
                    eye2.Checked = true;
                    lbleye.Text = "3. To Sound";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjeye[0].value == "4")
                {
                    eye1.Checked = true;
                    lbleye.Text = "4. Spontaneus";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }

                //List<Objective> tempobjmove = Jsongetsoap.list.objective.Where(y => y.soap_mapping_id == Guid.Parse("89B583A5-3003-43AB-9693-60EA6181C8D5")).ToList();
                List<Objective> tempobjmove = ((List<Objective>)Jsongetsoap.list.objective).Where(y => y.soap_mapping_id == Guid.Parse("89B583A5-3003-43AB-9693-60EA6181C8D5")).ToList();

                move = tempobjmove[0].value;
                hdnMove.Value = move;
                if (tempobjmove[0].value == "1")
                {
                    move6.Checked = true;
                    lblmove.Text = "1. None";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjmove[0].value == "2")
                {
                    move5.Checked = true;
                    lblmove.Text = "2. Extension";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjmove[0].value == "3")
                {
                    move4.Checked = true;
                    lblmove.Text = "3. Flexion to pain stumulus";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjmove[0].value == "4")
                {
                    move3.Checked = true;
                    lblmove.Text = "4. Withdrawns from pain";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjmove[0].value == "5")
                {
                    move2.Checked = true;
                    lblmove.Text = "5. Localizes to pain stimulus";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjmove[0].value == "6")
                {
                    move1.Checked = true;
                    lblmove.Text = "6. Obey Commands";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }

                //List<Objective> tempobjverbal = Jsongetsoap.list.objective.Where(y => y.soap_mapping_id == Guid.Parse("FE4CF48C-17A6-4720-AD23-186517DD9C85")).ToList();
                List<Objective> tempobjverbal = ((List<Objective>)Jsongetsoap.list.objective).Where(y => y.soap_mapping_id == Guid.Parse("FE4CF48C-17A6-4720-AD23-186517DD9C85")).ToList();

                verbal = tempobjverbal[0].value;
                hdnVerbal.Value = verbal;
                if (tempobjverbal[0].value == "1")
                {
                    verbal5.Checked = true;
                    lblverbal.Text = "1. None";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjverbal[0].value == "2")
                {
                    verbal4.Checked = true;
                    lblverbal.Text = "2. Incomprehensible sounds";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjverbal[0].value == "3")
                {
                    verbal3.Checked = true;
                    lblverbal.Text = "3. Inappropriate words";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjverbal[0].value == "4")
                {
                    verbal2.Checked = true;
                    lblverbal.Text = "4. Confused";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjverbal[0].value == "5")
                {
                    verbal1.Checked = true;
                    lblverbal.Text = "5. Orientated";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjverbal[0].value == "T")
                {
                    verbal6.Checked = true;
                    lblverbal.Text = "T. Tracheostomy";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (tempobjverbal[0].value == "A")
                {
                    verbal7.Checked = true;
                    lblverbal.Text = "A. Aphasia";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }

                //List<Objective> tempobjpainscale = Jsongetsoap.list.objective.Where(y => y.soap_mapping_id == Guid.Parse("3AAE8DC2-484F-4F3C-A01B-1B0C3F107176")).ToList();
                List<Objective> tempobjpainscale = ((List<Objective>)Jsongetsoap.list.objective).Where(y => y.soap_mapping_id == Guid.Parse("3AAE8DC2-484F-4F3C-A01B-1B0C3F107176")).ToList();

                txtPain.Text = tempobjpainscale[0].value;
                txtPainScale.Text = tempobjpainscale[0].value;
                hdnpainscale.Value = tempobjpainscale[0].value;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "getProgressBar", "getProgressBar(" + txtPainScale.Text + ");", true);

                //List<Objective> tempobjmental = Jsongetsoap.list.objective.Where(y => y.soap_mapping_id == Guid.Parse("73CC7D5A-E5A8-4C5D-938D-0F1209D316C2")).ToList();
                List<Objective> tempobjmental = ((List<Objective>)Jsongetsoap.list.objective).Where(y => y.soap_mapping_id == Guid.Parse("73CC7D5A-E5A8-4C5D-938D-0F1209D316C2")).ToList();

                hdnMentalStatus.Value = tempobjmental[0].value;
                hdnMentalStatusremark.Value = tempobjmental[0].remarks;
                if (tempobjmental[0].value == "")
                    lblmentalstatus.Text = "N/A";
                else
                {
                    if (HFisBahasaSOAP.Value == "ENG")
                    {
                        lblmentalstatus.Text = tempobjmental[0].value;
                    }
                    else if (HFisBahasaSOAP.Value == "IND")
                    {
                        lblmentalstatus.Text = tempobjmental[0].remarks;
                    }

                    if (tempobjmental[0].value.ToUpper() == "Orientasi baik".ToUpper()|| tempobjmental[0].value.ToUpper() == "Good Orientation".ToUpper())
                        mental1.Checked = true;
                    else if (tempobjmental[0].value.ToUpper() == "Disorientasi".ToUpper() || tempobjmental[0].value.ToUpper() == "Disorientated".ToUpper())
                        mental2.Checked = true;
                    else if (tempobjmental[0].value.ToUpper() == "Kooperatif".ToUpper() || tempobjmental[0].value.ToUpper() == "Cooperative".ToUpper())
                        mental3.Checked = true;
                    else if (tempobjmental[0].value.ToUpper() == "Tidak Kooperatif".ToUpper() || tempobjmental[0].value.ToUpper() == "Non Cooperative".ToUpper())
                        mental4.Checked = true;
                }

                lblTotalScore.Attributes.Add("ReadOnly", "ReadOnly");
                txtPainScale.Attributes.Add("ReadOnly", "ReadOnly");
                txtTravelRecommendation.Attributes.Add("ReadOnly", "ReadOnly");

                //List<Objective> tempobjconsciousness = Jsongetsoap.list.objective.Where(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).ToList();
                List<Objective> tempobjconsciousness = ((List<Objective>)Jsongetsoap.list.objective).Where(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).ToList();

                hdnConsciousness.Value = tempobjconsciousness[0].value;
                if (tempobjconsciousness[0].value == "")
                    lblconsciousness.Text = "N/A";
                else
                {
                    lblconsciousness.Text = tempobjconsciousness[0].value;
                    if (tempobjconsciousness[0].value.ToUpper() == "Compos mentis".ToUpper())
                        consciousness1.Checked = true;
                    else if (tempobjconsciousness[0].value.ToUpper() == "Somnolent".ToUpper())
                        consciousness2.Checked = true;
                    else if (tempobjconsciousness[0].value.ToUpper() == "Stupor".ToUpper())
                        consciousness3.Checked = true;
                    else if (tempobjconsciousness[0].value.ToUpper() == "Coma".ToUpper())
                        consciousness4.Checked = true;
                }


                //List<Objective> tempobjfallrisk = Jsongetsoap.list.objective.Where(y => y.soap_mapping_id == Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c")).ToList();
                List<Objective> tempobjfallrisk = ((List<Objective>)Jsongetsoap.list.objective).Where(y => y.soap_mapping_id == Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c")).ToList();
                List<Objective> tempobjfallriskhandling = ((List<Objective>)Jsongetsoap.list.objective).Where(y => y.soap_mapping_id == Guid.Parse("B0C9D8E0-7533-43CD-BFCF-E8C0AC1D4B7E")).ToList();

                List<string> tempvaluefallrisk = new List<string>();
                DataTable dtfallrisk = new DataTable();
                dtfallrisk.Columns.Add("Name", typeof(string));

                if (tempobjfallrisk.Count > 0)
                {
                    
                    if (tempobjfallrisk[0].value == "")
                    {
                        rptnofallrisk.DataSource = null;
                        rptnofallrisk.DataBind();
                        lblnofallrisk.Style.Add("display", "");
                        hdnFallRisk.Value = "";
                        hdnFallRiskHandling.Value = "";
                    }
                    else
                    {
                        hdnFallRisk.Value = new JavaScriptSerializer().Serialize(tempobjfallrisk);
                        lblnofallrisk.Style.Add("display", "none");
                        foreach (var x in tempobjfallrisk)
                        {
                            if (x.value.ToUpper() == "undergo sedation".ToUpper())
                            {
                                fall1.Checked = true;
                                dtfallrisk.Rows.Add(x.remarks);
                            }
                            else if (x.value.ToUpper() == "physical limitation".ToUpper())
                            {
                                fall2.Checked = true;
                                dtfallrisk.Rows.Add(x.remarks);
                            }
                            else if (x.value.ToUpper() == "motion aids".ToUpper())
                            {
                                fall3.Checked = true;
                                dtfallrisk.Rows.Add(x.remarks);
                            }
                            else if (x.value.ToUpper() == "Disorder".ToUpper())
                            {
                                fall4.Checked = true;
                                dtfallrisk.Rows.Add(x.remarks);
                            }
                            else if (x.value.ToUpper() == "fasting".ToUpper())
                            {
                                fall5.Checked = true;
                                dtfallrisk.Rows.Add(x.remarks);
                            }
                                
                        }

                        hdnFallRiskHandling.Value = new JavaScriptSerializer().Serialize(tempobjfallriskhandling);
                        foreach (var x in tempobjfallriskhandling)
                        {
                            if (x.value.ToUpper() == "Stiker".ToUpper())
                                chkfalltempelstiker.Checked = true;
                            else if (x.value.ToUpper() == "Edukasi".ToUpper())
                                chkfalledukasi.Checked = true;
                            else if (x.value.ToUpper() == "Pengaman Terpasang".ToUpper())
                                chkfallPengaman.Checked = true;
                            else if (x.value.ToUpper() == "Ditemani Keluarga".ToUpper())
                                chkfallTemaniKeluarga.Checked = true;
                            else if (x.value.ToUpper() == "Ambulasi".ToUpper())
                                chkfallAmbulasi.Checked = true;
                            else if (x.value.ToUpper() == "Dokumentasi Rekam Medis".ToUpper())
                                chkfallDokumentasiRM.Checked = true;
                        }

                        rptnofallrisk.DataSource = dtfallrisk;
                        rptnofallrisk.DataBind();
                        lblnofallrisk.Style.Add("display", "none");
                    }
                }

                if (eye != "" && move != "" && verbal != "")
                {
                    if (verbal != "T" && verbal != "A")
                    {
                        var total1 = int.Parse(eye);
                        var total2 = int.Parse(move);
                        var total3 = int.Parse(verbal);

                        lblScore.Text = (total1 + total2 + total3).ToString();
                    }
                    else
                        lblScore.Text = "_";
                }
                else
                    lblScore.Text = "_";
 

                #endregion modalFA

                //enable disable soap
                if (Jsongetsoap.list.mr_status == 1) //open status
                {
                    divTopSOAP.Style.Remove("transform");
                    divblokSOAP.Visible = false;

                    HFflagsoapisdisable_old.Value = "0"; //enable
                }
                else if (Jsongetsoap.list.mr_status == 2) //close status
                {
                    divTopSOAP.Style.Add("transform", "translate(0,0)");
                    divblokSOAP.Visible = true;

                    HFflagsoapisdisable_old.Value = "1"; //disable
                }

                //if (Jsongetsoap.list.lab_process_date == null && Jsongetsoap.list.rad_process_date == null)
                //{
                //    HFflaglabradisdisable_old.Value = "0"; //enable
                //}
                //else
                //{
                //    HFflaglabradisdisable_old.Value = "1"; //disable
                //}
                if (Jsongetsoap.list.lab_process_date == null)
                {
                    HFflaglabisdisable_old.Value = "0"; //enable
                }
                else
                {
                    HFflaglabisdisable_old.Value = "1"; //disable
                }
                if (Jsongetsoap.list.rad_process_date == null)
                {
                    HFflagradisdisable_old.Value = "0"; //enable
                }
                else
                {
                    HFflagradisdisable_old.Value = "1"; //disable
                }

                //enable disable page soap
                if (Jsongetsoap.list.status_id == 6) //cancel status
                {
                    divpage.Style.Add("transform", "translate(0,0)");
                    divBlokPage.Visible = true;

                    divFAmodal.Style.Add("transform", "translate(0,0)");
                    divBlokFA.Visible = true;

                    btnsave.Visible = false;
                    btnsubmitsigndiv.Visible = false;
                }
                else
                {
                    divpage.Style.Remove("transform");
                    divBlokPage.Visible = false;

                    divFAmodal.Style.Remove("transform");
                    divBlokFA.Visible = false;
                }

                LabelModifDateSoap.Text = Jsongetsoap.list.ModifiedDate;
                HiddenLastModif.Value = Jsongetsoap.list.ModifiedDate;

                List<Subjective> listsubjective = Jsongetsoap.list.subjective;
                if (listsubjective.Count > 0)
                {
                    Anamnesis.Text = listsubjective.Find(y => y.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0")).remarks;
                    Anamnesis.Rows = Anamnesis.Text.Split('\n').Length;

                    Complaint.Text = listsubjective.Find(y => y.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf")).remarks;
                    Complaint.Rows = Complaint.Text.Split('\n').Length;

                    if (listsubjective.Find(y => y.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")).value != "")
                    {
                        //chkpregnant.Checked = bool.Parse(listsubjective.Find(y => y.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")).value);
                        var nilaihamil = bool.Parse(listsubjective.Find(y => y.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")).value);
                        if(nilaihamil == true)
                        {
                            Radiohamilyes.Checked = true;
                        } 
                        else if (nilaihamil == false)
                        {
                            Radiohamilno.Checked = true;
                        }
                    }

                    if (listsubjective.Find(y => y.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")).value != "")
                    {
                        //chkbreastfeed.Checked = bool.Parse(listsubjective.Find(y => y.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")).value);
                        var nilaisusu = bool.Parse(listsubjective.Find(y => y.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")).value);
                        if (nilaisusu == true)
                        {
                            Radiosusuyes.Checked = true;
                        }
                        else if (nilaisusu == false)
                        {
                            Radiosusuno.Checked = true;
                        }
                    }
                }

                listobjective = Jsongetsoap.list.objective;
                if (listobjective.Count > 0)
                {
                    txtbloodlow.Text = listobjective.Find(y => y.soap_mapping_id == Guid.Parse("AE3CA8C2-EAB0-41B6-9E3E-3ECB8071A9D0")).value;
                    txtbloodhigh.Text = listobjective.Find(y => y.soap_mapping_id == Guid.Parse("E5EFD220-B68E-4652-AD03-D56EF29FCEBB")).value;
                    txtpulserate.Text = listobjective.Find(y => y.soap_mapping_id == Guid.Parse("78CBB61F-4A11-470A-B770-1A44EB04357F")).value;
                    txttemperature.Text = listobjective.Find(y => y.soap_mapping_id == Guid.Parse("2EECA752-A2EA-4426-B3CF-C1EA3833BF30")).value;
                    txtweight.Text = listobjective.Find(y => y.soap_mapping_id == Guid.Parse("52CE9350-BFB2-4072-8893-D0C6CF8B3634")).value;
                    txtheight.Text = listobjective.Find(y => y.soap_mapping_id == Guid.Parse("2A8DBDDB-EDFE-4704-876E-5A2D735BB541")).value;
                    txtrespiratory.Text = listobjective.Find(y => y.soap_mapping_id == Guid.Parse("E6AE2EA9-B321-4756-BF96-2DC232E4A7BA")).value;
                    txtspo.Text = listobjective.Find(y => y.soap_mapping_id == Guid.Parse("E903246C-DF95-4FE0-96D2-CF90C036D3D7")).value;
                    txtOthers.Text = listobjective.Find(y => y.soap_mapping_id == Guid.Parse("7218971c-e89f-4172-ae3c-b7fb855c1d6d")).remarks;
                    txtlingkarkepala.Text = listobjective.Find(y => y.soap_mapping_id == Guid.Parse("A8E0013B-0443-4E7A-B670-4DB9362B40E4")).value;
                    txtbmi.Text = listobjective.Find(y => y.soap_mapping_id == Guid.Parse("19516BE9-9D54-4E30-8E90-0A8B41F5BA66")).value;

                    txtOthers.Rows = txtOthers.Text.Split('\n').Length;
                }

                List<Assessment> listassessment = Jsongetsoap.list.assessment;
                if (listassessment.Count > 0)
                {
                    txtPrimary.Text = (
                            from a in listassessment
                            where a.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")
                            select a.remarks
                        ).FirstOrDefault();
                    txtPrimary.Rows = txtPrimary.Text.Split('\n').Length;
                }

                List<Planning> listplanning = Jsongetsoap.list.planning;
                if (listplanning.Count > 0)
                {
                    txtPlanning.Text = (
                            from a in listplanning
                            where a.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6")
                            select a.remarks
                        ).FirstOrDefault();
                    txtPlanning.Rows = txtPlanning.Text.Split('\n').Length;

                    txtHasilTindakan.Text = listplanning.Find(y => y.soap_mapping_id == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")).remarks;
                    txtHasilTindakan.Rows = txtHasilTindakan.Text.Split('\n').Length;

                    GetDataTravel(listplanning);
                }

                #region setting

                hfmandatorySOAP.Value = orgsetting.Find(y => y.setting_name.ToUpper() == "MANDATORY_SOAP".ToUpper()).setting_value;
                if (hfmandatorySOAP.Value.ToLower() == "true")
                {
                    LabelmandatoryS.Visible = true;
                    LabelmandatoryO.Visible = true;
                    LabelmandatoryA.Visible = true;
                    LabelmandatoryP.Visible = true;
                }

                hfmandatoryFA.Value = orgsetting.Find(y => y.setting_name.ToUpper() == "MANDATORY_DOCTORFA".ToUpper()).setting_value;

                if (orgsetting.Find(y => y.setting_name.ToUpper() == "SHOW_FA".ToUpper()).setting_value == "TRUE")
                {
                    if (Jsongetsoap.list.status_id == 1) //new
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "openmodal", "PreviewFA();", true);
                    }
                }

                if (orgsetting.Find(y => y.setting_name.ToUpper() == "EDIT_FA".ToUpper()).setting_value == "FALSE")
                {
                    if (hfsavemode.Value == "1")
                    {
                        btnEditRoutine.Visible = false;
                        btnEditIllness.Visible = false;
                        btnEditEndemic.Visible = false;
                        btnEditNutrition.Visible = false;
                        btnEditPhysical.Visible = false;
                    }
                }

                if (orgsetting.Find(y => y.setting_name.ToUpper() == "EDIT_ROUTINE".ToUpper()).setting_value == "FALSE")
                {
                    if (hfsavemode.Value == "1")
                    {
                        DisableRowList(1);
                    }   
                }
                else
                {

                    if (Jsongetsoap.list.take_date != null)
                    {
                        DisableRowList(1);
                        HFflagdrugisdisable_old.Value = "1"; //disable
                    }
                }

                if (Jsongetsoap.list.take_date != null)
                {
                    if (orgsetting.Find(y => y.setting_name.ToUpper() == "DISABLE_S".ToUpper()).setting_value == "FALSE")
                    {
                        Complaint.ReadOnly = false;
                        Anamnesis.ReadOnly = false;
                        chkpregnant.Enabled = true;
                        chkbreastfeed.Enabled = true;

                        Radiohamilno.Enabled = true;
                        Radiohamilyes.Enabled = true;
                        Radiosusuno.Enabled = true;
                        Radiosusuyes.Enabled = true;
                    }
                    else
                    {
                        Complaint.ReadOnly = true;
                        Anamnesis.ReadOnly = true;
                        chkpregnant.Enabled = false;
                        chkbreastfeed.Enabled = false;

                        Radiohamilno.Enabled = false;
                        Radiohamilyes.Enabled = false;
                        Radiosusuno.Enabled = false;
                        Radiosusuyes.Enabled = false;
                    }
                    
                    if (orgsetting.Find(y => y.setting_name.ToUpper() == "DISABLE_O".ToUpper()).setting_value == "FALSE")
                    {
                        txtbloodhigh.ReadOnly = false;
                        txtbloodlow.ReadOnly = false;
                        txtpulserate.ReadOnly = false;
                        txtrespiratory.ReadOnly = false;
                        txtspo.ReadOnly = false;
                        txttemperature.ReadOnly = false;                        
                        txtweight.ReadOnly = false;
                        txtheight.ReadOnly = false;
                        txtOthers.ReadOnly = false;
                        txtlingkarkepala.ReadOnly = false;
                        txtbmi.ReadOnly = false;
                    }
                    else
                    {
                        txtbloodhigh.ReadOnly = true;
                        txtbloodlow.ReadOnly = true;
                        txtpulserate.ReadOnly = true;
                        txtrespiratory.ReadOnly = true;
                        txtspo.ReadOnly = true;
                        txttemperature.ReadOnly = true;                        
                        txtweight.ReadOnly = true;
                        txtheight.ReadOnly = true;
                        txtOthers.ReadOnly = true;
                        txtlingkarkepala.ReadOnly = true;
                        txtbmi.ReadOnly = true;
                    }

                    if (orgsetting.Find(y => y.setting_name.ToUpper() == "DISABLE_A".ToUpper()).setting_value == "FALSE")
                    {
                        txtPrimary.ReadOnly = false;
                    }
                    else
                    {
                        txtPrimary.ReadOnly = true;
                    }


                    if (orgsetting.Find(y => y.setting_name.ToUpper() == "DISABLE_P".ToUpper()).setting_value == "FALSE")
                    {
                        txtPlanning.ReadOnly = false;
                    }
                    else
                    {
                        txtPlanning.ReadOnly = true;
                    }  
                }

                if (orgsetting.Find(y => y.setting_name.ToUpper() == "TRAVEL_RECOMMENDATION".ToUpper()).setting_value == "FALSE")
                {
                    divaddbuttontravel.Visible = false;
                    diveditbuttontravel.Visible = false;
                }
                else
                {
                    divaddbuttontravel.Visible = true;
                    diveditbuttontravel.Visible = true;
                }

                if (Jsongetsoap.list.additional_take_date != null)
                {
                    HFflagadddrugisdisable_old.Value = "1"; //disable
                }

                #endregion


                var localIPAdress = GetLocalIPAddress();
                preview.NavigateUrl = "http://" + localIPAdress + "/printingemr?printtype=MedicalResume&OrganizationId=" + Helper.organizationId
                        + "&AdmissionId=" + hfAdmissionId.Value.ToString() + "&EncounterId=" + hfEncounterId.Value.ToString() + "&PatientId=" + hfPatientId.Value.ToString() + "&PageSOAP=" + hfPageSoapId.Value.ToString() + "&PrintBy=" + Helper.GetLoginUser(this) ;
                //preview.NavigateUrl = preview.NavigateUrl + "?idPatient=" + hfPatientId.Value.ToString() + "&AdmissionId=" + hfAdmissionId.Value.ToString() + "&EncounterId=" + hfEncounterId.Value.ToString();
                email.NavigateUrl = email.NavigateUrl + "?idPatient=" + hfPatientId.Value.ToString() + "&AdmissionId=" + hfAdmissionId.Value.ToString() + "&EncounterId=" + hfEncounterId.Value.ToString();

                List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];

                string SAVESOAPmarker = markerlist.Find(x => x.key == "SAVESOAPmarker").value;
                if (SAVESOAPmarker == "marked")
                {
                    //ShowToastr("The data has been saved.", "Success", "Success");
                    ShowToastr("Save as Draft successful.", "Success", "Success");
                }
                markerlist.Find(x => x.key == "SAVESOAPmarker").value = "unmarked";

                string SUBMITSOAPmarker = markerlist.Find(x => x.key == "SUBMITSOAPmarker").value;
                if (SUBMITSOAPmarker == "marked")
                {
                    ShowToastr("Submit and Sign successful.", "Success", "Success");
                }
                markerlist.Find(x => x.key == "SUBMITSOAPmarker").value = "unmarked";

                string TAKENmarker = markerlist.Find(x => x.key == "TAKENmarker").value;
                if (TAKENmarker != "SUCCESS" && TAKENmarker != "unmarked")
                {
                    //string[] takenmark = Helper.TAKENmarker.Split(new string[] { "||" }, StringSplitOptions.None);
                    string alert = "";
                    string alert2 = "";
                    string alert3 = "";
                    string alertfail = "";

                    if (TAKENmarker != "CANCEL")
                    {
                        string HFflagsoapisdisable = markerlist.Find(x => x.key == "HFflagsoapisdisable").value;
                        if (TAKENmarker.Contains("MR TAKEN") && HFflagsoapisdisable != "1")
                        {
                            alert = alert + "Data already coded by MR team. </br>Please contact MR to reopen this encounter.</br>";
                            notifsoap.Style.Clear();
                            notifsoap.Attributes["style"] = "text-align: left; background-color: aliceblue; border: 1px solid #4081ed; border-radius: 6px; padding: 8px; margin-bottom: 5px;";
                            //notifsoap.Visible = true;
                        }
                        else
                        {
                            if (HFflagsoapisdisable == "1")
                            {
                                notifsoap.Visible = false;
                            }
                            else
                            {
                                notifsoap.Visible = true;
                                notifsoap.Style.Clear();
                                notifsoap.Attributes["style"] = "text-align: left; padding-top: 5px; padding-bottom: 15px;";

                                isubmit.Attributes["class"] = "fa fa-check-circle";
                                isubmit.Style.Add("color", "green");
                                alert = alert + "SOAP Data is Submitted.</br>";
                            }

                            string HFflagdrugisdisable = markerlist.Find(x => x.key == "HFflagdrugisdisable").value;
                            if (HFflagdrugisdisable == "1")
                            {
                                notifsoap2.Visible = false;
                            }
                            else
                            {
                                if (TAKENmarker.Contains("PRESCRIPTION TAKEN"))
                                {
                                    alert2 = alert2 + "Prescription is already processed by pharmacy. </br>To make changes please contact pharmacy to reopen prescription";
                                    notifsoap2.Visible = true;
                                }
                                else
                                {
                                    isubmit2.Attributes["class"] = "fa fa-check-circle";
                                    isubmit2.Style.Add("color", "green");
                                    alert2 = alert2 + "Prescription Data is Submitted. </br>";
                                    notifsoap2.Visible = true;
                                    notifsoap2.Style.Clear();
                                    //notifsoap2.Attributes["style"] = "text-align: left; background-color: #f0fff0; border: 1px solid #5bed40; border-radius: 6px; padding: 8px; margin-bottom: 5px;";
                                }
                            }

                            string HFflagadddrugisdisable = markerlist.Find(x => x.key == "HFflagadddrugisdisable").value;
                            if (HFflagadddrugisdisable == "1")
                            {
                                notifsoap3.Visible = false;
                            }
                            else
                            {
                                if (TAKENmarker.Contains("ADDITIONAL TAKEN"))
                                {
                                    alert3 = alert3 + "Additional prescription is already processed by pharmacy. </br>To make changes please contact pharmacy to reopen additional prescription";
                                    notifsoap3.Visible = true;
                                }
                                else
                                {
                                    if (Jsongetsoap.list.verify_date != null)
                                    {
                                        isubmit3.Attributes["class"] = "fa fa-check-circle";
                                        isubmit3.Style.Add("color", "green");
                                        alert3 = alert3 + "Additional Prescription Data is Submitted. </br>";
                                        notifsoap3.Visible = true;
                                        notifsoap3.Style.Clear();
                                        //notifsoap3.Attributes["style"] = "text-align: left; background-color: #f0fff0; border: 1px solid #5bed40; border-radius: 6px; padding: 8px; margin-bottom: 5px;";
                                    }
                                }
                            }

                            if (HFflagsoapisdisable == "1" && HFflagdrugisdisable == "1" && HFflagadddrugisdisable == "1")
                            {
                                isubmit.Attributes["class"] = "fa fa-check-circle";
                                isubmit.Style.Add("color", "green");
                                alert = "Submit Process is Successfull.</br>";

                                notifsoap.Visible = true;
                            }
                        }
                        notifsubmitheader.Text = "Submit Process Status";
                        notifsubmit.Text = alert;
                        notifsubmit2.Text = alert2;
                        notifsubmit3.Text = alert3;

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalnotifsubmit", "$('#modalnotifsubmit').modal('show');", true);
                    }
                    else
                    {
                        alertfail = alertfail + "We cannot save the data due to patient cancellation in Hope!</br></br>";

                        notifsubmitfail.Text = alertfail;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalnotifsubmitfail", "$('#modalnotifsubmitfail').modal('show');", true);
                    }

                    UpdatePanelNotifSubmit.Update();
                    UpdatePanelNotifSubmitFail.Update();
                }

                markerlist.Find(x => x.key == "HFflagsoapisdisable").value = HFflagsoapisdisable_old.Value;
                markerlist.Find(x => x.key == "HFflagdrugisdisable").value = HFflagdrugisdisable_old.Value;
                markerlist.Find(x => x.key == "HFflagadddrugisdisable").value = HFflagadddrugisdisable_old.Value;

                markerlist.Find(x => x.key == "TAKENmarker").value = "unmarked";
                markerlist.Find(x => x.key == "SUBMITSOAPmarker").value = "unmarked";

                Session[Helper.SESSIONmarker] = markerlist;

                txtSurgeryDate.Attributes.Add("ReadOnly", "ReadOnly");
                txtProcoutDate.Attributes.Add("ReadOnly", "ReadOnly");

                //specialty section//
                
                string templateid = Request.QueryString["PageSoapId"];
                if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
                {

                }
                else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
                {
                    Emergency_Load(Jsongetsoap.list);
                    StdTriage.Visible = true;
                    StdTriage.initializevalue(Jsongetsoap.list.objective);
                    ddlForm_Type.Enabled = false;
                    btnChooseTemplate.Enabled = false;


                }
                else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
                {
                    Obgyn_Load(Jsongetsoap.list, false);
                    StdObgyn.Visible = true;
                    StdObgyn.initializevalue(Jsongetsoap.list.pregnancy_data, Jsongetsoap.list.pregnancy_history, Jsongetsoap.list.objective);
                    //StdObgyn.submitObgynSender += new Form_SOAP_Control_Template_Specialty_StdObgyn.customHandler(submitObgyn);
                }
                else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
                {
                    Pediatric_Load(Jsongetsoap.list, false);
                    StdPediatric.Visible = true;
                    StdPediatric.initializevalue(Jsongetsoap.list.pediatric_data);

                    divmenukurva.Visible = true;
                    StdKurvaPertumbuhan.Visible = true;
                    //StdKurvaPertumbuhan.initializevalue(Jsongetsoap, header, hfguidadditional.Value);
                    StdKurvaPertumbuhan.initializevalue(header, hfguidadditional.Value, hfPatientId.Value, hfEncounterId.Value, hfAdmissionId.Value, Helper.GetDoctorID(this));
                    Session[Helper.SessionDataDetailChart + hfguidadditional.Value] = Jsongetsoap.list.pediatric_chart;

                    List<PediatricChart> initchart = Jsongetsoap.list.pediatric_chart;
                    Session[Helper.SessionDataDetailChart_Save + hfguidadditional.Value] = initchart.ToList();
                }

            }

            //HEALTH RECORD
            long OrganizationId = Helper.organizationId; //long.Parse(Request.QueryString["OrgId"]);
            long PatientId = long.Parse(Request.QueryString["idPatient"]);
            int StatusId = 1;//int.Parse(Request.QueryString["SttId"]);
            string EncounterId = Request.QueryString["EncounterId"];
            long userid = long.Parse(Helper.GetDoctorID(this)); //long.Parse(Request.QueryString["UsrId"]);

            GetDataHealthInfo(OrganizationId, PatientId, StatusId, EncounterId);
            GetDataIframe(Helper.organizationId, long.Parse(hfPatientId.Value), 1, hfEncounterId.Value, long.Parse(Helper.GetDoctorID(this)));
            UpdatePanelModalMedicationIframe.Update();
            UpdatePanelModalIllnessIframe.Update();
            //END HEALTH RECORD
            //Referal
            List<ReferalData> referalDatasobj = (List<ReferalData>)Session[Helper.SessionSOAPReferral + hfguidadditional.Value];
            var org = Helper.organizationId.ToString();
            if (referalDatasobj.Count() > 0)
            {
                divrujukansoap.Visible = true;
                DataTable dtreferal = Helper.ToDataTable(referalDatasobj);
                rptrujukan.DataSource = dtreferal;
                rptrujukan.DataBind();

            }
            else
            {
                divrujukansoap.Visible = false;
                UpdatePanelRujukan.Update();
            }

            DataTable dtrefralobj = Helper.ToDataTable(referalDatasobj);
            if (dtrefralobj.Select("IsProcess = 1 or is_editable = 1 ").Count() > 1)
            {
                BtnDeleteAllReferral.Enabled = false;
                BtnDeleteAllReferral.Style.Add("color", "#B9B9B9");
            }
            else
            {

                BtnDeleteAllReferral.Enabled = true;
                BtnDeleteAllReferral.Attributes.Add("href", "javascript:$('#modal-delete-referal').modal();");
                BtnDeleteAllReferral.Style.Add("color", "#E84118");
            }

            InpatientData inpatientDataObj = (InpatientData)Session[Helper.SessionSOAPRawatInap + hfguidadditional.Value];

            if (inpatientDataObj != null)
            {
                if (inpatientDataObj.admission_date != null)
                {
                    div_rawatinap.Visible = true;
                }
                else
                {
                    div_rawatinap.Visible = false;
                }
            }
            else
            {
                div_rawatinap.Visible = false;
            }


            //Referal

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Page_Load", StartTime, "OK", MyUser.GetUsername()
                          , "", "", ""));
            //Log.Info(LogConfig.LogEnd());
        }

        StdObgyn.submitObgynSender += new Form_SOAP_Control_Template_Specialty_StdObgyn.customHandler(submitObgyn);
        StdPediatric.submitPediatricSender += new Form_SOAP_Control_Template_Specialty_StdPediatric.customHandler(submitPediatric);

        CheckVisibleDiv();
        setSoapTextHeight();
        ScriptManager.RegisterStartupScript(this, GetType(), "totalfee", "CalculateTotalFee();", true);

    }

    protected dynamic MappingforGetdataSOAP()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        //set sesuai templatenya
        string templateid = Request.QueryString["PageSoapId"];

        Dictionary<string, string> logParam = new Dictionary<string, string>
        {
            { "Encounter_ID", hfEncounterId.Value },
            { "Patient_ID", hfPatientId.Value },
            { "Admission_ID", hfAdmissionId.Value },
            { "Organization_ID", Helper.organizationId.ToString() },
            { "Doctor_ID", Helper.GetDoctorID(this) },
            { "Template_ID", templateid.ToUpper() }

        };
        //log.debug(logconfig.LogStart("getSOAPDynamic", logParam));
        
        if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {
            var getsoapGeneral = clsSOAP.getSOAP(hfEncounterId.Value, long.Parse(hfPatientId.Value), long.Parse(hfAdmissionId.Value), Helper.organizationId, long.Parse(Helper.GetDoctorID(this)));
            ResultSOAP JsongetsoapGeneral = JsonConvert.DeserializeObject<ResultSOAP>(getsoapGeneral.Result.ToString());
            //log.debug(logconfig.LogEnd("getSOAP", JsongetsoapGeneral.Status, JsongetsoapGeneral.Message));

            //variable ini hanya dipakai untuk proses copy soap
            hfsoapstring.Value = getsoapGeneral.Result.ToString().Replace("<", "< "); ;
            hfsoapcopystring.Value = "";

            return JsongetsoapGeneral;
        }
        else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
        {
            var getsoapGeneral = clsSOAP.getSOAP(hfEncounterId.Value, long.Parse(hfPatientId.Value), long.Parse(hfAdmissionId.Value), Helper.organizationId, long.Parse(Helper.GetDoctorID(this)));
            ResultSOAP JsongetsoapGeneral = JsonConvert.DeserializeObject<ResultSOAP>(getsoapGeneral.Result.ToString());
            //log.debug(logconfig.LogEnd("getSOAP", JsongetsoapGeneral.Status, JsongetsoapGeneral.Message));
            
            //variable ini hanya dipakai untuk proses copy soap
            hfsoapstring.Value = getsoapGeneral.Result.ToString().Replace("<", "< "); ;
            hfsoapcopystring.Value = "";

            return JsongetsoapGeneral;
        }
        else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
        {
            var getsoapObgyn = clsSPObgynSOAP.getSOAPObgyn(hfEncounterId.Value, long.Parse(hfPatientId.Value), long.Parse(hfAdmissionId.Value), Helper.organizationId, long.Parse(Helper.GetDoctorID(this)));
            ResultSOAPObgyn JsongetsoapObgyn = JsonConvert.DeserializeObject<ResultSOAPObgyn>(getsoapObgyn.Result.ToString());
            //log.debug(logconfig.LogEnd("getSOAPObgyn", JsongetsoapObgyn.Status, JsongetsoapObgyn.Message));

            //variable ini hanya dipakai untuk proses copy soap
            hfsoapstring.Value = getsoapObgyn.Result.ToString().Replace("<", "< "); ;
            hfsoapcopystring.Value = "";

            return JsongetsoapObgyn;
        }
        else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
        {
            var getsoapPediatric = clsSPPediatricSOAP.getSOAPPediatric(hfEncounterId.Value, long.Parse(hfPatientId.Value), long.Parse(hfAdmissionId.Value), Helper.organizationId, long.Parse(Helper.GetDoctorID(this)));
            ResultSOAPPediatric JsongetsoapPediatric = JsonConvert.DeserializeObject<ResultSOAPPediatric>(getsoapPediatric.Result.ToString());
            //log.debug(logconfig.LogEnd("getSOAPPediatric", JsongetsoapPediatric.Status, JsongetsoapPediatric.Message));

            //variable ini hanya dipakai untuk proses copy soap
            hfsoapstring.Value = getsoapPediatric.Result.ToString().Replace("<", "< "); ;
            hfsoapcopystring.Value = "";

            return JsongetsoapPediatric;
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "MappingforGetdataSOAP", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());

        return null;
    }

    protected dynamic MappingforGetdataCOPYSOAP(string CopyEncID, long CopyPtnID, long CopyAdmID, long CopyOrgID, long CopyDocID)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        //set sesuai templatenya
        string templateid = Request.QueryString["PageSoapId"];

        Dictionary<string, string> logParam = new Dictionary<string, string>
        {
            { "Encounter_ID", CopyEncID },
            { "Patient_ID", CopyPtnID.ToString() },
            { "Admission_ID", CopyAdmID.ToString() },
            { "Organization_ID", CopyOrgID.ToString() },
            { "Doctor_ID", CopyDocID.ToString() },
            { "Template_ID", templateid.ToUpper() }

        };
        //log.debug(logconfig.LogStart("getCopySOAPDynamic", logParam));

        if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {
            var getsoapGeneral = clsSOAP.getSOAP(CopyEncID, CopyPtnID, CopyAdmID, CopyOrgID, CopyDocID);
            ResultSOAP JsongetsoapGeneral = JsonConvert.DeserializeObject<ResultSOAP>(getsoapGeneral.Result.ToString());
            //log.debug(logconfig.LogEnd("getSOAP", JsongetsoapGeneral.Status, JsongetsoapGeneral.Message));

            //variable ini hanya dipakai untuk proses copy soap
            hfsoapcopystring.Value = getsoapGeneral.Result.ToString();

            return JsongetsoapGeneral;
        }
        else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
        {
            var getsoapGeneral = clsSOAP.getSOAP(CopyEncID, CopyPtnID, CopyAdmID, CopyOrgID, CopyDocID);
            ResultSOAP JsongetsoapGeneral = JsonConvert.DeserializeObject<ResultSOAP>(getsoapGeneral.Result.ToString());
            //log.debug(logconfig.LogEnd("getSOAP", JsongetsoapGeneral.Status, JsongetsoapGeneral.Message));

            //variable ini hanya dipakai untuk proses copy soap
            hfsoapcopystring.Value = getsoapGeneral.Result.ToString();

            return JsongetsoapGeneral;
        }
        else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
        {
            var getsoapObgyn = clsSPObgynSOAP.getSOAPObgyn(CopyEncID, CopyPtnID, CopyAdmID, CopyOrgID, CopyDocID);
            ResultSOAPObgyn JsongetsoapObgyn = JsonConvert.DeserializeObject<ResultSOAPObgyn>(getsoapObgyn.Result.ToString());
            //log.debug(logconfig.LogEnd("getSOAPObgyn", JsongetsoapObgyn.Status, JsongetsoapObgyn.Message));

            //variable ini hanya dipakai untuk proses copy soap
            hfsoapcopystring.Value = getsoapObgyn.Result.ToString();

            return JsongetsoapObgyn;
        }
        else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
        {
            var getsoapPediatric = clsSPPediatricSOAP.getSOAPPediatric(CopyEncID, CopyPtnID, CopyAdmID, CopyOrgID, CopyDocID);
            ResultSOAPPediatric JsongetsoapPediatric = JsonConvert.DeserializeObject<ResultSOAPPediatric>(getsoapPediatric.Result.ToString());
            //log.debug(logconfig.LogEnd("getSOAPPediatric", JsongetsoapPediatric.Status, JsongetsoapPediatric.Message));

            //variable ini hanya dipakai untuk proses copy soap
            hfsoapcopystring.Value = getsoapPediatric.Result.ToString();

            return JsongetsoapPediatric;
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "MappingforGetdataCOPYSOAP", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());

        return null;
    }

    protected dynamic MappingforGetdataSOAPSession()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        //set sesuai templatenya
        string templateid = Request.QueryString["PageSoapId"];
        if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {
            SOAP ListgetSOAPGeneral = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
            return ListgetSOAPGeneral;
        }
        else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
        {
            SOAP ListgetSOAPGeneral = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
            return ListgetSOAPGeneral;
        }
        else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
        {
            SOAPObgyn ListgetSOAPObgyn = (SOAPObgyn)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
            return ListgetSOAPObgyn;
        }
        else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
        {
            SOAPPediatric ListgetSOAPPediatric = (SOAPPediatric)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
            return ListgetSOAPPediatric;
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "MappingforGetdataSOAPSession", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
        return null;
    }

    protected dynamic MappingforGetdataSOAPSessionBAckup()
    {
        //Log.Info(LogConfig.LogEnd());
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        //set sesuai templatenya
        string templateid = Request.QueryString["PageSoapId"];
        if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {
            SOAP ListgetSOAPGeneral = (SOAP)Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]];
            return ListgetSOAPGeneral;
        }
        else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
        {
            SOAP ListgetSOAPGeneral = (SOAP)Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]];
            return ListgetSOAPGeneral;
        }
        else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
        {
            SOAPObgyn ListgetSOAPObgyn = (SOAPObgyn)Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]];
            return ListgetSOAPObgyn;
        }
        else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
        {
            SOAPPediatric ListgetSOAPPediatric = (SOAPPediatric)Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]];
            return ListgetSOAPPediatric;
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "MappingforGetdataSOAPSessionBAckup", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
        return null;
    }

    protected dynamic MappingforAssigndataSOAP(string data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        //set sesuai templatenya
        string templateid = Request.QueryString["PageSoapId"];
        if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {
            ResultSOAP JsonrawsoapGeneral = JsonConvert.DeserializeObject<ResultSOAP>(data);
            return JsonrawsoapGeneral;
        }
        else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
        {
            ResultSOAP JsonrawsoapGeneral = JsonConvert.DeserializeObject<ResultSOAP>(data);
            return JsonrawsoapGeneral;
        }
        else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
        {
            ResultSOAPObgyn JsonrawsoapObgyn = JsonConvert.DeserializeObject<ResultSOAPObgyn>(data);
            return JsonrawsoapObgyn;
        }
        else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
        {
            ResultSOAPPediatric JsonrawsoapPediatric = JsonConvert.DeserializeObject<ResultSOAPPediatric>(data);
            return JsonrawsoapPediatric;
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "MappingforAssigndataSOAP", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
        return null;
    }

    protected dynamic MappingInitVarSOAP()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        //set sesuai templatenya
        string templateid = Request.QueryString["PageSoapId"];
        if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {
            return new SOAP();
        }
        else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
        {
            return new SOAP();
        }
        else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
        { 
            return new SOAPObgyn();
        }
        else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
        {
            return new SOAPPediatric();
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "MappingInitVarSOAP", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
        return null;
    }

    public void Obgyn_Load(SOAPObgyn data, bool isfrommodal)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        txtHPHT.Attributes.Add("ReadOnly", "ReadOnly");
        txtTHL.Attributes.Add("ReadOnly", "ReadOnly");

        divobgyn.Visible = true;
        divtext_row_obgyn.Visible = true;
        divimg_row_obgyn.Visible = true;

        List<PregnancyContraception> listkontrasepidata = new List<PregnancyContraception>();

        if (data.pregnancy_data.Count > 0)
        {
            foreach (PregnancyData x in data.pregnancy_data)
            {
                //front data
                if (isfrommodal != true)
                {
                    if (x.pregnancy_data_type.ToUpper() == "G")
                    {
                        txtG.Text = x.value;
                    }

                    if (x.pregnancy_data_type.ToUpper() == "P")
                    {
                        txtP.Text = x.value;
                    }

                    if (x.pregnancy_data_type.ToUpper() == "A")
                    {
                        txtA.Text = x.value;
                    }

                    if (x.pregnancy_data_type.ToUpper() == "HPHT")
                    {
                        txtHPHT.Text = x.value;
                    }

                    if (x.pregnancy_data_type.ToUpper() == "THL")
                    {
                        txtTHL.Text = x.value;
                    }
                }

                //modal data

                if (x.pregnancy_data_type.ToUpper() == "MENARCHE")
                {
                    lblmodalmenarche.Text = x.value + " tahun";
                }


                if (x.pregnancy_data_type.ToUpper() == "CONTRACEPTION")
                {
                    PregnancyContraception kontrasepsi = new PregnancyContraception();
                    kontrasepsi.pregnancy_data_id = x.pregnancy_data_id;
                    kontrasepsi.pregnancy_data_type = x.pregnancy_data_type;
                    kontrasepsi.value = x.value;
                    kontrasepsi.remarks = x.remarks;
                    kontrasepsi.status = x.status;
                    listkontrasepidata.Add(kontrasepsi);
                }
            }
        }

        if (data.objective.Count > 0)
        {
            foreach (Objective x in data.objective)
            {
                //modal data

                if (x.soap_mapping_name.ToUpper() == "HAID")
                {
                    if (x.value == "Teratur")
                    {
                        lblmodalmenstruation.Text = x.value;
                    }
                    else if(x.value == "Tidak Teratur")
                    {
                        lblmodalmenstruation.Text = x.value + ", " + x.remarks + " hari";
                    }
                }

                if (x.soap_mapping_name.ToUpper() == "HAID PROBLEM")
                {
                    if (x.value != "")
                    {
                        lblmodalcomplainmens.Text = x.value;
                    }
                }

            }
        }

        if (listkontrasepidata.Count > 0)
        {
            DataTable dtkontrasepsi = Helper.ToDataTable(listkontrasepidata);
            if (dtkontrasepsi.Rows[0]["value"].ToString() == "Tidak Ada Kontrasepsi")
            {
                rptcontraception .DataSource = null;
                rptcontraception.DataBind();
            }
            else
            {
                rptcontraception.DataSource = dtkontrasepsi;
                rptcontraception.DataBind();
            }
        }
        else
        {
            rptcontraception.DataSource = null;
            rptcontraception.DataBind();
        }

        if (data.pregnancy_history.Count > 0)
        {
            DataTable dtpregnanthistory = Helper.ToDataTable(data.pregnancy_history);

            rptpregnanthistory.DataSource = dtpregnanthistory;
            rptpregnanthistory.DataBind();

            divmodalpregnanthistory.Visible = true;
            lbl_noobgynpregnanthistory.Visible = false;
        }
        else
        {
            rptpregnanthistory.DataSource = null;
            rptpregnanthistory.DataBind();

            divmodalpregnanthistory.Visible = false;
            lbl_noobgynpregnanthistory.Visible = true;
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Obgyn_Load", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    public void Pediatric_Load(SOAPPediatric data, bool isfrommodal)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        divpediatric.Visible = true;
        divtext_row_pediatric.Visible = true;
        divimg_row_pediatric.Visible = true;

        if (data.pediatric_data.Count > 0)
        {
            foreach (PediatricData x in data.pediatric_data)
            {
                //front data
                if (isfrommodal != true)
                {
                    if (x.pediatric_data_type.ToUpper() == "TENGKURAP")
                    {
                        txtTengkurap.Text = x.value;
                    }

                    if (x.pediatric_data_type.ToUpper() == "DUDUK")
                    {
                        txtDuduk.Text = x.value;
                    }

                    if (x.pediatric_data_type.ToUpper() == "MERANGKAK")
                    {
                        txtMerangkak.Text = x.value;
                    }

                    if (x.pediatric_data_type.ToUpper() == "BERDIRI")
                    {
                        txtBerdiri.Text = x.value;
                    }

                    if (x.pediatric_data_type.ToUpper() == "BERJALAN")
                    {
                        txtBerjalan.Text = x.value;
                    }

                    if (x.pediatric_data_type.ToUpper() == "BERBICARA")
                    {
                        txtBerbicara.Text = x.value;
                    }
                }

                //modal data

                if (x.pediatric_data_type.ToUpper() == "LAMA KEHAMILAN")
                {
                    lblmodallamahamil.Text = x.value;
                }

                if (x.pediatric_data_type.ToUpper() == "KOMPLIKASI KEHAMILAN")
                {
                    lblmodalkomplikasihamil.Text = x.value;
                }

                if (x.pediatric_data_type.ToUpper() == "RIWAYAT PERSALINAN")
                {
                    if (x.value == "Lain")
                    {
                        lblmodalriwayatpersalinan.Text = x.value + " (" + x.remarks + ")";
                    }
                    else
                    {
                        lblmodalriwayatpersalinan.Text = x.value;
                    }
                }

                if (x.pediatric_data_type.ToUpper() == "PENYULIT PERSALINAN")
                {
                    if (x.value == "Tidak ada")
                    {
                        lblmodalpenyulitpersalinan.Text = x.value;
                    }
                    else if (x.value == "Ada")
                    {
                        lblmodalpenyulitpersalinan.Text = x.value + " (" + x.remarks + ")";
                    }
                }

                if (x.pediatric_data_type.ToUpper() == "BERAT BADAN LAHIR")
                {
                    lblmodalbbl.Text = x.value + " gr";
                }

                if (x.pediatric_data_type.ToUpper() == "PANJANG BADAN LAHIR")
                {
                    lblmodalpbl.Text = x.value + " cm";
                }

            }
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Pediatric_Load", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    public void Emergency_Load(SOAP data)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        TextTglKeluar1.Attributes.Add("ReadOnly", "ReadOnly");
        TextTglKeluar2.Attributes.Add("ReadOnly", "ReadOnly");
        emergencySection.Visible = true;
        emergencySectionDisable.Visible = true;

        if (data.objective.Count > 0)
        {
            foreach (Objective x in data.objective)
            {

                if (x.soap_mapping_id == Guid.Parse("0B2096B0-08CF-4ACE-9E4F-80C9E4502FB7"))
                {   //"Tindak Lanjut"
                    ddl_tindaklanjut1.SelectedValue = x.value;
                    TextKetTindakLanjut1.Text = x.remarks;

                    ddl_tindaklanjut2.SelectedValue = x.value;
                    TextKetTindakLanjut2.Text = x.remarks;
                }

                else if (x.soap_mapping_id == Guid.Parse("1C636AF0-8815-49EB-8633-D2ECB1DC8278"))
                {   //"Keluar ED"
                    TextTglKeluar1.Text = x.value;
                    TextJamKeluar1.Text = x.remarks;

                    TextTglKeluar2.Text = x.value;
                    TextJamKeluar2.Text = x.remarks;
                }

                else if (x.soap_mapping_id == Guid.Parse("D93D6DE4-1664-471B-B25E-B74510C05DF4"))
                {   //"Kondisi Keluar"
                    if (x.value.ToUpper() == "0".ToUpper())
                    {
                        rbkpstabil1.Checked = true;
                        rbkpstabil2.Checked = true;
                    }
                    else if (x.value.ToUpper() == "1".ToUpper())
                    {
                        rbkptidakstabil1.Checked = true;
                        rbkptidakstabil2.Checked = true;
                    }
                    else if (x.value.ToUpper() == "2".ToUpper())
                    {
                        rbkpmeninggal1.Checked = true;
                        rbkpmeninggal2.Checked = true;
                    }
                }

                else if (x.soap_mapping_id == Guid.Parse("05C8CB0F-A092-4648-8D9F-B79828E7B12A"))
                {   //"EWS"
                    if (x.value.ToUpper() == "EWS".ToUpper())
                    {
                        rbews1.Checked = true;
                        TextEWS1.Text = x.remarks;

                        rbews2.Checked = true;
                        TextEWS2.Text = x.remarks;
                    }
                    else if (x.value.ToUpper() == "PEWS".ToUpper())
                    {
                        rbpews1.Checked = true;
                        TextPEWS1.Text = x.remarks;

                        rbpews2.Checked = true;
                        TextPEWS2.Text = x.remarks;
                    }
                    else if (x.value.ToUpper() == "MEWS".ToUpper())
                    {
                        rbmews1.Checked = true;
                        TextMEWS1.Text = x.remarks;

                        rbmews2.Checked = true;
                        TextMEWS2.Text = x.remarks;
                    }
                }
            }
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Emergency_Load", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    public void Emergency_GetValues(SOAP data, bool isAlreadySubmit)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        if (isAlreadySubmit == false)
        {
            foreach (Objective x in data.objective)
            {
                if (x.soap_mapping_id == Guid.Parse("0B2096B0-08CF-4ACE-9E4F-80C9E4502FB7"))
                {   //"Tindak Lanjut"
                    x.value = ddl_tindaklanjut1.SelectedValue;
                    x.remarks = TextKetTindakLanjut1.Text;
                }

                else if (x.soap_mapping_id == Guid.Parse("1C636AF0-8815-49EB-8633-D2ECB1DC8278"))
                {   //"Keluar ED"
                    x.value = TextTglKeluar1.Text;
                    x.remarks = TextJamKeluar1.Text;
                }

                else if (x.soap_mapping_id == Guid.Parse("D93D6DE4-1664-471B-B25E-B74510C05DF4"))
                {   //"Kondisi Keluar"
                    if (rbkpstabil1.Checked == true)
                    {
                        x.value = "0";
                        x.remarks = "Stabil";
                    }
                    else if (rbkptidakstabil1.Checked == true)
                    {
                        x.value = "1";
                        x.remarks = "Tidak Stabil";
                    }
                    else if (rbkpmeninggal1.Checked == true)
                    {
                        x.value = "2";
                        x.remarks = "Meninggal";
                    }
                }

                else if (x.soap_mapping_id == Guid.Parse("05C8CB0F-A092-4648-8D9F-B79828E7B12A"))
                {   //"EWS"
                    if (rbews1.Checked == true)
                    {
                        x.value = "EWS";
                        x.remarks = TextEWS1.Text;
                    }
                    else if (rbpews1.Checked == true)
                    {
                        x.value = "PEWS";
                        x.remarks = TextPEWS1.Text;
                    }
                    else if (rbmews1.Checked == true)
                    {
                        x.value = "MEWS";
                        x.remarks = TextMEWS1.Text;
                    }
                }
            }
        }
        else if (isAlreadySubmit == true)
        {
            foreach (Objective x in data.objective)
            {
                if (x.soap_mapping_id == Guid.Parse("0B2096B0-08CF-4ACE-9E4F-80C9E4502FB7"))
                {   //"Tindak Lanjut"
                    x.value = ddl_tindaklanjut2.SelectedValue;
                    x.remarks = TextKetTindakLanjut2.Text;
                }

                else if (x.soap_mapping_id == Guid.Parse("1C636AF0-8815-49EB-8633-D2ECB1DC8278"))
                {   //"Keluar ED"
                    x.value = TextTglKeluar2.Text;
                    x.remarks = TextJamKeluar2.Text;
                }

                else if (x.soap_mapping_id == Guid.Parse("D93D6DE4-1664-471B-B25E-B74510C05DF4"))
                {   //"Kondisi Keluar"
                    if (rbkpstabil2.Checked == true)
                    {
                        x.value = "0";
                        x.remarks = "Stabil";
                    }
                    else if (rbkptidakstabil2.Checked == true)
                    {
                        x.value = "1";
                        x.remarks = "Tidak Stabil";
                    }
                    else if (rbkpmeninggal2.Checked == true)
                    {
                        x.value = "2";
                        x.remarks = "Meninggal";
                    }
                }

                else if (x.soap_mapping_id == Guid.Parse("05C8CB0F-A092-4648-8D9F-B79828E7B12A"))
                {   //"EWS"
                    if (rbews2.Checked == true)
                    {
                        x.value = "EWS";
                        x.remarks = TextEWS2.Text;
                    }
                    else if (rbpews2.Checked == true)
                    {
                        x.value = "PEWS";
                        x.remarks = TextPEWS2.Text;
                    }
                    else if (rbmews2.Checked == true)
                    {
                        x.value = "MEWS";
                        x.remarks = TextMEWS2.Text;
                    }
                }
            }
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Emergency_GetValues", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());

    }

    public void ActionBackUpToSession()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]] = null;

        //SOAP soapmodel = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
        //SOAP soapmodelBackUp = new SOAP();
        //soapmodelBackUp = soapmodel;

        var soapmodel = MappingforGetdataSOAPSession();
        var soapmodelBackUp = soapmodel;

        soapmodelBackUp = AssignDataFromSoapPage(soapmodelBackUp);
        soapmodelBackUp.BackupDate = DateTime.Now.ToString();
        
        //versi session
        Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]] = soapmodelBackUp;

        ////versi localstorage
        ////hfsoapstringsavetolocal.Value = new JavaScriptSerializer().Serialize(soapmodelBackUp);
        //string data = new JavaScriptSerializer().Serialize(soapmodelBackUp);
        //ScriptManager.RegisterStartupScript(this, GetType(), "savelocal", "SaveSOAPtoLocal(" + data + ");", true);


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "ActionBackUpToSession", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }
    public dynamic AssignDataFromSoapPage(dynamic soapmodel)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        ((List<Subjective>)soapmodel.subjective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("f815de0e-e57a-4250-a2be-bdd27c1876ac"));

        if (hdnTindakan.Value != "")
        {
            List<Subjective> listTindakan = new JavaScriptSerializer().Deserialize<List<Subjective>>(hdnTindakan.Value);
            soapmodel.subjective.AddRange(listTindakan);
        }

        ((List<Subjective>)soapmodel.subjective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("0cbb6a1d-392b-4fed-8e8a-696bea3cbc32"));

        if (hdnDeleteReason.Value != "")
        {
            List<Subjective> listDeleteReason = new JavaScriptSerializer().Deserialize<List<Subjective>>(hdnDeleteReason.Value);
            soapmodel.subjective.AddRange(listDeleteReason);
        }
        ((List<Objective>)soapmodel.objective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c"));
        if (hdnFallRisk.Value != "")
        {
            List<Objective> listobjfallrisk = new JavaScriptSerializer().Deserialize<List<Objective>>(hdnFallRisk.Value);
            soapmodel.objective.AddRange(listobjfallrisk);
        }
        ((List<Objective>)soapmodel.objective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("B0C9D8E0-7533-43CD-BFCF-E8C0AC1D4B7E"));
        if (hdnFallRiskHandling.Value != "")
        {
            List<Objective> listobjfallriskHandling = new JavaScriptSerializer().Deserialize<List<Objective>>(hdnFallRiskHandling.Value);
            soapmodel.objective.AddRange(listobjfallriskHandling);
        }
        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf")).remarks = Complaint.Text;
        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0")).remarks = Anamnesis.Text;
        string radhamil = "";
        if (Radiohamilno.Checked == true)
        {
            radhamil = "false";
        }
        else if (Radiohamilyes.Checked == true)
        {
            radhamil = "true";
        }
        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")).value = radhamil;
        string radsusu = "";
        if (Radiosusuno.Checked == true)
        {
            radsusu = "false";
        }
        else if (Radiosusuyes.Checked == true)
        {
            radsusu = "true";
        }
        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")).value = radsusu;
        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade")).remarks = hdnEndemicArea.Value;
        if (rbnutrisi2.Checked)
            ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).remarks = hdnNutrition.Value;
        else if (rbnutrisi1.Checked)
            ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).remarks = "";
        if (rbpuasa2.Checked)
            ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).remarks = hdnFasting.Value;
        else if (rbpuasa1.Checked)
            ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).remarks = "";
        if (rbOperas2.Checked)
        {
            if (hdnhistorysurgery.Value != "")
            {
                List<PatientSurgery> tempsurgery = new JavaScriptSerializer().Deserialize<List<PatientSurgery>>(hdnhistorysurgery.Value);
                soapmodel.patient_surgery = tempsurgery;
            }
            else
                soapmodel.patient_medication.Clear();
            //soapmodel.patient_surgery = GetRowList_PatientSurgery();
        }
        else if (rbOperasi.Checked)
        {
            if (hdnhistorysurgery.Value != "")
            {
                List<PatientSurgery> tempsurgery = new JavaScriptSerializer().Deserialize<List<PatientSurgery>>(hdnhistorysurgery.Value);
                soapmodel.patient_surgery = tempsurgery;
            }
            else
                soapmodel.patient_medication.Clear();
        }
        else
        {
            soapmodel.patient_surgery.Clear();
        }
        if (soapmodel.patient_surgery == null)
            soapmodel.patient_surgery = new List<PatientSurgery>();
        if (rbProcOut2.Checked)
        {
            List<PatientProcedureHistory> tempprocout = new JavaScriptSerializer().Deserialize<List<PatientProcedureHistory>>(hdnProcedureOutside.Value);
            soapmodel.patient_procedure = tempprocout;
        }
        else if (rbProcOut1.Checked)
        {
            soapmodel.patient_procedure.Clear();
        }
        if (soapmodel.patient_procedure == null)
            soapmodel.patient_procedure = new List<PatientProcedureHistory>();
        List<PatientSpecialNotification> datareminder = GetRowList_ReminderNotes();
        hdnremindernotes.Value = new JavaScriptSerializer().Serialize(datareminder);
        List<PatientSpecialNotification> tempreminder = new JavaScriptSerializer().Deserialize<List<PatientSpecialNotification>>(hdnremindernotes.Value);
        soapmodel.patient_notification = tempreminder;
        if (soapmodel.patient_notification == null)
            soapmodel.patient_notification = new List<PatientSpecialNotification>();
        if (hfenableroutine.Value == "1")
        {
            if (hdnhistoryroutine.Value != "")
            {
                List<PatientRoutineMedication> tempcurrmed = new JavaScriptSerializer().Deserialize<List<PatientRoutineMedication>>(hdnhistoryroutine.Value);
                soapmodel.patient_medication = tempcurrmed;
            }
            else
                soapmodel.patient_medication.Clear();
        }
        else if (hfenableroutine.Value == "0")
        {
            if (hdnhistoryroutine.Value != "")
            {
                List<PatientRoutineMedication> tempcurrmed = new JavaScriptSerializer().Deserialize<List<PatientRoutineMedication>>(hdnhistoryroutine.Value);
                soapmodel.patient_medication = tempcurrmed;
            }
            else
                soapmodel.patient_medication.Clear();
        }
        else
        {
            soapmodel.patient_medication.Clear();
        }
        if (soapmodel.patient_medication == null)
            soapmodel.patient_medication = new List<PatientRoutineMedication>();
        List<PatientRoutineMedication> pengobatan_nodata = new List<PatientRoutineMedication>();
        PatientRoutineMedication row_pengobatan = new PatientRoutineMedication();
        row_pengobatan.patient_routine_medication_id = Guid.Empty;
        row_pengobatan.medication = "Tidak Ada Pengobatan";
        row_pengobatan.routine_sales_item_code = "";
        row_pengobatan.routine_sales_item_id = 0;
        row_pengobatan.is_delete = 0;
        if (hfenableroutine.Value == "0")
        {
            pengobatan_nodata.Add(row_pengobatan);
            soapmodel.patient_medication = pengobatan_nodata;
        }
        if (rbdrug2.Checked)
        {
            List<PatientAllergy> tempdrugsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistorydrugallergies.Value);
            soapmodel.patient_allergy = tempdrugsallergy;
            if (rbfood2.Checked)
            {
                soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(2));
            }
            if (rbother2.Checked)
            {
                soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(7));
            }
        }
        else if (rbfood2.Checked)
        {
            List<PatientAllergy> tempfoodsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryfoodallergies.Value);
            soapmodel.patient_allergy = tempfoodsallergy;
            if (rbother2.Checked)
            {
                soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(7));
            }
        }
        else if (rbother2.Checked)
        {
            List<PatientAllergy> tempothersallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryotherallergies.Value);
            soapmodel.patient_allergy = tempothersallergy;
        }
        else
        {
            soapmodel.patient_allergy.Clear();
        }
        if (soapmodel.patient_allergy == null)
            soapmodel.patient_allergy = new List<PatientAllergy>();
        //initial no data
        List<PatientAllergy> drug_noalergi = new List<PatientAllergy>();
        List<PatientAllergy> food_noalergi = new List<PatientAllergy>();
        List<PatientAllergy> other_noalergi = new List<PatientAllergy>();
        PatientAllergy rowdrug = new PatientAllergy();
        rowdrug.patient_allergy_id = Guid.Empty;
        rowdrug.allergy_type = 1;
        rowdrug.allergy = "Tidak Ada Alergi";
        rowdrug.allergy_reaction = "Tidak Ada Reaksi";
        rowdrug.is_delete = 0;
        PatientAllergy rowfood = new PatientAllergy();
        rowfood.patient_allergy_id = Guid.Empty;
        rowfood.allergy_type = 2;
        rowfood.allergy = "Tidak Ada Alergi";
        rowfood.allergy_reaction = "Tidak Ada Reaksi";
        rowfood.is_delete = 0;
        PatientAllergy rowother = new PatientAllergy();
        rowother.patient_allergy_id = Guid.Empty;
        rowother.allergy_type = 7;
        rowother.allergy = "Tidak Ada Alergi";
        rowother.allergy_reaction = "Tidak Ada Reaksi";
        rowother.is_delete = 0;
        //end initial no data
        //set if no allergy
        if (rbdrug1.Checked)
        {
            drug_noalergi.Add(rowdrug);
            soapmodel.patient_allergy.AddRange(drug_noalergi);
        }
        if (rbfood1.Checked)
        {
            food_noalergi.Add(rowfood);
            soapmodel.patient_allergy.AddRange(food_noalergi);
        }
        if (rbother1.Checked)
        {
            other_noalergi.Add(rowother);
            soapmodel.patient_allergy.AddRange(other_noalergi);
        }
        List<PatientDiseaseHistory> templistdisease = new List<PatientDiseaseHistory>();
        if (rbpribadi2.Checked)
        {
            if (hdnDiseaseHistory.Value != "")
            {
                List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);
                templistdisease = tempdisease;
            }
        }
        else if (rbpribadi1.Checked)
        {
            if (hdnDiseaseHistory.Value != "")
            {
                List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);
                templistdisease = tempdisease;
            }
        }
        else
        {
            soapmodel.patient_disease.Clear();
        }
        if (rbkeluarga2.Checked)
        {
            if (hdnFamilyDiseaseHistory.Value != "")
            {
                List<PatientDiseaseHistory> tempdiseasefam = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnFamilyDiseaseHistory.Value);
                templistdisease.AddRange(tempdiseasefam);
            }
        }
        else if (rbkeluarga1.Checked)
        {
            if (hdnFamilyDiseaseHistory.Value != "")
            {
                List<PatientDiseaseHistory> tempdiseasefam = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnFamilyDiseaseHistory.Value);
                templistdisease.AddRange(tempdiseasefam);
            }
        }
        soapmodel.patient_disease = templistdisease;
        if (soapmodel.patient_disease == null)
            soapmodel.patient_disease = new List<PatientDiseaseHistory>();
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("ae3ca8c2-eab0-41b6-9e3e-3ecb8071a9d0")).value = txtbloodlow.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e5efd220-b68e-4652-ad03-d56ef29fcebb")).value = txtbloodhigh.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("78cbb61f-4a11-470a-b770-1a44eb04357f")).value = txtpulserate.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e6ae2ea9-b321-4756-bf96-2dc232e4a7ba")).value = txtrespiratory.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e903246c-df95-4fe0-96d2-cf90c036d3d7")).value = txtspo.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("2eeca752-a2ea-4426-b3cf-c1ea3833bf30")).value = txttemperature.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("52ce9350-bfb2-4072-8893-d0c6cf8b3634")).value = txtweight.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("2a8dbddb-edfe-4704-876e-5a2d735bb541")).value = txtheight.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D")).remarks = txtOthers.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D")).value = "Abnormal";
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("A8E0013B-0443-4E7A-B670-4DB9362B40E4")).value = txtlingkarkepala.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("19516BE9-9D54-4E30-8E90-0A8B41F5BA66")).value = txtbmi.Text;
        if (lbleyetotal.Text == "_")
            ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("a9c5cd3c-1e02-4db2-a047-2f1983d1315b")).value = "";
        else
            ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("a9c5cd3c-1e02-4db2-a047-2f1983d1315b")).value = hdnEye.Value;
        if (lblmovetotal.Text == "_")
            ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("89b583a5-3003-43ab-9693-60ea6181c8d5")).value = "";
        else
            ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("89b583a5-3003-43ab-9693-60ea6181c8d5")).value = hdnMove.Value;
        if (lblverbaltotal.Text == "_")
            ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("fe4cf48c-17a6-4720-ad23-186517dd9c85")).value = "";
        else
            ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("fe4cf48c-17a6-4720-ad23-186517dd9c85")).value = hdnVerbal.Value;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176")).value = hdnpainscale.Value;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = hdnMentalStatus.Value;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).remarks = hdnMentalStatusremark.Value;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = hdnConsciousness.Value;
        //if (mental1.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Good Orientation";
        //else if (mental2.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Disorientated";
        //else if (mental3.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Cooperative";
        //else if (mental4.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Non Cooperative";
        //if (consciousness1.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Compos mentis";
        //else if (consciousness2.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Somnolent";
        //else if (consciousness3.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Stupor";
        //else if (consciousness4.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Coma";
        foreach (var assessment in ((List<Assessment>)soapmodel.assessment).Where(x => x.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")))
        {
            if (assessment.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47"))
            {//"Primary"
                assessment.remarks = txtPrimary.Text;
            }
        }
        foreach (var planning in ((List<Planning>)soapmodel.planning).Where(x => x.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6")))
        {
            if (planning.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
            {
                planning.remarks = txtPlanning.Text;
            }
        }
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")).remarks = txtHasilTindakan.Text;
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")).value = soapmodel.patient_id.ToString();
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("7198D4D5-7670-4853-9AFA-CD7C606C5C27")).value = "";
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("7198D4D5-7670-4853-9AFA-CD7C606C5C27")).remarks = hdnSchedule_travel.Value;
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3893BF09-82A2-449A-B5F0-D3A2BF9905C1")).value = hdnCondition_travel.Value;
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3893BF09-82A2-449A-B5F0-D3A2BF9905C1")).remarks = hdncondition_date.Value;
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("A4D139D7-67E2-44BC-A5E7-8BB17DD3F21E")).value = hdnSeating_type.Value;
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("A4D139D7-67E2-44BC-A5E7-8BB17DD3F21E")).remarks = "";
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("80A8E43A-53BF-4A32-B992-C9F32C7AA458")).value = hdnEscort_type.Value;
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("80A8E43A-53BF-4A32-B992-C9F32C7AA458")).remarks = hdnescort_ddl.Value;
        ((List<Planning>)soapmodel.planning).RemoveAll(x => x.soap_mapping_id == Guid.Parse("F3378407-B8A6-45FF-A27B-87F3CCF0F2B4"));
        if (hdnSpecial_Needs.Value != "")
        {
            List<Planning> listSpecialNeeds = new List<Planning>();
            var dataspecialneeds = hdnSpecial_Needs.Value.Split(',');
            for (int i = 0; i < dataspecialneeds.Count(); i++)
            {
                Planning P = new Planning();
                P.soap_mapping_id = Guid.Parse("F3378407-B8A6-45FF-A27B-87F3CCF0F2B4");
                P.soap_mapping_name = "SPECIAL_NEEDS";
                P.planning_id = Guid.Empty;
                P.value = dataspecialneeds[i].ToString();
                P.remarks = "";
                listSpecialNeeds.Add(P);
            }
            soapmodel.planning.AddRange(listSpecialNeeds);
        }
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("30F9892A-22C7-4C8B-8005-BE08E105F05F")).value = "";
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("30F9892A-22C7-4C8B-8005-BE08E105F05F")).remarks = txtTravelRecommendation.Text.ToString();

        soapmodel.procedure_notes = txtProcedure.Text;
        soapmodel.procedure_diagnosis = (List<ProcedureDiagnosis>)Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value];

        soapmodel = StdPlanning.GetPlanningValues(soapmodel);
        soapmodel = StdImunisasi.GetImunisasiValues(soapmodel);


        string templateid = Request.QueryString["PageSoapId"];
        if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {

        }
        else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
        {
            soapmodel = StdTriage.GetTriageValues(soapmodel);
        }
        else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
        {
            //soapmodel = StdObgyn.GetObgynValues(soapmodel);
            soapmodel = getObgynData(soapmodel);
        }
        else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
        {
            soapmodel = getPediatricData(soapmodel);
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "AssignDataFromSoapPage", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());

        return soapmodel;
    }


    public void setSoapTextHeight()
    {
        Complaint.Rows = Complaint.Text.Split('\n').Length;
        Anamnesis.Rows = Anamnesis.Text.Split('\n').Length;
        txtOthers.Rows = txtOthers.Text.Split('\n').Length;
        txtPrimary.Rows = txtPrimary.Text.Split('\n').Length;
        txtPlanning.Rows = txtPlanning.Text.Split('\n').Length;
        txtHasilTindakan.Rows = txtHasilTindakan.Text.Split('\n').Length;
        txtTravelRecommendation.Rows = txtTravelRecommendation.Text.Split('\n').Length;
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }

    public void GetDataTravel(List<Planning> listplanning)
    {
        var SCHEDULED_TRAVEL = listplanning.Find(y => y.soap_mapping_id == Guid.Parse("7198D4D5-7670-4853-9AFA-CD7C606C5C27"));
        var CONDITION_TRAVEL = listplanning.Find(y => y.soap_mapping_id == Guid.Parse("3893BF09-82A2-449A-B5F0-D3A2BF9905C1"));
        var SEATING_TYPE = listplanning.Find(y => y.soap_mapping_id == Guid.Parse("A4D139D7-67E2-44BC-A5E7-8BB17DD3F21E"));
        var ESCORT_TYPE = listplanning.Find(y => y.soap_mapping_id == Guid.Parse("80A8E43A-53BF-4A32-B992-C9F32C7AA458"));
        var SPECIAL_NEEDS = listplanning.Where(y => y.soap_mapping_id == Guid.Parse("F3378407-B8A6-45FF-A27B-87F3CCF0F2B4")).ToList();

        if(SCHEDULED_TRAVEL.remarks != "")
        {
            txtdatescheduletravel.Text = SCHEDULED_TRAVEL.remarks;
        }

        if (CONDITION_TRAVEL.value != "")
        {
            if (CONDITION_TRAVEL.value == "Fit to fly as scheduled")
            {
                rbtravel1.Checked = true;
                ScriptManager.RegisterStartupScript(Page, GetType(), "showcondition", "showdetailtravel();", true);
            }
            else if (CONDITION_TRAVEL.value == "Not fit to fly as scheduled")
            {
                rbtravel2.Checked = true;
                ScriptManager.RegisterStartupScript(Page, GetType(), "showcondition", "hidedetailtravel();", true);
            }
            else if (CONDITION_TRAVEL.value == "Anticipated date fit to fly")
            {
                rbtravel3.Checked = true;
                txtdatefittofly.Text = CONDITION_TRAVEL.remarks;
                ScriptManager.RegisterStartupScript(Page, GetType(), "showcondition", "showdetailtravelanticipated();", true);
            }
        }

        if (SEATING_TYPE.value != "")
        {
            if (SEATING_TYPE.value == "Commercial flight regular seating")
            {
                rbseating1.Checked = true;
            }
            else if (SEATING_TYPE.value == "Commercial flight Business class")
            {
                rbseating2.Checked = true;
            }
            else if (SEATING_TYPE.value == "Stretcher Case")
            {
                rbseating3.Checked = true;
            }
            else if (SEATING_TYPE.value == "Air-ambulance")
            {
                rbseating4.Checked = true;
            }
        }

        if (ESCORT_TYPE.value != "")
        {
            if (ESCORT_TYPE.value == "Unescorted")
            {
                rbescort1.Checked = true;
            }
            else if (ESCORT_TYPE.value == "non-Medical Escort")
            {
                rbescort2.Checked = true;
            }
            else if (ESCORT_TYPE.value == "Medical Escort")
            {
                rbescort3.Checked = true;
                ddlescort.SelectedItem.Text = ESCORT_TYPE.remarks;
            }
        }

        if (SPECIAL_NEEDS.Count != 0)
        {
            foreach (var x in SPECIAL_NEEDS)
            {
                if (x.value.ToUpper() == "Wheel Chair Assistance".ToUpper())
                {
                    chkSpecialNeeds1.Checked = true;
                }
                else if (x.value.ToUpper() == "Oxygen Supplementation".ToUpper())
                {
                    chkSpecialNeeds2.Checked = true;
                }
                else if (x.value.ToUpper() == "Need Mechanical Ventilation".ToUpper())
                {
                    chkSpecialNeeds3.Checked = true;
                }
                else if (x.value.ToUpper() == "Need Vacuum Mattress".ToUpper())
                {
                    chkSpecialNeeds4.Checked = true;
                }              
            }
        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "savetravel", "SaveTravel();", true);
        
    }

    public void CheckVisibleDiv()
    {
        if (rbPengobatan2.Checked)
            ScriptManager.RegisterStartupScript(UpdatePanelModalMedication, UpdatePanelModalMedication.GetType(), "alert1", "ShowHideDiv();", true);
        else
            ScriptManager.RegisterStartupScript(UpdatePanelModalMedication, UpdatePanelModalMedication.GetType(), "alert1", "HideDiv();", true);

        if (rbOperas2.Checked)
            ScriptManager.RegisterStartupScript(UpdatePanelModalIllness, UpdatePanelModalIllness.GetType(), "alert2", "ShowHideDiv7();", true);
        else
            ScriptManager.RegisterStartupScript(UpdatePanelModalIllness, UpdatePanelModalIllness.GetType(), "alert2", "HideDiv7();", true);

        if (rbProcOut2.Checked)
            ScriptManager.RegisterStartupScript(UpdatePanelModalIllness, UpdatePanelModalIllness.GetType(), "alertProcout", "ShowHideDivProcout();", true);
        else
            ScriptManager.RegisterStartupScript(UpdatePanelModalIllness, UpdatePanelModalIllness.GetType(), "alertProcout", "HideDivProcout();", true);

        if (rbpribadi2.Checked)
            ScriptManager.RegisterStartupScript(UpdatePanelModalIllness, UpdatePanelModalIllness.GetType(), "alert3", "ShowHideDiv2();", true);
        else
            ScriptManager.RegisterStartupScript(UpdatePanelModalIllness, UpdatePanelModalIllness.GetType(), "alert3", "HideDiv2();", true);

        if (rbkeluarga2.Checked)
            ScriptManager.RegisterStartupScript(UpdatePanelModalIllness, UpdatePanelModalIllness.GetType(), "alert4", "ShowHideDiv3();", true);
        else
            ScriptManager.RegisterStartupScript(UpdatePanelModalIllness, UpdatePanelModalIllness.GetType(), "alert4", "HideDiv3();", true);

        if (rbkunjungan2.Checked)
            ScriptManager.RegisterStartupScript(UpdatePanelModalEndemic, UpdatePanelModalEndemic.GetType(), "alert5", "ShowHideDiv4();", true);
        else
            ScriptManager.RegisterStartupScript(UpdatePanelModalEndemic, UpdatePanelModalEndemic.GetType(), "alert5", "HideDiv4();", true);

        if (rbnutrisi2.Checked)
            ScriptManager.RegisterStartupScript(UpdatePanelModalNutrition, UpdatePanelModalNutrition.GetType(), "alert8", "ShowHideDiv5();", true);
        else
            ScriptManager.RegisterStartupScript(UpdatePanelModalNutrition, UpdatePanelModalNutrition.GetType(), "alert8", "HideDiv5();", true);

        if (rbpuasa2.Checked)
            ScriptManager.RegisterStartupScript(UpdatePanelModalNutrition, UpdatePanelModalNutrition.GetType(), "alert9", "ShowHideDiv6();", true);
        else
            ScriptManager.RegisterStartupScript(UpdatePanelModalNutrition, UpdatePanelModalNutrition.GetType(), "alert9", "HideDiv6();", true);

        if (rbdrug2.Checked)
            ScriptManager.RegisterStartupScript(UpdatePanelModalMedication, UpdatePanelModalMedication.GetType(), "alert6", "ShowHideDiv8();", true);
        else
            ScriptManager.RegisterStartupScript(UpdatePanelModalMedication, UpdatePanelModalMedication.GetType(), "alert6", "HideDiv8();", true);

        if (rbfood2.Checked)
            ScriptManager.RegisterStartupScript(UpdatePanelModalMedication, UpdatePanelModalMedication.GetType(), "alert7", "ShowHideDiv9();", true);
        else
            ScriptManager.RegisterStartupScript(UpdatePanelModalMedication, UpdatePanelModalMedication.GetType(), "alert7", "HideDiv9();", true);

        if (rbother2.Checked)
            ScriptManager.RegisterStartupScript(UpdatePanelModalMedication, UpdatePanelModalMedication.GetType(), "alertOther", "ShowHideDivOtherAllergy();", true);
        else
            ScriptManager.RegisterStartupScript(UpdatePanelModalMedication, UpdatePanelModalMedication.GetType(), "alertOther", "HideDivOtherAllergy();", true);

        string valuePain = txtPainScale.Text;
        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "getProgressBar", "getProgressBar(" + valuePain + ");", true);
        
    }

    //bool MyParentMethod(object sender)
    //{
    //    DataTable dtroutinemed = Session["routinemed"] as DataTable;
    //    Session.Remove("routinemed");

    //    if (Session["routinedeleted"] != null)
    //    {
    //        string itemdeleted = Session["routinedeleted"].ToString();
    //        Session.Remove("routinedeleted");
    //    }

    //    return true;
    //}

    //bool MyParentMethodAllergy(object sender) {
    //    DataTable dtroutinemed = Session["routinemed"] as DataTable;
    //    Session.Remove("routinemed");
    //    StdPlanning.UpdateDrugAllergy(dtroutinemed);
    //    return true;
    //}

    //bool MyParentMethodAllergyFood(object sender)
    //{
    //    DataTable dtroutinemed = Session["routinemed"] as DataTable;
    //    Session.Remove("routinemed");
    //    StdPlanning.UpdateFoodAllergy(dtroutinemed);
    //    return true;
    //}

    protected void btnChoose_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            if (ddlForm_Type.SelectedValue == "7ccd0a7e-9001-48ff-8052-ed07cf5716bb")
            {
                List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                markerlist.Find(x => x.key == "IsChooseTemplate").value = "true";
                Session[Helper.SESSIONmarker] = markerlist;
            }
            
            Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], Request.QueryString["pagefaid"], ddlForm_Type.SelectedValue, Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
            Helper.ResponseRedirectSOAP(Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], Request.QueryString["pagefaid"], ddlForm_Type.SelectedValue, Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);

            Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]] = null;


            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnChoose_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnChoose_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnPreview_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            //SoapPagePreview.Visible = true;
            //SoapPagePreview.initializevalue(Helper.organizationId, long.Parse(hfPatientId.Value), long.Parse(hfAdmissionId.Value), Guid.Parse(hfEncounterId.Value));

            var localIPAdress = GetLocalIPAddress();
            string baseURLhttp = "http://" + localIPAdress + "/viewer";
            string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_Viewer"]; //"https://gtn-devws-01.siloamhospitals.com:2123"; //nanti akan ambil dari registry
            string url = baseURLhttps + "/Form/FormViewer/MedicalResumePatient.aspx?org_id=" + Helper.organizationId + "&ptn_id=" + hfPatientId.Value + "&adm_id=" + hfAdmissionId.Value + "&enc_id=" + hfEncounterId.Value  + "&pagesoap_id=" + hfPageSoapId.Value + "&headertype=1" + "&username=" + Helper.GetLoginUser(this);
            //string url = "http://" + localIPAdress + "/Viewer/Form/FormViewer/MedicalResumePatient.aspx?org_id=" + Helper.organizationId + "&ptn_id=" + hfPatientId.Value + "&adm_id=" + hfAdmissionId.Value + "&enc_id=" + hfEncounterId.Value  + "&pagesoap_id=" + hfPageSoapId.Value + "&headertype=1" + "&username=" + Helper.GetLoginUser(this);
            //var localIPAdress = "localhost:62383";
            //string url = "http://" + localIPAdress +  "/Form/FormViewer/MedicalResumePatient.aspx?org_id=" + Helper.organizationId + "&ptn_id=" + hfPatientId.Value + "&adm_id=" + hfAdmissionId.Value + "&enc_id=" + hfEncounterId.Value + "&pagesoap_id=" + hfPageSoapId.Value + "&headertype=1" + "&username=" + Helper.GetLoginUser(this); 
            IframeMedicalResumePatient.Src = url;
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnPreview_click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnPreview_click", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());

    }
    protected void DisableRowList(int type)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            if (type == 1)//gvw_routinemed
            {
                if (rbPengobatan2.Checked)
                    hfenableroutine.Value = "1";
                else if (rbPengobatan1.Checked)
                    hfenableroutine.Value = "0";
                else
                    hfenableroutine.Value = "-1";

                txtRoutineMed.Enabled = false;
                btnRoutineMed.Enabled = false;
                rbPengobatan1.Enabled = false;
                rbPengobatan2.Enabled = false;
                foreach (GridViewRow rows in gvw_routinemed.Rows)
                {
                    //ImageButton btndelete = (ImageButton)rows.FindControl("btndelete");
                    //btndelete.Enabled = false;
                    
                    gvw_routinemed.Enabled = false;
                    //rows.ForeColor = ColorTranslator.FromHtml("#969696");
                }
            }
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "DisableRowList", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "DisableRowList", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    

    protected void btnsubmitFA_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            List<PatientRoutineMedication> pengobatan_nodata = new List<PatientRoutineMedication>();
            PatientRoutineMedication row_pengobatan = new PatientRoutineMedication();
            row_pengobatan.patient_routine_medication_id = Guid.Empty;
            row_pengobatan.medication = "Tidak Ada Pengobatan";
            row_pengobatan.routine_sales_item_code = "";
            row_pengobatan.routine_sales_item_id = 0;
            row_pengobatan.is_delete = 0;

            //if (hfenableroutine.Value == "0")
            if (rbPengobatan1.Checked)
            {
                pengobatan_nodata.Add(row_pengobatan);

                DataTable dtpengobatan = Helper.ToDataTable(pengobatan_nodata);
                rptroutinemedication.DataSource = dtpengobatan;
                rptroutinemedication.DataBind();
                lblmodalnoroute.Style.Add("display", "none");
                StdPlanning.UpdateRoutineMedication(dtpengobatan);
                hdnhistoryroutine.Value = new JavaScriptSerializer().Serialize(pengobatan_nodata);
                //StdPlanning.UpdateRoutineMedication(null);
                //hdnhistoryroutine.Value = "";
                Session[Helper.Sessionroutinemedication + hfguidadditional.Value] = pengobatan_nodata;

                hfenableroutine.Value = "0";
            }
            else if (rbPengobatan2.Checked)
            {
                List<PatientRoutineMedication> data = GetRowList_PatientRoutineMedication();
                if (data.Count > 0)
                {
                    DataTable dttemproutine = Helper.ToDataTable(data);
                    //if (dttemproutine.Rows[0]["medication"].ToString() == "Tidak Ada Pengobatan")
                    //{
                    //    rptroutinemedication.DataSource = dttemproutine;
                    //    rptroutinemedication.DataBind();
                    //    lblmodalnoroute.Style.Add("display", "none");
                    //    StdPlanning.UpdateRoutineMedication(dttemproutine);
                    //    hdnhistoryroutine.Value = new JavaScriptSerializer().Serialize(data);

                    //    rbPengobatan1.Checked = true;
                    //    StdPlanning.UncheckRoutine(data);
                    //    Session[Helper.Sessionroutinemedication + hfguidadditional.Value] = data;
                    //}
                    //else
                    //{
                    rptroutinemedication.DataSource = dttemproutine;
                    rptroutinemedication.DataBind();
                    StdPlanning.UpdateRoutineMedication(dttemproutine);
                    lblmodalnoroute.Style.Add("display", "none");
                    hdnhistoryroutine.Value = new JavaScriptSerializer().Serialize(data);

                    StdPlanning.UncheckRoutine(data);
                    Session[Helper.Sessionroutinemedication + hfguidadditional.Value] = data;
                    hfenableroutine.Value = "1";
                    //}

                }
                else
                {
                    rptroutinemedication.DataSource = null;
                    rptroutinemedication.DataBind();
                    lblmodalnoroute.Visible = true;
                    StdPlanning.UpdateRoutineMedication(null);
                    hdnhistoryroutine.Value = "";
                    StdPlanning.UncheckRoutine(data);
                    Session[Helper.Sessionroutinemedication + hfguidadditional.Value] = null;
                    //rbPengobatan1.Checked = true;
                    hfenableroutine.Value = "-1";
                }
            }
            else
            {
                rptroutinemedication.DataSource = null;
                rptroutinemedication.DataBind();
                lblmodalnoroute.Style.Add("display", "");
                StdPlanning.UpdateRoutineMedication(null);
                hdnhistoryroutine.Value = "";
                Session[Helper.Sessionroutinemedication + hfguidadditional.Value] = null;
                hfenableroutine.Value = "-1";
            }

            //initial no data
            List<PatientAllergy> drug_noalergi = new List<PatientAllergy>();
            List<PatientAllergy> food_noalergi = new List<PatientAllergy>();
            List<PatientAllergy> other_noalergi = new List<PatientAllergy>();
            PatientAllergy rowdrug = new PatientAllergy();
            rowdrug.patient_allergy_id = Guid.Empty;
            rowdrug.allergy_type = 1;
            rowdrug.allergy = "Tidak Ada Alergi";
            rowdrug.allergy_reaction = "Tidak Ada Reaksi";
            rowdrug.is_delete = 0;
            PatientAllergy rowfood = new PatientAllergy();
            rowfood.patient_allergy_id = Guid.Empty;
            rowfood.allergy_type = 2;
            rowfood.allergy = "Tidak Ada Alergi";
            rowfood.allergy_reaction = "Tidak Ada Reaksi";
            rowfood.is_delete = 0;
            PatientAllergy rowother = new PatientAllergy();
            rowother.patient_allergy_id = Guid.Empty;
            rowother.allergy_type = 7;
            rowother.allergy = "Tidak Ada Alergi";
            rowother.allergy_reaction = "Tidak Ada Reaksi";
            rowother.is_delete = 0;
            //end initial no data

            if (rbdrug1.Checked)
            {
                drug_noalergi.Add(rowdrug);

                rptdrugallergies.DataSource = Helper.ToDataTable(drug_noalergi);
                rptdrugallergies.DataBind();
                lblmodalnodrug.Style.Add("display", "none");
                StdPlanning.UpdateDrugAllergy(Helper.ToDataTable(drug_noalergi));
                hdnhistorydrugallergies.Value = new JavaScriptSerializer().Serialize(drug_noalergi);
                //StdPlanning.UpdateDrugAllergy(null);
                //hdnhistorydrugallergies.Value = "";
            }
            else if (rbdrug2.Checked)
            {
                List<PatientAllergy> data = GetRowList_PatientAllergy(1);
                if (data.Count > 0)
                {
                    rptdrugallergies.DataSource = Helper.ToDataTable(data);
                    rptdrugallergies.DataBind();
                    lblmodalnodrug.Style.Add("display", "none");
                    StdPlanning.UpdateDrugAllergy(Helper.ToDataTable(data));
                    hdnhistorydrugallergies.Value = new JavaScriptSerializer().Serialize(data);
                }
                else
                {
                    rptdrugallergies.DataSource = null;
                    rptdrugallergies.DataBind();
                    lblmodalnodrug.Visible = true;
                    StdPlanning.UpdateDrugAllergy(null);
                    hdnhistorydrugallergies.Value = "";
                }
            }

            if (rbfood1.Checked)
            {
                food_noalergi.Add(rowfood);

                rptfoodallergies.DataSource = Helper.ToDataTable(food_noalergi);
                rptfoodallergies.DataBind();
                lblmodalnofood.Style.Add("display", "none");
                StdPlanning.UpdateFoodAllergy(Helper.ToDataTable(food_noalergi));
                hdnhistoryfoodallergies.Value = new JavaScriptSerializer().Serialize(food_noalergi);
                //StdPlanning.UpdateFoodAllergy(null);
                //hdnhistoryfoodallergies.Value = "";
            }
            else if (rbfood2.Checked)
            {
                List<PatientAllergy> data = GetRowList_PatientAllergy(2);
                if (data.Count > 0)
                {
                    rptfoodallergies.DataSource = Helper.ToDataTable(data);
                    rptfoodallergies.DataBind();
                    lblmodalnofood.Style.Add("display", "none");
                    StdPlanning.UpdateFoodAllergy(Helper.ToDataTable(data));
                    hdnhistoryfoodallergies.Value = new JavaScriptSerializer().Serialize(data);
                }
                else
                {
                    rptfoodallergies.DataSource = null;
                    rptfoodallergies.DataBind();
                    lblmodalnofood.Style.Add("display", "");
                    StdPlanning.UpdateFoodAllergy(null);
                    hdnhistoryfoodallergies.Value = "";
                }
            }

            if (rbother1.Checked)
            {
                other_noalergi.Add(rowother);

                rptotherallergies.DataSource = Helper.ToDataTable(other_noalergi);
                rptotherallergies.DataBind();
                lblmodalnoother.Style.Add("display", "none");
                StdPlanning.UpdateOtherAllergy(Helper.ToDataTable(other_noalergi));
                hdnhistoryotherallergies.Value = new JavaScriptSerializer().Serialize(other_noalergi);
                //StdPlanning.UpdateOtherAllergy(null);
                //hdnhistoryotherallergies.Value = "";
            }
            else if (rbother2.Checked)
            {
                List<PatientAllergy> data = GetRowList_PatientAllergy(7);
                if (data.Count > 0)
                {
                    rptotherallergies.DataSource = Helper.ToDataTable(data);
                    rptotherallergies.DataBind();
                    lblmodalnoother.Style.Add("display", "none");
                    StdPlanning.UpdateOtherAllergy(Helper.ToDataTable(data));
                    hdnhistoryotherallergies.Value = new JavaScriptSerializer().Serialize(data);
                }
                else
                {
                    rptotherallergies.DataSource = null;
                    rptotherallergies.DataBind();
                    lblmodalnoother.Style.Add("display", "");
                    StdPlanning.UpdateOtherAllergy(null);
                    hdnhistoryotherallergies.Value = "";
                }
            }

            if (rbdrug1.Checked == true && rbfood1.Checked == true && rbother1.Checked == true)
            {
                StdPlanning.UpdateAllergyIsNo("No");
            }
            else if (rbdrug1.Checked == false && rbfood1.Checked == false && rbother1.Checked == false && rbdrug2.Checked == false && rbfood2.Checked == false && rbother2.Checked == false)
            {
                StdPlanning.UpdateAllergyIsNo("Unknown");
            }
            else if (rbdrug2.Checked == true || rbfood2.Checked == true || rbother2.Checked == true)
            {
                StdPlanning.UpdateAllergyIsNo("Yes");
            }

            UP_FA_MedicationAllergies.Update();
            StdPlanning.UpdateListAllergy();
            StdPlanning.UpdateListRoutine();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalEditMedication", "HidePreviewMedication();", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnsubmitFA_click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnsubmitFA_click", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnsubmitnutrition_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            if (rbnutrisi1.Checked)
            {
                lblnutrition.Visible = false;
                lblmodalnonutrition.Style.Add("display", "");
                hdnNutrition.Value = "";

                rbkunjungan1.Checked = true;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript5", "ShowHideDiv5();", true);
            }
            else if (rbnutrisi2.Checked)
            {
                if (txtNutrition.Text != "")
                {
                    lblnutrition.Visible = true;
                    lblmodalnonutrition.Style.Add("display", "none");
                    hdnNutrition.Value = txtNutrition.Text;
                    lblnutrition.Text = "Yes ~ " + txtNutrition.Text;

                    rbnutrisi2.Checked = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript5", "HideDiv5();", true);
                }
                else
                {
                    lblnutrition.Visible = false;
                    lblmodalnonutrition.Style.Add("display", "");
                    hdnNutrition.Value = "";

                    rbkunjungan1.Checked = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript5", "ShowHideDiv5();", true);
                }
            }

            if (rbpuasa1.Checked)
            {
                lblfasting.Visible = false;
                lblmodalnofasting.Style.Add("display", "");
                hdnFasting.Value = "";
                rbpuasa1.Checked = true;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript6", "ShowHideDiv6();", true);
            }
            else if (rbpuasa2.Checked)
            {
                if (txtFasting.Text != "")
                {
                    lblfasting.Visible = true;
                    lblmodalnofasting.Style.Add("display", "none");
                    lblfasting.Text = "Yes ~ " + txtFasting.Text;
                    hdnFasting.Value = txtFasting.Text;
                    rbpuasa2.Checked = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript6", "HideDiv6();", true);
                }
                else
                {
                    lblfasting.Visible = false;
                    lblmodalnofasting.Style.Add("display", "");
                    hdnFasting.Value = "";
                    rbpuasa1.Checked = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript6", "ShowHideDiv6();", true);
                }
                
            }

            UP_FA_Nutrition.Update();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalEditNutrition", "$('#modalEditNutrition').modal('hide');", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnsubmitnutrition_click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnsubmitnutrition_click", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

    }

    protected void btnsubmitendemic_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            SOAP soapmodel = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
            List<InfectiousDisease> listInfectiousDiseases = soapmodel.infectious_disease;
            List<InfectiousAlert> listInfectiousAlert = soapmodel.infectious_alert;

            foreach (var id in listInfectiousDiseases)
            {
                //Batuk Darah
                if (chkScreen1.Checked && id.infectious_symptoms_id == 1 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen1.Checked && id.infectious_symptoms_id == 1 && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {


                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 1 && ((!chkScreen1.Checked && id.is_new == true) || (chkScreen1.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Keringat di Malam Hari
                if (chkScreen2.Checked && id.infectious_symptoms_id == 2 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen2.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 2)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 2 && ((!chkScreen2.Checked && id.is_new == true) || (chkScreen2.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }
                //Pembengkakan Kelenjar leher
                if (chkScreen3.Checked && id.infectious_symptoms_id == 3 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen3.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 3)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 3 && ((!chkScreen3.Checked && id.is_new == true) || (chkScreen3.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Batuk lebih dari 2 minggu
                if (chkScreen4.Checked && id.infectious_symptoms_id == 4 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen4.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 4)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 4 && ((!chkScreen4.Checked && id.is_new == true) || (chkScreen4.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Riwayat MRSA
                if (chkScreen5.Checked && id.infectious_symptoms_id == 5 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen5.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 5)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 5 && ((!chkScreen5.Checked && id.is_new == true) || (chkScreen5.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Terpasang alat invasif
                if (chkScreen6.Checked && id.infectious_symptoms_id == 6 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen6.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 6)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 6 && ((!chkScreen6.Checked && id.is_new == true) || (chkScreen6.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Kontak dengan pasien MRSA
                if (chkScreen7.Checked && id.infectious_symptoms_id == 7 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen7.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 7)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 7 && ((!chkScreen7.Checked && id.is_new == true) || (chkScreen7.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Kaku kuduk
                if (chkScreen8.Checked && id.infectious_symptoms_id == 8 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen8.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 8)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 8 && ((!chkScreen8.Checked && id.is_new == true) || (chkScreen8.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Photophobia
                if (chkScreen9.Checked && id.infectious_symptoms_id == 9 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen9.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 9)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 9 && ((!chkScreen9.Checked && id.is_new == true) || (chkScreen9.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Demam
                if (chkScreen10.Checked && id.infectious_symptoms_id == 10 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen10.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 10)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 10 && ((!chkScreen10.Checked && id.is_new == true) || (chkScreen10.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Kenaikan suhu tubuh > 38C
                if (chkScreen11.Checked && id.infectious_symptoms_id == 11 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen11.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 11)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 11 && ((!chkScreen11.Checked && id.is_new == true) || (chkScreen11.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Nyeri sendi/otot
                if (chkScreen12.Checked && id.infectious_symptoms_id == 12 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen12.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 12)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 12 && ((!chkScreen12.Checked && id.is_new == true) || (chkScreen12.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Riwayat berpergian
                if (chkScreen13.Checked && id.infectious_symptoms_id == 13 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen13.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 13)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 13 && ((!chkScreen13.Checked && id.is_new == true) || (chkScreen13.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Diare, mual, dan muntah
                if (chkScreen14.Checked && id.infectious_symptoms_id == 14 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen14.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 14)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 14 && ((!chkScreen14.Checked && id.is_new == true) || (chkScreen14.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Luka terbuka dan bernanah/pus
                if (chkScreen15.Checked && id.infectious_symptoms_id == 15 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen15.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 15)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 15 && ((!chkScreen15.Checked && id.is_new == true) || (chkScreen15.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Suspect chickenpox/measles disertai batuk dan demam
                if (chkScreen16.Checked && id.infectious_symptoms_id == 16 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen16.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 16)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 16 && ((!chkScreen16.Checked && id.is_new == true) || (chkScreen16.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Suspect Meningococcus
                if (chkScreen17.Checked && id.infectious_symptoms_id == 17 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen17.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 17)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 17 && ((!chkScreen17.Checked && id.is_new == true) || (chkScreen17.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }

                //Kontak erat dengan pasien COVID-19
                if (chkScreen21.Checked && id.infectious_symptoms_id == 21 && id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    id.is_new = true;
                    id.is_delete = false;
                }
                else if (!chkScreen21.Checked && id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.infectious_symptoms_id == 21)
                {
                    id.is_delete = true;
                    id.is_new = false;
                }
                else if (id.infectious_symptoms_id == 21 && ((!chkScreen21.Checked && id.is_new == true) || (chkScreen21.Checked && id.is_delete == true)))
                {
                    id.is_delete = false;
                    id.is_new = false;
                }
            }

            List<Subjective> tempsubjectiveDelete = new List<Subjective>();

            ((List<Subjective>)soapmodel.subjective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("0CBB6A1D-392B-4FED-8E8A-696BEA3CBC32") && x.value == "");
            foreach (var ia in listInfectiousAlert)
            {
                if (chkKewaspadaanStandard.Checked && ia.alert_type_id == 1 && ia.patient_alert_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    ia.is_new = true;
                    ia.is_delete = false;
                }
                else if (!chkKewaspadaanStandard.Checked && ia.patient_alert_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && ia.alert_type_id == 1)
                {
                    ia.is_delete = true;
                    ia.is_new = false;
                    Subjective tempDeleteReason = new Subjective();
                    tempDeleteReason.subjective_id = Guid.Empty;
                    tempDeleteReason.soap_mapping_id = Guid.Parse("0CBB6A1D-392B-4FED-8E8A-696BEA3CBC32");
                    tempDeleteReason.soap_mapping_name = "INFECTIOUS ALERT DELETE REASON";
                    tempDeleteReason.value = "standard";
                    tempDeleteReason.remarks = HF_DeleteReasonS.Value;
                    tempsubjectiveDelete.Add(tempDeleteReason);
                }
                else if (ia.alert_type_id == 1 && ((!chkKewaspadaanStandard.Checked && ia.is_new == true) || (chkKewaspadaanStandard.Checked && ia.is_delete == true)))
                {
                    ia.is_delete = false;
                    ia.is_new = false;
                }

                if (chkKewaspadaanKontak.Checked && ia.alert_type_id == 2 && ia.patient_alert_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    ia.is_new = true;
                    ia.is_delete = false;
                }
                else if (!chkKewaspadaanKontak.Checked && ia.patient_alert_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && ia.alert_type_id == 2)
                {
                    ia.is_delete = true;
                    ia.is_new = false;
                    Subjective tempDeleteReason = new Subjective();
                    tempDeleteReason.subjective_id = Guid.Empty;
                    tempDeleteReason.soap_mapping_id = Guid.Parse("0CBB6A1D-392B-4FED-8E8A-696BEA3CBC32");
                    tempDeleteReason.soap_mapping_name = "INFECTIOUS ALERT DELETE REASON";
                    tempDeleteReason.value = "kontak";
                    tempDeleteReason.remarks = HF_DeleteReasonK.Value;
                    tempsubjectiveDelete.Add(tempDeleteReason);
                }
                else if (ia.alert_type_id == 2 && ((!chkKewaspadaanKontak.Checked && ia.is_new == true) || (chkKewaspadaanKontak.Checked && ia.is_delete == true)))
                {
                    ia.is_delete = false;
                    ia.is_new = false;
                }

                if (chkKewaspadaanDroplet.Checked && ia.alert_type_id == 3 && ia.patient_alert_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    ia.is_new = true;
                    ia.is_delete = false;
                }
                else if (!chkKewaspadaanDroplet.Checked && ia.patient_alert_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && ia.alert_type_id == 3)
                {
                    ia.is_delete = true;
                    ia.is_new = false;
                    Subjective tempDeleteReason = new Subjective();
                    tempDeleteReason.subjective_id = Guid.Empty;
                    tempDeleteReason.soap_mapping_id = Guid.Parse("0CBB6A1D-392B-4FED-8E8A-696BEA3CBC32");
                    tempDeleteReason.soap_mapping_name = "INFECTIOUS ALERT DELETE REASON";
                    tempDeleteReason.value = "droplet";
                    tempDeleteReason.remarks = HF_DeleteReasonD.Value;
                    tempsubjectiveDelete.Add(tempDeleteReason);
                }
                else if (ia.alert_type_id == 3 && ((!chkKewaspadaanDroplet.Checked && ia.is_new == true) || (chkKewaspadaanDroplet.Checked && ia.is_delete == true)))
                {
                    ia.is_delete = false;
                    ia.is_new = false;
                }

                if (chkKewaspadaanAirborne.Checked && ia.alert_type_id == 4 && ia.patient_alert_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    ia.is_new = true;
                    ia.is_delete = false;
                }
                else if (!chkKewaspadaanAirborne.Checked && ia.patient_alert_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && ia.alert_type_id == 4)
                {
                    ia.is_delete = true;
                    ia.is_new = false;
                    Subjective tempDeleteReason = new Subjective();
                    tempDeleteReason.subjective_id = Guid.Empty;
                    tempDeleteReason.soap_mapping_id = Guid.Parse("0CBB6A1D-392B-4FED-8E8A-696BEA3CBC32");
                    tempDeleteReason.soap_mapping_name = "INFECTIOUS ALERT DELETE REASON";
                    tempDeleteReason.value = "airborne";
                    tempDeleteReason.remarks = HF_DeleteReasonA.Value;
                    tempsubjectiveDelete.Add(tempDeleteReason);
                }
                else if (ia.alert_type_id == 4 && ((!chkKewaspadaanAirborne.Checked && ia.is_new == true) || (chkKewaspadaanAirborne.Checked && ia.is_delete == true)))
                {
                    ia.is_delete = false;
                    ia.is_new = false;
                }
            }

            List<InfectiousDisease> templistscreening = soapmodel.infectious_disease.Where(x => (x.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_delete == false) || (x.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_new == true)).ToList();
            if (templistscreening.Count > 0)
            {
                rptscreening.DataSource = Helper.ToDataTable(templistscreening);
                rptscreening.DataBind();
                lblmodalnoscreening.Style.Add("display", "none");

            }
            else
            {
                rptscreening.DataSource = null;
                rptscreening.DataBind();
                lblmodalnoscreening.Style.Add("display", "");
            }

			String[] tempArrayAlert = soapmodel.infectious_alert.Where(x => (x.patient_alert_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_delete == false) || (x.patient_alert_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_new == true)).Select(x => x.alert_type_name).Distinct().ToArray();
			if (tempArrayAlert.Length > 0)
			{
                divKewaspadaan2.Style.Add("display", "");
                var infectiousAlert = String.Join(", ", tempArrayAlert);
				listKewaspadaan2.InnerText = infectiousAlert;
			}
			else
			{
                divKewaspadaan2.Style.Add("display", "none");
                listKewaspadaan2.InnerText = "-";

			}


			List<InfectiousAlert> templistinfectiousalert = soapmodel.infectious_alert.Where(x => (x.patient_alert_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_delete == false) || (x.patient_alert_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_new == true)).ToList();
            if (templistinfectiousalert.Count > 0)
            {
                rptInfectiousAlert.DataSource = Helper.ToDataTable(templistinfectiousalert);
                rptInfectiousAlert.DataBind();
                lblmodalnoalert.Style.Add("display", "none");

            }
            else
            {

                rptInfectiousAlert.DataSource = null;
                rptInfectiousAlert.DataBind();
                lblmodalnoalert.Style.Add("display", "");
            }

            ((List<Subjective>)soapmodel.subjective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("F815DE0E-E57A-4250-A2BE-BDD27C1876AC") && x.value == "");
            List<Subjective> tempsubjectiveTindakan = new List<Subjective>();

            if (chkTindakan1.Checked)
            {
                Subjective tempTindakan = new Subjective();
                tempTindakan.subjective_id = Guid.Empty;
                tempTindakan.soap_mapping_id = Guid.Parse("F815DE0E-E57A-4250-A2BE-BDD27C1876AC");
                tempTindakan.soap_mapping_name = "TINDAKAN PASIEN INFEKSIUS";
                tempTindakan.value = "segregasi";
                tempTindakan.remarks = "Berikan jarak lebih dari 2 meter dari pasien non infeksi(Segregasi)";
                tempsubjectiveTindakan.Add(tempTindakan);

            }

            if (chkTindakan2.Checked)
            {
                Subjective tempTindakan = new Subjective();
                tempTindakan.subjective_id = Guid.Empty;
                tempTindakan.soap_mapping_id = Guid.Parse("F815DE0E-E57A-4250-A2BE-BDD27C1876AC");
                tempTindakan.soap_mapping_name = "TINDAKAN PASIEN INFEKSIUS";
                tempTindakan.value = "masker";
                tempTindakan.remarks = "Meminta pasien menggunakan masker";
                tempsubjectiveTindakan.Add(tempTindakan);

            }

            if (chkTindakan3.Checked)
            {
                Subjective tempTindakan = new Subjective();
                tempTindakan.subjective_id = Guid.Empty;
                tempTindakan.soap_mapping_id = Guid.Parse("F815DE0E-E57A-4250-A2BE-BDD27C1876AC");
                tempTindakan.soap_mapping_name = "TINDAKAN PASIEN INFEKSIUS";
                tempTindakan.value = "kewaspadaan";
                tempTindakan.remarks = "Mengkaji pasien dengan kewaspadaan";
                tempsubjectiveTindakan.Add(tempTindakan);

            }


            if (tempsubjectiveDelete.Count > 0)
            {
                hdnDeleteReason.Value = new JavaScriptSerializer().Serialize(tempsubjectiveDelete);

            }
            else
            {
                hdnDeleteReason.Value = "";
            }

            if (tempsubjectiveTindakan.Count > 0)
            {
                hdnTindakan.Value = new JavaScriptSerializer().Serialize(tempsubjectiveTindakan);

            }
            else
            {
                hdnTindakan.Value = "";
            }

            Session[Helper.Sessionsoapmodel + hfguidadditional.Value] = soapmodel;


            if (rbkunjungan1.Checked)
            {
                lblmodalendemic.Visible = false;
                lblmodalnoendemic.Style.Add("display", "");
                hdnEndemicArea.Value = "";
                rbkunjungan1.Checked = true;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript4", "HideDiv4();", true);
            }
            else if (rbkunjungan2.Checked)
            {
                if (txtEndemic.Text != "")
                {
                    lblmodalendemic.Visible = true;
                    lblmodalnoendemic.Style.Add("display", "none");
                    lblmodalendemic.Text = "Yes ~ " + txtEndemic.Text;
                    hdnEndemicArea.Value = txtEndemic.Text;
                    rbkunjungan2.Checked = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript4", "ShowHideDiv4();", true);
                }
                else
                {
                    lblmodalendemic.Visible = false;
                    lblmodalnoendemic.Style.Add("display", "");
                    hdnEndemicArea.Value = "";
                    rbkunjungan1.Checked = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript4", "HideDiv4();", true);
                }
            }

            if (rbskriningyes.Checked)
            {
                if (chkScreen1.Checked || chkScreen2.Checked || chkScreen3.Checked || chkScreen4.Checked || chkScreen5.Checked || chkScreen6.Checked || chkScreen7.Checked
                    || chkScreen8.Checked || chkScreen9.Checked || chkScreen10.Checked || chkScreen11.Checked || chkScreen12.Checked || chkScreen13.Checked || chkScreen14.Checked
                    || chkScreen15.Checked || chkScreen16.Checked || chkScreen17.Checked || chkScreen21.Checked || chkKewaspadaanStandard.Checked || chkKewaspadaanKontak.Checked
                    || chkKewaspadaanDroplet.Checked || chkKewaspadaanAirborne.Checked || chkTindakan1.Checked || chkTindakan2.Checked || chkTindakan3.Checked)
                {
                    rbskriningyes.Checked = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showSkrining", "ShowSkrining();", true);
                }
                else
                {
                    rbskriningno.Checked = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideSkrining", "HideSkrining();", true);

                }
            }
            else
            {
                rbskriningno.Checked = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideSkrining", "HideSkrining();", true);
            }

            UP_FA_Endemic.Update();
            UP_Kewaspadaan.Update();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalEditEndemic", "$('#modalEditEndemic').modal('hide');", true);

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnsubmitendemic_click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnsubmitendemic_click", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

    }

    protected void btnsubmitphysical_onclick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            string eye = "", move = "", verbal = "";
            if (eye4.Checked)
            {
                //eye4.Checked = true;
                lbleye.Text = "1. None";
                eye = "1";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (eye3.Checked)
            {
                //eye3.Checked = true;
                lbleye.Text = "2. To Pressure";
                eye = "2";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (eye2.Checked)
            {
                //eye2.Checked = true;
                lbleye.Text = "3. To Sound";
                eye = "3";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (eye1.Checked)
            {
                //eye1.Checked = true;
                lbleye.Text = "4. Spontaneus";
                eye = "4";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            hdnEye.Value = eye;

            if (move6.Checked)
            {
                //move6.Checked = true;
                lblmove.Text = "1. None";
                move = "1";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (move5.Checked)
            {
                //move5.Checked = true;
                lblmove.Text = "2. Extension";
                move = "2";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (move4.Checked)
            {
                //move4.Checked = true;
                lblmove.Text = "3. Flexion to pain stumulus";
                move = "3";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (move3.Checked)
            {
                //move3.Checked = true;
                lblmove.Text = "4. Withdrawns from pain";
                move = "4";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (move2.Checked)
            {
                //move2.Checked = true;
                lblmove.Text = "5. Localizes to pain stimulus";
                move = "5";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (move1.Checked)
            {
                //move1.Checked = true;
                lblmove.Text = "6. Obey Commands";
                move = "6";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            hdnMove.Value = move;

            if (verbal5.Checked)
            {
                //verbal5.Checked = true;
                lblverbal.Text = "1. None";
                verbal = "1";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (verbal4.Checked)
            {
                //verbal4.Checked = true;
                lblverbal.Text = "2. Incomprehensible sounds";
                verbal = "2";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (verbal3.Checked)
            {
                //verbal3.Checked = true;
                lblverbal.Text = "3. Inappropriate words";
                verbal = "3";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (verbal2.Checked)
            {
                //verbal2.Checked = true;
                lblverbal.Text = "4. Confused";
                verbal = "4";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (verbal1.Checked)
            {
                //verbal1.Checked = true;
                lblverbal.Text = "5. Orientated";
                verbal = "5";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (verbal6.Checked)
            {
                //verbal6.Checked = true;
                lblverbal.Text = "T. Tracheostomy";
                verbal = "T";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            else if (verbal7.Checked)
            {
                //verbal7.Checked = true;
                lblverbal.Text = "A. Aphasia";
                verbal = "A";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
            }
            hdnVerbal.Value = verbal;


            if (eye != "" && move != "" && verbal != "")
            {
                if (verbal != "T" && verbal != "A")
                {
                    var total1 = int.Parse(eye);
                    var total2 = int.Parse(move);
                    var total3 = int.Parse(verbal);

                    lblScore.Text = (total1 + total2 + total3).ToString();
                }
                else
                    lblScore.Text = "_";
            }
            else
                lblScore.Text = "_";

            txtPain.Text = txtPainScale.Text;
            //txtPainScale.ReadOnly = true;
            hdnpainscale.Value = txtPainScale.Text;

            if (mental1.Checked)
            {
                hdnMentalStatus.Value = "Good Orientation";
                hdnMentalStatusremark.Value = "Orientasi baik";
                
                if (HFisBahasaSOAP.Value == "ENG")
                {
                    lblmentalstatus.Text = hdnMentalStatus.Value;
                }
                else if (HFisBahasaSOAP.Value == "IND")
                {
                    lblmentalstatus.Text = hdnMentalStatusremark.Value;
                }
            }
            else if (mental2.Checked)
            {
                hdnMentalStatus.Value = "Disorientated";
                hdnMentalStatusremark.Value = "Disorientasi";

                if (HFisBahasaSOAP.Value == "ENG")
                {
                    lblmentalstatus.Text = hdnMentalStatus.Value;
                }
                else if (HFisBahasaSOAP.Value == "IND")
                {
                    lblmentalstatus.Text = hdnMentalStatusremark.Value;
                }
            }
            else if (mental3.Checked)
            {
                hdnMentalStatus.Value = "Cooperative";
                hdnMentalStatusremark.Value = "Kooperatif";

                if (HFisBahasaSOAP.Value == "ENG")
                {
                    lblmentalstatus.Text = hdnMentalStatus.Value;
                }
                else if (HFisBahasaSOAP.Value == "IND")
                {
                    lblmentalstatus.Text = hdnMentalStatusremark.Value;
                }
            }
            else if (mental4.Checked)
            {
                hdnMentalStatus.Value = "Non Cooperative";
                hdnMentalStatusremark.Value = "Tidak Kooperatif";

                if (HFisBahasaSOAP.Value == "ENG")
                {
                    lblmentalstatus.Text = hdnMentalStatus.Value;
                }
                else if (HFisBahasaSOAP.Value == "IND")
                {
                    lblmentalstatus.Text = hdnMentalStatusremark.Value;
                }
            }
            else
                lblmentalstatus.Text = "N/A";



            
            
            if (hdnMentalStatus.Value == "N/A")
                hdnMentalStatus.Value = "";

            if (consciousness1.Checked)
                lblconsciousness.Text = "Compos mentis";
            else if (consciousness2.Checked)
                lblconsciousness.Text = "Somnolent";
            else if (consciousness3.Checked)
                lblconsciousness.Text = "Stupor";
            else if (consciousness4.Checked)
                lblconsciousness.Text = "Coma";

            hdnConsciousness.Value = lblconsciousness.Text;
            if (hdnConsciousness.Value == "N/A")
                hdnConsciousness.Value = "";

            List<Objective> tempobjfallrisk = new List<Objective>();
            List<Objective> tempobjfallriskhandling = new List<Objective>();

            DataTable dtfallrisk = new DataTable();
            dtfallrisk.Columns.Add("Name", typeof(string));
            if (fall1.Checked)
            {
                Objective tempfallrisk = new Objective();
                tempfallrisk.objective_id = Guid.Empty;
                tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
                tempfallrisk.soap_mapping_name = "FALL RISK";
                tempfallrisk.value = "undergo sedation";
                tempfallrisk.remarks = "Menjalani sedasi";
                tempobjfallrisk.Add(tempfallrisk);
                dtfallrisk.Rows.Add("Patient undergo sedation");
            }
            if (fall2.Checked)
            {
                Objective tempfallrisk = new Objective();
                tempfallrisk.objective_id = Guid.Empty;
                tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
                tempfallrisk.soap_mapping_name = "FALL RISK";
                tempfallrisk.value = "physical limitation";
                tempfallrisk.remarks = "Keterbatasan fisik";
                tempobjfallrisk.Add(tempfallrisk);
                dtfallrisk.Rows.Add("Patient with physical limitation");
            }
            if (fall3.Checked)
            {
                Objective tempfallrisk = new Objective();
                tempfallrisk.objective_id = Guid.Empty;
                tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
                tempfallrisk.soap_mapping_name = "FALL RISK";
                tempfallrisk.value = "motion aids";
                tempfallrisk.remarks = "Alat bantu gerak";
                tempobjfallrisk.Add(tempfallrisk);
                dtfallrisk.Rows.Add("Patient with motion aids");
            }
            if (fall4.Checked)
            {
                Objective tempfallrisk = new Objective();
                tempfallrisk.objective_id = Guid.Empty;
                tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
                tempfallrisk.soap_mapping_name = "FALL RISK";
                tempfallrisk.value = "Disorder";
                tempfallrisk.remarks = "Gangguan keseimbangan";
                tempobjfallrisk.Add(tempfallrisk);
                dtfallrisk.Rows.Add("Patient with balance disorder");
            }
            if (fall5.Checked)
            {
                Objective tempfallrisk = new Objective();
                tempfallrisk.objective_id = Guid.Empty;
                tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
                tempfallrisk.soap_mapping_name = "FALL RISK";
                tempfallrisk.value = "fasting";
                tempfallrisk.remarks = "Puasa";
                tempobjfallrisk.Add(tempfallrisk);
                dtfallrisk.Rows.Add("Fasting patient to undergo further test(Lab/Radiology/etc)");
            }

            if (chkfalltempelstiker.Checked)
            {
                Objective tempfallriskhandling = new Objective();
                tempfallriskhandling.objective_id = Guid.Empty;
                tempfallriskhandling.soap_mapping_id = Guid.Parse("B0C9D8E0-7533-43CD-BFCF-E8C0AC1D4B7E");
                tempfallriskhandling.soap_mapping_name = "FALL RISK HANDLING";
                tempfallriskhandling.value = "Stiker";
                tempfallriskhandling.remarks = "";
                tempobjfallriskhandling.Add(tempfallriskhandling);
            }
            if (chkfalledukasi.Checked)
            {
                Objective tempfallriskhandling = new Objective();
                tempfallriskhandling.objective_id = Guid.Empty;
                tempfallriskhandling.soap_mapping_id = Guid.Parse("B0C9D8E0-7533-43CD-BFCF-E8C0AC1D4B7E");
                tempfallriskhandling.soap_mapping_name = "FALL RISK HANDLING";
                tempfallriskhandling.value = "Edukasi";
                tempfallriskhandling.remarks = "";
                tempobjfallriskhandling.Add(tempfallriskhandling);
            }
            if (chkfallPengaman.Checked)
            {
                Objective tempfallriskhandling = new Objective();
                tempfallriskhandling.objective_id = Guid.Empty;
                tempfallriskhandling.soap_mapping_id = Guid.Parse("B0C9D8E0-7533-43CD-BFCF-E8C0AC1D4B7E");
                tempfallriskhandling.soap_mapping_name = "FALL RISK HANDLING";
                tempfallriskhandling.value = "Pengaman Terpasang";
                tempfallriskhandling.remarks = "";
                tempobjfallriskhandling.Add(tempfallriskhandling);
            }
            if (chkfallTemaniKeluarga.Checked)
            {
                Objective tempfallriskhandling = new Objective();
                tempfallriskhandling.objective_id = Guid.Empty;
                tempfallriskhandling.soap_mapping_id = Guid.Parse("B0C9D8E0-7533-43CD-BFCF-E8C0AC1D4B7E");
                tempfallriskhandling.soap_mapping_name = "FALL RISK HANDLING";
                tempfallriskhandling.value = "Ditemani Keluarga";
                tempfallriskhandling.remarks = "";
                tempobjfallriskhandling.Add(tempfallriskhandling);
            }
            if (chkfallAmbulasi.Checked)
            {
                Objective tempfallriskhandling = new Objective();
                tempfallriskhandling.objective_id = Guid.Empty;
                tempfallriskhandling.soap_mapping_id = Guid.Parse("B0C9D8E0-7533-43CD-BFCF-E8C0AC1D4B7E");
                tempfallriskhandling.soap_mapping_name = "FALL RISK HANDLING";
                tempfallriskhandling.value = "Ambulasi";
                tempfallriskhandling.remarks = "";
                tempobjfallriskhandling.Add(tempfallriskhandling);
            }
            if (chkfallDokumentasiRM.Checked)
            {
                Objective tempfallriskhandling = new Objective();
                tempfallriskhandling.objective_id = Guid.Empty;
                tempfallriskhandling.soap_mapping_id = Guid.Parse("B0C9D8E0-7533-43CD-BFCF-E8C0AC1D4B7E");
                tempfallriskhandling.soap_mapping_name = "FALL RISK HANDLING";
                tempfallriskhandling.value = "Dokumentasi Rekam Medis";
                tempfallriskhandling.remarks = "";
                tempobjfallriskhandling.Add(tempfallriskhandling);
            }

            if (tempobjfallrisk.Count > 0)
            {
                hdnFallRisk.Value = new JavaScriptSerializer().Serialize(tempobjfallrisk);
                hdnFallRiskHandling.Value = new JavaScriptSerializer().Serialize(tempobjfallriskhandling);
                rptnofallrisk.DataSource = dtfallrisk;
                rptnofallrisk.DataBind();
                lblnofallrisk.Style.Add("display", "none");
                
            }
            else
            {
                hdnFallRisk.Value = "";
                hdnFallRiskHandling.Value = "";
                rptnofallrisk.DataSource = null;
                rptnofallrisk.DataBind();
                lblnofallrisk.Style.Add("display", "");
            }

            //List<string> tempvaluefallrisk = new List<string>();
            //DataTable dtfallrisk = new DataTable();
            //dtfallrisk.Columns.Add("Name", typeof(string));

            //if (fall1.Checked)
            //{
            //    fall1.Checked = true;
            //    dtfallrisk.Rows.Add("Patient undergo sedation");
            //}
            //if (fall2.Checked)
            //{
            //    fall2.Checked = true;
            //    dtfallrisk.Rows.Add("Patient with physical limitation");
            //}
            //if (fall3.Checked)
            //{
            //    fall3.Checked = true;
            //    dtfallrisk.Rows.Add("Patient with motion aids");
            //}
            //if (fall4.Checked)
            //{
            //    fall4.Checked = true;
            //    dtfallrisk.Rows.Add("Patient with balance disorder");
            //}
            //if (fall5.Checked)
            //{
            //    fall5.Checked = true;
            //    dtfallrisk.Rows.Add("Fasting patient to undergo further test(Lab/Radiology/etc)");
            //}

            //rptnofallrisk.DataSource = dtfallrisk;

            UP_FA_Physical.Update();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalEditPhysical", "$('#modalEditPhysical').modal('hide');", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnsubmitphysical_onclick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnsubmitphysical_onclick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }
    

    protected void btnsubmitIllness_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            List<PatientSurgery> data = GetRowList_PatientSurgery();
            if (data.Count > 0)
            {
                DataTable dttempsurgery = Helper.ToDataTable(data);
                rptsurgery.DataSource = dttempsurgery;
                rptsurgery.DataBind();
                lblmodalnosurgery.Style.Add("display", "none");

                hdnhistorysurgery.Value = new JavaScriptSerializer().Serialize(data);

                //Session[Helper.Sessionroutinemedication + hfguidadditional.Value] = data;

            }
            else
            {
                rptsurgery.DataSource = null;
                rptsurgery.DataBind();
                lblmodalnosurgery.Style.Add("display", "");
                hdnhistorysurgery.Value = "";
                //rbPengobatan1.Checked = true;
            }

            List<PatientProcedureHistory> dataProcedure = GetRowList_PatientProcout();
            if (dataProcedure.Count > 0)
            {
                DataTable dttempprocout = Helper.ToDataTable(dataProcedure);
                rptprocout.DataSource = dttempprocout;
                rptprocout.DataBind();
                lblmodalnoprocout.Style.Add("display", "none");
                hdnProcedureOutside.Value = new JavaScriptSerializer().Serialize(dataProcedure);
            }
            else
            {
                rptprocout.DataSource = null;
                rptprocout.DataBind();
                lblmodalnoprocout.Style.Add("display", "");
                hdnProcedureOutside.Value = "";
            }

            //if (rbOperasi.Checked)
            //{
            //    rptsurgery.DataSource = null;
            //    rptsurgery.DataBind();
            //    lblmodalnosurgery.Style.Add("display", "");
            //}
            //else if (rbOperas2.Checked)
            //{
            //    List<PatientSurgery> data = GetRowList_PatientSurgery();
            //    if (data.Count > 0)
            //    {
            //        rptsurgery.DataSource = Helper.ToDataTable(data);
            //        rptsurgery.DataBind();
            //        lblmodalnosurgery.Style.Add("display", "none");
            //    }
            //    else
            //    {
            //        rptsurgery.DataSource = null;
            //        rptsurgery.DataBind();
            //        lblmodalnosurgery.Style.Add("display", "");
            //    }

            //}

            List<PatientDiseaseHistory> templistdisease = new List<PatientDiseaseHistory>();
            if (rbpribadi2.Checked)
            {
                if (chkdisease1.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Hypertension";
                    temp_patienthistory.remarks = "Darah Tinggi";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);
                }
                if (chkdisease2.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Stroke";
                    temp_patienthistory.remarks = "Stroke";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);
                }
                if (chkdisease3.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "TBC";
                    temp_patienthistory.remarks = "TBC";
                    temp_patienthistory.status = DDL_TBC.SelectedValue;
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);

                    if (DDL_TBC.SelectedValue == "Tidak Diketahui")
                    {
                        Button_TbcStickeroff.Visible = true;
                        Button_TbcStickeron.Visible = false;
                    }
                    if (DDL_TBC.SelectedValue == "Sudah Sembuh")
                    {
                        Button_TbcStickeroff.Visible = true;
                        Button_TbcStickeron.Visible = false;
                    }
                    if (DDL_TBC.SelectedValue == "Belum Sembuh")
                    {
                        Button_TbcStickeroff.Visible = false;
                        Button_TbcStickeron.Visible = true;
                    }
                }
                else
                {
                    Button_TbcStickeroff.Visible = true;
                    Button_TbcStickeron.Visible = false;
                }

                if (chkdisease4.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Kidney";
                    temp_patienthistory.remarks = "Gagal Ginjal";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);
                }
                if (chkdisease5.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Convulsive";
                    temp_patienthistory.remarks = "Kejang";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);
                }
                if (chkdisease6.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Heart";
                    temp_patienthistory.remarks = "Gagal Jantung";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);
                }
                if (chkdisease7.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Diabetes";
                    temp_patienthistory.remarks = "Diabetes";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);
                }
                if (chkdisease8.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Asthma";
                    temp_patienthistory.remarks = "Asma";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);
                }
                if (chkdisease9.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Hepatitis";
                    temp_patienthistory.remarks = "Hepatitis";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);
                }
                if (chkdisease10.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Cancer";
                    temp_patienthistory.remarks = "Kanker";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);
                }
                if (chkdiseaseHepB.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Hepatitis B";
                    temp_patienthistory.remarks = "Hepatitis B";
                    temp_patienthistory.status = DDL_HepB.SelectedValue;
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);

                    if (DDL_HepB.SelectedValue == "Tidak Diketahui")
                    {
                        Button_HepBStickeroff.Visible = true;
                        Button_HepBStickeron.Visible = false;
                    }
                    if (DDL_HepB.SelectedValue == "Sudah Sembuh")
                    {
                        Button_HepBStickeroff.Visible = true;
                        Button_HepBStickeron.Visible = false;
                    }
                    if (DDL_HepB.SelectedValue == "Belum Sembuh")
                    {
                        Button_HepBStickeroff.Visible = false;
                        Button_HepBStickeron.Visible = true;
                    }
                }
                else
                {
                    Button_HepBStickeroff.Visible = true;
                    Button_HepBStickeron.Visible = false;
                }

                if (chkdiseaseHepC.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Hepatitis C";
                    temp_patienthistory.remarks = "Hepatitis C";
                    temp_patienthistory.status = DDL_HepC.SelectedValue;
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);

                    if (DDL_HepC.SelectedValue == "Tidak Diketahui")
                    {
                        Button_HepCStickeroff.Visible = true;
                        Button_HepCStickeron.Visible = false;
                    }
                    if (DDL_HepC.SelectedValue == "Sudah Sembuh")
                    {
                        Button_HepCStickeroff.Visible = true;
                        Button_HepCStickeron.Visible = false;
                    }
                    if (DDL_HepC.SelectedValue == "Belum Sembuh")
                    {
                        Button_HepCStickeroff.Visible = false;
                        Button_HepCStickeron.Visible = true;
                    }
                }
                else
                {
                    Button_HepCStickeroff.Visible = true;
                    Button_HepCStickeron.Visible = false;
                }

                if (CheckBoxHAD.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "HAD";
                    temp_patienthistory.remarks = "HAD";
                    temp_patienthistory.status = "Belum Sembuh";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);

                    Button_HadStickeroff.Visible = false;
                    Button_HadStickeron.Visible = true;
                }
                if (CheckBoxPRT.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "PRT";
                    temp_patienthistory.remarks = "PRT";
                    temp_patienthistory.status = "Belum Sembuh";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);

                    Button_PrtStickeroff.Visible = false;
                    Button_PrtStickeron.Visible = true;
                }
                if (CheckBoxRHN.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "RHN";
                    temp_patienthistory.remarks = "RHN";
                    temp_patienthistory.status = "Belum Sembuh";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);

                    Button_RhnStickeroff.Visible = false;
                    Button_RhnStickeron.Visible = true;
                }
                if (CheckBoxMRS.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "MRS";
                    temp_patienthistory.remarks = "MRS";
                    temp_patienthistory.status = "Belum Sembuh";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);

                    Button_MrsStickeroff.Visible = false;
                    Button_MrsStickeron.Visible = true;
                }
                if (CheckBoxCOVID.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "COVID";
                    temp_patienthistory.remarks = "COVID";
                    temp_patienthistory.status = "Belum Sembuh";
                    temp_patienthistory.disease_history_type = 1;
                    temp_patienthistory.is_delete = 0;
                    templistdisease.Add(temp_patienthistory);

                    Button_CvStickeroff.Visible = false;
                    Button_CvStickeron.Visible = true;
                }

                if (txtDisease.Text != "")
                {
                    PatientDiseaseHistory temp_patienthistoryremark = new PatientDiseaseHistory();
                    temp_patienthistoryremark.patient_disease_history_id = Guid.Empty;
                    temp_patienthistoryremark.value = "Lain-lain";
                    temp_patienthistoryremark.remarks = txtDisease.Text;
                    temp_patienthistoryremark.status = "";
                    temp_patienthistoryremark.disease_history_type = 1;
                    temp_patienthistoryremark.is_delete = 0;
                    templistdisease.Add(temp_patienthistoryremark);
                }

                hdnDiseaseHistory.Value = new JavaScriptSerializer().Serialize(templistdisease);

                List<PatientDiseaseHistory> disease = templistdisease.Where(x => x.disease_history_type == 1).ToList();
                foreach (var x in disease.Where(x => x.value == "Lain-lain"))
                {
                    x.value = x.remarks;
                }

                List<PatientDiseaseHistory> temprpt_disease = templistdisease.FindAll(x => x.value != "HAD" && x.value != "PRT" && x.value != "RHN" && x.value != "MRS" && x.value != "COVID");

                if (temprpt_disease.Count > 0)
                {
                    rptdisease.DataSource = Helper.ToDataTable(temprpt_disease);
                    rptdisease.DataBind();
                    lblmodalnodisease.Style.Add("display", "none");
                }
                else
                {
                    rptdisease.DataSource = null;
                    rptdisease.DataBind();
                    lblmodalnodisease.Style.Add("display", "");
                }

            }
            else if (rbpribadi1.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Tidak Ada Riwayat";
                temp_patienthistory.remarks = "Tidak Ada Riwayat";
                temp_patienthistory.status = "";
                temp_patienthistory.disease_history_type = 1;
                temp_patienthistory.is_delete = 0;
                templistdisease.Add(temp_patienthistory);

                hdnDiseaseHistory.Value = new JavaScriptSerializer().Serialize(templistdisease);
                rptdisease.DataSource = Helper.ToDataTable(templistdisease);
                rptdisease.DataBind();
                lblmodalnodisease.Style.Add("display", "none");
            }
            else
            {
                hdnDiseaseHistory.Value = "";
                templistdisease.Clear();
                rptdisease.DataSource = null;
                rptdisease.DataBind();
                lblmodalnodisease.Style.Add("display", "");
            }

            List<PatientDiseaseHistory> templistfamdisease = new List<PatientDiseaseHistory>();
            if (rbkeluarga2.Checked)
            {
                if (chkdiseasefam1.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Heart";
                    temp_patienthistory.remarks = "Gagal Jantung";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 2;
                    temp_patienthistory.is_delete = 0;
                    templistfamdisease.Add(temp_patienthistory);
                }
                if (chkdiseasefam2.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Diabetes";
                    temp_patienthistory.remarks = "Diabetes";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 2;
                    temp_patienthistory.is_delete = 0;
                    templistfamdisease.Add(temp_patienthistory);
                }
                if (chkdiseasefam3.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Asthma";
                    temp_patienthistory.remarks = "Asma";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 2;
                    temp_patienthistory.is_delete = 0;
                    templistfamdisease.Add(temp_patienthistory);
                }
                if (chkdiseasefam4.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Hypertension";
                    temp_patienthistory.remarks = "Darah Tinggi";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 2;
                    temp_patienthistory.is_delete = 0;
                    templistfamdisease.Add(temp_patienthistory);
                }
                if (chkdiseasefam5.Checked)
                {
                    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                    temp_patienthistory.patient_disease_history_id = Guid.Empty;
                    temp_patienthistory.value = "Cancer";
                    temp_patienthistory.remarks = "Kanker";
                    temp_patienthistory.status = "";
                    temp_patienthistory.disease_history_type = 2;
                    temp_patienthistory.is_delete = 0;
                    templistfamdisease.Add(temp_patienthistory);
                }

                if (txtDiseaseFam.Text != "")
                {
                    PatientDiseaseHistory temp_patienthistoryremark = new PatientDiseaseHistory();
                    temp_patienthistoryremark.patient_disease_history_id = Guid.Empty;
                    temp_patienthistoryremark.value = "Lain-lain";
                    temp_patienthistoryremark.remarks = txtDiseaseFam.Text;
                    temp_patienthistoryremark.status = "";
                    temp_patienthistoryremark.disease_history_type = 2;
                    temp_patienthistoryremark.is_delete = 0;
                    templistfamdisease.Add(temp_patienthistoryremark);
                }

                hdnFamilyDiseaseHistory.Value = new JavaScriptSerializer().Serialize(templistfamdisease);

                List<PatientDiseaseHistory> famdisease = templistfamdisease.Where(x => x.disease_history_type == 2).ToList();
                foreach (var x in famdisease.Where(x => x.value == "Lain-lain"))
                {
                    x.value = x.remarks;
                }

                rptfamdisease.DataSource = Helper.ToDataTable(templistfamdisease);
                rptfamdisease.DataBind();
                lblmodalnofamdisease.Style.Add("display", "none");
            }
            else if (rbkeluarga1.Checked)
            {
                PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
                temp_patienthistory.patient_disease_history_id = Guid.Empty;
                temp_patienthistory.value = "Tidak Ada Riwayat";
                temp_patienthistory.remarks = "Tidak Ada Riwayat";
                temp_patienthistory.status = "";
                temp_patienthistory.disease_history_type = 2;
                temp_patienthistory.is_delete = 0;
                templistfamdisease.Add(temp_patienthistory);

                hdnFamilyDiseaseHistory.Value = new JavaScriptSerializer().Serialize(templistfamdisease);
                rptfamdisease.DataSource = Helper.ToDataTable(templistfamdisease);
                rptfamdisease.DataBind();
                lblmodalnofamdisease.Style.Add("display", "none");
            }
            else
            {
                hdnFamilyDiseaseHistory.Value = "";
                templistfamdisease.Clear();
                rptfamdisease.DataSource = null;
                rptfamdisease.DataBind();
                lblmodalnofamdisease.Style.Add("display", "");
            }

            UP_FA_HealthRecord.Update();
            up_sticker.Update();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalEditIllness", "HidePreviewIllness();", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnsubmitIllness_click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnsubmitIllness_click", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnEditPhysical_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            if (hdnEye.Value != "")
            {
                eye4.Checked = false;
                eye3.Checked = false;
                eye2.Checked = false;
                eye1.Checked = false;
                if (hdnEye.Value == "1")
                {
                    eye4.Checked = true;
                    lbleye.Text = "1. None";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnEye.Value == "2")
                {
                    eye3.Checked = true;
                    lbleye.Text = "2. To Pressure";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnEye.Value == "3")
                {
                    eye2.Checked = true;
                    lbleye.Text = "3. To Sound";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnEye.Value == "4")
                {
                    eye1.Checked = true;
                    lbleye.Text = "4. Spontaneus";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
            }
            else
            {
                eye4.Checked = false;
                eye3.Checked = false;
                eye2.Checked = false;
                eye1.Checked = false;
            }
            if (hdnMove.Value != "")
            {
                move6.Checked = false;
                move5.Checked = false;
                move4.Checked = false;
                move3.Checked = false;
                move2.Checked = false;
                move1.Checked = false;
                if (hdnMove.Value == "1")
                {
                    move6.Checked = true;
                    lblmove.Text = "1. None";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnMove.Value == "2")
                {
                    move5.Checked = true;
                    lblmove.Text = "2. Extension";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnMove.Value == "3")
                {
                    move4.Checked = true;
                    lblmove.Text = "3. Flexion to pain stumulus";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnMove.Value == "4")
                {
                    move3.Checked = true;
                    lblmove.Text = "4. Withdrawns from pain";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnMove.Value == "5")
                {
                    move2.Checked = true;
                    lblmove.Text = "5. Localizes to pain stimulus";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnMove.Value == "6")
                {
                    move1.Checked = true;
                    lblmove.Text = "6. Obey Commands";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
            }
            else
            {
                move6.Checked = false;
                move5.Checked = false;
                move4.Checked = false;
                move3.Checked = false;
                move2.Checked = false;
                move1.Checked = false;
            }
            if (hdnVerbal.Value != "")
            {
                verbal5.Checked = false;
                verbal4.Checked = false;
                verbal3.Checked = false;
                verbal2.Checked = false;
                verbal1.Checked = false;

                if (hdnVerbal.Value == "1")
                {
                    verbal5.Checked = true;
                    lblverbal.Text = "1. None";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnVerbal.Value == "2")
                {
                    verbal4.Checked = true;
                    lblverbal.Text = "2. Incomprehensible sounds";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnVerbal.Value == "3")
                {
                    verbal3.Checked = true;
                    lblverbal.Text = "3. Inappropriate words";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnVerbal.Value == "4")
                {
                    verbal2.Checked = true;
                    lblverbal.Text = "4. Confused";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnVerbal.Value == "5")
                {
                    verbal1.Checked = true;
                    lblverbal.Text = "5. Orientated";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnVerbal.Value == "T")
                {
                    verbal6.Checked = true;
                    lblverbal.Text = "T. Tracheostomy";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
                else if (hdnVerbal.Value == "A")
                {
                    verbal7.Checked = true;
                    lblverbal.Text = "A. Aphasia";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                }
            }
            else
            {
                verbal5.Checked = false;
                verbal4.Checked = false;
                verbal3.Checked = false;
                verbal2.Checked = false;
                verbal1.Checked = false;
            }

            if (hdnpainscale.Value != "")
            {
                txtPainScale.Text = hdnpainscale.Value;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "getProgressBar", "getProgressBar(" + txtPainScale.Text + ");", true);
            }
            else
            {
                txtPainScale.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "getProgressBar", "getProgressBar(" + txtPainScale.Text + ");", true);
            }

            if (hdnMentalStatus.Value != "")
            {
                mental1.Checked = false;
                mental2.Checked = false;
                mental3.Checked = false;
                mental4.Checked = false;

                if (hdnMentalStatus.Value.ToUpper() == "Orientasi baik".ToUpper()|| hdnMentalStatus.Value.ToUpper() == "Good Orientation".ToUpper())
                    mental1.Checked = true;
                else if (hdnMentalStatus.Value.ToUpper() == "Disorientasi".ToUpper() || hdnMentalStatus.Value.ToUpper() == "Disorientated".ToUpper())
                    mental2.Checked = true;
                else if (hdnMentalStatus.Value.ToUpper() == "Kooperatif".ToUpper() || hdnMentalStatus.Value.ToUpper() == "Cooperative".ToUpper())
                    mental3.Checked = true;
                else if (hdnMentalStatus.Value.ToUpper() == "Tidak Kooperatif".ToUpper() || hdnMentalStatus.Value.ToUpper() == "Non Cooperative".ToUpper())
                    mental4.Checked = true;
            }
            else
            {
                mental1.Checked = false;
                mental2.Checked = false;
                mental3.Checked = false;
                mental4.Checked = false;
            }

            if (hdnConsciousness.Value != "")
            {
                consciousness1.Checked = false;
                consciousness2.Checked = false;
                consciousness3.Checked = false;
                consciousness4.Checked = false;

                if (hdnConsciousness.Value.ToUpper() == "Compos mentis".ToUpper())
                    consciousness1.Checked = true;
                else if (hdnConsciousness.Value.ToUpper() == "Somnolent".ToUpper())
                    consciousness2.Checked = true;
                else if (hdnConsciousness.Value.ToUpper() == "Stupor".ToUpper())
                    consciousness3.Checked = true;
                else if (hdnConsciousness.Value.ToUpper() == "Coma".ToUpper())
                    consciousness4.Checked = true;
            }
            else
            {
                consciousness1.Checked = false;
                consciousness2.Checked = false;
                consciousness3.Checked = false;
                consciousness4.Checked = false;
            }


            if (hdnFallRisk.Value != "")
            {
                List<Objective> tempobjfallrisk = new JavaScriptSerializer().Deserialize<List<Objective>>(hdnFallRisk.Value);
                if (tempobjfallrisk.Count > 0)
                {
                    fall1.Checked = false;
                    fall2.Checked = false;
                    fall3.Checked = false;
                    fall4.Checked = false;
                    fall5.Checked = false;
                    foreach (var x in tempobjfallrisk)
                    {
                        if (x.value.ToUpper() == "undergo sedation".ToUpper())
                        {
                            fall1.Checked = true;
                        }
                        else if (x.value.ToUpper() == "physical limitation".ToUpper())
                        {
                            fall2.Checked = true;
                        }
                        else if (x.value.ToUpper() == "motion aids".ToUpper())
                        {
                            fall3.Checked = true;
                        }
                        else if (x.value.ToUpper() == "Disorder".ToUpper())
                        {
                            fall4.Checked = true;
                        }
                        else if (x.value.ToUpper() == "fasting".ToUpper())
                        {
                            fall5.Checked = true;
                        }
                    }
                }
            }
            else
            {
                fall1.Checked = false;
                fall2.Checked = false;
                fall3.Checked = false;
                fall4.Checked = false;
                fall5.Checked = false;
            }

            if (hdnFallRiskHandling.Value != "")
            {
                List<Objective> tempobjfallriskHandling = new JavaScriptSerializer().Deserialize<List<Objective>>(hdnFallRiskHandling.Value);
                if (tempobjfallriskHandling.Count > 0)
                {
                    chkfalltempelstiker.Checked = false;
                    chkfalledukasi.Checked = false;
                    chkfallPengaman.Checked = false;
                    chkfallTemaniKeluarga.Checked = false;
                    chkfallAmbulasi.Checked = false;
                    chkfallDokumentasiRM.Checked = false;
                    foreach (var x in tempobjfallriskHandling)
                    {
                        if (x.value.ToUpper() == "Stiker".ToUpper())
                            chkfalltempelstiker.Checked = true;
                        else if (x.value.ToUpper() == "Edukasi".ToUpper())
                            chkfalledukasi.Checked = true;
                        else if (x.value.ToUpper() == "Pengaman Terpasang".ToUpper())
                            chkfallPengaman.Checked = true;
                        else if (x.value.ToUpper() == "Ditemani Keluarga".ToUpper())
                            chkfallTemaniKeluarga.Checked = true;
                        else if (x.value.ToUpper() == "Ambulasi".ToUpper())
                            chkfallAmbulasi.Checked = true;
                        else if (x.value.ToUpper() == "Dokumentasi Rekam Medis".ToUpper())
                            chkfallDokumentasiRM.Checked = true;
                    }
                }
            }
            else
            {
                chkfalltempelstiker.Checked = false;
                chkfalledukasi.Checked = false;
                chkfallPengaman.Checked = false;
                chkfallTemaniKeluarga.Checked = false;
                chkfallAmbulasi.Checked = false;
                chkfallDokumentasiRM.Checked = false;
            }

            upObjective.Update();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PreviewPhysical", "$('#modalEditPhysical').modal({ backdrop: 'static', keyboard: false });", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnEditPhysical_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnEditPhysical_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnEditNutrition_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            if (hdnNutrition.Value != "")
            {
                txtNutrition.Text = hdnNutrition.Value;
                rbnutrisi2.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript5", "ShowHideDiv5();", true);
            }
            else
            {
                rbnutrisi1.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript5", "HideDiv5();", true);
            }
            if (hdnFasting.Value != "")
            {
                txtFasting.Text = hdnFasting.Value;
                rbpuasa2.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript6", "ShowHideDiv6();", true);
            }
            else
            {
                rbpuasa1.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript6", "HideDiv6();", true);
            }

            UpdatePanelModalNutrition.Update();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PreviewNutrition", "$('#modalEditNutrition').modal({ backdrop: 'static', keyboard: false });", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnEditNutrition_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnEditNutrition_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnEditEndemic_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            SOAP soapmodel = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
            if (hdnEndemicArea.Value != "")
            {
                txtEndemic.Text = hdnEndemicArea.Value;
                rbkunjungan2.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript4", "ShowHideDiv4();", true);
            }
            else
            {
                rbkunjungan1.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript4", "HideDiv4();", true);
            }

            if (hdnTindakan.Value != "")
            {
                List<Subjective> tempTindakan = new JavaScriptSerializer().Deserialize<List<Subjective>>(hdnTindakan.Value);

                if (tempTindakan.Count() > 0)
                {
                    foreach (Subjective x in tempTindakan)
                    {
                        if (x.soap_mapping_id == Guid.Parse("f815de0e-e57a-4250-a2be-bdd27c1876ac"))
                        {//tindakan kewaspadaan
                            if (x.value == "segregasi")
                            {
                                chkTindakan1.Checked = true;
                            }
                            if (x.value == "masker")
                            {
                                chkTindakan2.Checked = true;
                            }
                            if (x.value == "kewaspadaan")
                            {
                                chkTindakan3.Checked = true;
                            }
                        }
                    }
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "DeleteList", "diseaseSelected = []; alertSelected = [];", true);

            if (soapmodel.infectious_disease.Where(x => (x.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_delete == false) || (x.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_new == true)).Count() > 0)
            {

                foreach (var id in soapmodel.infectious_disease)
                {
                    string showAlertSuggest = "";
                    string key = "";
                    if (id.infectious_symptoms_id == 1 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen1.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen1'), 1); console.log('masuk 1')";
                        key = "ShowAlert1";
                    }
                    else if (id.infectious_symptoms_id == 2 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen2.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen2'), 1);";
                        key = "ShowAlert2";

                    }
                    else if (id.infectious_symptoms_id == 3 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen3.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen3'), 1);";
                        key = "ShowAlert3";
                    }
                    else if (id.infectious_symptoms_id == 4 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen4.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen4'), 1);";
                        key = "ShowAlert4";
                    }
                    else if (id.infectious_symptoms_id == 5 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen5.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen5'), 2);";
                        key = "ShowAlert5";
                    }
                    else if (id.infectious_symptoms_id == 6 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen6.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen6'), 2);";
                        key = "ShowAlert6";
                    }
                    else if (id.infectious_symptoms_id == 7 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen7.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen7'), 2);";
                        key = "ShowAlert7";
                    }
                    else if (id.infectious_symptoms_id == 8 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen8.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen8'), 3);";
                        key = "ShowAlert8";
                    }
                    else if (id.infectious_symptoms_id == 9 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen9.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen9'), 3);";
                        key = "ShowAlert9";
                    }
                    else if (id.infectious_symptoms_id == 10 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen10.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen10'), 3);";
                        key = "ShowAlert10";
                    }
                    else if (id.infectious_symptoms_id == 11 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen11.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen11'), 4);";
                        key = "ShowAlert11";
                    }
                    else if (id.infectious_symptoms_id == 12 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen12.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen12'), 4);";
                        key = "ShowAlert12";
                    }
                    else if (id.infectious_symptoms_id == 13 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen13.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen13'), 4);";
                        key = "ShowAlert13";
                    }
                    else if (id.infectious_symptoms_id == 14 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen14.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen14'), 5);";
                        key = "ShowAlert14";
                    }
                    else if (id.infectious_symptoms_id == 15 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen15.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen15'), 6);";
                        key = "ShowAlert15";
                    }
                    else if (id.infectious_symptoms_id == 16 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen16.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen16'), 7);";
                        key = "ShowAlert16";
                    }
                    else if (id.infectious_symptoms_id == 17 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen17.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen17'), 8);";
                        key = "ShowAlert17";
                    }
                    else if (id.infectious_symptoms_id == 21 && ((id.patient_symptoms_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_delete == false) || (id.patient_symptoms_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && id.is_new == true)))
                    {
                        chkScreen21.Checked = true;
                        showAlertSuggest = "showAlertSuggest(document.getElementById('MainContent_chkScreen21'), 11);";
                        key = "ShowAlert21";
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), key, showAlertSuggest, true);

                }
            }

            if (soapmodel.infectious_alert.Where(x => x.patient_alert_id != Guid.Parse("00000000-0000-0000-0000-000000000000")).Count() > 0)
            {
                foreach (var ia in soapmodel.infectious_alert)
                {
                    if (ia.patient_alert_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && ia.alert_type_id == 1 && ia.is_delete == false)
                    {
                        chkKewaspadaanStandard.Checked = true;
                        HF_isCheckS.Value = "true";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "chkKewaspadaanS", "CheckKewaspadaanS()", true);
                    }
                    if (ia.patient_alert_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && ia.alert_type_id == 2 && ia.is_delete == false)
                    {
                        chkKewaspadaanKontak.Checked = true;
                        HF_isCheckK.Value = "true";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "chkKewaspadaanK", "CheckKewaspadaanK()", true);
                    }
                    if (ia.patient_alert_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && ia.alert_type_id == 3 && ia.is_delete == false)
                    {
                        chkKewaspadaanDroplet.Checked = true;
                        HF_isCheckD.Value = "true";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "chkKewaspadaanD", "CheckKewaspadaanD()", true);
                    }
                    if (ia.patient_alert_id != Guid.Parse("00000000-0000-0000-0000-000000000000") && ia.alert_type_id == 4 && ia.is_delete == false)
                    {
                        chkKewaspadaanAirborne.Checked = true;
                        HF_isCheckA.Value = "true";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "chkKewaspadaanA", "CheckKewaspadaanA()", true);
                    }
                }


            }

			if (chkScreen1.Checked || chkScreen2.Checked || chkScreen3.Checked || chkScreen4.Checked || chkScreen5.Checked || chkScreen6.Checked || chkScreen7.Checked
	            || chkScreen8.Checked || chkScreen9.Checked || chkScreen10.Checked || chkScreen11.Checked || chkScreen12.Checked || chkScreen13.Checked || chkScreen14.Checked
	            || chkScreen15.Checked || chkScreen16.Checked || chkScreen17.Checked || chkScreen21.Checked || chkKewaspadaanStandard.Checked || chkKewaspadaanKontak.Checked
	            || chkKewaspadaanDroplet.Checked || chkKewaspadaanAirborne.Checked || chkTindakan1.Checked || chkTindakan2.Checked || chkTindakan3.Checked)
			{
				rbskriningyes.Checked = true;
				ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showSkrining", "ShowSkrining();", true);
			}
			else
			{
				rbskriningno.Checked = true;
				ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideSkrining", "HideSkrining();", true);

			}

			UpdatePanelModalEndemic.Update();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PreviewEndemic", "$('#modalEditEndemic').modal({ backdrop: 'static', keyboard: false });", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InfoKewaspadaan", "ShowModalEndemic();", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnEditEndemic_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnEditEndemic_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnSvAlertDltReason_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            if (HF_alertType.Value.ToString() == "standard")
            {
                HF_isCheckS.Value = "false";
                HF_DeleteReasonS.Value = txtAlasanKewaspadaan.Text;
                chkKewaspadaanStandard.Checked = false;
            }
            if (HF_alertType.Value.ToString() == "kontak")
            {
                HF_isCheckK.Value = "false";
                HF_DeleteReasonK.Value = txtAlasanKewaspadaan.Text;
                chkKewaspadaanKontak.Checked = false;
            }
            if (HF_alertType.Value.ToString() == "droplet")
            {
                HF_isCheckD.Value = "false";
                HF_DeleteReasonD.Value = txtAlasanKewaspadaan.Text;
                chkKewaspadaanDroplet.Checked = false;
            }
            if (HF_alertType.Value.ToString() == "airborne")
            {
                HF_isCheckA.Value = "false";
                HF_DeleteReasonA.Value = txtAlasanKewaspadaan.Text;
                chkKewaspadaanAirborne.Checked = false;
            }
            rbskriningyes.Checked = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showSkriningAfterSaveReason", "ShowSkrining();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowTableInfo", "ShowTableInfo();", true);
            txtAlasanKewaspadaan.Text = "";
            UpdatePanelModalEndemic.Update();
            UP_FA_Endemic.Update();
            UP_Kewaspadaan.Update();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closeModalAlasanKewaspadaan", "saveAlertDeleteReason();", true);



            //Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID(), "OrganizationId", MyUser.GetHopeOrgID(), "btnSvAlertDltReason_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            //Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID(), "OrganizationId", MyUser.GetHopeOrgID(), "btnSvAlertDltReason_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            throw ex;
        }

    }
    protected void btnEditIllness_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            if (hdnhistorysurgery.Value != "")
            {
                List<PatientSurgery> tempsurgery = new JavaScriptSerializer().Deserialize<List<PatientSurgery>>(hdnhistorysurgery.Value);
                if (tempsurgery.Count() > 0)
                {
                    DataTable dt = Helper.ToDataTable(tempsurgery);
                    if (dt.Rows[0]["surgery_type"].ToString() == "Tidak Ada Operasi")
                    {
                        gvw_surgery.DataSource = null;
                        gvw_surgery.DataBind();
                        rbOperasi.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDiv7", "HideDiv7();", true);
                    }
                    else
                    {
                        gvw_surgery.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
                        gvw_surgery.DataBind();
                        rbOperas2.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "ShowHideDiv7();", true);
                    }
                }
                else
                {
                    gvw_surgery.DataSource = null;
                    gvw_surgery.DataBind();
                    //rbOperasi.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDiv7", "HideDiv7();", true);
                }
            }
            else
            {
                gvw_surgery.DataSource = null;
                gvw_surgery.DataBind();
                //rbOperasi.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDiv7", "HideDiv7();", true);
            }

            if (hdnProcedureOutside.Value != "")
            {
                List<PatientProcedureHistory> tempprocout = new JavaScriptSerializer().Deserialize<List<PatientProcedureHistory>>(hdnProcedureOutside.Value);
                if (tempprocout.Count() > 0)
                {
                    DataTable dt = Helper.ToDataTable(tempprocout);
                    gvw_procout.DataSource = dt;
                    gvw_procout.DataBind();
                    rbProcOut2.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showScript", "ShowHideDivProcout();", true);
                }
                else
                {
                    gvw_procout.DataSource = null;
                    gvw_procout.DataBind();
                    rbProcOut1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideScript", "HideDivProcout();", true);
                }
            }
            else
            {
                gvw_procout.DataSource = null;
                gvw_procout.DataBind();
                rbProcOut1.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideScript", "HideDivProcout();", true);
            }


            if (hdnDiseaseHistory.Value != "")
            {
                int tempflagdisease = 0;
                int no_tempflagdisease = 0;
                List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);
                chkdisease1.Checked = false;
                chkdisease2.Checked = false;
                chkdisease3.Checked = false;
                //DDL_TBC.Enabled = false;
                chkdisease4.Checked = false;
                chkdisease5.Checked = false;
                chkdisease6.Checked = false;
                chkdisease7.Checked = false;
                chkdisease8.Checked = false;
                chkdisease9.Checked = false;
                chkdisease10.Checked = false;
                chkdiseaseHepB.Checked = false;
                chkdiseaseHepC.Checked = false;
                //DDL_HepB.Enabled = false;
                //DDL_HepC.Enabled = false;
                txtDisease.Text = "";
                CheckBoxHAD.Checked = false;
                CheckBoxPRT.Checked = false;
                CheckBoxRHN.Checked = false;
                CheckBoxMRS.Checked = false;
                CheckBoxCOVID.Checked = false;
                foreach (PatientDiseaseHistory x in tempdisease)
                {
                    if (x.disease_history_type == 1)
                    {//patient disease
                        tempflagdisease = 1;
                        if (x.value == "Hypertension")
                            chkdisease1.Checked = true;
                        else if (x.value == "Stroke")
                            chkdisease2.Checked = true;
                        else if (x.value == "TBC")
                        {
                            chkdisease3.Checked = true;
                            if (x.status == "Tidak Diketahui")
                            {
                                DDL_TBC.SelectedValue = "Tidak Diketahui";
                            }
                            if (x.status == "Sudah Sembuh")
                            {
                                DDL_TBC.SelectedValue = "Sudah Sembuh";
                            }
                            if (x.status == "Belum Sembuh")
                            {
                                DDL_TBC.SelectedValue = "Belum Sembuh";
                            }
                        }
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
                        else if (x.value == "Hepatitis B")
                        {
                            chkdiseaseHepB.Checked = true;
                            if (x.status == "Tidak Diketahui")
                            {
                                DDL_HepB.SelectedValue = "Tidak Diketahui";
                            }
                            if (x.status == "Sudah Sembuh")
                            {
                                DDL_HepB.SelectedValue = "Sudah Sembuh";
                            }
                            if (x.status == "Belum Sembuh")
                            {
                                DDL_HepB.SelectedValue = "Belum Sembuh";
                            }
                        }
                        else if (x.value == "Hepatitis C")
                        {
                            chkdiseaseHepC.Checked = true;
                            if (x.status == "Tidak Diketahui")
                            {
                                DDL_HepC.SelectedValue = "Tidak Diketahui";
                            }
                            if (x.status == "Sudah Sembuh")
                            {
                                DDL_HepC.SelectedValue = "Sudah Sembuh";
                            }
                            if (x.status == "Belum Sembuh")
                            {
                                DDL_HepC.SelectedValue = "Belum Sembuh";
                            }
                        }
                        else if (x.value == "HAD")
                        {
                            CheckBoxHAD.Checked = true;
                        }
                        else if (x.value == "PRT")
                        {
                            CheckBoxPRT.Checked = true;
                        }
                        else if (x.value == "RHN")
                        {
                            CheckBoxRHN.Checked = true;
                        }
                        else if (x.value == "MRS")
                        {
                            CheckBoxMRS.Checked = true;
                        }
                        else if (x.value == "COVID")
                        {
                            CheckBoxCOVID.Checked = true;
                        }
                        else if (x.value == "Lain-lain")
                        {
                            txtDisease.Text = x.remarks;
                        }
                        else if (x.value == "Tidak Ada Riwayat")
                        {
                            no_tempflagdisease = 1;
                        }
                    }
                }
                
                if (tempflagdisease == 1)
                {
                    if (no_tempflagdisease == 1)
                    {
                        rbpribadi1.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript2", "HideDiv2();", true);
                    }
                    else
                    {
                        rbpribadi2.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript2", "ShowHideDiv2();", true);
                    }
                }
                else
                {
                    //rbpribadi1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript2", "HideDiv2();", true);
                }
            }
            else
            {
                //rbpribadi1.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript2", "HideDiv2();", true);
            }

            setCheckBoxToggle();

            if (hdnFamilyDiseaseHistory.Value != "")
            {
                int tempflagdiseasefam = 0;
                int no_tempflagdiseasefam = 0;
                List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnFamilyDiseaseHistory.Value);
                chkdiseasefam1.Checked = false;
                chkdiseasefam2.Checked = false;
                chkdiseasefam3.Checked = false;
                chkdiseasefam4.Checked = false;
                chkdiseasefam5.Checked = false;
                txtDiseaseFam.Text = "";

                foreach (PatientDiseaseHistory x in tempdisease)
                {
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
                        else if (x.value == "Tidak Ada Riwayat")
                        {
                            no_tempflagdiseasefam = 1;
                        }
                    }
                }

                if (tempflagdiseasefam == 1)
                {
                    if (no_tempflagdiseasefam == 1)
                    {
                        rbkeluarga1.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript3", "HideDiv3();", true);
                    }
                    else
                    {
                        rbkeluarga2.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript3", "ShowHideDiv3();", true);
                    }
                }
                else
                {
                    //rbkeluarga1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript3", "HideDiv3();", true);
                }

            }
            else
            {
                //rbkeluarga1.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript3", "HideDiv3();", true);
            }

            UpdatePanelModalIllness.Update();
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PreviewIllnesss", "PreviewIllnesss();", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnEditIllness_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnEditIllness_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    public void setCheckBoxToggle()
    {
        if (chkdisease3.Checked == true)
        {
            DDL_TBC.Style.Add("display", "inline-block");
        }
        else
        {
            DDL_TBC.Style.Add("display", "none");
        }

        if (chkdiseaseHepB.Checked == true)
        {
            DDL_HepB.Style.Add("display", "inline-block");
        }
        else
        {
            DDL_HepB.Style.Add("display", "none");
        }

        if (chkdiseaseHepC.Checked == true)
        {
            DDL_HepC.Style.Add("display", "inline-block");
        }
        else
        {
            DDL_HepC.Style.Add("display", "none");
        }

    }

    protected void btnEditRoutine_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            if (hdnhistoryroutine.Value != "")
            {
                List<PatientRoutineMedication> tempcurrmed = new JavaScriptSerializer().Deserialize<List<PatientRoutineMedication>>(hdnhistoryroutine.Value);
                if (tempcurrmed.Count() > 0)
                {
                    DataTable dt = Helper.ToDataTable(tempcurrmed);
                    if (dt.Rows[0]["medication"].ToString() == "Tidak Ada Pengobatan")
                    {
                        gvw_routinemed.DataSource = null;
                        gvw_routinemed.DataBind();
                        rbPengobatan1.Checked = true;
                        hfenableroutine.Value = "0";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript7", "HideDiv();", true);
                    }
                    else
                    {
                        gvw_routinemed.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
                        gvw_routinemed.DataBind();
                        rbPengobatan2.Checked = true;
                        hfenableroutine.Value = "1";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "ShowHideDiv();", true);
                    }
                }
                else
                {
                    gvw_routinemed.DataSource = null;
                    gvw_routinemed.DataBind();
                    //rbPengobatan1.Checked = true;
                    hfenableroutine.Value = "-1";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript7", "HideDiv();", true);
                }
            }
            else
            {
                gvw_routinemed.DataSource = null;
                gvw_routinemed.DataBind();
                //rbPengobatan1.Checked = true;
                hfenableroutine.Value = "-1";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript7", "HideDiv();", true);
            }

            if (hdnhistorydrugallergies.Value != "")
            {
                List<PatientAllergy> tempdrugsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistorydrugallergies.Value);
                DataTable dtAllergy = Helper.ToDataTable(tempdrugsallergy);
                if (dtAllergy.Select("is_delete = 0 and allergy_type = 1").Count() > 0)
                {
                    DataTable dtdrug = dtAllergy.Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
                    if (dtdrug.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                    {
                        gvw_allergy.DataSource = null;
                        gvw_allergy.DataBind();
                        rbdrug1.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript8", "HideDiv8();", true);

                        //rptdrugallergies.DataSource = null;
                        //rptdrugallergies.DataBind();
                        //lblmodalnodrug.Visible = true;
                    }
                    else
                    {
                        gvw_allergy.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
                        gvw_allergy.DataBind();
                        rbdrug2.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript8", "ShowHideDiv8();", true);

                        //rptdrugallergies.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 1").CopyToDataTable();
                        //rptdrugallergies.DataBind();
                        //lblmodalnodrug.Visible = false;
                    }
                }
                else
                {
                    gvw_allergy.DataSource = null;
                    gvw_allergy.DataBind();
                    //rbdrug1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript8", "HideDiv8();", true);

                    //rptdrugallergies.DataSource = null;
                    //rptdrugallergies.DataBind();
                    //lblmodalnodrug.Visible = true;
                }
            }
            else
            {
                gvw_allergy.DataSource = null;
                gvw_allergy.DataBind();
                //rbdrug1.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript8", "HideDiv8();", true);
            }

            if (hdnhistoryfoodallergies.Value != "")
            {
                List<PatientAllergy> tempfoodsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryfoodallergies.Value);
                DataTable dtAllergy = Helper.ToDataTable(tempfoodsallergy);
                if (dtAllergy.Select("is_delete = 0 and allergy_type = 2").Count() > 0)
                {
                    DataTable dtfood = dtAllergy.Select("is_delete = 0 and allergy_type = 2").CopyToDataTable();
                    if (dtfood.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                    {
                        gvw_foods.DataSource = null;
                        gvw_foods.DataBind();
                        rbfood1.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript9", "HideDiv9();", true);
                    }
                    else
                    {
                        gvw_foods.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 2").CopyToDataTable();
                        gvw_foods.DataBind();
                        rbfood2.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript9", "ShowHideDiv9();", true);
                    }
                }
                else
                {
                    gvw_foods.DataSource = null;
                    gvw_foods.DataBind();
                    //rbfood1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript9", "HideDiv9();", true);
                }
            }
            else
            {
                gvw_foods.DataSource = null;
                gvw_foods.DataBind();
                //rbfood1.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript9", "HideDiv9();", true);

            }

            if (hdnhistoryotherallergies.Value != "")
            {
                List<PatientAllergy> tempothersallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryotherallergies.Value);
                DataTable dtAllergy = Helper.ToDataTable(tempothersallergy);
                if (dtAllergy.Select("is_delete = 0 and allergy_type = 7").Count() > 0)
                {
                    DataTable dtother = dtAllergy.Select("is_delete = 0 and allergy_type = 7").CopyToDataTable();
                    if (dtother.Rows[0]["allergy"].ToString() == "Tidak Ada Alergi")
                    {
                        gvw_others.DataSource = null;
                        gvw_others.DataBind();
                        rbother1.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScriptOther", "HideDivOtherAllergy();", true);
                    }
                    else
                    {
                        gvw_others.DataSource = dtAllergy.Select("is_delete = 0 and allergy_type = 7").CopyToDataTable();
                        gvw_others.DataBind();
                        rbother2.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScriptOther", "ShowHideDivOtherAllergy();", true);
                    }
                }
                else
                {
                    gvw_others.DataSource = null;
                    gvw_others.DataBind();
                    //rbother1.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScriptOther", "HideDivOtherAllergy();", true);
                }
            }
            else
            {
                gvw_others.DataSource = null;
                gvw_others.DataBind();
                //rbother1.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScriptOther", "HideDivOtherAllergy();", true);

            }

            UpdatePanelModalMedication.Update();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalEditMedication", "PreviewMedication();", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnEditRoutine_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnEditRoutine_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnAddRoutineMed_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            DataTable dt = new DataTable();
            
                List<PatientRoutineMedication> data = GetRowList_PatientRoutineMedication();
                if (data == null)
                {
                    dt.Columns.Add("patient_routine_medication_id");
                    dt.Columns.Add("medication");
                    dt.Columns.Add("is_delete");
                    dt.Columns.Add("routine_sales_item_id ");
                    dt.Columns.Add("routine_sales_item_code  ");
                    dt.Rows.Add(new Object[] { Guid.Empty, txtRoutineMed.Text, 0, 0, "" });
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
                    dtRoutine.Rows.Add(new Object[] { Guid.Empty, txtRoutineMed.Text, 0, 0, "" });
                    gvw_routinemed.DataSource = dtRoutine;
                    gvw_routinemed.DataBind();

                    DataTable dtroutinemed = dtRoutine;
                    dtroutinemed.Columns["medication"].ColumnName = "current_medication";

                    //Session["routinemed"] = dtroutinemed;
                    //checkIfExist(sender);
                }
            if (rbPengobatan2.Checked)
                hfenableroutine.Value = "1";
            else if (rbPengobatan1.Checked)
                hfenableroutine.Value = "0";
            else
                hfenableroutine.Value = "-1";
            txtRoutineMed.Text = "";
                txtRoutineMed.Focus();
            //CheckVisibleDiv(); 

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnAddRoutineMed_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnAddRoutineMed_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDeleteRoutineMed_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
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
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteRoutineMed_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteRoutineMed_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnAddSurgery_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
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
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnAddSurgery_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnAddSurgery_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnAddProcout_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            DataTable dt = new DataTable();
            List<PatientProcedureHistory> data = GetRowList_PatientProcout();
            if (data == null)
            {
                dt.Columns.Add("procedure_history_id");
                dt.Columns.Add("procedure_remarks");
                dt.Columns.Add("procedure_date");
                dt.Columns.Add("doctor_name");
                dt.Columns.Add("is_myself");
                dt.Rows.Add(new Object[] { Guid.Empty, txtProcoutName.Text, DateTime.Parse(txtProcoutDate.Text),"", 1 });
                gvw_procout.DataSource = dt;
                gvw_procout.DataBind();
            }
            else
            {
                DataTable dtProcout = Helper.ToDataTable(data);
                dtProcout.Rows.Add(new Object[] { Guid.Empty, txtProcoutName.Text, DateTime.Parse(txtProcoutDate.Text), "", 1 });
                gvw_procout.DataSource = dtProcout;
                gvw_procout.DataBind();
            }
            txtProcoutName.Text = "";
            txtProcoutName.Focus();
            CheckVisibleDiv();
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnAddProcout_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnAddProcout_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDeleteSurgery_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
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
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteSurgery_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteSurgery_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDeleteProcout_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
            HiddenField procedure_history_id = (HiddenField)gvw_procout.Rows[selRowIndex].FindControl("procedure_history_id");

            List<PatientProcedureHistory> data = GetRowList_PatientProcout();
            DataTable dt = Helper.ToDataTable(data);
            dt.Rows[selRowIndex].Delete();
            if (dt.Rows.Count > 0)
            {
                gvw_procout.DataSource = dt;
                gvw_procout.DataBind();
            }
            else
            {
                gvw_procout.DataSource = null;
                gvw_procout.DataBind();
            }
            CheckVisibleDiv();
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteProcout_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteProcout_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected List<PatientSurgery> GetRowList_PatientSurgery()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        List<PatientSurgery> data = new List<PatientSurgery>();
        try
        {         
            foreach (GridViewRow rows in gvw_surgery.Rows)
            {
                HiddenField patient_surgery_id = (HiddenField)rows.FindControl("patient_surgery_id");
                Label surgery_type = (Label)rows.FindControl("surgery_type");
                Label surgery_date = (Label)rows.FindControl("surgery_date");

                PatientSurgery row = new PatientSurgery();

                row.patient_surgery_id = Guid.Parse(patient_surgery_id.Value);
                row.surgery_type = surgery_type.Text;
                row.surgery_date = DateTime.Parse(surgery_date.Text).AddHours(7);
                row.is_delete = 0;
                data.Add(row);
            }

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "GetRowList_PatientSurgery", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "GetRowList_PatientSurgery", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());

        return data;
    }

    protected List<PatientProcedureHistory> GetRowList_PatientProcout()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        List<PatientProcedureHistory> data = new List<PatientProcedureHistory>();
        try
        {
            foreach (GridViewRow rows in gvw_procout.Rows)
            {
                HiddenField procedure_history_id = (HiddenField)rows.FindControl("procedure_history_id");
                Label procedure_remarks = (Label)rows.FindControl("procedure_remarks");
                Label procedure_date = (Label)rows.FindControl("procedure_date");

                PatientProcedureHistory row = new PatientProcedureHistory();

                row.procedure_history_id = Guid.Parse(procedure_history_id.Value);
                row.procedure_remarks = procedure_remarks.Text;
                row.procedure_date = DateTime.Parse(procedure_date.Text).AddHours(7);
                row.doctor_name = "";
                row.is_myself = 1;
                data.Add(row);
            }

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "GetRowList_PatientProcout", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "GetRowList_PatientProcout", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());

        return data;
    }

    protected void btnAddAllergy_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
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

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnAddAllergy_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnAddAllergy_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDeleteAllergy_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            DataTable dt;
            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
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
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteAllergy_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteAllergy_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnAddFoodAllergy_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
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

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnAddFoodAllergy_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnAddFoodAllergy_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDeleteFoods_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            DataTable dt;
            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
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

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteFoods_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteFoods_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnAddOtherAllergy_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            DataTable dt = new DataTable();
            List<PatientAllergy> data = GetRowList_PatientAllergy(7);

            if (data == null)
            {
                dt.Columns.Add("patient_allergy_id");
                dt.Columns.Add("allergy_type");
                dt.Columns.Add("allergy");
                dt.Columns.Add("allergy_reaction");
                dt.Columns.Add("is_delete");
                dt.Rows.Add(new Object[] { Guid.Empty, 2, txtNameOthers.Text, txtReactionOthers.Text, 0 });
                gvw_others.DataSource = dt;
                gvw_others.DataBind();
            }
            else
            {
                DataTable dtAllergy = Helper.ToDataTable(data);
                dtAllergy.Rows.Add(new Object[] { Guid.Empty, 2, txtNameOthers.Text, txtReactionOthers.Text, 0 });
                gvw_others.DataSource = dtAllergy;
                gvw_others.DataBind();
            }
            txtNameOthers.Focus();
            CheckVisibleDiv();

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnAddOtherAllergy_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnAddOtherAllergy_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDeleteOthers_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            DataTable dt;
            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
            HiddenField patient_allergy_id = (HiddenField)gvw_others.Rows[selRowIndex].FindControl("patient_allergy_id");

            List<PatientAllergy> data = GetRowList_PatientAllergy(7);
            dt = Helper.ToDataTable(data);
            dt.Rows[selRowIndex].SetField("is_delete", 1);
            if (dt.Select("is_delete = 0").Count() > 0)
            {
                gvw_others.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
                gvw_others.DataBind();
            }
            else
            {
                gvw_others.DataSource = null;
                gvw_others.DataBind();
            }

            CheckVisibleDiv();

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteOthers_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteOthers_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected List<PatientAllergy> GetRowList_PatientAllergy(int type)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        List<PatientAllergy> data = new List<PatientAllergy>();
        try
        {
            
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
            else if (type == 2)
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
            else if (type == 7)
            {
                foreach (GridViewRow rows in gvw_others.Rows)
                {
                    HiddenField patient_allergy_id = (HiddenField)rows.FindControl("patient_allergy_id");
                    Label allergy = (Label)rows.FindControl("allergy");
                    Label allergy_reaction = (Label)rows.FindControl("allergy_reaction");

                    PatientAllergy row = new PatientAllergy();

                    row.patient_allergy_id = Guid.Parse(patient_allergy_id.Value);
                    row.allergy_type = 7;
                    row.allergy = allergy.Text;
                    row.allergy_reaction = allergy_reaction.Text;
                    row.is_delete = 0;
                    data.Add(row);
                }
            }

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "GetRowList_PatientAllergy", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "GetRowList_PatientAllergy", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());

        return data;
    }

    protected List<PatientRoutineMedication> GetRowList_PatientRoutineMedication()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        List<PatientRoutineMedication> data = new List<PatientRoutineMedication>();
        try
        {
            
            foreach (GridViewRow rows in gvw_routinemed.Rows)
            {
                HiddenField patient_routine_medication_id = (HiddenField)rows.FindControl("patient_routine_medication_id");
                HiddenField routine_sales_item_id = (HiddenField)rows.FindControl("routine_sales_item_id");
                HiddenField routine_sales_item_code = (HiddenField)rows.FindControl("routine_sales_item_code");
                Label medication = (Label)rows.FindControl("medication");

                PatientRoutineMedication row = new PatientRoutineMedication();

                row.patient_routine_medication_id = Guid.Parse(patient_routine_medication_id.Value);
                row.medication = medication.Text;
                row.is_delete = 0;
                row.routine_sales_item_id = routine_sales_item_id == null ? 0 : long.Parse(routine_sales_item_id.Value);
                row.routine_sales_item_code = routine_sales_item_code == null ? "" : routine_sales_item_code.Value;
                data.Add(row);
            }
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "GetRowList_PatientRoutineMedication", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "GetRowList_PatientRoutineMedication", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());

        return data;
    }


    protected void btnsave_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        //listchecked = (List<ListChecked>)Session[Helper.SessionLabPathologyChecked];
        try
        {
            
            //bool qty_flag = StdPlanning.CheckQuantityPrescription(1);
            //bool qtycomp_flag = StdPlanning.CheckQuantityPrescription(2);
            //bool qtycomp_detail_flag = StdPlanning.CheckQuantityPrescription(3);
            //bool qtycons_flag = StdPlanning.CheckQuantityPrescription(4);
            //if (!qty_flag)
            //{
            //    ShowToastr("Please Check Drugs Qty.", "Save as Draft Alert!", "Warning");
            //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Drugs Qty');", true);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "hidecopy", "hidecopy();", true);
            //}
            //else if (!qtycomp_flag)
            //{
            //    ShowToastr("Please Check Drugs Qty.", "Save as Draft Alert!", "Warning");
            //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Compound Qty');", true);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "hidecopy", "hidecopy();", true);
            //}
            //else if (!qtycomp_detail_flag)
            //{
            //    ShowToastr("Please Check Drugs Qty.", "Save as Draft Alert!", "Warning");
            //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Compound Detail Qty');", true);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "hidecopy", "hidecopy();", true);
            //}
            //else if (!qtycons_flag)
            //{
            //    ShowToastr("Please Check Consumables Qty.", "Save as Draft Alert!", "Warning");
            //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Consumables Qty');", true);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "hidecopy", "hidecopy();", true);
            //}
            //else
            //{
                SaveDraft_SOAP();
            //}
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnsave_click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch(Exception ex)
        {
            var x = MappingforGetdataSOAPSession();
            string jsonfailed = new JavaScriptSerializer().Serialize(x);

            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnsave_click", StartTime, "Error", MyUser.GetUsername(), "", jsonfailed, ex.Message)); 
            //Log.Error(LogConfig.LogError(ex.Message.ToString() + jsonfailed), ex);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('Failed to Submit SOAP');", true);

            //log.Error(LogLibrary.Error("btnsave_click", Helper.GetLoginUser(this), ex.InnerException.Message));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnFindICD_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            upErroricd.Update();
            string searchicd = txtSearchItemICD.Text;
            gvw_icd.DataSource = null;

            DataTable vardiseaseclassification = clsSOAP.getDiseaseClassification(searchicd);
            vardiseaseclassification.DefaultView.Sort = "DiseaseClassification";
            gvw_icd.DataSource = vardiseaseclassification;
            gvw_icd.DataBind();

            //txtSearchItemICD.Text = "";
            //txtSearchItemICD.Focus();

            upErroricd.Update();
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnFindICD_click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnFindICD_click", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    //protected void btnSaveTravel(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        log.Info(LogLibrary.Logging("S", "btnSaveTravel", Helper.GetLoginUser(this), ""));

    //        if(txtdatescheduletravel.Text != "")
    //        {
    //            hdnSchedule_travel.Value = txtdatescheduletravel.Text;
    //        }

    //        if (rbtravel1.Checked)
    //            hdnCondition_travel.Value = "Fit to fly as scheduled";
    //        else if (rbtravel2.Checked)
    //            hdnCondition_travel.Value = "Not fit to fly as scheduled";
    //        else if (rbtravel3.Checked)
    //            hdnCondition_travel.Value = "Anticipated date fit to fly: " + txtdatefittofly.Text;

    //        if (rbseating1.Checked)
    //            hdnSeating_type.Value = "Commercial flight regular seating";
    //        else if (rbseating2.Checked)
    //            hdnSeating_type.Value = "Commercial flight Business class";
    //        else if (rbseating3.Checked)
    //            hdnSeating_type.Value = "Stretcher Case";
    //        else if (rbseating4.Checked)
    //            hdnSeating_type.Value = "Air-ambulance";

    //        if (rbescort1.Checked)
    //            hdnEscort_type.Value = "Unescorted";
    //        else if (rbescort2.Checked)
    //            hdnEscort_type.Value = "non-Medical Escort";
    //        else if (rbescort3.Checked)
    //            hdnEscort_type.Value = "Medical Escort:" + ddlescort.SelectedItem;

    //        if (chkSpecialNeeds1.Checked)
    //            hdnSpecial_Needs.Value = "Wheel Chair Assistance,";
    //        if (chkSpecialNeeds2.Checked)
    //            hdnSpecial_Needs.Value = hdnSpecial_Needs + "Oxygen Supplementation,";
    //        if (chkSpecialNeeds3.Checked)
    //            hdnSpecial_Needs.Value = hdnSpecial_Needs + "Need Mechanical Ventilation,";
    //        if (chkSpecialNeeds4.Checked)
    //            hdnSpecial_Needs.Value = hdnSpecial_Needs + "Need Vacuum Mattress,";

    //        if (hdnSchedule_travel.Value != "")
    //            txtTravelRecommendation.Text = "\u2022" + " This patient is scheduled to travel on: " + hdnSchedule_travel.Value;
    //        else
    //            txtTravelRecommendation.Text = "\u2022" + " This patient is scheduled to travel on: - ";

    //        if (hdnCondition_travel.Value != "")
    //            txtTravelRecommendation.Text = txtTravelRecommendation.Text + "\n" + "\u2022 " + hdnCondition_travel.Value;
    //        else
    //            txtTravelRecommendation.Text = txtTravelRecommendation.Text + "\n" + "\u2022 " + "Patient's Condition: - ";

    //        if(hdnSeating_type.Value != "")
    //            txtTravelRecommendation.Text = txtTravelRecommendation.Text + "\n" + "\u2022 " + "Recommended Flight Seating Type: " + hdnSeating_type.Value;
    //        else
    //            txtTravelRecommendation.Text = txtTravelRecommendation.Text + "\n" + "\u2022 " + "Recommended Flight Seating Type: - ";

    //        if(hdnEscort_type.Value != "")
    //            txtTravelRecommendation.Text = txtTravelRecommendation.Text + "\n" + "\u2022 " + "Escort Type: " + hdnEscort_type.Value;
    //        else
    //            txtTravelRecommendation.Text = txtTravelRecommendation.Text + "\n" + "\u2022 " + "Escort Type: - ";

          
    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hidetravel", "$('#modalRekomendasiTravel').modal('hide');", true);
    //        txtTravelRecommendation.Focus();
    //        log.Info(LogLibrary.Logging("E", "btnSaveTravel", Helper.GetLoginUser(this), "Finish btnSaveTravel"));

    //    }
    //    catch(Exception ex)
    //    {
    //        log.Error(LogLibrary.Error("btnSaveTravel", Helper.GetLoginUser(this), ex.InnerException.Message));
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
    //    }
    //}

    protected void icditemselected_onclick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        LinkButton salesItemName = (LinkButton)gvw_icd.Rows[selRowIndex].FindControl("DiseaseClassification");
        if (txtPrimary.Text == "")
        {
            txtPrimary.Text = salesItemName.Text;
        }
        else
        {
            txtPrimary.Text = txtPrimary.Text + "\n" + salesItemName.Text;
            txtPrimary.Rows = txtPrimary.Text.Split('\n').Length;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "close", "loseBox(); copytextto();", true);


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "icditemselected_onclick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());

    }

    protected void btnSubmitDisable_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        btnsubmitdisable.Enabled = false;
        btnsubmitdisableTC.Enabled = false;

        try
        {

            //bool qty_flag = true, qtycons_flag = true, qtyadddrug_flag = true, qtyaddcons_flag = true;
            //if (hftakedate.Value == "")
            //{
            //    qty_flag = StdPlanning.CheckQuantityPrescription(1);
            //    //bool qtycomp_flag = StdPlanning.CheckQuantityPrescription(2);
            //    //bool qtycomp_detail_flag = StdPlanning.CheckQuantityPrescription(3);
            //    qtycons_flag = StdPlanning.CheckQuantityPrescription(4);
            //}
            //if (hfadditionaltakedate.Value == "")
            //{
            //    qtyadddrug_flag = StdPlanning.CheckQuantityPrescription(5);
            //    qtyaddcons_flag = StdPlanning.CheckQuantityPrescription(6);
            //}
            //bool ismandatory_flag = StdPlanning.isMandatory();
            //if (!qty_flag)
            //{
            //    HFlagdrug.Value = "1";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Drugs Qty/Dose/Frequency/Route/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
            //}
            ////else if (!qtycomp_flag)
            ////{
            ////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "toastr.warning('Please Check Compound Qty', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
            ////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Compound Qty');", true);
            ////}
            ////else if (!qtycomp_detail_flag)
            ////{
            ////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "toastr.warning('Please Check Compound Detail Qty', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
            ////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Compound Detail Qty');", true);
            ////}
            //else if (!qtycons_flag)
            //{
            //    HFlagcons.Value = "1";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Consumables Qty/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
            //}
            //else if (!qtyadddrug_flag)
            //{
            //    HFlagdrugadd.Value = "1";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Additional Drugs Qty/Dose/Frequency/Route/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
            //}
            //else if (!qtyaddcons_flag)
            //{
            //    HFlagconsadd.Value = "1";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Additional Consumables Qty/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
            //}
            //else if (!ismandatory_flag)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mandatory", "warningnotificationOption(); toastr.warning('Clinical Diagnosis is mandatory <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
            //}
            //else
            //{
            //    HFlagdrug.Value = "0";
            //    HFlagdrugadd.Value = "0";
            //    HFlagcons.Value = "0";
            //    HFlagconsadd.Value = "0";
            //    Submit_SOAP_disable();
            //}

            Submit_SOAP_disable();
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnSubmitDisable_click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch(Exception ex)
        {
            var x = MappingforGetdataSOAPSession();
            string jsonfailed = new JavaScriptSerializer().Serialize(x);

            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnSubmitDisable_click", StartTime, "Error", MyUser.GetUsername(), "", jsonfailed, ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString() + jsonfailed), ex);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('Failed to Submit SOAP');", true);

            //log.Error(LogLibrary.Error("btnSubmitDisable_click", Helper.GetLoginUser(this), ex.InnerException.Message));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Failed to Submit');", true);
        }

        btnsubmitdisable.Enabled = true;
        btnsubmitdisableTC.Enabled = true;
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnreloadsave_page(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalsavesuccess", "$('#modalsavesuccess').modal('hide');", true);
        Response.Redirect(Request.RawUrl);
        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnreloadsave_page", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnSubmit_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        btnsubmit.Enabled = false;
        btnsubmitTC.Enabled = false;

        try
        {
            //bool qty_flag = true, qtycons_flag = true, qtyadddrug_flag = true, qtyaddcons_flag = true;
            //List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            //string autosave = "";
            //if (orgsetting.Where(y => y.setting_name.ToLower() == "AUTO_SAVE".ToLower()).Count() > 0)
            //{
            //    autosave = orgsetting.Find(y => y.setting_name.ToUpper() == "AUTO_SAVE".ToUpper()).setting_value.ToString();
            //}
            //if (hftakedate.Value == "")
            //{
            //    qty_flag = StdPlanning.CheckQuantityPrescription(1);
            //    //bool qtycomp_flag = StdPlanning.CheckQuantityPrescription(2);
            //    //bool qtycomp_detail_flag = StdPlanning.CheckQuantityPrescription(3);
            //    qtycons_flag = StdPlanning.CheckQuantityPrescription(4);

            //}
            //if (hfadditionaltakedate.Value == "")
            //{
            //    qtyadddrug_flag = StdPlanning.CheckQuantityPrescription(5);
            //    qtyaddcons_flag = StdPlanning.CheckQuantityPrescription(6);

            //}
            //bool ismandatory_flag = StdPlanning.isMandatory();
            //if (!qty_flag)
            //{
            //    HFlagdrug.Value = "1";
            //    if (autosave == "TRUE")
            //    {
            //        AutoSaveSOAP();
            //    }

            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Drugs Qty/Dose/Frequency/Route/Instruction<br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmit').modal('hide');", true);
            //}
            ////else if (!qtycomp_flag)
            ////{
            ////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "toastr.warning('Please Check Compound Qty', 'Submit Alert!');$('#modalsubmit').modal('hide');", true);
            ////    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Compound Qty');", true);
            ////}
            ////else if (!qtycomp_detail_flag)
            ////{
            ////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "toastr.warning('Please Check Compound Detail Qty', 'Submit Alert!');$('#modalsubmit').modal('hide');", true);
            ////    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Compound Detail Qty');", true);
            ////}
            //else if (!qtycons_flag)
            //{
            //    HFlagcons.Value = "1";
            //    if (autosave == "TRUE")
            //    {
            //        AutoSaveSOAP();
            //    }
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Consumables Qty/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmit').modal('hide');", true);
            //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Consumables Qty');", true);
            //}
            //else if (!qtyadddrug_flag)
            //{
            //    HFlagdrugadd.Value = "1";
            //    if (autosave == "TRUE")
            //    {
            //        AutoSaveSOAP();
            //    }
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Additional Drugs Qty/Dose/Frequency/Route/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmit').modal('hide');", true);
            //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Additional Drugs Qty');", true);
            //}
            //else if (!qtyaddcons_flag)
            //{
            //    HFlagconsadd.Value = "1";
            //    if (autosave == "TRUE")
            //    {
            //        AutoSaveSOAP();
            //    }
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Additional Consumables Qty/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmit').modal('hide');", true);
            //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Please Check Additional Consumables Qty');", true);
            //}
            //else if (!ismandatory_flag)
            //{
            //    if (autosave == "TRUE")
            //    {
            //        AutoSaveSOAP();
            //    }
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mandatory", "warningnotificationOption(); toastr.warning('Clinical Diagnosis is mandatory <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmit').modal('hide');", true);
            //}
            //else
            //{
            //    HFlagdrug.Value = "0";
            //    HFlagdrugadd.Value = "0";
            //    HFlagcons.Value = "0";
            //    HFlagconsadd.Value = "0";
            //    Submit_SOAP();
            //}

            Submit_SOAP();
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnSubmit_click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch(Exception ex)
        {
            var x = MappingforGetdataSOAPSession();
            string jsonfailed = new JavaScriptSerializer().Serialize(x);

            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnSubmit_click", StartTime, "Error", MyUser.GetUsername(), "", jsonfailed, ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString() + jsonfailed), ex);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('Failed to Submit SOAP');", true);

            //log.Error(LogLibrary.Error("btnSubmit_click", Helper.GetLoginUser(this), ex.Message));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Failed to Submit');", true);
        }

        btnsubmit.Enabled = true;
        btnsubmitTC.Enabled = true;
        //Log.Info(LogConfig.LogEnd());
    }

    public void Submit_SOAP_disable()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Find(y => y.setting_name.ToUpper() == "MANDATORY_SOAP".ToUpper()).setting_value == "TRUE")
            {
                if (Complaint.Text != "" && Anamnesis.Text != "" && txtPrimary.Text != "" && txtOthers.Text != "" && txtPlanning.Text != "")
                {
                    //SOAP soapmodel = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
                    var soapmodel = MappingforGetdataSOAPSession();


                    ((List<Subjective>)soapmodel.subjective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("f815de0e-e57a-4250-a2be-bdd27c1876ac"));

                    if (hdnTindakan.Value != "")
                    {
                        List<Subjective> listTindakan = new JavaScriptSerializer().Deserialize<List<Subjective>>(hdnTindakan.Value);
                        soapmodel.subjective.AddRange(listTindakan);
                    }

                    ((List<Subjective>)soapmodel.subjective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("0cbb6a1d-392b-4fed-8e8a-696bea3cbc32"));

                    if (hdnDeleteReason.Value != "")
                    {
                        List<Subjective> listDeleteReason = new JavaScriptSerializer().Deserialize<List<Subjective>>(hdnDeleteReason.Value);
                        soapmodel.subjective.AddRange(listDeleteReason);
                    }


                    ((List<Objective>)soapmodel.objective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c"));
                    if (hdnFallRisk.Value != "")
                    {
                        List<Objective> listobjfallrisk = new JavaScriptSerializer().Deserialize<List<Objective>>(hdnFallRisk.Value);
                        soapmodel.objective.AddRange(listobjfallrisk);
                    }
                    ((List<Objective>)soapmodel.objective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("B0C9D8E0-7533-43CD-BFCF-E8C0AC1D4B7E"));
                    if (hdnFallRiskHandling.Value != "")
                    {
                        List<Objective> listobjfallriskHandling = new JavaScriptSerializer().Deserialize<List<Objective>>(hdnFallRiskHandling.Value);
                        soapmodel.objective.AddRange(listobjfallriskHandling);
                    }

                    ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf")).remarks = Complaint.Text;
                    ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0")).remarks = Anamnesis.Text;

                    string radhamil = "";
                    if (Radiohamilno.Checked == true)
                    {
                        radhamil = "false";
                    }
                    else if (Radiohamilyes.Checked == true)
                    {
                        radhamil = "true";
                    }
                    ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")).value = radhamil;
                    string radsusu = "";
                    if (Radiosusuno.Checked == true)
                    {
                        radsusu = "false";
                    }
                    else if (Radiosusuyes.Checked == true)
                    {
                        radsusu = "true";
                    }
                    ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")).value = radsusu;

                    ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade")).remarks = hdnEndemicArea.Value;

                    if (rbnutrisi2.Checked)
                        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).remarks = hdnNutrition.Value;
                    else if (rbnutrisi1.Checked)
                        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).remarks = "";

                    if (rbpuasa2.Checked)
                        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).remarks = hdnFasting.Value;
                    else if (rbpuasa1.Checked)
                        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).remarks = "";

                    if (rbOperas2.Checked)
                    {
                        List<PatientSurgery> tempsurgery = new JavaScriptSerializer().Deserialize<List<PatientSurgery>>(hdnhistorysurgery.Value);
                        soapmodel.patient_surgery = tempsurgery;
                        //soapmodel.patient_surgery = GetRowList_PatientSurgery();
                    }
                    else if (rbOperasi.Checked)
                    {
                        List<PatientSurgery> tempsurgery = new JavaScriptSerializer().Deserialize<List<PatientSurgery>>(hdnhistorysurgery.Value);
                        soapmodel.patient_surgery = tempsurgery;
                        
                    }
                    else
                    {
                        soapmodel.patient_surgery.Clear();
                    }

                    if (soapmodel.patient_surgery == null)
                        soapmodel.patient_surgery = new List<PatientSurgery>();

                    if (rbProcOut2.Checked)
                    {
                        List<PatientProcedureHistory> tempprocout = new JavaScriptSerializer().Deserialize<List<PatientProcedureHistory>>(hdnProcedureOutside.Value);
                        soapmodel.patient_procedure = tempprocout;
                    }
                    else if (rbProcOut1.Checked)
                    {
                        soapmodel.patient_procedure.Clear();
                    }

                    if (soapmodel.patient_procedure == null)
                        soapmodel.patient_procedure = new List<PatientProcedureHistory>();

                    List<PatientSpecialNotification> datareminder = GetRowList_ReminderNotes();
                    hdnremindernotes.Value = new JavaScriptSerializer().Serialize(datareminder);
                    List<PatientSpecialNotification> tempreminder = new JavaScriptSerializer().Deserialize<List<PatientSpecialNotification>>(hdnremindernotes.Value);
                    soapmodel.patient_notification = tempreminder;

                    if (soapmodel.patient_notification == null)
                        soapmodel.patient_notification = new List<PatientSpecialNotification>();

                    if (hfenableroutine.Value == "1")
                    {
                        if (hdnhistoryroutine.Value != "")
                        {
                            List<PatientRoutineMedication> tempcurrmed = new JavaScriptSerializer().Deserialize<List<PatientRoutineMedication>>(hdnhistoryroutine.Value);
                            soapmodel.patient_medication = tempcurrmed;
                        }
                        else
                            soapmodel.patient_medication.Clear();
                    }
                    else if (hfenableroutine.Value == "0")
                    {
                        if (hdnhistoryroutine.Value != "")
                        {
                            List<PatientRoutineMedication> tempcurrmed = new JavaScriptSerializer().Deserialize<List<PatientRoutineMedication>>(hdnhistoryroutine.Value);
                            soapmodel.patient_medication = tempcurrmed;
                        }
                        else
                            soapmodel.patient_medication.Clear();
                        
                    }
                    else
                    {
                        soapmodel.patient_medication.Clear();
                    }

                    if (soapmodel.patient_medication == null)
                        soapmodel.patient_medication = new List<PatientRoutineMedication>();

                    List<PatientRoutineMedication> pengobatan_nodata = new List<PatientRoutineMedication>();
                    PatientRoutineMedication row_pengobatan = new PatientRoutineMedication();
                    row_pengobatan.patient_routine_medication_id = Guid.Empty;
                    row_pengobatan.medication = "Tidak Ada Pengobatan";
                    row_pengobatan.routine_sales_item_code = "";
                    row_pengobatan.routine_sales_item_id = 0;
                    row_pengobatan.is_delete = 0;

                    if (hfenableroutine.Value == "0")
                    {
                        pengobatan_nodata.Add(row_pengobatan);
                        soapmodel.patient_medication = pengobatan_nodata;
                    }

                    if (rbdrug2.Checked)
                    {
                        List<PatientAllergy> tempdrugsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistorydrugallergies.Value);
                        soapmodel.patient_allergy = tempdrugsallergy;
                        if (rbfood2.Checked)
                        {
                            soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(2));
                        }
                        if (rbother2.Checked)
                        {
                            soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(7));
                        }
                    }
                    else if (rbfood2.Checked)
                    {     
                        List<PatientAllergy> tempfoodsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryfoodallergies.Value);
                        soapmodel.patient_allergy = tempfoodsallergy;
                        if (rbother2.Checked)
                        {
                            soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(7));
                        }   
                    }
                    else if (rbother2.Checked)
                    {
                        List<PatientAllergy> tempothersallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryotherallergies.Value);
                        soapmodel.patient_allergy = tempothersallergy;
                    }
                    else
                    {
                        soapmodel.patient_allergy.Clear();
                    }

                    if (soapmodel.patient_allergy == null)
                        soapmodel.patient_allergy = new List<PatientAllergy>();

                    //initial no data
                    List<PatientAllergy> drug_noalergi = new List<PatientAllergy>();
                    List<PatientAllergy> food_noalergi = new List<PatientAllergy>();
                    List<PatientAllergy> other_noalergi = new List<PatientAllergy>();
                    PatientAllergy rowdrug = new PatientAllergy();
                    rowdrug.patient_allergy_id = Guid.Empty;
                    rowdrug.allergy_type = 1;
                    rowdrug.allergy = "Tidak Ada Alergi";
                    rowdrug.allergy_reaction = "Tidak Ada Reaksi";
                    rowdrug.is_delete = 0;
                    PatientAllergy rowfood = new PatientAllergy();
                    rowfood.patient_allergy_id = Guid.Empty;
                    rowfood.allergy_type = 2;
                    rowfood.allergy = "Tidak Ada Alergi";
                    rowfood.allergy_reaction = "Tidak Ada Reaksi";
                    rowfood.is_delete = 0;
                    PatientAllergy rowother = new PatientAllergy();
                    rowother.patient_allergy_id = Guid.Empty;
                    rowother.allergy_type = 7;
                    rowother.allergy = "Tidak Ada Alergi";
                    rowother.allergy_reaction = "Tidak Ada Reaksi";
                    rowother.is_delete = 0;
                    //end initial no data

                    //set if no allergy
                    if (rbdrug1.Checked)
                    {
                        drug_noalergi.Add(rowdrug);
                        soapmodel.patient_allergy.AddRange(drug_noalergi);
                    }
                    if (rbfood1.Checked)
                    {
                        food_noalergi.Add(rowfood);
                        soapmodel.patient_allergy.AddRange(food_noalergi);
                    }
                    if (rbother1.Checked)
                    {
                        other_noalergi.Add(rowother);
                        soapmodel.patient_allergy.AddRange(other_noalergi);
                    }

                    List<PatientDiseaseHistory> templistdisease = new List<PatientDiseaseHistory>();
                    if (rbpribadi2.Checked)
                    {
                        List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);
                        //soapmodel.patient_disease = tempdisease;
                        templistdisease = tempdisease;
                    }
                    else if (rbpribadi1.Checked)
                    {
                        List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);
                        //soapmodel.patient_disease = tempdisease;
                        templistdisease = tempdisease;
                    }
                    else
                    {
                        soapmodel.patient_disease.Clear();
                    }

                    if (rbkeluarga2.Checked)
                    {
                        List<PatientDiseaseHistory> tempdiseasefam = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnFamilyDiseaseHistory.Value);
                        templistdisease.AddRange(tempdiseasefam);
                    }
                    else if (rbkeluarga1.Checked)
                    {
                        List<PatientDiseaseHistory> tempdiseasefam = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnFamilyDiseaseHistory.Value);
                        templistdisease.AddRange(tempdiseasefam);
                    }
                    
                    soapmodel.patient_disease = templistdisease;

                    if (soapmodel.patient_disease == null)
                        soapmodel.patient_disease = new List<PatientDiseaseHistory>();

                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("ae3ca8c2-eab0-41b6-9e3e-3ecb8071a9d0")).value = txtbloodlow.Text;
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e5efd220-b68e-4652-ad03-d56ef29fcebb")).value = txtbloodhigh.Text;
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("78cbb61f-4a11-470a-b770-1a44eb04357f")).value = txtpulserate.Text;
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e6ae2ea9-b321-4756-bf96-2dc232e4a7ba")).value = txtrespiratory.Text;
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e903246c-df95-4fe0-96d2-cf90c036d3d7")).value = txtspo.Text;
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("2eeca752-a2ea-4426-b3cf-c1ea3833bf30")).value = txttemperature.Text;
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("52ce9350-bfb2-4072-8893-d0c6cf8b3634")).value = txtweight.Text;
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("2a8dbddb-edfe-4704-876e-5a2d735bb541")).value = txtheight.Text;
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D")).remarks = txtOthers.Text;
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D")).value = "Abnormal";
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("A8E0013B-0443-4E7A-B670-4DB9362B40E4")).value = txtlingkarkepala.Text;
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("19516BE9-9D54-4E30-8E90-0A8B41F5BA66")).value = txtbmi.Text;


                    if (lbleyetotal.Text == "_")
                        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("a9c5cd3c-1e02-4db2-a047-2f1983d1315b")).value = "";
                    else
                        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("a9c5cd3c-1e02-4db2-a047-2f1983d1315b")).value = hdnEye.Value;

                    if (lblmovetotal.Text == "_")
                        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("89b583a5-3003-43ab-9693-60ea6181c8d5")).value = "";
                    else
                        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("89b583a5-3003-43ab-9693-60ea6181c8d5")).value = hdnMove.Value;

                    if (lblverbaltotal.Text == "_")
                        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("fe4cf48c-17a6-4720-ad23-186517dd9c85")).value = "";
                    else
                        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("fe4cf48c-17a6-4720-ad23-186517dd9c85")).value = hdnVerbal.Value;

                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176")).value = hdnpainscale.Value;

                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = hdnMentalStatus.Value;
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).remarks = hdnMentalStatusremark.Value;

                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = hdnConsciousness.Value;
                    
                    foreach (var assessment in ((List<Assessment>)soapmodel.assessment).Where(x => x.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")))
                    {
                        if (assessment.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47"))
                        {//"Primary"
                            assessment.remarks = txtPrimary.Text;
                        }
                    }
                    foreach (var planning in ((List<Planning>)soapmodel.planning).Where(x => x.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6")))
                    {
                        if (planning.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
                        {
                            planning.remarks = txtPlanning.Text;
                        }
                    }
                    ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")).remarks = txtHasilTindakan.Text;
                    ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")).value = soapmodel.patient_id.ToString();

                    ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("7198D4D5-7670-4853-9AFA-CD7C606C5C27")).value = "";
                    ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("7198D4D5-7670-4853-9AFA-CD7C606C5C27")).remarks = hdnSchedule_travel.Value;
                    ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3893BF09-82A2-449A-B5F0-D3A2BF9905C1")).value = hdnCondition_travel.Value;
                    ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3893BF09-82A2-449A-B5F0-D3A2BF9905C1")).remarks = hdncondition_date.Value;
                    ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("A4D139D7-67E2-44BC-A5E7-8BB17DD3F21E")).value = hdnSeating_type.Value;
                    ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("A4D139D7-67E2-44BC-A5E7-8BB17DD3F21E")).remarks = "";
                    ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("80A8E43A-53BF-4A32-B992-C9F32C7AA458")).value = hdnEscort_type.Value;
                    ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("80A8E43A-53BF-4A32-B992-C9F32C7AA458")).remarks = hdnescort_ddl.Value;
                    ((List<Planning>)soapmodel.planning).RemoveAll(x => x.soap_mapping_id == Guid.Parse("F3378407-B8A6-45FF-A27B-87F3CCF0F2B4"));
                    if (hdnSpecial_Needs.Value != "")
                    {
                        List<Planning> listSpecialNeeds = new List<Planning>();
                        var dataspecialneeds = hdnSpecial_Needs.Value.Split(',');
                        for (int i = 0; i < dataspecialneeds.Count(); i++)
                        {
                            Planning P = new Planning();
                            P.soap_mapping_id = Guid.Parse("F3378407-B8A6-45FF-A27B-87F3CCF0F2B4");
                            P.soap_mapping_name = "SPECIAL_NEEDS";
                            P.planning_id = Guid.Empty;
                            P.value = dataspecialneeds[i].ToString();
                            P.remarks = "";
                            listSpecialNeeds.Add(P);
                        }
                        soapmodel.planning.AddRange(listSpecialNeeds);
                    }
                    ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("30F9892A-22C7-4C8B-8005-BE08E105F05F")).value = "";
                    ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("30F9892A-22C7-4C8B-8005-BE08E105F05F")).remarks = txtTravelRecommendation.Text.ToString();

                    soapmodel.procedure_notes = txtnotes.Text;
                    soapmodel.procedure_diagnosis = (List<ProcedureDiagnosis>)Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value];

                    soapmodel = StdPlanning.GetPlanningValues(soapmodel);
                    soapmodel = StdImunisasi.GetImunisasiValues(soapmodel);

                    string templateid = Request.QueryString["PageSoapId"];
                    if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
                    {

                    }
                    else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
                    {
                        soapmodel = StdTriage.GetTriageValues(soapmodel);
                        Emergency_GetValues(soapmodel, true);
                    }
                    else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
                    {
                        //soapmodel = StdObgyn.GetObgynValues(soapmodel);
                        soapmodel = getObgynData(soapmodel);
                    }
                    else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
                    {
                        soapmodel = getPediatricData(soapmodel);
                    }

                    soapmodel.save_mode = 1;

                    //-----------------------------------------------------------------------------

                    long itemid = long.Parse(hfitemid.Value);
                    double ConsultationAmount = double.Parse(hfconsfee.Value);
                    double discountamount = double.Parse(hfdiscamount.Value);
                    string appointmentid = Request.QueryString["AppointmentId"];
                    string username = Helper.GetLoginUser(this);

                    Dictionary<string, string> logParam = new Dictionary<string, string>
                    {
                        { "Item_ID", itemid == null ? "-" : itemid.ToString() },
                        { "Consultation_Amount", ConsultationAmount == null ? "-" : ConsultationAmount.ToString() },
                        { "Discount_Amount", discountamount == null ? "-" : discountamount.ToString() },
                        { "Procedure_Notes", "-" },
                        { "PageSoap_ID", hfPageSoapId.Value },
                        { "Item_Name", hfitemname.Value },
                        { "Appointment_ID", appointmentid == null ? "-" : appointmentid.ToString() },
                        { "Username", username },
                    };
                    //log.debug(logconfig.LogStart("MappingforSubmitdataSOAP", logParam, LogConfig.JsonToString(soapmodel)));
                    var getMap = MappingforSubmitdataSOAP(soapmodel, itemid, ConsultationAmount, discountamount, "-", hfPageSoapId.Value, hfitemname.Value);
                    var JsongetMap = (JObject)JsonConvert.DeserializeObject<dynamic>(getMap.Result);
                    var Status = JsongetMap.Property("status").Value.ToString();
                    var Message = JsongetMap.Property("message").Value.ToString();
                    //log.debug(logconfig.LogEnd("MappingforSubmitdataSOAP", Status, Message));

                    var data = JsongetMap.Property("data").Value.ToString();

                    if (Status == "Fail")
                    {
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Failed to Submit SOAP');", true);
                        fail_box("Failed to Submit SOAP", Message);
                        ActionBackUpToSession();
                    }
                    else
                    {
                        //Submit Health Record
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Submit_HR", "SubmitIframe_HI();", true);

                        //reset the session backup
                        Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]] = null;

                        if (data != "SUCCESS")
                        {
                            if (data.Contains("MODIFIED"))
                            {
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('SOAP already modified by other user. <br /> Please refresh this page for update data. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "$('#modalnotifalreadymodif').modal('show');", true);
                            }
                            else
                            {
                                List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                                markerlist.Find(x => x.key == "TAKENmarker").value = data;
                                Session[Helper.SESSIONmarker] = markerlist;
                                //Helper.TAKENmarker = data;

                                if (hf_flagrujukan_aido.Value == "aido")
                                {
                                    string ip = GetLocalIPAddress();
                                    string docid = Helper.GetDoctorID(this);
                                    string param = "previewRujukan('" + ip + "','" + Helper.organizationId + "','" + hfPatientId.Value + "','" + hfAdmissionId.Value + "','" + hfEncounterId.Value + "','" + docid + "');";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rujukan", param, true);
                                    hf_flagrujukan_aido.Value = "";
                                }

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refresh", "window.location.reload();", true);
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "taken();", true);
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "$('#modalcompdetail').modal('hide');", true);
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Prescription already taken by Pharmacy');", true);
                            }
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "successsubmit();", true);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('Submit and Sign successful');", true);
                            //Response.Redirect(Request.RawUrl);

                            List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                            markerlist.Find(x => x.key == "SUBMITSOAPmarker").value = "marked";
                            Session[Helper.SESSIONmarker] = markerlist;

                            if (hf_flagrujukan_aido.Value == "aido")
                            {
                                string ip = GetLocalIPAddress();
                                string docid = Helper.GetDoctorID(this);
                                string param = "previewRujukan('" + ip + "','" + Helper.organizationId + "','" + hfPatientId.Value + "','" + hfAdmissionId.Value + "','" + hfEncounterId.Value + "','" + docid + "');";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rujukan", param, true);
                                hf_flagrujukan_aido.Value = "";
                            }

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refresh", "window.location.reload();", true);
                            
                        }

                    }

                    //Response.Redirect(Request.RawUrl);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('SOAP can not be empty');", true);
                }
            }
            else
            {
                //SOAP soapmodel = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
                var soapmodel = MappingforGetdataSOAPSession();


                ((List<Subjective>)soapmodel.subjective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("f815de0e-e57a-4250-a2be-bdd27c1876ac"));

                List<Subjective> listTindakan = new JavaScriptSerializer().Deserialize<List<Subjective>>(hdnTindakan.Value);
                soapmodel.subjective.AddRange(listTindakan);
                ((List<Subjective>)soapmodel.subjective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("0cbb6a1d-392b-4fed-8e8a-696bea3cbc32"));

                List<Subjective> listDeleteReason = new JavaScriptSerializer().Deserialize<List<Subjective>>(hdnDeleteReason.Value);
                soapmodel.subjective.AddRange(listDeleteReason);

                ((List<Objective>)soapmodel.objective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c"));
                if (hdnFallRisk.Value != "")
                {
                    List<Objective> listobjfallrisk = new JavaScriptSerializer().Deserialize<List<Objective>>(hdnFallRisk.Value);
                    soapmodel.objective.AddRange(listobjfallrisk);
                }
                ((List<Objective>)soapmodel.objective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("B0C9D8E0-7533-43CD-BFCF-E8C0AC1D4B7E"));
                if (hdnFallRiskHandling.Value != "")
                {
                    List<Objective> listobjfallriskHandling = new JavaScriptSerializer().Deserialize<List<Objective>>(hdnFallRiskHandling.Value);
                    soapmodel.objective.AddRange(listobjfallriskHandling);
                }

                ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf")).remarks = Complaint.Text;
                ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0")).remarks = Anamnesis.Text;

                string radhamil = "";
                if (Radiohamilno.Checked == true)
                {
                    radhamil = "false";
                }
                else if (Radiohamilyes.Checked == true)
                {
                    radhamil = "true";
                }
                ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")).value = radhamil;
                string radsusu = "";
                if (Radiosusuno.Checked == true)
                {
                    radsusu = "false";
                }
                else if (Radiosusuyes.Checked == true)
                {
                    radsusu = "true";
                }
                ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")).value = radsusu;

                ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade")).remarks = hdnEndemicArea.Value;

                if (rbnutrisi2.Checked)
                    ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).remarks = hdnNutrition.Value;
                else if (rbnutrisi1.Checked)
                    ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).remarks = "";

                if (rbpuasa2.Checked)
                    ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).remarks = hdnFasting.Value;
                else if (rbpuasa1.Checked)
                    ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).remarks = "";

                if (rbOperas2.Checked)
                {
                    List<PatientSurgery> tempsurgery = new JavaScriptSerializer().Deserialize<List<PatientSurgery>>(hdnhistorysurgery.Value);
                    soapmodel.patient_surgery = tempsurgery;
                    //soapmodel.patient_surgery = GetRowList_PatientSurgery();
                }
                else if (rbOperasi.Checked)
                {
                    List<PatientSurgery> tempsurgery = new JavaScriptSerializer().Deserialize<List<PatientSurgery>>(hdnhistorysurgery.Value);
                    soapmodel.patient_surgery = tempsurgery;
                    
                }
                else
                {
                    soapmodel.patient_surgery.Clear();
                }

                if (soapmodel.patient_surgery == null)
                    soapmodel.patient_surgery = new List<PatientSurgery>();

                if (rbProcOut2.Checked)
                {
                    List<PatientProcedureHistory> tempprocout = new JavaScriptSerializer().Deserialize<List<PatientProcedureHistory>>(hdnProcedureOutside.Value);
                    soapmodel.patient_procedure = tempprocout;
                }
                else if (rbProcOut1.Checked)
                {
                    soapmodel.patient_procedure.Clear();
                }

                if (soapmodel.patient_procedure == null)
                    soapmodel.patient_procedure = new List<PatientProcedureHistory>();

                List<PatientSpecialNotification> datareminder = GetRowList_ReminderNotes();
                hdnremindernotes.Value = new JavaScriptSerializer().Serialize(datareminder);
                List<PatientSpecialNotification> tempreminder = new JavaScriptSerializer().Deserialize<List<PatientSpecialNotification>>(hdnremindernotes.Value);
                soapmodel.patient_notification = tempreminder;

                if (soapmodel.patient_notification == null)
                    soapmodel.patient_notification = new List<PatientSpecialNotification>();

                if (rbPengobatan2.Checked)
                {
                    if (hdnhistoryroutine.Value != "")
                    {
                        List<PatientRoutineMedication> tempcurrmed = new JavaScriptSerializer().Deserialize<List<PatientRoutineMedication>>(hdnhistoryroutine.Value);
                        soapmodel.patient_medication = tempcurrmed;
                    }
                    else
                        soapmodel.patient_medication.Clear();
                }
                else if (rbPengobatan1.Checked)
                {
                    if (hdnhistoryroutine.Value != "")
                    {
                        List<PatientRoutineMedication> tempcurrmed = new JavaScriptSerializer().Deserialize<List<PatientRoutineMedication>>(hdnhistoryroutine.Value);
                        soapmodel.patient_medication = tempcurrmed;
                    }
                    else
                        soapmodel.patient_medication.Clear();
                    
                }
                else
                {
                    soapmodel.patient_medication.Clear();
                }

                if (soapmodel.patient_medication == null)
                    soapmodel.patient_medication = new List<PatientRoutineMedication>();

                if (rbdrug2.Checked)
                {
                    List<PatientAllergy> tempdrugsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistorydrugallergies.Value);
                    soapmodel.patient_allergy = tempdrugsallergy;
                    if (rbfood2.Checked)
                    {
                        soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(2));
                    }
                    if (rbother2.Checked)
                    {
                        soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(7));
                    }
                }
                else if (rbfood2.Checked)
                {
                    List<PatientAllergy> tempfoodsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryfoodallergies.Value);
                    soapmodel.patient_allergy = tempfoodsallergy;
                    if (rbother2.Checked)
                    {
                        soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(7));
                    }
                }
                else if (rbother2.Checked)
                {
                    List<PatientAllergy> tempothersallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryotherallergies.Value);
                    soapmodel.patient_allergy = tempothersallergy;
                }
                else
                {
                    soapmodel.patient_allergy.Clear();
                }

                if (soapmodel.patient_allergy == null)
                    soapmodel.patient_allergy = new List<PatientAllergy>();

                List<PatientDiseaseHistory> templistdisease = new List<PatientDiseaseHistory>();
                if (rbpribadi2.Checked)
                {
                    List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);
                    templistdisease = tempdisease;
                }
                else if (rbpribadi1.Checked)
                {
                    List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);
                    templistdisease = tempdisease;  
                }
                else
                {
                    soapmodel.patient_disease.Clear();
                }

                if (rbkeluarga2.Checked)
                {
                    List<PatientDiseaseHistory> tempdiseasefam = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnFamilyDiseaseHistory.Value);
                    templistdisease.AddRange(tempdiseasefam);
                }
                else if (rbkeluarga1.Checked)
                {
                    List<PatientDiseaseHistory> tempdiseasefam = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnFamilyDiseaseHistory.Value);
                    templistdisease.AddRange(tempdiseasefam);
                }

                soapmodel.patient_disease = templistdisease;
                if (soapmodel.patient_disease == null)
                    soapmodel.patient_disease = new List<PatientDiseaseHistory>();

                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("ae3ca8c2-eab0-41b6-9e3e-3ecb8071a9d0")).value = txtbloodlow.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e5efd220-b68e-4652-ad03-d56ef29fcebb")).value = txtbloodhigh.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("78cbb61f-4a11-470a-b770-1a44eb04357f")).value = txtpulserate.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e6ae2ea9-b321-4756-bf96-2dc232e4a7ba")).value = txtrespiratory.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e903246c-df95-4fe0-96d2-cf90c036d3d7")).value = txtspo.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("2eeca752-a2ea-4426-b3cf-c1ea3833bf30")).value = txttemperature.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("52ce9350-bfb2-4072-8893-d0c6cf8b3634")).value = txtweight.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("2a8dbddb-edfe-4704-876e-5a2d735bb541")).value = txtheight.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D")).remarks = "Abnormal";
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D")).value = txtOthers.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("A8E0013B-0443-4E7A-B670-4DB9362B40E4")).value = txtlingkarkepala.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("19516BE9-9D54-4E30-8E90-0A8B41F5BA66")).value = txtbmi.Text;

                if (lbleyetotal.Text == "_")
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("a9c5cd3c-1e02-4db2-a047-2f1983d1315b")).value = "";
                else
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("a9c5cd3c-1e02-4db2-a047-2f1983d1315b")).value = hdnEye.Value;

                if (lblmovetotal.Text == "_")
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("89b583a5-3003-43ab-9693-60ea6181c8d5")).value = "";
                else
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("89b583a5-3003-43ab-9693-60ea6181c8d5")).value = hdnMove.Value;

                if (lblverbaltotal.Text == "_")
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("fe4cf48c-17a6-4720-ad23-186517dd9c85")).value = "";
                else
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("fe4cf48c-17a6-4720-ad23-186517dd9c85")).value = hdnVerbal.Value;

                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176")).value = hdnpainscale.Value;

                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = hdnMentalStatus.Value;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).remarks = hdnMentalStatusremark.Value;

                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = hdnConsciousness.Value;
                
                foreach (var assessment in ((List<Assessment>)soapmodel.assessment).Where(x => x.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")))
                {
                    if (assessment.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47"))
                    {//"Primary"
                        assessment.remarks = txtPrimary.Text;
                    }
                }
                foreach (var planning in ((List<Planning>)soapmodel.planning).Where(x => x.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6")))
                {
                    if (planning.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
                    {
                        planning.remarks = txtPlanning.Text;
                    }
                }
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")).remarks = txtHasilTindakan.Text;
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")).value = soapmodel.patient_id.ToString();

                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("7198D4D5-7670-4853-9AFA-CD7C606C5C27")).value = "";
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("7198D4D5-7670-4853-9AFA-CD7C606C5C27")).remarks = hdnSchedule_travel.Value;
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3893BF09-82A2-449A-B5F0-D3A2BF9905C1")).value = hdnCondition_travel.Value;
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3893BF09-82A2-449A-B5F0-D3A2BF9905C1")).remarks = hdncondition_date.Value;
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("A4D139D7-67E2-44BC-A5E7-8BB17DD3F21E")).value = hdnSeating_type.Value;
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("A4D139D7-67E2-44BC-A5E7-8BB17DD3F21E")).remarks = "";
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("80A8E43A-53BF-4A32-B992-C9F32C7AA458")).value = hdnEscort_type.Value;
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("80A8E43A-53BF-4A32-B992-C9F32C7AA458")).remarks = hdnescort_ddl.Value;
                ((List<Planning>)soapmodel.planning).RemoveAll(x => x.soap_mapping_id == Guid.Parse("F3378407-B8A6-45FF-A27B-87F3CCF0F2B4"));
                if (hdnSpecial_Needs.Value != "")
                {
                    List<Planning> listSpecialNeeds = new List<Planning>();
                    var dataspecialneeds = hdnSpecial_Needs.Value.Split(',');
                    for (int i = 0; i < dataspecialneeds.Count(); i++)
                    {
                        Planning P = new Planning();
                        P.soap_mapping_id = Guid.Parse("F3378407-B8A6-45FF-A27B-87F3CCF0F2B4");
                        P.soap_mapping_name = "SPECIAL_NEEDS";
                        P.planning_id = Guid.Empty;
                        P.value = dataspecialneeds[i].ToString();
                        P.remarks = "";
                        listSpecialNeeds.Add(P);
                    }
                    soapmodel.planning.AddRange(listSpecialNeeds);
                }
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("30F9892A-22C7-4C8B-8005-BE08E105F05F")).value = "";
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("30F9892A-22C7-4C8B-8005-BE08E105F05F")).remarks = txtTravelRecommendation.Text.ToString();

                soapmodel = StdPlanning.GetPlanningValues(soapmodel);
                soapmodel = StdImunisasi.GetImunisasiValues(soapmodel);

                string templateid = Request.QueryString["PageSoapId"];
                if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
                {

                }
                else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
                {
                    soapmodel = StdTriage.GetTriageValues(soapmodel);
                    Emergency_GetValues(soapmodel, true);
                }
                else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
                {
                    //soapmodel = StdObgyn.GetObgynValues(soapmodel);
                    soapmodel = getObgynData(soapmodel);
                }
                else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
                {
                    soapmodel = getPediatricData(soapmodel);
                }

                soapmodel.save_mode = 1;

                List<ReferalData> referalDatas = (List<ReferalData>)Session[Helper.SessionSOAPReferral + hfguidadditional.Value];
                if (referalDatas != null)
                {
                    soapmodel.referal = referalDatas;
                }
                else
                {
                    soapmodel.referal = new List<ReferalData>();
                }

                //---------------------------------------------------------------------------------

                long itemid = long.Parse(hfitemid.Value);
                double ConsultationAmount = double.Parse(hfconsfee.Value);
                double discountamount = double.Parse(hfdiscamount.Value);
                string appointmentid = Request.QueryString["AppointmentId"];
                string username = Helper.GetLoginUser(this);

                Dictionary<string, string> logParam = new Dictionary<string, string>
                {
                    { "Item_ID", itemid == null ? "-" : itemid.ToString() },
                    { "Consultation_Amount", ConsultationAmount == null ? "-" : ConsultationAmount.ToString() },
                    { "Discount_Amount", discountamount == null ? "-" : discountamount.ToString() },
                    { "Procedure_Notes", txtnotes.Text },
                    { "PageSoap_ID", hfPageSoapId.Value },
                    { "Item_Name", hfitemname.Value },
                    { "Appointment_ID", appointmentid == null ? "-" : appointmentid.ToString() },
                    { "Username", username },
                };
                //log.debug(logconfig.LogStart("MappingforSubmitdataSOAP", logParam, LogConfig.JsonToString(soapmodel)));
                var getMap = MappingforSubmitdataSOAP(soapmodel, itemid, ConsultationAmount, discountamount, txtnotes.Text, hfPageSoapId.Value, hfitemname.Value);
                var JsongetMap = (JObject)JsonConvert.DeserializeObject<dynamic>(getMap.Result);
                var Status = JsongetMap.Property("status").Value.ToString();
                var Message = JsongetMap.Property("message").Value.ToString();
                //log.debug(logconfig.LogEnd("MappingforSubmitdataSOAP", Status, Message));

                var data = JsongetMap.Property("data").Value.ToString();

                if (Status == "Fail")
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Failed to Save SOAP');", true);
                    fail_box("Failed to Submit SOAP", Message);
                    ActionBackUpToSession();
                }
                else
                {
                    //Submit Health Record
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Submit_HR", "SubmitIframe_HI();", true);

                    //reset the session backup
                    Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]] = null;

                    if (data != "SUCCESS")
                    {
                        if (data.Contains("MODIFIED"))
                        {
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('SOAP already modified by other user. <br /> Please refresh this page for update data. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "$('#modalnotifalreadymodif').modal('show');", true);
                        }
                        else
                        {
                            List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                            markerlist.Find(x => x.key == "TAKENmarker").value = data;
                            Session[Helper.SESSIONmarker] = markerlist;
                            //Helper.TAKENmarker = data;

                            if (hf_flagrujukan_aido.Value == "aido")
                            {
                                string ip = GetLocalIPAddress();
                                string docid = Helper.GetDoctorID(this);
                                string param = "previewRujukan('" + ip + "','" + Helper.organizationId + "','" + hfPatientId.Value + "','" + hfAdmissionId.Value + "','" + hfEncounterId.Value + "','" + docid + "');";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rujukan", param, true);
                                hf_flagrujukan_aido.Value = "";
                            }

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refresh", "window.location.reload();", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "taken();", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "$('#modalcompdetail').modal('hide');", true);
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Prescription already taken by Pharmacy');", true);
                        }
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "successsubmit();", true);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('Submit and Sign successful');", true);
                        //Response.Redirect(Request.RawUrl);

                        List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                        markerlist.Find(x => x.key == "SUBMITSOAPmarker").value = "marked";
                        Session[Helper.SESSIONmarker] = markerlist;

                        if (hf_flagrujukan_aido.Value == "aido")
                        {
                            string ip = GetLocalIPAddress();
                            string docid = Helper.GetDoctorID(this);
                            string param = "previewRujukan('" + ip + "','" + Helper.organizationId + "','" + hfPatientId.Value + "','" + hfAdmissionId.Value + "','" + hfEncounterId.Value + "','" + docid + "');";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rujukan", param, true);
                            hf_flagrujukan_aido.Value = "";
                        }

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refresh", "window.location.reload();", true);
                        
                    }

                }

                //Response.Redirect(Request.RawUrl);
            }

            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hidecopy", "hidecopy();", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Submit_SOAP_disable", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            //SOAP x = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
            var x = MappingforGetdataSOAPSession();
            string jsonfailed = new JavaScriptSerializer().Serialize(x);

            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Submit_SOAP_disable", StartTime, "Error", MyUser.GetUsername(), "", jsonfailed, ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString() + jsonfailed), ex);
            //log.Error(LogLibrary.Error("Submit_SOAP_disable Failed", Helper.GetLoginUser(this), ex.Message + jsonfailed));
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('Failed to Submit SOAP');", true);
            error_box("Failed to Submit SOAP",ex);
        }   
    }

    public SOAPObgyn getObgynData(SOAPObgyn soapmodel)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        if (txtG.Text != "")
        {
            string flagempty = "kosong";
            foreach (PregnancyData x in soapmodel.pregnancy_data)
            {
                if (x.pregnancy_data_type.ToUpper() == "G")
                {
                    x.value = txtG.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PregnancyData data = new PregnancyData();
                data.pregnancy_data_id = Guid.Empty;
                data.pregnancy_data_type = "G";
                data.value = txtG.Text;
                data.remarks = "";
                data.status = "";
                soapmodel.pregnancy_data.Add(data);
            }
        }

        if (txtP.Text != "")
        {
            string flagempty = "kosong";
            foreach (PregnancyData x in soapmodel.pregnancy_data)
            {
                if (x.pregnancy_data_type.ToUpper() == "P")
                {
                    x.value = txtP.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PregnancyData data = new PregnancyData();
                data.pregnancy_data_id = Guid.Empty;
                data.pregnancy_data_type = "P";
                data.value = txtP.Text;
                data.remarks = "";
                data.status = "";
                soapmodel.pregnancy_data.Add(data);
            }
        }

        if (txtA.Text != "")
        {
            string flagempty = "kosong";
            foreach (PregnancyData x in soapmodel.pregnancy_data)
            {
                if (x.pregnancy_data_type.ToUpper() == "A")
                {
                    x.value = txtA.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PregnancyData data = new PregnancyData();
                data.pregnancy_data_id = Guid.Empty;
                data.pregnancy_data_type = "A";
                data.value = txtA.Text;
                data.remarks = "";
                data.status = "";
                soapmodel.pregnancy_data.Add(data);
            }
        }

        if (txtHPHT.Text != "")
        {
            string flagempty = "kosong";
            foreach (PregnancyData x in soapmodel.pregnancy_data)
            {
                if (x.pregnancy_data_type.ToUpper() == "HPHT")
                {
                    x.value = txtHPHT.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PregnancyData data = new PregnancyData();
                data.pregnancy_data_id = Guid.Empty;
                data.pregnancy_data_type = "HPHT";
                data.value = txtHPHT.Text;
                data.remarks = "";
                data.status = "";
                soapmodel.pregnancy_data.Add(data);
            }
        }

        if (txtTHL.Text != "")
        {
            string flagempty = "kosong";
            foreach (PregnancyData x in soapmodel.pregnancy_data)
            {
                if (x.pregnancy_data_type.ToUpper() == "THL")
                {
                    x.value = txtTHL.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PregnancyData data = new PregnancyData();
                data.pregnancy_data_id = Guid.Empty;
                data.pregnancy_data_type = "THL";
                data.value = txtTHL.Text;
                data.remarks = "";
                data.status = "";
                soapmodel.pregnancy_data.Add(data);
            }
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "getObgynData", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());

        return soapmodel;
    }

    public SOAPPediatric getPediatricData(SOAPPediatric soapmodel)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        if (txtTengkurap.Text != "")
        {
            string flagempty = "kosong";
            foreach (PediatricData x in soapmodel.pediatric_data)
            {
                if (x.pediatric_data_type.ToUpper() == "TENGKURAP")
                {
                    x.value = txtTengkurap.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PediatricData data = new PediatricData();
                data.pediatric_data_id = Guid.Empty;
                data.pediatric_data_type = "TENGKURAP";
                data.value = txtTengkurap.Text;
                data.remarks = "";
                soapmodel.pediatric_data.Add(data);
            }
        }

        if (txtDuduk.Text != "")
        {
            string flagempty = "kosong";
            foreach (PediatricData x in soapmodel.pediatric_data)
            {
                if (x.pediatric_data_type.ToUpper() == "DUDUK")
                {
                    x.value = txtDuduk.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PediatricData data = new PediatricData();
                data.pediatric_data_id = Guid.Empty;
                data.pediatric_data_type = "DUDUK";
                data.value = txtDuduk.Text;
                data.remarks = "";
                soapmodel.pediatric_data.Add(data);
            }
        }

        if (txtMerangkak.Text != "")
        {
            string flagempty = "kosong";
            foreach (PediatricData x in soapmodel.pediatric_data)
            {
                if (x.pediatric_data_type.ToUpper() == "MERANGKAK")
                {
                    x.value = txtMerangkak.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PediatricData data = new PediatricData();
                data.pediatric_data_id = Guid.Empty;
                data.pediatric_data_type = "MERANGKAK";
                data.value = txtMerangkak.Text;
                data.remarks = "";
                soapmodel.pediatric_data.Add(data);
            }
        }

        if (txtBerdiri.Text != "")
        {
            string flagempty = "kosong";
            foreach (PediatricData x in soapmodel.pediatric_data)
            {
                if (x.pediatric_data_type.ToUpper() == "BERDIRI")
                {
                    x.value = txtBerdiri.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PediatricData data = new PediatricData();
                data.pediatric_data_id = Guid.Empty;
                data.pediatric_data_type = "BERDIRI";
                data.value = txtBerdiri.Text;
                data.remarks = "";
                soapmodel.pediatric_data.Add(data);
            }
        }

        if (txtBerjalan.Text != "")
        {
            string flagempty = "kosong";
            foreach (PediatricData x in soapmodel.pediatric_data)
            {
                if (x.pediatric_data_type.ToUpper() == "BERJALAN")
                {
                    x.value = txtBerjalan.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PediatricData data = new PediatricData();
                data.pediatric_data_id = Guid.Empty;
                data.pediatric_data_type = "BERJALAN";
                data.value = txtBerjalan.Text;
                data.remarks = "";
                soapmodel.pediatric_data.Add(data);
            }
        }

        if (txtBerbicara.Text != "")
        {
            string flagempty = "kosong";
            foreach (PediatricData x in soapmodel.pediatric_data)
            {
                if (x.pediatric_data_type.ToUpper() == "BERBICARA")
                {
                    x.value = txtBerbicara.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PediatricData data = new PediatricData();
                data.pediatric_data_id = Guid.Empty;
                data.pediatric_data_type = "BERBICARA";
                data.value = txtBerbicara.Text;
                data.remarks = "";
                soapmodel.pediatric_data.Add(data);
            }
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "getPediatricData", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());

        return soapmodel;
    }

    public void ActionSubmit()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());
        //SOAP soapmodel = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];

        var soapmodel = MappingforGetdataSOAPSession();

        ((List<Subjective>)soapmodel.subjective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("f815de0e-e57a-4250-a2be-bdd27c1876ac"));
        if (hdnTindakan.Value != "")
        {
            List<Subjective> listtindakan = new JavaScriptSerializer().Deserialize<List<Subjective>>(hdnTindakan.Value);
            soapmodel.subjective.AddRange(listtindakan);
        }

        ((List<Subjective>)soapmodel.subjective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("0cbb6a1d-392b-4fed-8e8a-696bea3cbc32"));
        if (hdnDeleteReason.Value != "")
        {
            List<Subjective> listdeletereason = new JavaScriptSerializer().Deserialize<List<Subjective>>(hdnDeleteReason.Value);
            soapmodel.subjective.AddRange(listdeletereason);
        }

        ((List<Objective>)soapmodel.objective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c"));
        if (hdnFallRisk.Value != "")
        {
            List<Objective> listobjfallrisk = new JavaScriptSerializer().Deserialize<List<Objective>>(hdnFallRisk.Value);
            soapmodel.objective.AddRange(listobjfallrisk);
        }
        ((List<Objective>)soapmodel.objective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("B0C9D8E0-7533-43CD-BFCF-E8C0AC1D4B7E"));
        if (hdnFallRiskHandling.Value != "")
        {
            List<Objective> listobjfallriskHandling = new JavaScriptSerializer().Deserialize<List<Objective>>(hdnFallRiskHandling.Value);
            soapmodel.objective.AddRange(listobjfallriskHandling);
        }
        //if (fall1.Checked)
        //{
        //    Objective tempfallrisk = new Objective();
        //    tempfallrisk.objective_id = Guid.Empty;
        //    tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
        //    tempfallrisk.soap_mapping_name = "FALL RISK";
        //    tempfallrisk.value = "undergo sedation";
        //    tempfallrisk.remarks = "";
        //    soapmodel.objective.Add(tempfallrisk);
        //}
        //if (fall2.Checked)
        //{
        //    Objective tempfallrisk = new Objective();
        //    tempfallrisk.objective_id = Guid.Empty;
        //    tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
        //    tempfallrisk.soap_mapping_name = "FALL RISK";
        //    tempfallrisk.value = "physical limitation";
        //    tempfallrisk.remarks = "";
        //    soapmodel.objective.Add(tempfallrisk);
        //}
        //if (fall3.Checked)
        //{
        //    Objective tempfallrisk = new Objective();
        //    tempfallrisk.objective_id = Guid.Empty;
        //    tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
        //    tempfallrisk.soap_mapping_name = "FALL RISK";
        //    tempfallrisk.value = "motion aids";
        //    tempfallrisk.remarks = "";
        //    soapmodel.objective.Add(tempfallrisk);
        //}
        //if (fall4.Checked)
        //{
        //    Objective tempfallrisk = new Objective();
        //    tempfallrisk.objective_id = Guid.Empty;
        //    tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
        //    tempfallrisk.soap_mapping_name = "FALL RISK";
        //    tempfallrisk.value = "Disorder";
        //    tempfallrisk.remarks = "";
        //    soapmodel.objective.Add(tempfallrisk);
        //}
        //if (fall5.Checked)
        //{
        //    Objective tempfallrisk = new Objective();
        //    tempfallrisk.objective_id = Guid.Empty;
        //    tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
        //    tempfallrisk.soap_mapping_name = "FALL RISK";
        //    tempfallrisk.value = "fasting";
        //    tempfallrisk.remarks = "";
        //    soapmodel.objective.Add(tempfallrisk);
        //}

        //soapmodel = StdSubjective.GetSubjectiveValues(soapmodel);
        //soapmodel = StdGeneralCheckup.GetObjectiveValues(soapmodel);

        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf")).remarks = Complaint.Text;
        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0")).remarks = Anamnesis.Text;

        string radhamil = "";
        if (Radiohamilno.Checked == true)
        {
            radhamil = "false";
        }
        else if (Radiohamilyes.Checked == true)
        {
            radhamil = "true";
        }
        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")).value = radhamil;
        string radsusu = "";
        if (Radiosusuno.Checked == true)
        {
            radsusu = "false";
        }
        else if (Radiosusuyes.Checked == true)
        {
            radsusu = "true";
        }
        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")).value = radsusu;

        //if (rbkunjungan2.Checked)
        //    soapmodel.subjective.Find(y => y.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade")).remarks = txtEndemic.Text;
        //else if (rbkunjungan1.Checked)
        //    soapmodel.subjective.Find(y => y.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade")).remarks = "";
        ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade")).remarks = hdnEndemicArea.Value;

        if (rbnutrisi2.Checked)
            ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).remarks = hdnNutrition.Value;
        else if (rbnutrisi1.Checked)
            ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).remarks = "";

        if (rbpuasa2.Checked)
            ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).remarks = hdnFasting.Value;
        else if (rbpuasa1.Checked)
            ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).remarks = "";

        if (rbOperas2.Checked)
        {
            List<PatientSurgery> tempsurgery = new JavaScriptSerializer().Deserialize<List<PatientSurgery>>(hdnhistorysurgery.Value);
            soapmodel.patient_surgery = tempsurgery;
            //soapmodel.patient_surgery = GetRowList_PatientSurgery();
        }
        else if (rbOperasi.Checked)
        {
            List<PatientSurgery> tempsurgery = new JavaScriptSerializer().Deserialize<List<PatientSurgery>>(hdnhistorysurgery.Value);
            soapmodel.patient_surgery = tempsurgery;
            
        }
        else
        {
            soapmodel.patient_surgery.Clear();
        }

        if (soapmodel.patient_surgery == null)
            soapmodel.patient_surgery = new List<PatientSurgery>();

        if (rbProcOut2.Checked)
        {
            List<PatientProcedureHistory> tempprocout = new JavaScriptSerializer().Deserialize<List<PatientProcedureHistory>>(hdnProcedureOutside.Value);
            soapmodel.patient_procedure = tempprocout;
        }
        else if (rbProcOut1.Checked)
        {
            soapmodel.patient_procedure.Clear();
        }

        if (soapmodel.patient_procedure == null)
            soapmodel.patient_procedure = new List<PatientProcedureHistory>();


        List<PatientSpecialNotification> datareminder = GetRowList_ReminderNotes();
        hdnremindernotes.Value = new JavaScriptSerializer().Serialize(datareminder);
        List<PatientSpecialNotification> tempreminder = new JavaScriptSerializer().Deserialize<List<PatientSpecialNotification>>(hdnremindernotes.Value);
        soapmodel.patient_notification = tempreminder;

        if (soapmodel.patient_notification == null)
            soapmodel.patient_notification = new List<PatientSpecialNotification>();

        if (hfenableroutine.Value == "1")
        {
            if (hdnhistoryroutine.Value != "")
            {
                List<PatientRoutineMedication> tempcurrmed = new JavaScriptSerializer().Deserialize<List<PatientRoutineMedication>>(hdnhistoryroutine.Value);
                soapmodel.patient_medication = tempcurrmed;
            }
            else
                soapmodel.patient_medication.Clear();

        }
        else if (hfenableroutine.Value == "0")
        {
            if (hdnhistoryroutine.Value != "")
            {
                List<PatientRoutineMedication> tempcurrmed = new JavaScriptSerializer().Deserialize<List<PatientRoutineMedication>>(hdnhistoryroutine.Value);
                soapmodel.patient_medication = tempcurrmed;
            }
            else
                soapmodel.patient_medication.Clear();
            
        }
        else
        {
            soapmodel.patient_medication.Clear();
        }

        if (soapmodel.patient_medication == null)
            soapmodel.patient_medication = new List<PatientRoutineMedication>();

        List<PatientRoutineMedication> pengobatan_nodata = new List<PatientRoutineMedication>();
        PatientRoutineMedication row_pengobatan = new PatientRoutineMedication();
        row_pengobatan.patient_routine_medication_id = Guid.Empty;
        row_pengobatan.medication = "Tidak Ada Pengobatan";
        row_pengobatan.routine_sales_item_code = "";
        row_pengobatan.routine_sales_item_id = 0;
        row_pengobatan.is_delete = 0;

        if (hfenableroutine.Value == "0")
        {
            pengobatan_nodata.Add(row_pengobatan);
            soapmodel.patient_medication = pengobatan_nodata;
        }


        if (rbdrug2.Checked)
        {
            List<PatientAllergy> tempdrugsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistorydrugallergies.Value);
            soapmodel.patient_allergy = tempdrugsallergy;
            if (rbfood2.Checked)
            {
                soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(2));
            }
            if (rbother2.Checked)
            {
                soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(7));
            }
        }
        else if (rbfood2.Checked)
        {
            List<PatientAllergy> tempfoodsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryfoodallergies.Value);
            soapmodel.patient_allergy = tempfoodsallergy;
            if (rbother2.Checked)
            {
                soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(7));
            }
        }
        else if (rbother2.Checked)
        {
            List<PatientAllergy> tempothersallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryotherallergies.Value);
            soapmodel.patient_allergy = tempothersallergy;
        }
        else
        {
            soapmodel.patient_allergy.Clear();
        }

        if (soapmodel.patient_allergy == null)
            soapmodel.patient_allergy = new List<PatientAllergy>();

        //initial no data
        List<PatientAllergy> drug_noalergi = new List<PatientAllergy>();
        List<PatientAllergy> food_noalergi = new List<PatientAllergy>();
        List<PatientAllergy> other_noalergi = new List<PatientAllergy>();
        PatientAllergy rowdrug = new PatientAllergy();
        rowdrug.patient_allergy_id = Guid.Empty;
        rowdrug.allergy_type = 1;
        rowdrug.allergy = "Tidak Ada Alergi";
        rowdrug.allergy_reaction = "Tidak Ada Reaksi";
        rowdrug.is_delete = 0;
        PatientAllergy rowfood = new PatientAllergy();
        rowfood.patient_allergy_id = Guid.Empty;
        rowfood.allergy_type = 2;
        rowfood.allergy = "Tidak Ada Alergi";
        rowfood.allergy_reaction = "Tidak Ada Reaksi";
        rowfood.is_delete = 0;
        PatientAllergy rowother = new PatientAllergy();
        rowother.patient_allergy_id = Guid.Empty;
        rowother.allergy_type = 7;
        rowother.allergy = "Tidak Ada Alergi";
        rowother.allergy_reaction = "Tidak Ada Reaksi";
        rowother.is_delete = 0;
        //end initial no data

        //set if no allergy
        if (rbdrug1.Checked)
        {
            drug_noalergi.Add(rowdrug);
            soapmodel.patient_allergy.AddRange(drug_noalergi);
        }
        if (rbfood1.Checked)
        {
            food_noalergi.Add(rowfood);
            soapmodel.patient_allergy.AddRange(food_noalergi);
        }
        if (rbother1.Checked)
        {
            other_noalergi.Add(rowother);
            soapmodel.patient_allergy.AddRange(other_noalergi);
        }

        List<PatientDiseaseHistory> templistdisease = new List<PatientDiseaseHistory>();
        if (rbpribadi2.Checked)
        {
            if (hdnDiseaseHistory.Value != "")
            {
                List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);
                templistdisease = tempdisease;
            }

            //if (chkdisease1.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "Hypertension";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 1;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}
            //if (chkdisease2.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "Stroke";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 1;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}
            //if (chkdisease3.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "TBC";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 1;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}
            //if (chkdisease4.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "Kidney";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 1;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}
            //if (chkdisease5.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "Convulsive";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 1;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}
            //if (chkdisease6.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "Heart";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 1;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}
            //if (chkdisease7.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "Diabetes";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 1;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}
            //if (chkdisease8.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "Asthma";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 1;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}
            //if (chkdisease9.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "Hepatitis";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 1;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}

            //PatientDiseaseHistory temp_patienthistoryremark = new PatientDiseaseHistory();
            //temp_patienthistoryremark.patient_disease_history_id = Guid.Empty;
            //temp_patienthistoryremark.value = "Lain-lain";
            //temp_patienthistoryremark.remarks = txtDisease.Text;
            //temp_patienthistoryremark.disease_history_type = 1;
            //temp_patienthistoryremark.is_delete = 0;
            //templistdisease.Add(temp_patienthistoryremark);

        }
        else if (rbpribadi1.Checked)
        {
            if (hdnDiseaseHistory.Value != "")
            {
                List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);
                templistdisease = tempdisease;
            }
        }
        else
        {
            soapmodel.patient_disease.Clear();
        }

        if (rbkeluarga2.Checked)
        {
            if (hdnFamilyDiseaseHistory.Value != "")
            {
                List<PatientDiseaseHistory> tempdiseasefam = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnFamilyDiseaseHistory.Value);
                templistdisease.AddRange(tempdiseasefam);
            }

            //if (chkdiseasefam1.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "Heart";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 2;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}
            //if (chkdiseasefam2.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "Diabetes";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 2;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}
            //if (chkdiseasefam3.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "Asthma";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 2;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}
            //if (chkdiseasefam4.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "Hypertension";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 2;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}
            //if (chkdiseasefam5.Checked)
            //{
            //    PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            //    temp_patienthistory.patient_disease_history_id = Guid.Empty;
            //    temp_patienthistory.value = "Cancer";
            //    temp_patienthistory.remarks = "";
            //    temp_patienthistory.disease_history_type = 2;
            //    temp_patienthistory.is_delete = 0;
            //    templistdisease.Add(temp_patienthistory);
            //}
            //PatientDiseaseHistory temp_patienthistoryremark = new PatientDiseaseHistory();
            //temp_patienthistoryremark.patient_disease_history_id = Guid.Empty;
            //temp_patienthistoryremark.value = "Lain-lain";
            //temp_patienthistoryremark.remarks = txtDiseaseFam.Text;
            //temp_patienthistoryremark.disease_history_type = 2;
            //temp_patienthistoryremark.is_delete = 0;
            //templistdisease.Add(temp_patienthistoryremark);
        }
        else if(rbkeluarga1.Checked)
        {
            if (hdnFamilyDiseaseHistory.Value != "")
            {
                List<PatientDiseaseHistory> tempdiseasefam = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnFamilyDiseaseHistory.Value);
                templistdisease.AddRange(tempdiseasefam);
            }
        }

        soapmodel.patient_disease = templistdisease;

        if (soapmodel.patient_disease == null)
            soapmodel.patient_disease = new List<PatientDiseaseHistory>();

        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("ae3ca8c2-eab0-41b6-9e3e-3ecb8071a9d0")).value = txtbloodlow.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e5efd220-b68e-4652-ad03-d56ef29fcebb")).value = txtbloodhigh.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("78cbb61f-4a11-470a-b770-1a44eb04357f")).value = txtpulserate.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e6ae2ea9-b321-4756-bf96-2dc232e4a7ba")).value = txtrespiratory.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e903246c-df95-4fe0-96d2-cf90c036d3d7")).value = txtspo.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("2eeca752-a2ea-4426-b3cf-c1ea3833bf30")).value = txttemperature.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("52ce9350-bfb2-4072-8893-d0c6cf8b3634")).value = txtweight.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("2a8dbddb-edfe-4704-876e-5a2d735bb541")).value = txtheight.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D")).remarks =  txtOthers.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D")).value = "Abnormal";
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("A8E0013B-0443-4E7A-B670-4DB9362B40E4")).value = txtlingkarkepala.Text;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("19516BE9-9D54-4E30-8E90-0A8B41F5BA66")).value = txtbmi.Text;

        if (lbleyetotal.Text == "_")
            ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("a9c5cd3c-1e02-4db2-a047-2f1983d1315b")).value = "";
        else
            ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("a9c5cd3c-1e02-4db2-a047-2f1983d1315b")).value = hdnEye.Value;

        if (lblmovetotal.Text == "_")
            ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("89b583a5-3003-43ab-9693-60ea6181c8d5")).value = "";
        else
            ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("89b583a5-3003-43ab-9693-60ea6181c8d5")).value = hdnMove.Value;

        if (lblverbaltotal.Text == "_")
            ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("fe4cf48c-17a6-4720-ad23-186517dd9c85")).value = "";
        else
            ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("fe4cf48c-17a6-4720-ad23-186517dd9c85")).value = hdnVerbal.Value;

        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176")).value = hdnpainscale.Value;

        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = hdnMentalStatus.Value;
        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).remarks = hdnMentalStatusremark.Value;

        ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = hdnConsciousness.Value;
        //if (mental1.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Good Orientation";
        //else if (mental2.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Disorientated";
        //else if (mental3.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Cooperative";
        //else if (mental4.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Non Cooperative";

        //if (consciousness1.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Compos mentis";
        //else if (consciousness2.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Somnolent";
        //else if (consciousness3.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Stupor";
        //else if (consciousness4.Checked)
        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Coma";

        foreach (var assessment in ((List<Assessment>)soapmodel.assessment).Where(x => x.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")))
        {
            if (assessment.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47"))
            {//"Primary"
                assessment.remarks = txtPrimary.Text;
            }
        }
        foreach (var planning in ((List<Planning>)soapmodel.planning).Where(x => x.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6")))
        {
            if (planning.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
            {
                planning.remarks = txtPlanning.Text;
            }
        }

        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")).remarks = txtHasilTindakan.Text;
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")).value = soapmodel.patient_id.ToString();

        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("7198D4D5-7670-4853-9AFA-CD7C606C5C27")).value = "";
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("7198D4D5-7670-4853-9AFA-CD7C606C5C27")).remarks = hdnSchedule_travel.Value;
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3893BF09-82A2-449A-B5F0-D3A2BF9905C1")).value = hdnCondition_travel.Value;
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3893BF09-82A2-449A-B5F0-D3A2BF9905C1")).remarks = hdncondition_date.Value;
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("A4D139D7-67E2-44BC-A5E7-8BB17DD3F21E")).value = hdnSeating_type.Value;
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("A4D139D7-67E2-44BC-A5E7-8BB17DD3F21E")).remarks = "";
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("80A8E43A-53BF-4A32-B992-C9F32C7AA458")).value = hdnEscort_type.Value;
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("80A8E43A-53BF-4A32-B992-C9F32C7AA458")).remarks = hdnescort_ddl.Value;
        ((List<Planning>)soapmodel.planning).RemoveAll(x => x.soap_mapping_id == Guid.Parse("F3378407-B8A6-45FF-A27B-87F3CCF0F2B4"));
        if (hdnSpecial_Needs.Value != "")
        {
            List<Planning> listSpecialNeeds = new List<Planning>();
            var dataspecialneeds = hdnSpecial_Needs.Value.Split(',');
            for (int i = 0; i < dataspecialneeds.Count(); i++)
            {
                Planning P = new Planning();
                P.soap_mapping_id = Guid.Parse("F3378407-B8A6-45FF-A27B-87F3CCF0F2B4");
                P.soap_mapping_name = "SPECIAL_NEEDS";
                P.planning_id = Guid.Empty;
                P.value = dataspecialneeds[i].ToString();
                P.remarks = "";
                listSpecialNeeds.Add(P);
            }
            soapmodel.planning.AddRange(listSpecialNeeds);
        }
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("30F9892A-22C7-4C8B-8005-BE08E105F05F")).value = "";
        ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("30F9892A-22C7-4C8B-8005-BE08E105F05F")).remarks = txtTravelRecommendation.Text.ToString();

        soapmodel.procedure_notes = txtProcedure.Text;
        soapmodel.procedure_diagnosis = (List<ProcedureDiagnosis>)Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value];



        soapmodel = StdPlanning.GetPlanningValues(soapmodel);
        soapmodel = StdImunisasi.GetImunisasiValues(soapmodel);

        string templateid = Request.QueryString["PageSoapId"];
        if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {
            
        }
        else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
        {
            soapmodel = StdTriage.GetTriageValues(soapmodel);
            Emergency_GetValues(soapmodel, false);
        }
        else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
        {
            //soapmodel = StdObgyn.GetObgynValues(soapmodel);
            soapmodel = getObgynData(soapmodel);
        }
        else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
        {
            soapmodel = getPediatricData(soapmodel);
        }

        soapmodel.save_mode = 1;

        //-------------------------------------------------------------------------------------------

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

        string appointmentid = Request.QueryString["AppointmentId"];
        string username = Helper.GetLoginUser(this);

        Dictionary<string, string> logParam = new Dictionary<string, string>
        {
            { "Item_ID", itemid == null ? "-" : itemid.ToString() },
            { "Consultation_Amount", ConsultationAmount == null ? "-" : ConsultationAmount.ToString() },
            { "Discount_Amount", discountamount == null ? "-" : discountamount.ToString() },
            { "Procedure_Notes", "-" },
            { "PageSoap_ID", hfPageSoapId.Value },
            { "Item_Name", totalfeeformat[0] == null ? "-" : totalfeeformat[0].ToString() },
            { "Appointment_ID", appointmentid == null ? "-" : appointmentid.ToString() },
            { "Username", username },
        };
        //log.debug(logconfig.LogStart("MappingforSubmitdataSOAP", logParam, LogConfig.JsonToString(soapmodel)));
        var getMap = MappingforSubmitdataSOAP(soapmodel, itemid, ConsultationAmount, discountamount, "-", hfPageSoapId.Value, totalfeeformat[0].ToString());
        var JsongetMap = (JObject)JsonConvert.DeserializeObject<dynamic>(getMap.Result);
        var Status = JsongetMap.Property("status").Value.ToString();
        var Message = JsongetMap.Property("message").Value.ToString();
        //log.debug(logconfig.LogEnd("MappingforSubmitdataSOAP", Status, Message));
        //var data = JsongetMap.Property("data").Value.ToString();

        if (Status == "Fail")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Failed to Submit SOAP');", true);
            fail_box("Failed to Submit SOAP", Message);
            ActionBackUpToSession();
        }
        else
        {
            //Submit Health Record
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Submit_HR", "SubmitIframe_HI();", true);

            //reset the session backup
            Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]] = null;
            var data = JsongetMap.Property("data").Value.ToString();

            if (data != "SUCCESS")
            {
                if (data.Contains("MODIFIED"))
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('SOAP already modified by other user. <br /> Please refresh this page for update data. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "$('#modalnotifalreadymodif').modal('show');", true);
                }
                else
                {
                    List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                    markerlist.Find(x => x.key == "TAKENmarker").value = data;
                    Session[Helper.SESSIONmarker] = markerlist;
                    //Helper.TAKENmarker = data;

                    if (hf_flagrujukan_aido.Value == "aido")
                    {
                        string ip = GetLocalIPAddress();
                        string docid = Helper.GetDoctorID(this);
                        string param = "previewRujukan('" + ip + "','" + Helper.organizationId + "','" + hfPatientId.Value + "','" + hfAdmissionId.Value + "','" + hfEncounterId.Value + "','" + docid + "');";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rujukan", param, true);
                        hf_flagrujukan_aido.Value = "";
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refresh", "window.location.reload();", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "takennormal();", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Prescription already taken by Pharmacy');", true);
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalcompdetail", "successsubmitnormal();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('Submit and Sign successful');", true);
                //Response.Redirect(Request.RawUrl);

                List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                markerlist.Find(x => x.key == "SUBMITSOAPmarker").value = "marked";
                Session[Helper.SESSIONmarker] = markerlist;

                if (hf_flagrujukan_aido.Value == "aido")
                {
                    string ip = GetLocalIPAddress();
                    string docid = Helper.GetDoctorID(this);
                    string param = "previewRujukan('" + ip + "','" + Helper.organizationId + "','" + hfPatientId.Value + "','" + hfAdmissionId.Value + "','" + hfEncounterId.Value + "','" + docid + "');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rujukan", param, true);
                    hf_flagrujukan_aido.Value = "";
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refresh", "window.location.reload();", true);
                
            }
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "ActionSubmit", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
        //Response.Redirect(Request.RawUrl);
    }

    protected dynamic MappingforSubmitdataSOAP(dynamic soapmodel, long itemid, double ConsultationAmount, double discountamount, string ProcedureNotes, string pageid, string totalfeeformat)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        string appointmentid = Request.QueryString["AppointmentId"];
        string username = Helper.GetLoginUser(this);

        soapmodel.doctor_id = long.Parse(Helper.GetDoctorID(this));
        soapmodel.sq_data = new SingleQueueData();
        List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
        if (orgsetting.Find(y => y.setting_name.ToUpper() == "USE_SINGLEQUEUE".ToUpper()).setting_value == "TRUE")
        {
            //SOAP sq = new SOAP();
            soapmodel.sq_data.UrlDetailDrug = Helper.GenerateURLPrint("SHORTSOAP", Helper.organizationId.ToString(), hfAdmissionId.Value, hfEncounterId.Value, hfPatientId.Value, Helper.GetLoginUser(this), Helper.GetUserFullname(this), hfPageSoapId.Value);
            soapmodel.sq_data.UrlDetailLab = Helper.GenerateURLPrint("LAB", Helper.organizationId.ToString(), hfAdmissionId.Value, hfEncounterId.Value, hfPatientId.Value, Helper.GetLoginUser(this), Helper.GetUserFullname(this), hfPageSoapId.Value);
            soapmodel.sq_data.UrlDetailRad = Helper.GenerateURLPrint("RAD", Helper.organizationId.ToString(), hfAdmissionId.Value, hfEncounterId.Value, hfPatientId.Value, Helper.GetLoginUser(this), Helper.GetUserFullname(this), hfPageSoapId.Value);
            soapmodel.sq_data.UrlDetailMedicalResume = Helper.GenerateURLPrint("SHORTSOAP", Helper.organizationId.ToString(), hfAdmissionId.Value, hfEncounterId.Value, hfPatientId.Value, Helper.GetLoginUser(this), Helper.GetUserFullname(this), hfPageSoapId.Value);
            //soapmodel.sq_data.UrlDetailMedicalResume = Helper.GenerateURLPrint("LONGSOAP", Helper.organizationId.ToString(), hfAdmissionId.Value, hfEncounterId.Value, hfPatientId.Value, Helper.GetLoginUser(this), Helper.GetUserFullname(this), hfPageSoapId.Value);
            soapmodel.sq_data.HospitalId = Helper.GetHospitalID(this);
            soapmodel.sq_data.UserId = Helper.GetDoctorID(this);
            // soap procedure dan diagnosis
            soapmodel.sq_data.UrlDetailProcedureDiagnostic = Helper.GenerateURLPrint("ProcedureDiagnostic", Helper.organizationId.ToString(), hfAdmissionId.Value, hfEncounterId.Value, hfPatientId.Value, Helper.GetLoginUser(this), Helper.GetUserFullname(this), hfPageSoapId.Value);
            soapmodel.sq_data.UrlDetailShortSOAP = Helper.GenerateURLPrint("SHORTSOAP", Helper.organizationId.ToString(), hfAdmissionId.Value, hfEncounterId.Value, hfPatientId.Value, Helper.GetLoginUser(this), Helper.GetUserFullname(this), hfPageSoapId.Value);
        }

        List<ReferalData> referalDatas = (List<ReferalData>)Session[Helper.SessionSOAPReferral + hfguidadditional.Value];
        if (referalDatas != null)
        {
            soapmodel.referal = referalDatas;
        }
        else
        {
            soapmodel.referal = new List<ReferalData>();
        }

        //set fa sesuai templatenya
        string templateid = Request.QueryString["PageSoapId"];
        if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {
            var getMap = clsSOAP.SubmitSOAP(soapmodel, itemid, ConsultationAmount, discountamount, ProcedureNotes, pageid, totalfeeformat, appointmentid, username);
            return getMap;
        }
        else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
        {
            var getMap = clsSOAP.SubmitSOAP(soapmodel, itemid, ConsultationAmount, discountamount, ProcedureNotes, pageid, totalfeeformat, appointmentid, username);
            return getMap;
        }
        else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
        {
            var getMap = clsSPObgynSOAP.SubmitSOAPObgyn(soapmodel, itemid, ConsultationAmount, discountamount, ProcedureNotes, pageid, totalfeeformat, appointmentid, username);
            return getMap;
        }
        else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
        {
            var getMap = clsSPPediatricSOAP.SubmitSOAPPediatric(soapmodel, itemid, ConsultationAmount, discountamount, ProcedureNotes, pageid, totalfeeformat, appointmentid, username);
            return getMap;
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "MappingforSubmitdataSOAP", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
        return null;
    }

    protected dynamic MappingforSaveAsDraftSOAP(dynamic soapmodel)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        string appointmentid = Request.QueryString["AppointmentId"];
        string username = Helper.GetLoginUser(this);

        soapmodel.doctor_id = long.Parse(Helper.GetDoctorID(this));

        //set fa sesuai templatenya
        string templateid = Request.QueryString["PageSoapId"];
        if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {
            var getMap = clsSOAP.SaveAsDraftSOAP(soapmodel, hfPageSoapId.Value, appointmentid, username);
            return getMap;

        }
        else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
        {
            var getMap = clsSOAP.SaveAsDraftSOAP(soapmodel, hfPageSoapId.Value, appointmentid, username);
            return getMap;
        }
        else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
        {
            var getMap = clsSPObgynSOAP.SaveAsDraftSOAPObgyn(soapmodel, hfPageSoapId.Value, appointmentid, username);
            return getMap;
        }
        else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
        {
            var getMap = clsSPPediatricSOAP.SaveAsDraftSOAPPediatric(soapmodel, hfPageSoapId.Value, appointmentid, username);
            return getMap;
        }


        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "MappingforSaveAsDraftSOAP", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
        return null;
    }

    public void Submit_SOAP()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Find(y => y.setting_name.ToUpper() == "MANDATORY_SOAP".ToUpper()).setting_value == "TRUE")
            {
                if (Complaint.Text != "" && Anamnesis.Text != "" && txtPrimary.Text != "" && txtOthers.Text != "" && txtPlanning.Text != "")
                {
                    ActionSubmit();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('SOAP can not be empty');", true);
                }
            }
            else
            {
                ActionSubmit();
            }
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hidecopy", "hidecopy();", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Submit_SOAP", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            //SOAP x = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
            var x = MappingforGetdataSOAPSession();
            string jsonfailed = new JavaScriptSerializer().Serialize(x);

            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Submit_SOAP", StartTime, "Error", MyUser.GetUsername(), "", jsonfailed, ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString() + jsonfailed), ex);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('Failed to Submit SOAP');", true);
            error_box("Failed to Submit SOAP", ex);
        }

    }

    //protected void btnAutoSave_onClick(object sender, EventArgs e)
    //{
    //    //AutoSaveSOAP();
    //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "$('#modalconfirmleavepage').modal('show');", true);
    //}

    public void AutoSaveSOAP()
    {
        //try
        //{
        //    log.Info(LogLibrary.Logging("S", "AutoSaveSOAP", Helper.GetLoginUser(this), ""));

        //    SOAP soapmodel = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
        //    if (hfsavemode.Value == "0")
        //    {
        //        soapmodel.subjective.RemoveAll(x => x.soap_mapping_id == Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6"));

        //        if (hdnInfectiousDisease.Value != "")
        //        {
        //            List<Subjective> listscreening = new JavaScriptSerializer().Deserialize<List<Subjective>>(hdnInfectiousDisease.Value);
        //            soapmodel.subjective.AddRange(listscreening);
        //        }

        //        soapmodel.objective.RemoveAll(x => x.soap_mapping_id == Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c"));
        //        if (hdnFallRisk.Value != "")
        //        {
        //            List<Objective> listobjfallrisk = new JavaScriptSerializer().Deserialize<List<Objective>>(hdnFallRisk.Value);
        //            soapmodel.objective.AddRange(listobjfallrisk);
        //        }

        //        soapmodel.subjective.Find(y => y.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf")).remarks = Complaint.Text;
        //        soapmodel.subjective.Find(y => y.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0")).remarks = Anamnesis.Text;
        //        //soapmodel.subjective.Find(y => y.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")).value = chkpregnant.Checked.ToString();
        //        //soapmodel.subjective.Find(y => y.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")).value = chkbreastfeed.Checked.ToString();

        //        string radhamil = "";
        //        if (Radiohamilno.Checked == true)
        //        {
        //            radhamil = "false";
        //        }
        //        else if (Radiohamilyes.Checked == true)
        //        {
        //            radhamil = "true";
        //        }
        //        soapmodel.subjective.Find(y => y.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")).value = radhamil;
        //        string radsusu = "";
        //        if (Radiosusuno.Checked == true)
        //        {
        //            radsusu = "false";
        //        }
        //        else if (Radiosusuyes.Checked == true)
        //        {
        //            radsusu = "true";
        //        }
        //        soapmodel.subjective.Find(y => y.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")).value = radsusu;

        //        soapmodel.subjective.Find(y => y.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade")).remarks = hdnEndemicArea.Value;

        //        if (rbnutrisi2.Checked)
        //            soapmodel.subjective.Find(y => y.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).remarks = hdnNutrition.Value;
        //        else if (rbnutrisi1.Checked)
        //            soapmodel.subjective.Find(y => y.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).remarks = "";

        //        if (rbpuasa2.Checked)
        //            soapmodel.subjective.Find(y => y.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).remarks = hdnFasting.Value;
        //        else if (rbpuasa1.Checked)
        //            soapmodel.subjective.Find(y => y.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).remarks = "";

        //        if (rbOperas2.Checked)
        //        {
        //            if (hdnhistorysurgery.Value != "")
        //            {
        //                List<PatientSurgery> tempsurgery = new JavaScriptSerializer().Deserialize<List<PatientSurgery>>(hdnhistorysurgery.Value);
        //                soapmodel.patient_surgery = tempsurgery;
        //            }
        //            else
        //                soapmodel.patient_medication.Clear();
        //            //soapmodel.patient_surgery = GetRowList_PatientSurgery();
        //        }
        //        else if (rbOperasi.Checked)
        //        {
        //            if (hdnhistorysurgery.Value != "")
        //            {
        //                List<PatientSurgery> tempsurgery = new JavaScriptSerializer().Deserialize<List<PatientSurgery>>(hdnhistorysurgery.Value);
        //                soapmodel.patient_surgery = tempsurgery;
        //            }
        //            else
        //                soapmodel.patient_medication.Clear();
                    
        //        }
        //        else
        //        {
        //            soapmodel.patient_surgery.Clear();
        //        }

        //        if (soapmodel.patient_surgery == null)
        //            soapmodel.patient_surgery = new List<PatientSurgery>();

        //        if (rbProcOut2.Checked)
        //        {
        //            List<PatientProcedureHistory> tempprocout = new JavaScriptSerializer().Deserialize<List<PatientProcedureHistory>>(hdnProcedureOutside.Value);
        //            soapmodel.patient_procedure = tempprocout;
        //        }
        //        else if (rbProcOut1.Checked)
        //        {
        //            soapmodel.patient_procedure.Clear();
        //        }

        //        if (soapmodel.patient_procedure == null)
        //            soapmodel.patient_procedure = new List<PatientProcedureHistory>();

        //        List<PatientSpecialNotification> datareminder = GetRowList_ReminderNotes();
        //        hdnremindernotes.Value = new JavaScriptSerializer().Serialize(datareminder);
        //        List<PatientSpecialNotification> tempreminder = new JavaScriptSerializer().Deserialize<List<PatientSpecialNotification>>(hdnremindernotes.Value);
        //        soapmodel.patient_notification = tempreminder;

        //        if (soapmodel.patient_notification == null)
        //            soapmodel.patient_notification = new List<PatientSpecialNotification>();

        //        if (hfenableroutine.Value == "1")
        //        {
        //            if (hdnhistoryroutine.Value != "")
        //            {
        //                List<PatientRoutineMedication> tempcurrmed = new JavaScriptSerializer().Deserialize<List<PatientRoutineMedication>>(hdnhistoryroutine.Value);
        //                soapmodel.patient_medication = tempcurrmed;
        //            }
        //            else
        //                soapmodel.patient_medication.Clear();

        //        }
        //        else if (hfenableroutine.Value == "0")
        //        {
        //            if (hdnhistoryroutine.Value != "")
        //            {
        //                List<PatientRoutineMedication> tempcurrmed = new JavaScriptSerializer().Deserialize<List<PatientRoutineMedication>>(hdnhistoryroutine.Value);
        //                soapmodel.patient_medication = tempcurrmed;
        //            }
        //            else
        //                soapmodel.patient_medication.Clear();
                    
        //        }
        //        else
        //        {
        //            soapmodel.patient_medication.Clear();
        //        }

        //        if (soapmodel.patient_medication == null)
        //            soapmodel.patient_medication = new List<PatientRoutineMedication>();

        //        if (rbdrug2.Checked)
        //        {
        //            List<PatientAllergy> tempdrugsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistorydrugallergies.Value);
        //            soapmodel.patient_allergy = tempdrugsallergy;
        //            if (rbfood2.Checked)
        //            {
        //                soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(2));
        //            }
        //            if (rbother2.Checked)
        //            {
        //                soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(7));
        //            }
        //        }
        //        else if (rbfood2.Checked)
        //        {
        //            List<PatientAllergy> tempfoodsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryfoodallergies.Value);
        //            soapmodel.patient_allergy = tempfoodsallergy;
        //            if (rbother2.Checked)
        //            {
        //                soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(7));
        //            }
        //        }
        //        else if (rbother2.Checked)
        //        {
        //            List<PatientAllergy> tempothersallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryotherallergies.Value);
        //            soapmodel.patient_allergy = tempothersallergy;
        //        }
        //        else
        //        {
        //            soapmodel.patient_allergy.Clear();
        //        }

        //        if (soapmodel.patient_allergy == null)
        //            soapmodel.patient_allergy = new List<PatientAllergy>();

        //        List<PatientDiseaseHistory> templistdisease = new List<PatientDiseaseHistory>();
        //        if (rbpribadi2.Checked)
        //        {
        //            if (hdnDiseaseHistory.Value != "")
        //            {
        //                List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);
        //                templistdisease = tempdisease;
        //            }
        //        }
        //        else if (rbpribadi1.Checked)
        //        {
        //            if (hdnDiseaseHistory.Value != "")
        //            {
        //                List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);
        //                templistdisease = tempdisease;
        //            }
                    
        //        }
        //        else
        //        {
        //            soapmodel.patient_disease.Clear();
        //        }

        //        if (rbkeluarga2.Checked)
        //        {
        //            if (hdnFamilyDiseaseHistory.Value != "")
        //            {
        //                List<PatientDiseaseHistory> tempdiseasefam = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnFamilyDiseaseHistory.Value);
        //                templistdisease.AddRange(tempdiseasefam);
        //            }
        //        }
        //        else if (rbkeluarga1.Checked)
        //        {
        //            if (hdnFamilyDiseaseHistory.Value != "")
        //            {
        //                List<PatientDiseaseHistory> tempdiseasefam = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnFamilyDiseaseHistory.Value);
        //                templistdisease.AddRange(tempdiseasefam);
        //            }
        //        }

        //        soapmodel.patient_disease = templistdisease;
        //        if (soapmodel.patient_disease == null)
        //            soapmodel.patient_disease = new List<PatientDiseaseHistory>();

        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("ae3ca8c2-eab0-41b6-9e3e-3ecb8071a9d0")).value = txtbloodlow.Text;
        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("e5efd220-b68e-4652-ad03-d56ef29fcebb")).value = txtbloodhigh.Text;
        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("78cbb61f-4a11-470a-b770-1a44eb04357f")).value = txtpulserate.Text;
        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("e6ae2ea9-b321-4756-bf96-2dc232e4a7ba")).value = txtrespiratory.Text;
        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("e903246c-df95-4fe0-96d2-cf90c036d3d7")).value = txtspo.Text;
        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("2eeca752-a2ea-4426-b3cf-c1ea3833bf30")).value = txttemperature.Text;
        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("52ce9350-bfb2-4072-8893-d0c6cf8b3634")).value = txtweight.Text;
        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("2a8dbddb-edfe-4704-876e-5a2d735bb541")).value = txtheight.Text;
        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D")).remarks = txtOthers.Text;
        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D")).value = "Abnormal";
        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("A8E0013B-0443-4E7A-B670-4DB9362B40E4")).value = txtlingkarkepala.Text;

        //        if (lbleyetotal.Text == "_")
        //            soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("a9c5cd3c-1e02-4db2-a047-2f1983d1315b")).value = "";
        //        else
        //            soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("a9c5cd3c-1e02-4db2-a047-2f1983d1315b")).value = hdnEye.Value;

        //        if (lblmovetotal.Text == "_")
        //            soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("89b583a5-3003-43ab-9693-60ea6181c8d5")).value = "";
        //        else
        //            soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("89b583a5-3003-43ab-9693-60ea6181c8d5")).value = hdnMove.Value;

        //        if (lblverbaltotal.Text == "_")
        //            soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("fe4cf48c-17a6-4720-ad23-186517dd9c85")).value = "";
        //        else
        //            soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("fe4cf48c-17a6-4720-ad23-186517dd9c85")).value = hdnVerbal.Value;

        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176")).value = hdnpainscale.Value;

        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = hdnMentalStatus.Value;
        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).remarks = hdnMentalStatusremark.Value;

        //        soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = hdnConsciousness.Value;
        //        //if (mental1.Checked)
        //        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Good Orientation";
        //        //else if (mental2.Checked)
        //        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Disorientated";
        //        //else if (mental3.Checked)
        //        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Cooperative";
        //        //else if (mental4.Checked)
        //        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Non Cooperative";

        //        //if (consciousness1.Checked)
        //        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Compos mentis";
        //        //else if (consciousness2.Checked)
        //        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Somnolent";
        //        //else if (consciousness3.Checked)
        //        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Stupor";
        //        //else if (consciousness4.Checked)
        //        //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Coma";

        //        foreach (var assessment in soapmodel.assessment.Where(x => x.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")))
        //        {
        //            if (assessment.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47"))
        //            {//"Primary"
        //                assessment.remarks = txtPrimary.Text;
        //            }
        //        }
        //        foreach (var planning in soapmodel.planning.Where(x => x.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6")))
        //        {
        //            if (planning.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
        //            {
        //                planning.remarks = txtPlanning.Text;
        //            }
        //        }

        //        soapmodel.planning.Find(y => y.soap_mapping_id == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")).remarks = txtHasilTindakan.Text;
        //        soapmodel.planning.Find(y => y.soap_mapping_id == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")).value = soapmodel.patient_id.ToString();

        //        soapmodel = StdPlanning.GetPlanningValues(soapmodel);
        //        soapmodel = StdImunisasi.GetImunisasiValues(soapmodel);

        //        log.Debug(LogLibrary.Logging("S", "AutoSaveSOAP", Helper.GetLoginUser(this), soapmodel.ToString()));
        //        var getMap = clsSOAP.SaveAsDraftSOAP(soapmodel, hfPageSoapId.Value);
        //        var JsongetMap = (JObject)JsonConvert.DeserializeObject<dynamic>(getMap.Result);
        //        var Status = JsongetMap.Property("status").Value.ToString();
        //        var Message = JsongetMap.Property("message").Value.ToString();

        //        if (Status == "Fail")
        //        {
        //            log.Info(LogLibrary.Logging("E", "AutoSaveSOAP", Helper.GetLoginUser(this), "Failed Save SOAP : " + Message));
        //            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Failed to Save SOAP');", true);
        //            fail_box("Failed to Save SOAP", Message);
        //        }
                
        //        log.Debug(LogLibrary.Logging("E", "AutoSaveSOAP", Helper.GetLoginUser(this), getMap.ToString()));

        //        //Response.Redirect(Request.RawUrl);
        //        //soapmodel = StdAssessment.GetAssessmentValues(soapmodel);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Already Submitted');", true);
        //    }
        //    log.Info(LogLibrary.Logging("E", "AutoSaveSOAP", Helper.GetLoginUser(this), "Finish savedraft"));
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "hidecopy", "hidecopy();", true);
        //}
        //catch (Exception ex)
        //{
        //    SOAP x = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
        //    string jsonfailed = new JavaScriptSerializer().Serialize(x);

        //    log.Error(LogLibrary.Error("AutoSaveSOAP Failed", Helper.GetLoginUser(this), ex.Message + jsonfailed));
        //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('Failed to Save SOAP');", true);
        //    error_box("Failed to Save SOAP", ex);
        //}
    }

    public void SaveDraft_SOAP()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            //if (Complaint.Text != "" && Anamnesis.Text != "" && txtPrimary.Text != "")
            //{

            //SOAP soapmodel = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
            var soapmodel = MappingforGetdataSOAPSession();

            if (hfsavemode.Value == "0")
            {

                ((List<Subjective>)soapmodel.subjective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("f815de0e-e57a-4250-a2be-bdd27c1876ac"));

                if (hdnTindakan.Value != "")
                {
                    List<Subjective> listTindakan = new JavaScriptSerializer().Deserialize<List<Subjective>>(hdnTindakan.Value);
                    soapmodel.subjective.AddRange(listTindakan);
                }

                ((List<Subjective>)soapmodel.subjective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("0cbb6a1d-392b-4fed-8e8a-696bea3cbc32"));

                if (hdnDeleteReason.Value != "")
                {
                    List<Subjective> listDeleteReason = new JavaScriptSerializer().Deserialize<List<Subjective>>(hdnDeleteReason.Value);
                    soapmodel.subjective.AddRange(listDeleteReason);
                }

                ((List<Objective>)soapmodel.objective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c"));
                if (hdnFallRisk.Value != "")
                {
                    List<Objective> listobjfallrisk = new JavaScriptSerializer().Deserialize<List<Objective>>(hdnFallRisk.Value);
                    soapmodel.objective.AddRange(listobjfallrisk);
                }
                ((List<Objective>)soapmodel.objective).RemoveAll(x => x.soap_mapping_id == Guid.Parse("B0C9D8E0-7533-43CD-BFCF-E8C0AC1D4B7E"));
                if (hdnFallRiskHandling.Value != "")
                {
                    List<Objective> listobjfallriskHandling = new JavaScriptSerializer().Deserialize<List<Objective>>(hdnFallRiskHandling.Value);
                    soapmodel.objective.AddRange(listobjfallriskHandling);
                }

                ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf")).remarks = Complaint.Text;
                ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0")).remarks = Anamnesis.Text;

                string radhamil = "";
                if (Radiohamilno.Checked == true)
                {
                    radhamil = "false";
                }
                else if (Radiohamilyes.Checked == true)
                {
                    radhamil = "true";
                }
                ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")).value = radhamil;
                string radsusu = "";
                if (Radiosusuno.Checked == true)
                {
                    radsusu = "false";
                }
                else if (Radiosusuyes.Checked == true)
                {
                    radsusu = "true";
                }
                ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")).value = radsusu;

                ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("6a10c1fa-7c43-4e7c-a855-eaea815bcade")).remarks = hdnEndemicArea.Value;

                if (rbnutrisi2.Checked)
                    ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).remarks = hdnNutrition.Value;
                else if (rbnutrisi1.Checked)
                    ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("82b114b2-303c-43ec-963b-851b19a11eea")).remarks = "";

                if (rbpuasa2.Checked)
                    ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).remarks = hdnFasting.Value;
                else if (rbpuasa1.Checked)
                    ((List<Subjective>)soapmodel.subjective).Find(y => y.soap_mapping_id == Guid.Parse("bb077100-eaae-41e4-91db-b2b10154ee48")).remarks = "";

                if (rbOperas2.Checked)
                {
                    if (hdnhistorysurgery.Value != "")
                    {
                        List<PatientSurgery> tempsurgery = new JavaScriptSerializer().Deserialize<List<PatientSurgery>>(hdnhistorysurgery.Value);
                        soapmodel.patient_surgery = tempsurgery;
                    }
                    else
                        soapmodel.patient_medication.Clear();
                    //soapmodel.patient_surgery = GetRowList_PatientSurgery();
                }
                else if (rbOperasi.Checked)
                {
                    if (hdnhistorysurgery.Value != "")
                    {
                        List<PatientSurgery> tempsurgery = new JavaScriptSerializer().Deserialize<List<PatientSurgery>>(hdnhistorysurgery.Value);
                        soapmodel.patient_surgery = tempsurgery;
                    }
                    else
                        soapmodel.patient_medication.Clear();
                    
                }
                else
                {
                    soapmodel.patient_surgery.Clear();
                }

                if (soapmodel.patient_surgery == null)
                    soapmodel.patient_surgery = new List<PatientSurgery>();

                if (rbProcOut2.Checked)
                {
                    List<PatientProcedureHistory> tempprocout = new JavaScriptSerializer().Deserialize<List<PatientProcedureHistory>>(hdnProcedureOutside.Value);
                    soapmodel.patient_procedure = tempprocout;
                }
                else if (rbProcOut1.Checked)
                {
                    soapmodel.patient_procedure.Clear();
                }

                if (soapmodel.patient_procedure == null)
                    soapmodel.patient_procedure = new List<PatientProcedureHistory>();

                List<PatientSpecialNotification> datareminder = GetRowList_ReminderNotes();
                hdnremindernotes.Value = new JavaScriptSerializer().Serialize(datareminder);
                List<PatientSpecialNotification> tempreminder = new JavaScriptSerializer().Deserialize<List<PatientSpecialNotification>>(hdnremindernotes.Value);
                soapmodel.patient_notification = tempreminder;

                if (soapmodel.patient_notification == null)
                    soapmodel.patient_notification = new List<PatientSpecialNotification>();


                if (hfenableroutine.Value == "1")
                {
                    if (hdnhistoryroutine.Value != "")
                    {
                        List<PatientRoutineMedication> tempcurrmed = new JavaScriptSerializer().Deserialize<List<PatientRoutineMedication>>(hdnhistoryroutine.Value);
                        soapmodel.patient_medication = tempcurrmed;
                    }
                    else
                        soapmodel.patient_medication.Clear();

                }
                else if (hfenableroutine.Value == "0")
                {
                    if (hdnhistoryroutine.Value != "")
                    {
                        List<PatientRoutineMedication> tempcurrmed = new JavaScriptSerializer().Deserialize<List<PatientRoutineMedication>>(hdnhistoryroutine.Value);
                        soapmodel.patient_medication = tempcurrmed;
                    }
                    else
                        soapmodel.patient_medication.Clear();
                    
                }
                else
                {
                    soapmodel.patient_medication.Clear();
                }

                if (soapmodel.patient_medication == null)
                    soapmodel.patient_medication = new List<PatientRoutineMedication>();

                List<PatientRoutineMedication> pengobatan_nodata = new List<PatientRoutineMedication>();
                PatientRoutineMedication row_pengobatan = new PatientRoutineMedication();
                row_pengobatan.patient_routine_medication_id = Guid.Empty;
                row_pengobatan.medication = "Tidak Ada Pengobatan";
                row_pengobatan.routine_sales_item_code = "";
                row_pengobatan.routine_sales_item_id = 0;
                row_pengobatan.is_delete = 0;

                if (hfenableroutine.Value == "0")
                {
                    pengobatan_nodata.Add(row_pengobatan);
                    soapmodel.patient_medication = pengobatan_nodata;
                }

                if (rbdrug2.Checked)
                {
                    List<PatientAllergy> tempdrugsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistorydrugallergies.Value);
                    soapmodel.patient_allergy = tempdrugsallergy;
                    if (rbfood2.Checked)
                    {
                        soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(2));
                    }
                    if (rbother2.Checked)
                    {
                        soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(7));
                    }
                }
                else if (rbfood2.Checked)
                {
                    List<PatientAllergy> tempfoodsallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryfoodallergies.Value);
                    soapmodel.patient_allergy = tempfoodsallergy;
                    if (rbother2.Checked)
                    {
                        soapmodel.patient_allergy.AddRange(GetRowList_PatientAllergy(7));
                    }
                }
                else if (rbother2.Checked)
                {
                    List<PatientAllergy> tempothersallergy = new JavaScriptSerializer().Deserialize<List<PatientAllergy>>(hdnhistoryotherallergies.Value);
                    soapmodel.patient_allergy = tempothersallergy;
                }
                else
                {
                    soapmodel.patient_allergy.Clear();
                }

                if (soapmodel.patient_allergy == null)
                    soapmodel.patient_allergy = new List<PatientAllergy>();

                //initial no data
                List<PatientAllergy> drug_noalergi = new List<PatientAllergy>();
                List<PatientAllergy> food_noalergi = new List<PatientAllergy>();
                List<PatientAllergy> other_noalergi = new List<PatientAllergy>();
                PatientAllergy rowdrug = new PatientAllergy();
                rowdrug.patient_allergy_id = Guid.Empty;
                rowdrug.allergy_type = 1;
                rowdrug.allergy = "Tidak Ada Alergi";
                rowdrug.allergy_reaction = "Tidak Ada Reaksi";
                rowdrug.is_delete = 0;
                PatientAllergy rowfood = new PatientAllergy();
                rowfood.patient_allergy_id = Guid.Empty;
                rowfood.allergy_type = 2;
                rowfood.allergy = "Tidak Ada Alergi";
                rowfood.allergy_reaction = "Tidak Ada Reaksi";
                rowfood.is_delete = 0;
                PatientAllergy rowother = new PatientAllergy();
                rowother.patient_allergy_id = Guid.Empty;
                rowother.allergy_type = 7;
                rowother.allergy = "Tidak Ada Alergi";
                rowother.allergy_reaction = "Tidak Ada Reaksi";
                rowother.is_delete = 0;
                //end initial no data

                //set if no allergy
                if (rbdrug1.Checked)
                {
                    drug_noalergi.Add(rowdrug);
                    soapmodel.patient_allergy.AddRange(drug_noalergi);
                }
                if (rbfood1.Checked)
                {
                    food_noalergi.Add(rowfood);
                    soapmodel.patient_allergy.AddRange(food_noalergi);
                }
                if (rbother1.Checked)
                {
                    other_noalergi.Add(rowother);
                    soapmodel.patient_allergy.AddRange(other_noalergi);
                }

                List<PatientDiseaseHistory> templistdisease = new List<PatientDiseaseHistory>();
                if (rbpribadi2.Checked)
                {
                    if (hdnDiseaseHistory.Value != "")
                    {
                        List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);
                        templistdisease = tempdisease;
                    }
                }
                else if (rbpribadi1.Checked)
                {
                    if (hdnDiseaseHistory.Value != "")
                    {
                        List<PatientDiseaseHistory> tempdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);
                        templistdisease = tempdisease;
                    }
                }
                else
                {
                    soapmodel.patient_disease.Clear();
                }

                if (rbkeluarga2.Checked)
                {
                    if (hdnFamilyDiseaseHistory.Value != "")
                    {
                        List<PatientDiseaseHistory> tempdiseasefam = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnFamilyDiseaseHistory.Value);
                        templistdisease.AddRange(tempdiseasefam);
                    }
                }
                else if (rbkeluarga1.Checked)
                {
                    if (hdnFamilyDiseaseHistory.Value != "")
                    {
                        List<PatientDiseaseHistory> tempdiseasefam = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnFamilyDiseaseHistory.Value);
                        templistdisease.AddRange(tempdiseasefam);
                    }
                }
                soapmodel.patient_disease = templistdisease;
                if (soapmodel.patient_disease == null)
                    soapmodel.patient_disease = new List<PatientDiseaseHistory>();

                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("ae3ca8c2-eab0-41b6-9e3e-3ecb8071a9d0")).value = txtbloodlow.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e5efd220-b68e-4652-ad03-d56ef29fcebb")).value = txtbloodhigh.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("78cbb61f-4a11-470a-b770-1a44eb04357f")).value = txtpulserate.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e6ae2ea9-b321-4756-bf96-2dc232e4a7ba")).value = txtrespiratory.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("e903246c-df95-4fe0-96d2-cf90c036d3d7")).value = txtspo.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("2eeca752-a2ea-4426-b3cf-c1ea3833bf30")).value = txttemperature.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("52ce9350-bfb2-4072-8893-d0c6cf8b3634")).value = txtweight.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("2a8dbddb-edfe-4704-876e-5a2d735bb541")).value = txtheight.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D")).remarks = txtOthers.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D")).value = "Abnormal";
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("A8E0013B-0443-4E7A-B670-4DB9362B40E4")).value = txtlingkarkepala.Text;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("19516BE9-9D54-4E30-8E90-0A8B41F5BA66")).value = txtbmi.Text;

                if (lbleyetotal.Text == "_")
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("a9c5cd3c-1e02-4db2-a047-2f1983d1315b")).value = "";
                else
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("a9c5cd3c-1e02-4db2-a047-2f1983d1315b")).value = hdnEye.Value;

                if (lblmovetotal.Text == "_")
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("89b583a5-3003-43ab-9693-60ea6181c8d5")).value = "";
                else
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("89b583a5-3003-43ab-9693-60ea6181c8d5")).value = hdnMove.Value;

                if (lblverbaltotal.Text == "_")
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("fe4cf48c-17a6-4720-ad23-186517dd9c85")).value = "";
                else
                    ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("fe4cf48c-17a6-4720-ad23-186517dd9c85")).value = hdnVerbal.Value;

                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176")).value = hdnpainscale.Value;

                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = hdnMentalStatus.Value;
                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).remarks = hdnMentalStatusremark.Value;

                ((List<Objective>)soapmodel.objective).Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = hdnConsciousness.Value;
                //if (mental1.Checked)
                //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Good Orientation";
                //else if (mental2.Checked)
                //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Disorientated";
                //else if (mental3.Checked)
                //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Cooperative";
                //else if (mental4.Checked)
                //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")).value = "Non Cooperative";

                //if (consciousness1.Checked)
                //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Compos mentis";
                //else if (consciousness2.Checked)
                //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Somnolent";
                //else if (consciousness3.Checked)
                //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Stupor";
                //else if (consciousness4.Checked)
                //    soapmodel.objective.Find(y => y.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")).value = "Coma";

                foreach (var assessment in ((List<Assessment>)soapmodel.assessment).Where(x => x.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")))
                {
                    if (assessment.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47"))
                    {//"Primary"
                        assessment.remarks = txtPrimary.Text;
                    }
                }
                foreach (var planning in ((List<Planning>)soapmodel.planning).Where(x => x.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6")))
                {
                    if (planning.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))
                    {
                        planning.remarks = txtPlanning.Text;
                    }
                }
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")).remarks = txtHasilTindakan.Text;
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")).value = soapmodel.patient_id.ToString();

                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("7198D4D5-7670-4853-9AFA-CD7C606C5C27")).value = "";
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("7198D4D5-7670-4853-9AFA-CD7C606C5C27")).remarks = hdnSchedule_travel.Value;
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3893BF09-82A2-449A-B5F0-D3A2BF9905C1")).value = hdnCondition_travel.Value;
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("3893BF09-82A2-449A-B5F0-D3A2BF9905C1")).remarks = hdncondition_date.Value;
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("A4D139D7-67E2-44BC-A5E7-8BB17DD3F21E")).value = hdnSeating_type.Value;
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("A4D139D7-67E2-44BC-A5E7-8BB17DD3F21E")).remarks = "";
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("80A8E43A-53BF-4A32-B992-C9F32C7AA458")).value = hdnEscort_type.Value;
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("80A8E43A-53BF-4A32-B992-C9F32C7AA458")).remarks = hdnescort_ddl.Value;
                ((List<Planning>)soapmodel.planning).RemoveAll(x => x.soap_mapping_id == Guid.Parse("F3378407-B8A6-45FF-A27B-87F3CCF0F2B4"));
                if (hdnSpecial_Needs.Value != "")
                {
                    List<Planning> listSpecialNeeds = new List<Planning>();
                    var dataspecialneeds = hdnSpecial_Needs.Value.Split(',');
                    for (int i = 0; i < dataspecialneeds.Count() ; i++)
                    {
                        Planning P = new Planning();
                        P.soap_mapping_id = Guid.Parse("F3378407-B8A6-45FF-A27B-87F3CCF0F2B4");
                        P.soap_mapping_name = "SPECIAL_NEEDS";
                        P.planning_id = Guid.Empty;
                        P.value = dataspecialneeds[i].ToString();
                        P.remarks = "";
                        listSpecialNeeds.Add(P);
                    }
                    soapmodel.planning.AddRange(listSpecialNeeds);
                }
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("30F9892A-22C7-4C8B-8005-BE08E105F05F")).value = "";
                ((List<Planning>)soapmodel.planning).Find(y => y.soap_mapping_id == Guid.Parse("30F9892A-22C7-4C8B-8005-BE08E105F05F")).remarks = txtTravelRecommendation.Text.ToString();

                //soapmodel.procedure_notes = txtProcedure.Text;
                soapmodel.procedure_diagnosis = (List<ProcedureDiagnosis>)Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value];

                soapmodel = StdPlanning.GetPlanningValues(soapmodel);
                soapmodel = StdImunisasi.GetImunisasiValues(soapmodel);

                string templateid = Request.QueryString["PageSoapId"];
                if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
                {

                }
                else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
                {
                    soapmodel = StdTriage.GetTriageValues(soapmodel);
                }
                else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
                {
                    //soapmodel = StdObgyn.GetObgynValues(soapmodel);
                    soapmodel = getObgynData(soapmodel);
                }
                else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
                {
                    soapmodel = getPediatricData(soapmodel);
                }

                List<ReferalData> referalDatas = (List<ReferalData>)Session[Helper.SessionSOAPReferral + hfguidadditional.Value];
                if (referalDatas != null)
                {
                    soapmodel.referal = referalDatas;
                }
                else
                {
                    soapmodel.referal = new List<ReferalData>();
                }
                //------------------------------------------------------------------

                string appointmentid = Request.QueryString["AppointmentId"];
                string username = Helper.GetLoginUser(this);

                Dictionary<string, string> logParam = new Dictionary<string, string>
                {
                    { "PageSoap_ID", templateid },
                    { "Appointment_ID", appointmentid == null ? "-" : appointmentid.ToString() },
                    { "Username", username }
                };
                //log.debug(logconfig.LogStart("MappingforSaveAsDraftSOAP", logParam, LogConfig.JsonToString(soapmodel)));
                var getMap = MappingforSaveAsDraftSOAP(soapmodel);
                var JsongetMap = (JObject)JsonConvert.DeserializeObject<dynamic>(getMap.Result);
                var Status = JsongetMap.Property("status").Value.ToString();
                var Message = JsongetMap.Property("message").Value.ToString();
                //log.debug(logconfig.LogEnd("MappingforSaveAsDraftSOAP", Status, Message));

                var data = JsongetMap.Property("data").Value.ToString();

                if (Status == "Fail")
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Failed to Save SOAP');", true);
                    fail_box("Failed to Save SOAP", Message);
                    ActionBackUpToSession();
                }
                else
                {
                    //Submit Health Record
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Submit_HR", "SubmitIframe_HI();", true);

                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalsavesuccess", "$('#modalsavesuccess').modal({backdrop: 'static', keyboard: false});", true);
                    //btnOkSave.Focus();
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "savedraft", "savedraft();", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "savedraft", "savedraft();", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('Save as draft successful');", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refresh", "window.location.reload();", true);

                    //reset the session backup
                    Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]] = null;

                    if (data.Contains("MODIFIED"))
                    {
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('SOAP already modified by other user. <br /> Please refresh this page for update data. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "$('#modalnotifalreadymodif').modal('show');", true);
                    }
                    else
                    {
                        List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                        markerlist.Find(x => x.key == "SAVESOAPmarker").value = "marked";
                        Session[Helper.SESSIONmarker] = markerlist;
                        Response.Redirect(Request.RawUrl, false);
                    }
                }

                //Response.Redirect(Request.RawUrl);
                //soapmodel = StdAssessment.GetAssessmentValues(soapmodel);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Already Submitted');", true);
            }

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('Subjective/Analysis can not be empty');", true);
            //}

            ScriptManager.RegisterStartupScript(this, this.GetType(), "hidecopy", "hidecopy();", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "AutoSaveSOAP", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            //SOAP x = (SOAP)Session[Helper.Sessionsoapmodel + hfguidadditional.Value];
            var x = MappingforGetdataSOAPSession();
            string jsonfailed = new JavaScriptSerializer().Serialize(x);

            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "AutoSaveSOAP", StartTime, "Error", MyUser.GetUsername(), "", jsonfailed, ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString() + jsonfailed), ex);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertempty", "alert('Failed to Save SOAP');", true);
            error_box("Failed to Save SOAP", ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btngetitem_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            //foreach (GridViewRow rows in gvw_doctor.Rows)
            //{
            //    LinkButton linkcopy = (LinkButton)rows.FindControl("copyitem");
            //    linkcopy.Style.Add("color", "#000");
            //}

            if (HF_copysoap_oldrow.Value != "")
            {
                LinkButton linkcopy = (LinkButton)gvw_doctor.Rows[int.Parse(HF_copysoap_oldrow.Value)].FindControl("copyitem");
                linkcopy.Style.Add("color", "#000");
                linkcopy.Style.Add("font-weight", "normal");
            }

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            HiddenField hfcopyPatientId = (HiddenField)gvw_doctor.Rows[selRowIndex].FindControl("hfcopyPatientId");
            HiddenField hfcopyOrganizationId = (HiddenField)gvw_doctor.Rows[selRowIndex].FindControl("hfcopyOrganizationId");
            HiddenField hfcopyAdmissionId = (HiddenField)gvw_doctor.Rows[selRowIndex].FindControl("hfcopyAdmissionId");
            HiddenField hfcopyDoctorId = (HiddenField)gvw_doctor.Rows[selRowIndex].FindControl("hfcopyDoctorId");
            HiddenField hfcopyEncounterId = (HiddenField)gvw_doctor.Rows[selRowIndex].FindControl("hfcopyEncounterId");
            LinkButton linkpatientcopy = (LinkButton)gvw_doctor.Rows[selRowIndex].FindControl("copyitem");
            HF_copysoap_oldrow.Value = selRowIndex.ToString();

            linkpatientcopy.Style.Add("color", "#4d9b35");
            linkpatientcopy.Style.Add("font-weight", "bold");

            //var getsoap = clsSOAP.getSOAP(hfcopyEncounterId.Value, long.Parse(hfcopyPatientId.Value), long.Parse(hfcopyAdmissionId.Value),long.Parse(hfcopyOrganizationId.Value), long.Parse(hfcopyDoctorId.Value));
            //ResultSOAP Jsongetsoap = JsonConvert.DeserializeObject<ResultSOAP>(getsoap.Result.ToString());
            //hfsoapcopystring.Value = getsoap.Result.ToString();

            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Encounter_ID", hfcopyEncounterId.Value },
                { "Patient_ID", hfcopyPatientId.Value },
                { "Admission_ID", hfcopyAdmissionId.Value },
                { "Organization_ID", hfcopyOrganizationId.Value },
                { "Doctor_ID", hfcopyDoctorId.Value }
            };
            //log.debug(logconfig.LogStart("MappingforGetdataCOPYSOAP", logParam));

            var Jsongetsoap = MappingforGetdataCOPYSOAP(hfcopyEncounterId.Value, long.Parse(hfcopyPatientId.Value), long.Parse(hfcopyAdmissionId.Value), long.Parse(hfcopyOrganizationId.Value), long.Parse(hfcopyDoctorId.Value));
            //log.debug(logconfig.LogEnd("MappingforGetdataCOPYSOAP", Jsongetsoap.Status, Jsongetsoap.Message));

            int is_different_doctor = 0;
            LabelDisclaimerCopy.Visible = false;

            if (hfcopyDoctorId.Value != MyUser.GetHopeUserID())
            {
                is_different_doctor = 1;
                LabelDisclaimerCopy.Visible = true;
            }

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
            else
            {
                lblAnamnesis.Text = "";
                lblChiefComplaint.Text = "";
            }

            if (flagsubjective == 0 || is_different_doctor == 1)
                chkSubjective.Enabled = false;
            else
                chkSubjective.Checked = true;

            int flagobjective = 0;
            chkObjective.Enabled = true;
            chkObjective.Checked = false;
            List<Objective> listobjective = Jsongetsoap.list.objective;
            if (listobjective.Count > 0)
            {
                foreach (Objective x in listobjective.Where(x => x.soap_mapping_id == Guid.Parse("7218971c-e89f-4172-ae3c-b7fb855c1d6d")))
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
            else
            {
                lblobjective.Text = "";
            }

            if (flagobjective == 0 || is_different_doctor == 1)
                chkObjective.Enabled = false;
            else
                chkObjective.Checked = true;

            int flagassessment = 0;
            chkAssessment.Enabled = true;
            chkAssessment.Checked = false;
            List<Assessment> listassessment = Jsongetsoap.list.assessment;
            if (listassessment.Count > 0)
            {
                foreach (Assessment x in listassessment.Where(x => x.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")))
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
            else
            {
                lblAssessment.Text = "";
            }

            if (flagassessment == 0 || is_different_doctor == 1)
                chkAssessment.Enabled = false;
            else
                chkAssessment.Checked = true;

            int flagplanning = 0;
            chkPlanning.Enabled = true;
            chkPlanning.Checked = false;
            List<Planning> listplanning = Jsongetsoap.list.planning;
            if (listplanning.Count > 0)
            {
                foreach (Planning x in listplanning.Where(x => x.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6")))
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
            else
            {
                lblPlanning.Text = "";
            }

            if (flagplanning == 0 || is_different_doctor == 1)
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
                                    where (a.type == "ClinicLab" || a.type == "CitoLab" || a.type == "MicroLab" || a.type == "PatologiLab" || a.type == "PanelLab" || a.type == "MDCLab") && a.isdelete == 0 && a.IsFutureOrder == false
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

                if (flaglab == 0 || is_different_doctor == 1)
                    chkLab.Enabled = false;
                else
                    chkLab.Checked = true;

                var datarad = (
                                    from a in listcpoetrans
                                    where (a.type == "MRI1" || a.type == "MRI3" || a.type == "Radiology" || a.type == "USG" || a.type == "CT") && a.isdelete == 0 && a.IsFutureOrder == false
                                    select a
                               ).Distinct().ToList();


                if (datarad.Count > 0)
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
                else
                {
                    rptRad.DataSource = null;
                    rptRad.DataBind();
                }

                if (flagrad == 0 || is_different_doctor == 1)
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
            List<CompoundHeaderSoap> listcompheader = new List<CompoundHeaderSoap>();
            listprescription = Jsongetsoap.list.prescription;
            listcompheader = Jsongetsoap.list.compound_header;

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

            if (Helper.GetFlagCompound(this) == "FALSE")
                chkCompound.Enabled = false;
            else
            {
                chkCompound.Enabled = true;
                chkCompound.Checked = false;
            }
            

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

                //ViewState["listpres"] = listprescription;
                if (Helper.GetFlagCompound(this) == "FALSE")
                    chkCompound.Enabled = false;
                else
                {
                    chkCompound.Enabled = true;
                    chkCompound.Checked = false;
                    if (listcompheader.Count > 0)
                    {
                        flagcompound = 1;
                        DataTable dtcompdrug = Helper.ToDataTable(listcompheader);
                        //rptCompound.DataSource = dtcompdrug;
                        rptCompound.DataSource = null;
                        rptCompound.DataBind();
                    }
                    else
                    {
                        rptCompound.DataSource = null;
                        rptCompound.DataBind();
                    }
                }
                //ViewState.Remove("listpres");


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


            chkCompound.Enabled = false;
            chkCompound.Checked = false;

            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Find(y => y.setting_name.ToUpper() == "COPY_PRESCRIPTION".ToUpper()).setting_value == "FALSE")
            {
                chkSubjective.Checked = false;
                chkObjective.Checked = false;
                chkAssessment.Checked = false;
                chkPlanning.Checked = false;
                chkLab.Checked = false;
                chkRad.Checked = false;
                chkDrugs.Checked = false;
                chkConsumables.Checked = false;
                chkCompound.Checked = false;

                ToogleChkDrug();
            }

            divblokcopysoap.Visible = false;
            UpdatePanelModalCopySoap.Update();

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btngetitem_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btngetitem_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnCopyHOPE_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            List<PrescriptionHOPE> listprescriptionhope = GetRowList_PrescriptionHOPE();
            if (listprescriptionhope.Count > 0)
            {
                StdPlanning.copyprescriptionfromhope(listprescriptionhope);
            }
            else
            {
                ShowToastr("There is no Prescription to Copy", "Copy HOPE Alert!", "Warning");
            }

            StdPlanning.UpdateListPrescription();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCopyPrescription", "$('#modalCopyPrescription').modal('hide');", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnCopyHOPE_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnCopyHOPE_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected List<PrescriptionHOPE> GetRowList_PrescriptionHOPE()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        List<PrescriptionHOPE> data = new List<PrescriptionHOPE>();
        try
        {

            foreach (GridViewRow rows in gvw_drughope.Rows)
            {
                HiddenField prescription_id = (HiddenField)rows.FindControl("prescription_id");
                HiddenField prescription_no = (HiddenField)rows.FindControl("prescription_no");
                HiddenField item_id = (HiddenField)rows.FindControl("item_id");
                HiddenField uom_id = (HiddenField)rows.FindControl("uom_id");
                HiddenField frequency_id = (HiddenField)rows.FindControl("frequency_id");
                HiddenField dose_uom_id = (HiddenField)rows.FindControl("dose_uom_id");
                HiddenField dose_text = (HiddenField)rows.FindControl("dose_text");
                HiddenField administration_route_id = (HiddenField)rows.FindControl("administration_route_id");
                HiddenField is_routine = (HiddenField)rows.FindControl("is_routine");
                HiddenField is_consumables = (HiddenField)rows.FindControl("is_consumables");
                HiddenField compound_id = (HiddenField)rows.FindControl("compound_id");
                HiddenField compound_name = (HiddenField)rows.FindControl("compound_name");
                HiddenField origin_prescription_id = (HiddenField)rows.FindControl("origin_prescription_id");
                HiddenField hope_arinvoice_id = (HiddenField)rows.FindControl("hope_arinvoice_id");
                HiddenField is_delete = (HiddenField)rows.FindControl("is_delete");
                Label item_name = (Label)rows.FindControl("item_name");
                Label dosage_id = (Label)rows.FindControl("dosage_id");
                Label dose_uom = (Label)rows.FindControl("dose_uom");
                Label frequency_code = (Label)rows.FindControl("frequency_code");
                Label administration_route_code = (Label)rows.FindControl("administration_route_code");
                Label remarks = (Label)rows.FindControl("remarks");
                Label quantity = (Label)rows.FindControl("quantity");
                Label uom_code = (Label)rows.FindControl("uom_code");
                Label iteration = (Label)rows.FindControl("iteration");

                PrescriptionHOPE row = new PrescriptionHOPE();
                row.prescription_id = Guid.Parse(prescription_id.Value);
                row.prescription_no = prescription_no.Value;
                row.item_id = item_id.Value == null ? 0 : long.Parse(item_id.Value); ;
                row.uom_id = long.Parse(uom_id.Value);
                row.frequency_id = long.Parse(frequency_id.Value);
                row.dose_uom_id = long.Parse(dose_uom_id.Value);
                row.dose_text = dose_text.Value;
                row.administration_route_id = long.Parse(administration_route_id.Value);
                row.is_routine = int.Parse(is_routine.Value);
                row.is_consumables = int.Parse(is_consumables.Value);
                row.compound_id = Guid.Parse(compound_id.Value);
                row.compound_name = compound_name.Value;
                row.origin_prescription_id = Guid.Parse(origin_prescription_id.Value);
                row.hope_aritem_id = long.Parse(hope_arinvoice_id.Value);
                row.is_delete = int.Parse(is_delete.Value);
                row.item_name = item_name.Text;
                row.dosage_id = dosage_id.Text;
                row.dose_uom = dose_uom.Text;
                row.frequency_code = frequency_code.Text;
                row.administration_route_code = administration_route_code.Text;
                row.remarks = remarks.Text;
                row.quantity = quantity.Text;
                row.uom_code = uom_code.Text;
                row.iteration = int.Parse(iteration.Text);
                data.Add(row);
            }
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "GetRowList_PrescriptionHOPE", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "GetRowList_PrescriptionHOPE", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());

        return data;
    }

    protected void btngethopeprescription_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            //foreach (GridViewRow rows in gvw_doctorhope.Rows)
            //{
            //    LinkButton linkcopy = (LinkButton)rows.FindControl("copyitem");
            //    linkcopy.Style.Add("color", "#000");
            //}

            if (HF_copyhope_oldrow.Value != "")
            {
                LinkButton linkcopy = (LinkButton)gvw_doctorhope.Rows[int.Parse(HF_copyhope_oldrow.Value)].FindControl("copyitem");
                linkcopy.Style.Add("color", "#000");
                linkcopy.Style.Add("font-weight", "normal");
            }

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            HiddenField hfAdmissionId = (HiddenField)gvw_doctorhope.Rows[selRowIndex].FindControl("hfAdmissionId");
            LinkButton linkpatientcopy = (LinkButton)gvw_doctorhope.Rows[selRowIndex].FindControl("copyitem");
            HF_copyhope_oldrow.Value = selRowIndex.ToString();

            linkpatientcopy.Style.Add("color", "#4d9b35");
            linkpatientcopy.Style.Add("font-weight", "bold");

            var getprescriptionhope = clsSOAP.GetPrescriptionHope(long.Parse(hfAdmissionId.Value), 7);
            ResultViewMedicalEntryHOPE Jsongetprescriptionhope = JsonConvert.DeserializeObject<ResultViewMedicalEntryHOPE>(getprescriptionhope.Result.ToString());

            List<MedicalEntryHOPE> diagnosehope = Jsongetprescriptionhope.list.medicals;
            rptdiagnosahope.DataSource = diagnosehope;
            rptdiagnosahope.DataBind();

            List<PrescriptionHOPE> prescriptionhope = Jsongetprescriptionhope.list.prescriptions;

            foreach (var templist in prescriptionhope)
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

            if (prescriptionhope.Where(x => x.is_consumables == 0).Count() > 0)
            {
                gvw_drughope.DataSource = prescriptionhope.Where(x => x.is_consumables == 0);
                gvw_drughope.DataBind();
            }
            else
            {
                gvw_drughope.DataSource = null;
                gvw_drughope.DataBind();
            }

            if (prescriptionhope.Where(x => x.is_consumables == 1).Count() > 0)
            {
                gvw_conshope.DataSource = prescriptionhope.Where(x => x.is_consumables == 1);
                gvw_conshope.DataBind();
            }
            else
            {
                gvw_conshope.DataSource = null;
                gvw_conshope.DataBind();
            }

            div_nodatacopyhope.Style.Add("display", "none");
            UpdatePanelModalCopyDrugs.Update();

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btngethopeprescription_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btngethopeprescription_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var data = ((DataRowView)e.Item.DataItem).Row.ItemArray[18].ToString();

        //ResultSOAP Jsongetsoap = JsonConvert.DeserializeObject<ResultSOAP>(hfsoapcopystring.Value);
        var Jsongetsoap = MappingforAssigndataSOAP(hfsoapcopystring.Value);

        List<Prescription> listpres = Jsongetsoap.list.prescription;
        //List<Prescription> listpres = (List<Prescription>)ViewState["listpres"];
        DataTable dtcompdetail = Helper.ToDataTable(listpres).Select("compound_id <> '00000000-0000-0000-0000-000000000000' and item_id <> 0 and compound_name = '" + data + "'").CopyToDataTable();
        var repeater2 = (Repeater)e.Item.FindControl("rptCompDetail");
        repeater2.DataSource = dtcompdetail;
        repeater2.DataBind();

    }

    protected void txtsearchDoctor_onChange(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Organization_ID", Helper.organizationId.ToString() },
                { "Patient_ID", hfPatientId.Value },
                { "Keyword", txtSearchDoctor.Text }
            };
            //log.debug(logconfig.LogStart("getCopySOAP", logParam));
            var getcopysoap = clsSOAP.getCopySOAP(Helper.organizationId, hfPatientId.Value, txtSearchDoctor.Text);
            ResultViewAdmissionCopySOAP Jsongetcopysoap = JsonConvert.DeserializeObject<ResultViewAdmissionCopySOAP>(getcopysoap.Result.ToString());
            //log.debug(logconfig.LogEnd("getCopySOAP", Jsongetcopysoap.Status, Jsongetcopysoap.Message));

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

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "txtsearchDoctor_onChange", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "txtsearchDoctor_onChange", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void txtsearchDoctorprescription_onChange(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Patient_ID", hfPatientId.Value },
                { "Organization_ID", "7" }
            };
            //log.debug(logconfig.LogStart("getCopyPrescription", logParam));
            //var getcopyprescription = clsSOAP.getCopyPrescription(hfPatientId.Value, Helper.organizationId);
            var getcopyprescription = clsSOAP.getCopyPrescription(hfPatientId.Value, 7);
            ResultAdmissionPrescriptionHOPE Jsongetcopyprescription = JsonConvert.DeserializeObject<ResultAdmissionPrescriptionHOPE>(getcopyprescription.Result.ToString());
            //log.debug(logconfig.LogEnd("getCopyPrescription", Jsongetcopyprescription.Status, Jsongetcopyprescription.Message));

            if (Jsongetcopyprescription.list.Count() > 0)
            {
                List<AdmissionPrescriptionHOPE> tempdatahope = new List<AdmissionPrescriptionHOPE>();
                tempdatahope = Jsongetcopyprescription.list.Where(y => y.DoctorName.ToString().ToLower().Contains(txtsearchdoctor_prescription.Text.ToLower())).ToList();
                DataTable dtcopy = Helper.ToDataTable(tempdatahope);
                gvw_doctorhope.DataSource = dtcopy;
                gvw_doctorhope.DataBind();
            }
            else
            {
                gvw_doctorhope.DataSource = null;
                gvw_doctorhope.DataBind();
            }
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "txtsearchDoctorprescription_onChange", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "txtsearchDoctorprescription_onChange", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnCopySOAP_onClick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            if (hfsoapcopystring.Value != "")
            {
                string rawsoap = hfsoapstring.Value;
                string copysoap = hfsoapcopystring.Value;

                //ResultSOAP Jsonrawsoap = JsonConvert.DeserializeObject<ResultSOAP>(rawsoap);
                //ResultSOAP Jsoncopysoap = JsonConvert.DeserializeObject<ResultSOAP>(copysoap);
                var Jsonrawsoap = MappingforAssigndataSOAP(rawsoap);
                var Jsoncopysoap = MappingforAssigndataSOAP(copysoap);

                var result = MappingforGetdataSOAPSession();
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
                result.ModifiedDate = HiddenLastModif.Value.ToString();

                // procedure and diagnostic
                result.procedure_diagnosis = Jsonrawsoap.list.procedure_diagnosis;
                result.infectious_disease = Jsonrawsoap.list.infectious_disease;
                result.infectious_alert = Jsonrawsoap.list.infectious_alert;


                if (chkSubjective.Checked)
                {
                    var subj = (
                        from raw in (List<Subjective>)Jsonrawsoap.list.subjective
                        join copy in (List<Subjective>)Jsoncopysoap.list.subjective on raw.soap_mapping_id equals copy.soap_mapping_id
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
                            from raw in (List<Objective>)Jsonrawsoap.list.objective
                            join copy in (List<Objective>)Jsoncopysoap.list.objective on raw.soap_mapping_id equals copy.soap_mapping_id
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
                            from raw in (List<Assessment>)Jsonrawsoap.list.assessment
                            join copy in (List<Assessment>)Jsoncopysoap.list.assessment on raw.soap_mapping_id equals copy.soap_mapping_id
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
                            from raw in (List<Planning>)Jsonrawsoap.list.planning
                            join copy in (List<Planning>)Jsoncopysoap.list.planning on raw.soap_mapping_id equals copy.soap_mapping_id
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
                    foreach (var x in ((List<CpoeTrans>)Jsoncopysoap.list.cpoe_trans).Where(x => x.type == "ClinicLab" || x.type == "CitoLab" || x.type == "MicroLab" || x.type == "PatologiLab" || x.type == "PanelLab" || x.type == "MDCLab"))
                    {
                        if (x.type == "ClinicLab" || x.type == "CitoLab" || x.type == "MicroLab" || x.type == "PatologiLab" || x.type == "PanelLab" || x.type == "MDCLab")
                        {
                            x.isnew = 1;
                            x.issubmit = 0;
                            x.IsSendHope = 0;
                            temp.Add(x);
                        }
                    }
                }
                else
                {
                    foreach (var x in ((List<CpoeTrans>)Jsonrawsoap.list.cpoe_trans).Where(x => x.type == "ClinicLab" || x.type == "CitoLab" || x.type == "MicroLab" || x.type == "PatologiLab" || x.type == "PanelLab" || x.type == "MDCLab"))
                    {
                        if (x.type == "ClinicLab" || x.type == "CitoLab" || x.type == "MicroLab" || x.type == "PatologiLab" || x.type == "PanelLab" || x.type == "MDCLab")
                        {
                            temp.Add(x);
                        }
                    }
                }

                if (chkRad.Checked)
                {
                    foreach (var x in ((List<CpoeTrans>)Jsoncopysoap.list.cpoe_trans).Where(x => x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT"))
                    {
                        if (x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT")
                        {
                            x.isnew = 1;
                            x.issubmit = 0;
                            x.IsSendHope = 0;
                            temp.Add(x);
                        }
                    }
                }
                else
                {
                    foreach (var x in ((List<CpoeTrans>)Jsonrawsoap.list.cpoe_trans).Where(x => x.type == "MRI1" || x.type == "MRI3" || x.type == "Radiology" || x.type == "USG" || x.type == "CT"))
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
                List<CompoundHeaderSoap> compheadertemp = new List<CompoundHeaderSoap>();
                List<CompoundDetailSoap> compdetailtemp = new List<CompoundDetailSoap>();

                if (chkDrugs.Checked)
                {
                    List<string> listcheckdrug = new List<string>();
                    foreach (RepeaterItem rows in rptDrugs.Items)
                    {
                        HiddenField HF_drugsIdCopySoap = (HiddenField)rows.FindControl("HF_drugsIdCopySoap");
                        CheckBox chkChooseDrugs = (CheckBox)rows.FindControl("chkChooseDrugs");
                        if (chkChooseDrugs.Checked == true)
                        {
                            listcheckdrug.Add(HF_drugsIdCopySoap.Value);
                        }
                    }

                    bool is_itemissue = false;
                    List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
                    if (orgsetting.Find(y => y.setting_name.ToUpper() == "SEND_DATA_ITEM_ISSUE".ToUpper()).setting_value == "TRUE")
                    {
                        is_itemissue = true;
                    }
                    List<Prescription> listdruguomcheck = new List<Prescription>();

                    foreach (var x in ((List<Prescription>)Jsoncopysoap.list.prescription).Where(x => x.compound_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_consumables == 0 && x.is_delete == 0))
                    {
                        if (x.compound_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_consumables == 0 && x.is_delete == 0)
                        {
                            if (listcheckdrug.Where(y => y == x.item_id.ToString()).Count() > 0)
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
                        }
                    }

                    if (listdruguomcheck.Count != 0)
                    {
                        RepeaterDrugsUomChange.DataSource = listdruguomcheck;
                        RepeaterDrugsUomChange.DataBind();

                        dialogDrugsUomChange.Visible = true;
                        dialogDrugsUomChange.Attributes.Remove("style");
                        dialogDrugsUomChange.Attributes.Add("style", "position: fixed; top:15%; left:0; right:0; width: 40%; ");

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modaluomchangedrugssoap", "$('#modaluomchangedrugssoap').modal();", true);

                        UpdatePanelUomChangesoap.Update();

                    }

                    foreach (var x in ((List<Prescription>)Jsoncopysoap.list.prescription).Where(x => x.compound_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_consumables == 0 && x.is_delete == 0))
                    {
                        if (x.compound_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_consumables == 0 && x.is_delete == 0)
                        {
                            if (listcheckdrug.Where(y => y == x.item_id.ToString()).Count() > 0)
                            {
                                x.prescription_id = Guid.Empty;
                                if (x.uom_id != x.uom_idori)
                                {
                                    x.uom_id = x.uom_idori;
                                    x.uom_code = x.uom_codeori;
                                    x.quantity = "0";
                                }
                                drugstemp.Add(x);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var x in ((List<Prescription>)Jsonrawsoap.list.prescription).Where(x => x.compound_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_consumables == 0 && x.is_delete == 0))
                    {
                        if (x.compound_id == Guid.Parse("00000000-0000-0000-0000-000000000000") && x.is_consumables == 0 && x.is_delete == 0)
                        {
                            drugstemp.Add(x);
                        }
                    }
                }

                if (chkCompound.Checked)
                {
                    foreach (var x in Jsoncopysoap.list.compound_header)
                    {
                        x.prescription_compound_header_id = Guid.Empty;
                        compheadertemp.Add(x);
                    }

                    foreach (var x in Jsoncopysoap.list.compound_detail)
                    {
                        x.prescription_compound_detail_id = Guid.Empty;
                        compdetailtemp.Add(x);
                    }
                }
                else
                {
                    foreach (var x in Jsonrawsoap.list.compound_header)
                    {
                        x.prescription_compound_header_id = Guid.Empty;
                        compheadertemp.Add(x);
                    }

                    foreach (var x in Jsonrawsoap.list.compound_detail)
                    {
                        x.prescription_compound_detail_id = Guid.Empty;
                        compdetailtemp.Add(x);
                    }
                }

                if (chkConsumables.Checked)
                {
                    foreach (var x in ((List<Prescription>)Jsoncopysoap.list.prescription).Where(x => x.is_consumables == 1))
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
                    foreach (var x in ((List<Prescription>)Jsonrawsoap.list.prescription).Where(x => x.is_consumables == 1))
                    {
                        if (x.is_consumables == 1)
                        {
                            drugstemp.Add(x);
                        }
                    }
                }

                result.prescription = drugstemp;
                //result.compound_header = compheadertemp;
                //result.compound_detail = compdetailtemp;


                result.patient_allergy = Jsonrawsoap.list.patient_allergy;
                result.patient_surgery = Jsonrawsoap.list.patient_surgery;
                result.patient_disease = Jsonrawsoap.list.patient_disease;
                result.patient_medication = Jsonrawsoap.list.patient_medication;
                result.patient_procedure = Jsonrawsoap.list.patient_procedure;
                result.patient_notification = Jsonrawsoap.list.patient_notification;

                Session[Helper.Sessionsoapmodel + hfguidadditional.Value] = result;

                //StdSubjective.initializevalue(result.subjective, result.patient_disease, result.patient_surgery, result.patient_allergy, result.patient_medication);
                //StdGeneralCheckup.initializevalue(result.objective);

                //var varallergy = clsPatientDetail.GetPatientHistory(long.Parse(hfPatientId.Value), hfEncounterId.Value);
                //hfallergy.Value = varallergy.Result.ToString();

                ResultPatientHeader JsongetPatientHistory = JsonConvert.DeserializeObject<ResultPatientHeader>(hfheader.Value);
                //ResultPatientHistory Jsongetallergy = JsonConvert.DeserializeObject<ResultPatientHistory>(hfallergy.Value);
                ResultPatientHistory Jsongetallergy = new ResultPatientHistory();
                Jsongetallergy.list.allergy = new List<PatientHistory.PatientAllergy>();

                DataTable dtAllergy = Helper.ToDataTable(Jsongetallergy.list.allergy);
                StdPlanning.initializevaluecopy(result, JsongetPatientHistory.header, dtAllergy, Jsongetallergy.list.currentmedication, hfguidadditional.Value);

                
                List<Subjective> listsubjective = result.subjective;
                if (listsubjective.Count > 0)
                {
                    foreach (Subjective x in listsubjective)
                    {
                        if (x.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0"))
                        {//anamnesis
                            if (Anamnesis.Text != "")
                            {
                                if (chkSubjective.Checked)
                                {
                                    Anamnesis.Text = Anamnesis.Text + "\n" + x.remarks;
                                    Anamnesis.Rows = Anamnesis.Text.Split('\n').Length;
                                }
                            }
                            else
                            {
                                Anamnesis.Text = x.remarks;
                                Anamnesis.Rows = Anamnesis.Text.Split('\n').Length;
                            }
                        }
                        else if (x.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf"))
                        {//patient complaint
                            if (Complaint.Text != "")
                            {
                                if (chkSubjective.Checked)
                                {
                                    Complaint.Text = Complaint.Text + "\n" + x.remarks;
                                    Complaint.Rows = Complaint.Text.Split('\n').Length;
                                }
                            }
                            else
                            {
                                Complaint.Text = x.remarks;
                                Complaint.Rows = Complaint.Text.Split('\n').Length;
                            }
                        }
                        else if (x.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923"))
                        {//pregnancy

                            if (x.value != "")
                            {
                                //chkpregnant.Checked = bool.Parse(x.value);
                                var nilaihamil = bool.Parse(x.value);
                                if (nilaihamil == true)
                                {
                                    Radiohamilyes.Checked = true;
                                }
                                else if (nilaihamil == false)
                                {
                                    Radiohamilno.Checked = true;
                                }
                            }
                        }
                        else if (x.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937"))
                        {//breast feeding
                            if (x.value != "")
                            {
                                //chkbreastfeed.Checked = bool.Parse(x.value);
                                var nilaisusu = bool.Parse(x.value);
                                if (nilaisusu == true)
                                {
                                    Radiosusuyes.Checked = true;
                                }
                                else if (nilaisusu == false)
                                {
                                    Radiosusuno.Checked = true;
                                }
                            }
                        }
                    }
                }

                List<Objective> listobjective = result.objective;
                if (listobjective.Count > 0)
                {
                    foreach (Objective x in listobjective)
                    {
                        if (x.soap_mapping_id == Guid.Parse("AE3CA8C2-EAB0-41B6-9E3E-3ECB8071A9D0"))
                        {//"BLOOD PRESSURE LOW"
                            txtbloodlow.Text = x.value;
                        }
                        else if (x.soap_mapping_id == Guid.Parse("E5EFD220-B68E-4652-AD03-D56EF29FCEBB"))
                        {//"BLOOD PRESSURE HIGH"
                            txtbloodhigh.Text = x.value;
                        }
                        else if (x.soap_mapping_id == Guid.Parse("78CBB61F-4A11-470A-B770-1A44EB04357F"))
                        {//"PULSE RATE"
                            txtpulserate.Text = x.value;
                        }
                        else if (x.soap_mapping_id == Guid.Parse("2EECA752-A2EA-4426-B3CF-C1EA3833BF30"))
                        {//"TEMPERATURE"
                            txttemperature.Text = x.value;
                        }
                        else if (x.soap_mapping_id == Guid.Parse("52CE9350-BFB2-4072-8893-D0C6CF8B3634"))
                        {//"WEIGHT"
                            txtweight.Text = x.value;
                        }
                        else if (x.soap_mapping_id == Guid.Parse("2A8DBDDB-EDFE-4704-876E-5A2D735BB541"))
                        {//"HEIGHT"
                            txtheight.Text = x.value;
                        }
                        else if (x.soap_mapping_id == Guid.Parse("E6AE2EA9-B321-4756-BF96-2DC232E4A7BA"))
                        {//"RESPIRATORY RATE"
                            txtrespiratory.Text = x.value;
                        }
                        else if (x.soap_mapping_id == Guid.Parse("E903246C-DF95-4FE0-96D2-CF90C036D3D7"))
                        {//"SpO2"
                            txtspo.Text = x.value;
                        }
                        else if (x.soap_mapping_id == Guid.Parse("A8E0013B-0443-4E7A-B670-4DB9362B40E4"))
                        {//"Lingkar Kepala"
                            txtlingkarkepala.Text = x.value;
                        }
                        else if (x.soap_mapping_id == Guid.Parse("19516BE9-9D54-4E30-8E90-0A8B41F5BA66"))
                        {//"BMI"
                            txtbmi.Text = x.value;
                        }

                        else if (x.soap_mapping_id == Guid.Parse("7218971c-e89f-4172-ae3c-b7fb855c1d6d"))
                        {//"Others"
                            if (txtOthers.Text != "")
                            {
                                if (chkObjective.Checked)
                                {
                                    txtOthers.Text = txtOthers.Text + "\n" + x.remarks;
                                    txtOthers.Rows = txtOthers.Text.Split('\n').Length;
                                }
                            }
                            else
                            {
                                txtOthers.Text = x.remarks;
                                txtOthers.Rows = txtOthers.Text.Split('\n').Length;
                            }
                        }
                    }
                }

                List<Assessment> listassessment = result.assessment;
                if (listassessment.Count > 0)
                {
                    foreach (Assessment x in listassessment)
                    {
                        if (x.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47"))
                        {//"Primary"
                            if (txtPrimary.Text != "")
                            {
                                if (chkAssessment.Checked)
                                {
                                    txtPrimary.Text = txtPrimary.Text + "\n" + x.remarks;
                                    txtPrimary.Rows = txtPrimary.Text.Split('\n').Length;
                                }
                            }
                            else
                            {
                                txtPrimary.Text = x.remarks;
                                txtPrimary.Rows = txtPrimary.Text.Split('\n').Length;
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
                            if (txtPlanning.Text != "")
                            {
                                if (chkPlanning.Checked)
                                {
                                    txtPlanning.Text = txtPlanning.Text + "\n" + x.remarks;
                                    txtPlanning.Rows = txtPlanning.Text.Split('\n').Length;
                                }
                            }
                            else
                            {
                                txtPlanning.Text = x.remarks;
                                txtPlanning.Rows = txtPlanning.Text.Split('\n').Length;
                            }
                        }
                    }
                }
                Session[Helper.Sessionsoapmodel + hfguidadditional.Value] = result;

                //refresh update panel
                UP_S.Update();
                UP_SAnam.Update();
                UP_SPregnant.Update();
                UP_O.Update();
                UP_A.Update();
                UP_P.Update();

            }

            StdPlanning.UpdateListPrescription();
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCopySOAP", "$('#modalCopySOAP').modal('hide');", true);
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnCopySOAP_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnCopySOAP_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected List<PatientSpecialNotification> GetRowList_ReminderNotes()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        List<PatientSpecialNotification> data = new List<PatientSpecialNotification>();
        try
        {
            foreach (GridViewRow rows in gvw_remindernotes.Rows)
            {
                HiddenField special_notification_id = (HiddenField)rows.FindControl("special_notification_id");
                Label notification = (Label)rows.FindControl("lbl_notification");
                Label doctor_name = (Label)rows.FindControl("lbl_doctor_name");
                CheckBox check_reminder = (CheckBox)rows.FindControl("chkReminder");
                HiddenField is_myself = (HiddenField)rows.FindControl("HF_ismyself");

                PatientSpecialNotification row = new PatientSpecialNotification();

                row.special_notification_id = Guid.Parse(special_notification_id.Value);
                row.notification = notification.Text;
                row.doctor_name = doctor_name.Text;
                row.start_date = DateTime.Now;
                row.end_date = DateTime.Now;
                if (check_reminder.Checked)
                {
                    row.is_checked = true;
                }
                else
                {
                    row.is_checked = false;
                }
                row.is_myself = int.Parse(is_myself.Value.ToString());
                data.Add(row);
            }

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "GetRowList_ReminderNotes", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "GetRowList_ReminderNotes", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());

        return data;
    }

    protected void BtnAddReminder_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            DataTable dt = new DataTable();
            List<PatientSpecialNotification> data = GetRowList_ReminderNotes();
            if (data.Count == 0)
            {
                //dt.Columns.Add("special_notification_id");
                //dt.Columns.Add("notification");
                //dt.Columns.Add("doctor_name");
                //dt.Columns.Add("start_date");
                //dt.Columns.Add("end_date");
                //dt.Columns.Add("is_checked");
                //dt.Columns.Add("is_myself");
                //dt.Rows.Add(new Object[] { Guid.Empty, TxtReminderNotes.Text, Helper.GetLoginUser(this), DateTime.Now, DateTime.Now, true, 1 });

                PatientSpecialNotification newdata = new PatientSpecialNotification();
                newdata.special_notification_id = Guid.Empty;
                newdata.notification = TxtReminderNotes.Text;
                newdata.doctor_name = Helper.GetUserFullname(this);
                newdata.start_date = DateTime.Now;
                newdata.end_date = DateTime.Now;
                newdata.is_checked = true;
                newdata.is_myself = 1;
                data.Add(newdata);

                hdnremindernotes.Value = new JavaScriptSerializer().Serialize(data);
                dt = Helper.ToDataTable(data);
                dt.DefaultView.Sort = "start_date ASC";
                dt = dt.DefaultView.ToTable();

                gvw_remindernotes.DataSource = dt;
                gvw_remindernotes.DataBind();

            }
            else
            {
                DataTable dtReminder = new DataTable();
                //dtReminder.Rows.Add(new Object[] { Guid.Empty, TxtReminderNotes.Text, Helper.GetLoginUser(this), DateTime.Now, DateTime.Now, true, 1 });

                PatientSpecialNotification newdata = new PatientSpecialNotification();
                newdata.special_notification_id = Guid.Empty;
                newdata.notification = TxtReminderNotes.Text;
                newdata.doctor_name = Helper.GetUserFullname(this);
                newdata.start_date = DateTime.Now;
                newdata.end_date = DateTime.Now;
                newdata.is_checked = true;
                newdata.is_myself = 1;
                data.Add(newdata);

                hdnremindernotes.Value = new JavaScriptSerializer().Serialize(data);
                dtReminder = Helper.ToDataTable(data);
                dtReminder.DefaultView.Sort = "start_date ASC";
                dtReminder = dtReminder.DefaultView.ToTable();

                gvw_remindernotes.DataSource = dtReminder;
                gvw_remindernotes.DataBind();
            }

            TxtReminderNotes.Text = "";
            TxtReminderNotes.Focus();

            divtablereminder.Style.Add("display", "");
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "BtnAddReminder_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "BtnAddReminder_Click", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnDeleteReminder_onClick(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        try
        {

            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
            HiddenField special_notification_id = (HiddenField)gvw_remindernotes.Rows[selRowIndex].FindControl("special_notification_id");

            List<PatientSpecialNotification> data = GetRowList_ReminderNotes();
            DataTable dt = Helper.ToDataTable(data);
            data.RemoveAt(selRowIndex);
            dt.Rows[selRowIndex].Delete();
            if (dt.Rows.Count > 0)
            {
                dt.DefaultView.Sort = "start_date ASC";
                dt = dt.DefaultView.ToTable();
                gvw_remindernotes.DataSource = dt;
                gvw_remindernotes.DataBind();
                hdnremindernotes.Value = new JavaScriptSerializer().Serialize(data);
            }
            else
            {
                gvw_remindernotes.DataSource = null;
                gvw_remindernotes.DataBind();
                hdnremindernotes.Value = "";

                divtablereminder.Style.Add("display", "none");
            }

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteReminder_onClick", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btnDeleteReminder_onClick", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void chkhideReminder_CheckedChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        if (hdnremindernotes.Value != "")
        {
            if (chkhideReminder.Checked)
            {
                List<PatientSpecialNotification> tempreminder = new JavaScriptSerializer().Deserialize<List<PatientSpecialNotification>>(hdnremindernotes.Value);
                List<PatientSpecialNotification> reminderfilter = tempreminder.Where(y => y.is_myself == 1).ToList();
                if (reminderfilter.Count() > 0)
                {
                    DataTable dt = Helper.ToDataTable(reminderfilter);
                    dt.DefaultView.Sort = "start_date ASC";
                    dt = dt.DefaultView.ToTable();
                    gvw_remindernotes.DataSource = dt;
                    gvw_remindernotes.DataBind();
                }
                else
                {
                    gvw_remindernotes.DataSource = null;
                    gvw_remindernotes.DataBind();
                }
            }
            else
            {
                List<PatientSpecialNotification> tempreminder = new JavaScriptSerializer().Deserialize<List<PatientSpecialNotification>>(hdnremindernotes.Value);
                if (tempreminder.Count() > 0)
                {
                    DataTable dt = Helper.ToDataTable(tempreminder);
                    dt.DefaultView.Sort = "start_date ASC";
                    dt = dt.DefaultView.ToTable();
                    gvw_remindernotes.DataSource = dt;
                    gvw_remindernotes.DataBind();
                }
                else
                {
                    gvw_remindernotes.DataSource = null;
                    gvw_remindernotes.DataBind();
                }
            }
        }

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "chkhideReminder_CheckedChanged", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonSubmitdisableHidden_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        ActionBackUpToSession();

        bool iswarningmims = false;
        bool qty_flag = true, qtycons_flag = true, qtyadddrug_flag = true, qtyaddcons_flag = true;
        HFlagdrug.Value = "0";
        HFlagdrugadd.Value = "0";
        HFlagcons.Value = "0";
        HFlagconsadd.Value = "0";

        if (hftakedate.Value == "")
        {
            qty_flag = StdPlanning.CheckQuantityPrescription(1);
            //bool qtycomp_flag = StdPlanning.CheckQuantityPrescription(2);
            //bool qtycomp_detail_flag = StdPlanning.CheckQuantityPrescription(3);
            qtycons_flag = StdPlanning.CheckQuantityPrescription(4);
        }
        if (hfadditionaltakedate.Value == "")
        {
            qtyadddrug_flag = StdPlanning.CheckQuantityPrescription(5);
            qtyaddcons_flag = StdPlanning.CheckQuantityPrescription(6);
        }
        bool ismandatory_flag = StdPlanning.isMandatory();
        if (hftakedate.Value == "")
        {
            iswarningmims = StdPlanning.CheckDrugInteractionFunction(false, hfAdmissionNo.Value, hfMRNo.Value);
        }
        else if (hfadditionaltakedate.Value == "")
        {
            iswarningmims = StdPlanning.CheckDrugInteractionFunction(true, hfAdmissionNo.Value, hfMRNo.Value);
        }

        if (!qty_flag)
        {
            HFlagdrug.Value = "1";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Drugs Qty/Dose/Frequency/Route/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
        }
        else if (!qtycons_flag)
        {
            HFlagcons.Value = "1";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Consumables Qty/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
        }
        else if (!qtyadddrug_flag)
        {
            HFlagdrugadd.Value = "1";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Additional Drugs Qty/Dose/Frequency/Route/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
        }
        else if (!qtyaddcons_flag)
        {
            HFlagconsadd.Value = "1";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Additional Consumables Qty/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
        }
        else if (!ismandatory_flag)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mandatory", "warningnotificationOption(); toastr.warning('Clinical Diagnosis is mandatory <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmitDisable').modal('hide');", true);
        }
        else if (iswarningmims == true)
        {
            HFlagdrug.Value = "0";
            HFlagdrugadd.Value = "0";
            HFlagcons.Value = "0";
            HFlagconsadd.Value = "0";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mimswarning", "showdrugsinteraction();", true);
        }
        else
        {
            HFlagdrug.Value = "0";
            HFlagdrugadd.Value = "0";
            HFlagcons.Value = "0";
            HFlagconsadd.Value = "0";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mandatory", "$('#modalsubmitDisable').modal('show');", true);
            
        }

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "ButtonSubmitdisableHidden_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonSubmitHidden_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        ActionBackUpToSession();

        bool iswarningmims = false;
        bool qty_flag = true, qtycons_flag = true, qtyadddrug_flag = true, qtyaddcons_flag = true;
        HFlagdrug.Value = "0";
        HFlagdrugadd.Value = "0";
        HFlagcons.Value = "0";
        HFlagconsadd.Value = "0";

        List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
        string autosave = "";
        if (orgsetting.Where(y => y.setting_name.ToLower() == "AUTO_SAVE".ToLower()).Count() > 0)
        {
            autosave = orgsetting.Find(y => y.setting_name.ToUpper() == "AUTO_SAVE".ToUpper()).setting_value.ToString();
        }
        if (hftakedate.Value == "")
        {
            qty_flag = StdPlanning.CheckQuantityPrescription(1);
            //bool qtycomp_flag = StdPlanning.CheckQuantityPrescription(2);
            //bool qtycomp_detail_flag = StdPlanning.CheckQuantityPrescription(3);
            qtycons_flag = StdPlanning.CheckQuantityPrescription(4);

        }
        if (hfadditionaltakedate.Value == "")
        {
            qtyadddrug_flag = StdPlanning.CheckQuantityPrescription(5);
            qtyaddcons_flag = StdPlanning.CheckQuantityPrescription(6);

        }
        bool ismandatory_flag = StdPlanning.isMandatory();
        if (hftakedate.Value == "")
        {
            iswarningmims = StdPlanning.CheckDrugInteractionFunction(false, hfAdmissionNo.Value, hfMRNo.Value);
        }
        else if (hfadditionaltakedate.Value == "")
        {
            iswarningmims = StdPlanning.CheckDrugInteractionFunction(true, hfAdmissionNo.Value, hfMRNo.Value);
        }

        if (!qty_flag)
        {
            HFlagdrug.Value = "1";
            //if (autosave == "TRUE")
            //{
            //    AutoSaveSOAP();
            //}

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Drugs Qty/Dose/Frequency/Route/Instruction<br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmit').modal('hide');", true);
        }
        else if (!qtycons_flag)
        {
            HFlagcons.Value = "1";
            //if (autosave == "TRUE")
            //{
            //    AutoSaveSOAP();
            //}
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Consumables Qty/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmit').modal('hide');", true);
        }
        else if (!qtyadddrug_flag)
        {
            HFlagdrugadd.Value = "1";
            //if (autosave == "TRUE")
            //{
            //    AutoSaveSOAP();
            //}
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Additional Drugs Qty/Dose/Frequency/Route/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmit').modal('hide');", true);
        }
        else if (!qtyaddcons_flag)
        {
            HFlagconsadd.Value = "1";
            //if (autosave == "TRUE")
            //{
            //    AutoSaveSOAP();
            //}
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "warningnotificationOption(); toastr.warning('Please Check Additional Consumables Qty/Instruction <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmit').modal('hide');", true); 
        }
        else if (!ismandatory_flag)
        {
            //if (autosave == "TRUE")
            //{
            //    AutoSaveSOAP();
            //}
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mandatory", "warningnotificationOption(); toastr.warning('Clinical Diagnosis is mandatory <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Submit Alert!');$('#modalsubmit').modal('hide');", true);
        }
        else if (iswarningmims == true)
        {
            HFlagdrug.Value = "0";
            HFlagdrugadd.Value = "0";
            HFlagcons.Value = "0";
            HFlagconsadd.Value = "0";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mimswarning", "showdrugsinteraction();", true);
        }
        else
        {
            HFlagdrug.Value = "0";
            HFlagdrugadd.Value = "0";
            HFlagcons.Value = "0";
            HFlagconsadd.Value = "0";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mandatory", "$('#modalsubmit').modal('show');", true);
        }

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "ButtonSubmitHidden_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    public void error_box(string judul, Exception ex)
    {
        var frame = new StackTrace(ex, true).GetFrame(0);
        LabelErrorJudul.Text = "Error! " + judul;
        LabelErrorUser.Text = Helper.GetLoginUser(this.Page);
        LabelErrorTime.Text = DateTime.Now.ToString();
        LabelErrorEx.Text = ex.GetType().ToString();
        LabelErrorExDet.Text = ex.Message.ToString();
        LabelErrorExSF.Text = ex.StackTrace.ToString();
        LabelErrorExMethod.Text = ex.TargetSite.ToString();
        LabelErrorExLine.Text = frame.GetFileLineNumber().ToString();

        UpdatePanelErrorTitle.Update();
        UpdatePanelError.Update();

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Error", "showError();", true);
    }

    public void fail_box(string judul, string message)
    {
        LabelErrorJudul.Text = "Oops! " + judul;
        LabelErrorUser.Text = Helper.GetLoginUser(this.Page);
        LabelErrorTime.Text = DateTime.Now.ToString();
        LabelErrorEx.Text = message;

        UpdatePanelErrorTitle.Update();
        UpdatePanelError.Update();

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Fail", "showError();", true);
    }

    void SetStickerData(string btnid, string btnTT, string btnSS, string btnBS, string penyakit)
    {
        if (btnid == btnTT)
        {
            if (hdnDiseaseHistory.Value != "")
            {
                List<PatientDiseaseHistory> templistdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);

                PatientDiseaseHistory temp_patienthistory = templistdisease.Find(z => z.value == penyakit);
                templistdisease.Remove(temp_patienthistory);
                hdnDiseaseHistory.Value = new JavaScriptSerializer().Serialize(templistdisease);

                List<PatientDiseaseHistory> temprpt_disease = templistdisease.FindAll(x => x.value != "HAD" && x.value != "PRT" && x.value != "RHN" && x.value != "MRS" && x.value != "COVID");
                rptdisease.DataSource = Helper.ToDataTable(temprpt_disease);
                rptdisease.DataBind();
            }
        }
        else if (btnid == btnSS)
        {
            if (hdnDiseaseHistory.Value != "")
            {
                List<PatientDiseaseHistory> templistdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);

                templistdisease.Find(z => z.value == penyakit).status = "Sudah Sembuh";
                hdnDiseaseHistory.Value = new JavaScriptSerializer().Serialize(templistdisease);

                List<PatientDiseaseHistory> temprpt_disease = templistdisease.FindAll(x => x.value != "HAD" && x.value != "PRT" && x.value != "RHN" && x.value != "MRS" && x.value != "COVID");
                rptdisease.DataSource = Helper.ToDataTable(temprpt_disease);
                rptdisease.DataBind();
            }
        }
        else if (btnid == btnBS)
        {
            PatientDiseaseHistory temp_patienthistory = new PatientDiseaseHistory();
            temp_patienthistory.patient_disease_history_id = Guid.Empty;
            temp_patienthistory.value = penyakit;
            temp_patienthistory.remarks = penyakit;
            temp_patienthistory.status = "Belum Sembuh";
            temp_patienthistory.disease_history_type = 1;
            temp_patienthistory.is_delete = 0;

            if (hdnDiseaseHistory.Value != "")
            {
                List<PatientDiseaseHistory> templistdisease = new JavaScriptSerializer().Deserialize<List<PatientDiseaseHistory>>(hdnDiseaseHistory.Value);

                if (templistdisease.Find(z => z.value == penyakit) == null)
                {
                    templistdisease.Add(temp_patienthistory);
                }
                else
                {
                    templistdisease.Find(z => z.value == penyakit).status = "Belum Sembuh";
                }
                hdnDiseaseHistory.Value = new JavaScriptSerializer().Serialize(templistdisease);

                List<PatientDiseaseHistory> temprpt_disease = templistdisease.FindAll(x => x.value != "HAD" && x.value != "PRT" && x.value != "RHN" && x.value != "MRS" && x.value != "COVID");
                rptdisease.DataSource = Helper.ToDataTable(temprpt_disease);
                rptdisease.DataBind();
            }
            else
            {
                List<PatientDiseaseHistory> templistdisease = new List<PatientDiseaseHistory>();

                templistdisease.Add(temp_patienthistory);
                rbpribadi2.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "stickercheck", "ShowHideDiv2();", true);
                hdnDiseaseHistory.Value = new JavaScriptSerializer().Serialize(templistdisease);

                List<PatientDiseaseHistory> temprpt_disease = templistdisease.FindAll(x => x.value != "HAD" && x.value != "PRT" && x.value != "RHN" && x.value != "MRS" && x.value != "COVID");
                rptdisease.DataSource = Helper.ToDataTable(temprpt_disease);
                rptdisease.DataBind();
            }
        }

        if (rptdisease.Items.Count == 0)
        {
            lblmodalnodisease.Style.Add("display", "");
        }
        else
        {
            lblmodalnodisease.Style.Add("display", "none");
        }

        UP_FA_HealthRecord.Update();
        UpdatePanelModalIllness.Update();
    }

    protected void LB_hepb_Click(object sender, EventArgs e)
    {
        LinkButton buttonSticker = (LinkButton)sender;
        string buttonId = buttonSticker.ID;

        if (buttonId == "LB_hepb_1")
        {
            Button_HepBStickeroff.Visible = true;
            Button_HepBStickeron.Visible = false;
        }
        else if (buttonId == "LB_hepb_2")
        {
            Button_HepBStickeroff.Visible = true;
            Button_HepBStickeron.Visible = false; 
        }
        else if (buttonId == "LB_hepb_sakit")
        {
            Button_HepBStickeroff.Visible = false;
            Button_HepBStickeron.Visible = true; 
        }

        SetStickerData(buttonId, "LB_hepb_1", "LB_hepb_2", "LB_hepb_sakit", "Hepatitis B");
    }

    protected void LB_hepc_Click(object sender, EventArgs e)
    {
        LinkButton buttonSticker = (LinkButton)sender;
        string buttonId = buttonSticker.ID;

        if (buttonId == "LB_hepc_1")
        {
            Button_HepCStickeroff.Visible = true;
            Button_HepCStickeron.Visible = false;
        }
        else if (buttonId == "LB_hepc_2")
        {
            Button_HepCStickeroff.Visible = true;
            Button_HepCStickeron.Visible = false;
        }
        else if (buttonId == "LB_hepc_sakit")
        {
            Button_HepCStickeroff.Visible = false;
            Button_HepCStickeron.Visible = true;
        }

        SetStickerData(buttonId, "LB_hepc_1", "LB_hepc_2", "LB_hepc_sakit", "Hepatitis C");
    }

    protected void LB_tbc_Click(object sender, EventArgs e)
    {
        LinkButton buttonSticker = (LinkButton)sender;
        string buttonId = buttonSticker.ID;

        if (buttonId == "LB_tbc_1")
        {
            Button_TbcStickeroff.Visible = true;
            Button_TbcStickeron.Visible = false;
        }
        else if (buttonId == "LB_tbc_2")
        {
            Button_TbcStickeroff.Visible = true;
            Button_TbcStickeron.Visible = false;
        }
        else if (buttonId == "LB_tbc_sakit")
        {
            Button_TbcStickeroff.Visible = false;
            Button_TbcStickeron.Visible = true; 
        }

        SetStickerData(buttonId, "LB_tbc_1", "LB_tbc_2", "LB_tbc_sakit", "TBC");
    }

    protected void LB_Had_Click(object sender, EventArgs e)
    {
        LinkButton buttonSticker = (LinkButton)sender;
        string buttonId = buttonSticker.ID;

        if (buttonId == "LB_Had_1")
        {
            Button_HadStickeroff.Visible = true;
            Button_HadStickeron.Visible = false;
        }
        else if (buttonId == "LB_Had_2")
        {
            Button_HadStickeroff.Visible = true;
            Button_HadStickeron.Visible = false;
        }
        else if (buttonId == "LB_Had_sakit")
        {
            Button_HadStickeroff.Visible = false;
            Button_HadStickeron.Visible = true;
        }

        SetStickerData(buttonId, "LB_Had_1", "LB_Had_2", "LB_Had_sakit", "HAD");
    }

    protected void LB_Prt_Click(object sender, EventArgs e)
    {
        LinkButton buttonSticker = (LinkButton)sender;
        string buttonId = buttonSticker.ID;

        if (buttonId == "LB_Prt_1")
        {
            Button_PrtStickeroff.Visible = true;
            Button_PrtStickeron.Visible = false;
        }
        else if (buttonId == "LB_Prt_2")
        {
            Button_PrtStickeroff.Visible = true;
            Button_PrtStickeron.Visible = false;
        }
        else if (buttonId == "LB_Prt_sakit")
        {
            Button_PrtStickeroff.Visible = false;
            Button_PrtStickeron.Visible = true;
        }

        SetStickerData(buttonId, "LB_Prt_1", "LB_Prt_2", "LB_Prt_sakit", "PRT");
    }

    protected void LB_Rhn_Click(object sender, EventArgs e)
    {
        LinkButton buttonSticker = (LinkButton)sender;
        string buttonId = buttonSticker.ID;

        if (buttonId == "LB_Rhn_1")
        {
            Button_RhnStickeroff.Visible = true;
            Button_RhnStickeron.Visible = false;
        }
        else if (buttonId == "LB_Rhn_2")
        {
            Button_RhnStickeroff.Visible = true;
            Button_RhnStickeron.Visible = false;
        }
        else if (buttonId == "LB_Rhn_sakit")
        {
            Button_RhnStickeroff.Visible = false;
            Button_RhnStickeron.Visible = true;
        }

        SetStickerData(buttonId, "LB_Rhn_1", "LB_Rhn_2", "LB_Rhn_sakit", "RHN");
    }

    protected void LB_Mrs_Click(object sender, EventArgs e)
    {
        LinkButton buttonSticker = (LinkButton)sender;
        string buttonId = buttonSticker.ID;

        if (buttonId == "LB_Mrs_1")
        {
            Button_MrsStickeroff.Visible = true;
            Button_MrsStickeron.Visible = false;
        }
        else if (buttonId == "LB_Mrs_2")
        {
            Button_MrsStickeroff.Visible = true;
            Button_MrsStickeron.Visible = false;
        }
        else if (buttonId == "LB_Mrs_sakit")
        {
            Button_MrsStickeroff.Visible = false;
            Button_MrsStickeron.Visible = true;
        }

        SetStickerData(buttonId, "LB_Mrs_1", "LB_Mrs_2", "LB_Mrs_sakit", "MRS");
    }

    protected void LB_Cv_Click(object sender, EventArgs e)
    {
        LinkButton buttonSticker = (LinkButton)sender;
        string buttonId = buttonSticker.ID;

        if (buttonId == "LB_Cv_1")
        {
            Button_CvStickeroff.Visible = true;
            Button_CvStickeron.Visible = false;
        }
        else if (buttonId == "LB_Cv_2")
        {
            Button_CvStickeroff.Visible = true;
            Button_CvStickeron.Visible = false;
        }
        else if (buttonId == "LB_Cv_sakit")
        {
            Button_CvStickeroff.Visible = false;
            Button_CvStickeron.Visible = true;
        }

        SetStickerData(buttonId, "LB_Cv_1", "LB_Cv_2", "LB_Cv_sakit", "COVID");
    }

    protected void ButtonAjaxSearchDiagProc_Click(object sender, EventArgs e)
    {
        //DataTable DiagProcSelect;
        //DiagProcSelect = ((DataTable)Session[Helper.SessionItemDiagProc]).Select("SalesItemId = '" + HF_ItemSelectedDiagProc.Value + "'").CopyToDataTable();

        //List<ProcedureDiagnosis> data = (List<ProcedureDiagnosis>)Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value];
        //if (data == null)
        //{
        //    data = new List<ProcedureDiagnosis>();
        //}

        //if (DiagProcSelect.Rows.Count > 0)
        //{

        //    ProcedureDiagnosis temp = new ProcedureDiagnosis();

        //    temp.EncounterProcedureId = Guid.Empty;
        //    temp.ProcedureItemId = long.Parse(DiagProcSelect.Rows[0]["SalesItemId"].ToString());
        //    temp.ProcedureItemName = DiagProcSelect.Rows[0]["SalesItemName"].ToString();
        //    temp.ProcedureItemTypeId = long.Parse(DiagProcSelect.Rows[0]["SalesItemTypeId"].ToString());

        //    if (temp != null)
        //    {
        //        DataTable dttemp = Helper.ToDataTable(data);

        //        //if (data.Where(x => x.ProcedureItemId == temp.ProcedureItemId).Count() == 0)
        //        //{
        //            data.Add(temp);
        //            DataTable dta = Helper.ToDataTable(data);

        //            Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value] = data;
        //            Gvw_submitdiagnosticprocedure.DataSource = dta;
        //            Gvw_submitdiagnosticprocedure.DataBind();
        //        //}
        //        //else
        //        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);

        //    }
        //}

        //if (data.Count > 0)
        //{
        //    divdiagproc_en.Visible = true;
        //}
        //else
        //{
        //    divdiagproc_en.Visible = false;
        //}

        //txtItemDiagProc_AC.Text = "";
    }

    protected void btndeletediagproc_Click(object sender, ImageClickEventArgs e)
    {
        //string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

        //try
        //{
        //    int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        //    List<ProcedureDiagnosis> data = (List<ProcedureDiagnosis>)Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value];

        //    if (data.Count > 0)
        //    {
        //        data.RemoveAt(selRowIndex);
        //        Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value] = data;

        //        DataTable dt = Helper.ToDataTable(data);
        //        Gvw_submitdiagnosticprocedure.DataSource = dt;
        //        Gvw_submitdiagnosticprocedure.DataBind();
        //    }
        //    else
        //    {
        //        Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value] = null;
        //        Gvw_submitdiagnosticprocedure.DataSource = null;
        //        Gvw_submitdiagnosticprocedure.DataBind();
        //    }

        //    if (data.Count > 0)
        //    {
        //        divdiagproc_en.Visible = true;
        //    }
        //    else
        //    {
        //        divdiagproc_en.Visible = false;
        //    }

        //    Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btndeletediagproc_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        //}
        //catch (Exception ex)
        //{
        //    Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "btndeletediagproc_Click", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        //}
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonAjaxSearchDiagProc_Dis_Click(object sender, EventArgs e)
    {
        //DataTable DiagProcSelect;
        //DiagProcSelect = ((DataTable)Session[Helper.SessionItemDiagProc]).Select("SalesItemId = '" + HF_ItemSelectedDiagProc_Dis.Value + "'").CopyToDataTable();

        //List<ProcedureDiagnosis> data = (List<ProcedureDiagnosis>)Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value];
        //if (data == null)
        //{
        //    data = new List<ProcedureDiagnosis>();
        //}

        //if (DiagProcSelect.Rows.Count > 0)
        //{
        //    ProcedureDiagnosis temp = new ProcedureDiagnosis();

        //    temp.EncounterProcedureId = Guid.Empty;
        //    temp.ProcedureItemId = long.Parse(DiagProcSelect.Rows[0]["SalesItemId"].ToString());
        //    temp.ProcedureItemName = DiagProcSelect.Rows[0]["SalesItemName"].ToString();
        //    temp.ProcedureItemTypeId = long.Parse(DiagProcSelect.Rows[0]["SalesItemTypeId"].ToString());

        //    if (temp != null)
        //    {
        //        DataTable dttemp = Helper.ToDataTable(data);

                //if (data.Where(x => x.ProcedureItemId == temp.ProcedureItemId).Count() == 0)
                //{
                //data.Add(temp);
                //DataTable dta = Helper.ToDataTable(data);

                //Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value] = data;
                //Gvw_submitdiagnosticprocedure_dis.DataSource = dta;
                //Gvw_submitdiagnosticprocedure_dis.DataBind();
                //}
                //else
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);
        //    }
        //}

        //if (data.Count > 0)
        //{
        //    divdiagproc_dis.Visible = true;
        //}
        //else
        //{
        //    divdiagproc_dis.Visible = false;
        //}

        //txtItemDiagProc_AC_Dis.Text = "";
    }

    //protected void btndeletediagproc_dis_Click(object sender, ImageClickEventArgs e)
    //{
    //    string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); //Log.Info(LogConfig.LogStart());

    //    try
    //    {
    //        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

    //        List<ProcedureDiagnosis> data = (List<ProcedureDiagnosis>)Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value];

    //        if (data.Count > 0)
    //        {
    //            data.RemoveAt(selRowIndex);
    //            Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value] = data;

    //            DataTable dt = Helper.ToDataTable(data);
    //            Gvw_submitdiagnosticprocedure_dis.DataSource = dt;
    //            Gvw_submitdiagnosticprocedure_dis.DataBind();
    //        }
    //        else
    //        {
    //            Session[Helper.SessionDataselectedDiagProc + hfguidadditional.Value] = null;
    //            Gvw_submitdiagnosticprocedure_dis.DataSource = null;
    //            Gvw_submitdiagnosticprocedure_dis.DataBind();
    //        }

    //        if (data.Count > 0)
    //        {
    //            divdiagproc_dis.Visible = true;
    //        }
    //        else
    //        {
    //            divdiagproc_dis.Visible = false;
    //        }
    //        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "ButtonAjaxSearchDiagProc_Dis_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "ButtonAjaxSearchDiagProc_Dis_Click", StartTime, "Error", MyUser.GetUsername(), "", "", ex.Message)); //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
    //    }

    //    //Log.Info(LogConfig.LogEnd());
    //}

    protected void KeepAliveButton_Click(object sender, EventArgs e)
    {
        //just refresh
        ActionBackUpToSession();
    }

    protected void NoDrugAllergy_Click(object sender, EventArgs e)
    {
        //initial no data
        List<PatientAllergy> drug_noalergi = new List<PatientAllergy>();
        PatientAllergy rowdrug = new PatientAllergy();
        rowdrug.patient_allergy_id = Guid.Empty;
        rowdrug.allergy_type = 1;
        rowdrug.allergy = "Tidak Ada Alergi";
        rowdrug.allergy_reaction = "Tidak Ada Reaksi";
        rowdrug.is_delete = 0;
        //end initial no data

        drug_noalergi.Add(rowdrug);
        rptdrugallergies.DataSource = Helper.ToDataTable(drug_noalergi);
        rptdrugallergies.DataBind();
        lblmodalnodrug.Style.Add("display", "none");
        StdPlanning.UpdateDrugAllergy(Helper.ToDataTable(drug_noalergi));
        hdnhistorydrugallergies.Value = new JavaScriptSerializer().Serialize(drug_noalergi);

        UP_FA_MedicationAllergies.Update();
        StdPlanning.UpdateListAllergy();
    }

    protected void NoFoodAllergy_Click(object sender, EventArgs e)
    {
        List<PatientAllergy> food_noalergi = new List<PatientAllergy>();
        PatientAllergy rowfood = new PatientAllergy();
        rowfood.patient_allergy_id = Guid.Empty;
        rowfood.allergy_type = 2;
        rowfood.allergy = "Tidak Ada Alergi";
        rowfood.allergy_reaction = "Tidak Ada Reaksi";
        rowfood.is_delete = 0;

        food_noalergi.Add(rowfood);
        rptfoodallergies.DataSource = Helper.ToDataTable(food_noalergi);
        rptfoodallergies.DataBind();
        lblmodalnofood.Style.Add("display", "none");
        StdPlanning.UpdateFoodAllergy(Helper.ToDataTable(food_noalergi));
        hdnhistoryfoodallergies.Value = new JavaScriptSerializer().Serialize(food_noalergi);

        UP_FA_MedicationAllergies.Update();
        StdPlanning.UpdateListAllergy();
    }

    protected void NoOtherAllergy_Click(object sender, EventArgs e)
    {
        List<PatientAllergy> other_noalergi = new List<PatientAllergy>();
        PatientAllergy rowother = new PatientAllergy();
        rowother.patient_allergy_id = Guid.Empty;
        rowother.allergy_type = 7;
        rowother.allergy = "Tidak Ada Alergi";
        rowother.allergy_reaction = "Tidak Ada Reaksi";
        rowother.is_delete = 0;

        other_noalergi.Add(rowother);
        rptotherallergies.DataSource = Helper.ToDataTable(other_noalergi);
        rptotherallergies.DataBind();
        lblmodalnoother.Style.Add("display", "none");
        StdPlanning.UpdateOtherAllergy(Helper.ToDataTable(other_noalergi));
        hdnhistoryotherallergies.Value = new JavaScriptSerializer().Serialize(other_noalergi);

        UP_FA_MedicationAllergies.Update();
        StdPlanning.UpdateListAllergy();
    }

    protected void NoRoutineMedication_Click(object sender, EventArgs e)
    {
        hfenableroutine.Value = "0";

        List<PatientRoutineMedication> pengobatan_nodata = new List<PatientRoutineMedication>();
        PatientRoutineMedication row_pengobatan = new PatientRoutineMedication();
        row_pengobatan.patient_routine_medication_id = Guid.Empty;
        row_pengobatan.medication = "Tidak Ada Pengobatan";
        row_pengobatan.routine_sales_item_code = "";
        row_pengobatan.routine_sales_item_id = 0;
        row_pengobatan.is_delete = 0;

        pengobatan_nodata.Add(row_pengobatan);

        DataTable dtpengobatan = Helper.ToDataTable(pengobatan_nodata);
        rptroutinemedication.DataSource = dtpengobatan;
        rptroutinemedication.DataBind();
        lblmodalnoroute.Style.Add("display", "none");
        StdPlanning.UpdateRoutineMedication(dtpengobatan);
        hdnhistoryroutine.Value = new JavaScriptSerializer().Serialize(pengobatan_nodata);
        Session[Helper.Sessionroutinemedication + hfguidadditional.Value] = pengobatan_nodata;

        UP_FA_MedicationAllergies.Update();
        StdPlanning.UpdateListRoutine();
    }

    protected void NoAllAllergy_Click(object sender, EventArgs e)
    {
        //initial no data
        List<PatientAllergy> drug_noalergi = new List<PatientAllergy>();
        PatientAllergy rowdrug = new PatientAllergy();
        rowdrug.patient_allergy_id = Guid.Empty;
        rowdrug.allergy_type = 1;
        rowdrug.allergy = "Tidak Ada Alergi";
        rowdrug.allergy_reaction = "Tidak Ada Reaksi";
        rowdrug.is_delete = 0;
        //end initial no data

        drug_noalergi.Add(rowdrug);
        rptdrugallergies.DataSource = Helper.ToDataTable(drug_noalergi);
        rptdrugallergies.DataBind();
        lblmodalnodrug.Style.Add("display", "none");
        StdPlanning.UpdateDrugAllergy(Helper.ToDataTable(drug_noalergi));
        hdnhistorydrugallergies.Value = new JavaScriptSerializer().Serialize(drug_noalergi);

        List<PatientAllergy> food_noalergi = new List<PatientAllergy>();
        PatientAllergy rowfood = new PatientAllergy();
        rowfood.patient_allergy_id = Guid.Empty;
        rowfood.allergy_type = 2;
        rowfood.allergy = "Tidak Ada Alergi";
        rowfood.allergy_reaction = "Tidak Ada Reaksi";
        rowfood.is_delete = 0;

        food_noalergi.Add(rowfood);
        rptfoodallergies.DataSource = Helper.ToDataTable(food_noalergi);
        rptfoodallergies.DataBind();
        lblmodalnofood.Style.Add("display", "none");
        StdPlanning.UpdateFoodAllergy(Helper.ToDataTable(food_noalergi));
        hdnhistoryfoodallergies.Value = new JavaScriptSerializer().Serialize(food_noalergi);

        List<PatientAllergy> other_noalergi = new List<PatientAllergy>();
        PatientAllergy rowother = new PatientAllergy();
        rowother.patient_allergy_id = Guid.Empty;
        rowother.allergy_type = 7;
        rowother.allergy = "Tidak Ada Alergi";
        rowother.allergy_reaction = "Tidak Ada Reaksi";
        rowother.is_delete = 0;

        other_noalergi.Add(rowother);
        rptotherallergies.DataSource = Helper.ToDataTable(other_noalergi);
        rptotherallergies.DataBind();
        lblmodalnoother.Style.Add("display", "none");
        StdPlanning.UpdateOtherAllergy(Helper.ToDataTable(other_noalergi));
        hdnhistoryotherallergies.Value = new JavaScriptSerializer().Serialize(other_noalergi);

        UP_FA_MedicationAllergies.Update();
        StdPlanning.UpdateListAllergy();
    }

    protected void ButtonGetBackup_Click(object sender, EventArgs e)
    {
        //SOAP Data = (SOAP)Session[Helper.Sessionsoapbackup + Request.QueryString["EncounterId"]];
        //AssignDataToSoapPage(Data);
        //ResultSOAP JsonSoap = (ResultSOAP)Session[Helper.SessionJsonsoapbackup + Request.QueryString["EncounterId"]];
        //JsonSoap.list = Data;
        //Session[Helper.SessionJsonsoapbackup + Request.QueryString["EncounterId"]] = JsonSoap;
        List<MarkerConfig> markerbackup = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
        markerbackup.Find(x => x.key == "BACKUPSOAPmarker").value = "true";
        Session[Helper.SESSIONmarker] = markerbackup;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SetConfirmbackup", "var getbackup = true;", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refresh", "window.location.reload();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "comparemodal", "$('#modalCompareSOAP').modal('hide');", true);
    }
    protected void ButtonGetOri_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "comparemodal", "$('#modalCompareSOAP').modal('hide');", true);
    }

    protected void btnEditPregnancy_Click(object sender, EventArgs e)
    {
        var soapmodel = MappingforGetdataSOAPSession();
        StdObgyn.initializevalue(soapmodel.pregnancy_data, soapmodel.pregnancy_history, soapmodel.objective);
        StdObgyn.UpdateModalObgyn();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PreviewObgyn", "$('#modalEditObgyn').modal({ backdrop: 'static', keyboard: false });", true);
    }

    bool submitObgyn(object sender)
    {
        var soapmodel = MappingforGetdataSOAPSession();
        soapmodel = StdObgyn.GetObgynValues(soapmodel);
        Session[Helper.Sessionsoapmodel + hfguidadditional.Value] = soapmodel;
        Obgyn_Load(soapmodel, true);

        return true;
    }

    protected void btnEditPediatric_Click(object sender, EventArgs e)
    {
        var soapmodel = MappingforGetdataSOAPSession();
        StdPediatric.initializevalue(soapmodel.pediatric_data);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PreviewPediatric", "$('#modalEditPediatric').modal({ backdrop: 'static', keyboard: false });", true);
    }

    bool submitPediatric(object sender)
    {
        var soapmodel = MappingforGetdataSOAPSession();
        soapmodel = StdPediatric.GetPediatricValues(soapmodel);
        Session[Helper.Sessionsoapmodel + hfguidadditional.Value] = soapmodel;
        Pediatric_Load(soapmodel, true);

        return true;
    }

    protected void ButtonToogleChkDrugs_Click(object sender, EventArgs e)
    {
        ToogleChkDrug();
    }

    public void ToogleChkDrug()
    {
        if (chkDrugs.Checked == false)
        {
            foreach (RepeaterItem itemnya in rptDrugs.Items)
            {
                CheckBox check_choose = (CheckBox)itemnya.FindControl("chkChooseDrugs");
                check_choose.Checked = false;
                check_choose.Enabled = false;
            }
        }
        else if (chkDrugs.Checked == true)
        {
            foreach (RepeaterItem itemnya in rptDrugs.Items)
            {
                CheckBox check_choose = (CheckBox)itemnya.FindControl("chkChooseDrugs");
                //check_choose.Checked = true;

                HiddenField HF_drugsisactiveCopySoap = (HiddenField)itemnya.FindControl("HF_drugsisactiveCopySoap");
                if (HF_drugsisactiveCopySoap.Value.ToLower() == "true")
                {
                    check_choose.Enabled = true;
                }

                //Label Lbl_Inactive = (Label)itemnya.FindControl("LblFlagDrugActive");
                //Lbl_Inactive.Visible = false;
                check_choose.Checked = true;
                
            }
        }
    }

    protected void ButtonShowKurva_Click(object sender, EventArgs e)
    {
        List<PediatricChart> JsongetsoapListPediatric = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];
        //StdKurvaPertumbuhan.Visible = true;
        StdKurvaPertumbuhan.UPChartTemplate();
        StdKurvaPertumbuhan.LoadChart(JsongetsoapListPediatric);
        
    }

    protected void btnSaveReferal_Click(object sender, EventArgs e)
    {
        try
        {
            List<ReferalData> referalDatas = ModalReferal.getvalues();
            DataTable dtreferalDatas = Helper.ToDataTable(referalDatas);



            if (dtreferalDatas.Select("speciality_id = 0").Count() > 0)
            {
                ShowToastr("Pilih Spesialis terlebih dahulu!", "Save Form Alert", "Warning");
                Session[Helper.SessionSOAPReferral + hfguidadditional.Value] = null;
                //DataTable dtreferal = Helper.ToDataTable(null);
                rptrujukan.DataSource = null;
                rptrujukan.DataBind();
                divrujukansoap.Visible = false;
                lblbhs_rujukan.Visible = false;
            }
            else
            {
                Session[Helper.SessionSOAPReferral + hfguidadditional.Value] = referalDatas;
                DataTable dtreferal = Helper.ToDataTable(referalDatas);
                rptrujukan.DataSource = dtreferal;
                rptrujukan.DataBind();
                divrujukansoap.Visible = true;
                lblbhs_rujukan.Visible = true;
                UpdatePanelRujukan.Update();
                ModalReferal.UpdatePanel();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseReferal", "$('#modal-referal').modal('hide');", true);
                //ModalReferal.DokterValidation(true);
            }



        }
        catch (Exception ex)
        {
            var msg = ex.Message;
        }

    }
    //protected void ButtonSAVECHART_Click(object sender, EventArgs e)
    //{
    //    List<PediatricChart> ListPediatricData = (List<PediatricChart>)Session[Helper.SessionDataDetailChart + hfguidadditional.Value];
    //    Session[Helper.SessionDataDetailChart_Save + hfguidadditional.Value] = ListPediatricData;

    //    foreach (PediatricChart PC in ListPediatricData)
    //    {
    //        if (PC.isNew == true)
    //        {
    //            PC.chart_transaction_master_id = Guid.Empty;
    //        }
    //    }

    //    var resultChart = clsSPPediatricSOAP.SubmitPediatricChart(ListPediatricData, Helper.organizationId, long.Parse(hfPatientId.Value), Guid.Parse(hfEncounterId.Value), long.Parse(hfAdmissionId.Value), long.Parse(Helper.GetDoctorID(this)));
    //    var JsonChart = (JObject)JsonConvert.DeserializeObject<dynamic>(resultChart.Result);
    //    //var Status = JsonChart.Property("status").Value.ToString();
    //    //var Message = JsonChart.Property("message").Value.ToString();
    //}

    [WebMethod]
    public static string LogMims(string flagg, string admid, string admno, string mrno, string patientname, string reasonarray, string txtreasonother)
    {
        MimsInteractionWithLog datamims = (MimsInteractionWithLog)HttpContext.Current.Session[Helper.SessionMimsResultData];

        LogMimsModel newdata = new LogMimsModel();

        newdata.LogHeader = new LogMimsHeaderModel();
        newdata.LogDetail = new List<LogMimsDetailModel>();

        newdata.LogHeader.LogMimsHeaderId = 0;
        newdata.LogHeader.LogDate = DateTime.Now;
        newdata.LogHeader.OrganizationId = long.Parse(MyUser.GetHopeOrgID());
        newdata.LogHeader.OrganizationName = MyUser.GetOrgName();
        newdata.LogHeader.Modul = "DOCTOR";
        newdata.LogHeader.UserName = MyUser.GetUsername();
        newdata.LogHeader.FullName = MyUser.GetFullname();
        newdata.LogHeader.AdmissionId = long.Parse(admid);
        newdata.LogHeader.AdmissionNo = admno;
        newdata.LogHeader.MrNo = mrno;
        newdata.LogHeader.Action = flagg;
        newdata.LogHeader.ContinueReason1 = reasonarray;
        newdata.LogHeader.ContinueReason2 = "";
        newdata.LogHeader.ContinueReasonOther = txtreasonother;
        newdata.LogHeader.IsLatest = true;
        newdata.LogHeader.CreatedBy = MyUser.GetUsername();
        newdata.LogHeader.CreatedDate = DateTime.Now;

        foreach (LogsInteraction log in datamims.logsInteraction)
        {
            LogMimsDetailModel baru = new LogMimsDetailModel();
            baru.LogMimsDetailId = 0;
            baru.LogMimsHeaderId = 0;
            baru.Interaction = log.interaction;
            baru.Severity = log.level;
            baru.Drug = log.prescribingDrug;
            baru.InteractingWith = log.interactingDrug;
            baru.IsLatest = true;
            baru.CreatedBy = MyUser.GetUsername();
            baru.CreatedDate = DateTime.Now;

            newdata.LogDetail.Add(baru);
        }

        /*
        foreach (DrugsInteraction di in datamims.drugsInteraction)
        {
            string Interaction = "";
            int level = 0;
            int x = 0;
            if (di.drugToAllergyInteraction == true)
            {
                if (di.drugToAllergySeverity != null && di.drugToAllergySeverity != "")
                {
                    x = int.Parse(di.drugToAllergySeverity.Split(',')[1]);
                    if (x > level)
                    {
                        level = x;
                    }
                }
                Interaction = Interaction + "drugToAllergyInteraction [" + x + "] ; ";
            }
            if (di.drugToDrugInteraction == true)
            {
                if (di.drugToDrugSeverity != null && di.drugToDrugSeverity != "")
                {
                    x = int.Parse(di.drugToDrugSeverity.Split(',')[1]);
                    if (x > level)
                    {
                        level = x;
                    }
                }
                Interaction = Interaction + "drugToDrugInteraction [" + x + "] ; ";
            }
            if (di.drugToHealthInteraction == true)
            {
                if (di.drugToHealthSeverity != null && di.drugToHealthSeverity != "")
                {
                    x = int.Parse(di.drugToHealthSeverity.Split(',')[1]);
                    if (x > level)
                    {
                        level = x;
                    }
                }
                Interaction = Interaction + "drugToHealthInteraction [" + x + "] ; ";
            }
            if (di.drugToLactationInteraction == true)
            {
                if (di.drugToLactationSeverity != null && di.drugToLactationSeverity != "")
                {
                    x = int.Parse(di.drugToLactationSeverity.Split(',')[1]);
                    if (x > level)
                    {
                        level = x;
                    }
                }
                Interaction = Interaction + "drugToLactationInteraction [" + x + "] ; ";
            }
            if (di.drugToPregnancyInteraction == true)
            {
                if (di.drugToPregnancySeverity != null && di.drugToPregnancySeverity != "")
                {
                    x = int.Parse(di.drugToPregnancySeverity.Split(',')[1]);
                    if (x > level)
                    {
                        level = x;
                    }
                }
                Interaction = Interaction + "drugToPregnancyInteraction [" + x + "] ; ";
            }
            if (di.duplicateIngredient == true)
            {
                if (di.duplicateIngredientSeverity != null && di.duplicateIngredientSeverity != "")
                {
                    x = int.Parse(di.duplicateIngredientSeverity.Split(',')[1]);
                    if (x > level)
                    {
                        level = x;
                    }
                }
                Interaction = Interaction + "duplicateIngredient [" + x + "] ; ";
            }
            if (di.duplicateTherapy == true)
            {
                if (di.duplicateTherapySeverity != null && di.duplicateTherapySeverity != "")
                {
                    x = int.Parse(di.duplicateTherapySeverity.Split(',')[1]);
                    if (x > level)
                    {
                        level = x;
                    }
                }
                Interaction = Interaction + "duplicateTherapy [" + x + "] ; ";
            }

            string severitycategory = "";
            if (level <= 2)
            {
                severitycategory = "LOW";
            }
            else if (level == 3)
            {
                severitycategory = "MEDIUM";
            }
            else if (level >= 4)
            {
                severitycategory = "HIGH";
            }


            LogMimsDetailModel baru = new LogMimsDetailModel();
            baru.LogMimsDetailId = 0;
            baru.LogMimsHeaderId = 0;
            baru.Interaction = Interaction;
            baru.Severity = severitycategory;
            baru.Drug = di.itemName;
            baru.Allergy = "";
            baru.IsLatest = true;
            baru.CreatedBy = MyUser.GetUsername();
            baru.CreatedDate = DateTime.Now;

            newdata.LogDetail.Add(baru);
        }
        */

        var postMims = clsMims.PostLogMims(newdata);
        var JsonPostMims = (JObject)JsonConvert.DeserializeObject<dynamic>(postMims.Result);
        var Status = JsonPostMims.Property("status").Value.ToString();
        var Message = JsonPostMims.Property("message").Value.ToString();

        //log4net.ThreadContext.Properties["Organization"] = "MIMS";
        //log4net.ThreadContext.Properties["Feature"] = "MIMS";

        //Log.Info(LogLibrary.SaveLogMims(admno,mrno,patientname,flagg));

        //log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        //log4net.ThreadContext.Properties["Feature"] = "-";

        return flagg;
    }

    public static string CheckLevel(int level)
    {
        string severitycategory = "";
        if (level <= 2)
        {
            severitycategory = "LOW";
        }
        else if (level == 3)
        {
            severitycategory = "MEDIUM";
        }
        else if (level >= 4)
        {
            severitycategory = "HIGH";
        }

        return severitycategory;
    }

    #region HEALTHRECORD

    public void GetDataHealthInfo(long OrganizationId, long PatientId, int StatusId, string EncounterId)
    {
        try
        {
            var jsondata = clsHealthInfo.GetHealthInfo(OrganizationId, PatientId, StatusId, EncounterId);
            var listdata = JsonConvert.DeserializeObject<ResponsePatientHealthInfo>(jsondata.Result);

            PatientHealthInfo data = new PatientHealthInfo();

            if (listdata.Status == "Success")
            {
                data = listdata.Data;
                InitSOAPMedicationAllergy(data, OrganizationId, PatientId, StatusId, EncounterId);
                InitSOAPHealthRecord(data, OrganizationId, PatientId, StatusId, EncounterId);

            }
            else if (listdata.Status == "Fail")
            {

            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Unexpected character encountered while parsing value: O. Path"))
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "connection", "alert('Please check your connection');", true);
            }
            else
            {
                throw ex;
            }
        }
    }

    public void InitSOAPMedicationAllergy(PatientHealthInfo data, long OrganizationId, long PatientId, int StatusId, string EncounterId)
    {
        PatientHealthInfo dataHI = new PatientHealthInfo();
        dataHI.organization_id = OrganizationId;
        dataHI.patient_id = PatientId;
        dataHI.status_id = StatusId;
        dataHI.encounter_id = Guid.Parse(EncounterId);

        StdPlanning.InitSOAPMedicationAllergy(dataHI, data);
        CollectListAllergyForMims(data);

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.drugs && x.is_active == true && x.is_waiting_delete == false).ToList());
        RptDrugAllergy_HI.DataSource = dataHI.list_info;
        RptDrugAllergy_HI.DataBind();
        if (dataHI.list_info.Count > 0) { lblmodalnodrug.Style.Add("display", "none"); } else { lblmodalnodrug.Style.Add("display", ""); }

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.foods && x.is_active == true && x.is_waiting_delete == false).ToList());
        RptFoodAllergy_HI.DataSource = dataHI.list_info;
        RptFoodAllergy_HI.DataBind();
        if (dataHI.list_info.Count > 0) { lblmodalnofood.Style.Add("display", "none"); } else { lblmodalnofood.Style.Add("display", ""); }

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.others && x.is_active == true && x.is_waiting_delete == false).ToList());
        RptOtherAllergy_HI.DataSource = dataHI.list_info;
        RptOtherAllergy_HI.DataBind();
        if (dataHI.list_info.Count > 0) { lblmodalnoother.Style.Add("display", "none"); } else { lblmodalnoother.Style.Add("display", ""); }

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.medications && x.is_active == true && x.is_waiting_delete == false).ToList());
        RptMedication_HI.DataSource = dataHI.list_info;
        RptMedication_HI.DataBind();
        if (dataHI.list_info.Count > 0) { lblmodalnoroute.Style.Add("display", "none"); } else { lblmodalnoroute.Style.Add("display", ""); }
    }

    public void InitSOAPHealthRecord(PatientHealthInfo data, long OrganizationId, long PatientId, int StatusId, string EncounterId)
    {
        PatientHealthInfo dataHI = new PatientHealthInfo();
        dataHI.organization_id = OrganizationId;
        dataHI.patient_id = PatientId;
        dataHI.status_id = StatusId;
        dataHI.encounter_id = Guid.Parse(EncounterId);

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.surgeryhistory && x.is_active == true && x.is_waiting_delete == false).ToList());
        RptSurgeryHistory_HI.DataSource = dataHI.list_info;
        RptSurgeryHistory_HI.DataBind();
        if (dataHI.list_info.Count > 0) { lblmodalnosurgery.Style.Add("display", "none"); } else { lblmodalnosurgery.Style.Add("display", ""); }

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.procedure && x.is_active == true && x.is_waiting_delete == false).ToList());
        RptProcedure_HI.DataSource = dataHI.list_info;
        RptProcedure_HI.DataBind();
        if (dataHI.list_info.Count > 0) { lblmodalnoprocout.Style.Add("display", "none"); } else { lblmodalnoprocout.Style.Add("display", ""); }

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.disease && x.is_active == true && x.is_waiting_delete == false).ToList());
        RptDisease_HI.DataSource = dataHI.list_info;
        RptDisease_HI.DataBind();
        if (dataHI.list_info.Count > 0) { lblmodalnodisease.Style.Add("display", "none"); } else { lblmodalnodisease.Style.Add("display", ""); }
        setStickerDisease(dataHI.list_info);

        dataHI.list_info = new List<InfoHealth>();
        dataHI.list_info.AddRange(data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.familydisease && x.is_active == true && x.is_waiting_delete == false).ToList());
        RptFamilyDisease_HI.DataSource = dataHI.list_info;
        RptFamilyDisease_HI.DataBind();
        if (dataHI.list_info.Count > 0) { lblmodalnofamdisease.Style.Add("display", "none"); } else { lblmodalnofamdisease.Style.Add("display", ""); }
    }

    public void SetDataMedicationAllergies()
    {
        PatientHealthInfo data = new PatientHealthInfo();
        data = JsonConvert.DeserializeObject<PatientHealthInfo>(HF_CollectedData_MA.Value);

        StdPlanning.UpdateSOAPMedicationAllergy(data);
        CollectListAllergyForMims(data);

        List<InfoHealth> alldrug = data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.drugs && x.is_active == true && x.is_waiting_delete == false).ToList();
        RptDrugAllergy_HI.DataSource = alldrug;
        RptDrugAllergy_HI.DataBind();
        if (alldrug.Count > 0) { lblmodalnodrug.Style.Add("display", "none"); } else { lblmodalnodrug.Style.Add("display", ""); }
        //StdPlanning.UpdateDrugAllergy_HI(alldrug);

        List<InfoHealth> allfood = data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.foods && x.is_active == true && x.is_waiting_delete == false).ToList();
        RptFoodAllergy_HI.DataSource = allfood;
        RptFoodAllergy_HI.DataBind();
        if (allfood.Count > 0) { lblmodalnofood.Style.Add("display", "none"); } else { lblmodalnofood.Style.Add("display", ""); }
        //StdPlanning.UpdateFoodAllergy_HI(allfood);

        List<InfoHealth> allother = data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.others && x.is_active == true && x.is_waiting_delete == false).ToList();
        RptOtherAllergy_HI.DataSource = allother;
        RptOtherAllergy_HI.DataBind();
        if (allother.Count > 0) { lblmodalnoother.Style.Add("display", "none"); } else { lblmodalnoother.Style.Add("display", ""); }
        //StdPlanning.UpdateOtherAllergy_HI(allother);

        List<InfoHealth> medmed = data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.medications && x.is_active == true && x.is_waiting_delete == false).ToList();
        RptMedication_HI.DataSource = medmed;
        RptMedication_HI.DataBind();
        if (medmed.Count > 0) { lblmodalnoroute.Style.Add("display", "none"); } else { lblmodalnoroute.Style.Add("display", ""); }
        //StdPlanning.UpdateMedication_HI(medmed);

        UP_FA_MedicationAllergies.Update();
    }

    public void SetDataHealthRecord()
    {
        PatientHealthInfo data = new PatientHealthInfo();
        data = JsonConvert.DeserializeObject<PatientHealthInfo>(HF_CollectedData_HR.Value);

        List<InfoHealth> allsurgery = data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.surgeryhistory && x.is_active == true && x.is_waiting_delete == false).ToList();
        RptSurgeryHistory_HI.DataSource = allsurgery;
        RptSurgeryHistory_HI.DataBind();
        if (allsurgery.Count > 0) { lblmodalnosurgery.Style.Add("display", "none"); } else { lblmodalnosurgery.Style.Add("display", ""); }

        List<InfoHealth> allprocedure = data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.procedure && x.is_active == true && x.is_waiting_delete == false).ToList();
        RptProcedure_HI.DataSource = allprocedure;
        RptProcedure_HI.DataBind();
        if (allprocedure.Count > 0) { lblmodalnoprocout.Style.Add("display", "none"); } else { lblmodalnoprocout.Style.Add("display", ""); }

        List<InfoHealth> alldisease = data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.disease && x.is_active == true && x.is_waiting_delete == false).ToList();
        RptDisease_HI.DataSource = alldisease;
        RptDisease_HI.DataBind();
        if (alldisease.Count > 0) { lblmodalnodisease.Style.Add("display", "none"); } else { lblmodalnodisease.Style.Add("display", ""); }

        List<InfoHealth> allfamdisease = data.list_info.Where(x => x.health_info_type_id == CstHealthInfoType.familydisease && x.is_active == true && x.is_waiting_delete == false).ToList();
        RptFamilyDisease_HI.DataSource = allfamdisease;
        RptFamilyDisease_HI.DataBind();
        if (allfamdisease.Count > 0) { lblmodalnofamdisease.Style.Add("display", "none"); } else { lblmodalnofamdisease.Style.Add("display", ""); }

        UP_FA_HealthRecord.Update();

        setStickerDisease(alldisease);
        up_sticker_HI.Update();
    }

    public void setStickerDisease(List<InfoHealth> alldisease)
    {
        Button_HepBStickeroff_HI.Visible = true;
        Button_HepBStickeron_HI.Visible = false;
        Button_HepCStickeroff_HI.Visible = true;
        Button_HepCStickeron_HI.Visible = false;
        Button_TbcStickeroff_HI.Visible = true;
        Button_TbcStickeron_HI.Visible = false;
        Button_HadStickeroff_HI.Visible = true;
        Button_HadStickeron_HI.Visible = false;
        Button_PrtStickeroff_HI.Visible = true;
        Button_PrtStickeron_HI.Visible = false;
        Button_RhnStickeroff_HI.Visible = true;
        Button_RhnStickeron_HI.Visible = false;
        Button_MrsStickeroff_HI.Visible = true;
        Button_MrsStickeron_HI.Visible = false;
        Button_CvStickeroff_HI.Visible = true;
        Button_CvStickeron_HI.Visible = false;

        foreach (InfoHealth x in alldisease)
        {
            if (x.health_info_value.ToLower() == CstDiseaseType_V.Hepatitis_B.ToLower() && x.health_info_status.ToLower() == CstDiseaseConditionType.Belum_Sembuh.ToLower())
            {
                Button_HepBStickeroff_HI.Visible = false;
                Button_HepBStickeron_HI.Visible = true;
            }
            else if (x.health_info_value.ToLower() == CstDiseaseType_V.Hepatitis_C.ToLower() && x.health_info_status.ToLower() == CstDiseaseConditionType.Belum_Sembuh.ToLower())
            {
                Button_HepCStickeroff_HI.Visible = false;
                Button_HepCStickeron_HI.Visible = true;
            }
            else if (x.health_info_value.ToLower() == CstDiseaseType_V.TBC.ToLower() && x.health_info_status.ToLower() == CstDiseaseConditionType.Belum_Sembuh.ToLower())
            {
                Button_TbcStickeroff_HI.Visible = false;
                Button_TbcStickeron_HI.Visible = true;
            }
            else if (x.health_info_value.ToLower() == CstDiseaseType_V.HAD.ToLower() && x.health_info_status.ToLower() == CstDiseaseConditionType.Belum_Sembuh.ToLower())
            {
                Button_HadStickeroff_HI.Visible = false;
                Button_HadStickeron_HI.Visible = true;
            }
            else if (x.health_info_value.ToLower() == CstDiseaseType_V.PRT.ToLower() && x.health_info_status.ToLower() == CstDiseaseConditionType.Belum_Sembuh.ToLower())
            {
                Button_PrtStickeroff_HI.Visible = false;
                Button_PrtStickeron_HI.Visible = true;
            }
            else if (x.health_info_value.ToLower() == CstDiseaseType_V.RHN.ToLower() && x.health_info_status.ToLower() == CstDiseaseConditionType.Belum_Sembuh.ToLower())
            {
                Button_RhnStickeroff_HI.Visible = false;
                Button_RhnStickeron_HI.Visible = true;
            }
            else if (x.health_info_value.ToLower() == CstDiseaseType_V.MRS.ToLower() && x.health_info_status.ToLower() == CstDiseaseConditionType.Belum_Sembuh.ToLower())
            {
                Button_MrsStickeroff_HI.Visible = false;
                Button_MrsStickeron_HI.Visible = true;
            }
            else if (x.health_info_value.ToLower() == CstDiseaseType_V.CV.ToLower() && x.health_info_status.ToLower() == CstDiseaseConditionType.Belum_Sembuh.ToLower())
            {
                Button_CvStickeroff_HI.Visible = false;
                Button_CvStickeron_HI.Visible = true;
            }
        }
    }

    public void CollectListAllergyForMims(PatientHealthInfo data)
    {
        List<InfoHealth> allallergy = data.list_info.Where(x => (x.health_info_type_id == CstHealthInfoType.drugs || x.health_info_type_id == CstHealthInfoType.foods || x.health_info_type_id == CstHealthInfoType.others) && x.is_active == true && x.is_waiting_delete == false).ToList();
        List<Int64> ListIdAllergy = new List<Int64>();
        foreach (InfoHealth x in allallergy)
        {
            ListIdAllergy.Add(x.external_master_data_id);
        }
        Session[CstSession.SessionListAllergy_HI] = ListIdAllergy;

        List<InfoHealth> allroutine = data.list_info.Where(x => (x.health_info_type_id == CstHealthInfoType.medications) && x.is_active == true && x.is_waiting_delete == false).ToList();
        List<Int64> ListIdRoutine = new List<Int64>();
        foreach (InfoHealth x in allroutine)
        {
            ListIdRoutine.Add(x.external_master_data_id);
        }
        Session[CstSession.SessionListRoutine_HI] = ListIdRoutine;
    }

    public void GetDataIframe(long OrganizationId, long PatientId, int StatusId, string EncounterId, long userid)
    {
        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();
        string baseURLhttp = "http://" + localIPAdress + "/healthrecord";
        string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_HealthRecord"];

        string url_MA = baseURLhttps + "/Pages/FormViewer/MedicationAllergies.aspx?OrgID=" + OrganizationId + "&PtnID=" + PatientId + "&UsrID=" + userid + "&SttID=" + StatusId + "&EncID=" + EncounterId;
        IframeMedication.Src = url_MA;

        string url_HR = baseURLhttps + "/Pages/FormViewer/HealthRecord.aspx?OrgID=" + OrganizationId + "&PtnID=" + PatientId + "&UsrID=" + userid + "&SttID=" + StatusId + "&EncID=" + EncounterId;
        IframeHealthrecord.Src = url_HR;
    }

    protected void BtnSaveDrugAllergy_HI_Click(object sender, EventArgs e)
    {
        SetDataMedicationAllergies();
    }

    protected void BtnSaveHealthRecord_HI_Click(object sender, EventArgs e)
    {
        SetDataHealthRecord();
    }

    #endregion

    #region referal
    protected void BtnReferalHidden_Click(object sender, EventArgs e)
    {
        List<ReferalData> referalDatas = (List<ReferalData>)Session[Helper.SessionSOAPReferral + hfguidadditional.Value];
        ModalReferal.initializevalue(referalDatas);
    }
    protected void BtnDeleteAllReferral_Click(object sender, EventArgs e)
    {

        List<ReferalData> referalDatas = (List<ReferalData>)Session[Helper.SessionSOAPReferral + hfguidadditional.Value];
        DataTable dt = Helper.ToDataTable(referalDatas);
        //var cekdt = dt.Select("is_editable = 0");
        if (dt.Select("is_editable = 0") != null)
        {
            referalDatas.Clear();
            List<ReferalData> referalDataemp = new List<ReferalData>();
            Session[Helper.SessionSOAPReferral + hfguidadditional.Value] = referalDataemp;


            if (referalDataemp.Count <= 0)
            {
                //DataTable dtreferal = Helper.ToDataTable(referalDataemp);
                rptrujukan.DataSource = null;
                rptrujukan.DataBind();
            }
            else
            {
                DataTable dtreferal = Helper.ToDataTable(referalDataemp);
                rptrujukan.DataSource = dtreferal;
                rptrujukan.DataBind();
            }
            divrujukansoap.Visible = false;
            UpdatePanelRujukan.Update();

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Rujukan tidak bisa terhaous semua ada, yang aproved');", true);

        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseReferal", "$('#modal-delete-referal').modal('hide');", true);

    }
    protected void BtnEditReferral_Click(object sender, EventArgs e)
    {
        //is editb 1 => masih bisa di edit
    }
    protected void BtnDeleteReferral_Click(object sender, EventArgs e)
    {
        try
        {
            int selRowIndex = ((RepeaterItem)(((LinkButton)sender).Parent)).ItemIndex;

            List<ReferalData> referalDatas = (List<ReferalData>)Session[Helper.SessionSOAPReferral + hfguidadditional.Value];

            DataTable dt = Helper.ToDataTable(referalDatas);

            dt.Rows[selRowIndex].SetField("is_delete", 1);
            //DataTable dt = Session["presdrug"] as DataTable;

            if (dt.Select("is_delete = 0").Count() > 0)
            {
                Session[Helper.SessionSOAPReferral + hfguidadditional.Value] = Helper.ToDataList<ReferalData>(dt.Select("is_delete = 0").CopyToDataTable());//convert kr list
                rptrujukan.DataSource = dt.Select("is_delete = 0").CopyToDataTable();
                rptrujukan.DataBind();
            }
            else
            {
                referalDatas.Clear();
                Session[Helper.SessionConsumablesList + hfguidadditional.Value] = referalDatas;
                rptrujukan.DataSource = referalDatas;
                rptrujukan.DataBind();
                divrujukansoap.Visible = false;
                //lblbhs_rujukan.Visible = false;
                BtnDeleteAllReferral.Visible = false;
                BtnEditReferral.Visible = false;
            }
            UpdatePanelRujukan.Update();
            ModalReferal.UpdatePanel();
        }
        catch (Exception ex)
        {

            string msg = ex.Message;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Connection Time Out. Please Try Again');", true);
        }

    }
    #endregion

    #region rawat inap
    protected void BtnSaveRawatinap_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            InpatientData inpatientData = ModalRawatInap.getvalues();
            var inpatientprm = new JavaScriptSerializer().Serialize(inpatientData);

            var saveinpatient = clsSOAP.SaveInpatientData(inpatientData);
            var Jsongetinpatient = (JObject)JsonConvert.DeserializeObject<dynamic>(saveinpatient.Result);
            var Status = Jsongetinpatient.Property("status").Value.ToString();
            var Message = Jsongetinpatient.Property("message").Value.ToString();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseRawatinap", "$('#modal-rawatinap').modal('hide');", true);

            Session[Helper.SessionSOAPRawatInap + hfguidadditional.Value] = inpatientData;
            div_rawatinap.Visible = true;

            lbl_rawatinap_dokter.Text = inpatientData.doctor_name;
            lbl_rawatinap_spesialis.Text = lbl_rawatinap_spesialis.Text = inpatientData.spesialis_dokter;
            lbl_rawatinap_waktu.Text = inpatientData.operation_schedule_header.created_date;
            lbl_rawatinap_status.Text = inpatientData.operation_schedule_header.status_booking_name;
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetInpatientData", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));

        }



       


    }
    protected void BtnPrintRawatInap(object sender, EventArgs e)
    {

        var localIPAdress = "";
        localIPAdress = GetLocalIPAddress();
        //localIPAdress = "10.83.254.38"; //HARD CODE
        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=RawatInap&OrganizationId=" + Helper.organizationId + "&AdmissionId=" + hfAdmissionId.Value.ToString() + "&EncounterId=" + hfEncounterId.Value.ToString() + "&PatientId=" + hfPatientId.Value.ToString() + "&PageSOAP=" + hfPageSoapId.Value.ToString() + "&PrintBy=" + Helper.GetLoginUser(this) + "','_blank');", true);

    }
    protected void BtnDeleteRawatInap_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        try
        {
            InpatientData inpatientData = (InpatientData)Session[Helper.SessionSOAPRawatInap + hfguidadditional.Value];

            InpatientDeleteParam paramdelete = new InpatientDeleteParam();
            paramdelete.admission_no = inpatientData.operation_schedule_header.admission_no;
            paramdelete.encounter_id = Guid.Parse(inpatientData.encounter_id.ToString());
            paramdelete.operation_schedule_id = Guid.Parse(inpatientData.operation_schedule_id.ToString());
            paramdelete.organization_id = Convert.ToInt64(inpatientData.operation_schedule_header.organization_id);
            paramdelete.patient_id = Convert.ToInt64(inpatientData.patient_id);
            paramdelete.user_id = Convert.ToInt64(MyUser.GetHopeUserID());
            paramdelete.is_from_opd = inpatientData.operation_schedule_header.is_from_opd;

            var cancelinpatient = clsSOAP.InpatientCancel(paramdelete);
            var Jsoncancelinpatient = (JObject)JsonConvert.DeserializeObject<dynamic>(cancelinpatient.Result);
            var Status = Jsoncancelinpatient.Property("status").Value.ToString();
            var Message = Jsoncancelinpatient.Property("message").Value.ToString();
            div_rawatinap.Visible = true;

            lbl_rawatinap_dokter.Text = inpatientData.doctor_name;
            lbl_rawatinap_spesialis.Text = inpatientData.spesialis_dokter;
            lbl_rawatinap_waktu.Text = inpatientData.operation_schedule_header.created_date;
            lbl_rawatinap_status.Text = inpatientData.operation_schedule_header.status_booking_name;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseDeleteAllInpatient", "$('#modal-delete-rawatinap').modal('hide');", true);

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "Delete ", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));

        }



    }


    #endregion
}