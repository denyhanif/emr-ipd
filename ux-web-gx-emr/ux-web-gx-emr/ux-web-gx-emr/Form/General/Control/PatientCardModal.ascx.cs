using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static PatientHistory;

public partial class Form_General_Control_PatientCardModal : System.Web.UI.UserControl
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
            HFisBahasaPCM.Value = "ENG";
        }
        else if (Session[Helper.SessionLanguage].ToString() == "2")
        {
            setENG = "none";
            setIND = "";
            HFisBahasaPCM.Value = "IND";
        }
        else
        {
            setENG = "";
            setIND = "none";
            HFisBahasaPCM.Value = "ENG";
        }

        //set bahasa
        ScriptManager.RegisterStartupScript(this, GetType(), "bahasa", "switchBahasaPCM();", true);

        if (!IsPostBack)
        {
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

        //Log.Info(LogConfig.LogEnd());
    }
}