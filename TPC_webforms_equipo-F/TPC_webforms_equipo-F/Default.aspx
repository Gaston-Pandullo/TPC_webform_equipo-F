﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_webforms_equipo_F._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="TableOrder" runat="server">
        <h1>Mesas</h1>
        <p>Seleccione una mesa y agregue los platos que quiera!</p>
        <main id="mainTables" runat="server">
        </main>
    </asp:Panel>

    <asp:Button ID="btnAbrirMesa" runat="server" Text="Abrir Mesa" OnClick="btnAbrirMesa_Click" Visible="false" />

    <asp:Panel ID="OrderDetailsPanel" runat="server" Visible="false" style="padding:20px; margin-top: 20px;">
        <h2>Detalles del Pedido</h2>
        <p><strong>Fecha de Pedido:</strong> <asp:Label ID="lblFechaPedido" runat="server" Text=""></asp:Label></p>
        <p><strong>Número de Mesa:</strong> <asp:Label ID="lblNumeroMesa" runat="server" Text=""></asp:Label></p>

        <asp:Button ID="btnAgregarPlato" runat="server" Text="Agregar items a esta mesa" CssClass="btn btn-primary btn-lg my-2" OnClick="btnAgregarPlato_Click" />

        <p>
            <strong>Platos:</strong>
            <asp:Repeater ID="rptPlatosPedidos" runat="server">
    <HeaderTemplate>
        <div class="table-responsive">
            <table class="table table-striped table-bordered">
                <thead class="thead-dark">
                    <tr>
                        <th class="text-center">Nombre</th>
                        <th class="text-center">Cantidad</th>
                        <th class="text-center">Precio Unitario</th>
                    </tr>
                </thead>
                <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td class='text-center'><%# Eval("Nombre") %></td>
            <td class='text-center'><%# Eval("Cantidad") %></td>
            <td class='text-center'><%# Eval("precio_unitario", "{0:C}") %></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </tbody>
        </table>
        </div>
    </FooterTemplate>
</asp:Repeater>
            
        </p>
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
