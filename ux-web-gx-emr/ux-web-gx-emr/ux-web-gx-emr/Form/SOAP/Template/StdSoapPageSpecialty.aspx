<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="StdSoapPageSpecialty.aspx.cs" Inherits="Form_SOAP_Template_StdSoapPageSpecialty" %>

<%@ Register Src="~/Form/General/Control/PatientCard.ascx" TagPrefix="uc1" TagName="PatientCard" %>
<%@ Register Src="~/Form/General/Control/PatientCardModal.ascx" TagPrefix="uc1" TagName="PatientCardModal" %>
<%@ Register Src="~/Form/SOAP/PreviewTemplate/SoapPagePreview.ascx" TagPrefix="uc1" TagName="Preview" %>
<%@ Register Src="~/Form/SOAP/Control_Template/StdPlanning.ascx" TagName="StdPlanning" TagPrefix="uc1" %>
<%@ Register Src="~/Form/SOAP/Control_Template/StdImunisasi.ascx" TagName="StdImunisasi" TagPrefix="uc1" %>
<%@ Register Src="~/Form/SOAP/PreviewTemplate/CompareSOAP.ascx" TagPrefix="uc1" TagName="CompareSOAP" %>

<%@ Register Src="~/Form/SOAP/Control_Template/Specialty/StdTriage.ascx" TagName="StdTriage" TagPrefix="uc1" %>
<%@ Register Src="~/Form/SOAP/Control_Template/Specialty/StdObgyn.ascx" TagName="StdObgyn" TagPrefix="uc1" %>
<%@ Register Src="~/Form/SOAP/Control_Template/Specialty/StdPediatric.ascx" TagName="StdPediatric" TagPrefix="uc1" %>

<%@ Register Src="~/Form/SOAP/Control_Template/StdKurvaPertumbuhan.ascx" TagName="StdKurvaPertumbuhan" TagPrefix="uc1" %>
<%@ Register Src="~/Form/SOAP/Control_Template/Modal/ModalReferal.ascx" TagPrefix="uc1" TagName="ModalReferal" %>
<%@ Register Src="~/Form/SOAP/Control_Template/Modal/ModalRawatInap.ascx" TagPrefix="uc1" TagName="ModalRawatInap" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<style>
		body, html {
			width: 100%;
			height: 100%;
		}

		.item {
			width: 300px;
			margin-left: auto;
			padding: 7px;
			display: block;
			font-size: 18px;
			color: white;
			background: #2c3794;
			border-radius: 25px 0 0 25px;
			-webkit-transition: width 2s; /* Safari */
			transition: width 0.2s, background 0.2s, color 0.2s;
		}

			.item:hover {
				width: 440px;
				-webkit-transition: width 2s; /* Safari */
				transition: width 0.2s;
				background: #2c3794;
				color: white;
			}

			.item > span {
				margin-right: 20px;
				margin-left: 5px;
			}

		.itemcontainer > div {
			padding-top: 0px;
			padding-bottom: 2px;
		}

		.itemsave {
			width: 210px;
			height: 43px;
			margin-left: auto;
			padding: 10px;
			display: block;
			font-size: 14px;
			color: black;
			background: #4d9b35;
			border-radius: 25px 0 0 25px;
		}

			.itemsave:hover {
				width: 350px;
				height: 43px;
				-webkit-transition: width 2s; /* Safari */
				transition: width 0.2s;
				background: #4d9b35;
				color: white;
			}

			.itemsave > span {
				margin-right: 20px;
				margin-left: 5px;
			}

		.itempreview {
			width: 210px;
			height: 43px;
			margin-left: auto;
			padding: 10px;
			display: block;
			font-size: 14px;
			color: white;
			background: #8ba303;
			border-radius: 25px 0 0 25px;
			-webkit-transition: width 2s; /* Safari */
			transition: width 0.2s;
		}

			.itempreview:hover {
				width: 350px;
				height: 43px;
				-webkit-transition: width 2s; /* Safari */
				transition: width 0.2s;
				background: #8ba303;
				color: white;
			}

			.itempreview > span {
				margin-right: 20px;
				margin-left: 5px;
			}

		.itemappt {
			width: 210px;
			height: 43px;
			margin-left: auto;
			padding: 10px;
			display: block;
			font-size: 14px;
			color: black;
			background: #f88805;
			border-radius: 25px 0 0 25px;
		}

			.itemappt:hover {
				width: 370px;
				height: 43px;
				-webkit-transition: width 2s; /* Safari */
				transition: width 0.2s;
				background: #f88805;
				color: white;
			}

			.itemappt > span {
				margin-right: 20px;
				margin-left: 5px;
			}

		.itemsign {
			width: 210px;
			height: 43px;
			margin-left: auto;
			padding: 10px;
			display: block;
			font-size: 14px;
			color: white;
			background: #4081ed;
			border-radius: 25px 0 0 25px;
			-webkit-transition: width 2s; /* Safari */
			transition: width 0.2s;
		}

			.itemsign:hover {
				width: 350px;
				height: 43px;
				-webkit-transition: width 2s; /* Safari */
				transition: width 0.2s;
				background: #4081ed;
				color: white;
			}

			.itemsign > span {
				margin-right: 20px;
				margin-left: 5px;
			}

		.itemcontainersave > div {
			padding-top: 0px;
			padding-bottom: 2px;
		}

		.headerpanel {
			background-color: white;
			color: #000000;
			font-family: Helvetica, Arial, sans-serif;
			font-weight: bold;
			font-size: 14px;
			border-top: 0px;
			border-left: 0px;
			border-right: 0px;
		}

		.itemlab {
			font-size: 12px;
			color: #171717;
			font-weight: bold;
			padding-top: 0px;
			margin-bottom: 0px;
		}

		.mycheckbox input[type="checkbox"] {
			margin-right: 0%;
		}

		.mycheckboxFA input[type="checkbox"] {
			margin-right: 10px;
		}

		.stylink {
			font: 12px;
			color: #171717;
		}

			.stylink:hover {
				font: 12px;
				color: #4d9b35;
			}

		.square {
			width: 48px;
			height: 46px;
			border-radius: 6px;
			background-color: #f4f4f4;
		}

       .paddingTKA {
            height: 60px;
            padding-top: 10px;
            padding-bottom: 10px;
            border-right: 1px solid #efefef;
            border-top: 1px solid #efefef;
            border-bottom: 1px solid #efefef;
        }
        .btndelrujukan {
			border: none;
			font-size: 13px;
			font-weight: bold;
			font-family: Helvetica, Arial, sans-serif;
			width: 120px;
			height: 32px;
			border-radius: 4px;
			background: #E84118;
			color:#FFFFFF;
			padding: 8px 31px 9px 31px;
		}
		.btndelrujukan:hover {
			border: none;
			font-size: 13px;
			font-weight: bold;
			font-family: Helvetica, Arial, sans-serif;
			width: 120px;
			height: 32px;
			border-radius: 4px;
			background: #E21100;
			color:#FFFFFF;
			padding: 8px 31px 9px 31px;
		}

		.btncancelrujukan {
			font-size: 13px;
			font-weight: bold;
			font-family: Helvetica, Arial, sans-serif;
			width: 120px;
			height: 32px;
			border-radius: 4px;
			background: #FFFFFF;
			border: 0.5px solid #E84118;
			color: #E84118;
			padding: 8px 31px 9px 31px;
		}

		.disabled-form {
			pointer-events: none;
			opacity: 0.5;
			cursor: not-allowed;
		}
		.lbl-new{
			background: #1275FF; 
			border-radius: 10px; 
			height: 17px; 
			padding-top: 1px; 
			padding-bottom: 1px; 
			padding-left: 13px; 
			padding-right: 13px; 
			text-align: center; 
			width: 95px; 
			display: inline-block;
		}
		.lbl-appointment{
			background: #1D6B05; 
			border-radius: 10px; 
			height: 17px; 
			padding-top: 1px; 
			padding-bottom: 1px; 
			padding-left: 13px; 
			padding-right: 13px; 
			text-align: center; 
			width: 95px; 
			display: inline-block;
		}
		.lbl-cancel{
			background: #E84118; 
			border-radius: 10px; 
			height: 17px; 
			padding-top: 1px; 
			padding-bottom: 1px; 
			padding-left: 13px; 
			padding-right: 13px; 
			text-align: center; 
			width: 95px; 
			display: inline-block;
		}
		.lbl-chekin{
			background: #909194; 
			border-radius: 10px; 
			height: 17px; 
			padding-top: 1px; 
			padding-bottom: 1px; 
			padding-left: 13px; 
			padding-right: 13px; 
			text-align: center; 
			width: 95px; 
			display: inline-block;
		}

		.linkbtn-hapusreferal{
			font-family: Helvetica; 
			font-weight: bold; 
			font-size: 12px; 
			color: #E84118; 
			text-decoration: underline; 
			margin-left: 20px
		}
		.linkbtn-hapusreferal-disable{
			font-family: Helvetica; 
			font-weight: bold; 
			font-size: 12px; 
			color: #B9B9B9; 
			text-decoration: underline; 
			margin-left: 20px
		}
		.txtenable{
			color:#171717;
		}
		.txtdisable{
			color:#B9B9B9;
		}
    </style>

	<body onkeyup="closeTheLoading();">

		<div id="divpage" runat="server">

			<div id="divBlokPage" runat="server" visible="false">
				<div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0; text-align: center; cursor: not-allowed;"></div>
			</div>

			<%--<div id="divalarm" style="display: none;">
                <div class="modal-backdrop justblink" style="background-color: #f89406; opacity: 0.5; text-align: center;"></div>
            </div>--%>

			<div id="loading_HI" style="display: none;">
				<div class="modal-backdrop" style="background-color: black; opacity: 0.6; vertical-align: central; text-align: center; z-index: 2000;">
				</div>
				<div style="margin-top: 150px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
					<img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
				</div>
			</div>

			<div id="loading_FinalSubmit" style="display: none;">
				<div class="modal-backdrop" style="background-color: black; opacity: 0.6; vertical-align: central; text-align: center; z-index: 2000;">
				</div>
				<div style="margin-top: 150px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
					<img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
				</div>
			</div>

			<asp:UpdateProgress ID="UpdateProgress15" runat="server" AssociatedUpdatePanelID="UpdatePanelFinalSave">
				<ProgressTemplate>
					<div class="modal-backdrop" style="background-color: black; opacity: 0.6; vertical-align: central; text-align: center">
					</div>
					<div style="margin-top: 150px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
						<img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
					</div>
				</ProgressTemplate>
			</asp:UpdateProgress>

			<div id="progresslabrad" class="loadlabrad" style="display: none;">
				<div class="loadPageBG">
				</div>
				<div style="margin-top: 225px; margin-left: -100px; text-align: center; position: fixed; z-index: 9999; left: 50%;">
					<img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
				</div>
			</div>

			<asp:UpdateProgress ID="UpdateProgressShowKurva" runat="server" AssociatedUpdatePanelID="UpdatePanelBtnShowKurva">
				<ProgressTemplate>
					<div class="modal-backdrop" style="background-color: black; opacity: 0.6; vertical-align: central; text-align: center; z-index: 1060;">
					</div>
					<div style="margin-top: 200px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
						<img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
					</div>
				</ProgressTemplate>
			</asp:UpdateProgress>

			<asp:HiddenField ID="HFisBahasaSOAP" runat="server" />
			<asp:HiddenField ID="HFflagfinalsubmitloading" runat="server" Value="false" />

			<%-- ========================================================== PATIENT CARD & PAGE SPECIALITY ================================================================ --%>
			<div class="container-fluid kartu-pasien" style="box-shadow: 0 0 0 0 transparent;">
				<input type="hidden" id="hfPatientId" runat="server" />
				<asp:HiddenField ID="hfEncounterId" runat="server" />
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
				<asp:HiddenField ID="hfgender" runat="server" />
				<asp:HiddenField ID="hfage" runat="server" />
				<asp:HiddenField ID="hfsavemodeHI" runat="server" />
				<asp:HiddenField ID="hfAdmissionNo" runat="server" />
				<asp:HiddenField ID="hfMRNo" runat="server" />

				<asp:HiddenField ID="hfsoapstringsavetolocal" runat="server" />
				<asp:HiddenField ID="hfsoapstringgetfromlocal" runat="server" />

				<asp:HiddenField ID="hf_flagrujukan_aido" runat="server" />

				<uc1:PatientCard runat="server" ID="PatientCard" />
			</div>

			<div class="container-fluid bawah-kartu-pasien">
				<div class="row" style="padding-left: 15px; padding-top: 8px">

					<%--<table border="0" style="display:inline;">
                        <tr>
                            <td>--%>
					<label style="font-family: Helvetica; font-size: 12px;">Template</label>
					<asp:DropDownList Style="cursor: pointer; border-radius: 2px; border: solid 1px #efefef;" ID="ddlForm_Type" Width="280px" Height="25px" runat="server">
					</asp:DropDownList>
					<asp:Button runat="server" ID="btnChooseTemplate" CssClass="btn btn-lightGreen" Text="Choose" Style="width: 71px; height: 25px; padding-top: 3px; border-radius: 4px; background-color: #4d9b35; font-family: Helvetica; font-size: 12px; color: #ffffff" OnClick="btnChoose_onClick" />
					<asp:Button runat="server" CssClass="hidden" ID="btncopysoap" />
					<a href="javascript:PreviewCopy();" id="iconcopy" style="padding-left: 20px; display: none"><span>
						<img src="<%= Page.ResolveClientUrl("~/Images/PatientHistory/ic_copySOAP.svg") %>" style="padding-right: 5px" /></span><strong>Copy SOAP</strong></a>
					<a href="javascript:PreviewCopyPrescription();" id="iconcopydrugs" style="padding-left: 20px; display: none"><span>
						<img src="<%= Page.ResolveClientUrl("~/Images/PatientHistory/ic_copyHope.svg") %>" style="padding-right: 5px" /></span><strong>Copy Drugs HOPE</strong></a>

					<%--<asp:UpdatePanel runat="server" ID="upX" style="display: none">
                        <ContentTemplate>
                            <asp:Button ID="btnAutoSave" runat="server" Text="Test Autosave" OnClick="btnAutoSave_onClick" Style="height: 5px; display: none" />
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
					<%-- </td>
                            <td>
                                <asp:UpdateProgress ID="UpdateProgressProcessing" runat="server" AssociatedUpdatePanelID="UpdatePanelFinalSave">
                                <ProgressTemplate>
                                    <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                    </div>
                                    <div class="btn-sm btn-primary" style="display:inline; margin-left:20px; background-color:#4081ed;"> <lable style="font-weight:bold;">PROCESSING</lable> </div>  &nbsp; 
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                        
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>--%>
					<label style="float: right; padding-right: 45px; padding-top: 5px;">
						<label id="lblbhs_lastmodifsoap">Last modified </label>
						:
                    <asp:Label ID="LabelModifDateSoap" runat="server" Text="-"></asp:Label>
					</label>
				</div>
			</div>
			<div id="tombolPfloat" class="floatbuttonlabrad-hide">
				<div style="background-color: #e7e8ed; box-shadow: 0px 0px 2px 1px rgba(0, 0, 0, 0.25); padding-top: 8px; padding-left: 8px; padding-right: 8px; border-radius: 0px 0px 5px 5px; margin-right: 16%;">
					<a class="btn btn-default btn-sm" style="height: 25px; min-width: 15%; padding-top: 3px; margin-bottom: 10px;" href="#planning_begin">P</a> &nbsp;
                    <a class="btn btn-default btn-sm" style="height: 25px; min-width: 38%; padding-top: 3px; margin-bottom: 10px;" href="#labrad_begin">Lab/Rad</a>
					<a class="btn btn-default btn-sm" style="height: 25px; min-width: 38%; padding-top: 3px; margin-bottom: 10px;" href="#diagproc_begin">Diag/Proc</a>
					<a class="btn btn-default btn-sm" style="height: 25px; min-width: 38%; padding-top: 3px; margin-bottom: 10px;" href="#prescription_begin">Prescription</a>
				</div>
			</div>

			<asp:UpdatePanel ID="UpdatePanelKeepAlive" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<!-- keep alive button -->
					<asp:Button ID="KeepAliveButton" runat="server" Text="Button" CssClass="hidden" OnClick="KeepAliveButton_Click" />
					<asp:HiddenField ID="flagIsPostback" runat="server" />
					<!-- end keep alive button -->
				</ContentTemplate>
			</asp:UpdatePanel>

			<%-- ========================================================== PATIENT CARD & PAGE SPECIALITY================================================================ --%>
			<section id="subjective" style="padding-top: 5px; padding-bottom: 10px;" runat="server">
				<div class="container-fluid">
					<div class="row">
						<div class="col-lg-12 mx-auto" style="height: 100%; width: 98%;">
							<br />
							<div class="mini-dialog" style="margin-top: -10px; margin-bottom: 10px;" runat="server" id="div2">
								<asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<div class="row mini-header" style="margin-left: 0px; margin-right: 0px;">
											<div class="col-sm-12" style="padding-left: 0px;">
												<img src="<%= Page.ResolveClientUrl("~/Images/Dashboard/ic_Reminder.svg") %>" style="height: 20px; width: 20px;" />
												<label id="lblbhs_reminder" style="font-size: 15px; color: #c43d32; vertical-align: middle;">Reminder</label>
											</div>
										</div>
										<div class="container-fluid">
											<div class="row">
												<div class="col-sm-8" style="display: inline-flex;">
													<asp:TextBox ID="TxtReminderNotes" placeholder="Type reminder here..." runat="server" Style="width: 76%; margin-right: 5px;" onkeydown="return txtOnKeyPressReminder();"></asp:TextBox>
													<table>
														<tr>
															<td>
																<asp:Button ID="BtnAddReminder" CssClass="btn btn-primary" runat="server" Text="Add" Style="width: 50px; height: 24px; padding-top: 3px; border-radius: 4px; background-color: #2a3593; color: #ffffff" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" OnClientClick="return checkreminder();" OnClick="BtnAddReminder_Click" />
															</td>
															<td>
																<asp:UpdateProgress ID="UpdateProgress13" runat="server" AssociatedUpdatePanelID="UpdatePanel20">
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
												<div class="col-sm-4 text-right">
													<div class="pretty p-icon p-curve">
														<asp:CheckBox runat="server" ID="chkhideReminder" TabIndex="-1" OnCheckedChanged="chkhideReminder_CheckedChanged" AutoPostBack="true" />
														<div class="state p-success">
															<i class="icon fa fa-check"></i>
															<label id="lblbhs_hideotherdoctor" style="font-size: 12px;">Hide Others Doctor's Reminder </label>
														</div>
													</div>
												</div>
											</div>
											<div class="row" runat="server" id="divtablereminder">
												<div class="col-sm-12">
													<asp:HiddenField runat="server" ID="hdnremindernotes" />
													<div style="border: 1px solid lightgray; max-height: 125px; overflow-y: auto; margin-top: 5px;" class="scrollEMR">
														<asp:GridView ID="gvw_remindernotes" runat="server" AutoGenerateColumns="False" CssClass="table-kecil"
															DataKeyNames="special_notification_id" EmptyDataText="No Data" BorderWidth="0" HeaderStyle-BorderWidth="0">
															<PagerStyle CssClass="pagination-ys" />
															<Columns>
																<asp:TemplateField HeaderText="Reminder" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="50%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:HiddenField ID="special_notification_id" runat="server" Value='<%# Bind("special_notification_id") %>'></asp:HiddenField>
																		<asp:HiddenField ID="Hf_ismyself" runat="server" Value='<%# Bind("is_myself") %>'></asp:HiddenField>
																		<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="lbl_notification" runat="server" Text='<%# Bind("notification") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Doctor" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="30%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="lbl_doctor_name" runat="server" Text='<%# Bind("doctor_name") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Show on Dashboard" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="12%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" ItemStyle-HorizontalAlign="Center">
																	<ItemTemplate>
																		<div class="pretty p-icon p-curve">
																			<asp:CheckBox runat="server" ID="chkReminder" TabIndex="-1" Checked='<%# Eval("is_checked") %>' Enabled='<%# Eval("is_myself").ToString() == "0" ? false : true %>' />
																			<div class="state p-success">
																				<i class="icon fa fa-check"></i>
																				<label style="font-size: 12px;"></label>
																			</div>
																		</div>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField ItemStyle-Width="8%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" ItemStyle-HorizontalAlign="Center">
																	<ItemTemplate>
																		<asp:ImageButton ID="btndelete" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btnDeleteReminder_onClick" Enabled='<%# Eval("is_myself").ToString() == "0" ? false : true %>' class='<%# Eval("is_myself").ToString() == "0" ? "disableElement" : "" %>' Style="width: 12px; height: 12px; margin-top: 3px;" />
																	</ItemTemplate>
																</asp:TemplateField>
															</Columns>
														</asp:GridView>
													</div>
												</div>
											</div>
										</div>
									</ContentTemplate>
								</asp:UpdatePanel>
							</div>

							<div id="dvEmergency">
								<div class="row">
									<uc1:StdTriage runat="server" ID="StdTriage" Visible="false" />
								</div>
							</div>

							<div class="mini-dialog" runat="server" id="divTopSOAP">
								<div id="divblokSOAP" runat="server" visible="false">
									<div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0.3; text-align: center; cursor: not-allowed;"></div>
								</div>
								<div class="row mini-header" style="border-bottom: solid 1px #efefef; margin-left: 0px; margin-right: 0px;">
									<div class="col-sm-6" style="padding-left: 0px;">
										<a title="Compare" href="javascript:$('#modalCompareSOAP').modal();">SOAP SPECIALTY</a>
									</div>
									<div class="col-sm-6 text-right" style="padding-right: 0px;"><a href="javascript:resetSOAP();" style="margin-right: 10px; cursor: pointer; color: #c43d32; text-decoration: underline;">Reset SOAP </a></div>
								</div>
								<div class="row no-margin">
									<div class="col-sm-3 vertical-right" style="padding: 5px 0px 5px 15px; margin-right: -1px;">
										<asp:UpdatePanel runat="server" ID="UP_S" UpdateMode="Conditional">
											<ContentTemplate>
												<label style="font-size: 18px; font-weight: bold">
													S<asp:Label ID="LabelmandatoryS" runat="server" Style="color: red; font-weight: bold;" Text="*" Visible="false" title="mandatory"></asp:Label>
												</label>
												&nbsp;
                                                <a class="btn btn-default btn-sm" title="Template" style="height: 25px; padding-top: 3px; margin-bottom: 10px;" href="javascript:$('#modalTemplateS').modal();">
													<asp:Image ID="ImageTS" ImageUrl="~/Images/Icon/ic_Template.svg" class="img-template-btn" runat="server" /></a>
												<br />
												<div class="row" style="margin-top: -5px;">
													<div class="col-sm-12" style="padding-top: 3px; height: 25px;">
														<label id="lblbhs_chiefcomplaint" style="font-size: 12px; font-weight: bold;">Chief Complaint:</label>
													</div>
												</div>
												<div class="scrollEMR" style="max-height: 335px; overflow-y: auto" id="dvchief" runat="server">
													<asp:TextBox runat="server" CssClass="text-multiline-dialog" Style="overflow-y: hidden;" placeholder="Type here..." BorderColor="transparent" ID="Complaint" TextMode="MultiLine" Rows="1" onkeydown="AutoExpand(this)" onkeypress="return CheckBack();" onfocus="AutoExpand(this)" />
												</div>
											</ContentTemplate>
										</asp:UpdatePanel>
									</div>
									<div class="col-sm-6 vertical-left" style="padding-top: 5px; padding-bottom: 5px;">
										<asp:UpdatePanel runat="server" ID="UP_SAnam" UpdateMode="Conditional">
											<ContentTemplate>
												<a class="btn btn-default btn-sm" title="Template" style="height: 25px; padding-top: 3px; margin-bottom: 10px;" href="javascript:$('#modalTemplateSAnam').modal();">
													<asp:Image ID="ImageTAnam" ImageUrl="~/Images/Icon/ic_Template.svg" class="img-template-btn" runat="server" /></a>
												<label style="font-size: 18px; font-weight: bold;">&nbsp; </label>
												<br />
												<div class="row" style="margin-top: -5px;">
													<div class="col-sm-4" style="padding-top: 3px; height: 25px;">
														<label style="font-size: 12px; font-weight: bold;">Anamnesis:</label>
													</div>
													<div class="col-sm-8" style="text-align: right; height: 25px; padding-right: 15px;">
														&nbsp;
													</div>
												</div>
												<div class="scrollEMR" style="max-height: 335px; overflow-y: auto">
													<asp:TextBox runat="server" CssClass="text-multiline-dialog" Style="overflow-y: hidden;" placeholder="Type here..." BorderColor="transparent" ID="Anamnesis" TextMode="MultiLine" Rows="1" onkeydown="AutoExpand(this)" onkeypress="return CheckBack();" onfocus="AutoExpand(this)" />
												</div>
											</ContentTemplate>
										</asp:UpdatePanel>
									</div>
									<div id="divpregnat" runat="server" class="col-sm-3 vertical-left" style="padding-top: 5px; padding-bottom: 5px; border-bottom: 1px solid #efefef; border-radius: 0px 0px 0px 10px;">
										<asp:UpdatePanel runat="server" ID="UP_SPregnant" UpdateMode="Conditional">
											<ContentTemplate>
												<div style="display: none;">
													<div class="pretty p-icon p-curve">
														<asp:CheckBox runat="server" ID="chkpregnant" TabIndex="-1" />
														<div class="state p-success">
															<i class="icon fa fa-check"></i>
															<label id="lblbhs_ispregnantxxx" style="font-size: 12px;">Is Pregnant </label>
														</div>
													</div>
													<div class="pretty p-icon p-curve">
														<asp:CheckBox runat="server" ID="chkbreastfeed" TabIndex="-1" />
														<div class="state p-success">
															<i class="icon fa fa-check"></i>
															<label id="lblbhs_breastfeedingxxx" style="font-size: 12px;">Breast Feeding </label>
														</div>
													</div>
												</div>

												<table style="width: 100%;" class="table-condensed">
													<tr>
														<td>
															<label id="lblbhs_ispregnant" style="font-weight: bold;">Hamil<label style="color: red;">*</label>
															</label>
														</td>
														<td>
															<div class="pretty p-default p-round">
																<asp:RadioButton runat="server" GroupName="radiohamil" Value="0" ID="Radiohamilno" />
																<div class="state p-primary-o">
																	<label>No</label>
																</div>
															</div>
															<div class="pretty p-default p-round">
																<asp:RadioButton runat="server" GroupName="radiohamil" Value="1" ID="Radiohamilyes" />
																<div class="state p-primary-o">
																	<label>Yes</label>
																</div>
															</div>
														</td>
													</tr>
													<tr>
														<td>
															<label id="lblbhs_breastfeeding" style="font-weight: bold;">Menyusui </label>
														</td>
														<td>
															<div class="pretty p-default p-round">
																<asp:RadioButton runat="server" GroupName="radiosusu" Value="0" ID="Radiosusuno" />
																<div class="state p-primary-o">
																	<label>No</label>
																</div>
															</div>
															<div class="pretty p-default p-round">
																<asp:RadioButton runat="server" GroupName="radiosusu" Value="1" ID="Radiosusuyes" />
																<div class="state p-primary-o">
																	<label>Yes</label>
																</div>
															</div>
														</td>
													</tr>
												</table>

											</ContentTemplate>
										</asp:UpdatePanel>
									</div>
								</div>

								<div class="box-border-soap" style="padding-bottom: 0px">
									<div style="margin-left: 15px; margin-right: 0px;">

										<asp:UpdatePanel runat="server" ID="UP_O" UpdateMode="Conditional">
											<ContentTemplate>
												<%-- <div class="row">
                                            <div class="col-sm-12">
                                                <label id="lblbhs_templateo" style="font-size: 18px; font-weight: bold">Objective Template:</label> 
                                                <button class="btn btn-default btn-sm" style="height: 25px; padding-top: 3px;" onclick="$('#modalTemplateO').modal(); clearnormal();">Add Data</button>
                                            </div>
                                        </div>--%>

												<div class="row">
													<div class="col-sm-12">
														<label style="font-size: 18px; font-weight: bold;">O<asp:Label ID="LabelmandatoryO" runat="server" Style="color: red; font-weight: bold;" Text="*" Visible="false" title="mandatory"></asp:Label></label>
														&nbsp;
                                                <a class="btn btn-default btn-sm" title="Template" style="height: 25px; padding-top: 3px; margin-bottom: 10px;" href="javascript:$('#modalTemplateO').modal(); clearnormal();">
													<asp:Image ID="ImageTO" ImageUrl="~/Images/Icon/ic_Template.svg" class="img-template-btn" runat="server" /></a>
														<asp:DropDownList ID="ddlO" runat="server" Style="width: 135px; vertical-align: text-bottom;" onchange="copyddlTemplatetoSOAP();"></asp:DropDownList>
														<br />
														<div class="scrollEMR" style="max-height: 335px; overflow-y: auto">
															<asp:TextBox runat="server" CssClass="text-multiline-dialog" Style="overflow-y: hidden;" placeholder="Type here..." BorderColor="transparent" ID="txtOthers" TextMode="MultiLine" onkeydown="AutoExpand(this)" onkeypress="return CheckBack();" onfocus="AutoExpand(this)" />
														</div>
													</div>
												</div>

											</ContentTemplate>
										</asp:UpdatePanel>
									</div>
								</div>

								<%-- ===================================================================  VITAL SIGN ====================================================== --%>
								<div class="box-border-soap">
									<div class="row" style="margin-left: 0px; margin-right: 0px;">

										<div class="col-sm-2" style="width: 12%; padding-top: 9px;">
											<label id="lblbhs_vitalsign" style="font-size: 16px; font-weight: bold;">Vital Sign:</label>
										</div>
										<div class="col-sm-2" style="width: 150px; padding-right: 0%; padding-left: 0px;">
											<label id="lblbhs_bloodpressure" style="font-size: 11px;">Blood Pressure</label>
											<br />
											<asp:TextBox runat="server" CssClass="input-angka" ID="txtbloodhigh" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
											<label>/</label>
											<asp:TextBox runat="server" CssClass="input-angka" ID="txtbloodlow" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
											<label class="format-satuan">mmHg</label>
										</div>
										<div class="col-sm-2" style="width: 100px; padding-right: 0%; padding-left: 0px;">
											<label id="lblbhs_pulserate" style="font-size: 11px;">Pulse Rate</label>
											<br />
											<asp:TextBox runat="server" CssClass="input-angka" ID="txtpulserate" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
											<label class="format-satuan">X/mnt</label>
										</div>
										<div class="col-sm-2" style="width: 100px; padding-right: 0%; padding-left: 0px;">
											<label id="lblbhs_respiratoryrate" style="font-size: 11px;">Respiratory rate</label>
											<br />
											<asp:TextBox runat="server" CssClass="input-angka" ID="txtrespiratory" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
											<label class="format-satuan">X/mnt</label>
										</div>
										<div class="col-sm-2" style="width: 100px; padding-right: 0%; padding-left: 0px;">
											<label style="font-size: 11px;">SpO2</label>
											<br />
											<asp:TextBox runat="server" CssClass="input-angka" ID="txtspo" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
											<label class="format-satuan">%</label>
										</div>
										<div class="col-sm-2" style="width: 100px; padding-right: 0%; padding-left: 0px;">
											<label id="lblbhs_temperature" style="font-size: 11px;">Temperature</label>
											<br />
											<asp:TextBox runat="server" CssClass="input-angka" ID="txttemperature" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
											<label class="format-satuan">&#8451</label>
										</div>
										<div class="col-sm-2" style="width: 100px; padding-right: 0%; padding-left: 0px;">
											<label id="lblbhs_weight" style="font-size: 11px;">Weight</label>
											<br />
											<asp:TextBox runat="server" CssClass="input-angka" ID="txtweight" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();" onkeyup="hitungBMI();"></asp:TextBox>
											<label class="format-satuan">kg</label>
										</div>
										<div class="col-sm-2" style="width: 100px; padding-right: 0%; padding-left: 0px;">
											<label id="lblbhs_height" style="font-size: 11px;">Height</label>
											<br />
											<asp:TextBox runat="server" CssClass="input-angka" ID="txtheight" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();" onkeyup="hitungBMI();"></asp:TextBox>
											<label class="format-satuan">cm</label>
										</div>
										<div class="col-sm-2" style="width: 120px; padding-right: 0%; padding-left: 0px;">
											<label id="lblbhs_lingkarkepala" style="font-size: 11px;">Head Circumreference</label>
											<br />
											<asp:TextBox runat="server" CssClass="input-angka" ID="txtlingkarkepala" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
											<label class="format-satuan">cm</label>
										</div>
										<div class="col-sm-2" style="width: 120px; padding-right: 0%; padding-left: 0px;">
											<label id="lblbhs_bmi" style="font-size: 11px;">BMI</label>
											<br />
											<asp:TextBox runat="server" CssClass="input-angka" ID="txtbmi" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
											<label class="format-satuan">kg/m2</label>
										</div>
									</div>
								</div>
								<%-- =================================================================== END VITAL SIGN ====================================================== --%>

								<%--obgyn section--%>
								<div id="divobgyn" runat="server" visible="false" class="box-border-soap" style="padding-bottom: 10px; padding-top: 10px;">
									<div style="margin-left: 15px; margin-right: 0px;">
										<div class="row">
											<div class="col-sm-2" style="padding-bottom: 7px; width: 15%;">
												<label style="font-size: 16px; font-weight: bold">Data Kehamilan : </label>
											</div>
											<div class="col-sm-3">
												<table>
													<tr>
														<td style="padding-right: 5px;">
															<strong>G</strong>
															<br />
															<asp:TextBox runat="server" Style="height: 23px; width: 100%; max-width: 50px;" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtG" onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();" />
														</td>
														<td style="padding-right: 5px;">
															<strong>P</strong>
															<br />
															<asp:TextBox runat="server" Style="height: 23px; width: 100%; max-width: 50px;" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtP" onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();" />
														</td>
														<td style="padding-right: 5px;">
															<strong>A</strong>
															<br />
															<asp:TextBox runat="server" Style="height: 23px; width: 100%; max-width: 50px;" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtA" onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();" />
														</td>
													</tr>
												</table>
											</div>
											<div class="col-sm-3">
												<strong title="Hari Pertama Haid Terakhir">HPHT</strong>
												<br />
												<asp:TextBox runat="server" Style="height: 23px; width: 100%; max-width: 150px;" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtHPHT" onkeydown="return txtOnKeyPress();" placeholder="dd/mm/yyyy" onmousedown="dateHPHT();" onchange="dateCalcForTHL();" AutoCompleteType="Disabled" />
												<i class="fa fa-refresh" style="color: gray;" title="clear" onclick="clearText('txtHPHT');"></i>
											</div>
											<div class="col-sm-3">
												<strong title="Rumus : HPHT(day)+7 ; HPHT(month)-3 ; HPHT(year)+1">THL</strong> (Tafsiran Hari Lahir)
                                                <br />
												<asp:TextBox runat="server" Style="height: 23px; width: 100%; max-width: 150px;" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtTHL" onkeydown="return txtOnKeyPress();" placeholder="dd/mm/yyyy" onmousedown="dateTHL();" AutoCompleteType="Disabled" />
												<i class="fa fa-refresh" style="color: gray;" title="clear" onclick="clearText('txtTHL');"></i>
											</div>
										</div>
									</div>
								</div>
								<%--end obgyn section--%>

								<%--pediatric section--%>
								<div id="divpediatric" runat="server" visible="false" class="box-border-soap" style="padding-bottom: 10px; padding-top: 10px;">
									<div style="margin-left: 15px; margin-right: 15px;">
										<div class="row">
											<div class="col-sm-12" style="padding-bottom: 7px;">
												<label style="font-size: 16px; font-weight: bold">Tumbuh Kembang Anak </label>
											</div>

											<div class="col-sm-2 paddingTKA">
												<strong>Tengkurap</strong>
												<br />
												<asp:TextBox runat="server" Style="height: 23px; width: 100%; max-width: 100px; border: none; outline: none;" placeholder="ketik disini..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtTengkurap" onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();" />
												<label style="color: #b2b6d1; font-size: 12px; font-family: Helvetica, Arial, sans-serif;">bln </label>
											</div>
											<div class="col-sm-2 paddingTKA">
												<strong>Duduk</strong>
												<br />
												<asp:TextBox runat="server" Style="height: 23px; width: 100%; max-width: 100px; border: none; outline: none;" placeholder="ketik disini..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtDuduk" onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();" />
												<label style="color: #b2b6d1; font-size: 12px; font-family: Helvetica, Arial, sans-serif;">bln </label>
											</div>
											<div class="col-sm-2 paddingTKA">
												<strong>Merangkak</strong>
												<br />
												<asp:TextBox runat="server" Style="height: 23px; width: 100%; max-width: 100px; border: none; outline: none;" placeholder="ketik disini..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtMerangkak" onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();" />
												<label style="color: #b2b6d1; font-size: 12px; font-family: Helvetica, Arial, sans-serif;">bln </label>
											</div>
											<div class="col-sm-2 paddingTKA">
												<strong>Berdiri</strong>
												<br />
												<asp:TextBox runat="server" Style="height: 23px; width: 100%; max-width: 100px; border: none; outline: none;" placeholder="ketik disini..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtBerdiri" onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();" />
												<label style="color: #b2b6d1; font-size: 12px; font-family: Helvetica, Arial, sans-serif;">bln </label>
											</div>
											<div class="col-sm-2 paddingTKA">
												<strong>Berjalan</strong>
												<br />
												<asp:TextBox runat="server" Style="height: 23px; width: 100%; max-width: 100px; border: none; outline: none;" placeholder="ketik disini..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtBerjalan" onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();" />
												<label style="color: #b2b6d1; font-size: 12px; font-family: Helvetica, Arial, sans-serif;">bln </label>
											</div>
											<div class="col-sm-2 paddingTKA" style="border-right: none;">
												<strong>Berbicara</strong>
												<br />
												<asp:TextBox runat="server" Style="height: 23px; width: 100%; max-width: 100px; border: none; outline: none;" placeholder="ketik disini..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtBerbicara" onkeypress="return CheckNumeric();" onkeydown="return txtOnKeyPress();" />
												<label style="color: #b2b6d1; font-size: 12px; font-family: Helvetica, Arial, sans-serif;">bln </label>
											</div>

											<div class="col-sm-12" style="padding-bottom: 7px; padding-top: 10px;">
												<label style="font-size: 16px; font-weight: bold">Kurva Pertumbuhan </label>
												<br />
												<label style="font-size: 12px; color: #b2b6d1;">*Kurva yang direkomendasikan </label>
												<br />
												<a href="javascript:ShowKurva();">
													<label style="font-size: 16px; font-weight: bold; text-decoration: underline;">Lihat Kurva </label>
												</a>

											</div>

										</div>
									</div>
								</div>
								<%--end pediatric section--%>

								<div class="box-border-soap" style="padding-bottom: 0px">
									<div style="margin-left: 15px; margin-right: 0px;">

										<div class="row">
											<div class="col-sm-2" style="width: 135px; padding-right: 0px;">
												<label style="font-size: 18px; font-weight: bold">A<asp:Label ID="LabelmandatoryA" runat="server" Style="color: red; font-weight: bold;" Text="*" Visible="false" title="mandatory"></asp:Label></label>
												&nbsp;
                                                    <a class="btn btn-default btn-sm" title="Template" style="height: 25px; padding-top: 3px; margin-bottom: 10px;" href="javascript:$('#modalTemplateA').modal();">
														<asp:Image ID="ImageTA" ImageUrl="~/Images/Icon/ic_Template.svg" class="img-template-btn" runat="server" /></a>
											</div>
											<div class="col-sm-1" style="width: 100px;">
												<label style="font-size: 18px; font-weight: bold">ICD 10:</label>
											</div>
											<div class="col-sm-2" style="padding-left: 0px;">

												<!-- kotak pencarian -->
												<div style="display: none" runat="server" visible="false">
													<asp:UpdatePanel runat="server" UpdateMode="Conditional">
														<ContentTemplate>
															<div class="row">
																<div class="col-sm-3" style="width: 190px;">
																	<asp:TextBox runat="server" ID="txtItemId" Visible="false" ReadOnly="true" />
																	<div class="has-feedback" style="width: 180px;">
																		<asp:TextBox runat="server" ID="txtitemicd" Width="180px" Placeholder="Select" ReadOnly="true" onclick="OnClickICD()" />
																		<span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px; z-index: 0;"></span>
																	</div>
																</div>
																<div class="col-sm-3">
																	<asp:UpdateProgress ID="uProgLogin" runat="server" AssociatedUpdatePanelID="upErroricd">
																		<ProgressTemplate>
																			<img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
																		</ProgressTemplate>
																	</asp:UpdateProgress>
																</div>
															</div>
														</ContentTemplate>
													</asp:UpdatePanel>

													<div id="popupicd" style="display: none; position: absolute; z-index: 1; border: 1px groove; min-width: 300px; min-height: 200px; background-color: white; max-height: 250px; max-width: 400px">
														<asp:UpdatePanel runat="server" ID="upErroricd" UpdateMode="Conditional">
															<ContentTemplate>
																<div style="margin: 5px;">
																	<asp:TextBox runat="server" ID="txtSearchItemICD" onkeydown="return txtOnKeyPressICD();"></asp:TextBox>
																	<asp:Button runat="server" ID="btnfindicd" CssClass="btn btn-warning btn-emr-small" OnClick="btnFindICD_click" Text="Find" />
																	<%--<asp:LinkButton runat="server" ID="btncloseicd" Style="padding-left:170px" OnClientClick="OnClickICD()">X</asp:LinkButton>--%>
																	<a href="javascript:OnClickICD();" style="padding-left: 170px; display: none;">&times; </a>
																	<asp:HiddenField ID="HiddenFlagSearchFocus" runat="server" />
																</div>
																<div class="scrollEMR" style="overflow-y: auto; max-height: 200px; max-width: 400px; min-width: 300px; min-height: 200px;">
																	<asp:GridView ID="gvw_icd" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed" BorderColor="Black"
																		HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
																		ShowHeaderWhenEmpty="True" DataKeyNames="DiseaseClassificationid" EmptyDataText="No Data"
																		AllowSorting="True">
																		<PagerStyle CssClass="pagination-ys" />
																		<Columns>
																			<asp:TemplateField HeaderText="Disease Classification" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
																				<ItemTemplate>
																					<asp:Label ID="DiseaseClassificationid" runat="server" Text='<%# Bind("DiseaseClassificationid") %>' Visible="false"><%# Eval("DiseaseClassificationid").ToString() %></asp:Label>
																					<asp:LinkButton Font-Underline="true" Font-Size="12px" ID="DiseaseClassification" runat="server" Text='<%# Bind("DiseaseClassification") %>' OnClientClick="OnClickICD()" OnClick="icditemselected_onclick"></asp:LinkButton>
																				</ItemTemplate>
																			</asp:TemplateField>
																		</Columns>
																	</asp:GridView>
																</div>
															</ContentTemplate>
														</asp:UpdatePanel>
													</div>
												</div>
												<!-- end kotak pencarian -->

												<!-- ## kotak pencarian autocomplete ## -->
												<%--<asp:UpdatePanel runat="server">
                                                        <ContentTemplate>--%>
												<div class="row">
													<div class="col-sm-3" style="width: 190px;">
														<div class="has-feedback" style="width: 180px;">
															<asp:TextBox ID="txtItemICD_AC" runat="server" Placeholder="Select..." Style="width: 180px;" class="autosuggest" onkeydown="return txtOnKeyPressFalseICD();"></asp:TextBox>
															<span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px; z-index: 0;"></span>
														</div>
													</div>
												</div>
												<%--</ContentTemplate>
                                                    </asp:UpdatePanel>--%>
												<!-- ## end kotak pencarian autocomplete ## -->
											</div>

											<div class="col-sm-3" style="padding-left: 0px; padding-right: 0px; width: 315px;">
												<asp:UpdatePanel ID="up_sticker_HI" runat="server" UpdateMode="Conditional">
													<ContentTemplate>
														<div class="btn-group">
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_HepBStickeroff_HI" runat="server" ToolTip="Hepatitis B" ImageUrl="~/Images/Icon/ic_HepB_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</a>
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_HepBStickeron_HI" runat="server" ToolTip="Hepatitis B" ImageUrl="~/Images/Icon/ic_HepB_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
														</div>
														<div class="btn-group">
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_HepCStickeroff_HI" runat="server" ToolTip="Hepatitis C" ImageUrl="~/Images/Icon/ic_HepC_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</a>
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_HepCStickeron_HI" runat="server" ToolTip="Hepatitis C" ImageUrl="~/Images/Icon/ic_HepC_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
														</div>
														<div class="btn-group">
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_TbcStickeroff_HI" runat="server" ToolTip="TBC" ImageUrl="~/Images/Icon/ic_TBC_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</a>
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_TbcStickeron_HI" runat="server" ToolTip="TBC" ImageUrl="~/Images/Icon/ic_TBC_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
														</div>
														<div class="btn-group">
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_HadStickeroff_HI" runat="server" ToolTip="HIV/AIDS" ImageUrl="~/Images/Icon/ic_HAD_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</a>
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_HadStickeron_HI" runat="server" ToolTip="HIV/AIDS" ImageUrl="~/Images/Icon/ic_HAD_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
														</div>
														<div class="btn-group">
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_PrtStickeroff_HI" runat="server" ToolTip="Peri Natal Resiko Tinggi" ImageUrl="~/Images/Icon/ic_PRT_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</a>
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_PrtStickeron_HI" runat="server" ToolTip="Peri Natal Resiko Tinggi" ImageUrl="~/Images/Icon/ic_PRT_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
														</div>
														<div class="btn-group">
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_RhnStickeroff_HI" runat="server" ToolTip="Rhesus Negatif" ImageUrl="~/Images/Icon/ic_RHN_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</a>
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_RhnStickeron_HI" runat="server" ToolTip="Rhesus Negatif" ImageUrl="~/Images/Icon/ic_RHN_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
														</div>
														<div class="btn-group">
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_MrsStickeroff_HI" runat="server" ToolTip="MRSA" ImageUrl="~/Images/Icon/ic_MRS_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</a>
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_MrsStickeron_HI" runat="server" ToolTip="MRSA" ImageUrl="~/Images/Icon/ic_MRS_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
														</div>
														<div class="btn-group">
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_CvStickeroff_HI" runat="server" ToolTip="Covid 19" ImageUrl="~/Images/Icon/ic_COVID_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</a>
															<a href="javascript:shortcutFormIllness();">
																<asp:Image ID="Button_CvStickeron_HI" runat="server" ToolTip="Covid 19" ImageUrl="~/Images/Icon/ic_COVID_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
														</div>
													</ContentTemplate>
												</asp:UpdatePanel>
											</div>

											<div class="col-sm-3" style="padding-left: 0px; padding-right: 0px; width: 315px; display: none;">
												<asp:UpdatePanel ID="up_sticker" runat="server" UpdateMode="Conditional">
													<ContentTemplate>
														<div class="btn-group">
															<asp:LinkButton ID="LB_hepb_sakit" runat="server" OnClick="LB_hepb_Click">
																<asp:Image ID="Button_HepBStickeroff" runat="server" ToolTip="Hepatitis B" ImageUrl="~/Images/Icon/ic_HepB_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</asp:LinkButton>
															<a href="#" data-toggle="dropdown" aria-expanded="false">
																<asp:Image ID="Button_HepBStickeron" runat="server" ToolTip="Hepatitis B" ImageUrl="~/Images/Icon/ic_HepB_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
															<ul class="dropdown-menu bdrop">
																<li>
																	<asp:LinkButton ID="LB_hepb_1" runat="server" OnClick="LB_hepb_Click"> Tidak Pernah Ada Indikasi </asp:LinkButton>
																</li>
																<li>
																	<asp:LinkButton ID="LB_hepb_2" runat="server" OnClick="LB_hepb_Click"> Sudah Sembuh </asp:LinkButton>
																</li>
															</ul>
														</div>

														<div class="btn-group">
															<asp:LinkButton ID="LB_hepc_sakit" runat="server" OnClick="LB_hepc_Click">
																<asp:Image ID="Button_HepCStickeroff" runat="server" ToolTip="Hepatitis C" ImageUrl="~/Images/Icon/ic_HepC_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</asp:LinkButton>
															<a href="#" data-toggle="dropdown" aria-expanded="false">
																<asp:Image ID="Button_HepCStickeron" runat="server" ToolTip="Hepatitis C" ImageUrl="~/Images/Icon/ic_HepC_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
															<ul class="dropdown-menu bdrop">
																<li>
																	<asp:LinkButton ID="LB_hepc_1" runat="server" OnClick="LB_hepc_Click"> Tidak Pernah Ada Indikasi </asp:LinkButton>
																</li>
																<li>
																	<asp:LinkButton ID="LB_hepc_2" runat="server" OnClick="LB_hepc_Click"> Sudah Sembuh </asp:LinkButton>
																</li>
															</ul>
														</div>

														<div class="btn-group">
															<asp:LinkButton ID="LB_tbc_sakit" runat="server" OnClick="LB_tbc_Click">
																<asp:Image ID="Button_TbcStickeroff" runat="server" ToolTip="TBC" ImageUrl="~/Images/Icon/ic_TBC_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</asp:LinkButton>
															<a href="#" data-toggle="dropdown" aria-expanded="false">
																<asp:Image ID="Button_TbcStickeron" runat="server" ToolTip="TBC" ImageUrl="~/Images/Icon/ic_TBC_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
															<ul class="dropdown-menu bdrop">
																<li>
																	<asp:LinkButton ID="LB_tbc_1" runat="server" OnClick="LB_tbc_Click"> Tidak Pernah Ada Indikasi </asp:LinkButton>
																</li>
																<li>
																	<asp:LinkButton ID="LB_tbc_2" runat="server" OnClick="LB_tbc_Click"> Sudah Sembuh </asp:LinkButton>
																</li>
															</ul>
														</div>

														<div class="btn-group">
															<asp:LinkButton ID="LB_Had_sakit" runat="server" OnClick="LB_Had_Click">
																<asp:Image ID="Button_HadStickeroff" runat="server" ToolTip="HIV/AIDS" ImageUrl="~/Images/Icon/ic_HAD_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</asp:LinkButton>
															<a href="#" data-toggle="dropdown" aria-expanded="false">
																<asp:Image ID="Button_HadStickeron" runat="server" ToolTip="HIV/AIDS" ImageUrl="~/Images/Icon/ic_HAD_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
															<ul class="dropdown-menu bdrop">
																<li>
																	<asp:LinkButton ID="LB_Had_1" runat="server" OnClick="LB_Had_Click"> Tidak Pernah Ada Indikasi </asp:LinkButton>
																</li>
																<li>
																	<asp:LinkButton ID="LB_Had_2" runat="server" OnClick="LB_Had_Click"> Sudah Sembuh </asp:LinkButton>
																</li>
															</ul>
														</div>

														<div class="btn-group">
															<asp:LinkButton ID="LB_Prt_sakit" runat="server" OnClick="LB_Prt_Click">
																<asp:Image ID="Button_PrtStickeroff" runat="server" ToolTip="Peri Natal Resiko Tinggi" ImageUrl="~/Images/Icon/ic_PRT_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</asp:LinkButton>
															<a href="#" data-toggle="dropdown" aria-expanded="false">
																<asp:Image ID="Button_PrtStickeron" runat="server" ToolTip="Peri Natal Resiko Tinggi" ImageUrl="~/Images/Icon/ic_PRT_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
															<ul class="dropdown-menu bdrop">
																<li>
																	<asp:LinkButton ID="LB_Prt_1" runat="server" OnClick="LB_Prt_Click"> Tidak Pernah Ada Indikasi </asp:LinkButton>
																</li>
																<li>
																	<asp:LinkButton ID="LB_Prt_2" runat="server" OnClick="LB_Prt_Click"> Sudah Sembuh </asp:LinkButton>
																</li>
															</ul>
														</div>

														<div class="btn-group">
															<asp:LinkButton ID="LB_Rhn_sakit" runat="server" OnClick="LB_Rhn_Click">
																<asp:Image ID="Button_RhnStickeroff" runat="server" ToolTip="Rhesus Negatif" ImageUrl="~/Images/Icon/ic_RHN_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</asp:LinkButton>
															<a href="#" data-toggle="dropdown" aria-expanded="false">
																<asp:Image ID="Button_RhnStickeron" runat="server" ToolTip="Rhesus Negatif" ImageUrl="~/Images/Icon/ic_RHN_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
															<ul class="dropdown-menu bdrop">
																<li>
																	<asp:LinkButton ID="LB_Rhn_1" runat="server" OnClick="LB_Rhn_Click"> Tidak Pernah Ada Indikasi </asp:LinkButton>
																</li>
																<li>
																	<asp:LinkButton ID="LB_Rhn_2" runat="server" OnClick="LB_Rhn_Click"> Sudah Sembuh </asp:LinkButton>
																</li>
															</ul>
														</div>

														<div class="btn-group">
															<asp:LinkButton ID="LB_Mrs_sakit" runat="server" OnClick="LB_Mrs_Click">
																<asp:Image ID="Button_MrsStickeroff" runat="server" ToolTip="MRSA" ImageUrl="~/Images/Icon/ic_MRS_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</asp:LinkButton>
															<a href="#" data-toggle="dropdown" aria-expanded="false">
																<asp:Image ID="Button_MrsStickeron" runat="server" ToolTip="MRSA" ImageUrl="~/Images/Icon/ic_MRS_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
															<ul class="dropdown-menu bdrop">
																<li>
																	<asp:LinkButton ID="LB_Mrs_1" runat="server" OnClick="LB_Mrs_Click"> Tidak Pernah Ada Indikasi </asp:LinkButton>
																</li>
																<li>
																	<asp:LinkButton ID="LB_Mrs_2" runat="server" OnClick="LB_Mrs_Click"> Sudah Sembuh </asp:LinkButton>
																</li>
															</ul>
														</div>

														<div class="btn-group">
															<asp:LinkButton ID="LB_Cv_sakit" runat="server" OnClick="LB_Cv_Click">
																<asp:Image ID="Button_CvStickeroff" runat="server" ToolTip="Covid 19" ImageUrl="~/Images/Icon/ic_COVID_stickeroff.svg" Style="width: 35px; margin-top: -2px;" />
															</asp:LinkButton>
															<a href="#" data-toggle="dropdown" aria-expanded="false">
																<asp:Image ID="Button_CvStickeron" runat="server" ToolTip="Covid 19" ImageUrl="~/Images/Icon/ic_COVID_sticker.svg" Visible="false" Style="width: 35px; margin-top: -2px;" />
															</a>
															<ul class="dropdown-menu bdrop">
																<li>
																	<asp:LinkButton ID="LB_Cv_1" runat="server" OnClick="LB_Cv_Click"> Tidak Pernah Ada Indikasi </asp:LinkButton>
																</li>
																<li>
																	<asp:LinkButton ID="LB_Cv_2" runat="server" OnClick="LB_Cv_Click"> Sudah Sembuh </asp:LinkButton>
																</li>
															</ul>
														</div>

													</ContentTemplate>
												</asp:UpdatePanel>
											</div>
											<div class="col-sm-1" style="padding-top: 2px; padding-left: 0px;">
												<asp:UpdateProgress ID="UpdateProgress14" runat="server" AssociatedUpdatePanelID="up_sticker">
													<ProgressTemplate>
														<img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
													</ProgressTemplate>
												</asp:UpdateProgress>
											</div>
										</div>
										<br />
										<asp:UpdatePanel runat="server" ID="UP_A" UpdateMode="Conditional">
											<ContentTemplate>
												<div class="scrollEMR" style="max-height: 335px; overflow-y: auto">
													<asp:TextBox runat="server" CssClass="text-multiline-dialog copydatasrc" Style="overflow-y: hidden;" placeholder="Type here..." BorderColor="transparent" ID="txtPrimary" TextMode="MultiLine" onkeydown="AutoExpand(this)" onkeypress="return CheckBack();" onkeyup="copytextto();" onfocus="AutoExpand(this)" />
												</div>
											</ContentTemplate>
										</asp:UpdatePanel>
									</div>
								</div>
								<asp:UpdatePanel runat="server" ID="UP_P" UpdateMode="Conditional">
									<ContentTemplate>
										<span class="anchor" id="planning_begin"></span>
										<div class="box-border-soap" style="padding-bottom: 0px">
											<div style="margin-left: 15px; margin-right: 0px;">
												<label style="font-size: 18px; font-weight: bold">P<asp:Label ID="LabelmandatoryP" runat="server" Style="color: red; font-weight: bold;" Text="*" Visible="false" title="mandatory"></asp:Label></label>
												&nbsp;
                                                    <a class="btn btn-default btn-sm" title="Template" style="height: 25px; padding-top: 3px; margin-bottom: 10px;" href="javascript:$('#modalTemplateP').modal();">
														<asp:Image ID="ImageTP" ImageUrl="~/Images/Icon/ic_Template.svg" class="img-template-btn" runat="server" />
													</a>
												&nbsp;&nbsp;
                                                    <label class="btn btn-sm" style="margin-top: -8px;">Quick scroll to :</label>
												<a id="gotolabrad" class="btn btn-default btn-sm" style="height: 25px; min-width: 100px; padding-top: 3px; margin-bottom: 10px;" href="#labrad_begin">Lab/Rad</a>
												<a id="gotoprescription" class="btn btn-default btn-sm" style="height: 25px; min-width: 100px; padding-top: 3px; margin-bottom: 10px;" href="#prescription_begin">Prescription</a>
												<br />
												<div class="scrollEMR" style="max-height: 335px; overflow-y: auto">
													<asp:TextBox runat="server" CssClass="text-multiline-dialog" Style="overflow-y: hidden;" placeholder="Type here..." BorderColor="transparent" ID="txtPlanning" TextMode="MultiLine" onkeydown="AutoExpand(this)" onkeypress="return CheckBack();" onfocus="AutoExpand(this)" />
												</div>
											</div>
										</div>
									</ContentTemplate>
								</asp:UpdatePanel>

								<%--<asp:UpdatePanel runat="server">
                                        <ContentTemplate>--%>
                                            <div class="box-border-soap" style="padding-bottom: 0px">
                                                <div style="margin-left: 15px; margin-right: 0px;">
                                                    <label id="lblbhs_hasiltindakansoap" style="font-size: 18px; font-weight: bold">Procedure Result</label>
                                                    &nbsp;  
                                                    <div id="divaddbuttontravel" runat="server" style="display:inline-flex">
                                                        <a id="lnkModalRekomendasi" class="btn btn-default btn-sm" title="Template" style="height: 25px; padding-top: 3px; margin-bottom: 10px;background-color:#303C9E" href="javascript:$('#modalRekomendasiTravel').modal();">
                                                           <label id="lblbhs_btntravelrecomendationsoap" style="color:white;font-weight:bold;cursor:pointer"> <i class="fa fa-plus-circle"></i> Travel Recomendation</label>
                                                        </a>
                                                    </div>
                                                    <div id="divaddbuttonreferal" runat="server" style="display:inline-flex">
                                                        <a id="lnkModalreferal" runat="server" class="btn btn-default btn-sm" title="Template" style="height: 25px; padding-top: 3px; margin-bottom: 10px;background-color:#303C9E" href="javascript:$('#modal-referal').modal();$('#BtnHiddenReferal').trigger('click');">
                                                           <label id="lblbhs_btnreferalsoap" style="color:white;font-weight:bold;cursor:pointer"> <i class="fa fa-plus-circle"></i> Referal</label>
                                                        </a>
                                                    </div>
                                                    <div id="divaddbuttonrawatinap" runat="server" style="display:inline-flex">
                                                        <a id="lnkModalrawatinap" runat="server" class="btn btn-default btn-sm" title="Rawat Inap" style="height: 25px; padding-top: 3px; margin-bottom: 10px;background-color:#303C9E" href="javascript:$('#modal-rawatinap').modal();">

                                                           <label id="lblbhs_btnrawatinap" style="color:white;font-weight:bold;cursor:pointer"> <i class="fa fa-plus-circle"></i> Rawat Inap</label>
                                                        </a>
                                                    </div>
                                                    <br />
                                                    <div class="scrollEMR" style="max-height: 335px; overflow-y: auto">
                                                        <asp:TextBox runat="server" CssClass="text-multiline-dialog" placeholder="Type here..." BorderColor="transparent" ID="txtHasilTindakan" TextMode="MultiLine" onkeydown="AutoExpand(this)" onkeypress="return CheckBack();" onfocus="AutoExpand(this)" />
                                                    </div>
                                                </div>
                                            </div>
                                        <%--</ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                    	<asp:UpdatePanel runat="server" ID="UpdatePanelRujukan" UpdateMode="Conditional">
									<ContentTemplate>
										<div class="box-border-soap" id="divrujukansoap" runat="server" style="padding-bottom: 0px">

											<tabel style="margin-left: 15px; margin-right: 0px;">
												<tr>
													<td style="">
														<label id="lblbhs_rujukan" runat="server" style="font-size: 18px; font-weight: bold; margin-right: 20px">Rujukan</label>
													</td>
													<td style="">
														<asp:LinkButton ID="BtnEditReferral" Text="Edit Rujukan"  href="javascript:$('#modal-referal').modal();$('#BtnHiddenReferal').trigger('click');" Style="font-family: Helvetica; font-weight: bold; font-size: 12px; color: #303C9E; text-decoration: underline; border-right: 0.5px solid #B9B9B9; padding-right: 10px" runat="server" OnClick="BtnEditReferral_Click" CommandName="" CommandArgument='' />

													</td>
													<td style="width: 70px">
														<asp:LinkButton ID="BtnDeleteAllReferral" Text="Hapus Semua"  Style="font-family: Helvetica; font-weight: bold; font-size: 12px; text-decoration: underline; margin-left: 20px" runat="server">
															
														</asp:LinkButton>

													</td>
												</tr>
											</tabel>
											<div style="margin-left: 15px; margin-right: 0px; padding-bottom: 10px">
												<table style="border-collapse:separate;border-spacing: 0 0.3em;">

													<asp:Repeater ID="rptrujukan" runat="server">
														<ItemTemplate>

															<asp:HiddenField ID="referral_id" runat="server" Value='<%# Bind("referral_id") %>' />

															<tr style="margin-top: 5px; margin-bottom: 5px;">
																<td style="width: 470px">
																	<asp:Label Font-Size="12px" CssClass='<%#  Eval("is_editable").ToString().ToLower() == "1" ? "txtenable" : "txtdisable" %>' Font-Names="Helvetica, Arial, sans-serif" ID="lbl_referal_doctor_name" runat="server" Text='<%#Eval("referral_doctor_name") + " - " %>'></asp:Label>
																	<asp:Label Font-Size="12px"  CssClass='<%#  Eval("is_editable").ToString().ToLower() == "1" ? "txtenable" : "txtdisable" %>' Enabled="false" Font-Names="Helvetica, Arial, sans-serif" ID="lbl_speciality_name" runat="server" Text='<%#Eval("speciality_name").ToString().ToUpper() + " - " %>'></asp:Label>
																	<asp:Label Font-Size="12px"  CssClass='<%#  Eval("is_editable").ToString().ToLower() == "1" ? "txtenable" : "txtdisable" %>'  Enabled="false" Font-Names="Helvetica, Arial, sans-serif" ID="Label19" runat="server" Text='<%# DateTime.Parse(Eval("created_date").ToString()).ToString("HH:mm")%>'></asp:Label>

																</td>
																<td>
																	<span  class='<%# Eval("referal_status").ToString().ToLower() == "registered" || Eval("referal_status").ToString()== ""  ||Eval("referal_status").ToString().ToLower() == "new" ? "lbl-new" :Eval("referal_status").ToString().ToLower() == "apointment" ? " lbl-apointment" : Eval("referal_status").ToString().ToLower()== "cancel" ? "lbl-cancel" :"lbl-chekin" %>'>
																		<asp:Label Font-Size="12px" Style="color: #FFFFFF" Font-Names="Helvetica, Arial, sans-serif" ID="lbl_referal_status" runat="server" Text='<%# Eval("referal_status").ToString() == "" ? "New" : Eval("referal_status").ToString() %>'></asp:Label>
																	</span>
																</td>
																<td>
																	<asp:LinkButton ID="BtnDeleteReferral"  Enabled='<%# Eval("is_editable").ToString().ToLower() == "1" ? true : false %>'  Text="Hapus" CssClass='<%#  Eval("is_editable").ToString().ToLower() == "1" ? "linkbtn-hapusreferal" : "linkbtn-hapusreferal-disable"%>' runat="server" OnClick="BtnDeleteReferral_Click" CommandName="DeleteRow" CommandArgument='<%# Container.ItemIndex %>' />
																</td>
															</tr>

														</ItemTemplate>
													</asp:Repeater>

												</table>
											</div>
										</div>
									</ContentTemplate>
								</asp:UpdatePanel>

								        <asp:UpdatePanel runat="server" ID="UP_Rawatinap" UpdateMode="Conditional">
									        <ContentTemplate>
										        <div id="div_rawatinap" runat="server" class="box-border-soap" style="padding-bottom: 10px;">
											        <div class="row" style="margin-left: 15px">
												        <asp:UpdatePanel ID="UP_printrawatinap" runat="server">
													        <ContentTemplate>
														        <label id="lblbhs_rawatinap" style="font-size: 16px; font-weight: bold; margin-right: 20px;">Rawat Inap</label>
														        <asp:LinkButton ID="BtnEditRawatInap" Text="Edit Rawat Inap" Style="font-family: Helvetica; font-weight: bold; font-size: 12px; color: #B9B9B9; text-decoration: underline; padding-right: 20px; border-right: 0.5px solid #B9B9B9;" runat="server" href="javascript:$('#modal-rawatinap').modal();" CommandName="DeleteRow" CommandArgument='' />
														        <asp:LinkButton ID="BtnHapusRawatInap" Text="Hapus" Style="font-family: Helvetica; font-weight: bold; font-size: 12px; color: #E84118; text-decoration: underline; margin-left: 20px; padding-right: 20px; border-right: 0.5px solid #B9B9B9" runat="server" href="javascript:$('#modal-delete-rawatinap').modal();" CommandName="DeleteRow" CommandArgument='' />
														        <asp:LinkButton ID="BtnPrintawatInap" Enabled="true" Text="Lihat Pengantar Rawat Inap" OnClick="BtnPrintRawatInap" Style="font-family: Helvetica; font-weight: bold; font-size: 12px; color: #3A3680; text-decoration: underline; margin-left: 20px" runat="server" />
													        </ContentTemplate>
												        </asp:UpdatePanel>
											        </div>
											        <div class="row" style="margin-top: 12px; margin-left: 15px">

												      <div runat="server" id="rptrawatinap" >
	
															<asp:Label Font-Size="12px" Style="color: #333" Font-Names="Helvetica, Arial, sans-serif" ID="lbl_rawatinap_dokter" runat="server" Text="nama dokter"></asp:Label>

															<asp:Label Font-Size="12px" Style="color: #333" Font-Names="Helvetica, Arial, sans-serif" ID="lbl_rawatinap_spesialis" runat="server" Text="spesialis"></asp:Label>

															<asp:Label Font-Size="12px" Style="color: #333" Font-Names="Helvetica, Arial, sans-serif" ID="lbl_rawatinap_jenis" runat="server" Text="- Rawat Inap"></asp:Label>

															<asp:Label Font-Size="12px" Style="color: #333" Font-Names="Helvetica, Arial, sans-serif" ID="lbl_rawatinap_waktu" runat="server" Text="createdate"></asp:Label>

															<span runat="server" id="stickerinpatient" style="background: #E58314; border-radius: 10px; height: 17px; padding: 1px 13px 1px 13px;text-align: center; display: inline-block;">
																<asp:Label Font-Size="12px" Style="color: #FFFFFF; letter-spacing: 0.48px" Font-Names="Helvetica, Arial, sans-serif" ID="lbl_rawatinap_status" runat="server" Text="status_booking_name"> </asp:Label>
															</span>
													
														</div>

											        </div>
										        </div>
									        </ContentTemplate>
								        </asp:UpdatePanel>          
                                    <%--<asp:UpdatePanel runat="server">
                                        <ContentTemplate>--%>
								<asp:HiddenField runat="server" ID="hdnSchedule_travel" />
								<asp:HiddenField runat="server" ID="hdnCondition_travel" />
								<asp:HiddenField runat="server" ID="hdnSeating_type" />
								<asp:HiddenField runat="server" ID="hdnEscort_type" />
								<asp:HiddenField runat="server" ID="hdnSpecial_Needs" />

								<asp:HiddenField runat="server" ID="hdncondition_date" />
								<asp:HiddenField runat="server" ID="hdnescort_ddl" />
								<div class="box-border-soap" style="padding-bottom: 0px; display: none;" id="divtravel">
									<div style="margin-left: 15px; margin-right: 0px;">
										<label id="lblbhs_rekomendasitravelsoap" style="font-size: 18px; font-weight: bold">Travel Recommendation</label>

										<div id="diveditbuttontravel" runat="server" style="display: inline-flex">
											&nbsp;&nbsp;
                                                        <a style="font-size: 12px; font-weight: bold; color: #E21100" href="javascript:RemoveTravel();">
															<label id="lblremove_recommendation" style="cursor: pointer; text-decoration: underline">Remove</label>
														</a>

											&nbsp;&nbsp;
                                                        <a style="font-size: 12px; font-weight: bold; color: #9D1FC3" href="javascript:$('#modalRekomendasiTravel').modal();">
															<label id="lbledit_recommendation" style="cursor: pointer; text-decoration: underline">Edit</label>
														</a>
										</div>
										<br />
										<div class="scrollEMR" style="max-height: 335px; overflow-y: auto">
											<asp:TextBox runat="server" CssClass="text-multiline-dialog" BorderColor="transparent" ID="txtTravelRecommendation" TextMode="MultiLine" onfocus="AutoExpand(this)" />

										</div>
									</div>
								</div>
								<%--</ContentTemplate>
                                    </asp:UpdatePanel>--%>
							</div>
						</div>
					</div>
				</div>
			</section>
			<section id="planning" style="padding-top: 150px; margin-top: -150px">
				<div class="container-fluid">
					<div class="row">
						<div class="col-lg-8 mx-auto" style="height: 100%; width: 98%;">
							<uc1:StdPlanning runat="server" ID="StdPlanning" />
						</div>
					</div>
				</div>
			</section>
		</div>

		<%-- ========================================================== button floating ================================================================ --%>
		<div class="itemcontainer" style="position: fixed; right: -255px; top: 32%; transform: translate(0,-50%); text-align: left; z-index: 20">
			<div>
				<a class="item" href="javascript:PreviewFA();" style="height: 43px;" runat="server" id="linkNURSE">
					<%--<span><strong>FA</strong></span>First Assessment--%>
					<%--<i class="icon-ic_NurseSOAP" style="font-size: 25px; vertical-align: sub; margin-left: 6px; margin-right: 18px;"></i>--%>
					<span>
						<img src="<%= Page.ResolveClientUrl("~/Images/S/ic_NurseSOAP.svg") %>" style="width: 25px;" /></span>
					<label style="font-size: 14px;">Nurse SOAP</label>
				</a>
			</div>
		</div>

		<div class="itemcontainer" style="position: fixed; right: -255px; top: 39%; transform: translate(0,-50%); text-align: left; z-index: 20">
			<div>
				<a class="item" href="javascript:ShowImunisasi();" style="height: 43px;" runat="server" id="linkImunisasi">
					<%--<span><strong>FA</strong></span>First Assessment--%>
					<%--<i class="icon-ic_NurseSOAP" style="font-size: 25px; vertical-align: sub; margin-left: 6px; margin-right: 18px;"></i>--%>
					<span>
						<img src="<%= Page.ResolveClientUrl("~/Images/S/ic_Imunisasi.svg") %>" style="width: 25px;" /></span>
					<label style="font-size: 14px;">Imunisasi</label>
				</a>
			</div>
		</div>

		<div id="divmenukurva" runat="server" visible="false" class="itemcontainer" style="position: fixed; right: -255px; top: 46%; transform: translate(0,-50%); text-align: left; z-index: 20">
			<div>
				<a class="item" href="javascript:ShowKurva();" style="height: 43px;" runat="server" id="A1">
					<span>
						<img src="<%= Page.ResolveClientUrl("~/Images/S/ic_growth.svg") %>" style="width: 25px;" /></span>
					<label id="lblbhs_chart" style="font-size: 14px;">Chart</label>
					<asp:UpdatePanel ID="UpdatePanelBtnShowKurva" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Button ID="ButtonShowKurva" runat="server" Text="Show Kurva" CssClass="hidden" OnClick="ButtonShowKurva_Click" />
						</ContentTemplate>
					</asp:UpdatePanel>
				</a>
			</div>
		</div>

		<div class="itemcontainersave" id="btnsaveasdraft" style="display: none; position: fixed; right: -165px; top: 70%; transform: translate(0,-50%); text-align: left; z-index: 20">
			<div>
				<a href="javascript:SubmitIframe_HI('DRAFT');" class="itempreview"><span>
					<img src="<%= Page.ResolveClientUrl("~/Images/S/ic_SaveAsDraft.svg") %>" /></span>Save as Draft</a>
				<asp:LinkButton ID="btnsave" runat="server" CssClass="itemsave hidden" OnClientClick="disablebuttonsave();" OnClick="btnsave_click"></asp:LinkButton>
			</div>
		</div>

		<div class="itemcontainersave" style="position: fixed; right: -165px; top: 77%; transform: translate(0,-50%); text-align: left; z-index: 20">
			<div>
				<a href="javascript:Preview();" class="itempreview"><span>
					<img src="<%= Page.ResolveClientUrl("~/Images/S/ic_PreviewResume.svg") %>" /></span>Resume Preview</a>
			</div>
		</div>

		<div class="itemcontainersave" style="display: none; position: fixed; right: -165px; top: 84%; transform: translate(0,-50%); text-align: left; z-index: 20">
			<div>
				<asp:LinkButton runat="server" CssClass="itemappt"><span><img src="<%= Page.ResolveClientUrl("~/Images/S/ic_CreateAppointment.png") %>" /></span>Create Appointment</asp:LinkButton>
			</div>
		</div>

		<div class="itemcontainersave" style="position: fixed; right: -165px; top: 84%; transform: translate(0,-50%); text-align: left; z-index: 20">
			<div id="btnsubmitsigndiv" runat="server">
				<a href="javascript:SubmitIframe_HI('SUBMIT');" class="itemsign"><span>
					<img src="<%= Page.ResolveClientUrl("~/Images/S/ic_SubmitSign.svg") %>" /></span>Submit & Sign</a>
			</div>
			<asp:UpdatePanel ID="UpdatePanelFinalSave" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:Button ID="ButtonSubmitdisableHidden" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonSubmitdisableHidden_Click" />
					<asp:Button ID="ButtonSubmitHidden" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonSubmitHidden_Click" />
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>

		<%-- ========================================================== button floating ================================================================ --%>

		<%-- ========================================================== Modal First Assessment ================================================================ --%>
		<div class="modal fade" id="modalPreviewFA" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-dialog" style="top: 2%; width: 86%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div class="modal-header" style="height: 35px; padding-top: 7px; padding-bottom: 5px">
						<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
						<h5 class="modal-title">
							<asp:Label ID="Label4" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Nurse SOAP"></asp:Label></h5>
					</div>
					<div class="modal-body" id="divFAmodal" runat="server">
						<%--<asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>--%>
						<div id="divBlokFA" runat="server" visible="false">
							<div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0; text-align: center; cursor: not-allowed;"></div>
						</div>
						<div class="header-pasien-FA">
							<uc1:PatientCardModal runat="server" ID="PatientCardModal" />
						</div>

						<!--==================================================== Kewaspadaan ========================================================-->
						<asp:UpdatePanel ID="UP_Kewaspadaan" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<div id="divKewaspadaan2" runat="server" class="row" style="padding: 0; background-color: #FEEEC1; display: none">
									<div class="divKewaspadaan" runat="server" style="width: 100%; margin-bottom: 0; padding-left: 12px; padding-right: 7px;">
										<div>
											<div style="padding: 15px 10px 14px">
												<asp:Label ID="Label18" runat="server" CssClass="" Text="" Font-Size="20px" ForeColor="#8F701C">Kewaspadaan: <span id="listKewaspadaan2" runat="server" style="font: normal normal bold 20px/24px Helvetica;"></span>
												</asp:Label>
											</div>
										</div>
									</div>
								</div>
							</ContentTemplate>

						</asp:UpdatePanel>
						<div>
							<table style="width: 99%;">
								<tr>
									<td style="width: 25%; text-align: left; vertical-align: top; padding-top: 10px;">
										<div class="item-pasien-FA-A" style="color: #c43d32;">
											<label id="lblbhs_medication1">Medication & </label>
										</div>
										<br />
										<div class="item-pasien-FA-B" style="color: #c43d32;">
											<label id="lblbhs_medication2">Allergies </label>
										</div>
										<br />
										<table border="0">
											<tr>
												<td>
													<asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
														<ContentTemplate>
															<asp:LinkButton ID="btnEditRoutine" runat="server" class="item-pasien-FA-C" OnClick="btnEditRoutine_onClick" Text="Edit"></asp:LinkButton>
														</ContentTemplate>
													</asp:UpdatePanel>
												</td>
												<td>
													<asp:UpdateProgress ID="UpdateProgress8" runat="server" AssociatedUpdatePanelID="UpdatePanel13">
														<ProgressTemplate>
															&nbsp;
                                                                     <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
														</ProgressTemplate>
													</asp:UpdateProgress>
												</td>
											</tr>
										</table>
									</td>
									<td rowspan="2" style="width: 75%; vertical-align: top; border-bottom: 1px solid #cdced9;">
										<asp:UpdatePanel ID="UP_FA_MedicationAllergies" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<div style="padding-left: 10px; padding-right: 10px; padding-top: 10px; padding-bottom: 10px">
													<div class="btn-group btn-group-justified" role="group" aria-label="...">
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px; color: #c43d32;">
															<strong>
																<label id="lblbhs_drugallergiesfa">Drug Allergies </label>
															</strong>
															<br />
															<asp:HiddenField runat="server" ID="hdnhistorydrugallergies" />
															<asp:Label runat="server" ID="lblmodalnodrug">
                                                                <label id="lblbhs_nodrugallergiesfa" style="color: #bdbfd8;"> No Drug Allergy </label>
															</asp:Label>
															<asp:Repeater runat="server" ID="rptdrugallergies">
																<ItemTemplate>
																	<li style="display: none;">
																		<asp:Label ID="NameLabel" runat="server" Text='<%#Eval("allergy") %>' Font-Size="12px" Enabled="false" />
																	</li>
																</ItemTemplate>
															</asp:Repeater>
															<asp:Repeater ID="RptDrugAllergy_HI" runat="server">
																<ItemTemplate>
																	<li>
																		<asp:Label ID="LblHealthInfoValue" runat="server" Text='<%# Eval("health_info_value") %>'></asp:Label>
																	</li>
																</ItemTemplate>
															</asp:Repeater>
														</div>
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px; color: #c43d32;">
															<strong>
																<label id="lblbhs_foodallergiesfa">Food Allergies </label>
															</strong>
															<br />
															<asp:HiddenField runat="server" ID="hdnhistoryfoodallergies" />
															<asp:Label runat="server" ID="lblmodalnofood">
                                                                <label id="lblbhs_nofoodallergiesfa" style="color: #bdbfd8;"> No Food Allergy </label>
															</asp:Label>
															<asp:Repeater runat="server" ID="rptfoodallergies">
																<ItemTemplate>
																	<li style="display: none;">
																		<asp:Label ID="NameLabel" runat="server" Text='<%#Eval("allergy") %>' Font-Size="12px" Enabled="false" />
																	</li>
																</ItemTemplate>
															</asp:Repeater>
															<asp:Repeater ID="RptFoodAllergy_HI" runat="server">
																<ItemTemplate>
																	<li>
																		<asp:Label ID="LblHealthInfoValue" runat="server" Text='<%# Eval("health_info_value") %>'></asp:Label>
																	</li>
																</ItemTemplate>
															</asp:Repeater>
														</div>
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px; color: #c43d32;">
															<strong>
																<label id="lblbhs_otherallergiesfa">Other Allergies </label>
															</strong>
															<br />
															<asp:HiddenField runat="server" ID="hdnhistoryotherallergies" />
															<asp:Label runat="server" ID="lblmodalnoother">
                                                                <label id="lblbhs_nootherallergiesfa" style="color: #bdbfd8;"> No Other Allergy </label>
															</asp:Label>
															<asp:Repeater runat="server" ID="rptotherallergies">
																<ItemTemplate>
																	<li style="display: none;">
																		<asp:Label ID="NameLabel" runat="server" Text='<%#Eval("allergy") %>' Font-Size="12px" Enabled="false" />
																	</li>
																</ItemTemplate>
															</asp:Repeater>
															<asp:Repeater ID="RptOtherAllergy_HI" runat="server">
																<ItemTemplate>
																	<li>
																		<asp:Label ID="LblHealthInfoValue" runat="server" Text='<%# Eval("health_info_value") %>'></asp:Label>
																	</li>
																</ItemTemplate>
															</asp:Repeater>
														</div>
													</div>
													<br />
													<div class="btn-group btn-group-justified" role="group" aria-label="...">
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
															<asp:HiddenField runat="server" ID="hdnhistoryroutine" />
															<strong>
																<label id="lblbhs_routinemedicationfa">Routine Medication </label>
															</strong>
															<br />
															<asp:Label runat="server" ID="lblmodalnoroute">
                                                                 <label id="lblbhs_noroutinemedicationfa" style="color: #bdbfd8;"> No Routine Medication </label>
															</asp:Label>
															<asp:Repeater runat="server" ID="rptroutinemedication">
																<ItemTemplate>
																	<li style="display: none;">
																		<asp:Label ID="NameLabel" runat="server" Text='<%#Eval("medication") %>' Font-Size="12px" Enabled="false" />
																	</li>
																</ItemTemplate>
															</asp:Repeater>
															<asp:Repeater ID="RptMedication_HI" runat="server">
																<ItemTemplate>
																	<li>
																		<asp:Label ID="LblHealthInfoValue" runat="server" Text='<%# Eval("health_info_value") %>'></asp:Label>
																	</li>
																</ItemTemplate>
															</asp:Repeater>
														</div>
													</div>
												</div>
											</ContentTemplate>
										</asp:UpdatePanel>
									</td>
								</tr>
								<tr>
									<td style="border-bottom: 1px solid #cdced9; vertical-align: bottom;">
										<div style="width: 95%; border-bottom: 2px solid black; text-align: right; margin-top: -15px;">
											<%--<a href="javascript:PreviewMedication();" style="padding-right:15px;color:green">Edit</a>--%>
											<asp:Image ID="ImageA" runat="server" ImageUrl="~/Images/FANurse/ic_Allergy&Meds.svg" Style="height: 35px; width: 35px; vertical-align: text-top; margin-bottom: -4px; margin-right: -6px;" />
										</div>
									</td>
								</tr>
								<tr>
									<td style="width: 25%; text-align: left; vertical-align: top; padding-top: 10px;">
										<div class="item-pasien-FA-A">
											<label id="lblbhs_illnes1">Health </label>
										</div>
										<br />
										<div class="item-pasien-FA-B">
											<label id="lblbhs_illnes2">Record </label>
										</div>
										<br />
										<table border="0">
											<tr>
												<td>
													<asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
														<ContentTemplate>
															<asp:LinkButton runat="server" ID="btnEditIllness" CssClass="item-pasien-FA-C" OnClick="btnEditIllness_onClick" Text="Edit"></asp:LinkButton>
															<%--<a href="javascript:PreviewIllnesss();" class="item-pasien-FA-C">Edit</a>--%>
														</ContentTemplate>
													</asp:UpdatePanel>
												</td>
												<td>
													<asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel14">
														<ProgressTemplate>
															&nbsp;
                                                                     <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
														</ProgressTemplate>
													</asp:UpdateProgress>
												</td>
											</tr>
										</table>
									</td>
									<td rowspan="2" style="width: 75%; vertical-align: top; border-bottom: 1px solid #cdced9;">
										<asp:UpdatePanel ID="UP_FA_HealthRecord" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<div style="padding-left: 10px; padding-right: 10px; padding-top: 10px; padding-bottom: 10px">
													<div class="btn-group btn-group-justified" role="group" aria-label="...">
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
															<asp:HiddenField runat="server" ID="hdnhistorysurgery" />
															<strong>
																<label id="lblbhs_surgeryhistoryfa">Surgery History </label>
															</strong>
															<br />
															<asp:Label runat="server" ID="lblmodalnosurgery">
                                                                <label id="lblbhs_nosurgeryhistoryfa" style="color: #bdbfd8;"> No Surgery History </label>
															</asp:Label>
															<div style="max-height: 88px; overflow: hidden auto" class="scrollEMR">
																<asp:Repeater runat="server" ID="rptsurgery">
																	<ItemTemplate>
																		<li style="display: none;">
																			<asp:Label ID="NameLabel" runat="server" Text='<%#Eval("surgery_type") %>' Font-Size="12px" Enabled="false" />
																			<asp:Label ID="Lblsurgerydate" runat="server" Visible='<%# Eval("surgery_type").ToString() == "Tidak Ada Operasi" ? false : true %>' Text='<%# " - " + Eval("surgery_date","{0:MMMM yyyy}") %>' Font-Size="12px" Enabled="false" />
																		</li>
																	</ItemTemplate>
																</asp:Repeater>
																<asp:Repeater runat="server" ID="RptSurgeryHistory_HI">
																	<ItemTemplate>
																		<li>
																			<asp:Label ID="LblHealthInfoValue" runat="server" Text='<%# Eval("health_info_value") + " - " + Eval("health_info_remarks")%>'></asp:Label>
																		</li>
																	</ItemTemplate>
																</asp:Repeater>
															</div>
														</div>
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
															<asp:HiddenField runat="server" ID="hdnProcedureOutside" />
															<strong>
																<label id="lblbhs_procedureoutsidefa">Procedure Outside Encounter </label>
															</strong>
															<br />
															<asp:Label runat="server" ID="lblmodalnoprocout">
                                                                <label id="lblbhs_noprocedureoutsidefa" style="color: #bdbfd8;"> No Procedure </label>
															</asp:Label>
															<div style="max-height: 88px; overflow: hidden auto" class="scrollEMR">
																<asp:Repeater runat="server" ID="rptprocout">
																	<ItemTemplate>
																		<li style="display: none;">
																			<asp:Label ID="NameLabel" runat="server" Text='<%#Eval("procedure_remarks") %>' Font-Size="12px" Enabled="false" />
																			-
                                                                            <asp:Label ID="Lblproceduredate" runat="server" Text='<%#Eval("procedure_date","{0:MMMM yyyy}") %>' Font-Size="12px" Enabled="false" />
																		</li>
																	</ItemTemplate>
																</asp:Repeater>
																<asp:Repeater runat="server" ID="RptProcedure_HI">
																	<ItemTemplate>
																		<li>
																			<asp:Label ID="LblHealthInfoValue" runat="server" Text='<%# Eval("health_info_value") + " - " + Eval("health_info_remarks")%>'></asp:Label>
																		</li>
																	</ItemTemplate>
																</asp:Repeater>
															</div>
															<%--<asp:TextBox runat="server" CssClass="text-multiline-dialog" placeholder="Type here..." BorderColor="transparent" ID="TxtProcedureOutsideFA" TextMode="MultiLine" Rows="1" onkeydown="AutoExpand(this)" onblur="minexpand(this)" />--%>
														</div>
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
														</div>
													</div>
													<br />
													<div class="btn-group btn-group-justified" role="group" aria-label="...">
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
															<asp:HiddenField runat="server" ID="hdnDiseaseHistory" />
															<strong>
																<label id="lblbhs_diseasehistoryfa">Disease History </label>
															</strong>
															<br />
															<asp:Label runat="server" ID="lblmodalnodisease">
                                                                <label id="lblbhs_nodiseasehistoryfa" style="color: #bdbfd8;"> No Disease History </label>
															</asp:Label>
															<asp:Repeater runat="server" ID="rptdisease">
																<ItemTemplate>
																	<li style="display: none;">
																		<asp:Label ID="NameLabel" runat="server" Text='<%#Eval("Remarks") %>' Font-Size="12px" Enabled="false" />
																	</li>
																</ItemTemplate>
															</asp:Repeater>
															<asp:Repeater ID="RptDisease_HI" runat="server">
																<ItemTemplate>
																	<li>
																		<asp:Label ID="LblHealthInfoValue" runat="server" Text='<%# Eval("health_info_value") %>'></asp:Label>
																	</li>
																</ItemTemplate>
															</asp:Repeater>
														</div>
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
															<asp:HiddenField runat="server" ID="hdnFamilyDiseaseHistory" />
															<strong>
																<label id="lblbhs_familydiseasehistoryfa">Family Disease History </label>
															</strong>
															<br />
															<asp:Label runat="server" ID="lblmodalnofamdisease">
                                                                <label id="lblbhs_nofamilydiseasehistoryfa" style="color: #bdbfd8;"> No Family Disease History </label>
															</asp:Label>
															<asp:Repeater runat="server" ID="rptfamdisease">
																<ItemTemplate>
																	<li style="display: none;">
																		<asp:Label ID="NameLabel" runat="server" Text='<%#Eval("Remarks") %>' Font-Size="12px" Enabled="false" />
																	</li>
																</ItemTemplate>
															</asp:Repeater>
															<asp:Repeater ID="RptFamilyDisease_HI" runat="server">
																<ItemTemplate>
																	<li>
																		<asp:Label ID="LblHealthInfoValue" runat="server" Text='<%# Eval("health_info_value") %>'></asp:Label>
																	</li>
																</ItemTemplate>
															</asp:Repeater>
														</div>
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
														</div>
													</div>
												</div>
											</ContentTemplate>
										</asp:UpdatePanel>
									</td>
								</tr>
								<tr>
									<td style="border-bottom: 1px solid #cdced9; vertical-align: bottom;">
										<div style="width: 95%; border-bottom: 2px solid black; text-align: right; margin-top: -15px;">
											<asp:Image ID="ImageB" runat="server" ImageUrl="~/Images/FANurse/ic_DiseaseHistory.svg" Style="height: 35px; width: 35px; vertical-align: text-top; margin-bottom: -4px; margin-right: -6px;" />
										</div>
									</td>
								</tr>

								<tr id="divtext_row_obgyn" class="fa_row_obgyn" runat="server" visible="false">
									<td style="width: 25%; text-align: left; vertical-align: top; padding-top: 10px;">
										<div class="item-pasien-FA-A">
											<label id="lblbhs_pregnancy1">Pregnancy </label>
										</div>
										<br />
										<div class="item-pasien-FA-B">
											<label id="lblbhs_pregnancy2">Record </label>
										</div>
										<br />
										<table border="0">
											<tr>
												<td>
													<asp:UpdatePanel ID="UpdatePanelObgyn" runat="server" UpdateMode="Conditional">
														<ContentTemplate>
															<asp:LinkButton runat="server" ID="btnEditPregnancy" CssClass="item-pasien-FA-C" OnClick="btnEditPregnancy_Click" Text="Edit"></asp:LinkButton>
														</ContentTemplate>
													</asp:UpdatePanel>
												</td>
												<td>
													<asp:UpdateProgress ID="UpdateProgressObgyn" runat="server" AssociatedUpdatePanelID="UpdatePanelObgyn">
														<ProgressTemplate>
															&nbsp;
                                                                     <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
														</ProgressTemplate>
													</asp:UpdateProgress>
												</td>
											</tr>
										</table>
									</td>
									<td rowspan="2" style="width: 75%; vertical-align: top; border-bottom: 1px solid #cdced9;">
										<asp:UpdatePanel ID="UP_FA_Obgyn" runat="server">
											<ContentTemplate>
												<div style="padding-left: 10px; padding-right: 10px; padding-top: 10px; padding-bottom: 10px">
													<div class="btn-group btn-group-justified" role="group" aria-label="...">

														<div class="row">
															<div class="col-sm-4">
																<%--<asp:HiddenField runat="server" ID="HiddenField1" />--%>
																<strong>
																	<label id="lblbhs_obstetricsfa">Obstetrics History </label>
																</strong>
																<br />
																<li>
																	<label>Menarche age : </label>
																	<asp:Label ID="lblmodalmenarche" runat="server" Text="-"></asp:Label>
																</li>
																<li>
																	<label>Menstruation : </label>
																	<asp:Label ID="lblmodalmenstruation" runat="server" Text="-"></asp:Label>
																</li>
															</div>
															<div class="col-sm-8" style="padding-left: 5px;">
																<%--<asp:HiddenField runat="server" ID="HiddenField1" />--%>
																<strong>
																	<label id="lblbhs_obstetricsfa2">&nbsp; </label>
																</strong>
																<br />
																<li>
																	<label>Complains during menstruation : </label>
																	<asp:Label ID="lblmodalcomplainmens" runat="server" Text="-"></asp:Label>
																</li>
																<li>
																	<label>Contraceptive use : </label>
																	<div style="padding-left: 20px;">
																		<asp:Repeater runat="server" ID="rptcontraception">
																			<ItemTemplate>
																				<label>- </label>
																				<asp:Label ID="lbljenis" runat="server" Text='<%#Eval("value") %>' />
																				<label>( sejak </label>
																				<asp:Label ID="lblsejak" runat="server" Text='<%#Eval("remarks") %>' />
																				<label>hingga </label>
																				<asp:Label ID="lblhingga" runat="server" Text='<%#Eval("status") %>' />
																				<label>)</label>
																				<br />
																			</ItemTemplate>
																		</asp:Repeater>
																	</div>
																</li>
															</div>
														</div>

													</div>
													<br />
													<div class="btn-group btn-group-justified" role="group" aria-label="...">
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
															<%--<asp:HiddenField runat="server" ID="HiddenField2" />--%>
															<strong>
																<label id="lblbhs_pregnancyfa">Pregnancy History </label>
															</strong>
															<br />
															<asp:Label runat="server" ID="lbl_noobgynpregnanthistory">
                                                                <label id="lblbhs_noobgynpregnanthistory" style="color: #bdbfd8;"> No Pregnant History </label>
															</asp:Label>
															<div style="padding-top: 5px;" runat="server" id="divmodalpregnanthistory">
																<table>
																	<tr>
																		<td style="width: 10%; color: #76767c; text-align: center; font-size: 10px;">PREGNANCY </td>
																		<td style="width: 10%; color: #76767c; text-align: center; font-size: 10px;">CHILD'S AGE </td>
																		<td style="width: 10%; color: #76767c; text-align: center; font-size: 10px;">GENDER </td>
																		<td style="width: 15%; color: #76767c; text-align: center; font-size: 10px;">BIRTH WEIGHT </td>
																		<td style="width: 15%; color: #76767c; text-align: center; font-size: 10px;">DELIVERY METHOD </td>
																		<td style="width: 15%; color: #76767c; text-align: center; font-size: 10px;">ASSISTED BY </td>
																		<td style="width: 15%; color: #76767c; text-align: center; font-size: 10px;">LOCATION </td>
																		<td style="width: 10%; color: #76767c; text-align: center; font-size: 10px;">BIRTH STATUS </td>
																	</tr>
																	<asp:Repeater runat="server" ID="rptpregnanthistory">
																		<ItemTemplate>
																			<tr>
																				<td style="width: 10%; font-size: 10px; border: 1px solid #cccccc; padding: 2px 4px;">
																					<asp:Label ID="lblpregnancy" runat="server" Text='<%#Eval("pregnancy_sequence") %>' />
																				</td>
																				<td style="width: 10%; font-size: 10px; border: 1px solid #cccccc; padding: 2px 4px;">
																					<asp:Label ID="lblchildage" runat="server" Text='<%#Eval("child_age") + " " + Eval("age_type") %>' />
																				</td>
																				<td style="width: 10%; font-size: 10px; border: 1px solid #cccccc; padding: 2px 4px;">
																					<asp:Label ID="lblgender" runat="server" Text='<%#Eval("child_sex").ToString() == "1" ? "Pria" : Eval("child_sex").ToString() == "2" ? "Wanita" : "N/A" %>' />
																				</td>
																				<td style="width: 15%; font-size: 10px; border: 1px solid #cccccc; padding: 2px 4px;">
																					<asp:Label ID="lblbbl" runat="server" Text='<%#Eval("BBL") + " gr" %>' />
																				</td>
																				<td style="width: 15%; font-size: 10px; border: 1px solid #cccccc; padding: 2px 4px;">
																					<asp:Label ID="lblmethod" runat="server" Text='<%#Eval("labor_type") %>' />
																				</td>
																				<td style="width: 15%; font-size: 10px; border: 1px solid #cccccc; padding: 2px 4px;">
																					<asp:Label ID="lblassisted" runat="server" Text='<%#Eval("labor_helper") %>' />
																				</td>
																				<td style="width: 15%; font-size: 10px; border: 1px solid #cccccc; padding: 2px 4px;">
																					<asp:Label ID="lbllocation" runat="server" Text='<%#Eval("labor_place") %>' />
																				</td>
																				<td style="width: 10%; font-size: 10px; border: 1px solid #cccccc; padding: 2px 4px;">
																					<asp:Label ID="lblstatus" runat="server" Text='<%#Eval("labor_doa").ToString() == "1" ? "Hidup" : Eval("labor_doa").ToString() == "2" ? "Mati" : "N/A" %>' />
																				</td>
																			</tr>
																		</ItemTemplate>
																	</asp:Repeater>
																</table>
															</div>
														</div>
													</div>
												</div>
											</ContentTemplate>
										</asp:UpdatePanel>
									</td>
								</tr>
								<tr id="divimg_row_obgyn" class="fa_row_obgyn" runat="server" visible="false">
									<td style="border-bottom: 1px solid #cdced9; vertical-align: bottom;">
										<div style="width: 95%; border-bottom: 2px solid black; text-align: right; margin-top: -15px;">
											<asp:Image ID="ImageObgyn" runat="server" ImageUrl="~/Images/FANurse/ic_pregnancy.svg" Style="height: 35px; width: 35px; vertical-align: text-top; margin-bottom: -4px; margin-right: -6px;" />
										</div>
									</td>
								</tr>

								<tr id="divtext_row_pediatric" class="fa_row_pediatric" runat="server" visible="false">
									<td style="width: 25%; text-align: left; vertical-align: top; padding-top: 10px;">
										<div class="item-pasien-FA-A">
											<label id="lblbhs_pediatric1">Prenatal </label>
										</div>
										<br />
										<div class="item-pasien-FA-B">
											<label id="lblbhs_pediatric2">Record </label>
										</div>
										<br />
										<table border="0">
											<tr>
												<td>
													<asp:UpdatePanel ID="UpdatePanelPediatric" runat="server" UpdateMode="Conditional">
														<ContentTemplate>
															<asp:LinkButton runat="server" ID="btnEditPediatric" CssClass="item-pasien-FA-C" OnClick="btnEditPediatric_Click" Text="Edit"></asp:LinkButton>
														</ContentTemplate>
													</asp:UpdatePanel>
												</td>
												<td>
													<asp:UpdateProgress ID="UpdateProgressPediatric" runat="server" AssociatedUpdatePanelID="UpdatePanelPediatric">
														<ProgressTemplate>
															&nbsp;
                                                                     <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
														</ProgressTemplate>
													</asp:UpdateProgress>
												</td>
											</tr>
										</table>
									</td>
									<td rowspan="2" style="width: 75%; vertical-align: top; border-bottom: 1px solid #cdced9;">
										<asp:UpdatePanel ID="UP_FA_Pediatric" runat="server">
											<ContentTemplate>
												<div style="padding-left: 10px; padding-right: 10px; padding-top: 10px; padding-bottom: 10px">
													<div class="btn-group btn-group-justified" role="group" aria-label="...">

														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
															<strong>
																<label id="lblbhs_prenatalhistoryfa">Prenatal History </label>
															</strong>
															<br />
															<li>
																<label>Conceived for : </label>
																<asp:Label ID="lblmodallamahamil" runat="server" Text="-"></asp:Label>
															</li>
															<li>
																<label>Pregnancy Complication : </label>
																<asp:Label ID="lblmodalkomplikasihamil" runat="server" Text="-"></asp:Label>
															</li>
															<li>
																<label>Labor History : </label>
																<asp:Label ID="lblmodalriwayatpersalinan" runat="server" Text="-"></asp:Label>
															</li>
														</div>
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
															<strong>
																<label>&nbsp; </label>
															</strong>
															<br />
															<li>
																<label>Labor Complication : </label>
																<asp:Label ID="lblmodalpenyulitpersalinan" runat="server" Text="-"></asp:Label>
															</li>
															<li>
																<label>Birth Weight : </label>
																<asp:Label ID="lblmodalbbl" runat="server" Text="-"></asp:Label>
															</li>
															<li>
																<label>Birth Length : </label>
																<asp:Label ID="lblmodalpbl" runat="server" Text="-"></asp:Label>
															</li>
														</div>
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
														</div>

													</div>
												</div>
											</ContentTemplate>
										</asp:UpdatePanel>
									</td>
								</tr>
								<tr id="divimg_row_pediatric" class="fa_row_pediatric" runat="server" visible="false">
									<td style="border-bottom: 1px solid #cdced9; vertical-align: bottom;">
										<div style="width: 95%; border-bottom: 2px solid black; text-align: right; margin-top: -15px;">
											<asp:Image ID="ImagePediatric" runat="server" ImageUrl="~/Images/FANurse/ic_pregnancy.svg" Style="height: 35px; width: 35px; vertical-align: text-top; margin-bottom: -4px; margin-right: -6px;" />
										</div>
									</td>
								</tr>

								<tr>
									<td style="width: 25%; text-align: left; vertical-align: top; padding-top: 10px;">
										<div class="item-pasien-FA-A">
											<label id="lblbhs_endemic1">Endemic Area </label>
										</div>
										<br />
										<div class="item-pasien-FA-B">
											<label id="lblbhs_endemic2">Visitation </label>
										</div>
										<br />
										<table border="0">
											<tr>
												<td>
													<asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
														<ContentTemplate>
															<asp:LinkButton runat="server" ID="btnEditEndemic" CssClass="item-pasien-FA-C" OnClick="btnEditEndemic_onClick" Text="Edit"></asp:LinkButton>
															<%--<a href="javascript:PreviewEndemic();" class="item-pasien-FA-C">Edit</a>--%>
														</ContentTemplate>
													</asp:UpdatePanel>
												</td>
												<td>
													<asp:UpdateProgress ID="UpdateProgress10" runat="server" AssociatedUpdatePanelID="UpdatePanel15">
														<ProgressTemplate>
															&nbsp;
                                                                     <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
														</ProgressTemplate>
													</asp:UpdateProgress>
												</td>
											</tr>
										</table>
									</td>
									<td rowspan="2" style="width: 75%; vertical-align: top; border-bottom: 1px solid #cdced9;">
										<asp:UpdatePanel ID="UP_FA_Endemic" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<div style="padding-left: 10px; padding-right: 10px; padding-top: 10px; padding-bottom: 10px">
													<div class="btn-group btn-group-justified" role="group" aria-label="...">
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
															<asp:HiddenField runat="server" ID="hdnEndemicArea" />
															<strong>
																<label id="lblbhs_endemicareafa">Have Been To Endemic Area </label>
															</strong>
															<br />
															<asp:Label runat="server" ID="lblmodalnoendemic">
                                                                <label id="lblbhs_noendemicareafa" style="color: #bdbfd8;"> No Visit Endemic Area </label>
															</asp:Label>
															<asp:Label runat="server" ID="lblmodalendemic"></asp:Label>
														</div>
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
															<div class=" btn-group-justified" role="group" aria-label="..." style="margin-bottom: 5px;">
																<asp:HiddenField runat="server" ID="hdnInfectiousDisease" />
																<asp:HiddenField runat="server" ID="hdnInfectiousAlert" />
																<asp:HiddenField runat="server" ID="hdnTindakan" />
																<asp:HiddenField runat="server" ID="hdnDeleteReason" />
																<strong>
																	<label id="lblbhs_screeningfa">Screening Infectious Disease </label>
																</strong>
																<br />
																<asp:Label runat="server" ID="lblmodalnoscreening">
                                                                <label id="lblbhs_noscreeningfa" style="color: #bdbfd8;"> No Screening Infectious Disease </label>
																</asp:Label>
																<asp:Repeater runat="server" ID="rptscreening">
																	<ItemTemplate>
																		<li>
																			<asp:Label ID="NameLabel" runat="server" Text='<%#Eval("infectious_symptoms_name") %>' Font-Size="12px" Enabled="false" />
																		</li>
																	</ItemTemplate>
																</asp:Repeater>
															</div>
															<div class="btn-group-justified" role="group" aira-label="...">
																<strong>
																	<label id="lblbhs_alertTitle">Kewaspadaan</label>
																</strong>
																<br />
																<asp:Label runat="server" ID="lblmodalnoalert">
                                                                <label id="lblbhs_noalertfa" style="color: #bdbfd8;"> No Infectious Alert </label>
																</asp:Label>

																<asp:Repeater runat="server" ID="rptInfectiousAlert">
																	<ItemTemplate>
																		<li style="float: left; margin-right: 15px; color: #c43d32;">
																			<asp:Label ID="lbl_infectiousAlert" runat="server" Text='<%#Eval("alert_type_name") %>' Font-Size="12px" Enabled="false" />
																		</li>
																	</ItemTemplate>
																</asp:Repeater>

															</div>
														</div>
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
														</div>
													</div>
												</div>
											</ContentTemplate>
										</asp:UpdatePanel>
									</td>
								</tr>
								<tr>
									<td style="border-bottom: 1px solid #cdced9; vertical-align: bottom;">
										<div style="width: 95%; border-bottom: 2px solid black; text-align: right; margin-top: -15px;">
											<asp:Image ID="ImageC" runat="server" ImageUrl="~/Images/FANurse/ic_EndemicArea.svg" Style="height: 35px; width: 35px; vertical-align: text-top; margin-bottom: -4px; margin-right: -6px;" />
										</div>
									</td>
								</tr>
								<tr>
									<td style="width: 25%; text-align: left; vertical-align: top; padding-top: 10px;">
										<div class="item-pasien-FA-A">
											<label id="lblbhs_nutrition1">Nutrition & </label>
										</div>
										<br />
										<div class="item-pasien-FA-B">
											<label id="lblbhs_nutrition2">Fasting </label>
										</div>
										<br />
										<table border="0">
											<tr>
												<td>
													<asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
														<ContentTemplate>
															<asp:LinkButton runat="server" ID="btnEditNutrition" CssClass="item-pasien-FA-C" OnClick="btnEditNutrition_onClick" Text="Edit"></asp:LinkButton>
															<%--<a href="javascript:PreviewNutrition();" class="item-pasien-FA-C">Edit</a>--%>
														</ContentTemplate>
													</asp:UpdatePanel>
												</td>
												<td>
													<asp:UpdateProgress ID="UpdateProgress11" runat="server" AssociatedUpdatePanelID="UpdatePanel16">
														<ProgressTemplate>
															&nbsp;
                                                                     <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
														</ProgressTemplate>
													</asp:UpdateProgress>
												</td>
											</tr>
										</table>
									</td>
									<td rowspan="2" style="width: 75%; vertical-align: top; border-bottom: 1px solid #cdced9;">
										<asp:UpdatePanel ID="UP_FA_Nutrition" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<div style="padding-left: 10px; padding-right: 10px; padding-top: 10px; padding-bottom: 10px">
													<div class="btn-group btn-group-justified" role="group" aria-label="...">
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
															<asp:HiddenField runat="server" ID="hdnNutrition" />
															<strong>
																<label id="lblbhs_nutriproblemfa">Nutrition Problem </label>
															</strong>
															<br />
															<asp:Label runat="server" ID="lblmodalnonutrition">
                                                                <label id="lblbhs_nonutriproblemfa" style="color: #bdbfd8;"> No Nutrition Problem </label>
															</asp:Label>
															<asp:Label runat="server" ID="lblnutrition"></asp:Label>
														</div>
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
															<asp:HiddenField runat="server" ID="hdnFasting" />
															<strong>
																<label id="lblbhs_fastingfa">Fasting </label>
															</strong>
															<br />
															<asp:Label runat="server" ID="lblmodalnofasting">
                                                                <label id="lblbhs_nofastingfa" style="color: #bdbfd8;"> No Fasting </label>
															</asp:Label>
															<asp:Label runat="server" ID="lblfasting"></asp:Label>
														</div>
														<div class="btn-group" role="group" style="vertical-align: top; padding-right: 10px">
														</div>
													</div>
												</div>
											</ContentTemplate>
										</asp:UpdatePanel>
									</td>
								</tr>
								<tr>
									<td style="border-bottom: 1px solid #cdced9; vertical-align: bottom;">
										<div style="width: 95%; border-bottom: 2px solid black; text-align: right; margin-top: -15px;">
											<asp:Image ID="ImageD" runat="server" ImageUrl="~/Images/FANurse/ic_Nutrition.svg" Style="height: 35px; width: 35px; vertical-align: text-top; margin-bottom: -4px; margin-right: -6px;" />
										</div>
									</td>
								</tr>
								<tr>
									<td style="width: 25%; text-align: left; vertical-align: top; padding-top: 10px;">
										<div class="item-pasien-FA-A">
											<label id="lblbhs_physical1">Physical </label>
										</div>
										<br />
										<div class="item-pasien-FA-B">
											<label id="lblbhs_physical2">Examination </label>
										</div>
										<br />
										<table border="0">
											<tr>
												<td>
													<asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Conditional">
														<ContentTemplate>
															<asp:LinkButton runat="server" ID="btnEditPhysical" CssClass="item-pasien-FA-C" OnClick="btnEditPhysical_onClick" Text="Edit"></asp:LinkButton>
															<%--<a href="javascript:PreviewPhysical();" class="item-pasien-FA-C">Edit</a>--%>
														</ContentTemplate>
													</asp:UpdatePanel>
												</td>
												<td>
													<asp:UpdateProgress ID="UpdateProgress12" runat="server" AssociatedUpdatePanelID="UpdatePanel17">
														<ProgressTemplate>
															&nbsp;
                                                                     <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
														</ProgressTemplate>
													</asp:UpdateProgress>
												</td>
											</tr>
										</table>
									</td>
									<td rowspan="2" style="width: 75%; vertical-align: top; border-bottom: 1px solid #cdced9;">
										<asp:UpdatePanel ID="UP_FA_Physical" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<div style="padding-left: 10px; padding-right: 10px; padding-top: 10px; padding-bottom: 10px">
													<div class="row">
														<div class="col-sm-5" style="vertical-align: top; padding-right: 0px">
															<strong>EMV</strong><br />
															<asp:HiddenField runat="server" ID="hdnEye" />
															<asp:HiddenField runat="server" ID="hdnMove" />
															<asp:HiddenField runat="server" ID="hdnVerbal" />
															<div class="col-lg-1 square" style="margin-top: 3px; margin-left: 0px; margin-right: 10px; padding-left: 0px; padding-right: 10px">
																<label style="font-family: Helvetica, Arial, sans-serif; font-size: 12px; color: #171717; padding-left: 9px">
																	<label id="lblbhs_scoreemv">Score </label>
																</label>
																<asp:TextBox runat="server" ID="lblScore" Value="_" BorderStyle="None" ReadOnly="true" Width="50px" Style="font-family: Helvetica, Arial, sans-serif; font-size: 18px; font-weight: bold; color: #171717; padding-right: 6px; margin-top: 0px; text-align: center; background-color: transparent"></asp:TextBox>
															</div>
															<label style="padding-top: 10px">Eye(Mata): </label>
															<asp:Label runat="server" ID="lbleye" Text="_" BorderColor="White" BorderStyle="None" BorderWidth="0" Font-Bold="true"></asp:Label>
															<br />
															<label>Move(Motorik): </label>
															<asp:Label runat="server" ID="lblmove" Text="_" BorderColor="White" BorderStyle="None" BorderWidth="0" Font-Bold="true"></asp:Label>
															<br />
															<label>Verbal(Verbal): </label>
															<asp:Label runat="server" ID="lblverbal" Text="_" BorderColor="White" BorderStyle="None" BorderWidth="0" Font-Bold="true"></asp:Label>
														</div>
														<div class="col-sm-2" style="vertical-align: top; padding-right: 10px">
															<asp:HiddenField runat="server" ID="hdnpainscale" />
															<strong>
																<label id="lblbhs_painscalefa">Pain Scale </label>
															</strong>
															<br />
															<div class="col-lg-1 square" style="margin-top: 3px; margin-left: 0px; margin-right: 10px; padding-left: 0px; padding-right: 10px">
																<label style="font-family: Helvetica, Arial, sans-serif; font-size: 12px; color: #171717; padding-left: 9px">
																	<label id="lblbhs_scorepain">Score </label>
																</label>
																<asp:TextBox runat="server" ID="txtPain" Value="_" BorderStyle="None" ReadOnly="true" Width="50px" Style="font-family: Helvetica, Arial, sans-serif; font-size: 18px; font-weight: bold; color: #171717; padding-right: 6px; margin-top: 0px; text-align: center; background-color: transparent"></asp:TextBox>
															</div>
														</div>
														<div class="col-sm-2" style="vertical-align: top; padding-right: 10px">
															<strong>
																<label id="lblbhs_mentalstatusfa">Mental Status </label>
															</strong>
															<br />
															<asp:Label runat="server" ID="lblmentalstatus"></asp:Label>
														</div>
														<div class="col-sm-3" style="vertical-align: top; padding-right: 10px">
															<strong>
																<label id="lblbhs_clfa">Consciousness Level </label>
															</strong>
															<br />
															<asp:Label runat="server" ID="lblconsciousness"></asp:Label>
														</div>
													</div>
													<div class="row" style="padding-top: 15px">
														<div class="col-sm-12" style="vertical-align: top; padding-right: 10px">
															<strong>
																<label id="lblbhs_fallriskfa">Fall Risk (Condition that needs extra attention related to fall risk) </label>
															</strong>
															<br />
															<asp:Label runat="server" ID="lblnofallrisk">
                                                                <label id="lblbhs_nofallriskfa" style="color: #bdbfd8;"> No Fall Risk </label>
															</asp:Label>
															<asp:Repeater runat="server" ID="rptnofallrisk">
																<ItemTemplate>
																	<li>
																		<asp:Label ID="NameLabel" runat="server" Text='<%#Eval("Name") %>' Font-Size="12px" Enabled="false" />
																	</li>
																</ItemTemplate>
															</asp:Repeater>
														</div>
													</div>
												</div>
											</ContentTemplate>
										</asp:UpdatePanel>
									</td>
								</tr>
								<tr>
									<td style="border-bottom: 1px solid #cdced9; vertical-align: bottom;">
										<div style="width: 95%; border-bottom: 2px solid black; text-align: right; margin-top: -15px;">
											<asp:Image ID="ImageE" runat="server" ImageUrl="~/Images/FANurse/ic_GeneralCheck.svg" Style="height: 35px; width: 35px; vertical-align: text-top; margin-bottom: -4px; margin-right: -6px;" />
										</div>
									</td>
								</tr>
							</table>
						</div>

						<%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
					</div>
				</div>
			</div>
		</div>
		<%-- ========================================================== END Modal First Assessment ================================================================ --%>

		<%-- ========================================================== Modal Imunisasi ================================================================ --%>
		<div class="modal fade" id="modalImunisasi" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
			<div class="modal-dialog" style="top: 2%; width: 80%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24); padding-bottom: 15px;">
					<uc1:StdImunisasi runat="server" ID="StdImunisasi" />
				</div>
			</div>
		</div>
		<%-- ========================================================== END Modal Imunisasi ================================================================ --%>

		<%-- ========================================================== Modal Kurva Pertumbuhan ================================================================ --%>
		<div class="modal fade" id="modalKurvaPertumbuhan" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
			<div class="modal-dialog" style="top: 2%; width: 80%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24); padding-bottom: 15px;">
					<%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="ButtonSAVECHART" runat="server" Text="SAVE" OnClick="ButtonSAVECHART_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>

					<uc1:StdKurvaPertumbuhan runat="server" ID="StdKurvaPertumbuhan" Visible="false" />
				</div>
			</div>
		</div>
		<%-- ========================================================== END Modal Imunisasi ================================================================ --%>

		<%-- ========================================================== Modal Medication And Allergies ================================================================ --%>
		<div class="modal fade" id="modalEditMedication" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<asp:UpdatePanel ID="UpdatePanelModalMedication" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="modal-dialog" style="top: 65px; width: 84%;">
						<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
							<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
								<h5 class="modal-title">
									<asp:Label ID="Label5" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server">
                                        <label id="lblbhs_editmedication"> Edit - Medication & Allergies </label>
									</asp:Label></h5>
							</div>
							<div class="modal-body">
								<div class="row">
									<div class="col-sm-4">
										<%--<asp:UpdatePanel runat="server" ID="upAllergies">
                                            <ContentTemplate>--%>
										<strong>Drug Allergies<label class="subheader" style="font-size: 10px"> (Alergi Obat)</label></strong>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjective6" Value="0" ID="rbdrug1" onclick="hidegrid(2,'rbdrug2','dvdrugs')" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">No </label>
												</div>
											</div>
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjective6" Value="1" ID="rbdrug2" onclick="ShowHideDiv8()" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">Yes </label>
												</div>
											</div>
										</div>
										<div id="dvdrugs" style="display: none">
											<div class="row">
												<div class="col-sm-5" style="padding-right: 0px;">
													<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Nama obat" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtDrugsAllergy" onkeydown="return txtOnKeyPressDrugsAllergy();" />
												</div>
												<div class="col-sm-4" style="padding-right: 0px">
													<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Reaksi" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtReactionAllergy" onkeydown="return txtOnKeyPressDrugsAllergy();" />
												</div>
												<div class="col-sm-3">
													<asp:Button runat="server" CssClass="btn btn-primary" Style="width: 100%; height: 24px; padding-top: 3px; border-radius: 4px; background-color: #2a3593; color: #ffffff" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="btnDrugsAllergy" Text="Add" OnClientClick="return checkdrugsallergy();" OnClick="btnAddAllergy_onClick" />
												</div>
											</div>
											<div class="row">
												<div class="col-sm-12">
													<div style="border: 1px solid lightgray; max-height: 125px; overflow-y: auto; margin-top: 5px;" class="scrollEMR">
														<asp:GridView ID="gvw_allergy" runat="server" AutoGenerateColumns="false" CssClass="table-kecil"
															DataKeyNames="patient_allergy_id" EmptyDataText="No Data" BorderWidth="0" HeaderStyle-BorderWidth="0">
															<PagerStyle CssClass="pagination-ys" />
															<Columns>
																<asp:TemplateField HeaderText="Nama Obat" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="50%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:HiddenField ID="patient_allergy_id" runat="server" Value='<%# Bind("patient_allergy_id") %>'></asp:HiddenField>
																		<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="allergy" runat="server" Text='<%# Bind("allergy") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Reaksi" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="40%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="allergy_reaction" runat="server" Text='<%# Bind("allergy_reaction") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField ItemStyle-Width="10%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<%--<asp:Button Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="x" OnClick="btnDeleteAllergy_onClick"></asp:Button>--%>
																		<asp:ImageButton ID="btndelete" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btnDeleteAllergy_onClick" Style="width: 12px; height: 12px; margin-top: 3px;" />
																	</ItemTemplate>
																</asp:TemplateField>
															</Columns>
														</asp:GridView>
													</div>
												</div>
											</div>
										</div>
										<%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
									</div>
									<div class="col-sm-4">
										<%--<asp:UpdatePanel runat="server" ID="upFoods">
                                            <ContentTemplate>--%>
										<strong>Food Allergies<label class="subheader" style="font-size: 10px"> (Alergi Makanan)</label></strong>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjective7" Value="0" ID="rbfood1" onclick="hidegrid(3,'rbfood2','dvfoods')" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">No </label>
												</div>
											</div>
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjective7" Value="1" ID="rbfood2" onclick="ShowHideDiv9()" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">Yes </label>
												</div>
											</div>
										</div>
										<div id="dvfoods" style="display: none">
											<div class="row">
												<div class="col-sm-5" style="padding-right: 0px;">
													<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Nama Makanan" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtDrugsFoods" onkeydown="return txtOnKeyPressFoodsAllergy();" />
												</div>
												<div class="col-sm-4" style="padding-right: 0px">
													<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Reaksi" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtReactionFoods" onkeydown="return txtOnKeyPressFoodsAllergy();" />
												</div>
												<div class="col-sm-3">
													<asp:Button runat="server" CssClass="btn btn-primary" Style="width: 100%; height: 24px; padding-top: 3px; border-radius: 4px; background-color: #2a3593; color: #ffffff" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="btnFoodAllergy" Text="Add" OnClientClick="return checkfoodallergy();" OnClick="btnAddFoodAllergy_onClick" />
												</div>
											</div>
											<div class="row">
												<div class="col-sm-12">
													<div style="border: 1px solid lightgray; max-height: 125px; overflow-y: auto; margin-top: 5px;" class="scrollEMR">
														<asp:GridView ID="gvw_foods" runat="server" AutoGenerateColumns="False" CssClass="table-kecil"
															DataKeyNames="patient_allergy_id" EmptyDataText="No Data" BorderWidth="0" HeaderStyle-BorderWidth="0">
															<PagerStyle CssClass="pagination-ys" />
															<Columns>
																<asp:TemplateField HeaderText="Nama Makanan" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="50%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:HiddenField ID="patient_allergy_id" runat="server" Value='<%# Bind("patient_allergy_id") %>'></asp:HiddenField>
																		<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="allergy" runat="server" Text='<%# Bind("allergy") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Reaksi" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="40%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="allergy_reaction" runat="server" Text='<%# Bind("allergy_reaction") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField ItemStyle-Width="10%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:ImageButton ID="btndelete" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btnDeleteFoods_onClick" Style="width: 12px; height: 12px; margin-top: 3px;" />
																	</ItemTemplate>
																</asp:TemplateField>
															</Columns>
														</asp:GridView>
													</div>
												</div>
											</div>
										</div>
										<%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
									</div>
									<div class="col-sm-4">
										<%--<asp:UpdatePanel runat="server" ID="upOthers">
                                            <ContentTemplate>--%>
										<strong>Other Allergies<label class="subheader" style="font-size: 10px"> (Alergi Lain-lain)</label></strong>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjectiveOther" Value="0" ID="rbother1" onclick="hidegrid(4,'rbother2','dvothers')" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">No </label>
												</div>
											</div>
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjectiveOther" Value="1" ID="rbother2" onclick="ShowHideDivOtherAllergy()" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">Yes </label>
												</div>
											</div>
										</div>
										<div id="dvothers" style="display: none">
											<div class="row">
												<div class="col-sm-5" style="padding-right: 0px;">
													<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Nama Alergen" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtNameOthers" onkeydown="return txtOnKeyPressOthersAllergy();" />
												</div>
												<div class="col-sm-4" style="padding-right: 0px">
													<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Reaksi" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtReactionOthers" onkeydown="return txtOnKeyPressOthersAllergy();" />
												</div>
												<div class="col-sm-3">
													<asp:Button runat="server" CssClass="btn btn-primary" Style="width: 100%; height: 24px; padding-top: 3px; border-radius: 4px; background-color: #2a3593; color: #ffffff" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="btnOtherAllergy" Text="Add" OnClientClick="return checkotherallergy();" OnClick="btnAddOtherAllergy_onClick" />
												</div>
											</div>
											<div class="row">
												<div class="col-sm-12">
													<div style="border: 1px solid lightgray; max-height: 125px; overflow-y: auto; margin-top: 5px;" class="scrollEMR">
														<asp:GridView ID="gvw_others" runat="server" AutoGenerateColumns="False" CssClass="table-kecil"
															DataKeyNames="patient_allergy_id" EmptyDataText="No Data" BorderWidth="0" HeaderStyle-BorderWidth="0">
															<PagerStyle CssClass="pagination-ys" />
															<Columns>
																<asp:TemplateField HeaderText="Nama Alergen" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="50%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:HiddenField ID="patient_allergy_id" runat="server" Value='<%# Bind("patient_allergy_id") %>'></asp:HiddenField>
																		<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="allergy" runat="server" Text='<%# Bind("allergy") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Reaksi" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="40%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="allergy_reaction" runat="server" Text='<%# Bind("allergy_reaction") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField ItemStyle-Width="10%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:ImageButton ID="btndelete" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btnDeleteOthers_onClick" Style="width: 12px; height: 12px; margin-top: 3px;" />
																	</ItemTemplate>
																</asp:TemplateField>
															</Columns>
														</asp:GridView>
													</div>
												</div>
											</div>
										</div>
										<%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
									</div>
								</div>
								<hr />
								<div class="row">
									<div class="col-sm-4">
										<%-- =============================================== PENGOBATAN SAAT INI =============================================== --%>
										<%--<asp:UpdatePanel runat="server" ID="upCurrMedication">
                                            <ContentTemplate>--%>
										<%-- <asp:Button ID ="btnMedNone" runat="server" CssClass="hidden" OnClick="btnRoutineMedNone"/>
                                                <asp:Button ID ="btnMedShow" runat="server" CssClass="hidden" OnClick="btnRoutineMedShow"/>--%>
										<asp:HiddenField runat="server" ID="hfenableroutine" />
										<strong>Routine Medication<label class="subheader" style="font-size: 10px"> (Pengobatan saat ini)</label></strong>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjective1" Value="0" ID="rbPengobatan1" onclick="hidegrid(4,'rbPengobatan2','dvPengobatan')" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">No </label>
												</div>
											</div>
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjective1" Value="1" ID="rbPengobatan2" onclick="ShowHideDiv()" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">Yes </label>
												</div>
											</div>
										</div>
										<div id="dvPengobatan" style="display: none">
											<%--<asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;max-width:90%;width:90%;resize:none;padding-left:5px;" placeholder="Type here..."  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtPengobatan" TextMode="MultiLine" Rows="3" />--%>
											<div class="row">
												<div class="col-sm-9" style="padding-right: 0px;">
													<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Nama Obat" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtRoutineMed" onkeydown="return txtOnKeyPressRoutine();" />
												</div>
												<div class="col-sm-3">
													<asp:Button runat="server" CssClass="btn btn-primary" Style="width: 100%; height: 24px; padding-top: 3px; border-radius: 4px; background-color: #2a3593; color: #ffffff" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="btnRoutineMed" Text="Add" OnClientClick="return checkroutineempty();" OnClick="btnAddRoutineMed_onClick" />
												</div>
											</div>
											<div class="row">
												<div class="col-sm-12">
													<div style="border: 1px solid lightgray; max-height: 125px; overflow-y: auto; margin-top: 5px;" class="scrollEMR">
														<asp:GridView ID="gvw_routinemed" runat="server" AutoGenerateColumns="False" CssClass="table-kecil"
															DataKeyNames="patient_routine_medication_id" EmptyDataText="No Data" BorderWidth="0" HeaderStyle-BorderWidth="0">
															<PagerStyle CssClass="pagination-ys" />
															<Columns>
																<asp:TemplateField HeaderText="Nama Obat" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:HiddenField ID="patient_routine_medication_id" runat="server" Value='<%# Bind("patient_routine_medication_id") %>'></asp:HiddenField>
																		<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="medication" runat="server" Text='<%# Bind("medication") %>'></asp:Label>
																		<asp:HiddenField ID="routine_sales_item_id" runat="server" Value='<%# Bind("routine_sales_item_id") %>'></asp:HiddenField>
																		<asp:HiddenField ID="routine_sales_item_code" runat="server" Value='<%# Bind("routine_sales_item_code") %>'></asp:HiddenField>

																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField ItemStyle-Width="10%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<%--<asp:Button Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="x" OnClick="btnDeleteRoutineMed_onClick"></asp:Button>--%>
																		<asp:ImageButton ID="btndelete" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btnDeleteRoutineMed_onClick" Style="width: 12px; height: 12px; margin-top: 3px;" />
																	</ItemTemplate>
																</asp:TemplateField>
															</Columns>
														</asp:GridView>
													</div>
												</div>
											</div>
										</div>
										<%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
										<%-- =============================================== END PENGOBATAN SAAT INI =============================================== --%>
									</div>
								</div>
							</div>
							<div class="text-right" style="padding-right: 15px; padding-bottom: 15px;">
								<div style="height: 22px; text-align: right;">
									<asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanelModalMedication">
										<ProgressTemplate>
											<div class="modal-backdrop" style="background-color: white; opacity: 0.3; text-align: center">
											</div>
											<img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
											&nbsp;
										</ProgressTemplate>
									</asp:UpdateProgress>
								</div>
								<asp:Button CssClass="btn btn-default hidden" runat="server" Text="Cancel" OnClientClick="HidePreviewMedication();" />
								<asp:Button CssClass="btn btn-lightGreen" runat="server" Text="Save" OnClientClick="return checkmandatoryradioModal();" OnClick="btnsubmitFA_click" />
								<asp:Button CssClass="btn btn-lightGreen hidden" runat="server" Text="clickNoDrug" ID="NoDrugAllergy" OnClick="NoDrugAllergy_Click" />
								<asp:Button CssClass="btn btn-lightGreen hidden" runat="server" Text="clickNoFood" ID="NoFoodAllergy" OnClick="NoFoodAllergy_Click" />
								<asp:Button CssClass="btn btn-lightGreen hidden" runat="server" Text="clickNoOther" ID="NoOtherAllergy" OnClick="NoOtherAllergy_Click" />
								<asp:Button CssClass="btn btn-lightGreen hidden" runat="server" Text="clickNoAllAllergy" ID="NoAllAllergy" OnClick="NoAllAllergy_Click" />
								<asp:Button CssClass="btn btn-lightGreen hidden" runat="server" Text="clickNoRoutine" ID="NoRoutineMedication" OnClick="NoRoutineMedication_Click" />
							</div>
						</div>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>

		<div class="modal fade" id="modalEditMedicationIframe" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">

			<div class="modal-dialog" style="width: 84%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
						<h5 class="modal-title">
							<asp:Label ID="Label16" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server">
                                <label id="lblbhs_editmedication_iframe"> Edit - Medication & Allergies </label>
							</asp:Label></h5>
					</div>
					<div class="modal-body">
						<div class="row">
							<div class="col-sm-12">
								<iframe name="IframeMedication" id="IframeMedication" runat="server" style="width: 100%; height: 70vh; border: none;"></iframe>
							</div>
						</div>
					</div>
					<div class="text-right" style="padding-right: 15px; padding-bottom: 15px;">
						<asp:UpdatePanel ID="UpdatePanelModalMedicationIframe" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:HiddenField ID="HF_CollectedData_MA" runat="server" />
								<asp:Button CssClass="btn btn-default hidden" runat="server" Text="Cancel" OnClientClick="HidePreviewMedication();" />
								<%--<asp:Button CssClass="btn btn-lightGreen" runat="server" Text="Collect" OnClientClick="CollectIframe_MedicationAllergies();" />--%>
								<%--<asp:Button CssClass="btn btn-lightGreen" runat="server" Text="Save" ID="BtnSaveDrugAllergy_HI" OnClientClick="SaveIframe_MedicationAllergies();"/>--%>

								<div id="loading-MA" style="display: none;">
									<div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center"></div>
									&nbsp;
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
								</div>

								<a href="javascript:SaveIframe_MedicationAllergies();" class="btn btn-lightGreen">SAVE</a>
								<asp:Button CssClass="btn btn-lightGreen hidden" runat="server" Text="Save" ID="BtnSaveDrugAllergy_HI" OnClick="BtnSaveDrugAllergy_HI_Click" />
								<%--<asp:Button CssClass="btn btn-primary" runat="server" Text="SUBMIT" ID="BtnSubmitDrugAllergy_HI" OnClientClick="return SubmitIframe_HI();"  />--%>
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>

		</div>
		<%-- ========================================================== END Modal Medication And Allergies ================================================================ --%>

		<%-- ========================================================== Modal Illness History ================================================================ --%>
		<div class="modal fade" id="modalEditIllness" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<asp:UpdatePanel ID="UpdatePanelModalIllness" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="modal-dialog" style="top: 65px; width: 70%;">
						<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
							<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
								<%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
								<h5 class="modal-title">
									<asp:Label ID="Label6" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server">
                                       <label id="lblbhs_editillnes"> Edit - Health Record </label>
									</asp:Label></h5>
							</div>
							<div class="modal-body" style="padding-bottom: 0px">
								<div class="row">
									<div class="col-sm-6">
										<%-- =============================================== RIWAYAT OPERASI =============================================== --%>
										<%--<asp:UpdatePanel runat="server" ID="upSurgery">
                                            <ContentTemplate>--%>
										<strong>Surgery History<label class="subheader" style="font-size: 10px"> (Riwayat operasi)</label></strong>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjective2" Value="0" ID="rbOperasi" onclick="hidegrid(1,'rbOperas2','dvoperasi')" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">No </label>
												</div>
											</div>
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjective2" Value="1" ID="rbOperas2" onclick="ShowHideDiv7()" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">Yes </label>
												</div>
											</div>
										</div>
										<div id="dvoperasi" style="display: none">
											<div class="row">
												<div class="col-sm-5" style="padding-right: 0px;">
													<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Nama operasi" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtSurgeryName" onkeydown="return txtOnKeyPressSurgery();" />
												</div>
												<div class="col-sm-4" style="padding-right: 0px;">
													<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Tanggal operasi" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" onmousedown="datesurgery();" ID="txtSurgeryDate" onkeydown="return txtOnKeyPressSurgery();" />
												</div>
												<div class="col-sm-3">
													<asp:Button runat="server" CssClass="btn btn-primary" Style="width: 100%; height: 24px; padding-top: 3px; border-radius: 4px; background-color: #2a3593; color: #ffffff" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="btnSurgery" Text="Add" OnClientClick="return checksurgery();" OnClick="btnAddSurgery_onClick" />
												</div>
											</div>
											<div class="row">
												<div class="col-sm-12">
													<div style="border: 1px solid lightgray; max-height: 125px; overflow-y: auto; margin-top: 5px;" class="scrollEMR">
														<asp:GridView ID="gvw_surgery" runat="server" AutoGenerateColumns="False" CssClass="table-kecil"
															DataKeyNames="patient_surgery_id" EmptyDataText="No Data" BorderWidth="0" HeaderStyle-BorderWidth="0">
															<PagerStyle CssClass="pagination-ys" />
															<Columns>
																<asp:TemplateField HeaderText="Nama Operasi" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="50%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:HiddenField ID="patient_surgery_id" runat="server" Value='<%# Bind("patient_surgery_id") %>'></asp:HiddenField>
																		<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="surgery_type" runat="server" Text='<%# Bind("surgery_type") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Tanggal Operasi" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="40%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="surgery_date" runat="server" Text='<%# Bind("surgery_date","{0:MMMM yyyy}") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField ItemStyle-Width="10%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<%--<asp:Button Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="x" OnClick="btnDeleteSurgery_onClick"></asp:Button>--%>
																		<asp:ImageButton ID="btndelete" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btnDeleteSurgery_onClick" Style="width: 12px; height: 12px; margin-top: 3px;" />
																	</ItemTemplate>
																</asp:TemplateField>
															</Columns>
														</asp:GridView>
													</div>
												</div>
											</div>
										</div>
										<%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
										<%-- =============================================== END RIWAYAT OPERASI =============================================== --%>
									</div>
									<div class="col-sm-6">
										<%-- =============================================== PROCEDURE OUTSIDE ENCOUNTER =============================================== --%>
										<%--<asp:UpdatePanel runat="server" ID="upprocedureoutside">
                                            <ContentTemplate>--%>
										<strong>Procedure Outside Encounter<label class="subheader" style="font-size: 10px"> (Tindakan dil Luar Pertemuan)</label></strong>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjectiveProcout" Value="0" ID="rbProcOut1" Checked="true" onclick="hidegrid(1,'rbProcOut2','dvprocout')" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">No </label>
												</div>
											</div>
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjectiveProcout" Value="1" ID="rbProcOut2" onclick="ShowHideDivProcout()" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">Yes </label>
												</div>
											</div>
										</div>
										<div id="dvprocout" style="display: none">
											<div class="row">
												<div class="col-sm-5" style="padding-right: 0px;">
													<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Nama tindakan" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtProcoutName" onkeydown="return txtOnKeyPressProcout();" />
												</div>
												<div class="col-sm-4" style="padding-right: 0px;">
													<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Tanggal tindakan" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" onmousedown="dateprocout();" ID="txtProcoutDate" onkeydown="return txtOnKeyPressProcout();" />
												</div>
												<div class="col-sm-3">
													<asp:Button runat="server" CssClass="btn btn-primary" Style="width: 100%; height: 24px; padding-top: 3px; border-radius: 4px; background-color: #2a3593; color: #ffffff" Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="btnProcout" Text="Add" OnClientClick="return checkprocout();" OnClick="btnAddProcout_onClick" />
												</div>
											</div>
											<div class="row">
												<div class="col-sm-12">
													<div style="border: 1px solid lightgray; max-height: 125px; overflow-y: auto; margin-top: 5px;" class="scrollEMR">
														<asp:GridView ID="gvw_procout" runat="server" AutoGenerateColumns="False" CssClass="table-kecil"
															DataKeyNames="procedure_history_id" EmptyDataText="No Data" BorderWidth="0" HeaderStyle-BorderWidth="0">
															<PagerStyle CssClass="pagination-ys" />
															<Columns>
																<asp:TemplateField HeaderText="Nama Tindakan" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="50%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:HiddenField ID="procedure_history_id" runat="server" Value='<%# Bind("procedure_history_id") %>'></asp:HiddenField>
																		<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="procedure_remarks" runat="server" Text='<%# Bind("procedure_remarks") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Tanggal Tindakan" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="40%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="procedure_date" runat="server" Text='<%# Bind("procedure_date","{0:MMMM yyyy}") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField ItemStyle-Width="10%" HeaderStyle-Font-Size="11px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
																	<ItemTemplate>
																		<%--<asp:Button Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="x" OnClick="btnDeleteSurgery_onClick"></asp:Button>--%>
																		<asp:ImageButton ID="btndelete" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btnDeleteProcout_onClick" Style="width: 12px; height: 12px; margin-top: 3px;" />
																	</ItemTemplate>
																</asp:TemplateField>
															</Columns>
														</asp:GridView>
													</div>
												</div>
											</div>
										</div>
										<%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
										<%-- =============================================== END RIWAYAT OPERASI =============================================== --%>
									</div>
								</div>
								<hr />
								<div class="row">
									<div class="col-sm-6">
										<%--<asp:UpdatePanel runat="server" ID="upPribadi">
                                            <ContentTemplate>--%>
										<strong>Disease History<label class="subheader" style="font-size: 10px"> (Riwayat penyakit dahulu)</label></strong>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjective3" Value="0" ID="rbpribadi1" onclick="hidecheckboxes()" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">No </label>
												</div>
											</div>
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjective3" Value="1" ID="rbpribadi2" onclick="ShowHideDiv2()" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">Yes </label>
												</div>
											</div>
										</div>
										<div id="dvPenyakitPribadi" style="display: none">
											<div class="row">
												<div class="col-xs-6">
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdisease1" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px;">Hypertension (Darah Tinggi)</label>
															</div>
														</div>
													</div>
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdisease2" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px;">Stroke</label>
															</div>
														</div>
													</div>
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdisease3" onclick="showTxtStatus('TBC')" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px;">TBC</label>
															</div>
														</div>
														<asp:DropDownList ID="DDL_TBC" runat="server" Style="width: 110px; vertical-align: top; display: none;">
															<asp:ListItem Value="Tidak Diketahui">Tidak Diketahui</asp:ListItem>
															<asp:ListItem Value="Sudah Sembuh">Sudah Sembuh</asp:ListItem>
															<asp:ListItem Value="Belum Sembuh">Belum Sembuh</asp:ListItem>
														</asp:DropDownList>
													</div>
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdisease4" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px;">Kidney Failure (Gagal Ginjal)</label>
															</div>
														</div>
													</div>
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdisease5" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px;">Convulsive (Kejang)</label>
															</div>
														</div>
													</div>
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdiseaseHepB" onclick="showTxtStatus('HEPB')" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px;">Hepatitis B</label>
															</div>
														</div>
														<asp:DropDownList ID="DDL_HepB" runat="server" Style="width: 110px; vertical-align: top; display: none;">
															<asp:ListItem Value="Tidak Diketahui">Tidak Diketahui</asp:ListItem>
															<asp:ListItem Value="Sudah Sembuh">Sudah Sembuh</asp:ListItem>
															<asp:ListItem Value="Belum Sembuh">Belum Sembuh</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
												<div class="col-xs-6">
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdisease6" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px;">Heart Failure (Gagal Jantung)</label>
															</div>
														</div>
													</div>
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdisease7" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px;">Diabetes</label>
															</div>
														</div>
													</div>
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdisease8" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px;">Asthma (Asma)</label>
															</div>
														</div>
													</div>
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdisease9" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px;">Hepatitis</label>
															</div>
														</div>
													</div>
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdisease10" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px;">Cancer (Kanker)</label>
															</div>
														</div>
													</div>
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdiseaseHepC" onclick="showTxtStatus('HEPC')" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px;">Hepatitis C</label>
															</div>
														</div>
														<asp:DropDownList ID="DDL_HepC" runat="server" Style="width: 110px; vertical-align: top; display: none;">
															<asp:ListItem Value="Tidak Diketahui">Tidak Diketahui</asp:ListItem>
															<asp:ListItem Value="Sudah Sembuh">Sudah Sembuh</asp:ListItem>
															<asp:ListItem Value="Belum Sembuh">Belum Sembuh</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>
											<div class="row" style="padding-top: 5px;">
												<div class="col-xs-3">
													<div>
														<label style="font-size: 12px;">Others</label>
													</div>
													<div>
														<label style="font-size: 12px;">(Lain-lain)</label>
													</div>
												</div>
												<div class="col-xs-9" style="padding-left: 0px;">
													<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Type here..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtDisease" TextMode="MultiLine" Rows="2" />
												</div>
												<div class="margin-cekbox hidden">
													<div class="pretty p-icon p-curve">
														<asp:CheckBox runat="server" ID="CheckBoxHAD" />
														<div class="state p-success">
															<i class="icon fa fa-check"></i>
															<label style="font-size: 12px;">HAD</label>
														</div>
													</div>
												</div>
												<div class="margin-cekbox hidden">
													<div class="pretty p-icon p-curve">
														<asp:CheckBox runat="server" ID="CheckBoxPRT" />
														<div class="state p-success">
															<i class="icon fa fa-check"></i>
															<label style="font-size: 12px;">PRT</label>
														</div>
													</div>
												</div>
												<div class="margin-cekbox hidden">
													<div class="pretty p-icon p-curve">
														<asp:CheckBox runat="server" ID="CheckBoxRHN" />
														<div class="state p-success">
															<i class="icon fa fa-check"></i>
															<label style="font-size: 12px;">RHN</label>
														</div>
													</div>
												</div>
												<div class="margin-cekbox hidden">
													<div class="pretty p-icon p-curve">
														<asp:CheckBox runat="server" ID="CheckBoxMRS" />
														<div class="state p-success">
															<i class="icon fa fa-check"></i>
															<label style="font-size: 12px;">MRS</label>
														</div>
													</div>
												</div>
												<div class="margin-cekbox hidden">
													<div class="pretty p-icon p-curve">
														<asp:CheckBox runat="server" ID="CheckBoxCOVID" />
														<div class="state p-success">
															<i class="icon fa fa-check"></i>
															<label style="font-size: 12px;">COVID</label>
														</div>
													</div>
												</div>
											</div>
										</div>
										<br />
										<%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
									</div>
									<div class="col-sm-6">
										<%--<asp:UpdatePanel runat="server" ID="upKeluarga">
                                            <ContentTemplate>--%>
										<strong>Family Disease History<label class="subheader" style="font-size: 10px"> (Riwayat penyakit keluarga)</label></strong>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjective4" Value="0" ID="rbkeluarga1" onclick="hidecheckboxesfam()" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">No </label>
												</div>
											</div>
											<div class="pretty p-default p-round" style="margin-right: 10px;">
												<asp:RadioButton runat="server" GroupName="subjective4" Value="1" ID="rbkeluarga2" onclick="ShowHideDiv3()" />
												<div class="state p-primary-o">
													<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">Yes </label>
												</div>
											</div>
										</div>
										<div id="dvPenyakitKeluarga" style="display: none">
											<div class="row">
												<div class="col-xs-6">
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdiseasefam1" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px">Heart Failure (Gagal Jantung)</label>
															</div>
														</div>
													</div>
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdiseasefam2" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px">Diabetes</label>
															</div>
														</div>
													</div>
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdiseasefam3" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px">Asthma (Asma)</label>
															</div>
														</div>
													</div>
												</div>
												<div class="col-xs-6">
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdiseasefam4" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px">Hypertension (Darah Tinggi)</label>
															</div>
														</div>
													</div>
													<div class="margin-cekbox">
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkdiseasefam5" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px">Cancer (Kanker)</label>
															</div>
														</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="col-xs-3">
													<div>
														<label>Others</label>
													</div>
													<div>
														<label>(Lain-lain)</label>
													</div>
												</div>
												<div class="col-xs-9" style="padding-left: 0px;">
													<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Type here..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtDiseaseFam" TextMode="MultiLine" Rows="2" />
												</div>
											</div>
										</div>
										<%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
									</div>
								</div>
							</div>
							<div class="text-right" style="padding-right: 15px; padding-bottom: 15px;">
								<div style="height: 22px; text-align: right;">
									<asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanelModalIllness">
										<ProgressTemplate>
											<div class="modal-backdrop" style="background-color: white; opacity: 0.3; text-align: center">
											</div>
											<img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
											&nbsp;
										</ProgressTemplate>
									</asp:UpdateProgress>
								</div>
								<asp:Button CssClass="btn btn-default" runat="server" Text="Cancel" OnClientClick="HidePreviewIllness();" />
								<asp:Button runat="server" CssClass="btn btn-lightGreen" Text="Save" OnClick="btnsubmitIllness_click" />
							</div>
						</div>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>

		<div class="modal fade" id="modalEditIllnessIframe" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">

			<div class="modal-dialog" style="width: 85%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
						<h5 class="modal-title">
							<asp:Label ID="Label17" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server">
                                <label id="lblbhs_editillnes_iframe"> Edit - Health Record </label>
							</asp:Label></h5>
					</div>
					<div class="modal-body">
						<div class="row">
							<div class="col-sm-12">
								<iframe name="IframeHealthrecord" id="IframeHealthrecord" runat="server" style="width: 100%; height: 70vh; border: none;"></iframe>
							</div>
						</div>
					</div>
					<div class="text-right" style="padding-right: 15px; padding-bottom: 15px;">
						<asp:UpdatePanel ID="UpdatePanelModalIllnessIframe" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:HiddenField ID="HF_CollectedData_HR" runat="server" />
								<asp:Button CssClass="btn btn-default hidden" runat="server" Text="Cancel" OnClientClick="HidePreviewIllness();" />
								<%--<asp:Button CssClass="btn btn-lightGreen" runat="server" Text="Collect" OnClientClick="CollectIframe_HealthRecord();" />--%>
								<%--<asp:Button CssClass="btn btn-lightGreen" runat="server" Text="Save" ID="BtnSaveHealthRecord_HI" OnClientClick="SaveIframe_HealthRecord();" />--%>

								<div id="loading-HR" style="display: none;">
									<div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center"></div>
									&nbsp;
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
								</div>

								<a href="javascript:SaveIframe_HealthRecord();" class="btn btn-lightGreen">SAVE</a>
								<asp:Button CssClass="btn btn-lightGreen hidden" runat="server" Text="Save" ID="BtnSaveHealthRecord_HI" OnClick="BtnSaveHealthRecord_HI_Click" />
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>

		</div>
		<%-- ========================================================== END Illness History ================================================================ --%>

		<%-- ========================================================== Modal Endemic Area  ================================================================ --%>
		<div class="modal fade" id="modalEditEndemic" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<asp:UpdatePanel ID="UpdatePanelModalEndemic" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="modal-dialog" style="top: 65px; width: 84%;">
						<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
							<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
								<%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
								<h5 class="modal-title">
									<asp:Label ID="Label7" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server">
                                        <label id="lblbhs_editendemic"> Edit - Endemic Area Visitation </label>
									</asp:Label></h5>
							</div>
							<div class="modal-body" style="padding-bottom: 0px">
								<div style="margin: 0px 14px">
									<div class="row">
										<%-- =============================================== KUNJUNGAN DAERAH =============================================== --%>
										<%--<asp:UpdatePanel runat="server" ID="upEndemic">
                                            <ContentTemplate>--%>
										<strong>Have been to endemic area
                                                    <label class="subheader" style="font-size: 10px">(Kunjungan ke daerah endemis dalam 3 bulan terakhir)</label></strong>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="radio-margin">
												<div class="pretty p-default p-round" style="margin-right: 10px;">
													<asp:RadioButton runat="server" GroupName="subjective5" Value="0" ID="rbkunjungan1" Checked="true" onclick="chkNoEndemic()" />
													<div class="state p-primary-o">
														<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">No </label>
													</div>
												</div>
											</div>
											<div class="radio-margin">
												<div class="pretty p-default p-round" style="margin-right: 10px;">
													<asp:RadioButton runat="server" GroupName="subjective5" Value="1" ID="rbkunjungan2" onclick="ShowHideDiv4()" />
													<div class="state p-primary-o">
														<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">Yes </label>
													</div>
												</div>
											</div>
										</div>
										<div id="dvEndemic" style="display: none; padding-top: 5px">
											<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Type here..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtEndemic" TextMode="MultiLine" Rows="2" />
										</div>
										<%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
										<%-- ===============================================END KUNJUNGAN DAERAH =============================================== --%>
									</div>
									<div class="row">
										<%-- =============================================== SKRINING PENYAKIT =============================================== --%>

										<asp:HiddenField ID="HF_mapping_fa" runat="server" />
										<div class="headerMargin">
											<strong>Screening Infectius Disease. Apakah pasien memiliki kondisi/gejala klinis sebagai berikut:</strong>
											<div class="radio-margin">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="skriningpenyakit" Value="0" ID="rbskriningno" onclick="HideSkrining()" />
													<div class="state p-primary-o">
														<label>No</label>
													</div>
												</div>
											</div>
											<div class="radio-margin" style="margin-bottom: 10px">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="skriningpenyakit" Value="1" ID="rbskriningyes" onclick="ShowSkrining()" />
													<div class="state p-primary-o">
														<label>Yes</label>
													</div>
												</div>
											</div>
											<div id="dvSkriningPenyakit" class="disabled-form-skriningPenyakit">
												<table class="table-SP" border="0" style="margin-top: 5px; width: 100%;">
													<tr>
														<th>TB/RSV/Flu</th>
														<th>MRSA</th>
														<th>Meningitis</th>
														<th>Traveller's Fever</th>
														<th>Covid 19</th>

													</tr>
													<tr>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen1" onclick="return showAlertSuggest(this, 1);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Batuk Darah</label>
																</div>
															</div>
														</td>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen5" onclick="return showAlertSuggest(this, 2);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Riwayat MRSA</label>
																</div>
															</div>
														</td>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen8" onclick="return showAlertSuggest(this, 3);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Kaku Kuduk</label>
																</div>
															</div>
														</td>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen11" onclick="return showAlertSuggest(this, 4);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Kenaikan suhu tubuh > 38 C</label>
																</div>
															</div>
														</td>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen21" onclick="return showAlertSuggest(this, 11);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Kontak erat dengan pasien COVID-19</label>
																</div>
															</div>
														</td>
													</tr>
													<tr>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen2" onclick="return showAlertSuggest(this, 1);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Keringat di malam hari</label>
																</div>
															</div>
														</td>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen6" onclick="return showAlertSuggest(this, 2);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Terpasang alat invasif</label>
																</div>
															</div>
														</td>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen9" onclick="return showAlertSuggest(this, 3);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Photophobia</label>
																</div>
															</div>
														</td>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen12" onclick="return showAlertSuggest(this, 4);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Nyeri sendi/otot</label>
																</div>
															</div>
														</td>
													</tr>
													<tr>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen3" onclick="return showAlertSuggest(this, 1);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Pembengkakan kelenjar leher</label>
																</div>
															</div>
														</td>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen7" onclick="return showAlertSuggest(this, 2);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Kontak dengan MRSA</label>
																</div>
															</div>
														</td>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen10" onclick="return showAlertSuggest(this, 3);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Demam</label>
																</div>
															</div>
														</td>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen13" onclick="return showAlertSuggest(this, 4);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Riwayat berpergian</label>
																</div>
															</div>
														</td>
													</tr>
													<tr>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen4" onclick="return showAlertSuggest(this, 1);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Batuk lebih dari 2 minggu</label>
																</div>
															</div>
														</td>
													</tr>
													<tr>
														<th style="padding-top: 10px">Others</th>
													</tr>
													<tr>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen14" onclick="return showAlertSuggest(this, 5);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Diare, mual dan muntah</label>
																</div>
															</div>
														</td>
													</tr>
													<tr>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen15" onclick="return showAlertSuggest(this, 6);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Luka terbuka dan bernanah/pus</label>
																</div>
															</div>
														</td>
													</tr>
													<tr>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen16" onclick="return showAlertSuggest(this, 7);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Suspect chickenpox/measles disertai batuk dan demam</label>
																</div>
															</div>
														</td>
													</tr>
													<tr>
														<td>
															<div class="pretty p-icon p-curve">
																<asp:CheckBox runat="server" ID="chkScreen17" onclick="return showAlertSuggest(this, 8);" />
																<div class="state p-success">
																	<i class="icon fa fa-check"></i>
																	<label style="font-size: 12px">Suspect meningococcus</label>
																</div>
															</div>
														</td>
													</tr>
												</table>
												<%-- Saran Kewaspadaan dan Tabel Info Kewaspadaan --%>
												<div class="col-lg-4" style="margin-top: 25px">
													<div class="row">
														<label style="font: normal normal bold 16px/19px Helvetica;">Saran Kewaspadaan: </label>
													</div>
													<div class="row" style="height: 16px">
														<label id="saranKewaspadaan" style="font: normal normal bold 14px/16px Helvetica;">
														</label>
													</div>
													<div class="row" style="margin-top: 7px;">
														<div id="div_infoKewaspadaan" class="box-info scrollEMR" style="margin-right: 30px">
															<table class="table-kecil" cellspacing="0" rules="all" style="border-width: 0px; border-collapse: collapse;">
																<thead>
																	<tr>
																		<th class="label-th" scope="col" style="border-width: 0px; width: 40%;">Suspect Penyakit</th>
																		<th class="label-th" scope="col" style="border-width: 0px; width: 60%">Kewaspadaan</th>
																	</tr>
																</thead>
																<tbody id="table-kewaspadaan">
																	<tr style="margin-bottom: 50px;">
																		<td scope="col" style="border-width: 0px; width: 40%;"></td>
																		<td scope="col" style="border-width: 0px; width: 60%;"></td>
																	</tr>
																</tbody>
															</table>

														</div>
													</div>
												</div>
												<%-- Checkbox Kewaspadaan --%>
												<div class="col-lg-4" style="margin-top: 25px">
													<div class="row">
														<label style="font: normal normal bold 12px/15px Helvetica;">Kewaspadaan yang dilakukan <i style="color: red;">*</i></label>
													</div>
													<div class="row">
														<div class="pretty p-icon p-curve" style="padding-right: 15px;">
															<asp:CheckBox runat="server" ID="chkKewaspadaanStandard" />
															<asp:HiddenField ID="HF_isCheckS" runat="server" Value="false" />
															<asp:HiddenField ID="HF_DeleteReasonS" runat="server" Value="" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px">Standard</label>
															</div>
														</div>
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkKewaspadaanKontak" />
															<asp:HiddenField ID="HF_isCheckK" runat="server" Value="false" />
															<asp:HiddenField ID="HF_DeleteReasonK" runat="server" Value="" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px">Kontak</label>
															</div>
														</div>
														<div class="pretty p-icon p-curve" style="padding-right: 15px;">
															<asp:CheckBox runat="server" ID="chkKewaspadaanDroplet" />
															<asp:HiddenField ID="HF_isCheckD" runat="server" Value="false" />
															<asp:HiddenField ID="HF_DeleteReasonD" runat="server" Value="" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px">Droplet</label>
															</div>
														</div>
														<div class="pretty p-icon p-curve">
															<asp:CheckBox runat="server" ID="chkKewaspadaanAirborne" />
															<asp:HiddenField ID="HF_isCheckA" runat="server" Value="false" />
															<asp:HiddenField ID="HF_DeleteReasonA" runat="server" Value="" />
															<div class="state p-success">
																<i class="icon fa fa-check"></i>
																<label style="font-size: 12px">Airborne</label>
															</div>
														</div>
													</div>
												</div>
												<%-- Checkbox Tindakan --%>
												<div class="col-lg-4" style="margin-top: 25px">
													<table class="table-SP" border="0">
														<tr>
															<th>Tindakan yang dilakukan kepada pasien
									<label style="color: red;">*</label>
															</th>
														</tr>
														<tr>
															<td>
																<div class="pretty p-icon p-curve">
																	<asp:CheckBox runat="server" ID="chkTindakan1" />
																	<div class="state p-success">
																		<i class="icon fa fa-check"></i>
																		<label style="font-size: 12px">Berikan jarak lebih dari 2 meter dari pasien non infeksi (Segregasi)</label>
																	</div>
																</div>
															</td>
														</tr>
														<tr>
															<td>
																<div class="pretty p-icon p-curve">
																	<asp:CheckBox runat="server" ID="chkTindakan2" />
																	<div class="state p-success">
																		<i class="icon fa fa-check"></i>
																		<label style="font-size: 12px">Meminta pasien menggunakan masker</label>
																	</div>
																</div>
															</td>
														</tr>
														<tr>
															<td>
																<div class="pretty p-icon p-curve">
																	<asp:CheckBox runat="server" ID="chkTindakan3" />
																	<div class="state p-success">
																		<i class="icon fa fa-check"></i>
																		<label style="font-size: 12px">Mengkaji pasien dengan kewaspadaan</label>
																	</div>
																</div>
															</td>
														</tr>
													</table>
												</div>
											</div>
										</div>
										<%-- =============================================== END SKRINING PENYAKIT =============================================== --%>
									</div>
								</div>
							</div>
							<div class="text-right" style="padding-right: 15px; padding-bottom: 15px; padding-top: 15px;">
								<div style="height: 22px; text-align: right;">
									<asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanelModalEndemic">
										<ProgressTemplate>
											<div class="modal-backdrop" style="background-color: white; opacity: 0.3; text-align: center">
											</div>
											<img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
											&nbsp;
										</ProgressTemplate>
									</asp:UpdateProgress>
								</div>
								<asp:Button CssClass="btn btn-default" runat="server" Text="Cancel" OnClientClick="HidePreviewEndemic();" />
								<asp:Button runat="server" CssClass="btn btn-lightGreen" Text="Save" OnClick="btnsubmitendemic_click" OnClientClick="return ValidateInfeksius();" />
							</div>
						</div>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
		<%-- ========================================================== END Endemic Area  ================================================================ --%>

		<%-- =============================================== MODAL ALASAN UNCHECKLIST KEWASPADAAN =============================================== --%>
		<div class="modal fade" id="modalAlasanKewaspadaan" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
			<asp:UpdatePanel runat="server">
				<ContentTemplate>
					<div class="modal-dialog" style="top: 2%; width: 30%;">
						<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24); padding-bottom: 0;">
							<div class="modal-header">
								<button type="button" class="close" data-dismiss="modal" onclick="DismissModalAlasan()">&times;</button>
								<h4 class="modal-title" style="font: normal normal bold 20px/24px Helvetica;">Tulis Alasan</h4>
							</div>
							<div class="modal-body">
								<div class="form-group" style="margin-bottom: 0">
									<asp:TextBox class="form-control" runat="server" ID="txtAlasanKewaspadaan" placeholder="Tulis disini..." Style="margin: auto; max-width: 395px; height: 89px" />
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnSvAlertDltReason" OnClick="btnSvAlertDltReason_onClick" class="btn btn-lightGreen disabled-form" runat="server" Text="Save" />
								<%--OnClick="btnSvAlertDltReason_onClick" --%>
								<asp:HiddenField ID="HF_alertType" Value="" runat="server" />
							</div>
						</div>

					</div>
				</ContentTemplate>
			</asp:UpdatePanel>

		</div>
		<%-- =============================================== END MODAL ALASAN UNCHECKLIST KEWASPADAAN =============================================== --%>

		<%-- ========================================================== Modal Nutrition & Fasting  ================================================================ --%>
		<div class="modal fade" id="modalEditNutrition" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<asp:UpdatePanel ID="UpdatePanelModalNutrition" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="modal-dialog" style="top: 65px; width: 70%;">
						<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
							<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
								<%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
								<h5 class="modal-title">
									<asp:Label ID="Label8" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Edit - Nutrition & Fasting">
                                        <%--<label id="ENGeditnutrition" style="display:<%=setENG%>;"> Edit - Nutrition & Fasting </label>
                                        <label id="INDeditnutrition" style="display:<%=setIND%>;"> Edit - Nutrisi & Puasa </label>--%>
                                        <label id="lblbhs_editnutrition"> Edit - Nutrition & Fasting </label>
									</asp:Label></h5>
							</div>
							<div class="modal-body" style="padding-bottom: 0px;">
								<div class="row">
									<div class="col-sm-6">
										<%-- =============================================== PENGOBATAN SAAT INI =============================================== --%>
										<%--<asp:UpdatePanel runat="server" ID="upNutrition">
                                            <ContentTemplate>--%>
										<strong>Nutrition Problem<label class="subheader" style="font-size: 10px"> (Masalah nutrisi khusus)</label></strong>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="subjective8" Value="0" ID="rbnutrisi1" Checked="true" onclick="hidetext('txtNutrition','rbnutrisi2','dvnutrisi')" />
												<div class="state p-primary-o">
													<label>No</label>
												</div>
											</div>
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="subjective8" Value="1" ID="rbnutrisi2" onclick="ShowHideDiv5()" />
												<div class="state p-primary-o">
													<label>Yes</label>
												</div>
											</div>
										</div>
										<div class="row" style="padding-top: 5px">
											<div class="col-xs-12" style="display: none" id="dvnutrisi">
												<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Type here..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtNutrition" onkeypress="return checkenter();" />
											</div>
										</div>
										<%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
										<%-- =============================================== END PENGOBATAN SAAT INI =============================================== --%>
									</div>
									<div class="col-sm-6">
										<%-- =============================================== PENGOBATAN SAAT INI =============================================== --%>
										<%--<asp:UpdatePanel runat="server" ID="upFasting">
                                            <ContentTemplate>--%>
										<strong>Fasting<label class="subheader" style="font-size: 10px"> (Puasa)</label></strong>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="subjective9" Value="0" ID="rbpuasa1" Checked="true" onclick="hidetext('txtFasting','rbpuasa2','dvPuasa')" />
												<div class="state p-primary-o">
													<label>No</label>
												</div>
											</div>
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="subjective9" Value="1" ID="rbpuasa2" onclick="ShowHideDiv6()" />
												<div class="state p-primary-o">
													<label>Yes</label>
												</div>
											</div>
										</div>
										<div class="row" style="padding-top: 5px">
											<div class="col-xs-12" style="display: none" id="dvPuasa">
												<asp:TextBox runat="server" Style="border-radius: 2px; border: solid 1px #cdced9; max-width: 100%; width: 100%; resize: none; padding-left: 5px;" placeholder="Type here..." Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" ID="txtFasting" onkeypress="return checkenter();" />
											</div>
										</div>
										<%-- </ContentTemplate>
                                        </asp:UpdatePanel>--%>
										<%-- =============================================== END PENGOBATAN SAAT INI =============================================== --%>
									</div>
								</div>
							</div>
							<div class="text-right" style="padding-right: 15px; padding-bottom: 15px; padding-top: 15px;">
								<div style="height: 22px; text-align: right;">
									<asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="UpdatePanelModalNutrition">
										<ProgressTemplate>
											<div class="modal-backdrop" style="background-color: white; opacity: 0.3; text-align: center">
											</div>
											<img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
											&nbsp;
										</ProgressTemplate>
									</asp:UpdateProgress>
								</div>
								<asp:Button CssClass="btn btn-default" runat="server" Text="Cancel" OnClientClick="HidePreviewNutrition();" />
								<asp:Button runat="server" CssClass="btn btn-lightGreen" Text="Save" OnClick="btnsubmitnutrition_click" />
							</div>
						</div>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
		<%-- ========================================================== END Nutrition & Fasting  ================================================================ --%>

		<%-- ========================================================== Modal Physical Examination  ================================================================ --%>
		<div class="modal fade" id="modalEditPhysical" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<asp:UpdatePanel ID="upObjective" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="modal-dialog" style="top: 65px; width: 70%;">
						<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
							<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
								<%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
								<h5 class="modal-title">
									<asp:Label ID="Label9" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server">
                                        <label id="lblbhs_editphisycal"> Edit - Physical Examination </label>
									</asp:Label></h5>
							</div>
							<div class="modal-body" style="padding-bottom: 0px;">
								<%-- =================================================================== GCS ====================================================== --%>
								<div class="row" style="margin-left: 0px; margin-right: 0px">
									<div class="col-lg-4">
										<label class="labelheader">Eye</label>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="eye" Value="4" ID="eye1" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>4. Spontaneus</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="eye" Value="3" ID="eye2" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>3. To Sound</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="eye" Value="2" ID="eye3" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>2. To Pressure</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="eye" Value="1" ID="eye4" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>1. None</label>
												</div>
											</div>
										</div>
									</div>
									<div class="col-lg-4">
										<label class="labelheader">Move</label>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="move" Value="6" ID="move1" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>6. Obey Commands</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="move" Value="5" ID="move2" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>5. Localizes to pain stimulus</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="move" Value="4" ID="move3" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>4. Withdrawns from pain</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="move" Value="3" ID="move4" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>3. Flexion to pain stumulus</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="move" Value="2" ID="move5" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>2. Extension</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="move" Value="1" ID="move6" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>1. None</label>
												</div>
											</div>
										</div>
									</div>
									<div class="col-lg-4">
										<label class="labelheader">Verbal</label>
										<div style="margin-top: 5px; margin-bottom: 5px;">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="verbal" Value="5" ID="verbal1" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>5. Orientated</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="verbal" Value="4" ID="verbal2" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>4. Confused</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="verbal" Value="3" ID="verbal3" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>3. Inappropriate words</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="verbal" Value="2" ID="verbal4" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>2. Incomprehensible sounds</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="verbal" Value="1" ID="verbal5" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>1. None</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="verbal" Value="T" ID="verbal6" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>T. Tracheostomy</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="verbal" Value="A" ID="verbal7" onclick="SumEMV();" />
												<div class="state p-primary-o">
													<label>A. Aphasia</label>
												</div>
											</div>
										</div>
									</div>
								</div>
								<hr />
								<div class="row">
									<div class="col-lg-6">
									</div>
									<div class="col-lg-5" style="text-align: right;">
										<label style="font-family: Helvetica, Arial, sans-serif; font-size: 39px; font-weight: bold; color: #4d9b35;">E</label>
										<asp:TextBox runat="server" ID="lbleyetotal" Value="_" ReadOnly="true" Width="15px" BorderColor="White" BorderStyle="None" BorderWidth="0" Style="font-family: Helvetica, Arial, sans-serif; font-size: 20px; font-weight: bold; color: #f88805; padding-left: 0px; padding-right: 0px;"></asp:TextBox>
										<label style="font-family: Helvetica, Arial, sans-serif; font-size: 39px; font-weight: bold; color: #4d9b35;">M</label>
										<asp:TextBox runat="server" ID="lblmovetotal" Value="_" ReadOnly="true" Width="15px" BorderColor="White" BorderStyle="None" BorderWidth="0" Style="font-family: Helvetica, Arial, sans-serif; font-size: 20px; font-weight: bold; color: #f88805; padding-left: 0px; padding-right: 0px;"></asp:TextBox>
										<label style="font-family: Helvetica, Arial, sans-serif; font-size: 39px; font-weight: bold; color: #4d9b35;">V</label>
										<asp:TextBox runat="server" ID="lblverbaltotal" Value="_" ReadOnly="true" Width="15px" BorderColor="White" BorderStyle="None" BorderWidth="0" Style="font-family: Helvetica, Arial, sans-serif; font-size: 20px; font-weight: bold; color: #f88805; padding-left: 0px; padding-right: 0px;"></asp:TextBox>
									</div>
									<div class="col-lg-1 square" style="margin-top: 5px; margin-left: 0px; padding-left: 0px; padding-right: 10px; padding-top: 3px;">
										<label style="font-family: Helvetica, Arial, sans-serif; font-size: 12px; color: #171717; padding-left: 8px">Score</label>
										<asp:TextBox runat="server" ID="lblTotalScore" Value="_" BorderStyle="None" Width="50px" Style="font-family: Helvetica, Arial, sans-serif; font-size: 18px; font-weight: bold; color: #171717; padding-right: 7px; margin-top: 0px; text-align: center; background-color: transparent"></asp:TextBox>
									</div>
								</div>
								<hr />

								<%-- =================================================================== END GCS ====================================================== --%>
								<div class="row" style="margin: 0px; padding: 0px">
									<div class="col-lg-12" style="padding-top: 0px; padding-bottom: 10px; padding-right: 0px; font-size: 12px">
										<label style="font-size: 14px; font-family: Helvetica, Arial, sans-serif; font-weight: bold;">Pain Scale</label>
									</div>
								</div>
								<div class="row" style="margin: 0px; padding-bottom: 0px">
									<div class="col-sm-1" style="width: 14%">
										<asp:ImageButton ImageUrl="~/Images/O/ic_Pain_1.png" runat="server" OnClientClick="getProgressBar(0); return false;" />
									</div>
									<div class="col-sm-1" style="width: 14%">
										<asp:ImageButton ImageUrl="~/Images/O/ic_Pain_2.png" runat="server" OnClientClick="getProgressBar(2); return false;" />
									</div>
									<div class="col-sm-1" style="width: 14%">
										<asp:ImageButton ImageUrl="~/Images/O/ic_Pain_4.png" runat="server" OnClientClick="getProgressBar(4); return false;" />
									</div>
									<div class="col-sm-1" style="width: 14%">
										<asp:ImageButton ImageUrl="~/Images/O/ic_Pain_6.png" runat="server" OnClientClick="getProgressBar(6); return false;" />
									</div>
									<div class="col-sm-1" style="width: 14%">
										<asp:ImageButton ImageUrl="~/Images/O/ic_Pain_8.png" runat="server" OnClientClick="getProgressBar(8); return false;" />
									</div>
									<div class="col-sm-1">
										<asp:ImageButton ImageUrl="~/Images/O/ic_Pain_10.png" runat="server" OnClientClick="getProgressBar(10); return false;" />
									</div>
									<div class="col-sm-2">
										<label style="font-size: 14px; font-family: Helvetica, Arial, sans-serif; font-weight: bold;">Value</label>
										<br>
										<asp:TextBox runat="server" ID="txtPainScale" Value="0" Width="35px" BorderColor="White" BorderStyle="None" BorderWidth="0" Style="font-family: Helvetica, Arial, sans-serif; font-size: 20px; font-weight: bold; color: #f88805; padding-left: 7px"></asp:TextBox>
									</div>
								</div>
								<div class="row" style="margin: 0px; padding: 0px">
									<div class="col-sm-9" style="padding-top: 0px; padding-bottom: 0px; font-size: 12px; width: 80%;">
										<div class="progress" style="margin: 10px; width: 92%">
											<div class="progress-bar progress-bar-success" id="divGreen">
												<span class="sr-only">35% Complete (success)</span>
											</div>
											<div class="progress-bar progress-bar-warning" id="divYellow">
												<span class="sr-only">20% Complete (warning)</span>
											</div>
											<div class="progress-bar progress-bar-danger" id="divRed">
												<span class="sr-only">10% Complete (danger)</span>
											</div>
										</div>
									</div>
								</div>
								<div class="row" style="margin: 0px; padding-left: 10px">
									<div class="col-xs-1" style="width: 7%">
										<asp:LinkButton runat="server" Text="0" OnClientClick="getProgressBar(0); return false;"></asp:LinkButton>
									</div>
									<div class="col-xs-1" style="width: 7%">
										<asp:LinkButton runat="server" Text="1" OnClientClick="getProgressBar(1); return false;"></asp:LinkButton>
									</div>
									<div class="col-xs-1" style="width: 7%">
										<asp:LinkButton runat="server" Text="2" OnClientClick="getProgressBar(2); return false;"></asp:LinkButton>
									</div>
									<div class="col-xs-1" style="width: 7%">
										<asp:LinkButton runat="server" Text="3" OnClientClick="getProgressBar(3); return false;"></asp:LinkButton>
									</div>
									<div class="col-xs-1" style="width: 7.2%">
										<asp:LinkButton runat="server" Text="4" OnClientClick="getProgressBar(4); return false;"></asp:LinkButton>
									</div>
									<div class="col-xs-1" style="width: 7.2%">
										<asp:LinkButton runat="server" Text="5" OnClientClick="getProgressBar(5); return false;"></asp:LinkButton>
									</div>
									<div class="col-xs-1" style="width: 7.2%">
										<asp:LinkButton runat="server" Text="6" OnClientClick="getProgressBar(6); return false;"></asp:LinkButton>
									</div>
									<div class="col-xs-1" style="width: 7%">
										<asp:LinkButton runat="server" Text="7" OnClientClick="getProgressBar(7); return false;"></asp:LinkButton>
									</div>
									<div class="col-xs-1" style="width: 7%">
										<asp:LinkButton runat="server" Text="8" OnClientClick="getProgressBar(8); return false;"></asp:LinkButton>
									</div>
									<div class="col-xs-1" style="width: 7%">
										<asp:LinkButton runat="server" Text="9" OnClientClick="getProgressBar(9); return false;"></asp:LinkButton>
									</div>
									<div class="col-xs-1" style="width: 7%">
										<asp:LinkButton runat="server" Text="10" OnClientClick="getProgressBar(10); return false;"></asp:LinkButton>
									</div>
								</div>
								<div>
									&nbsp
								</div>
								<div class="row" style="margin: 0px; padding: 0px">
									<div class="col-lg-12" style="padding-top: 0px; padding-bottom: 5px; padding-right: 0px; font-size: 12px">
										<asp:HiddenField runat="server" ID="hdnMentalStatus" />
										<asp:HiddenField runat="server" ID="hdnMentalStatusremark" />
										<label style="font-size: 14px; font-family: Helvetica, Arial, sans-serif; font-weight: bold;">Mental Status</label>
									</div>
								</div>
								<div class="row" style="margin: 0px; padding: 0px">
									<div class="col-lg-2" style="padding-top: 0px; padding-bottom: 10px; padding-right: 0px; font-size: 12px; width: 150px">
										<div class="pretty p-default p-round">
											<asp:RadioButton runat="server" GroupName="mental" Value="Orientasi baik" ID="mental1" />
											<div class="state p-primary-o">
												<label>Good Orientation </label>
											</div>
										</div>
									</div>
									<div class="col-lg-2" style="padding-top: 0px; padding-bottom: 10px; padding-right: 0px; font-size: 12px; width: 150px">
										<div class="pretty p-default p-round">
											<asp:RadioButton runat="server" GroupName="mental" Value="Disorientasi" ID="mental2" />
											<div class="state p-primary-o">
												<label>Disorientated </label>
											</div>
										</div>
									</div>
									<div class="col-lg-2" style="padding-top: 0px; padding-bottom: 10px; padding-right: 0px; font-size: 12px; width: 150px">
										<div class="pretty p-default p-round">
											<asp:RadioButton runat="server" GroupName="mental" Value="Kooperatif" ID="mental3" />
											<div class="state p-primary-o">
												<label>Cooperative </label>
											</div>
										</div>
									</div>
									<div class="col-lg-2" style="padding-top: 0px; padding-bottom: 10px; padding-right: 0px; font-size: 12px; width: 150px">
										<div class="pretty p-default p-round">
											<asp:RadioButton runat="server" GroupName="mental" Value="Tidak Kooperatif" ID="mental4" />
											<div class="state p-primary-o">
												<label>Non Cooperative </label>
											</div>
										</div>
									</div>
								</div>
								<div class="row" style="margin: 0px; padding: 0px">
									<div class="col-lg-12" style="padding-top: 0px; padding-bottom: 5px; padding-right: 0px; font-size: 12px">
										<asp:HiddenField runat="server" ID="hdnConsciousness" />
										<label style="font-size: 14px; font-family: Helvetica, Arial, sans-serif; font-weight: bold;">Consciousness Level</label>
									</div>
								</div>
								<div class="row" style="margin: 0px; padding: 0px">
									<div class="col-lg-2" style="padding-top: 0px; padding-bottom: 10px; padding-right: 0px; font-size: 12px; width: 150px">
										<div class="pretty p-default p-round">
											<asp:RadioButton runat="server" GroupName="consciousness" Value="1" ID="consciousness1" />
											<div class="state p-primary-o">
												<label>Compos mentis </label>
											</div>
										</div>
									</div>
									<div class="col-lg-2" style="padding-top: 0px; padding-bottom: 10px; padding-right: 0px; font-size: 12px; width: 150px">
										<div class="pretty p-default p-round">
											<asp:RadioButton runat="server" GroupName="consciousness" Value="2" ID="consciousness2" />
											<div class="state p-primary-o">
												<label>Somnolent </label>
											</div>
										</div>
									</div>
									<div class="col-lg-2" style="padding-top: 0px; padding-bottom: 10px; padding-right: 0px; font-size: 12px; width: 150px">
										<div class="pretty p-default p-round">
											<asp:RadioButton runat="server" GroupName="consciousness" Value="3" ID="consciousness3" />
											<div class="state p-primary-o">
												<label>Stupor </label>
											</div>
										</div>
									</div>
									<div class="col-lg-2" style="padding-top: 0px; padding-bottom: 10px; padding-right: 0px; font-size: 12px; width: 150px">
										<div class="pretty p-default p-round">
											<asp:RadioButton runat="server" GroupName="consciousness" Value="4" ID="consciousness4" />
											<div class="state p-primary-o">
												<label>Coma </label>
											</div>
										</div>
									</div>
								</div>
								<div class="row" style="margin: 0px; padding: 0px">
									<div class="col-lg-12" style="padding-top: 0px; padding-bottom: 5px; padding-right: 0px; font-size: 12px">
										<label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #171717; font-weight: normal">Fall Risk (Condition that needs extra attention related to fall risk)</label>
										<asp:HiddenField runat="server" ID="hdnFallRisk" />
										<asp:HiddenField runat="server" ID="hdnFallRiskHandling" />
									</div>
									<div class="col-md-5">
										<div class="margin-cekbox" style="margin-top: 5px;">
											<div class="pretty p-icon p-curve">
												<asp:CheckBox runat="server" Value="1" ID="fall1" onclick="isVisiblePenanganan();" />
												<div class="state p-success">
													<i class="icon fa fa-check"></i>
													<label style="font-size: 12px">Patient undergo sedation</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-icon p-curve">
												<asp:CheckBox runat="server" Value="1" ID="fall2" onclick="isVisiblePenanganan();" />
												<div class="state p-success">
													<i class="icon fa fa-check"></i>
													<label style="font-size: 12px">Patient with physical limitation</label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-icon p-curve">
												<asp:CheckBox runat="server" Value="1" ID="fall3" onclick="isVisiblePenanganan();" />
												<div class="state p-success">
													<i class="icon fa fa-check"></i>
													<label style="font-size: 12px">Patient with motion aids </label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-icon p-curve">
												<asp:CheckBox runat="server" Value="1" ID="fall4" onclick="isVisiblePenanganan();" />
												<div class="state p-success">
													<i class="icon fa fa-check"></i>
													<label style="font-size: 12px">Patient with balance disorder </label>
												</div>
											</div>
										</div>
										<div class="margin-cekbox">
											<div class="pretty p-icon p-curve">
												<asp:CheckBox runat="server" Value="1" ID="fall5" onclick="isVisiblePenanganan();" />
												<div class="state p-success">
													<i class="icon fa fa-check"></i>
													<label style="font-size: 12px">Fasting patient to undergo further test(Lab/Radiology/etc) </label>
												</div>
											</div>
										</div>
									</div>
									<div class="col-md-7">
										<div id="divpenanganan" style="display: none; min-width: 250px; border: 1px solid gray; border-radius: 5px; padding: 5px;">
											<label style="font-weight: bold;">Langkah Pencegahan Jatuh :</label><label style="color: red;">*</label>
											<br />
											<div class="pretty p-icon p-curve" style="margin-bottom: 8px;">
												<asp:CheckBox runat="server" ID="chkfalltempelstiker" />
												<div class="state p-success">
													<i class="icon fa fa-check"></i>
													<label style="font-size: 12px">Tempel stiker risiko jatuh</label>
												</div>
											</div>
											<br />
											<div class="pretty p-icon p-curve" style="margin-bottom: 8px;">
												<asp:CheckBox runat="server" ID="chkfalledukasi" />
												<div class="state p-success">
													<i class="icon fa fa-check"></i>
													<label style="font-size: 12px">Edukasi pencegahan</label>
												</div>
											</div>
											<br />
											<div class="pretty p-icon p-curve" style="margin-bottom: 8px;">
												<asp:CheckBox runat="server" ID="chkfallPengaman" />
												<div class="state p-success">
													<i class="icon fa fa-check"></i>
													<label style="font-size: 12px">Pastikan rem terkunci, tempat tidur posisi rendah, dan pengaman terpasang</label>
												</div>
											</div>
											<br />
											<div class="pretty p-icon p-curve" style="margin-bottom: 8px;">
												<asp:CheckBox runat="server" ID="chkfallTemaniKeluarga" />
												<div class="state p-success">
													<i class="icon fa fa-check"></i>
													<label style="font-size: 12px">Sarankan pasien selalu ditemani keluarga</label>
												</div>
											</div>
											<br />
											<div class="pretty p-icon p-curve" style="margin-bottom: 8px;">
												<asp:CheckBox runat="server" ID="chkfallAmbulasi" />
												<div class="state p-success">
													<i class="icon fa fa-check"></i>
													<label style="font-size: 12px">Bantu pasien untuk ambulasi</label>
												</div>
											</div>
											<br />
											<div class="pretty p-icon p-curve" style="margin-bottom: 8px;">
												<asp:CheckBox runat="server" ID="chkfallDokumentasiRM" />
												<div class="state p-success">
													<i class="icon fa fa-check"></i>
													<label style="font-size: 12px">Dokumentasikan risiko jatuh dan intervensi pada rekam medis</label>
												</div>
											</div>
										</div>
										<br />
									</div>
								</div>
							</div>
							<div class="text-right" style="padding-right: 15px; padding-bottom: 15px; padding-top: 15px;">
								<div style="height: 22px; text-align: right;">
									<asp:UpdateProgress ID="UpdateProgress7" runat="server" AssociatedUpdatePanelID="upObjective">
										<ProgressTemplate>
											<div class="modal-backdrop" style="background-color: white; opacity: 0.3; text-align: center">
											</div>
											<img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
											&nbsp;
										</ProgressTemplate>
									</asp:UpdateProgress>
								</div>
								<asp:Button CssClass="btn btn-default" runat="server" Text="Cancel" OnClientClick="HidePreviewPhysical();" />
								<asp:Button runat="server" CssClass="btn btn-lightGreen" Text="submit" OnClientClick="return validateFallRisk();" OnClick="btnsubmitphysical_onclick" />
							</div>
						</div>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
		<%-- ========================================================== END Physical Examination  ================================================================ --%>


		<%--SPECIALTY MODAL--%>
		<%-- ========================================================== Modal Obgyn  ================================================================ --%>
		<div class="modal fade" id="modalEditObgyn" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-dialog" style="top: 65px; width: 84%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
						<%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
						<h5 class="modal-title">
							<asp:Label ID="Label14" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server">
                                <label id="lblbhs_editobgyn"> Edit - Pregnancy Record </label>
							</asp:Label></h5>
					</div>
					<div class="modal-body" style="padding-bottom: 15px">
						<uc1:StdObgyn runat="server" ID="StdObgyn" Visible="false" />
					</div>

				</div>
			</div>
		</div>
		<%-- ========================================================== END Obgyn  ================================================================ --%>

		<%-- ========================================================== Modal Pediatric  ================================================================ --%>
		<div class="modal fade" id="modalEditPediatric" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-dialog" style="top: 65px; width: 65%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
						<%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
						<h5 class="modal-title">
							<asp:Label ID="Label15" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server">
                                <label id="lblbhs_editpediatric"> Edit - Prenatal Record </label>
							</asp:Label></h5>
					</div>
					<div class="modal-body" style="padding-bottom: 15px">
						<uc1:StdPediatric runat="server" ID="StdPediatric" Visible="false" />
					</div>

				</div>
			</div>
		</div>
		<%-- ========================================================== END Pediatric  ================================================================ --%>
		<%--END SPECIALTY MODAL--%>


		<%-- ========================================================== Modal Rekomendasi Travel  ================================================================ --%>
		<div class="modal fade" id="modalRekomendasiTravel" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">

			<div class="modal-dialog" style="top: 65px; width: 70%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
						<%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
						<h5 class="modal-title">
							<asp:Label ID="Label13" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Edit - Nutrition & Fasting">
                                        <%--<label id="ENGeditnutrition" style="display:<%=setENG%>;"> Edit - Nutrition & Fasting </label>
                                        <label id="INDeditnutrition" style="display:<%=setIND%>;"> Edit - Nutrisi & Puasa </label>--%>
                                        <label id="lblbhs_rekomendasitravel"> Travel Recommendation </label>
							</asp:Label></h5>
					</div>
					<div class="modal-body" style="padding-bottom: 0px;">
						<%--<asp:UpdatePanel ID="UpdatePanel21" runat="server">
                            <ContentTemplate>--%>
						<div class="row">
							<div class="col-sm-12">
								<%-- =============================================== HEADER SECTION =============================================== --%>
								<strong>This patient is scheduled to travel on</strong>
								<div class="has-feedback" style="display: inline;">
									<asp:TextBox runat="server" ID="txtdatescheduletravel" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; resize: none; padding-left: 5px;" onmousedown="datescheduletravel();"></asp:TextBox>
									<span id="searchcalendar" runat="server" class="fa fa-calendar-o" style="margin-left: -22px"></span>
								</div>
								<br />
								<br />
								<strong>Patient’s Condition</strong>
								<br />

								<div style="margin-top: 5px; margin-bottom: 5px;">
									<div class="pretty p-default p-round">
										<asp:RadioButton runat="server" GroupName="rekomendasitravel" Value="0" ID="rbtravel1" onclick="showdetailtravel()" />
										<div class="state p-primary-o">
											<label>Fit to fly as scheduled</label>
										</div>
									</div>
								</div>
								<div style="margin-top: 5px; margin-bottom: 5px;">
									<div class="pretty p-default p-round">
										<asp:RadioButton runat="server" GroupName="rekomendasitravel" Value="1" ID="rbtravel2" onclick="hidedetailtravel()" />
										<div class="state p-primary-o">
											<label>Not fit to fly as scheduled</label>
										</div>
									</div>
								</div>
								<div style="margin-top: 5px; margin-bottom: 5px;">
									<div class="pretty p-default p-round">
										<asp:RadioButton runat="server" GroupName="rekomendasitravel" Value="2" ID="rbtravel3" onclick="showdetailtravelanticipated()" />
										<div class="state p-primary-o">
											<label>Anticipated date fit to fly</label>
										</div>
									</div>
									&nbsp;
                                                <div class="has-feedback" style="display: none;" id="divdateanticipated">
													<asp:TextBox runat="server" ID="txtdatefittofly" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; resize: none; padding-left: 5px;" onmousedown="datefittofly();"></asp:TextBox>
													<span id="Span1" runat="server" class="fa fa-calendar-o" style="margin-left: -22px"></span>
												</div>
								</div>
								<%-- =============================================== END HEADER SECTION =============================================== --%>
							</div>
						</div>
						<br />
						<div class="row" id="rowdetail" style="display: none">
							<%-- =============================================== DETAIL SECTION =============================================== --%>
							<div class="col-sm-4">
								<strong>Recommended Flight Seating Type</strong>
								<div style="margin-top: 5px; margin-bottom: 5px;">
									<div class="pretty p-default p-round">
										<asp:RadioButton runat="server" GroupName="SeatingType" Value="0" ID="rbseating1" />
										<div class="state p-primary-o">
											<label>Commercial flight regular seating</label>
										</div>
									</div>

								</div>
								<div style="margin-top: 5px; margin-bottom: 5px;">
									<div class="pretty p-default p-round">
										<asp:RadioButton runat="server" GroupName="SeatingType" Value="1" ID="rbseating2" />
										<div class="state p-primary-o">
											<label>Commercial flight Business class</label>
										</div>
									</div>
								</div>
								<div style="margin-top: 5px; margin-bottom: 5px;">
									<div class="pretty p-default p-round">
										<asp:RadioButton runat="server" GroupName="SeatingType" Value="2" ID="rbseating3" />
										<div class="state p-primary-o">
											<label>Stretcher Case</label>
										</div>
									</div>
								</div>
								<div style="margin-top: 5px; margin-bottom: 5px;">
									<div class="pretty p-default p-round">
										<asp:RadioButton runat="server" GroupName="SeatingType" Value="3" ID="rbseating4" />
										<div class="state p-primary-o">
											<label>Air-ambulance</label>
										</div>
									</div>
								</div>
							</div>

							<div class="col-sm-4">
								<strong>Escort Type</strong>
								<div style="margin-top: 5px; margin-bottom: 5px;">
									<div class="pretty p-default p-round">
										<asp:RadioButton runat="server" GroupName="EscortType" Value="0" ID="rbescort1" onclick="hidetext('txtFasting','rbpuasa2','dvPuasa')" />
										<div class="state p-primary-o">
											<label>Unescorted</label>
										</div>
									</div>

								</div>
								<div style="margin-top: 5px; margin-bottom: 5px;">
									<div class="pretty p-default p-round">
										<asp:RadioButton runat="server" GroupName="EscortType" Value="1" ID="rbescort2" onclick="ShowHideDiv6()" />
										<div class="state p-primary-o">
											<label>non-Medical Escort</label>
										</div>
									</div>
								</div>
								<div style="margin-top: 5px; margin-bottom: 5px;">
									<div class="pretty p-default p-round">
										<asp:RadioButton runat="server" GroupName="EscortType" Value="2" ID="rbescort3" onclick="ShowHideDiv6()" />
										<div class="state p-primary-o">
											<label>Medical Escort</label>
										</div>
									</div>
									&nbsp;
                                            <div class="has-feedback" style="display: inline;">
												<asp:DropDownList runat="server" ID="ddlescort" Width="100px">
													<asp:ListItem Value="Doctor">Doctor</asp:ListItem>
													<asp:ListItem Value="Nurse">Nurse</asp:ListItem>
												</asp:DropDownList>
											</div>
								</div>
							</div>

							<div class="col-sm-4">
								<strong>Special Needs<label class="subheader" style="font-size: 10px"> (mark only if needed)</label></strong>
								<div class="margin-cekbox" style="margin-top: 5px;">
									<div class="pretty p-icon p-curve">
										<asp:CheckBox runat="server" Value="0" ID="chkSpecialNeeds1" />
										<div class="state p-success">
											<i class="icon fa fa-check"></i>
											<label style="font-size: 12px">Wheel Chair Assistance</label>
										</div>
									</div>
								</div>
								<div class="margin-cekbox" style="margin-top: 5px;">
									<div class="pretty p-icon p-curve">
										<asp:CheckBox runat="server" Value="1" ID="chkSpecialNeeds2" />
										<div class="state p-success">
											<i class="icon fa fa-check"></i>
											<label style="font-size: 12px">Oxygen Supplementation</label>
										</div>
									</div>
								</div>
								<div class="margin-cekbox" style="margin-top: 5px;">
									<div class="pretty p-icon p-curve">
										<asp:CheckBox runat="server" Value="2" ID="chkSpecialNeeds3" />
										<div class="state p-success">
											<i class="icon fa fa-check"></i>
											<label style="font-size: 12px">Need Mechanical Ventilation</label>
										</div>
									</div>
								</div>
								<div class="margin-cekbox" style="margin-top: 5px;">
									<div class="pretty p-icon p-curve">
										<asp:CheckBox runat="server" Value="3" ID="chkSpecialNeeds4" />
										<div class="state p-success">
											<i class="icon fa fa-check"></i>
											<label style="font-size: 12px">Need Vacuum Mattress</label>
										</div>
									</div>
								</div>
							</div>
							<%-- =============================================== END DETAIL SECTION =============================================== --%>
						</div>

						<%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
					</div>
					<div class="text-right" style="padding-right: 15px; padding-bottom: 15px; padding-top: 15px;">
						<%--<div style="height: 22px; text-align: right;">
                            <asp:UpdateProgress ID="UpdateProgress18" runat="server" AssociatedUpdatePanelID="UpdatePanel10">
                                <ProgressTemplate>
                                    <div class="modal-backdrop" style="background-color: white; opacity: 0.3; text-align: center">
                                    </div>
                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                    &nbsp;
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <asp:Button CssClass="btn btn-default" runat="server" Text="Cancel" OnClientClick="return HidePreviewTravel();" />
                        <asp:Button runat="server" CssClass="btn btn-lightGreen" Text="Save" OnClientClick="SaveTravel()" />--%>
						<button class="btn btn-default" onclick="return HidePreviewTravel();">Cancel</button>
						<button class="btn btn-lightGreen" onclick="return SaveTravel();">Save</button>
					</div>
				</div>
			</div>

		</div>
		<%-- ========================================================== END Nutrition & Fasting  ================================================================ --%>

        <%-- ========================================================== Modal Referal  ================================================================ --%>
        <div class="modal fade" id="modal-referal">
	        <div class="modal-dialog modal-lg">
                <div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);" >
                    <div>
                        <asp:UpdateProgress ID="UpdateProgressModalreferal" runat="server" AssociatedUpdatePanelID="UPsaveReferal">
                            <ProgressTemplate>
                                <div class="modal-backdrop" style="background-color: white; opacity: 0.3; text-align: center">
                                </div>
                                <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                &nbsp;
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
			        <div class="modal-header" style="height: 35px; padding-top: 7px; padding-bottom: 5px">
				        <h4 class="modal-title"><asp:Label runat="server" Font-Bold="true" ID="modalReferalCaption" ClientIDMode="Static" Text="Rujukan"></asp:Label></h4>
                    </div>
		
			        <div class="modal-body">
                        <div class="header-pasien-FA">
                            <uc1:PatientCardModal runat="server" ID="PatientCardRefModal" />
                        </div>
                        <uc1:ModalReferal runat="server" ID="ModalReferal" />
			        </div>
			        <div class="modal-footer justify-content-right">
                        <asp:UpdatePanel ID="UPsaveReferal" runat="server">
                            <ContentTemplate>
                                 <asp:Button runat="server" ID="BtnHiddenReferal" ClientIDMode="Static" style="display:none" Onclick="BtnReferalHidden_Click"/>

				                <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                                <asp:Button ID="btnSaveReferal" CssClass="btn btn-lightGreen" runat="server"  Text="Save Form" OnClick="btnSaveReferal_Click"></asp:Button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
			        </div>
		        </div>
	        </div>
        </div>
       
        <%-- ========================================================== END Modal Referal  ================================================================ --%>

		<%-- ========================================================== Modal Hapus Referal  ================================================================ --%>
		<div class="modal fade" id="modal-delete-referal">
			<div class="modal-dialog modal-sm" style="top: 100px; width: 320px; height: 172px">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div>
						<asp:UpdateProgress ID="UpdateProgress18" runat="server" AssociatedUpdatePanelID="UPsaveReferal">
							<ProgressTemplate>
								<div class="modal-backdrop" style="background-color: white; opacity: 0.3; text-align: center">
								</div>
								<img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
								&nbsp;
							</ProgressTemplate>
						</asp:UpdateProgress>
					</div>
					<div class="modal-header" style="height: 35px; padding-top: 7px; padding-bottom: 5px">
						<h4 class="modal-title" style="text-align: center">
							<asp:Label runat="server" Font-Bold="true" ID="Label21" ClientIDMode="Static" Text="Hapus Semua Rujukan"></asp:Label></h4>
					</div>

					<div class="modal-body">
						<div class="">
							<h5 style="text-align: center">Apakah Anda yakin menghapus semua surat rujukan</h5>
						</div>
					</div>
					<div class="modal-footer justify-content-right">
						<asp:UpdatePanel ID="UpdatePanel4" runat="server">
							<ContentTemplate>
	
								<button class="btncancelrujukan" data-dismiss="modal" aria-hidden="true">Close</button>
								<asp:LinkButton ID="BtnModalDeleteallReferal" CssClass="btndelrujukan" Style="margin-left: 23px" runat="server"  OnClick="BtnDeleteAllReferral_Click" Text="Delete All" ></asp:LinkButton>
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>
		</div>
		<%-- ========================================================== END Modal Hapus Referal  ================================================================ --%>

		<%-- ========================================================== Modal Rawat Inap  ================================================================ --%>
		<div class="modal fade" id="modal-rawatinap">
			<div class="modal-dialog modal-lg">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div>
						<asp:UpdateProgress ID="UpdateProgress19" runat="server" AssociatedUpdatePanelID="UPsaveReferal">
							<ProgressTemplate>
								<div class="modal-backdrop" style="background-color: white; opacity: 0.3; text-align: center">
								</div>
								<img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
								&nbsp;
							</ProgressTemplate>
						</asp:UpdateProgress>
					</div>
					<div class="modal-header" style="height: 35px; padding-top: 7px; padding-bottom: 5px">
						<h4 class="modal-title">
							<asp:Label runat="server" Font-Bold="true" ID="Label20" ClientIDMode="Static" Text="Rujukan"></asp:Label></h4>
					</div>

					<div class="modal-body">
						<div class="header-pasien-FA">
							<uc1:PatientCardModal runat="server" ID="PatientCardRawatinapModal" />
						</div>
						<uc1:ModalRawatInap runat="server" ID="ModalRawatInap" />
					</div>
					<div class="modal-footer justify-content-right">
						<asp:UpdatePanel ID="UpdatePanel6" runat="server">
							<ContentTemplate>


								<button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
								<asp:Button ID="BtnSaveRawatInap" CssClass="btn btn-lightGreen" runat="server" Text="Save"  OnClick="BtnSaveRawatinap_Click"></asp:Button>
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>
		</div>
		<%-- ========================================================== END Modal Rawat Inap  ================================================================ --%>
		<%-- ========================================================== Modal Hapus Rawat Inap  ================================================================ --%>
		<div class="modal fade" id="modal-delete-rawatinap">
			<div class="modal-dialog modal-sm" style="top: 100px; width: 320px; height: 172px">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div>
						<asp:UpdateProgress ID="UpdateProgress20" runat="server" AssociatedUpdatePanelID="UPsaveReferal">
							<ProgressTemplate>
								<div class="modal-backdrop" style="background-color: white; opacity: 0.3; text-align: center">
								</div>
								<img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
								&nbsp;
							</ProgressTemplate>
						</asp:UpdateProgress>
					</div>
					<div class="modal-header" style="height: 35px; padding-top: 7px; padding-bottom: 5px">
						<h4 class="modal-title" style="text-align: center">
							<asp:Label runat="server" Font-Bold="true" ID="Label22" ClientIDMode="Static" Text="Hapus Semua Rujukan"></asp:Label></h4>
					</div>

					<div class="modal-body">
						<div class="">
							<h5 style="text-align: center">Apakah Anda yakin menghapus semua surat Rawat Inap?</h5>
						</div>
					</div>
					<div class="modal-footer justify-content-right">
						<asp:UpdatePanel ID="UpdatePanel7" runat="server">
							<ContentTemplate>

								<button class="btncancelrujukan" data-dismiss="modal" aria-hidden="true">Close</button>
								<asp:Button ID="BtnDeleteRawatInap" CssClass="btndelrujukan" Style="margin-left: 23px" runat="server" OnClick="BtnDeleteRawatInap_Click" Text="Delete All"></asp:Button>
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>
		</div>
		<%-- ========================================================== END Modal Hapus Referal  ================================================================ --%>


        <%-- ========================================================== Modal Template S  ================================================================ --%>
        <div class="modal fade" id="modalTemplateS" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="margin-top: 5%; width: 75%;">
                <div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
                   <%-- <asp:UpdatePanel ID="UpdatePanelmodalS" runat="server">
                        <ContentTemplate>--%>
					<div class="modal-header" style="height: 35px; padding-top: 5px; padding-bottom: 5px">
						<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
						<h5 class="modal-title">
							<asp:Label ID="Label12" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Template Subjective - Chief Complaint">
							</asp:Label></h5>
					</div>
					<div class="modal-body" style="padding-bottom: 0px;">
						<%--<asp:UpdatePanel runat="server" ID="UpdatePanelModalbodyS">
                                    <ContentTemplate>--%>
						<div class="row">
							<div class="col-sm-12" style="padding-bottom: 10px;">
								<asp:TextBox ID="TextBoxCari_Template_SCC" runat="server" Style="width: 200px;" placeholder="Search Keyword..." onkeyup="Search_Data(this,'HFcounteritemSCC','colSCC')"></asp:TextBox>
								<span class="fa fa-search" style="padding-right: 5px; color: darkgrey; margin-left: -20px;">
							</div>
							<div class="col-sm-6">
								<div class="kotak-template-modal scrollEMR">
									<asp:Repeater ID="RepeaterSCC" runat="server">
										<ItemTemplate>
											<div class="col-lg-4 no-padding" id="colSCC<%#(((RepeaterItem)Container).ItemIndex).ToString()%>">
												<a href="javascript:copytextTemplate('TextBox_Template_SCC','<%# Eval("template_remarks").ToString().Replace("\n", "\\n") %>');">
													<div class="item-template-modal" title='<%# Eval("template_remarks").ToString() %>'>
														<b>
															<asp:Label ID="LabelheaderSCC" runat="server" Text='<%# Eval("template_name") %>'></asp:Label></b><br />
														<asp:Label ID="LabeldetailSCC" runat="server" Text='<%# Eval("template_remarks").ToString().Replace("\n", "<br />") %>'></asp:Label>
													</div>
												</a>
											</div>
										</ItemTemplate>
									</asp:Repeater>
									<asp:HiddenField ID="HFcounteritemSCC" runat="server" />
								</div>
							</div>
							<div class="col-sm-6">
								<asp:TextBox ID="TextBox_Template_SCC" runat="server" placeholder="Type here..." TextMode="MultiLine" Rows="15" Style="width: 100%; max-width: 100%;" CssClass="scrollEMR"></asp:TextBox>
							</div>
						</div>
						<%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
					</div>
					<div class="text-right" style="padding-right: 15px; padding-bottom: 15px; padding-top: 15px;">
						<a class="btn btn-default" href="javascript:clearTemplateSOAP('TextBox_Template_SCC'); $('#modalTemplateS').modal('hide');">Cancel</a>
						<a class="btn btn-lightGreen" href="javascript:copytextTemplatetoSOAP('TextBox_Template_SCC','Complaint'); $('#modalTemplateS').modal('hide');">Save</a>
					</div>
					<%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
				</div>
			</div>
		</div>

		<div class="modal fade" id="modalTemplateSAnam" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-dialog" style="margin-top: 5%; width: 75%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<%--<asp:UpdatePanel ID="UpdatePanelmodalSAnam" runat="server">
                        <ContentTemplate>--%>
					<div class="modal-header" style="height: 35px; padding-top: 5px; padding-bottom: 5px">
						<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
						<h5 class="modal-title">
							<asp:Label ID="LabelSAnam" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Template Subjective - Anamnesis">
							</asp:Label></h5>
					</div>
					<div class="modal-body" style="padding-bottom: 0px;">
						<%--<asp:UpdatePanel runat="server" ID="UpdatePanelModalbodySAnam">
                                    <ContentTemplate>--%>
						<div class="row">
							<div class="col-sm-12" style="padding-bottom: 10px;">
								<asp:TextBox ID="TextBoxCari_Template_SA" runat="server" Style="width: 200px;" placeholder="Search Keyword..." onkeyup="Search_Data(this,'HFcounteritemSA','colSA')"></asp:TextBox>
								<span class="fa fa-search" style="padding-right: 5px; color: darkgrey; margin-left: -20px;">
							</div>
							<div class="col-sm-6">
								<div class="kotak-template-modal scrollEMR">
									<asp:Repeater ID="RepeaterSA" runat="server">
										<ItemTemplate>
											<div class="col-lg-4 no-padding" id="colSA<%#(((RepeaterItem)Container).ItemIndex).ToString()%>">
												<a href="javascript:copytextTemplate('TextBox_Template_SA','<%# Eval("template_remarks").ToString().Replace("\n", "\\n") %>');">
													<div class="item-template-modal" title='<%# Eval("template_remarks").ToString() %>'>
														<b>
															<asp:Label ID="LabelheaderSA" runat="server" Text='<%# Eval("template_name") %>'></asp:Label></b><br />
														<asp:Label ID="LabeldetailSA" runat="server" Text='<%# Eval("template_remarks").ToString().Replace("\n", "<br />") %>'></asp:Label>
													</div>
												</a>
											</div>
										</ItemTemplate>
									</asp:Repeater>
									<asp:HiddenField ID="HFcounteritemSA" runat="server" />
								</div>
							</div>
							<div class="col-sm-6">
								<asp:TextBox ID="TextBox_Template_SA" runat="server" placeholder="Type here..." TextMode="MultiLine" Rows="15" Style="width: 100%; max-width: 100%;" CssClass="scrollEMR"></asp:TextBox>
							</div>
						</div>
						<%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
					</div>
					<div class="text-right" style="padding-right: 15px; padding-bottom: 15px; padding-top: 15px;">
						<a class="btn btn-default" href="javascript:clearTemplateSOAP('TextBox_Template_SA'); $('#modalTemplateSAnam').modal('hide');">Cancel</a>
						<a class="btn btn-lightGreen" href="javascript:copytextTemplatetoSOAP('TextBox_Template_SA','Anamnesis'); $('#modalTemplateSAnam').modal('hide');">Save</a>
					</div>
					<%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
				</div>
			</div>
		</div>
		<%-- ========================================================== END Template S  ================================================================ --%>

		<%-- ========================================================== Modal Template A  ================================================================ --%>
		<div class="modal fade" id="modalTemplateA" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-dialog" style="margin-top: 5%; width: 75%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<%--<asp:UpdatePanel ID="UpdatePanelmodalA" runat="server">
                        <ContentTemplate>--%>
					<div class="modal-header" style="height: 35px; padding-top: 5px; padding-bottom: 5px">
						<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
						<h5 class="modal-title">
							<asp:Label ID="LabelA" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Template Assessment">
							</asp:Label></h5>
					</div>
					<div class="modal-body" style="padding-bottom: 0px;">
						<%--<asp:UpdatePanel runat="server" ID="UpdatePanelModalbodyA">
                                    <ContentTemplate>--%>
						<div class="row">
							<div class="col-sm-12" style="padding-bottom: 10px;">
								<asp:TextBox ID="TextBoxCari_Template_A" runat="server" Style="width: 200px;" placeholder="Search Keyword..." onkeyup="Search_Data(this,'HFcounteritemA','colA')"></asp:TextBox>
								<span class="fa fa-search" style="padding-right: 5px; color: darkgrey; margin-left: -20px;">
							</div>
							<div class="col-sm-6">
								<div class="kotak-template-modal scrollEMR">
									<asp:Repeater ID="RepeaterA" runat="server">
										<ItemTemplate>
											<div class="col-lg-4 no-padding" id="colA<%#(((RepeaterItem)Container).ItemIndex).ToString()%>">
												<a href="javascript:copytextTemplate('TextBox_Template_A','<%# Eval("template_remarks").ToString().Replace("\n", "\\n") %>');">
													<div class="item-template-modal" title='<%# Eval("template_remarks").ToString() %>'>
														<b>
															<asp:Label ID="LabelheaderA" runat="server" Text='<%# Eval("template_name") %>'></asp:Label></b><br />
														<asp:Label ID="LabeldetailA" runat="server" Text='<%# Eval("template_remarks").ToString().Replace("\n", "<br />") %>'></asp:Label>
													</div>
												</a>
											</div>
										</ItemTemplate>
									</asp:Repeater>
									<asp:HiddenField ID="HFcounteritemA" runat="server" />
								</div>
							</div>
							<div class="col-sm-6">
								<asp:TextBox ID="TextBox_Template_A" runat="server" placeholder="Type here..." TextMode="MultiLine" Rows="15" Style="width: 100%; max-width: 100%;" CssClass="scrollEMR"></asp:TextBox>
							</div>
						</div>
						<%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
					</div>
					<div class="text-right" style="padding-right: 15px; padding-bottom: 15px; padding-top: 15px;">
						<a class="btn btn-default" href="javascript:clearTemplateSOAP('TextBox_Template_A'); $('#modalTemplateA').modal('hide');">Cancel</a>
						<a class="btn btn-lightGreen" href="javascript:copytextTemplatetoSOAP('TextBox_Template_A','txtPrimary'); $('#modalTemplateA').modal('hide'); copytextto();">Save</a>
					</div>
					<%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
				</div>
			</div>
		</div>
		<%-- ========================================================== END Template A  ================================================================ --%>

		<%-- ========================================================== Modal Template P  ================================================================ --%>
		<div class="modal fade" id="modalTemplateP" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-dialog" style="margin-top: 5%; width: 75%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<%--<asp:UpdatePanel ID="UpdatePanelmodalP" runat="server">
                        <ContentTemplate>--%>
					<div class="modal-header" style="height: 35px; padding-top: 5px; padding-bottom: 5px">
						<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
						<h5 class="modal-title">
							<asp:Label ID="LabelP" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Template Planning">
							</asp:Label></h5>
					</div>
					<div class="modal-body" style="padding-bottom: 0px;">
						<%--<asp:UpdatePanel runat="server" ID="UpdatePanelModalbodyP">
                                    <ContentTemplate>--%>
						<div class="row">
							<div class="col-sm-12" style="padding-bottom: 10px;">
								<asp:TextBox ID="TextBoxCari_Template_P" runat="server" Style="width: 200px;" placeholder="Search Keyword..." onkeyup="Search_Data(this,'HFcounteritemP','colP')"></asp:TextBox>
								<span class="fa fa-search" style="padding-right: 5px; color: darkgrey; margin-left: -20px;">
							</div>
							<div class="col-sm-6">
								<div class="kotak-template-modal scrollEMR">
									<asp:Repeater ID="RepeaterP" runat="server">
										<ItemTemplate>
											<div class="col-lg-4 no-padding" id="colP<%#(((RepeaterItem)Container).ItemIndex).ToString()%>">
												<a href="javascript:copytextTemplate('TextBox_Template_P','<%# Eval("template_remarks").ToString().Replace("\n", "\\n") %>');">
													<div class="item-template-modal" title='<%# Eval("template_remarks").ToString() %>'>
														<b>
															<asp:Label ID="LabelheaderP" runat="server" Text='<%# Eval("template_name") %>'></asp:Label></b><br />
														<asp:Label ID="LabeldetailP" runat="server" Text='<%# Eval("template_remarks").ToString().Replace("\n", "<br />") %>'></asp:Label>
													</div>
												</a>
											</div>
										</ItemTemplate>
									</asp:Repeater>
									<asp:HiddenField ID="HFcounteritemP" runat="server" />
								</div>
							</div>
							<div class="col-sm-6">
								<asp:TextBox ID="TextBox_Template_P" runat="server" placeholder="Type here..." TextMode="MultiLine" Rows="15" Style="width: 100%; max-width: 100%;" CssClass="scrollEMR"></asp:TextBox>
							</div>
						</div>
						<%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
					</div>
					<div class="text-right" style="padding-right: 15px; padding-bottom: 15px; padding-top: 15px;">
						<a class="btn btn-default" href="javascript:clearTemplateSOAP('TextBox_Template_P'); $('#modalTemplateP').modal('hide');">Cancel</a>
						<a class="btn btn-lightGreen" href="javascript:copytextTemplatetoSOAP('TextBox_Template_P','txtPlanning'); $('#modalTemplateP').modal('hide');">Save</a>
					</div>
					<%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
				</div>
			</div>
		</div>
		<%-- ========================================================== END Template P  ================================================================ --%>

		<%-- ========================================================== Modal Template O  ================================================================ --%>
		<div class="modal fade" id="modalTemplateO" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-dialog" style="margin-top: 3%; width: 75%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div class="modal-header" style="height: 35px; padding-top: 5px; padding-bottom: 5px">
						<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
						<%--  <table style="width: 100%;">
                            <tr>
                                <td style="width: 50%;">--%>
						<h5 class="modal-title">
							<asp:Label ID="Label10" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Template Objective">
                            <%--<label id="lblbhs_templateO"> Template Objective </label>--%>
							</asp:Label></h5>
						<%-- </td>
                                <td style="width: 50%; text-align: right;">
                                    <a style="height: 25px; padding-top: 2px;" class="btn btn-default" href="javascript:$('#modalTemplateO').modal('hide');">Cancel</a>
                                    <a style="height: 25px; padding-top: 2px;" class="btn btn-lightGreen" href="javascript:SetTemplateToObjective();">Save</a>
                                </td>
                            </tr>
                        </table>--%>
					</div>
					<div class="modal-body" style="padding-bottom: 0px;">
						<%--<asp:UpdatePanel ID="UpdatePanelmodalO" runat="server">
                            <ContentTemplate>--%>
						<ul class="nav nav-tabs" style="margin-bottom: 15px;">
							<li class="active"><a data-toggle="tab" href="#templateOsatu">Template Standar</a></li>
							<li><a data-toggle="tab" href="#templateOdua">Template User</a></li>
						</ul>
						<div class="tab-content">
							<div id="templateOsatu" class="tab-pane fade in active">
								<div style="text-align: center;">
									<a class="btn btn-yellowgreen btn-sm" href="javascript:templatenormal();">All <b>Normal</b></a>
									<a class="btn btn-default btn-sm" href="javascript:clearnormal()">Reset</a>
								</div>

								<%-- ========================== UMUM ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										Keadaan Umum
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtumum" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
										<br />
										<div style="padding-top: 2px;">
											<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtumum','Baik');">Baik</a>
											<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtumum','Sadar');">Sadar</a>
											<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtumum','Tampak Sakit(sedang)');">Tampak Sakit(sedang)</a>
											<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtumum','Tampak Sakit(berat)');">Tampak Sakit(berat)</a>
										</div>
									</div>
								</div>
								<%-- ========================== UMUM ================================== --%>

								<%-- ========================== KULIT ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										Kulit
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtkulit" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-2">
										&nbsp;
									</div>
									<div class="col-sm-10" style="padding-top: 2px;">
										<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtkulit','Normal');">Normal</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtkulit','Ikterik');">Ikterik</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtkulit','Ruam');">Ruam</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtkulit','Lokasi');">Lokasi</a>
									</div>
								</div>
								<%-- ========================== KULIT ================================== --%>

								<%-- ========================== KEPALA ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										Kepala
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtkepala" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-2">
									</div>
									<div class="col-sm-10" style="padding-top: 2px;">
										<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtkepala','Normal');">Normal</a>
									</div>
								</div>
								<%-- ========================== KEPALA ================================== --%>

								<%-- ========================== MATA ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										Mata
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtmata" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-2">
									</div>
									<div class="col-sm-10" style="padding-top: 2px;">
										<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtmata','Normal');">Normal</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtmata','Anemis');">Anemis</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtmata','Ikterik');">Ikterik</a>
									</div>
								</div>
								<%-- ========================== MATA ================================== --%>

								<%-- ========================== THT ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										THT
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtTHT" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-2">
									</div>
									<div class="col-sm-10" style="padding-top: 2px;">
										<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtTHT','Normal');">Normal</a>
									</div>
								</div>
								<%-- ========================== THT ================================== --%>

								<%-- ========================== LEHER ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										Leher
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtLeher" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-2">
									</div>
									<div class="col-sm-10" style="padding-top: 2px;">
										<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtLeher','Normal');">Normal</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtLeher','Kaku Kuduk');">Kaku Kuduk</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtLeher','Pembesaran KGB');">Pembesaran KGB</a>
									</div>
								</div>
								<%-- ========================== LEHER ================================== --%>

								<%-- ========================== Thorax ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										Thorax
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtThorax" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-2">
									</div>
									<div class="col-sm-10" style="padding-top: 2px;">
										<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtThorax','Normal');">Normal</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtThorax','Simetris');">Simetris</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtThorax','Asimetris');">Asimetris</a>
									</div>
								</div>
								<%-- ========================== Thorax ================================== --%>

								<%-- ========================== COR ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										-Cor
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtCOr" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-2">
									</div>
									<div class="col-sm-10" style="padding-top: 2px;">
										<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtCOr','Normal');">Normal</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtCOr','Murmur');">Murmur</a>
									</div>
								</div>
								<%-- ========================== COR ================================== --%>

								<%-- ========================== PULMA ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										-Pulmo
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtPulma" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-2">
									</div>
									<div class="col-sm-10" style="padding-top: 2px;">
										<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtPulma','Normal');">Normal</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtPulma','Ronchi');">Ronchi</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtPulma','Wheezing');">Wheezing</a>
									</div>
								</div>
								<%-- ========================== PULMA ================================== --%>

								<%-- ========================== ABDOMNEN ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										Abdomen
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtAbdomen" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-2">
									</div>
									<div class="col-sm-10" style="padding-top: 2px;">
										<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtAbdomen','Normal');">Normal</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtAbdomen','Distensi');">Distensi</a>
									</div>
								</div>
								<%-- ========================== ABDOMNEN ================================== --%>

								<%-- ========================== HEPAR ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										-Hepar
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtHepar" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-2">
									</div>
									<div class="col-sm-10" style="padding-top: 2px;">
										<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtHepar','Normal');">Normal</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtHepar','Membesar');">Membesar</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtHepar','Nyeri Tekan');">Nyeri Tekan</a>
									</div>
								</div>
								<%-- ========================== HEPAR ================================== --%>

								<%-- ========================== LIEN ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										-Lien
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtLien" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-2">
									</div>
									<div class="col-sm-10" style="padding-top: 2px;">
										<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtLien','Normal');">Normal</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtLien','Membesar');">Membesar</a>
									</div>
								</div>
								<%-- ========================== LIEN ================================== --%>

								<%-- ========================== Ekstremitas ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										Ekstremitas
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtEkstremitas" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-2">
									</div>
									<div class="col-sm-10" style="padding-top: 2px;">
										<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtEkstremitas','Normal');">Normal</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtEkstremitas','Membesar');">Membesar</a>
										<a class="btn btn-default badge" style="color: #444;" href="javascript:copytext('txtEkstremitas','Oedema');">Oedema</a>
									</div>
								</div>
								<%-- ========================== Ekstremitas ================================== --%>

								<%-- ========================== GENETALIA ================================== --%>
								<div class="row" style="margin-top: 10px">
									<div class="col-sm-2 objective-tempplate-title">
										Genetalia
									</div>
									<div class="col-sm-10">
										<asp:TextBox runat="server" ID="txtGenetalia" Style="width: 100%" onkeypress="return checkenter();"></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-2">
									</div>
									<div class="col-sm-10" style="padding-top: 2px;">
										<a class="btn btn-yellowgreen badge" style="color: #444;" href="javascript:copytext('txtGenetalia','Normal');">Normal</a>
									</div>
								</div>
								<%-- ========================== Ekstremitas ================================== --%>

								<div class="text-right" style="padding-right: 15px; padding-bottom: 15px; padding-top: 15px;">
									<a class="btn btn-default" href="javascript:$('#modalTemplateO').modal('hide');">Cancel</a>
									<a class="btn btn-lightGreen" href="javascript:SetTemplateToObjective();">Save</a>
								</div>
							</div>
							<div id="templateOdua" class="tab-pane fade">
								<div class="row">
									<div class="col-sm-12" style="padding-bottom: 10px;">
										<asp:TextBox ID="TextBoxCari_Template_O" runat="server" Style="width: 200px;" placeholder="Search Keyword..." onkeyup="Search_Data(this,'HFcounteritemO','colO')"></asp:TextBox>
										<span class="fa fa-search" style="padding-right: 5px; color: darkgrey; margin-left: -20px;">
									</div>
									<div class="col-sm-6">
										<div class="kotak-template-modal scrollEMR">
											<asp:Repeater ID="RepeaterO" runat="server">
												<ItemTemplate>
													<div class="col-lg-4 no-padding" id="colO<%#(((RepeaterItem)Container).ItemIndex).ToString()%>">
														<a href="javascript:copytextTemplate('TextBox_Template_O','<%# Eval("template_remarks").ToString().Replace("\n", "\\n") %>');">
															<div class="item-template-modal" title='<%# Eval("template_remarks").ToString() %>'>
																<b>
																	<asp:Label ID="LabelheaderO" runat="server" Text='<%# Eval("template_name") %>'></asp:Label></b><br />
																<asp:Label ID="LabeldetailO" runat="server" Text='<%# Eval("template_remarks").ToString().Replace("\n", "<br />") %>'></asp:Label>
															</div>
														</a>
													</div>
												</ItemTemplate>
											</asp:Repeater>
											<asp:HiddenField ID="HFcounteritemO" runat="server" />
										</div>
									</div>
									<div class="col-sm-6">
										<asp:TextBox ID="TextBox_Template_O" runat="server" placeholder="Type here..." TextMode="MultiLine" Rows="15" Style="width: 100%; max-width: 100%;" CssClass="scrollEMR"></asp:TextBox>
									</div>
								</div>
								<div class="text-right" style="padding-right: 15px; padding-bottom: 15px; padding-top: 15px;">
									<a class="btn btn-default" href="javascript:clearTemplateSOAP('TextBox_Template_O'); $('#modalTemplateO').modal('hide');">Cancel</a>
									<a class="btn btn-lightGreen" href="javascript:copytextTemplatetoSOAP('TextBox_Template_O','txtOthers'); $('#modalTemplateO').modal('hide');">Save</a>
								</div>
							</div>
						</div>
						<%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
					</div>
				</div>
			</div>
		</div>
		<%-- ========================================================== END Template O  ================================================================ --%>


		<%-- ========================================================== modalPreview ================================================================ --%>
		<div class="modal fade" id="modalPreview" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-dialog" style="width: 80%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
						<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
						<h4 class="modal-title" style="text-align: left">
							<asp:Label ID="lblModalTitle" Style="font-weight: bold; font-size: 14px;" runat="server" Text="Medical Resume"></asp:Label></h4>
					</div>
					<div style="text-align: right; padding-right: 35px; background-color: lightgray;">
						<asp:HyperLink runat="server" ID="email" CssClass="btn btn-primary hidden" NavigateUrl="~/Form/SOAP/PreviewTemplate/EmailSoap.aspx" Target="_blank">Send as Email</asp:HyperLink>
						<asp:HyperLink runat="server" ID="preview" CssClass="btn btn-github" Target="_blank"><i class="fa fa-print"></i> Print / Save <i class="fa fa-save"></i> </asp:HyperLink>
					</div>
					<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div style="overflow-y: auto; width: 100%; padding-left: 20px; padding-right: 10px;">
								<asp:Button runat="server" CssClass="hidden" ID="btnPreview" OnClick="btnPreview_click" />
								<%--<uc1:Preview runat="server" ID="SoapPagePreview" Visible="false" />--%>
								<iframe name="IframeMedicalResumePatient" id="IframeMedicalResumePatient" runat="server" style="width: 100%; height: 80vh; border: none; margin-top: 0%; overflow-y: scroll; padding-right: 0; padding-left: 0%; margin-left: 0;"></iframe>
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
					<br />
				</div>
			</div>
		</div>
		<%-- ========================================================== modalPreview ================================================================ --%>

		<%-- ========================================================== MODAL COPY SOAP ================================================================ --%>
		<div class="modal fade" id="modalCopySOAP" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-dialog" style="top: 2%; width: 80%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
						<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
						<h5 class="modal-title">
							<asp:Label ID="Label3" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Copy SOAP"></asp:Label></h5>
					</div>
					<div class="modal-body">
						<asp:UpdatePanel ID="UpdatePanelModalCopySoap" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<div class="row">
									<div class="col-sm-3">
										<asp:HiddenField ID="hfsoapcopystring" runat="server" />
										<asp:HiddenField ID="hfheader" runat="server" />
										<asp:HiddenField ID="hfallergy" runat="server" />
										<asp:HiddenField ID="hfroutine" runat="server" />
										<asp:HiddenField ID="HiddenLastModif" runat="server" />
									</div>
									<div class="col-sm-6" style="padding-top: 12px">
										<label style="font-weight: bold; font-size: 14px">Preview</label>
									</div>
									<div class="col-sm-3" style="text-align: right">
										<asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<a href="javascript:confirmCopySOAP();" class="btn btn-lightGreen" id="hrefbtncopy" style="color: white; width: 176px; height: 32px; padding-top: 4px; border-radius: 4px; background-color: #4d9b35;">Copy SOAP</a>
												<asp:Button runat="server" CssClass="btn btn-lightGreen" ID="btncopy" Text="Copy SOAP" Style="color: white; width: 176px; height: 32px; padding-top: 4px; border-radius: 4px; background-color: #4d9b35; display: none;" OnClick="btnCopySOAP_onClick" />
											</ContentTemplate>
										</asp:UpdatePanel>
									</div>
									<asp:UpdateProgress ID="prog_copysoap" runat="server" AssociatedUpdatePanelID="UpdatePanel12">
										<ProgressTemplate>
											<div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
											</div>
											<div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
												<img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
											</div>
										</ProgressTemplate>
									</asp:UpdateProgress>
								</div>
								<div class="row" style="padding-top: 5px">
									<div class="col-sm-3" style="padding-right: 0px; padding-left: 0px;">
										<asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<div class="col-sm-12" style="padding-right: 0px;">
													<label style="font-size: 10px">Doctor</label>
													<br />
													<asp:Button runat="server" ID="btnsearchDoctor" Visible="false" OnClick="txtsearchDoctor_onChange" />
													<div class="has-feedback">
														<asp:TextBox runat="server" ID="txtSearchDoctor" AutoPostBack="true" onkeypress="keypressdoctor()" placeholder="Search..." Style="width: 100%; max-width: 100%; height: 32px; padding-left: 5px" OnTextChanged="txtsearchDoctor_onChange"></asp:TextBox>
														<span class="form-control-feedback" style="margin-top: -1px; margin-right: 15px;">
															<asp:UpdateProgress ID="prog_finddoc_cs" runat="server" AssociatedUpdatePanelID="UpdatePanel11">
																<ProgressTemplate>
																	<asp:Image ID="Image1" runat="server" Style="width: 40px; background-color: transparent;" ImageUrl="~/Images/Background/small-loader.gif" />
																</ProgressTemplate>
															</asp:UpdateProgress>
														</span>
													</div>
												</div>
												<div class="col-sm-12" style="padding-top: 10px; padding-right: 0px">
													<asp:GridView ID="gvw_doctor" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" BorderColor="Black"
														HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center" BorderWidth="0"
														ShowHeaderWhenEmpty="True" DataKeyNames="EncounterId" EmptyDataText="No Data" ShowHeader="false"
														AllowSorting="True">
														<PagerStyle CssClass="pagination-ys" />
														<Columns>
															<asp:TemplateField HeaderText="" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-BorderWidth="1" ItemStyle-BorderColor="#bbbfd4">
																<ItemTemplate>
																	<asp:LinkButton CssClass="stylink" ID="copyitem" runat="server" OnClick="btngetitem_onClick">
                                                                        <div style="width:100%">
                                                                        <%# Eval("AdmissionDate") %>&nbsp<%# Eval("AdmissionNo") %><br />
                                                                        <%# Eval("DoctorName") %><br />
                                                                        <%# Eval("SpecialtyName") %>
                                                                        </div>
																	</asp:LinkButton>
																	<asp:HiddenField ID="hfcopyEncounterId" Value='<%# Bind("EncounterId") %>' runat="server" />
																	<asp:HiddenField ID="hfcopyPatientId" Value='<%# Bind("PatientId") %>' runat="server" />
																	<asp:HiddenField ID="hfcopyOrganizationId" Value='<%# Bind("OrganizationId") %>' runat="server" />
																	<asp:HiddenField ID="hfcopyAdmissionId" Value='<%# Bind("AdmissionId") %>' runat="server" />
																	<asp:HiddenField ID="hfcopyAdmissionNo" Value='<%# Bind("AdmissionNo") %>' runat="server" />
																	<asp:HiddenField ID="hfcopyAdmissionDate" Value='<%# Bind("AdmissionDate") %>' runat="server" />
																	<asp:HiddenField ID="hfcopyDoctorId" Value='<%# Bind("DoctorId") %>' runat="server" />
																	<asp:HiddenField ID="hfcopyDoctorName" Value='<%# Bind("DoctorName") %>' runat="server" />
																	<asp:HiddenField ID="hfcopySpecialtyId" Value='<%# Bind("SpecialtyId") %>' runat="server" />
																	<asp:HiddenField ID="hfcopySpecialtyName" Value='<%# Bind("SpecialtyName") %>' runat="server" />
																	<%--<asp:Label id="itemlist" runat="server" Text='<%# Bind("item_list") %>' Font-Size="9px" Font-Names="Helvetica, Arial, sans-serif"></asp:Label>--%>
																</ItemTemplate>
															</asp:TemplateField>
															<%--<asp:BoundField HeaderText="activeIngredientsName" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="activeIngredientsName"  SortExpression="activeIngredientsName"></asp:BoundField>
                                                            <asp:BoundField HeaderText="totalQuantity" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" DataField="totalQuantity"  SortExpression="totalQuantity"></asp:BoundField>--%>
														</Columns>
													</asp:GridView>
													<asp:HiddenField ID="HF_copysoap_oldrow" runat="server" />
												</div>
											</ContentTemplate>
										</asp:UpdatePanel>
									</div>

									<div class="col-sm-9" style="transform: translate(0,0);">
										<div id="divblokcopysoap" runat="server" visible="true">
											<div class="modal-backdrop" style="background-color: #f4f4f4; opacity: 0; text-align: center; cursor: not-allowed;"></div>
										</div>
										<div style="border: 1px solid #707070; padding: 10px; margin-top: 5px;">
											<div style="text-align: left; padding-bottom: 10px;">
												<a href="javascript:checkCopySOAP();" class="btn btn-sm btn-default">Select All</a>
												<a href="javascript:uncheckCopySOAP();" class="btn btn-sm btn-default">Deselect All</a>
												<asp:Label ID="LabelDisclaimerCopy" runat="server" Text="*Dokter hanya dapat melakukan copy resep yang diberikan oleh dokter lain" Visible="false" Style="color: red; font-weight: bold; float: right;"></asp:Label>
											</div>
											<div class="divider-bottom">
												<div class="pretty p-icon p-curve">
													<asp:CheckBox runat="server" ID="chkSubjective" />
													<div class="state p-success">
														<i class="icon fa fa-check"></i>
														<label style="font-size: 12px; font-weight: bold; margin-left: 10px;">SUBJECTIVE</label>
													</div>
												</div>
												<br />
												<div>
													<label style="padding-left: 30px">Chief Complaint : </label>
													<asp:Label runat="server" Style="padding-left: 5px;" ID="lblChiefComplaint" Text=""></asp:Label>
													<br />
												</div>
												<label style="padding-left: 30px">Anamnesis : </label>
												<asp:Label runat="server" Style="padding-left: 5px;" ID="lblAnamnesis" Text=""></asp:Label>
											</div>
											<div style="padding-top: 10px" class="divider-bottom">
												<div class="pretty p-icon p-curve">
													<asp:CheckBox runat="server" ID="chkObjective" />
													<div class="state p-success">
														<i class="icon fa fa-check"></i>
														<label style="font-size: 12px; font-weight: bold; margin-left: 10px;">OBJECTIVE</label>
													</div>
												</div>
												<br />
												<asp:Label runat="server" Style="padding-left: 30px" ID="lblobjective" Text=""></asp:Label>
											</div>
											<div style="padding-top: 10px" class="divider-bottom">
												<div class="pretty p-icon p-curve">
													<asp:CheckBox runat="server" ID="chkAssessment" />
													<div class="state p-success">
														<i class="icon fa fa-check"></i>
														<label style="font-size: 12px; font-weight: bold; margin-left: 10px;">ASSESSMENT</label>
													</div>
												</div>
												<br />
												<asp:Label runat="server" Style="padding-left: 30px" ID="lblAssessment" Text=""></asp:Label>
											</div>
											<div style="padding-top: 10px" class="divider-bottom">
												<div class="pretty p-icon p-curve">
													<asp:CheckBox runat="server" ID="chkPlanning" />
													<div class="state p-success">
														<i class="icon fa fa-check"></i>
														<label style="font-size: 12px; font-weight: bold; margin-left: 10px;">PLANNING & PROCEDURE</label>
													</div>
												</div>
												<br />
												<asp:Label runat="server" Style="padding-left: 30px" ID="lblPlanning" Text=""></asp:Label>
											</div>
											<div style="padding-top: 10px" class="divider-bottom">
												<div class="pretty p-icon p-curve">
													<asp:CheckBox runat="server" ID="chkLab" />
													<div class="state p-success">
														<i class="icon fa fa-check"></i>
														<label style="font-size: 12px; font-weight: bold; margin-left: 10px;">
															LABORATORY
														</label>
													</div>
												</div>
												<ul style="padding-left: 45px">
													<asp:Repeater runat="server" ID="rptLab">
														<ItemTemplate>
															<li>
																<asp:Label ID="NameLabel" runat="server" Text='<%#Eval("name") %>' Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false" />
															</li>
														</ItemTemplate>
													</asp:Repeater>
												</ul>
											</div>
											<div style="padding-top: 10px" class="divider-bottom">
												<div class="pretty p-icon p-curve">
													<asp:CheckBox runat="server" ID="chkRad" />
													<div class="state p-success">
														<i class="icon fa fa-check"></i>
														<label style="font-size: 12px; font-weight: bold; margin-left: 10px;">RADIOLOGY</label>
													</div>
												</div>
												<br />
												<ul style="padding-left: 45px">
													<asp:Repeater runat="server" ID="rptRad">
														<ItemTemplate>
															<li>
																<asp:Label ID="NameLabel" runat="server" Text='<%#Eval("name") %>' Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false" />
															</li>
														</ItemTemplate>
													</asp:Repeater>
												</ul>
											</div>
											<div style="padding-top: 10px" class="divider-bottom">
												<div class="pretty p-icon p-curve">
													<asp:CheckBox runat="server" ID="chkDrugs" onclick="ToogleCopyDrugsCheckbox();" />
													<div class="state p-success">
														<i class="icon fa fa-check"></i>
														<label style="font-size: 12px; font-weight: bold; margin-left: 10px;">DRUGS PRESCRIPTION</label>
													</div>
												</div>
												<asp:Button ID="ButtonToogleChkDrugs" runat="server" Text="-" Style="display: none;" OnClick="ButtonToogleChkDrugs_Click" />
												<br />
												<ul style="padding-left: 45px">
													<asp:Repeater runat="server" ID="rptDrugs">
														<ItemTemplate>
															<li>
																<div class="pretty p-icon p-curve">
																	<asp:CheckBox runat="server" ID="chkChooseDrugs" Checked="true" />
																	<div class="state p-success">
																		<i class="icon fa fa-check"></i>
																		<label style="font-size: 12px;"><%#Eval("item_name") %>&nbsp&nbsp<%#Eval("quantity") %>&nbsp<%#Eval("uom_code") %>,&nbsp<%#Eval("frequency_code") %>,&nbsp<%#Eval("remarks") %>,&nbsp<%#Eval("administration_route_code") %>,&nbsp<%#Eval("is_routine") %></label>
																		<asp:Label ID="LblFlagDrugActive" runat="server" Text="Not Active" Visible='<%# Eval("IsActive").ToString().ToLower() == "false" ? true : false %>' CssClass="badge" Style="position: absolute; margin-top: -4px; margin-left: 4px;"></asp:Label>
																	</div>
																</div>
																<asp:HiddenField ID="HF_drugsIdCopySoap" runat="server" Value='<%#Eval("item_id") %>' />
																<asp:HiddenField ID="HF_drugsisactiveCopySoap" runat="server" Value='<%#Eval("IsActive ") %>' />
															</li>

															<%--<li>
                                                                <asp:Label ID="NameLabel" runat="server" Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false"><%#Eval("item_name") %>&nbsp&nbsp<%#Eval("quantity") %>&nbsp<%#Eval("uom_code") %>,&nbsp<%#Eval("frequency_code") %>,&nbsp<%#Eval("remarks") %>,&nbsp<%#Eval("administration_route_code") %>,&nbsp<%#Eval("is_routine") %></asp:Label>
                                                            </li>--%>
														</ItemTemplate>
													</asp:Repeater>
												</ul>
											</div>
											<div style="padding-top: 10px" class="divider-bottom">
												<div class="pretty p-icon p-curve">
													<asp:CheckBox runat="server" ID="chkCompound" />
													<div class="state p-success">
														<i class="icon fa fa-check"></i>
														<label style="font-size: 12px; font-weight: bold; margin-left: 10px;">COMPOUND</label>
													</div>
												</div>
												<ul style="padding-left: 45px">
													<asp:Repeater runat="server" ID="rptCompound">
														<ItemTemplate>
															<li>
																<asp:Label ID="NameLabel" runat="server" Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false"><%#Eval("compound_name") %>&nbsp&nbsp<%#Eval("quantity") %>&nbsp<%#Eval("uom_code") %>,&nbsp<%#Eval("frequency_code") %>,&nbsp<%#Eval("compound_note") %>,&nbsp<%#Eval("administration_route_code") %></asp:Label>
															</li>
															<%--<br />
                                                            <asp:Repeater ID="rptCompDetail" runat="server">
                                                                <ItemTemplate>
                                                                    <li>
                                                                        <asp:Label ID="NameLabel" runat="server" Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false"><%#Eval("item_name") %>&nbsp&nbsp<%#Eval("quantity") %>&nbsp<%#Eval("uom_code") %>,&nbsp<%#Eval("frequency_code") %>,&nbsp<%#Eval("remarks") %>,&nbsp<%#Eval("administration_route_code") %>,&nbsp<%#Eval("is_routine") %></asp:Label>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <br />--%>
														</ItemTemplate>
													</asp:Repeater>
												</ul>
											</div>
											<div style="padding-top: 10px">
												<div class="pretty p-icon p-curve">
													<asp:CheckBox runat="server" ID="chkConsumables" />
													<div class="state p-success">
														<i class="icon fa fa-check"></i>
														<label style="font-size: 12px; font-weight: bold; margin-left: 10px;">CONSUMABLES</label>
													</div>
												</div>
												<br />
												<ul style="padding-left: 45px">
													<asp:Repeater runat="server" ID="rptConsumables">
														<ItemTemplate>
															<li>
																<asp:Label ID="NameLabel" runat="server" Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false"><%#Eval("item_name") %>&nbsp&nbsp<%#Eval("quantity") %>&nbsp<%#Eval("uom_code") %>,&nbsp<%#Eval("frequency_code") %>,&nbsp<%#Eval("remarks") %>,&nbsp<%#Eval("administration_route_code") %>,&nbsp<%#Eval("is_routine") %></asp:Label>
															</li>
														</ItemTemplate>
													</asp:Repeater>
												</ul>
											</div>
										</div>
									</div>
								</div>
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>
		</div>
		<%-- ========================================================== MODAL COPY SOAP ================================================================ --%>

		<%-- ========================================================== MODAL COPY PRESCRIPTION ================================================================ --%>
		<div class="modal fade" id="modalCopyPrescription" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-dialog" style="top: 2%; width: 80%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
						<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
						<h5 class="modal-title">
							<asp:Label ID="Label11" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Copy Drugs HOPE"></asp:Label></h5>
					</div>
					<div class="modal-body">
						<asp:UpdatePanel ID="UpdatePanelModalCopyDrugs" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<div class="row">
									<div class="col-sm-3">
									</div>
									<div class="col-sm-6" style="padding-top: 12px">
										<label style="font-weight: bold; font-size: 14px">Preview</label>
									</div>
									<div class="col-sm-3" style="text-align: right">
										<asp:UpdatePanel ID="UpdatePanel25" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<asp:Button runat="server" CssClass="btn btn-lightGreen" ID="Button1" Text="Copy HOPE Drugs" Style="color: white; width: 176px; height: 32px; padding-top: 4px; border-radius: 4px; background-color: #4d9b35" OnClientClick="return cekbeforecopyhope();" OnClick="btnCopyHOPE_onClick" />
											</ContentTemplate>
										</asp:UpdatePanel>
									</div>
									<asp:UpdateProgress ID="UpdateProgress16" runat="server" AssociatedUpdatePanelID="UpdatePanel25">
										<ProgressTemplate>
											<div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
											</div>
											<div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
												<img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
											</div>
										</ProgressTemplate>
									</asp:UpdateProgress>
								</div>
								<div class="row" style="padding-top: 5px">
									<div class="col-sm-3" style="padding-right: 0px; padding-left: 0px;">
										<asp:UpdatePanel ID="UpdatePanel26" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<div class="col-sm-12" style="padding-right: 0px">
													<label style="font-size: 10px">Doctor</label>
													<br />
													<asp:Button runat="server" ID="btnSearchDoctor_CopyPrescription" Visible="false" OnClick="txtsearchDoctor_onChange" />
													<div class="has-feedback">
														<asp:TextBox runat="server" ID="txtsearchdoctor_prescription" AutoPostBack="true" onkeypress="keypressdoctorprescription()" placeholder="Search..." Style="width: 100%; max-width: 100%; height: 32px; padding-left: 5px" OnTextChanged="txtsearchDoctorprescription_onChange"></asp:TextBox>
														<span class="form-control-feedback" style="margin-top: -1px; margin-right: 15px;">
															<asp:UpdateProgress ID="UpdateProgress17" runat="server" AssociatedUpdatePanelID="UpdatePanel26">
																<ProgressTemplate>
																	<asp:Image ID="Image2" runat="server" Style="width: 40px; background-color: transparent;" ImageUrl="~/Images/Background/small-loader.gif" />
																</ProgressTemplate>
															</asp:UpdateProgress>
														</span>
													</div>
												</div>
												<div class="col-sm-12" style="padding-top: 10px; padding-right: 0px">
													<asp:GridView ID="gvw_doctorhope" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" BorderColor="Black"
														HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center" BorderWidth="0"
														ShowHeaderWhenEmpty="True" DataKeyNames="AdmissionId" EmptyDataText="No Data" ShowHeader="false"
														AllowSorting="True">
														<PagerStyle CssClass="pagination-ys" />
														<Columns>
															<asp:TemplateField HeaderText="" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-BorderWidth="1" ItemStyle-BorderColor="#bbbfd4">
																<ItemTemplate>
																	<asp:LinkButton CssClass="stylink" ID="copyitem" runat="server" OnClick="btngethopeprescription_onClick">
                                                                            <div style="width:100%">
                                                                            <%# Eval("AdmissionDate") %>&nbsp<%# Eval("AdmissionNo") %><br />
                                                                            <%# Eval("DoctorName") %>
                                                                            </div>
																	</asp:LinkButton>
																	<asp:HiddenField ID="hfAdmissionId" Value='<%# Bind("AdmissionId") %>' runat="server" />
																</ItemTemplate>
															</asp:TemplateField>
														</Columns>
													</asp:GridView>
													<asp:HiddenField ID="HF_copyhope_oldrow" runat="server" />
												</div>
											</ContentTemplate>
										</asp:UpdatePanel>
									</div>

									<div class="col-sm-9" style="transform: translate(0,0);">
										<div style="border: 1px solid #707070; padding: 10px; margin-top: 5px;">
											<div id="div_nodatacopyhope" runat="server" style="display: block;" class="empty-info">
												<label id="lbl_nodatacopyhope" style="font-size: 14px;">No Data Selected</label>
											</div>
											<div style="padding-top: 10px" class="divider-bottom">
												<asp:Repeater runat="server" ID="rptdiagnosahope">
													<ItemTemplate>
														<div style="margin-bottom: 5px;">
															<asp:Label runat="server" Style="font-size: 12px; font-weight: bold;" Text='<%#Eval("Name") %>'></asp:Label>
															<li>
																<asp:Label runat="server" Style="font-size: 12px; font-weight: bold; margin-left: -5px;" Text='<%#Eval("EntryText") %>'></asp:Label>
															</li>
														</div>
													</ItemTemplate>
												</asp:Repeater>
											</div>
											<div style="padding-top: 10px">
												<asp:GridView ID="gvw_drughope" runat="server" AutoGenerateColumns="False"
													CssClass="table table-striped table-bordered table-condensed"
													DataKeyNames="prescription_id">
													<Columns>
														<asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Item" ItemStyle-Width="30%" HeaderStyle-Font-Size="11px">
															<ItemTemplate>
																<asp:HiddenField ID="prescription_id" runat="server" Value='<%# Bind("prescription_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="prescription_no" runat="server" Value='<%# Bind("prescription_no") %>'></asp:HiddenField>
																<asp:HiddenField ID="item_id" runat="server" Value='<%# Bind("item_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="uom_id" runat="server" Value='<%# Bind("uom_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="frequency_id" runat="server" Value='<%# Bind("frequency_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="dose_uom_id" runat="server" Value='<%# Bind("dose_uom_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="dose_text" runat="server" Value='<%# Bind("dose_text") %>'></asp:HiddenField>
																<asp:HiddenField ID="administration_route_id" runat="server" Value='<%# Bind("administration_route_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="is_routine" runat="server" Value='<%# Bind("is_routine") %>'></asp:HiddenField>
																<asp:HiddenField ID="is_consumables" runat="server" Value='<%# Bind("is_consumables") %>'></asp:HiddenField>
																<asp:HiddenField ID="compound_id" runat="server" Value='<%# Bind("compound_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="compound_name" runat="server" Value='<%# Bind("compound_name") %>'></asp:HiddenField>
																<asp:HiddenField ID="origin_prescription_id" runat="server" Value='<%# Bind("origin_prescription_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="hope_arinvoice_id" runat="server" Value='<%# Bind("hope_aritem_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="is_delete" runat="server" Value='<%# Bind("is_delete") %>'></asp:HiddenField>
																<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="item_name" runat="server" Text='<%# Bind("item_name") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Dose" ItemStyle-CssClass="numberofGrid3" HeaderStyle-Font-Size="11px">
															<ItemTemplate>
																<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="dosage_id" runat="server" Text='<%# Bind("dosage_id") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Dose UoM" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
															<ItemTemplate>
																<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="dose_uom" runat="server" Text='<%# Bind("dose_uom") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Frequency" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
															<ItemTemplate>
																<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="frequency_code" runat="server" Text='<%# Bind("frequency_code") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Route" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
															<ItemTemplate>
																<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="administration_route_code" runat="server" Text='<%# Bind("administration_route_code") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Instruction" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
															<ItemTemplate>
																<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="remarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Qty" ItemStyle-CssClass="numberofGrid3" HeaderStyle-Font-Size="11px">
															<ItemTemplate>
																<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="quantity" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="UoM" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px">
															<ItemTemplate>
																<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="uom_code" runat="server" Text='<%# Bind("uom_code") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Iter" ItemStyle-CssClass="numberofGrid1" HeaderStyle-Font-Size="11px">
															<ItemTemplate>
																<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="iteration" runat="server" Text='<%# Bind("iteration") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
													</Columns>
												</asp:GridView>

												<asp:GridView ID="gvw_conshope" runat="server" AutoGenerateColumns="False"
													CssClass="table table-striped table-bordered table-condensed"
													DataKeyNames="prescription_id">
													<Columns>
														<asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Item" ItemStyle-Width="30%" HeaderStyle-Font-Size="11px">
															<ItemTemplate>
																<asp:HiddenField ID="prescription_id" runat="server" Value='<%# Bind("prescription_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="prescription_no" runat="server" Value='<%# Bind("prescription_no") %>'></asp:HiddenField>
																<asp:HiddenField ID="item_id" runat="server" Value='<%# Bind("item_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="uom_id" runat="server" Value='<%# Bind("uom_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="frequency_id" runat="server" Value='<%# Bind("frequency_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="dose_uom_id" runat="server" Value='<%# Bind("dose_uom_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="dose_text" runat="server" Value='<%# Bind("dose_text") %>'></asp:HiddenField>
																<asp:HiddenField ID="administration_route_id" runat="server" Value='<%# Bind("administration_route_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="is_routine" runat="server" Value='<%# Bind("is_routine") %>'></asp:HiddenField>
																<asp:HiddenField ID="is_consumables" runat="server" Value='<%# Bind("is_consumables") %>'></asp:HiddenField>
																<asp:HiddenField ID="compound_id" runat="server" Value='<%# Bind("compound_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="compound_name" runat="server" Value='<%# Bind("compound_name") %>'></asp:HiddenField>
																<asp:HiddenField ID="origin_prescription_id" runat="server" Value='<%# Bind("origin_prescription_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="hope_arinvoice_id" runat="server" Value='<%# Bind("hope_aritem_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="is_delete" runat="server" Value='<%# Bind("is_delete") %>'></asp:HiddenField>
																<asp:HiddenField ID="dosage_id" runat="server" Value='<%# Bind("dosage_id") %>'></asp:HiddenField>
																<asp:HiddenField ID="dose_uom" runat="server" Value='<%# Bind("dose_uom") %>'></asp:HiddenField>
																<asp:HiddenField ID="frequency_code" runat="server" Value='<%# Bind("frequency_code") %>'></asp:HiddenField>
																<asp:HiddenField ID="administration_route_code" runat="server" Value='<%# Bind("administration_route_code") %>'></asp:HiddenField>
																<asp:HiddenField ID="iteration" runat="server" Value='<%# Bind("iteration") %>'></asp:HiddenField>
																<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="item_name" runat="server" Text='<%# Bind("item_name") %>' Style="padding-left: 10px;"></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Qty" ItemStyle-CssClass="numberofGrid3" HeaderStyle-Font-Size="11px">
															<ItemTemplate>
																<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="quantity" runat="server" Text='<%# Bind("quantity") %>' Style="padding-left: 10px;"></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="UoM" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px">
															<ItemTemplate>
																<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="uom_code" runat="server" Text='<%# Bind("uom_code") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Instruction" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px">
															<ItemTemplate>
																<asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="remarks" runat="server" Text='<%# Bind("remarks") %>' Style="padding-left: 10px;"></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
													</Columns>
												</asp:GridView>
											</div>
										</div>
									</div>
								</div>
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>
		</div>
		<%-- ========================================================== MODAL COPY PRESCRIPTION ================================================================ --%>

        <%-- ============================================= MODAL NOTIF UOM DRUGS ============================================== --%>
        <div class="modal fade" id="modaluomchangedrugssoap" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <asp:UpdatePanel ID="UpdatePanelUomChangesoap" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-dialog" style="position: fixed; top: 15%; left:0; right:0; width: 30%;" runat="server" id="dialogDrugsUomChange">
                        <div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h5 class="modal-title">
                                    <asp:Label ID="LabelTitleUomChange" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 16px;" runat="server" Text="Change Quantity Of Drugs"></asp:Label></h5>
                            </div>
                            <div class="modal-body">

                                <label style="font-size:14px;">Please change/recalculate the drug Quantity below : </labe>
                                <br />
                                <br />
                                <ul style="padding-left: 15px;">
                                    <asp:Repeater runat="server" ID="RepeaterDrugsUomChange">
                                        <ItemTemplate>
                                            <li>
                                                <div style="font-size:14px; font-family:Helvetica; padding-bottom:10px;">
                                                    <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("item_name") %>'/> - 
                                                    <asp:Label ID="QtyLabel" runat="server" Text='<%#String.Format("{0:0.##}", decimal.Parse(Eval("quantity").ToString().Replace(".",","))) + " " + Eval("uom_code") %>' style="color:red; font-weight:bold;" /> 
                                                    <br />
                                                    <div style='<%# String.Format("{0:0.##}", decimal.Parse(Eval("uom_ratio").ToString())) == "1" ? "display:none;" : "display:inline-block;" %>'>
                                                    <label style="color:red; font-weight:bold;"><%# "1 " + Eval("uom_code") %></label> = <label style="color:deepskyblue; font-weight:bold;"><%#String.Format("{0:0.##}", decimal.Parse(Eval("uom_ratio").ToString())) + " " + Eval("uom_codeori") %></label> . 
                                                    </div>
                                                    <label>UoM automatically changed from</label>
                                                    <label style="color:red; font-weight:bold;"><%#Eval("uom_code") %></label>
                                                    <label>to</label>
                                                    <label style="color:deepskyblue; font-weight:bold;"><%#Eval("uom_codeori") %></label>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <label style="font-size:12px; font-style:italic;">*EMR Currently can only process uom with smallest ratio</label> /
                                <label style="font-size:12px;">EMR saat ini hanya bisa proses satuan terkecil obat.</labe>

                                <div style="text-align:right; padding-top:15px;">
                                    <button type="button" class="btn btn-sm btn-success" data-dismiss="modal" aria-hidden="true">OK</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%-- ============================================= MODAL NOTIF UOM DRUGS ============================================== --%>


        <%-- ========================================================== MODAL NOTIFICATION SUBMIT ========================================================== --%>
        <div class="modal fade" id="modalnotifsubmit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <asp:UpdatePanel ID="UpdatePanelNotifSubmit" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="HFflagsoapisdisable_old" runat="server" />
                    <asp:HiddenField ID="HFflaglabradisdisable_old" runat="server" />
                    <asp:HiddenField ID="HFflaglabisdisable_old" runat="server" />
                    <asp:HiddenField ID="HFflagradisdisable_old" runat="server" />
                    <asp:HiddenField ID="HFflagdrugisdisable_old" runat="server" />
                    <asp:HiddenField ID="HFflagadddrugisdisable_old" runat="server" />

					<div class="modal-dialog" style="padding-top: 12%; width: 35%;">
						<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
							<div class="modal-header" style="height: 40px; padding-top: 9px; padding-bottom: 9px; text-align: center; background-color: #4081ed; color: white; border-radius: 5px 5px 0px 0px;">
								<span style="font-size: 14px; font-weight: bold">
									<asp:Label runat="server" ID="notifsubmitheader"></asp:Label>
								</span>
							</div>
							<div class="modal-body">
								<div style="text-align: left;" visible="true" runat="server" id="notifsoap">
									<table>
										<tr>
											<td>
												<i id="isubmit" runat="server" style="font-size: 20px; color: #4081ed;" class="fa fa-info-circle"></i>&nbsp;&nbsp;
											</td>
											<td style="font-size: 14px;">
												<asp:Label runat="server" ID="notifsubmit"></asp:Label>
											</td>
										</tr>
									</table>
								</div>
								<div style="text-align: left; background-color: aliceblue; border: 1px solid #4081ed; border-radius: 6px; padding: 8px; margin-bottom: 5px;" visible="false" runat="server" id="notifsoap2">
									<table>
										<tr>
											<td>
												<i id="isubmit2" runat="server" style="font-size: 20px; color: #4081ed;" class="fa fa-info-circle"></i>&nbsp;&nbsp;
											</td>
											<td style="font-size: 14px;">
												<asp:Label runat="server" ID="notifsubmit2"></asp:Label>
											</td>
										</tr>
									</table>
								</div>
								<div style="text-align: left; background-color: aliceblue; border: 1px solid #4081ed; border-radius: 6px; padding: 8px; margin-bottom: 5px;" visible="false" runat="server" id="notifsoap3">
									<table>
										<tr>
											<td>
												<i id="isubmit3" runat="server" style="font-size: 20px; color: #4081ed;" class="fa fa-info-circle"></i>&nbsp;&nbsp;
											</td>
											<td style="font-size: 14px;">
												<asp:Label runat="server" ID="notifsubmit3"></asp:Label>
											</td>
										</tr>
									</table>
								</div>
								<div style="text-align: right; padding-top: 10px;">
									<%--<asp:Button runat="server" aria-hidden="true" ID="Button1" Style="width: 112px; height: 32px; border-radius: 4px; background-color: #c43d32; color: white" Text="OK" OnClientClick="javascript:shouldsubmit=true" OnClick="btnreloadsave_page" />--%>
									<button class="btn" data-dismiss="modal" style="width: 112px; background-color: #4081ed; color: white;">OK</button>
								</div>
							</div>
						</div>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>

		<div class="modal fade" id="modalnotifsubmitfail" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
			<asp:UpdatePanel ID="UpdatePanelNotifSubmitFail" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="modal-dialog" style="padding-top: 12%; width: 35%;">
						<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
							<div class="modal-header" style="height: 40px; padding-top: 9px; padding-bottom: 9px; text-align: center; background-color: red; color: white; border-radius: 5px 5px 0px 0px;">
								<span style="font-size: 14px; font-weight: bold">Submit process fail!</span>
							</div>
							<div class="modal-body">
								<div style="text-align: left;" visible="true" runat="server" id="Div1">
									<table>
										<tr>
											<td>
												<i id="i1" runat="server" style="font-size: 25px; color: red;" class="fa fa-times-circle"></i>&nbsp;&nbsp;
											</td>
											<td style="padding-top: 17px; font-size: 14px;">
												<asp:Label runat="server" ID="notifsubmitfail"></asp:Label>
											</td>
										</tr>
									</table>
								</div>
								<div style="text-align: right; padding-top: 10px;">
									<%--<asp:Button runat="server" aria-hidden="true" ID="Button1" onclick="window.location.reload();" Style="width: 112px; height: 32px; border-radius: 4px; background-color: #c43d32; color: white" Text="OK" OnClientClick="javascript:shouldsubmit=true" OnClick="btnreloadsave_page" />--%>
									<button class="btn" data-dismiss="modal" style="width: 112px; background-color: red; color: white;">OK</button>
								</div>
							</div>
						</div>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
		<%-- ========================================================== END MODAL NOTIFICATION SUBMIT ========================================================== --%>

		<%-- ========================================================== MODAL SAVE SUCCESS ========================================================== --%>
		<div class="modal fade" id="modalsavesuccess" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="modal-dialog" style="position: fixed; top: 25%; left: 40%; width: 20%;">
						<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
							<div class="modal-body">
								<div style="text-align: center">
									<label style="font-size: 14px">Save as draft successful</label>
								</div>
								<div>&nbsp</div>
								<div class="row" style="padding-left: 5%; padding-right: 5%">
									<div class="col-sm-12" style="text-align: center">
										<asp:Button runat="server" aria-hidden="true" ID="btnOkSave" Style="width: 112px; height: 32px; border-radius: 4px; background-color: #c43d32; color: white" Text="OK" OnClientClick="javascript:shouldsubmit=true" OnClick="btnreloadsave_page" />
									</div>
								</div>
							</div>
						</div>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
		<%-- ========================================================== END MODAL SAVE SUCCESS ========================================================== --%>

		<%-- ========================================================== MODAL SUBMIT AND SIGN ========================================================== --%>
		<div class="modal fade" id="modalsubmit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
			<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
				<ProgressTemplate>
					<div class="modal-backdrop" style="background-color: black; opacity: 0.6; vertical-align: central; text-align: center">
					</div>
					<div style="margin-top: 200px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
						<img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
					</div>
				</ProgressTemplate>
			</asp:UpdateProgress>

			<div class="modal-dialog" style="min-width: 530px; width: fit-content; padding-top: 80px;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
						<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
						<h5 class="modal-title">
							<asp:Label ID="Label1" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Submit & Sign"></asp:Label></h5>
					</div>
					<div class="modal-body">
						<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
							<ContentTemplate>

								<div id="emergencySection" style="width: 400px; display: inline-block; vertical-align: top;" runat="server" visible="false">
									<div class="row" style="border-right: 1px solid #f4f4f4;">
										<div class="col-sm-12" style="padding-left: 5%; margin-bottom: 15px;">
											<label id="lblbhs_tindaklanjut1" style="font-size: 12px; font-weight: bold; font-family: Helvetica">Tindak Lanjut</label>
											<br />
											<asp:DropDownList ID="ddl_tindaklanjut1" runat="server" Style="width: 50%; padding-left: 5px" onchange="enableTXTTL('ddl_tindaklanjut1','TextKetTindakLanjut1');">
												<asp:ListItem Value="-">- Pilih Tindak Lanjut -</asp:ListItem>
												<asp:ListItem Value="Pulang">Pulang</asp:ListItem>
												<asp:ListItem Value="Pulang atas kemauan sendiri">Pulang atas kemauan sendiri</asp:ListItem>
												<asp:ListItem Value="OT/HD/Cath Lab/">OT/HD/Cath Lab/</asp:ListItem>
												<asp:ListItem Value="Rujuk ke">Rujuk ke</asp:ListItem>
												<asp:ListItem Value="Rawat ke">Rawat ke</asp:ListItem>
											</asp:DropDownList>
											&nbsp;
                                            <asp:TextBox ID="TextKetTindakLanjut1" runat="server" CssClass="form-control" Disabled Style="font-size: 12px; width: 40%; display: inline-block; padding-left: 5px;" placeholder="Keterangan lebih lanjut" onkeypress="return checkenter();"></asp:TextBox>
										</div>
										<br />
										<div class="col-sm-12" style="padding-left: 5%; margin-bottom: 15px;">
											<label id="lblbhs_keluared1" style="font-size: 12px; font-weight: bold; font-family: Helvetica">Keluar dari ED</label>
											<br />
											<asp:TextBox ID="TextTglKeluar1" runat="server" CssClass="form-control" Style="font-size: 12px; width: 40%; display: inline-block; padding-left: 5px;" onkeydown="return txtOnKeyPress();" placeholder="dd/mm/yyyy" onmousedown="datepasienpulang();" AutoCompleteType="Disabled"></asp:TextBox>
											&nbsp;
                                            <asp:TextBox ID="TextJamKeluar1" runat="server" CssClass="form-control" Style="font-size: 12px; width: 20%; display: inline-block; padding-left: 5px;" placeholder="hh:mm" onkeypress="return checkenter();"></asp:TextBox>
										</div>
										<div class="col-sm-12" style="padding-left: 5%; margin-bottom: 15px;">
											<label id="lblbhs_kondisi1" style="font-size: 12px; font-weight: bold; font-family: Helvetica">Kondisi</label>
											<br />
											<div class="radio-margin" style="display: inline-block; margin-right: 20px;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="kondisipulang1" Value="0" ID="rbkpstabil1" />
													<div class="state p-primary-o">
														<label>Stabil</label>
													</div>
												</div>
											</div>
											<div class="radio-margin" style="display: inline-block; margin-right: 20px;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="kondisipulang1" Value="1" ID="rbkptidakstabil1" />
													<div class="state p-primary-o">
														<label>Tidak Stabil</label>
													</div>
												</div>
											</div>
											<div class="radio-margin" style="display: inline-block;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="kondisipulang1" Value="2" ID="rbkpmeninggal1" />
													<div class="state p-primary-o">
														<label>Meninggal</label>
													</div>
												</div>
											</div>
										</div>
										<div class="col-sm-12" style="padding-left: 5%; margin-bottom: 15px;">
											<label id="lblbhs_EWS1" style="font-size: 12px; font-weight: bold; font-family: Helvetica">EWS/PEWS/MEWS</label>
											<br />
											<div class="radio-margin" style="display: inline-block; margin-right: 20px;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="EWS1" Value="EWS" ID="rbews1" onclick="enableTXTEWS('TextEWS1','TextPEWS1','TextMEWS1');" />
													<div class="state p-primary-o">
														<label>EWS</label>
													</div>
												</div>
												<asp:TextBox ID="TextEWS1" runat="server" CssClass="form-control" Disabled Style="font-size: 12px; width: 45px; display: inline-block;" placeholder="skor" onkeypress="return CheckNumericnumber();"></asp:TextBox>
											</div>
											<div class="radio-margin" style="display: inline-block; margin-right: 20px;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="EWS1" Value="PEWS" ID="rbpews1" onclick="enableTXTEWS('TextPEWS1','TextEWS1','TextMEWS1');" />
													<div class="state p-primary-o">
														<label>PEWS</label>
													</div>
												</div>
												<asp:TextBox ID="TextPEWS1" runat="server" CssClass="form-control" Disabled Style="font-size: 12px; width: 45px; display: inline-block;" placeholder="skor" onkeypress="return CheckNumericnumber();"></asp:TextBox>
											</div>
											<div class="radio-margin" style="display: inline-block;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="EWS1" Value="MEWS" ID="rbmews1" onclick="enableTXTEWS('TextMEWS1','TextEWS1','TextPEWS1');" />
													<div class="state p-primary-o">
														<label>MEWS</label>
													</div>
												</div>
												<asp:TextBox ID="TextMEWS1" runat="server" CssClass="form-control" Disabled Style="font-size: 12px; width: 45px; display: inline-block;" placeholder="skor" onkeypress="return CheckNumericnumber();"></asp:TextBox>
											</div>
										</div>

									</div>

								</div>

								<div id="commonSection" style="display: inline-block; width: 500px;">
									<div class="row" style="padding-left: 5%; padding-right: 5%; display: none;">
										<div class="col-sm-2">
											<label style="font-size: 12px; font-weight: bold; font-family: Helvetica">Referral</label>
										</div>
										<div class="col-sm-3" style="padding-right: 0px; padding-left: 0px;">
											<div style="margin-right: 5px; margin-left: 5px;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="referral" Value="0" ID="rbreferral1" Checked="true" onclick="disableref()" />
													<div class="state p-primary-o">
														<label>No</label>
													</div>
												</div>
												&nbsp; &nbsp;
                                           <div class="pretty p-default p-round">
											   <asp:RadioButton runat="server" GroupName="referral" Value="1" ID="rbreferral2" onclick="enableref()" />
											   <div class="state p-primary-o">
												   <label>Yes</label>
											   </div>
										   </div>
											</div>
										</div>
										<div class="col-sm-7" style="padding-left: 0px;">
											<asp:TextBox runat="server" Style="font-size: 12px; font-family: Helvetica; color: #171717; font-weight: normal; height: 23px; width: 100%; max-width: 100%; border-radius: 2px; border: solid 1px #cdced9;" ID="txtreferal" Disabled="true"> </asp:TextBox>
										</div>
									</div>
									<div class="row" style="padding-left: 5%">
										<div class="col-sm-3">
											<label style="font-size: 12px; font-weight: bold; font-family: Helvetica">
												<label id="lblbhs_consultationfee1">Consultation fee </label>
											</label>
										</div>
									</div>
									<div class="row" style="padding-left: 5%; padding-right: 5%;">
										<div class="col-sm-12">
											<%--<asp:TextBox runat="server" style="font-size:14px;font-weight:bold;font-family:Helvetica;height:32px;width:170px;border-radius: 4px;border: solid 1px #9293a0;padding-left:11px">RP 300.000</asp:TextBox>--%>
											<asp:DropDownList ID="ddl_consultationfee" runat="server" Style="font-size: 12px; font-weight: bold; font-family: Helvetica; height: 32px; width: 100%; max-width: 100%; border-radius: 4px; border: solid 1px #9293a0; padding-left: 11px" onchange="normalcharges();"></asp:DropDownList>
										</div>
									</div>
									<div>&nbsp</div>
									<div class="row" style="padding-left: 5%">
										<div class="col-sm-3">
											<label style="font-size: 12px; font-weight: bold; font-family: Helvetica">
												<label id="lblbhs_spesialprice1">Special Price </label>
											</label>
										</div>
									</div>
									<div class="row" style="padding-left: 5%; padding-top: 5px;">
										<div class="col-sm-3">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="price" Value="0" ID="rbPrice1" Checked="true" onclick="normalcharges();" />
												<div class="state p-primary-o">
													<label id="lblbhs_normalprice1">Normal Price </label>
												</div>
											</div>
										</div>
									</div>
									<div class="row" style="padding-left: 5%; padding-top: 5px;">
										<div class="col-sm-3">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="price" Value="1" ID="rbPrice2" onclick="freecharges();" />
												<div class="state p-primary-o">
													<label id="lblbhs_freeprice1">Free of Charge </label>
												</div>
											</div>
										</div>
									</div>
									<div class="row" style="padding-left: 5%; padding-top: 5px;">
										<div class="col-sm-2">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="price" Value="2" ID="rbPrice3" onclick="enabledisc()" />
												<div class="state p-primary-o">
													<label id="lblbhs_discount1">Discount </label>
												</div>
											</div>
										</div>
										<div class="col-sm-3" style="display: inline-flex;">
											Rp.
                                        <asp:TextBox runat="server" Style="text-align: right; font-size: 12px; font-family: Helvetica; color: #171717; font-weight: normal; height: 23px; width: 150px; border-radius: 2px; border: solid 1px #cdced9;" Disabled="true" ID="txtDiscount" onkeypress="return CheckNumericnumber();" onblur="CalculateTotalFee();"></asp:TextBox>
										</div>
									</div>
									<div>&nbsp</div>
									<%--<div class="row" style="padding-left: 5%">
                                    <div class="col-sm-6">
                                        <label style="font-size: 10px; font-family: Helvetica">
                                            <%--<label id="ENGprocedurenotes1" style="display: <%=setENG%>;">Procedure notes </label>
                                            <label id="INDprocedurenotes1" style="display: <%=setIND%>;">Catatan Tindakan </label>--%>
									<%--<label id="lblbhs_procedurenotes1">Procedure On Encounter </label>
                                        </label>
                                    </div>
                                </div>--%>
									<div class="row" style="padding-left: 5%; padding-right: 5%">
										<div class="col-sm-12 hidden">
											<asp:TextBox runat="server" Style="outline-color: gray; max-width: 100%; width: 100%; border-radius: 4px; resize: none; font-family: Helvetica, Arial, sans-serif" placeholder="Type here..." BorderColor="gray" ID="txtProcedure" TextMode="MultiLine" Rows="3" onkeydown="checkTextAreaMaxLength(this,event,'100');" />
										</div>
										<%--<div class="col-sm-12">
                                        <!-- ## kotak pencarian autocomplete ## -->
                                        <%--<asp:UpdatePanel runat="server">
                                            <ContentTemplate>--%>
										<%--<div class="row">
                                                    <div class="col-sm-3" style="width: 190px;">
                                                        <div class="has-feedback" style="width: 180px;">
                                                            <asp:TextBox ID="txtItemDiagProc_AC" runat="server" Placeholder="Select..." Style="width: 180px;" class="autosuggest" onkeydown="return txtOnKeyPressFalseICD();"></asp:TextBox>
                                                            <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px; z-index: 0;"></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="HF_ItemSelectedDiagProc" runat="server" />
                                                <asp:Button ID="ButtonAjaxSearchDiagProc" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchDiagProc_Click" />
                                            <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
										<!-- ## end kotak pencarian autocomplete ## -->

										<%--<br />
                                        <div class="border-submitprocedure" id="divdiagproc_en" runat="server">
                                            <asp:GridView ID="Gvw_submitdiagnosticprocedure" runat="server" AutoGenerateColumns="false" CssClass="table-kecil" ShowHeader="false"
                                                DataKeyNames="ProcedureItemId" BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="titik" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="3%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                        <ItemTemplate>
                                                            <i class="fa fa-circle" style="font-size: 5px; vertical-align: middle; margin-right: 2px;"></i>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hf_id_submitdiagproc" runat="server" Value='<%# Bind("ProcedureItemId") %>'></asp:HiddenField>
                                                            <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="diagproc_name" runat="server" Text='<%# Bind("ProcedureItemName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                        <ItemTemplate>
                                                            <%--<asp:Button Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="x" OnClick="btnDeleteAllergy_onClick"></asp:Button>--%>
										<%--<asp:ImageButton ID="btndeletediagproc" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btndeletediagproc_Click" Style="width: 12px; height: 12px; margin-top: 3px;" Visible='<%# Eval("IsSendHope").ToString() == "0" ? true : false %>' />
                                                            <i class="fa fa-info-circle" style='font-size: 14px; color: cornflowerblue; margin-top: 3px; display:<%# Eval("IsSendHope").ToString() == "0" ? "none" : "block" %>' title="Already send to hope"></i>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>--%>
									</div>

									<div>&nbsp</div>
									<div class="row" style="padding-left: 5%; padding-right: 5%">
										<div class="col-sm-4"></div>
										<div class="col-sm-8" style="text-align: right">
											<label style="font-size: 12px; font-family: Helvetica; text-align: right">
												<label id="lblbhs_total1">Total Fee: </label>
											</label>
											<br />
											<asp:TextBox runat="server" ID="txttotalfee" BorderColor="Transparent" ReadOnly="true" Style="outline-color: transparent; font-weight: bold; font-size: 16px; font-family: Helvetica; width: 120px; text-align: right"></asp:TextBox>
										</div>
									</div>
									<div>&nbsp</div>
									<div class="row" style="padding-left: 5%; padding-right: 5%">
										<div class="col-sm-6" style="text-align: left; font-size: 12px;">
											<asp:Button runat="server" CssClass="btn" Style="width: 112px; height: 32px; padding-top: 7px; font-size: 12px; border-radius: 4px; background-color: #171717; color: white" data-dismiss="modal" Text="Cancel" />
										</div>
										<div class="col-sm-6" style="text-align: right; font-size: 12px;">
											<asp:Label ID="Labelnotneed" runat="server" Text="Tanpa rujukan : " Visible="false"></asp:Label>
											<asp:Button aria-hidden="true" runat="server" ID="btnsubmit" CssClass="btn btnsubmitclass" Style="width: 112px; height: 32px; padding-top: 7px; font-size: 12px; border-radius: 4px; background-color: #4081ed; color: white" Text="Submit & Sign" OnClientClick="submitvalidation();" OnClick="btnSubmit_click" />
										</div>
										<div class="col-sm-12" style="text-align: right; font-size: 12px; padding-top: 15px;" id="divReferalButton" runat="server" visible="false">
											<asp:Label ID="Labelneed" runat="server" Text="Dengan rujukan pemeriksaan fisik lanjutan ke RS : " Visible="false"></asp:Label>
											<asp:Button aria-hidden="true" runat="server" ID="btnsubmitTC" CssClass="btn btnsubmitclass" Style="width: 145px; height: 32px; padding-top: 7px; font-size: 12px; border-radius: 4px; background-color: #4081ed; color: white" Text="Submit, Sign & Refer" OnClientClick="submitvalidationTC();" OnClick="btnSubmit_click" />
										</div>
									</div>
								</div>

							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>
		</div>
		<%-- ========================================================== END MODAL SUBMIT AND SIGN ========================================================== --%>

		<%-- ========================================================== MODAL SUBMIT AND SIGN DISABLE ========================================================== --%>
		<div class="modal fade" id="modalsubmitDisable" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
			<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
				<ProgressTemplate>
					<div class="modal-backdrop" style="background-color: black; opacity: 0.6; vertical-align: central; text-align: center">
					</div>
					<div style="margin-top: 200px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
						<img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
					</div>
				</ProgressTemplate>
			</asp:UpdateProgress>
			<div class="modal-dialog" style="min-width: 530px; width: fit-content; padding-top: 80px;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div class="modal-header" style="height: 30px; padding-top: 5px; padding-bottom: 5px">
						<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
						<h5 class="modal-title">
							<asp:Label ID="Label2" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Submit & Sign"></asp:Label></h5>
					</div>
					<div class="modal-body">
						<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
							<ContentTemplate>

								<div id="emergencySectionDisable" style="width: 400px; display: inline-block; vertical-align: top;" runat="server" visible="false">
									<div class="row" style="border-right: 1px solid #f4f4f4;">
										<div class="col-sm-12" style="padding-left: 5%; margin-bottom: 15px;">
											<label id="lblbhs_tindaklanjut2" style="font-size: 12px; font-weight: bold; font-family: Helvetica">Tindak Lanjut</label>
											<br />
											<asp:DropDownList ID="ddl_tindaklanjut2" runat="server" Style="width: 50%; padding-left: 5px" onchange="enableTXTTL('ddl_tindaklanjut2','TextKetTindakLanjut2');">
												<asp:ListItem Value="-">- Pilih Tindak Lanjut -</asp:ListItem>
												<asp:ListItem Value="Pulang">Pulang</asp:ListItem>
												<asp:ListItem Value="Pulang atas kemauan sendiri">Pulang atas kemauan sendiri</asp:ListItem>
												<asp:ListItem Value="OT/HD/Cath Lab/">OT/HD/Cath Lab/</asp:ListItem>
												<asp:ListItem Value="Rujuk ke">Rujuk ke</asp:ListItem>
												<asp:ListItem Value="Rawat ke">Rawat ke</asp:ListItem>
											</asp:DropDownList>
											&nbsp;
                                            <asp:TextBox ID="TextKetTindakLanjut2" runat="server" CssClass="form-control" Disabled Style="font-size: 12px; width: 40%; display: inline-block; padding-left: 5px;" placeholder="Keterangan lebih lanjut" onkeypress="return checkenter();"></asp:TextBox>
										</div>
										<br />
										<div class="col-sm-12" style="padding-left: 5%; margin-bottom: 15px;">
											<label id="lblbhs_keluared2" style="font-size: 12px; font-weight: bold; font-family: Helvetica">Keluar dari ED</label>
											<br />
											<asp:TextBox ID="TextTglKeluar2" runat="server" CssClass="form-control" Style="font-size: 12px; width: 40%; display: inline-block; padding-left: 5px;" onkeydown="return txtOnKeyPress();" placeholder="dd/mm/yyyy" onmousedown="datepasienpulangdisable();" AutoCompleteType="Disabled"></asp:TextBox>
											&nbsp;
                                            <asp:TextBox ID="TextJamKeluar2" runat="server" CssClass="form-control" Style="font-size: 12px; width: 20%; display: inline-block; padding-left: 5px;" placeholder="hh:mm" onkeypress="return checkenter();"></asp:TextBox>
										</div>
										<div class="col-sm-12" style="padding-left: 5%; margin-bottom: 15px;">
											<label id="lblbhs_kondisi2" style="font-size: 12px; font-weight: bold; font-family: Helvetica">Kondisi</label>
											<br />
											<div class="radio-margin" style="display: inline-block; margin-right: 20px;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="kondisipulang2" Value="0" ID="rbkpstabil2" />
													<div class="state p-primary-o">
														<label>Stabil</label>
													</div>
												</div>
											</div>
											<div class="radio-margin" style="display: inline-block; margin-right: 20px;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="kondisipulang2" Value="1" ID="rbkptidakstabil2" />
													<div class="state p-primary-o">
														<label>Tidak Stabil</label>
													</div>
												</div>
											</div>
											<div class="radio-margin" style="display: inline-block;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="kondisipulang2" Value="2" ID="rbkpmeninggal2" />
													<div class="state p-primary-o">
														<label>Meninggal</label>
													</div>
												</div>
											</div>
										</div>
										<div class="col-sm-12" style="padding-left: 5%; margin-bottom: 15px;">
											<label id="lblbhs_EWS2" style="font-size: 12px; font-weight: bold; font-family: Helvetica">EWS/PEWS/MEWS</label>
											<br />
											<div class="radio-margin" style="display: inline-block; margin-right: 20px;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="EWS2" Value="EWS" ID="rbews2" onclick="enableTXTEWS('TextEWS2','TextPEWS2','TextMEWS2');" />
													<div class="state p-primary-o">
														<label>EWS</label>
													</div>
												</div>
												<asp:TextBox ID="TextEWS2" runat="server" CssClass="form-control" Disabled Style="font-size: 12px; width: 45px; display: inline-block;" placeholder="skor" onkeypress="return CheckNumericnumber();"></asp:TextBox>
											</div>
											<div class="radio-margin" style="display: inline-block; margin-right: 20px;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="EWS2" Value="PEWS" ID="rbpews2" onclick="enableTXTEWS('TextPEWS2','TextEWS2','TextMEWS2');" />
													<div class="state p-primary-o">
														<label>PEWS</label>
													</div>
												</div>
												<asp:TextBox ID="TextPEWS2" runat="server" CssClass="form-control" Disabled Style="font-size: 12px; width: 45px; display: inline-block;" placeholder="skor" onkeypress="return CheckNumericnumber();"></asp:TextBox>
											</div>
											<div class="radio-margin" style="display: inline-block;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="EWS2" Value="MEWS" ID="rbmews2" onclick="enableTXTEWS('TextMEWS2','TextEWS2','TextPEWS2');" />
													<div class="state p-primary-o">
														<label>MEWS</label>
													</div>
												</div>
												<asp:TextBox ID="TextMEWS2" runat="server" CssClass="form-control" Disabled Style="font-size: 12px; width: 45px; display: inline-block;" placeholder="skor" onkeypress="return CheckNumericnumber();"></asp:TextBox>
											</div>
										</div>

									</div>

								</div>

								<div id="commonSectionDisable" style="display: inline-block; width: 500px;">
									<div class="row" style="padding-left: 5%; padding-right: 5%; display: none;">
										<div class="col-sm-2">
											<label style="font-size: 12px; font-weight: bold; font-family: Helvetica">Referral</label>
										</div>
										<div class="col-sm-3" style="padding-right: 0px; padding-left: 0px;">
											<div style="margin-right: 5px; margin-left: 5px;">
												<div class="pretty p-default p-round">
													<asp:RadioButton runat="server" GroupName="referraldis" Value="0" ID="RadioButton3" Checked="true" Enabled="false" />
													<div class="state p-primary-o">
														<label>No</label>
													</div>
												</div>
												&nbsp; &nbsp;
                                           <div class="pretty p-default p-round">
											   <asp:RadioButton runat="server" GroupName="referraldis" Value="1" ID="RadioButton2" Enabled="false" />
											   <div class="state p-primary-o">
												   <label>Yes</label>
											   </div>
										   </div>
											</div>
										</div>
										<div class="col-sm-7" style="padding-left: 0px;">
											<asp:TextBox runat="server" Style="font-size: 12px; font-family: Helvetica; color: #171717; font-weight: normal; height: 23px; width: 100%; border-radius: 2px; border: solid 1px #cdced9;" ID="TextBox1" Disabled="true"> </asp:TextBox>
										</div>
									</div>

									<div class="row" style="padding-left: 5%">
										<div class="col-sm-3">
											<label style="font-size: 12px; font-weight: bold; font-family: Helvetica">
												<label id="lblbhs_consultationfee2">Consultation fee </label>
											</label>
										</div>
									</div>
									<div class="row" style="padding-left: 5%; padding-right: 5%;">
										<div class="col-sm-12">
											<asp:TextBox runat="server" Style="background-color: lightgray; font-size: 14px; font-weight: bold; font-family: Helvetica; height: 32px; width: 100%; max-width: 100%; border-radius: 4px; border: solid 1px #9293a0; padding-left: 11px" ID="txtConsfee" ReadOnly="true"></asp:TextBox>
											<%--<asp:DropDownList ID="DropDownList1" runat="server" style="font-size:14px;font-weight:bold;font-family:Helvetica;height:32px;width:170px;border-radius: 4px;border: solid 1px #9293a0;padding-left:11px"></asp:DropDownList>--%>
										</div>
									</div>
									<div>&nbsp</div>
									<div class="row" style="padding-left: 5%">
										<div class="col-sm-3">
											<label style="font-size: 12px; font-weight: bold; font-family: Helvetica">
												<label id="lblbhs_spesialprice2">Special Price </label>
											</label>
										</div>
									</div>
									<div class="row" style="padding-left: 5%; padding-top: 5px;">
										<div class="col-sm-3">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="pricedis" Value="0" ID="rbNormal" Enabled="false" />
												<div class="state p-primary-o">
													<label id="lblbhs_normalprice2">Normal Price </label>
												</div>
											</div>
										</div>
									</div>
									<div class="row" style="padding-left: 5%; padding-top: 5px;">
										<div class="col-sm-3">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="pricedis" Value="1" ID="rbFree" Enabled="false" />
												<div class="state p-primary-o">
													<label id="lblbhs_freeprice2">Free of Charge </label>
												</div>
											</div>
										</div>
									</div>
									<div class="row" style="padding-left: 5%; padding-top: 5px;">
										<div class="col-sm-2">
											<div class="pretty p-default p-round">
												<asp:RadioButton runat="server" GroupName="pricedis" Value="2" ID="rbdisc" Enabled="false" />
												<div class="state p-primary-o">
													<label id="lblbhs_discount2">Discount </label>
												</div>
											</div>
										</div>
										<div class="col-sm-3" style="display: inline-flex;">
											Rp.
                                        <asp:TextBox runat="server" Style="text-align: right; font-size: 12px; font-family: Helvetica; color: #171717; font-weight: normal; height: 23px; width: 150px; border-radius: 2px; border: solid 1px #cdced9;" Disabled="true" ID="txtdisc" onkeypress="return CheckNumericnumber();" onblur="CalculateTotalFee();"></asp:TextBox>
										</div>
									</div>

									<!-- Move outside of modal
                                <div>&nbsp</div>
                                <div class="row" style="padding-left: 5%">
                                    <div class="col-sm-6">
                                        <label style="font-size: 10px; font-family: Helvetica">
                                            <label id="lblbhs_procedurenotes2">Procedure On Encounter </label>
                                        </label>
                                    </div>
                                </div>
                                <div class="row" style="padding-left: 5%; padding-right: 5%">
                                    <div class="col-sm-12 hidden">
                                        <asp:TextBox runat="server" Style="outline-color: gray; max-width: 100%; width: 100%; border-radius: 4px; resize: none; font-family: Helvetica, Arial, sans-serif" placeholder="Type here..." BorderColor="gray" ID="txtnotes" TextMode="MultiLine" Rows="3" onkeydown="checkTextAreaMaxLength(this,event,'100');" />
                                    </div>
                                    <div class="col-sm-12">
                                        <!-- ## kotak pencarian autocomplete ## -->
									<%--<asp:UpdatePanel runat="server">
                                            <ContentTemplate>--%>
									<%--<div class="row">
                                                    <div class="col-sm-3" style="width: 190px;">
                                                        <div class="has-feedback" style="width: 180px;">
                                                            <asp:TextBox ID="txtItemDiagProc_AC_Dis" runat="server" Placeholder="Select..." Style="width: 180px;" class="autosuggest" onkeydown="return txtOnKeyPressFalseICD();"></asp:TextBox>
                                                            <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px; z-index: 0;"></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="HF_ItemSelectedDiagProc_Dis" runat="server" />
                                                <asp:Button ID="ButtonAjaxSearchDiagProc_Dis" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearchDiagProc_Dis_Click" />--%>
									<%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
									<!-- ## end kotak pencarian autocomplete ## -->

									<%--<br />--%>
									<%-- Grid sudah di pindah di luar modal  %>
                                        <div class="border-submitprocedure" id="divdiagproc_dis" runat="server">
                                            
                                            <%--<asp:GridView ID="Gvw_submitdiagnosticprocedure_dis" runat="server" AutoGenerateColumns="false" CssClass="table-kecil" ShowHeader="false"
                                                DataKeyNames="ProcedureItemId" BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="titik" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="3%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                        <ItemTemplate>
                                                            <i class="fa fa-circle" style="font-size: 5px; vertical-align: middle; margin-right: 2px;"></i>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="90%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hf_id_submitdiagproc" runat="server" Value='<%# Bind("ProcedureItemId") %>'></asp:HiddenField>
                                                            <asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="diagproc_name" runat="server" Text='<%# Bind("ProcedureItemName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Font-Size="12px" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                                                        <ItemTemplate>
                                                            <%--<asp:Button Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="x" OnClick="btnDeleteAllergy_onClick"></asp:Button>--%>
									<%-- <asp:ImageButton ID="btndeletediagproc_dis" runat="server" ImageUrl="~/Images/Icon/ic_delete.svg" OnClick="btndeletediagproc_dis_Click" Style="width: 12px; height: 12px; margin-top: 3px;" Visible='<%# Eval("IsSendHope").ToString() == "0" ? true : false %>' />
                                                            <i class="fa fa-info-circle" style='font-size: 14px; color: cornflowerblue; margin-top: 3px; display:<%# Eval("IsSendHope").ToString() == "0" ? "none" : "block" %>' title="Already send to hope"></i>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>--%>


									<div>&nbsp</div>
									<div class="row" style="padding-left: 5%; padding-right: 5%">
										<div class="col-sm-4"></div>
										<div class="col-sm-8" style="text-align: right">
											<label style="font-size: 12px; font-family: Helvetica; text-align: right">
												<label id="lblbhs_total2" style="color: darkgray;">Total Fee: </label>
											</label>
											<br />
											<asp:TextBox runat="server" ID="lbltotalfeedisable" BorderColor="Transparent" ReadOnly="true" Style="outline-color: transparent; color: darkgrey; font-weight: bold; font-size: 16px; font-family: Helvetica; width: 120px; text-align: right"></asp:TextBox>
										</div>
									</div>
									<div>&nbsp</div>
									<div class="row" style="padding-left: 5%; padding-right: 5%">
										<div class="col-sm-6" style="text-align: left; font-size: 12px;">
											<asp:Button runat="server" CssClass="btn" Style="width: 112px; height: 32px; padding-top: 7px; font-size: 12px; border-radius: 4px; background-color: #171717; color: white" data-dismiss="modal" Text="Cancel" />
										</div>
										<div class="col-sm-6" style="text-align: right; font-size: 12px;">
											<asp:Label ID="Labelnotneed2" runat="server" Text="Tanpa rujukan : " Visible="false"></asp:Label>
											<asp:Button runat="server" aria-hidden="true" ID="btnsubmitdisable" CssClass="btn btnsubmitclass" Style="width: 112px; height: 32px; padding-top: 7px; font-size: 12px; border-radius: 4px; background-color: #4081ed; color: white" Text="Submit & Sign" OnClientClick="submitvalidation();" OnClick="btnSubmitDisable_click" />
										</div>
										<div class="col-sm-12" style="text-align: right; font-size: 12px; padding-top: 15px;" id="divReferalButtonDisable" runat="server" visible="false">
											<asp:Label ID="Labelneed2" runat="server" Text="Dengan rujukan pemeriksaan fisik lanjutan ke RS : " Visible="false"></asp:Label>
											<asp:Button aria-hidden="true" ID="btnsubmitdisableTC" runat="server" CssClass="btn btnsubmitclass" Style="width: 145px; height: 32px; padding-top: 7px; font-size: 12px; border-radius: 4px; background-color: #4081ed; color: white" Text="Submit, Sign & Refer" OnClientClick="submitvalidationTC();" OnClick="btnSubmitDisable_click" />
										</div>
									</div>
								</div>

								<asp:HiddenField ID="HFlagdrug" runat="server" />
								<asp:HiddenField ID="HFlagdrugadd" runat="server" />
								<asp:HiddenField ID="HFlagcons" runat="server" />
								<asp:HiddenField ID="HFlagconsadd" runat="server" />
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>
		</div>

		<div class="modal fade" id="modalnotifalreadymodif" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
			<asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="modal-dialog" style="padding-top: 12%; width: 35%;">
						<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
							<div class="modal-header" style="height: 40px; padding-top: 9px; padding-bottom: 9px; text-align: center; background-color: red; color: white; border-radius: 5px 5px 0px 0px;">
								<span style="font-size: 14px; font-weight: bold">Submit Process Fail!</span>
							</div>
							<div class="modal-body">
								<div style="text-align: center; font-size: 16px;">
									<b>SOAP Already Modified by Other User!</b>
									<br />
									please refresh this page for update data
                                    <br />
									Your data will not be saved
								</div>
								<div style="text-align: center; padding-top: 10px;">
									<%--<asp:Button runat="server" aria-hidden="true" ID="Button1" onclick="window.location.reload();" Style="width: 112px; height: 32px; border-radius: 4px; background-color: #c43d32; color: white" Text="OK" OnClientClick="javascript:shouldsubmit=true" OnClick="btnreloadsave_page" />--%>
									<button class="btn" data-dismiss="modal" style="width: 112px; background-color: red; color: white;">OK</button>
								</div>
							</div>
						</div>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>

		<div class="modal fade" id="modalCompareSOAP" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-dialog" style="top: 2%; width: 90%;">
				<div class="modal-content" style="border-radius: 6px; box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
					<div class="modal-header" style="height: 35px; padding-top: 7px; padding-bottom: 5px">
						<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
						<h5 class="modal-title">
							<asp:Label ID="LabelCompareSoap" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="SOAP Data Recovery : Please select your data"></asp:Label>
						</h5>
					</div>
					<div class="modal-body">
						<asp:UpdatePanel ID="UpdatePanelCompareSOAP" runat="server" UpdateMode="Conditional">
							<ContentTemplate>

								<uc1:CompareSOAP runat="server" ID="CompareSOAPView" />
								<br />
								<div class="row">
									<div class="col-sm-6" style="text-align: left;">
										<asp:Button CssClass="btn btn-success btn-sm" ID="ButtonGetOri" runat="server" Text="Get Data" OnClick="ButtonGetOri_Click" />
									</div>
									<div class="col-sm-6" style="text-align: right;">
										<asp:Button CssClass="btn btn-success btn-sm" ID="ButtonGetBackup" runat="server" Text="Get Data" OnClick="ButtonGetBackup_Click" />
									</div>
								</div>
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>
		</div>
		<%-- ============================================= END MODAL SUBMIT AND SIGN DISABLE ============================================== --%>

		<!-- ##### Modal Update Error ##### -->
		<div class="modal in fade" id="modalError" role="dialog" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 120px;">
			<div class="modal-dialog" style="width: 500px">
				<div class="modal-content" style="border-radius: 7px 7px;">
					<!-- Modal Header -->
					<div class="modal-header" style="padding: 7px;">
						<button type="button" class="close" data-dismiss="modal">×</button>
						<asp:UpdatePanel ID="UpdatePanelErrorTitle" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<h4 class="modal-title" style="color: #d02626; text-align: center;">
									<i class="fa fa-warning"></i>
									<asp:Label ID="LabelErrorJudul" runat="server" Text="Oops..."></asp:Label>
								</h4>
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>

					<!-- Modal body -->
					<div class="modal-body" style="background-color: white; border-radius: 7px 7px; padding-bottom: 0px;">
						<asp:UpdatePanel ID="UpdatePanelError" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<table style="width: 100%;">
									<tr>
										<td style="width: 110px; vertical-align: top; font-weight: bold;">Error Time </td>
										<td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
										<td>
											<asp:Label ID="LabelErrorTime" runat="server" Text="-"></asp:Label>
											, <b>User : </b>
											<asp:Label ID="LabelErrorUser" runat="server" Text="-"></asp:Label>
										</td>
									</tr>
									<tr>
										<td style="width: 110px; vertical-align: top; font-weight: bold;">Message </td>
										<td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
										<td>
											<asp:Label ID="LabelErrorEx" runat="server" Text="-"></asp:Label>
										</td>
									</tr>
								</table>
								<div data-toggle="collapse" data-target="#detailerror" aria-expanded="false" aria-controls="collapseExample" style="text-align: right;">
									<label style="cursor: pointer; color: #d02626;"><< Show Detail</label>
								</div>
								<div class="collapse" id="detailerror">
									<table style="width: 100%;">
										<tr>
											<td style="width: 110px; vertical-align: top; font-weight: bold;">Exception Detail </td>
											<td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
											<td>
												<asp:Label ID="LabelErrorExDet" runat="server" Text="-"></asp:Label>
											</td>
										</tr>
										<tr>
											<td style="width: 110px; vertical-align: top; font-weight: bold;">Source File </td>
											<td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
											<td>
												<asp:Label ID="LabelErrorExSF" runat="server" Text="-"></asp:Label>
											</td>
										</tr>
										<tr>
											<td style="width: 110px; vertical-align: top; font-weight: bold;">Method </td>
											<td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
											<td>
												<asp:Label ID="LabelErrorExMethod" runat="server" Text="-"></asp:Label>
											</td>
										</tr>
										<tr>
											<td style="width: 110px; vertical-align: top; font-weight: bold;">Line </td>
											<td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
											<td>
												<asp:Label ID="LabelErrorExLine" runat="server" Text="-"></asp:Label>
											</td>
										</tr>
									</table>
								</div>
								<table style="width: 100%;">
									<tr>
										<td colspan="3">&nbsp; </td>
									</tr>
									<tr>
										<td colspan="3">
											<div style="font-size: 13px; font-weight: bold; text-align: justify;">
												Silakan dicoba kembali, cek setiap karakter atau data yg sudah diinputkan. 
                                                Jika error masih terjadi dalam periode yang lama, 
                                                silakan hubungi team IT untuk melaporkan error yang terjadi. Terima kasih. 
											</div>
										</td>
									</tr>
								</table>
								<br />
								<div class="row" style="background-color: #faf7fa; padding: 10px; border-radius: 0px 0px 7px 7px;">
									<div class="col-sm-6 text-left">
									</div>
									<div class="col-sm-6 text-right">
										<asp:Button ID="ButtonRetry" Style="width: 100px; background-color: white;" CssClass="btn btn-default btn-sm" runat="server" Text="OK" data-dismiss="modal" />
									</div>
								</div>
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>
		</div>
		<!-- End of Modal Update Error -->
	</body>

	<script type="text/javascript">

		function ICDSuggestionSOAP() {
			$("#MainContent_txtItemICD_AC").autocomplete({
				source: "../Control_Template/AutoCompleteICD.aspx",
				minLength: 0,
				open: function () {
					$('ul.ui-autocomplete').prepend('<li>'
						+ '<table style="width:500px; border-bottom:1px solid lightgrey; background-color:#f4f4f4;">'
						+ '<tr>'
						+ '<td style="width:100%; padding:5px; vertical-align:top; font-weight:bold;"> Disease Classification </td>'
						+ '</tr>'
						+ '</table>'
						+ '</li>');
				},
				position: { my: "left top", at: "left bottom", collision: "flip" },
				select: function (event, ui) {
					//assign value back to the form element
					if (ui.item) {
						$(event.target).val(ui.item.itemName);

						var primary = document.getElementById('<%= txtPrimary.ClientID %>');
						if (primary.value == "") {
							primary.value = ui.item.itemName;
						}
						else {
							primary.value = primary.value + '\n' + ui.item.itemName;
						}
						copytextto();
						$("#MainContent_txtItemICD_AC").val("");
						primary.focus();
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
							+ '<td style="width:100%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
							+ '</tr>'
							+ '</table>')
						.appendTo(ul);
				};
		}

        <%--function DiagProcSuggestionSOAP() {
            $("#MainContent_txtItemDiagProc_AC").autocomplete({
                source: "../Control_Template/AutoCompleteDiagProc.aspx?type=0",
                minLength: 0,
                open: function () {
                    $('ul.ui-autocomplete').prepend('<li>'
                        + '<table style="width:475px; border-bottom:1px solid lightgrey; background-color:#f4f4f4;">'
                        + '<tr>'
                        //+ '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Item Code </td>'
                        + '<td style="width:100%; padding:5px; vertical-align:top; font-weight:bold;"> Procedure & Diagnosis </td>'
                        + '</tr>'
                        + '</table>'
                        + '</li>');
                },
                position: { my: "left top", at: "left bottom", collision: "flip" },
                select: function (event, ui) {
                    //assign value back to the form element
                    if (ui.item) {
                        $(event.target).val(ui.item.SalesItemId);

                        document.getElementById('<%= HF_ItemSelectedDiagProc.ClientID %>').value = ui.item.SalesItemId;
                        document.getElementById('<%= ButtonAjaxSearchDiagProc.ClientID %>').click();
                    }
                }
            })
                .focus(function () {
                    $(this).autocomplete("search");
                })
                .autocomplete("instance")._renderItem = function (ul, item) {

                    return $("<li>")
                        .append('<table style="width:475px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            //+ '<td style="width:20%; padding:5px; vertical-align:top;">' + item.SalesItemCode + '</td>'
                            + '<td style="width:100%; padding:5px; vertical-align:top;">' + item.SalesItemName + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                };
        }--%>

        <%--function DiagProcSuggestionSOAP_Dis() {
            $("#MainContent_txtItemDiagProc_AC_Dis").autocomplete({
                source: "../Control_Template/AutoCompleteDiagProc.aspx?type=0",
                minLength: 0,
                open: function () {
                    $('ul.ui-autocomplete').prepend('<li>'
                        + '<table style="width:475px; border-bottom:1px solid lightgrey; background-color:#f4f4f4;">'
                        + '<tr>'
                        //+ '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Item Code </td>'
                        + '<td style="width:100%; padding:5px; vertical-align:top; font-weight:bold;"> Procedure & Diagnosis </td>'
                        + '</tr>'
                        + '</table>'
                        + '</li>');
                },
                position: { my: "left top", at: "left bottom", collision: "flip" },
                select: function (event, ui) {
                    //assign value back to the form element
                    if (ui.item) {
                        $(event.target).val(ui.item.SalesItemId);

                        document.getElementById('<%= HF_ItemSelectedDiagProc_Dis.ClientID %>').value = ui.item.SalesItemId;
                        document.getElementById('<%= ButtonAjaxSearchDiagProc_Dis.ClientID %>').click();
                    }
                }
            })
                .focus(function () {
                    $(this).autocomplete("search");
                })
                .autocomplete("instance")._renderItem = function (ul, item) {

                    return $("<li>")
                        .append('<table style="width:475px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            //+ '<td style="width:20%; padding:5px; vertical-align:top;">' + item.SalesItemCode + '</td>'
                            + '<td style="width:100%; padding:5px; vertical-align:top;">' + item.SalesItemName + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                };
        }--%>

		function txtOnKeyPressFalseICD() {
			var c = event.keyCode;
			if (c == 13) {
				return false;
			}
		}

		function closeTheLoading() {
			var c = event.keyCode;
			if (c == 27) {
				//alert("close");
				document.getElementById("<%= UpdateProgress15.ClientID %>").style.display = "none";
				document.getElementById("progresslabrad").style.display = "none";
				document.getElementById("loading_HI").style.display = "none";
				document.getElementById("loading_FinalSubmit").style.display = "none";
				BtnDisableFalse();
			}
		}

		$(window).load(function () {
			$(".loadPage").fadeOut("slow");
		});

		var shouldsubmit = true;
		var getbackup = false;
		//window.onbeforeunload = s => "";

		window.onbeforeunload = function () {
			//confirmExit();
			if (shouldsubmit != null) {
				if (!shouldsubmit) {
					if (getbackup != true) {
						return "You have attempted to leave this page.  If you have made any changes to the fields without clicking the Save button, your changes will be lost.  Are you sure you want to exit this page?";
					}
				}
			}
		};

		function confirmExit() {
			//$("[id$='btnAutoSave']").click();
			if (shouldsubmit != null) {
				if (!shouldsubmit) {
					return "You have attempted to leave this page.  If you have made any changes to the fields without clicking the Save button, your changes will be lost.  Are you sure you want to exit this page?";
				}
			}
		}

		function SaveSOAPtoLocal(data) {
            <%--var x = document.getElementById("<%= hfsoapstringsavetolocal.ClientID %>").value;--%>
			localStorage.setItem("BackupSOAP", JSON.stringify(data));
		}
		//js nya ga jalan inline runing
		function GetSOAPfromLocal() {
			var data = localStorage.getItem("BackupSOAP");
			document.getElementById("<%= hfsoapstringgetfromlocal.ClientID %>").value = JSON.stringify(data);
			//$("#MainContent_hfsoapstringgetfromlocal").val(JSON.stringify(data));
		}

        <%--function autosave() {
            //$('#modalconfirmleavepage').modal('show');
            document.getElementById('<%=btnAutoSave.ClientID%>').click();
            return true;
        }--%>

		$(document).ready(function () {
			notificationOption();
		});

		function notificationOption() {
			toastr.options.positionClass = "toast-top-right";
		}

		function notification() {
			toastr.success('The data has been saved.', 'Success');
			toastr.options.positionClass = "toast-top-right";
		}

		function warningnotificationOption() {
			toastr.options.positionClass = "toast-top-right";
			toastr.options.closeButton = true;
			toastr.options.timeOut = 0;
			toastr.options.extendedTimeOut = 0;
			toastr.options.tapToDismiss = true;
		}

		function notificationMandatorySOAP(msg) {
			warningnotificationOption();
			toastr.warning(msg + ' <br /> <button type="button" class="btn btn-danger btn-sm" style="height: 25px; padding-top: 3px; width: 55px; float:right;">OK</button>', 'Submit Alert!');
		}

		function Preview() {
			$('#modalPreview').modal('show');
			var Button = "<%=btnPreview.ClientID %>";
			document.getElementById(Button).click();
			return true;
		}

		function OnClickICD() {
			if (popupicd.style.display == "none") {
				popupicd.style.display = "";
				//$("[id$='txtitemicd']").val('click to hide search');
				$("[id$='txtSearchItemICD']").focus();
			}
			else {
				$("[id$='txtitemicd']").val('Select');
				popupicd.style.display = "none";
			}
		}

		function txtOnKeyPressICD() {

			var FlagFocus = document.getElementById('<%= HiddenFlagSearchFocus.ClientID %>');
			FlagFocus.value = "ICDfocus";

			var c = event.keyCode;
			if (c == 13) {
				var Button = "<%=btnfindicd.ClientID %>";
				document.getElementById(Button).click();
				return false;
			}
		}

		function PreviewFA() {
			//switchBahasaPC();
			$('#modalPreviewFA').modal('show');
			return true;
		}

		function ShowImunisasi() {
			$('#modalImunisasi').modal('show');
			return true;
		}

		function HideImunisasi() {
			$('#modalImunisasi').modal('hide');
			return true;
		}

		function HidePreviewMedication() {
			//$('#modalEditMedication').modal('hide');
			$('#modalEditMedicationIframe').modal('hide');
			return true;
		}

		function HidePreviewIllness() {
			//$('#modalEditIllness').modal('hide');
			$('#modalEditIllnessIframe').modal('hide');
			return true;
		}

		function HidePreviewEndemic() {
			$('#modalEditEndemic').modal('hide');
			return true;
		}

		function HidePreviewNutrition() {
			$('#modalEditNutrition').modal('hide');
			return true;
		}

		function HidePreviewTravel() {
			$('#modalRekomendasiTravel').modal('hide');
			return false;
		}

		function HidePreviewPhysical() {
			$('#modalEditPhysical').modal('hide');
			return true;
		}

		function PreviewMedication() {
			//$('#modalEditMedication').modal({ backdrop: 'static', keyboard: false });
			$('#modalEditMedicationIframe').modal({ backdrop: 'static', keyboard: false });
			//$('#modalEditMedicationIframe').modal('show');
			return true;
		}

		function PreviewIllnesss() {
			//$('#modalEditIllness').modal({ backdrop: 'static', keyboard: false });
			$('#modalEditIllnessIframe').modal({ backdrop: 'static', keyboard: false });
			//$('#modalEditIllnessIframe').modal('show');
			return true;
		}

		function PreviewEndemic() {
			$('#modalEditEndemic').modal({ backdrop: 'static', keyboard: false });
			return true;
		}

		function PreviewNutrition() {
			$('#modalEditNutrition').modal({ backdrop: 'static', keyboard: false });
			return true;
		}

		function PreviewPhysical() {
			$('#modalEditPhysical').modal({ backdrop: 'static', keyboard: false });
			return true;
		}

		function PreviewCopy() {
			$('#modalCopySOAP').modal('show');
            <%--var Button = "<%=btncopysoap.ClientID %>";
            document.getElementById(Button).click();--%>
			return true;
		}

		function PreviewCopyPrescription() {
			$('#modalCopyPrescription').modal('show');
            <%--var Button = "<%=btncopysoap.ClientID %>";
            document.getElementById(Button).click();--%>
			return true;
		}

		function showdetailtravel() {
			var dvrowtravel = document.getElementById("rowdetail");
			dvrowtravel.style.display = "block";

			var dvdateanticipated = document.getElementById("divdateanticipated");
			dvdateanticipated.style.display = "none";
		}

		function hidedetailtravel() {
			var dvrowtravel = document.getElementById("rowdetail");
			dvrowtravel.style.display = "none";

			var dvdateanticipated = document.getElementById("divdateanticipated");
			dvdateanticipated.style.display = "none";
		}

		function showdetailtravelanticipated() {
			var dvrowtravel = document.getElementById("rowdetail");
			dvrowtravel.style.display = "block";
			var dvdateanticipated = document.getElementById("divdateanticipated");
			dvdateanticipated.style.display = "inline";
		}

		function RemoveTravel() {
			var txtdatescheduletravel = document.getElementById("<%=txtdatescheduletravel.ClientID %>");
			var txtdatefittofly = document.getElementById("<%=txtdatefittofly.ClientID %>");
			var ddlescort = document.getElementById("<%=ddlescort.ClientID %>");
			var txtTravelRecommendation = document.getElementById("<%=txtTravelRecommendation.ClientID %>");
			var lnkModalRekomendasi = document.getElementById("lnkModalRekomendasi");

			var hdnSchedule_travel = document.getElementById("<%=hdnSchedule_travel.ClientID %>");
			var hdnCondition_travel = document.getElementById("<%=hdnCondition_travel.ClientID %>");
			var hdnSeating_type = document.getElementById("<%=hdnSeating_type.ClientID %>");
			var hdnEscort_type = document.getElementById("<%=hdnEscort_type.ClientID %>");
			var hdnSpecial_Needs = document.getElementById("<%=hdnSpecial_Needs.ClientID %>");
			var hdncondition_date = document.getElementById("<%=hdncondition_date.ClientID %>");
			var hdnescort_ddl = document.getElementById("<%=hdnescort_ddl.ClientID %>");

			var rbtravel1 = document.getElementById("<%=rbtravel1.ClientID %>");
			var rbtravel2 = document.getElementById("<%=rbtravel2.ClientID %>");
			var rbtravel3 = document.getElementById("<%=rbtravel3.ClientID %>");

			var rbseating1 = document.getElementById("<%=rbseating1.ClientID %>");
			var rbseating2 = document.getElementById("<%=rbseating2.ClientID %>");
			var rbseating3 = document.getElementById("<%=rbseating3.ClientID %>");
			var rbseating4 = document.getElementById("<%=rbseating4.ClientID %>");

			var rbescort1 = document.getElementById("<%=rbescort1.ClientID %>");
			var rbescort2 = document.getElementById("<%=rbescort2.ClientID %>");
			var rbescort3 = document.getElementById("<%=rbescort3.ClientID %>");

			var chkSpecialNeeds1 = document.getElementById("<%=chkSpecialNeeds1.ClientID %>");
			var chkSpecialNeeds2 = document.getElementById("<%=chkSpecialNeeds2.ClientID %>");
			var chkSpecialNeeds3 = document.getElementById("<%=chkSpecialNeeds3.ClientID %>");
			var chkSpecialNeeds4 = document.getElementById("<%=chkSpecialNeeds4.ClientID %>");

			txtdatescheduletravel.value = "";
			txtdatefittofly.value = "";
			txtTravelRecommendation.value = "";

			hdnSchedule_travel.value = "";
			hdnCondition_travel.value = "";
			hdnSeating_type.value = "";
			hdnEscort_type.value = "";
			hdnSpecial_Needs.value = "";
			hdncondition_date.value = "";
			hdnescort_ddl.value = "";

			rbtravel1.checked = false;
			rbtravel2.checked = false;
			rbtravel3.checked = false;

			rbseating1.checked = false;
			rbseating2.checked = false;
			rbseating3.checked = false;
			rbseating4.checked = false;

			rbescort1.checked = false;
			rbescort2.checked = false;
			rbescort3.checked = false;

			chkSpecialNeeds1.checked = false;
			chkSpecialNeeds2.checked = false;
			chkSpecialNeeds3.checked = false;
			chkSpecialNeeds4.checked = false;

			hidedetailtravel();
			if (lnkModalRekomendasi != null) {
				lnkModalRekomendasi.style.display = "inline";
			}
			document.getElementById("divtravel").style.display = "none";
		}

		function SaveTravel() {
			var txtdatescheduletravel = document.getElementById("<%=txtdatescheduletravel.ClientID %>");
			var txtdatefittofly = document.getElementById("<%=txtdatefittofly.ClientID %>");
			var ddlescort = document.getElementById("<%=ddlescort.ClientID %>");
			var txtTravelRecommendation = document.getElementById("<%=txtTravelRecommendation.ClientID %>");
			var lnkModalRekomendasi = document.getElementById("lnkModalRekomendasi");

			var hdnSchedule_travel = document.getElementById("<%=hdnSchedule_travel.ClientID %>");
			var hdnCondition_travel = document.getElementById("<%=hdnCondition_travel.ClientID %>");
			var hdnSeating_type = document.getElementById("<%=hdnSeating_type.ClientID %>");
			var hdnEscort_type = document.getElementById("<%=hdnEscort_type.ClientID %>");
			var hdnSpecial_Needs = document.getElementById("<%=hdnSpecial_Needs.ClientID %>");
			var hdncondition_date = document.getElementById("<%=hdncondition_date.ClientID %>");
			var hdnescort_ddl = document.getElementById("<%=hdnescort_ddl.ClientID %>");

			var rbtravel1 = document.getElementById("<%=rbtravel1.ClientID %>");
			var rbtravel2 = document.getElementById("<%=rbtravel2.ClientID %>");
			var rbtravel3 = document.getElementById("<%=rbtravel3.ClientID %>");

			var rbseating1 = document.getElementById("<%=rbseating1.ClientID %>");
			var rbseating2 = document.getElementById("<%=rbseating2.ClientID %>");
			var rbseating3 = document.getElementById("<%=rbseating3.ClientID %>");
			var rbseating4 = document.getElementById("<%=rbseating4.ClientID %>");

			var rbescort1 = document.getElementById("<%=rbescort1.ClientID %>");
			var rbescort2 = document.getElementById("<%=rbescort2.ClientID %>");
			var rbescort3 = document.getElementById("<%=rbescort3.ClientID %>");

			var chkSpecialNeeds1 = document.getElementById("<%=chkSpecialNeeds1.ClientID %>");
			var chkSpecialNeeds2 = document.getElementById("<%=chkSpecialNeeds2.ClientID %>");
			var chkSpecialNeeds3 = document.getElementById("<%=chkSpecialNeeds3.ClientID %>");
			var chkSpecialNeeds4 = document.getElementById("<%=chkSpecialNeeds4.ClientID %>");

			var flagddlescort = "";
			var flagtglcondition = "";

			if (txtdatescheduletravel.Text != "") {
				hdnSchedule_travel.value = txtdatescheduletravel.value;
				hdnCondition_travel.value = "";
				hdnSeating_type.value = "";
				hdnEscort_type.value = "";
				hdnSpecial_Needs.value = "";
			}
			if (rbtravel2.checked) {
				hdnCondition_travel.value = "Not fit to fly as scheduled";
			}
			else {
				if (rbtravel1.checked)
					hdnCondition_travel.value = "Fit to fly as scheduled";
				else if (rbtravel3.checked) {
					hdnCondition_travel.value = "Anticipated date fit to fly";
					flagtglcondition = " : " + txtdatefittofly.value;
					hdncondition_date.value = txtdatefittofly.value;
				}

				if (rbseating1.checked)
					hdnSeating_type.value = "Commercial flight regular seating";
				else if (rbseating2.checked)
					hdnSeating_type.value = "Commercial flight Business class";
				else if (rbseating3.checked)
					hdnSeating_type.value = "Stretcher Case";
				else if (rbseating4.checked)
					hdnSeating_type.value = "Air-ambulance";

				if (rbescort1.checked)
					hdnEscort_type.value = "Unescorted";
				else if (rbescort2.checked)
					hdnEscort_type.value = "non-Medical Escort";
				else if (rbescort3.checked) {
					hdnEscort_type.value = "Medical Escort";
					flagddlescort = " : " + ddlescort.options[ddlescort.selectedIndex].text;
					hdnescort_ddl.value = ddlescort.options[ddlescort.selectedIndex].text;
				}

				if (chkSpecialNeeds1.checked)
					hdnSpecial_Needs.value = "Wheel Chair Assistance,";
				if (chkSpecialNeeds2.checked)
					hdnSpecial_Needs.value = hdnSpecial_Needs.value + "Oxygen Supplementation,";
				if (chkSpecialNeeds3.checked)
					hdnSpecial_Needs.value = hdnSpecial_Needs.value + "Need Mechanical Ventilation,";
				if (chkSpecialNeeds4.checked)
					hdnSpecial_Needs.value = hdnSpecial_Needs.value + "Need Vacuum Mattress,";

			}

			if (hdnSchedule_travel.value == "" && hdnCondition_travel.value == "" && hdnSeating_type.value == "" && hdnEscort_type.value == "" && hdnSpecial_Needs.value == "") {
				if (lnkModalRekomendasi != null) {
					lnkModalRekomendasi.style.display = "inline";
				}
			}
			else {
				if (hdnSchedule_travel.value != "")
					txtTravelRecommendation.value = "-" + " This patient is scheduled to travel on: " + hdnSchedule_travel.value;
				else
					txtTravelRecommendation.value = "-" + " This patient is scheduled to travel on: - ";

				if (hdnCondition_travel.value != "")
					txtTravelRecommendation.value = txtTravelRecommendation.value + "\n" + "- " + hdnCondition_travel.value + flagtglcondition;
				else
					txtTravelRecommendation.value = txtTravelRecommendation.value + "\n" + "- " + "Patient's Condition: - ";

				if (hdnSeating_type.value != "")
					txtTravelRecommendation.value = txtTravelRecommendation.value + "\n" + "- " + "Recommended Flight Seating Type: " + hdnSeating_type.value;
				else
					txtTravelRecommendation.value = txtTravelRecommendation.value + "\n" + "- " + "Recommended Flight Seating Type: - ";

				if (hdnEscort_type.value != "")
					txtTravelRecommendation.value = txtTravelRecommendation.value + "\n" + "- " + "Escort Type: " + hdnEscort_type.value + flagddlescort;
				else
					txtTravelRecommendation.value = txtTravelRecommendation.value + "\n" + "- " + "Escort Type: - ";

				if (hdnSpecial_Needs.value.charAt(hdnSpecial_Needs.value.length - 1) == ',')
					hdnSpecial_Needs.value = hdnSpecial_Needs.value.substring(0, hdnSpecial_Needs.value.length - 1);

				if (hdnSpecial_Needs.value != "")
					txtTravelRecommendation.value = txtTravelRecommendation.value + "\n" + "- " + "Special Need: " + hdnSpecial_Needs.value;
				else
					txtTravelRecommendation.value = txtTravelRecommendation.value + "\n" + "- " + "Special Need: - ";

				if (lnkModalRekomendasi != null) {
					lnkModalRekomendasi.style.display = "none";
				}
				document.getElementById("divtravel").style.display = "block";
				txtTravelRecommendation.focus();
			}

			$('#modalRekomendasiTravel').modal('hide');

			return false;
		}

		function hidecopy() {
			var copy = $("[id$='hfsavemode']").val();
			var configprescriptionHOPE = $("[id$='hfprescriptionHOPE']").val();
			if (copy == '1') {
				var dvPassport = document.getElementById("iconcopy");
				dvPassport.style.display = "none";

				var dvPassport2 = document.getElementById("btnsaveasdraft");
				dvPassport2.style.display = "none";

				var dvPassport3 = document.getElementById("iconcopydrugs");
				dvPassport3.style.display = "none";
			}
			else {
				var dvPassport = document.getElementById("iconcopy");
				dvPassport.style.display = "";

				var dvPassport2 = document.getElementById("btnsaveasdraft");
				dvPassport2.style.display = "";

				if (configprescriptionHOPE == 'TRUE') {
					var dvPassport3 = document.getElementById("iconcopydrugs");
					dvPassport3.style.display = "";
				}
			}
		}

		function hidecopysoapbutton() {
			var btncopy = document.getElementById("iconcopy");
			btncopy.style.display = "none";
		}

		function validateclosetag() {
			var valueAnamnesis = document.getElementById("MainContent_Anamnesis").value;
			var res = valueAnamnesis;
			res = res.replace(/[^\x00-\x7F]/g, "?");
			res = res.replace(/</g, " < ").replace(/>/g, " > ");
			$("[id$='Anamnesis']").val(res);

			var valueComplaint = document.getElementById("MainContent_Complaint").value;
			var resComplaint = valueComplaint;
			resComplaint = resComplaint.replace(/[^\x00-\x7F]/g, "?");
			resComplaint = resComplaint.replace(/</g, " < ").replace(/>/g, " > ");
			$("[id$='Complaint']").val(resComplaint);

			var valuetxtOthers = document.getElementById("MainContent_txtOthers").value;
			var restxtOthers = valuetxtOthers;
			restxtOthers = restxtOthers.replace(/[^\x00-\x7F]/g, "?");
			restxtOthers = restxtOthers.replace(/</g, " < ").replace(/>/g, " > ");
			$("[id$='txtOthers']").val(restxtOthers);

			var valuetxtPrimary = document.getElementById("MainContent_txtPrimary").value;
			var restxtPrimary = valuetxtPrimary;
			restxtPrimary = restxtPrimary.replace(/[^\x00-\x7F]/g, "?");
			restxtPrimary = restxtPrimary.replace(/</g, " < ").replace(/>/g, " > ");
			$("[id$='txtPrimary']").val(restxtPrimary);

			var valuetxtPlanning = document.getElementById("MainContent_txtPlanning").value;
			var restxtPlanning = valuetxtPlanning;
			restxtPlanning = restxtPlanning.replace(/[^\x00-\x7F]/g, "?");
			restxtPlanning = restxtPlanning.replace(/</g, " < ").replace(/>/g, " > ");
			$("[id$='txtPlanning']").val(restxtPlanning);

			var valuetxthasil = document.getElementById("MainContent_txtHasilTindakan").value;
			var restxtHasilTindakan = valuetxthasil;
			restxtHasilTindakan = restxtHasilTindakan.replace(/[^\x00-\x7F]/g, "?");
			restxtHasilTindakan = restxtHasilTindakan.replace(/</g, " < ").replace(/>/g, " > ");
			$("[id$='txtHasilTindakan']").val(restxtHasilTindakan);
		}

		function disablebuttonsave() {
			shouldsubmit = true;

			validateclosetag();
			var dvPassport2 = document.getElementById("btnsaveasdraft");
			dvPassport2.style.display = "none";
		}

		function BtnDisableTrue() {
			var btn = document.getElementsByClassName("btnsubmitclass");
			var i;
			for (i = 0; i < btn.length; i++) {
				btn[i].classList.add("disabled-form");
			}
		}

		function BtnDisableFalse() {
			var btn = document.getElementsByClassName("btnsubmitclass");
			var i;
			for (i = 0; i < btn.length; i++) {
				btn[i].classList.remove("disabled-form");
			}
		}

		function submitvalidation() {

			BtnDisableTrue();
			//setTimeout(BtnDisableFalse, 15000);
			document.getElementById("<%=HFflagfinalsubmitloading.ClientID %>").value = "true";

			validateclosetag();
			var diskoncek = document.getElementById("<%=rbPrice3.ClientID %>");
			var diskontext = document.getElementById("<%=txtDiscount.ClientID %>");

			if (diskoncek.checked) {
				if (diskontext.value == "") {
					diskontext.focus();
					diskontext.placeholder = "This field is mandatory..."
					diskontext.classList.add("placeholderred");
					return false;
				}
			}
			shouldsubmit = true
		}

		function submitvalidationTC() {

			BtnDisableTrue();
			//setTimeout(BtnDisableFalse, 15000);
			document.getElementById("<%=HFflagfinalsubmitloading.ClientID %>").value = "true";

			validateclosetag();
			var diskoncek = document.getElementById("<%=rbPrice3.ClientID %>");
			var diskontext = document.getElementById("<%=txtDiscount.ClientID %>");

			if (diskoncek.checked) {
				if (diskontext.value == "") {
					diskontext.focus();
					diskontext.placeholder = "This field is mandatory..."
					diskontext.classList.add("placeholderred");
					return false;
				}
			}
			shouldsubmit = true
			document.getElementById("<%= hf_flagrujukan_aido.ClientID %>").value = "aido";
		}

		function isEmpty(str) {
			return !str.trim().length;
		}

		function ModalSubmit() {

			// Diagnostic and Procedure 
			var dp_diag = document.getElementById("MainContent_StdPlanning_dp_diag");
			var dp_proc = document.getElementById("MainContent_StdPlanning_dp_proc");
			var item_diagnostic_fo = document.getElementById("MainContent_StdPlanning_GridView_DiagnosticList_FutureOrder");
			var item_procedure_fo = document.getElementById("MainContent_StdPlanning_GridView_ProcedureList_FutureOrder");
			var txtOtherDiagnostic_fo = document.getElementById("MainContent_StdPlanning_txtplanningotherDiagnostic_FutureOrder");
			var txtOtherProcedure_fo = document.getElementById("MainContent_StdPlanning_txtplanningotherProcedure_FutureOrder");
			if (dp_diag.value == "" && (item_diagnostic_fo != null || (txtOtherDiagnostic_fo != null && txtOtherProcedure_fo.value != ""))) { //REVISI OTHERS DATE
				notificationMandatorySOAP("Diagnostic Future Order date is mandatory...");
				dp_diag.style.border = "1px solid red";
				return false;
			}

			if (dp_proc.value == "" && (item_procedure_fo != null || (txtOtherDiagnostic_fo != null && txtOtherProcedure_fo.value != ""))) { //REVISI OTHERS DATE
				notificationMandatorySOAP("Procedure Future Order date is mandatory...");
				dp_proc.style.border = "1px solid red";
				return false;
			}

			validateclosetag();
			document.getElementById('div_mims_reason').style.display = "none";
			document.getElementById("<%= hfAdmissionNo.ClientID %>").value = document.getElementById("MainContent_PatientCard_lblAdmissionNo").innerText;
			document.getElementById("<%= hfMRNo.ClientID %>").value = document.getElementById("MainContent_PatientCard_localMrNo").innerText;

			var configsoap = $("[id$='hfmandatorySOAP']").val();
			var valueComplaint = $("[id$='Complaint']").val();
			var valueAnamnesis = $("[id$='Anamnesis']").val();
			var valuetxtPrimary = $("[id$='txtPrimary']").val();
			var valuetxtOthers = $("[id$='txtOthers']").val();
			var valuetxtPlanning = $("[id$='txtPlanning']").val();
			if (configsoap == 'TRUE') {
				if (isEmpty(valueComplaint) == false && isEmpty(valueAnamnesis) == false && isEmpty(valuetxtPrimary) == false && isEmpty(valuetxtOthers) == false && isEmpty(valuetxtPlanning) == false) {

					var gender = document.getElementById("<%= hfgender.ClientID %>");
					var ageyear = document.getElementById("<%= hfage.ClientID %>");
					var radiohamilno = document.getElementById("<%= Radiohamilno.ClientID %>");
					var radiohamilyes = document.getElementById("<%= Radiohamilyes.ClientID %>");
					if (gender.value == 2 && (ageyear.value > 17 && ageyear.value < 60)) {
						if (radiohamilno.checked == false && radiohamilyes.checked == false) {
							radiohamilno.focus();
							document.getElementById("<%= divpregnat.ClientID %>").style.backgroundColor = "#ffe4c4";
							notificationMandatorySOAP("Kolom Kehamilan wajib diisi...");
							return false;
						}
					}

					var clindiag = document.getElementById("MainContent_StdPlanning_txtclinicaldiagnosis");
					if (clindiag != null & clindiag.value.length > 400) {
						notificationMandatorySOAP("Clinical Diagnosis more than 400 character...");
						return false;
					}

					var div_labradFO = document.getElementById("MainContent_StdPlanning_divFutureOrder");
					if (div_labradFO != null & div_labradFO.style.display == "block") {
						var dp_lab_fo = document.getElementById("MainContent_StdPlanning_dp_labFutureOrder");
						var dp_rad_fo = document.getElementById("MainContent_StdPlanning_dp_radFutureOrder");
						var item_lab_fo = document.getElementById("MainContent_StdPlanning_Repeater1_FutureOrder");
						var item_rad_fo = document.getElementById("MainContent_StdPlanning_rptRadiology_FutureOrder");
						var txtOthersLab_fo = document.getElementById("MainContent_StdPlanning_txtplanningotherLab_FutureOrder");//REVISI OTHERS DATE
						var txtOthersRad_fo = document.getElementById("MainContent_StdPlanning_txtplanningotherRad_FutureOrder");//REVISI OTHERS DATE

						dp_lab_fo.style.border = "";
						dp_rad_fo.style.border = "";
						if (dp_lab_fo.value == "" && (item_lab_fo != null || (txtOthersLab_fo != null && txtOthersLab_fo.value != ""))) { //REVISI OTHERS DATE
							notificationMandatorySOAP("Lab Future Order date is mandatory...");
							dp_lab_fo.style.border = "1px solid red";
							return false;
						}
						if (dp_rad_fo.value == "" && (item_rad_fo != null || (txtOthersRad_fo != null && txtOthersRad_fo.value != ""))) { //REVISI OTHERS DATE
							notificationMandatorySOAP("Rad Future Order date is mandatory...");
							dp_rad_fo.style.border = "1px solid red";
							return false;
						}
					}

					if (checkmandatoryradioSOAP() == true) {
						var savemode = $("[id$='hfsavemode']").val();
						if (savemode == '1') {
							//$('#modalsubmitDisable').modal('show');
							document.getElementById("<%=ButtonSubmitdisableHidden.ClientID %>").click();
						}
						else {
							//$('#modalsubmit').modal('show');
							document.getElementById("<%=ButtonSubmitHidden.ClientID %>").click();
						}
					}
				}
				else {
					notificationMandatorySOAP("SOAP can not be empty!");

					var textKeluhan = document.getElementById("<%=Complaint.ClientID %>");
					var textAnamnesis = document.getElementById("<%=Anamnesis.ClientID %>");
					var textO = document.getElementById("<%=txtPrimary.ClientID %>");
					var textA = document.getElementById("<%=txtOthers.ClientID %>");
					var textP = document.getElementById("<%=txtPlanning.ClientID %>");

					if (isEmpty(textP.value) == true) {
						textP.value = "";
						textP.focus();
						textP.placeholder = "This field is mandatory..."
						textP.classList.add("placeholderred");
						//return false;
					}
					else {
						textP.placeholder = "Type here..."
					}

					if (isEmpty(textA.value) == true) {
						textA.value = "";
						textA.focus();
						textA.placeholder = "This field is mandatory..."
						textA.classList.add("placeholderred");
						//return false;
					}
					else {
						textA.placeholder = "Type here..."
					}

					if (isEmpty(textO.value) == true) {
						textO.value = "";
						textO.focus();
						textO.placeholder = "This field is mandatory..."
						textO.classList.add("placeholderred");
						//return false;
					}
					else {
						textO.placeholder = "Type here..."
					}

					if (isEmpty(textAnamnesis.value) == true) {
						textAnamnesis.value = "";
						textAnamnesis.focus();
						textAnamnesis.placeholder = "This field is mandatory..."
						textAnamnesis.classList.add("placeholderred");
						//return false;
					}
					else {
						textAnamnesis.placeholder = "Type here..."
					}

					if (isEmpty(textKeluhan.value) == true) {
						textKeluhan.value = "";
						textKeluhan.focus();
						textKeluhan.placeholder = "This field is mandatory..."
						textKeluhan.classList.add("placeholderred");
						//return false;
					}
					else {
						textKeluhan.placeholder = "Type here..."
					}
				}
			}
			return true;
		}
		function enableref() {
			$("[id$='txtreferal']").removeAttr("Disabled");
		}
		function disableref() {
			$("[id$='txtreferal']").attr('disabled', 'disabled');
		}
		function freecharges() {
			$("[id$='txtDiscount']").attr('disabled', 'disabled');
			$("[id$='txttotalfee']").val('0');
		}

		function normalcharges() {
			var consfeestring = $("[id$='ddl_consultationfee'] option:selected").text();
			var totalfee = consfeestring.split(' ~ Rp ');
			$("[id$='txtDiscount']").attr('disabled', 'disabled');

			$("[id$='txttotalfee']").val(totalfee[1]);
			var Button = "<%=rbPrice1.ClientID %>";
			document.getElementById(Button).click();
		}

		function CheckNumericnumber() {
			return event.keyCode >= 48 && event.keyCode <= 57;
		}

		function checkenter() {
			return event.keyCode != 13;
		}

		function CheckBack() {
			return event.keyCode != 92;
		}

		function enabledisc() {
			var consfeestring = $("[id$='ddl_consultationfee'] option:selected").text();
			var totalfee = consfeestring.split(' ~ Rp ');

			$("[id$='txttotalfee']").val(totalfee[1]);
			$("[id$='txtDiscount']").val('');
			$("[id$='txtDiscount']").removeAttr("Disabled");
			$("[id$='txtDiscount']").focus();
		}

		function CalculateTotalFee() {
			var consfeestring = $("[id$='ddl_consultationfee'] option:selected").text();
			if (consfeestring != "") {
				var totalfee = consfeestring.split(' ~ Rp ');
				var totalfeeint = parseInt(totalfee[1].replace(/\,/g, ''));

				var disc = $("[id$='txtDiscount']").val();
				if (disc.length > 0) {
					var parsedisc = parseInt(disc);

					if (parsedisc > totalfeeint) {
						alert('discount greater than consultation fee');
					}
					else {
						var cal = totalfeeint - parsedisc;
						var total = addCommas(cal.toString());
						$("[id$='txttotalfee']").val(total);
					}
				}
				else {
					$("[id$='txttotalfee']").val(totalfee[1]);
				}
			}
		}

		function addCommas(nStr) {
			nStr += '';
			var x = nStr.split(',');
			var x1 = x[0];
			var x2 = x.length > 1 ? ',' + x[1] : '';
			var rgx = /(\d+)(\d{3})/;
			while (rgx.test(x1)) {
				x1 = x1.replace(rgx, '$1' + ',' + '$2');
			}
			return x1 + x2;
		}

		function clicktxtbox() {
			var Button = "<%=Anamnesis.ClientID %>";
			document.getElementById(Button).style.height = "55px";
			//document.getElementById(Button).style.height = (25 + Button.scrollHeight) + "px";
		}

		function AutoExpand(txtbox) {
			txtbox.style.height = "1px";
			txtbox.style.height = (25 + txtbox.scrollHeight) + "px";
			//alert(txtbox.scrollHeight);
		}

		function minexpand(txtbox) {
			txtbox.style.height = "1px";
			txtbox.style.height = (25 + txtbox.scrollHeight) + "px";
			//alert(txtbox.scrollHeight);
		}

		function keypressdoctor() {
			var c = event.keyCode;
			if (c == 13) {
				var Button = "<%=btnsearchDoctor.ClientID %>";
				document.getElementById(Button).click();
			}
		}

		function keypressdoctorprescription() {
			var c = event.keyCode;
			if (c == 13) {
				var Button = "<%=btnSearchDoctor_CopyPrescription.ClientID %>";
				document.getElementById(Button).click();
			}
		}

		function savedraft() {
			alert('Save as draft successful');
			location.reload();
		}

		function taken(val) {
			$('#modalsubmitDisable').modal('hide');
			//toastr.info('Prescription already taken by Pharmacy','Submit Info!');
			//alert('Prescription already taken by Pharmacy');
			location.reload();
			//__doPostBack('', '');
			//window.location.replace(val);
		}

		function successsubmit() {
			$('#modalsubmitDisable').modal('hide');
			alert('Submit and sign successful');
			location.reload();
			//__doPostBack('', '');
			//window.location.replace(val);
		}

		function takennormal() {
			$('#modalsubmit').modal('hide');
			//alert('Prescription already taken by Pharmacy');
			location.reload();
			//__doPostBack('', '');
			//window.location.replace(val);
		}

		function successsubmitnormal() {
			$('#modalsubmit').modal('hide');
			alert('Submit and sign successful');
			location.reload();
			//__doPostBack('', '');
			//window.location.replace(val);
		}

		function ShowHideDiv() {
			document.getElementById("<%=rbPengobatan2.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbPengobatan2.ClientID %>");
			var dvPassport = document.getElementById("dvPengobatan");
			if (chkYes.checked) {
				dvPassport.style.display = "";
			}
		}
		function HideDiv() {
            //document.getElementById("<%=rbPengobatan1.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbPengobatan1.ClientID %>");
			var dvPassport = document.getElementById("dvPengobatan");
			if (chkYes.checked) {
				dvPassport.style.display = "none";
			}
		}

		function ShowHideDiv2() {
			document.getElementById("<%=rbpribadi2.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbpribadi2.ClientID %>");
			var dvPassport = document.getElementById("dvPenyakitPribadi");
			if (chkYes.checked) {
				dvPassport.style.display = "";
			}
		}

		function HideDiv2() {
            //document.getElementById("<%=rbpribadi1.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbpribadi1.ClientID %>");
			var dvPassport = document.getElementById("dvPenyakitPribadi");
			if (chkYes.checked) {
				dvPassport.style.display = "none";
			}
		}

		function ShowHideDiv3() {
			document.getElementById("<%=rbkeluarga2.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbkeluarga2.ClientID %>");
			var dvPassport = document.getElementById("dvPenyakitKeluarga");
			if (chkYes.checked) {
				dvPassport.style.display = "";
			}
		}

		function HideDiv3() {
            //document.getElementById("<%=rbkeluarga1.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbkeluarga1.ClientID %>");
			var dvPassport = document.getElementById("dvPenyakitKeluarga");
			if (chkYes.checked) {
				dvPassport.style.display = "none";
			}
		}

		function chkNoEndemic() {
			var chkNo = document.getElementById("<%=rbkunjungan1.ClientID%>");
			var chkYes = document.getElementById("<%=rbkunjungan2.ClientID%>");
			var txtEndemic = $("#MainContent_txtEndemic").val();
			var chkNoSkrining = document.getElementById("<%=rbskriningno.ClientID%>");
			var chkYesSkrining = document.getElementById("<%=rbskriningyes.ClientID%>");
			var dvE = document.getElementById("dvEndemic");
			if (txtEndemic.length > 0) {
				if (confirm("There's data in the form, are you sure you want to change to 'No'?")) {
					$("#MainContent_txtEndemic").val("")
					dvE.style.display = "none";
					chkNoSkrining.disabled = false;
				}
				else
					chkYes.click();
			}
			else {
				dvE.style.display = "none";
				chkNoSkrining.disabled = false;
			}

		}

		function ShowHideDiv4() {
			document.getElementById("<%=rbkunjungan2.ClientID %>").checked = true;
			let chkYes = document.getElementById("<%=rbkunjungan2.ClientID %>");
			let chkYesSkrining = document.getElementById("<%=rbskriningyes.ClientID%>");
			let chkNokrining = document.getElementById("<%=rbskriningno.ClientID%>");
			let dvskrining = document.getElementById("dvSkriningPenyakit");
			let dvPassport = document.getElementById("dvEndemic");
			if (chkYes.checked) {
				dvPassport.style.display = "";
				chkYesSkrining.disabled = false;
				chkYesSkrining.checked = true;
				chkNokrining.disabled = true;
				dvskrining.classList.remove("disabled-form-skriningPenyakit");
			}
		}

		function HideDiv4() {
			document.getElementById("<%=rbkunjungan1.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbkunjungan1.ClientID %>");
			var dvPassport = document.getElementById("dvEndemic");
			if (chkYes.checked) {
				dvPassport.style.display = "none";
			}
		}

		function ShowHideDiv5() {
			document.getElementById("<%=rbnutrisi2.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbnutrisi2.ClientID %>");
			var dvPassport = document.getElementById("dvnutrisi");
			if (chkYes.checked) {
				dvPassport.style.display = "";
			}
		}

		function HideDiv5() {
			document.getElementById("<%=rbnutrisi1.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbnutrisi1.ClientID %>");
			var dvPassport = document.getElementById("dvnutrisi");
			if (chkYes.checked) {
				dvPassport.style.display = "none";
			}
		}

		function ShowHideDiv6() {
			document.getElementById("<%=rbpuasa2.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbpuasa2.ClientID %>");
			var dvPassport = document.getElementById("dvPuasa");
			if (chkYes.checked) {
				dvPassport.style.display = "";
			}
		}

		function HideDiv6() {
			document.getElementById("<%=rbpuasa1.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbpuasa1.ClientID %>");
			var dvPassport = document.getElementById("dvPuasa");
			if (chkYes.checked) {
				dvPassport.style.display = "none";
			}
		}

		function ShowHideDiv7() {
			document.getElementById("<%=rbOperas2.ClientID %>").checked = true;
			document.getElementById('<%=txtSurgeryName.ClientID %>').value = "";
			document.getElementById('<%=txtSurgeryDate.ClientID %>').value = "";
			var chkYes = document.getElementById("<%=rbOperas2.ClientID %>");
			var dvPassport = document.getElementById("dvoperasi");
			if (chkYes.checked) {
				dvPassport.style.display = "";
			}
		}

		function HideDiv7() {
            //document.getElementById("<%=rbOperasi.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbOperasi.ClientID %>");
			var dvPassport = document.getElementById("dvoperasi");
			if (chkYes.checked) {
				dvPassport.style.display = "none";
			}
		}

		function ShowHideDivProcout() {
			document.getElementById("<%=rbProcOut2.ClientID %>").checked = true;
			document.getElementById('<%=txtProcoutName.ClientID %>').value = "";
			document.getElementById('<%=txtProcoutDate.ClientID %>').value = "";
			var chkYes = document.getElementById("<%=rbProcOut2.ClientID %>");
			var dvProcoutside = document.getElementById("dvprocout");
			if (chkYes.checked) {
				dvProcoutside.style.display = "";
			}
		}

		function HideDivProcout() {
			document.getElementById("<%=rbProcOut1.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbProcOut1.ClientID %>");
			var dvProcoutside = document.getElementById("dvprocout");
			if (chkYes.checked) {
				dvProcoutside.style.display = "none";
			}
		}

		function ShowHideDiv8() {
			document.getElementById("<%=rbdrug2.ClientID %>").checked = true;
			document.getElementById('<%=txtDrugsAllergy.ClientID %>').value = "";
			document.getElementById('<%=txtReactionAllergy.ClientID %>').value = "";
			var chkYes = document.getElementById("<%=rbdrug2.ClientID %>");
			var dvPassport = document.getElementById("dvdrugs");
			if (chkYes.checked) {
				dvPassport.style.display = "";
			}
		}

		function HideDiv8() {
            //document.getElementById("<%=rbdrug1.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbdrug1.ClientID %>");
			var dvPassport = document.getElementById("dvdrugs");
			if (chkYes.checked) {
				dvPassport.style.display = "none";
			}
		}

		function ShowHideDiv9() {
			document.getElementById("<%=rbfood2.ClientID %>").checked = true;
			document.getElementById('<%=txtDrugsFoods.ClientID %>').value = "";
			document.getElementById('<%=txtReactionFoods.ClientID %>').value = "";
			var chkYes = document.getElementById("<%=rbfood2.ClientID %>");
			var dvPassport = document.getElementById("dvfoods");
			if (chkYes.checked) {
				dvPassport.style.display = "";
			}
		}

		function HideDiv9() {
            //document.getElementById("<%=rbfood1.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbfood1.ClientID %>");
			var dvPassport = document.getElementById("dvfoods");
			if (chkYes.checked) {
				dvPassport.style.display = "none";
			}
		}

		function ShowHideDivOtherAllergy() {
			document.getElementById("<%=rbother2.ClientID %>").checked = true;
			document.getElementById('<%=txtNameOthers.ClientID %>').value = "";
			document.getElementById('<%=txtReactionOthers.ClientID %>').value = "";
			var chkYes = document.getElementById("<%=rbother2.ClientID %>");
			var dvOther = document.getElementById("dvothers");
			if (chkYes.checked) {
				dvOther.style.display = "";
			}
		}

		function HideDivOtherAllergy() {
            //document.getElementById("<%=rbother1.ClientID %>").checked = true;
			var chkYes = document.getElementById("<%=rbother1.ClientID %>");
			var dvOther = document.getElementById("dvothers");
			if (chkYes.checked) {
				dvOther.style.display = "none";
			}
		}

		function hidecheckboxes() {
			var txttemp = $("[id$='txtDisease']").val();
			var chkYes1 = document.getElementById("<%=chkdisease1.ClientID %>");
			var chkYes2 = document.getElementById("<%=chkdisease2.ClientID %>");
			var chkYes3 = document.getElementById("<%=chkdisease3.ClientID %>");
			var chkYes4 = document.getElementById("<%=chkdisease4.ClientID %>");
			var chkYes5 = document.getElementById("<%=chkdisease5.ClientID %>");
			var chkYes6 = document.getElementById("<%=chkdisease6.ClientID %>");
			var chkYes7 = document.getElementById("<%=chkdisease7.ClientID %>");
			var chkYes8 = document.getElementById("<%=chkdisease8.ClientID %>");
			var chkYes9 = document.getElementById("<%=chkdisease9.ClientID %>");
			var chkYes10 = document.getElementById("<%=chkdisease10.ClientID %>");
			var chkYes11 = document.getElementById("<%=chkdiseaseHepB.ClientID %>");
			var chkYes12 = document.getElementById("<%=chkdiseaseHepC.ClientID %>");
			if (chkYes1.checked || chkYes2.checked || chkYes3.checked || chkYes4.checked || chkYes5.checked || chkYes6.checked ||
				chkYes7.checked || chkYes8.checked || chkYes9.checked || chkYes10.checked || chkYes11.checked || chkYes12.checked || txttemp.length > 0) {
				var chkYes11 = $("[id$='rbpribadi2']");
				var dvPassport = document.getElementById('dvPenyakitPribadi');
				if (confirm("There's data in the form, are you sure you want to change to 'No'?")) {
					dvPassport.style.display = "none";
				}
				else
					chkYes11.click();
			}
			else {
				var dvPassport = document.getElementById('dvPenyakitPribadi');
				dvPassport.style.display = "none";
			}
		}

		function hidecheckboxesfam() {
			var txttemp = $("[id$='txtDiseaseFam']").val();
			var chkYes1 = document.getElementById("<%=chkdiseasefam1.ClientID %>");
			var chkYes2 = document.getElementById("<%=chkdiseasefam2.ClientID %>");
			var chkYes3 = document.getElementById("<%=chkdiseasefam3.ClientID %>");
			var chkYes4 = document.getElementById("<%=chkdiseasefam4.ClientID %>");
			var chkYes5 = document.getElementById("<%=chkdiseasefam5.ClientID %>");
			if (chkYes1.checked || chkYes2.checked || chkYes3.checked || chkYes4.checked || chkYes5.checked || txttemp.length > 0) {
				var chkYes11 = $("[id$='rbkeluarga2']");
				var dvPassport = document.getElementById('dvPenyakitKeluarga');
				if (confirm("There's data in the form, are you sure you want to change to 'No'?")) {
					dvPassport.style.display = "none";
				}
				else
					chkYes11.click();
			}
			else {
				var dvPassport = document.getElementById('dvPenyakitKeluarga');
				dvPassport.style.display = "none";
			}
		}

		function showTxtStatus(cekname) {
			if (cekname == "TBC") {
				var cekTBC = document.getElementById("<%=chkdisease3.ClientID %>");
				var textTBC = document.getElementById("<%=DDL_TBC.ClientID %>");

				if (cekTBC.checked) {
					textTBC.style.display = "inline-block";
				}
				else {
					textTBC.style.display = "none";
				}
			}
			else if (cekname == "HEPB") {
				var cekHEPB = document.getElementById("<%=chkdiseaseHepB.ClientID %>");
				var textHEPB = document.getElementById("<%=DDL_HepB.ClientID %>");

				if (cekHEPB.checked) {
					textHEPB.style.display = "inline-block";
				}
				else {
					textHEPB.style.display = "none";
				}
			}
			else if (cekname == "HEPC") {
				var cekHEPC = document.getElementById("<%=chkdiseaseHepC.ClientID %>");
				var textHEPC = document.getElementById("<%=DDL_HepC.ClientID %>");

				if (cekHEPC.checked) {
					textHEPC.style.display = "inline-block";
				}
				else {
					textHEPC.style.display = "none";
				}
			}
		}

		function datesurgery() {
			var dp = $('#<%=txtSurgeryDate.ClientID%>');
			dp.datepicker({
				changeMonth: true,
				changeYear: true,
				format: "MM yyyy",
				language: "tr"
			}).on('changeDate', function (ev) {
				$(this).blur();
				$(this).datepicker('hide');
			});
		}

		function datescheduletravel() {
			var dp = $('#<%=txtdatescheduletravel.ClientID%>');
			dp.datepicker({
				changeMonth: true,
				changeYear: true,
				format: "dd MM yyyy",
				language: "tr"
			}).on('changeDate', function (ev) {
				$(this).blur();
				$(this).datepicker('hide');
			});
		}

		function datefittofly() {
			var dp = $('#<%=txtdatefittofly.ClientID%>');
			dp.datepicker({
				changeMonth: true,
				changeYear: true,
				format: "dd MM yyyy",
				language: "tr"
			}).on('changeDate', function (ev) {
				$(this).blur();
				$(this).datepicker('hide');
			});
		}

		function dateprocout() {
			var dp = $('#<%=txtProcoutDate.ClientID%>');
			dp.datepicker({
				changeMonth: true,
				changeYear: true,
				format: "MM yyyy",
				language: "tr"
			}).on('changeDate', function (ev) {
				$(this).blur();
				$(this).datepicker('hide');
			});
		}

		function txtOnKeyPressSurgery() {
			var c = event.keyCode;
			if (c == 13) {
				var UserName = $("[id$='txtSurgeryName']").val();
				var PassWord = $("[id$='txtSurgeryDate']").val();

				$("[id$='txtSurgeryName']").removeAttr("style");
				$("[id$='txtSurgeryDate']").removeAttr("style");

				if (UserName.length <= 0 && PassWord.length > 0) {
					$("[id$='txtSurgeryName']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtSurgeryName']").focus();
					return false;
				}
				else if (UserName.length > 0 && PassWord.length <= 0) {
					$("[id$='txtSurgeryDate']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtSurgeryDate']").focus();
					return false;
				}
				else if (UserName.length <= 0 && PassWord.length <= 0) {
					$("[id$='txtSurgeryName']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtSurgeryDate']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtSurgeryName']").focus();
					return false;
				}
				else {
					$("[id$='txtSurgeryName']").attr("style", "max-width:100%");
					$("[id$='txtSurgeryDate']").attr("style", "max-width:100%");
					document.getElementById('<%=btnSurgery.ClientID%>').click();
				}
				return false;
			}
		}

		function txtOnKeyPressProcout() {
			var c = event.keyCode;
			if (c == 13) {
				var ProcName = $("[id$='txtProcoutName']").val();
				var ProcDate = $("[id$='txtProcoutDate']").val();

				$("[id$='txtProcoutName']").removeAttr("style");
				$("[id$='txtProcoutDate']").removeAttr("style");

				if (ProcName.length <= 0 && ProcDate.length > 0) {
					$("[id$='txtProcoutName']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtProcoutName']").focus();
					return false;
				}
				else if (ProcName.length > 0 && ProcDate.length <= 0) {
					$("[id$='txtProcoutDate']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtProcoutDate']").focus();
					return false;
				}
				else if (ProcName.length <= 0 && ProcDate.length <= 0) {
					$("[id$='txtProcoutName']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtProcoutDate']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtProcoutName']").focus();
					return false;
				}
				else {
					$("[id$='txtProcoutName']").attr("style", "max-width:100%");
					$("[id$='txtProcoutDate']").attr("style", "max-width:100%");
					document.getElementById('<%=btnProcout.ClientID%>').click();
				}
				return false;
			}
		}

		function txtOnKeyPressReminder() {
			var c = event.keyCode;
			if (c == 13) {
				var ReminderName = $("[id$='TxtReminderNotes']").val();

				if (ReminderName.length <= 0) {
					$("[id$='TxtReminderNotes']").attr("style", "outline-color:red;max-width:100%; width:80%; margin-right: 5px;");
					$("[id$='TxtReminderNotes']").focus();
					return false;
				}
				else {
					$("[id$='TxtReminderNotes']").attr("style", "max-width:100%; width:80%; margin-right: 5px;");
					document.getElementById('<%=BtnAddReminder.ClientID%>').click();
				}
				return false;
			}
		}

		function checkprocout() {
			var ProcName = $("[id$='txtProcoutName']").val();
			var ProcDate = $("[id$='txtProcoutDate']").val();

			$("[id$='txtProcoutName']").removeAttr("style");
			$("[id$='txtProcoutDate']").removeAttr("style");

			if (ProcName.length <= 0 && ProcDate.length > 0) {
				$("[id$='txtProcoutName']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtProcoutName']").focus();
				return false;
			}
			else if (ProcName.length > 0 && ProcDate.length <= 0) {
				$("[id$='txtProcoutDate']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtProcoutDate']").focus();
				return false;
			}
			else if (ProcName.length <= 0 && ProcDate.length <= 0) {
				$("[id$='txtProcoutName']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtProcoutDate']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtProcoutName']").focus();
				return false;
			}
			else {
				$("[id$='txtProcoutName']").attr("style", "max-width:100%");
				$("[id$='txtProcoutDate']").attr("style", "max-width:100%");
				document.getElementById('<%=btnProcout.ClientID%>').click();
				return true;
			}
			return false;
		}

		function checkreminder() {
			var ReminderName = $("[id$='TxtReminderNotes']").val();

			if (ReminderName.length <= 0) {
				$("[id$='TxtReminderNotes']").attr("style", "outline-color:red;max-width:100%; width:80%; margin-right: 5px;");
				$("[id$='TxtReminderNotes']").focus();
				return false;
			}
			else {
				$("[id$='TxtReminderNotes']").attr("style", "max-width:100%; width:80%; margin-right: 5px;");
				document.getElementById('<%=BtnAddReminder.ClientID%>').click();
				return true;
			}
			return false;
		}

		function checksurgery() {
			var UserName = $("[id$='txtSurgeryName']").val();
			var PassWord = $("[id$='txtSurgeryDate']").val();

			$("[id$='txtSurgeryName']").removeAttr("style");
			$("[id$='txtSurgeryDate']").removeAttr("style");

			if (UserName.length <= 0 && PassWord.length > 0) {
				$("[id$='txtSurgeryName']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtSurgeryName']").focus();
				return false;
			}
			else if (UserName.length > 0 && PassWord.length <= 0) {
				$("[id$='txtSurgeryDate']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtSurgeryDate']").focus();
				return false;
			}
			else if (UserName.length <= 0 && PassWord.length <= 0) {
				$("[id$='txtSurgeryName']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtSurgeryDate']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtSurgeryName']").focus();
				return false;
			}
			else {
				$("[id$='txtSurgeryName']").attr("style", "max-width:100%");
				$("[id$='txtSurgeryDate']").attr("style", "max-width:100%");
				document.getElementById('<%=btnSurgery.ClientID%>').click();
				return true;
			}
			return false;
		}

		function txtOnKeyPressDrugsAllergy() {
			var c = event.keyCode;
			if (c == 13) {
				var UserName = $("[id$='txtDrugsAllergy']").val();
				var PassWord = $("[id$='txtReactionAllergy']").val();

				$("[id$='txtDrugsAllergy']").removeAttr("style");
				$("[id$='txtReactionAllergy']").removeAttr("style");

				if (UserName.length <= 0 && PassWord.length > 0) {
					$("[id$='txtDrugsAllergy']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtDrugsAllergy']").focus();
					return false;
				}
				else if (UserName.length > 0 && PassWord.length <= 0) {
					$("[id$='txtReactionAllergy']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtReactionAllergy']").focus();
					return false;
				}
				else if (UserName.length <= 0 && PassWord.length <= 0) {
					$("[id$='txtDrugsAllergy']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtReactionAllergy']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtDrugsAllergy']").focus();
					return false;
				}
				else {
					$("[id$='txtDrugsAllergy']").attr("style", "max-width:100%");
					$("[id$='txtReactionAllergy']").attr("style", "max-width:100%");
					document.getElementById('<%=btnDrugsAllergy.ClientID%>').click();
				}
				return false;
			}
		}

		function checkdrugsallergy() {
			var UserName = $("[id$='txtDrugsAllergy']").val();
			var PassWord = $("[id$='txtReactionAllergy']").val();

			$("[id$='txtDrugsAllergy']").removeAttr("style");
			$("[id$='txtReactionAllergy']").removeAttr("style");

			if (UserName.length <= 0 && PassWord.length > 0) {
				$("[id$='txtDrugsAllergy']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtDrugsAllergy']").focus();
				return false;
			}
			else if (UserName.length > 0 && PassWord.length <= 0) {
				$("[id$='txtReactionAllergy']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtReactionAllergy']").focus();
				return false;
			}
			else if (UserName.length <= 0 && PassWord.length <= 0) {
				$("[id$='txtDrugsAllergy']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtReactionAllergy']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtDrugsAllergy']").focus();
				return false;
			}
			else {
				$("[id$='txtDrugsAllergy']").attr("style", "max-width:100%");
				$("[id$='txtReactionAllergy']").attr("style", "max-width:100%");
				document.getElementById('<%=btnDrugsAllergy.ClientID%>').click();
				return true;
			}
			return false;
		}

		function txtOnKeyPressFoodsAllergy() {
			var c = event.keyCode;
			if (c == 13) {
				var UserName = $("[id$='txtDrugsFoods']").val();
				var PassWord = $("[id$='txtReactionFoods']").val();

				$("[id$='txtDrugsFoods']").removeAttr("style");
				$("[id$='txtReactionFoods']").removeAttr("style");

				if (UserName.length <= 0 && PassWord.length > 0) {
					$("[id$='txtDrugsFoods']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtDrugsFoods']").focus();
					return false;
				}
				else if (UserName.length > 0 && PassWord.length <= 0) {
					$("[id$='txtReactionFoods']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtReactionFoods']").focus();
					return false;
				}
				else if (UserName.length <= 0 && PassWord.length <= 0) {
					$("[id$='txtDrugsFoods']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtReactionFoods']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtDrugsFoods']").focus();
					return false;
				}
				else {
					$("[id$='txtDrugsFoods']").attr("style", "max-width:100%");
					$("[id$='txtReactionFoods']").attr("style", "max-width:100%");
					document.getElementById('<%=btnFoodAllergy.ClientID%>').click();
				}
				return false;
			}
		}

		function checkfoodallergy() {
			var UserName = $("[id$='txtDrugsFoods']").val();
			var PassWord = $("[id$='txtReactionFoods']").val();

			$("[id$='txtDrugsFoods']").removeAttr("style");
			$("[id$='txtReactionFoods']").removeAttr("style");

			if (UserName.length <= 0 && PassWord.length > 0) {
				$("[id$='txtDrugsFoods']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtDrugsFoods']").focus();
				return false;
			}
			else if (UserName.length > 0 && PassWord.length <= 0) {
				$("[id$='txtReactionFoods']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtReactionFoods']").focus();
				return false;
			}
			else if (UserName.length <= 0 && PassWord.length <= 0) {
				$("[id$='txtDrugsFoods']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtReactionFoods']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtDrugsFoods']").focus();
				return false;
			}
			else {
				$("[id$='txtDrugsFoods']").attr("style", "max-width:100%");
				$("[id$='txtReactionFoods']").attr("style", "max-width:100%");
				document.getElementById('<%=btnFoodAllergy.ClientID%>').click();
				return true;
			}
			return false;
		}

		function txtOnKeyPressOthersAllergy() {
			var c = event.keyCode;
			if (c == 13) {
				var UserName = $("[id$='txtNameOthers']").val();
				var PassWord = $("[id$='txtReactionOthers']").val();

				$("[id$='txtNameOthers']").removeAttr("style");
				$("[id$='txtReactionOthers']").removeAttr("style");

				if (UserName.length <= 0 && PassWord.length > 0) {
					$("[id$='txtNameOthers']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtNameOthers']").focus();
					return false;
				}
				else if (UserName.length > 0 && PassWord.length <= 0) {
					$("[id$='txtReactionOthers']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtReactionOthers']").focus();
					return false;
				}
				else if (UserName.length <= 0 && PassWord.length <= 0) {
					$("[id$='txtNameOthers']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtReactionOthers']").attr("style", "display:block; outline-color:red;max-width:100%");
					$("[id$='txtNameOthers']").focus();
					return false;
				}
				else {
					$("[id$='txtNameOthers']").attr("style", "max-width:100%");
					$("[id$='txtReactionOthers']").attr("style", "max-width:100%");
					document.getElementById('<%=btnFoodAllergy.ClientID%>').click();
				}
				return false;
			}
		}

		function checkotherallergy() {
			var UserName = $("[id$='txtNameOthers']").val();
			var PassWord = $("[id$='txtReactionOthers']").val();

			$("[id$='txtNameOthers']").removeAttr("style");
			$("[id$='txtReactionOthers']").removeAttr("style");

			if (UserName.length <= 0 && PassWord.length > 0) {
				$("[id$='txtNameOthers']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtNameOthers']").focus();
				return false;
			}
			else if (UserName.length > 0 && PassWord.length <= 0) {
				$("[id$='txtReactionOthers']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtReactionOthers']").focus();
				return false;
			}
			else if (UserName.length <= 0 && PassWord.length <= 0) {
				$("[id$='txtNameOthers']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtReactionOthers']").attr("style", "display:block; outline-color:red;max-width:100%");
				$("[id$='txtNameOthers']").focus();
				return false;
			}
			else {
				$("[id$='txtNameOthers']").attr("style", "max-width:100%");
				$("[id$='txtReactionOthers']").attr("style", "max-width:100%");
				document.getElementById('<%=btnOtherAllergy.ClientID%>').click();
				return true;
			}
			return false;
		}

		function hidetext(val1, val2, val3) {
			var txttemp = $("[id$='" + val1 + "']").val();
			var chkYes = $("[id$='" + val2 + "']");
			var dvPassport = document.getElementById(val3);
			if (txttemp.length > 0) {
				if (confirm("There's data in the form, are you sure you want to change to 'No'?")) {
					dvPassport.style.display = "none";
				}
				else
					chkYes.click();
			}
			else {
				dvPassport.style.display = "none";
			}
		}

		function txtOnKeyPressRoutine() {
			var c = event.keyCode;
			if (c == 13) {
				var UserName = $("[id$='txtRoutineMed']").val();

				$("[id$='txtRoutineMed']").removeAttr("style");

				if (UserName.length <= 0) {
					$("[id$='txtRoutineMed']").attr("style", "display:block; outline-color:red;max-width:100%;width: 100%");
					$("[id$='txtRoutineMed']").focus();
					return false;
				}
				else {
					$("[id$='txtRoutineMed']").attr("style", "max-width:100%;width: 100%");
					document.getElementById('<%=btnRoutineMed.ClientID%>').click();
				}
				return false;
			}
		}

		function checkroutineempty() {
			var UserName = $("[id$='txtRoutineMed']").val();

			$("[id$='txtRoutineMed']").removeAttr("style");

			if (UserName.length <= 0) {
				$("[id$='txtRoutineMed']").attr("style", "display:block; outline-color:red;max-width:100%;width: 100%");
				$("[id$='txtRoutineMed']").focus();
				return false;
			}
			else {
				$("[id$='txtRoutineMed']").attr("style", "max-width:100%;width: 100%");
				document.getElementById('<%=btnRoutineMed.ClientID%>').click();
				return true;
			}
			return false;
		}

		function hidegrid(val1, val2, val3) {
			if (val1 == 1) {
				var GridId = "<%=gvw_surgery.ClientID %>";
			}
			else if (val1 == 2) {
				var GridId = "<%=gvw_allergy.ClientID %>";
			}
			else if (val1 == 3) {
				var GridId = "<%=gvw_foods.ClientID %>";
			}
			else if (val1 == 4) {
				var GridId = "<%=gvw_others.ClientID %>";
			}
			else {
				var GridId = "<%=gvw_routinemed.ClientID %>";
			}
			var grid = document.getElementById(GridId);
			rowscount = grid.rows.length;

			if (rowscount > 1) {
				var chkYes = $("[id$='" + val2 + "']");
				var dvPassport = document.getElementById(val3);
				if (confirm("There's data in the form, are you sure you want to change to 'No'?")) {
					if (val1 == 4) {
						dvPassport.style.display = "none";
						//$("[id$='btnMedNone']").click();
					}
					else if (val1 == 3) {
						dvPassport.style.display = "none";
						//$("[id$='btnFoodAllergyNone']").click();
					}
					else if (val1 == 2) {
						dvPassport.style.display = "none";
						//$("[id$='btnAllergyDrugNone']").click();
					}
					else if (val1 == 1) {
						dvPassport.style.display = "none";
					}
				}
				else {
					chkYes.click();
				}
			}
			else {

				var dvPassport = document.getElementById(val3);
				dvPassport.style.display = "none";
			}
		}

		function SumEMV() {
			var rbEye1 = document.getElementById("<%=eye1.ClientID %>");
			var rbEye2 = document.getElementById("<%=eye2.ClientID %>");
			var rbEye3 = document.getElementById("<%=eye3.ClientID %>");
			var rbEye4 = document.getElementById("<%=eye4.ClientID %>");

			var rbMove1 = document.getElementById("<%=move1.ClientID %>");
			var rbMove2 = document.getElementById("<%=move2.ClientID %>");
			var rbMove3 = document.getElementById("<%=move3.ClientID %>");
			var rbMove4 = document.getElementById("<%=move4.ClientID %>");
			var rbMove5 = document.getElementById("<%=move5.ClientID %>");
			var rbMove6 = document.getElementById("<%=move6.ClientID %>");

			var rbVerbal1 = document.getElementById("<%=verbal1.ClientID %>");
			var rbVerbal2 = document.getElementById("<%=verbal2.ClientID %>");
			var rbVerbal3 = document.getElementById("<%=verbal3.ClientID %>");
			var rbVerbal4 = document.getElementById("<%=verbal4.ClientID %>");
			var rbVerbal5 = document.getElementById("<%=verbal5.ClientID %>");
			var rbVerbal6 = document.getElementById("<%=verbal6.ClientID %>");
			var rbVerbal7 = document.getElementById("<%=verbal7.ClientID %>");

			var eyeTotal = document.getElementById("<%=lbleyetotal.ClientID %>");
			var moveTotal = document.getElementById("<%=lblmovetotal.ClientID %>");
			var verbalTotal = document.getElementById("<%=lblverbaltotal.ClientID %>");

			var totalScore = document.getElementById("<%=lblTotalScore.ClientID %>");

			if (rbEye1.checked)
				eyeTotal.value = rbEye1.value;
			else if (rbEye2.checked)
				eyeTotal.value = rbEye2.value;
			else if (rbEye3.checked)
				eyeTotal.value = rbEye3.value;
			else if (rbEye4.checked)
				eyeTotal.value = rbEye4.value;

			if (rbMove1.checked)
				moveTotal.value = rbMove1.value;
			else if (rbMove2.checked)
				moveTotal.value = rbMove2.value;
			else if (rbMove3.checked)
				moveTotal.value = rbMove3.value;
			else if (rbMove4.checked)
				moveTotal.value = rbMove4.value;
			else if (rbMove5.checked)
				moveTotal.value = rbMove5.value;
			else if (rbMove6.checked)
				moveTotal.value = rbMove6.value;

			if (rbVerbal1.checked)
				verbalTotal.value = rbVerbal1.value;
			else if (rbVerbal2.checked)
				verbalTotal.value = rbVerbal2.value;
			else if (rbVerbal3.checked)
				verbalTotal.value = rbVerbal3.value;
			else if (rbVerbal4.checked)
				verbalTotal.value = rbVerbal4.value;
			else if (rbVerbal5.checked)
				verbalTotal.value = rbVerbal5.value;
			else if (rbVerbal6.checked)
				verbalTotal.value = rbVerbal6.value;
			else if (rbVerbal7.checked)
				verbalTotal.value = rbVerbal7.value;

			if (eyeTotal.value != "_" && moveTotal.value != "_" && verbalTotal.value != "_") {
				if (verbalTotal.value != "T" && verbalTotal.value != "A") {
					var total1 = parseInt(eyeTotal.value);
					var total2 = parseInt(moveTotal.value);
					var total3 = parseInt(verbalTotal.value);

					totalScore.value = total1 + total2 + total3;
				}
				else
					totalScore.value = "-";
			}
			else
				totalScore.value = "-";
		}

		function getProgressBar(val) {
			var painTotal = document.getElementById("<%=txtPainScale.ClientID %>");
			var dvGreen = document.getElementById("divGreen");
			var dvYellow = document.getElementById("divYellow");
			var dvRed = document.getElementById("divRed");
			//var Id = Ob.getAttribute("CommandArgument");
			if (val == "0") {
				dvGreen.style.width = "0%";
				dvYellow.style.width = "0%";
				dvRed.style.width = "0%";
				painTotal.value = "0"
			}
			else if (val == "1") {
				dvGreen.style.width = "10.5%";
				dvYellow.style.width = "0%";
				dvRed.style.width = "0%";
				painTotal.value = "1"
			}
			else if (val == "2") {
				dvGreen.style.width = "20.5%";
				dvYellow.style.width = "0%";
				dvRed.style.width = "0%";
				painTotal.value = "2"
			}
			else if (val == "3") {
				dvGreen.style.width = "30.5%";
				dvYellow.style.width = "0%";
				dvRed.style.width = "0%";
				painTotal.value = "3"
			}
			else if (val == "4") {
				dvGreen.style.width = "30.5%";
				dvYellow.style.width = "10.5%";
				dvRed.style.width = "0%";
				painTotal.value = "4"
			}
			else if (val == "5") {
				dvGreen.style.width = "30.5%";
				dvYellow.style.width = "20.5%";
				dvRed.style.width = "0%";
				painTotal.value = "5"
			}
			else if (val == "6") {
				dvGreen.style.width = "30.5%";
				dvYellow.style.width = "30.5%";
				dvRed.style.width = "0%";
				painTotal.value = "6"
			}
			else if (val == "7") {
				dvGreen.style.width = "30.5%";
				dvYellow.style.width = "40%";
				dvRed.style.width = "0%";
				painTotal.value = "7"
			}
			else if (val == "8") {
				dvGreen.style.width = "30.5%";
				dvYellow.style.width = "40%";
				dvRed.style.width = "10.5%";
				painTotal.value = "8"
			}
			else if (val == "9") {
				dvGreen.style.width = "30.5%";
				dvYellow.style.width = "40%";
				dvRed.style.width = "20.5%";
				painTotal.value = "9"
			}
			else if (val == "10") {
				dvGreen.style.width = "30.5%";
				dvYellow.style.width = "40%";
				dvRed.style.width = "29.5%";
				painTotal.value = "10"
			}
			return false;
		}

		function loseBox() {
			popupicd.style.display = 'none';
		}

		function copytext(txt, el) {
			//var checkempty = $("[id$='"+txt+"']").val();
			//if(checkempty != '')
			//{
			//    var tempconcat = $("[id$='"+txt+"']").val() + ', ' +el;
			//}
			//else
			//{
			//    var tempconcat = el;
			//}

			//$("[id$='"+txt+"']").val(tempconcat);

			var checkempty = document.getElementById("MainContent_" + txt).value;
			if (checkempty != '') {
				var tempconcat = document.getElementById("MainContent_" + txt).value + ', ' + el;
			}
			else {
				var tempconcat = el;
			}
			document.getElementById("MainContent_" + txt).value = tempconcat;
		}

		function templatenormal() {
			document.getElementById("MainContent_txtumum").value = "Baik";
			document.getElementById("MainContent_txtkulit").value = "Normal";
			document.getElementById("MainContent_txtkepala").value = "Normal";
			document.getElementById("MainContent_txtmata").value = "Normal";
			document.getElementById("MainContent_txtTHT").value = "Normal";
			document.getElementById("MainContent_txtLeher").value = "Normal";
			document.getElementById("MainContent_txtThorax").value = "Normal";
			document.getElementById("MainContent_txtCOr").value = "Normal";
			document.getElementById("MainContent_txtPulma").value = "Normal";
			document.getElementById("MainContent_txtAbdomen").value = "Normal";
			document.getElementById("MainContent_txtHepar").value = "Normal";
			document.getElementById("MainContent_txtLien").value = "Normal";
			document.getElementById("MainContent_txtEkstremitas").value = "Normal";
			document.getElementById("MainContent_txtGenetalia").value = "Normal";
		}

		function clearnormal() {
			document.getElementById("MainContent_txtumum").value = "";
			document.getElementById("MainContent_txtkulit").value = "";
			document.getElementById("MainContent_txtkepala").value = "";
			document.getElementById("MainContent_txtmata").value = "";
			document.getElementById("MainContent_txtTHT").value = "";
			document.getElementById("MainContent_txtLeher").value = "";
			document.getElementById("MainContent_txtThorax").value = "";
			document.getElementById("MainContent_txtCOr").value = "";
			document.getElementById("MainContent_txtPulma").value = "";
			document.getElementById("MainContent_txtAbdomen").value = "";
			document.getElementById("MainContent_txtHepar").value = "";
			document.getElementById("MainContent_txtLien").value = "";
			document.getElementById("MainContent_txtEkstremitas").value = "";
			document.getElementById("MainContent_txtGenetalia").value = "";
		}

		function SetTemplateToObjective() {
			var tempobj = '';
			if ($("[id$='txtumum']").val() != '') {
				if (tempobj == '') {
					tempobj = 'Keadaan Umum:\t' + $("[id$='txtumum']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'Keadaan Umum:\t' + $("[id$='txtumum']").val();
				}
			}
			if ($("[id$='txtkulit']").val() != '') {
				if (tempobj == '') {
					tempobj = 'Kulit:\t' + $("[id$='txtkulit']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'Kulit:\t' + $("[id$='txtkulit']").val();
				}
			}
			if ($("[id$='txtkepala']").val() != '') {
				if (tempobj == '') {
					tempobj = 'Kepala:\t' + $("[id$='txtkepala']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'Kepala:\t' + $("[id$='txtkepala']").val();
				}
			}
			if ($("[id$='txtmata']").val() != '') {
				if (tempobj == '') {
					tempobj = 'Mata:\t' + $("[id$='txtmata']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'Mata:\t' + $("[id$='txtmata']").val();
				}
			}
			if ($("[id$='txtTHT']").val() != '') {
				if (tempobj == '') {
					tempobj = 'THT:\t' + $("[id$='txtTHT']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'THT:\t' + $("[id$='txtTHT']").val();
				}
			}
			if ($("[id$='txtLeher']").val() != '') {
				if (tempobj == '') {
					tempobj = 'Leher:\t' + $("[id$='txtLeher']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'Leher:\t' + $("[id$='txtLeher']").val();
				}
			}
			if ($("[id$='txtThorax']").val() != '') {
				if (tempobj == '') {
					tempobj = 'Thorax:\t' + $("[id$='txtThorax']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'Thorax:\t' + $("[id$='txtThorax']").val();
				}
			}
			if ($("[id$='txtCOr']").val() != '') {
				if (tempobj == '') {
					tempobj = 'Cor:\t' + $("[id$='txtCOr']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'Cor:\t' + $("[id$='txtCOr']").val();
				}
			}
			if ($("[id$='txtPulma']").val() != '') {
				if (tempobj == '') {
					tempobj = 'Pulmo:\t' + $("[id$='txtPulma']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'Pulmo:\t' + $("[id$='txtPulma']").val();
				}
			}
			if ($("[id$='txtAbdomen']").val() != '') {
				if (tempobj == '') {
					tempobj = 'Abdomen:\t' + $("[id$='txtAbdomen']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'Abdomen:\t' + $("[id$='txtAbdomen']").val();
				}
			}
			if ($("[id$='txtHepar']").val() != '') {
				if (tempobj == '') {
					tempobj = 'Hepar:\t' + $("[id$='txtHepar']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'Hepar:\t' + $("[id$='txtHepar']").val();
				}
			}
			if ($("[id$='txtLien']").val() != '') {
				if (tempobj == '') {
					tempobj = 'Lien:\t' + $("[id$='txtLien']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'Lien:\t' + $("[id$='txtLien']").val();
				}
			}
			if ($("[id$='txtEkstremitas']").val() != '') {
				if (tempobj == '') {
					tempobj = 'Ekstremitas:\t' + $("[id$='txtEkstremitas']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'Ekstremitas:\t' + $("[id$='txtEkstremitas']").val();
				}
			}
			if ($("[id$='txtGenetalia']").val() != '') {
				if (tempobj == '') {
					tempobj = 'Genetalia:\t' + $("[id$='txtGenetalia']").val();
				}
				else {
					tempobj = tempobj + '\n' + 'Genetalia:\t' + $("[id$='txtGenetalia']").val();
				}
			}
			if ($("[id$='txtOthers']").val() == '') {
				$("[id$='txtOthers']").val(tempobj);
			}
			else {
				var tempothers = $("[id$='txtOthers']").val();
				tempothers = tempothers + '\n\n' + tempobj;
				$("[id$='txtOthers']").val(tempothers);
			}
			$('#modalTemplateO').modal('hide');
			$("[id$='txtOthers']").focus();
		}

		function copytextTemplate(txt, el) {
			var checkempty = document.getElementById("MainContent_" + txt).value;
			if (checkempty != '') {
				var tempconcat = document.getElementById("MainContent_" + txt).value + '\n' + el;
			}
			else {
				var tempconcat = el;
			}
			document.getElementById("MainContent_" + txt).value = tempconcat;
		}

		function copytextTemplatetoSOAP(txtsrc, txtdst) {

			var checkempty = document.getElementById("MainContent_" + txtdst).value;
			if (checkempty != '') {
				var tempconcat = document.getElementById("MainContent_" + txtdst).value + '\n\n' + document.getElementById("MainContent_" + txtsrc).value;
			}
			else {
				var tempconcat = document.getElementById("MainContent_" + txtsrc).value;
			}
			document.getElementById("MainContent_" + txtdst).value = tempconcat;
			document.getElementById("MainContent_" + txtdst).focus();
			document.getElementById("MainContent_" + txtsrc).value = "";
		}

		function copyddlTemplatetoSOAP() {

			var checkempty = document.getElementById("<%= txtOthers.ClientID %>").value;
			var ddlvalue = document.getElementById("<%= ddlO.ClientID %>");
			var tempconcat = "";
			if (ddlvalue.options[ddlvalue.selectedIndex].value == "All Normal") {
				tempconcat = "Keadaan Umum: Baik\nKulit: Normal\nKepala: Normal\nMata: Normal\nTHT: Normal\nLeher: Normal\nThorax: Normal\nCor: Normal\nPulmo: Normal\nAbdomen: Normal\nHepar: Normal\n Lien: Normal\nEkstremitas: Normal\nGenetalia: Normal";
			}
			else {
				if (checkempty != '') {
					tempconcat = document.getElementById("<%= txtOthers.ClientID %>").value + '\n\n' + ddlvalue.options[ddlvalue.selectedIndex].value;
				}
				else {
					tempconcat = ddlvalue.options[ddlvalue.selectedIndex].value;
				}
			}
			document.getElementById("<%= txtOthers.ClientID %>").value = tempconcat;
			document.getElementById("<%= txtOthers.ClientID %>").focus();
			ddlvalue.selectedIndex = 0;
		}

		function clearTemplateSOAP(txtsrc) {
			document.getElementById("MainContent_" + txtsrc).value = "";
		}

		//fungsi pencarian data di gridview client side
		function Search_Data(strKey, hflistcounter, dataRep) {

			var strData = strKey.value.toLowerCase().split(" ");
			var countData = parseInt(document.getElementById("MainContent_" + hflistcounter).value);

			var rowData;
			for (var i = 0; i < countData; i++) {
				rowData = document.getElementById(dataRep + i).textContent;
				var styleDisplay = 'none';
				for (var j = 0; j < strData.length; j++) {
					if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
						styleDisplay = '';
					else {
						styleDisplay = 'none';
						break;
					}
				}
				document.getElementById(dataRep + i).style.display = styleDisplay;
			}
		}

		function displayTemplateO(template) {
			if (template == "template1") {
				document.getElementById('templateOsatu').style.display = "block";
				document.getElementById('templateOdua').style.display = "none";
			}
			else if (template == "template2") {
				document.getElementById('templateOdua').style.display = "block";
				document.getElementById('templateOsatu').style.display = "none";
			}
		}

		//window.addEventListener('mouseup', function (e) {
		//    //alert("lvl 1 "+e.target);
		//    //alert("lvl 2 "+e.target.parentNode.id);
		//    //alert("lvl 3 "+e.target.parentNode.parentNode.id);
		//    //alert("lvl 4 "+e.target.parentNode.parentNode.parentNode.id);
		//    //alert("lvl 5 "+e.target.parentNode.parentNode.parentNode.parentNode.id);

		//    //hide pop up icd when click outside div
		//    if (e.target.parentNode.parentNode != null && e.target.parentNode.parentNode.parentNode != null && e.target.parentNode.parentNode.parentNode.parentNode != null) {
		//        if (e.target.parentNode.parentNode.id != "popupicd" && e.target.parentNode.parentNode.parentNode.id != "popupicd" && e.target.parentNode.parentNode.parentNode.id != "MainContent_gvw_icd") {
		//            popupicd.style.display = 'none';
		//        }
		//    }
		//});

		//function blokwarningshow() {
		//    divalarm.style.display = "block";
		//}

		//function blokwarninghide() {
		//    divalarm.style.display = "none";
		//}


		function EnDisSoapForm() {

			var fl = document.getElementById("<%=HFflagsoapisdisable_old.ClientID%>");
			if (fl.value == "1") {
				$('#MainContent_divTopSOAP').find('input, textarea, button, select').attr('disabled', 'disabled');
				$('#MainContent_divTopSOAP').find('a').removeAttr("href");
				//$('#MainContent_StdPlanning_divTopLABRAD').find('input, textarea, button, select').attr('disabled', 'disabled');
				//$('#MainContent_StdPlanning_divTopLABRAD').find('a').removeAttr("href");
			}
		}

		function EnDisLabRad() {

			var fl = document.getElementById("<%=HFflaglabradisdisable_old.ClientID%>");
			var fllab = document.getElementById("<%=HFflaglabisdisable_old.ClientID%>");
			var flrad = document.getElementById("<%=HFflagradisdisable_old.ClientID%>");
			//if (fl.value == "1") {
			//    $('#MainContent_StdPlanning_divTopLABRAD').find('input, textarea, button, select').attr('disabled', 'disabled');
			//    $('#MainContent_StdPlanning_divTopLABRAD').find('a').removeAttr("href");
			//}
			if (fllab.value == "1") {
				//$('#MainContent_StdPlanning_divTopLAB').find('input, textarea, button, select').attr('disabled', 'disabled');
				//$('#MainContent_StdPlanning_divTopLAB').find('a').removeAttr("href");
			}
			if (flrad.value == "1") {
				//$('#MainContent_StdPlanning_divTopRAD').find('input, textarea, button, select').attr('disabled', 'disabled');
				//$('#MainContent_StdPlanning_divTopRAD').find('a').removeAttr("href");
			}
		}

		function isVisiblePenanganan() {
			var f1 = document.getElementById("<%= fall1.ClientID %>");
			var f2 = document.getElementById("<%= fall2.ClientID %>");
			var f3 = document.getElementById("<%= fall3.ClientID %>");
			var f4 = document.getElementById("<%= fall4.ClientID %>");
			var f5 = document.getElementById("<%= fall5.ClientID %>");

			if (f1.checked == false && f2.checked == false && f3.checked == false && f4.checked == false && f5.checked == false) {
				document.getElementById("divpenanganan").style.display = "none";
				document.getElementById("<%= chkfalltempelstiker.ClientID %>").checked = false;
				document.getElementById("<%= chkfalledukasi.ClientID %>").checked = false;
				document.getElementById("<%= chkfallPengaman.ClientID %>").checked = false;
				document.getElementById("<%= chkfallTemaniKeluarga.ClientID %>").checked = false;
				document.getElementById("<%= chkfallAmbulasi.ClientID %>").checked = false;
				document.getElementById("<%= chkfallDokumentasiRM.ClientID %>").checked = false;
			}
			else {
				document.getElementById("divpenanganan").style.display = "inline-block";
			}
		}

		function validateFallRisk() {



			var f1 = document.getElementById("<%= fall1.ClientID %>");
			var f2 = document.getElementById("<%= fall2.ClientID %>");
			var f3 = document.getElementById("<%= fall3.ClientID %>");
			var f4 = document.getElementById("<%= fall4.ClientID %>");
			var f5 = document.getElementById("<%= fall5.ClientID %>");


			if (f1.checked == true || f2.checked == true || f3.checked == true || f4.checked == true || f5.checked == true) {

				var p1 = document.getElementById("<%= chkfalltempelstiker.ClientID %>");
				var p2 = document.getElementById("<%= chkfalledukasi.ClientID %>");
				var p3 = document.getElementById("<%= chkfallPengaman.ClientID %>");
				var p4 = document.getElementById("<%= chkfallTemaniKeluarga.ClientID %>");
				var p5 = document.getElementById("<%= chkfallAmbulasi.ClientID %>");
				var p6 = document.getElementById("<%= chkfallDokumentasiRM.ClientID %>");

				if (p1.checked == false && p2.checked == false && p3.checked == false && p4.checked == false && p5.checked == false && p6.checked == false) {
					toastr.warning('Please choose at least 1 Fall Prevention Steps!', 'Warning');
					return false;
				}
			}


			return true;
		}

		$(document).ready(function () {

			var dp = $('#<%=txtSurgeryDate.ClientID%>');
			dp.datepicker({
				changeMonth: true,
				changeYear: true,
				format: "MM yyyy",
				language: "tr"
			}).on('changeDate', function (ev) {
				$(this).blur();
				$(this).datepicker('hide');
			});

			var dp = $('#<%=txtProcoutDate.ClientID%>');
			dp.datepicker({
				changeMonth: true,
				changeYear: true,
				format: "MM yyyy",
				language: "tr"
			}).on('changeDate', function (ev) {
				$(this).blur();
				$(this).datepicker('hide');
			});

			$("#MainContent_TextJamKeluar1").mask("99:99");
			$("#MainContent_TextJamKeluar2").mask("99:99");

			ICDSuggestionSOAP();
			//DiagProcSuggestionSOAP();
			//DiagProcSuggestionSOAP_Dis();
			var txtTravelRecommendation = document.getElementById("<%=txtTravelRecommendation.ClientID %>");
			if (txtTravelRecommendation.innerText != "") {
				document.getElementById("divtravel").style.display = "block";
			}

			EnDisSoapForm();
			EnDisLabRad();

			isVisiblePenanganan();

			var prm = Sys.WebForms.PageRequestManager.getInstance();
			if (prm != null) {
				prm.add_beginRequest(function (sender, e) {
					if (sender._postBackSettings.panelsToUpdate != null) {

						document.getElementById('<%=flagIsPostback.ClientID %>').value = "true";

                        <%--var flgfinalsubmit = document.getElementById('<%=HFflagfinalsubmitloading.ClientID %>');
                        if (flgfinalsubmit.value == "true") {
							document.getElementById("loading_FinalSubmit").style.display = "block";
                            //flgfinalsubmit.value == "false";
						}--%>
					}
				});
				prm.add_endRequest(function (sender, e) {
					if (sender._postBackSettings.panelsToUpdate != null) {

						var flgfinalsubmit = document.getElementById('<%=HFflagfinalsubmitloading.ClientID %>');
						if (flgfinalsubmit.value == "true") {
							document.getElementById("loading_FinalSubmit").style.display = "block";
							flgfinalsubmit.value == "false";
						}

						//drug cons item validation
						var flgdrug = document.getElementById('<%=HFlagdrug.ClientID %>');
						var flgdrugadd = document.getElementById('<%=HFlagdrugadd.ClientID %>');
						var flgcons = document.getElementById('<%=HFlagcons.ClientID %>');
						var flgconsadd = document.getElementById('<%=HFlagconsadd.ClientID %>');

						if (flgdrug.value == "1") {
							document.getElementById('lblbhs_drugprescription').classList.add("blinkred");
							focustolabel("drug");
						}
						else if (flgdrugadd.value == "1") {
							document.getElementById('lblbhs_additionaldrugsprescription').classList.add("blinkred");
							focustolabel("adddrug");
						}
						else if (flgcons.value == "1") {
							document.getElementById('lblbhs_consumables').classList.add("blinkred");
							focustolabel("cons");
						}
						else if (flgconsadd.value == "1") {
							document.getElementById('lblbhs_additionalconsumables').classList.add("blinkred");
							focustolabel("addcons");
							//blokwarningshow();
							//setTimeout(blokwarninghide, 2000);
						}
						else {
							document.getElementById('lblbhs_drugprescription').classList.remove("blinkred");
							document.getElementById('lblbhs_additionaldrugsprescription').classList.remove("blinkred");
							document.getElementById('lblbhs_consumables').classList.remove("blinkred");
							document.getElementById('lblbhs_additionalconsumables').classList.remove("blinkred");
						}

						//netralisasi variable
						flgdrug.value = "0";
						flgdrugadd.value = "0";
						flgcons.value = "0";
						flgconsadd.value = "0";

                        <%--var FlagFocus = document.getElementById('<%= HiddenFlagSearchFocus.ClientID %>');

                        if (FlagFocus.value == "ICDfocus") {
                            document.getElementById('<%= txtSearchItemICD.ClientID %>').focus();
                            document.getElementById('<%= HiddenFlagSearchFocus.ClientID %>').value = "";
                        }--%>

						var dp = $('#<%=txtSurgeryDate.ClientID%>');
						dp.datepicker({
							changeMonth: true,
							changeYear: true,
							format: "MM yyyy",
							language: "tr"
						}).on('changeDate', function (ev) {
							$(this).blur();
							$(this).datepicker('hide');
						});

						var dp = $('#<%=txtProcoutDate.ClientID%>');
						dp.datepicker({
							changeMonth: true,
							changeYear: true,
							format: "MM yyyy",
							language: "tr"
						}).on('changeDate', function (ev) {
							$(this).blur();
							$(this).datepicker('hide');
						});

						$("#MainContent_TextJamKeluar1").mask("99:99");
						$("#MainContent_TextJamKeluar2").mask("99:99");

						ICDSuggestionSOAP();
						//DiagProcSuggestionSOAP();
						//DiagProcSuggestionSOAP_Dis();

						var txtTravelRecommendation = document.getElementById("<%=txtTravelRecommendation.ClientID %>");
						if (txtTravelRecommendation.innerText != "") {
							document.getElementById("divtravel").style.display = "block";
						}

						document.getElementById('<%=flagIsPostback.ClientID %>').value = "false";
						document.getElementsByClassName('loadingallergy')[0].style.display = "none";
						document.getElementsByClassName('loadingroutine')[0].style.display = "none";
						if (document.getElementById("<%= divpregnat.ClientID %>") != null) {
							document.getElementById("<%= divpregnat.ClientID %>").style.backgroundColor = "#ffffff";
						}

						EnDisSoapForm();
						EnDisLabRad();

						isVisiblePenanganan();
					}
				});
			};
		});

		function switchBahasaSOAP() {
			var bahasa = document.getElementById('<%=HFisBahasaSOAP.ClientID%>').value;
			if (bahasa == "ENG") {

				document.getElementById('lblbhs_reminder').innerHTML = "Reminder";
				document.getElementById('lblbhs_hideotherdoctor').innerHTML = "Hide Others Doctor's Reminder";

				var table = document.getElementById("<%=gvw_remindernotes.ClientID %>");
				if (table != null) {
					var headers = table.getElementsByTagName('th');

					if (headers.length != 0) {
						headers[0].innerText = "Reminder";
						headers[1].innerText = "Doctor";
						headers[2].innerText = "Show on Dashboard";
					}
				}

				document.getElementById('lblbhs_lastmodifsoap').innerHTML = "Last modified";
				document.getElementById('lblbhs_chiefcomplaint').innerHTML = "Chief Complaint:";
				if (document.getElementById('lblbhs_ispregnant') != null) {
					document.getElementById('lblbhs_ispregnant').innerHTML = "Is Pregnant";
					document.getElementById('lblbhs_breastfeeding').innerHTML = "Breast Feeding";
				}

				document.getElementById('lblbhs_vitalsign').innerHTML = "Vital Sign:";
				document.getElementById('lblbhs_bloodpressure').innerHTML = "Blood Pressure";
				document.getElementById('lblbhs_pulserate').innerHTML = "Pulse Rate";
				document.getElementById('lblbhs_respiratoryrate').innerHTML = "Respiratory rate";
				document.getElementById('lblbhs_temperature').innerHTML = "Temperature";
				document.getElementById('lblbhs_lingkarkepala').innerHTML = "Head Circumference"
				document.getElementById('lblbhs_weight').innerHTML = "Weight";
				document.getElementById('lblbhs_height').innerHTML = "Height";

				var table = document.getElementById("<%=gvw_icd.ClientID %>");
				if (table != null) {
					var headers = table.getElementsByTagName('th');

					if (headers.length != 0) {
						headers[0].innerText = "Disease Classification";
					}
				}

				//modal FA 
				document.getElementById('lblbhs_medication1').innerHTML = "Medication &";
				document.getElementById('lblbhs_medication2').innerHTML = "Allergies";
				document.getElementById('lblbhs_routinemedicationfa').innerHTML = "Current/Routine Medication";
				document.getElementById('lblbhs_noroutinemedicationfa').innerHTML = "No Routine Medication";
				document.getElementById('lblbhs_drugallergiesfa').innerHTML = "Drug Allergies";
				document.getElementById('lblbhs_nodrugallergiesfa').innerHTML = "No Drug Allergy";
				document.getElementById('lblbhs_foodallergiesfa').innerHTML = "Food Allergies";
				document.getElementById('lblbhs_nofoodallergiesfa').innerHTML = "No Food Allergy";

				document.getElementById('lblbhs_illnes1').innerHTML = "Health";
				document.getElementById('lblbhs_illnes2').innerHTML = "Record";
				document.getElementById('lblbhs_surgeryhistoryfa').innerHTML = "Surgery History";
				document.getElementById('lblbhs_nosurgeryhistoryfa').innerHTML = "No Surgery History";
				document.getElementById('lblbhs_diseasehistoryfa').innerHTML = "Disease History";
				document.getElementById('lblbhs_nodiseasehistoryfa').innerHTML = "No Disease History";
				document.getElementById('lblbhs_familydiseasehistoryfa').innerHTML = "Family Disease History";
				document.getElementById('lblbhs_nofamilydiseasehistoryfa').innerHTML = "No Family Disease History";
				document.getElementById('lblbhs_endemic1').innerHTML = "Endemic Area";
				document.getElementById('lblbhs_endemic2').innerHTML = "Visitation";
				document.getElementById('lblbhs_endemicareafa').innerHTML = "Have Been To Endemic Area";
				document.getElementById('lblbhs_noendemicareafa').innerHTML = "No Visit Endemic Area";
				document.getElementById('lblbhs_screeningfa').innerHTML = "Screening Infectious Disease";
				document.getElementById('lblbhs_noscreeningfa').innerHTML = "No Screening Infectious Disease";
				document.getElementById('lblbhs_nutrition1').innerHTML = "Nutrition & ";
				document.getElementById('lblbhs_nutrition2').innerHTML = "Fasting";
				document.getElementById('lblbhs_nutriproblemfa').innerHTML = "Nutrition Problem";
				document.getElementById('lblbhs_nonutriproblemfa').innerHTML = "No Nutrition Problem";
				document.getElementById('lblbhs_fastingfa').innerHTML = "Fasting";
				document.getElementById('lblbhs_nofastingfa').innerHTML = "No Fasting";
				document.getElementById('lblbhs_physical1').innerHTML = "Physical";
				document.getElementById('lblbhs_physical2').innerHTML = "Examination";
				document.getElementById('lblbhs_scoreemv').innerHTML = "Score";
				document.getElementById('lblbhs_painscalefa').innerHTML = "Pain Scale";
				document.getElementById('lblbhs_scorepain').innerHTML = "Score";
				document.getElementById('lblbhs_mentalstatusfa').innerHTML = "Mental Status";
				document.getElementById('lblbhs_clfa').innerHTML = "Consciousness Level";
				document.getElementById('lblbhs_fallriskfa').innerHTML = "Fall Risk (Condition that needs extra attention related to fall risk)";
				document.getElementById('lblbhs_nofallriskfa').innerHTML = "No Fall Risk";
				document.getElementById('lblbhs_procedureoutsidefa').innerHTML = "Procedure Outside Encounter";

				//Modal Medication & Allergies
				document.getElementById('lblbhs_editmedication').innerHTML = "Edit - Medication & Allergies";

				//Modal Illnes History
				document.getElementById('lblbhs_editillnes').innerHTML = "Edit - Health Record";

				//Modal Endemic
				document.getElementById('lblbhs_editendemic').innerHTML = "Edit - Endemic Area Visitation";

				//Modal Nutrition
				document.getElementById('lblbhs_editnutrition').innerHTML = "Edit - Nutrition & Fasting";

				//modal Rekomendasi Travel
				document.getElementById('lblbhs_rekomendasitravel').innerHTML = "Travel Recommendation";

				//Modal Physical
				document.getElementById('lblbhs_editphisycal').innerHTML = "Edit - Physical Examination";

				//Modal Submit Sign
				document.getElementById('lblbhs_consultationfee1').innerHTML = "Consultation fee";
				document.getElementById('lblbhs_spesialprice1').innerHTML = "Special Price";
				document.getElementById('lblbhs_normalprice1').innerHTML = "Normal Price";
				document.getElementById('lblbhs_freeprice1').innerHTML = "Free of Charge";
				document.getElementById('lblbhs_discount1').innerHTML = "Discount";
				//document.getElementById('lblbhs_procedurenotes1').innerHTML = "Procedure On Encounter";
				document.getElementById('lblbhs_total1').innerHTML = "Total Consultation Fee :";

				//Modal Submit Sign Disable
				document.getElementById('lblbhs_consultationfee2').innerHTML = "Consultation fee";
				document.getElementById('lblbhs_spesialprice2').innerHTML = "Special Price";
				document.getElementById('lblbhs_normalprice2').innerHTML = "Normal Price";
				document.getElementById('lblbhs_freeprice2').innerHTML = "Free of Charge";
				document.getElementById('lblbhs_discount2').innerHTML = "Discount";
				//document.getElementById('lblbhs_procedurenotes2').innerHTML = "Procedure On Encounter";
				document.getElementById('lblbhs_total2').innerHTML = "Total Consultation Fee :";

				document.getElementById('lblbhs_hasiltindakansoap').innerHTML = "Procedure Result";
				var traveldisplay = document.getElementById('lblbhs_btntravelrecomendationsoap');
				if (traveldisplay != null) {
					document.getElementById('lblbhs_rekomendasitravelsoap').innerHTML = "Travel Recommendation";
					document.getElementById('lblbhs_btntravelrecomendationsoap').innerHTML = "Travel Recommendation";
				}

				//specialty
				var btnchart = document.getElementById('lblbhs_chart');
				if (btnchart != null) {
					btnchart.innerHTML = "Growth Chart";
				}

			}
			else if (bahasa == "IND") {

				document.getElementById('lblbhs_reminder').innerHTML = "Pengingat";
				document.getElementById('lblbhs_hideotherdoctor').innerHTML = "Sembunyikan Pengingat Milik Dokter Lain";

				var table = document.getElementById("<%=gvw_remindernotes.ClientID %>");
				if (table != null) {
					var headers = table.getElementsByTagName('th');

					if (headers.length != 0) {
						headers[0].innerText = "Pengingat";
						headers[1].innerText = "Dokter";
						headers[2].innerText = "Tampilkan di Dashboard";
					}
				}

				document.getElementById('lblbhs_lastmodifsoap').innerHTML = "Terakhir diubah";
				document.getElementById('lblbhs_chiefcomplaint').innerHTML = "Keluhan Utama:";
				if (document.getElementById('lblbhs_ispregnant') != null) {
					document.getElementById('lblbhs_ispregnant').innerHTML = "Hamil";
					document.getElementById('lblbhs_breastfeeding').innerHTML = "Menyusui";
				}

				document.getElementById('lblbhs_vitalsign').innerHTML = "Tanda Vital:";
				document.getElementById('lblbhs_bloodpressure').innerHTML = "Tekanan Darah";
				document.getElementById('lblbhs_pulserate').innerHTML = "Nadi";
				document.getElementById('lblbhs_respiratoryrate').innerHTML = "Pernapasan";
				document.getElementById('lblbhs_temperature').innerHTML = "Suhu";
				document.getElementById('lblbhs_lingkarkepala').innerHTML = "Lingkar Kepala"
				document.getElementById('lblbhs_weight').innerHTML = "Berat Badan";
				document.getElementById('lblbhs_height').innerHTML = "Tinggi Badan";

				var table = document.getElementById("<%=gvw_icd.ClientID %>");
				if (table != null) {
					var headers = table.getElementsByTagName('th');

					if (headers.length != 0) {
						headers[0].innerText = "Klasifikasi Penyakit";
					}
				}

				//modal FA 
				document.getElementById('lblbhs_medication1').innerHTML = "Pengobatan &";
				document.getElementById('lblbhs_medication2').innerHTML = "Alergi";
				document.getElementById('lblbhs_routinemedicationfa').innerHTML = "Pengobatan Saat Ini";
				document.getElementById('lblbhs_noroutinemedicationfa').innerHTML = "Tidak ada Pengobatan Rutin";
				document.getElementById('lblbhs_drugallergiesfa').innerHTML = "Alergi Obat";
				document.getElementById('lblbhs_nodrugallergiesfa').innerHTML = "Tidak ada Alergi Obat";
				document.getElementById('lblbhs_foodallergiesfa').innerHTML = "Alergi Makanan";
				document.getElementById('lblbhs_nofoodallergiesfa').innerHTML = "Tidak ada Alergi Makanan";

				document.getElementById('lblbhs_illnes1').innerHTML = "Catatan";
				document.getElementById('lblbhs_illnes2').innerHTML = "Kesehatan";
				document.getElementById('lblbhs_surgeryhistoryfa').innerHTML = "Riwayat Operasi";
				document.getElementById('lblbhs_nosurgeryhistoryfa').innerHTML = "Tidak ada Riwayat Operasi";
				document.getElementById('lblbhs_diseasehistoryfa').innerHTML = "Riwayat Penyakit";
				document.getElementById('lblbhs_nodiseasehistoryfa').innerHTML = "Tidak ada Riwayat Penyakit";
				document.getElementById('lblbhs_familydiseasehistoryfa').innerHTML = "Riwayat Penyakit Keluarga";
				document.getElementById('lblbhs_nofamilydiseasehistoryfa').innerHTML = "Tidak ada Riwayat Penyakit Keluarga";
				document.getElementById('lblbhs_endemic1').innerHTML = "Kunjungan";
				document.getElementById('lblbhs_endemic2').innerHTML = "Area Endemis";
				document.getElementById('lblbhs_endemicareafa').innerHTML = "Pernah ke Daerah Endemis";
				document.getElementById('lblbhs_noendemicareafa').innerHTML = "Tidak Pernah ke Daerah Endemis";
				document.getElementById('lblbhs_screeningfa').innerHTML = "Skrining Penyakit Infeksius";
				document.getElementById('lblbhs_noscreeningfa').innerHTML = "Tidak ada Skrining Penyakit Infeksius";
				document.getElementById('lblbhs_nutrition1').innerHTML = "Nutrisi & ";
				document.getElementById('lblbhs_nutrition2').innerHTML = "Puasa";
				document.getElementById('lblbhs_nutriproblemfa').innerHTML = "Masalah Nutrisi";
				document.getElementById('lblbhs_nonutriproblemfa').innerHTML = "Tidak ada Masalah Nutrisi";
				document.getElementById('lblbhs_fastingfa').innerHTML = "Puasa";
				document.getElementById('lblbhs_nofastingfa').innerHTML = "Tidak Puasa";
				document.getElementById('lblbhs_physical1').innerHTML = "Pemeriksaan";
				document.getElementById('lblbhs_physical2').innerHTML = "Fisik";
				document.getElementById('lblbhs_scoreemv').innerHTML = "Skor";
				document.getElementById('lblbhs_painscalefa').innerHTML = "Skala Nyeri";
				document.getElementById('lblbhs_scorepain').innerHTML = "Skor";
				document.getElementById('lblbhs_mentalstatusfa').innerHTML = "Status Mental";
				document.getElementById('lblbhs_clfa').innerHTML = "Tingkat Kesadaran";
				document.getElementById('lblbhs_fallriskfa').innerHTML = "Risiko Jatuh (Kondisi yang memerlukan perhatian khusus terkait risiko jatuh)";
				document.getElementById('lblbhs_nofallriskfa').innerHTML = "Tidak ada Resiko Jatuh";
				document.getElementById('lblbhs_procedureoutsidefa').innerHTML = "Tindakan di Luar Pertemuan";

				//Modal Medication & Allergies
				document.getElementById('lblbhs_editmedication').innerHTML = "Edit - Pengobatan & Alergi";

				//Modal Illnes History
				document.getElementById('lblbhs_editillnes').innerHTML = "Edit - Catatan Kesehatan";

				//Modal Endemic
				document.getElementById('lblbhs_editendemic').innerHTML = "Edit - Kunjungan Daerah Endemis";

				//Modal Nutrition
				document.getElementById('lblbhs_editnutrition').innerHTML = "Edit - Nutrisi & Puasa";

				//modal Rekomendasi Travel
				document.getElementById('lblbhs_rekomendasitravel').innerHTML = "Rekomendasi Perjalanan";

				//Modal Physical
				document.getElementById('lblbhs_editphisycal').innerHTML = "Edit - Pemeriksaan Fisik";

				//Modal Submit Sign
				document.getElementById('lblbhs_consultationfee1').innerHTML = "Biaya Konsultasi";
				document.getElementById('lblbhs_spesialprice1').innerHTML = "Harga Spesial";
				document.getElementById('lblbhs_normalprice1').innerHTML = "Harga normal";
				document.getElementById('lblbhs_freeprice1').innerHTML = "Gratis";
				document.getElementById('lblbhs_discount1').innerHTML = "Diskon";
				//document.getElementById('lblbhs_procedurenotes1').innerHTML = "Tindakan Berdasarkan Pertemuan";
				document.getElementById('lblbhs_total1').innerHTML = "Total Biaya Konsultasi :";

				//Modal Submit Sign Disable
				document.getElementById('lblbhs_consultationfee2').innerHTML = "Biaya Konsultasi";
				document.getElementById('lblbhs_spesialprice2').innerHTML = "Harga Spesial";
				document.getElementById('lblbhs_normalprice2').innerHTML = "Harga normal";
				document.getElementById('lblbhs_freeprice2').innerHTML = "Gratis";
				document.getElementById('lblbhs_discount2').innerHTML = "Diskon";
				//document.getElementById('lblbhs_procedurenotes2').innerHTML = "Tindakan Berdasarkan Pertemuan";
				document.getElementById('lblbhs_total2').innerHTML = "Total Biaya Konsultasi :";

				document.getElementById('lblbhs_hasiltindakansoap').innerHTML = "Hasil Tindakan";
				var traveldisplay = document.getElementById('lblbhs_btntravelrecomendationsoap');
				if (traveldisplay != null) {
					document.getElementById('lblbhs_rekomendasitravelsoap').innerHTML = "Rekomendasi Perjalanan";
					document.getElementById('lblbhs_btntravelrecomendationsoap').innerHTML = "Rekomendasi Perjalanan";
				}

				//specialty
				var btnchart = document.getElementById('lblbhs_chart');
				if (btnchart != null) {
					btnchart.innerHTML = "Kurva Pertumbuhan";
				}
			}
		}

		function resetSOAP() {
			warningnotificationOption();
			toastr.warning('Are you sure to Reset SOAP Data? <br /> <div style="text-align:right; padding-top:5px;"><button type="button" class="btn btn-success btn-sm" style="height: 25px; padding-top: 3px; width: 55px;" value="yes" onclick="clearFormSOAP();">YES</button> &nbsp; <button type="button" class="btn btn-danger btn-sm clear" style="height: 25px; padding-top: 3px; width: 55px;" value="no">NO</button> </div>', 'SOAP');

            <%--toastr.warning('Are you sure to Reset SOAP Data? <br /> <div style="text-align:right; padding-top:5px;"><button type="button" class="btn btn-success btn-sm" style="height: 25px; padding-top: 3px; width: 55px;" value="yes">YES</button> &nbsp; <button type="button" class="btn btn-danger btn-sm clear" style="height: 25px; padding-top: 3px; width: 55px;" value="no">NO</button> </div>', 'Submit Alert!',
                {
                    allowHtml: true,
                    onclick: function (toast) {
                        value = toast.target.value
                        if (value == 'yes') {
                            document.getElementById("<%=Complaint.ClientID %>").value = "";
                            document.getElementById("<%=Anamnesis.ClientID %>").value = "";
                            document.getElementById("<%=txtOthers.ClientID %>").value = "";
                            document.getElementById("<%=txtPrimary.ClientID %>").value = "";
                            document.getElementById("<%=txtPlanning.ClientID %>").value = "";
                            document.getElementById("<%=chkpregnant.ClientID %>").checked = false;
                            document.getElementById("<%=chkbreastfeed.ClientID %>").checked = false;

                            document.getElementById("<%=txtbloodhigh.ClientID %>").value = "";
                            document.getElementById("<%=txtbloodlow.ClientID %>").value = "";
                            document.getElementById("<%=txtpulserate.ClientID %>").value = "";
                            document.getElementById("<%=txtrespiratory.ClientID %>").value = "";
                            document.getElementById("<%=txtspo.ClientID %>").value = "";
                            document.getElementById("<%=txttemperature.ClientID %>").value = "";
                            document.getElementById("<%=txtweight.ClientID %>").value = "";
                            document.getElementById("<%=txtheight.ClientID %>").value = "";

                            toastr.clear();
                        }
                        else {
                            toastr.clear();
                        }
                    }
                });--%>
		}

		function clearFormSOAP() {
			document.getElementById("<%=Complaint.ClientID %>").value = "";
			document.getElementById("<%=Anamnesis.ClientID %>").value = "";
			document.getElementById("<%=txtOthers.ClientID %>").value = "";
			document.getElementById("<%=txtPrimary.ClientID %>").value = "";
			document.getElementById("<%=txtPlanning.ClientID %>").value = "";
			document.getElementById("<%=chkpregnant.ClientID %>").checked = false;
			document.getElementById("<%=chkbreastfeed.ClientID %>").checked = false;

			document.getElementById("<%=txtbloodhigh.ClientID %>").value = "";
			document.getElementById("<%=txtbloodlow.ClientID %>").value = "";
			document.getElementById("<%=txtpulserate.ClientID %>").value = "";
			document.getElementById("<%=txtrespiratory.ClientID %>").value = "";
			document.getElementById("<%=txtspo.ClientID %>").value = "";
			document.getElementById("<%=txttemperature.ClientID %>").value = "";
			document.getElementById("<%=txtweight.ClientID %>").value = "";
			document.getElementById("<%=txtheight.ClientID %>").value = "";
		}

		function checkTextAreaMaxLength(textBox, e, length) {
			var mLen = textBox["MaxLength"];
			if (null == mLen)
				mLen = length;

			var maxLength = parseInt(mLen);
			if (!checkSpecialKeys(e)) {
				if (textBox.value.length > maxLength - 1) {
					if (window.event)//IE
						e.returnValue = false;
					else//Firefox
						e.preventDefault();
				}
			}
		}

		function checkSpecialKeys(e) {
			if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
				return false;
			else
				return true;
		}

		function cekbeforecopyhope() {
			var divx = document.getElementById('<%= div_nodatacopyhope.ClientID %>');
			if (divx.style.display == "block") {
				divx.classList.remove();
				divx.classList.add("error-info");
				document.getElementById('lbl_nodatacopyhope').innerText = "Please Select Data First!";
				return false;
			}
			return true;
		}

		function showError() {
			//show the modal
			$('#modalError').modal('show');

			//make the background transparent
			$('.modal-backdrop').addClass('clearr');

			return false;
		}

        function copytextto() {
            var diagnosis = document.getElementById("MainContent_ModalRawatInap_textbox_diagnosis");
            var from_elm = document.getElementById('<%=txtPrimary.ClientID%>');
            var to_elm = document.getElementsByClassName('copydata');

			if (to_elm != null) {
				to_elm[0].value = from_elm.value;
				AutoExpand(to_elm[0]);

                if(to_elm[1] != null)
                {
                    var FO = document.getElementById("MainContent_StdPlanning_divFutureOrder");
                    if (FO.style.display != "none") {
                        to_elm[1].value = from_elm.value;
                        AutoExpand(to_elm[1]);
                    }
					if (diagnosis != null) {
                    diagnosis.value = from_elm.value;
					}
                }
            }
        }

		function checkCopySOAP() {
			if (document.getElementById('<%= chkSubjective.ClientID %>').disabled == false) { document.getElementById('<%= chkSubjective.ClientID %>').checked = true; }
			if (document.getElementById('<%= chkObjective.ClientID %>').disabled == false) { document.getElementById('<%= chkObjective.ClientID %>').checked = true; }
			if (document.getElementById('<%= chkAssessment.ClientID %>').disabled == false) { document.getElementById('<%= chkAssessment.ClientID %>').checked = true; }
			if (document.getElementById('<%= chkPlanning.ClientID %>').disabled == false) { document.getElementById('<%= chkPlanning.ClientID %>').checked = true; }
			if (document.getElementById('<%= chkLab.ClientID %>').disabled == false) { document.getElementById('<%= chkLab.ClientID %>').checked = true; }
			if (document.getElementById('<%= chkRad.ClientID %>').disabled == false) { document.getElementById('<%= chkRad.ClientID %>').checked = true; }
			if (document.getElementById('<%= chkDrugs.ClientID %>').disabled == false) { document.getElementById('<%= chkDrugs.ClientID %>').checked = true; }
			if (document.getElementById('<%= chkCompound.ClientID %>').disabled == false) { document.getElementById('<%= chkCompound.ClientID %>').checked = true; }
			if (document.getElementById('<%= chkConsumables.ClientID %>').disabled == false) { document.getElementById('<%= chkConsumables.ClientID %>').checked = true; }

			ToogleCopyDrugsCheckbox();
		}

		function uncheckCopySOAP() {
			document.getElementById('<%= chkSubjective.ClientID %>').checked = false;
			document.getElementById('<%= chkObjective.ClientID %>').checked = false;
			document.getElementById('<%= chkAssessment.ClientID %>').checked = false;
			document.getElementById('<%= chkPlanning.ClientID %>').checked = false;
			document.getElementById('<%= chkLab.ClientID %>').checked = false;
			document.getElementById('<%= chkRad.ClientID %>').checked = false;
			document.getElementById('<%= chkDrugs.ClientID %>').checked = false;
			document.getElementById('<%= chkCompound.ClientID %>').checked = false;
			document.getElementById('<%= chkConsumables.ClientID %>').checked = false;

			ToogleCopyDrugsCheckbox();
		}

		function confirmCopySOAP() {

			var dialog = "";
			if (document.getElementById('<%= chkSubjective.ClientID %>').checked == true) { dialog = dialog + "- Subjective\n"; }
			if (document.getElementById('<%= chkObjective.ClientID %>').checked == true) { dialog = dialog + "- Objective\n"; }
			if (document.getElementById('<%= chkAssessment.ClientID %>').checked == true) { dialog = dialog + "- Assesment\n"; }
			if (document.getElementById('<%= chkPlanning.ClientID %>').checked == true) { dialog = dialog + "- Planning & Procedure\n"; }
			if (document.getElementById('<%= chkLab.ClientID %>').checked == true) { dialog = dialog + "- Laboratory\n"; }
			if (document.getElementById('<%= chkRad.ClientID %>').checked == true) { dialog = dialog + "- Radiology\n"; }
			if (document.getElementById('<%= chkDrugs.ClientID %>').checked == true) { dialog = dialog + "- Drug Prescription\n"; }
			if (document.getElementById('<%= chkCompound.ClientID %>').checked == true) { dialog = dialog + "- Compound\n"; }
			if (document.getElementById('<%= chkConsumables.ClientID %>').checked == true) { dialog = dialog + "- Consumable\n"; }

			if (dialog == "") {
				alert("\nPlease select minimum 1 item to copy soap!");
				return false;
			}
			else {
				var cc = "\nAre you sure to copy these selected items? \n\n" + dialog;
				//var answer = window.confirm(cc);
				//if (answer) {
				//    return true;
				//}
				//else {
				//    return false;
				//}

				swal({
					title: "Copy SOAP Warning",
					text: cc,
					buttons: true,
					dangerMode: true
				}).then((willCopy) => {
					if (willCopy) {
						document.getElementById('<%= btncopy.ClientID %>').click();
					}
					else {
						return false;
					}
				});
			}
		}

		$('body').scroll(function (e) {
			if ($(this).scrollTop() > document.getElementById('gotolabrad').offsetTop) {
				document.getElementById('tombolPfloat').className = "floatbuttonlabrad-show";
			} else {
				document.getElementById('tombolPfloat').className = "floatbuttonlabrad-hide";
			}
		});

		function shortcutFormAllergy() {
            //document.getElementById("<%=rbdrug1.ClientID %>").checked = false;
            //document.getElementById("<%=rbdrug2.ClientID %>").checked = true;

			document.getElementsByClassName('loadingallergy')[0].style.display = "block";
			document.getElementById('<%= btnEditRoutine.ClientID %>').click();
		}

		function shortcutFormRoutine() {

			document.getElementsByClassName('loadingroutine')[0].style.display = "block";
			document.getElementById('<%= btnEditRoutine.ClientID %>').click();
		}

		function shortcutNoDrug() {
			document.getElementById("<%=rbdrug1.ClientID %>").checked = true;
			document.getElementById("<%=rbdrug2.ClientID %>").checked = false;
			document.getElementById('<%= NoDrugAllergy.ClientID %>').click();
		}

		function shortcutNoFood() {
			document.getElementById("<%=rbfood1.ClientID %>").checked = true;
			document.getElementById("<%=rbfood2.ClientID %>").checked = false;
			document.getElementById('<%= NoFoodAllergy.ClientID %>').click();
		}

		function shortcutNoOther() {
			document.getElementById("<%=rbother1.ClientID %>").checked = true;
			document.getElementById("<%=rbother2.ClientID %>").checked = false;
			document.getElementById('<%= NoOtherAllergy.ClientID %>').click();
		}

		function shortcutNoAllAllergy() {
			document.getElementsByClassName('loadingallergy')[0].style.display = "block";

			document.getElementById("<%=rbdrug1.ClientID %>").checked = true;
			document.getElementById("<%=rbdrug2.ClientID %>").checked = false;
			document.getElementById("<%=rbfood1.ClientID %>").checked = true;
			document.getElementById("<%=rbfood2.ClientID %>").checked = false;
			document.getElementById("<%=rbother1.ClientID %>").checked = true;
			document.getElementById("<%=rbother2.ClientID %>").checked = false;
			document.getElementById('<%= NoAllAllergy.ClientID %>').click();
		}

		function shortcutNoRoutine() {
			document.getElementsByClassName('loadingroutine')[0].style.display = "block";

			document.getElementById("<%=rbPengobatan1.ClientID %>").checked = true;
			document.getElementById("<%=rbPengobatan2.ClientID %>").checked = false;
			document.getElementById('<%= NoRoutineMedication.ClientID %>').click();
		}

		function shortcutFormIllness() {

			//document.getElementsByClassName('loadingallergy')[0].style.display = "block";
			document.getElementById('<%= btnEditIllness.ClientID %>').click();
		}

		function keepAlive() {
			var postback = document.getElementById('<%=flagIsPostback.ClientID %>');
			if (postback.value != "true") {
				console.log('session refresh');
				document.getElementById('<%= KeepAliveButton.ClientID %>').click();
			}
		}

		function backupSOAP() {
			document.getElementById('<%= KeepAliveButton.ClientID %>').click();
		}

		setInterval(keepAlive, 600000); //10 minute

		function checkmandatoryradioModal() {
			var settingmandatory = document.getElementById("<%=hfmandatoryFA.ClientID %>");
			var flagMan = "no";
			var msgMan = "";

			var raddrugno = document.getElementById("<%=rbdrug1.ClientID %>");
			var raddrugyes = document.getElementById("<%=rbdrug2.ClientID %>");
			var radfoodno = document.getElementById("<%=rbfood1.ClientID %>");
			var radfoodyes = document.getElementById("<%=rbfood2.ClientID %>");
			var radotherno = document.getElementById("<%=rbother1.ClientID %>");
			var radotheryes = document.getElementById("<%=rbother2.ClientID %>");
			var radroutineno = document.getElementById("<%=rbPengobatan1.ClientID %>");
			var radroutineyes = document.getElementById("<%=rbPengobatan2.ClientID %>");

			if (settingmandatory.value.includes("DRUG_ALLERGY")) {
				if (raddrugno.checked == false && raddrugyes.checked == false) {
					//alert("Alergi Obat is mandatory!");
					//return false;
					flagMan = "yes";
					msgMan = msgMan + "- Alergi Obat is mandatory! \n";
				}
				else if (raddrugyes.checked == true) {
					var tbldrug = document.getElementById("<%=gvw_allergy.ClientID %>");
					var rowdrug = tbldrug.getElementsByTagName("th");
					if (rowdrug.length == 0) {
						alert("Data Alergi Obat tidak boleh kosong!");
						return false;
					}
				}
			}

			if (settingmandatory.value.includes("FOOD_ALLERGY")) {
				if (radfoodno.checked == false && radfoodyes.checked == false) {
					//alert("Alergi Makanan is mandatory!");
					//return false;
					flagMan = "yes";
					msgMan = msgMan + "- Alergi Makanan is mandatory! \n";
				}
				else if (radfoodyes.checked == true) {
					var tblfood = document.getElementById("<%=gvw_foods.ClientID %>");
					var rowfood = tblfood.getElementsByTagName("th");
					if (rowfood.length == 0) {
						alert("Data Alergi Makanan tidak boleh kosong!");
						return false;
					}
				}
			}

			if (settingmandatory.value.includes("OTHER_ALLERGY")) {
				if (radotherno.checked == false && radotheryes.checked == false) {
					//alert("Alergi Lain-lain is mandatory!");
					//return false;
					flagMan = "yes";
					msgMan = msgMan + "- Alergi Lain-lain is mandatory! \n";
				}
				else if (radotheryes.checked == true) {
					var tblother = document.getElementById("<%=gvw_others.ClientID %>");
					var rowother = tblother.getElementsByTagName("th");
					if (rowother.length == 0) {
						alert("Data Alergi Lain-lain tidak boleh kosong!");
						return false;
					}
				}
			}

			if (settingmandatory.value.includes("PENGOBATAN_SAAT_INI")) {
				if (radroutineno.checked == false && radroutineyes.checked == false) {
					//alert("Routine Medication is mandatory!");
					//return false;
					flagMan = "yes";
					msgMan = msgMan + "- Routine Medication is mandatory! \n";
				}
				else if (radroutineyes.checked == true) {
					var tblroutine = document.getElementById("<%=gvw_routinemed.ClientID %>");
					var rowroutine = tblroutine.getElementsByTagName("th");
					if (rowroutine.length == 0) {
						alert("Data Routine Medication tidak boleh kosong!");
						return false;
					}
				}
			}

			if (flagMan == "yes") {
				alert(msgMan);
				return false;
			}
		}

		function ToogleCopyDrugsCheckbox() {
			document.getElementById("<%= ButtonToogleChkDrugs.ClientID %>").click();
		}

		function openInNewTab(href) {
			Object.assign(document.createElement('a'), {
				target: '_blank',
				href,
			}).click();
		}

		function previewRujukan(ip, orgid, ptnid, admid, encid, hopeuid) {
			//if (IP.value != window.location.hostname) {
			//    IP.value = "10.83.254.38";
			//}
			var url = "http://" + ip + "/viewer/Form/Referenceletter.aspx?organization_id=" + orgid + "&admission_id=" + admid + "&encounter_id=" + encid + "&patient_id=" + ptnid + "&user_id=" + hopeuid + "";

			//var url = "http://" + "10.83.254.38" + ":5510";
			openInNewTab(url);
		}

		//obgyn section//

		function dateHPHT() {
			var dp = $('#<%=txtHPHT.ClientID%>');
			dp.datepicker({
				changeMonth: true,
				changeYear: true,
				format: "dd M yyyy",
				language: "tr",
				todayHighlight: true
			}).on('changeDate', function (ev) {
				$(this).blur();
				$(this).datepicker('hide');
			});
		}

		function dateTHL() {
			var dp = $('#<%=txtTHL.ClientID%>');
			dp.datepicker({
				changeMonth: true,
				changeYear: true,
				format: "dd M yyyy",
				language: "tr",
				todayHighlight: true
			}).on('changeDate', function (ev) {
				$(this).blur();
				$(this).datepicker('hide');
			});
		}

		function dateCalcForTHL() {
			var HPHT = document.getElementById("<%= txtHPHT.ClientID %>");
			var THL = document.getElementById("<%= txtTHL.ClientID %>");

			var HPHTdate = new Date(HPHT.value);
			var THLdate = HPHTdate;

			THLdate.setDate(HPHTdate.getDate() + 7);
			THLdate.setMonth(THLdate.getMonth() - 3);
			THLdate.setFullYear(THLdate.getFullYear() + 1);

			const monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']


			//THL.value = THLdate;
			THL.value = THLdate.getDate() + " " + monthNames[THLdate.getMonth()] + " " + THLdate.getFullYear();
		}

		function clearText(objek) {
			document.getElementById('MainContent_' + objek).value = "";
		}

		//pediatric section//

		function ShowKurva() {
			$('#modalKurvaPertumbuhan').modal('show');
			document.getElementById("<%= ButtonShowKurva.ClientID %>").click();
			return true;
		}

		function HideKurva() {
			$('#modalKurvaPertumbuhan').modal('hide');
			return true;
		}

		//emergency section//

		function datepasienpulang() {
			var dp = $('#<%=TextTglKeluar1.ClientID%>');
			dp.datepicker({
				changeMonth: true,
				changeYear: true,
				format: "dd MM yyyy",
				language: "tr"
			}).on('changeDate', function (ev) {
				$(this).blur();
				$(this).datepicker('hide');
			});
		}

		function datepasienpulangdisable() {
			var dp = $('#<%=TextTglKeluar2.ClientID%>');
			dp.datepicker({
				changeMonth: true,
				changeYear: true,
				format: "dd MM yyyy",
				language: "tr"
			}).on('changeDate', function (ev) {
				$(this).blur();
				$(this).datepicker('hide');
			});
		}

		function enableTXTEWS(iden, iddis1, iddis2) {
			$("[id$='" + iden + "']").removeAttr("Disabled");
			$("[id$='" + iddis1 + "']").attr("disabled", "disabled");
			$("[id$='" + iddis2 + "']").attr("disabled", "disabled");

			$("[id$='" + iddis1 + "']").val("");
			$("[id$='" + iddis2 + "']").val("");
		}

		function enableTXTTL(idddl, idtxt) {
			var x = $("[id$='" + idddl + "']").val();
			if (x == "-") {
				$("[id$='" + idtxt + "']").attr("disabled", "disabled");
				$("[id$='" + idtxt + "']").val("");
			}
			else {
				$("[id$='" + idtxt + "']").removeAttr("Disabled");
			}
		}

		function hitungBMI() {
			var BB = document.getElementById("<%=txtweight.ClientID%>");
			var TB = document.getElementById("<%=txtheight.ClientID%>");
			var BMI = document.getElementById("<%=txtbmi.ClientID%>");

			if (BB.value != "" && TB.value != "") {
				BMI.value = (BB.value / ((TB.value / 100) * (TB.value / 100))).toFixed(2);
			}
		}

		function MimsWarningLog(flagg) {
			this.event.preventDefault();
			var admid = new URLSearchParams(window.location.search).get('AdmissionId');
			var admno = document.getElementById("MainContent_PatientCard_lblAdmissionNo").innerText;
			var mrno = document.getElementById("MainContent_PatientCard_localMrNo").innerText;
			var patientname = document.getElementById("MainContent_PatientCard_patientName").innerText;

			var reasonid = "";
			$('#table_reason_mims input[type="checkbox"]').each(function (index) {
				if ($(this).prop('checked') == true) {
					reasonid = reasonid + document.getElementById('MainContent_StdPlanning_RepeaterChkReason_Lbl_Reason_' + index).innerText + ",";
				}
			});
			if (reasonid.length > 0) {
				reasonid = reasonid.slice(0, -1);
			}

			var txtreasonother = document.getElementById("MainContent_StdPlanning_Txt_ReasonOther");

			var paramnya = { 'flagg': flagg, 'admid': admid, 'admno': admno, 'mrno': mrno, 'patientname': patientname, 'reasonarray': reasonid, 'txtreasonother': txtreasonother.value };
			$.ajax({
				type: "POST",
				url: "StdSoapPageSpecialty.aspx/LogMims",
				data: JSON.stringify(paramnya),
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (msg) {
					if (msg.d == "success") {
						console.log("mims action : " + flagg + " success");
					}
					else {
						console.log("mims action : " + msg.d);
					}
				},
				error: function (xmlhttprequest, textstatus, errorthrown) {
					//alert(" conection to the server failed ");
					console.log("error: " + errorthrown);
				}
			});
		}

		//HEALTH RECORD

		function CollectIframe_MedicationAllergies() {
			var obj = {
				type: 'parentRequest_MA',
				flag: 'collect',
				requestData: 'Collect Data'
			};
			document.getElementById("<%=IframeMedication.ClientID%>").contentWindow.postMessage(obj, "*");
		}

		function SaveIframe_MedicationAllergies() {
			var obj = {
				type: 'parentRequest_MA',
				flag: 'save',
				requestData: 'Save Data'
			};
			document.getElementById("loading-MA").style.display = "inline-block";
			document.getElementById("<%=IframeMedication.ClientID%>").contentWindow.postMessage(obj, "*");

		}

		function ForceNoIframe_MedicationAllergies(flag) {
			var obj = {
				type: 'parentRequest_MA',
				flag: 'forceno',
				requestData: 'Force No Data ' + flag
			};
			if (flag == "Allergy") {
				document.getElementById("loading-MA-Allergy").style.display = "inline-block";
			}
			else if (flag == "Routine") {
				document.getElementById("loading-MA-Routine").style.display = "inline-block";
			}
			document.getElementById("<%=IframeMedication.ClientID%>").contentWindow.postMessage(obj, "*");
		}

		function SubmitIframe_MedicationAllergies() {
			var obj = {
				type: 'parentRequest_MA',
				flag: 'submit',
				requestData: 'Submit Data'
			};
			document.getElementById("<%=IframeMedication.ClientID%>").contentWindow.postMessage(obj, "*");
		}

		function CollectIframe_HealthRecord() {

			var obj = {
				type: 'parentRequest_HR',
				flag: 'collect',
				requestData: 'Collect Data'
			};
			document.getElementById("<%=IframeHealthrecord.ClientID%>").contentWindow.postMessage(obj, "*");
		}

		function SaveIframe_HealthRecord() {

			var obj = {
				type: 'parentRequest_HR',
				flag: 'save',
				requestData: 'Save Data'
			};
			document.getElementById("loading-HR").style.display = "inline-block";
			document.getElementById("<%=IframeHealthrecord.ClientID%>").contentWindow.postMessage(obj, "*");

		}

		function ForceNoIframe_HealthRecord() {

			var obj = {
				type: 'parentRequest_HR',
				flag: 'forceno',
				requestData: 'Force No Data'
			};
			document.getElementById("<%=IframeHealthrecord.ClientID%>").contentWindow.postMessage(obj, "*");

		}

		function SubmitIframe_HealthRecord() {

			var obj = {
				type: 'parentRequest_HR',
				flag: 'submit',
				requestData: 'Submit Data'
			};
			document.getElementById("<%=IframeHealthrecord.ClientID%>").contentWindow.postMessage(obj, "*");
		}

		function SubmitIframe_HI(flag) {

			$("[id$='hfsavemodeHI']").val(flag);
			document.getElementById("loading_HI").style.display = "block";

			var MA = SubmitIframe_MedicationAllergies();
			var HR = SubmitIframe_HealthRecord();

			if (MA == false || HR == false) {
				return false;
			}

			return true;
		}

		function HideMA() {
			$('#modalEditMedicationIframe').modal('hide');
		}

		function HideHR() {
			$('#modalEditIllnessIframe').modal('hide');
		}

		function messageHandler(event) {
            //if (event.data && (event.data.type === 'iframeResponse')) {
            //    //// Do some stuff with the sent data
            //    //var obj = document.getElementById("status");
            //    //obj.value = e.data.responseData;
            //    alert(event.data.responseData);
            //}

            <%--if (event.data && (event.data.type === 'iframeResponse_MA')) {
                if (event.data && (event.data.flag === 'collect')) {
                    if (event.data.responseData == 'false') {
                        return false;
                    }
                }
                else if (event.data && (event.data.flag === 'save')) {
                    var data = document.getElementById("<%=HF_CollectedData_MA.ClientID%>");
                    data.value = event.data.responseData;

                    HideMA();
                    document.getElementById("<%=BtnSaveDrugAllergy_HI_exec.ClientID%>").click();
                }
                else if (event.data && (event.data.flag === 'submit')) {
                    if (event.data.responseData == 'false') {
                        return false;
                    }
                }
            }
            else if (event.data && (event.data.type === 'iframeResponse_HR')) {
                if (event.data && (event.data.flag === 'collect')) {
                    if (event.data.responseData == 'false') {
                        return false;
                    }
                }
                else if (event.data && (event.data.flag === 'save')) {
                    var data = document.getElementById("<%=HF_CollectedData_HR.ClientID%>");
                    data.value = event.data.responseData;

                    HideHR();
                    document.getElementById("<%=BtnSaveHealthRecord_HI_exec.ClientID%>").click();
                }
                else if (event.data && (event.data.flag === 'submit')) {
                    if (event.data.responseData == 'false') {
                        return false;
                    }
                }
            }--%>

			if (event.data && (event.data.type === 'iframeResponse_MA')) {
				if (event.data && (event.data.flag === 'submit')) {
					if (event.data.responseData == 'false') {
						klikSubmitSOAP('MA', false)
					}
					else {
						klikSubmitSOAP('MA', true)
					}
				}
				//else if (event.data && (event.data.flag === 'validasi')) {
				//    if (event.data.responseData == 'false') {
				//        return false;
				//    }
				//    else {
				//        SubmitIframe_HI();                    
				//    }
				//}
			}
			else if (event.data && (event.data.type === 'iframeResponse_HR')) {
				if (event.data && (event.data.flag === 'submit')) {
					if (event.data.responseData == 'false') {
						klikSubmitSOAP('HR', false)
					}
					else {
						klikSubmitSOAP('HR', true)
					}
				}
			}

			if (event.data && (event.data.flag === 'save')) {
				if (event.data.responseData == 'false') {
					document.getElementById("loading-MA").style.display = "none";
					document.getElementById("loading-HR").style.display = "none";
				}
			}

			if (event.data && (event.data.type === 'collecteddataMA')) {

				var data = document.getElementById("<%=HF_CollectedData_MA.ClientID%>");
				data.value = event.data.responseData;

				HideMA();
				document.getElementById("loading-MA").style.display = "none";
				document.getElementById("loading-MA-Allergy").style.display = "none";
				document.getElementById("loading-MA-Routine").style.display = "none";

				document.getElementById("<%=BtnSaveDrugAllergy_HI.ClientID%>").click();
			}

			if (event.data && (event.data.type === 'collecteddataHR')) {

				var data = document.getElementById("<%=HF_CollectedData_HR.ClientID%>");
				data.value = event.data.responseData;

				HideHR();
				document.getElementById("loading-HR").style.display = "none";

				document.getElementById("<%=BtnSaveHealthRecord_HI.ClientID%>").click();
			}
		}

		var vlMA = false;
		var vlHR = false;

		function klikSubmitSOAP(flagfn, vl) {
			if (flagfn == 'MA') {
				vlMA = vl;
			}
			if (flagfn == 'HR') {
				vlHR = vl;
			}

			if (vlMA == true && vlHR == true) {
				vlMA = false;
				vlHR = false;

				var fl = $("[id$='hfsavemodeHI']").val();
				if (fl == "DRAFT") {
					document.getElementById('<%=btnsave.ClientID%>').click();
				}
				else if (fl == "SUBMIT") {
					ModalSubmit();
				}
				document.getElementById("loading_HI").style.display = "none";
			}
			else if (vlMA == false && vlHR == false) {

				toastr.info('Health Info Data Not Saved', 'Connection to GTN Fail!');

				var fl = $("[id$='hfsavemodeHI']").val();
				if (fl == "DRAFT") {
					document.getElementById('<%=btnsave.ClientID%>').click();
				}
				else if (fl == "SUBMIT") {
					ModalSubmit();
				}
				document.getElementById("loading_HI").style.display = "none";
			}
		}

		function ModalAlasan() {
			$('#modalAlasanKewaspadaan').modal('show');
			return true;
		}

		function CheckKewaspadaanS() {
			$('#MainContent_chkKewaspadaanStandard').on('change', function (e) {
				e.preventDefault();
				if (!e.target.checked) {
					if ($('#MainContent_HF_isCheckS').val() == "true") {
						ModalAlasan();
						$('#MainContent_HF_alertType').val("standard");
					}
				}
			});
			$('#MainContent_txtAlasanKewaspadaan').on('input', function () {
				if ($('#MainContent_txtAlasanKewaspadaan').val() == "") {
					$('#MainContent_btnSvAlertDltReason').addClass("disabled-form");
				} else {
					$('#MainContent_btnSvAlertDltReason').removeClass("disabled-form");
				}

			});
		}

		function CheckKewaspadaanK() {
			$('#MainContent_chkKewaspadaanKontak').on('change', function (e) {
				e.preventDefault();
				if (!e.target.checked) {
					if ($('#MainContent_HF_isCheckK').val() == "true") {
						ModalAlasan();
						$('#MainContent_HF_alertType').val("kontak");
					}
				}
			});
			$('#MainContent_txtAlasanKewaspadaan').on('input', function () {
				if ($('#MainContent_txtAlasanKewaspadaan').val() == "") {
					$('#MainContent_btnSvAlertDltReason').addClass("disabled-form");
				} else {
					$('#MainContent_btnSvAlertDltReason').removeClass("disabled-form");
				}

			});
		}

		function CheckKewaspadaanD() {
			$('#MainContent_chkKewaspadaanDroplet').on('change', function (e) {
				e.preventDefault();
				if (!e.target.checked) {
					if ($('#MainContent_HF_isCheckD').val() == "true") {
						ModalAlasan();
						$('#MainContent_HF_alertType').val("droplet");
					}
				}
			});
			$('#MainContent_txtAlasanKewaspadaan').on('input', function () {
				if ($('#MainContent_txtAlasanKewaspadaan').val() == "") {
					$('#MainContent_btnSvAlertDltReason').addClass("disabled-form");
				} else {
					$('#MainContent_btnSvAlertDltReason').removeClass("disabled-form");
				}

			});
		}

		function CheckKewaspadaanA() {
			$('#MainContent_chkKewaspadaanAirborne').on('change', function (e) {
				e.preventDefault();
				if (!e.target.checked) {
					if ($('#MainContent_HF_isCheckA').val() == "true") {
						ModalAlasan();
						$('#MainContent_HF_alertType').val("airborne");
					}
				}
			});
			$('#MainContent_txtAlasanKewaspadaan').on('input', function () {
				if ($('#MainContent_txtAlasanKewaspadaan').val() == "") {
					$('#MainContent_btnSvAlertDltReason').addClass("disabled-form");
				} else {
					$('#MainContent_btnSvAlertDltReason').removeClass("disabled-form");
				}

			});
		}




		function DismissModalAlasan() {
			if ($('#MainContent_HF_alertType').val() == "standard") {
				$('#MainContent_chkKewaspadaanStandard').prop("checked", true)
			}

			if ($('#MainContent_HF_alertType').val() == "kontak") {
				$('#MainContent_chkKewaspadaanKontak').prop("checked", true)
			}

			if ($('#MainContent_HF_alertType').val() == "droplet") {
				$('#MainContent_chkKewaspadaanDroplet').prop("checked", true)
			}

			if ($('#MainContent_HF_alertType').val() == "airborne") {
				$('#MainContent_chkKewaspadaanAirborne').prop("checked", true)
			}
		}

		function saveAlertDeleteReason() {
			event.preventDefault();
			$('#modalAlasanKewaspadaan').modal('hide');

			$('#MainContent_txtAlasanKewaspadaan').val("");
			CheckKewaspadaanS();
			CheckKewaspadaanK();
			CheckKewaspadaanD();
			CheckKewaspadaanA();
		}

		let alertSelected = []
		let diseaseSelected = []

		function showAlertSuggest(chk, Disease) {
			var hfMapping = $('#MainContent_HF_mapping_fa').val();
			var alertSuggest = document.getElementById("saranKewaspadaan");
			var chkDisease = document.getElementById(chk.id);
			var data = JSON.parse(hfMapping);



			if (chkDisease.checked) {
				diseaseSelected.push(Disease);
				var alertIDchk = data.filter(x => diseaseSelected.includes(x.infectious_disease_id));
				for (var i = 0; i < alertIDchk.length; i++) {
					alertSelected.push(alertIDchk[i].alert_type_name);
				}
			} else {
				var idx_disease = diseaseSelected.indexOf(Disease);
				diseaseSelected.splice(idx_disease, 1);
				alertSelected = [];
				var alertIDuchk = data.filter(x => diseaseSelected.includes(x.infectious_disease_id));
				for (var i = 0; i < alertIDuchk.length; i++) {
					alertSelected.push(alertIDuchk[i].alert_type_name);
				}


			}
			let alert = [...new Set(alertSelected)];


			alertSuggest.innerText = "";
			alertSuggest.innerText = alert.join(', ');

		}

		function notificationEmpty() {
			warningnotificationOption();
			toastr.warning('The field can not be Empty!', 'Warning');
		}

		function ValidateInfeksius() {
			var chk1 = document.getElementById("<%=chkScreen1.ClientID %>");
			var chk2 = document.getElementById("<%=chkScreen2.ClientID %>");
			var chk3 = document.getElementById("<%=chkScreen3.ClientID %>");
			var chk4 = document.getElementById("<%=chkScreen4.ClientID %>");
			var chk5 = document.getElementById("<%=chkScreen5.ClientID %>");
			var chk6 = document.getElementById("<%=chkScreen6.ClientID %>");
			var chk7 = document.getElementById("<%=chkScreen7.ClientID %>");
			var chk8 = document.getElementById("<%=chkScreen8.ClientID %>");
			var chk9 = document.getElementById("<%=chkScreen9.ClientID %>");
			var chk10 = document.getElementById("<%=chkScreen10.ClientID %>");
			var chk11 = document.getElementById("<%=chkScreen11.ClientID %>");
			var chk12 = document.getElementById("<%=chkScreen12.ClientID %>");
			var chk13 = document.getElementById("<%=chkScreen13.ClientID %>");
			var chk14 = document.getElementById("<%=chkScreen14.ClientID %>");
			var chk15 = document.getElementById("<%=chkScreen15.ClientID %>");
			var chk16 = document.getElementById("<%=chkScreen16.ClientID %>");
			var chk17 = document.getElementById("<%=chkScreen17.ClientID %>");
			var chk21 = document.getElementById("<%=chkScreen21.ClientID %>");

			var chkS = document.getElementById("<%=chkKewaspadaanStandard.ClientID%>");
			var chkK = document.getElementById("<%=chkKewaspadaanKontak.ClientID%>");
			var chkD = document.getElementById("<%=chkKewaspadaanDroplet.ClientID%>");
			var chkA = document.getElementById("<%=chkKewaspadaanAirborne.ClientID%>");

			var chkTindakan1 = document.getElementById("<%=chkTindakan1.ClientID%>");
			var chkTindakan2 = document.getElementById("<%=chkTindakan2.ClientID%>");
			var chkTindakan3 = document.getElementById("<%=chkTindakan3.ClientID%>");

			var chkscreen = chk1.checked || chk2.checked || chk3.checked || chk4.checked || chk5.checked || chk6.checked || chk7.checked || chk8.checked ||
				chk9.checked || chk10.checked || chk11.checked || chk12.checked || chk13.checked || chk14.checked || chk15.checked || chk16.checked ||
				chk17.checked || chk21.checked;
			var chkKewaspadaan = !chkS.checked && !chkK.checked && !chkD.checked && !chkA.checked;
			var chkTindakan = !chkTindakan1.checked && !chkTindakan2.checked && !chkTindakan3.checked;
			var chkIsiKewaspadaan = chkS.checked || chkK.checked || chkD.checked || chkA.checked;

			//Validasi Endemic
			var chkYesEndemic = document.getElementById("<%=rbkunjungan2.ClientID%>");
			var textEndemic = document.getElementById("<%=txtEndemic.ClientID%>");

			if (chkYesEndemic.checked) {
				if (textEndemic.value !== "" && chkKewaspadaan) {
					notificationMandatory("Wajib mengisi minimal 1 kewaspadaan...");
					return false;
				}
			}


			if (chkscreen && chkKewaspadaan && chkTindakan) {
				notificationMandatory("Wajib mengisi minimal 1 kewaspadaan & 1 tindakan...");
				return false
			}
			else if (chkscreen && chkKewaspadaan) {
				notificationMandatory("Wajib mengisi minimal 1 kewaspadaan...");
				return false
			} else if (chkIsiKewaspadaan && chkTindakan) {
				notificationMandatory("Wajib mengisi minimal 1 tindakan...");
				return false
			} else {
				return true
			}



		}

		function ShowModalEndemic() {
			//Tampilan Endemic dan Skrining
			var chk1 = document.getElementById("<%=chkScreen1.ClientID %>");
			var chk2 = document.getElementById("<%=chkScreen2.ClientID %>");
			var chk3 = document.getElementById("<%=chkScreen3.ClientID %>");
			var chk4 = document.getElementById("<%=chkScreen4.ClientID %>");
			var chk5 = document.getElementById("<%=chkScreen5.ClientID %>");
			var chk6 = document.getElementById("<%=chkScreen6.ClientID %>");
			var chk7 = document.getElementById("<%=chkScreen7.ClientID %>");
			var chk8 = document.getElementById("<%=chkScreen8.ClientID %>");
			var chk9 = document.getElementById("<%=chkScreen9.ClientID %>");
			var chk10 = document.getElementById("<%=chkScreen10.ClientID %>");
			var chk11 = document.getElementById("<%=chkScreen11.ClientID %>");
			var chk12 = document.getElementById("<%=chkScreen12.ClientID %>");
			var chk13 = document.getElementById("<%=chkScreen13.ClientID %>");
			var chk14 = document.getElementById("<%=chkScreen14.ClientID %>");
			var chk15 = document.getElementById("<%=chkScreen15.ClientID %>");
			var chk16 = document.getElementById("<%=chkScreen16.ClientID %>");
			var chk17 = document.getElementById("<%=chkScreen17.ClientID %>");
			var chk21 = document.getElementById("<%=chkScreen21.ClientID %>");

			var chkS = document.getElementById("<%=chkKewaspadaanStandard.ClientID%>");
			var chkK = document.getElementById("<%=chkKewaspadaanKontak.ClientID%>");
			var chkD = document.getElementById("<%=chkKewaspadaanDroplet.ClientID%>");
			var chkA = document.getElementById("<%=chkKewaspadaanAirborne.ClientID%>");

			var chkTindakan1 = document.getElementById("<%=chkTindakan1.ClientID%>");
			var chkTindakan2 = document.getElementById("<%=chkTindakan2.ClientID%>");
			var chkTindakan3 = document.getElementById("<%=chkTindakan3.ClientID%>");

			let rbYes = $("[id$='rbskriningyes']");
			let rbNo = $("[id$='rbskriningno']");

			let chkYesEndemic = document.getElementById("<%=rbkunjungan2.ClientID%>");
			let chkNoEndemic = document.getElementById("<%=rbkunjungan1.ClientID%>");
			let txtEndemic = document.getElementById("<%=txtEndemic.ClientID%>");
			if (txtEndemic.value !== "") {
				rbYes.click();
				chkYesEndemic.click();
			} else {
				chkNoEndemic.click();
			}
			if (chk1.checked || chk2.checked || chk3.checked || chk4.checked || chk5.checked || chk6.checked || chk7.checked || chk8.checked ||
				chk9.checked || chk10.checked || chk11.checked || chk12.checked || chk13.checked || chk14.checked || chk15.checked || chk16.checked ||
				chk17.checked || chk21.checked || chkS.checked || chkK.checked || chkD.checked || chkA.checked || chkTindakan1.checked
				|| chkTindakan2.checked || chkTindakan3.checked) {

				rbYes.click();
			} else {

				rbNo.click();
			}



			ShowTableInfo();
			return true;
		}

		function ShowTableInfo() {
			//Table Info Kewaspadaan
			let hfMapping = $('#MainContent_HF_mapping_fa').val();
			let data = JSON.parse(hfMapping);

			let alertInfo = "";
			let validateData = [];
			$.each(data, function (key, val) {

				if (validateData.some(v => v.infectious_disease_id == val.infectious_disease_id)) {
					return true;
				} else {
					validateData.push(val);
					alertInfo += `<tr style="margin-bottom:50px;">
							<td scope="col" style="border-width: 0px; width: 40%; vertical-align: top; padding-top: 7px">${val.infectious_disease_name}</td>
							<td scope="col" id="td-info-${val.infectious_disease_id}" style="border-width: 0px; width: 60%; vertical-align: top;padding-top: 7px"></td>
						  </tr>`;
				}
			});

			let lastValidateData = validateData[validateData.length - 1];
			$("#table-kewaspadaan").html(alertInfo);
			for (let i = 0; i <= lastValidateData.infectious_disease_id; i++) {
				let tdInfoId = document.getElementById("td-info-" + i);

				if (tdInfoId != null) {
					let infoKewaspadaan = [];
					$.each(data, function (key, val) {
						if (i == val.infectious_disease_id) {
							infoKewaspadaan.push(val.mapping_remarks);
						}
					});
					tdInfoId.innerText = infoKewaspadaan.join(", ");
				}


			}

			return true
		}

		function ShowSkrining() {
			var rbYes = document.getElementById("<%=rbskriningyes.ClientID %>");
			var dv = document.getElementById("dvSkriningPenyakit");
			if (rbYes.checked) {
				//dv.style.display = "";
				dv.classList.remove("disabled-form-skriningPenyakit");
			}
		}


		function HideSkrining() {


			var chk1 = document.getElementById("<%=chkScreen1.ClientID %>");
			var chk2 = document.getElementById("<%=chkScreen2.ClientID %>");
			var chk3 = document.getElementById("<%=chkScreen3.ClientID %>");
			var chk4 = document.getElementById("<%=chkScreen4.ClientID %>");
			var chk5 = document.getElementById("<%=chkScreen5.ClientID %>");
			var chk6 = document.getElementById("<%=chkScreen6.ClientID %>");
			var chk7 = document.getElementById("<%=chkScreen7.ClientID %>");
			var chk8 = document.getElementById("<%=chkScreen8.ClientID %>");
			var chk9 = document.getElementById("<%=chkScreen9.ClientID %>");
			var chk10 = document.getElementById("<%=chkScreen10.ClientID %>");
			var chk11 = document.getElementById("<%=chkScreen11.ClientID %>");
			var chk12 = document.getElementById("<%=chkScreen12.ClientID %>");
			var chk13 = document.getElementById("<%=chkScreen13.ClientID %>");
			var chk14 = document.getElementById("<%=chkScreen14.ClientID %>");
			var chk15 = document.getElementById("<%=chkScreen15.ClientID %>");
			var chk16 = document.getElementById("<%=chkScreen16.ClientID %>");
			var chk17 = document.getElementById("<%=chkScreen17.ClientID %>");
			var chk21 = document.getElementById("<%=chkScreen21.ClientID %>");
			if (chk1.checked || chk2.checked || chk3.checked || chk4.checked || chk5.checked || chk6.checked || chk7.checked || chk8.checked ||
				chk9.checked || chk10.checked || chk11.checked || chk12.checked || chk13.checked || chk14.checked || chk15.checked || chk16.checked ||
				chk17.checked || chk21.checked) {
				var rbYes = $("[id$='rbskriningyes']");
				var dv = document.getElementById('dvSkriningPenyakit');
				if (confirm("There's data in the form, are you sure you want to change to 'No'?")) {
					//dv.style.display = "none";
					dv.classList.add("disabled-form-skriningPenyakit");
					chk1.checked = false;
					chk2.checked = false;
					chk3.checked = false;
					chk4.checked = false;
					chk5.checked = false;
					chk6.checked = false;
					chk7.checked = false;
					chk8.checked = false;
					chk9.checked = false;
					chk10.checked = false;
					chk11.checked = false;
					chk12.checked = false;
					chk13.checked = false;
					chk14.checked = false;
					chk15.checked = false;
					chk16.checked = false;
					chk17.checked = false;
					chk21.checked = false;
				}
				else
					rbYes.click();
			}
			else {
				var dv = document.getElementById('dvSkriningPenyakit');
				//dv.style.display = "none";
				dv.classList.add("disabled-form-skriningPenyakit");
			}
		}

		function warningnotificationOption() {
			toastr.options.positionClass = "toast-top-right";
			toastr.options.closeButton = true;
			toastr.options.timeOut = 0;
			toastr.options.extendedTimeOut = 0;
			toastr.options.tapToDismiss = true;
		}

		function notificationMandatory(msg) {
			warningnotificationOption();
			toastr.warning(msg + ' <br /> <button type="button" class="btn btn-danger btn-sm" style="height: 25px; padding-top: 3px; width: 55px; float:right;">OK</button>', 'Save Alert!');
		}

		window.addEventListener('message', messageHandler, false);

	</script>
</asp:Content>
