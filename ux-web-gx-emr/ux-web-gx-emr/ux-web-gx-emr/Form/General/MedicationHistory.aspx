<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MedicationHistory.aspx.cs" Inherits="Form_General_MedicationHistory" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/Form/General/Control/PatientCard.ascx" TagPrefix="uc1" TagName="PatientCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .btn-white {
            color: #000;
            background-color: #fff;
            border-color: #cdced9;
            padding: 3px;
            padding-left: 4px;
            font-size: 12px;
        }

        @media screen and (max-width: 1366px) {
            .shadows {
                border-radius: 10px;
                padding: 7px 7px 7px 7px;
                width: 100%;
                max-width: 1280px;
                border: 2px solid lightgrey;
            }
        }

        @media screen and (min-width: 1367px) {
            .shadows {
                border-radius: 10px;
                padding: 7px 7px 7px 7px;
                width: 100%;
                border: 2px solid lightgrey;
            }
        }

        .shadows-search {
            border: 1px;
            border-radius: 10px;
            box-shadow: 0px 1px 5px #9293A0;
            padding: 7px 7px 7px 7px;
            width: 200px;
        }

        .itemcontainersave > div {
            padding-top: 0px;
            padding-bottom: 2px;
        }
    </style>
    <script type="text/javascript">
        $(window).load(function () {
            $(".loadPage").fadeOut("slow");
        });

        function Open(content) {
            document.getElementById('<%=hfCompoundName.ClientID%>').value = content;
            document.getElementById('<%=btnSample.ClientID%>').click();
            openModal();
            return true;
        }

        function openModal() {
            $('#modalCompound').modal('show');
            return true;
        }

        $(window).scroll(function () {

            if ($(this).scrollTop() > 0) {
                $('.filterdata').fadeOut();
            }
            else {
                $('.filterdata').fadeIn();
            }
        });

        function topFunction() {
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
        }

        $('body').scroll(function (e) {
            if ($(this).scrollTop() > 250) {
                $("#myIDtoTop").attr('class', 'bottomMenuu showw');
            } else {
                $("#myIDtoTop").attr('class', 'bottomMenuu hidee');
            }
        });

        function switchBahasa() {
            var bahasa = document.getElementById('<%=HFisBahasa.ClientID%>').value;
            if (bahasa == "ENG") {
                if (document.getElementById('lblbhs_nodata') != null) {
                    document.getElementById('lblbhs_nodata').innerHTML = "Oops! There is no data";
                    document.getElementById('lblbhs_subnodata').innerHTML = "Please search another date or parameter";
                }

            }
            else if (bahasa == "IND") {
                if (document.getElementById('lblbhs_nodata') != null) {
                    document.getElementById('lblbhs_nodata').innerHTML = "Oops! Tidak ada data";
                    document.getElementById('lblbhs_subnodata').innerHTML = "Silakan cari tanggal atau parameter lain";
                }
            }
        }

         //fungsi event klik pada area diluar modal
        $(document).ready(function () {
            //fungsi untuk menjaga style saat postback dalam updatepanel

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        $('.selectpicker').selectpicker();   

                    }
                });
            };
        });

        function dateFrom() {
            var dp = $('#<%=TextBoxDateFrom.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

        function dateTo() {
            var dp = $('#<%=TextBoxDateTo.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

    </script>
    <asp:HiddenField ID="HFisBahasa" runat="server" />

    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UPmedicalHistory">
        <ContentTemplate>
            <asp:Panel ID="medical_history_panel" runat="server">
                <div class="container-fluid kartu-pasien">
                    <asp:HiddenField ID="hfPatientId" runat="server" />
                    <asp:HiddenField ID="hfEncounterId" runat="server" />
                    <asp:HiddenField ID="hfAdmissionId" runat="server" />
                    <asp:HiddenField ID="hfPageSoapId" runat="server" />
                    <asp:HiddenField ID="hfCompoundName" runat="server" />
                    <uc1:PatientCard runat="server" ID="PatientCard" />
                </div>

                <div style="width: 100%; background-color: white; min-height: calc(100vh - 125px);">

                    <%--<section id="main_page" style="background-color: red; margin-top: -150px; padding-top: 150px; margin-bottom: 75px"></section>--%>
                    <div class="filterdata btn-group btn-group-justified" style="display:none; background-color: whitesmoke; height: 70px; position: fixed; padding-left: 15px; padding-top: 15px;" role="group" aria-label="...">

                        <table border="0">
                            <tr>
                                <td></td>
                                <td>Search Drugs</td>
                                <td>&nbsp; </td>
                                <td>Encounter Times </td>
                                <td>&nbsp; </td>
                                <td>Search by admission or doctor </td>
                                <td>&nbsp; </td>
                                <td>&nbsp; </td>
                                <td>&nbsp; </td>
                                <td>&nbsp; </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                </td>
                                <td>
                                    <asp:DropDownList ID="dllOrganizationCode" runat="server" CssClass="selectpicker" OnTextChanged="dllOrganizationCode_OnTextChange" AutoPostBack="true"></asp:DropDownList>
                                </td>
                                <td>&nbsp; </td>
                                <td>
                                    <asp:DropDownList ID="ddlEncounterMode" runat="server" Height="25px" Width="130px" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none;">
                                        <asp:ListItem Text="Last Encounter" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Last 5 Encounter" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Last 10 Encounter" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Last 20 Encounter" Value="20"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 30px; text-align: center;">OR </td>
                                <td>
                                    <asp:DropDownList ID="ddldoctor" runat="server" Height="25px" Width="130px" CssClass="selectpicker greyItem" data-live-search="true" data-style="btn-white" data-size="7" data-width="200px" data-height="24px" data-dropup-auto="false" data-container="body" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none; z-index: -1;"></asp:DropDownList>
                                </td>
                                <td style="width: 20px;">&nbsp; </td>
                                <td>
                                    <asp:DropDownList ID="ddlDrugsX" runat="server" data-live-search="true" CssClass="form-control selectpicker" OnSelectedIndexChanged="ddlDrugs_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </td>
                                <td style="width: 20px;">&nbsp; </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Width="70px" Text="Search" CssClass="btn btn-emr-small btn-lightGreen" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>

                    </div>

                    <div style="padding-left: 15px; padding-top: 10px; padding-bottom:10px; background-color: whitesmoke;">
                        <table border="0">
                            <tr>
                                <td>
                                    From
                                </td>
                                <td>
                                    To
                                </td>
                                <td>
                                    Iter
                                </td>
                                <td rowspan="2" style="padding-left:20px;">
                                    <asp:Button ID="ButtonSearchMedication" runat="server" Text="Search" CssClass="btn btn-lightGreen" OnClick="ButtonSearchMedication_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right:10px;">
                                    <asp:TextBox ID="TextBoxDateFrom" runat="server" CssClass="form-control" onmousedown="dateFrom();" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                                <td style="padding-right:10px;">
                                    <asp:TextBox ID="TextBoxDateTo" runat="server" CssClass="form-control" onmousedown="dateTo();" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                                <td style="padding-right:10px;">
                                    <asp:CheckBox ID="CheckBoxIter" runat="server" />
                                </td>
                                
                            </tr>
                        </table>
                    </div>
                    

                    <%-- ========================================================== button floating ================================================================ --%>
                    <div style="position: fixed; right: 10px; margin-right:20px; top: 23%; transform: translate(0,-50%); text-align: left; width:320px">
                        <div>
                            <asp:DropDownList data-live-search="true" runat="server" ID="ddlDrugs" OnSelectedIndexChanged="ddlDrugs_SelectedIndexChanged" 
                                CssClass="form-control selectpicker" style="width:100%; background-color:#ffffff" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div id="img_noData" runat="server" visible="false" style="text-align: center; padding-top: 6%;">
                        <div>
                            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                        </div>
                        <div runat="server" id="no_patient_data">
                            <span>
                                <h3 style="font-weight: 700; color: #585A6F">
                                    <%--<label style="display: <%=setENG%>;">Oops! There is no data </label>
                                    <label style="display: <%=setIND%>;">Oops! Tidak ada data </label>--%>
                                    <label id="lblbhs_nodata">Oops! There is no data </label>
                                </h3>
                            </span>
                            <span style="font-size: 14px; color: #585A6F">
                                <%--<label style="display: <%=setENG%>;">Please search another date or parameter </label>
                                <label style="display: <%=setIND%>;">Silakan cari tanggal atau parameter lain </label>--%>
                                <label id="lblbhs_subnodata">Please search another date or parameter </label>
                            </span>
                        </div>
                    </div>

                    <div style="width: 100%; padding-top:20px;">
                        <div runat="server" id="prescription" role="group" style="overflow: auto"></div>
                    </div>
                </div>

                <!-- buat jaga2 -->
                <div class="teta" style="height: 175px; display: none;"></div>
                <asp:GridView ID="gvw_history" runat="server" Visible="false" EmptyDataText="No Data">
                </asp:GridView>
                <!-- end buat jaga2 -->

            </asp:Panel>

            <a class="item" href="javascript:topFunction();">
                <div id="myIDtoTop" class="bottomMenuu hidee">
                    <span>
                        <img src="../../Images/Result/ic_Arrow_Top.png" /></span>
                </div>
            </a>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="modalCompound" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel ID="UPCompound" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="modal-dialog" style="width: 70%;">
                    <div class="modal-content" style="border-radius: 7px; height: 100%;">
                        <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" style="text-align: left">
                                <asp:Button runat="server" ID="btnSample" Text="" CssClass="hidden" OnClick="compoundDetail_Click" />
                                <asp:Label ID="headerCompound" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server"></asp:Label></h4>
                        </div>
                        <div class="btn-group btn-group-justified" style="width: 100%; background-color: lightgrey" role="group" aria-label="...">
                            <div class="btn-group container-fluid" role="group" style="width: 3%">
                                <div>
                                    <label>Order Set Name</label>
                                </div>
                                <div style="font-weight: bold">
                                    <asp:Label runat="server" ReadOnly="true" ID="orderSetName" Width="100%"></asp:Label>
                                </div>
                            </div>

                            <div class="btn-group" role="group" style="width: 1%">
                                <div>
                                    <label>Qty</label>
                                </div>
                                <div style="font-weight: bold">
                                    <asp:Label runat="server" ReadOnly="true" ID="qtyOrderSetName"></asp:Label>
                                </div>
                            </div>

                            <div class="btn-group" role="group">
                                <div>
                                    <label>U.O.M.</label>
                                </div>
                                <div style="font-weight: bold">
                                    <asp:Label runat="server" ReadOnly="true" ID="uomOrderSetName"></asp:Label>
                                </div>
                            </div>

                            <div class="btn-group" role="group">
                                <div>
                                    <label>Frequency</label>
                                </div>
                                <div style="font-weight: bold">
                                    <asp:Label runat="server" ReadOnly="true" ID="frequencyOrderSetName"></asp:Label>
                                </div>
                            </div>

                            <div class="btn-group" role="group">
                                <div>
                                    <label>Dose</label>
                                </div>
                                <div style="font-weight: bold">
                                    <asp:Label runat="server" ReadOnly="true" ID="doseOrderSetName"></asp:Label>
                                </div>
                            </div>

                            <div class="btn-group" role="group">
                                <div>
                                    <label>Dose Text</label>
                                </div>
                                <div style="font-weight: bold">
                                    <asp:Label runat="server" ReadOnly="true" ID="dose_textOrderSetName"></asp:Label>
                                </div>
                            </div>

                            <div class="btn-group" role="group">
                                <div>
                                    <label>Instruction</label>
                                </div>
                                <div style="font-weight: bold">
                                    <asp:Label runat="server" ReadOnly="true" ID="instructionOrderSetName"></asp:Label>
                                </div>
                            </div>

                            <div class="btn-group" role="group">
                                <div>
                                    <label>Route</label>
                                </div>
                                <div style="font-weight: bold">
                                    <asp:Label runat="server" ReadOnly="true" ID="routeOrderSetName"></asp:Label>
                                </div>
                            </div>

                            <div class="btn-group" role="group">
                                <div>
                                    <label>Iter</label>
                                </div>
                                <div style="font-weight: bold">
                                    <asp:Label runat="server" ReadOnly="true" ID="iterOrderSetName"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div style="width: 100%; background-color: white">
                                <asp:GridView ID="gvw_detail_compound" runat="server" CssClass="table table-striped table-condensed" BorderColor="Transparent" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="itemName" Wrap="true" Style="resize: none" BackColor="Transparent" Width="400px" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("itemName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Oty" Wrap="true" Style="resize: none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("quantity") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="U.O.M." HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="uom" Wrap="true" Style="resize: none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("uom") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dose Text" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="dose_text" Wrap="true" Style="resize: none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("doseText") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Instruction" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="instruction" Wrap="true" Style="resize: none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("instruction") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>