<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SoapPagePreview.ascx.cs" Inherits="Form_SOAP_PreviewTemplate_SoapPagePreview" %>

<style type="text/css">
    @media print {
        thead {
            display: table-header-group;
        }

        tfoot {
            display: table-footer-group;
        }
    }

    .bordertable {
        border: 1px solid #cdced9;
    }
</style>

<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div>
            <asp:HiddenField runat="server" ID="hfpreviewpres" />
            <br />
            <div>
                <table style="width: 99%;">
                    <%-- =============================================== HEADER ==================================================== --%>
                    <thead>
                        <tr>
                            <td colspan="3">
                                <div class="btn-group btn-group-justified" style="padding-bottom: 13px" role="group" aria-label="...">
                                    <div class="btn-group" role="group" style="vertical-align: top">
                                        <asp:ImageButton ImageUrl="~/Images/Icon/logo-SH.svg" Style="width: 280px;" runat="server" Enabled="false" />
                                    </div>
                                    <div class="btn-group" role="group" style="text-align: left; padding-right: 13px; padding-left: 30px">
                                        <div>
                                            <b>Resume Medis
                                            <asp:Label runat="server" ID="AdmissionType"></asp:Label></b>
                                        </div>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="padding-right: 20px; vertical-align: top">
                                                    <label style="font-size: 13px">MR</label>
                                                </td>
                                                <td>
                                                    <label>: </label>
                                                    <asp:Label runat="server" ID="lblmrno" Style="font-size: 13px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 20px; vertical-align: top">
                                                    <label style="font-size: 13px">Name</label>
                                                </td>
                                                <td>
                                                    <label>:<asp:Label runat="server" ID="lblnamepatient" Style="font-size: 13px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 20px; vertical-align: top">
                                                    <label style="font-size: 13px">DOB/Age</label>
                                                </td>
                                                <td>
                                                    <label>:<asp:Label runat="server" ID="lbldobpatient" Style="font-size: 13px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 20px; vertical-align: top">
                                                    <label style="font-size: 13px">Sex</label>
                                                </td>
                                                <td>
                                                    <label>:<asp:Label runat="server" ID="lblsexpatient" Style="font-size: 13px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 20px; vertical-align: top">
                                                    <label style="font-size: 13px">Doctor</label>
                                                </td>
                                                <td>
                                                    <label> :<asp:Label runat="server" ID="lbldoctorprimary" Style="font-size: 13px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </thead>
                    <%-- =============================================== END HEADER ==================================================== --%>

                    <tr style="background-color: black; border: solid 1px #cdced9">
                        <td style="width: 20%; border-right: solid 1px #cdced9">
                            <label style="color: white;">
                                <label style="padding-left: 13px;">Keterangan</label></td>
                        <td colspan="2" style="width: 80%">
                            <label style="color: white">
                                <label style="padding-left: 13px">Deskripsi</label></td>
                    </tr>

                    <%-- =============================================== CHEIF COMPLAINT ==================================================== --%>
                    <tr style="border: solid 1px #cdced9">
                        <td style="width: 20%; background-color: #efefef;" class="itemtable-priview-title">
                            <label style="padding-left: 13px; font-weight: bold; font-size: 13px">Chief Complaint</label><br />
                            <label style="padding-left: 13px; font-weight: bold; font-style: italic; font-size: 13px">(Keluhan Utama)</label></td>
                        <td style="width: 40%" class="itemtable-priview">
                            <asp:Label runat="server" ID="lblChiefComplaint" Style="font-size: 13px; vertical-align: top">
                            </asp:Label>
                        </td>
                        <td style="width: 40%;" class="itemtable-priview">
                            <label style="font-size: 13px; font-weight: bold">Pregnant </label>
                            <label style="font-size: 13px; font-weight: bold; font-style: italic">(Hamil)</label>
                            <asp:Label runat="server" ID="lblispregnant" Style="padding-left: 72px">: -</asp:Label><br />
                            <label style="font-size: 13px; font-weight: bold">Breast Feeding </label>
                            <label style="font-size: 13px; font-weight: bold; font-style: italic">(Menyusui)</label>
                            <asp:Label runat="server" ID="lblbreastfeed" Style="padding-left: 10px">: -</asp:Label>
                        </td>
                    </tr>
                    <%-- =============================================== END CHEIF COMPLAINT ==================================================== --%>
                    <%-- =============================================== ANAMNESIS ==================================================== --%>
                    <tr style="border: solid 1px #cdced9">
                        <td style="background-color: #efefef;" class="itemtable-priview-title">
                            <label style="padding-left: 13px; font-weight: bold; font-size: 13px">Anamnesis</label><br />
                            <label style="padding-left: 13px; font-weight: bold; font-style: italic; font-size: 13px">(Anamnesa)</label></td>
                        <td colspan="2" class="itemtable-priview">
                            <asp:Label runat="server" ID="Anamnesis" Style="font-size: 13px"></asp:Label>
                        </td>
                    </tr>
                    <%-- =============================================== END ANAMNESIS ==================================================== --%>
                    <%-- =============================================== MEDICATION & ALLERGIES ==================================================== --%>
                    <tr style="border: solid 1px #cdced9">
                        <td style="background-color: #efefef;" class="itemtable-priview-title">
                            <label style="padding-left: 13px; font-weight: bold; font-size: 13px">Medication & Allergies</label>
                            <br />
                            <label style="padding-left: 13px; font-weight: bold; font-style: italic; font-size: 13px">(Pengobatan & Alergi)</label>
                        </td>
                        <td colspan="2" style="border-right: solid 1px #cdced9;">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 25%;" class="itemtable-priview">
                                        <label style="font-size: 13px; font-weight: bold">Routine Medication</label><br />
                                        <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Pengobatan Rutin)</label><br />
                                        <asp:Label runat="server" ID="lblnoroute" Font-Size="13px">No Routine Medication</asp:Label>
                                        <asp:Repeater runat="server" ID="rptRoutine">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Label ID="NameLabel" runat="server" Style="font-size: 13px" Text='<%#Eval("value") %>' Enabled="false" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                    <td style="width: 25%;" class="itemtable-priview">
                                        <label style="font-size: 13px; font-weight: bold">Drug Allergies</label><br />
                                        <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Alergi Obat)</label><br />
                                        <asp:Label runat="server" ID="lblnodrugallergy" Font-Size="13px">No Drug Allergy</asp:Label>
                                        <asp:Repeater runat="server" ID="rptDrugAllergies">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Label ID="NameLabel" runat="server" Style="font-size: 13px" Text='<%#Eval("value") %>' Enabled="false" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                    <td style="width: 25%;" class="itemtable-priview">
                                        <label style="font-size: 13px; font-weight: bold">Food Allergies</label><br />
                                        <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Alergi Makanan)</label><br />
                                        <asp:Label runat="server" ID="lblnofoodallergy" Font-Size="13px">No Food Allergy</asp:Label>
                                        <asp:Repeater runat="server" ID="rptFoodAllergies">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Label ID="NameLabel" runat="server" Style="font-size: 13px" Text='<%#Eval("value") %>' Enabled="false" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                    <td style="width: 25%; border-right: 0px;" class="itemtable-priview">
                                        <label style="font-size: 13px; font-weight: bold">Other Allergies</label><br />
                                        <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Alergi Lainnya)</label><br />
                                        <asp:Label runat="server" ID="lblnootherallergy" Font-Size="13px">No Other Allergy</asp:Label>
                                        <asp:Repeater runat="server" ID="rptOtherAllergies">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Label ID="NameLabel" runat="server" Style="font-size: 13px" Text='<%#Eval("value") %>' Enabled="false" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%-- =============================================== END MEDICATION & ALLERGIES ==================================================== --%>
                    <%-- =============================================== ILLNESS HISTORY ==================================================== --%>
                    <tr style="border: solid 1px #cdced9">
                        <td style="background-color: #efefef;" class="itemtable-priview-title">
                            <label style="padding-left: 13px; font-weight: bold; font-size: 13px">Illness History</label><br />
                            <label style="padding-left: 13px; font-weight: bold; font-style: italic; font-size: 13px">(Riwayat Penyakit)</label></td>
                        <td colspan="2" style="border-right: solid 1px #cdced9;">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 35%;" class="itemtable-priview">
                                        <label style="font-size: 13px; font-weight: bold">Surgery History</label><br />
                                        <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Riwayat Operasi)</label><br />
                                        <asp:Label runat="server" ID="lblnosurgery" Font-Size="13px">No Surgery History</asp:Label>
                                        <asp:Repeater runat="server" ID="rptSurgeryHistory">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Label ID="NameLabel" runat="server" Style="font-size: 13px" Text='<%#Eval("value") %>' Enabled="false" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                    <td style="width: 35%;" class="itemtable-priview">
                                        <label style="font-size: 13px; font-weight: bold">Disease History</label><br />
                                        <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Riwayat Penyakit)</label><br />
                                        <asp:Label runat="server" ID="lblnodisease" Font-Size="13px">No Disease History</asp:Label>
                                        <asp:Repeater runat="server" ID="rptDiseaseHistory">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Label ID="NameLabel" runat="server" Style="font-size: 13px" Text='<%#Eval("remarks") %>' Enabled="false" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                    <td style="width: 30%; border-right: 0px;" class="itemtable-priview">
                                        <label style="font-size: 13px; font-weight: bold">Family Disease History</label><br />
                                        <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Riwayat Penyakit Keluarga)</label><br />
                                        <asp:Label runat="server" ID="lblnofamilydisease" Font-Size="13px">No Family Disease History</asp:Label>
                                        <asp:Repeater runat="server" ID="rptFamilyDisease">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Label ID="NameLabel" runat="server" Style="font-size: 13px" Text='<%#Eval("remarks") %>' Enabled="false" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
            </div>
            </td>
            </tr>
            <%-- =============================================== END ILLNESS HISTORY ==================================================== --%>
            <%-- =============================================== GENERAL EXAMINATION ==================================================== --%>
            <tr style="border: solid 1px #cdced9">
                <td style="background-color: #efefef;" class="itemtable-priview-title">
                    <label style="padding-left: 13px; font-weight: bold; font-size: 13px">General Examination</label><br />
                    <label style="padding-left: 13px; font-weight: bold; font-style: italic; font-size: 13px">(Pemeriksaan Umum)</label></td>
                <td colspan="2" style="border-right: solid 1px #cdced9;">
                    <table style="width: 100%;">
                        <tr>
                            <td class="itemtable-priview">
                                <label style="font-size: 13px; font-weight: bold">Eye </label>
                                <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Mata)</label>
                                <label style="font-size: 13px; font-weight: bold">:</label><br />
                                <asp:Label runat="server" ID="eye" Style="font-size: 13px" Text="-" />
                            </td>
                            <td class="itemtable-priview">
                                <label style="font-size: 13px; font-weight: bold">Move </label>
                                <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Motorik)</label>
                                <label style="font-size: 13px; font-weight: bold">:</label><br />
                                <asp:Label runat="server" ID="move" Style="font-size: 13px" Text="-" />
                            </td>
                            <td class="itemtable-priview">
                                <label style="font-size: 13px; font-weight: bold">Verbal </label>
                                <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Verbal)</label>
                                <label style="font-size: 13px; font-weight: bold">:</label><br />
                                <asp:Label runat="server" ID="verbal" Style="font-size: 13px" Text="-" />
                            </td>
                            <td style="border-right: 0px;" class="itemtable-priview">
                                <label style="font-size: 13px; font-weight: bold">Score </label>
                                <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Skor)</label>
                                <label style="font-size: 13px; font-weight: bold">:</label><br />
                                <asp:Label runat="server" ID="painscore" Style="font-size: 13px" Text="-" />
                            </td>
                        </tr>
                    </table>
                    <div style="border-top: solid 1px #cdced9">
                        <table>
                            <tr>
                                <td class="itemtable-priview" style="border-right: 0px;">
                                    <label style="font-size: 13px; font-weight: bold">Pain Scale </label>
                                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Skala Nyeri)</label>
                                    <label style="font-size: 13px; font-weight: bold">:</label><br />
                                    <asp:Label runat="server" Style="font-size: 13px" ID="lblpainscale" Text="-" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="border-top: solid 1px #cdced9">
                        <table style="width: 100%;">
                            <tr>
                                <td class="itemtable-priview">
                                    <label style="font-size: 13px; font-weight: bold">Blood Pressure </label>
                                    <br />
                                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Tekanan Darah)</label>
                                    <br />
                                    <asp:Label runat="server" Style="font-size: 13px" ID="bloodpreassure" Text="-" />
                                    <label style="font-size: 13px">mmHg</label>
                                </td>
                                <td class="itemtable-priview">
                                    <label style="font-size: 13px; font-weight: bold">Pulse Rate </label>
                                    <br />
                                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Nadi)</label>
                                    <br />
                                    <asp:Label runat="server" Style="font-size: 13px" ID="pulse" Text="-" /><label style="font-size: 13px">x/mnt</label>
                                </td>
                                <td class="itemtable-priview">
                                    <label style="font-size: 13px; font-weight: bold">Respiratory Rate </label>
                                    <br />
                                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Pernapasan)</label>
                                    <br />
                                    <asp:Label runat="server" Style="font-size: 13px" ID="respiratory" Text="-" /><label style="font-size: 13px">x/mnt</label>
                                </td>
                                <td class="itemtable-priview">
                                    <label style="font-size: 13px; font-weight: bold">SpO2 </label>
                                    <br />
                                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(SpO2)</label>
                                    <br />
                                    <asp:Label runat="server" Style="font-size: 13px" ID="spo" Text="-" /><label style="font-size: 13px">%</label>
                                </td>
                                <td class="itemtable-priview">
                                    <label style="font-size: 13px; font-weight: bold">Temperature </label>
                                    <br />
                                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Suhu)</label>
                                    <br />
                                    <asp:Label runat="server" Style="font-size: 13px" ID="temperature" Text="-" />
                                    &deg;<label style="font-size: 13px">C</label>
                                </td>
                                <td class="itemtable-priview">
                                    <label style="font-size: 13px; font-weight: bold">Weight </label>
                                    <br />
                                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Berat)</label>
                                    <br />
                                    <asp:Label runat="server" Style="font-size: 13px" ID="weight" Text="-" />
                                    <label style="font-size: 13px">kg</label>
                                </td>
                                <td class="itemtable-priview">
                                    <label style="font-size: 13px; font-weight: bold">Height </label>
                                    <br />
                                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Tinggi)</label>
                                    <br />
                                    <asp:Label runat="server" Style="font-size: 13px" ID="height" Text="-" />
                                    <label style="font-size: 13px">cm</label>
                                </td>
                                <td class="itemtable-priview" style="border-right: 0px;">
                                    <label style="font-size: 13px; font-weight: bold">Head Circumference </label>
                                    <br />
                                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Lingkar Kepala)</label>
                                    <br />
                                    <asp:Label runat="server" Style="font-size: 13px" ID="headcircumference" Text="-" />
                                    <label style="font-size: 13px">cm</label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="border-top: solid 1px #cdced9">
                        <table style="width: 100%;">
                            <tr>
                                <td class="itemtable-priview">
                                    <label style="font-size: 13px; font-weight: bold">Mental Status </label>
                                    <br />
                                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Status Mental)</label>
                                    <br />
                                    <asp:Label runat="server" Style="font-size: 13px" ID="lblmentalstatus" Text="-" />
                                </td>
                                <td class="itemtable-priview">
                                    <label style="font-size: 13px; font-weight: bold">Consciousness </label>
                                    <br />
                                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Kesadaran)</label>
                                    <br />
                                    <asp:Label runat="server" Style="font-size: 13px" ID="lblconsciousness" Text="-" />
                                </td>
                                <td class="itemtable-priview" style="border-right: 0px;">
                                    <label style="font-size: 13px; font-weight: bold">Fall Risk </label>
                                    <br />
                                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Risiko Jatuh)</label>
                                    <br />
                                    <asp:Repeater runat="server" ID="rptfallrisk">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="NameLabel" runat="server" Style="font-size: 13px" Text='<%#Eval("remarks") %>' Enabled="false" />
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <%-- =============================================== END GENERAL EXAMINATION ==================================================== --%>
            <%-- =============================================== PHYSICAL EXAMINATION ==================================================== --%>
            <tr style="border: solid 1px #cdced9">
                <td style="background-color: #efefef;" class="itemtable-priview-title">
                    <label style="padding-left: 13px; font-weight: bold; font-size: 13px">Physical Examination</label><br />
                    <label style="padding-left: 13px; font-weight: bold; font-style: italic; font-size: 13px">(Pemeriksaan Fisik)</label></td>
                <td colspan="2" class="itemtable-priview">
                    <asp:Label runat="server" Style="font-size: 13px" ID="lblphysicalexamination">-</asp:Label>
                </td>
            </tr>
            <%-- =============================================== END PHYSICAL EXAMINATION ==================================================== --%>
            <%-- =============================================== DIAGNOSIS ==================================================== --%>
            <tr style="border: solid 1px #cdced9">
                <td style="background-color: #efefef;" class="itemtable-priview-title">
                    <label style="padding-left: 13px; font-weight: bold; font-size: 13px">Diagnosis</label><br />
                    <label style="padding-left: 13px; font-weight: bold; font-style: italic; font-size: 13px">(Diagnosa)</label></td>
                <td colspan="2" class="itemtable-priview">
                    <asp:Label runat="server" Style="font-size: 13px" ID="primarydiagnosis"> - </asp:Label>
                </td>
            </tr>
            <%-- =============================================== END DIAGNOSIS ==================================================== --%>
            <%-- =============================================== PLANNING & PROCEDURE ==================================================== --%>
            <tr style="border: solid 1px #cdced9">
                <td style="background-color: #efefef;" class="itemtable-priview-title">
                    <label style="padding-left: 13px; font-weight: bold; font-size: 13px">Planning & Procedure</label><br />
                    <label style="padding-left: 13px; font-weight: bold; font-style: italic; font-size: 13px">(Tindakan di RS)</label></td>
                <td colspan="2" class="itemtable-priview">
                    <asp:Label runat="server" Style="font-size: 13px" ID="procedure"> - </asp:Label>
                </td>
            </tr>
            <%-- =============================================== END PLANNING & PROCEDURE ==================================================== --%>

            <%-- =============================================== PLANNING RESULT ==================================================== --%>
            <tr style="border: solid 1px #cdced9">
                <td style="background-color: #efefef;" class="itemtable-priview-title">
                    <label style="padding-left: 13px; font-weight: bold; font-size: 13px">Procedure Result</label><br />
                    <label style="padding-left: 13px; font-weight: bold; font-style: italic; font-size: 13px">(Hasil Tindakan)</label></td>
                <td colspan="2" class="itemtable-priview">
                    <asp:Label runat="server" Style="font-size: 13px" ID="procedureResult"> - </asp:Label>
                </td>
            </tr>
            <%-- =============================================== END PLANNING RESULT ==================================================== --%>
            <%-- =============================================== PRESCRIPTION ==================================================== --%>
            <tr style="border: solid 1px #cdced9">
                <td style="background-color: #efefef;" class="itemtable-priview-title">
                    <label style="padding-left: 13px; font-weight: bold; font-size: 13px">Prescription</label><br />
                    <label style="padding-left: 13px; font-weight: bold; font-style: italic; font-size: 13px">(Resep)</label>
                </td>
                <td colspan="2" class="itemtable-priview">
                    <label style="font-size: 13px; font-weight: bold;">Drugs </label>
                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Obat)</label><br />
                    <asp:Label runat="server" ID="drugs" Font-Size="13px" Text="No Drug Prescription" />
                    <div style="padding-bottom: 5px" runat="server" id="divdrugsprescription">
                        <table style="width: 100%;">
                            <tr class="bordertable" style="vertical-align: top">
                                <td style="width: 200px; padding-left: 13px">
                                    <label style="font-size: 13px; font-weight: bold">Item</label>
                                </td>
                                <td style="width: 150px">
                                    <label style="font-size: 13px; font-weight: bold">Dose</label>
                                </td>
                                <%--<td style="width: 100px">
                                    <label style="font-size: 13px; font-weight: bold">Dose UoM</label>
                                </td>--%>
                                <td style="width: 100px">
                                    <label style="font-size: 13px; font-weight: bold">Frequency</label>
                                </td>
                                <td style="width: 80px">
                                    <label style="font-size: 13px; font-weight: bold">Route</label>
                                </td>
                                <td style="width: 140px">
                                    <label style="font-size: 13px; font-weight: bold">Instruction</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Qty</label>
                                </td>
                                <td style="width: 40px">
                                    <label style="font-size: 13px; font-weight: bold">U.O.M</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Iter</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Routine</label>
                                </td>
                            </tr>
                            <asp:Repeater runat="server" ID="prescriptiondrugs">
                                <ItemTemplate>
                                    <tr class="bordertable" style="vertical-align: top">
                                        <td style="width: 200px; padding-left: 13px">
                                            <asp:Label ID="NameLabel" Style="font-size: 13px" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                        </td>
                                        <td style="width: 150px">
                                            <div runat="server" Visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                                <asp:Label ID="Label8" Style="font-size: 13px" runat="server" Enabled="false"><%#Eval("dose","{0:G29}") %></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="Label9" Style="font-size: 13px" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                            </div>
                                            <asp:Label ID="LabelDoseText" Style="font-size: 13px" Width="90%" runat="server" Enabled="false" Visible='<%# Eval("IsDoseText") %>'><%#Eval("doseText") %></asp:Label>
                                        </td>
                                        <%--<td style="width: 100px">
                                            <asp:Label ID="Label9" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                        </td>--%>
                                        <td style="width: 100px">
                                            <asp:Label ID="Label3" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("frequency") %></asp:Label>
                                        </td>
                                        <td style="width: 80px">
                                            <asp:Label ID="Label5" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("route") %></asp:Label>
                                        </td>
                                        <td style="width: 140px">
                                            <asp:Label ID="Label4" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("instruction") %></asp:Label>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="Label1" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                        </td>
                                        <td style="width: 40px">
                                            <asp:Label ID="Label2" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("uom") %></asp:Label>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="Label6" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("iteration") %></asp:Label>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="Label7" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("routine") %></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </td>
            </tr>
            <%-- =============================================== RACIKAN ==================================================== --%>
            <tr style="border: solid 1px #cdced9">
                <td style="background-color: #efefef;" class="itemtable-priview-title">
                    <label style="padding-left: 13px; font-weight: bold; font-size: 13px"></label>
                </td>
                <td colspan="2" class="itemtable-priview">
                    <label style="font-size: 13px; font-weight: bold;">Compounds </label>
                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Racikan)</label><br />
                    <asp:Label runat="server" ID="noracikan" Font-Size="13px" Text="No Compound Prescription" />
                    <div style="padding-bottom: 5px" runat="server" id="divRacikan">
                        <table style="width: 100%;">
                            <tr class="bordertable" style="vertical-align: top">
                                <td style="width: 200px; padding-left: 13px">
                                    <label style="font-size: 13px; font-weight: bold">Name</label>
                                </td>
                                <td style="width: 150px">
                                    <label style="font-size: 13px; font-weight: bold">Dose</label>
                                </td>
                                <%--<td style="width: 100px">
                                    <label style="font-size: 13px; font-weight: bold">Dose UoM</label>
                                </td>--%>
                                <td style="width: 100px">
                                    <label style="font-size: 13px; font-weight: bold">Frequency</label>
                                </td>
                                <td style="width: 80px">
                                    <label style="font-size: 13px; font-weight: bold">Route</label>
                                </td>
                                <td style="width: 140px">
                                    <label style="font-size: 13px; font-weight: bold">Instruction</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Qty</label>
                                </td>
                                <td style="width: 40px">
                                    <label style="font-size: 13px; font-weight: bold">U.O.M</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Iter</label>
                                </td>
                            </tr>
                            <asp:Repeater runat="server" ID="RepeaterRacikan">
                                <ItemTemplate>
                                    <tr class="bordertable" style="vertical-align: top">
                                        <td style="width: 200px; padding-left: 13px">
                                            <asp:Label ID="racikanname" Style="font-size: 13px" Width="90%" runat="server" Text='<%#Eval("compound_name") %>' Enabled="false" />
                                        </td>
                                        <td style="width: 150px">
                                            <div runat="server" Visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                                <asp:Label ID="racikandose" Style="font-size: 13px" runat="server" Enabled="false"><%#Eval("dose","{0:G29}") %></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="racikandoseuom" Style="font-size: 13px" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                            </div>
                                            <asp:Label ID="RacikanDoseText" Style="font-size: 13px" Width="90%" runat="server" Enabled="false" Visible='<%# Eval("IsDoseText") %>'><%#Eval("dose_Text") %></asp:Label>
                                        </td>
                                        <%--<td style="width: 100px">
                                            <asp:Label ID="racikandoseuom" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                        </td>--%>
                                        <td style="width: 100px">
                                            <asp:Label ID="racikanfrequency" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("frequency_code") %></asp:Label>
                                        </td>
                                        <td style="width: 80px">
                                            <asp:Label ID="racikanroute" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_route_code") %></asp:Label>
                                        </td>
                                        <td style="width: 140px">
                                            <asp:Label ID="racikaninstruction" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_instruction") %></asp:Label>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="racikanqty" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                        </td>
                                        <td style="width: 40px">
                                            <asp:Label ID="racikanuom" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("uom_code") %></asp:Label>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="racikaniter" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("iter") %></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </td>
            </tr>
            <%-- =============================================== PRESCRIPTION ==================================================== --%>
            <tr style="border: solid 1px #cdced9" runat="server" id="trcompound" visible="false">
                <td style="background-color: #efefef;" class="itemtable-priview-title">
                    <label style="padding-left: 13px; font-weight: bold"></label>
                </td>
                <td colspan="2" class="itemtable-priview">
                    <label style="font-size: 13px; font-weight: bold;">Compound</label><br />
                    <asp:Label runat="server" ID="compound" Font-Size="13px" Text="No Compound Prescription" />
                    <div runat="server" id="divcompoundprescription">
                        <table style="width: 100%;">
                            <tr class="bordertable" style="vertical-align: top">
                                <td style="width: 200px; padding-left: 13px">
                                    <label style="font-size: 13px; font-weight: bold">Item</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Qty</label>
                                </td>
                                <td style="width: 40px">
                                    <label style="font-size: 13px; font-weight: bold">U.O.M</label>
                                </td>
                                <td style="width: 100px">
                                    <label style="font-size: 13px; font-weight: bold">Frequency</label>
                                </td>
                                <td style="width: 140px">
                                    <label style="font-size: 13px; font-weight: bold">Dose & Instruction</label>
                                </td>
                                <td style="width: 80px">
                                    <label style="font-size: 13px; font-weight: bold">Route</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Iter</label>
                                </td>
                            </tr>
                            <asp:Repeater runat="server" ID="prescriptioncompound" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <tr class="bordertable" style="vertical-align: middle">
                                        <td style="width: 200px; padding-left: 13px">
                                            <asp:Label ID="NameLabel" Style="font-size: 13px" Width="90%" runat="server" Text='<%#Eval("compound_name") %>' Enabled="false" />
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="Label1" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                        </td>
                                        <td style="width: 40px">
                                            <asp:Label ID="Label2" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("uom") %></asp:Label>
                                        </td>
                                        <td style="width: 100px">
                                            <asp:Label ID="Label3" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("frequency") %></asp:Label>
                                        </td>
                                        <td style="width: 140px">
                                            <asp:Label ID="Label4" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("instruction") %></asp:Label>
                                        </td>
                                        <td style="width: 80px">
                                            <asp:Label ID="Label5" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("route") %></asp:Label>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="Label6" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("iteration") %></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="bordertable" style="vertical-align: top">
                                        <td style="width: 250px" colspan="7">
                                            <asp:Repeater ID="rptCompDetail" runat="server">
                                                <ItemTemplate>
                                                    <li style="padding-left: 15px">
                                                        <asp:Label ID="NameLabel" Font-Size="13px" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                                        <br />
                                                        <asp:Label ID="Label1" Font-Size="13px" Width="90%" runat="server" Enabled="false">Qty: <%#Eval("Remarks") %></asp:Label>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <%--<asp:Repeater runat="server" ID="rptCompound" OnItemDataBound="Repeater1_ItemDataBound">
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

                        <%--<asp:Repeater runat="server" ID="prescriptioncompound" OnItemDataBound="Repeater1_ItemDataBound">
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
                                </asp:Repeater>--%>
                    </div>
                </td>
            </tr>
            <tr style="border: solid 1px #cdced9">
                <td style="background-color: #efefef;" class="itemtable-priview-title">
                    <label style="padding-left: 13px; font-weight: bold"></label>
                </td>
                <td colspan="2" class="itemtable-priview">

                    <label style="font-size: 13px; font-weight: bold;">Consumables </label>
                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Alat Kesehatan)</label>
                    <br />
                    <asp:Label runat="server" ID="cons" Font-Size="13px" Text="No Consumable Prescription" />
                    <div runat="server" id="divconsumables">
                        <table style="width: 100%">
                            <tr class="bordertable" style="vertical-align: top">
                                <td style="width: 350px; padding-left: 13px">
                                    <label style="font-size: 13px; font-weight: bold">Item</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Qty</label>
                                </td>
                                <td style="width: 40px">
                                    <label style="font-size: 13px; font-weight: bold">U.O.M</label>
                                </td>
                                <td style="width: 240px">
                                    <label style="font-size: 13px; font-weight: bold">Dose & Instruction</label>
                                </td>
                            </tr>
                            <asp:Repeater runat="server" ID="prescriptionconsumables">
                                <ItemTemplate>
                                    <tr class="bordertable" style="vertical-align: top">
                                        <td style="width: 350px; padding-left: 13px">
                                            <asp:Label ID="NameLabel" Style="font-size: 13px" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="Label1" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                        </td>
                                        <td style="width: 40px">
                                            <asp:Label ID="Label2" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("uom") %></asp:Label>
                                        </td>
                                        <td style="width: 240px">
                                            <asp:Label ID="Label4" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("instruction") %></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="border: solid 1px #cdced9" runat="server" id="trAdditionalDrugs">
                <td style="background-color: #efefef;" class="itemtable-priview-title">
                    <label style="padding-left: 13px; font-weight: bold"></label>
                </td>
                <td colspan="2" class="itemtable-priview">
                    <label style="font-size: 13px; font-weight: bold;">Additional Drugs </label>
                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Tambahan Obat)</label><br />
                    <asp:Label runat="server" ID="lbladditionalpres" Font-Size="13px" Text="No Additional Drugs Prescription" />
                    <div runat="server" id="dvAdditionalPres">
                        <table style="width: 100%;">
                            <tr class="bordertable" style="vertical-align: top">
                                <td style="width: 200px; padding-left: 13px">
                                    <label style="font-size: 13px; font-weight: bold">Item</label>
                                </td>
                                <td style="width: 150px">
                                    <label style="font-size: 13px; font-weight: bold">Dose</label>
                                </td>
                                <%--<td style="width: 100px">
                                    <label style="font-size: 13px; font-weight: bold">Dose UoM</label>
                                </td>--%>
                                <td style="width: 100px">
                                    <label style="font-size: 13px; font-weight: bold">Frequency</label>
                                </td>
                                <td style="width: 80px">
                                    <label style="font-size: 13px; font-weight: bold">Route</label>
                                </td>
                                <td style="width: 140px">
                                    <label style="font-size: 13px; font-weight: bold">Instruction</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Qty</label>
                                </td>
                                <td style="width: 40px">
                                    <label style="font-size: 13px; font-weight: bold">U.O.M</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Iter</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Routine</label>
                                </td>
                            </tr>
                            <asp:Repeater runat="server" ID="rptAdditionalDrugs">
                                <ItemTemplate>
                                    <tr class="bordertable" style="vertical-align: top">
                                        <td style="width: 200px; padding-left: 13px">
                                            <asp:Label ID="NameLabel" Style="font-size: 13px" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                        </td>
                                        <td style="width: 150px">
                                            <div runat="server" Visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                                <asp:Label ID="drugdose" Style="font-size: 13px" runat="server" Enabled="false"><%#Eval("dose","{0:G29}") %></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="drugdoseuom" Style="font-size: 13px" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                            </div>
                                            <asp:Label ID="drugDoseText" Style="font-size: 13px" Width="90%" runat="server" Enabled="false" Visible='<%# Eval("IsDoseText") %>'><%#Eval("doseText") %></asp:Label>
                                        </td>
                                       <%-- <td style="width: 100px">
                                            <asp:Label ID="Label9" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                        </td>--%>
                                        <td style="width: 100px">
                                            <asp:Label ID="Label3" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("frequency") %></asp:Label>
                                        </td>
                                        <td style="width: 80px">
                                            <asp:Label ID="Label5" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("route") %></asp:Label>
                                        </td>
                                        <td style="width: 140px">
                                            <asp:Label ID="Label4" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("instruction") %></asp:Label>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="Label1" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                        </td>
                                        <td style="width: 40px">
                                            <asp:Label ID="Label2" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("uom") %></asp:Label>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="Label6" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("iteration") %></asp:Label>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="Label7" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("routine") %></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </td>
            </tr>

            <%-- =============================================== RACIKAN TAMBAHAN ==================================================== --%>
            <tr style="border: solid 1px #cdced9" runat="server" id="trAdditionalRacikan">
                <td style="background-color: #efefef;" class="itemtable-priview-title">
                    <label style="padding-left: 13px; font-weight: bold; font-size: 13px"></label>
                </td>
                <td colspan="2" class="itemtable-priview">
                    <label style="font-size: 13px; font-weight: bold;">Additional Compounds </label>
                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Racikan Tambahan)</label><br />
                    <asp:Label runat="server" ID="noracikanadd" Font-Size="13px" Text="No Additional Compound Prescription" />
                    <div style="padding-bottom: 5px" runat="server" id="divRacikanAdditional">
                        <table style="width: 100%;">
                            <tr class="bordertable" style="vertical-align: top">
                                <td style="width: 200px; padding-left: 13px">
                                    <label style="font-size: 13px; font-weight: bold">Name</label>
                                </td>
                                <td style="width: 150px">
                                    <label style="font-size: 13px; font-weight: bold">Dose</label>
                                </td>
                                <%--<td style="width: 100px">
                                    <label style="font-size: 13px; font-weight: bold">Dose UoM</label>
                                </td>--%>
                                <td style="width: 100px">
                                    <label style="font-size: 13px; font-weight: bold">Frequency</label>
                                </td>
                                <td style="width: 80px">
                                    <label style="font-size: 13px; font-weight: bold">Route</label>
                                </td>
                                <td style="width: 140px">
                                    <label style="font-size: 13px; font-weight: bold">Instruction</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Qty</label>
                                </td>
                                <td style="width: 40px">
                                    <label style="font-size: 13px; font-weight: bold">U.O.M</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Iter</label>
                                </td>
                            </tr>
                            <asp:Repeater runat="server" ID="RepeaterRAcikanAdd">
                                <ItemTemplate>
                                    <tr class="bordertable" style="vertical-align: top">
                                        <td style="width: 200px; padding-left: 13px">
                                            <asp:Label ID="racikanname" Style="font-size: 13px" Width="90%" runat="server" Text='<%#Eval("compound_name") %>' Enabled="false" />
                                        </td>
                                        <td style="width: 150px">
                                            <div runat="server" Visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                                <asp:Label ID="Label10" Style="font-size: 13px" runat="server" Enabled="false"><%#Eval("dose","{0:G29}") %></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="racikandoseuom" Style="font-size: 13px" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                            </div>
                                            <asp:Label ID="RacikanDoseText" Style="font-size: 13px" Width="90%" runat="server" Enabled="false" Visible='<%# Eval("IsDoseText") %>'><%#Eval("dose_Text") %></asp:Label>
                                        </td>
                                        <%--<td style="width: 100px">
                                            <asp:Label ID="racikandoseuom" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                        </td>--%>
                                        <td style="width: 100px">
                                            <asp:Label ID="racikanfrequency" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("frequency_code") %></asp:Label>
                                        </td>
                                        <td style="width: 80px">
                                            <asp:Label ID="racikanroute" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_route_code") %></asp:Label>
                                        </td>
                                        <td style="width: 140px">
                                            <asp:Label ID="racikaninstruction" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_instruction") %></asp:Label>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="racikanqty" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                        </td>
                                        <td style="width: 40px">
                                            <asp:Label ID="racikanuom" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("uom_code") %></asp:Label>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="racikaniter" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("iter") %></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="border: solid 1px #cdced9" runat="server" id="trAdditionalConsumables">
                <td style="background-color: #efefef;" class="itemtable-priview-title">
                    <label style="padding-left: 13px; font-weight: bold"></label>
                </td>
                <td colspan="2" class="itemtable-priview">
                    <label style="font-size: 13px; font-weight: bold;">Additional Consumables</label>
                    <label style="font-size: 13px; font-weight: bold; font-style: italic;">(Tambah Alat Kesehatan)</label><br />
                    <asp:Label runat="server" ID="lbladditionalcons" Font-Size="13px" Text="No Consumable Prescription" />
                    <div runat="server" id="dvAdditionalConsumables">
                        <table style="width: 100%">
                            <tr class="bordertable" style="vertical-align: top">
                                <td style="width: 350px; padding-left: 13px">
                                    <label style="font-size: 13px; font-weight: bold">Item</label>
                                </td>
                                <td style="width: 50px">
                                    <label style="font-size: 13px; font-weight: bold">Qty</label>
                                </td>
                                <td style="width: 40px">
                                    <label style="font-size: 13px; font-weight: bold">U.O.M</label>
                                </td>
                                <td style="width: 240px">
                                    <label style="font-size: 13px; font-weight: bold">Dose & Instruction</label>
                                </td>
                            </tr>
                            <asp:Repeater runat="server" ID="rptAdditionalConsumables">
                                <ItemTemplate>
                                    <tr class="bordertable" style="vertical-align: top">
                                        <td style="width: 350px; padding-left: 13px">
                                            <asp:Label ID="NameLabel" Style="font-size: 13px" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Label ID="Label1" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                        </td>
                                        <td style="width: 40px">
                                            <asp:Label ID="Label2" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("uom") %></asp:Label>
                                        </td>
                                        <td style="width: 240px">
                                            <asp:Label ID="Label4" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("instruction") %></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </td>
            </tr>
            </table>
        </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>