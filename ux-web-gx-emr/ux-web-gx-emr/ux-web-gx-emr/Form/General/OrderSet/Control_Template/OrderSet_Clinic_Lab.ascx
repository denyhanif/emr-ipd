<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderSet_Clinic_Lab.ascx.cs" Inherits="Form_General_OrderSet_Control_Template_OrderSet_Clinic_Lab" %>

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
<script type="text/javascript">
    function funCalled(obj) {
        if (obj.checked) {
            alert('check box checked')
        } else {
            alert('check box not checked')
        }
    }
</script>
<asp:GridView ID="grdCheckList" runat="server" BorderColor="White" Width="100%" Visible="false">
</asp:GridView>

<div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; font-size: 11px;">
    <div class="col-sm-6">
        <label>&nbsp; </label>
        <br />
        <label style="display: <%=setENG%>;">Please tick (&#10003) Lab test as request </label>
        <label style="display: <%=setIND%>;">Centang (&#10003) Pemeriksaan Lab yang akan diorder </label>
    </div>
    <div class="col-sm-6 text-right">
        <label style="display: <%=setENG%>;">&#42Fasting 10-12 hours</label>
        <label style="display: <%=setIND%>;">&#42Puasa 10-12 jam</label>
        <label style="margin-left: 25px; margin-right: -4px; display:none;">1 06:00 - 10:00</label>
        <br />
        <label style="display: <%=setENG%>;">&#42&#42Fasting 12-14 hours</label>
        <label style="display: <%=setIND%>;">&#42&#42Puasa 12-14 jam</label>
        <label style="margin-left: 25px; display:none;">2 16:00 - 20:00</label>
    </div>
</div>

<%-- ============================================= HEMATOLOGY ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
    <div class="col-sm-12" style="margin-top: 2%; background-color: #e7e8ef;">
        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">HEMATOLOGY</asp:Label>
    </div>
</div>

<%-- ============================================= HEMATOLOGY ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
    <div class="col-sm-4" style="padding-top: 2%">

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw1" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="HEMATOLOGY - GENERAL">
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
    <div class="col-sm-4" style="padding-top: 2%">

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw2" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="HEMATOLOGY - COAGULATION">
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
    <div class="col-sm-4" style="padding-top: 2%">

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw3" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="HEMATOLOGY - COAGULATION">
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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

<div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
    <div class="col-sm-4" style="padding-top: 2%">
        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw4" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="HEMATOLOGY - SPECIFIC">
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
    <div class="col-sm-4" style="padding-top: 2%">

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw5" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="HEMATOLOGY - SPECIFIC">
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
    <div class="col-sm-4" style="padding-top: 2%">

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw6" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="HEMATOLOGY - SPECIFIC">
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
        <div>&nbsp</div>

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw7" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="PLATELETE FUNCTION TEST">
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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

<div style="background-color: white">&nbsp</div>
<%-- ============================================= BLOOD CHEMISTRY ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
    <div class="col-sm-12" style="margin-top: 2%; background-color: #e7e8ef;">
        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">BLOOD CHEMISTRY</asp:Label>
    </div>
</div>

<%-- ============================================= BLOOD CHEMISTRY ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
    <div class="col-sm-4" style="padding-top: 2%">
        <asp:GridView ID="gvw8" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="HEART FUNCTION TEST">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
        <div>&nbsp</div>
        <asp:GridView ID="gvw11" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="LIVER FUNCTION TEST">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
        <div>&nbsp</div>
        <asp:GridView ID="gvw14" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="RENAL FUNCTION TEST">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
    <div class="col-sm-4" style="padding-top: 2%">
        <asp:GridView ID="gvw9" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="GLUCOSE">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            <div class="state p-success" style="margin-top: -3px; white-space: normal;">
                                <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                    <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %>  </label>
                                    <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
            </Columns>
        </asp:GridView>
        <div>&nbsp</div>
        <asp:GridView ID="gvw12" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="PANCREAS FUNCTION TEST">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
        <div>&nbsp</div>
        <asp:GridView ID="gvw15" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="ELECTROLITE & BLOOD GAS">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
        <div>&nbsp</div>
        <asp:GridView ID="gvw16" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="OTHERS">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
    <div class="col-sm-4" style="padding-top: 2%">
        <asp:GridView ID="gvw10" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="LIPID TEST">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            <div class="state p-success" style="margin-top: -3px; white-space: normal;">
                                <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 40 ? "-7px" : "-2px" %>;">
                                    <label class="label-tick" style="text-indent: 0;"><%# Eval("item_name").ToString() %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </label>
                                    <asp:HiddenField ID="Hidden_chk1" runat="server" Value='<%# Eval("item_name").ToString() %>' />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="item_id" DataField="item_id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
            </Columns>
        </asp:GridView>
        <div>&nbsp</div>
        <asp:GridView ID="gvw13" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="DRUG MONITORING">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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

<div style="background-color: white">&nbsp</div>
<%-- ============================================= IMMUNOLOGY ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
    <div class="col-sm-12" style="margin-top: 2%; background-color: #e7e8ef;">
        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">IMMUNOLOGY</asp:Label>
    </div>
</div>
<%-- ============================================= IMMUNOLOGY ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
    <div class="col-sm-4">
        <asp:GridView ID="gvw17" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
        <asp:GridView ID="gvw18" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
        <asp:GridView ID="gvw19" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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

<div style="background-color: white">&nbsp</div>
<%-- ============================================= SEROLOGY ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
    <div class="col-sm-12" style="margin-top: 2%; background-color: #e7e8ef;">
        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">SEROLOGY</asp:Label>
    </div>
</div>
<%-- ============================================= SEROLOGY ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
    <div class="col-sm-4" style="padding-top: 2%;">
        <asp:GridView ID="gvw20" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="TUMOR MARKER">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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

    <div class="col-sm-4" style="padding-top: 2%">
        <asp:GridView ID="gvw21" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="HEPATITIS MARKER">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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

    <div class="col-sm-4" style="padding-top: 2%">
        <asp:GridView ID="gvw22" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="TORCH">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
<div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
    <div class="col-sm-4" style="padding-top: 2%">
        <asp:GridView ID="gvw23" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="SEROLOGY - GENERAL">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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

    <div class="col-sm-4" style="padding-top: 2%">
        <asp:GridView ID="gvw24" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="SEROLOGY - GENERAL">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
    <div class="col-sm-4" style="padding-top: 2%">
        <asp:GridView ID="gvw25" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="SEROLOGY - GENERAL">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
<div style="background-color: white">&nbsp</div>
<%-- ============================================= HORMONE ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
    <div class="col-sm-12" style="margin-top: 2%; background-color: #e7e8ef;">
        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">HORMONE</asp:Label>
    </div>
</div>
<%-- ============================================= HORMONE ============================================================== --%>

<div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
    <div class="col-sm-4" style="padding-top: 2%">
        <asp:GridView ID="gvw26" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="HORMONE REPRODUCTION">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
        <div>&nbsp</div>
        <asp:GridView ID="gvw29" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="OSTEOPOROSIS">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 58 ? "20px" : Eval("item_name").ToString().Length > 30 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            <div class="state p-success" style="margin-top: -3px; white-space: normal;">
                                <i class="icon fa fa-check" style="font-size: 11px;"></i>
                                <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 58 ? "-17px" : Eval("item_name").ToString().Length > 30 ? "-7px" : "0px" %>;">
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
    <div class="col-sm-4" style="padding-top: 2%;">
        <asp:GridView ID="gvw27" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="HORMONE THYROID">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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

    <div class="col-sm-4" style="padding-top: 2%;">
        <asp:GridView ID="gvw28" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="HORMONE OTHERS">
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
    <div>&nbsp</div>
</div>

<div style="background-color: white">&nbsp</div>
<%-- ============================================= URINE ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
    <div class="col-sm-12" style="margin-top: 2%; background-color: #e7e8ef;">
        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">URINE</asp:Label>
    </div>
</div>
<%-- ============================================= URINE ============================================================== --%>

<div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
    <div class="col-sm-4" style="padding-top: 2%;">

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw30" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="GENERAL">
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
    <div class="col-sm-4" style="padding-top: 2%;">

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw31" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="URINE KUANTITATIF (24 HOURS)">
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
    <div class="col-sm-4" style="padding-top: 2%;">

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw32" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="DRUG ABUSE">
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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

        <div>&nbsp</div>

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw33" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="PREGNANCY">
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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

<div style="background-color: white">&nbsp</div>
<%-- ============================================= FAECES ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
    <div class="col-sm-12" style="margin-top: 2%; background-color: #e7e8ef;">
        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">FAECES</asp:Label>
    </div>
</div>
<%-- ============================================= FAECES ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
    <div class="col-sm-4">
        <asp:GridView ID="gvw34" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
        <asp:GridView ID="gvw35" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
        <asp:GridView ID="gvw36" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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

<div style="background-color: white">&nbsp</div>

<%-- ============================================= BODY FLUID ANALYSIS ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
    <div class="col-sm-12" style="margin-top: 2%; background-color: #e7e8ef;">
        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">BODY FLUID ANALYSIS</asp:Label>
    </div>
</div>
<%-- ============================================= BODY FLUID ANALYSIS ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
    <div class="col-sm-4">

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw37" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
    <div class="col-sm-4">

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw38" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
    <div class="col-sm-4" style="padding-top: 0px">

        <div class="checkbox-grup-width">
            <asp:GridView ID="gvw39" runat="server" BorderColor="White" Width="100%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                                <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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


<div style="background-color: white">&nbsp</div>
<%-- ============================================= MOLECULAR DIAGNOSTIC ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
    <div class="col-sm-12" style="margin-top: 2%; background-color: #e7e8ef;">
        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">MOLECULAR DIAGNOSTIC</asp:Label>
    </div>
</div>
<%-- ============================================= MOLECULAR DIAGNOSTIC ============================================================== --%>
<div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
    <div class="col-sm-4">
        <asp:GridView ID="gvw40" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
        <asp:GridView ID="gvw41" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
        <asp:GridView ID="gvw42" runat="server" BorderColor="White" Width="100%"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 40 ? "11px" : "6px" %>;">
                            <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>' Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
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