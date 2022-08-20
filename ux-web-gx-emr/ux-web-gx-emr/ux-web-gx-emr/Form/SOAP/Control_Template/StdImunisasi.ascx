<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdImunisasi.ascx.cs" Inherits="Form_SOAP_Control_Template_StdImunisasi" %>

<asp:HiddenField ID="hfguidadditional" runat="server" />

<div class="container-fluid">
    <asp:UpdatePanel ID="UpdatePanelControlImunisasi" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row" style="transform: translate(0,0);">
                <div class="col-sm-12 modal-header" style="padding: 7px 25px 7px 15px; min-height: 35px;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%; text-align: left;">
                                <label style="font-family: Helvetica; font-weight: bold; font-size: 14px;">Imunisasi </label>
                            </td>
                            <td style="width: 50%; text-align: right;">
                                <asp:UpdatePanel ID="UpdatePanelSaveImunisasi" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <%--<button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>--%>
                                        <asp:Button ID="ButtonCancelImunisasi" class="btn btn-default" runat="server" Text="Cancel" OnClick="ButtonCancelImunisasi_Click" />
                                        <asp:Button ID="ButtonSaveImunisasi" class="btn btn-lightGreen" runat="server" Text="Save" OnClick="ButtonSaveImunisasi_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-sm-12 modal-header" style="padding: 7px 25px 7px 15px; min-height: 35px;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%; text-align: left;">
                                <div class="row">
                                    <div class="col-sm-2" style="padding-right: 0px; width: 55px;">
                                        <asp:Image runat="server" ID="imgSex" Width="34px" />
                                    </div>
                                    <div class="col-sm-10" style="padding-left: 0px;">
                                        <asp:Label ID="LabelNamaPasienVaccine" runat="server" Text="-" Style="font-family: Helvetica; font-weight: bold; font-size: 14px;"></asp:Label>
                                        <br />
                                        <asp:Label ID="LabelAgePasienVaccine" runat="server" Text="-" Style="font-family: Helvetica; color: #76767c; font-size: 12px;"></asp:Label>
                                    </div>
                                </div>
                            </td>
                            <td style="width: 50%; text-align: right;">
                                <div class="pretty p-icon p-curve" style="margin-right: 20px; display:none;">
                                    <asp:CheckBox runat="server" ID="chk_autofillsoap" TabIndex="-1" />
                                    <div class="state p-success">
                                        <i class="icon fa fa-check"></i>
                                        <label id="lblbhs_autofillsoap" style="font-size: 12px;">Auto-fill SOAP </label>
                                    </div>
                                </div>
                                PILIH TAMPILAN : 
                                <asp:DropDownList ID="DDLtemplateimunisasi" runat="server" Style="width: 165px;" AutoPostBack="true" OnSelectedIndexChanged="DDLtemplateimunisasi_SelectedIndexChanged">
                                    <asp:ListItem Value="0"> Tabel Imunisasi </asp:ListItem>
                                    <asp:ListItem Value="1"> Kalender Imunisasi Dewasa </asp:ListItem>
                                    <asp:ListItem Value="2"> Kalender Imunisasi Anak </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>

                <!-- Table View -->
                <div id="DivImunisasiTable" runat="server">
                    <div class="col-sm-6">
                        <div style="padding: 5px;">
                            <table border="0">
                                <tr>
                                    <td>
                                        <label style="font-family: Helvetica; font-weight: bold; font-size: 14px;">Imunisasi Anak </label>
                                    </td>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgressTableAnak" runat="server" AssociatedUpdatePanelID="UpdatePanelImunisasiAnak">
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
                        </div>
                        <table style="width: 100%; font-weight: bold; border-color: #b9b9b9; border-width: 0px;" border="1" class="table-condensed table-sub-header-label">
                            <tr>
                                <td style="width: 20%;">Imunisasi</td>
                                <td style="width: 5%;">No</td>
                                <td style="width: 15%;">Tanggal</td>
                                <td style="width: 20%;">Dokter</td>
                                <td style="width: 18%;">Tanggal Exp.</td>
                                <td style="width: 17%;">Brand/No.Lot</td>
                                <td style="width: 5%;">&nbsp; </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanelImunisasiAnak" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Repeater ID="RepeaterImunisasiAnak" runat="server" OnItemDataBound="RepeaterImunisasiAnak_ItemDataBound">
                                    <ItemTemplate>
                                        <table style="width: 100%; border-color: #b9b9b9; border-width: 0px;" border="1">
                                            <tr>
                                                <td style="width: 20%; padding: 5px; vertical-align: top;">
                                                    <asp:HiddenField ID="HF_imunisasiIdAnk" Value='<%# Bind("vaccine_id") %>' runat="server" />
                                                    <asp:Label ID="Label_imunisasinameAnk" runat="server" Text='<%# Bind("vaccine_name") %>'></asp:Label>
                                                </td>
                                                <td style="width: 80%;">
                                                    <asp:GridView ID="GridViewImunisasiAnak" runat="server" AutoGenerateColumns="False" ShowHeader="false" Width="100%" BorderColor="#b9b9b9">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="6%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="HF_vaccinationid" runat="server" Value='<%# Bind("vaccination_id") %>' />
                                                                    <asp:HiddenField ID="HF_vaccineid" runat="server" Value='<%# Bind("vaccine_id") %>' />
                                                                    <asp:HiddenField ID="HF_vaccinationage" runat="server" Value='<%# Bind("vaccination_age") %>' />
                                                                    <%--<asp:HiddenField ID="HF_doctorid" runat="server" Value='<%# Bind("doctor_id") %>' />--%>
                                                                    <%--<asp:TextBox ID="Txt_noSequenceDws" runat="server" Style="width:100%;" CssClass="nooutlinenoborder" Text='<%# Container.DataItemIndex + 1 %>'></asp:TextBox>--%>
                                                                    <asp:Label ID="Lbl_noSequenceAnk" runat="server" Text='<%# Eval("vaccine_id").ToString() == "18" ? Container.DataItemIndex : (Container.DataItemIndex + 1) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="19%" ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Txt_tglImunisasiAnk" runat="server" Style="width: 100%;" CssClass="mask-date nooutlinenoborder" placeholder="dd/mm/yyyy" Text='<%# Bind("vaccination_date") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="25%" ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Txt_namaDokterAnk" runat="server" TextMode="MultiLine" Rows="1" Style="width: 100%; resize: vertical; overflow: hidden;" CssClass="nooutlinenoborder" Text='<%# Bind("doctor_name") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="23%" ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Txt_expDateAnk" runat="server" Style="width: 100%;" CssClass="nooutlinenoborder" placeholder="Type here..." Text='<%# Bind("expiry_date") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="21%" ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Txt_noLotAnk" runat="server" Style="width: 100%;" CssClass="nooutlinenoborder" placeholder="Type here..." Text='<%# Bind("no_lot") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="6%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <%--<asp:LinkButton ID="btndelete_ImunisasiDws" OnClick="btndelete_ImunisasiDws_Click" runat="server"><span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_delete.svg") %>" class="ic_delete"></span></asp:LinkButton>--%>
                                                                    <asp:ImageButton ID="btndelete_ImunisasiAnk" OnClick="btndelete_ImunisasiAnk_Click" runat="server" CssClass="ic_delete" ImageUrl="~/Images/Icon/ic_delete.svg" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Button ID="LB_addnewrowanak" OnClick="LB_addnewrowanak_Click" runat="server" Style="width: 100%; padding: 5px; background-color: transparent; border: 0px; color: #4d9b35; font-weight: bold; text-align: left;" Text="+ Add date" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-6">
                        <div style="padding: 5px;">
                            <table border="0">
                                <tr>
                                    <td>
                                        <label style="font-family: Helvetica; font-weight: bold; font-size: 14px;">Imunisasi Dewasa </label>
                                    </td>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgressTableDewasa" runat="server" AssociatedUpdatePanelID="UpdatePanelImunisasiDewasa">
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
                        </div>
                        <table style="width: 100%; font-weight: bold; border-color: #b9b9b9; border-width: 0px;" border="1" class="table-condensed table-sub-header-label">
                            <tr>
                                <td style="width: 20%;">Imunisasi</td>
                                <td style="width: 5%;">No</td>
                                <td style="width: 15%;">Tanggal</td>
                                <td style="width: 20%;">Dokter</td>
                                <td style="width: 18%;">Tanggal Exp.</td>
                                <td style="width: 17%;">Brand/No.Lot</td>
                                <td style="width: 5%;">&nbsp; </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanelImunisasiDewasa" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Repeater ID="RepeaterImunisasiDewasa" runat="server" OnItemDataBound="RepeaterImunisasiDewasa_ItemDataBound">
                                    <ItemTemplate>
                                        <table style="width: 100%; border-color: #b9b9b9; border-width: 0px;" border="1">
                                            <tr>
                                                <td style="width: 20%; padding: 5px; vertical-align: top;">
                                                    <asp:HiddenField ID="HF_imunisasiIdDws" Value='<%# Bind("vaccine_id") %>' runat="server" />
                                                    <asp:Label ID="Label_imunisasinameDws" runat="server" Text='<%# Bind("vaccine_name") %>'></asp:Label>
                                                </td>
                                                <td style="width: 80%;">
                                                    <asp:GridView ID="GridViewImunisasiDewasa" runat="server" AutoGenerateColumns="False" ShowHeader="false" Width="100%" BorderColor="#b9b9b9">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="6%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="HF_vaccinationid" runat="server" Value='<%# Bind("vaccination_id") %>' />
                                                                    <asp:HiddenField ID="HF_vaccineid" runat="server" Value='<%# Bind("vaccine_id") %>' />
                                                                    <asp:HiddenField ID="HF_vaccinationage" runat="server" Value='<%# Bind("vaccination_age") %>' />
                                                                    <%--<asp:HiddenField ID="HF_doctorid" runat="server" Value='<%# Bind("doctor_id") %>' />--%>
                                                                    <%--<asp:TextBox ID="Txt_noSequenceDws" runat="server" Style="width:100%;" CssClass="nooutlinenoborder" Text='<%# Container.DataItemIndex + 1 %>'></asp:TextBox>--%>
                                                                    <asp:Label ID="Lbl_noSequenceDws" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="19%" ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Txt_tglImunisasiDws" runat="server" Style="width: 100%;" CssClass="mask-date nooutlinenoborder" placeholder="dd/mm/yyyy" Text='<%# Bind("vaccination_date") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="25%" ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Txt_namaDokterDws" runat="server" TextMode="MultiLine" Rows="1" Style="width: 100%; resize: vertical; overflow: hidden;" CssClass="nooutlinenoborder" Text='<%# Bind("doctor_name") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="23%" ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Txt_expDateDws" runat="server" Style="width: 100%;" CssClass="nooutlinenoborder" placeholder="Type here..." Text='<%# Bind("expiry_date") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="21%" ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Txt_noLotDws" runat="server" Style="width: 100%;" CssClass="nooutlinenoborder" placeholder="Type here..." Text='<%# Bind("no_lot") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="6%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <%--<asp:LinkButton ID="btndelete_ImunisasiDws" OnClick="btndelete_ImunisasiDws_Click" runat="server"><span><img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_delete.svg") %>" class="ic_delete"></span></asp:LinkButton>--%>
                                                                    <asp:ImageButton ID="btndelete_ImunisasiDws" OnClick="btndelete_ImunisasiDws_Click" runat="server" CssClass="ic_delete" ImageUrl="~/Images/Icon/ic_delete.svg" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Button ID="LB_addnewrowdewasa" OnClick="LB_addnewrowdewasa_Click" runat="server" Style="width: 100%; padding: 5px; background-color: transparent; border: 0px; color: #4d9b35; font-weight: bold; text-align: left;" Text="+ Add date" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <!-- Kalender View Dewasa -->
                <div id="DivImunisasiKalenderDewasa" runat="server" visible="false">
                    <div class="col-sm-12 modal-header" style="padding: 7px 25px 7px 15px; min-height: 35px;">
                        <table style="width: 100%;">
                        <tr>
                            <td style="width: 30%; text-align: left;">
                                <div class="pretty p-icon p-curve" style="margin-right: 20px; display:none;">
                                    <asp:CheckBox runat="server" ID="CheckBoxDewasa" TabIndex="-1" />
                                    <div class="state p-success">
                                        <i class="icon fa fa-check"></i>
                                        <label id="lblbhs_tampilrekomendasiDws" style="font-size: 12px;"> Tampilkan Rekomendasi </label>
                                    </div>
                                </div>
                            </td>
                            <td style="width: 40%; text-align: center;">
                                <label style="font-weight:bold; font-size:20px;"> KALENDER IMUNISASI DEWASA </label>
                            </td>
                            <td style="width: 30%; text-align: right;">
                                <asp:Button ID="ButtonSwitchToAnak" runat="server" CssClass="btn btn-default btn-sm" Text="<  Kalender Anak" OnClick="ButtonSwitchToAnak_Click" />
                            </td>
                        </tr>
                        </table>
                    </div>

                    <div class="col-sm-12">
                         <table style="width: 100%; font-weight: bold; border-color: #b9b9b9; border-width: 0px;" border="1" class="table-condensed table-sub-header-label">
                            <tr>
                                <td style="width: 28%; text-align:center;" rowspan="2">Imunisasi</td>
                                <td style="width: 72%; text-align:center;" colspan="6"> Usia (Tahun) </td>
                            </tr>
                             <tr>       
                                <td style="width: 12%; text-align:center;">19 - 21</td>
                                <td style="width: 12%; text-align:center;">22 - 26</td>
                                <td style="width: 12%; text-align:center;">27 - 49</td>
                                <td style="width: 12%; text-align:center;">50 - 59</td>
                                <td style="width: 12%; text-align:center;">60 - 64</td>
                                <td style="width: 12%; text-align:center;"> &#8805; 65</td>
                             </tr>
                        </table>
                        <asp:GridView ID="GvwKalenderImunisasiDewasa" runat="server" AutoGenerateColumns="False" ShowHeader="false" Width="100%" BorderColor="#b9b9b9" CssClass="table-condensed" OnRowDataBound="GvwKalenderImunisasiDewasa_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="28%" ShowHeader="false" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="HF_imunisasiIdDws_Kal" Value='<%# Bind("vaccine_id") %>' runat="server" />
                                        <asp:Label ID="Label_imunisasinameDws_Kal" runat="server" Text='<%# Bind("vaccine_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="12%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_Age1921" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="12%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_Age2226" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="12%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_Age2749" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="12%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_Age5059" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="12%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_Age6064" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="12%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_Age65" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12" style="margin-top:15px;">
                        <table border="0">
                            <tr>
                                <td style="padding-bottom:7px;"> <div style="background-color:#d6e05f; border:1px solid #b9b9b9; width:50px; height:15px;"></div></td>
                                <td style="padding-bottom:7px; padding-left:2px;"> Diberikan kepada semua orang sesuai dengan kelompok usianya</td>
                            </tr>
                             <tr>
                                <td style="padding-bottom:7px;"> <div style="background-color:#dacbef; border:1px solid #b9b9b9; width:50px; height:15px;"></div></td>
                                <td style="padding-bottom:7px; padding-left:2px;"> Diberikan hanya kepada orang yang memiliki risiko (misalnya: pekerjaan, gaya hidup, bepergian, dll)</td>
                            </tr>
                            <tr>
                                <td style="padding-bottom:7px;"> <div style="background-color:#fcd2a8; border:1px solid #b9b9b9; width:50px; height:15px;"></div></td>
                                <td style="padding-bottom:7px; padding-left:2px;"> Diberikan pada daerah endemis atau yang bepergian ke daerah tersebut</td>
                            </tr>
                            <tr>
                                <td style="padding-bottom:7px;"> <div style="background-color:#ffffff; border:1px solid #b9b9b9; width:50px; height:15px;"></div></td>
                                <td style="padding-bottom:7px; padding-left:2px;"> Tidak ada rekomendasi</td>
                            </tr>
                            <tr>
                                <td colspan="2"> Jadwal imunisasi dewasa merupakan lanjutan dari Jadwal Imunisasi Anak. Informasi mendetail menegenai rekomendasi ini dapat dilihat pada catatan kaki</td>
                            </tr>
                        </table>
                    </div>
                </div>

                <!-- Kalender View Anak -->
                <div id="DivImunisasiKalenderAnak" runat="server" visible="false">
                    <div class="col-sm-12 modal-header" style="padding: 7px 25px 7px 15px; min-height: 35px;">
                        <table style="width: 100%;">
                        <tr>
                            <td style="width: 30%; text-align: left;">
                                <div class="pretty p-icon p-curve" style="margin-right: 20px; display:none;">
                                    <asp:CheckBox runat="server" ID="CheckBoxAnak" TabIndex="-1" />
                                    <div class="state p-success">
                                        <i class="icon fa fa-check"></i>
                                        <label id="lblbhs_tampilrekomendasiAnk" style="font-size: 12px;"> Tampilkan Rekomendasi </label>
                                    </div>
                                </div>
                            </td>
                            <td style="width: 40%; text-align: center;">
                                <label style="font-weight:bold; font-size:20px;"> KALENDER IMUNISASI ANAK </label>
                            </td>
                            <td style="width: 30%; text-align: right;">
                                <asp:Button ID="ButtonSwitchToDewasa" runat="server" CssClass="btn btn-default btn-sm" Text="<  Kalender Dewasa" OnClick="ButtonSwitchToDewasa_Click" />
                            </td>
                        </tr>
                        </table>
                    </div>

                    <div class="col-sm-12">
                         <table style="width: 100%; font-weight: bold; border-color: #b9b9b9; border-width: 0px;" border="1" class="table-condensed table-sub-header-label">
                            <tr>
                                <td style="width: 16%; text-align:center;" rowspan="3">Imunisasi</td>
                                <td style="width: 84%; text-align:center;" colspan="21"> Usia </td>
                            </tr>
                             <tr>       
                                <td style="width: 42%; text-align:center;" colspan="12">Bulan</td>
                                <td style="width: 42%; text-align:center;" colspan="9">Tahun</td> 
                             </tr>
                             <tr>       
                                 <td style="width: 4%; text-align:center;">Lahir</td>
                                 <td style="width: 4%; text-align:center;">1</td>
                                 <td style="width: 4%; text-align:center;">2</td>
                                 <td style="width: 4%; text-align:center;">3</td>
                                 <td style="width: 4%; text-align:center;">4</td>
                                 <td style="width: 4%; text-align:center;">5</td>
                                 <td style="width: 4%; text-align:center;">6</td>
                                 <td style="width: 4%; text-align:center;">9</td>
                                 <td style="width: 4%; text-align:center;">12</td>
                                 <td style="width: 4%; text-align:center;">15</td>
                                 <td style="width: 4%; text-align:center;">18</td>
                                 <td style="width: 4%; text-align:center;">24</td>

                                 <td style="width: 4%; text-align:center;">3</td>
                                 <td style="width: 4%; text-align:center;">5</td>
                                 <td style="width: 4%; text-align:center;">6</td>
                                 <td style="width: 4%; text-align:center;">7</td>
                                 <td style="width: 4%; text-align:center;">8</td>
                                 <td style="width: 4%; text-align:center;">9</td>
                                 <td style="width: 4%; text-align:center;">10</td>
                                 <td style="width: 4%; text-align:center;">12</td>
                                 <td style="width: 4%; text-align:center;">18</td>
                             </tr>
                        </table>
                        <asp:GridView ID="GvwKalenderImunisasiAnak" runat="server" AutoGenerateColumns="False" ShowHeader="false" Width="100%" BorderColor="#b9b9b9" CssClass="table-condensed" OnRowDataBound="GvwKalenderImunisasiAnak_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="16%" ShowHeader="false" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="HF_imunisasiIdAnk_Kal" Value='<%# Bind("vaccine_id") %>' runat="server" />
                                        <asp:Label ID="Label_imunisasinameAnk_Kal" runat="server" Text='<%# Bind("vaccine_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeBlnLahir" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeBln1" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeBln2" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeBln3" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeBln4" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeBln5" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeBln6" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeBln9" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeBln12" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeBln15" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeBln18" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeBln24" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeThn3" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeThn5" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeThn6" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeThn7" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeThn8" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeThn9" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeThn10" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeThn12" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="4%" ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_AgeThn18" style="font-weight:bold;" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="col-sm-12" style="margin-top:15px;">
                        <table border="0">
                            <tr>
                                <td style="padding-bottom:7px; font-weight:bold;"> Keterangan </td>   
                            </tr>
                            <tr>
                                <td style="padding-bottom:7px;"> Cara memebaca kolom usia : misal <span style="background-color:#cde398; padding-left:15px; padding-right:15px;">2</span> berarti usia 2 bulan (60 hari) s.d. 2 bulan 29 hari (89 hari)</td> 
                            </tr>
                            <tr>
                                <td style="padding-bottom:7px;"> Rekomendasi imunisasi berlaku mulai <span style="color:orangered;"> Januari 2017 </span></td> 
                            </tr>
                            <tr>
                                <td style="padding-bottom:7px;"> Dapat diakses pada <b>website IDAI (http://idai.or.id/public-articles/klinik/imunisasi/jadwal-imunisasi-2017)</b></td> 
                            </tr>
                            <tr>
                                <td style="padding-bottom:7px; padding-top:10px;"> 
                                    <div style="background-color:#cde398; border:1px solid #b9b9b9; width:50px; height:15px; display:inline-block;"></div> Optimal 
                                    <div style="background-color:#ffed64; border:1px solid #b9b9b9; width:50px; height:15px; display:inline-block; margin-left:30px;"></div> Catch-up
                                    <div style="background-color:#00b8f1; border:1px solid #b9b9b9; width:50px; height:15px; display:inline-block; margin-left:30px;"></div> Booster
                                    <div style="background-color:#fb71b5; border:1px solid #b9b9b9; width:50px; height:15px; display:inline-block; margin-left:30px;"></div> Daerah Endemis
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        //Mask the textbox as per your format 123-123-123
        $('.mask-date').mask("00/00/0000", {
            placeholder: "__/__/____"
        });

        //fungsi untuk menjaga style pada saat postback dalam updatepanel
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {

            prm.add_beginRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                }
            });

            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('.mask-date').mask("00/00/0000", {
                        placeholder: "__/__/____"
                    });
                }
            });
        };
    });
</script>
