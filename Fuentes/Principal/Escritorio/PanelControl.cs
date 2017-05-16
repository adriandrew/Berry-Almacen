using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Escritorio
{
    public partial class PanelControl : Form
    {

        FarPoint.Win.Spread.CellType.TextCellType tipoTexto = new FarPoint.Win.Spread.CellType.TextCellType();
        FarPoint.Win.Spread.CellType.TextCellType tipoTextoContrasena = new FarPoint.Win.Spread.CellType.TextCellType();
        FarPoint.Win.Spread.CellType.NumberCellType tipoEntero = new FarPoint.Win.Spread.CellType.NumberCellType();
        FarPoint.Win.Spread.CellType.NumberCellType tipoDoble = new FarPoint.Win.Spread.CellType.NumberCellType();
        FarPoint.Win.Spread.CellType.PercentCellType tipoPorcentaje = new FarPoint.Win.Spread.CellType.PercentCellType();
        FarPoint.Win.Spread.CellType.DateTimeCellType tipoHora = new FarPoint.Win.Spread.CellType.DateTimeCellType();
        FarPoint.Win.Spread.CellType.CheckBoxCellType tipoBooleano = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
        Entidades.Directorios directorios = new Entidades.Directorios();
        Entidades.Usuarios usuarios = new Entidades.Usuarios();
        Entidades.Modulos modulos = new Entidades.Modulos();
        Entidades.Programas programas = new Entidades.Programas();
        Entidades.SubProgramas subProgramas = new Entidades.SubProgramas();
        Entidades.BloqueoUsuarios bloqueoUsuarios = new Entidades.BloqueoUsuarios();
        public int opcionSeleccionada = 0;
        public static string nombreDirectorio = string.Empty;
        public int filaModulosDeProgramas = -1;
        public int filaProgramasDeSubProgramas = -1;
        // Variables de tamaños y posiciones de spreads.
        public int anchoTotal = 0; public int altoTotal = 0;
        public int anchoMitad = 0; public int altoMitad = 0;
        public int anchoTercio = 0;
        public int izquierda = 0; public int arriba = 0;
        
        public PanelControl()
        {
            InitializeComponent();
        }

        #region Eventos

        private void PanelControl_Load(object sender, EventArgs e)
        {

            Centrar();
            AsignarTooltips(); 
            CargarEncabezados();
            CargarMedidas();

        }

        private void PanelControl_Shown(object sender, EventArgs e)
        {

            FormatearSpread();
            OcultarSpreads();

        }

        private void rbtnUsuarios_CheckedChanged(object sender, EventArgs e)
        {

            if (rbtnUsuarios.Checked)
            {
                this.opcionSeleccionada = (int)TipoControl.Usuarios;
                CargarUsuarios();
            } 

        }

        private void rbtnProgramas_CheckedChanged(object sender, EventArgs e)
        {

            if (rbtnProgramas.Checked)
            {
                this.opcionSeleccionada = (int)TipoControl.Programas;
                CargarProgramasyModulos();
            } 

        }

        private void spUsuarios_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.F5) // Abrir catalogo de areas.
            {
                //if ((spUsuarios.ActiveSheet.ActiveColumnIndex == spUsuarios.ActiveSheet.Columns["idArea"].Index) || (spUsuarios.ActiveSheet.ActiveColumnIndex == spUsuarios.ActiveSheet.Columns["nombreArea"].Index))
                //{
                //    spUsuarios.Enabled = false;
                //    FormatearSpreadCatalogoAreas();
                //    spCatalogos.Focus();
                //}
            } 
            else if (e.KeyData == Keys.F6) // Eliminar.
            {
                if (this.opcionSeleccionada == (int)TipoControl.Usuarios)
                {
                    if (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    { 
                        //int fila = spUsuarios.ActiveSheet.ActiveRowIndex;
                        //int idUsuario = Logica.Funciones.ValidarNumero(spUsuarios.ActiveSheet.Cells[fila, spUsuarios.ActiveSheet.Columns["id"].Index].Text); 
                        //usuarios.Id = idUsuario;
                        //bool tieneDatos = usuarios.ValidarActividadPorId();
                        //if (tieneDatos)
                        //{
                        //    MessageBox.Show("No se puede eliminar este registro, ya que contiene actividades capturadas.", "No permitido.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //}
                        //else
                        //{
                            EliminarRegistro(spUsuarios);
                        //}
                    }
                }
            }
            else if (e.KeyData == Keys.Enter) // Validar.
            {
                ControlarSpreadEnter2(spUsuarios); 
            }

        }
        
        private void PanelControl_FormClosed(object sender, FormClosedEventArgs e)
        {

            new Principal().Show();

        }
  
        private void btnSalir_Click(object sender, EventArgs e)
        {

            this.Dispose();
            new Principal().Show();            

        }

        private void spUsuarios_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            if (this.opcionSeleccionada == (int)TipoControl.Usuarios)
            {
                int fila = e.Row;
                spUsuarios.ActiveSheet.ActiveRowIndex = fila;
                int valorCelda = Convert.ToInt32(spUsuarios.ActiveSheet.Cells[fila, spUsuarios.ActiveSheet.Columns["nivel"].Index].Value);
                spUsuarios.Width = this.anchoTotal;
                if (valorCelda == 0)
                {
                    OcultarSpreads();
                    spUsuarios.Visible = true;
                    Application.DoEvents();
                    spUsuarios.Height = this.altoTotal;
                }
                else if (valorCelda == 1) // Nivel de bloqueo de los módulos.
                {
                    OcultarSpreads();
                    spUsuarios.Visible = true;
                    spModulos.Visible = true;
                    Application.DoEvents();
                    spUsuarios.Height = this.altoMitad;
                    spModulos.Height = this.altoMitad;
                    spModulos.Width = this.anchoTotal;
                    spModulos.Top = this.altoMitad + pnlMenu.Height;
                    spModulos.Left = this.izquierda;
                    CargarModulosDeUsuario(true);
                }
                else if (valorCelda == 2) // Nivel de bloqueo de los programas.
                {
                    OcultarSpreads();
                    spUsuarios.Visible = true;
                    spModulos.Visible = true;
                    spProgramas.Visible = true;
                    Application.DoEvents();
                    spUsuarios.Height = this.altoMitad;
                    spModulos.Height = this.altoMitad;
                    spModulos.Width = this.anchoMitad;
                    spModulos.Top = this.altoMitad + pnlMenu.Height;
                    spModulos.Left = this.izquierda;
                    CargarModulosDeUsuario(false); 
                    spProgramas.Height = this.altoMitad;
                    spProgramas.Width = this.anchoMitad;
                    spProgramas.Top = this.altoMitad + pnlMenu.Height;
                    spProgramas.Left = this.anchoMitad;
                    CargarProgramasDeUsuario();
                }
                else if (valorCelda == 3) // Nivel de bloqueo de los subprogramas. TODO. Pendiente.
                {
                    //spProgramas.Visible = true;
                    //spSubProgramas.Visible = true;
                } 
            }

        }

        private void spModulos_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            if (this.opcionSeleccionada == (int)TipoControl.Usuarios)
            {
                int fila = e.Row;
                spModulos.ActiveSheet.ActiveRowIndex = fila; Application.DoEvents();
                bool estatus = Convert.ToBoolean(spModulos.ActiveSheet.Cells[fila, spModulos.ActiveSheet.Columns["estatus"].Index].Value);
                estatus = ((estatus == true) ? false : true);
                if (estatus) // Guardar.
                {
                    GuardarBloqueoUsuariosHastaModulos();
                }
                else if (!estatus) // Eliminar.
                {
                    EliminarBloqueoUsuariosHastaModulos();
                }
                CargarModulosDeUsuario(true);
            }
            else if (this.opcionSeleccionada == (int)TipoControl.Programas || this.opcionSeleccionada == (int)TipoControl.SubProgramas)
            {
                int fila = e.Row;
                this.filaModulosDeProgramas = fila;
                spModulos.ActiveSheet.ActiveRowIndex = fila; Application.DoEvents();
                int idModulo = Convert.ToInt32(spModulos.ActiveSheet.Cells[fila, spModulos.ActiveSheet.Columns["id"].Index].Value);
                CargarProgramas(idModulo);
            }

        }
         
        private void spProgramas_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            if (this.opcionSeleccionada == (int)TipoControl.Usuarios)
            {
                int fila = e.Row;
                spProgramas.ActiveSheet.ActiveRowIndex = fila; Application.DoEvents();
                bool estatus = Convert.ToBoolean(spProgramas.ActiveSheet.Cells[fila, spProgramas.ActiveSheet.Columns["estatus"].Index].Value);
                estatus = ((estatus == true) ? false : true);
                if (estatus) // Guardar.
                {
                    GuardarBloqueoUsuariosHastaProgramas();
                }
                else if (!estatus) // Eliminar.
                {
                    EliminarBloqueoUsuariosHastaProgramas();
                }
                CargarModulosDeUsuario(false);
                CargarProgramasDeUsuario();
            }
            else if (this.opcionSeleccionada == (int)TipoControl.SubProgramas)
            { 
                int idModulo = Convert.ToInt32(spModulos.ActiveSheet.Cells[spModulos.ActiveSheet.ActiveRowIndex, spModulos.ActiveSheet.Columns["id"].Index].Value);
                int filaProgramas = e.Row;
                this.filaProgramasDeSubProgramas = filaProgramas;
                spProgramas.ActiveSheet.ActiveRowIndex = filaProgramas; Application.DoEvents();
                int idPrograma = Convert.ToInt32(spProgramas.ActiveSheet.Cells[filaProgramas, spProgramas.ActiveSheet.Columns["id"].Index].Value);
                CargarSubProgramas(idModulo, idPrograma);
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (this.opcionSeleccionada == (int)TipoControl.Directorios)
            {
                GuardarEditarDirectorios();
            }
            else if (this.opcionSeleccionada == (int)TipoControl.Usuarios)
            {
                GuardarEditarUsuarios();
            }
            else if (this.opcionSeleccionada == (int)TipoControl.Modulos)
            {
                GuardarEditarModulos();
            }
            else if (this.opcionSeleccionada == (int)TipoControl.Programas)
            {
                GuardarEditarProgramas();
            }
            else if (this.opcionSeleccionada == (int)TipoControl.SubProgramas)
            {
                GuardarEditarSubProgramas();
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (this.opcionSeleccionada == (int)TipoControl.Directorios)
                EliminarDirectorios();
            else if (this.opcionSeleccionada == (int)TipoControl.Usuarios)
                EliminarUsuarios();
            else if (this.opcionSeleccionada == (int)TipoControl.Modulos)
                EliminarModulos();
            else if (this.opcionSeleccionada == (int)TipoControl.Programas)
                EliminarProgramas();
            else if (this.opcionSeleccionada == (int)TipoControl.SubProgramas)
                EliminarSubProgramas();

        }

        private void spUsuarios_DialogKey(object sender, FarPoint.Win.Spread.DialogKeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                ControlarSpreadEnter2(spUsuarios);
            }

        }

        private void spCatalogos_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            //int fila = e.Row;
            //spUsuarios.ActiveSheet.Cells[spUsuarios.ActiveSheet.ActiveRowIndex, spUsuarios.ActiveSheet.Columns["idArea"].Index].Text = spCatalogos.ActiveSheet.Cells[fila, spCatalogos.ActiveSheet.Columns["idArea"].Index].Text;
            //spUsuarios.ActiveSheet.Cells[spUsuarios.ActiveSheet.ActiveRowIndex, spUsuarios.ActiveSheet.Columns["nombreArea"].Index].Text = spCatalogos.ActiveSheet.Cells[fila, spCatalogos.ActiveSheet.Columns["nombreArea"].Index].Text;

        }

        private void spCatalogos_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            //spUsuarios.Enabled = true;
            //spUsuarios.Focus();
            //spUsuarios.ActiveSheet.SetActiveCell(spUsuarios.ActiveSheet.ActiveRowIndex, spUsuarios.ActiveSheet.Columns["idArea"].Index);
            //spCatalogos.Visible = false;

        }

        private void spCatalogos_KeyDown(object sender, KeyEventArgs e)
        {

            //if (e.KeyCode == Keys.Escape)
            //{
            //    spUsuarios.Enabled = true;
            //    spUsuarios.Focus();
            //    spUsuarios.ActiveSheet.SetActiveCell(spUsuarios.ActiveSheet.ActiveRowIndex, spUsuarios.ActiveSheet.Columns["idArea"].Index);
            //    spCatalogos.Visible = false;
            //} 

        }

        private void btnGuardar_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips("Guardar.");

        }

        private void btnEliminar_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips("Eliminar.");

        }

        private void btnSalir_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips("Salir.");

        }

        private void pnlEncabezado_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        private void pnlCuerpo_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        private void pnlPie_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        private void pnlContenido_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        private void spProgramas_DialogKey(object sender, FarPoint.Win.Spread.DialogKeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter) // Validar.
            {
                ControlarSpreadEnter2(spProgramas);
            }

        }

        private void spProgramas_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter) // Validar.
            {
                ControlarSpreadEnter2(spProgramas);
            }
            else if (e.KeyData == Keys.F6) // Eliminar.
            {
                if (this.opcionSeleccionada == (int)TipoControl.Programas)
                { 
                    if (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                            EliminarRegistro(spProgramas);
                    }
                }
            }

        }

        private void rbtnModulos_CheckedChanged(object sender, EventArgs e)
        {

            if (rbtnModulos.Checked)
            {
                this.opcionSeleccionada = (int)TipoControl.Modulos;
                CargarModulos();
            }

        } 

        private void spModulos_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter) // Validar.
            {
                ControlarSpreadEnter2(spModulos);
            }
            else if (e.KeyData == Keys.F6) // Eliminar.
            {
                if (this.opcionSeleccionada == (int)TipoControl.Modulos)
                { 
                    if (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                            EliminarRegistro(spModulos);
                    }
                }
            }

        }

        private void rbtnSubProgramas_CheckedChanged(object sender, EventArgs e)
        {

            if (rbtnSubProgramas.Checked)
            {
                this.opcionSeleccionada = (int)TipoControl.SubProgramas;
                CargarSubProgramasProgramasyModulos();
            }

        }

        private void spSubProgramas_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter) // Validar.
            {
                ControlarSpreadEnter2(spSubProgramas);
            }
            else if (e.KeyData == Keys.F6) // Eliminar.
            {
                if (this.opcionSeleccionada == (int)TipoControl.SubProgramas)
                { 
                    if (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                            EliminarRegistro(spSubProgramas);
                    }
                }
            }

        }

        private void pnlMenu_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips("Menú");

        }

        private void spDatosPrincipales_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        private void spUsuarios_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        private void spModulos_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        private void spProgramas_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        private void spSubProgramas_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        private void rbtnDirectorios_CheckedChanged(object sender, EventArgs e)
        {

            if (rbtnDirectorios.Checked)
            {
                this.opcionSeleccionada = (int)TipoControl.Directorios;
                CargarDirectorios();
            }

        }

        private void spDirectorios_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.F6) // Eliminar.
            {
                if (this.opcionSeleccionada == (int)TipoControl.Directorios)
                {
                    if (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        EliminarRegistro(spUsuarios);
                    }
                }
            }
            else if (e.KeyData == Keys.Enter) // Validar.
            {
                ControlarSpreadEnter2(spDirectorios);
            }

        }

        private void spDirectorios_DialogKey(object sender, FarPoint.Win.Spread.DialogKeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                ControlarSpreadEnter2(spDirectorios);
            }

        }

        private void spModulos_DialogKey(object sender, FarPoint.Win.Spread.DialogKeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                ControlarSpreadEnter2(spModulos);
            }

        }

        private void spSubProgramas_DialogKey(object sender, FarPoint.Win.Spread.DialogKeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                ControlarSpreadEnter2(spSubProgramas);
            }

        }

        #endregion

        #region Métodos 

        private void Centrar()
        {

            this.CenterToScreen();
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

        }

        private void AsignarTooltips()
        {

            ToolTip tp = new ToolTip();
            tp.AutoPopDelay = 5000;
            tp.InitialDelay = 0;
            tp.ReshowDelay = 100;
            tp.ShowAlways = true;
            tp.SetToolTip(this.btnGuardar, "Guardar.");
            tp.SetToolTip(this.btnEliminar, "Eliminar.");
            tp.SetToolTip(this.btnSalir, "Salir.");
            tp.SetToolTip(this.spModulos, "Módulos.");
            tp.SetToolTip(this.spProgramas, "Programas.");
            tp.SetToolTip(this.spSubProgramas, "SubProgramas.");
            tp.SetToolTip(this.spUsuarios, "Usuarios.");
            tp.SetToolTip(this.pnlMenu, "Menú");

        } 

        private void AsignarTooltips(string texto)
        {

            lblDescripcionTooltip.Text = texto;

        }

        private void CargarEncabezados()
        {

            lblEncabezadoPrograma.Text = "Programa: " + this.Text;
            lblEncabezadoDirectorio.Text = "Directorio: " + PanelControl.nombreDirectorio;
            
        }

        private void ControlarSpreadEnter2(FarPoint.Win.Spread.FpSpread spread)
        {

            int fila = spread.ActiveSheet.ActiveRowIndex; 
            if (spread.ActiveSheet.ActiveColumnIndex == spread.ActiveSheet.Columns.Count - 1)
            { 
                spread.ActiveSheet.Rows.Count += 1;
            }

        }

        private void CargarMedidas()
        {

            this.izquierda = 0; this.arriba = pnlMenu.Height;
            this.anchoTotal = pnlCuerpo.Width; this.altoTotal = pnlCuerpo.Height - pnlMenu.Height;
            this.anchoMitad = this.anchoTotal / 2; this.altoMitad = this.altoTotal / 2;
            this.anchoTercio = this.anchoTotal / 3;

        }

        private void ReiniciarOpcionesMenu()
        {

            rbtnDirectorios.Checked = false;
            rbtnUsuarios.Checked = false;
            rbtnModulos.Checked = false;
            rbtnProgramas.Checked = false;
            rbtnSubProgramas.Checked = false;

        }

        private void OcultarSpreads() 
        {

            spDirectorios.Visible = false;
            spUsuarios.Visible = false;
            spModulos.Visible = false;
            spProgramas.Visible = false;
            spSubProgramas.Visible = false;

        }

        private void FormatearSpread()
        {

            // Se cargan tipos de datos de spread.
            tipoEntero.DecimalPlaces = 0;
            tipoTextoContrasena.PasswordChar = '*';
            // Se cargan las opciones generales de cada spread.
            spDirectorios.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell; Application.DoEvents();
            spUsuarios.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell; Application.DoEvents();
            spModulos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell; Application.DoEvents();
            spProgramas.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell; Application.DoEvents();
            spSubProgramas.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell; Application.DoEvents();
            spCatalogos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Midnight; Application.DoEvents();
            spDirectorios.ActiveSheet.GrayAreaBackColor = Color.White; Application.DoEvents();
            spUsuarios.ActiveSheet.GrayAreaBackColor = Color.White; Application.DoEvents();
            spModulos.ActiveSheet.GrayAreaBackColor = Color.White; Application.DoEvents();
            spProgramas.ActiveSheet.GrayAreaBackColor = Color.White; Application.DoEvents();
            spSubProgramas.ActiveSheet.GrayAreaBackColor = Color.White; Application.DoEvents();
            spCatalogos.ActiveSheet.GrayAreaBackColor = Color.FromArgb(230, 230, 255); Application.DoEvents();
            spDirectorios.Font = new Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular); Application.DoEvents();
            spUsuarios.Font = new Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular); Application.DoEvents();
            spModulos.Font = new Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular); Application.DoEvents();
            spProgramas.Font = new Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular); Application.DoEvents();
            spSubProgramas.Font = new Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular); Application.DoEvents();
            spCatalogos.Font = new Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular); Application.DoEvents();
            spDirectorios.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded; Application.DoEvents();
            spUsuarios.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded; Application.DoEvents();
            spModulos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded; Application.DoEvents();
            spProgramas.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded; Application.DoEvents();
            spSubProgramas.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded; Application.DoEvents();
            spCatalogos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded; Application.DoEvents();
            spDirectorios.ActiveSheet.ColumnHeader.Rows[0].Height = Principal.alturaEncabezadosGrandeSpread; Application.DoEvents();
            spUsuarios.ActiveSheet.ColumnHeader.Rows[0].Height = Principal.alturaEncabezadosGrandeSpread; Application.DoEvents();
            spModulos.ActiveSheet.ColumnHeader.Rows[0].Height = Principal.alturaEncabezadosGrandeSpread; Application.DoEvents();
            spProgramas.ActiveSheet.ColumnHeader.Rows[0].Height = Principal.alturaEncabezadosGrandeSpread; Application.DoEvents();
            spSubProgramas.ActiveSheet.ColumnHeader.Rows[0].Height = Principal.alturaEncabezadosGrandeSpread; Application.DoEvents();
            spCatalogos.ActiveSheet.ColumnHeader.Rows[0].Height = Principal.alturaEncabezadosGrandeSpread; Application.DoEvents();
            spDirectorios.ActiveSheet.Rows[-1].Height = Principal.alturaFilasSpread; Application.DoEvents();
            spUsuarios.ActiveSheet.Rows[-1].Height = Principal.alturaFilasSpread; Application.DoEvents();
            spModulos.ActiveSheet.Rows[-1].Height = Principal.alturaFilasSpread; Application.DoEvents();
            spProgramas.ActiveSheet.Rows[-1].Height = Principal.alturaFilasSpread; Application.DoEvents();
            spSubProgramas.ActiveSheet.Rows[-1].Height = Principal.alturaFilasSpread; Application.DoEvents();
            spCatalogos.ActiveSheet.Rows[-1].Height = Principal.alturaFilasSpread; Application.DoEvents();
            
        }
        
        private void FormatearSpreadUsuarios()
        {

            spUsuarios.ActiveSheet.ColumnHeader.Rows[0].Font = new Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold); Application.DoEvents();
            ControlarSpreadEnter(spUsuarios);
            int numeracion = 0; 
            spUsuarios.ActiveSheet.Columns[numeracion].Tag = "id"; numeracion += 1;
            spUsuarios.ActiveSheet.Columns[numeracion].Tag = "nombre"; numeracion += 1;
            spUsuarios.ActiveSheet.Columns[numeracion].Tag = "contrasena"; numeracion += 1;
            spUsuarios.ActiveSheet.Columns[numeracion].Tag = "nivel"; numeracion += 1;
            spUsuarios.ActiveSheet.Columns[numeracion].Tag = "accesoTotal"; numeracion += 1;
            spUsuarios.ActiveSheet.Columns["id"].Width = 40; Application.DoEvents();
            spUsuarios.ActiveSheet.Columns["nombre"].Width = 220; Application.DoEvents();
            spUsuarios.ActiveSheet.Columns["contrasena"].Width = 180; Application.DoEvents();
            spUsuarios.ActiveSheet.Columns["nivel"].Width = 80; Application.DoEvents();
            spUsuarios.ActiveSheet.Columns["accesoTotal"].Width = 120; Application.DoEvents();
            spUsuarios.ActiveSheet.Columns["id"].CellType = tipoEntero; Application.DoEvents();
            spUsuarios.ActiveSheet.Columns["nombre"].CellType = tipoTexto; Application.DoEvents();
            spUsuarios.ActiveSheet.Columns["contrasena"].CellType = tipoTextoContrasena; Application.DoEvents();
            spUsuarios.ActiveSheet.Columns["nivel"].CellType = tipoEntero; Application.DoEvents();
            spUsuarios.ActiveSheet.Columns["accesoTotal"].CellType = tipoBooleano; Application.DoEvents();
            spUsuarios.ActiveSheet.ColumnHeader.Cells[0, spUsuarios.ActiveSheet.Columns["id"].Index].Value = "No.".ToUpper(); Application.DoEvents();
            spUsuarios.ActiveSheet.ColumnHeader.Cells[0, spUsuarios.ActiveSheet.Columns["nombre"].Index].Value = "Nombre".ToUpper(); Application.DoEvents();
            spUsuarios.ActiveSheet.ColumnHeader.Cells[0, spUsuarios.ActiveSheet.Columns["contrasena"].Index].Value = "Contraseña".ToUpper(); Application.DoEvents();
            spUsuarios.ActiveSheet.ColumnHeader.Cells[0, spUsuarios.ActiveSheet.Columns["nivel"].Index].Value = "Nivel".ToUpper(); Application.DoEvents();
            spUsuarios.ActiveSheet.ColumnHeader.Cells[0, spUsuarios.ActiveSheet.Columns["accesoTotal"].Index].Value = "Acceso Total".ToUpper(); Application.DoEvents();
            spUsuarios.ActiveSheet.Rows.Count += 1; Application.DoEvents();

        }

        private void FormatearSpreadModulos(bool estaHabilitado)
        {
          
            spModulos.Enabled = estaHabilitado;
            spModulos.ActiveSheet.ColumnHeader.Rows[0].Font = new Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold); Application.DoEvents();
            if (this.opcionSeleccionada == (int)TipoControl.Modulos)
            {
                spModulos.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal; Application.DoEvents(); 
                ControlarSpreadEnter(spModulos);
            }
            else
                spModulos.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect; Application.DoEvents(); 
            int columnas = 0;
            if (this.opcionSeleccionada == (int)TipoControl.Usuarios) 
                columnas = 4; 
            else 
                columnas = 3; 
            spModulos.ActiveSheet.Columns.Count = columnas; Application.DoEvents();
            int numeracion = 0;
            spModulos.ActiveSheet.Columns[numeracion].Tag = "id"; numeracion += 1;
            spModulos.ActiveSheet.Columns[numeracion].Tag = "nombre"; numeracion += 1;
            spModulos.ActiveSheet.Columns[numeracion].Tag = "prefijo"; numeracion += 1;
            if (this.opcionSeleccionada == (int)TipoControl.Usuarios) 
                spModulos.ActiveSheet.Columns[numeracion].Tag = "estatus"; numeracion += 1;
            spModulos.ActiveSheet.Columns["id"].Width = 40; Application.DoEvents();
            spModulos.ActiveSheet.Columns["nombre"].Width = 220; Application.DoEvents();
            spModulos.ActiveSheet.Columns["prefijo"].Width = 180; Application.DoEvents();
            if (this.opcionSeleccionada == (int)TipoControl.Usuarios) 
                spModulos.ActiveSheet.Columns["estatus"].Width = 120; Application.DoEvents(); 
            spModulos.ActiveSheet.Columns["id"].CellType = tipoEntero; Application.DoEvents();
            spModulos.ActiveSheet.Columns["nombre"].CellType = tipoTexto; Application.DoEvents();
            spModulos.ActiveSheet.Columns["prefijo"].CellType = tipoTexto; Application.DoEvents();
            if (this.opcionSeleccionada == (int)TipoControl.Usuarios) 
                spModulos.ActiveSheet.Columns["estatus"].CellType = tipoBooleano; Application.DoEvents(); 
            spModulos.ActiveSheet.ColumnHeader.Cells[0, spModulos.ActiveSheet.Columns["id"].Index].Value = "No.".ToUpper(); Application.DoEvents();
            spModulos.ActiveSheet.ColumnHeader.Cells[0, spModulos.ActiveSheet.Columns["nombre"].Index].Value = "Nombre".ToUpper(); Application.DoEvents();
            spModulos.ActiveSheet.ColumnHeader.Cells[0, spModulos.ActiveSheet.Columns["prefijo"].Index].Value = "Prefijo".ToUpper(); Application.DoEvents();
            if (this.opcionSeleccionada == (int)TipoControl.Usuarios) 
                spModulos.ActiveSheet.ColumnHeader.Cells[0, spModulos.ActiveSheet.Columns["estatus"].Index].Value = "Bloquear".ToUpper(); Application.DoEvents();
            if (this.opcionSeleccionada == (int)TipoControl.Modulos)
                spModulos.ActiveSheet.Rows.Count += 1; Application.DoEvents(); 
            this.filaModulosDeProgramas = -1;

        }

        private void FormatearSpreadProgramas()
        {

            spProgramas.ActiveSheet.ColumnHeader.Rows[0].Font = new Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold); Application.DoEvents();
            if (this.opcionSeleccionada == (int)TipoControl.Programas)
            {
                spProgramas.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal; Application.DoEvents(); 
                ControlarSpreadEnter(spProgramas);
            }
            else
                spProgramas.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect; Application.DoEvents(); 
            int columnas = 0;
            if (this.opcionSeleccionada == (int)TipoControl.Usuarios)
                columnas = 5;
            else
                columnas = 4;
            spProgramas.ActiveSheet.Columns.Count = columnas; Application.DoEvents();
            int numeracion = 0;
            spProgramas.ActiveSheet.Columns[numeracion].Tag = "idModulo"; numeracion += 1;
            spProgramas.ActiveSheet.Columns[numeracion].Tag = "nombreModulo"; numeracion += 1;
            spProgramas.ActiveSheet.Columns[numeracion].Tag = "id"; numeracion += 1;
            spProgramas.ActiveSheet.Columns[numeracion].Tag = "nombre"; numeracion += 1;
            if (this.opcionSeleccionada == (int)TipoControl.Usuarios)
                spProgramas.ActiveSheet.Columns[numeracion].Tag = "estatus"; numeracion += 1;
            spProgramas.ActiveSheet.Columns["idModulo"].Width = 100; Application.DoEvents();
            spProgramas.ActiveSheet.Columns["nombreModulo"].Width = 200; Application.DoEvents();
            spProgramas.ActiveSheet.Columns["id"].Width = 40; Application.DoEvents();
            spProgramas.ActiveSheet.Columns["nombre"].Width = 300; Application.DoEvents();
            if (this.opcionSeleccionada == (int)TipoControl.Usuarios)
                spProgramas.ActiveSheet.Columns["estatus"].Width = 120; Application.DoEvents();
            spProgramas.ActiveSheet.Columns["idModulo"].CellType = tipoEntero; Application.DoEvents();
            spProgramas.ActiveSheet.Columns["nombreModulo"].CellType = tipoTexto; Application.DoEvents();
            spProgramas.ActiveSheet.Columns["id"].CellType = tipoEntero; Application.DoEvents();
            spProgramas.ActiveSheet.Columns["nombre"].CellType = tipoTexto; Application.DoEvents();
            if (this.opcionSeleccionada == (int)TipoControl.Usuarios)
                spProgramas.ActiveSheet.Columns["estatus"].CellType = tipoBooleano; Application.DoEvents();
            spProgramas.ActiveSheet.ColumnHeader.Cells[0, spProgramas.ActiveSheet.Columns["idModulo"].Index].Value = "No. Módulo".ToUpper(); Application.DoEvents();
            spProgramas.ActiveSheet.ColumnHeader.Cells[0, spProgramas.ActiveSheet.Columns["nombreModulo"].Index].Value = "Nombre Módulo".ToUpper(); Application.DoEvents();
            spProgramas.ActiveSheet.ColumnHeader.Cells[0, spProgramas.ActiveSheet.Columns["id"].Index].Value = "No.".ToUpper(); Application.DoEvents();
            spProgramas.ActiveSheet.ColumnHeader.Cells[0, spProgramas.ActiveSheet.Columns["nombre"].Index].Value = "Nombre".ToUpper(); Application.DoEvents();
            if (this.opcionSeleccionada == (int)TipoControl.Usuarios)
                spProgramas.ActiveSheet.ColumnHeader.Cells[0, spProgramas.ActiveSheet.Columns["estatus"].Index].Value = "Bloquear".ToUpper(); Application.DoEvents();
            if (this.opcionSeleccionada == (int)TipoControl.Programas)
                spProgramas.ActiveSheet.Rows.Count += 1; Application.DoEvents(); 
            if (this.opcionSeleccionada != (int)TipoControl.Usuarios)
            {
                spProgramas.ActiveSheet.Columns["idModulo"].Visible = false; Application.DoEvents();
                spProgramas.ActiveSheet.Columns["nombreModulo"].Visible = false; Application.DoEvents();
            }
            this.filaProgramasDeSubProgramas = -1;

        }

        private void FormatearSpreadSubProgramas()
        {

            spSubProgramas.ActiveSheet.ColumnHeader.Rows[0].Font = new Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold); Application.DoEvents();
            if (this.opcionSeleccionada == (int)TipoControl.SubProgramas)
            {
                spSubProgramas.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal; Application.DoEvents(); 
                ControlarSpreadEnter(spSubProgramas);
            }
            else
                spSubProgramas.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect; Application.DoEvents(); 
            int columnas = 0;
            //if (this.opcionSeleccionada == (int)TipoControl.Usuarios)
            //    columnas = 3;
            //else
                columnas = 2;
            spSubProgramas.ActiveSheet.Columns.Count = columnas; Application.DoEvents();
            int numeracion = 0;
            spSubProgramas.ActiveSheet.Columns[numeracion].Tag = "id"; numeracion += 1;
            spSubProgramas.ActiveSheet.Columns[numeracion].Tag = "nombre"; numeracion += 1;
            spSubProgramas.ActiveSheet.Columns["id"].Width = 40; Application.DoEvents();
            spSubProgramas.ActiveSheet.Columns["nombre"].Width = 300; Application.DoEvents();
            spSubProgramas.ActiveSheet.Columns["id"].CellType = tipoEntero; Application.DoEvents();
            spSubProgramas.ActiveSheet.Columns["nombre"].CellType = tipoTexto; Application.DoEvents();
            spSubProgramas.ActiveSheet.ColumnHeader.Cells[0, spSubProgramas.ActiveSheet.Columns["id"].Index].Value = "No.".ToUpper(); Application.DoEvents();
            spSubProgramas.ActiveSheet.ColumnHeader.Cells[0, spSubProgramas.ActiveSheet.Columns["nombre"].Index].Value = "Nombre".ToUpper(); Application.DoEvents();
            if (this.opcionSeleccionada == (int)TipoControl.SubProgramas)
                spSubProgramas.ActiveSheet.Rows.Count += 1; Application.DoEvents(); 

        }

        //private void FormatearSpreadCatalogoAreas()
        //{

        //    spCatalogos.ActiveSheet.ColumnHeader.Rows[0].Font = new Font(Principal.nombreLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold); Application.DoEvents();
        //    spCatalogos.Location = new Point(spUsuarios.Location.X, spUsuarios.Location.Y); Application.DoEvents();
        //    spCatalogos.Visible = true; Application.DoEvents();
        //    spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never; Application.DoEvents();
        //    spCatalogos.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect; Application.DoEvents();
        //    spCatalogos.Height = spUsuarios.Height; Application.DoEvents();
        //    spCatalogos.Width = 305; Application.DoEvents();
        //    int numeracion = 0; 
        //    spCatalogos.ActiveSheet.Columns[numeracion].Tag = "idArea"; numeracion += 1;
        //    spCatalogos.ActiveSheet.Columns[numeracion].Tag = "nombreArea"; numeracion += 1;
        //    spCatalogos.ActiveSheet.Columns[numeracion].Tag = "claveArea"; numeracion += 1;  
        //    spCatalogos.ActiveSheet.Columns["idArea"].Width = 50; Application.DoEvents();
        //    spCatalogos.ActiveSheet.Columns["nombreArea"].Width = 220; Application.DoEvents(); 
        //    spCatalogos.ActiveSheet.Columns["idArea"].CellType = tipoEntero; Application.DoEvents();
        //    spCatalogos.ActiveSheet.Columns["nombreArea"].CellType = tipoTexto; Application.DoEvents(); 
        //    spCatalogos.ActiveSheet.ColumnHeader.Cells[0, spCatalogos.ActiveSheet.Columns["idArea"].Index].Value = "No.".ToUpper(); Application.DoEvents();
        //    spCatalogos.ActiveSheet.ColumnHeader.Cells[0, spCatalogos.ActiveSheet.Columns["nombreArea"].Index].Value = "Nombre".ToUpper(); Application.DoEvents();
        //    spCatalogos.ActiveSheet.Columns["claveArea"].Visible = false; Application.DoEvents();

        //}

        private void CargarUsuarios()
        {

            OcultarSpreads();
            spUsuarios.Visible = true;
            Application.DoEvents();
            spUsuarios.Width = this.anchoTotal;
            spUsuarios.Height = this.altoTotal;
            spUsuarios.Left = this.izquierda;
            spUsuarios.Top = this.arriba;
            spUsuarios.ActiveSheet.DataSource = usuarios.ObtenerListado();
            FormatearSpreadUsuarios();

        }

        private void CargarProgramasDeUsuario()
        {

            programas.IdModulo = -1;
            spProgramas.ActiveSheet.DataSource = programas.ObtenerListadoReporte();
            FormatearSpreadProgramas();
            int filaUsuarios = spUsuarios.ActiveSheet.ActiveRowIndex;
            int idUsuario = Convert.ToInt32(spUsuarios.ActiveSheet.Cells[filaUsuarios, spUsuarios.ActiveSheet.Columns["id"].Index].Text);
            int idSubPrograma = 0;
            for (int fila = 0; fila < spProgramas.ActiveSheet.Rows.Count; fila++)
            {
                int idModulo = Convert.ToInt32(spProgramas.ActiveSheet.Cells[fila, spProgramas.ActiveSheet.Columns["idModulo"].Index].Text);
                int idPrograma = Convert.ToInt32(spProgramas.ActiveSheet.Cells[fila, spProgramas.ActiveSheet.Columns["id"].Index].Text); 
                bloqueoUsuarios.IdUsuario = idUsuario;
                bloqueoUsuarios.IdModulo = idModulo;
                bloqueoUsuarios.IdPrograma = idPrograma;
                bloqueoUsuarios.IdSubPrograma = idSubPrograma;
                spProgramas.ActiveSheet.Cells[fila, spProgramas.ActiveSheet.Columns["estatus"].Index].Value = bloqueoUsuarios.Obtener(); Application.DoEvents();
            }

        }

        private void GuardarEditarUsuarios()
        {

            usuarios.Id = 0;
            usuarios.Eliminar();
            for (int fila = 0; fila < spUsuarios.ActiveSheet.Rows.Count; fila++)
            {
                int idUsuario = Logica.Funciones.ValidarNumero(spUsuarios.ActiveSheet.Cells[fila, spUsuarios.ActiveSheet.Columns["id"].Index].Text);
                string nombre = spUsuarios.ActiveSheet.Cells[fila, spUsuarios.ActiveSheet.Columns["nombre"].Index].Text;
                string contrasena = Logica.Funciones.ValidarLetra(spUsuarios.ActiveSheet.Cells[fila, spUsuarios.ActiveSheet.Columns["contrasena"].Index].Value);
                int nivel = Logica.Funciones.ValidarNumero(spUsuarios.ActiveSheet.Cells[fila, spUsuarios.ActiveSheet.Columns["nivel"].Index].Text);
                bool accesoTotal = Convert.ToBoolean(spUsuarios.ActiveSheet.Cells[fila, spUsuarios.ActiveSheet.Columns["accesoTotal"].Index].Value);
                if (idUsuario>0 && !string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(contrasena) && nivel>=0)
                {
                    usuarios.Id = idUsuario; 
                    usuarios.Nombre = nombre;
                    usuarios.Contrasena = contrasena;
                    usuarios.Nivel = nivel;
                    usuarios.AccesoTotal = accesoTotal; 
                    if (nivel == 0)
                    { 
                        bloqueoUsuarios.IdUsuario = idUsuario;
                        bloqueoUsuarios.IdModulo = 0;
                        bloqueoUsuarios.IdPrograma = 0;
                        bloqueoUsuarios.IdSubPrograma = 0;
                        bloqueoUsuarios.Eliminar();
                    } 
                    usuarios.Guardar();
                }
            }
            MessageBox.Show("Guardado finalizado.", "Terminado.", MessageBoxButtons.OK);
            OcultarSpreads();
            ReiniciarOpcionesMenu();

        }

        private void EliminarUsuarios() 
        {

            if (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bloqueoUsuarios.IdUsuario = 0;
                bloqueoUsuarios.IdModulo = 0;
                bloqueoUsuarios.IdPrograma = 0;
                bloqueoUsuarios.IdSubPrograma = 0;
                bloqueoUsuarios.Eliminar();
                usuarios.Id = 0;
                usuarios.Eliminar();
                OcultarSpreads();
                ReiniciarOpcionesMenu();
            }

        }

        private void GuardarBloqueoUsuariosHastaProgramas()
        {

            int filaUsuarios = spUsuarios.ActiveSheet.ActiveRowIndex;
            int filaProgramas = spProgramas.ActiveSheet.ActiveRowIndex;
            int idUsuario = Convert.ToInt32(spUsuarios.ActiveSheet.Cells[filaUsuarios, spUsuarios.ActiveSheet.Columns["id"].Index].Text);
            int idModulo = Convert.ToInt32(spProgramas.ActiveSheet.Cells[filaProgramas, spProgramas.ActiveSheet.Columns["idModulo"].Index].Text);
            int idPrograma = Convert.ToInt32(spProgramas.ActiveSheet.Cells[filaProgramas, spProgramas.ActiveSheet.Columns["id"].Index].Text);
            int idSubPrograma = 0;
            if (idUsuario > 0)
            { 
                bloqueoUsuarios.IdUsuario = idUsuario;
                bloqueoUsuarios.IdModulo = idModulo;
                bloqueoUsuarios.IdPrograma = idPrograma;
                bloqueoUsuarios.IdSubPrograma = idSubPrograma; 
                bloqueoUsuarios.Guardar(); 
            }

        }

        private void EliminarBloqueoUsuariosHastaProgramas()
        {

            int filaProgramas = spProgramas.ActiveSheet.ActiveRowIndex;
            int filaAdministrar = spUsuarios.ActiveSheet.ActiveRowIndex;
            int idUsuario = Convert.ToInt32(spUsuarios.ActiveSheet.Cells[filaAdministrar, spUsuarios.ActiveSheet.Columns["id"].Index].Text);
            int idModulo = Convert.ToInt32(spProgramas.ActiveSheet.Cells[filaProgramas, spProgramas.ActiveSheet.Columns["idModulo"].Index].Text);
            int idPrograma = Convert.ToInt32(spProgramas.ActiveSheet.Cells[filaProgramas, spProgramas.ActiveSheet.Columns["id"].Index].Text);
            int idSubPrograma = 0;
            if (idUsuario > 0)
            {
                bloqueoUsuarios.IdUsuario = idUsuario;
                bloqueoUsuarios.IdModulo = idModulo;
                bloqueoUsuarios.IdPrograma = idPrograma;
                bloqueoUsuarios.IdSubPrograma = idSubPrograma;
                bloqueoUsuarios.Eliminar();
            }

        }

        private void CargarModulosDeUsuario(bool estaHabilitado)
        {

            spModulos.ActiveSheet.DataSource = modulos.ObtenerListado();
            FormatearSpreadModulos(estaHabilitado);
            int filaUsuarios = spUsuarios.ActiveSheet.ActiveRowIndex;
            int idUsuario = Convert.ToInt32(spUsuarios.ActiveSheet.Cells[filaUsuarios, spUsuarios.ActiveSheet.Columns["id"].Index].Text);
            int idPrograma = 0;
            int idSubPrograma = 0;
            for (int fila = 0; fila < spModulos.ActiveSheet.Rows.Count; fila++)
            {
                int idModulo = Convert.ToInt32(spModulos.ActiveSheet.Cells[fila, spModulos.ActiveSheet.Columns["id"].Index].Text);
                bloqueoUsuarios.IdUsuario = idUsuario;
                bloqueoUsuarios.IdModulo = idModulo;
                bloqueoUsuarios.IdPrograma = idPrograma;
                bloqueoUsuarios.IdSubPrograma = idSubPrograma;
                spModulos.ActiveSheet.Cells[fila, spModulos.ActiveSheet.Columns["estatus"].Index].Value = bloqueoUsuarios.Obtener(); Application.DoEvents();
            }

        }
         
        private void CargarProgramasyModulos()
        {

            OcultarSpreads();
            spModulos.Visible = true;
            spProgramas.Visible = true;
            Application.DoEvents();
            spModulos.Width = this.anchoMitad;
            spModulos.Height = this.altoTotal;
            spModulos.Top = this.arriba;
            spModulos.Left = this.izquierda;
            spModulos.ActiveSheet.DataSource = modulos.ObtenerListadoReporte();
            FormatearSpreadModulos(true); 
            int idModulo = 0;
            if (this.filaModulosDeProgramas >= 0) 
                idModulo = Logica.Funciones.ValidarNumero(spModulos.ActiveSheet.Cells[this.filaModulosDeProgramas, spModulos.ActiveSheet.Columns["id"].Index].Text);  
            CargarProgramas(idModulo);

        }

        private void CargarProgramas(int idModulo)
        {

            spProgramas.Height = this.altoTotal;
            if (this.opcionSeleccionada == (int)TipoControl.SubProgramas)
            {
                spProgramas.Width = this.anchoTercio;
                spProgramas.Left = this.anchoTercio;
            }
            else
            {
                spProgramas.Width = this.anchoMitad;
                spProgramas.Left = this.anchoMitad;
            }
            spProgramas.Top = this.arriba;
            programas.IdModulo = idModulo;
            spProgramas.ActiveSheet.DataSource = programas.ObtenerListadoReporte();
            FormatearSpreadProgramas();
            if (idModulo > 0)
                spProgramas.Visible = true;
            else
                spProgramas.Visible = false;
            spModulos.Visible = true;
            Application.DoEvents();

        }

        private void GuardarEditarProgramas()
        {

            if (this.filaModulosDeProgramas >= 0)
            {
                int idModulo = Logica.Funciones.ValidarNumero(spModulos.ActiveSheet.Cells[spModulos.ActiveSheet.ActiveRowIndex, spModulos.ActiveSheet.Columns["id"].Index].Text);
                programas.IdModulo = idModulo;
                programas.Id = 0;
                programas.Eliminar();
                for (int fila = 0; fila < spProgramas.ActiveSheet.Rows.Count; fila++)
                {
                    int id = Logica.Funciones.ValidarNumero(spProgramas.ActiveSheet.Cells[fila, spProgramas.ActiveSheet.Columns["id"].Index].Text);
                    string nombre = spProgramas.ActiveSheet.Cells[fila, spProgramas.ActiveSheet.Columns["nombre"].Index].Text;
                    if (idModulo > 0 && id > 0 && !string.IsNullOrEmpty(nombre))
                    {
                        programas.IdModulo = idModulo;
                        programas.Id = id;
                        programas.Nombre = nombre;
                        programas.Guardar();
                    }
                }
                MessageBox.Show("Guardado finalizado.", "Terminado.", MessageBoxButtons.OK);
                OcultarSpreads();
                ReiniciarOpcionesMenu();
            }

        }

        private void EliminarProgramas()
        {

            if (this.filaModulosDeProgramas >= 0)
            {
                if (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int idModulo = Logica.Funciones.ValidarNumero(spModulos.ActiveSheet.Cells[spModulos.ActiveSheet.ActiveRowIndex, spModulos.ActiveSheet.Columns["id"].Index].Text);
                    programas.IdModulo = idModulo;
                    programas.Id = 0;
                    programas.Eliminar();
                    OcultarSpreads();
                    ReiniciarOpcionesMenu();
                }
            }

        }

        private void CargarModulos()
        {

            OcultarSpreads();
            spModulos.Visible = true;
            Application.DoEvents();
            spModulos.Height = this.altoTotal;
            spModulos.Width = this.anchoTotal;
            spModulos.Top = this.arriba;
            spModulos.Left = this.izquierda;
            spModulos.ActiveSheet.DataSource = modulos.ObtenerListadoReporte();
            FormatearSpreadModulos(true);

        }

        private void GuardarEditarModulos()
        {

            modulos.Id = 0;
            modulos.Eliminar();
            for (int fila = 0; fila < spModulos.ActiveSheet.Rows.Count; fila++)
            {
                int id = Logica.Funciones.ValidarNumero(spModulos.ActiveSheet.Cells[fila, spModulos.ActiveSheet.Columns["id"].Index].Text);
                string nombre = spModulos.ActiveSheet.Cells[fila, spModulos.ActiveSheet.Columns["nombre"].Index].Text;
                string prefijo = spModulos.ActiveSheet.Cells[fila, spModulos.ActiveSheet.Columns["prefijo"].Index].Text;
                if (id > 0 && !string.IsNullOrEmpty(nombre))
                { 
                    modulos.Id = id;
                    modulos.Nombre = nombre;
                    modulos.Prefijo = prefijo;
                    modulos.Guardar();
                }
            }
            MessageBox.Show("Guardado finalizado.", "Terminado.", MessageBoxButtons.OK);
            OcultarSpreads();
            ReiniciarOpcionesMenu();

        }

        private void EliminarModulos()
        {

            if (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { 
                modulos.Id = 0;
                modulos.Eliminar();
                OcultarSpreads();
                ReiniciarOpcionesMenu();
            }

        }

        private void GuardarBloqueoUsuariosHastaModulos()
        {

            int filaUsuarios = spUsuarios.ActiveSheet.ActiveRowIndex;
            int filaModulos = spModulos.ActiveSheet.ActiveRowIndex;
            int idUsuario = Convert.ToInt32(spUsuarios.ActiveSheet.Cells[filaUsuarios, spUsuarios.ActiveSheet.Columns["id"].Index].Text);
            int idModulo = Convert.ToInt32(spModulos.ActiveSheet.Cells[filaModulos, spModulos.ActiveSheet.Columns["id"].Index].Text);
            int idPrograma = 0;
            int idSubPrograma = 0;
            if (idUsuario > 0)
            {
                bloqueoUsuarios.IdUsuario = idUsuario;
                bloqueoUsuarios.IdModulo = idModulo;
                bloqueoUsuarios.IdPrograma = idPrograma;
                bloqueoUsuarios.IdSubPrograma = idSubPrograma;
                bloqueoUsuarios.Guardar();
            }

        }

        private void EliminarBloqueoUsuariosHastaModulos()
        {

            int filaModulos = spModulos.ActiveSheet.ActiveRowIndex;
            int filaAdministrar = spUsuarios.ActiveSheet.ActiveRowIndex;
            int idUsuario = Convert.ToInt32(spUsuarios.ActiveSheet.Cells[filaAdministrar, spUsuarios.ActiveSheet.Columns["id"].Index].Text);
            int idModulo = Convert.ToInt32(spModulos.ActiveSheet.Cells[filaModulos, spModulos.ActiveSheet.Columns["id"].Index].Text);
            int idPrograma = 0;
            int idSubPrograma = 0;
            if (idUsuario > 0)
            {
                bloqueoUsuarios.IdUsuario = idUsuario;
                bloqueoUsuarios.IdModulo = idModulo;
                bloqueoUsuarios.IdPrograma = idPrograma;
                bloqueoUsuarios.IdSubPrograma = idSubPrograma;
                bloqueoUsuarios.Eliminar();
            }

        }

        private void CargarSubProgramasProgramasyModulos()
        {

            OcultarSpreads();
            spModulos.Visible = true;
            spProgramas.Visible = true;
            spSubProgramas.Visible = true;
            Application.DoEvents();
            spModulos.Width = this.anchoTercio;
            spModulos.Height = this.altoTotal;
            spModulos.Top = this.arriba;
            spModulos.Left = this.izquierda;
            spModulos.ActiveSheet.DataSource = modulos.ObtenerListadoReporte();
            FormatearSpreadModulos(true);
            int idModulo = 0;
            if (this.filaModulosDeProgramas >= 0)
                idModulo = Logica.Funciones.ValidarNumero(spModulos.ActiveSheet.Cells[this.filaModulosDeProgramas, spModulos.ActiveSheet.Columns["id"].Index].Text);
            spProgramas.Height = this.altoTotal;
            spProgramas.Width = this.anchoMitad;
            spProgramas.Top = this.arriba;
            spProgramas.Left = this.anchoMitad;
            programas.IdModulo = idModulo;
            spProgramas.ActiveSheet.DataSource = programas.ObtenerListadoReporte();
            FormatearSpreadProgramas();
            if (idModulo > 0)
                spProgramas.Visible = true;
            else
                spProgramas.Visible = false;
            Application.DoEvents();
            int idPrograma = 0;
            if (this.filaProgramasDeSubProgramas >= 0)
                idPrograma = Logica.Funciones.ValidarNumero(spProgramas.ActiveSheet.Cells[this.filaProgramasDeSubProgramas, spProgramas.ActiveSheet.Columns["id"].Index].Text);
            CargarSubProgramas(idModulo, idPrograma);

        }

        private void CargarSubProgramas(int idModulo, int idPrograma)
        {

            spSubProgramas.Height = this.altoTotal;
            spSubProgramas.Width = this.anchoTercio;
            spSubProgramas.Top = this.arriba;
            spSubProgramas.Left = this.anchoTercio * 2;
            subProgramas.IdModuloo = idModulo;
            subProgramas.IdProgramaa = idPrograma;
            spSubProgramas.ActiveSheet.DataSource = subProgramas.ObtenerListadoReporte();
            FormatearSpreadSubProgramas();
            if (idPrograma > 0)
                spSubProgramas.Visible = true;
            else
                spSubProgramas.Visible = false;
            spModulos.Visible = true;
            Application.DoEvents();

        }
         
        private void GuardarEditarSubProgramas()
        {

            if (this.filaModulosDeProgramas >= 0 && this.filaProgramasDeSubProgramas >= 0)
            {
                int idModulo = Logica.Funciones.ValidarNumero(spModulos.ActiveSheet.Cells[spModulos.ActiveSheet.ActiveRowIndex, spModulos.ActiveSheet.Columns["id"].Index].Text);
                int idPrograma = Logica.Funciones.ValidarNumero(spProgramas.ActiveSheet.Cells[spProgramas.ActiveSheet.ActiveRowIndex, spProgramas.ActiveSheet.Columns["id"].Index].Text);
                subProgramas.IdModuloo = idModulo;
                subProgramas.IdProgramaa = idPrograma;
                subProgramas.Id = 0;
                subProgramas.Eliminar();
                for (int fila = 0; fila < spSubProgramas.ActiveSheet.Rows.Count; fila++)
                {
                    int id = Logica.Funciones.ValidarNumero(spSubProgramas.ActiveSheet.Cells[fila, spSubProgramas.ActiveSheet.Columns["id"].Index].Text);
                    string nombre = spSubProgramas.ActiveSheet.Cells[fila, spSubProgramas.ActiveSheet.Columns["nombre"].Index].Text;
                    if (idModulo > 0 && idPrograma > 0 && id > 0 && !string.IsNullOrEmpty(nombre))
                    {
                        subProgramas.IdModuloo = idModulo;
                        subProgramas.IdProgramaa = idPrograma;
                        subProgramas.Id = id;
                        subProgramas.Nombre = nombre;
                        subProgramas.Guardar();
                    }
                }
                MessageBox.Show("Guardado finalizado.", "Terminado.", MessageBoxButtons.OK);
                OcultarSpreads();
                ReiniciarOpcionesMenu();
            }

        }

        private void EliminarSubProgramas()
        {

            if (this.filaModulosDeProgramas >= 0 && this.filaProgramasDeSubProgramas >= 0)
            {
                if (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int idModulo = Logica.Funciones.ValidarNumero(spModulos.ActiveSheet.Cells[spModulos.ActiveSheet.ActiveRowIndex, spModulos.ActiveSheet.Columns["id"].Index].Text);
                    int idPrograma = Logica.Funciones.ValidarNumero(spProgramas.ActiveSheet.Cells[spProgramas.ActiveSheet.ActiveRowIndex, spProgramas.ActiveSheet.Columns["id"].Index].Text);
                    subProgramas.IdModuloo = idModulo;
                    subProgramas.IdProgramaa = idPrograma;
                    subProgramas.Id = 0;
                    subProgramas.Eliminar();
                    OcultarSpreads();
                    ReiniciarOpcionesMenu();
                }
            }

        }

        private void CargarDirectorios()
        {

            OcultarSpreads();
            spDirectorios.Visible = true;
            Application.DoEvents();
            spDirectorios.Height = this.altoTotal;
            spDirectorios.Width = this.anchoTotal;
            spDirectorios.Top = this.arriba;
            spDirectorios.Left = this.izquierda;
            spDirectorios.ActiveSheet.DataSource = directorios.ObtenerListadoCompletoReporte();
            FormatearSpreadDirectorios();

        }

        private void GuardarEditarDirectorios()
        {

            directorios.Id = 0;
            directorios.Eliminar();
            for (int fila = 0; fila < spDirectorios.ActiveSheet.Rows.Count; fila++)
            {
                int id = Logica.Funciones.ValidarNumero(spDirectorios.ActiveSheet.Cells[fila, spDirectorios.ActiveSheet.Columns["id"].Index].Text);
                string nombre = spDirectorios.ActiveSheet.Cells[fila, spDirectorios.ActiveSheet.Columns["nombre"].Index].Text;
                string descripcion = spDirectorios.ActiveSheet.Cells[fila, spDirectorios.ActiveSheet.Columns["descripcion"].Index].Text;
                string rutaLogo = spDirectorios.ActiveSheet.Cells[fila, spDirectorios.ActiveSheet.Columns["rutaLogo"].Index].Text;
                bool esPredeterminado = Convert.ToBoolean(spDirectorios.ActiveSheet.Cells[fila, spDirectorios.ActiveSheet.Columns["esPredeterminado"].Index].Value);
                string instanciaSql = spDirectorios.ActiveSheet.Cells[fila, spDirectorios.ActiveSheet.Columns["instanciaSql"].Index].Text;
                string usuarioSql = spDirectorios.ActiveSheet.Cells[fila, spDirectorios.ActiveSheet.Columns["usuarioSql"].Index].Text;
                string contrasenaSql = spDirectorios.ActiveSheet.Cells[fila, spDirectorios.ActiveSheet.Columns["contrasenaSql"].Index].Text;
                if (id > 0 && !string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(descripcion) && !string.IsNullOrEmpty(instanciaSql) && !string.IsNullOrEmpty(usuarioSql) && !string.IsNullOrEmpty(contrasenaSql))
                {
                    directorios.Id = id;
                    directorios.Nombre = nombre;
                    directorios.Descripcion = descripcion;
                    directorios.RutaLogo = rutaLogo;
                    directorios.EsPredeterminado = esPredeterminado;
                    directorios.InstanciaSql = instanciaSql;
                    directorios.UsuarioSql = usuarioSql;
                    directorios.ContrasenaSql = contrasenaSql;
                    directorios.Guardar();
                }
            }
            MessageBox.Show("Guardado finalizado.", "Terminado.", MessageBoxButtons.OK);
            OcultarSpreads();
            ReiniciarOpcionesMenu();

        }

        private void EliminarDirectorios()
        {

            if (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                directorios.Id = 0;
                directorios.Eliminar();
                OcultarSpreads();
                ReiniciarOpcionesMenu();
            }

        }

        private void FormatearSpreadDirectorios()
        {
             
            spDirectorios.ActiveSheet.ColumnHeader.Rows[0].Font = new Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold); Application.DoEvents();
            spDirectorios.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal; Application.DoEvents(); 
            ControlarSpreadEnter(spDirectorios); 
            //int columnas = 8;
            //spDirectorios.ActiveSheet.Columns.Count = columnas; Application.DoEvents();
            int numeracion = 0;
            spDirectorios.ActiveSheet.Columns[numeracion].Tag = "id"; numeracion += 1;
            spDirectorios.ActiveSheet.Columns[numeracion].Tag = "nombre"; numeracion += 1;
            spDirectorios.ActiveSheet.Columns[numeracion].Tag = "descripcion"; numeracion += 1;
            spDirectorios.ActiveSheet.Columns[numeracion].Tag = "rutaLogo"; numeracion += 1;
            spDirectorios.ActiveSheet.Columns[numeracion].Tag = "esPredeterminado"; numeracion += 1;
            spDirectorios.ActiveSheet.Columns[numeracion].Tag = "instanciaSql"; numeracion += 1;
            spDirectorios.ActiveSheet.Columns[numeracion].Tag = "usuarioSql"; numeracion += 1;
            spDirectorios.ActiveSheet.Columns[numeracion].Tag = "contrasenaSql"; numeracion += 1; 
            spDirectorios.ActiveSheet.Columns["id"].Width = 50; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["nombre"].Width = 400; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["descripcion"].Width = 500; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["rutaLogo"].Width = 200; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["esPredeterminado"].Width = 200; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["instanciaSql"].Width = 350; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["usuarioSql"].Width = 200; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["contrasenaSql"].Width = 150; Application.DoEvents(); 
            spDirectorios.ActiveSheet.Columns["id"].CellType = tipoEntero; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["nombre"].CellType = tipoTexto; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["descripcion"].CellType = tipoTexto; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["rutaLogo"].CellType = tipoTexto; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["esPredeterminado"].CellType = tipoBooleano; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["instanciaSql"].CellType = tipoTexto; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["usuarioSql"].CellType = tipoTexto; Application.DoEvents();
            spDirectorios.ActiveSheet.Columns["contrasenaSql"].CellType = tipoTexto; Application.DoEvents();  
            spDirectorios.ActiveSheet.ColumnHeader.Cells[0, spDirectorios.ActiveSheet.Columns["id"].Index].Value = "No.".ToUpper(); Application.DoEvents();
            spDirectorios.ActiveSheet.ColumnHeader.Cells[0, spDirectorios.ActiveSheet.Columns["nombre"].Index].Value = "Nombre".ToUpper(); Application.DoEvents();
            spDirectorios.ActiveSheet.ColumnHeader.Cells[0, spDirectorios.ActiveSheet.Columns["descripcion"].Index].Value = "Descripción".ToUpper(); Application.DoEvents();
            spDirectorios.ActiveSheet.ColumnHeader.Cells[0, spDirectorios.ActiveSheet.Columns["rutaLogo"].Index].Value = "Ruta Logo".ToUpper(); Application.DoEvents();
            spDirectorios.ActiveSheet.ColumnHeader.Cells[0, spDirectorios.ActiveSheet.Columns["esPredeterminado"].Index].Value = "Es Predeterminado".ToUpper(); Application.DoEvents();
            spDirectorios.ActiveSheet.ColumnHeader.Cells[0, spDirectorios.ActiveSheet.Columns["instanciaSql"].Index].Value = "Instancia Sql".ToUpper(); Application.DoEvents();
            spDirectorios.ActiveSheet.ColumnHeader.Cells[0, spDirectorios.ActiveSheet.Columns["usuarioSql"].Index].Value = "Usuario Sql".ToUpper(); Application.DoEvents();
            spDirectorios.ActiveSheet.ColumnHeader.Cells[0, spDirectorios.ActiveSheet.Columns["contrasenaSql"].Index].Value = "Contraseña Sql".ToUpper(); Application.DoEvents();
            spDirectorios.ActiveSheet.Rows.Count += 1; Application.DoEvents(); 

        }

        private void ControlarSpreadEnter(FarPoint.Win.Spread.FpSpread spread)
        {

            FarPoint.Win.Spread.InputMap valor1;
            FarPoint.Win.Spread.InputMap valor2;
            valor1 = spread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            valor1.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap);
            valor1 = spread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused);
            valor1.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap);
            valor2 = spread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused);
            valor2.Put(new FarPoint.Win.Spread.Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            valor2 = spread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            valor2.Put(new FarPoint.Win.Spread.Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

        }

        private void EliminarRegistro(FarPoint.Win.Spread.FpSpread spread)
        {

            spread.ActiveSheet.Rows.Remove(spread.ActiveSheet.ActiveRowIndex, 1); 

        }
         
        #endregion

        #region Enumeraciones

        public enum TipoControl
        {

            Modulos = 1,
            Programas = 2,
            SubProgramas = 3,
            Usuarios = 4,
            Directorios = 5

        }

        #endregion

    }
}
