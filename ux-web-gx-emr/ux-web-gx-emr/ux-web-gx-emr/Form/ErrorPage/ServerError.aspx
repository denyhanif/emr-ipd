<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServerError.aspx.cs" Inherits="Form_ErrorPage_ServerError" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMR Doctor</title>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />

    <link rel="stylesheet" href="~/Content/Site.css" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="~/Content/font-awesome/css/font-awesome.css" />

    <!-- Glitch -->
    <link rel="stylesheet" href="~/Content/Glitch.css" />

    <style>
        body {
            background-image: url("<%= Page.ResolveClientUrl("~/Images/Background/bg_error.jpg") %>") !important;
            background-size: cover;
            background-repeat: no-repeat;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            <Scripts>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />

                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanelError" runat="server">
            <ContentTemplate>


                <div class="container-fluid">
                    <div class="row text-center">

                        <div>

                            <div style="position: fixed; right: 18%; top: 15%;">
                                <h1 class="Glitch3D">SERVER ERROR</h1>
                                <h1 class="Glitch3D">SERVER ERROR</h1>
                                <h1 class="Glitch3D">SERVER ERROR</h1>
                            </div>

                            <div style="position: fixed;right: 20%;top: 35%;width: 320px;">

                                <table style="width: 100%; text-align: left;">
                                    <tr>
                                        <td style="width: 110px; vertical-align: top; font-weight: bold;">Error Time </td>
                                        <td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
                                        <td>
                                            <asp:Label ID="LabelErrorTime" runat="server" Text="-"></asp:Label>
                                            <br /> <b>User : </b>
                                            <asp:Label ID="LabelErrorUser" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; vertical-align: top; font-weight: bold;">Message </td>
                                        <td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
                                        <td>
                                            <asp:Label ID="LabelErrorEx" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <div id="detailerror">
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td style="width: 110px; vertical-align: top; font-weight: bold;">Exception Detail </td>
                                            <td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
                                            <td>
                                                <asp:Label ID="LabelErrorExDet" runat="server" Text="-" Style="color: red;"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 110px; vertical-align: top; font-weight: bold;">Source File </td>
                                            <td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
                                            <td>
                                                <asp:Label ID="LabelErrorExSF" runat="server" Text="[Detail...]" ToolTip="-" Style="color: #83b3fd; cursor:pointer;"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 110px; vertical-align: top; font-weight: bold;">Method </td>
                                            <td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
                                            <td>
                                                <asp:Label ID="LabelErrorExMethod" runat="server" Text="-"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 110px; vertical-align: top; font-weight: bold;">Line </td>
                                            <td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
                                            <td>
                                                <asp:Label ID="LabelErrorExLine" runat="server" Text="-"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>


            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
    <script>

</script>
</body>
</html>

