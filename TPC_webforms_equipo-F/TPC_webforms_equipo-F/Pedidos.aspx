<%@ Page Title="Pedidos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="TPC_webforms_equipo_F.Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administración de Pedidos</h1>
    <div id="row">
        <div id="pedidoMesa" style="border: 1px solid gray; margin-top: 20px; border-radius: 8px; padding: 13px; background-color: #cbbfbf; width: 600px; margin-left: 62px;">
            <label style="font-weight: bold;padding:12px;font-size:large">Mesa Numero:</label>
            <span id="lblNumMesa" style="font-size:large; font-weight:bold;">1</span><br />

            <label style="font-weight: bold;padding:12px;">Mesero:</label>
            <span id="lblNombreMesero">Diego Perez</span><br />

            <label style="font-weight: bold;padding:12px">Platos:</label>
            <div id="listaPedidos">
                <ul style="margin-left:49px">
                    <li>Spaghetti Carbonara</li>
                    <li>Ensalada César</li>
                </ul>
            </div>


            <label style="font-weight: bold;padding:12px;">Fecha de Pedido:</label>
            <span id="lblFechaPedido">24-05-2024</span><br />

            <label style="font-weight: bold;padding:12px;">Precio:</label>
            <span id="lblPrecioTotal">$ 2000</span><br />
        </div>
    </div>
</asp:Content>

