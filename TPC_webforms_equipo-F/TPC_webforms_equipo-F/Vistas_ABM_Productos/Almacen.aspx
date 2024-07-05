<%@ Page Title="Almacén" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Almacen.aspx.cs" Inherits="TPC_webforms_equipo_F.Almacen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Agregar acá los estilos CSS */
        #gvProductos {
            font-family: Arial, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

        #gvProductos td, #gvProductos th {
            border: 1px solid #dddddd;
            padding: 8px;
        }

        #gvProductos tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        #gvProductos th {
            background-color: #4CAF50;
            color: white;
        }

        .modal-body .btn-container {
            display: flex;
            justify-content: center;
            gap: 10px;
        }
    </style>
    <h1>STOCK</h1>
    <div class="container text-center">
        <div class="row">
            <div class="col">
                <div class="form-floating">
                    <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="form-control" placeholder="" OnTextChanged="txtFiltroNombre_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <label for="txtFiltroNombre">Filtro por nombre</label>
                </div>
            </div>
            <div class="col">
                 <div class="form-floating">
                    <asp:DropDownList ID="ddlFiltroCategoria" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroCategoria_SelectedIndexChanged">
                        <asp:ListItem Text="Seleccione una categoría" Value="x" />
                        <asp:ListItem Text="Plato" Value="C" />
                        <asp:ListItem Text="Bebida" Value="B" />
                        <asp:ListItem Text="Postre" Value="P" />
                    </asp:DropDownList>
                    <label for="ddlFiltroCategoria">Filtro por categoría:</label>
                </div>
            </div>
            <div class="col">
                
            </div>
        </div>
    </div>
    <hr />
    <asp:Repeater ID="gvProductos" runat="server" OnItemCommand="gvProductos_ItemCommand">
        <HeaderTemplate>
            <div class="table-responsive">
                <table class="table table-striped table-bordered">
                    <thead class="thead-dark">
                        <tr>
                            <th class="text-center">ID</th>
                            <th class="text-center">Nombre</th>
                            <th class="text-center">Descripcion</th>
                            <th class="text-center">Precio</th>
                            <th class="text-center">Stock</th>
                            <th class="text-center">Categoria</th>
                            <th class="text-center">Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class='text-center'><%# Eval("Id") %></td>
                <td class='text-center'><%# Eval("Nombre") %></td>
                <td class='text-center'><%# Eval("Descripcion") %></td>
                <td class='text-center'><%# Eval("Precio", "{0:C}") %></td>
                <td class='text-center'>
                    <asp:TextBox ID="txtStock" runat="server" Text='<%# Eval("Stock") %>' CssClass="form-control text-center" />
                </td>
                <td class='text-center'><%# Eval("Categoria") %></td>
                <td class='text-center'>
                    <asp:Button ID="btnGuardar" CssClass="btn btn-primary btn-sm" runat="server" Text="Guardar" CommandName="Guardar" CommandArgument='<%# Eval("Id") + "," + Eval("Categoria") + "," + Eval("Stock") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
                </table>
            </div>
        </FooterTemplate>
    </asp:Repeater>

    <div class="row mt-3">
        <asp:Button ID="btnAgregarItem" runat="server" Text="Administrar productos" CssClass="btn btn-primary" OnClientClick="openModal(); return false;" />
    </div>

    <div class="modal fade" id="agregarItemModal" tabindex="-1" aria-labelledby="agregarItemModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="agregarItemModalLabel">¿Qué desea hacer?</h5>
                    <asp:Button ID="btnCerrarVentana" CssClass="btn-close" runat="server" type="button" data-bs-dismiss="modal" />
                </div>
                <div class="modal-body">
                    <div class="btn-container">
                        <asp:Button ID="btnAgregar" type="button" CssClass="btn btn-secondary" runat="server" Text="Agregar" PostBackUrl="~/Vistas_ABM_Productos/AgregarPlato.aspx" />
                        <asp:Button ID="btnModificar" type="button" CssClass="btn btn-secondary" runat="server" Text="Modificar" PostBackUrl="~/Vistas_ABM_Productos/ModificarProducto.aspx" />
                        <asp:Button ID="btnEliminar" type="button" CssClass="btn btn-danger" runat="server" Text="Eliminar" PostBackUrl="~/Vistas_ABM_Productos/EliminarProducto.aspx" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal para notificación -->
    <div class="modal fade" id="notificationModal" tabindex="-1" aria-labelledby="notificationModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="notificationModalLabel">Notificación</h5>
                </div>
                <div class="modal-body">
                    <p>El stock ha sido actualizado correctamente.</p>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openModal() {
            var myModal = new bootstrap.Modal(document.getElementById('agregarItemModal'), {
                keyboard: false
            });
            myModal.show();
        }

        function showNotificationModal() {
            var notificationModal = new bootstrap.Modal(document.getElementById('notificationModal'), {
                keyboard: false
            });
            notificationModal.show();

            setTimeout(function () {
                notificationModal.hide();
            }, 2000);
        }
    </script>
</asp:Content>

