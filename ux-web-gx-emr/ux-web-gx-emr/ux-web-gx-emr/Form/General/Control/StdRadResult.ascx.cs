using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_General_Control_StdRadResult : System.Web.UI.UserControl
{
    //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        //log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgID();
    }

    public void initializevalue(List<radiologyByWeek> listAdmissionDetail)
    {
        //Log.Info(LogConfig.LogStart());

        StringBuilder radiologyInnerHTML = new StringBuilder();
        string imagePath = ResolveClientUrl("~/Images/PatientHistory/ic_newtab_blue.svg");
        if (listAdmissionDetail.Count != 0)
        {
            List<String> listDate = listAdmissionDetail.Select(x => String.Format("{0:dd MMM yyyy}", x.admissionDate)).Distinct().ToList();

            foreach (String data in listDate)
            {
                List<radiologyByWeek> listHistoryByDate = listAdmissionDetail.FindAll(x => data.Equals(String.Format("{0:dd MMM yyyy}", x.admissionDate)));

                radiologyInnerHTML.Append("<div class=\"sm-col-12\" style=\"background-color:#e7e8ef; font-size:14px; height: 35px;padding-top: 10px;\"><b><div class=\"container-fluid\">" + data + " - " + listHistoryByDate[0].orgCd + " - " + listHistoryByDate[0].doctorName + "</div></b></div> &nbsp;");
                foreach (radiologyByWeek dataRadiology in listHistoryByDate.OrderByDescending(x => x.admissionNo))
                {
                    radiologyInnerHTML.Append("<div class=\"sm-col-12\">" +
                                                    "<div class=\"container-fluid\"><b style=\"margin-right: 3px;\"><a href=\"" + dataRadiology.imageUrl + "\" style=\"color: blue; text-decoration:underline; \" target=\"_blank\">" + dataRadiology.salesItemName + "</a></b><span><img src=\"" + imagePath + "\" style=\"width: 14px; padding-bottom: 2px;\" /></span></div>" +
                                                    "<div class=\"container-fluid\">" + dataRadiology.responseMessage + "</div>" +
                                                "</div> &nbsp;");
                }
            }
            div_Radiology_detail.InnerHtml = radiologyInnerHTML.ToString();
            img_noData.Style.Add("display", "none");
        }
        else
        {
            div_Radiology_detail.InnerHtml = radiologyInnerHTML.ToString();
            img_noData.Style.Add("display", "block");
        }

        //Log.Info(LogConfig.LogEnd());
    }
}