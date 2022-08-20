using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Reflection;

public partial class Form_SOAP_Control_Template_AutoCompleteICD : System.Web.UI.Page
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string term = Request.QueryString["term"];

        Response.Clear();
        Response.ContentType = "application/json; charset=utf-8";

        DataTable dt = new DataTable();
        List<icdModel> filtersuggestions = new List<icdModel>();

        if (term == "")
        {
            DataTable vardiseaseclassification = clsSOAP.getDiseaseClassification("");
            vardiseaseclassification.DefaultView.Sort = "DiseaseClassification";
            dt = vardiseaseclassification;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                icdModel z = new icdModel();
                z.itemId = dt.Rows[i]["DiseaseClassificationid"].ToString();
                z.itemName = dt.Rows[i]["DiseaseClassification"].ToString();
                filtersuggestions.Add(z);
            }
        }
        else
        {
            try
            {
                DataTable vardiseaseclassification = clsSOAP.getDiseaseClassification(term);
                vardiseaseclassification.DefaultView.Sort = "DiseaseClassification";
                dt = vardiseaseclassification;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    icdModel z = new icdModel();
                    z.itemId = dt.Rows[i]["DiseaseClassificationid"].ToString();
                    z.itemName = dt.Rows[i]["DiseaseClassification"].ToString();
                    filtersuggestions.Add(z);
                }
            }
            catch
            {
                icdModel z = new icdModel();
                z.itemId = "";
                z.itemName = "No Match Found";
                filtersuggestions.Add(z);
            }
        }

        string responseJson = JsonConvert.SerializeObject(filtersuggestions);

        Response.Write(responseJson);
        Response.End();

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "UserID", MyUser.GetHopeUserID(), "Page_Load", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
    }

    public class icdModel
    {
        public string itemId;
        public string itemName;
    }
}