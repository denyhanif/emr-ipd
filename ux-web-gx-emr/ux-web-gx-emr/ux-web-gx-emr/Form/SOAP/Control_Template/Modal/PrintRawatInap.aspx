<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintRawatInap.aspx.cs" Inherits="Form_SOAP_Control_Template_Modal_PrintRawatInap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="~/Content/font-awesome/css/font-awesome.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="~/Content/ionicons/css/ionicons.css" />
    <!-- Ichek -->
    <link rel="stylesheet" href="~/Content/plugins/iCheck/all.css" />
    <!-- Datepicker -->
    <link rel="stylesheet" href="~/Content/plugins/datepicker/datepicker3.css" />
    <!-- DropDown -->
    <link rel="stylesheet" href="~/Content/plugins/select2/select2.min.css" />
    <!-- Mask -->
    <link rel="stylesheet" href="~/Content/plugins/jasny-bootstrap/css/jasny-bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="~/Content/dist/css/AdminLTE.css" />
    <link rel="stylesheet" href="~/Content/dist/css/skins/skin-blue-light.css" />
    <link rel="stylesheet" href="~/Content/Site.css" />
    <style>
        body{
            padding-left:100px;
            padding-right:100px;
            font-family: Helvetica, Arial, sans-serif;
        }
        table{
            boder-spacing:0px;
             border-collapse: collapse;
        }
        .print tr td{
            background:#e3dcdc;
            font-family: Roboto, Arial, sans-serif;
        }
    </style>
</head>
<body>
   
        
                <table style="width: 99%;">
                    <%-- =============================================== HEADER ==================================================== --%>
                    <thead>
                        <tr>
                            <td colspan="3">
                                <div class="btn-group btn-group-justified" style="padding-bottom: 13px" role="group" aria-label="...">
                                    <div class="btn-group" role="group" style="vertical-align: top">
                                        
