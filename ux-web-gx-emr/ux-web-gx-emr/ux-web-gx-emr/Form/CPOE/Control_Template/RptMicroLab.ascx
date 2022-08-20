<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RptMicroLab.ascx.cs" Inherits="Form_CPOE_Control_Template_RptMicroLab" %>

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
    <asp:HiddenField ID="HFisBahasa_microlab" runat="server" />
    <asp:HiddenField ID="hfguidadditional" runat="server" />
    <%--<asp:UpdateProgress ID="uProgMicroLab" runat="server" AssociatedUpdatePanelID="upmicrolab">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
            </div>
            <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <%--<asp:UpdatePanel runat="server" ID="upmicrolab">
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
                    <asp:TextBox ID="txtMicroNotes" Style="height: 35px; border-radius: 6px; width: 100%; max-width: 100%" Font-Size="15px" runat="server" onkeypress="return checkenter();" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; font-size: 11px;">
                <div class="col-sm-6">
                    <label>&nbsp; </label>
                    <br />
                    <label id="lblbhs_pleasetick1_microlab">Please tick (&#10003) Lab test as request </label>
                </div>
                <div class="col-sm-6 text-right">
                    &nbsp;
                </div>
            </div>

            <%-- =========================================================CONTENT====================================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw1">
                            <asp:Repeater ID="rpt1" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             BLOOD SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw1');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw2">
                            <asp:Repeater ID="rpt2" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             URINE SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw2');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw3">
                            <asp:Repeater ID="rpt3" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             FAECES SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw3');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw4">
                            <asp:Repeater ID="rpt4" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             PUS SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw4');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw5">
                            <asp:Repeater ID="rpt5" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             SPUTUM SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw5');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw6">
                            <asp:Repeater ID="rpt6" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             CSF SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw6');" />
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
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw7">
                            <asp:Repeater ID="rpt7" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             SECRETIONS SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw7');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw8">
                            <asp:Repeater ID="rpt8" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             THROAT SWAB SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw8');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw9">
                            <asp:Repeater ID="rpt9" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             BRONCHIAL FLUID SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw9');" />
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

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw10">
                            <asp:Repeater ID="rpt10" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             PLEURAL FLUID SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw10');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw11">
                            <asp:Repeater ID="rpt11" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             ASCITES FLUID SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw11');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw12">
                            <asp:Repeater ID="rpt12" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             OTHERS SWAB SPECIMEN & OTHERS
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw12');" />
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

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw13">
                            <asp:Repeater ID="rpt13" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             SKIN SCRAPPING SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw13');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw14">
                            <asp:Repeater ID="rpt14" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             NAIL SCRAPPING SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw14');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw15">
                            <asp:Repeater ID="rpt15" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             BIOPSY SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw15');" />
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

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw16">
                            <asp:Repeater ID="rpt16" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             WATER SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw16');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw17">
                            <asp:Repeater ID="rpt17" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             FOOD SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw17');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw18">
                            <asp:Repeater ID="rpt18" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             BEVERAGES SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw18');" />
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

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_Microbiology_stdmicro_gvw19">
                            <asp:Repeater ID="rpt19" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             ENVIRONMENT SPECIMEN
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselectedmicro(this,'MicroLab','','gvw19');" />
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

        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</div>