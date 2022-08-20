using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_SOAP_Control_Template_AutoCompleteCPOE : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string term = Request.QueryString["term"];
        string cpoetype = Request.QueryString["cpoetype"];

        Response.Clear();
        Response.ContentType = "application/json; charset=utf-8";

        DataTable dt = new DataTable();
        List<cpoeModel> filtersuggestions = new List<cpoeModel>();

        if (term == "")
        {
            dt = ((DataTable)Session[Helper.SessionItemAllCPOE]).Select("type <> 'CitoLab' AND template_name = '" + cpoetype + "' ").Take(100).CopyToDataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cpoeModel x = new cpoeModel();
                x.itemId = dt.Rows[i]["id"].ToString();
                x.itemName = dt.Rows[i]["name"].ToString();
                x.itemType = dt.Rows[i]["type"].ToString();
                x.itemRemarks = dt.Rows[i]["remarks"].ToString();
               
                filtersuggestions.Add(x);
            }
        }
        else
        {
            try
            {
                dt = ((DataTable)Session[Helper.SessionItemAllCPOE]).Select("type <> 'CitoLab' AND template_name = '" + cpoetype + "' and (name like '%" + term + "%' or type like '%" + term + "%')").Take(100).CopyToDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cpoeModel x = new cpoeModel();
                    x.itemId = dt.Rows[i]["id"].ToString();
                    x.itemName = dt.Rows[i]["name"].ToString();
                    x.itemType = dt.Rows[i]["type"].ToString();
                    x.itemRemarks = dt.Rows[i]["remarks"].ToString();
                   
                    filtersuggestions.Add(x);
                }
            }
            catch
            {
                cpoeModel x = new cpoeModel();
                x.itemId = "";
                x.itemName = "No Match Found";
                x.itemType = "";
                x.itemRemarks = "";

                filtersuggestions.Add(x);
            }
        }

        string responseJson = JsonConvert.SerializeObject(filtersuggestions);

        Response.Write(responseJson);
        Response.End();
    }

    public class cpoeModel
    {
        public string itemId;
        public string itemName;
        public string itemType;
        public string itemRemarks;
    }
}