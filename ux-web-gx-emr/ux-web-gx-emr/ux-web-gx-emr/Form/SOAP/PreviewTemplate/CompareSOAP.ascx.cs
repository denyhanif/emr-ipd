using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_SOAP_PreviewTemplate_CompareSOAP : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void initializevalue(SOAP Originaldata, SOAP Backupdata)
    {
        divdata_ori.InnerHtml = setBindingData(Originaldata);
        divdata_backup.InnerHtml = setBindingData(Backupdata);
    }

    public string setBindingData(SOAP Data)
    {
        StringBuilder innerDiv = new StringBuilder();

        StringBuilder reminderSection = new StringBuilder();
        StringBuilder soapSection = new StringBuilder();
        StringBuilder ttvSection = new StringBuilder();
        StringBuilder labSection = new StringBuilder();
        StringBuilder radSection = new StringBuilder();
        StringBuilder labradSection = new StringBuilder();

        StringBuilder diagnosticSection = new StringBuilder();
        StringBuilder procedureSection = new StringBuilder();
        StringBuilder diagprocSection = new StringBuilder();
        
        StringBuilder allergydrugSection = new StringBuilder();
        StringBuilder allergyfoodSection = new StringBuilder();
        StringBuilder allergyotherSection = new StringBuilder();
        StringBuilder allergySection = new StringBuilder();
        StringBuilder currmedSection = new StringBuilder();
        StringBuilder drugSection = new StringBuilder();
        StringBuilder consumableSection = new StringBuilder();
        StringBuilder racikanSection = new StringBuilder();
        StringBuilder adddrugSection = new StringBuilder();
        StringBuilder addconsumableSection = new StringBuilder();
        StringBuilder addracikanSection = new StringBuilder();


        reminderSection.Append("<label style=\"font-weight:bold;\">REMINDER :</label> <br />");
        if (Data.patient_notification.Count > 0)
        {
            for (int i = 0; i < Data.patient_notification.Count; i++)
            {
                reminderSection.Append("<label>- " + Data.patient_notification[i].notification + "</label><br />");
            }
        }

        string hamil = "";
        if (Data.subjective.Find(y => y.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")).value.ToLower() == "false")
        {
            hamil = "No";
        }
        else if (Data.subjective.Find(y => y.soap_mapping_id == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")).value.ToLower() == "true")
        {
            hamil = "Yes";
        }
        string susu = "";
        if (Data.subjective.Find(y => y.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")).value.ToLower() == "false")
        {
            susu = "No";
        }
        else if (Data.subjective.Find(y => y.soap_mapping_id == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")).value.ToLower() == "true")
        {
            susu = "Yes";
        }
        soapSection.Append("<label style=\"font-weight:bold;\">SOAP :</label> <br />");
        soapSection.Append("<table>"
            + "<tr><td><label>Chief Complaint</label></td> <td><label> : " + Data.subjective.Find(y => y.soap_mapping_id == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf")).remarks + "</label></td></tr>"
            + "<tr><td><label>Anamnesis</label></td> <td><label> : " + Data.subjective.Find(y => y.soap_mapping_id == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0")).remarks + "</label></td></tr>"
            + "<tr><td><label>Hamil</label></td> <td><label> : " + hamil + "</label></td></tr>"
            + "<tr><td><label>Menyusui</label></td> <td><label> : " + susu + "</label></td></tr>"
            + "<tr><td><label>O</label></td> <td><label> : " + Data.objective.Find(y => y.soap_mapping_id == Guid.Parse("7218971C-E89F-4172-AE3C-B7FB855C1D6D")).remarks + "</label></td></tr>"
            + "<tr><td><label>A</label></td> <td><label> : " + Data.assessment.Find(y => y.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")).remarks + "</label></td></tr>"
            + "<tr><td><label>P</label></td> <td><label> : " + Data.planning.Find(y => y.soap_mapping_id == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6")).remarks + "</label></td></tr>"
            + "<tr><td><label>Procedure Result</label></td> <td><label> : " + Data.planning.Find(y => y.soap_mapping_id == Guid.Parse("3e20f648-32ba-4d1d-b390-a3c372a7d30a")).remarks + "</label></td></tr>"
            + "</table>");

        ttvSection.Append("<label style=\"font-weight:bold;\">TTV :</label> <br />");
        ttvSection.Append("<table>"
            + "<tr><td><label>Blood Pressure</label></td> <td><label> : " + Data.objective.Find(y => y.soap_mapping_id == Guid.Parse("ae3ca8c2-eab0-41b6-9e3e-3ecb8071a9d0")).value + "/" + Data.objective.Find(y => y.soap_mapping_id == Guid.Parse("e5efd220-b68e-4652-ad03-d56ef29fcebb")).value + " mmHg" + "</label></td></tr>"
            + "<tr><td><label>Pulse Rate</label></td> <td><label> : " + Data.objective.Find(y => y.soap_mapping_id == Guid.Parse("78cbb61f-4a11-470a-b770-1a44eb04357f")).value + " X/mnt" + "</label></td></tr>"
            + "<tr><td><label>Respiratory rate</label></td> <td><label> : " + Data.objective.Find(y => y.soap_mapping_id == Guid.Parse("e6ae2ea9-b321-4756-bf96-2dc232e4a7ba")).value + " X/mnt" + "</label></td></tr>"
            + "<tr><td><label>SpO2</label></td> <td><label> : " + Data.objective.Find(y => y.soap_mapping_id == Guid.Parse("e903246c-df95-4fe0-96d2-cf90c036d3d7")).value + " %" +  "</label></td></tr>"
            + "<tr><td><label>Temperature</label></td> <td><label> : " + Data.objective.Find(y => y.soap_mapping_id == Guid.Parse("2eeca752-a2ea-4426-b3cf-c1ea3833bf30")).value + " ℃" + "</label></td></tr>"
            + "<tr><td><label>Weight</label></td> <td><label> : " + Data.objective.Find(y => y.soap_mapping_id == Guid.Parse("52ce9350-bfb2-4072-8893-d0c6cf8b3634")).value + " kg" + "</label></td></tr>"
            + "<tr><td><label>Height</label></td> <td><label> : " + Data.objective.Find(y => y.soap_mapping_id == Guid.Parse("2a8dbddb-edfe-4704-876e-5a2d735bb541")).value + " cm" + "</label></td></tr>"
            + "<tr><td><label>Head Circumference</label></td> <td><label> : " + Data.objective.Find(y => y.soap_mapping_id == Guid.Parse("A8E0013B-0443-4E7A-B670-4DB9362B40E4")).value + " cm" + "</label></td></tr>"
            + "</table>");

        //labSection.Append("<label style=\"font-weight:bold;\">LAB :</label> <br />");
        if (Data.cpoe_trans.Count > 0)
        {
            for (int i = 0; i < Data.cpoe_trans.Count ; i++)
            {
                if (Data.cpoe_trans[i].type == "ClinicLab" || Data.cpoe_trans[i].type == "CitoLab" || Data.cpoe_trans[i].type == "MicroLab" || Data.cpoe_trans[i].type == "PatologiLab" || Data.cpoe_trans[i].type == "PanelLab" || Data.cpoe_trans[i].type == "MDCLab")
                {
                    if (Data.cpoe_trans[i].isdelete == 0)
                    {
                        labSection.Append("<label>- " + Data.cpoe_trans[i].name + "</label><br />");
                    }
                }
            }
        }

        //radSection.Append("<label style=\"font-weight:bold;\">RAD :</label> <br />");
        if (Data.cpoe_trans.Count > 0)
        {
            for (int i = 0; i < Data.cpoe_trans.Count; i++)
            {
                if (Data.cpoe_trans[i].type == "MRI1" || Data.cpoe_trans[i].type == "MRI3" || Data.cpoe_trans[i].type == "Radiology" || Data.cpoe_trans[i].type == "USG" || Data.cpoe_trans[i].type == "CT")
                {
                    if (Data.cpoe_trans[i].isdelete == 0)
                    {
                        radSection.Append("<label>- " + Data.cpoe_trans[i].name + "</label><br />");
                    }
                }
            }
        }

        labradSection.Append("<table style=\"width:100%;\">"
            + "<tr><td style=\"width:50%;\"><label style=\"font-weight:bold;\">LAB :</label></td> <td style=\"width:50%;\"><label style=\"font-weight:bold;\">RAD :</label></td></tr>"
            + "<tr><td>" + labSection
            + "</td><td>" + radSection
            + "</td></tr> </table>");

        labradSection.Append("<br />"
            + "<label style=\"font-weight:bold;\">OTHERS :</label> <br />" + Data.planning.Find(y => y.soap_mapping_id == Guid.Parse("0c822444-a7ee-4904-a223-2869e8424579")).remarks + "<br /><br />"
            + "<label style=\"font-weight:bold;\">CLINICAL DIAGNOSIS :</label> <br />" + Data.planning.Find(y => y.soap_mapping_id == Guid.Parse("2a20fd9b-0911-4fbb-b8f0-84ce19c743e9")).remarks + "<br />");


        /// Compare SOAP Diagnostic and Procedure 
        // Diagnostic
        if (Data.procedure_diagnosis.Where(c=>c.ProcedureItemTypeId == 4).ToList().Count > 0)
        {
            foreach (var item in Data.procedure_diagnosis.Where(c => c.ProcedureItemTypeId == 4).ToList())
            {
                diagnosticSection.Append("<label>- " + item.ProcedureItemName + "</label><br />");
            }
        }
        // Procedure
        if (Data.procedure_diagnosis.Where(c => c.ProcedureItemTypeId == 5).ToList().Count > 0)
        {
            foreach (var item in Data.procedure_diagnosis.Where(c => c.ProcedureItemTypeId == 5).ToList())
            {
                procedureSection.Append("<label>- " + item.ProcedureItemName + "</label><br />");
            }
        }

        diagprocSection.Append("<table style=\"width:100%;\">"
            + "<tr><td style=\"width:50%;\"><label style=\"font-weight:bold;\">DIAGNOSTIC :</label></td> <td style=\"width:50%;\"><label style=\"font-weight:bold;\">PROCEDURE :</label></td></tr>"
            + "<tr><td>" + diagnosticSection + "</td>"
            + "<td>" + procedureSection + "</td></tr> "
            + "</table>");

        diagprocSection.Append("<br />"
            + "<label style=\"font-weight:bold;\">OTHERS DIAGNOSTIC :</label> <br />" + Data.planning.Find(y => y.soap_mapping_id == Guid.Parse("35779378-FC19-41B5-8445-D6C6D358BDE5")).remarks + "<br /><br />"
            + "<label style=\"font-weight:bold;\">OTHERS PROCEDURE:</label> <br />" + Data.planning.Find(y => y.soap_mapping_id == Guid.Parse("E4565CFB-1E9E-47EC-A06E-F21240043289")).remarks + "<br /><br />");

        allergydrugSection.Append("<label style=\"font-weight:bold;\">ALLERGY DRUG :</label> <br />");
        if (Data.patient_allergy.Count > 0)
        {
            for (int i = 0; i < Data.patient_allergy.Count; i++)
            {
                if (Data.patient_allergy[i].allergy_type == 1)
                {
                    allergydrugSection.Append("<label>- " + Data.patient_allergy[i].allergy + "</label><br />");
                }
            }
        }

        allergyfoodSection.Append("<label style=\"font-weight:bold;\">ALLERGY FOOD :</label> <br />");
        if (Data.patient_allergy.Count > 0)
        {
            for (int i = 0; i < Data.patient_allergy.Count; i++)
            {
                if (Data.patient_allergy[i].allergy_type == 2)
                {
                    allergyfoodSection.Append("<label>- " + Data.patient_allergy[i].allergy + "</label><br />");
                }
            }
        }

        allergyotherSection.Append("<label style=\"font-weight:bold;\">ALLERGY OTHER :</label> <br />");
        if (Data.patient_allergy.Count > 0)
        {
            for (int i = 0; i < Data.patient_allergy.Count; i++)
            {
                if (Data.patient_allergy[i].allergy_type == 7)
                {
                    allergyotherSection.Append("<label>- " + Data.patient_allergy[i].allergy + "</label><br />");
                }
            }
        }

        allergySection.Append("<table style=\"width:100%;\">"
            + "<tr><td>" + allergydrugSection
            + "</td><td>" + allergyfoodSection
            + "</td><td>" + allergyotherSection
            + "</td></tr> </table>");

        currmedSection.Append("<label style=\"font-weight:bold;\">CURRENT MEDICATION :</label> <br />");
        if (Data.patient_medication.Count > 0)
        {
            for (int i = 0; i < Data.patient_medication.Count; i++)
            {
                currmedSection.Append("<label>- " + Data.patient_medication[i].medication + "</label><br />");
            }
        }

        drugSection.Append("<label style=\"font-weight:bold;\">PRESCRIPTION :</label> <br />");
        if (Data.prescription.Count > 0)
        {
            List<Prescription> drug = Data.prescription.FindAll(x => x.is_consumables != 1);
            if (drug.Count > 0)
            {
                drugSection.Append("<table style=\"width:100%;\">"
                    + "<tr><td style=\"padding:2px; border:1px solid lightgray;\">Item</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Dose</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Dose UoM</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Frequency</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Route</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Qty</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">UoM</td> </tr>");
                for (int i = 0; i < drug.Count; i++)
                {
                    drugSection.Append("<tr><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].item_name
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].dosage_id.Split('.')[0]
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].dose_uom
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].frequency_code
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].administration_route_code
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].quantity.Split('.')[0]
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].uom_code
                        + "</td></tr>");
                }
                drugSection.Append("</table>");
            }
        }

        drugSection.Append("<br />"
            + "<label style=\"font-weight:bold;\">PRESCRIPTION NOTES :</label> <br />" + Data.planning.Find(y => y.soap_mapping_id == Guid.Parse("2df0294d-f94e-4ba4-8ba1-f017bfb55d92")).remarks + "<br />");

        racikanSection.Append("<label style=\"font-weight:bold;\">COMPOUND :</label> <br />");
        if (Data.compound_header.Count > 0)
        {
            List<CompoundHeaderSoap> racikan = Data.compound_header.FindAll(x => x.is_additional != true);
            if (racikan.Count > 0)
            {
                racikanSection.Append("<table style=\"width:100%;\">"
                    + "<tr><td style=\"padding:2px; border:1px solid lightgray;\">Item</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Dose</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Dose UoM</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Frequency</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Route</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Qty</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">UoM</td> </tr>");
                for (int i = 0; i < racikan.Count; i++)
                {
                    racikanSection.Append("<tr><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].compound_name
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].dose.Split('.')[0]
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].dose_uom
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].frequency_code
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].administration_route_code
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].quantity.Split('.')[0]
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].uom_code
                        + "</td></tr>");
                }
                racikanSection.Append("</table>");
            }
        }

        consumableSection.Append("<label style=\"font-weight:bold;\">CONSUMABLE :</label> <br />");
        if (Data.prescription.Count > 0)
        {
            List<Prescription> cons = Data.prescription.FindAll(x => x.is_consumables == 1);
            if (cons.Count > 0)
            {
                consumableSection.Append("<table style=\"width:100%;\">"
                    + "<tr><td style=\"padding:2px; border:1px solid lightgray;\">Item</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Qty</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">UoM</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Instruction</td> </tr>");
                for (int i = 0; i < cons.Count; i++)
                {
                    consumableSection.Append("<tr><td style=\"padding:2px; border:1px solid lightgray;\">" + cons[i].item_name
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + cons[i].quantity.Split('.')[0]
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + cons[i].uom_code
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + cons[i].remarks
                        + "</td></tr>");
                }
                consumableSection.Append("</table>");
            }
        }

        adddrugSection.Append("<label style=\"font-weight:bold;\">ADDITIONAL PRESCRIPTION :</label> <br />");
        if (Data.additional_prescription.Count > 0)
        {
            List<Prescription> drug = Data.additional_prescription.FindAll(x => x.is_consumables != 1);
            if (drug.Count > 0)
            {
                adddrugSection.Append("<table style=\"width:100%;\">"
                    + "<tr><td style=\"padding:2px; border:1px solid lightgray;\">Item</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Dose</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Dose UoM</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Frequency</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Route</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Qty</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">UoM</td> </tr>");
                for (int i = 0; i < drug.Count; i++)
                {
                    adddrugSection.Append("<tr><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].item_name
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].dosage_id.Split('.')[0]
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].dose_uom
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].frequency_code
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].administration_route_code
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].quantity.Split('.')[0]
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + drug[i].uom_code
                        + "</td></tr>");
                }
                adddrugSection.Append("</table>");
            }
        }

        adddrugSection.Append("<br />"
            + "<label style=\"font-weight:bold;\">ADDITIONAL PRESCRIPTION NOTES :</label> <br />" + Data.planning.Find(y => y.soap_mapping_id == Guid.Parse("5e34ae60-1d72-4efd-8440-c4442515aabe")).remarks + "<br />");

        addracikanSection.Append("<label style=\"font-weight:bold;\">ADDITIONAL COMPOUND :</label> <br />");
        if (Data.compound_header.Count > 0)
        {
            List<CompoundHeaderSoap> racikan = Data.compound_header.FindAll(x => x.is_additional == true);
            if (racikan.Count > 0)
            {
                addracikanSection.Append("<table style=\"width:100%;\">"
                    + "<tr><td style=\"padding:2px; border:1px solid lightgray;\">Item</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Dose</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Dose UoM</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Frequency</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Route</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Qty</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">UoM</td> </tr>");
                for (int i = 0; i < racikan.Count; i++)
                {
                    addracikanSection.Append("<tr><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].compound_name
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].dose.Split('.')[0]
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].dose_uom
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].frequency_code
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].administration_route_code
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].quantity.Split('.')[0]
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + racikan[i].uom_code
                        + "</td></tr>");
                }
                addracikanSection.Append("</table>");
            }
        }

        addconsumableSection.Append("<label style=\"font-weight:bold;\">ADDITIONAL CONSUMABLE :</label> <br />");
        if (Data.additional_prescription.Count > 0)
        {
            List<Prescription> cons = Data.additional_prescription.FindAll(x => x.is_consumables == 1);
            if (cons.Count > 0)
            {
                addconsumableSection.Append("<table style=\"width:100%;\">"
                    + "<tr><td style=\"padding:2px; border:1px solid lightgray;\">Item</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Qty</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">UoM</td>"
                    + "<td style=\"padding:2px; border:1px solid lightgray;\">Instruction</td> </tr>");
                for (int i = 0; i < cons.Count; i++)
                {
                    addconsumableSection.Append("<tr><td style=\"padding:2px; border:1px solid lightgray;\">" + cons[i].item_name
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + cons[i].quantity.Split('.')[0]
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + cons[i].uom_code
                        + "</td><td style=\"padding:2px; border:1px solid lightgray;\">" + cons[i].remarks
                        + "</td></tr>");
                }
                addconsumableSection.Append("</table>");
            }
        }



        innerDiv.Append("<div style=\"padding: 7px; border: 1px solid lightgray; border-radius: 7px;\">"
            + "<label style=\"font-size:14px;\"><i class=\"fa fa-clock-o\"></i> Date Modified : </label><label style=\"font-size:14px; font-weight:bold;\">" + DateTime.Parse(Data.BackupDate).ToString("HH:mm, dd MMM yyyy") + "</label>"
            + "<br /><br />"
            + reminderSection
            + "<br />"
            + soapSection
            + "<br />"
            + ttvSection
            + "<hr />"
            + labradSection
            + "<hr />"
            + diagprocSection
            + "<hr />"
            + allergySection
            + "<br />"
            + currmedSection
            + "<hr />"
            + drugSection
            + "<br />"
            + racikanSection
            + "<br />"
            + consumableSection
            + "<hr />"
            + adddrugSection
            + "<br />"
            + addracikanSection
            + "<br />"
            + addconsumableSection
            + "<br />"
            + "</div>");

        return innerDiv.ToString();
    }
}