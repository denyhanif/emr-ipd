<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RptMRIhalfRad.ascx.cs" Inherits="Form_CPOE_Control_Template_RptMRIhalfRad" %>

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
    <asp:HiddenField ID="HFisBahasa_mrihalfrad" runat="server" />
    <asp:HiddenField ID="hfguidadditional" runat="server" />
   <%-- <asp:UpdateProgress ID="uProgMRIHalf" runat="server" AssociatedUpdatePanelID="upMRIHalfRad">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
            </div>
            <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
   <%-- <asp:UpdatePanel runat="server" ID="upMRIHalfRad">
        <ContentTemplate>--%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; display: none;">
                <div class="col-sm-4">
                    <asp:Label Font-Bold="true" Font-Size="12px" runat="server">
            <label id="lblbhs_workingdiag4"> Working Diagnosis </label>
                    </asp:Label>
                </div>
            </div>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 5px; display: none;">
                <div class="col-sm-12">
                    <asp:TextBox ID="txtMRIhalfNotes" Style="height: 35px; border-radius: 6px; width: 100%; max-width: 100%" Font-Size="15px" runat="server" onkeypress="return checkenter();" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; font-size: 11px;">
                <div class="col-sm-6">
                    <label>&nbsp; </label>
                    <br />
                    <label id="lblbhs_pleasetick1_mrihalfrad">Please tick (&#10003) Rad test as request </label>
                </div>
                <div class="col-sm-6 text-right">
                    <label id="lblbhs_pleasetick2_mrihalfrad">&#42Fasting minimal 4 hours before examinations</label>
                </div>
            </div>

            <div class="row" style="margin: 0px; padding-bottom: 20px; background-color: white; padding-top: 0px">
                <div class="col-sm-6" style="padding-top: 0px; background-color: white">
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI BRAIN & NECK</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_MRI1_stdmrihalf_gvw1">
                        <asp:Repeater ID="rpt1" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw1');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw1');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw1');" />
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

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI SPINE</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_MRI1_stdmrihalf_gvw3">
                        <asp:Repeater ID="rpt3" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw3');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw3');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw3');" />
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

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI THORAX</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_MRI1_stdmrihalf_gvw5">
                        <asp:Repeater ID="rpt5" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw5');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw5');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw5');" />
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

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI ABDOMEN</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_MRI1_stdmrihalf_gvw7">
                        <asp:Repeater ID="rpt7" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw7');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw7');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw7');" />
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

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI TOTAL BODY</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_MRI1_stdmrihalf_gvw11">
                        <asp:Repeater ID="rpt11" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw11');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw11');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw11');" />
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

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI ADVANCED NEURO</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_MRI1_stdmrihalf_gvw9">
                        <asp:Repeater ID="rpt9" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw9');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw9');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw9');" />
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

                <%-- =============================================================================================================================== --%>
                <div class="col-sm-6" style="padding-top: 0px; background-color: white">
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI MUSCULOSKELETAL</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Upper Extremity</asp:Label>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_MRI1_stdmrihalf_gvw2">
                        <asp:Repeater ID="rpt2" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw2');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw2');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw2');" />
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
                    
                    <div>&nbsp;</div>

                    <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Lower Extremity</asp:Label>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_MRI1_stdmrihalf_gvw4">
                        <asp:Repeater ID="rpt4" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw4');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw4');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw4');" />
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

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MR ANGIOGRAPHY</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_MRI1_stdmrihalf_gvw6">
                        <asp:Repeater ID="rpt6" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw6');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw6');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw6');" />
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

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI Breast</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_MRI1_stdmrihalf_gvw8">
                        <asp:Repeater ID="rpt8" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw8');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw8');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw8');" />
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

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Others</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <table style="width:100%;" id="MainContent_StdPlanning_TabContainer2_MRI1_stdmrihalf_gvw10">
                        <asp:Repeater ID="rpt10" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:70%;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Text='<%# Eval("item_name").ToString() + " " + (Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**") %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' CssClass="std"
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw10');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>' Text="R"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="R" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>' 
						                        Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw10');" />
                                    </td>
                                    <td style="width:15%;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>' Text="L"
                                                Enabled='<%# Eval("issubmit").ToString() != "1" && Eval("IsSendHope").ToString() != "1" %>' Value="L" CssClass="std"
                                                Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                                Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                                onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw10');" />
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