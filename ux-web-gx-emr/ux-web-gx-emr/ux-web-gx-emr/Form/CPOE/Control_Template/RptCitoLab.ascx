<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RptCitoLab.ascx.cs" Inherits="Form_CPOE_Control_Template_RptCitoLab" %>

<style>
    .itemlab {
        font-size: 11px;
        font-family: Helvetica, Arial, sans-serif;
        color: #171717;
        font-weight: normal;
        padding-top: 0px;
        margin-bottom: 0px;
    }

    .std input[type="checkbox"] {
        margin-right: 7px;
        vertical-align:text-bottom;
    }

    .std label {
        font-size:11px;
    }

    .header-margin {
        margin-bottom: 4px;
    }
</style>

<asp:HiddenField ID="HFisBahasa_citolab" runat="server" />
<asp:HiddenField ID="hfguidadditional" runat="server" />
<div>
    <%--<asp:UpdateProgress ID="uProgCitoLab" runat="server" AssociatedUpdatePanelID="upcitolab">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
            </div>
            <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <%--<asp:UpdatePanel runat="server" ID="upcitolab">
        <ContentTemplate>--%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; display: none;">
                <div class="col-sm-4">
                    <asp:Label Font-Bold="true" Font-Size="12px" runat="server">
            <label id="lblbhs_clinicaldiaglab3">Clinical Diagnosis</label>
                    </asp:Label>
                </div>
            </div>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 5px; display: none;">
                <div class="col-sm-12">
                    <asp:TextBox ID="txtCitoNotes" Style="height: 35px; border-radius: 6px; width: 100%; max-width: 100%" Font-Size="15px" runat="server" onkeypress="return checkenter();" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; font-size: 11px;">
                <div class="col-sm-6">
                    <label>&nbsp; </label>
                    <br />
                    <label id="lblbhs_pleasetick1_citolab">Please tick (&#10003) Lab test as request </label>
                </div>
                <div class="col-sm-6 text-right">
                    &nbsp;
                </div>
            </div>

            <%-- =========================================================CONTENT====================================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-4" style="padding-left: 0px;">
                    <div style="background-color: #e7e8ef; padding-right: 5px;">
                        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">HEMATOLOGY</asp:Label>
                    </div>
                </div>
                <div class="col-sm-8" style="padding-right: 0px;">
                    <div style="background-color: #e7e8ef; padding-left: 5px;">
                        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">HEMATOLOGY - COAGULATION</asp:Label>
                    </div>
                </div>
            </div>

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_CITO_stdcito_gvw1">
                            <asp:Repeater ID="rpt1" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedcito(this,'CitoLab','','gvw1');" />
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_id").ToString() %> 
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_name").ToString() %> 
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>

                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_CITO_stdcito_gvw2">
                            <asp:Repeater ID="rpt2" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedcito(this,'CitoLab','','gvw2');" />
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_id").ToString() %> 
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_name").ToString() %> 
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>

                    </div>
                </div>

                <div class="col-sm-4" style="padding-top: 0px">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_CITO_stdcito_gvw3">
                            <asp:Repeater ID="rpt3" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedcito(this,'CitoLab','','gvw3');" />
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_id").ToString() %> 
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_name").ToString() %> 
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>

                    </div>
                </div>
            </div>

            <div class="row" style="margin: 0px; padding-bottom: 10px; padding-top: 10px; text-align: center;">
                <div class="col-sm-12" style="background-color: #e7e8ef">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">BLOOD CHEMISTRY</asp:Label>
                </div>
            </div>

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_CITO_stdcito_gvw4">
                            <asp:Repeater ID="rpt4" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             LIVER FUNCTION TEST
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedcito(this,'CitoLab','','gvw4');" />
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_id").ToString() %> 
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_name").ToString() %> 
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>

                    &nbsp;
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_CITO_stdcito_gvw7">
                            <asp:Repeater ID="rpt7" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             HEART FUNCTION TEST
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedcito(this,'CitoLab','','gvw7');" />
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_id").ToString() %> 
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_name").ToString() %> 
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>

                    &nbsp;
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_CITO_stdcito_gvw10">
                            <asp:Repeater ID="rpt10" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             URINE
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedcito(this,'CitoLab','','gvw10');" />
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_id").ToString() %> 
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_name").ToString() %> 
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>

                </div>

                <div class="col-sm-4">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_CITO_stdcito_gvw5">
                            <asp:Repeater ID="rpt5" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             ELECTROLYTE & BLOOD GAS
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedcito(this,'CitoLab','','gvw5');" />
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_id").ToString() %> 
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_name").ToString() %> 
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>

                    &nbsp;
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_CITO_stdcito_gvw8">
                            <asp:Repeater ID="rpt8" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             GLUCOSE
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedcito(this,'CitoLab','','gvw8');" />
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_id").ToString() %> 
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_name").ToString() %> 
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>

                </div>

                <div class="col-sm-4" style="padding-top: 0px">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_CITO_stdcito_gvw6">
                            <asp:Repeater ID="rpt6" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             RENAL FUNCTION TEST
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedcito(this,'CitoLab','','gvw6');" />
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_id").ToString() %> 
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_name").ToString() %> 
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>

                    &nbsp;
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_CITO_stdcito_gvw9">
                            <asp:Repeater ID="rpt9" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             OTHERS
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedcito(this,'CitoLab','','gvw9');" />
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_id").ToString() %> 
                                        </td>
                                        <td style="display:none;">
                                            <%# Eval("item_name").ToString() %> 
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                </div>
            </div>

        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</div>