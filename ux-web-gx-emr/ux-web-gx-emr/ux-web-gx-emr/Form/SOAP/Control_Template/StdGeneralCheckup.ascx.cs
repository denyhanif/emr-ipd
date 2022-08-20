using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_SOAP_Control_Template_StdGeneralCheckup : System.Web.UI.UserControl
{
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
                if (x.soap_mapping_id == Guid.Parse("A9C5CD3C-1E02-4DB2-A047-2F1983D1315B"))
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
        }
        return SOA;
    }
}