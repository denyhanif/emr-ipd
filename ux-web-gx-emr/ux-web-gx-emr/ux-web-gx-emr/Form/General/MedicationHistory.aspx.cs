using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using static PatientHistory;
using static MedicalHistory;
using System.Drawing;
using log4net;

public partial class Form_General_MedicationHistory : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public string setENG = "none";
    public string setIND = "none";
    public string isBahasa = "";

    string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();

        if (Session[Helper.SessionLanguage].ToString() == "1")
        {
            setENG = "";
            setIND = "none";
            isBahasa = "ENG";
            HFisBahasa.Value = "ENG";
        }
        else if (Session[Helper.SessionLanguage].ToString() == "2")
        {
            setENG = "none";
            setIND = "";
            isBahasa = "IND";
            HFisBahasa.Value = "IND";
        }
        else
        {
            setENG = "";
            setIND = "none";
            isBahasa = "ENG";
            HFisBahasa.Value = "ENG";
        }

        //set bahasa
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "bahasa", "switchBahasa();", true);

        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            HyperLink test = Master.FindControl("MedicationHistoryLink") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");

            if (Request.QueryString["EncounterId"] == null)
            {
                Response.Redirect("~/Form/General/Login.aspx", true);
                Context.ApplicationInstance.CompleteRequest();
            }
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
                Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], "", Request.QueryString["PageSoapId"], Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
                hfPatientId.Value = Request.QueryString["idPatient"];
                hfEncounterId.Value = Request.QueryString["EncounterId"];
                hfAdmissionId.Value = Request.QueryString["AdmissionId"];
                hfPageSoapId.Value = Request.QueryString["PageSoapId"];
                Session.Remove(Helper.ViewStateListData);
                ResponsePatientHeader JsongetPatientHistory;
                PatientHeader header = null;
                try
                {
                    Dictionary<string, string> logParam = new Dictionary<string, string>
                    {
                        { "Patient_ID", hfPatientId.Value },
                        { "Encounter_ID",  hfEncounterId.Value }
                    };
                    //Log.Debug(LogConfig.LogStart("GetPatientHeader", logParam));
                    var varResult = clsCommon.GetPatientHeader(long.Parse(hfPatientId.Value), hfEncounterId.Value.ToString());
                    JsongetPatientHistory = JsonConvert.DeserializeObject<ResponsePatientHeader>(varResult.Result.ToString());
                    //Log.Debug(LogConfig.LogEnd("GetPatientHeader", JsongetPatientHistory.Status, JsongetPatientHistory.Message));
                    header = JsongetPatientHistory.Data;

                    string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "Encounter_ID", hfEncounterId.Value, "Page_Load", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", varResult.Result.ToString()));
                }
                catch (Exception ex)
                {
                    //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
                    string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "Encounter_ID", hfEncounterId.Value, "Page_Load", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                }

                if (header != null)
                {
                    PatientCard.initializevalue(header);
                    getMedicalHistory(1, "1");
                }
            }

            //Log.Info(LogConfig.LogEnd());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //if (dlldoctor.SelectedValue != 0.ToString())
        //{
        //    getMedicalHistory(2, dlldoctor.SelectedValue);
        //}
        //else
        //{
        //    getMedicalHistory(1, ddlEncounterMode.SelectedValue);
        //}
        //ddlEncounterMode.SelectedIndex = 0;

        List<MedicationHistory> listData = new List<MedicationHistory>();
        List<MedicationHistory> listDataFiltered = new List<MedicationHistory>();

        List<string> listAdmission = new List<string>();
        listData = (List<MedicationHistory>)Session[Helper.SessionMedicalHistory];

        listAdmission = (from a in listData
                            where a.OrgCode == dllOrganizationCode.SelectedItem.ToString() 
                            && 
                            a.MedicationDoctor == ddldoctor.SelectedItem.ToString()
                            orderby a.AdmissionDate descending
                            select a.MedicationDoctor
                            ).Distinct().Take(int.Parse(ddlEncounterMode.SelectedValue.ToString())).ToList();

        listDataFiltered = (from a in listData
                            where listAdmission.Contains(a.MedicationDoctor)
                            select a
                            ).ToList();

        Session[Helper.SessionMedicalHistoryFiltered] = listDataFiltered;
        getMedicalHistoryFiltered();
    }

    protected void dllOrganizationCode_OnTextChange(object sender, EventArgs e)
    {
        List<MedicationHistory> listData = new List<MedicationHistory>();
        List<Doctor> listDoctor = new List<Doctor>();

        listData = (List<MedicationHistory>)Session[Helper.SessionMedicalHistory];
        listDoctor = (from a in listData
                      where a.OrgCode == dllOrganizationCode.SelectedItem.ToString()
                      group a by new { a.DoctorId, a.MedicationDoctor } into grouped
                      select new Doctor
                      { doctor_id = grouped.Key.DoctorId, name = grouped.Key.MedicationDoctor }
                      ).Distinct().ToList();

        DataTable dtdoctor = Helper.ToDataTable(listDoctor); ;
        ddldoctor.DataSource = dtdoctor;
        ddldoctor.DataTextField = "name";
        ddldoctor.DataValueField = "doctor_id";
        ddldoctor.DataBind();
        ddldoctor.Items.Insert(0, new ListItem("Select Doctor", "0"));
    }

    void getMedicalHistory(Int64 type, String value)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<MedicationHistory> listData = new List<MedicationHistory>();

        try
        {
            //Log.Debug(LogConfig.LogStart("getMedicalHistoryNew", LogConfig.LogParam("Patient_ID", hfPatientId.Value)));
            var dataMedical = clsMedicalHistory.getMedicalHistoryNew(hfPatientId.Value);
            var JsonMedical = JsonConvert.DeserializeObject<ResultMedicationHistory>(dataMedical.Result.ToString());
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "PatientId", hfPatientId.Value, "getMedicalHistory", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataMedical.Result.ToString()));
            //Log.Debug(LogConfig.LogEnd("getMedicalHistoryNew", JsonMedical.Status, JsonMedical.Message));

            listData = JsonMedical.list;

            bool isListDataEmpty = !listData.Any();
            if (isListDataEmpty)
            {
                gvw_history.DataSource = null;
                gvw_history.DataBind();

                //lblNoData.Visible = true;
                img_noData.Visible = true;
                no_patient_data.Visible = true;

            }
            else
            {
                //lblNoData.Visible = false;
                img_noData.Visible = false;
                no_patient_data.Visible = false;

                // show grid
                foreach (var templist in listData)
                {
                    templist.Quantity = Helper.formatDecimal(templist.Quantity);
                    templist.IssuedQuantity = Helper.formatDecimal(templist.IssuedQuantity);
                    templist.TotalQuantity = Helper.formatDecimal(templist.TotalQuantity);
                    templist.OutstandingQty = Helper.formatDecimal(templist.OutstandingQty);
                    templist.Dose = Helper.formatDecimal(templist.Dose);

                    //string[] tempqty = templist.Quantity.ToString().Split('.');
                    //string[] tempIssueQty = templist.IssuedQuantity.ToString().Split('.');
                    //string[] tempTotalQty = templist.TotalQuantity.ToString().Split('.');
                    //string[] tempOutstandingQty = templist.OutstandingQty.ToString().Split('.');
                    //string[] tempDose = templist.Dose.ToString().Split('.');

                    //if (tempqty[1].Length == 3)
                    //{
                    //    if (tempqty[1] == "000")
                    //    {
                    //        templist.Quantity = decimal.Parse(tempqty[0]).ToString();
                    //    }
                    //    else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                    //    {
                    //        templist.Quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                    //    }
                    //    else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                    //    {
                    //        templist.Quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                    //    }
                    //}

                    //if (tempIssueQty[1].Length == 3)
                    //{
                    //    if (tempIssueQty[1] == "000")
                    //    {
                    //        templist.IssuedQuantity = decimal.Parse(tempIssueQty[0]).ToString();
                    //    }
                    //    else if (tempIssueQty[1].Substring(tempIssueQty[1].Length - 2) == "00")
                    //    {
                    //        templist.IssuedQuantity = tempIssueQty[0] + "." + tempIssueQty[1].Substring(0, 1);
                    //    }
                    //    else if (tempIssueQty[1].Substring(tempIssueQty[1].Length - 1) == "0")
                    //    {
                    //        templist.IssuedQuantity = tempIssueQty[0] + "." + tempIssueQty[1].Substring(0, 2);
                    //    }
                    //}

                    //if (tempTotalQty[1].Length == 3)
                    //{
                    //    if (tempTotalQty[1] == "000")
                    //    {
                    //        templist.TotalQuantity = decimal.Parse(tempTotalQty[0]).ToString();
                    //    }
                    //    else if (tempTotalQty[1].Substring(tempTotalQty[1].Length - 2) == "00")
                    //    {
                    //        templist.TotalQuantity = tempTotalQty[0] + "." + tempTotalQty[1].Substring(0, 1);
                    //    }
                    //    else if (tempTotalQty[1].Substring(tempTotalQty[1].Length - 1) == "0")
                    //    {
                    //        templist.TotalQuantity = tempTotalQty[0] + "." + tempTotalQty[1].Substring(0, 2);
                    //    }
                    //}

                    //if (tempOutstandingQty[1].Length == 3)
                    //{
                    //    if (tempOutstandingQty[1] == "000")
                    //    {
                    //        templist.OutstandingQty = decimal.Parse(tempOutstandingQty[0]).ToString();
                    //    }
                    //    else if (tempOutstandingQty[1].Substring(tempOutstandingQty[1].Length - 2) == "00")
                    //    {
                    //        templist.OutstandingQty = tempOutstandingQty[0] + "." + tempOutstandingQty[1].Substring(0, 1);
                    //    }
                    //    else if (tempOutstandingQty[1].Substring(tempOutstandingQty[1].Length - 1) == "0")
                    //    {
                    //        templist.OutstandingQty = tempOutstandingQty[0] + "." + tempOutstandingQty[1].Substring(0, 2);
                    //    }
                    //}

                    //if (tempDose[1].Length == 3)
                    //{
                    //    if (tempDose[1] == "000")
                    //    {
                    //        templist.Dose = decimal.Parse(tempDose[0]).ToString();
                    //    }
                    //    else if (tempDose[1].Substring(tempDose[1].Length - 2) == "00")
                    //    {
                    //        templist.Dose = tempDose[0] + "." + tempDose[1].Substring(0, 1);
                    //    }
                    //    else if (tempDose[1].Substring(tempDose[1].Length - 1) == "0")
                    //    {
                    //        templist.Dose = tempDose[0] + "." + tempDose[1].Substring(0, 2);
                    //    }
                    //}
                }

                Session[Helper.SessionMedicalHistory] = listData;

                List<MedicationHistory> listPrescription = new List<MedicationHistory>();
                List<MedicationHistory> listCompound = new List<MedicationHistory>();
                List<MedicationHistory> listConsumables = new List<MedicationHistory>();

                List<DrugsFilter> ddlDrugPres = new List<DrugsFilter>();

                ddlDrugPres = (from a in listData
                               where a.ItemId != 0 && a.IsCompound != "1"
                               orderby a.ItemName
                               group a by new { a.ItemId, a.ItemName } into grouped
                               select new DrugsFilter
                               { itemId = grouped.Key.ItemId, itemName = grouped.Key.ItemName }
                               ).Distinct().ToList();

                ddlDrugs.DataSource = Helper.ToDataTable(ddlDrugPres);
                ddlDrugs.DataTextField = "itemName";
                ddlDrugs.DataValueField = "ItemId";
                ddlDrugs.DataBind();
                ddlDrugs.Items.Insert(0, new ListItem("-- All Items --", "0"));

                List<Organization> listOrganization = new List<Organization>();

                listOrganization = (from a in listData
                                    group a by new { a.OrganizationId, a.OrgCode } into grouped
                                    select new Organization
                                    { organizationId = grouped.Key.OrganizationId, code = grouped.Key.OrgCode }
                                    ).Distinct().ToList();

                DataTable dtorganization = Helper.ToDataTable(listOrganization);
                dllOrganizationCode.DataSource = dtorganization;
                dllOrganizationCode.DataTextField = "code";
                dllOrganizationCode.DataValueField = "organizationId";
                dllOrganizationCode.DataBind();
                dllOrganizationCode.Items.Insert(0, new ListItem("- select -", "0"));

                StringBuilder compoundHtml = new StringBuilder();

                if (listData.Count > 0)
                {
                    Session[Helper.ViewStateListData] = listData;
                    List<String> listDate = listData.Select(x => String.Format("{0:ddd, dd MMM yyyy}", x.AdmissionDate)).Distinct().ToList();
                    var dateTemp = String.Format("{0:ddd, dd MMM yyyy}", listData[0].AdmissionDate);

                    DataTable dt_history = Helper.ToDataTable(listData);
                    gvw_history.DataSource = dt_history;
                    gvw_history.DataBind();

                    foreach (String dataDate in listDate) //===== Take list by AdmissionDate
                    {
                        List<MedicationHistory> listHistoryByDate = listData.FindAll(x => dataDate.Equals(String.Format("{0:ddd, dd MMM yyyy}", x.AdmissionDate)));

                        List<String> listAdmissionNo = listHistoryByDate.Select(x => x.AdmissionNo).Distinct().ToList();

                        foreach (String dataAdmissionNo in listAdmissionNo) //===== Take list by AdmissionNo
                        {
                            List<MedicationHistory> listHistoryByAdmission = listHistoryByDate.FindAll(x => dataAdmissionNo.Equals(x.AdmissionNo));

                            List<String> listDoctorName = listHistoryByAdmission.Select(x => x.MedicationDoctor).Distinct().ToList();

                            foreach (String datadoctorName in listDoctorName) //===== Take list by MedicineDoctor
                            {
                                List<MedicationHistory> listHistoryByDoctor = listHistoryByAdmission.FindAll(x => datadoctorName.Equals(x.MedicationDoctor));

                                string payerNameText = (from alist in listData
                                                select alist.PayerName
                                                ).FirstOrDefault();

                                string checkverified = "";
                                if (listHistoryByDoctor[0].IsVerified == true)
                                {
                                    checkverified = "<i class=\"fa fa-check-square-o\" style=\"color: #4d9b35;font-size: 20px;\" title=\"Verified by Pharmacy\"></i>";
                                }
                                compoundHtml.Append("<div class = " + "container-fluid" + " style=" + "margin-top:10px;" + "padding-bottom:0px;" + "><div style=" + "font-size:25px;" + "><b>" + dataDate + "</b> " + checkverified + " </div><div style=" + "font-size:12px;" + ">" + dataAdmissionNo + " | " + datadoctorName + " | " + payerNameText + "</div>");

                                listPrescription = listHistoryByDoctor.FindAll(x => x.IsCompound.Equals("0") && x.IsConsumables.Equals("0"));
                                listCompound = listHistoryByDoctor.FindAll(x => x.IsCompound.Equals("1"));
                                listConsumables = listHistoryByDoctor.FindAll(x => x.IsConsumables.Equals("1"));

                                GridView data_grid = new GridView();
                                DataTable dt = new DataTable();

                                if (listPrescription.Count > 0)
                                {
                                    compoundHtml.Append("<div class=\"shadows\" style=\"background-color:white; margin-bottom:5px; margin-top:8px; max-height:350px; overflow:auto; padding-left:0px; padding-right:0px;\"><div><div style=\"font-size:17px; padding-left: 10px; padding-bottom: 5px;\"><b> <label style=\"display: " + setENG + "\"> Drug Prescription </label> <label style=\"display: " + setIND + "\"> Resep Obat </label> </b></div>");

                                    /// INI DENGAN ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                    //if (isBahasa == "ENG")
                                    //{
                                    //    compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" >" +
                                    //        "<tr>" +
                                    //        "<td><b>Organization</b></td>" +
                                    //        "<td><b>Transac. Date</b></td>");
                                    //    compoundHtml.Append("" +
                                    //        "<td><b>Item</b></td>" +
                                    //        "<td><b>Dose</b></td>" +
                                    //        "<td><b>Dose UOM</b></td>" +
                                    //        "<td><b>Frequency</b></td>" +
                                    //        "<td><b>Route</b></td>" +
                                    //        "<td><b>Instruction</b></td>" +
                                    //        "<td><b>R/Qty</b></td>" +
                                    //        "<td><b>U.O.M</b></td>" +
                                    //        "<td><b>Issue</b></td>" +
                                    //        "<td><b>Return</b></td>" +
                                    //        "<td><b>Outstd.</b></td>" +
                                    //        "<td><b>Iter</b></td>" +
                                    //        "<td><b>Routine</b></td>" +
                                    //        "</tr>");
                                    //}
                                    //else if (isBahasa == "IND")
                                    //{
                                    //    compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" >" +
                                    //        "<tr>" +
                                    //        "<td><b>Organisasi</b></td>" +
                                    //        "<td><b>Tgl. Transaksi</b></td>");
                                    //    compoundHtml.Append("" +
                                    //        "<td><b>Obat</b></td>" +
                                    //        "<td><b>Dosis</b></td>" +
                                    //        "<td><b>Unit Dosis</b></td>" +
                                    //        "<td><b>Frekuensi</b></td>" +
                                    //        "<td><b>Rute</b></td>" +
                                    //        "<td><b>Instruksi</b></td>" +
                                    //        "<td><b>Jml</b></td>" +
                                    //        "<td><b>Unit</b></td>" +
                                    //        "<td><b>Issue</b></td>" +
                                    //        "<td><b>Retur</b></td>" +
                                    //        "<td><b>Sisa</b></td>" +
                                    //        "<td><b>Iter</b></td>" +
                                    //        "<td><b>Rutin</b></td>" +
                                    //        "</tr>");
                                    //}

                                    /// INI TANPA ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA

                                    if (isBahasa == "ENG")
                                    {
                                        compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" >" +
                                            "<tr>" +
                                            "<td><b>Organization</b></td>" +
                                            "<td><b>Transac. Date</b></td>");
                                        compoundHtml.Append("" +
                                            "<td><b>Item</b></td>" +
                                            "<td><b>Dose</b></td>" +
                                            //"<td><b>Dose UOM</b></td>" +
                                            "<td><b>Frequency</b></td>" +
                                            "<td><b>Route</b></td>" +
                                            "<td><b>Instruction</b></td>" +
                                            "<td><b>R/Qty</b></td>" +
                                            "<td><b>U.O.M</b></td>" +
                                            "<td><b>Iter</b></td>" +
                                            "<td><b>Routine</b></td>" +
                                            "</tr>");
                                    }
                                    else if (isBahasa == "IND")
                                    {
                                        compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" >" +
                                            "<tr>" +
                                            "<td><b>Organisasi</b></td>" +
                                            "<td><b>Tgl. Transaksi</b></td>");
                                        compoundHtml.Append("" +
                                            "<td><b>Obat</b></td>" +
                                            "<td><b>Dosis</b></td>" +
                                            //"<td><b>Unit Dosis</b></td>" +
                                            "<td><b>Frekuensi</b></td>" +
                                            "<td><b>Rute</b></td>" +
                                            "<td><b>Instruksi</b></td>" +
                                            "<td><b>Jml</b></td>" +
                                            "<td><b>Unit</b></td>" +
                                            "<td><b>Iter</b></td>" +
                                            "<td><b>Rutin</b></td>" +
                                            "</tr>");
                                    }

                                    foreach (MedicationHistory data in listPrescription)
                                    {
                                        string link = "javascript:Open('" + data.ItemName + "')";

                                        //INI DENGAN ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA

                                        //compoundHtml.Append("<tr><td style=\"width:84px\">" + data.OrgCode + "</td>");
                                        //compoundHtml.Append("<td style=\"width:110px\">" + String.Format("{0: dd MMM yyyy}", data.OrderDate) + "</td>");
                                        //compoundHtml.Append("<td style=\"width:250px\">" + data.ItemName + "</td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.Dose + "</td>");
                                        //compoundHtml.Append("<td style=\"width:85px\">" + data.dose_uom + "</td>");
                                        //compoundHtml.Append("<td style=\"width:80px\">" + data.Frequency + "</td>");
                                        //compoundHtml.Append("<td style=\"width:95px\">" + data.Route + "</td>");
                                        //compoundHtml.Append("<td style=\"width:250px\" > " + data.Instruction + "</td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.Quantity + "</td>");
                                        //compoundHtml.Append("<td style=\"width:50px\">" + data.Uom + "</td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.IssuedQuantity + "</td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.ReturnQty + "</td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.OutstandingQty + "</td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.Iter + "</td>");
                                        //if (data.IsRoutine.ToLower() == "Not Routine".ToLower())
                                        //{
                                        //    compoundHtml.Append("<td style=\"width:85px\">" + "No" + "</td></tr>");
                                        //}
                                        //else if (data.IsRoutine.ToLower() == "Routine".ToLower())
                                        //{
                                        //    compoundHtml.Append("<td style=\"width:85px\">" + "Yes" + "</td></tr>");
                                        //}

                                        //INI TANNPA ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA

                                        string dosepres = "";
                                        if (data.IsDoseText == true)
                                        {
                                            dosepres = data.DoseText;
                                        }
                                        else if (data.IsDoseText == false)
                                        {
                                            dosepres = data.Dose + " " + data.dose_uom;
                                        }

                                        compoundHtml.Append("<tr><td style=\"width:84px\">" + data.OrgCode + "</td>");
                                        compoundHtml.Append("<td style=\"width:110px\">" + String.Format("{0: dd MMM yyyy}", data.OrderDate) + "</td>");
                                        compoundHtml.Append("<td style=\"width:250px\">" + data.ItemName + "</td>");
                                        compoundHtml.Append("<td style=\"width:125px\">" + dosepres + "</td>");
                                        //compoundHtml.Append("<td style=\"width:85px\">" + data.dose_uom + "</td>");
                                        compoundHtml.Append("<td style=\"width:80px\">" + data.Frequency + "</td>");
                                        compoundHtml.Append("<td style=\"width:95px\">" + data.Route + "</td>");
                                        compoundHtml.Append("<td style=\"width:250px\" > " + data.Instruction + "</td>");
                                        compoundHtml.Append("<td style=\"width:40px\">" + data.Quantity + "</td>");
                                        compoundHtml.Append("<td style=\"width:50px\">" + data.Uom + "</td>");                                                    
                                        
                                        compoundHtml.Append("<td style=\"width:40px\">" + data.Iter + "</td>");
                                        if (data.IsRoutine.ToLower() == "Not Routine".ToLower())
                                        {
                                            compoundHtml.Append("<td style=\"width:85px\">" + "No" + "</td></tr>");
                                        }
                                        else if (data.IsRoutine.ToLower() == "Routine".ToLower())
                                        {
                                            compoundHtml.Append("<td style=\"width:85px\">" + "Yes" + "</td></tr>");
                                        }
                                    }
                                    compoundHtml.Append("</table></div>");
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
                                    if (listPrescription.Count == 0)
                                    {
                                        compoundHtml.Append("<div class=\"shadows\" style=\"background-color:white; margin-bottom:5px; margin-top:8px; max-height:350px; overflow:auto; padding-left:0px; padding-right:0px;\"><div>");
                                    }
                                    
                                    compoundHtml.Append("<div style=\" font-size:17px; padding-left: 10px; padding-bottom: 5px;\"><b> <label style=\"display: " + setENG + "\"> Compound Prescription </label> <label style=\"display: " + setIND + "\"> Resep Racikan </label> </b></div>");
                                    

                                    //INI DENGAN ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                    //if (isBahasa == "ENG")
                                    //{
                                    //    compoundHtml.Append("<div style=\"display:none; border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organization</b></td><td><b>Transac. Date</b></td>");
                                    //    compoundHtml.Append("" +
                                    //        "<td><b>Item</b></td>" +
                                    //        "<td><b>Dose</b></td>" +
                                    //        "<td><b>Dose UOM</b></td>" +
                                    //        "<td><b>Frequency</b></td>" +
                                    //        "<td><b>Route</b></td>" +
                                    //        "<td><b>Instruction</b></td>" +
                                    //        "<td><b>R/Qty</b></td>" +
                                    //        "<td><b>U.O.M</b></td>" +
                                    //        "<td><b>Issue</b></td>" +
                                    //        "<td><b>Return</b></td>" +
                                    //        "<td><b>Outstd.</b></td>" +
                                    //        "<td><b>Iter</b></td>" +
                                    //        "<td><b>Routine</b></td>" +
                                    //        "</tr>");
                                    //}
                                    //else if (isBahasa == "IND")
                                    //{
                                    //    compoundHtml.Append("<div style=\"display:none; border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organisasi</b></td><td><b>Tgl. Transaksi</b></td>");
                                    //    compoundHtml.Append("" +
                                    //        "<td><b>Obat</b></td>" +
                                    //        "<td><b>Dosis</b></td>" +
                                    //        "<td><b>Unit Dosis</b></td>" +
                                    //        "<td><b>Frekuensi</b></td>" +
                                    //        "<td><b>Rute</b></td>" +
                                    //        "<td><b>Instruksi</b></td>" +
                                    //        "<td><b>Jml</b></td>" +
                                    //        "<td><b>Unit</b></td>" +
                                    //        "<td><b>Issue</b></td>" +
                                    //        "<td><b>Retur</b></td>" +
                                    //        "<td><b>Sisa</b></td>" +
                                    //        "<td><b>Iter</b></td>" +
                                    //        "<td><b>Rutin</b></td>" +
                                    //        "</tr>");
                                    //}

                                    //INI TANPA ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                    if (isBahasa == "ENG")
                                    {
                                        compoundHtml.Append("<div style=\" border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organization</b></td><td><b>Transac. Date</b></td>");
                                        compoundHtml.Append("" +
                                            "<td><b>Item</b></td>" +
                                            "<td><b>Dose</b></td>" +
                                            //"<td><b>Dose UOM</b></td>" +
                                            "<td><b>Frequency</b></td>" +
                                            "<td><b>Route</b></td>" +
                                            "<td><b>Instruction</b></td>" +
                                            "<td><b>R/Qty</b></td>" +
                                            "<td><b>U.O.M</b></td>" +
                                            "<td><b>Iter</b></td>" +
                                            "<td><b>Routine</b></td>" +
                                            "</tr>");
                                    }
                                    else if (isBahasa == "IND")
                                    {
                                        compoundHtml.Append("<div style=\" border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organisasi</b></td><td><b>Tgl. Transaksi</b></td>");
                                        compoundHtml.Append("" +
                                            "<td><b>Obat</b></td>" +
                                            "<td><b>Dosis</b></td>" +
                                            //"<td><b>Unit Dosis</b></td>" +
                                            "<td><b>Frekuensi</b></td>" +
                                            "<td><b>Rute</b></td>" +
                                            "<td><b>Instruksi</b></td>" +
                                            "<td><b>Jml</b></td>" +
                                            "<td><b>Unit</b></td>" +
                                            "<td><b>Iter</b></td>" +
                                            "<td><b>Rutin</b></td>" +
                                            "</tr>");
                                    }

                                    List<MedicationHistory> dataRacikanHeader = listCompound.FindAll(x => x.ItemId == 0 && x.IsCompoundHeader == true);
                                    List<MedicationHistory> dataRacikanDetail = listCompound.FindAll(x => x.IsCompoundHeader == false);

                                    foreach (MedicationHistory data in dataRacikanHeader)
                                    {
                                        string link = "javascript:Open('" + data.CompoundName + "')";

                                        //INI DENGAN ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                        //compoundHtml.Append("<tr><td style=\"width:84px\">" + data.OrgCode + "</td>");
                                        //compoundHtml.Append("<td style=\"width:110px\">" + String.Format("{0: dd MMM yyyy}", data.OrderDate) + "</td>");
                                        //compoundHtml.Append("<td style=\"width:250px\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline; \">" + data.CompoundName + "</a></td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.Dose + "</td>");
                                        //compoundHtml.Append("<td style=\"width:85px\">" + data.dose_uom + "</td>");
                                        //compoundHtml.Append("<td style=\"width:80px\">" + data.Frequency + "</td>");
                                        //compoundHtml.Append("<td style=\"width:95px\">" + data.Route + "</td>");
                                        //compoundHtml.Append("<td style=\"width:250px\">" + data.Instruction + "</td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.Quantity + "</td>");
                                        //compoundHtml.Append("<td style=\"width:50px\">" + data.Uom + "</td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.IssuedQuantity + "</td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.ReturnQty + "</td>");                                        
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.OutstandingQty + "</td>");                                        
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.Iter + "</td>");
                                        //if (data.IsRoutine.ToLower() == "Not Routine".ToLower())
                                        //{
                                        //    compoundHtml.Append("<td style=\"width:85px\">" + "No" + "</td></tr>");
                                        //}
                                        //else if (data.IsRoutine.ToLower() == "Routine".ToLower())
                                        //{
                                        //    compoundHtml.Append("<td style=\"width:85px\">" + "Yes" + "</td></tr>");
                                        //}

                                        //INI TANPA ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA

                                        string doseheader = "";
                                        if (data.IsDoseText == true)
                                        {
                                            doseheader = data.DoseText;
                                        }
                                        else if(data.IsDoseText == false)
                                        {
                                            doseheader = data.Dose + " " + data.dose_uom;
                                        }
                                        compoundHtml.Append("<tr><td style=\"width:84px\">" + data.OrgCode + "</td>");
                                        compoundHtml.Append("<td style=\"width:110px\">" + String.Format("{0: dd MMM yyyy}", data.OrderDate) + "</td>");
                                        //compoundHtml.Append("<td style=\"width:250px\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline; \">" + data.CompoundName + "</a></td>");
                                        compoundHtml.Append("<td style=\"width:205px\">" + data.CompoundName + "</td>");
                                        compoundHtml.Append("<td style=\"width:125px\">" + doseheader + "</td>");
                                        //compoundHtml.Append("<td style=\"width:85px\">" + data.dose_uom + "</td>");
                                        compoundHtml.Append("<td style=\"width:80px\">" + data.Frequency + "</td>");
                                        compoundHtml.Append("<td style=\"width:95px\">" + data.Route + "</td>");
                                        compoundHtml.Append("<td style=\"width:250px\">" + data.Instruction + "</td>");
                                        compoundHtml.Append("<td style=\"width:40px\">" + data.Quantity + "</td>");
                                        compoundHtml.Append("<td style=\"width:50px\">" + data.Uom + "</td>");
                                        compoundHtml.Append("<td style=\"width:40px\">" + data.Iter + "</td>");
                                        if (data.IsRoutine.ToLower() == "Not Routine".ToLower())
                                        {
                                            compoundHtml.Append("<td style=\"width:85px\">" + "No" + "</td></tr>");
                                        }
                                        else if (data.IsRoutine.ToLower() == "Routine".ToLower())
                                        {
                                            compoundHtml.Append("<td style=\"width:85px\">" + "Yes" + "</td></tr>");
                                        }

                                        List<MedicationHistory> dataRacikanDetailSelected = dataRacikanDetail.FindAll(x => x.CompoundId == data.CompoundId);
                                        if (dataRacikanDetailSelected.Count > 0)
                                        {
                                            compoundHtml.Append("<tr> <td colspan=\"12\">");
                                            compoundHtml.Append("<table border=\"0\" style=\"margin-left: 220px; display:inline-block; vertical-align:top;\">");
                                            compoundHtml.Append("<tr><td style=\"padding:2px 10px; font-weight:bold; min-width: 240px;\"> Item </td> <td style=\"padding:2px 10px; font-weight:bold; min-width: 100px;\"> Dose </td></tr>");
                                            foreach (MedicationHistory detail in dataRacikanDetailSelected)
                                            {
                                                string dosedetail = "";
                                                if (detail.IsDoseText == true)
                                                {
                                                    dosedetail = detail.DoseText;
                                                }
                                                else if (detail.IsDoseText == false)
                                                {
                                                    dosedetail = detail.Dose + " " + detail.dose_uom;
                                                }
                                                compoundHtml.Append("<tr><td style=\"padding:2px 10px;\"> • " + detail.ItemName + "</td> <td style=\"padding:2px 10px;\"> " + dosedetail + " </td> </tr>");
                                            }
                                            compoundHtml.Append("</table>");
                                            compoundHtml.Append("<table border=\"0\" style=\"display:inline-block; vertical-align:top;\">");
                                            compoundHtml.Append("<tr><td style=\"padding:2px 10px; font-weight:bold; min-width: 240px;\"> Instruction </td> </tr>");
                                            compoundHtml.Append("<tr><td style=\"padding:2px 10px; \"> " + data.CompoundNote + " </td> </tr>");
                                            compoundHtml.Append("</table>");
                                            compoundHtml.Append("</td> </tr>");
                                        }
                                    }
                                    compoundHtml.Append("</table></div>");

                                    if (listConsumables.Count != 0)
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
                                    if (listPrescription.Count == 0 && listCompound.Count == 0)
                                    {
                                        compoundHtml.Append("<div class=\"shadows\" style=\"background-color:white; margin-bottom:5px; margin-top:8px; max-height:350px; overflow:auto; padding-left:0px; padding-right:0px;\"><div>");
                                    }

                                    compoundHtml.Append("<div style=\"font-size:17px; padding-left: 10px; padding-bottom: 5px;\"><b> <label style=\"display: " + setENG + "\"> Consumables </label> <label style=\"display: " + setIND + "\"> Alat Kesehatan </label> </b></div>");

                                    //INI DENGAN ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                    //if (isBahasa == "ENG")
                                    //{
                                    //    compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organization</b></td><td><b>Transac. Date</b></td>");
                                    //    compoundHtml.Append("" +
                                    //        "<td><b>Item</b></td>" +
                                    //        "<td><b>Dose</b></td>" +
                                    //        "<td><b>Dose UOM</b></td>" +
                                    //        "<td><b>Frequency</b></td>" +
                                    //        "<td><b>Route</b></td>" +
                                    //        "<td><b>Instruction</b></td>" +
                                    //        "<td><b>R/Qty</b></td>" +
                                    //        "<td><b>U.O.M</b></td>" +
                                    //        "<td><b>Issue</b></td>" +
                                    //        "<td><b>Return</b></td>" +
                                    //        "<td><b>Outstd.</b></td>" +
                                    //        "<td><b>Iter</b></td>" +
                                    //        "<td><b>Routine</b></td>" +
                                    //        "</tr>");
                                    //}
                                    //else if (isBahasa == "IND")
                                    //{
                                    //    compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organisasi</b></td><td><b>Tgl. Transaksi</b></td>");
                                    //    compoundHtml.Append("" +
                                    //        "<td><b>Alkes</b></td>" +
                                    //        "<td><b>Dosis</b></td>" +
                                    //        "<td><b>Unit Dosis</b></td>" +
                                    //        "<td><b>Frekuensi</b></td>" +
                                    //        "<td><b>Rute</b></td>" +
                                    //        "<td><b>Instruksi</b></td>" +
                                    //        "<td><b>Jml</b></td>" +
                                    //        "<td><b>Unit</b></td>" +
                                    //        "<td><b>Issue</b></td>" +
                                    //        "<td><b>Retur</b></td>" +                                            
                                    //        "<td><b>Sisa</b></td>" +
                                    //        "<td><b>Iter</b></td>" +
                                    //        "<td><b>Rutin</b></td>" +
                                    //        "</tr>");
                                    //}

                                    //INI TANPA ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                    if (isBahasa == "ENG")
                                    {
                                        compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organization</b></td><td><b>Transac. Date</b></td>");
                                        compoundHtml.Append("" +
                                            "<td><b>Item</b></td>" +
                                            "<td><b>Dose</b></td>" +
                                            "<td><b>Dose UOM</b></td>" +
                                            "<td><b>Frequency</b></td>" +
                                            "<td><b>Route</b></td>" +
                                            "<td><b>Instruction</b></td>" +
                                            "<td><b>R/Qty</b></td>" +
                                            "<td><b>U.O.M</b></td>" +
                                            "<td><b>Iter</b></td>" +
                                            "<td><b>Routine</b></td>" +
                                            "</tr>");
                                    }
                                    else if (isBahasa == "IND")
                                    {
                                        compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organisasi</b></td><td><b>Tgl. Transaksi</b></td>");
                                        compoundHtml.Append("" +
                                            "<td><b>Alkes</b></td>" +
                                            "<td><b>Dosis</b></td>" +
                                            "<td><b>Unit Dosis</b></td>" +
                                            "<td><b>Frekuensi</b></td>" +
                                            "<td><b>Rute</b></td>" +
                                            "<td><b>Instruksi</b></td>" +
                                            "<td><b>Jml</b></td>" +
                                            "<td><b>Unit</b></td>" +
                                            "<td><b>Iter</b></td>" +
                                            "<td><b>Rutin</b></td>" +
                                            "</tr>");
                                    }

                                    foreach (MedicationHistory data in listConsumables)
                                    {
                                        //INI DENGAN ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                        //compoundHtml.Append("<tr><td style=\"width:84px\">" + data.OrgCode + "</td>");
                                        //compoundHtml.Append("<td style=\"width:110px\">" + String.Format("{0: dd MMM yyyy}", data.OrderDate) + "</td>");
                                        //compoundHtml.Append("<td style=\"width:250px\">" + data.ItemName + "</td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.Dose + "</td>");
                                        //compoundHtml.Append("<td style=\"width:85px\">" + data.dose_uom + "</td>");
                                        //compoundHtml.Append("<td style=\"width:80px\">" + data.Frequency + "</td>");
                                        //compoundHtml.Append("<td style=\"width:95px\">" + data.Route + "</td>");
                                        //compoundHtml.Append("<td style=\"width:250px\">" + data.Instruction + "</td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.Quantity + "</td>");
                                        //compoundHtml.Append("<td style=\"width:50px\">" + data.Uom + "</td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.IssuedQuantity + "</td>");
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.ReturnQty + "</td>");                                        
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.OutstandingQty + "</td>");                                        
                                        //compoundHtml.Append("<td style=\"width:40px\">" + data.Iter + "</td>");
                                        //if (data.IsRoutine.ToLower() == "Not Routine".ToLower())
                                        //{
                                        //    compoundHtml.Append("<td style=\"width:85px\">" + "No" + "</td></tr>");
                                        //}
                                        //else if (data.IsRoutine.ToLower() == "Routine".ToLower())
                                        //{
                                        //    compoundHtml.Append("<td style=\"width:85px\">" + "Yes" + "</td></tr>");
                                        //}

                                        //INI TANPA ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                        compoundHtml.Append("<tr><td style=\"width:84px\">" + data.OrgCode + "</td>");
                                        compoundHtml.Append("<td style=\"width:110px\">" + String.Format("{0: dd MMM yyyy}", data.OrderDate) + "</td>");
                                        compoundHtml.Append("<td style=\"width:250px\">" + data.ItemName + "</td>");
                                        compoundHtml.Append("<td style=\"width:40px\">" + data.Dose + "</td>");
                                        compoundHtml.Append("<td style=\"width:85px\">" + data.dose_uom + "</td>");
                                        compoundHtml.Append("<td style=\"width:80px\">" + data.Frequency + "</td>");
                                        compoundHtml.Append("<td style=\"width:95px\">" + data.Route + "</td>");
                                        compoundHtml.Append("<td style=\"width:250px\">" + data.Instruction + "</td>");
                                        compoundHtml.Append("<td style=\"width:40px\">" + data.Quantity + "</td>");
                                        compoundHtml.Append("<td style=\"width:50px\">" + data.Uom + "</td>");
                                        compoundHtml.Append("<td style=\"width:40px\">" + data.Iter + "</td>");
                                        if (data.IsRoutine.ToLower() == "Not Routine".ToLower())
                                        {
                                            compoundHtml.Append("<td style=\"width:85px\">" + "No" + "</td></tr>");
                                        }
                                        else if (data.IsRoutine.ToLower() == "Routine".ToLower())
                                        {
                                            compoundHtml.Append("<td style=\"width:85px\">" + "Yes" + "</td></tr>");
                                        }

                                    }
                                    compoundHtml.Append("</table></div>");
                                    compoundHtml.Append("</div></div></div>");
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
                }
            }

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getMedicalHistory", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "PatientId", hfPatientId.Value, "getMedicalHistory", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
        }
        //Log.Info(LogConfig.LogEnd());
    }

    void getMedicalHistoryFiltered()
    {
        //Log.Info(LogConfig.LogStart());

        List<MedicationHistory> listData = new List<MedicationHistory>();

        try
        {
            listData = (List<MedicationHistory>)Session[Helper.SessionMedicalHistoryFiltered];

            List<MedicationHistory> listPrescription = new List<MedicationHistory>();
            List<MedicationHistory> listCompound = new List<MedicationHistory>();
            List<MedicationHistory> listConsumables = new List<MedicationHistory>();

            StringBuilder compoundHtml = new StringBuilder();

            if (listData.Count > 0)
            {
                prescription.Visible = true;

                img_noData.Visible = false;
                no_patient_data.Visible = false;

                Session[Helper.ViewStateListData] = listData;
                List<String> listDate = listData.Select(x => String.Format("{0:ddd, dd MMM yyyy}", x.AdmissionDate)).Distinct().ToList();
                var dateTemp = String.Format("{0:ddd, dd MMM yyyy}", listData[0].AdmissionDate);

                DataTable dt_history = Helper.ToDataTable(listData);
                gvw_history.DataSource = dt_history;
                gvw_history.DataBind();

                foreach (String dataDate in listDate) //===== Take list by AdmissionDate
                {
                    List<MedicationHistory> listHistoryByDate = listData.FindAll(x => dataDate.Equals(String.Format("{0:ddd, dd MMM yyyy}", x.AdmissionDate)));

                    List<String> listAdmissionNo = listHistoryByDate.Select(x => x.AdmissionNo).Distinct().ToList();

                    foreach (String dataAdmissionNo in listAdmissionNo) //===== Take list by AdmissionNo
                    {
                        List<MedicationHistory> listHistoryByAdmission = listHistoryByDate.FindAll(x => dataAdmissionNo.Equals(x.AdmissionNo));

                        List<String> listDoctorName = listHistoryByAdmission.Select(x => x.MedicationDoctor).Distinct().ToList();

                        foreach (String datadoctorName in listDoctorName) //===== Take list by MedicineDoctor
                        {
                            List<MedicationHistory> listHistoryByDoctor = listHistoryByAdmission.FindAll(x => datadoctorName.Equals(x.MedicationDoctor));

                            string payerNameText = (from alist in listData
                                                    select alist.PayerName
                                                ).FirstOrDefault();
                            string checkverified = "";
                            if (listHistoryByDoctor[0].IsVerified == true)
                            {
                                checkverified = "<i class=\"fa fa-check-square-o\" style=\"color: #4d9b35;font-size: 20px;\" title=\"Verified by Pharmacy\"></i>";
                            }
                            compoundHtml.Append("<div class = " + "container-fluid" + " style=" + "margin-top:10px;" + "padding-bottom:0px;" + "><div style=" + "font-size:25px;" + "><b>" + dataDate + "</b> " + checkverified + "</div><div style=" + "font-size:12px;" + ">" + dataAdmissionNo + " | " + datadoctorName + " | " + payerNameText + "</div>");

                            listPrescription = listHistoryByDoctor.FindAll(x => x.IsCompound.Equals("0") && x.IsConsumables.Equals("0"));
                            listCompound = listHistoryByDoctor.FindAll(x => x.IsCompound.Equals("1"));
                            listConsumables = listHistoryByDoctor.FindAll(x => x.IsConsumables.Equals("1"));

                            GridView data_grid = new GridView();
                            DataTable dt = new DataTable();

                            if (listPrescription.Count > 0)
                            {
                                compoundHtml.Append("<div class=\"shadows\" style=\"background-color:white; margin-bottom:5px; margin-top:8px; max-height:350px; overflow:auto; padding-left:0px; padding-right:0px;\"><div><div style=\"font-size:17px; padding-left: 10px; padding-bottom: 5px;\"><b><label style=\"display: " + setENG + "\"> Drug Prescription </label> <label style=\"display: " + setIND + "\"> Resep Obat </label> </b></div>");

                                //INI DENGAN ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                //if (isBahasa == "ENG")
                                //{
                                //    compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organization</b></td><td><b>Transac. Date</b></td>");
                                //    compoundHtml.Append("" +
                                //        "<td><b>Item</b></td>" +
                                //        "<td><b>Dose</b></td>" +
                                //        "<td><b>Dose UOM</b></td>" +
                                //        "<td><b>Frequency</b></td>" +
                                //        "<td><b>Route</b></td>" +
                                //        "<td><b>Instruction</b></td>" +
                                //        "<td><b>R/Qty</b></td>" +
                                //        "<td><b>U.O.M</b></td>" +
                                //        "<td><b>Issue</b></td>" +
                                //        "<td><b>Return</b></td>" +                                        
                                //        "<td><b>Outstd.</b></td>" +                                        
                                //        "<td><b>Iter</b></td>" +
                                //        "<td><b>Routine</b></td>" +
                                //        "</tr>");
                                //}
                                //else if (isBahasa == "IND")
                                //{
                                //    compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organisasi</b></td><td><b>Tgl. Transaksi</b></td>");
                                //    compoundHtml.Append("" +
                                //        "<td><b>Obat</b></td>" +
                                //        "<td><b>Dosis</b></td>" +
                                //        "<td><b>Unit Dosis</b></td>" +
                                //        "<td><b>Frekuensi</b></td>" +
                                //        "<td><b>Rute</b></td>" +
                                //        "<td><b>Instruksi</b></td>" +
                                //        "<td><b>Jml</b></td>" +
                                //        "<td><b>Unit</b></td>" +
                                //        "<td><b>Issue</b></td>" +
                                //        "<td><b>Retur</b></td>" +                                        
                                //        "<td><b>Sisa</b></td>" +
                                //        "<td><b>Iter</b></td>" +
                                //        "<td><b>Rutin</b></td>" +
                                //        "</tr>");
                                //}

                                //INI TANPA ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                if (isBahasa == "ENG")
                                {
                                    compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organization</b></td><td><b>Transac. Date</b></td>");
                                    compoundHtml.Append("" +
                                        "<td><b>Item</b></td>" +
                                        "<td><b>Dose</b></td>" +
                                        //"<td><b>Dose UOM</b></td>" +
                                        "<td><b>Frequency</b></td>" +
                                        "<td><b>Route</b></td>" +
                                        "<td><b>Instruction</b></td>" +
                                        "<td><b>R/Qty</b></td>" +
                                        "<td><b>U.O.M</b></td>" +
                                        "<td><b>Iter</b></td>" +
                                        "<td><b>Routine</b></td>" +
                                        "</tr>");
                                }
                                else if (isBahasa == "IND")
                                {
                                    compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organisasi</b></td><td><b>Tgl. Transaksi</b></td>");
                                    compoundHtml.Append("" +
                                        "<td><b>Obat</b></td>" +
                                        "<td><b>Dosis</b></td>" +
                                        //"<td><b>Unit Dosis</b></td>" +
                                        "<td><b>Frekuensi</b></td>" +
                                        "<td><b>Rute</b></td>" +
                                        "<td><b>Instruksi</b></td>" +
                                        "<td><b>Jml</b></td>" +
                                        "<td><b>Unit</b></td>" +
                                        "<td><b>Iter</b></td>" +
                                        "<td><b>Rutin</b></td>" +
                                        "</tr>");
                                }

                                foreach (MedicationHistory data in listPrescription)
                                {
                                    string link = "javascript:Open('" + data.ItemName + "')";

                                    //INI DENGAN ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                    //compoundHtml.Append("<tr><td style=\"width:84px\">" + data.OrgCode + "</td>");
                                    //compoundHtml.Append("<td style=\"width:110px\">" + String.Format("{0: dd MMM yyyy}", data.OrderDate) + "</td>");
                                    //compoundHtml.Append("<td style=\"width:250px\">" + data.ItemName + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.Dose + "</td>");
                                    //compoundHtml.Append("<td style=\"width:85px\">" + data.dose_uom + "</td>");
                                    //compoundHtml.Append("<td style=\"width:80px\">" + data.Frequency + "</td>");
                                    //compoundHtml.Append("<td style=\"width:95px\">" + data.Route + "</td>");
                                    //compoundHtml.Append("<td style=\"width:250px\">" + data.Instruction + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.Quantity + "</td>");
                                    //compoundHtml.Append("<td style=\"width:50px\">" + data.Uom + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.IssuedQuantity + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.ReturnQty + "</td>");                                    
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.OutstandingQty + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.Iter + "</td>");
                                    //if (data.IsRoutine.ToLower() == "Not Routine".ToLower())
                                    //{
                                    //    compoundHtml.Append("<td style=\"width:85px\">" + "No" + "</td></tr>");
                                    //}
                                    //else if (data.IsRoutine.ToLower() == "Routine".ToLower())
                                    //{
                                    //    compoundHtml.Append("<td style=\"width:85px\">" + "Yes" + "</td></tr>");
                                    //}

                                    //INI TANPA ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA

                                    string dosepres = "";
                                    if (data.IsDoseText == true)
                                    {
                                        dosepres = data.DoseText;
                                    }
                                    else if (data.IsDoseText == false)
                                    {
                                        dosepres = data.Dose + " " + data.dose_uom;
                                    }
                                    compoundHtml.Append("<tr><td style=\"width:84px\">" + data.OrgCode + "</td>");
                                    compoundHtml.Append("<td style=\"width:110px\">" + String.Format("{0: dd MMM yyyy}", data.OrderDate) + "</td>");
                                    compoundHtml.Append("<td style=\"width:250px\">" + data.ItemName + "</td>");
                                    compoundHtml.Append("<td style=\"width:125px\">" + dosepres + "</td>");
                                    //compoundHtml.Append("<td style=\"width:85px\">" + data.dose_uom + "</td>");
                                    compoundHtml.Append("<td style=\"width:80px\">" + data.Frequency + "</td>");
                                    compoundHtml.Append("<td style=\"width:95px\">" + data.Route + "</td>");
                                    compoundHtml.Append("<td style=\"width:250px\">" + data.Instruction + "</td>");
                                    compoundHtml.Append("<td style=\"width:40px\">" + data.Quantity + "</td>");
                                    compoundHtml.Append("<td style=\"width:50px\">" + data.Uom + "</td>");
                                    compoundHtml.Append("<td style=\"width:40px\">" + data.Iter + "</td>");
                                    if (data.IsRoutine.ToLower() == "Not Routine".ToLower())
                                    {
                                        compoundHtml.Append("<td style=\"width:85px\">" + "No" + "</td></tr>");
                                    }
                                    else if (data.IsRoutine.ToLower() == "Routine".ToLower())
                                    {
                                        compoundHtml.Append("<td style=\"width:85px\">" + "Yes" + "</td></tr>");
                                    }
                                }
                                compoundHtml.Append("</table></div>");
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
                                if (listPrescription.Count == 0)
                                {
                                    compoundHtml.Append("<div class=\"shadows\" style=\"background-color:white; margin-bottom:5px; margin-top:8px; max-height:350px; overflow:auto; padding-left:0px; padding-right:0px;\"><div>");
                                }
                                compoundHtml.Append("<div style=\" font-size:17px; padding-left: 10px; padding-bottom: 5px;\"><b> <label style=\"display: " + setENG + "\"> Compound Prescription </label> <label style=\"display: " + setIND + "\"> Resep Racikan </label> </b></div>");

                                //INI DENGAN ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                //if (isBahasa == "ENG")
                                //{
                                //    compoundHtml.Append("<div style=\"display:none; border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organization</b></td><td><b>Transac. Date</b></td>");
                                //    compoundHtml.Append("" +
                                //        "<td><b>Item</b></td>" +
                                //        "<td><b>Dose</b></td>" +
                                //        "<td><b>Dose UOM</b></td>" +
                                //        "<td><b>Frequency</b></td>" +
                                //        "<td><b>Route</b></td>" +
                                //        "<td><b>Instruction</b></td>" +
                                //        "<td><b>R/Qty</b></td>" +
                                //        "<td><b>U.O.M</b></td>" +
                                //        "<td><b>Issue</b></td>" +
                                //        "<td><b>Return</b></td>" +                                        
                                //        "<td><b>Outstd.</b></td>" +                                        
                                //        "<td><b>Iter</b></td>" +
                                //        "<td><b>Routine</b></td>" +
                                //        "</tr>");
                                //}
                                //else if (isBahasa == "IND")
                                //{
                                //    compoundHtml.Append("<div style=\"display:none; border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organisasi</b></td><td><b>Tgl. Transaksi</b></td>");
                                //    compoundHtml.Append("" +
                                //        "<td><b>Obat</b></td>" +
                                //        "<td><b>Dosis</b></td>" +
                                //        "<td><b>Unit Dosis</b></td>" +
                                //        "<td><b>Frekuensi</b></td>" +
                                //        "<td><b>Rute</b></td>" +
                                //        "<td><b>Instruksi</b></td>" +
                                //        "<td><b>Jml</b></td>" +
                                //        "<td><b>Unit</b></td>" +
                                //        "<td><b>Issue</b></td>" +
                                //        "<td><b>Retur</b></td>" +
                                //        "<td><b>Sisa</b></td>" +
                                //        "<td><b>Iter</b></td>" +
                                //        "<td><b>Rutin</b></td>" +
                                //        "</tr>");
                                //}

                                //INI TANPA ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                if (isBahasa == "ENG")
                                {
                                    compoundHtml.Append("<div style=\" border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organization</b></td><td><b>Transac. Date</b></td>");
                                    compoundHtml.Append("" +
                                        "<td><b>Item</b></td>" +
                                        "<td><b>Dose</b></td>" +
                                        //"<td><b>Dose UOM</b></td>" +
                                        "<td><b>Frequency</b></td>" +
                                        "<td><b>Route</b></td>" +
                                        "<td><b>Instruction</b></td>" +
                                        "<td><b>R/Qty</b></td>" +
                                        "<td><b>U.O.M</b></td>" +
                                        "<td><b>Iter</b></td>" +
                                        "<td><b>Routine</b></td>" +
                                        "</tr>");
                                }
                                else if (isBahasa == "IND")
                                {
                                    compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organisasi</b></td><td><b>Tgl. Transaksi</b></td>");
                                    compoundHtml.Append("" +
                                        "<td><b>Obat</b></td>" +
                                        "<td><b>Dosis</b></td>" +
                                        //"<td><b>Unit Dosis</b></td>" +
                                        "<td><b>Frekuensi</b></td>" +
                                        "<td><b>Rute</b></td>" +
                                        "<td><b>Instruksi</b></td>" +
                                        "<td><b>Jml</b></td>" +
                                        "<td><b>Unit</b></td>" +
                                        "<td><b>Iter</b></td>" +
                                        "<td><b>Rutin</b></td>" +
                                        "</tr>");
                                }

                                List<MedicationHistory> dataRacikanHeader = listCompound.FindAll(x => x.ItemId == 0 && x.IsCompoundHeader == true);
                                List<MedicationHistory> dataRacikanDetail = listCompound.FindAll(x => x.IsCompoundHeader == false);

                                foreach (MedicationHistory data in dataRacikanHeader)
                                {
                                    string link = "javascript:Open('" + data.CompoundName + "')";

                                    //INI DENGAN ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                    //compoundHtml.Append("<tr><td style=\"width:84px\">" + data.OrgCode + "</td>");
                                    //compoundHtml.Append("<td style=\"width:110px\">" + String.Format("{0: dd MMM yyyy}", data.OrderDate) + "</td>");
                                    //compoundHtml.Append("<td style=\"width:205px\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline; \">" + data.CompoundName + "</a></td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.Dose + "</td>");
                                    //compoundHtml.Append("<td style=\"width:85px\">" + data.dose_uom + "</td>");
                                    //compoundHtml.Append("<td style=\"width:80px\">" + data.Frequency + "</td>");
                                    //compoundHtml.Append("<td style=\"width:95px\">" + data.Route + "</td>");
                                    //compoundHtml.Append("<td style=\"width:250px\">" + data.Instruction + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.Quantity + "</td>");
                                    //compoundHtml.Append("<td style=\"width:50px\">" + data.Uom + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.IssuedQuantity + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.ReturnQty + "</td>");                                    
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.OutstandingQty + "</td>");                                    
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.Iter + "</td>");
                                    //if (data.IsRoutine.ToLower() == "Not Routine".ToLower())
                                    //{
                                    //    compoundHtml.Append("<td style=\"width:85px\">" + "No" + "</td></tr>");
                                    //}
                                    //else if (data.IsRoutine.ToLower() == "Routine".ToLower())
                                    //{
                                    //    compoundHtml.Append("<td style=\"width:85px\">" + "Yes" + "</td></tr>");
                                    //}

                                    //INI TANPA ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA

                                    string doseheader = "";
                                    if (data.IsDoseText == true)
                                    {
                                        doseheader = data.DoseText;
                                    }
                                    else if (data.IsDoseText == false)
                                    {
                                        doseheader = data.Dose + " " + data.dose_uom;
                                    }

                                    compoundHtml.Append("<tr><td style=\"width:84px\">" + data.OrgCode + "</td>");
                                    compoundHtml.Append("<td style=\"width:110px\">" + String.Format("{0: dd MMM yyyy}", data.OrderDate) + "</td>");
                                    //compoundHtml.Append("<td style=\"width:205px\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline; \">" + data.CompoundName + "</a></td>");
                                    compoundHtml.Append("<td style=\"width:205px\">" + data.CompoundName + "</td>");
                                    compoundHtml.Append("<td style=\"width:125px\">" + doseheader + "</td>");
                                    //compoundHtml.Append("<td style=\"width:85px\">" + data.dose_uom + "</td>");
                                    compoundHtml.Append("<td style=\"width:80px\">" + data.Frequency + "</td>");
                                    compoundHtml.Append("<td style=\"width:95px\">" + data.Route + "</td>");
                                    compoundHtml.Append("<td style=\"width:250px\">" + data.Instruction + "</td>");
                                    compoundHtml.Append("<td style=\"width:40px\">" + data.Quantity + "</td>");
                                    compoundHtml.Append("<td style=\"width:50px\">" + data.Uom + "</td>");
                                    compoundHtml.Append("<td style=\"width:40px\">" + data.Iter + "</td>");
                                    if (data.IsRoutine.ToLower() == "Not Routine".ToLower())
                                    {
                                        compoundHtml.Append("<td style=\"width:85px\">" + "No" + "</td></tr>");
                                    }
                                    else if (data.IsRoutine.ToLower() == "Routine".ToLower())
                                    {
                                        compoundHtml.Append("<td style=\"width:85px\">" + "Yes" + "</td></tr>");
                                    }

                                    List<MedicationHistory> dataRacikanDetailSelected = dataRacikanDetail.FindAll(x => x.CompoundId == data.CompoundId);
                                    if (dataRacikanDetailSelected.Count > 0)
                                    {
                                        compoundHtml.Append("<tr> <td colspan=\"12\">");
                                        compoundHtml.Append("<table border=\"0\" style=\"margin-left: 220px; display:inline-block; vertical-align:top;\">");
                                        compoundHtml.Append("<tr><td style=\"padding:2px 10px; font-weight:bold; min-width: 240px;\"> Item </td> <td style=\"padding:2px 10px; font-weight:bold; min-width: 100px;\"> Dose </td></tr>");
                                        foreach (MedicationHistory detail in dataRacikanDetailSelected)
                                        {
                                            string dosedetail = "";
                                            if (detail.IsDoseText == true)
                                            {
                                                dosedetail = detail.DoseText;
                                            }
                                            else if (detail.IsDoseText == false)
                                            {
                                                dosedetail = detail.Dose + " " + detail.dose_uom;
                                            }
                                            compoundHtml.Append("<tr><td style=\"padding:2px 10px;\"> • " + detail.ItemName + "</td> <td style=\"padding:2px 10px;\"> " + dosedetail + " </td> </tr>");
                                        }
                                        compoundHtml.Append("</table>");
                                        compoundHtml.Append("<table border=\"0\" style=\"display:inline-block; vertical-align:top;\">");
                                        compoundHtml.Append("<tr><td style=\"padding:2px 10px; font-weight:bold; min-width: 240px;\"> Instruction </td> </tr>");
                                        compoundHtml.Append("<tr><td style=\"padding:2px 10px; \"> " + data.CompoundNote + " </td> </tr>");
                                        compoundHtml.Append("</table>");
                                        compoundHtml.Append("</td> </tr>");
                                    }
                                    
                                }
                                compoundHtml.Append("</table></div>");

                                if (listConsumables.Count != 0)
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
                                if (listPrescription.Count == 0 && listCompound.Count == 0)
                                {
                                    compoundHtml.Append("<div class=\"shadows\" style=\"background-color:white; margin-bottom:5px; margin-top:8px; max-height:350px; overflow:auto; padding-left:0px; padding-right:0px;\"><div> ");
                                }
                               
                                compoundHtml.Append("<div style=\"font-size:17px; padding-left: 10px; padding-bottom: 5px;\"><b> <label style=\"display: " + setENG + "\"> Consumables </label> <label style=\"display: " + setIND + "\"> Alat Kesehatan </label> </b></div>");


                                //INI DENGAN ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                //if (isBahasa == "ENG")
                                //{
                                //    compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organization</b></td><td><b>Transac. Date</b></td>");
                                //    compoundHtml.Append("" +
                                //        "<td><b>Item</b></td>" +
                                //        "<td><b>Dose</b></td>" +
                                //        "<td><b>Dose UOM</b></td>" +
                                //        "<td><b>Frequency</b></td>" +
                                //        "<td><b>Route</b></td>" +
                                //        "<td><b>Instruction</b></td>" +
                                //        "<td><b>R/Qty</b></td>" +
                                //        "<td><b>U.O.M</b></td>" +
                                //        "<td><b>Issue</b></td>" +
                                //        "<td><b>Return</b></td>" +
                                //        "<td><b>Outstd.</b></td>" +
                                //        "<td><b>Iter</b></td>" +
                                //        "<td><b>Routine</b></td>" +
                                //        "</tr>");
                                //}
                                //else if (isBahasa == "IND")
                                //{
                                //    compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organisasi</b></td><td><b>Tgl. Transaksi</b></td>");
                                //    compoundHtml.Append("" +
                                //        "<td><b>Alkes</b></td>" +
                                //        "<td><b>Dosis</b></td>" +
                                //        "<td><b>Unit Dosis</b></td>" +
                                //        "<td><b>Frekuensi</b></td>" +
                                //        "<td><b>Rute</b></td>" +
                                //        "<td><b>Instruksi</b></td>" +
                                //        "<td><b>Jml</b></td>" +
                                //        "<td><b>Unit</b></td>" +
                                //        "<td><b>Issue</b></td>" +
                                //        "<td><b>Retur</b></td>" +
                                //        "<td><b>Sisa</b></td>" +
                                //        "<td><b>Iter</b></td>" +
                                //        "<td><b>Rutin</b></td>" +
                                //        "</tr>");
                                //}

                                //INI TANPA ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                if (isBahasa == "ENG")
                                {
                                    compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organization</b></td><td><b>Transac. Date</b></td>");
                                    compoundHtml.Append("" +
                                        "<td><b>Item</b></td>" +
                                        "<td><b>Dose</b></td>" +
                                        "<td><b>Dose UOM</b></td>" +
                                        "<td><b>Frequency</b></td>" +
                                        "<td><b>Route</b></td>" +
                                        "<td><b>Instruction</b></td>" +
                                        "<td><b>R/Qty</b></td>" +
                                        "<td><b>U.O.M</b></td>" +
                                        "<td><b>Iter</b></td>" +
                                        "<td><b>Routine</b></td>" +
                                        "</tr>");
                                }
                                else if (isBahasa == "IND")
                                {
                                    compoundHtml.Append("<div style=\"border-bottom: 1px solid lightgray; border-top: 1px solid lightgray; min-width:1376px;\"><table class=\"table table-striped table-condensed\" style=\"margin-bottom:0px; margin-left:5px; margin-right:5px; min-width:1366px;\" ><tr><td><b>Organisasi</b></td><td><b>Tgl. Transaksi</b></td>");
                                    compoundHtml.Append("" +
                                        "<td><b>Alkes</b></td>" +
                                        "<td><b>Dosis</b></td>" +
                                        "<td><b>Unit Dosis</b></td>" +
                                        "<td><b>Frekuensi</b></td>" +
                                        "<td><b>Rute</b></td>" +
                                        "<td><b>Instruksi</b></td>" +
                                        "<td><b>Jml</b></td>" +
                                        "<td><b>Unit</b></td>" +
                                        "<td><b>Iter</b></td>" +
                                        "<td><b>Rutin</b></td>" +
                                        "</tr>");
                                }

                                foreach (MedicationHistory data in listConsumables)
                                {
                                    //INI DENGAN ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                    //compoundHtml.Append("<tr><td style=\"width:84px\">" + data.OrgCode + "</td>");
                                    //compoundHtml.Append("<td style=\"width:110px\">" + String.Format("{0: dd MMM yyyy}", data.OrderDate) + "</td>");
                                    //compoundHtml.Append("<td style=\"width:250px\">" + data.ItemName + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.Dose + "</td>");
                                    //compoundHtml.Append("<td style=\"width:85px\">" + data.dose_uom + "</td>");
                                    //compoundHtml.Append("<td style=\"width:80px\">" + data.Frequency + "</td>");
                                    //compoundHtml.Append("<td style=\"width:95px\">" + data.Route + "</td>");
                                    //compoundHtml.Append("<td style=\"width:250px\">" + data.Instruction + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.Quantity + "</td>");
                                    //compoundHtml.Append("<td style=\"width:50px\">" + data.Uom + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.IssuedQuantity + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.ReturnQty + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.OutstandingQty + "</td>");
                                    //compoundHtml.Append("<td style=\"width:40px\">" + data.Iter + "</td>");
                                    //if (data.IsRoutine.ToLower() == "Not Routine".ToLower())
                                    //{
                                    //    compoundHtml.Append("<td style=\"width:85px\">" + "No" + "</td></tr>");
                                    //}
                                    //else if (data.IsRoutine.ToLower() == "Routine".ToLower())
                                    //{
                                    //    compoundHtml.Append("<td style=\"width:85px\">" + "Yes" + "</td></tr>");
                                    //}

                                    //INI TANPA ISSUE RETURN OUTSTD, UNCOMMENT KETIKA AKAN MENGGUNAKAN NYA
                                    compoundHtml.Append("<tr><td style=\"width:84px\">" + data.OrgCode + "</td>");
                                    compoundHtml.Append("<td style=\"width:110px\">" + String.Format("{0: dd MMM yyyy}", data.OrderDate) + "</td>");
                                    compoundHtml.Append("<td style=\"width:250px\">" + data.ItemName + "</td>");
                                    compoundHtml.Append("<td style=\"width:40px\">" + data.Dose + "</td>");
                                    compoundHtml.Append("<td style=\"width:85px\">" + data.dose_uom + "</td>");
                                    compoundHtml.Append("<td style=\"width:80px\">" + data.Frequency + "</td>");
                                    compoundHtml.Append("<td style=\"width:95px\">" + data.Route + "</td>");
                                    compoundHtml.Append("<td style=\"width:250px\">" + data.Instruction + "</td>");
                                    compoundHtml.Append("<td style=\"width:40px\">" + data.Quantity + "</td>");
                                    compoundHtml.Append("<td style=\"width:50px\">" + data.Uom + "</td>");
                                    compoundHtml.Append("<td style=\"width:40px\">" + data.Iter + "</td>");
                                    if (data.IsRoutine.ToLower() == "Not Routine".ToLower())
                                    {
                                        compoundHtml.Append("<td style=\"width:85px\">" + "No" + "</td></tr>");
                                    }
                                    else if (data.IsRoutine.ToLower() == "Routine".ToLower())
                                    {
                                        compoundHtml.Append("<td style=\"width:85px\">" + "Yes" + "</td></tr>");
                                    }
                                }
                                compoundHtml.Append("</table></div>");
                                compoundHtml.Append("</div></div></div>");
                                //if (listCompound.Count != 0)
                                //{
                                //    compoundHtml.Append("<br />");
                                //}
                                //else
                                //{
                                //    compoundHtml.Append("</div></div></div>");
                                //}
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
            }
            else
            {
                prescription.Visible = false;

                img_noData.Visible = true;
                no_patient_data.Visible = true;
            }
            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value, "getMedicalHistoryFiltered", StartTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

        }
        catch (Exception ex)
        {
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "PatientId", hfPatientId.Value, "getMedicalHistoryFiltered", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));
        }
        //Log.Info(LogConfig.LogEnd());
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
    
    protected void ddlDrugs_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<MedicationHistory>  listData = (List<MedicationHistory>)Session[Helper.SessionMedicalHistory];
        Session.Remove(Helper.SessionMedicalHistoryFiltered);
        var itemId = ddlDrugs.SelectedValue;

        if (ddlDrugs.SelectedItem.ToString() == "-- All Items --")
        {
            Session[Helper.SessionMedicalHistoryFiltered] = listData;
        }
        else
        {
            List<MedicationHistory> listDataFiltered = new List<MedicationHistory>();
            listDataFiltered = (from a in listData
                                where a.ItemId == Int64.Parse(itemId.ToString())
                                orderby a.AdmissionDate descending
                                select a
                                ).ToList();
            Session[Helper.SessionMedicalHistoryFiltered] = listDataFiltered;
        }

        getMedicalHistoryFiltered();
        resetFilterLeft();
    }

    protected void ButtonSearchMedication_Click(object sender, EventArgs e)
    {
        if (Session[Helper.SessionMedicalHistory] != null)
        {
            List<MedicationHistory> listData = (List<MedicationHistory>)Session[Helper.SessionMedicalHistory];
            Session.Remove(Helper.SessionMedicalHistoryFiltered);

            List<MedicationHistory> listDataFiltered = new List<MedicationHistory>();

            if (TextBoxDateFrom.Text == "" && TextBoxDateTo.Text == "")
            {
                listDataFiltered = listData;
                Session[Helper.SessionMedicalHistoryFiltered] = listData;
            }
            else if (TextBoxDateTo.Text == "")
            {
                listDataFiltered = (from a in listData
                                    where a.AdmissionDate >= DateTime.Parse(TextBoxDateFrom.Text)
                                    orderby a.AdmissionDate descending
                                    select a
                                    ).ToList();
                Session[Helper.SessionMedicalHistoryFiltered] = listDataFiltered;
            }
            else if (TextBoxDateFrom.Text == "")
            {
                listDataFiltered = (from a in listData
                                    where a.AdmissionDate <= DateTime.Parse(TextBoxDateTo.Text).AddDays(1)
                                    orderby a.AdmissionDate descending
                                    select a
                                    ).ToList();
                Session[Helper.SessionMedicalHistoryFiltered] = listDataFiltered;
            }
            else
            {
                listDataFiltered = (from a in listData
                                    where a.AdmissionDate >= DateTime.Parse(TextBoxDateFrom.Text) && a.AdmissionDate <= DateTime.Parse(TextBoxDateTo.Text).AddDays(1)
                                    orderby a.AdmissionDate descending
                                    select a
                                    ).ToList();
                Session[Helper.SessionMedicalHistoryFiltered] = listDataFiltered;
            }

            if (listDataFiltered.Count > 0)
            {
                if (CheckBoxIter.Checked == true)
                {
                    listDataFiltered = (from a in listDataFiltered
                                        where int.Parse(a.Iter) > 0
                                        orderby a.AdmissionDate descending
                                        select a
                                        ).ToList();
                    Session[Helper.SessionMedicalHistoryFiltered] = listDataFiltered;
                }
                else
                {
                    listDataFiltered = (from a in listDataFiltered
                                        orderby a.AdmissionDate descending
                                        select a
                                        ).ToList();
                    Session[Helper.SessionMedicalHistoryFiltered] = listDataFiltered;
                }
            }

            getMedicalHistoryFiltered();
            resetFilterRight();
        }
    }

    public void resetFilterLeft()
    {
        CheckBoxIter.Checked = false;
        TextBoxDateFrom.Text = "";
        TextBoxDateTo.Text = "";
    }

    public void resetFilterRight()
    {
        ddlDrugs.SelectedValue = "0";
    }
}