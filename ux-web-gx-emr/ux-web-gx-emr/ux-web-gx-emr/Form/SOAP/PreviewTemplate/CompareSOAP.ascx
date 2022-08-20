<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CompareSOAP.ascx.cs" Inherits="Form_SOAP_PreviewTemplate_CompareSOAP" %>

<div class="row">
    <div class="col-sm-6" style="padding-right:8px;"> 
        <div style="padding-bottom:5px; font-size: 13px;"><i class="fa fa-bars"></i> <label>Older Data</label></div>
        <div id="divdata_ori" runat="server"></div>
    </div>
    <div class="col-sm-6" style="padding-left:8px;"> 
        <div style="padding-bottom:5px; font-size: 13px;"><i class="fa fa-bars"></i> <label>Newer Data</label></div>
        <div id="divdata_backup" runat="server"></div>
    </div>
</div>