<%@ Control Language="C#" AutoEventWireup="true" CodeFile="KebutuhanInformasi.ascx.cs" Inherits="Form_General_FirstAssesment_Control_Template_KebutuhanInformasi" %>
<script type="text/javascript">
    function ShowHidedvPenerjemah() {
        var chkYes = document.getElementById("<%=rbPenerjemah2.ClientID %>");
        var dvPassport = document.getElementById("dvPenerjemah");
        if (chkYes.checked) {
            dvPassport.style.display = "";
        }
    }

    function HidePdvPenerjemah() {
        var chkYes = document.getElementById("<%=rbPenerjemah1.ClientID %>");
        var dvPassport = document.getElementById("dvPenerjemah");
        if (chkYes.checked) {
            dvPassport.style.display = "none";
        }
    }
function txtOnKeyPress()   
    {   
       var c = event.keyCode;  
       if (c == 13)   
       {     
           return false;
       }        
    }  
</script>

    <div class="col-lg-12" style="margin:0px;padding-right:0px">
        <div style="min-height:120px;background-color:white; width:100%;border:1px; border-radius: 7px;box-shadow: 0px 2px 5px #9293A0;margin-top:0px;margin-left:0px" class="modal-dialog center-block">
           <div>
               <label style="background-color:white;color:#000000;font-family:Helvetica, Arial, sans-serif;font-weight:bold;font-size:14px;border-top-left-radius: 7px;border-top-right-radius: 7px;" class="form-control">Kebutuhan Informasi & Edukasi</label>
           </div>
           <div class="modal-body" style="padding-top:0px;padding-bottom:25px">

            <asp:UpdatePanel runat="server" ID="upKebutuhanInfo">
                <ContentTemplate>
                   <h6><strong>Bahasa sehari-hari</strong></h6>
                    <div class="row">
                       <div class="col-sm-12" style="padding-bottom:20px">
                           <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height:23px;width:100%;max-width:680px" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtBahasa" onkeydown="return txtOnKeyPress();"  />
                       </div>
                   </div>
                    <h6 style="margin-bottom:5px"><strong>Perlu Penerjemah</strong></h6>
                    <div class="row" style="padding-top:5px">
                       <div class="col-sm-2">
                           <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="penerjemah1"  Value="0" id="rbPenerjemah1" Checked="true" onclick="HidePdvPenerjemah()" /> Tidak </label>
                       </div>
                   </div>
                    <div class="row">
                       <div class="col-sm-1" style="padding-right:0px;margin-right:0px">
                           <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="penerjemah1"  Value="1" id="rbPenerjemah2" onclick="ShowHidedvPenerjemah()" /> Ya </label>
                       </div>
                        <div class="col-sm-5" style="display:none" id="dvPenerjemah">
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> ,Bahasa</label>
                            <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height:23px;width:100%;max-width:200px" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtPenerjemah" onkeydown="return txtOnKeyPress();" />
                        </div>
                   </div>

                    <h6 style="margin-bottom:5px"><strong>Metode belajar yang disukai</strong></h6>
                    <div class="row" style="padding-top:5px">
                       <div class="col-sm-12">
                           <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height:23px;width:100%;max-width:680px" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtMetodeBelajar" onkeydown="return txtOnKeyPress();" />
                       </div>
                   </div>

                    <h6 style="margin-bottom:5px"><strong>Masalah yang berhubungan dengan proses pembelajaran</strong></h6>
                    <div class="row" style="padding-top:5px">
                       <div class="col-sm-12">
                           <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height:23px;width:100%;max-width:680px" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtProsesBelajar" onkeydown="return txtOnKeyPress();" />
                       </div>
                   </div>

                    <h6 style="margin-bottom:5px"><strong>Kesediaan pasien dan keluarga menerima informasi dan edukasi</strong></h6>
                    <div class="row" style="padding-top:5px">
                       <div class="col-sm-12">
                           <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height:23px;width:100%;max-width:680px" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtEdukasi" onkeydown="return txtOnKeyPress();" />
                       </div>
                   </div>

                    <h6 style="margin-bottom:5px"><strong>Informasi dan edukasi kesehatan yang dibutuhkan</strong></h6>
                    <div class="row" style="padding-top:5px">
                       <div class="col-sm-12">
                           <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height:23px;width:100%;max-width:680px" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtInformasi" onkeydown="return txtOnKeyPress();" />
                       </div>
                   </div>

                </ContentTemplate>
            </asp:UpdatePanel>


        </div>
    </div>
</div>