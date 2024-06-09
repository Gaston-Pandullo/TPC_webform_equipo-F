<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsignacionMeseros.aspx.cs" Inherits="TPC_webforms_equipo_F.AsignacionMeseros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="gvMesas" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvMesas_RowDataBound">
        <Columns>
            <asp:BoundField DataField="id_mesa" HeaderText="Mesa" />
            <asp:TemplateField HeaderText="Mesero Asignado">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlMeseros" runat="server">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
</asp:Content>
