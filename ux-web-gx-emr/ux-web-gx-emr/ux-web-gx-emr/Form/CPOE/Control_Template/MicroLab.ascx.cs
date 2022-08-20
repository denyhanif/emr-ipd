using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_CPOE_Control_Template_MicroLab : System.Web.UI.UserControl
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

    public void GetMappingMicroLab(List<CpoeMapping> listmap, string guidadditional)
    {
        DataTable dt;
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
                    arrayString = arrayString + "list.push({ id:" + x.id + " , name: '" + x.name + "',type: '" + x.type
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