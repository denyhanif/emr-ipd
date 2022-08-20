<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PatientHistoryLite.aspx.cs" Inherits="Form_General_PatientHistoryLite" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .shadows-search {
            border: 1px;
            border-radius: 2px;
            padding: 7px 7px 7px 7px;
            height: 32px;
            border: solid 1px;
            border-color: #cdced9;
            margin-left: 5px;
            margin-right: 5px;
        }

        h3 {
            color: black;
            padding-left: 7px;
        }

        .verticalline {
            border-left: 1px dashed lightgrey;
            height: 45px;
            padding-left: 22px;
        }

        .btnSave {
            width: 140px;
            height: 32px;
            font-family: Helvetica;
            font-size: 12px;
            font-weight: bold;
            font-style: normal;
            font-stretch: normal;
            line-height: 1.17;
            border-radius: 4px;
            background-color: #4d9b35;
            color: white;
            border: none;
        }

            .btnSave:hover {
                width: 140px;
                height: 32px;
                font-family: Helvetica;
                font-size: 12px;
                font-weight: bold;
                font-style: normal;
                font-stretch: normal;
                line-height: 1.17;
                border-radius: 4px;
                background-color: #42852e;
                color: white;
                border: none;
            }
    </style>

    <body>
        <asp:UpdatePanel runat="server" ID="updateBIG" UpdateMode="Conditional">
            <ContentTemplate>

                <%--=========================================================SEARCH SECTION START===================================================--%>

                <div class="container-fluid">
                    <div id="searchSection" runat="server" class="row" style="border-bottom: 1px solid lightgrey; background-color: white;">
                        <div class="col-sm-3" style="display: inline-flex; padding-top: 15px; padding-right: 0px; min-height: 55px;">
                            <asp:Label runat="server" Text="MR No" Font-Names="Helvetica" Font-Size="12px" Style="padding-top: 5px;"></asp:Label>
                            <asp:TextBox runat="server" ID="txtMRno" CssClass="shadows-search" Font-Names="Helvetica" Height="24px" Width="50%" onkeypress="return CheckNumeric();" OnTextChanged="btnSearch_Click" AutoPostBack="true"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btnSave" Height="24px" Width="25%" OnClick="btnSearch_Click" />
                        </div>
                        <div class="col-sm-9" style="margin-left: -1px; min-height: 55px; padding-top: 5px; padding-bottom: 5px; padding-left: 0px;">
                            <div class="verticalline" runat="server" id="divLine">
                                <div>
                                    <asp:Label ID="lblNama" runat="server" Font-Bold="true" Font-Names="Helvetica" Font-Size="17px"></asp:Label>
                                </div>
                                <div>
                                    <asp:Label ID="lblDOBJudul" runat="server" Text="Tgl. Lahir" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                                    <asp:Label ID="lblDOB" runat="server" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                                    &nbsp;
                                    &nbsp;
                                    <asp:Label ID="lblAgeJudul" runat="server" Text="Umur" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                                    <asp:Label ID="lblAge" runat="server" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                                    &nbsp;
                                    &nbsp;
                                    <asp:Label ID="lblReligionJudul" runat="server" Text="Agama" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                                    <asp:Label ID="lblReligion" runat="server" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                                    &nbsp;
                                    &nbsp;
                                    <asp:Label ID="lblGenderJudul" runat="server" Text="Gender" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                                    <asp:Image ID="ImageICMale" runat="server" ImageUrl="~/Images/Worklist/ic_Male.png" style="height:16px; padding-bottom:2px;" Visible="false" />
                                    <asp:Image ID="ImageICFemale" runat="server" ImageUrl="~/Images/Worklist/ic_Female.png" style="height:15px; padding-bottom:2px;" Visible="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <%--===================================================SEARCH SECTION END===================================================--%>


                <div style="margin-top: 0%; text-align: center; border-radius: 4px; margin-left: 1%; margin-right: 1%">
                    <%--<asp:Label runat="server" ID="lblNoData" Text="No Medication History" Visible="true" Font-Names="Helvetica" Font-Size="25px"></asp:Label>--%>

                    <div id="img_noData" runat="server" visible="false">
                        <div>
                            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                        </div>
                        <%--<div runat="server" id="no_patient_today" visible="false">
                                <span>
                                    <h3 style="font-weight: 700; color: #585A6F">No patient yet, today</h3>
                                </span>
                                <span style="font-size: 14px; color: #585A6F">Please wait some more time or search another date</span>
                            </div>--%>
                        <div runat="server" id="no_patient_data" visible="false">
                            <span>
                                <h3 style="font-weight: 700; color: #585A6F">Oops! There is no data</h3>
                            </span>
                            <span style="font-size: 14px; color: #585A6F; font-family: Arial, Helvetica, sans-serif">Please check MR number or search another MR number</span>
                        </div>
                    </div>

                </div>

                <%--===================================================CONTENT START===================================================--%>
                <div runat="server" id="divFrame" visible="false">
                    <iframe name="myIframe" id="myIframe" runat="server" style="width: 100%; height: calc(100vh - 106px); border: none; margin-bottom: -6px;"></iframe>
                </div>
                <%--====================================================CONTENT END===================================================--%>
            </ContentTemplate>
        </asp:UpdatePanel>


        <script type="text/javascript">

            function CheckNumeric() {
                return event.keyCode >= 48 && event.keyCode <= 57 || event.keyCode == 46;
            }

            <%-- $(document).ready(function () {


                var status = document.getElementById('<%=hdnStatus.ClientID%>');

                if (status.value == "true") {
                    $('#CheckBoxStatus').checked = true;
                }

                else
                {
                    $('#CheckBoxStatus').checked = false;
                }

                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            $('.CheckBoxSwitch').bootstrapToggle();
                        }
                    });
                };

            }
            );--%>

        </script>

    </body>
</asp:Content>