using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_CPOE_Control_Template_RptMicroLab : System.Web.UI.UserControl
{
    public List<CpoeTrans> listchecked = new List<CpoeTrans>();
    public List<CpoeMapping> listmapping = new List<CpoeMapping>();
    public delegate bool customHandler(object sender);
    public event customHandler checkIfExistMicro;

    //public string setENG = "none";
    //public string setIND = "none";

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session[Helper.SessionLanguage] == "1")
        //{
        //    setENG = "";
        //    setIND = "none";
        //    HFisBahasa_microlab.Value = "ENG";
        //}
        //else if (Session[Helper.SessionLanguage] == "2")
        //{
        //    setENG = "none";
        //    setIND = "";
        //    HFisBahasa_microlab.Value = "IND";
        //}
        //else
        //{
        //    setENG = "";
        //    setIND = "none";
        //    HFisBahasa_microlab.Value = "ENG";
        //}

        //set bahasa
        //ScriptManager.RegisterStartupScript(this, GetType(), "bahasa", "switchBahasa_microlab();", true);

        //if (!IsPostBack)
        //{
        //    //GetMappingMicroLab(2, "MicroLab");
        //}
    }

    public void InitializeNotes(CpoeNotes cpoenotes)
    {
        txtMicroNotes.Text = cpoenotes.notes;
    }

    public List<CpoeNotes> getvaluesnotes(List<CpoeNotes> cpoenotes)
    {
        //bool alreadyExists = cpoenotes.Any(x => x.notes_type == "CitoLab");
        if (cpoenotes.Any(x => x.notes_type == "MicroLab"))
        {
            foreach (CpoeNotes tempvalues in cpoenotes)
            {
                if (tempvalues.notes_type == "MicroLab")
                {
                    tempvalues.notes = txtMicroNotes.Text;
                }
            }
        }
        else
        {
            CpoeNotes tempnotes = new CpoeNotes();
            tempnotes.cpoe_notes_id = Guid.Empty;
            tempnotes.notes_type = "MicroLab";
            tempnotes.notes = txtMicroNotes.Text;
            cpoenotes.Add(tempnotes);
        }
        return cpoenotes;
    }

    public void GetMappingMicroLab(List<CpoeMapping> listmap, string guidadditional, string flagFO)
    {
        DataTable dt = new DataTable();
        try
        {
            //log.Info(LogLibrary.Logging("S", "GetDataWorklist", txtUsername.Text.ToString(), ""));
            //var getMap = clsCpoeMapping.GetMapping(organizationId);
            //var getMapJson = JsonConvert.DeserializeObject<ResultMapping>(getMap.Result.ToString());
            hfguidadditional.Value = guidadditional;
            listmapping = listmap;
            if (Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] != null)
            {
                listchecked = (List<CpoeTrans>)Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value];
                string arrayString = "list = [];";
                foreach (var x in listchecked)
                {
                    if (x.FutureOrderDate != null)
                    {
                        arrayString = arrayString + "list.push({ id:" + x.id + " , name: '" + x.name + "',type: '" + x.type
                                        + "', remarks: '" + x.remarks + "', isnew: " + x.isnew + ", iscito: " + x.iscito + ", issubmit: " + x.issubmit
                                        + ",isdelete: " + x.isdelete + ", ischeck: " + x.ischeck + ", IsSendHope: " + x.IsSendHope
                                        + ", IsFutureOrder: " + x.IsFutureOrder.ToString().ToLower() + ", FutureOrderDate: '" + x.FutureOrderDate + "' });";
                    }
                    else
                    {
                        arrayString = arrayString + "list.push({ id:" + x.id + " , name: '" + x.name + "',type: '" + x.type
                                        + "', remarks: '" + x.remarks + "', isnew: " + x.isnew + ", iscito: " + x.iscito + ", issubmit: " + x.issubmit
                                        + ",isdelete: " + x.isdelete + ", ischeck: " + x.ischeck + ", IsSendHope: " + x.IsSendHope
                                        + ", IsFutureOrder: " + x.IsFutureOrder.ToString().ToLower() + ", FutureOrderDate: '"+ DateTime.Now +"' });";
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
                                where a.type == "MicroLab" && a.organizationId != 0
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
                                where a.type == "MicroLab" && a.organizationId != 0
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
                                    IsSendHope = b == null ? 0 : b.IsSendHope
                                }
                           ).Distinct().ToList();
                    }

                    dt = Helper.ToDataTable(data);
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

                    if (dt.Select("gridview_name = 'gvw4'").Count() > 0)
                    {
                        rpt4.DataSource = dt.Select("gridview_name = 'gvw4'").CopyToDataTable();
                        rpt4.DataBind();
                    }
                    else
                    {
                        rpt4.DataSource = null;
                        rpt4.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw5'").Count() > 0)
                    {
                        rpt5.DataSource = dt.Select("gridview_name = 'gvw5'").CopyToDataTable();
                        rpt5.DataBind();
                    }
                    else
                    {
                        rpt5.DataSource = null;
                        rpt5.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw6'").Count() > 0)
                    {
                        rpt6.DataSource = dt.Select("gridview_name = 'gvw6'").CopyToDataTable();
                        rpt6.DataBind();
                    }
                    else
                    {
                        rpt6.DataSource = null;
                        rpt6.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw7'").Count() > 0)
                    {
                        rpt7.DataSource = dt.Select("gridview_name = 'gvw7'").CopyToDataTable();
                        rpt7.DataBind();
                    }
                    else
                    {
                        rpt7.DataSource = null;
                        rpt7.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw8'").Count() > 0)
                    {
                        rpt8.DataSource = dt.Select("gridview_name = 'gvw8'").CopyToDataTable();
                        rpt8.DataBind();
                    }
                    else
                    {
                        rpt8.DataSource = null;
                        rpt8.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw9'").Count() > 0)
                    {
                        rpt9.DataSource = dt.Select("gridview_name = 'gvw9'").CopyToDataTable();
                        rpt9.DataBind();
                    }
                    else
                    {
                        rpt9.DataSource = null;
                        rpt9.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw10'").Count() > 0)
                    {
                        rpt10.DataSource = dt.Select("gridview_name = 'gvw10'").CopyToDataTable();
                        rpt10.DataBind();
                    }
                    else
                    {
                        rpt10.DataSource = null;
                        rpt10.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw11'").Count() > 0)
                    {
                        rpt11.DataSource = dt.Select("gridview_name = 'gvw11'").CopyToDataTable();
                        rpt11.DataBind();
                    }
                    else
                    {
                        rpt11.DataSource = null;
                        rpt11.DataBind();
                    }


                    if (dt.Select("gridview_name = 'gvw12'").Count() > 0)
                    {
                        rpt12.DataSource = dt.Select("gridview_name = 'gvw12'").CopyToDataTable();
                        rpt12.DataBind();
                    }
                    else
                    {
                        rpt12.DataSource = null;
                        rpt12.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw13'").Count() > 0)
                    {
                        rpt13.DataSource = dt.Select("gridview_name = 'gvw13'").CopyToDataTable();
                        rpt13.DataBind();
                    }
                    else
                    {
                        rpt13.DataSource = null;
                        rpt13.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw14'").Count() > 0)
                    {
                        rpt14.DataSource = dt.Select("gridview_name = 'gvw14'").CopyToDataTable();
                        rpt14.DataBind();
                    }
                    else
                    {
                        rpt14.DataSource = null;
                        rpt14.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw15'").Count() > 0)
                    {
                        rpt15.DataSource = dt.Select("gridview_name = 'gvw15'").CopyToDataTable();
                        rpt15.DataBind();
                    }
                    else
                    {
                        rpt15.DataSource = null;
                        rpt15.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw16'").Count() > 0)
                    {
                        rpt16.DataSource = dt.Select("gridview_name = 'gvw16'").CopyToDataTable();
                        rpt16.DataBind();
                    }
                    else
                    {
                        rpt16.DataSource = null;
                        rpt16.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw17'").Count() > 0)
                    {
                        rpt17.DataSource = dt.Select("gridview_name = 'gvw17'").CopyToDataTable();
                        rpt17.DataBind();
                    }
                    else
                    {
                        rpt17.DataSource = null;
                        rpt17.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw18'").Count() > 0)
                    {
                        rpt18.DataSource = dt.Select("gridview_name = 'gvw18'").CopyToDataTable();
                        rpt18.DataBind();
                    }
                    else
                    {
                        rpt18.DataSource = null;
                        rpt18.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw19'").Count() > 0)
                    {
                        rpt19.DataSource = dt.Select("gridview_name = 'gvw19'").CopyToDataTable();
                        rpt19.DataBind();
                    }
                    else
                    {
                        rpt19.DataSource = null;
                        rpt19.DataBind();
                    }
                }
            }
            else
            {
                listchecked = new List<CpoeTrans>();
                List<LabList> data = new List<LabList>();
                if (flagFO == "true")
                {
                        data = (
                                from a in listmapping
                                join b in listchecked on a.item_id equals b.id into joined
                                from b in joined.Where(x => x.isdelete == 0 && x.IsFutureOrder == true).DefaultIfEmpty()
                                where a.type == "MicroLab" && a.organizationId != 0
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
                                where a.type == "MicroLab" && a.organizationId != 0
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
                                    IsSendHope = b == null ? 0 : b.IsSendHope
                                }
                           ).Distinct().ToList();
                }
                if (listmapping.Count > 0)
                {
                        
                    dt = Helper.ToDataTable(data);
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

                    if (dt.Select("gridview_name = 'gvw4'").Count() > 0)
                    {
                        rpt4.DataSource = dt.Select("gridview_name = 'gvw4'").CopyToDataTable();
                        rpt4.DataBind();
                    }
                    else
                    {
                        rpt4.DataSource = null;
                        rpt4.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw5'").Count() > 0)
                    {
                        rpt5.DataSource = dt.Select("gridview_name = 'gvw5'").CopyToDataTable();
                        rpt5.DataBind();
                    }
                    else
                    {
                        rpt5.DataSource = null;
                        rpt5.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw6'").Count() > 0)
                    {
                        rpt6.DataSource = dt.Select("gridview_name = 'gvw6'").CopyToDataTable();
                        rpt6.DataBind();
                    }
                    else
                    {
                        rpt6.DataSource = null;
                        rpt6.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw7'").Count() > 0)
                    {
                        rpt7.DataSource = dt.Select("gridview_name = 'gvw7'").CopyToDataTable();
                        rpt7.DataBind();
                    }
                    else
                    {
                        rpt7.DataSource = null;
                        rpt7.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw8'").Count() > 0)
                    {
                        rpt8.DataSource = dt.Select("gridview_name = 'gvw8'").CopyToDataTable();
                        rpt8.DataBind();
                    }
                    else
                    {
                        rpt8.DataSource = null;
                        rpt8.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw9'").Count() > 0)
                    {
                        rpt9.DataSource = dt.Select("gridview_name = 'gvw9'").CopyToDataTable();
                        rpt9.DataBind();
                    }
                    else
                    {
                        rpt9.DataSource = null;
                        rpt9.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw10'").Count() > 0)
                    {
                        rpt10.DataSource = dt.Select("gridview_name = 'gvw10'").CopyToDataTable();
                        rpt10.DataBind();
                    }
                    else
                    {
                        rpt10.DataSource = null;
                        rpt10.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw11'").Count() > 0)
                    {
                        rpt11.DataSource = dt.Select("gridview_name = 'gvw11'").CopyToDataTable();
                        rpt11.DataBind();
                    }
                    else
                    {
                        rpt11.DataSource = null;
                        rpt11.DataBind();
                    }


                    if (dt.Select("gridview_name = 'gvw12'").Count() > 0)
                    {
                        rpt12.DataSource = dt.Select("gridview_name = 'gvw12'").CopyToDataTable();
                        rpt12.DataBind();
                    }
                    else
                    {
                        rpt12.DataSource = null;
                        rpt12.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw13'").Count() > 0)
                    {
                        rpt13.DataSource = dt.Select("gridview_name = 'gvw13'").CopyToDataTable();
                        rpt13.DataBind();
                    }
                    else
                    {
                        rpt13.DataSource = null;
                        rpt13.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw14'").Count() > 0)
                    {
                        rpt14.DataSource = dt.Select("gridview_name = 'gvw14'").CopyToDataTable();
                        rpt14.DataBind();
                    }
                    else
                    {
                        rpt14.DataSource = null;
                        rpt14.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw15'").Count() > 0)
                    {
                        rpt15.DataSource = dt.Select("gridview_name = 'gvw15'").CopyToDataTable();
                        rpt15.DataBind();
                    }
                    else
                    {
                        rpt15.DataSource = null;
                        rpt15.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw16'").Count() > 0)
                    {
                        rpt16.DataSource = dt.Select("gridview_name = 'gvw16'").CopyToDataTable();
                        rpt16.DataBind();
                    }
                    else
                    {
                        rpt16.DataSource = null;
                        rpt16.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw17'").Count() > 0)
                    {
                        rpt17.DataSource = dt.Select("gridview_name = 'gvw17'").CopyToDataTable();
                        rpt17.DataBind();
                    }
                    else
                    {
                        rpt17.DataSource = null;
                        rpt17.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw18'").Count() > 0)
                    {
                        rpt18.DataSource = dt.Select("gridview_name = 'gvw18'").CopyToDataTable();
                        rpt18.DataBind();
                    }
                    else
                    {
                        rpt18.DataSource = null;
                        rpt18.DataBind();
                    }

                    if (dt.Select("gridview_name = 'gvw19'").Count() > 0)
                    {
                        rpt19.DataSource = dt.Select("gridview_name = 'gvw19'").CopyToDataTable();
                        rpt19.DataBind();
                    }
                    else
                    {
                        rpt19.DataSource = null;
                        rpt19.DataBind();
                    }
                }
                //DataTable dt = Helper.ToDataTable(data);
                //DataRow[] results = dt.Select("gridview_name = 'gvw1'");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}