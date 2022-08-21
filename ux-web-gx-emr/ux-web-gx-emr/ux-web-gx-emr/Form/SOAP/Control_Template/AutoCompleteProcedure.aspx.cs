using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_SOAP_Control_Template_AutoCompleteProcedure : System.Web.UI.Page
{
    private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string term = Request.QueryString["term"];

        Response.Clear();
        Response.ContentType = "application/json; charset=utf-8";

        DataTable dt = new DataTable();
        List<procedureModel> filterSuggestions = new List<procedureModel>();

        if (term == "")
        {
            dt = ((DataTable)Session[Helper.SessionProcedureInpatientData]).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                procedureModel x = new procedureModel();
                x.itemProcedureId = dt.Rows[i]["operationprocedure_id"].ToString();
                x.itemProcedureName = dt.Rows[i]["procedure_name"].ToString();
                x.itemProcedureDiagnosis = dt.Rows[i]["diagnosis"].ToString();
             
                filterSuggestions.Add(x);
            }
        }
        else
        {
            try
            {
                dt = ((DataTable)Session[Helper.SessionProcedureInpatientData]).Select("procedure_name like '%" + term + "%'").Take(100).CopyToDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    procedureModel x = new procedureModel();
                    x.itemProcedureId = dt.Rows[i]["operationprocedure_id"].ToString();
                    x.itemProcedureName = dt.Rows[i]["procedure_name"].ToString();
                    x.itemProcedureDiagnosis = dt.Rows[i]["diagnosis"].ToString();

                    filterSuggestions.Add(x);
                }
            }
            catch
            {
                procedureModel x = new procedureModel();
                x.itemProcedureId = "";
                x.itemProcedureName = "No Match Found";
                x.itemProcedureDiagnosis = "";

                filterSuggestions.Add(x);
            }
        }

        string responseJson = JsonConvert.SerializeObject(filterSuggestions);

        Response.Write(responseJson);
        Response.End();

        Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "UserID", MyUser.GetHopeUserID(), "Page_Load", StartTime, "OK", MyUser.GetUsername(), "", "", ""));
    }

    public class procedureModel
    {
        public string itemProcedureId;
        public string itemProcedureName;
        public string itemProcedureDiagnosis;
    }
}