<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Form_General_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <%--<title>Siloam EMR</title>--%>
    <link href="../../favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />

    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrapmin.min.css" />
    <link rel="stylesheet" href="~/Content/dist/css/AdminLTEmin.min.css" />
    <link rel="stylesheet" href="~/Content/bootstrap-select/css/bootstrap-select.min.css" />
    <link rel="stylesheet" href="~/Content/toast/toastr.css" />
    <link rel="stylesheet" href="~/Content/font-awesome/css/font-awesome.css" />
    <style>
        body {
            background-image: url("<%= Page.ResolveClientUrl("~/Images/Background/ic_BG_EMR.svg") %>") !important;
            background-size: cover;
            background-repeat: no-repeat;
            padding-top: 50px;

           /*-webkit-animation: fadein 2s;
            animation: fadein 2s;*/
        }

        /*@keyframes fadein {
            from { opacity: 0; }
            to   { opacity: 1; }
        }*/

        .btn-primary {
            background-color: #1A2268;
            transition: all 0.5s;
        }

            .btn-primary:hover {
                background-color: #4350B1;
                transition: all 0.5s;
            }
        /* Modal QR */
        #modalQR {
            padding-top: 10px;
        }
        #modalQR strong {
            font-size:20px;
        }

        #modalQR .close {
            line-height:0.6 !important;
            font-size:40px !important;
            font-weight:200 !important;
        }

        #modalQR p {
            font-size:20px;
            text-align:center;
        }

        #modalQR p  a{
            font-size:16px;
            font-weight:700;
            text-decoration: underline;
        }

        #btnRefreshQR {
            width:150px;
            height:150px;
            text-align:center;
            background-color:#205081;
            color:white;
            border-radius:50%;
            border:0;
            white-space:normal;
            word-wrap:break-word;
            padding:1em !important;
            font-size:16px;
        }
        #icon-btn-refresh {
            font-size:30px !important;
            margin:0.3em;
        }
        /* Setting Media Screen for login box */
        @media only screen and (min-width: 1000px) {
          /* For desktop: */
            .login-box {
                width:650px;
            }
            .login-box-body-login {
                display:flex !important;
                align-items:stretch;
            }
        }
        @media screen and (max-width: 1000px) {
          /* For tablets: */
          .login-box {
                width:400px;
            }
            .login-box-body-login {
                display:block !important;
            }
        }
        /* override loading overlay layout z-index */
        .loadingoverlay {
            z-index:1000 !important;
        }
        /* override modal dialog bootstrap */
        .modal-dialog-turorial {
            width:70% !important;
            padding-top:0.5em;
        }
        .video-container {
            height:75vh;
        }
    </style>
    
</head>

