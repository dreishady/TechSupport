<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Assessment3.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background-image: url("https://i.imgur.com/1mebUic.jpg");
            background-size: cover;
        }
    </style>
    <div style="text-align: center;" runat="server" id="LoginScreenOne" visible="true">
        <h1>Assessment Three</h1>
        <p>
            Please log in with your Customer ID and password to get started.<br/>
            If you don't have a user account yet, please register first.
        </p>
    </div>

    <div class="row" runat="server" id="LoginScreen" visible="true">
        <asp:Table HorizontalAlign="center" runat="server">
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" ID="CustomerLabel">Customer ID: </asp:Label></asp:TableCell>
                <asp:TableCell><asp:TextBox class="form-control" runat="server" id="CustomerField" style="margin-top: 5px; margin-left: 2px;"/></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server">Password: </asp:Label></asp:TableCell>
                <asp:TableCell><asp:TextBox class="form-control" runat="server" id="PasswordField" style="margin-top: 5px; margin-left: 2px;" TextMode="Password"/></asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <span style="display: flex; align-items: center; justify-content: center;">
            <button class="btn btn-primary" runat="server" onserverclick="LoginProcess" style="margin-top: 5px;" id="LoginButton">
                Login <span class="glyphicon glyphicon-arrow-right" />
            </button>
        </span>
    </div>
    <div runat="server" id="TestDiv" visible="false">
        <h1 style="text-align: center">Tech Support</h1>

        <center><img src="https://d1qalvrjm3afwl.cloudfront.net/images/general/8ixbv7iq8xkw.png" /></center>

        <div class="row">
            <p style="float:left" runat="server" id="WelcomeText"></p>
            <p style="float:right" runat="server" id="RoleText"></p>
        </div>
    </div>
</asp:Content>
