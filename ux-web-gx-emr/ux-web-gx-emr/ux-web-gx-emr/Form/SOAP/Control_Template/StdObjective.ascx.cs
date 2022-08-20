using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Form_SOAP_Control_Template_StdObjective : System.Web.UI.UserControl
{
    public List<Objective> listobjective = new List<Objective>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
        CheckVisibleDiv();
    }

    public void CheckVisibleDiv()
    {
        string valuePain = txtPainScale.Text;
        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "getProgressBar", "getProgressBar(" + valuePain + ");", true);

        if (rbhead3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv1", "ShowDiv('dvHead');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv1", "hide('dvHead');", true);

        if (rbeyes3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv2", "ShowDiv('dvEyes');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv2", "hide('dvEyes');", true);

        if (rbent3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv3", "ShowDiv('dvEnt');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv3", "hide('dvEnt');", true);

        if (rbMouth3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv4", "ShowDiv('dvMouth');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv4", "hide('dvMouth');", true);

        if (rbTeeth3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv5", "ShowDiv('dvTeeth');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv5", "hide('dvTeeth');", true);

        if (rbNeck3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv6", "ShowDiv('dvNeck');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv6", "hide('dvNeck');", true);

        if (rbChest3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv7", "ShowDiv('dvChest');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv7", "hide('dvChest');", true);

        if (rbheart3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv8", "ShowDiv('dvHeart');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv8", "hide('dvHeart');", true);

        if (rbLung3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv9", "ShowDiv('dvLung');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv9", "hide('dvLung');", true);

        if (rbBack3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv10", "ShowDiv('dvBack');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv10", "hide('dvBack');", true);

        if (rbAbdomen3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv11", "ShowDiv('dvAbdomen');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv11", "hide('dvAbdomen');", true);

        if (rbLimbs3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv12", "ShowDiv('dvLimbs');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv12", "hide('dvLimbs');", true);

        if (rbAnogenital3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv13", "ShowDiv('dvAnogenital');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv13", "hide('dvAnogenital');", true);

        if (rbSkin3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv14", "ShowDiv('dvSkin');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv14", "hide('dvSkin');", true);

        if (rbOthers3.Checked)
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "ShowDiv15", "ShowDiv('dvOthers');", true);
        else
            ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv15", "hide('dvOthers');", true);

        checkAccordionObjective();
    }

    public void checkAccordionObjective()
    {
        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "accordionobj", "checkAccordionobj();", true);
    }

    public void initializevalue(List<Objective> listobjective)
    {
        //listsubjective = Jsongetsoap.list.subjective;
        if (listobjective.Count > 0)
        {
            foreach (Objective x in listobjective)
            {
                if (x.soap_mapping_id == Guid.Parse("AE3CA8C2-EAB0-41B6-9E3E-3ECB8071A9D0"))
                {//"BLOOD PRESSURE LOW"
                    txtbloodlow.Text = x.value;
                }
                else if (x.soap_mapping_id == Guid.Parse("E5EFD220-B68E-4652-AD03-D56EF29FCEBB"))
                {//"BLOOD PRESSURE HIGH"
                    txtbloodhigh.Text = x.value;
                }
                else if (x.soap_mapping_id == Guid.Parse("78CBB61F-4A11-470A-B770-1A44EB04357F"))
                {//"PULSE RATE"
                    txtpulserate.Text = x.value;
                }
                else if (x.soap_mapping_id == Guid.Parse("2EECA752-A2EA-4426-B3CF-C1EA3833BF30"))
                {//"TEMPERATURE"
                    txttemperature.Text = x.value;
                }
                else if (x.soap_mapping_id == Guid.Parse("52CE9350-BFB2-4072-8893-D0C6CF8B3634"))
                {//"WEIGHT"
                    txtweight.Text = x.value;
                }
                else if (x.soap_mapping_id == Guid.Parse("2A8DBDDB-EDFE-4704-876E-5A2D735BB541"))
                {//"HEIGHT"
                    txtheight.Text = x.value;
                }
                else if (x.soap_mapping_id == Guid.Parse("E6AE2EA9-B321-4756-BF96-2DC232E4A7BA"))
                {//"RESPIRATORY RATE"
                    txtrespiratory.Text = x.value;
                }
                else if (x.soap_mapping_id == Guid.Parse("E903246C-DF95-4FE0-96D2-CF90C036D3D7"))
                {//"SpO2"
                    txtspo.Text = x.value;
                }
                else if (x.soap_mapping_id == Guid.Parse("A9C5CD3C-1E02-4DB2-A047-2F1983D1315B"))
                {//"GCS Eye"
                    if (x.value == "1")
                    {
                        eye4.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "2")
                    {
                        eye3.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "3")
                    {
                        eye2.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "4")
                    {
                        eye1.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("89B583A5-3003-43AB-9693-60EA6181C8D5"))
                {//"GCS Move"
                    if (x.value == "1")
                    {
                        move6.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "2")
                    {
                        move5.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "3")
                    {
                        move4.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "4")
                    {
                        move3.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "5")
                    {
                        move2.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "6")
                    {
                        move1.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("FE4CF48C-17A6-4720-AD23-186517DD9C85"))
                {//"GCS Verbal"
                    if (x.value == "1")
                    {
                        verbal5.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "2")
                    {
                        verbal4.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "3")
                    {
                        verbal3.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "4")
                    {
                        verbal2.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "5")
                    {
                        verbal1.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "T")
                    {
                        verbal6.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                    else if (x.value == "A")
                    {
                        verbal7.Checked = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "SumEMV();", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("3AAE8DC2-484F-4F3C-A01B-1B0C3F107176"))
                {//"Pain Scale"
                    txtPainScale.Text = x.value;
                    ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "getProgressBar", "getProgressBar(" + txtPainScale.Text + ");", true);
                }
                else if (x.soap_mapping_id == Guid.Parse("73CC7D5A-E5A8-4C5D-938D-0F1209D316C2"))
                {//"Mental Status"
                    if (x.value.ToUpper() == "Good Orientation".ToUpper())
                        mental1.Checked = true;
                    else if (x.value.ToUpper() == "Disorientated".ToUpper())
                        mental2.Checked = true;
                    else if (x.value.ToUpper() == "Cooperative".ToUpper())
                        mental3.Checked = true;
                    else if (x.value.ToUpper() == "Non Cooperative".ToUpper())
                        mental4.Checked = true;
                }
                else if (x.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909"))
                {//"CONSCIOUSNESS LEVEL"
                    if (x.value.ToUpper() == "Compos mentis".ToUpper())
                        consciousness1.Checked = true;
                    else if (x.value.ToUpper() == "Somnolent".ToUpper())
                        consciousness2.Checked = true;
                    else if (x.value.ToUpper() == "Stupor".ToUpper())
                        consciousness3.Checked = true;
                    else if (x.value.ToUpper() == "Coma".ToUpper())
                        consciousness4.Checked = true;
                }
                else if (x.soap_mapping_id == Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c"))
                {//"FALL RISK"
                    if (x.value.ToUpper() == "undergo sedation".ToUpper())
                        fall1.Checked = true;
                    else if (x.value.ToUpper() == "physical limitation".ToUpper())
                        fall2.Checked = true;
                    else if (x.value.ToUpper() == "motion aids".ToUpper())
                        fall3.Checked = true;
                    else if (x.value.ToUpper() == "Disorder".ToUpper())
                        fall4.Checked = true;
                    else if (x.value.ToUpper() == "fasting".ToUpper())
                        fall5.Checked = true;
                }
                else if (x.soap_mapping_id == Guid.Parse("44ac7b50-c6ce-47e6-93a9-bcf9285da275"))
                {//"Head"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbhead1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv1", "hide('dvHead');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbhead2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv1", "hide('dvHead');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbhead3.Checked = true;
                        txthead.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv1", "ShowDiv('dvHead');", true);
                    }
                    else
                    {
                        rbhead1.Checked = true;
                        rbhead2.Checked = false;
                        rbhead3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv1", "hide('dvHead');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("9fb63e44-ff80-4d45-9ec6-d2c6242afa36"))
                {//"Eyes"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbeyes1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv2", "hide('dvEyes');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbeyes2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv2", "hide('dvEyes');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbeyes3.Checked = true;
                        txteyes.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv2", "ShowDiv('dvEyes');", true);
                    }
                    else
                    {
                        rbeyes1.Checked = true;
                        rbeyes2.Checked = false;
                        rbeyes3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv2", "hide('dvEyes');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("e1da1a48-8158-4761-b34a-9f09a2586cea"))
                {//"Eyes"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbent1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv15", "hide('dvEnt');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbent2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv15", "hide('dvEnt');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbent3.Checked = true;
                        txtEnt.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv15", "ShowDiv('dvEnt');", true);
                    }
                    else
                    {
                        rbent1.Checked = true;
                        rbent2.Checked = false;
                        rbent3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv15", "hide('dvEnt');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("ef374b08-da9d-4a26-9693-5f011f2f5cf9"))
                {//"Mouth"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbMouth1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv3", "hide('dvMouth');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbMouth2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv3", "hide('dvMouth');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbMouth3.Checked = true;
                        txtMouth.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv3", "ShowDiv('dvMouth');", true);
                    }
                    else
                    {
                        rbMouth1.Checked = true;
                        rbMouth2.Checked = false;
                        rbMouth3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv3", "hide('dvMouth');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("5153f3c8-eef3-4cc9-833f-4f8edabcfcfe"))
                {//"Teeth"
                    if (x.value.ToUpper() == "Not Checked".ToUpper())
                    {
                        rbTeeth1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv4", "hide('dvTeeth');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbTeeth2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv4", "hide('dvTeeth');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbTeeth3.Checked = true;
                        txtTeeth.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv4", "ShowDiv('dvTeeth');", true);
                    }
                    else
                    {
                        rbTeeth1.Checked = true;
                        rbTeeth2.Checked = false;
                        rbTeeth3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv4", "hide('dvTeeth');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("60e75ace-c7db-458f-a6bc-14db1759d67e"))
                {//"Neck"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbNeck1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv5", "hide('dvNeck');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbNeck2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv5", "hide('dvNeck');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbNeck3.Checked = true;
                        txtNeck.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv5", "ShowDiv('dvNeck');", true);
                    }
                    else
                    {
                        rbNeck1.Checked = true;
                        rbNeck2.Checked = false;
                        rbNeck3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv5", "hide('dvNeck');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("b6d97ee2-13d9-4f19-9885-27e26ffd3a0b"))
                {//"Chest"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbChest1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv6", "hide('dvChest');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbChest2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv6", "hide('dvChest');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbChest3.Checked = true;
                        txtChest.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv6", "ShowDiv('dvChest');", true);
                    }
                    else
                    {
                        rbChest1.Checked = true;
                        rbChest2.Checked = false;
                        rbChest3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv6", "hide('dvChest');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("a33420b1-cff7-4ad5-bcb4-29d801ac2098"))
                {//"Heart"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbheart1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv7", "hide('dvHeart');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbheart2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv7", "hide('dvHeart');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbheart3.Checked = true;
                        txtHeart.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv7", "ShowDiv('dvHeart');", true);
                    }
                    else
                    {
                        rbheart1.Checked = true;
                        rbheart2.Checked = false;
                        rbheart3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv7", "hide('dvHeart');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("89282a8a-4c7c-4960-ab0a-e8413c4c9fa5"))
                {//"Lung"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbLung1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv8", "hide('dvLung');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbLung2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv8", "hide('dvLung');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbLung3.Checked = true;
                        txtLung.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv8", "ShowDiv('dvLung');", true);
                    }
                    else
                    {
                        rbLung1.Checked = true;
                        rbLung2.Checked = false;
                        rbLung3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv8", "hide('dvLung');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("98dff381-4fc7-4a14-876b-014f6d109458"))
                {//"Back"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbBack1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv9", "hide('dvBack');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbBack2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv9", "hide('dvBack');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbBack3.Checked = true;
                        txtBack.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv9", "ShowDiv('dvBack');", true);
                    }
                    else
                    {
                        rbBack1.Checked = true;
                        rbBack2.Checked = false;
                        rbBack3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv9", "hide('dvBack');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("80bbd8ef-3e38-47c1-97bd-fd7998eac1c3"))
                {//"Abdomen"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbAbdomen1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv10", "hide('dvAbdomen');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbAbdomen2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv10", "hide('dvAbdomen');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbAbdomen3.Checked = true;
                        txtAbdomen.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv10", "ShowDiv('dvAbdomen');", true);
                    }
                    else
                    {
                        rbAbdomen1.Checked = true;
                        rbAbdomen2.Checked = false;
                        rbAbdomen3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv10", "hide('dvAbdomen');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("acd1b3f6-8e28-4b69-bbbd-17d2669b9adc"))
                {//"limbs"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbLimbs1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv11", "hide('dvLimbs');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbLimbs2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv11", "hide('dvLimbs');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbLimbs3.Checked = true;
                        txtLimbs.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv11", "ShowDiv('dvLimbs');", true);
                    }
                    else
                    {
                        rbLimbs1.Checked = true;
                        rbLimbs2.Checked = false;
                        rbLimbs3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv11", "hide('dvLimbs');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("3450FD71-3794-4BAE-81E5-11A04FCA06FD"))
                {//"Anogenital"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbAnogenital1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv12", "hide('dvAnogenital');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbAnogenital2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv12", "hide('dvAnogenital');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbAnogenital3.Checked = true;
                        txtAnogenital.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv12", "ShowDiv('dvAnogenital');", true);
                    }
                    else
                    {
                        rbAnogenital1.Checked = true;
                        rbAnogenital2.Checked = false;
                        rbAnogenital3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv12", "hide('dvAnogenital');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("74091244-a274-4095-88a9-987c19b468f9"))
                {//"Skin"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbSkin1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv13", "hide('dvSkin');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbSkin2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv13", "hide('dvSkin');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbSkin3.Checked = true;
                        txtSkin.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv13", "ShowDiv('dvSkin');", true);
                    }
                    else
                    {
                        rbSkin1.Checked = true;
                        rbSkin2.Checked = false;
                        rbSkin3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv13", "hide('dvSkin');", true);
                    }
                }
                else if (x.soap_mapping_id == Guid.Parse("7218971c-e89f-4172-ae3c-b7fb855c1d6d"))
                {//"Others"
                    if (x.value.ToUpper() == "Not checked".ToUpper())
                    {
                        rbOthers1.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv14", "hide('dvOthers');", true);
                    }
                    else if (x.value.ToUpper() == "Normal".ToUpper())
                    {
                        rbOthers2.Checked = true;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv14", "hide('dvOthers');", true);
                    }
                    else if (x.value.ToUpper() == "Abnormal".ToUpper())
                    {
                        rbOthers3.Checked = true;
                        txtOthers.Text = x.remarks;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "showDiv14", "ShowDiv('dvOthers');", true);
                    }
                    else
                    {
                        rbOthers1.Checked = true;
                        rbOthers2.Checked = false;
                        rbOthers3.Checked = false;
                        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "HideDiv14", "hide('dvOthers');", true);
                    }
                }

            }
        }
        CheckVisibleDiv();
    }

    public SOAP GetObjectiveValues(SOAP SOA)
    {
        SOA.objective.RemoveAll(x => x.soap_mapping_id == Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c"));
        if (fall1.Checked)
        {
            Objective tempfallrisk = new Objective();
            tempfallrisk.objective_id = Guid.Empty;
            tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
            tempfallrisk.soap_mapping_name = "FALL RISK";
            tempfallrisk.value = "undergo sedation";
            tempfallrisk.remarks = "";
            SOA.objective.Add(tempfallrisk);
        }
        if (fall2.Checked)
        {
            Objective tempfallrisk = new Objective();
            tempfallrisk.objective_id = Guid.Empty;
            tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
            tempfallrisk.soap_mapping_name = "FALL RISK";
            tempfallrisk.value = "physical limitation";
            tempfallrisk.remarks = "";
            SOA.objective.Add(tempfallrisk);
        }
        if (fall3.Checked)
        {
            Objective tempfallrisk = new Objective();
            tempfallrisk.objective_id = Guid.Empty;
            tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
            tempfallrisk.soap_mapping_name = "FALL RISK";
            tempfallrisk.value = "motion aids";
            tempfallrisk.remarks = "";
            SOA.objective.Add(tempfallrisk);
        }
        if (fall4.Checked)
        {
            Objective tempfallrisk = new Objective();
            tempfallrisk.objective_id = Guid.Empty;
            tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
            tempfallrisk.soap_mapping_name = "FALL RISK";
            tempfallrisk.value = "Disorder";
            tempfallrisk.remarks = "";
            SOA.objective.Add(tempfallrisk);
        }
        if (fall5.Checked)
        {
            Objective tempfallrisk = new Objective();
            tempfallrisk.objective_id = Guid.Empty;
            tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
            tempfallrisk.soap_mapping_name = "FALL RISK";
            tempfallrisk.value = "fasting";
            tempfallrisk.remarks = "";
            SOA.objective.Add(tempfallrisk);
        }

        foreach (var objective in SOA.objective)
        {
            if (objective.soap_mapping_id == Guid.Parse("a9c5cd3c-1e02-4db2-a047-2f1983d1315b"))
            {//GCS Eye
                if (lbleyetotal.Text == "_")
                    objective.value = "";
                else
                    objective.value = lbleyetotal.Text;
            }
            if (objective.soap_mapping_id == Guid.Parse("89b583a5-3003-43ab-9693-60ea6181c8d5"))
            {//GCS Move
                if (lblmovetotal.Text == "_")
                    objective.value = "";
                else
                    objective.value = lblmovetotal.Text;
            }
            if (objective.soap_mapping_id == Guid.Parse("fe4cf48c-17a6-4720-ad23-186517dd9c85"))
            {//GCS Verbal
                if (lblverbaltotal.Text == "_")
                    objective.value = "";
                else
                    objective.value = lblverbaltotal.Text;
            }
            if (objective.soap_mapping_id == Guid.Parse("ae3ca8c2-eab0-41b6-9e3e-3ecb8071a9d0"))
            {//blood low
                objective.value = txtbloodlow.Text;
            }
            if (objective.soap_mapping_id == Guid.Parse("e5efd220-b68e-4652-ad03-d56ef29fcebb"))
            {//blood hight
                objective.value = txtbloodhigh.Text;
            }
            if (objective.soap_mapping_id == Guid.Parse("78cbb61f-4a11-470a-b770-1a44eb04357f"))
            {//pulse rate
                objective.value = txtpulserate.Text;
            }
            if (objective.soap_mapping_id == Guid.Parse("e6ae2ea9-b321-4756-bf96-2dc232e4a7ba"))
            {//respiratory
                objective.value = txtrespiratory.Text;
            }
            if (objective.soap_mapping_id == Guid.Parse("e903246c-df95-4fe0-96d2-cf90c036d3d7"))
            {//SpO2
                objective.value = txtspo.Text;
            }
            if (objective.soap_mapping_id == Guid.Parse("2eeca752-a2ea-4426-b3cf-c1ea3833bf30"))
            {//temperature
                objective.value = txttemperature.Text;
            }
            if (objective.soap_mapping_id == Guid.Parse("52ce9350-bfb2-4072-8893-d0c6cf8b3634"))
            {//weight
                objective.value = txtweight.Text;
            }
            if (objective.soap_mapping_id == Guid.Parse("2a8dbddb-edfe-4704-876e-5a2d735bb541"))
            {//height
                objective.value = txtheight.Text;
            }
            if (objective.soap_mapping_id == Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176"))
            {//pain scale
                objective.value = txtPainScale.Text;
            }
            if (objective.soap_mapping_id == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2"))
            {//Mental Status
                if (mental1.Checked)
                    objective.value = "Good Orientation";
                else if (mental2.Checked)
                    objective.value = "Disorientated";
                else if (mental3.Checked)
                    objective.value = "Cooperative";
                else if (mental4.Checked)
                    objective.value = "Non Cooperative";
            }
            if (objective.soap_mapping_id == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909"))
            {//Consciousness Level
                if (consciousness1.Checked)
                    objective.value = "Compos mentis";
                else if (consciousness2.Checked)
                    objective.value = "Somnolent";
                else if (consciousness3.Checked)
                    objective.value = "Stupor";
                else if (consciousness4.Checked)
                    objective.value = "Coma";
            }
            if (objective.soap_mapping_id == Guid.Parse("44AC7B50-C6CE-47E6-93A9-BCF9285DA275"))
            {//head
                if (rbhead1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbhead2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbhead3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txthead.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("9FB63E44-FF80-4D45-9EC6-D2C6242AFA36"))
            {//eyes
                if (rbeyes1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbeyes2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbeyes3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txteyes.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("E1DA1A48-8158-4761-B34A-9F09A2586CEA"))
            {//ent
                if (rbent1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbent2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbent3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txtEnt.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("EF374B08-DA9D-4A26-9693-5F011F2F5CF9"))
            {//mouth
                if (rbMouth1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbMouth2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbMouth3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txtMouth.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("5153F3C8-EEF3-4CC9-833F-4F8EDABCFCFE"))
            {//teeth
                if (rbTeeth1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbTeeth2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbTeeth3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txtTeeth.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("60E75ACE-C7DB-458F-A6BC-14DB1759D67E"))
            {//neck
                if (rbNeck1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbNeck2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbNeck3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txtNeck.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("B6D97EE2-13D9-4F19-9885-27E26FFD3A0B"))
            {//chest
                if (rbChest1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbChest2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbChest3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txtChest.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("A33420B1-CFF7-4AD5-BCB4-29D801AC2098"))
            {//heart
                if (rbheart1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbheart2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbheart3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txtHeart.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("89282A8A-4C7C-4960-AB0A-E8413C4C9FA5"))
            {//lung
                if (rbLung1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbLung2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbLung3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txtLung.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("98DFF381-4FC7-4A14-876B-014F6D109458"))
            {//back
                if (rbBack1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbBack2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbBack3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txtBack.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("80BBD8EF-3E38-47C1-97BD-FD7998EAC1C3"))
            {//abdomen
                if (rbAbdomen1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbAbdomen2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbAbdomen3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txtAbdomen.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("ACD1B3F6-8E28-4B69-BBBD-17D2669B9ADC"))
            {//limbs
                if (rbLimbs1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbLimbs2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbLimbs3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txtLimbs.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("3450FD71-3794-4BAE-81E5-11A04FCA06FD"))
            {//Anogenital
                if (rbAnogenital1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbAnogenital2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbAnogenital3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txtAnogenital.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("74091244-A274-4095-88A9-987C19B468F9"))
            {//Skin
                if (rbSkin1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbSkin2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbSkin3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txtSkin.Text;
                }
            }
            if (objective.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D"))
            {//Others
                if (rbOthers1.Checked)
                {
                    objective.value = "Not Checked";
                    objective.remarks = "";
                }
                else if (rbOthers2.Checked)
                {
                    objective.value = "Normal";
                    objective.remarks = "";
                }
                else if (rbOthers3.Checked)
                {
                    objective.value = "Abnormal";
                    objective.remarks = txtOthers.Text;
                }
            }
        }
        return SOA;
    }

}