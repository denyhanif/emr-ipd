using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using static PatientReferralModel;
using static SPDocument;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Web.Services;

public partial class Form_SOAP_Control_Template_Modal_ModalReferalList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }


    public void initializevalue(List<PatientReferral> data)
    {
        //List<PatientReferral> patientReferralsSelf = data.Where(y => y.IsSelf == 1).ToList();
        //List<PatientReferral> patientReferralsOther = data.Where(y => y.IsSelf == 0).ToList();
        data = data.OrderByDescending(x => x.IsSelf).ToList();
        rptReferralList.DataSource = Helper.ToDataTable(data);
        rptReferralList.DataBind();
    }


}

