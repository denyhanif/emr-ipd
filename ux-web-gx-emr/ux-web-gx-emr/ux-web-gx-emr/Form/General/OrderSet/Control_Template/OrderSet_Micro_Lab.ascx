<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderSet_Micro_Lab.ascx.cs" Inherits="Form_General_OrderSet_Control_Template_OrderSet_Micro_Lab" %>

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

<asp:GridView ID="grdCheckList" runat="server" Width="100%" Visible="false">
</asp:GridView>
<div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; font-size: 11px;">
    <div class="col-sm-6">
        <label>&nbsp; </label>
        <br />
        <label style="display: <%=setENG%>;">Please tick (&#10003) Lab test as request </label>
        <label style="display: <%=setIND%>;">Centang (&#10003) Pemeriksaan Lab yang akan diorder </label>
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
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                    <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                    <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                    <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                    <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                    <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                    <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                    <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                    <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                    <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                    <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
            </Columns>
        </asp:GridView>
    </div>

    <div class="col-sm-4" style="padding-top: 2%">
        <asp:GridView ID="gvw6" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="CSF SPECIMEN">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 45 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 45 ? "-7px" : "-2px" %>;">
                                    <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                    <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                    <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                    <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 39 ? "-7px" : "-2px" %>;">
                                        <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                        <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                    <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                    <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                        <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                        <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                    <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                    <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                        <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                        <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                    <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                    <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 41 ? "-7px" : "-2px" %>;">
                                        <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                        <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>

    </div>
    <div class="col-sm-4" style="padding-top: 2%">

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw11" runat="server" BorderColor="White" Width="100%" AutoGenerateColumns="false" ShowHeader="true">
                <Columns>
                     <asp:TemplateField HeaderText="ASCITES FLUID SPECIMEN">
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 42 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                    <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                    <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 42 ? "-7px" : "-2px" %>;">
                                        <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                        <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 41 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                    <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                    <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 41 ? "-7px" : "-2px" %>;">
                                        <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                        <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                    <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                    <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                        <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                        <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                    <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                    <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                        <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                        <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                    <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                    <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                        <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                        <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                    <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                    <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                        <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                        <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                    <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                    <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                        <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                        <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                    <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                    <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                        <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                        <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                    <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                    <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 41 ? "-7px" : "-2px" %>;">
                                        <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %>  </label>
                                        <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>

    </div>

</div>