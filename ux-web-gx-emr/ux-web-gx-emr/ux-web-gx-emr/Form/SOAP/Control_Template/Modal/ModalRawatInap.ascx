<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalRawatInap.ascx.cs" Inherits="Form_SOAP_Control_Template_Modal_ModalRawatInap" %>
<link href="../../../../Content/timepicker/bootstrap-datetimepicker.css" rel="stylesheet" />
<style>
    
    input[type='checkbox']:before{
        margin-right:0px;
        padding:3px;
        border-radius: 3px;
    }
    
    .cblist > table {
        border-collapse: separate; 
        border-spacing: 0 2em ;
    }

    .cblist label{
        margin-right:40px;
        padding:3px
    }
    
    #div_ward  table{
        border-collapse:separate ; 
        border-spacing: 0 1em ;
    }

    #cbl_recoveryroom table{

    }
    
    .cbx_recoveryroom{
        margin-right:30px;
        padding:3px
    }

</style>
<%--<link href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.37/css/bootstrap-datetimepicker.css" rel="stylesheet"/>--%>



<div class="content-rawat-inap">
    <asp:UpdateProgress ID="UpdateProgress15" runat="server" AssociatedUpdatePanelID="Up_rawatinap">
	    <ProgressTemplate>
		    <div class="modal-backdrop" style="background-color: black; opacity: 0.6; vertical-align: central; text-align: center"></div>
			<div style="margin-top: 150px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
	            <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
		    </div>
		</ProgressTemplate>
    </asp:UpdateProgress>
   
    <asp:UpdatePanel runat="server" ID="Up_rawatinap" UpdateMode="Conditional">
        <ContentTemplate>
            <input type="hidden" id="hfPatientId" runat="server" />
       


            <!-- Use --->
            <asp:HiddenField ID="hfOperationScheduleAdditionalId" runat="server" />
            <asp:HiddenField ID="hf_operationScheduleId2" runat="server" />
            <!-- Use --->

            <input type="hidden" id="Hidden1" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="hfAdmissionId" runat="server" />
            <asp:HiddenField ID="hfPagefaId" runat="server" />
            <asp:HiddenField ID="hfPageSoapId" runat="server" />
            <asp:HiddenField ID="hfsavemode" runat="server" />
            <asp:HiddenField ID="hfitemid" runat="server" />
            <asp:HiddenField ID="hfitemname" runat="server" />
            <asp:HiddenField ID="hfconsfee" runat="server" />
            <asp:HiddenField ID="hfdiscamount" runat="server" />
            <asp:HiddenField ID="hfsubmited" runat="server" />
            <asp:HiddenField ID="hfsoapstring" runat="server" />
            <asp:HiddenField ID="hfguidadditional" runat="server" />
            <asp:HiddenField ID="hfmandatorySOAP" runat="server" />
            <asp:HiddenField ID="hfmandatoryFA" runat="server" />
            <asp:HiddenField ID="hftakedate" runat="server" />
            <asp:HiddenField ID="hfadditionaltakedate" runat="server" />
            <asp:HiddenField ID="hfprescriptionHOPE" runat="server" />
            <asp:HiddenField ID="hfDoctorID" runat="server" />

                
            <asp:HiddenField ID="hfstatusId" runat="server"  Value='<%# Bind("status_id") %>' />
            <asp:HiddenField ID="hfoperationScheduleId" runat="server"  Value='<%# Bind("operation_schedule_id") %>' />
            <asp:HiddenField ID="hfEncounterId" runat="server" Value='<%# Bind("encounter_id") %>'  />
        

