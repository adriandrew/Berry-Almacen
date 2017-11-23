Imports System.IO
Imports System.ComponentModel
Imports System.Threading

Public Class Principal

    ' Variables de objetos de entidades.
    Public usuarios As New ALMEntidadesEntradas.Usuarios()
    Public entradas As New ALMEntidadesEntradas.Entradas()
    Public origenes As New ALMEntidadesEntradas.Origenes()
    Public almacenes As New ALMEntidadesEntradas.Almacenes()
    Public familias As New ALMEntidadesEntradas.Familias()
    Public subFamilias As New ALMEntidadesEntradas.SubFamilias()
    Public articulos As New ALMEntidadesEntradas.Articulos()
    Public unidadesMedidas As New ALMEntidadesEntradas.UnidadesMedidas()
    Public proveedores As New ALMEntidadesEntradas.Proveedores()
    Public monedas As New ALMEntidadesEntradas.Monedas()
    Public tiposCambios As New ALMEntidadesEntradas.TiposCambios()
    Public tiposEntradas As New ALMEntidadesEntradas.TiposEntradas()
    Public sucursales As New ALMEntidadesEntradas.Sucursales()
    ' Variables de tipos de datos de spread.
    Public tipoTexto As New FarPoint.Win.Spread.CellType.TextCellType()
    Public tipoTextoContrasena As New FarPoint.Win.Spread.CellType.TextCellType()
    Public tipoEntero As New FarPoint.Win.Spread.CellType.NumberCellType()
    Public tipoDoble As New FarPoint.Win.Spread.CellType.NumberCellType()
    Public tipoPorcentaje As New FarPoint.Win.Spread.CellType.PercentCellType()
    Public tipoHora As New FarPoint.Win.Spread.CellType.DateTimeCellType()
    Public tipoFecha As New FarPoint.Win.Spread.CellType.DateTimeCellType()
    Public tipoBooleano As New FarPoint.Win.Spread.CellType.CheckBoxCellType()
    Public tipoMoneda As New FarPoint.Win.Spread.CellType.CurrencyCellType()
    ' Variables de tamaños y posiciones de spreads.
    Public anchoTotal As Integer = 0 : Public altoTotal As Integer = 0
    Public anchoMitad As Integer = 0 : Public altoMitad As Integer = 0
    Public anchoTercio As Integer = 0 : Public altoTercio As Integer = 0 : Public altoCuarto As Integer = 0
    Public izquierda As Integer = 0 : Public arriba As Integer = 0
    ' Variables de formatos de spread.
    Public Shared tipoLetraSpread As String = "Microsoft Sans Serif" : Public Shared tamañoLetraSpread As Integer = 8
    Public Shared alturaFilasEncabezadosGrandesSpread As Integer = 35 : Public Shared alturaFilasEncabezadosMedianosSpread As Integer = 28
    Public Shared alturaFilasEncabezadosChicosSpread As Integer = 22 : Public Shared alturaFilasSpread As Integer = 20
    ' Variables de estilos.
    Public Shared colorSpreadAreaGris As Color = Color.FromArgb(245, 245, 245) : Public Shared colorSpreadTotal As Color = Color.White
    Public Shared colorCaptura As Color = Color.White : Public Shared colorCapturaBloqueada As Color = Color.FromArgb(235, 255, 255)
    Public Shared colorAdvertencia As Color = Color.Orange
    Public Shared colorTemaAzul As Color = Color.FromArgb(99, 160, 162)
    ' Variables generales.
    Public nombreEstePrograma As String = String.Empty
    Public estaCerrando As Boolean = False
    Public estaMostrado As Boolean = False
    Public ejecutarProgramaPrincipal As New ProcessStartInfo()
    Public prefijoBaseDatosAlmacen As String = "ALM" & "_"
    Public cantidadFilas As Integer = 1
    Public opcionCatalogoSeleccionada As Integer = 0
    Public esGuardadoValido As Boolean = True
    Public esIzquierda As Boolean = False
    Public idTipoEntradaTraspaso As Integer = 3 ' Es para tipo de entrada o salida por traspaso.
    ' Variables fijas.
    Public idOrigen As Integer = 1 ' Siempre es 1 para el almacén.
    ' Hilos para carga rapida. 
    Public hiloCentrar As New Thread(AddressOf Centrar)
    Public hiloNombrePrograma As New Thread(AddressOf CargarNombrePrograma)
    Public hiloEncabezadosTitulos As New Thread(AddressOf CargarEncabezadosTitulos)
    ' Variable de desarrollo.
    Public esDesarrollo As Boolean = False

