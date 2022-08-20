<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewOTScheduling.aspx.cs" Inherits="Form_General_View_ViewOTScheduling" %>

<%@ Register Src="~/Form/General/Control/PatientCard.ascx" TagPrefix="tag1" TagName="PatientCard" %>

<asp:Content ID="OTScheduling" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server" ID="UPOTS" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="background-color: #ededed; text-align:right;">

                <iframe name="IframeOTS" id="IframeOTS" runat="server" style="width: 100%; height: calc(100vh - 50px); border: none; margin-bottom: -6px;"></iframe>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