<%--        <asp:HiddenField ID="hfstatusBookingId" runat="server"  Value='<%# Bind("status_booking_id") %>' />--%>
            <!-- DOKTER NAME -->
            <div class="row" style="margin-bottom:20px; margin-top:10px;">
                <div class="col-sm-12">
                    <label><strong>Dokter Penanggung Jawab<sup style="color: red; font-weight:900;">*</sup></strong></label>
                    <asp:TextBox runat="server" ID="textbox_dokter" Style="width:283px;height:32px;border-radius:4px"  Text='<%# Eval("doctor_name").ToString() %>' />
                </div>
            </div>

            <!-- DIAGNOSIS -->
            <div class="row" style="margin-bottom:20px">
                <div class="col-sm-12">
                    <label style="display:block"><strong>Diagnosis<sup style="color: red; font-weight:900;">*</sup></strong></label>
                    <asp:TextBox ID="textbox_diagnosis" runat="server" Enabled="false"  Value='<%# Eval("diagnosis").ToString() %>' TextMode="MultiLine" Rows="4" Style="width:100%; border: solid 1px #cdced9; resize:none;height:80px; max-width: 100%; padding-left:7px;border-radius:4px; padding-right:7px" CssClass="scrollEMR"></asp:TextBox>
                </div>
            </div>

            <!-- DATE & TIME / ADMISSION DATE -->
            <div class="row" style="margin-bottom:20px">
                <div class="col-sm-4">
                    <label style="display:block"><strong>Tanggal Masuk Rawat</strong> (Optional)</label>
                    <div class="has-feedback" style="display: inline;">
                        <asp:TextBox runat="server" ID="txttglmasukrawat" Style="width:216px;height:32px;border-radius:4px; border: solid 1px #cdced9;  resize: none; padding-left: 5px;" Text='<%# Eval("admission_date").ToString() %>' onmousedown="tglmasukrawat();"></asp:TextBox>
                        <span id="searchcalendar" runat="server" class="fa fa-calendar-o" style="margin-left: -22px"></span>
                    </div>
                </div>
                <div class="col-sm-8">
                    <label style="display:block"><strong>Waktu</strong> (Optional)</label>
                    <div class="has-feedback" style="display: inline;">
                        <asp:TextBox runat="server" ID="txtwaktumasukrawat" Style="width:216px;height:32px;border-radius:4px; border: solid 1px #cdced9; resize: none; padding-left: 5px;" Text='<%# Eval("admission_date").ToString() %>' onmousedown="wktumasukrawat();"></asp:TextBox>
                        <span id="searchclock" runat="server" class="fa fa-clock-o" style="margin-left: -22px"></span>
                    </div>
                </div>
            </div>

            <!-- BANGSAL -->
            <label><strong>Bangsal</strong></label>
            <div class="row" id="div_ward" style="margin-bottom:20px">
                <div class="row" style="margin-left:15px">
                    <asp:CheckBoxList runat="server" ID="cbl_ward" class="cblist" RepeatColumns="4" RepeatDirection="Horizontal"  DataTextField="wardName" DataValueField="wardId"></asp:CheckBoxList>
                    
                    <asp:CheckBox runat="server" Value="3" ID="chck_BangsalLain" onclick="javascript:chckBangsalLain(this);" />
                    <div style="margin-left:10px; display:inline-block;">
                        <asp:TextBox runat="server" style="margin-left:-10px;" Enabled="false" Width="170px" Height="32" ID="txt_BangsalLain" placeholder="Lain-lain"/>
                    </div>
                </div>
            </div>
            
            <!-- PERKIRAAN LAMA RAWAT -->
            <div class="row" style="margin-bottom:20px">
                <div class="col-sm-12">
                    <label style="display:block"><strong>Perkiraan Lama Rawat</strong></label>
                    <asp:CheckBox runat="server" ID="chbx_lamarawat_kurangseminggu" Text=" <7 Hari"  onclick="javascript:chckLamaRawatKrngSeminggu(this);" />
                    <asp:CheckBox runat="server" ID="chbx_lamarawat_lebihseminggu" Text=" >7 Hari" style="margin-left:40px" onclick="javascript:chckLamaRawatLbhSeminggu(this);" />
                </div>
            </div>

            <!-- TINDAKAN OPERASI/PROSEDUR -->
            <div class="row" style="margin-bottom:20px">
                <div class="col-sm-12">
                    <label style="display:block"><strong>Tindakan Operasi/Prosedur<sup style="color: red; font-weight:900;">*</sup></strong></label>
                    <asp:CheckBox runat="server" ID="chbx_tindakanoperasi_tidak" Text=" Tidak" onclick="javascript:chckTindakanOperasiTidak(this);" />
                    <asp:CheckBox runat="server" ID="chbx_tindakanoperasi_ya" style="margin-left:50px" Text=" Ya" onclick="javascript:chckTindakanOperasiYa(this);"/>
                </div>
            </div>

            <!-- TINDAKAN OPERASI/PROSEDUR SECTION " -->
            <div ID="divTindakanOperasi" style="display:none" >

                <!-- TANGGAL PERKIRAAN OPERASI DAN WAKTU -->
                <div class="row" style="margin-bottom:20px">
                    <div class="col-sm-4">
                        <label style="display:block"><strong>Tanggal Perkiraan Operasi/Tindakan</strong></label>
                        <div class="has-feedback" style="display: inline;">
                            <asp:TextBox runat="server" ID="txt_tglperkiraanoperasi" Style="border-radius: 4px; border: solid 1px #cdced9; height: 32px; width:216px; resize: none; padding-left: 5px;" onmousedown="tglperkiraanoprsi();"></asp:TextBox>
                            <span id="Span1" runat="server" class="fa fa-calendar-o" style="margin-left: -22px"></span>
                        </div>
                    </div>
                    <div class="col-sm-8">
                        <label style="display:block"><strong>Waktu</strong></label>
                        <div class="has-feedback" style="display: inline;">
                            <asp:TextBox runat="server" ID="txtwaktuperkiraanoperasi" Style="border-radius: 4px; border: solid 1px #cdced9; height: 32px; width:216px; resize: none; padding-left: 5px;" onmousedown="wktperkiraanoprsi();"></asp:TextBox>
                            <span id="Span2" runat="server" class="fa fa-clock-o" style="margin-left:-22px"></span>
                        </div>
                    </div>
                </div>

                <!-- NAMA OPERASI/TINDAKAN DAN LAMA OPERASI/TINDAKAN -->
                <div class="row">
                    <div class="col-sm-4">
                        <%--<label style="display:block"><strong>Nama Operasi/Tindakan<sup style="color: red; font-weight:900;">*</sup></strong></label>
                        <asp:DropDownList ID="ddl_namaoperasi" runat="server" Style="cursor: pointer; border: solid 1px #AAAAB3; border-radius: 4px; margin-right:10px" Width="200px" Height="32px"  >
                            <asp:ListItem Value="0">Pilih Operasi/Tindakan</asp:ListItem>
                        </asp:DropDownList>
            
                        <asp:CheckBox runat="server" Text=" Ketik Lainnya" ID="chck_OperasiLain" style="display:block; padding-top:5px;" onclick="javascript:enableOperasiLain(this);" />
                        <div id="namaOperasiLain" style="margin-top:10px">
                            <asp:TextBox runat="server" ID="txt_NamaOperasiLain" Style="display:none; margin-bottom:10px;" Width="200px" Height="32px"></asp:TextBox>
                        </div>--%>

                        <asp:UpdatePanel ID="up_DivProcedure" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table style="border:0; margin-bottom: 4px;">
                                    <tr>
                                        <td>
                                            <label style="display:block"><strong>Nama Operasi/Tindakan<sup style="color: red; font-weight:900;">*</sup></strong></label>
                                        </td>
                                        <td>
                                            <div class="loading-procedure" style="display: none;">
                                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center"></div>&nbsp;
                                                <img alt="" style="background-color: transparent; height: 15px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                <asp:HiddenField ID="hf_LoadingProcedure" runat="server" />
                                            </div>
                                        </td>
                                    </tr>                                                  
                                </table>

                                <!-- tama code-->
                                <asp:UpdatePanel runat="server" ID="up_SearchProcedure" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="display: inline;">
                                            <div class="has-feedback" style="display: inline;">
                                                <asp:TextBox ID="txt_ItemProcedure" runat="server" Placeholder="Add item here..." Style="width: 214px;height:32px;border-radius:4px; font-weight: normal; font-size: 12px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('inpatientProcedure','true');"></asp:TextBox>
                                                <span class="fa fa-chevron-down form-control-feedback" style="margin-top: -9px; z-index: 0; "></span>
                                            </div>
                        
                                            <asp:HiddenField ID="hf_ItemSelectedProcedure" runat="server" />
                                            <asp:HiddenField ID="hf_ItemSelectedProcedure_name" runat="server" />
                                            <asp:HiddenField ID="hf_ItemSelectedProcedure_diagnosis" runat="server" />
                                    
                                            <asp:Button ID="Btn_AjaxSearchProcedure" runat="server" Text="Button" CssClass="hidden" OnClick="BtnAjaxSearchProcedure_Click" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                                <div style="overflow-y: auto; max-height: 250px;" class="scrollEMR">
                                    <div class="col-md-6" style="margin-left: -60px;margin-top: 10px;">
                                         <ul style="">
                                            <asp:GridView ID="gv_procedure" runat="server" AutoGenerateColumns="false" CssClass="table-kecil" ShowHeader="false" DataKeyNames="procedure_name" BorderWidth="0" HeaderStyle-BorderWidth="0">
                                            <Columns>
                                                <asp:TemplateField HeaderText="titik" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="3%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                    <ItemTemplate>
                                                        <i class="fa fa-circle" style="font-size: 8px; vertical-align: middle; margin-right: 2px;"></i>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nama" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hf_id_procedure" runat="server" Value='<%# Bind("operation_procedure_id") %>'></asp:HiddenField>
                                                        <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="lbl_ProcedureName" runat="server" Text='<%# Eval("procedure_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btn_DeleteProcedure" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="BtnDeleteProcedure_Click" Style="width: 12px; height: 12px; margin-top: 3px;" />                                
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>                                
                                        </asp:GridView>
                                         </ul>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="col-sm-8">
                        <label runat="server" style="display:block"><strong>Lama Operasi/Tindakan<sup style="color: red; font-weight:900;">*</sup></strong></label>
                        <asp:TextBox runat="server" ID="txt_JamLamaOperasi" Width="69px" Height="32" Style="text-align:center;border-radius:4px" onkeypress="return validateNumber(event)"></asp:TextBox>
                        <label style="margin-left: 10px;"> Jam</label>
                        &nbsp;&nbsp;
                        <asp:TextBox runat="server" ID="txt_MenitLamaOperasi" Width="69px" Height="32" Style="text-align:center;border-radius:4px;" onkeypress="return validateNumber(event)"></asp:TextBox>
                        <label style="margin-left: 10px;"> Menit</label>
                    </div>
                </div>

                <!-- ANESTHETIC METHOD -->
                <div class="row" style="margin-bottom:20px; margin-top:10px;">
                    <div class="col-sm-12">
                        <label style="display:block"><strong>Anesthetic Method<sup style="color:red;">&nbsp;*</sup></strong></label>
                        <asp:DropDownList Style="cursor: pointer; border: solid 1px #AAAAB3; border-radius: 4px; margin-right:10px" ID="ddl_anasteticmethod" Width="200px" Height="32px" runat="server" >
                            <asp:ListItem Value="-1" >- Anestetic Method -</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- ALAT -->
                <div class="row" style="margin-bottom:15px">
                    <div class="col-sm-12">
                        <label style="display:block"><strong>Alat<sup style="color: red; font-weight:900;">*</sup></strong></label>
                        <asp:CheckBox runat="server" Value="3" ID="chbx_alat_tidak" Text=" Tidak" onclick="javascript:chckAlatTidak(this);" />
                        
                        <asp:CheckBox runat="server" ID="chbx_alat_ya" style="margin-left:50px" onclick="javascript:enableTxtAlat(this);" />
                        <div style="display:inline-block; margin-left:5px;">
                            <asp:TextBox runat="server" style="border-radius:4px" Enabled="false" Width="214px" Height="32" ID="txt_alat_ya" placeholder="Ketik alat disini"/>
                        </div>
                    </div>
                </div>

                <!-- TABEL CATEGORY -->
                <div class="row" style="margin-bottom:15px">
                    <div class="col-sm-12">
                        <label style="display:block"><strong>Tabel Kategori</strong></label>
                        <table style="width:100%">
                            <tr>
                                <td style="width:5%"><asp:CheckBox runat="server" ID="chck_Tabel1" value="1" Text=" 1" onclick="javascript:chckTabel1(this);" /></td>
                                <td style="width:5%"><asp:CheckBox runat="server" ID="chck_Tabel2" value="2" Text=" 2" onclick="javascript:chckTabel2(this);" /></td>
                                <td style="width:5%"><asp:CheckBox runat="server" ID="chck_Tabel3" value="3" Text=" 3" onclick="javascript:chckTabel3(this);" /></td>
                                <td style="width:5%"><asp:CheckBox runat="server" ID="chck_Tabel4" value="4" Text=" 4" onclick="javascript:chckTabel4(this);" /></td>
                                <td style="width:5%"><asp:CheckBox runat="server" ID="chck_Tabel5" value="5" Text=" 5" onclick="javascript:chckTabel5(this);" /></td>
                                <td style="width:5%"><asp:CheckBox runat="server" ID="chck_Tabel6" value="6" Text=" 6" onclick="javascript:chckTabel6(this);" /></td>
                                <td style="width:5%"><asp:CheckBox runat="server" ID="chck_Tabel7" value="7" Text=" 7" onclick="javascript:chckTabel7(this);" /></td>
                                <td style="width:1%;">
                                    <asp:CheckBox runat="server" ID="chck_TabelLain" onclick="javascript:chckTabellain(this);" />
                                </td>
                                <td style="width:64%;">
                                    <asp:TextBox runat="server" style="margin-left:10px;" Enabled="false" Width="214px" Height="32" ID="txt_TabelLain" placeholder="Lain-lain"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <!-- SETELAH OPERASI/TINDAKAN  -->
                <div class="row" style="margin-bottom:15px">
                    <div class="col-sm-12">
                        <label style="display:block"><strong>Setelah Operasi/Tindakan</strong></label>
                        <table style="width:100%">
                            <tr>
                                <td style="width:70%">
                                    <asp:CheckBoxList runat="server" ID="cbl_recoveryroom" CssClass="cblist" RepeatDirection="Horizontal"  DataTextFormatString=""  DataTextField="" DataValueField=""></asp:CheckBoxList>
                                </td>
                                <td style="width:1%">
                                    <asp:CheckBox runat="server" ID="chck_OperasiTindakanLain" onclick="javascript:checkAfterOP(this)"/>
                                </td>
                                <td style="width:29%">
                                    <asp:TextBox runat="server" style="margin-left:10px; display:inline-block;" Width="214px" Enabled="false" Height="32px" ID="txt_OperasiTindakanLain" placeholder="Lain-lain"/>
                                </td>
                            </tr>
                        </table>
                    </div>    
                </div>
                
                <div class="row" style="margin-bottom:20px">
                    <div class="col-sm-12">
                        <label style="display:block"><strong>Persiapan Operasi/Tindakan</strong></label>

                        <!-- PUASA -->
                        <label style="display:block; margin-top:10px;"><strong>Puasa</strong></label>
                        <asp:CheckBox runat="server" Text=" Tidak" ID="chbx_puasa_tidak"  onclick="javascript:chckPuasaTidak(this)" />
                        
                        <asp:CheckBox runat="server" ID="chbx_puasa_ya" style="margin-left:50px" onclick="javascript:chckPuasaYa(this)" />
                        <div style="display:inline-block; margin-left:5px;">
                            <asp:TextBox runat="server" Style="text-align:center;" Height="32" Width="50px" Enabled="false" ID="txt_puasa_ya" onkeypress="return validateNumber(event)" placeholder="0"/> Jam
                        </div>
         
                        <!-- RADIOLOGI -->
                        <asp:UpdatePanel ID="up_DivRad" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table style="margin-top:15px; border:0; margin-bottom: 4px;">
                                    <tr>
                                        <td>
                                            <label><strong>Radiologi</strong></label>
                                        </td>
                                        <td>
                                            <div class="loading-rad" style="display: none;">
                                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center"></div>&nbsp;
                                                <img alt="" style="background-color: transparent; height: 15px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                <asp:HiddenField ID="hf_LoadingRad" runat="server" />
                                            </div>
                                        </td>
                                    </tr>                                                  
                                </table>                         

                                <asp:UpdatePanel runat="server" ID="up_SearchRad" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="display: inline;">
                                            <div class="has-feedback" style="display: inline;">
                                                <asp:TextBox ID="txt_ItemRad" runat="server" Placeholder="Add item here..." Style="width: 214px;height:32px;border-radius:4px; font-weight: normal; font-size: 12px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('cpoerad','true');"></asp:TextBox>
                                                <span class="fa fa-chevron-down form-control-feedback" style="margin-top: -9px; z-index: 0; "></span>
                                            </div>
                        
                                            <asp:HiddenField ID="hf_ItemSelectedRad" runat="server" />
                                            <asp:HiddenField ID="HF_ItemSelectedRad_name" runat="server" />
                                            <asp:HiddenField ID="HF_ItemSelectedRad_remarks" runat="server" />

                                          <%--  <asp:Button ID="Btn_AjaxSearchRad" runat="server" Text="Button" CssClass="hidden" />--%>

                                            <asp:Button ID="Btn_AjaxSearchRad" runat="server" Text="Button" CssClass="hidden" OnClick="BtnAjaxSearchRAD_Click" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
            
                                <div style="overflow-y: auto; max-height: 250px;" class="scrollEMR">
                                    <div class="col-md-6" style="margin-left: -60px;margin-top: 10px;">
                                         <ul style="">
                                            <asp:GridView ID="gv_Rad" runat="server" AutoGenerateColumns="false" CssClass="table-kecil" ShowHeader="false" DataKeyNames="name" BorderWidth="0" HeaderStyle-BorderWidth="0">
                                            <Columns>
                                                <asp:TemplateField HeaderText="titik" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="3%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                    <ItemTemplate>
                                                        <i class="fa fa-circle" style="font-size: 8px; vertical-align: middle; margin-right: 2px;"></i>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nama" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hf_id_rad" runat="server" Value='<%# Bind("id") %>'></asp:HiddenField>
                                                        <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="lbl_NamaRad" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btn_DeleteRad" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="BtnDeleteRad_Click" Style="width: 12px; height: 12px; margin-top: 3px;" Visible='<%# Eval("IsSendHope").ToString() == "0" ? true : false %>' />                                
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>                                
                                        </asp:GridView>
                                         </ul>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <label style="display:block; margin-top:10px;">Other Rad</label>
                        <asp:TextBox runat="server" ID="txt_OtherRad" Width="412px" Height="32px" placeholder="Add Item Here"></asp:TextBox>
                        <span style="display:block; color:#AAB3B9;"><small>Ex: Order diagnostic atau order radiologi yang tidak ada dalam form</small></span>

                        <!-- LABORATORIUM -->
                        <asp:Updatepanel runat="server" ID="up_DivLab" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table style="margin-top:15px; border:0; margin-bottom: 4px;">
                                    <tr>
                                        <td>
                                            <label><strong>Laboratorium</strong></label>
                                        </td>
                                        <td>
                                            <div class="loading-lab" style="display: none;">
                                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center"></div>&nbsp;
                                                <img alt="" style="background-color: transparent; height: 15px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                <asp:HiddenField ID="hf_LoadingLab" runat="server" />
                                            </div>
                                        </td>
                                    </tr>                                                  
                                </table> 

                                <asp:UpdatePanel runat="server" ID="up_SearchLab" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="display: inline;">
                                            <div class="has-feedback" style="display: inline;">
                                                <asp:TextBox ID="txt_ItemLab" runat="server" Placeholder="Add item here..." Style="width: 214px;height:32px; border-radius:4px; font-weight: normal; font-size: 12px;" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('cpoelab','true');"></asp:TextBox>
                                                <span class="fa fa-chevron-down form-control-feedback" style="margin-top: -9px; z-index: 0; "></span>
                                            </div>
                                            <asp:HiddenField ID="hf_ItemSelectedLab" runat="server" />
                                            <asp:Button ID="Btn_AjaxSearchLab" runat="server" Text="Button" CssClass="hidden" OnClick="BtnAjaxSearchLAB_Click" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div style="overflow-y: auto; max-height: 250px;" class="scrollEMR">
                                    <div class="col-md-6" style="margin-left: -60px;margin-top: 10px;">
                                        <ul style="">
                                            <asp:GridView ID="gv_Lab" runat="server" AutoGenerateColumns="false" CssClass="table-kecil" ShowHeader="false" DataKeyNames="name" BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="titik" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="3%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                        <ItemTemplate>
                                                            <i class="fa fa-circle" style="font-size: 8px; vertical-align: middle; margin-right: 2px;"></i>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Nama" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hf_id_lab" runat="server" Value='<%# Bind("id") %>'></asp:HiddenField>
                                                            <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="labname" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btndeletelab" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="BtnDeletelab_Click" Style="width: 12px; height: 12px; margin-top: 3px;" Visible='<%# Eval("IsSendHope").ToString() == "0" ? true : false %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>                                
                                            </asp:GridView>
                                        </ul>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:Updatepanel>
                        <label style="display:block; margin-top:10px;">Other Lab</label>
                        <asp:TextBox runat="server" ID="txt_otherLab" Width="412px" Height="32" placeholder="Add Item Here"></asp:TextBox>
                        <span style="display:block; color:#AAB3B9;"><small>Ex: Order diagnostic atau order lab yang tidak ada dalam form</small></span>
                    </div>
                </div>
            </div>
            <!-- END TINDAKAN OPERASI/PROSEDUR SECTION " -->
   
            <!-- INSTRUKSI -->
            <div class="row" style="margin-bottom:10px">
                <div class="col-sm-12">
                    <label style="display:block"><strong>Instruksi Rawat Inap<sup style="color: red; font-weight:900;">*</sup></strong></label>
                    <asp:TextBox ID="txtinstruksirawatinap" runat="server" placeholder="Tuliskan instruksi rawat inap di sini...." TextMode="MultiLine"  Rows="4" Style=" width:100% !important; resize:none; padding-left:7px;resize: none;border-radius:4px; padding-right:7px"></asp:TextBox>
                </div>
            </div>
            
            <!-- REMARKS -->
            <div class="row" style="margin-bottom:30px">
                <div class="col-sm-12">
                    <label style="color:#76767C; display:block;">REMARKS</label>
                    <asp:TextBox ID="txtremarks" runat="server" placeholder="Type any remarks here..." TextMode="MultiLine" Cols="0" Rows="4" Style="width: 100%; resize:none; max-width: 100%; padding-left:7px; padding-right:7px;border-radius:4px;" CssClass="scrollEMR"></asp:TextBox>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

