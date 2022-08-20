<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MDCLab.ascx.cs" Inherits="Form_CPOE_Control_Template_MDCLab" %>

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

    .header-margin {
        margin-bottom: 4px;
    }
</style>

<div>
    <asp:HiddenField ID="HFisBahasa_mdc" runat="server" />
    <asp:HiddenField ID="hfguidadditional" runat="server" />
    <asp:UpdateProgress ID="uProgmdc" runat="server" AssociatedUpdatePanelID="upmdc">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
            </div>
            <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upmdc">
        <ContentTemplate>
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
                    <asp:GridView ID="gvw1" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmdc(this,'MDCLab','','gvw1');" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "0px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                                <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <%-- ============================================= GENETIC MUTATION ANALYSIS ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12" style="background-color: #e7e8ef;">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">GENETIC MUTATION ANALYSIS</asp:Label>
                </div>
            </div>

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">
                    <asp:GridView ID="gvw2" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="LUNG CANCER">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmdc(this,'MDCLab','','gvw2');" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "0px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                                <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="col-sm-4" style="padding-top: 2%">
                    <asp:GridView ID="gvw3" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="BREAST CANCER">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmdc(this,'MDCLab','','gvw3');" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "0px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                                <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="col-sm-4" style="padding-top: 2%">
                    <asp:GridView ID="gvw4" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="CERVICAL CANCER">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmdc(this,'MDCLab','','gvw4');" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "0px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                                <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">
                    <asp:GridView ID="gvw5" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="COLON CANCER">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmdc(this,'MDCLab','','gvw5');" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "0px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                                <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="col-sm-4" style="padding-top: 2%">
                    <asp:GridView ID="gvw6" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="HEMATOLOGI CANCER">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmdc(this,'MDCLab','','gvw6');" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "0px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                                <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="col-sm-4" style="padding-top: 2%">
                    <asp:GridView ID="gvw7" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="OTHERS">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmdc(this,'MDCLab','','gvw7');" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "0px" %>;">
                                                <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                                <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="item_name" DataField="item_name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</div>