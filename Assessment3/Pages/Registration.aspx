<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Assessment3.Pages.Registration" %>

<%--
Author: Micheal Thompson
Student ID - 3100553617
Date: 13/11/2017
Known Bugs: None
--%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .TextBox_Spacing { margin-top: 5px; }
        .Scrollable { height: 400px; overflow-y: scroll }
    </style>
    <h1>Registration List</h1>
    <div class="container-fluid Scrollable TextBox_Spacing" style="padding-bottom: 2em;">
        <asp:Table runat="server" CellPadding="2" ID="ItemsTable" CssClass="table table-bordered table table-hover">
            <asp:TableHeaderRow>
                <asp:TableCell Font-Bold="true">Registration ID:</asp:TableCell>
                <asp:TableCell Font-Bold="true">Customer ID:</asp:TableCell>
                <asp:TableCell Font-Bold="true">Product Code:</asp:TableCell>
                <asp:TableCell Font-Bold="true">Registration Date:</asp:TableCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>

    <div class="container-fluid">
        <asp:Table HorizontalAlign="Center" runat="server">
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">Customer:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:DropDownList CssClass="form-control TextBox_Spacing" runat="server" ID="CustomerDropDown"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">Product:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:DropDownList CssClass="form-control TextBox_Spacing" runat="server" ID="ProductDropDown"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">Reg Date:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ReadOnly="true" ID="DateTextBox"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Button CssClass="btn btn-primary" runat="server" OnClick="AddRegistration" Text="Add"/></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
