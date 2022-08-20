using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;

public partial class Form_SOAP_PreviewTemplate_ResumePreview : System.Web.UI.UserControl
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
            DataTable dt = Helper.ToDataTable(JsongetPatientHistory.list.resumeprescription);

            #region ChiefComplain
            lblChiefComplaint.Text = (from a in resumedata
                                      where a.MappingId == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf")
                                      select a.Remarks).SingleOrDefault().ToString();
            #endregion

            #region Anamnesis
            Anamnesis.Text = (from a in resumedata
                              where a.MappingId == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0")
                              select a.Remarks).SingleOrDefault().ToString();
            #endregion

            #region Illness History
            IllnessHistory.DataSource = null;
            var illness = (from a in resumedata
                           where a.MappingId == Guid.Parse("1979DDCB-33BC-4187-BE92-04FBDB0A50D6")
                           select new { remarks = a.Remarks }).ToList();

            IllnessHistory.DataSource = illness;
            IllnessHistory.DataBind();

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
                        eye.Text = "None";
                        break;
                    case "2":
                        eye.Text = "To Pressure";
                        break;
                    case "3":
                        eye.Text = "To Sound";
                        break;
                    case "4":
                        eye.Text = "Spontaneus";
                        break;
                }
            }

            if (gcsMove != "")
            {
                switch (gcsMove)
                {
                    case "1":
                        move.Text = "None";
                        break;
                    case "2":
                        move.Text = "Extension";
                        break;
                    case "3":
                        move.Text = "Flexion to pain stumulus";
                        break;
                    case "4":
                        move.Text = "Withdrawns from pain";
                        break;
                    case "5":
                        move.Text = "Localizes to pain stimulus";
                        break;
                    case "6":
                        move.Text = "Obey Commands";
                        break;
                }
            }

            if (gcsVerbal != "")
            {
                switch (gcsVerbal)
                {
                    case "1":
                        verbal.Text = "None";
                        break;
                    case "2":
                        verbal.Text = "Incomprehensible sounds";
                        break;
                    case "3":
                        verbal.Text = "Inappropriate words";
                        break;
                    case "4":
                        verbal.Text = "Confused";
                        break;
                    case "5":
                        verbal.Text = "Orientated";
                        break;
                    case "T":
                        verbal.Text = "Tracheostomy";
                        break;
                    case "A":
                        verbal.Text = "Aphasia";
                        break;
                }
            }

            bloodpreassure.Text = (from a in resumedata
                                   where a.MappingId == Guid.Parse("E5EFD220-B68E-4652-AD03-D56EF29FCEBB")
                                   select a.Value).SingleOrDefault().ToString() + "/ " +
                                   (from a in resumedata
                                    where a.MappingId == Guid.Parse("AE3CA8C2-EAB0-41B6-9E3E-3ECB8071A9D0")
                                    select a.Value).SingleOrDefault().ToString();

            pulse.Text = (from a in resumedata
                          where a.MappingId == Guid.Parse("78cbb61f-4a11-470a-b770-1a44eb04357f")
                          select a.Value).SingleOrDefault().ToString();

            respiratory.Text = (from a in resumedata
                                where a.MappingId == Guid.Parse("e6ae2ea9-b321-4756-bf96-2dc232e4a7ba")
                                select a.Value).SingleOrDefault().ToString();

            spo.Text = (from a in resumedata
                        where a.MappingId == Guid.Parse("e903246c-df95-4fe0-96d2-cf90c036d3d7")
                        select a.Value).SingleOrDefault().ToString();

            temperature.Text = (from a in resumedata
                                where a.MappingId == Guid.Parse("2eeca752-a2ea-4426-b3cf-c1ea3833bf30")
                                select a.Value).SingleOrDefault().ToString();

            weight.Text = (from a in resumedata
                           where a.MappingId == Guid.Parse("52ce9350-bfb2-4072-8893-d0c6cf8b3634")
                           select a.Value).SingleOrDefault().ToString();

            height.Text = (from a in resumedata
                           where a.MappingId == Guid.Parse("2a8dbddb-edfe-4704-876e-5a2d735bb541")
                           select a.Value).SingleOrDefault().ToString();

            painscore.Text = (from a in resumedata
                              where a.MappingId == Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176")
                              select a.Value).SingleOrDefault().ToString();


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
                              select a.Remarks).SingleOrDefault().ToString();
            #endregion

            #region diagnosis
            primarydiagnosis.Text = (from a in resumedata
                                     where a.MappingId == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")
                                     select a.Remarks).SingleOrDefault().ToString();
            #endregion

            #region prescription
            prescriptiondrugs.DataSource = null;
            prescriptioncompound.DataSource = null;
            prescriptionconsumables.DataSource = null;
            List<ResumeDrugs> listpres = (from a in JsongetPatientHistory.list.resumeprescription
                                          where a.compound_id == Guid.Empty && a.isConsumables == false
                                          select new ResumeDrugs
                                          {
                                              salesItemName = a.salesItemName,
                                              Remarks = string.IsNullOrEmpty(a.doseText) ? a.quantity.ToString("0.###") + a.uom.ToString() + " - " + a.frequency.ToString() + " (" + a.instruction.ToString() + ") " + a.route.ToString() : a.doseText
                                          }).ToList();
            DataTable dttemp = Helper.ToDataTable(JsongetPatientHistory.list.resumeprescription);

            if (listpres.Count > 0)
            {
                drugs.Visible = false;
                prescriptiondrugs.DataSource = listpres;
                prescriptiondrugs.DataBind();
            }
            else
            {
                drugs.Visible = true;
                prescriptiondrugs.DataSource = null;
                prescriptiondrugs.DataBind();
            }

            List<ResumeDrugs> listcompheader = (from a in JsongetPatientHistory.list.resumeprescription
                                                where a.salesItemName == "" && a.isConsumables == false
                                                select new ResumeDrugs
                                                {
                                                    salesItemName = a.compound_name,
                                                    Remarks = string.IsNullOrEmpty(a.doseText) ? a.quantity.ToString("0.###") + a.uom.ToString() + " - " + a.frequency.ToString() + " (" + a.instruction.ToString() + ") " + a.route.ToString() : a.doseText
                                                }).ToList();

            if (listcompheader.Count > 0)
            {
                compound.Visible = false;
                prescriptioncompound.DataSource = listcompheader;
                prescriptioncompound.DataBind();
            }
            else
            {
                compound.Visible = true;
                prescriptioncompound.DataSource = null;
                prescriptioncompound.DataBind();
            }

            List<ResumeDrugs> listcons = (from a in JsongetPatientHistory.list.resumeprescription
                                          where a.isConsumables == true
                                          select new ResumeDrugs
                                          {
                                              salesItemName = a.salesItemName,
                                              Remarks = string.IsNullOrEmpty(a.doseText) ? a.quantity.ToString("0.###") + a.uom.ToString() + " - " + a.frequency.ToString() + " (" + a.instruction.ToString() + ") " + a.route.ToString() : a.doseText
                                          }).ToList();

            if (listcons.Count > 0)
            {
                cons.Visible = false;
                prescriptionconsumables.DataSource = listcons;
                prescriptionconsumables.DataBind();
            }
            else
            {
                cons.Visible = true;
                prescriptionconsumables.DataSource = null;
                prescriptionconsumables.DataBind();
            }

            #endregion

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", EncounterId.ToString() , "initializevalue", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));

        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", EncounterId.ToString() , "initializevalue", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var data = ((ResumeDrugs)e.Item.DataItem).salesItemName;
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