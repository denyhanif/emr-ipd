using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_General_ChangePassword : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public string setENG = "none";
    public string setIND = "none";

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();

        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            if (Session[Helper.SessionLanguage] == null)
                Session[Helper.SessionLanguage] = 1;

            if (Session[Helper.SessionLanguage].ToString() == "1")
            {
                HFisBahasa.Value = "ENG";
            }
            else if (Session[Helper.SessionLanguage].ToString() == "2")
            {
                HFisBahasa.Value = "IND";
            }
            else
            {
                HFisBahasa.Value = "ENG";
            }

            List<MarkerConfig> markerlist = (List<MarkerConfig>)Session[Helper.SESSIONmarker];
            markerlist.Find(x => x.key == "DOBmarker").value = "marked";
            Session[Helper.SESSIONmarker] = markerlist;

            //Log.Info(LogConfig.LogEnd());
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserID", MyUser.GetHopeUserID(), "Page_Load", StartTime, EndTime, "OK", ""
                            , "", "", ""));
        }

        //set bahasa
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "bahasa", "switchBahasa();", true);
    }

    //fungsi untuk menampilkan toast via akses javascript
    void ShowToastr(string message, string title, string type)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastr_message",
            String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
    }

    //fungsi save change password
    protected void Pass_ButtonSavePass_Click(object sender, EventArgs e)
    {
        //Log.Info(LogConfig.LogStart());
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
           // Log.Debug(LogConfig.LogStart("ChangePasswordUser", LogConfig.LogParam("Username", MyUser.GetUsername())));
            var hasil = clsCommon.ChangePasswordUser(Helper.GetLoginUser(this.Page), Pass_TextOldPass.Text, Pass_TextNewPass.Text, Helper.GetLoginUser(this.Page));
            var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(hasil.Result);
            var Status = Response.Property("status").Value.ToString();
            var Message = Response.Property("message").Value.ToString();
            //Log.Debug(LogConfig.LogEnd("ChangePasswordUser", Status, Message));
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", Helper.GetLoginUser(this.Page), "Pass_ButtonSavePass_Click", StartTime, EndTime, "OK", Helper.GetLoginUser(this.Page), "", "", Message));

            if (Status == "Fail")
            {
                p_Add.Attributes.Remove("style");
                p_Add.Attributes.Add("style", "display:block; color:red;");
                p_Add.InnerText = Message;
                ShowToastr(Status + "! " + Message, "Save Failed", "error");
            }
            else
            {
                p_Add.Attributes.Remove("style");
                p_Add.Attributes.Add("style", "display:block; color:green;");
                p_Add.InnerText = "Change Password Success!";

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "$('#modalAfterSave').modal('show');", addScriptTags: true);
                clearFormPass();
            }
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(MyUser.GetHopeOrgID().ToString(), "UserName", Helper.GetLoginUser(this.Page), "Pass_ButtonSavePass_Click", StartTime, ErrorTime, "ERROR", Helper.GetLoginUser(this.Page), "", "", ex.Message));

            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }
        //Log.Info(LogConfig.LogEnd());
    }

    //fungsi untuk clear form input
    void clearFormPass()
    {
        Pass_TextOldPass.Text = "";
        Pass_TextNewPass.Text = "";
        Pass_TextNewPass_confirm.Text = "";
    }

    //fungsi klik button relogin
    protected void ButtonRelogin_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Form/General/Login.aspx", false);
        Context.ApplicationInstance.CompleteRequest();
    }
}