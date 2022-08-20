<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdObgyn.ascx.cs" Inherits="Form_SOAP_Control_Template_Specialty_StdObgyn" %>


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

    function ShowDvHaid() {
        var rbhaidtidakteratur = document.getElementById("<%=rbhaidtidakteratur.ClientID %>");
        var divhaid = document.getElementById("dvHaid");
        if (rbhaidtidakteratur.checked) {
            divhaid.style.display = "inline-block";
        }
    }
    function HideDvHaid() {
       var rbhaidteratur = document.getElementById("<%=rbhaidteratur.ClientID %>");
        var divhaid = document.getElementById("dvHaid");
        if (rbhaidteratur.checked) {
            divhaid.style.display = "none";
        }
    }

    function ShowDvKontrasepsi() {
        var rbkontrasepsiyes = document.getElementById("<%=rbkontrasepsiyes.ClientID %>");
        var divkontrasepsi = document.getElementById("dvKontrasepsi");
        if (rbkontrasepsiyes.checked) {
            divkontrasepsi.style.display = "block";
        }
    }
    function HideDvKontrasepsi() {
        var rbkontrasepsino = document.getElementById("<%=rbkontrasepsino.ClientID %>");
        var divkontrasepsi = document.getElementById("dvKontrasepsi");
        if (rbkontrasepsino.checked) {
            divkontrasepsi.style.display = "none";
        }
    }

    function txtOnKeyPressContraception() {
        var c = event.keyCode;
        if (c == 13) {
            var jenis = $("[id$='txtJenisKontrasepsi']").val();
            var sejak = $("[id$='txtSejakKontrasepsi']").val();
            var hingga = $("[id$='txtHinggaKontrasepsi']").val();

            $("[id$='txtJenisKontrasepsi']").removeAttr("style");
            $("[id$='txtSejakKontrasepsi']").removeAttr("style");
            $("[id$='txtHinggaKontrasepsi']").removeAttr("style");

            $("[id$='txtJenisKontrasepsi']").attr("style", "max-width:175px; width:100%");
            $("[id$='txtSejakKontrasepsi']").attr("style", "max-width:150px; width:100%");
            $("[id$='txtHinggaKontrasepsi']").attr("style", "max-width:150px; width:100%");

            if (jenis.length <= 0) {
                $("[id$='txtJenisKontrasepsi']").attr("style", "outline-color:red;max-width:175px; width:100%");
                $("[id$='txtJenisKontrasepsi']").focus();
                return false;
            }
            else if (sejak.length <= 0) {
                $("[id$='txtSejakKontrasepsi']").attr("style", "outline-color:red;max-width:150px; width:100%");
                $("[id$='txtSejakKontrasepsi']").focus();
                return false;
            }
            else if (hingga.length <= 0) {
                $("[id$='txtHinggaKontrasepsi']").attr("style", "outline-color:red;max-width:150px; width:100%");
                $("[id$='txtHinggaKontrasepsi']").focus();
                return false;
            }
            else {
                document.getElementById('<%=ButtonAddKontrasepsi.ClientID%>').click();
            }
            return false;
        }
    }

    function cekEmptyContraception() {
        var jenis = $("[id$='txtJenisKontrasepsi']").val();
        var sejak = $("[id$='txtSejakKontrasepsi']").val();
        var hingga = $("[id$='txtHinggaKontrasepsi']").val();

        $("[id$='txtJenisKontrasepsi']").removeAttr("style");
        $("[id$='txtSejakKontrasepsi']").removeAttr("style");
        $("[id$='txtHinggaKontrasepsi']").removeAttr("style");

        $("[id$='txtJenisKontrasepsi']").attr("style", "max-width:175px; width:100%");
        $("[id$='txtSejakKontrasepsi']").attr("style", "max-width:150px; width:100%");
        $("[id$='txtHinggaKontrasepsi']").attr("style", "max-width:150px; width:100%");

        if (jenis.length <= 0) {
            $("[id$='txtJenisKontrasepsi']").attr("style", "outline-color:red;max-width:175px; width:100%");
            $("[id$='txtJenisKontrasepsi']").focus();
            return false;
        }
        else if (sejak.length <= 0) {
            $("[id$='txtSejakKontrasepsi']").attr("style", "outline-color:red;max-width:150px; width:100%");
            $("[id$='txtSejakKontrasepsi']").focus();
            return false;
        }
        else if (hingga.length <= 0) {
            $("[id$='txtHinggaKontrasepsi']").attr("style", "outline-color:red;max-width:150px; width:100%");
            $("[id$='txtHinggaKontrasepsi']").focus();
            return false;
        }
        else {
            return true;
        }
        return false;
    }

    function HidePreviewObgyn() {
        $('#modalEditObgyn').modal('hide');
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

 <asp:UpdatePanel ID="UpdatePanelModalObgyn" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

<div class="row">
    <div class="col-sm-4">
        <strong>Menarche Umur</strong>
        <br />
        <asp:TextBox runat="server" placeholder="Ketik disini..." Style="height: 23px; width: 100%; max-width: 100px" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtMenarche" onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();" /> 
        <label style="color: #b2b6d1; font-size: 12px; font-family: Helvetica, Arial, sans-serif;"> tahun </label>
    </div>
    <div class="col-sm-4">
        <strong>Haid</strong>
        <div class="radio-margin">
            <div class="pretty p-default p-round">
                <asp:RadioButton runat="server" GroupName="haid1" Value="0" ID="rbhaidteratur" onclick="HideDvHaid();" />
                <div class="state p-primary-o">
                    <label>Teratur</label>
                </div>
            </div>
        </div>
        <div class="radio-margin">
            <div class="pretty p-default p-round">
                <asp:RadioButton runat="server" GroupName="haid1" Value="1" ID="rbhaidtidakteratur" onclick="ShowDvHaid();" />
                <div class="state p-primary-o">
                    <label>Tidak Teratur</label>
                </div>
            </div>
            <div style="display:none;" id="dvHaid">
                &nbsp; , lama
                <asp:TextBox runat="server" placeholder="Ketik disini..." Style="height: 23px; width: 100%; max-width: 100px;" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtHaidTakTeratur" onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();" />
                <label style="color: #b2b6d1; font-size: 12px; font-family: Helvetica, Arial, sans-serif;"> hari </label>
            </div>
        </div>
    </div>
    <div class="col-sm-4">
        <strong>Keluhan Saat Haid</strong>
        <br />
        <asp:TextBox runat="server" placeholder="Ketik disini..." Style="height: 23px; width: 100%; max-width: 100%" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtKeluhanHaid" onkeydown="return txtOnKeyPress();" /> 
    </div>

    <div class="col-sm-12" style="padding-top:15px;">
        <strong>Penggunaan Kontrasepsi</strong>
        <div class="radio-margin">
            <div class="pretty p-default p-round">
                <asp:RadioButton runat="server" GroupName="kontasepsi1" Value="0" ID="rbkontrasepsino" onclick="HideDvKontrasepsi();" />
                <div class="state p-primary-o">
                    <label>No</label>
                </div>
            </div>
        </div>
        <div class="radio-margin">
            <div class="pretty p-default p-round">
                <asp:RadioButton runat="server" GroupName="kontasepsi1" Value="1" ID="rbkontrasepsiyes" onclick="ShowDvKontrasepsi();" />
                <div class="state p-primary-o">
                    <label>Yes</label>
                </div>
            </div>
            <div style="display:none; width:90%; margin-top: 5px;" id="dvKontrasepsi">
                Jenis
                <asp:TextBox runat="server" placeholder="Ketik disini..." Style="height: 23px; width: 100%; max-width: 175px;" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtJenisKontrasepsi" onkeydown="return txtOnKeyPressContraception();" />
                &nbsp; &nbsp;  Sejak
                <asp:TextBox runat="server" placeholder="Ketik disini..." Style="height: 23px; width: 100%; max-width: 150px;" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtSejakKontrasepsi" onkeydown="return txtOnKeyPressContraception();" />
                &nbsp; &nbsp;  Hingga
                <asp:TextBox runat="server" placeholder="Ketik disini..." Style="height: 23px; width: 100%; max-width: 150px;" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtHinggaKontrasepsi" onkeydown="return txtOnKeyPressContraception();" />
                &nbsp; &nbsp; <asp:Button ID="ButtonAddKontrasepsi" runat="server" Text="Add" CssClass="btn btn-primary" style="font-family: Helvetica,Arial,sans-serif;font-size: 12px; width: 49px; height: 24px; padding-top: 3px; background-color: #2a3593; color: #ffffff;"  OnClientClick="return cekEmptyContraception();" OnClick="ButtonAddKontrasepsi_Click" /> 

                <div style="border: 1px solid lightgray; overflow-y: auto; max-height: 125px; margin-top:5px; width: 650px; margin-left:30px;" class="scrollNURSE">
                    <asp:GridView ID="gvw_kontrasepsi" runat="server" AutoGenerateColumns="False" CssClass="table-kecil"
                        DataKeyNames="pregnancy_data_id" EmptyDataText="No Data" BorderWidth="0" HeaderStyle-BorderWidth="0">
                        <Columns>
                            <asp:TemplateField HeaderText="Jenis Kontrasepsi" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="35%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                <ItemTemplate>
                                    <asp:HiddenField ID="kontrasepsi_id" runat="server" Value='<%# Bind("pregnancy_data_id") %>'></asp:HiddenField>
                                    <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="jeniskontrasespsi" runat="server" Text='<%# Bind("value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sejak" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="30%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                <ItemTemplate>
                                    <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="labelSejak" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hingga" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="30%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                <ItemTemplate>
                                    <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="labelHingga" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButtonDeleteKontrasepsi" runat="server"  ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="ImageButtonDeleteKontrasepsi_Click"  Style="width: 12px; height: 12px;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>
    
    <div class="col-sm-12">
        <hr />
        <strong>Pregnancy History</strong>
        <asp:GridView ID="gvw_riwayathamil" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-riwayathamil"
        DataKeyNames="pregnancy_history_id" OnRowDataBound="gvw_riwayathamil_RowDataBound">
        <Columns>
            <asp:TemplateField ItemStyle-Width="6%" HeaderText="Hamil ke" ItemStyle-VerticalAlign="Middle">
                <ItemTemplate>
                    <asp:HiddenField ID="HF_riwayathamilid" runat="server" Value='<%# Bind("pregnancy_history_id") %>' />
                    <asp:TextBox ID="TextBoxHamilKe" Style="width:50px; text-align:center;" runat="server" Text='<%# Bind("pregnancy_sequence") %>' onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="12%" HeaderText="Umur Anak" ItemStyle-VerticalAlign="Middle">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxUmur" Style="width:50px;" runat="server" Text='<%# Bind("child_age") %>' onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();"></asp:TextBox>
                    <asp:DropDownList ID="DDLUmur" runat="server" style="width:70px;">
                        <asp:ListItem Value="-">- select -</asp:ListItem>
                        <asp:ListItem Value="Hari">Hari</asp:ListItem>
                        <asp:ListItem Value="Minggu">Minggu</asp:ListItem>
                        <asp:ListItem Value="Bulan">Bulan</asp:ListItem>
                        <asp:ListItem Value="TAHUN">Tahun</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="8%" HeaderText="Jenis Kelamin" ItemStyle-VerticalAlign="Middle">
                <ItemTemplate>
                    <asp:DropDownList ID="DDLjk" runat="server">
                        <asp:ListItem Value="0">- N/A -</asp:ListItem>
                        <asp:ListItem Value="1">Pria</asp:ListItem>
                        <asp:ListItem Value="2">Wanita</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="8%" HeaderText="BBL" ItemStyle-VerticalAlign="Middle">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxBBL" Style="width:50px;" runat="server" Text='<%# Bind("BBL") %>' onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();"></asp:TextBox> <label style="color:lightgray;"> gr</label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="14%" HeaderText="Cara Persalinan" ItemStyle-VerticalAlign="Middle">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxPersalinan" runat="server" Text='<%# Bind("labor_type") %>' onkeydown="return txtOnKeyPress();"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="14%" HeaderText="Ditolong Oleh" ItemStyle-VerticalAlign="Middle">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxDitolong" runat="server" Text='<%# Bind("labor_helper") %>' onkeydown="return txtOnKeyPress();"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="14%" HeaderText="Tempat" ItemStyle-VerticalAlign="Middle">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxTempatLahir" runat="server" Text='<%# Bind("labor_place") %>' onkeydown="return txtOnKeyPress();"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Lahir Hidup/Mati" ItemStyle-VerticalAlign="Middle">
                <ItemTemplate>
                    <asp:DropDownList ID="DDLhidupmati" runat="server">
                        <asp:ListItem Value="0">- select -</asp:ListItem>
                        <asp:ListItem Value="1">Hidup</asp:ListItem>
                        <asp:ListItem Value="2">Mati</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="8%" ItemStyle-VerticalAlign="Middle">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButtonDeleteKehamilan" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="ImageButtonDeleteKehamilan_Click" Style="width: 12px; height: 12px; vertical-align: middle;" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

        <asp:Button ID="ButtonAddKehamilan" CssClass="btn btn-primary btn-sm" runat="server" Text="+ Tambah" OnClick="ButtonAddKehamilan_Click" />
    </div>

     <div class="text-right col-sm-12" style="padding-right: 15px; padding-bottom: 15px; padding-top: 15px;">
        <div style="height: 22px; text-align: right;">
            <asp:UpdateProgress ID="UpdateProgressModalObgyn" runat="server" AssociatedUpdatePanelID="UpdatePanelModalObgyn">
                <ProgressTemplate>
                    <div class="modal-backdrop" style="background-color: white; opacity: 0.3; text-align: center">
                    </div>
                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                    &nbsp;
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <asp:Button CssClass="btn btn-default" runat="server" Text="Cancel" OnClientClick="HidePreviewObgyn();" />
        <asp:Button ID="btnsubmitobgyn" runat="server" CssClass="btn btn-lightGreen" Text="Save" OnClick="btnsubmitobgyn_Click" />
    </div>

</div>

        </ContentTemplate>
</asp:UpdatePanel>

