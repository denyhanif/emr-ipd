﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_CPOE_Control_Template_RptUSGRad : System.Web.UI.UserControl
{
    public List<CpoeTrans> listchecked, listcheckedtemp = new List<CpoeTrans>();
    public List<CpoeMapping> listmapping = new List<CpoeMapping>();

    //public string setENG = "none";
    //public string setIND = "none";

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session[Helper.SessionLanguage] == "1")
        //{
        //    setENG = "";
        //    setIND = "none";
        //    HFisBahasa_usgrad.Value = "ENG";
        //}
        //else if (Session[Helper.SessionLanguage] == "2")
        //{
        //    setENG = "none";
        //    setIND = "";
        //    HFisBahasa_usgrad.Value = "IND";
        //}
        //else
        //{
        //    setENG = "";
        //    setIND = "none";
        //    HFisBahasa_usgrad.Value = "ENG";
        //}

        //set bahasa
        //ScriptManager.RegisterStartupScript(this, GetType(), "bahasa", "switchBahasa_usgrad();", true);

        //if (!IsPostBack)
        //{
        //    //GetMappingClinicLab(2, "cliniclab");
        //}
    }

    public void InitializeNotes(CpoeNotes cpoenotes)
    {
        txtUsgNotes.Text = cpoenotes.notes;
    }

    public List<CpoeNotes> getvaluesnotes(List<CpoeNotes> cpoenotes)
    {
        //bool alreadyExists = cpoenotes.Any(x => x.notes_type == "CitoLab");
        if (cpoenotes.Any(x => x.notes_type == "USG"))
        {
            foreach (CpoeNotes tempvalues in cpoenotes)
            {
                if (tempvalues.notes_type == "USG")
                {
                    tempvalues.notes = txtUsgNotes.Text;
                }
            }
        }
        else
        {
            CpoeNotes tempnotes = new CpoeNotes();
            tempnotes.cpoe_notes_id = Guid.Empty;
            tempnotes.notes_type = "USG";
            tempnotes.notes = txtUsgNotes.Text;
            cpoenotes.Add(tempnotes);
        }
        return cpoenotes;
    }

    public delegate bool customHandler(object sender);
    public event customHandler checkIfExistUSG;
    public void GetMappingClinicLab(List<CpoeMapping> listmap, string guidadditional, string flagFO)
    {
        DataTable dt;
        try
        {
            //log.Info(LogLibrary.Logging("S", "GetDataWorklist", txtUsername.Text.ToString(), ""));
            //var getMap = clsCpoeMapping.GetMapping(organizationId);
            //var getMapJson = JsonConvert.DeserializeObject<ResultMapping>(getMap.Result.ToString());
            hfguidadditional.Value = guidadditional;
            listmapping = listmap;
            if (Session[Helper.Sessionradcheck + hfguidadditional.Value] != null)
            {
                listchecked = (List<CpoeTrans>)Session[Helper.Sessionradcheck + hfguidadditional.Value];
                string arrayString = "listradiology = [];";
                foreach (var x in listchecked)
                {
                    if (x.FutureOrderDate != null)
                    {
                        arrayString = arrayString + "listradiology.push({ id:" + x.id + " , name: '" + x.name + "',type: '" + x.type
                                        + "', remarks: '" + x.remarks + "', isnew: " + x.isnew + ", iscito: " + x.iscito + ", issubmit: " + x.issubmit
                                        + ",isdelete: " + x.isdelete + ", ischeck: " + x.ischeck + ", IsSendHope: " + x.IsSendHope
                                        + ", IsFutureOrder: " + x.IsFutureOrder.ToString().ToLower() + ", FutureOrderDate: '" + x.FutureOrderDate + "' });";
                    }
                    else
                    {
                        arrayString = arrayString + "listradiology.push({ id:" + x.id + " , name: '" + x.name + "',type: '" + x.type
                                        + "', remarks: '" + x.remarks + "', isnew: " + x.isnew + ", iscito: " + x.iscito + ", issubmit: " + x.issubmit
                                        + ",isdelete: " + x.isdelete + ", ischeck: " + x.ischeck + ", IsSendHope: " + x.IsSendHope
                                        + ", IsFutureOrder: " + x.IsFutureOrder.ToString().ToLower() + ", FutureOrderDate: '" + DateTime.Now + "' });";
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdditional", arrayString, true);
                List<LabList> data = new List<LabList>();
                if (listmapping.Count > 0)
                {
                    if (flagFO == "true")
                    {
                        data = (
                                from a in listmapping
                                join b in listchecked on a.item_id equals b.id into joined
                                from b in joined.Where(x => x.isdelete == 0 && x.IsFutureOrder == true).DefaultIfEmpty()
                                where a.type == "USG" && a.organizationId != 0
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
                                    is_leftright = a.is_leftright,
                                    remarks = b == null ? "" : b.remarks,
                                    fasting_flag = a.fasting_flag,
                                    IsSendHope = b == null ? 0 : b.IsSendHope
                                }
                           ).Distinct().ToList();
                    }
                    else if (flagFO == "false")
                    {
                        data = (
                                from a in listmapping
                                join b in listchecked on a.item_id equals b.id into joined
                                from b in joined.Where(x => x.isdelete == 0 && x.IsFutureOrder == false).DefaultIfEmpty()
                                where a.type == "USG" && a.organizationId != 0
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
                                    is_leftright = a.is_leftright,
                                    remarks = b == null ? "" : b.remarks,
                                    fasting_flag = a.fasting_flag,
                                    IsSendHope = b == null ? 0 : b.IsSendHope
                                }
                           ).Distinct().ToList();
                    }
                        

                    dt = Helper.ToDataTable(data);
                    DataRow[] results = dt.Select("gridview_name = 'gvw1'");
                    if (dt.Select("gridview_name = 'gvw1'").Count() > 0)
                    {
                        rpt1.DataSource = dt.Select("gridview_name = 'gvw1'").CopyToDataTable();
                        rpt1.DataBind();
                    }
                    else
                    {
                        rpt1.DataSource = null;
                        rpt1.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw2'").Count() > 0)
                    {
                        rpt2.DataSource = dt.Select("gridview_name = 'gvw2'").CopyToDataTable();
                        rpt2.DataBind();
                    }
                    else
                    {
                        rpt2.DataSource = null;
                        rpt2.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw3'").Count() > 0)
                    {
                        rpt3.DataSource = dt.Select("gridview_name = 'gvw3'").CopyToDataTable();
                        rpt3.DataBind();
                    }
                    else
                    {
                        rpt3.DataSource = null;
                        rpt3.DataBind();
                    }
                }
            }
            else
            {
                listchecked = new List<CpoeTrans>();
                List<LabList> data = new List<LabList>();
                if (listmapping.Count > 0)
                {
                    if (flagFO == "true")
                    {
                        data = (
                                from a in listmapping
                                join b in listchecked on a.item_id equals b.id into joined
                                from b in joined.Where(x => x.isdelete == 0 && x.IsFutureOrder == true).DefaultIfEmpty()
                                where a.type == "USG" && a.organizationId != 0
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
                                    is_leftright = a.is_leftright,
                                    remarks = b == null ? "" : b.remarks,
                                    fasting_flag = a.fasting_flag,
                                    IsSendHope = b == null ? 0 : b.IsSendHope
                                }
                           ).Distinct().ToList();
                    }
                    else if (flagFO == "false")
                    {
                        data = (
                                from a in listmapping
                                join b in listchecked on a.item_id equals b.id into joined
                                from b in joined.Where(x => x.isdelete == 0 && x.IsFutureOrder == false).DefaultIfEmpty()
                                where a.type == "USG" && a.organizationId != 0
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
                                    is_leftright = a.is_leftright,
                                    remarks = b == null ? "" : b.remarks,
                                    fasting_flag = a.fasting_flag,
                                    IsSendHope = b == null ? 0 : b.IsSendHope
                                }
                           ).Distinct().ToList();
                    }
                        

                    dt = Helper.ToDataTable(data);
                    DataRow[] results = dt.Select("gridview_name = 'gvw1'");
                    if (dt.Select("gridview_name = 'gvw1'").Count() > 0)
                    {
                        rpt1.DataSource = dt.Select("gridview_name = 'gvw1'").CopyToDataTable();
                        rpt1.DataBind();
                    }
                    else
                    {
                        rpt1.DataSource = null;
                        rpt1.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw2'").Count() > 0)
                    {
                        rpt2.DataSource = dt.Select("gridview_name = 'gvw2'").CopyToDataTable();
                        rpt2.DataBind();
                    }
                    else
                    {
                        rpt2.DataSource = null;
                        rpt2.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw3'").Count() > 0)
                    {
                        rpt3.DataSource = dt.Select("gridview_name = 'gvw3'").CopyToDataTable();
                        rpt3.DataBind();
                    }
                    else
                    {
                        rpt3.DataSource = null;
                        rpt3.DataBind();
                    }
                }
            }
            //DataTable dt = Helper.ToDataTable(data);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}