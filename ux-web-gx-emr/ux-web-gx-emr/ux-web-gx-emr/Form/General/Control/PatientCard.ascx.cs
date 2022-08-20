using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static PatientHistory;

public partial class Form_General_Control_PatientCard : System.Web.UI.UserControl
{
    //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public string setENG = "none";
    public string setIND = "none";

    protected void Page_Load(object sender, EventArgs e)
    {
        //log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgID();

        if (Session[Helper.SessionLanguage] == null)
            Session[Helper.SessionLanguage] = 1;

        if (Session[Helper.SessionLanguage].ToString() == "1")
        {
            setENG = "";
            setIND = "none";
            HFisBahasaPC.Value = "ENG";
        }
        else if (Session[Helper.SessionLanguage].ToString() == "2")
        {
            setENG = "none";
            setIND = "";
            HFisBahasaPC.Value = "IND";
        }
        else
        {
            setENG = "";
            setIND = "none";
            HFisBahasaPC.Value = "ENG";
        }

        //set bahasa
        ScriptManager.RegisterStartupScript(this, GetType(), "bahasa", "switchBahasaPC();", true);

        if (!IsPostBack)
        {
            /*
            if (Request.QueryString["idPatient"] != null && Request.QueryString["AdmissionId"] != null && Request.QueryString["EncounterId"] != null)
            {
                long idPatient = long.Parse(Request.QueryString["idPatient"]);
                long AdmissionId = long.Parse(Request.QueryString["AdmissionId"]);
                string EncounterId = Request.QueryString["EncounterId"];
                int organization_id = int.Parse(MyUser.GetHopeOrgID());

                setSticker(idPatient, AdmissionId, EncounterId, organization_id);
            }
            */
        }
    }

    public void initializevalue(PatientHeader model)
    {
        //Log.Info(LogConfig.LogStart());

        if (model.Gender == 1)
        {
            Image1.ImageUrl = "~/Images/Dashboard/ic_PatientMale_Big.svg";
            imgSex.ImageUrl = "~/Images/Icon/ic_Male.svg";
        }
        else if (model.Gender == 2)
        {
            Image1.ImageUrl = "~/Images/Dashboard/ic_PatientFemale_Big.svg";
            imgSex.ImageUrl = "~/Images/Icon/ic_Female.svg";
        }

        patientName.Text = model.PatientName;
        localMrNo.Text = model.MrNo;
        primaryDoctor.Text = model.DoctorName;
        lblAdmissionNo.Text = model.AdmissionNo;
        lblDOB.Text = model.BirthDate.ToString("dd MMM yyyy");
        lblAge.Text = clsCommon.GetAge(model.BirthDate);
        lblReligion.Text = model.Religion;
        lblPayer.Text = model.PayerName;
        lblPatientType.Text = model.PatientTypeName;
        if (model.IsFAL == 1)
        {
            ImageJatuhSticker_new.Style.Add("display","");
        }

        /*
        if (model.IsFAL == 1)
        {
            ImageJatuhSticker.Visible = true;
        }
        if (model.IsALG == 1)
        {
            ImageAllergySticker.Visible = true;
        }
        if (model.IsHBS == 1)
        {
            ImageHepBSticker.Visible = true;
        }
        if (model.IsHCS == 1)
        {
            ImageHepCSticker.Visible = true;
        }
        if (model.IsTBC == 1)
        {
            ImageTBCSticker.Visible = true;
        }
        if (model.IsHAD == 1)
        {
            ImageHADSticker.Visible = true;
        }
        if (model.IsPRT == 1)
        {
            ImagePRTSticker.Visible = true;
        }
        if (model.IsRHN == 1)
        {
            ImageRHNSticker.Visible = true;
        }
        if (model.IsMRS == 1)
        {
            ImageMRSSticker.Visible = true;
        }
        if (model.IsCOVID == 1)
        {
            ImageCVSticker.Visible = true;
        }
        if (model.IsCovidVac == 1)
        {
            ImageCVVaccineSticker.Visible = true;
        }
        */
        Session[CstSession.SessionPatientHeader] = model;

        //Log.Info(LogConfig.LogEnd());
    }

    /*
    public void setSticker(long PatientId, long AdmissionId, string EncounterId, int OrgId)
    {
        var DataSticker1 = clsHealthInfo.GetStickerHealthInfo1(PatientId);
        var StickerJson1 = JsonConvert.DeserializeObject<responsePatientHealthInfoFlag>(DataSticker1.Result.ToString());

        var DataSticker2 = clsHealthInfo.GetStickerHealthInfo2(OrgId, PatientId, AdmissionId, EncounterId);
        var StickerJson2 = JsonConvert.DeserializeObject<responseSingleConsent>(DataSticker2.Result.ToString());

        PatientHealthInfoFlag Sticker1 = new PatientHealthInfoFlag();
        Sticker1 = StickerJson1.Data;

        SingleConsent Sticker2 = new SingleConsent();
        Sticker2 = StickerJson2.Data;

        //if (Sticker1.IsFAL == true)
        //{
        //    ImageJatuhSticker.Visible = true;
        //}
        if (Sticker1.IsALG == true)
        {
            ImageAllergySticker.Visible = true;
        }
        if (Sticker1.IsHBS == true)
        {
            ImageHepBSticker.Visible = true;
        }
        if (Sticker1.IsHCS == true)
        {
            ImageHepCSticker.Visible = true;
        }
        if (Sticker1.IsTBC == true)
        {
            ImageTBCSticker.Visible = true;
        }
        if (Sticker1.IsHAD == true)
        {
            ImageHADSticker.Visible = true;
        }
        if (Sticker1.IsPRT == true)
        {
            ImagePRTSticker.Visible = true;
        }
        if (Sticker1.IsRHN == true)
        {
            ImageRHNSticker.Visible = true;
        }
        if (Sticker1.IsMRS == true)
        {
            ImageMRSSticker.Visible = true;
        }
        if (Sticker1.IsCOVID == true)
        {
            ImageCVSticker.Visible = true;
        }
        if (Sticker2.is_covidvac == true)
        {
            ImageCVVaccineSticker.Visible = true;
        }
    }
    */

}