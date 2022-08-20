using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using static PatientHistory;
using static MedicalHistory;
using log4net;

public partial class Form_General_MedicalHistory : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public class gridMedical
    {
        public String itemName { get; set; }
        public Int64? quantity { get; set; }
        public String uom { get; set; }
        public String frequency { get; set; }
        public Int64? dose { get; set; }
        public String doseText { get; set; }
        public String instruction { get; set; }
        public String route { get; set; }
        public String iter { get; set; }
        public String orderDate { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        if (!IsPostBack)
        {
            //log4net.Config.XmlConfigurator.Configure();
            HyperLink test = Master.FindControl("MedicationHistoryLink") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");
            if (Request.QueryString["idPatient"] == null)
            {
                Response.Redirect("~/Form/General/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], "", Request.QueryString["PageSoapId"], Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
                hfPatientId.Value = Request.QueryString["idPatient"];
                hfEncounterId.Value = Request.QueryString["EncounterId"];
                hfAdmissionId.Value = Request.QueryString["AdmissionId"];
                hfPageSoapId.Value = Request.QueryString["PageSoapId"];
                Session.Remove(Helper.ViewStateListData);
                ResultPatientHeader JsongetPatientHistory;
                PatientHeader header = null;
                try
                {
                    //log.Debug(LogLibrary.Logging("S", "GetPatientHeader", Helper.GetLoginUser(this), ""));
                    var varResult = clsCommon.GetPatientHeader(long.Parse(hfPatientId.Value), hfEncounterId.Value.ToString());
                    JsongetPatientHistory = JsonConvert.DeserializeObject<ResultPatientHeader>(varResult.Result.ToString());
                    //log.Debug(LogLibrary.Logging("E", "GetPatientHeader", Helper.GetLoginUser(this), JsongetPatientHistory.ToString()));
                    header = JsongetPatientHistory.header;

                    Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Page_Load", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
                }
                catch (Exception ex)
                {
                    Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Page_Load", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
                    //log.Error(LogLibrary.Error("GetPatientHeader", Helper.GetLoginUser(this), ex.InnerException.Message));
                }
                //log.Info(LogLibrary.Logging("E", "GetPatientHeader", Helper.GetLoginUser(this), ""));

                
                 
                if (header != null)
                {
                    PatientCard.initializevalue(header);
                    getMedicalHistory(1, "1");
                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txt_admissionNo_doctorName.Text != "")
        {
            getMedicalHistory(2, txt_admissionNo_doctorName.Text);
        }
        else
        {
            getMedicalHistory(1, ddlEncounterMode.SelectedValue);
        }
            ddlEncounterMode.SelectedIndex = 0;
    }

    void getMedicalHistory(Int64 type, String value)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        List<MedicationHistory> listData = new List<MedicationHistory>();

        try
        {
            //log.Debug(LogLibrary.Logging("S", "getMedicalHistory", Helper.GetLoginUser(this), ""));
            var dataMedical = clsMedicalHistory.getMedicalHistoryNew(hfPatientId.Value);
            var JsonMedical = JsonConvert.DeserializeObject<ResultMedicationHistory>(dataMedical.Result.ToString());
            //log.Debug(LogLibrary.Logging("E", "getMedicalHistory", Helper.GetLoginUser(this), JsonMedical.ToString()));

            listData = JsonMedical.list;

            List<MedicationHistory> listPrescription = new List<MedicationHistory>();
            List<MedicationHistory> listCompound = new List<MedicationHistory>();
            List<MedicationHistory> listConsumables = new List<MedicationHistory>();

            StringBuilder compoundHtml = new StringBuilder();

            if (listData.Count > 0)
            {
                Session[Helper.ViewStateListData] = listData;
                List<String> listDate = listData.Select(x => String.Format("{0:ddd, MMM d, yyyy}", x.AdmissionDate)).Distinct().ToList();
                var dateTemp = String.Format("{0:ddd, MMM d, yyyy}", listData[0].AdmissionDate);

                DataTable dt_history = Helper.ToDataTable(listData);
                gvw_history.DataSource = dt_history;
                gvw_history.DataBind();

                foreach (String dataDate in listDate) //===== Take list by AdmissionDate
                {
                    List<MedicationHistory> listHistoryByDate = listData.FindAll(x => dataDate.Equals(String.Format("{0:ddd, MMM d, yyyy}", x.AdmissionDate)));

                    List<String> listAdmissionNo = listHistoryByDate.Select(x => x.AdmissionNo).Distinct().ToList();

                    foreach (String dataAdmissionNo in listAdmissionNo) //===== Take list by AdmissionNo
                    {
                        List<MedicationHistory> listHistoryByAdmission = listHistoryByDate.FindAll(x => dataAdmissionNo.Equals(x.AdmissionNo));

                        List<String> listDoctorName = listHistoryByAdmission.Select(x => x.MedicationDoctor).Distinct().ToList();

                        foreach (String datadoctorName in listDoctorName) //===== Take list by MedicineDoctor
                        {
                            List<MedicationHistory> listHistoryByDoctor = listHistoryByAdmission.FindAll(x => datadoctorName.Equals(x.MedicationDoctor));


                            compoundHtml.Append("<div class = " + "container-fluid" + " style=" + "padding-top:40px" + "><div style=" + "font-size:25px;" + "><b>" + dataDate + "</b></div><div style=" + "font-size:12px;" + ">" + dataAdmissionNo + " | " + datadoctorName + "</div>");

                            listPrescription = listHistoryByDoctor.FindAll(x => x.IsCompound.Equals("0") && x.IsConsumables.Equals("0"));
                            //listPrescription = listHistoryByDoctor.FindAll(x => x.isConsumables.Equals("0"));

                            listCompound = listHistoryByDoctor.FindAll(x => x.IsCompound.Equals("1"));
                            listConsumables = listHistoryByDoctor.FindAll(x => x.IsConsumables.Equals("1"));

                            GridView data_grid = new GridView();
                            DataTable dt = new DataTable();

                            if (listPrescription.Count > 0)
                            {
                                compoundHtml.Append("<br /><div class=" + "shadows" + " style = " + "background-color:white;" + "><div class=" + "container-fluid" + "><div style=" + "font-size:17px;" + "><b>Drug Prescription </b></div> <br />");

                                compoundHtml.Append("<table class=\"table table-striped table-condensed\"><tr><td><b>Item</b></td><td><b>QTY</b></td>");
                                compoundHtml.Append("<td><b>U.O.M</b></td><td><b>Frequency</b></td><td><b>Dose</b></td><td><b>Dose Text</b></td><td><b>Instruction</b></td><td><b>Route</b></td><td><b>iter</b></td><td><b>Order Date</b></td></tr>");
                                foreach (MedicationHistory data in listPrescription)
                                {
                                    string link = "javascript:Open('" + data.ItemName + "')";
                                    compoundHtml.Append("<tr><td>" + data.ItemName + "</td>");
                                    compoundHtml.Append("<td>" + data.Quantity + "</td>");
                                    compoundHtml.Append("<td>" + data.Uom + "</td>");
                                    compoundHtml.Append("<td>" + data.Frequency + "</td>");
                                    compoundHtml.Append("<td>" + data.Dose + "</td>");
                                    compoundHtml.Append("<td>" + data.DoseText + "</td>");
                                    compoundHtml.Append("<td>" + data.Instruction + "</td>");
                                    compoundHtml.Append("<td>" + data.Route + "</td>");
                                    compoundHtml.Append("<td>" + data.Iter + "</td>");
                                    compoundHtml.Append("<td>" + String.Format("{0:ddd, MMM d, yyyy}", data.OrderDate) + "</td></tr>");
                                }
                                compoundHtml.Append("</table>");
                                if (listCompound.Count != 0 || listConsumables.Count != 0)
                                {
                                    compoundHtml.Append("<br />");
                                }
                                else
                                {
                                    compoundHtml.Append("</div></div></div>");
                                }
                            }

                            if (listCompound.Count > 0)
                            {
                                compoundHtml.Append("<div style =\"background-color:white; font-size:17px;\"><b>Compound Prescription </b></div><br />");

                                compoundHtml.Append("<table class=\"table table-striped table-condensed\"><tr><td><b>Item</b></td><td><b>QTY</b></td>");
                                compoundHtml.Append("<td><b>U.O.M</b></td><td><b>Frequency</b></td><td><b>Dose</b></td><td><b>Dose Text</b></td>");
                                compoundHtml.Append("<td><b>Instruction</b></td><td><b>Route</b></td><td><b>iter</b></td><td><b>Order Date</b></td></tr>");
                                List<MedicationHistory> dataMedicalGrid = listCompound.FindAll(x => x.ItemId == 0);
                                foreach (MedicationHistory data in dataMedicalGrid)
                                {
                                    string link = "javascript:Open('" + data.CompoundName + "')";
                                    compoundHtml.Append("<tr><td><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline; \">" + data.CompoundName + "</a></td>");
                                    compoundHtml.Append("<td>" + data.Quantity + "</td>");
                                    compoundHtml.Append("<td>" + data.Uom + "</td>");
                                    compoundHtml.Append("<td>" + data.Frequency + "</td>");
                                    compoundHtml.Append("<td>" + data.Dose + "</td>");
                                    compoundHtml.Append("<td>" + data.DoseText + "</td>");
                                    compoundHtml.Append("<td>" + data.Instruction + "</td>");
                                    compoundHtml.Append("<td>" + data.Route + "</td>");
                                    compoundHtml.Append("<td>" + data.Iter + "</td>");
                                    compoundHtml.Append("<td>" + String.Format("{0:ddd, MMM d, yyyy}", data.OrderDate) + "</td></tr>");
                                }
                                compoundHtml.Append("</table>");

                                if (listConsumables.Count != 0 || listConsumables.Count != 0)
                                {
                                    compoundHtml.Append("<br />");
                                }
                                else
                                {
                                    compoundHtml.Append("</div></div></div>");
                                }
                            }

                            if (listConsumables.Count > 0)
                            {
                                compoundHtml.Append("<div style =\"background-color:white; font-size:17px;\"><b>Consumables </b></div><br />");

                                compoundHtml.Append("<table class=\"table table-striped table-condensed\"><tr><td><b>Item</b></td><td><b>QTY</b></td>");
                                compoundHtml.Append("<td><b>U.O.M</b></td><td><b>Frequency</b></td><td><b>Dose</b></td><td><b>Dose Text</b></td>");
                                compoundHtml.Append("<td><b>Instruction</b></td><td><b>Route</b></td><td><b>iter</b></td><td><b>Order Date</b></td></tr>");

                                foreach (MedicationHistory data in listConsumables)
                                {
                                    compoundHtml.Append("<tr><td>" + data.ItemName + "</td>");
                                    compoundHtml.Append("<td>" + data.Quantity + "</td>");
                                    compoundHtml.Append("<td>" + data.Uom + "</td>");
                                    compoundHtml.Append("<td>" + data.Frequency + "</td>");
                                    compoundHtml.Append("<td>" + data.Dose + "</td>");
                                    compoundHtml.Append("<td>" + data.DoseText + "</td>");
                                    compoundHtml.Append("<td>" + data.Instruction + "</td>");
                                    compoundHtml.Append("<td>" + data.Route + "</td>");
                                    compoundHtml.Append("<td>" + data.Iter + "</td>");
                                    compoundHtml.Append("<td>" + String.Format("{0:ddd, MMM d, yyyy}", data.OrderDate) + "</td></tr>");
                                }
                                compoundHtml.Append("</table>");
                                compoundHtml.Append("</div></div></div></div>");

                            }
                            listPrescription = new List<MedicationHistory>();
                            listCompound = new List<MedicationHistory>();
                            listConsumables = new List<MedicationHistory>();
                            listHistoryByDoctor = new List<MedicationHistory>();

                        }

                        listDoctorName = new List<string>();
                        listHistoryByAdmission = new List<MedicationHistory>();
                    }

                    listHistoryByDate = new List<MedicationHistory>();
                    listAdmissionNo = new List<string>();
                }
                prescription.InnerHtml = compoundHtml.ToString();

                listDate = new List<string>();
                Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "getMedicalHistory", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
            }
        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "getMedicalHistory", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
            //log.Error(LogLibrary.Error("getMedicalHistory", Helper.GetLoginUser(this), ex.InnerException.Message));
        }
        //log.Info(LogLibrary.Logging("E", "getMedicalHistory", Helper.GetLoginUser(this), ""));


    }
    
    protected void compoundDetail_Click(object sender, EventArgs e)
    {
        headerCompound.Text = "Header - " + hfCompoundName.Value;
        List<MedicalHistory> tempData = (List<MedicalHistory>)Session[Helper.ViewStateListData];
        List<MedicalHistory> detailCompoundData = tempData.FindAll(x => x.compoundName.Equals(hfCompoundName.Value) && x.itemId != 0);
        if (detailCompoundData.Count != 0)
        {
            DataTable dt = Helper.ToDataTable(detailCompoundData);
            gvw_detail_compound.DataSource = dt;
            gvw_detail_compound.DataBind();
        }
        
        MedicalHistory compoundData = tempData.FindLast(x => x.itemId == 0 && x.compoundName.Equals(hfCompoundName.Value));
        if (compoundData != null)
        {
            orderSetName.Text = compoundData.compoundName;
            qtyOrderSetName.Text = compoundData.quantity.ToString();
            uomOrderSetName.Text = compoundData.uom;
            frequencyOrderSetName.Text = compoundData.frequency;
            doseOrderSetName.Text = compoundData.dose.ToString();
            dose_textOrderSetName.Text = compoundData.doseText;
            instructionOrderSetName.Text = compoundData.instruction;
            routeOrderSetName.Text = compoundData.route;
            iterOrderSetName.Text = compoundData.itemName;
        }
    }

}