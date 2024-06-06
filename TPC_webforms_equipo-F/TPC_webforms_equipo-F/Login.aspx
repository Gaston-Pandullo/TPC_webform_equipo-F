<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPC_webforms_equipo_F.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row">
            <div class="col"></div>
            <div class="col">
                <div class="mb-3">
                    <asp:Label ID="lblUsername" CssClass="form-label" runat="server" Text="Usuario:"></asp:Label>
                    <asp:TextBox ID="txtUsername" CssClass="form-control" type="text" runat="server" placeholder="usuario"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblContrasenia" CssClass="form-label" runat="server" Text="Contraseña:"></asp:Label>
                    <asp:TextBox ID="txtContrasenia" type="password" aria-describedby="passwordHelpBlock" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Button ID="btnAceptar" CssClass="btn btn-primary" OnClick="btnLogin_Click" OnClientClick="return validarFormulario();" runat="server" Text="Aceptar" />
                </div>
                <asp:Label ID="LabelError" CssClass="block form-label text-danger" runat="server" Text=""></asp:Label>
            </div>
            <div class="col"></div>
        </div>
        <script>
            function validarFormulario() {
                var username = document.getElementById('<%= txtUsername.ClientID %>').value;
                var contrasenia = document.getElementById('<%= txtContrasenia.ClientID %>').value;
        
                if (username === "" || contrasenia === "") {
                    var errorLabel = document.getElementById('<%= LabelError.ClientID %>');
                    errorLabel.innerText = "Por favor, complete todos los campos.";
                    return false;
                }
                return true;
            }
        </script>
</asp:Content>

