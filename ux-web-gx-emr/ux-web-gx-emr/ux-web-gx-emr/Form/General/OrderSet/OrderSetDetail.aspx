<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderSetDetail.aspx.cs" MasterPageFile="~/Site.master" Inherits="Form_General_OrderSet_OrderSetDetail" %>

<%@ Register Src="~/Form/General/OrderSet/Control_Template/OrderSet_Clinic_Lab.ascx" TagName="StdClinicControl" TagPrefix="uc1" %>
<%@ Register Src="~/Form/General/OrderSet/Control_Template/OrderSet_Micro_Lab.ascx" TagName="StdMicroLabControl" TagPrefix="uc2" %>
<%@ Register Src="~/Form/General/OrderSet/Control_Template/OrderSet_AnatomiLab.ascx" TagName="StdAnatomiLab" TagPrefix="uc3" %>
<%@ Register Src="~/Form/General/OrderSet/Control_Template/OrderSet_PanelLab.ascx" TagName="StdPanelLab" TagPrefix="uc4" %>
<%@ Register Src="~/Form/General/OrderSet/Control_Template/OrderSet_MDC_Lab.ascx" TagName="StdMDCLabControl" TagPrefix="uc5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="CreateOrderSet" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .ui-autocomplete {
            max-height: 200px;
            overflow-y: auto;
            overflow-x: hidden;
        }

        .select {
            height: 29px;
            font-size: 12px;
            padding-top: 0px;
            width: 80px;
            padding-left: 2px;
        }

        .shadows {
            border: 1px;
            border-radius: 10px 10px 10px 10px;
            box-shadow: 0px 1px 5px #9293A0;
            margin: 20px;
        }

        .hidecoll {
        display:none;
        }
    </style>

    <script type="text/javascript">

        function ICDSuggestion() {

            $("#MainContent_icd_10").autocomplete({
                source: "../../SOAP/Control_Template/AutoCompleteICD.aspx",
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
                        var txt_Template_SOAP = document.getElementById('<%= txt_Template_SOAP.ClientID %>');
                        if (txt_Template_SOAP.value == "") {
                            txt_Template_SOAP.value = ui.item.itemName;
                        } else {
                            txt_Template_SOAP.value = txt_Template_SOAP.value + '\n' + ui.item.itemName;
                        }
                        txt_Template_SOAP.focus();
                    }
                }
            })
                .focus(function () {
                    $(this).autocomplete("search");
                })
                .autocomplete("instance")._renderItem = function (ul, item) {
                    return $("<li>")
                        .append('<table style="width:400px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                };
        }

        function DrugSuggestion() {

            $("#TextBoxACSearch").autocomplete({
                source: "AutoCompleteDrugOrderSet.aspx",
                minLength: 0,
                //success: function(data) {
                //    response($.map(data, function (item) {
                //        return {
                //                   hasil: item.itemId
                //               }
                //    }));
                //},
                open: function () {
                    $('ul.ui-autocomplete').prepend('<li>'
                        + '<table style="width:800px; border-bottom:1px solid lightgrey; background-color:#f4f4f4;">'
                        + '<tr>'
                        + '<td style="width:40%; padding:5px; vertical-align:top; font-weight:bold;"> Items </td>'
                        + '<td style="width:40%; padding:5px; vertical-align:top; font-weight:bold;"> Active Ingredients </td>'
                        + '<td style="width:20%; padding:5px; vertical-align:top; font-weight:bold;"> Formularium </td>'
                        + '</tr>'
                        + '</table>'
                        + '</li>');
                },
                position: { my: "left top", at: "left bottom", collision: "flip" },
                select: function (event, ui) {
                    //assign value back to the form element
                    if (ui.item) {
                        $(event.target).val(ui.item.itemId);
                        document.getElementById('<%= HF_flagfocussearch.ClientID %>').value = "searchfocus";

                        document.getElementById('<%= HF_ItemSelected.ClientID %>').value = ui.item.itemId;
                        document.getElementById('<%= ButtonAjaxSearch.ClientID %>').click();
                    }
                }
            })
                .focus(function () {
                    $(this).autocomplete("search");
                })
                .autocomplete("instance")._renderItem = function (ul, item) {
                    return $("<li>")
                        .append('<table style="width:800px; border-bottom:1px solid lightgrey;">'
                            + '<tr>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemName + '</td>'
                            + '<td style="width:40%; padding:5px; vertical-align:top;">' + item.itemIngredient + '</td>'
                            + '<td style="width:20%; padding:5px; vertical-align:top;">' + item.itemFormularium + '</td>'
                            + '</tr>'
                            + '</table>')
                        .appendTo(ul);
                };
        }

        $(window).load(function () {
            $(".loadPage").fadeOut("slow");
        });

        $(document).ready(function () {
            notificationOption();
        });

        function notificationWarning(pesan, title) {
            toastr.warning(pesan + '<br /> <button type=\"button\" class=\"btn btn-danger btn-sm\" style=\"height: 25px; padding-top: 3px; width: 75px; float:right; \">OK</button>', title);
            toastr.options.positionClass = "toast-top-right";
            toastr.options.closeButton = true;
            toastr.options.timeOut = 0;
            toastr.options.extendedTimeOut = 0;
            toastr.options.tapToDismiss = true;
        }

        function notificationOption() {
            toastr.options.positionClass = "toast-top-right";
        }

        function warningnotificationOption() {
            toastr.options.positionClass = "toast-top-right";
            toastr.options.closeButton = true;
            toastr.options.timeOut = 0;
            toastr.options.extendedTimeOut = 0;
            toastr.options.tapToDismiss = true;
        }

        function CheckField() {
            document.getElementById("<%=btnSave.ClientID%>").disabled = false;
            var checkFormularium = $("[id$='checkFormularium']").val();
            var Check1 = document.getElementById("<%=txtNewOrderSet_Name.ClientID%>");
            var checkItemCountCompound = $("[id$='checkItemCount']").val();
            var CheckValue = $('#<%=typeOrderSet.ClientID %> option:selected').val();
            var GridId = "<%=drugsGrid.ClientID %>";
            var grid = document.getElementById(GridId);
            var txt_Template_SOAP = document.getElementById("<%=txt_Template_SOAP.ClientID%>");

            if (Check1.value == "") {
                //$("[id$='txtNewOrderSet_Name']").attr("style", "display:block; border-color:red;width:100%;");
                //alert("Order set name is empty");
                notificationWarning("Order Set Name can not be empty!", "Save Alert!");
                Check1.focus();
                Check1.placeholder = "This field is mandatory..."
                Check1.classList.add("placeholderred");
                return false;
            } else {
                if (CheckValue == 0) {
                    grid = document.getElementById(GridId);
                    rowscount = grid.rows.length;
                    if (rowscount > 1) {
                        return true;
                    }
                    else {
                        //alert('Please input List Item');
                        notificationWarning("Please input List Item", "Save Alert!");
                        return false;
                    }
                }
                else if (CheckValue == 1) {
                    var txtQty = document.getElementById('<%= txtQty.ClientID%>');
                    var ddlUOM_header = document.getElementById('<%=ddlUOM_header.ClientID %>').value;

                   if (ddlUOM_header == 0){
                        notificationWarning("Please Select UOM", "Save Alert!");
                        return false;
                   }
                    
                   if (txtQty.value == 0 || txtQty.value == ""){
                        notificationWarning("Please Input Compound Quantity", "Save Alert!");
                        return false;
                    }

                    //Boleh 1 item
                    //if (checkItemCountCompound == "One") {
                    //    alert("Compound should contains minimum 2 items");
                    //    return false;
                    //}
                }
                else if (CheckValue != 0 && CheckValue != 2 && CheckValue != 1) {
                    if (txt_Template_SOAP.value == "") {
                        notificationWarning("Please input Text Detail", "Save Alert!");
                        return false;
                    }
                }

            }

            if (checkFormularium == "Not Formularium") {
                $('#myModal').modal('show');
                return false;
            }
            else {
                if (Check1.value == "") {
                    //$("[id$='txtNewOrderSet_Name']").attr("style", "display:block; border-color:red;width:100%;");
                    //alert("Order set name is empty");
                    notificationWarning("Order Set Name can not be empty!", "Save Alert!");
                    Check1.focus();
                    Check1.placeholder = "This field is mandatory..."
                    Check1.classList.add("placeholderred");
                    return false;
                }
                else {

                    Check1.placeholder = "Type here...";
                    if (CheckValue == 0) {
                        rowscount = grid.rows.length;
                        if (rowscount > 1) {
                            return true;
                        }
                        else {
                            //alert('Please input List Item');
                            notificationWarning("Please input List Item", "Save Alert!");
                            return false;
                        }
                    }
                    else if (CheckValue == 1) {
                        var GridId = "<%=compoundGrid.ClientID %>";
                        var grid = document.getElementById(GridId);
                        rowscount = grid.rows.length;
                        if (rowscount > 1) {
                            return true;
                        }
                        else {
                            //alert('Please input List Item');
                            notificationWarning("Please input List Item", "Save Alert!");
                            return false;
                        }
                    }
                    else if (CheckValue == 2) {
                        return true;
                    }
                }
            }
        }

        function txtOnKeyPressFalse() {
            var c = event.keyCode;
            if (c == 13) {
                return false;
            }
        }

        function text_check() {
            var c = event.keyCode;
            if (c == 13) {

                return false;
            }

            var regex = new RegExp("^[a-zA-Z0-9 _()!:,\.\\\\-]+$");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        }

        function CheckNumeric() {
            return event.keyCode >= 48 && event.keyCode <= 57 || event.keyCode == 46;
        }


        function validateFloatKeyPress(el) {
            var v = parseFloat(el.value);
            var strv = el.value.split('.');
            var strval = strv[1];

            if (strval != null) {
                if (strval.length == 3) {
                    if (strval == "000") {
                        el.value = strv[0];
                    }
                    else
                        el.value = (isNaN(v)) ? '' : v.toFixed(3);
                }
                else if (strval.length == 2) {
                    if (strval == "00") {
                        el.value = strv[0];
                    }
                    else
                        el.value = (isNaN(v)) ? '' : v.toFixed(2);
                }
                else if (strval.length == 1) {
                    if (strval == "0") {
                        el.value = strv[0];
                    }
                    else
                        el.value = (isNaN(v)) ? '' : v.toFixed(2);
                }
                else
                    el.value = (isNaN(v)) ? '' : v.toFixed(2);
            }
        }

        function OnClick() {
            if (testpopup.style.display == "none") {
                testpopup.style.display = "";
                $("[id$='txtSearchItem']").focus();
            }
            else
                testpopup.style.display = "none";
        }

        function OnClickXX() {
            if (testpopupXX.style.display == "none") {
                testpopupXX.style.display = "";
                $("[id$='txtSearchItemXX']").focus();
            }
            else
                testpopupXX.style.display = "none";
        }

        function changeView() {
            var compound = document.getElementById('<%= compoundView.ClientID %>');
            var order_name = document.getElementById('<%= orderSet_name.ClientID %>');
            var tblDrug = document.getElementById('<%= tblDrug.ClientID %>');
            var tblCompound = document.getElementById('<%= tblCompound.ClientID %>');
            var frmLaboratory = document.getElementById('<%= frmLaboratory.ClientID %>');
            var orderSet_content = document.getElementById('<%= orderSet_content.ClientID %>');
            var save_orderSet = document.getElementById('<%= save_orderSet.ClientID %>');
            var txt_search = document.getElementById('<%= txt_search.ClientID %>');
            var btn_save_part = document.getElementById('<%= btn_save_part.ClientID %>');
            var frmTemplateSOAP = document.getElementById('<%= frmTemplateSOAP.ClientID%>');
            var upSOAPTemplate = document.getElementById('<%= upSOAPTemplate.ClientID%>');
            var order_set_content = document.getElementById('<%= order_set_content.ClientID%>');
            var icd_10 = document.getElementById('<%= icd_10.ClientID%>');
            var header_div = document.getElementById('<%=header_div.ClientID%>');
            var txbCattDokter_compound = document.getElementById('<%= txbCattDokter_compound.ClientID%>');
            var compoundCattDoctor = document.getElementById('<%= compoundCattDoctor.ClientID%>');
            var txt_orderHeaderId = document.getElementById('<%= txt_orderHeaderId.ClientID%>');
            var headerOrderSet = document.getElementById('<%= headerOrderSet.ClientID%>');
            var nameOrderSet = document.getElementById('<%= nameOrderSet.ClientID%>');

            if ($("[id$='typeOrderSet']").val() == 0) {
                compound.style.display = 'none';
                tblDrug.style.display = 'block';
                tblCompound.style.display = 'none';
                frmLaboratory.style.display = 'none';
                orderSet_content.style.background = 'white';
                save_orderSet.style.background = 'white';
                txt_search.style.display = 'block';
                frmTemplateSOAP.style.display = 'none';
                order_set_content.style.height = 'calc(100vh - 100px)';
                icd_10.style.display = 'none';
                //header_div.style.removeProperty("height");
                txbCattDokter_compound.style.display = 'none';
                compoundCattDoctor.style.display = 'none';
            }
            else if ($("[id$='typeOrderSet']").val() == 1) {
                console.log("Data HEader Order set", txt_orderHeaderId.value);
                if (txt_orderHeaderId.value != "") {
                    //headerOrderSet.classList.remove("col-sm-3");
                    //headerOrderSet.classList.add("col-sm-2");
                    //nameOrderSet.style.removeProperty("width");
                    //nameOrderSet.style.width = "70%";
                } else {
                    //headerOrderSet.removeAttribute("class");
                    //headerOrderSet.classList.add("col-sm-3");
                    //nameOrderSet.style.removeProperty("width");
                    //nameOrderSet.style.width = "45%";
                }
                //header_div.style.height="125px";
                compound.style.display = 'block';
                tblCompound.style.display = 'block';
                tblDrug.style.display = 'none';
                frmLaboratory.style.display = 'none';
                orderSet_content.style.background = 'white';
                save_orderSet.style.background = 'white';
                txt_search.style.display = 'block';
                frmTemplateSOAP.style.display = 'none';
                order_set_content.style.height = 'calc(100vh - 100px)';
                icd_10.style.display = 'none';
                compoundCattDoctor.style.display = 'block';
            }
            else if ($("[id$='typeOrderSet']").val() == 2) {
                compound.style.display = 'none';
                tblDrug.style.display = 'none';
                tblCompound.style.display = 'none';
                save_orderSet.style.background = 'transparent';
                frmLaboratory.style.display = 'block';
                txt_search.style.display = 'none';
                frmTemplateSOAP.style.display = 'none';
                order_set_content.style.removeProperty('height');
                icd_10.style.display = 'none';
                //header_div.style.removeProperty("height");
                txbCattDokter_compound.style.display = 'none';
                compoundCattDoctor.style.display = 'none';
            } else {
                if ($("[id$='typeOrderSet']").val() == 6)
                    icd_10.style.display = 'block';
                else
                    icd_10.style.display = 'none';

                compound.style.display = 'none';
                tblDrug.style.display = 'none';
                tblCompound.style.display = 'none';
                frmLaboratory.style.display = 'none';
                orderSet_content.style.background = 'white';
                save_orderSet.style.background = 'white';
                txt_search.style.display = 'none';
                frmTemplateSOAP.style.display = 'block';
                upSOAPTemplate.style.display = 'block';
                order_set_content.style.height = 'calc(100vh - 100px)';
                //header_div.style.removeProperty("height");
                txbCattDokter_compound.style.display = 'none';
                compoundCattDoctor.style.display = 'none';
            }

            return true;
        }

        function loseBox() {
            testpopup.style.display = 'none';
        }

        //jangan dihapus
        //window.addEventListener('mouseup', function (e) {
        //    //alert("lvl 1 "+e.target);
        //    //alert("lvl 2 "+e.target.parentNode.id);
        //    //alert("lvl 3 "+e.target.parentNode.parentNode.id);
        //    //alert("lvl 4 "+e.target.parentNode.parentNode.parentNode.id);
        //    //alert("lvl 5 "+e.target.parentNode.parentNode.parentNode.parentNode.id);

        //    //hide pop up icd when click outside div
        //    if (e.target.parentNode.parentNode != null && e.target.parentNode.parentNode.parentNode != null && e.target.parentNode.parentNode.parentNode.parentNode != null) {
        //        if (e.target.parentNode.parentNode.id != "testpopup" && e.target.parentNode.parentNode.parentNode.id != "testpopup" && e.target.parentNode.parentNode.parentNode.id != "MainContent_gvw_data") {
        //            testpopup.style.display = 'none';
        //        }
        //    }
        //});

        function txtOnKeyPressOrder() {
            var FlagFocus = document.getElementById('<%= HiddenFieldSearchFocus.ClientID %>');
            FlagFocus.value = "orderfocus";

            var c = event.keyCode;
            if (c == 13) {
                var Button = "<%=buttonOrderSearch.ClientID %>";
                document.getElementById(Button).click();
                return false;
            }
        }

        function txtOnKeyPressOrderXX() {

            var c = event.keyCode;
            if (c == 13) {
                var Button = "<%=buttonFindXX.ClientID %>";
                document.getElementById(Button).click();
                return false;
            }
        }

        function AutoExpand(txtbox) {
            txtbox.style.height = "1px";
            txtbox.style.height = (25 + txtbox.scrollHeight) + "px";
        }

        function minexpand(txtbox) {

            txtbox.style.height = "1px";
            txtbox.style.height = (5 + txtbox.scrollHeight) + "px";
        }

        $(document).ready(function () {

            DrugSuggestion();
            ICDSuggestion();

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_beginRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {

                        var flagLoadingOS = document.getElementById('<%= HFloadingorderset.ClientID %>');

                        if (flagLoadingOS.value == "true") {
                            $(".loadingOS").show();
                        }

                    }
                });

                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {

                        var FlagFocus = document.getElementById('<%= HiddenFieldSearchFocus.ClientID %>');

                        if (FlagFocus.value == "orderfocus") {
                            document.getElementById('<%= txtSearchItem.ClientID %>').focus();
                        }

                        DrugSuggestion();
                        ICDSuggestion();

                        var FlagSearchFocus = document.getElementById('<%= HF_flagfocussearch.ClientID %>');

                        if (FlagSearchFocus.value == "searchfocus") {
                            $("#TextBoxACSearch").focus();
                            document.getElementById('<%= HF_flagfocussearch.ClientID %>').value = "";
                        }

                        $(".loadingOS").hide();
                        document.getElementById('<%= HFloadingorderset.ClientID %>').value = "false";
                    }
                });
            };
        });

        function setflagloading(item, status) {
            if (item == "orderset") {
                document.getElementById('<%= HFloadingorderset.ClientID %>').value = "true";
            }
        }

        function switchBahasa() {
            var bahasa = document.getElementById('<%=HFisBahasa.ClientID%>').value;
            if (bahasa == "ENG") {
                if (document.getElementById('lblbhs_choosecategory') != null) {
                    document.getElementById('lblbhs_choosecategory').innerHTML = "Category";
                }
                document.getElementById('lblbhs_ordersetname').innerHTML = "Order Set Name";
                document.getElementById('lblbhs_clinicalpathology').innerHTML = "Clinical Pathology";
                document.getElementById('lblbhs_microbiology').innerHTML = "Microbiology";
                document.getElementById('lblbhs_patologiAnatomi').innerHTML = "Anatomical Pathology";

                var table = document.getElementById("<%=drugsGrid.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    headers[0].innerText = "Item";
                    headers[1].innerText = "Text";
                    headers[2].innerText = "Dose";
                    headers[3].innerText = "Frequency";
                    headers[4].innerText = "Route";
                    headers[5].innerText = "Instruction";
                    headers[6].innerText = "Qty";
                    headers[7].innerText = "UOM";
                }

                var table = document.getElementById("<%=gvw_data.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "Item";
                        headers[1].innerText = "Active Ingredients";
                    }
                }
            }
            else if (bahasa == "IND") {

                if (document.getElementById('lblbhs_choosecategory') != null) {
                    document.getElementById('lblbhs_choosecategory').innerHTML = "Pilih Kategori";
                }
                document.getElementById('lblbhs_ordersetname').innerHTML = "Nama Paket";
                document.getElementById('lblbhs_clinicalpathology').innerHTML = "Patologi Klinik";
                document.getElementById('lblbhs_microbiology').innerHTML = "Mikrobiologi";
                document.getElementById('lblbhs_patologiAnatomi').innerHTML = "Patologi Anatomi";

                var table = document.getElementById("<%=drugsGrid.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    headers[0].innerText = "Obat";
                    headers[1].innerText = "Teks";
                    headers[2].innerText = "Dosis";
                    headers[3].innerText = "Frekuensi";
                    headers[4].innerText = "Rute";
                    headers[5].innerText = "Instruksi";
                    headers[6].innerText = "Jml";
                    headers[7].innerText = "Unit";
                }

                var table = document.getElementById("<%=gvw_data.ClientID %>");
                if (table != null) {
                    var headers = table.getElementsByTagName('th');

                    if (headers.length != 0) {
                        headers[0].innerText = "Obat";
                        headers[1].innerText = "Bahan Aktif";
                    }
                }
            }
        }

        function isValidKarakter(evt) {
            evt = (evt) ? evt : window.event;
            
            var key = String.fromCharCode(!evt.charCode ? evt.which : evt.charCode);
            if (key.search(/'|"/g) !== -1) {
               evt.preventDefault();
               return false;
            }
            return true;
        }

    </script>

    <div style="min-height: calc(100vh - 82px); background-color: white; border-radius: 7px 7px 7px 7px; margin: 15px;" id="order_set_content" runat="server">
        <asp:HiddenField ID="HFisBahasa" runat="server" />
        <asp:UpdateProgress ID="uprog_saveorderset" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
                </div>
                <div style="margin-top: 12%; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                    <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <!-- #### CREATE #### -->
        <div class="row no-margin">
            <div class="col-lg-12" style="background-color: white; border-radius: 7px 7px 0px 0px;" id="header_form" runat="server">
            </div>
        </div>

        <!-- #### TOP SECTION #### -->
        <div style="padding: 10px; background-color: #e7e8ef; min-height:75px;" id="header_div" runat="server">

            <div style="width: 100%">

                <!-- #### order set name textbox #### -->
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <div id="orderSet_name" runat="server" class="container-fluid no-padding">
                            <div class="row">
                                <div class=" col-sm-3" id="headerOrderSet" runat="server">
                                    <div class="row">
                                        <div id="ddlOrderset_Type" runat="server" class="col-sm-5" style="display: none; padding-right: 0px;">
                                            <div runat="server">
                                                <div>
                                                    <%--<label style="display: <%=setENG%>;">Choose Category </label>
                                                        <label style="display: <%=setIND%>;">Pilih Kategori </label>--%>
                                                    <label id="lblbhs_choosecategory">Category</label>
                                                </div>
                                                <asp:HiddenField ID="hfStatusCompound" runat="server" />
                                                <asp:DropDownList runat="server" ID="typeOrderSet" OnSelectedIndexChanged="typeOrderSet_SelectedIndexChanged" AutoPostBack="true" CssClass="select" Width="100%">
                                                    <asp:ListItem Value="0" Text="Drugs"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Compound"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Laboratory"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="S - Chief Complaint"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="S - Anamnesis"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="Objective"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="Assessment"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="Planning"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-7" id="nameOrderSet" runat="server">
                                            <div>
                                                <%--<label style="display: <%=setENG%>;">Order Set Name </label>
                                                <label style="display: <%=setIND%>;">Nama Paket </label>--%>
                                                <label id="lblbhs_ordersetname">Order Set Name </label>
                                                <label style="color: red;">*</label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtNewOrderSet_Name" onKeyPress="javascript:return text_check();" Style="text-transform: capitalize; font-weight: bold; font-size: 15px; height: 29px; width: 100%;" runat="server" placeholder=""></asp:TextBox>
                                                <asp:HiddenField ID="txt_orderHeaderId" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6" id="compoundView" runat="server" style="padding-right: 0px;">
                                    <div class="row">
                                        <div class="col-sm-1" style="width: 8%">
                                            <div>
                                                <label>Text</label>
                                            </div>
                                            <div>
                                                <div class="pretty p-icon p-curve" style="margin-top: 4px;"  title="Use Dose Text">
                                                    <asp:CheckBox ID="chk_is_dosetext" runat="server" AutoPostBack="true" OnCheckedChanged="chk_is_dosetext_CheckedChanged" ></asp:CheckBox>
                                                    <div class="state p-success">
                                                        <i class="icon fa fa-check"></i>
                                                        <label></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2" style="padding-left: 0px; width: 19%">
                                            <div>
                                                <label>Dose</label>
                                            </div>
                                            <div>
                                                <%--<asp:TextBox ID="txtDoze" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" runat="server" Style="width: 100%; text-transform: capitalize; font-weight: bold; font-size: 12px; height: 29px;"></asp:TextBox>
                                                <asp:DropDownList ID="ddlDoseUom_header" Style="cursor: pointer; width: 100%; text-transform: capitalize; font-weight: bold; font-size: 12px; height: 29px;" runat="server"></asp:DropDownList>--%>
                                            
                                                <div runat="server" id="div_dose_header">
                                                    <asp:TextBox  Style="text-align: right; width:40px; height:29px;" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="txtDoze" runat="server" Text='<%# Bind("dose") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                                    <asp:DropDownList  ID="ddlDoseUom_header" style="margin: 0px; width:55px; height:29px;" Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" runat="server" ></asp:DropDownList>
                                                </div>
                                                <asp:TextBox  Width="100%" MaxLength="100" Font-Size="12px" Style="margin: 0px; overflow: hidden; height:29px;" Font-Names="Helvetica, Arial, sans-serif" ID="racikan_dosetext" runat="server" Visible="false" onkeydown="AutoExpand(this);" TextMode="MultiLine" Rows="1" onfocus="minexpand(this)" CssClass="text-multiline-dialog"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2" style="padding-left: 0px; width: 13%">
                                            <div>
                                                <label>Frequency</label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlFrequency_header" Style="cursor: pointer; width: 100%; text-transform: capitalize; font-size: 12px; height: 29px;" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-2" style="padding-left: 0px; width: 13%">
                                            <div>
                                                <label>Route</label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlRoute_header" Style="cursor: pointer; width: 100%; text-transform: capitalize; font-size: 12px; height: 29px;" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-2" style="padding-left: 0px; width: 19%">
                                            <div>
                                                <label>Instruction</label>
                                            </div>
                                            <div>
                                                <textarea id="txtInstruction" style="width: 100%; max-width: 100%; max-height: 40px; font-size: 12px;" maxlength="100" runat="server"></textarea>
                                            </div>
                                        </div>
                                        <div class="col-sm-1" style="padding-left: 0px; width: 9%">
                                            <div>
                                                <label>Qty</label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtQty" Style="width: 100%; text-transform: capitalize; font-size: 12px; height: 29px;" runat="server">1</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-1" style="padding-left: 0px; width: 13%">
                                            <div>
                                                <label>U.O.M</label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlUOM_header" Style="cursor: pointer; width: 100%; text-transform: capitalize; font-size: 12px; height: 29px;" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-1" style="padding-left: 0px; padding-right: 0px; width: 6%">
                                            <div>
                                                <label>Iter</label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtIter" onkeypress="return CheckNumeric();" Style="width: 100%; text-transform: capitalize; font-size: 12px; height: 29px;" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="padding-left: 0px; float: right; margin-right: 20px;">
                                    <div>&nbsp;</div>
                                    <div id="btn_save_part" runat="server">
                                        <asp:LinkButton ID="btnCancelDrugs" runat="server" CssClass="btn btn-github btn-emr-medium" Style="width: 65px;" PostBackUrl="~/Form/General/OrderSet/ManageOrderSet.aspx"> <i class="fa fa-arrow-circle-left"></i> Back </asp:LinkButton>
                                        <asp:Button ID="btnSave" OnClientClick="return CheckField();" OnClick="btnSave_Click" runat="server" Text="Save & Close" CssClass="btn btn-lightGreen btn-emr-medium" Style="width: 95px;" />
                                        <asp:HiddenField ID="hf_check_data" runat="server" />
                                    </div>
                                </div>
                            </div>
                            
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- #### end order set name textbox #### -->

                <!-- #### compound choose section textbox #### -->
                <%--<div id="compoundView" style="width: 100%; display: none" class="btn-group" role="group" runat="server">
                        <div class="btn-group btn-group-justified" style="width: 100%" role="group" aria-label="...">
                            <div class="btn-group" role="group" style="width: 1%"></div>
                            <div class="btn-group" role="group" style="width: 5%;">
                                <div>
                                    <label>Dose</label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtDoze" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Width="100%" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="btn-group" role="group" style="width: 1%"></div>
                            <div class="btn-group" role="group" style="width: 19%;">
                                <div>
                                    <label>Dose UOM</label>
                                </div>
                                <div>
                                    <asp:DropDownList Style="cursor: pointer" ID="ddlDoseUom_header" Width="100%" Height="25px" runat="server"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="btn-group" role="group" style="width: 1%"></div>
                            <div class="btn-group" role="group" style="width: 15%">
                                <div>
                                    <label>Frequency</label>
                                </div>
                                <div>
                                    <asp:DropDownList Style="cursor: pointer" ID="ddlFrequency_header" Width="100%" Height="25px" runat="server"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="btn-group" role="group" style="width: 1%;"></div>
                            <div class="btn-group" role="group" id="routeCompound" style="width: 15%;">
                                <div>
                                    <label>Route</label>
                                </div>
                                <div>
                                    <asp:DropDownList Style="cursor: pointer" ID="ddlRoute_header" Width="100%" Height="25px" runat="server"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="btn-group" role="group" style="width: 1%"></div>
                            <div class="btn-group" role="group" style="width: 20%">
                                <div>
                                    <label>Instruction</label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtInstruction" MaxLength="100" Width="100%" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="btn-group" role="group" style="width: 1%"></div>
                            <div class="btn-group" role="group" style="width: 3%">
                                <div>
                                    <label>Qty</label>
                                </div>
                                <div>
                                    <asp:TextBox Style="text-align: right; cursor: not-allowed" ID="txtQty" Width="100%" Font-Bold="true" onkeypress="return CheckNumeric();" runat="server" ReadOnly="true">1</asp:TextBox>
                                </div>
                            </div>
                            <div class="btn-group" role="group" style="width: 1%"></div>
                            <div class="btn-group" role="group" style="width: 12%">
                                <div>
                                    <label>U.O.M</label>
                                </div>
                                <div>
                                    <asp:DropDownList Style="cursor: pointer" ID="ddlUOM_header" Width="100%" Height="25px" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="btn-group" role="group" style="width: 1%;"></div>
                            <div class="btn-group" role="group" id="iterCompound" style="width: 4%;">
                                <div>
                                    <label>Iter</label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtIter" onkeypress="return CheckNumeric();" Width="100%" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                <!-- #### end compound choose section #### -->
            </div>
        </div>

        <!-- #### END TOP SECTION #### -->

        <!-- #### MIDDLE SECTION #### -->

        <div class="row" style="width: 100%; background-color: white; margin-left: 0px; padding: 10px;" id="orderSet_content" runat="server">
            <div class="col-sm-12" id="tblDrug" style="padding-left: 0px; padding-right: 0px;" runat="server">
                <div>
                    <asp:UpdatePanel runat="server" UpdateMode="Always" ID="updatepanelDrugOS">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="checkFormularium"></asp:HiddenField>

                            <asp:GridView ID="drugsGrid" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed footer-table-margin"
                                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="text-center" OnRowDataBound="drugsGrid_RowDataBound" OnRowDeleting="drugsGrid_RowDeleting">
                                <Columns>

                                    <asp:TemplateField HeaderText="Item" ItemStyle-Width="20%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="label_item_drugs" runat="server" Text='<%# Bind("salesItemName") %>'></asp:Label>
                                            <asp:HiddenField ID="id_item_drugs" runat="server" Value='<%# Bind("salesItemId") %>'></asp:HiddenField>
                                            <asp:HiddenField ID="id_order_drugs_detail" runat="server" Value='<%# Bind("order_set_detail_id") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Text" ItemStyle-Width="4%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div class="pretty p-icon p-curve" style="margin-top: 4px;"  title="Use Dose Text">
                                                <asp:CheckBox ID="drug_is_dosetext" runat="server" OnCheckedChanged="drug_is_dosetext_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("IsDoseTextDetail") %>'></asp:CheckBox>
                                                <div class="state p-success">
                                                    <i class="icon fa fa-check"></i>
                                                    <label></label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dose" ItemStyle-Width="10%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div runat="server" Visible='<%# Eval("IsDoseTextDetail").ToString() == "False" %>'>
                                                <asp:TextBox ID="dose_drugs" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Width="40px" runat="server" Text='<%# Bind("doze") %>'></asp:TextBox>
                                                <asp:DropDownList ID="ddldoseUOM_drugs" Width="65px" runat="server"></asp:DropDownList>
                                            </div>
                                            <asp:TextBox  Width="100%" MaxLength="100" Style="margin: 0px; overflow: hidden;" ID="doseText_drugs" runat="server" Visible='<%# Eval("IsDoseTextDetail") %>' Text='<%# Bind("dose_text") %>' onkeydown="AutoExpand(this);" TextMode="MultiLine" Rows="1" onfocus="minexpand(this)" CssClass="text-multiline-dialog"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Frequency" ItemStyle-Width="10%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlfrequency_drugs" Width="100%" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Route" ItemStyle-Width="10%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlroute_drugs" Width="100%" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Instruction" ItemStyle-Width="25%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="instruction_drugs" MaxLength="100" runat="server" Width="100%" Text='<%# Bind("instruction") %>' onkeydown="AutoExpand(this);" TextMode="MultiLine" Rows="1" onfocus="minexpand(this)" CssClass="text-multiline-dialog"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" ItemStyle-Width="5%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="qty_drugs" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Width="100%" runat="server" Text='<%# Bind("quantity") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM" ItemStyle-Width="8%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddluom_drugs" Width="100%" runat="server" Enabled="false" style="border:0px;"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Iter" ItemStyle-Width="3%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="iter_drugs" onkeypress="return CheckNumeric();" Width="30px" runat="server" Text='<%# Bind("iteration") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Formularium" ItemStyle-Width="3%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="formularium_drugs" onkeypress="return CheckNumeric();" Width="30px" runat="server" Text='<%# Bind("formularium") %>' ReadOnly="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Icon/ic_delete.svg" ItemStyle-Width="3%" ShowDeleteButton="true" ControlStyle-Width="15px" ControlStyle-Height="15px" />
                                </Columns>
                            </asp:GridView>
                            <div style="height: 22px; text-align: center;">
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updatepanelDrugOS">
                                    <ProgressTemplate>

                                        <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />

                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="col-sm-12" id="tblCompound" runat="server" style="padding-left: 0px; padding-right: 0px;">
                <div>
                    <asp:UpdatePanel runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="checkItemCount"></asp:HiddenField>
                            <asp:GridView ID="compoundGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed footer-table-margin"
                                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="text-center" OnRowDeleting="compoundGrid_RowDeleting" OnRowDataBound="compoundGrid_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item" ItemStyle-Width="20%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="item_compound" runat="server" Text='<%# Bind("salesItemName") %>'></asp:Label>
                                            <asp:HiddenField ID="id_item_compound" runat="server" Value='<%# Bind("salesItemId") %>'></asp:HiddenField>
                                            <asp:HiddenField ID="id_order_compound_detail" runat="server" Value='<%# Bind("order_set_detail_id") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-Width="5%" HeaderText="Text" HeaderStyle-BackColor="#f4f4f4" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <div class="pretty p-icon p-curve" style="margin-top: 4px;"  title="Use Dose Text">
                                                <asp:CheckBox ID="racikan_is_dosetext" runat="server" AutoPostBack="true" OnCheckedChanged="racikan_is_dosetext_CheckedChanged"  Checked='<%# Eval("IsDoseTextDetail") %>'></asp:CheckBox>
                                                <div class="state p-success">
                                                    <i class="icon fa fa-check"></i>
                                                    <label></label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="Dose" HeaderStyle-BackColor="#f4f4f4" HeaderStyle-Font-Size="11px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <div runat="server" Visible='<%# Eval("IsDoseTextDetail").ToString() == "False" %>'>
                                                <asp:TextBox  Style="text-align: right; width:50px;" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="racikan_dosage_id" runat="server" Text='<%# Bind("doze") %>' onkeydown="return txtOnKeyPressFalse();"></asp:TextBox>
                                                <asp:DropDownList  ID="racikan_doseuom" style="margin: 0px; width:100px;" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" runat="server" ></asp:DropDownList>
                                            </div>
                                            <asp:TextBox  Width="160px" MaxLength="100" Font-Size="11px" Style="margin: 0px; overflow: hidden;" Font-Names="Helvetica, Arial, sans-serif" ID="racikan_dosetext" runat="server" Visible='<%# Eval("IsDoseTextDetail") %>' Text='<%# Bind("dose_text") %>' onkeydown="AutoExpand(this);" TextMode="MultiLine" Rows="1" onfocus="minexpand(this)" CssClass="text-multiline-dialog"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Jml per Racikan" ItemStyle-Width="5%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="hidecoll" HeaderStyle-CssClass="hidecoll">
                                        <ItemTemplate>
                                            <asp:TextBox ID="qty_compound" Style="text-align: right" onkeypress="return CheckNumeric();" onchange="validateFloatKeyPress(this);" Width="30px" runat="server" Text='<%# Bind("quantity") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit" ItemStyle-Width="10%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="hidecoll" HeaderStyle-CssClass="hidecoll">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddluom_compound" Width="100%" Height="25px" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Instruction" ItemStyle-Width="35%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:TextBox ID="instruction_compound" MaxLength="100" runat="server" Width="100%" Text='<%# Bind("instruction") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/Icon/ic_delete.svg" ItemStyle-Width="5%" ShowDeleteButton="true" ControlStyle-Width="15px" ControlStyle-Height="15px" />
                                </Columns>
                            </asp:GridView>

                            <div id="compoundCattDoctor" runat="server">
                                <div class="row">
                                    <div class="col-sm-9" style="padding-top:5px;">
                                        <textarea id="txbCattDokter_compound" runat="server" style="width: 103%" placeholder="Write compound Instruction for Pharmacy..."></textarea>
                                    </div>
                                </div>
                            </div>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="col-sm-12" id="frmLaboratory" runat="server" style="margin-top: 10px; padding-left: 0px; display: none; padding-right: 0px; margin-bottom: 10px;">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:Repeater runat="server" ID="Repeater1" Visible="false">
                            <ItemTemplate>
                                <li>
                                    <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("name") %>' Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false" />
                                </li>

                            </ItemTemplate>
                        </asp:Repeater>
                        <cc1:TabContainer ID="TabContainer1" runat="server">
                            <cc1:TabPanel ID="ClinicalLab" Visible="true" HeaderText="Clinical Pathology" Font-Size="14px" runat="server">
                                <HeaderTemplate>
                                    <label id="lblbhs_clinicalpathology">Clinical Pathology </label>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div style="width: 1080px;">
                                        <uc1:StdClinicControl runat="server" ID="StdClinicControl" />
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="Microbiology" Visible="true" HeaderText="Microbiology " runat="server">
                                <HeaderTemplate>
                                    <label id="lblbhs_microbiology">Microbiology </label>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div style="width: 1080px;">
                                        <uc2:StdMicroLabControl runat="server" ID="StdMicroLabControl" />
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="PatologiAnatomi" Visible="true" HeaderText="Patologi Anatomi " runat="server">
                                <HeaderTemplate>
                                    <label id="lblbhs_patologiAnatomi">Anatomical Pathology </label>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div style="width: 1080px;">
                                        <uc3:StdAnatomiLab runat="server" ID="StdAnatomiLab" />
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="MDCLab" Visible="true" HeaderText="MDC " runat="server">
                                <HeaderTemplate>
                                    <label id="lblbhs_mdc">MDC </label>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div style="width: 1080px;">
                                        <uc5:StdMDCLabControl runat="server" ID="StdMDCLabControl" />
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="PanelLan" Visible="true" HeaderText="Panel and Others" runat="server">
                                <HeaderTemplate>
                                    <label id="lblbhs_panelLab">Panel and Others</label>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div style="width: 1080px;">
                                        <uc4:StdPanelLab runat="server" ID="StdPanelLab" />
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="col-sm-12" id="frmTemplateSOAP" style="padding-left: 0px; padding-right: 0px; display: none" runat="server">

                <asp:UpdatePanel runat="server" UpdateMode="Always" ID="upSOAPTemplate">
                    <ContentTemplate>
                        <div style="margin-bottom: 15px">
                            <asp:TextBox ID="icd_10" runat="server" class="autosuggest" onkeydown="return txtOnKeyPressFalse();" placeholder="Add ICD 10 ..." Style="display: none"></asp:TextBox>
                        </div>
                        <textarea id="txt_Template_SOAP" runat="server" maxlength="5000" style="width: 50%; height: calc(100vh - 300px)" onkeypress="return isValidKarakter(event)"></textarea><br />
                        <label>5000 characters</label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <!-- #### END MIDDLE SECTION #### -->

        <!-- #### BOTTOM SECTION #### -->

        <!-- ==== Normal Search ==== -->

        <div class="col-sm-12" style="width: 100%; background-color: white; padding-left: 0px; padding-right: 0px; display: none;" id="save_orderSet" runat="server">

            <!-- #### button save, cancel, textbox search #### -->
            <div id="search_drugs_Normal" runat="server">

                <div class="row">
                    <div class="col-sm-6">
                        <div id="txt_search_Normal" runat="server">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>

                                    <div class="row" style="padding-left: 15px; padding-bottom: 5px;">
                                        <div class="col-sm-6" style="padding-right: 35px;">
                                            <div class="has-feedback">
                                                <asp:TextBox ID="item_drugs_autocomplete" Width="100%" placeholder="Add item here..." runat="server" ReadOnly="true" OnClick="OnClick();"></asp:TextBox>
                                                <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px;"></span>
                                            </div>
                                            <asp:TextBox ID="id_item_autocomplete" runat="server" Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:UpdateProgress ID="uProgCariObat" runat="server" AssociatedUpdatePanelID="updatepanelpencarianobat">
                                                <ProgressTemplate>
                                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-sm-6 text-right">
                    </div>
                </div>

            </div>
            <!-- #### end button save, cancel, textbox search #### -->

            <asp:HiddenField ID="HiddenFieldSearchFocus" runat="server" />
            <!-- #### kotak pencarian #### -->
            <div id="testpopup" style="display: none; position: absolute; border: 1px groove; min-width: 600px; min-height: 200px; background-color: white; max-height: 250px; max-width: 800px; margin-left: 15px; z-index: 10;">
                <asp:UpdatePanel runat="server" ID="updatepanelpencarianobat">
                    <ContentTemplate>
                        <div class="col-sm-6" style="padding: 5px; width: 258px;">
                            <asp:TextBox runat="server" ID="txtSearchItem" CssClass="form-control" onkeydown="return txtOnKeyPressOrder();"></asp:TextBox>
                        </div>
                        <div class="col-sm-1" style="padding: 5px">
                            <asp:Button ID="buttonOrderSearch" runat="server" OnClick="btnFind_click" CssClass="btn btn-warning btn-emr-small" Text="Find"></asp:Button>
                        </div>

                        <div style="overflow-y: auto; max-height: 200px; max-width: 800px; min-width: 600px; min-height: 200px;" class="scrollEMR">
                            <asp:GridView ID="gvw_data" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed" BorderColor="Black"
                                HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                                ShowHeaderWhenEmpty="True" DataKeyNames="salesItemId" EmptyDataText="No Data"
                                AllowSorting="True">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Item" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <%--<HeaderTemplate>
                                                    <label style="display: <%=setENG%>;">Item </label>
                                                    <label style="display: <%=setIND%>;">Obat </label>
                                                </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="salesItemId" runat="server" Text='<%# Bind("salesItemId") %>' Visible="false"><%# Eval("salesItemId").ToString() %></asp:Label>
                                            <asp:Button ID="salesItemName" BackColor="Transparent" Font-Bold="true" runat="server" BorderColor="Transparent" Text='<%# Bind("salesItemName") %>' OnClick="itemselected_onclick" OnClientClick="OnClick();" />
                                            <asp:HiddenField ID="hfUomId" Value='<%# Bind("salesUomId") %>' runat="server" />
                                            <asp:HiddenField ID="hfUomCode" Value='<%# Bind("salesUomCode") %>' runat="server" />
                                            <asp:HiddenField ID="hfDose" Value='<%# Eval("Dose") %>' runat="server" />
                                            <asp:HiddenField ID="hfDoseUomId" Value='<%# Bind("DoseUomId") %>' runat="server" />
                                            <asp:HiddenField ID="hfFrequencyId" Value='<%# Bind("AdministrationFrequencyId") %>' runat="server" />
                                            <asp:HiddenField ID="hfRouteId" Value='<%# Bind("AdministrationRouteId") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="activeIngredientsName"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Active Ingredients" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                                        <%--<HeaderTemplate>
                                                    <label style="display: <%=setENG%>;">Active Ingredients </label>
                                                    <label style="display: <%=setIND%>;">Bahan Aktif </label>
                                                </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="LabelActiveIng" runat="server" Text='<%# Bind("activeIngredientsName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="formularium" runat="server" Text='<%# Bind("Formularium") %>' Visible="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Formularium" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="Formularium"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- #### end kotak pencarian #### -->

        </div>

        <!-- ==== END Normal Search ==== -->

        <!-- ==== Auto Complete Ajax ==== -->

        <div class="col-sm-12" style="width: 100%; background-color: white; padding-left: 0px; padding-right: 0px;" id="Div4" runat="server">

            <!-- #### textbox search #### -->
            <div id="search_drugs" runat="server">

                <div class="row">
                    <div class="col-sm-6">
                        <div id="txt_search" runat="server">
                            <asp:UpdatePanel runat="server" ID="updatepanelajaxsearch">
                                <ContentTemplate>

                                    <div class="row" style="padding-left: 15px; padding-bottom: 5px;">
                                        <div class="col-sm-5" style="padding-right: 0px;">
                                            <div class="has-feedback">
                                                <input type="text" id="TextBoxACSearch" placeholder="Add item here..." class="autosuggest" style="width: 100%;" onkeydown="return txtOnKeyPressFalse();" onfocus="setflagloading('orderset','true');" />
                                                <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px;"></span>
                                            </div>
                                            <asp:HiddenField ID="HF_flagfocussearch" runat="server" />
                                            <asp:HiddenField ID="HF_ItemSelected" runat="server" />
                                            <asp:Button ID="ButtonAjaxSearch" runat="server" Text="Button" CssClass="hidden" OnClick="ButtonAjaxSearch_Click" />
                                        </div>
                                        <div class="col-sm-3">

                                            <div class="loadingOS" style="display: none;">
                                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                </div>
                                                &nbsp;
                                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                <asp:HiddenField ID="HFloadingorderset" runat="server" />
                                            </div>

                                            <%--<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updatepanelajaxsearch">
                                                    <ProgressTemplate>
                                                        <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>--%>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-sm-6 text-right">
                    </div>

                </div>

            </div>
            <!-- #### end button save, cancel, textbox search #### -->

        </div>

        <!-- ==== END Auto Complete Ajax ==== -->

        <!-- #### END BOTTOM SECTION #### -->


        <!-- #### EXPERIMENT SEARCH #### -->

        <!-- ==== Fuzzy Search ==== -->

        <div class="col-sm-12" style="width: 100%; background-color: white; padding-left: 0px; padding-right: 0px; display: none;" id="Div1" runat="server">

            <!-- #### button save, cancel, textbox search #### -->
            <div id="Div2" runat="server">

                <div class="row">
                    <div class="col-sm-6">
                        <div id="Div3" runat="server">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>

                                    <div class="row" style="padding-left: 15px; padding-bottom: 5px;">
                                        <div class="col-sm-6" style="padding-right: 35px;">
                                            <div class="has-feedback">
                                                <asp:TextBox ID="TextBox1" Width="100%" placeholder="Levenstein Search..." runat="server" ReadOnly="true" OnClick="OnClickXX();"></asp:TextBox>
                                                <span class="fa fa-caret-down form-control-feedback" style="margin-top: -6px;"></span>
                                            </div>
                                            <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatepanelpencarianobat">
                                                <ProgressTemplate>
                                                    <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-sm-6 text-right">
                    </div>
                </div>

            </div>
            <!-- #### end button save, cancel, textbox search #### -->

            <asp:HiddenField ID="HiddenField1" runat="server" />
            <!-- #### kotak pencarian #### -->
            <div id="testpopupXX" style="display: none; position: absolute; border: 1px groove; min-width: 600px; min-height: 200px; background-color: white; max-height: 250px; max-width: 800px; margin-left: 15px; z-index: 9;">
                <asp:UpdatePanel runat="server" ID="updatepanel2">
                    <ContentTemplate>
                        <div class="col-sm-6" style="padding: 5px; width: 258px;">
                            <asp:TextBox runat="server" ID="txtSearchItemXX" CssClass="form-control" onkeydown="return txtOnKeyPressOrderXX();"></asp:TextBox>
                        </div>
                        <div class="col-sm-1" style="padding: 5px">
                            <asp:Button ID="buttonFindXX" runat="server" OnClick="buttonFindXX_Click" CssClass="btn btn-warning btn-emr-small" Text="Find"></asp:Button>
                        </div>

                        <div style="overflow-y: auto; max-height: 200px; max-width: 800px; min-width: 600px; min-height: 200px;" class="scrollEMR">
                            <asp:GridView ID="GridViewXX" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed" BorderColor="Black"
                                HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                                ShowHeaderWhenEmpty="True" DataKeyNames="salesItemId" EmptyDataText="No Data"
                                AllowSorting="True">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Item" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <%--<HeaderTemplate>
                                                    <label style="display: <%=setENG%>;">Item </label>
                                                    <label style="display: <%=setIND%>;">Obat </label>
                                                </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="salesItemId" runat="server" Text='<%# Bind("salesItemId") %>' Visible="false"><%# Eval("salesItemId").ToString() %></asp:Label>
                                            <asp:Button ID="salesItemName" BackColor="Transparent" Font-Bold="true" runat="server" BorderColor="Transparent" Text='<%# Bind("salesItemName") %>' OnClick="itemselected_onclick" OnClientClick="OnClick();" />
                                            <asp:HiddenField ID="hfUomId" Value='<%# Bind("salesUomId") %>' runat="server" />
                                            <asp:HiddenField ID="hfUomCode" Value='<%# Bind("salesUomCode") %>' runat="server" />
                                            <asp:HiddenField ID="hfDose" Value='<%# Eval("Dose") %>' runat="server" />
                                            <asp:HiddenField ID="hfDoseUomId" Value='<%# Bind("DoseUomId") %>' runat="server" />
                                            <asp:HiddenField ID="hfFrequencyId" Value='<%# Bind("AdministrationFrequencyId") %>' runat="server" />
                                            <asp:HiddenField ID="hfRouteId" Value='<%# Bind("AdministrationRouteId") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="activeIngredientsName"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Active Ingredients" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                                        <%--<HeaderTemplate>
                                                    <label style="display: <%=setENG%>;">Active Ingredients </label>
                                                    <label style="display: <%=setIND%>;">Bahan Aktif </label>
                                                </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="LabelActiveIng" runat="server" Text='<%# Bind("activeIngredientsName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="formularium" runat="server" Text='<%# Bind("Formularium") %>' Visible="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Formularium" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="Formularium"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- #### end kotak pencarian #### -->

        </div>

        <!-- ==== END Fuzzy Search ==== -->

        <!-- #### END EXPERIMENT SEARCH #### -->



        <!-- #### MODAL #### -->

        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" onabort="modal_onclose">
            <div class="modal-dialog" style="width: 30%;" runat="server">
                <div class="modal-content" style="border-radius: 7px">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align: center">
                            <asp:Label ID="Label1" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server" Text="Save Order Set"></asp:Label></h4>
                    </div>

                    <div class="modal-body" style="background-color: white; text-align: center">
                        <h5>There are items that have <u>different Formularium</u>,<br />
                            are you sure you want to save? &nbsp;</h5>
                        <asp:Label runat="server" ID="warning_text"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                        <asp:Button ID="btn_save_orderset" aria-hidden="true" OnClientClick="this.disabled = true; this.value = 'Loading...';"
                            UseSubmitBehavior="false" CssClass="btn btn-danger" runat="server" Text="Save" OnClick="btnSave_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalData" tabindex="-1" role="dialog" aria-labelledby="modalDataLabel" aria-hidden="true" onabort="modal_onclose">
            <div class="modal-dialog" style="width: 50%;" runat="server">
                <div class="modal-content" style="border-radius: 7px">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align: center">
                            <asp:Label ID="Label2" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server" Text="Save Order Set"></asp:Label></h4>
                    </div>

                    <div class="modal-body" style="background-color: white; text-align: center">
                        <h5>This action can not done. Item's quantity is 0 or there are same Item. &nbsp;</h5>
                        <asp:Label runat="server" ID="Label3"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                        <asp:Button ID="Button1" aria-hidden="true" OnClientClick="this.disabled = true; this.value = 'Loading...';"
                            UseSubmitBehavior="false" CssClass="btn btn-danger" runat="server" Text="Save" OnClick="btnSave_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>

        <!-- #### END MODAL #### -->

        <!-- #### END CREATE #### -->
    </div>



</asp:Content>
