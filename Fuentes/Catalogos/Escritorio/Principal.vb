Imports System.IO
Imports System.ComponentModel

Public Class Principal

    Public usuarios As New EntidadesCatalogos.Usuarios()
    Public almacenes As New EntidadesCatalogos.Almacenes()
    Public familias As New EntidadesCatalogos.Familias()
    Public tipoTexto As New FarPoint.Win.Spread.CellType.TextCellType()
    Public tipoTextoContrasena As New FarPoint.Win.Spread.CellType.TextCellType()
    Public tipoEntero As New FarPoint.Win.Spread.CellType.NumberCellType()
    Public tipoDoble As New FarPoint.Win.Spread.CellType.NumberCellType()
    Public tipoPorcentaje As New FarPoint.Win.Spread.CellType.PercentCellType()
    Public tipoHora As New FarPoint.Win.Spread.CellType.DateTimeCellType()
    Public tipoFecha As New FarPoint.Win.Spread.CellType.DateTimeCellType()
    Public tipoBooleano As New FarPoint.Win.Spread.CellType.CheckBoxCellType()
    Public opcionSeleccionada As Integer = 0
    Dim ejecutarProgramaPrincipal As New ProcessStartInfo()
    Public estaCerrando As Boolean = False
    ' Variables de tamaños y posiciones de spreads.
    Public anchoTotal As Integer = 0 : Public altoTotal As Integer = 0
    Public anchoMitad As Integer = 0 : Public altoMitad As Integer = 0
    Public anchoTercio As Integer = 0
    Public izquierda As Integer = 0 : Public arriba As Integer = 0
    Public prefijoDeAlmacen As String = "ALM" & "_"
    ' Variables de spread.
    Public Shared tipoLetraSpread As String = "Microsoft Sans Serif"
    Public Shared tamañoLetraSpread As Integer = 12
    Public Shared alturaEncabezadosGrandeSpread As Integer = 45
    Public Shared alturaEncabezadosChicoSpread As Integer = 35
    Public Shared alturaFilasSpread As Integer = 25

    Public filaAlmacen As Integer = -1

    Public esPrueba As Boolean = False

