using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SPPediatric;

public partial class Form_SOAP_Control_Template_Specialty_StdPediatric : System.Web.UI.UserControl
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
    }

    public void CheckVisibleDiv()
    {
        string templateid = Request.QueryString["PageSoapId"];
        if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
        {
            if (rbpersalinanlain.Checked)
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowPersalinan", "dvRiwayatPersalinanLain();", true);
            else
                ScriptManager.RegisterStartupScript(Page, GetType(), "HidePersalinan", "HideDvRiwayatPersalinan();", true);

            if (rbpenyulityes.Checked)
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowPenyulit", "ShowDvPenyulitSalin();", true);
            else
                ScriptManager.RegisterStartupScript(Page, GetType(), "HidePenyulit", "HideDvPenyulitSalid();", true);
        }
    }

    public void initializevalue(List<PediatricData> listpediatricdata)
    {
        if (listpediatricdata.Count > 0)
        {

            foreach (PediatricData x in listpediatricdata)
            {
                if (x.pediatric_data_type.ToUpper() == "LAMA KEHAMILAN")
                {
                    txtLamaKehamilan.Text = x.value;
                }

                if (x.pediatric_data_type.ToUpper() == "KOMPLIKASI KEHAMILAN")
                {
                    txtKomplikasiKehamilan.Text = x.value;
                }

                if (x.pediatric_data_type.ToUpper() == "RIWAYAT PERSALINAN")
                {
                    if (x.value == "Spontan")
                    {
                        rbpersalinanspontan.Checked = true;
                    }
                    else if (x.value == "Sectio")
                    {
                        rbpersalinansectio.Checked = true;
                    }
                    else if (x.value == "Vacum")
                    {
                        rbpersalinanvacum.Checked = true;
                    }
                    else if (x.value == "Forceps")
                    {
                        rbpersalinanforceps.Checked = true;
                    }
                    else if (x.value == "Lain")
                    {
                        rbpersalinanlain.Checked = true;
                        txtriwayatpersalinan.Text = x.remarks;
                    }
                }

                if (x.pediatric_data_type.ToUpper() == "PENYULIT PERSALINAN")
                {
                    if (x.value == "Tidak ada")
                    {
                        rbpenyulitno.Checked = true;
                    }
                    else if (x.value == "Ada")
                    {
                        rbpenyulityes.Checked = true;
                        txtpenyulit.Text = x.remarks;
                    }
                }

                if (x.pediatric_data_type.ToUpper() == "BERAT BADAN LAHIR")
                {
                    txtBeratBadanLahir.Text = x.value;
                }

                if (x.pediatric_data_type.ToUpper() == "PANJANG BADAN LAHIR")
                {
                    txtPanjangBadanLahir.Text = x.value;
                }
            }
        }

        CheckVisibleDiv();
    }

    public SOAPPediatric GetPediatricValues(SOAPPediatric fapediatric)
    {
        if (txtLamaKehamilan.Text != "")
        {
            string flagempty = "kosong";
            foreach (PediatricData x in fapediatric.pediatric_data)
            {
                if (x.pediatric_data_type.ToUpper() == "LAMA KEHAMILAN")
                {
                    x.value = txtLamaKehamilan.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PediatricData data = new PediatricData();
                data.pediatric_data_id = Guid.Empty;
                data.pediatric_data_type = "LAMA KEHAMILAN";
                data.value = txtLamaKehamilan.Text;
                data.remarks = "";
                fapediatric.pediatric_data.Add(data);
            }
        }

        if (txtKomplikasiKehamilan.Text != "")
        {
            string flagempty = "kosong";
            foreach (PediatricData x in fapediatric.pediatric_data)
            {
                if (x.pediatric_data_type.ToUpper() == "KOMPLIKASI KEHAMILAN")
                {
                    x.value = txtKomplikasiKehamilan.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PediatricData data = new PediatricData();
                data.pediatric_data_id = Guid.Empty;
                data.pediatric_data_type = "KOMPLIKASI KEHAMILAN";
                data.value = txtKomplikasiKehamilan.Text;
                data.remarks = "";
                fapediatric.pediatric_data.Add(data);
            }
        }

        if (rbpersalinanspontan.Checked == true || rbpersalinansectio.Checked == true || rbpersalinanvacum.Checked == true || rbpersalinanforceps.Checked == true || rbpersalinanlain.Checked == true)
        {
            string val = "";
            string rmks = "";

            if (rbpersalinanspontan.Checked == true)
            {
                val = "Spontan";
            }
            else if (rbpersalinansectio.Checked == true)
            {
                val = "Sectio";
            }
            else if (rbpersalinanvacum.Checked == true)
            {
                val = "Vacum";
            }
            else if (rbpersalinanforceps.Checked == true)
            {
                val = "Forceps";
            }
            else if (rbpersalinanlain.Checked == true)
            {
                val = "Lain";
                rmks = txtriwayatpersalinan.Text;
            }

            string flagempty = "kosong";
            foreach (PediatricData x in fapediatric.pediatric_data)
            {
                if (x.pediatric_data_type.ToUpper() == "RIWAYAT PERSALINAN")
                {
                    x.value = val;
                    x.remarks = rmks;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PediatricData data = new PediatricData();
                data.pediatric_data_id = Guid.Empty;
                data.pediatric_data_type = "RIWAYAT PERSALINAN";
                data.value = val;
                data.remarks = rmks;
                fapediatric.pediatric_data.Add(data);
            }
        }

        if (rbpenyulitno.Checked == true || rbpenyulityes.Checked == true)
        {
            string val = "";
            string rmks = "";

            if (rbpenyulitno.Checked == true)
            {
                val = "Tidak ada";
            }
            else if (rbpenyulityes.Checked == true)
            {
                val = "Ada";
                rmks = txtpenyulit.Text;
            }

            string flagempty = "kosong";
            foreach (PediatricData x in fapediatric.pediatric_data)
            {
                if (x.pediatric_data_type.ToUpper() == "PENYULIT PERSALINAN")
                {
                    x.value = val;
                    x.remarks = rmks;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PediatricData data = new PediatricData();
                data.pediatric_data_id = Guid.Empty;
                data.pediatric_data_type = "PENYULIT PERSALINAN";
                data.value = val;
                data.remarks = rmks;
                fapediatric.pediatric_data.Add(data);
            }
        }

        if (txtBeratBadanLahir.Text != "")
        {
            string flagempty = "kosong";
            foreach (PediatricData x in fapediatric.pediatric_data)
            {
                if (x.pediatric_data_type.ToUpper() == "BERAT BADAN LAHIR")
                {
                    x.value = txtBeratBadanLahir.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PediatricData data = new PediatricData();
                data.pediatric_data_id = Guid.Empty;
                data.pediatric_data_type = "BERAT BADAN LAHIR";
                data.value = txtBeratBadanLahir.Text;
                data.remarks = "";
                fapediatric.pediatric_data.Add(data);
            }
        }

        if (txtPanjangBadanLahir.Text != "")
        {
            string flagempty = "kosong";
            foreach (PediatricData x in fapediatric.pediatric_data)
            {
                if (x.pediatric_data_type.ToUpper() == "PANJANG BADAN LAHIR")
                {
                    x.value = txtPanjangBadanLahir.Text;
                    flagempty = "ada";
                }
            }
            if (flagempty == "kosong")
            {
                PediatricData data = new PediatricData();
                data.pediatric_data_id = Guid.Empty;
                data.pediatric_data_type = "PANJANG BADAN LAHIR";
                data.value = txtPanjangBadanLahir.Text;
                data.remarks = "";
                fapediatric.pediatric_data.Add(data);
            }
        }

        return fapediatric;
    }
    

    public delegate bool customHandler(object sender);
    public event customHandler submitPediatricSender;
    protected void btnsubmitpediatric_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        submitPediatricSender(sender);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalEditPediatric", "$('#modalEditPediatric').modal('hide');", true);

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btnsubmitpediatric_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());
    }
}