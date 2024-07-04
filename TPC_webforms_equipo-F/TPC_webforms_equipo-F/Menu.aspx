<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="TPC_webforms_equipo_F.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex align-items-center justify-content-between">
        <h1>Listado de productos</h1>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" CssClass="btn btn-success mt-3" />
    </div>
    <asp:Label ID="ID_PEDIDO" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- Lista Platos --%>
<div class="row">
    <h2>🍔 Platos 🍔</h2>
    <hr />
    <asp:Repeater ID="rptPlatos" runat="server" OnItemCommand="rptPlatos_ItemCommand">
        <ItemTemplate>
            <div class="col-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <div class="d-flex justify-content-between align-items-center">
                            <h3 class="card-title">
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("nombre") %>' />
                            </h3>
                            <h5 class="card-subtitle mb-2 text-muted"><%# Eval("precio", "{0:C}") %></h5>
                        </div>
                        <p class="card-text"><%# Eval("descripcion") %></p>
                        <div class="d-flex justify-content-center gap-2">
                            <asp:Button ID="restarCantidad" CssClass="btn btn-primary" runat="server" Text="-" CommandName="RestarCantidad" CommandArgument='<%# Container.ItemIndex %>' />
                            <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("cantidad") %>' ReadOnly="true" CssClass="form-control w-25" />
                            <asp:Button ID="sumarCantidad" CssClass="btn btn-primary" runat="server" Text="+" CommandName="SumarCantidad" CommandArgument='<%# Container.ItemIndex %>' />
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

<%-- Lista Bebidas --%>
<div class="row">
    <h2>🍺 Bebidas 🍺</h2>
    <hr />
    <asp:Repeater ID="rptBebidas" runat="server" OnItemCommand="rptBebidas_ItemCommand">
        <ItemTemplate>
            <div class="col-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <div class="d-flex justify-content-between align-items-center">
                            <h3 class="card-title">
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("nombre") %>' />
                            </h3>
                            <h5 class="card-subtitle mb-2 text-muted"><%# Eval("precio", "{0:C}") %></h5>
                        </div>
                        <p class="card-text"><%# Eval("descripcion") %></p>
                        <div class="d-flex justify-content-center gap-2">
                            <asp:Button ID="restarCantidad" CssClass="btn btn-primary" runat="server" Text="-" CommandName="RestarCantidad" CommandArgument='<%# Container.ItemIndex %>' />
                            <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("cantidad") %>' ReadOnly="true" CssClass="form-control w-25" />
                            <asp:Button ID="sumarCantidad" CssClass="btn btn-primary" runat="server" Text="+" CommandName="SumarCantidad" CommandArgument='<%# Container.ItemIndex %>' />
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

<%-- Lista Postres --%>
<div class="row">
    <h2>🍨 Postres 🍨</h2>
    <hr />
    <asp:Repeater ID="rptPostres" runat="server" OnItemCommand="rptPostres_ItemCommand">
        <ItemTemplate>
            <div class="col-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <div class="d-flex justify-content-between align-items-center">
                            <h3 class="card-title">
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("nombre") %>' />
                            </h3>
                            <h5 class="card-subtitle mb-2 text-muted"><%# Eval("precio", "{0:C}") %></h5>
                        </div>
                        <p class="card-text"><%# Eval("descripcion") %></p>
                        <div class="d-flex justify-content-center gap-2">
                            <asp:Button ID="restarCantidad" CssClass="btn btn-primary" runat="server" Text="-" CommandName="RestarCantidad" CommandArgument='<%# Container.ItemIndex %>' />
                            <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("cantidad") %>' ReadOnly="true" CssClass="form-control w-25" />
                            <asp:Button ID="sumarCantidad" CssClass="btn btn-primary" runat="server" Text="+" CommandName="SumarCantidad" CommandArgument='<%# Container.ItemIndex %>' />
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

