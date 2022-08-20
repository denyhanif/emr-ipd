<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StdSoapTeleconsultation.aspx.cs" Inherits="Form_SOAP_Template_StdSoapTeleconsultation" %>

<asp:Content ID="SoapTeleconsultation" ContentPlaceHolderID="MainContent" runat="server">

    <%--<asp:UpdatePanel runat="server" ID="UPST" UpdateMode="Conditional">
        <ContentTemplate>
            <div>--%>

                <iframe name="IframeTele" id="IframeTele" allow="camera; microphone; cross-origin-isolated;" runat="server" style="width: 100%; height: calc(100vh - 50px); border: none; margin-bottom: -6px;"></iframe>

            <%--</div>
        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script>
        function messageHandler(event) {

            if (event.data && (event.data.type === 'OpenPDF')) {
                window.open(event.data.responseData.toString());
            }
        }

        window.addEventListener('message', messageHandler, false);
    </script>
</asp:Content>

