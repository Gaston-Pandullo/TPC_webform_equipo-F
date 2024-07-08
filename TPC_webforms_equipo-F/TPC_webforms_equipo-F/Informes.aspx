<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Informes.aspx.cs" Inherits="TPC_webforms_equipo_F.Informes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Informes</h1>
    <div class="container text-center">
        <div class="row">
            <%-- Informacion de meseros --%>
            <div class="col">
                <strong>Meseros</strong>
                <asp:Repeater ID="rptMeseros" runat="server">
                    <HeaderTemplate>
                        <div class="table-responsive">
                            <table class="table table-striped table-bordered">
                                <thead class="thead-dark">
                                    <tr>
                                        <th class="text-center">Nombre</th>
                                        <th class="text-center">Pedidos atendidos</th>
                                        <th class="text-center">Total facturado</th>
                                    </tr>
                                </thead>
                                <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                                    <tr>
                                        <td class='text-center'><%# Eval("Nombre") %></td>
                                        <td class='text-center'><%# Eval("PedidosAtendidos") %></td>
                                        <td class='text-center'><%# Eval("TotalFacturado", "{0:C}") %></td>
                                    </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                                </tbody>
                            </table>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <%-- Informacion de items pedidos --%>
    <div class="col">
        <strong>Productos pedidos</strong>
        <asp:Repeater ID="rptProductos" runat="server">
            <HeaderTemplate>
                <div class="table-responsive">
                    <table class="table table-striped table-bordered">
                        <thead class="thead-dark">
                            <tr>
                                <th class="text-center">Nombre</th>
                                <th class="text-center">Cantidades vendidas</th>
                            </tr>
                        </thead>
                        <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class='text-center'><%# Eval("Nombre") %></td>
                    <td class='text-center'><%# Eval("CantidadesVendidas") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
            </table>
        </div>
   
            </FooterTemplate>
        </asp:Repeater>

    </div>
    </div>
        <%-- Facturacion del dia --%>
        <div class="row">
            <div class="col">
                <strong>Facturación del día</strong>
                <asp:Label ID="lblFacturacionDia" runat="server" CssClass="form-control"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
