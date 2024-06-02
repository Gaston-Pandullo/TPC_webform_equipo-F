<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_webforms_equipo_F._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Mesas</h1>
    <p>Seleccione una mesa y agregue los platos que quiera!</p>
    <p class="text-end">
        <span class="fw-bold">2</span> mesas libres de <span class="fw-bold">5</span>
    </p>
    <main>
        <asp:Button ID="btnTable1" runat="server" Text="1" CssClass="table-button " OnClick="TableButton_Click" CommandArgument="1" />
        <asp:Button ID="btnTable2" runat="server" Text="2" CssClass="table-button" OnClick="TableButton_Click" CommandArgument="2" />
        <asp:Button ID="btnTable3" runat="server" Text="3" CssClass="table-button green" OnClick="TableButton_Click" CommandArgument="3" />
        <asp:Button ID="btnTable4" runat="server" Text="4" CssClass="table-button green" OnClick="TableButton_Click" CommandArgument="4" />
        <asp:Button ID="btnTable5" runat="server" Text="5" CssClass="table-button" OnClick="TableButton_Click" CommandArgument="5" />
    </main>

    <style>
        .table-button {
            width: 100px;
            height: 100px;
            margin: 10px;
            font-size: 24px;
            text-align: center;
            background-color: indianred;
        }
        .green{
            background-color: aquamarine;
        }
    </style>
</asp:Content>
