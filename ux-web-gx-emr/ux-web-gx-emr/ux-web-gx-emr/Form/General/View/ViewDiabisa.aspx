<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewDiabisa.aspx.cs" Inherits="Form_General_View_ViewDiabisa" %>

<asp:Content ID="Diabisa" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server" ID="UPDB" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="background-color: #ededed; text-align:right;">

                <iframe name="IframeViewDiabisa" id="IframeViewDiabisa" runat="server" style="width: 100%; height: calc(100vh - 50px); border: none; margin-bottom: -6px;"></iframe>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
