<%@ Page Title="User Setting" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="UserSetting.aspx.cs" Inherits="Form_General_UserSetting" %>

<asp:Content ID="UserSetting" ContentPlaceHolderID="MainContent" runat="server">
    <!--###########################################################################################################################################-->
    <!------------------------------------------------------------------ The Script ----------------------------------------------------------------->

    <script type="text/javascript">
         $(window).load(function() {
		    $(".loadPage").fadeOut("slow");
        });

        $(document).ready(function () { 

            $('.CheckBoxSwitch').bootstrapToggle();

            //fungsi untuk mempertahankan style saat postback di updatepanel
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        $('.CheckBoxSwitch').bootstrapToggle();
                    }
                });
            };
        });

        function getTheSetting()
        {
            var hfbirthday = document.getElementById('<%= HF_birthday.ClientID %>');
            var ckbirthday = document.getElementById('CheckBoxBirthday');

            var hfnursesoap = document.getElementById('<%= HF_nursesoap.ClientID %>');
            var cknursesoap = document.getElementById('CheckBoxNurseSOAP');

            if (hfbirthday != null) {
                if (hfbirthday.value == "TRUE") {
                    ckbirthday.checked = true;
                }
                else {
                    ckbirthday.checked = false;
                }
            }

            if (hfnursesoap != null) {
                if (hfnursesoap.value == "TRUE") {
                    cknursesoap.checked = true;
                }
                else {
                    cknursesoap.checked = false;
                }
            }
        }

        function updateSettingUser(cekbox) {
            
            if (cekbox == "CheckBoxBirthday")
            {
                var data_birthday = document.getElementById(cekbox);
                document.getElementById('<%= HF_birthday.ClientID %>').value = data_birthday.checked;
            }else if (cekbox == "CheckBoxNurseSOAP")
            {
                var data_nursesoap = document.getElementById(cekbox);
                document.getElementById('<%= HF_nursesoap.ClientID %>').value = data_nursesoap.checked;
            }
        }

       <%-- function switchBahasa() {
            var bahasa = document.getElementById('<%=HFisBahasa.ClientID%>').value;
            if (bahasa == "ENG") {

                document.getElementById('lblbhs_changepass').innerHTML = "Change Password";
                document.getElementById('lblbhs_recentpass').innerHTML = "Recent Password";
                document.getElementById('lblbhs_newpass').innerHTML = "New Password";
                document.getElementById('lblbhs_confirmnewpass').innerHTML = "Confirm New Password";
            }
            else if (bahasa == "IND") {

                document.getElementById('lblbhs_changepass').innerHTML = "Ubah Password";
                document.getElementById('lblbhs_recentpass').innerHTML = "Password Sekarang";
                document.getElementById('lblbhs_newpass').innerHTML = "Password Baru";
                document.getElementById('lblbhs_confirmnewpass').innerHTML = "Konfirmasi Password Baru";
            }
        }--%>
    </script>

    <!--###########################################################################################################################################-->
    <!------------------------------------------------------------------ The Content ---------------------------------------------------------------->

    <asp:HiddenField ID="HFisBahasa" runat="server" />

    <div class="row" style="margin-top:15px;">
        <div class="col-sm-3">
        </div>

        <div class="col-sm-6 Contentutama">
            <div class="row borderTitle" style="padding-top: 10px; padding-bottom: 10px">
                <div class="col-sm-8 TeksHeader" style="padding-top: 5px;"><b> <label id="lblbhs_changepass" style="font-size:16px;">Setting</label> </b> </div>
                <div class="col-sm-4 text-right"> 
                    <asp:UpdateProgress ID="PassuProgSAVE" runat="server" AssociatedUpdatePanelID="UpdatePanelSAVE">
                        <ProgressTemplate>
                         <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center"></div>
                        <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                </div>
            </div>

            <br />

            <asp:UpdatePanel ID="UpdatePanel1" runat="server"> <ContentTemplate>
            <div class="row" style="border-bottom:1px dashed lightgrey; padding-top:10px; display:none;" id="settingbirthday" runat="server">
                <div class="col-sm-9">
                    <label style="font-size:14px;font-weight:bold;"> Hidupkan Notifikasi Ulang Tahun </label> 
                    <p class="text-muted">Munculkan popup Ulang Tahun ketika hari ulang tahun tiba</p>
                </div>
                <div class="col-sm-3 text-center" style="padding-top:4px;">
                    <input id="CheckBoxBirthday" class="CheckBoxSwitch" type="checkbox" data-toggle="toggle" data-on="ON" data-off="OFF" data-onstyle="success" data-offstyle="default" data-size="small" onchange="updateSettingUser('CheckBoxBirthday');">
                    <asp:HiddenField ID="HF_birthday" runat="server" />
                </div>
            </div>
            <div class="row" style="border-bottom:1px dashed lightgrey; padding-top:10px;" id="settingpopupFA" runat="server">
                <div class="col-sm-9">
                    <label style="font-size:14px;font-weight:bold;"> Munculkan Nurse SOAP </label> 
                    <p class="text-muted">Munculkan popup Nurse SOAP setiap masuk ke menu SOAP</p>
                </div>
                <div class="col-sm-3 text-center" style="padding-top:4px;">
                    <input id="CheckBoxNurseSOAP" class="CheckBoxSwitch" type="checkbox" data-toggle="toggle" data-on="ON" data-off="OFF" data-onstyle="success" data-offstyle="default" data-size="small" onchange="updateSettingUser('CheckBoxNurseSOAP');">
                    <asp:HiddenField ID="HF_nursesoap" runat="server" />
                </div>
            </div>
            <div class="row" style="border-bottom:1px dashed lightgrey; padding-top:10px;" id="settingsoaptemplate" runat="server">
                <div class="col-sm-12">
                    <label style="font-size:14px;font-weight:bold;"> Set Default Template </label> 
                    <p class="text-muted">Mengatur Otomatis template SOAP saat pertama kali load data</p>
                </div>
                <div class="col-sm-12" style="padding-bottom:15px;">
                    <asp:DropDownList Style="cursor: pointer; border-radius: 2px; border: solid 1px #efefef;" ID="ddlForm_Type" Width="280px" Height="25px" runat="server">
                    </asp:DropDownList>
                    <asp:HiddenField ID="HF_pageselect" runat="server" />
                </div>
            </div>
            <div class="row" style="border-bottom:1px dashed lightgrey; padding-top:10px;" id="settingadmtype" runat="server">
                <div class="col-sm-12">
                    <label style="font-size:14px;font-weight:bold;"> Set Default Admission Type </label> 
                    <p class="text-muted">Mengatur Otomatis Admission Type pada Worklist</p>
                </div>
                <div class="col-sm-12" style="padding-bottom:15px;">
                    <asp:DropDownList Style="cursor: pointer; border-radius: 2px; border: solid 1px #efefef;" ID="ddlAdm_Type" Width="280px" Height="25px" runat="server">
                        <asp:ListItem Value="OPD"> OPD </asp:ListItem>
                        <asp:ListItem Value="ED"> ED </asp:ListItem>
                        <%--<asp:ListItem Value="IPD"> IPD </asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:HiddenField ID="HF_admselect" runat="server" />
                </div>
            </div>
            </ContentTemplate> </asp:UpdatePanel>

            <br /><br />

            <table border="0" style="width:100%">
                <tr>
                    <td> 
                        <asp:UpdatePanel ID="UpdatePanelCekPass" runat="server"> <ContentTemplate>
                        <b> <p style="color: red; display: none" id="p_Add" runat="server"> </p> </b> 
                        </ContentTemplate> </asp:UpdatePanel>
                    </td>
                    <td style="width:30%; text-align:right;">             
                        <asp:UpdatePanel ID="UpdatePanelSAVE" runat="server"> <ContentTemplate>
                        <asp:Button ID="ButtonSaveSetting" runat="server" Text="Save Changes" CssClass="btn btn-lightGreen" OnClick="ButtonSaveSetting_Click"></asp:Button>
                        </ContentTemplate> </asp:UpdatePanel>
                     </td>
                </tr>
            </table>

            <br />
        </div>

        <div class="col-sm-3">
        </div>
        
    </div>

    <!--###########################################################################################################################################-->
    <!------------------------------------------------------------------ The Modal ------------------------------------------------------------------>

    <!-- ##### Modal Change Password ##### -->
    <div class="modal fade" id="modalAfterSave" role="dialog" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 50px;" data-keyboard="false">
       
        <asp:UpdatePanel ID="UpdatePanelAfterChangepass" runat="server" UpdateMode="Conditional"> <ContentTemplate>

        <div class="modal-dialog" style="width: 500px">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button> 
                    <h4 class="modal-title">
                        <label style="color:#4e9c36;"> <i class="fa fa-check"></i> Change Password Success  </label>
                    </h4>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="text-center">
                        Your Password Has Been Changed <br /> <b> Klik OK to Relogin. </b>
                        <br /><br />
                        <%--<asp:Button ID="ButtonRelogin" runat="server" Text="OK" class="btn btn-success" Style="width:90px;" OnClick="ButtonRelogin_Click"/> --%>
                    </div>
                </div>
            </div>
        </div>
        </ContentTemplate> </asp:UpdatePanel>
    </div>
    <!-- End of Modal Change Password -->

</asp:Content>
