<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="TPC_webforms_equipo_F.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex align-items-center justify-content-between">
        <h1>--Menú--</h1>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" CssClass="btn btn-success mt-3" />
    </div>
    <%-- Lista platos --%>
    <div class="row">
        <h2>Platos</h2>
        <hr />
        <asp:Repeater ID="rptPlatos" runat="server">
            <ItemTemplate>
                <div class="col-4 mb-4">
                    <div class="card">
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center">
                                <h3 class="card-title"><%# Eval("nombre") %></h3>
                                <h5 class="card-subtitle mb-2 text-muted"><%# Eval("precio", "{0:C}") %></h5>
                            </div>
                            <p class="card-text"><%# Eval("descripcion") %></p>
                            <div class="d-flex justify-content-center gap-2">
                                <asp:Button ID="restarCantidad" CssClass="btn btn-primary" runat="server" Text="-" OnClientClick="return updateQuantity(this, -1);" />
                                <input id="txtCantidad" value="0" type="number" readonly class="form-control w-25" />
                                <asp:Button ID="sumarCantidad" CssClass="btn btn-primary" runat="server" Text="+" OnClientClick="return updateQuantity(this, 1);" />
                            </div>
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
                <div class="col-4 mb-4">
                    <div class="card">
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center">
                                <h3 class="card-title"><%# Eval("nombre") %></h3>
                                <h5 class="card-subtitle mb-2 text-muted"><%# Eval("precio", "{0:C}") %></h5>
                            </div>
                            <p class="card-text"><%# Eval("descripcion") %></p>
                            <div class="d-flex justify-content-center gap-2">
                                <asp:Button ID="restarCantidad" CssClass="btn btn-primary" runat="server" Text="-" OnClientClick="return updateQuantity(this, -1);" />
                                <input id="txtCantidad" value="0" type="number" readonly class="form-control w-25" />
                                <asp:Button ID="sumarCantidad" CssClass="btn btn-primary" runat="server" Text="+" OnClientClick="return updateQuantity(this, 1);" />
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
        
    <script type="text/javascript">
        function updateQuantity(button, delta) {
            var container = button.parentElement;
            var input = container.querySelector("input[type='number']");
            var currentValue = parseInt(input.value);
            var newValue = currentValue + delta;
            if (newValue < 0) newValue = 0;
            input.value = newValue;
            return false;
        }
    </script>
</asp:Content>
