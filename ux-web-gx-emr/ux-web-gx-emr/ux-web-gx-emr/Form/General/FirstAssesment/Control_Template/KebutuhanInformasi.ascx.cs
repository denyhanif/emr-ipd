using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static FirstAssesment;

public partial class Form_General_FirstAssesment_Control_Template_KebutuhanInformasi : System.Web.UI.UserControl
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
        if (rbPenerjemah2.Checked)
            ScriptManager.RegisterStartupScript(upKebutuhanInfo, upKebutuhanInfo.GetType(), "ShowHidedvPenerjemah", "ShowHidedvPenerjemah();", true);
        else
            ScriptManager.RegisterStartupScript(upKebutuhanInfo, upKebutuhanInfo.GetType(), "HidePdvPenerjemah", "HidePdvPenerjemah();", true);
    }

    public void initializevalue(List<OthersFA> listother)
    {
        //Log.Info(LogConfig.LogStart());

        if (listother.Count > 0)
        {
            foreach (OthersFA x in listother)
            {
                if (x.others_mapping_id == Guid.Parse("9a0b6591-b01b-4b6d-92c4-133137d23832"))
                {//masalah belajar
                    if (x.remarks != "")
                        txtProsesBelajar.Text = x.remarks;
                }

                if (x.others_mapping_id == Guid.Parse("35a5f484-8ac2-408f-9ab0-35654bb2d5c9"))
                {//penerjemah
                    if (x.remarks == "")
                    {
                        rbPenerjemah1.Checked = true;
                    }
                    else
                    {
                        rbPenerjemah2.Checked = true;
                        txtPenerjemah.Text = x.remarks;
                    }
                }

                if (x.others_mapping_id == Guid.Parse("e334eebf-c59e-434d-af5f-81ad3b818a90"))
                {//KESEDIAAN MENERIMA INFORMASI
                    if (x.remarks != "")
                        txtEdukasi.Text = x.remarks;
                }

                if (x.others_mapping_id == Guid.Parse("42d1e20e-29dd-456c-ae7a-940cb2892906"))
                {//Bahasa
                    if (x.remarks != "")
                        txtBahasa.Text = x.remarks;
                }

                if (x.others_mapping_id == Guid.Parse("fbb3f5dd-092a-4b08-9e1d-ae57653163f2"))
                {//INFO EDUKASI KESEHATAN
                    if (x.remarks != "")
                        txtInformasi.Text = x.remarks;
                }

                if (x.others_mapping_id == Guid.Parse("39009ae8-57b7-4cfc-9ceb-b45938789fae"))
                {//METODE BELAJAR
                    if (x.remarks != "")
                        txtMetodeBelajar.Text = x.remarks;
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
            if (x.others_mapping_id == Guid.Parse("9a0b6591-b01b-4b6d-92c4-133137d23832"))
            {//masalah belajar
                    x.remarks = txtProsesBelajar.Text;
            }

            if (x.others_mapping_id == Guid.Parse("35a5f484-8ac2-408f-9ab0-35654bb2d5c9"))
            {//penerjemah
                if (rbPenerjemah1.Checked)
                {
                    x.value = "";
                    x.remarks = "";
                }
                else if(rbPenerjemah2.Checked)
                {
                    x.remarks = txtPenerjemah.Text;
                }
            }

            if (x.others_mapping_id == Guid.Parse("e334eebf-c59e-434d-af5f-81ad3b818a90"))
            {//KESEDIAAN MENERIMA INFORMASI
                x.remarks = txtEdukasi.Text;
            }

            if (x.others_mapping_id == Guid.Parse("42d1e20e-29dd-456c-ae7a-940cb2892906"))
            {//Bahasa
                x.remarks = txtBahasa.Text;
            }

            if (x.others_mapping_id == Guid.Parse("fbb3f5dd-092a-4b08-9e1d-ae57653163f2"))
            {//INFO EDUKASI KESEHATAN
                x.remarks = txtInformasi.Text;
            }

            if (x.others_mapping_id == Guid.Parse("39009ae8-57b7-4cfc-9ceb-b45938789fae"))
            {//METODE BELAJAR
                x.remarks = txtMetodeBelajar.Text;
            }
        }

        //Log.Info(LogConfig.LogEnd());

        return fa;
    }
}