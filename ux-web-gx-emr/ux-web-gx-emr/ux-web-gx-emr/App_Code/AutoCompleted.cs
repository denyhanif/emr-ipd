using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for AutoCompleted
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AutoCompleted : System.Web.Services.WebService
{
    public List<Item> ItemList = new List<Item>();
    public int Count = 0;
    public static string a = "";

    [System.Web.Services.WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public List<string> GetListOfSettings(string prefixText, int count)
    {
        DataTable dt = new DataTable();
        if (Session[Helper.SessionDrug] == null)
        {

        }
        else
        {
             dt = Session[Helper.SessionDrug] as DataTable;
        }
        List<string> settingNames = new List<string>();
        IEnumerable<string> a = new List<string>();
        DataTable filter = new DataTable();

        try
        {
            filter = dt.Select("salesItemName LIKE '%" + prefixText.ToUpper() + "%'").CopyToDataTable();
            string custItem = string.Empty;

            foreach (DataRow row in filter.Rows)
            {
                custItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["salesItemName"].ToString(), row["salesItemId"].ToString());
                settingNames.Add(custItem);
            }
        }
        catch (Exception ex)
        {

        }

        return settingNames;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] Test(string prefixText, int count, string contextKey)
    {
        string[] suggestedSettings = new string[0];

        if (contextKey == "13")
        {
            var data = clsOrderSet.getSearchItem(2, prefixText);
            var JsonOrderSet = JsonConvert.DeserializeObject<ResultItem>(data.Result.ToString());
            List<Item> listOrderSet = new List<Item>();
            listOrderSet = JsonOrderSet.list;
            List<string> settingNames = new List<string>();

            foreach (Item name in listOrderSet)
            {
                settingNames.Add(name.salesItemName);
            }

            if (settingNames.Count > 0)
            {
                suggestedSettings = settingNames.ToArray();
            }
        }

        return suggestedSettings;
    }
}
