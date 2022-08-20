<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdSubjective.ascx.cs" Inherits="Form_SOAP_Control_Template_StdSubjective" %>

<script type="text/javascript">

    //function ShowDivRoutine() {
    //    $("[id$='btnMedShow']").click();
    //}

    //function ShowDivAllergy() {
    //    $("[id$='btnAllergyDrugShow']").click();
    //}

    //function ShowDivFood() {
    //    $("[id$='btnFoodAllergyShow']").click();
    //}

    function ShowHideDiv() {
        var chkYes = document.getElementById("<%=rbPengobatan2.ClientID %>");
        var dvPassport = document.getElementById("dvPengobatan");
        if (chkYes.checked) {
            dvPassport.style.display = "";
        }
    }

    function HideDiv() {
        var chkYes = document.getElementById("<%=rbPengobatan1.ClientID %>");
        var dvPassport = document.getElementById("dvPengobatan");
        if (chkYes.checked) {
            dvPassport.style.display = "none";
        }
    }

    function ShowHideDiv2() {
        var chkYes = document.getElementById("<%=rbpribadi2.ClientID %>");
        var dvPassport = document.getElementById("dvPenyakitPribadi");
        if (chkYes.checked) {
            dvPassport.style.display = "";
        }
    }

    function HideDiv2() {
        var chkYes = document.getElementById("<%=rbpribadi1.ClientID %>");
        var dvPassport = document.getElementById("dvPenyakitPribadi");
        if (chkYes.checked) {
            dvPassport.style.display = "none";
        }
    }

    function ShowHideDiv3() {
        var chkYes = document.getElementById("<%=rbkeluarga2.ClientID %>");
        var dvPassport = document.getElementById("dvPenyakitKeluarga");
        if (chkYes.checked) {
            dvPassport.style.display = "";
        }
    }

    function HideDiv3() {
        var chkYes = document.getElementById("<%=rbkeluarga1.ClientID %>");
        var dvPassport = document.getElementById("dvPenyakitKeluarga");
        if (chkYes.checked) {
            dvPassport.style.display = "none";
        }
    }

    function ShowHideDiv4() {
        var chkYes = document.getElementById("<%=rbkunjungan2.ClientID %>");
        var dvPassport = document.getElementById("dvEndemic");
        if (chkYes.checked) {
            dvPassport.style.display = "";
        }
    }

    function HideDiv4() {
        var chkYes = document.getElementById("<%=rbkunjungan1.ClientID %>");
        var dvPassport = document.getElementById("dvEndemic");
        if (chkYes.checked) {
            dvPassport.style.display = "none";
        }
    }

    function ShowHideDiv5() {
        var chkYes = document.getElementById("<%=rbnutrisi2.ClientID %>");
        var dvPassport = document.getElementById("dvnutrisi");
        if (chkYes.checked) {
            dvPassport.style.display = "";
        }
    }

    function HideDiv5() {
        var chkYes = document.getElementById("<%=rbnutrisi1.ClientID %>");
        var dvPassport = document.getElementById("dvnutrisi");
        if (chkYes.checked) {
            dvPassport.style.display = "none";
        }
    }

    function ShowHideDiv6() {
        var chkYes = document.getElementById("<%=rbpuasa2.ClientID %>");
        var dvPassport = document.getElementById("dvPuasa");
        if (chkYes.checked) {
            dvPassport.style.display = "";
        }
    }

    function HideDiv6() {
        var chkYes = document.getElementById("<%=rbpuasa1.ClientID %>");
        var dvPassport = document.getElementById("dvPuasa");
        if (chkYes.checked) {
            dvPassport.style.display = "none";
        }
    }

    function ShowHideDiv7() {
        document.getElementById('<%=txtSurgeryName.ClientID %>').value = "";
        document.getElementById('<%=txtSurgeryDate.ClientID %>').value = "";
        var chkYes = document.getElementById("<%=rbOperas2.ClientID %>");
        var dvPassport = document.getElementById("dvoperasi");
        if (chkYes.checked) {
            dvPassport.style.display = "";
        }
    }

    function HideDiv7() {
        var chkYes = document.getElementById("<%=rbOperasi.ClientID %>");
        var dvPassport = document.getElementById("dvoperasi");
        if (chkYes.checked) {
            dvPassport.style.display = "none";
        }
    }

    function ShowHideDiv8() {
        document.getElementById('<%=txtDrugsAllergy.ClientID %>').value = "";
        document.getElementById('<%=txtReactionAllergy.ClientID %>').value = "";
        var chkYes = document.getElementById("<%=rbdrug2.ClientID %>");
        var dvPassport = document.getElementById("dvdrugs");
        if (chkYes.checked) {
            dvPassport.style.display = "";
        }
    }

    function HideDiv8() {
        var chkYes = document.getElementById("<%=rbdrug1.ClientID %>");
        var dvPassport = document.getElementById("dvdrugs");
        if (chkYes.checked) {
            dvPassport.style.display = "none";
        }
    }

    function ShowHideDiv9() {
        document.getElementById('<%=txtDrugsFoods.ClientID %>').value = "";
        document.getElementById('<%=txtReactionFoods.ClientID %>').value = "";
        var chkYes = document.getElementById("<%=rbfood2.ClientID %>");
        var dvPassport = document.getElementById("dvfoods");
        if (chkYes.checked) {
            dvPassport.style.display = "";
        }
    }

    function HideDiv9() {
        var chkYes = document.getElementById("<%=rbfood1.ClientID %>");
        var dvPassport = document.getElementById("dvfoods");
        if (chkYes.checked) {
            dvPassport.style.display = "none";
        }
    }

    function datesurgery() {
    var dp = $('#<%=txtSurgeryDate.ClientID%>');
        dp.datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd M yyyy",
            language: "tr"
        }).on('changeDate', function (ev) {
            $(this).blur();
            $(this).datepicker('hide');
            });
    }

    function txtOnKeyPress()
    {
       var c = event.keyCode;
       if (c == 13)
       {
           return false;
       }
    }

    function txtOnKeyPressSurgery()
    {
       var c = event.keyCode;
       if (c == 13)
       {
           var UserName = $("[id$='txtSurgeryName']").val();
           var PassWord = $("[id$='txtSurgeryDate']").val();

            $("[id$='txtSurgeryName']").removeAttr("style");
            $("[id$='txtSurgeryDate']").removeAttr("style");

           if (UserName.length <= 0 && PassWord.length > 0) {
               $("[id$='txtSurgeryName']").attr("style", "display:block; border-color:red;max-width:90%");
               $("[id$='txtSurgeryName']").focus();
               return false;
           }
           else if (UserName.length > 0 && PassWord.length <= 0) {
               $("[id$='txtSurgeryDate']").attr("style", "display:block; border-color:red;max-width:90%");
               $("[id$='txtSurgeryDate']").focus();
               return false;
           }
           else if (UserName.length <= 0 && PassWord.length <= 0) {
               $("[id$='txtSurgeryName']").attr("style", "display:block; border-color:red;max-width:90%");
               $("[id$='txtSurgeryDate']").attr("style", "display:block; border-color:red;max-width:90%");
               $("[id$='txtSurgeryName']").focus();
               return false;
           }
           else {
                $("[id$='txtSurgeryName']").attr("style", "max-width:90%");
               $("[id$='txtSurgeryDate']").attr("style", "max-width:90%");
               document.getElementById('<%=btnSurgery.ClientID%>').click();
           }
            return false;
       }
    }

    function txtOnKeyPressRoutine()
    {
       var c = event.keyCode;
       if (c == 13)
       {
           var UserName = $("[id$='txtRoutineMed']").val();

            $("[id$='txtRoutineMed']").removeAttr("style");

           if (UserName.length <= 0) {
               $("[id$='txtRoutineMed']").attr("style", "display:block; border-color:red;max-width:90%;width: 90%");
               $("[id$='txtRoutineMed']").focus();
               return false;
           }
           else {
                $("[id$='txtRoutineMed']").attr("style", "max-width:90%;width: 90%");
               document.getElementById('<%=btnRoutineMed.ClientID%>').click();
           }
            return false;
       }
    }

    function CheckisEmptyText() {
            var UserName = $("[id$='txtRoutineMed']").val();

            $("[id$='txtRoutineMed']").removeAttr("style");
			$("[id$='txtRoutineMed']").attr("style", "display:block; max-width:90%;width: 90%");
            if (UserName.length <= 0) {
                $("[id$='txtRoutineMed']").attr("style", "display:block; border-color:red;max-width:90%;width: 90%");
                $("[id$='txtRoutineMed']").focus();
                return false;
            }
            else
                return true;

    }

    function txtOnKeyPressDrugsAllergy()
    {
       var c = event.keyCode;
       if (c == 13)
       {
           var UserName = $("[id$='txtDrugsAllergy']").val();
           var PassWord = $("[id$='txtReactionAllergy']").val();

            $("[id$='txtDrugsAllergy']").removeAttr("style");
            $("[id$='txtReactionAllergy']").removeAttr("style");

           if (UserName.length <= 0 && PassWord.length > 0) {
               $("[id$='txtDrugsAllergy']").attr("style", "display:block; border-color:red;max-width:90%");
               $("[id$='txtDrugsAllergy']").focus();
               return false;
           }
           else if (UserName.length > 0 && PassWord.length <= 0) {
               $("[id$='txtReactionAllergy']").attr("style", "display:block; border-color:red;max-width:90%");
               $("[id$='txtReactionAllergy']").focus();
               return false;
           }
           else if (UserName.length <= 0 && PassWord.length <= 0) {
               $("[id$='txtDrugsAllergy']").attr("style", "display:block; border-color:red;max-width:90%");
               $("[id$='txtReactionAllergy']").attr("style", "display:block; border-color:red;max-width:90%");
               $("[id$='txtDrugsAllergy']").focus();
               return false;
           }
           else {
                $("[id$='txtDrugsAllergy']").attr("style", "max-width:90%");
               $("[id$='txtReactionAllergy']").attr("style", "max-width:90%");
               document.getElementById('<%=btnDrugsAllergy.ClientID%>').click();
           }
            return false;
       }
    }

    function txtOnKeyPressFoodsAllergy()
    {
       var c = event.keyCode;
       if (c == 13)
       {
           var UserName = $("[id$='txtDrugsFoods']").val();
           var PassWord = $("[id$='txtReactionFoods']").val();

            $("[id$='txtDrugsFoods']").removeAttr("style");
            $("[id$='txtReactionFoods']").removeAttr("style");

           if (UserName.length <= 0 && PassWord.length > 0) {
               $("[id$='txtDrugsFoods']").attr("style", "display:block; border-color:red;max-width:90%");
               $("[id$='txtDrugsFoods']").focus();
               return false;
           }
           else if (UserName.length > 0 && PassWord.length <= 0) {
               $("[id$='txtReactionFoods']").attr("style", "display:block; border-color:red;max-width:90%");
               $("[id$='txtReactionFoods']").focus();
               return false;
           }
           else if (UserName.length <= 0 && PassWord.length <= 0) {
               $("[id$='txtDrugsFoods']").attr("style", "display:block; border-color:red;max-width:90%");
               $("[id$='txtReactionFoods']").attr("style", "display:block; border-color:red;max-width:90%");
               $("[id$='txtDrugsFoods']").focus();
               return false;
           }
           else {
                $("[id$='txtDrugsFoods']").attr("style", "max-width:90%");
               $("[id$='txtReactionFoods']").attr("style", "max-width:90%");
               document.getElementById('<%=btnFoodAllergy.ClientID%>').click();
           }
            return false;
       }
    }


    function hidetext(val1, val2, val3) {
        var txttemp = $("[id$='"+val1+"']").val();
        var chkYes = $("[id$='"+val2+"']");
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
        else
        {
            var GridId = "<%=gvw_routinemed.ClientID %>";
        }
        var grid = document.getElementById(GridId);
        rowscount = grid.rows.length;
        if (rowscount > 1) {
            var chkYes = $("[id$='"+val2+"']");
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
        if (chkYes1.checked || chkYes2.checked || chkYes3.checked || chkYes4.checked || chkYes5.checked || chkYes6.checked ||
            chkYes7.checked || chkYes8.checked || chkYes9.checked || chkYes10.checked || txttemp.length>0) {
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
        if (chkYes1.checked || chkYes2.checked || chkYes3.checked || chkYes4.checked || chkYes5.checked || txttemp.length>0) {
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

    function classdown() {
            if (document.getElementById("icondown1").className == "glyphicon glyphicon-chevron-right") {
                $("[id$='icondown1']").removeClass('glyphicon glyphicon-chevron-right');
                $("[id$='icondown1']").addClass('glyphicon glyphicon-chevron-down');
                $("[id$='valAccordion']").val('1');
            }
            else {
                $("[id$='icondown1']").removeClass('glyphicon glyphicon-chevron-down');
                $("[id$='icondown1']").addClass('glyphicon glyphicon-chevron-right');
                $("[id$='valAccordion']").val('0');
            }
        }
    function checkAccordion() {
        if ($("[id$='valAccordion']").val() == '1') {
            $("[id$='collapseRiwayat']").removeClass('panel-collapse collapse');
            $("[id$='collapseRiwayat']").addClass('panel-collapse collapse in');
            $("[id$='icondown1']").removeClass('glyphicon glyphicon-chevron-right');
            $("[id$='icondown1']").addClass('glyphicon glyphicon-chevron-down');
        }

    }


</script>
<style>
    .itemlab {
    font-size:12px;
    font-family: Helvetica;
    font-weight:normal;
    padding-top:0px;
    margin-bottom:0px;
    }
    .mycheckbox input[type="checkbox"]
    {
        margin-right: 5%;
    }
	.mycheckboxFA input[type="checkbox"]
	{
		margin-right: 10px;
	}

    .subheader {
    font-family:Helvetica, Arial, sans-serif;
    font-weight:bold;
    font-size:12px;
    font-style:italic
    }

    .hidden { display: none; }
</style>

<asp:HiddenField runat="server" ID="valAccordion" Value="0"></asp:HiddenField>
<div class="panel-group" id="accordion" style="margin:0px">
    <div class="panel panel-default" style="border:0px; border-radius:6px 6px  0 0;">
    <a data-toggle="collapse" data-parent="#accordion" href="#collapseRiwayat">
        <div style="padding-left:15px;padding-right:15px">
		<label class="form-control headerpanel" onclick="classdown();" style="margin:0px;border:none;padding-right:0px;padding-left:0px;padding-top: 1‒;padding-bottom: 5px;height: 27px;padding-top: 6px;font-size: 12px;"><span aria-hidden="true" id="icondown1" class="glyphicon glyphicon-chevron-right" style="padding-right:10px"></span>First Assessment</label>
		<hr style="border-color:#cdced9;margin-top:0px;margin-bottom:-1px;padding-bottom:0px;padding-left:0px">
		</div>
		</a>
    <div id="collapseRiwayat" class="panel-collapse collapse">
        <div class="panel-body">
    <%-- =============================================== PENGOBATAN SAAT INI =============================================== --%>
            <asp:UpdatePanel runat="server" ID="upCurrMedication">
            <ContentTemplate>
<%--                <asp:Button ID ="btnMedNone" runat="server" CssClass="hidden" OnClick="btnRoutineMedNone"/>
                <asp:Button ID ="btnMedShow" runat="server" CssClass="hidden" OnClick="btnRoutineMedShow"/>--%>
            <h6><strong>Routine Medication<label class="subheader" style="font-size:10px"> (Pengobatan saat ini)</label></strong></h6>
            <div>
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective1"  Value="0" id="rbPengobatan1" Checked="true" onclick="hidegrid(4,'rbPengobatan2','dvPengobatan')" /> No </label>
            </div>
            <div class="row">
                <div class="col-xs-1" style="padding-right:0px">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective1"  Value="1" id="rbPengobatan2" onclick="ShowHideDiv()" /> Yes </label>
                </div>
            </div>
            <div id="dvPengobatan">
                <%--<asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;max-width:90%;width:90%;resize:none;padding-left:5px;" placeholder="Type here..."  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtPengobatan" TextMode="MultiLine" Rows="3" />--%>
                    <div class="row">
                        <div class="col-sm-2" style="padding-right:0px;margin-right:0px;width: 315px;">
                            <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height: 24px;max-width:90%;width:90%;resize:none;padding-left:5px;" placeholder="Nama Obat"  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtRoutineMed" onkeydown="return txtOnKeyPressRoutine();" />
                        </div>
                        <div class="col-sm-2" style="padding-left:0px;margin-left:0px;">
                            <asp:Button runat="server" style="width: 56px;height: 24px;border-radius: 4px;background-color: #2a3593;color:#ffffff"  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="btnRoutineMed" Text="Add" OnClientClick="return CheckisEmptyText();" onclick="btnAddRoutineMed_onClick" />
                        </div>
                    </div>

                <div class="row">
                    <div class="col-sm-3" style="max-height:220px;overflow-y:auto;min-width:500px;max-width:700px">
                        <br />
						<div style="min-width:400px;max-width:700px">
                        <asp:GridView ID="gvw_routinemed" runat="server" AutoGenerateColumns="False"  CssClass="table table-bordered table-condensed"
                    DataKeyNames="patient_routine_medication_id" EmptyDataText="No Data" >
                <PagerStyle CssClass="pagination-ys" />
                    <Columns>
                        <asp:TemplateField HeaderText="Nama Obat" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="50%" HeaderStyle-Font-Size="11px">
                        <ItemTemplate>
                            <asp:HiddenField ID="patient_routine_medication_id" runat="server" Value='<%# Bind("patient_routine_medication_id") %>'></asp:HiddenField>
                            <asp:Label  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="medication" runat="server"  Text='<%# Bind("medication") %>'></asp:Label>
                        </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField  ItemStyle-Width="5%" HeaderStyle-Font-Size="11px">
                        <ItemTemplate>
                            <asp:Button  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="x" OnClick="btnDeleteRoutineMed_onClick" ></asp:Button>
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
            <%-- =============================================== END PENGOBATAN SAAT INI =============================================== --%>
        <%-- =============================================== RIWAYAT OPERASI =============================================== --%>
        <br />
		<asp:updatepanel runat="server" ID="upSurgery">
        <ContentTemplate>
    <h6><strong>Surgery History<label class="subheader" style="font-size:10px"> (Riwayat operasi)</label></strong></h6>
    <div>
        <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective2"  Value="0" id="rbOperasi" Checked="true" onclick="hidegrid(1,'rbOperas2','dvoperasi')" /> No </label>
    </div>
    <div class="row">
        <div class="col-xs-1" style="padding-right:0px">
            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective2"  Value="1" id="rbOperas2" onclick="ShowHideDiv7()" /> Yes </label>
        </div>
    </div>

    <div id="dvoperasi" style="display:none">
            <div class="row">
                <div class="col-sm-2" style="padding-right:0px;margin-right:0px;">
                    <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height: 24px;max-width:90%;width:90%;resize:none;padding-left:5px;" placeholder="Nama operasi"  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtSurgeryName" onkeydown="return txtOnKeyPressSurgery();" />
                </div>
                <div class="col-sm-2" style="padding-left:0px;margin-left:0px;padding-right:0px">
                    <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height: 24px;max-width:90%;width:90%;resize:none;padding-left:5px;" placeholder="Tanggal operasi"  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px" onmousedown="datesurgery();" ID="txtSurgeryDate" onkeydown="return txtOnKeyPressSurgery();" />
                </div>
                <div class="col-sm-2" style="padding-left:0px;margin-left:0px;">
                    <asp:Button runat="server" style="width: 56px;height: 24px;border-radius: 4px;background-color: #2a3593;color:#ffffff"  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="btnSurgery" Text="Add" OnClick="btnAddSurgery_onClick" />
                </div>
            </div>
        <div class="row">
            <div class="col-sm-6"  style="max-height:220px;overflow-y:auto;">
                <br />


                <asp:GridView ID="gvw_surgery" runat="server" AutoGenerateColumns="False"  CssClass="table table-bordered table-condensed"
            DataKeyNames="patient_surgery_id" EmptyDataText="No Data" >
        <PagerStyle CssClass="pagination-ys" />
            <Columns>
                <asp:TemplateField HeaderText="Nama Operasi" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="50%" HeaderStyle-Font-Size="11px">
                <ItemTemplate>
                    <asp:HiddenField ID="patient_surgery_id" runat="server" Value='<%# Bind("patient_surgery_id") %>'></asp:HiddenField>
                    <asp:Label  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="surgery_type" runat="server"  Text='<%# Bind("surgery_type") %>'></asp:Label>
                </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Tanggal Operasi" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="30%" HeaderStyle-Font-Size="11px">
                <ItemTemplate>
                    <asp:Label  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="surgery_date" runat="server"  Text='<%# Bind("surgery_date") %>'></asp:Label>
                </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField  ItemStyle-Width="5%" HeaderStyle-Font-Size="11px">
                <ItemTemplate>
                    <asp:Button  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="x" OnClick="btnDeleteSurgery_onClick" ></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>

            </div>
        </div>
        </div>
</ContentTemplate>
    </asp:updatepanel>
             <%-- =============================================== END RIWAYAT OPERASI =============================================== --%>

                <%-- =============================================== RIWAYAT PENYAKIT DAHULU =============================================== --%>
                <asp:updatepanel runat="server" ID="upPribadi">
                <ContentTemplate>
               <h6><strong>Disease History<label class="subheader" style="font-size:10px"> (Riwayat penyakit dahulu)</label></strong></h6>
               <div>
                   <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective3"  Value="0" id="rbpribadi1" Checked="true" onclick="hidecheckboxes()" /> No </label>
               </div>
               <div>
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective3"  Value="1" id="rbpribadi2" onclick="ShowHideDiv2()" /> Yes </label>
                </div>
               <div id="dvPenyakitPribadi" style="display:none">
                   <div class="row"  >
                    <div class="col-xs-2" style="width:230px;">
                        <div style="padding-top:5px">
                            <label class="itemlab" style="margin-left:15px"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  Text="Hypertension (Darah Tinggi)"  ID="chkdisease1" /></label>
                        </div>
                        <div>
                            <label class="itemlab" style="margin-left:15px"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  style="padding-right:10px"  Text="Stroke" ID="chkdisease2"  /></label>
                        </div>
                    </div>
                    <div class="col-xs-2" style="width:220px;padding:0px;margin:0px;">
                        <div style="padding-top:5px">
                            <label class="itemlab"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  Text="TBC" ID="chkdisease3"  /></label>
                        </div>
                        <div>
                            <label class="itemlab"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  style="padding-right:10px"  Text="Kidney Failure (Gagal Ginjal)" ID="chkdisease4"  /></label>
                        </div>
                    </div>
                    <div class="col-xs-2" style="width:220px;padding:0px;margin:0px;">
                        <div style="padding-top:5px">
                            <label class="itemlab"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  Text="Convulsive (Kejang)" ID="chkdisease5"  /></label>
                        </div>
                        <div>
                            <label class="itemlab"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  style="padding-right:10px"  Text="Heart Failure (Gagal Jantung)"  ID="chkdisease6" /></label>
                        </div>
                    </div>
                    <div class="col-xs-2" style="width:120px;padding:0px;margin:0px;">
                        <div style="padding-top:5px">
                            <label class="itemlab"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  Text="Diabetes" ID="chkdisease7"  /></label>
                        </div>
                        <div>
                            <label class="itemlab"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  style="padding-right:10px"  Text="Asthma (Asma)" ID="chkdisease8"  /></label>
                        </div>
                    </div>
                    <div class="col-xs-2" style="width:120px;padding:0px;margin:0px;">
                        <div style="padding-top:5px">
                            <label class="itemlab"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  Text="Hepatitis" ID="chkdisease9"  /></label>
                        </div>
                        <div>
                            <label class="itemlab"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  style="padding-right:10px"  Text="Cancer (Kanker)" ID="chkdisease10"  /></label>
                        </div>
                    </div>
                </div>
                   <div class="row">
                       <div class="col-xs-1" style="padding-right:0px;padding-left:2.5%">
                           <div>
                               <label class="itemlab">Others</label>
                           </div>
                           <div>
                               <label class="itemlab">(Lain-lain)</label>
                           </div>
                       </div>
                       <div class="col-xs-7">
                           <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;max-width:90%;width:90%;resize:none;padding-left:5px;" placeholder="Type here..."  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtDisease" TextMode="MultiLine" Rows="2" />
                       </div>
                    </div>
                   </div>
                    <br />
					</ContentTemplate>
                </asp:updatepanel>
                <asp:updatepanel runat="server" ID="upKeluarga">
                <ContentTemplate>
               <h6><strong>Family Disease History<label class="subheader" style="font-size:10px"> (Riwayat penyakit keluarga)</label></strong></h6>
               <div>
                   <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective4"  Value="0" id="rbkeluarga1" Checked="true" onclick="hidecheckboxesfam()" /> No </label>
               </div>
               <div>
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective4"  Value="1" id="rbkeluarga2" onclick="ShowHideDiv3()"  /> Yes </label>
                </div>

               <div id="dvPenyakitKeluarga" style="display:none">
                   <div class="row"  >
                    <div class="col-xs-2" style="width:230px;">
                        <div style="padding-top:5px">
                            <label class="itemlab" style="margin-left:15px"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  Text="Heart Failure (Gagal Jantung)" ID="chkdiseasefam1"  /></label>
                        </div>
                    </div>
                    <div class="col-xs-2" style="width:120px;padding:0px;margin:0px;">
                        <div style="padding-top:5px">
                            <label class="itemlab"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  Text="Diabetes" ID="chkdiseasefam2"  /></label>
                        </div>
                    </div>
                    <div class="col-xs-2" style="width:120px;padding:0px;margin:0px;">
                        <div style="padding-top:5px">
                            <label class="itemlab"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  Text="Asthma (Asma)" ID="chkdiseasefam3"  /></label>
                        </div>
                    </div>
                    <div class="col-xs-2" style="width:220px;padding:0px;margin:0px;">
                        <div style="padding-top:5px">
                            <label class="itemlab"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  Text="Hypertension (Darah Tinggi)" ID="chkdiseasefam4"  /></label>
                        </div>
                    </div>
                    <div class="col-xs-2" style="width:120px;padding:0px;margin:0px;">
                        <div style="padding-top:5px">
                            <label class="itemlab"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  Text="Cancer (Kanker)" ID="chkdiseasefam5"  /></label>
                        </div>
                    </div>
                </div>
                   <div class="row">
                       <div class="col-xs-1" style="padding-right:0px;padding-left:2.5%">
                           <div>
                               <label class="itemlab">Others</label>
                           </div>
                           <div>
                               <label class="itemlab">(Lain-lain)</label>
                           </div>
                       </div>
                       <div class="col-xs-7">
                           <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;max-width:90%;width:90%;resize:none;padding-left:5px;" placeholder="Type here..."  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtDiseaseFam" TextMode="MultiLine" Rows="2" />
                       </div>
                    </div>
                   </div>
                    </ContentTemplate>
                    </asp:updatepanel>
               <%-- =============================================== END RIWAYAT PENYAKIT DAHULU =============================================== --%>

                 <%-- =============================================== KUNJUNGAN DAERAH =============================================== --%>
                <br />
				<asp:updatepanel runat="server" ID="upEndemic">
                <ContentTemplate>
               <h6><strong>Have been to endemic area<label class="subheader" style="font-size:10px"> (Kunjungan ke daerah endemis dalam 3 bulan terakhir)</label></strong></h6>
               <div>
                   <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective5"  Value="0" id="rbkunjungan1" Checked="true" onclick="hidetext('txtEndemic','rbkunjungan2','dvEndemic')" /> No </label>
               </div>
               <div>
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective5"  Value="1" id="rbkunjungan2" onclick="ShowHideDiv4()"  /> Yes </label>
                </div>
               <div id="dvEndemic" style="display:none;padding-bottom:15px">
                   <div class="row">
                       <div class="col-xs-7">
                           <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;max-width:90%;width:90%;resize:none;padding-left:5px;" placeholder="Type here..."  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtEndemic" TextMode="MultiLine" Rows="2" />
                       </div>
                    </div>
                   </div>
                    </ContentTemplate>
                    </asp:updatepanel>
               <%-- ===============================================END KUNJUNGAN DAERAH =============================================== --%>
              <%-- =============================================== SKRINING PENYAKIT =============================================== --%>
              <br />
               <h6 style="margin:0px;"><strong>Screening Infectius Disease<label class="subheader" style="font-size:10px"> (Skrining penyakit infeksius. Apakah pasien memiliki kondisi/gejala klinis sebagai berikut:)</label></strong></h6>
               <div class="row">
                    <div class="col-sm-5" style="padding:0px;margin:0px;">
                        <div style="padding-top:5px">
                            <label class="itemlab" style="margin-left:15px"><asp:CheckBox runat="server"  CssClass="mycheckboxFA" style="padding-right:10px" Text="Temperature >38 C " ID="chkScreen1"  /><label style="font-style:italic">(Ada kenaikan suhu tubuh >38  C)</label></label>
                        </div>
                        <div>
                            <label class="itemlab" style="margin-left:15px"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  style="padding-right:10px"  Text="Hemoptysis " ID="chkScreen2"  /><label style="font-style:italic"></label>(Batuk darah)</label>
                        </div>
                        <div>
                            <label class="itemlab" style="margin-left:15px"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  style="padding-right:10px"  Text="Night Sweats " ID="chkScreen3"  /><label style="font-style:italic">(Berkeringat di malam hari)</label></label>
                        </div>
                        <div>
                            <label class="itemlab" style="margin-left:15px"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  style="padding-right:10px"  Text="Diarrhea, nausea, vomit " ID="chkScreen4"  /><label style="font-style:italic">(Diare, mual & muntah)</label></label>
                        </div>
                    </div>

                    <div class="col-sm-5" style="padding:0px;margin:0px;">
                        <div style="padding-top:5px">
                            <label class="itemlab" style="margin-left:15px"><asp:CheckBox runat="server" CssClass="mycheckboxFA" style="padding-right:10px"  Text="Cough more than 2 weeks " ID="chkScreen5"  /><label style="font-style:italic">(Ada batuk selama >2 minggu)</label></label>
                        </div>
                        <div>
                            <label class="itemlab" style="margin-left:15px"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  style="padding-right:10px"  Text="Swollen neck gland/s " ID="chkScreen6"  /><label style="font-style:italic">(Pembengkakan kelenjar leher)</label></label>
                        </div>
                        <div>
                            <label class="itemlab" style="margin-left:15px"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  style="padding-right:10px"  Text="Skin rash "  ID="chkScreen7" /><label style="font-style:italic">(Timbul kemerahan pada kulit)</label></label>
                        </div>
                        <div>
                            <label class="itemlab" style="margin-left:15px"><asp:CheckBox runat="server" CssClass="mycheckboxFA"  style="padding-right:10px"  Text="Open wounds and pus " ID="chkScreen8"  /><label style="font-style:italic">(Ada luka terbuka dan bernanah/pus)</label></label>
                        </div>
                    </div>
               </div>
               <label class="itemlab" style="font-style:italic">if patient has one or more condition above, continue to infectious disease comprehensive analysis</label>
           <%-- =============================================== END SKRINING PENYAKIT =============================================== --%>

            <%-- =============================================== ALERGI =============================================== --%>
               <br />
			   <div class="row" style="padding-top:10px">
                <div class="col-sm-6">
					<asp:UpdatePanel runat="server" ID="upAllergies">
                    <ContentTemplate>
<%--                    <asp:Button ID ="btnAllergyDrugNone" runat="server" CssClass="hidden" OnClick="btnAllergyDrugNone_onClick"/>
                    <asp:Button ID ="btnAllergyDrugShow" runat="server" CssClass="hidden" OnClick="btnAllergyDrugShow_onClick"/>
                    <asp:Button ID ="btnFoodAllergyNone" runat="server" CssClass="hidden" OnClick="btnFoodAllergyNone_onClick"/>
                    <asp:Button ID ="btnFoodAllergyShow" runat="server" CssClass="hidden" OnClick="btnFoodAllergyShow_onClick"/>--%>
                    <h6><strong>Drug Allergies<label class="subheader" style="font-size:10px"> (Alergi Obat)</label></strong></h6>
                   <div>
                       <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective6"  Value="0" id="rbdrug1" Checked="true" onclick="hidegrid(2,'rbdrug2','dvdrugs')" /> No </label>
                   </div>
                    <div>
                        <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective6"  Value="1" id="rbdrug2" onclick="ShowHideDiv8()"  /> Yes </label>
                    </div>
                    <div id="dvdrugs" style="display:none">
                       <div class="row">
                           <div class="col-sm-4" style="padding-right:0px;margin-right:0px;">
                               <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height: 24px;max-width:90%;width:90%;resize:none;padding-left:5px;" placeholder="Nama obat"  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtDrugsAllergy" onkeydown="return txtOnKeyPressDrugsAllergy();" />
                           </div>
                           <div class="col-sm-4" style="padding-left:0px;margin-left:0px;padding-right:0px">
                               <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height: 24px;max-width:90%;width:90%;resize:none;padding-left:5px;" placeholder="Reaksi"  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtReactionAllergy" onkeydown="return txtOnKeyPressDrugsAllergy();" />
                           </div>
                            <div class="col-sm-2" style="padding-left:0px;margin-left:0px;">
                               <asp:Button runat="server" style="width: 56px;height: 24px;border-radius: 4px;background-color: #2a3593;color:#ffffff"  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="btnDrugsAllergy" Text="Add" OnClick="btnAddAllergy_onClick" />
                           </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-10" style="max-height:220px;overflow-y:auto;">
                        <asp:GridView ID="gvw_allergy" runat="server" AutoGenerateColumns="false"  CssClass="table table-bordered table-condensed"
                      DataKeyNames="patient_allergy_id" EmptyDataText="No Data" >
                    <PagerStyle CssClass="pagination-ys" />
                        <Columns>
                            <asp:TemplateField HeaderText="Nama Obat" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="50%" HeaderStyle-Font-Size="11px">
                            <ItemTemplate>
                                <asp:HiddenField ID="patient_allergy_id" runat="server" Value='<%# Bind("patient_allergy_id") %>'></asp:HiddenField>
                                <asp:Label  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="allergy" runat="server"  Text='<%# Bind("allergy") %>'></asp:Label>
                            </ItemTemplate>
                             </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reaksi" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="30%" HeaderStyle-Font-Size="11px">
                            <ItemTemplate>
                                <asp:Label  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="allergy_reaction" runat="server"  Text='<%# Bind("allergy_reaction") %>'></asp:Label>
                            </ItemTemplate>
                             </asp:TemplateField>
                         <asp:TemplateField  ItemStyle-Width="5%" HeaderStyle-Font-Size="11px">
                            <ItemTemplate>
                                <asp:Button  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="x" OnClick="btnDeleteAllergy_onClick" ></asp:Button>
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

                <div class="col-sm-6">
                    <asp:UpdatePanel runat="server" ID="upFoods">
                        <ContentTemplate>
                    <h6><strong>Food Allergies<label class="subheader" style="font-size:10px"> (Alergi Makanan)</label></strong></h6>
                   <div>
                       <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective7"  Value="0" id="rbfood1" Checked="true" onclick="hidegrid(3,'rbfood2','dvfoods')" /> No </label>
                   </div>
                    <div>
                        <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective7"  Value="1" id="rbfood2" onclick="ShowHideDiv9()"  /> Yes </label>
                    </div>
                    <div id="dvfoods" style="display:none">
                       <div class="row">
                           <div class="col-sm-4" style="padding-right:0px;margin-right:0px;">
                               <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height: 24px;max-width:90%;width:90%;resize:none;padding-left:5px;" placeholder="Nama Makanan"  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtDrugsFoods" onkeydown="return txtOnKeyPressFoodsAllergy();" />
                           </div>
                           <div class="col-sm-4" style="padding-left:0px;margin-left:0px;padding-right:0px">
                               <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;height: 24px;max-width:90%;width:90%;resize:none;padding-left:5px;" placeholder="Reaksi"  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtReactionFoods" onkeydown="return txtOnKeyPressFoodsAllergy();" />
                           </div>
                            <div class="col-sm-2" style="padding-left:0px;margin-left:0px;">
                               <asp:Button runat="server" style="width: 56px;height: 24px;border-radius: 4px;background-color: #2a3593;color:#ffffff"  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="btnFoodAllergy" Text="Add" OnClick="btnAddFoodAllergy_onClick" />
                           </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-10" style="max-height:220px;overflow-y:auto;">
                        <asp:GridView ID="gvw_foods" runat="server" AutoGenerateColumns="False"  CssClass="table table-bordered table-condensed"
                      DataKeyNames="patient_allergy_id" EmptyDataText="No Data" >
                    <PagerStyle CssClass="pagination-ys" />
                        <Columns>
                            <asp:TemplateField HeaderText="Nama Obat" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="50%" HeaderStyle-Font-Size="11px">
                            <ItemTemplate>
                                <asp:HiddenField ID="patient_allergy_id" runat="server" Value='<%# Bind("patient_allergy_id") %>'></asp:HiddenField>
                                <asp:Label  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="allergy" runat="server"  Text='<%# Bind("allergy") %>'></asp:Label>
                            </ItemTemplate>
                             </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reaksi" HeaderStyle-ForeColor="#2a3593" ItemStyle-Width="30%" HeaderStyle-Font-Size="11px">
                            <ItemTemplate>
                                <asp:Label  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="allergy_reaction" runat="server"  Text='<%# Bind("allergy_reaction") %>'></asp:Label>
                            </ItemTemplate>
                             </asp:TemplateField>
                         <asp:TemplateField  ItemStyle-Width="5%" HeaderStyle-Font-Size="11px">
                            <ItemTemplate>
                                <asp:Button  Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="btndelete" runat="server" Text="x" OnClick="btnDeleteFoods_onClick" ></asp:Button>
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
               </div>
            <%-- =============================================== END ALERGI =============================================== --%>
            <%-- =============================================== PENGOBATAN SAAT INI =============================================== --%>
    <br />
	<asp:UpdatePanel runat="server" ID="upNutrition">
                <ContentTemplate>
        <h6><strong>Nutrition Problem<label class="subheader" style="font-size:10px"> (Masalah nutrisi khusus)</label></strong></h6>
        <div>
            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective8"  Value="0" id="rbnutrisi1" Checked="true" onclick="hidetext('txtNutrition','rbnutrisi2','dvnutrisi')" /> No </label>
        </div>
        <div class="row">
            <div class="col-xs-1" style="padding-right:0px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective8"  Value="1" id="rbnutrisi2" onclick="ShowHideDiv5()" /> Yes </label>
            </div>
            <div class="col-xs-4" style="padding-left:0px;display:none" id="dvnutrisi">
                <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;max-width:100%;width:100%;resize:none;padding-left:5px;" placeholder="Type here..."  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtNutrition"  />
            </div>
        </div>
        </ContentTemplate>
            </asp:UpdatePanel>
        <%-- =============================================== END PENGOBATAN SAAT INI =============================================== --%>

        <%-- =============================================== PENGOBATAN SAAT INI =============================================== --%>
        <br />
		<asp:UpdatePanel runat="server" ID="upFasting">
    <ContentTemplate>
        <h6><strong>Fasting<label class="subheader" style="font-size:10px"> (Puasa)</label></strong></h6>
        <div>
            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective9"  Value="0" id="rbpuasa1" Checked="true" onclick="hidetext('txtFasting','rbpuasa2','dvPuasa')"/> No </label>
        </div>
        <div class="row">
            <div class="col-xs-1" style="padding-right:0px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="subjective9"  Value="1" id="rbpuasa2" onclick="ShowHideDiv6()" /> Yes </label>
            </div>
            <div class="col-xs-4" style="padding-left:0px;display:none" id="dvPuasa">
                <asp:TextBox runat="server" style="border-radius: 2px;border: solid 1px #cdced9;max-width:100%;width:100%;resize:none;padding-left:5px;" placeholder="Type here..."  Font-Names="Helvetica, Arial, sans-serif" Font-Size="12px"  ID="txtFasting" />
            </div>
        </div>
                        </ContentTemplate>
            </asp:UpdatePanel>
     <%-- =============================================== END PENGOBATAN SAAT INI =============================================== --%>
            </div>
        </div>
    </div>
</div>
