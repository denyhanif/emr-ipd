<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderSet_AnatomiLab.ascx.cs" Inherits="Form_CPOE_Control_Template_AnatomiLab" %>

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

<script type="text/javascript">

    function checkenter() {
        return event.keyCode != 13;
    }

    //function serviceselectedanatomi(val, type, remarks,GridId) {

    //    var row = val.parentNode.parentNode.parentNode;
    //    var Grid = 'MainContent_StdPlanning_TabContainer1_Anatomi_stdanatomi_' + GridId;
    //    var tbl = document.getElementById(Grid);
    //    var tbl_row = tbl.rows[parseInt(row.rowIndex)];            
    //    var tbl_Cell = tbl_row.cells[1];         
    //    var tbl_Cel2 = tbl_row.cells[2];   
    //    var value = tbl_Cell.innerHTML.toString();
    //    var value2 = tbl_Cel2.innerHTML.toString();
    //    //alert(value + ',' + value2);
    //    if (val.checked == true) {

    //        var checkexist = 0;
    //        //var a = '{"id":' + value
    //            //    + ',"name":"",' + '"type":"' + type
    //            //    + '","remarks":"' + remarks + '","isnew":1,"iscito":0,"issubmit":0,"isdelete":0,"ischeck":1}';

    //        if (list.length > 0) {
    //            var index;
    //            for (index = 0; index < list.length; ++index) {
    //                if (list[index].id == value && list[index].isnew != 1) {
    //                    list[index].isdelete = 0;
    //                    checkexist = 1;
    //                }
    //            }
    //            if (checkexist == 0) {
    //                list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1 });
    //            }
    //        }
    //        else {
    //            list.push({ id: value, name: value2, type: type, remarks: remarks, isnew: 1, iscito: 0, issubmit: 0, isdelete: 0, ischeck: 1 });
    //        }
    //    }
    //    else {
    //        if (list.length > 0) {
    //            var index;
    //            for (index = 0; index < list.length; ++index) {
    //                if (list[index].id == value && list[index].isnew == 1) {
    //                    list.splice(index, 1);
    //                }
    //                else if (list[index].id == value && list[index].isnew == 0 && list[index].ischeck == 1) {
    //                    list[index].isdelete = 1;
    //                }
    //            }
    //        }
    //    }
    //    //alert(list[0].name);
    //}

    function switchBahasa_anatomi() {
        var bahasa = document.getElementById('<%=HFisBahasa_anatomi.ClientID%>').value;
        if (bahasa == "ENG") {
            document.getElementById('lblbhs_pleasetick1_anatomilab').innerHTML = "Please tick (&#10003) Lab test as request";
            document.getElementById('lblbhs_patologianatomi').innerHTML = "Anatomical Pathology";
        }
        else if (bahasa == "IND") {
            document.getElementById('lblbhs_pleasetick1_anatomilab').innerHTML = "Centang (&#10003) Pemeriksaan Lab yang akan diorder";
            document.getElementById('lblbhs_patologianatomi').innerHTML = "Patologi Anatomi";
        }
    }
</script>


<div>
    <asp:HiddenField ID="HFisBahasa_anatomi" runat="server" />
    <asp:UpdateProgress ID="uProgAnatomiLab" runat="server" AssociatedUpdatePanelID="upanatomilab">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
            </div>
            <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upanatomilab">
        <ContentTemplate>

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 10px; display: none;">
                <div class="col-sm-4">
                    <asp:Label Font-Bold="true" Font-Size="12px" runat="server">
            <label id="lblbhs_patologianatomi">Patologi Anatomi</label>
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
                    <%--<label style="display: <%=setENG%>;">Please tick (&#10003) Lab test as request </label>
                    <label style="display: <%=setIND%>;">Centang (&#10003) Pemeriksaan Lab yang akan diorder </label>--%>
                    <label id="lblbhs_pleasetick1_anatomilab">Please tick (&#10003) Lab test as request </label>
                </div>
                <div class="col-sm-6 text-right">
                    &nbsp;
                </div>
            </div>

            <%-- =========================================================CONTENT====================================================================== --%>
            <asp:GridView ID="grdCheckList" runat="server" BorderColor="White" Width="100%" Visible="false">
            </asp:GridView>

            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-6" style="padding-left: 0px;">
                    <div style="background-color: #e7e8ef; padding-right: 5px;">
                        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">HISTOPATOLOGI</asp:Label>
                    </div>
                </div>
                <div class="col-sm-6" style="padding-right: 0px;">
                    <div style="background-color: #e7e8ef; padding-left: 5px;">
                        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">SITOLOGI</asp:Label>
                    </div>
                </div>
            </div>

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-6">

                    <asp:GridView ID="gvw1" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 75 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="chk1_CheckedChanged" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon mdi fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 75 ? "-7px" : "0px" %>;">
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
                <div class="col-sm-6">
                    <asp:GridView ID="gvw2" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 75 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="chk1_CheckedChanged" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon mdi fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 75 ? "-7px" : "0px" %>;">
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

            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-6" style="padding-left: 0px;">
                    <div style="background-color: #e7e8ef; padding-right: 5px;">
                        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">IMUNOHISTOKIMIA (IHK)</asp:Label>
                    </div>
                </div>
                <div class="col-sm-6" style="padding-right: 0px;">
                    <div style="background-color: #e7e8ef; padding-left: 5px;">
                        <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">POTONG BEKU / VC</asp:Label>
                    </div>
                </div>
            </div>

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-6">

                    <asp:GridView ID="gvw3" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 75 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="chk1_CheckedChanged" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon mdi fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 75 ? "-7px" : "0px" %>;">
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
                <div class="col-sm-6">
                    <asp:GridView ID="gvw4" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 75 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="chk1_CheckedChanged" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon mdi fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 75 ? "-7px" : "0px" %>;">
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

            <%-- ============================================= LAIN-LAIN ============================================================== --%>
            <div class="row" style="margin: 0px; padding-bottom: 0px; padding-top: 0px; text-align: center">
                <div class="col-sm-12" style="background-color: #e7e8ef;">
                    <asp:Label Style="font-family: Helvetica, Arial, sans-serif;" Font-Size="12px" Font-Bold="true" runat="server">LAIN-LAIN</asp:Label>
                </div>
            </div>

            <div class="row" style="margin: 0px; padding-bottom: 0px; background-color: white; padding-top: 0px">
                <div class="col-sm-6">

                    <asp:GridView ID="gvw5" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 75 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="chk1_CheckedChanged" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon mdi fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 75 ? "-7px" : "0px" %>;">
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
                <div class="col-sm-6">
                    <asp:GridView ID="gvw6" runat="server" BorderColor="White" Width="100%"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="pretty p-icon p-curve" style="margin-top: <%# Eval("item_name").ToString().Length > 75 ? "11px" : "6px" %>;">
                                        <asp:CheckBox runat="server" Enabled='<%# Eval("issubmit").ToString() != "1" %>'
                                            Value="10" ID="chk1" Checked='<%# Eval("is_checked").ToString() != "0" %>' AutoPostBack="true" OnCheckedChanged="chk1_CheckedChanged" />
                                        <div class="state p-success" style="margin-top: -2px; white-space: normal;">
                                            <i class="icon mdi fa fa-check" style="font-size: 11px;"></i>
                                            <div style="margin-left: 22px; margin-top: <%# Eval("item_name").ToString().Length > 75 ? "-7px" : "0px" %>;">
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