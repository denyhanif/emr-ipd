<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageOrderSet.aspx.cs" MasterPageFile="~/Site.master" Inherits="Form_General_ManageOrderSet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="ManageOrderSet" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .Initial {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left;
            /*background: url("../Images/InitialImage.png") no-repeat right top;*/
            background-color: #e7e8ef;
            color: Black;
            font-weight: bold;
            border-radius: 10px;
            border-radius: 6px 6px 0 0;
        }

            .Initial:hover {
                color: cornflowerblue;
                /*background: url("../Images/SelectedButton.png") no-repeat right top;*/
                background-color: #f4f4f4;
                border-radius: 6px 6px 0 0;
            }

        .Clicked {
            float: left;
            display: block;
            background-color: white;
            padding: 4px 18px 4px 18px;
            font-weight: bold;
            font-family: Tahoma;
            border-radius: 6px 6px 0 0;
        }

        .buttonAdd {
            position: absolute;
            left: 1100px;
            height: 32px;
            background: none;
            border: none;
            -webkit-border-radius: 4px 4px 4px 4px;
            -moz-border-radius: 4px 4px 4px 4px;
            border-radius: 4px 4px 4px 4px;
        }

        .shadows {
            border: 1px;
            border-radius: 10px;
            box-shadow: 0px 1px 5px #9293A0;
            margin-top: 0px;
        }

        .shadows-tab {
            border: 1px;
            border-radius: 50px;
            box-shadow: 0px 1px 5px #9293A0;
            margin-top: 0px;
        }

        .btn-circle {
            width: 25px;
            height: 25px;
            padding: 1px 1px;
            font-size: 20px;
            line-height: 1.1;
            font-weight: bold;
            border-radius: 25px;
        }

        .hidden {
            visibility: hidden;
        }
    </style>

    <script type="text/javascript">
        $(window).load(function () {
            $(".loadPage").fadeOut("slow");
        });

        function openDrugDeleteModal() {
            $('#deleteOrderSetDrugModal').modal('show');
        }

        function openCompoundDeleteModal() {
            $('#deleteOrderSetCompoundModal').modal('show');
        }
        function openLaboratoryDeleteModal() {
            $('#deleteOrderSetLaboratoryModal').modal('show');
        }

        function openSubjectCCDeleteModal() {
            $('#deleteOrderSetSubjectSSModal').modal('show');
        }

        function openSubjectADeleteModal() {
            $('#deleteOrderSetSubjectAModal').modal('show');
        }

        function openObjectiveDeleteModal() {
            $('#deleteOrderSetObjectiveModal').modal('show');
        }

        function openAnalysisDeleteModal() {
            $('#deleteOrderSetAnalysisModal').modal('show');
        }

        function openPlanningDeleteModal() {
            $('#deleteOrderSetPlanningModal').modal('show');
        }

        function switchBahasa() {
            var bahasa = document.getElementById('<%=HFisBahasa.ClientID%>').value;
            if (bahasa == "ENG") {
                document.getElementById('lblbhs_drugs').innerHTML = "Drugs";
                if (document.getElementById('lblbhs_compound') != null) {
                    document.getElementById('lblbhs_compound').innerHTML = "Compound";
                }
                document.getElementById('lblbhs_laboratory').innerHTML = "Laboratory";

                if (document.getElementById('lblbhs_drugsetname') != null) {
                    document.getElementById('lblbhs_drugsetname').innerHTML = "Order Set Name";
                    document.getElementById('lblbhs_drugdetail').innerHTML = "Detail";
                    document.getElementById('lblbhs_drugcreated').innerHTML = "Date Created";
                }

                if (document.getElementById('lblbhs_compsetname') != null) {
                    document.getElementById('lblbhs_compsetname').innerHTML = "Order Set Name";
                    document.getElementById('lblbhs_compdetail').innerHTML = "Detail";
                    document.getElementById('lblbhs_compcreated').innerHTML = "Date Created";
                }

                if (document.getElementById('lblbhs_labsetname') != null) {
                    document.getElementById('lblbhs_labsetname').innerHTML = "Order Set Name";
                    document.getElementById('lblbhs_labdetail').innerHTML = "Detail";
                    document.getElementById('lblbhs_labcreated').innerHTML = "Date Created";
                }
            }
            else if (bahasa == "IND") {
                document.getElementById('lblbhs_drugs').innerHTML = "Obat";
                if (document.getElementById('lblbhs_compound') != null) {
                    document.getElementById('lblbhs_compound').innerHTML = "Racikan";
                }
                document.getElementById('lblbhs_laboratory').innerHTML = "Laboratorium";

                if (document.getElementById('lblbhs_drugsetname') != null) {
                    document.getElementById('lblbhs_drugsetname').innerHTML = "Nama Paket";
                    document.getElementById('lblbhs_drugdetail').innerHTML = "Detil";
                    document.getElementById('lblbhs_drugcreated').innerHTML = "Tanggal Dibuat";
                }

                if (document.getElementById('lblbhs_compsetname') != null) {
                    document.getElementById('lblbhs_compsetname').innerHTML = "Nama Paket";
                    document.getElementById('lblbhs_compdetail').innerHTML = "Detil";
                    document.getElementById('lblbhs_compcreated').innerHTML = "Tanggal Dibuat";
                }

                if (document.getElementById('lblbhs_labsetname') != null) {
                    document.getElementById('lblbhs_labsetname').innerHTML = "Nama Paket";
                    document.getElementById('lblbhs_labdetail').innerHTML = "Detil";
                    document.getElementById('lblbhs_labcreated').innerHTML = "Tanggal Dibuat";
                }
            }
        }
    </script>

    <asp:HiddenField ID="HFisBahasa" runat="server" />

    <div class="row" style="margin: 15px; margin-top: 15px; background-color:white; border-radius: 7px 7px 7px 7px; font-family:Arial, Helvetica, sans-serif">

        <div class="col-sm-12" style="padding: 0px;">
            <div class="col-sm-6" id="header_form" runat="server">
                <h4>Manage Order Set</h4>
            </div>
             <div class="col-sm-6" style="text-align:right; padding-top:5px; padding-bottom:5px;">
                 <asp:LinkButton runat="server" CssClass="itemsign btn btn-default" PostBackUrl="~/Form/General/OrderSet/OrderSetDetail.aspx"><span><img style="width:20px; height:20px" src="../../../Images/Icon/ic_add.svg" /></span><b> Create Order Set </b></asp:LinkButton>
             </div>
        </div>

        <div class="col-sm-12" style="padding-left: 0px; padding-right: 0px;">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table style="width: 100%; align-content: center;">
                        <tr>
                            <td>
                                <div class="col-sm-12" style="background-color:#e7e8ef; padding-top:1%">
                                    <asp:LinkButton BorderStyle="None" ID="Tab1" CssClass="Initial .shadows-tab" runat="server" OnClick="Tab1_Click"> 
                                        <%--<label style="display: <%=setENG%>;"> Drugs </label> 
                                        <label style="display: <%=setIND%>;"> Obat </label> --%> 
                                        <label id="lblbhs_drugs"> Drugs </label> 
                                    </asp:LinkButton>
                                    <asp:LinkButton BorderStyle="None" ID="Tab2" CssClass="Initial .shadows-tab" runat="server" OnClick="Tab2_Click"> 
                                        <%--<label style="display: <%=setENG%>;"> Compound </label> 
                                        <label style="display: <%=setIND%>;"> Racikan </label>--%> 
                                        <label id="lblbhs_compound"> Compound </label>
                                    </asp:LinkButton>
                                    <asp:LinkButton BorderStyle="None" ID="Tab3" CssClass="Initial .shadows-tab" runat="server" OnClick="Tab3_Click"> 
                                        <%--<label style="display: <%=setENG%>;"> Laboratory </label> 
                                        <label style="display: <%=setIND%>;"> Laboratorium </label>--%> 
                                        <label id="lblbhs_laboratory"> Laboratory </label> 
                                    </asp:LinkButton>
                                    <asp:LinkButton BorderStyle="None" ID="Tab_S_cc" CssClass="Initial .shadows-tab" runat="server" OnClick="Tab_S_cc_Click"> 
                                        <label id="lblbhs_subjectivecc"> Subjective - Chief Complaint </label> 
                                    </asp:LinkButton>
                                    <asp:LinkButton BorderStyle="None" ID="Tab_S_a" CssClass="Initial .shadows-tab" runat="server" OnClick="Tab_S_a_Click"> 
                                        <label id="lblbhs_subjectivea"> Subjective - Anamnesis </label> 
                                    </asp:LinkButton>
                                    <asp:LinkButton BorderStyle="None" ID="Tab_Objective" CssClass="Initial .shadows-tab" runat="server" OnClick="Tab_Objective_Click"> 
                                        <label id="lblbhs_objective"> Objective </label> 
                                    </asp:LinkButton>
                                    <asp:LinkButton BorderStyle="None" ID="Tab_Analysis" CssClass="Initial .shadows-tab" runat="server" OnClick="Tab_Analysis_Click"> 
                                        <label id="lblbhs_analysis"> Assessment </label> 
                                    </asp:LinkButton>
                                    <asp:LinkButton BorderStyle="None" ID="Tab_Planning" CssClass="Initial .shadows-tab" runat="server" OnClick="Tab_Planning_Click"> 
                                        <label id="lblbhs_planning"> Planning </label> 
                                    </asp:LinkButton>
                                </div>
                                

                                <div class="col-sm-12 no-padding" style="min-height: calc(100vh - 165px);">
                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:MultiView ID="MainView" runat="server">
                                                <asp:View ID="View1" runat="server">
                                                    &nbsp;
                                                 <asp:GridView ID="drugsGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-condensed"
                                                     BorderColor="Transparent">
                                                     <Columns>
                                                         <asp:TemplateField>
                                                             <HeaderTemplate>
                                                                 <th style="height: 40px; width: 5%">
                                                                     <div class="pretty p-icon p-curve">
                                                                         <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" Text="" OnCheckedChanged="chkAll_CheckedChanged" />
                                                                         <div class="state p-success">
                                                                             <i class="icon fa fa-check"></i>
                                                                             <label style="font-size: 12px"></label>
                                                                         </div>
                                                                     </div>
                                                                 </th>
                                                                 <th style="height: 40px; width: 25%">
                                                                     <div style="color: blue; text-decoration: underline;">
                                                                         <asp:LinkButton ID="deletechkAll" runat="server" Visible="false" OnClientClick="openDrugDeleteModal();"> <label style="display: <%=setENG%>; color: red; text-decoration:underline; cursor:pointer;"> Delete Set </label> <label style="display: <%=setIND%>;"> Hapus Paket </label> </asp:LinkButton>
                                                                     </div>
                                                                     <asp:Label runat="server" ID="Drug1"> 
                                                                         <%--<label style="display: <%=setENG%>;"> Order Set Name </label> 
                                                                         <label style="display: <%=setIND%>;"> Nama Paket </label>--%>
                                                                         <label id="lblbhs_drugsetname"> Order Set Name </label> 
                                                                     </asp:Label>
                                                                 </th>
                                                                 <th style="height: 40px; width: 60%">
                                                                     <asp:Label runat="server" ID="Drug2"> 
                                                                         <%--<label style="display: <%=setENG%>;"> Detail </label> 
                                                                         <label style="display: <%=setIND%>;"> Detil </label> --%>
                                                                         <label id="lblbhs_drugdetail"> Detail </label> 
                                                                     </asp:Label>
                                                                 </th>
                                                                 <th style="height: 40px; width: 10%">
                                                                     <asp:Label runat="server" ID="Drug3"> 
                                                                         <%--<label style="display: <%=setENG%>;"> Date Created </label> 
                                                                         <label style="display: <%=setIND%>;"> Tanggal Dibuat </label>--%> 
                                                                         <label id="lblbhs_drugcreated"> Date Created </label> 
                                                                     </asp:Label>
                                                                 </th>
                                                             </HeaderTemplate>
                                                             <ItemTemplate>
                                                                 <td>
                                                                     <div class="pretty p-icon p-curve">
                                                                         <asp:CheckBox ID="chkDelete_Drug" runat="server" AutoPostBack="true" OnCheckedChanged="chkDelete_Drug_CheckedChanged" />
                                                                         <div class="state p-success">
                                                                             <i class="icon fa fa-check"></i>
                                                                             <label style="font-size: 12px"></label>
                                                                         </div>
                                                                     </div>
                                                                 </td>
                                                                 <td>
                                                                     <asp:HyperLink runat="server" ID="lnkDrug_Name" Text='<%# Bind("set_name") %>' Style="color: blue; text-decoration: underline;"
                                                                         NavigateUrl='<%# String.Format("~/Form/General/OrderSet/OrderSetDetail.aspx?nameOrderSet={0}&isCompound={1}&type={2}", Eval("set_name"),0,0) %>'></asp:HyperLink>
                                                                     <asp:HiddenField ID="hfOrderSetName" Value='<%# Bind("set_name")%>' runat="server" />
                                                                 </td>
                                                                 <td>
                                                                     <asp:Label ID="txtItem_list" runat="server" Text='<%# Bind("item_list")%>'></asp:Label>
                                                                     <asp:TextBox ID="txtId_DrugOrderset" runat="server" Text='<%# Bind("id")%>' Visible="false"></asp:TextBox>
                                                                 </td>
                                                                 <td>
                                                                     <asp:Label ID="txtCreated_Date1" runat="server" Text='<%# Convert.ToDateTime(Eval("created_date")).ToString("dd MMM yyyy") %>' />
                                                                     <asp:Label Visible="false" ID="txt_hf_create_date1" runat="server" Text='<%# Eval("created_date") %>' />
                                                                 </td>
                                                             </ItemTemplate>
                                                         </asp:TemplateField>
                                                     </Columns>
                                                 </asp:GridView>
                                                </asp:View>
                                                <asp:View ID="View2" runat="server">
                                                    &nbsp;
                                                 <asp:GridView ID="compoundGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-condensed"
                                                     BorderColor="Transparent" EmptyDataText="No Data">
                                                     <Columns>
                                                         <asp:TemplateField>
                                                             <HeaderTemplate>
                                                                 <th style="height: 40px; width: 5%">
                                                                     <div class="pretty p-icon p-curve">
                                                                         <asp:CheckBox ID="chkAllCompound" runat="server" AutoPostBack="true" Text="" OnCheckedChanged="chkAllCompound_CheckedChanged" />
                                                                         <div class="state p-success">
                                                                             <i class="icon fa fa-check"></i>
                                                                             <label style="font-size: 12px"></label>
                                                                         </div>
                                                                     </div>
                                                                 </th>
                                                                 <th style="height: 40px; width: 25%">
                                                                     <div style="color: blue; text-decoration: underline;">
                                                                         <asp:LinkButton ID="deletechkCompound" runat="server" Visible="false" OnClientClick="openCompoundDeleteModal();"> <label style="display: <%=setENG%>;"> Delete Set </label> <label style="display: <%=setIND%>;"> Hapus Paket </label> </asp:LinkButton>
                                                                     </div>
                                                                     <asp:Label runat="server" ID="Compound1"> 
                                                                         <%--<label style="display: <%=setENG%>;"> Order Set Name </label> 
                                                                         <label style="display: <%=setIND%>;"> Nama Paket </label>  --%>
                                                                         <label id="lblbhs_compsetname"> Order Set Name </label>
                                                                     </asp:Label>
                                                                 </th>
                                                                 <th style="height: 40px; width: 60%">
                                                                     <asp:Label runat="server" ID="Compound2"> 
                                                                         <%--<label style="display: <%=setENG%>;"> Detail </label> 
                                                                         <label style="display: <%=setIND%>;"> Detil </label>  --%>
                                                                         <label id="lblbhs_compdetail"> Detail </label> 
                                                                     </asp:Label>
                                                                 </th>
                                                                 <th style="height: 40px; width: 10%">
                                                                     <asp:Label runat="server" ID="Compound3"> 
                                                                         <%--<label style="display: <%=setENG%>;"> Date Created </label> 
                                                                         <label style="display: <%=setIND%>;"> Tanggal Dibuat </label>--%> 
                                                                         <label id="lblbhs_compcreated"> Date Created </label>
                                                                     </asp:Label>
                                                                 </th>
                                                             </HeaderTemplate>
                                                             <ItemTemplate>
                                                                 <td>
                                                                     <div class="pretty p-icon p-curve">
                                                                         <asp:CheckBox ID="chkDelete_Compound" runat="server" AutoPostBack="true" OnCheckedChanged="chkDelete_Compound_CheckedChanged" />
                                                                         <div class="state p-success">
                                                                             <i class="icon fa fa-check"></i>
                                                                             <label style="font-size: 12px"></label>
                                                                         </div>
                                                                     </div>
                                                                 </td>
                                                                 <td>
                                                                     <asp:HyperLink runat="server" ID="lnkCompound_Name" Text='<%# Bind("set_name") %>' Style="color: blue; text-decoration: underline;"
                                                                         NavigateUrl='<%# String.Format("~/Form/General/OrderSet/OrderSetDetail.aspx?nameOrderSet={0}&isCompound={1}&type={2}", Eval("set_name"),1,1) %>'></asp:HyperLink></td>
                                                                 <asp:HiddenField ID="hfCompoundSetName" Value='<%# Bind("set_name")%>' runat="server" />
                                                                 <td>
                                                                     <asp:Label ID="txtItem_Compoundlist" runat="server" Text='<%# Bind("item_list")%>'></asp:Label>
                                                                     <asp:TextBox ID="txtId_CompoundOrderset" runat="server" Text='<%# Bind("id")%>' Visible="false"></asp:TextBox>
                                                                 </td>
                                                                 <td>
                                                                     <asp:Label ID="txtCreated_Date2" runat="server" Text='<%# Convert.ToDateTime(Eval("created_date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                                                     <asp:Label Visible="false" ID="txt_hf_create_date2" runat="server" Text='<%# Eval("created_date") %>' />
                                                                 </td>
                                                             </ItemTemplate>
                                                         </asp:TemplateField>
                                                     </Columns>
                                                 </asp:GridView>
                                                </asp:View>

                                               <asp:View ID="View3" runat="server">
                                                    &nbsp;
                                                 <asp:GridView ID="laboratoryGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-condensed"
                                                      BorderColor="Transparent" EmptyDataText="No Data">
                                                      <Columns>
                                                          <asp:TemplateField>
                                                              <HeaderTemplate>
                                                                  <th style="height: 40px; width: 5%">
                                                                      <div class="pretty p-icon p-curve">
                                                                          <asp:CheckBox ID="chkAllLaboratory" runat="server" AutoPostBack="true" Text="" OnCheckedChanged="chkAllLaboratory_CheckedChanged" />
                                                                          <div class="state p-success">
                                                                              <i class="icon fa fa-check"></i>
                                                                              <label style="font-size: 12px"></label>
                                                                          </div>
                                                                      </div>
                                                                  </th>
                                                                  <th style="height: 40px; width: 25%">
                                                                      <div style="color: blue; text-decoration: underline;">
                                                                          <asp:LinkButton ID="deletechkLaboratory" runat="server" Visible="false" OnClientClick="openLaboratoryDeleteModal();"> <label style="display: <%=setENG%>; color: red; text-decoration:underline;"> Delete Set </label> <label style="display: <%=setIND%>;"> Hapus Paket </label>  </asp:LinkButton>
                                                                      </div>
                                                                      <asp:Label runat="server" ID="Laboratory1"> 
                                                                          
                                                                          <label id="lblbhs_labsetname"> Order Set Name </label>
                                                                      </asp:Label>
                                                                  </th>
                                                                  <th style="height: 40px; width: 60%">
                                                                      <asp:Label runat="server" ID="Laboratory2"> 
                                                                          
                                                                          <label id="lblbhs_labdetail"> Detail </label> 
                                                                      </asp:Label>
                                                                  </th>
                                                                  <th style="height: 40px; width: 10%">
                                                                      <asp:Label runat="server" ID="Laboratory3"> 
                                                                         
                                                                          <label id="lblbhs_labcreated"> Date Created </label> 
                                                                      </asp:Label>
                                                                  </th>
                                                              </HeaderTemplate>
                                                              <ItemTemplate>
                                                                  <td>
                                                                      <div class="pretty p-icon p-curve">
                                                                          <asp:CheckBox ID="chkDelete_Laboratory" runat="server" AutoPostBack="true" OnCheckedChanged="chkDelete_Laboratory_CheckedChanged" />
                                                                          <div class="state p-success">
                                                                              <i class="icon fa fa-check"></i>
                                                                              <label style="font-size: 12px"></label>
                                                                          </div>
                                                                      </div>
                                                                  </td>
                                                                  <td>
                                                                      <asp:HyperLink runat="server" ID="lnkCompound_Name" Text='<%# Bind("set_name") %>' Style="color: blue; text-decoration: underline;"
                                                                          NavigateUrl='<%# String.Format("~/Form/General/OrderSet/OrderSetDetail.aspx?nameOrderSet={0}&isCompound={1}&type={2}&orderSetId={3}", Eval("set_name"),0, 2, Eval("id") ) %>'></asp:HyperLink></td>
                                                                  <asp:HiddenField ID="hfLaboratorySetName" Value='<%# Bind("set_name")%>' runat="server" />
                                                                  <td>
                                                                      <asp:Label ID="txtItem_Laboratorylist" runat="server" Text='<%# Bind("item_list")%>'></asp:Label>
                                                                      <asp:TextBox ID="txtId_LaboratoryOrderset" runat="server" Text='<%# Bind("id")%>' Visible="false"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                      <asp:Label ID="txtCreated_Date_laboratory" runat="server" Text='<%# Convert.ToDateTime(Eval("created_date")).ToString("dd MMM yyyy") %>'></asp:Label>
                                                                      <asp:Label Visible="false" ID="txt_hf_create_date3" runat="server" Text='<%# Eval("created_date") %>' />
                                                                  </td>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                      </Columns>
                                                  </asp:GridView>
                                                </asp:View>

                                                <asp:View ID="View_S_cc" runat="server">
                                                    <asp:HiddenField ID="hfsubjectccMapping" Value='E851F782-8210-49EB-A074-F26C104F5DDF' runat="server" />
                                                    <div id="img_noData_S_cc" runat="server" style="display: none; text-align:center">
                                                        <div>
                                                            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 100px" />
                                                        </div>
                                                        <div>
                                                            <span>
                                                                <h3 style="font-weight: 700; color: #585A6F">
                                                                    <label>No Data </label>
                                                                </h3>
                                                            </span>
                                                            <span style="font-size: 14px; color: #585A6F">
                                                                <label>Please create new Subjective - Chief Complaint </label>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        &nbsp;
                                                        <asp:GridView ID="SubjectCCGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-condensed"
                                                          BorderColor="Transparent" EmptyDataText="No Data">
                                                          <Columns>
                                                              <asp:TemplateField>
                                                                  <HeaderTemplate>
                                                                      <th style="height: 40px; width: 5%">
                                                                          <div class="pretty p-icon p-curve">
                                                                              <asp:CheckBox ID="chkAllSubjectCC" runat="server" AutoPostBack="true" Text="" OnCheckedChanged="chkAllSubjectCC_CheckedChanged" />
                                                                              <div class="state p-success">
                                                                                  <i class="icon fa fa-check"></i>
                                                                                  <label style="font-size: 12px"></label>
                                                                              </div>
                                                                          </div>
                                                                      </th>
                                                                      <th style="height: 40px; width: 25%">
                                                                          <div style="color: blue; text-decoration: underline;">
                                                                              <asp:LinkButton ID="deletechkSubjectCC" runat="server" Visible="false" OnClientClick="openSubjectCCDeleteModal();"> <label style="display: <%=setENG%>; color: red; text-decoration:underline;"> Delete Set </label> <label style="display: <%=setIND%>;"> Hapus Paket </label>  </asp:LinkButton>
                                                                          </div>
                                                                          <asp:Label runat="server" ID="SubjectCC1"> 
                                                                              <label id="lblbhs_subcc1"> Template Name </label>
                                                                          </asp:Label>
                                                                      </th>
                                                                      <th style="height: 40px; width: 60%">
                                                                          <asp:Label runat="server" ID="SubjectCC2"> 
                                                                              <label id="lblbhs_subcc2"> Detail Value </label> 
                                                                          </asp:Label>
                                                                      </th>
                                                                      <th style="height: 40px; width: 10%">
                                                                          <asp:Label runat="server" ID="SubjectCC3"> 
                                                                              <label id="lblbhs_subcc3"> Date Created </label> 
                                                                          </asp:Label>
                                                                      </th>
                                                                  </HeaderTemplate>
                                                                  <ItemTemplate>
                                                                      <td>
                                                                          <div class="pretty p-icon p-curve">
                                                                              <asp:CheckBox ID="chkDelete_SubjectCC" runat="server" AutoPostBack="true" OnCheckedChanged="chkDelete_SubjectCC_CheckedChanged" />
                                                                              <div class="state p-success">
                                                                                  <i class="icon fa fa-check"></i>
                                                                                  <label style="font-size: 12px"></label>
                                                                              </div>
                                                                          </div>
                                                                      </td>
                                                                      <td>
                                                                         <asp:HyperLink runat="server" ID="linkSubjectCC_name" Text='<%# Bind("template_name") %>' Style="color: blue; text-decoration: underline;" NavigateUrl='<%# String.Format("~/Form/General/OrderSet/OrderSetDetail.aspx?nameOrderSet={0}&isCompound={1}&type={2}&selected={3}&mapping={4}", Eval("template_name"),0,3,3,Eval("soap_mapping_id")) %>'></asp:HyperLink>
                                                                         <asp:HiddenField ID="hfsubjectccName" Value='<%# Bind("template_name")%>' runat="server" />
                                                                      </td>
                                                                      <td>
                                                                          <asp:Label ID="txtItem_subjectcclist" runat="server" Text='<%# Bind("template_remarks")%>'></asp:Label>
                                                                      </td>
                                                                      <td>
                                                                          <asp:Label ID="txtCreated_Date_Subject_CC" runat="server" Text='<%# Convert.ToDateTime(Eval("created_date")).ToString("dd MMM yyyy") %>'></asp:Label>
                                                                      </td>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                          </Columns>
                                                      </asp:GridView>
                                                    </div>
                                                 
                                                </asp:View>

                                                <asp:View ID="View_S_a" runat="server">
                                                    <asp:HiddenField ID="hfsubjectaMapping" Value='2874A832-8503-4CAD-B5DD-535775E94AC0' runat="server" />
                                                    <div id="img_noData_S_a" runat="server" style="display: none; text-align:center">
                                                        <div>
                                                            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 100px" />
                                                        </div>
                                                        <div>
                                                            <span>
                                                                <h3 style="font-weight: 700; color: #585A6F">
                                                                    <label>No Data </label>
                                                                </h3>
                                                            </span>
                                                            <span style="font-size: 14px; color: #585A6F">
                                                                <label>Please create new Subjective - Anamnesis </label>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        &nbsp;
                                                        <asp:GridView ID="SubjectAGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-condensed"
                                                          BorderColor="Transparent" EmptyDataText="No Data">
                                                          <Columns>
                                                              <asp:TemplateField>
                                                                  <HeaderTemplate>
                                                                      <th style="height: 40px; width: 5%">
                                                                          <div class="pretty p-icon p-curve">
                                                                              <asp:CheckBox ID="chkAllSubjectA" runat="server" AutoPostBack="true" Text="" OnCheckedChanged="chkAllSubjectA_CheckedChanged" />
                                                                              <div class="state p-success">
                                                                                  <i class="icon fa fa-check"></i>
                                                                                  <label style="font-size: 12px"></label>
                                                                              </div>
                                                                          </div>
                                                                      </th>
                                                                      <th style="height: 40px; width: 25%">
                                                                          <div style="color: blue; text-decoration: underline;">
                                                                              <asp:LinkButton ID="deletechkSubjectA" runat="server" Visible="false" OnClientClick="openSubjectADeleteModal();"> <label style="display: <%=setENG%>; color: red; text-decoration:underline;"> Delete Set </label> <label style="display: <%=setIND%>;"> Hapus Paket </label>  </asp:LinkButton>
                                                                          </div>
                                                                          <asp:Label runat="server" ID="SubjectA1"> 
                                                                              <label id="lblbhs_subcc1"> Template Name </label>
                                                                          </asp:Label>
                                                                      </th>
                                                                      <th style="height: 40px; width: 60%">
                                                                          <asp:Label runat="server" ID="SubjectA2"> 
                                                                              <label id="lblbhs_subcc2"> Detail Value </label> 
                                                                          </asp:Label>
                                                                      </th>
                                                                      <th style="height: 40px; width: 10%">
                                                                          <asp:Label runat="server" ID="SubjectA3"> 
                                                                              <label id="lblbhs_subcc3"> Date Created </label> 
                                                                          </asp:Label>
                                                                      </th>
                                                                  </HeaderTemplate>
                                                                  <ItemTemplate>
                                                                      <td>
                                                                          <div class="pretty p-icon p-curve">
                                                                              <asp:CheckBox ID="chkDelete_SubjectA" runat="server" AutoPostBack="true" OnCheckedChanged="chkDelete_SubjectA_CheckedChanged" />
                                                                              <div class="state p-success">
                                                                                  <i class="icon fa fa-check"></i>
                                                                                  <label style="font-size: 12px"></label>
                                                                              </div>
                                                                          </div>
                                                                      </td>
                                                                      <td>
                                                                         <asp:HyperLink runat="server" ID="linkSubjectA_name" Text='<%# Bind("template_name") %>' Style="color: blue; text-decoration: underline;" NavigateUrl='<%# String.Format("~/Form/General/OrderSet/OrderSetDetail.aspx?nameOrderSet={0}&isCompound={1}&type={2}&selected={3}&mapping={4}", Eval("template_name"),0,3,4,Eval("soap_mapping_id")) %>'></asp:HyperLink>
                                                                          <asp:HiddenField ID="hfsubjectaName" Value='<%# Bind("template_name")%>' runat="server" />
                                                                      </td>
                                                                      <td>
                                                                          <asp:Label ID="txtItem_subjectalist" runat="server" Text='<%# Bind("template_remarks")%>'></asp:Label>
                                                                      </td>
                                                                      <td>
                                                                          <asp:Label ID="txtCreated_Date_SubjectA" runat="server" Text='<%# Convert.ToDateTime(Eval("created_date")).ToString("dd MMM yyyy") %>'></asp:Label>
                                                                      </td>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                          </Columns>
                                                      </asp:GridView>
                                                    </div>
                                                </asp:View>

                                                <asp:View ID="View_objective" runat="server">
                                                    <asp:HiddenField ID="hfObjectiveMapping" Value='7218971C-E89F-4172-AE3C-B7FB855C1D6D' runat="server" />
                                                    <div id="img_noData_objective" runat="server" style="display: none; text-align:center">
                                                        <div>
                                                            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 100px" />
                                                        </div>
                                                        <div>
                                                            <span>
                                                                <h3 style="font-weight: 700; color: #585A6F">
                                                                    <label>No Data </label>
                                                                </h3>
                                                            </span>
                                                            <span style="font-size: 14px; color: #585A6F">
                                                                <label>Please create new Objective </label>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        &nbsp;
                                                        <asp:GridView ID="ObjectiveGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-condensed"
                                                          BorderColor="Transparent" EmptyDataText="No Data">
                                                          <Columns>
                                                              <asp:TemplateField>
                                                                  <HeaderTemplate>
                                                                      <th style="height: 40px; width: 5%">
                                                                          <div class="pretty p-icon p-curve">
                                                                              <asp:CheckBox ID="chkAllObjective" runat="server" AutoPostBack="true" Text="" OnCheckedChanged="chkAllObjective_CheckedChanged" />
                                                                              <div class="state p-success">
                                                                                  <i class="icon fa fa-check"></i>
                                                                                  <label style="font-size: 12px"></label>
                                                                              </div>
                                                                          </div>
                                                                      </th>
                                                                      <th style="height: 40px; width: 25%">
                                                                          <div style="color: blue; text-decoration: underline;">
                                                                              <asp:LinkButton ID="deletechkObjective" runat="server" Visible="false" OnClientClick="openObjectiveDeleteModal();"> <label style="display: <%=setENG%>; color: red; text-decoration:underline;"> Delete Set </label> <label style="display: <%=setIND%>;"> Hapus Paket </label>  </asp:LinkButton>
                                                                          </div>
                                                                          <asp:Label runat="server" ID="Objective1"> 
                                                                              <label id="lblbhs_subcc1"> Template Name </label>
                                                                          </asp:Label>
                                                                      </th>
                                                                      <th style="height: 40px; width: 60%">
                                                                          <asp:Label runat="server" ID="Objective2"> 
                                                                              <label id="lblbhs_subcc2"> Detail Value </label> 
                                                                          </asp:Label>
                                                                      </th>
                                                                      <th style="height: 40px; width: 10%">
                                                                          <asp:Label runat="server" ID="Objective3"> 
                                                                              <label id="lblbhs_subcc3"> Date Created </label> 
                                                                          </asp:Label>
                                                                      </th>
                                                                  </HeaderTemplate>
                                                                  <ItemTemplate>
                                                                      <td>
                                                                          <div class="pretty p-icon p-curve">
                                                                              <asp:CheckBox ID="chkDelete_Objective" runat="server" AutoPostBack="true" OnCheckedChanged="chkDelete_Objective_CheckedChanged" />
                                                                              <div class="state p-success">
                                                                                  <i class="icon fa fa-check"></i>
                                                                                  <label style="font-size: 12px"></label>
                                                                              </div>
                                                                          </div>
                                                                      </td>
                                                                      <td>
                                                                         <asp:HyperLink runat="server" ID="linkObjective_name" Text='<%# Bind("template_name") %>' Style="color: blue; text-decoration: underline;" NavigateUrl='<%# String.Format("~/Form/General/OrderSet/OrderSetDetail.aspx?nameOrderSet={0}&isCompound={1}&type={2}&selected={3}&mapping={4}", Eval("template_name"),0,3,5,Eval("soap_mapping_id")) %>'></asp:HyperLink>
                                                                          <asp:HiddenField ID="hfobjectiveName" Value='<%# Bind("template_name")%>' runat="server" />
                                                                      </td>
                                                                      <td>
                                                                          <asp:Label ID="txtItem_Objectivelist" runat="server" Text='<%# Bind("template_remarks")%>'></asp:Label>
                                                                      </td>
                                                                      <td>
                                                                          <asp:Label ID="txtCreated_Date_Objective" runat="server" Text='<%# Convert.ToDateTime(Eval("created_date")).ToString("dd MMM yyyy") %>'></asp:Label>
                                                                      </td>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                          </Columns>
                                                      </asp:GridView>
                                                    </div>
                                                </asp:View>

                                                <asp:View ID="View_analysis" runat="server">
                                                    <asp:HiddenField ID="hfAnalysisMapping" Value='D24D0881-7C06-4563-BF75-3A20B843DC47' runat="server" />
                                                    <div id="img_noData_analysis" runat="server" style="display: none; text-align:center">
                                                        <div>
                                                            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 100px" />
                                                        </div>
                                                        <div>
                                                            <span>
                                                                <h3 style="font-weight: 700; color: #585A6F">
                                                                    <label>No Data </label>
                                                                </h3>
                                                            </span>
                                                            <span style="font-size: 14px; color: #585A6F">
                                                                <label>Please create new Analysis </label>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        &nbsp;
                                                        <asp:GridView ID="AnalysisGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-condensed"
                                                          BorderColor="Transparent" EmptyDataText="No Data">
                                                          <Columns>
                                                              <asp:TemplateField>
                                                                  <HeaderTemplate>
                                                                      <th style="height: 40px; width: 5%">
                                                                          <div class="pretty p-icon p-curve">
                                                                              <asp:CheckBox ID="chkAllAnalysis" runat="server" AutoPostBack="true" Text="" OnCheckedChanged="chkAllAnalysis_CheckedChanged" />
                                                                              <div class="state p-success">
                                                                                  <i class="icon fa fa-check"></i>
                                                                                  <label style="font-size: 12px"></label>
                                                                              </div>
                                                                          </div>
                                                                      </th>
                                                                      <th style="height: 40px; width: 25%">
                                                                          <div style="color: blue; text-decoration: underline;">
                                                                              <asp:LinkButton ID="deletechkAnalysis" runat="server" Visible="false" OnClientClick="openAnalysisDeleteModal();"> <label style="display: <%=setENG%>; color: red; text-decoration:underline;"> Delete Set </label> <label style="display: <%=setIND%>;"> Hapus Paket </label>  </asp:LinkButton>
                                                                          </div>
                                                                          <asp:Label runat="server" ID="Analysis1"> 
                                                                              <label id="lblbhs_subcc1"> Template Name </label>
                                                                          </asp:Label>
                                                                      </th>
                                                                      <th style="height: 40px; width: 60%">
                                                                          <asp:Label runat="server" ID="Analysis2"> 
                                                                              <label id="lblbhs_subcc2"> Detail Value </label> 
                                                                          </asp:Label>
                                                                      </th>
                                                                      <th style="height: 40px; width: 10%">
                                                                          <asp:Label runat="server" ID="Analysis3"> 
                                                                              <label id="lblbhs_subcc3"> Date Created </label> 
                                                                          </asp:Label>
                                                                      </th>
                                                                  </HeaderTemplate>
                                                                  <ItemTemplate>
                                                                      <td>
                                                                          <div class="pretty p-icon p-curve">
                                                                              <asp:CheckBox ID="chkDelete_Analysis" runat="server" AutoPostBack="true" OnCheckedChanged="chkDelete_Analysis_CheckedChanged" />
                                                                              <div class="state p-success">
                                                                                  <i class="icon fa fa-check"></i>
                                                                                  <label style="font-size: 12px"></label>
                                                                              </div>
                                                                          </div>
                                                                      </td>
                                                                      <td>
                                                                         <asp:HyperLink runat="server" ID="linkAnalysis_name" Text='<%# Bind("template_name") %>' Style="color: blue; text-decoration: underline;" NavigateUrl='<%# String.Format("~/Form/General/OrderSet/OrderSetDetail.aspx?nameOrderSet={0}&isCompound={1}&type={2}&selected={3}&mapping={4}", Eval("template_name"),0,3,6,Eval("soap_mapping_id")) %>'></asp:HyperLink>
                                                                          <asp:HiddenField ID="hfanalysisName" Value='<%# Bind("template_name")%>' runat="server" />
                                                                      </td>
                                                                      <td>
                                                                          <asp:Label ID="txtItem_Analysislist" runat="server" Text='<%# Bind("template_remarks")%>'></asp:Label>
                                                                          <asp:HiddenField ID="hd_analysis_value" runat="server" Value='<%# Bind("template_value")%>' />
                                                                      </td>
                                                                      <td>
                                                                          <asp:Label ID="txtCreated_Date_Analysis" runat="server" Text='<%# Convert.ToDateTime(Eval("created_date")).ToString("dd MMM yyyy") %>'></asp:Label>
                                                                      </td>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                          </Columns>
                                                      </asp:GridView>
                                                    </div>
                                                </asp:View>

                                                <asp:View ID="View_planning" runat="server">
                                                    <asp:HiddenField ID="hfPlanning_Mapping" Value='337A371F-BAF5-424A-BDC5-C320C277CAC6' runat="server" />
                                                    <div id="img_noData_planning" runat="server" style="display: none; text-align:center">
                                                        <div>
                                                            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 100px" />
                                                        </div>
                                                        <div>
                                                            <span>
                                                                <h3 style="font-weight: 700; color: #585A6F">
                                                                    <label>No Data </label>
                                                                </h3>
                                                            </span>
                                                            <span style="font-size: 14px; color: #585A6F">
                                                                <label>Please create new Planning </label>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        &nbsp;
                                                        <asp:GridView ID="PlanningGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-condensed"
                                                          BorderColor="Transparent" EmptyDataText="No Data">
                                                          <Columns>
                                                              <asp:TemplateField>
                                                                  <HeaderTemplate>
                                                                      <th style="height: 40px; width: 5%">
                                                                          <div class="pretty p-icon p-curve">
                                                                              <asp:CheckBox ID="chkAllPlanning" runat="server" AutoPostBack="true" Text="" OnCheckedChanged="chkAllPlanning_CheckedChanged" />
                                                                              <div class="state p-success">
                                                                                  <i class="icon fa fa-check"></i>
                                                                                  <label style="font-size: 12px"></label>
                                                                              </div>
                                                                          </div>
                                                                      </th>
                                                                      <th style="height: 40px; width: 25%">
                                                                          <div style="color: blue; text-decoration: underline;">
                                                                              <asp:LinkButton ID="deletechkPlanning" runat="server" Visible="false" OnClientClick="openPlanningDeleteModal();"> <label style="display: <%=setENG%>; color: red; text-decoration:underline;"> Delete Set </label> <label style="display: <%=setIND%>;"> Hapus Paket </label>  </asp:LinkButton>
                                                                              <asp:HiddenField ID="hfplanningName" Value='<%# Bind("template_name")%>' runat="server" />
                                                                          </div>
                                                                          <asp:Label runat="server" ID="Planning1"> 
                                                                              <label id="lblbhs_subcc1"> Template Name </label>
                                                                          </asp:Label>
                                                                      </th>
                                                                      <th style="height: 40px; width: 60%">
                                                                          <asp:Label runat="server" ID="Planning2"> 
                                                                              <label id="lblbhs_subcc2"> Detail Value </label> 
                                                                          </asp:Label>
                                                                      </th>
                                                                      <th style="height: 40px; width: 10%">
                                                                          <asp:Label runat="server" ID="Planning3"> 
                                                                              <label id="lblbhs_subcc3"> Date Created </label> 
                                                                          </asp:Label>
                                                                      </th>
                                                                  </HeaderTemplate>
                                                                  <ItemTemplate>
                                                                      <td>
                                                                          <div class="pretty p-icon p-curve">
                                                                              <asp:CheckBox ID="chkDelete_Planning" runat="server" AutoPostBack="true" OnCheckedChanged="chkDelete_Planning_CheckedChanged" />
                                                                              <div class="state p-success">
                                                                                  <i class="icon fa fa-check"></i>
                                                                                  <label style="font-size: 12px"></label>
                                                                              </div>
                                                                          </div>
                                                                      </td>
                                                                      <td>
                                                                         <asp:HyperLink runat="server" ID="linkPlanning_name" Text='<%# Bind("template_name") %>' Style="color: blue; text-decoration: underline;" NavigateUrl='<%# String.Format("~/Form/General/OrderSet/OrderSetDetail.aspx?nameOrderSet={0}&isCompound={1}&type={2}&selected={3}&mapping={4}", Eval("template_name"),0,3,7,Eval("soap_mapping_id")) %>'></asp:HyperLink>
                                                                          <asp:HiddenField ID="hfplanningName" Value='<%# Bind("template_name")%>' runat="server" />
                                                                      </td>
                                                                      <td>
                                                                          <asp:Label ID="txtItem_Planninglist" runat="server" Text='<%# Bind("template_remarks")%>'></asp:Label>
                                                                          <asp:HiddenField ID="hd_planning_value" runat="server" Value='<%# Bind("template_value")%>' />
                                                                      </td>
                                                                      <td>
                                                                          <asp:Label ID="txtCreated_Date_Planning" runat="server" Text='<%# Convert.ToDateTime(Eval("created_date")).ToString("dd MMM yyyy") %>'></asp:Label>
                                                                      </td>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                          </Columns>
                                                      </asp:GridView>
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade" id="deleteOrderSetDrugModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 30%;" runat="server">
            <div class="modal-content" style="border-radius: 7px">
                <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="text-align: center">
                        <asp:Label ID="lblModalTitle" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server" Text="Delete Order Set"></asp:Label></h4>
                </div>

                <div class="modal-body" style="background-color: white; text-align: center">
                    <h5>This action cannot be undone. &nbsp;</h5>
                    <h5>Delete Order Set ?</h5>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                    <asp:Button ID="btnDel_Drugs" aria-hidden="true" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="btnDel_Drugs_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="deleteOrderSetCompoundModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 20%;" runat="server">
            <div class="modal-content" style="border-radius: 7px">
                <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="text-align: center">
                        <asp:Label ID="Label1" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server" Text="Delete Order Set"></asp:Label></h4>
                </div>

                <div class="modal-body" style="background-color: white; text-align: center">
                    <h5>This action cannot be undone. &nbsp;</h5>
                    <h5>Delete Order Set ?</h5>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                    <asp:Button ID="btnDel_Compound" aria-hidden="true" OnClientClick="this.disabled = true; this.value = 'Loading...';"
                        UseSubmitBehavior="false" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="btnDel_Compound_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="deleteOrderSetLaboratoryModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 20%;" runat="server">
            <div class="modal-content" style="border-radius: 7px">
                <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="text-align: center">
                        <asp:Label ID="Label2" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server" Text="Delete Order Set"></asp:Label></h4>
                </div>

                <div class="modal-body" style="background-color: white; text-align: center">
                    <h5>This action cannot be undone. &nbsp;</h5>
                    <h5>Delete Order Set ?</h5>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                    <asp:Button ID="btnDel_Laboratory" aria-hidden="true" OnClientClick="this.disabled = true; this.value = 'Loading...';"
                        UseSubmitBehavior="false" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="btnDel_Laboratory_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="deleteOrderSetSubjectSSModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 20%;" runat="server">
            <div class="modal-content" style="border-radius: 7px">
                <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="text-align: center">
                        <asp:Label ID="Label3" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server" Text="Delete Order Set"></asp:Label></h4>
                </div>

                <div class="modal-body" style="background-color: white; text-align: center">
                    <h5>This action cannot be undone. &nbsp;</h5>
                    <h5>Delete Order Set ?</h5>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                    <asp:Button ID="btnDel_Subject_SS" aria-hidden="true" OnClientClick="this.disabled = true; this.value = 'Loading...';"
                        UseSubmitBehavior="false" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="btnDel_Subject_SS_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="deleteOrderSetSubjectAModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 20%;" runat="server">
            <div class="modal-content" style="border-radius: 7px">
                <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="text-align: center">
                        <asp:Label ID="Label4" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server" Text="Delete Order Set"></asp:Label></h4>
                </div>

                <div class="modal-body" style="background-color: white; text-align: center">
                    <h5>This action cannot be undone. &nbsp;</h5>
                    <h5>Delete Order Set ?</h5>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                    <asp:Button ID="btnDel_Subject_A" aria-hidden="true" OnClientClick="this.disabled = true; this.value = 'Loading...';"
                        UseSubmitBehavior="false" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="btnDel_Subject_A_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="deleteOrderSetObjectiveModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 20%;" runat="server">
            <div class="modal-content" style="border-radius: 7px">
                <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="text-align: center">
                        <asp:Label ID="Label5" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server" Text="Delete Order Set"></asp:Label></h4>
                </div>

                <div class="modal-body" style="background-color: white; text-align: center">
                    <h5>This action cannot be undone. &nbsp;</h5>
                    <h5>Delete Order Set ?</h5>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                    <asp:Button ID="btnDel_Objective" aria-hidden="true" OnClientClick="this.disabled = true; this.value = 'Loading...';"
                        UseSubmitBehavior="false" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="btnDel_Objective_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="deleteOrderSetAnalysisModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 20%;" runat="server">
            <div class="modal-content" style="border-radius: 7px">
                <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="text-align: center">
                        <asp:Label ID="Label6" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server" Text="Delete Order Set"></asp:Label></h4>
                </div>

                <div class="modal-body" style="background-color: white; text-align: center">
                    <h5>This action cannot be undone. &nbsp;</h5>
                    <h5>Delete Order Set ?</h5>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                    <asp:Button ID="btnDel_Analysis" aria-hidden="true" OnClientClick="this.disabled = true; this.value = 'Loading...';"
                        UseSubmitBehavior="false" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="btnDel_Analysis_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="deleteOrderSetPlanningModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 20%;" runat="server">
            <div class="modal-content" style="border-radius: 7px">
                <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="text-align: center">
                        <asp:Label ID="Label7" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server" Text="Delete Order Set"></asp:Label></h4>
                </div>

                <div class="modal-body" style="background-color: white; text-align: center">
                    <h5>This action cannot be undone. &nbsp;</h5>
                    <h5>Delete Order Set ?</h5>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                    <asp:Button ID="btnDel_Planning" aria-hidden="true" OnClientClick="this.disabled = true; this.value = 'Loading...';"
                        UseSubmitBehavior="false" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="btnDel_Planning_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>