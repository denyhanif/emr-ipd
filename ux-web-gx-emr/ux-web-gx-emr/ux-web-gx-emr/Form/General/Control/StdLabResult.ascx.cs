using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static PatientHistory;
using log4net;
using System.Text;

public partial class Form_General_Control_StdLabResult : System.Web.UI.UserControl
{
    //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    Panel tempPanel = new Panel();

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
            HFisBahasax.Value = "ENG";
        }
        else if (Session[Helper.SessionLanguage].ToString() == "2")
        {
            setENG = "none";
            setIND = "";
            HFisBahasax.Value = "IND";
        }
        else
        {
            setENG = "";
            setIND = "none";
            HFisBahasax.Value = "ENG";
        }

        //set bahasa
        ScriptManager.RegisterStartupScript(this, GetType(), "bahasa", "switchBahasax();", true);

        if (!IsPostBack) { }
    }

    public void initializevalue(List<LaboratoryResult> listlaboratory)
    {
        //Log.Info(LogConfig.LogStart());

        panel1.InnerHtml = "";
        //try {

            StringBuilder labHistory = new StringBuilder();

            if (listlaboratory.Count != 0)
            {
                if (listlaboratory[0].ConnStatus == "OFFLINE")
                {
                    img_noConnection.Visible = true;
                    img_noData.Style.Add("display", "none");
                }
                else
                {
                    img_noData.Style.Add("display", "none");

                    List<Int64?> listAdmissionId = listlaboratory.Select(x => x.admissionId).Distinct().ToList();
                    List<gridLaboratory> gridLaboratoryResult = new List<gridLaboratory>();
                    DataTable dt = new DataTable();
                    foreach (Int64? dataAdmission in listAdmissionId)
                    {
                        var admissionData = listlaboratory.Find(x => x.admissionId == dataAdmission);
                        List<String> listOno = listlaboratory.FindAll(x => x.admissionId == dataAdmission && x.IsHeader == 1).Select(x => x.ono).Distinct().ToList();

                    var linkterlampir = admissionData.Path == "" ? "" : "<a style=\"color:#2A3593; font-weight:bold; margin-left:15px; font-size:16px; text-decoration:underline;\" href=\"" + admissionData.Path + "\">Hasil Terlampir <i class=\"fa fa-external-link\"></i></a>";

                    labHistory.Append("<div style=\"color:#4D9B35; border-bottom:1px solid #cdd2dd; border-top:1px solid #cdd2dd;\"><h4>" + String.Format("{0:ddd, MMM d, yyyy}", admissionData.admissionDate) + " - " + admissionData.cliniciaN_NM + linkterlampir + "</h4></div>" +
                            "<div><table class=\"table table-striped table-condensed\">" +
                            "<tr>" +
                                "<td><b>Test</b></td>" +
                                "<td></td>" +
                                "<td><b>Hasil</b></td>" +
                                "<td><b>Unit</b></td>" +
                                "<td><b>Nilai Rujukan</b></td> " +
                                "<td></td>" +
                            "</tr>");
                        if (listOno.Count != 0)
                        {
                            foreach (String data in listOno)
                            {
                                List<String> testGrop = listlaboratory.FindAll(x => x.admissionId == dataAdmission && x.ono == data && x.IsHeader == 1).Select(x => x.tesT_GROUP).Distinct().ToList();

                                foreach (String dataTestGroup in testGrop)
                                {
                                    labHistory.Append("<tr style=\"background-color:lightgray;\"><td><h7><b>" + dataTestGroup + "</b></h7></td><td></td><td></td><td></td><td></td><td></td>" +
                                        //"<td></td>" +
                                        //"<td></td>" +
                                        "</tr>");

                                    gridLaboratoryResult = (
                                    from a in listlaboratory
                                    where a.admissionId == dataAdmission && a.ono == data && a.tesT_GROUP == dataTestGroup
                                    select new gridLaboratory
                                    {
                                        tesT_NM = a.tesT_NM,
                                        resulT_VALUE = a.resulT_VALUE,
                                        unit = a.unit,
                                        reF_RANGE = a.reF_RANGE,
                                        ono = a.ono,
                                        dis_sq = a.disP_SEQ,
                                        IsHeader = a.IsHeader,
                                        Flag = a.flag,
                                        tesT_COMMENT = a.tesT_COMMENT,
                                    }).ToList();


                                    foreach (gridLaboratory dataLab in gridLaboratoryResult)
                                    {
                                        string flagcomment = "none";
                                        if (dataLab.tesT_COMMENT.Length > 0)
                                        {
                                            flagcomment = "block";
                                        }

                                        if (dataLab.IsHeader == 1)
                                        {
                                            if (dataLab.Flag == "H" || dataLab.Flag == "HH" || dataLab.Flag == "L" || dataLab.Flag == "LL")
                                            {
                                                //string LH = LowHighChecker(dataLab.resulT_VALUE, dataLab.reF_RANGE).ToString();

                                                labHistory.Append("<tr><td style=\"width:30%\"><h7><b>" + dataLab.tesT_NM + "</b></h7></td>" +
                                                                "<td style=\"width:5%; color:red; font-weight:bold;\"><h7>" + dataLab.Flag + "</h7></td>" +
                                                                "<td style=\"width:20%; color:red; font-weight:bold;\"><h7>" + dataLab.resulT_VALUE + "</h7></td>" +
                                                                "<td style=\"width:20%; color:red; font-weight:bold;\"><h7>" + dataLab.unit + "</h7></td>" +
                                                                "<td style=\"width:20%\"><h7>" + dataLab.reF_RANGE + "</h7></td>" +
                                                                "<td style=\"width:5%\"><h7>" + "<a title=\"Test Comment\" href=\"javascript:ViewComment('" + dataLab.tesT_NM.Replace(" ", "_") + "','" + dataLab.tesT_COMMENT.Replace("\\n", "<br/>").Replace(" ", "_") + "');\" style=\"color:#9d1fc3; font-weight:bold; text-decoration: underline; display:" + flagcomment + "\">Detail</a>" + "</h7></td>" +
                                                                //"<td><h7><b>" + dataLab.ono + "</b></h7></td>" +
                                                                //"<td><h7><b>" + dataLab.dis_sq + "</b></h7></td>" +
                                                                "</tr>");
                                            }
                                            else //dataLab.Flag == "*" || flag DC || flag N || flag kosong
                                            {
                                                labHistory.Append("<tr><td style=\"width:30%\"><h7><b>" + dataLab.tesT_NM + "</b></h7></td>" +
                                                               "<td style=\"width:5%; color:black;\"><h7>" + "" + "</h7></td>" +
                                                               "<td style=\"width:20%; color:black;\"><h7>" + dataLab.resulT_VALUE + "</h7></td>" +
                                                               "<td style=\"width:20%; color:black;\"><h7>" + dataLab.unit + "</h7></td>" +
                                                               "<td style=\"width:20%\"><h7>" + dataLab.reF_RANGE + "</h7></td>" +
                                                                "<td style=\"width:5%\"><h7>" + "<a title=\"Test Comment\" href=\"javascript:ViewComment('" + dataLab.tesT_NM.Replace(" ", "_") + "','" + dataLab.tesT_COMMENT.Replace("\\n", "<br/>").Replace(" ", "_") + "');\" style=\"color:#9d1fc3; font-weight:bold; text-decoration: underline; display:" + flagcomment + "\">Detail</a>" + "</h7></td>" +
                                                               //"<td><h7><b>" + dataLab.ono + "</b></h7></td>" +
                                                               //"<td><h7><b>" + dataLab.dis_sq + "</b></h7></td>" +
                                                               "</tr>");
                                            }
                                        }
                                        else
                                        {
                                            if (dataLab.Flag == "H" || dataLab.Flag == "HH" || dataLab.Flag == "L" || dataLab.Flag == "LL")
                                            {
                                                //string LH = LowHighChecker(dataLab.resulT_VALUE, dataLab.reF_RANGE).ToString();

                                                labHistory.Append("<tr><td style=\"width:30%\">" + dataLab.tesT_NM + "</td>" +
                                                                "<td style=\"width:5%; color:red; font-weight:bold;\"><h7>" + dataLab.Flag + "</h7></td>" +
                                                                "<td style=\"width:20%; color:red; font-weight:bold;\">" + dataLab.resulT_VALUE + "</td>" +
                                                                "<td style=\"width:20%; color:red; font-weight:bold;\">" + dataLab.unit + "</td>" +
                                                                "<td style=\"width:20%\">" + dataLab.reF_RANGE + "</td>" +
                                                                "<td style=\"width:5%\"><h7>" + "<a title=\"Test Comment\" href=\"javascript:ViewComment('" + dataLab.tesT_NM.Replace(" ", "_") + "','" + dataLab.tesT_COMMENT.Replace("\\n", "<br/>").Replace(" ", "_") + "');\" style=\"color:#9d1fc3; font-weight:bold; text-decoration: underline; display:" + flagcomment + "\">Detail</a>" + "</h7></td>" +
                                                                //"<td>" + dataLab.ono + "</td>" +
                                                                //"<td>" + dataLab.dis_sq + "</td>" +
                                                                "</tr>");
                                            }
                                            else //dataLab.Flag == "*" || flag DC || flag N || flag kosong
                                            {
                                                labHistory.Append("<tr><td style=\"width:30%\">" + dataLab.tesT_NM + "</td>" +
                                                                "<td style=\"width:5%; color:black;\"><h7>" + "" + "</h7></td>" +
                                                                "<td style=\"width:20%; color:black;\">" + dataLab.resulT_VALUE + "</td>" +
                                                                "<td style=\"width:20%; color:black;\">" + dataLab.unit + "</td>" +
                                                                "<td style=\"width:20%\">" + dataLab.reF_RANGE + "</td>" +
                                                                 "<td style=\"width:5%\"><h7>" + "<a title=\"Test Comment\" href=\"javascript:ViewComment('" + dataLab.tesT_NM.Replace(" ", "_") + "','" + dataLab.tesT_COMMENT.Replace("\\n", "<br/>").Replace(" ", "_") + "');\" style=\"color:#9d1fc3; font-weight:bold; text-decoration: underline; display:" + flagcomment + "\">Detail</a>" + "</h7></td>" +
                                                                //"<td>" + dataLab.ono + "</td>" +
                                                                //"<td>" + dataLab.dis_sq + "</td>" +
                                                                "</tr>");
                                            }
                                        }
                                    }
                                    gridLaboratoryResult = new List<gridLaboratory>();
                                }
                            }
                            labHistory.Append("</table></div>");
                        }
                    }
                }
            }
            else
            {
                img_noData.Style.Add("display", "");
            }
            
            panel1.InnerHtml = labHistory.ToString();
            //Log.Info(LogConfig.LogEnd());

        //}
        //catch (Exception ex)
        //{
        //    Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        //}


    }

    public string LowHighChecker(string value, string range)
    {
        var rangedata = range.Split('-');
        if (rangedata != null && rangedata.Count() > 1)
        {
            try
            {
                decimal rmin = decimal.Parse(rangedata[0].ToString().Replace(".", ","));
                decimal rmax = decimal.Parse(rangedata[1].ToString().Replace(".", ","));
                decimal rval = decimal.Parse(value.Replace(".", ","));

                if (rval > rmax)
                {
                    return "H";
                }
                else if (rval < rmin)
                {
                    return "L";
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        else
        {
            return "";
        }

    }

}