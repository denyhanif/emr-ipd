﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

public partial class Form_CPOE_Control_Template_XrayRad : System.Web.UI.UserControl
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
        //    HFisBahasa_xrayrad.Value = "ENG";
        //}
        //else if (Session[Helper.SessionLanguage] == "2")
        //{
        //    setENG = "none";
        //    setIND = "";
        //    HFisBahasa_xrayrad.Value = "IND";
        //}
        //else
        //{
        //    setENG = "";
        //    setIND = "none";
        //    HFisBahasa_xrayrad.Value = "ENG";
        //}

        //set bahasa
        //ScriptManager.RegisterStartupScript(this, GetType(), "bahasa", "switchBahasa_xrayrad();", true);

        //if (!IsPostBack)
        //{
        //    //GetMappingClinicLab(2, "cliniclab");
        //}
    }

    public void InitializeNotes(CpoeNotes cpoenotes)
    {
        txtXrayNotes.Text = cpoenotes.notes;
    }

    public List<CpoeNotes> getvaluesnotes(List<CpoeNotes> cpoenotes)
    {
        //bool alreadyExists = cpoenotes.Any(x => x.notes_type == "CitoLab");
        if (cpoenotes.Any(x => x.notes_type == "Xray"))
        {
            foreach (CpoeNotes tempvalues in cpoenotes)
            {
                if (tempvalues.notes_type == "Xray")
                {
                    tempvalues.notes = txtXrayNotes.Text;
                }
            }
        }
        else
        {
            CpoeNotes tempnotes = new CpoeNotes();
            tempnotes.cpoe_notes_id = Guid.Empty;
            tempnotes.notes_type = "Xray";
            tempnotes.notes = txtXrayNotes.Text;
            cpoenotes.Add(tempnotes);
        }
        return cpoenotes;
    }

    public delegate bool customHandler(object sender);
    public event customHandler checkIfExist;

    protected void btnGetValueCPOE(object sender, EventArgs e)
    {
        string objectcpoe = hfbuilderobjectradiology.Value;
        if (objectcpoe.Length > 0)
        {
            objectcpoe = objectcpoe.Remove(objectcpoe.Length - 1, 1);
            objectcpoe = "[" + objectcpoe + "]";
            List<CpoeTrans> tempcurrmed = new JavaScriptSerializer().Deserialize<List<CpoeTrans>>(objectcpoe);

            var duplicatedlab =
                            from p in tempcurrmed
                            group p by p.id into g
                            where g.Count() > 1
                            select g.Key;

            if (duplicatedlab.Count() > 0)
            {
                tempcurrmed = tempcurrmed.GroupBy(i => i.id).Select(g => g.First()).ToList();
            }


            Button chk = (Button)sender;
            Session[Helper.Sessionradcheck + hfguidadditional.Value] = tempcurrmed;
            checkIfExist(sender);
        }
        else
        {
            Button chk = (Button)sender;
            Session[Helper.Sessionradcheck + hfguidadditional.Value] = null;
            checkIfExist(sender);
        }
    }

    public void GetMappingClinicLab(List<CpoeMapping> listmap, string guidadditional)
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
                    arrayString = arrayString + "listradiology.push({ id:" + x.id + " , name: '" + x.name + "',type: '" + x.type
                        + "', remarks: '" + x.remarks + "', isnew: " + x.isnew + ", iscito: " + x.iscito + ", issubmit: " + x.issubmit
                        + ",isdelete: " + x.isdelete + ", ischeck: " + x.ischeck + " });";
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdditional", arrayString, true);

                if (listmapping.Count > 0)
                {
                    var data = (
                                from a in listmapping
                                join b in listchecked on a.item_id equals b.id into joined
                                from b in joined.Where(x => x.isdelete == 0).DefaultIfEmpty()
                                where a.type == "Radiology" && a.organizationId != 0
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
                }
            }
            else
            {
                listchecked = new List<CpoeTrans>();
                if (listmapping.Count > 0)
                {
                    var data = (
                                from a in listmapping
                                join b in listchecked on a.item_id equals b.id into joined
                                from b in joined.Where(x => x.isdelete == 0).DefaultIfEmpty()
                                where a.type == "Radiology" && a.organizationId != 0
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
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}