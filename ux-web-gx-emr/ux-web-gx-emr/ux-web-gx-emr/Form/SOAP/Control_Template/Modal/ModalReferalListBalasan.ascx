<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalReferalListBalasan.ascx.cs" Inherits="Form_SOAP_Control_Template_Modal_ModalReferalListBalasan" %>

<asp:UpdatePanel ID="UP_ReferralList" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField ID="hf_tab_postion" runat="server" />
        <div style="width: 100%; height: 45px; background-color: #e7e8ed; z-index: 4; top: 125px;">

            <a href="javascript:tab_referralbalasan(1);" id="LB_referralreferral">
                <div id="btn_referral_referral" runat="server" class="col-sm-1" style="top: 15%; width: 130px; text-align: center; margin-right: 0px; margin-left: 5px; padding: 6px 3px 3px 3px; border-radius: 5px 0px 0px 5px; border: 1px solid #bdbfd8;">
                    <b> Surat Rujukan Anda </b>
                </div>
            </a>

            <a id="LB_referralbalasan" runat="server" href="javascript:tab_referralbalasan(2);">
                <div id="btn_referral_balasan" runat="server" class="col-sm-1" style="top: 15%; width: 150px; text-align: center; margin-right: 5px; margin-left: -1px; padding: 6px 3px 3px 3px; border-radius: 0px 5px 5px 0px; background-color: #d6dbff; border: 1px solid #bdbfd8;">
                    <b> Jawaban Dokter </b>
                    <asp:Label ID="LabelNotif" runat="server" Text="0" CssClass="badge bg-red" style="position: absolute; font-size:9px; margin-left:6px;"></asp:Label>
                </div>
                
            </a>

        </div>

        <div runat="server" id="referral_div" style="display: none;">
            <asp:Repeater runat="server" ID="rptReferralList">
                <ItemTemplate>
                    <div>
                        <row>
                           <table>
                                <tr>
                                    <td><asp:Label ID="Label2" runat="server" Text="Kepada TS"></asp:Label></td>
                                    <td style="padding-left:15px; padding-right:5px;"> : </td>
                                    <td><asp:Label ID="Label5"  Style="font-weight: bold;" runat="server" Text='<%# Bind("DoctorReferral") %>'></asp:Label></td>
                                    <td style="padding-left:15px;"><div style="background-color: #228b22; color:white; border-radius: 10px 10px 10px 10px;"><asp:Label ID="Label9" runat="server" Text="" style="margin-left:5px; margin-right:5px;"> <i class="fa fa-check-circle"></i>
                                        <label id="lblbhs_noreminder"> <%#Eval("referral_type").ToString()%> </label>
                                    </asp:Label></div></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label3" runat="server" Text="Dari TS"></asp:Label></td>
                                    <td style="padding-left:15px; padding-right:5px;"> : </td>
                                    <td><asp:Label ID="Label6"  runat="server" Text='<%# Bind("DoctorName") %>'></asp:Label></td>
                                    <td style="padding-left:15px;"></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label4" runat="server" Text="Registrasi"></asp:Label></td>
                                    <td style="padding-left:15px; padding-right:5px;"> : </td>
                                    <td><asp:Label ID="Label7" runat="server" Text='<%# Bind("CreatedDate") %>'></asp:Label> / <asp:Label ID="Label8" runat="server" Text='<%# Bind("AdmissionNo") %>'></asp:Label></td>
                                    <td style="padding-left:15px;"></td>
                                </tr>
                            </table>
                            </br>
                        </row>
                        <row>
                            <asp:TextBox runat="server" CssClass="text-multiline-dialog" style="border: solid; border-width: thin; background-color: #E2E2E2; max-width: 100%; width: 100%; height: 100px; resize: none;" BorderColor="transparent" ID="txtRemark" TextMode="MultiLine" Text='<%# Bind("referral_remark") %>' />
                            </br>
                        </row>
                        <row>
                            <a href="javascript:void(0);" style="cursor: pointer;">
                                <asp:Label ID="Label12" runat="server" onclick ="openTabSoap(this.id)"  Text="" Style="cursor: pointer; font-family: Helvetica; font-weight: bold; font-size:12px;"> <i class="fa fa-caret-down"></i>
                                    <label id="Label11" style="cursor: pointer;">SOAP</label>
                                </asp:Label>
                            </a>
                            <div runat="server" id="soapTable" style="display:block;" >
                                <table class="table-condensed table-fill-width" cellspacing="0" rules="all" style="width: 100%; border-top: 1px solid #b9b9b9; border-color:#B9B9B9;border-width:0px;border-collapse:collapse;">
                                    <tr>
                                        <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Doctor</th>
                                        <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Subjective</th>
                                        <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Objective</th>
                                        <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Assestment</th>
                                        <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Planning</th>
                                        <th class="font-content-dashboard table-sub-header-label" style="width:25%;">Prescription</th>
                                    </tr>
                                    <tr>
                                        <td><asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="Label13" runat="server" Text='<%# Bind("DoctorName") %>'></asp:Label></td>
                                        <td><asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label14" runat="server" Text=""> <%# Eval("Subjective").ToString().Replace("\\n","<br />") %> </asp:Label></td>
                                        <td><asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label15" runat="server" Text=""> <%# Eval("Objective").ToString().Replace("\\n","<br />") %> </asp:Label></td>
                                        <td><asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label16" runat="server" Text=""> <%# Eval("Diagnosis").ToString().Replace("\\n","<br />") %> </asp:Label></td>
                                        <td>
                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label18" runat="server" Text=""> <%# Eval("PlanningProcedure").ToString().Replace("\\n","<br />") %> </asp:Label>
                                            <br/>
                                            <br/>
                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" Font-Bold="true" ID="Label21" runat="server" Visible='<%# Eval("OrderLab").ToString() == "" ? false : true %>' Text="Order Lab"></asp:Label>
                                            <br/>
                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label22" runat="server" Text=""> <%# Eval("OrderLab").ToString().Replace("\\n","<br />") %> </asp:Label>
                                            <br/>
                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" Font-Bold="true" ID="Label23" runat="server" Visible='<%# Eval("OrderRad").ToString() == "" ? false : true %>' Text="Order Rad"></asp:Label>
                                            <br/>
                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label24" runat="server" Text=""> <%# Eval("OrderRad").ToString().Replace("\\n","<br />") %> </asp:Label>
                                        </td>
                                        <td><asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label17" runat="server" Text=""> <%# Eval("Prescription").ToString().Replace("\\n","<br />") %> </asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                            </br>
                        </row>
                        <row>
                            <asp:Label ID="Label19" Style="font-family: Helvetica;font-size:11px; float:right;" runat="server" Text="Terima kasih atas bantuan dan kerjasamanya,"></asp:Label>
                            <br/>
                            <br/>
                         </row>
                        <row>
                            <asp:Label ID="Label20" Style="font-family: Helvetica;font-size:11px; float:right;" runat="server" Text='<%# Bind("DoctorName") %>'></asp:Label>
                            <br/>
                            <br/>
                        </row> 
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div runat="server" id="balasan_div">
            <asp:Repeater runat="server" ID="rptBalasanList">
                <ItemTemplate>
                    <div>
                        <row>
                            <table>
                                <tr>
                                    <td><asp:Label ID="Label3" runat="server" Text="Kepada TS"></asp:Label></td>
                                    <td style="padding-left:15px; padding-right:5px;"> : </td>
                                    <td><asp:Label ID="Label6"  runat="server" Text='<%# Bind("DoctorNameOri") %>'></asp:Label></td>
                                    <td style="padding-left:15px;"></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label2" runat="server" Text="Dari"></asp:Label></td>
                                    <td style="padding-left:15px; padding-right:5px;"> : </td>
                                    <td><asp:Label ID="Label5"  Style="font-weight: bold;" runat="server" Text='<%# Bind("DoctorReferral") %>'></asp:Label></td>
                                </tr>
                                
                            </table>
                            </br>
                        </row>
                        <row>
                            <a href="javascript:void(0);" style="cursor: pointer;">
                                <asp:Label ID="Label12" runat="server" onclick ="openTabSoapBalasan(this.id)"  Text="" Style="cursor: pointer; font-family: Helvetica; font-weight: bold; font-size:12px;"> <i class="fa fa-caret-down"></i>
                                    <label id="Label11" style="cursor: pointer;">SOAP</label>
                                </asp:Label>
                            </a>
                            <div runat="server" id="soapTable" style="display:block;" >
                                <table class="table-condensed table-fill-width" cellspacing="0" rules="all" style="width: 100%; border-top: 1px solid #b9b9b9; border-color:#B9B9B9;border-width:0px;border-collapse:collapse;">
                                    <tr>
                                        <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Doctor</th>
                                        <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Subjective</th>
                                        <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Objective</th>
                                        <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Assestment</th>
                                        <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Planning</th>
                                        <th class="font-content-dashboard table-sub-header-label" style="width:25%;">Prescription</th>
                                    </tr>
                                    <tr>
                                        <td><asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="Label13" runat="server" Text='<%# Bind("DoctorName") %>'></asp:Label></td>
                                        <td><asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label14" runat="server" Text=""> <%# Eval("Subjective").ToString().Replace("\\n","<br />") %> </asp:Label></td>
                                        <td><asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label15" runat="server" Text=""> <%# Eval("Objective").ToString().Replace("\\n","<br />") %> </asp:Label></td>
                                        <td><asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label16" runat="server" Text=""> <%# Eval("Diagnosis").ToString().Replace("\\n","<br />") %> </asp:Label></td>
                                        <td>
                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label18" runat="server" Text=""> <%# Eval("PlanningProcedure").ToString().Replace("\\n","<br />") %> </asp:Label>
                                            <br/>
                                            <br/>
                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" Font-Bold="true" ID="Label21" runat="server" Visible='<%# Eval("OrderLab").ToString() == "" ? false : true %>' Text="Order Lab"></asp:Label>
                                            <br/>
                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label22" runat="server" Text=""> <%# Eval("OrderLab").ToString().Replace("\\n","<br />") %> </asp:Label>
                                            <br/>
                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" Font-Bold="true" ID="Label23" runat="server" Visible='<%# Eval("OrderRad").ToString() == "" ? false : true %>' Text="Order Rad"></asp:Label>
                                            <br/>
                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label24" runat="server" Text=""> <%# Eval("OrderRad").ToString().Replace("\\n","<br />") %> </asp:Label>
                                        </td>
                                        <td><asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label17" runat="server" Text=""> <%# Eval("Prescription").ToString().Replace("\\n","<br />") %> </asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                            </br>
                        </row>
                        <row>
                            <asp:Label ID="Label19" Style="font-family: Helvetica;font-size:11px; float:right;" runat="server" Text="Terima kasih atas kerjasamanya,"></asp:Label>
                            <br/>
                            <br/>
                         </row>
                        <row>
                            <asp:Label ID="Label1" Style="font-family: Helvetica;font-size:11px; float:right;" runat="server" Text="Dokter penerima konsultasi"></asp:Label>
                            <br/>
                         </row>
                        <row>
                            <asp:Label ID="Label20" Style="font-family: Helvetica;font-size:11px; float:right;" runat="server" Text='<%# Bind("DoctorName") %>'></asp:Label>
                            <br/>
                            <br/>
                        </row> 
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>



