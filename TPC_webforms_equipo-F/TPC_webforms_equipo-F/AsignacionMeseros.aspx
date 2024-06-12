<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsignacionMeseros.aspx.cs" Inherits="TPC_webforms_equipo_F.AsignacionMeseros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <contenttemplate>
            <asp:Literal ID="ltAlert" runat="server"></asp:Literal>
            <asp:GridView ID="gvMesas" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvMesas_RowDataBound" CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="id_mesa" HeaderText="Mesa" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                    <asp:TemplateField HeaderText="Mesero Asignado" HeaderStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlMeseros" runat="server" CssClass="form-select mx-auto">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div class="text-end">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            </div>
        </contenttemplate>
    </div>
</asp:Content>
