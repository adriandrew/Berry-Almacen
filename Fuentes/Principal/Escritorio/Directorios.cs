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
    public partial class AdministrarDirectorios : Form
    {
         
        Entidades.Directorios directorios = new Entidades.Directorios();
         
        public AdministrarDirectorios()
        {
            InitializeComponent();
        }

        #region Eventos

        private void AdministrarDirectorios_Load(object sender, EventArgs e)
        {

            Centrar();
            AsignarTooltips();
            CargarEncabezados();
            CargarDirectorios();
            FormatearSpread();

        }

        private void spDirectorios_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        { 

            CambiarDirectorio(e.Row);

        }

        private void spDirectorios_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            PredeterminarDirectorio(e.Row, e.Column);

        }

        private void AdministrarDirectorios_FormClosed(object sender, FormClosedEventArgs e)
        {

            Salir();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            Salir();

        }

        private void btnSalir_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips("Salir.");

        }

        private void pnlContenido_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        private void pnlEncabezado_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        private void pnlCuerpo_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        private void spDirectorios_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        private void pnlPie_MouseEnter(object sender, EventArgs e)
        {

            AsignarTooltips(string.Empty);

        }

        #endregion

        #region Métodos

        private void Salir()
        {

            this.Cursor = Cursors.WaitCursor;
            new Principal().Show();
            this.Dispose();
            this.Cursor = Cursors.Default;

        }

        private void Centrar()
        {

            this.CenterToScreen();
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Opacity = .98;

        }

        private void AsignarTooltips()
        {

            ToolTip tp = new ToolTip();
            tp.AutoPopDelay = 5000;
            tp.InitialDelay = 0;
            tp.ReshowDelay = 100; 
            tp.SetToolTip(this.btnSalir, "Salir."); 

        }

        private void AsignarTooltips(string texto)
        {

            lblDescripcionTooltip.Text = texto;

        }

        private void CargarEncabezados()
        {

            lblEncabezadoPrograma.Text = "Programa: " + this.Text;
            lblEncabezadoDirectorio.Text = "Directorio: " + Logica.Directorios.nombre;
            lblEncabezadoUsuario.Text = "Usuario: " + Logica.Usuarios.nombre; 

        }

        private void CargarDirectorios()
        {

            spDirectorios.DataSource = directorios.ObtenerListadoBasicoReporte();

        }

        private void FormatearSpread()
        {

            FarPoint.Win.Spread.CellType.TextCellType tipoTexto = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType tipoEntero = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType tipoDoble = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.PercentCellType tipoPorcentaje = new FarPoint.Win.Spread.CellType.PercentCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType tipoHora = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            spDirectorios.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            spDirectorios.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            spDirectorios.ActiveSheet.ColumnHeader.Rows[0].Height = Principal.alturaEncabezadosChicoSpread; 
            spDirectorios.ActiveSheet.Rows[-1].Height = Principal.alturaEncabezadosGrandeSpread; 
            spDirectorios.ActiveSheet.GrayAreaBackColor = Color.White; 
            spDirectorios.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect; 
            spDirectorios.Font = new Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular); 
            spDirectorios.ActiveSheet.ColumnHeader.Rows[0].Font = new Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold); 
            int numeracion = 0;
            spDirectorios.ActiveSheet.Columns[numeracion].Tag = "id"; numeracion += 1;
            spDirectorios.ActiveSheet.Columns[numeracion].Tag = "nombre"; numeracion += 1;
            spDirectorios.ActiveSheet.Columns[numeracion].Tag = "descripcion"; numeracion += 1;
            spDirectorios.ActiveSheet.Columns[numeracion].Tag = "esPredeterminado"; numeracion += 1; 
            spDirectorios.ActiveSheet.Columns["id"].Width = 50; 
            spDirectorios.ActiveSheet.Columns["nombre"].Width = 400; 
            spDirectorios.ActiveSheet.Columns["descripcion"].Width = 500; 
            spDirectorios.ActiveSheet.Columns["esPredeterminado"].Width = 200; 
            spDirectorios.ActiveSheet.ColumnHeader.Cells[0, spDirectorios.ActiveSheet.Columns["id"].Index].Value = "No.".ToUpper(); 
            spDirectorios.ActiveSheet.ColumnHeader.Cells[0, spDirectorios.ActiveSheet.Columns["nombre"].Index].Value = "Nombre".ToUpper(); 
            spDirectorios.ActiveSheet.ColumnHeader.Cells[0, spDirectorios.ActiveSheet.Columns["descripcion"].Index].Value = "Descripción".ToUpper(); 
            spDirectorios.ActiveSheet.ColumnHeader.Cells[0, spDirectorios.ActiveSheet.Columns["esPredeterminado"].Index].Value = "Es Predeterminado?".ToUpper();
            Application.DoEvents();

        }

        private void CambiarDirectorio(int fila) 
        {

            int id = Logica.Funciones.ValidarNumero(spDirectorios.ActiveSheet.Cells[fila, spDirectorios.ActiveSheet.Columns["id"].Index].Text);
            Principal.esCambioDirectorio = true;
            Principal.idDirectorioSeleccionado = id; 
            Salir(); 

        }

        private void PredeterminarDirectorio(int fila, int columna)
        {

            if (columna == spDirectorios.ActiveSheet.Columns["esPredeterminado"].Index)
            {
                int id = Logica.Funciones.ValidarNumero(spDirectorios.ActiveSheet.Cells[fila, spDirectorios.ActiveSheet.Columns["id"].Index].Text);
                directorios.Id = id;
                directorios.Predeterminar();
                CargarDirectorios();
                FormatearSpread();
            }

        }
        
        #endregion 

    }
}
