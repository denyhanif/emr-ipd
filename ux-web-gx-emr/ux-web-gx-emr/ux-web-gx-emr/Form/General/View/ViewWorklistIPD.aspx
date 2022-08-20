<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewWorklistIPD.aspx.cs" Inherits="Form_General_View_ViewWorklistIPD" %>

<asp:Content ID="WorklistIPD" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server" ID="UPIPD" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="background-color: #ededed; text-align:right;">

                <iframe name="IframeIPD" id="IframeIPD" runat="server" style="width: 100%; height: calc(100vh - 50px); border: none; margin-bottom: -6px;"></iframe>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
