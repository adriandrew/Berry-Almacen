Imports System.IO
Imports System.ComponentModel
Imports System.Threading

Public Class Principal

    ' Variables de objetos de entidades.
    Public usuarios As New ALMEntidadesSalidas.Usuarios()
    Public salidas As New ALMEntidadesSalidas.Salidas()
    Public origenes As New ALMEntidadesSalidas.Origenes()
    Public almacenes As New ALMEntidadesSalidas.Almacenes()
    Public familias As New ALMEntidadesSalidas.Familias()
    Public subFamilias As New ALMEntidadesSalidas.SubFamilias()
    Public articulos As New ALMEntidadesSalidas.Articulos()
    Public unidadesMedidas As New ALMEntidadesSalidas.UnidadesMedidas()
    Public clientes As New ALMEntidadesSalidas.Clientes()
    Public monedas As New ALMEntidadesSalidas.Monedas()
    Public tiposCambios As New ALMEntidadesSalidas.TiposCambios()
    Public tiposSalidas As New ALMEntidadesSalidas.TiposSalidas()
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
    Public Shared tipoLetraSpread As String = "Microsoft Sans Serif" : Public Shared tamañoLetraSpread As Integer = 9
    Public Shared alturaFilasEncabezadosGrandesSpread As Integer = 35 : Public Shared alturaFilasEncabezadosMedianosSpread As Integer = 28
    Public Shared alturaFilasEncabezadosChicosSpread As Integer = 22 : Public Shared alturaFilasSpread As Integer = 20
    Public Shared colorAreaGris = Color.White 
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
    ' Variables fijas.
    Public idOrigen As Integer = 1 ' Siempre será 1 para almacén.
    ' Hilos para carga rápida.
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
        FormatearSpreadSalidas()
        CargarOrigenes()
        CargarAlmacenes()
        CargarMonedas()
        CargarTiposSalidas()
        CargarClientes()
        AsignarFoco(cbAlmacenes)
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

    Private Sub spSalidas_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spSalidas.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spSalidas)
        End If

    End Sub

    Private Sub spSalidas_KeyDown(sender As Object, e As KeyEventArgs) Handles spSalidas.KeyDown

        If (e.KeyData = Keys.F6) Then ' Eliminar un registro.
            If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                EliminarRegistroDeSpread(spSalidas)
            End If
        ElseIf (e.KeyData = Keys.Enter) Then ' Validar registros.
            ControlarSpreadEnter(spSalidas)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos. 
            CargarCatalogoEnSpread()
        ElseIf (e.KeyData = Keys.Escape) Then
            spSalidas.ActiveSheet.SetActiveCell(0, 0)
            AsignarFoco(cbClientes)
        End If

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        ValidarGuardado()
        If (Me.esGuardadoValido) Then
            Me.Cursor = Cursors.WaitCursor
            GuardarEditarSalidas()
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        Me.Cursor = Cursors.WaitCursor
        EliminarSalidas(True)
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

    Private Sub pnlEncabezado_MouseEnter(sender As Object, e As EventArgs) Handles pnlPie.MouseEnter, pnlEncabezado.MouseEnter, pnlCuerpo.MouseEnter

        AsignarTooltips(String.Empty)

    End Sub

    Private Sub spCatalogos_CellClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spCatalogos.CellClick

        Dim fila As Integer = e.Row
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.moneda Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.tipoSalida) Then
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
            If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.moneda Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.tipoSalida) Then
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

        MostrarAyuda()

    End Sub

    Private Sub btnAyuda_MouseEnter(sender As Object, e As EventArgs) Handles btnAyuda.MouseEnter

        AsignarTooltips("Ayuda.")

    End Sub

    Private Sub txtId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtId.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (IsNumeric(txtId.Text)) Then
                e.SuppressKeyPress = True
                CargarSalidas()
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
            txtId.SelectAll()
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

        CargarTiposCambios()

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

    Private Sub cbTiposSalidas_KeyDown(sender As Object, e As KeyEventArgs) Handles cbTiposSalidas.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbTiposSalidas.SelectedValue > 0) Then
                AsignarFoco(cbClientes)
            Else
                cbTiposSalidas.SelectedIndex = 0
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(txtTipoCambio)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.tipoSalida
            CargarCatalogoEnOtros()
        End If

    End Sub

    Private Sub txtTipoCambio_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTipoCambio.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            AsignarFoco(cbTiposSalidas)
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

    Private Sub cbClientes_KeyDown(sender As Object, e As KeyEventArgs) Handles cbClientes.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbClientes.SelectedValue > 0) Then
                AsignarFoco(spSalidas)
            Else
                cbClientes.SelectedIndex = 0
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(cbTiposSalidas)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente
            CargarCatalogoEnOtros()
        End If

    End Sub

    Private Sub btnIdAnterior_Click(sender As Object, e As EventArgs) Handles btnIdAnterior.Click

        If (ALMLogicaSalidas.Funciones.ValidarNumeroACero(txtId.Text) > 1) Then
            txtId.Text -= 1
            CargarSalidas()
        End If

    End Sub

    Private Sub btnIdSiguiente_Click(sender As Object, e As EventArgs) Handles btnIdSiguiente.Click

        If (ALMLogicaSalidas.Funciones.ValidarNumeroACero(txtId.Text) >= 1) Then
            txtId.Text += 1
            CargarSalidas()
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

    Private Sub txtBuscarCatalogo_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarCatalogo.TextChanged

        BuscarCatalogos()

    End Sub

    Private Sub txtBuscarCatalogo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBuscarCatalogo.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            AsignarFoco(spCatalogos)
        ElseIf (e.KeyCode = Keys.Escape) Then
            VolverFocoDeCatalogos()
        End If

    End Sub

    Private Sub btnListados_Click(sender As Object, e As EventArgs) Handles btnListados.Click

        Me.Cursor = Cursors.WaitCursor
        CargarListados()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnListados_MouseEnter(sender As Object, e As EventArgs) Handles btnListados.MouseEnter

        AsignarTooltips("Mostrar/Ocultar Listado.")

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

#End Region

#Region "Métodos"

