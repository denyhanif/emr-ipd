<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdPlanning.ascx.cs" Inherits="Form_SOAP_Control_Template_StdPlanning" %>

<%--<%@ Register Src="~/Form/CPOE/Control_Template/ClinicalLab.ascx" TagName="ClinicalLab" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/MicroLab.ascx" TagName="MicroLab" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/CitoLab.ascx" TagName="CitoLab" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/AnatomiLab.ascx" TagName="AnatomiLab" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/MDCLab.ascx" TagName="MDCLab" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/PanelLab.ascx" TagName="PanelLab" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/XrayRad.ascx" TagName="XrayRad" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/USGRad.ascx" TagName="USGRad" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/CTRad.ascx" TagName="CTRad" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/MRIfullRad.ascx" TagName="MRIfullRad" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/MRIhalfRad.ascx" TagName="MRIhalfRad" TagPrefix="uc3" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="~/Form/CPOE/Control_Template/RptClinicalLab.ascx" TagName="RptClinicalLab" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/RptMicroLab.ascx" TagName="RptMicroLab" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/RptCitoLab.ascx" TagName="RptCitoLab" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/RptAnatomiLab.ascx" TagName="RptAnatomiLab" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/RptMDCLab.ascx" TagName="RptMDCLab" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/RptPanelLab.ascx" TagName="RptPanelLab" TagPrefix="uc3" %>

<%@ Register Src="~/Form/CPOE/Control_Template/RptXrayRad.ascx" TagName="RptXrayRad" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/RptUSGRad.ascx" TagName="RptUSGRad" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/RptCTRad.ascx" TagName="RptCTRad" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/RptMRIhalfRad.ascx" TagName="RptMRIhalfRad" TagPrefix="uc3" %>
<%@ Register Src="~/Form/CPOE/Control_Template/RptMRIfullRad.ascx" TagName="RptMRIfullRad" TagPrefix="uc3" %>


<style>
    .ui-autocomplete {
        max-height: 200px;
        overflow-y: auto;
        overflow-x: hidden;
        z-index: 2147483647;
    }

    .box {
        border-radius: 2px;
        background-color: #ffffff;
        border: solid 1px #cdced9;
        height: 23px;
    }

    .hiddencol {
        display: none;
    }

        .mycheckbox input[type="checkbox"] {
            margin-right: 2px;
            vertical-align: top;
        }

    .ic_delete {
        width: 12px;
        height: 12px;
        object-fit: contain;
    }

    .ic_edit {
        width: 14px;
        height: 14px;
        object-fit: contain;
    }

    .stylink {
        font: 12px;
        color: #171717;
    }

        .stylink:hover {
            font: 12px;
            color: #4d9b35;
        }

    .future-order-date-delete {
        position: relative
    }

        .future-order-date-delete a {
            position: absolute;
            right: 3em;
            /* top: 1px;*/
            bottom: 0;
            line-height: 34px;
        }

    .btn-delete-style {
        cursor: pointer;
        color: blue;
    }

        .btn-delete-style:hover {
            color: midnightblue;
        }
</style>

<asp:HiddenField ID="hfPayerType" runat="server" />
<asp:HiddenField ID="hftakedate" runat="server" />
<asp:HiddenField ID="hfverifydate" runat="server" />
<asp:HiddenField ID="hfadditional_take_date" runat="server" />
<asp:HiddenField ID="hfguidadditional" runat="server" />
<asp:HiddenField ID="hfpanellab" runat="server" />

<asp:HiddenField ID="HFisBahasaSOAP_Planning" runat="server" />
<asp:HiddenField ID="HFflagRacikan" runat="server" />
<asp:HiddenField ID="HFmandatoryRacikan" runat="server" />
<asp:HiddenField ID="hfmandatoryFA" runat="server" />

<div class="row" style="transform: translate(0,0);">
    <div class="col-sm-9" style="padding-right: 0px;">

        <%-- ==================================================== Notes ============================================================ --%>
        <%--<asp:UpdatePanel runat="server">
            <ContentTemplate>--%>
                <div class="mini-dialog" style="margin-bottom: 10px">
                    <div style="padding-bottom: 0px; padding-top: 5px;">
                        <div style="padding-left: 15px;">
                            <%--<label id="ENGothersnotes" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px; display: <%=setENG%>;">Doctor Notes to Nurse</label>
                            <label id="INDothersnotes" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px; display: <%=setIND%>;">Pesan Dokter untuk Perawat</label>--%>
                            <label id="lblbhs_othersnotes" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Doctor Notes to Nurse</label>
                            <br />
                            <div class="scrollEMR" style="max-height: 88px; min-height: 40px; overflow-y: auto">
                                <asp:TextBox runat="server" CssClass="text-multiline-dialog" placeholder="Type here..." BorderColor="transparent" ID="txtDocNurseNotes" TextMode="MultiLine" Rows="1" onkeydown="AutoExpand(this)" onblur="minexpand(this)" />
                            </div>
                        </div>
                    </div>
                    <div class="box-border-soap" style="padding-bottom: 0px">
                        <div style="padding-left: 15px;">
                            <%--<label id="ENGothersnotes2" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px; display: <%=setENG%>;">Nurse Notes</label>
                            <label id="INDothersnotes2" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px; display: <%=setIND%>;">Pesan Perawat</label>--%>
                            <label id="lblbhs_othersnotes2" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Nurse Notes</label>
                            <br />
                            <div class="scrollEMR" style="max-height: 88px; overflow-y: auto">
                                <asp:TextBox runat="server" CssClass="text-multiline-dialog" placeholder="..." BorderColor="transparent" ID="txtNurseNotes" ReadOnly="true" TextMode="MultiLine" Rows="1" onkeydown="AutoExpand(this)" onblur="minexpand(this)" />
                            </div>
                        </div>
                    </div>
                </div>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        <%-- ==================================================== end Notes ============================================================ --%>

        <%-- ==================================================== LAB & RAD ============================================================ --%>
        <%--<asp:UpdatePanel runat="server">
            <ContentTemplate>--%>
                <div class="mini-dialog" style="margin-bottom: 10px;" runat="server" id="divTopLABRAD">
                    <div id="divblokLABRAD" runat="server" visible="false">
                        <div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0.3; text-align: center; cursor: not-allowed;"></div>
                    </div>
                    <div class="row">
                        <asp:UpdatePanel ID="UpdatePanelDivLab" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <span class="anchor" id="labrad_begin"></span>
                                <div class="col-sm-6 vertical-right" style="padding-right: 1px; padding-bottom: 5px; margin-right: -1px; z-index: 1;">
                                    <div id="divTopLAB" runat="server">
                                        <div id="divblokLAB" runat="server" visible="false">
                                            <div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0.3; text-align: center; cursor: not-allowed; margin-left:15px;"></div>
                                        </div>

                                        <div class="row mini-header">
                                            <div class="col-sm-4" style="padding-right: 0px;">
                                                <table border="0">
                                                    <tr>
                                                        <td>
                                                            <label id="lblbhs_laboratory" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Laboratory</label>
                                                        </td>
                                                        <td>
                                                            <div class="loadingcpoelab" style="display: none;">
                                                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                                </div>
                                                                &nbsp;
                                                                <img alt="" style="background-color: transparent; height: 15px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                                <asp:HiddenField ID="HFloadingcpoelab" runat="server" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-sm-8 text-right" style="padding-left: 0px;">

                                                <!-- kotak pencarian Autocomplete -->
                                                <asp:UpdatePanel runat="server" ID="UP_SearchLabAC" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="btn-group" style="padding: 0px 6px 0px 6px; margin-top: -3px;">
                                                            <asp:HyperLink ID="HyperLinkSaveAsLab" runat="server" data-toggle="dropdown" aria-expanded="false" NavigateUrl="#" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px; display: none;" onmouseup="klikSaveAsFocus('TextBoxSaveAsLabName');">Save As</asp:HyperLink>
                                                            <ul class="dropdown-menu pop-shadow" style="padding: 8px;">
                                                                <li style="padding-bottom: 3px; font-weight: normal;">Input Order Set Name :
                                                                </li>
                                                                <li style="padding-bottom: 5px; font-weight: normal;">
                                                                    <asp:TextBox ID="TextBoxSaveAsLabName" onkeydown="return txtOnKeyPressFalse();" onkeyup="checkSaveAsEmpty('TextBoxSaveAsLabName','LinkSaveAsLab');" runat="server"></asp:TextBox>
                                                                </li>
                                                                <li style="float: right;">
                                                                    <asp:LinkButton ID="LinkSaveAsLab" class="btn btn-default btn-sm" runat="server" Style="font-weight: bold; background-color: #228b22; color: white; width: 80px; display: none;" OnClick="LinkSaveAsLab_Click" OnClientClick="setflagloading('cpoelab','true');">Save</asp:LinkButton>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <asp:Button runat="server" ID="btnResetLab" Visible="false" BorderStyle="None" Style="background-color: white; color: #c43d32; font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px" Text="Reset" OnClientClick="return konfirmasireset('Laboratory');" OnClick="btnResetLab_Click"></asp:Button>
                                                        <asp:Button runat="server" ID="btnEditLab" Visible="false" BorderStyle="None" Style="background-color: white; color: #9d1fc3; font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px" Text="Edit" OnClientClick="$('#myModal').modal();" OnClick="linklabbutton_Click"></asp:Button>
                                                        <asp:Button runat="server" ID="btnCloseLab" BorderStyle="None" Style="background-color: white; color: #9d1fc3; font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px" Text="Edit" CssClass="hidden" OnClick="btnCloseLab_Click"></asp:Button>
                                                        <asp:HiddenField ID="HF_lab_open" runat="server" />
                                                        <div style="display: inline;">
                                                            <div class="has-feedback" style="display: inline;">
                                                                <asp:TextBox ID="txtItemCPOE_LAB" runat="server" Placeholder="Add item here..." Style="width: 130px; font-weight: normal; font-size: 12px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('cpoelab','true');"></asp:TextBox>
                                                                <span class="fa fa-caret-down form-control-feedback" style="margin-top: -9px; z-index: 0;"></span>
                                                            </div>
                                                            <asp:HiddenField ID="HF_ItemSelectedcpoelab" runat="server" />
                                                            <asp:Button ID="ButtonAjaxSearchCPOE_LAB" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchCPOE_LAB_Click" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <!-- end kotak pencarian -->
                                            </div>
                                        </div>
                                        <div style="overflow-y: auto; max-height: 250px;" class="scrollEMR">
                                            <asp:Label runat="server" ID="labempty" Style="padding-left: 15px;">
                                                <span>
                                                    <img src="<%= Page.ResolveClientUrl("~/Images/P/ic_LabEmpty.svg") %>" />
                                                </span>
                                                <label id="lblbhs_nolabadded" style="font-size: 12px; font-weight: bold; color: #bdbfd8; margin-right: 5px;">No Lab Added</label>
                                                <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="linklabbutton" OnClientClick="$('#myModal').modal();" OnClick="linklabbutton_Click">
                                                    <label id="lblbhs_addlabhere" style="cursor:pointer;">Add Lab Here</label> 
                                                </asp:LinkButton>

                                            </asp:Label>
                                            <ul style="padding-left: 15px">
                                                <%--<asp:Repeater runat="server" ID="Repeater1">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("name") %>' Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>--%>
                                                <asp:GridView ID="Repeater1" runat="server" AutoGenerateColumns="false" CssClass="table-kecil" ShowHeader="false"
                                                    DataKeyNames="name" BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="titik" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="3%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <i class="fa fa-circle" style="font-size: 5px; vertical-align: middle; margin-right: 2px;"></i>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hf_id_lab" runat="server" Value='<%# Bind("id") %>'></asp:HiddenField>
                                                                <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="labname" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <%--<asp:Button Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="x" OnClick="btnDeleteAllergy_onClick"></asp:Button>--%>
                                                                <asp:ImageButton ID="btndeletelab" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btndeletelab_Click" Style="width: 12px; height: 12px; margin-top: 3px;" Visible='<%# Eval("IsSendHope").ToString() == "0" ? true : false %>' />
                                                                <i class="fa fa-info-circle" style='font-size: 14px; color: cornflowerblue; margin-top: 3px; display:<%# Eval("IsSendHope").ToString() == "0" ? "none" : "block" %>' title="Already send to hope"></i>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanelDivRad" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-sm-6 vertical-left" style="padding-left: 0px; padding-bottom: 5px;">
                                    <div id="divTopRAD" runat="server">
                                        <div id="divblokRAD" runat="server" visible="false">
                                            <div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0.3; text-align: center; cursor: not-allowed; margin-left:15px;"></div>
                                        </div>
                                        <div class="row mini-header">
                                            <div class="col-sm-4">
                                                <table border="0">
                                                    <tr>
                                                        <td>
                                                            <label id="lblbhs_radiology" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Radiology</label>
                                                        </td>
                                                        <td>
                                                            <div class="loadingcpoerad" style="display: none;">
                                                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                                </div>
                                                                &nbsp;
                                                                <img alt="" style="background-color: transparent; height: 15px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                                <asp:HiddenField ID="HFloadingcpoerad" runat="server" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-sm-8 text-right" style="padding-left: 0px;">

                                                <!-- kotak pencarian Autocomplete -->
                                                <asp:UpdatePanel runat="server" ID="UP_SearchRadAC" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div style="display: inline;" runat="server" id="divcitorad" visible="false">
                                                            <div class="pretty p-icon p-curve" style="margin-right: 0px;">
                                                                <asp:CheckBox runat="server" ID="chkcitorad" />
                                                                <div class="state p-success">
                                                                    <i class="icon fa fa-check"></i>
                                                                    <label style="vertical-align: text-top;">CITO</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Button runat="server" ID="btnResetRad" Visible="false" BorderStyle="None" Style="background-color: white; color: #c43d32; font-family: Helvetica, Arial, sans-serif; text-decoration: underline; font-weight: bold; font-size: 12px; height: 20px" Text="Reset" OnClientClick="return konfirmasireset('Radiology');" OnClick="btnResetRad_Click"></asp:Button>
                                                        <asp:Button runat="server" ID="btnEditRad" Visible="false" BorderStyle="None" Style="background-color: white; color: #9d1fc3; font-family: Helvetica, Arial, sans-serif; text-decoration: underline; font-weight: bold; font-size: 12px; height: 20px" Text="Edit" OnClientClick="$('#modalRad').modal();" OnClick="linkradbutton_Click"></asp:Button>
                                                        <%--<asp:Label ID="btnEditRad" runat="server" Visible="false" Text="Edit" style="cursor:pointer; font-size: 12px; font-weight: bold; color: #9d1fc3; margin-left: 5px; text-decoration: underline; padding-right:6px;" data-toggle="dropdown"></asp:Label>
                                                        <ul class="dropdown-menu bdrop" style="top: auto; left: auto; position: fixed; min-width: 65px; margin-left: 115px;">
                                                        <li>
                                                            <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButton1" OnClientClick="LoadTabRadData('All');" OnClick="ButtonChooseTabRad_Click">
                                                               All    
                                                            </asp:LinkButton>
                                                        </li>
                                                        <li>
                                                            <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButton2" OnClientClick="LoadTabRadData('Xray');" OnClick="ButtonChooseTabRad_Click">
                                                               Xray    
                                                            </asp:LinkButton>
                                                        </li>
                                                        <li>
                                                            <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButton3" OnClientClick="LoadTabRadData('USG');" OnClick="ButtonChooseTabRad_Click">
                                                               USG    
                                                            </asp:LinkButton>
                                                        </li>
                                                        <li>
                                                            <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButton4" OnClientClick="LoadTabRadData('CT');" OnClick="ButtonChooseTabRad_Click">
                                                               CT    
                                                            </asp:LinkButton>
                                                        </li>
                                                        <li>
                                                            <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButton5" OnClientClick="LoadTabRadData('MRI1');" OnClick="ButtonChooseTabRad_Click">
                                                               MRI 1,5    
                                                            </asp:LinkButton>
                                                        </li>
                                                        <li>
                                                            <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButton6" OnClientClick="LoadTabRadData('MRI3');" OnClick="ButtonChooseTabRad_Click">
                                                               MRI 3    
                                                            </asp:LinkButton>
                                                        </li>
                                                        </ul>--%>
                                                        <asp:Button runat="server" ID="btnCloseRad" BorderStyle="None" Style="background-color: white; color: #9d1fc3; font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px" Text="Edit" CssClass="hidden" OnClick="btnCloseRad_Click"></asp:Button>
                                                        <asp:HiddenField ID="HF_rad_open" runat="server" />
                                                        <div style="display: inline;">
                                                            <div class="has-feedback" style="display: inline;">
                                                                <asp:TextBox ID="txtItemCPOE_RAD" runat="server" Placeholder="Add item here..." Style="width: 130px; font-weight: normal; font-size: 12px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('cpoerad','true');"></asp:TextBox>
                                                                <span class="fa fa-caret-down form-control-feedback" style="margin-top: -9px; z-index: 0;"></span>
                                                            </div>
                                                            <asp:HiddenField ID="HF_ItemSelectedcpoerad" runat="server" />
                                                            <asp:HiddenField ID="HF_ItemSelectedcpoerad_name" runat="server" />
                                                            <asp:HiddenField ID="HF_ItemSelectedcpoerad_remarks" runat="server" />
                                                            <asp:Button ID="ButtonAjaxSearchCPOE_RAD" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchCPOE_RAD_Click" />

                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <!-- end kotak pencarian -->
                                            </div>
                                        </div>
                                        <div style="overflow-y: auto; max-height: 250px; margin-left: -9px;" class="scrollEMR">
                                            <asp:Label runat="server" ID="radempty" Style="padding-left: 25px;">
                                                <span>
                                                    <img src="<%= Page.ResolveClientUrl("~/Images/P/ic_RadEmpty.svg") %>" />
                                                </span>
                                                <label id="lblbhs_noradiologyadded" style="font-size: 12px; font-weight: bold; color: #bdbfd8; margin-left: 5px;">No radiology added </label>
                                                <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3; margin-left: 5px;" ID="linkradbutton" OnClientClick="$('#modalRad').modal();" OnClick="linkradbutton_Click">
                                                    <label id="lblbhs_addradiologyhere" style="cursor:pointer;">Add Radiology Here</label>
                                                </asp:LinkButton>

                                                <%--<label id="lblbhs_addlabherenew" style="cursor:pointer; font-size: 12px; font-weight: bold; color: #9d1fc3; margin-left: 5px;" data-toggle="dropdown">Add Radiology Here</label>
                                                <ul class="dropdown-menu bdrop" style="top: auto; left: auto; position: fixed; min-width: 65px; margin-left: 200px; margin-top: -10px;">
                                                    <li>
                                                        <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButtonAll" OnClientClick="LoadTabRadData('All');" OnClick="ButtonChooseTabRad_Click">
                                                           All    
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButtonXray" OnClientClick="LoadTabRadData('Xray');" OnClick="ButtonChooseTabRad_Click">
                                                           Xray    
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButtonUSG" OnClientClick="LoadTabRadData('USG');" OnClick="ButtonChooseTabRad_Click">
                                                           USG    
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButtonCT" OnClientClick="LoadTabRadData('CT');" OnClick="ButtonChooseTabRad_Click">
                                                           CT    
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButtonMRI1" OnClientClick="LoadTabRadData('MRI1');" OnClick="ButtonChooseTabRad_Click">
                                                           MRI 1,5    
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButtonMRI3" OnClientClick="LoadTabRadData('MRI3');" OnClick="ButtonChooseTabRad_Click">
                                                           MRI 3    
                                                        </asp:LinkButton>
                                                    </li>
                                                </ul>--%>
                                            </asp:Label>
                                            <ul style="padding-left: 25px">
                                                <%-- <asp:Repeater runat="server" ID="rptRadiology">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("name") %>' Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>--%>
                                                <asp:GridView ID="rptRadiology" runat="server" AutoGenerateColumns="false" CssClass="table-kecil" ShowHeader="false"
                                                    DataKeyNames="name" BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="titik" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="3%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <i class="fa fa-circle" style="font-size: 5px; vertical-align: middle; margin-right: 2px;"></i>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hf_id_rad" runat="server" Value='<%# Bind("id") %>'></asp:HiddenField>
                                                                <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="radname" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <%--<asp:Button Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="x" OnClick="btnDeleteAllergy_onClick"></asp:Button>--%>
                                                                <asp:ImageButton ID="btndeleterad" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btndeleterad_Click" Style="width: 12px; height: 12px; margin-top: 3px;" Visible='<%# Eval("IsSendHope").ToString() == "0" ? true : false %>' />
                                                                <i class="fa fa-info-circle" style='font-size: 14px; color: cornflowerblue; margin-top: 3px; display:<%# Eval("IsSendHope").ToString() == "0" ? "none" : "block" %>' title="Already send to hope"></i>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
					<div class="box-border-soap" style="padding-bottom: 0px; padding-top: 0px">
						<div class="row">
							<div class="col-sm-6 vertical-right" style="padding-left: 30px; padding-top: 5px; margin-right: -1px;">
								<label id="lblbhs_othersLab" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Others Lab</label>
								(Misal: Order diagnostik atau order lab / radiologi yang tidak ada dalam form)
                            <br />
								<div class="scrollEMR" style="max-height: 88px; overflow-y: auto">
									<asp:TextBox runat="server" CssClass="text-multiline-dialog" placeholder="Type here..." BorderColor="transparent" ID="txtplanningotherLab" TextMode="MultiLine" Rows="1" onkeyup="isMandatoryField();" onkeydown="AutoExpand(this); checkTextAreaMaxLength(this,event,'100');" onblur="minexpand(this)" />
								</div>
							</div>
							<div class="col-sm-6 vertical-left" style="padding-left: 15px; padding-top: 5px">
								<label id="lblbhs_othersRad" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Others Rad </label>
								(Misal: Order diagnostik atau order lab / radiologi yang tidak ada dalam form)
                            <br />
								<div class="scrollEMR" style="max-height: 88px; overflow-y: auto">
									<asp:TextBox runat="server" CssClass="text-multiline-dialog" placeholder="Type here..." BorderColor="transparent" ID="txtplanningotherRad" TextMode="MultiLine" Rows="1" onkeyup="isMandatoryField();" onkeydown="AutoExpand(this); checkTextAreaMaxLength(this,event,'100');" onblur="minexpand(this)" />
								</div>
							</div>
						</div>
					</div>
                    <div class="box-border-soap" style="padding-bottom: 0px">
                        <div style="padding-left: 15px;">
                            <label id="lblbhs_clinicaldiag" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Clinical Diagnosis</label>
                            <asp:Label ID="LabelmandatoryCD" Style="color: black;" runat="server" Text=""></asp:Label><asp:HiddenField ID="HFmandatoryCD" runat="server" />
                            <br />
                            <div class="scrollEMR" style="max-height: 88px; overflow-y: auto">
                                <asp:TextBox runat="server" CssClass="text-multiline-dialog copydata" placeholder="Type here..." BorderColor="transparent" ID="txtclinicaldiagnosis" TextMode="MultiLine" Rows="1" onkeydown="AutoExpand(this); return inputLimit(this,'400');" onkeyup="copytexttoINV();" onfocus="minexpand(this)" />
                            </div>
                        </div>
                    </div>

                  <!-- Future Order -->
					<div id="divBtnFutureOrder" runat="server" style="display:flex; margin-top:0px; margin-bottom:-15px">
						<a href="#" onclick="btnToggleFutureOrder()" id="LB_ShowFutureOrder" class="btn btn-default btn-sm" style="border-radius:6px; color: white; background-color: #228b22; height: auto; padding-top: 3px; margin:0 auto"> <span id="logo_FutureOrder"> <i class="fa fa-plus-circle"></i> </span> <label id="lblbhs_showFutureOrder" style="cursor:pointer;">Future Order</label> </a>
					</div>
					<div runat="server" id="divFutureOrder" class="box-border-soap" style="display: none; padding-top: 13px; padding-bottom: 0px; background-color:#F2F3F4">
						<div class="row">
							<asp:UpdatePanel ID="UpdatePanelDivLab_FutureOrder" runat="server" UpdateMode="Conditional">
								<ContentTemplate>
									<span class="anchor" id="labrad_begin_FutureOrder"></span>
									<div class="col-sm-6" style="padding-right: 1px; padding-bottom: 5px; margin-right: -1px; z-index: 1; border-right:solid 1px #cbcdcf">
										<div id="divTopLAB_FutureOrder" runat="server">
											<div id="divblokLAB_FutureOrder" runat="server" visible="false">
												<div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0.3; text-align: center; cursor: not-allowed; margin-left:15px;"></div>
											</div>

											<div class="row mini-header">
												<div class="col-sm-4" style="padding-right: 0px;">
													<table border="0">
														<tr>
															<td>
																<label id="lblbhs_laboratory_FutureOrder" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Future Laboratory</label>
															</td>
															<td>
																<div class="loadingcpoelab_FutureOrder" style="display: none;">
																	<div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
																	</div>
																	&nbsp;
																	<img alt="" style="background-color: transparent; height: 15px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
																	<asp:HiddenField ID="HFloadingcpoelab_FutureOrder" runat="server" />
																</div>
															</td>
														</tr>
													</table>
												</div>
												<div class="col-sm-8 text-right" style="padding-left: 0px;">

													<!-- kotak pencarian Autocomplete -->
													<asp:UpdatePanel runat="server" ID="UP_SearchLabAC_FutureOrder" UpdateMode="Conditional">
														<ContentTemplate>
															<div class="btn-group" style="padding: 0px 6px 0px 6px; margin-top: -3px;">
																<asp:HyperLink ID="HyperLinkSaveAsLab_FutureOrder" runat="server" data-toggle="dropdown" aria-expanded="false" NavigateUrl="#" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px; display: none;" onmouseup="klikSaveAsFocus('TextBoxSaveAsLabName');">Save As</asp:HyperLink>
																<ul class="dropdown-menu pop-shadow" style="padding: 8px;">
																	<li style="padding-bottom: 3px; font-weight: normal;">Input Order Set Name :
																	</li>
																	<li style="padding-bottom: 5px; font-weight: normal;">
																		<asp:TextBox ID="TextBoxSaveAsLabName_FutureOrder" onkeydown="return txtOnKeyPressFalse();" onkeyup="checkSaveAsEmpty('TextBoxSaveAsLabName_FutureOrder','LinkSaveAsLab_FutureOrder');" runat="server"></asp:TextBox>
																	</li>
																	<li style="float: right;">
																		<asp:LinkButton ID="LinkSaveAsLab_FutureOrder" class="btn btn-default btn-sm" runat="server" Style="font-weight: bold; background-color: #228b22; color: white; width: 80px; display: none;" OnClick="LinkSaveAsLab_Click" OnClientClick="setflagloading('cpoelab_FutureOrder','true');">Save</asp:LinkButton>
																	</li>
																</ul>
															</div>
															<asp:Button runat="server" ID="btnResetLab_FutureOrder" Visible="false" BorderStyle="None" Style="background-color: #F2F3F4; color: #c43d32; font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px" Text="Reset" OnClientClick="return konfirmasireset('Laboratory');" OnClick="btnResetLab_Click"></asp:Button>
															<asp:Button runat="server" ID="btnEditLab_FutureOrder" Visible="false" BorderStyle="None" Style="background-color: #F2F3F4; color: #9d1fc3; font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px" Text="Edit" OnClientClick="$('#myModal').modal();" OnClick="linklabbutton_FutureOrder_Click"></asp:Button>
															<asp:Button runat="server" ID="btnCloseLab_FutureOrder" BorderStyle="None" Style="background-color: white; color: #9d1fc3; font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px" Text="Edit" CssClass="hidden" OnClick="btnCloseLab_Click"></asp:Button>
															<asp:HiddenField ID="HF_lab_open_FutureOrder" runat="server" />
															<div style="display: inline;">
																<div class="has-feedback" style="display: inline;">
																	<asp:TextBox ID="txtItemCPOE_LAB_FutureOrder" runat="server" Placeholder="Add item here..." Style="width: 130px; font-weight: normal; font-size: 12px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('cpoelab_FutureOrder','true');"></asp:TextBox>
																	<span class="fa fa-caret-down form-control-feedback" style="margin-top: -9px; z-index: 0;"></span>
																</div>
																<asp:HiddenField ID="HF_ItemSelectedcpoelab_FutureOrder" runat="server" />
																<asp:Button ID="ButtonAjaxSearchCPOE_LAB_FutureOrder" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchCPOE_LAB_Click" />
															</div>
														</ContentTemplate>
													</asp:UpdatePanel>
													<!-- end kotak pencarian -->
												</div>
											</div>
											<div style="margin-left: 13px; margin-top: 7.5px; margin-bottom: 13.1px;">
												<div class="future-order-date-delete">
													<asp:TextBox class="form-control" ID="dp_labFutureOrder" runat="server" CssClass="isCalendar" Style="width: 100%; height: 35px;" placeholder="choose an estimated date..." onmousedown="dateSelectLabFO();" />
													<a id="dp_labFutureOrderDelete" runat="server" title="Delete" class="btn-delete-style" style="display:none" onclick="deleteFutureOrderDate(this,'futureOrderLab');">
														<i class="fa fa-times-circle"></i>
													</a>
												</div>
											</div>
											<div style="overflow-y: auto; max-height: 250px;" class="scrollEMR">
												<asp:Label runat="server" ID="labempty_FutureOrder" Style="padding-left: 15px;">
													<span>
														<img src="<%= Page.ResolveClientUrl("~/Images/P/ic_LabEmpty.svg") %>" />
													</span>
													<label id="lblbhs_nolabadded_FutureOrder" style="font-size: 12px; font-weight: bold; color: #bdbfd8; margin-right: 5px;">No Lab Added</label>
													<asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="linklabbutton_FutureOrder" OnClientClick="$('#myModal').modal();" OnClick="linklabbutton_FutureOrder_Click">
														<label id="lblbhs_addlabhere_FutureOrder" style="cursor:pointer;">Add Lab Here</label> 
													</asp:LinkButton>

												</asp:Label>
												<ul style="padding-left: 15px">
													<asp:GridView ID="Repeater1_FutureOrder" runat="server" AutoGenerateColumns="false" CssClass="table-kecil" ShowHeader="false"
														DataKeyNames="name" BorderWidth="0" HeaderStyle-BorderWidth="0">
														<Columns>
															<asp:TemplateField HeaderText="titik" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="3%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																<ItemTemplate>
																	<i class="fa fa-circle" style="font-size: 5px; vertical-align: middle; margin-right: 2px;"></i>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Nama" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																<ItemTemplate>
																	<asp:HiddenField ID="hf_id_lab_FutureOrder" runat="server" Value='<%# Bind("id") %>'></asp:HiddenField>
																	<asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="labname_FutureOrder" runat="server" Text='<%# Bind("name") %>'></asp:Label>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																<ItemTemplate>
																	<asp:ImageButton ID="btndeletelab_FutureOrder" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btndeletelab_Click" Style="width: 12px; height: 12px; margin-top: 3px;" Visible='<%# Eval("IsSendHope").ToString() == "0" ? true : false %>' />
																	<i class="fa fa-info-circle" style='font-size: 14px; color: cornflowerblue; margin-top: 3px; display:<%# Eval("IsSendHope").ToString() == "0" ? "none" : "block" %>' title="Already send to hope"></i>
																</ItemTemplate>
															</asp:TemplateField>
														</Columns>
													</asp:GridView>
												</ul>
											</div>
										</div>
									</div>
								</ContentTemplate>
							</asp:UpdatePanel>

                             <asp:UpdatePanel ID="UpdatePanelDivRad_FutureOrder" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-sm-6 vertical-left" style="padding-left: 0px; padding-bottom: 5px;">
                                    <div id="divTopRAD_FutureOrder" runat="server">
                                        <div id="divblokRAD_FutureOrder" runat="server" visible="false">
                                            <div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0.3; text-align: center; cursor: not-allowed; margin-left:15px;"></div>
                                        </div>
                                        <div class="row mini-header">
                                            <div class="col-sm-4">
                                                <table border="0">
                                                    <tr>
                                                        <td>
                                                            <label id="lblbhs_radiology_FutureOrder" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Future Radiology</label>
                                                        </td>
                                                        <td>
                                                            <div class="loadingcpoerad_FutureOrder" style="display: none;">
                                                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                                </div>
                                                                &nbsp;
                                                                <img alt="" style="background-color: transparent; height: 15px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                                <asp:HiddenField ID="HFloadingcpoerad_FutureOrder" runat="server" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-sm-8 text-right" style="padding-left: 0px;">

                                                <!-- kotak pencarian Autocomplete -->
                                                <asp:UpdatePanel runat="server" ID="UP_SearchRadAC_FutureOrder" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div style="display: inline;" runat="server" id="divcitorad_FutureOrder" visible="false">
                                                            <div class="pretty p-icon p-curve" style="margin-right: 0px;">
                                                                <asp:CheckBox runat="server" ID="chkcitorad_FutureOrder" />
                                                                <div class="state p-success">
                                                                    <i class="icon fa fa-check"></i>
                                                                    <label style="vertical-align: text-top;">CITO</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Button runat="server" ID="btnResetRad_FutureOrder" Visible="false" BorderStyle="None" Style="background-color: #F2F3F4; color: #c43d32; font-family: Helvetica, Arial, sans-serif; text-decoration: underline; font-weight: bold; font-size: 12px; height: 20px" Text="Reset" OnClientClick="return konfirmasireset('Radiology');" OnClick="btnResetRad_Click"></asp:Button>
                                                        <asp:Button runat="server" ID="btnEditRad_FutureOrder" Visible="false" BorderStyle="None" Style="background-color: #F2F3F4; color: #9d1fc3; font-family: Helvetica, Arial, sans-serif; text-decoration: underline; font-weight: bold; font-size: 12px; height: 20px" Text="Edit" OnClientClick="$('#modalRad').modal();" OnClick="linkradbutton_FutureOrder_Click"></asp:Button>
                                                        <%--<asp:Label ID="btnEditRad_FutureOrder" runat="server" Visible="false" Text="Edit" style="cursor:pointer; font-size: 12px; font-weight: bold; color: #9d1fc3; margin-left: 5px; text-decoration: underline; padding-right:6px;" data-toggle="dropdown"></asp:Label>
                                                        <ul class="dropdown-menu bdrop" style="top: auto; left: auto; position: fixed; min-width: 65px; margin-left: 115px;">
                                                        <li>
                                                            <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButton1" OnClientClick="LoadTabRadData('All');" OnClick="ButtonChooseTabRad_Click">
                                                               All    
                                                            </asp:LinkButton>
                                                        </li>
                                                        <li>
                                                            <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButton2" OnClientClick="LoadTabRadData('Xray');" OnClick="ButtonChooseTabRad_Click">
                                                               Xray    
                                                            </asp:LinkButton>
                                                        </li>
                                                        <li>
                                                            <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButton3" OnClientClick="LoadTabRadData('USG');" OnClick="ButtonChooseTabRad_Click">
                                                               USG    
                                                            </asp:LinkButton>
                                                        </li>
                                                        <li>
                                                            <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButton4" OnClientClick="LoadTabRadData('CT');" OnClick="ButtonChooseTabRad_Click">
                                                               CT    
                                                            </asp:LinkButton>
                                                        </li>
                                                        <li>
                                                            <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButton5" OnClientClick="LoadTabRadData('MRI1');" OnClick="ButtonChooseTabRad_Click">
                                                               MRI 1,5    
                                                            </asp:LinkButton>
                                                        </li>
                                                        <li>
                                                            <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButton6" OnClientClick="LoadTabRadData('MRI3');" OnClick="ButtonChooseTabRad_Click">
                                                               MRI 3    
                                                            </asp:LinkButton>
                                                        </li>
                                                        </ul>--%>
                                                        <asp:Button runat="server" ID="btnCloseRad_FutureOrder" BorderStyle="None" Style="background-color: white; color: #9d1fc3; font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px" Text="Edit" CssClass="hidden" OnClick="btnCloseRad_Click"></asp:Button>
                                                        <asp:HiddenField ID="HF_rad_open_FutureOrder" runat="server" />
                                                        <div style="display: inline;">
                                                            <div class="has-feedback" style="display: inline;">
                                                                <asp:TextBox ID="txtItemCPOE_RAD_FutureOrder" runat="server" Placeholder="Add item here..." Style="width: 130px; font-weight: normal; font-size: 12px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('cpoerad_FutureOrder','true');"></asp:TextBox>
                                                                <span class="fa fa-caret-down form-control-feedback" style="margin-top: -9px; z-index: 0;"></span>
                                                            </div>
                                                            <asp:HiddenField ID="HF_ItemSelectedcpoerad_FutureOrder" runat="server" />
                                                            <asp:HiddenField ID="HF_ItemSelectedcpoerad_name_FutureOrder" runat="server" />
                                                            <asp:HiddenField ID="HF_ItemSelectedcpoerad_remarks_FutureOrder" runat="server" />
                                                            <asp:Button ID="ButtonAjaxSearchCPOE_RAD_FutureOrder" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchCPOE_RAD_Click" />

                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <!-- end kotak pencarian -->
                                            </div>
                                        </div>
										<div style=" margin-left: 13px; margin-top: 7.5px; margin-bottom: 13.1px;">
											<div class="future-order-date-delete">
												<asp:TextBox class="form-control" ID="dp_radFutureOrder" runat="server" CssClass="isCalendar" Style="width: 100%; height: 35px;" placeholder="choose an estimated date..." onmousedown="dateSelectRadFO();" />
												<a id="dp_radFutureOrderDelete" runat="server" title="Delete" class="btn-delete-style" style="display: none" onclick="deleteFutureOrderDate(this,'futureOrderRad');">
													<i class="fa fa-times-circle"></i>
												</a>
											</div>
										</div>
                                        <div style="overflow-y: auto; max-height: 250px; margin-left: -9px;" class="scrollEMR">
                                            <asp:Label runat="server" ID="radempty_FutureOrder" Style="padding-left: 25px;">
                                                <span>
                                                    <img src="<%= Page.ResolveClientUrl("~/Images/P/ic_RadEmpty.svg") %>" />
                                                </span>
                                                <label id="lblbhs_noradiologyadded_FutureOrder" style="font-size: 12px; font-weight: bold; color: #bdbfd8; margin-left: 5px;">No radiology added </label>
                                                <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3; margin-left: 5px;" ID="linkradbutton_FutureOrder" OnClientClick="$('#modalRad').modal();" OnClick="linkradbutton_FutureOrder_Click">
                                                    <label id="lblbhs_addradiologyhere_FutureOrder" style="cursor:pointer;">Add Radiology Here</label>
                                                </asp:LinkButton>

                                                <%--<label id="lblbhs_addlabherenew" style="cursor:pointer; font-size: 12px; font-weight: bold; color: #9d1fc3; margin-left: 5px;" data-toggle="dropdown">Add Radiology Here</label>
                                                <ul class="dropdown-menu bdrop" style="top: auto; left: auto; position: fixed; min-width: 65px; margin-left: 200px; margin-top: -10px;">
                                                    <li>
                                                        <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButtonAll" OnClientClick="LoadTabRadData('All');" OnClick="ButtonChooseTabRad_Click">
                                                           All    
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButtonXray" OnClientClick="LoadTabRadData('Xray');" OnClick="ButtonChooseTabRad_Click">
                                                           Xray    
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButtonUSG" OnClientClick="LoadTabRadData('USG');" OnClick="ButtonChooseTabRad_Click">
                                                           USG    
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButtonCT" OnClientClick="LoadTabRadData('CT');" OnClick="ButtonChooseTabRad_Click">
                                                           CT    
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButtonMRI1" OnClientClick="LoadTabRadData('MRI1');" OnClick="ButtonChooseTabRad_Click">
                                                           MRI 1,5    
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton runat="server" Style="font-size: 12px; font-weight: bold; color: #9d1fc3" ID="LinkButtonMRI3" OnClientClick="LoadTabRadData('MRI3');" OnClick="ButtonChooseTabRad_Click">
                                                           MRI 3    
                                                        </asp:LinkButton>
                                                    </li>
                                                </ul>--%>
                                            </asp:Label>
                                            <ul style="padding-left: 25px">
                                                <asp:GridView ID="rptRadiology_FutureOrder" runat="server" AutoGenerateColumns="false" CssClass="table-kecil" ShowHeader="false"
                                                    DataKeyNames="name" BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="titik" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="3%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <i class="fa fa-circle" style="font-size: 5px; vertical-align: middle; margin-right: 2px;"></i>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hf_id_rad_FutureOrder" runat="server" Value='<%# Bind("id") %>'></asp:HiddenField>
                                                                <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="radname_FutureOrder" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btndeleterad_FutureOrder" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btndeleterad_Click" Style="width: 12px; height: 12px; margin-top: 3px;" Visible='<%# Eval("IsSendHope").ToString() == "0" ? true : false %>' />
                                                                <i class="fa fa-info-circle" style='font-size: 14px; color: cornflowerblue; margin-top: 3px; display:<%# Eval("IsSendHope").ToString() == "0" ? "none" : "block" %>' title="Already send to hope"></i>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
						</div>
						<div class="box-border-soap" style="padding-bottom: 0px; padding-top: 0px; border-top: solid 1px #cbcdcf;">
							<div class="row">
								<div class="col-sm-6" style="padding-left: 30px; padding-top: 5px; border-right: solid 1px #cbcdcf; margin-right: -1px;">
									<label id="lblbhs_othersLab_FutureOrder" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Others Future Lab</label>
									(Misal: Order diagnostik atau order lab / radiologi yang tidak ada dalam form)
                            <br />
									<div class="scrollEMR" style="max-height: 88px; overflow-y: auto">
										<asp:TextBox BackColor="#F2F3F4" runat="server" CssClass="text-multiline-dialog" placeholder="Type here..." BorderColor="transparent" ID="txtplanningotherLab_FutureOrder" TextMode="MultiLine" Rows="1" onkeyup="isMandatoryField();" onkeydown="AutoExpand(this); checkTextAreaMaxLength(this,event,'100');" onblur="minexpand(this)" />
									</div>
								</div>
								<div class="col-sm-6" style="padding-left: 15px; padding-top: 5px; border-left: solid 1px #cbcdcf;">
									<label id="lblbhs_othersRad_FutureOrder" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Others Future Rad </label>
									(Misal: Order diagnostik atau order lab / radiologi yang tidak ada dalam form)
                            <br />
									<div class="scrollEMR" style="max-height: 88px; overflow-y: auto">
										<asp:TextBox BackColor="#F2F3F4" runat="server" CssClass="text-multiline-dialog" placeholder="Type here..." BorderColor="transparent" ID="txtplanningotherRad_FutureOrder" TextMode="MultiLine" Rows="1" onkeyup="isMandatoryField();" onkeydown="AutoExpand(this); checkTextAreaMaxLength(this,event,'100');" onblur="minexpand(this)" />
									</div>
								</div>
							</div>
						</div>
						<div class="box-border-soap" style="padding-bottom: 0px; border-top: solid 1px #cbcdcf;">
							<div style="padding-left: 15px;">
								<label id="lblbhs_clinicaldiag_FutureOrder" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Clinical Diagnosis</label>
								<asp:Label ID="LabelmandatoryCD_FutureOrder" Style="color: black;" runat="server" Text=""></asp:Label><asp:HiddenField ID="HFmandatoryCD_FutureOrder" runat="server" />
								<br />
								<div class="scrollEMR" style="max-height: 88px; overflow-y: auto">
									<asp:TextBox BackColor="#F2F3F4" runat="server" CssClass="text-multiline-dialog copydata" placeholder="Type here..." BorderColor="transparent" ID="txtclinicaldiagnosis_FutureOrder" TextMode="MultiLine" Rows="1" onkeydown="AutoExpand(this); return inputLimit(this,'400');" onkeyup="copytexttoINV();" onfocus="minexpand(this)" />
								</div>
							</div>
						</div>
					</div>
                    <!-- End Future Order -->
                </div>

            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        <%-- ==================================================== end LAB & RAD ============================================================ --%>









        <%-- ==================================================== DIAGNOSTIC & PROCEDURE ============================================================ --%>
                <div class="mini-dialog" style="margin-bottom: 10px;" runat="server" id="div_DIAPRO">
                    <div id="divBlokDIAPRO" runat="server" visible="false">
                        <div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0.3; text-align: center; cursor: not-allowed;"></div>
                    </div>
                    <div class="row">
                        <!-- Update Panel Div Diagnostic  -->
                       <asp:UpdatePanel ID="UpdatePanelDivDiagnostic" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <span class="anchor" id="diagproc_begin"></span>
                                <div class="col-sm-6 vertical-right" style="padding-right: 1px; padding-bottom: 5px; margin-right: -1px; z-index: 1;">
                                    <div id="divTopDiagnostic" runat="server">
                                        <div id="divBlokDiagnostic" runat="server" visible="false">
                                            <div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0.3; text-align: center; cursor: not-allowed; margin-left:15px;"></div>
                                        </div>
                                        <div class="row mini-header">
                                            <div class="col-sm-4" style="padding-right: 0px;">
                                                <table border="0">
                                                    <tr>
                                                        <td>
                                                            <label id="lblbhs_diagnostic" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Diagnostic</label>
                                                        </td>
                                                        <td>
                                                            <div class="loadingdiagnostic" runat="server" id="loadingdiagnostic" style="display: none;">
                                                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">StdPlanning_Label2
                                                                </div>
                                                                &nbsp;
                                                                <img alt="" style="background-color: transparent; height: 15px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                                <asp:HiddenField ID="HFloadingdiagnostic" runat="server" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-sm-8 text-right" style="padding-left: 0px;">
                                                <!-- kotak pencarian Autocomplete -->
                                                <asp:UpdatePanel runat="server" ID="UpdatePanelSearchBoxDiagnostic" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnResetDiagnostic" Visible="false" BorderStyle="None" Style="background-color: white; color: #c43d32; font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px" Text="Reset" OnClientClick="return konfirmasireset('Diagnostic');" OnClick="btnResetDiagProc_Click"></asp:Button>
                                                        <div style="display: inline;">
                                                            <div class="has-feedback" style="display: inline;">
                                                                <asp:TextBox ID="txtItem_DIAG" runat="server" Placeholder="Add item here..." Style="width: 130px; font-weight: normal; font-size: 12px;" class="autosuggest" onkeydown="return txtOnKeyPressFalseICD();" onfocus="setflagloading('diagnostic','true');"></asp:TextBox>
                                                                <span class="fa fa-caret-down form-control-feedback" style="margin-top: -9px; z-index: 0;"></span>
                                                            </div>
                                                            <asp:HiddenField ID="HF_ItemSelectedDiagnosticNonModal" runat="server" />
                                                            <asp:Button ID="ButtonAjaxSearchDiagnosticNonModal" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchDiagProcNonModal_Click" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <!-- end kotak pencarian -->
                                            </div>
                                        </div>
                                        <div style="overflow-y: auto; max-height: 250px;" class="scrollEMR">
                                            <asp:Label runat="server" ID="labempty_Diagnostic" Style="padding-left: 15px;">
                                                <span>
                                                    <img src="<%= Page.ResolveClientUrl("~/Images/Dashboard/ic_LatestHistory_new.svg") %>" />
                                                </span>
                                                <label id="lblbhs_nolabadded_procedure" style="font-size: 12px; font-weight: bold; color: #bdbfd8; margin-right: 5px;">Tidak ada order </label>
                                            </asp:Label>
                                            <ul style="padding-left: 15px">
                                                <asp:GridView ID="GridView_DiagnosticList" runat="server" AutoGenerateColumns="false" CssClass="table-kecil" ShowHeader="false"
                                                    DataKeyNames="ProcedureItemId" BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="titik" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="3%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <i class="fa fa-circle" style="font-size: 5px; vertical-align: middle; margin-right: 2px;"></i>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hf_id_submitdiagnostic" runat="server" Value='<%# Bind("ProcedureItemId") %>'></asp:HiddenField>
                                                                <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="lblItemDiagnosticName" runat="server" Text='<%# Bind("ProcedureItemName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btndeletediag" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btndeletediagproc_Click" Style="width: 12px; height: 12px; margin-top: 3px;" Visible='<%# Eval("IsSendHope").ToString() == "0" ? true : false %>' />
                                                                <i class="fa fa-info-circle" style='font-size: 14px; color: cornflowerblue; margin-top: 3px; display:<%# Eval("IsSendHope").ToString() == "0" ? "none" : "block" %>' title="Already send to hope"></i>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!-- Update Panel Div Procedure  -->
                       <asp:UpdatePanel ID="UpdatePanelDivProcedure" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-sm-6 vertical-right" style="padding-left: 0px; padding-bottom: 5px;border-left:solid 1px #eaecef">
                                    <div id="div1" runat="server">
                                        <div id="divTopProcedure" runat="server" visible="false">
                                            <div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0.3; text-align: center; cursor: not-allowed; margin-left:15px;"></div>
                                        </div>
                                        <div class="row mini-header">
                                            <div class="col-sm-4" style="padding-right: 0px;">
                                                <table border="0">
                                                    <tr>
                                                        <td>
                                                            <label id="lblbhs_procedure" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Procedure</label>
                                                        </td>
                                                        <td>
                                                            <div class="loadingprocedure" runat="server" id="loadingprocedure" style="display: none;">
                                                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">StdPlanning_Label2
                                                                </div>
                                                                &nbsp;
                                                                <img alt="" style="background-color: transparent; height: 15px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                                <asp:HiddenField ID="HFloadingprocedure" runat="server" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-sm-8 text-right" style="padding-left: 0px;">
                                                <!-- kotak pencarian Autocomplete -->
                                                <asp:UpdatePanel runat="server" ID="UpdatePanelSearchBoxProcedure" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnResetProcedure" Visible="false" BorderStyle="None" Style="background-color: white; color: #c43d32; font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px" Text="Reset" OnClientClick="return konfirmasireset('Procedure');" OnClick="btnResetDiagProc_Click"></asp:Button>
                                                        <div style="display: inline;">
                                                            <div class="has-feedback" style="display: inline;">
                                                                <asp:TextBox ID="txtItem_PROC" runat="server" Placeholder="Add item here..." Style="width: 130px; font-weight: normal; font-size: 12px;" class="autosuggest" onkeydown="return txtOnKeyPressFalseICD();" onfocus="setflagloading('procedure','true');"></asp:TextBox>
                                                                <span class="fa fa-caret-down form-control-feedback" style="margin-top: -9px; z-index: 0;"></span>
                                                            </div>
                                                            <asp:HiddenField ID="HF_ItemSelectedProcedureNonModal" runat="server" />
                                                            <asp:Button ID="ButtonAjaxSearchProcedureNonModal" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchDiagProcNonModal_Click" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <!-- end kotak pencarian -->
                                            </div>
                                        </div>
                                        <div style="overflow-y: auto; max-height: 250px;" class="scrollEMR">
                                            <asp:Label runat="server" ID="labempty_Procedure" Style="padding-left: 15px;">
                                                <span>
                                                    <img src="<%= Page.ResolveClientUrl("~/Images/Dashboard/ic_LatestHistory_new.svg") %>" />
                                                </span>
                                                <label id="lblbhs_nolabadded_diagnostic" style="font-size: 12px; font-weight: bold; color: #bdbfd8; margin-right: 5px;">Tidak ada order </label>
                                            </asp:Label>
                                            <ul style="padding-left: 15px">
                                                <asp:GridView ID="GridView_ProcedureList" runat="server" AutoGenerateColumns="false" CssClass="table-kecil" ShowHeader="false"
                                                    DataKeyNames="ProcedureItemId" BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="titik" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="3%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <i class="fa fa-circle" style="font-size: 5px; vertical-align: middle; margin-right: 2px;"></i>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nama" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hf_id_submitprocedure" runat="server" Value='<%# Bind("ProcedureItemId") %>'></asp:HiddenField>
                                                                <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="lblItemProcedureName" runat="server" Text='<%# Bind("ProcedureItemName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btndeleteproc" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btndeletediagproc_Click" Style="width: 12px; height: 12px; margin-top: 3px;" Visible='<%# Eval("IsSendHope").ToString() == "0" ? true : false %>' />
                                                                <i class="fa fa-info-circle" style='font-size: 14px; color: cornflowerblue; margin-top: 3px; display:<%# Eval("IsSendHope").ToString() == "0" ? "none" : "block" %>' title="Already send to hope"></i>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="box-border-soap" style="padding-bottom: 0px; padding-top: 0px">
                    <div class="row">
                            <div class="col-sm-6 vertical-right" style="padding-left: 30px; padding-top: 5px; margin-right: -1px;">
                                <label id="lblbhs_othersDiagnostic" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Others Diagnostic</label>
                                (Misal: Diagnostik yang tidak ada didalam form)
                            <br />
                                <div class="scrollEMR" style="max-height: 88px; overflow-y: auto">
                                    <asp:TextBox runat="server" CssClass="text-multiline-dialog" placeholder="Type here..." BorderColor="transparent" ID="txtPlanningOtherDiagnostic" TextMode="MultiLine" Rows="1" onkeyup="isMandatoryField();" onkeydown="AutoExpand(this); checkTextAreaMaxLength(this,event,'100');" onblur="minexpand(this)" />
                                </div>
                            </div>
                            <div class="col-sm-6 vertical-left" style="padding-left: 15px; padding-top: 5px">
                                <label id="lblbhs_othersProcedure" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Others Procedure</label>
                                (Misal: Prosedur yang tidak ada didalam form)
                            <br />
                                <div class="scrollEMR" style="max-height: 88px; overflow-y: auto">
                                    <asp:TextBox runat="server" CssClass="text-multiline-dialog" placeholder="Type here..." BorderColor="transparent" ID="txtPlanningOtherProcedure" TextMode="MultiLine" Rows="1" onkeyup="isMandatoryField();" onkeydown="AutoExpand(this); checkTextAreaMaxLength(this,event,'100');" onblur="minexpand(this)" />
                                </div>
                            </div>
                        </div>
                    </div>
					<div id="divBtnFutureOrderDiAGPROC" runat="server" style="display:flex; margin-top:0px; margin-bottom:-15px">
						<a href="#" onclick="btnToggleFutureOrderDiagProc()" id="LB_ShowFutureOrderDiagProc" class="btn btn-default btn-sm" style="border-radius:6px; color: white; background-color: #228b22; height: auto; padding-top: 3px; margin:0 auto"> <span id="logo_FutureOrderDIAGPROC"> <i class="fa fa-plus-circle"></i> </span> <label id="lblbhs_showFutureOrderDIAPRO" style="cursor:pointer;">Future Order</label> </a>
					</div>
                    <div runat="server" id="divFutureOrderDiagProc" class="box-border-soap" style="padding-top: 13px; padding-bottom: 0px; background-color:#F2F3F4;display:none;">
						<div class="row">
							<asp:UpdatePanel ID="UpdatePanelDivDiagnosticFutureOrder" runat="server" UpdateMode="Conditional">
								<ContentTemplate>
									<div class="col-sm-6" style="padding-right: 1px; padding-bottom: 5px; margin-right: -1px; z-index: 1; border-right:solid 1px #cbcdcf">
										<div id="div5" runat="server">
											<div id="div6" runat="server" visible="false">
												<div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0.3; text-align: center; cursor: not-allowed; margin-left:15px;"></div>
											</div>

											<div class="row mini-header">
												<div class="col-sm-4" style="padding-right: 0px;">
													<table border="0">
														<tr>
															<td>
																<label id="lblbhs_laboratory_FutureOrder_Diagnostic" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Future Diagnostic</label>
															</td>
															<td>
																<div class="loadingdiagnostic_FutureOrder" style="display: none;" runat="server" id="loadingdiagnostic_FutureOrder">
																	<div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
																	</div>
																	&nbsp;
																	<img alt="" style="background-color: transparent; height: 15px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
																    <asp:HiddenField ID="HFloadingdiagnostic_FutureOrder" runat="server" />
                                                                </div>
															</td>
														</tr>
													</table>
												</div>
												<div class="col-sm-8 text-right" style="padding-left: 0px;">
													<!-- kotak pencarian Autocomplete -->
													<asp:UpdatePanel runat="server" ID="UpdatePanelSearchBoxDiagnosticFutureOrder" UpdateMode="Conditional">
														<ContentTemplate>
                                                            <asp:Button runat="server" ID="btnResetDiagnosticFutureOrder" Visible="false" BorderStyle="None" Style="color: #9d1fc3; font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px" Text="Reset" OnClientClick="return konfirmasireset('Furure Order Diagnostic');" OnClick="btnResetDiagProc_Click"></asp:Button>
															<div style="display: inline;">
																<div class="has-feedback" style="display: inline;">
																	<asp:TextBox ID="txtItem_DIAG_FutureOrder" runat="server" Placeholder="Add item here..." Style="width: 130px; font-weight: normal; font-size: 12px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('diagnostic_FutureOrder','true');"></asp:TextBox>
																	<span class="fa fa-caret-down form-control-feedback" style="margin-top: -9px; z-index: 0;"></span>
																</div>
																<asp:HiddenField ID="HF_ItemSelectedDiagFutureOrderNonModal" runat="server" />
																<asp:Button ID="ButtonAjaxSearchDiagnosticFutureOrderNonModal" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchDiagProcNonModal_Click" />
															</div>
														</ContentTemplate>
													</asp:UpdatePanel>
													<!-- end kotak pencarian -->
												</div>
											</div>
                                            <div style="margin-left: 13px; margin-top: 7.5px; margin-bottom: 13.1px;">
												<div class="future-order-date-delete">
													<asp:TextBox class="form-control" ID="dp_diag" runat="server" CssClass="isCalendar" Style="width: 100%; height: 35px;" placeholder="choose an estimated date..." onmousedown="dateSelectDiag();" />
													<a id="dp_diagDelete" runat="server" title="Delete" class="btn-delete-style" style="display:none;" onclick="deleteFutureOrderDate(this,'futureOrderDiag');">
														<i class="fa fa-times-circle"></i>
													</a>
												</div>
                                            </div>
                                            <div style="overflow-y: auto; max-height: 250px;" class="scrollEMR">
                                                <asp:Label runat="server" ID="labempty_Diagnostic_FutureOrder" Style="padding-left: 15px;">
                                                <span>
                                                    <img src="<%= Page.ResolveClientUrl("~/Images/Dashboard/ic_LatestHistory_new.svg") %>" />
                                                </span>
                                                <label id="lblbhs_nolabadded_diagnostic_futureorder" style="font-size: 12px; font-weight: bold; color: #bdbfd8; margin-right: 5px;">Tidak ada order </label>
                                                </asp:Label>
                                                <ul style="padding-left: 15px">
                                                    <asp:GridView ID="GridView_DiagnosticList_FutureOrder" runat="server" AutoGenerateColumns="false" CssClass="table-kecil" ShowHeader="false"
                                                        DataKeyNames="ProcedureItemId" BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="titik" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="3%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                                <ItemTemplate>
                                                                    <i class="fa fa-circle" style="font-size: 5px; vertical-align: middle; margin-right: 2px;"></i>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nama" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hf_id_submitdiagnostic_futureorder" runat="server" Value='<%# Bind("ProcedureItemId") %>'></asp:HiddenField>
                                                                    <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="lblItemDiagnosticFutureOrderName" runat="server" Text='<%# Bind("ProcedureItemName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btndeletediag_FutureOrder" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btndeletediagproc_Click" Style="width: 12px; height: 12px; margin-top: 3px;" Visible='<%# Eval("IsSendHope").ToString() == "0" ? true : false %>' />
                                                                <i class="fa fa-info-circle" style='font-size: 14px; color: cornflowerblue; margin-top: 3px; display:<%# Eval("IsSendHope").ToString() == "0" ? "none" : "block" %>' title="Already send to hope"></i>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ul>
                                            </div>
										</div>
									</div>
								</ContentTemplate>
							</asp:UpdatePanel>

                            <asp:UpdatePanel ID="UpdatePanelDivProcedureFutureOrder" runat="server" UpdateMode="Conditional">
								<ContentTemplate>
									<div class="col-sm-6" style="padding-left: 0px; padding-bottom: 5px;border-left:solid 1px #cbcdcf">
										<div id="div2" runat="server">
											<div id="div3" runat="server" visible="false">
												<div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0.3; text-align: center; cursor: not-allowed; margin-left:15px;"></div>
											</div>

											<div class="row mini-header">
												<div class="col-sm-4" style="padding-right: 0px;">
													<table border="0">
														<tr>
															<td>
																<label id="lblbhs_laboratory_FutureOrder_Procedure" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Future Diagnostic</label>
															</td>
															<td>
																<div class="loadingprocedure_FutureOrder" style="display: none;" runat="server" id="loadingprocedure_FutureOrder">
																	<div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
																	</div>
																	&nbsp;
																	<img alt="" style="background-color: transparent; height: 15px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
																    <asp:HiddenField ID="HFloadingprocedure_FutureOrder" runat="server" />
                                                                </div>
															</td>
														</tr>
													</table>
												</div>
												<div class="col-sm-8 text-right" style="padding-left: 0px;">
													<!-- kotak pencarian Autocomplete -->
													<asp:UpdatePanel runat="server" ID="UpdatePanelSearchBoxProcedureFutureOrder" UpdateMode="Conditional">
														<ContentTemplate>
                                                            <asp:Button runat="server" ID="btnResetProcedureFutureOrder" Visible="false" BorderStyle="None" Style="color: #9d1fc3; font-family: Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: underline; font-size: 12px; height: 20px" Text="Reset" OnClientClick="return konfirmasireset('Future Order Procedure');" OnClick="btnResetDiagProc_Click"></asp:Button>
															<div style="display: inline;">
																<div class="has-feedback" style="display: inline;">
																	<asp:TextBox ID="txtItem_PROC_FutureOrder" runat="server" Placeholder="Add item here..." Style="width: 130px; font-weight: normal; font-size: 12px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('procedure_FutureOrder','true');"></asp:TextBox>
																	<span class="fa fa-caret-down form-control-feedback" style="margin-top: -9px; z-index: 0;"></span>
																</div>
																<asp:HiddenField ID="HF_ItemSelectedProcedureFutureOrderNonModal" runat="server" />
																<asp:Button ID="ButtonAjaxSearchProcedureFutureOrderNonModal" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchDiagProcNonModal_Click" />
															</div>
														</ContentTemplate>
													</asp:UpdatePanel>
													<!-- end kotak pencarian -->
												</div>
											</div>
                                            <div style=" margin-left: 13px; margin-top: 7.5px; margin-bottom: 13.1px;">
												<div class="future-order-date-delete">
													<asp:TextBox class="form-control" ID="dp_proc" runat="server" CssClass="isCalendar" Style="width: 100%; height: 35px;" placeholder="choose an estimated date..." onmousedown="dateSelectProc();" />
													<a id="dp_procDelete" runat="server" title="Delete" class="btn-delete-style" style="display:none;" onclick="deleteFutureOrderDate(this,'futureOrderProc');">
														<i class="fa fa-times-circle"></i>
													</a>
												</div>
                                            </div>
                                            <div style="overflow-y: auto; max-height: 250px;" class="scrollEMR">
                                                <asp:Label runat="server" ID="labempty_Procedure_FutureOrder" Style="padding-left: 15px;">
                                                <span>
                                                    <img src="<%= Page.ResolveClientUrl("~/Images/Dashboard/ic_LatestHistory_new.svg") %>" />
                                                </span>
                                                <label id="lblbhs_nolabadded_procedure_futureorder" style="font-size: 12px; font-weight: bold; color: #bdbfd8; margin-right: 5px;">Tidak ada order </label>
                                                </asp:Label>
                                                <ul style="padding-left: 15px">
                                                    <asp:GridView ID="GridView_ProcedureList_FutureOrder" runat="server" AutoGenerateColumns="false" CssClass="table-kecil" ShowHeader="false"
                                                        DataKeyNames="ProcedureItemId" BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="titik" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="3%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                                <ItemTemplate>
                                                                    <i class="fa fa-circle" style="font-size: 5px; vertical-align: middle; margin-right: 2px;"></i>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nama" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hf_id_submitprocedure_futureorder" runat="server" Value='<%# Bind("ProcedureItemId") %>'></asp:HiddenField>
                                                                    <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="lblItemProcedureFutureOrderName" runat="server" Text='<%# Bind("ProcedureItemName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btndeleteproc_FutureOrder" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btndeletediagproc_Click" Style="width: 12px; height: 12px; margin-top: 3px;" Visible='<%# Eval("IsSendHope").ToString() == "0" ? true : false %>' />
                                                                <i class="fa fa-info-circle" style='font-size: 14px; color: cornflowerblue; margin-top: 3px; display:<%# Eval("IsSendHope").ToString() == "0" ? "none" : "block" %>' title="Already send to hope"></i>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ul>
                                            </div>
										</div>
									</div>
								</ContentTemplate>
							</asp:UpdatePanel>
						</div>
						<div class="box-border-soap" style="padding-bottom: 0px; padding-top: 0px; border-top: solid 1px #cbcdcf;">
							<div class="row">
								<div class="col-sm-6" style="padding-left: 30px; padding-top: 5px; border-right: solid 1px #cbcdcf; margin-right: -1px;">
									<label id="lblbhs_othersDiag_FutureOrder" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Others Diagnostic</label>
									(Misal: Diagnostic yang tidak ada didalam form)
                            <br />
									<div class="scrollEMR" style="max-height: 88px; overflow-y: auto">
										<asp:TextBox BackColor="#F2F3F4" runat="server" CssClass="text-multiline-dialog" placeholder="Type here..." BorderColor="transparent" ID="txtplanningotherDiagnostic_FutureOrder" TextMode="MultiLine" Rows="1" onkeyup="isMandatoryField();" onkeydown="AutoExpand(this); checkTextAreaMaxLength(this,event,'100');" onblur="minexpand(this)" />
									</div>
								</div>
								<div class="col-sm-6" style="padding-left: 15px; padding-top: 5px; border-left: solid 1px #cbcdcf;">
									<label id="lblbhs_othersProd_FutureOrder" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Others Procedure</label>
									(Misal: Procedure yang tidak ada didalam form)
                            <br />
									<div class="scrollEMR" style="max-height: 88px; overflow-y: auto">
										<asp:TextBox BackColor="#F2F3F4" runat="server" CssClass="text-multiline-dialog" placeholder="Type here..." BorderColor="transparent" ID="txtplanningotherProcedure_FutureOrder" TextMode="MultiLine" Rows="1" onkeyup="isMandatoryField();" onkeydown="AutoExpand(this); checkTextAreaMaxLength(this,event,'100');" onblur="minexpand(this)" />
									</div>
								</div>
							</div>
						</div>
					</div>
                </div>
                <!-- End Future Order -->
        <%-- ==================================================== end DIAGNOSTIC & PROCEDURE ============================================================ --%>


























        <div class="mini-dialog" style="margin-bottom: 10px;">
            <%-- ==================================================== alergie dan routine ============================================================ --%>
            <div class="row">
                <span class="anchor" id="allergy_begin"></span>
                <div class="col-sm-6 vertical-right" style="padding-right: 1px; padding-bottom: 5px; margin-right: -1px;">
                    <asp:UpdatePanel runat="server" ID="UpdatePanelAllergyPlanning" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row mini-header-color" style="background-color: #c43d32; border-radius: 7px 0px 0px 0px; margin-left: 0px; margin-right: 0px;">
                                <div class="col-sm-6" style="padding-left: 0px;">
                                    <table border="0">
                                        <tr>
                                            <td>
                                                <img src="<%= Page.ResolveClientUrl("~/Images/Dashboard/ic_Allergies_putih.svg") %>" style="height: 20px; width: 20px;" />
                                                <label id="lblbhs_allergies" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Allergies</label>
                                                <a id="linkto_lblbhs_allergies" href="#allergy_begin" style="display: none;"></a>
                                            </td>
                                            <td>
                                                <div class="loadingallergy" style="display: none;">
                                                    <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                    </div>
                                                    &nbsp;
                                                    <img alt="" style="background-color: transparent; height: 20px; filter: brightness(2);" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-6" style="text-align: right; padding-right: 0px;">
                                    <%--<a href="javascript:shortcutFormAllergy();" style="background-color: white; padding: 2px 4px 2px 4px; border-radius: 5px;"> 
                                        <label id="lblbhs_addallergies" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 12px; color:#c43d32; cursor:pointer;" title="Add Allergy"><i class="fa fa-plus-square"></i> Add </label> 
                                    </a>--%>
                                    <asp:HiddenField ID="HF_ForceNo_MA_Allergy" runat="server" />
                                    <div id="loading-MA-Allergy" style="display:none;">
                                        <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center"></div>
                                        &nbsp;
                                        <img alt="" style="background-color: transparent; height: 20px; filter: brightness(2);" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                    </div>
                                    <div style="background-color: white; padding: 2px 4px 2px 4px; border-radius: 5px; color: #c43d32; display: table; float: right;">
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    <div class="radio-margin">
                                                        <div class="pretty p-default p-round">
                                                            <asp:RadioButton runat="server" GroupName="radioallalergiout" Value="0" ID="rballergy1" onclick="klikForceNo('Allergy'); ForceNoIframe_MedicationAllergies('Allergy');" />
                                                            <div class="state p-primary-o">
                                                                <label style="font-weight: bold;">No</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="radio-margin">
                                                        <div class="pretty p-default p-round">
                                                            <asp:RadioButton runat="server" GroupName="radioallalergiout" Value="1" ID="rballergy2" onclick="shortcutFormAllergy();" />
                                                            <div class="state p-primary-o">
                                                                <label style="font-weight: bold;">Yes</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <%--<asp:Button ID="ButtonAddAllergies" runat="server" CssClass="btn btn-default" Text="Add Allergy" OnClientClick="shortcutFormAllergy();" />--%>
                                </div>
                            </div>
                            <div style="overflow-y: auto; max-height: 85px; margin-left: 15px; margin-top: 5px;" class="scrollEMR">
                                <div class="btn-group btn-group-justified" role="group">
                                    <div class="btn-group" role="group" style="vertical-align: top">
                                        <div>
                                            <b> <label id="lblbhs_drugs">Drugspln</label> </b>
                                            <table border="0" style="margin-bottom: 8px;" class="hidden">
                                                <tr>
                                                    <td>
                                                        <div class="radio-margin">
                                                            <div class="pretty p-default p-round">
                                                                <asp:RadioButton runat="server" GroupName="radiodrugout" Value="0" ID="rbdrug1" onclick="shortcutNoDrug();" />
                                                                <div class="state p-primary-o">
                                                                    <label>No</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="radio-margin">
                                                            <div class="pretty p-default p-round">
                                                                <asp:RadioButton runat="server" GroupName="radiodrugout" Value="1" ID="rbdrug2" onclick="shortcutFormAllergy();" />
                                                                <div class="state p-primary-o">
                                                                    <label>Yes</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Label runat="server" ID="nodrugs">
                                            <label id="lblbhs_nodrugallergy" style="color: #bdbfd8;"> No Drug Allergy </label>
                                        </asp:Label>
                                        <asp:Repeater runat="server" ID="DrugAllergy">
                                            <ItemTemplate>
                                                <li style="display:none;">
                                                    <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("allergy") %>' Enabled="false" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater runat="server" ID="DrugAllergy_HI">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Label ID="LblHealthInfoValue" runat="server" Text='<%#Eval("health_info_value") %>' Enabled="false" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="btn-group" role="group" style="vertical-align: top">
                                        <div>
                                            <b> <label id="lblbhs_food">Food</label> </b>
                                            <table border="0" style="margin-bottom: 8px;" class="hidden">
                                                <tr>
                                                    <td>
                                                        <div class="radio-margin">
                                                            <div class="pretty p-default p-round">
                                                                <asp:RadioButton runat="server" GroupName="radiofoodout" Value="0" ID="rbfood1" onclick="shortcutNoFood();" />
                                                                <div class="state p-primary-o">
                                                                    <label>No</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="radio-margin">
                                                            <div class="pretty p-default p-round">
                                                                <asp:RadioButton runat="server" GroupName="radiofoodout" Value="1" ID="rbfood2" onclick="shortcutFormAllergy();" />
                                                                <div class="state p-primary-o">
                                                                    <label>Yes</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Label runat="server" ID="nofood">
                                            <label id="lblbhs_nofoodallergy" style="color: #bdbfd8;"> No Food Allergy </label>
                                        </asp:Label>
                                        <asp:Repeater runat="server" ID="FoodAllergy">
                                            <ItemTemplate>
                                                <li style="display:none;">
                                                    <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("allergy") %>' Enabled="false" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater runat="server" ID="FoodAllergy_HI">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Label ID="LblHealthInfoValue" runat="server" Text='<%#Eval("health_info_value") %>' Enabled="false" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="btn-group" role="group" style="vertical-align: top">
                                        <div>
                                            <b> <label id="lblbhs_otheralergi">Others</label> </b>
                                            <table border="0" style="margin-bottom: 8px;" class="hidden">
                                                <tr>
                                                    <td>
                                                        <div class="radio-margin">
                                                            <div class="pretty p-default p-round">
                                                                <asp:RadioButton runat="server" GroupName="radiootherout" Value="0" ID="rbother1" onclick="shortcutNoOther();" />
                                                                <div class="state p-primary-o">
                                                                    <label>No</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="radio-margin">
                                                            <div class="pretty p-default p-round">
                                                                <asp:RadioButton runat="server" GroupName="radiootherout" Value="1" ID="rbother2" onclick="shortcutFormAllergy();" />
                                                                <div class="state p-primary-o">
                                                                    <label>Yes</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Label runat="server" ID="noother">
                                            <label id="lblbhs_nootherallergy" style="color: #bdbfd8;"> No Other Allergy </label>
                                        </asp:Label>
                                        <asp:Repeater runat="server" ID="OtherAllergy">
                                            <ItemTemplate>
                                                <li style="display:none;">
                                                    <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("allergy") %>' Enabled="false" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater runat="server" ID="OtherAllergy_HI">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Label ID="LblHealthInfoValue" runat="server" Text='<%#Eval("health_info_value") %>' Enabled="false" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="col-sm-6 vertical-left" style="padding-left: 0px; padding-bottom: 5px;">
                    <asp:UpdatePanel runat="server" ID="UpdatePanelRoutinePlanning" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row mini-header-color" style="background-color: #7b88ff; border-radius: 0px 7px 0px 0px; margin-left: 0px; margin-right: -1px;">
                                <div class="col-sm-7" style="padding-left: 0px;">
                                    <table border="0">
                                        <tr>
                                            <td>
                                                <img src="<%= Page.ResolveClientUrl("~/Images/Dashboard/ic_LatestMedication_putih.svg") %>" style="height: 20px; width: 20px;" />
                                                <label id="lblbhs_routinemedication" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Routine Medication</label>
                                            </td>
                                            <td>
                                                <div class="loadingroutine" style="display: none;">
                                                    <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                    </div>
                                                    &nbsp;
                                                    <img alt="" style="background-color: transparent; height: 20px; filter: brightness(2);" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-5" style="text-align: right; padding-right: 0px;">
                                    <%--<a href="javascript:shortcutFormAllergy();" style="background-color: white; padding: 2px 4px 2px 4px; border-radius: 5px;"> 
                                        <label id="lblbhs_addroutinemed" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 12px; color:#7b88ff; cursor:pointer;" title="Add Routine Medication"><i class="fa fa-plus-square"></i> Add </label> 
                                    </a>--%>
                                    <asp:HiddenField ID="HF_ForceNo_MA_Routine" runat="server" />
                                    <div id="loading-MA-Routine" style="display:none;">
                                        <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center"></div>
                                        &nbsp;
                                        <img alt="" style="background-color: transparent; height: 20px; filter: brightness(2);" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                    </div>
                                    <div style="background-color: white; padding: 2px 4px 2px 4px; border-radius: 5px; color: #7b88ff; display: table; float: right;">
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    <div class="radio-margin">
                                                        <div class="pretty p-default p-round">
                                                            <asp:RadioButton runat="server" GroupName="radiopengobatanout" Value="0" ID="rbpengobatan1" onclick="klikForceNo('Routine'); ForceNoIframe_MedicationAllergies('Routine');" />
                                                            <div class="state p-primary-o">
                                                                <label style="font-weight: bold;">No</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="radio-margin">
                                                        <div class="pretty p-default p-round">
                                                            <asp:RadioButton runat="server" GroupName="radiopengobatanout" Value="1" ID="rbpengobatan2" onclick="shortcutFormRoutine();" />
                                                            <div class="state p-primary-o">
                                                                <label style="font-weight: bold;">Yes</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div style="overflow-y: auto; max-height: 85px; margin-left: 15px; margin-top: 5px;" class="scrollEMR">
                                <asp:Label runat="server" ID="noroutine">
                                    <label id="lblbhs_noroutinemedication" style="color: #bdbfd8;"> No Routine Medication </label>
                                </asp:Label>
                                <asp:Repeater runat="server" ID="RepCurrentMedication">
                                    <ItemTemplate>
                                        <li style="display:none;">
                                            <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("medication") %>' Enabled="false" />
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater runat="server" ID="RepCurrentMedication_HI">
                                    <ItemTemplate>
                                        <li>
                                            <asp:Label ID="LblHealthInfoValue" runat="server" Text='<%#Eval("health_info_value") %>' Enabled="false" />
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <%-- ==================================================== end alergie dan routine ============================================================ --%>

            <%-- ==================================================== drug prescription ============================================================ --%>
            <div class="box-border-soap" style="padding-bottom: 0px">
                <div style="padding-left: 15px;">
                    <%--<asp:UpdatePanel runat="server">
                        <ContentTemplate>--%>
                            <div class="row">
                                <span class="anchor" id="prescription_begin"></span>
                                <div class="col-sm-6" style="padding-left: 0px;">
                                    <table border="0" style="background-color: #f88805; border-radius: 0px 7px 0px 0px; min-width: 196px;">
                                        <tr>
                                            <td style="padding-left: 15px; padding-top: 6px; padding-bottom: 7px; color: white;">
                                                <img src="<%= Page.ResolveClientUrl("~/Images/Dashboard/ic_rx_putih.svg") %>" style="height: 20px; width: 20px;" />
                                                <label id="lblbhs_drugprescription" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Drugs Prescription</label>
                                                <a id="linkto_lblbhs_drugprescription" href="#prescription_begin" style="display: none;"></a>
                                            </td>
                                            <td style="padding-right: 15px; padding-top: 6px; padding-bottom: 7px;">
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelListPrescription">
                                                    <ProgressTemplate>
                                                        <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                        </div>
                                                        &nbsp;
                                                        <img alt="" style="background-color: transparent; height: 20px; filter: brightness(2);" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upFormularium">
                                                    <ProgressTemplate>
                                                        <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                        </div>
                                                        &nbsp; 
                                                        <img alt="" style="background-color: transparent; height: 20px; filter: brightness(2);" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-6 text-right" style="padding-right: 30px; padding-top: 3px;">
                                    <table border="0" style="float: right">
                                        <tr>
                                            <td>
                                                <asp:UpdateProgress ID="UpdateProgress12" runat="server" AssociatedUpdatePanelID="upSaveAsOrderSet">
                                                    <ProgressTemplate>
                                                        <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center"> </div>
                                                        <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                        &nbsp; &nbsp;
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                            <td style="padding-right: 15px;">
                                                <asp:UpdatePanel runat="server" ID="upSaveAsOrderSet" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="btn-group">
                                                            <asp:HyperLink ID="HyperLinkSaveOrderSet" runat="server" data-toggle="dropdown" aria-expanded="false" NavigateUrl="#" Style="font-weight: bold; text-decoration: underline; display: none;" onmouseup="klikSaveAsFocus('TextBoxSaveAsOrderSetName');">Save As Order Set</asp:HyperLink>
                                                            <ul class="dropdown-menu pop-shadow" style="padding: 8px;">
                                                                <li style="padding-bottom: 3px;">Input Order Set Name :
                                                                </li>
                                                                <li style="padding-bottom: 5px;">
                                                                    <asp:TextBox ID="TextBoxSaveAsOrderSetName" onkeydown="return txtOnKeyPressFalse();" onkeyup="checkSaveAsEmpty('TextBoxSaveAsOrderSetName','LinkSaveAsOrderSet');" runat="server"></asp:TextBox>
                                                                </li>
                                                                <li style="float: right;">
                                                                    <asp:LinkButton ID="LinkSaveAsOrderSet" class="btn btn-default btn-sm" runat="server" Style="font-weight: bold; background-color: #228b22; color: white; width: 80px; display: none;" OnClick="LinkSaveAsOrderSet_Click">Save</asp:LinkButton>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="padding-top: 3px;">
                                                <asp:UpdatePanel runat="server" ID="upFormularium" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="pretty p-icon p-curve">
                                                            <asp:CheckBox runat="server" ID="chkformularium" AutoPostBack="true" OnCheckedChanged="formularium_onclik" />
                                                            <div class="state p-success">
                                                                <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                                                <label id="lblbhs_drugoutsideformularium" style="font-size: 11px; vertical-align: text-top;">Drugs Outside Formularium</label>
                                                            </div>
                                                        </div>
                                                        <asp:HiddenField ID="hfchkformularium" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>

                    <div class="scrollEMR" style="max-height: 340px; overflow-y: auto; margin-left: -15px; margin-top: 0px;">
                        <asp:UpdatePanel ID="UpdatePanelListPrescription" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="gvw_drug" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed"
                                    OnRowDataBound="drugs_data_RowDataBound" DataKeyNames="prescription_id">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Item" ItemStyle-Width="30%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <div style="display:table">
                                                    <div style="display:none;"> <%--style="display:table-cell;"--%>
                                                        <asp:Label ID="LblIconMims" runat="server" Text="" Visible='<%# Eval("cims_result") == "" ? false : true %>'>
                                                            <a href="#" style='pointer-events:<%# Eval("cims_result") == "fa fa-check-circle m-safe-icon" ? "none" : "auto" %>;'> <i class='<%# Eval("cims_result") %>'></i> </a> 
                                                        </asp:Label>
                                                    </div>
                                                    <div style="display:table-cell; padding-left:10px;">
                                                        <asp:HiddenField ID="prescription_id" runat="server" Value='<%# Bind("prescription_id") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="prescription_no" runat="server" Value='<%# Bind("prescription_no") %>'></asp:HiddenField>
                                                        <%--<asp:Label id="prescription_id" runat="server" Text='<%# Bind("prescription_id") %>' Visible="false"><%# Eval("prescription_id").ToString() %></asp:Label>--%>
                                                        <%--<asp:Label id="item_id" runat="server" Text='<%# Bind("item_id") %>' Visible="false"><%# Eval("item_id").ToString() %></asp:Label>--%>
                                                        <asp:HiddenField ID="item_id" runat="server" Value='<%# Bind("item_id") %>'></asp:HiddenField>
                                                        <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="item_name" runat="server" Text='<%# Bind("item_name") %>'></asp:Label>
                                                    </div>
                                                </div>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Text" ItemStyle-CssClass="numberofGrid3" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <div class="pretty p-icon p-curve" style="margin-top: 4px;"  title="Use Dose Text">
                                                    <asp:CheckBox ID="is_dosetext" runat="server" OnCheckedChanged="is_dosetext_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("IsDoseText") %>'></asp:CheckBox>
                                                    <div class="state p-success">
                                                        <i class="icon fa fa-check"></i>
                                                        <label></label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dose" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <div runat="server" Visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                                    <asp:TextBox  Style="text-align: right; width:40px;" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="dosage_id" runat="server" Text='<%# Bind("dosage_id") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                                <asp:DropDownList  ID="doseuom" style="margin: 0px; width:60px;" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server" ></asp:DropDownList>
                                                </div>
                                                
                                                <asp:TextBox  Width="100%" MaxLength="100" Font-Size="11px" Style="margin: 0px; overflow: hidden;" Font-Names="Helvetica, Arial, sans-serif" ID="dosetext" runat="server" Visible='<%# Eval("IsDoseText") %>' Text='<%# Bind("dose_text") %>' onkeydown="AutoExpand(this);" onkeyup="charLimitInstruction(this,100);" TextMode="MultiLine" Rows="1" onfocus="minexpand(this)" CssClass="text-multiline-dialog"></asp:TextBox>
                                                <asp:HiddenField ID="dose_text" runat="server" Value='<%# Bind("dose_text") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Frequency" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <%--<asp:TextBox Width="100%" CssClass="box"  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="frequency_code" runat="server"  Text='<%# Bind("frequency_code") %>'></asp:TextBox>--%>
                                                <asp:DropDownList ID="frequency_code" Style="margin: 0px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="administrationRouteCode" Style="margin: 0px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server"></asp:DropDownList>
                                                <%--<asp:TextBox Width="100%" CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="administrationRouteCode" runat="server"  Text='<%# Bind("administrationRouteCode") %>'></asp:TextBox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Instruction" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:TextBox Width="100%" MaxLength="100" Font-Size="11px" Style="margin: 0px; overflow: hidden;" Font-Names="Helvetica, Arial, sans-serif" ID="remarks" runat="server" Text='<%# Bind("remarks") %>' onkeydown="AutoExpand(this);" onkeyup="charLimitInstruction(this,100);" TextMode="MultiLine" Rows="1" onfocus="minexpand(this)" CssClass="text-multiline-dialog auto-expand"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" ItemStyle-CssClass="numberofGrid3" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:TextBox Width="100%" Style="text-align: right; margin: 0px" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="quantity" runat="server" Text='<%# Bind("quantity") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UoM" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="uom_id" runat="server" Value='<%# Bind("uom_id") %>'></asp:HiddenField>
                                                <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="uom_code" runat="server" Text='<%# Bind("uom_code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Iter" ItemStyle-CssClass="numberofGrid1" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:TextBox Width="100%" Style="text-align: right; margin: 0px" onkeypress="return CheckNumeric();" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="iteration" runat="server" Text='<%# Bind("iteration") %>' onkeydown="return txtOnKeyPressFalse();" Enabled='<%# Eval("is_iter").ToString().ToUpper() == "TRUE"? true: false%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Routine" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div class="pretty p-icon p-curve" style="margin-top: 4px;">
                                                    <asp:CheckBox ID="is_routine" runat="server" Checked='<%# Eval("is_routine").ToString() != "0" %>'></asp:CheckBox>
                                                    <div class="state p-success">
                                                        <i class="icon fa fa-check"></i>
                                                        <label></label>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="is_consumables" runat="server" Value='<%# Bind("is_consumables") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="compound_id" runat="server" Value='<%# Bind("compound_id") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="compound_name" runat="server" Value='<%# Bind("compound_name") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="origin_prescription_id" runat="server" Value='<%# Bind("origin_prescription_id") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="hope_arinvoice_id" runat="server" Value='<%# Bind("hope_arinvoice_id") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="is_delete" runat="server" Value='<%# Bind("is_delete") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btndelete" runat="server" OnClick="btnDelete_Click"><span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_delete.svg") %>" class="ic_delete"></span></asp:LinkButton>
                                                <%--<asp:Button  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="X" OnClick="btnDelete_Click"></asp:Button>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <asp:HiddenField ID="HiddenFlagSearchFocus" runat="server" />

                    <!-- kotak pencarian -->
                    <div style="margin-top: 10px; display: none;" runat="server" visible="false">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-sm-3" style="width: 190px;">
                                        <asp:TextBox runat="server" ID="txtItemId" Visible="false" ReadOnly="true" />
                                        <div class="has-feedback" style="width: 180px;">
                                            <asp:TextBox runat="server" ID="txtItemAdd" Width="180px" Placeholder="Add item here..." ReadOnly="true" onclick="OnClick()" />
                                            <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px; z-index: 0;"></span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:UpdateProgress ID="uProgLogin" runat="server" AssociatedUpdatePanelID="upError">
                                            <ProgressTemplate>
                                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                </div>
                                                <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div id="testpopup" style="display: none; position: absolute; z-index: 1; border: 1px groove; min-width: 600px; min-height: 200px; background-color: white; max-height: 250px; max-width: 800px">
                            <asp:UpdatePanel runat="server" ID="upError" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div style="margin: 5px;" class="row">
                                        <div class="col-sm-6" style="padding-left: 0px;">
                                            <asp:TextBox runat="server" ID="txtSearchItem" onkeydown="return txtOnKeyPressDrugs();"></asp:TextBox>
                                            <asp:Button runat="server" ID="btnfind" CssClass="btn btn-warning btn-emr-small" OnClick="btnFind_click" Text="Find" />
                                        </div>
                                        <div class="col-sm-6 text-right">
                                            <div id="divwarningconnection" runat="server" visible="false" style="color: #c43d32; font-size: 14px;">
                                                <i class="fa fa-warning blink"></i>
                                                <label id="lblbhs_stokupdate" style="font-weight: bold;">Offline: Stock is not update </label>
                                            </div>
                                            <label id="lblbhs_updatestockdrug" style="color: #c43d32; font-size: 14px; font-weight: bold;">
                                                Stok update per tanggal
                                                <asp:Label ID="Label_updatestockdrug" runat="server" Text="-"></asp:Label>
                                                
                                            </label>
                                        </div>
                                    </div>
                                    <div class="scrollEMR" style="overflow-y: auto; max-height: 200px; max-width: 800px; min-width: 600px; min-height: 200px;">
                                        <asp:GridView ID="gvw_data" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed" BorderColor="Black"
                                            HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                                            ShowHeaderWhenEmpty="True" DataKeyNames="SalesItemId" EmptyDataText="No Data"
                                            AllowSorting="True">
                                            <PagerStyle CssClass="pagination-ys" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="salesitemid" runat="server" Text='<%# Bind("SalesItemId") %>' Visible="false"><%# Eval("SalesItemId").ToString() %></asp:Label>
                                                        <asp:LinkButton Font-Underline="true" Font-Size="12px" ID="salesItemName" runat="server" Text='<%# Bind("SalesItemName") %>' OnClick="itemselected_onclick" OnClientClick="OnClick()"></asp:LinkButton>
                                                        <asp:HiddenField ID="hfUomId" Value='<%# Bind("SalesUomId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfUomCode" Value='<%# Bind("SalesUomCode") %>' runat="server" />
                                                        <asp:HiddenField ID="hfDose" Value='<%# Bind("Dose") %>' runat="server" />
                                                        <asp:HiddenField ID="hfDoseText" Value='<%# Bind("DoseText") %>' runat="server" />
                                                        <asp:HiddenField ID="hfDoseUomId" Value='<%# Bind("DoseUomId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfAdministrationFrequencyId" Value='<%# Bind("AdministrationFrequencyId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfAdministrationRouteId" Value='<%# Bind("AdministrationRouteId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfAdministrationInstruction" Value='<%# Bind("AdministrationInstruction") %>' runat="server" />
                                                        <asp:HiddenField ID="hfIsIter" runat="server" Value='<%#Bind("IsIter") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Active Ingredients" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ActiveIng" runat="server" Text='<%# Bind("ActiveIngredientsName") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="5%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Qty" runat="server"> <span style="color:<%# Eval("TotalQuantity","{0:0.00}").ToString() == "0,00" ? "red" : "#333"%>; width:65px"> <%# Eval("TotalQuantity","{0:#,##0.00}") %> </span> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Quantity" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" DataField="TotalQuantity" DataFormatString="{0:0.00}" SortExpression="TotalQuantity"></asp:BoundField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <!-- end kotak pencarian -->

                    <!-- kotak pencarian Autocomplete -->
                    <div style="margin-top: 10px;">
                        <asp:UpdatePanel runat="server" ID="UP_SearchDrug" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-sm-3" style="width: 190px;">
                                        <div class="has-feedback" style="width: 180px;">
                                            <%-- <input type="text" ID="txtItemAdd_AC" Placeholder="Add item here..." style="width:180px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" />--%>
                                            <asp:TextBox ID="txtItemAdd_AC" runat="server" Placeholder="Add item here..." Style="width: 180px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('drug','true');"></asp:TextBox>
                                            <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px; z-index: 0;"></span>
                                        </div>
                                        <asp:HiddenField ID="HF_flagfocusdrugsearch" runat="server" />
                                        <asp:HiddenField ID="HF_ItemSelecteddrug" runat="server" />
                                        <asp:HiddenField ID="HF_connstatus" runat="server" />
                                        <asp:HiddenField ID="HF_Label_updatestockdrug" runat="server" />
                                        <asp:Button ID="ButtonAjaxSearchDrug" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchDrug_Click" />
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="loadingdrug" style="display: none;">
                                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                            </div>
                                            &nbsp;
                                            <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                            <asp:HiddenField ID="HFloadingdrug" runat="server" />
                                        </div>
                                    </div>
                                    
                                    <asp:Button ID="BtnCheckDrugInteraction" runat="server" Text="Check Drug Interaction !" CssClass="btn btn-danger btn-sm" OnClick="BtnCheckDrugInteraction_Click" style="background-color:#BA1000; color:white; float:right; margin-right: 40px; height: 25px; padding-top: 3px; display:none;" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <!-- end kotak pencarian -->
                </div>
            </div>
            <%-- ==================================================== end drug prescription ============================================================ --%>

            <%-- ==================================================== Prescription and Pharmacist Notes ============================================================ --%>
            <%--<asp:UpdatePanel runat="server">
                <ContentTemplate>--%>
                    <div class="box-border-soap" style="padding-bottom: 0px; margin-top: 10px;">
                        <div style="padding-left: 15px;">
                            <label id="lblbhs_prescriptionnotes" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Prescription Notes</label>
                            <br />
                            <div class="scrollEMR" style="max-height: 90px; overflow-y: auto">
                                <asp:TextBox runat="server" CssClass="text-multiline-dialog" placeholder="Type here..." BorderColor="transparent" ID="txtPresNotes" TextMode="MultiLine" Rows="1" onkeydown="AutoExpand(this)" onblur="minexpand(this)" />
                            </div>
                        </div>
                    </div>
                    <div class="box-border-soap" style="padding-bottom: 0px">
                        <div style="padding-left: 15px;">
                            <label id="lblbhs_pharmacistnotes" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Pharmacist Notes</label>
                            <br />
                            <div class="scrollEMR" style="max-height: 90px; overflow-y: auto">
                                <asp:TextBox runat="server" CssClass="text-multiline-dialog" placeholder="..." BorderColor="transparent" ID="txtPharmacistNotes" TextMode="MultiLine" Rows="1" ReadOnly="true" onkeydown="AutoExpand(this)" onblur="minexpand(this)" />
                            </div>
                        </div>
                    </div>
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
            <%-- ==================================================== end Prescription Notes ============================================================ --%>
        </div>

        <%--<div class="mini-dialog" style="margin-bottom: 10px;" id="divcompound" runat="server" visible="false">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <!-- ==================================================== COMPOUND PRESCRIPTION [Exclude] =================================================== -->
                    <div class="mini-header">
                        <label style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Compound Prescription</label>
                    </div>
                    <div class="scrollEMR" style="max-height: 340px; overflow-y: auto;">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="gvw_comp" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed"
                                    OnRowDataBound="comp_data_RowDataBound" DataKeyNames="prescription_id">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Item" ItemStyle-Width="35%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="prescription_comp_id" runat="server" Value='<%# Bind("prescription_id") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="prescription_comp_no" runat="server" Value='<%# Bind("prescription_no") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="item_comp_id" runat="server" Value='<%# Bind("item_id") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="item_comp_name" runat="server" Value='<%# Bind("item_name") %>'></asp:HiddenField>
                                                <asp:LinkButton Font-Size="12px" Font-Underline="true" Style="padding-left: 10px;" Font-Names="Helvetica, Arial, sans-serif" ID="compound_comp_name" runat="server" Text='<%# Bind("compound_name") %>' OnClick="CompDetail_onClick"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:TextBox Width="100%" Style="text-align: right; margin: 0px" Font-Size="11px" onkeypress="return CheckNumeric();" Font-Names="Helvetica, Arial, sans-serif" ID="quantity_comp" runat="server" Text='<%# Bind("quantity") %>' onkeydown="return txtOnKeyPressFalse();" onchange="validateFloatKeyPress(this);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UoM" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="uom_comp_id" runat="server" Value='<%# Bind("uom_id") %>'></asp:HiddenField>
                                                <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="uom_comp_code" runat="server" Text='<%# Bind("uom_code") %>' onkeydown="return txtOnKeyPressFalse();"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Frequency" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="frequency_comp_code" Style="margin: 0px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dose" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox Width="100%" Style="text-align: right" Font-Size="11px" onkeypress="return CheckNumeric();" Font-Names="Helvetica, Arial, sans-serif" ID="dosage_comp_id" runat="server" Text='<%# Bind("dosage_id") %>' onkeydown="return txtOnKeyPressFalse();" onchange="validateFloatKeyPress(this);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dose Text" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox Width="100%" CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="dose_comp_text" runat="server" Text='<%# Bind("dose_text") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dose & Instruction" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:TextBox Width="100%" Style="margin: 0px" MaxLength="100" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="remarks_comp" runat="server" Text='<%# Bind("remarks") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="administrationRouteCode_comp" Style="margin: 0px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Iter" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:TextBox Width="100%" Style="text-align: right; margin: 0px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="iteration_comp" runat="server" Text='<%# Bind("iteration") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Routine" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                            <ItemTemplate>
                                                <asp:CheckBox Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="is_routine_comp" runat="server" Checked='<%# Eval("is_routine").ToString() != "0" %>' onkeydown="return txtOnKeyPressFalse();"></asp:CheckBox>
                                                <asp:HiddenField ID="is_consumables_comp" runat="server" Value='<%# Bind("is_consumables") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="compound_comp_id" runat="server" Value='<%# Bind("compound_id") %>'></asp:HiddenField>
                                                
                                                <asp:HiddenField ID="origin_prescription_comp_id" runat="server" Value='<%# Bind("origin_prescription_id") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="hope_arinvoice_comp_id" runat="server" Value='<%# Bind("hope_arinvoice_id") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="is_delete_comp" runat="server" Value='<%# Bind("is_delete") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btndelete" runat="server" OnClick="btnDeleteheadercomp_Click"><span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_delete.svg") %>" class="ic_delete"></span></asp:LinkButton>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="gvw_compdetail" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed hiddencol"
                                    DataKeyNames="prescription_id" EmptyDataText="No Data">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item" ItemStyle-Width="10%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:Label ID="compound_compdtlhdn_name" runat="server" Text='<%# Bind("compound_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item" ItemStyle-Width="10%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="prescription_compdtlhdn_id" runat="server" Value='<%# Bind("prescription_id") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="prescription_compdtlhdn_no" runat="server" Value='<%# Bind("prescription_no") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="item_compdtlhdn_id" runat="server" Value='<%# Bind("item_id") %>'></asp:HiddenField>
                                               
                                                <asp:Label Font-Size="12px" Font-Underline="true" Font-Names="Helvetica, Arial, sans-serif" ID="item_compdtlhdn_name" runat="server" Text='<%# Bind("item_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" ItemStyle-Width="3%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:Label Width="100%" CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="quantity_compdtlhdn" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UoM" ItemStyle-Width="2%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="uom_compdtlhdn_id" runat="server" Value='<%# Bind("uom_id") %>'></asp:HiddenField>
                                                <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="uom_compdtlhdn_code" runat="server" Text='<%# Bind("uom_code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Frequency" ItemStyle-Width="8%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                               
                                                <asp:HiddenField ID="frequency_compdtlhdn_id" Value='<%# Bind("frequency_id") %>' runat="server"></asp:HiddenField>
                                                <asp:Label ID="frequency_compdtlhdn_code" Text='<%# Bind("frequency_code") %>' CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dose" ItemStyle-Width="2%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:Label Width="100%" CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="dosage_compdtlhdn_id" runat="server" Text='<%# Bind("dosage_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dose Text" ItemStyle-Width="6%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:Label Width="100%" CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="dose_compdtlhdn_text" runat="server" Text='<%# Bind("dose_text") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Instruction" ItemStyle-Width="10%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:Label Width="100%" CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="remarks_compdtlhdn" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route" ItemStyle-Width="7%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="administrationRouteid_compdtlhdn" Value='<%# Bind("administration_route_id") %>' runat="server"></asp:HiddenField>
                                                <asp:Label ID="administrationRouteCode_compdtlhdn" Text='<%# Bind("administration_route_code") %>' CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server"></asp:Label>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Iter" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:Label Width="100%" CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="iteration_compdtlhdn" runat="server" Text='<%# Bind("iteration") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Routine" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:CheckBox Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="is_routine_compdtlhdn" runat="server" Checked='<%# Eval("is_routine").ToString() != "0" %>'></asp:CheckBox>
                                                <asp:HiddenField ID="is_consumables_compdtlhdn" runat="server" Value='<%# Bind("is_consumables") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="compound_compdtlhdn_id" runat="server" Value='<%# Bind("compound_id") %>'></asp:HiddenField>
                                               
                                                <asp:HiddenField ID="origin_prescription_compdtlhdn_id" runat="server" Value='<%# Bind("origin_prescription_id") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="hope_arinvoice_compdtlhdn_id" runat="server" Value='<%# Bind("hope_arinvoice_id") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="is_delete_compdtlhdn" runat="server" Value='<%# Bind("is_delete") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <!-- ==================================================== end COMPOUND PRESCRIPTION =================================================== -->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>--%>

        <%-- ==================================================== RACIKAN PRESCRIPTION =================================================== --%>
        <div class="mini-dialog" style="margin-bottom: 10px;" id="dvRacikanShow" runat="server">
            <div class="mini-header">
                <%--<asp:UpdatePanel runat="server">
                    <ContentTemplate>--%>
                        <span class="anchor" id="racikan_begin"></span>
                        <table border="0">
                            <tr>
                                <td>
                                    <label id="lblbhs_racikan" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Compound Prescription</label>
                                    <a id="linkto_lblbhs_racikan" href="#racikan_begin" style="display: none;"></a>
                                </td>
                                <td>
                                    <%--<asp:UpdateProgress ID="UpdateProgress12" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
                                        <ProgressTemplate>
                                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                            </div>
                                            &nbsp;
                                            <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>--%>
                                </td>
                            </tr>
                        </table>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>

            <asp:UpdatePanel ID="UpdatePanel_gvw_racikan" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div>
                        <%--class="scrollEMR" style="max-height: 500px; overflow-y: auto;"--%>
                        <asp:GridView ID="gvw_racikan_header" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-header-racikan"
                            DataKeyNames="prescription_compound_header_id" OnRowDataBound="gvw_racikan_header_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="100%" HeaderStyle-BackColor="#f4f4f4" ItemStyle-CssClass="no-padding">
                                    <HeaderTemplate>
                                        <table style="width: 100%;">
                                            <tr style="font-weight: bold; background-color: #f4f4f4;">
                                                <td style="width: 25%; padding-left: 10px;">
                                                    <label id="lblbhs_racik_compoundname">Name</label></td>
                                                <%--<td style="width: 5%;">
                                                    <label id="lblbhs_racik_dose">Text</label></td>--%>
                                                <td style="width: 15%;">
                                                    <label id="lblbhs_racik_doseuom">Dose</label></td>
                                                <td style="width: 10%;">
                                                    <label id="lblbhs_racik_freq">Frequency</label></td>
                                                <td style="width: 10.5%;">
                                                    <label id="lblbhs_racik_route">Route</label></td>
                                                <td style="width: 15%;">
                                                    <label id="lblbhs_racik_instruction">Instruction</label></td>
                                                <td style="width: 5%;">
                                                    <label id="lblbhs_racik_qty">Qty</label></td>
                                                <td style="width: 5%;">
                                                    <label id="lblbhs_racik_uom">UoM</label></td>
                                                <td style="width: 5%;">
                                                    <label id="lblbhs_racik_iter">Iter</label></td>
                                                <td style="width: 10%; text-align: center;">
                                                    <label id="lblbhs_racik_action">Action</label></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="HF_headerid_racikan" runat="server" Value='<%# Bind("prescription_compound_header_id") %>' />
                                        <asp:HiddenField ID="HF_uomid_racikan" runat="server" Value='<%# Bind("uom_id") %>' />
                                        <asp:HiddenField ID="HF_doseuomid_racikan" runat="server" Value='<%# Bind("dose_uom_id") %>' />
                                        <asp:HiddenField ID="HF_freqid_racikan" runat="server" Value='<%# Bind("administration_frequency_id") %>' />
                                        <asp:HiddenField ID="HF_routeid_racikan" runat="server" Value='<%# Bind("administration_route_id") %>' />
                                        <asp:HiddenField ID="HF_isdosetext_racikan" runat="server" Value='<%# Bind("IsDoseText") %>' />
                                        <table style="width: 100%;" class="table-bordered table-condensed">
                                            <tr>
                                                <td style="width: 25%; font-weight: bold; padding-left: 15px;">
                                                    <div>
                                                        <asp:Label ID="lbl_nama_racikan" runat="server" Text='<%# Bind("compound_name") %>'></asp:Label>
                                                    </div>
                                                </td>
                                                <%--<td style="width: 5%; text-align: right;">
                                                </td> --%>   
                                                <td style="width: 15%;">
                                                    <asp:Label ID="lbl_dosis_racikan" runat="server" Text='<%# Bind("dose") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                    <asp:Label ID="lbl_dosisunit_racikan" runat="server" Text='<%# Bind("dose_uom") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                    <asp:Label id="lbl_dosistext_racikan" runat="server"  Text='<%# Bind("dose_text") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:inline" : "display:none" %>'></asp:Label>
                                                </td>
                                                <td style="width: 10%;">
                                                    <asp:Label ID="lbl_frekuensi_racikan" runat="server" Text='<%# Bind("frequency_code") %>'></asp:Label></td>
                                                <td style="width: 10%;">
                                                    <asp:Label ID="lbl_rute_racikan" runat="server" Text='<%# Bind("administration_route_code") %>'></asp:Label></td>
                                                <td style="width: 15%;">
                                                    <asp:Label ID="lbl_instruksi_racikan" runat="server" Text='<%# Bind("administration_instruction") %>'></asp:Label></td>
                                                <td style="width: 5%; text-align: right;">
                                                    <asp:Label ID="lbl_jml_racikan" runat="server" Text='<%# Bind("quantity") %>'></asp:Label></td>
                                                <td style="width: 5%;">
                                                    <asp:Label ID="lbl_unit_racikan" runat="server" Text='<%# Bind("uom_code") %>'></asp:Label></td>
                                                <td style="width: 5%; text-align: right;">
                                                    <asp:Label ID="lbl_iter_racikan" runat="server" Text='<%# Bind("iter") %>'></asp:Label></td>
                                                <td style="width: 10%; text-align: center;">
                                                    <%-- <asp:LinkButton ID="btnsaveRacikanHeader" runat="server" ToolTip="Save As Order Set"><span><i class="fa fa-save" style="font-size:17px;"></i> </span></asp:LinkButton>--%>
                                                    <div class="btn-group" style="vertical-align: initial;">
                                                        <asp:HyperLink ID="HyperLinkSaveOrderSetRacikan" runat="server" data-toggle="dropdown" aria-expanded="false" NavigateUrl="#" Style="font-weight: bold; text-decoration: underline;" onmouseup="klikSaveAsFocus('TextBoxSaveAsOrderSetName');" ToolTip="Save As Order Set"> <i class="fa fa-save" style="font-size:17px;"></i> </asp:HyperLink>
                                                        <ul class="dropdown-menu pop-shadow" style="padding: 8px;">
                                                            <li style="padding-bottom: 3px;">Input Order Set Name :
                                                            </li>
                                                            <li style="padding-bottom: 5px;">
                                                                <asp:TextBox ID="TextBoxSaveAsOrderSetNameRacikan" Text='<%# Bind("compound_name") %>' onkeydown="return txtOnKeyPressFalse();" onkeyup="checkSaveAsEmpty('TextBoxSaveAsOrderSetName','LinkSaveAsOrderSet');" runat="server"></asp:TextBox>
                                                            </li>
                                                            <li style="float: right;">
                                                                <asp:LinkButton ID="LinkSaveAsOrderSetRacikan" class="btn btn-default btn-sm" runat="server" Style="font-weight: bold; background-color: #228b22; color: white; width: 80px;" OnClick="LinkSaveAsOrderSetRacikan_Click">Save</asp:LinkButton>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    &nbsp;
                                                    <asp:LinkButton ID="btneditRacikanHeader" runat="server" OnClick="btneditRacikanHeader_Click" ToolTip="Edit"><span><i class="fa fa-pencil-square" style="font-size:18px;"></i> </span></asp:LinkButton>
                                                    &nbsp;
                                                    <asp:LinkButton ID="btndeleteRacikanHeader" runat="server" OnClick="btndeleteRacikanHeader_Click" ToolTip="Delete"><span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_delete.svg") %>" class="ic_delete" style="vertical-align:baseline;"></span></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>

                                        <div class="row" style="margin-left: 15px; margin-right: 15px; background-color: #f2f3f4; padding-top: 10px; padding-bottom: 10px;">
                                            <div class="col-sm-6" style="border-right: 1px dashed #d0d1d2;">
                                                <asp:GridView ID="gvw_racikan_detail" runat="server" AutoGenerateColumns="False" CssClass="table-detail-racikan"
                                                    DataKeyNames="prescription_compound_detail_id">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="50%" DataField="item_name" HeaderText="Item" ShowHeader="false" ItemStyle-VerticalAlign="Top" />
                                                        <asp:TemplateField ItemStyle-Width="15%" HeaderText="Dose" ShowHeader="false">
                                                            <ItemTemplate>
                                                                <asp:Label id="lbldose" runat="server"  Text='<%# Bind("dose") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                <asp:Label id="lbldoseuom" runat="server"  Text='<%# Bind("dose_uom_code") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                <asp:Label id="lbldosetext" runat="server"  Text='<%# Bind("dose_text") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:inline" : "display:none" %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                &nbsp;
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField ItemStyle-Width="10%" DataField="quantity" HeaderText="Qty" ShowHeader="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="text-right" Visible="false" />
                                                        <asp:TemplateField ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                &nbsp;
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField ItemStyle-Width="15%" DataField="uom_code" HeaderText="Unit" ShowHeader="false" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="text-left" Visible="false" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-6" style="border-left: 1px dashed #d0d1d2; margin-left: -1px">
                                                <label id="lblbhs_racik_note" style="font-weight: bold;">Instruksi Racikan Untuk Farmasi</label>
                                                <br />
                                                <asp:Label ID="lbl_instruksi_racikan_farmasi" runat="server" Text='<%# Eval("compound_note").ToString().Replace("\n","<br />") %>'></asp:Label>
                                                <asp:HiddenField ID="HF_lbl_instruksi_racikan_farmasi" runat="server" Value='<%# Bind("compound_note") %>' />
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <table>
                        <tr>
                            <td>
                                <asp:LinkButton ID="LB_tambahracikanbaru" runat="server" OnClick="LB_tambahracikanbaru_Click" class="btn btn-default btn-sm" Style="height: 25px; padding-top: 3px; margin-bottom: 10px; margin-top: 10px; margin-left: 15px;"> <i class="fa fa-plus-circle"></i> <label id="lblbhs_racik_addnew" style="cursor:pointer;">Tambah Racikan Baru</label> </asp:LinkButton>
                                <%--<a class="btn btn-default btn-sm" style="height: 25px; padding-top: 3px; margin-bottom: 10px; margin-left: 15px;" href="javascript:$('#modalInputRacikan').modal();">
                                    <i class="fa fa-plus-circle"></i> Tambah Racikan Baru</a>--%>
                            </td>
                            <td>
                                <asp:UpdateProgress ID="UpdateProgressRacikan" runat="server" AssociatedUpdatePanelID="UpdatePanel_gvw_racikan">
                                    <ProgressTemplate>
                                        <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                        </div>
                                        &nbsp;
                                        <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%-- ==================================================== END RACIKAN PRESCRIPTION =================================================== --%>

        <%-- ==================================================== CONSUMABLES PRESCRIPTION =================================================== --%>
        <div class="mini-dialog" style="margin-bottom: 10px;" id="dvConsumablesShow">
            <div class="mini-header">
                <%--<asp:UpdatePanel runat="server">
                    <ContentTemplate>--%>
                        <span class="anchor" id="consumables_begin"></span>
                        <table border="0">
                            <tr>
                                <td>
                                    <label id="lblbhs_consumables" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Consumables</label>
                                    <a id="linkto_lblbhs_consumables" href="#consumables_begin" style="display: none;"></a>
                                </td>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanelConsumable">
                                        <ProgressTemplate>
                                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                            </div>
                                            &nbsp;
                                            <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <div class="scrollEMR" style="max-height: 340px; overflow-y: auto;">
                <asp:UpdatePanel ID="UpdatePanelConsumable" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvw_consumables" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed"
                            DataKeyNames="prescription_id">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Item" ItemStyle-Width="35%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="prescription_id_cons" runat="server" Value='<%# Bind("prescription_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="prescription_no_cons" runat="server" Value='<%# Bind("prescription_no") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="frequency_code_cons" runat="server" Value='<%# Bind("frequency_code") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="frequency_id_cons" runat="server" Value='<%# Bind("frequency_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="dosage_id_cons" runat="server" Value='<%# Bind("dosage_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="dose_text_cons" runat="server" Value='<%# Bind("dose_text") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="administrationRouteCode_cons" runat="server" Value='<%# Bind("administration_route_code") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="administrationRouteId_cons" runat="server" Value='<%# Bind("administration_route_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("administration_route_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="is_routine_cons" runat="server" Value='<%# Bind("is_routine") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="is_consumables_cons" runat="server" Value='<%# Bind("is_consumables") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="compound_id_cons" runat="server" Value='<%# Bind("compound_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="compound_name_cons" runat="server" Value='<%# Bind("compound_name") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="origin_prescription_id_cons" runat="server" Value='<%# Bind("origin_prescription_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="hope_arinvoice_id_cons" runat="server" Value='<%# Bind("hope_arinvoice_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="is_delete_cons" runat="server" Value='<%# Bind("is_delete") %>'></asp:HiddenField>
                                        <%--<asp:Label id="prescription_id" runat="server" Text='<%# Bind("prescription_id") %>' Visible="false"><%# Eval("prescription_id").ToString() %></asp:Label>--%>
                                        <%--<asp:Label id="item_id" runat="server" Text='<%# Bind("item_id") %>' Visible="false"><%# Eval("item_id").ToString() %></asp:Label>--%>
                                        <asp:HiddenField ID="iteration_cons" runat="server" Value='<%# Bind("iteration") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="itemId_cons" runat="server" Value='<%# Bind("item_id") %>'></asp:HiddenField>
                                        <asp:Label Font-Size="11px" Style="padding-left: 10px;" Font-Names="Helvetica, Arial, sans-serif" ID="item_name_cons" runat="server" Text='<%# Bind("item_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" ItemStyle-CssClass="numberofGrid3" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:TextBox Width="100%" Style="text-align: right; margin: 0px" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="quantity_cons" runat="server" Text='<%# Bind("quantity") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UoM" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="uom_id_cons" runat="server" Value='<%# Bind("uom_id") %>'></asp:HiddenField>
                                        <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="uom_code_cons" runat="server" Text='<%# Bind("uom_code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Instruction" ItemStyle-Width="50%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:TextBox Width="100%" MaxLength="100" Style="margin: 0px; max-width: 100%; overflow: hidden;" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="remarks_cons" runat="server" Text='<%# Bind("remarks") %>' onkeydown="AutoExpand(this);" onkeyup="charLimitInstruction(this,100);" TextMode="MultiLine" Rows="1" onfocus="minexpand(this)" CssClass="text-multiline-dialog auto-expand"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btndelete" runat="server" OnClick="btnDeleteCons_Click"><span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_delete.svg") %>" class="ic_delete"></span></asp:LinkButton>
                                        <%--<asp:Button  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="X" OnClick="btnDelete_Click"></asp:Button>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <!-- kotak pencarian -->
            <div style="margin-top: 10px; margin-left: 15px; display: none;" runat="server" visible="false">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-3" style="width: 190px;">
                                <asp:TextBox runat="server" ID="TextBox2" Visible="false" ReadOnly="true" />
                                <div class="has-feedback" style="width: 180px;">
                                    <asp:TextBox runat="server" Width="180px" ID="txtitemcons" Placeholder="Add item here..." ReadOnly="true" onclick="OnClickcons()" />
                                    <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px; z-index: 0;"></span>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="upConsItem">
                                    <ProgressTemplate>
                                        <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                        </div>
                                        <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div id="popupcons" style="display: none; position: absolute; z-index: 1; border: 1px groove; min-width: 600px; min-height: 200px; background-color: white; max-height: 250px; max-width: 800px">
                    <asp:UpdatePanel runat="server" ID="upConsItem" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="margin: 5px;">
                                <asp:TextBox runat="server" ID="txtSearchItemcons" onkeydown="return txtOnKeyPressCons();"></asp:TextBox>
                                <asp:Button runat="server" CssClass="btn btn-warning btn-emr-small" ID="Button2" OnClick="btnFindCons_click" Text="Find" />
                            </div>
                            <div style="overflow-y: scroll; max-height: 200px; max-width: 800px; min-width: 600px; min-height: 200px;">
                                <asp:GridView ID="gvw_cons" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed" BorderColor="Black"
                                    HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="SalesItemId" EmptyDataText="No Data"
                                    AllowSorting="True">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="salesitemid" runat="server" Text='<%# Bind("SalesItemId") %>' Visible="false"><%# Eval("SalesItemId").ToString() %></asp:Label>
                                                <asp:LinkButton Font-Underline="true" Font-Size="12px" ID="salesItemName" runat="server" Text='<%# Bind("SalesItemName") %>' OnClick="itemselectedCons_onclick" OnClientClick="OnClickcons()"></asp:LinkButton>
                                                <asp:HiddenField ID="hfUomId" Value='<%# Bind("SalesUomId") %>' runat="server" />
                                                <asp:HiddenField ID="hfUomCode" Value='<%# Bind("SalesUomCode") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Active Ingredients" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="ActiveIngredientsName" SortExpression="ActiveIngredientsName"></asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="5%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="Qty" runat="server"> <span style="color: <%# Eval("TotalQuantity","{0:0.00}").ToString() == "0,00" ? "red" : "#333"%>; width: 65px"> <%# Eval("TotalQuantity","{0:#,##0.00}") %> </span> </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Quantity" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" DataField="TotalQuantity" DataFormatString="{0:0.00}" SortExpression="TotalQuantity"></asp:BoundField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <!-- end kotak pencarian -->

            <!-- kotak pencarian Autocomplete -->
            <div style="margin-top: 10px; margin-left: 15px;">
                <asp:UpdatePanel runat="server" ID="UP_SearchCons" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-3" style="width: 190px;">
                                <div class="has-feedback" style="width: 180px;">
                                    <%--<input type="text" ID="txtItemCons_AC" Placeholder="Add item here..." style="width:180px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" />--%>
                                    <asp:TextBox ID="txtItemCons_AC" runat="server" Placeholder="Add item here..." Style="width: 180px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('cons','true');"></asp:TextBox>
                                    <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px; z-index: 0;"></span>
                                </div>
                                <asp:HiddenField ID="HF_flagfocusconssearch" runat="server" />
                                <asp:HiddenField ID="HF_ItemSelectedcons" runat="server" />
                                <asp:Button ID="ButtonAjaxSearchCons" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchCons_Click" />
                            </div>
                            <div class="col-sm-3">
                                <div class="loadingcons" style="display: none;">
                                    <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                    </div>
                                    &nbsp;
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                    <asp:HiddenField ID="HFloadingcons" runat="server" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- end kotak pencarian -->
        </div>
        <%-- ==================================================== end CONSUMABLES PRESCRIPTION =================================================== --%>

        <%-- ==================================================== ADDITIONAL PRESCRIPTION =================================================== --%>
        <div class="mini-dialog" style="margin-bottom: 10px;" id="dvAdditionalShow">
            <div class="mini-header">
                <%--<asp:UpdatePanel runat="server">
                    <ContentTemplate>--%>
                        <span class="anchor" id="additional_prescription_begin"></span>
                        <table border="0">
                            <tr>
                                <td>
                                    <label id="lblbhs_additionaldrugsprescription" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Additional Drugs Prescription</label>
                                    <a id="linkto_lblbhs_additionaldrugsprescription" href="#additional_prescription_begin" style="display: none;"></a>
                                </td>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress7" runat="server" AssociatedUpdatePanelID="UpdatePanelListPrescription">
                                        <ProgressTemplate>
                                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                            </div>
                                            &nbsp;
                                            <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <div class="scrollEMR" style="max-height: 340px; overflow-y: auto;">
                <asp:UpdatePanel ID="UpdatePanelListPrescriptionAdd" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvwAdditionalDrugs" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed"
                            OnRowDataBound="drugsadditional_data_RowDataBound" DataKeyNames="prescription_id">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Item" ItemStyle-Width="30%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>

                                        <div style="display:table">
                                            <div style="display:none;">
                                                <asp:Label ID="LblIconMims" runat="server" Text="" Visible='<%# Eval("cims_result") == "" ? false : true %>'>
                                                    <a href="#" style='pointer-events:<%# Eval("cims_result") == "fa fa-check-circle m-safe-icon" ? "none" : "auto" %>;'> <i class='<%# Eval("cims_result") %>'></i> </a> 
                                                </asp:Label>
                                            </div>
                                            <div style="display:table-cell; padding-left:10px;">
                                                <asp:HiddenField ID="prescription_id" runat="server" Value='<%# Bind("prescription_id") %>'></asp:HiddenField>
                                                <asp:HiddenField ID="prescription_no" runat="server" Value='<%# Bind("prescription_no") %>'></asp:HiddenField>
                                                <%--<asp:Label id="prescription_id" runat="server" Text='<%# Bind("prescription_id") %>' Visible="false"><%# Eval("prescription_id").ToString() %></asp:Label>--%>
                                                <%--<asp:Label id="item_id" runat="server" Text='<%# Bind("item_id") %>' Visible="false"><%# Eval("item_id").ToString() %></asp:Label>--%>
                                                <asp:HiddenField ID="item_id" runat="server" Value='<%# Bind("item_id") %>'></asp:HiddenField>
                                                <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="item_name" runat="server" Text='<%# Bind("item_name") %>'></asp:Label>
                                            </div>
                                        </div>

                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Text" ItemStyle-CssClass="numberofGrid3" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: 4px;"  title="Use Dose Text">
                                            <asp:CheckBox ID="is_dosetext" runat="server" OnCheckedChanged="is_dosetext_add_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("IsDoseText") %>'></asp:CheckBox>
                                            <div class="state p-success">
                                                <i class="icon fa fa-check"></i>
                                                <label></label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dose" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <div runat="server" Visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                            <asp:TextBox Style="text-align: right; width:40px;" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="dosage_id" runat="server" Text='<%# Bind("dosage_id") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                            <asp:DropDownList ID="doseuom" Style="margin: 0px; width:60px;" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server"></asp:DropDownList>
                                        </div>
                                        <asp:TextBox  Width="100%" MaxLength="100" Font-Size="11px" Style="margin: 0px; overflow: hidden;" Font-Names="Helvetica, Arial, sans-serif" ID="dosetext" runat="server" Visible='<%# Eval("IsDoseText") %>' Text='<%# Bind("dose_text") %>' onkeydown="AutoExpand(this);" onkeyup="charLimitInstruction(this,100);" TextMode="MultiLine" Rows="1" onfocus="minexpand(this)" CssClass="text-multiline-dialog"></asp:TextBox>
                                        <asp:HiddenField ID="dose_text" runat="server" Value='<%# Bind("dose_text") %>'></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Frequency" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <%--<asp:TextBox Width="100%" CssClass="box"  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="frequency_code" runat="server"  Text='<%# Bind("frequency_code") %>'></asp:TextBox>--%>
                                        <asp:DropDownList ID="frequency_code" Style="margin: 0px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Route" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="administrationRouteCode" Style="margin: 0px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server"></asp:DropDownList>
                                        <%--<asp:TextBox Width="100%" CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="administrationRouteCode" runat="server"  Text='<%# Bind("administrationRouteCode") %>'></asp:TextBox>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Instruction" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:TextBox Width="100%" MaxLength="100" Font-Size="11px" Style="margin: 0px; overflow: hidden;" Font-Names="Helvetica, Arial, sans-serif" ID="remarks" runat="server" Text='<%# Bind("remarks") %>' onkeydown="AutoExpand(this);" onkeyup="charLimitInstruction(this,100);" TextMode="MultiLine" Rows="1" onfocus="minexpand(this)" CssClass="text-multiline-dialog auto-expand"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" ItemStyle-CssClass="numberofGrid3" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:TextBox Width="100%" Style="text-align: right; margin: 0px" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="quantity" runat="server" Text='<%# Bind("quantity") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UoM" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="uom_id" runat="server" Value='<%# Bind("uom_id") %>'></asp:HiddenField>
                                        <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="uom_code" runat="server" Text='<%# Bind("uom_code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Iter" ItemStyle-CssClass="numberofGrid1" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:TextBox Width="100%" Style="text-align: right; margin: 0px" onkeypress="return CheckNumeric();" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="iteration" runat="server" Text='<%# Bind("iteration") %>' onkeydown="return txtOnKeyPressFalse();" Enabled='<%# Eval("is_iter").ToString().ToUpper() == "TRUE"? true: false%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Routine" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: 4px;">
                                            <asp:CheckBox ID="is_routine" runat="server" Checked='<%# Eval("is_routine").ToString() != "0" %>'></asp:CheckBox>
                                            <div class="state p-success">
                                                <i class="icon fa fa-check"></i>
                                                <label></label>
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="is_consumables" runat="server" Value='<%# Bind("is_consumables") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="compound_id" runat="server" Value='<%# Bind("compound_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="compound_name" runat="server" Value='<%# Bind("compound_name") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="origin_prescription_id" runat="server" Value='<%# Bind("origin_prescription_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="hope_arinvoice_id" runat="server" Value='<%# Bind("hope_arinvoice_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="is_delete" runat="server" Value='<%# Bind("is_delete") %>'></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btndelete" runat="server" OnClick="btnDeleteAdditionalDrugs_Click"><span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_delete.svg") %>" class="ic_delete"></span></asp:LinkButton>
                                        <%--<asp:Button  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="X" OnClick="btnDelete_Click"></asp:Button>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <!-- kotak pencarian -->
            <div style="margin-top: 10px; margin-left: 15px; display: none;" runat="server" visible="false">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-3" style="width: 190px;">
                                <asp:TextBox runat="server" ID="txtAdditional" Visible="false" ReadOnly="true" />
                                <div class="has-feedback" style="width: 180px;">
                                    <asp:TextBox runat="server" Width="180px" ID="txtItemAddDrugs" Placeholder="Add item here..." ReadOnly="true" onclick="OnClickadditionalDrug()" />
                                    <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px; z-index: 0;"></span>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdateProgress ID="UpdateProgress8" runat="server" AssociatedUpdatePanelID="upAdditionalItem">
                                    <ProgressTemplate>
                                        <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                        </div>
                                        <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div id="popupadddrugs" style="display: none; position: absolute; z-index: 1; border: 1px groove; min-width: 600px; min-height: 200px; background-color: white; max-height: 250px; max-width: 800px">
                    <asp:UpdatePanel runat="server" ID="upAdditionalItem" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="margin: 5px;" class="row">
                                <div class="col-sm-6" style="padding-left: 0px;">
                                    <asp:TextBox runat="server" ID="txtSearchItemAddDrugs" onkeydown="return txtOnKeyPressDrugAdd();"></asp:TextBox>
                                    <asp:Button runat="server" CssClass="btn btn-warning btn-emr-small" ID="Button1" OnClick="btnFindAdditionalDrugs_click" Text="Find" />
                                </div>
                                <div class="col-sm-6 text-right">
                                    <label id="lblbhs_updatestockdrugadd" style="color: #c43d32; font-size: 14px; font-weight: bold;">
                                        Stok update per tanggal
                                        <asp:Label ID="Label_updatestockdrugadd" runat="server" Text="-"></asp:Label>
                                    </label>
                                </div>
                            </div>
                            <div style="overflow-y: scroll; max-height: 200px; max-width: 800px; min-width: 600px; min-height: 200px;">
                                <asp:GridView ID="gvw_add_drugs" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed" BorderColor="Black"
                                    HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="SalesItemId" EmptyDataText="No Data"
                                    AllowSorting="True">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="salesitemid" runat="server" Text='<%# Bind("SalesItemId") %>' Visible="false"><%# Eval("SalesItemId").ToString() %></asp:Label>
                                                <asp:LinkButton Font-Underline="true" Font-Size="12px" ID="salesItemName" runat="server" Text='<%# Bind("SalesItemName") %>' OnClick="itemselectedAdditionalDrug_onclick" OnClientClick="OnClickadditionalDrug()"></asp:LinkButton>
                                                <asp:HiddenField ID="hfUomId" Value='<%# Bind("SalesUomId") %>' runat="server" />
                                                <asp:HiddenField ID="hfUomCode" Value='<%# Bind("SalesUomCode") %>' runat="server" />
                                                <asp:HiddenField ID="hfDose" Value='<%# Bind("Dose") %>' runat="server" />
                                                <asp:HiddenField ID="hfDoseText" Value='<%# Bind("DoseText") %>' runat="server" />
                                                <asp:HiddenField ID="hfDoseUomId" Value='<%# Bind("DoseUomId") %>' runat="server" />
                                                <asp:HiddenField ID="hfAdministrationFrequencyId" Value='<%# Bind("AdministrationFrequencyId") %>' runat="server" />
                                                <asp:HiddenField ID="hfAdministrationRouteId" Value='<%# Bind("AdministrationRouteId") %>' runat="server" />
                                                <asp:HiddenField ID="hfAdministrationInstruction" Value='<%# Bind("AdministrationInstruction") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Active Ingredients" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="ActiveIngredientsName" SortExpression="ActiveIngredientsName"></asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="5%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="Qty" runat="server"> <span style="color: <%# Eval("TotalQuantity","{0:0.00}").ToString() == "0,00" ? "red" : "#333"%>; width: 65px"> <%# Eval("TotalQuantity","{0:#,##0.00}") %> </span> </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Quantity" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" DataField="TotalQuantity" DataFormatString="{0:0.00}" SortExpression="TotalQuantity"></asp:BoundField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <!-- end kotak pencarian -->

            <!-- kotak pencarian Autocomplete -->
            <div style="margin-top: 10px; margin-left: 15px;">
                <asp:UpdatePanel runat="server" ID="UP_SearchDrugAdd" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-3" style="width: 190px;">
                                <div class="has-feedback" style="width: 180px;">
                                    <%-- <input type="text" ID="txtItemAdd_AC_additional" Placeholder="Add item here..." style="width:180px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" />--%>
                                    <asp:TextBox ID="txtItemAdd_AC_additional" runat="server" Placeholder="Add item here..." Style="width: 180px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('drug_add','true');"></asp:TextBox>
                                    <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px; z-index: 0;"></span>
                                </div>
                                <asp:HiddenField ID="HF_flagfocusdrugsearch_add" runat="server" />
                                <asp:HiddenField ID="HF_ItemSelecteddrug_add" runat="server" />
                                <asp:Button ID="ButtonAjaxSearchDrug_add" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchDrug_add_Click" />
                            </div>
                            <div class="col-sm-3">
                                <div class="loadingdrugadd" style="display: none;">
                                    <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                    </div>
                                    &nbsp;
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                    <asp:HiddenField ID="HFloadingdrug_add" runat="server" />
                                </div>
                            </div>

                            <asp:Button ID="BtnCheckDrugInteraction_Add" runat="server" Text="Check Drug Interaction !" CssClass="btn btn-danger btn-sm" OnClick="BtnCheckDrugInteraction_Add_Click" style="background-color:#BA1000; color:white; float:right; margin-right: 40px; height: 25px; padding-top: 3px; display:none;" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- end kotak pencarian -->

            <%-- ====================================================Additional Prescription and Pharmacist Notes ============================================================ --%>
            <%--<asp:UpdatePanel runat="server">
                <ContentTemplate>--%>
                    <div class="box-border-soap" style="padding-bottom: 0px; margin-top: 10px;">
                        <div style="padding-left: 15px;">
                            <%--<label id="ENGadditionalprescriptionnotes" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px; display: <%=setENG%>;">Additional Prescription Notes</label>
                            <label id="INDadditionalprescriptionnotes" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px; display: <%=setIND%>;">Catatan Resep Tambahan</label>--%>
                            <label id="lblbhs_additionalprescriptionnotes" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Additional Prescription Notes</label>
                            <br />
                            <div class="scrollEMR" style="max-height: 90px; overflow-y: auto">
                                <asp:TextBox runat="server" CssClass="text-multiline-dialog" placeholder="Type here..." BorderColor="transparent" ID="txtAdditionalPresNotes" TextMode="MultiLine" Rows="1" onkeydown="AutoExpand(this)" onblur="minexpand(this)" />
                            </div>
                        </div>
                    </div>
                    <div class="box-border-soap" style="padding-bottom: 0px">
                        <div style="padding-left: 15px;">
                            <label id="lblbhs_additionalpharmacistnotes" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Additional Pharmacist Notes</label>
                            <br />
                            <div class="scrollEMR" style="max-height: 90px; overflow-y: auto">
                                <asp:TextBox runat="server" CssClass="text-multiline-dialog" placeholder="..." BorderColor="transparent" ID="txtAdditionalPharmacistNotes" TextMode="MultiLine" Rows="1" ReadOnly="true" onkeydown="AutoExpand(this)" onblur="minexpand(this)" />
                            </div>
                        </div>
                    </div>
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
            <%-- ==================================================== end Prescription Notes ============================================================ --%>
        </div>
        <%-- ==================================================== end ADDITIONAL PRESCRIPTION =================================================== --%>

        <%-- ==================================================== ADDITIONAL RACIKAN PRESCRIPTION =================================================== --%>
        <div class="mini-dialog" style="margin-bottom: 10px;" id="dvAdditionalRacikanShow" runat="server">
            <div class="mini-header">
                <%--<asp:UpdatePanel runat="server">
                    <ContentTemplate>--%>
                        <span class="anchor" id="additional_racikan_begin"></span>
                        <table border="0">
                            <tr>
                                <td>
                                    <label id="lblbhs_racikan_add" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Additional Compound Prescription</label>
                                    <a id="linkto_lblbhs_racikan_add" href="#additional_racikan_begin" style="display: none;"></a>
                                </td>
                                <td>
                                    <%--<asp:UpdateProgress ID="UpdateProgress12" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
                                        <ProgressTemplate>
                                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                            </div>
                                            &nbsp;
                                            <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>--%>
                                </td>
                            </tr>
                        </table>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>

            <asp:UpdatePanel ID="UpdatePanel_gvw_racikan_add" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div>
                        <%--class="scrollEMR" style="max-height: 500px; overflow-y: auto;"--%>
                        <asp:GridView ID="gvw_racikan_header_add" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-header-racikan"
                            DataKeyNames="prescription_compound_header_id" OnRowDataBound="gvw_racikan_header_add_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="100%" HeaderStyle-BackColor="#f4f4f4" ItemStyle-CssClass="no-padding">
                                    <HeaderTemplate>
                                        <table style="width: 100%;">
                                            <tr style="font-weight: bold; background-color: #f4f4f4;">
                                                <td style="width: 25%; padding-left: 10px;">
                                                    <label id="lblbhs_racik_compoundname_add">Name</label></td>
                                                <%--<td style="width: 5%;">
                                                    <label id="lblbhs_racik_dose_add">Teks</label></td>--%>
                                                <td style="width: 15%;">
                                                    <label id="lblbhs_racik_doseuom_add">Dose</label></td>
                                                <td style="width: 10%;">
                                                    <label id="lblbhs_racik_freq_add">Frequency</label></td>
                                                <td style="width: 10.5%;">
                                                    <label id="lblbhs_racik_route_add">Route</label></td>
                                                <td style="width: 15%;">
                                                    <label id="lblbhs_racik_instruction_add">Instruction</label></td>
                                                <td style="width: 5%;">
                                                    <label id="lblbhs_racik_qty_add">Qty</label></td>
                                                <td style="width: 5%;">
                                                    <label id="lblbhs_racik_uom_add">UoM</label></td>
                                                <td style="width: 5%;">
                                                    <label id="lblbhs_racik_iter_add">Iter</label></td>
                                                <td style="width: 10%; text-align: center;">
                                                    <label id="lblbhs_racik_action_add">Action</label></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="HF_headerid_racikan" runat="server" Value='<%# Bind("prescription_compound_header_id") %>' />
                                        <asp:HiddenField ID="HF_uomid_racikan" runat="server" Value='<%# Bind("uom_id") %>' />
                                        <asp:HiddenField ID="HF_doseuomid_racikan" runat="server" Value='<%# Bind("dose_uom_id") %>' />
                                        <asp:HiddenField ID="HF_freqid_racikan" runat="server" Value='<%# Bind("administration_frequency_id") %>' />
                                        <asp:HiddenField ID="HF_routeid_racikan" runat="server" Value='<%# Bind("administration_route_id") %>' />
                                        <asp:HiddenField ID="HF_isdosetext_racikan" runat="server" Value='<%# Bind("IsDoseText") %>' />
                                        <table style="width: 100%;" class="table-bordered table-condensed">
                                            <tr>
                                                <td style="width: 25%; font-weight: bold; padding-left: 15px;">
                                                    <div>
                                                        <asp:Label ID="lbl_nama_racikan" runat="server" Text='<%# Bind("compound_name") %>'></asp:Label>
                                                    </div>
                                                </td>
                                                <%--<td style="width: 5%; text-align: right;">
                                                </td> --%>   
                                                <td style="width: 15%;">
                                                    <asp:Label ID="lbl_dosis_racikan" runat="server" Text='<%# Bind("dose") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                    <asp:Label ID="lbl_dosisunit_racikan" runat="server" Text='<%# Bind("dose_uom") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                    <asp:Label id="lbl_dosistext_racikan" runat="server"  Text='<%# Bind("dose_text") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:inline" : "display:none" %>'></asp:Label>
                                                </td>
                                                <td style="width: 10%;">
                                                    <asp:Label ID="lbl_frekuensi_racikan" runat="server" Text='<%# Bind("frequency_code") %>'></asp:Label></td>
                                                <td style="width: 10%;">
                                                    <asp:Label ID="lbl_rute_racikan" runat="server" Text='<%# Bind("administration_route_code") %>'></asp:Label></td>
                                                <td style="width: 15%;">
                                                    <asp:Label ID="lbl_instruksi_racikan" runat="server" Text='<%# Bind("administration_instruction") %>'></asp:Label></td>
                                                <td style="width: 5%; text-align: right;">
                                                    <asp:Label ID="lbl_jml_racikan" runat="server" Text='<%# Bind("quantity") %>'></asp:Label></td>
                                                <td style="width: 5%;">
                                                    <asp:Label ID="lbl_unit_racikan" runat="server" Text='<%# Bind("uom_code") %>'></asp:Label></td>
                                                <td style="width: 5%; text-align: right;">
                                                    <asp:Label ID="lbl_iter_racikan" runat="server" Text='<%# Bind("iter") %>'></asp:Label></td>
                                                <td style="width: 10%; text-align: center;">
                                                    <div class="btn-group" style="vertical-align: initial;">
                                                        <asp:HyperLink ID="HyperLinkSaveOrderSetRacikanAdd" runat="server" data-toggle="dropdown" aria-expanded="false" NavigateUrl="#" Style="font-weight: bold; text-decoration: underline;" onmouseup="klikSaveAsFocus('TextBoxSaveAsOrderSetName');" ToolTip="Save As Order Set"> <i class="fa fa-save" style="font-size:17px;"></i> </asp:HyperLink>
                                                        <ul class="dropdown-menu pop-shadow" style="padding: 8px;">
                                                            <li style="padding-bottom: 3px;">Input Order Set Name :
                                                            </li>
                                                            <li style="padding-bottom: 5px;">
                                                                <asp:TextBox ID="TextBoxSaveAsOrderSetNameRacikanAdd" Text='<%# Bind("compound_name") %>' onkeydown="return txtOnKeyPressFalse();" onkeyup="checkSaveAsEmpty('TextBoxSaveAsOrderSetName','LinkSaveAsOrderSet');" runat="server"></asp:TextBox>
                                                            </li>
                                                            <li style="float: right;">
                                                                <asp:LinkButton ID="LinkSaveAsOrderSetRacikanAdd" class="btn btn-default btn-sm" runat="server" Style="font-weight: bold; background-color: #228b22; color: white; width: 80px;" OnClick="LinkSaveAsOrderSetRacikanAdd_Click">Save</asp:LinkButton>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    &nbsp;
                                                    <asp:LinkButton ID="btneditRacikanHeader_add" runat="server" OnClick="btneditRacikanHeader_add_Click" ToolTip="Edit"><span><i class="fa fa-pencil-square" style="font-size:18px;"></i> </span></asp:LinkButton>
                                                    &nbsp;
                                                    <asp:LinkButton ID="btndeleteRacikanHeader_add" runat="server" OnClick="btndeleteRacikanHeader_add_Click" ToolTip="Delete"><span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_delete.svg") %>" class="ic_delete" style="vertical-align:baseline;"></span></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="row" style="margin-left: 15px; margin-right: 15px; background-color: #f2f3f4; padding-top: 10px; padding-bottom: 10px;">
                                            <div class="col-sm-6" style="border-right: 1px dashed #d0d1d2;">
                                                <asp:GridView ID="gvw_racikan_detail_add" runat="server" AutoGenerateColumns="False" CssClass="table-detail-racikan"
                                                    DataKeyNames="prescription_compound_detail_id">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="50%" DataField="item_name" HeaderText="Item" ShowHeader="false" />
                                                        <asp:TemplateField ItemStyle-Width="15%" HeaderText="Dose" ShowHeader="false">
                                                            <ItemTemplate>
                                                                <asp:Label id="lbldose" runat="server"  Text='<%# Bind("dose") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                <asp:Label id="lbldoseuom" runat="server"  Text='<%# Bind("dose_uom_code") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                <asp:Label id="lbldosetext" runat="server"  Text='<%# Bind("dose_text") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:inline" : "display:none" %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                &nbsp;
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField ItemStyle-Width="10%" DataField="quantity" HeaderText="Qty" ShowHeader="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="text-right" Visible="false" />
                                                        <asp:TemplateField ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                &nbsp;
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField ItemStyle-Width="15%" DataField="uom_code" HeaderText="Unit" ShowHeader="false" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="text-left" Visible="false" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-6" style="border-left: 1px dashed #d0d1d2; margin-left: -1px">
                                                <label id="lblbhs_racik_note_add" style="font-weight: bold;">Instruksi Racikan Untuk Farmasi</label>
                                                <br />
                                                <asp:Label ID="lbl_instruksi_racikan_farmasi" runat="server" Text='<%# Eval("compound_note").ToString().Replace("\n","<br />") %>'></asp:Label>
                                                <asp:HiddenField ID="HF_lbl_instruksi_racikan_farmasi" runat="server" Value='<%# Bind("compound_note") %>' />
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <table>
                        <tr>
                            <td>
                                <asp:LinkButton ID="LB_tambahracikanbaru_add" runat="server" OnClick="LB_tambahracikanbaru_add_Click" class="btn btn-default btn-sm" Style="height: 25px; padding-top: 3px; margin-bottom: 10px; margin-top: 10px; margin-left: 15px;"> <i class="fa fa-plus-circle"></i> <label id="lblbhs_racik_addnew_add" style="cursor:pointer;">Tambah Racikan Baru</label> </asp:LinkButton>
                                <%--<a class="btn btn-default btn-sm" style="height: 25px; padding-top: 3px; margin-bottom: 10px; margin-left: 15px;" href="javascript:$('#modalInputRacikan').modal();">
                                <i class="fa fa-plus-circle"></i> Tambah Racikan Baru</a>--%>
                            </td>
                            <td>
                                <asp:UpdateProgress ID="UpdateProgressRacikan_add" runat="server" AssociatedUpdatePanelID="UpdatePanel_gvw_racikan_add">
                                    <ProgressTemplate>
                                        <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                        </div>
                                        &nbsp;
                                        <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%-- ==================================================== ADDITIONAL END RACIKAN PRESCRIPTION =================================================== --%>

        <%-- ==================================================== ADDITIONAL CONSUMABLES PRESCRIPTION =================================================== --%>
        <div class="mini-dialog" style="margin-bottom: 10px;" id="dvAddConsumablesShow">
            <div class="mini-header">
                <%--<asp:UpdatePanel runat="server">
                    <ContentTemplate>--%>
                        <span class="anchor" id="additional_consumables_begin"></span>
                        <table border="0">
                            <tr>
                                <td>
                                    <label id="lblbhs_additionalconsumables" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Additional Consumables</label>
                                    <a id="linkto_lblbhs_additionalconsumables" href="#additional_consumables_begin" style="display: none;"></a>
                                </td>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanelConsumableAdd">
                                        <ProgressTemplate>
                                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                            </div>
                                            &nbsp;
                                            <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <div class="scrollEMR" style="max-height: 340px; overflow-y: auto;">
                <asp:UpdatePanel ID="UpdatePanelConsumableAdd" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvw_add_cons" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed"
                            DataKeyNames="prescription_id">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Item" ItemStyle-Width="35%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="prescription_id_cons" runat="server" Value='<%# Bind("prescription_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="prescription_no_cons" runat="server" Value='<%# Bind("prescription_no") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="frequency_code_cons" runat="server" Value='<%# Bind("frequency_code") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="frequency_id_cons" runat="server" Value='<%# Bind("frequency_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="dosage_id_cons" runat="server" Value='<%# Bind("dosage_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="dose_text_cons" runat="server" Value='<%# Bind("dose_text") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="administrationRouteCode_cons" runat="server" Value='<%# Bind("administration_route_code") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="administrationRouteId_cons" runat="server" Value='<%# Bind("administration_route_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("administration_route_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="is_routine_cons" runat="server" Value='<%# Bind("is_routine") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="is_consumables_cons" runat="server" Value='<%# Bind("is_consumables") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="compound_id_cons" runat="server" Value='<%# Bind("compound_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="compound_name_cons" runat="server" Value='<%# Bind("compound_name") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="origin_prescription_id_cons" runat="server" Value='<%# Bind("origin_prescription_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="hope_arinvoice_id_cons" runat="server" Value='<%# Bind("hope_arinvoice_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="is_delete_cons" runat="server" Value='<%# Bind("is_delete") %>'></asp:HiddenField>
                                        <%--<asp:Label id="prescription_id" runat="server" Text='<%# Bind("prescription_id") %>' Visible="false"><%# Eval("prescription_id").ToString() %></asp:Label>--%>
                                        <%--<asp:Label id="item_id" runat="server" Text='<%# Bind("item_id") %>' Visible="false"><%# Eval("item_id").ToString() %></asp:Label>--%>
                                        <asp:HiddenField ID="iteration_cons" runat="server" Value='<%# Bind("iteration") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="itemId_cons" runat="server" Value='<%# Bind("item_id") %>'></asp:HiddenField>
                                        <asp:Label Font-Size="11px" Style="padding-left: 10px;" Font-Names="Helvetica, Arial, sans-serif" ID="item_name_cons" runat="server" Text='<%# Bind("item_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" ItemStyle-CssClass="numberofGrid3" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:TextBox Width="100%" Style="text-align: right; margin: 0px" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="quantity_cons" runat="server" Text='<%# Bind("quantity") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UoM" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="uom_id_cons" runat="server" Value='<%# Bind("uom_id") %>'></asp:HiddenField>
                                        <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="uom_code_cons" runat="server" Text='<%# Bind("uom_code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Instruction" ItemStyle-Width="50%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:TextBox Width="100%" MaxLength="100" Style="margin: 0px; max-width: 100%; overflow: hidden;" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="remarks_cons" runat="server" Text='<%# Bind("remarks") %>' onkeydown="AutoExpand(this);" onkeyup="charLimitInstruction(this,100);" TextMode="MultiLine" Rows="1" onfocus="minexpand(this)" CssClass="text-multiline-dialog auto-expand"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btndelete" runat="server" OnClick="btnDeleteConsAdd_Click"><span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_delete.svg") %>" class="ic_delete"></span></asp:LinkButton>
                                        <%--<asp:Button  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="X" OnClick="btnDelete_Click"></asp:Button>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <!-- kotak pencarian -->
            <div style="margin-top: 10px; margin-left: 15px; display: none;" runat="server" visible="false">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-3" style="width: 190px;">
                                <asp:TextBox runat="server" ID="TextBox1" Visible="false" ReadOnly="true" />
                                <div class="has-feedback" style="width: 180px;">
                                    <asp:TextBox runat="server" Width="180px" ID="txtItemAddCons" Placeholder="Add item here..." ReadOnly="true" onclick="OnClickadditionalCons()" />
                                    <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px; z-index: 0;"></span>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdateProgress ID="UpdateProgress10" runat="server" AssociatedUpdatePanelID="upConsAdd">
                                    <ProgressTemplate>
                                        <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                        </div>
                                        <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div id="popupconsadd" style="display: none; position: absolute; z-index: 1; border: 1px groove; min-width: 600px; min-height: 200px; background-color: white; max-height: 250px; max-width: 800px">
                    <asp:UpdatePanel runat="server" ID="upConsAdd" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="margin: 5px;">
                                <asp:TextBox runat="server" ID="txtSearchAddItemCons" onkeydown="return txtOnKeyPressConsAdd();"></asp:TextBox>
                                <asp:Button runat="server" CssClass="btn btn-warning btn-emr-small" ID="Button3" OnClick="btnFindConsAdd_click" Text="Find" />
                            </div>
                            <div style="overflow-y: scroll; max-height: 200px; max-width: 800px; min-width: 600px; min-height: 200px;">
                                <asp:GridView ID="gvw_item_cons_additional" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed" BorderColor="Black"
                                    HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="SalesItemId" EmptyDataText="No Data"
                                    AllowSorting="True">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="salesitemid" runat="server" Text='<%# Bind("SalesItemId") %>' Visible="false"><%# Eval("SalesItemId").ToString() %></asp:Label>
                                                <asp:LinkButton Font-Underline="true" Font-Size="12px" ID="salesItemName" runat="server" Text='<%# Bind("SalesItemName") %>' OnClick="itemselectedConsAdd_onclick" OnClientClick="OnClickadditionalCons()"></asp:LinkButton>
                                                <asp:HiddenField ID="hfUomId" Value='<%# Bind("SalesUomId") %>' runat="server" />
                                                <asp:HiddenField ID="hfUomCode" Value='<%# Bind("SalesUomCode") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Active Ingredients" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="ActiveIngredientsName" SortExpression="ActiveIngredientsName"></asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="5%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="Qty" runat="server"> <span style="color: <%# Eval("TotalQuantity","{0:0.00}").ToString() == "0,00" ? "red" : "#333"%>; width: 65px"> <%# Eval("TotalQuantity","{0:#,##0.00}") %> </span> </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Quantity" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" DataField="TotalQuantity" DataFormatString="{0:0.00}" SortExpression="TotalQuantity"></asp:BoundField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <!-- end kotak pencarian -->

            <!-- kotak pencarian Autocomplete -->
            <div style="margin-top: 10px; margin-left: 15px;">
                <asp:UpdatePanel runat="server" ID="UP_SearchConsAdd" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-3" style="width: 190px;">
                                <div class="has-feedback" style="width: 180px;">
                                    <%--<input type="text" ID="txtItemCons_AC_additional" Placeholder="Add item here..." style="width:180px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" />--%>
                                    <asp:TextBox ID="txtItemCons_AC_additional" runat="server" Placeholder="Add item here..." Style="width: 180px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('cons_add','true');"></asp:TextBox>
                                    <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px; z-index: 0;"></span>
                                </div>
                                <asp:HiddenField ID="HF_flagfocusconssearch_add" runat="server" />
                                <asp:HiddenField ID="HF_ItemSelectedcons_add" runat="server" />
                                <asp:Button ID="ButtonAjaxSearchCons_add" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchCons_add_Click" />
                            </div>
                            <div class="col-sm-3">
                                <div class="loadingconsadd" style="display: none;">
                                    <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                    </div>
                                    &nbsp;
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                    <asp:HiddenField ID="HFloadingcons_add" runat="server" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- end kotak pencarian -->
        </div>
        <%-- ==================================================== end ADDITIONAL CONSUMABLES PRESCRIPTION =================================================== --%>
    </div>

    <div class="col-sm-3">
        <!-- style="position: sticky; top: 165px; overflow: auto; max-height: calc(100vh - 175px);" -->
        <div class="mini-dialog">
            <div class="mini-header" style="font-size: 14px;">
                <%--<asp:UpdatePanel runat="server">
                    <ContentTemplate>--%>
                        <asp:HiddenField ID="HiddenFlagTabSet" runat="server" />
                        <table border="0" style="display: inline;">
                            <tr>
                                <td>
                                    <label id="btnlabset" style="background-color: transparent; border: 0px; font-weight: bold; cursor: pointer; margin-right:15px">Lab Set  </label>
                                    <br />
                                    <asp:CheckBox ID="chk_isLabsetFO" runat="server" Text="Future Order" Font-Size="12px" CssClass="mycheckbox" />
                                    <asp:HiddenField ID="HF_LabsetFO" runat="server" Value="false" />
                                </td>
                                <td style="text-align: right;">
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanelLabSet">
                                        <ProgressTemplate>
                                            &nbsp;
                                        <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>

                        <div style="display: inline; float: right;">
                            <asp:TextBox ID="Search_TextLabSet_new" name="Search_TextLabSet_new" runat="server" placeholder="Search..." AutoCompleteType="Disabled" onkeydown="return txtOnKeyPressFalse();" onkeyup="Search_Gridview(this, 'gvw_labset')" Style="width: 100px; border-top: 0px; border-right: 0px; border-left: 0px; outline: 0px; font-weight: normal;"></asp:TextBox>
                            <span class="fa fa-search" style="padding-right: 10px; color: darkgrey; margin-left: -20px;"></span>
                        </div>

                        <%-- <table border="0">
                            <tr>
                                <td style="width:75px;">
                                    <div id="divlab" onclick="labonclick();" style="text-align: center; padding-top: 6px; height:30px; background-color: #1a2269; border-radius: 6px 6px 0px 0px; color: white;">
                                        <label id="btnlabset" style="background-color: transparent; border: 0px; font-weight: bold; cursor: pointer;">Lab Set</label>
                                    </div>
                                </td>
                                <td style="width:75px;">
                                    <div id="divpanel" onclick="labpanelonclick();" style="text-align: center; padding-top: 6px; height:30px; background-color: white; border-radius: 6px 6px 0px 0px;">
                                        <label id="btnpanelset" style="background-color: transparent; border: 0px; font-weight: bold; cursor: pointer;">Panel Set</label>
                                    </div>
                                </td>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                        <ProgressTemplate>
                                            <div class="modal-backdrop" style="background-color:white; opacity: 0; text-align: center">
                                            </div>
                                            &nbsp;
                                            <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:UpdateProgress ID="UpdateProgress14" runat="server" AssociatedUpdatePanelID="UpPanelSet">
                                        <ProgressTemplate>
                                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                            </div>
                                            &nbsp;
                                            <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>--%>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <div class="scrollEMR" id="divlabset" style="max-height: 250px; overflow-y: auto;">
                <asp:UpdatePanel ID="UpdatePanelLabSet" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvw_labset" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed" BorderColor="Black"
                            HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                            ShowHeaderWhenEmpty="True" DataKeyNames="id" EmptyDataText="No Data" ShowHeader="false"
                            AllowSorting="True">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:BoundField DataField="set_name" HeaderText="set_name" Visible="false" />
                                <asp:TemplateField HeaderText="" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton CssClass="stylink" ID="setname_lab" runat="server" OnClick="btnLabSet_onClick">
                                            <div style="padding-left: 10px;">
                                                <b>
                                                    <asp:Label ID="Label_setname" runat="server" Text='<%# Bind("set_name") %>'></asp:Label>
                                                </b>
                                                <br />
                                                <asp:Label ID="itemlist_lab" runat="server" Text='<%# Bind("item_list") %>' Font-Size="9px" Font-Names="Helvetica, Arial, sans-serif"></asp:Label>
                                            </div>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="activeIngredientsName" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="activeIngredientsName"  SortExpression="activeIngredientsName"></asp:BoundField>
                                <asp:BoundField HeaderText="totalQuantity" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" DataField="totalQuantity"  SortExpression="totalQuantity"></asp:BoundField>--%>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="scrollEMR" id="divpanelset" style="max-height: 250px; overflow-y: auto; display: none">
                <asp:UpdatePanel ID="UpPanelSet" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvw_panelset" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed" BorderColor="Black"
                            HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                            ShowHeaderWhenEmpty="True" DataKeyNames="id" EmptyDataText="No Data" ShowHeader="false"
                            AllowSorting="True">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:BoundField DataField="set_name" HeaderText="set_name" Visible="false" />
                                <asp:TemplateField HeaderText="" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton CssClass="stylink" ID="setname_lab" runat="server" OnClick="btnPanelSet_onClick">
                                            <div style="padding-left: 10px;">
                                                <b>
                                                    <asp:Label ID="Label_setname" runat="server" Text='<%# Bind("set_name") %>'></asp:Label>
                                                </b>
                                                <br />
                                                <asp:Label ID="itemlist_lab" runat="server" Text='<%# Bind("item_list") %>' Font-Size="9px" Font-Names="Helvetica, Arial, sans-serif"></asp:Label>
                                            </div>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="activeIngredientsName" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="activeIngredientsName"  SortExpression="activeIngredientsName"></asp:BoundField>
                                <asp:BoundField HeaderText="totalQuantity" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" DataField="totalQuantity"  SortExpression="totalQuantity"></asp:BoundField>--%>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="mini-dialog" style="margin-top: 10px;">
            <div class="mini-header">
                <table border="0">
                    <tr>
                        <td>
                            <label id="lblbhs_frequentlyuseddrugs" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Frequently Used Drugs</label>
                        </td>
                        <td>
                            <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="upError3">
                                <ProgressTemplate>
                                    <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                    </div>
                                    &nbsp;
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="scrollEMR">
                <asp:UpdatePanel ID="upError3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvw_frequent_drugs" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed" BorderColor="Black"
                            HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                            ShowHeaderWhenEmpty="True" DataKeyNames="salesItemId" EmptyDataText="No Data" ShowHeader="false"
                            AllowSorting="True">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton CssClass="stylink" ID="frequentdrugs_name" runat="server" OnClick="frequentdrugs_onclick">
                                            <div style="padding-left: 10px;">
                                                <asp:Label ID="Label_frequentdrugs_name" runat="server" Text='<%# Bind("salesItemName") %>'></asp:Label>
                                                <br />
                                                <asp:HiddenField ID="hffrequentdrugs_id" Value='<%# Bind("salesItemId") %>' runat="server" />
                                                <asp:HiddenField ID="hfuom_id" Value='<%# Bind("uom_id") %>' runat="server" />
                                                <asp:HiddenField ID="hfuom_code" Value='<%# Bind("uom_code") %>' runat="server" />
                                                <asp:HiddenField ID="hfDose" Value='<%# Bind("Dose") %>' runat="server" />
                                                <asp:HiddenField ID="hfDoseText" Value='<%# Bind("DoseText") %>' runat="server" />
                                                <asp:HiddenField ID="hfDoseUomId" Value='<%# Bind("DoseUomId") %>' runat="server" />
                                                <asp:HiddenField ID="hfAdministrationFrequencyId" Value='<%# Bind("AdministrationFrequencyId") %>' runat="server" />
                                                <asp:HiddenField ID="hfAdministrationRouteId" Value='<%# Bind("AdministrationRouteId") %>' runat="server" />
                                                <asp:HiddenField ID="hfAdministrationInstruction" Value='<%# Bind("AdministrationInstruction") %>' runat="server" />
                                                <asp:HiddenField ID="hfis_iter" Value='<%# Bind("is_iter") %>' runat="server" />

                                                <%--<asp:Label id="itemlist" runat="server" Text='<%# Bind("item_list") %>' Font-Size="9px" Font-Names="Helvetica, Arial, sans-serif"></asp:Label>--%>
                                            </div>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="activeIngredientsName" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="activeIngredientsName"  SortExpression="activeIngredientsName"></asp:BoundField>
                                <asp:BoundField HeaderText="totalQuantity" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" DataField="totalQuantity"  SortExpression="totalQuantity"></asp:BoundField>--%>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="mini-dialog" style="margin-top: 10px; margin-bottom: 10px;">
            <div class="mini-header">
                <table border="0" style="display: inline;">
                    <tr>
                        <td>
                            <label id="lblbhs_orderset" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 14px;">Order Set</label>
                        </td>
                        <td style="text-align: right;">
                            <asp:UpdateProgress ID="UpdateProgress11" runat="server" AssociatedUpdatePanelID="UpdatePanelOrderSet">
                                <ProgressTemplate>
                                    <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                    </div>
                                    &nbsp;
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
                <div style="display: inline; float: right;">
                    <asp:TextBox ID="Search_TextOrderSet_new" name="Search_TextOrderSet_new" runat="server" placeholder="Search..." AutoCompleteType="Disabled" onkeydown="return txtOnKeyPressFalse();" onkeyup="Search_Gridview(this, 'gvw_orderset')" Style="width: 100px; border-top: 0px; border-right: 0px; border-left: 0px; outline: 0px; font-weight: normal;"></asp:TextBox>
                    <span class="fa fa-search" style="padding-right: 10px; color: darkgrey; margin-left: -20px;"></span>
                </div>
            </div>
            <div class="scrollEMR" style="max-height: 250px; overflow-y: auto;">
                <asp:UpdatePanel ID="UpdatePanelOrderSet" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvw_orderset" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed" BorderColor="Black"
                            HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                            ShowHeaderWhenEmpty="True" DataKeyNames="id" EmptyDataText="No Data" ShowHeader="false"
                            AllowSorting="True">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:BoundField DataField="set_name" HeaderText="set_name" Visible="false" />
                                <asp:TemplateField HeaderText="" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton CssClass="stylink" Font-Size="12px" ID="setname" runat="server" OnClick="orderset_onclick">
                                            <div style="padding-left: 10px;">
                                                <b>
                                                    <asp:Label ID="Label_setname" runat="server" Text='<%# Bind("set_name") %>'></asp:Label></b>
                                                <br />
                                                <asp:Label ID="itemlist" runat="server" Text='<%# Bind("item_list") %>' Font-Size="9px" Font-Names="Helvetica, Arial, sans-serif"></asp:Label>
                                            </div>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="activeIngredientsName" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="activeIngredientsName"  SortExpression="activeIngredientsName"></asp:BoundField>
                                <asp:BoundField HeaderText="totalQuantity" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" DataField="totalQuantity"  SortExpression="totalQuantity"></asp:BoundField>--%>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>
<%-- ============================================= FREQUENTLY USED DRUNGS & ORDER SET HEADER ============================================== --%>
<%-- ============================================= END FREQUENTLY USED DRUNGS & ORDER SET HEADER ============================================== --%>

<%-- ============================================= MODAL EXCLUDED DRUGS ============================================== --%>
<div class="modal fade" id="modaluomchangedrugs" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <asp:UpdatePanel ID="UpdatePanelUomChange" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog" style="position: fixed; top: 25%; left:0; right:0; width: 30%;" runat="server" id="dialogDrugsUomChange">
                <div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h5 class="modal-title">
                            <asp:Label ID="LabelTitleUomChange" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 16px;" runat="server" Text="Change Quantity Of Drugs"></asp:Label></h5>
                    </div>
                    <div class="modal-body">

                        <label style="font-size:14px;">Please change/recalculate the drug Quantity below : </labe>
                        <br />
                        <br />
                        <ul style="padding-left: 15px;">
                            <asp:Repeater runat="server" ID="RepeaterDrugsUomChange">
                                <ItemTemplate>
                                    <li>
                                        <div style="font-size:14px; font-family:Helvetica; padding-bottom:10px;">
                                            <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("item_name") %>'/> - 
                                            <asp:Label ID="QtyLabel" runat="server" Text='<%#Eval("quantity") + " " + Eval("uom_code") %>' style="color:red; font-weight:bold;" /> 
                                            <br />
                                            <div style='<%# String.Format("{0:0.##}", decimal.Parse(Eval("uom_ratio").ToString())) == "1" ? "display:none;" : "display:inline-block;" %>'>
                                            <label style="color:red; font-weight:bold;"><%# "1 " + Eval("uom_code") %></label> = <label style="color:deepskyblue; font-weight:bold;"><%#String.Format("{0:0.##}", decimal.Parse(Eval("uom_ratio").ToString())) + " " + Eval("uom_codeori") %></label> . 
                                            </div>
                                            <label>UoM automatically changed from</label>
                                            <label style="color:red; font-weight:bold;"><%#Eval("uom_code") %></label>
                                            <label>to</label>
                                            <label style="color:deepskyblue; font-weight:bold;"><%#Eval("uom_codeori") %></label>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <label style="font-size:12px; font-style:italic;">*EMR Currently can only process uom with smallest ratio</label> /
                        <label style="font-size:12px;">EMR saat ini hanya bisa proses satuan terkecil obat.</labe>

                        <div style="text-align:right; padding-top:15px;">
                            <button type="button" class="btn btn-sm btn-success" data-dismiss="modal" aria-hidden="true">OK</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<div class="modal fade" id="modalexdrugs" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <asp:UpdatePanel ID="UpdatePanelExistDrugs" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog" style="position: fixed; top: 25%; left: 51%; width: 30%;" runat="server" id="dialogDrugsAlreadyExist">
                <div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h5 class="modal-title">
                            <asp:Label ID="Label1" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 12px;" runat="server" Text="Drugs Already Exist"></asp:Label></h5>
                    </div>
                    <div class="modal-body">
                        <ul>
                            <asp:Repeater runat="server" ID="RepeaterDrugsAlreadyExist">
                                <ItemTemplate>
                                    <li>
                                        <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("item_name") %>' Font-Size="10px" Font-Names="Helvetica" Style="max-width: 100%" />
                                        <br />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanelNotavailDrugs" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog" style="position: fixed; top: 25%; left: 20%; width: 30%;" runat="server" id="dialogDrugsNotAvailable">
                <div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h5 class="modal-title">
                            <asp:Label ID="Label4" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 12px;" runat="server" Text="Drugs not Available"></asp:Label></h5>
                    </div>
                    <div class="modal-body">
                        <ul>
                            <asp:Repeater runat="server" ID="RepeaterDrugsNotAvailable">
                                <ItemTemplate>
                                    <li>
                                        <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("item_name") %>' Font-Size="10px" Font-Names="Helvetica" Style="max-width: 100%" />
                                        <br />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</div>
<%-- ============================================= END MODAL EXCLUDED DRUGS ============================================== --%>

<%-- ============================================= MODAL COMPOUND DETAIL ============================================== --%>
<%--<div class="modal fade" id="modalcompdetail" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h5 class="modal-title">
                    <asp:Label ID="Label5" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server" Text="Compound Detail"></asp:Label></h5>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div>
                            <asp:Label runat="server" ID="itemex" Font-Names="Helvetica, Arial, sans-serif" Text="Drugs not Available"></asp:Label>
                            <ul>
                                <asp:Repeater runat="server" ID="Repeater4">
                                    <ItemTemplate>
                                        <li>
                                            <asp:Label ID="NameLabel" runat="server" Font-Names="Helvetica, Arial, sans-serif" Text='<%#Eval("item_name") %>' Font-Size="12px" Style="max-width: 100%" />
                                            <br />
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvw_comp_detail" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed"
                            DataKeyNames="prescription_id" EmptyDataText="No Data">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:TemplateField HeaderText="Item" ItemStyle-Width="10%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="prescription_compdtl_id" runat="server" Value='<%# Bind("prescription_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="prescription_compdtl_no" runat="server" Value='<%# Bind("prescription_no") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="item_compdtl_id" runat="server" Value='<%# Bind("item_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="frequency_compdtl_id" runat="server" Value='<%# Bind("frequency_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="frequency_compdtl_code" runat="server" Value='<%# Bind("frequency_code") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="administration_route_compdtl_id" runat="server" Value='<%# Bind("administration_route_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="administration_route_compdtl_code" runat="server" Value='<%# Bind("administration_route_code") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="is_routine_compdtl" runat="server" Value='<%# Bind("is_routine") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="is_consumables_compdtl" runat="server" Value='<%# Bind("is_consumables") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="compound_compdtl_id" runat="server" Value='<%# Bind("compound_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="compound_compdtl_name" runat="server" Value='<%# Bind("compound_name") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="origin_prescription_compdtl_id" runat="server" Value='<%# Bind("origin_prescription_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="hope_arinvoice_compdtl_id" runat="server" Value='<%# Bind("hope_arinvoice_id") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="is_delete_compdtl" runat="server" Value='<%# Bind("is_delete") %>'></asp:HiddenField>
                                        <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="item_compdtl_name" runat="server" Text='<%# Bind("item_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" ItemStyle-Width="3%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:TextBox Width="100%" Style="text-align: right; margin: 0px" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="quantity_compdtl" runat="server" Text='<%# Bind("quantity") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UoM" ItemStyle-Width="2%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="uom_compdtl_id" runat="server" Value='<%# Bind("uom_id") %>'></asp:HiddenField>
                                        <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="uom_compdtl_code" runat="server" Text='<%# Bind("uom_code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dose" ItemStyle-Width="2%" HeaderStyle-Font-Size="11px" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox Width="100%" Style="text-align: right" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="dosage_compdtl_id" runat="server" Text='<%# Bind("dosage_id") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dose Text" ItemStyle-Width="6%" HeaderStyle-Font-Size="11px" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox Width="100%" CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="dose_compdtl_text" runat="server" Text='<%# Bind("dose_text") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dose & Instruction" ItemStyle-Width="10%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:TextBox Width="100%" MaxLength="100" Style="margin: 0px" CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="remarks_compdtl" runat="server" Text='<%# Bind("remarks") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Iter" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:TextBox Width="100%" Style="text-align: right; margin: 0px" CssClass="box" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="iteration_compdtl" runat="server" Text='<%# Bind("iteration") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="1%" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btndelete" runat="server" OnClick="btnDeleteComp_Click"><span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_delete.svg") %>" class="ic_delete"></span></asp:LinkButton>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox runat="server" ID="item_id_detail" Visible="false" ReadOnly="true" />
                        <asp:TextBox runat="server" ID="item_search_detail" Placeholder="Item Name" ReadOnly="true" onclick="OnClick2()" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="testpopup2" style="display: none; position: absolute; z-index: 1; border: 1px groove; min-width: 600px; min-height: 200px; background-color: white; max-height: 250px; max-width: 800px">
                    <asp:UpdatePanel runat="server" ID="Updatepanel3" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div>
                                <asp:TextBox runat="server" ID="find_detail" onkeydown="return txtOnKeyPress();" AutoPostBack="true" OnTextChanged="btnFindcomp_click"></asp:TextBox>
                                <asp:Button runat="server" OnClick="btnFindcomp_click" Text="Find" />
                            </div>
                            <div style="overflow-y: scroll; max-height: 200px; max-width: 800px; min-width: 600px; min-height: 200px;">
                                <asp:GridView ID="gvw_item_detail" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed" BorderColor="Black"
                                    HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="salesItemId" EmptyDataText="No Data"
                                    AllowSorting="True">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="salesitemid_detail" runat="server" Text='<%# Bind("salesItemId") %>' Visible="false"><%# Eval("salesItemId").ToString() %></asp:Label>
                                                <asp:LinkButton Font-Underline="true" Font-Size="12px" ID="salesItemName_detail" runat="server" Text='<%# Bind("salesItemName") %>' OnClientClick="OnClick2()" OnClick="itemselecteddetail_onclick"></asp:LinkButton>
                                                <asp:HiddenField ID="hfUomId_detail" Value='<%# Bind("salesUomId") %>' runat="server" />
                                                <asp:HiddenField ID="hfUomCode_detail" Value='<%# Bind("salesUomCode") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Active Ingredients" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="activeIngredientsName" SortExpression="activeIngredientsName"></asp:BoundField>
                                        <asp:BoundField HeaderText="Quantity" DataFormatString="{0:0.00}" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" DataField="totalQuantity" SortExpression="totalQuantity"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button runat="server" Text="Save" class="box" OnClick="btnSaveCompDetail_onClick" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>--%>
<%-- ============================================= END  MODAL COMPOUND DETAIL ============================================== --%>


<%-- ========================================================== Modal Racikan  ================================================================ --%>
<div class="modal fade" id="modalInputRacikan" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog" style="margin-top: 5%; width: 75%;">
        <div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
            <asp:UpdatePanel ID="UpdatePanelmodalInputRacikan" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-header" style="height: 35px; padding-top: 5px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h5 class="modal-title">
                            <asp:Label ID="LabelHeaderModalRacikan" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Tambah Racikan Baru">
                            </asp:Label></h5>
                    </div>
                    <div class="modal-body no-padding" style="padding-bottom: 0px;">
                        <asp:UpdatePanel runat="server" ID="UpdatePanelModalbodyRacikan" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:HiddenField ID="inputhidden_isadditionaltype" runat="server" />
                                <table style="width: 100%; border-bottom: 2px solid lightgrey; border-top: 2px solid lightgrey;" class="table-condensed table-bordered">
                                    <tr style="font-weight: bold; background-color: #f4f4f4;">
                                        <td style="width: 25%; padding-left: 10px;">
                                            <label id="lblbhs_racikmodal_compoundname">Nama</label></td>
                                        <td style="width: 5%;">
                                            <label id="lblbhs_racikmodal_dose">Teks</label></td>
                                        <td style="width: 15%;">
                                            <label id="lblbhs_racikmodal_doseuom">Dosis</label></td>
                                        <td style="width: 10%;">
                                            <label id="lblbhs_racikmodal_freq">Frekuensi</label></td>
                                        <td style="width: 10%;">
                                            <label id="lblbhs_racikmodal_route">Rute</label></td>
                                        <td style="width: 15%;">
                                            <label id="lblbhs_racikmodal_instruction">Instruksi</label></td>
                                        <td style="width: 5%;">
                                            <label id="lblbhs_racikmodal_qty">Jml</label></td>
                                        <td style="width: 10%;">
                                            <label id="lblbhs_racikmodal_uom">Unit</label></td>
                                        <td style="width: 5%;">
                                            <label id="lblbhs_racikmodal_iter">Iter</label></td>
                                    </tr>
                                    <tr style="background-color: #f4f4f4;">
                                        <td style="width: 25%; padding-left: 10px;">
                                            <asp:HiddenField ID="inputhidden_prescription_compound_header_id" runat="server" />
                                            <asp:TextBox ID="input_namaRacikan" Width="100%" Style="font-weight: bold;" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 5%;">
                                            <div class="pretty p-icon p-curve" style="margin-top: 4px;"  title="Use Dose Text">
                                                <asp:CheckBox ID="input_is_dosetext" runat="server" AutoPostBack="true" OnCheckedChanged="input_is_dosetext_CheckedChanged" Checked='<%# Eval("IsDoseText") %>'></asp:CheckBox>
                                                <div class="state p-success">
                                                    <i class="icon fa fa-check"></i>
                                                    <label></label>
                                                </div>
                                            </div>
                                        </td>
                                        <td style="width: 10%;">
                                            <div runat="server" id="input_dvdose">
                                            <asp:TextBox ID="input_dosisRacikan" Width="45px" Style="text-align: right" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server" Text="" onkeydown="return txtOnKeyPressFalse();" data-toggle="dropdown"></asp:TextBox>
                                            <ul class="dropdown-menu bdrop" style="top: auto; left: auto; position: fixed; min-width: 65px;">
                                                <li>
                                                    <a href="javascript:convertText('input_dosisRacikan','0.50')">1/2 </a>
                                                </li>
                                                <li>
                                                    <a href="javascript:convertText('input_dosisRacikan','0.25')">1/4 </a>
                                                </li>
                                                <li>
                                                    <a href="javascript:convertText('input_dosisRacikan','0.167')">1/6 </a>
                                                </li>
                                                <li>
                                                    <a href="javascript:convertText('input_dosisRacikan','0.125')">1/8 </a>
                                                </li>
                                                <li>
                                                    <a href="javascript:convertText('input_dosisRacikan','0.33')">1/3 </a>
                                                </li>
                                                <li>
                                                    <a href="javascript:convertText('input_dosisRacikan','0.67')">2/3 </a>
                                                </li>
                                                <li>
                                                    <a href="javascript:convertText('input_dosisRacikan','0.75')">3/4 </a>
                                                </li>
                                            </ul>
                                            <asp:DropDownList ID="inputddl_dosisunitRacikan" Style="margin: 0px; width:80px;" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server"></asp:DropDownList>
                                            </div>

                                            <asp:TextBox  Width="100%" MaxLength="100" Font-Size="11px" Style="margin: 0px; overflow: hidden; display:none;" Font-Names="Helvetica, Arial, sans-serif" ID="input_dosetext" runat="server" Text='<%# Bind("dose_text") %>' onkeydown="AutoExpand(this);" onkeyup="charLimitInstruction(this,100);" TextMode="MultiLine" Rows="1" onfocus="minexpand(this)" CssClass="text-multiline-dialog"></asp:TextBox>
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:DropDownList ID="inputddl_frekuensiRacikan" Style="margin: 0px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server"></asp:DropDownList>
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:DropDownList ID="inputddl_ruteRacikan" Style="margin: 0px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server"></asp:DropDownList>
                                        </td>
                                        <td style="width: 15%;">
                                            <asp:TextBox ID="input_instruksiRacikan" Width="100%" MaxLength="100" Font-Size="11px" Style="margin: 0px; overflow: hidden;" Font-Names="Helvetica, Arial, sans-serif" runat="server" Text="" onkeydown="AutoExpand(this);" onkeyup="charLimitInstruction(this,100);" TextMode="MultiLine" Rows="1" onfocus="minexpand(this)" CssClass="text-multiline-dialog"></asp:TextBox>
                                        </td>
                                        <td style="width: 5%;">
                                            <asp:TextBox ID="input_jmlRacikan" Width="100%" Style="text-align: right; margin: 0px" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server" Text='<%# Bind("quantity") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:DropDownList ID="inputddl_unitRacikan" Style="margin: 0px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server"></asp:DropDownList>
                                        </td>
                                        <td style="width: 5%;">
                                            <asp:TextBox ID="input_iterRacikan" Width="100%" Style="text-align: right; margin: 0px" onkeypress="return CheckNumeric();" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server" Text="" onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div style="padding-left: 10px; padding-right: 10px;">
                                    <asp:GridView ID="input_gvw_racikan_detail" runat="server" AutoGenerateColumns="False" CssClass="table-bordered table-condensed"
                                        DataKeyNames="prescription_compound_detail_id" OnRowDataBound="input_gvw_racikan_detail_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30%" HeaderText="Item" HeaderStyle-BackColor="#f4f4f4" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="HF_racikan_detail_id" runat="server" Value='<%# Bind("prescription_compound_detail_id") %>' />
                                                    <asp:HiddenField ID="HF_racikan_header_id" runat="server" Value='<%# Bind("prescription_compound_header_id") %>' />
                                                    <asp:HiddenField ID="HF_racikan_item_id" runat="server" Value='<%# Bind("item_id") %>' />
                                                    <asp:HiddenField ID="HF_racikan_uom_id" runat="server" Value='<%# Bind("uom_id") %>' />
                                                    <div style="text-align: left;">
                                                        <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="racikan_item_name" runat="server" Text='<%# Bind("item_name") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="5%" HeaderText="Text" HeaderStyle-BackColor="#f4f4f4" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <div class="pretty p-icon p-curve" style="margin-top: 4px;"  title="Use Dose Text">
                                                        <asp:CheckBox ID="racikan_is_dosetext" runat="server" AutoPostBack="true" OnCheckedChanged="racikan_is_dosetext_CheckedChanged" Checked='<%# Eval("IsDoseText") %>'></asp:CheckBox>
                                                        <div class="state p-success">
                                                            <i class="icon fa fa-check"></i>
                                                            <label></label>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="20%" HeaderText="Dose" HeaderStyle-BackColor="#f4f4f4" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <div runat="server" Visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                                        <asp:TextBox  Style="text-align: right; width:50px;" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="racikan_dosage_id" runat="server" Text='<%# Bind("dose") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                                        <asp:DropDownList  ID="racikan_doseuom" style="margin: 0px; width:100px;" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server" ></asp:DropDownList>
                                                    </div>
                                                    <asp:TextBox  Width="100%" MaxLength="100" Font-Size="11px" Style="margin: 0px; overflow: hidden;" Font-Names="Helvetica, Arial, sans-serif" ID="racikan_dosetext" runat="server" Visible='<%# Eval("IsDoseText") %>' Text='<%# Bind("dose_text") %>' onkeydown="AutoExpand(this);" onkeyup="charLimitInstruction(this,100);" TextMode="MultiLine" Rows="1" onfocus="minexpand(this)" CssClass="text-multiline-dialog"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Qty" HeaderStyle-BackColor="#f4f4f4" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="racikan_quantity" Width="100%" Style="text-align: right; margin: 0px;" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server" Text='<%# Bind("quantity") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Unit" HeaderStyle-BackColor="#f4f4f4" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="false">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="racikan_uom_code" Style="margin: 0px; cursor: not-allowed;" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server" Enabled="false"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30%" HeaderStyle-BackColor="#f4f4f4" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete_inputRacikan" OnClick="btndelete_inputRacikan_Click" runat="server"><span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_delete.svg") %>" class="ic_delete"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <!-- kotak pencarian Autocomplete -->
                                <div style="margin-top: 10px;">
                                    <asp:UpdatePanel runat="server" ID="UP_SearchRacikan" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-3" style="width: 190px;">
                                                    <div class="has-feedback" style="width: 180px;">
                                                        <asp:TextBox ID="txtinput_ItemRacikan_AC" runat="server" Placeholder="Add item here..." Style="width: 180px; margin-left: 10px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('racikan','true');"></asp:TextBox>
                                                        <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px; z-index: 0;"></span>
                                                    </div>
                                                    <asp:HiddenField ID="HF_flagfocusracikansearch" runat="server" />
                                                    <asp:HiddenField ID="HF_ItemSelectedracikan" runat="server" />
                                                    <asp:Button ID="ButtonAjaxSearchRacikan" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchRacikan_Click" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="loadingdrug" style="display: none;">
                                                        <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                        </div>
                                                        &nbsp;
                                                                <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                        <asp:HiddenField ID="HFloadingracikan" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- end kotak pencarian -->
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="container-fluid">
                        <div class="row" style="padding-bottom: 10px;">
                            <div style="height: 22px; text-align: right; padding-right: 15px;">
                                <asp:UpdateProgress ID="UpdateProgressModalracikan" runat="server" AssociatedUpdatePanelID="UPsaveRacikan">
                                    <ProgressTemplate>
                                        <div class="modal-backdrop" style="background-color: white; opacity: 0.3; text-align: center">
                                        </div>
                                        <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                        &nbsp;
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <div class="col-sm-10" style="padding-left: 10px;">
                                <asp:TextBox ID="input_instruksiRacikan_note" Width="100%" MaxLength="100" Font-Size="11px" Style="margin: 0px; overflow: hidden; min-height: 35px;" Font-Names="Helvetica, Arial, sans-serif" placeholder="Instruksi racikan untuk farmasi ketik disini..." runat="server" Text="" onkeydown="AutoExpand(this);" TextMode="MultiLine" Rows="2" onfocus="minexpand(this)" CssClass="text-multiline-dialog"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 text-right" style="padding-left: 0px;">
                                <asp:UpdatePanel ID="UPsaveRacikan" runat="server">
                                    <ContentTemplate>
                                        <a class="btn btn-default" href="javascript: $('#modalInputRacikan').modal('hide');">Cancel</a>
                                        <asp:Button ID="BtnSave_Racikan" runat="server" class="btn btn-lightGreen" Text="Save" OnClientClick="return validasi_inputRacikan();" OnClick="BtnSave_Racikan_Click" />
                                        <asp:Button ID="BtnUpdate_Racikan" runat="server" class="btn btn-lightGreen" Text="Update" OnClientClick="return validasi_inputRacikan();" OnClick="BtnUpdate_Racikan_Click" />
                                        <asp:Button ID="BtnSave_Racikan_Add" runat="server" class="btn btn-lightGreen" Text="Save" OnClientClick="return validasi_inputRacikan();" OnClick="BtnSave_Racikan_Add_Click" />
                                        <asp:Button ID="BtnUpdate_Racikan_Add" runat="server" class="btn btn-lightGreen" Text="Update" OnClientClick="return validasi_inputRacikan();" OnClick="BtnUpdate_Racikan_Add_Click" />
                                        <%--<a class="btn btn-lightGreen" href="javascript: $('#modalInputRacikan').modal('hide');">Save</a>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-12" style="padding-left: 10px; padding-top: 5px;">
                                <div id="notifRacikan" runat="server" class="alert-danger" style="display: none; border-radius: 3px; padding: 10px; font-weight: bold;">
                                    <asp:Label ID="LabelNotifRacikan" runat="server" Style="color: white;" Text="Data Wajib Diisi (Tidak Boleh Kosong)!"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<%-- ========================================================== END Racikan  ================================================================ --%>

<%-- ============================================= MODAL ALERT LAB ============================================== --%>
<div class="modal fade" id="modallab" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="position: fixed; top: 25%; left: 35%; width: 30%;">
        <div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
            <div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h5 class="modal-title">
                    <asp:Label ID="Label6" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Service Lab / Rad Warning"></asp:Label></h5>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel runat="server" ID="updatepanelexistlabrad" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label Style="font-family: Helvetica; font-size: 12px; font-weight: bold;" runat="server" ID="lblExist" Text="Service Already Exist"></asp:Label>
                        <ul>
                            <asp:Repeater runat="server" ID="rptExist">
                                <ItemTemplate>
                                    <li>
                                        <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("name") %>' Font-Size="11px" Font-Names="Helvetica" Style="max-width: 100%" />
                                        <br />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <asp:Label runat="server" Style="font-family: Helvetica; font-size: 12px; font-weight: bold;" ID="lblNotExist" Text="Service Not Available"></asp:Label>
                        <ul>
                            <asp:Repeater runat="server" ID="rptNotExist">
                                <ItemTemplate>
                                    <li>
                                        <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("name") %>' Font-Size="11px" Font-Names="Helvetica" Style="max-width: 100%" />
                                        <br />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>
<%-- ============================================= END MODAL SUBMIT AND SIGN ============================================== --%>

<%-- ============================================= MODAL RADIOLOGY ============================================== --%>
<div class="modal fade" id="modalRad" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" onabort="modal_onclose">
    <div class="modal-dialog" style="width: 1080px;">
        <asp:UpdatePanel ID="UP_ContainerRad" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content" style="border-radius: 7px">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" id="dismissrad" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h5 class="modal-title">
                            <asp:Label ID="LblRadiology" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server">
                                <label id="lblbhs_modalradiology">Radiology</label>
                            </asp:Label></h5>
                    </div>

                    <div class="modal-body" style="background-color: #e7e8ef; margin: 0px; padding-top: 10px; padding-bottom: 0px">
                        <asp:UpdateProgress ID="UpdateProgress14" runat="server" AssociatedUpdatePanelID="UpdatePanelDivRad">
                            <ProgressTemplate>
                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                </div>
                                <div style="text-align:center">
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                        <asp:HiddenField ID="hfbuilderobjectradiology" runat="server" />
                        <asp:Button runat="server" ID="btnvalueradiology" OnClick="btnGetValueCPOE_Rad" Style="display: none" />

                        <asp:HiddenField runat="server" ID="HF_FlagTabRad" />
                        <asp:Button ID="ButtonChooseTabRad" runat="server" Text="-" style="display:none;" OnClick="ButtonChooseTabRad_Click" />

                        <asp:HiddenField runat="server" ID="HF_FlagTabXray" Value="open" /> 
                        <asp:HiddenField runat="server" ID="HF_FlagTabUSG" />
                        <asp:HiddenField runat="server" ID="HF_FlagTabCT" />
                        <asp:HiddenField runat="server" ID="HF_FlagTabMRI1" />
                        <asp:HiddenField runat="server" ID="HF_FlagTabMRI3" />
                        <asp:HiddenField runat="server" ID="HF_FlagFutureOrderRad" Value ="false" />

                        <cc1:TabContainer ID="TabContainer2" runat="server" Visible="false" OnClientActiveTabChanged="LoadTabRadData">
                            <cc1:TabPanel ID="XRay" Visible="true" HeaderText="XRay" Font-Size="14px" Style="background-color: #e7e8ef;" runat="server">
                                <HeaderTemplate>XRay</HeaderTemplate>
                                <ContentTemplate>
                                    <%--<uc3:XrayRad runat="server" ID="stdxray" />--%>
                                    <uc3:RptXrayRad runat="server" ID="stdxray" />
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="USG" Visible="true" HeaderText="USG " Font-Size="14px" Style="background-color: #e7e8ef;" runat="server">
                                <HeaderTemplate>USG</HeaderTemplate>
                                <ContentTemplate>
                                    <%--<uc3:USGRad runat="server" ID="stdusg" />--%>
                                    <uc3:RptUSGRad runat="server" ID="stdusg" Visible="false" />
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="CT" Visible="true" HeaderText="CT" Font-Size="14px" Style="background-color: #e7e8ef;" runat="server">
                                <HeaderTemplate>CT</HeaderTemplate>
                                <ContentTemplate>
                                    <%--<uc3:CTRad runat="server" ID="stdctrad" />--%>
                                    <uc3:RptCTRad runat="server" ID="stdctrad" Visible="false" />
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="MRI1" Visible="true" HeaderText="MRI 1,5 Tesla" Font-Size="14px" Style="background-color: #e7e8ef;" runat="server">
                                <HeaderTemplate>MRI 1,5 Tesla</HeaderTemplate>
                                <ContentTemplate>
                                    <%--<uc3:MRIhalfRad runat="server" ID="stdmrihalf" />--%>
                                    <uc3:RptMRIhalfRad runat="server" ID="stdmrihalf" Visible="false" />
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="MRI3" Visible="true" HeaderText="MRI 3 Tesla" Font-Size="14px" Style="background-color: #e7e8ef;" runat="server">
                                <HeaderTemplate>MRI 3 Tesla</HeaderTemplate>
                                <ContentTemplate>
                                    <%--<uc3:MRIfullRad runat="server" ID="stdmrifull" />--%>
                                    <uc3:RptMRIfullRad runat="server" ID="stdmrifull" Visible="false" />
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>

                        <asp:UpdateProgress ID="UpdateProgress16" runat="server" AssociatedUpdatePanelID="UP_ContainerRad">
                            <ProgressTemplate>
                                <div style="text-align:center">
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-info" id="closerad" data-dismiss="modal" aria-hidden="true">Close</button>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<%-- ============================================= END MODAL RADIOLOGY ============================================== --%>

<%-- ============================================= MODAL LABORATORY ============================================== --%>
<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" onabort="modal_onclose">
    <div class="modal-dialog" style="width: 1080px;">
        <asp:UpdatePanel ID="UP_ContainerLab" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content" style="border-radius: 7px">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" id="dismisslab" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h5 class="modal-title">
                            <asp:Label ID="lblModalTitle" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server">
                                <%--<label style="display: <%=setENG%>;">Laboratory</label>
                                <label style="display: <%=setIND%>;">Laboratorium</label>--%>
                                <label id="lblbhs_modallaboratory">Laboratory</label>
                            </asp:Label></h5>
                    </div>
                    <div class="modal-body" style="background-color: #e7e8ef; margin: 0px; padding-top: 10px; padding-bottom: 0px">
                        <asp:UpdateProgress ID="UpdateProgress13" runat="server" AssociatedUpdatePanelID="UpdatePanelDivLab">
                            <ProgressTemplate>
                                <div style="text-align:center">
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                        <asp:HiddenField runat="server" ID="hfbuilderobject" />
                        <asp:Button runat="server" ID="btnvaluecpoe" OnClick="btnGetValueCPOE_Lab" Style="display: none" />

                        <asp:HiddenField runat="server" ID="HF_FlagTabLab" />
                        <asp:Button ID="ButtonChooseTabLab" runat="server" Text="-" style="display:none;" OnClick="ButtonChooseTabLab_Click" />

                        <asp:HiddenField runat="server" ID="HF_FlagTabClinical" Value="open" /> 
                        <asp:HiddenField runat="server" ID="HF_FlagTabMicrobiology" />
                        <asp:HiddenField runat="server" ID="HF_FlagTabCITO" />
                        <asp:HiddenField runat="server" ID="HF_FlagTabAnatomical" />
                        <asp:HiddenField runat="server" ID="HF_FlagTabMDC" />
                        <asp:HiddenField runat="server" ID="HF_FlagTabPanel" />
                        <asp:HiddenField runat="server" ID="HF_FlagFutureOrder" Value="false" />

                        <cc1:TabContainer ID="TabContainer1" runat="server" Visible="false" OnClientActiveTabChanged="LoadTabLabData">
                            <cc1:TabPanel ID="TabPanel1" Visible="true" HeaderText="Clinical Pathology " Font-Size="14px" Style="background-color: #e7e8ef;" runat="server">
                                <HeaderTemplate>
                                    <label id="lblbhs_tabclinical">Clinical Pathology </label>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <%--<uc3:ClinicalLab runat="server" ID="stdclinic" />--%>
                                    <uc3:RptClinicalLab runat="server" ID="stdclinic" />
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="Microbiology" Visible="true" HeaderText="Microbiology " runat="server">
                                <HeaderTemplate>
                                    <label id="lblbhs_tabmicro">Microbiology </label>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <%--<uc3:MicroLab runat="server" ID="stdmicro" />--%>
                                    <uc3:RptMicroLab runat="server" ID="stdmicro" Visible="false" />
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="CITO" Visible="true" HeaderText="Clinical Pathology (CITO) " runat="server">
                                <HeaderTemplate>
                                    <span style="color: red">
                                        <label id="lblbhs_tabcito">Clinical Pathology (CITO) </label>
                                    </span>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <%--<uc3:CitoLab runat="server" ID="stdcito" />--%>
                                    <uc3:RptCitoLab runat="server" ID="stdcito" Visible="false" />
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="Anatomi" Visible="true" HeaderText="Patologi Anatomi" runat="server">
                                <HeaderTemplate>
                                    <label id="lblbhs_tabanatomi">Anatomical Pathology</label>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <%--<uc3:AnatomiLab runat="server" ID="stdanatomi" />--%>
                                    <uc3:RptAnatomiLab runat="server" ID="stdanatomi" Visible="false" />
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabMDC" Visible="true" HeaderText="MDC" runat="server">
                                <HeaderTemplate>
                                    <label id="lblbhs_tabMDC">MDC</label>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <%--<uc3:MDCLab runat="server" ID="stdmdc" />--%>
                                    <uc3:RptMDCLab runat="server" ID="stdmdc" Visible="false" />
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabPanel2" Visible="true" HeaderText="Panel and Others" runat="server">
                                <HeaderTemplate>
                                    <label id="lblbhs_tabpanel">Panel and Others</label>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <%--<uc3:PanelLab runat="server" ID="stdpanel" />--%>
                                    <uc3:RptPanelLab runat="server" ID="stdpanel" Visible="false" />
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>

                        <asp:UpdateProgress ID="UpdateProgress15" runat="server" AssociatedUpdatePanelID="UP_ContainerLab">
                            <ProgressTemplate>
                                <div style="text-align:center">
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-info" id="closelab" data-dismiss="modal" aria-hidden="true">Close</button>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<%-- ============================================= END MODAL LABORATORY ============================================== --%>

<%-- ============================================= MODAL ALERT DIAGNOSTIC AND PROCEDURE ============================================== --%>
<div class="modal fade" id="modaldiagnostic" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="position: fixed; top: 25%; left: 35%; width: 30%;">
        <div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
            <div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h5 class="modal-title">
                    <asp:Label ID="Label2" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Service Diagnostic / Procedure Warning"></asp:Label></h5>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel runat="server" ID="updatepanelexistdiagproc" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label Style="font-family: Helvetica; font-size: 12px; font-weight: bold;" runat="server" ID="lblExistDiagnostic" Text="Service Already Exist"></asp:Label>
                        <ul>
                            <asp:Repeater runat="server" ID="rptExistDiagProc">
                                <ItemTemplate>
                                    <li>
                                        <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("ProcedureItemName") %>' Font-Size="11px" Font-Names="Helvetica" Style="max-width: 100%" />
                                        <br />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>


<!-- ##### Modal Drugs Interaction ##### -->
<div class="modal fade" id="modalMimsInteraction" role="dialog" tabindex="-1" aria-labelledby="mymodalMimsInteraction" aria-hidden="true">
    <div class="modal-dialog" style="width:70%;">
        <div class="modal-content" style="border-radius: 6px;">
            <!-- Modal Header -->
            <div class="modal-header" style="padding: 8px;">
                <button type="button" class="close" data-dismiss="modal">×</button>
                <div>
                    <b style="font-size: 15px;">Drug Interaction Alert</b>
                </div>
            </div>
            <!-- Modal body -->
            <div class="modal-body no-padding">
                <asp:UpdatePanel ID="UpdatePanelMimsResult" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblMimsHtmlResult" runat="server" Text="" Style="display:none;"></asp:Label>
                        <iframe name="IframeMIMS" id="IframeMIMS" runat="server" src="~/Form/SOAP/PreviewTemplate/MimsResult.aspx" style="width: 100%; height: 80vh; border: none;"></iframe>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="text-align:right; padding: 5px 10px 10px 10px; border-radius: 7px; font-size:12px;">
                <asp:Label ID="labelrefresh" runat="server" Text="" CssClass="btn btn-default" style="float:left;" onclick="document.getElementById('MainContent_StdPlanning_IframeMIMS').contentWindow.refreshmims();"> <i class="fa fa-refresh"></i></asp:Label> &nbsp;
                <asp:Label ID="labelcorrection" runat="server" Text="EDIT PRESCRIPTION" CssClass="btn btn-success" onclick="hidemimsmodal();"></asp:Label> &nbsp;
                <asp:Label ID="labelcontinue" runat="server" Text="UNDERSTAND & PROCEED" CssClass="btn btn-danger" onclick="showdivmimsreason();"></asp:Label>
            </div>
            <div id="div_mims_reason" style="padding: 10px; display:none;">
                <asp:UpdatePanel ID="UpdatePanelReason" runat="server">
                    <ContentTemplate>
                        <div style="padding: 10px; border: 1px solid #eb7b6d; border-radius: 7px;">
                            <h4 style="color:#dd4b39;">Override Alert Reason</h4>
                            <table id="table_reason_mims" border="0">
                                <asp:Repeater ID="RepeaterChkReason" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Lbl_Reason" runat="server" Text='<%# Eval("MimsReasonId") %>' style="display:none;"></asp:Label>
                                                <asp:CheckBox ID="Chk_Reason" runat="server" Style="font-size:15px; margin-bottom:10px;" CssClass="mycheckbox" Checked='<%# Eval("IsChecked") %>' Value='<%# Eval("MimsReasonId") %>' Text='<%# Eval("Reason") %>' onclick='<%# "showhideinputreason(this,\"" + Eval("MimsReasonId") + "\");" %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>

                            <asp:TextBox ID="Txt_ReasonOther" runat="server" TextMode="MultiLine" Rows="2" style="width:55%; vertical-align: text-top; margin-left: 20px; display:none;"></asp:TextBox>
                            <br />

                            <div style="text-align:right;">
                                <asp:Label ID="labelcontinuereason" runat="server" Text="SAVE & PROCEED" CssClass="btn btn-danger" onclick="showsubmitmodal();"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>
<!-- End of Modal -->

<script type="text/javascript">

    function showdivmimsreason() {
        document.getElementById('div_mims_reason').style.display = "";
        document.getElementById('MainContent_StdPlanning_Txt_ReasonOther').value = "";
        $('#modalMimsInteraction').animate({ scrollTop: $('#modalMimsInteraction .modal-content').height() }, 'slow');
    }
    function showsubmitmodal() {
        var flagchk = 0;
        $('#table_reason_mims input[type="checkbox"]').each(function (index) {
            if ($(this).prop('checked') == true) {
                flagchk = 1;
            }
        });

        var txtarea = document.getElementById("MainContent_StdPlanning_Txt_ReasonOther");
        if (txtarea.style.display == "") {
            if (txtarea.value == "") {
                flagchk = 1;
                alert("Please Fill Other Reason!");
                //toastr.error('Please Fill Other Reason!', 'Empty Reason');
                return false;
            }
        }

        if (flagchk == 1) {
            $('#modalMimsInteraction').modal('hide');

            var savemode = $("[id$='hfsavemode']").val();
            if (savemode == '1') {
                $('#modalsubmitDisable').modal('show');
            }
            else {
                $('#modalsubmit').modal('show');
            }

            MimsWarningLog("Continue");
        }
        else {
            alert("Please select minimum 1 reason !");
        }
    }

    function hidemimsmodal() {
        $('#modalMimsInteraction').modal('hide');

        MimsWarningLog("Edit");
    }

    function showhideinputreason(chk_rsn, rsn_id) {
        if (rsn_id == '3') {
            if (chk_rsn.checked == true) {
                document.getElementById("MainContent_StdPlanning_Txt_ReasonOther").style.display = "";
            }
            else if (chk_rsn.checked == false) {
                document.getElementById("MainContent_StdPlanning_Txt_ReasonOther").style.display = "none";
                document.getElementById("MainContent_StdPlanning_Txt_ReasonOther").value = "";
            }
        }
    }

    //fungsi pencarian data di gridview client side
    function Search_Gridview(strKey, strGV) {
        var strData = strKey.value.toLowerCase().split(" ");
        var tblData = document.getElementById('MainContent_StdPlanning_' + strGV);
        var rowData;
        for (var i = 0; i < tblData.rows.length; i++) {
            rowData = tblData.rows[i].textContent;
            var styleDisplay = 'none';
            for (var j = 0; j < strData.length; j++) {
                if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                    styleDisplay = '';
                else {
                    styleDisplay = 'none';
                    break;
                }
            }
            tblData.rows[i].style.display = styleDisplay;
        }
    }

    function DrugSuggestionSOAP() {
        var payertype = document.getElementById('<%=hfPayerType.ClientID%>').value;
        var chkformula = document.getElementById('<%=hfchkformularium.ClientID%>').value;
        var stokupdate = document.getElementById('<%= HF_Label_updatestockdrug.ClientID %>').value;
        var connstatus = document.getElementById('<%= HF_connstatus.ClientID %>').value;
        var labelconnstatus = "";

        if (connstatus == "offline") {
            labelconnstatus = '<i class="fa fa-warning blink" style="color: #c43d32;"></i> <label id="lblbhs_stokupdate_new" style="color: #c43d32; font-size: 14px; font-weight: bold;">Offline: Stock is not update </label>';
        }
        else {
            labelconnstatus = '<label id="lblbhs_updatestockdrug_ac" style="color: #c43d32; font-size: 14px; font-weight: bold;">Stok update per tanggal ' + stokupdate + '</label>'
        }

        $("#MainContent_StdPlanning_txtItemAdd_AC").autocomplete({
            source: "../Control_Template/AutoCompleteDrugSOAP.aspx?payertype=" + payertype + "&chkformula=" + chkformula,
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<div style="width:100%; text-align:right; padding:5px;">' + labelconnstatus + '</div>'
                    + '<table style="width:800px; border-bottom:1px solid lightgrey; background-color:#f4f4f4;">'
                    + '<tr>'
                    + '<td style="width:40%; padding:5px; vertical-align:top; font-weight:bold;"> Items </td>'
                    + '<td style="width:40%; padding:5px; vertical-align:top; font-weight:bold;"> Active Ingredients </td>'
                    + '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold; text-align:right; padding-right:15px;"> Quantity </td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.itemId);
                    document.getElementById('<%= HF_flagfocusdrugsearch.ClientID %>').value = "searchfocusDrug";

                    document.getElementById('<%= HF_ItemSelecteddrug.ClientID %>').value = ui.item.itemId;
                    document.getElementById('<%= ButtonAjaxSearchDrug.ClientID %>').click();
                }
            }
        })
            .focus(function () {
                $(this).autocomplete("search");
            })
            .autocomplete("instance")._renderItem = function (ul, item) {
                if (item.itemQuantity == "0,00" || item.itemQuantity == "0.00") {
                    return $("<li>")
                        .append('<table style="width:800px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemIngredient + '</td>'
                            + '<td style="width:20%; padding:5px; vertical-align:top; text-align:right; padding-right:15px; color:red;">' + item.itemQuantity + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                }
                else {
                    return $("<li>")
                        .append('<table style="width:800px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemIngredient + '</td>'
                            + '<td style="width:20%; padding:5px; vertical-align:top; text-align:right; padding-right:15px;">' + item.itemQuantity + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                }
            };
    }

    function DrugAdditionalSuggestionSOAP() {
        var payertype = document.getElementById('<%=hfPayerType.ClientID%>').value;
        var chkformula = document.getElementById('<%=hfchkformularium.ClientID%>').value;
        var stokupdate = document.getElementById('<%= HF_Label_updatestockdrug.ClientID %>').value;

        $("#MainContent_StdPlanning_txtItemAdd_AC_additional").autocomplete({
            source: "../Control_Template/AutoCompleteDrugSOAP.aspx?payertype=" + payertype + "&chkformula=" + chkformula,
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<div style="width:100%; text-align:right; padding:5px;"><label id="lblbhs_updatestockdrug_ac" style="color: #c43d32; font-size: 14px; font-weight: bold;">Stok update per tanggal ' + stokupdate + '</label></div>'
                    + '<table style="width:800px; border-bottom:1px solid lightgrey; background-color:#f4f4f4;">'
                    + '<tr>'
                    + '<td style="width:40%; padding:5px; vertical-align:top; font-weight:bold;"> Items </td>'
                    + '<td style="width:40%; padding:5px; vertical-align:top; font-weight:bold;"> Active Ingredients </td>'
                    + '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold; text-align:right; padding-right:15px;"> Quantity </td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.itemId);
                    document.getElementById('<%= HF_flagfocusdrugsearch_add.ClientID %>').value = "searchfocusDrug_add";

                    document.getElementById('<%= HF_ItemSelecteddrug_add.ClientID %>').value = ui.item.itemId;
                    document.getElementById('<%= ButtonAjaxSearchDrug_add.ClientID %>').click();
                }
            }
        })
            .focus(function () {
                $(this).autocomplete("search");
            })
            .autocomplete("instance")._renderItem = function (ul, item) {
                if (item.itemQuantity == "0,00" || item.itemQuantity == "0.00") {
                    return $("<li>")
                        .append('<table style="width:800px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemIngredient + '</td>'
                            + '<td style="width:20%; padding:5px; vertical-align:top; text-align:right; padding-right:15px; color:red;">' + item.itemQuantity + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                }
                else {
                    return $("<li>")
                        .append('<table style="width:800px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemIngredient + '</td>'
                            + '<td style="width:20%; padding:5px; vertical-align:top; text-align:right; padding-right:15px;">' + item.itemQuantity + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                }
            };
    }

    function ConsumableSuggestionSOAP() {
        $("#MainContent_StdPlanning_txtItemCons_AC").autocomplete({
            source: "../Control_Template/AutoCompleteConsSOAP.aspx",
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<table style="width:800px; border-bottom:1px solid lightgrey; background-color:#f4f4f4;">'
                    + '<tr>'
                    + '<td style="width:80%; padding:5px; vertical-align:top; font-weight:bold;"> Items </td>'
                    + '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold; text-align:right; padding-right:15px;"> Quantity </td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.itemId);
                    document.getElementById('<%= HF_flagfocusconssearch.ClientID %>').value = "searchfocusCons";

                    document.getElementById('<%= HF_ItemSelectedcons.ClientID %>').value = ui.item.itemId;
                    document.getElementById('<%= ButtonAjaxSearchCons.ClientID %>').click();
                }
            }
        })
            .focus(function () {
                $(this).autocomplete("search");
            })
            .autocomplete("instance")._renderItem = function (ul, item) {
                if (item.itemQuantity == "0,00" || item.itemQuantity == "0.00") {
                    return $("<li>")
                        .append('<table style="width:800px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:80%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                            + '<td style="width:20%; padding:5px; vertical-align:top; text-align:right; padding-right:15px; color:red;">' + item.itemQuantity + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                }
                else {
                    return $("<li>")
                        .append('<table style="width:800px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:80%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                            + '<td style="width:20%; padding:5px; vertical-align:top; text-align:right; padding-right:15px;">' + item.itemQuantity + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                }
            };
    }

    function ConsumableAdditionalSuggestionSOAP() {
        $("#MainContent_StdPlanning_txtItemCons_AC_additional").autocomplete({
            source: "../Control_Template/AutoCompleteConsSOAP.aspx",
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<table style="width:800px; border-bottom:1px solid lightgrey; background-color:#f4f4f4;">'
                    + '<tr>'
                    + '<td style="width:80%; padding:5px; vertical-align:top; font-weight:bold;"> Items </td>'
                    + '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold; text-align:right; padding-right:15px;"> Quantity </td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.itemId);
                    document.getElementById('<%= HF_flagfocusconssearch_add.ClientID %>').value = "searchfocusCons_add";

                    document.getElementById('<%= HF_ItemSelectedcons_add.ClientID %>').value = ui.item.itemId;
                    document.getElementById('<%= ButtonAjaxSearchCons_add.ClientID %>').click();
                }
            }
        })
            .focus(function () {
                $(this).autocomplete("search");
            })
            .autocomplete("instance")._renderItem = function (ul, item) {
                if (item.itemQuantity == "0,00" || item.itemQuantity == "0.00") {
                    return $("<li>")
                        .append('<table style="width:800px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:80%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                            + '<td style="width:20%; padding:5px; vertical-align:top; text-align:right; padding-right:15px; color:red;">' + item.itemQuantity + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                }
                else {
                    return $("<li>")
                        .append('<table style="width:800px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:80%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                            + '<td style="width:20%; padding:5px; vertical-align:top; text-align:right; padding-right:15px;">' + item.itemQuantity + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                }
            };
    }

    function LabCPOESuggestionSOAP() {
        $("#MainContent_StdPlanning_txtItemCPOE_LAB").autocomplete({
            source: "../Control_Template/AutoCompleteCPOE.aspx?cpoetype=LAB",
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<table style="width:500px; border-bottom:1px solid lightgrey; background-color:#9d1fc3; color:white;">'
                    + '<tr>'
                    + '<td style="width:80%; padding:5px; vertical-align:top; font-weight:bold;"> Laboratory Items </td>'
                    + '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Type </td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.itemId);

                    document.getElementById('<%= HF_ItemSelectedcpoelab.ClientID %>').value = ui.item.itemId;
                    document.getElementById('<%= ButtonAjaxSearchCPOE_LAB.ClientID %>').click();
                }
            }
        })
            .focus(function () {
                $(this).autocomplete("search");
            })
            .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append('<table style="width:500px; border-bottom:1px solid lightgrey;">'
                        + '<tr>'
                        + '<td style="width:80%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                        + '<td style="width:20%; padding:5px; vertical-align:top;">' + item.itemType + '</td>'
                        + '</tr>'
                        + '</table>')
                    .appendTo(ul);
            };
    }

	function LabCPOESuggestionSOAP_FutureOrder() {
		$("#MainContent_StdPlanning_txtItemCPOE_LAB_FutureOrder").autocomplete({
			source: "../Control_Template/AutoCompleteCPOE.aspx?cpoetype=LAB",
			minLength: 0,
			open: function () {
				$('ul.ui-autocomplete').prepend('<li>'
					+ '<table style="width:500px; border-bottom:1px solid lightgrey; background-color:#9d1fc3; color:white;">'
					+ '<tr>'
					+ '<td style="width:80%; padding:5px; vertical-align:top; font-weight:bold;"> Laboratory Items </td>'
					+ '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Type </td>'
					+ '</tr>'
					+ '</table>'
					+ '</li>');
			},
			position: { my: "left top", at: "left bottom", collision: "flip" },
			select: function (event, ui) {
				//assign value back to the form element
				if (ui.item) {
					$(event.target).val(ui.item.itemId);

					document.getElementById('<%= HF_ItemSelectedcpoelab_FutureOrder.ClientID %>').value = ui.item.itemId;
					document.getElementById('<%= ButtonAjaxSearchCPOE_LAB_FutureOrder.ClientID %>').click();
				}
			}
		})
				.focus(function () {
					$(this).autocomplete("search");
				})
				.autocomplete("instance")._renderItem = function (ul, item) {
					return $("<li>")
						.append('<table style="width:500px; border-bottom:1px solid lightgrey;">'
							+ '<tr>'
							+ '<td style="width:80%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
							+ '<td style="width:20%; padding:5px; vertical-align:top;">' + item.itemType + '</td>'
							+ '</tr>'
							+ '</table>')
						.appendTo(ul);
				};
		}

    function RadCPOESuggestionSOAP() {
        $("#MainContent_StdPlanning_txtItemCPOE_RAD").autocomplete({
            source: "../Control_Template/AutoCompleteCPOE.aspx?cpoetype=RAD",
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<table style="width:500px; border-bottom:1px solid lightgrey; background-color:#ffda00; color:Black;">'
                    + '<tr>'
                    + '<td style="width:80%; padding:5px; vertical-align:top; font-weight:bold;"> Radiology Items </td>'
                    + '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Type </td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.itemId);

                    document.getElementById('<%= HF_ItemSelectedcpoerad.ClientID %>').value = ui.item.itemId;
                    document.getElementById('<%= HF_ItemSelectedcpoerad_name.ClientID %>').value = ui.item.itemName;
                    document.getElementById('<%= HF_ItemSelectedcpoerad_remarks.ClientID %>').value = ui.item.itemRemarks;

                    document.getElementById('<%= ButtonAjaxSearchCPOE_RAD.ClientID %>').click();
                }
            }
        })
            .focus(function () {
                $(this).autocomplete("search");
            })
            .autocomplete("instance")._renderItem = function (ul, item) {
                if (item.itemRemarks != "") {
                    return $("<li>")
                        .append('<table style="width:500px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:80%; padding:5px; vertical-align:top;">' + item.itemName + ' (' + item.itemRemarks + ') </td>'
                            + '<td style="width:20%; padding:5px; vertical-align:top;">' + item.itemType + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                }
                else {
                    return $("<li>")
                        .append('<table style="width:500px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:80%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                            + '<td style="width:20%; padding:5px; vertical-align:top;">' + item.itemType + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                }
            };
    }

	function RadCPOESuggestionSOAP_FutureOrder() {
		$("#MainContent_StdPlanning_txtItemCPOE_RAD_FutureOrder").autocomplete({
			source: "../Control_Template/AutoCompleteCPOE.aspx?cpoetype=RAD",
			minLength: 0,
			open: function () {
				$('ul.ui-autocomplete').prepend('<li>'
					+ '<table style="width:500px; border-bottom:1px solid lightgrey; background-color:#ffda00; color:Black;">'
					+ '<tr>'
					+ '<td style="width:80%; padding:5px; vertical-align:top; font-weight:bold;"> Radiology Items </td>'
					+ '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Type </td>'
					+ '</tr>'
					+ '</table>'
					+ '</li>');
			},
			position: { my: "left top", at: "left bottom", collision: "flip" },
			select: function (event, ui) {
				//assign value back to the form element
				if (ui.item) {
					$(event.target).val(ui.item.itemId);

					document.getElementById('<%= HF_ItemSelectedcpoerad_FutureOrder.ClientID %>').value = ui.item.itemId;
					document.getElementById('<%= HF_ItemSelectedcpoerad_name_FutureOrder.ClientID %>').value = ui.item.itemName;
                    document.getElementById('<%= HF_ItemSelectedcpoerad_remarks_FutureOrder.ClientID %>').value = ui.item.itemRemarks;

					document.getElementById('<%= ButtonAjaxSearchCPOE_RAD_FutureOrder.ClientID %>').click();
				}
			}
		})
			.focus(function () {
				$(this).autocomplete("search");
			})
			.autocomplete("instance")._renderItem = function (ul, item) {
				if (item.itemRemarks != "") {
					return $("<li>")
						.append('<table style="width:500px; border-bottom:1px solid lightgrey;">'
							+ '<tr>'
							+ '<td style="width:80%; padding:5px; vertical-align:top;">' + item.itemName + ' (' + item.itemRemarks + ') </td>'
							+ '<td style="width:20%; padding:5px; vertical-align:top;">' + item.itemType + '</td>'
							+ '</tr>'
							+ '</table>')
						.appendTo(ul);
				}
				else {
					return $("<li>")
						.append('<table style="width:500px; border-bottom:1px solid lightgrey;">'
							+ '<tr>'
							+ '<td style="width:80%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
							+ '<td style="width:20%; padding:5px; vertical-align:top;">' + item.itemType + '</td>'
							+ '</tr>'
							+ '</table>')
						.appendTo(ul);
				}
			};
	}


    function DiagProcSuggestionSOAPNonModal() {
        // --------------------- DIAGNOSTIC ---------------------------//
        // add item on txtItemDiag non future order
        $("#MainContent_StdPlanning_txtItem_DIAG").autocomplete({
            source: "../Control_Template/AutoCompleteDiagProc.aspx?type=4",
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<table style="width:475px; border-bottom:1px solid lightgrey; background-color:#f4f4f4;">'
                    + '<tr>'
                    //+ '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Item Code </td>'
                    + '<td style="width:100%; padding:5px; vertical-align:top; font-weight:bold;">Diagnostic</td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.SalesItemId);

                    document.getElementById('<%= HF_ItemSelectedDiagnosticNonModal.ClientID %>').value = ui.item.SalesItemId;
                    document.getElementById('<%= ButtonAjaxSearchDiagnosticNonModal.ClientID %>').click();
                }
            }
        })
        .focus(function () {
            $(this).autocomplete("search");
        })
        .autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append('<table style="width:475px; border-bottom:1px solid lightgrey;">'
                    + '<tr>'
                    //+ '<td style="width:20%; padding:5px; vertical-align:top;">' + item.SalesItemCode + '</td>'
                    + '<td style="width:100%; padding:5px; vertical-align:top;">' + item.SalesItemName + '</td>'
                    + '</tr>'
                    + '</table>')
                .appendTo(ul);
            };

        // add item on txtItemDiag future order
        $("#MainContent_StdPlanning_txtItem_DIAG_FutureOrder").autocomplete({
            source: "../Control_Template/AutoCompleteDiagProc.aspx?type=4",
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<table style="width:475px; border-bottom:1px solid lightgrey; background-color:#f4f4f4;">'
                    + '<tr>'
                    //+ '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Item Code </td>'
                    + '<td style="width:100%; padding:5px; vertical-align:top; font-weight:bold;">Future Order Diagnostic</td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.SalesItemId);

                    document.getElementById('<%= HF_ItemSelectedDiagFutureOrderNonModal.ClientID %>').value = ui.item.SalesItemId;
                    document.getElementById('<%= ButtonAjaxSearchDiagnosticFutureOrderNonModal.ClientID %>').click();
                }
            }
        })
        .focus(function () {
            $(this).autocomplete("search");
        })
        .autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append('<table style="width:475px; border-bottom:1px solid lightgrey;">'
                    + '<tr>'
                    //+ '<td style="width:20%; padding:5px; vertical-align:top;">' + item.SalesItemCode + '</td>'
                    + '<td style="width:100%; padding:5px; vertical-align:top;">' + item.SalesItemName + '</td>'
                    + '</tr>'
                    + '</table>')
                .appendTo(ul);
            };


        // -------------------------- Procedure ---------------------------//
        // add item on txtItemProcedure non future order
        $("#MainContent_StdPlanning_txtItem_PROC").autocomplete({
            source: "../Control_Template/AutoCompleteDiagProc.aspx?type=5",
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<table style="width:475px; border-bottom:1px solid lightgrey; background-color:#f4f4f4;">'
                    + '<tr>'
                    //+ '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Item Code </td>'
                    + '<td style="width:100%; padding:5px; vertical-align:top; font-weight:bold;">Procedure</td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.SalesItemId);

                    document.getElementById('<%= HF_ItemSelectedProcedureNonModal.ClientID %>').value = ui.item.SalesItemId;
                    document.getElementById('<%= ButtonAjaxSearchProcedureNonModal.ClientID %>').click();
                }
            }
        })
        .focus(function () {
            $(this).autocomplete("search");
        })
        .autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append('<table style="width:475px; border-bottom:1px solid lightgrey;">'
                    + '<tr>'
                    + '<td style="width:100%; padding:5px; vertical-align:top;">' + item.SalesItemName + '</td>'
                    + '</tr>'
                    + '</table>')
                .appendTo(ul);
            };

        // add item on txtItemProcedure non future order
        $("#MainContent_StdPlanning_txtItem_PROC_FutureOrder").autocomplete({
            source: "../Control_Template/AutoCompleteDiagProc.aspx?type=5",
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<table style="width:475px; border-bottom:1px solid lightgrey; background-color:#f4f4f4;">'
                    + '<tr>'
                    //+ '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Item Code </td>'
                    + '<td style="width:100%; padding:5px; vertical-align:top; font-weight:bold;">Future Order Procedure</td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.SalesItemId);

                    document.getElementById('<%= HF_ItemSelectedProcedureFutureOrderNonModal.ClientID %>').value = ui.item.SalesItemId;
                    document.getElementById('<%= ButtonAjaxSearchProcedureFutureOrderNonModal.ClientID %>').click();
                }
            }
        })
        .focus(function () {
            $(this).autocomplete("search");
        })
        .autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append('<table style="width:475px; border-bottom:1px solid lightgrey;">'
                    + '<tr>'
                    + '<td style="width:100%; padding:5px; vertical-align:top;">' + item.SalesItemName + '</td>'
                    + '</tr>'
                    + '</table>')
                .appendTo(ul);
        };

    }


    function RacikanSuggestionSOAP() {
        var payertype = document.getElementById('<%=hfPayerType.ClientID%>').value;
        var chkformula = document.getElementById('<%=hfchkformularium.ClientID%>').value;
        var stokupdate = document.getElementById('<%= HF_Label_updatestockdrug.ClientID %>').value;

        $("#MainContent_StdPlanning_txtinput_ItemRacikan_AC").autocomplete({
            source: "../Control_Template/AutoCompleteDrugSOAP.aspx?payertype=" + payertype + "&chkformula=" + chkformula,
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<div style="width:100%; text-align:right; padding:5px;"><label id="lblbhs_updatestockracikan_ac" style="color: #c43d32; font-size: 14px; font-weight: bold;">Stok update per tanggal ' + stokupdate + '</label></div>'
                    + '<table style="width:800px; border-bottom:1px solid lightgrey; background-color:#f4f4f4;">'
                    + '<tr>'
                    + '<td style="width:40%; padding:5px; vertical-align:top; font-weight:bold;"> Items </td>'
                    + '<td style="width:40%; padding:5px; vertical-align:top; font-weight:bold;"> Active Ingredients </td>'
                    + '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold; text-align:right; padding-right:15px;"> Quantity </td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.itemId);
                    document.getElementById('<%= HF_flagfocusracikansearch.ClientID %>').value = "searchfocusRacikan";

                    document.getElementById('<%= HF_ItemSelectedracikan.ClientID %>').value = ui.item.itemId;
                    document.getElementById('<%= ButtonAjaxSearchRacikan.ClientID %>').click();
                }
            }
        })
            .focus(function () {
                $(this).autocomplete("search");
            })
            .autocomplete("instance")._renderItem = function (ul, item) {
                if (item.itemQuantity == "0,00" || item.itemQuantity == "0.00") {
                    return $("<li>")
                        .append('<table style="width:800px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemIngredient + '</td>'
                            + '<td style="width:20%; padding:5px; vertical-align:top; text-align:right; padding-right:15px; color:red;">' + item.itemQuantity + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                }
                else {
                    return $("<li>")
                        .append('<table style="width:800px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemIngredient + '</td>'
                            + '<td style="width:20%; padding:5px; vertical-align:top; text-align:right; padding-right:15px;">' + item.itemQuantity + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                }
            };
    }

    function OnClick() {
        if (testpopup.style.display == "none") {
            testpopup.style.display = "";
            //$("[id$='txtItemAdd']").val('click to hide search item');
            $("[id$='txtSearchItem']").focus();
        }
        else {
            $("[id$='txtItemAdd']").val('Add item here...');
            testpopup.style.display = "none";
        }

    }

    function OnClickcons() {
        if (popupcons.style.display == "none") {
            popupcons.style.display = "";
            $("[id$='txtSearchItemcons']").focus();
        }
        else
            popupcons.style.display = "none";
    }

    function OnClickadditionalDrug() {
        if (popupadddrugs.style.display == "none") {
            popupadddrugs.style.display = "";
            $("[id$='txtSearchItemAddDrugs']").focus();
        }
        else
            popupadddrugs.style.display = "none";
    }

    function OnClickadditionalCons() {
        if (popupconsadd.style.display == "none") {
            popupconsadd.style.display = "";
            $("[id$='txtSearchAddItemCons']").focus();
        }
        else
            popupconsadd.style.display = "none";
    }

    function OnClick2() {
        if (testpopup2.style.display == "none") {
            testpopup2.style.display = "";
            $("[id$='find_detail']").focus();
        }
        else
            testpopup2.style.display = "none";
    }

    function showConsumables() {
        var dvPassport = document.getElementById("dvConsumables");
        dvPassport.style.display = "";
        return false;
    }

    function testfind() {
        var dvPassport = document.getElementById("searchlab").value;
        window.find(dvPassport, false, false, true, true, false).focus();
        return true;
    }

    function clicksearch() {
        var dvPassport = document.getElementById("searchlab");
        dvPassport.onclick = testfind('Laboratory');
        //testfind('Laboratory');
    }

    function HideConsumables() {
        var dvPassport = document.getElementById("dvConsumables");
        dvPassport.style.display = "none";
        var dvPassport2 = document.getElementById("dvConsumablesShow");
        dvPassport2.style.display = "";
        return false;
    }

    function HideDivRacikan()
    {
        var dvRac = document.getElementById('<%= dvRacikanShow.ClientID %>');
        if (dvRac != null) {
            dvRac.style.display = "none";
        }
        var dvRacAdd = document.getElementById('<%= dvAdditionalRacikanShow.ClientID %>');
        if (dvRacAdd != null) {
            dvRacAdd.style.display = "none";
        }
    }

    function HideAdditional() {
        var dvadd = document.getElementById("dvAdditionalShow");
        dvadd.style.display = "none";
        var dvadd2 = document.getElementById("dvAddConsumablesShow");
        dvadd2.style.display = "none";
        var dvadd3 = document.getElementById('<%= dvAdditionalRacikanShow.ClientID %>');
        if (dvadd3 != null) {
            dvadd3.style.display = "none";
        }
    }

    function ShowAdditional() {
        var dvadd = document.getElementById("dvAdditionalShow");
        dvadd.style.display = "";
        var dvadd2 = document.getElementById("dvAddConsumablesShow");
        dvadd2.style.display = "";
        var dvadd3 = document.getElementById('<%= dvAdditionalRacikanShow.ClientID %>');
        if (dvadd3 != null) {
            dvadd3.style.display = "";
        }
    }

    function CheckNumeric() {
        return event.keyCode >= 48 && event.keyCode <= 57 || event.keyCode == 46;
    }

    function myFunction() {
        testpopup.style.display = "none";
        return false;
    }

    function txtOnKeyPress() {
        var c = event.keyCode;
        if (c == 13) {
            return true;
        }
    }

    function txtOnKeyPressFalse() {
        var c = event.keyCode;
        if (c == 13) {
            return false;
        }
    }

    function txtOnKeyPressDrugs() {
        var FlagFocus = document.getElementById('<%= HiddenFlagSearchFocus.ClientID %>');
        FlagFocus.value = "drugfocus";

        var c = event.keyCode;
        //kalo ini diaktifkan, pakai onkeydown
        if (c == 13) {
            var Button = "<%=btnfind.ClientID %>";
            document.getElementById(Button).click();
            return false;
        }

        //kalo ini diaktifkan, pakai onkeyup
        <%--if (c != 37 && c != 39) {
            var t = document.getElementById('<%= txtSearchItem.ClientID %>');
            if (t.value.length >= 3 || t.value.length == 0) {
                var Button = "<%=btnfind.ClientID %>";
                document.getElementById(Button).click();
                return false;
            }
        }--%>
    }

    function moveCursorToEnd(el) {
        if (typeof el.selectionStart == "number") {
            el.selectionStart = el.selectionEnd = el.value.length;
        } else if (typeof el.createTextRange != "undefined") {
            el.focus();
            var range = el.createTextRange();
            range.collapse(false);
            range.select();
        }
    }

    function txtOnKeyPressCons() {
        var FlagFocus = document.getElementById('<%= HiddenFlagSearchFocus.ClientID %>');
        FlagFocus.value = "consfocus";

        var c = event.keyCode;
        //kalo ini diaktifkan, pakai onkeydown
        if (c == 13) {
            var Button = "<%=Button2.ClientID %>";
            document.getElementById(Button).click();
            return false;
        }

        //kalo ini diaktifkan, pakai onkeyup
        <%--if (c != 37 && c != 39) {
            var t = document.getElementById('<%= txtSearchItemcons.ClientID %>');
            if (t.value.length >= 3 || t.value.length == 0) {
                 var Button = "<%=Button2.ClientID %>";
                 document.getElementById(Button).click();
                return false;
            }
        }--%>
    }

    function txtOnKeyPressDrugAdd() {
        var FlagFocus = document.getElementById('<%= HiddenFlagSearchFocus.ClientID %>');
        FlagFocus.value = "adddrugfocus";

        var c = event.keyCode;
        if (c == 13) {
            var Button = "<%=Button1.ClientID %>";
            document.getElementById(Button).click();
            return false;
        }
    }

    function txtOnKeyPressConsAdd() {
        var FlagFocus = document.getElementById('<%= HiddenFlagSearchFocus.ClientID %>');
        FlagFocus.value = "addconsfocus";

        var c = event.keyCode;
        if (c == 13) {
            var Button = "<%=Button3.ClientID %>";
            document.getElementById(Button).click();
            return false;
        }
    }

    function validateFloatKeyPress(el) {
        var v = parseFloat(el.value);
        var strv = el.value.split('.');
        var strval = strv[1];
        // alert(strval.length);
        if (strval != null) {
            if (strval.length == 3) {
                if (strval == "000") {
                    el.value = strv[0];
                }
                else
                    el.value = (isNaN(v)) ? '' : v.toFixed(3);
            }
            else if (strval.length == 2) {
                if (strval == "00") {
                    el.value = strv[0];
                }
                else
                    el.value = (isNaN(v)) ? '' : v.toFixed(2);
            }
            else if (strval.length == 1) {
                if (strval == "0") {
                    el.value = strv[0];
                }
                else
                    el.value = (isNaN(v)) ? '' : v.toFixed(2);
            }
            else
                el.value = (isNaN(v)) ? '' : v.toFixed(2);
            //el.value = (isNaN(v)) ? '' : v.toFixed(2);
        }
    }

    function inputLimit(txtbox, limit) {
        if (txtbox.value.length > limit) {
            var str = txtbox.value.substring(0, limit);
            txtbox.value = str;
            return false;
        }
        return true;
    }

    function AutoExpandbyClass() {

        //$('.auto-expand').height(5 + $(this).prop('scrollHeight') + "px");

        $.each($(".auto-expand"), function () {
            var scrollHeight = parseInt($(this).prop('scrollHeight'));
            $(this).height(scrollHeight - 5 + "px");
        });
    }

    function AutoExpand(txtbox) {
        txtbox.style.height = "1px";
        txtbox.style.height = (25 + txtbox.scrollHeight) + "px";
    }

    function minexpand(txtbox) {

        validateclosetagplanning();
        txtbox.style.height = "1px";
        txtbox.style.height = (5 + txtbox.scrollHeight) + "px";
    }

    function validateclosetagplanning() {

        var valuetxtDocNurseNotes = document.getElementById("MainContent_StdPlanning_txtDocNurseNotes").value;
        var res = valuetxtDocNurseNotes.replace("</", "< /");
        $("[id$='txtDocNurseNotes']").val(res);

		var valuetxtplanningotherLab = document.getElementById("MainContent_StdPlanning_txtplanningotherLab").value;
		var restxtplanningotherLab = valuetxtplanningotherLab.replace("</", "< /");
		$("[id$='txtplanningotherLab']").val(restxtplanningotherLab);

		var valuetxtplanningotherRad = document.getElementById("MainContent_StdPlanning_txtplanningotherRad").value;
		var restxtplanningotherRad = valuetxtplanningotherRad.replace("</", "< /");
		$("[id$='txtplanningotherRad']").val(restxtplanningotherRad);

		var valuetxtplanningotherLab_FutureOrder = document.getElementById("MainContent_StdPlanning_txtplanningotherLab_FutureOrder").value;
		var restxtplanningotherLab_FutureOrder = valuetxtplanningotherLab_FutureOrder.replace("</", "< /");
		$("[id$='txtplanningotherLab_FutureOrder']").val(restxtplanningotherLab_FutureOrder);

		var valuetxtplanningotherRad_FutureOrder = document.getElementById("MainContent_StdPlanning_txtplanningotherRad_FutureOrder").value;
		var restxtplanningotherRad_FutureOrder = valuetxtplanningotherRad_FutureOrder.replace("</", "< /");
		$("[id$='txtplanningotherRad_FutureOrder']").val(restxtplanningotherRad_FutureOrder);

        var valuetxtclinicaldiagnosis = document.getElementById("MainContent_StdPlanning_txtclinicaldiagnosis").value;
        var restxtclinicaldiagnosis = valuetxtclinicaldiagnosis.replace("</", "< /");
        $("[id$='txtclinicaldiagnosis']").val(restxtclinicaldiagnosis);

        var valuetxtPresNotes = document.getElementById("MainContent_StdPlanning_txtPresNotes").value;
        var restxtPresNotes = valuetxtPresNotes.replace("</", "< /");
        $("[id$='txtPresNotes']").val(restxtPresNotes);

        var valuetxtAdditionalPresNotes = document.getElementById("MainContent_StdPlanning_txtAdditionalPresNotes").value;
        var restxtAdditionalPresNotes = valuetxtAdditionalPresNotes.replace("</", "< /");
        $("[id$='txtAdditionalPresNotes']").val(restxtAdditionalPresNotes);
    }

    function loseBoxDrug() {
        testpopup.style.display = 'none';
    }

    function loseBoxCons() {
        popupcons.style.display = 'none';
    }

    function loseBoxDrugAdd() {
        popupadddrugs.style.display = 'none';
    }

    function loseBoxConsAdd() {
        popupconsadd.style.display = 'none';
    }

    //window.addEventListener('mouseup', function (e) {
    //    //alert("lvl 1 "+e.target);
    //    //alert("lvl 2 "+e.target.parentNode.id);
    //    //alert("lvl 3 "+e.target.parentNode.parentNode.id);
    //    //alert("lvl 4 "+e.target.parentNode.parentNode.parentNode.id);
    //    //alert("lvl 5 "+e.target.parentNode.parentNode.parentNode.parentNode.id);

    //    if (e.target.parentNode.parentNode != null && e.target.parentNode.parentNode.parentNode != null && e.target.parentNode.parentNode.parentNode.parentNode != null) {
    //        //hide pop up icd when click outside div
    //        if (e.target.parentNode.parentNode.id != "testpopup" && e.target.parentNode.parentNode.parentNode.parentNode.id != "testpopup" && e.target.parentNode.parentNode.parentNode.id != "MainContent_gvw_data") {
    //            testpopup.style.display = 'none';
    //        }

    //        if (e.target.parentNode.parentNode.id != "popupcons" && e.target.parentNode.parentNode.parentNode.id != "popupcons" && e.target.parentNode.parentNode.parentNode.id != "MainContent_gvw_cons") {
    //            popupcons.style.display = 'none';
    //        }

    //        if (e.target.parentNode.parentNode.id != "popupadddrugs" && e.target.parentNode.parentNode.parentNode.parentNode.id != "popupadddrugs" && e.target.parentNode.parentNode.parentNode.id != "MainContent_gvw_add_drugs") {
    //            popupadddrugs.style.display = 'none';
    //        }

    //        if (e.target.parentNode.parentNode.id != "popupconsadd" && e.target.parentNode.parentNode.parentNode.id != "popupconsadd" && e.target.parentNode.parentNode.parentNode.id != "MainContent_gvw_item_cons_additional") {
    //            popupconsadd.style.display = 'none';
    //        }
    //    }

    //});

    //fungsi untuk mengcopy lab rad (modal) ke halaman SOAP
    window.onclick = function (event) {

        var modallab = document.getElementById('myModal');
        var dismisslab = document.getElementById('dismisslab');
        var closelab = document.getElementById('closelab');

        if (modallab != null && dismisslab != null && closelab != null) {

            if (event.target == modallab || event.target.id == dismisslab.id || event.target.id == closelab.id) {

                //alert("modal closed");
                getobjectlist();

                document.getElementById('<%= HF_lab_open.ClientID %>').value = "yes";
                $("[id$='btnvaluecpoe']").click();

                //var a = $("[id$='hfbuilderobject']").val();
                $(".loadlabrad").css("display", "");
            }
        }

        var modalrad = document.getElementById('modalRad');
        var dismissrad = document.getElementById('dismissrad');
        var closerad = document.getElementById('closerad');

        if (modalrad != null && dismissrad != null && closerad != null) {

            if (event.target == modalrad || event.target.id == dismissrad.id || event.target.id == closerad.id) {

                //alert("modal closed");
                getobjectlistradiology();

                document.getElementById('<%= HF_rad_open.ClientID %>').value = "yes";
                $("[id$='btnvalueradiology']").click();
                //var a = $("[id$='hfbuilderobject']").val();

                $(".loadlabrad").css("display", "");
            }
        }

        var modalifma = document.getElementById('modalEditMedicationIframe');
        var modalifhr = document.getElementById('modalEditIllnessIframe');
        if (modalifma != null) {
            if (event.target == modalifma) {
                SaveIframe_MedicationAllergies();
            }
        }

        if (modalifhr != null) {
            if (event.target == modalifhr) {
                SaveIframe_HealthRecord();
            }
        }
    }

    $(document).ready(function () {

        isMandatoryField();

        DrugSuggestionSOAP();
        DrugAdditionalSuggestionSOAP();
        ConsumableSuggestionSOAP();
        ConsumableAdditionalSuggestionSOAP();
        RacikanSuggestionSOAP();

        LabCPOESuggestionSOAP();
        LabCPOESuggestionSOAP_FutureOrder();
        RadCPOESuggestionSOAP();
        RadCPOESuggestionSOAP_FutureOrder();

        // Add Dropdown Diagnostic and procedure
        DiagProcSuggestionSOAPNonModal();


        AutoExpandbyClass();

        LabSetCheckBoxSet();

        //fungsi untuk menjaga style pada saat postback dalam updatepanel
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {

            prm.add_beginRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    var flagLoadingDrug = document.getElementById('<%= HFloadingdrug.ClientID %>');
                    var flagLoadingCons = document.getElementById('<%= HFloadingcons.ClientID %>');
                    var flagLoadingDrug_add = document.getElementById('<%= HFloadingdrug_add.ClientID %>');
                    var flagLoadingCons_add = document.getElementById('<%= HFloadingcons_add.ClientID %>');
                    var flagLoadingCpoe_lab = document.getElementById('<%= HFloadingcpoelab.ClientID %>');
                    var flagLoadingCpoe_rad = document.getElementById('<%= HFloadingcpoerad.ClientID %>');
					var flagLoadingCpoe_lab_FO = document.getElementById('<%= HFloadingcpoelab_FutureOrder.ClientID %>');
                    var flagLoadingCpoe_rad_FO = document.getElementById('<%= HFloadingcpoerad_FutureOrder.ClientID %>');
                    // Diagnostic and procedure
                    var flagLoadingDiagnostic = document.getElementById('<%= HFloadingdiagnostic.ClientID %>');
                    var flagLoadingProcedure = document.getElementById('<%= HFloadingprocedure.ClientID %>');
                    var flagLoadingDiagnostic_FO = document.getElementById('<%= HFloadingdiagnostic_FutureOrder.ClientID %>');
                    var flagLoadingProcedure_FO = document.getElementById('<%= HFloadingprocedure_FutureOrder.ClientID %>');
                    //Date Delete
                    var dp_labFO = document.getElementById('<%= dp_labFutureOrder.ClientID %>');
                    var dp_radFO = document.getElementById('<%= dp_radFutureOrder.ClientID %>');
					var dp_diag = document.getElementById('<%= dp_diag.ClientID%>');
					var dp_proc = document.getElementById('<%= dp_proc.ClientID%>');


                    if (flagLoadingDrug.value == "true") {
                        $(".loadingdrug").show();
                    }
                    if (flagLoadingCons.value == "true") {
                        $(".loadingcons").show();
                    }
                    if (flagLoadingDrug_add.value == "true") {
                        $(".loadingdrugadd").show();
                    }
                    if (flagLoadingCons_add.value == "true") {
                        $(".loadingconsadd").show();
                    }
                    if (flagLoadingCpoe_lab.value == "true") {
                        $(".loadingcpoelab").show();
                    }
                    if (flagLoadingCpoe_rad.value == "true") {
                        $(".loadingcpoerad").show();
                    }
					if (flagLoadingCpoe_lab_FO.value == "true") {
						$(".loadingcpoelab_FutureOrder").show();
					}
					if (flagLoadingCpoe_rad_FO.value == "true") {
						$(".loadingcpoerad_FutureOrder").show();
                    }
                    // Diagnostic and procedure
                    if (flagLoadingDiagnostic.value == "true") {
                        $(".loadingdiagnostic").show();
                    }
                    if (flagLoadingProcedure.value == "true") {
                        $(".loadingprocedure").show();
                    }
                    if (flagLoadingDiagnostic_FO.value == "true") {
                        $(".loadingdiagnostic_FutureOrder").show();
                    }
                    if (flagLoadingProcedure_FO.value == "true") {
                        $(".loadingprocedure_FutureOrder").show();
                    }

                    //Date Delete

                    if (dp_labFO.value != "") {
                        $("#MainContent_StdPlanning_dp_labFutureOrderDelete").css("display", "block");
                    } else {
						$("#MainContent_StdPlanning_dp_labFutureOrderDelete").css("display", "none");
                    }

					if (dp_radFO.value != "") {
						$("#MainContent_StdPlanning_dp_radFutureOrderDelete").css("display", "block");
					} else {
						$("#MainContent_StdPlanning_dp_radFutureOrderDelete").css("display", "none");
                    }

					if (dp_diag.value != "") {
						$("#MainContent_StdPlanning_dp_diagDelete").css("display", "block");
					} else {
						$("#MainContent_StdPlanning_dp_diagDelete").css("display", "none");
					}

					if (dp_proc.value != "") {
						$("#MainContent_StdPlanning_dp_procDelete").css("display", "block");
					} else {
						$("#MainContent_StdPlanning_dp_procDelete").css("display", "none");
					}
                    
                }
            });

            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    //var FlagTab = document.getElementById('<%= HiddenFlagTabSet.ClientID %>');

                    //if (FlagTab.value == "lab") {
                    //    labonclick();
                    //}

                    //if (FlagTab.value == "panel") {
                    //    labpanelonclick();
                    //}



                    DrugSuggestionSOAP();
                    DrugAdditionalSuggestionSOAP();
                    ConsumableSuggestionSOAP();
                    ConsumableAdditionalSuggestionSOAP();
                    RacikanSuggestionSOAP();

                    LabCPOESuggestionSOAP();
                    LabCPOESuggestionSOAP_FutureOrder();
                    RadCPOESuggestionSOAP();
                    RadCPOESuggestionSOAP_FutureOrder();

                    // Diagnostic and procedure 
                    DiagProcSuggestionSOAPNonModal();

					//Date Delete
					var dp_labFO = document.getElementById('<%= dp_labFutureOrder.ClientID %>');
                    var dp_radFO = document.getElementById('<%= dp_radFutureOrder.ClientID %>');
                    var dp_diag = document.getElementById('<%= dp_diag.ClientID%>');
                    var dp_proc = document.getElementById('<%= dp_proc.ClientID%>');

					if (dp_labFO.value != "") {
						$("#MainContent_StdPlanning_dp_labFutureOrderDelete").css("display", "block");
					} else {
						$("#MainContent_StdPlanning_dp_labFutureOrderDelete").css("display", "none");
					}

					if (dp_radFO.value != "") {
						$("#MainContent_StdPlanning_dp_radFutureOrderDelete").css("display", "block");
					} else {
						$("#MainContent_StdPlanning_dp_radFutureOrderDelete").css("display", "none");
                    }

					if (dp_diag.value != "") {
						$("#MainContent_StdPlanning_dp_diagDelete").css("display", "block");
					} else {
						$("#MainContent_StdPlanning_dp_diagDelete").css("display", "none");
                    }

					if (dp_proc.value != "") {
						$("#MainContent_StdPlanning_dp_procDelete").css("display", "block");
					} else {
						$("#MainContent_StdPlanning_dp_procDelete").css("display", "none");
					}

                    //flag pencarian NEW
                    var FlagSearchFocus_drug = document.getElementById('<%= HF_flagfocusdrugsearch.ClientID %>');
                    var FlagSearchFocus_adddrug = document.getElementById('<%= HF_flagfocusdrugsearch_add.ClientID %>');
                    var FlagSearchFocus_cons = document.getElementById('<%= HF_flagfocusconssearch.ClientID %>');
                    var FlagSearchFocus_addcons = document.getElementById('<%= HF_flagfocusconssearch_add.ClientID %>');

                    if (FlagSearchFocus_drug.value == "searchfocusDrug") {
                        //$("#txtItemAdd_AC").focus();
                        document.getElementById('<%= HF_flagfocusdrugsearch.ClientID %>').value = "";
                    }
                    if (FlagSearchFocus_adddrug.value == "searchfocusDrug_add") {
                        //$("#txtItemAdd_AC_additional").focus();
                        document.getElementById('<%= HF_flagfocusdrugsearch_add.ClientID %>').value = "";
                    }
                    if (FlagSearchFocus_cons.value == "searchfocusCons") {
                        //$("#txtItemCons_AC").focus();
                        document.getElementById('<%= HF_flagfocusconssearch.ClientID %>').value = "";
                    }
                    if (FlagSearchFocus_addcons.value == "searchfocusCons_add") {
                        //$("#txtItemCons_AC_additional").focus();
                        document.getElementById('<%= HF_flagfocusconssearch_add.ClientID %>').value = "";
                    }

                    //flag pencarian OLD
                    var FlagFocus = document.getElementById('<%= HiddenFlagSearchFocus.ClientID %>');

                    if (FlagFocus.value == "drugfocus") {
                        document.getElementById('<%= txtSearchItem.ClientID %>').focus();
                        moveCursorToEnd(document.getElementById('<%= txtSearchItem.ClientID %>'));
                    }
                    else if (FlagFocus.value == "consfocus") {
                        document.getElementById('<%= txtSearchItemcons.ClientID %>').focus();
                        moveCursorToEnd(document.getElementById('<%= txtSearchItemcons.ClientID %>'));
                    }
                    else if (FlagFocus.value == "adddrugfocus") {
                        document.getElementById('<%= txtSearchItemAddDrugs.ClientID %>').focus();
                    }
                    else if (FlagFocus.value == "addconsfocus") {
                        document.getElementById('<%= txtSearchAddItemCons.ClientID %>').focus();
                    }

                    switchBahasaSOAP_Planning();
                    isMandatoryField();

                    $(".loadingdrug").hide();
                    document.getElementById('<%= HFloadingdrug.ClientID %>').value = "false";
                    $(".loadingcons").hide();
                    document.getElementById('<%= HFloadingcons.ClientID %>').value = "false";
                    $(".loadingdrugadd").hide();
                    document.getElementById('<%= HFloadingdrug_add.ClientID %>').value = "false";
                    $(".loadingconsadd").hide();
                    document.getElementById('<%= HFloadingcons_add.ClientID %>').value = "false";
                    $(".loadingcpoelab").hide();
                    document.getElementById('<%= HFloadingcpoelab.ClientID %>').value = "false";
                    $(".loadingcpoerad").hide();
                    document.getElementById('<%= HFloadingcpoerad.ClientID %>').value = "false";
					$(".loadingcpoelab_FutureOrder").hide();
					document.getElementById('<%= HFloadingcpoelab_FutureOrder.ClientID %>').value = "false";
                    $(".loadingcpoerad_FutureOrder").hide();
                    document.getElementById('<%= HFloadingcpoerad_FutureOrder.ClientID %>').value = "false";
                    // diagnostic and procedure
                    $(".loadingdiagnostic").hide();
                    document.getElementById('<%= HFloadingdiagnostic.ClientID %>').value = "false";
                    $(".loadingcprocedure").hide();
                    document.getElementById('<%= HFloadingprocedure.ClientID %>').value = "false";
                    $(".loadingdiagnostic_FutureOrder").hide();
                    document.getElementById('<%= HFloadingdiagnostic_FutureOrder.ClientID %>').value = "false";
                    $(".loadingprocedure_FutureOrder").hide();
                    document.getElementById('<%= HFloadingprocedure_FutureOrder.ClientID %>').value = "false";

                    if (document.getElementById('<%= HF_lab_open.ClientID %>').value == "yes") {
                        document.getElementById('<%= HF_lab_open.ClientID %>').value = "no"
                        //document.getElementById('<%= btnCloseLab.ClientID %>').click();
                        hideloading();
                    }

                    if (document.getElementById('<%= HF_rad_open.ClientID %>').value == "yes") {
                        document.getElementById('<%= HF_rad_open.ClientID %>').value = "no"
                        //document.getElementById('<%= btnCloseRad.ClientID %>').click();
                        hideloading();
                    }

					if (document.getElementById('<%= HF_lab_open_FutureOrder.ClientID %>').value == "yes") {
						document.getElementById('<%= HF_lab_open_FutureOrder.ClientID %>').value = "no"
                        //document.getElementById('<%= btnCloseLab.ClientID %>').click();
                        hideloading();
                    }

                    if (document.getElementById('<%= HF_rad_open_FutureOrder.ClientID %>').value == "yes") {
                        document.getElementById('<%= HF_rad_open_FutureOrder.ClientID %>').value = "no"
						//document.getElementById('<%= btnCloseRad.ClientID %>').click();
						hideloading();
					}

                    AutoExpandbyClass();
                }
            });
        };
    });

    function setflagloading(item, status) {
        if (item == "drug") {
            document.getElementById('<%= HFloadingdrug.ClientID %>').value = "true";
        }
        else if (item == "drug_add") {
            document.getElementById('<%= HFloadingdrug_add.ClientID %>').value = "true";
        }
        else if (item == "cons") {
            document.getElementById('<%= HFloadingcons.ClientID %>').value = "true";
        }
        else if (item == "cons_add") {
            document.getElementById('<%= HFloadingcons_add.ClientID %>').value = "true";
        }
        else if (item == "cpoelab") {
            document.getElementById('<%= HFloadingcpoelab.ClientID %>').value = "true";
        }
        else if (item == "cpoerad") {
            document.getElementById('<%= HFloadingcpoerad.ClientID %>').value = "true";
        }
        else if (item == "cpoelab_FutureOrder") {
			document.getElementById('<%= HFloadingcpoelab_FutureOrder.ClientID %>').value = "true";
        }
        else if (item == "cpoerad_FutureOrder") {
			document.getElementById('<%= HFloadingcpoerad_FutureOrder.ClientID %>').value = "true";
        }
        // Diagnostic and Procedure
        else if (item == "diagnostic") {
            document.getElementById('<%= HFloadingdiagnostic.ClientID %>').value = "true";
        }
        else if (item == "procedure") {
            document.getElementById('<%= HFloadingprocedure.ClientID %>').value = "true";
        }
        else if (item == "diagnostic_FutureOrder") {
            document.getElementById('<%= HFloadingdiagnostic_FutureOrder.ClientID %>').value = "true";
        }
        else if (item == "procedure_FutureOrder") {
            document.getElementById('<%= HFloadingprocedure_FutureOrder.ClientID %>').value = "true";
        }
    }

    function labonclick() {
        divlabset.style.display = "";
        divlab.style.backgroundColor = "#1a2269"
        divlab.style.color = "white"
        divpanelset.style.display = "none";
        divpanel.style.backgroundColor = "white"
        divpanel.style.color = "black"
        document.getElementById('<%= HiddenFlagTabSet.ClientID %>').value = "lab";
    }

    function labpanelonclick() {
        divpanelset.style.display = "";
        divpanel.style.backgroundColor = "#1a2269"
        divpanel.style.color = "white"
        divlabset.style.display = "none";
        divlab.style.backgroundColor = "white"
        divlab.style.color = "black"
        document.getElementById('<%= HiddenFlagTabSet.ClientID %>').value = "panel";
    }

    function switchBahasaSOAP_Planning() {
        var bahasa = document.getElementById('<%=HFisBahasaSOAP_Planning.ClientID%>').value;
        var flagRacikan = document.getElementById('<%=HFflagRacikan.ClientID%>').value;

        if (bahasa == "ENG") {
            document.getElementById('lblbhs_othersnotes').innerHTML = "Doctor Notes to Nurse";
            document.getElementById('lblbhs_othersnotes2').innerHTML = "Nurse Notes";

            document.getElementById('lblbhs_laboratory').innerHTML = "Laboratory";
			document.getElementById('lblbhs_laboratory_FutureOrder').innerHTML = "Future Laboratory";
			document.getElementById('lblbhs_nolabadded').innerHTML = "No Lab Added";
			document.getElementById('lblbhs_nolabadded_FutureOrder').innerHTML = "No Lab Added";
			document.getElementById('lblbhs_addlabhere').innerHTML = "Add Lab Here";
			document.getElementById('lblbhs_addlabhere_FutureOrder').innerHTML = "Add Lab Here";
			document.getElementById('lblbhs_radiology').innerHTML = "Radiology";
			document.getElementById('lblbhs_radiology_FutureOrder').innerHTML = "Future Radiology";
			document.getElementById('lblbhs_noradiologyadded').innerHTML = "No radiology added";
			document.getElementById('lblbhs_noradiologyadded_FutureOrder').innerHTML = "No radiology added";
			document.getElementById('lblbhs_addradiologyhere').innerHTML = "Add Radiology Here";
			document.getElementById('lblbhs_addradiologyhere_FutureOrder').innerHTML = "Add Radiology Here";
			document.getElementById('lblbhs_othersLab').innerHTML = "Others Lab";
			document.getElementById('lblbhs_othersRad').innerHTML = "Others Rad";
			document.getElementById('lblbhs_othersLab_FutureOrder').innerHTML = "Others Future Lab";
			document.getElementById('lblbhs_othersRad_FutureOrder').innerHTML = "Others Future Rad";
			document.getElementById('lblbhs_clinicaldiag').innerHTML = "Clinical Diagnosis";
			document.getElementById('lblbhs_clinicaldiag_FutureOrder').innerHTML = "Clinical Diagnosis";

            document.getElementById('lblbhs_allergies').innerHTML = "Allergies";
            document.getElementById('lblbhs_drugs').innerHTML = "Drugs";
            document.getElementById('lblbhs_nodrugallergy').innerHTML = "No Drug Allergy";
            document.getElementById('lblbhs_food').innerHTML = "Food";
            document.getElementById('lblbhs_nofoodallergy').innerHTML = "No Food Allergy";
            document.getElementById('lblbhs_otheralergi').innerHTML = "Others";
            document.getElementById('lblbhs_nootherallergy').innerHTML = "No Other Allergy";
            document.getElementById('lblbhs_routinemedication').innerHTML = "Current/Routine Medication";
            document.getElementById('lblbhs_noroutinemedication').innerHTML = "No Routine Medication";

            document.getElementById('lblbhs_drugprescription').innerHTML = "Drugs Prescription";
            document.getElementById('lblbhs_drugoutsideformularium').innerHTML = "Drugs Outside Formularium";

            var tabledrug = document.getElementById("<%=gvw_drug.ClientID %>");
            if (tabledrug != null) {
                var headers = tabledrug.getElementsByTagName('th');

                headers[0].innerText = "\xa0\xa0\xa0Item";
                headers[1].innerText = "Text";
                headers[2].innerText = "Dose";
                headers[3].innerText = "Frequency";
                headers[4].innerText = "Route";
                headers[5].innerText = "Instruction";
                headers[6].innerText = "Qty";
                headers[7].innerText = "UoM";
                headers[9].innerText = "Routine";
            }

            var tabledata = document.getElementById("<%=gvw_data.ClientID %>");
            if (tabledata != null) {
                var headers = tabledata.getElementsByTagName('th');

                if (headers.length != 0) {
                    headers[0].innerText = "Item";
                    headers[1].innerText = "Active Ingredients";
                    headers[2].innerText = "Quantity";
                }
            }

            document.getElementById('lblbhs_prescriptionnotes').innerHTML = "Prescription Notes";
            document.getElementById('lblbhs_pharmacistnotes').innerHTML = "Pharmacist Notes";

            document.getElementById('lblbhs_consumables').innerHTML = "Consumables";

            var tableconsumables = document.getElementById("<%=gvw_consumables.ClientID %>");
            if (tableconsumables != null) {
                var headers = tableconsumables.getElementsByTagName('th');

                headers[0].innerText = "\xa0\xa0\xa0Item";
                headers[1].innerText = "Qty";
                headers[2].innerText = "UoM";
                headers[3].innerText = "Instruction";
            }

            var tablecons = document.getElementById("<%=gvw_cons.ClientID %>");
            if (tablecons != null) {
                var headers = tablecons.getElementsByTagName('th');

                if (headers.length != 0) {
                    headers[0].innerText = "Item";
                    headers[1].innerText = "Quantity";
                }
            }

            document.getElementById('lblbhs_additionaldrugsprescription').innerHTML = "Additional Drugs Prescription";

            var tabledrugadd = document.getElementById("<%=gvwAdditionalDrugs.ClientID %>");
            if (tabledrugadd != null) {
                var headers = tabledrugadd.getElementsByTagName('th');

                headers[0].innerText = "\xa0\xa0\xa0Item";
                headers[1].innerText = "Text";
                headers[2].innerText = "Dose";
                headers[3].innerText = "Frequency";
                headers[4].innerText = "Route";
                headers[5].innerText = "Instruction";
                headers[6].innerText = "Qty";
                headers[7].innerText = "UoM";
                headers[9].innerText = "Routine";
            }

            var tabledataadd = document.getElementById("<%=gvw_add_drugs.ClientID %>");
            if (tabledataadd != null) {
                var headers = tabledataadd.getElementsByTagName('th');

                if (headers.length != 0) {
                    headers[0].innerText = "Item";
                    headers[1].innerText = "Quantity";
                }
            }

            document.getElementById('lblbhs_additionalprescriptionnotes').innerHTML = "Additional Prescription Notes";
            document.getElementById('lblbhs_additionalpharmacistnotes').innerHTML = "Additional Pharmacist Notes";
            document.getElementById('lblbhs_additionalconsumables').innerHTML = "Additional Consumables";

            var tableconsumablesadd = document.getElementById("<%=gvw_add_cons.ClientID %>");
            if (tableconsumablesadd != null) {
                var headers = tableconsumablesadd.getElementsByTagName('th');

                headers[0].innerText = "\xa0\xa0\xa0Item";
                headers[1].innerText = "Qty";
                headers[2].innerText = "UoM";
                headers[3].innerText = "Instruction";
            }

            var tableconsadd = document.getElementById("<%=gvw_item_cons_additional.ClientID %>");
            if (tableconsadd != null) {
                var headers = tableconsadd.getElementsByTagName('th');

                if (headers.length != 0) {
                    headers[0].innerText = "Item";
                    headers[1].innerText = "Quantity";
                }
            }

            //document.getElementById('lblbhs_frequentlyuseddrugs').innerHTML = "Frequently Used Drugs";
            //document.getElementById('lblbhs_orderset').innerHTML = "Order Set";
            //document.getElementById('lblbhs_modalradiology').innerHTML = "Radiology";
            //document.getElementById('lblbhs_modallaboratory').innerHTML = "Laboratory";
            //document.getElementById('lblbhs_tabclinical').innerHTML = "Clinical Pathology";
            //document.getElementById('lblbhs_tabmicro').innerHTML = "Microbiology";
            //document.getElementById('lblbhs_tabcito').innerHTML = "Clinical Pathology (CITO)";
            //document.getElementById('lblbhs_tabanatomi').innerHTML = "Anatomical Pathology";

            if (flagRacikan == "true") {
                var tableracheader = document.getElementById("<%=gvw_racikan_header.ClientID %>");
                if (tableracheader != null) {
                    document.getElementById('lblbhs_racik_compoundname').innerHTML = "Compound Name";
                    //document.getElementById('lblbhs_racik_dose').innerHTML = "Text";
                    document.getElementById('lblbhs_racik_doseuom').innerHTML = "Dose";
                    document.getElementById('lblbhs_racik_freq').innerHTML = "Frequency";
                    document.getElementById('lblbhs_racik_route').innerHTML = "Route";
                    document.getElementById('lblbhs_racik_instruction').innerHTML = "Instruction";
                    document.getElementById('lblbhs_racik_qty').innerHTML = "Qty";
                    document.getElementById('lblbhs_racik_uom').innerHTML = "UoM";
                    document.getElementById('lblbhs_racik_iter').innerHTML = "Iter";
                    document.getElementById('lblbhs_racik_action').innerHTML = "Action";
                    //document.getElementById('lblbhs_racik_note').innerHTML = "Compound Instruction For Pharmacy";
                }
                document.getElementById('lblbhs_racikan').innerHTML = "Compound Prescription";
                document.getElementById('lblbhs_racik_addnew').innerHTML = "Add New Compound";

                var tableracheader_add = document.getElementById("<%=gvw_racikan_header_add.ClientID %>");
                if (tableracheader_add != null) {
                    document.getElementById('lblbhs_racik_compoundname_add').innerHTML = "Compound Name";
                    //document.getElementById('lblbhs_racik_dose_add').innerHTML = "Text";
                    document.getElementById('lblbhs_racik_doseuom_add').innerHTML = "Dose";
                    document.getElementById('lblbhs_racik_freq_add').innerHTML = "Frequency";
                    document.getElementById('lblbhs_racik_route_add').innerHTML = "Route";
                    document.getElementById('lblbhs_racik_instruction_add').innerHTML = "Instruction";
                    document.getElementById('lblbhs_racik_qty_add').innerHTML = "Qty";
                    document.getElementById('lblbhs_racik_uom_add').innerHTML = "UoM";
                    document.getElementById('lblbhs_racik_iter_add').innerHTML = "Iter";
                    document.getElementById('lblbhs_racik_action_add').innerHTML = "Action";
                    //document.getElementById('lblbhs_racik_note_add').innerHTML = "Compound Instruction For Pharmacy";
                }
                document.getElementById('lblbhs_racikan_add').innerHTML = "Compound Prescription";
                document.getElementById('lblbhs_racik_addnew_add').innerHTML = "Add New Compound";
            }

            document.getElementById('lblbhs_racikmodal_compoundname').innerHTML = "Compound Name";
            document.getElementById('lblbhs_racikmodal_dose').innerHTML = "Text";
            document.getElementById('lblbhs_racikmodal_doseuom').innerHTML = "Dose";
            document.getElementById('lblbhs_racikmodal_freq').innerHTML = "Frequency";
            document.getElementById('lblbhs_racikmodal_route').innerHTML = "Route";
            document.getElementById('lblbhs_racikmodal_instruction').innerHTML = "Instruction";
            document.getElementById('lblbhs_racikmodal_qty').innerHTML = "Qty";
            document.getElementById('lblbhs_racikmodal_uom').innerHTML = "UoM";
            document.getElementById('lblbhs_racikmodal_iter').innerHTML = "Iter";

            // Diagnostic and Procedure language
            $('#lblbhs_diagnostic').html('Diagnostic');
            $('#lblbhs_procedure').html('Procedure');
            $('#lblbhs_nolabadded_procedure').html('No Diagnostic Added');
            $('#lblbhs_nolabadded_diagnostic').html('No Procedure Added');
            $('#lblbhs_nolabadded_diagnostic_futureorder').html('No Diagnostic Added');
            $('#lblbhs_nolabadded_procedure_futureorder').html('No Procedure Added');

            $('#lblbhs_othersDiagnostic').html('Others Diagnostic');
            $('#lblbhs_othersProcedure').html('Others Procedure');
            $('#lblbhs_othersDiag_FutureOrder').html('Future Order Others Diagnostic');
            $('#lblbhs_othersProd_FutureOrder').html('Others Order Procedure');

            $('#lblbhs_laboratory_FutureOrder_Diagnostic').html('Future Diagnostic');
            $('#lblbhs_laboratory_FutureOrder_Procedure').html('Future Procedure');
        }
        else if (bahasa == "IND") {


            //Diagnostic and Procedure ID language
            document.getElementById('lblbhs_diagnostic').innerHTML = "Diagnostik";
            document.getElementById('lblbhs_procedure').innerHTML = "Prosedur";
            document.getElementById('lblbhs_nolabadded_procedure').innerHTML = "Tidak ada diagnostik yg ditambahkan";
            document.getElementById('lblbhs_nolabadded_diagnostic').innerHTML = "Tidak ada prosedur yg ditambahkan";
            document.getElementById('lblbhs_nolabadded_diagnostic_futureorder').innerHTML = "Tidak ada diagnostik yg ditambahkan";
            document.getElementById('lblbhs_nolabadded_procedure_futureorder').innerHTML = "Tidak ada prosedur yg ditambahkan";
            document.getElementById('lblbhs_othersDiagnostic').innerHTML = "Diagnostik lainnya";
            document.getElementById('lblbhs_othersProcedure').innerHTML = "Prosedur lainnya";
            document.getElementById('lblbhs_othersDiag_FutureOrder').innerHTML = "Diagnostik lainnya Selanjutnya";
            document.getElementById('lblbhs_othersProd_FutureOrder').innerHTML = "Prosedur lainnya Selanjutnya";
            document.getElementById('lblbhs_laboratory_FutureOrder_Diagnostic').innerHTML = "Diagnostik selanjutnya";
            document.getElementById('lblbhs_laboratory_FutureOrder_Procedure').innerHTML = "Prosedur selanjutnya";
            // End Diagnostic and procedure ID language

            document.getElementById('lblbhs_othersnotes').innerHTML = "Pesan Dokter untuk Perawat";
            document.getElementById('lblbhs_othersnotes2').innerHTML = "Pesan Perawat";

            document.getElementById('lblbhs_laboratory').innerHTML = "Laboratorium";
            document.getElementById('lblbhs_nolabadded').innerHTML = "Belum ada data Lab";
            document.getElementById('lblbhs_addlabhere').innerHTML = "Tambahkan";
            document.getElementById('lblbhs_radiology').innerHTML = "Radiologi";
            document.getElementById('lblbhs_noradiologyadded').innerHTML = "Belum ada data Radiologi";
            document.getElementById('lblbhs_addradiologyhere').innerHTML = "Tambahkan";
            //document.getElementById('lblbhs_others').innerHTML = "Lain-lain";
            document.getElementById('lblbhs_clinicaldiag').innerHTML = "Diagnosa Klinis";

            document.getElementById('lblbhs_allergies').innerHTML = "Alergi";
            document.getElementById('lblbhs_drugs').innerHTML = "Obat";
            document.getElementById('lblbhs_nodrugallergy').innerHTML = "Tidak ada alergi Obat";
            document.getElementById('lblbhs_food').innerHTML = "Makanan";
            document.getElementById('lblbhs_nofoodallergy').innerHTML = "Tidak ada alergi Makanan";
            document.getElementById('lblbhs_otheralergi').innerHTML = "Lain-lain";
            document.getElementById('lblbhs_nootherallergy').innerHTML = "Tidak ada alergi Lain-lain";
            document.getElementById('lblbhs_routinemedication').innerHTML = "Pengobatan Saat Ini";
            document.getElementById('lblbhs_noroutinemedication').innerHTML = "Tidak ada Pengobatan Rutin";

            document.getElementById('lblbhs_drugprescription').innerHTML = "Resep Obat";
            document.getElementById('lblbhs_drugoutsideformularium').innerHTML = "Obat diluar Formularium";

            var tabledrug = document.getElementById("<%=gvw_drug.ClientID %>");
            if (tabledrug != null) {
                var headers = tabledrug.getElementsByTagName('th');

                headers[0].innerText = "\xa0\xa0\xa0Obat";
                headers[1].innerText = "Teks";
                headers[2].innerText = "Dosis";
                headers[3].innerText = "Frekuensi";
                headers[4].innerText = "Rute";
                headers[5].innerText = "Instruksi";
                headers[6].innerText = "Jml";
                headers[7].innerText = "Unit";
                headers[9].innerText = "Rutin";
            }

            var tabledata = document.getElementById("<%=gvw_data.ClientID %>");
            if (tabledata != null) {
                var headers = tabledata.getElementsByTagName('th');

                if (headers.length != 0) {
                    headers[0].innerText = "Obat";
                    headers[1].innerText = "Bahan Aktif";
                    headers[2].innerText = "Jumlah";
                }
            }

            document.getElementById('lblbhs_prescriptionnotes').innerHTML = "Catatan Resep";
            document.getElementById('lblbhs_pharmacistnotes').innerHTML = "Catatan Farmasi";
            document.getElementById('lblbhs_consumables').innerHTML = "Alat Kesehatan";

            var tableconsumables = document.getElementById("<%=gvw_consumables.ClientID %>");
            if (tableconsumables != null) {
                var headers = tableconsumables.getElementsByTagName('th');

                headers[0].innerText = "\xa0\xa0\xa0Alat";
                headers[1].innerText = "Jml";
                headers[2].innerText = "Unit";
                headers[3].innerText = "Instruksi";
            }

            var tablecons = document.getElementById("<%=gvw_cons.ClientID %>");
            if (tablecons != null) {
                var headers = tablecons.getElementsByTagName('th');

                if (headers.length != 0) {
                    headers[0].innerText = "Alat";
                    headers[1].innerText = "Jumlah";
                }
            }

            document.getElementById('lblbhs_additionaldrugsprescription').innerHTML = "Resep Obat Tambahan";

            var tabledrugadd = document.getElementById("<%=gvwAdditionalDrugs.ClientID %>");
            if (tabledrugadd != null) {
                var headers = tabledrugadd.getElementsByTagName('th');

                headers[0].innerText = "\xa0\xa0\xa0Obat";
                headers[1].innerText = "Teks";
                headers[2].innerText = "Dosis";
                headers[3].innerText = "Frekuensi";
                headers[4].innerText = "Rute";
                headers[5].innerText = "Instruksi";
                headers[6].innerText = "Jml";
                headers[7].innerText = "Unit";
                headers[9].innerText = "Rutin";
            }

            var tabledataadd = document.getElementById("<%=gvw_add_drugs.ClientID %>");
            if (tabledataadd != null) {
                var headers = tabledataadd.getElementsByTagName('th');

                if (headers.length != 0) {
                    headers[0].innerText = "Obat";
                    headers[1].innerText = "Jumlah";
                }
            }

            document.getElementById('lblbhs_additionalprescriptionnotes').innerHTML = "Catatan Resep Tambahan";
            document.getElementById('lblbhs_additionalpharmacistnotes').innerHTML = "Catatan Farmasi Tambahan";
            document.getElementById('lblbhs_additionalconsumables').innerHTML = "Alat Kesehatan Tambahan";

            var tableconsumablesadd = document.getElementById("<%=gvw_add_cons.ClientID %>");
            if (tableconsumablesadd != null) {
                var headers = tableconsumablesadd.getElementsByTagName('th');

                headers[0].innerText = "\xa0\xa0\xa0Alat";
                headers[1].innerText = "Jml";
                headers[2].innerText = "Unit";
                headers[3].innerText = "Instruksi";
            }

            var tableconsadd = document.getElementById("<%=gvw_item_cons_additional.ClientID %>");
            if (tableconsadd != null) {
                var headers = tableconsadd.getElementsByTagName('th');

                if (headers.length != 0) {
                    headers[0].innerText = "Alat";
                    headers[1].innerText = "Jumlah";
                }
            }

            //document.getElementById('lblbhs_frequentlyuseddrugs').innerHTML = "Obat yang sering digunakan";
            //document.getElementById('lblbhs_orderset').innerHTML = "Paket Obat";
            //document.getElementById('lblbhs_modalradiology').innerHTML = "Radiologi";
            //document.getElementById('lblbhs_modallaboratory').innerHTML = "Laboratorium";
            //document.getElementById('lblbhs_tabclinical').innerHTML = "Patologi Klinik";
            //document.getElementById('lblbhs_tabmicro').innerHTML = "Mikrobiologi";
            //document.getElementById('lblbhs_tabcito').innerHTML = "Patologi Klinik (CITO)";
            //document.getElementById('lblbhs_tabanatomi').innerHTML = "Patologi Anatomi";

            if (flagRacikan == "true") {
                var tableracheader = document.getElementById("<%=gvw_racikan_header.ClientID %>");
                if (tableracheader != null) {
                    document.getElementById('lblbhs_racik_compoundname').innerHTML = "Nama Racikan";
                    //document.getElementById('lblbhs_racik_dose').innerHTML = "Teks";
                    document.getElementById('lblbhs_racik_doseuom').innerHTML = "Dosis";
                    document.getElementById('lblbhs_racik_freq').innerHTML = "Frekuensi";
                    document.getElementById('lblbhs_racik_route').innerHTML = "Rute";
                    document.getElementById('lblbhs_racik_instruction').innerHTML = "Instruksi";
                    document.getElementById('lblbhs_racik_qty').innerHTML = "Jml";
                    document.getElementById('lblbhs_racik_uom').innerHTML = "Unit";
                    document.getElementById('lblbhs_racik_iter').innerHTML = "Iter";
                    document.getElementById('lblbhs_racik_action').innerHTML = "Aksi";
                    //document.getElementById('lblbhs_racik_note').innerHTML = "Instruksi Racikan Untuk Farmasi";
                }
                document.getElementById('lblbhs_racikan').innerHTML = "Resep Racikan";
                document.getElementById('lblbhs_racik_addnew').innerHTML = "Tambah Racikan Baru";

                var tableracheader_add = document.getElementById("<%=gvw_racikan_header_add.ClientID %>");
                if (tableracheader_add != null) {
                    document.getElementById('lblbhs_racik_compoundname_add').innerHTML = "Nama Racikan";
                    //document.getElementById('lblbhs_racik_dose_add').innerHTML = "Teks";
                    document.getElementById('lblbhs_racik_doseuom_add').innerHTML = "Dosis";
                    document.getElementById('lblbhs_racik_freq_add').innerHTML = "Frekuensi";
                    document.getElementById('lblbhs_racik_route_add').innerHTML = "Rute";
                    document.getElementById('lblbhs_racik_instruction_add').innerHTML = "Instruksi";
                    document.getElementById('lblbhs_racik_qty_add').innerHTML = "Jml";
                    document.getElementById('lblbhs_racik_uom_add').innerHTML = "Unit";
                    document.getElementById('lblbhs_racik_iter_add').innerHTML = "Iter";
                    document.getElementById('lblbhs_racik_action_add').innerHTML = "Aksi";
                    //document.getElementById('lblbhs_racik_note_add').innerHTML = "Instruksi Racikan Untuk Farmasi";
                }
                document.getElementById('lblbhs_racikan_add').innerHTML = "Resep Racikan";
                document.getElementById('lblbhs_racik_addnew_add').innerHTML = "Tambah Racikan Baru";
            }

            document.getElementById('lblbhs_racikmodal_compoundname').innerHTML = "Nama Racikan";
            document.getElementById('lblbhs_racikmodal_dose').innerHTML = "Teks";
            document.getElementById('lblbhs_racikmodal_doseuom').innerHTML = "Dosis";
            document.getElementById('lblbhs_racikmodal_freq').innerHTML = "Frekuensi";
            document.getElementById('lblbhs_racikmodal_route').innerHTML = "Rute";
            document.getElementById('lblbhs_racikmodal_instruction').innerHTML = "Instruksi";
            document.getElementById('lblbhs_racikmodal_qty').innerHTML = "Jml";
            document.getElementById('lblbhs_racikmodal_uom').innerHTML = "Unit";
            document.getElementById('lblbhs_racikmodal_iter').innerHTML = "Iter";


            

        }
    }

    //fungsi hidden animasi loading, dipanggil di codebehind saat modal rad lab diclose
    function hideloading() {
        $(".loadlabrad").css("display", "none");
        isMandatoryField();
        //backupSOAP();
    }

    //pengecekan field mandatory
    function isMandatoryField() {
        //cek field lab rad jika label empty ada yg display none, berarti mandatory
		if (document.getElementById('<%= labempty.ClientID %>').style.display == "" && document.getElementById('<%= radempty.ClientID %>').style.display == "" && document.getElementById('<%= txtplanningotherLab.ClientID %>').value == "" && document.getElementById('<%= txtplanningotherRad.ClientID %>').value == "") {
            document.getElementById('<%= HFmandatoryCD.ClientID %>').value = "";
            document.getElementById('<%= LabelmandatoryCD.ClientID %>').innerHTML = "";
            document.getElementById('<%= LabelmandatoryCD.ClientID %>').style.color = "black";
        }
        else {
            document.getElementById('<%= HFmandatoryCD.ClientID %>').value = "*";
            document.getElementById('<%= LabelmandatoryCD.ClientID %>').innerHTML = "*";
            document.getElementById('<%= LabelmandatoryCD.ClientID %>').style.color = "red";
        }
    }

    function setFocusMandatory() {
        var mandatCD = document.getElementById('<%= txtclinicaldiagnosis.ClientID %>');
        mandatCD.focus();
        mandatCD.placeholder = "This field is mandatory..."
        mandatCD.classList.add("placeholderred");
    }

    function konfirmasireset(form) {
        var conf = confirm("Are you sure to Reset " + form + " Data?");
        if (conf == true) {
            return true;
        }
        else {
            return false;
        }
    }

    function checkTextAreaMaxLength(textBox, e, length) {
        var mLen = textBox["MaxLength"];
        if (null == mLen)
            mLen = length;

        var maxLength = parseInt(mLen);
        if (!checkSpecialKeys(e)) {
            if (textBox.value.length > maxLength - 1) {
                if (window.event)//IE
                    e.returnValue = false;
                else//Firefox
                    e.preventDefault();
            }
        }
    }

    function checkSpecialKeys(e) {
        if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
            return false;
        else
            return true;
    }

    function copytexttoINV() {
        var from_elmINV = document.getElementById('<%=txtclinicaldiagnosis.ClientID%>');
        var to_elmINV = document.getElementsByClassName('copydatasrc');

        //if (to_elmINV != null) {
        //    to_elmINV[0].value = from_elmINV.value;
        //    AutoExpand(to_elmINV[0]);
        //}
    }

    function focustolabel(labelnya) {
        if (labelnya == "drug") {
            document.getElementById('linkto_lblbhs_drugprescription').click();
        } else if (labelnya == "cons") {
            document.getElementById('linkto_lblbhs_consumables').click();
        }
        else if (labelnya == "adddrug") {
            document.getElementById('linkto_lblbhs_additionaldrugsprescription').click();
        }
        else if (labelnya == "addcons") {
            document.getElementById('linkto_lblbhs_additionalconsumables').click();
        }
    }

    function checkSaveAsEmpty(txtelemn, linkelemn) {
        var txt = document.getElementById('MainContent_StdPlanning_' + txtelemn);
        var link = document.getElementById('MainContent_StdPlanning_' + linkelemn);

        if (txt.value != "") {
            //link.disabled = false;
            //link.style.backgroundColor = "#228b22";
            link.style.display = "";
        }
        else {
            //link.disabled = true;
            //link.style.backgroundColor = "#f4f4f4";
            link.style.display = "none";
        }
    }

    function klikSaveAsFocus(txtelemn) {
        var txt = document.getElementById('MainContent_StdPlanning_' + txtelemn);
        setTimeout(function () { txt.focus(); }, 10);
    }

    function validasi_inputRacikan() {
        var notif = document.getElementById('MainContent_StdPlanning_notifRacikan');
        var lblnotif = document.getElementById('<%= LabelNotifRacikan.ClientID %>');
        notif.style.display = "none";
        lblnotif.innerText = "Data Wajib Diisi (Tidak Boleh Kosong)!";

        var mandatory_field = document.getElementById('<%= HFmandatoryRacikan.ClientID %>');

        var nama_racikan = document.getElementById('<%= input_namaRacikan.ClientID %>');
        var dosis_racikan = document.getElementById('<%= input_dosisRacikan.ClientID %>');
        var dosisunit_racikan = document.getElementById('<%= inputddl_dosisunitRacikan.ClientID %>');
        var frekuensi_racikan = document.getElementById('<%= inputddl_frekuensiRacikan.ClientID %>');
        var rute_racikan = document.getElementById('<%= inputddl_ruteRacikan.ClientID %>');
        var instruksi_racikan = document.getElementById('<%= input_instruksiRacikan.ClientID %>');
        var jml_racikan = document.getElementById('<%= input_jmlRacikan.ClientID %>');
        var unit_racikan = document.getElementById('<%= inputddl_unitRacikan.ClientID %>');
        var iter_racikan = document.getElementById('<%= input_iterRacikan.ClientID %>');
        var detail_racikan = document.getElementById('<%= input_gvw_racikan_detail.ClientID %>');
        var note_racikan = document.getElementById('<%= input_instruksiRacikan_note.ClientID %>');
        var is_dosetext_racikan = document.getElementById('<%= input_is_dosetext.ClientID %>');
        var dosetext_racikan = document.getElementById('<%= input_dosetext.ClientID %>');

        nama_racikan.style.removeProperty("border-color");
        dosis_racikan.style.removeProperty("border-color");
        dosisunit_racikan.style.removeProperty("border-color");
        frekuensi_racikan.style.removeProperty("border-color");
        rute_racikan.style.removeProperty("border-color");
        instruksi_racikan.style.removeProperty("border-color");
        jml_racikan.style.removeProperty("border-color");
        unit_racikan.style.removeProperty("border-color");
        iter_racikan.style.removeProperty("border-color");
        note_racikan.style.removeProperty("border-color");
        dosetext_racikan.style.removeProperty("border-color");

        var flagValidasi = 0;

        if (nama_racikan.value == "" && mandatory_field.value.includes("COMPOUND_NAME")) {
            nama_racikan.style.borderColor = "red";
            notif.style.display = "";
            flagValidasi = 1;
        }
        if (is_dosetext_racikan.checked == false) {
            if ((dosis_racikan.value == "" || dosis_racikan.value == "0") && mandatory_field.value.includes("DOSE")) {
                dosis_racikan.style.borderColor = "red";
                notif.style.display = "";
                flagValidasi = 1;
            }
            if (dosisunit_racikan.options[dosisunit_racikan.selectedIndex].value == "0" && mandatory_field.value.includes("DOSE_UOM")) {
                dosisunit_racikan.style.borderColor = "red";
                notif.style.display = "";
                flagValidasi = 1;
            }
        }
        else if (is_dosetext_racikan.checked == true) {
            if (dosetext_racikan.value == "" && mandatory_field.value.includes("DOSE_TEXT")) {
                dosetext_racikan.style.borderColor = "red";
                notif.style.display = "";
                flagValidasi = 1;
            }
        }

        if (frekuensi_racikan.options[frekuensi_racikan.selectedIndex].value == "0" && mandatory_field.value.includes("FREQUENCY")) {
            frekuensi_racikan.style.borderColor = "red";
            notif.style.display = "";
            flagValidasi = 1;
        }
        if (rute_racikan.options[rute_racikan.selectedIndex].value == "0" && mandatory_field.value.includes("ROUTE")) {
            rute_racikan.style.borderColor = "red";
            notif.style.display = "";
            flagValidasi = 1;
        }
        if (instruksi_racikan.value == "" && mandatory_field.value.includes("INSTRUCTION")) {
            instruksi_racikan.style.borderColor = "red";
            notif.style.display = "";
            flagValidasi = 1;
        }
        if ((jml_racikan.value == "" || jml_racikan.value == "0") && mandatory_field.value.includes("QUANTITY")) {
            jml_racikan.style.borderColor = "red";
            notif.style.display = "";
            flagValidasi = 1;
        }
        if (unit_racikan.options[unit_racikan.selectedIndex].value == "0" && mandatory_field.value.includes("QTY_UOM")) {
            unit_racikan.style.borderColor = "red";
            notif.style.display = "";
            flagValidasi = 1;
        }
        //if (iter_racikan.value == "") {
        //    iter_racikan.style.borderColor = "red";
        //    notif.style.display = "";
        //    flagValidasi = 1;
        //}
        if ((detail_racikan == null || detail_racikan.rows.length == 0) && mandatory_field.value.includes("COMPOUND_ITEMDETAIL")) {
            notif.style.display = "";
            flagValidasi = 1;
        }
        if (note_racikan.value == "" && mandatory_field.value.includes("COMPOUND_NOTE")) {
            note_racikan.style.borderColor = "red";
            notif.style.display = "";
            flagValidasi = 1;
        }

        if (detail_racikan != null && detail_racikan.rows.length > 0) {
            var flagIn = 0;
            for (var i = 0; i < detail_racikan.rows.length - 1; i++) {
                //var txtQty = $("input[id*=racikan_quantity]");
                //txtQty[i].style.removeProperty("border-color");
                //if (txtQty[i].value == '' || txtQty[i].value == '0') {
                //    txtQty[i].style.borderColor = "red";
                //    notif.style.display = "";
                //    flagValidasi = 1;
                //}

                //var chkDose = $("input[id*=racikan_is_dosetext]");
                //var txtDose = $("input[id*=racikan_dosage_id]");
                //if (chkDose[i].checked == false) {
                //    txtDose[i].style.removeProperty("border-color");
                //    if (txtDose[i].value == '' || txtDose[i].value == '0') {
                //        txtDose[i].style.borderColor = "red";
                //        notif.style.display = "";
                //        flagValidasi = 1;
                //    }                    
                //}
            }
        }

        if (flagValidasi == 1) {
            return false;
        }
        return true;
    }

    function convertText(txtbox, angka) {
        document.getElementById("MainContent_StdPlanning_" + txtbox).value = angka;
    }

    function charLimitInstruction(limitField, limitNum) {
        if (limitField.value.length > limitNum) {
            limitField.value = limitField.value.substring(0, limitNum);
        }
    }

    function checkmandatoryradioSOAP() {

        var settingmandatoryPLANNING = document.getElementById("<%=hfmandatoryFA.ClientID %>");
        var flagMan = "no";
        var msgMan = "";

        <%--var raddrugno = document.getElementById("<%=rbdrug1.ClientID %>");
        var raddrugyes = document.getElementById("<%=rbdrug2.ClientID %>");
        var radfoodno = document.getElementById("<%=rbfood1.ClientID %>");
        var radfoodyes = document.getElementById("<%=rbfood2.ClientID %>");
        var radotherno = document.getElementById("<%=rbother1.ClientID %>");
        var radotheryes = document.getElementById("<%=rbother2.ClientID %>");--%>

        var radallergyno = document.getElementById("<%=rballergy1.ClientID %>");
        var radallergyyes = document.getElementById("<%=rballergy2.ClientID %>");
        var radroutineno = document.getElementById("<%=rbpengobatan1.ClientID %>");
        var radroutineyes = document.getElementById("<%=rbpengobatan2.ClientID %>");

        //if (raddrugno.checked == false && raddrugyes.checked == false) {
        //    alert("Alergi Obat is mandatory!");
        //    return false;
        //}
        //else if (radfoodno.checked == false && radfoodyes.checked == false) {
        //    alert("Alergi Makanan is mandatory!");
        //    return false;
        //}
        //else if (radotherno.checked == false && radotheryes.checked == false) {
        //    alert("Alergi Lain-lain is mandatory!");
        //    return false;
        //}

        if (settingmandatoryPLANNING.value.includes("DRUG_ALLERGY")) {
            if (radallergyno.checked == false && radallergyyes.checked == false) {
                //alert("Alergi is mandatory!");
                //return false;
                flagMan = "yes";
                msgMan = msgMan + "- Alergi is mandatory! <br />";
            }
        }

        if (settingmandatoryPLANNING.value.includes("PENGOBATAN_SAAT_INI")) {
            if (radroutineno.checked == false && radroutineyes.checked == false) {
                //alert("Routine Medication is mandatory!");
                //return false;
                flagMan = "yes";
                msgMan = msgMan + "- Routine Medication is mandatory! <br />";
            }
        }

        if (flagMan == "yes") {
            notificationMandatorySOAP(msgMan);
            document.getElementById('linkto_lblbhs_allergies').click();
            return false;
        }
        return true;
    }

    //////////////////////////////////////////////////////////////// common lab

    function checkenter() {
        return event.keyCode != 13;
    }

    var list = [];

    function getobjectlist() {
        var index;
        var builderobject = '';
		$("[id$='hfbuilderobject']").val(builderobject);
        for (index = 0; index < list.length; ++index) {
            var a = '{"id":' + list[index].id
                + ',"name":"' + list[index].name + '",' + '"type":"' + list[index].type
                + '","remarks":"' + list[index].remarks + '","isnew":' + list[index].isnew
                + ',"iscito":' + list[index].iscito + ',"issubmit":' + list[index].issubmit
                + ',"isdelete":' + list[index].isdelete + ',"ischeck":' + list[index].ischeck + ',"IsSendHope":' + list[index].IsSendHope
                + ',"IsFutureOrder":' + list[index].IsFutureOrder + ',"FutureOrderDate": "' + list[index].FutureOrderDate + '"},';

            builderobject = builderobject + a;
        }

        $("[id$='hfbuilderobject']").val(builderobject);
    }

    //////////////////////////////////////////////////////////////// dari clinical lab

    function serviceselected(val, type, remarks, GridId) {
        var row = val.parentNode.parentNode.parentNode;
        var Grid = 'MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_' + GridId;
        //alert(GridId);
        var tbl = document.getElementById(Grid);
        var tbl_row = tbl.rows[parseInt(row.rowIndex)];
        var tbl_Cell = tbl_row.cells[1];
        var tbl_Cel2 = tbl_row.cells[2];
        var value = tbl_Cell.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value2 = tbl_Cel2.innerHTML.toString().replace(/^\s+|\s+$/g, '');

		var flagFO = document.getElementById("<%=HF_FlagFutureOrder.ClientID%>");
		var dateFO = document.getElementById("<%=dp_labFutureOrder.ClientID%>");
        var flagFO_value = false;
		var dateFO_value = "<%=DateTime.Now.ToString("dd MMM yyyy")%>";
		if (flagFO.value == "true") {
			flagFO_value = true;
		}
		if (dateFO.value != "") {
			dateFO_value = dateFO.value;
		}

        //alert(value + ',' + value2);
        if (val.checked == true) {

            var checkexist = 0;
            //var a = '{"id":' + value
            //    + ',"name":"",' + '"type":"' + type
            //    + '","remarks":"' + remarks + '","isnew":1,"iscito":0,"issubmit":0,"isdelete":0,"ischeck":1}';


            if (list.length > 0) {
                var index;
                for (index = 0; index < list.length; ++index) {
                    if (list[index].id == value && list[index].isdelete == 0 && list[index].iscito == 1 && list[index].IsFutureOrder == flagFO_value) {
                        alert('service already checked in CITO tab');
                        return false;
                    }
					else if (list[index].id == value && list[index].isnew != 1 && list[index].iscito == 0 && list[index].IsFutureOrder == flagFO_value) {
                        list[index].isdelete = 0;
                        checkexist = 1;
                    }
                }
                if (checkexist == 0) {
                    list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                }
            }
            else {
                list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
            }
        }
        else {
            if (list.length > 0) {
                var index;
                for (index = 0; index < list.length; ++index) {
                    if (list[index].id == value && list[index].isnew == 1 && list[index].iscito == 0 && list[index].IsFutureOrder == flagFO_value) {
                        list.splice(index, 1);
                    }
					else if (list[index].id == value && list[index].isnew == 0 && list[index].ischeck == 1 && list[index].iscito == 0 && list[index].IsFutureOrder == flagFO_value) {
                        list[index].isdelete = 1;
                    }
                }
            }
        }
    }

    <%--function switchBahasa_clinicallab() {
        var bahasa = document.getElementById('<%=HFisBahasa_clinicallab.ClientID%>').value;
        if (bahasa == "ENG") {
            document.getElementById('lblbhs_pleasetick1_clinicallab').innerHTML = "Please tick (&#10003) Lab test as request";
            document.getElementById('lblbhs_pleasetick2_clinicallab').innerHTML = "&#42Fasting 10-12 hours";
            document.getElementById('lblbhs_pleasetick3_clinicallab').innerHTML = "&#42&#42Fasting 12-14 hours";
        }
        else if (bahasa == "IND") {
            document.getElementById('lblbhs_pleasetick1_clinicallab').innerHTML = "Centang (&#10003) Pemeriksaan Lab yang akan diorder";
            document.getElementById('lblbhs_pleasetick2_clinicallab').innerHTML = "&#42Puasa 10-12 jam";
            document.getElementById('lblbhs_pleasetick3_clinicallab').innerHTML = "&#42&#42Puasa 12-14 jam";
        }
    }--%>

    //////////////////////////////////////////////////////////////// dari micro lab

    function serviceselectedmicro(val, type, remarks, GridId) {

        var row = val.parentNode.parentNode.parentNode;
        var Grid = 'MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_' + GridId;
        var tbl = document.getElementById(Grid);
        var tbl_row = tbl.rows[parseInt(row.rowIndex)];
        var tbl_Cell = tbl_row.cells[1];
        var tbl_Cel2 = tbl_row.cells[2];
        var value = tbl_Cell.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value2 = tbl_Cel2.innerHTML.toString().replace(/^\s+|\s+$/g, '');

		var flagFO = document.getElementById("<%=HF_FlagFutureOrder.ClientID%>");
			var dateFO = document.getElementById("<%=dp_labFutureOrder.ClientID%>");
		var flagFO_value = false;
		var dateFO_value = "<%=DateTime.Now.ToString("dd MMM yyyy")%>";
		if (flagFO.value == "true") {
			flagFO_value = true;
		}
		if (dateFO.value != "") {
			dateFO_value = dateFO.value;
		}

        //alert(value + ',' + value2);
        if (val.checked == true) {

            var checkexist = 0;
            //var a = '{"id":' + value
            //    + ',"name":"",' + '"type":"' + type
            //    + '","remarks":"' + remarks + '","isnew":1,"iscito":0,"issubmit":0,"isdelete":0,"ischeck":1}';


            if (list.length > 0) {
                var index;
                for (index = 0; index < list.length; ++index) {
					if (list[index].id == value && list[index].isnew != 1 && list[index].IsFutureOrder == flagFO_value) {
                        list[index].isdelete = 0;
                        checkexist = 1;
                    }
                }
                if (checkexist == 0) {
                    list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                }
            }
            else {
                list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
            }
        }
        else {
            if (list.length > 0) {
                var index;
                for (index = 0; index < list.length; ++index) {
					if (list[index].id == value && list[index].isnew == 1 && list[index].IsFutureOrder == flagFO_value) {
                        list.splice(index, 1);
                    }
					else if (list[index].id == value && list[index].isnew == 0 && list[index].ischeck == 1 && list[index].IsFutureOrder == flagFO_value) {
                        list[index].isdelete = 1;
                    }
                }
            }
        }
        //alert(list[0].name);
    }

    <%--function switchBahasa_microlab() {
        var bahasa = document.getElementById('<%=HFisBahasa_microlab.ClientID%>').value;
        if (bahasa == "ENG") {
            document.getElementById('lblbhs_pleasetick1_microlab').innerHTML = "Please tick (&#10003) Lab test as request";
        }
        else if (bahasa == "IND") {
            document.getElementById('lblbhs_pleasetick1_microlab').innerHTML = "Centang (&#10003) Pemeriksaan Lab yang akan diorder";
        }
    }--%>

    //////////////////////////////////////////////////////////////// dari cito lab

    function serviceselectedcito(val, type, remarks, GridId) {

        var row = val.parentNode.parentNode.parentNode;
        var Grid = 'MainContent_StdPlanning_TabContainer1_CITO_stdcito_' + GridId;
        var tbl = document.getElementById(Grid);
        var tbl_row = tbl.rows[parseInt(row.rowIndex)];
        var tbl_Cell = tbl_row.cells[1];
        var tbl_Cel2 = tbl_row.cells[2];
        var value = tbl_Cell.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value2 = tbl_Cel2.innerHTML.toString().replace(/^\s+|\s+$/g, '');

		var flagFO = document.getElementById("<%=HF_FlagFutureOrder.ClientID%>");
		var dateFO = document.getElementById("<%=dp_labFutureOrder.ClientID%>");
			var flagFO_value = false;
		var dateFO_value = "<%=DateTime.Now.ToString("dd MMM yyyy")%>";
		if (flagFO.value == "true") {
			flagFO_value = true;
		}
		if (dateFO.value != "") {
			dateFO_value = dateFO.value;
        }

        //alert(value + ',' + value2);
        if (val.checked == true) {

            var checkexist = 0;
            //var a = '{"id":' + value
            //    + ',"name":"",' + '"type":"' + type
            //    + '","remarks":"' + remarks + '","isnew":1,"iscito":0,"issubmit":0,"isdelete":0,"ischeck":1}';


            if (list.length > 0) {
                var index;
                for (index = 0; index < list.length; ++index) {
					if (list[index].id == value && list[index].isnew != 1 && list[index].iscito == 1 && list[index].IsFutureOrder == flagFO_value) {
                        list[index].isdelete = 0;
                        checkexist = 1;
                    }

                    if (list[index].id == value && list[index].iscito == 0) {
                        list.splice(index, 1);
                    }
                }
                if (checkexist == 0) {
                    list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 1, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                }
            }
            else {
                list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 1, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
            }
        }
        else {
            if (list.length > 0) {
                var index;
                for (index = 0; index < list.length; ++index) {
					if (list[index].id == value && list[index].isnew == 1 && list[index].IsFutureOrder == flagFO_value) {
                        list.splice(index, 1);
                    }
					else if (list[index].id == value && list[index].isnew == 0 && list[index].ischeck == 1 && list[index].IsFutureOrder == flagFO_value) {
                        list[index].isdelete = 1;
                    }
                }
            }
        }
        //alert(list[0].name);
    }

    <%--function switchBahasa_citolab() {
        var bahasa = document.getElementById('<%=HFisBahasa_citolab.ClientID%>').value;
        if (bahasa == "ENG") {
            document.getElementById('lblbhs_pleasetick1_citolab').innerHTML = "Please tick (&#10003) Lab test as request";
        }
        else if (bahasa == "IND") {
            document.getElementById('lblbhs_pleasetick1_citolab').innerHTML = "Centang (&#10003) Pemeriksaan Lab yang akan diorder";
        }
    }--%>

    //////////////////////////////////////////////////////////////// dari anatomi lab

    function serviceselectedanatomi(val, type, remarks, GridId) {

        var row = val.parentNode.parentNode.parentNode;
        var Grid = 'MainContent_StdPlanning_TabContainer1_Anatomi_stdanatomi_' + GridId;
        var tbl = document.getElementById(Grid);
        var tbl_row = tbl.rows[parseInt(row.rowIndex)];
        var tbl_Cell = tbl_row.cells[1];
        var tbl_Cel2 = tbl_row.cells[2];
        var value = tbl_Cell.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value2 = tbl_Cel2.innerHTML.toString().replace(/^\s+|\s+$/g, '');

		var flagFO = document.getElementById("<%=HF_FlagFutureOrder.ClientID%>");
		var dateFO = document.getElementById("<%=dp_labFutureOrder.ClientID%>");
	    var flagFO_value = false;
		var dateFO_value = "<%=DateTime.Now.ToString("dd MMM yyyy")%>";
		if (flagFO.value == "true") {
			flagFO_value = true;
		}
		if (dateFO.value != "") {
			dateFO_value = dateFO.value;
		}

        //alert(value + ',' + value2);
        if (val.checked == true) {

            var checkexist = 0;
            //var a = '{"id":' + value
            //    + ',"name":"",' + '"type":"' + type
            //    + '","remarks":"' + remarks + '","isnew":1,"iscito":0,"issubmit":0,"isdelete":0,"ischeck":1}';


            if (list.length > 0) {
                var index;
                for (index = 0; index < list.length; ++index) {
					if (list[index].id == value && list[index].isnew != 1 && list[index].IsFutureOrder == flagFO_value) {
                        list[index].isdelete = 0;
                        checkexist = 1;
                    }
                }
                if (checkexist == 0) {
                    list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                }
            }
            else {
                list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
            }
        }
        else {
            if (list.length > 0) {
                var index;
                for (index = 0; index < list.length; ++index) {
					if (list[index].id == value && list[index].isnew == 1 && list[index].IsFutureOrder == flagFO_value) {
                        list.splice(index, 1);
                    }
					else if (list[index].id == value && list[index].isnew == 0 && list[index].ischeck == 1 && list[index].IsFutureOrder == flagFO_value) {
                        list[index].isdelete = 1;
                    }
                }
            }
        }
        //alert(list[0].name);
    }

    <%--function switchBahasa_anatomi() {
        var bahasa = document.getElementById('<%=HFisBahasa_anatomi.ClientID%>').value;
        if (bahasa == "ENG") {
            document.getElementById('lblbhs_pleasetick1_anatomilab').innerHTML = "Please tick (&#10003) Lab test as request";
            document.getElementById('lblbhs_patologianatomi').innerHTML = "Anatomical Pathology";
        }
        else if (bahasa == "IND") {
            document.getElementById('lblbhs_pleasetick1_anatomilab').innerHTML = "Centang (&#10003) Pemeriksaan Lab yang akan diorder";
            document.getElementById('lblbhs_patologianatomi').innerHTML = "Patologi Anatomi";
        }
    }--%>

    //////////////////////////////////////////////////////////////// dari mdc lab

    function serviceselectedmdc(val, type, remarks, GridId) {

        var row = val.parentNode.parentNode.parentNode;
        var Grid = 'MainContent_StdPlanning_TabContainer1_TabMDC_stdmdc_' + GridId;
        var tbl = document.getElementById(Grid);
        var tbl_row = tbl.rows[parseInt(row.rowIndex)];
        var tbl_Cell = tbl_row.cells[1];
        var tbl_Cel2 = tbl_row.cells[2];
        var value = tbl_Cell.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value2 = tbl_Cel2.innerHTML.toString().replace(/^\s+|\s+$/g, '');

		var flagFO = document.getElementById("<%=HF_FlagFutureOrder.ClientID%>");
			var dateFO = document.getElementById("<%=dp_labFutureOrder.ClientID%>");
		var flagFO_value = false;
		var dateFO_value = "<%=DateTime.Now.ToString("dd MMM yyyy")%>";
		if (flagFO.value == "true") {
			flagFO_value = true;
		}
		if (dateFO.value != "") {
			dateFO_value = dateFO.value;
		}

        //alert(value + ',' + value2);
        if (val.checked == true) {

            var checkexist = 0;
            //var a = '{"id":' + value
            //    + ',"name":"",' + '"type":"' + type
            //    + '","remarks":"' + remarks + '","isnew":1,"iscito":0,"issubmit":0,"isdelete":0,"ischeck":1}';


            if (list.length > 0) {
                var index;
                for (index = 0; index < list.length; ++index) {
					if (list[index].id == value && list[index].isnew != 1 && list[index].IsFutureOrder == flagFO_value) {
                        list[index].isdelete = 0;
                        checkexist = 1;
                    }
                }
                if (checkexist == 0) {
                    list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                }
            }
            else {
                list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
            }
        }
        else {
            if (list.length > 0) {
                var index;
                for (index = 0; index < list.length; ++index) {
					if (list[index].id == value && list[index].isnew == 1 && list[index].IsFutureOrder == flagFO_value) {
                        list.splice(index, 1);
                    }
					else if (list[index].id == value && list[index].isnew == 0 && list[index].ischeck == 1 && list[index].IsFutureOrder == flagFO_value) {
                        list[index].isdelete = 1;
                    }
                }
            }
        }
        //alert(list[0].name);
    }

    <%--function switchBahasa_mdc() {
        var bahasa = document.getElementById('<%=HFisBahasa_mdc.ClientID%>').value;
        if (bahasa == "ENG") {
            document.getElementById('lblbhs_pleasetick1_mdc').innerHTML = "Please tick (&#10003) Lab test as request";
        }
        else if (bahasa == "IND") {
            document.getElementById('lblbhs_pleasetick1_mdc').innerHTML = "Centang (&#10003) Pemeriksaan Lab yang akan diorder";
        }
    }--%>

    //////////////////////////////////////////////////////////////// dari panel lab

    function serviceselectedpanel(val, type, remarks, GridId) {

        var row = val.parentNode.parentNode.parentNode;
        var Grid = 'MainContent_StdPlanning_TabContainer1_TabPanel2_stdpanel_' + GridId;
        var tbl = document.getElementById(Grid);
        var tbl_row = tbl.rows[parseInt(row.rowIndex)];
        var tbl_Cell = tbl_row.cells[1];
        var tbl_Cel2 = tbl_row.cells[2];
        var value = tbl_Cell.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value2 = tbl_Cel2.innerHTML.toString().replace(/^\s+|\s+$/g, '');

		var flagFO = document.getElementById("<%=HF_FlagFutureOrder.ClientID%>");
		var dateFO = document.getElementById("<%=dp_labFutureOrder.ClientID%>");
		var flagFO_value = false;
		var dateFO_value = "<%=DateTime.Now.ToString("dd MMM yyyy")%>";
		if (flagFO.value == "true") {
			flagFO_value = true;
		}
		if (dateFO.value != "") {
			dateFO_value = dateFO.value;
		}

        //alert(value + ',' + value2);
        if (val.checked == true) {

            var checkexist = 0;
            //var a = '{"id":' + value
            //    + ',"name":"",' + '"type":"' + type
            //    + '","remarks":"' + remarks + '","isnew":1,"iscito":0,"issubmit":0,"isdelete":0,"ischeck":1}';



            if (list.length > 0) {
                var index;
                for (index = 0; index < list.length; ++index) {
					if (list[index].id == value && list[index].isnew != 1 && list[index].IsFutureOrder == flagFO_value) {
                        list[index].isdelete = 0;
                        checkexist = 1;
                    }
                }
                if (checkexist == 0) {
                    list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                }
            }
            else {
                list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
            }
        }
        else {
            if (list.length > 0) {
                var index;
                for (index = 0; index < list.length; ++index) {
					if (list[index].id == value && list[index].isnew == 1 && list[index].IsFutureOrder == flagFO_value) {
                        list.splice(index, 1);
                    }
					else if (list[index].id == value && list[index].isnew == 0 && list[index].ischeck == 1 && list[index].IsFutureOrder == flagFO_value) {
                        list[index].isdelete = 1;
                    }
                }
            }
        }
        //alert(list[0].name);
    }

    function gethelper() {
        var ip = $("[id$='iplocal']").val();
        var url = "http://" + ip + "/form/LabFormPanelTM.pdf";
        var address = encodeURI(url);
        window.open(address, '_blank');
        return false;
    }

    <%--function switchBahasa_panel() {
        var bahasa = document.getElementById('<%=HFisBahasa_panel.ClientID%>').value;
        if (bahasa == "ENG") {
            document.getElementById('lblbhs_pleasetick1_panellab').innerHTML = "Please tick (&#10003) Lab test as request";
        }
        else if (bahasa == "IND") {
            document.getElementById('lblbhs_pleasetick1_panellab').innerHTML = "Centang (&#10003) Pemeriksaan Lab yang akan diorder";
        }
    }--%>

    //////////////////////////////////////////////////////////////// common rad

    var listradiology = [];

    function getobjectlistradiology() {

        var index;
        var builderobject = '';
        for (index = 0; index < listradiology.length; ++index) {
            var a = '{"id":' + listradiology[index].id
                + ',"name":"' + listradiology[index].name + '",' + '"type":"' + listradiology[index].type
                + '","remarks":"' + listradiology[index].remarks + '","isnew":' + listradiology[index].isnew
                + ',"iscito":' + listradiology[index].iscito + ',"issubmit":' + listradiology[index].issubmit
                + ',"isdelete":' + listradiology[index].isdelete + ',"ischeck":' + listradiology[index].ischeck + ',"IsSendHope":' + listradiology[index].IsSendHope
                + ',"IsFutureOrder":' + listradiology[index].IsFutureOrder + ',"FutureOrderDate": "' + listradiology[index].FutureOrderDate + '"},';

            builderobject = builderobject + a;
        }
        //alert(builderobject);
        $("[id$='hfbuilderobjectradiology']").val(builderobject);
    }

    //////////////////////////////////////////////////////////////// dari xray rad

    function serviceselectedxray(val, type, remarks, GridId) {

        var row = val.parentNode.parentNode.parentNode;
        var Grid = 'MainContent_StdPlanning_TabContainer2_XRay_stdxray_' + GridId;
        var tbl = document.getElementById(Grid);

        var tbl_row = tbl.rows[parseInt(row.rowIndex)];
        var tbl_Cell = tbl_row.cells[3];
        var tbl_Cel2 = tbl_row.cells[4];
        var tbl_Cel3 = tbl_row.cells[5];
        var tbl_Cel4 = tbl_row.cells[1];
        var tbl_Cel5 = tbl_row.cells[0];
        var tbl_Cel6 = tbl_row.cells[2];

        var inputradioright = tbl_Cel4.getElementsByTagName("input");
        var inputradioleft = tbl_Cel6.getElementsByTagName("input");
        var inputchecked = tbl_Cel5.getElementsByTagName("input");
        var value = tbl_Cell.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value2 = tbl_Cel2.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value3 = tbl_Cel3.innerHTML.toString().replace(/^\s+|\s+$/g, '');

		var flagFO = document.getElementById("<%=HF_FlagFutureOrderRad.ClientID%>");
		var dateFO = document.getElementById("<%=dp_radFutureOrder.ClientID%>");
		var flagFO_value = false;
		var dateFO_value = "<%=DateTime.Now.ToString("dd MMM yyyy")%>";
		if (flagFO.value == "true") {
			flagFO_value = true;
		}
		if (dateFO.value != "") {
			dateFO_value = dateFO.value;
		}

        //alert(value + ',' + value2 + ','+ value3);

        if (val.checked == true) {
            var checkexist = 0;
            //var a = '{"id":' + value
            //    + ',"name":"",' + '"type":"' + type
            //    + '","remarks":"' + remarks + '","isnew":1,"iscito":0,"issubmit":0,"isdelete":0,"ischeck":1}';


            if (listradiology.length > 0) {
                var index;

                for (index = 0; index < listradiology.length; ++index) {
                    if (listradiology[index].id == value && listradiology[index].isnew != 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        listradiology[index].isdelete = 0;

                        if (value2 === 'False') {
                            inputchecked[0].checked = true;
                            listradiology[index].remarks = '';
                        }
                        else {
                            if (remarks === 'Right') {
                                listradiology[index].remarks = 'Right';
                                inputradioright[0].checked = true;
                                inputchecked[0].checked = true;
                            }
                            else if (remarks === 'Left') {
                                listradiology[index].remarks = 'Left';
                                inputradioleft[0].checked = true;
                                inputchecked[0].checked = true;
                            }
                        }
                        checkexist = 1;
                    }
                    else if (listradiology[index].id == value && listradiology[index].isnew == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        if (remarks === 'Right') {
                            listradiology[index].remarks = 'Right';
                            inputradioright[0].checked = true;
                            inputchecked[0].checked = true;
                        }
                        else if (remarks === 'Left') {
                            listradiology[index].remarks = 'Left';
                            inputradioleft[0].checked = true;
                            inputchecked[0].checked = true;
                        }
                        checkexist = 1;
                    }
                }
                if (checkexist == 0) {

                    if (value2 === 'False') {
						listradiology.push({ id: value, name: value3, type: type, remarks: '', isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                    }
                    else {
                        if (remarks === 'Right') {
                            inputradioright[0].checked = true;
                            inputchecked[0].checked = true;
							listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                        }
                        else {
                            inputchecked[0].checked = true;
                            inputchecked[0].checked = true;
							listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                        }
                    }
                }
            }
            else {
                if (value2 === 'False') {
					listradiology.push({ id: value, name: value3, type: type, remarks: '', isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                }
                else {
                    if (remarks === 'Right') {
                        inputradioright[0].checked = true;
                        inputchecked[0].checked = true;
						listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value  });
                    }
                    else {
                        inputradioleft[0].checked = true;
                        inputchecked[0].checked = true;
						listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value  });
                    }
                }
            }
        }
        else {
            if (listradiology.length > 0) {
                var index;
                for (index = 0; index < listradiology.length; ++index) {
					if (listradiology[index].id == value && listradiology[index].isnew == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        listradiology.splice(index, 1);

                        if (inputradioright[0]) {
                            inputradioright[0].checked = false;
                        }
                        if (inputradioleft[0]) {
                            inputradioleft[0].checked = false;
                        }
                        //inputradioright[0].checked = false;
                        inputchecked[0].checked = false;
                        //inputradioleft[0].checked = false;
                    }
					else if (listradiology[index].id == value && listradiology[index].isnew == 0 && listradiology[index].ischeck == 1 && listradiology[index].IsFutureOrder == flagFO_value) {

                        if (inputradioright[0]) {
                            inputradioright[0].checked = false;
                        }
                        if (inputradioleft[0]) {
                            inputradioleft[0].checked = false;
                        }

                        inputchecked[0].checked = false;

                        listradiology[index].isdelete = 1;
                    }
                }
            }
        }
        //alert(list[0].name);
    }

    <%--function switchBahasa_xrayrad() {
        var bahasa = document.getElementById('<%=HFisBahasa_xrayrad.ClientID%>').value;
        if (bahasa == "ENG") {
            document.getElementById('lblbhs_pleasetick1_xrayrad').innerHTML = "Please tick (&#10003) Lab test as request";
            document.getElementById('lblbhs_pleasetick2_xrayrad').innerHTML = "&#42&#42Fasting minimal 4 hours before examinations";
            document.getElementById('lblbhs_pleasetick3_xrayrad').innerHTML = "&#42&#42&#42For reservation please contact Radiology Department";
        }
        else if (bahasa == "IND") {
            document.getElementById('lblbhs_pleasetick1_xrayrad').innerHTML = "Centang (&#10003) Pemeriksaan Lab yang akan diorder";
            document.getElementById('lblbhs_pleasetick2_xrayrad').innerHTML = "&#42&#42Puasa minimal 4 jam sebelum pemeriksaan";
            document.getElementById('lblbhs_pleasetick3_xrayrad').innerHTML = "&#42&#42&#42Persiapan khusus harus melalui perjanjian dengan Bagian Radiologi";
        }
    }--%>

    //////////////////////////////////////////////////////////////// dari usg rad

    function serviceselectedusg(val, type, remarks, GridId) {

        var row = val.parentNode.parentNode.parentNode;
        var Grid = 'MainContent_StdPlanning_TabContainer2_USG_stdusg_' + GridId;
        var tbl = document.getElementById(Grid);

        var tbl_row = tbl.rows[parseInt(row.rowIndex)];
        var tbl_Cell = tbl_row.cells[3];
        var tbl_Cel2 = tbl_row.cells[4];
        var tbl_Cel3 = tbl_row.cells[5];
        var tbl_Cel4 = tbl_row.cells[1];
        var tbl_Cel5 = tbl_row.cells[0];
        var tbl_Cel6 = tbl_row.cells[2];

        var inputradioright = tbl_Cel4.getElementsByTagName("input");
        var inputradioleft = tbl_Cel6.getElementsByTagName("input");
        var inputchecked = tbl_Cel5.getElementsByTagName("input");
        var value = tbl_Cell.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value2 = tbl_Cel2.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value3 = tbl_Cel3.innerHTML.toString().replace(/^\s+|\s+$/g, '');

		var flagFO = document.getElementById("<%=HF_FlagFutureOrderRad.ClientID%>");
		var dateFO = document.getElementById("<%=dp_radFutureOrder.ClientID%>");
		var flagFO_value = false;
		var dateFO_value = "<%=DateTime.Now.ToString("dd MMM yyyy")%>";
		if (flagFO.value == "true") {
			flagFO_value = true;
		}
		if (dateFO.value != "") {
			dateFO_value = dateFO.value;
		}

        //alert(value + ',' + value2 + ','+ value3);
        if (val.checked == true) {
            var checkexist = 0;
            //var a = '{"id":' + value
            //    + ',"name":"",' + '"type":"' + type
            //    + '","remarks":"' + remarks + '","isnew":1,"iscito":0,"issubmit":0,"isdelete":0,"ischeck":1}';


            if (listradiology.length > 0) {
                var index;

                for (index = 0; index < listradiology.length; ++index) {
					if (listradiology[index].id == value && listradiology[index].isnew != 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        listradiology[index].isdelete = 0;

                        if (value2 === 'False') {
                            inputchecked[0].checked = true;
                            listradiology[index].remarks = '';
                        }
                        else {
                            if (remarks === 'Right') {
                                listradiology[index].remarks = 'Right';
                                inputradioright[0].checked = true;
                                inputchecked[0].checked = true;
                            }
                            else if (remarks === 'Left') {
                                listradiology[index].remarks = 'Left';
                                inputradioleft[0].checked = true;
                                inputchecked[0].checked = true;
                            }
                        }
                        checkexist = 1;
                    }
					else if (listradiology[index].id == value && listradiology[index].isnew == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        if (remarks === 'Right') {
                            listradiology[index].remarks = 'Right';
                            inputradioright[0].checked = true;
                            inputchecked[0].checked = true;
                        }
                        else if (remarks === 'Left') {
                            listradiology[index].remarks = 'Left';
                            inputradioleft[0].checked = true;
                            inputchecked[0].checked = true;
                        }
                        checkexist = 1;
                    }
                }
                if (checkexist == 0) {

                    if (value2 === 'False') {
                        listradiology.push({ id: value, name: value3, type: type, remarks: '', isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                    }
                    else {
                        if (remarks === 'Right') {
                            inputradioright[0].checked = true;
                            inputchecked[0].checked = true;
                            listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                        }
                        else {
                            inputchecked[0].checked = true;
                            inputchecked[0].checked = true;
                            listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                        }
                    }
                }
            }
            else {
                if (value2 === 'False') {
                    listradiology.push({ id: value, name: value3, type: type, remarks: '', isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                }
                else {
                    if (remarks === 'Right') {
                        inputradioright[0].checked = true;
                        inputchecked[0].checked = true;
                        listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                    }
                    else {
                        inputradioleft[0].checked = true;
                        inputchecked[0].checked = true;
                        listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                    }
                }
            }
        }
        else {
            if (listradiology.length > 0) {
                var index;
                for (index = 0; index < listradiology.length; ++index) {
					if (listradiology[index].id == value && listradiology[index].isnew == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        listradiology.splice(index, 1);
                        if (inputradioright[0]) {
                            inputradioright[0].checked = false;
                        }
                        if (inputradioleft[0]) {
                            inputradioleft[0].checked = false;
                        }
                        //inputradioright[0].checked = false;
                        inputchecked[0].checked = false;
                        //inputradioleft[0].checked = false;
                    }
					else if (listradiology[index].id == value && listradiology[index].isnew == 0 && listradiology[index].ischeck == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        if (inputradioright[0]) {
                            inputradioright[0].checked = false;
                        }
                        if (inputradioleft[0]) {
                            inputradioleft[0].checked = false;
                        }
                        inputchecked[0].checked = false;
                        listradiology[index].isdelete = 1;
                    }
                }
            }
        }
        //alert(list[0].name);
    }

    <%--function switchBahasa_usgrad() {
        var bahasa = document.getElementById('<%=HFisBahasa_usgrad.ClientID%>').value;
        if (bahasa == "ENG") {
            document.getElementById('lblbhs_pleasetick1_usgrad').innerHTML = "Please tick (&#10003) Lab test as request";
            document.getElementById('lblbhs_pleasetick2_usgrad').innerHTML = "&#42&#42Fasting minimal 4 hours before examinations";
            document.getElementById('lblbhs_pleasetick3_usgrad').innerHTML = "&#42&#42&#42For reservation please contact Radiology Department";
        }
        else if (bahasa == "IND") {
            document.getElementById('lblbhs_pleasetick1_usgrad').innerHTML = "Centang (&#10003) Pemeriksaan Lab yang akan diorder";
            document.getElementById('lblbhs_pleasetick2_usgrad').innerHTML = "&#42&#42Puasa minimal 4 jam sebelum pemeriksaan";
            document.getElementById('lblbhs_pleasetick3_usgrad').innerHTML = "&#42&#42&#42Persiapan khusus harus melalui perjanjian dengan Bagian Radiologi";
        }
    }--%>

    //////////////////////////////////////////////////////////////// dari ct rad

    function serviceselectedctrad(val, type, remarks, GridId) {

        var row = val.parentNode.parentNode.parentNode;
        var Grid = 'MainContent_StdPlanning_TabContainer2_CT_stdctrad_' + GridId;
        var tbl = document.getElementById(Grid);

        var tbl_row = tbl.rows[parseInt(row.rowIndex)];
        var tbl_Cell = tbl_row.cells[3];
        var tbl_Cel2 = tbl_row.cells[4];
        var tbl_Cel3 = tbl_row.cells[5];
        var tbl_Cel4 = tbl_row.cells[1];
        var tbl_Cel5 = tbl_row.cells[0];
        var tbl_Cel6 = tbl_row.cells[2];

        var inputradioright = tbl_Cel4.getElementsByTagName("input");
        var inputradioleft = tbl_Cel6.getElementsByTagName("input");
        var inputchecked = tbl_Cel5.getElementsByTagName("input");
        var value = tbl_Cell.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value2 = tbl_Cel2.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value3 = tbl_Cel3.innerHTML.toString().replace(/^\s+|\s+$/g, '');

		var flagFO = document.getElementById("<%=HF_FlagFutureOrderRad.ClientID%>");
		var dateFO = document.getElementById("<%=dp_radFutureOrder.ClientID%>");
		var flagFO_value = false;
		var dateFO_value = "<%=DateTime.Now.ToString("dd MMM yyyy")%>";
		if (flagFO.value == "true") {
			flagFO_value = true;
		}
		if (dateFO.value != "") {
			dateFO_value = dateFO.value;
		}

        //alert(value + ',' + value2 + ','+ value3);
        if (val.checked == true) {
            var checkexist = 0;
            //var a = '{"id":' + value
            //    + ',"name":"",' + '"type":"' + type
            //    + '","remarks":"' + remarks + '","isnew":1,"iscito":0,"issubmit":0,"isdelete":0,"ischeck":1}';



            if (listradiology.length > 0) {
                var index;

                for (index = 0; index < listradiology.length; ++index) {
					if (listradiology[index].id == value && listradiology[index].isnew != 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        listradiology[index].isdelete = 0;

                        if (value2 === 'False') {
                            inputchecked[0].checked = true;
                            listradiology[index].remarks = '';
                        }
                        else {
                            if (remarks === 'Right') {
                                listradiology[index].remarks = 'Right';
                                inputradioright[0].checked = true;
                                inputchecked[0].checked = true;
                            }
                            else if (remarks === 'Left') {
                                listradiology[index].remarks = 'Left';
                                inputradioleft[0].checked = true;
                                inputchecked[0].checked = true;
                            }
                        }
                        checkexist = 1;
                    }
					else if (listradiology[index].id == value && listradiology[index].isnew == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        if (remarks === 'Right') {
                            listradiology[index].remarks = 'Right';
                            inputradioright[0].checked = true;
                            inputchecked[0].checked = true;
                        }
                        else if (remarks === 'Left') {
                            listradiology[index].remarks = 'Left';
                            inputradioleft[0].checked = true;
                            inputchecked[0].checked = true;
                        }
                        checkexist = 1;
                    }
                }
                if (checkexist == 0) {

                    if (value2 === 'False') {
                        listradiology.push({ id: value, name: value3, type: type, remarks: '', isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                    }
                    else {
                        if (remarks === 'Right') {
                            inputradioright[0].checked = true;
                            inputchecked[0].checked = true;
                            listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                        }
                        else {
                            inputchecked[0].checked = true;
                            inputchecked[0].checked = true;
                            listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                        }
                    }
                }
            }
            else {
                if (value2 === 'False') {
                    listradiology.push({ id: value, name: value3, type: type, remarks: '', isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                }
                else {
                    if (remarks === 'Right') {
                        inputradioright[0].checked = true;
                        inputchecked[0].checked = true;
                        listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                    }
                    else {
                        inputradioleft[0].checked = true;
                        inputchecked[0].checked = true;
                        listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                    }
                }
            }
        }
        else {
            if (listradiology.length > 0) {
                var index;
                for (index = 0; index < listradiology.length; ++index) {
					if (listradiology[index].id == value && listradiology[index].isnew == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        listradiology.splice(index, 1);
                        if (inputradioright[0]) {
                            inputradioright[0].checked = false;
                        }
                        if (inputradioleft[0]) {
                            inputradioleft[0].checked = false;
                        }
                        //inputradioright[0].checked = false;
                        inputchecked[0].checked = false;
                        //inputradioleft[0].checked = false;
                    }
					else if (listradiology[index].id == value && listradiology[index].isnew == 0 && listradiology[index].ischeck == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        if (inputradioright[0]) {
                            inputradioright[0].checked = false;
                        }
                        if (inputradioleft[0]) {
                            inputradioleft[0].checked = false;
                        }

                        //inputradioright[0].checked = false;
                        inputchecked[0].checked = false;
                        //inputradioleft[0].checked = false;
                        listradiology[index].isdelete = 1;
                    }
                }
            }
        }
        //alert(list[0].name);
    }

    <%--function switchBahasa_ctrad() {
        var bahasa = document.getElementById('<%=HFisBahasa_ctrad.ClientID%>').value;
        if (bahasa == "ENG") {
            document.getElementById('lblbhs_pleasetick1_ctrad').innerHTML = "Please tick (&#10003) Lab test as request";
            document.getElementById('lblbhs_pleasetick2_ctrad').innerHTML = "&#42&#42Fasting minimal 4 hours before examinations";
            document.getElementById('lblbhs_pleasetick3_ctrad').innerHTML = "&#42&#42&#42For reservation please contact Radiology Department";
        }
        else if (bahasa == "IND") {
            document.getElementById('lblbhs_pleasetick1_ctrad').innerHTML = "Centang (&#10003) Pemeriksaan Lab yang akan diorder";
            document.getElementById('lblbhs_pleasetick2_ctrad').innerHTML = "&#42&#42Puasa minimal 4 jam sebelum pemeriksaan";
            document.getElementById('lblbhs_pleasetick3_ctrad').innerHTML = "&#42&#42&#42Persiapan khusus harus melalui perjanjian dengan Bagian Radiologi";
        }
    }--%>

    //////////////////////////////////////////////////////////////// dari mrifull rad

    function serviceselectedmrifull(val, type, remarks, GridId) {

        var row = val.parentNode.parentNode.parentNode;
        var Grid = 'MainContent_StdPlanning_TabContainer2_MRI3_stdmrifull_' + GridId;
        var tbl = document.getElementById(Grid);

        var tbl_row = tbl.rows[parseInt(row.rowIndex)];
        var tbl_Cell = tbl_row.cells[3];
        var tbl_Cel2 = tbl_row.cells[4];
        var tbl_Cel3 = tbl_row.cells[5];
        var tbl_Cel4 = tbl_row.cells[1];
        var tbl_Cel5 = tbl_row.cells[0];
        var tbl_Cel6 = tbl_row.cells[2];

        var inputradioright = tbl_Cel4.getElementsByTagName("input");
        var inputradioleft = tbl_Cel6.getElementsByTagName("input");
        var inputchecked = tbl_Cel5.getElementsByTagName("input");
        var value = tbl_Cell.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value2 = tbl_Cel2.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value3 = tbl_Cel3.innerHTML.toString().replace(/^\s+|\s+$/g, '');

		var flagFO = document.getElementById("<%=HF_FlagFutureOrderRad.ClientID%>");
		var dateFO = document.getElementById("<%=dp_radFutureOrder.ClientID%>");
		var flagFO_value = false;
		var dateFO_value = "<%=DateTime.Now.ToString("dd MMM yyyy")%>";
		if (flagFO.value == "true") {
			flagFO_value = true;
		}
		if (dateFO.value != "") {
			dateFO_value = dateFO.value;
		}


        //alert(value + ',' + value2 + ','+ value3);
        if (val.checked == true) {
            var checkexist = 0;
            //var a = '{"id":' + value
            //    + ',"name":"",' + '"type":"' + type
            //    + '","remarks":"' + remarks + '","isnew":1,"iscito":0,"issubmit":0,"isdelete":0,"ischeck":1}';



            if (listradiology.length > 0) {
                var index;

                for (index = 0; index < listradiology.length; ++index) {
					if (listradiology[index].id == value && listradiology[index].isnew != 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        listradiology[index].isdelete = 0;

                        if (value2 === 'False') {
                            inputchecked[0].checked = true;
                            listradiology[index].remarks = '';
                        }
                        else {
                            if (remarks === 'Right') {
                                listradiology[index].remarks = 'Right';
                                inputradioright[0].checked = true;
                                inputchecked[0].checked = true;
                            }
                            else if (remarks === 'Left') {
                                listradiology[index].remarks = 'Left';
                                inputradioleft[0].checked = true;
                                inputchecked[0].checked = true;
                            }
                        }
                        checkexist = 1;
                    }
					else if (listradiology[index].id == value && listradiology[index].isnew == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        if (remarks === 'Right') {
                            listradiology[index].remarks = 'Right';
                            inputradioright[0].checked = true;
                            inputchecked[0].checked = true;
                        }
                        else if (remarks === 'Left') {
                            listradiology[index].remarks = 'Left';
                            inputradioleft[0].checked = true;
                            inputchecked[0].checked = true;
                        }
                        checkexist = 1;
                    }
                }
                if (checkexist == 0) {

                    if (value2 === 'False') {
                        listradiology.push({ id: value, name: value3, type: type, remarks: '', isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                    }
                    else {
                        if (remarks === 'Right') {
                            inputradioright[0].checked = true;
                            inputchecked[0].checked = true;
                            listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                        }
                        else {
                            inputchecked[0].checked = true;
                            inputchecked[0].checked = true;
                            listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                        }
                    }
                }
            }
            else {
                if (value2 === 'False') {
                    listradiology.push({ id: value, name: value3, type: type, remarks: '', isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                }
                else {
                    if (remarks === 'Right') {
                        inputradioright[0].checked = true;
                        inputchecked[0].checked = true;
                        listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                    }
                    else {
                        inputradioleft[0].checked = true;
                        inputchecked[0].checked = true;
                        listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                    }
                }
            }
        }
        else {
            if (listradiology.length > 0) {
                var index;
                for (index = 0; index < listradiology.length; ++index) {
					if (listradiology[index].id == value && listradiology[index].isnew == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        listradiology.splice(index, 1);
                        if (inputradioright[0]) {
                            inputradioright[0].checked = false;
                        }
                        if (inputradioleft[0]) {
                            inputradioleft[0].checked = false;
                        }
                        //inputradioright[0].checked = false;
                        inputchecked[0].checked = false;
                        //inputradioleft[0].checked = false;
                    }
					else if (listradiology[index].id == value && listradiology[index].isnew == 0 && listradiology[index].ischeck == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        if (inputradioright[0]) {
                            inputradioright[0].checked = false;
                        }
                        if (inputradioleft[0]) {
                            inputradioleft[0].checked = false;
                        }

                        //inputradioright[0].checked = false;
                        inputchecked[0].checked = false;
                        //inputradioleft[0].checked = false;
                        listradiology[index].isdelete = 1;
                    }
                }
            }
        }
        //alert(list[0].name);
    }

    <%--function switchBahasa_mrifullrad() {
        var bahasa = document.getElementById('<%=HFisBahasa_mrifullrad.ClientID%>').value;
        if (bahasa == "ENG") {
            document.getElementById('lblbhs_pleasetick1_mrifullrad').innerHTML = "Please tick (&#10003) Lab test as request";
            document.getElementById('lblbhs_pleasetick2_mrifullrad').innerHTML = "&#42&#42Fasting minimal 4 hours before examinations";
        }
        else if (bahasa == "IND") {
            document.getElementById('lblbhs_pleasetick1_mrifullrad').innerHTML = "Centang (&#10003) Pemeriksaan Lab yang akan diorder";
            document.getElementById('lblbhs_pleasetick2_mrifullrad').innerHTML = "&#42&#42Puasa minimal 4 jam sebelum pemeriksaan";
        }
    }--%>

    //////////////////////////////////////////////////////////////// dari mrihalf rad

    function serviceselectedmrihalf(val, type, remarks, GridId) {

        var row = val.parentNode.parentNode.parentNode;
        var Grid = 'MainContent_StdPlanning_TabContainer2_MRI1_stdmrihalf_' + GridId;
        var tbl = document.getElementById(Grid);

        var tbl_row = tbl.rows[parseInt(row.rowIndex)];
        var tbl_Cell = tbl_row.cells[3];
        var tbl_Cel2 = tbl_row.cells[4];
        var tbl_Cel3 = tbl_row.cells[5];
        var tbl_Cel4 = tbl_row.cells[1];
        var tbl_Cel5 = tbl_row.cells[0];
        var tbl_Cel6 = tbl_row.cells[2];

        var inputradioright = tbl_Cel4.getElementsByTagName("input");
        var inputradioleft = tbl_Cel6.getElementsByTagName("input");
        var inputchecked = tbl_Cel5.getElementsByTagName("input");
        var value = tbl_Cell.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value2 = tbl_Cel2.innerHTML.toString().replace(/^\s+|\s+$/g,'');
        var value3 = tbl_Cel3.innerHTML.toString().replace(/^\s+|\s+$/g, '');

		var flagFO = document.getElementById("<%=HF_FlagFutureOrderRad.ClientID%>");
		var dateFO = document.getElementById("<%=dp_radFutureOrder.ClientID%>");
		var flagFO_value = false;
		var dateFO_value = "<%=DateTime.Now.ToString("dd MMM yyyy")%>";
		if (flagFO.value == "true") {
			flagFO_value = true;
		}
		if (dateFO.value != "") {
			dateFO_value = dateFO.value;
		}

        //alert(value + ',' + value2 + ','+ value3);
        if (val.checked == true) {
            var checkexist = 0;
            //var a = '{"id":' + value
            //    + ',"name":"",' + '"type":"' + type
            //    + '","remarks":"' + remarks + '","isnew":1,"iscito":0,"issubmit":0,"isdelete":0,"ischeck":1}';



            if (listradiology.length > 0) {
                var index;

                for (index = 0; index < listradiology.length; ++index) {
					if (listradiology[index].id == value && listradiology[index].isnew != 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        listradiology[index].isdelete = 0;

                        if (value2 === 'False') {
                            inputchecked[0].checked = true;
                            listradiology[index].remarks = '';
                        }
                        else {
                            if (remarks === 'Right') {
                                listradiology[index].remarks = 'Right';
                                inputradioright[0].checked = true;
                                inputchecked[0].checked = true;
                            }
                            else if (remarks === 'Left') {
                                listradiology[index].remarks = 'Left';
                                inputradioleft[0].checked = true;
                                inputchecked[0].checked = true;
                            }
                        }
                        checkexist = 1;
                    }
					else if (listradiology[index].id == value && listradiology[index].isnew == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        if (remarks === 'Right') {
                            listradiology[index].remarks = 'Right';
                            inputradioright[0].checked = true;
                            inputchecked[0].checked = true;
                        }
                        else if (remarks === 'Left') {
                            listradiology[index].remarks = 'Left';
                            inputradioleft[0].checked = true;
                            inputchecked[0].checked = true;
                        }
                        checkexist = 1;
                    }
                }
                if (checkexist == 0) {

                    if (value2 === 'False') {
                        listradiology.push({ id: value, name: value3, type: type, remarks: '', isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                    }
                    else {
                        if (remarks === 'Right') {
                            inputradioright[0].checked = true;
                            inputchecked[0].checked = true;
                            listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                        }
                        else {
                            inputchecked[0].checked = true;
                            inputchecked[0].checked = true;
                            listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                        }
                    }
                }
            }
            else {
                if (value2 === 'False') {
                    listradiology.push({ id: value, name: value3, type: type, remarks: '', isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                }
                else {
                    if (remarks === 'Right') {
                        inputradioright[0].checked = true;
                        inputchecked[0].checked = true;
                        listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                    }
                    else {
                        inputradioleft[0].checked = true;
                        inputchecked[0].checked = true;
                        listradiology.push({ id: value, name: value3, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1, IsSendHope: 0, IsFutureOrder: flagFO_value, FutureOrderDate: dateFO_value });
                    }
                }
            }
        }
        else {
            if (listradiology.length > 0) {
                var index;
                for (index = 0; index < listradiology.length; ++index) {
					if (listradiology[index].id == value && listradiology[index].isnew == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        listradiology.splice(index, 1);
                        if (inputradioright[0]) {
                            inputradioright[0].checked = false;
                        }
                        if (inputradioleft[0]) {
                            inputradioleft[0].checked = false;
                        }
                        //inputradioright[0].checked = false;
                        inputchecked[0].checked = false;
                        //inputradioleft[0].checked = false;
                    }
					else if (listradiology[index].id == value && listradiology[index].isnew == 0 && listradiology[index].ischeck == 1 && listradiology[index].IsFutureOrder == flagFO_value) {
                        if (inputradioright[0]) {
                            inputradioright[0].checked = false;
                        }
                        if (inputradioleft[0]) {
                            inputradioleft[0].checked = false;
                        }

                        //inputradioright[0].checked = false;
                        inputchecked[0].checked = false;
                        //inputradioleft[0].checked = false;
                        listradiology[index].isdelete = 1;
                    }
                }
            }
        }
        //alert(list[0].name);
    }

    <%--function switchBahasa_mrihalfrad() {
        var bahasa = document.getElementById('<%=HFisBahasa_mrihalfrad.ClientID%>').value;
        if (bahasa == "ENG") {
            document.getElementById('lblbhs_pleasetick1_mrihalfrad').innerHTML = "Please tick (&#10003) Lab test as request";
            document.getElementById('lblbhs_pleasetick2_mrihalfrad').innerHTML = "&#42&#42Fasting minimal 4 hours before examinations";
        }
        else if (bahasa == "IND") {
            document.getElementById('lblbhs_pleasetick1_mrihalfrad').innerHTML = "Centang (&#10003) Pemeriksaan Lab yang akan diorder";
            document.getElementById('lblbhs_pleasetick2_mrihalfrad').innerHTML = "&#42&#42Puasa minimal 4 jam sebelum pemeriksaan";
        }
    }--%>

    //////////////////////////////////////////////////////////////// end lab rad

    function LoadTabLabData() {
        <%--var tabIndex = document.getElementById("<%= TabContainer1.ClientID %>");--%>
        var tabIndex = $find("MainContent_StdPlanning_TabContainer1");
        var i = tabIndex._activeTabIndex; 

        var L0 = document.getElementById("<%= HF_FlagTabClinical.ClientID %>");
        var L1 = document.getElementById("<%= HF_FlagTabMicrobiology.ClientID %>");
        var L2 = document.getElementById("<%= HF_FlagTabCITO.ClientID %>");
        var L3 = document.getElementById("<%= HF_FlagTabAnatomical.ClientID %>");
        var L4 = document.getElementById("<%= HF_FlagTabMDC.ClientID %>");
        var L5 = document.getElementById("<%= HF_FlagTabPanel.ClientID %>");

        if (i == 0) {
            if (L0.value == "open") {
                return false;
            }
            else {
                document.getElementById("<%= HF_FlagTabLab.ClientID %>").value = "Clinical";
                document.getElementById("<%= ButtonChooseTabLab.ClientID %>").click();
            }
        }
        else if (i == 1) {
            if (L1.value == "open") {
                return false;
            }
            else {
                document.getElementById("<%= HF_FlagTabLab.ClientID %>").value = "Microbiology";
                document.getElementById("<%= ButtonChooseTabLab.ClientID %>").click();
            }
        }
        else if (i == 2) {
            if (L2.value == "open") {
                return false;
            }
            else {
                document.getElementById("<%= HF_FlagTabLab.ClientID %>").value = "CITO";
                document.getElementById("<%= ButtonChooseTabLab.ClientID %>").click();
            }
        }
        else if (i == 3) {
            if (L3.value == "open") {
                return false;
            }
            else {
                document.getElementById("<%= HF_FlagTabLab.ClientID %>").value = "Anatomical";
                document.getElementById("<%= ButtonChooseTabLab.ClientID %>").click();
            }
        }
        else if (i == 4) {
            if (L4.value == "open") {
                return false;
            }
            else {
                document.getElementById("<%= HF_FlagTabLab.ClientID %>").value = "MDC";
                document.getElementById("<%= ButtonChooseTabLab.ClientID %>").click();
            }
        }
        else if (i == 5) {
            if (L5.value == "open") {
                return false;
            }
            else {
                document.getElementById("<%= HF_FlagTabLab.ClientID %>").value = "Panel";
                document.getElementById("<%= ButtonChooseTabLab.ClientID %>").click();
            }
        }

        return true;
    }

    <%--function LoadTabRadData(tabname) {
        $('#modalRad').modal();
        document.getElementById("<%= HF_FlagTabRad.ClientID %>").value = tabname;
    }--%>

    function LoadTabRadData() {
        <%--var tabIndex = document.getElementById("<%= TabContainer1.ClientID %>");--%>
        var tabIndex = $find("MainContent_StdPlanning_TabContainer2");
        var i = tabIndex._activeTabIndex; 

        var R0 = document.getElementById("<%= HF_FlagTabXray.ClientID %>");
        var R1 = document.getElementById("<%= HF_FlagTabUSG.ClientID %>");
        var R2 = document.getElementById("<%= HF_FlagTabCT.ClientID %>");
        var R3 = document.getElementById("<%= HF_FlagTabMRI1.ClientID %>");
        var R4 = document.getElementById("<%= HF_FlagTabMRI3.ClientID %>");

        if (i == 0) {
            if (R0.value == "open") {
                return false;
            }
            else {
                document.getElementById("<%= HF_FlagTabRad.ClientID %>").value = "Xray";
                document.getElementById("<%= ButtonChooseTabRad.ClientID %>").click();
            }
        }
        else if (i == 1) {
            if (R1.value == "open") {
                return false;
            }
            else {
                document.getElementById("<%= HF_FlagTabRad.ClientID %>").value = "USG";
                document.getElementById("<%= ButtonChooseTabRad.ClientID %>").click();
            }
        }
        else if (i == 2) {
            if (R2.value == "open") {
                return false;
            }
            else {
                document.getElementById("<%= HF_FlagTabRad.ClientID %>").value = "CT";
                document.getElementById("<%= ButtonChooseTabRad.ClientID %>").click();
            }
        }
        else if (i == 3) {
            if (R3.value == "open") {
                return false;
            }
            else {
                document.getElementById("<%= HF_FlagTabRad.ClientID %>").value = "MRI1";
                document.getElementById("<%= ButtonChooseTabRad.ClientID %>").click();
            }
        }
        else if (i == 4) {
            if (R4.value == "open") {
                return false;
            }
            else {
                document.getElementById("<%= HF_FlagTabRad.ClientID %>").value = "MRI3";
                document.getElementById("<%= ButtonChooseTabRad.ClientID %>").click();
            }
        }

        return true;
    }

    //Mims
    function showdrugsinteraction() {

        RefreshIframe_Mims();
        $("#modalMimsInteraction").modal("show");
    }

    function RefreshIframe_Mims() {

        var LBL_Mims = document.getElementById("<%=lblMimsHtmlResult.ClientID%>");
        var obj = {
            type: 'parentRequest_MIMS',
            flag: 'refresh',
            responseData: LBL_Mims.innerHTML
        };
        document.getElementById("<%=IframeMIMS.ClientID%>").contentWindow.postMessage(obj, "*");
    }

    function klikForceNo(flag) {
        if (flag == "Allergy") {
            document.getElementById("<%=HF_ForceNo_MA_Allergy.ClientID%>").value = "true";
        }
        else if (flag == "Routine") {
            document.getElementById("<%=HF_ForceNo_MA_Routine.ClientID%>").value = "true";
        }
    }


	function btnToggleFutureOrder() {
		var FO = document.getElementById("MainContent_StdPlanning_divFutureOrder");
        var txt_primary = document.getElementById("MainContent_txtPrimary");
        var txt_clindiag_fo = document.getElementById("MainContent_StdPlanning_txtclinicaldiagnosis_FutureOrder");
		var logo_FO = document.getElementById("logo_FutureOrder");
		if (FO.style.display === "none") {
			FO.style.display = "block";
			logo_FO.innerHTML = `<i class="fa fa-minus-circle"></i>`;
			$('#MainContent_StdPlanning_chk_isLabsetFO').prop('checked', true);
			$('#MainContent_StdPlanning_chk_isLabsetFO').prop('disabled', false);

            txt_clindiag_fo.value = txt_primary.value;

		} else {

			var dp_lab_fo = document.getElementById("MainContent_StdPlanning_dp_labFutureOrder");
			var dp_rad_fo = document.getElementById("MainContent_StdPlanning_dp_radFutureOrder");
			var item_lab_fo = document.getElementById("MainContent_StdPlanning_Repeater1_FutureOrder");
			var item_rad_fo = document.getElementById("MainContent_StdPlanning_rptRadiology_FutureOrder");
			var txt_planotherLab_fo = document.getElementById("MainContent_StdPlanning_txtplanningotherLab_FutureOrder");
			var txt_planotherRad_fo = document.getElementById("MainContent_StdPlanning_txtplanningotherRad_FutureOrder");

			if (item_lab_fo != null || item_rad_fo != null || txt_planotherLab_fo.value != "" || txt_planotherRad_fo.value != "") {
				notificationWarningBig("Please clear Future Order Data to toggle off", "Future Order");
				return false;
			}
			else {
				FO.style.display = "none";
				dp_lab_fo.value = "";
				dp_rad_fo.value = "";
				txt_clindiag_fo.value = "";
				logo_FO.innerHTML = `<i class="fa fa-plus-circle"></i>`;
				$('#MainContent_StdPlanning_chk_isLabsetFO').prop('checked', false);
				$('#MainContent_StdPlanning_chk_isLabsetFO').prop('disabled', true);
			}
		}
    }

    function btnToggleFutureOrderDiagProc() {
        var FO = document.getElementById("MainContent_StdPlanning_divFutureOrderDiagProc");
        var logo_FO = document.getElementById("logo_FutureOrderDIAGPROC");
        if (FO.style.display === "none") {
            FO.style.display = "block";
            logo_FO.innerHTML = `<i class="fa fa-minus-circle"></i>`;

        } else {


            var dp_diag_fo = document.getElementById("MainContent_StdPlanning_dp_diag");
            var dp_proc_fo = document.getElementById("MainContent_StdPlanning_dp_proc");
            var item_diag_fo = document.getElementById("MainContent_StdPlanning_GridView_DiagnosticList_FutureOrder");
            var item_proc_fo = document.getElementById("MainContent_StdPlanning_GridView_ProcedureList_FutureOrder");
            var txt_planotherDiag_fo = document.getElementById("MainContent_StdPlanning_txtplanningotherDiagnostic_FutureOrder");
            var txt_planotherProc_fo = document.getElementById("MainContent_StdPlanning_txtplanningotherProcedure_FutureOrder");


            if (item_diag_fo != null || item_proc_fo != null || txt_planotherDiag_fo.value != "" || txt_planotherProc_fo.value != "") {
                notificationWarningBig("Please clear Future Order Data to toggle off", "Future Order");
                return false;
            }
            else {
                FO.style.display = "none";
                dp_diag_fo.value = "";
                dp_proc_fo.value = "";
                logo_FO.innerHTML = `<i class="fa fa-plus-circle"></i>`;
            }
        }
    }


    function notificationWarningBig(msg, title) {
        warningnotificationOption();
        toastr.warning(msg + ' <br /> <button type="button" class="btn btn-danger btn-sm" style="height: 25px; padding-top: 3px; width: 55px; float:right;">OK</button>', title);
    }

    function deleteFutureOrderDate(btn, mode) {
        if (mode == 'futureOrderDiag') {
            $("[id$='dp_diag']").val("");
            btn.style.display = "none";
        }  
        else if (mode == 'futureOrderProc') {
            $("[id$='dp_proc']").val("");
            btn.style.display = "none";
        }
        else if (mode == 'futureOrderLab') {
            $("[id$='dp_labFutureOrder']").val("");
            btn.style.display = "none";
        }
        else if (mode == 'futureOrderRad') {
            $("[id$='dp_radFutureOrder']").val("");
            btn.style.display = "none";
        }
    }


    function dateSelectLabFO() {

        var dp = $('#<%=dp_labFutureOrder.ClientID%>');
        dp.datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd M yyyy",
            language: "tr",
            todayHighlight: true,
			startDate: '+1d',
			endDate: '+6m',
        }).on('changeDate', function (ev) {
            $(this).blur();
            $(this).datepicker('hide');
            $("[id$='dp_labFutureOrderDelete']").css("display", "block");
        });
    }

	function dateSelectRadFO() {

		var dp = $('#<%=dp_radFutureOrder.ClientID%>');
			dp.datepicker({
				changeMonth: true,
				changeYear: true,
				format: "dd M yyyy",
                language: "tr",
				todayHighlight: true,
				startDate: '+1d',
				endDate: '+6m',
			}).on('changeDate', function (ev) {
				$(this).blur();
				$(this).datepicker('hide');
                $("[id$='dp_radFutureOrderDelete']").css("display", "block");
			});
    }

    function dateSelectDiag() {

        var dp = $('#<%=dp_diag.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr",
				todayHighlight: true,
                startDate: '+1d',
                endDate: '+6m',
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
                $("[id$='dp_diagDelete']").css("display", "block");
            });
    }

    function dateSelectProc() {

        var dp = $('#<%=dp_proc.ClientID%>');
        dp.datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd M yyyy",
            language: "tr",
			todayHighlight: true,
            startDate: '+1d',
            endDate: '+6m',
        }).on('changeDate', function (ev) {
            $(this).blur();
            $(this).datepicker('hide');
            $("[id$='dp_procDelete']").css("display", "block");
        });
    }

    function LabSetCheckBoxSet() {
		var FO = document.getElementById("MainContent_StdPlanning_divFutureOrder");
		var logo_FO = document.getElementById("logo_FutureOrder");
        if (FO.style.display === "none") {
            logo_FO.innerHTML = `<i class="fa fa-plus-circle"></i>`;
            $('#MainContent_StdPlanning_chk_isLabsetFO').prop('checked', false);
            $('#MainContent_StdPlanning_chk_isLabsetFO').prop('disabled', true);
        } else {
			logo_FO.innerHTML = `<i class="fa fa-minus-circle"></i>`;
			$('#MainContent_StdPlanning_chk_isLabsetFO').prop('checked', true);
			$('#MainContent_StdPlanning_chk_isLabsetFO').prop('disabled', false);
		}
	}

    


</script>
