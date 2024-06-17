<%@ Page Title="Administracion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="TPC_webforms_equipo_F.Administrador" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>--Lista de Usuarios--</h1>
    <div class="row mb-2">
        <asp:Button ID="btnNuevoUsuario" runat="server" Text="Nuevo Usuario" CssClass="btn btn-primary" OnClick="btnNuevoUsuario_Click" />
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
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-warning mr-2" OnClick="btnModificar_Click" />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandArgument='<%# Eval("id") %>' OnClick="btnEliminar_Click" CssClass="btn btn-danger" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <!-- Campo oculto para almacenar el ID del usuario a eliminar -->
    <asp:HiddenField ID="hfUserIdToDelete" runat="server" />

    <!-- Modal de confirmación -->
    <div class="modal fade" id="confirmModal" tabindex="-1" role="dialog" aria-labelledby="confirmModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="confirmModalLabel">Confirmación de eliminación de usuario</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    El usuario se eliminará de forma permanente. ¿Está seguro de que desea eliminarlo?
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCancelarModal" runat="server" Text="Cancelar" CssClass="btn btn-secondary" data-bs-dismiss="modal" />
                    <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                </div>
            </div>
        </div>
    </div>

    <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3" Visible="false"></asp:Label>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/5.0.0-alpha2/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function showConfirmModal() {
            $('#confirmModal').modal('show');
        }
    </script>
</asp:Content>
