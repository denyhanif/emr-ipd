﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_General_OrderSet_Control_Template_OrderSet_MDC_Lab : System.Web.UI.UserControl
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

    public List<CpoeTrans> getDataChecklistMDC()
    {
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

    public void GetMappingMDCLab(List<CpoeMapping> listmap, IEnumerable<CpoeTrans> listCheck)
    {
        DataTable dt;
        DataTable dt_listCheck;
        try
        {
            if (listCheck != null)
            {
                dt_listCheck = Helper.ToDataTable(listCheck.ToList());
                grdCheckList.DataSource = dt_listCheck;
                grdCheckList.DataBind();
                if (listmap.Count > 0)
                {
                    var data = (
                                from a in listmap
                                join b in listCheck on a.item_id equals b.id into joined
                                from b in joined.Where(x => x.isdelete == 0).DefaultIfEmpty()
                                where a.type == "MDCLab"
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
                                    iscito = b == null ? 0 : b.iscito
                                }
                           ).Distinct().ToList();
                    dt = Helper.ToDataTable(data);

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

                }
            }
            else
            {
                listCheck = new List<CpoeTrans>();

                if (listmap.Count > 0)
                {
                    var data = (
                                from a in listmap
                                join b in listCheck on a.item_id equals b.id into joined
                                from b in joined.Where(x => x.isdelete == 0).DefaultIfEmpty()
                                where a.type == "MDCLab"
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
                                    iscito = b == null ? 0 : b.iscito
                                }
                           ).Distinct().ToList();
                    dt = Helper.ToDataTable(data);
                    gvw1.DataSource = dt.Select("gridview_name = 'gvw1'").CopyToDataTable();
                    gvw1.DataBind();

                    gvw2.DataSource = dt.Select("gridview_name = 'gvw2'").CopyToDataTable();
                    gvw2.DataBind();

                    gvw3.DataSource = dt.Select("gridview_name = 'gvw3'").CopyToDataTable();
                    gvw3.DataBind();

                    gvw4.DataSource = dt.Select("gridview_name = 'gvw4'").CopyToDataTable();
                    gvw4.DataBind();

                    gvw5.DataSource = dt.Select("gridview_name = 'gvw5'").CopyToDataTable();
                    gvw5.DataBind();

                    gvw6.DataSource = dt.Select("gridview_name = 'gvw6'").CopyToDataTable();
                    gvw6.DataBind();

                    gvw7.DataSource = dt.Select("gridview_name = 'gvw7'").CopyToDataTable();
                    gvw7.DataBind();

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
                    type = "MDCLab",
                    isnew = 1,
                    isdelete = 0,
                    issubmit = 0,
                    iscito = 0,
                    ischeck = 1,
                    //encounter_ticket_id = Guid.Parse("7A6060BB-F2CC-43D8-B3AD-1F4CCE320106")
                });
            }
            else
            {
                int index = new_listCheck.IndexOf(item);
                //new_listCheck[index].isdelete = 0;
                new_listCheck.RemoveAt(index);
            }
        }
        else
        {
            var item = new_listCheck.FirstOrDefault(o => o.name == chk.Value);
            int index = new_listCheck.IndexOf(item);
            new_listCheck.RemoveAt(index);
        }
        new_listCheck = listchecked;

        DataTable dt_listCheck;
        dt_listCheck = Helper.ToDataTable(new_listCheck);
        grdCheckList.DataSource = dt_listCheck;
        grdCheckList.DataBind();
    }
}