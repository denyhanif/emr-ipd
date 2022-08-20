<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RptMDCLab.ascx.cs" Inherits="Form_CPOE_Control_Template_RptMDCLab" %>

<style>
    .itemlab {
        font-size: 11px;
        font-family: Helvetica;
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

<div>
    <asp:HiddenField ID="HFisBahasa_mdc" runat="server" />
    <asp:HiddenField ID="hfguidadditional" runat="server" />
   <%-- <asp:UpdateProgress ID="uProgmdc" runat="server" AssociatedUpdatePanelID="upmdc">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
            </div>
            <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <%--<asp:UpdatePanel runat="server" ID="upmdc">
        <ContentTemplate>--%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; display: none;">
                <div class="col-sm-4">
                    <asp:Label Font-Bold="true" Font-Size="12px" runat="server">
                        <label id="lblbhs_clinicaldiaglab2">Clinical Diagnosis</label>
                    </asp:Label>
                </div>
            </div>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 5px; display: none;">
                <div class="col-sm-12">
                    <asp:TextBox ID="txtMDCNotes" Style="height: 35px; border-radius: 6px; width: 100%; max-width: 100%" Font-Size="15px" runat="server" onkeypress="return checkenter();" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; font-size: 11px;">
                <div class="col-sm-6">
                    <label>&nbsp; </label>
                    <br />
                    <label id="lblbhs_pleasetick1_mdc">Please tick (&#10003) Lab test as request </label>
                </div>
                <div class="col-sm-6 text-right">
                    &nbsp;
                </div>
            </div>

            <%-- ============================================= INFECTIOUS DISESASE ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12" style="background-color: #e7e8ef;">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">INFECTIOUS DISESASE</asp:Label>
                </div>
            </div>

            <%-- =========================================================CONTENT====================================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-12" style="padding-bottom: 15px;">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabMDC_stdmdc_gvw1">
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
                                                    onclick="return serviceselectedmdc(this,'MDCLab','','gvw1');" />
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

            <%-- ============================================= GENETIC MUTATION ANALYSIS ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center;">
                <div class="col-sm-12" style="background-color: #e7e8ef;">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">GENETIC MUTATION ANALYSIS</asp:Label>
                </div>
            </div>

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px;">
                <div class="col-sm-4" style="padding-top: 2%;">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabMDC_stdmdc_gvw2">
                            <asp:Repeater ID="rpt2" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             LUNG CANCER
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmdc(this,'MDCLab','','gvw2');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabMDC_stdmdc_gvw3">
                            <asp:Repeater ID="rpt3" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             BREAST CANCER
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmdc(this,'MDCLab','','gvw3');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabMDC_stdmdc_gvw4">
                            <asp:Repeater ID="rpt4" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             CERVICAL CANCER
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmdc(this,'MDCLab','','gvw4');" />
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

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabMDC_stdmdc_gvw5">
                            <asp:Repeater ID="rpt5" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             COLON CANCER
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmdc(this,'MDCLab','','gvw5');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabMDC_stdmdc_gvw6">
                            <asp:Repeater ID="rpt6" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             HEMATOLOGI CANCER
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmdc(this,'MDCLab','','gvw6');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabMDC_stdmdc_gvw7">
                            <asp:Repeater ID="rpt7" runat="server">
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
                                                    onclick="return serviceselectedmdc(this,'MDCLab','','gvw7');" />
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