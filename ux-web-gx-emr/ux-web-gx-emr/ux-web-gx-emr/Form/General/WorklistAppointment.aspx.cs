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
using System.Web.Services;

public partial class Form_General_WorklistAppointment : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public string TeleConfig = "";
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

        if (Session[Helper.SessionOrganizationSetting] != null)
        {
            List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)Session[Helper.SessionOrganizationSetting];
            if (orgsetting.Find(y => y.setting_name.ToUpper() == "USE_TELECONSUL_SOAP".ToUpper()) != null)
            {
                if (orgsetting.Find(y => y.setting_name.ToUpper() == "USE_TELECONSUL_SOAP".ToUpper()).setting_value == "TRUE")
                {
                    TeleConfig = "True";
                }
                else
                {
                    TeleConfig = "False";
                }
            }
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

                HyperLink linkactive = Master.FindControl("linkGoSomewhere") as HyperLink;
                linkactive.Style.Add("background-color", "#D6DBFF");
                string doctorid = Helper.GetDoctorID(this).ToString();
                hfOrgID.Value = Helper.organizationId.ToString();
                TextBoxDateSelected.Text = DateTime.Now.ToString("dd MMM yyyy");
                //DateTextboxStart.Text = DateTime.Now.ToString("dd MMM yyyy");
                //DateTextboxEnd.Text = DateTime.Now.ToString("dd MMM yyyy");

                if (hfOrgID.Value != "0" && doctorid != "")
                {
                    DataTable datalogin = (DataTable)Session[Helper.SessionListOrganization];

                    if (Session[Helper.SessionLastKeywordAppointment] != null)
                    {
                        List<string> lk = (List<string>)Session[Helper.SessionLastKeywordAppointment];
                        if (lk.ElementAt(0) == "1")
                        {
                            opd.Checked = true;
                        }
                        else if (lk.ElementAt(0) == "3")
                        {
                            ed.Checked = true;
                        }
                        TextBoxDateSelected.Text = lk.ElementAt(1);
                        MRsearch.Text = lk.ElementAt(2);
                        status.SelectedValue = lk.ElementAt(3);
                        //DateTextboxStart.Text = lk.ElementAt(4);
                        //DateTextboxEnd.Text = lk.ElementAt(5);

                        GetDataWorklist(Int64.Parse(doctorid), Int64.Parse(hfOrgID.Value), int.Parse(lk.ElementAt(0)), DateTime.Parse(lk.ElementAt(1)), lk.ElementAt(2), int.Parse(lk.ElementAt(3)));
                    }
                    else
                    {
                        pageSetting();
                        if (opd.Checked)
                        {
                            GetDataWorklist(Int64.Parse(doctorid), Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(TextBoxDateSelected.Text), "", 0);
                        }
                        if (ed.Checked)
                        {
                            GetDataWorklist(Int64.Parse(doctorid), Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(TextBoxDateSelected.Text), "", 0);
                        }
                    }

                    Session[Helper.SessionUserID] = Helper.GetLoginUser(this).ToString();
                    //getRoom();
                }

                List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
                markerlist.Find(x => x.key == "SAVESOAPmarker").value = "unmarked";
                Session[Helper.SESSIONmarker] = markerlist;

                //DateTextboxStart.Attributes.Add("ReadOnly", "ReadOnly");
                //DateTextboxEnd.Attributes.Add("ReadOnly", "ReadOnly");
                TextBoxDateSelected.Attributes.Add("ReadOnly", "ReadOnly");

                if (Session["alertpassexpired"] != null)
                {
                    string pesan = (string)Session["alertpassexpired"];
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ExpiredNotif", "warningnotification('"+ pesan +"');", true);
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

        setColorDate();
        showHideCheckin();
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

    void ShowToastr(string message, string title, string type)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastr_message",
             String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
    }

    void GetTotalTicket(List<ViewWorklistAppointment> worklistAppointmentData)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (worklistAppointmentData != null)
        {
            var totalticketsubmit = worklistAppointmentData.FindAll(x => x.Status == "Submit").Count;
            var totalticketcancel = worklistAppointmentData.FindAll(x => x.Status == "Cancelled").Count;
            var getticket = worklistAppointmentData.Count;
            var totalticketCheckIn = worklistAppointmentData.FindAll(x => x.AdmissionId != 0).Count; //getticket - totalticketcancel;

            if (worklistAppointmentData.Count != 0)
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
        else
        {
            patientotal.Text = "0";
            checkin.Text = "0";
            totalcancel.Text = "0";
            lblsubmitcount.Text = "0";
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "GetTotalTicket", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());
    }

    void fill_gvw_worklistAppointment(List<ViewWorklistAppointment> worklist)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            if (worklist.Count == 0)
            {
                //gvw_data.Visible = false;
                //img_noConnection.Style.Add("display", "none");
                //img_noData.Style.Add("display", "");
            }
            else
            {
                if (worklist[0].ConnStatus == null)
                {
                    //gvw_data.Visible = false;
                    //img_noConnection.Style.Add("display", "");
                    //img_noData.Style.Add("display", "none");
                }
                else
                {
                    DataTable dt = Helper.ToDataTable(worklist);

                    if (worklist.ElementAt(0).AppointmentType == "3")
                    {
                        DataTable dtantrianfixed = Helper.ToDataTable(worklist.FindAll(x => x.Status != "Submit" && x.Status != "Cancelled" && x.AppointmentType == "3" && x.IsWaitingList == false));
                        if (dtantrianfixed.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtantrianfixed.Rows.Count; i++)
                            {
                                dtantrianfixed.Rows[i]["TempToday"] = DateTime.Parse(TextBoxDateSelected.Text).ToString();
                            }
                        }
                        RepeaterPasien.DataSource = dtantrianfixed;
                        RepeaterPasien.DataBind();
                        if (dtantrianfixed.Rows.Count == 0)
                        {
                            rownodata_fixed.Visible = true;
                        }
                        else
                        {
                            rownodata_fixed.Visible = false;
                        }

                        divfixedworklist.Visible = true;
                        divhourlyworklist.Visible = false;
                        divfirstcomeworklist.Visible = false;
                        divwaitingworklist.Visible = true;
                    }
                    else if (worklist.ElementAt(0).AppointmentType == "2")
                    {

                        DataTable dtantrianhourly = Helper.ToDataTable(worklist.FindAll(x => x.Status != "Submit" && x.Status != "Cancelled" && x.AppointmentType == "2" && x.IsWaitingList == false));
                        //var listantrianhourly = Helper.ToDataList<ViewWorklistAppointment>(dtantrianhourly);
                        //var listdistinct = listantrianhourly.Select(x => x.AppointmentToTime).Distinct();
                        //List<int> listcount = new List<int>();
                        //for(int i=0; i<listdistinct.Count(); i++)
                        //{
                        //    int jml = listantrianhourly.Count(y => y.AppointmentToTime == listdistinct.ElementAt(i));
                        //    listcount.Add(jml);
                        //}
                        //int inc = 0;
                        //for (int j = 0; j < listcount.Count(); j++)
                        //{
                        //    for (int k = 0; k < listcount.ElementAt(j); k++)
                        //    {
                        //        dtantrianhourly.Rows[inc]["TempIndex"] = k;
                        //        inc++;
                        //    }
                        //}
                        if (dtantrianhourly.Rows.Count > 0)
                        {
                            int idx = 0;
                            var tempx = dtantrianhourly.Rows[0]["AppointmentToTime"].ToString();
                            for (int i = 0; i < dtantrianhourly.Rows.Count; i++)
                            {
                                if (tempx != dtantrianhourly.Rows[i]["AppointmentToTime"].ToString())
                                {
                                    tempx = dtantrianhourly.Rows[i]["AppointmentToTime"].ToString();
                                    idx = 0;
                                }
                                
                                dtantrianhourly.Rows[i]["TempIndex"] = idx;
                                idx++;

                                dtantrianhourly.Rows[i]["TempToday"] = DateTime.Parse(TextBoxDateSelected.Text).ToString();
                            }
                        }
                        RepeaterPasienHourly.DataSource = dtantrianhourly;
                        RepeaterPasienHourly.DataBind();
                        if (dtantrianhourly.Rows.Count == 0)
                        {
                            rownodata_hourly.Visible = true;
                        }
                        else
                        {
                            rownodata_hourly.Visible = false;
                        }

                        divfixedworklist.Visible = false;
                        divhourlyworklist.Visible = true;
                        divfirstcomeworklist.Visible = false;
                        divwaitingworklist.Visible = true;
                    }
                    else if (worklist.ElementAt(0).AppointmentType == "1")
                    {
                        DataTable dtantrianfirstcome = Helper.ToDataTable(worklist.FindAll(x => x.Status != "Submit" && x.Status != "Cancelled" && x.AppointmentType == "1" && x.IsWaitingList == false));
                        if (dtantrianfirstcome.Rows.Count > 0)
                        {
                            int idx = 0;
                            var tempx = dtantrianfirstcome.Rows[0]["AppointmentToTime"].ToString();
                            for (int i = 0; i < dtantrianfirstcome.Rows.Count; i++)
                            {
                                if (tempx != dtantrianfirstcome.Rows[i]["AppointmentToTime"].ToString())
                                {
                                    tempx = dtantrianfirstcome.Rows[i]["AppointmentToTime"].ToString();
                                    idx = 0;
                                }

                                dtantrianfirstcome.Rows[i]["TempIndex"] = idx;
                                idx++;
                            }
                        }
                        RepeaterPasienFirstCome.DataSource = dtantrianfirstcome;
                        RepeaterPasienFirstCome.DataBind();
                        if (dtantrianfirstcome.Rows.Count == 0)
                        {
                            rownodata_firstcome.Visible = true;
                        }
                        else
                        {
                            rownodata_firstcome.Visible = false;
                        }

                        divfixedworklist.Visible = false;
                        divhourlyworklist.Visible = false;
                        divfirstcomeworklist.Visible = true;
                        divwaitingworklist.Visible = false;
                    }


                    DataTable dtantriancancel = Helper.ToDataTable(worklist.FindAll(x => x.Status == "Cancelled"));
                    RepeaterPasienCancel.DataSource = dtantriancancel;
                    RepeaterPasienCancel.DataBind();

                    DataTable dtwaiting = Helper.ToDataTable(worklist.FindAll(x => x.Status != "Submit" && x.Status != "Cancelled" && x.IsWaitingList == true));
                    if (dtwaiting.Rows.Count > 0)
                    {
                        int idx = 0;
                        var tempx = dtwaiting.Rows[0]["AppointmentToTime"].ToString();
                        for (int i = 0; i < dtwaiting.Rows.Count; i++)
                        {
                            if (tempx != dtwaiting.Rows[i]["AppointmentToTime"].ToString())
                            {
                                tempx = dtwaiting.Rows[i]["AppointmentToTime"].ToString();
                                idx = 0;
                            }

                            dtwaiting.Rows[i]["TempIndex"] = idx;
                            idx++;
                        }

                        RepeaterPasienWaiting.DataSource = dtwaiting;
                        RepeaterPasienWaiting.DataBind();

                        divwaitingworklist.Visible = true;
                    }
                    else
                    {
                        divwaitingworklist.Visible = false;
                    }
                    

                    DataTable dtsubmit = Helper.ToDataTable(worklist.FindAll(x => x.Status == "Submit"));
                    RepeaterPasienSubmit.DataSource = dtsubmit;
                    RepeaterPasienSubmit.DataBind();
                    if (dtsubmit.Rows.Count == 0)
                    {
                        rownodata_submit.Visible = true;
                    }
                    else
                    {
                        rownodata_submit.Visible = false;
                    }

                    //img_noConnection.Style.Add("display", "none");
                    //img_noData.Style.Add("display", "none");
                }
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "fill_gvw_worklistAppointment", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "fill_gvw_worklistAppointment", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
            throw ex;
        }
        //Log.Info(LogConfig.LogEnd());
    }

    void GetDataWorklist(Int64 doctorId, Int64 orgId, Int64 admTypeId, DateTime dateselect, string search, int status)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        string rspn_status = "", rspn_message = "";

        try
        {
            //Log.Debug(LogConfig.LogStart("worklistdoctorappointment", doctorId + LogConfig.ParamDivider + orgId + LogConfig.ParamDivider + admTypeId + LogConfig.ParamDivider + dateselect + LogConfig.ParamDivider + search + LogConfig.ParamDivider + status));
            var getworklist = clsWorklistAppointment.worklistdoctorappointment(doctorId, orgId, admTypeId, dateselect, search, status);
            var listWorklist = JsonConvert.DeserializeObject<ResponseWorklistAppointment>(getworklist.Result.ToString());
            rspn_status = listWorklist.Status;
            rspn_message = listWorklist.Message;
            //Log.Debug(LogConfig.LogEnd("worklistdoctorappointment", rspn_status, rspn_message));

            if (rspn_status == "Success")
            {
                List<ViewWorklistAppointment> worklist = new List<ViewWorklistAppointment>();
                worklist = listWorklist.Data.ViewWorklistAppointments;

                List<ViewScheduleDoctor> schedule = new List<ViewScheduleDoctor>();
                schedule = listWorklist.Data.ViewScheduleDoctor;

                //keyword search
                List<string> lastkeyword = new List<string>();
                lastkeyword.Add(admTypeId.ToString());
                lastkeyword.Add(TextBoxDateSelected.Text);
                lastkeyword.Add(search);
                lastkeyword.Add(status.ToString());

                //lastkeyword.Add(DateTextboxStart.Text);
                //lastkeyword.Add(DateTextboxEnd.Text);

                Session[Helper.SessionLastKeywordAppointment] = lastkeyword;

                //if (worklist.Count > 0)
                if (worklist != null)
                {
                    fill_gvw_worklistAppointment(worklist);
                    GetTotalTicket(worklist);
                    img_noData.Style.Add("display", "none");
                }
                else
                {
                    divfixedworklist.Visible = false;
                    divhourlyworklist.Visible = false;
                    divfirstcomeworklist.Visible = false;
                    divwaitingworklist.Visible = false;

                    ClearWorklist();
                    GetTotalTicket(worklist);

                    if (dateselect.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd") && MRsearch.Text == "")
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

                    Session[Helper.ViewStateWorklistAppoitnmentData] = new List<ViewWorklistAppointment>();
                }

                //if (schedule.Count > 0)
                if (schedule != null)
                {
                    dropdownSchedule.DataSource = Helper.ToDataTable(schedule);
                    dropdownSchedule.DataTextField = "from_to_time";
                    dropdownSchedule.DataValueField = "schedule_id";
                    dropdownSchedule.DataBind();
                    dropdownSchedule.Items.Insert(0, new ListItem("- select -", "0"));

                    Session[Helper.SessionSchedule] = schedule;

                    ViewScheduleDoctor dataselect = schedule.Find(x => x.isCurrentRoom == "1");
                    if (dataselect != null)
                    {
                        LabelRoomSelected.Text = dataselect.room_name;
                        divaddcheckin.Visible = false;
                        diveditcheckin.Visible = true;

                        dropdownSchedule.SelectedValue = dataselect.schedule_id.ToString();
                        roombind();
                        dropdownRoom.SelectedValue = dataselect.room_mapping_id.ToString();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "enabledisablecall", "CallDelayFalse();", true);
                    }
                    else
                    {
                        divaddcheckin.Visible = true;
                        diveditcheckin.Visible = false;

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "enabledisablecall", "CallDelayTrue();", true);
                    }
                }
                else
                {
                    divaddcheckin.Visible = true;
                    diveditcheckin.Visible = false;

                    dropdownSchedule.Items.Clear();
                    dropdownSchedule.DataBind();

                    dropdownRoom.Items.Clear();
                    dropdownRoom.DataBind();

                    Session[Helper.SessionSchedule] = null;
                }
            }
            else
            {
                //Worklist
                divfixedworklist.Visible = false;
                divhourlyworklist.Visible = false;
                divfirstcomeworklist.Visible = false;
                divwaitingworklist.Visible = false;

                ClearWorklist();
                GetTotalTicket(null);

                if (dateselect.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd") && MRsearch.Text == "")
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

                Session[Helper.ViewStateWorklistAppoitnmentData] = new List<ViewWorklistAppointment>();

                //Schedule
                divaddcheckin.Visible = true;
                diveditcheckin.Visible = false;

                dropdownSchedule.Items.Clear();
                dropdownSchedule.DataBind();

                dropdownRoom.Items.Clear();
                dropdownRoom.DataBind();

                Session[Helper.SessionSchedule] = null;
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
            if (ex.Message.Contains("Unexpected character encountered while parsing value: O. Path"))
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "connection", "alert(" + "'" + ex.Message + "'" + ");", true);
            }
            else
            {
                throw ex;
            }

        }
    }

    public void ClearWorklist()
    {
        RepeaterPasien.DataSource = null;
        RepeaterPasien.DataBind();

        RepeaterPasienHourly.DataSource = null;
        RepeaterPasienHourly.DataBind();

        RepeaterPasienFirstCome.DataSource = null;
        RepeaterPasienFirstCome.DataBind();

        RepeaterPasienWaiting.DataSource = null;
        RepeaterPasienWaiting.DataBind();

        RepeaterPasienCancel.DataSource = null;
        RepeaterPasienCancel.DataBind();

        RepeaterPasienSubmit.DataSource = null;
        RepeaterPasienSubmit.DataBind();
        rownodata_submit.Visible = true;
    }

    protected void AdmTypeChanges(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            MRsearch.Text = "";
            status.SelectedValue = "0";
            string dtselect = Request.Form[TextBoxDateSelected.UniqueID];
            TextBoxDateSelected.Text = dtselect;

            Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());
            if (opd.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(TextBoxDateSelected.Text), "", 0);
            }
            if (ed.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(TextBoxDateSelected.Text), "", 0);
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
        //try
        //{
        //    //gvw_data.PageIndex = 0;
        //    HiddenPageIndex.Value = "0";
        //    //MRsearch.Text = "";
        //    //status.SelectedValue = "0";

        //    string dtstart = Request.Form[DateTextboxStart.UniqueID];
        //    DateTextboxStart.Text = dtstart;

        //    string dtend = Request.Form[DateTextboxEnd.UniqueID];
        //    DateTextboxEnd.Text = dtend;

        //    Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());

        //    if (opd.Checked)
        //    {
        //        GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
        //        //GetTotalTicket(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text));
        //    }
        //    if (ed.Checked)
        //    {
        //        GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
        //        //GetTotalTicket(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text));
        //    }
        //}
        //catch (Exception ex)
        //{
        //    log.Error(LogLibrary.Error("DateTextboxEnd_TextChanged", Helper.GetLoginUser(this), ex.InnerException.Message));
        //}
    }

    protected void DateTextboxStart_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    //gvw_data.PageIndex = 0;
        //    HiddenPageIndex.Value = "0";
        //    //MRsearch.Text = "";
        //    //status.SelectedValue = "0";

        //    string dtstart = Request.Form[DateTextboxStart.UniqueID];
        //    DateTextboxStart.Text = dtstart;

        //    string dtend = Request.Form[DateTextboxEnd.UniqueID];
        //    DateTextboxEnd.Text = dtend;

        //    Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());

        //    if (opd.Checked)
        //    {
        //        GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
        //        //GetTotalTicket(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text));
        //    }
        //    if (ed.Checked)
        //    {
        //        GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
        //        //GetTotalTicket(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(DateTextboxStart.Text), DateTime.Parse(DateTextboxEnd.Text));
        //    }
        //    List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
        //    markerlist.Find(x => x.key == "DOBmarker").value = "marked";
        //    Session[Helper.SESSIONmarker] = markerlist;
        //}
        //catch (Exception ex)
        //{
        //    log.Error(LogLibrary.Error("DateTextboxStart_TextChanged", Helper.GetLoginUser(this), ex.InnerException.Message));
        //}
    }

    protected void MRSearch_TextChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        try
        {
            string dtselect = Request.Form[TextBoxDateSelected.UniqueID];
            TextBoxDateSelected.Text = dtselect;

            Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());

            if (opd.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(TextBoxDateSelected.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
            }
            if (ed.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(TextBoxDateSelected.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
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
            string dtselect = Request.Form[TextBoxDateSelected.UniqueID];
            TextBoxDateSelected.Text = dtselect;

            Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());

            if (opd.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(TextBoxDateSelected.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
            }
            if (ed.Checked)
            {
                GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(TextBoxDateSelected.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
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
        var varResult = clsCommon.GetPatientHeader(Int64.Parse(patientID), hf_ticket_patient.Value.Replace("/", "")).Result;
        var JsongetPatientHistory = JsonConvert.DeserializeObject<ResultPatientHeader>(varResult);

        PatientCard.initializevalue(JsongetPatientHistory.header);
        List<LaboratoryResult> listlaboratory = new List<LaboratoryResult>();
        var dataLaboratory = clsResult.getLaboratoryResult(admissionId.ToString());
        var JsonLaboratory = JsonConvert.DeserializeObject<ResultLaboratoryResult>(dataLaboratory.Result.ToString());
        listlaboratory = new List<LaboratoryResult>();
        listlaboratory = JsonLaboratory.list;

        StdLabResult.initializevalue(listlaboratory);
        UP_LabModal.Update();
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "openmodalLab", "openLabModal();", true);

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "labResult_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void radResult_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        string rspn_status = "", rspn_message = "";

        if (hf_admiss_id.Value != "")
        {
            try
            {
                //Log.Debug(LogConfig.LogStart("GetPatientHeader", hf_patient_id.Value + LogConfig.ParamDivider + hf_ticket_patient.Value.Replace("/", "")));
                var varResult = clsCommon.GetPatientHeader(Int64.Parse(hf_patient_id.Value), hf_ticket_patient.Value.Replace("/", "")).Result;
                var JsongetPatientHistory = JsonConvert.DeserializeObject<ResponsePatientHeader>(varResult);
                rspn_status = JsongetPatientHistory.Status;
                rspn_message = JsongetPatientHistory.Message;
                //Log.Debug(LogConfig.LogEnd("Getworklistpharmacytele", rspn_status, rspn_message));

                PatientCardRad.initializevalue(JsongetPatientHistory.Data);

                //Log.Debug(LogConfig.LogStart("getRadResultAdmissionDetail", hf_admiss_id.Value));
                var dataAdmissionDetail = clsResult.getRadResultAdmissionDetail(hf_admiss_id.Value.ToString());
                var JsonAdmissionDetail = JsonConvert.DeserializeObject<ResponseRadiologyByWeek>(dataAdmissionDetail.Result);
                rspn_status = JsonAdmissionDetail.Status;
                rspn_message = JsonAdmissionDetail.Message;
                //Log.Debug(LogConfig.LogEnd("Getworklistpharmacytele", rspn_status, rspn_message));

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
            //img_noData.Style.Add("display", "none");
        }
        else
        {
            //img_noData.Style.Add("display", "");
        }

         //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonPrevDate_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        TextBoxDateSelected.Text =  DateTime.Parse(TextBoxDateSelected.Text).AddDays(-1).ToString("dd MMM yyyy");
        //DateTextboxStart.Text = TextBoxDateSelected.Text;
        //DateTextboxEnd.Text = TextBoxDateSelected.Text;
        setColorDate();
        showHideCheckin();

        Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());

        if (opd.Checked)
        {
            GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(TextBoxDateSelected.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
        }
        if (ed.Checked)
        {
            GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(TextBoxDateSelected.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "ButtonPrevDate_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonNextDate_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        TextBoxDateSelected.Text = DateTime.Parse(TextBoxDateSelected.Text).AddDays(1).ToString("dd MMM yyyy");
        //DateTextboxStart.Text = TextBoxDateSelected.Text;
        //DateTextboxEnd.Text = TextBoxDateSelected.Text;
        setColorDate();
        showHideCheckin();

        Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());

        if (opd.Checked)
        {
            GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(TextBoxDateSelected.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
        }
        if (ed.Checked)
        {
            GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(TextBoxDateSelected.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "ButtonNextDate_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void TextBoxDateSelected_TextChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        //DateTextboxStart.Text = TextBoxDateSelected.Text;
        //DateTextboxEnd.Text = TextBoxDateSelected.Text;
        Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());
        setColorDate();
        showHideCheckin();

        if (opd.Checked)
        {
            GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(TextBoxDateSelected.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
        }
        if (ed.Checked)
        {
            GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(TextBoxDateSelected.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "TextBoxDateSelected_TextChanged", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    public void setColorDate()
    {
        if (DateTime.Parse(TextBoxDateSelected.Text).Date == DateTime.Now.Date)
        {
            TextBoxDateSelected.Style.Add("color", "#555555");
        }
        else
        {
            TextBoxDateSelected.Style.Add("color", "#518bbd");
        }
    }

    protected void ButtonCallAppoint_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        int selRowIndex = ((RepeaterItem)(((Button)sender).Parent)).ItemIndex;
        HiddenField hfadmid = (HiddenField)RepeaterPasien.Items[selRowIndex].FindControl("HFAppointmentAdmissionId");
        HiddenField hforgid = (HiddenField)RepeaterPasien.Items[selRowIndex].FindControl("HFAppointmentHospitalId");
        HiddenField hfdocid = (HiddenField)RepeaterPasien.Items[selRowIndex].FindControl("HFAppointmentDoctorId");
        Label ptnname = (Label)RepeaterPasien.Items[selRowIndex].FindControl("lblPatientNameDisable");

        var CallAction = clsWorklistAppointment.CallPatientMySiloam(Guid.Parse(hforgid.Value), Guid.Parse(hfdocid.Value), Guid.Parse(hfadmid.Value));
        var JsonCall = (JObject)JsonConvert.DeserializeObject<dynamic>(CallAction.Result);
        var Status = JsonCall.Property("status").Value.ToString();
        var Message = JsonCall.Property("message").Value.ToString();

        if (Status == "ERROR")
        {
            ShowToastr(Status.Replace(@"'", @"\'") + ", " + Message.Replace(@"'", @"\'"), "Call Failed", "Error");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", Alert(Status,Message), true);
        }
        else
        {
            ShowToastr("Pasien " + ptnname.Text + " Telah dipanggil ke ruangan", "Call Success", "Success");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Call Success');", true);
        }

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this).ToString(), "ButtonCallAppoint_Click", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
    }

    public static string Alert(string status, string message)
    {
        return String.Format("alert('Failed To Call : {0},{1}');", status.Replace(@"'", @"\'"),message.Replace(@"'", @"\'"));
    }

    [WebMethod]
    public static string CallPatient(string admid, string orgid, string docid)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var _UserID = HttpContext.Current.Session[Helper.SessionUserID].ToString();
        Dictionary<string, string> logParam = new Dictionary<string, string>
        {
            { "Admission_ID", admid },
            { "Organization_ID", orgid },
            { "Doctor_ID", docid }
        };
        //Log.Debug(LogConfig.LogStart("CallPatientMySiloam", logParam));
        var CallAction = clsWorklistAppointment.CallPatientMySiloam(Guid.Parse(orgid), Guid.Parse(docid), Guid.Parse(admid));
        var JsonCall = (JObject)JsonConvert.DeserializeObject<dynamic>(CallAction.Result);
        var Status = JsonCall.Property("status").Value.ToString().Replace(@"'", @"\'");
        var Message = JsonCall.Property("message").Value.ToString().Replace(@"'", @"\'");
        //Log.Debug(LogConfig.LogEnd("CallPatientMySiloam", Status, Message));

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", docid, "CallPatient", StartTime, EndTime, "OK", _UserID, "", "", Message.ToString()));
        if (Status == "ERROR")
        {
            return Status + ", " + Message;
        }
        else
        {
            return "success";
        }
    }

    [WebMethod]
    public static string ClickPatient(string admid, string orgid, string docid, string apptid, string isaido)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var _UserID = HttpContext.Current.Session[Helper.SessionUserID].ToString();
        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Admission_ID", admid },
                { "Organization_ID", orgid },
                { "Doctor_ID", docid },
                { "Appointment_ID", apptid },
                { "Flag_Istele", isaido }
            };

            if (isaido.ToLower() == "false")
            {
                
                //Log.Debug(LogConfig.LogStart("FirstClickPatientMySiloam", logParam));
                var CallAction = clsWorklistAppointment.FirstClickPatientMySiloam(Guid.Parse(orgid), Guid.Parse(docid), Guid.Parse(admid));
                var JsonCall = (JObject)JsonConvert.DeserializeObject<dynamic>(CallAction.Result);
                var Status = JsonCall.Property("status").Value.ToString().Replace(@"'", @"\'");
                var Message = JsonCall.Property("message").Value.ToString().Replace(@"'", @"\'");
                //Log.Debug(LogConfig.LogEnd("FirstClickPatientMySiloam", Status, Message));

                if (Status == "ERROR")
                {
                    return Status + ", " + Message;
                }
                else
                {
                    return "success";
                }
            }
            else if (isaido.ToLower() == "true")
            {
                //Log.Debug(LogConfig.LogStart("FirstClickPatientTeleMySiloam", logParam));
                var CallAction = clsWorklistAppointment.FirstClickPatientTeleMySiloam(Guid.Parse(orgid), Guid.Parse(docid), Guid.Parse(apptid));
                var JsonCall = (JObject)JsonConvert.DeserializeObject<dynamic>(CallAction.Result);
                var Status = JsonCall.Property("status").Value.ToString().Replace(@"'", @"\'");
                var Message = JsonCall.Property("message").Value.ToString().Replace(@"'", @"\'");
                //Log.Debug(LogConfig.LogEnd("FirstClickPatientTeleMySiloam", Status, Message));

                if (Status == "ERROR")
                {
                    return Status + ", " + Message;
                }
                else
                {
                    return "success";
                }
            }
            else
            {
                return "ERROR";
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", docid, "ClickPatient", StartTime, EndTime, "OK", _UserID, "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", docid, "ClickPatient", StartTime, ErrorTime, "Error", _UserID, "", "", ex.Message));

            return "ERROR" + ", " + ex.ToString();
        }
    }

    protected void RefreshButton_Click(object sender, EventArgs e)
    {
        string doctorid = Helper.GetDoctorID(this).ToString();
        if (Session[Helper.SessionLastKeywordAppointment] != null)
        {
            List<string> lk = (List<string>)Session[Helper.SessionLastKeywordAppointment];
            if (lk.ElementAt(0) == "1")
            {
                opd.Checked = true;
            }
            else if (lk.ElementAt(0) == "3")
            {
                ed.Checked = true;
            }
            TextBoxDateSelected.Text = lk.ElementAt(1);
            MRsearch.Text = lk.ElementAt(2);
            status.SelectedValue = lk.ElementAt(3);
            //DateTextboxStart.Text = lk.ElementAt(4);
            //DateTextboxEnd.Text = lk.ElementAt(5);

            GetDataWorklist(Int64.Parse(doctorid), Int64.Parse(hfOrgID.Value), int.Parse(lk.ElementAt(0)), DateTime.Parse(lk.ElementAt(1)), lk.ElementAt(2), int.Parse(lk.ElementAt(3)));
        }
        else
        {
            if (opd.Checked)
            {
                GetDataWorklist(Int64.Parse(doctorid), Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(TextBoxDateSelected.Text), "", 0);
            }
            if (ed.Checked)
            {
                GetDataWorklist(Int64.Parse(doctorid), Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(TextBoxDateSelected.Text), "", 0);
            }
        }
    }

    public void getRoom()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        string rspn_status = "", rspn_message = "";

        try
        {
            //Log.Debug(LogConfig.LogStart("GetRoom", hfOrgID.Value));
            var getRoom = clsWorklistAppointment.GetRoom(Int64.Parse(hfOrgID.Value));
            var listRoom = JsonConvert.DeserializeObject<ResponseViewRoom>(getRoom.Result.ToString());
            rspn_status = listRoom.Status;
            rspn_message = listRoom.Message;
            //Log.Debug(LogConfig.LogEnd("getRoom", rspn_status, rspn_message));

            List<ViewRoom> room = new List<ViewRoom>();

            room = listRoom.Data;

            if (room.Count > 0)
            {
                dropdownRoom.DataSource = Helper.ToDataTable(room.Where(x => x.is_delete == false).ToList());
                dropdownRoom.DataTextField = "room_name";
                dropdownRoom.DataValueField = "room_mapping_id";
                dropdownRoom.DataBind();
                dropdownRoom.Items.Insert(0, new ListItem("- select -", "0"));
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "getRoom", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "getRoom", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);

            Session[CstSession.sessionerror] = ex;
            throw ex;
        }
    }

    protected void ButtonCheckin_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            Int64 orgid = Int64.Parse(hfOrgID.Value);
            Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this).ToString());
            string dateselect = DateTime.Parse(TextBoxDateSelected.Text).ToString("yyyy-MM-dd");
            Guid schid = Guid.Parse(dropdownSchedule.SelectedValue);
            Guid rmid = Guid.Parse(dropdownRoom.SelectedValue);
            string isPermanent = HF_isPermanent_selected.Value;
            Guid hosptialId = Guid.Parse(HF_HospitalIDMysiloam_selected.Value);
            Guid doctorId = Guid.Parse(HF_DoctorIDMysiloam_selected.Value);


            //Log.Debug(LogConfig.LogStart("CheckinRoom", orgid + LogConfig.ParamDivider + doctorid + LogConfig.ParamDivider + dateselect + LogConfig.ParamDivider + schid + LogConfig.ParamDivider + rmid + LogConfig.ParamDivider + isPermanent + LogConfig.ParamDivider + hosptialId + LogConfig.ParamDivider + doctorId));
            var CheckinAction = clsWorklistAppointment.CheckinRoom(orgid, doctorid, dateselect, schid, rmid, isPermanent, hosptialId, doctorId);
            var JsonChecin = (JObject)JsonConvert.DeserializeObject<dynamic>(CheckinAction.Result);
            var Status = JsonChecin.Property("status").Value.ToString();
            var Message = JsonChecin.Property("message").Value.ToString();
            //Log.Debug(LogConfig.LogEnd("CheckinRoom", Status, Message));

            if (Status == "Fail")
            {
                //ShowToastr(Status.Replace(@"'", @"\'") + ", " + Message.Replace(@"'", @"\'"), "Call Failed", "Error");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Checkin Error! "+ Message.Replace(@"'", @"\'") + "')", true);
            }
            else
            {
                //ShowToastr("Pasien " + ptnname.Text + " Telah dipanggil ke ruangan", "Call Success", "Success");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "connection", "alert('Call Success');", true);
                if (opd.Checked)
                {
                    GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 1, DateTime.Parse(TextBoxDateSelected.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
                }
                if (ed.Checked)
                {
                    GetDataWorklist(doctorid, Int64.Parse(hfOrgID.Value), 3, DateTime.Parse(TextBoxDateSelected.Text), MRsearch.Text, int.Parse(status.SelectedValue.ToString()));
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalChooseSchedule", "$('#modalChooseSchedule').modal('hide');", true);
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "ButtonCheckin_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
            
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "ButtonCheckin_Click", StartTime, ErrorTime, "Error", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            Session[CstSession.sessionerror] = ex;
            throw ex;
        }

        //Log.Info(LogConfig.LogEnd());
    }


    protected void dropdownSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());
        roombind();
        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "dropdownSchedule_SelectedIndexChanged", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    public void roombind()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<ViewScheduleDoctor> sch = (List<ViewScheduleDoctor>)Session[Helper.SessionSchedule];
        string schid = dropdownSchedule.SelectedValue.ToString();
        ViewScheduleDoctor dataselect = sch.Find(x => x.schedule_id.ToString() == schid);

        if (dataselect != null)
        {
            if (dataselect.isPermanent == "0")
            {
                getRoom();
            }
            else
            {
                DataTable dtr = new DataTable();
                dtr.Columns.Add("room_mapping_id");
                dtr.Columns.Add("room_name");
                dtr.Columns.Add("organization_id");
                dtr.Columns.Add("is_delete");

                dtr.Rows.Add(dataselect.room_mapping_id, dataselect.room_name, hfOrgID.Value.ToString(), false);

                dropdownRoom.DataSource = dtr;
                dropdownRoom.DataTextField = "room_name";
                dropdownRoom.DataValueField = "room_mapping_id";
                dropdownRoom.DataBind();
                dropdownRoom.Items.Insert(0, new ListItem("- select -", "0"));

                dropdownRoom.SelectedValue = dataselect.room_mapping_id.ToString();
            }

            HF_isPermanent_selected.Value = dataselect.isPermanent;
            HF_HospitalIDMysiloam_selected.Value = dataselect.hospital_id.ToString();
            HF_DoctorIDMysiloam_selected.Value = dataselect.doctor_id.ToString();
        }
        else
        {
            dropdownRoom.Items.Clear();
            dropdownRoom.Items.Insert(0, new ListItem("- select -", "0"));
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "doctorid", Helper.GetDoctorID(this).ToString(), "roombind", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    public void showHideCheckin()
    {
        if (DateTime.Parse(TextBoxDateSelected.Text).Date == DateTime.Now.Date)
        {
            divgroupCheckin.Visible = true;
        }
        else
        {
            divgroupCheckin.Visible = false;
        }
    }

    [WebMethod]
    public static string LogZoom(string encid, string orgid, string ptnid, string admid)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var _UserID = HttpContext.Current.Session[Helper.SessionUserID].ToString();
        if (encid == "" || orgid == "" || ptnid == "" || admid == "")
        {
            return "salah satu parameter belum lengkap!";
        }
        else
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Encounter_ID", encid },
                { "Organization_ID", orgid },
                { "Patient_ID", ptnid },
                { "Admission_ID", admid }
            };
            //Log.Debug(LogConfig.LogStart("PostLogZoom", logParam));
            var LogAction = clsWorklistAppointment.PostLogZoom(encid, long.Parse(orgid), long.Parse(ptnid), long.Parse(admid));
            var JsonLog = (JObject)JsonConvert.DeserializeObject<dynamic>(LogAction.Result);
            var Status = JsonLog.Property("status").Value.ToString().Replace(@"'", @"\'");
            var Message = JsonLog.Property("message").Value.ToString().Replace(@"'", @"\'");
            //Log.Debug(LogConfig.LogEnd("PostLogZoom", Status, Message));

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "Encounter_ID", encid, "LogZoom", StartTime, EndTime, "OK", _UserID, "", "", Message.ToString()));

            if (Status == "ERROR")
            {
                return Status + ", " + Message;
            }
            else
            {
                return "success";
            }
        }
    }
}