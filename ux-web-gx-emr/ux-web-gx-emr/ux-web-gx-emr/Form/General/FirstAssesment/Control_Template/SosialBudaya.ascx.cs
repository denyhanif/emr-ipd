using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static FirstAssesment;

public partial class Form_General_FirstAssesment_Control_Template_SosialBudaya : System.Web.UI.UserControl
{
    //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        //log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgID();

        if (!IsPostBack)
        {

        }
        CheckVisibleDiv();
    }

    public void CheckVisibleDiv()
    {
        if (rbemosi4.Checked)
            ScriptManager.RegisterStartupScript(upEmosi, upEmosi.GetType(), "ShowHideinfo", "ShowHideinfo();", true);
        else
            ScriptManager.RegisterStartupScript(upEmosi, upEmosi.GetType(), "Hideinfo", "Hideinfo();", true);
    }

    public void initializevalue(List<OthersFA> listother)
    {
        //Log.Info(LogConfig.LogStart());

        if (listother.Count > 0)
        {
            foreach (OthersFA x in listother)
            {
                if (x.others_mapping_id == Guid.Parse("dab1353c-c47e-4299-b735-740eb04c29ba"))
                {//respon emosi
                    if (x.value == "Tenang")
                        rbemosi1.Checked = true;
                    else if (x.value == "Marah")
                        rbemosi2.Checked = true;
                    else if (x.value == "Sedih")
                        rbemosi3.Checked = true;
                    else if(x.value == "Lain-lain")
                    {
                        rbemosi4.Checked = true;
                        txtinfo.Text = x.remarks;
                    }
                }

                if (x.others_mapping_id == Guid.Parse("27718369-1763-4a9c-b079-e308573cbb28"))
                {
                    if(x.remarks != "")
                        txtNilaiSosial.Text = x.remarks;
                }
            }
        }
        CheckVisibleDiv();

        //Log.Info(LogConfig.LogEnd());
    }

    public FirstAnalysis getvalues(FirstAnalysis fa)
    {
        //Log.Info(LogConfig.LogStart());

        foreach (OthersFA x in fa.others_fa)
        {
            if (x.others_mapping_id == Guid.Parse("dab1353c-c47e-4299-b735-740eb04c29ba"))
            {//respon emosi
                if (rbemosi1.Checked)
                    x.value = "Tenang";
                else if (rbemosi2.Checked)
                    x.value = "Marah";
                else if (rbemosi3.Checked)
                    x.value = "Sedih";
                else if (rbemosi4.Checked)
                {
                    x.remarks = txtinfo.Text;
                    x.value = "Lain-lain";
                }
            }

            if (x.others_mapping_id == Guid.Parse("27718369-1763-4a9c-b079-e308573cbb28"))
            {
                x.remarks = txtNilaiSosial.Text;
            }
        }

        //Log.Info(LogConfig.LogEnd());

        return fa;
    }
}