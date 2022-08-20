<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function autoCompleteEx_ItemSelected(sender, e) {
            alert(" Key : " + e.get_text() + "  Value :  " + e.get_value());
            $("[id$='hfItem']").val(e.get_value());
            var popup = document.getElementById('hfItem');
        }
        function clearText() {
            if (event.keyCode == 8) {
                alert('b');
                document.getElementById('TextBox1').val("");
                alert('c');
            }
            return true;
        }
    </script>
    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    <%--</div>--%>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
        <select class="selectpicker" data-live-search="true">
            <option>Mustard</option>
            <option>Ketchup</option>
            <option>Relish</option>
        </select>
        <asp:DropDownList runat="server" CssClass="selectpicker" data-size="10" data-dropup-auto="false" data-live-search="true" ID="test" />

        <asp:ScriptManager><Services><asp:ServiceReference Path="~/Form/General/OrderSet/AutoCompleted.asmx"</Services>
    </asp:ScriptManager>

<style type="text/css">
        .shadows {
            border: 1px; 
            border-radius: 7px;
            box-shadow: 0px 2px 5px #9293A0;
            margin-top: 0px;
        }

        .autocomplete_completionListElement
{
    background-color: white;
    color: windowtext;
    border-width: 1px;
    border-style: solid;
    overflow-x:auto;
    max-height: 300px;
    text-align: left;
    list-style-type: none;
    font-size:12px;
    padding:0px;
}
    </style>
         <asp:TextBox ID="txtDrugsSearch" onkeypress = "return SetContextKey();" runat="server"></asp:TextBox> 
                    <asp:AutoCompleteExtender ServicePath="~/Form/General/OrderSet/AutoCompleted.asmx" ServiceMethod="Test" MinimumPrefixLength="2"
                        ID="AutoCompleteExtender1" runat="server"
                        CompletionInterval="100"
                        EnableCaching="false" UseContextKey="true"
                        CompletionSetCount="2"
                        TargetControlID="txtDrugsSearch"
                        FirstRowSelected="false">
                    </asp:AutoCompleteExtender>

    </div>
        <input type="button" onclick="TestClick()" value="Click Me"></input>

         <div>        
            <asp:TextBox ID="txtAutoComplete" runat="server"></asp:TextBox>
            <asp:AutoCompleteExtender ID="SuggestionsAutoComplete" runat="server" 
                TargetControlID="txtAutoComplete" ServicePath="~/Form/General/OrderSet/AutoCompleted.asmx" ServiceMethod="Test"  MinimumPrefixLength="1"
                CompletionSetCount="20">
            </asp:AutoCompleteExtender>
            <br />
            <asp:Button ID="btnTrigger" runat="server" Text="Trigger Auto Complete" 
                OnClientClick="javascript:showAutoComplete()"/>        
        </div>
        <div><h1>aaaa</h1>
        <asp:GridView ID="Gridview2" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="No Data Available" HeaderStyle-BackColor="LightCoral"
            CssClass="gridview" AlternatingRowStyle-BackColor="LightSteelBlue"  DataKeyNames="casenumber" 
            AllowPaging="True" PageSize="10" BorderWidth="0px" AllowSorting="true"
            AutoPostBack="True" Width="100%" ShowHeaderWhenEmpty="true" >
                <Columns>
                    <asp:BoundField HeaderText="Case Number" DataField="casenumber" ItemStyle-CssClass="gridview_item_center" ItemStyle-Width="90"/>
                    <asp:TemplateField HeaderText="Add" ItemStyle-Width="20" ItemStyle-CssClass="gridview_ite_mcenter">
                        <ItemTemplate>
                            <asp:Dropdownlist runat="server">
                                <asp:ListItem Value="1" Text="AAA" />
                                <asp:ListItem Value="2" Text="BBB" />
                            </asp:Dropdownlist>
                        </ItemTemplate>
                   </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button runat="server" Text="Add" />
        </div>
        <div>
            <asp:UpdatePanel runat="server"><ContentTemplate>
                <asp:GridView ID="gvw_data" runat="server" AutoGenerateColumns="False" CssClass="box table"  BorderColor="Black"
                AllowPaging="True"  PageSize="300" HeaderStyle-CssClass="text-center" HeaderStyle-ForeColor="Blue"
                ShowHeaderWhenEmpty="True" DataKeyNames="Data" EmptyDataText="No Data" >
                <PagerStyle CssClass="pagination-ys"/>
                    <Columns>
                        <asp:TemplateField HeaderText="No" HeaderStyle-ForeColor="#3C8DBC" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_no" runat="server" Text="<%# (Container.DataItemIndex + 1).ToString() %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Data" DataField="Data" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true"></asp:BoundField>
                        <asp:TemplateField  HeaderText="Harga satuan" ItemStyle-Width="30%"  HeaderStyle-CssClass="hide-on-portrait" ItemStyle-CssClass="hide-on-portrait">
                            <ItemTemplate> 
                                <asp:TextBox ID="TextBox1" runat="server" OnKeyUp="return clearText();"></asp:TextBox>
                                <asp:TextBox ID="hfItem" runat="server" />
                                <asp:AutoCompleteExtender ID="ddl" OnClientItemSelected="autoCompleteEx_ItemSelected"  TargetControlID="TextBox1" runat="server" ServiceMethod="GetListOfSettings" ServicePath="~/Form/General/OrderSet/AutoCompleted.asmx" CompletionSetCount="10"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="CustomerAddress" DataField="CustomerAddress" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true"></asp:BoundField>
                    </Columns>
                </asp:GridView>
                <%--For the DropDownList--%>
               <%-- <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                   ConnectionString="<%$ ConnectionStrings:ConString %>" 
                   SelectCommand="SELECT CustomerId, CustomerName FROM [ViewCustomer] with(nolock)">
                </asp:SqlDataSource> --%> 
            </ContentTemplate></asp:UpdatePanel>
            </div>
            <asp:TextBox runat="server" ID="txtTest" />
</asp:Content>
