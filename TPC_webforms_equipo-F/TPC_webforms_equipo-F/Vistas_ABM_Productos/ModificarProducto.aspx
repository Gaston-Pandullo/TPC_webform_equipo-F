<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarProducto.aspx.cs" Inherits="TPC_webforms_equipo_F.Vistas_ABM_Productos.ModificarProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

 <h1>Formulario para modificar un producto del menú</h1>

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
                 <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>

                 <%-- Descripción del producto --%>
                 <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" AssociatedControlID="txtDescripcion" CssClass="form-label mt-3"></asp:Label>
                 <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>

                 <%-- Precio del producto --%>
                 <asp:Label ID="lblPrecio" runat="server" Text="Precio:" AssociatedControlID="txtPrecio" CssClass="form-label mt-3"></asp:Label>
                 <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>

                 <%-- Stock del producto --%>
                 <asp:Label ID="lblStock" runat="server" Text="Stock:" AssociatedControlID="txtStock" CssClass="form-label mt-3"></asp:Label>
                 <asp:TextBox ID="txtStock" runat="server" CssClass="form-control"></asp:TextBox>

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

</asp:Content>