#Region "Eventos"

    Private Sub Principal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        Me.Cursor = Cursors.WaitCursor
        Dim nombrePrograma As String = "PrincipalBerry"
        AbrirPrograma(nombrePrograma, True)
        System.Threading.Thread.Sleep(3000)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Principal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Me.estaCerrando = True
        Desvanecer()

    End Sub

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Centrar()
        AsignarTooltips()
        ConfigurarConexiones()
        CargarEncabezados()
        CargarMedidas()
        FormatearSpread()
        'CargarTiposDeDatos()

    End Sub

    Private Sub Principal_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        'If (Not ValidarAccesoTotal()) Then
        '    Salir()
        'End If

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click

        Salir()

    End Sub

    Private Sub spAlmacen_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spAlmacen.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter2(spCatalogos)
        End If

    End Sub

    Private Sub spAlmacen_KeyDown(sender As Object, e As KeyEventArgs) Handles spAlmacen.KeyDown

        If e.KeyData = Keys.F5 Then ' Abrir catalogos.
            If (Me.opcionSeleccionada = OpcionMenu.Familia) Then
                If (spAlmacen.ActiveSheet.ActiveColumnIndex = spAlmacen.ActiveSheet.Columns("idUsuario").Index) Or (spAlmacen.ActiveSheet.ActiveColumnIndex = spAlmacen.ActiveSheet.Columns("nombreUsuario").Index) Then
                    spAlmacen.Enabled = False
                    CargarCatalogoUsuarios()
                    FormatearSpreadCatalogoUsuarios(False)
                    spCatalogos.Focus()
                End If
            End If
        ElseIf e.KeyData = Keys.F6 Then ' Eliminar un registro.
            If (Me.opcionSeleccionada = OpcionMenu.Almacen) Then
                If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                    'Dim fila As Integer = spAlmacen.ActiveSheet.ActiveRowIndex
                    'Dim id As Integer = 0
                    'Dim idUsuario As Integer = LogicaCatalogos.Funciones.ValidarNumero(spAlmacen.ActiveSheet.Cells(fila, spAlmacen.ActiveSheet.Columns("id").Index).Text)
                    'usuarios.EId = idUsuario
                    'Dim tieneDatos As Boolean = usuarios.ValidarActividadPorId()
                    'If (tieneDatos) Then
                    '    MessageBox.Show("No se puede eliminar este registro, ya que contiene actividades capturadas.", "No permitido.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    'Else
                    EliminarRegistro(spAlmacen)
                    'End If
                End If
            End If
        ElseIf (e.KeyData = Keys.Enter) Then ' Validar registros.
            If (Me.opcionSeleccionada = OpcionMenu.Almacen) Then
                ControlarSpreadEnter2(spAlmacen)
            ElseIf (Me.opcionSeleccionada = OpcionMenu.Familia) Then
                ControlarSpreadEnter2(spFamilia)
            End If
        End If

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        If (Me.opcionSeleccionada = OpcionMenu.Almacen) Then
            GuardarEditarAlmacenes()
        ElseIf (Me.opcionSeleccionada = OpcionMenu.Familia) Then
            If (Me.filaAlmacen >= 0) Then
                GuardarEditarFamilias()
            End If
        End If

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        If (Me.opcionSeleccionada = OpcionMenu.Almacen) Then
            EliminarAlmacenes(True)
        ElseIf (Me.opcionSeleccionada = OpcionMenu.Familia) Then
            If (Me.filaAlmacen >= 0) Then
                Dim idAlmacen As Integer = LogicaCatalogos.Funciones.ValidarNumero(spAlmacen.ActiveSheet.Cells(Me.filaAlmacen, spAlmacen.ActiveSheet.Columns("id").Index).Value)
                EliminarFamilias(True, idAlmacen)
            End If
        End If

    End Sub

    Private Sub btnGuardar_MouseHover(sender As Object, e As EventArgs) Handles btnGuardar.MouseHover

        AsignarTooltips("Guardar.")

    End Sub

    Private Sub btnEliminar_MouseHover(sender As Object, e As EventArgs) Handles btnEliminar.MouseHover

        AsignarTooltips("Eliminar.")

    End Sub

    Private Sub btnSalir_MouseHover(sender As Object, e As EventArgs) Handles btnSalir.MouseHover

        AsignarTooltips("Salir.")

    End Sub

    Private Sub pnlEncabezado_MouseHover(sender As Object, e As EventArgs) Handles pnlPie.MouseHover, pnlEncabezado.MouseHover, pnlCuerpo.MouseHover

        AsignarTooltips(String.Empty)

    End Sub

    Private Sub spCatalogos2_CellClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spCatalogos.CellClick

        Dim fila As Integer = e.Row
        If (Me.opcionSeleccionada = OpcionMenu.Familia) Then
            spAlmacen.ActiveSheet.Cells(spAlmacen.ActiveSheet.ActiveRowIndex, spAlmacen.ActiveSheet.Columns("idUsuario").Index).Text = spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spAlmacen.ActiveSheet.Cells(spAlmacen.ActiveSheet.ActiveRowIndex, spAlmacen.ActiveSheet.Columns("nombreUsuario").Index).Text = spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (Me.opcionSeleccionada = OpcionMenu.Almacen) Then
            spAlmacen.ActiveSheet.Cells(spAlmacen.ActiveSheet.ActiveRowIndex, spAlmacen.ActiveSheet.Columns("idArea").Index).Text = spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spAlmacen.ActiveSheet.Cells(spAlmacen.ActiveSheet.ActiveRowIndex, spAlmacen.ActiveSheet.Columns("nombreArea").Index).Text = spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        End If

    End Sub

    Private Sub spCatalogos2_CellDoubleClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spCatalogos.CellDoubleClick

        VolverFocoCatalogos()

    End Sub

    Private Sub spCatalogos2_KeyDown(sender As Object, e As KeyEventArgs) Handles spCatalogos.KeyDown

        If e.KeyCode = Keys.Escape Then
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

    Private Sub btnAyuda_MouseHover(sender As Object, e As EventArgs) Handles btnAyuda.MouseHover

        AsignarTooltips("Ayuda.")

    End Sub

#End Region

#Region "Metodos"

