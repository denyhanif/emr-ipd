<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PatientCard.ascx.cs" Inherits="Form_General_Control_PatientCard" %>

<%--<asp:UpdatePanel runat="server">
    <ContentTemplate>--%>

        <asp:HiddenField ID="HFisBahasaPC" runat="server" />

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
                            <label id="lblbhs_pcadmissionno"> Admission No. </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblAdmissionNo"></asp:Label>
                    </div>
                    <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server" ToolTip="Date of Birth">
                            <label id="lblbhs_pcdob"> DOB </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblDOB"></asp:Label>
                    </div>
                    <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server">
                            <label id="lblbhs_pcage"> Age </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblAge"></asp:Label>
                    </div>
                    <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server">
                            <label id="lblbhs_pcreligion"> Religion </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblReligion"></asp:Label>
                    </div>
                    <div class="btn-group" role="group" style="max-width: 50%; vertical-align: top; padding-right: 25px">
                        <asp:Label CssClass="form-group" runat="server">
                            <label id="lblbhs_pcpayer"> Payer </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Style="font-weight: bold; width: 100%;" runat="server" ID="lblPayer"></asp:Label>
                    </div>
                    <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server">
                            <label id="lblbhs_pcpatienttype"> Patient Type </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblPatientType"></asp:Label>
                    </div>
                </div>
            </div>
            <div style="position:absolute; right:0px; padding: 10px 30px; display:none;">
                <asp:Image ID="ImageJatuhSticker" runat="server" ToolTip="Fall Risk" Visible="false" ImageUrl="~/Images/Icon/ic_Jatuh_sticker.svg" Style="width:45px" />
                <asp:Image ID="ImageAllergySticker" runat="server" ToolTip="Allergy" Visible="false" ImageUrl="~/Images/Icon/ic_Allergy_sticker.svg" Style="width:45px" />
                <asp:Image ID="ImageHepBSticker" runat="server" ToolTip="Hepatitis B" Visible="false" ImageUrl="~/Images/Icon/ic_HepB_sticker.svg" Style="width:45px" />
                <asp:Image ID="ImageHepCSticker" runat="server" ToolTip="Hepatitis C" Visible="false" ImageUrl="~/Images/Icon/ic_HepC_sticker.svg" Style="width:45px" />
                <asp:Image ID="ImageTBCSticker" runat="server" ToolTip="TBC" Visible="false" ImageUrl="~/Images/Icon/ic_TBC_sticker.svg" Style="width:45px" />
                <asp:Image ID="ImageHADSticker" runat="server" ToolTip="HIV/AIDS" Visible="false" ImageUrl="~/Images/Icon/ic_HAD_sticker.svg" Style="width:45px" />
                <asp:Image ID="ImagePRTSticker" runat="server" ToolTip="Peri Natal Resiko Tinggi" Visible="false" ImageUrl="~/Images/Icon/ic_PRT_sticker.svg" Style="width:45px" />
                <asp:Image ID="ImageRHNSticker" runat="server" ToolTip="Rhesus Negatif" Visible="false" ImageUrl="~/Images/Icon/ic_RHN_sticker.svg" Style="width:45px" />
                <asp:Image ID="ImageMRSSticker" runat="server" ToolTip="MRSA" Visible="false" ImageUrl="~/Images/Icon/ic_MRS_sticker.svg" Style="width:45px" />
                <asp:Image ID="ImageCVSticker" runat="server" ToolTip="Covid 19" Visible="false" ImageUrl="~/Images/Icon/ic_COVID_sticker.svg" Style="width:45px" />
                <asp:Image ID="ImageCVVaccineSticker" runat="server" ToolTip="Covid 19 Vaccine" Visible="false" ImageUrl="~/Images/Icon/ic_CVVaccine_sticker.svg" Style="width:45px" />
            </div>
            <div style="position:absolute; right:0px; padding: 10px 30px;">          
                <asp:Image ID="ImageJatuhSticker_new" runat="server" ToolTip="Fall Risk" ImageUrl="~/Images/Icon/ic_Jatuh_sticker.svg" Style="width:45px; display:none;" />
                <asp:Image ID="ImageAllergySticker_new" runat="server" ToolTip="Allergy" ImageUrl="~/Images/Icon/ic_Allergy_sticker.svg" Style="width:45px; display:none;" />
                <asp:Image ID="ImageHepBSticker_new" runat="server" ToolTip="Hepatitis B" ImageUrl="~/Images/Icon/ic_HepB_sticker.svg" Style="width:45px; display:none;" />
                <asp:Image ID="ImageHepCSticker_new" runat="server" ToolTip="Hepatitis C" ImageUrl="~/Images/Icon/ic_HepC_sticker.svg" Style="width:45px; display:none;" />
                <asp:Image ID="ImageTBCSticker_new" runat="server" ToolTip="TBC" ImageUrl="~/Images/Icon/ic_TBC_sticker.svg" Style="width:45px; display:none;" />
                <asp:Image ID="ImageHADSticker_new" runat="server" ToolTip="HIV/AIDS" ImageUrl="~/Images/Icon/ic_HAD_sticker.svg" Style="width:45px; display:none;" />
                <asp:Image ID="ImagePRTSticker_new" runat="server" ToolTip="Peri Natal Resiko Tinggi" ImageUrl="~/Images/Icon/ic_PRT_sticker.svg" Style="width:45px; display:none;" />
                <asp:Image ID="ImageRHNSticker_new" runat="server" ToolTip="Rhesus Negatif" ImageUrl="~/Images/Icon/ic_RHN_sticker.svg" Style="width:45px; display:none;" />
                <asp:Image ID="ImageMRSSticker_new" runat="server" ToolTip="MRSA" ImageUrl="~/Images/Icon/ic_MRS_sticker.svg" Style="width:45px; display:none;" />
                <asp:Image ID="ImageCVSticker_new" runat="server" ToolTip="Covid 19" ImageUrl="~/Images/Icon/ic_COVID_sticker.svg" Style="width:45px; display:none;" />
                <asp:Image ID="ImageCVVaccineSticker_new" runat="server" ToolTip="Covid 19 Vaccine" ImageUrl="~/Images/Icon/ic_CVVaccine_sticker.svg" Style="width:45px; display:none;" />
                <div id="div_sticker_fail" style="display:none;"><asp:Label ID="LabelStickerLoadFailed" runat="server" Text="Failed to get patient sticker!" CssClass="badge bg-gray"></asp:Label> <i class="fa fa-refresh" title="Reload" onclick="loadSticker();" style="cursor:pointer;"></i></div>
            </div>
        </div>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>

