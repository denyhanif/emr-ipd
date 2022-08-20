using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using static FirstAssesment;
using static SPDocument;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Web.Services;

public partial class Form_SOAP_Control_Template_Specialty_StdDocumentUpload : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable doctypedt;
            if (Session[Helper.SessionDocTypeSOAP] == null)
            {

                string templateName = "";
                string templateid = Request.QueryString["PageSoapId"];
                List<FormTypeHOPE> listdoctype = new List<FormTypeHOPE>();
                if (templateid.ToUpper() == "882854F0-780A-48BB-89EC-A6FF7519D10B") //DOCUMENT
                {
                    templateName = "CARDIOLOGY";
                }
                var doc_type = clsSOAP.GetFormTypeHOPETemplate(Helper.organizationId, templateName);

                var Jsondoctype = JsonConvert.DeserializeObject<ResultFormTypeHOPE>(doc_type.Result.ToString());

                listdoctype = Jsondoctype.list;
                doctypedt = Helper.ToDataTable(listdoctype);
                Session[Helper.SessionDocTypeSOAP] = doctypedt;

            }
            else
                doctypedt = (DataTable)Session[Helper.SessionDocTypeSOAP];

            ModalDrawImageType.DataSource = doctypedt;
            ModalDrawImageType.DataTextField = "formTypeName";
            ModalDrawImageType.DataValueField = "formTypeName";
            ModalDrawImageType.DataBind();

        }
    }


    public void initializevalue(List<SOAPDocument> listsoapdocument)
    {
        StringBuilder sb = new StringBuilder();
        if (listsoapdocument.Count > 0)
        {
            jsonDataHolder.Value = JsonConvert.SerializeObject(listsoapdocument);
            //Session[Helper.SessionDocument] = listsoapdocument;
            
        }

        sb.Append(" var soap_document = " + JsonConvert.SerializeObject(listsoapdocument) + ";");
        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), sb.ToString(), true);

    }

    public SOAPSDocument getvalues(SOAPSDocument soapsdocument)
    {
        soapsdocument.soap_document = JsonConvert.DeserializeObject<List<SOAPDocument>>(jsonDataHolder.Value);

        return soapsdocument;
    }


}

