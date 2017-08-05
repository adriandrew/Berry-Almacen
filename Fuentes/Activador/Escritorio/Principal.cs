using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Data.Sql;

namespace Escritorio
{
    public partial class Principal : Form
    {

        // Variables de objetos de entidades.
        Entidades.BaseDatos baseDatos = new Entidades.BaseDatos(); 
        Entidades.Directorios directorios = new Entidades.Directorios();
        Entidades.Licencia licencia = new Entidades.Licencia();
        ProcessStartInfo ejecutarProgramaPrincipal = new ProcessStartInfo();
        // Variables generales.
        public bool tieneParametros = false;
        public bool esInicioSesion = true; 
        public string nombrePrograma = string.Empty;
        public bool estaCerrando = false; public bool estaAbriendoPrograma = false;
        public bool estaMostrado = false;
        public int diasDePrueba = 15;
        // Variable de desarrollo.
        public bool esDesarrollo = false;

        public Principal()
        {
            InitializeComponent();
        }
        
        #region Eventos

        private void Principal_Load(object sender, EventArgs e)
        {

            Centrar();
            AsignarTooltips();
            AsignarFocos();
            ConfigurarConexiones();
            CargarEncabezados();
            CargarTitulosDirectorio();

        }

        private void Principal_Shown(object sender, EventArgs e)
        {

            this.txtUsuario.Focus();
            this.estaMostrado = true;
            ValidarLicencia();

        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {

            Salir();

        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {

            this.estaCerrando = true;

        }
  
        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (!string.IsNullOrEmpty(this.txtUsuario.Text))
                { 
                    this.txtContraseña.Focus();                
                }            
            } 

        }

        private void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        { 

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (!string.IsNullOrEmpty(this.txtContraseña.Text))
                {
                    this.btnEntrar.Focus();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.txtUsuario.Focus();
            } 

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        { 

            ValidarSesion(); 

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            SalirOVolver();

        }

        private void btnEntrar_MouseHover(object sender, EventArgs e)
        {

            AsignarTooltips("Entrar.");

        }

        private void btnSalir_MouseHover(object sender, EventArgs e)
        {

            if (this.esInicioSesion)
            {
                AsignarTooltips("Salir."); 
            }
            else
            {
                AsignarTooltips("Volver a Iniciar Sesión."); 
            }

        }
         
        private void pnlEncabezado_MouseHover(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty); 

        }

        private void pnlContenido_MouseHover(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty); 

        }

