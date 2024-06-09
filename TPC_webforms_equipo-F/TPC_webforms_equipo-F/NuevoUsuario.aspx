<%@ Page Title="Nuevo Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoUsuario.aspx.cs" Inherits="TPC_webforms_equipo_F.NuevoUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Nuevo Usuario</h1>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />

    <div class="form-group">
        <label for="txtUsername">Nombre de Usuario</label>
        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" required="required"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="txtPassword">Contraseña</label>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" required="required" TextMode="Password"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="txtName">Nombre</label>
        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="txtLastname">Apellido</label>
        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="chkAdmin">Tipo de Usuario</label>
        <asp:DropDownList ID="ddlAdmin" runat="server" CssClass="form-control">
            <asp:ListItem Text="Mesero" Value="0"></asp:ListItem>
            <asp:ListItem Text="Administrador" Value="1"></asp:ListItem>
</asp:DropDownList>
    </div>
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />

</asp:Content>
