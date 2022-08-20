using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_General_OrderSet_OrderSetDetail : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable frequencydt = new DataTable();
    public static DataTable routedt = new DataTable();
    public static DataTable uomdt = new DataTable();
    public static DataTable doseUomdt = new DataTable();

    void setCompound()
    {
        if (Helper.GetFlagCompound(this) == "FALSE")
            typeOrderSet.Items.FindByValue("1").Enabled = false;
        else
            typeOrderSet.Items.FindByValue("1").Enabled = true;
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

        if (Helper.GetLoginUser(this) == null)
        {
            Response.Redirect("~/Form/General/Login.aspx", true);
            Context.ApplicationInstance.CompleteRequest();
        }
        if (Helper.GetDoctorID(this) == "")
        {
            Response.Redirect("~/Form/General/Login.aspx", true);
            Context.ApplicationInstance.CompleteRequest();
        }
        else
        {
            if (Session[Helper.SessionLanguage] == null)
                Session[Helper.SessionLanguage] = 1;

            if (Session[Helper.SessionLanguage].ToString() == "1")
            {
                setENG = "";
                setIND = "none";
                HFisBahasa.Value = "ENG";
                //typeOrderSet.Items[0].Text = "Drugs";
                //typeOrderSet.Items[1].Text = "Compound";
                //typeOrderSet.Items[2].Text = "Laboratory";
            }
            else if (Session[Helper.SessionLanguage].ToString() == "2")
            {
                setENG = "none";
                setIND = "";
                HFisBahasa.Value = "IND";
                //typeOrderSet.Items[0].Text = "Obat";
                //typeOrderSet.Items[1].Text = "Racikan";
                //typeOrderSet.Items[2].Text = "Laboratorium";
            }
            else
            {
                setENG = "";
                setIND = "none";
                HFisBahasa.Value = "ENG";
                //typeOrderSet.Items[0].Text = "Drugs";
                //typeOrderSet.Items[1].Text = "Compound";
                //typeOrderSet.Items[2].Text = "Laboratory";
            }

            //set bahasa
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "bahasa", "switchBahasa();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "changeView", "changeView();", true);


            if (!IsPostBack)
            {
                //Log.Info(LogConfig.LogStart());

                if (Session[Helper.SESSIONmarker] == null)
                {
                    Session[Helper.SESSIONmarker] = Helper.setInitializeMarker();
                }

                //setCompound();
                log4net.Config.XmlConfigurator.Configure();

                var nameOrderSet = Request.QueryString["nameOrderSet"];
                var isCompound = Request.QueryString["isCompound"];
                var type = Request.QueryString["type"];
                var orderSetId = Request.QueryString["orderSetId"];

                if (Helper.organizationId != 0)
                {

                    Session.Remove(Helper.SessionDrug);
                    Session.Remove(Helper.ViewStateDrug);
                    Session.Remove(Helper.ViewStateCompound);

                    initUOM();

                    DataTable dt = new DataTable();
                    dt = clsOrderSet.GetItemLite();
                    
                    //bisa dibuka lagi jika ingin menggunakan metode pencarian yang sebelumnya
                    //gvw_data.DataSource = dt.Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
                    //gvw_data.DataBind();
                    //GridViewXX.DataSource = dt.Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
                    //GridViewXX.DataBind();

                    Session[Helper.SessionDrug] = dt;

                    if (nameOrderSet != null)
                    {
                        header_form.InnerHtml = (new StringBuilder().Append("<h4>Edit Order Set</h4>")).ToString();
                        if (type == "2")
                        {
                            //txt_search.Visible = false;
                            //orderSet_content.Style.Add("background-color", "transparent");
                            //save_orderSet.Style.Add("background-color", "transparent");

                            List<CpoeMapping> listOrderSet = new List<CpoeMapping>();

                            try
                            {
                                Dictionary<string, string> logParam = new Dictionary<string, string>
                                {
                                    { "Orderset_Name", nameOrderSet },
                                    { "Org_ID", Helper.organizationId.ToString() },
                                    { "Doctor_ID", Helper.GetDoctorID(this) }
                                };
                                //Log.Debug(LogConfig.LogStart("getDrugsbyLaboratorySetName", logParam));
                                var dataOrderSet = clsOrderSet.getDrugsbyLaboratorySetName(nameOrderSet, Helper.organizationId.ToString(), long.Parse(Helper.GetDoctorID(this)));
                                var JsonOrderSet = JsonConvert.DeserializeObject<ResultMapping>(dataOrderSet.Result.ToString());
                                string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Orderset_Name", nameOrderSet, "Page_Load", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataOrderSet.Result.ToString()));
                                //Log.Debug(LogConfig.LogEnd("getDrugsbyLaboratorySetName", JsonOrderSet.Status, JsonOrderSet.Message));
                                listOrderSet = JsonOrderSet.list;
                                
                            }
                            catch (Exception ex)
                            {
                                string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Orderset_Name", nameOrderSet, "Page_Load", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                                //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
                            }

                            List<CpoeMapping> listChecked = listOrderSet.FindAll(x => x.is_checked.Equals(1));

                            var listClinicChecked = listChecked.FindAll(x => x.type == ("ClinicLab"))
                                .Select(x => new CpoeTrans
                                {
                                    id = x.item_id,
                                    name = x.item_name,
                                    type = x.type,
                                    isnew = 0,
                                    iscito = 0,
                                    issubmit = 0,
                                    isdelete = 0,
                                    ischeck = x.is_checked
                                });

                            var listMicroChecked = listChecked.FindAll(x => x.type == ("MicroLab"))
                                .Select(x => new CpoeTrans
                                {
                                    id = x.item_id,
                                    name = x.item_name,
                                    type = x.type,
                                    isnew = 0,
                                    iscito = 0,
                                    issubmit = 0,
                                    isdelete = 0,
                                    ischeck = x.is_checked
                                });

                            var listPatologiChecked = listChecked.FindAll(x => x.type == ("PatologiLab"))
                                .Select(x => new CpoeTrans
                                {
                                    id = x.item_id,
                                    name = x.item_name,
                                    type = x.type,
                                    isnew = 0,
                                    iscito = 0,
                                    issubmit = 0,
                                    isdelete = 0,
                                    ischeck = x.is_checked
                                });

                            var listPanelChecked = listChecked.FindAll(x => x.type == ("PanelLab"))
                                .Select(x => new CpoeTrans
                                {
                                    id = x.item_id,
                                    name = x.item_name,
                                    type = x.type,
                                    isnew = 0,
                                    iscito = 0,
                                    issubmit = 0,
                                    isdelete = 0,
                                    ischeck = x.is_checked
                                });

                            var listMDCChecked = listChecked.FindAll(x => x.type == ("MDCLab"))
                                .Select(x => new CpoeTrans
                                {
                                    id = x.item_id,
                                    name = x.item_name,
                                    type = x.type,
                                    isnew = 0,
                                    iscito = 0,
                                    issubmit = 0,
                                    isdelete = 0,
                                    ischeck = x.is_checked
                                });

                            StdClinicControl.GetMappingClinicLab(listOrderSet, listClinicChecked);
                            StdMicroLabControl.GetMappingMicroLab(listOrderSet, listMicroChecked);
                            StdAnatomiLab.GetMappingAnatomiLab(listOrderSet, listPatologiChecked);
                            StdPanelLab.GetMappingPanelLab(listOrderSet, listPanelChecked);
                            StdMDCLabControl.GetMappingMDCLab(listOrderSet, listMDCChecked);
                        }
                        //txt_header.Value = "Edit Order Set";
                        //header_form.Visible = false;
                        //ddlOrderset_Type.Visible = false;
                        txtNewOrderSet_Name.Text = nameOrderSet;

                        getUpdateFormOrderSet(nameOrderSet, isCompound, type, orderSetId);
                    }
                    else
                    {
                        ddlOrderset_Type.Style.Add("display", "block");

                        header_form.InnerHtml = (new StringBuilder().Append("<h4>Create Order Set</h4>")).ToString();
                        List<gridItem> data = new List<gridItem>();
                        DataTable dts = Helper.ToDataTable(data);
                        drugsGrid.DataSource = dts;
                        drugsGrid.DataBind();
                        compoundGrid.DataSource = dts;
                        compoundGrid.DataBind();

                        //Log.Debug(LogConfig.LogStart("getlistItemLaboratory", LogConfig.LogParam("Org_Id", Helper.organizationId.ToString())));
                        var dataOrderSet = clsOrderSet.getlistItemLaboratory(Helper.organizationId.ToString());
                        var JsonOrderSet = JsonConvert.DeserializeObject<ResultMapping>(dataOrderSet.Result.ToString());
                        
                        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "organizationId", Helper.organizationId.ToString(), "Page_Load", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataOrderSet.Result.ToString()));
                        //Log.Debug(LogConfig.LogEnd("getlistItemLaboratory", JsonOrderSet.Status, JsonOrderSet.Message));
                        List<CpoeMapping> listOrderSet = new List<CpoeMapping>();

                        listOrderSet = JsonOrderSet.list;

                        StdClinicControl.GetMappingClinicLab(listOrderSet, null);
                        StdMicroLabControl.GetMappingMicroLab(listOrderSet, null);
                        StdAnatomiLab.GetMappingAnatomiLab(listOrderSet, null);
                        StdPanelLab.GetMappingPanelLab(listOrderSet, null);
                        StdMDCLabControl.GetMappingMDCLab(listOrderSet, null);

                    }
                }
                else
                {
                    Response.Redirect("~/Form/General/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                //Log.Info(LogConfig.LogEnd());
            }

            Session[Helper.ViewStateOrderSetType] = typeOrderSet.SelectedValue;
            //if (typeOrderSet.SelectedValue == "1")
            //{
            //    compoundView.Visible = true;
            //    compoundGrid.Visible = true;
            //    drugsGrid.Visible = false;
            //    frmLaboratory.Visible = false;
            //    txt_search.Visible = false;
            //}
        }
    }

    //protected void fillICD(string srcKey)
    //{
    //    DataTable vardiseaseclassification = clsSOAP.getDiseaseClassification(srcKey);
    //    vardiseaseclassification.DefaultView.Sort = "DiseaseClassification";
    //    dataICD.DataSource = vardiseaseclassification;
    //    dataICD.DataBind();
    //}

    public void initUOM()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        /*Frequency */
        if (frequencydt.Rows.Count == 0)
        {
            List<Frequency> listfrequency = new List<Frequency>();

            try
            {
                StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //Log.Debug(LogConfig.LogStart("getFrequency"));
                var frequencyData = clsOrderSet.getFrequency();
                var Jsonfrequency = JsonConvert.DeserializeObject<ResultFrequency>(frequencyData.Result.ToString());

                string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "getFrequency", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", frequencyData.Result.ToString()));
                //Log.Debug(LogConfig.LogEnd("getFrequency", Jsonfrequency.Status, Jsonfrequency.Message));
                listfrequency = Jsonfrequency.list;
                frequencydt = Helper.ToDataTable(listfrequency);
            }
            catch (Exception ex)
            {
                string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "getFrequency", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            }
        }

        /*Administration Route */
        if (routedt.Rows.Count == 0)
        {
            List<AdministrationRoute> listadministrationRoute = new List<AdministrationRoute>();
            try
            {
                StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //Log.Debug(LogConfig.LogStart("getAdministrationRoute"));
                var administrationRouteData = clsOrderSet.getAdministrationRoute();
                var JsonadministrationRoute = JsonConvert.DeserializeObject<ResultAdministrationRoute>(administrationRouteData.Result.ToString());

                string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "getAdministrationRoute", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", administrationRouteData.Result.ToString()));
                //Log.Debug(LogConfig.LogEnd("getAdministrationRoute", JsonadministrationRoute.Status, JsonadministrationRoute.Message));
                listadministrationRoute = JsonadministrationRoute.list;
                routedt = Helper.ToDataTable(listadministrationRoute);

            }
            catch (Exception ex)
            {
                string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "getAdministrationRoute", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            }
        }

        /*UOM */
        if (uomdt.Rows.Count == 0)
        {
            List<UOM> listUOM = new List<UOM>();

            List<AdministrationRoute> listadministrationRoute = new List<AdministrationRoute>();
            try
            {
                StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                // Log.Debug(LogConfig.LogStart("getUOM"));
                var uomData = clsOrderSet.getUOM();
                var JsonUOM = JsonConvert.DeserializeObject<ResultUOM>(uomData.Result.ToString());
                
                string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "getUOM", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", uomData.Result.ToString()));
                //Log.Debug(LogConfig.LogEnd("getUOM", JsonUOM.Status, JsonUOM.Message));
                listUOM = JsonUOM.list;
                uomdt = Helper.ToDataTable(listUOM);
            }
            catch (Exception ex)
            {
                string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "getUOM", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            }
        }

        /*Dose UOM */
        if (doseUomdt.Rows.Count == 0)
        {
            List<Dose> listdoseUom = new List<Dose>();

            try
            {
                StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //Log.Debug(LogConfig.LogStart("getDose"));
                var doseUomData = clsOrderSet.getDose();
                var JsondoseUom = JsonConvert.DeserializeObject<ResultDose>(doseUomData.Result.ToString());

                string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "getDose", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", doseUomData.Result.ToString()));
                //Log.Debug(LogConfig.LogEnd("getDose", JsondoseUom.Status, JsondoseUom.Message));
                listdoseUom = JsondoseUom.list;
                doseUomdt = Helper.ToDataTable(listdoseUom);
            }
            catch (Exception ex)
            {
                string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "getDose", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            }
        }

        /*Dose UOM*/
        ddlDoseUom_header.DataSource = doseUomdt;
        ddlDoseUom_header.DataTextField = "name";
        ddlDoseUom_header.DataValueField = "doseUomId";
        ddlDoseUom_header.DataBind();
        ddlDoseUom_header.Items.Insert(0, new ListItem("-", "0"));

        /*Frequency*/
        ddlFrequency_header.DataSource = frequencydt;
        ddlFrequency_header.DataTextField = "name";
        ddlFrequency_header.DataValueField = "administrationFrequencyId";
        ddlFrequency_header.DataBind();
        ddlFrequency_header.Items.Insert(0, new ListItem("-", "0"));

        /*Administration Route*/
        ddlRoute_header.DataSource = routedt;
        ddlRoute_header.DataTextField = "name";
        ddlRoute_header.DataValueField = "administration_route_id";
        ddlRoute_header.DataBind();
        ddlRoute_header.Items.Insert(0, new ListItem("-", "0"));

        /*UOM */
        ddlUOM_header.DataSource = uomdt;
        ddlUOM_header.DataTextField = "name";
        ddlUOM_header.DataValueField = "uomId";
        ddlUOM_header.DataBind();
        ddlUOM_header.Items.Insert(0, new ListItem("-", "0"));
    }

    protected void getUpdateFormOrderSet(string nameOrderSet, string compound, string type, string orderSetId)
    {
        //Log.Info(LogConfig.LogStart());
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            if (compound == "0")
            {
                if (type == "0")
                {
                    typeOrderSet.SelectedValue = "0";
                    //tblDrug.Style.Remove("display");
                    //order_set_content.Style.Add("height", "calc(100vh - 100px)");
                    List<OrderSetDetail> listOrderSet = new List<OrderSetDetail>();

                    try
                    {
                        Dictionary<string, string> logParam = new Dictionary<string, string>
                        {
                            { "Orderset_Name", nameOrderSet },
                            { "Doctor_ID", Helper.GetDoctorID(this) }
                        };
                        //Log.Debug(LogConfig.LogStart("getDrugbyOrderSetName", logParam));
                        var dataOrderSet = clsOrderSet.getDrugbyOrderSetName(nameOrderSet, long.Parse(Helper.GetDoctorID(this)));
                        var JsonOrderSet = JsonConvert.DeserializeObject<ResultOrderSetDetail>(dataOrderSet.Result.ToString());

                        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Orderset_Name", nameOrderSet, "getUpdateFormOrderSet", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataOrderSet.Result.ToString()));
                        //Log.Debug(LogConfig.LogEnd("getDrugbyOrderSetName", JsonOrderSet.Status, JsonOrderSet.Message));
                        listOrderSet = JsonOrderSet.list;
                        
                    }
                    catch (Exception ex)
                    {
                        string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Orderset_Name", nameOrderSet, "getUpdateFormOrderSet", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                        //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
                    }

                    List<gridItem> data = new List<gridItem>();
                    txt_orderHeaderId.Value = listOrderSet[0].order_set_id;
                    foreach (OrderSetDetail grid in listOrderSet)
                    {
                        gridItem row = new gridItem();
                        row.salesItemId = grid.item_id;
                        row.salesItemName = grid.item_name;
                        row.doze = grid.dosage_id;
                        row.dozeuomId = grid.dose_uom_id;
                        row.dose_uom = grid.dose_uom;
                        row.frequencyId = grid.frequency_id;
                        row.frequency = grid.frequency_code;
                        row.routeId = grid.administration_route_id;
                        row.route = grid.administration_route_code;
                        row.instruction = grid.remarks;
                        row.quantity = grid.quantity;
                        row.uomId = grid.uomId;
                        row.uom = grid.uom_code;
                        row.iteration = grid.Iteration;
                        row.order_set_detail_id = grid.order_set_detail_id;
                        row.formularium = grid.formularium;
                        row.IsDoseTextDetail = grid.isDoseTextDetail;
                        row.dose_text = grid.dose_text;

                        data.Add(row);
                    }

                    if (listOrderSet.Count > 0)
                    {
                        DataTable dt = Helper.ToDataTable(data);
                        Session[Helper.ViewStateDrug] = dt;
                        drugsGrid.DataSource = dt;
                        drugsGrid.DataBind();
                    }
                }
                else if (type == "3")
                {
                    //order_set_content.Style.Add("height", "calc(100vh - 100px)");
                    var selectedItem = Request.QueryString["selected"];
                    var mappingItem = Request.QueryString["mapping"];
                    List<TemplateSet> listTemplate_Set = (List<TemplateSet>)Session[Helper.SessionTemplateSOAP];

                    //if (selectedItem == "6")
                    //    icd_10.Style.Remove("display");
                    //else
                    //    icd_10.Style.Add("display","none");

                    txt_Template_SOAP.Value = listTemplate_Set.Find(x => x.template_name == nameOrderSet && x.soap_mapping_id == Guid.Parse(mappingItem)).template_remarks;
                    txt_orderHeaderId.Value = listTemplate_Set.Find(x => x.template_name == nameOrderSet && x.soap_mapping_id == Guid.Parse(mappingItem)).soap_template_id.ToString();


                    //frmTemplateSOAP.Style.Add("display", "block");
                    search_drugs.Style.Add("display", "none");
                    typeOrderSet.SelectedValue = selectedItem.ToString();
                }
                else
                {
                    //order_set_content.Style.Remove("height");
                    //frmLaboratory.Style.Add("display", "block");
                    typeOrderSet.SelectedValue = "2";
                    List<CpoeMapping> listOrderSet = new List<CpoeMapping>();

                    try
                    {
                        Dictionary<string, string> logParam = new Dictionary<string, string>
                        {
                            { "Orderset_Name", nameOrderSet },
                            { "Org_ID", Helper.organizationId.ToString() },
                            { "Doctor_ID", Helper.GetDoctorID(this) }
                        };
                        //Log.Debug(LogConfig.LogStart("getDrugsbyLaboratorySetName", logParam));
                        var dataOrderSet = clsOrderSet.getDrugsbyLaboratorySetName(nameOrderSet, Helper.organizationId.ToString(), long.Parse(Helper.GetDoctorID(this)));
                        var JsonOrderSet = JsonConvert.DeserializeObject<ResultMapping>(dataOrderSet.Result.ToString());
                        //string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Orderset_Name", nameOrderSet, "getUpdateFormOrderSet", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataOrderSet.Result.ToString()));
                        //Log.Debug(LogConfig.LogEnd("getDrugsbyLaboratorySetName", JsonOrderSet.Status, JsonOrderSet.Message));
                        listOrderSet = JsonOrderSet.list;

                        txt_orderHeaderId.Value = orderSetId;
                    }
                    catch (Exception ex)
                    {
                        string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Orderset_Name", nameOrderSet, "getUpdateFormOrderSet", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                        //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
                    }
                }
            }
            else
            {
                typeOrderSet.SelectedValue = "1";
                List<CompoundDetail> listOrderSet = new List<CompoundDetail>();

                try
                {
                    Dictionary<string, string> logParam = new Dictionary<string, string>
                    {
                        { "Orderset_Name", nameOrderSet },
                        { "Doctor_ID", Helper.GetDoctorID(this) }
                    };
                    //Log.Debug(LogConfig.LogStart("getCompoundbyOrderSetName", logParam));
                    var dataOrderSet = clsOrderSet.getCompoundbyOrderSetName(nameOrderSet, long.Parse(Helper.GetDoctorID(this)));
                    var JsonOrderSet = JsonConvert.DeserializeObject<ResultCompoundDetail>(dataOrderSet.Result.ToString());

                    //string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Orderset_Name", nameOrderSet, "getUpdateFormOrderSet", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataOrderSet.Result.ToString()));
                    //Log.Debug(LogConfig.LogEnd("getCompoundbyOrderSetName", JsonOrderSet.Status, JsonOrderSet.Message));

                    listOrderSet = JsonOrderSet.list;
                }
                catch (Exception ex)
                {
                    string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Orderset_Name", nameOrderSet, "getUpdateFormOrderSet", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                    //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
                }

                var sem = listOrderSet[0].compound_uom_id.ToString();
                txtQty.Text = Helper.formatDecimal(listOrderSet[0].compound_quantity.ToString().Replace(",", ".")); //listOrderSet[0].compound_quantity % 1 == 0 ? Decimal.ToInt64(listOrderSet[0].compound_quantity).ToString() : listOrderSet[0].compound_quantity.ToString("#.##");
                ddlUOM_header.SelectedValue = listOrderSet[0].compound_uom_id.ToString();
                ddlFrequency_header.SelectedValue = listOrderSet[0].compound_frequency_id.ToString();
                txtDoze.Text = Helper.formatDecimal(listOrderSet[0].compound_dosage_id.ToString().Replace(",",".")); //listOrderSet[0].compound_dosage_id % 1 == 0 ? Decimal.ToInt64(listOrderSet[0].compound_dosage_id).ToString() : listOrderSet[0].compound_dosage_id.ToString("#.##");
                txtInstruction.Value = listOrderSet[0].compound_remarks;
                ddlRoute_header.SelectedValue = listOrderSet[0].compound_administration_route_id.ToString();
                txtIter.Text = listOrderSet[0].compound_iteration.ToString();
                txt_orderHeaderId.Value = listOrderSet[0].order_set_id.ToString();
                ddlDoseUom_header.SelectedValue = listOrderSet[0].compound_dose_uom_id.ToString();
                txbCattDokter_compound.Value = listOrderSet[0].compound_note;
                racikan_dosetext.Text = listOrderSet[0].compound_dose_text;
                if (listOrderSet[0].IsDoseTextHeader == true)
                {
                    div_dose_header.Visible = false;
                    racikan_dosetext.Visible = true;
                    chk_is_dosetext.Checked = true;
                }
                else if(listOrderSet[0].IsDoseTextHeader == false)
                {
                    div_dose_header.Visible = true;
                    racikan_dosetext.Visible = false;
                    chk_is_dosetext.Checked = false;
                }

                List<gridItem> data = new List<gridItem>();

                foreach (CompoundDetail grid in listOrderSet)
                {
                    gridItem row = new gridItem();
                    row.salesItemId = grid.item_id;
                    row.salesItemName = grid.item_name;
                    row.quantity = Helper.formatDecimal(grid.quantity.ToString().Replace(",", ".")); //grid.quantity;
                    row.uomId = grid.UomId;
                    row.uom = grid.uom_code;
                    row.frequencyId = grid.frequency_id;
                    row.frequency = grid.frequency_code;
                    row.doze = Helper.formatDecimal(grid.dosage_id.ToString().Replace(",", ".")); //grid.dosage_id.ToString();
                    row.dozeuomId = grid.dose_uom_id;
                    row.dose_uom = grid.dose_uom;
                    row.instruction = grid.remarks;
                    row.routeId = grid.administration_route_id;
                    row.route = grid.administration_route_code;
                    row.iteration = grid.iteration;
                    row.order_set_detail_id = grid.order_set_detail_id.ToString();
                    row.item_sequence = grid.item_sequence;
                    row.compound_note = grid.compound_note;
                    row.IsDoseTextDetail = grid.IsDoseTextDetail;
                    row.dose_text = grid.dose_text;

                    data.Add(row);
                }

                if (listOrderSet.Count > 0)
                {
                    DataTable dt = Helper.ToDataTable(data);
                    DataView dv = new DataView(dt);
                    dv.Sort = "item_sequence";
                    dt = dv.ToTable();

                    Session[Helper.ViewStateCompound] = dt;
                    compoundGrid.DataSource = dt;
                    compoundGrid.DataBind();
                }
            }
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "Orderset_Name", nameOrderSet, "getUpdateFormOrderSet", StartTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Orderset_Name", nameOrderSet, "getUpdateFormOrderSet", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            Doctor userData = (Doctor)Session[Helper.SessionDoctor];
            var UserID = Helper.GetLoginUser(this);
            List<InsertOrderSet> insertOrderSet = new List<InsertOrderSet>();
            LaboratoryInsert insertLab = new LaboratoryInsert();

            List<CpoeTrans> itemChecked = new List<CpoeTrans>();
            List<CpoeTrans> clinicChecked = new List<CpoeTrans>();
            List<CpoeTrans> microChecked = new List<CpoeTrans>();
            List<CpoeTrans> patologiChecked = new List<CpoeTrans>();
            List<CpoeTrans> mdcChecked = new List<CpoeTrans>();
            List<CpoeTrans> panelChecked = new List<CpoeTrans>();
            clinicChecked = StdClinicControl.getDataChecklistLab();
            //CITOChecked = StdCitoControl.getDataChecklistCITO();
            microChecked = StdMicroLabControl.getDataChecklistMicro();
            patologiChecked = StdAnatomiLab.getDataChecklistLab();
            mdcChecked = StdMDCLabControl.getDataChecklistMDC();
            panelChecked = StdPanelLab.getDataChecklistLab();

            TextInfo textOrderSet = new CultureInfo("en-US", false).TextInfo;

            DataTable dt = Session[Helper.ViewStateDrug] as DataTable;

            List<gridItem> grid = new List<gridItem>();

            if (clinicChecked.Count > 0)
            {
                foreach (CpoeTrans data in clinicChecked)
                {
                    itemChecked.Add(data);
                }
            }

            if (panelChecked.Count > 0)
            {
                foreach (CpoeTrans data in panelChecked)
                {
                    itemChecked.Add(data);
                }
            }

            if (patologiChecked.Count > 0)
            {
                foreach (CpoeTrans data in patologiChecked)
                {
                    itemChecked.Add(data);
                }
            }

            if (mdcChecked.Count > 0)
            {
                foreach (CpoeTrans data in mdcChecked)
                {
                    itemChecked.Add(data);
                }
            }

            if (microChecked.Count > 0)
            {
                foreach (CpoeTrans data in microChecked)
                {
                    itemChecked.Add(data);
                }
            }

            DataTable dataSave = new DataTable();
            bool statusDataSave = true;
            int countorderset = 1;

            if (typeOrderSet.SelectedValue == "0" && drugsGrid.Rows.Count > 0)
            {
                foreach (GridViewRow rows in drugsGrid.Rows)
                {
                    Label nameItem = (Label)rows.FindControl("label_item_drugs");
                    HiddenField idItem = (HiddenField)rows.FindControl("id_item_drugs");
                    HiddenField orderSetDetailId = (HiddenField)rows.FindControl("id_order_drugs_detail");
                    TextBox quantity = (TextBox)rows.FindControl("qty_drugs");
                    DropDownList ddluom_drugs = (DropDownList)rows.FindControl("ddluom_drugs");
                    DropDownList ddlfrequency_drugs = (DropDownList)rows.FindControl("ddlfrequency_drugs");
                    TextBox dose_drugs = (TextBox)rows.FindControl("dose_drugs");
                    DropDownList ddlroute_drugs = (DropDownList)rows.FindControl("ddlroute_drugs");
                    DropDownList ddldoseUOM_drugs = (DropDownList)rows.FindControl("ddldoseUOM_drugs");
                    TextBox instruction_drugs = (TextBox)rows.FindControl("instruction_drugs");
                    TextBox iter_drugs = (TextBox)rows.FindControl("iter_drugs");
                    Label formularium = (Label)rows.FindControl("formularium_drugs");
                    TextBox doseText_drugs = (TextBox)rows.FindControl("doseText_drugs");
                    CheckBox drug_is_dosetext = (CheckBox)rows.FindControl("drug_is_dosetext");

                    InsertOrderSet orderSet = new InsertOrderSet();
                    orderSet.item_sequence = countorderset;
                    countorderset = countorderset + 1;
                    orderSet.is_compound = false;
                    if (txt_orderHeaderId.Value.ToString() == "")
                    {
                        orderSet.order_set_id = default(Guid);
                    }
                    else
                    {
                        orderSet.order_set_id = Guid.Parse(txt_orderHeaderId.Value.ToString());
                    }
                    orderSet.doctor_id = long.Parse(Helper.GetDoctorID(this));
                    orderSet.order_set_name = textOrderSet.ToTitleCase(txtNewOrderSet_Name.Text);
                    //compound
                    orderSet.compound_quantity = 0;
                    orderSet.compound_uom_id = 0;
                    orderSet.compound_dosage_id = 0;
                    orderSet.compound_dose_text = "";
                    orderSet.compound_frequency_id = 0;
                    orderSet.compound_administration_route_id = 0;
                    orderSet.compound_Iteration = 0;
                    orderSet.compound_remarks = "";
                    //detail
                    if (orderSetDetailId.Value == "")
                    {
                        orderSet.order_set_detail_id = default(Guid);
                    }
                    else
                    {
                        orderSet.order_set_detail_id = Guid.Parse(orderSetDetailId.Value);
                    }
                    orderSet.item_id = long.Parse(idItem.Value);
                    orderSet.item_name = nameItem.Text.ToString();
                    if (quantity.Text.ToString() == "")
                    {
                        orderSet.quantity = "0";
                    }
                    else
                    {
                        orderSet.quantity = quantity.Text.ToString();
                    }
                    if (ddluom_drugs.SelectedValue.ToString() == "")
                    {
                        orderSet.uom_id = 0;
                    }
                    else
                    {
                        orderSet.uom_id = long.Parse(ddluom_drugs.SelectedValue.ToString());
                    }

                    if (dose_drugs.Text.ToString() == "")
                    {
                        orderSet.dosage_id = "0";
                    }
                    else
                    {
                        orderSet.dosage_id = dose_drugs.Text.Replace(",", ".");
                    }
                    if (ddldoseUOM_drugs.SelectedValue.ToString() == "")
                    {
                        orderSet.dose_uom_id = 0;
                    }
                    else
                    {
                        orderSet.dose_uom_id = long.Parse(ddldoseUOM_drugs.SelectedValue.ToString());
                    }

                    orderSet.dose_text = doseText_drugs.Text;
                    orderSet.IsDoseTextDetail = drug_is_dosetext.Checked;
                    orderSet.frequency_id = long.Parse(ddlfrequency_drugs.SelectedValue);
                    orderSet.administration_route_id = long.Parse(ddlroute_drugs.SelectedValue);
                    if (iter_drugs.Text.ToString() == "")
                    {
                        orderSet.iteration = 0;
                    }
                    else
                    {
                        orderSet.iteration = int.Parse(iter_drugs.Text);
                    }
                    orderSet.remarks = instruction_drugs.Text.ToString();
                    orderSet.type = "";
                    orderSet.created_by = userData.doctor_id.ToString();
                    orderSet.modified_by = userData.doctor_id.ToString();

                    insertOrderSet.Add(orderSet);
                    grid.Add(new gridItem
                    {
                        salesItemName = nameItem.Text,
                        salesItemId = Int64.Parse(idItem.Value),
                        quantity = quantity.Text,
                        uom = ddluom_drugs.SelectedValue,
                        uomId = Int64.Parse(ddluom_drugs.SelectedValue),
                        dozeuomId = Int64.Parse(ddldoseUOM_drugs.SelectedValue),
                        formularium = formularium.Text,
                        frequency = ddlfrequency_drugs.SelectedValue,
                        frequencyId = Int64.Parse(ddlfrequency_drugs.SelectedValue),
                        route = ddlroute_drugs.SelectedValue,
                        routeId = Int64.Parse(ddlroute_drugs.SelectedValue),
                        instruction = instruction_drugs.Text,
                        iteration = Int64.Parse(iter_drugs.Text)
                    });
                }
                Session[Helper.ViewStateDrug] = Helper.ToDataTable(grid);
                dataSave = Session[Helper.ViewStateDrug] as DataTable;
                var quantityItem = (from DataRow dr in dataSave.Rows
                                    where (string)dr["quantity"] == "0"
                                    select dr["salesItemName"]);

                if (quantityItem.Count() != 0)
                {
                    statusDataSave = false;
                }

                var sameItem = dataSave.DefaultView.ToTable(true, "salesItemName");

                if (sameItem.Rows.Count != dataSave.Rows.Count)
                {
                    statusDataSave = false;
                }

                if (insertOrderSet.Count > 0)
                {
                    if (!statusDataSave)
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "alert('This action can not done. Items quantity is 0 or there are same Item.');", true);
                        //ShowToastr("This action can not done. Items quantity is 0 or there are same Item.", "Save Alert!", "Warning");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "warning", "warningnotificationOption(); toastr.warning('This action can not done. Items quantity is 0 or there are same Item. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Alert!');", true);
                    }
                    else
                    {
                        //Log.Debug(LogConfig.LogStart("insertNewOrderSet", LogConfig.JsonToString(insertOrderSet)));
                        var result = clsOrderSet.insertNewOrderSet(insertOrderSet);
                        var resultJson = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result);
                        var Status = resultJson.Property("status").Value.ToString();
                        var Message = resultJson.Property("message").Value.ToString();

                        //string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "btnSave_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message.ToString()));
                        //Log.Debug(LogConfig.LogEnd("insertNewOrderSet", Status, Message));

                        if (result.ToString().Count() > 0)
                        {
                            var resultMessage = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result.ToString());
                            var status = resultMessage.Property("status").Value;
                            if (status.ToString() == "Success")
                            {
                                var message = resultMessage.Property("data").Value;
                                if (message.ToString() == "DUPLICATE")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "warning", "warningnotificationOption(); toastr.warning('Fail. Order set name already exist. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Alert!');", true);
                                }
                                else
                                {
                                    Session.Remove(Helper.SessionDrug);
                                    Session.Remove(Helper.ViewStateDrug);
                                    Session.Remove(Helper.ViewStateCompound);

                                    Response.Redirect("~/Form/General/OrderSet/ManageOrderSet.aspx", false);
                                    Context.ApplicationInstance.CompleteRequest();

                                    List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                                    markerlist.Find(x => x.key == "SAVEORDERmarker").value = "marked";
                                    Session[Helper.SESSIONmarker] = markerlist;
                                }
                            }
                            else
                            {
                                var message = resultMessage.Property("message").Value;

                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Fail. Order set name already exist');", true);
                                //ShowToastr("Fail. Order set name already exist.", "Save Alert!", "Warning");
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "warning", "warningnotificationOption(); toastr.warning('Fail. Order set name already exist. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Alert!');", true);
                            }
                        }
                    }
                }
            }
            else if (typeOrderSet.SelectedValue == "1" && compoundGrid.Rows.Count > 0)
            {

                foreach (GridViewRow rows in compoundGrid.Rows)
                {
                    Label nameItem = (Label)rows.FindControl("item_compound");
                    HiddenField idItem = (HiddenField)rows.FindControl("id_item_compound");
                    HiddenField orderSetDetailId = (HiddenField)rows.FindControl("id_order_compound_detail");

                    CheckBox isDosetext = (CheckBox)rows.FindControl("racikan_is_dosetext");
                    TextBox dose = (TextBox)rows.FindControl("racikan_dosage_id");
                    DropDownList ddlDose = (DropDownList)rows.FindControl("racikan_doseuom");
                    TextBox doseText = (TextBox)rows.FindControl("racikan_dosetext");

                    TextBox quantity = (TextBox)rows.FindControl("qty_compound");
                    DropDownList ddluom_compound = (DropDownList)rows.FindControl("ddluom_compound");
                    //TextBox doseText_compound = (TextBox)rows.FindControl("doseText_compound");
                    TextBox instruction_compound = (TextBox)rows.FindControl("instruction_compound");

                    InsertOrderSet orderSet = new InsertOrderSet();
                    orderSet.is_compound = true;
                    if (txt_orderHeaderId.Value.ToString() == "")
                    {
                        orderSet.order_set_id = default(Guid);
                    }
                    else
                    {
                        orderSet.order_set_id = Guid.Parse(txt_orderHeaderId.Value.ToString());
                    }
                    orderSet.doctor_id = long.Parse(Helper.GetDoctorID(this));
                    orderSet.order_set_name = textOrderSet.ToTitleCase(txtNewOrderSet_Name.Text);
                    //compound
                    if (txtQty.Text.ToString() == "")
                    {
                        orderSet.compound_quantity = 1;
                    }
                    else
                    {
                        orderSet.compound_quantity = decimal.Parse(txtQty.Text);
                    }
                    orderSet.compound_uom_id = long.Parse(ddlUOM_header.SelectedValue);

                    if (txtDoze.Text.ToString() == "")
                    {
                        orderSet.compound_dosage_id = 0;
                    }
                    else
                    {
                        orderSet.compound_dosage_id = decimal.Parse(txtDoze.Text);
                    }
                    orderSet.compound_dose_uom_id = Int64.Parse(ddlDoseUom_header.SelectedValue);

                    if (chk_is_dosetext.Checked == true)
                    {
                        orderSet.IsDoseTextHeader = true;
                        orderSet.compound_dose_text = racikan_dosetext.Text;
                    }
                    else if(chk_is_dosetext.Checked == false)
                    {
                        orderSet.IsDoseTextHeader = false;
                        orderSet.compound_dose_text = "";
                    }

                    orderSet.compound_frequency_id = long.Parse(ddlFrequency_header.SelectedValue);
                    orderSet.compound_administration_route_id = long.Parse(ddlRoute_header.SelectedValue);
                    if (txtIter.Text.ToString() == "")
                    {
                        orderSet.compound_Iteration = 0;
                    }
                    else
                    {
                        orderSet.compound_Iteration = int.Parse(txtIter.Text.ToString());
                    }
                    orderSet.compound_remarks = txtInstruction.Value.ToString();
                    //detail
                    if (orderSetDetailId.Value == "")
                    {
                        orderSet.order_set_detail_id = default(Guid);
                    }
                    else
                    {
                        orderSet.order_set_detail_id = Guid.Parse(orderSetDetailId.Value);
                    }
                    orderSet.item_id = long.Parse(idItem.Value);
                    orderSet.item_name = nameItem.Text.ToString();
                    if (quantity.Text.ToString() == "")
                    {
                        orderSet.quantity = "0";
                    }
                    else
                    {
                        orderSet.quantity = quantity.Text.ToString();
                    }
                    if (ddluom_compound.SelectedValue.ToString() == "")
                    {
                        orderSet.uom_id = 0;
                    }
                    else
                    {
                        orderSet.uom_id = long.Parse(ddluom_compound.SelectedValue);
                    }
                    orderSet.dosage_id = "0";
                    orderSet.dose_text = "";

                    if (dose.Text == "")
                    {
                        orderSet.dosage_id = "0";
                    }
                    else
                    {
                        orderSet.dosage_id = dose.Text.ToString();
                    }

                    if (ddlDose.SelectedItem.ToString() != "")
                    {
                        orderSet.dose_uom_id = Int64.Parse(ddlDose.SelectedValue.ToString());
                    }
                    else if (ddlDose.SelectedItem.ToString() == "")
                    {
                        orderSet.dose_uom_id = 0;
                    }

                    if (isDosetext.Checked == true)
                    {
                        orderSet.IsDoseTextDetail = true;
                        orderSet.dose_text = doseText.Text;
                    }
                    else
                    {
                        orderSet.IsDoseTextDetail = false;
                        orderSet.dose_text = "";
                    }

                    orderSet.frequency_id = 0;
                    orderSet.administration_route_id = 0;
                    orderSet.iteration = 0;
                    orderSet.remarks = instruction_compound.Text.ToString();
                    orderSet.type = "";
                    orderSet.created_by = userData.doctor_id.ToString();
                    orderSet.modified_by = userData.doctor_id.ToString();
                    orderSet.item_sequence = countorderset;
                    countorderset++;
                    orderSet.compound_note = txbCattDokter_compound.Value;

                    insertOrderSet.Add(orderSet);

                    grid.Add(new gridItem
                    {
                        salesItemName = nameItem.Text,
                        salesItemId = Int64.Parse(idItem.Value),
                        quantity = quantity.Text,
                        uom = ddluom_compound.SelectedValue,
                        uomId = Int64.Parse(ddluom_compound.SelectedValue)
                    });

                }
                Session[Helper.ViewStateCompound] = Helper.ToDataTable(grid);
                dataSave = Session[Helper.ViewStateCompound] as DataTable;
                var quantityItem = (from DataRow dr in dataSave.Rows
                                    where (string)dr["quantity"] == "0"
                                    select dr["salesItemName"]);

                //if (quantityItem.Count() != 0)
                //{
                //    statusDataSave = false;
                //}

                var sameItem = dataSave.DefaultView.ToTable(true, "salesItemName");

                if (sameItem.Rows.Count != dataSave.Rows.Count)
                {
                    statusDataSave = false;
                }

                if (insertOrderSet.Count > 0)
                {
                    if (!statusDataSave)
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "alert('This action can not done. Items quantity is 0 or there are same Item.');", true);
                        //ShowToastr("This action can not done. Items quantity is 0 or there are same Item.", "Save Alert!", "Warning");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "warning", "warningnotificationOption(); toastr.warning('This action can not done. Items quantity is 0 or there are same Item. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Alert!');", true);
                    }
                    else
                    {
                       // Log.Debug(LogConfig.LogStart("insertNewOrderSet", LogConfig.JsonToString(insertOrderSet)));
                        var result = clsOrderSet.insertNewOrderSet(insertOrderSet);
                        var resultJson = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result);
                        var Status = resultJson.Property("status").Value.ToString();
                        var Message = resultJson.Property("message").Value.ToString();

                        //string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "btnSave_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message.ToString()));
                        //Log.Debug(LogConfig.LogEnd("insertNewOrderSet", Status, Message));

                        if (result.ToString().Count() > 0)
                        {
                            var resultMessage = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result.ToString());
                            var status = resultMessage.Property("status").Value;
                            if (status.ToString() == "Success")
                            {
                                var message = resultMessage.Property("data").Value;
                                if (message.ToString() == "DUPLICATE")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "warning", "warningnotificationOption(); toastr.warning('Fail. Order set name already exist. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Alert!');", true);
                                }
                                else
                                {
                                    Session.Remove(Helper.SessionDrug);
                                    Session.Remove(Helper.ViewStateDrug);
                                    Session.Remove(Helper.ViewStateCompound);

                                    Response.Redirect("~/Form/General/OrderSet/ManageOrderSet.aspx", false);
                                    Context.ApplicationInstance.CompleteRequest();

                                    List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                                    markerlist.Find(x => x.key == "SAVEORDERmarker").value = "marked";
                                    Session[Helper.SESSIONmarker] = markerlist;
                                }
                            }
                            else
                            {
                                var message = resultMessage.Property("message").Value;

                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Fail. Order set name already exist');", true);
                                //ShowToastr("Fail. Order set name already exist.", "Save Alert!", "Warning");
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "warning", "warningnotificationOption(); toastr.warning('Fail. Order set name already exist. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Alert!');", true);
                            }
                        }
                    }
                }
            }
            else if (typeOrderSet.SelectedValue == "2")
            {
                if (itemChecked.Count > 0)
                {
                    Guid order_set_id;
                    if (txt_orderHeaderId.Value.ToString() == "")
                    {
                        order_set_id = Guid.Empty;
                    }
                    else
                    {
                        order_set_id = Guid.Parse(txt_orderHeaderId.Value);
                    }

                    Dictionary<string, string> logParam = new Dictionary<string, string>
                    {
                        { "Orderset_ID", order_set_id.ToString() },
                        { "Orderset_Name", textOrderSet.ToTitleCase(txtNewOrderSet_Name.Text) },
                        { "Doctor_ID", Helper.GetDoctorID(this) }
                    };
                    //Log.Debug(LogConfig.LogStart("insertNewLabOrderSet", logParam, LogConfig.JsonToString(itemChecked)));
                    var result = clsOrderSet.insertNewLabOrderSet(order_set_id, textOrderSet.ToTitleCase(txtNewOrderSet_Name.Text), long.Parse(Helper.GetDoctorID(this)), itemChecked);
                    var resultJson = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result);
                    var Status = resultJson.Property("status").Value.ToString();
                    var Message = resultJson.Property("message").Value.ToString();
                    
                    //string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "btnSave_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message.ToString()));
                    //Log.Debug(LogConfig.LogEnd("insertNewLabOrderSet", Status, Message));

                    if (result.ToString().Count() > 0)
                    {
                        var resultMessage = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result.ToString());
                        var status = resultMessage.Property("status").Value;
                        if (status.ToString() == "Success")
                        {
                            var message = resultMessage.Property("data").Value;
                            if (message.ToString() == "DUPLICATE")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "warning", "warningnotificationOption(); toastr.warning('Fail. Order set name already exist. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Alert!');", true);
                            }
                            else
                            {
                                Session.Remove(Helper.SessionDrug);
                                Session.Remove(Helper.ViewStateDrug);
                                Session.Remove(Helper.ViewStateCompound);

                                Session[Helper.ViewStateOrderSetType] = typeOrderSet.SelectedValue;

                                Response.Redirect("~/Form/General/OrderSet/ManageOrderSet.aspx", false);
                                Context.ApplicationInstance.CompleteRequest();

                                List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                                markerlist.Find(x => x.key == "SAVEORDERmarker").value = "marked";
                                Session[Helper.SESSIONmarker] = markerlist;
                            }
                        }
                        else
                        {
                            var message = resultMessage.Property("message").Value;

                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Fail. Order set name already exist');", true);
                            //ShowToastr("Fail. Order set name already exist.", "Save Alert!", "Warning");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "warning", "warningnotificationOption(); toastr.warning('Fail. Order set name already exist. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Alert!');", true);
                        }
                    }
                    else
                    {
                        //ShowToastr("Plase tick minimum 1 item.", "Save Alert!", "Warning");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "warning", "warningnotificationOption(); toastr.warning('Plase tick minimum 1 item. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Alert!');", true);
                    }
                }
            }
            else
            {
                SpesificTemplateSet newData = new SpesificTemplateSet();
                if (typeOrderSet.SelectedValue == "5")
                {
                    newData.template_name = txtNewOrderSet_Name.Text;
                    newData.template_remarks = txt_Template_SOAP.Value;
                    newData.template_value = "Abnormal";
                }
                else
                {
                    newData.template_name = txtNewOrderSet_Name.Text;
                    newData.template_remarks = txt_Template_SOAP.Value;
                    newData.template_value = "";
                }

                List<templateMappingID> templateData =  Helper.getMappingTemplateSOAP(this);
                var mappingId = templateData.Find(x => x.mappingName == typeOrderSet.SelectedItem.Text).mappingID;


                try
                {
                    Task<string> result;
                    if (txt_orderHeaderId.Value == "")
                    {
                        Dictionary<string, string> logParam = new Dictionary<string, string>
                        {
                            { "Mapping_ID", mappingId },
                            { "Doctor_ID", Helper.GetDoctorID(this) }
                        };
                        //Log.Debug(LogConfig.LogStart("insertNewTemplateSOAP", logParam, LogConfig.JsonToString(newData)));
                        result = clsOrderSet.insertNewTemplateSOAP(mappingId, Int64.Parse(Helper.GetDoctorID(this)), newData);
                        var resultJson = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result);
                        var Status = resultJson.Property("status").Value.ToString();
                        var Message = resultJson.Property("message").Value.ToString();

                        //string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "btnSave_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message.ToString()));
                        //Log.Debug(LogConfig.LogEnd("insertNewTemplateSOAP", Status, Message));
                    }
                    else
                    {
                        Dictionary<string, string> logParam = new Dictionary<string, string>
                        {
                            { "Mapping_ID", mappingId },
                            { "Doctor_ID", Helper.GetDoctorID(this) },
                            { "Old_Orderset_Name", txt_orderHeaderId.Value }
                        };
                        //Log.Debug(LogConfig.LogStart("updateTemplateSOAP", logParam, LogConfig.JsonToString(newData)));
                        result = clsOrderSet.updateTemplateSOAP(mappingId, Int64.Parse(Helper.GetDoctorID(this)), txt_orderHeaderId.Value, newData);
                        var resultJson = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result);
                        var Status = resultJson.Property("status").Value.ToString();
                        var Message = resultJson.Property("message").Value.ToString();

                        //string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        //Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "btnSave_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message.ToString()));
                        //Log.Debug(LogConfig.LogEnd("updateTemplateSOAP", Status, Message));
                    }

                    if (result.ToString().Count() > 0)
                    {
                        var resultMessage = (JObject)JsonConvert.DeserializeObject<dynamic>(result.Result.ToString());
                        var status = resultMessage.Property("status").Value;
                        if (status.ToString() == "Success")
                        {
                            var message = resultMessage.Property("data").Value;
                            if (message.ToString() == "DUPLICATE")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "warning", "warningnotificationOption(); toastr.warning('Fail. Order set name already exist. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Alert!');", true);
                            }
                            else
                            {
                                Session.Remove(Helper.SessionDrug);
                                Session.Remove(Helper.ViewStateDrug);
                                Session.Remove(Helper.ViewStateCompound);

                                Session[Helper.ViewStateOrderSetType] = typeOrderSet.SelectedValue;

                                Response.Redirect("~/Form/General/OrderSet/ManageOrderSet.aspx", false);
                                Context.ApplicationInstance.CompleteRequest();

                                List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                                markerlist.Find(x => x.key == "SAVEORDERmarker").value = "marked";
                                Session[Helper.SESSIONmarker] = markerlist;
                            }
                        }
                        else
                        {
                            var message = resultMessage.Property("message").Value;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "warning", "warningnotificationOption(); toastr.warning('Fail. Order set name already exist. <br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', 'Save Alert!');", true);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "btnSave_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                    //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
                }

                string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "btnSave_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            }
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "btnSave_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogStart());
    }

    protected void btnFind_click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        string a = txtSearchItem.Text;
        DataTable dt = new DataTable();
        if (a == "")
        {
            dt = ((DataTable)Session[Helper.SessionDrug]).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
            gvw_data.DataSource = dt;
            gvw_data.DataBind();
        }
        else
        {
            try
            {
                dt = ((DataTable)Session[Helper.SessionDrug]).Select("salesItemName like '%" + a + "%' or activeIngredientsName like '%" + a + "%'").CopyToDataTable();
                gvw_data.DataSource = dt;
            }
            catch
            {
                gvw_data.DataSource = null;
            }
            gvw_data.DataBind();
        }
        txtSearchItem.Focus();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "btnFind_click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void txtDrugsSearch_TextChanged(object sender, EventArgs e)
    {
        btnFind_click(null, null);
    }

    protected void itemselected_onclick(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (typeOrderSet.SelectedValue == "0")  //drugs
        {
            int selRowIndex = ((GridViewRow)(((Button)sender).Parent.Parent)).RowIndex;
            Button salesItemName = (Button)gvw_data.Rows[selRowIndex].FindControl("salesItemName");
            Label salesitemid = (Label)gvw_data.Rows[selRowIndex].FindControl("salesitemid");
            HiddenField hfUomId = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfUomId");
            HiddenField hfUomCode = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfUomCode");

            HiddenField hfDose = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfDose");
            HiddenField hfDoseUomId = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfDoseUomId");
            HiddenField hfFrequencyId = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfFrequencyId");
            HiddenField hfRouteId = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfRouteId");

            TextBox txtFormularium = (TextBox)gvw_data.Rows[selRowIndex].FindControl("formularium");
            
            List<gridItem> row = getRowList(1);

            gridItem temp = new gridItem();
            temp.salesItemId = long.Parse(salesitemid.Text);
            temp.salesItemName = salesItemName.Text;


            /* ========== Logic untuk pengecekan angka di belakang point decimal ========= */
            Decimal doseDecimal = Convert.ToDecimal(hfDose.Value);
            int doseInt = Convert.ToInt16(doseDecimal);

            Decimal newDose = doseDecimal - doseInt;
            String doseGrid = "";
            if (newDose / 1 == 0)
                doseGrid = doseInt.ToString();
            else
                doseGrid = hfDose.Value.Replace(",", ".");
            /* ========== Logic untuk pengecekan angka di belakang point decimal ========= */


            temp.doze = doseGrid;
            temp.dozeuomId = Int64.Parse(hfDoseUomId.Value);
            temp.frequencyId = Int64.Parse(hfFrequencyId.Value);
            temp.routeId = Int64.Parse(hfRouteId.Value);
            temp.quantity = "0";
            temp.iteration = 0;
            temp.order_set_detail_id = "";
            temp.uom = hfUomCode.Value;
            temp.uomId = long.Parse(hfUomId.Value);
            temp.formularium = txtFormularium.Text;

            if (row.FindAll(x => x.formularium.Equals(txtFormularium.Text.ToString())).Count == row.Count)
            {
                checkFormularium.Value = "Formularium";
            }
            else
            {
                checkFormularium.Value = "Not Formularium";
            }

            if (temp != null)
            {
                row.Add(temp);
                DataTable dta = Helper.ToDataTable(row);
                Session[Helper.ViewStateDrug] = dta;

                drugsGrid.DataSource = dta;
                drugsGrid.DataBind();
                txtSearchItem.Text = "";
            }
        }
        else if (typeOrderSet.SelectedValue == "1")  //compounds
        {
            int selRowIndex = ((GridViewRow)(((Button)sender).Parent.Parent)).RowIndex;
            Button salesItemName = (Button)gvw_data.Rows[selRowIndex].FindControl("salesItemName");
            Label salesitemid = (Label)gvw_data.Rows[selRowIndex].FindControl("salesitemid");
            HiddenField hfUomId = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfUomId");
            HiddenField hfUomCode = (HiddenField)gvw_data.Rows[selRowIndex].FindControl("hfUomCode");

            List<gridItem> row = getRowList(0);

            if (row.Count > 1) {
                checkItemCount.Value = "More One";
            }
            else
            {
                checkItemCount.Value = "One";
            }

            gridItem temp = new gridItem();
            temp.salesItemId = long.Parse(salesitemid.Text);
            temp.salesItemName = salesItemName.Text;
            temp.quantity = "0";
            temp.iteration = 0;
            temp.order_set_detail_id = "";
            temp.uom = hfUomCode.Value;
            temp.uomId = long.Parse(hfUomId.Value);

            if (temp != null)
            {
                row.Add(temp);
                DataTable dta = Helper.ToDataTable(row);
                Session[Helper.ViewStateCompound] = dta;
                compoundGrid.DataSource = dta;
                compoundGrid.DataBind();
                txtSearchItem.Text = "";
            }
        }

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "close", "loseBox()", true);

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "itemselected_onclick", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected List<gridItem> getRowList(int type)
    {
        List<gridItem> data = new List<gridItem>();
        if (type == 1)
        {
            foreach (GridViewRow rows in drugsGrid.Rows)
            {
                Label nameItem = (Label)rows.FindControl("label_item_drugs");
                HiddenField idItem = (HiddenField)rows.FindControl("id_item_drugs");
                HiddenField orderSetDetailId = (HiddenField)rows.FindControl("id_order_drugs_detail");
                TextBox quantity = (TextBox)rows.FindControl("qty_drugs");
                DropDownList ddluom_drugs = (DropDownList)rows.FindControl("ddluom_drugs");
                DropDownList ddlfrequency_drugs = (DropDownList)rows.FindControl("ddlfrequency_drugs");
                DropDownList ddldoseUOM_drugs = (DropDownList)rows.FindControl("ddldoseUOM_drugs");
                TextBox dose_drugs = (TextBox)rows.FindControl("dose_drugs");
                TextBox doseText_drugs = (TextBox)rows.FindControl("doseText_drugs");
                DropDownList ddlroute_drugs = (DropDownList)rows.FindControl("ddlroute_drugs");
                TextBox instruction_drugs = (TextBox)rows.FindControl("instruction_drugs");
                TextBox iter_drugs = (TextBox)rows.FindControl("iter_drugs");
                Label formularium_drugs = (Label)rows.FindControl("formularium_drugs");
                CheckBox chkIsDoseText = (CheckBox)rows.FindControl("drug_is_dosetext");

                gridItem row = new gridItem();

                row.salesItemId = Int64.Parse(idItem.Value);
                row.salesItemName = nameItem.Text;
                if (quantity.Text == "")
                {
                    row.quantity = "0";
                }
                else
                {
                    //var decimaltemp = Decimal.Parse(quantity.Text);
                    row.quantity = quantity.Text.ToString();
                }
                if (ddluom_drugs.SelectedItem.ToString() != "")
                {
                    row.uomId = Int64.Parse(ddluom_drugs.SelectedValue.ToString());
                    row.uom = ddluom_drugs.SelectedItem.Text;
                }
                else if (ddluom_drugs.SelectedItem.ToString() == "")
                {
                    row.uomId = 0;
                    row.uom = "";
                }
                row.frequencyId = Int64.Parse(ddlfrequency_drugs.SelectedValue);
                row.frequency = ddlfrequency_drugs.SelectedItem.Text;
                row.doze = dose_drugs.Text;
                row.dozeuomId = Int64.Parse(ddldoseUOM_drugs.SelectedItem.Value);
                row.dose_text = doseText_drugs.Text;
                row.instruction = instruction_drugs.Text;
                row.routeId = Int64.Parse(ddlroute_drugs.SelectedValue);
                row.route = ddlroute_drugs.SelectedItem.Text;
                row.order_set_detail_id = orderSetDetailId.Value;
                row.formularium = formularium_drugs.Text;
                if (iter_drugs.Text == "")
                {
                    row.iteration = 0;
                }
                else
                {
                    row.iteration = Int64.Parse(iter_drugs.Text);
                }
                row.IsDoseTextDetail = chkIsDoseText.Checked;

                data.Add(row);
            }
        }
        else
        {
            //tblCompound.Style.Add("display", "block");
            //compoundView.Style.Add("display", "block");
            //compoundCattDoctor.Style.Add("display", "block");

            foreach (GridViewRow rows in compoundGrid.Rows)
            {
                Label nameItem = (Label)rows.FindControl("item_compound");
                HiddenField idItem = (HiddenField)rows.FindControl("id_item_compound");
                HiddenField id_order_compound_detail = (HiddenField)rows.FindControl("id_order_compound_detail");

                CheckBox isDosetext = (CheckBox)rows.FindControl("racikan_is_dosetext");
                TextBox dose = (TextBox)rows.FindControl("racikan_dosage_id");
                DropDownList ddlDose = (DropDownList)rows.FindControl("racikan_doseuom");
                TextBox doseText = (TextBox)rows.FindControl("racikan_dosetext");

                TextBox quantity = (TextBox)rows.FindControl("qty_compound");
                DropDownList ddluom_compound = (DropDownList)rows.FindControl("ddluom_compound");

                //TextBox doseText_compound = (TextBox)rows.FindControl("doseText_compound");
                TextBox instruction_compound = (TextBox)rows.FindControl("instruction_compound");

                gridItem row = new gridItem();

                row.salesItemId = Int64.Parse(idItem.Value);
                row.salesItemName = nameItem.Text;
                if (quantity.Text == "")
                {
                    row.quantity = "0";
                }
                else
                {
                    //var decimaltemp = Decimal.Parse(quantity.Text);
                    row.quantity = quantity.Text.ToString();
                }

                if (ddluom_compound.SelectedItem.ToString() != "")
                {
                    row.uomId = Int64.Parse(ddluom_compound.SelectedValue.ToString());
                    row.uom = ddluom_compound.SelectedItem.Text;
                }
                else if (ddluom_compound.SelectedItem.ToString() == "")
                {
                    row.uomId = 0;
                    row.uom = "";
                }

                if (dose.Text == "")
                {
                    row.doze = "0";
                }
                else
                {
                    //var decimaltemp = Decimal.Parse(quantity.Text);
                    row.doze = dose.Text.ToString();
                }

                if (ddlDose.SelectedItem.ToString() != "")
                {
                    row.dozeuomId = Int64.Parse(ddlDose.SelectedValue.ToString());
                    row.dose_uom = ddlDose.SelectedItem.Text;
                }
                else if (ddlDose.SelectedItem.ToString() == "")
                {
                    row.dozeuomId = 0;
                    row.dose_uom = "";
                }

                if (isDosetext.Checked == true)
                {
                    row.IsDoseTextDetail = true;
                    row.dose_text = doseText.Text;
                }
                else
                {
                    row.IsDoseTextDetail = false;
                    row.dose_text = "";
                }
                
                row.instruction = instruction_compound.Text;
                row.order_set_detail_id = id_order_compound_detail.Value;

                data.Add(row);
            }
        }

        return data;
    }

    protected void compoundGrid_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField idItem = (HiddenField)e.Row.FindControl("id_item_compound");
            DropDownList racikan_doseuom = (DropDownList)e.Row.FindControl("racikan_doseuom");
            DropDownList ddluom_compound = (DropDownList)e.Row.FindControl("ddluom_compound");
            DataTable dt = ((DataTable)Session[Helper.ViewStateCompound]).Select("salesItemId = " + idItem.Value).CopyToDataTable();

            //ddluom_compound.Items.Insert(0, new ListItem(dt.Rows[0]["uom"].ToString(), dt.Rows[0]["uomId"].ToString()));//
            ddluom_compound.DataSource = uomdt;
            ddluom_compound.DataTextField = "name";
            ddluom_compound.DataValueField = "uomId";
            ddluom_compound.DataBind();
            ddluom_compound.Items.Insert(0, new ListItem("-", "0"));
            ddluom_compound.SelectedValue = dt.Rows[0]["uomId"].ToString();

            racikan_doseuom.DataSource = doseUomdt;
            racikan_doseuom.DataTextField = "name";
            racikan_doseuom.DataValueField = "doseUomId";
            racikan_doseuom.DataBind();
            racikan_doseuom.Items.Insert(0, new ListItem("-", "0"));
            racikan_doseuom.SelectedValue = dt.Rows[0]["dozeuomId"].ToString();
        }
    }

    protected void drugsGrid_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField idItem = (HiddenField)e.Row.FindControl("id_item_drugs");
            DropDownList ddlfrequency_drugs = (DropDownList)e.Row.FindControl("ddlfrequency_drugs");
            DropDownList ddlroute_drugs = (DropDownList)e.Row.FindControl("ddlroute_drugs");
            DropDownList ddluom_drugs = (DropDownList)e.Row.FindControl("ddluom_drugs");
            DropDownList ddldoseUOM_drugs = (DropDownList)e.Row.FindControl("ddldoseUOM_drugs");
            Label txt_formularium = (Label)e.Row.FindControl("formularium_drugs");

            DataTable dt = ((DataTable)Session[Helper.ViewStateDrug]).Select("salesItemId = " + idItem.Value).CopyToDataTable();

            ddlfrequency_drugs.DataSource = frequencydt;
            ddlfrequency_drugs.DataTextField = "name";
            ddlfrequency_drugs.DataValueField = "administrationFrequencyId";
            ddlfrequency_drugs.DataBind();
            ddlfrequency_drugs.Items.Insert(0, new ListItem("-", "0"));
            ddlfrequency_drugs.SelectedValue = dt.Rows[0]["frequencyId"].ToString();

            ddlroute_drugs.DataSource = routedt;
            ddlroute_drugs.DataTextField = "name";
            ddlroute_drugs.DataValueField = "administration_route_id";
            ddlroute_drugs.DataBind();
            ddlroute_drugs.Items.Insert(0, new ListItem("-", "0"));
            ddlroute_drugs.SelectedValue = dt.Rows[0]["routeId"].ToString();

            ddluom_drugs.DataSource = uomdt;
            ddluom_drugs.DataTextField = "name";
            ddluom_drugs.DataValueField = "uomId";
            ddluom_drugs.DataBind();
            ddluom_drugs.Items.Insert(0, new ListItem("-", "0"));
            ddluom_drugs.SelectedValue = dt.Rows[0]["uomId"].ToString();

            ddldoseUOM_drugs.DataSource = doseUomdt;
            ddldoseUOM_drugs.DataTextField = "name";
            ddldoseUOM_drugs.DataValueField = "doseUomId";
            ddldoseUOM_drugs.DataBind();
            ddldoseUOM_drugs.Items.Insert(0, new ListItem("-", "0"));
            ddldoseUOM_drugs.SelectedValue = dt.Rows[0]["dozeuomId"].ToString();

            txt_formularium.Text = dt.Rows[0]["formularium"].ToString();
        }
    }

    protected void drugsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int index = Convert.ToInt32(e.RowIndex);
        gridItem temp = new gridItem();

        DataTable dt = Session[Helper.ViewStateDrug] as DataTable;
        var tempFormularium = dt.DefaultView.ToTable(true, "formularium");

        if (tempFormularium.Rows.Count == 1)
        {
            checkFormularium.Value = "Formularium";
        }

        Session[Helper.ViewStateDrug] = dt;
        //drugsGrid.DataSource = dt;
        //drugsGrid.DataBind();

        if (typeOrderSet.SelectedValue == "0")
        {
            List<gridItem> row = getRowList(1);
            DataTable dta = Helper.ToDataTable(row);
            dta.Rows[index].Delete();
            Session[Helper.ViewStateDrug] = dta;
            drugsGrid.DataSource = dta;
            drugsGrid.DataBind();
            txtSearchItem.Text = "";
        }
        else if (typeOrderSet.SelectedValue == "1")
        {
            
            List<gridItem> row = getRowList(0);

            if (row.Count > 1)
            {
                checkItemCount.Value = "More One";
            }
            else
            {
                checkItemCount.Value = "One";
            }

            DataTable dta = Helper.ToDataTable(row);
            dta.Rows[index].Delete();
            Session[Helper.ViewStateCompound] = dta;

            compoundGrid.DataSource = dta;
            compoundGrid.DataBind();
            txtSearchItem.Text = "";
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "drugsGrid_RowDeleting", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void compoundGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = Session[Helper.ViewStateCompound] as DataTable;
        dt.Rows[index].Delete();
        Session[Helper.ViewStateCompound] = dt;
        compoundGrid.DataSource = dt;
        compoundGrid.DataBind();

        if (dt.AsEnumerable().ToList().Count > 1)
        {
            checkItemCount.Value = "More One";
        }
        else
        {
            checkItemCount.Value = "One";
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "compoundGrid_RowDeleting", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void typeOrderSet_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Session[Helper.ViewStateOrderSetType] != "")
        //{
        //    typeOrderSet.SelectedValue = (String)Session[Helper.ViewStateOrderSetType];
        //}
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "changeView", "changeView();", true);
        List<gridItem> data = new List<gridItem>();
        DataTable dts = Helper.ToDataTable(data);
        drugsGrid.DataSource = dts;
        drugsGrid.DataBind();

        txt_Template_SOAP.Value = "";

    }

    //====================================================TAMBAHAN====================================================//

    #region fuzzysearch
    //--------------------------------------Fuzzy Search Function
    protected void buttonFindXX_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        string a = txtSearchItemXX.Text;
        DataTable dt = new DataTable();
        if (a == "")
        {
            dt = ((DataTable)Session[Helper.SessionDrug]).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
            GridViewXX.DataSource = dt;
            GridViewXX.DataBind();
        }
        else
        {
            //try
            //{
            //    dt = ((DataTable)Session[Helper.SessionDrug]).Select("salesItemName like '%" + a + "%' or activeIngredientsName like '%" + a + "%'").CopyToDataTable();
            //    GridViewXX.DataSource = dt;
            //}
            //catch
            //{
            //    GridViewXX.DataSource = null;
            //}
            //GridViewXX.DataBind();

            dt = (DataTable)Session[Helper.SessionDrug];

            string word = txtSearchItemXX.Text;
            DataTable pencarian = FuzzySearch(word, dt, 0.7);
            if (pencarian.Rows.Count > 0)
            {
                GridViewXX.DataSource = pencarian.Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
                GridViewXX.DataBind();
            }
            else
            {
                GridViewXX.DataSource = null;
                GridViewXX.DataBind();
            }

            //List<string> wordList = new List<string>
            //{
            //    "Panadol flu",
            //    "Panadol batuk",
            //    "Obat Panadol",
            //    "Paramex",
            //    "oskadon",
            //    "PANADOL 120MG CHEW TAB (CHERRY)",
            //    "PANADOL 160MG/5ML-60ML SYR",
            //    "PANADOL 500MG TAB",
            //    "PANADOL 80MG/0,8ML-15ML DROPS",
            //    "PANADOL COLD & FLU TAB",
            //    "PANADOL EXTRA TAB"
            //};

            //List<string> foundWords = FuzzySearch(word, wordList, 0.20);
            //StringBuilder hasil = new StringBuilder("");

            //foreach (string f in foundWords)
            //{
            //    hasil.Append(f + ", ");
            //}
            //ScriptManager.RegisterStartupScript(Page, GetType(), "test fuzzy", "alert('" + hasil + "')", true);

        }
        txtSearchItemXX.Focus();

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "buttonFindXX_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

        //Log.Info(LogConfig.LogEnd());
    }

    public static int LevenshteinDistance(string src, string dest)
    {
        int[,] d = new int[src.Length + 1, dest.Length + 1];
        int i, j, cost;
        char[] str1 = src.ToCharArray();
        char[] str2 = dest.ToCharArray();

        for (i = 0; i <= str1.Length; i++)
        {
            d[i, 0] = i;
        }
        for (j = 0; j <= str2.Length; j++)
        {
            d[0, j] = j;
        }
        for (i = 1; i <= str1.Length; i++)
        {
            for (j = 1; j <= str2.Length; j++)
            {

                if (str1[i - 1] == str2[j - 1])
                    cost = 0;
                else
                    cost = 1;

                d[i, j] =
                    Math.Min(
                        d[i - 1, j] + 1,              // Deletion
                        Math.Min(
                            d[i, j - 1] + 1,          // Insertion
                            d[i - 1, j - 1] + cost)); // Substitution

                if ((i > 1) && (j > 1) && (str1[i - 1] ==
                    str2[j - 2]) && (str1[i - 2] == str2[j - 1]))
                {
                    d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
                }
            }
        }

        return d[str1.Length, str2.Length];
    }

    public static DataTable FuzzySearch(string word, DataTable wordList, double fuzzyness)
    {
        DataTable foundWords = wordList.Clone();

        for (int i = 0; i < wordList.Rows.Count; i++)
        {
            string s = wordList.Rows[i]["salesItemName"].ToString();
            DataRow dr = wordList.Rows[i];

            // Calculate the Levenshtein-distance:
            int levenshteinDistance =
                LevenshteinDistance(word.ToUpper(), s.ToUpper());

            // Length of the longer string:
            int length = Math.Max(word.Length, s.Length);

            // Calculate the score:
            double score = 1.0 - (double)levenshteinDistance / length;

            // Match?
            if (score > fuzzyness)
            {
                foundWords.Rows.Add(wordList.Rows[i]["SalesItemID"], wordList.Rows[i]["SalesItemCode"], wordList.Rows[i]["SalesItemName"], wordList.Rows[i]["ActiveIngredientsname"], wordList.Rows[i]["SalesUomId"], wordList.Rows[i]["SalesUomCode"], wordList.Rows[i]["Dose"], wordList.Rows[i]["DoseText"], wordList.Rows[i]["DoseUomId"], wordList.Rows[i]["AdministrationFrequencyId"], wordList.Rows[i]["AdministrationRouteId"], wordList.Rows[i]["AdministrationInstruction"], wordList.Rows[i]["Formularium"]);
                //foundWords.Add(s);
            }
        }

        //foreach (string s in wordList)
        //{
        //    // Calculate the Levenshtein-distance:
        //    int levenshteinDistance =
        //        LevenshteinDistance(word, s);

        //    // Length of the longer string:
        //    int length = Math.Max(word.Length, s.Length);

        //    // Calculate the score:
        //    double score = 1.0 - (double)levenshteinDistance / length;

        //    // Match?
        //    if (score > fuzzyness)
        //        foundWords.Add(s);
        //}
        return foundWords;
    }
    #endregion

    //-----------------------------------------Auto Complete Function
    protected void ButtonAjaxSearch_Click(object sender, EventArgs e)
    {
        List<gridItem> row = new List<gridItem>();
        gridItem temp = new gridItem();
        DataTable wordList;

        wordList = ((DataTable)Session[Helper.SessionDrug]).Select("salesItemId = '" + HF_ItemSelected.Value + "'").CopyToDataTable();

        if (wordList.Rows.Count > 0)
        {
            for (int i = 0; i < wordList.Rows.Count; i++)
            {
                temp.salesItemId = long.Parse(wordList.Rows[i]["SalesItemID"].ToString());
                temp.salesItemName = wordList.Rows[i]["SalesItemName"].ToString();

                /* ========== Logic untuk pengecekan angka di belakang point decimal ========= */
                //Decimal doseDecimal = Convert.ToDecimal(wordList.Rows[i]["Dose"].ToString());
                //int doseInt = Convert.ToInt16(doseDecimal);

                //Decimal newDose = doseDecimal - doseInt;
                //String doseGrid = "";
                //if (newDose / 1 == 0)
                //    doseGrid = doseInt.ToString();
                //else
                //    doseGrid = wordList.Rows[i]["Dose"].ToString().Replace(",", ".");

                String doseGrid = "";
                doseGrid = Helper.formatDecimal(wordList.Rows[i]["Dose"].ToString().Replace(",", "."));
                /* ========== Logic untuk pengecekan angka di belakang point decimal ========= */

                temp.doze = doseGrid;
                temp.dozeuomId = Int64.Parse(wordList.Rows[i]["DoseUomId"].ToString());
                temp.frequencyId = Int64.Parse(wordList.Rows[i]["AdministrationFrequencyId"].ToString());
                temp.routeId = Int64.Parse(wordList.Rows[i]["AdministrationRouteId"].ToString());
                temp.quantity = "0";
                temp.iteration = 0;
                temp.order_set_detail_id = "";
                temp.uom = wordList.Rows[i]["SalesUomCode"].ToString();
                temp.uomId = long.Parse(wordList.Rows[i]["SalesUomId"].ToString());
                temp.formularium = wordList.Rows[i]["Formularium"].ToString();

                if (row.FindAll(x => x.formularium.Equals(wordList.Rows[i]["Formularium"].ToString())).Count == row.Count)
                {
                    checkFormularium.Value = "Formularium";
                }
                else
                {
                    checkFormularium.Value = "Not Formularium";
                }
            }

            DataTable dta = new DataTable();
            
            if (typeOrderSet.SelectedValue == "0")
            {
                row = getRowList(1);
                if (row.Where(x => x.salesItemName == temp.salesItemName).ToList().Count == 0)
                {
                    row.Add(temp);
                    dta = Helper.ToDataTable(row);

                    Session[Helper.ViewStateDrug] = dta;
                    drugsGrid.DataSource = dta;
                    drugsGrid.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);
                }
            }
            else if (typeOrderSet.SelectedValue == "1")
            {
                row = getRowList(2);

                if (row.Where(x => x.salesItemName == temp.salesItemName).ToList().Count == 0)
                {
                    row.Add(temp);
                    dta = Helper.ToDataTable(row);

                    Session[Helper.ViewStateCompound] = dta;
                    compoundGrid.DataSource = dta;
                    compoundGrid.DataBind();

                    if (row.Count > 1)
                    {
                        checkItemCount.Value = "More One";
                    }
                    else
                    {
                        checkItemCount.Value = "One";
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "existing", "alert('Item Already Exist');", true);
                }
            }
            
        }
    }


    protected void chk_is_dosetext_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_is_dosetext.Checked == true)
        {
            racikan_dosetext.Visible = true;
            div_dose_header.Visible = false;
        }
        else if (chk_is_dosetext.Checked == false)
        {
            racikan_dosetext.Visible = false;
            div_dose_header.Visible = true;
        }
    }

    protected void racikan_is_dosetext_CheckedChanged(object sender, EventArgs e)
    {
        //int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;

        List<gridItem> data = getRowList(2);
        DataTable dt = Helper.ToDataTable(data);

        Session[Helper.ViewStateCompound] = dt;
        compoundGrid.DataSource = dt;
        compoundGrid.DataBind();
    }

    protected void drug_is_dosetext_CheckedChanged(object sender, EventArgs e)
    {
        List<gridItem> data = getRowList(1);
        DataTable dt = Helper.ToDataTable(data);

        Session[Helper.ViewStateDrug] = dt;
        drugsGrid.DataSource = dt;
        drugsGrid.DataBind();
    }

    //protected void btnSave_Click1(object sender, EventArgs e)
    //{
    //    DataTable dataSave = new DataTable();
    //    bool statusDataSave = true;
    //    if (typeOrderSet.SelectedValue == "0" && drugsGrid.Rows.Count > 0)
    //    {
    //        dataSave = Session[Helper.ViewStateDrug] as DataTable;
    //        var quantityItem = (from DataRow dr in dataSave.Rows
    //                            where (string)dr["quantity"] == "0"
    //                            select dr["salesItemName"]);

    //        if (quantityItem != null)
    //        {
    //            statusDataSave = false;
    //        }

    //        var sameItem = dataSave.DefaultView.ToTable(true, "salesItemName");

    //        if (sameItem.Rows.Count != dataSave.Rows.Count)
    //        {
    //            statusDataSave = false;
    //        }
    //    }
    //    else if (typeOrderSet.SelectedValue == "1" && compoundGrid.Rows.Count > 0)
    //    {
    //        dataSave = Session[Helper.ViewStateCompound] as DataTable;
    //        var quantityItem = (from DataRow dr in dataSave.Rows
    //                            where (string)dr["quantity"] == "0"
    //                            select dr["salesItemName"]) as List<gridItem>;

    //        if (quantityItem.Count != 0)
    //        {
    //            statusDataSave = false;
    //        }

    //        var sameItem = dataSave.DefaultView.ToTable(true, "salesItemName");

    //        if (sameItem.Rows.Count != dataSave.Rows.Count)
    //        {
    //            statusDataSave = false;
    //        }
    //    }
    //    else
    //    {
    //        statusDataSave = true;
    //    }

    //    hf_check_data.Value = statusDataSave.ToString();
    //}
}