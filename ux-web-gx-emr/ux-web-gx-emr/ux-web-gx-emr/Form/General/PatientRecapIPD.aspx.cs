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

public partial class Form_General_PatientRecapIPD : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                HyperLink test = Master.FindControl("PatientRecapIPDLink") as HyperLink;
                test.Style.Add("background-color", "#D6DBFF");
            }

            List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
            markerlist.Find(x => x.key == "DOBmarker").value = "marked";
            Session[Helper.SESSIONmarker] = markerlist;

            GetRecapIPD();

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "", "", "Page_Load", StartTime, EndTime, "OK", MyUser.GetUsername()
                            , "", "", ""));
            //Log.Info(LogConfig.LogEnd());
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

    public void GetRecapIPD()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {

            Int64 hospitalid = Helper.organizationId; //14; 
            Int64 doctorid = Int64.Parse(Helper.GetDoctorID(this)); //14000000369;

            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Hospital_ID", hospitalid.ToString() },
                { "Doctor_ID", doctorid.ToString() }
            };
            //Log.Debug(LogConfig.LogStart("GetRecapIPD", logParam));
            var dataRecap = clsRecap.GetRecapIPD(hospitalid, doctorid);
            var RecapJson = JsonConvert.DeserializeObject<ResultRecap>(dataRecap.Result.ToString());

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "GetRecapIPD", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", dataRecap.Result.ToString()));
            //Log.Debug(LogConfig.LogEnd("GetRecapIPD", RecapJson.Status, RecapJson.Message));

            List<Recap> RecapIPD = new List<Recap>();
            RecapIPD = RecapJson.data.list;

            if (RecapIPD.Count > 0)
            {
                Gvw_RecapIPD.DataSource = Helper.ToDataTable(RecapIPD);
                Gvw_RecapIPD.DataBind();

                img_noData.Visible = false;
            }
            else
            {
                Gvw_RecapIPD.DataSource = null;
                Gvw_RecapIPD.DataBind();

                img_noData.Visible = true;
            }

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "DoctorID", Helper.GetDoctorID(this), "GetRecapIPD", StartTime, ErrorTime, "OK", Helper.GetLoginUser(this.Page), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }
}