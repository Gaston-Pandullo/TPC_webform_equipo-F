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
    <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" OnRowCommand="gvProductos_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
            <asp:BoundField DataField="Stock" HeaderText="Stock" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
            <asp:BoundField DataField="Categoria" HeaderText="Categoria" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
            <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Button ID="btnIncrementar" runat="server" CommandName="Incrementar" CommandArgument='<%# Eval("Id") %>' Text="+" CssClass="btn btn-success btn-sm" />
                    <asp:Button ID="btnDecrementar" runat="server" CommandName="Decrementar" CommandArgument='<%# Eval("Id") %>' Text="-" CssClass="btn btn-danger btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>
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
