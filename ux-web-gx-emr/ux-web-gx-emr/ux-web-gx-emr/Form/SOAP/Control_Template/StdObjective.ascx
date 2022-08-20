<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdObjective.ascx.cs" Inherits="Form_SOAP_Control_Template_StdObjective" %>

<style>

    .cardheader {
        border-bottom: solid 1px #cdced9;
        padding-left:15px;
        padding-top:6px;
    }
    .labelheader {
      font-family: Helvetica, Arial, sans-serif;
      font-size: 14px;
      font-weight: bold;
      font-style: normal;
      font-stretch: normal;
      line-height: 1.14;
      letter-spacing: normal;
      text-align: left;
      color: #171717;
    }

    .square {
  width: 48px;
  height: 46px;
  border-radius: 6px;
  background-color: #f4f4f4;
}

</style>
<script type="text/javascript">
    function CheckNumeric() {
        return event.keyCode >= 48 && event.keyCode <= 57 || event.keyCode == 44;
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

                totalScore.value = total1+total2+total3;
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
    function ShowDiv(val1) {
        var dvPassport = document.getElementById(val1);
            dvPassport.style.display = "";
    }
    function hide(val1) {
        var dvPassport = document.getElementById(val1);
            dvPassport.style.display = "none";
    }

    function CheckNormal() {
        var head3 = document.getElementById("<%=rbhead3.ClientID %>");
        var head2 = document.getElementById("<%=rbhead2.ClientID %>");
        if (!head3.checked)
            head2.click();

        var eyes3 = document.getElementById("<%=rbeyes3.ClientID %>");
        var eyes2 = document.getElementById("<%=rbeyes2.ClientID %>");
        if (!eyes3.checked)
            eyes2.click();

        var ent3 = document.getElementById("<%=rbent3.ClientID %>");
        var ent2 = document.getElementById("<%=rbent2.ClientID %>");
        if (!ent3.checked)
            ent2.click();

        var Mouth3 = document.getElementById("<%=rbMouth3.ClientID %>");
        var Mouth2 = document.getElementById("<%=rbMouth2.ClientID %>");
        if (!Mouth3.checked)
            Mouth2.click();

        var Teeth3 = document.getElementById("<%=rbTeeth3.ClientID %>");
        var Teeth2 = document.getElementById("<%=rbTeeth2.ClientID %>");
        if (!Teeth3.checked)
            Teeth2.click();

        var Neck3 = document.getElementById("<%=rbNeck3.ClientID %>");
        var Neck2 = document.getElementById("<%=rbNeck2.ClientID %>");
        if (!Neck3.checked)
            Neck2.click();

        var Chest3 = document.getElementById("<%=rbChest3.ClientID %>");
        var Chest2 = document.getElementById("<%=rbChest2.ClientID %>");
        if (!Chest3.checked)
            Chest2.click();

        var heart3 = document.getElementById("<%=rbheart3.ClientID %>");
        var heart2 = document.getElementById("<%=rbheart2.ClientID %>");
        if (!heart3.checked)
            heart2.click();

        var Lung3 = document.getElementById("<%=rbLung3.ClientID %>");
        var Lung2 = document.getElementById("<%=rbLung2.ClientID %>");
        if (!Lung3.checked)
            Lung2.click();

        var Back3 = document.getElementById("<%=rbBack3.ClientID %>");
        var Back2 = document.getElementById("<%=rbBack2.ClientID %>");
        if (!Back3.checked)
            Back2.click();

        var Abdomen3 = document.getElementById("<%=rbAbdomen3.ClientID %>");
        var Abdomen2 = document.getElementById("<%=rbAbdomen2.ClientID %>");
        if (!Abdomen3.checked)
            Abdomen2.click();

        var Limbs3 = document.getElementById("<%=rbLimbs3.ClientID %>");
        var Limbs2 = document.getElementById("<%=rbLimbs2.ClientID %>");
        if (!Limbs3.checked)
            Limbs2.click();

        var Anogenital3 = document.getElementById("<%=rbAnogenital3.ClientID %>");
        var Anogenital2 = document.getElementById("<%=rbAnogenital2.ClientID %>");
        if (!Anogenital3.checked)
            Anogenital2.click();

        var Skin3 = document.getElementById("<%=rbSkin3.ClientID %>");
        var Skin2 = document.getElementById("<%=rbSkin2.ClientID %>");
        if (!Skin3.checked)
            Skin2.click();

        var Others3 = document.getElementById("<%=rbOthers3.ClientID %>");
        var Others2 = document.getElementById("<%=rbOthers2.ClientID %>");
        if (!Others3.checked)
            Others2.click();

    }

    function validateFloatKeyPress(el) {
        var v = parseFloat(el.value);
        var strv = el.value.split('.');
        var strval = strv[1];
        // alert(strval.length);
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
        //el.value = (isNaN(v)) ? '' : v.toFixed(2);
    }

    function classdownobj() {
        if (document.getElementById("icondown2").className == "glyphicon glyphicon-chevron-right") {
                $("[id$='icondown2']").removeClass('glyphicon glyphicon-chevron-right');
                $("[id$='icondown2']").addClass('glyphicon glyphicon-chevron-down');
                $("[id$='valObj']").val('1');
            }
        else {
                $("[id$='icondown2']").removeClass('glyphicon glyphicon-chevron-down');
                $("[id$='icondown2']").addClass('glyphicon glyphicon-chevron-right');
                $("[id$='valObj']").val('0');
            }
        }
    function checkAccordionobj() {
        if ($("[id$='valObj']").val() == '1') {
            $("[id$='collapseObjective']").removeClass('panel-collapse collapse');
            $("[id$='collapseObjective']").addClass('panel-collapse collapse in');
            $("[id$='icondown2']").removeClass('glyphicon glyphicon-chevron-right');
            $("[id$='icondown2']").addClass('glyphicon glyphicon-chevron-down');
        }

    }
</script>

<asp:HiddenField runat="server" ID="valObj" Value="0"></asp:HiddenField>

<asp:updatepanel runat="server" ID="upObjective">
    <ContentTemplate>
<div class="panel-group" id="accordionObj">
    <div class="panel panel-default" style="border:0px">
    <a data-toggle="collapse" data-parent="#accordionObj" href="#collapseObjective">
        <label class="form-control headerpanel" onclick="classdownobj();" style="border:0px;margin:0px;"><span aria-hidden="true" id="icondown2"  class="glyphicon glyphicon-chevron-right" style="padding-right:10px;"></span>General Checkup</label></a>
        <div id="collapseObjective" class="panel-collapse collapse">
            <div class="panel-body" style="padding:0px">
                <%-- =================================================================== GCS ====================================================== --%>
                <div>
                <div class="row" style="margin-top:0px;margin-left:0px;margin-right:0px">
                    <div class="col-lg-4" style="padding-top:5px;padding-bottom:3px;border-top: solid 1px #eee;border-left: solid 1px #eee;">
                        <label class="labelheader">Eye</label>
                    </div>
                    <div class="col-lg-4" style="padding-top:5px;padding-bottom:3px;border-top: solid 1px #eee;border-left: solid 1px #eee;">
                        <label class="labelheader">Move</label>
                    </div>
                    <div class="col-lg-4" style="padding-top:5px;padding-bottom:3px;border-top: solid 1px #eee;border-left: solid 1px #eee;border-right: solid 1px #eee;">
                        <label class="labelheader">Verbal</label>
                    </div>
                </div>

                 <div class="row" style="margin-top:0px;margin-left:0px;margin-right:0px">
                    <div class="col-lg-4" style="padding-top:12px;padding-bottom:3px;border-top: solid 1px #eee;height: 180px;border-left: solid 1px #eee;border-bottom: solid 1px #eee;">
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="eye"  Value="4" id="eye1" onclick ="SumEMV();" /> 4. Spontaneus </label>
                        </div>
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="eye"  Value="3" id="eye2" onclick ="SumEMV();" /> 3. To Sound </label>
                        </div>
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="eye"  Value="2" id="eye3" onclick ="SumEMV();" /> 2. To Pressure </label>
                        </div>
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="eye"  Value="1" id="eye4" onclick ="SumEMV();" /> 1. None </label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-top:12px;padding-bottom:3px;border-top: solid 1px #eee;border-bottom: solid 1px #eee;border-left: solid 1px #eee;height: 180px;">
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="move"  Value="6" id="move1" onclick ="SumEMV();" /> 6. Obey Commands </label>
                        </div>
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="move"  Value="5" id="move2" onclick ="SumEMV();" /> 5. Localizes to pain stimulus </label>
                        </div>
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="move"  Value="4" id="move3" onclick ="SumEMV();" /> 4. Withdrawns from pain </label>
                        </div>
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="move"  Value="3" id="move4" onclick ="SumEMV();" /> 3. Flexion to pain stumulus </label>
                        </div>
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="move"  Value="2" id="move5" onclick ="SumEMV();" /> 2. Extension </label>
                        </div>
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="move"  Value="1" id="move6" onclick ="SumEMV();" /> 1. None </label>
                        </div>
                    </div>
                    <div class="col-lg-4" style="padding-top:12px;padding-bottom:20px;border: solid 1px #eee;height: 180px;">
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="verbal"  Value="5" id="verbal1" onclick ="SumEMV();" /> 5. Orientated </label>
                        </div>
                        <div>
                            <label style="font-size:12px;fo,nt-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="verbal"  Value="4" id="verbal2" onclick ="SumEMV();" /> 4. Confused </label>
                        </div>
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="verbal"  Value="3" id="verbal3" onclick ="SumEMV();" /> 3. Inappropriate words </label>
                        </div>
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="verbal"  Value="2" id="verbal4" onclick ="SumEMV();" /> 2. Incomprehensible sounds </label>
                        </div>
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="verbal"  Value="1" id="verbal5" onclick ="SumEMV();" /> 1. None </label>
                        </div>
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="verbal"  Value="T" id="verbal6" onclick ="SumEMV();" /> T. Tracheostomy </label>
                        </div>
                        <div>
                            <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="verbal"  Value="A" id="verbal7" onclick ="SumEMV();" /> A. Aphasia </label>
                        </div>
                    </div>
                </div>

                <div class="row" style="margin-top:0px;margin-left:0px;margin-right:0px;text-align:right">
                    <div class="col-lg-6">
                    </div>
                    <div class="col-lg-5" style="padding-top:0px;padding-bottom:0px;">
                        <label style="font-family: Helvetica, Arial, sans-serif;font-size: 39px;font-weight: bold;color: #4d9b35;">E</label>
                        <asp:TextBox runat="server" ID="lbleyetotal" Value="_" Width="10px" BorderColor="White" BorderStyle="None" BorderWidth="0" style="font-family: Helvetica, Arial, sans-serif;font-size: 20px;font-weight: bold;color: #f88805;" ></asp:TextBox>
                        <label style="font-family: Helvetica, Arial, sans-serif;font-size: 39px;font-weight: bold;color: #4d9b35;">M</label>
                        <asp:TextBox runat="server" ID="lblmovetotal" Value="_" Width="10px" BorderColor="White" BorderStyle="None" BorderWidth="0" style="font-family: Helvetica, Arial, sans-serif;font-size: 20px;font-weight: bold;color: #f88805;" ></asp:TextBox>
                        <label style="font-family: Helvetica, Arial, sans-serif;font-size: 39px;font-weight: bold;color: #4d9b35;">V</label>
                        <asp:TextBox runat="server" ID="lblverbaltotal" Value="_" Width="30px" BorderColor="White" BorderStyle="None" BorderWidth="0" style="font-family: Helvetica, Arial, sans-serif;font-size: 20px;font-weight: bold;color: #f88805;" ></asp:TextBox>
                    </div>
                    <div class="col-lg-1 square" style="margin-top:10px;margin-left:0px;padding-left:0px;padding-right:10px">
                            <label style="font-family: Helvetica, Arial, sans-serif;font-size: 12px;color: #171717;padding-left:5px">Score</label>
                            <asp:TextBox runat="server" ID="lblTotalScore" Value="_" BorderStyle="None" Width="50px" style="font-family: Helvetica, Arial, sans-serif;font-size: 18px;font-weight:bold;color: #171717;padding-right:10px;margin-top:0px;text-align:center;background-color:transparent"></asp:TextBox>
                    </div>
                </div>
                </div>
                <hr />

                <%-- =================================================================== END GCS ====================================================== --%>
                <div class="row" style="margin:0px;padding:0px">
                    <div class="col-lg-5" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px">
                        <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Pain Scale</label>

                    </div>
                </div>
                <div class="row" style="margin:0px;padding-bottom:10px">
                    <div class="col-sm-1" style="width:9.5%">
                        <asp:ImageButton ImageUrl="~/Images/O/ic_Pain_1.png" runat="server" OnClientClick="getProgressBar(0); return false;"/>
                    </div>
                    <div class="col-sm-1" style="width:10%">
                        <asp:ImageButton ImageUrl="~/Images/O/ic_Pain_2.png" runat="server" OnClientClick="getProgressBar(2); return false;"/>
                    </div>
                    <div class="col-sm-1" style="width:10%">
                        <asp:ImageButton ImageUrl="~/Images/O/ic_Pain_4.png" runat="server" OnClientClick="getProgressBar(4); return false;"/>
                    </div>
                    <div class="col-sm-1" style="width:10%">
                        <asp:ImageButton ImageUrl="~/Images/O/ic_Pain_6.png" runat="server" OnClientClick="getProgressBar(6); return false;"/>
                    </div>
                    <div class="col-sm-1" style="width:10%">
                        <asp:ImageButton ImageUrl="~/Images/O/ic_Pain_8.png" runat="server" OnClientClick="getProgressBar(8); return false;"/>
                    </div>
                    <div class="col-sm-1">
                        <asp:ImageButton ImageUrl="~/Images/O/ic_Pain_10.png" runat="server" OnClientClick="getProgressBar(10); return false;"/>
                    </div>
                    <div class="col-sm-2">
                            <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Value</label>
                    </div>
                </div>

                <div class="row" style="margin:0px;padding:0px">
                    <div class="col-sm-7" style="padding-top:0px;padding-bottom:0px;font-size:12px;">
                        <div class="progress" style="margin:10px;width:90%">
                            <div class="progress-bar progress-bar-success"  id="divGreen">
                            <span class="sr-only">35% Complete (success)</span>
                            </div>
                            <div class="progress-bar progress-bar-warning"  id="divYellow">
                            <span class="sr-only">20% Complete (warning)</span>
                            </div>
                            <div class="progress-bar progress-bar-danger"  id="divRed">
                            <span class="sr-only">10% Complete (danger)</span>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-1">
                            <asp:TextBox runat="server" ID="txtPainScale" Value="0" Width="30px" BorderColor="White" BorderStyle="None" BorderWidth="0" style="font-family: Helvetica, Arial, sans-serif;font-size: 20px;font-weight: bold;color: #f88805;" ></asp:TextBox>
                    </div>
                </div>

                <div class="row" style="margin:0px;padding-left:10px">
                    <div class="col-sm-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="0" OnClientClick ="getProgressBar(0); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-sm-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="1" OnClientClick ="getProgressBar(1); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-sm-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="2" OnClientClick ="getProgressBar(2); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-sm-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="3" OnClientClick ="getProgressBar(3); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-sm-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="4" OnClientClick ="getProgressBar(4); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-sm-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="5" OnClientClick ="getProgressBar(5); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-sm-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="6" OnClientClick ="getProgressBar(6); return false;"></asp:LinkButton>
                    </div>
                        <div class="col-sm-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="7" OnClientClick ="getProgressBar(7); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-sm-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="8" OnClientClick ="getProgressBar(8); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-sm-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="9" OnClientClick ="getProgressBar(9); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-sm-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="10" OnClientClick ="getProgressBar(10); return false;"></asp:LinkButton>
                    </div>
            </div>

                <div>
                    &nbsp
                </div>
                <div class="row" style="margin:0px;padding:0px">
                    <div class="col-lg-5" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px">
                        <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Mental Status</label>
                    </div>
                </div>

                <div class="row" style="margin:0px;padding:0px">
                    <div class="col-lg-3" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px">
                        <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server"  GroupName="mental" Value="Good Orientation" id="mental1" />Good Orientation  </label>
                    </div>
                    <div class="col-lg-3" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px">
                        <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="mental" Value="Disorientated" id="mental2" /> Disorientated </label>
                    </div>
                    <div class="col-lg-3" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px">
                        <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="mental" Value="Cooperative" id="mental3" /> Cooperative </label>
                    </div>
                    <div class="col-lg-3" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px">
                        <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="mental" Value="Non Cooperative" id="mental4" /> Non Cooperative </label>
                    </div>
                </div>

                <div class="row" style="margin:0px;padding:0px">
            <div class="col-lg-5" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px">
                <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Consciousness Level</label>
            </div>
        </div>

        <div class="row" style="margin:0px;padding:0px">
            <div class="col-lg-3" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server"  GroupName="consciousness" Value="1" id="consciousness1" />Compos mentis  </label>
            </div>
            <div class="col-lg-3" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="consciousness" Value="2" id="consciousness2" /> Somnolent </label>
            </div>
            <div class="col-lg-3" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="consciousness" Value="3" id="consciousness3" /> Stupor </label>
            </div>
            <div class="col-lg-3" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="consciousness" Value="4" id="consciousness4" /> Coma </label>
            </div>
        </div>

        <div class="row" style="margin:0px;padding:0px">
            <div class="col-lg-8" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> Fall Risk (Condition that needs extra attention related to fall risk)</label>
            </div>
        </div>
        <div class="row" style="margin:0px;padding-left:30px;padding-bottom:0px;padding-right:0px;padding-top:0px">
            <div class="col-lg-8" style="padding-top:0px;padding-bottom:0px;padding-right:0px;font-size:12px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:CheckBox runat="server"  GroupName="admtype6" Value="1" id="fall1" />Patient undergo sedation</label>
            </div>
        </div>
        <div class="row" style="margin:0px;padding-left:30px;padding-bottom:0px;padding-right:0px;padding-top:0px">
            <div class="col-lg-8" style="padding-top:0px;padding-bottom:0px;padding-right:0px;font-size:12px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:CheckBox runat="server"  GroupName="admtype6" Value="1" id="fall2" />Patient with physical limitation</label>
            </div>
        </div>
        <div class="row" style="margin:0px;padding-left:30px;padding-bottom:0px;padding-right:0px;padding-top:0px">
            <div class="col-lg-8" style="padding-top:0px;padding-bottom:0px;padding-right:0px;font-size:12px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:CheckBox runat="server"  GroupName="admtype6" Value="1" id="fall3" />Patient with motion aids</label>
            </div>
        </div>
        <div class="row" style="margin:0px;padding-left:30px;padding-bottom:0px;padding-right:0px;padding-top:0px">
            <div class="col-lg-8" style="padding-top:0px;padding-bottom:0px;padding-right:0px;font-size:12px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:CheckBox runat="server"  GroupName="admtype6" Value="1" id="fall4" />Patient with balance disorder</label>
            </div>
        </div>
        <div class="row" style="margin:0px;padding-left:30px;padding-bottom:30px;padding-right:0px;padding-top:0px">
            <div class="col-lg-8" style="padding-top:0px;padding-bottom:0px;padding-right:0px;font-size:12px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:CheckBox runat="server"  GroupName="admtype6" Value="1" id="fall5" />Fasting patient to undergo further test(Lab/Radiology/etc)</label>
            </div>
        </div>

            </div>
        </div>
    </div>
</div>

       <table>
                                        <tbody>
                                            <tr>
                                                <td style="padding-left:15px; padding-right:15px; font-weight:700; font-size:14px; min-width:100px">
                                                    <label>Vital Sign:</label>
                                                </td>
                                                <td>
                                <%-- ===================================================================  VITAL SIGN ====================================================== --%>
                                        <div class="row" style="margin:0px;padding:0px">
                <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;font-size:12px;width:160px">
                			<label>Blood Pressure</label>
                                        </div>
                                        <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;font-size:12px;padding-right:0px;width:120px">
                                            <label>Pulse Rate</label>
                                        </div>
                                        <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;padding-left:0px;margin-left:0px;font-size:12px;width:120px">
                                            <label>Respiratory rate</label>
                                        </div>
                                        <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;padding-left:0px;margin-left:0px;font-size:12px;width:70px">
                                            <label>SpO2</label>
                                        </div>
                                        <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;font-size:12px;width:100px">
                                            <label>Temperature</label>
                                        </div>
                                        <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;font-size:12px;padding-right:0px;width:100px">
                                            <label>Weight</label>
                                        </div>
                                        <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;padding-left:0px;margin-left:0px;font-size:12px;width:100px">
                                            <label>Height</label>
                                        </div>
                                    </div>
                                        <div class="row" style="margin:0px;padding:0px">
                                            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:160px">
                                                <asp:TextBox runat="server" Width="40px" Height="23px" Style="text-align:right" ID="txtbloodlow" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
                                                <label>/</label>
                                                <asp:TextBox runat="server" Width="40px" Height="23px" Style="text-align:right" ID="txtbloodhigh" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
                                                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">mmHg</label>
                                            </div>
                                            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:120px">
                                                <asp:TextBox runat="server" Width="40px" Height="23px" Style="text-align:right" ID="txtpulserate" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
                                                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">X/mnt</label>
                                            </div>
                                            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;padding-left:0px;margin-left:0px;font-size:12px;width:120px">
                                                <asp:TextBox runat="server" Width="40px" Height="23px" Style="text-align:right" ID="txtrespiratory" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
                                                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">X/mnt</label>
                                            </div>
                                            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;padding-left:0px;margin-left:0px;font-size:12px;width:70px">
                                                <asp:TextBox runat="server" Width="40px" Height="23px" Style="text-align:right" ID="txtspo" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
                                                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">%</label>
                                            </div>
                                            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:100px">
                                                <asp:TextBox runat="server" Width="40px" Height="23px" Style="text-align:right" ID="txttemperature" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
                                                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">&#8451</label>
                                            </div>
                                            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:100px">
                                                <asp:TextBox runat="server" Width="40px" Height="23px" Style="text-align:right" ID="txtweight" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
                                                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">kg</label>
                                            </div>
                                            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;padding-left:0px;margin-left:0px;font-size:12px;width:100px">
                                                <asp:TextBox runat="server" Width="40px" Height="23px" Style="text-align:right" ID="txtheight" onchange="validateFloatKeyPress(this);" onkeypress="return CheckNumeric();"></asp:TextBox>
                                                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">cm</label>
                                            </div>
                                        </div>

                                            <%-- =================================================================== END VITAL SIGN ====================================================== --%>


                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
        <div class="row cardheader" style="margin:0px">
            <div class="col-sm-10" style="padding-left:10px;padding-top:5px">
                <label class="labelheader">Physical Examination</label>
            </div>
            <asp:LinkButton Style="color: #4d9b35;font-weight: bold;" runat="server" GroupName="allnormal"  Value="1" id="rbAllNormal" OnClientClick ="CheckNormal();" Text="Otherwise Normal" />
        </div>

        <div style="padding-bottom:20px">
            <%-- ======================================================= HEAD ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Head</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="head"  Value="1" id="rbhead1" onclick ="hide('dvHead');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="head"  Value="2" id="rbhead2" onclick ="hide('dvHead');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="head"  Value="3" id="rbhead3" onclick ="ShowDiv('dvHead');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvHead">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txthead" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END HEAD ================================================ --%>
            <%-- ======================================================= Eyes ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Eyes</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="eyes"  Value="1" id="rbeyes1" onclick ="hide('dvEyes');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="eyes"  Value="2" id="rbeyes2" onclick ="hide('dvEyes');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="eyes"  Value="3" id="rbeyes3" onclick ="ShowDiv('dvEyes');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvEyes">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txteyes" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END Eyes ================================================ --%>
            <%-- ======================================================= ENT ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Ent</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="ent"  Value="1" id="rbent1" onclick ="hide('dvEnt');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="ent"  Value="2" id="rbent2" onclick ="hide('dvEnt');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="ent"  Value="3" id="rbent3" onclick ="ShowDiv('dvEnt');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvEnt">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txtEnt" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END ENT ================================================ --%>
            <%-- ======================================================= Mouth ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Mouth</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="mouth"  Value="1" id="rbMouth1" onclick ="hide('dvMouth');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="mouth"  Value="2" id="rbMouth2" onclick ="hide('dvMouth');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="mouth"  Value="3" id="rbMouth3" onclick ="ShowDiv('dvMouth');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvMouth">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txtMouth" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END Mouth ================================================ --%>
            <%-- ======================================================= Teeth ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Teeth</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="teeth"  Value="1" id="rbTeeth1" onclick ="hide('dvTeeth');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="teeth"  Value="2" id="rbTeeth2" onclick ="hide('dvTeeth');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="teeth"  Value="3" id="rbTeeth3" onclick ="ShowDiv('dvTeeth');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvTeeth">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txtTeeth" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END Teeth ================================================ --%>
            <%-- ======================================================= Neck ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Neck</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="neck"  Value="1" id="rbNeck1" onclick ="hide('dvNeck');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="neck"  Value="2" id="rbNeck2" onclick ="hide('dvNeck');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="neck"  Value="3" id="rbNeck3" onclick ="ShowDiv('dvNeck');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvNeck">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txtNeck" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END Neck ================================================ --%>
            <%-- ======================================================= Chest ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Chest</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="chest"  Value="1" id="rbChest1" onclick ="hide('dvChest');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="chest"  Value="2" id="rbChest2" onclick ="hide('dvChest');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="chest"  Value="3" id="rbChest3" onclick ="ShowDiv('dvChest');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvChest">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txtChest" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END Chest ================================================ --%>
            <%-- ======================================================= Heart ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Heart</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="heart"  Value="1" id="rbheart1" onclick ="hide('dvHeart');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="heart"  Value="2" id="rbheart2" onclick ="hide('dvHeart');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="heart"  Value="3" id="rbheart3" onclick ="ShowDiv('dvHeart');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvHeart">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txtHeart" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END Heart ================================================ --%>
            <%-- ======================================================= Lung ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Lung</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Lung"  Value="1" id="rbLung1" onclick ="hide('dvLung');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Lung"  Value="2" id="rbLung2" onclick ="hide('dvLung');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Lung"  Value="3" id="rbLung3" onclick ="ShowDiv('dvLung');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvLung">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txtLung" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END Lung ================================================ --%>
            <%-- ======================================================= Back ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Back</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Back"  Value="1" id="rbBack1" onclick ="hide('dvBack');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Back"  Value="2" id="rbBack2" onclick ="hide('dvBack');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Back"  Value="3" id="rbBack3" onclick ="ShowDiv('dvBack');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvBack">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txtBack" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END Back ================================================ --%>
            <%-- ======================================================= Abdomen ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Abdomen</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Abdomen"  Value="1" id="rbAbdomen1" onclick ="hide('dvAbdomen');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Abdomen"  Value="2" id="rbAbdomen2" onclick ="hide('dvAbdomen');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Abdomen"  Value="3" id="rbAbdomen3" onclick ="ShowDiv('dvAbdomen');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvAbdomen">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txtAbdomen" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END Abdomen ================================================ --%>
            <%-- ======================================================= Limbs ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Limbs</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Limbs"  Value="1" id="rbLimbs1" onclick ="hide('dvLimbs');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Limbs"  Value="2" id="rbLimbs2" onclick ="hide('dvLimbs');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Limbs"  Value="3" id="rbLimbs3" onclick ="ShowDiv('dvLimbs');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvLimbs">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txtLimbs" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END Limbs ================================================ --%>
            <%-- ======================================================= Anogenital ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Anogenital</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Anogenital"  Value="1" id="rbAnogenital1" onclick ="hide('dvAnogenital');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Anogenital"  Value="2" id="rbAnogenital2" onclick ="hide('dvAnogenital');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Anogenital"  Value="3" id="rbAnogenital3" onclick ="ShowDiv('dvAnogenital');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvAnogenital">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txtAnogenital" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END Anogenital ================================================ --%>
            <%-- ======================================================= Skin ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Skin</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Skin"  Value="1" id="rbSkin1" onclick ="hide('dvSkin');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Skin"  Value="2" id="rbSkin2" onclick ="hide('dvSkin');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Skin"  Value="3" id="rbSkin3" onclick ="ShowDiv('dvSkin');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none" id="dvSkin">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txtSkin" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END Skin ================================================ --%>
            <%-- ======================================================= Others ================================================= --%>
            <div class="row" style="margin:0px;padding:0px;margin-top:10px">
                <div class="col-sm-1">
                    <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Others</label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Others"  Value="1" id="rbOthers1" onclick ="hide('dvOthers');" Checked="true" />Not checked </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Others"  Value="2" id="rbOthers2" onclick ="hide('dvOthers');" />Normal </label>
                </div>
                <div class="col-sm-2">
                    <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="Others"  Value="3" id="rbOthers3" onclick ="ShowDiv('dvOthers');" />Abnormal </label>
                </div>
            </div>
            <div class="row" style="margin:0px;padding:0px;display:none;margin-bottom:20px;" id="dvOthers">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-6" style="padding-top:10px">
                    <asp:TextBox runat="server" style="border: solid 1px #bdbfd8;max-width:100%;width:100%;resize:none;font-family:Helvetica, Arial, sans-serif;border-radius: 4px;" placeholder="Type here..." ID="txtOthers" TextMode="MultiLine" Rows="5" />
                </div>
            </div>
            <%-- ===================================================== END Others ================================================ --%>
        </div>

    </ContentTemplate>
</asp:updatepanel>
