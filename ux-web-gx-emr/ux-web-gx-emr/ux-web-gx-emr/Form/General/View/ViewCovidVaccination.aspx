<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewCovidVaccination.aspx.cs" Inherits="Form_General_View_ViewCovidVaccination" %>

<asp:Content ID="CovidVaccination" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server" ID="UPCV" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="background-color: #ededed; text-align:right;">

                <iframe name="IframeCV19" id="IframeCV19" runat="server" style="width: 100%; height: calc(100vh - 50px); border: none; margin-bottom: -6px;"></iframe>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
