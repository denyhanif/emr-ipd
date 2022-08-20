<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ResultLABRAD.aspx.cs" Inherits="Form_General_Result_ResultLABRAD" %>

<%@ Register Src="~/Form/General/Control/PatientCard.ascx" TagPrefix="tag1" TagName="PatientCard" %>
<%@ Register Src="~/Form/General/Control/StdLabResult.ascx" TagPrefix="tag1" TagName="StdLabResult" %>
<%@ Register Src="~/Form/General/Control/StdRadResult.ascx" TagPrefix="tag1" TagName="StdRadResult" %>

<asp:Content ID="Result" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(window).load(function () {
            $(".loadPage").fadeOut("slow");
        });

<%--        function addAdmissionList(content, count) {

            var listAdmissionId = document.getElementById('<%=hfListAdmission.ClientID%>').value;

            var stringAdmission = "";

            var trs = document.getElementById("tbl_date_lab").getElementsByTagName("tr");
            var bg_color = trs[1].cells[count].style.backgroundColor;

            if (bg_color == "orange") {
                trs[1].cells[count].style.backgroundColor = "grey";

                if (listAdmissionId != "")
                    stringAdmission = listAdmissionId + "," + content;
                else
                    stringAdmission = content;

            } else {
                trs[1].cells[count].style.backgroundColor = "orange";
                var stringAdmission = listAdmissionId.split(',').filter(item => !content.includes(item)).toString();
            }

            document.getElementById('<%=hfListAdmission.ClientID%>').value = stringAdmission;
        }

        function btnSearch() {
            document.getElementById('<%=hfListAdmission.ClientID%>').value = "";
        }

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

        $(document).ready(function () {

            //fungsi untuk action in postback updatepanel
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {

                prm.add_beginRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {

                        //gridRadRes.style.display = "none";
                    }
                });

                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {

                        //gridRadRes.style.display = "";
                    }
                });
            };
        });

        function switchBahasa() {
            var bahasa = document.getElementById('<%=HFisBahasa.ClientID%>').value;
            if (bahasa == "ENG") {
                if (document.getElementById('lblbhs_lastlaborder') != null) {
                    document.getElementById('lblbhs_lastlaborder').innerHTML = "Last 10 Lab Order Result";
                }
                if (document.getElementById('lblbhs_nodata') != null) {
                    document.getElementById('lblbhs_nodata').innerHTML = "Oops! There is no data";
                    document.getElementById('lblbhs_subnodata').innerHTML = "Please search another date or parameter";
                }
            }
            else if (bahasa == "IND") {
                if (document.getElementById('lblbhs_lastlaborder') != null) {
                    document.getElementById('lblbhs_lastlaborder').innerHTML = "10 Hasil Lab Terakhir";
                }
                if (document.getElementById('lblbhs_nodata') != null) {
                    document.getElementById('lblbhs_nodata').innerHTML = "Oops! Tidak ada data";
                    document.getElementById('lblbhs_subnodata').innerHTML = "Silakan cari tanggal atau parameter lain";
                }
            }
        }--%>

    </script>

    <style type="text/css">
        .shadows {
            border: 1px;
            border-radius: 10px;
            box-shadow: 0px 1px 5px #9293A0;
            margin-top: 0px;
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

    <asp:HiddenField ID="HFisBahasa" runat="server" />
    <asp:UpdatePanel runat="server" ID="UPlabResult" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="background-color: transparent">
                <div class="container-fluid kartu-pasien">
                    <asp:HiddenField ID="hfPatientId" runat="server" />
                    <asp:HiddenField ID="hfEncounterId" runat="server" />
                    <asp:HiddenField ID="hfAdmissionId" runat="server" />
                    <asp:HiddenField ID="hfPageSoapId" runat="server" />
                    <asp:HiddenField ID="hfAppointmentId" runat="server" />
                    <asp:HiddenField ID="hfIsTele" runat="server" />
                    <asp:HiddenField ID="hfListAdmission" runat="server" />
                    <tag1:PatientCard runat="server" ID="PatientCard" />
                </div>

                <asp:LinkButton ID="img_compare" runat="server" Style="    position: absolute; top: 135px; right: 40px;"><span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_Compare.png") %>" style="height:25px;width:25px;margin-right:3px;" /></span><b> Compare Lab </b></asp:LinkButton>
                <iframe name="myLabRadIframe" id="myLabRadIframe" runat="server" style="width: 100%; height: calc(100vh - 125px); border: none; margin-bottom: -6px;"></iframe>

                <a class="item" href="javascript:topFunction();">
                    <div id="myIDtoTop" class="bottomMenuu hidee">
                        <span>
                            <img src="../../../Images/Result/ic_Arrow_Top.png" /></span>
                    </div>
                </a>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
