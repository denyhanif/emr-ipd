<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdAssessment.ascx.cs" Inherits="Form_SOAP_Control_Template_StdAssessment" %>

<div class="col-lg-12" style="padding-left:0px;margin:0px;">
    <div style="min-height:120px;background-color:white; width:100%;border-bottom:1px; margin-top:0px;    border-radius: 6px 6px 0px 0px;" class="modal-dialog center-block">
        <div>
            <label class="form-control headerpanel" style="border-radius:6px 6px 0px 0px;">Diagnosis</label> 
        </div>
        <div class="modal-body" style="padding-top:0px;padding-bottom:5px">
            <asp:TextBox runat="server" style="outline-color:transparent;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif" placeholder="Type here..."  BorderColor="transparent" ID="txtPrimary" TextMode="MultiLine" Rows="3" />
        </div>
    </div>
</div>