<%@ Page Title="Product Maintenance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductMaintenance.aspx.cs" Inherits="Assessment3.ProductMaintenance" %>

<%--
    Author     : Ben Moir | Micheal Thompson
    Date       : 10/11/2017
    Student ID : 5101965116 | 3100553617
    Known Bugs : None 
--%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .EditButton { margin-right: 5px; }
    .TextBox_Spacing { margin-top: 5px; }
</style>
    <h1><%: Title %></h1>

    <div class="container-fluid" style="padding-bottom: 2em;">
        <asp:Table runat="server" CellPadding="2" ID="ItemsTable" CssClass="table table-bordered table table-hover">
            <asp:TableHeaderRow>
                <asp:TableCell Font-Bold="true">Product Code:</asp:TableCell>
                <asp:TableCell Font-Bold="true">Name:</asp:TableCell>
                <asp:TableCell Font-Bold="true">Version:</asp:TableCell>
                <asp:TableCell Font-Bold="true">Release Date:</asp:TableCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>

    <div class="container-fluid">
        <asp:Button CssClass="btn btn-primary" runat="server" Visible="false" ID="AddProductButton" OnClick="ClickEvent"/>
        <asp:Table ID="AddProductTable" HorizontalAlign="center" runat="server" Visible="false">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server">Product Code: </asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                    <asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ID="ProductCodeTextBox"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server">Name:</asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                    <asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ID="ProductNameTextBox"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server">Version:</asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                    <asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ID="ProductVersionTextBox"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button CssClass="btn btn-primary" OnClick="AddProduct" Text="Add Product" runat="server"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
