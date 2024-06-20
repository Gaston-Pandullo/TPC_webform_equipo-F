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
        <div class="modal fade" id="confirmModal" tabindex="-1" role="dialog" aria-labelledby="confirmModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="confirmModalLabel">Confirmación de asignación de meseros</h5>
                        <button type="button" class="close cerrar" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        Meseros asignados correctamente!
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-success cerrar" data-dismiss="modal" aria-label="Close">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= btnGuardar.ClientID %>').click(function () {
                $('#confirmModal').modal('show');
            });

            $(document).on('click', '.cerrar', function () {
                console.log("HELLO");
                $('#confirmModal').modal('hide');
            });
        });
    </script>
</asp:Content>
