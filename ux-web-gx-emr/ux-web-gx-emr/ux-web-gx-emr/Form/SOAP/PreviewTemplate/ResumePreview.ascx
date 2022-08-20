<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ResumePreview.ascx.cs" Inherits="Form_SOAP_PreviewTemplate_ResumePreview" %>


<style type="text/css">
    @media print {
        thead { display: table-header-group; }
        tfoot { display: table-footer-group; }
    }


 </style>
<asp:updatepanel runat="server">
    <ContentTemplate>
        <div>
            <asp:HiddenField runat="server" ID="hfpreviewpres" />
            <br />
            <div>
                <table style="width:99%;">
                    <thead>
                        <tr>
                            <td colspan="2">            
                                <div class="btn-group btn-group-justified" style="padding-bottom:10px" role="group" aria-label="...">
                                  <div class="btn-group" role="group" style="vertical-align:top">
                                    <asp:ImageButton  ImageUrl="~/Images/Icon/logo-SH.svg" runat="server" Enabled="false" />
                                  </div>
                                  
                                  <div class="btn-group" role="group" style="text-align:left;padding-right:10px;padding-left:30px">
                                    <div><b>Resume Medis <asp:Label runat="server" ID="AdmissionType"></asp:Label></b></div>
                                      <table style="width:100%">
                                          <tr>
                                              <td style="padding-right:20px;vertical-align:top">
                                                  <label>MR</label>
                                              </td>
                                              <td>
                                                  <label>: </label><asp:Label runat="server" ID="lblmrno"></asp:Label>
                                              </td>
                                           </tr>
                                          <tr>
                                              <td style="padding-right:20px;vertical-align:top">
                                                  <label>Name</label>
                                              </td>
                                              <td>
                                                  <label>: <asp:Label runat="server" ID="lblnamepatient"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td style="padding-right:20px;vertical-align:top">
                                                  <label>DOB/Age</label>
                                              </td>
                                              <td>
                                                  <label>: <asp:Label runat="server" ID="lbldobpatient"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td style="padding-right:20px;vertical-align:top">
                                                  <label>Sex</label>
                                              </td>
                                              <td>
                                                  <label>: <asp:Label runat="server" ID="lblsexpatient"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td style="padding-right:20px;vertical-align:top">
                                                  <label>Doctor</label>
                                              </td>
                                              <td>
                                                  <label>: <asp:Label runat="server" ID="lbldoctorprimary"></asp:Label>
                                              </td>
                                          </tr>
                                      </table>
                                  </div>
                                </div>
                            </td>
                        </tr>
                    </thead>
                    <tr style="background-color:black;border:solid 1px #cdced9">
                        <td style="width:30%;border-right:solid 1px #cdced9"><label style="color:white;"><label style="padding-left:10px">Keterangan</label></td>
                        <td style="width:70%"><label style="color:white"><label style="padding-left:10px">Deskripsi</label></td>
                    </tr>
                    <tr style="border:solid 1px #cdced9">
                        <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9"><label style="padding-left:10px;font-weight:bold">Chief Complaint</label><br /><label style="padding-left:10px;font-weight:bold;font-style:italic">(Keluhan Utama)</label></td>
                        <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                            <asp:Label runat="server" ID="lblChiefComplaint">
                            </asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr style="border:solid 1px #cdced9">
                        <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9"><label style="padding-left:10px;font-weight:bold">Anamnesis</label><br /><label style="padding-left:10px;font-weight:bold;font-style:italic">(Anamnesa)</label></td>
                        <td><div style="padding-left:10px;padding-right:10px;">
                            <table>
                                <tr>
                                    <td style="max-width:300px;padding-top:10px;padding-bottom:10px;border-right:solid 1px #cdced9;padding-right:10px">
                                        <asp:Label runat="server" ID="Anamnesis"></asp:Label>
                                    </td>
                                    <td style="padding-left:10px">
                                        <b>Pregnant</b><asp:Label runat="server" ID="Label4" Style="padding-left:60px">: -</asp:Label><br />
                                    <b>Breast Feeding</b><asp:Label runat="server" ID="Label5" Style="padding-left:20px">: -</asp:Label>
                                    </td>
                                </tr>
                            </table>
                            </div>
                        </td>
                    </tr>
                    <tr style="border:solid 1px #cdced9">
                        <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9"><label style="padding-left:10px;font-weight:bold">Medication & Allergies</label><br /><label style="padding-left:10px;font-weight:bold;font-style:italic">(Pengobatan & Alergi)</label></td>
                        <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                            <asp:Label runat="server" ID="Label2">
                            </asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr style="border:solid 1px #cdced9">
                        <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9"><label style="padding-left:10px;font-weight:bold">Illness History</label><br /><label style="padding-left:10px;font-weight:bold;font-style:italic">(Riwayat Penyakit)</label></td>
                        <td>
                            <div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                                <asp:Repeater runat="server" ID="IllnessHistory" >
                                    <ItemTemplate>
                                            <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("remarks") %>' Enabled="false" />
                                        <br/>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </td>
                    </tr>
                    <tr style="border:solid 1px #cdced9">
                        <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9"><label style="padding-left:10px;font-weight:bold">General Examination</label><br /><label style="padding-left:10px;font-weight:bold;font-style:italic">(Pemeriksaan Umum)</label></td>
                        <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                            <asp:Label runat="server" ID="Label3">
                            </asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr style="border:solid 1px #cdced9">
                        <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9"><label style="padding-left:10px;font-weight:bold">Physical Examination</label><br /><label style="padding-left:10px;font-weight:bold;font-style:italic">(Pemeriksaan Fisik)</label></td>
                        <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                                <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                  <div class="btn-group" role="group" style="vertical-align:top">
                                   <strong> Eye : </strong>  <asp:Label runat="server" ID="eye" Text="-"/>
                                  </div>
                                  <div class="btn-group" role="group" style="vertical-align:top">
                                     <strong>Move : </strong><asp:Label runat="server" ID="move" Text="-"/>
                                  </div>
                                  <div class="btn-group" role="group" style="vertical-align:top">
                                    <strong> Verbal : </strong><asp:Label runat="server" ID="verbal" Text="-"/>
                                  </div>
                                    <div class="btn-group" role="group" style="vertical-align:top">
                                    <strong> Score : </strong><asp:Label runat="server" ID="painscore" Text="-"/>
                                  </div>

                                </div>
                                <br />
                                <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                  <div class="btn-group" role="group" style="vertical-align:top">
                                   <strong> Blood Preassure </strong>
                                  </div>
                                  <div class="btn-group" role="group" style="vertical-align:top">
                                     <strong> Pulse rate</strong>
                                  </div>
                                    <div class="btn-group" role="group" style="vertical-align:top">
                                     <strong> Respiratory rate</strong>
                                  </div>
                                   <div class="btn-group" role="group" style="vertical-align:top">
                                       <strong> SpO2</strong>
                                  </div>
                                </div>
                                <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                   <div class="btn-group" role="group" style="vertical-align:top">
                                       <asp:Label runat="server" ID="bloodpreassure" Font-Bold="true" Text="-"/> mmHg
                                  </div>
                                  <div class="btn-group" role="group" style="vertical-align:top">
                                    <asp:Label runat="server" ID="pulse" Font-Bold="true" Text="-"/> x/mnt
                                  </div>
                                    <div class="btn-group" role="group" style="vertical-align:top">
                                    <asp:Label runat="server" ID="respiratory" Font-Bold="true" Text="-"/> x/mnt
                                  </div>
                                    <div class="btn-group" role="group" style="vertical-align:top">
                                    <asp:Label runat="server" ID="spo" Font-Bold="true" Text="-"/> %
                                  </div>
                                  
                                </div>
                            <br />
                             <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                <div class="btn-group" role="group" style="vertical-align:top">
                                        <strong> Temperature</strong>
                                  </div>
                                    <div class="btn-group" role="group" style="vertical-align:top">
                                        <strong> Weight</strong>
                                  </div>
                                    <div class="btn-group" role="group" style="vertical-align:top">
                                        <strong> Height</strong>
                                  </div>
                                 <div class="btn-group" role="group" style="vertical-align:top">
                                   </div>
                             </div>
                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                <div class="btn-group" role="group" style="vertical-align:top">
                                     <asp:Label runat="server" ID="temperature" Font-Bold="true" Text="-"/> &deg;C
                                  </div>
                                    <div class="btn-group" role="group" style="vertical-align:top">
                                       <asp:Label runat="server" ID="weight" Font-Bold="true" Text="-"/> kg
                                  </div>
                                    <div class="btn-group" role="group" style="vertical-align:top">
                                       <asp:Label runat="server" ID="height" Font-Bold="true" Text="-"/> cm
                                  </div>
                                 <div class="btn-group" role="group" style="vertical-align:top">
                                   </div>
                            </div>
                            </div>
                        </td>
                    </tr>
                    <tr style="border:solid 1px #cdced9">
                        <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9"><label style="padding-left:10px;font-weight:bold">Diagnosis</label><br /><label style="padding-left:10px;font-weight:bold;font-style:italic">(Diagnosa)</label></td>
                        <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                            <div style="padding-bottom:5px">
                                <asp:Label runat="server" ID="primarydiagnosis">
                                    -
                                </asp:Label>
                            </div>
                            </div>
                        </td>
                    </tr>
                    <tr style="border:solid 1px #cdced9">
                        <td style="background-color:#efefef;vertical-align:top;padding-top:10px;padding-bottom:10px;border-right:solid 1px #cdced9"><label style="padding-left:10px;font-weight:bold">Planning & Procedure</label><br /><label style="padding-left:10px;font-weight:bold;font-style:italic">(Tindakan di RS)</label></td>
                        <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                            <div style="padding-bottom:5px">
                            <asp:Label runat="server" ID="procedure">
                               -
                            </asp:Label>
                            </div>
                            </div>
                        </td>
                    </tr>
                    <tr style="border:solid 1px #cdced9">
                        <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9"><label style="padding-left:10px;font-weight:bold">Prescription</label><br /><label style="padding-left:10px;font-weight:bold;font-style:italic">(Resep)</label></td>
                        <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                            <div style="padding-bottom:5px">
                                    <b>Drugs</b> : 
                                    <asp:Label runat="server" ID="drugs" Text="No available document" />
                                    <asp:Repeater runat="server" ID="prescriptiondrugs" >
                                    <ItemTemplate>
                                       <li>
                                            <asp:Label ID="NameLabel" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                           <br />
                                           <asp:Label ID="Label1" Width="90%" runat="server" Enabled="false">Qty: <%#Eval("Remarks") %></asp:Label>
                                       </li>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            </div>
                            </td>
                            </tr>
                    <tr style="border:solid 1px #cdced9">
                        <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9"><label style="padding-left:10px;font-weight:bold"></label></td>
                            <td>
                                <div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                                <div>
                                    <b>Compound</b> : 
                                    <asp:Label runat="server" ID="compound" Text="No available document" />

