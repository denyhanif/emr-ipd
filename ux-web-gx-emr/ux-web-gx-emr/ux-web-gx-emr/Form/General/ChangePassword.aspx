<%@ Page Title="Change Password" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="ChangePassword.aspx.cs" Inherits="Form_General_ChangePassword" %>

<asp:Content ID="ChangePassword" ContentPlaceHolderID="MainContent" runat="server">
    <!--###########################################################################################################################################-->
    <!------------------------------------------------------------------ The Script ----------------------------------------------------------------->

    <script type="text/javascript">
         $(window).load(function() {
		    $(".loadPage").fadeOut("slow");
        });

        //fungsi  untuk validasi input text kosong, lalu menampilkan notifikasi text berwarna merah
        function FormCheck()
        {
            if ($("[id$='Pass_TextOldPass']").val().length == 0) {
                $("[id$='Pass_TextOldPass']").focus();
                $("[id$='p_Add']").removeAttr("style");
                $("[id$='p_Add']").attr("style", "display:block; color:red;");
                document.getElementById('<%= p_Add.ClientID %>').innerHTML = "Old Password cannot be empty!";

                return false;
            }
            else if ($("[id$='Pass_TextNewPass']").val().length == 0) {
                $("[id$='Pass_TextNewPass']").focus();
                $("[id$='p_Add']").removeAttr("style");
                $("[id$='p_Add']").attr("style", "display:block; color:red;");
                document.getElementById('<%= p_Add.ClientID %>').innerHTML = "New Password cannot be empty!";

                return false;
            }
            else if ($("[id$='Pass_TextNewPass_confirm']").val().length == 0) {
                $("[id$='Pass_TextNewPass_confirm']").focus();
                $("[id$='p_Add']").removeAttr("style");
                $("[id$='p_Add']").attr("style", "display:block; color:red;");
                document.getElementById('<%= p_Add.ClientID %>').innerHTML = "Confirm New Password cannot be empty!";

                return false;
            }
            else if ($("[id$='Pass_TextNewPass']").val() != $("[id$='Pass_TextNewPass_confirm']").val()) {
                $("[id$='Pass_TextNewPass_confirm']").focus();
                $("[id$='p_Add']").removeAttr("style");
                $("[id$='p_Add']").attr("style", "display:block; color:red;");
                document.getElementById('<%= p_Add.ClientID %>').innerHTML = "Confirm New Password must be same with New Password!";

                return false;
            }

            return validatePass($("[id$='Pass_TextNewPass']").val());
        }

        function validatePass(objval) {
                var value = objval;
                var regex = /^(?=.{8,})(?=.*[a-zA-Z])(?=.*[0-9]).*$/; //(?=.*[@#$%^&+=])
                var bolvalue = regex.test(value);
                if (bolvalue == true) {
                    $("[id$='p_Add']").removeAttr("style");
                    document.getElementById('<%= p_Add.ClientID %>').innerHTML = "";
                    return true;
                }
                else {
                    $("[id$='Pass_TextNewPass']").focus();
                    $("[id$='p_Add']").removeAttr("style");
                    $("[id$='p_Add']").attr("style", "display:block; color:red;");
                    document.getElementById('<%= p_Add.ClientID %>').innerHTML = "The password must has minimum 8 characters at least 1 Alphabet and 1 Number!"; //and 1 Special Character
                    return false;
                }
            }

        //fungsi event klik pada area diluar modal
        $(document).ready(function () { 
            $('#modalAfterSave').on('hidden.bs.modal', function (e) {
                 document.getElementById('<%= ButtonRelogin.ClientID %>').click();
             });
        });

        function switchBahasa() {
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
        }
    </script>

    <!--###########################################################################################################################################-->
    <!------------------------------------------------------------------ The Content ---------------------------------------------------------------->

    <asp:HiddenField ID="HFisBahasa" runat="server" />

    <div class="row" style="margin-top:8%;">
        <div class="col-sm-4">
        </div>

        <div class="col-sm-4 Contentutama">
            <div class="row borderTitle" style="padding-top: 10px; padding-bottom: 10px">
                <div class="col-sm-8 TeksHeader" style="padding-top: 5px;"><b> <label id="lblbhs_changepass" style="font-size:16px;">Change Password</label> </b> </div>
                <div class="col-sm-4 text-right"> 
                    <asp:UpdateProgress ID="PassuProgSAVE" runat="server" AssociatedUpdatePanelID="UpdatePanelSAVE">
                        <ProgressTemplate>
                        <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                </div>
            </div>

            <br />

            <div class="form-group">
                <label id="lblbhs_recentpass">Recent password</label>
                    <asp:TextBox ID="Pass_TextOldPass" runat="server" CssClass="MaxWidthTextbox form-control" TextMode="Password" placeholder="Type here..." AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="form-group">
                <label id="lblbhs_newpass">New password</label>
                    <asp:TextBox ID="Pass_TextNewPass" runat="server" CssClass="MaxWidthTextbox form-control" TextMode="Password" placeholder="Type here..." AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="form-group">
                <label id="lblbhs_confirmnewpass">Confirm new password</label>
                    <asp:TextBox ID="Pass_TextNewPass_confirm" runat="server" CssClass="MaxWidthTextbox form-control" TextMode="Password" placeholder="Type here..." AutoCompleteType="Disabled"></asp:TextBox>
            </div>

            <table border="0" style="width:100%">
                <tr>
                    <td> 
                        <asp:UpdatePanel ID="UpdatePanelCekPass" runat="server"> <ContentTemplate>
                        <b> <p style="color: red; display: none" id="p_Add" runat="server"> </p> </b> 
                        </ContentTemplate> </asp:UpdatePanel>
                    </td>
                    <td style="width:30%; text-align:right;">             
                        <asp:UpdatePanel ID="UpdatePanelSAVE" runat="server"> <ContentTemplate>
                        <asp:Button ID="Pass_ButtonSavePass" runat="server" Text="Change Password" CssClass="btn btn-lightGreen" OnClientClick="return FormCheck()" OnClick="Pass_ButtonSavePass_Click"></asp:Button>
                        </ContentTemplate> </asp:UpdatePanel>
                     </td>
                </tr>
            </table>

            <br />
        </div>

        <div class="col-sm-4">
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
                        <asp:Button ID="ButtonRelogin" runat="server" Text="OK" class="btn btn-success" Style="width:90px;" OnClick="ButtonRelogin_Click"/> 
                    </div>
                </div>
            </div>
        </div>
        </ContentTemplate> </asp:UpdatePanel>
    </div>
    <!-- End of Modal Change Password -->

</asp:Content>
