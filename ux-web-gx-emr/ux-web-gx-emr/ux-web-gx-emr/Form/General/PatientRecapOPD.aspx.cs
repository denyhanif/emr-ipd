using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_General_PatientRecapOPD : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public string offsetforui = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        
        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            if (Helper.GetUserID(this) == null)
            {
                Response.Redirect("~/Form/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                HyperLink test = Master.FindControl("PatientRecapOPDLink") as HyperLink;
                test.Style.Add("background-color", "#D6DBFF");
            }

            List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
            markerlist.Find(x => x.key == "DOBmarker").value = "marked";
            Session[Helper.SESSIONmarker] = markerlist;

            DateTextboxStart.Text = DateTime.Now.ToString("dd MMM yyyy");
            DateTextboxEnd.Text = DateTime.Now.ToString("dd MMM yyyy");
            HFLimitOnprogress.Value = "50";
            HFOffsetOnprogress.Value = "0";

            GetRecapOPD();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "", "", "Page_Load", StartTime, EndTime, "OK", MyUser.GetUsername()
                            , "", "", ""));
            // Log.Info(LogConfig.LogEnd());
        }
    }

    protected string GetLocalIPAddress()
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

    public void GetRecapOPD()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {

            //Guid hospitalid = Guid.Parse("49a1ef75-2bdb-4586-b287-557929a7d873");
            //Guid doctorid = Guid.Parse("dda4310c-6319-498b-8a9d-8b521c96cbe3");
            Int64 hospitalid = Helper.organizationId; //14;
            Int64 doctorid =  Int64.Parse(Helper.GetDoctorID(this)); //14000000369;
            string datefrom = DateTime.Parse(DateTextboxStart.Text).ToString("yyyy-MM-dd");
            string dateto = DateTime.Parse(DateTextboxEnd.Text).ToString("yyyy-MM-dd");
            int limitt = int.Parse(HFLimitOnprogress.Value); //50;
            int offsett = int.Parse(HFOffsetOnprogress.Value); //0;
            string nama = PatientSearch.Text;
            offsetforui = HFOffsetOnprogress.Value;

            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Hospital_ID", hospitalid.ToString() },
                { "Doctor_ID", doctorid.ToString() },
                { "Date_From", datefrom },
                { "Date_To", dateto },
                { "Limit", limitt.ToString() },
                { "Offset", offsett.ToString() },
                { "Patient_Name", nama }
            };
            //Log.Debug(LogConfig.LogStart("GetRecapOPD", logParam));
            var dataRecap = clsRecap.GetRecapOPD(hospitalid, doctorid, datefrom, dateto, limitt, offsett, nama);
            var RecapJson = JsonConvert.DeserializeObject<ResultRecapOPD>(dataRecap.Result.ToString());

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "GetRecapOPD", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataRecap.Result.ToString()));
            //Log.Debug(LogConfig.LogEnd("GetRecapOPD", RecapJson.Status, RecapJson.Message));

            List<RecapOPD> RecapOPD = new List<RecapOPD>();
            RecapOPD = RecapJson.data.list;

            if (RecapOPD.Count > 0)
            {
                Gvw_RecapOPD.DataSource = Helper.ToDataTable(RecapOPD);
                Gvw_RecapOPD.DataBind();

                img_noData.Visible = false;
                div_pagination.Visible = true;
            }
            else
            {
                Gvw_RecapOPD.DataSource = null;
                Gvw_RecapOPD.DataBind();

                img_noData.Visible = true;
                div_pagination.Visible = false;
            }

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "GetRecapOPD", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        //Log.Info(LogConfig.LogStart());

        resetoffsetpage();
        GetRecapOPD();
        prevnextonprogress_status();

        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonNextPageOnprogress_Click(object sender, EventArgs e)
    {
        //Log.Info(LogConfig.LogStart());

        HFOffsetOnprogress.Value = calcnextpage(HFOffsetOnprogress.Value, HFLimitOnprogress.Value);
        //populate_worklistQueue("0");
        GetRecapOPD();
        prevnextonprogress_status();

        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonPrevPageOnprogress_Click(object sender, EventArgs e)
    {
        //Log.Info(LogConfig.LogStart());

        HFOffsetOnprogress.Value = calcprevpage(HFOffsetOnprogress.Value, HFLimitOnprogress.Value);
        //populate_worklistQueue("0");
        GetRecapOPD();
        prevnextonprogress_status();

        //Log.Info(LogConfig.LogEnd());
    }

    public void prevnextonprogress_status()
    {
        if (int.Parse(HFOffsetOnprogress.Value) <= 0)
        {
            ButtonPrevPageOnprogress.Enabled = false;
        }
        else
        {
            ButtonPrevPageOnprogress.Enabled = true;
        }

        if (Gvw_RecapOPD.Rows.Count < int.Parse(HFLimitOnprogress.Value))
        {
            ButtonNextPageOnprogress.Enabled = false;
        }
        else
        {
            ButtonNextPageOnprogress.Enabled = true;
        }
    }

    public string calcnextpage(string offset, string limit)
    {
        return (int.Parse(offset) + int.Parse(limit)).ToString();
    }

    public string calcprevpage(string offset, string limit)
    {
        return (int.Parse(offset) - int.Parse(limit)).ToString();
    }

    public void resetoffsetpage()
    {
        HFOffsetOnprogress.Value = "0";
    }
}