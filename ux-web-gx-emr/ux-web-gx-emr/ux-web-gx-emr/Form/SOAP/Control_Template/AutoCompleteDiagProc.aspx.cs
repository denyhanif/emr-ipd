using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_SOAP_Control_Template_AutoCompleteDiagProc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string term = Request.QueryString["term"];
        string type = Request.QueryString["type"];

        Response.Clear();
        Response.ContentType = "application/json; charset=utf-8";

        DataTable dt = new DataTable();
        List<DPModel> filtersuggestions = new List<DPModel>();

        if (term == "")
        {
            dt = ((DataTable)Session[Helper.SessionItemDiagProc]).Select("SalesItemTypeId = '" + type + "' ").Take(100).CopyToDataTable();
            //dt = ((DataTable)Session[Helper.SessionItemDiagProc]).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DPModel x = new DPModel();
                x.SalesItemId = dt.Rows[i]["SalesItemId"].ToString();
                x.SalesItemTypeId = dt.Rows[i]["SalesItemTypeId"].ToString();
                x.SalesItemCode = dt.Rows[i]["SalesItemCode"].ToString();
                x.SalesItemName = dt.Rows[i]["SalesItemName"].ToString();
               
                filtersuggestions.Add(x);
            }
        }
        else
        {
            try
            {
                dt = ((DataTable)Session[Helper.SessionItemDiagProc]).Select("SalesItemTypeId = " + type + " and (SalesItemName like '%" + term + "%' or SalesItemCode like '%" + term + "%')").Take(100).CopyToDataTable();
                //dt = ((DataTable)Session[Helper.SessionItemDiagProc]).Select("SalesItemTypeId = '" + type + "' and (SalesItemName like '%" + term + "%')" + "%' or SalesItemCode like '%").Take(100).CopyToDataTable();
                //dt = ((DataTable)Session[Helper.SessionItemDiagProc]).Select("SalesItemName like '%" + term + "%' or SalesItemCode like '%" + term + "%'").Take(100).CopyToDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DPModel x = new DPModel();
                    x.SalesItemId = dt.Rows[i]["SalesItemId"].ToString();
                    x.SalesItemTypeId = dt.Rows[i]["SalesItemTypeId"].ToString();
                    x.SalesItemCode = dt.Rows[i]["SalesItemCode"].ToString();
                    x.SalesItemName = dt.Rows[i]["SalesItemName"].ToString();

                    filtersuggestions.Add(x);
                }
            }
            catch
            {
                DPModel x = new DPModel();
                x.SalesItemId = "";
                x.SalesItemTypeId = "";
                x.SalesItemCode = "";
                x.SalesItemName = "No Match Found";

                filtersuggestions.Add(x);
            }
        }

        string responseJson = JsonConvert.SerializeObject(filtersuggestions);

        Response.Write(responseJson);
        Response.End();
    }

    public class DPModel
    {
        public string SalesItemId;
        public string SalesItemTypeId;
        public string SalesItemCode;
        public string SalesItemName;
    }
}