<body class="hold-transition">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap-select/js/bootstrap-select.min.js" />
                <asp:ScriptReference Path="~/Content/toast/toastr.min.js" />
            </Scripts>
        </asp:ScriptManager>

        <asp:UpdateProgress ID="uProgLoginX" runat="server" AssociatedUpdatePanelID="upError">
            <ProgressTemplate>
                <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
                </div>
                <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                    <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="login-box">
            <div class="login-box-body row login-box-body-login" style="background-color: white; width: 100%; border: 1px; border-radius: 5px; box-shadow: 0px 1px 2px rgba(26, 34, 105, 0.5); padding: 30px;">
                <div class="col-md-6">
                    <asp:UpdatePanel ID="upError" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="login-logo" style="color: Blue; font-weight: bold; height: 60px;">
                                <img src="<%= Page.ResolveClientUrl("~/Images/Login/ic_LogoLogin.svg") %>" width="80%" />
                                <br />
                            </div>
                            <div class="form-group row">
                                <div class="col-md-12 inputGroupContainer" style="margin-top: 10px;">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:TextBox ID="txtUsername" Style="width: 100%; max-width: 100%;" runat="server" CssClass="form-control" placeholder="Username" Visible="false"></asp:TextBox>
                                        <input id="inputTxtUsername" type="text" style="width: 100%; max-width: 100%;" class="form-control" placeholder="Username" />
                                        <asp:HiddenField ID="hfTxtUsername" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-12 inputGroupContainer" style="margin-top: 10px;">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                        <asp:TextBox ID="txtPassword" TextMode="Password" Style="width: 100%; max-width: 100%;" runat="server" CssClass="form-control" placeholder="Password" Visible="false"></asp:TextBox>
                                        <input id="inputTxtPassword" type="password" style="width: 100%; max-width: 100%;" class="form-control" placeholder="Password" />
                                        <asp:HiddenField ID="hfTxtPassword" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-12 inputGroupContainer" style="text-align: right;">

                                    <div style="text-align: right; margin-top: 5px; margin-left: 55px; font-size: 12px;">
                                        <p style="text-align: right; color: red; display: none" id="pError" runat="server"></p>
                                    </div>

                                    <table border="0" style="width: 100%;">
                                        <tr>
                                            <td style="text-align: left;">
                                                <small style="color: #171717; display: none;">
                                                    <asp:CheckBox ID="CheckBoxLoginAD" runat="server" OnCheckedChanged="CheckBoxLoginAD_CheckedChanged" AutoPostBack="true" Style="vertical-align: middle;" />
                                                    Login by Active Directory </small>
                                            </td>
                                            <td style="text-align: right;">
                                                <a href="#modalForgotPass" data-toggle="modal" style="text-align: right; color: green; font-size: 12px">Forgot Password?</a>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-12 inputGroupContainer" style="margin-top: 20px;">
                                    <asp:Button ID="btnSignInEncode" OnClientClick="return CheckFieldEncode();" runat="server" CssClass="btn btn-primary" Style="width: 100%; max-width: 100%; height: 42px; font-size: 20px; border-radius: 4px;" Text="Log In" Font-Bold="true" OnClick="btnSignInEncode_Click" />
                                    <asp:Button ID="btnSignIn" OnClientClick="return CheckField();" runat="server" CssClass="btn btn-primary" Style="width: 100%; max-width: 100%; height: 42px; font-size: 20px; border-radius: 4px;" Text="Log In" Font-Bold="true" OnClick="btnSignIn_Click" Visible="false" />
                                    <asp:Button ID="ButtonRedirect" runat="server" Text="Redirect" OnClick="ButtonRedirect_Click" Style="display: none;" />
                                    <asp:Button ID="btnSignInQR" runat="server" CssClass="btn btn-primary" Style="width: 100%; max-width: 100%; height: 42px; font-size: 20px; border-radius: 4px;display:block;display:none;" Text="Log In" Font-Bold="true" OnClick="btnSignInQR_Click"/>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSignIn" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <!-- divider -->
                <div class="col-md-1" style="border-left-width:0;border-right-width: 1px;border-style:solid; border-image: linear-gradient( to bottom, transparent, #ccc,transparent ) 1 100%;">
                    <span class="border-right"></span>
                </div>
                <div class="col-md-5 border border-primary" id="qr-container">
                    <!-- QR here -->
                    <div class="row" style="text-align: center;" id="box-qr">
                        <img src="#" alt="QRCode" id="imgQR" style="width: 200px; height: 200px; visibility: hidden;" />
                    </div>
                    <p style="text-align: center; color: black">
                        <strong>Login With QR Code</strong>
                    </p>
                    <p style="color: black; text-align: center;">
                        <span>Open <strong>Doctor App</strong> on your phone</span>
                        <span>Go to <strong>More > Scan to Login EMR</strong></span>
                    </p>
                    <p style="color: black; font-size: 12px; text-align: center; text-decoration: underline;">
                        <a href="#" onclick="ShowTutorial()" style="text-decoration:underline">Need Help? See the video tutorial</a>
                    </p>
                </div>
            </div>
        </div>
        <%--<div class="login-box" style="width: 400px;">
            <asp:UpdatePanel ID="upError" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="login-box-body" style="background-color: white; width: 100%; border: 1px; border-radius: 5px; box-shadow: 0px 1px 2px rgba(26, 34, 105, 0.5); padding: 30px;">
                        <div class="login-logo" style="color: Blue; font-weight: bold; height: 60px;">
                            <img src="<%= Page.ResolveClientUrl("~/Images/Login/ic_LogoLogin.svg") %>" width="80%" />
                            <br />
                        </div>

                        <div class="form-group row">
                            <div class="col-md-12 inputGroupContainer" style="margin-top: 10px;">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:TextBox ID="txtUsername" Style="width: 100%; max-width: 100%;" runat="server" CssClass="form-control" placeholder="Username" Visible="false"></asp:TextBox>
                                    <input id="inputTxtUsername" type="text" style="width: 100%; max-width: 100%;" class="form-control" placeholder="Username" />
                                    <asp:HiddenField ID="hfTxtUsername" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-12 inputGroupContainer" style="margin-top: 10px;">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                    <asp:TextBox ID="txtPassword" TextMode="Password" Style="width: 100%; max-width: 100%;" runat="server" CssClass="form-control" placeholder="Password" Visible="false"></asp:TextBox>
                                    <input id="inputTxtPassword" type="password" style="width: 100%; max-width: 100%;" class="form-control" placeholder="Password" />
                                    <asp:HiddenField ID="hfTxtPassword" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-12 inputGroupContainer" style="text-align: right;">

                                <div style="text-align: right; margin-top: 5px; margin-left: 55px; font-size: 12px;">
                                    <p style="text-align: right; color: red; display: none" id="pError" runat="server"></p>
                                </div>

                                <table border="0" style="width: 100%;">
                                    <tr>
                                        <td style="text-align: left;">
                                            <small style="color: #171717; display: none;">
                                                <asp:CheckBox ID="CheckBoxLoginAD" runat="server" OnCheckedChanged="CheckBoxLoginAD_CheckedChanged" AutoPostBack="true" Style="vertical-align: middle;" />
                                                Login by Active Directory </small>
                                        </td>
                                        <td style="text-align: right;">
                                            <a href="#modalForgotPass" data-toggle="modal" style="text-align: right; color: green; font-size: 12px">Forgot Password?</a>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-12 inputGroupContainer" style="margin-top: 20px;">
                                <asp:Button ID="btnSignInEncode" OnClientClick="return CheckFieldEncode();" runat="server" CssClass="btn btn-primary" Style="width: 100%; max-width: 100%; height: 42px; font-size: 20px; border-radius: 4px;" Text="Log In" Font-Bold="true" OnClick="btnSignInEncode_Click"/>
                                <asp:Button ID="btnSignIn" OnClientClick="return CheckField();" runat="server" CssClass="btn btn-primary" Style="width: 100%; max-width: 100%; height: 42px; font-size: 20px; border-radius: 4px;" Text="Log In" Font-Bold="true" OnClick="btnSignIn_Click" Visible="false" />
                                <asp:Button ID="ButtonRedirect" runat="server" Text="Redirect" OnClick="ButtonRedirect_Click" style="display:none;"/>
                                <!-- Button QR -->
                                <p class="text-center" style="margin-top:0.5em;"><strong>Or</strong></p>
                                <asp:Button ID="btnSignInQR" runat="server" CssClass="btn btn-primary" Style="width: 100%; max-width: 100%; height: 42px; font-size: 20px; border-radius: 4px;display:block;display:none;" Text="Log In" Font-Bold="true" OnClick="btnSignInQR_Click"/>
                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalQR" 
                                    style="width: 100%; max-width: 100%; height: 42px; font-size: 20px; border-radius: 4px;" onclick="GetQR()"><strong>Login With QR Code</strong></button>
                            </div>
                        </div>

                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSignIn" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>--%>

        <div style="bottom: 3px; right: 5px; position: fixed; text-align:center; color:gray; font-size:10px;">
            Version : <asp:Label ID="LabelVersion" runat="server" Text="-XXX-"></asp:Label>
        </div>

        <!-- ##### Modal Forgot Pass ##### -->
        <div class="modal fade" id="modalForgotPass" role="dialog" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 100px;" data-keyboard="false">

            <div class="modal-dialog" style="width: 420px">
                <div class="modal-content" style="border-radius: 6px;">

                    <!-- Modal Header -->
                    <div class="modal-header" style="padding: 8px;">
                        <button type="button" class="close" data-dismiss="modal">×</button>
                        <div>
                            <b>Forgot Password</b>
                        </div>
                    </div>

                    <!-- Modal body -->
                    <div class="modal-body">
                        Silakan hubungi staff IT di rumah sakit tempat anda bekerja.
                        <br />
                        <br />
                        <div style="text-align: right;">
                            <button class="btn btn-success btn-sm" style="width: 100px;" data-dismiss="modal">Ok </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- End of Modal Forgot Pass -->

        <!-- ##### Modal Change Pass ##### -->
        <div class="modal fade" id="modalChangePass" role="dialog" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 100px;" data-keyboard="false">

            <div class="modal-dialog" style="width: 550px">
                <div class="modal-content" style="border-radius: 6px;">

                    <!-- Modal Header -->
                    <div class="modal-header" style="padding: 8px;">
                        <button type="button" class="close" data-dismiss="modal">×</button>
                        <div>
                            <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                            <b>
                            <asp:Label ID="LabelChangePassTitle" runat="server" Text="-"></asp:Label>
                            </b>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <!-- Modal body -->
                    <div class="modal-body">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                
                                <div style="text-align:center;">
                                    <iframe name="iframechangepass" id="iframechangepass" runat="server" style="width: 500px; height: 300px; border: none; overflow-y: auto;"></iframe>
                                </div>
                                <div style="text-align: right; display:none;">
                                    <button class="btn btn-success btn-sm" style="width: 100px;" data-dismiss="modal">Ok </button>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <!-- End of Modal Forgot Pass -->

        <%-- ============================================= MODAL CHOOSE ORG ============================================== --%>
        <div class="modal fade" id="modalChooseOrg" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <asp:Panel ID="panel1" runat="server" DefaultButton="btnContinue">
                <div class="modal-dialog" style="width: 30%;padding-top:15%">
                    <div class="modal-content" style="height: 100%; border-radius: 5px">

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h5 class="modal-title">
                                        <asp:Label ID="Label1" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Select Organization"></asp:Label></h5>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="modal-body">
                            <div style="width: 100%;">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div id="foundThis" runat="server">
                                            <asp:DropDownList ID="dropdownOrg" runat="server" CssClass="selecpicker" data-live-search="true" data-size="10" data-width="100%" data-dropup-auto="false" Style="font-size: 14px; width:100%;"></asp:DropDownList></div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="modal-footer" style="width: 100%">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Button runat="server" Text="Continue" CssClass="btn btn-success btn-sm" class="box" Width="30%" ID="btnContinue" OnClick="btnContinue_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <%-- ============================================= END OF MODAL CHOOSE ORG ============================================== --%>

