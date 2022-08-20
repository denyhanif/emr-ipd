using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_SOAP_PreviewTemplate_PrintSoap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["idPatient"] != null && Request.QueryString["AdmissionId"] != null && Request.QueryString["EncounterId"] != null)
            {
                SoapPagePreview.initializevalue(Helper.organizationId, long.Parse(Request.QueryString["idPatient"]), long.Parse(Request.QueryString["AdmissionId"]), Guid.Parse(Request.QueryString["EncounterId"]));
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "printpreview", "printpreview();", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "printpreview", "printpreview()", true);

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "printpreview", "printpreview();", true);
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