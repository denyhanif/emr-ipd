<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CitoLab.ascx.cs" Inherits="Form_CPOE_Control_Template_CitoLab" %>

<style>
    .itemlab {
        font-size: 11px;
        font-family: Helvetica, Arial, sans-serif;
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

<asp:HiddenField ID="HFisBahasa_citolab" runat="server" />
<asp:HiddenField ID="hfguidadditional" runat="server" />
<div>
    <asp:UpdateProgress ID="uProgCitoLab" runat="server" AssociatedUpdatePanelID="upcitolab">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
            </div>
            <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upcitolab">
        <ContentTemplate>
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
                        <asp:GridView ID="gvw1" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedcito(this,'CitoLab','','gvw1');" />
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

                <div class="col-sm-4">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw2" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedcito(this,'CitoLab','','gvw2');" />
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

                <div class="col-sm-4" style="padding-top: 0px">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw3" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedcito(this,'CitoLab','','gvw3');" />
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
            </div>

            <div class="row" style="margin: 0px; padding-bottom: 10px; padding-top: 10px; text-align: center">
                <div class="col-sm-12" style="background-color: #e7e8ef">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">BLOOD CHEMISTRY</asp:Label>
                </div>
            </div>

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4">
                    <asp:GridView ID="gvw4" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="LIVER FUNCTION TEST">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedcito(this,'CitoLab','','gvw4');" />
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
                    &nbsp;
                    <asp:GridView ID="gvw7" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="HEART FUNCTION TEST">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedcito(this,'CitoLab','','gvw7');" />
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
                    &nbsp;
                    <asp:GridView ID="gvw10" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="URINE">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedcito(this,'CitoLab','','gvw10');" />
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

                <div class="col-sm-4">
                    <asp:GridView ID="gvw5" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="ELECTROLYTE & BLOOD GAS">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedcito(this,'CitoLab','','gvw5');" />
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
                    &nbsp;
                    <asp:GridView ID="gvw8" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="GLUCOSE">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedcito(this,'CitoLab','','gvw8');" />
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

                <div class="col-sm-4" style="padding-top: 0px">
                    <asp:GridView ID="gvw6" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="RENAL FUNCTION TEST">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedcito(this,'CitoLab','','gvw6');" />
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
                    &nbsp;
                    <asp:GridView ID="gvw9" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="OTHERS">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedcito(this,'CitoLab','','gvw9');" />
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