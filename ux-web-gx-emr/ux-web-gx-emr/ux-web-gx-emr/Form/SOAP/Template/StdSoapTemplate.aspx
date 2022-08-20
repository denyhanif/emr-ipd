<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StdSoapTemplate.aspx.cs" Inherits="Form_SOAP_Template_StdSoapTeleconsultation" %>

<asp:Content ID="SoapTeleconsultation" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server" ID="UPST" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <asp:HiddenField ID="HF_Form_Type" runat="server" />
                <asp:Button id="BtnChooseTemplate" runat="server" CssClass="btn btn-lightGreen hidden" Text="Choose" Style="width: 71px; height: 25px; padding-top: 3px; border-radius: 4px; background-color: #4d9b35; font-family: Helvetica; font-size: 12px; color: #ffffff" OnClick="btnChoose_onClick" />

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <iframe name="IframeTemplate" id="IframeTemplate" allow="camera; microphone" runat="server" style="width: 100%; height: calc(100vh - 50px); border: none; margin-bottom: -6px;"></iframe>
    
    <script>
		function b64toBlob(dataURI) {

			 var byteString = atob(dataURI.split(',')[1]);
			 var ab = new ArrayBuffer(byteString.length);
			 var ia = new Uint8Array(ab);

			 for (var i = 0; i < byteString.length; i++) {
				 ia[i] = byteString.charCodeAt(i);
			 }
			 return new Blob([ab], { type: 'application/pdf' });
		 }
			
        function messageHandler(event) {

            if (event.data && (event.data.type === 'SelectedTemplate')) {

                document.getElementById("<%=HF_Form_Type.ClientID%>").value = event.data.responseData;
                document.getElementById("<%=BtnChooseTemplate.ClientID%>").click();
            }
			
			if (event.data && (event.data.type === 'OpenPDF')) {

                const blob = b64toBlob(event.data.responseData.toString());
                var url = URL.createObjectURL(blob);

                window.open(url);
            }
        }

        window.addEventListener('message', messageHandler, false);
    </script>



</asp:Content>


