using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_ErrorPage_ServerError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[CstSession.sessionerror] != null)
        {
            Exception exer = (Exception)Session[CstSession.sessionerror];
            error_box(exer);
        }
    }

    public void error_box(Exception ex)
    {

        var frame = new StackTrace(ex, true).GetFrame(0);
        var fn_arr = frame.GetFileName().Split('\\');
        var fn = fn_arr[fn_arr.Count()-1].ToString();

        //LabelErrorJudul.Text = "Error! " + judul;
        LabelErrorUser.Text = MyUser.GetUsername();
        LabelErrorTime.Text = DateTime.Now.ToString();
        LabelErrorEx.Text = ex.GetType().ToString();
        LabelErrorExDet.Text = ex.Message.ToString();
        LabelErrorExSF.Text = fn;
        LabelErrorExSF.ToolTip = ex.StackTrace.ToString();
        LabelErrorExMethod.Text = ex.TargetSite.ToString();
        LabelErrorExLine.Text = frame.GetFileLineNumber().ToString();

        Session[CstSession.sessionerror] = null;
    }
}