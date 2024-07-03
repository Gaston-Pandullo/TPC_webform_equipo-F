<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarPlato.aspx.cs" Inherits="TPC_webforms_equipo_F.AgregarPlato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Formulario para agregar un producto al menú</h1>

    <div class="container">
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <asp:Panel ID="pnlAgregarPlato" runat="server" CssClass="form-group">

                    <%-- Tipo de producto --%>
                    <asp:Label ID="lblTipoProducto" runat="server" Text="Tipo:"></asp:Label>
                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select">
                        <asp:ListItem Value="C">Plato (C)</asp:ListItem>
                        <asp:ListItem Value="B">Bebida (B)</asp:ListItem>
                        <asp:ListItem Value="P">Postre (P)</asp:ListItem>
                    </asp:DropDownList>

                    <p></p>
                    <%-- Nombre del producto --%>
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:" AssociatedControlID="txtNombre" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="lblErrorNombre" runat="server" CssClass="text-danger mt-3" Visible="false"></asp:Label>
                    <p></p>

                    <%-- Descripción del producto --%>
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" AssociatedControlID="txtDescripcion" CssClass="form-label mt-3"></asp:Label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>

                    <%-- Precio del producto --%>
                    <asp:Label ID="lblPrecio" runat="server" Text="Precio:" AssociatedControlID="txtPrecio" CssClass="form-label mt-3"></asp:Label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="lblErrorPrecio" runat="server" CssClass="text-danger mt-3" Visible="false"></asp:Label>
                    <p></p>

                    <%-- Stock del producto --%>
                    <asp:Label ID="lblStock" runat="server" Text="Stock:" AssociatedControlID="txtStock" CssClass="form-label mt-3"></asp:Label>
                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="lblErrorStock" runat="server" CssClass="text-danger mt-3" Visible="false"></asp:Label>

                    <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-3" Visible="false"></asp:Label>
                    <p></p>

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
