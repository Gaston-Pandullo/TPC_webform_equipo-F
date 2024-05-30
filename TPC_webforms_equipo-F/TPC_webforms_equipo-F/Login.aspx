<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPC_webforms_equipo_F.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row">
            <div class="col"></div>
            <div class="col">
                <div class="mb-3">
                    <asp:Label ID="lblEmail" CssClass="form-label" runat="server" Text="Correo electrónico:"></asp:Label>
                    <asp:TextBox ID="txtEmailUsuario" CssClass="form-control" type="email" runat="server" placeholder="pepito@gmail.com"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblContrasenia" CssClass="form-label" runat="server" Text="Contraseña:"></asp:Label>
                    <asp:TextBox ID="txtContrasenia" type="password" aria-describedby="passwordHelpBlock" CssClass="form-control" runat="server"></asp:TextBox>
                    <div id="requisitosContrasenia" class="form-text">
                        Sin restricciones por el momento...
                    </div>
                </div>
                <asp:Button ID="btnAceptar" CssClass="btn btn-primary" runat="server" Text="Aceptar" />
            </div>
            <div class="col"></div>
        </div>
</asp:Content>
