<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SosialBudaya.ascx.cs" Inherits="Form_General_FirstAssesment_Control_Template_SosialBudaya" %>
<script type="text/javascript">
    function ShowHideinfo() {
        var chkYes = document.getElementById("<%=rbemosi4.ClientID %>");
        var dvPassport = document.getElementById("dvEmosi");
        if (chkYes.checked) {
            dvPassport.style.display = "";
        }
    }

    function Hideinfo() {
        var dvPassport = document.getElementById("dvEmosi");
            dvPassport.style.display = "none";
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
            <label style="background-color:white;color:#000000;font-family:Helvetica, Arial, sans-serif;font-weight:bold;font-size:14px;border-top-left-radius: 7px;border-top-right-radius: 7px;" class="form-control">Psiko - Sosial - Spiritual - Budaya</label>
        </div>
        <div class="modal-body" style="padding-top:0px;padding-bottom:25px">

    <asp:UpdatePanel runat="server" ID="upEmosi">
        <ContentTemplate>
            <h6><strong>Respon emosi</strong></h6>
            <div class="row">
                <div class="col-sm-1" style="padding-right:0px;margin-right:0px">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="emosi1"  Value="0" id="rbemosi1" onclick="Hideinfo()" /> Tenang </label>
                </div>
                <div class="col-sm-1" style="padding-right:0px;margin-right:0px">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="emosi1"  Value="1" id="rbemosi2" onclick="Hideinfo()" /> Marah </label>
                </div>
                <div class="col-sm-1" style="padding-right:0px;margin-right:0px">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="emosi1"  Value="2" id="rbemosi3" onclick="Hideinfo()" /> Sedih </label>
                </div>
                <div class="col-sm-1" style="padding-right:0px;margin-right:0px">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="emosi1"  Value="3" id="rbemosi4" onclick="ShowHideinfo()" /> Lain-lain </label>
                </div>
                <div class="col-sm-7" style="padding-right:0px;margin-right:0px;display:none" id="dvEmosi">
                    <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height:23px;width:100%;max-width:320px" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtinfo" onkeydown="return txtOnKeyPress();" />
                </div>
            </div>

            <h6 style="margin-bottom:5px"><strong>Nilai/ nilai budaya/ kepercayaan (Mis: “Boleh tidaknya pasien melakukan donor darah”)</strong></h6>
            <div class="row" style="padding-top:5px">
                <div class="col-sm-12">
                    <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height:23px;width:100%;max-width:680px" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtNilaiSosial" onkeydown="return txtOnKeyPress();" />
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
        </div>
    </div>
</div>