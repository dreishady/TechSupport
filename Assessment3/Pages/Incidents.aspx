<%@ Page Title="Incident Maintenance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Incidents.aspx.cs" Inherits="Assessment3.Incidents" %>

<%--
Author: Corey Schmid | Micheal Thompson
Student ID - 0100601817 | 3100553617
Date: 13/11/2017
Known Bugs: None
--%>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .Scrollable { height: 400px; overflow-y: scroll }
        .EditButton { margin-right: 5px; }
        .TextBox_Spacing { margin-top: 5px; }
    </style>
    <h1><%: Title %></h1>
    <div class="container-fluid">
        <asp:Table style="margin-top: 5px" Visible="false" ID="CustomerTable" runat="server" HorizontalAlign="center">
            <asp:TableRow>
                <asp:TableCell> <asp:Label runat="server">Search Customer:</asp:Label> </asp:TableCell>
                <asp:TableCell> <asp:TextBox CssClass="form-control" ID="CustomerIDTxtBox" runat="server"></asp:TextBox> </asp:TableCell>
                <asp:TableCell> <asp:Button CssClass="btn btn-primary" OnClick="SearchForCustomer" runat="server" Text="Get Customer" /> </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell> Customer ID: </asp:TableCell>
                <asp:TableCell> <asp:TextBox class="form-control TextBox_Spacing" runat="server" ReadOnly="true" ID="CustomerTextBox1"></asp:TextBox> </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell> Name: </asp:TableCell>
                <asp:TableCell> <asp:TextBox runat="server" CssClass="form-control" style="margin-top:5px;" ReadOnly="true" ID="NameTextBox1"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell> Address: </asp:TableCell>
                <asp:TableCell> <asp:TextBox runat="server" CssClass="form-control" style="margin-top:5px;" ReadOnly="true" ID="AddressTextBox1"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell> City, State, Zip: </asp:TableCell>
                <asp:TableCell> <asp:TextBox runat="server" CssClass="form-control" style="margin-top:5px;" ReadOnly="true" ID="LocationTextBox1"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell> Phone: </asp:TableCell>
                <asp:TableCell> <asp:TextBox runat="server" CssClass="form-control" style="margin-top:5px;" ReadOnly="true" ID="PhoneTextBox1"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell> Email: </asp:TableCell>
                <asp:TableCell> <asp:TextBox runat="server" CssClass="form-control" style="margin-top:5px;" ReadOnly="true" ID="EmailTextBox1"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <div class="Scrollable container-fluid">
        <asp:Table ID="IncidentsTable" runat="server" CssClass="table table-bordered table table-hover Scrollable TextBox_Spacing">
        <asp:TableHeaderRow>
            <asp:TableCell Width="110px" Font-Bold="true">Incident ID</asp:TableCell>
            <asp:TableCell Width="110px" Font-Bold="true">Customer ID</asp:TableCell>
            <asp:TableCell Width="115px" Font-Bold="true">Product Code</asp:TableCell>
            <asp:TableCell Width="85px" Font-Bold="true">Tech ID</asp:TableCell>
            <asp:TableCell Font-Bold="true">Date Opened</asp:TableCell>
            <asp:TableCell Font-Bold="true">Date Closed</asp:TableCell>
            <asp:TableCell Font-Bold="true">Title</asp:TableCell>
            <asp:TableCell Font-Bold="true">Description</asp:TableCell>
        </asp:TableHeaderRow>
    </asp:Table>
    </div>
    <div class="container-fluid">
        <asp:Button CssClass="btn btn-primary" runat="server" ID="AddIncidentButton" Visible="false" OnClick="ClickEvent"/>
        <asp:Table ID="AddIncidentTable" HorizontalAlign="center" runat="server" Visible="false">

            <asp:TableRow>
                <asp:TableCell>Customer ID: </asp:TableCell>
                <asp:TableCell><asp:DropDownList CssClass="form-control TextBox_Spacing" runat="server" ID="CustomerDropDown"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell> Product Code: </asp:TableCell>
                <asp:TableCell> <asp:DropDownList CssClass="form-control TextBox_Spacing" runat="server" ID="ProductDropDown"></asp:DropDownList> </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Tech ID: </asp:TableCell>
                <asp:TableCell><asp:DropDownList CssClass="form-control TextBox_Spacing" runat="server" ID="TechDropDown"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell> Title: </asp:TableCell>
                <asp:TableCell> <asp:TextBox class="form-control TextBox_Spacing" runat="server" ID="TitleTextBox"></asp:TextBox> </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell> Description: </asp:TableCell>
                <asp:TableCell> <textarea Height="200px" Width="150%" class="form-control TextBox_Spacing" id="DescriptionTextBox" runat="server"></textarea> </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell> <asp:TableCell> <asp:Button class="btn btn-primary" runat="server" OnClick="AddIncident" Text="Add"/></asp:TableCell> </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>