<%--                                        <asp:ImageButton runat="server" ImageUrl="~/Images/Icon/logo-SH.svg" Style="width: 280px;" runat="server" Enabled="false" />--%>
                                    </div>
                                    <div class="btn-group" role="group" style="text-align: left; padding-right: 13px; padding-left: 30px">
                                        <div>
                                            <b>Resume Medis
                                            <asp:Label runat="server" ID="AdmissionType"></asp:Label></b>
                                        </div>
                                        <table style="width: 50%;margin-left:60%">
                                            <tr>
                                                <td style="padding-right: 20px; vertical-align: top">
                                                    <label style="font-size: 13px">MR</label>
                                                </td>
                                                <td>
                                                    <label>: </label>
                                                    <asp:Label runat="server" ID="lblmrno" Style="font-size: 13px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin-right: 10px;width:80px; vertical-align: top">
                                                    <label style="font-size: 13px">Name</label>
                                                </td>
                                                <td>
                                                    <label>:<asp:Label runat="server" ID="lblnamepatient" Style="font-size: 13px">Juliana P dungsu</asp:Label></label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 20px; vertical-align: top">
                                                    <label style="font-size: 13px">TTL/Umur</label>
                                                </td>
                                                <td>
                                                    <label>:<asp:Label runat="server" ID="lbttl" Style="font-size: 13px">25 Jan 1995</asp:Label> / <asp:Label runat="server" ID="lblumur" Style="font-size: 13px">25T 17B 8M</asp:Label> </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 20px; vertical-align: top">
                                                    <label style="font-size: 13px">Sex</label>
                                                </td>
                                                <td>
                                                    <label>:<asp:Label runat="server" ID="lblsexpatient" Style="font-size: 13px">Perempuan</asp:Label></label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 20px; vertical-align: top">
                                                    <label style="font-size: 13px">Doctor</label>
                                                </td>
                                                <td>
                                                    <label> :<asp:Label runat="server" ID="lbldoctorprimary" Style="font-size: 13px">Dr. Eka wahyu</asp:Label></label>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td style="padding-right: 20px; vertical-align: top">
                                                    <label style="font-size: 13px">No.Adm</label>
                                                </td>
                                                <td>
                                                    <label> :<asp:Label runat="server" ID="lblNoAdmision" Style="font-size: 13px">Dr. Eka wahyu</asp:Label></label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </thead>
                    <%-- =============================================== END HEADER ==================================================== --%>

                    
                </table>
                 <asp:Label runat="server" style="font-size:18px;color:#171717;text-align:left;font-weight:bold;display:block" Text="Surat Pengatar Rawat Inap"></asp:Label>
                 <asp:Label runat="server" style="font-size:14px;color:#171717;text-align:left;display:block" Text="Admission Referal Letter"></asp:Label>
                 <asp:Label runat="server" style="font-size:14px;color:#171717;text-align:left;display:block" Text="Dari OPD"></asp:Label>

                <table style="width:100%" class="print" >
                    <tr style="background-color: black; border: 1px solid #B9B9B9">
                        <td style="width: 34%;height:40px;">
                            
                            <label style="color: #000000;font-size: 13px;font-weight:bold;display:block;padding-top:3px;padding-left:11px">Dokter penanggung Jawab Pelayanan</label>
                            <label style="padding-left: 13px;font-size:11px;font-style:italic;font-weight:normal">Primary Doktor</label>
                        </td>
                        <td colspan="2" style="width: 60%;background-color:white">
                            <label>
                                <label style="padding-left: 13px">Deskripsi</label></label>
                        </td>
                    </tr>
                    <tr style="background-color: black; border: 1px solid #B9B9B9">
                        <td style="width: 40%;height:40px;">
                            
                            <label style="color: #000000;font-size: 13px;font-weight:bold;display:block">Dokterpenanggungpenanggungpenanggung penanggung Jawab Pelayanan</label>
                            <label style="padding-left: 13px;">Primary Doktor</label>
                        </td>
                        <td colspan="2" style="width: 60%;background-color:white">
                            <label>
                                <label style="padding-left: 13px">Deskripsi</label></label>
                        </td>
                    </tr>
                     <tr style="background-color: black; border: 1px solid #B9B9B9">
                        <td style="width: 40%;height:40px; background-color:#e3dcdc">
                            
                            <label style="color: #000000;font-size: 13px;font-weight:bold;display:block">Dokter penanggung Jawab Pelayanan</label>
                            <label style="padding-left: 13px;">Primary Doktor</label>
                        </td>
                        <td colspan="2" style="width: 60%;background-color:white">
                            <label>
                                <label style="padding-left: 13px">Deskripsi</label></label>
                        </td>
                    </tr>
                     <tr style="background-color: black; border: 1px solid #B9B9B9">
                        <td style="width: 40%;height:40px; background-color:#B9B9B9">
                            
                            <label style="color: #000000;font-size: 13px;font-weight:bold;display:block">Dokter penanggung Jawab Pelayanan</label>
                            <label style="padding-left: 13px;">Primary Doktor</label>
                        </td>
                        <td colspan="2" style="width: 60%;background-color:white">
                            <label>
                                <label style="padding-left: 13px">Deskripsi</label></label>
                        </td>
                    </tr>
                     <tr style="background-color: black; border: 1px solid #B9B9B9">
                        <td style="width: 40%;height:40px; background-color:#B9B9B9">
                            
                            <label style="color: #000000;font-size: 13px;font-weight:bold;display:block">Dokter penanggung Jawab Pelayanan</label>
                            <label style="padding-left: 13px;">Primary Doktor</label>
                        </td>
                        <td colspan="2" style="width: 60%;background-color:white">
                            <label>
                                <label style="padding-left: 13px">Deskripsi</label></label>
                        </td>
                    </tr>
                     <tr style="background-color: black; border: 1px solid #B9B9B9">
                        <td style="width: 40%;height:40px; background-color:#B9B9B9">
                            
                            <label style="color: #000000;font-size: 13px;font-weight:bold;display:block">Dokter penanggung Jawab Pelayanan</label>
                            <label style="padding-left: 13px;">Primary Doktor</label>
                        </td>
                        <td colspan="2" style="width: 60%;background-color:white">
                            <label>
                                <label style="padding-left: 13px">Deskripsi</label></label>
                        </td>
                    </tr>
                     <tr style="background-color: black; border: 1px solid #B9B9B9">
                        <td style="width: 40%;height:40px; background-color:#B9B9B9">
                            
                            <label style="color: #000000;font-size: 13px;font-weight:bold;display:block">Dokter penanggung Jawab Pelayanan</label>
                            <label style="padding-left: 13px;">Primary Doktor</label>
                        </td>
                        <td colspan="2" style="width: 60%;background-color:white">
                            <label>
                                <label style="padding-left: 13px">Deskripsi</label></label>
                        </td>
                          <tr style="background-color: black; border: 1px solid #B9B9B9">
                        <td style="width: 40%;height:40px; background-color:#B9B9B9">
                            
                            <label style="color: #000000;font-size: 13px;font-weight:bold;display:block">Dokter penanggung Jawab Pelayanan</label>
                            <label style="padding-left: 13px;">Primary Doktor</label>
                        </td>
                        <td colspan="2" style="width: 60%;background-color:white">
                            <label>
                                <label style="padding-left: 13px">Deskripsi</label></label>
                        </td>
                    </tr>
                    </tr>
                </table>

</body>
</html>
