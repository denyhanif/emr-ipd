using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_General_OrderSet_Control_Template_OrderSet_Clinic_Lab : System.Web.UI.UserControl
{
    public string setENG = "none";
    public string setIND = "none";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Helper.SessionLanguage] == null)
            Session[Helper.SessionLanguage] = 1;

        if (Session[Helper.SessionLanguage].ToString() == "1")
        {
            setENG = "";
            setIND = "none";
        }
        else if (Session[Helper.SessionLanguage].ToString() == "2")
        {
            setENG = "none";
            setIND = "";
        }
        else
        {
            setENG = "";
            setIND = "none";
        }
    }

    public List<CpoeTrans> getDataChecklistLab() {
        List<CpoeTrans> listchecked = new List<CpoeTrans>();
        if (grdCheckList.Rows.Count > 0)
        {
            foreach (GridViewRow data in grdCheckList.Rows)
            {
                listchecked.Add(new CpoeTrans
                {
                    id = Int64.Parse(data.Cells[0].Text),
                    name = data.Cells[1].Text,
                    type = data.Cells[2].Text,
                    isnew = int.Parse(data.Cells[3].Text),
                    iscito = int.Parse(data.Cells[4].Text),
                    issubmit = int.Parse(data.Cells[5].Text),
                    isdelete = int.Parse(data.Cells[6].Text),
                    ischeck = int.Parse(data.Cells[7].Text)
                });
            }
        }

        return listchecked;
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        List<CpoeTrans> listchecked = new List<CpoeTrans>();
        List<CpoeTrans> new_listCheck = new List<CpoeTrans>();
        if (grdCheckList.Rows.Count > 0)
        {
            foreach (GridViewRow data in grdCheckList.Rows)
            {
                listchecked.Add(new CpoeTrans
                {
                    id = Int64.Parse(data.Cells[0].Text),
                    name = data.Cells[1].Text,
                    type = data.Cells[2].Text,
                    isnew = int.Parse(data.Cells[3].Text),
                    iscito = int.Parse(data.Cells[4].Text),
                    issubmit = int.Parse(data.Cells[5].Text),
                    isdelete = int.Parse(data.Cells[6].Text),
                    ischeck = int.Parse(data.Cells[7].Text)
                });
            }
        }

        if (listchecked != null)
        {
            new_listCheck = listchecked;
        }
        else
        {
            new_listCheck = new List<CpoeTrans>();
        }
        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
        HiddenField chk = (HiddenField)Row.Cells[0].FindControl("Hidden_chk1");

        if (chk.Value != "")
        {
            var item = listchecked.FirstOrDefault(o => o.name == chk.Value);
            if (item == null)
            {
                new_listCheck.Add(new CpoeTrans()
                {
                    id = Int64.Parse(Row.Cells[1].Text.ToString()),
                    name = chk.Value,
                    type = "ClinicLab",
                    isnew = 1,
                    isdelete = 0,
                    issubmit = 0,
                    iscito = 0,
                    ischeck = 1,
                });
            }
            else
            {
                int index = new_listCheck.IndexOf(item);
                new_listCheck.RemoveAt(index);
            }
        }
        else
        {
            var item = new_listCheck.FirstOrDefault(o => o.name == chk.Value);
            int index = new_listCheck.IndexOf(item);
            new_listCheck.RemoveAt(index);
        }
        DataTable dt_listCheck;
        dt_listCheck = Helper.ToDataTable(new_listCheck);
        grdCheckList.DataSource = dt_listCheck;
        grdCheckList.DataBind();
    }

    public void GetMappingClinicLab(List<CpoeMapping> listmap, IEnumerable<CpoeTrans> listCheck)
    {
        List<CpoeMapping> listmapping = new List<CpoeMapping>();

        DataTable dt;
        DataTable dt_listCheck;
        try
        {
            listmapping = listmap;
            if (listCheck != null)
            {
                dt_listCheck = Helper.ToDataTable(listCheck.ToList());
                grdCheckList.DataSource = dt_listCheck;
                grdCheckList.DataBind();

                if (listmapping.Count > 0)
                {
                    var data = (
                                from a in listmapping
                                join b in listCheck on a.item_id equals b.id into joined
                                from b in joined.Where(x => x.iscito == 0 && x.isdelete == 0).DefaultIfEmpty()
                                where a.type == "ClinicLab"
                                select new LabList
                                {
                                    organizationId = a.organizationId,
                                    template_name = a.template_name,
                                    type = a.type,
                                    gridview_name = a.gridview_name,
                                    item_sequence = a.item_sequence,
                                    item_id = a.item_id,
                                    item_name = a.item_name,
                                    is_checked = b == null ? 0 : 1,
                                    issubmit = b == null ? 0 : b.issubmit,
                                    iscito = b == null ? 0 : b.iscito,
                                    fasting_flag = a.fasting_flag
                                }
                           ).Distinct().ToList();

                    dt = Helper.ToDataTable(data);
                    DataRow[] results = dt.Select("gridview_name = 'gvw1'");

                    if (dt.Select("gridview_name = 'gvw1'").Count() > 0)
                    {
                        gvw1.DataSource = dt.Select("gridview_name = 'gvw1'").CopyToDataTable();
                        gvw1.DataBind();
                    }
                    else
                    {
                        gvw1.DataSource = null;
                        gvw1.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw2'").Count() > 0)
                    {
                        gvw2.DataSource = dt.Select("gridview_name = 'gvw2'").CopyToDataTable();
                        gvw2.DataBind();
                    }
                    else
                    {
                        gvw2.DataSource = null;
                        gvw2.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw3'").Count() > 0)
                    {
                        gvw3.DataSource = dt.Select("gridview_name = 'gvw3'").CopyToDataTable();
                        gvw3.DataBind();
                    }
                    else
                    {
                        gvw3.DataSource = null;
                        gvw3.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw4'").Count() > 0)
                    {
                        gvw4.DataSource = dt.Select("gridview_name = 'gvw4'").CopyToDataTable();
                        gvw4.DataBind();
                    }
                    else
                    {
                        gvw4.DataSource = null;
                        gvw4.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw5'").Count() > 0)
                    {
                        gvw5.DataSource = dt.Select("gridview_name = 'gvw5'").CopyToDataTable();
                        gvw5.DataBind();
                    }
                    else
                    {
                        gvw5.DataSource = null;
                        gvw5.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw6'").Count() > 0)
                    {
                        gvw6.DataSource = dt.Select("gridview_name = 'gvw6'").CopyToDataTable();
                        gvw6.DataBind();
                    }
                    else
                    {
                        gvw6.DataSource = null;
                        gvw6.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw7'").Count() > 0)
                    {
                        gvw7.DataSource = dt.Select("gridview_name = 'gvw7'").CopyToDataTable();
                        gvw7.DataBind();
                    }
                    else
                    {
                        gvw7.DataSource = null;
                        gvw7.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw8'").Count() > 0)
                    {
                        gvw8.DataSource = dt.Select("gridview_name = 'gvw8'").CopyToDataTable();
                        gvw8.DataBind();
                    }
                    else
                    {
                        gvw8.DataSource = null;
                        gvw8.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw9'").Count() > 0)
                    {
                        gvw9.DataSource = dt.Select("gridview_name = 'gvw9'").CopyToDataTable();
                        gvw9.DataBind();
                    }
                    else
                    {
                        gvw9.DataSource = null;
                        gvw9.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw10'").Count() > 0)
                    {
                        gvw10.DataSource = dt.Select("gridview_name = 'gvw10'").CopyToDataTable();
                        gvw10.DataBind();
                    }
                    else
                    {
                        gvw10.DataSource = null;
                        gvw10.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw11'").Count() > 0)
                    {
                        gvw11.DataSource = dt.Select("gridview_name = 'gvw11'").CopyToDataTable();
                        gvw11.DataBind();
                    }
                    else
                    {
                        gvw11.DataSource = null;
                        gvw11.DataBind();
                    }


                    if (dt.Select("gridview_name = 'gvw12'").Count() > 0)
                    {
                        gvw12.DataSource = dt.Select("gridview_name = 'gvw12'").CopyToDataTable();
                        gvw12.DataBind();
                    }
                    else
                    {
                        gvw12.DataSource = null;
                        gvw12.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw13'").Count() > 0)
                    {
                        gvw13.DataSource = dt.Select("gridview_name = 'gvw13'").CopyToDataTable();
                        gvw13.DataBind();
                    }
                    else
                    {
                        gvw13.DataSource = null;
                        gvw13.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw14'").Count() > 0)
                    {
                        gvw14.DataSource = dt.Select("gridview_name = 'gvw14'").CopyToDataTable();
                        gvw14.DataBind();
                    }
                    else
                    {
                        gvw14.DataSource = null;
                        gvw14.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw15'").Count() > 0)
                    {
                        gvw15.DataSource = dt.Select("gridview_name = 'gvw15'").CopyToDataTable();
                        gvw15.DataBind();
                    }
                    else
                    {
                        gvw15.DataSource = null;
                        gvw15.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw16'").Count() > 0)
                    {
                        gvw16.DataSource = dt.Select("gridview_name = 'gvw16'").CopyToDataTable();
                        gvw16.DataBind();
                    }
                    else
                    {
                        gvw16.DataSource = null;
                        gvw16.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw17'").Count() > 0)
                    {
                        gvw17.DataSource = dt.Select("gridview_name = 'gvw17'").CopyToDataTable();
                        gvw17.DataBind();
                    }
                    else
                    {
                        gvw17.DataSource = null;
                        gvw17.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw18'").Count() > 0)
                    {
                        gvw18.DataSource = dt.Select("gridview_name = 'gvw18'").CopyToDataTable();
                        gvw18.DataBind();
                    }
                    else
                    {
                        gvw18.DataSource = null;
                        gvw18.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw19'").Count() > 0)
                    {
                        gvw19.DataSource = dt.Select("gridview_name = 'gvw19'").CopyToDataTable();
                        gvw19.DataBind();
                    }
                    else
                    {
                        gvw19.DataSource = null;
                        gvw19.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw20'").Count() > 0)
                    {
                        gvw20.DataSource = dt.Select("gridview_name = 'gvw20'").CopyToDataTable();
                        gvw20.DataBind();
                    }
                    else
                    {
                        gvw20.DataSource = null;
                        gvw20.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw21'").Count() > 0)
                    {
                        gvw21.DataSource = dt.Select("gridview_name = 'gvw21'").CopyToDataTable();
                        gvw21.DataBind();
                    }
                    else
                    {
                        gvw21.DataSource = null;
                        gvw21.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw22'").Count() > 0)
                    {
                        gvw22.DataSource = dt.Select("gridview_name = 'gvw22'").CopyToDataTable();
                        gvw22.DataBind();
                    }
                    else
                    {
                        gvw22.DataSource = null;
                        gvw22.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw23'").Count() > 0)
                    {
                        gvw23.DataSource = dt.Select("gridview_name = 'gvw23'").CopyToDataTable();
                        gvw23.DataBind();
                    }
                    else
                    {
                        gvw23.DataSource = null;
                        gvw23.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw24'").Count() > 0)
                    {
                        gvw24.DataSource = dt.Select("gridview_name = 'gvw24'").CopyToDataTable();
                        gvw24.DataBind();
                    }
                    else
                    {
                        gvw24.DataSource = null;
                        gvw24.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw25'").Count() > 0)
                    {
                        gvw25.DataSource = dt.Select("gridview_name = 'gvw25'").CopyToDataTable();
                        gvw25.DataBind();
                    }
                    else
                    {
                        gvw25.DataSource = null;
                        gvw25.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw26'").Count() > 0)
                    {
                        gvw26.DataSource = dt.Select("gridview_name = 'gvw26'").CopyToDataTable();
                        gvw26.DataBind();
                    }
                    else
                    {
                        gvw26.DataSource = null;
                        gvw26.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw27'").Count() > 0)
                    {
                        gvw27.DataSource = dt.Select("gridview_name = 'gvw27'").CopyToDataTable();
                        gvw27.DataBind();
                    }
                    else
                    {
                        gvw27.DataSource = null;
                        gvw27.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw28'").Count() > 0)
                    {
                        gvw28.DataSource = dt.Select("gridview_name = 'gvw28'").CopyToDataTable();
                        gvw28.DataBind();
                    }
                    else
                    {
                        gvw28.DataSource = null;
                        gvw28.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw29'").Count() > 0)
                    {
                        gvw29.DataSource = dt.Select("gridview_name = 'gvw29'").CopyToDataTable();
                        gvw29.DataBind();
                    }
                    else
                    {
                        gvw29.DataSource = null;
                        gvw29.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw30'").Count() > 0)
                    {
                        gvw30.DataSource = dt.Select("gridview_name = 'gvw30'").CopyToDataTable();
                        gvw30.DataBind();
                    }
                    else
                    {
                        gvw30.DataSource = null;
                        gvw30.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw31'").Count() > 0)
                    {
                        gvw31.DataSource = dt.Select("gridview_name = 'gvw31'").CopyToDataTable();
                        gvw31.DataBind();
                    }
                    else
                    {
                        gvw31.DataSource = null;
                        gvw31.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw32'").Count() > 0)
                    {
                        gvw32.DataSource = dt.Select("gridview_name = 'gvw32'").CopyToDataTable();
                        gvw32.DataBind();
                    }
                    else
                    {
                        gvw32.DataSource = null;
                        gvw32.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw33'").Count() > 0)
                    {
                        gvw33.DataSource = dt.Select("gridview_name = 'gvw33'").CopyToDataTable();
                        gvw33.DataBind();
                    }
                    else
                    {
                        gvw33.DataSource = null;
                        gvw33.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw34'").Count() > 0)
                    {
                        gvw34.DataSource = dt.Select("gridview_name = 'gvw34'").CopyToDataTable();
                        gvw34.DataBind();
                    }
                    else
                    {
                        gvw34.DataSource = null;
                        gvw34.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw35'").Count() > 0)
                    {
                        gvw35.DataSource = dt.Select("gridview_name = 'gvw35'").CopyToDataTable();
                        gvw35.DataBind();
                    }
                    else
                    {
                        gvw35.DataSource = null;
                        gvw35.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw36'").Count() > 0)
                    {
                        gvw36.DataSource = dt.Select("gridview_name = 'gvw36'").CopyToDataTable();
                        gvw36.DataBind();
                    }
                    else
                    {
                        gvw36.DataSource = null;
                        gvw36.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw37'").Count() > 0)
                    {
                        gvw37.DataSource = dt.Select("gridview_name = 'gvw37'").CopyToDataTable();
                        gvw37.DataBind();
                    }
                    else
                    {
                        gvw37.DataSource = null;
                        gvw37.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw38'").Count() > 0)
                    {
                        gvw38.DataSource = dt.Select("gridview_name = 'gvw38'").CopyToDataTable();
                        gvw38.DataBind();
                    }
                    else
                    {
                        gvw38.DataSource = null;
                        gvw38.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw39'").Count() > 0)
                    {
                        gvw39.DataSource = dt.Select("gridview_name = 'gvw39'").CopyToDataTable();
                        gvw39.DataBind();
                    }
                    else
                    {
                        gvw39.DataSource = null;
                        gvw39.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw40'").Count() > 0)
                    {
                        gvw40.DataSource = dt.Select("gridview_name = 'gvw40'").CopyToDataTable();
                        gvw40.DataBind();
                    }
                    else
                    {
                        gvw40.DataSource = null;
                        gvw40.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw41'").Count() > 0)
                    {
                        gvw41.DataSource = dt.Select("gridview_name = 'gvw41'").CopyToDataTable();
                        gvw41.DataBind();
                    }
                    else
                    {
                        gvw41.DataSource = null;
                        gvw41.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw42'").Count() > 0)
                    {
                        gvw42.DataSource = dt.Select("gridview_name = 'gvw42'").CopyToDataTable();
                        gvw42.DataBind();
                    }
                    else
                    {
                        gvw42.DataSource = null;
                        gvw42.DataBind();
                    }
                }
            }

            else
            {
                listCheck = new List<CpoeTrans>();
                if (listmapping.Count > 0)
                {
                    var data = (
                                from a in listmapping
                                join b in listCheck on a.item_id equals b.id into joined
                                from b in joined.Where(x => x.iscito == 0 && x.isdelete == 0).DefaultIfEmpty()
                                where a.type == "ClinicLab"
                                select new LabList
                                {
                                    organizationId = a.organizationId,
                                    template_name = a.template_name,
                                    type = a.type,
                                    gridview_name = a.gridview_name,
                                    item_sequence = a.item_sequence,
                                    item_id = a.item_id,
                                    item_name = a.item_name,
                                    is_checked = b == null ? 0 : 1,
                                    issubmit = b == null ? 0 : b.issubmit,
                                    iscito = b == null ? 0 : b.iscito,
                                    fasting_flag = a.fasting_flag
                                }
                           ).Distinct().ToList();

                    dt = Helper.ToDataTable(data);
                    DataRow[] results = dt.Select("gridview_name = 'gvw1'");
                    if (dt.Select("gridview_name = 'gvw1'").Count() > 0)
                    {
                        gvw1.DataSource = dt.Select("gridview_name = 'gvw1'").CopyToDataTable();
                        gvw1.DataBind();
                    }
                    else
                    {
                        gvw1.DataSource = null;
                        gvw1.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw2'").Count() > 0)
                    {
                        gvw2.DataSource = dt.Select("gridview_name = 'gvw2'").CopyToDataTable();
                        gvw2.DataBind();
                    }
                    else
                    {
                        gvw2.DataSource = null;
                        gvw2.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw3'").Count() > 0)
                    {
                        gvw3.DataSource = dt.Select("gridview_name = 'gvw3'").CopyToDataTable();
                        gvw3.DataBind();
                    }
                    else
                    {
                        gvw3.DataSource = null;
                        gvw3.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw4'").Count() > 0)
                    {
                        gvw4.DataSource = dt.Select("gridview_name = 'gvw4'").CopyToDataTable();
                        gvw4.DataBind();
                    }
                    else
                    {
                        gvw4.DataSource = null;
                        gvw4.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw5'").Count() > 0)
                    {
                        gvw5.DataSource = dt.Select("gridview_name = 'gvw5'").CopyToDataTable();
                        gvw5.DataBind();
                    }
                    else
                    {
                        gvw5.DataSource = null;
                        gvw5.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw6'").Count() > 0)
                    {
                        gvw6.DataSource = dt.Select("gridview_name = 'gvw6'").CopyToDataTable();
                        gvw6.DataBind();
                    }
                    else
                    {
                        gvw6.DataSource = null;
                        gvw6.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw7'").Count() > 0)
                    {
                        gvw7.DataSource = dt.Select("gridview_name = 'gvw7'").CopyToDataTable();
                        gvw7.DataBind();
                    }
                    else
                    {
                        gvw7.DataSource = null;
                        gvw7.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw8'").Count() > 0)
                    {
                        gvw8.DataSource = dt.Select("gridview_name = 'gvw8'").CopyToDataTable();
                        gvw8.DataBind();
                    }
                    else
                    {
                        gvw8.DataSource = null;
                        gvw8.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw9'").Count() > 0)
                    {
                        gvw9.DataSource = dt.Select("gridview_name = 'gvw9'").CopyToDataTable();
                        gvw9.DataBind();
                    }
                    else
                    {
                        gvw9.DataSource = null;
                        gvw9.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw10'").Count() > 0)
                    {
                        gvw10.DataSource = dt.Select("gridview_name = 'gvw10'").CopyToDataTable();
                        gvw10.DataBind();
                    }
                    else
                    {
                        gvw10.DataSource = null;
                        gvw10.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw11'").Count() > 0)
                    {
                        gvw11.DataSource = dt.Select("gridview_name = 'gvw11'").CopyToDataTable();
                        gvw11.DataBind();
                    }
                    else
                    {
                        gvw11.DataSource = null;
                        gvw11.DataBind();
                    }


                    if (dt.Select("gridview_name = 'gvw12'").Count() > 0)
                    {
                        gvw12.DataSource = dt.Select("gridview_name = 'gvw12'").CopyToDataTable();
                        gvw12.DataBind();
                    }
                    else
                    {
                        gvw12.DataSource = null;
                        gvw12.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw13'").Count() > 0)
                    {
                        gvw13.DataSource = dt.Select("gridview_name = 'gvw13'").CopyToDataTable();
                        gvw13.DataBind();
                    }
                    else
                    {
                        gvw13.DataSource = null;
                        gvw13.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw14'").Count() > 0)
                    {
                        gvw14.DataSource = dt.Select("gridview_name = 'gvw14'").CopyToDataTable();
                        gvw14.DataBind();
                    }
                    else
                    {
                        gvw14.DataSource = null;
                        gvw14.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw15'").Count() > 0)
                    {
                        gvw15.DataSource = dt.Select("gridview_name = 'gvw15'").CopyToDataTable();
                        gvw15.DataBind();
                    }
                    else
                    {
                        gvw15.DataSource = null;
                        gvw15.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw16'").Count() > 0)
                    {
                        gvw16.DataSource = dt.Select("gridview_name = 'gvw16'").CopyToDataTable();
                        gvw16.DataBind();
                    }
                    else
                    {
                        gvw16.DataSource = null;
                        gvw16.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw17'").Count() > 0)
                    {
                        gvw17.DataSource = dt.Select("gridview_name = 'gvw17'").CopyToDataTable();
                        gvw17.DataBind();
                    }
                    else
                    {
                        gvw17.DataSource = null;
                        gvw17.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw18'").Count() > 0)
                    {
                        gvw18.DataSource = dt.Select("gridview_name = 'gvw18'").CopyToDataTable();
                        gvw18.DataBind();
                    }
                    else
                    {
                        gvw18.DataSource = null;
                        gvw18.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw19'").Count() > 0)
                    {
                        gvw19.DataSource = dt.Select("gridview_name = 'gvw19'").CopyToDataTable();
                        gvw19.DataBind();
                    }
                    else
                    {
                        gvw19.DataSource = null;
                        gvw19.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw20'").Count() > 0)
                    {
                        gvw20.DataSource = dt.Select("gridview_name = 'gvw20'").CopyToDataTable();
                        gvw20.DataBind();
                    }
                    else
                    {
                        gvw20.DataSource = null;
                        gvw20.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw21'").Count() > 0)
                    {
                        gvw21.DataSource = dt.Select("gridview_name = 'gvw21'").CopyToDataTable();
                        gvw21.DataBind();
                    }
                    else
                    {
                        gvw21.DataSource = null;
                        gvw21.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw22'").Count() > 0)
                    {
                        gvw22.DataSource = dt.Select("gridview_name = 'gvw22'").CopyToDataTable();
                        gvw22.DataBind();
                    }
                    else
                    {
                        gvw22.DataSource = null;
                        gvw22.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw23'").Count() > 0)
                    {
                        gvw23.DataSource = dt.Select("gridview_name = 'gvw23'").CopyToDataTable();
                        gvw23.DataBind();
                    }
                    else
                    {
                        gvw23.DataSource = null;
                        gvw23.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw24'").Count() > 0)
                    {
                        gvw24.DataSource = dt.Select("gridview_name = 'gvw24'").CopyToDataTable();
                        gvw24.DataBind();
                    }
                    else
                    {
                        gvw24.DataSource = null;
                        gvw24.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw25'").Count() > 0)
                    {
                        gvw25.DataSource = dt.Select("gridview_name = 'gvw25'").CopyToDataTable();
                        gvw25.DataBind();
                    }
                    else
                    {
                        gvw25.DataSource = null;
                        gvw25.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw26'").Count() > 0)
                    {
                        gvw26.DataSource = dt.Select("gridview_name = 'gvw26'").CopyToDataTable();
                        gvw26.DataBind();
                    }
                    else
                    {
                        gvw26.DataSource = null;
                        gvw26.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw27'").Count() > 0)
                    {
                        gvw27.DataSource = dt.Select("gridview_name = 'gvw27'").CopyToDataTable();
                        gvw27.DataBind();
                    }
                    else
                    {
                        gvw27.DataSource = null;
                        gvw27.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw28'").Count() > 0)
                    {
                        gvw28.DataSource = dt.Select("gridview_name = 'gvw28'").CopyToDataTable();
                        gvw28.DataBind();
                    }
                    else
                    {
                        gvw28.DataSource = null;
                        gvw28.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw29'").Count() > 0)
                    {
                        gvw29.DataSource = dt.Select("gridview_name = 'gvw29'").CopyToDataTable();
                        gvw29.DataBind();
                    }
                    else
                    {
                        gvw29.DataSource = null;
                        gvw29.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw30'").Count() > 0)
                    {
                        gvw30.DataSource = dt.Select("gridview_name = 'gvw30'").CopyToDataTable();
                        gvw30.DataBind();
                    }
                    else
                    {
                        gvw30.DataSource = null;
                        gvw30.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw31'").Count() > 0)
                    {
                        gvw31.DataSource = dt.Select("gridview_name = 'gvw31'").CopyToDataTable();
                        gvw31.DataBind();
                    }
                    else
                    {
                        gvw31.DataSource = null;
                        gvw31.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw32'").Count() > 0)
                    {
                        gvw32.DataSource = dt.Select("gridview_name = 'gvw32'").CopyToDataTable();
                        gvw32.DataBind();
                    }
                    else
                    {
                        gvw32.DataSource = null;
                        gvw32.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw33'").Count() > 0)
                    {
                        gvw33.DataSource = dt.Select("gridview_name = 'gvw33'").CopyToDataTable();
                        gvw33.DataBind();
                    }
                    else
                    {
                        gvw33.DataSource = null;
                        gvw33.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw34'").Count() > 0)
                    {
                        gvw34.DataSource = dt.Select("gridview_name = 'gvw34'").CopyToDataTable();
                        gvw34.DataBind();
                    }
                    else
                    {
                        gvw34.DataSource = null;
                        gvw34.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw35'").Count() > 0)
                    {
                        gvw35.DataSource = dt.Select("gridview_name = 'gvw35'").CopyToDataTable();
                        gvw35.DataBind();
                    }
                    else
                    {
                        gvw35.DataSource = null;
                        gvw35.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw36'").Count() > 0)
                    {
                        gvw36.DataSource = dt.Select("gridview_name = 'gvw36'").CopyToDataTable();
                        gvw36.DataBind();
                    }
                    else
                    {
                        gvw36.DataSource = null;
                        gvw36.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw37'").Count() > 0)
                    {
                        gvw37.DataSource = dt.Select("gridview_name = 'gvw37'").CopyToDataTable();
                        gvw37.DataBind();
                    }
                    else
                    {
                        gvw37.DataSource = null;
                        gvw37.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw38'").Count() > 0)
                    {
                        gvw38.DataSource = dt.Select("gridview_name = 'gvw38'").CopyToDataTable();
                        gvw38.DataBind();
                    }
                    else
                    {
                        gvw38.DataSource = null;
                        gvw38.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw39'").Count() > 0)
                    {
                        gvw39.DataSource = dt.Select("gridview_name = 'gvw39'").CopyToDataTable();
                        gvw39.DataBind();
                    }
                    else
                    {
                        gvw39.DataSource = null;
                        gvw39.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw40'").Count() > 0)
                    {
                        gvw40.DataSource = dt.Select("gridview_name = 'gvw40'").CopyToDataTable();
                        gvw40.DataBind();
                    }
                    else
                    {
                        gvw40.DataSource = null;
                        gvw40.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw41'").Count() > 0)
                    {
                        gvw41.DataSource = dt.Select("gridview_name = 'gvw41'").CopyToDataTable();
                        gvw41.DataBind();
                    }
                    else
                    {
                        gvw41.DataSource = null;
                        gvw41.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw42'").Count() > 0)
                    {
                        gvw42.DataSource = dt.Select("gridview_name = 'gvw42'").CopyToDataTable();
                        gvw42.DataBind();
                    }
                    else
                    {
                        gvw42.DataSource = null;
                        gvw42.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}