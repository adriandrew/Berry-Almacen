Imports System.IO
Imports System.ComponentModel

Public Class Principal

    ' Variables de objetos de entidades.
    Public usuarios As New ALMEntidadesEntradas.Usuarios()
    Public entradas As New ALMEntidadesEntradas.Entradas()
    Public almacenes As New ALMEntidadesEntradas.Almacenes()
    Public familias As New ALMEntidadesEntradas.Familias()
    Public subFamilias As New ALMEntidadesEntradas.SubFamilias()
    Public articulos As New ALMEntidadesEntradas.Articulos()
    Public unidadesMedidas As New ALMEntidadesEntradas.UnidadesMedidas()
    Public proveedores As New ALMEntidadesEntradas.Proveedores()
    Public monedas As New ALMEntidadesEntradas.Monedas()
    Public tiposCambios As New ALMEntidadesEntradas.TiposCambios()
    Public tiposEntradas As New ALMEntidadesEntradas.TiposEntradas()
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
    Public opcionCatalogoSeleccionada As Integer = 0
    Public esGuardadoValido As Boolean = True
    ' Variable de desarrollo.
    Public esDesarrollo As Boolean = False

#Region "Eventos"

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor
        Centrar()
        CargarNombrePrograma()
        AsignarTooltips()
        ConfigurarConexiones()
        CargarMedidas()
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
        CargarAlmacenes()
        CargarTiposEntradas()
        CargarMonedas()
        Me.Enabled = True
        AsignarFoco(cbAlmacenes)
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
            AsignarFoco(txtIdProveedor)
        End If

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        ValidarGuardado()
        If (Me.esGuardadoValido) Then
            GuardarEditarEntradas()
        End If

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

        Dim fila As Integer = e.Row
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.proveedor) Then
            CargarDatosEnOtrosDeCatalogos(fila)
        Else
            CargarDatosEnSpreadDeCatalogos(fila)
        End If

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
        End If

    End Sub

    Private Sub cbTiposEntradas_KeyDown(sender As Object, e As KeyEventArgs) Handles cbTiposEntradas.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbTiposEntradas.SelectedValue > 0) Then
                AsignarFoco(txtIdProveedor)
            Else
                cbTiposEntradas.SelectedIndex = 0
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(txtTipoCambio)
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

    Private Sub txtIdProveedor_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdProveedor.KeyDown, txtNombreProveedor.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            Dim idProveedor As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtIdProveedor.Text)
            proveedores.EId = idProveedor
            Dim lista As List(Of ALMEntidadesEntradas.Proveedores)
            lista = proveedores.ObtenerListado()
            Dim nombre As String = String.Empty
            If (lista.Count > 0) Then
                nombre = lista(0).ENombre()
            End If
            txtNombreProveedor.Text = nombre
            If (idProveedor <= 0 Or String.IsNullOrEmpty(nombre)) Then
                txtIdProveedor.Clear()
                txtNombreProveedor.Clear()
            Else
                AsignarFoco(spEntradas)
            End If
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.proveedor
            CargarCatalogoEnOtros()
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(cbTiposEntradas)
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
            txtAyuda.Text = "Sección de Ayuda: " & vbNewLine & vbNewLine & "* Teclas básicas: " & vbNewLine & "F5 sirve para mostrar catálogos. " & vbNewLine & "F6 sirve para eliminar un registro únicamente. " & vbNewLine & "Escape sirve para ocultar catálogos que se encuentren desplegados. " & vbNewLine & vbNewLine & "* Catálogos desplegados: " & vbNewLine & "Cuando se muestra algún catálogo, al seleccionar alguna opción de este, se va mostrando en tiempo real en la captura de donde se originó. Cuando se le da doble clic en alguna opción o a la tecla escape se oculta dicho catálogo. " & vbNewLine & vbNewLine & "* Datos obligatorios: " & vbNewLine & "Todos los que tengan el simbolo * son estrictamente obligatorios." & vbNewLine & vbNewLine & "* Captura:" & vbNewLine & "* Parte superior: " & vbNewLine & "En esta parte se capturarán todos los datos que son generales, tal cual como el número de la entrada, el almacén al que corresponde, etc." & vbNewLine & "* Parte inferior: " & vbNewLine & "En esta parte se capturarán todos los datos que pueden combinarse, por ejemplo los distintos artículos de ese número de entrada." & vbNewLine & vbNewLine & "* Existen los botones de guardar/editar y eliminar todo dependiendo lo que se necesite hacer. " : Application.DoEvents()
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
            ALMLogicaEntradas.Directorios.id = 1
            ALMLogicaEntradas.Directorios.instanciaSql = "BERRY1-DELL\SQLEXPRESS2008"
            ALMLogicaEntradas.Directorios.usuarioSql = "AdminBerry"
            ALMLogicaEntradas.Directorios.contrasenaSql = "@berry2017"
            ALMLogicaEntradas.Usuarios.id = 1
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

    Private Sub CargarTitulosDirectorio()

        Me.Text = "Programa:  " + Me.nombreEstePrograma + "              Directorio:  " + ALMLogicaEntradas.Directorios.nombre + "              Usuario:  " + ALMLogicaEntradas.Usuarios.nombre

    End Sub

    Private Sub CargarEncabezados()

        lblEncabezadoPrograma.Text = "Programa: " + Me.Text
        lblEncabezadoEmpresa.Text = "Directorio: " + ALMLogicaEntradas.Directorios.nombre
        lblEncabezadoUsuario.Text = "Usuario: " + ALMLogicaEntradas.Usuarios.nombre

    End Sub

    Private Sub AbrirPrograma(nombre As String, salir As Boolean)

        If (Me.esDesarrollo) Then
            Exit Sub
        End If
        ejecutarProgramaPrincipal.UseShellExecute = True
        ejecutarProgramaPrincipal.FileName = nombre & Convert.ToString(".exe")
        ejecutarProgramaPrincipal.WorkingDirectory = Directory.GetCurrentDirectory()
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
            c.BackColor = Color.White
        Next
        For fila = 0 To spEntradas.ActiveSheet.Rows.Count - 1
            For columna = 0 To spEntradas.ActiveSheet.Columns.Count - 1
                spEntradas.ActiveSheet.Cells(fila, columna).BackColor = Color.White
            Next
        Next
        If (Not chkConservarDatos.Checked) Then
            cbMonedas.SelectedIndex = 0
            cbTiposEntradas.SelectedIndex = 0
            dtpFecha.Value = Today
            txtIdProveedor.Clear() : txtNombreProveedor.Clear()
            CargarTiposCambios()
        End If
        txtIdExterno.Clear()
        spEntradas.ActiveSheet.DataSource = Nothing
        spEntradas.ActiveSheet.Rows.Count = 1
        spEntradas.ActiveSheet.SetActiveCell(0, 0)
        LimpiarSpread(spEntradas)

    End Sub

    Private Sub LimpiarSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.ClearRange(0, 0, spread.ActiveSheet.Rows.Count, spread.ActiveSheet.Columns.Count, True)

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
            Dim idMoneda As Integer = IIf(IsNumeric(cbMonedas.SelectedValue), cbMonedas.SelectedValue, 1)
            tiposCambios.EIdMoneda = idMoneda
            Dim valor As Double = 1
            If (idMoneda > 0) Then
                Dim lista As New List(Of ALMEntidadesEntradas.TiposCambios)
                lista = tiposCambios.ObtenerListado()
                If (lista.Count = 1) Then
                    valor = lista(0).EValor
                End If
            End If
            txtTipoCambio.Text = valor
        End If

    End Sub

    Private Sub CargarTiposEntradas()

        cbTiposEntradas.DataSource = tiposEntradas.ObtenerListadoReporte()
        cbTiposEntradas.DisplayMember = "Nombre"
        cbTiposEntradas.ValueMember = "Id"

    End Sub

    Private Sub CargarAlmacenes()

        cbAlmacenes.DataSource = almacenes.ObtenerListadoReporte()
        cbAlmacenes.DisplayMember = "Nombre"
        cbAlmacenes.ValueMember = "Id"

    End Sub

    Private Sub FormatearSpread()

        ' Se cargan tipos de datos de spread.
        CargarTiposDeDatos()
        ' Se cargan las opciones generales. 
        pnlCatalogos.Visible = False
        spEntradas.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spCatalogos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Midnight
        spEntradas.ActiveSheet.GrayAreaBackColor = Principal.colorAreaGris
        spEntradas.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spCatalogos.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spEntradas.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spEntradas.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spCatalogos.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spEntradas.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spEntradas.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        'spEntradas.EditModePermanent = True
        spEntradas.EditModeReplace = True
        Application.DoEvents()

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
        If (spread.Name = spEntradas.Name) Then
            Dim fila As Integer = 0
            If (columnaActiva = spEntradas.ActiveSheet.Columns("idFamilia").Index) Then
                fila = spEntradas.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue))
                Dim idFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index).Value)
                familias.EIdAlmacen = idAlmacen
                familias.EId = idFamilia
                If (idAlmacen > 0 And idFamilia > 0) Then
                    Dim lista As New List(Of ALMEntidadesEntradas.Familias)
                    lista = familias.ObtenerListado()
                    If (lista.Count = 1) Then
                        spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("nombreFamilia").Index).Value = lista(0).ENombre
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
                    Dim lista As New List(Of ALMEntidadesEntradas.SubFamilias)
                    lista = subFamilias.ObtenerListado()
                    If (lista.Count = 1) Then
                        spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = lista(0).ENombre
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
                    Dim lista As New List(Of ALMEntidadesEntradas.Articulos)
                    lista = articulos.ObtenerListado()
                    If (lista.Count = 1) Then
                        spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("nombreArticulo").Index).Value = lista(0).ENombre
                        Dim lista2 As New List(Of ALMEntidadesEntradas.UnidadesMedidas)
                        unidadesMedidas.EId = lista(0).EIdUnidadMedida
                        lista2 = unidadesMedidas.ObtenerListado()
                        If (lista2.Count = 1) Then
                            spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = lista2(0).ENombre
                        End If
                        spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex + 2)
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
                    Dim valorPrecio As String = ALMLogicaEntradas.Funciones.ValidarLetra(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precioUnitario").Index).Text)
                    If (String.IsNullOrEmpty(valorPrecio)) Then
                        Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
                        Dim idFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index).Value)
                        Dim idSubFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Value)
                        Dim idArticulo As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index).Value)
                        Dim lista As New List(Of ALMEntidadesEntradas.Articulos)
                        articulos.EIdAlmacen = idAlmacen
                        articulos.EIdFamilia = idFamilia
                        articulos.EIdSubFamilia = idSubFamilia
                        articulos.EId = idArticulo
                        lista = articulos.ObtenerListado()
                        If (lista.Count = 1) Then
                            Dim precio As Double = lista(0).EPrecio
                            spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precioUnitario").Index).Value = precio
                            spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Value = cantidad * precio
                            Dim tipoCambio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtTipoCambio.Text)
                            spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).Value = cantidad * precio * tipoCambio
                        End If
                    End If
                Else
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).Value = String.Empty
                    spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spEntradas.ActiveSheet.Columns("precioUnitario").Index) Then
                fila = spEntradas.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).Value)
                Dim precio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precioUnitario").Index).Value)
                Dim tipoCambio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtTipoCambio.Text)
                If (cantidad > 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Value = cantidad * precio
                    If (tipoCambio > 0) Then
                        spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).Value = cantidad * precio * tipoCambio
                    End If
                ElseIf (precio = 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precioUnitario").Index).Value = 0
                Else
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precioUnitario").Index).Value = String.Empty
                    spEntradas.ActiveSheet.SetActiveCell(fila, spEntradas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spEntradas.ActiveSheet.Columns("total").Index) Then
                fila = spEntradas.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).Value)
                Dim total As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Value)
                Dim tipoCambio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtTipoCambio.Text)
                If (cantidad > 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precioUnitario").Index).Value = total / cantidad
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

    Private Sub CargarIdConsecutivo()

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

        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.proveedor) Then
            txtIdProveedor.Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            txtNombreProveedor.Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        End If

    End Sub

    Private Sub CargarCatalogoEnSpread()

        spEntradas.Enabled = False
        Dim columna As Integer = spEntradas.ActiveSheet.ActiveColumnIndex
        If (columna = spEntradas.ActiveSheet.Columns("idFamilia").Index) Or (columna = spEntradas.ActiveSheet.Columns("nombreFamilia").Index) Then
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
            FormatearSpreadCatalogo(OpcionPosicion.centro)
        ElseIf (columna = spEntradas.ActiveSheet.Columns("idSubFamilia").Index) Or (columna = spEntradas.ActiveSheet.Columns("nombreSubFamilia").Index) Then
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
            FormatearSpreadCatalogo(OpcionPosicion.centro)
        ElseIf (columna = spEntradas.ActiveSheet.Columns("idArticulo").Index) Or (columna = spEntradas.ActiveSheet.Columns("nombreArticulo").Index) Then
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
            FormatearSpreadCatalogo(OpcionPosicion.centro)
        End If

    End Sub

    Private Sub CargarCatalogoEnOtros()

        pnlCapturaSuperior.Enabled = False
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.proveedor) Then
            proveedores.EId = 0
            Dim datos As New DataTable
            datos = proveedores.ObtenerListadoReporte()
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                pnlCapturaSuperior.Enabled = True
            End If
            FormatearSpreadCatalogo(OpcionPosicion.centro)
        End If

    End Sub

    Private Sub FormatearSpreadCatalogo(ByVal posicion As Integer)

        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.Width = 450
            spCatalogos.ActiveSheet.Columns.Count = 3
        Else
            spCatalogos.Width = 320
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
        spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        spCatalogos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCatalogos.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        Dim numeracion As Integer = 0
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "unidadMedida" : numeracion += 1
        End If
        spCatalogos.ActiveSheet.Columns("id").Width = 50
        spCatalogos.ActiveSheet.Columns("nombre").Width = 235
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.Columns("unidadMedida").Width = 130
        End If
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("nombre").Index).Value = "Nombre".ToUpper
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("unidadMedida").Index).Value = "Unidad".ToUpper
        End If
        pnlCatalogos.Height = spEntradas.Height
        pnlCatalogos.Size = spCatalogos.Size
        pnlCatalogos.BringToFront()
        pnlCatalogos.Visible = True
        AsignarFoco(pnlCatalogos)
        AsignarFoco(spCatalogos)
        Application.DoEvents()

    End Sub

    Private Sub VolverFocoCatalogos()

        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.proveedor) Then
            pnlCapturaSuperior.Enabled = True
            AsignarFoco(txtIdProveedor)
        Else
            spEntradas.Enabled = True
            AsignarFoco(spEntradas)
        End If
        pnlCatalogos.Visible = False

    End Sub

    Private Sub CargarEntradas()

        Me.Cursor = Cursors.WaitCursor
        entradas.EIdAlmacen = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        entradas.EId = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtId.Text)
        If (entradas.EId > 0) Then
            Dim lista As New List(Of ALMEntidadesEntradas.Entradas)
            lista = entradas.ObtenerListado
            If (lista.Count > 0) Then
                cbMonedas.SelectedValue = lista(0).EIdMoneda
                txtTipoCambio.Text = lista(0).ETipoCambio
                txtIdExterno.Text = lista(0).EIdExterno
                dtpFecha.Value = lista(0).EFecha
                cbTiposEntradas.SelectedValue = lista(0).EIdTipoEntrada
                Dim idProveedor As Integer = lista(0).EIdProveedor
                txtIdProveedor.Text = idProveedor
                Dim lista2 As New List(Of ALMEntidadesEntradas.Proveedores)
                proveedores.EId = idProveedor
                lista2 = proveedores.ObtenerListado()
                If (lista2.Count = 1) Then
                    txtNombreProveedor.Text = lista2(0).ENombre
                End If
                spEntradas.ActiveSheet.DataSource = entradas.ObtenerListadoReporte()
                cantidadFilas = spEntradas.ActiveSheet.Rows.Count + 1
                FormatearSpreadEntradas()
            Else
                LimpiarPantalla()
            End If
        End If
        AsignarFoco(txtIdExterno)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub FormatearSpreadEntradas()

        spEntradas.ActiveSheet.ColumnHeader.RowCount = 2
        spEntradas.ActiveSheet.ColumnHeader.Rows(0, spEntradas.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spEntradas.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spEntradas.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spEntradas.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        spEntradas.ActiveSheet.Rows.Count = cantidadFilas
        ControlarSpreadEnterASiguienteColumna(spEntradas)
        Dim numeracion As Integer = 0
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "idFamilia" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "nombreFamilia" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "idSubFamilia" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "nombreSubFamilia" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "idArticulo" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "nombreArticulo" : numeracion += 1
        spEntradas.ActiveSheet.Columns(numeracion).Tag = "nombreUnidadMedida" : numeracion += 1
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
        spEntradas.ActiveSheet.Columns("idFamilia").Width = 50
        spEntradas.ActiveSheet.Columns("nombreFamilia").Width = 150
        spEntradas.ActiveSheet.Columns("idSubFamilia").Width = 50
        spEntradas.ActiveSheet.Columns("nombreSubFamilia").Width = 150
        spEntradas.ActiveSheet.Columns("idArticulo").Width = 50
        spEntradas.ActiveSheet.Columns("nombreArticulo").Width = 150
        spEntradas.ActiveSheet.Columns("nombreUnidadMedida").Width = 100
        spEntradas.ActiveSheet.Columns("cantidad").Width = 110
        spEntradas.ActiveSheet.Columns("precioUnitario").Width = 110
        spEntradas.ActiveSheet.Columns("total").Width = 110
        spEntradas.ActiveSheet.Columns("totalPesos").Width = 110
        spEntradas.ActiveSheet.Columns("observaciones").Width = 200
        spEntradas.ActiveSheet.Columns("factura").Width = 130
        spEntradas.ActiveSheet.Columns("chofer").Width = 130
        spEntradas.ActiveSheet.Columns("camion").Width = 130
        spEntradas.ActiveSheet.Columns("noEconomico").Width = 130
        spEntradas.ActiveSheet.Columns("idFamilia").CellType = tipoEntero
        spEntradas.ActiveSheet.Columns("nombreFamilia").CellType = tipoTexto
        spEntradas.ActiveSheet.Columns("idSubFamilia").CellType = tipoEntero
        spEntradas.ActiveSheet.Columns("nombreSubFamilia").CellType = tipoTexto
        spEntradas.ActiveSheet.Columns("idArticulo").CellType = tipoEntero
        spEntradas.ActiveSheet.Columns("nombreArticulo").CellType = tipoTexto
        spEntradas.ActiveSheet.Columns("nombreUnidadMedida").CellType = tipoTexto
        spEntradas.ActiveSheet.Columns("cantidad").CellType = tipoEntero
        spEntradas.ActiveSheet.Columns("precioUnitario").CellType = tipoDoble
        spEntradas.ActiveSheet.Columns("total").CellType = tipoDoble
        spEntradas.ActiveSheet.Columns("totalPesos").CellType = tipoDoble
        spEntradas.ActiveSheet.Columns("observaciones").CellType = tipoTexto
        spEntradas.ActiveSheet.Columns("factura").CellType = tipoTexto
        spEntradas.ActiveSheet.Columns("chofer").CellType = tipoTexto
        spEntradas.ActiveSheet.Columns("camion").CellType = tipoTexto
        spEntradas.ActiveSheet.Columns("noEconomico").CellType = tipoTexto
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
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("precioUnitario").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("precioUnitario").Index).Value = "Precio Unitario *".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("total").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("total").Index).Value = "Total *".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("totalPesos").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("totalPesos").Index).Value = "Total Pesos *".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("observaciones").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("observaciones").Index).Value = "Observaciones".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("factura").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("factura").Index).Value = "Factura".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("chofer").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("chofer").Index).Value = "Chofer".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("camion").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("camion").Index).Value = "Camión".ToUpper()
        spEntradas.ActiveSheet.AddColumnHeaderSpanCell(0, spEntradas.ActiveSheet.Columns("noEconomico").Index, 2, 1)
        spEntradas.ActiveSheet.ColumnHeader.Cells(0, spEntradas.ActiveSheet.Columns("noEconomico").Index).Value = "No Económico".ToUpper()
        Application.DoEvents()

    End Sub

    Private Sub ValidarGuardado()

        Me.Cursor = Cursors.WaitCursor
        Me.esGuardadoValido = True
        ' Parte superior.
        Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        If (idAlmacen <= 0) Then
            cbAlmacenes.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        Dim id As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtId.Text)
        If (id <= 0) Then
            txtId.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        Dim idMoneda As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbMonedas.SelectedValue)
        If (idMoneda <= 0) Then
            cbMonedas.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        Dim tipoCambio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroAUno(txtTipoCambio.Text)
        If (tipoCambio < 1) Then
            txtTipoCambio.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        Dim idTipoEntrada As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbTiposEntradas.SelectedValue)
        If (idTipoEntrada <= 0) Then
            cbTiposEntradas.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        Dim idProveedor As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtIdProveedor.Text)
        If (idProveedor <= 0) Then
            txtIdProveedor.BackColor = Color.Orange
            txtNombreProveedor.BackColor = Color.Orange
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
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
                Dim precioUnitario As String = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precioUnitario").Index).Text
                Dim precioUnitario2 As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precioUnitario").Index).Text)
                If (String.IsNullOrEmpty(precioUnitario) Or precioUnitario2 < 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precioUnitario").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
                Dim total As String = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Text
                Dim total2 As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Text)
                If (String.IsNullOrEmpty(total) Or total2 < 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
                Dim totalPesos As String = spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).Text
                Dim totalPesos2 As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).Text)
                If (String.IsNullOrEmpty(totalPesos) Or totalPesos2 < 0) Then
                    spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
            End If
        Next
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub GuardarEditarEntradas()

        Me.Cursor = Cursors.WaitCursor
        EliminarEntradas(False)
        ' Parte superior.
        Dim idAlmacen As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        Dim id As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtId.Text)
        Dim idExterno As String = txtIdExterno.Text
        Dim idMoneda As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbMonedas.SelectedValue)
        Dim tipoCambio As Double = ALMLogicaEntradas.Funciones.ValidarNumeroAUno(txtTipoCambio.Text)
        Dim fecha As Date = dtpFecha.Value
        Dim idTipoEntrada As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(cbTiposEntradas.SelectedValue)
        Dim idProveedor As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(txtIdProveedor.Text)
        ' Parte inferior.
        For fila As Integer = 0 To spEntradas.ActiveSheet.Rows.Count - 1
            Dim idFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("idArticulo").Index).Text)
            Dim cantidad As Integer = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).Text)
            Dim precioUnitario As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("precioUnitario").Index).Text)
            Dim total As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Text)
            Dim totalPesos As Double = ALMLogicaEntradas.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("totalPesos").Index).Text)
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
        LimpiarPantalla()
        CargarIdConsecutivo()
        AsignarFoco(txtId)
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
        Me.Cursor = Cursors.Default

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
         
        familia = 1
        subfamilia = 2
        articulo = 3
        proveedor = 4

    End Enum

#End Region
     
End Class