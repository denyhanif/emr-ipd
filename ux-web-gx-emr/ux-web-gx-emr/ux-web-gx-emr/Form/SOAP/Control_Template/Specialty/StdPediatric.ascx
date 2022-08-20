<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdPediatric.ascx.cs" Inherits="Form_SOAP_Control_Template_Specialty_StdPediatric" %>


<script type="text/javascript">
    
    function txtOnKeyPress() {
        var c = event.keyCode;
        if (c == 13) {
            return false;
        }
    }

    function CheckNumeric() {
        return event.keyCode >= 48 && event.keyCode <= 57 || event.keyCode == 46;
    }

    function ShowDvRiwayatPersalinan() {
        var rbpersalinanlain = document.getElementById("<%=rbpersalinanlain.ClientID %>");
        var divRP = document.getElementById("dvRiwayatPersalinanLain");
        if (rbpersalinanlain.checked) {
            divRP.style.display = "inline-block";
        }
    }

    function HideDvRiwayatPersalinan() {
        var rbspontan = document.getElementById("<%=rbpersalinanspontan.ClientID %>");
        var rbsectio = document.getElementById("<%=rbpersalinansectio.ClientID %>");
        var rbvacum = document.getElementById("<%=rbpersalinanvacum.ClientID %>");
        var rbforceps = document.getElementById("<%=rbpersalinanforceps.ClientID %>");
        var divRP = document.getElementById("dvRiwayatPersalinanLain");
        if (rbspontan.checked || rbsectio.checked || rbvacum.checked || rbforceps.checked) {
            divRP.style.display = "none";
        }
    }

    function ShowDvPenyulitSalin() {
        var rbpenyulityes = document.getElementById("<%=rbpenyulityes.ClientID %>");
        var divpenyulit = document.getElementById("dvPenyulit");
        if (rbpenyulityes.checked) {
            divpenyulit.style.display = "inline";
        }
    }
    function HideDvPenyulitSalid() {
        var rbpenyulitno = document.getElementById("<%=rbpenyulitno.ClientID %>");
        var divpenyulit = document.getElementById("dvPenyulit");
        if (rbpenyulitno.checked) {
            divpenyulit.style.display = "none";
        }
    }
    
    function HidePreviewPediatric() {
        $('#modalEditPediatric').modal('hide');
        return true;
    }

    //////////////////////////////////////////////////////////////////////

    //fungsi event klik pada area diluar modal
        //$(document).ready(function () { 

        //    //fungsi untuk mempertahankan style saat postback di updatepanel
        //    var prm = Sys.WebForms.PageRequestManager.getInstance();
        //    if (prm != null) {
        //        prm.add_endRequest(function (sender, e) {
        //            if (sender._postBackSettings.panelsToUpdate != null) {
        //                //updateLabel();
        //            }
        //        });
        //    };
        //});
</script>

 <asp:UpdatePanel ID="UpdatePanelModalPediatric" runat="server">
        <ContentTemplate>

