using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_SOAP_Control_Template_Specialty_StdTriage : System.Web.UI.UserControl
{
    //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        //log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgID();
        CheckVisibleDiv();
    }

    public void CheckVisibleDiv()
    {
        string templateid = Request.QueryString["PageSoapId"];
        if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
        {
            if (rbPasienDirujuk.Checked)
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowPasienDirujuk", "ShowDvPasienDirujuk();", true);
            else if (rbPasienLain.Checked)
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowPasienLain", "ShowDvPasienLain();", true);
            else
                ScriptManager.RegisterStartupScript(Page, GetType(), "HidePasienDatang", "HideDvPasienDatang();", true);

        }
    }

    public void initializevalue(List<Objective> listobjective)
    {
        if (listobjective.Count > 0)
        {
            foreach (Objective x in listobjective)
            {
                if (x.soap_mapping_id == Guid.Parse("EB3330BD-D413-4B70-999C-0D7AB29FBE36"))
                {   //"TRIAGE SCORE"
                    if (x.value.ToUpper() == "1".ToUpper())
                        rbtriage1.Checked = true;
                    else if (x.value.ToUpper() == "2".ToUpper())
                        rbtriage2.Checked = true;
                    else if (x.value.ToUpper() == "3".ToUpper())
                        rbtriage3.Checked = true;
                }

                else if (x.soap_mapping_id == Guid.Parse("64B06F14-6480-46DA-8846-9CAC5A499748"))
                {   //"TRIAGE TRAUMA"
                    if (x.value.ToUpper() == "Trauma".ToUpper())
                        rbtrauma.Checked = true;
                    else if (x.value.ToUpper() == "Non-trauma".ToUpper())
                        rbnontrauma.Checked = true;
                }

                else if (x.soap_mapping_id == Guid.Parse("D2D42792-B16F-472B-B5CB-428980C5003E"))
                {   //"TRIAGE PASIEN DATANG"
                    if (x.value.ToUpper() == "Sendiri".ToUpper())
                    {
                        rbPasienSendiri.Checked = true;
                    }
                    else if (x.value.ToUpper() == "Dirujuk".ToUpper())
                    {
                        rbPasienDirujuk.Checked = true;
                        txtPasienDirujuk.Text = x.remarks;
                    }
                    else if (x.value.ToUpper() == "Lain-lain".ToUpper())
                    {
                        rbPasienLain.Checked = true;
                        txtPasienLain.Text = x.remarks;
                    }
                }

                else if (x.soap_mapping_id == Guid.Parse("8C1799E0-C793-4918-A850-1B9BE72359CF"))
                {   //"TRIAGE KEADAAN UMUM"
                    if (x.value.ToUpper() == "Baik".ToUpper())
                        rbKeadaanBaik.Checked = true;
                    else if (x.value.ToUpper() == "Sedang".ToUpper())
                        rbKeadaanSedang.Checked = true;
                    else if (x.value.ToUpper() == "Buruk".ToUpper())
                        rbKeadaanBuruk.Checked = true;
                }

                else if (x.soap_mapping_id == Guid.Parse("220F9B0A-4F91-4982-BED3-CA2DDEB1884F"))
                {   //"Airway"
                    txtAirway.Text = x.value;
                }
                else if (x.soap_mapping_id == Guid.Parse("DF853881-EADC-4DA8-9140-960F962535E3"))
                {   //"Breathing"
                    txtBreathing.Text = x.value;
                }
                else if (x.soap_mapping_id == Guid.Parse("576416E4-AEF9-45BC-A7E0-AE3CEDF6EE94"))
                {   //"Circulation"
                    txtCirculation.Text = x.value;
                }
                else if (x.soap_mapping_id == Guid.Parse("058F59BA-7FA9-443A-994A-E848F4FAEE7F"))
                {   //"Disability"
                    txtDisability.Text = x.value;
                }

            }
        }

        CheckVisibleDiv();
    }

    public SOAP GetTriageValues(SOAP SoapFa)
    {
        foreach (var x in SoapFa.objective)
        {
            if (x.soap_mapping_id == Guid.Parse("EB3330BD-D413-4B70-999C-0D7AB29FBE36"))
            {   //"Triage Score"
                if (rbtriage1.Checked == true)
                {
                    x.value = "1";
                }
                else if (rbtriage2.Checked == true)
                {
                    x.value = "2";
                }
                else if (rbtriage3.Checked == true)
                {
                    x.value = "3";
                }
            }

            else if (x.soap_mapping_id == Guid.Parse("64B06F14-6480-46DA-8846-9CAC5A499748"))
            {   //"Trauma"
                if (rbtrauma.Checked == true)
                {
                    x.value = "Trauma";
                }
                else if (rbnontrauma.Checked == true)
                {
                    x.value = "Non-trauma";
                }
            }

            else if (x.soap_mapping_id == Guid.Parse("D2D42792-B16F-472B-B5CB-428980C5003E"))
            {   //"Pasien Datang"
                if (rbPasienSendiri.Checked == true)
                {
                    x.value = "Sendiri";
                    x.remarks = "";
                }
                else if (rbPasienDirujuk.Checked == true)
                {
                    x.value = "Dirujuk";
                    x.remarks = txtPasienDirujuk.Text;
                }
                else if (rbPasienLain.Checked == true)
                {
                    x.value = "Lain-lain";
                    x.remarks = txtPasienLain.Text;
                }
            }

            else if (x.soap_mapping_id == Guid.Parse("8C1799E0-C793-4918-A850-1B9BE72359CF"))
            {   //"Keadaan Umum"
                if (rbKeadaanBaik.Checked == true)
                {
                    x.value = "Baik";
                }
                else if (rbKeadaanSedang.Checked == true)
                {
                    x.value = "Sedang";
                }
                else if (rbKeadaanBuruk.Checked == true)
                {
                    x.value = "Buruk";
                }
            }

            else if (x.soap_mapping_id == Guid.Parse("220F9B0A-4F91-4982-BED3-CA2DDEB1884F"))
            {   //"Airway"
                x.value = txtAirway.Text;
            }
            else if (x.soap_mapping_id == Guid.Parse("DF853881-EADC-4DA8-9140-960F962535E3"))
            {   //"Breathing"
                x.value = txtBreathing.Text;
            }
            else if (x.soap_mapping_id == Guid.Parse("576416E4-AEF9-45BC-A7E0-AE3CEDF6EE94"))
            {   //"Circulation"
                x.value = txtCirculation.Text;
            }
            else if (x.soap_mapping_id == Guid.Parse("058F59BA-7FA9-443A-994A-E848F4FAEE7F"))
            {   //"Disability"
                x.value = txtDisability.Text;
            }

        }

        return SoapFa;
    }
}