<script type="text/javascript">
    function switchBahasaPC()
    {
        var bahasa = document.getElementById('<%=HFisBahasaPC.ClientID%>').value;
        if (bahasa == "ENG")
        {
            document.getElementById('lblbhs_pcadmissionno').innerHTML = "Admission No.";
            document.getElementById('lblbhs_pcdob').innerHTML = "DOB";
            document.getElementById('lblbhs_pcage').innerHTML = "Age";
            document.getElementById('lblbhs_pcreligion').innerHTML = "Religion";
            document.getElementById('lblbhs_pcpayer').innerHTML = "Payer";
            document.getElementById('lblbhs_pcpatienttype').innerHTML = "Patient Type";
        }
        else if (bahasa == "IND")
        {
            document.getElementById('lblbhs_pcadmissionno').innerHTML = "No. Admisi";
            document.getElementById('lblbhs_pcdob').innerHTML = "Tgl. Lahir";
            document.getElementById('lblbhs_pcage').innerHTML = "Umur";
            document.getElementById('lblbhs_pcreligion').innerHTML = "Agama";
            document.getElementById('lblbhs_pcpayer').innerHTML = "Penanggung";
            document.getElementById('lblbhs_pcpatienttype').innerHTML = "Tipe Pasien";
        }
    }

    $(document).ready(function () {

            //fungsi untuk action in postback updatepanel
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {

                loadSticker();
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        switchBahasaPC();
                        //loadSticker1();
                    }
                });
            };
    });

    function loadSticker() {

        var ptnid = new URLSearchParams(window.location.search).get('idPatient');
        var admid = new URLSearchParams(window.location.search).get('AdmissionId');
        var encid = new URLSearchParams(window.location.search).get('EncounterId');

        if (ptnid != null && admid != null && encid != null) {
            var paramnya = { 'PatientId': ptnid, 'AdmissiontId': admid, 'EncounterId': encid };
            $.ajax({
                type: "POST",
                //url: "Control/TempCard.aspx/LoadStickerPatient",
                url: "<%= Page.ResolveClientUrl("~/Form/General/Control/TempCard.aspx/LoadStickerPatient") %>",
                data: JSON.stringify(paramnya),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d.includes("Fail")) {
                        console.log(" FAIL ");
                        document.getElementById("div_sticker_fail").style.display = "inline-block";
                    }
                    else {
                        console.log(" SUCCESS ");
                        printSticker(msg.d);
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrown) {
                    //alert(" conection to the server failed ");
                    console.log("error: " + errorthrown);
                    document.getElementById("div_sticker_fail").style.display = "inline-block";
                }
            });
        }

    }

    function printSticker(datasticker) {
        var data = JSON.parse(datasticker);

        if (data.isALG == true) {
            document.getElementById("<%=ImageAllergySticker_new.ClientID%>").style.display = "";
        }
        if (data.isTBC == true) {
            document.getElementById("<%=ImageTBCSticker_new.ClientID%>").style.display = "";
        }
        if (data.isHCS == true) {
            document.getElementById("<%=ImageHepCSticker_new.ClientID%>").style.display = "";
        }
        if (data.isHBS == true) {
            document.getElementById("<%=ImageHepBSticker_new.ClientID%>").style.display = "";
        }
        if (data.isHAD == true) {
            document.getElementById("<%=ImageHADSticker_new.ClientID%>").style.display = "";
        }
        if (data.isPRT == true) {
            document.getElementById("<%=ImagePRTSticker_new.ClientID%>").style.display = "";
        }
        if (data.isRHN == true) {
            document.getElementById("<%=ImageRHNSticker_new.ClientID%>").style.display = "";
        }
        if (data.isMRS == true) {
            document.getElementById("<%=ImageMRSSticker_new.ClientID%>").style.display = "";
        }
        if (data.isCOVID == true) {
            document.getElementById("<%=ImageCVSticker_new.ClientID%>").style.display = "";
        }
        if (data.is_covidvac == true) {
            document.getElementById("<%=ImageCVVaccineSticker_new.ClientID%>").style.display = "";
        }
        document.getElementById("div_sticker_fail").style.display = "none";
    }
</script>