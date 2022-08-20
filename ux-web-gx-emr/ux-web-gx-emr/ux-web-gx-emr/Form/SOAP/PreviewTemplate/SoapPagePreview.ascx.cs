using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;

public partial class Form_SOAP_PreviewTemplate_SoapPagePreview : System.Web.UI.UserControl
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
    }

    public void initializevalue(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Organization_ID", OrganizationId.ToString() },
                { "Patient_ID", PatientId.ToString() },
                { "Admission_ID", AdmissionId.ToString() },
                { "Encounter_ID", EncounterId.ToString() },
            };
            //Log.Debug(LogConfig.LogStart("GetResume", logParam));
            var Data = clsMedicalResume.GetResume(OrganizationId, PatientId, AdmissionId, EncounterId);
            var JsongetPatientHistory = JsonConvert.DeserializeObject<ResultMedicalResume>(Data.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("GetResume", JsongetPatientHistory.Status, JsongetPatientHistory.Message));

            AdmissionType.Text = JsongetPatientHistory.list.resumeheader.AdmissionType.ToString();
            lblmrno.Text = JsongetPatientHistory.list.resumeheader.LocalMrNo.ToString();
            lblnamepatient.Text = JsongetPatientHistory.list.resumeheader.PatientName.ToString();
            lbldobpatient.Text = JsongetPatientHistory.list.resumeheader.BirthDate.ToString() + "/" + JsongetPatientHistory.list.resumeheader.Age.ToString();
            lblsexpatient.Text = JsongetPatientHistory.list.resumeheader.Gender.ToString();
            lbldoctorprimary.Text = JsongetPatientHistory.list.resumeheader.DoctorName.ToString();


            //AdmissionDate.Text = DateTime.Parse(JsongetPatientHistory.list.resumeheader.admissionDate.ToString()).ToString("dd MMM yyyy");
            //PatientData.Text = "MR No. " + JsongetPatientHistory.list.resumeheader.localMrNo.ToString() + " - " + JsongetPatientHistory.list.resumeheader.patientName.ToString();
            //Age.Text = JsongetPatientHistory.list.resumeheader.age.ToString();
            hfpreviewpres.Value = Data.Result.ToString();
            List<ResumeData> resumedata = JsongetPatientHistory.list.resumedata;
            List<ResumeFA> resumefa = JsongetPatientHistory.list.resumefa;
            List<ResumePrescription> resumepres = JsongetPatientHistory.list.resumeprescription;
            DataTable dt = Helper.ToDataTable(JsongetPatientHistory.list.resumeprescription);

            #region ChiefComplain
            lblChiefComplaint.Text = (from a in resumedata
                              where a.MappingId == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf")
                              select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            #endregion

            #region Anamnesis
            Anamnesis.Text = (from a in resumedata
                              where a.MappingId == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0")
                              select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            var pregnant = (from a in resumedata
                                  where a.MappingId == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")
                                  select a.Value).SingleOrDefault().ToString();
            if (pregnant == "True")
                lblispregnant.Text = ": Yes";

            var breastfeed = (from a in resumedata
                              where a.MappingId == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")
                              select a.Value).SingleOrDefault().ToString();

            if (breastfeed == "True")
                lblbreastfeed.Text = ": Yes";

            #endregion

            #region Medication & Allergy

            var routine = (from a in resumefa
                           where a.Type == "RoutineMedication"
                           select a).ToList();

            if (routine.Count > 0)
            {
                DataTable dtroutine = Helper.ToDataTable(routine);
                rptRoutine.DataSource = dtroutine;
                rptRoutine.DataBind();
                lblnoroute.Visible = false;
            }
            else
            {
                DataTable dtroutine = null;
                rptRoutine.DataSource = dtroutine;
                rptRoutine.DataBind();
                lblnoroute.Visible = true;
            }
            

            var drugsallergy = (from a in resumefa
                           where a.Type == "DrugAllergy"
                           select a).ToList();

            if (drugsallergy.Count > 0)
            {
                rptDrugAllergies.DataSource = drugsallergy;
                rptDrugAllergies.DataBind();
                lblnodrugallergy.Visible = false;
            }
            else
            {
                rptDrugAllergies.DataSource = null;
                rptDrugAllergies.DataBind();
                lblnodrugallergy.Visible = true;
            }

            

            var foodallergy = (from a in resumefa
                         where a.Type == "FoodAllergy"
                         select a).ToList();

            if (foodallergy.Count > 0)
            {
                rptFoodAllergies.DataSource = foodallergy;
                rptFoodAllergies.DataBind();
                lblnofoodallergy.Visible = false;
            }
            else
            {
                rptFoodAllergies.DataSource = null;
                rptFoodAllergies.DataBind();
                lblnofoodallergy.Visible = true;
            }

            var otherallergy = (from a in resumefa
                               where a.Type == "OtherAllergy"
                               select a).ToList();

            if (otherallergy.Count > 0)
            {
                rptOtherAllergies.DataSource = otherallergy;
                rptOtherAllergies.DataBind();
                lblnootherallergy.Visible = false;
            }
            else
            {
                rptOtherAllergies.DataSource = null;
                rptOtherAllergies.DataBind();
                lblnootherallergy.Visible = true;
            }

            #endregion

            #region Illness History
            //IllnessHistory.DataSource = null;
            //var illness = (from a in resumedata
            //                             where a.MappingId == Guid.Parse("1979DDCB-33BC-4187-BE92-04FBDB0A50D6")
            //                             select new { remarks = a.Remarks }).ToList();

            //IllnessHistory.DataSource = illness;
            //IllnessHistory.DataBind();

            var surgery = (from a in resumefa
                           where a.Type == "Surgery"
                           select a).ToList();

            if (surgery.Count > 0)
            {
                rptSurgeryHistory.DataSource = surgery;
                rptSurgeryHistory.DataBind();
                lblnosurgery.Visible = false;
            }
            else
            {
                rptSurgeryHistory.DataSource = null;
                rptSurgeryHistory.DataBind();
                lblnosurgery.Visible = true;
            }
            

            var personaldisease = (from a in resumefa
                           where a.Type == "PersonalDisease"
                                   select a).ToList();

            foreach (var x in personaldisease.Where(x => x.Value == "Lain-lain"))
            {
                x.Value = x.Remarks;
            }

            if (personaldisease.Count > 0)
            {
                rptDiseaseHistory.DataSource = personaldisease;
                rptDiseaseHistory.DataBind();
                lblnodisease.Visible = false;
            }
            else
            {
                rptDiseaseHistory.DataSource = null;
                rptDiseaseHistory.DataBind();
                lblnodisease.Visible = true;
            }


            var familydisease = (from a in resumefa
                                   where a.Type == "FamilyDisease"
                                 select a).ToList();

            foreach (var x in familydisease.Where(x => x.Value == "Lain-lain"))
            {
                x.Value = x.Remarks;
            }

            if (familydisease.Count > 0)
            {
                rptFamilyDisease.DataSource = familydisease;
                rptFamilyDisease.DataBind();
                lblnofamilydisease.Visible = false;
            }
            else
            {
                rptFamilyDisease.DataSource = null;
                rptFamilyDisease.DataBind();
                lblnofamilydisease.Visible = true;
            }
            

            #endregion

            #region Physical Examination
            var gcsEye = (from a in resumedata
                        where a.MappingId == Guid.Parse("A9C5CD3C-1E02-4DB2-A047-2F1983D1315B")
                        select a.Value).SingleOrDefault().ToString();

            var gcsMove = (from a in resumedata
                          where a.MappingId == Guid.Parse("89B583A5-3003-43AB-9693-60EA6181C8D5")
                          select a.Value).SingleOrDefault().ToString();

            var gcsVerbal = (from a in resumedata
                          where a.MappingId == Guid.Parse("FE4CF48C-17A6-4720-AD23-186517DD9C85")
                          select a.Value).SingleOrDefault().ToString();

            if (gcsEye != "")
            {
                switch (gcsEye)
                {
                    case "1":
                        eye.Text = "1. None";
                        break;
                    case "2":
                        eye.Text = "2. To Pressure";
                        break;
                    case "3":
                        eye.Text = "3. To Sound";
                        break;
                    case "4":
                        eye.Text = "4. Spontaneus";
                        break;
                }
            }

            if (gcsMove != "")
            {
                switch (gcsMove)
                {
                    case "1":
                        move.Text = "1. None";
                        break;
                    case "2":
                        move.Text = "2. Extension";
                        break;
                    case "3":
                        move.Text = "3. Flexion to pain stumulus";
                        break;
                    case "4":
                        move.Text = "4. Withdrawns from pain";
                        break;
                    case "5":
                        move.Text = "5. Localizes to pain stimulus";
                        break;
                    case "6":
                        move.Text = "6. Obey Commands";
                        break;
                }
            }

            if (gcsVerbal != "")
            {
                switch (gcsVerbal)
                {
                    case "1":
                        verbal.Text = "1. None";
                        break;
                    case "2":
                        verbal.Text = "2. Incomprehensible sounds";
                        break;
                    case "3":
                        verbal.Text = "3. Inappropriate words";
                        break;
                    case "4":
                        verbal.Text = "4. Confused";
                        break;
                    case "5":
                        verbal.Text = "5. Orientated";
                        break;
                    case "T":
                        verbal.Text = "T. Tracheostomy";
                        break;
                    case "A":
                        verbal.Text = "A. Aphasia";
                        break;
                }
            }

            if (gcsEye != "" && gcsMove != "" && gcsVerbal != "")
            {
                if (gcsVerbal != "T" && gcsVerbal != "A")
                {
                    int tempgcs = int.Parse(gcsEye) + int.Parse(gcsMove) + int.Parse(gcsVerbal);
                    painscore.Text = tempgcs.ToString();
                }
                else
                {
                    painscore.Text = "-";
                }
            }
            else
                painscore.Text = "-";


            //painscore.Text = (from a in resumedata
            //                  where a.MappingId == Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176")
            //                  select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();


            lblpainscale.Text = (from a in resumedata
                                 where a.MappingId == Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176")
                                 select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

            bloodpreassure.Text = (from a in resumedata
                                   where a.MappingId == Guid.Parse("E5EFD220-B68E-4652-AD03-D56EF29FCEBB")
                                   select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString() + "/ " +
                                   (from a in resumedata
                                    where a.MappingId == Guid.Parse("AE3CA8C2-EAB0-41B6-9E3E-3ECB8071A9D0")
                                    select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

            pulse.Text = (from a in resumedata
                          where a.MappingId == Guid.Parse("78cbb61f-4a11-470a-b770-1a44eb04357f")
                          select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

            respiratory.Text = (from a in resumedata
                                where a.MappingId == Guid.Parse("e6ae2ea9-b321-4756-bf96-2dc232e4a7ba")
                                select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

            spo.Text = (from a in resumedata
                        where a.MappingId == Guid.Parse("e903246c-df95-4fe0-96d2-cf90c036d3d7")
                        select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

            temperature.Text = (from a in resumedata
                                where a.MappingId == Guid.Parse("2eeca752-a2ea-4426-b3cf-c1ea3833bf30")
                                select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

            weight.Text = (from a in resumedata
                                where a.MappingId == Guid.Parse("52ce9350-bfb2-4072-8893-d0c6cf8b3634")
                                select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

            height.Text = (from a in resumedata
                           where a.MappingId == Guid.Parse("2a8dbddb-edfe-4704-876e-5a2d735bb541")
                           select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

            headcircumference.Text = (from a in resumedata
                           where a.MappingId == Guid.Parse("A8E0013B-0443-4E7A-B670-4DB9362B40E4")
                           select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();



            lblmentalstatus.Text = (from a in resumedata
                                    where a.MappingId == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")
                                    select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();

            lblconsciousness.Text = (from a in resumedata
                                     where a.MappingId == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")
                                     select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

            lblphysicalexamination.Text = (from a in resumedata
                                           where a.MappingId == Guid.Parse("7218971c-e89f-4172-ae3c-b7fb855c1d6d")
                                           select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();

            var fallrisk = (from a in resumedata
                            where a.MappingId == Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c")
                            select a).ToList();

            if (fallrisk.Count > 0)
            {
                foreach (var x in fallrisk)
                {
                    if (x.Value.ToUpper() == "undergo sedation".ToUpper())
                    {
                        x.Value = "Patient undergo sedation";
                    }
                    else if (x.Value.ToUpper() == "physical limitation".ToUpper())
                    {
                        x.Value = "Patient with physical limitation";
                    }
                    else if (x.Value.ToUpper() == "motion aids".ToUpper())
                    {
                        x.Value = "Patient with motion aids";
                    }
                    else if (x.Value.ToUpper() == "Disorder".ToUpper())
                    {
                        x.Value = "Patient with balance disorder";
                    }
                    else if (x.Value.ToUpper() == "fasting".ToUpper())
                    {
                        x.Value = "Fasting patient to undergo further test(Lab/Radiology/etc)";
                    }
                }
                rptfallrisk.DataSource = fallrisk;
                rptfallrisk.DataBind();
            }
            else
            {
                rptfallrisk.DataSource = null;
                rptfallrisk.DataBind();
            }

            
            //weight.Text = (from a in resumedata
            //               where a.mappingId == Guid.Parse("52ce9350-bfb2-4072-8893-d0c6cf8b3634")
            //               select a.value).SingleOrDefault().ToString();

            //breathing.Text = (from a in resumedata
            //                  where a.mappingId == Guid.Parse("E6AE2EA9-B321-4756-BF96-2DC232E4A7BA")
            //                  select a.value).SingleOrDefault().ToString();

            //mentalstatus.Text = (from a in resumedata
            //                     where a.mappingId == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")
            //                     select a.value).SingleOrDefault().ToString();

            #endregion


            #region procedure
            procedure.Text = (from a in resumedata
                              where a.MappingId == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6")
                              select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            #endregion

            #region diagnosis
            primarydiagnosis.Text = (from a in resumedata
                              where a.MappingId == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")
                              select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            #endregion

            #region Procedure Result
            procedureResult.Text = (from a in resumedata
                              where a.MappingId == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")
                              select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            #endregion

            #region prescription
            prescriptiondrugs.DataSource = null;
            RepeaterRacikan.DataSource = null;
            prescriptioncompound.DataSource = null;
            prescriptionconsumables.DataSource = null;


            List<ResumePrescription> drugspresc = (from a in resumepres
                                                   where a.isConsumables == false && a.compound_id == Guid.Empty 
                                                   select a).ToList();

            List<ResumePrescription> compheaderpresc = (from a in resumepres
                                                   where a.salesItemName == ""
                                                   select a).ToList();

            List<ResumePrescription> consumables = (from a in resumepres
                                                    where a.isConsumables == true 
                                                    select a).ToList();

            List<CompoundHeaderSoap> racikanheader = JsongetPatientHistory.list.compound_header;

            List<CompoundHeaderSoap> tmpracikanheader = (from a in racikanheader
                                                         where a.is_additional == false
                                                         select a).ToList();

            List<CompoundHeaderSoap> tmpracikanheaderadd = (from a in racikanheader
                                                         where a.is_additional == true
                                                         select a).ToList();

            foreach (var templist in tmpracikanheader)
            {
                string a = templist.quantity.ToString().Substring(0, 3);
                string[] tempqty = templist.quantity.ToString().Split('.');
                if (tempqty[1].Length == 3)
                {
                    if (tempqty[1] == "000")
                    {
                        templist.quantity = decimal.Parse(tempqty[0]).ToString();
                    }
                    else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                    {
                        templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                    }
                    else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                    {
                        templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                    }
                }

                string[] tempdose = templist.dose.ToString().Split('.');

                if (tempdose[1].Length == 3)
                {
                    if (tempdose[1] == "000")
                    {
                        templist.dose = decimal.Parse(tempdose[0]).ToString();
                    }
                    else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                    {
                        templist.dose = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                    }
                    else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                    {
                        templist.dose = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                    }
                }
            }

            foreach (var templist in tmpracikanheaderadd)
            {
                string a = templist.quantity.ToString().Substring(0, 3);
                string[] tempqty = templist.quantity.ToString().Split('.');
                if (tempqty[1].Length == 3)
                {
                    if (tempqty[1] == "000")
                    {
                        templist.quantity = decimal.Parse(tempqty[0]).ToString();
                    }
                    else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                    {
                        templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                    }
                    else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                    {
                        templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                    }
                }

                string[] tempdose = templist.dose.ToString().Split('.');

                if (tempdose[1].Length == 3)
                {
                    if (tempdose[1] == "000")
                    {
                        templist.dose = decimal.Parse(tempdose[0]).ToString();
                    }
                    else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                    {
                        templist.dose = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                    }
                    else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                    {
                        templist.dose = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                    }
                }
            }

            //List<ResumeDrugs> listpres = (from a in JsongetPatientHistory.list.resumeprescription
            //                where a.compound_id == Guid.Empty && a.isConsumables == false
            //                select new ResumeDrugs
            //                {
            //                    salesItemName = a.salesItemName,
            //                    Remarks = string.IsNullOrEmpty(a.doseText) ? a.quantity.ToString("0.###") + a.uom.ToString() + " - " + a.frequency.ToString() + " (" + a.instruction.ToString() + ") " + a.route.ToString() : a.doseText
            //                }).ToList();
            //DataTable dttemp = Helper.ToDataTable(drugspresc);

            foreach (var x in drugspresc)
            {
                if (x.isRoutine)
                    x.routine = "Yes";
                else
                    x.routine = "No";
            }

            if (drugspresc.Count > 0)
            {
                drugs.Visible = false;
                divdrugsprescription.Visible = true;
                prescriptiondrugs.DataSource = drugspresc.Where(a => a.IsAdditional == 0);
                prescriptiondrugs.DataBind();
            }
            else
            {
                drugs.Visible = true;
                divdrugsprescription.Visible = false;
                prescriptiondrugs.DataSource = null;
                prescriptiondrugs.DataBind();
            }

            if (drugspresc.Where(a => a.IsAdditional == 1).Count() > 0)
            {
                trAdditionalDrugs.Visible = true;
                lbladditionalpres.Visible = false;
                dvAdditionalPres.Visible = true;
                rptAdditionalDrugs.DataSource = drugspresc.Where(a => a.IsAdditional == 1);
                rptAdditionalDrugs.DataBind();
            }
            else
            {
                trAdditionalDrugs.Visible = false;
                lbladditionalpres.Visible = true;
                dvAdditionalPres.Visible = false;
                rptAdditionalDrugs.DataSource = null;
                rptAdditionalDrugs.DataBind();
            }

            if (consumables.Where(a => a.IsAdditional == 1).Count() > 0)
            {
                trAdditionalConsumables.Visible = true;
                lbladditionalcons.Visible = false;
                dvAdditionalConsumables.Visible = true;
                
                rptAdditionalConsumables.DataSource = consumables.Where(a => a.IsAdditional == 1);
                rptAdditionalConsumables.DataBind();
            }
            else
            {
                trAdditionalConsumables.Visible = false;
                lbladditionalcons.Visible = true;
                dvAdditionalConsumables.Visible = false;
                rptAdditionalConsumables.DataSource = null;
                rptAdditionalConsumables.DataBind();
            }

            if (tmpracikanheader.Count > 0)
            {
                noracikan.Visible = false;
                divRacikan.Visible = true;
                RepeaterRacikan.DataSource = tmpracikanheader;
                RepeaterRacikan.DataBind();
            }
            else
            {
                noracikan.Visible = true;
                divRacikan.Visible = false;
                RepeaterRacikan.DataSource = null;
                RepeaterRacikan.DataBind();
            }

            if (tmpracikanheaderadd.Count > 0)
            {
                trAdditionalRacikan.Visible = true;
                noracikanadd.Visible = false;
                divRacikanAdditional.Visible = true;
                RepeaterRAcikanAdd.DataSource = tmpracikanheaderadd;
                RepeaterRAcikanAdd.DataBind();
            }
            else
            {
                trAdditionalRacikan.Visible = false;
                noracikanadd.Visible = true;
                divRacikanAdditional.Visible = false;
                RepeaterRAcikanAdd.DataSource = null;
                RepeaterRAcikanAdd.DataBind();
            }

            List<ResumeDrugs> listcompheader = (from a in JsongetPatientHistory.list.resumeprescription
                                          where a.salesItemName == "" && a.isConsumables == false
                                          select new ResumeDrugs
                                          {
                                              salesItemName = a.compound_name,
                                              Remarks = string.IsNullOrEmpty(a.doseText) ? a.quantity.ToString("0.###") + a.uom.ToString() + " - " + a.frequency.ToString() + " (" + a.instruction.ToString() + ") " + a.route.ToString() : a.doseText
                                          }).ToList();

            if (compheaderpresc.Count > 0)
            {
                compound.Visible = false;
                divcompoundprescription.Visible = true;
                prescriptioncompound.DataSource = compheaderpresc;
                prescriptioncompound.DataBind();
            }
            else
            {
                compound.Visible = true;
                divcompoundprescription.Visible = false;
                prescriptioncompound.DataSource = null;
                prescriptioncompound.DataBind();
            }

            var orgsetting = clsCommon.GetOrganizationSettingbyOrgId(OrganizationId, Helper.doctorid);
            var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());
            ViewOrganizationSetting tempvaluecompound = jsonorgsetting.list.Find(y => y.setting_name == "IS_COMPOUND");
            
            if (tempvaluecompound.setting_value == "FALSE")
                trcompound.Visible = false;
            else
                trcompound.Visible = true;

            List<ResumeDrugs> listcons = (from a in JsongetPatientHistory.list.resumeprescription
                                          where a.isConsumables == true
                                          select new ResumeDrugs
                                          {
                                              salesItemName = a.salesItemName,
                                              Remarks = string.IsNullOrEmpty(a.doseText) ? a.quantity.ToString("0.###") + a.uom.ToString() + " - " + a.frequency.ToString() + " (" + a.instruction.ToString() + ") " + a.route.ToString() : a.doseText
                                          }).ToList();

            if (consumables.Count > 0)
            {
                cons.Visible = false;
                divconsumables.Visible = true;
                prescriptionconsumables.DataSource = consumables.Where(a => a.IsAdditional == 0);
                prescriptionconsumables.DataBind();
            }
            else
            {
                cons.Visible = true;
                divconsumables.Visible = false;
                prescriptionconsumables.DataSource = null;
                prescriptionconsumables.DataBind();
            }

            #endregion

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", EncounterId.ToString(), "initializevalue", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));


        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", EncounterId.ToString(), "initializevalue", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var data = ((ResumePrescription)e.Item.DataItem).compound_name;
        ResultMedicalResume JsongetPatientHistory = JsonConvert.DeserializeObject<ResultMedicalResume>(hfpreviewpres.Value);
        List<ResumePrescription> listpres = JsongetPatientHistory.list.resumeprescription;
        List<ResumeDrugs> listcompdetail = (from a in listpres
                                            where a.compound_id != Guid.Empty && a.salesItemName != "" && a.compound_name == data
                                            select new ResumeDrugs
                                            {
                                                salesItemName = a.salesItemName,
                                                Remarks = string.IsNullOrEmpty(a.doseText) ? a.quantity.ToString("0.##") + a.uom.ToString() + " - " + a.frequency.ToString() + " (" + a.instruction.ToString() + ") " + a.route.ToString() : a.doseText
                                            }).ToList();
        var repeater2 = (Repeater)e.Item.FindControl("rptCompDetail");
        repeater2.DataSource = listcompdetail;
        repeater2.DataBind();
    }
}