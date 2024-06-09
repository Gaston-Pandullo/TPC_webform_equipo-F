<%@ Page Title="Administracion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="TPC_webforms_equipo_F.Administrador" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>--Lista de Usuarios--</h1>
    <div class="row mb-2">
        <asp:Button ID="btnNuevoUsuario" runat="server" Text="Nuevo Usuario" CssClass="btn btn-primary" />
    </div>
    <div class="row">
        <asp:Repeater ID="rptUsuarios" runat="server">
            <ItemTemplate>
                <div class="col-12 mb-4">
                    <div class="card" style="width: 100%; margin: auto;">
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center">
                                <h5 class="card-subtitle mb-2 text-muted">Nombre: <%# Eval("Name") %> <%# Eval("Lastname") %></h5>                
                            </div>
                            <p class="card-text">Nombre de Usuario: <%# Eval("User") %></p>
                            <p class="card-text">ID: <%# Eval("id") %></p>
                            <p class="card-text">Tipo de Usuario: <%# Eval("TipoUsuario") %></p>
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-warning mr-2" />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandArgument='<%# Eval("id") %>' OnClick="btnEliminar_Click" CommandName="Delete" CssClass="btn btn-danger" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
