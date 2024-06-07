<%@ Page Title="Almacén" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Almacen.aspx.cs" Inherits="TPC_webforms_equipo_F.Almacen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Agrega aquí los estilos CSS */
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
    </style>
     <h1>Aca se administrará el stock de insumos</h1>
     <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" OnRowCommand="gvProductos_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Stock" HeaderText="Stock" />
            <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
            <asp:ButtonField ButtonType="Button" CommandName="Incrementar" Text="+" />
            <asp:ButtonField ButtonType="Button" CommandName="Decrementar" Text="-" />
        </Columns>
    </asp:GridView>  
</asp:Content>
