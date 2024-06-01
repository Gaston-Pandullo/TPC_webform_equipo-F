<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="TPC_webforms_equipo_F.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>--Menú--</h1>
    <div class="row">
        <asp:Repeater ID="rptPlatos" runat="server">
            <ItemTemplate>
                <div class="col-12 mb-4">
                    <div class="card" style="width: 100%; margin: auto;">
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("nombre") %></h5>
                            <h5 class="card-text">$<%# Eval("precio") %></h5>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>


</asp:Content>
