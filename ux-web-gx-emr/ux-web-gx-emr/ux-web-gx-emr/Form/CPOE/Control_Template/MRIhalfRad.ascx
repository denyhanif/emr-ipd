<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MRIhalfRad.ascx.cs" Inherits="Form_CPOE_Control_Template_MRIhalfRad" %>

<style>
    .itemlab {
        font-size: 11px;
        font-family: Helvetica;
        color: #171717;
        font-weight: normal;
        padding-top: 0px;
        margin-bottom: 0px;
    }

    .mycheckbox input[type="checkbox"] {
        margin-right: 5%;
    }

    .state::before {
        display: none;
    }
</style>

<div>
    <asp:HiddenField ID="HFisBahasa_mrihalfrad" runat="server" />
    <asp:HiddenField ID="hfguidadditional" runat="server" />
    <asp:UpdateProgress ID="uProgMRIHalf" runat="server" AssociatedUpdatePanelID="upMRIHalfRad">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
            </div>
            <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upMRIHalfRad">
        <ContentTemplate>
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
                    <asp:GridView ID="gvw1" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 45 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw1');" />
                                        <div class="state p-success" style="margin-top: -3px; white-space: <%# Eval("item_name").ToString().Length <= 45 ? "nowrap" : "normal" %>;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 45 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </label>
                                                <asp:HiddenField ID="Hidden_chkrad" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="R"
                                            Visible='<%# Eval("is_leftright").ToString() != "False" %>' Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw1');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">R </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="L" Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                            Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw1');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">L </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="is_leftright" DataField="is_leftright" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI SPINE</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:GridView ID="gvw3" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 41 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw3');" />
                                        <div class="state p-success" style="margin-top: -3px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 41 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </label>
                                                <asp:HiddenField ID="Hidden_chkrad" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="R"
                                            Visible='<%# Eval("is_leftright").ToString() != "False" %>' Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw3');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">R </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="L" Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                            Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw3');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">L </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="is_leftright" DataField="is_leftright" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI THORAX</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:GridView ID="gvw5" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 42 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw5');" />
                                        <div class="state p-success" style="margin-top: -3px; white-space: <%# Eval("item_name").ToString().Length <= 42 ? "nowrap" : "normal" %>;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 42 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </label>
                                                <asp:HiddenField ID="Hidden_chkrad" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="R"
                                            Visible='<%# Eval("is_leftright").ToString() != "False" %>' Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw5');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">R </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="L" Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                            Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw5');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">L </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="is_leftright" DataField="is_leftright" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI ABDOMEN</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:GridView ID="gvw7" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 38 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw7');" />
                                        <div class="state p-success" style="margin-top: -3px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 38 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </label>
                                                <asp:HiddenField ID="Hidden_chkrad" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="R"
                                            Visible='<%# Eval("is_leftright").ToString() != "False" %>' Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw7');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">R </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="L" Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                            Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw7');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">L </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="is_leftright" DataField="is_leftright" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI TOTAL BODY</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:GridView ID="gvw11" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 35 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw11');" />
                                        <div class="state p-success" style="margin-top: -3px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 35 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </label>
                                                <asp:HiddenField ID="Hidden_chkrad" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="R"
                                            Visible='<%# Eval("is_leftright").ToString() != "False" %>' Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw11');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">R </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="L" Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                            Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw11');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">L </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="is_leftright" DataField="is_leftright" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI ADVANCED NEURO</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:GridView ID="gvw9" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 37 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw9');" />
                                        <div class="state p-success" style="margin-top: -3px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 37 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </label>
                                                <asp:HiddenField ID="Hidden_chkrad" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="R"
                                            Visible='<%# Eval("is_leftright").ToString() != "False" %>' Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw9');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">R </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="L" Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                            Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw9');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">L </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="is_leftright" DataField="is_leftright" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                </div>

                <%-- =============================================================================================================================== --%>
                <div class="col-sm-6" style="padding-top: 0px; background-color: white">
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI MUSCULOSKELETAL</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Upper Extremity</asp:Label>
                    <asp:GridView ID="gvw2" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="width:290px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw2');" />
                                        <div class="state p-success" style="margin-top: -3px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </label>
                                                <asp:HiddenField ID="Hidden_chkrad" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 50 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="R"
                                            Visible='<%# Eval("is_leftright").ToString() != "False" %>' Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw2');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 50 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">R </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 50 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="L" Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                            Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw2');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 50 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">L </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="is_leftright" DataField="is_leftright" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                    <div>&nbsp;</div>

                    <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Lower Extremity</asp:Label>
                    <asp:GridView ID="gvw4" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="width:265px; margin-top: <%# Eval("item_name").ToString().Length > 37 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw4');" />
                                        <div class="state p-success" style="margin-top: -3px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 37 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </label>
                                                <asp:HiddenField ID="Hidden_chkrad" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="R"
                                            Visible='<%# Eval("is_leftright").ToString() != "False" %>' Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw4');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">R </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="L" Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                            Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw4');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">L </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="is_leftright" DataField="is_leftright" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MR ANGIOGRAPHY</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:GridView ID="gvw6" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 44 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw6');" />
                                        <div class="state p-success" style="margin-top: -3px; white-space: <%# Eval("item_name").ToString().Length <= 44 ? "nowrap" : "normal" %>;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 44 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </label>
                                                <asp:HiddenField ID="Hidden_chkrad" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="R"
                                            Visible='<%# Eval("is_leftright").ToString() != "False" %>' Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw6');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">R </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="L" Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                            Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw6');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">L </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="is_leftright" DataField="is_leftright" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MRI Breast</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:GridView ID="gvw8" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw8');" />
                                        <div class="state p-success" style="margin-top: -3px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </label>
                                                <asp:HiddenField ID="Hidden_chkrad" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">

                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="R"
                                            Visible='<%# Eval("is_leftright").ToString() != "False" %>' Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw8');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">R </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="L" Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                            Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw8');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">L </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="is_leftright" DataField="is_leftright" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>

                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Others</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:GridView ID="gvw10" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw10');" />
                                        <div class="state p-success" style="margin-top: -3px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </label>
                                                <asp:HiddenField ID="Hidden_chkrad" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbRight" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="R"
                                            Visible='<%# Eval("is_leftright").ToString() != "False" %>' Checked='<%# Eval("remarks").ToString() == "Right" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Right','gvw10');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">R </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <ItemTemplate>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <div class="pretty p-default p-round" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>; display: <%# Eval("is_leftright").ToString() != "False" ? "inline-block" : "none" %>;">
                                        <asp:RadioButton runat="server" ID="rbLeft" GroupName='<%# Eval("item_name").ToString() %>'
                                            Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="L" Visible='<%# Eval("is_leftright").ToString() != "False" %>'
                                            Checked='<%# Eval("remarks").ToString() == "Left" %>'
                                            onclick="return serviceselectedmrihalf(this,'MRI1','Left','gvw10');" />
                                        <div class="state p-primary-o" style="margin-top: -3px; white-space: normal;">
                                            <div style="margin-left: 15px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                                <label class="label-tick" style="text-indent: 0;">L </label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="is_leftright" DataField="is_leftright" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>