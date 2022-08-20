<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdTriage.ascx.cs" Inherits="Form_SOAP_Control_Template_Specialty_StdTriage" %>

<style>
    .paddingTKA {
        height: 80px;
        padding-top: 15px;
        padding-bottom: 15px;
        border-right: 1px solid lightgrey;
        border-top: 1px solid lightgrey;
        border-bottom: 1px solid lightgrey;
    }
</style>

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

    function ShowDvPasienDirujuk() {
        var rbpasienrujuk = document.getElementById("<%=rbPasienDirujuk.ClientID %>");
        var divpasienrujuk = document.getElementById("dvPasienDirujuk");
        var divpasienlain = document.getElementById("dvPasienLain");
        if (rbpasienrujuk.checked) {
            divpasienrujuk.style.display = "inline-block";
            divpasienlain.style.display = "none";
        }
    }
    function ShowDvPasienLain() {
        var rbpasienlain = document.getElementById("<%=rbPasienLain.ClientID %>");
        var divpasienrujuk = document.getElementById("dvPasienDirujuk");
        var divpasienlain = document.getElementById("dvPasienLain");
        if (rbpasienlain.checked) {
            divpasienrujuk.style.display = "none";
            divpasienlain.style.display = "inline-block";
        }
    }
    function HideDvPasienDatang() {
        var rbpasiensendiri = document.getElementById("<%=rbPasienSendiri.ClientID %>");
        var divpasienrujuk = document.getElementById("dvPasienDirujuk");
        var divpasienlain = document.getElementById("dvPasienLain");
        if (rbpasiensendiri.checked) {
            divpasienrujuk.style.display = "none";
            divpasienlain.style.display = "none";
        }
    }

    function copytexttoINV(objsrc, objdst) {
        var from_elm = document.getElementById('MainContent_Triage_' + objsrc);
        var to_elm = document.getElementsByClassName(objdst);

        if (to_elm[0] != null) {
            to_elm[0].value = from_elm.value;
        }
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

<div class="col-lg-12" style="margin: 0px;">
     <div class="mini-dialog" style="margin-bottom: 15px;">
         <div class="modal-header mini-header" style="padding:0px;">
             <table style="width:100%;">
                 <tr>
                     <td style="width:50%; padding-left:15px; padding-top:6px; padding-bottom:6px;">
                         <label>Triage</label>

                         <div style="display:inline-block; float:right;">
                             <div class="radio-margin" style="display:inline-block;">
                                <div class="pretty p-default p-round">
                                    <asp:RadioButton runat="server" GroupName="triage" Value="1" ID="rbtriage1" />
                                    <div class="state p-primary-o">
                                        <label><b>1</b></label>
                                    </div>
                                </div>
                            </div>
                             <div class="radio-margin" style="display:inline-block; margin-left:50px;">
                                <div class="pretty p-default p-round">
                                    <asp:RadioButton runat="server" GroupName="triage" Value="2" ID="rbtriage2" />
                                    <div class="state p-primary-o">
                                        <label><b>2</b></label>
                                    </div>
                                </div>
                            </div>
                             <div class="radio-margin" style="display:inline-block; margin-left:50px;">
                                <div class="pretty p-default p-round">
                                    <asp:RadioButton runat="server" GroupName="triage" Value="3" ID="rbtriage3" />
                                    <div class="state p-primary-o">
                                        <label><b>3</b></label>
                                    </div>
                                </div>
                            </div>
                         </div>
                         
                     </td>
                     <td style="width:50%; padding:6px; background-color:#2a3593; border-top-right-radius:7px;">
                         <div style="display:inline-block; background-color: white; padding: 1px 4px 2px 9px; border-radius: 5px; color: #2a3593;">
                             <div class="radio-margin" style="display:inline-block;">
                                <div class="pretty p-default p-round">
                                    <asp:RadioButton runat="server" GroupName="trauma" Value="Trauma" ID="rbtrauma" />
                                    <div class="state p-primary-o">
                                        <label><b>Trauma</b></label>
                                    </div>
                                </div>
                            </div>
                             <div class="radio-margin" style="display:inline-block; margin-left:25px;">
                                <div class="pretty p-default p-round">
                                    <asp:RadioButton runat="server" GroupName="trauma" Value="Non-trauma" ID="rbnontrauma" />
                                    <div class="state p-primary-o">
                                        <label><b>Non-trauma</b></label>
                                    </div>
                                </div>
                            </div>
                         </div>
                     </td>
                 </tr>
             </table>
                
        </div>
         <div class="modal-body" style="padding-top: 0px; padding-bottom: 0px;" id="modal_triage" runat="server">
             <asp:UpdatePanel runat="server" ID="upTriage">
                <ContentTemplate>

                    <div class="headerMargin"> 
                        <div class="row">
                            <div class="col-sm-6">
                                <strong>Cara pasien datang</strong>
                                <div class="radio-margin">
                                    <div class="pretty p-default p-round">
                                        <asp:RadioButton runat="server" GroupName="pasien" Value="Sendiri" ID="rbPasienSendiri" onclick="HideDvPasienDatang()" />
                                        <div class="state p-primary-o">
                                            <label>Sendiri</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="radio-margin">
                                    <div class="pretty p-default p-round">
                                        <asp:RadioButton runat="server" GroupName="pasien" Value="Dirujuk" ID="rbPasienDirujuk" onclick="ShowDvPasienDirujuk()" />
                                        <div class="state p-primary-o">
                                            <label>Dirujuk oleh</label>
                                        </div>
                                    </div>
                                    <div style="display: none; width: 350px;" id="dvPasienDirujuk">
                                        <asp:TextBox runat="server" placeholder="Ketik disini..." Style="height: 23px; width: 100%; max-width: 300px;" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtPasienDirujuk" onkeypress="return checkenter();" />
                                    </div>
                                </div>
                                <div class="radio-margin">
                                    <div class="pretty p-default p-round">
                                        <asp:RadioButton runat="server" GroupName="pasien" Value="Lain-lain" ID="rbPasienLain" onclick="ShowDvPasienLain()" />
                                        <div class="state p-primary-o">
                                            <label>Lain-lain</label>
                                        </div>
                                    </div>
                                    <div style="display: none; width: 350px;" id="dvPasienLain">
                                        <asp:TextBox runat="server" placeholder="Ketik disini..." Style="height: 23px; width: 100%; max-width: 318px" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtPasienLain" onkeypress="return checkenter();" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <strong>Keadaan Umum</strong>
                                <div class="radio-margin">
                                    <div class="pretty p-default p-round">
                                        <asp:RadioButton runat="server" GroupName="keadaan1" Value="Baik" ID="rbKeadaanBaik" />
                                        <div class="state p-primary-o">
                                            <label>Baik</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="radio-margin">
                                    <div class="pretty p-default p-round">
                                        <asp:RadioButton runat="server" GroupName="keadaan1" Value="Sedang" ID="rbKeadaanSedang" />
                                        <div class="state p-primary-o">
                                            <label>Sedang</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="radio-margin">
                                    <div class="pretty p-default p-round">
                                        <asp:RadioButton runat="server" GroupName="keadaan1" Value="Buruk" ID="rbKeadaanBuruk" />
                                        <div class="state p-primary-o">
                                            <label>Buruk</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    </div>

                    <div class="headerMargin"> 
                        <div class="row">
                            <div class="col-sm-3 paddingTKA">
                                <strong>Airway</strong>
                                <br />
                                <asp:TextBox runat="server" Style="height: 23px; width: 100%; border:none; outline:none;" placeholder="ketik disini..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtAirway" onkeypress="return checkenter();" /> 
              
                            </div>
                            <div class="col-sm-3 paddingTKA">
                                <strong>Breathing</strong>
                                <br />
                                <asp:TextBox runat="server" Style="height: 23px; width: 100%; border:none; outline:none;" placeholder="ketik disini..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtBreathing" onkeypress="return checkenter();" /> 

                            </div>
                            <div class="col-sm-3 paddingTKA">
                                <strong>Circulation</strong>
                                <br />
                                <asp:TextBox runat="server" Style="height: 23px; width: 100%; border:none; outline:none;" placeholder="ketik disini..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtCirculation" onkeypress="return checkenter();" /> 
                             
                            </div>
                            <div class="col-sm-3 paddingTKA" style="border-right:none;">
                                <strong>Disability</strong>
                                <br />
                                <asp:TextBox runat="server" Style="height: 23px; width: 100%; border:none; outline:none;" placeholder="ketik disini..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtDisability" onkeypress="return checkenter();" /> 
                               
                            </div>
                            
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
         </div>
     </div>
</div>