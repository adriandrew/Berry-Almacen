Imports System.IO
Imports System.ComponentModel

Public Class Principal

    ' Variables de objetos de entidades.
    Public usuarios As New EntidadesEntradas.Usuarios()
    Public entradas As New EntidadesEntradas.Entradas()
    Public almacenes As New EntidadesEntradas.Almacenes()
    Public familias As New EntidadesEntradas.Familias()
    Public subFamilias As New EntidadesEntradas.SubFamilias()
    Public articulos As New EntidadesEntradas.Articulos()
    Public unidadesMedidas As New EntidadesEntradas.UnidadesMedidas()
    Public proveedores As New EntidadesEntradas.Proveedores()
    Public monedas As New EntidadesEntradas.Monedas()
    Public tiposCambios As New EntidadesEntradas.TiposCambios()
    Public tiposEntradas As New EntidadesEntradas.TiposEntradas()
    Public tiposSalidas As New EntidadesEntradas.TiposSalidas()
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
    Public Shared tipoLetraSpread As String = "Microsoft Sans Serif" : Public Shared tamañoLetraSpread As Integer = 11
    Public Shared alturaFilasEncabezadosGrandesSpread As Integer = 35 : Public Shared alturaFilasEncabezadosMedianosSpread As Integer = 28
    Public Shared alturaFilasEncabezadosChicosSpread As Integer = 22 : Public Shared alturaFilasSpread As Integer = 20
    Public Shared colorAreaGris = Color.White
    ' Variables de eventos de spread.
    Public filaAlmacen As Integer = -1 : Public filaFamilia As Integer = -1 : Public filaSubFamilia As Integer = -1
    ' Variables generales.
    Public nombreEstePrograma As String = String.Empty 
    Public estaCerrando As Boolean = False
    Public ejecutarProgramaPrincipal As New ProcessStartInfo()
    Public prefijoBaseDatosAlmacen As String = "ALM" & "_"
    Public cantidadFilas As Integer = 1
    ' Variable de desarrollo.
    Public esDesarrollo As Boolean = True

#Region "Eventos"

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor
        Centrar()
        CargarNombrePrograma()
        AsignarTooltips()
        ConfigurarConexiones()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Principal_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Me.Cursor = Cursors.WaitCursor
        Me.Enabled = False
        'If (Not ValidarAccesoTotal()) Then
        '    Salir()
        'End If
        CargarEncabezados()
        CargarTitulosDirectorio() 
        FormatearSpread()
        FormatearSpreadEntradas()
        CargarTiposEntradas()
        CargarMonedas()
        Me.Enabled = True
        AsignarFoco(txtIdAlmacen)
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
        Desvanecer()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click

        Salir()

    End Sub

    Private Sub spEntradas_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spEntradas.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spCatalogos)
        End If

    End Sub

    Private Sub spEntradas_KeyDown(sender As Object, e As KeyEventArgs) Handles spEntradas.KeyDown

        If (e.KeyData = Keys.F6) Then ' Eliminar un registro.
            If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                'Dim fila As Integer = spAlmacen.ActiveSheet.ActiveRowIndex
                'Dim id As Integer = 0
                'Dim idUsuario As Integer = LogicaCatalogos.Funciones.ValidarNumero(spAlmacen.ActiveSheet.Cells(fila, spAlmacen.ActiveSheet.Columns("id").Index).Text)
                'usuarios.EId = idUsuario
                'Dim tieneDatos As Boolean = usuarios.ValidarActividadPorId()
                'If (tieneDatos) Then
                '    MessageBox.Show("No se puede eliminar este registro, ya que contiene actividades capturadas.", "No permitido.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                'Else
                EliminarRegistro(spEntradas)
                'End If
            End If
        ElseIf (e.KeyData = Keys.Enter) Then ' Validar registros.
            ControlarSpreadEnter(spEntradas)
        ElseIf (e.KeyData = Keys.Escape) Then
            AsignarFoco(txtTipoCambio)
        End If

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        GuardarEditarEntradas()

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        EliminarEntradas(True)

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

        'Dim fila As Integer = e.Row 
        'spAlmacenes.ActiveSheet.Cells(spAlmacenes.ActiveSheet.ActiveRowIndex, spAlmacenes.ActiveSheet.Columns("idUnidadMedida").Index).Text = spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("id").Index).Text
        'spAlmacenes.ActiveSheet.Cells(spAlmacenes.ActiveSheet.ActiveRowIndex, spAlmacenes.ActiveSheet.Columns("nombreUnidadMedida").Index).Text = spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("nombre").Index).Text

    End Sub

    Private Sub spCatalogos_CellDoubleClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spCatalogos.CellDoubleClick

        VolverFocoCatalogos()

    End Sub

    Private Sub spCatalogos_KeyDown(sender As Object, e As KeyEventArgs) Handles spCatalogos.KeyDown

        If (e.KeyCode = Keys.Escape) Then
            VolverFocoCatalogos()
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

