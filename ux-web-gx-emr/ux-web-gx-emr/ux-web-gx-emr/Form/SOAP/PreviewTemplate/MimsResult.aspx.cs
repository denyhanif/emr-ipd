using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_SOAP_PreviewTemplate_MimsResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Helper.SessionMimsResultData] != null)
        {
            MimsInteractionWithLog datamims = (MimsInteractionWithLog)Session[Helper.SessionMimsResultData];
            if (datamims == null)
            {
                lblMimsRefresh.Visible = false;
                div_mims_notfound.Visible = true;
            }
            else
            {
                lblMimsRefresh.Visible = true;
                div_mims_notfound.Visible = false;
                lblMimsHtmlResult.Text = datamims.htmlResult;
                if (datamims.disclaimer != "")
                {
                    div_disclaimer.Visible = true;
                    lblMimsDisclaimer.Text = "<b>Disclaimer :</b> <br />" + datamims.disclaimer;
                }
            }
        }
    }

    protected void ButtonRefreshMims_Click(object sender, EventArgs e)
    {
        //if (Session[Helper.SessionMimsResultData] != null)
        //{
        //    lblMimsHtmlResult.Text = (string)Session[Helper.SessionMimsResultData];
        //}
    }
}