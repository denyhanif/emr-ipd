<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdKurvaPertumbuhan.ascx.cs" Inherits="Form_SOAP_Control_Template_StdKurvaPertumbuhan" %>

<asp:HiddenField ID="hfguidadditional" runat="server" />

<style>
    .inputGroupR {
        position: relative;
    }

        .inputGroupR input[type="text"] {
            
        }

        .inputGroupR span {
            position: absolute;
            right: 10px;
            top: 3px;
        }

    .force-close {
    opacity:0;
    transition:all ease-in-out 2s;
    }

    .force-close:hover {
    opacity:1;
    transition:all ease-in-out 2s;
    }
</style>

<div class="container-fluid">
    <%--<asp:UpdatePanel ID="UpdatePanelControlKurva" runat="server" UpdateMode="Always">
        <ContentTemplate>--%>
    <div class="row" style="transform: translate(0,0);">
        <div class="col-sm-12 modal-header" style="padding: 7px 25px 7px 15px; min-height: 35px;">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%; text-align: left;">
                        <label style="font-family: Helvetica; font-weight: bold; font-size: 14px;">Kurva Pertumbuhan </label>
                        <a href="javascript:HideKurva();" class="btn btn-sm btn-default force-close">CLOSE</a>
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <asp:UpdatePanel ID="UpdatePanelSaveKurva" runat="server">
                            <ContentTemplate>
                                <div style="display:inline-block;">
                                     <asp:UpdateProgress ID="UpdateProgressSaveKurva" runat="server" AssociatedUpdatePanelID="UpdatePanelSaveKurva">
                                        <ProgressTemplate>
                                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                            </div>
                                            &nbsp;
                                            <img alt="" style="background-color: transparent; height: 20px; margin-right:10px; margin-bottom:3px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <asp:Label ID="LabelChartSaveFail" runat="server" Visible="false" Text="" Style="font-family: Helvetica; font-weight: bold; font-size: 14px; color:red; margin-right:20px;"> <i class="fa fa-warning"></i> Save Failed </asp:Label>
                                <%--<button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>--%>
                                <asp:Button ID="ButtonCancelKurva" class="btn btn-default" runat="server" Text="Cancel" OnClick="ButtonCancelKurva_Click" />
                                <asp:Button ID="ButtonSaveKurva" class="btn btn-lightGreen" runat="server" Text="Save" OnClick="ButtonSaveKurva_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div class="col-sm-12 modal-header" style="padding: 7px 25px 7px 15px; min-height: 35px; margin-bottom:20px;">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%; text-align: left;">
                        <div class="row">
                            <div class="col-sm-2" style="padding-right: 0px; width: 55px;">
                                <asp:Image runat="server" ID="imgSex" Width="34px" />
                            </div>
                            <div class="col-sm-10" style="padding-left: 0px;">
                                <asp:Label ID="LabelNamaPasienChart" runat="server" Text="-" Style="font-family: Helvetica; font-weight: bold; font-size: 14px;"></asp:Label>
                                <br />
                                <asp:Label ID="LabelAgePasienChart" runat="server" Text="-" Style="font-family: Helvetica; color: #76767c; font-size: 12px;"></asp:Label>
                                <asp:HiddenField ID="HFAgePasienDays" runat="server" />
                                <asp:HiddenField ID="HFAgePasienMonths" runat="server" />
                                <asp:HiddenField ID="HFAgePasienYears" runat="server" />
                                <asp:HiddenField ID="HFGenderPasien" runat="server" />

                                <asp:HiddenField ID="HF_PtnID" runat="server" />
                                <asp:HiddenField ID="HF_EncID" runat="server" />
                                <asp:HiddenField ID="HF_AdmID" runat="server" />
                                <asp:HiddenField ID="HF_DocID" runat="server" />
                            </div>
                        </div>
                    </td>
                    <td style="width: 50%; text-align: right;">

                        <asp:UpdatePanel ID="UpdatePanelChartTemplate" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <div style="display:inline-block;">
                                     <asp:UpdateProgress ID="UpdateProgressStandardValue" runat="server" AssociatedUpdatePanelID="UpdatePanelChartTemplate">
                                        <ProgressTemplate>
                                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                            </div>
                                            &nbsp;
                                            <img alt="" style="background-color: transparent; height: 20px; margin-right:10px; margin-bottom:3px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>

                                <asp:CheckBox ID="CheckBoxStandardValue" runat="server" Text="  Show Standard Value" OnCheckedChanged="CheckBoxStandardValue_CheckedChanged" AutoPostBack="true" style="display:none;" />
                                <asp:HiddenField ID="HF_FlagChartTemplate" runat="server" />

                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanelChartTemplatePBU" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%--PBU--%>
                                <asp:HiddenField ID="HF_PBU0" runat="server" />
                                <%--<asp:HiddenField ID="HF_PBUplus1" runat="server" />
                                    <asp:HiddenField ID="HF_PBUminus1" runat="server" />--%>
                                <asp:HiddenField ID="HF_PBUplus2" runat="server" />
                                <asp:HiddenField ID="HF_PBUminus2" runat="server" />
                                <asp:HiddenField ID="HF_PBUplus3" runat="server" />
                                <asp:HiddenField ID="HF_PBUminus3" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanelChartTemplateBBU" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%--BBU--%>
                                <asp:HiddenField ID="HF_BBU0" runat="server" />
                                <%--<asp:HiddenField ID="HF_BBUplus1" runat="server" />
                                    <asp:HiddenField ID="HF_BBUminus1" runat="server" />--%>
                                <asp:HiddenField ID="HF_BBUplus2" runat="server" />
                                <asp:HiddenField ID="HF_BBUminus2" runat="server" />
                                <asp:HiddenField ID="HF_BBUplus3" runat="server" />
                                <asp:HiddenField ID="HF_BBUminus3" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanelChartTemplateBBPB" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%--BBPB--%>
                                <asp:HiddenField ID="HF_BBPB0" runat="server" />
                                <asp:HiddenField ID="HF_BBPBplus1" runat="server" />
                                <asp:HiddenField ID="HF_BBPBminus1" runat="server" />
                                <asp:HiddenField ID="HF_BBPBplus2" runat="server" />
                                <asp:HiddenField ID="HF_BBPBminus2" runat="server" />
                                <asp:HiddenField ID="HF_BBPBplus3" runat="server" />
                                <asp:HiddenField ID="HF_BBPBminus3" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanelChartTemplateLKU" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%--LKU--%>
                                <asp:HiddenField ID="HF_LKU0" runat="server" />
                                <asp:HiddenField ID="HF_LKUplus1" runat="server" />
                                <asp:HiddenField ID="HF_LKUminus1" runat="server" />
                                <asp:HiddenField ID="HF_LKUplus2" runat="server" />
                                <asp:HiddenField ID="HF_LKUminus2" runat="server" />
                                <asp:HiddenField ID="HF_LKUplus3" runat="server" />
                                <asp:HiddenField ID="HF_LKUminus3" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanelChartTemplateBMI" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%--BMI--%>
                                <asp:HiddenField ID="HF_BMI0" runat="server" />
                                <asp:HiddenField ID="HF_BMIplus1" runat="server" />
                                <asp:HiddenField ID="HF_BMIminus1" runat="server" />
                                <asp:HiddenField ID="HF_BMIplus2" runat="server" />
                                <asp:HiddenField ID="HF_BMIminus2" runat="server" />
                                <asp:HiddenField ID="HF_BMIplus3" runat="server" />
                                <asp:HiddenField ID="HF_BMIminus3" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>  

                        <asp:UpdatePanel ID="UpdatePanelChartTemplateLLU" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%--LLU--%>
                                <asp:HiddenField ID="HF_LLU0" runat="server" />
                                <asp:HiddenField ID="HF_LLUplus1" runat="server" />
                                <asp:HiddenField ID="HF_LLUminus1" runat="server" />
                                <asp:HiddenField ID="HF_LLUplus2" runat="server" />
                                <asp:HiddenField ID="HF_LLUminus2" runat="server" />
                                <asp:HiddenField ID="HF_LLUplus3" runat="server" />
                                <asp:HiddenField ID="HF_LLUminus3" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanelPBU" runat="server">
            <ContentTemplate>
                <div class="col-sm-9" style="width: 70%;">

                    <div style="padding-bottom: 15px;">
                        <asp:Label ID="LabelHeaderPBU" runat="server" Text="PB/U (Panjang Badan Menurut Umur)"></asp:Label>
                        <br />
                        <asp:DropDownList ID="DDLKurvaPBU" runat="server" Style="width: 165px;" AutoPostBack="true" OnSelectedIndexChanged="DDLKurvaPBU_SelectedIndexChanged">
                            <asp:ListItem Value="0"> Birth to 6 Months </asp:ListItem>
                            <asp:ListItem Value="1"> Birth to 2 Years </asp:ListItem>
                            <asp:ListItem Value="2"> Birth to 5 Years </asp:ListItem>
                            <asp:ListItem Value="3"> 5 Years to 19 Years </asp:ListItem>
                        </asp:DropDownList>
                        (z-scores)

                        &nbsp;
                        &nbsp;

                        <asp:CheckBox ID="CheckBoxStandardValuePBU" runat="server" Text="  Show Standard Value" OnCheckedChanged="CheckBoxStandardValuePBU_CheckedChanged" AutoPostBack="true" />
                    </div>
                    <div style="width: 95%; height: 500px;">
                        <canvas id="canvasPBU"></canvas>
                    </div>
                    <hr />
                </div>
                <div class="col-sm-3" style="width: 30%; padding-top: 95px;">

                    <asp:HiddenField ID="HF_PBU_Data" runat="server" />
                    <asp:HiddenField ID="HF_PBU_MinMaxAge" runat="server" />
                    <asp:HiddenField ID="HF_PBU_MinMaxY" runat="server" />

                    List Data PB/U
                    <br />
                    <div class="chart-listdata-box scrollEMR">
                    <table class="table-condensed table-divider" style="width:100%;">
                        <asp:Repeater ID="RepeaterPBU" runat="server">
                            <HeaderTemplate>
                                <tr>
                                    <th>No
                                    </th>
                                    <%--<th>X
                                    </th>--%>
                                    <th>Length
                                    </th>
                                    <th>Age
                                    </th>
                                    <th></th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Container.ItemIndex + 1 %>
                                        <asp:HiddenField ID="HFPBUMasterId" runat="server" Value='<%#Eval("chart_transaction_master_id") %>' />
                                    </td>
                                   <%-- <td>
                                        <asp:Label ID="LabelPBUx" runat="server" Text='<%#Eval("x") %>'></asp:Label>
                                    </td>--%>
                                    <td>
                                        <asp:Label ID="LabelPBUy" runat="server" Text='<%#Eval("y") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelPBUage" runat="server" Text='<%#Eval("age") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="ButtonEditPBU" runat="server" Text="E" OnClick="ButtonEditPBU_Click" />
                                        <asp:Button ID="ButtonDeletePBU" runat="server" Text="D" OnClick="ButtonDeletePBU_Click" />--%>
                                        <asp:ImageButton ID="ButtonEditPBU" runat="server" OnClick="ButtonEditPBU_Click" ImageUrl="~/Images/Icon/ic_edit.svg" style="width:16px; height:16px; margin-right:5px;" ToolTip="Edit" />
                                        <asp:ImageButton ID="ButtonDeletePBU" runat="server" OnClick="ButtonDeletePBU_Click" ImageUrl="~/Images/Icon/ic_delete.svg" style="width:14px; height:14px;" ToolTip="Delete" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    </div>
                    <br />
                    <asp:Button ID="ButtonAddPBU" runat="server" Text="Add PBU" CssClass="btn btn-github btn-emr-small" Style="margin-top: 10px; margin-bottom: 0px;" OnClick="ButtonAddPBU_Click" />
                    <div style="display:inline-block;">
                         <asp:UpdateProgress ID="UpdateProgressPBU" runat="server" AssociatedUpdatePanelID="UpdatePanelPBU">
                            <ProgressTemplate>
                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                </div>
                                &nbsp;
                                <img alt="" style="background-color: transparent; height: 20px; margin-top:7px; margin-left:7px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <br />
                    <div id="divPBUData" runat="server" visible="false" class="chart-listdata-box" style="background-color:whitesmoke;">
                        <table class="table-condensed">
                            <tr>
                                <th colspan="3">Input Length
                                </th>
                            </tr>
                            <tr>
                                <td>Length
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <%--<div class="inputGroupR">--%>
                                    <asp:TextBox ID="TextBoxPBULength" runat="server" onkeypress="return ValidateNumberWithComma();"></asp:TextBox>
                                    <span style="margin-left:-25px;">cm</span>     
                                    <%--</div>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>Age
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtAgePBUYear" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();"></asp:TextBox>
                                    Y
                                    <asp:TextBox ID="TxtAgePBUMonth" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();" onkeyup="ValidateMonth(this);"></asp:TextBox>
                                    M
                                    <asp:TextBox ID="TxtAgePBUDay" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();" onkeyup="ValidateDay(this);"></asp:TextBox>
                                    D
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: right;">
                                    <asp:HiddenField ID="HFTempPBUMasterId" runat="server" />
                                    <asp:Button ID="ButtonCancelPBU" runat="server" Text="Cancel" OnClick="ButtonCancelPBU_Click" />
                                    <asp:Button ID="ButtonSavePBULength" runat="server" Text="Add" OnClick="ButtonSavePBULength_Click" />
                                    <asp:Button ID="ButtonUpdatePBULength" runat="server" Text="Update" OnClick="ButtonUpdatePBULength_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanelBBU" runat="server">
            <ContentTemplate>
                <div class="col-sm-9" style="width: 70%;">

                    <div style="padding-bottom: 15px;">
                        <asp:Label ID="LabelHeaderBBU" runat="server" Text="BB/U (Berat Badan Menurut Umur)"></asp:Label>
                        <br />
                        <asp:DropDownList ID="DDLKurvaBBU" runat="server" Style="width: 165px;" AutoPostBack="true" OnSelectedIndexChanged="DDLKurvaBBU_SelectedIndexChanged">
                            <asp:ListItem Value="0"> Birth to 6 Months </asp:ListItem>
                            <asp:ListItem Value="1"> Birth to 2 Years </asp:ListItem>
                            <asp:ListItem Value="2"> Birth to 5 Years </asp:ListItem>
                            <asp:ListItem Value="3"> 5 Years to 10 Years </asp:ListItem>
                        </asp:DropDownList>
                        (z-scores)

                        &nbsp;
                        &nbsp;

                        <asp:CheckBox ID="CheckBoxStandardValueBBU" runat="server" Text="  Show Standard Value" OnCheckedChanged="CheckBoxStandardValueBBU_CheckedChanged" AutoPostBack="true" />
                    </div>
                    <div style="width: 95%; height: 500px;">
                        <canvas id="canvasBBU"></canvas>
                    </div>
                    <hr />
                </div>
                <div class="col-sm-3" style="width: 30%; padding-top: 95px;">

                    <asp:HiddenField ID="HF_BBU_Data" runat="server" />
                    <asp:HiddenField ID="HF_BBU_MinMaxAge" runat="server" />
                    <asp:HiddenField ID="HF_BBU_MinMaxY" runat="server" />

                    List Data BB/U
                    <br />
                    <div class="chart-listdata-box scrollEMR">
                    <table class="table-condensed table-divider" style="width:100%;">
                        <asp:Repeater ID="RepeaterBBU" runat="server">
                            <HeaderTemplate>
                                <tr>
                                    <th>No
                                    </th>
                                    <%--<th>X
                                    </th>--%>
                                    <th>Weight
                                    </th>
                                    <th>Age
                                    </th>
                                    <th></th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Container.ItemIndex + 1 %>
                                        <asp:HiddenField ID="HFBBUMasterId" runat="server" Value='<%#Eval("chart_transaction_master_id") %>' />
                                    </td>
                                    <%--<td>
                                        <asp:Label ID="LabelBBUx" runat="server" Text='<%#Eval("x") %>'></asp:Label>
                                    </td>--%>
                                    <td>
                                        <asp:Label ID="LabelBBUy" runat="server" Text='<%#Eval("y") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelBBUage" runat="server" Text='<%#Eval("age") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="ButtonEditBBU" runat="server" Text="E" OnClick="ButtonEditBBU_Click" />
                                        <asp:Button ID="ButtonDeleteBBU" runat="server" Text="D" OnClick="ButtonDeleteBBU_Click" />--%>
                                        <asp:ImageButton ID="ButtonEditBBU" runat="server" OnClick="ButtonEditBBU_Click" ImageUrl="~/Images/Icon/ic_edit.svg" style="width:16px; height:16px; margin-right:5px;" ToolTip="Edit" />
                                        <asp:ImageButton ID="ButtonDeleteBBU" runat="server" OnClick="ButtonDeleteBBU_Click" ImageUrl="~/Images/Icon/ic_delete.svg" style="width:14px; height:14px;" ToolTip="Delete" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    </div>
                    <br />
                    <asp:Button ID="ButtonAddBBU" runat="server" Text="Add BBU" CssClass="btn btn-github btn-emr-small" Style="margin-top: 10px; margin-bottom: 0px;" OnClick="ButtonAddBBU_Click" />
                    <div style="display:inline-block;">
                         <asp:UpdateProgress ID="UpdateProgressBBU" runat="server" AssociatedUpdatePanelID="UpdatePanelBBU">
                            <ProgressTemplate>
                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                </div>
                                &nbsp;
                                <img alt="" style="background-color: transparent; height: 20px; margin-top:7px; margin-left:7px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <br />
                    <div id="divBBUData" runat="server" visible="false" class="chart-listdata-box" style="background-color:whitesmoke;">
                        <table class="table-condensed">
                            <tr>
                                <th colspan="3">Input Weight
                                </th>
                            </tr>
                            <tr>
                                <td>Weight
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxBBUWeight" runat="server" onkeypress="return ValidateNumberWithComma();"></asp:TextBox>
                                    <span style="margin-left:-25px;">kg</span>  
                                </td>
                            </tr>
                            <tr>
                                <td>Age
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtAgeBBUYear" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();"></asp:TextBox>
                                    Y
                                    <asp:TextBox ID="TxtAgeBBUMonth" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();" onkeyup="ValidateMonth(this);"></asp:TextBox>
                                    M
                                    <asp:TextBox ID="TxtAgeBBUDay" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();" onkeyup="ValidateDay(this);"></asp:TextBox>
                                    D
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: right;">
                                    <asp:HiddenField ID="HFTempBBUMasterId" runat="server" />
                                    <asp:Button ID="ButtonCancelBBU" runat="server" Text="Cancel" OnClick="ButtonCancelBBU_Click" />
                                    <asp:Button ID="ButtonSaveBBUWeight" runat="server" Text="Add" OnClick="ButtonSaveBBUWeight_Click" />
                                    <asp:Button ID="ButtonUpdateBBUWeight" runat="server" Text="Update" OnClick="ButtonUpdateBBUWeight_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanelBBPB" runat="server">
            <ContentTemplate>
                <div class="col-sm-9" style="width: 70%;">

                    <div style="padding-bottom: 15px;">
                        <asp:Label ID="LabelHeaderBBPB" runat="server" Text="BB/PB (Berat Badan Menurut Panjang Badan)"></asp:Label>
                        <br />
                        <asp:DropDownList ID="DDLKurvaBBPB" runat="server" Style="width: 165px;" AutoPostBack="true" OnSelectedIndexChanged="DDLKurvaBBPB_SelectedIndexChanged">
                            <%--<asp:ListItem Value="0"> Birth to 6 Months </asp:ListItem>--%>
                            <asp:ListItem Value="1"> Birth to 2 Years </asp:ListItem>
                            <asp:ListItem Value="2"> Birth to 5 Years </asp:ListItem>
                        </asp:DropDownList>
                        (z-scores)

                        &nbsp;
                        &nbsp;

                        <asp:CheckBox ID="CheckBoxStandardValueBBPB" runat="server" Text="  Show Standard Value" OnCheckedChanged="CheckBoxStandardValueBBPB_CheckedChanged" AutoPostBack="true" />
                    </div>
                    <div style="width: 95%; height: 500px;">
                        <canvas id="canvasBBPB"></canvas>
                    </div>
                    <hr />
                </div>
                <div class="col-sm-3" style="width: 30%; padding-top: 95px;">

                    <asp:HiddenField ID="HF_BBPB_Data" runat="server" />
                    <asp:HiddenField ID="HF_BBPB_MinMaxAge" runat="server" />
                    <asp:HiddenField ID="HF_BBPB_MinMaxX" runat="server" />
                    <asp:HiddenField ID="HF_BBPB_MinMaxY" runat="server" />

                    List Data BB/PB
                    <br />
                    <div class="chart-listdata-box scrollEMR">
                    <table class="table-condensed table-divider" style="width:100%;">
                        <asp:Repeater ID="RepeaterBBPB" runat="server">
                            <HeaderTemplate>
                                <tr>
                                    <th>No
                                    </th>
                                    <th>Length
                                    </th>
                                    <th>Weight
                                    </th>
                                    <th>Age
                                    </th>
                                    <th></th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Container.ItemIndex + 1 %>
                                        <asp:HiddenField ID="HFBBPBMasterId" runat="server" Value='<%#Eval("chart_transaction_master_id") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelBBPBx" runat="server" Text='<%#Eval("x") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelBBPBy" runat="server" Text='<%#Eval("y") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelBBPBage" runat="server" Text='<%#Eval("age") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="ButtonEditBBPB" runat="server" Text="E" OnClick="ButtonEditBBPB_Click" />
                                        <asp:Button ID="ButtonDeleteBBPB" runat="server" Text="D" OnClick="ButtonDeleteBBPB_Click" />--%>
                                        <asp:ImageButton ID="ButtonEditBBPB" runat="server" OnClick="ButtonEditBBPB_Click" ImageUrl="~/Images/Icon/ic_edit.svg" style="width:16px; height:16px; margin-right:5px;" ToolTip="Edit" />
                                        <asp:ImageButton ID="ButtonDeleteBBPB" runat="server" OnClick="ButtonDeleteBBPB_Click" ImageUrl="~/Images/Icon/ic_delete.svg" style="width:14px; height:14px;" ToolTip="Delete" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    </div>
                    <br />
                    <asp:Button ID="ButtonAddBBPB" runat="server" Text="Add BBPB" CssClass="btn btn-github btn-emr-small" Style="margin-top: 10px; margin-bottom: 0px;" OnClick="ButtonAddBBPB_Click" />
                    <div style="display:inline-block;">
                         <asp:UpdateProgress ID="UpdateProgressBBPB" runat="server" AssociatedUpdatePanelID="UpdatePanelBBPB">
                            <ProgressTemplate>
                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                </div>
                                &nbsp;
                                <img alt="" style="background-color: transparent; height: 20px; margin-top:7px; margin-left:7px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <br />
                    <div id="divBBPBData" runat="server" visible="false" class="chart-listdata-box" style="background-color:whitesmoke;">
                        <table class="table-condensed">
                            <tr>
                                <th colspan="3">Input Weight & Length
                                </th>
                            </tr>
                            <tr>
                                <td>Weight
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxBBPBWeight" runat="server" onkeypress="return ValidateNumberWithComma();"></asp:TextBox>
                                    <span style="margin-left:-25px;">kg</span>  
                                </td>
                            </tr>
                            <tr>
                                <td>Length
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxBBPBLength" runat="server" onkeypress="return ValidateNumberWithComma();"></asp:TextBox>
                                    <span style="margin-left:-25px;">cm</span>  
                                </td>
                            </tr>
                            <tr>
                                <td>Age
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtAgeBBPBYear" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();"></asp:TextBox>
                                    Y
                                    <asp:TextBox ID="TxtAgeBBPBMonth" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();" onkeyup="ValidateMonth(this);"></asp:TextBox>
                                    M
                                    <asp:TextBox ID="TxtAgeBBPBDay" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();" onkeyup="ValidateDay(this);"></asp:TextBox>
                                    D
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: right;">
                                    <asp:HiddenField ID="HFTempBBPBMasterId" runat="server" />
                                    <asp:Button ID="ButtonCancelBBPB" runat="server" Text="Cancel" OnClick="ButtonCancelBBPB_Click" />
                                    <asp:Button ID="ButtonSaveBBPBWeight" runat="server" Text="Add" OnClick="ButtonSaveBBPBWeight_Click" />
                                    <asp:Button ID="ButtonUpdateBBPBWeight" runat="server" Text="Update" OnClick="ButtonUpdateBBPBWeight_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanelLKU" runat="server">
            <ContentTemplate>
                <div class="col-sm-9" style="width: 70%;">

                    <div style="padding-bottom: 15px;">
                        <asp:Label ID="LabelHeaderLKU" runat="server" Text="LK/U (Lingkar Kepala Menurut Umur)"></asp:Label>
                        <br />
                        <asp:DropDownList ID="DDLKurvaLKU" runat="server" Style="width: 165px;" AutoPostBack="true" OnSelectedIndexChanged="DDLKurvaLKU_SelectedIndexChanged">
                            <asp:ListItem Value="0"> Birth to 13 Weeks </asp:ListItem>
                            <asp:ListItem Value="1"> Birth to 2 Years </asp:ListItem>
                            <asp:ListItem Value="2"> Birth to 5 Years </asp:ListItem>
                        </asp:DropDownList>
                        (z-scores)

                        &nbsp;
                        &nbsp;

                        <asp:CheckBox ID="CheckBoxStandardValueLKU" runat="server" Text="  Show Standard Value" OnCheckedChanged="CheckBoxStandardValueLKU_CheckedChanged" AutoPostBack="true" />
                    </div>
                    <div style="width: 95%; height: 500px;">
                        <canvas id="canvasLKU"></canvas>
                    </div>
                    <hr />
                </div>
                <div class="col-sm-3" style="width: 30%; padding-top: 95px;">

                    <asp:HiddenField ID="HF_LKU_Data" runat="server" />
                    <asp:HiddenField ID="HF_LKU_MinMaxAge" runat="server" />
                    <asp:HiddenField ID="HF_LKU_MinMaxY" runat="server" />

                    List Data LK/U
                    <br />
                    <div class="chart-listdata-box scrollEMR">
                    <table class="table-condensed table-divider" style="width:100%;">
                        <asp:Repeater ID="RepeaterLKU" runat="server">
                            <HeaderTemplate>
                                <tr>
                                    <th>No
                                    </th>
                                    <%--<th>X
                                    </th>--%>
                                    <th>Head Circ.
                                    </th>
                                    <th>Age
                                    </th>
                                    <th></th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Container.ItemIndex + 1 %>
                                        <asp:HiddenField ID="HFLKUMasterId" runat="server" Value='<%#Eval("chart_transaction_master_id") %>' />
                                    </td>
                                    <%--<td>
                                        <asp:Label ID="LabelLKUx" runat="server" Text='<%#Eval("x") %>'></asp:Label>
                                    </td>--%>
                                    <td>
                                        <asp:Label ID="LabelLKUy" runat="server" Text='<%#Eval("y") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelLKUage" runat="server" Text='<%#Eval("age") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="ButtonEditLKU" runat="server" Text="E" OnClick="ButtonEditLKU_Click" />
                                        <asp:Button ID="ButtonDeleteLKU" runat="server" Text="D" OnClick="ButtonDeleteLKU_Click" />--%>
                                        <asp:ImageButton ID="ButtonEditLKU" runat="server" OnClick="ButtonEditLKU_Click" ImageUrl="~/Images/Icon/ic_edit.svg" style="width:16px; height:16px; margin-right:5px;" ToolTip="Edit" />
                                        <asp:ImageButton ID="ButtonDeleteLKU" runat="server" OnClick="ButtonDeleteLKU_Click" ImageUrl="~/Images/Icon/ic_delete.svg" style="width:14px; height:14px;" ToolTip="Delete" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    </div>
                    <br />
                    <asp:Button ID="ButtonAddLKU" runat="server" Text="Add LKU" CssClass="btn btn-github btn-emr-small" Style="margin-top: 10px; margin-bottom: 0px;" OnClick="ButtonAddLKU_Click" />
                    <div style="display:inline-block;">
                         <asp:UpdateProgress ID="UpdateProgressLKU" runat="server" AssociatedUpdatePanelID="UpdatePanelLKU">
                            <ProgressTemplate>
                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                </div>
                                &nbsp;
                                <img alt="" style="background-color: transparent; height: 20px; margin-top:7px; margin-left:7px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <br />
                    <div id="divLKUData" runat="server" visible="false" class="chart-listdata-box" style="background-color:whitesmoke;">
                        <table class="table-condensed">
                            <tr>
                                <th colspan="3">Input Head Circumference
                                </th>
                            </tr>
                            <tr>
                                <td>Head Circ.
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxLKUHeadCirc" runat="server" onkeypress="return ValidateNumberWithComma();"></asp:TextBox>
                                    <span style="margin-left:-25px;">cm</span>  
                                </td>
                            </tr>
                            <tr>
                                <td>Age
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtAgeLKUYear" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();"></asp:TextBox>
                                    Y
                                    <asp:TextBox ID="TxtAgeLKUMonth" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();" onkeyup="ValidateMonth(this);"></asp:TextBox>
                                    M
                                    <asp:TextBox ID="TxtAgeLKUDay" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();" onkeyup="ValidateDay(this);"></asp:TextBox>
                                    D
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: right;">
                                    <asp:HiddenField ID="HFTempLKUMasterId" runat="server" />
                                    <asp:Button ID="ButtonCancelLKU" runat="server" Text="Cancel" OnClick="ButtonCancelLKU_Click" />
                                    <asp:Button ID="ButtonSaveLKUHeadCirc" runat="server" Text="Add" OnClick="ButtonSaveLKUHeadCirc_Click" />
                                    <asp:Button ID="ButtonUpdateLKUHeadCirc" runat="server" Text="Update" OnClick="ButtonUpdateLKUHeadCirc_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanelBMI" runat="server">
            <ContentTemplate>
                <div class="col-sm-9" style="width: 70%;">

                    <div style="padding-bottom: 15px;">
                        <asp:Label ID="LabelHeaderBMI" runat="server" Text="BMI Menurut Umur"></asp:Label>
                        <br />
                        <asp:DropDownList ID="DDLKurvaBMI" runat="server" Style="width: 165px;" AutoPostBack="true" OnSelectedIndexChanged="DDLKurvaBMI_SelectedIndexChanged">
                            <%--<asp:ListItem Value="0"> Birth to 6 Months </asp:ListItem>--%>
                            <asp:ListItem Value="1"> Birth to 2 Years </asp:ListItem>
                            <asp:ListItem Value="2"> Birth to 5 Years </asp:ListItem>
                            <asp:ListItem Value="3"> 5 Years to 19 Years </asp:ListItem>
                        </asp:DropDownList>
                        (z-scores)

                        &nbsp;
                        &nbsp;

                        <asp:CheckBox ID="CheckBoxStandardValueBMI" runat="server" Text="  Show Standard Value" OnCheckedChanged="CheckBoxStandardValueBMI_CheckedChanged" AutoPostBack="true" />
                    </div>
                    <div style="width: 95%; height: 500px;">
                        <canvas id="canvasBMI"></canvas>
                    </div>
                    <hr />
                </div>
                <div class="col-sm-3" style="width: 30%; padding-top: 95px;">

                    <asp:HiddenField ID="HF_BMI_Data" runat="server" />
                    <asp:HiddenField ID="HF_BMI_MinMaxAge" runat="server" />
                    <asp:HiddenField ID="HF_BMI_MinMaxY" runat="server" />

                    List Data BMI/U
                    <br />
                    <div class="chart-listdata-box scrollEMR">
                    <table class="table-condensed table-divider" style="width:100%;">
                        <asp:Repeater ID="RepeaterBMI" runat="server">
                            <HeaderTemplate>
                                <tr>
                                    <th>No
                                    </th>
                                    <%--<th>X
                                    </th>--%>
                                    <th>BMI
                                    </th>
                                    <th>Age
                                    </th>
                                    <th></th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Container.ItemIndex + 1 %>
                                        <asp:HiddenField ID="HFBMIMasterId" runat="server" Value='<%#Eval("chart_transaction_master_id") %>' />
                                    </td>
                                    <%--<td>
                                        <asp:Label ID="LabelBMIx" runat="server" Text='<%#Eval("x") %>'></asp:Label>
                                    </td>--%>
                                    <td>
                                        <asp:Label ID="LabelBMIy" runat="server" Text='<%#Eval("y") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelBMIage" runat="server" Text='<%#Eval("age") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="ButtonEditBMI" runat="server" Text="E" OnClick="ButtonEditBMI_Click" />
                                        <asp:Button ID="ButtonDeleteBMI" runat="server" Text="D" OnClick="ButtonDeleteBMI_Click" />--%>
                                        <asp:ImageButton ID="ButtonEditBMI" runat="server" OnClick="ButtonEditBMI_Click" ImageUrl="~/Images/Icon/ic_edit.svg" style="width:16px; height:16px; margin-right:5px;" ToolTip="Edit" />
                                        <asp:ImageButton ID="ButtonDeleteBMI" runat="server" OnClick="ButtonDeleteBMI_Click" ImageUrl="~/Images/Icon/ic_delete.svg" style="width:14px; height:14px;" ToolTip="Delete" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    </div>
                    <br />
                    <asp:Button ID="ButtonAddBMI" runat="server" Text="Add BMI" CssClass="btn btn-github btn-emr-small" Style="margin-top: 10px; margin-bottom: 0px;" OnClick="ButtonAddBMI_Click" />
                    <div style="display:inline-block;">
                         <asp:UpdateProgress ID="UpdateProgressBMI" runat="server" AssociatedUpdatePanelID="UpdatePanelBMI">
                            <ProgressTemplate>
                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                </div>
                                &nbsp;
                                <img alt="" style="background-color: transparent; height: 20px; margin-top:7px; margin-left:7px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <br />
                    <div id="divBMIData" runat="server" visible="false" class="chart-listdata-box" style="background-color:whitesmoke;">
                        <table class="table-condensed">
                            <tr>
                                <th colspan="3">Input BMI
                                </th>
                            </tr>
                            <tr>
                                <td>BMI
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxBMIWeight" runat="server" onkeypress="return ValidateNumberWithComma();"></asp:TextBox>
                                    <span style="margin-left:-42px;">kg/m2</span>  
                                </td>
                            </tr>
                            <tr>
                                <td>Age
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtAgeBMIYear" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();"></asp:TextBox>
                                    Y
                                    <asp:TextBox ID="TxtAgeBMIMonth" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();" onkeyup="ValidateMonth(this);"></asp:TextBox>
                                    M
                                    <asp:TextBox ID="TxtAgeBMIDay" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();" onkeyup="ValidateDay(this);"></asp:TextBox>
                                    D
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: right;">
                                    <asp:HiddenField ID="HFTempBMIMasterId" runat="server" />
                                    <asp:Button ID="ButtonCancelBMI" runat="server" Text="Cancel" OnClick="ButtonCancelBMI_Click" />
                                    <asp:Button ID="ButtonSaveBMIWeight" runat="server" Text="Add" OnClick="ButtonSaveBMIWeight_Click" />
                                    <asp:Button ID="ButtonUpdateBMIWeight" runat="server" Text="Update" OnClick="ButtonUpdateBMIWeight_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanelLLU" runat="server">
            <ContentTemplate>
                <div class="col-sm-9" style="width: 70%;">

                    <div style="padding-bottom: 15px;">
                        <asp:Label ID="LabelHeaderLLU" runat="server" Text="LL/U (Lingkar Lengan Menurut Umur)"></asp:Label>
                        <br />
                        <asp:DropDownList ID="DDLKurvaLLU" runat="server" Style="width: 165px;">
                            <asp:ListItem Value="0"> 3 Months to 5 Years </asp:ListItem>
                        </asp:DropDownList>
                        (z-scores)

                        &nbsp;
                        &nbsp;

                        <asp:CheckBox ID="CheckBoxStandardValueLLU" runat="server" Text="  Show Standard Value" OnCheckedChanged="CheckBoxStandardValueLLU_CheckedChanged" AutoPostBack="true" />
                    </div>
                    <div style="width: 95%; height: 500px;">
                        <canvas id="canvasLLU"></canvas>
                    </div>
                    <hr />
                </div>
                <div class="col-sm-3" style="width: 30%; padding-top: 95px;">

                    <asp:HiddenField ID="HF_LLU_Data" runat="server" />
                    <asp:HiddenField ID="HF_LLU_MinMaxAge" runat="server" />
                    <asp:HiddenField ID="HF_LLU_MinMaxY" runat="server" />

                    List Data LL/U
                    <br />
                    <div class="chart-listdata-box scrollEMR">
                    <table class="table-condensed table-divider" style="width:100%;">
                        <asp:Repeater ID="RepeaterLLU" runat="server">
                            <HeaderTemplate>
                                <tr>
                                    <th>No
                                    </th>
                                    <%--<th>X
                                    </th>--%>
                                    <th>Arm Circ.
                                    </th>
                                    <th>Age
                                    </th>
                                    <th></th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Container.ItemIndex + 1 %>
                                        <asp:HiddenField ID="HFLLUMasterId" runat="server" Value='<%#Eval("chart_transaction_master_id") %>' />
                                    </td>
                                    <%--<td>
                                        <asp:Label ID="LabelLLUx" runat="server" Text='<%#Eval("x") %>'></asp:Label>
                                    </td>--%>
                                    <td>
                                        <asp:Label ID="LabelLLUy" runat="server" Text='<%#Eval("y") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelLLUage" runat="server" Text='<%#Eval("age") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="ButtonEditLLU" runat="server" Text="E" OnClick="ButtonEditLLU_Click" />
                                        <asp:Button ID="ButtonDeleteLLU" runat="server" Text="D" OnClick="ButtonDeleteLLU_Click" />--%>
                                        <asp:ImageButton ID="ButtonEditLLU" runat="server" OnClick="ButtonEditLLU_Click" ImageUrl="~/Images/Icon/ic_edit.svg" style="width:16px; height:16px; margin-right:5px;" ToolTip="Edit" />
                                        <asp:ImageButton ID="ButtonDeleteLLU" runat="server" OnClick="ButtonDeleteLLU_Click" ImageUrl="~/Images/Icon/ic_delete.svg" style="width:14px; height:14px;" ToolTip="Delete" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    </div>
                    <br />
                    <asp:Button ID="ButtonAddLLU" runat="server" Text="Add LLU" CssClass="btn btn-github btn-emr-small" Style="margin-top: 10px; margin-bottom: 0px;" OnClick="ButtonAddLLU_Click" />
                    <div style="display:inline-block;">
                         <asp:UpdateProgress ID="UpdateProgressLLU" runat="server" AssociatedUpdatePanelID="UpdatePanelLLU">
                            <ProgressTemplate>
                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                </div>
                                &nbsp;
                                <img alt="" style="background-color: transparent; height: 20px; margin-top:7px; margin-left:7px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <br />
                    <div id="divLLUData" runat="server" visible="false" class="chart-listdata-box" style="background-color:whitesmoke;">
                        <table class="table-condensed">
                            <tr>
                                <th colspan="3">Input Arm Circumference
                                </th>
                            </tr>
                            <tr>
                                <td>Arm Circ.
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxLLUWeight" runat="server" onkeypress="return ValidateNumberWithComma();"></asp:TextBox>
                                    <span style="margin-left:-25px;">cm</span>  
                                </td>
                            </tr>
                            <tr>
                                <td>Age
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtAgeLLUYear" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();"></asp:TextBox>
                                    Y
                                    <asp:TextBox ID="TxtAgeLLUMonth" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();" onkeyup="ValidateMonth(this);"></asp:TextBox>
                                    M
                                    <asp:TextBox ID="TxtAgeLLUDay" runat="server" Style="width: 35px;" onkeypress="return ValidateNumber();" onkeyup="ValidateDay(this);"></asp:TextBox>
                                    D
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: right;">
                                    <asp:HiddenField ID="HFTempLLUMasterId" runat="server" />
                                    <asp:Button ID="ButtonCancelLLU" runat="server" Text="Cancel" OnClick="ButtonCancelLLU_Click" />
                                    <asp:Button ID="ButtonSaveLLUWeight" runat="server" Text="Add" OnClick="ButtonSaveLLUWeight_Click" />
                                    <asp:Button ID="ButtonUpdateLLUWeight" runat="server" Text="Update" OnClick="ButtonUpdateLLUWeight_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="col-sm-12" style="padding: 7px 25px 7px 15px; min-height: 35px; border-top: 1px solid #e5e5e5;">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%; text-align: left;">
                       
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <asp:UpdatePanel ID="UpdatePanelSaveKurva2" runat="server">
                            <ContentTemplate>
                                <div style="display:inline-block;">
                                     <asp:UpdateProgress ID="UpdateProgressSaveKurva2" runat="server" AssociatedUpdatePanelID="UpdatePanelSaveKurva2">
                                        <ProgressTemplate>
                                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                            </div>
                                            &nbsp;
                                            <img alt="" style="background-color: transparent; height: 20px; margin-right:10px; margin-bottom:3px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <asp:Label ID="LabelChartSaveFail2" runat="server" Visible="false" Text="" Style="font-family: Helvetica; font-weight: bold; font-size: 14px; color:red; margin-right:20px;"> <i class="fa fa-warning"></i> Save Failed </asp:Label>
                                <asp:Button ID="ButtonCancelKurva2" class="btn btn-default" runat="server" Text="Cancel" OnClick="ButtonCancelKurva_Click" />
                                <asp:Button ID="ButtonSaveKurva2" class="btn btn-lightGreen" runat="server" Text="Save" OnClick="ButtonSaveKurva_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>

    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</div>

