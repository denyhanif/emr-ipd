<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RptUSGRad.ascx.cs" Inherits="Form_CPOE_Control_Template_RptUSGRad" %>

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

    .std input[type="radio"] {
        margin-right: 2px;
        vertical-align:text-bottom;
    }

    .std label {
        font-size:11px;
    }

    .state::before {
        display: none;
    }
</style>

<div>
    <asp:HiddenField ID="HFisBahasa_usgrad" runat="server" />
    <asp:HiddenField ID="hfguidadditional" runat="server" />
    <%--<asp:UpdateProgress ID="uProgUSGRad" runat="server" AssociatedUpdatePanelID="upUSGRad">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
            </div>
            <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
   <%-- <asp:UpdatePanel runat="server" ID="upUSGRad">
        <ContentTemplate>--%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; display: none;">
                <div class="col-sm-4">
                    <asp:Label Font-Bold="true" Font-Size="12px" runat="server">
                    <label id="lblbhs_workingdiag2"> Working Diagnosis </label>
                    </asp:Label>
                </div>
            </div>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 5px; display: none;">
                <div class="col-sm-12">
                    <asp:TextBox ID="txtUsgNotes" Style="height: 35px; border-radius: 6px; width: 100%; max-width: 100%" Font-Size="15px" onkeypress="return checkenter();" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; font-size: 11px;">
                <div class="col-sm-6">
                    <label>&nbsp; </label>
                    <br />
                    <label id="lblbhs_pleasetick1_usgrad">Please tick (&#10003) Rad test as request </label>
                </div>
                <div class="col-sm-6 text-right">
                    <label id="lblbhs_pleasetick2_usgrad">&#42Fasting minimal 4 hours before examinations</label>
                    <br />
                    <label id="lblbhs_pleasetick3_usgrad">&#42&#42For reservation please contact Radiology Department</label>
                </div>
            </div>

            <%-- ============================================= USG ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12">
                    <asp:Label Font-Size="12px" Font-Bold="true" runat="server">USG</asp:Label>
                </div>
            </div>

            <%-- ============================================= USG ============================================================== --%>

            <div class="row" style="margin: 0px; padding-bottom: 20px; background-color: white; padding-top: 20px">
                <div class="col-sm-4" style="padding-top: 0px">
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_USG_stdusg_gvw1">
                        <asp:Repeater ID="rpt1" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedusg(this,'USG','Right','gvw1');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedusg(this,'USG','Right','gvw1');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedusg(this,'USG','Left','gvw1');" />
                                    </td>
                                    <td style="display:none;">
                                        <%# Eval("item_id").ToString() %> 
                                    </td>
                                    <td style="display:none;">
                                        <%# Eval("is_leftright").ToString() %> 
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
                    
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_USG_stdusg_gvw2">
                        <asp:Repeater ID="rpt2" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedusg(this,'USG','Right','gvw2');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedusg(this,'USG','Right','gvw2');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedusg(this,'USG','Left','gvw2');" />
                                    </td>
                                    <td style="display:none;">
                                        <%# Eval("item_id").ToString() %> 
                                    </td>
                                    <td style="display:none;">
                                        <%# Eval("is_leftright").ToString() %> 
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

            <%-- ============================================= USG - COLOR DOPPLER ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12">
                    <asp:Label Font-Size="12px" Font-Bold="true" runat="server">USG - COLOR DOPPLER</asp:Label>
                </div>
            </div>

            <%-- ============================================= USG - COLOR DOPPLER ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 20px; background-color: white; padding-top: 20px">
                <div class="col-sm-6">
                    
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_USG_stdusg_gvw3">
                        <asp:Repeater ID="rpt3" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedusg(this,'USG','Right','gvw3');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedusg(this,'USG','Right','gvw3');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedusg(this,'USG','Left','gvw3');" />
                                    </td>
                                    <td style="display:none;">
                                        <%# Eval("item_id").ToString() %> 
                                    </td>
                                    <td style="display:none;">
                                        <%# Eval("is_leftright").ToString() %> 
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