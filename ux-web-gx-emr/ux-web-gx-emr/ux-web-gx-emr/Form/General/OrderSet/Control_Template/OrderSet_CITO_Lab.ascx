<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderSet_CITO_Lab.ascx.cs" Inherits="Form_General_OrderSet_Control_Template_OrderSet_CITO_Lab" %>

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
<div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
    <div class="col-sm-4" style="background-color: lightgrey">
        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">HEMATOLOGY</asp:Label>
    </div>
    <div class="col-sm-1" style="background-color: white">
    </div>
    <div class="col-sm-7" style="background-color: lightgrey">
        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">HEMATOLOGY - COAGULATION</asp:Label>
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
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -3px; white-space: normal;">
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
    <div class="col-sm-1"></div>
    <div class="col-sm-3">
        <asp:GridView ID="gvw2" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            <div class="state p-success" style="margin-top: -3px; white-space: normal;">
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
    <div class="col-sm-4" style="padding-top: 0px">

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw3" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -3px; white-space: normal;">
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

<div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 10px; text-align: center">
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
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            <div class="state p-success" style="margin-top: -3px; white-space: normal;">
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
        &nbsp;
    <asp:GridView ID="gvw7" runat="server" BorderColor="White" Width="100%"
        AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="HEART FUNCTION TEST">
                <ItemTemplate>
                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <div class="state p-success" style="margin-top: -3px; white-space: normal;">
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
    <div class="col-sm-4">
        <asp:GridView ID="gvw5" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="ELECTROLYTE & BLOOD GAS">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            <div class="state p-success" style="margin-top: -3px; white-space: normal;">
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
        &nbsp;
            <asp:GridView ID="gvw8" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="GLUCOSE">
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                <div class="state p-success" style="margin-top: -3px; white-space: normal;">
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
    <div class="col-sm-4" style="padding-top: 0px">
        <asp:GridView ID="gvw6" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="RENAL FUNCTION TEST">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            <div class="state p-success" style="margin-top: -3px; white-space: normal;">
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
        &nbsp;
    <asp:GridView ID="gvw9" runat="server" BorderColor="White" Width="100%"
        AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="OTHERS">
                <ItemTemplate>
                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <div class="state p-success" style="margin-top: -3px; white-space: normal;">
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