#Region "Eventos"

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor
        MostrarCargando(True)
        ConfigurarConexiones()
        IniciarHilosCarga()
        AsignarTooltips()
        CargarMedidas()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Principal_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Me.Cursor = Cursors.WaitCursor
        'If (Not ValidarAccesoTotal()) Then
        '    Salir()
        'End If 
        FormatearSpread()
        FormatearSpreadEntradas()
        CargarComboOrigenes()
        CargarComboAlmacenes()
        CargarComboMonedas()
        CargarComboTiposEntradas()
        CargarComboProveedores()
        CargarComboSucursales()
        AsignarFoco(cbAlmacenes)
        CargarEstilos()
        Me.estaMostrado = True
        MostrarCargando(False)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Principal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        Me.Cursor = Cursors.WaitCursor
        Dim nombrePrograma As String = "PrincipalBerry"
        AbrirPrograma(nombrePrograma, True)
        System.Threading.Thread.Sleep(3000)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Principal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Me.Cursor = Cursors.WaitCursor
        Me.estaCerrando = True
        MostrarCargando(True)
        Desvanecer()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click

        Salir()

    End Sub

    Private Sub spEntradas_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spEntradas.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spEntradas)
        End If

    End Sub

    Private Sub spEntradas_KeyDown(sender As Object, e As KeyEventArgs) Handles spEntradas.KeyDown

        If (e.KeyData = Keys.F6) Then ' Eliminar un registro.
            If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                EliminarRegistroDeSpread(spEntradas)
            End If
        ElseIf (e.KeyData = Keys.Enter) Then ' Validar registros.
            ControlarSpreadEnter(spEntradas)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            CargarCatalogoEnSpread()
        ElseIf (e.KeyData = Keys.Escape) Then
            spEntradas.ActiveSheet.SetActiveCell(0, 0)
            AsignarFoco(IIf(cbTiposEntradas.SelectedValue = Me.idTipoEntradaTraspaso, cbSucursalesOrigen, txtFactura))
        End If

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Me.Cursor = Cursors.WaitCursor
        ValidarGuardadoEntradas()
        If (Me.esGuardadoValido) Then
            GuardarEditarEntradas()
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        Me.Cursor = Cursors.WaitCursor
        EliminarEntradas(True)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnGuardar_MouseEnter(sender As Object, e As EventArgs) Handles btnGuardar.MouseEnter

        AsignarTooltips("Guardar.")

    End Sub

    Private Sub btnEliminar_MouseEnter(sender As Object, e As EventArgs) Handles btnEliminar.MouseEnter

        AsignarTooltips("Eliminar.")

    End Sub

    Private Sub btnSalir_MouseEnter(sender As Object, e As EventArgs) Handles btnSalir.MouseEnter

        AsignarTooltips("Salir.")

    End Sub

    Private Sub pnlEncabezado_MouseEnter(sender As Object, e As EventArgs) Handles pnlEncabezado.MouseEnter, pnlCuerpo.MouseEnter

        AsignarTooltips(String.Empty)

    End Sub

    Private Sub spCatalogos_CellClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spCatalogos.CellClick

        Dim fila As Integer = e.Row
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.proveedor Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.moneda Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.tipoEntrada) Then
            CargarDatosEnOtrosDeCatalogos(fila)
        Else
            CargarDatosEnSpreadDeCatalogos(fila)
        End If

    End Sub

    Private Sub spCatalogos_CellDoubleClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spCatalogos.CellDoubleClick

        VolverFocoDeCatalogos()

    End Sub

    Private Sub spCatalogos_KeyDown(sender As Object, e As KeyEventArgs) Handles spCatalogos.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            Dim fila As Integer = spCatalogos.ActiveSheet.ActiveRowIndex
            If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.proveedor Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.moneda Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.tipoEntrada) Then
                CargarDatosEnOtrosDeCatalogos(fila)
            Else
                CargarDatosEnSpreadDeCatalogos(fila)
            End If
        ElseIf (e.KeyCode = Keys.Escape) Then
            VolverFocoDeCatalogos()
        End If

    End Sub

    Private Sub temporizador_Tick(sender As Object, e As EventArgs) Handles temporizador.Tick

        If (Me.estaCerrando) Then
            Desvanecer()
        End If

    End Sub

    Private Sub btnAyuda_Click(sender As Object, e As EventArgs) Handles btnAyuda.Click

        Me.Cursor = Cursors.WaitCursor
        MostrarAyuda()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnAyuda_MouseEnter(sender As Object, e As EventArgs) Handles btnAyuda.MouseEnter

        AsignarTooltips("Ayuda.")

    End Sub

    Private Sub txtId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtId.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (IsNumeric(txtId.Text)) Then
                e.SuppressKeyPress = True
                CargarEntradas()
            Else
                txtId.Clear()
                LimpiarPantalla()
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(cbAlmacenes)
        End If

    End Sub

    Private Sub txtIdExterno_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdExterno.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            AsignarFoco(dtpFecha)
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(txtId)
        End If

    End Sub

    Private Sub dtpFecha_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpFecha.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            AsignarFoco(cbMonedas)
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(txtIdExterno)
        End If

    End Sub

    Private Sub cbMonedas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMonedas.SelectedIndexChanged

        CargarComboTiposCambios()

    End Sub

    Private Sub cbMonedas_KeyDown(sender As Object, e As KeyEventArgs) Handles cbMonedas.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbMonedas.SelectedValue > 0) Then
                AsignarFoco(txtTipoCambio)
            Else
                cbMonedas.SelectedIndex = 0
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(dtpFecha)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.moneda
            CargarCatalogoEnOtros()
        End If

    End Sub

    Private Sub cbTiposEntradas_KeyDown(sender As Object, e As KeyEventArgs) Handles cbTiposEntradas.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbTiposEntradas.SelectedValue > 0) Then
                AsignarFoco(cbProveedores)
            Else
                cbTiposEntradas.SelectedIndex = 0
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(txtTipoCambio)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.tipoEntrada
            CargarCatalogoEnOtros()
        End If

    End Sub

    Private Sub txtTipoCambio_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTipoCambio.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            AsignarFoco(cbTiposEntradas)
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(cbMonedas)
        End If

    End Sub

    Private Sub cbAlmacenes_KeyDown(sender As Object, e As KeyEventArgs) Handles cbAlmacenes.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbAlmacenes.SelectedValue > 0) Then
                CargarIdConsecutivo()
                AsignarFoco(txtId)
                txtId.SelectAll()
            Else
                cbAlmacenes.SelectedIndex = 0
            End If
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen
            CargarCatalogoEnOtros()
        End If

    End Sub

    Private Sub cbAlmacenes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAlmacenes.SelectedIndexChanged

        If (Me.estaMostrado) Then
            If (cbAlmacenes.SelectedValue > 0) Then
                CargarIdConsecutivo()
                LimpiarPantalla()
            Else
                cbAlmacenes.SelectedIndex = 0
                txtId.Clear()
            End If
        End If

    End Sub

    Private Sub cbProveedores_KeyDown(sender As Object, e As KeyEventArgs) Handles cbProveedores.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbProveedores.SelectedValue > 0) Then
                AsignarFoco(txtFactura)
            Else
                cbProveedores.SelectedIndex = 0
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(cbTiposEntradas)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.proveedor
            CargarCatalogoEnOtros()
        End If

    End Sub

    Private Sub btnIdAnterior_Click(sender As Object, e As EventArgs) Handles btnIdAnterior.Click

        If (ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtId.Text) > 1) Then
            txtId.Text -= 1
            CargarEntradas()
        End If

    End Sub

    Private Sub btnIdSiguiente_Click(sender As Object, e As EventArgs) Handles btnIdSiguiente.Click

        If (ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtId.Text) >= 1) Then
            txtId.Text += 1
            CargarEntradas()
        End If

    End Sub

    Private Sub btnMostrarOcultar_Click(sender As Object, e As EventArgs) Handles btnMostrarOcultar.Click

        MostrarOcultar()

    End Sub

    Private Sub btnMostrarOcultar_MouseEnter(sender As Object, e As EventArgs) Handles btnMostrarOcultar.MouseEnter

        If (Me.esIzquierda) Then
            AsignarTooltips("Mostrar.")
        Else
            AsignarTooltips("Ocultar.")
        End If

    End Sub

    Private Sub txtBuscarCatalogo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBuscarCatalogo.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            AsignarFoco(spCatalogos)
        ElseIf (e.KeyCode = Keys.Escape) Then
            VolverFocoDeCatalogos()
        End If

    End Sub

    Private Sub txtBuscarCatalogo_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarCatalogo.TextChanged

        BuscarCatalogos()

    End Sub

    Private Sub btnListados_MouseEnter(sender As Object, e As EventArgs) Handles btnListados.MouseEnter

        AsignarTooltips("Mostrar u Ocultar Listado.")

    End Sub

    Private Sub btnListados_Click(sender As Object, e As EventArgs) Handles btnListados.Click

        Me.Cursor = Cursors.WaitCursor
        CargarListados()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub spListados_CellClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spListados.CellClick

        Dim fila As Integer = e.Row
        CargarDatosDeListados(fila)

    End Sub

    Private Sub spListados_CellDoubleClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spListados.CellDoubleClick

        VolverFocoDeListados()

    End Sub

    Private Sub spListados_KeyDown(sender As Object, e As KeyEventArgs) Handles spListados.KeyDown

        If (e.KeyCode = Keys.Escape) Then
            VolverFocoDeListados()
        End If

    End Sub

    Private Sub pbMarca_MouseEnter(sender As Object, e As EventArgs) Handles pbMarca.MouseEnter

        AsignarTooltips("Producido por Berry.")

    End Sub

    Private Sub pnlCapturaSuperior_MouseEnter(sender As Object, e As EventArgs) Handles pnlCapturaSuperior.MouseEnter

        AsignarTooltips("Capturar Datos Generales.")

    End Sub

    Private Sub spEntradas_MouseEnter(sender As Object, e As EventArgs) Handles spEntradas.MouseEnter

        AsignarTooltips("Capturar Datos Detallados.")

    End Sub

    Private Sub pnlPie_MouseEnter(sender As Object, e As EventArgs) Handles pnlPie.MouseEnter

        AsignarTooltips("Opciones.")

    End Sub

    Private Sub cbTiposEntradas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTiposEntradas.SelectedIndexChanged

        If (Me.estaMostrado) Then
            If (cbTiposEntradas.SelectedValue = Me.idTipoEntradaTraspaso) Then
                lblSucursal.Visible = True
                cbSucursalesOrigen.Visible = True
            Else
                lblSucursal.Visible = False
                cbSucursalesOrigen.Visible = False
            End If
        End If

    End Sub

    Private Sub cbSucursalesOrigen_KeyDown(sender As Object, e As KeyEventArgs) Handles cbSucursalesOrigen.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbSucursalesOrigen.SelectedValue > 0) Then
                AsignarFoco(spEntradas)
            Else
                cbSucursalesOrigen.SelectedIndex = 0
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(txtFactura)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.sucursal
            CargarCatalogoEnOtros()
        End If

    End Sub

    Private Sub txtFactura_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFactura.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            AsignarFoco(IIf(cbTiposEntradas.SelectedValue = Me.idTipoEntradaTraspaso, cbSucursalesOrigen, spEntradas))
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(cbProveedores)
        End If

    End Sub

#End Region

#Region "Métodos"