<script type="text/javascript">
    function openTabSoap(ids) {
        var idparam = ids.replace("MainContent_ModalReferalList_rptReferralList_Label12_", "MainContent_ModalReferalList_rptReferralList_soapTable_");
        
        var opd_data = document.getElementById(idparam);

        if (opd_data.style.display == "block") {
            opd_data.style.display = "none";
        }
        else {
            opd_data.style.display = "block";
        }
    }

    function openTabSoapBalasan(ids) {
        var idparam = ids.replace("MainContent_ModalReferalList_rptBalasanList_Label12_", "MainContent_ModalReferalList_rptBalasanList_soapTable_");

        var opd_data = document.getElementById(idparam);

        if (opd_data.style.display == "block") {
            opd_data.style.display = "none";
        }
        else {
            opd_data.style.display = "block";
        }
    }

    function tab_referralbalasan(content) {
        var hf_tab_postion = document.getElementById('<%= hf_tab_postion.ClientID%>');

        /* Button */
        var btn_referral_referral = document.getElementById('<%= btn_referral_referral.ClientID %>');
        var btn_referral_balasan = document.getElementById('<%= btn_referral_balasan.ClientID%>');

        /* Div */
        var referral_div = document.getElementById('<%= referral_div.ClientID%>');
        var balasan_div = document.getElementById('<%= balasan_div.ClientID%>');

        if (content == 1) {
            hf_tab_postion.value = "1";

            btn_referral_referral.style.backgroundColor = "#d6dbff";
            btn_referral_balasan.style.backgroundColor = "transparent";

            referral_div.style.display = "block";
            balasan_div.style.display = "none";

        } else if (content == 2) {
            console.log("Masuk apa enggak");
            hf_tab_postion.value = "2";

            btn_referral_referral.style.backgroundColor = "transparent";
            btn_referral_balasan.style.backgroundColor = "#d6dbff";

            referral_div.style.display = "none";
            balasan_div.style.display = "block";
        }
    }
</script>
