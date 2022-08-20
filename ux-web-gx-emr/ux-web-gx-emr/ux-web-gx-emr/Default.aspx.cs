using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<ItemLite> dataItem = new List<ItemLite>();

        if (!IsPostBack)
        {
            Response.Redirect("~/Form/General/Login.aspx", true);
            Context.ApplicationInstance.CompleteRequest();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("casenumber");

            ////dt.Rows.Add(new Object[] { "DENTIST" });
            //dt.Rows.Add(new Object[] { "" });

            //Gridview2.DataSource = dt;
            //Gridview2.DataBind();

            //var itemData = clsOrderSet.getItem(Helper.organizationId);
            //var JsonItem = JsonConvert.DeserializeObject<ResultItemLite>(itemData.Result.ToString());

            //dataItem = JsonItem.list;
            //dt = Helper.ToDataTable(dataItem);
            //Session.Add("a", dt);

            //DataTable row = new DataTable();
            //row.Columns.Add("Data");
            //row.Columns.Add("CustomerAddress");
            //row.Columns.Add("Customer");

            //DataRow a = row.NewRow();
            //a[0] = "Test1";
            //a[1] = "";
            //a[2] = "";
            //row.Rows.Add(a);

            //DataRow b = row.NewRow();
            //b[0] = "Test2";
            //b[1] = "";
            //b[2] = "";
            //row.Rows.Add(b);

            //DataRow c = row.NewRow();
            //c[0] = "Test3";
            //c[1] = "";
            //c[2] = "";
            //row.Rows.Add(c);

            //DataRow d = row.NewRow();
            //d[0] = "Test4";
            //d[1] = "";
            //d[2] = "";
            //row.Rows.Add(d);

            //DataRow f = row.NewRow();
            //f[0] = "Test5";
            //f[1] = "";
            //f[2] = "";
            //row.Rows.Add(f);

            //gvw_data.DataSource = row;
            //gvw_data.DataBind();
        }
    }

    protected void cbList_CheckedChanged(object sender, EventArgs e)
    {
            //string a = "test";       
    }

    protected void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTest.Text = sender.ToString();
    }
}