<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PatientRecapOPD.aspx.cs" Inherits="Form_General_PatientRecapOPD" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="ContentRecapOPD" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
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
    </script>

    <style type="text/css">

        #MainContent_Gvw_RecapOPD th {
            border:1px solid #cdd2dd;
        }

        #MainContent_Gvw_RecapOPD td {
            border:1px solid #cdd2dd;
        }


    </style>

    <asp:UpdatePanel runat="server" ID="updateOPD">
        <ContentTemplate>

            <div class="container-fluid">
                <div style="background-color: white; text-align: center; min-height: calc(100vh - 80px); transform: translate(0,-0%); border-radius:6px 6px; margin-top: 15px; margin-bottom: 15px; padding:15px;">
                    <table style="width:100%;">
                        <tr>
                            <td style="width:50%;">
                                <table>
                                    <tr>
                                        <td>
                                            <h4 style="text-align:left;">OPD Patient Recap</h4>
                                        </td>
                                        <td>
                                            <asp:UpdateProgress ID="UpdateProgressOPDRecap" runat="server" AssociatedUpdatePanelID="updateOPD">
                                                <ProgressTemplate>
                                                    <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                    </div>
                                                    &nbsp;
                                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width:50%;">
                                <table style="width:100%;">
                                    <tr>
                                        <td>From date : <asp:TextBox class="form-control" runat="server" ID="DateTextboxStart" name="date" Style="width:50%; height: 25px; display:inline; font-size: 12px; background-color: white;" placeholder="dd/mm/yyyy" onmousedown="dateStart();" AutoCompleteType="Disabled" /></td>
                                        <td>To date : <asp:TextBox class="form-control" runat="server" ID="DateTextboxEnd" name="date" Style="width:50%; height: 25px; display:inline; font-size: 12px; background-color: white;" placeholder="dd/mm/yyyy" onmousedown="dateEnd();" AutoCompleteType="Disabled" /></td>
                                        <td>Search Patient : <asp:TextBox class="form-control" runat="server" ID="PatientSearch" name="search" Style="width:50%; height: 25px; display:inline; font-size: 12px" type="text" placeholder="patient name" /></td>
                                        <td><asp:Button ID="ButtonSearch" runat="server" Text="Search" CssClass="btn btn-lightGreen btn-emr-small" OnClick="ButtonSearch_Click" /></td>
                                        
                                    </tr>
                                </table>
                                
                            </td>
                        </tr>
                    </table>
                    
                <asp:GridView ID="Gvw_RecapOPD" runat="server" AutoGenerateColumns="false" CssClass="table table-condensed table-striped" DataKeyNames="admission_id" >
                    <Columns>
                        <asp:TemplateField HeaderText="No." ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                 <%#Container.DataItemIndex+1+ (int.Parse(offsetforui)) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Patient Name" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Left" DataField="patient_name"></asp:BoundField>
                        <asp:BoundField HeaderText="Payer" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Left" DataField="payer"></asp:BoundField>
                        
                    </Columns>
                </asp:GridView>
             
                <div id="img_noData" runat="server" visible="true">
                    <div style="margin-top: 5%; text-align: center; border-radius: 4px; margin-left: 1%; margin-right: 1%">
                        <div>
                            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                        </div>
                        <div runat="server" id="no_patient_data" visible="true">
                            <span>
                                <h3 style="font-weight: 700; color: #585A6F">Oops! There is no data</h3>
                            </span>
                            <span style="display:none; font-size: 14px; color: #585A6F; font-family: Arial, Helvetica, sans-serif">Please check MR number or search another MR number</span>
                        </div>
                    </div>

                </div>

                <div style="text-align:right;" id="div_pagination" runat="server">
                    <asp:Button ID="ButtonPrevPageOnprogress" runat="server" style="height: 25px; padding-top: 3px;" CssClass="btn btn-default btn-sm" Text="Prev" OnClick="ButtonPrevPageOnprogress_Click" />
                    <asp:Button ID="ButtonNextPageOnprogress" runat="server" style="height: 25px; padding-top: 3px;" CssClass="btn btn-default btn-sm" Text="Next" OnClick="ButtonNextPageOnprogress_Click" />
                    <asp:HiddenField ID="HFLimitOnprogress" runat="server" />
                    <asp:HiddenField ID="HFOffsetOnprogress" runat="server" />
                </div>
                
                </div>
            </div>

            

            <%--===================================================CONTENT START===================================================--%>
            <%--<div runat="server" id="divFrame" visible="false">
                <iframe name="myIframe" id="myIframe" runat="server" style="width: 100%; height: calc(100vh - 106px); border: none; margin-bottom: -6px;"></iframe>
            </div>--%>
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
</asp:Content>
