using log4net;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static PatientHistory;


public partial class Form_General_Result_CompareLaboratory : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            HyperLink test = Master.FindControl("ResultLink") as HyperLink;
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
                Session.Remove(Helper.ViewStateItemList);
                Session.Remove(Helper.ViewStateHeaderChecklist);
                Helper.LinkBinder(this, Request.QueryString["idPatient"], Request.QueryString["AdmissionId"], Request.QueryString["EncounterId"], Request.QueryString["PagefaId"], Request.QueryString["PageSoapId"], Request.QueryString["AppointmentId"], Request.QueryString["IsTele"]);
                hfPatientId.Value = Request.QueryString["idPatient"];
                hfEncounterId.Value = Request.QueryString["EncounterId"];
                hfAdmissionId.Value = Request.QueryString["AdmissionId"];
                hfPagefaId.Value = Request.QueryString["PagefaId"];
                hfPageSoapId.Value = Request.QueryString["PageSoapId"];
                hfAppointmentId.Value = Request.QueryString["AppointmentId"];
                hfIsTele.Value = Request.QueryString["IsTele"];
                getHeader();
                getDataItem();
                btnCancelDrugs.PostBackUrl = String.Format("~/Form/General/Result/ResultLABRAD.aspx?idPatient={0}&EncounterId={1}&AdmissionId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}", hfPatientId.Value, hfEncounterId.Value, hfAdmissionId.Value, hfPagefaId.Value, hfPageSoapId.Value, hfAppointmentId.Value, hfIsTele.Value);
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", hfEncounterId.Value.ToString(), "Page_Load", StartTime, EndTime, "OK", MyUser.GetUsername()
                          , "", "", ""));
            //Log.Info(LogConfig.LogEnd());
        }
    }

    void getHeader()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Patient_ID", hfPatientId.Value },
                { "Encounter_ID", hfEncounterId.Value }
            };
            //Log.Debug(LogConfig.LogStart("GetPatientHeader", logParam));
            var varResult = clsCommon.GetPatientHeader(long.Parse(hfPatientId.Value), hfEncounterId.Value.ToString());
            var JsongetPatientHistory = JsonConvert.DeserializeObject<ResponsePatientHeader>(varResult.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("GetPatientHeader", JsongetPatientHistory.Status, JsongetPatientHistory.Message));

            PatientHeader header = JsongetPatientHistory.Data;
            PatientCard.initializevalue(header);
            PatientCard1.initializevalue(header);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getHeader", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getHeader", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    void getDataItem()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<LaboratoryHeader> listLabHeaderItem = new List<LaboratoryHeader>();
        List<String> headerLab = new List<String>();
        StringBuilder innerHTMLHeader = new StringBuilder();

        try
        {

            //Log.Debug(LogConfig.LogStart("getHeaderLab", LogConfig.LogParam("Patient_ID", hfPatientId.Value)));
            var dataLaboratory = clsResult.getHeaderLab(hfPatientId.Value);
            var JsonLaboratory = JsonConvert.DeserializeObject<ResultLaboratoryHeader>(dataLaboratory.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("getHeaderLab", JsonLaboratory.Status, JsonLaboratory.Message));

            headerLab = JsonLaboratory.list.DistinctBy(x => x.tesT_GROUP).Select(x => x.tesT_GROUP).ToList();
            listLabHeaderItem = JsonLaboratory.list;

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getDataItem", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getDataItem", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        Session[Helper.ViewStateItemList] = listLabHeaderItem;
        createinnerHTMLHeader(headerLab, listLabHeaderItem);
        Session[Helper.ViewStateAllHeaderLaboratory] = headerLab;

        //Log.Info(LogConfig.LogEnd());
    }

    void createinnerHTMLHeader(List<String> headerLab, List<LaboratoryHeader> listLabHeaderItem)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<HeaderData> gridDataHeader = new List<HeaderData>();
        StringBuilder innerHTMLHeader = new StringBuilder();
        List<String> headerCheck = new List<String>();
        if (headerLab.Count != 0) {
            try
            {

                if (hf_test_group.Value != "")
                {
                    headerCheck = hf_test_group.Value.Split(',').ToList();
                }

                innerHTMLHeader.Append("<div style=\"height:90%; width:100%\"><table style=\"width:100%\">");
                foreach (String data in headerLab)
                {

                    string hideopenTestGroup = "javascript:hideItemGroupTest('" + data.Replace(' ', '_') + "')";
                    string listCheckTestGroup = "javascript:checklistTestGroup('" + data + "')";

                    var checkHeader = headerCheck.Find(x => x.Equals(data));
                    var inputcheck = "";
                    if (checkHeader != null)
                        inputcheck = "<div class=\"pretty p-icon p-curve\"> <input id=\"" + data + "_check" + "\" type =\"checkbox\" value=" + data + " checked =\"checked\" onclick=\"" + listCheckTestGroup + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div>";
                    else
                        inputcheck = "<div class=\"pretty p-icon p-curve\"> <input id=\"" + data + "_check" + "\" type =\"checkbox\" value=" + data + " onclick=\"" + listCheckTestGroup + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div>";

                    gridDataHeader.Add(new HeaderData { tesT_GROUP = data, ordeR_TESTNM = data, statusHeader = true, statusCheck = false });

                    var idIcon = data.Replace(' ', '_') + "_";
                    innerHTMLHeader.Append("<tr>" +
                                            "<td style=\"border-top:1px solid #CDD2DD; padding-top: 8px; padding-bottom: 8px; width:10%;\">" + inputcheck + "</td>" +
                                            "<td style=\"border-top:1px solid #CDD2DD; padding-top: 8px; padding-bottom: 8px; width:80%;\"><label for=\"" + data + "_check" + "\">" + data + "</label></td>" +
                                            "<td style=\"border-top:1px solid #CDD2DD; padding-top: 8px; padding-bottom: 8px; width:10%;\"><span id=\"" + idIcon + "\" style=\"cursor: pointer;\" aria-hidden=\"true\" class=\"glyphicon glyphicon-chevron-right\" onclick=\"" + hideopenTestGroup + "\"></span></td>" +
                                            "</tr><tr><td></td><td colspan=\"2\">");
                    gridDataHeader.AddRange(listLabHeaderItem.FindAll(x => x.tesT_GROUP.Equals(data)).ToList().Select(
                        x => new HeaderData
                        {
                            tesT_GROUP = data,
                            ordeR_TESTNM = x.tesT_NM,
                            statusCheck = false,
                            statusHeader = false
                        }
                        ));

                    innerHTMLHeader.Append("<div id=" + data.Replace(' ', '_') + " style=\"display:none\"> <table class=\"table-condensed\"> ");
                    
                    foreach (HeaderData dataItem in gridDataHeader.FindAll(x => x.tesT_GROUP.Equals(data) && x.statusHeader == false))
                    {
                        innerHTMLHeader.Append("<tr><td><span>" + dataItem.ordeR_TESTNM + "</span></td></tr>");
                    }
                    innerHTMLHeader.Append("</table></div></td></tr>");
                }

                innerHTMLHeader.Append("</table></div>");

                string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "createinnerHTMLHeader", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));


            }
            catch (Exception ex)
            {
                string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "createinnerHTMLHeader", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            }
        } 
        divLabTestGroup.InnerHtml = innerHTMLHeader.ToString();

         //Log.Info(LogConfig.LogEnd());
    }

    protected void src_item_txt_TextChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<String> gridDataHeader = (List<String>)Session[Helper.ViewStateAllHeaderLaboratory];
        List<LaboratoryHeader> gridItemList = (List<LaboratoryHeader>)Session[Helper.ViewStateItemList];
        List<String> gridSearchResult = new List<String>();
        try
        {

            Session[Helper.ViewStateHeaderChecklist] = hf_test_group.Value;
            gridSearchResult = gridDataHeader.Where(x => x.Contains(src_item_txt.Text.ToUpper())).ToList();

            if (gridSearchResult.Count != 0)
            {
                createinnerHTMLHeader(gridSearchResult, gridItemList);
            }
            else
            {
                if (src_item_txt.Text != "")
                {
                    createinnerHTMLHeader(new List<string>(), gridItemList);
                }
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "src_item_txt_TextChanged", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "src_item_txt_TextChanged", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    void setTableCompare(List<LaboratoryCompare> data, String testHeader)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        StringBuilder table = new StringBuilder();

        try
        {
            var temp = JsonConvert.SerializeObject(data);

            var date = data.Find(x => x.isInfo == 3);
            var ono = data.Find(x => x.isInfo == 1);
            var header = data.FindAll(x => x.IsHeader == 1).DistinctBy(x => x.ordeR_TESTNM);

            string link1 = "";
            string link2 = "";
            string link3 = "";
            string link4 = "";
            string link5 = "";

            string date_ono1 = "";
            string date_ono2 = "";
            string date_ono3 = "";
            string date_ono4 = "";
            string date_ono5 = "";

            if (ono.onO_1 != "")
                link1 = "javascript:Open('" + ono.onO_1 + "')";

            if (ono.onO_2 != "")
                link2 = "javascript:Open('" + ono.onO_2 + "')";

            if (ono.onO_3 != "")
                link3 = "javascript:Open('" + ono.onO_3 + "')";

            if (ono.onO_4 != "")
                link4 = "javascript:Open('" + ono.onO_4 + "')";

            if (ono.onO_5 != "")
                link5 = "javascript:Open('" + ono.onO_5 + "')";

            if (date.onO_1 != null)
                date_ono1 = DateTime.ParseExact(date.onO_1, "dd/MM/yyyy", null).Day.ToString() + " " + DateTime.ParseExact(date.onO_1, "dd/MM/yyyy", null).ToString("MMM") + " " + DateTime.ParseExact(date.onO_1, "dd/MM/yyyy", null).Year.ToString();

            if (date.onO_2 != null)
                date_ono2 = DateTime.ParseExact(date.onO_2, "dd/MM/yyyy", null).Day.ToString() + " " + DateTime.ParseExact(date.onO_2, "dd/MM/yyyy", null).ToString("MMM") + " " + DateTime.ParseExact(date.onO_2, "dd/MM/yyyy", null).Year.ToString();

            if (date.onO_3 != null)
                date_ono3 = DateTime.ParseExact(date.onO_3, "dd/MM/yyyy", null).Day.ToString() + " " + DateTime.ParseExact(date.onO_3, "dd/MM/yyyy", null).ToString("MMM") + " " + DateTime.ParseExact(date.onO_3, "dd/MM/yyyy", null).Year.ToString();

            if (date.onO_4 != null)
                date_ono4 = DateTime.ParseExact(date.onO_4, "dd/MM/yyyy", null).Day.ToString() + " " + DateTime.ParseExact(date.onO_4, "dd/MM/yyyy", null).ToString("MMM") + " " + DateTime.ParseExact(date.onO_4, "dd/MM/yyyy", null).Year.ToString();

            if (date.onO_5 != null)
                date_ono5 = DateTime.ParseExact(date.onO_5, "dd/MM/yyyy", null).Day.ToString() + " " + DateTime.ParseExact(date.onO_5, "dd/MM/yyyy", null).ToString("MMMM") + " " + DateTime.ParseExact(date.onO_5, "dd/MM/yyyy", null).Year.ToString();

            table.Append("<div style=\"margin-top:-37px;\"><h4>" + testHeader + "</h4></div>" +
                "<div><table class=\"table table-striped table-condensed table-divider-v\"><tr><td style=\"border-left:0px; width:20%;\"><b>Test</b></td>" +
                                "<td style=\"border-left:0px; width:10%;\"><b>" + date_ono1 + "</b></td>" +
                                "<td style=\"border-left:0px; width:10%;\"><b>" + date_ono2 + "</b></td>" +
                                "<td style=\"border-left:0px; width:10%;\"><b>" + date_ono3 + "</b></td>" +
                                "<td style=\"border-left:0px; width:10%;\"><b>" + date_ono4 + "</b></td>" +
                                "<td style=\"border-left:0px; width:10%;\"><b>" + date_ono5 + "</b></td>" +
                                "<td style=\"border-left:0px; width:10%;\"><b>Unit</b></td>" +
                                "<td style=\"border-left:0px; width:10%;\"><b>References</b></td></tr>" +
                             "<tr><td style=\"border-left:0px;\"></td>" +
                                "<td style=\"border-left:0px;\"><a href=\"" + link1 + "\" style=\"color: blue; text-decoration:underline; \">" + ono.onO_1 + "</a></td>" +
                                "<td style=\"border-left:0px;\"><a href=\"" + link2 + "\" style=\"color: blue; text-decoration:underline; \">" + ono.onO_2 + "</a></td>" +
                                "<td style=\"border-left:0px;\"><a href=\"" + link3 + "\" style=\"color: blue; text-decoration:underline; \">" + ono.onO_3 + "</a></td>" +
                                "<td style=\"border-left:0px;\"><a href=\"" + link4 + "\" style=\"color: blue; text-decoration:underline; \">" + ono.onO_4 + "</a></td>" +
                                "<td style=\"border-left:0px;\"><a href=\"" + link5 + "\" style=\"color: blue; text-decoration:underline; \">" + ono.onO_5 + "</a></td>" +
                                "<td style=\"border-left:0px;\"></td>" +
                                "<td style=\"border-left:0px;\"></td></tr>");

            foreach (LaboratoryCompare labHeader in header)
            {
                string headono1 = ""; string headono2 = ""; string headono3 = ""; string headono4 = ""; string headono5 = "";
                if (labHeader.ONO_F_1 == "*" || labHeader.ONO_F_1 == "H" || labHeader.ONO_F_1 == "HH" || labHeader.ONO_F_1 == "L" || labHeader.ONO_F_1 == "LL")
                {
                    headono1 = "<label style=\"color:red; font-weight:bold;\">" + labHeader.onO_1 + " " + LowHighChecker(labHeader.onO_1, labHeader.reF_RANGE).ToString() + "</label>";
                }
                else
                {
                    headono1 = "<label style=\"color:black;\">" + labHeader.onO_1 + "</label>";
                }
                if (labHeader.ONO_F_2 == "*" || labHeader.ONO_F_2 == "H" || labHeader.ONO_F_2 == "HH" || labHeader.ONO_F_2 == "L" || labHeader.ONO_F_2 == "LL")
                {
                    headono2 = "<label style=\"color:red; font-weight:bold;\">" + labHeader.onO_2 + " " + LowHighChecker(labHeader.onO_2, labHeader.reF_RANGE).ToString() + "</label>";
                }
                else
                {
                    headono2 = "<label style=\"color:black;\">" + labHeader.onO_2 + "</label>";
                }
                if (labHeader.ONO_F_3 == "*" || labHeader.ONO_F_3 == "H" || labHeader.ONO_F_3 == "HH" || labHeader.ONO_F_3 == "L" || labHeader.ONO_F_3 == "LL")
                {
                    headono3 = "<label style=\"color:red; font-weight:bold;\">" + labHeader.onO_3 + " " + LowHighChecker(labHeader.onO_3, labHeader.reF_RANGE).ToString() + "</label>";
                }
                else
                {
                    headono3 = "<label style=\"color:black;\">" + labHeader.onO_3 + "</label>";
                }
                if (labHeader.ONO_F_4 == "*" || labHeader.ONO_F_4 == "H" || labHeader.ONO_F_4 == "HH" || labHeader.ONO_F_4 == "L" || labHeader.ONO_F_4 == "LL")
                {
                    headono4 = "<label style=\"color:red; font-weight:bold;\">" + labHeader.onO_4 + " " + LowHighChecker(labHeader.onO_4, labHeader.reF_RANGE).ToString() + "</label>";
                }
                else
                {
                    headono4 = "<label style=\"color:black;\">" + labHeader.onO_4 + "</label>";
                }
                if (labHeader.ONO_F_5 == "*" || labHeader.ONO_F_5 == "H" || labHeader.ONO_F_5 == "HH" || labHeader.ONO_F_5 == "L" || labHeader.ONO_F_5 == "LL")
                {
                    headono5 = "<label style=\"color:red; font-weight:bold;\">" + labHeader.onO_5 + " " + LowHighChecker(labHeader.onO_5, labHeader.reF_RANGE).ToString() + "</label>";
                }
                else
                {
                    headono5 = "<label style=\"color:black;\">" + labHeader.onO_5 + "</label>";
                }

                table.Append("<tr><td style=\"border-left:0px;\"><b>" + labHeader.tesT_NM + "</b></td>" +
                                    "<td><b>" + headono1 + "</b></td>" +
                                    "<td><b>" + headono2 + "</b></td>" +
                                    "<td><b>" + headono3 + "</b></td>" +
                                    "<td><b>" + headono4 + "</b></td>" +
                                    "<td><b>" + headono5 + "</b></td>" +
                                    "<td><b>" + labHeader.unit + "</b></td>" +
                                    "<td><b>" + labHeader.reF_RANGE + "</b></td></tr>");


                var item = data.FindAll(x => x.ordeR_TESTNM == labHeader.ordeR_TESTNM && x.isInfo == 0 && x.IsHeader == 0);
                foreach (LaboratoryCompare itemData in item)
                {
                    string ono1 = ""; string ono2 = ""; string ono3 = ""; string ono4 = ""; string ono5 = "";
                    if (itemData.ONO_F_1 == "*" || itemData.ONO_F_1 == "H" || itemData.ONO_F_1 == "HH" || itemData.ONO_F_1 == "L" || itemData.ONO_F_1 == "LL")
                    {
                        ono1 = "<label style=\"color:red; font-weight:bold;\">" + itemData.onO_1 + " " + LowHighChecker(itemData.onO_1, itemData.reF_RANGE).ToString() + "</label>";
                    }
                    else
                    {
                        ono1 = "<label style=\"color:black;\">" + itemData.onO_1 + "</label>";
                    }
                    if (itemData.ONO_F_2 == "*" || itemData.ONO_F_2 == "H" || itemData.ONO_F_2 == "HH" || itemData.ONO_F_2 == "L" || itemData.ONO_F_2 == "LL")
                    {
                        ono2 = "<label style=\"color:red; font-weight:bold;\">" + itemData.onO_2 + " " + LowHighChecker(itemData.onO_2, itemData.reF_RANGE).ToString() + "</label>";
                    }
                    else
                    {
                        ono2 = "<label style=\"color:black;\">" + itemData.onO_2 + "</label>";
                    }
                    if (itemData.ONO_F_3 == "*" || itemData.ONO_F_3 == "H" || itemData.ONO_F_3 == "HH" || itemData.ONO_F_3 == "L" || itemData.ONO_F_3 == "LL")
                    {
                        ono3 = "<label style=\"color:red; font-weight:bold;\">" + itemData.onO_3 + " " + LowHighChecker(itemData.onO_3, itemData.reF_RANGE).ToString() + "</label>";
                    }
                    else
                    {
                        ono3 = "<label style=\"color:black;\">" + itemData.onO_3 + "</label>";
                    }
                    if (itemData.ONO_F_4 == "*" || itemData.ONO_F_4 == "H" || itemData.ONO_F_4 == "HH" || itemData.ONO_F_4 == "L" || itemData.ONO_F_4 == "LL")
                    {
                        ono4 = "<label style=\"color:red; font-weight:bold;\">" + itemData.onO_4 + " " + LowHighChecker(itemData.onO_4, itemData.reF_RANGE).ToString() + "</label>";
                    }
                    else
                    {
                        ono4 = "<label style=\"color:black;\">" + itemData.onO_4 + "</label>";
                    }
                    if (itemData.ONO_F_5 == "*" || itemData.ONO_F_5 == "H" || itemData.ONO_F_5 == "HH" || itemData.ONO_F_5 == "L" || itemData.ONO_F_5 == "LL")
                    {
                        ono5 = "<label style=\"color:red; font-weight:bold;\">" + itemData.onO_5 + " " + LowHighChecker(itemData.onO_5, itemData.reF_RANGE).ToString() + "</label>";
                    }
                    else
                    {
                        ono5 = "<label style=\"color:black;\">" + itemData.onO_5 + "</label>";
                    }

                    table.Append("<tr><td style=\"border-left:0px;\">" + itemData.tesT_NM + "</td>" +
                                    "<td>" + ono1 + "</td>" +
                                    "<td>" + ono2 + "</td>" +
                                    "<td>" + ono3 + "</td>" +
                                    "<td>" + ono4 + "</td>" +
                                    "<td>" + ono5 + "</td>" +
                                    "<td>" + itemData.unit + "</td>" +
                                    "<td>" + itemData.reF_RANGE + "</td></tr>");
                }
            }
            table.Append("</table></div>");
            tbl_compare.InnerHtml = table.ToString();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "setTableCompare", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "setTableCompare", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    public string LowHighChecker(string value, string range)
    {
        var rangedata = range.Split('-');
        if (rangedata != null && rangedata.Count() > 1)
        {
            try
            {
                decimal rmin = decimal.Parse(rangedata[0].ToString().Replace(".", ","));
                decimal rmax = decimal.Parse(rangedata[1].ToString().Replace(".", ","));
                decimal rval = decimal.Parse(value.Replace(".", ","));

                if (rval > rmax)
                {
                    return " &nbsp;(H)";
                }
                else if (rval < rmin)
                {
                    return " &nbsp;(L)";
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }

    void getCompareResult(String patientID, String orderTestName)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<LaboratoryCompare> labCompareTestGroup = new List<LaboratoryCompare>();

        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Patient_ID", patientID },
                { "Test_Group", orderTestName }
            };
            //Log.Debug(LogConfig.LogStart("getlabCompareTestGroup", logParam));
            var dataLaboratory = clsResult.getlabCompareTestGroup(patientID, orderTestName);
            var JsonLaboratory = JsonConvert.DeserializeObject<ResultLaboratoryCompare>(dataLaboratory.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("getlabCompareTestGroup", JsonLaboratory.Status, JsonLaboratory.Message));

            if (JsonLaboratory != null)
            {
                labCompareTestGroup = JsonLaboratory.list;

                setTableCompare(labCompareTestGroup, orderTestName);
                img_noData.Visible = false;
                pg_index_compare.Visible = true;
            }
            else
            {
                tbl_compare.InnerHtml = new StringBuilder().Append("").ToString();
                img_noData.Visible = true;
                pg_index_compare.Visible = false;
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getCompareResult", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "getCompareResult", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }


        //Log.Info(LogConfig.LogEnd());
    }

    protected void btn_compare_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<String> gridDataHeader = (List<String>)Session[Helper.ViewStateAllHeaderLaboratory];
        List<LaboratoryHeader> gridItemList = (List<LaboratoryHeader>)Session[Helper.ViewStateItemList];
        Session.Remove(Helper.ViewStateHeaderChecklist);

        try
        {

            Session[Helper.ViewStateHeaderChecklist] = hf_test_group.Value;
            var headerCheck = hf_test_group.Value.Split(',');
            getCompareResult(hfPatientId.Value, headerCheck[0]);
            btnPrev.Enabled = false;
            //hf_test_group.Value = "";
            lbl_count_paging.Text = "1";

            if (headerCheck.ToList().Count() == 1)
            {
                btnPrev.Enabled = false;
                btnPrev.Style.Add("cursor", "not-allowed");
                btnNext.Enabled = false;
                btnNext.Style.Add("cursor", "not-allowed");
            }
            else
            {
                btnPrev.Enabled = false;
                btnPrev.Style.Add("cursor", "not-allowed");
                btnNext.Enabled = true;
                btnNext.Style.Add("cursor", "pointer");
            }

            compareResult.Visible = true;

            createinnerHTMLHeader(gridDataHeader, gridItemList);
            src_item_txt.Text = "";

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "btn_compare_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "btn_compare_Click", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }


        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        var headerCheck = ((String)Session[Helper.ViewStateHeaderChecklist]).Split(',');

        var currentPaging = Int64.Parse(lbl_count_paging.Text);

        lbl_count_paging.Text = (currentPaging - 1).ToString();
        List<LaboratoryCompare> labCompareTestGroup = new List<LaboratoryCompare>();
        getCompareResult(hfPatientId.Value, headerCheck[(int)currentPaging - 2]);

        if (headerCheck.ToList().Count != 1)
        {
            btnNext.Enabled = true;
            btnNext.Style.Add("cursor", "pointer");
        }

        if (currentPaging - 1 == 1)
        {
            btnPrev.Enabled = false;
            btnPrev.Style.Add("cursor", "not-allowed");
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "btnPrev_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        var headerCheck = ((String)Session[Helper.ViewStateHeaderChecklist]).Split(',');

        var currentPaging = Int64.Parse(lbl_count_paging.Text);

        lbl_count_paging.Text = (currentPaging + 1).ToString();
        List<LaboratoryCompare> labCompareTestGroup = new List<LaboratoryCompare>();
        getCompareResult(hfPatientId.Value, headerCheck[(int)currentPaging]);

        if (currentPaging == headerCheck.ToList().Count - 1)
        {
            btnNext.Enabled = false;
            btnNext.Style.Add("cursor", "not-allowed");
        }
        btnPrev.Enabled = true;
        btnPrev.Style.Add("cursor", "pointer");

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "btnNext_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void btn_lab_result_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<LaboratoryResult> listlaboratory = new List<LaboratoryResult>();

        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Patient_ID", hfPatientId.Value },
                { "Admission_ID", hfAdmissionId.Value },
                { "Ono_ID", hf_ono_id.Value }
            };
            //Log.Debug(LogConfig.LogStart("getLabByOno", logParam));
            var dataLaboratory = clsResult.getLabByOno(hfPatientId.Value, hfAdmissionId.Value, hf_ono_id.Value);
            var JsonLaboratory = JsonConvert.DeserializeObject<ResponseLaboratoryResult>(dataLaboratory.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("getLabByOno", JsonLaboratory.Status, JsonLaboratory.Message));

            listlaboratory = new List<LaboratoryResult>();
            listlaboratory = JsonLaboratory.Data;
            StdLabResult.initializevalue(listlaboratory);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "btn_lab_result_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "PatientId", hfPatientId.Value.ToString(), "btn_lab_result_Click", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }
}