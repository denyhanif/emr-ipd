<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WorklistAppointment.aspx.cs" Inherits="Form_General_WorklistAppointment" %>

<%@ Register Src="~/Form/General/Control/PatientCard.ascx" TagPrefix="tag1" TagName="PatientCard" %>
<%@ Register Src="~/Form/General/Control/StdLabResult.ascx" TagPrefix="tag1" TagName="StdLabResult" %>
<%@ Register Src="~/Form/General/Control/StdRadResult.ascx" TagPrefix="tag1" TagName="StdRadResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript">

        $(window).load(function () {
            $(".loadPage").fadeOut("slow");
        });

        function dateSelect() {
            var dp = $('#<%=TextBoxDateSelected.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr",
                todayHighlight: true
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

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

             $('.selecpicker').selectpicker();

            //fungsi untuk action in postback updatepanel
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {

                prm.add_beginRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {

                    }
                });

                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {

                        //gridWorklist.style.display = "";
                        document.getElementById("<%=HiddenLabMark.ClientID %>").value = 0;
                        document.getElementById("<%=HiddenRadMark.ClientID %>").value = 0;

                        //set bahasa after end postback
                        //switchBahasa();
                        <%--var flagcall = document.getElementById("<%=HF_flagcallclick.ClientID %>");
                        if (flagcall.value != "clicked") {
                            //window.location.reload();
                            //clear all timer...wkwkwk
                            var highestTimeoutId = setTimeout(";");
                            for (var i = 0 ; i < highestTimeoutId ; i++) {
                                clearTimeout(i); 
                            }
                        }
                        else {
                            flagcall.value = "";
                        }--%>

                        //clear all timer...wkwkwk
                        var highestTimeoutId = setTimeout(";");
                        for (var i = 0 ; i < highestTimeoutId ; i++) {
                            clearTimeout(i); 
                        }

                        //reset timer
                        $(".timer").each(function () {
                            var appointmentTime = $(this).data("start");
                            Countdown($(this).attr("id"), appointmentTime, new Date());
                        });

                        $(".timerhourly").each(function () {
                            var appointmentTimeHourly = $(this).data("start");
                            CountdownHourly($(this).attr("id"), appointmentTimeHourly, new Date());
                        });

                        $(".timerwaiting").each(function () {
                            var appointmentTimeWaiting = $(this).data("start");
                            CountdownWaiting($(this).attr("id"), appointmentTimeWaiting, new Date());
                        });

                        setInterval(keepRefresh, 120000); //2 minute
                        setInterval(RefreshCountNotifOT, 300000); //5 minute
                        $('.selecpicker').selectpicker();
                    }
                });
            };
        });

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
                //document.getElementById('lblbhs_patientlist').innerHTML = "Patient List";
                //document.getElementById('lblbhs_show').innerHTML = "Show :";
                document.getElementById('lblbhs_nopatient').innerHTML = "No patient yet, today";
                document.getElementById('lblbhs_subnopatient').innerHTML = "Please wait some more time or search another date";
                document.getElementById('lblbhs_nodata').innerHTML = "Oops! There is no data";
                document.getElementById('lblbhs_subnodata').innerHTML = "Please search another date or parameter";
                //document.getElementById('lblbhs_nointernet').innerHTML = "No internet connection";
                //document.getElementById('lblbhs_subnointernet').innerHTML = "Please check your connection & refresh";

                var tablefixed = document.getElementById("tableworklist_fixed");
                if (tablefixed != null) {
                    document.getElementById('lblbhs_fixedtitle').innerHTML = "PATIENT LIST";
                    document.getElementById('lblbhs_fixedtimeslot').innerHTML = "Time Slot";
                    //document.getElementById('lblbhs_fixedno').innerHTML = "No";
                    document.getElementById('lblbhs_fixedpatientname').innerHTML = "Patient Name";
                    document.getElementById('lblbhs_fixedage').innerHTML = "Age";
                    document.getElementById('lblbhs_fixedsex').innerHTML = "Sex";
                    document.getElementById('lblbhs_fixedmrno').innerHTML = "MR No";
                    document.getElementById('lblbhs_fixedpayer').innerHTML = "Payer";
                    document.getElementById('lblbhs_fixedvisitno').innerHTML = "Visit No";
                    document.getElementById('lblbhs_fixedstatus').innerHTML = "Status";
                    document.getElementById('lblbhs_fixedtime').innerHTML = "Time";
                }

                var tablehourly = document.getElementById("tableworklist_hourly");
                if (tablehourly != null) {
                    document.getElementById('lblbhs_hourlytitle').innerHTML = "PATIENT LIST";
                    document.getElementById('lblbhs_hourlytimeslot').innerHTML = "Time Slot";
                    //document.getElementById('lblbhs_hourlyno').innerHTML = "No";
                    document.getElementById('lblbhs_hourlypatientname').innerHTML = "Patient Name";
                    document.getElementById('lblbhs_hourlyage').innerHTML = "Age";
                    document.getElementById('lblbhs_hourlysex').innerHTML = "Sex";
                    document.getElementById('lblbhs_hourlymrno').innerHTML = "MR No";
                    document.getElementById('lblbhs_hourlypayer').innerHTML = "Payer";
                    document.getElementById('lblbhs_hourlyvisitno').innerHTML = "Visit No";
                    document.getElementById('lblbhs_hourlystatus').innerHTML = "Status";
                    document.getElementById('lblbhs_hourlytime').innerHTML = "Time";
                }

                var tablefirst = document.getElementById("tableworklist_first");
                if (tablefirst != null) {
                    document.getElementById('lblbhs_firsttitle').innerHTML = "PATIENT LIST";
                    document.getElementById('lblbhs_firsttimeslot').innerHTML = "Time Slot";
                    //document.getElementById('lblbhs_firstno').innerHTML = "No";
                    document.getElementById('lblbhs_firstpatientname').innerHTML = "Patient Name";
                    document.getElementById('lblbhs_firstage').innerHTML = "Age";
                    document.getElementById('lblbhs_firstsex').innerHTML = "Sex";
                    document.getElementById('lblbhs_firstmrno').innerHTML = "MR No";
                    document.getElementById('lblbhs_firstpayer').innerHTML = "Payer";
                    document.getElementById('lblbhs_firstvisitno').innerHTML = "Visit No";
                    document.getElementById('lblbhs_firststatus').innerHTML = "Status";
                    document.getElementById('lblbhs_firsttime').innerHTML = "Time";
                }

                var tablewaiting = document.getElementById("tableworklist_waiting");
                if (tablewaiting != null) {
                    document.getElementById('lblbhs_waitingtitle').innerHTML = "WAITING LIST";
                    document.getElementById('lblbhs_waitingtimeslot').innerHTML = "Time Slot";
                    //document.getElementById('lblbhs_waitingno').innerHTML = "No";
                    document.getElementById('lblbhs_waitingpatientname').innerHTML = "Patient Name";
                    document.getElementById('lblbhs_waitingage').innerHTML = "Age";
                    document.getElementById('lblbhs_waitingsex').innerHTML = "Sex";
                    document.getElementById('lblbhs_waitingmrno').innerHTML = "MR No";
                    document.getElementById('lblbhs_waitingpayer').innerHTML = "Payer";
                    document.getElementById('lblbhs_waitingvisitno').innerHTML = "Visit No";
                    document.getElementById('lblbhs_waitingstatus').innerHTML = "Status";
                    document.getElementById('lblbhs_waitingtime').innerHTML = "Time";
                }

                var tablesubmit = document.getElementById("tableworklist_submit");
                if (tablesubmit != null) {
                    document.getElementById('lblbhs_submittitle').innerHTML = "SUBMITTED PATIENT";
                    document.getElementById('lblbhs_submittimeslot').innerHTML = "Time Slot";
                    //document.getElementById('lblbhs_submitno').innerHTML = "No";
                    document.getElementById('lblbhs_submitpatientname').innerHTML = "Patient Name";
                    document.getElementById('lblbhs_submitage').innerHTML = "Age";
                    document.getElementById('lblbhs_submitsex').innerHTML = "Sex";
                    document.getElementById('lblbhs_submitmrno').innerHTML = "MR No";
                    document.getElementById('lblbhs_submitpayer').innerHTML = "Payer";
                    document.getElementById('lblbhs_submitvisitno').innerHTML = "Visit No";
                    document.getElementById('lblbhs_submitstatus').innerHTML = "Status";
                    document.getElementById('lblbhs_submittime').innerHTML = "Time";
                }
            }
            else if (bahasa == "IND") {
                document.getElementById('lblbhs_searchdate').innerHTML = "Tanggal Pencarian";
                document.getElementById('lblbhs_mrno').innerHTML = "No. MR atau nama pasien";
                document.getElementById('lblbhs_mrstatus').innerHTML = "Status MR";
                document.getElementById('lblbhs_patienttotal').innerHTML = "Total Pasien";
                //document.getElementById('lblbhs_patientlist').innerHTML = "Daftar Pasien";
                //document.getElementById('lblbhs_show').innerHTML = "Tampilkan :";
                document.getElementById('lblbhs_nopatient').innerHTML = "Belum ada pasien, hari ini";
                document.getElementById('lblbhs_subnopatient').innerHTML = "Harap tunggu beberapa saat lagi atau cari tanggal lain";
                document.getElementById('lblbhs_nodata').innerHTML = "Oops! Tidak ada data";
                document.getElementById('lblbhs_subnodata').innerHTML = "Silakan cari tanggal atau parameter lain";
                //document.getElementById('lblbhs_nointernet').innerHTML = "Tidak ada koneksi internet";
                //document.getElementById('lblbhs_subnointernet').innerHTML = "Silakan periksa koneksi Anda & refresh";

                var tablefixed = document.getElementById("tableworklist_fixed");
                if (tablefixed != null) {
                    document.getElementById('lblbhs_fixedtitle').innerHTML = "DAFTAR PASIEN";
                    document.getElementById('lblbhs_fixedtimeslot').innerHTML = "Slot Waktu";
                    //document.getElementById('lblbhs_fixedno').innerHTML = "No";
                    document.getElementById('lblbhs_fixedpatientname').innerHTML = "Nama Pasien";
                    document.getElementById('lblbhs_fixedage').innerHTML = "Umur";
                    document.getElementById('lblbhs_fixedsex').innerHTML = "Seks";
                    document.getElementById('lblbhs_fixedmrno').innerHTML = "No MR";
                    document.getElementById('lblbhs_fixedpayer').innerHTML = "Penanggung";
                    document.getElementById('lblbhs_fixedvisitno').innerHTML = "No Visit";
                    document.getElementById('lblbhs_fixedstatus').innerHTML = "Status";
                    document.getElementById('lblbhs_fixedtime').innerHTML = "Waktu";
                }

                var tablehourly = document.getElementById("tableworklist_hourly");
                if (tablehourly != null) {
                    document.getElementById('lblbhs_hourlytitle').innerHTML = "DAFTAR PASIEN";
                    document.getElementById('lblbhs_hourlytimeslot').innerHTML = "Slot Waktu";
                    //document.getElementById('lblbhs_hourlyno').innerHTML = "No";
                    document.getElementById('lblbhs_hourlypatientname').innerHTML = "Nama Pasien";
                    document.getElementById('lblbhs_hourlyage').innerHTML = "Umur";
                    document.getElementById('lblbhs_hourlysex').innerHTML = "Seks";
                    document.getElementById('lblbhs_hourlymrno').innerHTML = "No MR";
                    document.getElementById('lblbhs_hourlypayer').innerHTML = "Penanggung";
                    document.getElementById('lblbhs_hourlyvisitno').innerHTML = "No Visit";
                    document.getElementById('lblbhs_hourlystatus').innerHTML = "Status";
                    document.getElementById('lblbhs_hourlytime').innerHTML = "Waktu";
                }

                var tablefirst = document.getElementById("tableworklist_first");
                if (tablefirst != null) {
                    document.getElementById('lblbhs_firsttitle').innerHTML = "DAFTAR PASIEN";
                    document.getElementById('lblbhs_firsttimeslot').innerHTML = "Slot Waktu";
                    //document.getElementById('lblbhs_firstno').innerHTML = "No";
                    document.getElementById('lblbhs_firstpatientname').innerHTML = "Nama Pasien";
                    document.getElementById('lblbhs_firstage').innerHTML = "Umur";
                    document.getElementById('lblbhs_firstsex').innerHTML = "Seks";
                    document.getElementById('lblbhs_firstmrno').innerHTML = "No MR";
                    document.getElementById('lblbhs_firstpayer').innerHTML = "Penanggung";
                    document.getElementById('lblbhs_firstvisitno').innerHTML = "No Visit";
                    document.getElementById('lblbhs_firststatus').innerHTML = "Status";
                    document.getElementById('lblbhs_firsttime').innerHTML = "Waktu";
                }

                var tablewaiting = document.getElementById("tableworklist_waiting");
                if (tablewaiting != null) {
                    document.getElementById('lblbhs_waitingtitle').innerHTML = "DAFTAR TUNGGU";
                    document.getElementById('lblbhs_waitingtimeslot').innerHTML = "Slot Waktu";
                    //document.getElementById('lblbhs_waitingno').innerHTML = "No";
                    document.getElementById('lblbhs_waitingpatientname').innerHTML = "Nama Pasien";
                    document.getElementById('lblbhs_waitingage').innerHTML = "Umur";
                    document.getElementById('lblbhs_waitingsex').innerHTML = "Seks";
                    document.getElementById('lblbhs_waitingmrno').innerHTML = "No MR";
                    document.getElementById('lblbhs_waitingpayer').innerHTML = "Penanggung";
                    document.getElementById('lblbhs_waitingvisitno').innerHTML = "No Visit";
                    document.getElementById('lblbhs_waitingstatus').innerHTML = "Status";
                    document.getElementById('lblbhs_waitingtime').innerHTML = "Waktu";
                }

                var tablesubmit = document.getElementById("tableworklist_submit");
                if (tablesubmit != null) {
                    document.getElementById('lblbhs_submittitle').innerHTML = "PASIEN SUDAH SELESAI";
                    document.getElementById('lblbhs_submittimeslot').innerHTML = "Slot Waktu";
                    //document.getElementById('lblbhs_submitno').innerHTML = "No";
                    document.getElementById('lblbhs_submitpatientname').innerHTML = "Nama Pasien";
                    document.getElementById('lblbhs_submitage').innerHTML = "Umur";
                    document.getElementById('lblbhs_submitsex').innerHTML = "Seks";
                    document.getElementById('lblbhs_submitmrno').innerHTML = "No MR";
                    document.getElementById('lblbhs_submitpayer').innerHTML = "Penanggung";
                    document.getElementById('lblbhs_submitvisitno').innerHTML = "No Visit";
                    document.getElementById('lblbhs_submitstatus').innerHTML = "Status";
                    document.getElementById('lblbhs_submittime').innerHTML = "Waktu";
                }
            }
        }

        function showLoad() {
            $(".loadPage").show();
        }

        $(function () {
            $(".timer").each(function () {
                var appointmentTime = $(this).data("start");
                Countdown($(this).attr("id"), appointmentTime,  new Date());
            });
        });

        function Countdown(ElementID, ExpDateTimeString, NowDateTimeString) {
            var NowDateTime = new Date(NowDateTimeString);
            var ExpDateTime = new Date(ExpDateTimeString);

            // convert the current date and the target date into miliseconds                       
            var NowDateTimeMS = (NowDateTime).getTime();
            var ExpDateTimeMS = (ExpDateTime).getTime();

            // find their difference, and convert that into seconds                 
            var TimeLeftSecs = Math.round((NowDateTimeMS - ExpDateTimeMS) / 1000);

            if (TimeLeftSecs < 0) {
                TimeLeftSecs = 0;
            }

            var Hours = Math.floor(TimeLeftSecs / (60 * 60));
            TimeLeftSecs %= (60 * 60);
            var Minutes = Math.floor(TimeLeftSecs / 60);
            if (Minutes < 10) {
                Minutes = "0" + Minutes;
            }
            TimeLeftSecs %= 60;
            var Seconds = TimeLeftSecs;
            if (Seconds < 10) {
                Seconds = "0" + Seconds;
            }

            if (document.getElementById(ElementID) != null) {
                document.getElementById(ElementID).innerHTML = Hours + ":" + Minutes; // + ":" + Seconds; 
                if (document.getElementById(ElementID).innerHTML == "0:00") {
                    document.getElementById(ElementID).style.display = "none";
                }
            }

            // increment the NowDateTime 1 second
            ExpDateTimeMS -= 1000;
            ExpDateTime.setTime(ExpDateTimeMS);

            // recursive call, keeps the clock ticking
            var FunctionCall = "Countdown('" + ElementID + "','" + ExpDateTime + "','" + NowDateTime + "');";
            setTimeout(FunctionCall, 1000);
        }

        $(function () {
            $(".timerhourly").each(function () {
                var appointmentTimeHourly = $(this).data("start");
                CountdownHourly($(this).attr("id"), appointmentTimeHourly,  new Date());
            });
        });

        function CountdownHourly(ElementID, ExpDateTimeString, NowDateTimeString) {
            var NowDateTime = new Date(NowDateTimeString);
            var ExpDateTime = new Date(ExpDateTimeString);

            // convert the current date and the target date into miliseconds                       
            var NowDateTimeMS = (NowDateTime).getTime();
            var ExpDateTimeMS = (ExpDateTime).getTime();

            // find their difference, and convert that into seconds                 
            var TimeLeftSecs = Math.round((NowDateTimeMS - ExpDateTimeMS) / 1000);

            if (TimeLeftSecs < 0) {
                TimeLeftSecs = 0;
            }

            var Hours = Math.floor(TimeLeftSecs / (60 * 60));
            TimeLeftSecs %= (60 * 60);
            var Minutes = Math.floor(TimeLeftSecs / 60);
            if (Minutes < 10) {
                Minutes = "0" + Minutes;
            }
            TimeLeftSecs %= 60;
            var Seconds = TimeLeftSecs;
            if (Seconds < 10) {
                Seconds = "0" + Seconds;
            }

            if (document.getElementById(ElementID) != null) {
                document.getElementById(ElementID).innerHTML = Hours + ":" + Minutes; // + ":" + Seconds; 
                if (document.getElementById(ElementID).innerHTML == "0:00") {
                    document.getElementById(ElementID).style.display = "none";
                }
            }

            // increment the NowDateTime 1 second
            ExpDateTimeMS -= 1000;
            ExpDateTime.setTime(ExpDateTimeMS);

            // recursive call, keeps the clock ticking
            var FunctionCallHourly = "CountdownHourly('" + ElementID + "','" + ExpDateTime + "','" + NowDateTime + "');";
            setTimeout(FunctionCallHourly, 1000);
        }

        $(function () {
            $(".timerwaiting").each(function () {
                var appointmentTimeWaiting = $(this).data("start");
                CountdownWaiting($(this).attr("id"), appointmentTimeWaiting,  new Date());
            });
        });

        function CountdownWaiting(ElementID, ExpDateTimeString, NowDateTimeString) {
            var NowDateTime = new Date(NowDateTimeString);
            var ExpDateTime = new Date(ExpDateTimeString);

            // convert the current date and the target date into miliseconds                       
            var NowDateTimeMS = (NowDateTime).getTime();
            var ExpDateTimeMS = (ExpDateTime).getTime();

            // find their difference, and convert that into seconds                 
            var TimeLeftSecs = Math.round((NowDateTimeMS - ExpDateTimeMS) / 1000);

            if (TimeLeftSecs < 0) {
                TimeLeftSecs = 0;
            }

            var Hours = Math.floor(TimeLeftSecs / (60 * 60));
            TimeLeftSecs %= (60 * 60);
            var Minutes = Math.floor(TimeLeftSecs / 60);
            if (Minutes < 10) {
                Minutes = "0" + Minutes;
            }
            TimeLeftSecs %= 60;
            var Seconds = TimeLeftSecs;
            if (Seconds < 10) {
                Seconds = "0" + Seconds;
            }

            if (document.getElementById(ElementID) != null) {
                document.getElementById(ElementID).innerHTML = Hours + ":" + Minutes; // + ":" + Seconds; 
                if (document.getElementById(ElementID).innerHTML == "0:00") {
                    document.getElementById(ElementID).style.display = "none";
                }
            }

            // increment the NowDateTime 1 second
            ExpDateTimeMS -= 1000;
            ExpDateTime.setTime(ExpDateTimeMS);

            // recursive call, keeps the clock ticking
            var FunctionCallWaiting = "CountdownWaiting('" + ElementID + "','" + ExpDateTime + "','" + NowDateTime + "');";
            setTimeout(FunctionCallWaiting, 1000);
        }

        //var NowDateTime = new Date();
        //var ExpDateTime = new Date("December 27 2019 06:30");
        //var ElementID = "00:00:00"; 

        //// recursive call, keeps the clock ticking
        //var FunctionCall = "Countdown('" + ElementID + "','" + ExpDateTime + "','" + NowDateTime + "');";
        //setTimeout(FunctionCall, 1000);

        <%--function iscall(idcall) {
            //document.getElementById("<%=HF_flagcallclick.ClientID %>").value = "clicked";
            //document.getElementById(idcall).value = "...";
            //document.getElementById(idcall).style.backgroundColor = "#1a2269";
        }--%>

        <%--function klikTanggal() {
            //document.getElementById("<%=TextBoxDateSelected.ClientID %>").click();
            //$("#MainContent_TextBoxDateSelected").mousedown();
            //$('#<%=TextBoxDateSelected.ClientID%>').mousedown()

        }--%>

        function keepRefresh() {
            console.log('worklist refresh');
            document.getElementById('<%= RefreshButton.ClientID %>').click();

            //RefreshCountNotifOT();
        }

        setInterval(keepRefresh, 120000); //2 minute
        setInterval(RefreshCountNotifOT, 300000); //5 minute

        function klikCall(admidd, orgidd, docidd, ptnname, Qno, idbtn) {
            this.event.preventDefault();

            //var btn = document.getElementById(idbtn);
            //btn.disabled = true;
            //setTimeout(function () { btn.disabled = false; }, 5000);

            CallDelayTrue();
            setTimeout(CallDelayFalse, 5000);

            var namapasien = ptnname.toString().replace(/_/g, " ");
            var paramnya = { 'admid': admidd, 'orgid': orgidd, 'docid': docidd };
            $.ajax({
                type: "POST",
                url: "WorklistAppointment.aspx/CallPatient",
                data: JSON.stringify(paramnya),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "success") {
                        toastr.success("Pasien " + namapasien + " Telah dipanggil ke ruangan", "Calling Queue " + Qno);
                        toastr.options.positionClass = "toast-top-right";
                    }
                    else {
                        toastr.error(msg.d, "Call Failed");
                        toastr.options.positionClass = "toast-top-right";
                    }  
                },
                error: function (xmlhttprequest, textstatus, errorthrown) {
                    alert(" conection to the server failed ");
                    console.log("error: " + errorthrown);
                }
            });
        }

        function klikPatient(admissionId, alertList, admidd, orgidd, docidd, ptnname, apptidd, isaidoo, linkurl) {
            this.event.preventDefault();

            if (apptidd != "00000000-0000-0000-0000-000000000000") {
                var namapasien = ptnname.toString().replace(/_/g, " ");
                var paramnya = { 'admid': admidd, 'orgid': orgidd, 'docid': docidd, 'apptid': apptidd, 'isaido': isaidoo };
                $.ajax({
                    type: "POST",
                    url: "WorklistAppointment.aspx/ClickPatient",
                    data: JSON.stringify(paramnya),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "success") {
                            console.log("Click : " + namapasien + " success");
                        }
                        else {
                            console.log("Click : " + namapasien + " fail");
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrown) {
                        //alert(" conection to the server failed ");
                        console.log("error: " + errorthrown);
                    }
                });
            }

            localStorage.setItem("alertList" + admissionId, alertList);

            //location.href = linkurl;
            if(this.event.which != 3) //not click right mouse
            {
                //window.location = linkurl;
            }
        }

        function klikDirectSoapTele(encidd, ptnidd, admidd, linkurl) {
            this.event.preventDefault();

            klikZoomLog(encidd, ptnidd, admidd);

            window.location = linkurl;
        }

        function kliksetalert(admissionId, alertList) {
            localStorage.setItem("alertList" + admissionId, alertList);
        }

        function CallDelayTrue() {
            var btn = document.getElementsByClassName("btn-call");
            var i;
            for (i = 0; i < btn.length; i++) {
                btn[i].disabled = true;
            }
        }

        function CallDelayFalse() {
            var btn = document.getElementsByClassName("btn-call");
            var i;
            for (i = 0; i < btn.length; i++) {
                btn[i].disabled = false;
            }
        }

        function OpenModalSchedule() {
             $('#modalChooseSchedule').modal('show');
        }

        function ValidasiCheckin() {
            var ddlsch = document.getElementById("<%= dropdownSchedule.ClientID %>");
            var ddlroom = document.getElementById("<%= dropdownRoom.ClientID %>");

            if (ddlsch.selectedIndex != -1 && ddlroom.selectedIndex != -1) {
                if (ddlsch.options[ddlsch.selectedIndex].value == "0" || ddlroom.options[ddlroom.selectedIndex].value == "0") {
                    alert("Please Select Schedule and Room First!")
                    return false;
                }
            }
            else {
                alert("Please Select Schedule and Room First!")
                return false;
            }
        }

        function klikZoom(encidd, ptnidd, admidd, linkurl) {
            this.event.preventDefault();
            var orgidd = document.getElementById("<%=hfOrgID.ClientID%>").value;
            var paramnya = { 'encid': encidd, 'orgid': orgidd, 'ptnid': ptnidd, 'admid': admidd };
            $.ajax({
                type: "POST",
                url: "WorklistAppointment.aspx/LogZoom",
                data: JSON.stringify(paramnya),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "success") {
                        console.log("zoom log : " + encidd + " success");
                    }
                    else {
                        console.log("zoom log : " + msg.d );
                    }  
                },
                error: function (xmlhttprequest, textstatus, errorthrown) {
                    //alert(" conection to the server failed ");
                    console.log("error: " + errorthrown);
                }
            });
            window.open(linkurl, '_blank').focus();
        }

        function klikZoomLog(encidd, ptnidd, admidd) {
            this.event.preventDefault();
            var orgidd = document.getElementById("<%=hfOrgID.ClientID%>").value;
            var paramnya = { 'encid': encidd, 'orgid': orgidd, 'ptnid': ptnidd, 'admid': admidd };
            $.ajax({
                type: "POST",
                url: "WorklistAppointment.aspx/LogZoom",
                data: JSON.stringify(paramnya),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "success") {
                        console.log("zoom log : " + encidd + " success");
                    }
                    else {
                        console.log("zoom log : " + msg.d );
                    }  
                },
                error: function (xmlhttprequest, textstatus, errorthrown) {
                    //alert(" conection to the server failed ");
                    console.log("error: " + errorthrown);
                }
            });
        }

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

        .borderTime {
        border-bottom:3px solid #1172f7 !important;
        }

        .warnaBGSubmit {
            background-color:palegreen !important;
        }

        .warnaBGCancel {
            background-color:#ffdde2 !important;
        }

        .warnaBGCall3x {
            background-color:#f8f3ac !important;
        }

        .warnaBGnotcheckin {
            background-color:#eeeeee !important;
            font-size:10px;
            padding-bottom:0px !important;
        }
        .WarnaBGKewaspadaan {
            color: #8F701C !important;
            background-color: #FEEEC1 !important;
        }

        .sizeImgnormal {
        width:20px;
        height:20px;
        }

        .sizeImgnotcheckin {
        width:17px;
        height:17px;
        }

        .callDisableNormal {
            padding-top: 1px;
            padding-bottom: 1px;
            background-color: #cdceda;
            color: white;
            width: 45px;
            cursor: not-allowed;
        }

        .callDisableNotCheckin{
            padding-top: 0px;
            padding-bottom: 0px;
            font-size:10px;
            background-color: #cdceda;
            color: white;
            width: 45px;
            cursor: not-allowed;
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
                                    <td style="display:none;">
                                        <label id="lblbhs_searchdate">Search Date </label>
                                    </td>
                                    <td style="display:none;">&nbsp; </td>
                                    <td style="display:none;">&nbsp; </td>
                                    <td style="display:none;">&nbsp; </td>

                                    <td>
                                        <label id="lblbhs_mrno">MR No. or patient name  </label>
                                    </td>
                                    <td>&nbsp; </td>
                                    <td>
                                        <label id="lblbhs_mrstatus">MR Status  </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="display:none;">
                                        <asp:TextBox class="form-control" runat="server" ID="DateTextboxStart" name="date" Style="height: 25px; font-size: 12px; background-color: white;" placeholder="dd/mm/yyyy" OnTextChanged="DateTextboxStart_TextChanged" onmousedown="dateStart();" AutoPostBack="true" AutoCompleteType="Disabled" />
                                    </td>
                                    <td style="width: 20px; text-align: center; display:none;">- </td>
                                    <td style="display:none;">
                                        <asp:TextBox class="form-control" runat="server" ID="DateTextboxEnd" name="date" Style="height: 25px; font-size: 12px; background-color: white;" placeholder="dd/mm/yyyy" OnTextChanged="DateTextboxEnd_TextChanged" onmousedown="dateEnd ();" AutoPostBack="true" AutoCompleteType="Disabled" />
                                    </td>
                                    <td style="width: 15px; display:none;">&nbsp; </td>

                                    <td style="width:30%;">
                                        <asp:TextBox class="form-control" runat="server" ID="MRsearch" name="MRsearch" Style="height: 25px; font-size: 12px" type="text" placeholder="MR or patient name" OnTextChanged="MRSearch_TextChanged" AutoPostBack="true" />
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
                
                <div style="text-align:center; font-size:14px;">
                    <div class="row">
                        <asp:HiddenField ID="HF_schid_selected" runat="server" />
                        <asp:HiddenField ID="HF_rmid_selected" runat="server" />
                        <asp:HiddenField ID="HF_isPermanent_selected" runat="server" />
                        <asp:HiddenField ID="HF_DoctorIDMysiloam_selected" runat="server" />
                        <asp:HiddenField ID="HF_HospitalIDMysiloam_selected" runat="server" />

                        <div class="col-sm-6" style="padding-left:40px;">
                            <div id="divgroupCheckin" runat="server" >
                                <div style="padding-top: 10px; padding-left: 2px; text-align:left;" id="divaddcheckin" runat="server">
                                    <label>Please check-in to call patient</label> &nbsp;
                                    <button class="btn btn-success btn-emr-small" onclick="OpenModalSchedule();">Check In</button>
                                </div>
                                <div style="padding-top: 10px; padding-left: 2px; text-align:left;" id="diveditcheckin" runat="server" visible="false">
                                    <i class="fa fa-check-circle" style="color:forestgreen;"> &nbsp; </i><label>You're checked in at :</label> 
                                    <asp:Label ID="LabelRoomSelected" runat="server" Text="-" style="font-weight:bold;"></asp:Label> &nbsp;
                                    <a href="javascript:OpenModalSchedule();" style="color:#9d1fc3; font-weight:bold; text-decoration:underline">Change</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6" style="text-align:right; padding-right:40px;">

                            <div style="display:inline-block;">
                                <asp:UpdateProgress ID="UpdateProgressWL" runat="server" AssociatedUpdatePanelID="upError">
                                    <ProgressTemplate>
                                        <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <asp:LinkButton ID="ButtonPrevDate" CssClass="btn" style="padding: 5px; margin-right: -5px;" runat="server" OnClick="ButtonPrevDate_Click"> <i class="icon-ic_PreviousCalendar" style="font-size:22px; background-color: white;border-radius: 5px;"></i></asp:LinkButton>
                            <asp:TextBox class="form-control" runat="server" ID="TextBoxDateSelected" CssClass="isCalendar" Style="height: 25px; width:135px; padding-left:10px; border-radius:5px; font-size: 15px; font-weight:bold; background-color: #ffffff66; border:1px solid transparent; display:inline-block; text-align:left; cursor:pointer;" placeholder="dd/mm/yyyy" onmousedown="dateSelect();" OnTextChanged="TextBoxDateSelected_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled" />
                            <span class="fa fa-calendar" style="margin-right: 9px; display:none;"></span>
                            <asp:LinkButton ID="ButtonNextDate" CssClass="btn" style="padding: 5px; margin-left: -5px;" runat="server" OnClick="ButtonNextDate_Click"><i class="icon-ic_NextCalendar" style="font-size:22px; background-color: white;border-radius: 5px;"></i></asp:LinkButton>
                        </div>
                    </div>
                    
                </div>

                <div style="background-color: transparent; padding-top:5px;">

                    <asp:HiddenField ID="HF_flagcallclick" runat="server" />
                    <asp:HiddenField runat="server" ID="hfOrgID" />
                    <asp:HiddenField runat="server" ID="hf_admiss_id" />
                    <asp:HiddenField runat="server" ID="hf_patient_id" />
                    <asp:HiddenField runat="server" ID="hf_ticket_patient" />
                    <asp:HiddenField ID="HiddenLabMark" runat="server" />
                    <asp:HiddenField ID="HiddenRadMark" runat="server" />
                    <asp:HiddenField ID="HiddenPageIndex" runat="server" />
                    <asp:HiddenField ID="HF_flagtimer" runat="server" />
                    <asp:Button ID="RefreshButton" runat="server" Text="Button" CssClass="hidden" OnClick="RefreshButton_Click" />

                    <div style="background-color: white; width: 96%; border-radius: 7px 7px 7px 7px; padding-top: 10px; padding-bottom: 10px;" class="container-fluid">
                        
                        <div id="img_noData" runat="server" style="display: none; padding: 30px; text-align: center;">
                            <div>
                                <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px;" />
                            </div>
                            <div runat="server" id="no_patient_today" style="display: none;">
                                <span>
                                    <h3 style="font-weight: 700; color: #585A6F">
                                        <label id="lblbhs_nopatient">No patient yet, today </label>
                                    </h3>
                                </span>
                                <span style="font-size: 14px; color: #585A6F">
                                    <label id="lblbhs_subnopatient">Please wait some more time or search another date </label>
                                </span>
                            </div>
                            <div runat="server" id="no_patient_data" style="display: none;">
                                <span>
                                    <h3 style="font-weight: 700; color: #585A6F">
                                        <label id="lblbhs_nodata">Oops! There is no data </label>
                                    </h3>
                                </span>
                                <span style="font-size: 14px; color: #585A6F">
                                    <label id="lblbhs_subnodata">Please search another date or parameter </label>
                                </span>
                            </div>
                        </div>

                        <div id="divfixedworklist" runat="server">
                        <b> <label id="lblbhs_fixedtitle"> PATIENT LIST </label> </b> <br /> 
                        <table id="tableworklist_fixed" style="width:100%; border:1px solid #cdd2dd;" border="1" class="table table-condensed table-hover-worklist">
                        <tr style="background-color:#f4f4f4;">
                            <th style="border:1px solid #cdd2dd; width:8%;">
                                <label id="lblbhs_fixedtimeslot">Time Slot </label>    
                            </th>
                            <%--<th style="border:1px solid #cdd2dd; width:4%;">
                                <label id="lblbhs_fixedno">No </label>  
                            </th>--%>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_fixedvisitno">Visit No </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:21%;">
                                <label id="lblbhs_fixedpatientname">Patient Name </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:5%;">
                                &nbsp;
                            </th>
                            <th style="border:1px solid #cdd2dd; width:5%;">
                                &nbsp;
                            </th>
                            <th style="border:1px solid #cdd2dd; width:7%;">
                                <label id="lblbhs_fixedage">Age </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:4%;">
                                <label id="lblbhs_fixedsex">Sex </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_fixedmrno">MR No </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:26%;">
                                <label id="lblbhs_fixedpayer">Payer </label> 
                            </th>
                            
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_fixedstatus">Status </label> 
                            </th>
                            <%--<th style="border:1px solid #cdd2dd;">
                                Admission Date
                            </th>--%>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_fixedtime">Time </label> 
                            </th>
                        </tr>                          
                        <asp:Repeater ID="RepeaterPasien" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                       
                                        <asp:Label ID="LabelFromTime" runat="server" Text='<%# Eval("AppointmentFromTime").ToString().Substring(0,5) %>' Visible='<%# Eval("IsVIP").ToString() != "True" %>'></asp:Label> <asp:Label ID="Labelstrip" runat="server" Text="-" Visible='<%# Eval("IsVIP").ToString() != "True" %>'></asp:Label> <asp:Label ID="LabelToTime" runat="server" Text='<%# Eval("AppointmentToTime").ToString().Substring(0,5) %>' Visible='<%# Eval("IsVIP").ToString() != "True" %>'></asp:Label>
                                    </td>
                                    <%--<td style="border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %>'>                       
                                        <asp:Label ID="LabelAppointNo" runat="server" Text='<%# Eval("AppointmentNo") %>'></asp:Label>
                                    </td>--%>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>   
                                        <asp:Label ID="lblqueueno" runat="server" Text='<%# Bind("QueueNo") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>   
                                        <asp:HyperLink ID="lblAdmissionId" runat="server" style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "color:#8F701C;": "color:#1a2269;") %>' Visible='<%# Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() %>' NavigateUrl='<%# String.Format("~/Form/General/PatientDashboard.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO")) %>'> <label style="font-weight:bold; text-decoration:underline; cursor:pointer;" onmousedown=<%# "klikPatient('" + Eval("AdmissionId") + "','" + Eval("alertList") + "','" + Eval("AppointmentAdmissionId")+ "','" + Eval("AppointmentHospitalId")  + "','" + Eval("AppointmentDoctorId") + "','" + Eval("PatientName").ToString().Replace(" ","_").Replace("'","_") + "','" + Eval("AppointmentId") + "','" + Eval("IsAIDO").ToString().ToLower() + "','" + String.Format("PatientDashboard.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}&ZoomLink={7}&ApptAdmID={8}&ApptOrgID={9}&ApptDocID={10}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO"), Eval("ZoomLink"), Eval("AppointmentAdmissionId"), Eval("AppointmentHospitalId"), Eval("AppointmentDoctorId")) + "')" %> >  <%# Eval("PatientName") %>  </label> </asp:HyperLink>
                                        <asp:Label ID="lblPatientNameDisable" runat="server" Visible='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() %>' style=" color:#1a2269;" Text='<%# Bind("PatientName") %>'></asp:Label>

										<i id="iconKewaspadaan" title='<%#"kewaspadaan: "+ Eval("alertList").ToString() %>' runat="server" class="fa fa-exclamation-circle" style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "display:inline-block; color:#FBC531; font-size:14px": "display:none") %>'></i>


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
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>   
                                        <div id="divcallappointment" runat="server" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "false" %>'>
                                            <asp:Button ID="ButtonCallAppoint" runat="server" Text="Call" CssClass="btn btn-lightGreen btn-sm btn-call" Visible='<%# Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("QueueNo").ToString() != "" %>' style="padding-top:1px; padding-bottom:1px; width: 45px;" OnClientClick=<%# "klikCall('" + Eval("AppointmentAdmissionId") + "','" + Eval("AppointmentHospitalId")  + "','" + Eval("AppointmentDoctorId") + "','" + Eval("PatientName").ToString().Replace(" ","_").Replace("'","_") + "','" + Eval("QueueNo") + "','" + "MainContent_RepeaterPasien_ButtonCallAppoint_" + Container.ItemIndex  + "')" %> />
                                            <asp:Label ID="ButtonNotCallAppoint" runat="server" Text="Call" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "btn btn-sm callDisableNotCheckin" : "btn btn-sm callDisableNormal" %>' Visible='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() || Eval("QueueNo").ToString() == "" %>'></asp:Label>
                                        </div>
                                        <asp:HiddenField ID="HFAppointmentAdmissionId" runat="server" Value='<%# Eval("AppointmentAdmissionId").ToString() %>' />
                                        <asp:HiddenField ID="HFAppointmentHospitalId" runat="server" Value='<%# Eval("AppointmentHospitalId").ToString() %>' />
                                        <asp:HiddenField ID="HFAppointmentDoctorId" runat="server" Value='<%# Eval("AppointmentDoctorId").ToString() %>' />
                                        <a href='<%# Eval("ZoomLink") %>' target="_blank" onclick=<%# "klikZoom('" + Eval("EncounterId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + Eval("ZoomLink") +  "')" %> >
                                            <asp:Label ID="ButtonCallZoom" runat="server" Text="Zoom" style="padding-top:1px; padding-bottom:1px; padding-left:7px; width: 45px;"  CssClass="btn btn-twitter btn-sm" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "true" && TeleConfig == "False" %>'  ></asp:Label> 
                                        </a>
                                        <asp:Label ID="ButtonCallZoomInternal" runat="server" Text="Zoom" style="padding-top:1px; padding-bottom:1px; padding-left:7px; width: 45px;"  CssClass="btn btn-twitter btn-sm" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "true" && TeleConfig == "True" %>' onclick=<%# "klikDirectSoapTele('" + Eval("EncounterId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + String.Format("../SOAP/Template/StdSoapTeleconsultation.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}&DirectTele={7}&ZoomLink={8}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO"),"True", Eval("ZoomLink")) + "')" %> ></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>    
                                        <asp:ImageButton ID="labResult" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labE.png" runat="server" Visible='<%# Eval("isLab").ToString() != "0" %>' OnClick="labResult_Click" OnClientClick='<%# "openLabResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                        <asp:ImageButton class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labD.png" runat="server" Enabled="false" Visible='<%# Eval("isLab").ToString() != "1" %>' />
                                        <asp:ImageButton ID="radResult" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Radiology" ImageUrl="~/Images/Icon/radE.png" runat="server" Visible='<%# Eval("isRad").ToString() != "0" %>' OnClick="radResult_Click" OnClientClick='<%# "openRadResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                        <asp:ImageButton class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Radiology" ImageUrl="~/Images/Icon/radD.png" runat="server" Enabled="false" Visible='<%# Eval("isRad").ToString() != "1" %>' />
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>   
                                        <asp:Label ID="lblage" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>   
                                        <asp:Label ID="lblgender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>    
                                        <asp:Label ID="lbllocalmrno" runat="server" Text='<%# Bind("LocalMrNo") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>    
                                        <asp:Label ID="lblpayername" runat="server" Text='<%# Bind("PayerName") %>'></asp:Label>
                                    </td>
                                    
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>    
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                    </td>
                                   <%-- <td style="border:1px solid #cdd2dd;" class='<%# DateTime.Parse(Eval("AdmissionDate").ToString()).Date == DateTime.Parse("17 Dec 2019").Date ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" ? "warnaBGnotcheckin" : "" %>'>
                                         <asp:Label ID="lblsdmissiondate" runat="server" Text='<%# Eval("AdmissionDate","{0:dd MMM yyyy}") %>'></asp:Label>
                                    </td>--%>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                                         
                                        <label id="ElementID_<%# Container.ItemIndex %>" class="timer" data-start='<%# Eval("AdmissionDate","{0:MMMM dd yyyy}").ToString() + " " + Eval("AppointmentToTime").ToString().Substring(0,5) %>' style='display:<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() || Eval("AppointmentFromTime").ToString().Substring(0, 5) == "00:00" || Eval("IsVIP").ToString() == "True" ? "none" : "" %>'><label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                            <tr id="rownodata_fixed" runat="server">
                                <td colspan="12" style="text-align:center; padding: 10px; font-size: 15px; color: darkgrey;">
                                    <i class="fa fa-ban"></i> No Data
                                </td>
                            </tr>
                        </table>
                        </div>

                        <div id="divhourlyworklist" runat="server">
                        <b> <label id="lblbhs_hourlytitle"> PATIENT LIST </label> </b> <br /> 
                        <table id="tableworklist_hourly" style="width:100%; border:1px solid #cdd2dd;" border="1" class="table table-condensed table-hover-worklist">
                        <tr style="background-color:#f4f4f4;">
                            <th style="border:1px solid #cdd2dd; width:8%;">
                                <label id="lblbhs_hourlytimeslot">Time Slot </label>    
                            </th>
                            <%--<th style="border:1px solid #cdd2dd; width:4%;">
                                <label id="lblbhs_hourlyno">No </label>  
                            </th>--%>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_hourlyvisitno">Visit No </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:21%;">
                                <label id="lblbhs_hourlypatientname">Patient Name </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:5%;">
                                &nbsp;
                            </th>
                            <th style="border:1px solid #cdd2dd; width:5%;">
                                &nbsp;
                            </th>
                            <th style="border:1px solid #cdd2dd; width:7%;">
                                <label id="lblbhs_hourlyage">Age </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:4%;">
                                <label id="lblbhs_hourlysex">Sex </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_hourlymrno">MR No </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:26%;">
                                <label id="lblbhs_hourlypayer">Payer </label> 
                            </th>
                            
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_hourlystatus">Status </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_hourlytime">Time </label> 
                            </th>
                        </tr>                          
                        <asp:Repeater ID="RepeaterPasienHourly" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("IsVIP").ToString() != "True"  ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                    
                                        <asp:Label ID="LabelFromTime" runat="server" Text='<%# Eval("AppointmentFromTime").ToString().Substring(0,5) %>' Visible='<%# Eval("TempIndex").ToString() == "0" && Eval("IsVIP").ToString() != "True" %>'></asp:Label> <asp:Label ID="Labelstrip" runat="server" Text="-" Visible='<%# Eval("TempIndex").ToString() == "0" && Eval("IsVIP").ToString() != "True" %>'></asp:Label>  <asp:Label ID="LabelToTime" runat="server" Text='<%# Eval("AppointmentToTime").ToString().Substring(0,5) %>' Visible='<%# Eval("TempIndex").ToString() == "0" && Eval("IsVIP").ToString() != "True" %>'></asp:Label>
                                    </td>
                                    <%--<td style="border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %>'>                      
                                        <asp:Label ID="LabelAppointNo" runat="server" Text='<%# Eval("AppointmentNo") %>'></asp:Label>
                                    </td>--%>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                       
                                        <asp:Label ID="lblqueueno" runat="server" Text='<%# Bind("QueueNo") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:HyperLink ID="lblAdmissionId" runat="server" style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "color:#8F701C;": "color:#1a2269;") %>'  Visible='<%# Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() %>' NavigateUrl='<%# String.Format("~/Form/General/PatientDashboard.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO")) %>'> <label style="font-weight:bold; text-decoration:underline; cursor:pointer;" onmousedown=<%# "klikPatient('" + Eval("AdmissionId") + "','" + Eval("alertList") + "','" + Eval("AppointmentAdmissionId") + "','" + Eval("AppointmentHospitalId")  + "','" + Eval("AppointmentDoctorId") + "','" + Eval("PatientName").ToString().Replace(" ","_").Replace("'","_")  + "','" + Eval("AppointmentId") + "','" + Eval("IsAIDO").ToString().ToLower() + "','" + String.Format("PatientDashboard.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}&ZoomLink={7}&ApptAdmID={8}&ApptOrgID={9}&ApptDocID={10}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO"), Eval("ZoomLink"),Eval("AppointmentAdmissionId"), Eval("AppointmentHospitalId"), Eval("AppointmentDoctorId")) + "')" %> >  <%# Eval("PatientName") %>  </label> </asp:HyperLink>
                                        <asp:Label ID="lblPatientNameDisable" runat="server" Visible='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() %>' style=" color:#1a2269;" Text='<%# Bind("PatientName") %>'></asp:Label>

										<i id="iconKewaspadaan" title='<%#"kewaspadaan: "+ Eval("alertList").ToString() %>' runat="server" class="fa fa-exclamation-circle" style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "display:inline-block; color:#FBC531; font-size:14px": "display:none") %>'></i>

                                        <asp:HiddenField ID="HF_alert_patient" Value='<%#Eval("alertList").ToString()%>' runat="server" />

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
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <%--<asp:Button ID="ButtonCallAppoint" runat="server" Text="Call" CssClass="btn btn-lightGreen btn-sm" Visible='<%# Eval("AdmissionId").ToString() != "0" %>' style="padding-top:1px; padding-bottom:1px; width: 45px;" OnClientClick="iscall(this.id);" OnClick="ButtonCallAppoint_Click" />--%>
                                        <div id="divcallappointment" runat="server" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "false" %>'>
                                            <asp:Button ID="ButtonCallAppoint" runat="server" Text="Call" CssClass="btn btn-lightGreen btn-sm btn-call" Visible='<%# Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("QueueNo").ToString() != "" %>' style="padding-top:1px; padding-bottom:1px; width: 45px;" OnClientClick=<%# "klikCall('" + Eval("AppointmentAdmissionId") + "','" + Eval("AppointmentHospitalId")  + "','" + Eval("AppointmentDoctorId") + "','" + Eval("PatientName").ToString().Replace(" ","_").Replace("'","_") + "','" + Eval("QueueNo") + "','" + "MainContent_RepeaterPasienHourly_ButtonCallAppoint_" + Container.ItemIndex  + "')" %> />
                                            <asp:Label ID="ButtonNotCallAppoint" runat="server" Text="Call" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "btn btn-sm callDisableNotCheckin" : "btn btn-sm callDisableNormal" %>' Visible='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() || Eval("QueueNo").ToString() == "" %>'></asp:Label>
                                        </div>
                                        <asp:HiddenField ID="HFAppointmentAdmissionId" runat="server" Value='<%# Eval("AppointmentAdmissionId").ToString() %>' />
                                        <asp:HiddenField ID="HFAppointmentHospitalId" runat="server" Value='<%# Eval("AppointmentHospitalId").ToString() %>' />
                                        <asp:HiddenField ID="HFAppointmentDoctorId" runat="server" Value='<%# Eval("AppointmentDoctorId").ToString() %>' />
                                        <a href='<%# Eval("ZoomLink") %>' target="_blank" onclick=<%# "klikZoom('" + Eval("EncounterId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + Eval("ZoomLink") +  "')" %> >
                                            <asp:Label ID="ButtonCallZoom" runat="server" Text="Zoom" style="padding-top:1px; padding-bottom:1px; padding-left:7px; width: 45px;"  CssClass="btn btn-twitter btn-sm" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "true" && TeleConfig == "False" %>'  ></asp:Label> 
                                        </a>
                                        <asp:Label ID="ButtonCallZoomInternal" runat="server" Text="Zoom" style="padding-top:1px; padding-bottom:1px; padding-left:7px; width: 45px;"  CssClass="btn btn-twitter btn-sm" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "true" && TeleConfig == "True" %>' onclick=<%# "klikDirectSoapTele('" + Eval("EncounterId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + String.Format("../SOAP/Template/StdSoapTeleconsultation.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}&DirectTele={7}&ZoomLink={8}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO"),"True", Eval("ZoomLink")) + "')" %> ></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:ImageButton ID="labResult" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labE.png" runat="server" Visible='<%# Eval("isLab").ToString() != "0" %>' OnClick="labResult_Click" OnClientClick='<%# "openLabResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                        <asp:ImageButton class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labD.png" runat="server" Enabled="false" Visible='<%# Eval("isLab").ToString() != "1" %>' />
                                        <asp:ImageButton ID="radResult" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Radiology" ImageUrl="~/Images/Icon/radE.png" runat="server" Visible='<%# Eval("isRad").ToString() != "0" %>' OnClick="radResult_Click" OnClientClick='<%# "openRadResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                        <asp:ImageButton class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Radiology" ImageUrl="~/Images/Icon/radD.png" runat="server" Enabled="false" Visible='<%# Eval("isRad").ToString() != "1" %>' />
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:Label ID="lblage" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                       
                                        <asp:Label ID="lblgender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:Label ID="lbllocalmrno" runat="server" Text='<%# Bind("LocalMrNo") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:Label ID="lblpayername" runat="server" Text='<%# Bind("PayerName") %>'></asp:Label>
                                    </td>
                                    
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentFromTime").ToString()).TimeOfDay) > 0 && TimeSpan.Compare(DateTime.Now.TimeOfDay,DateTime.Parse(Eval("AdmissionDate","{0:yyyy/MM/dd}").ToString() + " " + Eval("AppointmentToTime").ToString()).TimeOfDay) < 0 && Eval("TempIndex").ToString() == "0" && Eval("TempToday").ToString() == DateTime.Now.Date.ToString() && Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("IsVIP").ToString() != "True" ? "borderTime" : "" %> <%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                                                           
                                        <label id="Hourly_ElementID_<%# Container.ItemIndex %>" class="timerhourly" data-start='<%# Eval("AdmissionDate","{0:MMMM dd yyyy}").ToString() + " " + Eval("AppointmentToTime").ToString().Substring(0,5) %>' style='display:<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() || Eval("AppointmentFromTime").ToString().Substring(0, 5) == "00:00" || Eval("IsVIP").ToString() == "True" ? "none" : "" %>'><label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                            <tr id="rownodata_hourly" runat="server">
                                <td colspan="12" style="text-align:center; padding: 10px; font-size: 15px; color: darkgrey;">
                                    <i class="fa fa-ban"></i> No Data
                                </td>
                            </tr>
                        </table>
                        </div>

                        <div id="divfirstcomeworklist" runat="server">
                        <b> <label id="lblbhs_firsttitle"> PATIENT LIST </label> </b> <br /> 
                        <table id="tableworklist_first" style="width:100%; border:1px solid #cdd2dd;" border="1" class="table table-condensed table-hover-worklist">
                        <tr style="background-color:#f4f4f4;">
                            <th style="border:1px solid #cdd2dd; width:8%;">
                                <label id="lblbhs_firsttimeslot">Time Slot </label>    
                            </th>
                            <%--<th style="border:1px solid #cdd2dd; width:4%;">
                                <label id="lblbhs_firstno">No </label>  
                            </th>--%>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_firstvisitno">Visit No </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:21%;">
                                <label id="lblbhs_firstpatientname">Patient Name </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:5%;">
                                &nbsp;
                            </th>
                            <th style="border:1px solid #cdd2dd; width:5%;">
                                &nbsp;
                            </th>
                            <th style="border:1px solid #cdd2dd; width:7%;">
                                <label id="lblbhs_firstage">Age </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:4%;">
                                <label id="lblbhs_firstsex">Sex </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_firstmrno">MR No </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:26%;">
                                <label id="lblbhs_firstpayer">Payer </label> 
                            </th>
                            
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_firststatus">Status </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_firsttime">Time </label> 
                            </th>
                        </tr>                          
                        <asp:Repeater ID="RepeaterPasienFirstCome" runat="server">
                            <ItemTemplate>
                                <tr>
                                <tr>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                    
                                        <asp:Label ID="LabelFromTime" runat="server" Text='<%# Eval("AppointmentFromTime").ToString().Substring(0,5) %>' Visible='<%# Eval("TempIndex").ToString() == "0" && Eval("IsVIP").ToString() != "True" %>'></asp:Label> <asp:Label ID="Labelstrip" runat="server" Text="-" Visible='<%# Eval("TempIndex").ToString() == "0" && Eval("IsVIP").ToString() != "True" %>'></asp:Label>  <asp:Label ID="LabelToTime" runat="server" Text='<%# Eval("AppointmentToTime").ToString().Substring(0,5) %>' Visible='<%# Eval("TempIndex").ToString() == "0" && Eval("IsVIP").ToString() != "True" %>'></asp:Label>
                                    </td>
                                    <%--<td style="border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %>'>                      
                                        <asp:Label ID="LabelAppointNo" runat="server" Text='<%# Eval("AppointmentNo") %>'></asp:Label>
                                    </td>--%>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                       
                                        <asp:Label ID="lblqueueno" runat="server" Text='<%# Bind("QueueNo") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:HyperLink ID="lblAdmissionId" runat="server" style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "color:#8F701C;": "color:#1a2269;") %>'   Visible='<%# Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() %>' NavigateUrl='<%# String.Format("~/Form/General/PatientDashboard.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO")) %>'>
	<%--										<asp:LinkButton Text="" runat="server" ID="lb_PatientName" OnClick="lb_PatientName_Click" />--%>
                                            <label style="font-weight:bold; text-decoration:underline; cursor:pointer;" onmousedown=<%# "klikPatient('" + Eval("AdmissionId") + "','" + Eval("alertList") + "','" + Eval("AppointmentAdmissionId") + "','" + Eval("AppointmentHospitalId")  + "','" + Eval("AppointmentDoctorId") + "','" + Eval("PatientName").ToString().Replace(" ","_").Replace("'","_")  + "','" + Eval("AppointmentId") + "','" + Eval("IsAIDO").ToString().ToLower() + "','" + String.Format("PatientDashboard.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}&ZoomLink={7}&ApptAdmID={8}&ApptOrgID={9}&ApptDocID={10}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO"), Eval("ZoomLink"),Eval("AppointmentAdmissionId"), Eval("AppointmentHospitalId"), Eval("AppointmentDoctorId")) + "')" %> >  <%# Eval("PatientName") %>  </label> </asp:HyperLink>
                                        <asp:Label ID="lblPatientNameDisable" runat="server" Visible='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() %>' style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "color:#8F701C;": "color:#1a2269;") %>' Text='<%# Bind("PatientName") %>'></asp:Label>

										<i id="iconKewaspadaan" title='<%#"kewaspadaan: "+ Eval("alertList").ToString() %>' runat="server" class="fa fa-exclamation-circle" style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "display:inline-block; color:#FBC531; font-size:14px": "display:none") %>'></i>


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
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <%--<asp:Button ID="ButtonCallAppoint" runat="server" Text="Call" CssClass="btn btn-lightGreen btn-sm" Visible='<%# Eval("AdmissionId").ToString() != "0" %>' style="padding-top:1px; padding-bottom:1px; width: 45px;" OnClientClick="iscall(this.id);" OnClick="ButtonCallAppoint_Click" />--%>
                                        <div id="divcallappointment" runat="server" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "false" %>'>
                                            <asp:Button ID="ButtonCallAppoint" runat="server" Text="Call" CssClass="btn btn-lightGreen btn-sm btn-call" Visible='<%# Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("QueueNo").ToString() != "" %>' style="padding-top:1px; padding-bottom:1px; width: 45px;" OnClientClick=<%# "klikCall('" + Eval("AppointmentAdmissionId") + "','" + Eval("AppointmentHospitalId")  + "','" + Eval("AppointmentDoctorId") + "','" + Eval("PatientName").ToString().Replace(" ","_").Replace("'","_") + "','" + Eval("QueueNo") + "','" + "MainContent_RepeaterPasienFirstCome_ButtonCallAppoint_" + Container.ItemIndex  + "')" %> />
                                            <asp:Label ID="ButtonNotCallAppoint" runat="server" Text="Call" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "btn btn-sm callDisableNotCheckin" : "btn btn-sm callDisableNormal" %>' Visible='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() || Eval("QueueNo").ToString() == "" %>'></asp:Label>
                                        </div>
                                        <asp:HiddenField ID="HFAppointmentAdmissionId" runat="server" Value='<%# Eval("AppointmentAdmissionId").ToString() %>' />
                                        <asp:HiddenField ID="HFAppointmentHospitalId" runat="server" Value='<%# Eval("AppointmentHospitalId").ToString() %>' />
                                        <asp:HiddenField ID="HFAppointmentDoctorId" runat="server" Value='<%# Eval("AppointmentDoctorId").ToString() %>' />
                                        <a href='<%# Eval("ZoomLink") %>' target="_blank" onclick=<%# "klikZoom('" + Eval("EncounterId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + Eval("ZoomLink") +  "')" %> >
                                            <asp:Label ID="ButtonCallZoom" runat="server" Text="Zoom" style="padding-top:1px; padding-bottom:1px; padding-left:7px; width: 45px;"  CssClass="btn btn-twitter btn-sm" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "true" && TeleConfig == "False" %>'  ></asp:Label> 
                                        </a>
                                        <asp:Label ID="ButtonCallZoomInternal" runat="server" Text="Zoom" style="padding-top:1px; padding-bottom:1px; padding-left:7px; width: 45px;"  CssClass="btn btn-twitter btn-sm" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "true" && TeleConfig == "True" %>' onclick=<%# "klikDirectSoapTele('" + Eval("EncounterId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + String.Format("../SOAP/Template/StdSoapTeleconsultation.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}&DirectTele={7}&ZoomLink={8}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO"),"True", Eval("ZoomLink")) + "')" %> ></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:ImageButton ID="labResult" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labE.png" runat="server" Visible='<%# Eval("isLab").ToString() != "0" %>' OnClick="labResult_Click" OnClientClick='<%# "openLabResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                        <asp:ImageButton class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labD.png" runat="server" Enabled="false" Visible='<%# Eval("isLab").ToString() != "1" %>' />
                                        <asp:ImageButton ID="radResult" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Radiology" ImageUrl="~/Images/Icon/radE.png" runat="server" Visible='<%# Eval("isRad").ToString() != "0" %>' OnClick="radResult_Click" OnClientClick='<%# "openRadResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                        <asp:ImageButton class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Radiology" ImageUrl="~/Images/Icon/radD.png" runat="server" Enabled="false" Visible='<%# Eval("isRad").ToString() != "1" %>' />
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:Label ID="lblage" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                       
                                        <asp:Label ID="lblgender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:Label ID="lbllocalmrno" runat="server" Text='<%# Bind("LocalMrNo") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:Label ID="lblpayername" runat="server" Text='<%# Bind("PayerName") %>'></asp:Label>
                                    </td>
                                    
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                    &nbsp;    
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                            <tr id="rownodata_firstcome" runat="server">
                                <td colspan="12" style="text-align:center; padding: 10px; font-size: 15px; color: darkgrey;">
                                    <i class="fa fa-ban"></i> No Data
                                </td>
                            </tr>
                        </table>
                        </div>

                        <div id="divcancelworklist" style="margin-top:-20px;">
                        <%--<b>DAFTAR PASIEN CANCEL</b> <br /> --%>
                        <table style="width:100%; border:1px solid #cdd2dd;" border="1" class="table table-condensed table-hover-worklist">                         
                        <tr style="display:none;">
                            <th style="border:1px solid #cdd2dd; width:8%;">
                                Time Slot
                            </th>
                            <%--<th style="border:1px solid #cdd2dd; width:4%;">
                                No
                            </th>--%>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                Queue
                            </th>
                            <th style="border:1px solid #cdd2dd; width:21%;">
                                Patient Name
                            </th>
                            <th style="border:1px solid #cdd2dd; width:5%;">
                                &nbsp;
                            </th>
                            <th style="border:1px solid #cdd2dd; width:5%;">
                                &nbsp;
                            </th>
                            <th style="border:1px solid #cdd2dd; width:7%;">
                                Age
                            </th>
                            <th style="border:1px solid #cdd2dd; width:4%;">
                                Sex
                            </th>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                MR No
                            </th>
                            <th style="border:1px solid #cdd2dd; width:26%;">
                                Payer
                            </th>
                            
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                Status
                            </th>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                Time
                            </th>
                        </tr> 
                        <asp:Repeater ID="RepeaterPasienCancel" runat="server">
                            <ItemTemplate>
                                <tr>

                                    <td  style="color:#1a2269; border:1px solid #cdd2dd; width: 8%;" class='<%# Eval("Status").ToString() == "Cancelled" ? "warnaBGCancel" : "" %>'>                    
                                        <%--<asp:Label ID="LabelFromTime" runat="server" Text='<%# Eval("AppointmentFromTime").ToString().Substring(0,5) %>' Visible='<%# Eval("TempIndex").ToString() == "0" %>'></asp:Label> <asp:Label ID="Labelstrip" runat="server" Text="-" Visible='<%# Eval("TempIndex").ToString() == "0" %>'></asp:Label>  <asp:Label ID="LabelToTime" runat="server" Text='<%# Eval("AppointmentToTime").ToString().Substring(0,5) %>' Visible='<%# Eval("TempIndex").ToString() == "0" %>'></asp:Label>--%>
                                    </td>
                                    <%--<td style="border:1px solid #cdd2dd; width:4%;" class='<%# Eval("Status").ToString() == "Cancelled" ? "warnaBGCancel" : "" %>'>                      
                                        //<asp:Label ID="LabelAppointNo" runat="server" Text='<%# Eval("AppointmentNo") %>'></asp:Label>
                                    </td>--%>
                                    <td  style="color:#1a2269; border:1px solid #cdd2dd; width: 6%" class='<%# Eval("Status").ToString() == "Cancelled" ? "warnaBGCancel" : "" %>'>                       
                                        <asp:Label ID="lblqueueno" runat="server" Text='<%# Bind("QueueNo") %>'></asp:Label>
                                    </td>
                                    <td  style="color:#1a2269; border:1px solid #cdd2dd; width:21%" class='<%# Eval("Status").ToString() == "Cancelled" ? "warnaBGCancel" : "" %>'>                      
                                        <asp:HyperLink ID="lblAdmissionId" runat="server" style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "color:#8F701C;": "color:#1a2269;") %>'  Visible='<%# Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() %>' NavigateUrl='<%# String.Format("~/Form/General/PatientDashboard.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO")) %>'> <label style="font-weight:bold; text-decoration:underline; cursor:pointer;" onclick="showLoad();">  <%# Eval("PatientName") %>  </label> </asp:HyperLink>
                                        <asp:Label ID="lblPatientNameDisable" runat="server" Visible='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() %>' style=" color:#1a2269;" Text='<%# Bind("PatientName") %>'></asp:Label>

										<i id="iconKewaspadaan" title='<%#"kewaspadaan: "+ Eval("alertList").ToString() %>' runat="server" class="fa fa-exclamation-circle" style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "display:inline-block; color:#FBC531; font-size:14px": "display:none") %>'></i>


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
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" width:5%;" class='<%# Eval("Status").ToString() == "Cancelled" ? "warnaBGCancel" : "" %>'>                      
                                        <%--<asp:Button ID="ButtonCallAppoint" runat="server" Text="Call" CssClass="btn btn-lightGreen btn-sm" Visible='<%# Eval("AdmissionId").ToString() != "0" %>' style="padding-top:1px; padding-bottom:1px; width: 45px;" OnClick="ButtonCallAppoint_Click" />--%>
                                        <asp:Label ID="ButtonNotCallAppoint" runat="server" Text="Call" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "btn btn-sm callDisableNotCheckin" : "btn btn-sm callDisableNormal" %>' Visible='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() || Eval("QueueNo").ToString() == "" %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" width:5%;" class='<%# Eval("Status").ToString() == "Cancelled" ? "warnaBGCancel" : "" %>'>                      
                                        <asp:ImageButton ID="labResult" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labE.png" runat="server" Visible='<%# Eval("isLab").ToString() != "0" %>' OnClick="labResult_Click" OnClientClick='<%# "openLabResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                        <asp:ImageButton class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labD.png" runat="server" Enabled="false" Visible='<%# Eval("isLab").ToString() != "1" %>' />
                                        <asp:ImageButton ID="radResult" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Radiology" ImageUrl="~/Images/Icon/radE.png" runat="server" Visible='<%# Eval("isRad").ToString() != "0" %>' OnClick="radResult_Click" OnClientClick='<%# "openRadResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                        <asp:ImageButton class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Radiology" ImageUrl="~/Images/Icon/radD.png" runat="server" Enabled="false" Visible='<%# Eval("isRad").ToString() != "1" %>' />
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" width:7%;" class='<%# Eval("Status").ToString() == "Cancelled" ? "warnaBGCancel" : "" %>'>                      
                                        <asp:Label ID="lblage" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd; width:4%;" class='<%# Eval("Status").ToString() == "Cancelled" ? "warnaBGCancel" : "" %>'>                       
                                        <asp:Label ID="lblgender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd; width:6%;" class='<%# Eval("Status").ToString() == "Cancelled" ? "warnaBGCancel" : "" %>'>                      
                                        <asp:Label ID="lbllocalmrno" runat="server" Text='<%# Bind("LocalMrNo") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd; width:26%;" class='<%# Eval("Status").ToString() == "Cancelled" ? "warnaBGCancel" : "" %>'>                      
                                        <asp:Label ID="lblpayername" runat="server" Text='<%# Bind("PayerName") %>'></asp:Label>
                                    </td>
                                    
                                    <td style="color:#1a2269; border:1px solid #cdd2dd; width:6%;" class='<%# Eval("Status").ToString() == "Cancelled" ? "warnaBGCancel" : "" %>'>                      
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd; width:6%;" class='<%# Eval("Status").ToString() == "Cancelled" ? "warnaBGCancel" : "" %>'> 
                                        &nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                        </table>
                        </div>

                        <div id="divwaitingworklist" runat="server">
                        <b> <label id="lblbhs_waitingtitle"> WAITING LIST </label> </b> <br /> 
                        <table id="tableworklist_waiting" style="width:100%; border:1px solid #cdd2dd;" border="1" class="table table-condensed table-hover-worklist">
                        <tr style="background-color:#f4f4f4;">
                            <th style="border:1px solid #cdd2dd; width:8%;">
                                <label id="lblbhs_waitingtimeslot">Time Slot </label>    
                            </th>
                            <%--<th style="border:1px solid #cdd2dd; width:4%;">
                                <label id="lblbhs_waitingno">No </label>  
                            </th>--%>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_waitingvisitno">Visit No </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:21%;">
                                <label id="lblbhs_waitingpatientname">Patient Name </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:5%;">
                                &nbsp;
                            </th>
                            <th style="border:1px solid #cdd2dd; width:5%;">
                                &nbsp;
                            </th>
                            <th style="border:1px solid #cdd2dd; width:7%;">
                                <label id="lblbhs_waitingage">Age </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:4%;">
                                <label id="lblbhs_waitingsex">Sex </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_waitingmrno">MR No </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:26%;">
                                <label id="lblbhs_waitingpayer">Payer </label> 
                            </th>
                            
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_waitingstatus">Status </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_waitingtime">Time </label> 
                            </th>
                        </tr>                          
                        <asp:Repeater ID="RepeaterPasienWaiting" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                    
                                        <asp:Label ID="LabelFromTime" runat="server" Text='<%# Eval("AppointmentFromTime").ToString().Substring(0,5) %>' Visible='<%# Eval("TempIndex").ToString() == "0" %>'></asp:Label> <asp:Label ID="Labelstrip" runat="server" Text="-" Visible='<%# Eval("TempIndex").ToString() == "0" %>'></asp:Label>  <asp:Label ID="LabelToTime" runat="server" Text='<%# Eval("AppointmentToTime").ToString().Substring(0,5) %>' Visible='<%# Eval("TempIndex").ToString() == "0" %>'></asp:Label>
                                    </td>
                                    <%--<td style="border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %>'>                      
                                        <asp:Label ID="LabelAppointNo" runat="server" Text='<%# Eval("AppointmentNo") %>'></asp:Label>
                                    </td>--%>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                       
                                        <asp:Label ID="lblqueueno" runat="server" Text='<%# Bind("QueueNo") %>'></asp:Label>
                                    </td>
                                    <td  style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:HyperLink ID="lblAdmissionId" runat="server" style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "color:#8F701C;": "color:#1a2269;") %>'  Visible='<%# Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() %>' NavigateUrl='<%# String.Format("~/Form/General/PatientDashboard.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}&ZoomLink={7}&ApptAdmID={8}&ApptOrgID={9}&ApptDocID={10}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO"), Eval("ZoomLink"),Eval("AppointmentAdmissionId"), Eval("AppointmentHospitalId"), Eval("AppointmentDoctorId")) %>'> <label style="font-weight:bold; text-decoration:underline; cursor:pointer;" onmousedown=<%# "klikPatient('" + Eval("AdmissionId") + "','" + Eval("alertList") + "','" + Eval("AppointmentAdmissionId") + "','" + Eval("AppointmentHospitalId")  + "','" + Eval("AppointmentDoctorId") + "','" + Eval("PatientName").ToString().Replace(" ","_").Replace("'","_")  + "','" + Eval("AppointmentId") + "','" + Eval("IsAIDO").ToString().ToLower() + "','" + String.Format("PatientDashboard.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}&ZoomLink={7}&ApptAdmID={8}&ApptOrgID={9}&ApptDocID={10}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO"),Eval("ZoomLink"),Eval("AppointmentAdmissionId"), Eval("AppointmentHospitalId"), Eval("AppointmentDoctorId")) + "')" %> >  <%# Eval("PatientName") %>  </label> </asp:HyperLink>
                                        <asp:Label ID="lblPatientNameDisable" runat="server" Visible='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() %>' style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "color:#8F701C;": "color:#1a2269;") %>' Text='<%# Bind("PatientName") %>'></asp:Label>

										<i id="iconKewaspadaan" title='<%#"kewaspadaan: "+ Eval("alertList").ToString() %>' runat="server" class="fa fa-exclamation-circle" style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "display:inline-block; color:#FBC531; font-size:14px": "display:none") %>'></i>


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
                                    </td>
                                    <td style="border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <%--<asp:Button ID="ButtonCallAppoint" runat="server" Text="Call" CssClass="btn btn-lightGreen btn-sm" Visible='<%# Eval("AdmissionId").ToString() != "0" %>' style="padding-top:1px; padding-bottom:1px; width: 45px;" OnClientClick="iscall(this.id);" OnClick="ButtonCallAppoint_Click" />--%>
                                        <div id="divcallappointment" runat="server" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "false" %>'>
                                            <asp:Button ID="ButtonCallAppoint" runat="server" Text="Call" CssClass="btn btn-lightGreen btn-sm btn-call" Visible='<%# Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() && Eval("QueueNo").ToString() != "" %>' style="padding-top:1px; padding-bottom:1px; width: 45px;" OnClientClick=<%# "klikCall('" + Eval("AppointmentAdmissionId") + "','" + Eval("AppointmentHospitalId")  + "','" + Eval("AppointmentDoctorId") + "','" + Eval("PatientName").ToString().Replace(" ","_").Replace("'","_") + "','" + Eval("QueueNo") + "','" + "MainContent_RepeaterPasienWaiting_ButtonCallAppoint_" + Container.ItemIndex  + "')" %> />
                                            <asp:Label ID="ButtonNotCallAppoint" runat="server" Text="Call" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "btn btn-sm callDisableNotCheckin" : "btn btn-sm callDisableNormal" %>' Visible='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() || Eval("QueueNo").ToString() == "" %>'></asp:Label>
                                        </div>
                                        <asp:HiddenField ID="HFAppointmentAdmissionId" runat="server" Value='<%# Eval("AppointmentAdmissionId").ToString() %>' />
                                        <asp:HiddenField ID="HFAppointmentHospitalId" runat="server" Value='<%# Eval("AppointmentHospitalId").ToString() %>' />
                                        <asp:HiddenField ID="HFAppointmentDoctorId" runat="server" Value='<%# Eval("AppointmentDoctorId").ToString() %>' />
                                        <a href='<%# Eval("ZoomLink") %>' target="_blank" onclick=<%# "klikZoom('" + Eval("EncounterId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + Eval("ZoomLink") +  "')" %> >
                                            <asp:Label ID="ButtonCallZoom" runat="server" Text="Zoom" style="padding-top:1px; padding-bottom:1px; padding-left:7px; width: 45px;"  CssClass="btn btn-twitter btn-sm" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "true" && TeleConfig == "False" %>'  ></asp:Label> 
                                        </a>
                                        <asp:Label ID="ButtonCallZoomInternal" runat="server" Text="Zoom" style="padding-top:1px; padding-bottom:1px; padding-left:7px; width: 45px;"  CssClass="btn btn-twitter btn-sm" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "true" && TeleConfig == "True" %>' onclick=<%# "klikDirectSoapTele('" + Eval("EncounterId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + String.Format("../SOAP/Template/StdSoapTeleconsultation.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}&DirectTele={7}&ZoomLink={8}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO"),"True", Eval("ZoomLink")) + "')" %> ></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:ImageButton ID="labResult" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labE.png" runat="server" Visible='<%# Eval("isLab").ToString() != "0" %>' OnClick="labResult_Click" OnClientClick='<%# "openLabResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                        <asp:ImageButton class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labD.png" runat="server" Enabled="false" Visible='<%# Eval("isLab").ToString() != "1" %>' />
                                        <asp:ImageButton ID="radResult" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Radiology" ImageUrl="~/Images/Icon/radE.png" runat="server" Visible='<%# Eval("isRad").ToString() != "0" %>' OnClick="radResult_Click" OnClientClick='<%# "openRadResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                        <asp:ImageButton class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Radiology" ImageUrl="~/Images/Icon/radD.png" runat="server" Enabled="false" Visible='<%# Eval("isRad").ToString() != "1" %>' />
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:Label ID="lblage" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                       
                                        <asp:Label ID="lblgender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:Label ID="lbllocalmrno" runat="server" Text='<%# Bind("LocalMrNo") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:Label ID="lblpayername" runat="server" Text='<%# Bind("PayerName") %>'></asp:Label>
                                    </td>
                                    
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                      
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                    </td>
                                    <td style="color:#1a2269; border:1px solid #cdd2dd;" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "warnaBGnotcheckin" : "" %> <%# Eval("AdmissionId").ToString() != "0" && Eval("IsYellow").ToString().ToLower() == "true" ? "warnaBGCall3x" : "" %> <%# Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "WarnaBGKewaspadaan" : "" %>'>                                                           
                                        <label id="Waiting_ElementID_<%# Container.ItemIndex %>" class="timerwaiting" data-start='<%# Eval("AdmissionDate","{0:MMMM dd yyyy HH:mm}").ToString() %>' style='display:<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() || Eval("AppointmentFromTime").ToString().Substring(0, 5) == "00:00" ? "none" : "" %>'><label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                        </table>
                        </div>
                    </div>
                </div>

                <div style="background-color: transparent; padding-bottom: 15px;">
                    &nbsp;
                    <div style="background-color: white; width: 96%; border-radius: 7px 7px 7px 7px; padding-top: 10px; padding-bottom: 10px;" class="container-fluid">
                        <b> <label id="lblbhs_submittitle"> SUBMITTED PATIENT </label> </b> <br /> 
                        <table id="tableworklist_submit" style="width:100%; border:1px solid #cdd2dd;" border="1" class="table table-condensed table-hover-worklist">
                        <tr style="background-color:#f4f4f4;">
                            <th style="border:1px solid #cdd2dd; width:8%;">
                                <label id="lblbhs_submittimeslot">Time Slot </label>    
                            </th>
                            <%--<th style="border:1px solid #cdd2dd; width:4%;">
                                <label id="lblbhs_submitno">No </label>  
                            </th>--%>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_submitvisitno">Visit No </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:21%;">
                                <label id="lblbhs_submitpatientname">Patient Name </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:5%;">
                                &nbsp;
                            </th>
                            <th style="border:1px solid #cdd2dd; width:5%;">
                                &nbsp;
                            </th>
                            <th style="border:1px solid #cdd2dd; width:7%;">
                                <label id="lblbhs_submitage">Age </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:4%;">
                                <label id="lblbhs_submitsex">Sex </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_submitmrno">MR No </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:26%;">
                                <label id="lblbhs_submitpayer">Payer </label> 
                            </th>
                            
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_submitstatus">Status </label> 
                            </th>
                            <th style="border:1px solid #cdd2dd; width:6%;">
                                <label id="lblbhs_submittime">Time </label> 
                            </th>
                        </tr>                          
                        <asp:Repeater ID="RepeaterPasienSubmit" runat="server">
                            <ItemTemplate>
                                <tr>

                                    <td style="border:1px solid #cdd2dd;" class='<%# Eval("Status").ToString() == "Submit" ? "warnaBGSubmit" : "" %>'>                    
                                        <asp:Label ID="LabelFromTime" runat="server" Text='<%# Eval("AppointmentFromTime").ToString().Substring(0,5) %>' Visible='<%# Eval("TempIndex").ToString() == "0" && Eval("IsVIP").ToString() != "True" %>'></asp:Label> <asp:Label ID="Labelstrip" runat="server" Text="-" Visible='<%# Eval("TempIndex").ToString() == "0" && Eval("IsVIP").ToString() != "True" %>'></asp:Label>  <asp:Label ID="LabelToTime" runat="server" Text='<%# Eval("AppointmentToTime").ToString().Substring(0,5) %>' Visible='<%# Eval("TempIndex").ToString() == "0" && Eval("IsVIP").ToString() != "True" %>'></asp:Label>
                                    </td>
                                    <%--<td style="border:1px solid #cdd2dd;" class='<%# Eval("Status").ToString() == "Submit" ? "warnaBGSubmit" : "" %>'>                      
                                        <asp:Label ID="LabelAppointNo" runat="server" Text='<%# Eval("AppointmentNo") %>'></asp:Label>
                                    </td>--%>
                                    <td style="border:1px solid #cdd2dd;" class='<%# Eval("Status").ToString() == "Submit" ? "warnaBGSubmit" : "" %>'>                       
                                        <asp:Label ID="lblqueueno" runat="server" Text='<%# Bind("QueueNo") %>'></asp:Label>
                                    </td>
                                    <td style="border:1px solid #cdd2dd;" class='<%# Eval("Status").ToString() == "Submit" ? "warnaBGSubmit" : "" %>'>                      
                                        <asp:HyperLink ID="lblAdmissionId" runat="server" style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "color:#8F701C;": "color:#1a2269;") %>'  Visible='<%# Eval("AdmissionId").ToString() != "0" && Eval("EncounterId").ToString() != Guid.Empty.ToString() %>' NavigateUrl='<%# String.Format("~/Form/General/PatientDashboard.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO")) %>'> <label style="font-weight:bold; text-decoration:underline; cursor:pointer;" onclick="showLoad();" onmousedown=<%# "kliksetalert('" + Eval("AdmissionId") + "','" + Eval("alertList") + "')" %>>  <%# Eval("PatientName") %>  </label> </asp:HyperLink>
                                        <asp:Label ID="lblPatientNameDisable" runat="server" Visible='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() %>' style=" color:#1a2269;" Text='<%# Bind("PatientName") %>'></asp:Label>

										<i id="iconKewaspadaan" title='<%#"kewaspadaan: "+ Eval("alertList").ToString() %>' runat="server" class="fa fa-exclamation-circle" style='<%# (Eval("isInfectious").ToString().ToUpper() == "TRUE" ? "display:inline-block; color:#FBC531; font-size:14px": "display:none") %>'></i>


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
                                    </td>
                                    <td style="border:1px solid #cdd2dd;" class='<%# Eval("Status").ToString() == "Submit" ? "warnaBGSubmit" : "" %>'>                      
                                        <%--<asp:Button ID="ButtonCallAppoint" runat="server" Text="Call" CssClass="btn btn-lightGreen btn-sm" Visible='<%# Eval("AdmissionId").ToString() != "0" %>' style="padding-top:1px; padding-bottom:1px; width: 45px;" OnClick="ButtonCallAppoint_Click" />--%>
                                        <div id="divcallappointment" runat="server" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "false" %>'>
                                            <asp:Label ID="ButtonNotCallAppoint" runat="server" Text="Call" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "btn btn-sm callDisableNotCheckin" : "btn btn-sm callDisableNormal" %>'></asp:Label>
                                        </div>
                                        <a href='<%# Eval("ZoomLink") %>' target="_blank" onclick=<%# "klikZoom('" + Eval("EncounterId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + Eval("ZoomLink") +  "')" %> >
                                            <asp:Label ID="ButtonCallZoom" runat="server" Text="Zoom" style="padding-top:1px; padding-bottom:1px; padding-left:7px; width: 45px;"  CssClass="btn btn-twitter btn-sm" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "true" && TeleConfig == "False" %>'  ></asp:Label> 
                                        </a>
                                        <asp:Label ID="ButtonCallZoomInternal" runat="server" Text="Zoom" style="padding-top:1px; padding-bottom:1px; padding-left:7px; width: 45px;"  CssClass="btn btn-twitter btn-sm" Visible='<%# Eval("IsAIDO").ToString().ToLower() == "true" && TeleConfig == "True" %>' onclick=<%# "klikDirectSoapTele('" + Eval("EncounterId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + String.Format("../SOAP/Template/StdSoapTeleconsultation.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}&AppointmentId={5}&IsTele={6}&DirectTele={7}&ZoomLink={8}", Eval("PatientId"), Eval("AdmissionId"), Eval("EncounterId"), Eval("pageFA"), Eval("pageSOAP"), Eval("AppointmentId"), Eval("IsAIDO"),"True", Eval("ZoomLink")) + "')" %> ></asp:Label>
                                    </td>
                                    <td style="border:1px solid #cdd2dd;" class='<%# Eval("Status").ToString() == "Submit" ? "warnaBGSubmit" : "" %>'>                      
                                        <asp:ImageButton ID="labResult" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labE.png" runat="server" Visible='<%# Eval("isLab").ToString() != "0" %>' OnClick="labResult_Click" OnClientClick='<%# "openLabResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                        <asp:ImageButton class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labD.png" runat="server" Enabled="false" Visible='<%# Eval("isLab").ToString() != "1" %>' />
                                        <asp:ImageButton ID="radResult" class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Radiology" ImageUrl="~/Images/Icon/radE.png" runat="server" Visible='<%# Eval("isRad").ToString() != "0" %>' OnClick="radResult_Click" OnClientClick='<%# "openRadResultModal(/" + Eval("encounterId") + "/ ," + Eval("patientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                        <asp:ImageButton class='<%# Eval("AdmissionId").ToString() == "0" || Eval("EncounterId").ToString() == Guid.Empty.ToString() ? "sizeImgnotcheckin" : "sizeImgnormal" %>' ToolTip="View Radiology" ImageUrl="~/Images/Icon/radD.png" runat="server" Enabled="false" Visible='<%# Eval("isRad").ToString() != "1" %>' />
                                    </td>
                                    <td style="border:1px solid #cdd2dd;" class='<%# Eval("Status").ToString() == "Submit" ? "warnaBGSubmit" : "" %>'>                      
                                        <asp:Label ID="lblage" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                                    </td>
                                    <td style="border:1px solid #cdd2dd;" class='<%# Eval("Status").ToString() == "Submit" ? "warnaBGSubmit" : "" %>'>                       
                                        <asp:Label ID="lblgender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                    </td>
                                    <td style="border:1px solid #cdd2dd;" class='<%# Eval("Status").ToString() == "Submit" ? "warnaBGSubmit" : "" %>'>                      
                                        <asp:Label ID="lbllocalmrno" runat="server" Text='<%# Bind("LocalMrNo") %>'></asp:Label>
                                    </td>
                                    <td style="border:1px solid #cdd2dd;" class='<%# Eval("Status").ToString() == "Submit" ? "warnaBGSubmit" : "" %>'>                      
                                        <asp:Label ID="lblpayername" runat="server" Text='<%# Bind("PayerName") %>'></asp:Label>
                                    </td>
                                    
                                    <td style="border:1px solid #cdd2dd;" class='<%# Eval("Status").ToString() == "Submit" ? "warnaBGSubmit" : "" %>'>                      
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                    </td>
                                    <td style="border:1px solid #cdd2dd;" class='<%# Eval("Status").ToString() == "Submit" ? "warnaBGSubmit" : "" %>'>
                                    &nbsp;    
                                    </td>
                                </tr>
                            </ItemTemplate>
                            
                        </asp:Repeater>
                            <tr id="rownodata_submit" runat="server">
                                <td colspan="12" style="text-align:center; padding: 10px; font-size: 15px; color: darkgrey;">
                                    <i class="fa fa-ban"></i> No Data
                                </td>
                            </tr>

                        </table>
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

        <%-- ============================================= MODAL CHOOSE Schedule ============================================== --%>
        <div class="modal fade" id="modalChooseSchedule" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <asp:Panel ID="panelschedule" runat="server" DefaultButton="ButtonCheckin">
                <div class="modal-dialog" style="width: 35%;padding-top:10%">
                    <div class="modal-content" style="height: 100%; border-radius: 5px">

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px; border-bottom:none;">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="modal-body">
                            <div style="width: 100%;">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div style="padding:5px; text-align:center;">
                                            <label style="font-size:16px;">Please Check-in to your room to call patient</label>
                                            <br />
                                            <br />
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width:40%; text-align:left; padding:5px;">
                                                        Select Schedule
                                                    </td>
                                                    <td style="width:60%; text-align:left; padding:5px;">
                                                        Select Room
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding:5px;">
                                                        <asp:DropDownList ID="dropdownSchedule" runat="server" CssClass="selecpicker" data-live-search="true" data-size="10" data-width="100%" data-dropup-auto="false" Style="font-size: 14px; width:200px;" OnSelectedIndexChanged="dropdownSchedule_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding:5px;">
                                                        <asp:DropDownList ID="dropdownRoom" runat="server" CssClass="selecpicker" data-live-search="true" data-size="10" data-width="100%" data-dropup-auto="false" Style="font-size: 14px; width:200px;">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="modal-footer" style="width: 100%; text-align:center; border-top:none;">
                            <asp:UpdateProgress ID="UpdateProgressCheckin" runat="server" AssociatedUpdatePanelID="UPCheckIn">
                                <ProgressTemplate>
                                    <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                    </div>
                                    &nbsp;
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel runat="server" ID="UPCheckIn">
                                <ContentTemplate>
                                    <asp:Button runat="server" Text="Check In" CssClass="btn btn-success btn-sm" class="box" Width="30%" ID="ButtonCheckin" OnClientClick="return ValidasiCheckin();" OnClick="ButtonCheckin_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <%-- ============================================= END OF MODAL CHOOSE ORG ============================================== --%>


    </body>

</asp:Content>