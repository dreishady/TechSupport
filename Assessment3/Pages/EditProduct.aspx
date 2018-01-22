<%@ Page Title="Edit Product" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="Assessment3.Pages.EditProduct" %>

<%--
Author     : Ben Moir | Waleed 
Student ID : 5101965116 | 6100758617
Date       : 13/11/2017
Known Bugs : None
--%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .TextBox_Spacing { margin-top: 5px; }
    </style>

    <h1><%: Title %></h1>

    <div class="container-fluid">
        <asp:Table HorizontalAlign="Center" runat="server">
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">Name:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ID="NameTextBox"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">Version:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ID="VersionTextBox"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">Release Date:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ID="ReleaseDateTextBox"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Button CssClass="btn btn-primary" runat="server" OnClick="ApplyChanges" Text="Apply"/></asp:TableCell>
                <asp:TableCell><asp:Button style="float: right" CssClass="btn btn-primary" runat="server" OnClick="Cancel" Text="Cancel" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