<div class="row">
    <div class="col-sm-6">
        <div style="padding-bottom:15px;">
            <strong>Lama Kehamilan</strong>
            <br />
            <asp:TextBox runat="server" placeholder="Ketik disini..." Style="height: 23px; width: 100%; max-width: 100%" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtLamaKehamilan" onkeydown="return txtOnKeyPress();" /> 
        </div>

        <div style="padding-bottom:15px;">
        <strong>Riwayat Persalinan</strong>
        <div class="radio-margin" style="margin-top: 5px; margin-bottom: 5px;">
            <div class="pretty p-default p-round">
                <asp:RadioButton runat="server" GroupName="persalinan1" Value="0" ID="rbpersalinanspontan" onclick="HideDvRiwayatPersalinan()" />
                <div class="state p-primary-o">
                    <label>Spontan</label>
                </div>
            </div>
        </div>
        <div class="radio-margin" style="margin-top: 5px; margin-bottom: 5px;">
            <div class="pretty p-default p-round">
                <asp:RadioButton runat="server" GroupName="persalinan1" Value="1" ID="rbpersalinansectio" onclick="HideDvRiwayatPersalinan()" />
                <div class="state p-primary-o">
                    <label>Sectio</label>
                </div>
            </div>
        </div>
        <div class="radio-margin" style="margin-top: 5px; margin-bottom: 5px;">
            <div class="pretty p-default p-round">
                <asp:RadioButton runat="server" GroupName="persalinan1" Value="2" ID="rbpersalinanvacum" onclick="HideDvRiwayatPersalinan()" />
                <div class="state p-primary-o">
                    <label>Vacum</label>
                </div>
            </div>
        </div>
        <div class="radio-margin" style="margin-top: 5px; margin-bottom: 5px;">
            <div class="pretty p-default p-round">
                <asp:RadioButton runat="server" GroupName="persalinan1" Value="3" ID="rbpersalinanforceps" onclick="HideDvRiwayatPersalinan()" />
                <div class="state p-primary-o">
                    <label>Forceps</label>
                </div>
            </div>
        </div>
        <div class="radio-margin" style="margin-top: 5px; margin-bottom: 5px;">
            <div class="pretty p-default p-round">
                <asp:RadioButton runat="server" GroupName="persalinan1" Value="4" ID="rbpersalinanlain" onclick="ShowDvRiwayatPersalinan()" />
                <div class="state p-primary-o">
                    <label>Lain-lain</label>
                </div>
            </div>

            <div style="display: none; width: 300px;" id="dvRiwayatPersalinanLain">
                <asp:TextBox runat="server" Style="height: 23px; width: 100%; max-width: 200px; vertical-align: text-top;" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtriwayatpersalinan" onkeydown="return txtOnKeyPress();" />
            </div>
        </div>
        </div>

        <div style="padding-bottom:15px;">
            <strong>Berat Badan Lahir</strong>
            <br />
            <asp:TextBox runat="server" placeholder="Ketik disini..." Style="height: 23px; width: 100%; max-width: 100px" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtBeratBadanLahir" onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();" /> 
            <label style="color: #b2b6d1; font-size: 12px; font-family: Helvetica, Arial, sans-serif;"> gram </label>
        </div>

    </div>
    <div class="col-sm-6">

        <div style="padding-bottom:15px;">
            <strong>Komplikasi Kehamilan</strong>
            <br />
            <asp:TextBox runat="server" placeholder="Ketik disini..." Style="height: 23px; width: 100%; max-width: 100%" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtKomplikasiKehamilan" onkeydown="return txtOnKeyPress();" /> 
        </div>

        <div style="padding-bottom:15px;">
        <strong>Penyulit Persalinan</strong>
        <div class="radio-margin" style="margin-top: 5px; margin-bottom: 5px;">
            <div class="pretty p-default p-round">
                <asp:RadioButton runat="server" GroupName="penyulit1" Value="0" ID="rbpenyulitno" onclick="HideDvPenyulitSalid();" />
                <div class="state p-primary-o">
                    <label>Tidak Ada</label>
                </div>
            </div>
        </div>
        <div class="radio-margin" style="margin-top: 5px; margin-bottom: 5px;">
            <div class="pretty p-default p-round">
                <asp:RadioButton runat="server" GroupName="penyulit1" Value="1" ID="rbpenyulityes" onclick="ShowDvPenyulitSalin();" />
                <div class="state p-primary-o">
                    <label>Ada</label>
                </div>
            </div>
            <div style="display:none;" id="dvPenyulit">
                &nbsp; , Jelaskan
                <asp:TextBox runat="server" placeholder="Ketik disini..." Style="height: 23px; width: 100%; max-width: 250px;" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtpenyulit"  onkeydown="return txtOnKeyPress();" />
            </div>
        </div>
        </div>

        <div style="padding-bottom:15px;">
            <strong>Panjang Badan Lahir</strong>
            <br />
            <asp:TextBox runat="server" placeholder="Ketik disini..." Style="height: 23px; width: 100%; max-width: 100px" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtPanjangBadanLahir" onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();" /> 
            <label style="color: #b2b6d1; font-size: 12px; font-family: Helvetica, Arial, sans-serif;"> cm </label>
        </div>
    </div>

     <div class="text-right col-sm-12" style="padding-right: 15px; padding-bottom: 15px; padding-top: 15px;">
        <div style="height: 22px; text-align: right;">
            <asp:UpdateProgress ID="UpdateProgressModalPediatric" runat="server" AssociatedUpdatePanelID="UpdatePanelModalPediatric">
                <ProgressTemplate>
                    <div class="modal-backdrop" style="background-color: white; opacity: 0.3; text-align: center">
                    </div>
                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                    &nbsp;
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <asp:Button CssClass="btn btn-default" runat="server" Text="Cancel" OnClientClick="HidePreviewPediatric();" />
        <asp:Button ID="btnsubmitpediatric" runat="server" CssClass="btn btn-lightGreen" Text="Save" OnClick="btnsubmitpediatric_Click" />
    </div>

</div>

        </ContentTemplate>
</asp:UpdatePanel>

