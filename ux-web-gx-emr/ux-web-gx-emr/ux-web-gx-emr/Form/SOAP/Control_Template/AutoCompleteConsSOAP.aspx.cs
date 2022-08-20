using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_SOAP_Control_Template_AutoCompleteConsSOAP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string term = Request.QueryString["term"];

        Response.Clear();
        Response.ContentType = "application/json; charset=utf-8";

        DataTable dt = new DataTable();
        List<consModel> filtersuggestions = new List<consModel>();

        if (term == "")
        {
            dt = ((DataTable)Session[Helper.SessionDrugsConsumables]).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                consModel y = new consModel();
                y.itemId = dt.Rows[i]["salesItemId"].ToString();
                y.itemName = dt.Rows[i]["salesItemName"].ToString();
                var qty = decimal.Parse(dt.Rows[i]["totalQuantity"].ToString());
                y.itemQuantity = String.Format("{0:#,##0.00}", qty);
                filtersuggestions.Add(y);
            }
        }
        else
        {
            try
            {
                dt = ((DataTable)Session[Helper.SessionDrugsConsumables]).Select("salesItemName like '%" + term + "%' or activeIngredientsName like '%" + term + "%'").Take(100).CopyToDataTable();
               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    consModel y = new consModel();
                    y.itemId = dt.Rows[i]["salesItemId"].ToString();
                    y.itemName = dt.Rows[i]["salesItemName"].ToString();
                    var qty = decimal.Parse(dt.Rows[i]["totalQuantity"].ToString());
                    y.itemQuantity = String.Format("{0:#,##0.00}", qty);
                    filtersuggestions.Add(y);
                }
            }
            catch
            {
                consModel y = new consModel();
                y.itemId = "";
                y.itemName = "No Match Found";
                y.itemQuantity = "";
                filtersuggestions.Add(y);
            }
        }

        string responseJson = JsonConvert.SerializeObject(filtersuggestions);

        Response.Write(responseJson);
        Response.End();
    }

    public class consModel
    {
        public string itemId;
        public string itemName;
        public string itemQuantity;
    }
}