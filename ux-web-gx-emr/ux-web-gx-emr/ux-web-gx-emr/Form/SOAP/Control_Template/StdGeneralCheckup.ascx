<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdGeneralCheckup.ascx.cs" Inherits="Form_SOAP_Control_Template_StdGeneralCheckup" %>
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
        <div class="panel-group" id="accordionObj" style="margin-bottom:0px;background-color:#ffffff">
    <div class="panel panel-default" style="border:0px;border-radius:0px;box-shadow:0px 0px 0px transparent">
    <a data-toggle="collapse" data-parent="#accordionObj" href="#collapseObjective">
		<div style="padding-left:15px;padding-right:15px">
		<hr style="border-color:#efefef;margin-top:0px;margin-bottom:0px">
        <label class="form-control headerpanel" onclick="classdownobj();" style="border-color:#efefef; margin:0px;padding-left:0px;padding-top: ‒2;padding-bottom: 0px;height: 26px;padding-top: 4px;font-size:12px;"><span aria-hidden="true" id="icondown2"  class="glyphicon glyphicon-chevron-right" style="padding-right:10px"></span>General Checkup</label>

		</div>
		</a>
        <div id="collapseObjective" class="panel-collapse collapse">
            <div class="panel-body" style="padding:0px 15px 0px 15px">
                <%-- =================================================================== GCS ====================================================== --%>
                <div>
                <div class="row" style="margin-top:0px;margin-left:0px;margin-right:0px">

                </div>

                 <div class="row" style="margin-top:0px;margin-left:0px;margin-right:0px">
                    <div class="col-lg-4" style="padding-top:12px;padding-bottom:3px;border-top: solid 0px #cdced9;height: 120px;border-left: solid 0px #cdced9;border-bottom: solid 0px #eee;">
                        <label class="labelheader">Eye</label>
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
                    <div class="col-lg-4" style="padding-top:12px;padding-bottom:3px;border-top: solid 0px #cdced9;border-bottom: solid 0px #cdced9;border-left: solid 0px #eee;height: 180px;">
                        <label class="labelheader">Move</label>
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
                    <div class="col-lg-4" style="padding-top:12px;padding-bottom:20px;border: solid 0px #eee;height: 180px;">
						<label class="labelheader">Verbal</label>
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
                <div class="row" style="margin:0px;padding-bottom:0px">
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
							<br>
                            <asp:TextBox runat="server" ID="txtPainScale" Value="0" Width="35px" BorderColor="White" BorderStyle="None" BorderWidth="0" style="font-family: Helvetica, Arial, sans-serif;font-size: 20px;font-weight: bold;color: #f88805;padding-left:12px" ></asp:TextBox>
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
                </div>

                <div class="row" style="margin:0px;padding-left:10px">
                    <div class="col-xs-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="0" OnClientClick ="getProgressBar(0); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-xs-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="1" OnClientClick ="getProgressBar(1); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-xs-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="2" OnClientClick ="getProgressBar(2); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-xs-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="3" OnClientClick ="getProgressBar(3); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-xs-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="4" OnClientClick ="getProgressBar(4); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-xs-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="5" OnClientClick ="getProgressBar(5); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-xs-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="6" OnClientClick ="getProgressBar(6); return false;"></asp:LinkButton>
                    </div>
                        <div class="col-xs-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="7" OnClientClick ="getProgressBar(7); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-xs-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="8" OnClientClick ="getProgressBar(8); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-xs-1" style="width:5%">
                            <asp:LinkButton runat="server" Text="9" OnClientClick ="getProgressBar(9); return false;"></asp:LinkButton>
                    </div>
                    <div class="col-xs-1" style="width:5%">
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
                    <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:150px">
                        <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server"  GroupName="mental" Value="Good Orientation" id="mental1" /> Good Orientation  </label>
                    </div>
                    <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:150px">
                        <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="mental" Value="Disorientated" id="mental2" /> Disorientated </label>
                    </div>
                    <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:150px">
                        <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="mental" Value="Cooperative" id="mental3" /> Cooperative </label>
                    </div>
                    <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:150px">
                        <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="mental" Value="Non Cooperative" id="mental4" /> Non Cooperative </label>
                    </div>
                </div>

                <div class="row" style="margin:0px;padding:0px">
            <div class="col-lg-5" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px">
                <label style="font-size: 14px;font-family: Helvetica, Arial, sans-serif;font-weight: bold;">Consciousness Level</label>
            </div>
        </div>

        <div class="row" style="margin:0px;padding:0px">
            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:150px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server"  GroupName="consciousness" Value="1" id="consciousness1" /> Compos mentis  </label>
            </div>
            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:150px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="consciousness" Value="2" id="consciousness2" /> Somnolent </label>
            </div>
            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:150px">
                <label style="font-size:12px;font-family: Helvetica, Arial, sans-serif;color: #171717;font-weight:normal"> <asp:RadioButton runat="server" GroupName="consciousness" Value="3" id="consciousness3" /> Stupor </label>
            </div>
            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:150px">
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
					<hr style="border-color:#efefef;margin-top:0px;margin-bottom:0px">
            </div>

        </div>
    </div>
</div>
     </ContentTemplate>
</asp:updatepanel>
