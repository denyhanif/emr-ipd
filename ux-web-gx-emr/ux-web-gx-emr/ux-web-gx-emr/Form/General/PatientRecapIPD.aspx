<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PatientRecapIPD.aspx.cs" Inherits="Form_General_PatientRecapIPD" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="ContentRecapIPD" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">

        #MainContent_Gvw_RecapIPD th {
            border:1px solid #cdd2dd;
        }

        #MainContent_Gvw_RecapIPD td {
            border:1px solid #cdd2dd;
        }


    </style>

    <asp:UpdatePanel runat="server" ID="updateIPD">
        <ContentTemplate>

            <div class="container-fluid">
                <div style="background-color: white; text-align: center; min-height: calc(100vh - 80px); transform: translate(0,-0%); border-radius:6px 6px; margin-top: 15px; margin-bottom: 15px; padding:15px;">
                    <h4 style="text-align:left;">IPD Patient List</h4>
                <asp:GridView ID="Gvw_RecapIPD" runat="server" AutoGenerateColumns="false" CssClass="table table-condensed table-striped" DataKeyNames="admission_id">
                    <Columns>
                        <asp:TemplateField HeaderText="No." ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                 <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Patient Name" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" DataField="patient_name"></asp:BoundField>
                        <asp:BoundField HeaderText="Birth Date" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" DataField="patient_age"></asp:BoundField>
                        <asp:BoundField HeaderText="MR No." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" DataField="patient_mr"></asp:BoundField>
                        <asp:BoundField HeaderText="Room" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" DataField="procedure_room"></asp:BoundField>
                        <asp:BoundField HeaderText="Class" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" DataField="room_class"></asp:BoundField>
                        <asp:BoundField HeaderText="Illness" ItemStyle-Width="16%" ItemStyle-HorizontalAlign="Left" DataField="patient_diagnosis"></asp:BoundField>
                        <asp:BoundField HeaderText="Doctor Status" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" DataField="doctor_status"></asp:BoundField>
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
