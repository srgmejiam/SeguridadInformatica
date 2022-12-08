using BL;
using EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static EL.Enum;

namespace SeguridadInformatica
{
    public partial class cambiarPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Seguridad
        private object ValidarEnteros(object PosibleEntero, int TipoEntero = 32)
        {
            try
            {
                switch (TipoEntero)
                {
                    case 16:
                        return Convert.ToInt16(PosibleEntero);
                    case 32:
                        return Convert.ToInt32(PosibleEntero);
                    case 64:
                        return Convert.ToInt64(PosibleEntero);
                    default:
                        return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
        protected bool ValidarDatosSesion(int IdFormulario = 0)
        {
            string PageLogin = "Login.aspx";
            string PagePrincipal = "Principal.aspx";
            try
            {
                int IdUsuarioSesion = (int)ValidarEnteros(Session["IdUsuarioSesion"]);
                int IdRolSesion = (int)ValidarEnteros(Session["IdRolSesion"]);
                int FormularioSesion = (int)ValidarEnteros(Session["FormularioSesion"]);


                if (!(IdUsuarioSesion > 0))
                {
                    Mensaje(Justify("La sesión a caducado, por favor inicie sesión nuevamente."), (int)eMessage.Info, "", true, true, true, PageLogin);
                    return false;
                }

                if (!(IdRolSesion > 0))
                {
                    Mensaje(Justify("La sesión a caducado, por favor inicie sesión nuevamente."), (int)eMessage.Info, "", true, true, true, PageLogin);
                    return false;
                }

                if (!(FormularioSesion > 0))
                {
                    Mensaje(Justify("La sesión a caducado, por favor inicie sesión nuevamente."), (int)eMessage.Info, "", true, true, true, PageLogin);
                    return false;
                }

                vUsuario vUser = new vUsuario();
                vUser = BL_Usuario.vUsuario(IdUsuarioSesion);

                if (vUser == null)
                {
                    Mensaje(Justify("Error al verificar los datos de la cuenta de usuario.Por favor inicie sesión nuevamente"), (int)eMessage.Error, "", true, true, true, PageLogin);
                    return false;
                }

                string UISesion = Session["UISesion"].ToString();
                if (vUser.Login != UISesion || vUser.IdRol != IdRolSesion)
                {
                    Mensaje(Justify("Error al verificar los datos de la cuenta de usuario.Por favor inicie sesión nuevamente"), (int)eMessage.Error, "", true, true, true, PageLogin);
                    return false;
                }

                List<RolFormulario> rolFormulario = new List<RolFormulario>();
                rolFormulario = (List<RolFormulario>)Session["FormulariosRolSesion"];
                if (rolFormulario == null)
                {
                    Mensaje(Justify("Error al verificar los datos de la cuenta de usuario.Por favor inicie sesión nuevamente"), (int)eMessage.Error, "", true, true, true, PageLogin);
                    return false;
                }

                if (!(rolFormulario.Count > 0))
                {
                    Mensaje(Justify("Estimado usuario, usted no cuenta con permisos registrados para acceder al sistema."), (int)eMessage.Error, "", true, true, true, PageLogin);
                    return false;
                }

                if (IdFormulario > 0)
                {
                    if (!(rolFormulario.Where(a => a.IdFormulario == IdFormulario).Count() > 0))
                    {
                        Mensaje(Justify("Estimado usuario, usted no cuenta con los permisos necesarios para acceder al recurso solicitado."), (int)eMessage.Error, "", true, true, true, PagePrincipal);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception Error)
            {
                CerrarSesion(Error.Message);
                return false;
            }
        }
        protected bool ValidarPermiso(int Permiso)
        {
            try
            {
                List<RolFormulario> rolFormularios = new List<RolFormulario>();
                rolFormularios = (List<RolFormulario>)Session["FormularioRolGl"];

                if (rolFormularios == null)
                { return false; }

                if (rolFormularios.Count == 0)
                { return false; }

                if (rolFormularios.Count > 0)
                {
                    if (Permiso == (int)ePermisos.Escribir)
                    {
                        if (rolFormularios.Where(a => a.Escribir == true).Count() > 0)
                        {
                            return true;
                        }
                    }

                    if (Permiso == (int)ePermisos.Anular)
                    {
                        if (rolFormularios.Where(a => a.Anular == true).Count() > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

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
        private void CerrarSesion(string msg)
        {
            try
            {
                Session.Abandon();
                Session.RemoveAll();
                HttpCookie cses = new HttpCookie("ASP.NET_SessionId", "");
                Response.Cookies.Add(cses);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SweetAlert", "Swal.fire('',' " + msg + " ','error').then((result) =>{ window.location.replace('Login.aspx')});", true);
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, (int)eMessage.Error);
            }

        }
        #endregion

        #region Metodos y Funciones   

        public bool Valida()
        {
            string NuevoPass;
            string ConfirmarPass;
            int IdUser;

            try
            {
                IdUser = (int)ValidarEnteros(Session["IdUsuarioSesion"].ToString());

                NuevoPass = txtPassNueva.Text.ToString();
                ConfirmarPass = txtConfirmPass.Text.ToString();

                if (NuevoPass.ToString().Length == 0 || ConfirmarPass.ToString().Length == 0)
                {
                    Mensaje("Estimado usuario, por favor indicar la nueva contraseña y su confirmación.", (int)eMessage.Alerta);
                    return false;
                }

                if (NuevoPass.Length < 8)
                {
                    Mensaje("Estimado usuario, la nueva contraseña debe tener una longitud mínima de 8 caracteres.", (int)eMessage.Alerta);
                    return false;
                }
                if (NuevoPass != ConfirmarPass)
                {
                    Mensaje("Estimado usuario, la contraseña nueva y su confirmación no coinciden.", (int)eMessage.Alerta);
                    return false;
                }
                if ((IsStrongPassword(NuevoPass) == false))
                {
                    Mensaje("Estimado usuario, por seguridad la nueva contraseña debe estar formada como mínimo de 8 caracteres, los cuales deben incluir al menos 1 letra minúscula, 1 letra mayúscula y un número.", (int)eMessage.Alerta);
                    return false;
                }
            }
            catch (Exception)
            {
                Mensaje("Datos Incorrectos, Intente nuevamente ...!!!", (int)eMessage.Alerta);
                return false;
            }
            return true;
        }
        public bool GuardarPassword()
        {
            string NuevoPassword;

            Byte[] EncriptaPassword;
            int IdUser;

            Usuario entidad = new Usuario();

            try
            {
                IdUser = (int)ValidarEnteros(Session["IdUsuarioSesion"].ToString());

                NuevoPassword = txtPassNueva.Text.ToString();
                EncriptaPassword = BL_Usuario.Encrypt(NuevoPassword);

                entidad.IdUsuario = IdUser;
                entidad.Password = EncriptaPassword;
                entidad.UsuarioActualiza = IdUser;
                entidad = BL.BL_Usuario.PasswordUpdate(entidad);
                if (entidad.IdUsuario > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool IsStrongPassword(string password)
        {
            int upperCount = 0;
            int lowerCount = 0;
            int digitCount = 0;

            for (int i = 0; i <= password.Length - 1; i++)
            {
                if (Char.IsUpper(password[i]))
                {
                    upperCount += 1;
                }
                else if (Char.IsLetter(password[i]))
                {
                    lowerCount += 1;
                }
                else if (Char.IsDigit(password[i]))
                {
                    digitCount += 1;
                }
                //else if (Char.IsSymbol(password[i]))
                //{
                //    symbolCoun += 1;
                //}
                //caracter especial
                //ElseIf[Char].IsSymbol(password(i)) Then
                //   symbolCount += 1
                //End If
            }

            return password.Length >= 8 && upperCount >= 1 && lowerCount >= 1 && digitCount >= 1;
            //AndAlso symbolCount >= 1
        }

        #endregion

        #region Evento de los controles
        protected void LnkVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }
        protected void LnkCambiarPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (Valida() == true)
                {
                    if (GuardarPassword() == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SweetAlert", "Swal.fire({title:'Cambio de Contraseña',text:'Estimado usuario, su contraseña ha sido modificada con éxito, por favor inicie sesión con las nuevas credenciales.',icon:'success',confirmButtonText:'Aceptar',confirmButtonColor: '#41DD60',backdrop: 'rgba(224,225,226,1)'}).then((result) =>{ window.location.replace('Login.aspx')});", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message.ToString().Replace("'", ""), (int)eMessage.Error);
            }
        }
        protected void Mostrar(TextBox textBox, HtmlGenericControl Icono)
        {
            try
            {
                if (textBox.TextMode == TextBoxMode.SingleLine)
                {
                    textBox.TextMode = TextBoxMode.Password;
                    Icono.Attributes.Remove("fas fa-eye-slash");
                    Icono.Attributes.Add("class", "fas fa-eye");

                }
                else if (textBox.TextMode == TextBoxMode.Password)
                {
                    textBox.TextMode = TextBoxMode.SingleLine;
                    Icono.Attributes.Remove("fas fa-eye");
                    Icono.Attributes.Add("class", "fas fa-eye-slash");
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message.ToString().Replace("'", ""), (int)eMessage.Error);
            }
        }
        protected void lnkMostrarPassword_Click(object sender, EventArgs e)
        {
            try
            {
                Mostrar(this.txtPassNueva, ipassnuevo);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message.ToString().Replace("'", ""), (int)eMessage.Error);
            }
        }
        protected void lnkMostrarRepetirPassword_Click(object sender, EventArgs e)
        {
            try
            {
                Mostrar(this.txtConfirmPass, ipassconfirm);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message.ToString().Replace("'", ""), (int)eMessage.Error);
            }
        }

        #endregion
    }
}