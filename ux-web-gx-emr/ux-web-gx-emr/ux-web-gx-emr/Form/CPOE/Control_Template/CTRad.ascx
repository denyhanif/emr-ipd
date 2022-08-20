<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CTRad.ascx.cs" Inherits="Form_CPOE_Control_Template_CTRad" %>

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
    <asp:HiddenField ID="HFisBahasa_ctrad" runat="server" />
    <asp:HiddenField ID="hfguidadditional" runat="server" />
    <asp:UpdateProgress ID="uProgCTRad" runat="server" AssociatedUpdatePanelID="upCTRad">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
            </div>
            <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upCTRad">
        <ContentTemplate>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; display: none;">
                <div class="col-sm-4">
                    <asp:Label Font-Bold="true" Font-Size="12px" runat="server">
            <label id="lblbhs_workingdiag3"> Working Diagnosis </label>
                    </asp:Label>
                </div>
            </div>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 5px; display: none;">
                <div class="col-sm-12">
                    <asp:TextBox ID="txtCTNotes" Style="height: 35px; border-radius: 6px; width: 100%; max-width: 100%" Font-Size="15px" runat="server" onkeypress="return checkenter();" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; font-size: 11px;">
                <div class="col-sm-6">
                    <label>&nbsp; </label>
                    <br />
                    <label id="lblbhs_pleasetick1_ctrad">Please tick (&#10003) Rad test as request </label>
                </div>
                <div class="col-sm-6 text-right">
                    <label id="lblbhs_pleasetick2_ctrad">&#42Fasting minimal 4 hours before examinations</label>
                    <br />
                    <label id="lblbhs_pleasetick3_ctrad">&#42&#42For reservation please contact Radiology Department</label>
                </div>
            </div>

            <div class="row" style="margin: 0px; padding-bottom: 20px; background-color: white; padding-top: 0px">
                <div class="col-sm-6">
                    <div style="background-color: #e7e8ef; text-align: center; padding">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">HEAD</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <asp:GridView ID="gvw1" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw1');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw1');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Left','gvw1');" />
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
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">NECK</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:GridView ID="gvw3" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw3');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw3');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Left','gvw3');" />
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
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Thorax</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:GridView ID="gvw9" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw9');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw9');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Left','gvw9');" />
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
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Spine</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:GridView ID="gvw11" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw11');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw11');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Left','gvw11');" />
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
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Abdomen + Pelvic</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:GridView ID="gvw13" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw13');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw13');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Left','gvw13');" />
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

                <%-- ==================================================================================================================================== --%>
                <div class="col-sm-6" style="padding-top: 0px">
                    <div style="background-color: #e7e8ef; text-align: center">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">MUSCULOSKELETAL</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>
                    <asp:GridView ID="gvw2" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw2');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw2');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Left','gvw2');" />
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
                    <div style="background-color: #e7e8ef; text-align: center">
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">CT Angiography **</asp:Label>
                    </div>
                    <div style="padding-bottom: 20px; background-color: white"></div>

                    <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Cardiac</asp:Label>
                    <asp:GridView ID="gvw4" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw4');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw4');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Left','gvw4');" />
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
                    <div>&nbsp;</div>

                    <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Head</asp:Label>
                    <asp:GridView ID="gvw5" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw5');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw5');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Left','gvw5');" />
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
                    <div>&nbsp;</div>

                    <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Thorax</asp:Label>
                    <asp:GridView ID="gvw6" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw6');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw6');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Left','gvw6');" />
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
                    <div>&nbsp;</div>

                    <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Abdomen</asp:Label>
                    <asp:GridView ID="gvw7" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw7');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw7');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Left','gvw7');" />
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
                    <div>&nbsp;</div>

                    <asp:Label Font-Size="12px" Font-Bold="true" runat="server">Extremity</asp:Label>
                    <asp:GridView ID="gvw8" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw8');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw8');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Left','gvw8');" />
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
                        <asp:Label Font-Size="12px" Font-Bold="true" runat="server">CT Total Body</asp:Label>
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw10');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw10');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Left','gvw10');" />
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

                    <asp:GridView ID="gvw12" runat="server" BorderColor="White" Width="100%" ShowHeader="false"
                        AutoGenerateColumns="false" EmptyDataText="Service not Available" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="#cdce9d">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw12');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Right','gvw12');" />
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
                                            onclick="return serviceselectedctrad(this,'CT','Left','gvw12');" />
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