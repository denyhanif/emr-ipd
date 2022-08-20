<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="Form_General_Result" %>

<%@ Register Src="~/Form/General/Control/PatientCard.ascx" TagPrefix="tag1" TagName="PatientCard" %>
<%@ Register Src="~/Form/General/Control/StdLabResult.ascx" TagPrefix="tag1" TagName="StdLabResult" %>
<%@ Register Src="~/Form/General/Control/StdRadResult.ascx" TagPrefix="tag1" TagName="StdRadResult" %>

<asp:Content ID="Result" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(window).load(function () {
            $(".loadPage").fadeOut("slow");
        });

        function addAdmissionList(content, count) {

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
        }

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
                    <asp:HiddenField ID="hfListAdmission" runat="server" />
                    <tag1:PatientCard runat="server" ID="PatientCard" />
                </div>

                <div style="width: 100%; height: 45px; padding-left: 15px; background-color: #e7e8ed;">
                    <asp:LinkButton ID="labResult" runat="server" OnClick="labResult_Click">
                        <div id="labResult_div" runat="server" class="col-sm-1" style="top: 15%; width: 130px; text-align: center; margin-right: 5px; margin-left: 5px; padding: 6px 3px 3px 3px; border-radius: 5px; border: 1px solid #bdbfd8; background-color: #d6dbff;">
                            <span>
                                <img src="<%= Page.ResolveClientUrl("~/Images/Result/ic_Lab.svg") %>" style="height: 25px; width: 25px; margin-right: 3px; margin-top: -3px;" /></span>
                            <b>Laboratory </b>
                        </div>
                    </asp:LinkButton>
                    <asp:LinkButton ID="radResult" runat="server" OnClick="radResult_Click">
                        <div id="radResult_div" runat="server" class="col-sm-1" style="top: 15%; width: 130px; text-align: center; margin-right: 5px; margin-left: 5px; padding: 6px 3px 3px 3px; border-radius: 5px; border: 1px solid #bdbfd8;">
                            <span>
                                <img src="<%= Page.ResolveClientUrl("~/Images/Result/ic_Rad.svg") %>" style="height: 25px; width: 25px; margin-right: 3px; margin-top: -3px;" /></span>
                            <b>Radiology </b>
                        </div>
                    </asp:LinkButton>
                    <div id="diagResult_div" runat="server" class="col-sm-1" style="top: 15%; width: 130px; text-align: center; margin-right: 5px; margin-left: 5px; padding: 6px 3px 3px 3px; border-radius: 5px; border: 1px solid #bdbfd8; display: none;">
                        <asp:LinkButton ID="diagResult" runat="server" OnClick="diagResult_Click" Style="display: none;">
                            <span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_Diag.png") %>" style="height:25px;width:25px;margin-right:3px;margin-top: -3px;" /></span>
                            <b> Diagnosis </b></asp:LinkButton>
                    </div>
                </div>

                <div class="container-fluid" style="height: 100%; margin-top: 10px;">
                    <div style="width: 100%; transform: translate(0,-0%); background-color: white; padding-bottom: 10px; min-height: calc(100vh - 195px); margin-bottom: 10px;" class="shadows col-sm-12">
                        <asp:MultiView ID="MainView" runat="server">
                            <asp:View ID="View1" runat="server">
                                <div style="width: 100%; height: 1%; padding-top: 10px">
                                    <div class="btn-group btn-group-justified" style="height: 40px; border-bottom: 1px solid #cdd2dd;" role="group" aria-label="...">
                                        <div class="btn-group" role="group" style="width: 1%" runat="server" visible="false">
                                            <div class="container-fluid">
                                                Last Visit
                                                    <br />
                                                <asp:DropDownList ID="ddlEncounterMode" runat="server" Height="25px" Width="130px" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none;">
                                                    <asp:ListItem Text="Last Visit" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Last 3 Visits" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Last 5 Visits" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Last 7 Visits" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Last 10 Visits" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="Last 25 Visits" Value="25"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="btn-group" role="group" style="width: 0.1%" runat="server" visible="false">
                                            <br />
                                            OR
                                        </div>
                                        <div class="btn-group" role="group" style="width: 1%" runat="server" visible="false">
                                            <div class="container-fluid">
                                                Search by admission or doctor
                                                     <br />
                                                <asp:TextBox runat="server" ID="txt_admissionNo_doctorName" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none;" placeholder="Admission No - Doctor Name"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="btn-group" role="group" style="width: 2%" runat="server" visible="false">
                                            <div class="container-fluid">
                                                <br />
                                                <asp:Button ID="btnSearch" runat="server" Width="70px" Text="Search" CssClass="btn btn-success" OnClientClick="btnSearch();" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                        <div class="btn-group" role="group" style="width: 100%">
                                            <div>
                                                <asp:Label runat="server" Font-Bold="true" Font-Size="Larger" Width="80%">
                                                        <%--<label style="display: <%=setENG%>;"> Last 10 Lab Order Result </label>
                                                        <label style="display: <%=setIND%>;"> 10 Hasil Lab Terakhir </label>--%>
                                                        <label id="lblbhs_lastlaborder"> Last 10 Lab Order Result </label>
                                                </asp:Label>
                                                <asp:LinkButton ID="img_compare" runat="server" Style="float: right;"><span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_Compare.png") %>" style="height:25px;width:25px;margin-right:3px;" /></span><b> Compare </b></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <tag1:StdLabResult runat="server" ID="StdLabResult" />
                                </div>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <asp:UpdatePanel ID="upRadiologyResult" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <div class="text-center" style="padding-top: 10px; padding-bottom: 10px;">
                                            <div class="row">
                                                <div class="col-sm-4">&nbsp;</div>
                                                <div class="col-sm-4 text-center">
                                                    <asp:LinkButton runat="server" Style="margin-right: 15px; padding: 0px; height: 30px; box-shadow: none;" CssClass="btn" onmouseover="this.style.color='#4d9b35';" onmouseout="this.style.color='#444444';" Font-Size="25px" ID="btn_prev_ono" OnClick="btn_prev_ono_Click"> <i class="icon-ic_PreviousCalendar"></i> </asp:LinkButton>
                                                    <asp:Label runat="server" CssClass="btn btn-default" Style="background-color: transparent; outline-color: transparent; border: 0px; box-shadow: none;" Font-Size="20px" Font-Bold="true" ID="lblYear"></asp:Label>
                                                    <asp:LinkButton runat="server" Style="margin-left: 15px; padding: 0px; height: 30px; box-shadow: none;" CssClass="btn" onmouseover="this.style.color='#4d9b35';" onmouseout="this.style.color='#444444';" Font-Size="25px" ID="btn_next_ono" Enabled="false" OnClick="btn_next_ono_Click"> <i class="icon-ic_NextCalendar"></i> </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-4 text-right" style="padding-top: 10px;">
                                                    <asp:Button CssClass="btn btn-emr-small btn-lightGreen" ID="btnDetailRadiology" Text="Search" runat="server" OnClick="btnDetailRadiology_Click" />
                                                </div>
                                            </div>
                                            <div id="divDateRadiology" runat="server"></div>
                                        </div>

                                        <div id="gridRadRes" style="transform: translate(0,-0%); width: 100%;">
                                            <asp:UpdateProgress ID="uProgWorklist" runat="server" AssociatedUpdatePanelID="UPlabResult">
                                                <ProgressTemplate>
                                                    <div class="modal-backdrop" style="background-color: white; text-align: center">
                                                    </div>
                                                    <div style="margin-top: 50px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                                                        <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <tag1:StdRadResult runat="server" ID="StdRadResult" />
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </asp:View>
                            <asp:View ID="View3" runat="server">
                                <h1>Page for Diagnosis</h1>
                            </asp:View>
                            <asp:View ID="View4" runat="server">
                                <h1>Page For Compare</h1>
                            </asp:View>
                        </asp:MultiView>
                    </div>

                    <a class="item" href="javascript:topFunction();">
                        <div id="myIDtoTop" class="bottomMenuu hidee">
                            <span>
                                <img src="../../../Images/Result/ic_Arrow_Top.png" /></span>
                        </div>
                    </a>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>