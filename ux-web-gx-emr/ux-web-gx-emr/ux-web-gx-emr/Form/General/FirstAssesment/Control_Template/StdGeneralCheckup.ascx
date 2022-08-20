<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdGeneralCheckup.ascx.cs" Inherits="Form_General_FirstAssesment_Control_Template_StdGeneralCheckup" %>

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
</script>
<asp:updatepanel runat="server" ID="upObjective">
    <ContentTemplate>
<div class="col-lg-12" style="margin:0px;padding-right:0px">
    <div style="min-height:120px;background-color:white; width:100%;border:1px; border-radius: 7px;box-shadow: 0px 2px 5px #9293A0;margin-top:0px;margin-left:0px" class="modal-dialog center-block">
        <div class="cardheader">
            <label class="labelheader">General Checkup</label>
        </div>
           
        <div>
        <div class="row" style="margin:0px;padding:0px">
            <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;font-size:12px;width:200px">
                <label>Blood Pressure</label>
            </div>
            <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;font-size:12px;padding-right:0px;width:140px">
                <label>Pulse Rate</label>
            </div>
            <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;padding-left:0px;margin-left:0px;font-size:12px;width:140px">
                <label>Respiratory rate</label>
            </div>
            <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;padding-left:0px;margin-left:0px;font-size:12px;width:100px">
                <label>SpO2</label>
            </div>
            <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;font-size:12px;width:130px">
                <label>Temperature</label>
            </div>
            <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;font-size:12px;padding-right:0px;width:130px">
                <label>Weight</label>
            </div>
            <div class="col-lg-2" style="padding-top:12px;padding-bottom:0px;padding-left:0px;margin-left:0px;font-size:12px;width:130px">
                <label>Height</label>
            </div>
        </div>
        <div class="row" style="margin:0px;padding:0px">
            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:200px">
                <asp:TextBox runat="server" Width="66px" Height="23px" Style="text-align:right" ID="txtbloodlow" onkeypress="return CheckNumeric();"></asp:TextBox>
                <label>/</label>
                <asp:TextBox runat="server" Width="66px" Height="23px" Style="text-align:right" ID="txtbloodhigh" onkeypress="return CheckNumeric();"></asp:TextBox>
                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">mmHg</label>
            </div>
            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:140px">
                <asp:TextBox runat="server" Width="66px" Height="23px" Style="text-align:right" ID="txtpulserate" onkeypress="return CheckNumeric();"></asp:TextBox>
                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">X/mnt</label>
            </div>
            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;padding-left:0px;margin-left:0px;font-size:12px;width:140px">
                <asp:TextBox runat="server" Width="83px" Height="23px" Style="text-align:right" ID="txtrespiratory" onkeypress="return CheckNumeric();"></asp:TextBox>
                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">X/mnt</label>
            </div>
            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;padding-left:0px;margin-left:0px;font-size:12px;width:100px">
                <asp:TextBox runat="server" Width="56px" Height="23px" Style="text-align:right" ID="txtspo" onkeypress="return CheckNumeric();"></asp:TextBox>
                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">%</label>
            </div>
            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:130px">
                <asp:TextBox runat="server" Width="66px" Height="23px" Style="text-align:right" ID="txttemperature" onkeypress="return CheckNumeric();"></asp:TextBox>
                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">&#8451</label>
            </div>
            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;font-size:12px;width:130px">
                <asp:TextBox runat="server" Width="66px" Height="23px" Style="text-align:right" ID="txtweight" onkeypress="return CheckNumeric();"></asp:TextBox>
                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">kg</label>
            </div>
            <div class="col-lg-2" style="padding-top:0px;padding-bottom:10px;padding-right:0px;padding-left:0px;margin-left:0px;font-size:12px;width:130px">
                <asp:TextBox runat="server" Width="83px" Height="23px" Style="text-align:right" ID="txtheight" onkeypress="return CheckNumeric();"></asp:TextBox>
                <label style="color: #b2b6d1;font-size: 12px;font-family: Helvetica, Arial, sans-serif;">cm</label>
            </div>
        </div>

                           
            <div>&nbsp</div>

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
                     </ContentTemplate>
    </asp:updatepanel>