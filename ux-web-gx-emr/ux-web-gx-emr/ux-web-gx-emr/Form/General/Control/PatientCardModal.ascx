<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PatientCardModal.ascx.cs" Inherits="Form_General_Control_PatientCardModal" %>

<%--<asp:UpdatePanel runat="server">
    <ContentTemplate>--%>

        <asp:HiddenField ID="HFisBahasaPCM" runat="server" />

        <div class="row">
            <div class="col-sm-2" style="padding-top: 10px; padding-right: 0px; width: 100px; margin-left: 5px;">
                <asp:Image runat="server" ID="Image1" Width="54px" Height="54px" Style="vertical-align: bottom;" />
                <asp:Image runat="server" ID="imgSex" Width="25px" Style="margin-left: -15px;" />
            </div>
            <div class="col-sm-10" style="padding-left: 0px; width: 86%;">
                <div class="input-group">
                    <h5>
                        <asp:Label ID="patientName" class="form-group" runat="server" Font-Bold="true" Font-Size="13px"></asp:Label>
                        <asp:Label ID="Label2" class="form-group" runat="server" Font-Bold="true" ForeColor="LightGray" Font-Size="14px">&nbsp;|&nbsp; </asp:Label>
                        <asp:Label ID="localMrNo" class="form-group" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
                        <asp:Label ID="Label4" class="form-group" runat="server" Font-Bold="true" ForeColor="LightGray" Font-Size="14px">&nbsp;|&nbsp;</asp:Label>
                        <asp:Label ID="primaryDoctor" class="form-group" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
                    </h5>
                </div>
                <div role="group" style="width: 100%" aria-label="...">
                    <div class="btn-group" role="group" style="width: 115px; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server">
                            <label id="lblbhs_pcadmissionnoM"> Admission No. </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblAdmissionNo"></asp:Label>
                    </div>
                    <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server" ToolTip="Date of Birth">
                            <label id="lblbhs_pcdobM"> DOB </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblDOB"></asp:Label>
                    </div>
                    <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server">
                            <label id="lblbhs_pcageM"> Age </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblAge"></asp:Label>
                    </div>
                    <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server">
                            <label id="lblbhs_pcreligionM"> Religion </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblReligion"></asp:Label>
                    </div>
                    <div class="btn-group" role="group" style="max-width: 50%; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server">
                            <label id="lblbhs_pcpayerM"> Payer </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Style="font-weight: bold; width: 100%;" runat="server" ID="lblPayer"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>

<script type="text/javascript">
    function switchBahasaPCM()
    {
        var bahasa = document.getElementById('<%=HFisBahasaPCM.ClientID%>').value;
        if (bahasa == "ENG")
        {
            document.getElementById('lblbhs_pcadmissionnoM').innerHTML = "Admission No.";
            document.getElementById('lblbhs_pcdobM').innerHTML = "DOB";
            document.getElementById('lblbhs_pcageM').innerHTML = "Age";
            document.getElementById('lblbhs_pcreligionM').innerHTML = "Religion";
            document.getElementById('lblbhs_pcpayerM').innerHTML = "Payer";
        }
        else if (bahasa == "IND")
        {
            document.getElementById('lblbhs_pcadmissionnoM').innerHTML = "No. Admisi";
            document.getElementById('lblbhs_pcdobM').innerHTML = "Tgl. Lahir";
            document.getElementById('lblbhs_pcageM').innerHTML = "Umur";
            document.getElementById('lblbhs_pcreligionM').innerHTML = "Agama";
            document.getElementById('lblbhs_pcpayerM').innerHTML = "Penanggung";
        }
    }
</script>