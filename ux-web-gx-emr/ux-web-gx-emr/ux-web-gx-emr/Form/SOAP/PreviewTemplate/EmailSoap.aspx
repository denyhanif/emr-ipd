<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailSoap.aspx.cs" Inherits="Form_SOAP_PreviewTemplate_EmailSoap" %>

<%@ Register Src="~/Form/SOAP/PreviewTemplate/SoapPagePreview.ascx" TagPrefix="uc1" TagName="Preview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/jasny-bootstrap/js/jasny-bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/iCheck/icheck.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/datepicker/bootstrap-datepicker.js" />
                <asp:ScriptReference Path="~/Content/plugins/select2/select2.full.min.js" />
                <%--<asp:ScriptReference Path="~/Scripts/bootbox.min.js" />--%>
                <asp:ScriptReference Path="~/Content/dist/js/app.min.js" />
                <%--<asp:ScriptReference Path="~/Scripts/site.js" />--%>
                <asp:ScriptReference Path="~/Content/bootstrap-select/js/bootstrap-select.js" />
                <asp:ScriptReference Path="~/Content/selectize/js/selectize.min.js" />
                <asp:ScriptReference Path="~/Content/selectize/js/standalone/selectize.js" />
            </Scripts>
        </asp:ScriptManager>
        <div>
            <uc1:Preview runat="server" id="SoapPagePreview" />
        </div>
    </form>
</body>
</html>
