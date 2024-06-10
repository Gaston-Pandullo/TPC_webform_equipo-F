<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Info Mesero.aspx.cs" Inherits="TPC_webforms_equipo_F.InfoMesero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>--Información de Meseros--</h1>
    <div class="row">
        <div class="Meseros" style="margin-top:15px;">
            <label style="font-weight: bold;"> Seleccione un mesero: </label>
            <asp:DropDownList ID="ddlMeseros" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlMeseros_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>

    <div class="row" style="border: 1px solid gray; margin-top: 20px; border-radius: 8px; padding: 13px; background-color: #cbbfbf; width: 600px; margin-left: 62px;">
        <div>
            <label style="font-weight: bold;padding:12px;">Nombre:</label>
            <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <label style="font-weight: bold;padding:12px;">Apellido:</label>
            <asp:Label ID="lblApellido" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <label style="font-weight: bold;padding:12px;">Cantidad de Mesas Asignadas:</label>
            <asp:Label ID="lblCantidaddeMesas" runat="server" Text=""></asp:Label>
        </div>
       
    </div>
</asp:Content>