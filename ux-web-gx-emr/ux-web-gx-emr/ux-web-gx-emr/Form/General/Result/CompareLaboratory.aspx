<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="CompareLaboratory.aspx.cs" Inherits="Form_General_Result_CompareLaboratory" %>

<%@ Register Src="~/Form/General/Control/PatientCard.ascx" TagPrefix="uc1" TagName="PatientCard" %>
<%@ Register Src="~/Form/General/Control/StdLabResult.ascx" TagPrefix="uc1" TagName="StdLabResult" %>

<asp:Content ID="CompareLaboratory" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(window).load(function () {
            $(".loadPage").fadeOut("slow");
        });

        function Open(content) {
            document.getElementById('<%=hf_ono_id.ClientID%>').value = content;
            document.getElementById('<%=btn_lab_result.ClientID%>').click();
            openLabResultModal();
            return true;
        }

        function openLabResultModal() {
            $('#laboratoryResult').modal('show');
            return true;
        }

        function hideItemGroupTest(content) {
            var divItem = document.getElementById(content);
            if (divItem.style.display == 'none') {
                divItem.style.display = 'block';
            } else {
                divItem.style.display = 'none';
            }

            var idIcon = content + "_";

            if (document.getElementById(content + '_').className == "glyphicon glyphicon-chevron-right") {
                $("[id$='" + idIcon + "']").removeClass('glyphicon glyphicon-chevron-right');
                $("[id$='" + idIcon + "']").addClass('glyphicon glyphicon-chevron-down');
            }
            else {
                $("[id$='" + idIcon + "']").removeClass('glyphicon glyphicon-chevron-down');
                $("[id$='" + idIcon + "']").addClass('glyphicon glyphicon-chevron-right');
            }
        }

        function checklistTestGroup(content) {
            var listTestGroup = document.getElementById('<%=hf_test_group.ClientID%>');

            if (listTestGroup.value == "") {
                listTestGroup.value = content;
            } else {
                var arrayCheckTestGroup = listTestGroup.value.split(',');

                var stringCheckTestGroup = "";
                var status = false;
                for (var i = 0; i < arrayCheckTestGroup.length; i++) {

                    if (arrayCheckTestGroup[i] != content) {
                        if (stringCheckTestGroup == "") {
                            stringCheckTestGroup = arrayCheckTestGroup[i];
                        }
                        else {
                            stringCheckTestGroup = stringCheckTestGroup + "," + arrayCheckTestGroup[i];
                        }

                    } else {
                        status = true;
                    }
                }

                if (status == false)
                    stringCheckTestGroup = stringCheckTestGroup + "," + content;

                listTestGroup.value = stringCheckTestGroup;
            }
        }

        $(document).ready(function () {

            //fungsi untuk action in postback updatepanel
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {

                prm.add_beginRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        gridLabRes.style.display = "none";
                    }
                });

                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        gridLabRes.style.display = "";
                    }
                });
            };
        });

    </script>

    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UPLabCompare">
        <ContentTemplate>
            <div class="container-fluid kartu-pasien">
                <asp:HiddenField ID="hfPatientId" runat="server" />
                <asp:HiddenField ID="hfEncounterId" runat="server" />
                <asp:HiddenField ID="hfAdmissionId" runat="server" />
                <asp:HiddenField ID="hfPagefaId" runat="server" />
                <asp:HiddenField ID="hfPageSoapId" runat="server" />
                <asp:HiddenField ID="hfAppointmentId" runat="server" />
                <asp:HiddenField ID="hfIsTele" runat="server" />
                <asp:HiddenField ID="hf_ono_id" Value="" runat="server" />
                <asp:HiddenField ID="hf_test_group" runat="server" />
                <uc1:PatientCard runat="server" ID="PatientCard" />
            </div>

            <div style="width: 100%; height: 45px; padding-left: 15px; padding-top: 10px; background-color: #e7e8ed;">
                <asp:LinkButton ID="btnCancelDrugs" runat="server" CssClass="btn btn-github btn-emr-medium" Style="width: 70px;"> <i class="fa fa-arrow-circle-left"></i> Back </asp:LinkButton>
            </div>

            <div class="row" style="background-color: white; margin-left: 0px; min-height: calc(100vh - 170px); width: 100%;">

                <div class="col-sm-3" style="padding-right: 0px; padding-left: 0px; border-right: 1px solid lightgrey; min-height: calc(100vh - 170px);">
                    <div style="padding: 0px 0px 5px 0px;">
                        <div style="margin-top: 10px; padding-left: 10px;">
                            <div class="has-feedback" style="width: 100%;">
                                <asp:TextBox runat="server" Style="border: 0px; outline-color: transparent;" ID="src_item_txt" Width="100%" placeholder="Search i.e hematology..." OnTextChanged="src_item_txt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <span class="fa fa-search form-control-feedback" style="margin-top: -6px; margin-right: 4px; z-index: 0;"></span>
                            </div>
                        </div>

                        <div style="margin-top: 10px; border-top: 1px solid #CDD2DD; padding-top: 8px; padding-left: 10px;">
                            <asp:Label Font-Bold="true" runat="server" ID="TextBox1" Text="Items"></asp:Label>
                            <asp:Button ID="btn_compare" runat="server" Text="Compare" CssClass="btn btn-lightGreen" Style="float: right; margin-right: 10px; height: 25px; padding-top: 1px;" OnClick="btn_compare_Click" />
                            <asp:HiddenField ID="hf_list_header_checked" runat="server" Value="" />
                        </div>

                        <div style="width: 100%; margin-top: 20px; overflow: hidden auto; max-height: 600px; padding-left: 10px;" class="scrollEMR">
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div runat="server" id="divLabTestGroup"></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <div class="col-sm-9" style="min-height: calc(100vh - 210px); background-color: white; transform: translate(0,-0%); overflow-y: auto; border-left: 1px solid lightgrey; margin-left: -1px;">

                    <asp:UpdateProgress ID="uProgLabCompare" runat="server" AssociatedUpdatePanelID="UPLabCompare">
                        <ProgressTemplate>
                            <div style="background-color: white; text-align: center; z-index: 5; position: fixed; width: 100%; left: 0px; height: calc(100vh - 210px);">
                                <div style="margin-top: 100px;">
                                    <img alt="" height="225px" width="225px" style="background-color: transparent; vertical-align: middle;" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                    <div id="gridLabRes">
                        <div id="compareResult" runat="server" visible="false">

                            <div class="btn-group" role="group" aria-label="..." style="height: 40px; right: auto; left: 87%; top: 5px;" id="pg_index_compare" runat="server" visible="false">
                                <asp:LinkButton runat="server" Style="margin-right: 15px; padding: 0px; height: 30px; box-shadow: none;" CssClass="btn" onmouseover="this.style.color='#4d9b35';" onmouseout="this.style.color='#444444';" Font-Size="25px" ID="btnPrev" OnClick="btnPrev_Click"> <i class="icon-ic_PreviousCalendar"></i> </asp:LinkButton>
                                <asp:Label CssClass="btn" runat="server" Style="padding-left: 5px; padding-right: 5px;" ID="lbl_count_paging" Text="1"></asp:Label>
                                <asp:LinkButton runat="server" Style="margin-left: 15px; padding: 0px; height: 30px; box-shadow: none;" CssClass="btn" onmouseover="this.style.color='#4d9b35';" onmouseout="this.style.color='#444444';" Font-Size="25px" ID="btnNext" OnClick="btnNext_Click"> <i class="icon-ic_NextCalendar"></i> </asp:LinkButton>
                            </div>

                            <div id="tbl_compare" runat="server"></div>

                            <div id="img_noData" runat="server" visible="false" style="text-align: center; margin-top: 95px">
                                <div>
                                    <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                                </div>
                                <div>
                                    <span>
                                        <h3 style="font-weight: 700; color: #585A6F">Oops! Please Select Data First</h3>
                                    </span>
                                    <span style="font-size: 14px; color: #585A6F">Please tick one or more data to compare</span>
                                </div>
                            </div>

                            <div id="img_noConnection" runat="server" visible="false" style="text-align: center;">
                                <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noConnection.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px;" />
                                <span>
                                    <h3 style="font-weight: 700; color: #585A6F">No internet connection</h3>
                                </span>
                                <span style="font-size: 14px; color: #585A6F">Please check your connection & refresh</span>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="laboratoryResult" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog" style="width: 75%;" runat="server">
                    <div class="modal-content" style="border-radius: 7px; height: 100%;">
                        <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                            <asp:Button ID="btn_lab_result" runat="server" CssClass="hidden" OnClick="btn_lab_result_Click" />
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" style="text-align: left">
                                <asp:Label ID="lblModalTitle" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server" Text="Laboratory Result"></asp:Label></h4>
                        </div>

                        <div class="modal-body">
                            <div style="width: 100%" class="btn-group" role="group">
                                <div style="background-color: whitesmoke">
                                    <uc1:PatientCard runat="server" ID="PatientCard1" />
                                </div>
                                &nbsp;
                            <div style="overflow-y: auto; height: 400px;" class="scrollEMR">
                                <uc1:StdLabResult runat="server" ID="StdLabResult" />
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>