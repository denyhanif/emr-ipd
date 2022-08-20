﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="PatientDashboard.aspx.cs" Inherits="Form_General_PatientDashboard" %>

<%@ Register Src="~/Form/General/Control/PatientCard.ascx" TagPrefix="uc1" TagName="PatientCard" %>
<%@ Register Src="~/Form/General/Control/PatientCardModal.ascx" TagPrefix="uc1" TagName="PatientCardModal" %>
<%@ Register Src="~/Form/General/Control/StdLabResult.ascx" TagPrefix="tag1" TagName="StdLabResult" %>
<%@ Register Src="~/Form/SOAP/PreviewTemplate/SoapPagePreview.ascx" TagPrefix="uc1" TagName="Preview" %>
<%@ Register Src="~/Form/General/Control/StdRadResult.ascx" TagPrefix="tag1" TagName="StdRadResult" %>
<%--<%@ Register Src="~/Form/General/Control/StdPatientHistory.ascx" TagPrefix="tag1" TagName="StdPatientHistory" %>--%>
<%@ Register Src="~/Form/SOAP/Control_Template/Modal/ModalReferalList.ascx" TagPrefix="uc1" TagName="ModalReferalList" %>
<%@ Register Src="~/Form/SOAP/Control_Template/Modal/ModalReferalListBalasan.ascx" TagPrefix="uc1" TagName="ModalReferalListBalasan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body, html {
            width: 100%;
            height: 100%;
        }

        .row-eq-height {
            display: -webkit-box;
            display: -webkit-flex;
            display: -ms-flexbox;
            display: flex;
        }

        .breakword {
            word-break: break-word;
        }
    </style>

    <body>
        <asp:HiddenField ID="HFisBahasa" runat="server" />

        <div style="background-color: #CDD2DD;">
            <div class="container-fluid kartu-pasien">
                <asp:HiddenField ID="hfPatientId" runat="server" />
                <asp:HiddenField ID="hfEncounterId" runat="server" />
                <asp:HiddenField ID="hfAdmissionId" runat="server" />
                <asp:HiddenField ID="hfPageSoapId" runat="server" />
                <asp:HiddenField ID="hfPageSoapIdHeader" runat="server" />
                <uc1:PatientCard runat="server" ID="PatientCard" />
            </div>

            <div class="container-fluid" style="padding-top: 10px">

                <!--==================================================== Kewaspadaan ========================================================-->
				<div id="divKewaspadaan" runat="server" class="row" style="padding-left:8px; padding-right:8px; display:none">
					<div class="divKewaspadaan" runat="server" style="width:100%; margin-bottom: 15px; padding-left: 7px; padding-right: 7px;">
						<div class="box-small-empty" style="background-color: #FEEEC1">
                            <div style="padding:15px 10px 14px">
                                <asp:Label ID="lblkewaspadaan" runat="server" CssClass="" Text="" Font-Size="20px" ForeColor="#8F701C"> 
                                   Kewaspadaan: <label id="listKewaspadaan" runat="server" style="font:normal normal bold 20px/24px Helvetica;">-</label>
								</asp:Label>
                            </div>
						</div>
					</div>
				</div>
                <!-- ######################################################--- box empty ---###################################################### -->
                <div class="row" style="padding-left: 8px; padding-right: 8px;">
                    <div id="divkosong_reminder" runat="server" class="col-lg-3" style="margin-bottom: 15px; padding-left: 7px; padding-right: 7px;">
                        <div class="box-small-empty">
                            <div id="header-reminders-hide" class="padding-title-empty">
                                <asp:Image ID="Image4" ImageUrl="~/Images/Dashboard/ic_Reminder.svg" CssClass="title-img-box" runat="server" />
                                <label id="lblbhs_reminder_hide" class="font-header-dashboard title-text-box" style="color: #C43D32;">Reminder</label>
                            </div>
                            <div class="padding-content-empty">
                                <asp:Label ID="lblemptyreminder_new" runat="server" CssClass="content-text-box-empty" Text=" "> 
                                    <i class="fa fa-ban"></i> <label id="lblbhs_noreminder_hide">No reminder</label>
                                </asp:Label>
                            </div>
                        </div>
                    </div>

                    <div id="divkosong_allergy" runat="server" class="col-lg-3" style="margin-bottom: 15px; padding-left: 7px; padding-right: 7px;">
                        <div class="box-small-empty">
                            <div id="header-allergies-hide" class="padding-title-empty">
                                <asp:Image ID="Image3" ImageUrl="~/Images/Dashboard/ic_Allergies.svg" CssClass="title-img-box" runat="server" />
                                <label id="lblbhs_allergies_hide" class="font-header-dashboard title-text-box" style="color: #C43D32;">Allergies</label>
                            </div>
                            <div class="padding-content-empty">
                                <asp:Label ID="Lblemptyallergy_new" runat="server" CssClass="content-text-box-empty" Text=" "> 
                                    <i class="fa fa-ban"></i> <label id="lblbhs_noallergies_hide">No allergies</label>
                                </asp:Label>
                            </div>
                        </div>
                    </div>

                    <div id="divkosong_routinemed" runat="server" class="col-lg-3" style="margin-bottom: 15px; padding-left: 7px; padding-right: 7px;">
                        <div class="box-small-empty">
                            <div id="header-routinemed-hide" class="padding-title-empty">
                                <asp:Image ID="Image5" ImageUrl="~/Images/Dashboard/ic_Routine_new.svg" CssClass="title-img-box" runat="server" />
                                <label id="lbhbhs_routinemedication_hide" class="font-header-dashboard title-text-box" style="color: #0013b5;">Routine Medication</label>
                            </div>
                            <div class="padding-content-empty">
                                <asp:Label ID="lblemptyroutinemed_new" runat="server" CssClass="content-text-box-empty" Text=" "> 
                                    <i class="fa fa-ban"></i> <label id="lblbhs_noroutinemedication_hide">No routine medication</label>
                                </asp:Label>
                            </div>
                        </div>
                    </div>

                    <div id="divkosong_procresult" runat="server" class="col-lg-3" style="margin-bottom: 15px; padding-left: 7px; padding-right: 7px;">
                        <div class="box-small-empty">
                            <div id="header-procresult-hide" class="padding-title-empty">
                                <asp:Image ID="Image6" ImageUrl="~/Images/Dashboard/ic_LatestHistory_new.svg" CssClass="title-img-box" runat="server" />
                                <label id="lblbhs_riwayattindakan_new" class="font-header-dashboard title-text-box" style="color: #219000;">Procedure Result</label>
                            </div>
                            <div class="padding-content-empty">
                                <asp:Label ID="lblemptyprocresult_new" runat="server" CssClass="content-text-box-empty" Text=" "> 
                                    <i class="fa fa-ban"></i> <label id="lblbhs_notindakan_hide">No procedure result</label>
                                </asp:Label>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- ######################################################--- end box empty ---###################################################### -->

                <!-- ######################################################--- box isi data ---###################################################### -->
                <div class="row">
                    <div id="divisi_reminder" runat="server" class="col-sm-12 box-margin-btm">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="box-big-filled">
                                    <div id="header-reminders" class="header-title-filled">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <asp:Image ID="Image1" ImageUrl="~/Images/Dashboard/ic_Reminder.svg" CssClass="title-img-box" runat="server" />
                                                <label id="lblbhs_reminder" class="font-header-dashboard title-text-box" style="color: #C43D32;">Reminder</label>
                                            </div>
                                            <div class="col-sm-9" style="text-align: right; padding-top: 5px; padding-right: 0px;">
                                                <asp:HiddenField runat="server" ID="hfjsonreminder" />
                                                <div class="pretty p-icon p-curve">
                                                    <asp:CheckBox runat="server" ID="chkreminder" OnCheckedChanged="btnFilterReminder_Click" AutoPostBack="true" />
                                                    <div class="state p-success">
                                                        <i class="icon fa fa-check font-content-dashboard"></i>
                                                        <label id="lblbhs_hideotherdoctor" style="font-size: 12px;" class="font-content-dashboard">Hide Others Doctor's Reminder </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12" style="padding-left: 15px;">
                                            <div class="box-padding-btm">
                                                <asp:Label ID="lblemptyreminder" runat="server" CssClass="content-text-box-empty" Text=""> <i class="fa fa-ban"></i>
                                                    <label id="lblbhs_noreminder"> No Reminder </label>
                                                </asp:Label>
                                                <asp:GridView ID="gvw_reminder" runat="server" BorderWidth="0" BorderColor="#b9b9b9"
                                                    AutoGenerateColumns="False" CssClass="table-condensed table-fill-width"
                                                    HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;No" ItemStyle-Width="4%" HeaderStyle-Font-Size="11px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="tindakan" runat="server" Style="padding-left: 10px;"> <%# Container.DataItemIndex + 1 %> </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Tgl. Dibuat" ItemStyle-Width="12%" HeaderStyle-Font-Size="11px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hfdoctorid" Value='<%# Bind("doctor_id") %>' />
                                                                <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="tgl_dibuat" runat="server" Text='<%# Bind("created_date") %>' Style="padding-left: 10px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Reminder" ItemStyle-Width="60%" HeaderStyle-Font-Size="11px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="reminder" runat="server" Text='<%# Bind("notification") %>' Style="padding-left: 10px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Doctor" ItemStyle-Width="24%" HeaderStyle-Font-Size="11px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="doctor" runat="server" Text='<%# Bind("doctor_name") %>' Style="padding-left: 10px;"></asp:Label>
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

                    <div id="divisi_allergy" runat="server" class="col-sm-12 box-margin-btm">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="box-big-filled">
                                    <div id="header-allergies" class="header-title-filled">
                                        <asp:Image ID="Image8" ImageUrl="~/Images/Dashboard/ic_Allergies.svg" CssClass="title-img-box" runat="server" />
                                        <label id="lblbhs_allergies" class="font-header-dashboard title-text-box" style="color: #C43D32;">Allergies</label>
                                    </div>
                                    <div class="row">
                                        <div style="padding-top: 5px; padding-left: 25px; padding-right: 0px; display: inline-table; min-width: 20%; max-width: 33%;">
                                            <label id="lblbhs_drugs" class="font-subheader-dashboard sub-header-label" style="font-weight: bold; font-size: 14px;">Drugs</label>
                                            <br />
                                            <div class="box-padding-btm">
                                                <asp:Label ID="Lblemptyaledrug" runat="server" CssClass="content-text-box-empty" Text=" "> <i class="fa fa-ban"></i> 
                                            <label id="lblbhs_noallergiesdrugs">No allergies</label>
                                                </asp:Label>
                                                <asp:Repeater runat="server" ID="DrugAllergy">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" runat="server" class="font-content-dashboard" Text='<%#Eval("allergy") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                        <div style="padding-top: 5px; padding-left: 10px; display: inline-table; min-width: 20%; max-width: 33%;">
                                            <label id="lblbhs_food" class="font-subheader-dashboard sub-header-label" style="font-weight: bold; font-size: 14px;">Food</label>
                                            <br />
                                            <div class="box-padding-btm">
                                                <asp:Label ID="Lblemptyalefood" runat="server" CssClass="content-text-box-empty" Text=""> <i class="fa fa-ban"></i> 
                                            <label id="lblbhs_noallergiesfood">No allergies</label>
                                                </asp:Label>
                                                <asp:Repeater runat="server" ID="FoodAllergy">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" class="font-content-dashboard" runat="server" Text='<%#Eval("allergy") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                        <div style="padding-top: 5px; padding-left: 10px; display: inline-table; min-width: 20%; max-width: 33%;">
                                            <label id="lblbhs_lain2" class="font-subheader-dashboard sub-header-label" style="font-weight: bold; font-size: 14px;">Others</label>
                                            <br />
                                            <div class="box-padding-btm">
                                                <asp:Label ID="Lblemptyalelain" runat="server" CssClass="content-text-box-empty" Text=""> <i class="fa fa-ban"></i> 
                                            <label id="lblbhs_noallergieslain">No allergies</label>
                                                </asp:Label>
                                                <asp:Repeater runat="server" ID="OtherAllergy">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" class="font-content-dashboard" runat="server" Text='<%#Eval("allergy") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div id="divisi_routinemed" runat="server" class="col-sm-12 box-margin-btm">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="box-big-filled">
                                    <div id="header-routinemed" class="header-title-filled">
                                        <asp:Image ID="Imageroutinemed" ImageUrl="~/Images/Dashboard/ic_Routine_new.svg" CssClass="title-img-box" runat="server" />
                                        <label id="lbhbhs_routinemedication" class="font-header-dashboard title-text-box" style="color: #0013b5;">Routine Medication</label>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12" style="padding-top: 5px; padding-left: 25px;">
                                            <div class="box-padding-btm">
                                                <asp:Label ID="lblemptyroutinemed" runat="server" CssClass="content-text-box-empty" Text="">  <i class="fa fa-ban"></i>  
                                                    <label id="lblbhs_noroutinemedication">No routine medication</label>
                                                </asp:Label>

                                                <div class="row">
                                                    <asp:Repeater runat="server" ID="RepCurrentMedication">
                                                        <ItemTemplate>
                                                            <div class="col-sm-4">
                                                                <i class="mdi mdi-circle-medium"></i>
                                                                <asp:Label ID="NameLabel" class="font-content-dashboard" runat="server" Text='<%#Eval("current_medication") %>' Enabled="false" />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div id="divisi_procresult" runat="server" class="col-sm-12 box-margin-btm">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="box-big-filled">
                                    <div id="header-procresult" class="header-title-filled">
                                        <asp:Image ID="Image9" ImageUrl="~/Images/Dashboard/ic_LatestHistory_new.svg" CssClass="title-img-box" runat="server" />
                                        <label id="lblbhs_riwayattindakan" class="font-header-dashboard title-text-box" style="color: #219000;">Procedure Result</label>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="box-padding-btm">
                                                <div id="divnotindakan" runat="server" class="content-text-box-empty" style="text-align: left; padding-top: 5px; padding-left: 10px; display: none;">
                                                    <i class="fa fa-ban"></i>
                                                    <label id="lblbhs_notindakan">No Procedure Result </label>
                                                </div>
                                                <div id="divriwayattindakan" runat="server" style="font-size: 14px; text-align: left; padding-top: 0px;">
                                                    <asp:GridView runat="server" ID="gvw_hasiltindakan" AutoGenerateColumns="False" CssClass="table-condensed table-fill-width"
                                                        BorderWidth="0" BorderColor="#b9b9b9">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="&nbsp;Date / Admission" ItemStyle-Width="12%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                                <ItemTemplate>
                                                                    <div style="padding-left: 5px;">
                                                                        <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="item_name" runat="server" Text='<%# Bind("admission") %>'></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Doctor" ItemStyle-Width="12%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                                <ItemTemplate>
                                                                    <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="item_name" runat="server" Text='<%# Bind("doctor_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Procedure Result" ItemStyle-Width="76%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                                <ItemTemplate>
                                                                    <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="item_name" runat="server" Text='<%# Eval("planning_remarks").ToString().Replace("\n", "<br />") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div id="divisi_latestsoap" runat="server" class="col-sm-12 box-margin-btm">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="box-big-filled">
                                    <div id="header-lastsoap" class="header-title-filled">
                                        <asp:Image ID="Image10" ImageUrl="~/Images/Dashboard/ic_LatestSOAP_new.svg" CssClass="title-img-box" runat="server" />
                                        <label id="lblbhs_SOAP" class="font-header-dashboard title-text-box" style="color: #b716a4;">5 Latest SOAP</label>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 box-padding-btm">
                                            <div id="divNoSoap" runat="server" class="content-text-box-empty" style="text-align: center; padding-top: 5px; display: none;">
                                                <i class="fa fa-ban"></i>
                                                <label id="lblbhs_no3soap">No Latest SOAP </label>
                                            </div>
                                            <div id="SOAP_patientHistory" runat="server" style="display: none;"></div>

                                            <asp:GridView runat="server" ID="gvw_latestsoap" AutoGenerateColumns="False" CssClass="table-condensed table-fill-width"
                                                BorderWidth="0" BorderColor="#b9b9b9">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="&nbsp;Date" ItemStyle-Width="11%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                        <ItemTemplate>
                                                            <div style="padding-left: 5px;">
                                                                <div style="font-size: 16px;">
                                                                    <b>
                                                                        <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="date_soap" runat="server" Text='<%# Bind("AdmissionDate") %>'></asp:Label></b>
                                                                </div>
                                                                <div class="font-content-dashboard">
                                                                    <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label3" runat="server" Text='<%# Bind("AdmissionNo") %>'></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <a href='<%# Eval("IsLab").ToString().ToLower() == "1" ? String.Format("javascript:Lab({0})", Eval("AdmissionId")) : "javascript:NothingToDo();" %>' style='cursor: <%# Eval("IsLab").ToString().ToLower() == "1" ? "pointer" : "not-allowed" %>; margin-right: 5px;' title="Laboratory Result">
                                                                        <asp:Image ID="ImgLab" ImageUrl='<%# Eval("IsLab").ToString().ToLower() == "1" ? "~/Images/Icon/ic_Lab.svg" : "~/Images/Icon/ic_Lab_NotActive.svg" %>' runat="server" />
                                                                    </a>
                                                                    <a href='<%# Eval("IsRad").ToString().ToLower() == "1" ? String.Format("javascript:Rad({0})", Eval("AdmissionId")) : "javascript:NothingToDo();" %>' style='cursor: <%# Eval("IsRad").ToString().ToLower() == "1" ? "pointer" : "not-allowed" %>; margin-right: 5px;' title="Radiology Result">
                                                                        <asp:Image ID="ImgRad" ImageUrl='<%# Eval("IsRad").ToString().ToLower() == "1" ? "~/Images/Icon/ic_Rad.svg" : "~/Images/Icon/ic_Rad_NotActive.svg" %>' runat="server" />
                                                                    </a>
                                                                    <%--<a target="_blank" title="" href="javascript:Lab('<%# Eval("AdmissionId") %>')"  style="color: blue; margin-right:5px; text-decoration:underline; "><span><img src="../../Images/Icon/ic_Lab.svg" /></span></a> 
                                                                    <a target="_blank" title="" href="javascript:Rad('<%# Eval("AdmissionId") %>')"  style="color: blue; margin-right:5px; text-decoration:underline; "><span><img src="../../Images/Icon/ic_Rad.svg" /></span></a> --%>
                                                                    <a title="Patient History" href="javascript:patientHistory('<%# Eval("PatientId") %>','<%# Eval("OrganizationId") %>','<%# Eval("AdmissionId") %>','<%# Eval("EncounterId") %>','<%# Eval("pageSOAP") %>')" style="color: blue; margin-right: 5px; text-decoration: underline;"><span>
                                                                        <img src="../../Images/Icon/ic_History.svg" /></span></a>
                                                                </div>
                                                                <br />
                                                                <div style="padding-top: 10px;">
                                                                    <label class="font-content-dashboard" style="font-weight: bold;">Created Date</label><br />
                                                                    <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="LabelCreateddate" runat="server" Text='<%# Bind("CreatedDate") %>'></asp:Label>
                                                                </div>
                                                                <div style="padding-top: 10px;">
                                                                    <label class="font-content-dashboard" style="font-weight: bold;">Modified Date</label><br />
                                                                    <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="LabelModifieddate" runat="server" Text='<%# Bind("ModifiedDate") %>'></asp:Label>
                                                                </div>
                                                                <br />
                                                                <a href="javascript:RevModal('<%# Eval("OrganizationId") %>','<%# Eval("PatientId") %>','<%# Eval("AdmissionId") %>','<%# Eval("EncounterId") %>')" style="font-weight: bold; font-size: 14px; text-decoration: underline;">Revision</a>

                                                                <br />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doctor" ItemStyle-Width="11%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="date_soap" runat="server" Text='<%# Bind("DoctorName") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="LabelIsTele" runat="server" Text="Teleconsultation" Style="background-color: #cddef4; color: #1172f7; padding: 2px 5px 2px 5px; border-radius: 5px 5px; font-size: 14px;" Visible='<%# Eval("IsTeleconsultation") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S" ItemStyle-Width="13%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label" ItemStyle-CssClass="breakword">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="date_soap" runat="server" Text=""> <%# Eval("Subjective").ToString().Replace("\\n","<br />") %> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="O" ItemStyle-Width="13%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label" ItemStyle-CssClass="breakword">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="date_soap" runat="server" Text=""> <%# Eval("Objective").ToString().Replace("\n","<br />").Replace("\\n","<br />") %> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="A" ItemStyle-Width="13%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label" ItemStyle-CssClass="breakword">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="date_soap" runat="server" Text=""> <%# Eval("Diagnosis").ToString().Replace("\n","<br />").Replace("\\n","<br />") %> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="P" ItemStyle-Width="13%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label" ItemStyle-CssClass="breakword">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="date_soap" runat="server" Text=""> <%# Eval("PlanningProcedure").ToString().Replace("\n","<br />").Replace("\\n","<br />") %> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prescription" ItemStyle-Width="26%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                        <ItemTemplate>
                                                            <div style='padding-bottom: 5px; display: <%# Eval("IsEditPrescription").ToString().ToLower() == "true" ? "block" : "none" %>'>
                                                                <asp:Label ID="LabelEditedByFarmasi" runat="server" Text="Edited by Pharmacy" Style="background-color: #cddef4; color: #1172f7; padding: 2px 5px 2px 5px; border-radius: 5px 5px; font-size: 14px;"> </asp:Label>
                                                            </div>
                                                            <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="date_soap" runat="server" Text=""> <%# Eval("Prescription").ToString().Replace("\\n","<br />") %> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div id="divkosong_latestsoap" runat="server" class="col-sm-12" style="margin-bottom: 15px;">
                        <div class="box-small-empty">
                            <div id="header-lastsoap-hide" class="padding-title-empty">
                                <asp:Image ID="Image7" ImageUrl="~/Images/Dashboard/ic_LatestSOAP_new.svg" CssClass="title-img-box" runat="server" />
                                <label id="lblbhs_SOAP_hide" class="font-header-dashboard title-text-box" style="color: #b716a4;">5 Latest SOAP</label>
                            </div>
                            <div class="padding-content-empty">
                                <asp:Label ID="lblemptysoap_new" runat="server" CssClass="content-text-box-empty" Text=" "> 
                                    <i class="fa fa-ban"></i> <label id="lblbhs_no5soap_hide">No latest SOAP </label>
                                </asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 box-margin-btm">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="box-big-filled">
                                    <div id="header-admihis" class="header-title-filled">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <asp:Image ID="Image11" ImageUrl="~/Images/Dashboard/ic_RiwayatKonsultasi.svg" CssClass="title-img-box" runat="server" />
                                                <label id="lblbhs_admissionhistory" class="font-header-dashboard" style="font-weight: bold; font-size: 20px; color: #e67d05;">Admission History</label>
                                            </div>
                                            <div class="col-sm-4 text-center">
                                                <div class="btn-group" role="group" aria-label="..." style="height: 35px">
                                                    <asp:LinkButton runat="server" Style="margin-top: 3px; margin-right: 15px; padding: 0px; height: 30px; box-shadow: none;" CssClass="btn" onmouseover="this.style.color='#e67d05';" onmouseout="this.style.color='#444444';" Font-Size="25px" ID="btnPrev" OnClick="btnPrev_Click"> <i class="icon-ic_PreviousCalendar"></i> </asp:LinkButton>
                                                    <asp:Label runat="server" CssClass="btn btn-default" Style="background-color: transparent; outline-color: transparent; border: 0px; box-shadow: none; cursor: default;" Font-Size="20px" Font-Bold="true" ID="lblYear"></asp:Label>
                                                    <asp:LinkButton runat="server" Style="margin-top: 3px; margin-left: 15px; padding: 0px; height: 30px; box-shadow: none;" CssClass="btn" onmouseover="this.style.color='#e67d05';" onmouseout="this.style.color='#444444';" Font-Size="25px" ID="btnNext" OnClick="btnNext_Click"> <i class="icon-ic_NextCalendar"></i> </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div style="text-align: center;" class="box-padding-btm">
                                                <div id="divContentReport" runat="server"></div>
                                                <asp:Label ID="Labeladmishis" runat="server" Style="padding: 10px;" CssClass="content-text-box-empty" Text=""> <br /> <i class="fa fa-ban"></i>
                                                <label id="lblbhs_noadmissionhistory"> No admission history this year  </label>
                                                <br />
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <!-- ######################################################--- end box isi data ---###################################################### -->

                <a class="item" href="javascript:topFunction();">
                    <div id="myIDtoTop" class="bottomMenuu hidee">
                        <span>
                            <img src="../../Images/Result/ic_Arrow_Top.png" title="go to top" /></span>
                    </div>
                </a>

                <!-- =============================================================================================================================================================== -->
                <div class="row" runat="server" id="divdisable" visible="false">
                    <div class="col-lg-4">
                    </div>
                    <div class="col-lg-8" style="padding-left: 0px;">

                        <div style="background-color: white; border-radius: 0px 5px 5px 0px; box-shadow: 0px 1px 3px #9293A0; display: none">
                            <div style="height: 130px;">
                                <div id="header-drugpres" style="border-top: 5px solid #0290A2; border-bottom: 1px solid #ededed; border-radius: 0px 5px 0px 0px; padding: 5px 10px 5px 10px;">
                                    <asp:Image ID="Imagedrugpres" ImageUrl="~/Images/Dashboard/ic_History.svg" Style="width: 27px; height: 27px; vertical-align: top; margin-right: 2px;" runat="server" />
                                    <label id="lblbhs_lastdrugprescription" style="font-weight: bold; font-size: 20px; color: #0290A2;">Last Drug Prescription</label>
                                    <asp:Label runat="server" Style="font-weight: bold; font-size: 16px; color: #0290A2; float: right;" ID="admissionDate" Text="" />
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="padding-top: 5px; padding-left: 25px;">
                                        <div style="padding-bottom: 5px; overflow-y: auto; max-height: 75px;" class="scrollEMR">
                                            <asp:Label ID="lblemptydrugpres" runat="server" Style="font-size: 14px; color: #CDCED9;" Text=""> <i class="fa fa-ban"></i>
                                            <label id="lblbhs_nodrugprescription"> No drug prescription </label>
                                            </asp:Label>
                                            <asp:Label ID="lbldoctordrugpres" runat="server" Style="font-size: 12px; color: #a8a9b1;" Text="doctor name"> </asp:Label>
                                            <asp:Repeater runat="server" ID="LastMedication">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("item_name") %>' Font-Size="12px" Font-Names="Helvetica" Enabled="false" />
                                                        <asp:Label ID="Label1" ForeColor="Gray" runat="server" Text=" (Routine)" Visible='<%#Eval("is_routine") %>' Font-Size="10px" Font-Names="Helvetica" Enabled="false" />
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div style="height: 130px;">
                                <div id="header-medihis" style="border-top: 5px solid #4D9B35; border-bottom: 1px solid #ededed; padding: 5px 10px 5px 10px;">
                                    <asp:Image ID="Imagemedihis" ImageUrl="~/Images/Dashboard/ic_LatestHistory.svg" Style="width: 27px; height: 27px; vertical-align: top; margin-right: 2px;" runat="server" />
                                    <label id="lblbhs_latestmedicalhistory" style="font-weight: bold; font-size: 20px; color: #4D9B35;">Latest Medical History</label>
                                    <asp:Label runat="server" Style="font-weight: bold; font-size: 16px; color: #4D9B35; float: right;" ID="lblMedHistoryDate" Text="" />
                                </div>
                                <div class="row">
                                    <div class="col-sm-6" style="padding-top: 5px; padding-left: 25px; padding-right: 0px;">
                                        <asp:Label ID="Lbldoctormedihis" runat="server" Style="font-size: 12px; color: #a8a9b1;" Text="doctor name"> </asp:Label><br />
                                        <label id="lblbhs_diagnosis" style="font-weight: bold; font-size: 14px;">Diagnosis</label><br />
                                        <div style="padding-bottom: 5px; overflow-y: auto; max-height: 42px;" class="scrollEMR">
                                            <asp:Label ID="lblemptymedihis1" runat="server" Style="font-size: 14px; color: #CDCED9;" Text=""> <i class="fa fa-ban"></i>
                                            <label id="lblbhs_nomedicalhistory">  No medical history </label>
                                            </asp:Label>
                                            <asp:Label runat="server" ID="lblPrimary" Width="100%" Height="100%" />
                                        </div>
                                    </div>
                                    <div class="col-sm-1" style="border-left: 1px solid #ededed; width: 1%; height: 85px; padding-left: 0px; padding-right: 0px;"></div>
                                    <div class="col-sm-6" style="width: 49%; padding-top: 5px; padding-left: 10px;">
                                        <br />
                                        <label id="lblbhs_planningandprocedure" style="font-weight: bold; font-size: 14px;">Planning and Procedure</label><br />
                                        <div style="padding-bottom: 5px; overflow-y: auto; max-height: 42px;" class="scrollEMR">
                                            <asp:Label ID="lblemptymedihis2" runat="server" Style="font-size: 14px; color: #CDCED9;" Text=""> <i class="fa fa-ban"></i>  
                                            <label id="lblbhs_noprocedure"> No procedure </label>
                                            </asp:Label>
                                            <asp:Label runat="server" ID="lblProcedure" Width="100%" Height="100%" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div style="background-color: white; border-radius: 0px 5px 5px 0px; box-shadow: 0px 1px 3px #9293A0;">
                            <div style="min-height: 275px;">
                                <div id="header-riwayat" style="border-top: 5px solid #4D9B35; border-bottom: 1px solid #ededed; padding: 5px 10px 5px 10px;">
                                    <asp:Image ID="Image2" ImageUrl="~/Images/Dashboard/ic_LatestHistory.svg" Style="width: 27px; height: 27px; vertical-align: top; margin-right: 2px;" runat="server" />
                                    <label id="lblbhs_healthhistory" class="font-header-dashboard" style="font-weight: bold; font-size: 20px; color: #4D9B35;">Health History</label>
                                    <asp:Label runat="server" Style="font-weight: bold; font-size: 16px; color: #4D9B35; float: right;" ID="Label5" Text="" />
                                </div>
                                <div class="row" style="min-height: 160px;">
                                    <div class="col-sm-6" style="padding-top: 5px; padding-left: 25px; padding-right: 0px;">
                                        <label id="lblbhs_outsideprocedure" class="font-subheader-dashboard" style="font-weight: bold; font-size: 14px;">Tindakan di Luar Pertemuan</label>
                                        <br />
                                        <div style="padding-bottom: 5px; padding-right: 10px; overflow-y: auto; max-height: 125px;" class="scrollEMR">
                                            <asp:Label ID="lblemptyoutside" runat="server" Style="font-size: 14px; color: #CDCED9;" Text=" "> <i class="fa fa-ban"></i> 
                                            <label id="lblbhs_nooutsideprocedure">No Outside Procedure</label>
                                            </asp:Label>
                                            <asp:GridView ID="gvw_outsideprocedure" runat="server"
                                                AutoGenerateColumns="False" CssClass="table table-bordered table-condensed"
                                                HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;No" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px" HeaderStyle-ForeColor="#767dba" HeaderStyle-CssClass="font-content-dashboard">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="tindakan" runat="server" Style="padding-left: 10px;"> <%# Container.DataItemIndex + 1 %> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;tindakan" ItemStyle-Width="65%" HeaderStyle-Font-Size="11px" HeaderStyle-ForeColor="#767dba" HeaderStyle-CssClass="font-content-dashboard">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="tindakan" runat="server" Text='<%# Bind("procedure_remarks") %>' Style="padding-left: 10px;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Tanggal" ItemStyle-Width="30%" HeaderStyle-Font-Size="11px" HeaderStyle-ForeColor="#767dba" HeaderStyle-CssClass="font-content-dashboard">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="tanggal" runat="server" Text='<%# Bind("procedure_date","{0:MMMM yyyy}") %>' Style="padding-left: 10px;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-sm-1" style="border-left: 1px solid #ededed; width: 1%; min-height: 160px; padding-left: 0px; padding-right: 0px;"></div>
                                    <div class="col-sm-6" style="width: 49%; padding-top: 5px; padding-left: 5px;">
                                        <label id="lblbhs_operasi" class="font-subheader-dashboard" style="font-weight: bold; font-size: 14px;">Riwayat Operasi</label>
                                        <br />
                                        <div style="padding-bottom: 5px; padding-right: 10px; overflow-y: auto; max-height: 125px;" class="scrollEMR">
                                            <asp:Label ID="lblemptysurgery" runat="server" Style="font-size: 14px; color: #CDCED9;" Text=""> <i class="fa fa-ban"></i> 
                                            <label id="lblbhs_nosurgery">No Surgery History</label>
                                            </asp:Label>

                                            <asp:GridView ID="gvw_surgery" runat="server"
                                                AutoGenerateColumns="False" CssClass="table table-bordered table-condensed"
                                                HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;No" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px" HeaderStyle-ForeColor="#767dba" HeaderStyle-CssClass="font-content-dashboard">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="tindakan" runat="server" Style="padding-left: 10px;"> <%# Container.DataItemIndex + 1 %> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Operasi" ItemStyle-Width="65%" HeaderStyle-Font-Size="11px" HeaderStyle-ForeColor="#767dba" HeaderStyle-CssClass="font-content-dashboard">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="operasi" runat="server" Text='<%# Bind("allergy") %>' Style="padding-left: 10px;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Tanggal" ItemStyle-Width="30%" HeaderStyle-Font-Size="11px" HeaderStyle-ForeColor="#767dba" HeaderStyle-CssClass="font-content-dashboard">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="tanggal" runat="server" Text='<%# DateTime.Parse(Eval("other_health_info_remarks").ToString()).ToString("MMMM yyyy") %>' Style="padding-left: 10px;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div style="border-bottom: 1px solid #ededed">
                                </div>
                                <div class="row" style="min-height: 160px">
                                    <div class="col-sm-12" style="padding-top: 5px; padding-left: 25px;">
                                        <label id="lblbhs_procedure" class="font-subheader-dashboard" style="font-weight: bold; font-size: 14px;">Tindakan Berdasarkan Pertemuan</label>
                                        <br />
                                        <div style="padding-bottom: 5px; padding-right: 10px; overflow-y: auto; max-height: 125px;" class="scrollEMR">
                                            <asp:Label ID="lblemptyinternal" runat="server" Style="font-size: 14px; color: #CDCED9;" Text=" "> <i class="fa fa-ban"></i> 
                                            <label id="lblbhs_nointernalprocedure">No Procedure</label>
                                            </asp:Label>
                                            <asp:GridView ID="gvw_procedure" runat="server"
                                                AutoGenerateColumns="False" CssClass="table table-bordered table-condensed"
                                                HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;No" ItemStyle-Width="5%" HeaderStyle-Font-Size="11px" HeaderStyle-ForeColor="#767dba" HeaderStyle-CssClass="font-content-dashboard">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="tindakan" runat="server" Style="padding-left: 10px;"> <%# Container.DataItemIndex + 1 %> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Tindakan" ItemStyle-Width="40%" HeaderStyle-Font-Size="11px" HeaderStyle-ForeColor="#767dba" HeaderStyle-CssClass="font-content-dashboard">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="tindakan" runat="server" Text='<%# Bind("procedure_remarks") %>' Style="padding-left: 10px;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Doctor" ItemStyle-Width="40%" HeaderStyle-Font-Size="11px" HeaderStyle-ForeColor="#767dba" HeaderStyle-CssClass="font-content-dashboard">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="doctor" runat="server" Text='<%# Bind("doctor_name") %>' Style="padding-left: 10px;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Tanggal" ItemStyle-Width="15%" HeaderStyle-Font-Size="11px" HeaderStyle-ForeColor="#767dba" HeaderStyle-CssClass="font-content-dashboard">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="tanggal" runat="server" Text='<%# Bind("procedure_date","{0:dd MMMM yyyy}") %>' Style="padding-left: 10px;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <!-- =============================================================================================================================================================== -->
            </div>
        </div>


        <!-- ######################################################--- MODAL ---###################################################### -->

        <div class="modal fade" id="modalAssign" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content" style="border-radius: 7px;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" style="text-align: left">
                                    <asp:Label ID="Label2" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server">
                                    <label id="lblbhs_admissionlist"> Admission List </label>
                                    </asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div id="divAdmissionDetail"></div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalEMR" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 80vw;">
                <div class="modal-content" style="border-radius: 7px; height: 90vh;">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align: left">
                            <label id="lblbhs_medicalresumemodal" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold">Medical Resume </label>
                        </h4>
                        <asp:HiddenField ID="EMROrganization" runat="server" />
                        <asp:HiddenField ID="EMREncounter" runat="server" />
                        <asp:HiddenField ID="EMRAdmission" runat="server" />
                    </div>
                    <div style="overflow-y: auto; height: 80vh; width: 100%; padding-left: 15px; padding-right: 15px;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button runat="server" CssClass="hidden" ID="btnPreview" OnClick="btnPreview_Click" />
                                <uc1:Preview runat="server" ID="SoapPagePreview" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalLab" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 70vw;">
                <div class="modal-content" style="border-radius: 7px; height: 80vh;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" style="text-align: left">
                                    <label id="lblbhs_labresultmodal" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold">Laboratory Result </label>
                                </h4>
                                <asp:Button runat="server" ID="btn_labModal" Text="" CssClass="hidden" OnClick="btnlabModal_Click" />
                                <asp:HiddenField ID="hfLab" runat="server" />
                            </div>
                            <div style="overflow-y: auto; height: 70vh; width: 100%; padding-left: 15px; padding-right: 15px;">
                                <tag1:StdLabResult runat="server" ID="StdLabResult" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalRad" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 70vw;">
                <div class="modal-content" style="border-radius: 7px; height: 80vh;">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" style="text-align: left">
                                    <label id="lblbhs_radresultmodal" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold">Radiology Result </label>
                                </h4>
                                <asp:Button runat="server" ID="btnRadDetail" Text="" CssClass="hidden" OnClick="btnRadDetail_Click" />
                                <asp:HiddenField ID="hfRad" runat="server" />
                            </div>
                            <div style="overflow-y: auto; height: 70vh; width: 100%;">
                                <tag1:StdRadResult runat="server" ID="StdRadResult" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div id="patientHistoryModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 70vw;">
                <div class="modal-content" style="border-radius: 7px; height: 80vh;">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" style="text-align: left">
                                    <label id="lblbhs_phresultmodal" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold">Patient History </label>
                                </h4>
                                <asp:Button ID="btn_patientDetail" runat="server" OnClick="btn_patientDetail_Click" CssClass="hidden" />
                                <asp:HiddenField ID="hf_patientID" runat="server" />
                                <asp:HiddenField ID="hf_organizationID" runat="server" />
                                <asp:HiddenField ID="hf_admissionID" runat="server" />
                                <asp:HiddenField ID="hf_encounterID" runat="server" />
                                <asp:HiddenField ID="hf_pagesoapID" runat="server" />
                            </div>
                            <%--<div style="overflow-y: auto; height: 70vh; width: 100%;"></div>--%>
                            <div>
                                <%--<tag1:StdPatientHistory runat="server" ID="StdPatientHistory" Visible="false" />--%>
                                <iframe name="IframeMedicalResumePatient" id="IframeMedicalResumePatient" runat="server" style="width: 100%; height: 80vh; border: none; margin-top: 0%; overflow-y: scroll; padding-right: 0; padding-left: 0%; margin-left: 0;"></iframe>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalRevision" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 70vw;">
                <div class="modal-content" style="border-radius: 7px; height: 85vh;">
                    <asp:UpdatePanel ID="UpdatePanelRevision" runat="server">
                        <ContentTemplate>
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" style="text-align: left">
                                    <label id="lblbhs_revisionmodal" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold">Revision History </label>
                                </h4>
                                <asp:Button runat="server" ID="btnrevisionModal" Text="" CssClass="hidden" OnClick="btnrevisionModal_Click" />
                                <asp:HiddenField ID="HFrevOrgID" runat="server" />
                                <asp:HiddenField ID="HFrevPtnID" runat="server" />
                                <asp:HiddenField ID="HFrevAdmID" runat="server" />
                                <asp:HiddenField ID="HFrevEncID" runat="server" />
                            </div>
                            <div style="overflow-y: auto; height: 78vh; width: 100%; padding-left: 15px; padding-right: 15px;">
                                <asp:Repeater ID="RepeaterRevisionHeader" runat="server" OnItemDataBound="RepeaterRevisionHeader_ItemDataBound">
                                    <ItemTemplate>

                                        <div class="row">
                                            <div class="col-sm-12" style="background-color:#2a3593; color:white; padding:7px 15px; font-size:14px; font-weight:bold;">
                                                <asp:HiddenField ID="HF_REVHeaderID" runat="server" Value='<%#Eval("ID") %>' />
                                                <asp:Label ID="LabelDate" runat="server" Text='<%# DateTime.Parse(Eval("LogDate").ToString()).ToString("dd MMM yyyy, HH.mm") %>'></asp:Label>
                                                &nbsp;-&nbsp; 
                                                <asp:Label ID="LabelDokter" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                            </div>
                                        </div>

                                        <asp:Panel id="panelS" runat="server" class="row" style="border-top:1px solid #ceced3; border-bottom:1px solid #ceced3; background-color:#f2f3f4;">
                                            <div class="col-sm-2" style="padding-top:7px; padding-bottom:7px;">
                                                <label style="font-size:14px; font-weight:bold;">S</label>
                                            </div>
                                            <div class="col-sm-10" style="border-left:1px solid #ceced3; background-color:white; padding-top:7px; padding-bottom:7px;">
                                                <asp:Label ID="LabelComplaint" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelAnamnesis" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelDoctorNotesToNurse" runat="server" Text=""></asp:Label>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel id="panelO" runat="server" class="row" style="border-top:1px solid #ceced3; border-bottom:1px solid #ceced3; background-color:#f2f3f4;">
                                            <div class="col-sm-2" style="padding-top:7px; padding-bottom:7px;">
                                                <label style="font-size:14px; font-weight:bold;">O</label>
                                            </div>
                                            <div class="col-sm-10" style="border-left:1px solid #ceced3; background-color:white; padding-top:7px; padding-bottom:7px;">
                                                <asp:Label ID="LabelOther" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelBloodPresH" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelBloodPresL" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelPulse" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelRespiratory" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelSPO2" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelTemp" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelWeight" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelHeight" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelHeadCir" runat="server" Text=""></asp:Label>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel id="panelA" runat="server" class="row" style="border-top:1px solid #ceced3; border-bottom:1px solid #ceced3; background-color:#f2f3f4;">
                                            <div class="col-sm-2" style="padding-top:7px; padding-bottom:7px;">
                                                <label style="font-size:14px; font-weight:bold;">A</label>
                                            </div>
                                            <div class="col-sm-10" style="border-left:1px solid #ceced3; background-color:white; padding-top:7px; padding-bottom:7px;">
                                                <asp:Label ID="LabelPrimaryDiagnosis" runat="server" Text=""></asp:Label>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel id="panelP" runat="server" class="row" style="border-top:1px solid #ceced3; border-bottom:1px solid #ceced3; background-color:#f2f3f4;">
                                            <div class="col-sm-2" style="padding-top:7px; padding-bottom:7px;">
                                                <label style="font-size:14px; font-weight:bold;">P</label>
                                            </div>
                                            <div class="col-sm-10" style="border-left:1px solid #ceced3; background-color:white; padding-top:7px; padding-bottom:7px;">
                                                <asp:Label ID="LabelPlanningProcedure" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelPlanningOthers" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelProcedureResult" runat="server" Text=""></asp:Label>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel id="panelCPOE" runat="server" class="row" style="border-top:1px solid #ceced3; border-bottom:1px solid #ceced3; background-color:#f2f3f4;">
                                            <div class="col-sm-2" style="padding-top:7px; padding-bottom:7px;">
                                                <label style="font-size:14px; font-weight:bold;">CPOE</label>
                                            </div>
                                            <div class="col-sm-10" style="border-left:1px solid #ceced3; background-color:white; padding-top:7px; padding-bottom:7px;">
                                                
                                                <asp:Repeater ID="RptLabRad" runat="server">
                                                    <%--<HeaderTemplate>
                                                        <b>LAB</b>
                                                    </HeaderTemplate>--%>
                                                    <ItemTemplate>
                                                        <div> 
                                                            <li><asp:Label ID="LabelItemName" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label></li>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <%--<br />
                                                <asp:Repeater ID="RptRad" runat="server">
                                                    <HeaderTemplate>
                                                        <b>RAD</b>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div> 
                                                            <asp:Label ID="LabelItemName" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>--%>

                                            </div>
                                        </asp:Panel>

                                        <asp:Panel id="panelPRES" runat="server" class="row" style="border-top:1px solid #ceced3; border-bottom:1px solid #ceced3; background-color:#f2f3f4;">
                                            <div class="col-sm-2" style="padding-top:7px; padding-bottom:7px;">
                                                <label style="font-size:14px; font-weight:bold;">Prescription</label>
                                            </div>
                                            <div class="col-sm-10" style="border-left:1px solid #ceced3; background-color:white; padding-top:7px; padding-bottom:7px;">
                                                
                                                <asp:Repeater ID="RptDrugs" runat="server">
                                                    <HeaderTemplate>
                                                        <b>Drugs</b>
                                                        <table style="width:100%;" class="table-striped table-condensed">
                                                            <tr>
                                                                <th>Item</th>
                                                                <th>Dose</th>
                                                                <th>Frequency</th>
                                                                <th>Route</th>
                                                                <th>Instruction</th>
                                                                <th>Qty</th>
                                                                <th>UoM</th>
                                                                <th>Iter</th>
                                                                <th>Routine</th>
                                                            </tr>
                                                        
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        
                                                            <tr>
                                                                <td><asp:Label ID="lblItemName" runat="server" Text='<%#Eval("SalesItemName") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblDose" runat="server" Text='<%#Eval("Dose") %>'></asp:Label> &nbsp;&nbsp; <asp:Label ID="lblDoseUom" runat="server" Text='<%#Eval("DoseUom") %>'></asp:Label> </td>
                                                                <td><asp:Label ID="lblFreq" runat="server" Text='<%#Eval("Frequency") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblRoute" runat="server" Text='<%#Eval("Route") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblInstruction" runat="server" Text='<%#Eval("Instruction") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblQty" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblUom" runat="server" Text='<%#Eval("Uom") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblIter" runat="server" Text='<%#Eval("Iteration") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblRoutine" runat="server" Text='<%#Eval("IsRoutine").ToString().ToLower() == "false" ? "No" : "Yes" %>'></asp:Label></td>
                                                            </tr>
                                                       
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                <br />
                                                <asp:Repeater ID="RptCons" runat="server">
                                                    <HeaderTemplate>
                                                        <b>Consumables</b>
                                                        <table style="width:100%;" class="table-striped table-condensed">
                                                            <tr>
                                                                <th>Item</th>
                                                                <th>Qty</th>
                                                                <th>UoM</th>
                                                                <th>Instruction</th>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                            <tr>
                                                                <td><asp:Label ID="lblItemName" runat="server" Text='<%#Eval("SalesItemName") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblQty" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblUom" runat="server" Text='<%#Eval("Uom") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblInstruction" runat="server" Text='<%#Eval("Instruction") %>'></asp:Label></td>
                                                            </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>

                                            </div>
                                        </asp:Panel>
                                    

                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modal-referal-list">
	        <div class="modal-dialog" style="top: 2%; width: 86%;">
                <div class="modal-content">
			        <div class="modal-header" style="height: 35px; padding-top: 7px; padding-bottom: 5px">
				        <h4 class="modal-title"><asp:Label runat="server" Font-Bold="true" ID="modalReferalListCaption" ClientIDMode="Static" Text="REFERAL"></asp:Label></h4>
                    </div>
		
			        <div class="modal-body">
                        <div class="header-pasien-FA">
                            <uc1:PatientCardModal runat="server" ID="PatientCardRefModal" />
                        </div>
                        <uc1:ModalReferalList runat="server" ID="ModalReferalList" />
			        </div>
			        <div class="modal-footer justify-content-right">
                        <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
			        </div>
		        </div>
	        </div>
        </div>

        <div class="modal fade" id="modal-referal-list-balasan">
	        <div class="modal-dialog" style="top: 2%; width: 86%;">
                <div class="modal-content">
			        <div class="modal-header" style="height: 35px; padding-top: 7px; padding-bottom: 5px">
				        <h4 class="modal-title"><asp:Label runat="server" Font-Bold="true" ID="modalReferalListBalasanCaption" ClientIDMode="Static" Text="Form Balasan"></asp:Label></h4>
                    </div>
		
			        <div class="modal-body">
                        <div class="header-pasien-FA">
                            <uc1:PatientCardModal runat="server" ID="PatientCardRefModalBalasan" />
                        </div>
                        <uc1:ModalReferalListBalasan runat="server" ID="ModalReferalListBalasan" />
			        </div>
			        <div class="modal-footer justify-content-right">
                        <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
			        </div>
		        </div>
	        </div>
        </div>

    </body>

    <script type="text/javascript">
        $(window).load(function () {
            $(".loadPage").fadeOut("slow");
        });

        function Open(content) {
            $('#modalAssign').modal('show');

            var list = content.split("|");
            var div = document.createElement('div');
            var popup = document.getElementById('divAdmissionDetail');

            popup.innerHTML = "";

            //div.innerHTML += "<div id='myPopover" + number + "' class='popover popover-x popover-default'> <div class='arrow'></div> <div class='popover-body popover-content'> TES aja </div></div>";

            for (var i = 0; i < list.length; i++) {
                if (i != 0) {
                    div.innerHTML += "<hr>";
                }

                var data = list[i].split("#");

                //var y = data[0].split(" ");
                //var x = y[0].split("/");
                //var tgl = new Date(x[2], x[1], x[0]);

                //var months = ["", "JAN", "FEB", "MAR","APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"];

                div.innerHTML += "<div class='btn-group btn-group-justified' role='group' aria-label='...'>";
                if (data[6] == "0") {
                    div.innerHTML += "<div class='btn-group' role='group' style='width:79%'><label style='font-weight:bold'>" + data[5] + "</label><br/><label style='font-size:11px'>" + data[0] + " " + "</label><br/<label style='font-size:11px'>" + data[7] + "</label></div>";
                }
                else {
                    div.innerHTML += "<div class='btn-group' role='group' style='width:79%'><label style='color:green;font-weight:bold'>" + data[5] + "</label><br/><label>" + data[0] + "</label><br/<label style='font-size:11px'>" + data[7] + "</label></div>";
                }
                if (data[1] != "-") {
                    div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><a href='javascript:Lab(" + data[1].toString() + ")'><img src='../../Images/Icon/labE.png'/ title='Laboratory' width='30px' height='30px'></a></div>";
                }
                else if (data[1] == "-") {
                    div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><img src='../../Images/Icon/labD.png\' title='Laboratory' width='30px' height='30px'></div>";
                }
                if (data[2] != "-") {
                    div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><a href='javascript:Rad(" + data[2].toString() + ")'><img src='../../Images/Icon/radE.png' title='Radiology' width='30px' height='30px'/></a></div>";
                }
                else if (data[2] == "-") {
                    div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><img src='../../Images/Icon/radD.png\' title='Radiology' width='30px' height='30px'></div>";
                }
                if (data[3] != "-") {
                    div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><a href='javascript:EMR(" + data[8].toString() + ", /" + data[3].toString() + "/, " + data[4].toString() + ")'><img src='../../Images/Icon/ic_HistoryA.svg' title='Patient History' width='30px' height='30px'/></a></div>";
                }
                else if (data[3] == "-") {
                    div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><img src='../../Images/Icon/ic_HistoryN.svg\' title='Patient History' width='30px' height='30px'></div>";
                }
                //div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><img src='../../Images/Dashboard/ic_History.png\' width='30px' height='30px'></div>";
                div.innerHTML += "</div></div>";
            }
            popup.appendChild(div);

            //document.getElementById("mybtn" + number).click();
            return true;
        }
        function Lab(admissionList) {
            $('#modalLab').modal('show');
            document.getElementById('<%=hfLab.ClientID%>').value = admissionList;
            document.getElementById('<%=btn_labModal.ClientID%>').click();
            return true;
        }

        function patientHistory(patientID, organizationID, admissionId, encounterID, pagesoapID) {
            $('#patientHistoryModal').modal('show');
            document.getElementById('<%=hf_patientID.ClientID%>').value = patientID;
            document.getElementById('<%=hf_admissionID.ClientID%>').value = admissionId;
            document.getElementById('<%=hf_organizationID.ClientID%>').value = organizationID;
            document.getElementById('<%=hf_encounterID.ClientID%>').value = encounterID;
            document.getElementById('<%=hf_pagesoapID.ClientID%>').value = pagesoapID;

            document.getElementById('<%= btn_patientDetail.ClientID%>').click();
            return true;
        }

        function Rad(admissionList) {
            $('#modalRad').modal('show');
            document.getElementById('<%=hfRad.ClientID%>').value = admissionList;
            document.getElementById('<%=btnRadDetail.ClientID%>').click();

            return true;
        }
        function EMR(Organization, Encounter, Admission) {
            $('#modalEMR').modal('show');
            document.getElementById('<%=EMROrganization.ClientID%>').value = Organization;
            document.getElementById('<%=EMREncounter.ClientID%>').value = Encounter;
            document.getElementById('<%=EMRAdmission.ClientID%>').value = Admission;

            var Button = "<%=btnPreview.ClientID %>";
            document.getElementById(Button).click();

            return true;
        }

        function switchBahasa() {
            var bahasa = document.getElementById('<%=HFisBahasa.ClientID%>').value;
            if (bahasa == "ENG") {
                document.getElementById('lblbhs_allergies').innerHTML = "Allergies";
                document.getElementById('lblbhs_drugs').innerHTML = "Drugs";
                document.getElementById('lblbhs_noallergiesdrugs').innerHTML = "No allergies";
                document.getElementById('lblbhs_food').innerHTML = "Food";
                document.getElementById('lblbhs_noallergiesfood').innerHTML = "No allergies";
                document.getElementById('lblbhs_lain2').innerHTML = "Others";
                document.getElementById('lblbhs_noallergieslain').innerHTML = "No allergies";

                document.getElementById('lbhbhs_routinemedication').innerHTML = "Current/Routine Medication";
                document.getElementById('lblbhs_noroutinemedication').innerHTML = "No routine medication";
                //document.getElementById('lblbhs_lastdrugprescription').innerHTML = "Last Drug Prescription";
                //document.getElementById('lblbhs_nodrugprescription').innerHTML = "No drug prescription";
                //document.getElementById('lblbhs_latestmedicalhistory').innerHTML = "Latest Medical History";
                //document.getElementById('lblbhs_diagnosis').innerHTML = "Diagnosis";
                //document.getElementById('lblbhs_nomedicalhistory').innerHTML = "No medical history";
                //document.getElementById('lblbhs_planningandprocedure').innerHTML = "Planning and Procedure";
                //document.getElementById('lblbhs_noprocedure').innerHTML = "No procedure";
                document.getElementById('lblbhs_admissionhistory').innerHTML = "Admission History";
                document.getElementById('lblbhs_noadmissionhistory').innerHTML = "No admission history this year";
                document.getElementById('lblbhs_admissionlist').innerHTML = "Admission List";
                document.getElementById('lblbhs_SOAP').innerHTML = "5 Latest SOAP";
                document.getElementById('lblbhs_no3soap').innerHTML = "No Latest SOAP";
                document.getElementById('lblbhs_labresultmodal').innerHTML = "Laboratory Result";
                document.getElementById('lblbhs_radresultmodal').innerHTML = "Radiology Result";
                document.getElementById('lblbhs_phresultmodal').innerHTML = "Patient History";
                document.getElementById('lblbhs_medicalresumemodal').innerHTML = "Medical Resume";

                document.getElementById('lblbhs_reminder').innerHTML = "Reminder";
                document.getElementById('lblbhs_hideotherdoctor').innerHTML = "Hide Others Doctor's Reminder";
                document.getElementById('lblbhs_noreminder').innerHTML = "No Reminder";

                var table = document.getElementById("<%=gvw_reminder.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "\xa0\xa0No";
                        headers[1].innerText = "\xa0\xa0\xa0Created Date";
                        headers[2].innerText = "\xa0\xa0\xa0Reminder";
                        headers[3].innerText = "\xa0\xa0\xa0Doctor";
                    }
                }

                //document.getElementById('lblbhs_healthhistory').innerHTML = "Health History";
                //document.getElementById('lblbhs_outsideprocedure').innerHTML = "Procedure Outside Encounter";
                //document.getElementById('lblbhs_nooutsideprocedure').innerHTML = "No Procedure";
                //document.getElementById('lblbhs_operasi').innerHTML = "Surgery History";
                //document.getElementById('lblbhs_nosurgery').innerHTML = "No Surgery History";
                //document.getElementById('lblbhs_procedure').innerHTML = "Procedure On Encounter";
                //document.getElementById('lblbhs_nointernalprocedure').innerHTML = "No Procedure";
                document.getElementById('lblbhs_riwayattindakan').innerHTML = "Procedure Result";
                document.getElementById('lblbhs_notindakan').innerHTML = "No Procedure Result";

                var table = document.getElementById("<%=gvw_outsideprocedure.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "\xa0\xa0\xa0No";
                        headers[1].innerText = "\xa0\xa0\xa0Procedure";
                        headers[2].innerText = "\xa0\xa0\xa0Date";
                    }
                }

                var table = document.getElementById("<%=gvw_surgery.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "\xa0\xa0\xa0No";
                        headers[1].innerText = "\xa0\xa0\xa0Surgery";
                        headers[2].innerText = "\xa0\xa0\xa0Date";
                    }
                }

                var table = document.getElementById("<%=gvw_procedure.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "\xa0\xa0\xa0No";
                        headers[1].innerText = "\xa0\xa0\xa0Procedure";
                        headers[2].innerText = "\xa0\xa0\xa0Doctor";
                        headers[3].innerText = "\xa0\xa0\xa0Date";
                    }
                }

                var table = document.getElementById("<%=gvw_hasiltindakan.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "\xa0Date / Admission";
                        headers[1].innerText = "Doctor";
                        headers[2].innerText = "Procedure Result";
                    }
                }

                var table = document.getElementById("<%=gvw_latestsoap.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "\xa0Date";
                        headers[1].innerText = "Doctor";
                        headers[6].innerText = "Prescription";
                    }
                }

                document.getElementById('lblbhs_reminder_hide').innerHTML = "Reminder";
                document.getElementById('lblbhs_noreminder_hide').innerHTML = "No reminder";
                document.getElementById('lblbhs_allergies_hide').innerHTML = "Allergies";
                document.getElementById('lblbhs_noallergies_hide').innerHTML = "No allergies";
                document.getElementById('lbhbhs_routinemedication_hide').innerHTML = "Routine Medication";
                document.getElementById('lblbhs_noroutinemedication_hide').innerHTML = "No routine medication";
                document.getElementById('lblbhs_riwayattindakan_new').innerHTML = "Procedure Result";
                document.getElementById('lblbhs_notindakan_hide').innerHTML = "No procedure result";
                document.getElementById('lblbhs_SOAP_hide').innerHTML = "5 Latest SOAP";
                document.getElementById('lblbhs_no5soap_hide').innerHTML = "No latest SOAP";

            }
            else if (bahasa == "IND") {
                document.getElementById('lblbhs_allergies').innerHTML = "Alergi";
                document.getElementById('lblbhs_drugs').innerHTML = "Obat";
                document.getElementById('lblbhs_noallergiesdrugs').innerHTML = "Tidak ada alergi";
                document.getElementById('lblbhs_food').innerHTML = "Makanan";
                document.getElementById('lblbhs_noallergiesfood').innerHTML = "Tidak ada alergi";
                document.getElementById('lblbhs_lain2').innerHTML = "Lain-lain";
                document.getElementById('lblbhs_noallergieslain').innerHTML = "Tidak ada alergi";

                document.getElementById('lbhbhs_routinemedication').innerHTML = "Pengobatan Saat Ini";
                document.getElementById('lblbhs_noroutinemedication').innerHTML = "Tidak ada pengobatan rutin";
                //document.getElementById('lblbhs_lastdrugprescription').innerHTML = "Obat Yang Terakhir Diresepkan";
                //document.getElementById('lblbhs_nodrugprescription').innerHTML = "Tidak ada resep";
                //document.getElementById('lblbhs_latestmedicalhistory').innerHTML = "Riwayat Medis Terakhir";
                //document.getElementById('lblbhs_diagnosis').innerHTML = "Diagnosa";
                //document.getElementById('lblbhs_nomedicalhistory').innerHTML = "Tidak ada riwayat medis";
                //document.getElementById('lblbhs_planningandprocedure').innerHTML = "Rencana dan Tindakan";
                //document.getElementById('lblbhs_noprocedure').innerHTML = "Tidak ada tindakan";
                document.getElementById('lblbhs_admissionhistory').innerHTML = "Riwayat Konsultasi";
                document.getElementById('lblbhs_noadmissionhistory').innerHTML = "Tidak ada riwayat konsultasi tahun ini";
                document.getElementById('lblbhs_admissionlist').innerHTML = "Daftar Admisi";
                document.getElementById('lblbhs_SOAP').innerHTML = "5 SOAP Terakhir";
                document.getElementById('lblbhs_no3soap').innerHTML = "Belum ada SOAP Terakhir";
                document.getElementById('lblbhs_labresultmodal').innerHTML = "Hasil Laboratorium";
                document.getElementById('lblbhs_radresultmodal').innerHTML = "Hasil Radiologi";
                document.getElementById('lblbhs_phresultmodal').innerHTML = "Riwayat Pasien";
                document.getElementById('lblbhs_medicalresumemodal').innerHTML = "Resume Medis";

                document.getElementById('lblbhs_reminder').innerHTML = "Pengingat";
                document.getElementById('lblbhs_hideotherdoctor').innerHTML = "Sembunyikan Pengingat Milik Dokter Lain";
                document.getElementById('lblbhs_noreminder').innerHTML = "Tidak ada pengingat";

                var table = document.getElementById("<%=gvw_reminder.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "\xa0\xa0No";
                        headers[1].innerText = "\xa0\xa0\xa0Tgl. Dibuat";
                        headers[2].innerText = "\xa0\xa0\xa0Pengingat";
                        headers[3].innerText = "\xa0\xa0\xa0Dokter";
                    }
                }

                //document.getElementById('lblbhs_healthhistory').innerHTML = "Riwayat Kesehatan";
                //document.getElementById('lblbhs_outsideprocedure').innerHTML = "Tindakan di Luar Pertemuan";
                //document.getElementById('lblbhs_nooutsideprocedure').innerHTML = "Tidak ada tindakan";
                //ocument.getElementById('lblbhs_operasi').innerHTML = "Riwayat Operasi";
                //document.getElementById('lblbhs_nosurgery').innerHTML = "Tidak ada riwayat operasi";
                //document.getElementById('lblbhs_procedure').innerHTML = "Tindakan Berdasarkan Pertemuan";
                //document.getElementById('lblbhs_nointernalprocedure').innerHTML = "Tidak ada tindakan";
                document.getElementById('lblbhs_riwayattindakan').innerHTML = "Hasil Tindakan";
                document.getElementById('lblbhs_notindakan').innerHTML = "Tidak ada hasil tindakan";

                var table = document.getElementById("<%=gvw_outsideprocedure.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "\xa0\xa0\xa0No";
                        headers[1].innerText = "\xa0\xa0\xa0Tindakan";
                        headers[2].innerText = "\xa0\xa0\xa0Tanggal";
                    }
                }

                var table = document.getElementById("<%=gvw_surgery.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "\xa0\xa0\xa0No";
                        headers[1].innerText = "\xa0\xa0\xa0Operasi";
                        headers[2].innerText = "\xa0\xa0\xa0Tanggal";
                    }
                }

                var table = document.getElementById("<%=gvw_procedure.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "\xa0\xa0\xa0No";
                        headers[1].innerText = "\xa0\xa0\xa0Tindakan";
                        headers[2].innerText = "\xa0\xa0\xa0Dokter";
                        headers[3].innerText = "\xa0\xa0\xa0Tanggal";
                    }
                }

                var table = document.getElementById("<%=gvw_hasiltindakan.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "\xa0Tgl / Admission";
                        headers[1].innerText = "Dokter";
                        headers[2].innerText = "Hasil Tindakan";
                    }
                }

                var table = document.getElementById("<%=gvw_latestsoap.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "\xa0Tanggal";
                        headers[1].innerText = "Dokter";
                        headers[6].innerText = "Resep";
                    }
                }

                document.getElementById('lblbhs_reminder_hide').innerHTML = "Pengingat";
                document.getElementById('lblbhs_noreminder_hide').innerHTML = "Tidak ada pengingat";
                document.getElementById('lblbhs_allergies_hide').innerHTML = "Alergi";
                document.getElementById('lblbhs_noallergies_hide').innerHTML = "Tidak ada alergi";
                document.getElementById('lbhbhs_routinemedication_hide').innerHTML = "Pengobatan Rutin";
                document.getElementById('lblbhs_noroutinemedication_hide').innerHTML = "Tidak ada pengobatan rutin";
                document.getElementById('lblbhs_riwayattindakan_new').innerHTML = "Hasil Tindakan";
                document.getElementById('lblbhs_notindakan_hide').innerHTML = "Tidak ada hasil tindakan";
                document.getElementById('lblbhs_SOAP_hide').innerHTML = "5 SOAP Terakhir";
                document.getElementById('lblbhs_no5soap_hide').innerHTML = "Tidak ada SOAP";
            }
        }

        function topFunction() {
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
        }

        $('body').scroll(function (e) {
            if ($(this).scrollTop() > 250) {
                $("#myIDtoTop").attr('class', 'bottomMenuu showw');
            } else {
                $("#myIDtoTop").attr('class', 'bottomMenuu hidee');
            }
        });

        function NothingToDo() {
            return false;
        }

        function RevModal(orgid, ptnid, admid, encid) {
            $('#modalRevision').modal('show');
            document.getElementById('<%=HFrevOrgID.ClientID%>').value = orgid;
            document.getElementById('<%=HFrevPtnID.ClientID%>').value = ptnid;
            document.getElementById('<%=HFrevAdmID.ClientID%>').value = admid;
            document.getElementById('<%=HFrevEncID.ClientID%>').value = encid;
            document.getElementById('<%=btnrevisionModal.ClientID%>').click();
            return true;
        }

        //$(function () {
        //    var interval = setInterval(function () {
        //        if ($('body').scrollTop() < ($('body')[0].scrollHeight - screen.height)) {
        //            $('body').scrollTop($('body').scrollTop() + (screen.height - 250));

        //            console.log("scroll : " + $('body').scrollTop() + " <"+screen.height+"> height : " + ($('body')[0].scrollHeight-screen.height));
        //        } else {
        //            topFunction();
        //            //clearInterval(interval);
        //            console.log("stop");
        //        }
        //    }, 4000);
        //});

        //$(document).ready(function () {
            //$('.poppop').popover();

            //$('.poppop').popover({   
            //    template: '<div class="popover"><div class="arrow"></div><div class="popover-inner"><h3 class="popover-title"></h3><div class="popover-content" style="padding:0px;overflow-y:auto;max-height:450px; width: 400px"><p></p></div></div></div>',     
            //    html: true,
    	       // placement: 'bottom',
    	       // title: 'Notifications'
            //    });
         //});

        $(document).ready(function () {
            var admissionId = new URLSearchParams(window.location.search).get('AdmissionId');


            var listKewaspadaan = document.getElementById('MainContent_listKewaspadaan');
            var divKewaspadaan = document.getElementById('MainContent_divKewaspadaan');

            var alertList = localStorage.getItem("alertList" + admissionId);

            if (alertList != "-") {
                divKewaspadaan.style.display = "";
                listKewaspadaan.innerText = localStorage.getItem("alertList" + admissionId);
            } else {
				divKewaspadaan.style.display = "none";
			}


        })

	</script>

</asp:Content>