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
        public Entidades.BaseDatos baseDatos = new Entidades.BaseDatos();
        public Entidades.Directorios directorios = new Entidades.Directorios(); 
        public Entidades.Usuarios usuarios = new Entidades.Usuarios();
        public Entidades.Modulos modulos = new Entidades.Modulos();
        public Entidades.Programas programas = new Entidades.Programas();
        public Entidades.SubProgramas subProgramas = new Entidades.SubProgramas();
        public Entidades.BloqueoUsuarios bloqueoUsuarios = new Entidades.BloqueoUsuarios();
        public Entidades.Licencia licencia = new Entidades.Licencia();
        public Entidades.Registros registros = new Entidades.Registros();
        public Entidades.Estilos estilos = new Entidades.Estilos(); 
        // Variables de menú.
        public Color colorCuadroOriginal = Color.Transparent;
        public int nivelMenu = 1; // 1 módulo, 2 programa y 3 subprograma.
        // Variables de formatos de spread.
        public static int tamañoLetraSpread = 12; public static string tipoLetraSpread = "Microsoft Sans Serif";
        public static int alturaEncabezadosGrandeSpread = 45; public static int alturaEncabezadosChicoSpread = 35; public static int alturaFilasSpread = 25;
        // Variables generales.
        public static bool esConexionesVariasCorrecta = false; public static bool esConexionPrincipalCorrecta = false;
        public static bool esCambioDirectorio = false; public static int idDirectorioSeleccionado;
        public bool tieneParametros = false; public bool tieneSesionActivada = false;
        public int idUsuarioSesion = 0; public int idModuloSesion = 0; public int idProgramaSesion = 0; public int idSubProgramaSesion = 0;
        public string nombreEstePrograma = string.Empty; public string nombreProgramaAbierto = string.Empty;
        public bool estaCerrando = false; public bool estaAbriendoPrograma = false;
        public string nombreEsteEquipo = System.Environment.MachineName;
        public bool esInicioSesion = true;
        public ProcessStartInfo ejecutarPrograma = new ProcessStartInfo();
        public int diasDePrueba = 15;
        // Estilos.
        public List<Entidades.Estilos> listaEstilos = new List<Entidades.Estilos>();
        // Variable de desarrollo.
        public bool esDesarrollo = false;

        public Principal()
        {
            InitializeComponent();
        }
        
        #region Eventos

        private void Principal_Load(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            Centrar();
            CargarNombrePrograma();
            AsignarTooltips();
            AsignarFocos();
            ConfigurarConexiones();
            this.Cursor = Cursors.Default;

        }

        private void Principal_Shown(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            if (!Principal.esConexionPrincipalCorrecta)
                btnCambiarDirectorio.Enabled = false;
            //if (!Principal.esConexionesVariasCorrecta)
            //{
            //    ApplicationExit();
            //}
            //else
            //{
            //CargarEstilos(); TODO. Pendiente.
            CargarEncabezados();
            CargarTitulosDirectorio();
            VerificarLicencia();
            this.txtUsuario.Focus();
            //}
            this.Cursor = Cursors.Default;

        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {

            Salir();

        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {

            this.estaCerrando = true;
            Desvanecer();

        }

        private void cuadro_Click(object sender, EventArgs e)
        {

            ValidarAbrirPrograma(sender);

        }

        private void cuadro_MouseEnter(object sender, EventArgs e)
        {

            EventoRatonEncimaCuadro(sender);

        }

        private void cuadro_MouseLeave(object sender, EventArgs e)
        {

            EventoRatonDejandoCuadro(sender);
             
        }

        private void etiquetaNombre_Click(object sender, EventArgs e)
        {

            Label objetoEtiqueta = new Label();
            objetoEtiqueta = (Label)sender;
            Control objetoControl = new Control();
            objetoControl = objetoEtiqueta.Parent;
            Panel objetoPanel = new Panel();
            objetoPanel = (Panel)objetoControl;
            ValidarAbrirPrograma(objetoPanel);

        }

        private void etiquetaNombre_MouseEnter(object sender, EventArgs e)
        {
             
            Label objetoEtiqueta = new Label(); 
            objetoEtiqueta = (Label)sender;
            Control objetoControl = new Control();
            objetoControl = objetoEtiqueta.Parent;
            Panel objetoPanel = new Panel();
            objetoPanel = (Panel)objetoControl;
            EventoRatonEncimaCuadro(objetoPanel);

        }

        private void etiquetaNombre_MouseLeave(object sender, EventArgs e)
        {

            Label objetoEtiqueta = new Label();
            objetoEtiqueta = (Label)sender;
            Control objetoControl = new Control();
            objetoControl = objetoEtiqueta.Parent;
            Panel objetoPanel = new Panel();
            objetoPanel = (Panel)objetoControl;
            EventoRatonDejandoCuadro(objetoPanel);

        }

        private void btnCambiarDirectorio_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            if (Principal.esConexionesVariasCorrecta) 
                GuardarEditarRegistro(0, false);
            new AdministrarDirectorios().Show();
            this.Hide();
            this.Cursor = Cursors.Default;

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

        private void btnEntrar_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips("Entrar.");

        }

        private void btnSalir_MouseEnter(object sender, EventArgs e)
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
         
        private void pnlEncabezado_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty); 

        }

        private void pnlContenido_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty); 

        }

        private void pnlMenu_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty); 

        }

        private void pnlIniciarSesion_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty); 

        }

        private void pnlPie_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty); 

        }

        private void temporizador_Tick(object sender, EventArgs e)
        {

            if (this.estaCerrando)
            {
                Desvanecer();
            }

        }
         
        private void btnAyuda_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips("Ayuda.");

        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {

            MostrarAyuda();

        }

        private void btnCambiarDirectorio_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips("Cambiar de Directorio.");

        }

        private void btnRegresarMenu_MouseEnter(object sender, EventArgs e)
        {

            string nombre = "Regresar Menú a listado de "; string nombre2 = string.Empty;
            if (this.nivelMenu == (int)Nivel.Modulos)
                nombre = "Regresar en el Menú.";
            else if (this.nivelMenu == (int)Nivel.Programas)
                nombre2 = "Módulos.";
            else if (this.nivelMenu == (int)Nivel.SubProgramas)
                nombre2 = "Programas.";
            AsignarTooltips(nombre + nombre2);

        }

        private void btnRegresarMenu_Click(object sender, EventArgs e)
        {

            if (this.nivelMenu == (int)Nivel.Programas)
                this.nivelMenu = (int)Nivel.Modulos;
            else if (this.nivelMenu == (int)Nivel.SubProgramas)
                this.nivelMenu = (int)Nivel.Programas; 
            GenerarMenus();

        }

        #endregion

        #region Métodos

        private void VerificarLicencia()
        {

            List<Entidades.Licencia> lista = new List<Entidades.Licencia>();
            lista = licencia.ObtenerListado();
            if (lista.Count > 0)
            {
                if (lista[0].EsPrueba)
                {
                    DateTime registro; DateTime vencimiento;
                    if (DateTime.TryParse(lista[0].FechaRegistro.ToString(), out registro) == true)
                    { 
                        if (DateTime.TryParse(lista[0].FechaVencimiento.ToString(), out vencimiento) == true) 
                        {
                            //vencimiento = Convert.ToDateTime("22/04/2017");
                            if (DateTime.Today.CompareTo(vencimiento) > 0) // Si la fecha de vencimiento es menor a la fecha actual.
                            {
                                MessageBox.Show("La versión de prueba ha terminado. Contacte a su proveedor de este sistema para obtener una licencia.", "Versión de prueba.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                SalirOVolver();
                            }
                            else // Se cargan los dias restantes.
                            {
                                lblEncabezadoLicencia.Text = "Dias restantes: " + Math.Abs((DateTime.Today - vencimiento).TotalDays);
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Este programa no se encuentra registrado. Si desea, puede activar la versión de prueba dando clic en aceptar/si.", "Versión de prueba.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            licencia.EsPrueba = true;
                            licencia.FechaRegistro = DateTime.Today;
                            licencia.FechaVencimiento = DateTime.Today.AddDays(this.diasDePrueba);
                            licencia.Eliminar();
                            licencia.Guardar();
                            MessageBox.Show("Activación de versión de prueba de " + this.diasDePrueba + " dias correcta.", "Versión de prueba.", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                        else
                        {
                            SalirOVolver();
                        }
                    }
                }
            }
            else
            {
                if (MessageBox.Show("Este programa no se encuentra registrado. Active la versión de prueba dando clic en aceptar/si.", "Versión de prueba.", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    licencia.EsPrueba = true;
                    licencia.FechaRegistro = DateTime.Today;
                    licencia.FechaVencimiento = DateTime.Today.AddDays(this.diasDePrueba);
                    licencia.Eliminar();
                    licencia.Guardar();
                    MessageBox.Show("Activación de versión de prueba de " + this.diasDePrueba + " dias correcta.", "Versión de prueba.", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    SalirOVolver();
                }
            }

        }

        private void ApplicationExit()
        {

            Application.Exit();
        
        }

        private void Salir() 
        {

            this.Cursor = Cursors.WaitCursor; 
            if (this.estaAbriendoPrograma)
            {
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                if (Principal.esConexionesVariasCorrecta) 
                    GuardarEditarRegistro(0, false); 
            }
            ApplicationExit();
            this.Cursor = Cursors.Default;

        }

        private void SalirOVolver() 
        {

            this.Cursor = Cursors.WaitCursor; 
            if (Principal.esConexionesVariasCorrecta) 
                GuardarEditarRegistro(0, false); 
            if (this.esInicioSesion)
            {
                ApplicationExit();
            }
            else
            {
                pnlContenido.BackgroundImage = global::PrincipalBerry.Properties.Resources.Logo3;
                pnlContenido.BackgroundImageLayout = ImageLayout.Zoom;
                pnlContenido.BackColor = Color.DarkSlateGray;
                pnlMenu.Visible = false;
                btnRegresarMenu.Visible = false;
                pnlIniciarSesion.Visible = true; 
                txtContraseña.Text = string.Empty;
                txtUsuario.Text = string.Empty;
                txtUsuario.Focus();
                this.esInicioSesion = true;
            }
            this.Cursor = Cursors.Default;

        }

        private void MostrarAyuda()
        {

            this.Cursor = Cursors.WaitCursor;  
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
            if (!pnlAyuda.Visible)
            {
                if (this.esInicioSesion)
                {
                    pnlIniciarSesion.Visible = false; Application.DoEvents(); 
                }
                else
                {
                    pnlMenu.Visible = false; Application.DoEvents();
                    btnRegresarMenu.Visible = false;
                }
                pnlAyuda.Visible = true; Application.DoEvents();
                pnlAyuda.Size = pnlMenu.Size; Application.DoEvents();
                pnlAyuda.Location = pnlMenu.Location; Application.DoEvents();
                pnlContenido.Controls.Add(pnlAyuda); Application.DoEvents();
                txtAyuda.ScrollBars = ScrollBars.Both; Application.DoEvents();
                txtAyuda.Multiline = true; Application.DoEvents();
                txtAyuda.Width = pnlAyuda.Width - 10; Application.DoEvents();
                txtAyuda.Height = pnlAyuda.Height - 10; Application.DoEvents();
                txtAyuda.Location = new Point(5, 5); Application.DoEvents();
                txtAyuda.Text = "Sección de Ayuda: " + System.Environment.NewLine + System.Environment.NewLine + "* Iniciar Sesión: " + System.Environment.NewLine + "En esta parte se capturarán los datos de usuario y contraseña. " + System.Environment.NewLine + "Se le puede dar enter para avanzar, primero en usuario, luego en contraseña y despues de otro enter iniciará sesión, o en su caso darle clic al botón de la flecha. " + System.Environment.NewLine + System.Environment.NewLine + "* Menú: " + System.Environment.NewLine + "Una vez iniciado sesión, en este apartado aparecen todos los programas en un color distinto y aleatorio. " + System.Environment.NewLine + "Para abrir un programa simplemente hay que darle clic en la opción correspondiente y esperar a que se muestre. Dependiendo los permisos de usuario se podrá acceder o no."+ System.Environment.NewLine + System.Environment.NewLine + "* Cambiar directorio: " + System.Environment.NewLine + "En la parte inferior izquierda se encuentra un botón con el cual se puede cambiar de directorio, el cual es practicamente el ambiente donde se encuentra trabajando. Cada directorio tiene su propia información completamente independiente."; Application.DoEvents();
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
                    pnlMenu.Visible = true; Application.DoEvents();
                    btnRegresarMenu.Visible = true;
                }
                pnlAyuda.Visible = false; Application.DoEvents();
            }
            this.Cursor = Cursors.Default;

        } 

        private void Desvanecer()
        {
             
            // Se ponen en color gris todos los controles de esta pantalla.
            foreach (Control c in this.Controls)
            {
                if (c.BackColor != Color.Gray)
                c.BackColor = Color.Gray;
                c.Parent.BackColor = Color.Gray; 
            }
            Application.DoEvents();
            // Se comienza a desaparecer el programa gradualmente.
            temporizador.Interval = 10;
            temporizador.Enabled = true;
            temporizador.Start();
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.25; Application.DoEvents();
            }
            else
            {
                temporizador.Enabled = false;
                temporizador.Stop();
            } 

        }
         
        private void ValidarSesion()
        { 

            if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtContraseña.Text))
            {
                if (txtUsuario.Text.ToUpper().Equals("Admin".ToUpper()))
                {
                    if (txtContraseña.Text.Equals("@berry2017"))
                    {
                        PanelControl.nombreDirectorio = Logica.Directorios.nombre;
                        new PanelControl().Show();
                        this.Hide();
                        }
                    else
                    {
                        txtContraseña.Clear();
                        txtContraseña.Focus();
                    }
                }
                else
                {
                    if (!Principal.esConexionPrincipalCorrecta || !Principal.esConexionesVariasCorrecta)
                    {
                        txtUsuario.Clear();
                        txtContraseña.Clear();
                        txtUsuario.Focus();
                        return; 
                    }
                    usuarios.Nombre = txtUsuario.Text; 
                    List<Entidades.Usuarios> lista = usuarios.ObtenerListadoPorNombre();
                    if (lista.Count > 0)
                    {
                        if (txtContraseña.Text.Equals(lista[0].Contrasena))
                        {
                            // Se guarda registro de los equipos.
                            GuardarEditarRegistro(lista[0].Id, true);
                            PermitirAcceso(lista[0].Id);
                        }
                        else
                        {
                            if (lista[0].Id.Equals(string.Empty))
                            {
                                MessageBox.Show("Usuario inexistente en este directorio.", "Datos incorrectos.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtUsuario.Clear();
                                txtContraseña.Clear();
                                txtUsuario.Focus();
                            }
                            else
                            {
                                txtContraseña.Clear();
                                txtContraseña.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario inexistente en este directorio.", "Datos incorrectos.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUsuario.Clear();
                        txtContraseña.Clear();
                        txtUsuario.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Faltan datos.", "Faltan datos.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUsuario.Focus(); 
            }

        }
         
        private void GuardarEditarRegistro(int idUsuario, bool esSesionIniciada)
        {

            string nombreEquipo = this.nombreEsteEquipo; 
            if (!string.IsNullOrEmpty(nombreEquipo))
            { 
                registros.IdUsuario = idUsuario; 
                registros.NombreEquipo = nombreEquipo;
                registros.EsSesionIniciada = esSesionIniciada;
                bool tieneRegistro = registros.ValidarPorNombreEquipo();
                if (tieneRegistro)
                {
                    if (esSesionIniciada)
                    {
                        registros.Editar();
                    }
                    else
                    {
                        registros.EditarSoloSesion();
                    }
                }
                else
                {
                    registros.Guardar();
                }
            }

        }

        private void PermitirAcceso(int idUsuario)
        {

            pnlIniciarSesion.Visible = false; Application.DoEvents(); 
            pnlMenu.Visible = true; Application.DoEvents();
            btnRegresarMenu.Visible = true;
            this.esInicioSesion = false; 
            this.idUsuarioSesion = idUsuario;
            ConsultarInformacionUsuario(this.idUsuarioSesion);
            CargarEncabezados();
            CargarTitulosDirectorio();
            this.nivelMenu = (int)Nivel.Modulos;
            GenerarMenus(); // Siempre se muestra el menú de módulos.

        }

        private void Centrar()
        {

            this.CenterToScreen();
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Opacity = .98; // Está bien perro esto. Un toque de elegancia.

        }

        private void CargarNombrePrograma()
        {

            this.nombreEstePrograma = this.Text;

        }

        private void AsignarTooltips()
        {

            ToolTip tp = new ToolTip();
            tp.AutoPopDelay = 5000;
            tp.InitialDelay = 0;
            tp.ReshowDelay = 100;
            tp.ShowAlways = true;
            tp.SetToolTip(this.pnlEncabezado, "Datos Principales.");
            tp.SetToolTip(this.btnCambiarDirectorio, "Cambiar de Directorio.");
            tp.SetToolTip(this.btnEntrar, "Entrar.");
            tp.SetToolTip(this.btnSalir, "Salir.");
            tp.SetToolTip(this.btnAyuda, "Ayuda.");
            tp.SetToolTip(this.btnRegresarMenu, "Regresar en el Menú.");

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
            }
            else
            {
                string ruta = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                ruta = ruta.Replace("file:\\", null);
                baseDatos.CadenaConexionPrincipal = string.Format("{0}\\DatosPrincipales.sdf", ruta); 
            }
            baseDatos.ConfigurarConexionPrincipal();
        
        }

        public void ConfigurarConexiones() 
        {
 
            // Se verifica si tiene parametros.
	        string[] parametros = Environment.GetCommandLineArgs().ToArray();
	        if (parametros.Length > 1)
            {
		        this.tieneParametros = true;
	        } 
            ConfigurarConexionPrincipal(); 
            if (this.tieneParametros)
            {
                Logica.Directorios.ObtenerParametros(); // Se obtienen los parametros.
                Logica.Usuarios.ObtenerParametros(); // Se obtienen los parametros.
                Principal.esConexionPrincipalCorrecta = true; // Se supone que debe ser una conexión correcta si tiene parametros.
            }
            else
            {
                if (Principal.esCambioDirectorio) // Si viene de cambiar el directorio, se toma el id que seleccionó, de lo contrario, se carga el predeterminado.
                {
                    ConsultarInformacionDirectorioPorId(Principal.idDirectorioSeleccionado);
                }
                else
                { 
                    ConsultarInformacionDirectorioPredeterminado();
                }
            }
            if (!Principal.esConexionPrincipalCorrecta)
            {
                Principal.esConexionesVariasCorrecta = true;
                return;
            }
            baseDatos.CadenaConexionConfiguracion = "Configuracion" + Logica.Directorios.id; 
            baseDatos.CadenaConexionCatalogo = "Catalogo" + Logica.Directorios.id;
            baseDatos.CadenaConexionAlmacen = "Almacen" + Logica.Directorios.id;
            baseDatos.ConfigurarConexionConfiguracion();
            baseDatos.ConfigurarConexionCatalogo();
            baseDatos.ConfigurarConexionAlmacen();
            string resultado = string.Empty;
            resultado = baseDatos.ProbarConexion(Entidades.BaseDatos.conexionConfiguracion); // Se valida la conexión.
            if (!string.IsNullOrEmpty(resultado))
            {
                MostrarMensaje(resultado);
                Principal.esConexionesVariasCorrecta = false;
                return;
            }
            resultado = baseDatos.ProbarConexion(Entidades.BaseDatos.conexionCatalogo); // Se valida la conexión.
            if (!string.IsNullOrEmpty(resultado))
            {
                MostrarMensaje(resultado);
                Principal.esConexionesVariasCorrecta = false;
                return;
            }
            resultado = baseDatos.ProbarConexion(Entidades.BaseDatos.conexionAlmacen); // Se valida la conexión.
            if (!string.IsNullOrEmpty(resultado))
            {
                MostrarMensaje(resultado);
                Principal.esConexionesVariasCorrecta = false;
                return;
            }
            Principal.esConexionesVariasCorrecta = true; 
            if (this.tieneParametros) // Se permite el acceso ya que con los parametros se sabe cuales son sus datos.
            {
                PermitirAcceso(Logica.Usuarios.id);
            }
            else // Si no tiene parametros solo se carga normal.
            { 
                // Se pregunta si tiene sesión iniciada, para proceder a cargar su información. 
                registros.NombreEquipo = this.nombreEsteEquipo; 
                bool tieneSesion = registros.ValidarSesionPorNombreEquipo(); 
                if (tieneSesion)
                { 
                    // Se obtienen registros de lo que corresponda a este directorio y este nombre de equipo.
                    ObtenerRegistroPorNombreEquipo();
                    PermitirAcceso(Logica.Usuarios.id); 
                } 
            }

        }

        private void MostrarMensaje(string resultado)
        {

            MessageBox.Show("No se pudo abrir la base de datos. Si no existe hay que crearla. " + Environment.NewLine + Environment.NewLine + resultado, "Error de conexión.", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void ObtenerRegistroPorNombreEquipo()
        {

            List<Entidades.Registros> lista = new List<Entidades.Registros>();
            registros.NombreEquipo = this.nombreEsteEquipo;
            lista = registros.ObtenerListadoPorNombreEquipo(); 
            if (lista.Count > 0)
            {
                ConsultarInformacionUsuario(lista[0].IdUsuario); 
            }

        }

        private void ConsultarInformacionDirectorioPorId(int id)
        {

            directorios.Id = id;
            List<Entidades.Directorios> lista = new List<Entidades.Directorios>();
            lista = directorios.ObtenerListadoPorId();
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
                Principal.esConexionPrincipalCorrecta = true;
            }
            else
            {
                MessageBox.Show("No existe información en la base de datos principal.", "Faltan datos.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Principal.esConexionPrincipalCorrecta = false;
            }

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
                Principal.esConexionPrincipalCorrecta = true;
            }
            else
            {
                MessageBox.Show("No existe información en la base de datos principal.", "Faltan datos.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Principal.esConexionPrincipalCorrecta = false;
            }

        }
         
        public void ConsultarInformacionUsuario(int idUsuario)
        {
             
            usuarios.Id = idUsuario;
            List<Entidades.Usuarios> lista = new List<Entidades.Usuarios>();
            lista = usuarios.ObtenerListadoPorId();
            if (lista.Count > 0)
            { 
                Logica.Usuarios.id = lista[0].Id;
                Logica.Usuarios.nombre = lista[0].Nombre;
                Logica.Usuarios.contrasena = lista[0].Contrasena;
                Logica.Usuarios.nivel = lista[0].Nivel;
                Logica.Usuarios.accesoTotal = lista[0].AccesoTotal; 
            }

        } 

        private void EventoRatonEncimaCuadro(object sender)
        {

            try
            {
                string nombre = ((Panel)sender).Name;
                Panel objetoPanel = new Panel();
                objetoPanel = (Panel)(pnlMenu.Controls[nombre]);
                objetoPanel.BorderStyle = BorderStyle.Fixed3D;
                this.colorCuadroOriginal = objetoPanel.BackColor;
                objetoPanel.BackColor = ControlPaint.Dark(objetoPanel.BackColor);
            }
            catch (Exception)
            {  
            }

        }

        private void EventoRatonDejandoCuadro(object sender)
        {

            try
            {
                string nombre = ((Panel)sender).Name;
                Panel objetoPanel = new Panel();
                objetoPanel = (Panel)(pnlMenu.Controls[nombre]);
                objetoPanel.BorderStyle = BorderStyle.FixedSingle;
                objetoPanel.BackColor = this.colorCuadroOriginal; 
            }
            catch (Exception)
            {
            }

        }

        private void ValidarAbrirPrograma(object sender)
        {

            this.Cursor = Cursors.WaitCursor;
            string nombre = ((Panel)sender).Name;
            string[] nombres = nombre.Split('_');
            int idModulo = 0;
            int idPrograma = 0;
            int idSubPrograma = 0;
            if (this.nivelMenu == (int)Nivel.Modulos)
            {
                idModulo = Convert.ToInt32(nombres[1]);;
                idPrograma = 0;
                idSubPrograma = 0;
            }
            else if (this.nivelMenu == (int)Nivel.Programas)
            {
                idModulo = Convert.ToInt32(nombres[1]);
                idPrograma = Convert.ToInt32(nombres[2]);
                idSubPrograma = 0;
            }
            else if (this.nivelMenu == (int)Nivel.SubProgramas)
            {
                idModulo = Convert.ToInt32(nombres[1]);
                idPrograma = Convert.ToInt32(nombres[2]);
                idSubPrograma = Convert.ToInt32(nombres[3]);
            }
            this.idModuloSesion = idModulo;
            this.idProgramaSesion = idPrograma;
            this.idSubProgramaSesion = idSubPrograma;
            List<Entidades.Modulos> lista = new List<Entidades.Modulos>();
            modulos.Id = this.idModuloSesion;
            lista = modulos.ObtenerListadoPorId();
            string nombreModulo = lista[0].Prefijo;
            string nombrePrograma = string.Empty;
            if (this.nivelMenu == (int)Nivel.Modulos)
            {
                this.nivelMenu = (int)Nivel.Programas;
                GenerarMenus(); 
            }
            else if (this.nivelMenu == (int)Nivel.Programas)
            { 
                subProgramas.IdModuloo = this.idModuloSesion;
                subProgramas.IdProgramaa = this.idProgramaSesion;
                bool resultado = subProgramas.ValidarPorIdModuloyIdPrograma();
                if (resultado)
                {
                    this.nivelMenu = (int)Nivel.SubProgramas;
                    GenerarMenus();
                }
                else
                {
                    nombrePrograma = nombreModulo + idPrograma.ToString().PadLeft(2, '0'); 
                    AbrirPrograma(nombrePrograma, true);  
                }
            }
            else if (this.nivelMenu == (int)Nivel.SubProgramas)
            {
                nombrePrograma = nombreModulo + idPrograma.ToString().PadLeft(2, '0') + idSubPrograma.ToString().PadLeft(2, '0'); 
                AbrirPrograma(nombrePrograma, true);
            }
            this.Cursor = Cursors.Default;

        }
         
        private void GenerarMenus() // Los niveles son: 1 modulos, 2 programas y 3 subprogramas.
        {

            this.Cursor = Cursors.WaitCursor;
            //CargarEstilos(); TODO. Pendiente.
            // Se limpia siempre. 
            pnlMenu.Controls.Clear(); Application.DoEvents();
            // Se generan las opciones de menú.
            List<Entidades.Modulos> listaModulos = new List<Entidades.Modulos>();
            List<Entidades.Programas> listaProgramas = new List<Entidades.Programas>();
            List<Entidades.SubProgramas> listaSubProgramas = new List<Entidades.SubProgramas>(); 
            // Esto corresponde al desglose de menú.
            if (this.nivelMenu == (int)Nivel.Modulos)
            {
                modulos.IdUsuario = this.idUsuarioSesion;
                if (Logica.Usuarios.nivel == 1)
                    listaModulos = modulos.ObtenerMenuListado();
                else
                    listaModulos = modulos.ObtenerListado();
            }
            else if (this.nivelMenu == (int)Nivel.Programas)
            {
                programas.IdModulo = this.idModuloSesion;
                programas.IdUsuario = this.idUsuarioSesion;
                listaProgramas = programas.ObtenerMenuListado();
            }
            else if (this.nivelMenu == (int)Nivel.SubProgramas)
            {
                subProgramas.IdModuloo = this.idModuloSesion;
                subProgramas.IdProgramaa = this.idProgramaSesion;
                subProgramas.IdUsuario = this.idUsuarioSesion;
                listaSubProgramas = subProgramas.ObtenerMenuListado();
            } 
            // Se calculan los controles necesarios.
            int alto = 0; int ancho = 0; // Los tamaños de los controles.
            if (this.nivelMenu == (int)Nivel.Modulos)
            {
                alto = 237; ancho = 475;
            }
            else if (this.nivelMenu == (int)Nivel.Programas || this.nivelMenu == (int)Nivel.SubProgramas)
            {
                alto = 190; ancho = 380; 
            } 
            int posicionY = 0; int posicionX = 0; // Las posiciones donde inician los controles.
            int margen = 5; // Margen de espacio hacia los lados
            double cantidadEnAltura = Convert.ToDouble(pnlMenu.Height) / (alto + margen + 10); // Tamaño de menu entre alto de paneles mas margenes mas 10 de la barra inferior.
            cantidadEnAltura = Math.Floor(cantidadEnAltura); // Es la cantidad de controles que caben verticalmente.
            int cantidad = 0; // Cantidad de programas configurados obtenidos desde bd.
            if (this.nivelMenu == (int)Nivel.Modulos)
                cantidad = listaModulos.Count;
            else if (this.nivelMenu == (int)Nivel.Programas)
                cantidad = listaProgramas.Count;
            else if (this.nivelMenu == (int)Nivel.SubProgramas)
                cantidad = listaSubProgramas.Count; 
            int indiceVariable = 0; // Se utiliza para controlar la cantidad de opciones verticales.
            for (int indice = 0; indice < cantidad; indice++) // Crea todos los controles.
            {
                // Se crean los paneles.
                Panel cuadro = new Panel();
                cuadro.Size = new Size(ancho, alto);
                cuadro.Top = posicionY;
                cuadro.Left = posicionX;
                cuadro.BorderStyle = BorderStyle.FixedSingle;
                // Se cargan los estilos. TODO. Pendiente.
                //string colorFondoUsuario = string.Empty;
                //if (this.listaEstilos.Count == 1) 
                //{
                //    colorFondoUsuario = listaEstilos[0].ColorFondoMenu;
                //}
                //if (colorFondoUsuario.ToUpper().Equals("Random".ToUpper()) || string.IsNullOrEmpty(colorFondoUsuario))
                //{
                cuadro.BackColor = ObtenerColorAleatorio(); 
                //}
                //else
                //{
                    //cuadro.BackColor = Color.FromName(colorFondoUsuario);
                //}
                System.Threading.Thread.Sleep(70);
                if (this.nivelMenu == (int)Nivel.Modulos)
                    cuadro.Name = "pnlPrograma_" + listaModulos[indice].Id;
                else if (this.nivelMenu == (int)Nivel.Programas)
                    cuadro.Name = "pnlPrograma_" + listaProgramas[indice].IdModulo + "_" + listaProgramas[indice].Id;
                else if (this.nivelMenu == (int)Nivel.SubProgramas)
                    cuadro.Name = "pnlPrograma_" + listaSubProgramas[indice].IdModuloo + "_" + listaSubProgramas[indice].IdProgramaa + "_" + listaSubProgramas[indice].Id;
                cuadro.Click += new System.EventHandler(cuadro_Click); // Se genera el evento desde código. 
                cuadro.MouseEnter += new System.EventHandler(cuadro_MouseEnter); // Se genera el evento desde código.
                cuadro.MouseLeave += new System.EventHandler(cuadro_MouseLeave); // Se genera el evento desde código.
                cuadro.Cursor = Cursors.Hand;
                pnlMenu.Controls.Add(cuadro); Application.DoEvents();
                // Se crean las etiquetas de los nombres de los paneles.
                Label etiquetaNombre = new Label();
                etiquetaNombre.Width = ancho;
                string nombre = string.Empty;
                if (this.nivelMenu == (int)Nivel.Modulos)
                    nombre = listaModulos[indice].Nombre.ToString();
                else if (this.nivelMenu == (int)Nivel.Programas)
                    nombre = listaProgramas[indice].Nombre.ToString();
                else if (this.nivelMenu == (int)Nivel.SubProgramas)
                    nombre = listaSubProgramas[indice].Nombre.ToString();
                if (nombre.Length > 24)
                {
                    if (this.nivelMenu == (int)Nivel.Programas || this.nivelMenu == (int)Nivel.SubProgramas)
                    {
                        etiquetaNombre.Top = cuadro.Height - etiquetaNombre.Height - 45;
                        etiquetaNombre.Height = 80;
                    }
                }
                else
                {
                    etiquetaNombre.Top = cuadro.Height - etiquetaNombre.Height - 15;
                    etiquetaNombre.Height = 40;
                }
                etiquetaNombre.BorderStyle = BorderStyle.None;
                etiquetaNombre.Left = 0;
                etiquetaNombre.Text = nombre;
                // Se cargan los estilos. TODO. Pendiente.
                //string colorLetraUsuario = "White";
                //if (this.listaEstilos.Count == 1)
                //{
                //    colorLetraUsuario = listaEstilos[0].ColorLetraMenu;
                //}
                //etiquetaNombre.ForeColor = Color.FromName(colorLetraUsuario);
                etiquetaNombre.ForeColor = Color.White;
                etiquetaNombre.Font = new Font(Principal.tipoLetraSpread, 20, FontStyle.Regular);
                etiquetaNombre.Click += new System.EventHandler(etiquetaNombre_Click); // Se genera el evento desde código. 
                etiquetaNombre.MouseEnter += new System.EventHandler(etiquetaNombre_MouseEnter); // Se genera el evento desde código.
                etiquetaNombre.MouseLeave += new System.EventHandler(etiquetaNombre_MouseLeave); // Se genera el evento desde código.
                etiquetaNombre.Cursor = Cursors.Hand;
                cuadro.Controls.Add(etiquetaNombre); Application.DoEvents();
                // Se calculan y se distribuyen de acuerdo al tamaño del panel del menú.
                indiceVariable += 1;
                if (indiceVariable < Convert.ToInt32(cantidadEnAltura)) // Se pinta sobre la misma columna vertical.
                {
                    posicionY += alto + margen;
                }
                else // Se pinta sobre una nueva columna vertical a la derecha de la actual.
                {
                    indiceVariable = 0;
                    posicionX += ancho + margen;
                    posicionY = 0;
                }
            }
            this.Cursor = Cursors.Default;

        }
        
        private Color ObtenerColorAleatorio() 
        {

            Random aleatorio = new Random();
            Color opcionColor = new Color();
            KnownColor[] nombres = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor nombreAleatorio = nombres[aleatorio.Next(nombres.Length)];
            opcionColor = Color.FromKnownColor(nombreAleatorio); 
            Color colorOscuro = ControlPaint.Dark(opcionColor); // Se oscurece el color elegido.
            return colorOscuro;

        }

        private void CargarEstilos()
        {

            estilos.IdUsuario = Logica.Usuarios.id;
            this.listaEstilos = estilos.ObtenerListado();
            if (this.listaEstilos.Count == 1)
            {
                pnlEncabezado.BackColor = Color.FromName(listaEstilos[0].ColorFondo);
                pnlPie.BackColor = Color.FromName(listaEstilos[0].ColorFondo);
            }

        }

        private void CargarTitulosDirectorio()
        {

            this.Text = "Programa:  " + this.nombreEstePrograma + "              Directorio:  " + Logica.Directorios.nombre + "              Usuario:  " + Logica.Usuarios.nombre;

        }

        private void CargarEncabezados()
        {
             
            lblEncabezadoPrograma.Text = "Programa: " + this.nombreEstePrograma;
            lblEncabezadoDirectorio.Text = "Directorio: " + Logica.Directorios.nombre;
            lblEncabezadoUsuario.Text = "Usuario: " + Logica.Usuarios.nombre;
            Application.DoEvents();

        }

        private void CerrarPrograma(string nombre)
        {

            Process[] myProcesses;
            myProcesses = Process.GetProcessesByName(nombre);
            foreach (Process myProcess in myProcesses)
            { 
                myProcess.Kill();
            }

        }

        private void AbrirPrograma(string nombre, bool salir)
        {

            this.estaAbriendoPrograma = true;
            this.nombreProgramaAbierto = nombre;
            ejecutarPrograma.UseShellExecute = true;
            ejecutarPrograma.FileName = nombre + ".exe";
            ejecutarPrograma.WorkingDirectory = Directory.GetCurrentDirectory();
            ejecutarPrograma.Arguments = Logica.Directorios.id.ToString().Trim().Replace(" ", "|") + " " + Logica.Directorios.nombre.ToString().Trim().Replace(" ", "|") + " " + Logica.Directorios.descripcion.ToString().Trim().Replace(" ", "|") + " " + Logica.Directorios.rutaLogo.ToString().Trim().Replace(" ", "|") + " " + Logica.Directorios.esPredeterminado.ToString().Trim().Replace(" ", "|") + " " + Logica.Directorios.instanciaSql.ToString().Trim().Replace(" ", "|") + " " + Logica.Directorios.usuarioSql.ToString().Trim().Replace(" ", "|") + " " + Logica.Directorios.contrasenaSql.ToString().Trim().Replace(" ", "|") + " " + "Aquí terminan los de directorios, indice 9 ;)".Replace(" ", "|") + " " + Logica.Usuarios.id.ToString().Trim().Replace(" ", "|") + " " + "Aquí terminan los de usuario, indice 11 ;)".Replace(" ", "|");
            try
            {
                var proceso = Process.Start(ejecutarPrograma);
                proceso.WaitForInputIdle();
                if (salir)
                { 
                    if (this.ShowIcon)
                    {
                        this.ShowIcon = false;
                    }
                    ApplicationExit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puede abrir el programa principal en la ruta : " + ejecutarPrograma.WorkingDirectory + "\\" + nombre + Environment.NewLine + Environment.NewLine + ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        #region Nivel

        public enum Nivel
        {
             
            Modulos = 1,
            Programas = 2,
            SubProgramas = 3

        }

        #endregion
        
    }
}