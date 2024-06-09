<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="TPC_webforms_equipo_F.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>--Menú--</h1>
    <%-- Lista platos --%>
    <div class="row">
        <h2>Platos</h2>
        <hr />
        <asp:Repeater ID="rptPlatos" runat="server">
            <ItemTemplate>
                <div class="col-12 mb-4">
                    <div class="card" style="width: 100%; margin: auto;">
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center">
                                <h3 class="card-title"><%# Eval("nombre") %></h3>
                                <h5 class="card-subtitle mb-2 text-muted">$<%# Eval("precio") %></h5>
                            </div>
                            <p class="card-text"><%# Eval("descripcion") %></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
     <%-- Lista bebidas --%>
    <div class="row">
    <h2>Bebidas</h2>
    <hr />
    <asp:Repeater ID="rptBebidas" runat="server">
        <ItemTemplate>
            <div class="col-12 mb-4">
                <div class="card" style="width: 100%; margin: auto;">
                    <div class="card-body">
                        <div class="d-flex justify-content-between align-items-center">
                            <h3 class="card-title"><%# Eval("nombre") %></h3>
                            <h5 class="card-subtitle mb-2 text-muted">$<%# Eval("precio") %></h5>
                        </div>
                        <p class="card-text"><%# Eval("descripcion") %></p>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    </div>


</asp:Content>