#Region "Básicos"

    Private Sub BuscarCatalogos()

        Dim valorBuscado As String = txtBuscarCatalogo.Text.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
        For fila = 0 To spCatalogos.ActiveSheet.Rows.Count - 1
            Dim valorSpread As String = ALMLogicaSalidas.Funciones.ValidarLetra(spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("id").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("nombre").Index).Text).Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
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
            spSalidas.Left = anchoMenor + espacio
            spSalidas.Width = Me.anchoTotal - anchoMenor - espacio
            Me.esIzquierda = True
        Else
            pnlCapturaSuperior.Left = 0
            spSalidas.Left = pnlCapturaSuperior.Width + espacio
            spSalidas.Width = Me.anchoTotal - pnlCapturaSuperior.Width - espacio
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
            txtAyuda.Text = "Sección de Ayuda: " & vbNewLine & vbNewLine & "* Teclas básicas: " & vbNewLine & "F5 sirve para mostrar catálogos. " & vbNewLine & "F6 sirve para eliminar un registro únicamente. " & vbNewLine & "Escape sirve para ocultar catálogos que se encuentren desplegados. " & vbNewLine & vbNewLine & "* Catálogos desplegados: " & vbNewLine & "Cuando se muestra algún catálogo, al seleccionar alguna opción de este, se va mostrando en tiempo real en la captura de donde se originó. Cuando se le da doble clic en alguna opción o a la tecla escape se oculta dicho catálogo. " & vbNewLine & vbNewLine & "* Datos obligatorios: " & vbNewLine & "Todos los que tengan el simbolo * son estrictamente obligatorios." & vbNewLine & vbNewLine & "* Captura:" & vbNewLine & "* Parte superior: " & vbNewLine & "En esta parte se capturarán todos los datos que son generales, tal cual como el número de la salida, el almacén al que corresponde, etc." & vbNewLine & "* Parte inferior: " & vbNewLine & "En esta parte se capturarán todos los datos que pueden combinarse, por ejemplo los distintos artículos de ese número de salida." & vbNewLine & vbNewLine & "* Existen los botones de guardar/editar y eliminar todo dependiendo lo que se necesite hacer. "
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

        If ((Not ALMLogicaSalidas.Usuarios.accesoTotal) Or (ALMLogicaSalidas.Usuarios.accesoTotal = 0) Or (ALMLogicaSalidas.Usuarios.accesoTotal = False)) Then
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

    End Sub

    Private Sub AsignarTooltips(ByVal texto As String)

        lblDescripcionTooltip.Text = texto

    End Sub

    Private Sub ConfigurarConexiones()

        If (Me.esDesarrollo) Then
            ALMLogicaSalidas.Directorios.id = 2
            ALMLogicaSalidas.Directorios.instanciaSql = "BERRY1-DELL\SQLEXPRESS2008"
            ALMLogicaSalidas.Directorios.usuarioSql = "AdminBerry"
            ALMLogicaSalidas.Directorios.contrasenaSql = "@berry2017"
            pnlEncabezado.BackColor = Color.DarkRed
            pnlPie.BackColor = Color.DarkRed
        Else
            ALMLogicaSalidas.Directorios.ObtenerParametros()
            ALMLogicaSalidas.Usuarios.ObtenerParametros()
        End If
        ALMLogicaSalidas.Programas.bdCatalogo = "Catalogo" & ALMLogicaSalidas.Directorios.id
        ALMLogicaSalidas.Programas.bdConfiguracion = "Configuracion" & ALMLogicaSalidas.Directorios.id
        ALMLogicaSalidas.Programas.bdAlmacen = "Almacen" & ALMLogicaSalidas.Directorios.id
        ALMEntidadesSalidas.BaseDatos.ECadenaConexionCatalogo = ALMLogicaSalidas.Programas.bdCatalogo
        ALMEntidadesSalidas.BaseDatos.ECadenaConexionConfiguracion = ALMLogicaSalidas.Programas.bdConfiguracion
        ALMEntidadesSalidas.BaseDatos.ECadenaConexionAlmacen = ALMLogicaSalidas.Programas.bdAlmacen
        ALMEntidadesSalidas.BaseDatos.AbrirConexionCatalogo()
        ALMEntidadesSalidas.BaseDatos.AbrirConexionConfiguracion()
        ALMEntidadesSalidas.BaseDatos.AbrirConexionAlmacen()
        ConsultarInformacionUsuario()
        CargarPrefijoBaseDatosAlmacen()

    End Sub

    Private Sub CargarPrefijoBaseDatosAlmacen()

        ALMLogicaSalidas.Programas.prefijoBaseDatosAlmacen = Me.prefijoBaseDatosAlmacen

    End Sub

    Private Sub ConsultarInformacionUsuario()

        Dim lista As New List(Of ALMEntidadesSalidas.Usuarios)
        usuarios.EId = ALMLogicaSalidas.Usuarios.id
        lista = usuarios.ObtenerListado()
        If (lista.Count > 0) Then
            ALMLogicaSalidas.Usuarios.id = lista(0).EId
            ALMLogicaSalidas.Usuarios.nombre = lista(0).ENombre
            ALMLogicaSalidas.Usuarios.contrasena = lista(0).EContrasena
            ALMLogicaSalidas.Usuarios.nivel = lista(0).ENivel
            ALMLogicaSalidas.Usuarios.accesoTotal = lista(0).EAccesoTotal
        End If

    End Sub

    Private Sub CargarEncabezadosTitulos()

        lblEncabezadoPrograma.Text = "Programa: " + Me.Text
        lblEncabezadoEmpresa.Text = "Directorio: " + ALMLogicaSalidas.Directorios.nombre
        lblEncabezadoUsuario.Text = "Usuario: " + ALMLogicaSalidas.Usuarios.nombre
        Me.Text = "Programa:  " + Me.nombreEstePrograma + "              Directorio:  " + ALMLogicaSalidas.Directorios.nombre + "              Usuario:  " + ALMLogicaSalidas.Usuarios.nombre
        hiloEncabezadosTitulos.Abort()

    End Sub

    Private Sub AbrirPrograma(nombre As String, salir As Boolean)

        If (Me.esDesarrollo) Then
            Exit Sub
        End If
        ejecutarProgramaPrincipal.UseShellExecute = True
        ejecutarProgramaPrincipal.FileName = nombre & Convert.ToString(".exe")
        ejecutarProgramaPrincipal.WorkingDirectory = Application.StartupPath
        ejecutarProgramaPrincipal.Arguments = ALMLogicaSalidas.Directorios.id.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaSalidas.Directorios.nombre.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaSalidas.Directorios.descripcion.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaSalidas.Directorios.rutaLogo.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaSalidas.Directorios.esPredeterminado.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaSalidas.Directorios.instanciaSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaSalidas.Directorios.usuarioSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaSalidas.Directorios.contrasenaSql.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de directorios, indice 9 ;)".Replace(" ", "|") & " " & ALMLogicaSalidas.Usuarios.id.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de usuario, indice 11 ;)".Replace(" ", "|")
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
        Me.arriba = spSalidas.Top
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

    Private Function CalcularPrecioPromedio(ByVal idAlmacen As Integer, ByVal idFamilia As Integer, ByVal idSubFamilia As Integer, ByVal idArticulo As Integer) As Double

        Dim promedio As Double = 0
        salidas.EIdAlmacen = idAlmacen
        salidas.EIdFamilia = idFamilia
        salidas.EIdSubFamilia = idSubFamilia
        salidas.EId = idArticulo
        promedio = salidas.ObtenerPrecioPromedio()
        Return Math.Round(promedio, 2)

    End Function

    Private Sub LimpiarPantalla()

        For Each c As Control In pnlCapturaSuperior.Controls
            c.BackColor = Color.White
        Next
        For fila = 0 To spSalidas.ActiveSheet.Rows.Count - 1
            For columna = 0 To spSalidas.ActiveSheet.Columns.Count - 1
                spSalidas.ActiveSheet.Cells(fila, columna).BackColor = Color.White
            Next
        Next
        If (Not chkConservarDatos.Checked) Then
            dtpFecha.Value = Today
            cbMonedas.SelectedIndex = 0
            cbTiposSalidas.SelectedIndex = 0
            cbClientes.SelectedIndex = 0
            CargarTiposCambios()
        End If
        txtIdExterno.Clear()
        spSalidas.ActiveSheet.DataSource = Nothing
        spSalidas.ActiveSheet.Rows.Count = 1
        spSalidas.ActiveSheet.SetActiveCell(0, 0)
        LimpiarSpread(spSalidas)

    End Sub
     
    Private Sub LimpiarSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.ClearRange(0, 0, spread.ActiveSheet.Rows.Count, spread.ActiveSheet.Columns.Count, True)

    End Sub

    Private Sub CargarMonedas()

        cbMonedas.DataSource = monedas.ObtenerListadoReporte()
        cbMonedas.DisplayMember = "IdNombre"
        cbMonedas.ValueMember = "Id"
        CargarTiposCambios()

    End Sub

    Private Sub CargarTiposCambios()

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

    Private Sub CargarTiposSalidas()

        cbTiposSalidas.DataSource = tiposSalidas.ObtenerListadoReporte()
        cbTiposSalidas.DisplayMember = "IdNombre"
        cbTiposSalidas.ValueMember = "Id"

    End Sub

    Private Sub CargarAlmacenes()

        cbAlmacenes.DataSource = almacenes.ObtenerListadoReporte()
        cbAlmacenes.DisplayMember = "IdNombre"
        cbAlmacenes.ValueMember = "Id"

    End Sub

    Private Sub CargarClientes()

        cbClientes.DataSource = clientes.ObtenerListadoReporte()
        cbClientes.DisplayMember = "IdNombre"
        cbClientes.ValueMember = "Id"

    End Sub

    Private Sub CargarOrigenes()

        cbOrigenes.DataSource = origenes.ObtenerListadoReporte()
        cbOrigenes.DisplayMember = "IdNombre"
        cbOrigenes.ValueMember = "Id"
        cbOrigenes.SelectedValue = 1 ' Siempre es almacén.

    End Sub

    Private Sub FormatearSpread()

        ' Se cargan tipos de datos de spread.
        CargarTiposDeDatos()
        ' Se cargan las opciones generales. 
        pnlCatalogos.Visible = False
        spSalidas.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spCatalogos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Midnight
        spListados.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Grayscale
        spSalidas.ActiveSheet.GrayAreaBackColor = Principal.colorAreaGris
        spSalidas.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spCatalogos.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spListados.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spSalidas.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spListados.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spSalidas.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spCatalogos.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spListados.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spSalidas.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spSalidas.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        spCatalogos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Always
        spListados.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        spListados.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Always 
        spSalidas.EditModeReplace = True

    End Sub

    Private Sub EliminarRegistroDeSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        If (spread.ActiveSheet.ActiveRowIndex > 0) Then
            spread.ActiveSheet.Rows.Remove(spread.ActiveSheet.ActiveRowIndex, 1)
        Else
            spread.ActiveSheet.ClearRange(spread.ActiveSheet.ActiveRowIndex, 0, 1, spread.ActiveSheet.Columns.Count, False)
            spread.ActiveSheet.SetActiveCell(spread.ActiveSheet.ActiveRowIndex, 0)
        End If

    End Sub

    Private Sub ControlarSpreadEnter(ByVal spread As FarPoint.Win.Spread.FpSpread)

        Dim columnaActiva As Integer = spread.ActiveSheet.ActiveColumnIndex
        If (columnaActiva = spread.ActiveSheet.Columns.Count - 1) Then
            spread.ActiveSheet.Rows.Count += 1
        End If
        If (spread.Name = spSalidas.Name) Then
            Dim fila As Integer = 0
            If (columnaActiva = spSalidas.ActiveSheet.Columns("idFamilia").Index) Then
                fila = spSalidas.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue))
                Dim idFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idFamilia").Index).Value)
                familias.EIdAlmacen = idAlmacen
                familias.EId = idFamilia
                If (idAlmacen > 0 And idFamilia > 0) Then 
                    Dim datos As New DataTable
                    datos = familias.ObtenerListado()
                    If (datos.Rows.Count > 0) Then
                        spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("nombreFamilia").Index).Value = datos.Rows(0).Item("Nombre")
                        spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idFamilia").Index, fila, spSalidas.ActiveSheet.Columns("nombreFamilia").Index).Value = String.Empty
                        spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idFamilia").Index, fila, spSalidas.ActiveSheet.Columns("nombreFamilia").Index).Value = String.Empty
                    spSalidas.ActiveSheet.ClearSelection()
                    spSalidas.ActiveSheet.SetActiveCell(fila, 0)
                End If
            ElseIf (columnaActiva = spSalidas.ActiveSheet.Columns("idSubFamilia").Index) Then
                fila = spSalidas.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
                Dim idFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idFamilia").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idSubFamilia").Index).Value)
                subFamilias.EIdAlmacen = idAlmacen
                subFamilias.EIdFamilia = idFamilia
                subFamilias.EId = idSubFamilia
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0) Then
                    Dim datos As New DataTable
                    datos = subFamilias.ObtenerListado()
                    If (datos.Rows.Count > 0) Then
                        spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = datos.Rows(0).Item("Nombre")
                        spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idSubFamilia").Index, fila, spSalidas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = String.Empty
                        spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idSubFamilia").Index, fila, spSalidas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = String.Empty
                    spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spSalidas.ActiveSheet.Columns("idArticulo").Index) Then
                fila = spSalidas.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
                Dim idFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idFamilia").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idSubFamilia").Index).Value)
                Dim idArticulo As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idArticulo").Index).Value)
                articulos.EIdAlmacen = idAlmacen
                articulos.EIdFamilia = idFamilia
                articulos.EIdSubFamilia = idSubFamilia
                articulos.EId = idArticulo
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0 And idArticulo > 0) Then
                    For indice = 0 To spSalidas.ActiveSheet.Rows.Count - 1 ' Se valida que no se repitan los articulos.
                        Dim idArticuloLocal As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(indice, spSalidas.ActiveSheet.Columns("idArticulo").Index).Text)
                        Dim idSubFamiliaLocal As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(indice, spSalidas.ActiveSheet.Columns("idSubFamilia").Index).Text)
                        Dim idFamiliaLocal As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(indice, spSalidas.ActiveSheet.Columns("idFamilia").Index).Text)
                        If (idArticuloLocal > 0 And idFamiliaLocal > 0 And idSubFamiliaLocal > 0) Then
                            If (idArticuloLocal = idArticulo And idSubFamiliaLocal = idSubFamilia And idFamiliaLocal = idFamilia And indice <> fila) Then
                                spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idArticulo").Index).Text = String.Empty
                                spSalidas.ActiveSheet.ClearRange(fila, spSalidas.ActiveSheet.Columns("idArticulo").Index, 1, spSalidas.ActiveSheet.Columns.Count - 1, True)
                                spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.ActiveColumnIndex - 1)
                                Exit Sub
                            End If
                        End If
                    Next 
                    Dim datos As New DataTable
                    datos = articulos.ObtenerListado()
                    If (datos.Rows.Count > 0) Then ' Se carga nombre de artículo.
                        spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("nombreArticulo").Index).Value = datos.Rows(0).Item("Nombre")
                        Dim datos2 As New DataTable
                        unidadesMedidas.EId = datos.Rows(0).Item("IdUnidadMedida")
                        datos2 = unidadesMedidas.ObtenerListado()
                        If (datos2.Rows.Count > 0) Then ' Se carga nombre de unidad.
                            spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = datos2.Rows(0).Item("Nombre")
                        End If
                        spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.ActiveColumnIndex + 2)
                        Dim esCapturado As Boolean = spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("esCapturado").Index).Value
                        If (Not esCapturado) Then ' Si no es capturado, es decir, si es nuevo registro.
                            Dim fecha As Date = dtpFecha.Value
                            Dim listaSaldo As New List(Of String)
                            listaSaldo = ValidarFechasPosteriores(idAlmacen, idFamilia, idSubFamilia, idArticulo, fecha)
                            Dim resultado As Boolean = listaSaldo(0)
                            Dim idResultado As Integer = listaSaldo(1)
                            Dim fechaResultado As Date = listaSaldo(2)
                            If (resultado) Then ' Se validan fechas posteriores capturadas.
                                MsgBox("Este artículo tiene salida(s) con fecha posterior. La mas próxima es la no. " & idResultado & " con fecha de " & fechaResultado, MsgBoxStyle.Exclamation, "No permitido.")
                                spSalidas.ActiveSheet.ClearRange(fila, spSalidas.ActiveSheet.Columns("idArticulo").Index, 1, spSalidas.ActiveSheet.Columns.Count - 1, True)
                                spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.Columns("idArticulo").Index - 1)
                                Exit Sub
                            End If
                        End If
                    Else
                        spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idArticulo").Index, fila, spSalidas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = String.Empty
                        spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idArticulo").Index, fila, spSalidas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = String.Empty
                    spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spSalidas.ActiveSheet.Columns("cantidad").Index) Then
                fila = spSalidas.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("cantidad").Index).Value)
                If (cantidad > 0) Then
                    Dim valorPrecio As String = ALMLogicaSalidas.Funciones.ValidarLetra(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("precio").Index).Text)
                    Dim idAlmacen As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
                    Dim idFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idFamilia").Index).Value)
                    Dim idSubFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idSubFamilia").Index).Value)
                    Dim idArticulo As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idArticulo").Index).Value)
                    articulos.EIdAlmacen = idAlmacen
                    articulos.EIdFamilia = idFamilia
                    articulos.EIdSubFamilia = idSubFamilia
                    articulos.EId = idArticulo
                    Dim esCapturado As Boolean = spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("esCapturado").Index).Value
                    If (Not esCapturado) Then ' Si no es capturado, es decir, si es nuevo registro. 
                        Dim listaSaldo As New List(Of String)
                        listaSaldo = ValidarSaldoSuficiente(idAlmacen, idFamilia, idSubFamilia, idArticulo, cantidad)
                        Dim resultado As Boolean = listaSaldo(0)
                        Dim saldo As Integer = listaSaldo(1)
                        If (Not resultado) Then ' Se valida el saldo del articulo.
                            MsgBox("El saldo de este artículo no es suficiente para realizar la salida. Tienes " & saldo & " unidades.", MsgBoxStyle.Exclamation, "No permitido.")
                            spSalidas.ActiveSheet.ClearRange(fila, spSalidas.ActiveSheet.Columns("cantidad").Index, 1, 4, True)
                            spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.ActiveColumnIndex - 1)
                            Exit Sub
                        End If
                    End If
                    If (String.IsNullOrEmpty(valorPrecio)) Then ' Se cargan el precio promedio y totales. 
                        Dim precio As Double = 0
                        Dim id As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(txtId.Text)
                        precio = CalcularPrecioPromedio(idAlmacen, idFamilia, idSubFamilia, idArticulo)
                        spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("precio").Index).Value = precio
                        spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("total").Index).Value = cantidad * precio
                        Dim tipoCambio As Double = ALMLogicaSalidas.Funciones.ValidarNumeroACero(txtTipoCambio.Text)
                        spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("totalPesos").Index).Value = cantidad * precio * tipoCambio
                    End If
                Else
                    spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("cantidad").Index).Value = 0
                    spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spSalidas.ActiveSheet.Columns("precio").Index) Then
                fila = spSalidas.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("cantidad").Index).Value)
                Dim precio As Double = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("precio").Index).Value)
                Dim tipoCambio As Double = ALMLogicaSalidas.Funciones.ValidarNumeroACero(txtTipoCambio.Text)
                If (cantidad > 0) Then
                    spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("total").Index).Value = cantidad * precio
                    If (tipoCambio > 0) Then
                        spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("totalPesos").Index).Value = cantidad * precio * tipoCambio
                    End If
                ElseIf (precio = 0) Then
                    spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("precio").Index).Value = 0
                Else
                    spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("precio").Index).Value = String.Empty
                    spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spSalidas.ActiveSheet.Columns("total").Index) Then
                fila = spSalidas.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("cantidad").Index).Value)
                Dim total As Double = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("total").Index).Value)
                Dim tipoCambio As Double = ALMLogicaSalidas.Funciones.ValidarNumeroACero(txtTipoCambio.Text)
                If (cantidad > 0) Then
                    spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("precio").Index).Value = total / cantidad
                    If (tipoCambio > 0) Then
                        spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("totalPesos").Index).Value = total * tipoCambio
                    End If
                Else
                    spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("total").Index).Value = String.Empty
                    spSalidas.ActiveSheet.SetActiveCell(fila, spSalidas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            End If
        End If

    End Sub

    Private Sub CargarIdConsecutivo()

        salidas.EIdOrigen = Me.idOrigen
        salidas.EIdAlmacen = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        Dim idMaximo As Integer = salidas.ObtenerMaximoId()
        txtId.Text = idMaximo

    End Sub

    Private Sub CargarDatosEnSpreadDeCatalogos(ByVal filaCatalogos As Integer)

        If (spSalidas.ActiveSheet.ActiveColumnIndex = spSalidas.ActiveSheet.Columns("idFamilia").Index Or spSalidas.ActiveSheet.ActiveColumnIndex = spSalidas.ActiveSheet.Columns("nombreFamilia").Index) Then
            spSalidas.ActiveSheet.Cells(spSalidas.ActiveSheet.ActiveRowIndex, spSalidas.ActiveSheet.Columns("idFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spSalidas.ActiveSheet.Cells(spSalidas.ActiveSheet.ActiveRowIndex, spSalidas.ActiveSheet.Columns("nombreFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spSalidas.ActiveSheet.ActiveColumnIndex = spSalidas.ActiveSheet.Columns("idSubFamilia").Index Or spSalidas.ActiveSheet.ActiveColumnIndex = spSalidas.ActiveSheet.Columns("nombreSubFamilia").Index) Then
            spSalidas.ActiveSheet.Cells(spSalidas.ActiveSheet.ActiveRowIndex, spSalidas.ActiveSheet.Columns("idSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spSalidas.ActiveSheet.Cells(spSalidas.ActiveSheet.ActiveRowIndex, spSalidas.ActiveSheet.Columns("nombreSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spSalidas.ActiveSheet.ActiveColumnIndex = spSalidas.ActiveSheet.Columns("idArticulo").Index Or spSalidas.ActiveSheet.ActiveColumnIndex = spSalidas.ActiveSheet.Columns("nombreArticulo").Index) Then
            spSalidas.ActiveSheet.Cells(spSalidas.ActiveSheet.ActiveRowIndex, spSalidas.ActiveSheet.Columns("idArticulo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spSalidas.ActiveSheet.Cells(spSalidas.ActiveSheet.ActiveRowIndex, spSalidas.ActiveSheet.Columns("nombreArticulo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
            spSalidas.ActiveSheet.Cells(spSalidas.ActiveSheet.ActiveRowIndex, spSalidas.ActiveSheet.Columns("nombreUnidadMedida").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("unidadMedida").Index).Text
        End If

    End Sub

    Private Sub CargarDatosEnOtrosDeCatalogos(ByVal filaCatalogos As Integer)

        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen) Then
            cbAlmacenes.SelectedValue = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.moneda) Then
            cbMonedas.SelectedValue = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.tipoSalida) Then
            cbTiposSalidas.SelectedValue = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
            cbClientes.SelectedValue = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
        End If

    End Sub

    Private Sub CargarCatalogoEnSpread()

        spSalidas.Enabled = False
        Dim columna As Integer = spSalidas.ActiveSheet.ActiveColumnIndex
        If ((columna = spSalidas.ActiveSheet.Columns("idFamilia").Index) Or (columna = spSalidas.ActiveSheet.Columns("nombreFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.familia
            Dim idAlmacen As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
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
                    spSalidas.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spSalidas.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        ElseIf ((columna = spSalidas.ActiveSheet.Columns("idSubFamilia").Index) Or (columna = spSalidas.ActiveSheet.Columns("nombreSubFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.subfamilia
            Dim idAlmacen As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            Dim idFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(spSalidas.ActiveSheet.ActiveRowIndex, spSalidas.ActiveSheet.Columns("idFamilia").Index).Text)
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
                    spSalidas.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spSalidas.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        ElseIf ((columna = spSalidas.ActiveSheet.Columns("idArticulo").Index) Or (columna = spSalidas.ActiveSheet.Columns("nombreArticulo").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo
            Dim idAlmacen As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            Dim idFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(spSalidas.ActiveSheet.ActiveRowIndex, spSalidas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(spSalidas.ActiveSheet.ActiveRowIndex, spSalidas.ActiveSheet.Columns("idSubFamilia").Index).Text)
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
                    spSalidas.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spSalidas.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        Else
            spSalidas.Enabled = True
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
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.tipoSalida) Then
            tiposSalidas.EId = 0
            Dim datos As New DataTable
            datos = tiposSalidas.ObtenerListadoReporteCatalogo()
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                pnlCapturaSuperior.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.centro)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
            clientes.EId = 0
            Dim datos As New DataTable
            datos = clientes.ObtenerListadoReporteCatalogo()
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
            spCatalogos.Width = 630
            spCatalogos.ActiveSheet.Columns.Count = 3
        Else
            spCatalogos.Width = 500
            spCatalogos.ActiveSheet.Columns.Count = 2
        End If
        If (posicion = OpcionPosicion.izquierda) Then ' Izquierda.
            pnlCatalogos.Location = New Point(Me.izquierda, Me.arriba)
        ElseIf (posicion = OpcionPosicion.centro) Then ' Centrar.
            pnlCatalogos.Location = New Point(Me.anchoMitad - (spCatalogos.Width / 2), Me.arriba)
        ElseIf (posicion = OpcionPosicion.derecha) Then ' Derecha.
            pnlCatalogos.Location = New Point(Me.anchoTotal - spCatalogos.Width, Me.arriba)
        End If
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spCatalogos.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        Dim numeracion As Integer = 0
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "unidadMedida" : numeracion += 1
        End If
        spCatalogos.ActiveSheet.Columns("id").Width = 70
        spCatalogos.ActiveSheet.Columns("nombre").Width = 370
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.Columns("unidadMedida").Width = 130
        End If
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("nombre").Index).Value = "Nombre".ToUpper
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("unidadMedida").Index).Value = "Unidad".ToUpper
        End If
        pnlCatalogos.Height = spSalidas.Height
        pnlCatalogos.Width = spCatalogos.Width
        spCatalogos.Height = pnlCatalogos.Height - txtBuscarCatalogo.Height - 5
        spCatalogos.Width = pnlCatalogos.Width
        spCatalogos.ActiveSheet.Columns(0, spCatalogos.ActiveSheet.Columns.Count - 1).AllowAutoFilter = True
        spCatalogos.ActiveSheet.Columns(0, spCatalogos.ActiveSheet.Columns.Count - 1).AllowAutoSort = True
        pnlCatalogos.BringToFront()
        pnlCatalogos.Visible = True
        spCatalogos.Refresh()

    End Sub

    Private Sub VolverFocoDeCatalogos()

        pnlCapturaSuperior.Enabled = True
        spSalidas.Enabled = True
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen) Then
            AsignarFoco(cbAlmacenes)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.moneda) Then
            AsignarFoco(cbMonedas)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.tipoSalida) Then
            AsignarFoco(cbTiposSalidas)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
            AsignarFoco(cbClientes)
        Else
            AsignarFoco(spSalidas)
        End If
        txtBuscarCatalogo.Clear()
        pnlCatalogos.Visible = False

    End Sub

    Private Sub CargarSalidas()

        Me.Cursor = Cursors.WaitCursor
        salidas.EIdOrigen = cbOrigenes.SelectedValue
        Dim idAlmacen As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        salidas.EIdAlmacen = idAlmacen
        Dim id As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(txtId.Text)
        salidas.EId = id
        If (Me.idOrigen > 0 AndAlso idAlmacen > 0 AndAlso id > 0) Then
            Dim datos As New DataTable
            datos = salidas.ObtenerListadoParteSuperior()
            If (datos.Rows.Count > 0) Then
                txtIdExterno.Text = datos.Rows(0).Item("IdExterno")
                dtpFecha.Value = datos.Rows(0).Item("Fecha")
                cbMonedas.SelectedValue = datos.Rows(0).Item("IdMoneda")
                txtTipoCambio.Text = datos.Rows(0).Item("TipoCambio")
                cbTiposSalidas.SelectedValue = datos.Rows(0).Item("IdTipoSalida")
                cbClientes.SelectedValue = datos.Rows(0).Item("IdCliente")
                spSalidas.ActiveSheet.DataSource = salidas.ObtenerListadoCargaInferior()
                cantidadFilas = spSalidas.ActiveSheet.Rows.Count + 1
                FormatearSpreadSalidas()
                If (cbOrigenes.SelectedValue > 1) Then ' Si es de origen externo.
                    btnGuardar.Enabled = False
                    btnEliminar.Enabled = False
                Else
                    btnGuardar.Enabled = True
                    btnEliminar.Enabled = True
                End If
            Else
                LimpiarPantalla()
            End If
        End If
        AsignarFoco(txtIdExterno)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub FormatearSpreadSalidas()

        spSalidas.ActiveSheet.ColumnHeader.RowCount = 2
        spSalidas.ActiveSheet.ColumnHeader.Rows(0, spSalidas.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spSalidas.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spSalidas.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spSalidas.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        spSalidas.ActiveSheet.Rows.Count = cantidadFilas
        ControlarSpreadEnterASiguienteColumna(spSalidas)
        Dim numeracion As Integer = 0
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "esCapturado" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "idFamilia" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "nombreFamilia" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "idSubFamilia" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "nombreSubFamilia" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "idArticulo" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "nombreArticulo" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "nombreUnidadMedida" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "cantidad" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "precio" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "total" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "totalPesos" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "observaciones" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "factura" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "chofer" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "camion" : numeracion += 1
        spSalidas.ActiveSheet.Columns(numeracion).Tag = "noEconomico" : numeracion += 1
        spSalidas.ActiveSheet.Columns.Count = numeracion
        spSalidas.ActiveSheet.Columns("idFamilia").Width = 50
        spSalidas.ActiveSheet.Columns("nombreFamilia").Width = 140
        spSalidas.ActiveSheet.Columns("idSubFamilia").Width = 50
        spSalidas.ActiveSheet.Columns("nombreSubFamilia").Width = 140
        spSalidas.ActiveSheet.Columns("idArticulo").Width = 50
        spSalidas.ActiveSheet.Columns("nombreArticulo").Width = 200
        spSalidas.ActiveSheet.Columns("nombreUnidadMedida").Width = 80
        spSalidas.ActiveSheet.Columns("cantidad").Width = 100
        spSalidas.ActiveSheet.Columns("precio").Width = 100
        spSalidas.ActiveSheet.Columns("total").Width = 90
        spSalidas.ActiveSheet.Columns("totalPesos").Width = 90
        spSalidas.ActiveSheet.Columns("observaciones").Width = 200
        spSalidas.ActiveSheet.Columns("factura").Width = 100
        spSalidas.ActiveSheet.Columns("chofer").Width = 100
        spSalidas.ActiveSheet.Columns("camion").Width = 100
        spSalidas.ActiveSheet.Columns("noEconomico").Width = 100
        spSalidas.ActiveSheet.Columns("idFamilia").CellType = tipoEntero
        spSalidas.ActiveSheet.Columns("nombreFamilia").CellType = tipoTexto
        spSalidas.ActiveSheet.Columns("idSubFamilia").CellType = tipoEntero
        spSalidas.ActiveSheet.Columns("nombreSubFamilia").CellType = tipoTexto
        spSalidas.ActiveSheet.Columns("idArticulo").CellType = tipoEntero
        spSalidas.ActiveSheet.Columns("nombreArticulo").CellType = tipoTexto
        spSalidas.ActiveSheet.Columns("nombreUnidadMedida").CellType = tipoTexto
        spSalidas.ActiveSheet.Columns("cantidad").CellType = tipoEntero
        spSalidas.ActiveSheet.Columns("precio").CellType = tipoDoble
        spSalidas.ActiveSheet.Columns("total").CellType = tipoDoble
        spSalidas.ActiveSheet.Columns("totalPesos").CellType = tipoDoble
        spSalidas.ActiveSheet.Columns("observaciones").CellType = tipoTexto
        spSalidas.ActiveSheet.Columns("factura").CellType = tipoTexto
        spSalidas.ActiveSheet.Columns("chofer").CellType = tipoTexto
        spSalidas.ActiveSheet.Columns("camion").CellType = tipoTexto
        spSalidas.ActiveSheet.Columns("noEconomico").CellType = tipoTexto
        spSalidas.ActiveSheet.AddColumnHeaderSpanCell(0, spSalidas.ActiveSheet.Columns("idFamilia").Index, 1, 2)
        spSalidas.ActiveSheet.ColumnHeader.Cells(0, spSalidas.ActiveSheet.Columns("idFamilia").Index).Value = "F a m i l i a".ToUpper()
        spSalidas.ActiveSheet.ColumnHeader.Cells(1, spSalidas.ActiveSheet.Columns("idFamilia").Index).Value = "No. *".ToUpper()
        spSalidas.ActiveSheet.ColumnHeader.Cells(1, spSalidas.ActiveSheet.Columns("nombreFamilia").Index).Value = "Nombre *".ToUpper()
        spSalidas.ActiveSheet.AddColumnHeaderSpanCell(0, spSalidas.ActiveSheet.Columns("idSubFamilia").Index, 1, 2)
        spSalidas.ActiveSheet.ColumnHeader.Cells(0, spSalidas.ActiveSheet.Columns("idSubFamilia").Index).Value = "S u b F a m i l i a".ToUpper()
        spSalidas.ActiveSheet.ColumnHeader.Cells(1, spSalidas.ActiveSheet.Columns("idSubFamilia").Index).Value = "No. *".ToUpper()
        spSalidas.ActiveSheet.ColumnHeader.Cells(1, spSalidas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = "Nombre *".ToUpper()
        spSalidas.ActiveSheet.AddColumnHeaderSpanCell(0, spSalidas.ActiveSheet.Columns("idArticulo").Index, 1, 3)
        spSalidas.ActiveSheet.ColumnHeader.Cells(0, spSalidas.ActiveSheet.Columns("idArticulo").Index).Value = "A r t í c u l o".ToUpper()
        spSalidas.ActiveSheet.ColumnHeader.Cells(1, spSalidas.ActiveSheet.Columns("idArticulo").Index).Value = "No. *".ToUpper()
        spSalidas.ActiveSheet.ColumnHeader.Cells(1, spSalidas.ActiveSheet.Columns("nombreArticulo").Index).Value = "Nombre *".ToUpper()
        spSalidas.ActiveSheet.ColumnHeader.Cells(1, spSalidas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = "Unidad *".ToUpper()
        spSalidas.ActiveSheet.AddColumnHeaderSpanCell(0, spSalidas.ActiveSheet.Columns("cantidad").Index, 2, 1)
        spSalidas.ActiveSheet.ColumnHeader.Cells(0, spSalidas.ActiveSheet.Columns("cantidad").Index).Value = "Cantidad *".ToUpper()
        spSalidas.ActiveSheet.AddColumnHeaderSpanCell(0, spSalidas.ActiveSheet.Columns("precio").Index, 2, 1)
        spSalidas.ActiveSheet.ColumnHeader.Cells(0, spSalidas.ActiveSheet.Columns("precio").Index).Value = "Precio *".ToUpper()
        spSalidas.ActiveSheet.AddColumnHeaderSpanCell(0, spSalidas.ActiveSheet.Columns("total").Index, 2, 1)
        spSalidas.ActiveSheet.ColumnHeader.Cells(0, spSalidas.ActiveSheet.Columns("total").Index).Value = "Total *".ToUpper()
        spSalidas.ActiveSheet.AddColumnHeaderSpanCell(0, spSalidas.ActiveSheet.Columns("totalPesos").Index, 2, 1)
        spSalidas.ActiveSheet.ColumnHeader.Cells(0, spSalidas.ActiveSheet.Columns("totalPesos").Index).Value = "Total Pesos *".ToUpper()
        spSalidas.ActiveSheet.AddColumnHeaderSpanCell(0, spSalidas.ActiveSheet.Columns("observaciones").Index, 2, 1)
        spSalidas.ActiveSheet.ColumnHeader.Cells(0, spSalidas.ActiveSheet.Columns("observaciones").Index).Value = "Observaciones".ToUpper()
        spSalidas.ActiveSheet.AddColumnHeaderSpanCell(0, spSalidas.ActiveSheet.Columns("factura").Index, 2, 1)
        spSalidas.ActiveSheet.ColumnHeader.Cells(0, spSalidas.ActiveSheet.Columns("factura").Index).Value = "Factura".ToUpper()
        spSalidas.ActiveSheet.AddColumnHeaderSpanCell(0, spSalidas.ActiveSheet.Columns("chofer").Index, 2, 1)
        spSalidas.ActiveSheet.ColumnHeader.Cells(0, spSalidas.ActiveSheet.Columns("chofer").Index).Value = "Chofer".ToUpper()
        spSalidas.ActiveSheet.AddColumnHeaderSpanCell(0, spSalidas.ActiveSheet.Columns("camion").Index, 2, 1)
        spSalidas.ActiveSheet.ColumnHeader.Cells(0, spSalidas.ActiveSheet.Columns("camion").Index).Value = "Camión".ToUpper()
        spSalidas.ActiveSheet.AddColumnHeaderSpanCell(0, spSalidas.ActiveSheet.Columns("noEconomico").Index, 2, 1)
        spSalidas.ActiveSheet.ColumnHeader.Cells(0, spSalidas.ActiveSheet.Columns("noEconomico").Index).Value = "No Económico".ToUpper()
        spSalidas.ActiveSheet.Columns(spSalidas.ActiveSheet.Columns("esCapturado").Index).Visible = False
        spSalidas.Refresh()

    End Sub

    Private Sub ValidarGuardado()

        Me.Cursor = Cursors.WaitCursor
        Me.esGuardadoValido = True
        ' Parte superior.
        Dim idAlmacen As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        If (idAlmacen <= 0) Then
            cbAlmacenes.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        Dim id As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(txtId.Text)
        If (id <= 0) Then
            txtId.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        Dim idMoneda As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbMonedas.SelectedValue)
        If (idMoneda <= 0) Then
            cbMonedas.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        Dim tipoCambio As Double = ALMLogicaSalidas.Funciones.ValidarNumeroAUno(txtTipoCambio.Text)
        If (tipoCambio < 1) Then
            txtTipoCambio.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        Dim idTipoSalida As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbTiposSalidas.SelectedValue)
        If (idTipoSalida <= 0) Then
            cbTiposSalidas.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        Dim idCliente As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
        If (idCliente <= 0) Then
            cbClientes.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        ' Parte inferior.
        For fila As Integer = 0 To spSalidas.ActiveSheet.Rows.Count - 1
            Dim idFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idArticulo").Index).Text)
            If (idFamilia > 0 And idSubFamilia > 0 And idArticulo > 0) Then
                Dim cantidad As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("cantidad").Index).Text)
                If (cantidad <= 0) Then
                    spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("cantidad").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
                Dim precio As String = spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("precio").Index).Text
                Dim precio2 As Double = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("precio").Index).Text)
                If (String.IsNullOrEmpty(precio) Or precio2 < 0) Then
                    spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("precio").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
                Dim total As String = spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("total").Index).Text
                Dim total2 As Double = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("total").Index).Text)
                If (String.IsNullOrEmpty(total) Or total2 < 0) Then
                    spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("total").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
                Dim totalPesos As String = spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("totalPesos").Index).Text
                Dim totalPesos2 As Double = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("totalPesos").Index).Text)
                If (String.IsNullOrEmpty(totalPesos) Or totalPesos2 < 0) Then
                    spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("totalPesos").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
            End If
        Next
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub GuardarEditarSalidas()

        EliminarSalidas(False)
        ' Parte superior.
        Dim idAlmacen As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        Dim id As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(txtId.Text)
        Dim idExterno As String = txtIdExterno.Text
        Dim idMoneda As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbMonedas.SelectedValue)
        Dim tipoCambio As Double = ALMLogicaSalidas.Funciones.ValidarNumeroAUno(txtTipoCambio.Text)
        Dim fecha As Date = dtpFecha.Value
        Dim idTipoSalida As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbTiposSalidas.SelectedValue)
        Dim idCliente As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
        ' Parte inferior.
        For fila As Integer = 0 To spSalidas.ActiveSheet.Rows.Count - 1
            Dim idFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("idArticulo").Index).Text)
            Dim cantidad As Integer = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("cantidad").Index).Text)
            Dim precio As Double = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("precio").Index).Text)
            Dim total As Double = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("total").Index).Text)
            Dim totalPesos As Double = ALMLogicaSalidas.Funciones.ValidarNumeroACero(spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("totalPesos").Index).Text)
            Dim orden As Integer = fila
            Dim observaciones As String = spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("observaciones").Index).Text
            Dim factura As String = spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("factura").Index).Text
            Dim chofer As String = spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("chofer").Index).Text
            Dim camion As String = spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("camion").Index).Text
            Dim noEconomico As String = spSalidas.ActiveSheet.Cells(fila, spSalidas.ActiveSheet.Columns("noEconomico").Index).Text
            If (id > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0 AndAlso idMoneda > 0 AndAlso idTipoSalida > 0 AndAlso idCliente > 0) Then
                salidas.EIdOrigen = Me.idOrigen
                salidas.EIdAlmacen = idAlmacen
                salidas.EId = id
                salidas.EIdFamilia = idFamilia
                salidas.EIdSubFamilia = idSubFamilia
                salidas.EIdArticulo = idArticulo
                salidas.EIdExterno = idExterno
                salidas.EIdTipoSalida = idTipoSalida
                salidas.EIdCliente = idCliente
                salidas.EIdMoneda = idMoneda
                salidas.ETipoCambio = tipoCambio
                salidas.EFecha = fecha
                salidas.ECantidad = cantidad
                salidas.EPrecio = precio
                salidas.ETotal = total
                salidas.ETotalPesos = totalPesos
                salidas.EOrden = orden
                salidas.EObservaciones = observaciones
                salidas.EFactura = factura
                salidas.EChofer = chofer
                salidas.ECamion = camion
                salidas.ENoEconomico = noEconomico
                salidas.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        LimpiarPantalla()
        CargarIdConsecutivo()
        AsignarFoco(txtId)
        txtId.SelectAll()

    End Sub

    Private Sub EliminarSalidas(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar esta salida?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            salidas.EIdOrigen = Me.idOrigen
            salidas.EIdAlmacen = ALMLogicaSalidas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            salidas.EId = ALMLogicaSalidas.Funciones.ValidarNumeroACero(txtId.Text)
            salidas.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            LimpiarPantalla()
            CargarIdConsecutivo()
            AsignarFoco(txtId)
            txtId.SelectAll()
        End If

    End Sub

    Private Function ValidarSaldoSuficiente(ByVal idAlmacen As Integer, ByVal idFamilia As Integer, ByVal idSubFamilia As Integer, ByVal idArticulo As Integer, ByVal cantidad As Integer) As List(Of String)

        Dim valor As Boolean = False
        Dim saldo As Integer = 0
        salidas.EIdAlmacen = idAlmacen
        salidas.EIdFamilia = idFamilia
        salidas.EIdSubFamilia = idSubFamilia
        salidas.EIdArticulo = idArticulo
        Dim datos As New DataTable
        datos = salidas.ObtenerSaldos()
        If (datos.Rows.Count = 1) Then
            saldo = ALMLogicaSalidas.Funciones.ValidarNumeroACero(datos.Rows(0).Item("Cantidad").ToString)
            If (saldo >= cantidad) Then
                valor = True
            Else
                valor = False
            End If
        Else
            valor = False
        End If
        Dim lista As New List(Of String)
        lista.Add(valor)
        lista.Add(saldo)
        Return lista

    End Function

    Private Function ValidarFechasPosteriores(ByVal idAlmacen As Integer, ByVal idFamilia As Integer, ByVal idSubFamilia As Integer, ByVal idArticulo As Integer, ByVal fecha As Date) As List(Of String)

        Dim valor As Boolean = False
        Dim id As Integer = 0
        Dim fechaLocal As Date = Now
        salidas.EIdAlmacen = idAlmacen
        salidas.EIdFamilia = idFamilia
        salidas.EIdSubFamilia = idSubFamilia
        salidas.EIdArticulo = idArticulo
        salidas.EFecha = fecha
        Dim datos As New DataTable
        datos = salidas.ValidarFechasPosteriores()
        If (datos.Rows.Count > 0) Then
            valor = True
            id = datos.Rows(0).Item("Id")
            fechaLocal = datos.Rows(0).Item("Fecha")
        Else
            valor = False
        End If
        Dim lista As New List(Of String)
        lista.Add(valor)
        lista.Add(id)
        lista.Add(fechaLocal)
        Return lista

    End Function

    Private Sub FormatearSpreadListados(ByVal posicion As Integer)

        spListados.Width = 500
        spListados.ActiveSheet.Columns.Count = 5
        If (posicion = OpcionPosicion.izquierda) Then ' Izquierda.
            spListados.Location = New Point(Me.izquierda, Me.arriba)
        ElseIf (posicion = OpcionPosicion.centro) Then ' Centrar.
            spListados.Location = New Point(Me.anchoMitad - (spListados.Width / 2), Me.arriba)
        ElseIf (posicion = OpcionPosicion.derecha) Then ' Derecha.
            spListados.Location = New Point(Me.anchoTotal - spListados.Width, Me.arriba)
        End If
        spListados.ActiveSheet.ColumnHeader.Rows(0).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spListados.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spListados.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        Dim numeracion As Integer = 0
        spListados.ActiveSheet.Columns(numeracion).Tag = "idOrigen" : numeracion += 1
        spListados.ActiveSheet.Columns(numeracion).Tag = "nombreOrigen" : numeracion += 1
        spListados.ActiveSheet.Columns(numeracion).Tag = "idAlmacen" : numeracion += 1
        spListados.ActiveSheet.Columns(numeracion).Tag = "nombreAlmacen" : numeracion += 1
        spListados.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spListados.ActiveSheet.Columns("idOrigen").Width = 70
        spListados.ActiveSheet.Columns("nombreOrigen").Width = 100
        spListados.ActiveSheet.Columns("idAlmacen").Width = 70
        spListados.ActiveSheet.Columns("nombreAlmacen").Width = 120
        spListados.ActiveSheet.Columns("id").Width = 70
        spListados.ActiveSheet.ColumnHeader.Cells(0, spListados.ActiveSheet.Columns("idOrigen").Index).Value = "No.".ToUpper
        spListados.ActiveSheet.ColumnHeader.Cells(0, spListados.ActiveSheet.Columns("nombreOrigen").Index).Value = "Origen".ToUpper
        spListados.ActiveSheet.ColumnHeader.Cells(0, spListados.ActiveSheet.Columns("idAlmacen").Index).Value = "No.".ToUpper
        spListados.ActiveSheet.ColumnHeader.Cells(0, spListados.ActiveSheet.Columns("nombreAlmacen").Index).Value = "Almacén".ToUpper
        spListados.ActiveSheet.ColumnHeader.Cells(0, spListados.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper
        spListados.Height = spSalidas.Height
        spListados.Width = spListados.Width
        spListados.ActiveSheet.Columns(0, spListados.ActiveSheet.Columns.Count - 1).AllowAutoFilter = True
        spListados.ActiveSheet.Columns(0, spListados.ActiveSheet.Columns.Count - 1).AllowAutoSort = True
        spListados.BringToFront()
        spListados.Visible = True
        spListados.Refresh()

    End Sub

    Private Sub CargarListados()

        If (spListados.Visible) Then
            spListados.Visible = False
            spSalidas.Enabled = True
        Else
            spSalidas.Enabled = False
            salidas.EIdOrigen = 0
            salidas.EIdAlmacen = 0
            salidas.EId = 0
            Dim datos As New DataTable
            datos = salidas.ObtenerListado()
            If (datos.Rows.Count > 0) Then
                spListados.ActiveSheet.DataSource = datos
            Else
                spListados.ActiveSheet.DataSource = Nothing
                spListados.ActiveSheet.Rows.Count = 0
                spSalidas.Enabled = True
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
        spSalidas.Enabled = True
        CargarSalidas()
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
        cliente = 5
        moneda = 6
        tipoSalida = 7

    End Enum

#End Region

End Class