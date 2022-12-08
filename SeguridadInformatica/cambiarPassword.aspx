<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="cambiarPassword.aspx.cs" Inherits="SeguridadInformatica.cambiarPassword" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container-fluid" style="margin-top: 20px">
                <div class="card">
                    <div class="card card-header" style="background-color: #2baccf">
                        <h5 style="color: #fff">Administración de usuarios</h5>
                    </div>

                    <div class="card-body">

                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblPassNueva" runat="server" Text="Contraseña Nueva:"></asp:Label>
                                <div class="form-group">
                                    <div class="input-group custom-search-form">
                                        <asp:TextBox ID="txtPassNueva" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <span class="input-group-append">
                                            <asp:LinkButton ID="lnkMostrarPassword" runat="server" class="btn btn-block btn-primary" OnClick="lnkMostrarPassword_Click" UseSubmitBehavior="false"><i runat="server" id="ipassnuevo" class="fas fa-eye-slash"></i></asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 10px;">
                            <div class="col-md-4">
                                <asp:Label ID="lblPassConfirm" runat="server" Text="Confirmación de Contraseña:"></asp:Label>
                                <div class="form-group">
                                    <div class="input-group custom-search-form">
                                        <asp:TextBox ID="txtConfirmPass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <span class="input-group-append">
                                            <asp:LinkButton ID="lnkMostrarRepetirPassword" runat="server" class="btn btn-block btn-primary" OnClick="lnkMostrarRepetirPassword_Click" UseSubmitBehavior="false"><i runat="server" id="ipassconfirm" class="fas fa-eye-slash"></i></asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 10px;">
                            <div class="col-md-2">
                                <asp:LinkButton ID="lnkVolver" runat="server" CssClass="w-100 btn" BackColor="#6699CC" ForeColor="White" Text="" Width="100%" UseSubmitBehavior="false" OnClick="LnkVolver_Click">Volver</asp:LinkButton>
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton ID="lnkCambiarPassword" runat="server" CssClass="w-100 btn" BackColor="#669999" ForeColor="White" UseSubmitBehavior="false" OnClientClick="confirmar('','Seguro desea guardar el registro.','lnkCambiarPassword',true);return false;" OnClick="LnkCambiarPassword_Click">Cambiar Contraseña</asp:LinkButton>
                            </div>
                        </div>
                    </div>
        </ContentTemplate>


    </asp:UpdatePanel>
</asp:Content>
