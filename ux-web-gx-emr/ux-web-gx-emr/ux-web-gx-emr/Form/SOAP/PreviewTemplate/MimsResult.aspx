<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MimsResult.aspx.cs" Inherits="Form_SOAP_PreviewTemplate_MimsResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div_mims_notfound" runat="server" visible="false" style="text-align:center;">
            <h3>Gagal Menghubungkan Ke Jaringan MIMS</h3>
        </div>
        <div>
            <div style="background-color:lightgrey; padding:15px; font-size:12px;" id="div_disclaimer" runat="server" visible="false">
                <asp:Label ID="lblMimsDisclaimer" runat="server" Text=""></asp:Label>
            </div>

            <asp:Label ID="lblMimsRefresh" runat="server" Text="Refresh" CssClass="ui-widget-content ui-button" Style="margin-top: 5px; border-radius: 5px; position: absolute; z-index: 5; right: 15px; top: 10px; padding:4px; display:none;" onclick="refreshmims();"></asp:Label>
            <asp:Label ID="lblMimsHtmlResult" runat="server" Text=""></asp:Label>
            <asp:Button ID="ButtonRefreshMims" runat="server" Text="Refresh" Style="display: none;" OnClick="ButtonRefreshMims_Click" />
        </div>
        
    </form>

    <script>
        function refreshmims() {
            location.reload();
        }

        function messageHandler(event) {

            if (event.data && (event.data.type === 'parentRequest_MIMS')) {
                if (event.data && (event.data.flag === 'refresh')) {
                    <%--document.getElementById("<%=ButtonRefreshMims.ClientID%>").click();--%>
                    <%--document.getElementById("<%=lblMimsHtmlResult.ClientID%>").innerHTML = event.data.responseData;--%>

                    location.reload();
                }
            }

        }

        window.addEventListener('message', messageHandler, false);
    </script>
</body>
</html>
