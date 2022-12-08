<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SeguridadInformatica.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" href="assets/Images/1995704.ico" type="image/x-icon" />
    <title>Seguridad Informática | Iniciar Sesión</title>

    <%-- Referencias CSS--%>
    <link href="assets/bootstrap-5.2.0/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/Fontawesome/css/all.min.css" rel="stylesheet" />
    <link href="assets/SweeAlert/sweetalert.min.css" rel="stylesheet" />
    <link href="assets/CSS/Login.css" rel="stylesheet" />
</head>
<body>
    <form id="formularioLogin" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="HF_Correo" runat="server" Value="0" />
                <div style="max-width: 380px; max-height: 400px; margin-left: auto; margin-right: auto; margin-top: 30px">

                    <div class="card" style="background-color: #F2F2F2; border-radius: 15px">
                        <div class="card-body">

                            <div style="margin-left: auto; margin-right: auto; text-align: center">
                                <img src="assets/Images/1995704.png"  width="250px"/>
                            </div>

                            <div class="input-group mb-3">
                                <span class="input-group-text"><i class="fas fa-user"></i></span>
                                <div class="form-floating" style="width: 300px">
                                    <asp:TextBox runat="server" onkeypress="return blockCaracteresEspeciales(event)" CssClass="form-control" ID="txtUsuario" AutoCompleteType="None" autocomplete="off" placeholder="Usuario"></asp:TextBox>
                                    <label for="floatingInput">Usuario</label>
                                </div>
                            </div>

                            <div class="input-group mb-3">
                                <span class="input-group-text"><i class="fas fa-key"></i></span>
                                <div class="form-floating" style="width: 300px">
                                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="txtPassword" AutoCompleteType="None" autocomplete="off" placeholder="Contraseña"></asp:TextBox>
                                    <label for="floatingInput">Contraseña</label>
                                </div>

                            </div>


                            <div class="row col-md-8" style="margin-left: auto; margin-right: auto">
                                <asp:Button runat="server" ID="btnLogin" CssClass="btn" Style="background-color: #2E56C7; color: #fff; font-size: 20px" Text="Iniciar Sesión" OnClick="btnLogin_Click" />
                            </div>


                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>


    <%-- Referencias JS --%>
    <script src="assets/JQuery/jquery.min.js"></script>
    <script src="assets/bootstrap-5.2.0/js/bootstrap.min.js"></script>
    <script src="assets/JavaScript/Validaciones.js"></script>
    <script src="assets/SweeAlert/sweetalert.all.min.js"></script>
    <script src="assets/SweeAlert/sweetAlertStyle.js"></script>

</body>
</html>
