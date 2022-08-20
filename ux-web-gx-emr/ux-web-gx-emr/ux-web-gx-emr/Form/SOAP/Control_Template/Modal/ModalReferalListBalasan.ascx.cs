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

public partial class Form_SOAP_Control_Template_Modal_ModalReferalListBalasan : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }


    public void initializevalue(PatientReferralBalasan data)
    {
        //List<PatientReferral> patientReferralsSelf = data.Where(y => y.IsSelf == 1).ToList();
        //List<PatientReferral> patientReferralsOther = data.Where(y => y.IsSelf == 0).ToList();
        if (data.referrals != null)
        {
            data.referrals = data.referrals.OrderByDescending(x => x.IsSelf).ToList();
            rptReferralList.DataSource = Helper.ToDataTable(data.referrals);
            rptReferralList.DataBind();
        }

        if(data.balasan != null)
        {
            //if (data.referrals != null)
            //{
            //    foreach (var col in data.balasan)
            //    {
            //        col.DoctorNameOri = data.referrals[0].DoctorName;
            //    }
            //}
            foreach (var col in data.balasan)
            {
                col.DoctorNameOri = MyUser.GetFullname();
            }

            LabelNotif.Text = data.balasan.Count().ToString();

            rptBalasanList.DataSource = Helper.ToDataTable(data.balasan);
            rptBalasanList.DataBind();
        }
    }


}

