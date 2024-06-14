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
                <td class='text-center'><%# Eval("Stock") %></td>
                <td class='text-center'><%# Eval("Categoria") %></td>
                <td class='text-center'>
                    <asp:Button ID="Button1" CssClass="btn btn-success btn-sm" runat="server" Text="+" CommandName="Incrementar" CommandArgument='<%# Eval("Id") + "," + Eval("Categoria") + "," + Eval("Stock") %>' />
                    <asp:Button ID="Button2" CssClass='btn btn-danger btn-sm' runat="server" Text="-" CommandName="Decrementar" CommandArgument='<%# Eval("Id") + "," + Eval("Categoria") + "," + Eval("Stock") %>' />
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

    <script type="text/javascript">
        function openModal() {
            var myModal = new bootstrap.Modal(document.getElementById('agregarItemModal'), {
                keyboard: false
            });
            myModal.show();
        }
    </script>

</asp:Content>
