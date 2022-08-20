<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalReferal.ascx.cs" Inherits="Form_SOAP_Control_Template_Modal_ModalReferal" %>
<%@ Register Src="~/Form/General/Control/PatientCard.ascx" TagPrefix="uc1" TagName="PatientCard" %>


<style>
     .header-pasien{
            /*padding: 25px 8px 25px 10px;*/
            /*position:absolute;
            left:0*/
            padding-top:8px;
            padding-left:25px;
            padding-bottom:12px;
            margin-top:10px;

            background:#F2F3F4 0% 0% no-repeat padding-box;
            border-top:solid 1px #00000029;
            border-bottom:solid 1px #00000029;
     }
     .textbox{
         width:250px;
         height:32px;
         border:1px solid #76767c;
         border-radius:6px;
     }
     .input-group-addon {
            border-left: none;
            background-color: #fff;
        }
</style>


<div>
    <input type="hidden" id="hfPatientId" runat="server" />
    <asp:HiddenField ID="hfEncounterId" runat="server" />
    <%-- <uc1:PatientCard runat="server" ID="PatientCard" />--%>
    <div id="divider_reveral">
        
        
        <%--<div class="header-pasien" >
             <uc1:PatientCard runat="server" ID="PatientCard" />
        </div>--%>
        <asp:UpdateProgress ID="UpdateProgress15" runat="server" AssociatedUpdatePanelID="UpdatePanelformrujukan">
			<ProgressTemplate>
				<div class="modal-backdrop" style="background-color: black; opacity: 0.6; vertical-align: central; text-align: center">
				</div>
				<div style="margin-top: 150px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
					<img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
				</div>
			</ProgressTemplate>
		</asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanelformrujukan" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <asp:Repeater runat="server" ID="rptReferral">
            <ItemTemplate>
              <asp:HiddenField ID="referral_id" runat="server" Value='<%# Bind("referral_id") %>'  />
              <asp:HiddenField ID="referral_doctor_id" runat="server" Value='<%# Bind("referral_doctor_id") %>' />
              <asp:HiddenField ID="speciality_id" runat="server" Value='<%# Bind("speciality_id") %>' />
              <asp:HiddenField ID="is_new" runat="server" Value='<%# Bind("is_new") %>'/>
              <asp:HiddenField ID="is_delete" runat="server" Value='<%# Bind("is_delete") %>'/>
              <asp:HiddenField ID="is_editable" runat="server" Value='<%# Bind("is_editable") %>'/>
               <asp:HiddenField ID="referal_status" runat="server" Value='<%# Bind("referal_status") %>' />

             <div class="row " style="margin-top:23px;">
             <div class="col-md-3">
                  <span style="font-size:14px;font-weight:normal">Kepada TS   :</span>
                  <span style="font-size:14px;font-weight:normal">Internal</span>
             </div>
             <%--<div runat="server" class="col-md-4" style="border-right:dashed 1px #707070">
                 <div class="state p-primary-o">
                             <label>Internal</label>
                 </div> 
                 
                <div class="pretty p-default p-round">
                        <asp:RadioButton runat="server" Value="internal" ID="RB_Internal" Checked='<%# Eval("referral_target").ToString() == "Internal" ? true : false %>' Enabled='<%# Eval("is_editable").ToString() == "0" ? false : true %>' onCheckedChanged="chkRBInternal_CheckedChanged" AutoPostBack="true"/>
                        <div class="state p-primary-o">
                             <label>Internal</label>
                        </div>
                   </div>
                  <div class="pretty p-default p-round">
                       <asp:RadioButton runat="server" Value="siloam" ID="RB_Siloam" Checked='<%# Eval("referral_target").ToString() == "Siloam" ? true : false %>' Enabled="false" OnCheckedChanged="chkRBSIloam_CheckedChanged" AutoPostBack="true" />
                       <div class="state p-primary-o">
                            <label>Siloam</label>
                       </div>
                   </div>
                  <div class="pretty p-default p-round" id="">
                     <asp:RadioButton runat="server" Value="external" ID="RB_DokterExternal" Checked='<%# Eval("referral_target").ToString() == "External" ? true : false %>' Enabled="false" OnCheckedChanged="chkRBExternal_CheckedChanged"  AutoPostBack="true" />
                        <div class="state p-primary-o">
                            <label>Eksternal</label>
                         </div>
                  </div>                  
             </div>--%>
             <div class="col-md-5"  id="divjenisrujukan" runat="server" style="border-left:dashed 1px #707070;margin-left: -70px;">
                 <div class="pretty p-default p-round">
                       <asp:RadioButton runat="server" Value="1" ID="RB_Konsul1x" Checked='<%# Eval("referral_type").ToString() == "Konsultasi 1 Kali" ? true : false %>' Enabled='<%# Eval("is_editable").ToString() == "0" ? false : true %>'  OnCheckedChanged="RB_Konsul1x_CheckedChanged"  AutoPostBack="true"/>
                       <div class="state p-primary-o">
                           <label>Konsultasi 1 Kali</label>
                        </div>
                 </div>
                 <div class="pretty p-default p-round">
                    <asp:RadioButton runat="server"  Value="2" ID="RB_AlihRawat" Checked='<%# Eval("referral_type").ToString() == "Alih Rawat" ? true : false %>' Enabled='<%# Eval("is_editable").ToString() == "0" ? false : true %>' OnCheckedChanged="RB_AlihRawat_CheckedChanged"  AutoPostBack="true"/>
                    <div class="state p-primary-o">
                         <label>Alih Rawat</label>
                     </div>
                 </div>
                 <div class="pretty p-default p-round">
                    <asp:RadioButton runat="server" Value="3" ID="RB_RawatBersama" Checked='<%# Eval("referral_type").ToString() == "Rawat Bersama" ? true : false %>' Enabled='<%# Eval("is_editable").ToString() == "0" ? false : true %>'  OnCheckedChanged="RB_RawatBersama_CheckedChanged"  AutoPostBack="true"/>
                    <div class="state p-primary-o">
                        <label>Rawat Bersama</label>
                     </div>
                 </div>                   
             </div>

             <div class="col-md-4 text-right" stye=" margin-right:20px" id="btnDeleterujukan" runat="server">
                    <asp:LinkButton ID="BtnDeleteReferral" Text="Delete" Style='<%# Eval("is_editable").ToString() == "0" && Eval("is_editable").ToString() == "0" ? "display:none;" : "font-family: Helvetica; font-weight: bold; font-size: 14px; color:#E21100; text-decoration:underline;" %>' runat="server" OnClick="BtnDeleteReferral_Click" Enabled='<%# Eval("is_editable").ToString() == "0" ? false : true %>' CommandName="DeleteRow" CommandArgument='<%# Container.ItemIndex %>'/>
             </div>
             </div>

                    <div class="row " style="margin-top:10px;margin-bottom:15px" id="divDokterRujukan" runat="server">
                        <div class="col-md-2"></div>
                        <div class="col-md-4 text-left" style="margin-left: -70px;">                          
                          <asp:DropDownList Style="cursor: pointer; border-radius: 6px; border: solid 1px #76767C; margin-right:10px" ID="Ddl_DokterSpesialis" Width="260px" Height="32px" runat="server" Enabled='<%# Eval("is_editable").ToString() == "0" ? false : true %>' AutoPostBack="true" OnSelectedIndexChanged = "Ddl_DokterSpesialis_SelectedIndexChanged"  >
                          </asp:DropDownList>                                
                        </div>
                         <div class="col-md-4 text-left" style="margin-left:-25px">   
                            <%-- <asp:DropDownList  Style="cursor: pointer; border-radius: 6px; border: solid 1px #76767C; margin-right:10px" ID="DropDownList1" Width="260px" Height="32px" Enabled='<%# Eval("is_new").ToString() == "0" ||Eval("is_editable").ToString() == "1" ? false : true %>' runat="server" AutoPostBack="true" OnSelectedIndexChanged="Ddl_Dokter_SelectIndexChange" >
                                <asp:ListItem Text="Please select" Selected="false" Value="0" >Anyy Doctor ui</asp:ListItem>                                 
                          </asp:DropDownList> --%> 
                            <asp:DropDownList  Style="cursor: pointer; border-radius: 6px; border: solid 1px #76767C; margin-right:10px" ID="Ddl_DokterRujukan" Width="260px" Height="32px" Enabled='<%# Eval("speciality_id").ToString() == "0" ? false : false%>' runat="server" AutoPostBack="true" OnSelectedIndexChanged="Ddl_Dokter_SelectIndexChange" >
                                <asp:ListItem Text="Please select" Selected="false" Value="0" >Anyy Doctor ui</asp:ListItem>                                 
                          </asp:DropDownList>                              
                         </div>                               
                                               
                </div>

                    <%--rujukan external--%>
                    <div id="form_rujukan_external" runat="server" visible="false">
                        <div class="row" style="margin-top:30px;margin-bottom:24px; margin-left:0px">
                            <table style="width:100%;margin-right:170px">
                            <tr>
                                <td style="width:110px">
                                     <span style="font-size:14px;font-weight:normal">Kepada </span>
                                </td>
                                <td style="width:300px">
                                      <asp:TextBox runat="server" Style="border:solid 1px #76767C; border-radius:6px; height:32px;width:254px"  ID="TBexternal_referral_to" Text='<%# Eval("external_referral_to") %>'  ></asp:TextBox>
                                </td>
                                 <td>
                                     <span style="font-size:14px;font-weight:normal">Tempat </span>
                                </td>
                                <td>
                                      <asp:TextBox runat="server" Style="border:solid 1px #76767C; border-radius:6px; height:32px;width:254px"  ID="TBexternal_referral_place" Text='<%# Eval("external_referral_place") %>' ></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        </div>

                        <div class="row" style="margin-top:30px;margin-bottom:24px; margin-left:0px">
                            <table style="width:100%;margin-right:170px">
                            <tr>
                                <td style="width:110px">
                                     <span style="font-size:14px;font-weight:normal">Tanggal </span>
                                </td>

                                <td style="width:300px">
                                    <asp:TextBox class="form-control" placeholder="-" runat="server" ID="TBexternal_referral_date" Text='<%# Eval("external_referral_date") %>' onmousedown="dateSelect(this.id);" CssClass="isCalendar" Style="height: 32px; width:254px; padding-left:10px; border-radius:5px; font-size: 14px;  background-color: #ffffff66; border:solid 1px #76767C; display:inline-block; text-align:left; cursor:pointer;"   />

                                    <%--<div class="input-group date" data-provide="datepicker" style="border:solid 1px #76767C; border-radius:6px; height:32px;width:254px">
                                      <asp:TextBox runat="server" ID="TextBox6" ></asp:TextBox>
                                      <div class="input-group-addon">
                                            <span><i class="fa fa-calendar-o"></i></span>
                                        </div>
                                    </div>--%>
                                </td>
                                 <td style="width:75px">
                                     <span style="font-size:14px;font-weight:normal">Waktu </span>
                                </td>
                                <td >
                                    <div style="border:solid 1px #76767C; border-radius:6px; height:32px;width:254px">
                                      <asp:TextBox runat="server" style=" border-bottom:solid 1px #76767C;border-right:none;border-radius:6px; height:31px;width:220px;position:absolute" Text='<%# Eval("external_referral_time").ToString() %>' CssClass="isClock"  ID="TBexternal_referral_time" ></asp:TextBox>
                                      <span ><i class="fa fa-clock-o" style="color:#76767C;font-size:20px;position:absolute;margin-left:225px;margin-top:5px"></i></span>
                                
                                    </div>
                                 </td>
                            </tr>
                        </table>
                        </div>

                        <div class="row" style="margin-top:30px;margin-bottom:24px; margin-left:0px">
                            <table style="width:100%;margin-right:17px">
                            <tr>
                                <td style="width:110px">
                                     <span style="font-size:14px;font-weight:normal">Alasan dirujuk</span>
                                </td>
                                <td>
                                 <%--<asp:DropDownList Selected='<%# Eval("external_referral_reason").ToString() %>' Style="cursor: pointer; border-radius: 6px; border: solid 1px #76767C; margin-right:10px" ID="Ddl_external_referral_reason" Width="260px" Height="32px"  runat="server" >
                                    <asp:ListItem Text="Please select"  >Tempat Penuh</asp:ListItem>                                 

                                    </asp:DropDownList>  --%>
                                      <asp:TextBox runat="server"  Style="border:solid 1px #76767C; border-radius:6px; height:32px;width:254px"  Text='<%# Eval("external_referral_reason").ToString() %>' ID="TBexternal_referral_reason" ></asp:TextBox>
                                </td>
                     
                            </tr>
                        </table>
                        </div>
                    </div>
        
                    <%--end external rujukan--%>
                    <div class="row " style="margin-top:20px;padding-left:15px;padding-right:15px;">
                         <div>
                               <h4 style="font-size:11px;color:#76767C;font-weight:normal;margin-bottom:5px">REMARKS<sup style="color: red; font-weight:900;">*</sup></h4>
                         </div>
                         <div>
                               <asp:textbox id="TBreferral_remark" OnClientClick="return CheckField();" runat="server" Rows="1" placeholder="Type any remarks here..." Enabled='<%# Eval("is_editable").ToString().ToLower() == "1" ? true : false %>' Text='<%# Bind("referral_remark") %>'  TextMode="MultiLine"  CssClass="scrollEMR" ForeColor='<%# Eval("is_editable").ToString() == "1" ? System.Drawing.Color.Black : System.Drawing.Color.Gray %>' Style="resize:none; max-width: 100%;border-radius:6px;"  Width="100%" Height="80px"   />
                                <p style="text-align: right; color: red;display:none" id="pError" runat="server">Test</p>
                         </div>

                    </div>
                                                           
                    <div class="divider" id="divadddokter" runat="server" style="margin-top:26px;width:100%;height:8px;background-color:#E6E6E6;left:0px">   
                    </div>
            </ItemTemplate>
         </asp:Repeater>
        <div id="divBtnAddDokter" runat="server" style="border-radius:6px; color: white; background-color: #1172F7; height: auto; padding-top: 3px;padding-left:6px; margin:0 auto; width:134px;height:32px;position:relative;margin-top:-20px" >           
                <i class="fa fa-plus-circle"></i>
                <asp:Button ID="BtnAddDokter" runat="server" CssClass="btn btn-default btn-sm" style="background-color:transparent;color:white;border:none"  OnClick="BtnAddDokter_Click" Text="Tambah Dokter"  />            
		</div>
    </ContentTemplate>
</asp:UpdatePanel>

</div>
        
        
</div>


<script>


    function dateSelect(ids) {
        var dp = $('#' + ids);
        dp.datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd M yyyy",
            language: "id",
            todayHighlight: true
        }).on('changeDate', function (ev) {
            $(this).blur();
            $(this).datepicker('hide');
        });
    }

  




</script>
