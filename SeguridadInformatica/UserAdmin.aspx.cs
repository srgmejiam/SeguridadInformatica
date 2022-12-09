using BL;
using EL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static EL.Enum;

namespace SeguridadInformatica
{
    public partial class administracionUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (ValidarDatosSesion((int)eFormularios.Usuarios))
                    {
                        cargarGridUsuarios();
                        cargarDDL_Rol();
                        cargarddlBaja();
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message.ToString().Replace("'", ""), (int)eMessage.Error);
            }

        }

        #region Seguridad
        protected bool ValidarDatosSesion(int IdFormulario = 0)
        {
            string PageLogin = "Login.aspx";
            string PagePrincipal = "Principal.aspx";
            try
            {
                int IdUsuarioSesion = (int)(Session["IdUsuarioSesion"]);
                int IdRolSesion = (int)(Session["IdRolSesion"]);
                int FormularioSesion = (int)(Session["FormularioSesion"]);


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
                Mensaje("Datos de sesión incompletos, Por favor inicie sesión nuevamente" + Error.Message.Replace("'", ""), (int)eMessage.Error, "", true, true, true, PageLogin);
                return false;
            }
        }
        protected bool ValidarPermiso(int Permiso, int idFormulario)
        {
            try
            {
                List<RolFormulario> rolFormularios = new List<RolFormulario>();
                rolFormularios = (List<RolFormulario>)Session["FormulariosRolSesion"];

                if (rolFormularios == null)
                { return false; }

                if (rolFormularios.Count == 0)
                { return false; }

                if (rolFormularios.Count > 0)
                {
                    if (Permiso == (int)ePermisos.Escribir)
                    {
                        if (rolFormularios.Where(a => a.Escribir == true && a.IdFormulario == idFormulario).Count() > 0)
                        {
                            return true;
                        }
                    }

                    if (Permiso == (int)ePermisos.Anular)
                    {
                        if (rolFormularios.Where(a => a.Anular == true && a.IdFormulario == idFormulario).Count() > 0)
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
        private void Modal(bool Abrir, string NombreModal)
        {
            if (Abrir)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AbrirModal", "$('#" + NombreModal + "').modal('show');", true);
                return;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CerrarModal", "$('#" + NombreModal + "').modal('hide');", true);
            return;
        }
        #endregion

        #region Metodos y Funciones Formulario Principal
        protected void Filtrar(GridView Grid, string textoFiltrar, List<vUsuario> dataSource, string[] Columnas)
        {
            this.lblCantidadRegistros.Text = "";
            try
            {
                string frase = textoFiltrar.Trim();
                if (frase.Length >= 1 && Grid.Rows.Count > 0)
                {

                    Grid.DataSource = dataSource.Where(a => DbFunctions.Like(a.Nombre, "%" + Columnas[0] + "%"));
                    Grid.DataBind();

                    this.lblCantidadRegistros.Text = "Cantidad de Registros: " + dataSource.Count;
                }
            }
            catch (Exception ex)
            {
                Grid.DataSource = null;
                Grid.DataBind();
            }
        }
        protected void cambiarPaginado(GridView Grid, int PageSize)
        {
            if (PageSize > 0)
            {
                Grid.AllowPaging = true;
                Grid.PageSize = PageSize;
            }
            else
            {
                Grid.AllowPaging = false;
            }
        }
        protected bool validarComplejidadPassword(string password)
        {
            int Mayusculas = 0;
            int Minisculas = 0;
            int Numeros = 0;

            for (int i = 0; i <= password.Length - 1; i++)
            {
                if (char.IsUpper(password[i]))
                {
                    Mayusculas += 1;
                }
                else if (char.IsLetter(password[i]))
                {
                    Minisculas += 1;
                }
                else if (char.IsDigit(password[i]))
                {
                    Numeros += 1;
                }

            }
            return password.Length >= 8 && Mayusculas >= 1 && Minisculas >= 1 && Numeros >= 1;

        }
        public void administrarBotones(bool Nuevo = false, bool Guardar = false, bool Anular = false, bool Desbloquear = false)
        {
            try
            {
                panelBtnGuardar.Visible = true;
                panelBtnAnular.Visible = (ValidarPermiso((int)ePermisos.Anular, (int)eFormularios.Usuarios)) ? Anular : false;
                panelBtnDesbloquear.Visible = (ValidarPermiso((int)ePermisos.Escribir, (int)eFormularios.Usuarios)) ? Desbloquear : false;
            }
            catch (Exception)
            {
            }
        }
        protected void limpiarControles()
        {
            try
            {
                txtNombre.Text = "";
                txtLogin.Text = "";
                txtContraseña.Text = "";
                txtEmail.Text = "";
                txtCargo.Text = "";
                ddlBaja.SelectedValue = "1";

                cargarDDL_Rol();

                HF_Nombre.Value = "";
                HF_Login.Value = "";
                HF_Email.Value = "";
                HF_Cargo.Value = "";
                HF_IdUsuario.Value = "0";
                HF_CodigoMunicipio.Value = "0";
                administrarBotones(true, true, false, false);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message.Replace("'", "").ToString(), (int)eMessage.Error);
            }
        }
        protected void cargarGridUsuarios()
        {
            {
                DataView dv = new DataView();
                try
                {

                    gridUsuarios.DataSource = null;
                    gridUsuarios.DataBind();
                    gridUsuarios.DataSource = BL_Usuario.VistaUsuarios();
                    gridUsuarios.DataBind();

                    if (dv.Count > 0)
                    {
                        this.lblCantidadRegistros.Text = "Cantidad Registros : " + dv.Count;
                    }
                    else
                    {
                        this.lblCantidadRegistros.Text = string.Empty;
                        this.gridUsuarios.EmptyDataText = "No se encontraron registros";
                    }
                    //ddlPagerSize.Visible = true;

                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message.Replace("'", "").ToString(), (int)eMessage.Error);
                }
            }
            administrarBotones(true, true, false, false);
        }
        private void TraerDatos()
        {
            bool baja = false;
            EL.Usuario datos = new Usuario();
            try
            {
                datos = BL_Usuario.BuscarUsuario(Convert.ToInt32(HF_IdUsuario.Value));

                txtNombre.Text = datos.Nombre.ToString();
                txtLogin.Text = datos.Login.ToString();
                txtEmail.Text = datos.Email.ToString();
                txtCargo.Text = datos.Cargo.ToString();

                ddlRol.SelectedValue = datos.IdRol.ToString();
                baja = datos.Baja;
                if (baja == false)
                {
                    ddlBaja.SelectedValue = "1";
                }
                else
                {
                    ddlBaja.SelectedValue = "2";
                }




            }
            catch (Exception error)
            {
                string msj = error.Message.ToString();
                Mensaje(msj, (int)eMessage.Error);
            }
        }
        protected void cargarDDL_Rol()
        {
            try
            {
                ddlRol.Items.Clear();
                ddlRol.Items.Insert(0, "--Seleccione--");
                ddlRol.DataSource = BL_Rol.ListadeRoles();
                ddlRol.DataValueField = "IdRol";
                ddlRol.DataTextField = "NombreRol";
                ddlRol.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message.Replace("'", ""), (int)eMessage.Error);
            }
        }
        protected bool validarControles(int idusuario)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    Mensaje("Por favor ingresar el nombre del usuario.", (int)eMessage.Alerta);
                    return false;
                }
                if (string.IsNullOrEmpty(txtLogin.Text) || string.IsNullOrWhiteSpace(txtLogin.Text))
                {
                    Mensaje("Por favor ingresar el login del usuario.", (int)eMessage.Alerta);
                    return false;
                }
                if (idusuario == 0)
                {
                    if (BL_Usuario.ExiteLogin(txtLogin.Text))
                    {
                        Mensaje("El Login que ingresó, ya se encuentra asociado a un usuario registrado.", (int)eMessage.Error);
                        return false;
                    }
                }
                if (idusuario > 0)
                {
                    if (BL_Usuario.ExiteLoginUpdate(txtLogin.Text, idusuario))
                    {
                        Mensaje("El Login que ingresó, ya se encuentra asociado a un usuario registrado.", (int)eMessage.Error);
                        return false;
                    }
                }
                if (idusuario == 0)
                {
                    if (string.IsNullOrEmpty(txtContraseña.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text))
                    {
                        {
                            Mensaje("Por favor ingresar la contraseña del usuario.", (int)eMessage.Alerta);
                            return false;
                        }
                    }
                    if (!validarComplejidadPassword(txtContraseña.Text.Trim()))
                    {
                        string msj = "Estimado usuario, por seguridad el password debe tener un mínimo de 8 caracteres con los siguientes criterios: 1 letra mayúscula, 1 letra minúscula y 1 número.";

                        Mensaje(msj, (int)eMessage.Alerta);
                        return false;
                    }
                }
                if (idusuario > 0)
                {
                    if (!(string.IsNullOrEmpty(txtContraseña.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text)))
                    {
                        if (!validarComplejidadPassword(txtContraseña.Text.Trim()))
                        {
                            string msj = "Estimado usuario, por seguridad el password debe tener un mínimo de ocho caracteres con los siguientes criterios: Una letra mayúscula, Una letra minúscula y Un número.";

                            Mensaje(msj, (int)eMessage.Alerta);
                            return false;
                        }
                    }
                }
                if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    Mensaje("Por favor ingrese el correo del usuario.", (int)eMessage.Alerta);
                    return false;
                }
                if (!BL_Usuario.ValidarFormatoEmail(txtEmail.Text))
                {
                    string msj = "Estimado usuario, por favor ingrese un correo valido.";
                    Mensaje(msj, (int)eMessage.Alerta);
                    return false;
                }
                if (idusuario == 0)
                {
                    if (BL_Usuario.ExisteCorreo(txtEmail.Text))
                    {
                        Mensaje("El Correo que ingresó, ya se encuentra asociado a un usuario registrado.", (int)eMessage.Error);
                        return false;
                    }
                }
                if (idusuario > 0)
                {
                    if (BL_Usuario.ExisteCorreoUpdate(txtEmail.Text, idusuario))
                    {
                        Mensaje("El Correo que ingresó, ya se encuentra asociado a un usuario registrado.", (int)eMessage.Error);
                        return false;
                    }
                }
                if (ddlRol.SelectedIndex == 0)
                {

                    Mensaje("Por favor seleccionar el Rol del usuario.", (int)eMessage.Alerta);
                    return false;
                }

                if (string.IsNullOrEmpty(txtCargo.Text) || string.IsNullOrWhiteSpace(txtCargo.Text))
                {
                    Mensaje("Por favor ingresar el cargo.", (int)eMessage.Alerta);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message.Replace("'", ""), (int)eMessage.Alerta);
                return false;
            }

        }
        protected bool ValidarControlesUpdate()
        {
            try
            {
                int IdUsuario = Convert.ToInt32(HF_IdUsuario.Value);
                {
                    if (IdUsuario > 0)
                    {
                        if (BL_Usuario.ExiteLogin(txtLogin.Text))
                        {
                            Mensaje("El Login que ingresó, ya se encuentra asociado a un usuario registrado.", (int)eMessage.Error);
                            return false;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Mensaje(ex.Message.Replace("'", ""), (int)eMessage.Alerta);
                return false;
            }
            return false;
        }
        protected bool GuardarUsuario()
        {
            Usuario entidad = new Usuario();
            try
            {
                int IdUsuarioSession = (int)(Session["IdUsuarioSesion"]);

                entidad.IdUsuario = Convert.ToInt32(HF_IdUsuario.Value);
                entidad.Nombre = txtNombre.Text;
                entidad.Login = txtLogin.Text.Trim();
                entidad.Email = txtEmail.Text.Trim();
                entidad.Cargo = txtCargo.Text;
                entidad.IdRol = Convert.ToInt32(ddlRol.SelectedValue);
                if (ddlBaja.SelectedValue == "1")
                {
                    entidad.Baja = false;
                }
                else
                {
                    entidad.Baja = true;
                }
                entidad.FechaRegistro = DateTime.Now;
                entidad.UsuarioRegistro = IdUsuarioSession;



                if (BL_Usuario.ExisteUsuario(entidad.IdUsuario))
                {
                    bool updatepassword = false;
                    if (!(string.IsNullOrEmpty(txtContraseña.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text)))
                    {
                        entidad.Password = BL_Usuario.Encrypt(txtContraseña.Text.Trim());
                        updatepassword = true;
                    }
                    entidad.UsuarioActualiza = IdUsuarioSession;
                    entidad.FechaActualiza = DateTime.Now;
                    return BL_Usuario.UpdateUsuario(entidad, updatepassword);
                }
                else
                {
                    entidad.Password = BL_Usuario.Encrypt(txtContraseña.Text.Trim());
                    entidad.UsuarioRegistro = IdUsuarioSession;
                    return BL_Usuario.InsertUsuario(entidad).IdUsuario > 0;

                }
            }
            catch (Exception error)
            {
                string msj = error.Message.ToString();
                Mensaje(msj, (int)eMessage.Error);
                return false;
            }

        }
        protected bool anularUsuario()
        {

            bool valor = true;

            Usuario entidad = new Usuario();
            try
            {
                int IdUsuarioSession = (int)(Session["IdUsuarioSesion"]);
                entidad.IdUsuario = Convert.ToInt32(HF_IdUsuario.Value);
                entidad.UsuarioActualiza = IdUsuarioSession;
                if (BL_Usuario.AnularUsuario(entidad))
                {
                    valor = true;
                }

            }
            catch (Exception error)
            {
                string msj = error.Message.ToString();
                Mensaje(msj, (int)eMessage.Error);
                valor = false;
            }
            return valor;
        }
        private void cargarddlBaja()
        {
            DataTable dt = new DataTable();
            try
            {

                dt.Columns.Add("Id");
                dt.Columns.Add("Baja");

                DataRow row = dt.NewRow();
                row["Id"] = 1;
                row["Baja"] = "NO";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row["Id"] = 2;
                row["Baja"] = "SI";
                dt.Rows.Add(row);

                if (Convert.ToInt32(dt.Rows.Count.ToString()) != 0)
                {

                    this.ddlBaja.DataSource = dt;
                    ddlBaja.DataTextField = "Baja";
                    ddlBaja.DataValueField = "Id";
                    ddlBaja.DataBind();
                }
            }
            catch (SystemException error)
            {
                string msj = error.Message.ToString();
            }
        }
        protected bool ValidarAtencion()
        {
            bool valor = true;
            DataView dv = new DataView();
            try
            {
                int IdUsuario = Convert.ToInt32(HF_IdUsuario.Value);




                if (dv.Count > 0)
                {

                    Mensaje("El Usuario no puede ser anulado porque tiene una atencion pendiente .", (int)eMessage.Error);
                    valor = false;
                }


            }

            catch (Exception ex)
            {
                Mensaje(ex.Message.Replace("'", ""), (int)eMessage.Alerta);
                valor = false;
            }
            return valor;
        }

        #endregion

        #region Eventos de los Controles Formulario Principal   
        protected void lnkMostrarPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtContraseña.TextMode == TextBoxMode.SingleLine)
                {
                    this.txtContraseña.TextMode = TextBoxMode.Password;
                    iconoverpassword.Attributes.Remove("fa fa-eye-slash");
                    iconoverpassword.Attributes.Add("class", "fa fa-eye");
                }
                else if (this.txtContraseña.TextMode == TextBoxMode.Password)
                {
                    this.txtContraseña.TextMode = TextBoxMode.SingleLine;
                    iconoverpassword.Attributes.Remove("fa fa-eye");
                    iconoverpassword.Attributes.Add("class", "fa fa-eye-slash");
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message.ToString().Replace("'", ""), (int)eMessage.Error);

            }
        }

        protected void lnkVolver_Click(object sender, EventArgs e)
        {
            {
                Response.Redirect("~/Principal.aspx");
            }
        }
        protected void lnkNuevo_Click1(object sender, EventArgs e)
        {
            limpiarControles();
            administrarBotones(true, true, false, false);
            cargarGridUsuarios();
        }
        protected void lnkGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int IdUsuarioSession = (int)(Session["IdUsuarioSesion"]);
                int IdUsuario = Convert.ToInt32(HF_IdUsuario.Value);
                if (validarControles(IdUsuario))
                {

                    if (GuardarUsuario() == true)
                    {
                        cargarGridUsuarios();
                        Mensaje("Guardado con éxito", (int)eMessage.Exito);
                        limpiarControles();
                    }
                    else
                    {
                        Mensaje("El registro no se guardo", (int)eMessage.Error);
                    }


                }
            }
            catch (Exception error)
            {
                string msj = error.Message.ToString();
                Mensaje(msj, (int)eMessage.Error);
            }
        }
        protected void lnkAnular_Click(object sender, EventArgs e)
        {
            try
            {

                int IdUsuario = Convert.ToInt32(HF_IdUsuario.Value);

                if (IdUsuario > 0)
                {
                    if (ValidarAtencion() == false)
                    {
                        return;
                    }
                    if (anularUsuario())
                    {

                        Mensaje("Usuario anulado con éxito", (int)eMessage.Exito);
                        cargarGridUsuarios();
                        limpiarControles();
                    }
                    else
                    {
                        Mensaje("El registro no se anulo", (int)eMessage.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message.Replace("'", ""), (int)eMessage.Error);
            }
        }
        protected void lnkDesbloquear_Click(object sender, EventArgs e)
        {
            DesbloquearUsuario();
        }
        protected void gridUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in this.gridUsuarios.Rows)
            {
                if ((gvr.RowIndex != this.gridUsuarios.SelectedIndex))
                {
                    txtContraseña.Text = "";
                    this.gridUsuarios.Rows[gvr.RowIndex].ForeColor = System.Drawing.Color.Black;
                    ((ImageButton)gvr.Cells[0].FindControl("ImageButton1")).ImageUrl = "~/assets/Images/empty.png";
                }
                else
                {
                    this.gridUsuarios.Rows[gvr.RowIndex].ForeColor = System.Drawing.Color.DarkBlue;
                    ((ImageButton)gvr.Cells[0].FindControl("ImageButton1")).ImageUrl = "~/assets/Images/selected.png";
                }

            }
        }
        protected void gridUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridUsuarios.PageIndex = e.NewPageIndex;
            cargarGridUsuarios();
            limpiarControles();
        }
        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message.ToString(), (int)eMessage.Error);
            }
        }
        protected void gridUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            bool Baja = false;
            try
            {
                if (e.CommandName.Equals("Select"))
                {
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    int index = gvr.RowIndex;

                    HF_IdUsuario.Value = gridUsuarios.DataKeys[index]["IdUsuario"].ToString();
                    HF_Bloqueado.Value = gridUsuarios.DataKeys[index]["Bloqueado"].ToString();
                    TraerDatos();
                    bool Desbloquear = Convert.ToBoolean(HF_Bloqueado.Value);
                    administrarBotones(true, true, true, Desbloquear);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message.ToString(), (int)eMessage.Error);
            }
        }
        protected void gridUsuarios_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;

                bool xActivo = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Bloqueado").ToString());
                Label lblBloqueado = (Label)e.Row.FindControl("lblBloqueado");
                if (xActivo == false)
                {
                    lblBloqueado.Text = "NO";
                }
                else
                {
                    lblBloqueado.Text = "SI";
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;

                bool xActivo = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Baja").ToString());
                Label lblBaja = (Label)e.Row.FindControl("lblBaja");
                if (xActivo == true)
                {
                    lblBaja.Text = "SI";
                }
                else
                {
                    lblBaja.Text = "NO";
                }
            }

        }
        protected void DesbloquearUsuario()
        {
            try
            {

                int IdUsuarioSession = (int)(Session["IdUsuarioSesion"]);
                int IdUsuario = Convert.ToInt32(HF_IdUsuario.Value);
                if (!(IdUsuario > 0))
                {
                    Mensaje("Error al recuperar el ID del usuario", (int)eMessage.Error);
                    return;
                }

                Usuario User = new Usuario();
                User = BL_Usuario.BuscarUsuario(IdUsuario);

                if (!(IdUsuario != 0))
                {
                    Mensaje("Error al recuperar la Entidad del usuario", (int)eMessage.Error);
                    return;
                }

                int Intentos = User.Intentos;

                if (!BL_Usuario.RestablecerIntentosFallido(IdUsuario))
                {
                    Mensaje("Error al restablecer los intentos fallidos de inicio de sesión del usuario", (int)eMessage.Error);
                    return;
                }

                if (!BL_Usuario.BloquearCuentaUsuario(IdUsuario, false))
                {
                    Mensaje("Error al desbloquear la cuenta del usuario", (int)eMessage.Error);
                    return;
                }

                Historial RegistroHistorial = new Historial();
                RegistroHistorial.Descripcion = "La cuenta del usuario ID: " + User.IdUsuario + " ,con login actualmente: " + User.Login + " fue bloqueada por multiples intentos fallidos de iniciar sesión (" + Intentos.ToString()
                    + " Intentos). Fue desbloqueda por el usuario ID: " + IdUsuarioSession.ToString();
                RegistroHistorial.UsuarioRegistro = IdUsuarioSession;
             
                Mensaje("Usuario desbloqueado con exito!!!", (int)eMessage.Exito);
                cargarGridUsuarios();
                limpiarControles();

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message.Replace("'", ""), (int)eMessage.Error);
            }
        }


        #endregion

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void ddlBaja_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}