#Region "Genericos"

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

        If ((Not LogicaCatalogos.Usuarios.accesoTotal) Or (LogicaCatalogos.Usuarios.accesoTotal = 0) Or (LogicaCatalogos.Usuarios.accesoTotal = False)) Then
            MsgBox("No tienes permisos suficientes para acceder a este programa.", MsgBoxStyle.Information, "Permisos insuficientes.")
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub Centrar()

        Me.CenterToScreen()
        'Me.Opacity = 0.97 'Está bien perro esto.  
        Me.Location = Screen.PrimaryScreen.WorkingArea.Location
        Me.Size = Screen.PrimaryScreen.WorkingArea.Size

    End Sub

    Private Sub AsignarTooltips()

        Dim tp As New ToolTip()
        tp.AutoPopDelay = 5000
        tp.InitialDelay = 0
        tp.ReshowDelay = 100
        tp.ShowAlways = True
        tp.SetToolTip(Me.btnSalir, "Salir.")

    End Sub

    Private Sub AsignarTooltips(ByVal texto As String)

        lblDescripcionTooltip.Text = texto

    End Sub

    Private Sub ConfigurarConexiones()

        If (Me.esPrueba) Then
            LogicaCatalogos.Directorios.id = 1
            LogicaCatalogos.Directorios.instanciaSql = "BERRY1-DELL\SQLEXPRESS2008"
            LogicaCatalogos.Directorios.usuarioSql = "AdminBerry"
            LogicaCatalogos.Directorios.contrasenaSql = "@berry2017"
            LogicaCatalogos.Usuarios.id = 1
        Else
            LogicaCatalogos.Directorios.ObtenerParametros()
            LogicaCatalogos.Usuarios.ObtenerParametros()
        End If
        EntidadesCatalogos.BaseDatos.ECadenaConexionCatalogo = "Catalogo" & LogicaCatalogos.Directorios.id
        EntidadesCatalogos.BaseDatos.ECadenaConexionConfiguracion = "Configuracion" & LogicaCatalogos.Directorios.id
        EntidadesCatalogos.BaseDatos.ECadenaConexionAlmacen = "Almacen" & LogicaCatalogos.Directorios.id
        EntidadesCatalogos.BaseDatos.AbrirConexionCatalogo()
        EntidadesCatalogos.BaseDatos.AbrirConexionConfiguracion()
        EntidadesCatalogos.BaseDatos.AbrirConexionAlmacen()
        ConsultarInformacionUsuario()
        CargarPrefijoAlmacen()

    End Sub

    Private Sub CargarPrefijoAlmacen()

        LogicaCatalogos.Programas.prefijo = Me.prefijoDeAlmacen

    End Sub

    Private Sub ConsultarInformacionUsuario()

        Dim lista As New List(Of EntidadesCatalogos.Usuarios)
        usuarios.EId = LogicaCatalogos.Usuarios.id
        lista = usuarios.ObtenerListado()
        If (lista.Count > 0) Then
            LogicaCatalogos.Usuarios.id = lista(0).EId
            LogicaCatalogos.Usuarios.nombre = lista(0).ENombre
            LogicaCatalogos.Usuarios.contrasena = lista(0).EContrasena
            LogicaCatalogos.Usuarios.nivel = lista(0).ENivel
            LogicaCatalogos.Usuarios.accesoTotal = lista(0).EAccesoTotal
        End If

    End Sub

    Private Sub CargarEncabezados()

        lblEncabezadoPrograma.Text = "Programa: " + Me.Text
        lblEncabezadoEmpresa.Text = "Directorio: " + LogicaCatalogos.Directorios.nombre
        lblEncabezadoUsuario.Text = "Usuario: " + LogicaCatalogos.Usuarios.nombre

    End Sub

    Private Sub AbrirPrograma(nombre As String, salir As Boolean)

        If (Me.esPrueba) Then
            Exit Sub
        End If
        ejecutarProgramaPrincipal.UseShellExecute = True
        ejecutarProgramaPrincipal.FileName = nombre & Convert.ToString(".exe")
        ejecutarProgramaPrincipal.WorkingDirectory = Directory.GetCurrentDirectory()
        ejecutarProgramaPrincipal.Arguments = LogicaCatalogos.Directorios.id.ToString().Trim().Replace(" ", "|") & " " & LogicaCatalogos.Directorios.nombre.ToString().Trim().Replace(" ", "|") & " " & LogicaCatalogos.Directorios.descripcion.ToString().Trim().Replace(" ", "|") & " " & LogicaCatalogos.Directorios.rutaLogo.ToString().Trim().Replace(" ", "|") & " " & LogicaCatalogos.Directorios.esPredeterminado.ToString().Trim().Replace(" ", "|") & " " & LogicaCatalogos.Directorios.instanciaSql.ToString().Trim().Replace(" ", "|") & " " & LogicaCatalogos.Directorios.usuarioSql.ToString().Trim().Replace(" ", "|") & " " & LogicaCatalogos.Directorios.contrasenaSql.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de directorios, indice 9 ;)".Replace(" ", "|") & " " & LogicaCatalogos.Usuarios.id.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de usuario, indice 11 ;)".Replace(" ", "|")
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

