<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdRadResult.ascx.cs" Inherits="Form_General_Control_StdRadResult" %>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div id="div_Radiology_detail" runat="server"></div>
        <div id="img_noData" runat="server" style="text-align: center;">
            <div>
                <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
            </div>
            <div>
                <span>
                    <h3 style="font-weight: 700; color: #585A6F">
                        <%--<label style="display: <%=setENG%>;">Oops! There is no data </label>
                        <label style="display: <%=setIND%>;">Oops! Tidak ada data </label>--%>
                        <label id="lblbhs_nodata">Oops! There is no data </label>
                    </h3>
                </span>
                <span style="font-size: 14px; color: #585A6F">
                    <%--<label style="display: <%=setENG%>;">Please search another date or parameter </label>
                    <label style="display: <%=setIND%>;">Silakan cari tanggal atau parameter lain </label>--%>
                    <label id="lblbhs_subnodata">Please search another date or parameter </label>
                </span>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>