#End Region

#Region "Métodos"

#Region "Básicos"

    Private Sub Salir()

        Application.Exit()

    End Sub

    Private Sub MostrarAyuda()

        Dim pnlAyuda As New Panel()
        Dim txtAyuda As New TextBox()
        If (pnlContenido.Controls.Find("pnlAyuda", True).Count = 0) Then
            pnlAyuda.Name = "pnlAyuda" : Application.DoEvents()
            pnlAyuda.Visible = False : Application.DoEvents()
            pnlContenido.Controls.Add(pnlAyuda) : Application.DoEvents()
            txtAyuda.Name = "txtAyuda" : Application.DoEvents()
            pnlAyuda.Controls.Add(txtAyuda) : Application.DoEvents()
        Else
            pnlAyuda = pnlContenido.Controls.Find("pnlAyuda", False)(0) : Application.DoEvents()
            txtAyuda = pnlAyuda.Controls.Find("txtAyuda", False)(0) : Application.DoEvents()
        End If
        If (Not pnlAyuda.Visible) Then
            pnlCuerpo.Visible = False : Application.DoEvents()
            pnlAyuda.Visible = True : Application.DoEvents()
            pnlAyuda.Size = pnlCuerpo.Size : Application.DoEvents()
            pnlAyuda.Location = pnlCuerpo.Location : Application.DoEvents()
            pnlContenido.Controls.Add(pnlAyuda) : Application.DoEvents()
            txtAyuda.ScrollBars = ScrollBars.Both : Application.DoEvents()
            txtAyuda.Multiline = True : Application.DoEvents()
            txtAyuda.Width = pnlAyuda.Width - 10 : Application.DoEvents()
            txtAyuda.Height = pnlAyuda.Height - 10 : Application.DoEvents()
            txtAyuda.Location = New Point(5, 5) : Application.DoEvents()
            txtAyuda.Text = "Sección de Ayuda: " & vbNewLine & vbNewLine & "* Teclas básicas: " & vbNewLine & "F5 sirve para mostrar catálogos. " & vbNewLine & "F6 sirve para eliminar un registro únicamente. " & vbNewLine & "Escape sirve para ocultar catálogos que se encuentren desplegados. " & vbNewLine & vbNewLine & "* Catálogos desplegados: " & vbNewLine & "Cuando se muestra algún catálogo, al seleccionar alguna opción de este, se va mostrando en tiempo real en la captura de donde se originó. Cuando se le da doble clic en alguna opción o a la tecla escape se oculta dicho catálogo. " & vbNewLine & vbNewLine & "* Areas: " & vbNewLine & "En esta pestaña se capturarán todas las areas necesarias. " & vbNewLine & "Existen los botones de guardar/editar y eliminar todo dependiendo lo que se necesite hacer. " & vbNewLine & vbNewLine & "* Usuarios: " & vbNewLine & "En esta parte se capturarán todos los usuarios. " & vbNewLine & "Descripción de los datos que pide: " & vbNewLine & "- Contraseña: esta permite letras y/o números sin ningun problema, no existen restricciones de ningún tipo." & vbNewLine & "- Nivel: 0 es para acceso a todos los programas, excepto los de gerencia. 1 es para los módulos, en este caso como es uno solo, no aplica. 2 es para los programas, si se le da doble clic aparecerán los programas para seleccionar cuales se permitirán y cuales no. 3 es para subprogramas, no aplica en este caso. " & vbNewLine & "- Acceso Total: es solamente para usuarios de gerencia. " & vbNewLine & "Existen los botones de guardar/editar y eliminar todo dependiendo lo que se necesite hacer. " & vbNewLine & vbNewLine & "* Correos: " & vbNewLine & "En este apartado se capturarán todos los usuarios con sus respectivos correos para enviarles sus notificaciones de actividades pendientes que se encuentran retrasadas en tiempos. " & vbNewLine & "Existen los botones de guardar/editar y eliminar todo dependiendo lo que se necesite hacer. " : Application.DoEvents()
            pnlAyuda.Controls.Add(txtAyuda) : Application.DoEvents()
        Else
            pnlCuerpo.Visible = True : Application.DoEvents()
            pnlAyuda.Visible = False : Application.DoEvents()
        End If

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

        If ((Not LogicaEntradas.Usuarios.accesoTotal) Or (LogicaEntradas.Usuarios.accesoTotal = 0) Or (LogicaEntradas.Usuarios.accesoTotal = False)) Then
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

    End Sub

    Private Sub CargarNombrePrograma()

        Me.nombreEstePrograma = Me.Text

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
            LogicaEntradas.Directorios.id = 1
            LogicaEntradas.Directorios.instanciaSql = "BERRY1-DELL\SQLEXPRESS2008"
            LogicaEntradas.Directorios.usuarioSql = "AdminBerry"
            LogicaEntradas.Directorios.contrasenaSql = "@berry2017"
            LogicaEntradas.Usuarios.id = 1
        Else
            LogicaEntradas.Directorios.ObtenerParametros()
            LogicaEntradas.Usuarios.ObtenerParametros()
        End If
        LogicaEntradas.Programas.bdCatalogo = "Catalogo" & LogicaEntradas.Directorios.id
        LogicaEntradas.Programas.bdConfiguracion = "Configuracion" & LogicaEntradas.Directorios.id
        LogicaEntradas.Programas.bdAlmacen = "Almacen" & LogicaEntradas.Directorios.id
        EntidadesEntradas.BaseDatos.ECadenaConexionCatalogo = LogicaEntradas.Programas.bdCatalogo
        EntidadesEntradas.BaseDatos.ECadenaConexionConfiguracion = LogicaEntradas.Programas.bdConfiguracion
        EntidadesEntradas.BaseDatos.ECadenaConexionAlmacen = LogicaEntradas.Programas.bdAlmacen
        EntidadesEntradas.BaseDatos.AbrirConexionCatalogo()
        EntidadesEntradas.BaseDatos.AbrirConexionConfiguracion()
        EntidadesEntradas.BaseDatos.AbrirConexionAlmacen()
        ConsultarInformacionUsuario()
        CargarPrefijoBaseDatosAlmacen()

    End Sub

    Private Sub CargarPrefijoBaseDatosAlmacen()

        LogicaEntradas.Programas.prefijoBaseDatosAlmacen = Me.prefijoBaseDatosAlmacen

    End Sub

    Private Sub ConsultarInformacionUsuario()

        Dim lista As New List(Of EntidadesEntradas.Usuarios)
        usuarios.EId = LogicaEntradas.Usuarios.id
        lista = usuarios.ObtenerListado()
        If (lista.Count > 0) Then
            LogicaEntradas.Usuarios.id = lista(0).EId
            LogicaEntradas.Usuarios.nombre = lista(0).ENombre
            LogicaEntradas.Usuarios.contrasena = lista(0).EContrasena
            LogicaEntradas.Usuarios.nivel = lista(0).ENivel
            LogicaEntradas.Usuarios.accesoTotal = lista(0).EAccesoTotal
        End If

    End Sub

    Private Sub CargarTitulosDirectorio()

        Me.Text = "Programa:  " + Me.nombreEstePrograma + "              Directorio:  " + LogicaEntradas.Directorios.nombre + "              Usuario:  " + LogicaEntradas.Usuarios.nombre

    End Sub

    Private Sub CargarEncabezados()

        lblEncabezadoPrograma.Text = "Programa: " + Me.Text
        lblEncabezadoEmpresa.Text = "Directorio: " + LogicaEntradas.Directorios.nombre
        lblEncabezadoUsuario.Text = "Usuario: " + LogicaEntradas.Usuarios.nombre

    End Sub

    Private Sub AbrirPrograma(nombre As String, salir As Boolean)

        If (Me.esDesarrollo) Then
            Exit Sub
        End If
        ejecutarProgramaPrincipal.UseShellExecute = True
        ejecutarProgramaPrincipal.FileName = nombre & Convert.ToString(".exe")
        ejecutarProgramaPrincipal.WorkingDirectory = Directory.GetCurrentDirectory()
        ejecutarProgramaPrincipal.Arguments = LogicaEntradas.Directorios.id.ToString().Trim().Replace(" ", "|") & " " & LogicaEntradas.Directorios.nombre.ToString().Trim().Replace(" ", "|") & " " & LogicaEntradas.Directorios.descripcion.ToString().Trim().Replace(" ", "|") & " " & LogicaEntradas.Directorios.rutaLogo.ToString().Trim().Replace(" ", "|") & " " & LogicaEntradas.Directorios.esPredeterminado.ToString().Trim().Replace(" ", "|") & " " & LogicaEntradas.Directorios.instanciaSql.ToString().Trim().Replace(" ", "|") & " " & LogicaEntradas.Directorios.usuarioSql.ToString().Trim().Replace(" ", "|") & " " & LogicaEntradas.Directorios.contrasenaSql.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de directorios, indice 9 ;)".Replace(" ", "|") & " " & LogicaEntradas.Usuarios.id.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de usuario, indice 11 ;)".Replace(" ", "|")
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
        Me.arriba = 0
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
         
        txtIdExterno.Clear()
        cbMonedas.SelectedIndex = 0
        cbTiposEntradas.SelectedIndex = 0
        txtTipoCambio.Clear()  
        spEntradas.ActiveSheet.DataSource = Nothing
        spEntradas.ActiveSheet.Rows.Count = 1
        Application.DoEvents()

    End Sub

    Private Sub CargarMonedas()

        cbMonedas.DataSource = monedas.ObtenerListadoReporte()
        cbMonedas.DisplayMember = "Nombre"
        cbMonedas.ValueMember = "Id" 
        CargarTiposCambios()

    End Sub

    Private Sub CargarTiposCambios()

        If (cbMonedas.Items.Count > 0) Then
            tiposCambios.EFecha = IIf(IsDate(dtpFecha.Value), dtpFecha.Value, Today)
            tiposCambios.EIdMoneda = IIf(IsNumeric(cbMonedas.SelectedValue), cbMonedas.SelectedValue, 1)
            Dim lista As New List(Of EntidadesEntradas.TiposCambios)
            lista = tiposCambios.ObtenerListado()
            Dim valor As Double = 1
            If (lista.Count > 0) Then
                valor = lista(0).EValor
            End If
            txtTipoCambio.Text = valor
        End If

    End Sub

    Private Sub CargarTiposEntradas()

        cbTiposEntradas.DataSource = tiposEntradas.ObtenerListadoReporte()
        cbTiposEntradas.DisplayMember = "Nombre"
        cbTiposEntradas.ValueMember = "Id"

    End Sub

    Private Sub OcultarSpreads()

        spCatalogos.Hide()
        Application.DoEvents()

    End Sub

    Private Sub FormatearSpread()

        ' Se cargan tipos de datos de spread.
        CargarTiposDeDatos()
        ' Se cargan las opciones generales. 
        spCatalogos.Visible = False : Application.DoEvents()
        spEntradas.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell : Application.DoEvents()
        spCatalogos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Midnight : Application.DoEvents()
        spEntradas.ActiveSheet.GrayAreaBackColor = Principal.colorAreaGris : Application.DoEvents()
        spEntradas.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular) : Application.DoEvents()
        spCatalogos.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread : Application.DoEvents()
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread : Application.DoEvents()
        spEntradas.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread : Application.DoEvents()
        spCatalogos.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread : Application.DoEvents()
        spEntradas.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded : Application.DoEvents()
        spEntradas.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded : Application.DoEvents()

    End Sub

    Private Sub EliminarRegistro(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.Rows.Remove(spread.ActiveSheet.ActiveRowIndex, 1)

    End Sub

    Private Sub ControlarSpreadEnter(ByVal spread As FarPoint.Win.Spread.FpSpread)

        Dim columnaActiva As Integer = spread.ActiveSheet.ActiveColumnIndex
        If (columnaActiva = spread.ActiveSheet.Columns.Count - 1) Then
            spread.ActiveSheet.Rows.Count += 1
        End If
        'If (spread.Name = spEntradas.Name) Then 
        '    Dim fila As Integer = 0
        '    If (columnaActiva = spEntradas.ActiveSheet.Columns("idProveedor").Index) Then
        '        fila = spEntradas.ActiveSheet.ActiveRowIndex
        '        Dim idProveedor As Integer = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idProveedor").Index).Value
        '        proveedores.EId = idProveedor
        '        If (idProveedor > 0) Then
        '            Dim lista As New List(Of EntidadesEntradas.UnidadesMedidas)
        '            lista = proveedores.ObtenerListado()
        '            If (lista.Count > 0) Then
        '                spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("nombreProveedor").Index).Value = lista(0).ENombre
        '            End If
        '        End If
        '    End If
        'End If

    End Sub

    Private Sub LimpiarSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.ClearRange(0, 0, spread.ActiveSheet.Rows.Count, spread.ActiveSheet.Columns.Count, True) : Application.DoEvents()

    End Sub

    Private Sub CargarIdConsecutivo()

        entradas.EIdAlmacen = LogicaEntradas.Funciones.ValidarNumeroACero(txtIdAlmacen.Text)
        Dim idMaximo As Integer = entradas.ObtenerMaximoId()
        txtId.Text = idMaximo

    End Sub

    Private Sub CargarCatalogoUnidadesMedidas()

        unidadesMedidas.EId = 0
        spCatalogos.ActiveSheet.DataSource = unidadesMedidas.ObtenerListado() : Application.DoEvents()
        AsignarFoco(spCatalogos)

    End Sub

    Private Sub FormatearSpreadCatalogoUnidadesMedidas(ByVal izquierda As Boolean)

        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold) : Application.DoEvents()
        spCatalogos.Width = 300 : Application.DoEvents()
        If (izquierda) Then
            spCatalogos.Location = New Point(Me.izquierda, Me.arriba) : Application.DoEvents()
        Else
            spCatalogos.Location = New Point(Me.anchoTotal - spCatalogos.Width, Me.arriba) : Application.DoEvents()
        End If
        spCatalogos.Visible = True : Application.DoEvents()
        spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never : Application.DoEvents()
        spCatalogos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded : Application.DoEvents()
        spCatalogos.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect : Application.DoEvents()
        spCatalogos.Height = Me.altoTotal : Application.DoEvents()
        Dim numeracion As Integer = 0
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spCatalogos.ActiveSheet.Columns("id").Width = 50 : Application.DoEvents()
        spCatalogos.ActiveSheet.Columns("nombre").Width = 210 : Application.DoEvents()
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper : Application.DoEvents()
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("nombre").Index).Value = "Nombre".ToUpper : Application.DoEvents()
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosMedianosSpread : Application.DoEvents()

    End Sub

    Private Sub VolverFocoCatalogos()

        spEntradas.Enabled = True
        AsignarFoco(spEntradas)
        spEntradas.ActiveSheet.SetActiveCell(spEntradas.ActiveSheet.ActiveRowIndex, spEntradas.ActiveSheet.Columns("idUnidadMedida").Index + 2)
        spCatalogos.Visible = False

    End Sub

    Private Sub CargarEntradas()

        Me.Cursor = Cursors.WaitCursor
        entradas.EIdAlmacen = LogicaEntradas.Funciones.ValidarNumeroACero(txtIdAlmacen.Text)
        entradas.EId = LogicaEntradas.Funciones.ValidarNumeroACero(txtId.Text)
        Dim lista As New List(Of EntidadesEntradas.Entradas)
        lista = entradas.ObtenerListado
        If (lista.Count > 0) Then
            cbMonedas.SelectedValue = lista(0).EIdMoneda
            txtTipoCambio.Text = lista(0).ETipoCambio
            txtIdExterno.Text = lista(0).EIdExterno
            dtpFecha.Value = lista(0).EFecha
            cbTiposEntradas.SelectedValue = lista(0).EIdTipoEntrada
            spEntradas.ActiveSheet.DataSource = entradas.ObtenerListadoReporte()
            cantidadFilas = spEntradas.ActiveSheet.Rows.Count + 1
            FormatearSpreadEntradas()
        Else
            LimpiarPantalla()
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub FormatearSpreadEntradas()

        spEntradas.ActiveSheet.ColumnHeader.RowCount = 2 : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Rows(0, spEntradas.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold) : Application.DoEvents() : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread : Application.DoEvents()
        spEntradas.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal : Application.DoEvents()
        spEntradas.ActiveSheet.Rows.Count = cantidadFilas
        ControlarSpreadEnterASiguienteColumna(spEntradas)
        Dim numeracion As Integer = 0
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "idFamilia" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "nombreFamilia" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "idSubFamilia" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "nombreSubFamilia" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "idArticulo" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "nombreArticulo" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "idProveedor" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "nombreProveedor" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "cantidad" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "precioUnitario" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "total" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "totalPesos" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "observaciones" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "factura" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "chofer" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "camion" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "noEconomico" : numeracion += 1
        spEntradas.ActiveSheet.Columns.Count = numeracion
        spEntradas.ActiveSheet.Columns("idFamilia").Width = 50 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("nombreFamilia").Width = 150 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("idSubFamilia").Width = 50 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("nombreSubFamilia").Width = 150 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("idArticulo").Width = 50 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("nombreArticulo").Width = 150 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("idProveedor").Width = 50 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("nombreProveedor").Width = 150 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("cantidad").Width = 150 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("precioUnitario").Width = 150 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("total").Width = 150 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("totalPesos").Width = 150 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("observaciones").Width = 150 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("factura").Width = 150 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("chofer").Width = 150 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("camion").Width = 150 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("noEconomico").Width = 150 : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("idFamilia").CellType = tipoEntero : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("nombreFamilia").CellType = tipoTexto : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("idSubFamilia").CellType = tipoEntero : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("nombreSubFamilia").CellType = tipoTexto : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("idArticulo").CellType = tipoEntero : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("nombreArticulo").CellType = tipoTexto : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("idProveedor").CellType = tipoEntero : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("nombreProveedor").CellType = tipoTexto : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("cantidad").CellType = tipoEntero : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("precioUnitario").CellType = tipoDoble : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("total").CellType = tipoDoble : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("totalPesos").CellType = tipoDoble : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("observaciones").CellType = tipoTexto : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("factura").CellType = tipoTexto : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("chofer").CellType = tipoTexto : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("camion").CellType = tipoTexto : Application.DoEvents()
        spEntradas.ActiveSheet.Columns("noEconomico").CellType = tipoTexto : Application.DoEvents()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("idFamilia").Index, 1, 2) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("idFamilia").Index).Value = "F a m i l i a".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("idFamilia").Index).Value = "No. *".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("nombreFamilia").Index).Value = "Nombre *".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("idSubFamilia").Index, 1, 2) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Value = "S u b F a m i l i a".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Value = "No. *".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = "Nombre *".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("idArticulo").Index, 1, 2) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("idArticulo").Index).Value = "A r t í c u l o".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("idArticulo").Index).Value = "No. *".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("nombreArticulo").Index).Value = "Nombre *".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("idProveedor").Index, 1, 2) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("idProveedor").Index).Value = "P r o v e e d o r".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("idProveedor").Index).Value = "No. *".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(1, spEntradas.ActiveSheet.Columns("nombreProveedor").Index).Value = "Nombre *".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("cantidad").Index, 2, 1) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("cantidad").Index).Value = "Cantidad".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("precioUnitario").Index, 2, 1) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("precioUnitario").Index).Value = "Precio Unitario".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("total").Index, 2, 1) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("total").Index).Value = "Total".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("totalPesos").Index, 2, 1) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("totalPesos").Index).Value = "Total Pesos".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("observaciones").Index, 2, 1) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("observaciones").Index).Value = "Observaciones".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("factura").Index, 2, 1) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("factura").Index).Value = "Factura".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("chofer").Index, 2, 1) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("chofer").Index).Value = "Chofer".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("camion").Index, 2, 1) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("camion").Index).Value = "Camión".ToUpper() : Application.DoEvents()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("noEconomico").Index, 2, 1) : Application.DoEvents()
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("noEconomico").Index).Value = "No Económico".ToUpper() : Application.DoEvents()

    End Sub

    Private Sub GuardarEditarEntradas()

        Me.Cursor = Cursors.WaitCursor
        EliminarEntradas(False)
        For fila As Integer = 0 To spEntradas.ActiveSheet.Rows.Count - 1
            Dim idMoneda As Integer = LogicaEntradas.Funciones.ValidarNumeroACero(cbMonedas.SelectedValue)
            Dim tipoCambio As Double = LogicaEntradas.Funciones.ValidarNumeroAUno(txtTipoCambio.Text)
            Dim idAlmacen As Integer = LogicaEntradas.Funciones.ValidarNumeroACero(txtIdAlmacen.Text)
            Dim id As Integer = LogicaEntradas.Funciones.ValidarNumeroACero(txtId.Text)
            Dim idExterno As String = txtIdExterno.Text
            Dim fecha As Date = dtpFecha.Value
            Dim idTipoEntrada As Integer = LogicaEntradas.Funciones.ValidarNumeroACero(cbTiposEntradas.SelectedValue)
            Dim idFamilia As Integer = LogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = LogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = LogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index).Text)
            Dim idProveedor As Integer = LogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idProveedor").Index).Text)
            Dim cantidad As Integer = LogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).Text)
            Dim precioUnitario As Double = LogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precioUnitario").Index).Text)
            Dim total As Double = LogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Text)
            Dim totalPesos As Double = LogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).Text)
            Dim orden As Integer = fila
            Dim observaciones As String = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("observaciones").Index).Text
            Dim factura As String = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("factura").Index).Text
            Dim chofer As String = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("chofer").Index).Text
            Dim camion As String = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("camion").Index).Text
            Dim noEconomico As String = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("noEconomico").Index).Text
            If (id > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0 AndAlso idMoneda > 0 AndAlso idTipoEntrada > 0 AndAlso idProveedor > 0) Then
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
                entradas.EPrecioUnitario = precioUnitario
                entradas.ETotal = total
                entradas.ETotalPesos = totalPesos
                entradas.EOrden = fila
                entradas.EObservaciones = observaciones
                entradas.EFactura = factura
                entradas.EChofer = chofer
                entradas.ECamion = camion
                entradas.ENoEconomico = noEconomico
                entradas.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        'CargarEntradas()
        LimpiarPantalla()
        CargarIdConsecutivo()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub EliminarEntradas(ByVal conMensaje As Boolean)

        Me.Cursor = Cursors.WaitCursor
        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar esta entrada?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            entradas.EIdAlmacen = LogicaEntradas.Funciones.ValidarNumeroACero(txtIdAlmacen.Text)
            entradas.EId = LogicaEntradas.Funciones.ValidarNumeroACero(txtId.Text)
            entradas.Eliminar()
        End If
        If (conMensaje) Then
            'CargarEntradas()
            LimpiarPantalla()
            CargarIdConsecutivo()
        End If
        Me.Cursor = Cursors.Default

    End Sub