        private void pnlMenu_MouseHover(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty); 

        }

        private void pnlIniciarSesion_MouseHover(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty); 

        }

        private void pnlPie_MouseHover(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty); 

        }
         
        private void btnAyuda_MouseHover(object sender, EventArgs e)
        {

            AsignarTooltips("Ayuda.");

        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {

            MostrarAyuda();

        }

        private void chkActivar_CheckedChanged(object sender, EventArgs e)
        {

            if (this.estaMostrado)
            {
                if (chkActivar.Checked)
                {
                    chkActivar.Text = "Activado";
                    Activar();
                }
                else
                {
                    chkActivar.Text = "Desactivado";
                    Desactivar();
                } 
            }

        }

        #endregion

        #region Métodos

        private void ValidarLicencia()
        {

            List<Entidades.Licencia> lista = new List<Entidades.Licencia>(); 
            lista = licencia.ObtenerListado();
            if (lista.Count > 0)
            {
                if (lista[0].EsPrueba)
                {
                    chkActivar.Checked = false;
                }
                else
                {
                    chkActivar.Checked = true;
                }
            }
            else
            {
                chkActivar.Checked = false;
            }

        }

        private void Salir() 
        {

            this.Cursor = Cursors.WaitCursor;
            if (this.estaAbriendoPrograma)
            {
                System.Threading.Thread.Sleep(10000);
            }
            Application.ExitThread();
            Application.Exit();
            this.Cursor = Cursors.Default;

        }

        private void SalirOVolver() 
        {

            if (this.esInicioSesion)
            {
                Application.Exit();
            }
            else
            {
                pnlContenido.BackgroundImage = global::Activador.Properties.Resources.Logo3;
                pnlContenido.BackgroundImageLayout = ImageLayout.Zoom;
                pnlContenido.BackColor = Color.DarkSlateGray;
                pnlPanelControl.Visible = false;
                pnlIniciarSesion.Visible = true;
                txtContraseña.Text = string.Empty;
                txtUsuario.Text = string.Empty;
                txtUsuario.Focus();
                this.esInicioSesion = true;
            }

        }

        private void MostrarAyuda()
        {

            Panel pnlAyuda = new Panel();
            TextBox txtAyuda = new TextBox();
            if (pnlContenido.Controls.Find("pnlAyuda", true).Count() == 0)
            {
                pnlAyuda.Name = "pnlAyuda"; Application.DoEvents();
                pnlAyuda.Visible = false; Application.DoEvents();
                pnlContenido.Controls.Add(pnlAyuda); Application.DoEvents();
                txtAyuda.Name = "txtAyuda"; Application.DoEvents();
                pnlAyuda.Controls.Add(txtAyuda); Application.DoEvents();
            }
            else
            {
                pnlAyuda = pnlContenido.Controls.Find("pnlAyuda", false).FirstOrDefault() as Panel; Application.DoEvents();
                txtAyuda = pnlAyuda.Controls.Find("txtAyuda", false).FirstOrDefault() as TextBox; Application.DoEvents();
            }
            if ((!pnlAyuda.Visible))
            {
                if (this.esInicioSesion)
                {
                    pnlIniciarSesion.Visible = false; Application.DoEvents();
                }
                else
                { 
                    pnlPanelControl.Visible = false; Application.DoEvents(); 
                }
                pnlAyuda.Visible = true; Application.DoEvents();
                pnlAyuda.Size = pnlPanelControl.Size; Application.DoEvents();
                pnlAyuda.Location = pnlPanelControl.Location; Application.DoEvents();
                pnlContenido.Controls.Add(pnlAyuda); Application.DoEvents();
                txtAyuda.ScrollBars = ScrollBars.Both; Application.DoEvents();
                txtAyuda.Multiline = true; Application.DoEvents();
                txtAyuda.Width = pnlAyuda.Width - 10; Application.DoEvents();
                txtAyuda.Height = pnlAyuda.Height - 10; Application.DoEvents();
                txtAyuda.Location = new Point(5, 5); Application.DoEvents();
                txtAyuda.Text = "Sección de Ayuda: " + System.Environment.NewLine + System.Environment.NewLine + "* Activador: " + System.Environment.NewLine + "Únicamente usuarios autorizados por Berry pueden utilizar este programa para activar el sistema. " + System.Environment.NewLine + "Saludos."; Application.DoEvents();
                pnlAyuda.Controls.Add(txtAyuda); Application.DoEvents();
            }
            else
            {
                if (this.esInicioSesion)
                {
                    pnlIniciarSesion.Visible = true; Application.DoEvents();
                }
                else 
                { 
                    pnlPanelControl.Visible = true; Application.DoEvents(); 
                }
                pnlAyuda.Visible = false; Application.DoEvents();
            }

        } 

        private void ValidarSesion()
        { 
             
            if (!string.IsNullOrEmpty(this.txtUsuario.Text) && !string.IsNullOrEmpty(this.txtContraseña.Text))
            {
                if ((txtUsuario.Text.ToUpper().Equals("Admin".ToUpper())) && (txtContraseña.Text.Equals("@berry2017")))
                { 
                    PermitirAcceso();
                }
                else
                {
                    MessageBox.Show("Credenciales no válidas.", "Datos incorrectos.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUsuario.Focus();
                }
            }
            else
            {
                MessageBox.Show("Faltan datos.", "Faltan datos.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUsuario.Focus(); 
            }

        }

        private void PermitirAcceso()
        {

            pnlIniciarSesion.Visible = false; Application.DoEvents();
            pnlPanelControl.Visible = true; Application.DoEvents();
            this.esInicioSesion = false;  

        }

        private void Centrar()
        {

            this.nombrePrograma = this.Text;
            this.CenterToScreen();
            //this.Opacity = .97; //Está bien perro esto.  
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;

        }

        private void AsignarTooltips()
        {

            ToolTip tp = new ToolTip();
            tp.AutoPopDelay = 5000;
            tp.InitialDelay = 0;
            tp.ReshowDelay = 100;
            tp.ShowAlways = true; 
            tp.SetToolTip(this.btnEntrar, "Entrar.");
            tp.SetToolTip(this.btnSalir, "Salir.");
            tp.SetToolTip(this.btnAyuda, "Ayuda.");

        }

        private void AsignarTooltips(string texto)
        {

            lblDescripcionTooltip.Text = texto;

        }

        private void AsignarFocos()
        {

            this.btnEntrar.Focus();            

        }

        private void ConfigurarConexionPrincipal() 
        {

            if (this.esDesarrollo)
            {
                baseDatos.CadenaConexionPrincipal = "C:\\Berry ERP\\DatosPrincipales.sdf";
                pnlEncabezado.BackColor = Color.DarkRed;
                pnlPie.BackColor = Color.DarkRed;
            }
            else
            {
                string ruta = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                ruta = ruta.Replace("file:\\", null);
                baseDatos.CadenaConexionPrincipal = string.Format("{0}\\DatosPrincipales.sdf", ruta); 
            }
            baseDatos.AbrirConexionPrincipal();
        
        }

        private void ConfigurarConexiones() 
        {

            ConfigurarConexionPrincipal();
            ConsultarInformacionDirectorioPredeterminado();

        }
         
        private void ConsultarInformacionDirectorioPredeterminado()
        {
             
            List<Entidades.Directorios> lista = new List<Entidades.Directorios>();
            lista = directorios.ObtenerPredeterminado();
            if (lista.Count > 0)
            {
                Logica.Directorios.id = Convert.ToInt32(lista[0].Id);
                Logica.Directorios.nombre = lista[0].Nombre.ToString();
                Logica.Directorios.descripcion = Logica.Funciones.ValidarLetra(lista[0].Descripcion.ToString());
                Logica.Directorios.rutaLogo = Logica.Funciones.ValidarLetra(lista[0].RutaLogo.ToString());
                Logica.Directorios.esPredeterminado = Convert.ToBoolean(lista[0].EsPredeterminado.ToString());
                Logica.Directorios.instanciaSql = lista[0].InstanciaSql.ToString();
                Logica.Directorios.usuarioSql = lista[0].UsuarioSql.ToString();
                Logica.Directorios.contrasenaSql = lista[0].ContrasenaSql.ToString(); 
            }

        }
                 
        private void CargarTitulosDirectorio()
        {

            this.Text = this.nombrePrograma;

        }

        private void CargarEncabezados()
        { 

            lblEncabezadoPrograma.Text = "Programa: " + this.nombrePrograma; 

        }

        private void Activar()
        {

            licencia.EsPrueba = false;
            licencia.FechaRegistro = DateTime.Today;
            licencia.FechaVencimiento = DateTime.Today;
            licencia.Eliminar();
            licencia.Guardar();

        }

        private void Desactivar()
        {

            licencia.EsPrueba = true;
            licencia.FechaRegistro = DateTime.Today;
            licencia.FechaVencimiento = DateTime.Today;
            licencia.Eliminar(); 

        }

        #endregion

    }
}