<script src="../../../../Content/momentjs/moment.min.js"></script>
<script src="../../../../Content/timepicker/bootstrap.js"></script>
<script src="../../../../Content/timepicker/bootstrap-datetimepicker.min.js"></script>

<script type="text/javascript">

    function validateNumber(e) {
        const pattern = /^[0-9]$/;
        return pattern.test(e.key)
    }

    function tglmasukrawat() {
        var dp = $('#<%=txttglmasukrawat.ClientID%>');
        dp.datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd MM yyyy",
            language: "tr"
        }).on('changeDate', function (ev) {
            $(this).blur();
            $(this).datepicker('hide');
        })
    }

    function tglperkiraanoprsi() {
        var dp = $('#<%=txt_tglperkiraanoperasi.ClientID%>');
        dp.datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd MM yyyy",
            language: "tr"
        }).on('changeDate', function (ev) {
            $(this).blur();
            $(this).datepicker('hide');
        })
    }

    function wktumasukrawat() {
        var dp = $('#<%=txtwaktumasukrawat.ClientID%>');
        dp.datetimepicker({
            format: 'LT'
        })
    }

    function wktperkiraanoprsi() {
        var dp = $('#<%=txtwaktuperkiraanoperasi.ClientID%>');
        dp.datetimepicker({
            format: 'LT'
        })
    }

   <%-- function enableOperasiLain(chks) {
        var chck = document.getElementById("<%= chck_OperasiLain.ClientID %>");
        if (chck.checked) {
            $('#<%= ddl_namaoperasi.ClientID %>').attr('style', 'height:32px;border-radius:4px;background-color:#E7E8EF; width:170px');
            $('#<%= ddl_namaoperasi.ClientID %>').attr("disabled", "disabled");
            document.getElementById("<% = ddl_namaoperasi.ClientID %>").value = "0";
            $('#<%= txt_NamaOperasiLain.ClientID %>').show();
            $('#<%= txt_NamaOperasiLain.ClientID %>').focus();
        } else {
            $('#<%= ddl_namaoperasi.ClientID %>').attr('style', 'height:32px;border-radius:4px;background-color:transparent; width:170px');
            $('#<%=ddl_namaoperasi.ClientID %>').removeAttr("disabled");
            document.getElementById("<% = txt_NamaOperasiLain.ClientID %>").value = "";
            $('#<%=txt_NamaOperasiLain.ClientID %>').hide();
        }
    }--%>

    // BANGSAL
    function chckBangsalLain(chks) {
        var cbx_bangsallain = document.getElementById("<%= chck_BangsalLain.ClientID%>");
        var txt_BangsalLain = document.getElementById("<%= txt_BangsalLain.ClientID %>");

        if (cbx_bangsallain.checked) {
            txt_BangsalLain.removeAttribute("disabled");
            $('#<%=cbl_ward.ClientID %>').find("input,button,textarea,select").attr("disabled", "disabled");
            $('#<%=cbl_ward.ClientID %>').find("input,button,textarea,select").removeAttr('checked');
        } else {
            $('#<%=cbl_ward.ClientID %>').find("input,button,textarea,select").attr("disabled", false);
            txt_BangsalLain.setAttribute("disabled", true);
            txt_BangsalLain.value = "";
        }
    }

    // Seletah operasi tindakan
    function checkAfterOP(chks) {
        var cbx_OperasiTindakanLain = document.getElementById("<%= chck_OperasiTindakanLain.ClientID %>");
        var txt_OperasiTindakanLain = document.getElementById("<%= txt_OperasiTindakanLain.ClientID%>");
        if (cbx_OperasiTindakanLain.checked) {
            txt_OperasiTindakanLain.removeAttribute("disabled");
            $('#<%=cbl_recoveryroom.ClientID %>').find("input,button,textarea,select").attr("disabled", "disabled");
            $('#<%=cbl_recoveryroom.ClientID %>').find("input,button,textarea,select").removeAttr('checked');
        } else {
            $('#<%=cbl_recoveryroom.ClientID %>').find("input,button,textarea,select").attr("disabled", false);
            txt_OperasiTindakanLain.setAttribute("disabled", true);
            txt_OperasiTindakanLain.value="";
        }
    }

    // PUASA
    function chckPuasaTidak(chk) {
        var chks = document.getElementById("<% = chbx_puasa_tidak.ClientID %>");
        var cbx_puasaya = document.getElementById("<%=chbx_puasa_ya.ClientID%>");
        var txt_puasa_ya = document.getElementById("<%=txt_puasa_ya.ClientID%>");
        if (chks.checked) {
            txt_puasa_ya.setAttribute("disabled", true);
            cbx_puasaya.checked = false;
        }
    }

    function chckPuasaYa(chk) {
        var cbx_puasatidak = document.getElementById("<% = chbx_puasa_tidak.ClientID %>");
        var chks = document.getElementById("<%=chbx_puasa_ya.ClientID%>");
        var txt_puasa_ya = document.getElementById("<%=txt_puasa_ya.ClientID%>");
        if (chks.checked) {
            txt_puasa_ya.disabled = false;
            cbx_puasatidak.checked = false;
        }
    }
  
    // LAMA RAWAT
    function chckLamaRawatKrngSeminggu(chk) {
        var chks = document.getElementById("<% = chbx_lamarawat_kurangseminggu.ClientID %>");
        if (chks.checked) {
            var chksLbhSeminggu = document.getElementById("<% = chbx_lamarawat_lebihseminggu.ClientID %>");
            chksLbhSeminggu.checked = false;
        }
    }

    function chckLamaRawatLbhSeminggu(chk) {
        var chks = document.getElementById("<% = chbx_lamarawat_lebihseminggu.ClientID %>");
        if (chks.checked) {
            var chksKrngSeminggu = document.getElementById("<% = chbx_lamarawat_kurangseminggu.ClientID %>");
            chksKrngSeminggu.checked = false;
        }
    }

    // TINDAKAN OPERASI
    function chckTindakanOperasiTidak(chc) {
        var chks = document.getElementById("<%= chbx_tindakanoperasi_tidak.ClientID %>");
        if (chks.checked) {
            var chksTindakanOperasi = document.getElementById("<%= chbx_tindakanoperasi_ya.ClientID%>");
            chksTindakanOperasi.checked = false;
            $("#divTindakanOperasi").hide()
        }
    }

    function chckTindakanOperasiYa(chc) {
        var chks = document.getElementById("<%= chbx_tindakanoperasi_ya.ClientID %>");
        if (chks.checked) {
            var chksTindakanOperasi_tidak = document.getElementById("<%= chbx_tindakanoperasi_tidak.ClientID%>");
            chksTindakanOperasi_tidak.checked = false;
            $("#divTindakanOperasi").show()
        } else {
            $("#divTindakanOperasi").hide()
        }
    }

    // ALAT
    function chckAlatTidak(chk) {
        var chks = document.getElementById('<%= chbx_alat_tidak.ClientID%>');
        if (chks.checked) {
            var chksAlat_ya = document.getElementById("<%= chbx_alat_ya.ClientID%>");
            chksAlat_ya.checked = false;
            $('#<%= txt_alat_ya.ClientID %>').attr("disabled", "disabled");
            document.getElementById("<% = txt_alat_ya.ClientID %>").value = "";
        }
    }

    function enableTxtAlat(chk) {
        var chks = document.getElementById("<% = chbx_alat_ya.ClientID %>");
        if (chks.checked) {
            var chksAlat_tidak = document.getElementById("<%= chbx_alat_tidak.ClientID%>");
            chksAlat_tidak.checked = false;
            $('#<%= txt_alat_ya.ClientID %>').removeAttr("disabled");
            $('#<%= txt_alat_ya.ClientID %>').focus();
        } else {
            $('#<%=txt_alat_ya.ClientID %>').attr("disabled", "disabled");
            document.getElementById("<% = txt_alat_ya.ClientID %>").value = "";
        }
    }

    // TABEL CATEGORY
    function chckTabel1(chk) {
        var cbxtabel1 = document.getElementById('<%= chck_Tabel1.ClientID%>');
        if (cbxtabel1.checked) {
            var cbxtabel2 = document.getElementById('<%= chck_Tabel2.ClientID%>');
            var cbxtabel3 = document.getElementById('<%= chck_Tabel3.ClientID%>');
            var cbxtabel4 = document.getElementById('<%= chck_Tabel4.ClientID%>');
            var cbxtabel5 = document.getElementById('<%= chck_Tabel5.ClientID%>');
            var cbxtabel6 = document.getElementById('<%= chck_Tabel6.ClientID%>');
            var cbxtabel7 = document.getElementById('<%= chck_Tabel7.ClientID%>');
            var cbxtabellain = document.getElementById('<%= chck_TabelLain.ClientID%>');
            var txttabellain = document.getElementById('<%= txt_TabelLain.ClientID%>');
            cbxtabel2.checked = false;
            cbxtabel3.checked = false;
            cbxtabel4.checked = false;
            cbxtabel5.checked = false;
            cbxtabel6.checked = false;
            cbxtabel7.checked = false;
            cbxtabellain.checked = false;
            txttabellain.setAttribute("disabled", true);
            txttabellain.value = "";
            console.log("tabel 1");
        }
    }

    function chckTabel2(chk) {
        var cbxtabel2 = document.getElementById('<%= chck_Tabel2.ClientID%>');
         if (cbxtabel2.checked) {
            var cbxtabel1 = document.getElementById('<%= chck_Tabel1.ClientID%>');
            var cbxtabel3 = document.getElementById('<%= chck_Tabel3.ClientID%>');
            var cbxtabel4 = document.getElementById('<%= chck_Tabel4.ClientID%>');
            var cbxtabel5 = document.getElementById('<%= chck_Tabel5.ClientID%>');
            var cbxtabel6 = document.getElementById('<%= chck_Tabel6.ClientID%>');
            var cbxtabel7 = document.getElementById('<%= chck_Tabel7.ClientID%>');
            var cbxtabellain = document.getElementById('<%= chck_TabelLain.ClientID%>');
            var txttabellain = document.getElementById('<%= txt_TabelLain.ClientID%>');
             cbxtabel1.checked = false;
             cbxtabel3.checked = false;
             cbxtabel4.checked = false;
             cbxtabel5.checked = false;
             cbxtabel6.checked = false;
             cbxtabel7.checked = false;
             cbxtabellain.checked = false;
             txttabellain.setAttribute("disabled", true);
             txttabellain.value = "";
             console.log("tabel 2");
         }
    }

    function chckTabel3(chk) {
        var cbxtabel3 = document.getElementById('<%= chck_Tabel3.ClientID%>');
         if (cbxtabel3.checked) {
            var cbxtabel1 = document.getElementById('<%= chck_Tabel1.ClientID%>');
            var cbxtabel2 = document.getElementById('<%= chck_Tabel2.ClientID%>');
            var cbxtabel4 = document.getElementById('<%= chck_Tabel4.ClientID%>');
            var cbxtabel5 = document.getElementById('<%= chck_Tabel5.ClientID%>');
            var cbxtabel6 = document.getElementById('<%= chck_Tabel6.ClientID%>');
            var cbxtabel7 = document.getElementById('<%= chck_Tabel7.ClientID%>');
            var cbxtabellain = document.getElementById('<%= chck_TabelLain.ClientID%>');
             var txttabellain = document.getElementById('<%= txt_TabelLain.ClientID%>');
             cbxtabel1.checked = false;
             cbxtabel2.checked = false;
             cbxtabel4.checked = false;
             cbxtabel5.checked = false;
             cbxtabel6.checked = false;
             cbxtabel7.checked = false;
             cbxtabellain.checked = false;
             txttabellain.setAttribute("disabled", true);
             txttabellain.value = "";
             console.log("tabel 3");
         }
    }

    function chckTabel4(chk) {
        var cbxtabel4 = document.getElementById('<%= chck_Tabel4.ClientID%>');
        if (cbxtabel4.checked) {
            var cbxtabel1 = document.getElementById('<%= chck_Tabel1.ClientID%>');
            var cbxtabel2 = document.getElementById('<%= chck_Tabel2.ClientID%>');
            var cbxtabel3 = document.getElementById('<%= chck_Tabel3.ClientID%>');
            var cbxtabel5 = document.getElementById('<%= chck_Tabel5.ClientID%>');
            var cbxtabel6 = document.getElementById('<%= chck_Tabel6.ClientID%>');
            var cbxtabel7 = document.getElementById('<%= chck_Tabel7.ClientID%>');
            var cbxtabellain = document.getElementById('<%= chck_TabelLain.ClientID%>');
            var txttabellain = document.getElementById('<%= txt_TabelLain.ClientID%>');
            cbxtabel1.checked = false;
            cbxtabel2.checked = false;
            cbxtabel3.checked = false;
            cbxtabel5.checked = false;
            cbxtabel6.checked = false;
            cbxtabel7.checked = false;
            cbxtabellain.checked = false;
            txttabellain.value = "";
            txttabellain.setAttribute("disabled", true);
            console.log("tabel 4");
        }
    }

    function chckTabel5(chk) {
        var cbxtabel5 = document.getElementById('<%= chck_Tabel5.ClientID%>');
        if (cbxtabel5.checked) {
            var cbxtabel1 = document.getElementById('<%= chck_Tabel1.ClientID%>');
            var cbxtabel2 = document.getElementById('<%= chck_Tabel2.ClientID%>');
            var cbxtabel3 = document.getElementById('<%= chck_Tabel3.ClientID%>');
            var cbxtabel4 = document.getElementById('<%= chck_Tabel4.ClientID%>');
            var cbxtabel6 = document.getElementById('<%= chck_Tabel6.ClientID%>');
            var cbxtabel7 = document.getElementById('<%= chck_Tabel7.ClientID%>');
            var cbxtabellain = document.getElementById('<%= chck_TabelLain.ClientID%>');
            var txttabellain = document.getElementById('<%= txt_TabelLain.ClientID%>');
            cbxtabel1.checked = false;
            cbxtabel2.checked = false;
            cbxtabel3.checked = false;
            cbxtabel4.checked = false;
            cbxtabel6.checked = false;
            cbxtabel7.checked = false;
            cbxtabellain.checked = false;
            txttabellain.setAttribute("disabled", true);
            txttabellain.value = "";
            console.log("tabel 5");
        }
    }

    function chckTabel6(chk) {
        var cbxtabel6 = document.getElementById('<%= chck_Tabel6.ClientID%>');
        if (cbxtabel6.checked) {
            var cbxtabel1 = document.getElementById('<%= chck_Tabel1.ClientID%>');
            var cbxtabel2 = document.getElementById('<%= chck_Tabel2.ClientID%>');
            var cbxtabel3 = document.getElementById('<%= chck_Tabel3.ClientID%>');
            var cbxtabel4 = document.getElementById('<%= chck_Tabel4.ClientID%>');
            var cbxtabel5 = document.getElementById('<%= chck_Tabel5.ClientID%>');
            var cbxtabel7 = document.getElementById('<%= chck_Tabel7.ClientID%>');
            var cbxtabellain = document.getElementById('<%= chck_TabelLain.ClientID%>');
            var txttabellain = document.getElementById('<%= txt_TabelLain.ClientID%>');
            cbxtabel1.checked = false;
            cbxtabel2.checked = false;
            cbxtabel3.checked = false;
            cbxtabel4.checked = false;
            cbxtabel5.checked = false;
            cbxtabel7.checked = false;
            txttabellain.setAttribute("disabled", true);
            cbxtabellain.checked = false;
            txttabellain.value = "";
            console.log("tabel 6");
        }
    }

    function chckTabel7(chk) {
        var cbxtabel7 = document.getElementById('<%= chck_Tabel7.ClientID%>'); 

        if (cbxtabel7.checked) {
            var cbxtabel1 = document.getElementById('<%= chck_Tabel1.ClientID%>');
            var cbxtabel2 = document.getElementById('<%= chck_Tabel2.ClientID%>');
            var cbxtabel3 = document.getElementById('<%= chck_Tabel3.ClientID%>');
            var cbxtabel4 = document.getElementById('<%= chck_Tabel4.ClientID%>');
            var cbxtabel5 = document.getElementById('<%= chck_Tabel5.ClientID%>');
            var cbxtabel6 = document.getElementById('<%= chck_Tabel6.ClientID%>');
            var cbxtabellain = document.getElementById('<%= chck_TabelLain.ClientID%>');
            var txttabellain = document.getElementById('<%= txt_TabelLain.ClientID%>');
            cbxtabel1.checked = false;
            cbxtabel2.checked = false;
            cbxtabel3.checked = false;
            cbxtabel4.checked = false;
            cbxtabel5.checked = false;
            cbxtabel6.checked = false;
            cbxtabellain.checked = false;
            txttabellain.setAttribute("disabled", true);
            txttabellain.value = "";
            console.log("tabel 7");
        }
    }

    function chckTabellain(chk) {
         var cbxTabellain = document.getElementById('<%= chck_TabelLain.ClientID%>');
         var cbxtabel1 = document.getElementById('<%= chck_Tabel1.ClientID%>');
         var cbxtabel2 = document.getElementById('<%= chck_Tabel2.ClientID%>');
         var cbxtabel3 = document.getElementById('<%= chck_Tabel3.ClientID%>');
         var cbxtabel4 = document.getElementById('<%= chck_Tabel4.ClientID%>');
         var cbxtabel5 = document.getElementById('<%= chck_Tabel5.ClientID%>');
         var cbxtabel6 = document.getElementById('<%= chck_Tabel6.ClientID%>');
         var cbxtabel7 = document.getElementById('<%= chck_Tabel7.ClientID%>');
         var cbxtabellain = document.getElementById('<%= chck_TabelLain.ClientID%>');
         var txttabellain = document.getElementById('<%= txt_TabelLain.ClientID%>');

         if (cbxTabellain.checked) {
             
             cbxtabel1.checked = false;
             cbxtabel2.checked = false;
             cbxtabel3.checked = false;
             cbxtabel4.checked = false;
             cbxtabel5.checked = false;
             cbxtabel6.checked = false;
             cbxtabel7.checked = false;
             cbxtabellain.checked = true;
             txttabellain.removeAttribute("disabled");
         } else {
             txttabellain.value = "";
             txttabellain.setAttribute("disabled", true);
         }
    }

    function setflagloading(item, status) {
        if (item == "cpoelab") {
            document.getElementById('<%= hf_LoadingLab.ClientID %>').value = "true";
        }
        else if (item == "cpoerad") {
            document.getElementById('<%= hf_LoadingRad.ClientID %>').value = "true";
        }
        else if (item == "inpatientProcedure") {
            document.getElementById('<%= hf_LoadingProcedure.ClientID %>').value = "true";
        }
    }


    function ProcedureOperation() {
        console.log('masuk ProcedureOperation')
        $("#MainContent_ModalRawatInap_txt_ItemProcedure").autocomplete({
            source: "../Control_Template/AutoCompleteProcedure.aspx",
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<table style="width:500px; border-bottom:1px solid lightgrey; background-color:#E7E8ED; color:Black;">'
                    + '<tr>'
                    + '<td style="width:80%; padding:5px; vertical-align:top; font-weight:bold;"> Procedure Items </td>'
                    + '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Diagnosis </td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.itemProcedureId);
                    document.getElementById('<%= hf_ItemSelectedProcedure.ClientID %>').value = ui.item.itemProcedureId;
                    document.getElementById('<%= hf_ItemSelectedProcedure_name.ClientID %>').value = ui.item.itemProcedureName;
                    document.getElementById('<%= hf_ItemSelectedProcedure_diagnosis.ClientID %>').value = ui.item.itemProcedureDiagnosis;

                    document.getElementById('<%= Btn_AjaxSearchProcedure.ClientID %>').click();
                }
            }
        })
            .focus(function () {
                $(this).autocomplete("search");
                // console.log('focus');
            })
            .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append('<table style="width:500px; border-bottom:1px solid lightgrey;">'
                        + '<tr>'
                        + '<td style="width:80%; padding:5px; vertical-align:top;">' + item.itemProcedureName + '</td>'
                        + '<td style="width:20%; padding:5px; vertical-align:top;">' + item.itemProcedureDiagnosis + '</td>'
                        + '</tr>'
                        + '</table>')
                    .appendTo(ul);
            }
    }

    function RadRawatInap() {
        $("#MainContent_ModalRawatInap_txt_ItemRad").autocomplete({
            source: "../Control_Template/AutoCompleteCPOE.aspx?cpoetype=RAD",
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<table style="width:500px; border-bottom:1px solid lightgrey; background-color:#E7E8ED; color:Black;">'
                    + '<tr>'
                    + '<td style="width:80%; padding:5px; vertical-align:top; font-weight:bold;"> Radiology Items </td>'
                    + '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Type </td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.itemId);
                    document.getElementById('<%= hf_ItemSelectedRad.ClientID %>').value = ui.item.itemId;
                    document.getElementById('<%= HF_ItemSelectedRad_name.ClientID %>').value = ui.item.itemName;
                    document.getElementById('<%= HF_ItemSelectedRad_remarks.ClientID %>').value = ui.item.itemRemarks;
                    document.getElementById('<%= Btn_AjaxSearchRad.ClientID %>').click();
                }
            }
        })
            .focus(function () {
                $(this).autocomplete("search");
                // console.log('focus');
            })
            .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append('<table style="width:500px; border-bottom:1px solid lightgrey;">'
                        + '<tr>'
                        + '<td style="width:80%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                        + '<td style="width:20%; padding:5px; vertical-align:top;">' + item.itemType + '</td>'
                        + '</tr>'
                        + '</table>')
                    .appendTo(ul);
            }
    }

    function LabRawatInap() {
        $("#MainContent_ModalRawatInap_txt_ItemLab").autocomplete({
            source: "../Control_Template/AutoCompleteCPOE.aspx?cpoetype=LAB",
            minLength: 0,
            open: function () {
                $('ul.ui-autocomplete').prepend('<li>'
                    + '<table style="width:500px; border-bottom:1px solid lightgrey; background-color:#E7E8ED; color:black;">'
                    + '<tr>'
                    + '<td style="width:80%; padding:5px; vertical-align:top; font-weight:bold;"> Laboratory Items </td>'
                    + '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Type </td>'
                    + '</tr>'
                    + '</table>'
                    + '</li>');
            },
            position: { my: "left top", at: "left bottom", collision: "flip" },
            select: function (event, ui) {
                //assign value back to the form element
                if (ui.item) {
                    $(event.target).val(ui.item.itemId);
                    document.getElementById('<%= hf_ItemSelectedLab.ClientID %>').value = ui.item.itemId;
                    document.getElementById('<%= Btn_AjaxSearchLab.ClientID %>').click();
                }
            }
        })
            .focus(function () {
                $(this).autocomplete("search");
            })
            .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append('<table style="width:500px; border-bottom:1px solid lightgrey;">'
                        + '<tr>'
                        + '<td style="width:80%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                        + '<td style="width:20%; padding:5px; vertical-align:top;">' + item.itemType + '</td>'
                        + '</tr>'
                        + '</table>')
                    .appendTo(ul);
            }
    }

    function MutExChkList(chk) {
        var chkList = chk.parentNode.parentNode.parentNode;
        var chks = chkList.getElementsByTagName("input");
        for (var i = 0; i < chks.length; i++) {
            if (chks[i] != chk && chk.checked) {
                chks[i].checked = false;
            }
        }
    }

    function MutExChkListRecovery(chk) {
        var chkList = chk.parentNode.parentNode.parentNode;
        var chks = chkList.getElementsByTagName("input");
        for (var i = 0; i < chks.length; i++) {
            if (chks[i] != chk && chk.checked) {
                chks[i].checked = false;
            }
        }
    }

    $(document).ready(function () {
        ProcedureOperation();
        RadRawatInap();
        LabRawatInap();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_beginRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    var flagLoadingProcedure = document.getElementById('<%= hf_LoadingProcedure.ClientID %>');
                    var flagLoadingRad = document.getElementById('<%= hf_LoadingRad.ClientID %>');
                    var flagLoadingLab = document.getElementById('<%= hf_LoadingLab.ClientID %>');

                    if (flagLoadingRad.value == "true") {
                        $('.loading-rad').show();
                    } else if (flagLoadingLab.value == "true") {
                        $('.loading-lab').show();
                    } else if (flagLoadingProcedure.value == "true") {
                        $('.loading-procedure').show();
                    }

                }
            });
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    ProcedureOperation();
                    RadRawatInap();
                    LabRawatInap();
                    $(".loading-procedure").hide();
                    document.getElementById('<%= hf_LoadingProcedure.ClientID %>').value = "false";
                    $(".loading-rad").hide();
                    document.getElementById('<%= hf_LoadingRad.ClientID %>').value = "false";
                    $(".loading-lab").hide();
                    document.getElementById('<%= hf_LoadingLab.ClientID %>').value = "false";
                }
            });

            // ini kenapa masuk if prm
            //copy diagnosis
            if (document.getElementById('MainContent_ModalRawatInap_textbox_diagnosis').value = null); {
                var rawatinapdiagnosis = document.getElementById('MainContent_ModalRawatInap_textbox_diagnosis');
                var from_elm = document.getElementById('MainContent_txtPrimary');
                rawatinapdiagnosis.value = from_elm.value;

            }

            if (document.getElementById('MainContent_ModalRawatInap_hfoperationScheduleId').value != null) {
                $("#divTindakanOperasi").show()
                //$("#divTindakanOperasi").css('visibility', 'visible');
                console.log('div operasi harusnya uncul');
            } else {
                $("#divTindakanOperasi").hide()
                //$("#divTindakanOperasi").css('visibility', 'hidden');
            }
        }   
    })

</script>