<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="StdSoapPage.aspx.cs" Inherits="Form_SOAP_Template_StdSoapPage" %>
<%@ Register Src="~/Form/General/Control/PatientCard.ascx" TagPrefix="uc1" TagName="PatientCard" %>
<%@ Register Src="~/Form/SOAP/PreviewTemplate/SoapPagePreview.ascx" TagPrefix="uc1" TagName="Preview" %>
<%@ Register Src="~/Form/SOAP/Control_Template/StdSubjective.ascx" TagName="StdSubjective" TagPrefix="uc1"%>
<%@ Register Src="~/Form/SOAP/Control_Template/StdObjective.ascx" TagName="StdObjective" TagPrefix="uc1"%>
<%@ Register Src="~/Form/SOAP/Control_Template/StdPlanning.ascx" TagName="StdPlanning" TagPrefix="uc1"%>
<%@ Register Src="~/Form/SOAP/Control_Template/StdAssessment.ascx" TagName="StdAssessment" TagPrefix="uc1"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body, html {
            width: 100%;
            height: 100%;
        }
        .item{
             width:100px;
             margin-left:auto;
             padding: 10px;
             display: block;
             font-size: 18px;
             color: black;
             background:#b3b7d1;
             border-radius: 25px 0 0 25px;
     
         }
         .item:hover{
           width:200px;
           -webkit-transition: width 2s; /* Safari */
           transition: width 0.2s;
           background:#2c3794;
           color: white;
            }
         .item>span{
             margin-right:20px;
             margin-left:5px;
         }

        .itemcontainer > div {
            padding-top:0px;
            padding-bottom:2px;
        }

        .itemsave{
             width:185px;
             height:43px;
             margin-left:auto;
             padding: 10px;
             display: block;
             font-size: 14px;
             color: black;
             background:#4d9b35;
             border-radius: 25px 0 0 25px;
         }
         .itemsave:hover{
           width:300px;
           height:43px;
           -webkit-transition: width 2s; /* Safari */
           transition: width 0.2s;
           background:#4d9b35;
           color: white;
            }
         .itemsave>span{
             margin-right:20px;
             margin-left:5px;
         }

         .itempreview{
             width:210px;
             height:43px;
             margin-left:auto;
             padding: 10px;
             display: block;
             font-size: 14px;
             color: black;
             background:#8ba303;
             border-radius: 25px 0 0 25px;
         }
         .itempreview:hover{
           width:340px;
           height:43px;
           -webkit-transition: width 2s; /* Safari */
           transition: width 0.2s;
           background:#8ba303;
           color: white;
            }
         .itempreview>span{
             margin-right:20px;
             margin-left:5px;
         }


 
         .itemappt{
             width:210px;
             height:43px;
             margin-left:auto;
             padding: 10px;
             display: block;
             font-size: 14px;
             color: black;
             background:#f88805;
             border-radius: 25px 0 0 25px;
         }
         .itemappt:hover{
           width:370px;
           height:43px;
           -webkit-transition: width 2s; /* Safari */
           transition: width 0.2s;
           background:#f88805;
           color: white;
            }
         .itemappt>span{
             margin-right:20px;
             margin-left:5px;
         }

 
         .itemsign{
             width:210px;
             height:43px;
             margin-left:auto;
             padding: 10px;
             display: block;
             font-size: 14px;
             color: black;
             background:#c43d32;
             border-radius: 25px 0 0 25px;
         }
         .itemsign:hover{
           width:330px;
           height:43px;
           -webkit-transition: width 2s; /* Safari */
           transition: width 0.2s;
           background:#c43d32;
           color: white;
            }
         .itemsign>span{
             margin-right:20px;
             margin-left:5px;
         }

        .itemcontainersave > div {
            padding-top:0px;
            padding-bottom:2px;
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
    </style>

    <script type="text/javascript">
        var shouldsubmit = false;
        window.onbeforeunload = confirmExit;
        function confirmExit()
        {
            if (!shouldsubmit) {
                return "You have attempted to leave this page.  If you have made any changes to the fields without clicking the Save button, your changes will be lost.  Are you sure you want to exit this page?";
            }
        }

        function Preview() {
            $('#modalPreview').modal('show');
            var Button = "<%=btnPreview.ClientID %>";
            document.getElementById(Button).click();
            return true;
        }

        function PreviewCopy() {
            $('#modalCopySOAP').modal('show');
            <%--var Button = "<%=btncopysoap.ClientID %>";
            document.getElementById(Button).click();--%>
            return true;
        }

        function hidecopy() {
            var copy = $("[id$='hfsavemode']").val();
            if (copy == '1') {
                var dvPassport = document.getElementById("iconcopy");
                dvPassport.style.display = "none";

                var dvPassport2 = document.getElementById("btnsaveasdraft");
                dvPassport2.style.display = "none";
            }
            else {
                var dvPassport = document.getElementById("iconcopy");
                dvPassport.style.display = "";

                var dvPassport2 = document.getElementById("btnsaveasdraft");
                dvPassport2.style.display = "";
            }
        }

        function ModalSubmit() {
            var savemode = $("[id$='hfsavemode']").val();
            if (savemode == '1') {
                $('#modalsubmitDisable').modal('show');
            }
            else {
                $('#modalsubmit').modal('show');
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
        function enabledisc() {
            var consfeestring = $("[id$='ddl_consultationfee'] option:selected").text();
            var totalfee = consfeestring.split(' ~ Rp ');
            
            $("[id$='txttotalfee']").val(totalfee[1]);
            $("[id$='txtDiscount']").val('');   
            $("[id$='txtDiscount']").removeAttr("Disabled");
        }
        function CalculateTotalFee() {
            var consfeestring = $("[id$='ddl_consultationfee'] option:selected").text();
            var totalfee = consfeestring.split(' ~ Rp ');
            var totalfeeint = parseInt(totalfee[1].replace(/\,/g,''));

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

        function keypressdoctor() {
            var c = event.keyCode;
            if (c == 13) {
                var Button = "<%=btnsearchDoctor.ClientID %>";
                document.getElementById(Button).click();
            }
        }
    </script>
    <body>
        <%-- ========================================================== PATIENT CARD & PAGE SPECIALITY ================================================================ --%>
    <div style="height:76px;position:fixed;background-color:white; width:100%;transform:translate(0,-0%);z-index:1;box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.26);" class="container-fluid">
        <asp:HiddenField ID="hfPatientId" runat="server" />
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
        <uc1:PatientCard runat="server" ID="PatientCard" />
    </div>

    <div style="height:40px;position:fixed;top:126px;background-color:#e7e8ed; width:100%;transform:translate(0,-0%);z-index:1;box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.26);" class="container-fluid">
        <div class="row" style="padding-left:15px;padding-top:8px">
            <label style="font-family:Helvetica;font-size:12px;">Poli Selection</label>
                <asp:DropDownList style="cursor:pointer;border-radius: 2px;border: solid 1px #cdced9;" ID="ddlForm_Type" Width="100%" Height="25px" runat="server">
                </asp:DropDownList>
            <asp:Button runat="server" Text="Choose" Style="width: 71px;height: 25px;border-radius: 4px;background-color: #4d9b35;font-family:Helvetica;font-size:12px;color:#ffffff" OnClick="btnChoose_onClick" />
            <asp:Button runat="server" CssClass="hidden" ID="btncopysoap"/>
            <a href="javascript:PreviewCopy();" id="iconcopy" style="padding-left:20px;display:none"><span><img src="<%= Page.ResolveClientUrl("~/Images/PatientHistory/ic_ScannedMR.png") %>" style="padding-right:5px" /></span><strong>Copy SOAP</strong></a>
        </div>
    </div>
    <%-- ========================================================== PATIENT CARD & PAGE SPECIALITY================================================================ --%>
    

        <section id="subjective" style="padding-top:150px; margin-top:-30px">
          <div class="container-fluid">
            <div class="row">
              <div class="col-lg-8 mx-auto" style="height:100%;width:98%;">
                  <h3><strong>SUBJECTIVE</strong></h3>
                    <asp:UpdatePanel runat="server">
                      <ContentTemplate>

                        <div class="col-lg-12" style="padding-left:0px;margin:0px;">
                            <div style="min-height:120px;background-color:white; width:100%;border-bottom:1px; margin-top:0px;    border-radius: 6px 6px 0px 0px;" class="modal-dialog center-block">
                               <div>
                                    <label class="form-control headerpanel" style="border-radius:6px 6px 0px 0px;">Chief Complaint <label class="subheader"> (Keluhan Utama)</label></label> 
                                </div>
                                <div class="modal-body" style="padding-top:0px;padding-bottom:5px">
                                    <asp:TextBox runat="server" style="outline-color:transparent;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif" placeholder="Type here..."  BorderColor="transparent" ID="Complaint" TextMode="MultiLine" Rows="3" MaxLength="0" />
                                </div>

                                <div class="row" style="margin:0px;border-bottom:solid 1px #cdced9;padding-bottom:5px">
                                    <div class="col-sm-4"><label class="headerpanel">Anamnesis</label> </div>
                                    <div class="col-sm-3" style="text-align:right">
                                        <label class="itemlab"><asp:CheckBox runat="server" ID="chkpregnant" CssClass="mycheckbox" Style="font-weight:bold;font-size:12px" Text="Is Pregnant" /></label>
                                    </div>
                                    <div class="col-sm-3"  style="text-align:right">
                                        <label class="itemlab"><asp:CheckBox runat="server" ID="chkbreastfeed" CssClass="mycheckbox" Style="font-weight:bold;font-size:12px" Text="Breast Feeding" /></label>
                                    </div>
                                </div>
                                <div class="modal-body" style="padding-top:0px;padding-bottom:5px">
                                    <asp:TextBox runat="server" style="outline-color:transparent;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif" placeholder="Type here..."  BorderColor="transparent" ID="Anamnesis" TextMode="MultiLine" Rows="3" />
                                </div>
                                      <asp:UpdatePanel runat="server">
                                          <ContentTemplate>
                                              <uc1:StdSubjective runat="server" id="StdSubjective"/>
                                          </ContentTemplate>
                                      </asp:UpdatePanel>
                                </div>
                           </div>

                      </ContentTemplate>
                  </asp:UpdatePanel>
              </div>
            </div>
          </div>
        </section>

        <section id="objective" style="padding-top:150px; margin-top:-150px">
          <div class="container-fluid">
	        <div class="row">
	          <div class="col-lg-8 mx-auto" style="height:100%;width:98%;">
		          <h3><strong>OBJECTIVE</strong></h3>
                  <asp:UpdatePanel runat="server">
                      <ContentTemplate>
                          <div class="col-lg-12" style="padding-left:0px;margin:0px;">
                            <div style="min-height:120px;background-color:white; width:100%;border-bottom:1px; margin-top:0px;    border-radius: 6px 6px 0px 0px;" class="modal-dialog center-block">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <uc1:StdObjective runat="server" id="StdObjective"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                           </div>
                      </ContentTemplate>
                  </asp:UpdatePanel>
		          </div>
	        </div>
          </div>
        </section>


        <section id="analysis" style="padding-top:150px; margin-top:-150px">
          <div class="container-fluid">
	        <div class="row">
	          <div class="col-lg-8 mx-auto" style="height:100%;width:98%;">
		          <h3><strong>ASSESSMENT</strong></h3>
                  <asp:UpdatePanel runat="server">
                      <ContentTemplate>
                          <uc1:StdAssessment runat="server" id="StdAssessment"/>
                      </ContentTemplate>
                  </asp:UpdatePanel>
		          </div>
	        </div>
          </div>
        </section>

        <section id="planning" style="padding-top:150px; margin-top:-150px">
          <div class="container-fluid">
	        <div class="row">
	          <div class="col-lg-8 mx-auto" style="height:100%;width:98%;">
		          <h3><strong>PLANNING</strong></h3>
                  <%-- ==================================================== PLANNING & PROCEDURE ============================================================ --%>
                <div class="col-lg-12" style="padding-left:0px;margin:0px;">
                    <div style="min-height:120px;background-color:white; width:100%;border-bottom:1px; margin-top:0px;    border-radius: 6px 6px 0px 0px;" class="modal-dialog center-block">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div>
                                    <label class="form-control headerpanel" style="border-radius:6px 6px 0px 0px;">Planning & Procedure</label> 
                                </div>
                                <div class="modal-body" style="padding-top:0px;padding-bottom:5px">
                                    <asp:TextBox runat="server" style="outline-color:transparent;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif" placeholder="Type here..."  BorderColor="transparent" ID="txtPlanning" TextMode="MultiLine" Rows="3" />
                                </div>
                            </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                </div>
                <%-- ==================================================== END PLANNING & PROCEDURE ============================================================ --%>

                    <uc1:StdPlanning runat="server" id="StdPlanning"/>
                      
		          </div>
	        </div>
          </div>
        </section>

   <%-- ========================================================== button floating ================================================================ --%>
    <div class="itemcontainer" style="position:fixed; right:-60px;top:40%; transform:translate(0,-50%); text-align:left; z-index:1" >
        <div>
            <a class="item" href="#subjective"><span><strong>S</strong></span>Subjective</a>
        </div>
        <div>
            <a class="item" href="#subjective"><span><strong>O</strong></span>Objective</a>
        </div>
        <div>
            <a class="item" href="#subjective"><span><strong>A</strong></span>Analysis</a>
        </div>
        <div>
            <a class="item" href="#planning"><span><strong>P</strong></span>Planning</a>
        </div>
    </div>
        
    <div class="itemcontainersave" id="btnsaveasdraft" style="display:none;position:fixed; right:-140px;top:70%; transform:translate(0,-50%); text-align:left; z-index:1" >
       <div>
           <asp:LinkButton  runat="server" CssClass="itemsave" OnClientClick="javascript:shouldsubmit=true" OnClick="btnsave_click"><span><img src="<%= Page.ResolveClientUrl("~/Images/S/ic_SaveAsDraft.png") %>" /></span>Save as Draft</asp:LinkButton>
       </div>
    </div>
    <div class="itemcontainersave" style="position:fixed; right:-165px;top:77%; transform:translate(0,-50%); text-align:left; z-index:1" >
        <div>
           <a href="javascript:Preview();" class="itempreview" ><span><img src="<%= Page.ResolveClientUrl("~/Images/S/ic_PreviewResume.png") %>" /></span>Resume Preview</a>
       </div>
    </div>
    <div class="itemcontainersave" style="position:fixed; right:-165px;top:84%; transform:translate(0,-50%); text-align:left; z-index:1" >
        <div>
           <asp:LinkButton runat="server" CssClass="itemappt" ><span><img src="<%= Page.ResolveClientUrl("~/Images/S/ic_CreateAppointment.png") %>" /></span>Create Appointment</asp:LinkButton>
       </div>
    </div>
    <div class="itemcontainersave" style="position:fixed; right:-165px;top:91%; transform:translate(0,-50%); text-align:left; z-index:1" >
        <div>
           <a href="javascript:ModalSubmit();" class="itemsign" ><span><img src="<%= Page.ResolveClientUrl("~/Images/S/ic_SubmitSign.png") %>" /></span>Submit & Sign</a>
       </div>
    </div>
    <%-- ========================================================== button floating ================================================================ --%>
    <%-- ========================================================== modalPreview ================================================================ --%>
        <div class="modal fade" id="modalPreview" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div style="width:80%;height:85%;vertical-align:middle" class="container-fluid">
                <div class="modal-content">
                    <div class="modal-header" style="height:40px;padding-top:10px;padding-bottom:5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align:left"><asp:Label ID="lblModalTitle" style="font-family:Helvetica, Arial, sans-serif;font-weight:bold" runat="server" Text="Medical Resume"></asp:Label></h4>
                        <div style="text-align:center;background-color:lightgray;"">
                            <asp:HyperLink runat="server" ID="email" CssClass="btn btn-primary" NavigateUrl="~/Form/SOAP/PreviewTemplate/EmailSoap.aspx" Target="_blank">Send as Email</asp:HyperLink>
                            <asp:HyperLink runat="server" ID="preview" CssClass="btn btn-primary" NavigateUrl="~/Form/SOAP/PreviewTemplate/PrintSoap.aspx" Target="_blank">Save as PDF</asp:HyperLink>
                        </div>
                        <br />
                    </div>
                    <br />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional"><ContentTemplate>
                        <div style="overflow-y: auto; height:500px;width:98%;padding-left:15px;padding-top:10px">
                            <asp:Button runat="server" CssClass="hidden" ID="btnPreview" OnClick="btnPreview_click"/>
                            <uc1:Preview runat="server" id="SoapPagePreview" />
                        </div>
                    </ContentTemplate></asp:UpdatePanel>
                    <br />
                </div>
            </div>
        </div>

    <%-- ========================================================== modalPreview ================================================================ --%>
   
    <%-- ========================================================== MODAL COPY SOAP ================================================================ --%>
<div class="modal fade" id="modalCopySOAP" tabindex="-1"  role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate>
    <div class="modal-dialog" style="top: 2%;width:80%;">
        <div class="modal-content" style="border-radius: 6px;box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
            <div class="modal-header" style="height:30px;padding-top:5px;padding-bottom:5px">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h5 class="modal-title"><asp:Label ID="Label3" style="font-family:Helvetica;font-weight:bold;font-size:14px" runat="server" Text="Copy SOAP"></asp:Label></h5>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-sm-3"><asp:HiddenField ID="hfsoapcopystring" runat="server" />
                        <asp:HiddenField ID="hfheader" runat="server" />
                        <asp:HiddenField ID="hfallergy" runat="server" />
                        <asp:HiddenField ID="hfroutine" runat="server" />
                    </div>
                    <div class="col-sm-6" style="padding-top:20px"><label style="font-weight:bold;font-size:14px">Preview</label></div>
                    <div class="col-sm-3" style="text-align:right"><asp:Button runat="server" ID="btncopy" Text="Copy SOAP" Style="color:white;width:176px;height:32px;border-radius:4px;background-color:#4d9b35" OnClick="btnCopySOAP_onClick" /></div>
                </div>

                <div class="row" style="padding-top:5px">
                    <div class="col-sm-3" style="padding-right:0px">
                        <div class="col-sm-12" style="padding-right:0px">
                            <label style="font-size:10px">Doctor</label>
                            <br />
                            <asp:Button runat="server" ID="btnsearchDoctor" Visible="false" OnClick="txtsearchDoctor_onChange" />
                            <asp:TextBox runat="server" ID="txtSearchDoctor" AutoPostBack="true" onkeypress="keypressdoctor()" placeholder="Search..." Style="width:100%;height: 32px;padding-left:5px" OnTextChanged="txtsearchDoctor_onChange" ></asp:TextBox>
                        </div>

                        <div class="col-sm-12" style="padding-top:10px;padding-right:0px">
                            <asp:GridView ID="gvw_doctor" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed" BorderColor="Black"
                                HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                                ShowHeaderWhenEmpty="True" DataKeyNames="EncounterId" EmptyDataText="No Data" ShowHeader="false"
                                AllowSorting="True">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="15%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton CssClass="stylink" ID="copyitem" runat="server" OnClick="btngetitem_onClick" ><%# Eval("AdmissionDate") %>&nbsp<%# Eval("AdmissionNo") %><br /><%# Eval("DoctorName") %><br /><%# Eval("SpecialtyName") %></asp:LinkButton>
                                            <br />
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
                        </div>
                    </div>
                    <div class="col-sm-9">
                        <div style="border:1px solid #707070;padding:10px">
                            <div>
                                <div style="padding-bottom:10px">
                                    <label class="itemlab"><asp:CheckBox runat="server" ID="chkSubjective" CssClass="mycheckbox" Style="font-weight:bold;font-size:12px" Text="SUBJECTIVE" /></label>
                                <br />
                                </div>
                                <div>
                                    <label style="padding-left:50px">Chief Complaint : </label><asp:Label runat="server" Style="padding-left:5px;" ID="lblChiefComplaint" Text=""></asp:Label>
                                    <br />
                                </div>
                                    <label style="padding-left:50px">Anamnesis : </label><asp:Label runat="server" Style="padding-left:5px;" ID="lblAnamnesis" Text=""></asp:Label>
                            </div>

                            <div style="padding-top:10px">
                                <label class="itemlab"><asp:CheckBox runat="server" ID="chkObjective" CssClass="mycheckbox" Style="font-weight:bold;font-size:12px" Text="OBJECTIVE" /></label>
                                <br />
                                <asp:Label runat="server" style="padding-left:52px" ID="lblobjective" Text=""></asp:Label>
                            </div>

                            <div style="padding-top:10px">
                                <label class="itemlab"><asp:CheckBox runat="server" ID="chkAssessment" CssClass="mycheckbox" Style="font-weight:bold;font-size:12px" Text="ASSESSMENT" /></label>
                                <br />
                                <asp:Label runat="server" style="padding-left:52px" ID="lblAssessment" Text=""></asp:Label>
                            </div>

                            <div style="padding-top:10px">
                                <label class="itemlab"><asp:CheckBox runat="server" ID="chkPlanning" CssClass="mycheckbox" Style="font-weight:bold;font-size:12px" Text="PLANNING & PROCEDURE" /></label>
                                <br />
                                <asp:Label runat="server" style="padding-left:52px" ID="lblPlanning" Text=""></asp:Label>
                            </div>

                            <div style="padding-top:10px">
                                <label class="itemlab"><asp:CheckBox runat="server" ID="chkLab" CssClass="mycheckbox" Style="font-weight:bold;font-size:12px" Text="LABORATORY" /></label>
                                <br />
                                <ul style="padding-left:52px">
                                <asp:Repeater runat="server" ID="rptLab">
                                    <ItemTemplate>
                                        <li>
                                            <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("name") %>' Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false" />
                                        </li>

                                    </ItemTemplate>
                                </asp:Repeater>
                                </ul>
                            </div>

                            <div style="padding-top:10px">
                                <label class="itemlab"><asp:CheckBox runat="server" ID="chkRad" CssClass="mycheckbox" Style="font-weight:bold;font-size:12px" Text="RADIOLOGY" /></label>
                                <br />
                                <ul style="padding-left:52px">
                                <asp:Repeater runat="server" ID="rptRad">
                                    <ItemTemplate>
                                        <li>
                                            <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("name") %>' Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false" />
                                        </li>

                                    </ItemTemplate>
                                </asp:Repeater>
                                </ul>
                            </div>

                            <div style="padding-top:10px">
                                <label class="itemlab"><asp:CheckBox runat="server" ID="chkDrugs" CssClass="mycheckbox" Style="font-weight:bold;font-size:12px" Text="DRUGS PRESCRIPTION" /></label>
                                <br />
                                <ul style="padding-left:52px">
                                <asp:Repeater runat="server" ID="rptDrugs">
                                    <ItemTemplate>
                                        <li>
                                            <asp:Label ID="NameLabel" runat="server" Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false"><%#Eval("item_name") %>&nbsp&nbsp<%#Eval("quantity") %>&nbsp<%#Eval("uom_code") %>,&nbsp<%#Eval("frequency_code") %>,&nbsp<%#Eval("remarks") %>,&nbsp<%#Eval("administration_route_code") %>,&nbsp<%#Eval("is_routine") %></asp:Label>
                                        </li>

                                    </ItemTemplate>
                                </asp:Repeater>
                                </ul>
                            </div>

                            <div style="padding-top:10px">
                                <label class="itemlab"><asp:CheckBox runat="server" ID="chkCompound" CssClass="mycheckbox" Style="font-weight:bold;font-size:12px" Text="COMPOUND" /></label>
                                
                                <ul style="padding-left:52px">
                                <asp:Repeater runat="server" ID="rptCompound" OnItemDataBound="Repeater1_ItemDataBound">
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
                                </asp:Repeater>
                                </ul>
                            </div>


                            <div style="padding-top:10px">
                                <label class="itemlab"><asp:CheckBox runat="server" ID="chkConsumables" CssClass="mycheckbox" Style="font-weight:bold;font-size:12px" Text="CONSUMABLES" /></label>
                                <br />
                                <ul style="padding-left:52px">
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
            </div>
        </div>
    </div>
    </ContentTemplate></asp:UpdatePanel>
</div>
    <%-- ========================================================== MODAL COPY SOAP ================================================================ --%>

        
    <%-- ============================================= MODAL SUBMIT AND SIGN ============================================== --%>
<div class="modal fade" id="modalsubmit" tabindex="-1"  role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
    <div class="modal-dialog" style="position:fixed;top: 5%;left: 25%;width:40%;">
        <div class="modal-content" style="border-radius: 6px;box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
            <div class="modal-header" style="height:30px;padding-top:5px;padding-bottom:5px">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h5 class="modal-title"><asp:Label ID="Label1" style="font-family:Helvetica;font-weight:bold;font-size:14px" runat="server" Text="Submit & Sign"></asp:Label></h5>
            </div>
            <div class="modal-body">
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:12px;font-weight:bold;font-family:Helvetica">Referral</label>
                        <label style="font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal;padding-left:10px"> <asp:RadioButton runat="server" GroupName="referral"  Value="0" id="rbreferral1" Checked="true" onclick="disableref()"/> No </label>
                    </div>
                    <div class="col-sm-2" style="padding-right:0px;margin-right:0px">
                        <label style="font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="referral"  Value="1" id="rbreferral2" onclick="enableref()" /> Yes </label>
                    </div>
                    <div class="col-sm-3" style="padding-left:0px;margin-left:0px">
                        <asp:TextBox runat="server" style="font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal;height:23px;width: 240px;border-radius: 2px;border: solid 1px #cdced9;" ID="txtreferal" Disabled="true"> </asp:TextBox>
                    </div>
                </div>
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:10px;font-family:Helvetica">Consultation fee</label>
                    </div>
                </div>
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-11">
                        <%--<asp:TextBox runat="server" style="font-size:14px;font-weight:bold;font-family:Helvetica;height:32px;width:170px;border-radius: 4px;border: solid 1px #9293a0;padding-left:11px">RP 300.000</asp:TextBox>--%>
                        <asp:DropDownList ID="ddl_consultationfee" runat="server" style="font-size:12px;font-weight:bold;font-family:Helvetica;height:32px;width:100%;max-width:100%;border-radius: 4px;border: solid 1px #9293a0;padding-left:11px" onchange="normalcharges();"></asp:DropDownList>
                    </div>
                </div>
                <div>&nbsp</div>
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:10px;font-weight:bold;font-family:Helvetica">Special Price</label>
                    </div>
                </div>
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="price"  Value="0" id="rbPrice1" Checked="true" onclick="normalcharges();" /> Normal Price </label>
                    </div>
                </div>
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="price"  Value="1" id="rbPrice2" onclick="freecharges();" /> Free of Charge </label>
                    </div>
                </div>

                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="price"  Value="2" id="rbPrice3" onclick="enabledisc()" /> Discount </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox runat="server" style="text-align:right;font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal;height:23px;width: 150px;border-radius: 2px;border: solid 1px #cdced9;" Disabled="true" ID="txtDiscount" onkeypress="return CheckNumericnumber();" onblur="CalculateTotalFee();"></asp:TextBox>
                    </div>
                </div>

                <div>&nbsp</div>
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:10px;font-family:Helvetica">Procedure notes</label>
                    </div>
                </div>
                <div class="row" style="padding-left:5%;padding-right:5%">
                    <div class="col-sm-12">
                        <asp:TextBox runat="server" style="outline-color:gray;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif" placeholder="Type here..."  BorderColor="gray" ID="txtProcedure" TextMode="MultiLine" Rows="3" />
                    </div>
                </div>

                <div>&nbsp</div>
                <div class="row" style="padding-left:5%;padding-right:5%">
                    <div class="col-sm-6"></div>
                    <div class="col-sm-6" style="text-align:right">
                        <label style="font-size:12px;font-family:Helvetica;text-align:right">Total Fee:</label><asp:TextBox runat="server" ID="txttotalfee" BorderColor="Transparent" style="outline-color:transparent;font-weight:bold;font-size:26px;font-family:Helvetica;width:100px;text-align:right" ></asp:TextBox>
                    </div>
                </div>

                <div>&nbsp</div>
                <div class="row" style="padding-left:5%;padding-right:5%">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-8" style="text-align:right">
                        <asp:Button runat="server" style="width: 112px;height: 32px;border-radius: 4px;background-color: #171717;color:white" data-dismiss="modal" Text="Cancel" />
                        <asp:Button runat="server" style="width: 112px;height: 32px;border-radius: 4px;background-color: #c43d32;color:white" Text="Submit & Sign" OnClientClick="javascript:shouldsubmit=true" OnClick="btnSubmit_click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<%-- ============================================= END MODAL SUBMIT AND SIGN ============================================== --%>
<%-- ============================================= MODAL SUBMIT AND SIGN DISABLE ============================================== --%>
<div class="modal fade" id="modalsubmitDisable" tabindex="-1"  role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate>
    <div class="modal-dialog" style="position:fixed;top: 5%;left: 25%;width:40%;">
        <div class="modal-content" style="border-radius: 6px;box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.24);">
            <div class="modal-header" style="height:30px;padding-top:5px;padding-bottom:5px">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h5 class="modal-title"><asp:Label ID="Label2" style="font-family:Helvetica;font-weight:bold;font-size:14px" runat="server" Text="Submit & Sign"></asp:Label></h5>
            </div>
            <div class="modal-body">
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:12px;font-weight:bold;font-family:Helvetica">Referral</label>
                        <label style="font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal;padding-left:10px"> <asp:RadioButton runat="server" GroupName="referraldis"  Value="0" id="RadioButton1" Checked="true" Enabled="false"/> No </label>
                    </div>
                    <div class="col-sm-2" style="padding-right:0px;margin-right:0px">
                        <label style="font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="referraldis"  Value="1" id="RadioButton2" Enabled="false" /> Yes </label>
                    </div>
                    <div class="col-sm-3" style="padding-left:0px;margin-left:0px">
                        <asp:TextBox runat="server" style="font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal;height:23px;width: 240px;border-radius: 2px;border: solid 1px #cdced9;" ID="TextBox1" Disabled="true"> </asp:TextBox>
                    </div>
                </div>
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:10px;font-family:Helvetica">Consultation fee</label>
                    </div>
                </div>
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-11">
                        <asp:TextBox runat="server" style="font-size:14px;font-weight:bold;font-family:Helvetica;height:32px;width:100%;max-width:100%;border-radius: 4px;border: solid 1px #9293a0;padding-left:11px" ID="txtConsfee" ReadOnly="true"></asp:TextBox>
                        <%--<asp:DropDownList ID="DropDownList1" runat="server" style="font-size:14px;font-weight:bold;font-family:Helvetica;height:32px;width:170px;border-radius: 4px;border: solid 1px #9293a0;padding-left:11px"></asp:DropDownList>--%>
                    </div>
                </div>
                <div>&nbsp</div>
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:10px;font-weight:bold;font-family:Helvetica">Special Price</label>
                    </div>
                </div>
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="pricedis"  Value="0" id="rbNormal" Enabled="false" /> Normal Price </label>
                    </div>
                </div>
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="pricedis"  Value="1" id="rbFree" Enabled="false" /> Free of Charge </label>
                    </div>
                </div>

                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="pricedis"  Value="2" id="rbdisc" Enabled="false" /> Discount </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox runat="server" style="font-size:12px;font-family: Helvetica;color: #171717;font-weight:normal;height:23px;width: 150px;border-radius: 2px;border: solid 1px #cdced9;" Disabled="true" ID="txtdisc" onkeypress="return CheckNumericnumber();" onblur="CalculateTotalFee();"></asp:TextBox>
                    </div>
                </div>

                <div>&nbsp</div>
                <div class="row" style="padding-left:5%">
                    <div class="col-sm-3">
                        <label style="font-size:10px;font-family:Helvetica">Procedure notes</label>
                    </div>
                </div>
                <div class="row" style="padding-left:5%;padding-right:5%">
                    <div class="col-sm-12">
                        <asp:TextBox runat="server" style="outline-color:gray;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif" placeholder="Type here..."  BorderColor="gray" ID="txtnotes" TextMode="MultiLine" Rows="3" />
                    </div>
                </div>

                <div>&nbsp</div>
                <div class="row" style="padding-left:5%;padding-right:5%">
                    <div class="col-sm-6"></div>
                    <div class="col-sm-6" style="text-align:right">
                        <label style="font-size:12px;font-family:Helvetica;text-align:right">Total Fee:</label><asp:TextBox runat="server" ID="lbltotalfeedisable" BorderColor="Transparent" style="outline-color:transparent;font-weight:bold;font-size:26px;font-family:Helvetica;width:100px;text-align:right" ></asp:TextBox>
                    </div>
                </div>

                <div>&nbsp</div>
                <div class="row" style="padding-left:5%;padding-right:5%">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-8" style="text-align:right">
                        <Button runat="server" style="width: 112px;height: 32px;border-radius: 4px;background-color: #171717;color:white" data-dismiss="modal">Cancel</Button>
                        <asp:Button runat="server" style="width: 112px;height: 32px;border-radius: 4px;background-color: #c43d32;color:white" Text="Submit & Sign" OnClientClick="javascript:shouldsubmit=true" OnClick="btnSubmitDisable_click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    </ContentTemplate></asp:UpdatePanel>
</div>
<%-- ============================================= END MODAL SUBMIT AND SIGN DISABLE ============================================== --%>
    </body>

</asp:Content>