#End Region

#End Region

    Private Sub txtIdAlmacen_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdAlmacen.KeyDown

        If (e.KeyData = Keys.Enter) Then
            Dim idAlmacen As Integer = LogicaEntradas.Funciones.ValidarNumeroACero(txtIdAlmacen.Text)
            almacenes.EId = idAlmacen
            Dim lista As List(Of EntidadesEntradas.Almacenes)
            lista = almacenes.ObtenerListado()
            Dim nombre As String = String.Empty
            If (lista.Count > 0) Then
                nombre = lista(0).ENombre()
            End If
            txtNombreAlmacen.Text = nombre
            If (String.IsNullOrEmpty(nombre)) Then
                txtIdAlmacen.Clear()
            Else
                CargarIdConsecutivo()
                AsignarFoco(txtId)
                txtId.SelectAll()
            End If
        End If

    End Sub

    Private Sub txtId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtId.KeyDown

        If (e.KeyData = Keys.Enter) Then
            If (IsNumeric(txtId.Text)) Then
                CargarEntradas() 
                AsignarFoco(txtIdExterno)
            Else
                LimpiarPantalla()
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            AsignarFoco(txtIdAlmacen)
        End If

    End Sub

    Private Sub txtIdExterno_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdExterno.KeyDown

        If (e.KeyData = Keys.Enter) Then  
            AsignarFoco(cbMonedas)
        ElseIf (e.KeyData = Keys.Escape) Then
            AsignarFoco(txtId)
        End If

    End Sub

    Private Sub dtpFecha_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpFecha.KeyDown

        If (e.KeyData = Keys.Enter) Then
            AsignarFoco(cbTiposEntradas)
        ElseIf (e.KeyData = Keys.Escape) Then
            AsignarFoco(cbMonedas)
        End If

    End Sub

    Private Sub cbMonedas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMonedas.SelectedIndexChanged

        CargarTiposCambios()

    End Sub

    Private Sub cbMonedas_KeyDown(sender As Object, e As KeyEventArgs) Handles cbMonedas.KeyDown

        If (e.KeyData = Keys.Enter) Then 
            AsignarFoco(dtpFecha)
        ElseIf (e.KeyData = Keys.Escape) Then
            AsignarFoco(txtIdExterno)
        End If

    End Sub

    Private Sub cbTiposEntradas_KeyDown(sender As Object, e As KeyEventArgs) Handles cbTiposEntradas.KeyDown

        If (e.KeyData = Keys.Enter) Then 
            AsignarFoco(txtTipoCambio)
        ElseIf (e.KeyData = Keys.Escape) Then
            AsignarFoco(dtpFecha)
        End If

    End Sub

    Private Sub txtTipoCambio_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTipoCambio.KeyDown

        If (e.KeyData = Keys.Enter) Then 
            AsignarFoco(spEntradas)
        ElseIf (e.KeyData = Keys.Escape) Then
            AsignarFoco(cbTiposEntradas)
        End If

    End Sub

End Class