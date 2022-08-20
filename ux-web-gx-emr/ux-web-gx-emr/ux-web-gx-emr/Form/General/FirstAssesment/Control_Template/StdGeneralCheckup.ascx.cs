using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static FirstAssesment;

public partial class Form_General_FirstAssesment_Control_Template_StdGeneralCheckup : System.Web.UI.UserControl
{
    //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        //log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgID();

        if (!IsPostBack)
        {

        }
        string valuePain = txtPainScale.Text;
        ScriptManager.RegisterStartupScript(upObjective, upObjective.GetType(), "getProgressBar", "getProgressBar(" + valuePain + ");", true);

    }
    public void initializevalue(List<ObjectiveFA> listobjective)
    {
        //Log.Info(LogConfig.LogStart());

        //listsubjective = Jsongetsoap.list.subjective;
        if (listobjective.Count > 0)
        {
            foreach (ObjectiveFA x in listobjective)
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

        //Log.Info(LogConfig.LogEnd());
    }

    public FirstAnalysis getvalues(FirstAnalysis fa)
    {
        //Log.Info(LogConfig.LogStart());

        fa.objective_fa.RemoveAll(x => x.soap_mapping_id == Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c"));
        if (fall1.Checked)
        {
            ObjectiveFA tempfallrisk = new ObjectiveFA();
            tempfallrisk.objective_id = Guid.Empty;
            tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
            tempfallrisk.soap_mapping_name = "FALL RISK";
            tempfallrisk.value = "undergo sedation";
            tempfallrisk.remarks = "";
            fa.objective_fa.Add(tempfallrisk);
        }
        if (fall2.Checked)
        {
            ObjectiveFA tempfallrisk = new ObjectiveFA();
            tempfallrisk.objective_id = Guid.Empty;
            tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
            tempfallrisk.soap_mapping_name = "FALL RISK";
            tempfallrisk.value = "physical limitation";
            tempfallrisk.remarks = "";
            fa.objective_fa.Add(tempfallrisk);
        }
        if (fall3.Checked)
        {
            ObjectiveFA tempfallrisk = new ObjectiveFA();
            tempfallrisk.objective_id = Guid.Empty;
            tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
            tempfallrisk.soap_mapping_name = "FALL RISK";
            tempfallrisk.value = "motion aids";
            tempfallrisk.remarks = "";
            fa.objective_fa.Add(tempfallrisk);
        }
        if (fall4.Checked)
        {
            ObjectiveFA tempfallrisk = new ObjectiveFA();
            tempfallrisk.objective_id = Guid.Empty;
            tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
            tempfallrisk.soap_mapping_name = "FALL RISK";
            tempfallrisk.value = "Disorder";
            tempfallrisk.remarks = "";
            fa.objective_fa.Add(tempfallrisk);
        }
        if (fall5.Checked)
        {
            ObjectiveFA tempfallrisk = new ObjectiveFA();
            tempfallrisk.objective_id = Guid.Empty;
            tempfallrisk.soap_mapping_id = Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c");
            tempfallrisk.soap_mapping_name = "FALL RISK";
            tempfallrisk.value = "fasting";
            tempfallrisk.remarks = "";
            fa.objective_fa.Add(tempfallrisk);
        }

        foreach (var objective in fa.objective_fa)
        {
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
        }

        //Log.Info(LogConfig.LogEnd());

        return fa;
    }
}