<%--        <!-- ##### Modal QR ##### -->
        <div class="modal fade" id="modalQR" role="dialog" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false">

            <div class="modal-dialog" style="max-width:430px;min-width:420px;">
                <div class="modal-content" style="border-radius: 6px;">
                    <div class="modal-body" style="background-color: white; width: 100%; border: 1px; border-radius: 5px; box-shadow: 0px 1px 2px rgba(26, 34, 105, 0.5); padding: 10px;margin-top:0.7em;">
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <strong>Login With QR Code</strong>
                                <button type="button" class="close pull-right" data-dismiss="modal" aria-hidden="true">&times;</button>
                            </div>
                        </div>
                        <div class="row" style="text-align:center;" id="box-qr">
                            <img src="#" alt="QRCode" id="imgQR" style="width:350px;height:330px;visibility:hidden;"/>
                        </div>
                        <p style="text-align:center;">
                            <strong>Scan From Doctor App</strong>
                        </p>
                        <p>1. Open Doctor App on your phone</p>
                        <p> 2. Go to A>A> Scan QR Code</p>
                        <p> 3. Scan this image to login</p>
                        <p><a href="#" onclick="CloseModalQR()">Or login with User Name and Password</a></p>
                    </div>
                </div>
            </div>
        </div>
        <!-- End of Modal QR -->--%>
        <!-- Modal Tutorial -->
        <div class="modal fade" id="modalTutorial" role="dialog" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false">
            <div class="modal-dialog modal-dialog-turorial">
                <div class="modal-content" style="border-radius: 6px;">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h5 class="modal-title"><b>Video Tutorial</b></h5>
                    </div>
                    <div class="modal-body">
                        <div style="display:flex;" class="video-container">
                            <video controls="controls" muted="muted" style="width:100%;height:auto;" id="videoControl">
                                <source src="" type='video/mp4'/>
                            </video>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- end of modal Tutorial -->

    </form>

    <script type="text/javascript">
        history.pushState(null, null, document.title);
        window.addEventListener('popstate', function () {
            history.pushState(null, null, document.title);
        });
        function CheckField() {
            var UserName = $("[id$='txtUsername']").val();
            var PassWord = $("[id$='txtPassword']").val();

            $("[id$='txtUsername']").removeAttr("style");
            $("[id$='txtPassword']").removeAttr("style");

            if (UserName.length <= 0 && PassWord.length <= 0) {
                $("[id$='txtUsername']").attr("style", "display:block; border-color:red;");
                $("[id$='txtPassword']").attr("style", "display:block; border-color:red;");
                $("[id$='pError']").removeAttr("style");
                $("[id$='txtUsername']").focus();
                $("[id$='pError']").attr("style", "display:block; color:red;");
                document.getElementById("pError").innerHTML = "Masukkan Username dan Password !";

                return false;
            }
            else if (UserName.length <= 0 && PassWord.length > 0) {
                $("[id$='txtUsername']").attr("style", "display:block; border-color:red;");
                $("[id$='txtUsername']").focus();
                $("[id$='pError']").removeAttr("style");
                $("[id$='pError']").attr("style", "display:block; color:red;");
                document.getElementById("pError").innerHTML = "Masukkan Username !";

                return false;
            }
            else if (UserName.length > 0 && PassWord.length <= 0) {
                if (document.getElementById('<%= CheckBoxLoginAD.ClientID %>').checked == true) {
                    return true;
                }
                else {
                    $("[id$='txtPassword']").attr("style", "display:block; border-color:red;");
                    $("[id$='txtPassword']").focus();
                    $("[id$='pError']").removeAttr("style");
                    $("[id$='pError']").attr("style", "display:block; color:red;");
                    document.getElementById("pError").innerHTML = "Masukkan Password !";

                    return false;
                }
            }
            else {
                return true;
            }
        }

        $(document).ready(function () {
            $('.selecpicker').selectpicker();
            toastr.options.positionClass = "toast-top-center";

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        $('.selecpicker').selectpicker();
                    }
                });
            };
            localStorage.clear();
        });

        function redirectlogin() {
            document.getElementById("<%= ButtonRedirect.ClientID %>").click();
        }

        function hideChangePass() {
            $('#modalChangePass').modal('hide');
            changePassSuccess();
        }

        function changePassSuccess() {
            toastr.success('Change Password Success.', 'Success');
            toastr.options.positionClass = "toast-top-center";
        }

        function CheckFieldEncode() {
            var UserName = $("[id$='inputTxtUsername']").val();
            var PassWord = $("[id$='inputTxtPassword']").val();

            $("[id$='hfTxtUsername']").val(btoa(UserName));
            $("[id$='hfTxtPassword']").val(btoa(PassWord));

            //$("[id$='hfTxtUsername']").val(crypt("salt", UserName));
            //$("[id$='hfTxtPassword']").val(crypt("salt", PassWord));

            //-------------------------------------------------

            $("[id$='inputTxtUsername']").removeAttr("style");
            $("[id$='inputTxtPassword']").removeAttr("style");

            if (UserName.length <= 0 && PassWord.length <= 0) {
                $("[id$='inputTxtUsername']").attr("style", "display:block; border-color:red;");
                $("[id$='inputTxtPassword']").attr("style", "display:block; border-color:red;");
                $("[id$='pError']").removeAttr("style");
                $("[id$='inputTxtUsername']").focus();
                $("[id$='pError']").attr("style", "display:block; color:red;");
                document.getElementById("pError").innerHTML = "Masukkan Username dan Password !";

                return false;
            }
            else if (UserName.length <= 0 && PassWord.length > 0) {
                $("[id$='inputTxtUsername']").attr("style", "display:block; border-color:red;");
                $("[id$='inputTxtUsername']").focus();
                $("[id$='pError']").removeAttr("style");
                $("[id$='pError']").attr("style", "display:block; color:red;");
                document.getElementById("pError").innerHTML = "Masukkan Username !";

                return false;
            }
            else if (UserName.length > 0 && PassWord.length <= 0) {
                if (document.getElementById('<%= CheckBoxLoginAD.ClientID %>').checked == true) {
                    return true;
                }
                else {
                    $("[id$='inputTxtPassword']").attr("style", "display:block; border-color:red;");
                    $("[id$='inputTxtPassword']").focus();
                    $("[id$='pError']").removeAttr("style");
                    $("[id$='pError']").attr("style", "display:block; color:red;");
                    document.getElementById("pError").innerHTML = "Masukkan Password !";

                    return false;
                }
            }
            else {
                return true;
            }
        }

        const crypt = (salt, text) => {
            const textToChars = (text) => text.split("").map((c) => c.charCodeAt(0));
            const byteHex = (n) => ("0" + Number(n).toString(16)).substr(-2);
            const applySaltToChar = (code) => textToChars(salt).reduce((a, b) => a ^ b, code);

            return text
                .split("")
                .map(textToChars)
                .map(applySaltToChar)
                .map(byteHex)
                .join("");
        };

        const decrypt = (salt, encoded) => {
            const textToChars = (text) => text.split("").map((c) => c.charCodeAt(0));
            const applySaltToChar = (code) => textToChars(salt).reduce((a, b) => a ^ b, code);
            return encoded
                .match(/.{1,2}/g)
                .map((hex) => parseInt(hex, 16))
                .map(applySaltToChar)
                .map((charCode) => String.fromCharCode(charCode))
                .join("");
        };
    </script>

    <!-- Script Embed QR Code -->
    <script type="text/javascript" src="../../Content/js.QR/loadingoverlay.min.js"></script>
    <script type="text/javascript" src="../../Content/js.QR/login.qr.js" ></script>

</body>
</html>
