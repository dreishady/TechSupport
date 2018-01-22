<%@ Page Title="Edit Incident" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditIncident.aspx.cs" Inherits="Assessment3.Pages.EditIncident" %>

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
                <asp:TableCell><asp:Label runat="server">Customer:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:DropDownList CssClass="form-control TextBox_Spacing" runat="server" ID="CustomerDropDown"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">Technician:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:DropDownList CssClass="form-control TextBox_Spacing" runat="server" ID="TechnicianDropDown"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">Product:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:DropDownList CssClass="form-control TextBox_Spacing" runat="server" ID="ProductDropDown"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">Date Opened:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ReadOnly="true" ID="OpenDateTextBox"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">Date Closed:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ID="CloseDateTextBox"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">Title:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ID="TitleTextBox"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">Description:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ID="DescriptionTextBox"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Button CssClass="btn btn-primary" runat="server" OnClick="ApplyChanges" Text="Apply"/></asp:TableCell>
                <asp:TableCell><asp:Button style="float: right" CssClass="btn btn-primary" runat="server" OnClick="Cancel" Text="Cancel" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
