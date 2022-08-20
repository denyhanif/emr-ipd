using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static PatientHistory;

public partial class Form_SOAP_Control_Template_StdImunisasi : System.Web.UI.UserControl
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            List<Vaccine> listVaccine = new List<Vaccine>();
            DataTable dt_vaccine_dws = new DataTable();
            DataTable dt_vaccine_ank = new DataTable();

            //Log.Debug(LogConfig.LogStart("getVaccine"));
            var vaccineData = clsSOAP.getVaccine();
            var JsonVaccine = JsonConvert.DeserializeObject<ResultVaccine>(vaccineData.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("getVaccine", JsonVaccine.Status, JsonVaccine.Message));

            listVaccine = JsonVaccine.list;

            //... Dewasa
            dt_vaccine_dws = Helper.ToDataTable(listVaccine).Select("(vaccine_flag = 2) AND is_active = " + true).CopyToDataTable(); //dewasa : 2
            if (RepeaterImunisasiDewasa.Items.Count == 0)
            {
                RepeaterImunisasiDewasa.DataSource = dt_vaccine_dws;
                RepeaterImunisasiDewasa.DataBind();

                //for kalender dewasa
                GvwKalenderImunisasiDewasa.DataSource = dt_vaccine_dws;
                GvwKalenderImunisasiDewasa.DataBind();

                Session[Helper.SessionVaccineDWS] = dt_vaccine_dws;
            }

            //... Anak
            dt_vaccine_ank = Helper.ToDataTable(listVaccine).Select("(vaccine_flag = 1) AND is_active = " + true).CopyToDataTable(); //anak : 1
            if (RepeaterImunisasiAnak.Items.Count == 0)
            {
                RepeaterImunisasiAnak.DataSource = dt_vaccine_ank;
                RepeaterImunisasiAnak.DataBind();

                //for kalender anak
                GvwKalenderImunisasiAnak.DataSource = dt_vaccine_ank;
                GvwKalenderImunisasiAnak.DataBind();

                Session[Helper.SessionVaccineANK] = dt_vaccine_ank;
            }

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "Page_Load", StartTime, EndTime, "OK", MyUser.GetUsername()
                          , "", "", ""));
            //Log.Info(LogConfig.LogEnd());
        }

    }

    public void initializevalue(dynamic Jsongetsoap, PatientHeader header, string guidadditional)
    {

        hfguidadditional.Value = guidadditional;
        if (Jsongetsoap.list.vaccination != null)
        {
            Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] = Helper.ToDataTable(Jsongetsoap.list.vaccination);
            Session[Helper.SessionDataDetailVaksinasi_Save + hfguidadditional.Value] = Helper.ToDataTable(Jsongetsoap.list.vaccination);
        }
        else
        {
            Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] = null;
            Session[Helper.SessionDataDetailVaksinasi_Save + hfguidadditional.Value] = null;
        }

        LabelNamaPasienVaccine.Text = header.PatientName;
        LabelAgePasienVaccine.Text = clsCommon.GetAge(header.BirthDate);

        if (header.Gender == 1)
        {
            imgSex.ImageUrl = "~/Images/Icon/ic_Male.svg";
        }
        else if (header.Gender == 2)
        {
            imgSex.ImageUrl = "~/Images/Icon/ic_Female.svg";
        }
    }

    //... FOR TABLE ...//

    protected List<Vaccination> GetRowListVaccination()
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); 
        //Log.Info(LogConfig.LogStart());

        List<Vaccination> data = new List<Vaccination>();
        try
        {
            for (int i = 0; i < RepeaterImunisasiDewasa.Items.Count; i++)
            {
                GridView gvw_imunisasiDws = (GridView)RepeaterImunisasiDewasa.Items[i].FindControl("GridViewImunisasiDewasa");
                if (gvw_imunisasiDws.Rows.Count != 0)
                {
                    foreach (GridViewRow rows in gvw_imunisasiDws.Rows)
                    {
                        HiddenField hf_vaccinationid = (HiddenField)rows.FindControl("HF_vaccinationid");
                        HiddenField hf_vaccineid = (HiddenField)rows.FindControl("HF_vaccineid");
                        //HiddenField hf_doctorid = (HiddenField)rows.FindControl("HF_doctorid");
                        HiddenField hf_vaccinationage = (HiddenField)rows.FindControl("HF_vaccinationage");

                        Label teks_seq = (Label)rows.FindControl("Lbl_noSequenceDws");
                        TextBox teks_tglimunisasi = (TextBox)rows.FindControl("Txt_tglImunisasiDws");
                        TextBox teks_namadokter = (TextBox)rows.FindControl("Txt_namaDokterDws");
                        TextBox teks_tglexpired = (TextBox)rows.FindControl("Txt_expDateDws");
                        TextBox teks_nolot = (TextBox)rows.FindControl("Txt_noLotDws");

                        Vaccination row = new Vaccination();
                        row.vaccination_id = Guid.Parse(hf_vaccinationid.Value);
                        row.vaccine_id = long.Parse(hf_vaccineid.Value);
                        //row.doctor_id = long.Parse(hf_doctorid.Value);
                        row.doctor_name = teks_namadokter.Text.ToString();
                        row.vaccination_sequence = (short)int.Parse(teks_seq.Text.ToString());
                        row.vaccination_date = teks_tglimunisasi.Text.ToString();
                        row.vaccination_age = hf_vaccinationage.Value;
                        row.expiry_date = teks_tglexpired.Text.ToString();
                        row.no_lot = teks_nolot.Text.ToString();

                        data.Add(row);
                    }
                }
            }

            for (int i = 0; i < RepeaterImunisasiAnak.Items.Count; i++)
            {
                GridView gvw_imunisasiAnk = (GridView)RepeaterImunisasiAnak.Items[i].FindControl("GridViewImunisasiAnak");
                if (gvw_imunisasiAnk.Rows.Count != 0)
                {
                    foreach (GridViewRow rows in gvw_imunisasiAnk.Rows)
                    {
                        HiddenField hf_vaccinationid = (HiddenField)rows.FindControl("HF_vaccinationid");
                        HiddenField hf_vaccineid = (HiddenField)rows.FindControl("HF_vaccineid");
                        //HiddenField hf_doctorid = (HiddenField)rows.FindControl("HF_doctorid");
                        HiddenField hf_vaccinationage = (HiddenField)rows.FindControl("HF_vaccinationage");

                        Label teks_seq = (Label)rows.FindControl("Lbl_noSequenceAnk");
                        TextBox teks_tglimunisasi = (TextBox)rows.FindControl("Txt_tglImunisasiAnk");
                        TextBox teks_namadokter = (TextBox)rows.FindControl("Txt_namaDokterAnk");
                        TextBox teks_tglexpired = (TextBox)rows.FindControl("Txt_expDateAnk");
                        TextBox teks_nolot = (TextBox)rows.FindControl("Txt_noLotAnk");

                        Vaccination row = new Vaccination();
                        row.vaccination_id = Guid.Parse(hf_vaccinationid.Value);
                        row.vaccine_id = long.Parse(hf_vaccineid.Value);
                        //row.doctor_id = long.Parse(hf_doctorid.Value);
                        row.doctor_name = teks_namadokter.Text.ToString();
                        row.vaccination_sequence = (short)int.Parse(teks_seq.Text.ToString());
                        row.vaccination_date = teks_tglimunisasi.Text.ToString();
                        row.vaccination_age = hf_vaccinationage.Value;
                        row.expiry_date = teks_tglexpired.Text.ToString();
                        row.no_lot = teks_nolot.Text.ToString();

                        data.Add(row);
                    }
                }
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetRowListVaccination", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetRowListVaccination", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());

        return data;
    }


    //... Dewasa Section
    protected void LB_addnewrowdewasa_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        DataTable selectedGridData = new DataTable();
        DataTable allGridData = new DataTable();

        int selItemIndex = ((sender as Button).NamingContainer as RepeaterItem).ItemIndex;
        HiddenField hf_vaccineid = (HiddenField)RepeaterImunisasiDewasa.Items[selItemIndex].FindControl("HF_imunisasiIdDws");

        List<Vaccination> datadetail = GetRowListVaccination();
        DataTable dt_alldataimunisasi = Helper.ToDataTable(datadetail);
        Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] = dt_alldataimunisasi;

        GridView gvw_imunisasiDws = ((sender as Button).NamingContainer as RepeaterItem).FindControl("GridViewImunisasiDewasa") as GridView;

        //process to each grid data

        if (gvw_imunisasiDws.Rows.Count == 0)
        {
            selectedGridData.Columns.Add("vaccination_id");
            selectedGridData.Columns.Add("vaccine_id");
            //selectedGridData.Columns.Add("doctor_id");
            selectedGridData.Columns.Add("doctor_name");
            selectedGridData.Columns.Add("vaccination_sequence");
            selectedGridData.Columns.Add("vaccination_date");
            selectedGridData.Columns.Add("vaccination_age");
            selectedGridData.Columns.Add("expiry_date");
            selectedGridData.Columns.Add("no_lot");
        }
        else
        {
            selectedGridData = ((DataTable)Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value]).Select("vaccine_id = " + hf_vaccineid.Value).CopyToDataTable();
        }

        int item_seq = Convert.ToInt32(selectedGridData.AsEnumerable().Max(row => row["vaccination_sequence"]));

        DataRow temp_selectedData = selectedGridData.NewRow();
        temp_selectedData["vaccination_id"] = Guid.Empty;
        temp_selectedData["vaccine_id"] = hf_vaccineid.Value;
        //temp_selectedData["doctor_id"] = int.Parse(Helper.GetDoctorID(this.Page));
        temp_selectedData["doctor_name"] = Helper.GetUserFullname(this.Page);
        temp_selectedData["vaccination_sequence"] = (short)(item_seq + 1); ;
        temp_selectedData["vaccination_date"] = DateTime.Now.ToString("dd/MM/yyyy");
        temp_selectedData["vaccination_age"] = "";
        temp_selectedData["expiry_date"] = "";
        temp_selectedData["no_lot"] = "";

        selectedGridData.Rows.Add(temp_selectedData);
        gvw_imunisasiDws.DataSource = selectedGridData;
        gvw_imunisasiDws.DataBind();

        //process to all data

        if (Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] == null)
        {
            allGridData.Columns.Add("vaccination_id");
            allGridData.Columns.Add("vaccine_id");
            //allGridData.Columns.Add("doctor_id");
            allGridData.Columns.Add("doctor_name");
            allGridData.Columns.Add("vaccination_sequence");
            allGridData.Columns.Add("vaccination_date");
            allGridData.Columns.Add("vaccination_age");
            allGridData.Columns.Add("expiry_date");
            allGridData.Columns.Add("no_lot");
        }
        else
        {
            allGridData = (DataTable)Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value];
        }

        DataRow temp_allData = allGridData.NewRow();
        temp_allData["vaccination_id"] = Guid.Empty;
        temp_allData["vaccine_id"] = hf_vaccineid.Value;
        //temp_allData["doctor_id"] = int.Parse(Helper.GetDoctorID(this.Page));
        temp_allData["doctor_name"] = Helper.GetUserFullname(this.Page);
        temp_allData["vaccination_sequence"] = (short)(item_seq + 1); ;
        temp_allData["vaccination_date"] = DateTime.Now.ToString("dd/MM/yyyy");
        temp_allData["vaccination_age"] = "";
        temp_allData["expiry_date"] = "";
        temp_allData["no_lot"] = "";
        allGridData.Rows.Add(temp_allData);

        Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] = allGridData;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "LB_addnewrowdewasa_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());
    }

    protected void RepeaterImunisasiDewasa_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RepeaterItem item = e.Item;

            string headerId = (item.FindControl("HF_imunisasiIdDws") as HiddenField).Value;
            GridView gv_imunisasi = item.FindControl("GridViewImunisasiDewasa") as GridView;

            if (Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] != null)
            {
                DataTable alldata = (DataTable)Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value];
                if (alldata.Select("vaccine_id = " + headerId).Count() > 0)
                {
                    DataTable dataVaksinasi = alldata.Select("vaccine_id = " + headerId).CopyToDataTable();
                    dataVaksinasi.DefaultView.Sort = "vaccination_sequence asc";
                    dataVaksinasi = dataVaksinasi.DefaultView.ToTable();

                    gv_imunisasi.DataSource = dataVaksinasi;
                    gv_imunisasi.DataBind();
                }
                else
                {
                    gv_imunisasi.DataSource = null;
                    gv_imunisasi.DataBind();
                }
            }
        }
    }

    protected void btndelete_ImunisasiDws_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        DataTable selectedGridData = new DataTable();
        int selItemIndex = ((RepeaterItem)(((ImageButton)sender).Parent.Parent.Parent.Parent.NamingContainer)).ItemIndex;
        //int selItemIndex = ((sender as LinkButton).NamingContainer as RepeaterItem).ItemIndex;       
        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        GridView gvw_imunisasiDws = (GridView)RepeaterImunisasiDewasa.Items[selItemIndex].FindControl("GridViewImunisasiDewasa");

        HiddenField hf_vaccineid = (HiddenField)RepeaterImunisasiDewasa.Items[selItemIndex].FindControl("HF_imunisasiIdDws");
        Label txt_seq = (Label)gvw_imunisasiDws.Rows[selRowIndex].FindControl("Lbl_noSequenceDws");

        //DataTable dt_alldataimunisasi = (DataTable)Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value];
        List<Vaccination> datadetail = GetRowListVaccination();
        DataTable dt_alldataimunisasi = Helper.ToDataTable(datadetail);
        for (int i = 0; i < dt_alldataimunisasi.Rows.Count; i++)
        {
            DataRow dr = dt_alldataimunisasi.Rows[i];
            if (dr["vaccine_id"].ToString() == hf_vaccineid.Value && dr["vaccination_sequence"].ToString() == txt_seq.Text)
            {
                dr.Delete();
            }
        }
        dt_alldataimunisasi.AcceptChanges();

        if (dt_alldataimunisasi.Select("vaccine_id = " + hf_vaccineid.Value).Count() > 0)
        {
            selectedGridData = dt_alldataimunisasi.Select("vaccine_id = " + hf_vaccineid.Value).CopyToDataTable();
            gvw_imunisasiDws.DataSource = selectedGridData;
            gvw_imunisasiDws.DataBind();
        }
        else
        {
            gvw_imunisasiDws.DataSource = null;
            gvw_imunisasiDws.DataBind();
        }

        Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] = dt_alldataimunisasi;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btndelete_ImunisasiDws_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());

        //GridView gvw_imunisasiDws = ((sender as LinkButton).NamingContainer as RepeaterItem).FindControl("GridViewImunisasiDewasa") as GridView;
    }


    //... Anak Section
    protected void LB_addnewrowanak_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        DataTable selectedGridData = new DataTable();
        DataTable allGridData = new DataTable();

        int selItemIndex = ((sender as Button).NamingContainer as RepeaterItem).ItemIndex;
        HiddenField hf_vaccineid = (HiddenField)RepeaterImunisasiAnak.Items[selItemIndex].FindControl("HF_imunisasiIdAnk");

        List<Vaccination> datadetail = GetRowListVaccination();
        DataTable dt_alldataimunisasi = Helper.ToDataTable(datadetail);
        Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] = dt_alldataimunisasi;

        GridView gvw_imunisasiAnk = ((sender as Button).NamingContainer as RepeaterItem).FindControl("GridViewImunisasiAnak") as GridView;

        //process to each grid data

        if (gvw_imunisasiAnk.Rows.Count == 0)
        {
            selectedGridData.Columns.Add("vaccination_id");
            selectedGridData.Columns.Add("vaccine_id");
            //selectedGridData.Columns.Add("doctor_id");
            selectedGridData.Columns.Add("doctor_name");
            selectedGridData.Columns.Add("vaccination_sequence");
            selectedGridData.Columns.Add("vaccination_date");
            selectedGridData.Columns.Add("vaccination_age");
            selectedGridData.Columns.Add("expiry_date");
            selectedGridData.Columns.Add("no_lot");
        }
        else
        {
            selectedGridData = ((DataTable)Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value]).Select("vaccine_id = " + hf_vaccineid.Value).CopyToDataTable();
        }

        int item_seq = Convert.ToInt32(selectedGridData.AsEnumerable().Max(row => row["vaccination_sequence"]));

        DataRow temp_selectedData = selectedGridData.NewRow();
        temp_selectedData["vaccination_id"] = Guid.Empty;
        temp_selectedData["vaccine_id"] = hf_vaccineid.Value;
        //temp_selectedData["doctor_id"] = int.Parse(Helper.GetDoctorID(this.Page));
        temp_selectedData["doctor_name"] = Helper.GetUserFullname(this.Page);
        temp_selectedData["vaccination_sequence"] = (short)(item_seq + 1); ;
        temp_selectedData["vaccination_date"] = DateTime.Now.ToString("dd/MM/yyyy");
        temp_selectedData["vaccination_age"] = "";
        temp_selectedData["expiry_date"] = "";
        temp_selectedData["no_lot"] = "";

        selectedGridData.Rows.Add(temp_selectedData);
        gvw_imunisasiAnk.DataSource = selectedGridData;
        gvw_imunisasiAnk.DataBind();

        //process to all data

        if (Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] == null)
        {
            allGridData.Columns.Add("vaccination_id");
            allGridData.Columns.Add("vaccine_id");
            //allGridData.Columns.Add("doctor_id");
            allGridData.Columns.Add("doctor_name");
            allGridData.Columns.Add("vaccination_sequence");
            allGridData.Columns.Add("vaccination_date");
            allGridData.Columns.Add("vaccination_age");
            allGridData.Columns.Add("expiry_date");
            allGridData.Columns.Add("no_lot");
        }
        else
        {
            allGridData = (DataTable)Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value];
        }

        DataRow temp_allData = allGridData.NewRow();
        temp_allData["vaccination_id"] = Guid.Empty;
        temp_allData["vaccine_id"] = hf_vaccineid.Value;
        //temp_allData["doctor_id"] = int.Parse(Helper.GetDoctorID(this.Page));
        temp_allData["doctor_name"] = Helper.GetUserFullname(this.Page);
        temp_allData["vaccination_sequence"] = (short)(item_seq + 1); ;
        temp_allData["vaccination_date"] = DateTime.Now.ToString("dd/MM/yyyy");
        temp_allData["vaccination_age"] = "";
        temp_allData["expiry_date"] = "";
        temp_allData["no_lot"] = "";
        allGridData.Rows.Add(temp_allData);

        Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] = allGridData;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "LB_addnewrowanak_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", "")); 
        //Log.Info(LogConfig.LogEnd());
    }

    protected void RepeaterImunisasiAnak_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RepeaterItem item = e.Item;

            string headerId = (item.FindControl("HF_imunisasiIdAnk") as HiddenField).Value;
            GridView gv_imunisasi = item.FindControl("GridViewImunisasiAnak") as GridView;

            if (Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] != null)
            {
                DataTable alldata = (DataTable)Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value];
                if (alldata.Select("vaccine_id = " + headerId).Count() > 0)
                {
                    DataTable dataVaksinasi = alldata.Select("vaccine_id = " + headerId).CopyToDataTable();
                    dataVaksinasi.DefaultView.Sort = "vaccination_sequence asc";
                    dataVaksinasi = dataVaksinasi.DefaultView.ToTable();

                    gv_imunisasi.DataSource = dataVaksinasi;
                    gv_imunisasi.DataBind();
                }
                else
                {
                    gv_imunisasi.DataSource = null;
                    gv_imunisasi.DataBind();
                }
            }
        }
    }

    protected void btndelete_ImunisasiAnk_Click(object sender, ImageClickEventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        DataTable selectedGridData = new DataTable();
        int selItemIndex = ((RepeaterItem)(((ImageButton)sender).Parent.Parent.Parent.Parent.NamingContainer)).ItemIndex;
        //int selItemIndex = ((sender as LinkButton).NamingContainer as RepeaterItem).ItemIndex;       
        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        GridView gvw_imunisasiAnk = (GridView)RepeaterImunisasiAnak.Items[selItemIndex].FindControl("GridViewImunisasiAnak");

        HiddenField hf_vaccineid = (HiddenField)RepeaterImunisasiAnak.Items[selItemIndex].FindControl("HF_imunisasiIdAnk");
        Label txt_seq = (Label)gvw_imunisasiAnk.Rows[selRowIndex].FindControl("Lbl_noSequenceAnk");

        //DataTable dt_alldataimunisasi = (DataTable)Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value];
        List<Vaccination> datadetail = GetRowListVaccination();
        DataTable dt_alldataimunisasi = Helper.ToDataTable(datadetail);
        for (int i = 0; i < dt_alldataimunisasi.Rows.Count; i++)
        {
            DataRow dr = dt_alldataimunisasi.Rows[i];
            if (dr["vaccine_id"].ToString() == hf_vaccineid.Value && dr["vaccination_sequence"].ToString() == txt_seq.Text)
            {
                dr.Delete();
            }
        }
        dt_alldataimunisasi.AcceptChanges();

        if (dt_alldataimunisasi.Select("vaccine_id = " + hf_vaccineid.Value).Count() > 0)
        {
            selectedGridData = dt_alldataimunisasi.Select("vaccine_id = " + hf_vaccineid.Value).CopyToDataTable();
            gvw_imunisasiAnk.DataSource = selectedGridData;
            gvw_imunisasiAnk.DataBind();
        }
        else
        {
            gvw_imunisasiAnk.DataSource = null;
            gvw_imunisasiAnk.DataBind();
        }

        Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] = dt_alldataimunisasi;
        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "btndelete_ImunisasiAnk_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    public dynamic GetImunisasiValues(dynamic DataImunisasi)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        try
        {
            //log.Info(LogLibrary.Logging("S", "GetImunisasiValues", Helper.GetLoginUser(this.Parent.Page), ""));

            DataTable dataAll = (DataTable)Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value];
            DataImunisasi.vaccination = new List<Vaccination>();

            if (dataAll != null && dataAll.Rows.Count != 0)
            {
                DataImunisasi.vaccination.AddRange(GetRowListVaccination());
                //DataImunisasi.vaccination.AddRange(Helper.ToDataList<Vaccination>(dataAll));
            }
            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetImunisasiValues", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
            
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Log.Error(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "GetImunisasiValues", StartTime, ErrorTime, "Error", MyUser.GetUsername(), "", "", ex.Message));
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
        }

        //Log.Info(LogConfig.LogEnd());
        return DataImunisasi;
    }

    protected void ButtonSaveImunisasi_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        List<Vaccination> datadetail = GetRowListVaccination();
        DataTable dt_alldataimunisasi = Helper.ToDataTable(datadetail);
        Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] = dt_alldataimunisasi;
        Session[Helper.SessionDataDetailVaksinasi_Save + hfguidadditional.Value] = dt_alldataimunisasi;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalImunisasi", "HideImunisasi();", true);

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonSaveImunisasi_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonCancelImunisasi_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        DataTable datainitial = (DataTable)Session[Helper.SessionDataDetailVaksinasi_Save + hfguidadditional.Value];
        Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] = datainitial;

        RepeaterImunisasiDewasa.DataSource = (DataTable) Session[Helper.SessionVaccineDWS];
        RepeaterImunisasiDewasa.DataBind();
        RepeaterImunisasiAnak.DataSource = (DataTable)Session[Helper.SessionVaccineANK];
        RepeaterImunisasiAnak.DataBind();

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalImunisasi", "HideImunisasi();", true);

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonCancelImunisasi_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void DDLtemplateimunisasi_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (DDLtemplateimunisasi.SelectedValue == "0")
        {
            DivImunisasiTable.Visible = true;
            DivImunisasiKalenderDewasa.Visible = false;
            DivImunisasiKalenderAnak.Visible = false;
        }
        else if (DDLtemplateimunisasi.SelectedValue == "1")
        {
            DivImunisasiTable.Visible = false;
            DivImunisasiKalenderDewasa.Visible = true;
            DivImunisasiKalenderAnak.Visible = false;
        }
        else if (DDLtemplateimunisasi.SelectedValue == "2")
        {
            DivImunisasiTable.Visible = false;
            DivImunisasiKalenderDewasa.Visible = false;
            DivImunisasiKalenderAnak.Visible = true;
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "DDLtemplateimunisasi_SelectedIndexChanged", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonSwitchToAnak_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        DivImunisasiTable.Visible = false;
        DivImunisasiKalenderDewasa.Visible = false;
        DivImunisasiKalenderAnak.Visible = true;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonSwitchToAnak_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    protected void ButtonSwitchToDewasa_Click(object sender, EventArgs e)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        DivImunisasiTable.Visible = false;
        DivImunisasiKalenderDewasa.Visible = true;
        DivImunisasiKalenderAnak.Visible = false;

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "ButtonSwitchToDewasa_Click", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }


    //... FOR KALENDER Dewasa ...//

    protected void GvwKalenderImunisasiDewasa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            HiddenField idVaccine = (HiddenField)e.Row.FindControl("HF_imunisasiIdDws_Kal");
            Label data1921 = (Label)e.Row.FindControl("Lbl_Age1921");
            Label data2226 = (Label)e.Row.FindControl("Lbl_Age2226");
            Label data2749 = (Label)e.Row.FindControl("Lbl_Age2749");
            Label data5059 = (Label)e.Row.FindControl("Lbl_Age5059");
            Label data6064 = (Label)e.Row.FindControl("Lbl_Age6064");
            Label data65 = (Label)e.Row.FindControl("Lbl_Age65");

            if (Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] != null)
            {
                //add method fill bg table
                fillBGKalenderDWS(e, idVaccine.Value);

                DataTable alldata = (DataTable)Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value];
                if (alldata.Select("vaccine_id = " + idVaccine.Value).Count() > 0)
                {
                    DataTable dataVaksinasi = alldata.Select("vaccine_id = " + idVaccine.Value).CopyToDataTable();
                    for (int i = 0; i < dataVaksinasi.Rows.Count; i++)
                    {
                        //method calc age in month
                        int age = convertAgeToMonth(dataVaksinasi.Rows[i]["vaccination_age"].ToString());

                        if (age >= 228 && age < 252) //-- in month
                        {
                            if (data1921.Text == "")
                            {
                                data1921.Text = data1921.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                data1921.Text = data1921.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 252 && age < 312)
                        {
                            if (data2226.Text == "")
                            {
                                data2226.Text = data2226.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                data2226.Text = data2226.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 312 && age < 588)
                        {
                            if (data2749.Text == "")
                            {
                                data2749.Text = data2749.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                data2749.Text = data2749.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 588 && age < 708)
                        {
                            if (data5059.Text == "")
                            {
                                data5059.Text = data5059.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                data5059.Text = data5059.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 708 && age < 768)
                        {
                            if (data6064.Text == "")
                            {
                                data6064.Text = data6064.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                data6064.Text = data6064.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age > 768)
                        {
                            if (data65.Text == "")
                            {
                                data65.Text = data65.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                data65.Text = data65.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }

                        }
                    }
                }
                else
                {
                    data1921.Text = "";
                    data2226.Text = "";
                    data2749.Text = "";
                    data5059.Text = "";
                    data6064.Text = "";
                    data65.Text = "";
                }
            }
        }
    }

    public void fillBGKalenderDWS(GridViewRowEventArgs e, string vaccineid)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (vaccineid == "1" || vaccineid == "2" || vaccineid == "3" || vaccineid == "10" || vaccineid == "11" || vaccineid == "12" || vaccineid == "13")
        {
            e.Row.Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[6].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
        }
        else if (vaccineid == "4")
        {
            e.Row.Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
        }
        else if (vaccineid == "34")
        {
            e.Row.Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
        }
        else if (vaccineid == "5" || vaccineid == "7")
        {
            e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[6].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
        }
        else if (vaccineid == "6")
        {
            e.Row.Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
        }
        else if (vaccineid == "8")
        {
            e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
            e.Row.Cells[6].BackColor = System.Drawing.ColorTranslator.FromHtml("#d6e05f");
        }
        else if (vaccineid == "9" || vaccineid == "14" || vaccineid == "15")
        {
            e.Row.Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#dacbef");
            e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#dacbef");
            e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#dacbef");
            e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#dacbef");
            e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#dacbef");
            e.Row.Cells[6].BackColor = System.Drawing.ColorTranslator.FromHtml("#dacbef");
        }
        else if (vaccineid == "16" || vaccineid == "17")
        {
            e.Row.Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#fcd2a8");
            e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#fcd2a8");
            e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#fcd2a8");
            e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#fcd2a8");
            e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#fcd2a8");
            e.Row.Cells[6].BackColor = System.Drawing.ColorTranslator.FromHtml("#fcd2a8");
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "fillBGKalenderDWS", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }

    public int convertAgeToMonth(string age)
    {
        var usia = age.Split(',');
        int totalusia = (int.Parse(usia[0]) * 12) + (int.Parse(usia[1]));
        return totalusia;
    }


    //... FOR KALENDER Anak ...//

    protected void GvwKalenderImunisasiAnak_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            HiddenField idVaccine = (HiddenField)e.Row.FindControl("HF_imunisasiIdAnk_Kal");
            Label datalahir = (Label)e.Row.FindControl("Lbl_AgeBlnLahir");
            Label databln1 = (Label)e.Row.FindControl("Lbl_AgeBln1");
            Label databln2 = (Label)e.Row.FindControl("Lbl_AgeBln2");
            Label databln3 = (Label)e.Row.FindControl("Lbl_AgeBln3");
            Label databln4 = (Label)e.Row.FindControl("Lbl_AgeBln4");
            Label databln5 = (Label)e.Row.FindControl("Lbl_AgeBln5");
            Label databln6 = (Label)e.Row.FindControl("Lbl_AgeBln6");
            Label databln9 = (Label)e.Row.FindControl("Lbl_AgeBln9");
            Label databln12 = (Label)e.Row.FindControl("Lbl_AgeBln12");
            Label databln15 = (Label)e.Row.FindControl("Lbl_AgeBln15");
            Label databln18 = (Label)e.Row.FindControl("Lbl_AgeBln18");
            Label databln24 = (Label)e.Row.FindControl("Lbl_AgeBln24");

            Label datathn3 = (Label)e.Row.FindControl("Lbl_AgeThn3");
            Label datathn5 = (Label)e.Row.FindControl("Lbl_AgeThn5");
            Label datathn6 = (Label)e.Row.FindControl("Lbl_AgeThn6");
            Label datathn7 = (Label)e.Row.FindControl("Lbl_AgeThn7");
            Label datathn8 = (Label)e.Row.FindControl("Lbl_AgeThn8");
            Label datathn9 = (Label)e.Row.FindControl("Lbl_AgeThn9");
            Label datathn10 = (Label)e.Row.FindControl("Lbl_AgeThn10");
            Label datathn12 = (Label)e.Row.FindControl("Lbl_AgeThn12");
            Label datathn18 = (Label)e.Row.FindControl("Lbl_AgeThn18");


            if (Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value] != null)
            {
                //add method fill bg table
                fillBGKalenderANK(e, idVaccine.Value);

                DataTable alldata = (DataTable)Session[Helper.SessionDataDetailVaksinasi + hfguidadditional.Value];
                if (alldata.Select("vaccine_id = " + idVaccine.Value).Count() > 0)
                {
                    DataTable dataVaksinasi = alldata.Select("vaccine_id = " + idVaccine.Value).CopyToDataTable();
                    for (int i = 0; i < dataVaksinasi.Rows.Count; i++)
                    {
                        //method calc age in month
                        int age = convertAgeToMonth(dataVaksinasi.Rows[i]["vaccination_age"].ToString());

                        if (age == 0) //-- in month
                        {
                            if (datalahir.Text == "")
                            {
                                datalahir.Text = datalahir.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                datalahir.Text = datalahir.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age == 1)
                        {
                            if (databln1.Text == "")
                            {
                                databln1.Text = databln1.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                databln1.Text = databln1.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age == 2)
                        {
                            if (databln2.Text == "")
                            {
                                databln2.Text = databln2.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                databln2.Text = databln2.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age == 3)
                        {
                            if (databln3.Text == "")
                            {
                                databln3.Text = databln3.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                databln3.Text = databln3.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age == 4)
                        {
                            if (databln4.Text == "")
                            {
                                databln4.Text = databln4.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                databln4.Text = databln4.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age == 5)
                        {
                            if (databln5.Text == "")
                            {
                                databln5.Text = databln5.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                databln5.Text = databln5.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age == 6)
                        {
                            if (databln6.Text == "")
                            {
                                databln6.Text = databln6.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                databln6.Text = databln6.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 7 && age <= 9)
                        {
                            if (databln9.Text == "")
                            {
                                databln9.Text = databln9.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                databln9.Text = databln9.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 10 && age <= 12)
                        {
                            if (databln12.Text == "")
                            {
                                databln12.Text = databln12.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                databln12.Text = databln12.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 13 && age <= 15)
                        {
                            if (databln15.Text == "")
                            {
                                databln15.Text = databln15.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                databln15.Text = databln15.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 16 && age <= 18)
                        {
                            if (databln18.Text == "")
                            {
                                databln18.Text = databln18.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                databln18.Text = databln18.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 19 && age <= 35)
                        {
                            if (databln24.Text == "")
                            {
                                databln24.Text = databln24.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                databln24.Text = databln24.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }

                        else if (age >= 36 && age < 60)
                        {
                            if (datathn3.Text == "")
                            {
                                datathn3.Text = datathn3.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                datathn3.Text = datathn3.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 60 && age < 72)
                        {
                            if (datathn5.Text == "")
                            {
                                datathn5.Text = datathn5.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                datathn5.Text = datathn5.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 72 && age < 84)
                        {
                            if (datathn6.Text == "")
                            {
                                datathn6.Text = datathn6.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                datathn6.Text = datathn6.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 84 && age < 96)
                        {
                            if (datathn7.Text == "")
                            {
                                datathn7.Text = datathn7.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                datathn7.Text = datathn7.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 96 && age < 108)
                        {
                            if (datathn8.Text == "")
                            {
                                datathn8.Text = datathn8.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                datathn8.Text = datathn8.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 108 && age < 120)
                        {
                            if (datathn9.Text == "")
                            {
                                datathn9.Text = datathn9.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                datathn9.Text = datathn9.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 120 && age < 144)
                        {
                            if (datathn10.Text == "")
                            {
                                datathn10.Text = datathn10.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                datathn10.Text = datathn10.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 144 && age < 216)
                        {
                            if (datathn12.Text == "")
                            {
                                datathn12.Text = datathn12.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                datathn12.Text = datathn12.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }
                        else if (age >= 216 && age < 228)
                        {
                            if (datathn18.Text == "")
                            {
                                datathn18.Text = datathn18.Text + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                            else
                            {
                                datathn18.Text = datathn18.Text + " , " + dataVaksinasi.Rows[i]["vaccination_sequence"].ToString();
                            }
                        }

                    }
                }
                else
                {
                    datalahir.Text = "";
                    databln1.Text = "";
                    databln2.Text = "";
                    databln3.Text = "";
                    databln4.Text = "";
                    databln5.Text = "";
                    databln6.Text = "";
                    databln9.Text = "";
                    databln12.Text = "";
                    databln15.Text = "";
                    databln18.Text = "";
                    databln24.Text = "";

                    datathn3.Text = "";
                    datathn5.Text = "";
                    datathn6.Text = "";
                    datathn7.Text = "";
                    datathn8.Text = "";
                    datathn9.Text = "";
                    datathn10.Text = "";
                    datathn12.Text = "";
                    datathn18.Text = "";

                }
            }
        }
    }

    public void fillBGKalenderANK(GridViewRowEventArgs e, string vaccineid)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //Log.Info(LogConfig.LogStart());

        if (vaccineid == "30") //hepatitis B
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i == 1 || i == 3 || i == 4 || i == 5)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffed64");
                }
            }
        }
        else if (vaccineid == "18") //polio
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i >= 1 && i <= 5)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else if (i == 11)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#00b8f1");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffed64");
                }
            }
        }
        else if (vaccineid == "19") //BCG
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i >= 1 && i <= 3)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else if (i >= 15 && i <= 21)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffed64");
                }
            }
        }
        else if (vaccineid == "20") //DTP
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i >= 3 && i <= 5)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else if (i >= 1 && i <= 2)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
                else if (i == 11 || i == 14 || i == 19 || i == 20 || i == 21)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#00b8f1");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffed64");
                }
            }
        }
        else if (vaccineid == "21") //HIB
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i >= 3 && i <= 5)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else if ((i >= 1 && i <= 2) || (i >= 15 && i <= 21))
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
                else if (i == 10 || i == 11)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#00b8f1");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffed64");
                }
            }
        }
        else if (vaccineid == "22") //PCV
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i == 3 || i == 5 || i == 7)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else if ((i >= 1 && i <= 2) || (i >= 15 && i <= 21))
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
                else if (i == 9 || i == 10)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#00b8f1");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffed64");
                }
            }
        }
        else if (vaccineid == "23") //Rotavirus
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i == 3 || i == 5 || i == 7)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
            }
        }
        else if (vaccineid == "26") //Influenza
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i >= 7 && i <= 21)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
            }
        }
        else if (vaccineid == "24") //Campak
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i == 8)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else if (i >= 1 && i <= 7)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
                else if (i == 11 || i == 15 || i == 16)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#00b8f1");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffed64");
                }
            }
        }
        else if (vaccineid == "28") //MMR
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i == 10)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else if (i >= 1 && i <= 9)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
                else if (i == 14)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#00b8f1");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffed64");
                }
            }
        }
        else if (vaccineid == "32" || vaccineid == "29") //Tifoid & Hepatitis A
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i >= 12 && i <= 21)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
            }
        }
        else if (vaccineid == "27") //Varisela
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i >= 9 && i <= 21)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
            }
        }
        else if (vaccineid == "33") //HPV
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i >= 19 && i <= 21)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
            }
        }
        else if (vaccineid == "31") //JE
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i == 9 || i == 12 || i == 13)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#fb71b5");
                }
                else if (i >= 1 && i <= 8)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffed64");
                }
            }
        }
        else if (vaccineid == "25") //Dengue
        {
            for (int i = 1; i <= 21; i++)
            {
                if (i >= 18 && i <= 21)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#cde398");
                }
                else
                {
                    e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
            }
        }

        string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Log.Debug(LogLibrary.SaveLog(Helper.organizationId.ToString(), "EncounterId", Request.QueryString["EncounterId"] != null ? Request.QueryString["EncounterId"].ToString() : "", "fillBGKalenderANK", StartTime, EndTime, "OK", MyUser.GetUsername(), "", "", ""));
        //Log.Info(LogConfig.LogEnd());
    }
}