#End Region

#Region "Todos"

    Private Sub EliminarRegistro(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.Rows.Remove(spread.ActiveSheet.ActiveRowIndex, 1)

    End Sub

    Private Sub CargarMedidas()

        Me.izquierda = 0
        Me.arriba = pnlMenu.Height
        Me.anchoTotal = pnlCuerpo.Width
        Me.altoTotal = pnlCuerpo.Height - pnlMenu.Height
        Me.anchoMitad = Me.anchoTotal / 2
        Me.altoMitad = Me.altoTotal / 2
        Me.anchoTercio = Me.anchoTotal / 3

    End Sub

    Private Sub LimpiarSpread()

        spAlmacen.ActiveSheet.ClearRange(0, 0, spAlmacen.ActiveSheet.Rows.Count, spAlmacen.ActiveSheet.Columns.Count, False)

    End Sub

    Private Sub OcultarSpreads()

        spAlmacen.Hide()
        spFamilia.Hide()
        spCatalogos.Hide()

    End Sub

    Private Sub FormatearSpread()

        ' Se cargan tipos de datos de spread.
        CargarTiposDeDatos()
        ' Se cargan las opciones generales de cada spread.
        spAlmacen.Reset() : Application.DoEvents()
        spCatalogos.Reset() : Application.DoEvents()
        OcultarSpreads()
        spAlmacen.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell : Application.DoEvents()
        spCatalogos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Midnight : Application.DoEvents()
        spFamilia.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell : Application.DoEvents()
        spAlmacen.ActiveSheet.GrayAreaBackColor = Color.White : Application.DoEvents()
        spFamilia.ActiveSheet.GrayAreaBackColor = Color.White : Application.DoEvents()
        spAlmacen.Font = New Font("Microsoft Sans Serif", Principal.tamañoLetraSpread, FontStyle.Regular) : Application.DoEvents()
        spCatalogos.Font = New Font("Microsoft Sans Serif", Principal.tamañoLetraSpread, FontStyle.Regular) : Application.DoEvents()
        spFamilia.Font = New Font("Microsoft Sans Serif", Principal.tamañoLetraSpread, FontStyle.Regular) : Application.DoEvents()
        spAlmacen.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaEncabezadosGrandeSpread : Application.DoEvents()
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaEncabezadosGrandeSpread : Application.DoEvents()
        spFamilia.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaEncabezadosGrandeSpread : Application.DoEvents()
        spAlmacen.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread : Application.DoEvents()
        spCatalogos.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread : Application.DoEvents()
        spFamilia.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread : Application.DoEvents()
        spAlmacen.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded : Application.DoEvents()
        spAlmacen.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded : Application.DoEvents()

    End Sub

    Public Sub ControlarSpreadEnter(ByVal spread As FarPoint.Win.Spread.FpSpread)

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

    End Sub

    Private Sub SeleccionoFamilia()

        Me.Cursor = Cursors.WaitCursor
        Me.opcionSeleccionada = OpcionMenu.Familia
        Me.filaAlmacen = -1
        CargarFamilias(-1)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub ControlarSpreadEnter2(ByVal spread As FarPoint.Win.Spread.FpSpread)

        Dim columnaActiva As Integer = spread.ActiveSheet.ActiveColumnIndex
        If (columnaActiva = spread.ActiveSheet.Columns.Count - 1) Then
            spread.ActiveSheet.Rows.Count += 1
        End If
        Dim fila As Integer = spAlmacen.ActiveSheet.ActiveRowIndex
        If (Me.opcionSeleccionada = OpcionMenu.Familia) Then
            'If (columnaActiva = spAlmacen.ActiveSheet.Columns("idUsuario").Index) Then
            '    Dim idUsuario As Integer = spAlmacen.ActiveSheet.Cells(fila, spAlmacen.ActiveSheet.Columns("idUsuario").Index).Value
            '    usuarios.EId = idUsuario
            '    If (idUsuario > 0) Then
            '        Dim lista As New List(Of EntidadesCatalogos.Usuarios)
            '        lista = usuarios.ObtenerListado()
            '        If (lista.Count > 0) Then
            '            spAlmacen.ActiveSheet.Cells(fila, spAlmacen.ActiveSheet.Columns("nombreUsuario").Index).Value = lista(0).ENombre
            '        End If
            '    End If
            'End If
        End If

    End Sub

    Private Sub SeleccionoAlmacen()

        Me.Cursor = Cursors.WaitCursor
        Me.opcionSeleccionada = OpcionMenu.Almacen
        CargarAlmacenes()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub FormatearSpreadCatalogoUsuarios(ByVal izquierda As Boolean)

        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold) : Application.DoEvents()
        spCatalogos.Width = 300 : Application.DoEvents()
        If (izquierda) Then
            spCatalogos.Location = New Point(spAlmacen.Location.X, spAlmacen.Location.Y) : Application.DoEvents()
        Else
            spCatalogos.Location = New Point(spAlmacen.Width - spCatalogos.Width, spAlmacen.Location.Y) : Application.DoEvents()
        End If
        spCatalogos.Visible = True : Application.DoEvents()
        spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never : Application.DoEvents()
        spCatalogos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded : Application.DoEvents()
        spCatalogos.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect : Application.DoEvents()
        spCatalogos.Height = spAlmacen.Height : Application.DoEvents()
        Dim numeracion As Integer = 0
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spCatalogos.ActiveSheet.Columns("id").Width = 50 : Application.DoEvents()
        spCatalogos.ActiveSheet.Columns("nombre").Width = 210 : Application.DoEvents()
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper : Application.DoEvents()
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("nombre").Index).Value = "Nombre".ToUpper : Application.DoEvents()
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = 35 : Application.DoEvents()

    End Sub

    Private Sub CargarCatalogoUsuarios()

        'usuarios.EIdEmpresa = datosEmpresa.EId
        spCatalogos.ActiveSheet.DataSource = usuarios.ObtenerListadoReporte() : Application.DoEvents()
        spCatalogos.Focus()

    End Sub

    Private Sub VolverFocoCatalogos()

        spAlmacen.Enabled = True
        spAlmacen.Focus()
        If (Me.opcionSeleccionada = OpcionMenu.Familia) Then
            spAlmacen.ActiveSheet.SetActiveCell(spAlmacen.ActiveSheet.ActiveRowIndex, spAlmacen.ActiveSheet.Columns("idUsuario").Index)
        ElseIf (Me.opcionSeleccionada = OpcionMenu.Almacen) Then
            spAlmacen.ActiveSheet.SetActiveCell(spAlmacen.ActiveSheet.ActiveRowIndex, spAlmacen.ActiveSheet.Columns("idArea").Index)
        End If
        spCatalogos.Visible = False

    End Sub

    Private Sub RestaurarAlturaSpread()

        spFamilia.Visible = False : Application.DoEvents()
        spAlmacen.Height = pnlCuerpo.Height - pnlMenu.Height : Application.DoEvents()

    End Sub

    Private Sub CargarFamilias(ByVal idAlmacen As Integer)

        If (idAlmacen < 0) Then
            CargarAlmacenes()
        End If
        spFamilia.Height = Me.altoTotal
        spFamilia.Width = Me.anchoMitad
        spFamilia.Top = Me.arriba
        spFamilia.Left = spAlmacen.Width
        familias.EIdAlmacen = idAlmacen
        familias.EId = 0
        spFamilia.ActiveSheet.DataSource = familias.ObtenerListadoReporte()
        FormatearSpreadFamilias()

    End Sub

    Private Sub FormatearSpreadFamilias()

        spFamilia.ActiveSheet.ColumnHeader.Rows(0).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold) : Application.DoEvents()
        If (Me.opcionSeleccionada = OpcionMenu.Familia) Then
            spFamilia.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal : Application.DoEvents()
        Else
            spFamilia.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect : Application.DoEvents()
        End If
        If (Me.opcionSeleccionada = OpcionMenu.Familia) Then
            ControlarSpreadEnter(spFamilia)
        End If
        Dim numeracion As Integer = 0
        spFamilia.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spFamilia.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spFamilia.ActiveSheet.Columns("id").Width = 50 : Application.DoEvents()
        spFamilia.ActiveSheet.Columns("nombre").Width = 400 : Application.DoEvents()
        spFamilia.ActiveSheet.Columns("id").CellType = tipoEntero : Application.DoEvents()
        spFamilia.ActiveSheet.Columns("nombre").CellType = tipoTexto : Application.DoEvents()  
        spFamilia.ActiveSheet.ColumnHeader.Cells(0, spFamilia.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper() : Application.DoEvents()
        spFamilia.ActiveSheet.ColumnHeader.Cells(0, spFamilia.ActiveSheet.Columns("nombre").Index).Value = "Nombre".ToUpper() : Application.DoEvents()  
        If (Me.opcionSeleccionada = OpcionMenu.Familia) Then
            spFamilia.ActiveSheet.Rows.Count += 1 : Application.DoEvents()
        End If

    End Sub

    Private Sub CargarAlmacenes()

        OcultarSpreads()
        'LimpiarSpread()
        'spAlmacen.Reset()
        spAlmacen.Show()
        If (Me.opcionSeleccionada = OpcionMenu.Almacen) Then
            spAlmacen.Height = Me.altoTotal
            spAlmacen.Width = Me.anchoTotal
            spAlmacen.Top = Me.arriba
            spAlmacen.Left = Me.izquierda
        ElseIf (Me.opcionSeleccionada = OpcionMenu.Familia) Then 
            spAlmacen.Height = Me.altoTotal
            spAlmacen.Width = Me.anchoMitad
            spAlmacen.Top = Me.arriba
            spAlmacen.Left = Me.izquierda
        End If
        spAlmacen.ActiveSheet.DataSource = almacenes.ObtenerListadoReporte()
        FormatearSpreadAlmacen()

    End Sub

    Private Sub FormatearSpreadAlmacen()

        spAlmacen.ActiveSheet.ColumnHeader.Rows(0).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold) : Application.DoEvents() : Application.DoEvents()
        spAlmacen.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaEncabezadosGrandeSpread : Application.DoEvents()
        If (Me.opcionSeleccionada = OpcionMenu.Almacen) Then
            spAlmacen.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal : Application.DoEvents()
        Else
            spAlmacen.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect : Application.DoEvents()
        End If
        If (Me.opcionSeleccionada = OpcionMenu.Almacen) Then
            ControlarSpreadEnter(spAlmacen)
        End If
        Dim numeracion As Integer = 0
        spAlmacen.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spAlmacen.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spAlmacen.ActiveSheet.Columns(numeracion).Tag = "abreviatura" : numeracion += 1
        spAlmacen.ActiveSheet.Columns("id").Width = 50 : Application.DoEvents()
        spAlmacen.ActiveSheet.Columns("nombre").Width = 400 : Application.DoEvents()
        spAlmacen.ActiveSheet.Columns("abreviatura").Width = 160 : Application.DoEvents()
        spAlmacen.ActiveSheet.Columns("id").CellType = tipoEntero : Application.DoEvents()
        spAlmacen.ActiveSheet.Columns("nombre").CellType = tipoTexto : Application.DoEvents()
        spAlmacen.ActiveSheet.Columns("abreviatura").CellType = tipoTexto : Application.DoEvents()
        spAlmacen.ActiveSheet.ColumnHeader.Cells(0, spAlmacen.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper() : Application.DoEvents()
        spAlmacen.ActiveSheet.ColumnHeader.Cells(0, spAlmacen.ActiveSheet.Columns("nombre").Index).Value = "Nombre".ToUpper() : Application.DoEvents()
        spAlmacen.ActiveSheet.ColumnHeader.Cells(0, spAlmacen.ActiveSheet.Columns("abreviatura").Index).Value = "Abreviatura".ToUpper() : Application.DoEvents()
        If (Me.opcionSeleccionada = OpcionMenu.Almacen) Then
            spAlmacen.ActiveSheet.Rows.Count += 1 : Application.DoEvents()
        End If
        spAlmacen.Focus() : Application.DoEvents()

    End Sub

    Private Sub GuardarEditarAlmacenes()

        EliminarAlmacenes(False)
        For fila As Integer = 0 To spAlmacen.ActiveSheet.Rows.Count - 1
            Dim id As Integer = LogicaCatalogos.Funciones.ValidarNumero(spAlmacen.ActiveSheet.Cells(fila, spAlmacen.ActiveSheet.Columns("id").Index).Text)
            Dim nombre As String = spAlmacen.ActiveSheet.Cells(fila, spAlmacen.ActiveSheet.Columns("nombre").Index).Text
            Dim abreviatura As String = LogicaCatalogos.Funciones.ValidarLetra(spAlmacen.ActiveSheet.Cells(fila, spAlmacen.ActiveSheet.Columns("abreviatura").Index).Value)
            If (id > 0 AndAlso Not String.IsNullOrEmpty(nombre)) Then
                almacenes.EId = id
                almacenes.ENombre = nombre
                almacenes.EAbreviatura = abreviatura
                almacenes.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        CargarAlmacenes()

    End Sub

    Private Sub EliminarAlmacenes(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            almacenes.EId = 0
            almacenes.Eliminar()
        End If
        If (conMensaje) Then
            CargarAlmacenes()
        End If

    End Sub

    Private Sub GuardarEditarFamilias()

        Dim idAlmacen As Integer = LogicaCatalogos.Funciones.ValidarNumero(spAlmacen.ActiveSheet.Cells(spAlmacen.ActiveSheet.ActiveRowIndex, spAlmacen.ActiveSheet.Columns("id").Index).Value)
        EliminarFamilias(False, idAlmacen)
        For fila As Integer = 0 To spFamilia.ActiveSheet.Rows.Count - 1
            Dim id As Integer = LogicaCatalogos.Funciones.ValidarNumero(spFamilia.ActiveSheet.Cells(fila, spFamilia.ActiveSheet.Columns("id").Index).Text)
            Dim nombre As String = spFamilia.ActiveSheet.Cells(fila, spFamilia.ActiveSheet.Columns("nombre").Index).Text
            If (idAlmacen > 0 AndAlso id > 0 AndAlso Not String.IsNullOrEmpty(nombre)) Then
                familias.EIdAlmacen = idAlmacen
                familias.EId = id
                familias.ENombre = nombre
                familias.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        CargarFamilias(-1)

    End Sub

    Private Sub EliminarFamilias(ByVal conMensaje As Boolean, ByVal idAlmacen As Integer)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            familias.EIdAlmacen = idAlmacen
            familias.EId = 0
            familias.Eliminar()
        End If
        If (conMensaje) Then
            CargarFamilias(-1)
        End If

    End Sub

#End Region

#End Region

#Region "Enumeraciones"

    Public Enum OpcionMenu
         
        Almacen = 1
        Familia = 2

    End Enum

#End Region

    Private Sub rbtnAlmacenes_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnAlmacenes.CheckedChanged

        If (rbtnAlmacenes.Checked) Then
            SeleccionoAlmacen()
        End If
        
    End Sub

    Private Sub rbtnFamilias_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnFamilias.CheckedChanged

        If (rbtnFamilias.Checked) Then
            SeleccionoFamilia()
        End If 

    End Sub

    Private Sub rbtnSubFamilias_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnSubFamilias.CheckedChanged

    End Sub

    Private Sub rbtnArticulos_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnArticulos.CheckedChanged

    End Sub

    Private Sub rbtnUnidadesMedidas_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnUnidadesMedidas.CheckedChanged

    End Sub
     
    Private Sub spAlmacen_CellClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spAlmacen.CellClick

        If (Me.opcionSeleccionada = OpcionMenu.Familia) Then
            If (e.Row >= 0) Then
                Me.filaAlmacen = e.Row
                spFamilia.Show()
                Dim idAlmacen As Integer = LogicaCatalogos.Funciones.ValidarNumero(spAlmacen.ActiveSheet.Cells(Me.filaAlmacen, spAlmacen.ActiveSheet.Columns("id").Index).Value)
                CargarFamilias(idAlmacen)
            End If
        End If

    End Sub

    Private Sub spFamilia_KeyDown(sender As Object, e As KeyEventArgs) Handles spFamilia.KeyDown

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter2(spFamilia) 
        ElseIf e.KeyData = Keys.F6 Then ' Eliminar un registro.
            If (Me.opcionSeleccionada = OpcionMenu.Familia) Then
                If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                    EliminarRegistro(spFamilia)
                End If
            End If
        End If

    End Sub

    Private Sub spFamilia_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spFamilia.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter2(spFamilia)
        End If

    End Sub

End Class