#Region "Básicos"

    Private Sub CargarEstilos()

        pnlCapturaSuperior.BackColor = Principal.colorSpreadAreaGris
        spEntradas.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris
        pnlPie.BackColor = Principal.colorSpreadAreaGris

    End Sub

    Private Sub BuscarCatalogos()

        Dim valorBuscado As String = txtBuscarCatalogo.Text.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
        For fila = 0 To spCatalogos.ActiveSheet.Rows.Count - 1
            Dim valorSpread As String = ALMLogicaEntradas.Funciones.ValidarLetra(spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("id").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("nombre").Index).Text).Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
            If (valorSpread.ToUpper.Contains(valorBuscado.ToUpper)) Then
                spCatalogos.ActiveSheet.Rows(fila).Visible = True
            Else
                spCatalogos.ActiveSheet.Rows(fila).Visible = False
            End If
        Next

    End Sub

    Private Sub MostrarCargando(ByVal mostrar As Boolean)

        Dim pnlCargando As New Panel
        Dim lblCargando As New Label
        Dim crear As Boolean = False
        If (Me.Controls.Find("pnlCargando", True).Count = 0) Then ' Si no existe, se crea. 
            crear = True
        Else ' Si existe, se obtiene.
            pnlCargando = Me.Controls.Find("pnlCargando", False)(0)
            crear = False
        End If
        If (crear And mostrar) Then ' Si se tiene que crear y mostrar.
            ' Imagen de fondo.
            Try
                pnlCargando.BackgroundImage = Image.FromFile(String.Format("{0}\{1}\{2}", IIf(Me.esDesarrollo, "W:", Application.StartupPath), "Imagenes", "cargando.png"))
            Catch
                pnlCargando.BackgroundImage = Image.FromFile(String.Format("{0}\{1}\{2}", IIf(Me.esDesarrollo, "W:", Application.StartupPath), "Imagenes", "logoBerry.png"))
            End Try
            pnlCargando.BackgroundImageLayout = ImageLayout.Center
            pnlCargando.BackColor = Color.DarkSlateGray
            pnlCargando.Width = Me.Width
            pnlCargando.Height = Me.Height
            pnlCargando.Location = New Point(Me.Location)
            pnlCargando.Name = "pnlCargando"
            pnlCargando.Visible = True
            Me.Controls.Add(pnlCargando)
            ' Etiqueta de cargando.
            lblCargando.Text = "¡cargando!"
            lblCargando.BackColor = pnlCargando.BackColor
            lblCargando.ForeColor = Color.White
            lblCargando.AutoSize = False
            lblCargando.Width = Me.Width
            lblCargando.Height = 75
            lblCargando.TextAlign = ContentAlignment.TopCenter
            lblCargando.Font = New Font(Principal.tipoLetraSpread, 40, FontStyle.Regular)
            lblCargando.Location = New Point(lblCargando.Location.X, (Me.Height / 2) + 140)
            pnlCargando.Controls.Add(lblCargando)
            pnlCargando.BringToFront()
            pnlCargando.Focus()
        ElseIf (Not crear) Then ' Si ya existe, se checa si se muestra o no.
            If (mostrar) Then ' Se muestra.
                pnlCargando.Visible = True
                pnlCargando.BringToFront()
            Else ' No se muestra.
                pnlCargando.Visible = False
                pnlCargando.SendToBack()
            End If
        End If
        Application.DoEvents()

    End Sub

    Private Sub MostrarOcultar()

        Dim anchoMenor As Integer = btnMostrarOcultar.Width
        Dim espacio As Integer = 1
        If (Not Me.esIzquierda) Then
            pnlCapturaSuperior.Left = -pnlCapturaSuperior.Width + anchoMenor
            spEntradas.Left = anchoMenor + espacio
            spEntradas.Width = Me.anchoTotal - anchoMenor - espacio
            Me.esIzquierda = True
        Else
            pnlCapturaSuperior.Left = 0
            spEntradas.Left = pnlCapturaSuperior.Width + espacio
            spEntradas.Width = Me.anchoTotal - pnlCapturaSuperior.Width - espacio
            Me.esIzquierda = False
        End If

    End Sub

    Public Sub IniciarHilosCarga()

        CheckForIllegalCrossThreadCalls = False
        hiloNombrePrograma.Start()
        hiloCentrar.Start()
        hiloEncabezadosTitulos.Start()

    End Sub

    Private Sub Salir()

        Application.Exit()

    End Sub

    Private Sub MostrarAyuda()

        Dim pnlAyuda As New Panel()
        Dim txtAyuda As New TextBox()
        If (pnlContenido.Controls.Find("pnlAyuda", True).Count = 0) Then
            pnlAyuda.Name = "pnlAyuda"
            pnlAyuda.Visible = False
            pnlContenido.Controls.Add(pnlAyuda)
            txtAyuda.Name = "txtAyuda"
            pnlAyuda.Controls.Add(txtAyuda)
        Else
            pnlAyuda = pnlContenido.Controls.Find("pnlAyuda", False)(0)
            txtAyuda = pnlAyuda.Controls.Find("txtAyuda", False)(0)
        End If
        If (Not pnlAyuda.Visible) Then
            pnlCuerpo.Visible = False
            pnlAyuda.Visible = True
            pnlAyuda.Size = pnlCuerpo.Size
            pnlAyuda.Location = pnlCuerpo.Location
            pnlContenido.Controls.Add(pnlAyuda)
            txtAyuda.ScrollBars = ScrollBars.Both
            txtAyuda.Multiline = True
            txtAyuda.Width = pnlAyuda.Width - 10
            txtAyuda.Height = pnlAyuda.Height - 10
            txtAyuda.Location = New Point(5, 5)
            txtAyuda.Text = "Sección de Ayuda: " & vbNewLine & vbNewLine & "* Teclas básicas: " & vbNewLine & "F5 sirve para mostrar catálogos. " & vbNewLine & "F6 sirve para eliminar un registro únicamente. " & vbNewLine & "F7 sirve para mostrar listados." & vbNewLine & "Escape sirve para ocultar catálogos o listados que se encuentren desplegados. " & vbNewLine & vbNewLine & "* Catálogos o listados desplegados: " & vbNewLine & "Cuando se muestra algún catálogo o listado, al seleccionar alguna opción de este, se va mostrando en tiempo real en la captura de donde se originó. Cuando se le da doble clic en alguna opción o a la tecla escape se oculta dicho catálogo o listado. " & vbNewLine & vbNewLine & "* Datos obligatorios:" & vbNewLine & "Todos los que tengan el simbolo * son estrictamente obligatorios." & vbNewLine & vbNewLine & "* Captura:" & vbNewLine & "* Parte superior/izquierda: " & vbNewLine & "En esta parte se capturarán todos los datos que son generales, tal cual como el número de la entrada, el almacén al que corresponde, etc." & vbNewLine & "* Parte inferior/derecha: " & vbNewLine & "En esta parte se capturarán todos los datos que pueden combinarse, por ejemplo los distintos artículos de ese número de entrada." & vbNewLine & vbNewLine & "* Existen los botones de guardar/editar y eliminar todo dependiendo lo que se necesite hacer. "
            pnlAyuda.Controls.Add(txtAyuda)
        Else
            pnlCuerpo.Visible = True
            pnlAyuda.Visible = False
        End If
        Application.DoEvents()

    End Sub

    Private Sub Desvanecer()

        temporizador.Interval = 10
        temporizador.Enabled = True
        temporizador.Start()
        If (Me.Opacity > 0) Then
            Me.Opacity -= 0.25 : Application.DoEvents()
        Else
            temporizador.Enabled = False
            temporizador.Stop()
        End If

    End Sub

    Private Function ValidarAccesoTotal() As Boolean

        If ((Not ALMLogicaEntradas.Usuarios.accesoTotal) Or (ALMLogicaEntradas.Usuarios.accesoTotal = 0) Or (ALMLogicaEntradas.Usuarios.accesoTotal = False)) Then
            MsgBox("No tienes permisos suficientes para acceder a este programa.", MsgBoxStyle.Information, "Permisos insuficientes.")
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub Centrar()

        Me.CenterToScreen()
        Me.Opacity = 0.98
        Me.Location = Screen.PrimaryScreen.WorkingArea.Location
        Me.Size = Screen.PrimaryScreen.WorkingArea.Size
        hiloCentrar.Abort()

    End Sub

    Private Sub CargarNombrePrograma()

        Me.nombreEstePrograma = Me.Text
        hiloNombrePrograma.Abort()

    End Sub

    Private Sub AsignarTooltips()

        Dim tp As New ToolTip()
        tp.AutoPopDelay = 5000
        tp.InitialDelay = 0
        tp.ReshowDelay = 100
        tp.ShowAlways = True
        tp.SetToolTip(Me.pnlEncabezado, "Datos Principales.")
        tp.SetToolTip(Me.btnAyuda, "Ayuda.")
        tp.SetToolTip(Me.btnSalir, "Salir.")
        tp.SetToolTip(Me.btnGuardar, "Guardar.")
        tp.SetToolTip(Me.btnEliminar, "Eliminar.")
        tp.SetToolTip(Me.btnListados, "Mostrar u Ocultar Listado.")
        tp.SetToolTip(Me.btnMostrarOcultar, "Mostrar u Ocultar.")
        tp.SetToolTip(Me.pbMarca, "Producido por Berry.")

    End Sub

    Private Sub AsignarTooltips(ByVal texto As String)

        lblDescripcionTooltip.Text = texto

    End Sub

    Private Sub ConfigurarConexiones()

        If (Me.esDesarrollo) Then
            ALMLogicaEntradas.Directorios.id = 2
            ALMLogicaEntradas.Directorios.instanciaSql = "BERRY1-DELL\SQLEXPRESS2008"
            ALMLogicaEntradas.Directorios.usuarioSql = "AdminBerry"
            ALMLogicaEntradas.Directorios.contrasenaSql = "@berry2017"
            pnlEncabezado.BackColor = Color.DarkRed
        Else
            ALMLogicaEntradas.Directorios.ObtenerParametros()
            ALMLogicaEntradas.Usuarios.ObtenerParametros()
        End If
        ALMLogicaEntradas.Programas.bdCatalogo = "Catalogo" & ALMLogicaEntradas.Directorios.id
        ALMLogicaEntradas.Programas.bdConfiguracion = "Configuracion" & ALMLogicaEntradas.Directorios.id
        ALMLogicaEntradas.Programas.bdAlmacen = "Almacen" & ALMLogicaEntradas.Directorios.id
        ALMEntidadesEntradas.BaseDatos.ECadenaConexionCatalogo = ALMLogicaEntradas.Programas.bdCatalogo
        ALMEntidadesEntradas.BaseDatos.ECadenaConexionConfiguracion = ALMLogicaEntradas.Programas.bdConfiguracion
        ALMEntidadesEntradas.BaseDatos.ECadenaConexionAlmacen = ALMLogicaEntradas.Programas.bdAlmacen
        ALMEntidadesEntradas.BaseDatos.AbrirConexionCatalogo()
        ALMEntidadesEntradas.BaseDatos.AbrirConexionConfiguracion()
        ALMEntidadesEntradas.BaseDatos.AbrirConexionAlmacen()
        ConsultarInformacionUsuario()
        CargarPrefijoBaseDatosAlmacen()

    End Sub

    Private Sub CargarPrefijoBaseDatosAlmacen()

        ALMLogicaEntradas.Programas.prefijoBaseDatosAlmacen = Me.prefijoBaseDatosAlmacen

    End Sub

    Private Sub ConsultarInformacionUsuario()

        Dim lista As New List(Of ALMEntidadesEntradas.Usuarios)
        usuarios.EId = ALMLogicaEntradas.Usuarios.id
        lista = usuarios.ObtenerListado()
        If (lista.Count > 0) Then
            ALMLogicaEntradas.Usuarios.id = lista(0).EId
            ALMLogicaEntradas.Usuarios.nombre = lista(0).ENombre
            ALMLogicaEntradas.Usuarios.contrasena = lista(0).EContrasena
            ALMLogicaEntradas.Usuarios.nivel = lista(0).ENivel
            ALMLogicaEntradas.Usuarios.accesoTotal = lista(0).EAccesoTotal
        End If

    End Sub

    Private Sub CargarEncabezadosTitulos()

        lblEncabezadoPrograma.Text = "Programa: " & Me.Text
        lblEncabezadoEmpresa.Text = "Directorio: " & ALMLogicaEntradas.Directorios.nombre
        lblEncabezadoUsuario.Text = "Usuario: " & ALMLogicaEntradas.Usuarios.nombre
        Me.Text = "Programa:  " & Me.nombreEstePrograma & "              Directorio:  " & ALMLogicaEntradas.Directorios.nombre & "              Usuario:  " & ALMLogicaEntradas.Usuarios.nombre
        hiloEncabezadosTitulos.Abort()

    End Sub

    Private Sub AbrirPrograma(nombre As String, salir As Boolean)

        If (Me.esDesarrollo) Then
            Exit Sub
        End If
        ejecutarProgramaPrincipal.UseShellExecute = True
        ejecutarProgramaPrincipal.FileName = nombre & Convert.ToString(".exe")
        ejecutarProgramaPrincipal.WorkingDirectory = Application.StartupPath
        ejecutarProgramaPrincipal.Arguments = ALMLogicaEntradas.Directorios.id.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaEntradas.Directorios.nombre.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaEntradas.Directorios.descripcion.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaEntradas.Directorios.rutaLogo.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaEntradas.Directorios.esPredeterminado.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaEntradas.Directorios.instanciaSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaEntradas.Directorios.usuarioSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaEntradas.Directorios.contrasenaSql.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de directorios, indice 9 ;)".Replace(" ", "|") & " " & ALMLogicaEntradas.Usuarios.id.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de usuario, indice 11 ;)".Replace(" ", "|")
        Try
            Dim proceso = Process.Start(ejecutarProgramaPrincipal)
            proceso.WaitForInputIdle()
            If (salir) Then
                If (Me.ShowIcon) Then
                    Me.ShowIcon = False
                End If
                Application.Exit()
            End If
        Catch ex As Exception
            MessageBox.Show((Convert.ToString("No se puede abrir el programa principal en la ruta : " & ejecutarProgramaPrincipal.WorkingDirectory & "\") & nombre) & Environment.NewLine & Environment.NewLine & ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub AsignarFoco(ByVal c As Control)

        c.Focus()

    End Sub

    Public Sub ControlarSpreadEnterASiguienteColumna(ByVal spread As FarPoint.Win.Spread.FpSpread)

        Dim valor1 As FarPoint.Win.Spread.InputMap
        Dim valor2 As FarPoint.Win.Spread.InputMap
        valor1 = spread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        valor1.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        valor1 = spread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
        valor1.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        valor2 = spread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
        valor2.Put(New FarPoint.Win.Spread.Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        valor2 = spread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        valor2.Put(New FarPoint.Win.Spread.Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None)

    End Sub

    Private Sub CargarTiposDeDatos()

        tipoDoble.DecimalPlaces = 2
        tipoDoble.DecimalSeparator = "."
        tipoDoble.Separator = ","
        tipoDoble.ShowSeparator = True
        tipoEntero.DecimalPlaces = 0
        tipoEntero.Separator = ","
        tipoEntero.ShowSeparator = True
        tipoTextoContrasena.PasswordChar = "*"
        tipoMoneda.MinimumValue = 0
        tipoMoneda.DecimalPlaces = 8
        tipoMoneda.Separator = ","
        tipoMoneda.DecimalSeparator = "."
        tipoMoneda.ShowCurrencySymbol = True
        tipoMoneda.ShowSeparator = True

    End Sub

    Private Sub CargarMedidas()

        Me.izquierda = 0
        Me.arriba = spEntradas.Top
        Me.anchoTotal = pnlCuerpo.Width
        Me.altoTotal = pnlCuerpo.Height
        Me.anchoMitad = Me.anchoTotal / 2
        Me.altoMitad = Me.altoTotal / 2
        Me.anchoTercio = Me.anchoTotal / 3
        Me.altoTercio = Me.altoTotal / 3
        Me.altoCuarto = Me.altoTotal / 4

    End Sub

#End Region

#Region "Todos los demás"

    Private Sub LimpiarPantalla()

        For Each c As Control In pnlCapturaSuperior.Controls
            If ((Not TypeOf c Is Button) AndAlso (Not TypeOf c Is Label)) Then
                c.BackColor = Principal.colorCaptura
            End If
        Next
        For fila = 0 To spEntradas.ActiveSheet.Rows.Count - 1
            For columna = 0 To spEntradas.ActiveSheet.Columns.Count - 1
                spEntradas.ActiveSheet.Cells(fila, columna).BackColor = Principal.colorCaptura
            Next
        Next
        If (Not chkMantenerDatos.Checked) Then
            dtpFecha.Value = Today
            cbMonedas.SelectedIndex = 0
            cbTiposEntradas.SelectedIndex = 0
            cbProveedores.SelectedIndex = 0
            CargarComboTiposCambios()
        End If
        txtIdExterno.Clear()
        txtFactura.Clear()
        cbSucursalesOrigen.SelectedIndex = 0
        spEntradas.ActiveSheet.DataSource = Nothing
        spEntradas.ActiveSheet.Rows.Count = 1
        spEntradas.ActiveSheet.SetActiveCell(0, 0)
        LimpiarSpread(spEntradas)

    End Sub

    Private Sub LimpiarSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.ClearRange(0, 0, spread.ActiveSheet.Rows.Count, spread.ActiveSheet.Columns.Count, True)

    End Sub

    Private Sub CargarComboMonedas()

        cbMonedas.DataSource = monedas.ObtenerListadoReporte()
        cbMonedas.DisplayMember = "IdNombre"
        cbMonedas.ValueMember = "Id"
        CargarComboTiposCambios()

    End Sub

    Private Sub CargarComboTiposCambios()

        If (cbMonedas.Items.Count > 0) Then
            tiposCambios.EFecha = IIf(IsDate(dtpFecha.Value), dtpFecha.Value, Today)
            Dim idMoneda As Integer = IIf(IsNumeric(cbMonedas.SelectedValue), cbMonedas.SelectedValue, 1)
            tiposCambios.EIdMoneda = idMoneda
            Dim valor As Double = 1
            If (idMoneda > 0) Then
                Dim datos As New DataTable
                datos = tiposCambios.ObtenerListado()
                If (datos.Rows.Count > 0) Then
                    valor = datos.Rows(0).Item("Valor")
                End If
            End If
            txtTipoCambio.Text = valor
        End If

    End Sub

    Private Sub CargarComboTiposEntradas()

        cbTiposEntradas.DataSource = tiposEntradas.ObtenerListadoReporte()
        cbTiposEntradas.DisplayMember = "IdNombre"
        cbTiposEntradas.ValueMember = "Id"

    End Sub

    Private Sub CargarComboAlmacenes()

        cbAlmacenes.DataSource = almacenes.ObtenerListadoReporte()
        cbAlmacenes.DisplayMember = "IdNombre"
        cbAlmacenes.ValueMember = "Id"

    End Sub

    Private Sub CargarComboProveedores()

        cbProveedores.DataSource = proveedores.ObtenerListadoReporte()
        cbProveedores.DisplayMember = "IdNombre"
        cbProveedores.ValueMember = "Id"

    End Sub

    Private Sub CargarComboOrigenes()

        cbOrigenes.DataSource = origenes.ObtenerListadoReporte()
        cbOrigenes.DisplayMember = "IdNombre"
        cbOrigenes.ValueMember = "Id"
        cbOrigenes.SelectedValue = 1 ' Siempre es almacén.

    End Sub

    Private Sub CargarComboSucursales()

        cbSucursalesOrigen.DataSource = sucursales.ObtenerListadoReporte()
        cbSucursalesOrigen.DisplayMember = "IdNombre"
        cbSucursalesOrigen.ValueMember = "Id"

    End Sub

    Private Sub FormatearSpread()

        ' Se cargan tipos de datos de spread.
        CargarTiposDeDatos()
        ' Se cargan las opciones generales. 
        pnlCatalogos.Visible = False
        spEntradas.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spCatalogos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Midnight
        spListados.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Rose
        spEntradas.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris
        spListados.ActiveSheet.GrayAreaBackColor = Color.FromArgb(255, 230, 230)
        spEntradas.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spCatalogos.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spListados.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spEntradas.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spListados.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spEntradas.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spCatalogos.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spListados.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spEntradas.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spEntradas.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        spCatalogos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Always
        spListados.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        spListados.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Always
        spEntradas.EditModeReplace = True

    End Sub

    Private Sub EliminarRegistroDeSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        Dim idFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spread.ActiveSheet.Cells(spread.ActiveSheet.ActiveRowIndex, spread.ActiveSheet.Columns("idFamilia").Index).Text)
        Dim idSubFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spread.ActiveSheet.Cells(spread.ActiveSheet.ActiveRowIndex, spread.ActiveSheet.Columns("idSubFamilia").Index).Text)
        Dim idArticulo As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spread.ActiveSheet.Cells(spread.ActiveSheet.ActiveRowIndex, spread.ActiveSheet.Columns("idArticulo").Index).Text)
        spread.ActiveSheet.Rows.Remove(spread.ActiveSheet.ActiveRowIndex, 1)
        spread.ActiveSheet.Rows.Count += 1
        CalcularSaldos(idAlmacen, idFamilia, idSubFamilia, idArticulo)

    End Sub

    Private Sub ControlarSpreadEnter(ByVal spread As FarPoint.Win.Spread.FpSpread)

        Dim columnaActiva As Integer = spread.ActiveSheet.ActiveColumnIndex
        If (columnaActiva = spread.ActiveSheet.Columns.Count - 1) Then
            spread.ActiveSheet.Rows.Count += 1
        End If
        If (spread.Name = spEntradas.Name) Then
            Dim fila As Integer = 0
            If (columnaActiva = spEntradas.ActiveSheet.Columns("idFamilia").Index) Then
                fila = spEntradas.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue))
                Dim idFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index).Value)
                familias.EIdAlmacen = idAlmacen
                familias.EId = idFamilia
                If (idAlmacen > 0 And idFamilia > 0) Then
                    Dim datos As New DataTable
                    datos = familias.ObtenerListado()
                    If (datos.Rows.Count > 0) Then
                        spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("nombreFamilia").Index).Value = datos.Rows(0).Item("Nombre")
                        spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index, fila, spEntradas.ActiveSheet.Columns("nombreFamilia").Index).Value = String.Empty
                        spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index, fila, spEntradas.ActiveSheet.Columns("nombreFamilia").Index).Value = String.Empty
                    spEntradas.ActiveSheet.ClearSelection()
                    spEntradas.ActiveSheet.SetActiveCell(fila, 0)
                End If
            ElseIf (columnaActiva = spEntradas.ActiveSheet.Columns("idSubFamilia").Index) Then
                fila = spEntradas.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
                Dim idFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Value)
                subFamilias.EIdAlmacen = idAlmacen
                subFamilias.EIdFamilia = idFamilia
                subFamilias.EId = idSubFamilia
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0) Then
                    Dim datos As New DataTable
                    datos = subFamilias.ObtenerListado()
                    If (datos.Rows.Count > 0) Then
                        spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = datos.Rows(0).Item("Nombre")
                        spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idSubFamilia").Index, fila, spEntradas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = String.Empty
                        spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idSubFamilia").Index, fila, spEntradas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = String.Empty
                    spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spEntradas.ActiveSheet.Columns("idArticulo").Index) Then
                fila = spEntradas.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
                Dim idFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Value)
                Dim idArticulo As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index).Value)
                articulos.EIdAlmacen = idAlmacen
                articulos.EIdFamilia = idFamilia
                articulos.EIdSubFamilia = idSubFamilia
                articulos.EId = idArticulo
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0 And idArticulo > 0) Then
                    For indice = 0 To spEntradas.ActiveSheet.Rows.Count - 1 ' Se valida que no se repitan los articulos.
                        Dim idArticuloLocal As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(indice, spEntradas.ActiveSheet.Columns("idArticulo").Index).Text)
                        Dim idSubFamiliaLocal As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(indice, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Text)
                        Dim idFamiliaLocal As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(indice, spEntradas.ActiveSheet.Columns("idFamilia").Index).Text)
                        If (idArticuloLocal > 0 And idFamiliaLocal > 0 And idSubFamiliaLocal > 0) Then
                            If (idArticuloLocal = idArticulo And idSubFamiliaLocal = idSubFamilia And idFamiliaLocal = idFamilia And indice <> fila) Then
                                spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index).Value = String.Empty
                                spEntradas.ActiveSheet.ClearRange(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index, 1, spEntradas.ActiveSheet.Columns.Count - 1, True)
                                spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex - 1)
                                Exit Sub
                            End If
                        End If
                    Next
                    Dim datos As New DataTable
                    datos = articulos.ObtenerListado()
                    If (datos.Rows.Count > 0) Then
                        spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("nombreArticulo").Index).Value = datos.Rows(0).Item("Nombre")
                        Dim datos2 As New DataTable
                        unidadesMedidas.EId = datos.Rows(0).Item("IdUnidadMedida")
                        datos2 = unidadesMedidas.ObtenerListado()
                        If (datos2.Rows.Count > 0) Then
                            spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = datos2.Rows(0).Item("Nombre")
                        End If
                        spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex + 2)
                        ' Se calculan los saldos.
                        CalcularSaldos(idAlmacen, idFamilia, idSubFamilia, idArticulo)
                    Else
                        spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index, fila, spEntradas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = String.Empty
                        spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index, fila, spEntradas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = String.Empty
                    spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spEntradas.ActiveSheet.Columns("cantidad").Index) Then
                fila = spEntradas.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).Value)
                If (cantidad > 0) Then
                    Dim valorPrecio As String = ALMLogicaEntradas.Funciones.ValidarLetra(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precio").Index).Text)
                    Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
                    Dim idFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index).Value)
                    Dim idSubFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Value)
                    Dim idArticulo As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index).Value)
                    If (String.IsNullOrEmpty(valorPrecio)) Then
                        Dim datos As New DataTable
                        articulos.EIdAlmacen = idAlmacen
                        articulos.EIdFamilia = idFamilia
                        articulos.EIdSubFamilia = idSubFamilia
                        articulos.EId = idArticulo
                        datos = articulos.ObtenerListado()
                        If (datos.Rows.Count > 0) Then
                            Dim precio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(datos.Rows(0).Item("Precio"))
                            spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precio").Index).Value = precio
                            spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Value = cantidad * precio
                            Dim tipoCambio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtTipoCambio.Text)
                            spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).Value = cantidad * precio * tipoCambio
                        End If
                    End If
                    ' Se calculan los saldos.
                    CalcularSaldos(idAlmacen, idFamilia, idSubFamilia, idArticulo)
                Else
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).Value = String.Empty
                    spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spEntradas.ActiveSheet.Columns("precio").Index) Then
                fila = spEntradas.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).Value)
                Dim precio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precio").Index).Value)
                Dim tipoCambio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtTipoCambio.Text)
                If (cantidad > 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Value = cantidad * precio
                    If (tipoCambio > 0) Then
                        spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).Value = cantidad * precio * tipoCambio
                    End If
                ElseIf (precio = 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precio").Index).Value = 0
                Else
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precio").Index).Value = String.Empty
                    spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spEntradas.ActiveSheet.Columns("total").Index) Then
                fila = spEntradas.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).Value)
                Dim total As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Value)
                Dim tipoCambio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtTipoCambio.Text)
                If (cantidad > 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precio").Index).Value = total / cantidad
                    If (tipoCambio > 0) Then
                        spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).Value = total * tipoCambio
                    End If
                Else
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Value = String.Empty
                    spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            End If
        End If

    End Sub

    Private Function CalcularSaldos(ByVal idAlmacen As Integer, ByVal idFamilia As Integer, ByVal idSubFamilia As Integer, ByVal idArticulo As Integer) As Integer

        ' Con esta instrucción siguiente se quita el bindeo de la columna saldo que le hace el datatable al spread.
        spEntradas.ActiveSheet.BindDataColumn(spEntradas.ActiveSheet.Columns("saldoUnidades").Index, Nothing)
        ' Se obtienen los saldos exceptuando esta entrada.
        Dim id As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtId.Text)
        entradas.EIdAlmacen = idAlmacen
        entradas.EIdFamilia = idFamilia
        entradas.EIdSubFamilia = idSubFamilia
        entradas.EIdArticulo = idArticulo
        entradas.EId = id
        Dim unidadesRestantesExcepcion As Integer = entradas.ObtenerSaldos(False)
        'Se obtiene la sumatoria de unidades escritas por el usuario.
        Dim unidadesTemporales As Integer = 0
        For fila As Integer = 0 To spEntradas.ActiveSheet.Rows.Count - 1
            If (ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue) = idAlmacen AndAlso ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index).Value) = idFamilia AndAlso ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Value) = idSubFamilia AndAlso ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index).Value) = idArticulo) Then
                unidadesTemporales += spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).Value
            End If
        Next
        ' Se actualizan los saldos en pantalla.
        Dim saldoTiempoReal As Integer = unidadesRestantesExcepcion + unidadesTemporales
        For fila As Integer = 0 To spEntradas.ActiveSheet.Rows.Count - 1
            If (ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue) = idAlmacen AndAlso ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index).Value) = idFamilia AndAlso ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Value) = idSubFamilia AndAlso ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index).Value) = idArticulo) Then
                spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("saldoUnidades").Index).Value = saldoTiempoReal
            End If
        Next
        Return saldoTiempoReal

    End Function

    Private Sub CalcularSaldosConExcepcion()

        ' Con esta instrucción siguiente se quita el bindeo de la columna saldo que le hace el datatable al spread.
        spEntradas.ActiveSheet.BindDataColumn(spEntradas.ActiveSheet.Columns("saldoUnidades").Index, Nothing)
        ' Con esto se calculan todos los saldos de algún id.
        For fila As Integer = 0 To spEntradas.ActiveSheet.Rows.Count - 1
            Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            Dim idFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index).Text)
            entradas.EIdAlmacen = idAlmacen
            entradas.EIdFamilia = idFamilia
            entradas.EIdSubFamilia = idSubFamilia
            entradas.EIdArticulo = idArticulo
            Dim unidadesRestantes As Integer = entradas.ObtenerSaldos(True)
            spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("saldoUnidades").Index).Value = unidadesRestantes
        Next

    End Sub

    Private Sub CargarIdConsecutivo()

        entradas.EIdOrigen = Me.idOrigen
        entradas.EIdAlmacen = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        Dim idMaximo As Integer = entradas.ObtenerMaximoId()
        txtId.Text = idMaximo

    End Sub

    Private Sub CargarDatosEnSpreadDeCatalogos(ByVal filaCatalogos As Integer)

        If (spEntradas.ActiveSheet.ActiveColumnIndex = spEntradas.ActiveSheet.Columns("idFamilia").Index Or spEntradas.ActiveSheet.ActiveColumnIndex = spEntradas.ActiveSheet.Columns("nombreFamilia").Index) Then
            spEntradas.ActiveSheet.Cells(spEntradas.ActiveSheet.ActiveRowIndex, spEntradas.ActiveSheet.Columns("idFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spEntradas.ActiveSheet.Cells(spEntradas.ActiveSheet.ActiveRowIndex, spEntradas.ActiveSheet.Columns("nombreFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spEntradas.ActiveSheet.ActiveColumnIndex = spEntradas.ActiveSheet.Columns("idSubFamilia").Index Or spEntradas.ActiveSheet.ActiveColumnIndex = spEntradas.ActiveSheet.Columns("nombreSubFamilia").Index) Then
            spEntradas.ActiveSheet.Cells(spEntradas.ActiveSheet.ActiveRowIndex, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spEntradas.ActiveSheet.Cells(spEntradas.ActiveSheet.ActiveRowIndex, spEntradas.ActiveSheet.Columns("nombreSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spEntradas.ActiveSheet.ActiveColumnIndex = spEntradas.ActiveSheet.Columns("idArticulo").Index Or spEntradas.ActiveSheet.ActiveColumnIndex = spEntradas.ActiveSheet.Columns("nombreArticulo").Index) Then
            spEntradas.ActiveSheet.Cells(spEntradas.ActiveSheet.ActiveRowIndex, spEntradas.ActiveSheet.Columns("idArticulo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spEntradas.ActiveSheet.Cells(spEntradas.ActiveSheet.ActiveRowIndex, spEntradas.ActiveSheet.Columns("nombreArticulo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
            spEntradas.ActiveSheet.Cells(spEntradas.ActiveSheet.ActiveRowIndex, spEntradas.ActiveSheet.Columns("nombreUnidadMedida").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("unidadMedida").Index).Text
        End If

    End Sub

    Private Sub CargarDatosEnOtrosDeCatalogos(ByVal filaCatalogos As Integer)

        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen) Then
            cbAlmacenes.SelectedValue = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.moneda) Then
            cbMonedas.SelectedValue = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.tipoEntrada) Then
            cbTiposEntradas.SelectedValue = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.proveedor) Then
            cbProveedores.SelectedValue = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
        End If

    End Sub

    Private Sub CargarCatalogoEnSpread()

        spEntradas.Enabled = False
        Dim columna As Integer = spEntradas.ActiveSheet.ActiveColumnIndex
        If ((columna = spEntradas.ActiveSheet.Columns("idFamilia").Index) Or (columna = spEntradas.ActiveSheet.Columns("nombreFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.familia
            Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            If (idAlmacen > 0) Then
                familias.EIdAlmacen = idAlmacen
                familias.EId = 0
                Dim datos As New DataTable
                datos = familias.ObtenerListadoReporte()
                If (datos.Rows.Count > 0) Then
                    spCatalogos.ActiveSheet.DataSource = datos
                Else
                    spCatalogos.ActiveSheet.DataSource = Nothing
                    spCatalogos.ActiveSheet.Rows.Count = 0
                    spEntradas.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spEntradas.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        ElseIf ((columna = spEntradas.ActiveSheet.Columns("idSubFamilia").Index) Or (columna = spEntradas.ActiveSheet.Columns("nombreSubFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.subfamilia
            Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            Dim idFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(spEntradas.ActiveSheet.ActiveRowIndex, spEntradas.ActiveSheet.Columns("idFamilia").Index).Text)
            If (idAlmacen > 0 And idFamilia > 0) Then
                subFamilias.EIdAlmacen = idAlmacen
                subFamilias.EIdFamilia = idFamilia
                subFamilias.EId = 0
                Dim datos As New DataTable
                datos = subFamilias.ObtenerListadoReporte()
                If (datos.Rows.Count > 0) Then
                    spCatalogos.ActiveSheet.DataSource = datos
                Else
                    spCatalogos.ActiveSheet.DataSource = Nothing
                    spCatalogos.ActiveSheet.Rows.Count = 0
                    spEntradas.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spEntradas.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        ElseIf ((columna = spEntradas.ActiveSheet.Columns("idArticulo").Index) Or (columna = spEntradas.ActiveSheet.Columns("nombreArticulo").Index) Or (columna = spEntradas.ActiveSheet.Columns("nombreUnidadMedida").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo
            Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            Dim idFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(spEntradas.ActiveSheet.ActiveRowIndex, spEntradas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(spEntradas.ActiveSheet.ActiveRowIndex, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Text)
            If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0) Then
                articulos.EIdAlmacen = idAlmacen
                articulos.EIdFamilia = idFamilia
                articulos.EIdSubFamilia = idSubFamilia
                articulos.EId = 0
                Dim datos As New DataTable
                datos = articulos.ObtenerListadoReporte()
                If (datos.Rows.Count > 0) Then
                    spCatalogos.ActiveSheet.DataSource = datos
                Else
                    spCatalogos.ActiveSheet.DataSource = Nothing
                    spCatalogos.ActiveSheet.Rows.Count = 0
                    spEntradas.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spEntradas.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        Else
            spEntradas.Enabled = True
        End If
        AsignarFoco(txtBuscarCatalogo)

    End Sub

    Private Sub CargarCatalogoEnOtros()

        pnlCapturaSuperior.Enabled = False
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen) Then
            almacenes.EId = 0
            Dim datos As New DataTable
            datos = almacenes.ObtenerListadoReporteCatalogo()
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                pnlCapturaSuperior.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.centro)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.moneda) Then
            monedas.EId = 0
            Dim datos As New DataTable
            datos = monedas.ObtenerListadoReporteCatalogo()
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                pnlCapturaSuperior.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.centro)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.tipoEntrada) Then
            tiposEntradas.EId = 0
            Dim datos As New DataTable
            datos = tiposEntradas.ObtenerListadoReporteCatalogo()
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                pnlCapturaSuperior.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.centro)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.proveedor) Then
            proveedores.EId = 0
            Dim datos As New DataTable
            datos = proveedores.ObtenerListadoReporteCatalogo()
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                pnlCapturaSuperior.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.centro)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.sucursal) Then
            sucursales.EId = 0
            Dim datos As New DataTable
            datos = sucursales.ObtenerListadoReporteCatalogo()
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                pnlCapturaSuperior.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.centro)
        End If
        AsignarFoco(txtBuscarCatalogo)

    End Sub

    Private Sub FormatearSpreadCatalogos(ByVal posicion As Integer)

        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.Width = 640
            spCatalogos.ActiveSheet.Columns.Count = 3
        Else
            spCatalogos.Width = 510
            spCatalogos.ActiveSheet.Columns.Count = 2
        End If
        If (posicion = OpcionPosicion.izquierda) Then ' Izquierda.
            pnlCatalogos.Location = New Point(Me.izquierda, Me.arriba)
        ElseIf (posicion = OpcionPosicion.centro) Then ' Centrar.
            pnlCatalogos.Location = New Point(Me.anchoMitad - (spCatalogos.Width / 2), Me.arriba)
        ElseIf (posicion = OpcionPosicion.derecha) Then ' Derecha.
            pnlCatalogos.Location = New Point(Me.anchoTotal - spCatalogos.Width, Me.arriba)
        End If
        spCatalogos.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        Dim numeracion As Integer = 0
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "unidadMedida" : numeracion += 1
        End If
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("nombre").Index).Value = "Nombre".ToUpper
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("unidadMedida").Index).Value = "Unidad".ToUpper
        End If
        spCatalogos.ActiveSheet.Columns("id").Width = 70
        spCatalogos.ActiveSheet.Columns("nombre").Width = 370
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.Columns("unidadMedida").Width = 130
        End If
        spCatalogos.ActiveSheet.Columns(0, spCatalogos.ActiveSheet.Columns.Count - 1).AllowAutoFilter = True
        spCatalogos.ActiveSheet.Columns(0, spCatalogos.ActiveSheet.Columns.Count - 1).AllowAutoSort = True
        pnlCatalogos.Height = spEntradas.Height
        pnlCatalogos.Width = spCatalogos.Width
        spCatalogos.Height = pnlCatalogos.Height - txtBuscarCatalogo.Height - 5
        spCatalogos.Width = pnlCatalogos.Width
        pnlCatalogos.BringToFront()
        pnlCatalogos.Visible = True
        spCatalogos.Refresh()

    End Sub

    Private Sub VolverFocoDeCatalogos()

        pnlCapturaSuperior.Enabled = True
        spEntradas.Enabled = True
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen) Then
            AsignarFoco(cbAlmacenes)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.moneda) Then
            AsignarFoco(cbMonedas)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.tipoEntrada) Then
            AsignarFoco(cbTiposEntradas)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.proveedor) Then
            AsignarFoco(cbProveedores)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.sucursal) Then
            AsignarFoco(cbSucursalesOrigen)
        Else
            AsignarFoco(spEntradas)
        End If
        txtBuscarCatalogo.Clear()
        pnlCatalogos.Visible = False

    End Sub

    Private Sub CargarEntradas()

        Me.Cursor = Cursors.WaitCursor
        Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        Dim id As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtId.Text)
        If (Me.idOrigen > 0 AndAlso idAlmacen > 0 AndAlso id > 0) Then
            entradas.EIdOrigen = cbOrigenes.SelectedValue
            entradas.EIdAlmacen = idAlmacen
            entradas.EId = id
            Dim datos As New DataTable
            datos = entradas.ObtenerListadoGeneral()
            If (datos.Rows.Count > 0) Then
                txtIdExterno.Text = datos.Rows(0).Item("IdExterno")
                dtpFecha.Value = datos.Rows(0).Item("Fecha")
                cbMonedas.SelectedValue = datos.Rows(0).Item("IdMoneda")
                txtTipoCambio.Text = datos.Rows(0).Item("TipoCambio")
                cbTiposEntradas.SelectedValue = datos.Rows(0).Item("IdTipoEntrada")
                cbProveedores.SelectedValue = datos.Rows(0).Item("IdProveedor")
                txtFactura.Text = datos.Rows(0).Item("Factura")
                cbSucursalesOrigen.SelectedValue = datos.Rows(0).Item("IdSucursalOrigen")
                spEntradas.ActiveSheet.DataSource = entradas.ObtenerListadoDetallado()
                Me.cantidadFilas = spEntradas.ActiveSheet.Rows.Count + 1
                FormatearSpreadEntradas()
                If (cbOrigenes.SelectedValue > 1) Then ' Si es de origen externo.
                    btnGuardar.Enabled = False
                    btnEliminar.Enabled = False
                Else
                    btnGuardar.Enabled = True
                    btnEliminar.Enabled = True
                End If
                CalcularSaldosConExcepcion()
            Else
                LimpiarPantalla()
            End If
        End If
        AsignarFoco(txtIdExterno)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub FormatearSpreadEntradas()

        ControlarSpreadEnterASiguienteColumna(spEntradas)
        spEntradas.ActiveSheet.Rows.Count = Me.cantidadFilas
        spEntradas.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        Dim numeracion As Integer = 0
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "idFamilia" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "nombreFamilia" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "idSubFamilia" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "nombreSubFamilia" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "idArticulo" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "nombreArticulo" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "nombreUnidadMedida" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "cantidad" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "precio" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "total" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "totalPesos" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "observaciones" : numeracion += 1
        spEntradas.ActiveSheet.Columns.Count = numeracion + 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "saldoUnidades" : numeracion += 1
        spEntradas.ActiveSheet.Columns.Count = numeracion
        spEntradas.ActiveSheet.ColumnHeader.RowCount = 2
        spEntradas.ActiveSheet.ColumnHeader.Rows(0, spEntradas.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spEntradas.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spEntradas.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("idFamilia").Index, 1, 2)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("idFamilia").Index).Value = "F a m i l i a".ToUpper()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("idFamilia").Index).Value = "No. *".ToUpper()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("nombreFamilia").Index).Value = "Nombre *".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("idSubFamilia").Index, 1, 2)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Value = "S u b F a m i l i a".ToUpper()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Value = "No. *".ToUpper()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = "Nombre *".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("idArticulo").Index, 1, 3)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("idArticulo").Index).Value = "A r t í c u l o".ToUpper()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("idArticulo").Index).Value = "No. *".ToUpper()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("nombreArticulo").Index).Value = "Nombre *".ToUpper()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = "Unidad *".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("cantidad").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("cantidad").Index).Value = "Cantidad *".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("precio").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("precio").Index).Value = "Precio *".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("total").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("total").Index).Value = "Total *".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("totalPesos").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("totalPesos").Index).Value = "Total Pesos *".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("observaciones").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("observaciones").Index).Value = "Observaciones".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("saldoUnidades").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("saldoUnidades").Index).Value = "Saldo".ToUpper()
        spEntradas.ActiveSheet.Columns("idFamilia").Width = 50
        spEntradas.ActiveSheet.Columns("nombreFamilia").Width = 130
        spEntradas.ActiveSheet.Columns("idSubFamilia").Width = 50
        spEntradas.ActiveSheet.Columns("nombreSubFamilia").Width = 130
        spEntradas.ActiveSheet.Columns("idArticulo").Width = 50
        spEntradas.ActiveSheet.Columns("nombreArticulo").Width = 180
        spEntradas.ActiveSheet.Columns("nombreUnidadMedida").Width = 65
        spEntradas.ActiveSheet.Columns("cantidad").Width = 80
        spEntradas.ActiveSheet.Columns("precio").Width = 75
        spEntradas.ActiveSheet.Columns("total").Width = 75
        spEntradas.ActiveSheet.Columns("totalPesos").Width = 75
        spEntradas.ActiveSheet.Columns("observaciones").Width = 150
        spEntradas.ActiveSheet.Columns("saldoUnidades").Width = 60
        spEntradas.ActiveSheet.Columns("idFamilia").CellType = tipoEntero
        spEntradas.ActiveSheet.Columns("nombreFamilia").CellType = tipoTexto
        spEntradas.ActiveSheet.Columns("idSubFamilia").CellType = tipoEntero
        spEntradas.ActiveSheet.Columns("nombreSubFamilia").CellType = tipoTexto
        spEntradas.ActiveSheet.Columns("idArticulo").CellType = tipoEntero
        spEntradas.ActiveSheet.Columns("nombreArticulo").CellType = tipoTexto
        spEntradas.ActiveSheet.Columns("nombreUnidadMedida").CellType = tipoTexto
        spEntradas.ActiveSheet.Columns("cantidad").CellType = tipoEntero
        spEntradas.ActiveSheet.Columns("precio").CellType = tipoDoble
        spEntradas.ActiveSheet.Columns("total").CellType = tipoDoble
        spEntradas.ActiveSheet.Columns("totalPesos").CellType = tipoDoble
        spEntradas.ActiveSheet.Columns("observaciones").CellType = tipoTexto
        spEntradas.ActiveSheet.Columns("saldoUnidades").CellType = tipoEntero
        spEntradas.ActiveSheet.Columns("saldoUnidades").Locked = True
        spEntradas.ActiveSheet.LockBackColor = Principal.colorCapturaBloqueada
        spEntradas.Refresh()

    End Sub

    Private Sub ValidarGuardadoEntradas()

        Me.esGuardadoValido = True
        ' Parte superior.
        Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        If (idAlmacen <= 0) Then
            cbAlmacenes.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        Dim id As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtId.Text)
        If (id <= 0) Then
            txtId.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        Dim idMoneda As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbMonedas.SelectedValue)
        If (idMoneda <= 0) Then
            cbMonedas.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        Dim tipoCambio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroAUno(txtTipoCambio.Text)
        If (tipoCambio < 1) Then
            txtTipoCambio.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        Dim idTipoEntrada As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbTiposEntradas.SelectedValue)
        If (idTipoEntrada <= 0) Then
            cbTiposEntradas.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        Dim idProveedor As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbProveedores.SelectedValue)
        If (idProveedor <= 0) Then
            cbProveedores.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        ' Parte inferior.
        For fila As Integer = 0 To spEntradas.ActiveSheet.Rows.Count - 1
            Dim idFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index).Text)
            If (idFamilia > 0 And idSubFamilia > 0 And idArticulo > 0) Then
                Dim cantidad As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).Text)
                If (cantidad <= 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim precio As String = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precio").Index).Text
                Dim precio2 As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precio").Index).Text)
                If (String.IsNullOrEmpty(precio) Or precio2 < 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precio").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim total As String = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Text
                Dim total2 As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Text)
                If (String.IsNullOrEmpty(total) Or total2 < 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim totalPesos As String = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).Text
                Dim totalPesos2 As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).Text)
                If (String.IsNullOrEmpty(totalPesos) Or totalPesos2 < 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                ' Se calculan los saldos.
                CalcularSaldos(idAlmacen, idFamilia, idSubFamilia, idArticulo)
                Dim saldoUnidades As String = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("saldoUnidades").Index).Text
                Dim saldoUnidades2 As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("saldoUnidades").Index).Text)
                If (String.IsNullOrEmpty(saldoUnidades) Or saldoUnidades2 < 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
            End If
        Next

    End Sub

    Private Sub GuardarEditarEntradas()

        EliminarEntradas(False)
        ' Parte superior.
        Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        Dim id As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtId.Text)
        Dim idExterno As String = txtIdExterno.Text
        Dim idMoneda As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbMonedas.SelectedValue)
        Dim tipoCambio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroAUno(txtTipoCambio.Text)
        Dim fecha As Date = dtpFecha.Value
        Dim idTipoEntrada As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbTiposEntradas.SelectedValue)
        Dim idProveedor As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbProveedores.SelectedValue)
        Dim factura As String = txtFactura.Text
        Dim idSucursalOrigen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbSucursalesOrigen.SelectedValue)
        ' Parte inferior.
        For fila As Integer = 0 To spEntradas.ActiveSheet.Rows.Count - 1
            Dim idFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index).Text)
            Dim cantidad As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).Text)
            Dim precio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precio").Index).Text)
            Dim total As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Text)
            Dim totalPesos As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).Text)
            Dim orden As Integer = fila
            Dim observaciones As String = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("observaciones").Index).Text
            If (id > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0 AndAlso idMoneda > 0 AndAlso idTipoEntrada > 0 AndAlso idProveedor > 0) Then
                entradas.EIdOrigen = Me.idOrigen
                entradas.EIdAlmacen = idAlmacen
                entradas.EId = id
                entradas.EIdFamilia = idFamilia
                entradas.EIdSubFamilia = idSubFamilia
                entradas.EIdArticulo = idArticulo
                entradas.EIdExterno = idExterno
                entradas.EIdTipoEntrada = idTipoEntrada
                entradas.EIdProveedor = idProveedor
                entradas.EIdMoneda = idMoneda
                entradas.ETipoCambio = tipoCambio
                entradas.EFecha = fecha
                entradas.ECantidad = cantidad
                entradas.EPrecio = precio
                entradas.ETotal = total
                entradas.ETotalPesos = totalPesos
                entradas.EOrden = orden
                entradas.EObservaciones = observaciones
                entradas.EFactura = factura
                entradas.EIdSucursalOrigen = idSucursalOrigen
                entradas.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        LimpiarPantalla()
        CargarIdConsecutivo()
        AsignarFoco(txtId)

    End Sub

    Private Sub EliminarEntradas(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar esta entrada?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            entradas.EIdOrigen = Me.idOrigen
            entradas.EIdAlmacen = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            entradas.EId = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtId.Text)
            entradas.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            LimpiarPantalla()
            CargarIdConsecutivo()
            AsignarFoco(txtId)
        End If

    End Sub

    Private Sub FormatearSpreadListados(ByVal posicion As Integer)

        spListados.Width = 500
        If (posicion = OpcionPosicion.izquierda) Then ' Izquierda.
            spListados.Location = New Point(Me.izquierda, Me.arriba)
        ElseIf (posicion = OpcionPosicion.centro) Then ' Centrar.
            spListados.Location = New Point(Me.anchoMitad - (spListados.Width / 2), Me.arriba)
        ElseIf (posicion = OpcionPosicion.derecha) Then ' Derecha.
            spListados.Location = New Point(Me.anchoTotal - spListados.Width, Me.arriba)
        End If
        spListados.ActiveSheet.Columns.Count = 5
        spListados.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        Dim numeracion As Integer = 0
        spListados.ActiveSheet.Columns(numeracion).Tag = "idOrigen" : numeracion += 1
        spListados.ActiveSheet.Columns(numeracion).Tag = "nombreOrigen" : numeracion += 1
        spListados.ActiveSheet.Columns(numeracion).Tag = "idAlmacen" : numeracion += 1
        spListados.ActiveSheet.Columns(numeracion).Tag = "nombreAlmacen" : numeracion += 1
        spListados.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spListados.ActiveSheet.ColumnHeader.Rows(0).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spListados.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spListados.ActiveSheet.ColumnHeader.Cells(0, spListados.ActiveSheet.Columns("idOrigen").Index).Value = "No.".ToUpper
        spListados.ActiveSheet.ColumnHeader.Cells(0, spListados.ActiveSheet.Columns("nombreOrigen").Index).Value = "Origen".ToUpper
        spListados.ActiveSheet.ColumnHeader.Cells(0, spListados.ActiveSheet.Columns("idAlmacen").Index).Value = "No.".ToUpper
        spListados.ActiveSheet.ColumnHeader.Cells(0, spListados.ActiveSheet.Columns("nombreAlmacen").Index).Value = "Almacén".ToUpper
        spListados.ActiveSheet.ColumnHeader.Cells(0, spListados.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper
        spListados.ActiveSheet.Columns("idOrigen").Width = 70
        spListados.ActiveSheet.Columns("nombreOrigen").Width = 100
        spListados.ActiveSheet.Columns("idAlmacen").Width = 70
        spListados.ActiveSheet.Columns("nombreAlmacen").Width = 120
        spListados.ActiveSheet.Columns("id").Width = 70
        spListados.ActiveSheet.Columns(0, spListados.ActiveSheet.Columns.Count - 1).AllowAutoFilter = True
        spListados.ActiveSheet.Columns(0, spListados.ActiveSheet.Columns.Count - 1).AllowAutoSort = True
        spListados.Height = spEntradas.Height
        spListados.BringToFront()
        spListados.Visible = True
        spListados.Refresh()

    End Sub

    Private Sub CargarListados()

        If (spListados.Visible) Then
            spListados.Visible = False
            spEntradas.Enabled = True
        Else
            spEntradas.Enabled = False
            entradas.EIdOrigen = 0
            entradas.EIdAlmacen = 0
            entradas.EId = 0
            Dim datos As New DataTable
            datos = entradas.ObtenerListado()
            If (datos.Rows.Count > 0) Then
                spListados.ActiveSheet.DataSource = datos
            Else
                spListados.ActiveSheet.DataSource = Nothing
                spListados.ActiveSheet.Rows.Count = 0
                spEntradas.Enabled = True
            End If
            FormatearSpreadListados(OpcionPosicion.centro)
        End If

    End Sub

    Private Sub CargarDatosDeListados(ByVal filaCatalogos As Integer)

        cbOrigenes.SelectedValue = spListados.ActiveSheet.Cells(filaCatalogos, spListados.ActiveSheet.Columns("idOrigen").Index).Text
        cbAlmacenes.SelectedValue = spListados.ActiveSheet.Cells(filaCatalogos, spListados.ActiveSheet.Columns("idAlmacen").Index).Text
        txtId.Text = spListados.ActiveSheet.Cells(filaCatalogos, spListados.ActiveSheet.Columns("id").Index).Text

    End Sub

    Private Sub VolverFocoDeListados()

        pnlCapturaSuperior.Enabled = True
        spEntradas.Enabled = True
        CargarEntradas()
        AsignarFoco(txtIdExterno)
        spListados.Visible = False

    End Sub

#End Region

#End Region

#Region "Enumeraciones"

    Enum OpcionPosicion

        izquierda = 1
        centro = 2
        derecha = 3

    End Enum

    Enum OpcionCatalogo

        almacen = 1
        familia = 2
        subfamilia = 3
        articulo = 4
        proveedor = 5
        moneda = 6
        tipoEntrada = 7
        sucursal = 8

    End Enum

#End Region

End Class