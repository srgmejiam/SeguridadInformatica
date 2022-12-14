using BL;
using EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using static EL.Enum;

namespace SeguridadInformatica
{
    public partial class Menu : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ValidarDatosSesion((int)eFormularios.SiteMaster))
                {
                    PermisosMenu();
                }
            }
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
        public bool PermisosMenu()
        {
            string PageLogin = "Login.aspx";

            try
            {
                lnkFormularios.Visible = false;
                Formulario_1.Visible = false;
                Formulario_2.Visible = false;
                Formulario_3.Visible = false;
                lnkAdministrarUsuarios.Visible = false;
                lnkCatalogos.Visible = false;
                lnkCatalogo_1.Visible = false;
                lnkCatalogo_2.Visible = false;
                lnkCatalogo_3.Visible = false;
                lnkCambiarPassword.Visible = true;
                lnkSalir.Visible = true;

                List<RolFormulario> rolFormulario = new List<RolFormulario>();
                rolFormulario = (List<RolFormulario>)Session["FormulariosRolSesion"];


                if ((rolFormulario.Where(a => a.IdFormulario == (int)eFormularios.Formularios).Count() > 0))
                {
                    lnkFormularios.Visible = true;
                }
                if ((rolFormulario.Where(a => a.IdFormulario == (int)eFormularios.Catalogos).Count() > 0))
                {
                    lnkCatalogos.Visible = true;
                }
                if ((rolFormulario.Where(a => a.IdFormulario == (int)eFormularios.Usuarios).Count() > 0))
                {
                    lnkAdministrarUsuarios.Visible = true;
                }
                if ((rolFormulario.Where(a => a.IdFormulario == (int)eFormularios.Formulario_1).Count() > 0))
                {
                    Formulario_1.Visible = true;
                }
                if ((rolFormulario.Where(a => a.IdFormulario == (int)eFormularios.Formulario_2).Count() > 0))
                {
                    Formulario_2.Visible = true;
                }
                if ((rolFormulario.Where(a => a.IdFormulario == (int)eFormularios.Formulario_3).Count() > 0))
                {
                    Formulario_3.Visible = true;
                }
                if ((rolFormulario.Where(a => a.IdFormulario == (int)eFormularios.Catalogo_1).Count() > 0))
                {
                    lnkCatalogo_1.Visible = true;
                }
                if ((rolFormulario.Where(a => a.IdFormulario == (int)eFormularios.Catalogo_2).Count() > 0))
                {
                    lnkCatalogo_2.Visible = true;
                }
                if ((rolFormulario.Where(a => a.IdFormulario == (int)eFormularios.Catalogo_3).Count() > 0))
                {
                    lnkCatalogo_3.Visible = true;
                }

                return true;
            }
            catch (Exception Error)
            {
                Mensaje("Datos de sesión incompletos, Por favor inicie sesión nuevamente" + Error.Message.Replace("'", ""), (int)eMessage.Error, "", true, true, true, PageLogin);
                return false;
            }
        }
        #endregion

        #region Metodos y Funciones
        private void Mensaje(string Message, int tipoMensaje, string Encabezado = "", bool Html = false, bool Fondo = false, bool returnLogin = false, string UrlReturn = "", bool CerrarClick = true)
        {
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
        #endregion

        #region Evento de los Controles
        protected void lnkInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }
        protected void lnkAdministrarUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserAdmin.aspx", true);
        }
        protected void lnkSalir_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("Login.aspx", true);
        }
        protected void lnkCambiarPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("cambiarPassword.aspx");
        }
        #endregion

    }



}