<script type="text/javascript">

    function ValidateNumber() {
        if (event.keyCode >= 48 && event.keyCode <= 57) {
            return true
        }
        else {
            return false
        }
    }

    function ValidateNumberWithComma() {
        if ((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 44 || event.keyCode == 46) {
            return true
        }
        else {
            return false
        }
    }

    function ValidateMonth(obj) {
        if (obj.value > 12) {
            obj.value = 0;
        }
    }

    function ValidateDay(obj) {
        if (obj.value > 31) {
            obj.value = 0;
        }
    }

    //var datanol = HFdatanol.value.replace(/\"/g, "");
    //console.log(datamindua);
    function configKurvaPBU() {

        var HFdatapbu = document.getElementById("<%= HF_PBU_Data.ClientID %>");
        var datapbu = JSON.parse(HFdatapbu.value);


        var HFdatanol = document.getElementById("<%= HF_PBU0.ClientID %>");
        var datanol = JSON.parse(HFdatanol.value);

        var HFdatadua = document.getElementById("<%= HF_PBUplus2.ClientID %>");
        var datadua = JSON.parse(HFdatadua.value);

        var HFdatamindua = document.getElementById("<%= HF_PBUminus2.ClientID %>");
        var datamindua = JSON.parse(HFdatamindua.value);

        var HFdatatiga = document.getElementById("<%= HF_PBUplus3.ClientID %>");
        var datatiga = JSON.parse(HFdatatiga.value);

        var HFdatamintiga = document.getElementById("<%= HF_PBUminus3.ClientID %>");
        var datamintiga = JSON.parse(HFdatamintiga.value);


        var HFminmaxAge = document.getElementById("<%= HF_PBU_MinMaxAge.ClientID %>");
        var minmaxpbuage = HFminmaxAge.value;
        var HFminmaxY = document.getElementById("<%= HF_PBU_MinMaxY.ClientID %>");
        var minmaxpbuY = HFminmaxY.value;

        return {
            type: 'scatter',
            data: {
                datasets: [
                    {
                        label: 'Data',
                        fill: false,
                        backgroundColor: 'black',
                        borderColor: 'black',
                        data: datapbu,
                        //data: [
                        //    {
                        //        x: 2,
                        //        y: 0
                        //    },
                        //    {
                        //        x: 50,
                        //        y: 6
                        //    },
                        //    {
                        //        x: 70,
                        //        y: 8
                        //    }
                        //],
                        showLine: true,
                        pointBackgroundColor: 'black',
                        pointBorderColor: 'black'
                    },
                    {
                        label: '3',
                        fill: false,
                        backgroundColor: window.chartColors.grey,
                        borderColor: window.chartColors.grey,
                        data: datatiga,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '2',
                        fill: false,
                        backgroundColor: window.chartColors.red,
                        borderColor: window.chartColors.red,
                        data: datadua,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '0',
                        fill: false,
                        backgroundColor: window.chartColors.green,
                        borderColor: window.chartColors.green,
                        data: datanol,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-2',
                        fill: false,
                        backgroundColor: window.chartColors.red,
                        borderColor: window.chartColors.red,
                        data: datamindua,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-3',
                        fill: false,
                        backgroundColor: window.chartColors.grey,
                        borderColor: window.chartColors.grey,
                        data: datamintiga,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    }]
            },
            options: {
                title: {
                    display: false,
                    text: 'PB/U (Panjang Badan Menurut Umur)'
                },
                legend: {
                    display: true,
                    labels: {
                        usePointStyle: false
                    }
                },
                scales: {
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: minmaxpbuage.split(';')[3],
                        },
                        ticks: {
                            min: Number(minmaxpbuage.split(';')[0]),
                            max: Number(minmaxpbuage.split(';')[1]),
                            stepSize: Number(minmaxpbuage.split(';')[2]),
                            callback:
                                function (tick) {

                                    if (minmaxpbuage == "60;228;3;Age(completed months and years)") {
                                        if (tick % 12 != 0) {
                                            var remain = tick % 3;
                                            if (remain === 0) {
                                                if (tick <= 12) {
                                                    return [(tick / 3).toString(), ''];
                                                }
                                                else {
                                                    var basedata = tick - (~~(tick / 12) * 12);
                                                    if (basedata % 9 == 0) {
                                                        return '9';
                                                    }
                                                    else if (basedata % 6 == 0) {
                                                        return '6';
                                                    }
                                                    else if (basedata % 3 == 0) {
                                                        return '3';
                                                    }
                                                }
                                            }
                                        }
                                        else {
                                            var remain = tick % 12;
                                            if (remain === 0) {
                                                return ['', (tick / 12).toString()];
                                            }
                                        }
                                    }
                                    else if (minmaxpbuage == "0;168;7;Age(completed weeks or months)")
                                    {
                                        if (tick <= 84) {
                                            var remain = tick % 7;
                                            if (remain === 0) {
                                                if (tick == 84) {
                                                    return [(tick / 7).toString(), (tick / 28).toString()];
                                                }
                                                else {
                                                    return [(tick / 7).toString(), ''];
                                                }
                                            }
                                        }
                                        else {
                                            var remain = tick % 28;
                                            if (remain === 0) {
                                                return ['', (tick / 28).toString()];
                                            }
                                        }
                                    }
                                    else {
                                        if (tick % 336 != 0) {
                                            var remain = tick % 28;
                                            if (remain === 0) {
                                                if (tick <= 336) {
                                                    return [(tick / 28).toString(), ''];
                                                }
                                                else {
                                                    var basedata = tick - (~~(tick / 336) * 336);
                                                    if (basedata % 308 == 0) {
                                                        return '11';
                                                    }
                                                    else if (basedata % 280 == 0) {
                                                        return '10';
                                                    }
                                                    else if (basedata % 252 == 0) {
                                                        return '9';
                                                    }
                                                    else if (basedata % 224 == 0) {
                                                        return '8';
                                                    }
                                                    else if (basedata % 196 == 0) {
                                                        return '7';
                                                    }
                                                    else if (basedata % 168 == 0) {
                                                        return '6';
                                                    }
                                                    else if (basedata % 140 == 0) {
                                                        return '5';
                                                    }
                                                    else if (basedata % 112 == 0) {
                                                        return '4';
                                                    }
                                                    else if (basedata % 84 == 0) {
                                                        return '3';
                                                    }
                                                    else if (basedata % 56 == 0) {
                                                        return '2';
                                                    }
                                                    else if (basedata % 28 == 0) {
                                                        return '1';
                                                    }
                                                }
                                            }
                                        }
                                        else {
                                            if (tick === 0) {
                                                return '';
                                            }
                                            var remain = tick % 336;
                                            if (remain === 0) {
                                                return ['', (tick / 336).toString()];
                                            }
                                        }
                                    }
                                    return '';

                                    //if (minmaxpbuage == "60;228;3;Age(completed months and years)") {

                                    //    if (tick % 12 != 0) {
                                    //        var remain = tick % 3;
                                    //        if (remain === 0) {
                                    //            if (tick <= 12) {
                                    //                return [(tick / 3).toString(), ''];
                                    //            }
                                    //            else {
                                    //                var basedata = tick - (~~(tick / 12) * 12);
                                    //                if (basedata % 9 == 0) {
                                    //                    return '9';
                                    //                }
                                    //                else if (basedata % 6 == 0) {
                                    //                    return '6';
                                    //                }
                                    //                else if (basedata % 3 == 0) {
                                    //                    return '3';
                                    //                }
                                    //            }
                                    //        }
                                    //    }
                                    //    else {
                                    //        var remain = tick % 12;
                                    //        if (remain === 0) {
                                    //            return ['', (tick / 12).toString()];
                                    //        }
                                    //    }
                                    //}
                                    //else {
                                    //    if (tick <= 84) {
                                    //        var remain = tick % 7;
                                    //        if (remain === 0) {
                                    //            if (tick == 84) {
                                    //                return [(tick / 7).toString(), (tick / 28).toString()];
                                    //            }
                                    //            else {
                                    //                return [(tick / 7).toString(), ''];
                                    //            }
                                    //        }
                                    //    }
                                    //    else {
                                    //        var remain = tick % 28;
                                    //        if (remain === 0) {
                                    //            return ['', (tick / 28).toString()];
                                    //        }
                                    //    }
                                    //}
                                    //return '';
                                },
                        }
                    }],
                    yAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'Length(cm)'
                        },
                        ticks: {
                            min: Number(minmaxpbuY.split(';')[0]),
                            max: Number(minmaxpbuY.split(';')[1]),
                            stepSize: Number(minmaxpbuY.split(';')[2])
                        }
                    }]
                },
                responsive: true,
                maintainAspectRatio: false
            },

        };
    }

    function configKurvaBBU() {

        var HFdatabbu = document.getElementById("<%= HF_BBU_Data.ClientID %>");
        var databbu = JSON.parse(HFdatabbu.value);


        var HFdatanol = document.getElementById("<%= HF_BBU0.ClientID %>");
        var datanol = JSON.parse(HFdatanol.value);

        var HFdatadua = document.getElementById("<%= HF_BBUplus2.ClientID %>");
        var datadua = JSON.parse(HFdatadua.value);

        var HFdatamindua = document.getElementById("<%= HF_BBUminus2.ClientID %>");
        var datamindua = JSON.parse(HFdatamindua.value);

        var HFdatatiga = document.getElementById("<%= HF_BBUplus3.ClientID %>");
        var datatiga = JSON.parse(HFdatatiga.value);

        var HFdatamintiga = document.getElementById("<%= HF_BBUminus3.ClientID %>");
        var datamintiga = JSON.parse(HFdatamintiga.value);


        var HFminmaxAge = document.getElementById("<%= HF_BBU_MinMaxAge.ClientID %>");
        var minmaxbbuage = HFminmaxAge.value;
        var HFminmaxY = document.getElementById("<%= HF_BBU_MinMaxY.ClientID %>");
        var minmaxbbuY = HFminmaxY.value;

        //console.log(datatiga);

        return {
            type: 'scatter',
            data: {
                datasets: [
                    {
                        label: 'Data',
                        fill: false,
                        backgroundColor: 'black',
                        borderColor: 'black',
                        data: databbu,
                        //data: [
                        //    {
                        //        x: 2,
                        //        y: 0
                        //    },
                        //    {
                        //        x: 50,
                        //        y: 6
                        //    },
                        //    {
                        //        x: 70,
                        //        y: 8
                        //    }
                        //],
                        showLine: true,
                        pointBackgroundColor: 'black',
                        pointBorderColor: 'black'
                    },
                    {
                        label: '3',
                        fill: false,
                        backgroundColor: window.chartColors.grey,
                        borderColor: window.chartColors.grey,
                        data: datatiga,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '2',
                        fill: false,
                        backgroundColor: window.chartColors.red,
                        borderColor: window.chartColors.red,
                        data: datadua,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '0',
                        fill: false,
                        backgroundColor: window.chartColors.green,
                        borderColor: window.chartColors.green,
                        data: datanol,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-2',
                        fill: false,
                        backgroundColor: window.chartColors.red,
                        borderColor: window.chartColors.red,
                        data: datamindua,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-3',
                        fill: false,
                        backgroundColor: window.chartColors.grey,
                        borderColor: window.chartColors.grey,
                        data: datamintiga,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    }]
            },
            options: {
                title: {
                    display: false,
                    text: 'BB/U (Berat Badan Menurut Umur)'
                },
                legend: {
                    display: true,
                    labels: {
                        usePointStyle: false
                    }
                },
                scales: {
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: minmaxbbuage.split(';')[3],
                        },
                        ticks: {
                            min: Number(minmaxbbuage.split(';')[0]),
                            max: Number(minmaxbbuage.split(';')[1]),
                            stepSize: Number(minmaxbbuage.split(';')[2]),
                            callback:
                                function (tick) {

                                    if (minmaxbbuage == "60;120;3;Age(completed months and years)") {
                                        if (tick % 12 != 0) {
                                            var remain = tick % 3;
                                            if (remain === 0) {
                                                if (tick <= 12) {
                                                    return [(tick / 3).toString(), ''];
                                                }
                                                else {
                                                    var basedata = tick - (~~(tick / 12) * 12);
                                                    if (basedata % 9 == 0) {
                                                        return '9';
                                                    }
                                                    else if (basedata % 6 == 0) {
                                                        return '6';
                                                    }
                                                    else if (basedata % 3 == 0) {
                                                        return '3';
                                                    }
                                                }
                                            }
                                        }
                                        else {
                                            var remain = tick % 12;
                                            if (remain === 0) {
                                                return ['', (tick / 12).toString()];
                                            }
                                        }
                                    }
                                    else if (minmaxbbuage == "0;168;7;Age(completed weeks or months)")
                                    {
                                        if (tick <= 84) {
                                            var remain = tick % 7;
                                            if (remain === 0) {
                                                if (tick == 84) {
                                                    return [(tick / 7).toString(), (tick / 28).toString()];
                                                }
                                                else {
                                                    return [(tick / 7).toString(), ''];
                                                }
                                            }
                                        }
                                        else {
                                            var remain = tick % 28;
                                            if (remain === 0) {
                                                return ['', (tick / 28).toString()];
                                            }
                                        }
                                    }
                                    else {
                                        if (tick % 336 != 0) {
                                            var remain = tick % 28;
                                            if (remain === 0) {
                                                if (tick <= 336) {
                                                    return [(tick / 28).toString(), ''];
                                                }
                                                else {
                                                    var basedata = tick - (~~(tick / 336) * 336);
                                                    if (basedata % 308 == 0) {
                                                        return '11';
                                                    }
                                                    else if (basedata % 280 == 0) {
                                                        return '10';
                                                    }
                                                    else if (basedata % 252 == 0) {
                                                        return '9';
                                                    }
                                                    else if (basedata % 224 == 0) {
                                                        return '8';
                                                    }
                                                    else if (basedata % 196 == 0) {
                                                        return '7';
                                                    }
                                                    else if (basedata % 168 == 0) {
                                                        return '6';
                                                    }
                                                    else if (basedata % 140 == 0) {
                                                        return '5';
                                                    }
                                                    else if (basedata % 112 == 0) {
                                                        return '4';
                                                    }
                                                    else if (basedata % 84 == 0) {
                                                        return '3';
                                                    }
                                                    else if (basedata % 56 == 0) {
                                                        return '2';
                                                    }
                                                    else if (basedata % 28 == 0) {
                                                        return '1';
                                                    }
                                                }
                                            }
                                        }
                                        else {
                                            if (tick === 0) {
                                                return '';
                                            }
                                            var remain = tick % 336;
                                            if (remain === 0) {
                                                return ['', (tick / 336).toString()];
                                            }
                                        }
                                    }
                                    return '';

                                    //if (minmaxbbuage == "60;120;3;Age(completed months and years)")
                                    //{
                                    //    if (tick % 12 != 0) {
                                    //        var remain = tick % 3;
                                    //        if (remain === 0) {
                                    //            if (tick <= 12) {
                                    //                return [(tick / 3).toString(), ''];
                                    //            }
                                    //            else {
                                    //                var basedata = tick - (~~(tick / 12) * 12);
                                    //                if (basedata % 9 == 0) {
                                    //                    return '9';
                                    //                }
                                    //                else if (basedata % 6 == 0) {
                                    //                    return '6';
                                    //                }
                                    //                else if (basedata % 3 == 0) {
                                    //                    return '3';
                                    //                }
                                    //            }
                                    //        }
                                    //    }
                                    //    else {
                                    //        var remain = tick % 12;
                                    //        if (remain === 0) {
                                    //            return ['', (tick / 12).toString()];
                                    //        }
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    if (tick <= 84) {
                                    //        var remain = tick % 7;
                                    //        if (remain === 0) {
                                    //            if (tick == 84) {
                                    //                return [(tick / 7).toString(), (tick / 28).toString()];
                                    //            }
                                    //            else {
                                    //                return [(tick / 7).toString(), ''];
                                    //            }
                                    //        }
                                    //    }
                                    //    else {
                                    //        var remain = tick % 28;
                                    //        if (remain === 0) {
                                    //            return ['', (tick / 28).toString()];
                                    //        }
                                    //    }
                                    //}

                                    return '';
                                },
                        }
                    }],
                    yAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'Weight(kg)'
                        },
                        ticks: {
                            min:  Number(minmaxbbuY.split(';')[0]),
                            max: Number(minmaxbbuY.split(';')[1]),
                            stepSize: Number(minmaxbbuY.split(';')[2])
                        }
                    }]
                },
                responsive: true,
                maintainAspectRatio: false
            },

        };
    }

    function configKurvaBBPB() {

        var HFdatabbpb = document.getElementById("<%= HF_BBPB_Data.ClientID %>");
        var databbpb = JSON.parse(HFdatabbpb.value);


        var HFdatanol = document.getElementById("<%= HF_BBPB0.ClientID %>");
        var datanol = JSON.parse(HFdatanol.value);

        var HFdatasatu = document.getElementById("<%= HF_BBPBplus1.ClientID %>");
        var datasatu = JSON.parse(HFdatasatu.value);

        var HFdataminsatu = document.getElementById("<%= HF_BBPBminus1.ClientID %>");
        var dataminsatu = JSON.parse(HFdataminsatu.value);

        var HFdatadua = document.getElementById("<%= HF_BBPBplus2.ClientID %>");
        var datadua = JSON.parse(HFdatadua.value);

        var HFdatamindua = document.getElementById("<%= HF_BBPBminus2.ClientID %>");
        var datamindua = JSON.parse(HFdatamindua.value);

        var HFdatatiga = document.getElementById("<%= HF_BBPBplus3.ClientID %>");
        var datatiga = JSON.parse(HFdatatiga.value);

        var HFdatamintiga = document.getElementById("<%= HF_BBPBminus3.ClientID %>");
        var datamintiga = JSON.parse(HFdatamintiga.value);


        var HFminmaxAge = document.getElementById("<%= HF_BBPB_MinMaxAge.ClientID %>");
        var minmaxbbpbage = HFminmaxAge.value;
        var HFminmaxX = document.getElementById("<%= HF_BBPB_MinMaxX.ClientID %>");
        var minmaxbbpbX = HFminmaxX.value;
        var HFminmaxY = document.getElementById("<%= HF_BBPB_MinMaxY.ClientID %>");
        var minmaxbbpbY = HFminmaxY.value;

        //console.log(datatiga);

        return {
            type: 'scatter',
            data: {
                datasets: [
                    {
                        label: 'Data',
                        fill: false,
                        backgroundColor: 'black',
                        borderColor: 'black',
                        data: databbpb,
                        //data: [
                        //    {
                        //        x: 2,
                        //        y: 0
                        //    },
                        //    {
                        //        x: 50,
                        //        y: 6
                        //    },
                        //    {
                        //        x: 70,
                        //        y: 8
                        //    }
                        //],
                        showLine: true,
                        pointBackgroundColor: 'black',
                        pointBorderColor: 'black'
                    },
                    {
                        label: '3',
                        fill: false,
                        backgroundColor: window.chartColors.grey,
                        borderColor: window.chartColors.grey,
                        data: datatiga,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '1',
                        fill: false,
                        backgroundColor: window.chartColors.orange,
                        borderColor: window.chartColors.orange,
                        data: datasatu,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '2',
                        fill: false,
                        backgroundColor: window.chartColors.red,
                        borderColor: window.chartColors.red,
                        data: datadua,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '0',
                        fill: false,
                        backgroundColor: window.chartColors.green,
                        borderColor: window.chartColors.green,
                        data: datanol,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-1',
                        fill: false,
                        backgroundColor: window.chartColors.orange,
                        borderColor: window.chartColors.orange,
                        data: dataminsatu,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-2',
                        fill: false,
                        backgroundColor: window.chartColors.red,
                        borderColor: window.chartColors.red,
                        data: datamindua,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-3',
                        fill: false,
                        backgroundColor: window.chartColors.grey,
                        borderColor: window.chartColors.grey,
                        data: datamintiga,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    }]
            },
            options: {
                title: {
                    display: false,
                    text: 'BB/PB (Berat Badan Menurut Panjang Badan)'
                },
                legend: {
                    display: true,
                    labels: {
                        usePointStyle: false
                    }
                },
                scales: {
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: minmaxbbpbX.split(';')[3], //'Length(cm)',
                        },
                        ticks: {
                            min: Number(minmaxbbpbX.split(';')[0]), //45,
                            max: Number(minmaxbbpbX.split(';')[1]), //110,
                            stepSize: Number(minmaxbbpbX.split(';')[2]) //5
                        }
                    }],
                    yAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'Weight(kg)'
                        },
                        ticks: {
                            min: Number(minmaxbbpbY.split(';')[0]), //0,
                            max: Number(minmaxbbpbY.split(';')[1]), //24,
                            stepSize: Number(minmaxbbpbY.split(';')[2]) //2
                        }
                    }]
                },
                responsive: true,
                maintainAspectRatio: false
            },

        };
    }

    function configKurvaLKU() {

        var HFdatalku = document.getElementById("<%= HF_LKU_Data.ClientID %>");
        var datalku = JSON.parse(HFdatalku.value);


        var HFdatanol = document.getElementById("<%= HF_LKU0.ClientID %>");
        var datanol = JSON.parse(HFdatanol.value);

        var HFdatasatu = document.getElementById("<%= HF_LKUplus1.ClientID %>");
        var datasatu = JSON.parse(HFdatasatu.value);

        var HFdataminsatu = document.getElementById("<%= HF_LKUminus1.ClientID %>");
        var dataminsatu = JSON.parse(HFdataminsatu.value);

        var HFdatadua = document.getElementById("<%= HF_LKUplus2.ClientID %>");
        var datadua = JSON.parse(HFdatadua.value);

        var HFdatamindua = document.getElementById("<%= HF_LKUminus2.ClientID %>");
        var datamindua = JSON.parse(HFdatamindua.value);

        var HFdatatiga = document.getElementById("<%= HF_LKUplus3.ClientID %>");
        var datatiga = JSON.parse(HFdatatiga.value);

        var HFdatamintiga = document.getElementById("<%= HF_LKUminus3.ClientID %>");
        var datamintiga = JSON.parse(HFdatamintiga.value);


        var HFminmaxAge = document.getElementById("<%= HF_LKU_MinMaxAge.ClientID %>");
        var minmaxlkuage = HFminmaxAge.value;
        var HFminmaxY = document.getElementById("<%= HF_LKU_MinMaxY.ClientID %>");
        var minmaxlkuY = HFminmaxY.value;

        return {
            type: 'scatter',
            data: {
                datasets: [
                    {
                        label: 'Data',
                        fill: false,
                        backgroundColor: 'black',
                        borderColor: 'black',
                        data: datalku,
                        //data: [
                        //    {
                        //        x: 2,
                        //        y: 0
                        //    },
                        //    {
                        //        x: 50,
                        //        y: 6
                        //    },
                        //    {
                        //        x: 70,
                        //        y: 8
                        //    }
                        //],
                        showLine: true,
                        pointBackgroundColor: 'black',
                        pointBorderColor: 'black'
                    },
                    {
                        label: '3',
                        fill: false,
                        backgroundColor: window.chartColors.grey,
                        borderColor: window.chartColors.grey,
                        data: datatiga,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '2',
                        fill: false,
                        backgroundColor: window.chartColors.red,
                        borderColor: window.chartColors.red,
                        data: datadua,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '1',
                        fill: false,
                        backgroundColor: window.chartColors.orange,
                        borderColor: window.chartColors.orange,
                        data: datasatu,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '0',
                        fill: false,
                        backgroundColor: window.chartColors.green,
                        borderColor: window.chartColors.green,
                        data: datanol,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-1',
                        fill: false,
                        backgroundColor: window.chartColors.orange,
                        borderColor: window.chartColors.orange,
                        data: dataminsatu,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-2',
                        fill: false,
                        backgroundColor: window.chartColors.red,
                        borderColor: window.chartColors.red,
                        data: datamindua,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-3',
                        fill: false,
                        backgroundColor: window.chartColors.grey,
                        borderColor: window.chartColors.grey,
                        data: datamintiga,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    }]
            },
            options: {
                title: {
                    display: false,
                    text: 'LK/U (Lingkar Kepala Menurut Umur)'
                },
                legend: {
                    display: true,
                    labels: {
                        usePointStyle: false
                    }
                },
                scales: {
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: minmaxlkuage.split(';')[3],
                        },
                        ticks: {
                            min: Number(minmaxlkuage.split(';')[0]),
                            max: Number(minmaxlkuage.split(';')[1]),
                            stepSize: Number(minmaxlkuage.split(';')[2]),
                            callback:
                                function (tick) {

                                    if (minmaxlkuage == "0;91;7;Age(weeks)") {
                                        var remain = tick % 7;
                                        if (remain === 0) {                                            
                                            return [(tick / 7).toString(), ''];  
                                        }
                                        else {
                                            return '';
                                        }
                                    }
                                    else
                                    {
                                        if (tick % 336 != 0) {
                                            var remain = tick % 56;
                                            if (remain === 0) {
                                                if (tick <= 336) {
                                                    return [(tick / 28).toString(), ''];
                                                }
                                                else {
                                                    var basedata = tick - (~~(tick / 336) * 336);
                                                    if (basedata % 280 == 0) {
                                                        return '10';
                                                    }
                                                    else if (basedata % 224 == 0) {
                                                        return '8';
                                                    }
                                                    else if (basedata % 168 == 0) {
                                                        return '6';
                                                    }
                                                    else if (basedata % 112 == 0) {
                                                        return '4';
                                                    }
                                                    if (basedata % 56 == 0) {
                                                        return '2';
                                                    }
                                                }
                                            }
                                        }
                                        else {
                                            if (tick === 0) {
                                                return '';
                                            }
                                            var remain = tick % 336;
                                            if (remain === 0) {
                                                return ['', (tick / 336).toString()];
                                            }
                                        }
                                    }
                                    return '';
                                },
                        }
                    }],
                    yAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'Head Circumference(cm)'
                        },
                        ticks: {
                            min: Number(minmaxlkuY.split(';')[0]), //30,
                            max: Number(minmaxlkuY.split(';')[1]), //52,
                            stepSize: Number(minmaxlkuY.split(';')[2])//1
                        }
                    }]
                },
                responsive: true,
                maintainAspectRatio: false
            },

        };
    }

    function configKurvaBMI() {

        var HFdatabmi = document.getElementById("<%= HF_BMI_Data.ClientID %>");
        var databmi = JSON.parse(HFdatabmi.value);


        var HFdatanol = document.getElementById("<%= HF_BMI0.ClientID %>");
        var datanol = JSON.parse(HFdatanol.value);

        var HFdatasatu = document.getElementById("<%= HF_BMIplus1.ClientID %>");
        var datasatu = JSON.parse(HFdatasatu.value);

        var HFdataminsatu = document.getElementById("<%= HF_BMIminus1.ClientID %>");
        var dataminsatu = JSON.parse(HFdataminsatu.value);

        var HFdatadua = document.getElementById("<%= HF_BMIplus2.ClientID %>");
        var datadua = JSON.parse(HFdatadua.value);

        var HFdatamindua = document.getElementById("<%= HF_BMIminus2.ClientID %>");
        var datamindua = JSON.parse(HFdatamindua.value);

        var HFdatatiga = document.getElementById("<%= HF_BMIplus3.ClientID %>");
        var datatiga = JSON.parse(HFdatatiga.value);

        var HFdatamintiga = document.getElementById("<%= HF_BMIminus3.ClientID %>");
        var datamintiga = JSON.parse(HFdatamintiga.value);


        var HFminmaxAge = document.getElementById("<%= HF_BMI_MinMaxAge.ClientID %>");
        var minmaxbmiage = HFminmaxAge.value;
        var HFminmaxY = document.getElementById("<%= HF_BMI_MinMaxY.ClientID %>");
        var minmaxbmiY = HFminmaxY.value;

        return {
            type: 'scatter',
            data: {
                datasets: [
                    {
                        label: 'Data',
                        fill: false,
                        backgroundColor: 'black',
                        borderColor: 'black',
                        data: databmi,
                        //data: [
                        //    {
                        //        x: 2,
                        //        y: 0
                        //    },
                        //    {
                        //        x: 50,
                        //        y: 6
                        //    },
                        //    {
                        //        x: 70,
                        //        y: 8
                        //    }
                        //],
                        showLine: true,
                        pointBackgroundColor: 'black',
                        pointBorderColor: 'black'
                    },
                    {
                        label: '3',
                        fill: false,
                        backgroundColor: window.chartColors.grey,
                        borderColor: window.chartColors.grey,
                        data: datatiga,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '2',
                        fill: false,
                        backgroundColor: window.chartColors.red,
                        borderColor: window.chartColors.red,
                        data: datadua,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '1',
                        fill: false,
                        backgroundColor: window.chartColors.orange,
                        borderColor: window.chartColors.orange,
                        data: datasatu,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '0',
                        fill: false,
                        backgroundColor: window.chartColors.green,
                        borderColor: window.chartColors.green,
                        data: datanol,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-1',
                        fill: false,
                        backgroundColor: window.chartColors.orange,
                        borderColor: window.chartColors.orange,
                        data: dataminsatu,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-2',
                        fill: false,
                        backgroundColor: window.chartColors.red,
                        borderColor: window.chartColors.red,
                        data: datamindua,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-3',
                        fill: false,
                        backgroundColor: window.chartColors.grey,
                        borderColor: window.chartColors.grey,
                        data: datamintiga,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    }]
            },
            options: {
                title: {
                    display: false,
                    text: 'BMI Menurut Umur'
                },
                legend: {
                    display: true,
                    labels: {
                        usePointStyle: false
                    }
                },
                scales: {
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: minmaxbmiage.split(';')[3],
                        },
                        ticks: {
                            min: Number(minmaxbmiage.split(';')[0]),
                            max: Number(minmaxbmiage.split(';')[1]),
                            stepSize: Number(minmaxbmiage.split(';')[2]),
                            callback:
                                function (tick) {

                                    if (minmaxbmiage == "60;228;3;Age(completed months and years)") {
                                        if (tick % 12 != 0) {
                                            var remain = tick % 3;
                                            if (remain === 0) {
                                                if (tick <= 12) {
                                                    return [(tick / 3).toString(), ''];
                                                }
                                                else {
                                                    var basedata = tick - (~~(tick / 12) * 12);
                                                    if (basedata % 9 == 0) {
                                                        return '9';
                                                    }
                                                    else if (basedata % 6 == 0) {
                                                        return '6';
                                                    }
                                                    else if (basedata % 3 == 0) {
                                                        return '3';
                                                    }
                                                }
                                            }
                                        }
                                        else {
                                            var remain = tick % 12;
                                            if (remain === 0) {
                                                return ['', (tick / 12).toString()];
                                            }
                                        }
                                    }
                                    else {
                                        if (tick % 336 != 0) {
                                            var remain = tick % 28;
                                            if (remain === 0) {
                                                if (tick <= 336) {
                                                    return [(tick / 28).toString(), ''];
                                                }
                                                else {
                                                    var basedata = tick - (~~(tick / 336) * 336);
                                                    if (basedata % 308 == 0) {
                                                        return '11';
                                                    }
                                                    else if (basedata % 280 == 0) {
                                                        return '10';
                                                    }
                                                    else if (basedata % 252 == 0) {
                                                        return '9';
                                                    }
                                                    else if (basedata % 224 == 0) {
                                                        return '8';
                                                    }
                                                    else if (basedata % 196 == 0) {
                                                        return '7';
                                                    }
                                                    else if (basedata % 168 == 0) {
                                                        return '6';
                                                    }
                                                    else if (basedata % 140 == 0) {
                                                        return '5';
                                                    }
                                                    else if (basedata % 112 == 0) {
                                                        return '4';
                                                    }
                                                    else if (basedata % 84 == 0) {
                                                        return '3';
                                                    }
                                                    else if (basedata % 56 == 0) {
                                                        return '2';
                                                    }
                                                    else if (basedata % 28 == 0) {
                                                        return '1';
                                                    }
                                                }
                                            }
                                        }
                                        else {
                                            if (tick === 0) {
                                                return '';
                                            }
                                            var remain = tick % 336;
                                            if (remain === 0) {
                                                return ['', (tick / 336).toString()];
                                            }
                                        }
                                    }
                                    return '';
                                },
                        }
                    }],
                    yAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'BMI(kg/m2)'
                        },
                        ticks: {
                            min: Number(minmaxbmiY.split(';')[0]),
                            max: Number(minmaxbmiY.split(';')[1]),
                            stepSize: Number(minmaxbmiY.split(';')[2])
                        }
                    }]
                },
                responsive: true,
                maintainAspectRatio: false
            },

        };
    }

    function configKurvaLLU() {

        var HFdatallu = document.getElementById("<%= HF_LLU_Data.ClientID %>");
        var datallu = JSON.parse(HFdatallu.value);


        var HFdatanol = document.getElementById("<%= HF_LLU0.ClientID %>");
        var datanol = JSON.parse(HFdatanol.value);

        var HFdatasatu = document.getElementById("<%= HF_LLUplus1.ClientID %>");
        var datasatu = JSON.parse(HFdatasatu.value);

        var HFdataminsatu = document.getElementById("<%= HF_LLUminus1.ClientID %>");
        var dataminsatu = JSON.parse(HFdataminsatu.value);

        var HFdatadua = document.getElementById("<%= HF_LLUplus2.ClientID %>");
        var datadua = JSON.parse(HFdatadua.value);

        var HFdatamindua = document.getElementById("<%= HF_LLUminus2.ClientID %>");
        var datamindua = JSON.parse(HFdatamindua.value);

        var HFdatatiga = document.getElementById("<%= HF_LLUplus3.ClientID %>");
        var datatiga = JSON.parse(HFdatatiga.value);

        var HFdatamintiga = document.getElementById("<%= HF_LLUminus3.ClientID %>");
        var datamintiga = JSON.parse(HFdatamintiga.value);


        var HFminmaxAge = document.getElementById("<%= HF_LLU_MinMaxAge.ClientID %>");
        var minmaxlluage = HFminmaxAge.value;
        var HFminmaxY = document.getElementById("<%= HF_LLU_MinMaxY.ClientID %>");
        var minmaxlluY = HFminmaxY.value;

        return {
            type: 'scatter',
            data: {
                datasets: [
                    {
                        label: 'Data',
                        fill: false,
                        backgroundColor: 'black',
                        borderColor: 'black',
                        data: datallu,
                        //data: [
                        //    {
                        //        x: 2,
                        //        y: 0
                        //    },
                        //    {
                        //        x: 50,
                        //        y: 6
                        //    },
                        //    {
                        //        x: 70,
                        //        y: 8
                        //    }
                        //],
                        showLine: true,
                        pointBackgroundColor: 'black',
                        pointBorderColor: 'black'
                    },
                    {
                        label: '3',
                        fill: false,
                        backgroundColor: window.chartColors.grey,
                        borderColor: window.chartColors.grey,
                        data: datatiga,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '2',
                        fill: false,
                        backgroundColor: window.chartColors.red,
                        borderColor: window.chartColors.red,
                        data: datadua,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '1',
                        fill: false,
                        backgroundColor: window.chartColors.orange,
                        borderColor: window.chartColors.orange,
                        data: datasatu,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '0',
                        fill: false,
                        backgroundColor: window.chartColors.green,
                        borderColor: window.chartColors.green,
                        data: datanol,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-1',
                        fill: false,
                        backgroundColor: window.chartColors.orange,
                        borderColor: window.chartColors.orange,
                        data: dataminsatu,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-2',
                        fill: false,
                        backgroundColor: window.chartColors.red,
                        borderColor: window.chartColors.red,
                        data: datamindua,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    },
                    {
                        label: '-3',
                        fill: false,
                        backgroundColor: window.chartColors.grey,
                        borderColor: window.chartColors.grey,
                        data: datamintiga,
                        showLine: true,
                        pointBackgroundColor: 'transparent',
                        pointBorderColor: 'transparent'
                    }]
            },
            options: {
                title: {
                    display: false,
                    text: 'Lingkar Lengan Menurut Umur'
                },
                legend: {
                    display: true,
                    labels: {
                        usePointStyle: false
                    }
                },
                scales: {
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'Age(completed months and years)',
                        },
                        ticks: {
                            min: Number(minmaxlluage.split(';')[0]),
                            max: Number(minmaxlluage.split(';')[1]),
                            stepSize: Number(minmaxlluage.split(';')[2]),
                            callback:
                                function (tick) {

                                    if (tick % 336 != 0) {
                                        var remain = tick % 84;
                                        if (remain === 0) {
                                            if (tick <= 336) {
                                                //return [(tick / 84).toString(), ''];
                                                if (tick % 252 == 0) {
                                                    return '9';
                                                }
                                                else if (tick % 168 == 0) {
                                                    return '6';
                                                }
                                                else if (tick % 84 == 0) {
                                                    return '3';
                                                }
                                            }
                                            else {
                                                var basedata = tick - (~~(tick / 336) * 336);
                                                if (basedata % 252 == 0) {
                                                    return '9';
                                                }
                                                else if (basedata % 168 == 0) {
                                                    return '6';
                                                }
                                                else if (basedata % 84 == 0) {
                                                    return '3';
                                                }
                                            }
                                        }
                                    }
                                    else {
                                        var remain = tick % 336;
                                        if (remain === 0) {
                                            return ['', (tick / 336).toString()];
                                        }
                                    }
                                    return '';
                                },
                        }
                    }],
                    yAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'Arm Circumference(cm)'
                        },
                        ticks: {
                            min: Number(minmaxlluY.split(';')[0]),
                            max: Number(minmaxlluY.split(';')[1]),
                            stepSize: Number(minmaxlluY.split(';')[2])
                        }
                    }]
                },
                responsive: true,
                maintainAspectRatio: false
            },

        };
    }

    //window.onload = function () {
    //    var configBBU = configKurvaBBU();
    //    var ctx = document.getElementById('canvas').getContext('2d');
    //    window.myLine = new Chart(ctx, configBBU);
    //};

    $(document).ready(function () {

        //var configPBU = configKurvaPBU();
        //var ctxPBU = document.getElementById('canvasPBU').getContext('2d');
        //window.myLine = new Chart(ctxPBU, configPBU);

        //var configBBU = configKurvaBBU();
        //var ctxBBU = document.getElementById('canvasBBU').getContext('2d');
        //window.myLine = new Chart(ctxBBU, configBBU);

        //var configBBPB = configKurvaBBPB();
        //var ctxBBPB = document.getElementById('canvasBBPB').getContext('2d');
        //window.myLine = new Chart(ctxBBPB, configBBPB);

        //var configLKU = configKurvaLKU();
        //var ctxLKU = document.getElementById('canvasLKU').getContext('2d');
        //window.myLine = new Chart(ctxLKU, configLKU);

        //var configBMI = configKurvaBMI();
        //var ctxBMI = document.getElementById('canvasBMI').getContext('2d');
        //window.myLine = new Chart(ctxBMI, configBMI);

        //fungsi untuk menjaga style pada saat postback dalam updatepanel
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {

            prm.add_beginRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                }
            });

            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    var flagopenchart = document.getElementById("<%= HF_FlagChartTemplate.ClientID %>");
                    if (flagopenchart.value == "OPEN") {

                        var configPBU = configKurvaPBU();
                        var ctxPBU = document.getElementById('canvasPBU').getContext('2d');
                        window.myLine = new Chart(ctxPBU, configPBU);

                        var configBBU = configKurvaBBU();
                        var ctxBBU = document.getElementById('canvasBBU').getContext('2d');
                        window.myLine = new Chart(ctxBBU, configBBU);

                        var configBBPB = configKurvaBBPB();
                        var ctxBBPB = document.getElementById('canvasBBPB').getContext('2d');
                        window.myLine = new Chart(ctxBBPB, configBBPB);

                        var configLKU = configKurvaLKU();
                        var ctxLKU = document.getElementById('canvasLKU').getContext('2d');
                        window.myLine = new Chart(ctxLKU, configLKU);

                        var configBMI = configKurvaBMI();
                        var ctxBMI = document.getElementById('canvasBMI').getContext('2d');
                        window.myLine = new Chart(ctxBMI, configBMI);

                        var configLLU = configKurvaLLU();
                        var ctxLLU = document.getElementById('canvasLLU').getContext('2d');
                        window.myLine = new Chart(ctxLLU, configLLU);
                    }
                }
            });
        };
    });
</script>
