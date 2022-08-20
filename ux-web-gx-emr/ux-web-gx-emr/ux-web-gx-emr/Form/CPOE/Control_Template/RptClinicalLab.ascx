<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RptClinicalLab.ascx.cs" Inherits="Form_CPOE_Control_Template_RptClinicalLab" %>

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
        padding-bottom: 12px;
    }
</style>

<div>
    <asp:HiddenField ID="HFisBahasa_clinicallab" runat="server" />
    <asp:HiddenField ID="hfguidadditional" runat="server" />
    <%--<asp:HiddenField runat="server" ID="hfbuilderobject" />
    <asp:Button runat="server" ID="btnvaluecpoe" OnClick="btnGetValueCPOE" Style="display: none" />--%>
    <%--<asp:UpdateProgress ID="uProghematology" runat="server" AssociatedUpdatePanelID="upclinicallab">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
            </div>
            <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>

    <%--<asp:UpdatePanel runat="server" ID="upclinicallab">
        <ContentTemplate>--%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; display: none;">
                <div class="col-sm-4">
                    <asp:Label Font-Bold="true" Font-Size="12px" runat="server">
                <label id="lblbhs_clinicaldiaglab1">Clinical Diagnosis</label>
                    </asp:Label>
                </div>
            </div>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 5px; display: none">
                <div class="col-sm-12">
                    <asp:TextBox ID="txtClinicNotex" Style="height: 35px; border-radius: 6px; width: 100%; max-width: 100%" Font-Size="15px" runat="server" CssClass="form-control" onkeypress="return checkenter();"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; font-size: 11px;">
                <div class="col-sm-6">
                    <label>&nbsp; </label>
                    <br />
                    <label id="lblbhs_pleasetick1_clinicallab">Please tick (&#10003) Lab test as request </label>
                </div>
                <div class="col-sm-6 text-right">
                    <label id="lblbhs_pleasetick2_clinicallab">&#42Fasting 10-12 hours</label>
                    <label style="margin-left: 25px; margin-right: -4px; display: none;">1 06:00 - 10:00</label>
                    <br />
                    <label id="lblbhs_pleasetick3_clinicallab">&#42&#42Fasting 12-14 hours</label>
                    <label style="margin-left: 25px; display: none;">2 16:00 - 20:00</label>
                </div>
            </div>

            <%-- ============================================= HEMATOLOGY ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">HEMATOLOGY</asp:Label>
                </div>
            </div>

            <%-- ============================================= HEMATOLOGY ============================================================== --%>

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">

                    <div class="checkbox-grup-width">

                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw1">
                            <asp:Repeater ID="rpt1" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             HEMATOLOGY - GENERAL
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw1');" />
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

                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw2">
                            <asp:Repeater ID="rpt2" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             HEMATOLOGY - COAGULATION
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw2');" />
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

                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw3">
                            <asp:Repeater ID="rpt3" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             HEMATOLOGY - COAGULATION
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw3');" />
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

            <%--<div style="background-color: white">&nbsp</div>--%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">

                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw4">
                            <asp:Repeater ID="rpt4" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             HEMATOLOGY - SPECIFIC
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw4');" />
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
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw5">
                            <asp:Repeater ID="rpt5" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             HEMATOLOGY - SPECIFIC
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw5');" />
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
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw6">
                            <asp:Repeater ID="rpt6" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             HEMATOLOGY - SPECIFIC
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw6');" />
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
                    <div>&nbsp</div>
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw7">
                            <asp:Repeater ID="rpt7" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             PLATELETE FUNCTION TEST
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw7');" />
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

            <div style="background-color: white">&nbsp</div>
            <%-- ============================================= BLOOD CHEMISTRY ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">BLOOD CHEMISTRY</asp:Label>
                </div>
            </div>

            <%-- ============================================= BLOOD CHEMISTRY ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%; padding-right: 5%;">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw8">
                            <asp:Repeater ID="rpt8" runat="server">
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
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw8');" />
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
                    
                    <div>&nbsp</div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw11">
                            <asp:Repeater ID="rpt11" runat="server">
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
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw11');" />
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
    
                    <div>&nbsp</div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw14">
                            <asp:Repeater ID="rpt14" runat="server">
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
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw14');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw9">
                            <asp:Repeater ID="rpt9" runat="server">
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
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw9');" />
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

                    <div>&nbsp</div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw12">
                            <asp:Repeater ID="rpt12" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             PANCREAS FUNCTION TEST
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw12');" />
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

                    <div>&nbsp</div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw15">
                            <asp:Repeater ID="rpt15" runat="server">
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
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw15');" />
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
                    
                    <div>&nbsp</div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw16">
                            <asp:Repeater ID="rpt16" runat="server">
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
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw16');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw10">
                            <asp:Repeater ID="rpt10" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             LIPID TEST
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw10');" />
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
                    
                    <div>&nbsp</div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw13">
                            <asp:Repeater ID="rpt13" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             DRUG MONITORING
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw13');" />
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

            <div style="background-color: white">&nbsp</div>
            <%-- ============================================= IMMUNOLOGY ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">IMMUNOLOGY</asp:Label>
                </div>
            </div>
            <%-- ============================================= IMMUNOLOGY ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw17">
                            <asp:Repeater ID="rpt17" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw17');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw18">
                            <asp:Repeater ID="rpt18" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                            
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw18');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw19">
                            <asp:Repeater ID="rpt19" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                            
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw19');" />
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

            <div style="background-color: white">&nbsp</div>
            <%-- ============================================= SEROLOGY ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">SEROLOGY</asp:Label>
                </div>
            </div>
            <%-- ============================================= SEROLOGY ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%;">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw20">
                            <asp:Repeater ID="rpt20" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             TUMOR MARKER
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw20');" />
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

                <div class="col-sm-4" style="padding-top: 2%;">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw21">
                            <asp:Repeater ID="rpt21" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             HEPATITIS MARKER
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw21');" />
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

                <div class="col-sm-4" style="padding-top: 2%;">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw22">
                            <asp:Repeater ID="rpt22" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             TORCH
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw22');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw23">
                            <asp:Repeater ID="rpt23" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             SEROLOGY - GENERAL
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw23');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw24">
                            <asp:Repeater ID="rpt24" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             SEROLOGY - GENERAL
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw24');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw25">
                            <asp:Repeater ID="rpt25" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             SEROLOGY - GENERAL
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw25');" />
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
            <div style="background-color: white">&nbsp</div>
            <%-- ============================================= HORMONE ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">HORMONE</asp:Label>
                </div>
            </div>
            <%-- ============================================= HORMONE ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw26">
                            <asp:Repeater ID="rpt26" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             HORMONE REPRODUCTION
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw26');" />
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

                    <div>&nbsp</div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw29">
                            <asp:Repeater ID="rpt29" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             OSTEOPOROSIS
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw29');" />
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
                <div class="col-sm-4" style="padding-top: 2%;">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw27">
                            <asp:Repeater ID="rpt27" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             HORMONE THYROID
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw27');" />
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

                <div class="col-sm-4" style="padding-top: 2%;">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw28">
                            <asp:Repeater ID="rpt28" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             HORMONE OTHERS
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw28');" />
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
                <div>&nbsp</div>
            </div>

            <div style="background-color: white">&nbsp</div>
            <%-- ============================================= URINE ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">URINE</asp:Label>
                </div>
            </div>
            <%-- ============================================= URINE ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%;">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw30">
                            <asp:Repeater ID="rpt30" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             GENERAL
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw30');" />
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
                <div class="col-sm-4" style="padding-top: 2%;">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw31">
                            <asp:Repeater ID="rpt31" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             URINE KUANTITATIF
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw31');" />
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
                <div class="col-sm-4" style="padding-top: 2%;">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw32">
                            <asp:Repeater ID="rpt32" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             DRUG ABUSE
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw32');" />
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

                    <div>&nbsp</div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw33">
                            <asp:Repeater ID="rpt33" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             PREGNANCY
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw33');" />
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

            <div style="background-color: white">&nbsp</div>
            <%-- ============================================= FAECES ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">FAECES</asp:Label>
                </div>
            </div>
            <%-- ============================================= FAECES ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw34">
                            <asp:Repeater ID="rpt34" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                            
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw34');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw35">
                            <asp:Repeater ID="rpt35" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw35');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw36">
                            <asp:Repeater ID="rpt36" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                            
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw36');" />
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
            <div style="background-color: white">&nbsp</div>

            <%-- ============================================= BODY FLUID ANALYSIS ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">BODY FLUID ANALYSIS</asp:Label>
                </div>
            </div>
            <%-- ============================================= BODY FLUID ANALYSIS ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4">
                    <div class="checkbox-grup-width">
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw37">
                            <asp:Repeater ID="rpt37" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw37');" />
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
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw38">
                            <asp:Repeater ID="rpt38" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw38');" />
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
                        <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw39">
                            <asp:Repeater ID="rpt39" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw39');" />
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

            <div style="background-color: white">&nbsp</div>
            <%-- ============================================= MOLECULAR DIAGNOSTIC ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">MOLECULAR DIAGNOSTIC</asp:Label>
                </div>
            </div>
            <%-- ============================================= MOLECULAR DIAGNOSTIC ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw40">
                            <asp:Repeater ID="rpt40" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw40');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw41">
                            <asp:Repeater ID="rpt41" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw41');" />
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
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer1_TabPanel1_stdclinic_gvw42">
                            <asp:Repeater ID="rpt42" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3">
                                             
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                             <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                    Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                    onclick="return serviceselected(this,'ClinicLab','','gvw42');" />
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