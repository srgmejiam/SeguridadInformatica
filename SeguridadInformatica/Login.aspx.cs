using BL;
using EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static EL.Enum;

namespace SeguridadInformatica
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Metodos y Funciones
        private void Mensaje(string Message, int tipoMensaje, string Encabezado = "", bool Html = false, bool Fondo = false, bool returnLogin = false, string UrlReturn = "", bool CerrarClick = true)
        {
            //icon -->      success,warning, error,  info
            //btnColor -->  #32A525,#E38618,#F27474,#3FC3EE

            //Parametros que recibe el metodo
            //function Mensaje(title, mensaje, icon = 'success', btnConfirmText = 'Aceptar', btnConfirmColor = '#32A525', html = false, fondo = false, ReturnLogin = false, UrlReturn)

            switch (tipoMensaje)
            {
                case (int)eMessage.Exito:
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SweetAlert Exito", "Mensaje('" + Encabezado + "', '" + Message + "','success','Aceptar','#32A525'," + Html.ToString().ToLower() + "," + Fondo.ToString().ToLower() + "," + returnLogin.ToString().ToLower() + ",'" + UrlReturn.ToString().ToLower() + "'," + CerrarClick.ToString().ToLower() + ");", true);
                    break;
                case (int)eMessage.Alerta:
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SweetAlert Alerta", "Mensaje('" + Encabezado + "', '" + Message + "','warning','Aceptar','#E38618'," + Html.ToString().ToLower() + "," + Fondo.ToString().ToLower() + "," + returnLogin.ToString().ToLower() + ",'" + UrlReturn.ToString().ToLower() + "'," + CerrarClick.ToString().ToLower() + ");", true);
                    break;
                case (int)eMessage.Error:
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SweetAlert Error", "Mensaje('" + Encabezado + "', '" + Message + "','error','Aceptar','#F27474'," + Html.ToString().ToLower() + "," + Fondo.ToString().ToLower() + "," + returnLogin.ToString().ToLower() + ",'" + UrlReturn.ToString().ToLower() + "'," + CerrarClick.ToString().ToLower() + ");", true);
                    break;
                case (int)eMessage.Info:
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SweetAlert Info", "Mensaje('" + Encabezado + "', '" + Message + "','info','Aceptar','#3FC3EE'," + Html.ToString().ToLower() + "," + Fondo.ToString().ToLower() + "," + returnLogin.ToString().ToLower() + ",'" + UrlReturn.ToString().ToLower() + "'," + CerrarClick.ToString().ToLower() + ");", true);
                    break;
            }
        }
        private string Justify(string msj)
        {
            string Html = "<div style = text-align:justify> " + msj + " </div>";
            return Html;
        }
        private bool ValidarAcceso()
        {
            try
            {
                string Login = txtUsuario.Text;
                string PasswordString = txtPassword.Text;
                byte[] PasswordByte = BL_Usuario.Encrypt(PasswordString);
                Usuario User = new Usuario();


                if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    Mensaje(Justify("Estimado usuario, el campo nombre de usuario es obligatorio, por favor ingrese su usuario"), (int)eMessage.Alerta, "", true);
                    return false;
                }

                if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    Mensaje(Justify("Estimado usuario, la contraseña es un campo obligatorio, por favor ingrese su contraseña"), (int)eMessage.Alerta, "", true);
                    return false;
                }

                if (!BL_Usuario.ExiteLogin(Login))
                {
                    Mensaje(Justify("Las credenciales proporcionadas no son válidas.Por favor verifique."), (int)eMessage.Alerta, "", true);
                    return false;
                }

                if (BL_Usuario.CuentaBloqueada(Login))
                {
                    Mensaje(Justify("La Cuenta ha sido bloqueada por múltiples<b> intentos fallidos </ b > de inicio de sesión.Comuniquese con el administrador del sistema."), (int)eMessage.Error, "Cuenta Bloqueada", true);
                    return false;
                }

                if (!BL_Usuario.ValidarCredenciales(Login, PasswordByte))
                {

                    User = BL_Usuario.ObtenerUsuario_x_Login(Login);

                    if (!(User == null))
                    {
                        int IntentosFallidos = User.Intentos;
                        int MaximoIntentosFallidos = Convert.ToInt32(BL_Parametros.BuscarParametro((short)eParametro.IntentosFallidos).Parametro);

                        if (IntentosFallidos + 1 >= MaximoIntentosFallidos)
                        {
                            BL_Usuario.SumarIntentoFallido(User.IdUsuario);
                            BL_Usuario.BloquearCuentaUsuario(User.IdUsuario, true);
                            Mensaje(Justify("La Cuenta ha sido bloqueada por múltiples <b>intentos fallidos</b> de inicio de sesión. Comuniquese con el administrador del sistema."), (int)eMessage.Alerta, "", true);
                            return false;
                        }
                        BL_Usuario.SumarIntentoFallido(User.IdUsuario);
                        Mensaje(Justify("Las credenciales proporcionadas no son válidas, si supera el límite de intentos fallidos permitidos (" + MaximoIntentosFallidos.ToString() + ") la <b>cuenta será bloqueada</b>."), (int)eMessage.Alerta, "", true);
                        return false;
                    }
                    Mensaje(Justify("Las credenciales proporcionadas no son válidas.Por favor verifique."), (int)eMessage.Alerta, "", true);
                    return false;
                }

                User = BL_Usuario.ObtenerUsuario_x_Login(Login);

                if (User == null)
                {
                    Mensaje(Justify("No se cargaron correctamente los datos de sesión del usuario."), (int)eMessage.Error, "", true);
                    return false;
                }

                vUsuario vUsuario = new vUsuario();
                vUsuario = BL_Usuario.vUsuario(User.IdUsuario);

                Session["IdUsuarioSesion"] = vUsuario.IdUsuario;
                Session["LoginSesion"] = vUsuario.Login;
                Session["NombreCompleto"] = vUsuario.Nombre;
                Session["Cargo"] = vUsuario.Cargo;
                Session["IdRolSesion"] = vUsuario.IdRol;
                Session["Rol"] = vUsuario.Rol;
                Session["UISesion"] = vUsuario.Login;

                Session["FormularioSesion"] = (int)eFormularios.Principal;

                List<RolFormulario> rolFormularios = new List<RolFormulario>();
                rolFormularios = BL_RolFormulario.Formularios_x_Rol(vUsuario.IdRol);

                if (!(rolFormularios.Count > 0))
                {
                    Mensaje(Justify("Estimado usuario usted no tiene los permisos necesarios para acceder al recurso solicitado, por favor comunicarse con el administrador del sistema."), (int)eMessage.Error, "", true);
                    return false;
                }
                Session["FormulariosRolSesion"] = rolFormularios;

                int IntentosFallidosLogin = User.Intentos;
                if (IntentosFallidosLogin > 0)
                {
                    BL_Usuario.RestablecerIntentosFallido(User.IdUsuario);
                }

                return true;
            }
            catch (Exception Error)
            {
                Mensaje(Justify(Error.Message.Replace("'", "")), (int)eMessage.Error, "", true);
                return false;
            }
        }
        #endregion

        #region Eventos de los Controles
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidarAcceso())
            {
                return;
            }
            Response.Redirect("~/Principal.aspx");
        }
        #endregion
    }
}