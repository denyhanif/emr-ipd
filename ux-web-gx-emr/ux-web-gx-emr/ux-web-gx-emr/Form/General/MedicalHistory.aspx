<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MedicalHistory.aspx.cs" Inherits="Form_General_MedicalHistory" MaintainScrollPositionOnPostback="true" %>
<%@ Register Src="~/Form/General/Control/PatientCard.ascx" TagPrefix="uc1" TagName="PatientCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .shadows {
            border-radius: 10px;
            box-shadow: 0px 1px 5px;
            padding: 7px 7px 7px 7px;
            width: 100%;
        }

        .shadows-search {
            border: 1px; 
            border-radius: 10px;
            box-shadow: 0px 1px 5px #9293A0;
            padding: 7px 7px 7px 7px;
            width: 200px;
        }
        
        .itemcontainersave > div {
            padding-top:0px;
            padding-bottom:2px;
        }
     </style>
    <script type="text/javascript">
        function Open(content)
        {
            document.getElementById('<%=hfCompoundName.ClientID%>').value = content;
            document.getElementById('<%=btnSample.ClientID%>').click();
            openModal();
            return true;
        }
        
        function openModal(){
            $('#modalCompound').modal('show');
            return true;
        }
    </script>
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UPmedicalHistory">
        <ContentTemplate>
            <asp:Panel ID="medical_history_panel" runat="server" >
            <div style="height:90px;position:fixed;background-color:white; width:100%;transform:translate(0,-0%);z-index:1;box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.26);" class="container-fluid">
                <asp:HiddenField ID="hfPatientId" runat="server" />
                <asp:HiddenField ID="hfEncounterId" runat="server" />
                <asp:HiddenField ID="hfAdmissionId" runat="server" />
                <asp:HiddenField ID="hfPageSoapId" runat="server" />
                <asp:HiddenField ID="hfCompoundName" runat="server" />
                <uc1:PatientCard runat="server" ID="PatientCard" />
            </div>

            <div style="width:100%; height:100px; padding-top:90px">
                <section id="main_page" style="background-color:red; margin-top:-150px; padding-top:150px;"></section>
                <div class="btn-group btn-group-justified" style="background-color:whitesmoke;height:70px" role="group" aria-label="...">
                    <div class="btn-group" role="group" style="width:1%">
                        <div class="container-fluid">
                        Encounter Times <br />
                        <asp:DropDownList ID="ddlEncounterMode" runat="server" Height="25px" Width="130px" style="border-radius: 2px;border: solid 1px #cdced9;height: 24px;max-width:100%;width:100%;resize:none;">
                            <asp:ListItem Text="Last Encounter" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Last 5 Encounter" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Last 10 Encounter" Value="10"></asp:ListItem>
                        </asp:DropDownList>
                        </div>
                    </div>
                    <div class="btn-group" role="group" style="width:0.1%">
                        <br />
                        OR
                    </div>
                    <div class="btn-group" role="group" style="width:1%">
                        <div class="container-fluid">
                        Search by admission or doctor <br />
                        <asp:TextBox runat="server" ID="txt_admissionNo_doctorName" style="border-radius: 2px;border: solid 1px #cdced9;height: 24px;max-width:100%;width:100%;resize:none;" placeholder="Admission No - Doctor Name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="btn-group" role="group" style="width:2%">
                    <div class="container-fluid">
                        <br />
                        <asp:Button ID="btnSearch" runat="server" Width="70px" Text="Search" CssClass="btn btn-success" OnClick="btnSearch_Click"/>
                    </div>
                    </div>
                </div>
                <div style="width:100%;">
                    <div runat="server" id="prescription" role="group" style="overflow:auto"></div>
                </div>
            </div>
            
            <asp:GridView ID="gvw_history" runat="server" Visible="false"></asp:GridView>
                </asp:Panel>
            <a class="item" href="#main_page">
            <div style="position:fixed; right:10px;top:90%; transform:translate(0,-50%); text-align:left; z-index:1" >
                <div>
                    <span><img src="<%= Page.ResolveClientUrl("~/Images/Result/ic_Arrow_Top.png") %>" /></span>
                </div>
            </div>
            </a>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <div class="modal fade" id="modalCompound" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel ID="UPCompound" UpdateMode="Conditional" runat="server"><ContentTemplate>
            <div class="modal-dialog" style="width: 70%;">
                <div class="modal-content" style="border-radius:7px; height:100%;">
                     <div class="modal-header" style="height:40px;padding-top:10px;padding-bottom:5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align:left">
                            <asp:Button runat="server" ID="btnSample" Text="" CssClass="hidden" OnClick="compoundDetail_Click"/>
                            <asp:Label ID="headerCompound" style="font-family:Helvetica, Arial, sans-serif;font-weight:bold" runat="server"></asp:Label></h4>
                    </div>
                    <div class="btn-group btn-group-justified" style="width:100%; background-color:lightgrey" role="group" aria-label="...">
                            <div class="btn-group container-fluid" role="group" style="width:3%">
                                <div><label>Order Set Name</label></div>
                                <div style="font-weight:bold"><asp:Label runat="server" ReadOnly="true" ID="orderSetName" Width="100%"></asp:Label></div>
                            </div>

                            <div class="btn-group" role="group" style="width:1%">
                                <div><label>Qty</label></div>
                                <div style="font-weight:bold"><asp:Label runat="server" ReadOnly="true" ID="qtyOrderSetName"></asp:Label></div>
                            </div>

                            <div class="btn-group" role="group">
                                <div><label>U.O.M.</label></div>
                                <div style="font-weight:bold"><asp:Label runat="server" ReadOnly="true" ID="uomOrderSetName"></asp:Label></div>
                            </div>

                            <div class="btn-group" role="group">
                                <div><label>Frequency</label></div>
                                <div style="font-weight:bold"><asp:Label runat="server" ReadOnly="true" ID="frequencyOrderSetName"></asp:Label></div>
                            </div>

                            <div class="btn-group" role="group">
                                <div><label>Dose</label></div>
                                <div style="font-weight:bold"><asp:Label runat="server" ReadOnly="true" ID="doseOrderSetName"></asp:Label></div>
                            </div>

                            <div class="btn-group" role="group">
                                <div><label>Dose Text</label></div>
                                <div style="font-weight:bold"><asp:Label runat="server" ReadOnly="true" ID="dose_textOrderSetName"></asp:Label></div>
                            </div>

                            <div class="btn-group" role="group">
                                <div><label>Instruction</label></div>
                                <div style="font-weight:bold"><asp:Label runat="server" ReadOnly="true" ID="instructionOrderSetName"></asp:Label></div>
                            </div>

                            <div class="btn-group" role="group">
                                <div><label>Route</label></div>
                                <div style="font-weight:bold"><asp:Label runat="server" ReadOnly="true" ID="routeOrderSetName"></asp:Label></div>
                            </div>

                            <div class="btn-group" role="group">
                                <div><label>Iter</label></div>
                                <div style="font-weight:bold"><asp:Label runat="server" ReadOnly="true" ID="iterOrderSetName"></asp:Label></div>
                            </div>
                        </div>
                    <div class="modal-body">
                        <div style="width:100%; background-color:white">
                            <asp:GridView ID="gvw_detail_compound" runat="server" CssClass="table table-striped table-condensed" BorderColor="Transparent" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="itemName" Wrap="true" style="resize:none" BackColor="Transparent" Width="400px" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("itemName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="Oty" Wrap="true" style="resize:none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("quantity") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="U.O.M." HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="uom" Wrap="true" style="resize:none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("uom") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dose Text" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="dose_text" Wrap="true" style="resize:none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("doseText") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Instruction" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="instruction" Wrap="true" style="resize:none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("instruction") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            </ContentTemplate></asp:UpdatePanel>
        </div>
</asp:Content>