<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminarProducto.aspx.cs" Inherits="TPC_webforms_equipo_F.Vistas_ABM_Productos.EliminarProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Formulario para eliminar un producto del menú</h1>

    <div class="container">
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <asp:Panel ID="pnlModificarProducto" runat="server" CssClass="form-group">

                    <%-- Tipo de producto --%>
                    <asp:Label ID="lblTipoProducto" runat="server" Text="Tipo de Producto:"></asp:Label>
                    <asp:DropDownList ID="ddlTipoProducto" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoProducto_SelectedIndexChanged">
                    </asp:DropDownList>

                    <%-- Producto --%>
                    <asp:Label ID="lblProducto" runat="server" Text="Producto:" AssociatedControlID="ddlProducto" CssClass="form-label mt-3"></asp:Label>
                    <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
                    </asp:DropDownList>

                    <%-- Nombre del producto --%>
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:" AssociatedControlID="txtNombre" CssClass="form-label mt-3"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                    <%-- Descripción del producto --%>
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" AssociatedControlID="txtDescripcion" CssClass="form-label mt-3"></asp:Label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                    <%-- Precio del producto --%>
                    <asp:Label ID="lblPrecio" runat="server" Text="Precio:" AssociatedControlID="txtPrecio" CssClass="form-label mt-3"></asp:Label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                    <%-- Stock del producto --%>
                    <asp:Label ID="lblStock" runat="server" Text="Stock:" AssociatedControlID="txtStock" CssClass="form-label mt-3"></asp:Label>
                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                    <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3" Visible="false"></asp:Label>

                    <%-- Botones de aceptar y cancelar --%>
                    <div class="mt-4">
                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <%-- Modal de confirmación --%>
    <div class="modal fade" id="confirmModal" tabindex="-1" role="dialog" aria-labelledby="confirmModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="confirmModalLabel">Confirmación de elimnacion de producto</h5>
                    <asp:Button ID="btnCerrarVentana" CssClass="btn-close" runat="server" type="button" data-bs-dismiss="modal"/>
                </div>
                <div class="modal-body">
                    El producto se eliminará de forma permanente ¿Está seguro que desea borrarlo?
            
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCancelarModal" runat="server" Text="Cancelar" CssClass="btn btn-secondary" data-dismiss="modal"/>
                    <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminar_Click" />
                </div>
            </div>
        </div>
     </div>

<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%= btnAceptar.ClientID %>').click(function () {
            $('#confirmModal').modal('show');
        });

        $('#confirmModal .btn-secondary').click(function () {
            $('#confirmModal').modal('hide');
        });
    });
</script>
</asp:Content>
