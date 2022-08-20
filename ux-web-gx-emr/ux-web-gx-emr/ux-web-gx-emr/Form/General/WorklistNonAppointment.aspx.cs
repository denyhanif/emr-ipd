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
using System.Configuration;

//Session[Helper.ViewStateWorklistNonAppoitnmentData]

public partial class Form_General_WorklistNonAppointment : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public string setENG = "none";
    public string setIND = "none";

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        if (Session[Helper.SessionLanguage] == null)
            Session[Helper.SessionLanguage] = 1;

        if (Session[Helper.SessionLanguage].ToString() == "1")
        {
            setENG = "";
            setIND = "none";
            HFisBahasa.Value = "ENG";
        }
        else if (Session[Helper.SessionLanguage].ToString() == "2")
        {
            setENG = "none";
            setIND = "";
            HFisBahasa.Value = "IND";
        }
        else
        {
            setENG = "";
            setIND = "none";
            HFisBahasa.Value = "ENG";
        }

        //set bahasa
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "bahasa", "switchBahasa();", true);

        if (!IsPostBack)
        {
            try
            {
                //Log.Info(LogConfig.LogStart());

                if (Session[Helper.SESSIONmarker] == null)
                {
                    Session[Helper.SESSIONmarker] = Helper.setInitializeMarker();
                }

                HyperLink test = Master.FindControl("linkGoSomewhere") as HyperLink;
                test.Style.Add("background-color", "#D6DBFF");
                DateTextboxStart.Text = DateTime.Now.ToString("dd MMM yyyy");
                DateTextboxEnd.Text = DateTime.Now.ToString("dd MMM yyyy");
                string doctorid = Helper.GetDoctorID(this).ToString();
                hfOrgID.Value = Helper.organizationId.ToString();

                if (hfOrgID.Value != "0" && doctorid != "")
                {
                    DataTable datalogin = (DataTable)Session[Helper.SessionListOrganization];
                    HyperLinkMysiloam.NavigateUrl = ConfigurationManager.AppSettings["linkMysiloam"].ToString() + "&doctorId=" + datalogin.Rows[0]["hope_user_id"].ToString() + "&hospitalId=" + Helper.organizationId;

                    if (Session[Helper.SessionLastKeyword] != null)
                    {
                        List<string> lk = (List<string>)Session[Helper.SessionLastKeyword];
                        if (lk.ElementAt(0) == "1")
                        {
                            opd.Checked = true;
                        }
                        else if (lk.ElementAt(0) == "3")
                        {
                            ed.Checked = true;
                        }
                        DateTextboxStart.Text = lk.ElementAt(1);
                        DateTextboxEnd.Text = lk.ElementAt(2);
                        MRsearch.Text = lk.ElementAt(3);
                        status.SelectedValue = lk.ElementAt(4);
                        HiddenPageIndex.Value = lk.ElementAt(5);
                        if (lk.ElementAt(6) == "25")
                        {
                            showPaging25.Checked = true;
                        }
                        else if (lk.ElementAt(6) == "15")
                        {
                            showPaging15.Checked = true;
                        }
                        else if (lk.ElementAt(6) == "10")
                        {
                            showPaging10.Checked = true;
                        }
                        else if (lk.ElementAt(6) == "")
                        {
                            showPaging10.Checked = true;
                        }
                        showPaging.SelectedValue = lk.ElementAt(6);
                        GetDataWorklist(Int64.Parse(doctorid), Int64.Parse(hfOrgID.Value), int.Parse(lk.ElementAt(0)), DateTime.Parse(lk.ElementAt(1)), DateTime.Parse(lk.ElementAt(2)), lk.ElementAt(3), int.Parse(lk.ElementAt(4)));
                    }
                    else
                    {
                        pageSetting();
                        HiddenPageIndex.Value = "0";
                        if (opd.Checked)
                        {
                            GetDataWorklist(Int64.Parse(doctorid), Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), "", 0);
                        }
                        if (ed.Checked)
                        {
                            GetDataWorklist(Int64.Parse(doctorid), Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), "", 0);
                        }

                    }
                }

                List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                markerlist.Find(x => x.key == "SAVESOAPmarker").value = "unmarked";
                Session[Helper.SESSIONmarker] = markerlist;

                DateTextboxStart.Attributes.Add("ReadOnly", "ReadOnly");
                DateTextboxEnd.Attributes.Add("ReadOnly", "ReadOnly");

                if (Session["alertpassexpired"] != null)
                {
                    string pesan = (string)Session["alertpassexpired"];
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ExpiredNotif", "warningnotification('" + pesan + "');", true);
                    Session["alertpassexpired"] = null;
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OTNotif", "RefreshCountNotifOT();", true);

                string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "Page_Load", StartTime, EndTime, "OK", MyUser.GetUsername()
                              , "", "", ""));
                //Log.Info(LogConfig.LogEnd());
            }
            catch (Exception ex)
            {
                Session[CstSession.sessionerror] = ex;
                throw ex;
            }
        }
    }

    void pageSetting()
    {
        List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
        string admtype = orgsetting.Find(y => y.setting_name.ToUpper() == "DEFAULT_ADMISSION_TYPE".ToUpper()).setting_value.ToUpper();
        if (admtype == "OPD")
        {
            opd.Checked = true;
        }
        else if (admtype == "ED")
        {
            ed.Checked = true;
        }
    }

    void GetTotalTicket(List<WorklistNonAppointment> worklistNonAppointmentData)
    {
        var totalticketsubmit = worklistNonAppointmentData.FindAll(x => x.Status == "Submit").Count;
        var totalticketcancel = worklistNonAppointmentData.FindAll(x => x.Status == "Cancelled").Count;
        var getticket = worklistNonAppointmentData.Count;
        var totalticketCheckIn = getticket - totalticketcancel;

        if(worklistNonAppointmentData.Count != 0)
        {
            patientotal.Text = getticket.ToString();
            checkin.Text = totalticketCheckIn.ToString();
            totalcancel.Text = totalticketcancel.ToString();
            lblsubmitcount.Text = totalticketsubmit.ToString();
        }
        else
        {
            patientotal.Text = "0";
            checkin.Text = "0";
            totalcancel.Text = "0";
            lblsubmitcount.Text = "0";
        }
    }

    void fill_gvw_worklistNonAppointment(List<WorklistNonAppointment> worklist)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); 
        //Log.Info(LogConfig.LogStart());

        try
        {
            if (worklist.Count == 0)
            {
                gvw_data.Visible = false;
                img_noConnection.Style.Add("display", "none");
                img_noData.Style.Add("display", "");
            }
            else
            {
                if (worklist[0].ConnStatus == null)
                {
                    gvw_data.Visible = false;
                    img_noConnection.Style.Add("display", "");
                    img_noData.Style.Add("display", "none");
                }
                else
                {
                    DataTable dt = Helper.ToDataTable(worklist);
                    if (showPaging.SelectedValue != "")
                    {
                        if (showPaging.SelectedValue == "0")
                        {
                            gvw_data.PageSize = worklist.Count();
                        }
                        else
                        {
                            gvw_data.PageSize = int.Parse(showPaging.SelectedValue);
                        }
                    }
                    gvw_data.Visible = true;
                    gvw_data.PageIndex = int.Parse(HiddenPageIndex.Value);
                    gvw_data.DataSource = dt;
                    gvw_data.DataBind();

                    img_noConnection.Style.Add("display", "none");
                    img_noData.Style.Add("display", "none");
                }
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "fill_gvw_worklistNonAppointment", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "fill_gvw_worklistNonAppointment", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
            throw ex;
        }

        //Log.Info(LogConfig.LogEnd());
    }

    void GetDataWorklist(Int64 doctorId, Int64 orgId, Int64 admTypeId, DateTime datestart, DateTime datefinish, string search,  int status)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Doctor_ID", doctorId.ToString() },
                { "Org_ID", orgId.ToString() },
                { "AdmType_ID", admTypeId.ToString() },
                { "Date_Start", datestart.ToString() },
                { "Date_Finish", datefinish.ToString() },
                { "Search", search.ToString() },
                { "Status", status.ToString() }
            };
            //Log.Debug(LogConfig.LogStart("worklistdoctor", logParam));
            var getworklist = clsWorklistNonAppointment.worklistdoctor(doctorId,orgId,admTypeId,datestart,datefinish,search,status);
            var listWorklist = JsonConvert.DeserializeObject<ResultWorklistNonAppointment>(getworklist.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("worklistdoctor", listWorklist.Status, listWorklist.Message));

            List<WorklistNonAppointment> worklist = new List<WorklistNonAppointment>();

            worklist = listWorklist.list;

            List<string> lastkeyword = new List<string>();
            lastkeyword.Add(admTypeId.ToString());
            lastkeyword.Add(DateTextboxStart.Text);
            lastkeyword.Add(DateTextboxEnd.Text);
            lastkeyword.Add(search);
            lastkeyword.Add(status.ToString());
            lastkeyword.Add(HiddenPageIndex.Value);
            lastkeyword.Add(showPaging.SelectedValue);
            Session[Helper.SessionLastKeyword] = lastkeyword;

            if (worklist.Count > 0)
            {
                fill_gvw_worklistNonAppointment(worklist);
                GetTotalTicket(worklist);
            }
            else
            {
                GetTotalTicket(worklist);
                gvw_data.Visible = false;

                if (datestart.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    img_noData.Style.Add("display", "");
                    no_patient_today.Style.Add("display", "");
                    no_patient_data.Style.Add("display", "none");
                }
                else
                {
                    img_noData.Style.Add("display", "");
                    no_patient_data.Style.Add("display", "");
                    no_patient_today.Style.Add("display", "none");
                }
                Session[Helper.ViewStateWorklistNonAppoitnmentData] = new List<WorklistNonAppointment>();
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "GetDataWorklist", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "GetDataWorklist", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
            throw ex;
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void grv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            gvw_data.PageIndex = e.NewPageIndex;
            HiddenPageIndex.Value = e.NewPageIndex.ToString();

            string dtstart = Request.Form[DateTextboxStart.UniqueID];
            DateTextboxStart.Text = dtstart;

            string dtend = Request.Form[DateTextboxEnd.UniqueID];
            DateTextboxEnd.Text = dtend;

            Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());
            if (opd.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
            }
            if (ed.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
            }

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "top", "topFunction();", true);

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "grv_PageIndexChanging", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "grv_PageIndexChanging", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
            throw ex;
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void AdmTypeChanges(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            gvw_data.PageIndex = 0;
            HiddenPageIndex.Value = "0";
            MRsearch.Text = "";
            status.SelectedValue = "0";
            string dtstart = Request.Form[DateTextboxStart.UniqueID];
            DateTextboxStart.Text = dtstart;

            string dtend = Request.Form[DateTextboxEnd.UniqueID];
            DateTextboxEnd.Text = dtend;

            Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());
            if (opd.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), "", 0);
            }
            if (ed.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), "", 0);
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "AdmTypeChanges", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "AdmTypeChanges", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
            throw ex;
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void DateTextboxEnd_TextChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            gvw_data.PageIndex = 0;
            HiddenPageIndex.Value = "0";
            //MRsearch.Text = "";
            //status.SelectedValue = "0";

            string dtstart = Request.Form[DateTextboxStart.UniqueID];
            DateTextboxStart.Text = dtstart;

            string dtend = Request.Form[DateTextboxEnd.UniqueID];
            DateTextboxEnd.Text = dtend;

            Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());

            if (opd.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
                //GetTotalTicket(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text));
            }
            if (ed.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
                //GetTotalTicket(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text));
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "DateTextboxEnd_TextChanged", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "DateTextboxEnd_TextChanged", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
            throw ex;
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void DateTextboxStart_TextChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            gvw_data.PageIndex = 0;
            HiddenPageIndex.Value = "0";
            //MRsearch.Text = "";
            //status.SelectedValue = "0";

            string dtstart = Request.Form[DateTextboxStart.UniqueID];
            DateTextboxStart.Text = dtstart;

            string dtend = Request.Form[DateTextboxEnd.UniqueID];
            DateTextboxEnd.Text = dtend;

            Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());

            if (opd.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
                //GetTotalTicket(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text));
            }
            if (ed.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
                //GetTotalTicket(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text));
            }
            List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
            markerlist.Find(x => x.key == "DOBmarker").value = "marked";
            Session[Helper.SESSIONmarker] = markerlist;

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "DateTextboxStart_TextChanged", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "DateTextboxStart_TextChanged", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
            throw ex;
        }
        //Log.Info(LogConfig.LogEnd());
    }

    protected void MRSearch_TextChanged(object sender, EventArgs e)
    {

        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            gvw_data.PageIndex = 0;
            HiddenPageIndex.Value = "0";

            string dtstart = Request.Form[DateTextboxStart.UniqueID];
            DateTextboxStart.Text = dtstart;

            string dtend = Request.Form[DateTextboxEnd.UniqueID];
            DateTextboxEnd.Text = dtend;

            Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());

            if (opd.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
            }
            if (ed.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
                //GetTotalTicket(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text));
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "MRSearch_TextChanged", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "MRSearch_TextChanged", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
            throw ex;
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void Status_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            gvw_data.PageIndex = 0;
            HiddenPageIndex.Value = "0";

            string dtstart = Request.Form[DateTextboxStart.UniqueID];
            DateTextboxStart.Text = dtstart;

            string dtend = Request.Form[DateTextboxEnd.UniqueID];
            DateTextboxEnd.Text = dtend;

            Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());

            if (opd.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
            }
            if (ed.Checked)
            {
                //GetTotalTicket(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text));
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "Status_SelectedIndexChanged", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "Status_SelectedIndexChanged", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
            throw ex;
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void labResult_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        ImageButton ibtn = sender as ImageButton;
        string admissionId = hf_admiss_id.Value;
        String patientID = hf_patient_id.Value;

        Dictionary<string, string> logParam = new Dictionary<string, string>
        {
            { "Patient_ID", patientID },
            { "Ticket_ID", hf_ticket_patient.Value.Replace("/", "") }
        };
        //Log.Debug(LogConfig.LogStart("GetPatientHeader", logParam));
        var varResult = clsCommon.GetPatientHeader(Int64.Parse(patientID), hf_ticket_patient.Value.Replace("/", "")).Result;
        var JsongetPatientHistory = JsonConvert.DeserializeObject<ResponsePatientHeader>(varResult);
        //Log.Debug(LogConfig.LogEnd("GetPatientHeader", JsongetPatientHistory.Status, JsongetPatientHistory.Message));

        PatientCard.initializevalue(JsongetPatientHistory.Data);
        List<LaboratoryResult> listlaboratory = new List<LaboratoryResult>();

        //Log.Debug(LogConfig.LogStart("getLaboratoryResult", LogConfig.LogParam("Admission_Id", admissionId.ToString())));
        var dataLaboratory = clsResult.getLaboratoryResult(admissionId.ToString());
        var JsonLaboratory = JsonConvert.DeserializeObject<ResponseLaboratoryResult>(dataLaboratory.Result.ToString());
        //Log.Debug(LogConfig.LogEnd("getLaboratoryResult", JsonLaboratory.Status, JsonLaboratory.Message));

        listlaboratory = new List<LaboratoryResult>();
        listlaboratory = JsonLaboratory.Data;

        StdLabResult.initializevalue(listlaboratory);
        UP_LabModal.Update();
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "openmodalLab", "openLabModal();", true);

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "labResult_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void showPaging_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        string doctorid = Helper.GetDoctorID(this).ToString();
        if (opd.Checked)
        {
            GetDataWorklist(Int64.Parse(doctorid), Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), "", 0);
        }
        if (ed.Checked)
        {
            GetDataWorklist(Int64.Parse(doctorid), Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), "", 0);
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "showPaging_SelectedIndexChanged", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void showPaging_CheckedChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        string doctorid = Helper.GetDoctorID(this).ToString();

        if (showPaging1.Checked)
        {
            showPaging.SelectedValue = hdn_paging1.Value;
            showPaging50.Checked = false;
            showPaging25.Checked = false;
            showPaging15.Checked = false;
            showPaging10.Checked = false;
        }

        if (showPaging50.Checked)
        {
            showPaging.SelectedValue = hdn_paging50.Value;
            showPaging1.Checked = false;
            showPaging25.Checked = false;
            showPaging15.Checked = false;
            showPaging10.Checked = false;
        }

        if (showPaging25.Checked)
        {
            showPaging.SelectedValue = hdn_paging25.Value;
            showPaging1.Checked = false;
            showPaging50.Checked = false;
            showPaging15.Checked = false;
            showPaging10.Checked = false;
        }

        if (showPaging15.Checked)
        {
            showPaging.SelectedValue = hdn_paging15.Value;
            showPaging1.Checked = false;
            showPaging25.Checked = false;
            showPaging50.Checked = false;
            showPaging10.Checked = false;
        }

        if (showPaging10.Checked)
        {
            showPaging.SelectedValue = hdn_paging10.Value;
            showPaging1.Checked = false;
            showPaging25.Checked = false;
            showPaging50.Checked = false;
            showPaging15.Checked = false;
        }

        if (opd.Checked)
        {
            GetDataWorklist(Int64.Parse(doctorid), Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
        }
        if (ed.Checked)
        {
            GetDataWorklist(Int64.Parse(doctorid), Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "showPaging_CheckedChanged", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void gvw_data_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !((e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate)) || (e.Row.RowState == DataControlRowState.Edit)))
        {
            Label lblStatus = (Label)e.Row.FindControl("Status_");
            if (lblStatus.Text.ToLower() == "submit")
            {
                e.Row.BackColor = System.Drawing.Color.PaleGreen;
            }
        }
        else if(e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#f4f4f4");
        }
    }

    protected void radResult_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (hf_admiss_id.Value != "")
        {
            try
            {
                Dictionary<string, string> logParam = new Dictionary<string, string>
                {
                    { "Patient_ID", hf_patient_id.Value },
                    { "Ticket_ID", hf_ticket_patient.Value.Replace("/", "") }
                };
                //Log.Debug(LogConfig.LogStart("GetPatientHeader", logParam));
                var varResult = clsCommon.GetPatientHeader(Int64.Parse(hf_patient_id.Value), hf_ticket_patient.Value.Replace("/", "")).Result;
                var JsongetPatientHistory = JsonConvert.DeserializeObject<ResponsePatientHeader>(varResult);
                //Log.Debug(LogConfig.LogEnd("GetPatientHeader", JsongetPatientHistory.Status, JsongetPatientHistory.Message));

                PatientCardRad.initializevalue(JsongetPatientHistory.Data);

                //Log.Debug(LogConfig.LogStart("getRadResultAdmissionDetail", LogConfig.LogParam("Admission_Id", hf_admiss_id.Value.ToString())));
                var dataAdmissionDetail = clsResult.getRadResultAdmissionDetail(hf_admiss_id.Value.ToString());
                var JsonAdmissionDetail = JsonConvert.DeserializeObject<ResponseRadiologyByWeek>(dataAdmissionDetail.Result);
                //Log.Debug(LogConfig.LogEnd("getRadResultAdmissionDetail", JsonAdmissionDetail.Status, JsonAdmissionDetail.Message));

                List<radiologyByWeek> listAdmissionDetail = JsonAdmissionDetail.Data;

                StdRadResult.initializevalue(listAdmissionDetail);
                UP_RadModal.Update();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "openRadModal", "openRadModal();", true);

                string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "radResult_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
                
            }
            catch (Exception ex)
            {
                string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "radResult_Click", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
                //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
                Session[CstSession.sessionerror] = ex;
                throw ex;
            }

            hf_admiss_id.Value = "";
            img_noData.Style.Add("display", "none");
        }
        else
        {
            img_noData.Style.Add("display", "");
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void queueCall_Click(object sender, ImageClickEventArgs e)
    {

    }
}