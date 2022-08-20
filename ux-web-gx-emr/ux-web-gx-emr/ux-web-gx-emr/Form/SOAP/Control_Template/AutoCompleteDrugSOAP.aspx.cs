using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_SOAP_Control_Template_AutoCompleteDrugSOAP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string term = Request.QueryString["term"];
        string payer = Request.QueryString["payertype"];
        string chkformula = Request.QueryString["chkformula"];

        Response.Clear();
        Response.ContentType = "application/json; charset=utf-8";

        DataTable dt = new DataTable();
        List<drugsModel> filtersuggestions = new List<drugsModel>();

        if (term == "")
        {
            if (chkformula == "true")
            {
                dt = ((DataTable)Session[Helper.SessionItemDrugPres]).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
            }
            else
            {
                dt = ((DataTable)Session[Helper.SessionItemDrugPres]).Select("Formularium = '" + payer + "' ").Take(100).CopyToDataTable();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                drugsModel x = new drugsModel();
                x.itemId = dt.Rows[i]["salesItemId"].ToString();
                x.itemName = dt.Rows[i]["salesItemName"].ToString();
                x.itemIngredient = dt.Rows[i]["activeIngredientsName"].ToString();
                x.itemFormularium = dt.Rows[i]["Formularium"].ToString();
                var qty = decimal.Parse(dt.Rows[i]["totalQuantity"].ToString());
                x.itemQuantity = String.Format("{0:#,##0.00}", qty);
                filtersuggestions.Add(x);
            }
        }
        else
        {
            try
            {
                if (chkformula == "true")
                {
                    dt = ((DataTable)Session[Helper.SessionItemDrugPres]).Select("salesItemName like '%" + term + "%' or activeIngredientsName like '%" + term + "%'").Take(100).CopyToDataTable();
                }
                else
                {
                    dt = ((DataTable)Session[Helper.SessionItemDrugPres]).Select("Formularium = '" + payer + "' and (salesItemName like '%" + term + "%' or activeIngredientsName like '%" + term + "%')").Take(100).CopyToDataTable();
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    drugsModel x = new drugsModel();
                    x.itemId = dt.Rows[i]["salesItemId"].ToString();
                    x.itemName = dt.Rows[i]["salesItemName"].ToString();
                    x.itemIngredient = dt.Rows[i]["activeIngredientsName"].ToString();
                    x.itemFormularium = dt.Rows[i]["Formularium"].ToString();
                    var qty = decimal.Parse(dt.Rows[i]["totalQuantity"].ToString());
                    x.itemQuantity = String.Format("{0:#,##0.00}", qty);
                    filtersuggestions.Add(x);
                }
            }
            catch
            {
                drugsModel x = new drugsModel();
                x.itemId = "";
                x.itemName = "No Match Found";
                x.itemIngredient = "";
                x.itemFormularium = "";
                x.itemQuantity = "";
                filtersuggestions.Add(x);
            }
        }

        string responseJson = JsonConvert.SerializeObject(filtersuggestions);

        Response.Write(responseJson);
        Response.End();
    }

    public class drugsModel
    {
        public string itemId;
        public string itemName;
        public string itemIngredient;
        public string itemFormularium;
        public string itemQuantity;
    }
}