<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_webforms_equipo_F._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="TableOrder" runat="server">
        <h1>Mesas</h1>
        <p>Seleccione una mesa y agregue los platos que quiera!</p>
        <p class="text-end">
            <span class="fw-bold">2</span> mesas libres de <span class="fw-bold">5</span>
        </p>
        <main>
            <asp:Button ID="btnTable1" runat="server" Text="1" CssClass="table-button" OnClick="TableButton_Click" CommandArgument="1" />
            <asp:Button ID="btnTable2" runat="server" Text="2" CssClass="table-button" OnClick="TableButton_Click" CommandArgument="2" />
            <asp:Button ID="btnTable3" runat="server" Text="3" CssClass="table-button" OnClick="TableButton_Click" CommandArgument="3" />
            <asp:Button ID="btnTable4" runat="server" Text="4" CssClass="table-button" OnClick="TableButton_Click" CommandArgument="4" />
            <asp:Button ID="btnTable5" runat="server" Text="5" CssClass="table-button" OnClick="TableButton_Click" CommandArgument="5" />
        </main>
    </asp:Panel>

    <asp:Button ID="btnAbrirMesa" runat="server" Text="Abrir Mesa" OnClick="btnAbrirMesa_Click" Visible="false" />

    <asp:Panel ID="OrderDetailsPanel" runat="server" Visible="false" style="padding:20px;">
        <h2>Detalles del Pedido</h2>
        <p><strong>Fecha de Pedido:</strong> <asp:Label ID="lblFechaPedido" runat="server" Text=""></asp:Label></p>
        <p><strong>Número de Mesa:</strong> <asp:Label ID="lblNumeroMesa" runat="server" Text=""></asp:Label></p>
        

        <asp:DropDownList ID="ddlPlatos" runat="server"></asp:DropDownList>
        <asp:Button ID="btnAgregarPlato" runat="server" Text="Agregar" OnClick="btnAgregarPlato_Click" />

        <p><strong>Platos:</strong> <asp:Label ID="lblPlatos" runat="server" Text=""></asp:Label></p>
        <p><strong>Precio Total:</strong> <asp:Label ID="lblPrecioTotal" runat="server" Text=""></asp:Label></p>
        <asp:Button ID="btnCerrarMesa" runat="server" Text="Cerrar Mesa" OnClick="btnCerrarMesa_Click" />
    </asp:Panel>

    <style>
        .table-button {
            width: 100px;
            height: 100px;
            margin: 10px;
            font-size: 24px;
            text-align: center;
        }
        .green {
            background-color: aquamarine; /* Mesa libre */
        }
        .red {
            background-color: indianred; /* Mesa ocupada */
        }
    </style>
</asp:Content>
