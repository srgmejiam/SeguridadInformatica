using System;

namespace SWSAT
{
    public class varSesion : System.Web.HttpApplication
    {
        protected void Session_Start(object sender, EventArgs e)
        {
            Session["IdUsuarioSesion"] = 0;
            Session["LoginSesion"] = 0;
            Session["NombreCompleto"] = 0;
            Session["Cargo"] = 0;
            Session["IdRolSesion"] = 0;
            Session["Rol"] = 0;           
            Session["FormularioSesion"] = 0;
            Session["FormulariosRolSesion"]= 0;
        }
        protected void Session_End(object sender, EventArgs e)
        {
            Session.Remove("IdUsuarioSesion");
            Session.Remove("LoginSesion");
            Session.Remove("NombreCompleto");
            Session.Remove("Cargo");
            Session.Remove("IdRolSesion");
            Session.Remove("Rol");
            Session.Remove("FormularioSesion");
            Session.Remove("FormulariosRolSesion");
        }
    }
}