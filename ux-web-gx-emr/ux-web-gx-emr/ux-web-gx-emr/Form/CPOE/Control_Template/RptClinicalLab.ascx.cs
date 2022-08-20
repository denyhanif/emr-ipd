using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

public partial class Form_CPOE_Control_Template_RptClinicalLab : System.Web.UI.UserControl
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
		//    HFisBahasa_clinicallab.Value = "ENG";
		//}
		//else if (Session[Helper.SessionLanguage] == "2")
		//{
		//    setENG = "none";
		//    setIND = "";
		//    HFisBahasa_clinicallab.Value = "IND";
		//}
		//else
		//{
		//    setENG = "";
		//    setIND = "none";
		//    HFisBahasa_clinicallab.Value = "ENG";
		//}

		//set bahasa
		//ScriptManager.RegisterStartupScript(this, GetType(), "bahasa", "switchBahasa_clinicallab();", true);

		//if (!IsPostBack)
		//{
		//    //GetMappingClinicLab(2, "cliniclab");
		//}
	}
	public delegate bool customHandler(object sender);
	public event customHandler checkIfExist;

	//protected void btnGetValueCPOE(object sender, EventArgs e)
	//{
	//    string objectcpoe = hfbuilderobject.Value;
	//    if (objectcpoe.Length > 0)
	//    {
	//        objectcpoe = objectcpoe.Remove(objectcpoe.Length - 1, 1);
	//        objectcpoe = "[" + objectcpoe + "]";
	//        List<CpoeTrans> tempcurrmed = new JavaScriptSerializer().Deserialize<List<CpoeTrans>>(objectcpoe);

	//        var duplicatedlab =
	//                        from p in tempcurrmed
	//                        group p by p.id into g
	//                        where g.Count() > 1
	//                        select g.Key;

	//        if (duplicatedlab.Count() > 0)
	//        {
	//            tempcurrmed = tempcurrmed.GroupBy(i => i.id).Select(g => g.First()).ToList();
	//        }

	//        //Button chk = (Button)sender;
	//        Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = tempcurrmed;
	//        checkIfExist(sender);
	//    }
	//    else
	//    {
	//        //Button chk = (Button)sender;
	//        Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = null;
	//        checkIfExist(sender);
	//    }
	//}

	public void resetlist()
	{
		//ScriptManager.RegisterStartupScript(this, this.GetType(), "resetlab", "alert('123');", true);
		ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "resetlab", "resetlab();", true);
		ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "getobjectlist", " return getobjectlist();", true);
	}

	//public void updateresetplanning()
	//{
	//    string objectcpoe = hfbuilderobject.Value;
	//    if (objectcpoe.Length > 0)
	//    {
	//        objectcpoe = objectcpoe.Remove(objectcpoe.Length - 1, 1);
	//        objectcpoe = "[" + objectcpoe + "]";
	//        List<CpoeTrans> tempcurrmed = new JavaScriptSerializer().Deserialize<List<CpoeTrans>>(objectcpoe);

	//        Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = tempcurrmed;
	//    }
	//    else
	//    {
	//        Session[Helper.SessionLabPathologyChecked + hfguidadditional.Value] = null;
	//    }
	//}

	protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
	{

	}

	public void InitializeNotes(CpoeNotes cpoenotes)
	{
		txtClinicNotex.Text = cpoenotes.notes;
	}

	public List<CpoeNotes> getvaluesnotes(List<CpoeNotes> cpoenotes)
	{
		//bool alreadyExists = cpoenotes.Any(x => x.notes_type == "CitoLab");
		if (cpoenotes.Any(x => x.notes_type == "ClinicLab"))
		{
			foreach (CpoeNotes tempvalues in cpoenotes)
			{
				if (tempvalues.notes_type == "ClinicLab")
				{
					tempvalues.notes = txtClinicNotex.Text;
				}
			}
		}
		else
		{
			CpoeNotes tempnotes = new CpoeNotes();
			tempnotes.cpoe_notes_id = Guid.Empty;
			tempnotes.notes_type = "ClinicLab";
			tempnotes.notes = txtClinicNotex.Text;
			cpoenotes.Add(tempnotes);
		}
		return cpoenotes;
	}

	public void GetMappingClinicLab(List<CpoeMapping> listmap, string guidadditional, string flagFO)
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
								from b in joined.Where(x => x.iscito == 0 && x.isdelete == 0 && x.IsFutureOrder == true).DefaultIfEmpty()
								where a.type == "ClinicLab" && a.organizationId != 0
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
									from b in joined.Where(x => x.iscito == 0 && x.isdelete == 0 && x.IsFutureOrder == false).DefaultIfEmpty()
									where a.type == "ClinicLab" && a.organizationId != 0
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
										fasting_flag = a.fasting_flag,
										IsSendHope = b == null ? 0 : b.IsSendHope
									}
							   ).Distinct().ToList();
					
					}
					//dt = Helper.ToDataTable(data.Where(x => x.IsFutureOrder == true).ToList());
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

					if (dt.Select("gridview_name = 'gvw20'").Count() > 0)
					{
						rpt20.DataSource = dt.Select("gridview_name = 'gvw20'").CopyToDataTable();
						rpt20.DataBind();
					}
					else
					{
						rpt20.DataSource = null;
						rpt20.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw21'").Count() > 0)
					{
						rpt21.DataSource = dt.Select("gridview_name = 'gvw21'").CopyToDataTable();
						rpt21.DataBind();
					}
					else
					{
						rpt21.DataSource = null;
						rpt21.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw22'").Count() > 0)
					{
						rpt22.DataSource = dt.Select("gridview_name = 'gvw22'").CopyToDataTable();
						rpt22.DataBind();
					}
					else
					{
						rpt22.DataSource = null;
						rpt22.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw23'").Count() > 0)
					{
						rpt23.DataSource = dt.Select("gridview_name = 'gvw23'").CopyToDataTable();
						rpt23.DataBind();
					}
					else
					{
						rpt23.DataSource = null;
						rpt23.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw24'").Count() > 0)
					{
						rpt24.DataSource = dt.Select("gridview_name = 'gvw24'").CopyToDataTable();
						rpt24.DataBind();
					}
					else
					{
						rpt24.DataSource = null;
						rpt24.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw25'").Count() > 0)
					{
						rpt25.DataSource = dt.Select("gridview_name = 'gvw25'").CopyToDataTable();
						rpt25.DataBind();
					}
					else
					{
						rpt25.DataSource = null;
						rpt25.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw26'").Count() > 0)
					{
						rpt26.DataSource = dt.Select("gridview_name = 'gvw26'").CopyToDataTable();
						rpt26.DataBind();
					}
					else
					{
						rpt26.DataSource = null;
						rpt26.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw27'").Count() > 0)
					{
						rpt27.DataSource = dt.Select("gridview_name = 'gvw27'").CopyToDataTable();
						rpt27.DataBind();
					}
					else
					{
						rpt27.DataSource = null;
						rpt27.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw28'").Count() > 0)
					{
						rpt28.DataSource = dt.Select("gridview_name = 'gvw28'").CopyToDataTable();
						rpt28.DataBind();
					}
					else
					{
						rpt28.DataSource = null;
						rpt28.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw29'").Count() > 0)
					{
						rpt29.DataSource = dt.Select("gridview_name = 'gvw29'").CopyToDataTable();
						rpt29.DataBind();
					}
					else
					{
						rpt29.DataSource = null;
						rpt29.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw30'").Count() > 0)
					{
						rpt30.DataSource = dt.Select("gridview_name = 'gvw30'").CopyToDataTable();
						rpt30.DataBind();
					}
					else
					{
						rpt30.DataSource = null;
						rpt30.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw31'").Count() > 0)
					{
						rpt31.DataSource = dt.Select("gridview_name = 'gvw31'").CopyToDataTable();
						rpt31.DataBind();
					}
					else
					{
						rpt31.DataSource = null;
						rpt31.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw32'").Count() > 0)
					{
						rpt32.DataSource = dt.Select("gridview_name = 'gvw32'").CopyToDataTable();
						rpt32.DataBind();
					}
					else
					{
						rpt32.DataSource = null;
						rpt32.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw33'").Count() > 0)
					{
						rpt33.DataSource = dt.Select("gridview_name = 'gvw33'").CopyToDataTable();
						rpt33.DataBind();
					}
					else
					{
						rpt33.DataSource = null;
						rpt33.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw34'").Count() > 0)
					{
						rpt34.DataSource = dt.Select("gridview_name = 'gvw34'").CopyToDataTable();
						rpt34.DataBind();
					}
					else
					{
						rpt34.DataSource = null;
						rpt34.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw35'").Count() > 0)
					{
						rpt35.DataSource = dt.Select("gridview_name = 'gvw35'").CopyToDataTable();
						rpt35.DataBind();
					}
					else
					{
						rpt35.DataSource = null;
						rpt35.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw36'").Count() > 0)
					{
						rpt36.DataSource = dt.Select("gridview_name = 'gvw36'").CopyToDataTable();
						rpt36.DataBind();
					}
					else
					{
						rpt36.DataSource = null;
						rpt36.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw37'").Count() > 0)
					{
						rpt37.DataSource = dt.Select("gridview_name = 'gvw37'").CopyToDataTable();
						rpt37.DataBind();
					}
					else
					{
						rpt37.DataSource = null;
						rpt37.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw38'").Count() > 0)
					{
						rpt38.DataSource = dt.Select("gridview_name = 'gvw38'").CopyToDataTable();
						rpt38.DataBind();
					}
					else
					{
						rpt38.DataSource = null;
						rpt38.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw39'").Count() > 0)
					{
						rpt39.DataSource = dt.Select("gridview_name = 'gvw39'").CopyToDataTable();
						rpt39.DataBind();
					}
					else
					{
						rpt39.DataSource = null;
						rpt39.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw40'").Count() > 0)
					{
						rpt40.DataSource = dt.Select("gridview_name = 'gvw40'").CopyToDataTable();
						rpt40.DataBind();
					}
					else
					{
						rpt40.DataSource = null;
						rpt40.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw41'").Count() > 0)
					{
						rpt41.DataSource = dt.Select("gridview_name = 'gvw41'").CopyToDataTable();
						rpt41.DataBind();
					}
					else
					{
						rpt41.DataSource = null;
						rpt41.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw42'").Count() > 0)
					{
						rpt42.DataSource = dt.Select("gridview_name = 'gvw42'").CopyToDataTable();
						rpt42.DataBind();
					}
					else
					{
						rpt42.DataSource = null;
						rpt42.DataBind();
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
							   from b in joined.Where(x => x.iscito == 0 && x.isdelete == 0 && x.IsFutureOrder == true).DefaultIfEmpty()
							   where a.type == "ClinicLab" && a.organizationId != 0
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
								from b in joined.Where(x => x.iscito == 0 && x.isdelete == 0 && x.IsFutureOrder == false).DefaultIfEmpty()
								where a.type == "ClinicLab" && a.organizationId != 0
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

					if (dt.Select("gridview_name = 'gvw20'").Count() > 0)
					{
						rpt20.DataSource = dt.Select("gridview_name = 'gvw20'").CopyToDataTable();
						rpt20.DataBind();
					}
					else
					{
						rpt20.DataSource = null;
						rpt20.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw21'").Count() > 0)
					{
						rpt21.DataSource = dt.Select("gridview_name = 'gvw21'").CopyToDataTable();
						rpt21.DataBind();
					}
					else
					{
						rpt21.DataSource = null;
						rpt21.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw22'").Count() > 0)
					{
						rpt22.DataSource = dt.Select("gridview_name = 'gvw22'").CopyToDataTable();
						rpt22.DataBind();
					}
					else
					{
						rpt22.DataSource = null;
						rpt22.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw23'").Count() > 0)
					{
						rpt23.DataSource = dt.Select("gridview_name = 'gvw23'").CopyToDataTable();
						rpt23.DataBind();
					}
					else
					{
						rpt23.DataSource = null;
						rpt23.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw24'").Count() > 0)
					{
						rpt24.DataSource = dt.Select("gridview_name = 'gvw24'").CopyToDataTable();
						rpt24.DataBind();
					}
					else
					{
						rpt24.DataSource = null;
						rpt24.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw25'").Count() > 0)
					{
						rpt25.DataSource = dt.Select("gridview_name = 'gvw25'").CopyToDataTable();
						rpt25.DataBind();
					}
					else
					{
						rpt25.DataSource = null;
						rpt25.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw26'").Count() > 0)
					{
						rpt26.DataSource = dt.Select("gridview_name = 'gvw26'").CopyToDataTable();
						rpt26.DataBind();
					}
					else
					{
						rpt26.DataSource = null;
						rpt26.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw27'").Count() > 0)
					{
						rpt27.DataSource = dt.Select("gridview_name = 'gvw27'").CopyToDataTable();
						rpt27.DataBind();
					}
					else
					{
						rpt27.DataSource = null;
						rpt27.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw28'").Count() > 0)
					{
						rpt28.DataSource = dt.Select("gridview_name = 'gvw28'").CopyToDataTable();
						rpt28.DataBind();
					}
					else
					{
						rpt28.DataSource = null;
						rpt28.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw29'").Count() > 0)
					{
						rpt29.DataSource = dt.Select("gridview_name = 'gvw29'").CopyToDataTable();
						rpt29.DataBind();
					}
					else
					{
						rpt29.DataSource = null;
						rpt29.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw30'").Count() > 0)
					{
						rpt30.DataSource = dt.Select("gridview_name = 'gvw30'").CopyToDataTable();
						rpt30.DataBind();
					}
					else
					{
						rpt30.DataSource = null;
						rpt30.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw31'").Count() > 0)
					{
						rpt31.DataSource = dt.Select("gridview_name = 'gvw31'").CopyToDataTable();
						rpt31.DataBind();
					}
					else
					{
						rpt31.DataSource = null;
						rpt31.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw32'").Count() > 0)
					{
						rpt32.DataSource = dt.Select("gridview_name = 'gvw32'").CopyToDataTable();
						rpt32.DataBind();
					}
					else
					{
						rpt32.DataSource = null;
						rpt32.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw33'").Count() > 0)
					{
						rpt33.DataSource = dt.Select("gridview_name = 'gvw33'").CopyToDataTable();
						rpt33.DataBind();
					}
					else
					{
						rpt33.DataSource = null;
						rpt33.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw34'").Count() > 0)
					{
						rpt34.DataSource = dt.Select("gridview_name = 'gvw34'").CopyToDataTable();
						rpt34.DataBind();
					}
					else
					{
						rpt34.DataSource = null;
						rpt34.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw35'").Count() > 0)
					{
						rpt35.DataSource = dt.Select("gridview_name = 'gvw35'").CopyToDataTable();
						rpt35.DataBind();
					}
					else
					{
						rpt35.DataSource = null;
						rpt35.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw36'").Count() > 0)
					{
						rpt36.DataSource = dt.Select("gridview_name = 'gvw36'").CopyToDataTable();
						rpt36.DataBind();
					}
					else
					{
						rpt36.DataSource = null;
						rpt36.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw37'").Count() > 0)
					{
						rpt37.DataSource = dt.Select("gridview_name = 'gvw37'").CopyToDataTable();
						rpt37.DataBind();
					}
					else
					{
						rpt37.DataSource = null;
						rpt37.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw38'").Count() > 0)
					{
						rpt38.DataSource = dt.Select("gridview_name = 'gvw38'").CopyToDataTable();
						rpt38.DataBind();
					}
					else
					{
						rpt38.DataSource = null;
						rpt38.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw39'").Count() > 0)
					{
						rpt39.DataSource = dt.Select("gridview_name = 'gvw39'").CopyToDataTable();
						rpt39.DataBind();
					}
					else
					{
						rpt39.DataSource = null;
						rpt39.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw40'").Count() > 0)
					{
						rpt40.DataSource = dt.Select("gridview_name = 'gvw40'").CopyToDataTable();
						rpt40.DataBind();
					}
					else
					{
						rpt40.DataSource = null;
						rpt40.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw41'").Count() > 0)
					{
						rpt41.DataSource = dt.Select("gridview_name = 'gvw41'").CopyToDataTable();
						rpt41.DataBind();
					}
					else
					{
						rpt41.DataSource = null;
						rpt41.DataBind();
					}

					if (dt.Select("gridview_name = 'gvw42'").Count() > 0)
					{
						rpt42.DataSource = dt.Select("gridview_name = 'gvw42'").CopyToDataTable();
						rpt42.DataBind();
					}
					else
					{
						rpt42.DataSource = null;
						rpt42.DataBind();
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