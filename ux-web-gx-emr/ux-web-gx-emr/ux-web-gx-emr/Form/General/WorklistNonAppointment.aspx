<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="WorklistNonAppointment.aspx.cs" Inherits="Form_General_WorklistNonAppointment" %>

<%@ Register Src="~/Form/General/Control/PatientCard.ascx" TagPrefix="tag1" TagName="PatientCard" %>
<%@ Register Src="~/Form/General/Control/StdLabResult.ascx" TagPrefix="tag1" TagName="StdLabResult" %>
<%@ Register Src="~/Form/General/Control/StdRadResult.ascx" TagPrefix="tag1" TagName="StdRadResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript">

        $(window).load(function () {
            $(".loadPage").fadeOut("slow");
        });

        function dateStart() {
            var dp = $('#<%=DateTextboxStart.ClientID%>');
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
        function dateEnd() {
            var dp = $('#<%=DateTextboxEnd.ClientID%>');
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

        function openLabResultModal(ticket, patient, admission) {
            var hdnFieldAdmm = document.getElementById("<%=hf_admiss_id.ClientID %>");
            var hdnFieldPatient = document.getElementById("<%=hf_patient_id.ClientID %>");
            var hdnFieldTicket = document.getElementById("<%=hf_ticket_patient.ClientID %>");
            hdnFieldAdmm.value = admission;
            hdnFieldPatient.value = patient;
            hdnFieldTicket.value = ticket;

            document.getElementById("<%=HiddenLabMark.ClientID %>").value = 1;
        }

        function openRadResultModal(ticket, patient, admission) {
            var hdnFieldAdmm = document.getElementById("<%=hf_admiss_id.ClientID %>");
            var hdnFieldPatient = document.getElementById("<%=hf_patient_id.ClientID %>");
            var hdnFieldTicket = document.getElementById("<%=hf_ticket_patient.ClientID %>");

            hdnFieldAdmm.value = admission;
            hdnFieldPatient.value = patient;
            hdnFieldTicket.value = ticket;

            document.getElementById("<%=HiddenRadMark.ClientID %>").value = 1;
        }

        function openLabModal() {
            $('#laboratoryResult').modal('show');
            switchBahasaPC();
        }

        function openRadModal() {
            $('#radiologyResult').modal('show');
            switchBahasaPC();
        }

        function topFunction() {
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
        }

        $(document).ready(function () {

            var dp = $('#<%=DateTextboxStart.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });

            var dp = $('#<%=DateTextboxEnd.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });

            setBorderPencarian();

            //fungsi untuk action in postback updatepanel
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {

                prm.add_beginRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {

                        if (document.getElementById("<%=HiddenLabMark.ClientID %>").value != 1) {
                            gridWorklist.style.display = "none";
                        }
                    }
                });

                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {

                        gridWorklist.style.display = "";
                        document.getElementById("<%=HiddenLabMark.ClientID %>").value = 0;
                        document.getElementById("<%=HiddenRadMark.ClientID %>").value = 0;

                        //set bahasa after end postback
                        //switchBahasa();

                        setBorderPencarian();
                        setInterval(keepRefreshWLNA, 120000); //2 minute
                    }
                });
            };
        });

        function setBorderPencarian() {
            var namesearch = document.getElementById("<%=MRsearch.ClientID %>");
            if (namesearch.value.length > 0) {
                namesearch.style.border = "2px solid #356c9b";
            }
            else {
                namesearch.style.border = "1px solid #cdced9"
            }
        }

        function switchBahasaPC() {
            var bahasa = document.getElementById('<%=HFisBahasa.ClientID%>').value;
            if (bahasa == "ENG") {
                document.getElementById('lblbhs_pcadmissionno').innerHTML = "Admission No.";
                document.getElementById('lblbhs_pcdob').innerHTML = "DOB";
                document.getElementById('lblbhs_pcage').innerHTML = "Age";
                document.getElementById('lblbhs_pcreligion').innerHTML = "Religion";
                document.getElementById('lblbhs_pcpayer').innerHTML = "Payer";
            }
            else if (bahasa == "IND") {
                document.getElementById('lblbhs_pcadmissionno').innerHTML = "No. Admisi";
                document.getElementById('lblbhs_pcdob').innerHTML = "Tgl. Lahir";
                document.getElementById('lblbhs_pcage').innerHTML = "Umur";
                document.getElementById('lblbhs_pcreligion').innerHTML = "Agama";
                document.getElementById('lblbhs_pcpayer').innerHTML = "Penanggung";
            }
        }

        function switchBahasa() {
            var bahasa = document.getElementById('<%=HFisBahasa.ClientID%>').value;
            if (bahasa == "ENG") {
                document.getElementById('lblbhs_searchdate').innerHTML = "Search Date";
                document.getElementById('lblbhs_mrno').innerHTML = "MR No. or patient name";
                document.getElementById('lblbhs_mrstatus').innerHTML = "MR Status";
                document.getElementById('lblbhs_patienttotal').innerHTML = "Patient Total";
                document.getElementById('lblbhs_patientlist').innerHTML = "Patient List";
                document.getElementById('lblbhs_show').innerHTML = "Show :";
                document.getElementById('lblbhs_nopatient').innerHTML = "No patient yet, today";
                document.getElementById('lblbhs_subnopatient').innerHTML = "Please wait some more time or search another date";
                document.getElementById('lblbhs_nodata').innerHTML = "Oops! There is no data";
                document.getElementById('lblbhs_subnodata').innerHTML = "Please search another date or parameter";
                document.getElementById('lblbhs_nointernet').innerHTML = "No internet connection";
                document.getElementById('lblbhs_subnointernet').innerHTML = "Please check your connection & refresh";

                var table = document.getElementById("<%=gvw_data.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    headers[0].innerText = "Patient Name";
                    headers[2].innerText = "Queue";
                    headers[4].innerText = "Age";
                    headers[5].innerText = "Sex";
                    headers[6].innerText = "MR No.";
                    headers[7].innerText = "Admission Date";
                    headers[8].innerText = "Payer";
                    headers[9].innerText = "Admission Status";
                }
            }
            else if (bahasa == "IND") {
                document.getElementById('lblbhs_searchdate').innerHTML = "Tanggal Pencarian";
                document.getElementById('lblbhs_mrno').innerHTML = "No. MR atau nama pasien";
                document.getElementById('lblbhs_mrstatus').innerHTML = "Status MR";
                document.getElementById('lblbhs_patienttotal').innerHTML = "Total Pasien";
                document.getElementById('lblbhs_patientlist').innerHTML = "Daftar Pasien";
                document.getElementById('lblbhs_show').innerHTML = "Tampilkan :";
                document.getElementById('lblbhs_nopatient').innerHTML = "Belum ada pasien, hari ini";
                document.getElementById('lblbhs_subnopatient').innerHTML = "Harap tunggu beberapa saat lagi atau cari tanggal lain";
                document.getElementById('lblbhs_nodata').innerHTML = "Oops! Tidak ada data";
                document.getElementById('lblbhs_subnodata').innerHTML = "Silakan cari tanggal atau parameter lain";
                document.getElementById('lblbhs_nointernet').innerHTML = "Tidak ada koneksi internet";
                document.getElementById('lblbhs_subnointernet').innerHTML = "Silakan periksa koneksi Anda & refresh";

                var table = document.getElementById("<%=gvw_data.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    headers[0].innerText = "Nama Pasien";
                    headers[2].innerText = "Antrian";
                    headers[4].innerText = "Umur";
                    headers[5].innerText = "Seks";
                    headers[6].innerText = "No. MR";
                    headers[7].innerText = "Tgl. Admisi";
                    headers[8].innerText = "Penanggung";
                    headers[9].innerText = "Status Admisi";
                }
            }
        }

        function showLoad() {
            $(".loadPage").show();
        }

        function keepRefreshWLNA() {
            RefreshCountNotifOT();
        }

        setInterval(keepRefreshWLNA, 120000); //2 minute

        function warningnotificationOption() {
            toastr.options.positionClass = "toast-top-right";
            toastr.options.closeButton = true;
            toastr.options.timeOut = 0;
            toastr.options.extendedTimeOut = 0;
            toastr.options.tapToDismiss = true;
        }

        function warningnotification(msg) {
            warningnotificationOption();
            toastr.warning('Please change your password!' + ' <br /> <button type="button" class="btn btn-danger btn-sm" style="height: 25px; padding-top: 3px; width: 55px; float:right;">OK</button>', msg);
        }

    </script>

    <style type="text/css">
        .select {
            height: 25px;
            font-size: 11px;
            padding-top: 0px;
            width: 80px;
            padding-left: 2px;
        }
    </style>

    <body>
        <asp:HiddenField ID="HFisBahasa" runat="server" />

        <asp:UpdatePanel ID="upError" runat="server">
            <ContentTemplate>

                <div style="background-color: #E7E8ED; padding-top: 6px; padding-bottom: 5px;">

                    <div class="row" style="margin-left: 30px; margin-right: 15px;">
                        <div class="col-sm-2" style="padding-left: 0px; padding-right: 0px; padding-top: 15px;">

                            <div class="pretty p-default p-round">
                                <asp:RadioButton runat="server" GroupName="admtype" ID="opd" value="1" AutoPostBack="true" OnCheckedChanged="AdmTypeChanges" />
                                <div class="state p-primary-o">
                                    <label>OPD</label>
                                </div>
                            </div>

                            <div class="pretty p-default p-round">
                                <asp:RadioButton runat="server" GroupName="admtype" ID="ed" Value="3" AutoPostBack="true" OnCheckedChanged="AdmTypeChanges" />
                                <div class="state p-primary-o">
                                    <label>ED</label>
                                </div>
                            </div>

                            <div class="pretty p-default p-round">
                                <input type="radio" groupname="admtype" id="ipd" value="2" disabled />
                                <div class="state p-primary-o">
                                    <label>IPD</label>
                                </div>
                            </div>

                        </div>

                        <div class="col-sm-7" style="text-align: -webkit-center;">
                            <table style="width: 100%;" border="0">
                                <tr>
                                    <td>
                                        <%--<label style="display: <%=setENG%>;">Search Date </label>
                                        <label style="display: <%=setIND%>;">Tanggal Pencarian </label>--%>
                                        <label id="lblbhs_searchdate">Search Date </label>
                                    </td>
                                    <td>&nbsp; </td>
                                    <td>&nbsp; </td>
                                    <td>&nbsp; </td>
                                    <td>
                                        <%--<label style="display: <%=setENG%>;">MR No. or patient name  </label>
                                        <label style="display: <%=setIND%>;">No. MR atau nama pasien </label>--%>
                                        <label id="lblbhs_mrno">MR No. or patient name  </label>
                                    </td>
                                    <td>&nbsp; </td>
                                    <td>
                                        <%--<label style="display: <%=setENG%>;">MR Status  </label>
                                        <label style="display: <%=setIND%>;">Status MR </label>--%>
                                        <label id="lblbhs_mrstatus">MR Status  </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox class="form-control" runat="server" ID="DateTextboxStart" name="date" Style="height: 25px; font-size: 12px; background-color: white;" placeholder="dd/mm/yyyy" OnTextChanged="DateTextboxStart_TextChanged" onmousedown="dateStart();" AutoPostBack="true" AutoCompleteType="Disabled" />
                                    </td>
                                    <td style="width: 20px; text-align: center">- </td>
                                    <td>
                                        <asp:TextBox class="form-control" runat="server" ID="DateTextboxEnd" name="date" Style="height: 25px; font-size: 12px; background-color: white;" placeholder="dd/mm/yyyy" OnTextChanged="DateTextboxEnd_TextChanged" onmousedown="dateEnd ();" AutoPostBack="true" AutoCompleteType="Disabled" />
                                    </td>
                                    <td style="width: 15px;">&nbsp; </td>
                                    <td>
                                        <asp:TextBox class="form-control" runat="server" ID="MRsearch" name="MRsearch" Style="height: 25px; font-size: 12px" type="text" placeholder="MR or patient name" onkeyup="setBorderPencarian();" OnTextChanged="MRSearch_TextChanged" AutoPostBack="true" />
                                    </td>
                                    <td style="width: 15px;">&nbsp; </td>
                                    <td>
                                        <asp:DropDownList ID="status" runat="server" class="form-control" Font-Size="12px" CssClass="select" OnSelectedIndexChanged="Status_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="New" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Draft" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Submit" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Re-Open" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Cancel" Value="6"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="col-sm-3">
                            <div class="row text-right">

                                <div class="col-sm-3" style="text-align: right; padding-left: 0px; margin-left: 0px; padding-right: 0px; margin-right: 8px;">
                                    <div style="font-size: 12px;" class="badge">
                                        <%--<label style="display: <%=setENG%>;">Patient Total </label>
                                        <label style="display: <%=setIND%>;">Total Pasien </label>--%>
                                        <label id="lblbhs_patienttotal">Patient Total </label>
                                    </div>
                                    <br />
                                    <asp:Label ID="patientotal" runat="server" Style="font-size: 22px; padding-right: 2px;" Font-Bold="true" />
                                </div>
                                <div class="col-sm-3" style="text-align: right; padding-left: 0px; margin-left: 0px; padding-right: 0px; margin-right: -10px;">
                                    <div style="font-size: 12px; background-color: #356c9b;" class="badge">Check In</div>
                                    <br />
                                    <asp:Label ID="checkin" runat="server" Style="font-size: 22px; padding-right: 2px;" Font-Bold="true" />
                                </div>
                                <div class="col-sm-3" style="text-align: right; padding-left: 0px; margin-left: 0px; padding-right: 0px; margin-right: 0px;">
                                    <div style="font-size: 12px; background-color: #4D9B35;" class="badge">Submit</div>
                                    <br />
                                    <asp:Label ID="lblsubmitcount" runat="server" Style="font-size: 22px; padding-right: 2px;" Font-Bold="true" />
                                </div>
                                <div class="col-sm-1" style="text-align: right; padding-left: 0px; margin-left: 0px; padding-right: 0px; margin-right: 0px; display: none;">
                                    <div style="font-size: 12px;" class="badge">Not Check In</div>
                                    <asp:Label ID="notcheckin" runat="server" Style="font-size: 22px;" Font-Bold="true" />
                                </div>
                                <div class="col-sm-3" style="text-align: right; padding-left: 0px; margin-left: 0px; padding-right: 15px; margin-right: 0px;">
                                    <div style="font-size: 12px; background-color: #C43D32;" class="badge">Cancel</div>
                                    <br />
                                    <asp:Label ID="totalcancel" runat="server" Style="font-size: 22px; padding-right: 2px;" Font-Bold="true" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <div style="background-color: transparent; height: 95%">
                    &nbsp;
                    <div style="background-color: white; width: 96%; height: 95%; border-radius: 7px 7px 0px 0px; padding-top: 10px; padding-bottom: 10px;" class="container-fluid">

                        <asp:HiddenField runat="server" ID="hfOrgID" />
                        <asp:HiddenField runat="server" ID="hf_admiss_id" />
                        <asp:HiddenField runat="server" ID="hf_patient_id" />
                        <asp:HiddenField runat="server" ID="hf_ticket_patient" />
                        <div class="row">
                            <div class="col-sm-6">
                                <b>
                                    <%--<label style="display: <%=setENG%>;">Patient List </label>
                                    <label style="display: <%=setIND%>;">Daftar Pasien </label>--%>
                                    <label id="lblbhs_patientlist">Patient List </label>
                                </b>
                                &nbsp; | &nbsp;
                                <asp:Label runat="server">
                                    <%--<label style="display: <%=setENG%>;"> Show : </label>
                                    <label style="display: <%=setIND%>;"> Tampilkan : </label>--%>
                                    <label id="lblbhs_show"> Show : </label>
                                </asp:Label>
                                <asp:RadioButtonList runat="server" Style="float: right; margin-top: -4px; margin-left: 5px;" ID="showPaging" RepeatDirection="Horizontal" RepeatLayout="Table" AutoPostBack="true" OnSelectedIndexChanged="showPaging_SelectedIndexChanged" Visible="false">
                                    <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                </asp:RadioButtonList>

                                <div class="pretty p-default p-round" style="display: none;">
                                    <asp:RadioButton runat="server" GroupName="admtypeX" ID="showPaging1" AutoPostBack="true" OnCheckedChanged="showPaging_CheckedChanged" />
                                    <div class="state p-primary-o">
                                        <label>All</label>
                                        <asp:HiddenField ID="hdn_paging1" Value="0" runat="server" />
                                    </div>
                                </div>

                                <div class="pretty p-default p-round" style="display: none;">
                                    <asp:RadioButton runat="server" GroupName="admtypeX" ID="showPaging50" AutoPostBack="true" OnCheckedChanged="showPaging_CheckedChanged" />
                                    <div class="state p-primary-o">
                                        <label>50</label>
                                        <asp:HiddenField ID="hdn_paging50" Value="50" runat="server" />
                                    </div>
                                </div>

                                <div class="pretty p-default p-round">
                                    <asp:RadioButton runat="server" GroupName="admtypeX" ID="showPaging25" AutoPostBack="true" OnCheckedChanged="showPaging_CheckedChanged" />
                                    <div class="state p-primary-o">
                                        <label>25</label>
                                        <asp:HiddenField ID="hdn_paging25" Value="25" runat="server" />
                                    </div>
                                </div>

                                <div class="pretty p-default p-round">
                                    <asp:RadioButton runat="server" GroupName="admtypeX" ID="showPaging15" AutoPostBack="true" OnCheckedChanged="showPaging_CheckedChanged" />
                                    <div class="state p-primary-o">
                                        <label>15</label>
                                        <asp:HiddenField ID="hdn_paging15" Value="15" runat="server" />
                                    </div>
                                </div>

                                 <div class="pretty p-default p-round">
                                    <asp:RadioButton runat="server" GroupName="admtypeX" ID="showPaging10" AutoPostBack="true" OnCheckedChanged="showPaging_CheckedChanged" />
                                    <div class="state p-primary-o">
                                        <label>10</label>
                                        <asp:HiddenField ID="hdn_paging10" Value="10" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 text-right">
                                <b>
                                    <asp:HyperLink ID="HyperLinkMysiloam" Target="_blank" runat="server" Style="text-decoration: underline;">Go to Appointment MySiloam</asp:HyperLink>
                                </b>
                            </div>
                        </div>

                    </div>
                    <div style="background-color: white; text-align: center; width: 96%; min-height: calc(100vh - 180px); transform: translate(0,-0%); border-radius: 0 0 6px 6px; margin-bottom: 15px;" class="container-fluid">

                        <asp:UpdateProgress ID="uProgWorklist" runat="server" AssociatedUpdatePanelID="upError">
                            <ProgressTemplate>
                                <!-- loading v1 -->
                                <%--<div class="loading-bar"></div>--%>

                                <!-- loading v2 -->
                                <div style="background-color: white; text-align: center; z-index: 5; position: fixed; width: 100%; left: 0px; height: calc(100vh - 200px); border-radius: 6px;">
                                    <div style="margin-top: 100px;">
                                        <img alt="" height="225px" width="225px" style="background-color: transparent; vertical-align: middle;" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
                                    </div>
                                </div>

                                <!-- loading v3 -->
                                <%--<div class="load-bar" style="width: 100%"> 
                                    <div class="bar"></div>
                                    <div class="bar"></div>
                                    <div class="bar"></div>
                                </div>--%>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                        <div id="img_noData" runat="server" style="display: none;">
                            <div>
                                <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 100px" />
                            </div>
                            <div runat="server" id="no_patient_today" style="display: none;">
                                <span>
                                    <h3 style="font-weight: 700; color: #585A6F">
                                        <%--<label style="display: <%=setENG%>;">No patient yet, today </label>
                                        <label style="display: <%=setIND%>;">Belum ada pasien, hari ini </label>--%>
                                        <label id="lblbhs_nopatient">No patient yet, today </label>
                                    </h3>
                                </span>
                                <span style="font-size: 14px; color: #585A6F">
                                    <%--<label style="display: <%=setENG%>;">Please wait some more time or search another date </label>
                                    <label style="display: <%=setIND%>;">Harap tunggu beberapa saat lagi atau cari tanggal lain </label>--%>
                                    <label id="lblbhs_subnopatient">Please wait some more time or search another date </label>
                                </span>
                            </div>
                            <div runat="server" id="no_patient_data" style="display: none;">
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
                        <div id="img_noConnection" runat="server" style="display: none;">
                            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noConnection.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px;" />
                            <span>
                                <h3 style="font-weight: 700; color: #585A6F">
                                    <%--<label style="display: <%=setENG%>;">No internet connection </label>
                                    <label style="display: <%=setIND%>;">Tidak ada koneksi internet </label>--%>
                                    <label id="lblbhs_nointernet">No internet connection </label>
                                </h3>
                            </span>
                            <span style="font-size: 14px; color: #585A6F">
                                <%--<label style="display: <%=setENG%>;">Please check your connection & refresh </label>
                                <label style="display: <%=setIND%>;">Silakan periksa koneksi Anda & refresh </label>--%>
                                <label id="lblbhs_subnointernet">Please check your connection & refresh </label>
                            </span>
                        </div>

                        <div id="gridWorklist">
                            <asp:HiddenField ID="HiddenLabMark" runat="server" />
                            <asp:HiddenField ID="HiddenRadMark" runat="server" />
                            <asp:HiddenField ID="HiddenPageIndex" runat="server" />
                            <asp:GridView ID="gvw_data" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-hover-worklist" BorderColor="#cdd2dd"
                                AllowPaging="True" PageSize="10" HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvw_data_RowDataBound"
                                ShowHeaderWhenEmpty="True" DataKeyNames="PatientId" EmptyDataText="No Data"
                                AllowSorting="True" OnPageIndexChanging="grv_PageIndexChanging">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Patient Name" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#CDD2DD">
                                        <%--<HeaderTemplate>
                                            <label style="display: <%=setENG%>;">Patient Name </label>
                                            <label style="display: <%=setIND%>;">Nama Pasien </label>
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="linkPatient" runat="server" Visible="false" Text='<%# Bind("AdmissionId") %>'><%# Eval("AdmissionId").ToString() %></asp:Label>
                                            <asp:Label ID="patientID_" runat="server" Visible="false" Text='<%# Bind("patientId") %>'></asp:Label>
                                            <asp:Label ID="tiketID_" runat="server" Visible="false" Text='<%# Bind("encounterId") %>'></asp:Label>
                                            <asp:Label ID="Status_" runat="server" Visible="false" Text='<%# Bind("Status") %>'></asp:Label>

                                            <asp:HyperLink ID="lblAdmissionId" runat="server" NavigateUrl='<%# String.Format("~/Form/General/PatientDashboard.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Guid.Empty.ToString()) %>'> <label style="font-weight:bold; text-decoration:underline; cursor:pointer;" onclick="showLoad();">  <%# Eval("PatientName") %>  </label> </asp:HyperLink>
                                            <%--<a href="<%=ResolveUrl("~/Form/General/PatientDetail.aspx?idPatient="<%= Eval("admission_id").ToString() %>")%>">
                                            <asp:Label runat="server"><%# Eval("patient_name").ToString() %></asp:Label>
                                            </a>--%>

                                            <i id="iconKewaspadaan" title="kewaspadaan: AirBone,Kontak, Droplet" runat="server" class="fa fa-exclamation-circle" style='<%# (Eval("Gender").ToString().ToUpper() == "F" ? "display:inline-block; color:#FBC531; font-size:14px": "display:none") %>'></i>

                                            <asp:Image Width="20px" Height="10px" ImageUrl="~/Images/Icon/vip.png" runat="server" Visible='<%# Eval("isVIP").ToString() != "False" %>' />
                                            <asp:Image Width="20px" Height="10px" ImageUrl="~/Images/Worklist/ic_New.png" runat="server" Visible='<%# Eval("IsNew").ToString() != "False" %>' />
                                            <asp:Image style="width:25px; margin-right:-5px;" ToolTip="Fall Risk" ImageUrl="~/Images/Icon/ic_Jatuh_sticker.svg" runat="server" Visible='<%# Eval("IsFAL").ToString() != "0" %>' />
                                            <asp:Image style="width:25px; margin-right:-5px;" ToolTip="Allergy" ImageUrl="~/Images/Icon/ic_Allergy_sticker.svg" runat="server" Visible='<%# Eval("IsALG").ToString() != "0" %>' />
                                            <asp:Image style="width:25px; margin-right:-5px;" ToolTip="Hepatitis B" ImageUrl="~/Images/Icon/ic_HepB_sticker.svg" runat="server" Visible='<%# Eval("IsHBS").ToString() != "0" %>' />
                                            <asp:Image style="width:25px; margin-right:-5px;" ToolTip="Hepatitis C" ImageUrl="~/Images/Icon/ic_HepC_sticker.svg" runat="server" Visible='<%# Eval("IsHCS").ToString() != "0" %>' />
                                            <asp:Image style="width:25px; margin-right:-5px;" ToolTip="TBC" ImageUrl="~/Images/Icon/ic_TBC_sticker.svg" runat="server" Visible='<%# Eval("IsTBC").ToString() != "0" %>' />
                                            <asp:Image style="width:25px; margin-right:-5px;" ToolTip="HIV/AIDS" ImageUrl="~/Images/Icon/ic_HAD_sticker.svg" runat="server" Visible='<%# Eval("IsHAD").ToString() != "0" %>' />
                                            <asp:Image style="width:25px; margin-right:-5px;" ToolTip="Peri Natal Resiko Tinggi" ImageUrl="~/Images/Icon/ic_PRT_sticker.svg" runat="server" Visible='<%# Eval("IsPRT").ToString() != "0" %>' />
                                            <asp:Image style="width:25px; margin-right:-5px;" ToolTip="Rhesus Negatif" ImageUrl="~/Images/Icon/ic_RHN_sticker.svg" runat="server" Visible='<%# Eval("IsRHN").ToString() != "0" %>' />
                                            <asp:Image style="width:25px; margin-right:-5px;" ToolTip="MRSA" ImageUrl="~/Images/Icon/ic_MRS_sticker.svg" runat="server" Visible='<%# Eval("IsMRS").ToString() != "0" %>' />
                                            <asp:Image style="width:25px; margin-right:-5px;" ToolTip="Covid 19" ImageUrl="~/Images/Icon/ic_COVID_sticker.svg" runat="server" Visible='<%# Eval("IsCOVID").ToString() != "0" %>' />
                                            <asp:Image style="width:25px; margin-right:-5px;" ToolTip="Covid 19 Vaccine" ImageUrl="~/Images/Icon/ic_CVVaccine_sticker.svg" runat="server" Visible='<%# Eval("IsCovidVac").ToString() != "0" %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="PatientId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="PatientId" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" HeaderStyle-BorderColor="#CDD2DD"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Queue" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#CDD2DD" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQueue" runat="server" Text='<%# Bind("QueueNo") %>'></asp:Label>
                                            <asp:ImageButton ID="queueCall" Width="20px" Height="20px" style="margin-left:5px; margin-right:5px; vertical-align:top; float:right;" ToolTip="Call Patient" ImageUrl="~/Images/Icon/ic_callE.svg" runat="server" Visible='<%# (bool)Eval("IsFinish") == true ? false : true %>' OnClick="queueCall_Click" />
                                            <asp:ImageButton Width="20px" Height="20px"  style="margin-left:5px; margin-right:5px; vertical-align:top; float:right;" ToolTip="Call Patient" ImageUrl="~/Images/Icon/ic_callD.svg" runat="server" Enabled="false" Visible='<%# Eval("IsFinish") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="6%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="#CDD2DD">
                                        <ItemTemplate>
                                            <%--<asp:ImageButton ID="labResult" Width="20px" Height="20px" ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labE.png" runat="server" Visible='<%# Eval("isLab").ToString() != "0" %>' OnClick="labResult_Click" OnClientClick="openLabResultModal();"/>--%>
                                            <asp:ImageButton ID="labResult" Width="20px" Height="20px" ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labE.png" runat="server" Visible='<%# Eval("isLab").ToString() != "0" %>' OnClick="labResult_Click" OnClientClick='<%# "openLabResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                            <asp:ImageButton Width="20px" Height="20px" ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labD.png" runat="server" Enabled="false" Visible='<%# Eval("isLab").ToString() != "1" %>' />
                                            <asp:ImageButton ID="radResult" Width="20px" Height="20px" ToolTip="View Radiology" ImageUrl="~/Images/Icon/radE.png" runat="server" Visible='<%# Eval("isRad").ToString() != "0" %>' OnClick="radResult_Click" OnClientClick='<%# "openRadResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                            <asp:ImageButton Width="20px" Height="20px" ToolTip="View Radiology" ImageUrl="~/Images/Icon/radD.png" runat="server" Enabled="false" Visible='<%# Eval("isRad").ToString() != "1" %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- <asp:BoundField HeaderText="Age" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" DataField="Age"></asp:BoundField>
                                    <asp:BoundField HeaderText="Sex" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" DataField="Gender"></asp:BoundField>
                                    <asp:BoundField HeaderText="MR No." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" DataField="LocalMrNo"></asp:BoundField>
                                    <asp:BoundField HeaderText="Admission Date" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" DataField="AdmissionDate" DataFormatString="{0:dd MMM yyyy HH:mm}"></asp:BoundField>
                                    <asp:BoundField HeaderText="Payer" ItemStyle-HorizontalAlign="Left" DataField="PayerName"></asp:BoundField>
                                    <asp:BoundField HeaderText="Admission Status" ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Left" DataField="AdmissionStatus"></asp:BoundField>--%>

                                    <asp:TemplateField HeaderText="Age" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#CDD2DD">
                                        <%-- <HeaderTemplate>
                                            <label style="display: <%=setENG%>;">Age </label>
                                            <label style="display: <%=setIND%>;">Umur </label>
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lblage" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sex" ItemStyle-Width="4%" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderStyle-BorderColor="#CDD2DD">
                                        <%-- <HeaderTemplate>
                                            <label style="display: <%=setENG%>;">Sex </label>
                                            <label style="display: <%=setIND%>;">Seks </label>
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MR No." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#CDD2DD">
                                        <%-- <HeaderTemplate>
                                            <label style="display: <%=setENG%>;">MR No. </label>
                                            <label style="display: <%=setIND%>;">No. MR </label>
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lbllocalmrno" runat="server" Text='<%# Bind("LocalMrNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Admission Date" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#CDD2DD">
                                        <%--<HeaderTemplate>
                                            <label style="display: <%=setENG%>;">Admission Date </label>
                                            <label style="display: <%=setIND%>;">Tgl. Admisi </label>
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsdmissiondate" runat="server" Text='<%# Eval("AdmissionDate","{0:dd MMM yyyy, HH:mm}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payer" ItemStyle-Width="23%" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#CDD2DD">
                                        <%--<HeaderTemplate>
                                            <label style="display: <%=setENG%>;">Payer </label>
                                            <label style="display: <%=setIND%>;">Penanggung </label>
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lblpayername" runat="server" Text='<%# Bind("PayerName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Admission Status" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#CDD2DD">
                                        <%--<HeaderTemplate>
                                            <label style="display: <%=setENG%>;">Admission Status </label>
                                            <label style="display: <%=setIND%>;">Status Admisi </label>
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lbladmissionstatus" runat="server" Text='<%# Bind("AdmissionStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Status" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" DataField="Status" HeaderStyle-BorderColor="#CDD2DD"></asp:BoundField>
                                    <asp:BoundField HeaderText="Rev." ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" DataField="revision" HeaderStyle-BorderColor="#CDD2DD"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="modal fade" id="laboratoryResult" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UP_LabModal">
                <ContentTemplate>
                    <div class="modal-dialog" style="width: 70%;" runat="server">
                        <div class="modal-content" style="border-radius: 7px; height: 100%;">
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" style="text-align: left">
                                    <asp:Label ID="lblModalTitle" Style="font-weight: bold" runat="server" Text="Laboratory Result"></asp:Label></h4>
                            </div>

                            <div class="modal-body">
                                <div style="width: 100%" class="btn-group" role="group">
                                    <tag1:PatientCard runat="server" ID="PatientCard" />
                                    &nbsp;
                            <div style="overflow-y: auto; height: 400px;">
                                <tag1:StdLabResult runat="server" ID="StdLabResult" />
                            </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="modal fade" id="radiologyResult" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UP_RadModal">
                <ContentTemplate>
                    <div class="modal-dialog" style="width: 70%;" runat="server">
                        <div class="modal-content" style="border-radius: 7px; height: 100%;">
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" style="text-align: left">
                                    <asp:Label ID="Label1" Style="font-weight: bold" runat="server" Text="Radiology Result"></asp:Label></h4>
                            </div>

                            <div class="modal-body">
                                <div style="width: 100%" class="btn-group" role="group">
                                    <tag1:PatientCard runat="server" ID="PatientCardRad" />
                                    &nbsp;
                            <div style="overflow-y: auto; height: 400px;">
                                <tag1:StdRadResult runat="server" ID="StdRadResult" />
                            </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </body>

</asp:Content>