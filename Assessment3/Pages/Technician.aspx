<%@ Page Title="Technician Maintenance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Technician.aspx.cs" Inherits="Assessment3.Contact" %>

<%--
Author: Andrei Rico | Micheal Thompson
Student ID - 3106107616 | 3100553617
Date: 16/11/2017
Known Bugs: None
--%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>    
        .EditButton { margin-right: 5px; }
        .TextBox_Spacing { margin-top: 5px; }
    </style>
    <h1><%: Title %></h1>

    <asp:Table runat="server" ID="TechnicianTable" CssClass="table table-bordered table table-hover">
        <asp:TableHeaderRow>
            <asp:TableCell Font-Bold="true">Tech ID:</asp:TableCell>
            <asp:TableCell Font-Bold="true">Name:</asp:TableCell>
            <asp:TableCell Font-Bold="true">Tech Level:</asp:TableCell>
            <asp:TableCell Font-Bold="true">Phone:</asp:TableCell>
            <asp:TableCell Font-Bold="true">Email:</asp:TableCell>
        </asp:TableHeaderRow>
    </asp:Table>

    <div class="container-fluid">
        <asp:Button CssClass="btn btn-primary" runat="server" ID="AddTechnicianButton" Visible="false" OnClick="ClickEvent"/>
        <asp:Table ID="AddTechnicianTable" HorizontalAlign="center" runat="server" Visible="false">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server">Name: </asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                    <asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ID="TechNameTextBox"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server">Technician Level:</asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                    <asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ID="TechLevelTextBox"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server">Phone:</asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                    <asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ID="TechPhoneTextBox"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server">Email:</asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                    <asp:TextBox CssClass="form-control TextBox_Spacing" runat="server" ID="TechEmailTextBox"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button CssClass="btn btn-primary" OnClick="AddTechnician" Text="Add Technician" runat="server"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>

</asp:Content>
