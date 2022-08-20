<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MicroLab.ascx.cs" Inherits="Form_CPOE_Control_Template_MicroLab" %>

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
    <asp:HiddenField ID="HFisBahasa_microlab" runat="server" />
    <asp:HiddenField ID="hfguidadditional" runat="server" />
    <asp:UpdateProgress ID="uProgMicroLab" runat="server" AssociatedUpdatePanelID="upmicrolab">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
            </div>
            <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upmicrolab">
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
                    <asp:GridView ID="gvw1" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="BLOOD SPECIMEN">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmicro(this,'MicroLab','','gvw1');" />
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
                    <asp:GridView ID="gvw2" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="URINE SPECIMEN">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmicro(this,'MicroLab','','gvw2');" />
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
                            <asp:TemplateField HeaderText="FAECES SPECIMEN">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmicro(this,'MicroLab','','gvw3');" />
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
                    <asp:GridView ID="gvw4" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="PUS SPECIMEN">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmicro(this,'MicroLab','','gvw4');" />
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
                    <asp:GridView ID="gvw5" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="SPUTUM SPECIMEN">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmicro(this,'MicroLab','','gvw5');" />
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
                            <asp:TemplateField HeaderText="CSF SPECIMEN">
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 41 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                            onclick="return serviceselectedmicro(this,'MicroLab','','gvw6');" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 41 ? "-7px" : "0px" %>;">
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
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw7" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="SECRETIONS SPECIMEN">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 39 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedmicro(this,'MicroLab','','gvw7');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw8" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="THROAT SWAB SPECIMEN">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedmicro(this,'MicroLab','','gvw8');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw9" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="BRONCHIAL FLUID SPECIMEN">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedmicro(this,'MicroLab','','gvw9');" />
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

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw10" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="PLEURAL FLUID SPECIMEN">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 41 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedmicro(this,'MicroLab','','gvw10');" />
                                            <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                                <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                                <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 41 ? "-7px" : "0px" %>;">
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw11" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="ASCITES FLUID SPECIMEN">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 41 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedmicro(this,'MicroLab','','gvw11');" />
                                            <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                                <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                                <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 41 ? "-7px" : "0px" %>;">
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw12" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="OTHERS SWAB SPECIMEN & OTHERS">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedmicro(this,'MicroLab','','gvw12');" />
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

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw13" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="SKIN SCRAPPING SPECIMEN">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedmicro(this,'MicroLab','','gvw13');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw14" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="NAIL SCRAPPING SPECIMEN">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedmicro(this,'MicroLab','','gvw14');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw15" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="BIOPSY SPECIMEN">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedmicro(this,'MicroLab','','gvw15');" />
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

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw16" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="WATER SPECIMEN">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedmicro(this,'MicroLab','','gvw16');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw17" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="FOOD SPECIMEN">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedmicro(this,'MicroLab','','gvw17');" />
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

                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw18" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="BEVERAGES SPECIMEN">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedmicro(this,'MicroLab','','gvw18');" />
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

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-4" style="padding-top: 2%">
                    <div class="checkbox-grup-width">
                        <asp:GridView ID="gvw19" runat="server" BorderColor="White" Width="100%"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="ENVIRONMENT SPECIMEN">
                                    <ItemTemplate>
                                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 41 ? "11px" : "6px" %>;">
                                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                                Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>'
                                                onclick="return serviceselectedmicro(this,'MicroLab','','gvw19');" />
                                            <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                                <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                                <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 41 ? "-7px" : "0px" %>;">
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

        </ContentTemplate>
    </asp:UpdatePanel>
</div>