<%--                                <asp:Repeater runat="server" ID="rptCompound" OnItemDataBound="Repeater1_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:Label ID="NameLabel" runat="server" Font-Size="12px" Font-Bold="true" Font-Names="Helvetica, Arial, sans-serif" Enabled="false"><%#Eval("item_name") %>&nbsp&nbsp<%#Eval("quantity") %>&nbsp<%#Eval("uom_code") %>,&nbsp<%#Eval("frequency_code") %>,&nbsp<%#Eval("remarks") %>,&nbsp<%#Eval("administration_route_code") %>,&nbsp<%#Eval("is_routine") %></asp:Label>
                                        <br />
                                        <asp:Repeater ID="rptCompDetail" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Label ID="NameLabel" runat="server" Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false"><%#Eval("item_name") %>&nbsp&nbsp<%#Eval("quantity") %>&nbsp<%#Eval("uom_code") %>,&nbsp<%#Eval("frequency_code") %>,&nbsp<%#Eval("remarks") %>,&nbsp<%#Eval("administration_route_code") %>,&nbsp<%#Eval("is_routine") %></asp:Label>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>--%>

                                    <asp:Repeater runat="server" ID="prescriptioncompound" OnItemDataBound="Repeater1_ItemDataBound">
                                    <ItemTemplate>
                                            <asp:Label ID="NameLabel" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                           <br />
                                           <asp:Label ID="Label1" Width="90%" runat="server" Enabled="false">Qty: <%#Eval("Remarks") %></asp:Label>
                                        <br />
                                        <asp:Repeater ID="rptCompDetail" runat="server">
                                            <ItemTemplate>  
                                                <li style="padding-left:15px">
                                                    <asp:Label ID="NameLabel" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                                    <br />
                                                    <asp:Label ID="Label1" Width="90%" runat="server" Enabled="false">Qty: <%#Eval("Remarks") %></asp:Label>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                                    </div>
                                </td>
                        </tr>
                    <tr style="border:solid 1px #cdced9">
                        <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9"><label style="padding-left:10px;font-weight:bold"></label></td>
                            <td>
                                <div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                                <div>
                                    <b>Consumables</b> : 
                                    <asp:Label runat="server" ID="cons" Text="No available document" />
                                    <asp:Repeater runat="server" ID="prescriptionconsumables" >
                                    <ItemTemplate>
                                       <li>
                                            <asp:Label ID="NameLabel" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                           <br />
                                           <asp:Label ID="Label1" Width="90%" runat="server" Enabled="false">Qty: <%#Eval("Remarks") %></asp:Label>
                                       </li>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                                    </div>
                            </div>
<%--                                <asp:Repeater runat="server" ID="prescription" >
                                    <ItemTemplate>
                                        <li>
                                            <asp:Label ID="NameLabel" Width="40%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                            <asp:Label ID="Label1" Width="55%" runat="server" Text='<%#Eval("Remarks") %>' Enabled="false" />
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>--%>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </ContentTemplate>
</asp:updatepanel>