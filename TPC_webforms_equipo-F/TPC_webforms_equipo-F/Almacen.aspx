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
     <h1>Aca se administrará el stock de insumos</h1>
     <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" OnRowCommand="gvProductos_RowCommand">
        <Columns>
            <%--<asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />--%>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Stock" HeaderText="Stock" />
            <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
            <asp:ButtonField ButtonType="Button" CommandName="Incrementar" Text="+" />
            <asp:ButtonField ButtonType="Button" CommandName="Decrementar" Text="-" />
        </Columns>
    </asp:GridView>

     <div class="row mt-3">
        <asp:Button ID="btnAgregarItem" runat="server" Text="Agregar mercaderia" CssClass="btn btn-primary" OnClientClick="openModal(); return false;" />
    </div>

    <div class="modal fade" id="agregarItemModal" tabindex="-1" aria-labelledby="agregarItemModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="agregarItemModalLabel">¿Qué desea agregar?</h5>
                    
                    <asp:Button ID="btnCerrarVentana" CssClass="btn-close" runat="server" type="button" data-bs-dismiss="modal"/>
                </div>
                <div class="modal-body">
                    <div class="btn-container">
                        
                        <asp:Button ID="btnPlato" type="button" CssClass="btn btn-primary" runat="server" Text="Plato" PostBackUrl="~/AgregarPlato.aspx" />
                        
                        <asp:Button ID="btnBebida" type="button"  CssClass="btn btn-primary" runat="server" Text="Bebida" />

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
