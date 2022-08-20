using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_SOAP_PreviewTemplate_EmailSoap : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        if (!IsPostBack)
        {
            if (Request.QueryString["idPatient"] != null && Request.QueryString["AdmissionId"] != null && Request.QueryString["EncounterId"] != null)
            {
                SoapPagePreview.initializevalue(Helper.organizationId, long.Parse(Request.QueryString["idPatient"]), long.Parse(Request.QueryString["AdmissionId"]), Guid.Parse(Request.QueryString["EncounterId"]));

                try
                {
                    Email model = new Email();

                    model.email_type_name = "MEDICAL-RESUME";
                    model.email_sender = "noreply@siloamhospitals.com";
                    model.display_name = "SILOAM HOSPITALS GROUP";
                    model.email_to = "andy.syahputera@siloamhospitals.com";
                    model.email_cc = "";
                    model.email_bcc = "gheko.makmur@siloamhospitals.com";
                    model.subject = "Medical Resume :" + ((Label)SoapPagePreview.FindControl("PatientData")).Text.ToString() + " On " + ((Label)SoapPagePreview.FindControl("AdmissionDate")).Text.ToString();
                    model.body = "";
                    model.total_attachment = 0;
                    model.attachment_name = "";
                    model.attachment_file = null;
                    model.schedule_date = DateTime.Now;
                    model.created_by = Helper.GetLoginUser(this);

                    var sendEmail = clsCommon.SendEmail(model);

                    string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
                    base.Response.Write(close);

                    Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"].ToString(), "Page_Load", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
                }
                catch (Exception ex)
                {
                    Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"].ToString(), "Page_Load", StartTime, "ERROR", MyUser.GetUsername(), "", "", ex.Message));
                    throw ex;
                }
            }
            else
            {
                string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
                base.Response.Write(close);
            }
        }
    }

 
}