<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdLabResult.ascx.cs" Inherits="Form_General_Control_StdLabResult" %>

<script type="text/javascript">
    function switchBahasax()
    {
        if (document.getElementById('<%=HFisBahasax.ClientID%>') != null) {
            var bahasa = document.getElementById('<%=HFisBahasax.ClientID%>').value;
            if (bahasa == "ENG")
            {
                if (document.getElementById('lblbhs_nodatax') != null) {
                    document.getElementById('lblbhs_nodatax').innerHTML = "Oops! There is no data";
                    //document.getElementById('lblbhs_subnodata').innerHTML = "Please search another date or parameter";
                }

            }
            else if (bahasa == "IND")
            {
                if (document.getElementById('lblbhs_nodatax') != null) {
                    document.getElementById('lblbhs_nodatax').innerHTML = "Oops! Tidak ada data";
                    //document.getElementById('lblbhs_subnodata').innerHTML = "Silakan cari tanggal atau parameter lain";
                }
            }
        }
    }
</script>

<asp:HiddenField ID="HFisBahasax" runat="server" />
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div runat="server" id="panel1"></div>

        <%--<div style="margin-top:2%;text-align:center;border-radius:4px; margin-left:1%; margin-right:1%">
            <asp:Label runat="server" ID="lblNoData" Text="No Laboratory Data" Visible="true" Font-Names="Helvetica" Font-Size="25px"></asp:Label>
        </div>--%>

        <div id="img_noData" runat="server" style="text-align: center; display:none;">
            <div>
                <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
            </div>
            <div runat="server">
                <span>
                    <h3 style="font-weight: 700; color: #585A6F">
                         <%--<label style="display: <%=setENG%>;">Oops! There is no data </label>
                         <label style="display: <%=setIND%>;">Oops! Tidak ada data </label>--%>
                        <label id="lblbhs_nodatax">Oops! There is no data </label>
                    </h3>
                </span>
                <span style="font-size: 14px; color: #585A6F">
                    <%--<label style="display: <%=setENG%>;">Please search another date or parameter </label>
                    <label style="display: <%=setIND%>;">Silakan cari tanggal atau parameter lain </label>--%>
                </span>
            </div>
        </div>
        <div id="img_noConnection" runat="server" visible="false" style="text-align: center;">
            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noConnection.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px;" />
            <span>
                <h3 style="font-weight: 700; color: #585A6F">No internet connection</h3>
            </span>
            <span style="font-size: 14px; color: #585A6F">Please check your connection & refresh</span>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<%--modal--%>

<div id="modalcomment" class="modal fade" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="width: 50%; margin-top:5%;" runat="server">
        <div class="modal-content" style="border-radius: 7px;">
            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title" style="text-align: left">
                    <asp:Label ID="Label1" Style="font-family: Arial, Helvetica, sans-serif;" runat="server" Text="Test Comment"></asp:Label></h4>
            </div>
            <div class="modal-body" style="background-color: white; border-radius: 7px">
                <div style="width: 100%">
                    <b><asp:Label ID="LabelLabName" runat="server" Text="-"></asp:Label></b>
                    <br />
                    <br />
                    <asp:Label ID="LabelComment" runat="server" Text="-"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</div>

<script>
    function ViewComment(testname, testcomment) {
        $('#modalcomment').appendTo("body").modal('show');

        var nm = document.getElementById("<%=LabelLabName.ClientID%>");
        var cm = document.getElementById("<%=LabelComment.ClientID%>");
        nm.innerHTML = testname.replace(/_/g, " ");
        cm.innerHTML = testcomment.replace(/_/g, " ");
        
    }
</script>