using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_General_OrderSet_AutoCompleteDrug : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string term = Request.QueryString["term"];

        Response.Clear();
        Response.ContentType = "application/json; charset=utf-8";

        //List<string> suggestions = new List<string>();

        DataTable dt = new DataTable();       
        List<drugsModel> filtersuggestions = new List<drugsModel>();

        if (term == "")
        {
            dt = ((DataTable)Session[Helper.SessionDrug]).Rows.Cast<System.Data.DataRow>().Take(100).CopyToDataTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //filtersuggestions.Add(dt.Rows[i]["salesItemName"].ToString());

                drugsModel x = new drugsModel();
                x.itemId = dt.Rows[i]["salesItemId"].ToString();
                x.itemName = dt.Rows[i]["salesItemName"].ToString();
                x.itemIngredient = dt.Rows[i]["activeIngredientsName"].ToString();
                x.itemFormularium = dt.Rows[i]["Formularium"].ToString();
                filtersuggestions.Add(x);
            }
        }
        else
        {
            try
            {
                dt = ((DataTable)Session[Helper.SessionDrug]).Select("salesItemName like '%" + term + "%' or activeIngredientsName like '%" + term + "%'").Take(100).CopyToDataTable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //filtersuggestions.Add(dt.Rows[i]["salesItemName"].ToString());

                    drugsModel x = new drugsModel();
                    x.itemId = dt.Rows[i]["salesItemId"].ToString();
                    x.itemName = dt.Rows[i]["salesItemName"].ToString();
                    x.itemIngredient = dt.Rows[i]["activeIngredientsName"].ToString();
                    x.itemFormularium = dt.Rows[i]["Formularium"].ToString();
                    filtersuggestions.Add(x);
                }
            }
            catch
            {
                //filtersuggestions.Add("Not Found");

                drugsModel x = new drugsModel();
                x.itemId = "";
                x.itemName = "No Match Found";
                x.itemIngredient = "";
                x.itemFormularium = "";
                filtersuggestions.Add(x);
            }
        }
        
        //suggestions.Add("data satu");
        //suggestions.Add("data dua");
        //suggestions.Add("data tiga");
        //suggestions.Add("data empat");
        //suggestions.Add("data lima");
        
        //if (term != null && term.Length > 0)
        //{
        //    foreach (string data in suggestions)
        //    {
        //        if (data.Contains(term))
        //        {
        //            filtersuggestions.Add(data);
        //        }
        //    }
        //}

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
    }
}