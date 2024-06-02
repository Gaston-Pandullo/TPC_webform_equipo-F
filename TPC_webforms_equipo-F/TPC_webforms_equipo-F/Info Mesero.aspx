<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Info Mesero.aspx.cs" Inherits="TPC_webforms_equipo_F.InfoMesero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>--Información de Meseros--</h1>
    <div class="row">
        <div class="Meseros" style="margin-top:15px;">
            <label style="font-weight: bold;"> Mesero: </label>
            <asp:DropDownList ID="ddlMeseros" runat="server" AutoPostBack="true">
                <asp:ListItem Text="Leonardo Tamashiro" Value="" />              
            </asp:DropDownList>
            <div id="meseroInfo" style="border: 1px solid gray; margin-top: 20px; border-radius: 8px; padding: 13px; background-color: #cbbfbf; width: 600px; margin-left: 62px;">
                <label style="font-weight: bold;padding:12px;">Nombre y Apellido:</label>
                <span id="lblNombreApellido">Leonardo Tamashiro</span><br />

                <label style="font-weight: bold;padding:12px;">Mesas asignadas:</label>
                <span id="lblMesasAsignadas">2</span><br />

                <label style="font-weight: bold;padding:12px;">Cantidad de Pedidos Actuales:</label>
                <span id="lblCantPedidosActuales">8</span><br />

                <label style="font-weight: bold;padding:12px;">Total de Pedidos:</label>
                <span id="lblTotalPedidos">20</span><br />
            </div>
        </div>
    </div>
</asp:Content>