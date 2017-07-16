Imports System.IO
Imports System.ComponentModel

Public Class Principal

    ' Variables de objetos de entidades.
    Public usuarios As New ALMEntidadesCatalogos.Usuarios()
    Public almacenes As New ALMEntidadesCatalogos.Almacenes()
    Public familias As New ALMEntidadesCatalogos.Familias()
    Public subFamilias As New ALMEntidadesCatalogos.SubFamilias()
    Public articulos As New ALMEntidadesCatalogos.Articulos()
    Public unidadesMedidas As New ALMEntidadesCatalogos.UnidadesMedidas()
    Public proveedores As New ALMEntidadesCatalogos.Proveedores()
    Public clientes As New ALMEntidadesCatalogos.Clientes()
    Public monedas As New ALMEntidadesCatalogos.Monedas()
    Public tiposCambios As New ALMEntidadesCatalogos.TiposCambios()
    Public tiposEntradas As New ALMEntidadesCatalogos.TiposEntradas()
    Public tiposSalidas As New ALMEntidadesCatalogos.TiposSalidas()
    ' Variables de tipos de datos de spread.
    Public tipoTexto As New FarPoint.Win.Spread.CellType.TextCellType()
    Public tipoTextoContrasena As New FarPoint.Win.Spread.CellType.TextCellType()
    Public tipoEntero As New FarPoint.Win.Spread.CellType.NumberCellType()
    Public tipoDoble As New FarPoint.Win.Spread.CellType.NumberCellType()
    Public tipoPorcentaje As New FarPoint.Win.Spread.CellType.PercentCellType()
    Public tipoHora As New FarPoint.Win.Spread.CellType.DateTimeCellType()
    Public tipoFecha As New FarPoint.Win.Spread.CellType.DateTimeCellType()
    Public tipoBooleano As New FarPoint.Win.Spread.CellType.CheckBoxCellType()
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
    Public medidasUnaVez As Boolean = False
    Public opcionSeleccionada As Integer = 0
    Public estaCerrando As Boolean = False
    Public ejecutarProgramaPrincipal As New ProcessStartInfo()
    Public prefijoBaseDatosAlmacen As String = "ALM" & "_"
    ' Variable de desarrollo.
    Public esDesarrollo As Boolean = False

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
        'If (Not ValidarAccesoTotal()) Then
        '    Salir()
        'End If
        CargarEncabezados()
        CargarTitulosDirectorio()
        CargarMedidas()
        FormatearSpread()
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

    Private Sub spAlmacen_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spAlmacenes.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spCatalogos)
        End If

    End Sub

    Private Sub spAlmacen_KeyDown(sender As Object, e As KeyEventArgs) Handles spAlmacenes.KeyDown

        If (e.KeyData = Keys.F6) Then ' Eliminar un registro.
            If (Me.opcionSeleccionada = OpcionMenu.almacenes) Then
                If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                    'Dim fila As Integer = spAlmacen.ActiveSheet.ActiveRowIndex
                    'Dim id As Integer = 0
                    'Dim idUsuario As Integer = LogicaCatalogos.Funciones.ValidarNumero(spAlmacen.ActiveSheet.Cells(fila, spAlmacen.ActiveSheet.Columns("id").Index).Text)
                    'usuarios.EId = idUsuario
                    'Dim tieneDatos As Boolean = usuarios.ValidarActividadPorId()
                    'If (tieneDatos) Then
                    '    MessageBox.Show("No se puede eliminar este registro, ya que contiene actividades capturadas.", "No permitido.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    'Else
                    EliminarRegistroDeSpread(spAlmacenes)
                    'End If
                End If
            End If
        ElseIf (e.KeyData = Keys.Enter) Then ' Validar registros.
            If (Me.opcionSeleccionada = OpcionMenu.almacenes) Then
                ControlarSpreadEnter(spAlmacenes)
            ElseIf (Me.opcionSeleccionada = OpcionMenu.familias) Then
                ControlarSpreadEnter(spFamilias)
            End If
        End If

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        If (Me.opcionSeleccionada = OpcionMenu.almacenes) Then
            GuardarEditarAlmacenes()
        ElseIf (Me.opcionSeleccionada = OpcionMenu.familias) Then
            If (Me.filaAlmacen >= 0) Then
                GuardarEditarFamilias()
            End If
        ElseIf (Me.opcionSeleccionada = OpcionMenu.subFamilias) Then
            If (Me.filaAlmacen >= 0 And Me.filaFamilia >= 0) Then
                GuardarEditarSubFamilias()
            End If
        ElseIf (Me.opcionSeleccionada = OpcionMenu.articulos) Then
            If (Me.filaAlmacen >= 0 And Me.filaFamilia >= 0 And Me.filaSubFamilia >= 0) Then
                GuardarEditarArticulos()
            End If
        ElseIf (Me.opcionSeleccionada = OpcionMenu.proveedores) Then
            GuardarEditarProveedores()
        ElseIf (Me.opcionSeleccionada = OpcionMenu.clientes) Then
            GuardarEditarClientes()
        ElseIf (Me.opcionSeleccionada = OpcionMenu.monedas) Then
            GuardarEditarMonedas()
        ElseIf (Me.opcionSeleccionada = OpcionMenu.tiposCambios) Then
            GuardarEditarTiposCambios()
        ElseIf (Me.opcionSeleccionada = OpcionMenu.tiposEntradas) Then
            GuardarEditarTiposEntradas()
        ElseIf (Me.opcionSeleccionada = OpcionMenu.tiposSalidas) Then
            GuardarEditarTiposSalidas()
        ElseIf (Me.opcionSeleccionada = OpcionMenu.unidadesMedidas) Then
            GuardarEditarUnidadesMedidas()
        End If

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        If (Me.opcionSeleccionada = OpcionMenu.almacenes) Then
            EliminarAlmacenes(True)
        ElseIf (Me.opcionSeleccionada = OpcionMenu.familias) Then
            If (Me.filaAlmacen >= 0) Then
                Dim idAlmacen As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spAlmacenes.ActiveSheet.Cells(Me.filaAlmacen, spAlmacenes.ActiveSheet.Columns("id").Index).Value)
                EliminarFamilias(True, idAlmacen)
            End If
        ElseIf (Me.opcionSeleccionada = OpcionMenu.subFamilias) Then
            If (Me.filaAlmacen >= 0 And Me.filaFamilia >= 0) Then
                Dim idAlmacen As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spAlmacenes.ActiveSheet.Cells(Me.filaAlmacen, spAlmacenes.ActiveSheet.Columns("id").Index).Value)
                Dim idFamilia As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spFamilias.ActiveSheet.Cells(Me.filaFamilia, spFamilias.ActiveSheet.Columns("id").Index).Value)
                EliminarSubFamilias(True, idAlmacen, idFamilia)
            End If
        ElseIf (Me.opcionSeleccionada = OpcionMenu.articulos) Then
            If (Me.filaAlmacen >= 0 And Me.filaFamilia >= 0 And Me.filaSubFamilia >= 0) Then
                Dim idAlmacen As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spAlmacenes.ActiveSheet.Cells(Me.filaAlmacen, spAlmacenes.ActiveSheet.Columns("id").Index).Value)
                Dim idFamilia As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spFamilias.ActiveSheet.Cells(Me.filaFamilia, spFamilias.ActiveSheet.Columns("id").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spSubFamilias.ActiveSheet.Cells(Me.filaSubFamilia, spSubFamilias.ActiveSheet.Columns("id").Index).Value)
                EliminarArticulos(True, idAlmacen, idFamilia, idSubFamilia)
            End If
        ElseIf (Me.opcionSeleccionada = OpcionMenu.proveedores) Then
            EliminarProveedores(True)
        ElseIf (Me.opcionSeleccionada = OpcionMenu.clientes) Then
            EliminarClientes(True)
        ElseIf (Me.opcionSeleccionada = OpcionMenu.monedas) Then
            EliminarMonedas(True)
        ElseIf (Me.opcionSeleccionada = OpcionMenu.tiposCambios) Then
            EliminarTiposCambios(True, Today)
        ElseIf (Me.opcionSeleccionada = OpcionMenu.tiposEntradas) Then
            EliminarTiposEntradas(True)
        ElseIf (Me.opcionSeleccionada = OpcionMenu.tiposSalidas) Then
            EliminarTiposSalidas(True)
        ElseIf (Me.opcionSeleccionada = OpcionMenu.unidadesMedidas) Then
            EliminarUnidadesMedidas(True)
        End If

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
        If (Me.opcionSeleccionada = OpcionMenu.articulos) Then
            spArticulos.ActiveSheet.Cells(spArticulos.ActiveSheet.ActiveRowIndex, spArticulos.ActiveSheet.Columns("idUnidadMedida").Index).Text = spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spArticulos.ActiveSheet.Cells(spArticulos.ActiveSheet.ActiveRowIndex, spArticulos.ActiveSheet.Columns("nombreUnidadMedida").Index).Text = spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (Me.opcionSeleccionada = OpcionMenu.tiposCambios) Then
            spVarios.ActiveSheet.Cells(spVarios.ActiveSheet.ActiveRowIndex, spVarios.ActiveSheet.Columns("idMoneda").Index).Text = spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spVarios.ActiveSheet.Cells(spVarios.ActiveSheet.ActiveRowIndex, spVarios.ActiveSheet.Columns("nombreMoneda").Index).Text = spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
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

    Private Sub rbtnAlmacenes_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnAlmacenes.CheckedChanged

        If (rbtnAlmacenes.Checked) Then
            SeleccionoAlmacenes()
        End If

    End Sub

    Private Sub rbtnFamilias_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnFamilias.CheckedChanged

        If (rbtnFamilias.Checked) Then
            SeleccionoFamilias()
        End If

    End Sub

    Private Sub rbtnSubFamilias_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnSubFamilias.CheckedChanged

        If (rbtnSubFamilias.Checked) Then
            SeleccionoSubFamilias()
        End If

    End Sub

    Private Sub rbtnArticulos_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnArticulos.CheckedChanged

        If (rbtnArticulos.Checked) Then
            SeleccionoArticulos()
        End If

    End Sub

    Private Sub spAlmacen_CellClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spAlmacenes.CellClick

        If (Me.opcionSeleccionada = OpcionMenu.familias Or Me.opcionSeleccionada = OpcionMenu.subFamilias Or Me.opcionSeleccionada = OpcionMenu.articulos) Then
            If (e.Row >= 0) Then
                Me.filaAlmacen = e.Row
                spFamilias.Show()
                Dim idAlmacen As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spAlmacenes.ActiveSheet.Cells(Me.filaAlmacen, spAlmacenes.ActiveSheet.Columns("id").Index).Value)
                CargarFamilias(idAlmacen)
            End If
        End If

    End Sub

    Private Sub spFamilia_KeyDown(sender As Object, e As KeyEventArgs) Handles spFamilias.KeyDown

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spFamilias)
        ElseIf e.KeyData = Keys.F6 Then ' Eliminar un registro.
            If (Me.opcionSeleccionada = OpcionMenu.familias) Then
                If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                    EliminarRegistroDeSpread(spFamilias)
                End If
            End If
        End If

    End Sub

    Private Sub spFamilia_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spFamilias.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spFamilias)
        End If

    End Sub

    Private Sub spFamilia_CellClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spFamilias.CellClick

        If (Me.opcionSeleccionada = OpcionMenu.subFamilias Or Me.opcionSeleccionada = OpcionMenu.articulos) Then
            If (e.Row >= 0) Then
                Me.filaFamilia = e.Row
                spSubFamilias.Show()
                Dim idAlmacen As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spAlmacenes.ActiveSheet.Cells(Me.filaAlmacen, spAlmacenes.ActiveSheet.Columns("id").Index).Value)
                Dim idFamilia As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spFamilias.ActiveSheet.Cells(Me.filaFamilia, spFamilias.ActiveSheet.Columns("id").Index).Value)
                CargarSubFamilias(idAlmacen, idFamilia)
            End If
        End If

    End Sub

    Private Sub spSubFamilia_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spSubFamilias.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spSubFamilias)
        End If

    End Sub

    Private Sub spSubFamilia_KeyDown(sender As Object, e As KeyEventArgs) Handles spSubFamilias.KeyDown

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spSubFamilias)
        ElseIf e.KeyData = Keys.F6 Then ' Eliminar un registro.
            If (Me.opcionSeleccionada = OpcionMenu.subFamilias) Then
                If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                    EliminarRegistroDeSpread(spSubFamilias)
                End If
            End If
        End If

    End Sub

    Private Sub spSubFamilias_CellClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spSubFamilias.CellClick

        If (Me.opcionSeleccionada = OpcionMenu.articulos) Then
            If (e.Row >= 0) Then
                Me.filaSubFamilia = e.Row
                spArticulos.Show()
                Dim idAlmacen As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spAlmacenes.ActiveSheet.Cells(Me.filaAlmacen, spAlmacenes.ActiveSheet.Columns("id").Index).Value)
                Dim idFamilia As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spFamilias.ActiveSheet.Cells(Me.filaFamilia, spFamilias.ActiveSheet.Columns("id").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spSubFamilias.ActiveSheet.Cells(Me.filaSubFamilia, spSubFamilias.ActiveSheet.Columns("id").Index).Value)
                CargarArticulos(idAlmacen, idFamilia, idSubFamilia)
            End If
        End If

    End Sub

    Private Sub spArticulos_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spArticulos.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spArticulos)
        End If

    End Sub

    Private Sub spArticulos_KeyDown(sender As Object, e As KeyEventArgs) Handles spArticulos.KeyDown

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spArticulos)
        ElseIf (e.KeyData = Keys.F6) Then ' Eliminar un registro.
            If (Me.opcionSeleccionada = OpcionMenu.articulos) Then
                If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                    EliminarRegistroDeSpread(spArticulos)
                End If
            End If
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            If (Me.opcionSeleccionada = OpcionMenu.articulos) Then
                If (spArticulos.ActiveSheet.ActiveColumnIndex = spArticulos.ActiveSheet.Columns("idUnidadMedida").Index) Or (spArticulos.ActiveSheet.ActiveColumnIndex = spArticulos.ActiveSheet.Columns("nombreUnidadMedida").Index) Then
                    spArticulos.Enabled = False
                    CargarCatalogoUnidadesMedidas()
                    FormatearSpreadCatalogo(False)
                    spCatalogos.Focus()
                End If
            End If
        End If

    End Sub

    Private Sub pnlMenu_MouseEnter(sender As Object, e As EventArgs) Handles pnlMenu.MouseEnter

        AsignarTooltips("Menú.")

    End Sub

    Private Sub rbtnProveedores_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnProveedores.CheckedChanged

        If (rbtnProveedores.Checked) Then
            SeleccionoProveedores()
        End If

    End Sub

    Private Sub spVarios_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spVarios.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spVarios)
        End If

    End Sub

    Private Sub spVarios_KeyDown(sender As Object, e As KeyEventArgs) Handles spVarios.KeyDown

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spVarios)
        ElseIf (e.KeyData = Keys.F6) Then ' Eliminar un registro. 
            If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                EliminarRegistroDeSpread(spVarios)
            End If
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            If (Me.opcionSeleccionada = OpcionMenu.tiposCambios) Then
                If (spVarios.ActiveSheet.ActiveColumnIndex = spVarios.ActiveSheet.Columns("idMoneda").Index) Or (spVarios.ActiveSheet.ActiveColumnIndex = spVarios.ActiveSheet.Columns("nombreMoneda").Index) Then
                    spVarios.Enabled = False
                    CargarCatalogoMonedas()
                    FormatearSpreadCatalogo(False)
                    spCatalogos.Focus()
                End If
            End If
        End If

    End Sub

    Private Sub rbtnMonedas_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnMonedas.CheckedChanged

        If (rbtnMonedas.Checked) Then
            SeleccionoMonedas()
        End If

    End Sub

    Private Sub rbtnTiposEntradas_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnTiposEntradas.CheckedChanged

        If (rbtnTiposEntradas.Checked) Then
            SeleccionoTiposEntradas()
        End If

    End Sub

    Private Sub rbtnTiposSalidas_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnTiposSalidas.CheckedChanged

        If (rbtnTiposSalidas.Checked) Then
            SeleccionoTiposSalidas()
        End If

    End Sub

    Private Sub Principal_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize

        If (Not Me.medidasUnaVez) Then
            If (pnlMenu.HorizontalScroll.Visible) Then
                pnlMenu.Height += 15
                CargarMedidas()
                Me.medidasUnaVez = True
            End If
        Else
            If (Not pnlMenu.HorizontalScroll.Visible) Then
                pnlMenu.Height -= 15
                CargarMedidas()
                Me.medidasUnaVez = False
            End If
        End If

    End Sub

    Private Sub rbtnUnidadesMedidas_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnUnidadesMedidas.CheckedChanged

        If (rbtnUnidadesMedidas.Checked) Then
            SeleccionoUnidadesMedidas()
        End If

    End Sub

    Private Sub rbtnTiposCambios_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnTiposCambios.CheckedChanged

        If (rbtnTiposCambios.Checked) Then
            SeleccionoTiposCambios()
        End If

    End Sub

    Private Sub rbtnClientes_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnClientes.CheckedChanged

        If (rbtnClientes.Checked) Then
            SeleccionoClientes()
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
            txtAyuda.Text = "Sección de Ayuda: " & vbNewLine & vbNewLine & "* Teclas básicas: " & vbNewLine & "F5 sirve para mostrar catálogos. " & vbNewLine & "F6 sirve para eliminar un registro únicamente. " & vbNewLine & "Escape sirve para ocultar catálogos que se encuentren desplegados. " & vbNewLine & vbNewLine & "* Catálogos desplegados: " & vbNewLine & "Cuando se muestra algún catálogo, al seleccionar alguna opción de este, se va mostrando en tiempo real en la captura de donde se originó. Cuando se le da doble clic en alguna opción o a la tecla escape se oculta dicho catálogo. " & vbNewLine & vbNewLine & "* Datos obligatorios:" & vbNewLine & "Todos los que tengan el simbolo * son estrictamente obligatorios." & vbNewLine & vbNewLine & "* Captura:" & vbNewLine & "* Almacenes: " & vbNewLine & "En esta pestaña se capturarán los distintos almacenes que se manejan. " & vbNewLine & "* Familias: " & vbNewLine & "En esta parte se agrupan por un primer nivel dependiendo el almacén, ejemplo: insecticidas, agroquimicos, etc. " & vbNewLine & "* SubFamilias: " & vbNewLine & "En este apartado se capturarán todos los segundos niveles de acuerdo al almacén y familia correspondiente seleccionadas. " & vbNewLine & "* Artículos: " & vbNewLine & "En este lugar se agrupan los terceros niveles de acuerdo al almacén, familia y subfamilia correspondiente seleccionadas. " & vbNewLine & "* Existen distintas opciones que se tienen que configurar como proveedores, monedas, tipos de cambio, etc. en las cuales especifíca claramente todo lo que se necesita." & vbNewLine & vbNewLine & "* Para todas las opciones existen los botones de guardar/editar y eliminar todo dependiendo lo que se necesite hacer. " : Application.DoEvents()
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

        If ((Not ALMLogicaCatalogos.Usuarios.accesoTotal) Or (ALMLogicaCatalogos.Usuarios.accesoTotal = 0) Or (ALMLogicaCatalogos.Usuarios.accesoTotal = False)) Then
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
        tp.SetToolTip(Me.pnlMenu, "Menú.")

    End Sub

    Private Sub AsignarTooltips(ByVal texto As String)

        lblDescripcionTooltip.Text = texto

    End Sub

    Private Sub ConfigurarConexiones()

        If (Me.esDesarrollo) Then
            ALMLogicaCatalogos.Directorios.id = 1
            ALMLogicaCatalogos.Directorios.instanciaSql = "BERRY1-DELL\SQLEXPRESS2008"
            ALMLogicaCatalogos.Directorios.usuarioSql = "AdminBerry"
            ALMLogicaCatalogos.Directorios.contrasenaSql = "@berry2017"
            ALMLogicaCatalogos.Usuarios.id = 1
        Else
            ALMLogicaCatalogos.Directorios.ObtenerParametros()
            ALMLogicaCatalogos.Usuarios.ObtenerParametros()
        End If
        ALMEntidadesCatalogos.BaseDatos.ECadenaConexionCatalogo = "Catalogo" & ALMLogicaCatalogos.Directorios.id
        ALMEntidadesCatalogos.BaseDatos.ECadenaConexionConfiguracion = "Configuracion" & ALMLogicaCatalogos.Directorios.id
        ALMEntidadesCatalogos.BaseDatos.ECadenaConexionAlmacen = "Almacen" & ALMLogicaCatalogos.Directorios.id
        ALMEntidadesCatalogos.BaseDatos.AbrirConexionCatalogo()
        ALMEntidadesCatalogos.BaseDatos.AbrirConexionConfiguracion()
        ALMEntidadesCatalogos.BaseDatos.AbrirConexionAlmacen()
        ConsultarInformacionUsuario()
        CargarPrefijoBaseDatosAlmacen()

    End Sub

    Private Sub CargarPrefijoBaseDatosAlmacen()

        ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen = Me.prefijoBaseDatosAlmacen

    End Sub

    Private Sub ConsultarInformacionUsuario()

        Dim lista As New List(Of ALMEntidadesCatalogos.Usuarios)
        usuarios.EId = ALMLogicaCatalogos.Usuarios.id
        lista = usuarios.ObtenerListado()
        If (lista.Count > 0) Then
            ALMLogicaCatalogos.Usuarios.id = lista(0).EId
            ALMLogicaCatalogos.Usuarios.nombre = lista(0).ENombre
            ALMLogicaCatalogos.Usuarios.contrasena = lista(0).EContrasena
            ALMLogicaCatalogos.Usuarios.nivel = lista(0).ENivel
            ALMLogicaCatalogos.Usuarios.accesoTotal = lista(0).EAccesoTotal
        End If

    End Sub

    Private Sub CargarTitulosDirectorio()

        Me.Text = "Programa:  " + Me.nombreEstePrograma + "              Directorio:  " + ALMLogicaCatalogos.Directorios.nombre + "              Usuario:  " + ALMLogicaCatalogos.Usuarios.nombre

    End Sub

    Private Sub CargarEncabezados()

        lblEncabezadoPrograma.Text = "Programa: " + Me.Text
        lblEncabezadoEmpresa.Text = "Directorio: " + ALMLogicaCatalogos.Directorios.nombre
        lblEncabezadoUsuario.Text = "Usuario: " + ALMLogicaCatalogos.Usuarios.nombre

    End Sub

    Private Sub AbrirPrograma(nombre As String, salir As Boolean)

        If (Me.esDesarrollo) Then
            Exit Sub
        End If
        ejecutarProgramaPrincipal.UseShellExecute = True
        ejecutarProgramaPrincipal.FileName = nombre & Convert.ToString(".exe")
        ejecutarProgramaPrincipal.WorkingDirectory = Directory.GetCurrentDirectory()
        ejecutarProgramaPrincipal.Arguments = ALMLogicaCatalogos.Directorios.id.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCatalogos.Directorios.nombre.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCatalogos.Directorios.descripcion.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCatalogos.Directorios.rutaLogo.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCatalogos.Directorios.esPredeterminado.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCatalogos.Directorios.instanciaSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCatalogos.Directorios.usuarioSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCatalogos.Directorios.contrasenaSql.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de directorios, indice 9 ;)".Replace(" ", "|") & " " & ALMLogicaCatalogos.Usuarios.id.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de usuario, indice 11 ;)".Replace(" ", "|")
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

    End Sub

    Private Sub CargarMedidas()

        Me.izquierda = 0
        Me.arriba = pnlMenu.Height
        Me.anchoTotal = pnlCuerpo.Width
        Me.altoTotal = pnlCuerpo.Height - pnlMenu.Height
        Me.anchoMitad = Me.anchoTotal / 2
        Me.altoMitad = Me.altoTotal / 2
        Me.anchoTercio = Me.anchoTotal / 3
        Me.altoTercio = Me.altoTotal / 3
        Me.altoCuarto = Me.altoTotal / 4

    End Sub

    Private Sub OcultarSpreads()

        spAlmacenes.Hide()
        spFamilias.Hide()
        spSubFamilias.Hide()
        spArticulos.Hide()
        spCatalogos.Hide()
        spVarios.Hide()
        Application.DoEvents()

    End Sub

    Private Sub FormatearSpread()

        ' Se cargan tipos de datos de spread.
        CargarTiposDeDatos()
        ' Se cargan las opciones generales de cada spread.
        OcultarSpreads()
        spAlmacenes.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spFamilias.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spSubFamilias.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spArticulos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spVarios.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spCatalogos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Midnight
        spAlmacenes.ActiveSheet.GrayAreaBackColor = Principal.colorAreaGris
        spFamilias.ActiveSheet.GrayAreaBackColor = Principal.colorAreaGris
        spSubFamilias.ActiveSheet.GrayAreaBackColor = Principal.colorAreaGris
        spArticulos.ActiveSheet.GrayAreaBackColor = Principal.colorAreaGris
        spVarios.ActiveSheet.GrayAreaBackColor = Principal.colorAreaGris
        spAlmacenes.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spFamilias.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spSubFamilias.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spArticulos.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spVarios.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spCatalogos.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spAlmacenes.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spFamilias.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spSubFamilias.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spArticulos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spVarios.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spAlmacenes.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spFamilias.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spSubFamilias.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spArticulos.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spVarios.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spCatalogos.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spAlmacenes.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spAlmacenes.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spFamilias.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spFamilias.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spSubFamilias.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spSubFamilias.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spArticulos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spArticulos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spVarios.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spVarios.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Application.DoEvents()

    End Sub

    Private Sub EliminarRegistroDeSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.Rows.Remove(spread.ActiveSheet.ActiveRowIndex, 1)

    End Sub

    Private Sub ControlarSpreadEnter(ByVal spread As FarPoint.Win.Spread.FpSpread)

        Dim columnaActiva As Integer = spread.ActiveSheet.ActiveColumnIndex
        If (columnaActiva = spread.ActiveSheet.Columns.Count - 1) Then
            spread.ActiveSheet.Rows.Count += 1
        End If
        Dim fila As Integer = 0
        If (Me.opcionSeleccionada = OpcionMenu.articulos) Then
            If (columnaActiva = spArticulos.ActiveSheet.Columns("idUnidadMedida").Index) Then
                fila = spArticulos.ActiveSheet.ActiveRowIndex
                Dim idUnidadMedida As Integer = spArticulos.ActiveSheet.Cells(fila, spArticulos.ActiveSheet.Columns("idUnidadMedida").Index).Value
                unidadesMedidas.EId = idUnidadMedida
                If (idUnidadMedida > 0) Then
                    Dim lista As New List(Of ALMEntidadesCatalogos.UnidadesMedidas)
                    lista = unidadesMedidas.ObtenerListado()
                    If (lista.Count > 0) Then
                        spArticulos.ActiveSheet.Cells(fila, spArticulos.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = lista(0).ENombre
                    End If
                End If
            End If
        ElseIf (Me.opcionSeleccionada = OpcionMenu.tiposCambios) Then
            fila = spVarios.ActiveSheet.ActiveRowIndex
            If (columnaActiva = spVarios.ActiveSheet.Columns("idMoneda").Index) Then
                Dim idMonedaa As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("idMoneda").Index).Text)
                If (idMonedaa > 0) Then
                    Dim lista As New List(Of ALMEntidadesCatalogos.Monedas)
                    monedas.EId = idMonedaa
                    lista = monedas.ObtenerListado
                    If (lista.Count > 0) Then
                        spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("nombreMoneda").Index).Text = lista(0).ENombre
                    End If
                Else
                    spVarios.ActiveSheet.SetActiveCell(fila - 1, spVarios.ActiveSheet.Columns.Count - 1)
                End If
            ElseIf (columnaActiva = spVarios.ActiveSheet.Columns("fecha").Index) Then
                Dim fecha As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("fecha").Index).Text
                If (String.IsNullOrEmpty(fecha)) Then
                    spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("fecha").Index).Value = Today
                End If
            ElseIf (columnaActiva = spVarios.ActiveSheet.Columns("valor").Index) Then
                Dim idMoneda As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("idMoneda").Index).Text)
                Dim fecha As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("fecha").Index).Text
                If (String.IsNullOrEmpty(fecha)) Then
                    spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("fecha").Index).Value = Today
                End If
                For indice = 0 To spVarios.ActiveSheet.Rows.Count - 1
                    Dim fechaa As String = spVarios.ActiveSheet.Cells(indice, spVarios.ActiveSheet.Columns("fecha").Index).Text
                    Dim idMonedaa As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spVarios.ActiveSheet.Cells(indice, spVarios.ActiveSheet.Columns("idMoneda").Index).Text)
                    If (Not String.IsNullOrEmpty(fecha)) Then
                        If (fechaa = fecha And idMonedaa = idMoneda And indice <> fila) Then
                            LimpiarFilaSpread(spVarios, fila)
                            spVarios.ActiveSheet.SetActiveCell(fila - 1, spVarios.ActiveSheet.Columns.Count - 1)
                        End If
                    End If
                Next
                Dim valor As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("valor").Index).Text
                If (String.IsNullOrEmpty(valor)) Then
                    spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("valor").Index).Text = String.Empty
                    spVarios.ActiveSheet.SetActiveCell(fila, spVarios.ActiveSheet.Columns("valor").Index - 1)
                End If
            End If
        End If

    End Sub

    Private Sub LimpiarFilaSpread(ByVal spread As FarPoint.Win.Spread.FpSpread, ByVal fila As Integer)

        spread.ActiveSheet.ClearRange(fila, 0, 1, spread.ActiveSheet.Columns.Count, True)
        spread.ActiveSheet.SetActiveCell(fila, -1)
        Application.DoEvents()

    End Sub

    Private Sub LimpiarSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.ClearRange(0, 0, spread.ActiveSheet.Rows.Count, spread.ActiveSheet.Columns.Count, True) : Application.DoEvents()

    End Sub

    Private Sub CargarCatalogoUnidadesMedidas()

        unidadesMedidas.EId = 0
        spCatalogos.ActiveSheet.DataSource = unidadesMedidas.ObtenerListado() ': Application.DoEvents()
        spCatalogos.Focus()

    End Sub

    Private Sub CargarCatalogoMonedas()

        monedas.EId = 0
        spCatalogos.ActiveSheet.DataSource = monedas.ObtenerListado()
        spCatalogos.Focus()

    End Sub

    Private Sub FormatearSpreadCatalogo(ByVal izquierda As Boolean)

        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spCatalogos.Width = 300
        If (izquierda) Then
            spCatalogos.Location = New Point(Me.izquierda, Me.arriba)
        Else
            spCatalogos.Location = New Point(Me.anchoTotal - spCatalogos.Width, Me.arriba)
        End If
        spCatalogos.Visible = True
        spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        spCatalogos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCatalogos.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        spCatalogos.Height = Me.altoTotal
        Dim numeracion As Integer = 0
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spCatalogos.ActiveSheet.Columns("id").Width = 50
        spCatalogos.ActiveSheet.Columns("nombre").Width = 210
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("nombre").Index).Value = "Nombre".ToUpper
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosMedianosSpread
        Application.DoEvents()

    End Sub

    Private Sub VolverFocoCatalogos()

        If (Me.opcionSeleccionada = OpcionMenu.articulos) Then
            spArticulos.Enabled = True
            spArticulos.Focus()
            spArticulos.ActiveSheet.SetActiveCell(spArticulos.ActiveSheet.ActiveRowIndex, spArticulos.ActiveSheet.Columns("idUnidadMedida").Index + 2)
        ElseIf (Me.opcionSeleccionada = OpcionMenu.tiposCambios) Then
            spVarios.Enabled = True
            spVarios.Focus()
            spVarios.ActiveSheet.SetActiveCell(spVarios.ActiveSheet.ActiveRowIndex, spVarios.ActiveSheet.Columns("idMoneda").Index + 2)
        End If
        spCatalogos.Visible = False

    End Sub

    Private Sub SeleccionoAlmacenes()

        Me.Cursor = Cursors.WaitCursor
        Me.opcionSeleccionada = OpcionMenu.almacenes
        ReiniciarValoresIndices()
        OcultarSpreads()
        CargarAlmacenes()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CargarAlmacenes()

        If (Me.opcionSeleccionada = OpcionMenu.almacenes) Then
            spAlmacenes.Height = Me.altoTotal
            spAlmacenes.Width = Me.anchoTotal
            spAlmacenes.Top = Me.arriba
            spAlmacenes.Left = Me.izquierda
        ElseIf (Me.opcionSeleccionada = OpcionMenu.familias) Then
            spAlmacenes.Height = Me.altoTotal
            spAlmacenes.Width = Me.anchoMitad
            spAlmacenes.Top = Me.arriba
            spAlmacenes.Left = Me.izquierda
        ElseIf (Me.opcionSeleccionada = OpcionMenu.subFamilias) Then
            spAlmacenes.Height = Me.altoTotal
            spAlmacenes.Width = Me.anchoTercio
            spAlmacenes.Top = Me.arriba
            spAlmacenes.Left = Me.izquierda
        ElseIf (Me.opcionSeleccionada = OpcionMenu.articulos) Then
            spAlmacenes.Height = Me.altoCuarto
            spAlmacenes.Width = Me.anchoTercio
            spAlmacenes.Top = Me.arriba
            spAlmacenes.Left = Me.izquierda
        End If
        spAlmacenes.Show()
        spAlmacenes.ActiveSheet.DataSource = almacenes.ObtenerListadoReporte()
        FormatearSpreadAlmacenes()

    End Sub

    Private Sub FormatearSpreadAlmacenes()

        spAlmacenes.ActiveSheet.ColumnHeader.RowCount = 2
        spAlmacenes.ActiveSheet.ColumnHeader.Rows(0, spAlmacenes.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spAlmacenes.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spAlmacenes.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        If (Me.opcionSeleccionada = OpcionMenu.almacenes) Then
            spAlmacenes.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        Else
            spAlmacenes.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        End If
        If (Me.opcionSeleccionada = OpcionMenu.almacenes) Then
            ControlarSpreadEnterASiguienteColumna(spAlmacenes)
        End If
        Dim numeracion As Integer = 0
        spAlmacenes.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spAlmacenes.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spAlmacenes.ActiveSheet.Columns(numeracion).Tag = "abreviatura" : numeracion += 1
        spAlmacenes.ActiveSheet.Columns("id").Width = 50
        spAlmacenes.ActiveSheet.Columns("nombre").Width = 400
        spAlmacenes.ActiveSheet.Columns("abreviatura").Width = 160
        spAlmacenes.ActiveSheet.Columns("id").CellType = tipoEntero
        spAlmacenes.ActiveSheet.Columns("nombre").CellType = tipoTexto
        spAlmacenes.ActiveSheet.Columns("abreviatura").CellType = tipoTexto
        spAlmacenes.ActiveSheet.AddColumnHeaderSpanCell(0, 0, 1, spAlmacenes.ActiveSheet.Columns.Count)
        Dim obligatorio As String = String.Empty
        If (Me.opcionSeleccionada = OpcionMenu.almacenes) Then
            obligatorio = " *"
        End If
        spAlmacenes.ActiveSheet.ColumnHeader.Cells(0, 0).Value = "A  l  m  a  c  e  n  e  s".ToUpper()
        spAlmacenes.ActiveSheet.ColumnHeader.Cells(1, spAlmacenes.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper() & obligatorio
        spAlmacenes.ActiveSheet.ColumnHeader.Cells(1, spAlmacenes.ActiveSheet.Columns("nombre").Index).Value = "Nombre".ToUpper() & obligatorio
        spAlmacenes.ActiveSheet.ColumnHeader.Cells(1, spAlmacenes.ActiveSheet.Columns("abreviatura").Index).Value = "Abreviatura".ToUpper()
        If (Me.opcionSeleccionada = OpcionMenu.almacenes) Then
            spAlmacenes.ActiveSheet.Rows.Count += 1
        End If
        If (Me.opcionSeleccionada <> OpcionMenu.almacenes And Me.opcionSeleccionada <> OpcionMenu.familias) Then
            spAlmacenes.ActiveSheet.Columns("abreviatura").Visible = False
        Else
            spAlmacenes.ActiveSheet.Columns("abreviatura").Visible = True
        End If
        spAlmacenes.Focus()
        Application.DoEvents()

    End Sub

    Private Sub GuardarEditarAlmacenes()

        EliminarAlmacenes(False)
        For fila As Integer = 0 To spAlmacenes.ActiveSheet.Rows.Count - 1
            Dim id As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spAlmacenes.ActiveSheet.Cells(fila, spAlmacenes.ActiveSheet.Columns("id").Index).Text)
            Dim nombre As String = spAlmacenes.ActiveSheet.Cells(fila, spAlmacenes.ActiveSheet.Columns("nombre").Index).Text
            Dim abreviatura As String = ALMLogicaCatalogos.Funciones.ValidarLetra(spAlmacenes.ActiveSheet.Cells(fila, spAlmacenes.ActiveSheet.Columns("abreviatura").Index).Value)
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
        If (conMensaje And respuestaSi) Then
            CargarAlmacenes()
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        End If

    End Sub

    Private Sub SeleccionoFamilias()

        Me.Cursor = Cursors.WaitCursor
        Me.opcionSeleccionada = OpcionMenu.familias
        ReiniciarValoresIndices()
        OcultarSpreads()
        CargarFamilias(-1)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CargarFamilias(ByVal idAlmacen As Integer)

        If (idAlmacen < 0) Then
            CargarAlmacenes()
        End If
        If (idAlmacen > 0) Then
            If (Me.opcionSeleccionada = OpcionMenu.familias) Then
                spFamilias.Height = Me.altoTotal
                spFamilias.Width = Me.anchoMitad
                spFamilias.Top = Me.arriba
                spFamilias.Left = Me.anchoMitad
            ElseIf (Me.opcionSeleccionada = OpcionMenu.subFamilias) Then
                spFamilias.Height = Me.altoTotal
                spFamilias.Width = Me.anchoTercio
                spFamilias.Top = Me.arriba
                spFamilias.Left = Me.anchoTercio
            ElseIf (Me.opcionSeleccionada = OpcionMenu.articulos) Then
                spFamilias.Height = Me.altoCuarto
                spFamilias.Width = Me.anchoTercio
                spFamilias.Top = Me.arriba
                spFamilias.Left = Me.anchoTercio
            End If
            familias.EIdAlmacen = idAlmacen
            familias.EId = 0
            spFamilias.ActiveSheet.DataSource = familias.ObtenerListadoReporte()
            FormatearSpreadFamilias()
        End If

    End Sub

    Private Sub FormatearSpreadFamilias()

        spFamilias.ActiveSheet.ColumnHeader.RowCount = 2
        spFamilias.ActiveSheet.ColumnHeader.Rows(0, spFamilias.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spFamilias.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spFamilias.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        If (Me.opcionSeleccionada = OpcionMenu.familias) Then
            spFamilias.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        Else
            spFamilias.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        End If
        If (Me.opcionSeleccionada = OpcionMenu.familias) Then
            ControlarSpreadEnterASiguienteColumna(spFamilias)
        End If
        Dim numeracion As Integer = 0
        spFamilias.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spFamilias.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spFamilias.ActiveSheet.Columns("id").Width = 50
        spFamilias.ActiveSheet.Columns("nombre").Width = 400
        spFamilias.ActiveSheet.Columns("id").CellType = tipoEntero
        spFamilias.ActiveSheet.Columns("nombre").CellType = tipoTexto
        spFamilias.ActiveSheet.AddColumnHeaderSpanCell(0, 0, 1, spFamilias.ActiveSheet.Columns.Count)
        Dim obligatorio As String = String.Empty
        If (Me.opcionSeleccionada = OpcionMenu.familias) Then
            obligatorio = " *"
        End If
        spFamilias.ActiveSheet.ColumnHeader.Cells(0, 0).Value = "F  a  m  i  l  i  a  s".ToUpper()
        spFamilias.ActiveSheet.ColumnHeader.Cells(1, spFamilias.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper() & obligatorio
        spFamilias.ActiveSheet.ColumnHeader.Cells(1, spFamilias.ActiveSheet.Columns("nombre").Index).Value = "Nombre".ToUpper() & obligatorio
        If (Me.opcionSeleccionada = OpcionMenu.familias) Then
            spFamilias.ActiveSheet.Rows.Count += 1
        End If
        Application.DoEvents()

    End Sub

    Private Sub GuardarEditarFamilias()

        Dim idAlmacen As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spAlmacenes.ActiveSheet.Cells(spAlmacenes.ActiveSheet.ActiveRowIndex, spAlmacenes.ActiveSheet.Columns("id").Index).Value)
        EliminarFamilias(False, idAlmacen)
        For fila As Integer = 0 To spFamilias.ActiveSheet.Rows.Count - 1
            Dim id As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spFamilias.ActiveSheet.Cells(fila, spFamilias.ActiveSheet.Columns("id").Index).Text)
            Dim nombre As String = spFamilias.ActiveSheet.Cells(fila, spFamilias.ActiveSheet.Columns("nombre").Index).Text
            If (idAlmacen > 0 AndAlso id > 0 AndAlso Not String.IsNullOrEmpty(nombre)) Then
                familias.EIdAlmacen = idAlmacen
                familias.EId = id
                familias.ENombre = nombre
                familias.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        CargarFamilias(idAlmacen)

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
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            CargarFamilias(idAlmacen)
        End If

    End Sub

    Private Sub SeleccionoSubFamilias()

        Me.Cursor = Cursors.WaitCursor
        Me.opcionSeleccionada = OpcionMenu.subFamilias
        ReiniciarValoresIndices()
        OcultarSpreads()
        CargarSubFamilias(Me.filaAlmacen, Me.filaFamilia)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CargarSubFamilias(ByVal idAlmacen As Integer, ByVal idFamilia As Integer)

        If (idAlmacen < 0) Then
            CargarAlmacenes()
        End If
        If (idFamilia < 0) Then
            CargarFamilias(idAlmacen)
        End If
        If (idAlmacen >= 0 And idFamilia >= 0) Then
            If (Me.opcionSeleccionada = OpcionMenu.subFamilias) Then
                spSubFamilias.Height = Me.altoTotal
                spSubFamilias.Width = Me.anchoTercio
                spSubFamilias.Top = Me.arriba
                spSubFamilias.Left = Me.anchoTercio * 2
            ElseIf (Me.opcionSeleccionada = OpcionMenu.articulos) Then
                spSubFamilias.Height = Me.altoCuarto
                spSubFamilias.Width = Me.anchoTercio
                spSubFamilias.Top = Me.arriba
                spSubFamilias.Left = Me.anchoTercio * 2
            End If
            subFamilias.EIdAlmacen = idAlmacen
            subFamilias.EIdFamilia = idFamilia
            subFamilias.EId = 0
            spSubFamilias.ActiveSheet.DataSource = subFamilias.ObtenerListadoReporte()
            FormatearSpreadSubFamilias()
        End If

    End Sub

    Private Sub FormatearSpreadSubFamilias()

        spSubFamilias.ActiveSheet.ColumnHeader.RowCount = 2
        spSubFamilias.ActiveSheet.ColumnHeader.Rows(0, spSubFamilias.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spSubFamilias.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spSubFamilias.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        If (Me.opcionSeleccionada = OpcionMenu.subFamilias) Then
            spSubFamilias.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        Else
            spSubFamilias.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        End If
        If (Me.opcionSeleccionada = OpcionMenu.subFamilias) Then
            ControlarSpreadEnterASiguienteColumna(spSubFamilias)
        End If
        Dim numeracion As Integer = 0
        spSubFamilias.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spSubFamilias.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spSubFamilias.ActiveSheet.Columns("id").Width = 50
        spSubFamilias.ActiveSheet.Columns("nombre").Width = 400
        spSubFamilias.ActiveSheet.Columns("id").CellType = tipoEntero
        spSubFamilias.ActiveSheet.Columns("nombre").CellType = tipoTexto
        spSubFamilias.ActiveSheet.AddColumnHeaderSpanCell(0, 0, 1, spSubFamilias.ActiveSheet.Columns.Count)
        Dim obligatorio As String = String.Empty
        If (Me.opcionSeleccionada = OpcionMenu.subFamilias) Then
            obligatorio = " *"
        End If
        spSubFamilias.ActiveSheet.ColumnHeader.Cells(0, 0).Value = "S  u  b  F  a  m  i  l  i  a  s".ToUpper()
        spSubFamilias.ActiveSheet.ColumnHeader.Cells(1, spSubFamilias.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper() & obligatorio
        spSubFamilias.ActiveSheet.ColumnHeader.Cells(1, spSubFamilias.ActiveSheet.Columns("nombre").Index).Value = "Nombre".ToUpper() & obligatorio
        If (Me.opcionSeleccionada = OpcionMenu.subFamilias) Then
            spSubFamilias.ActiveSheet.Rows.Count += 1
        End If
        Application.DoEvents()

    End Sub

    Private Sub GuardarEditarSubFamilias()

        Dim idAlmacen As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spAlmacenes.ActiveSheet.Cells(spAlmacenes.ActiveSheet.ActiveRowIndex, spAlmacenes.ActiveSheet.Columns("id").Index).Value)
        Dim idFamilia As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spFamilias.ActiveSheet.Cells(spFamilias.ActiveSheet.ActiveRowIndex, spFamilias.ActiveSheet.Columns("id").Index).Value)
        EliminarSubFamilias(False, idAlmacen, idFamilia)
        For fila As Integer = 0 To spSubFamilias.ActiveSheet.Rows.Count - 1
            Dim id As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spSubFamilias.ActiveSheet.Cells(fila, spSubFamilias.ActiveSheet.Columns("id").Index).Text)
            Dim nombre As String = spSubFamilias.ActiveSheet.Cells(fila, spSubFamilias.ActiveSheet.Columns("nombre").Index).Text
            If (idAlmacen > 0 AndAlso idFamilia > 0 AndAlso id > 0 AndAlso Not String.IsNullOrEmpty(nombre)) Then
                subFamilias.EIdAlmacen = idAlmacen
                subFamilias.EIdFamilia = idFamilia
                subFamilias.EId = id
                subFamilias.ENombre = nombre
                subFamilias.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        CargarSubFamilias(idAlmacen, idFamilia)

    End Sub

    Private Sub EliminarSubFamilias(ByVal conMensaje As Boolean, ByVal idAlmacen As Integer, ByVal idFamilia As Integer)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            subFamilias.EIdAlmacen = idAlmacen
            subFamilias.EIdFamilia = idFamilia
            subFamilias.EId = 0
            subFamilias.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            CargarSubFamilias(idAlmacen, idFamilia)
        End If

    End Sub

    Private Sub SeleccionoArticulos()

        Me.Cursor = Cursors.WaitCursor
        Me.opcionSeleccionada = OpcionMenu.articulos
        ReiniciarValoresIndices()
        OcultarSpreads()
        CargarArticulos(Me.filaAlmacen, Me.filaFamilia, Me.filaSubFamilia)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CargarArticulos(ByVal idAlmacen As Integer, ByVal idFamilia As Integer, ByVal idSubFamilia As Integer)

        If (idAlmacen < 0) Then
            CargarAlmacenes()
        End If
        If (idFamilia < 0) Then
            CargarFamilias(idAlmacen)
        End If
        If (idSubFamilia < 0) Then
            CargarSubFamilias(idAlmacen, idFamilia)
        End If
        If (idAlmacen >= 0 And idFamilia >= 0 And idSubFamilia >= 0) Then
            spArticulos.Height = Me.altoCuarto * 3
            spArticulos.Width = Me.anchoTotal
            spArticulos.Top = Me.altoCuarto + pnlMenu.Height
            spArticulos.Left = Me.izquierda
            articulos.EIdAlmacen = idAlmacen
            articulos.EIdFamilia = idFamilia
            articulos.EIdSubFamilia = idSubFamilia
            articulos.EId = 0
            spArticulos.ActiveSheet.DataSource = articulos.ObtenerListadoReporte()
            FormatearSpreadArticulos()
        End If

    End Sub

    Private Sub FormatearSpreadArticulos()

        spArticulos.ActiveSheet.ColumnHeader.RowCount = 2
        spArticulos.ActiveSheet.ColumnHeader.Rows(0, spArticulos.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spArticulos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spArticulos.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosGrandesSpread
        If (Me.opcionSeleccionada = OpcionMenu.articulos) Then
            spArticulos.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        Else
            spArticulos.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        End If
        If (Me.opcionSeleccionada = OpcionMenu.articulos) Then
            ControlarSpreadEnterASiguienteColumna(spArticulos)
        End If
        Dim numeracion As Integer = 0
        spArticulos.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spArticulos.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spArticulos.ActiveSheet.Columns(numeracion).Tag = "nombreComercial" : numeracion += 1
        spArticulos.ActiveSheet.Columns(numeracion).Tag = "idUnidadMedida" : numeracion += 1
        spArticulos.ActiveSheet.Columns(numeracion).Tag = "nombreUnidadMedida" : numeracion += 1
        spArticulos.ActiveSheet.Columns(numeracion).Tag = "cantidadMinima" : numeracion += 1
        spArticulos.ActiveSheet.Columns(numeracion).Tag = "cantidadMaxima" : numeracion += 1
        spArticulos.ActiveSheet.Columns(numeracion).Tag = "precio" : numeracion += 1
        spArticulos.ActiveSheet.Columns(numeracion).Tag = "seccion" : numeracion += 1
        spArticulos.ActiveSheet.Columns(numeracion).Tag = "estante" : numeracion += 1
        spArticulos.ActiveSheet.Columns(numeracion).Tag = "nivel" : numeracion += 1
        spArticulos.ActiveSheet.Columns("id").Width = 50
        spArticulos.ActiveSheet.Columns("nombre").Width = 400
        spArticulos.ActiveSheet.Columns("nombreComercial").Width = 250
        spArticulos.ActiveSheet.Columns("idUnidadMedida").Width = 50
        spArticulos.ActiveSheet.Columns("nombreUnidadMedida").Width = 200
        spArticulos.ActiveSheet.Columns("cantidadMinima").Width = 120
        spArticulos.ActiveSheet.Columns("cantidadMaxima").Width = 120
        spArticulos.ActiveSheet.Columns("precio").Width = 120
        spArticulos.ActiveSheet.Columns("seccion").Width = 120
        spArticulos.ActiveSheet.Columns("estante").Width = 120
        spArticulos.ActiveSheet.Columns("nivel").Width = 120
        spArticulos.ActiveSheet.Columns("id").CellType = tipoEntero
        spArticulos.ActiveSheet.Columns("nombre").CellType = tipoTexto
        spArticulos.ActiveSheet.Columns("nombreComercial").CellType = tipoTexto
        spArticulos.ActiveSheet.Columns("idUnidadMedida").CellType = tipoEntero
        spArticulos.ActiveSheet.Columns("nombreUnidadMedida").CellType = tipoTexto
        spArticulos.ActiveSheet.Columns("cantidadMinima").CellType = tipoEntero
        spArticulos.ActiveSheet.Columns("cantidadMaxima").CellType = tipoEntero
        spArticulos.ActiveSheet.Columns("precio").CellType = tipoDoble
        spArticulos.ActiveSheet.Columns("seccion").CellType = tipoTexto
        spArticulos.ActiveSheet.Columns("estante").CellType = tipoTexto
        spArticulos.ActiveSheet.Columns("nivel").CellType = tipoTexto
        spArticulos.ActiveSheet.AddColumnHeaderSpanCell(0, 0, 1, spArticulos.ActiveSheet.Columns.Count)
        spArticulos.ActiveSheet.ColumnHeader.Cells(0, 0).Value = "A  r  t  í  c  u  l  o  s".ToUpper()
        spArticulos.ActiveSheet.ColumnHeader.Cells(1, spArticulos.ActiveSheet.Columns("id").Index).Value = "No. *".ToUpper()
        spArticulos.ActiveSheet.ColumnHeader.Cells(1, spArticulos.ActiveSheet.Columns("nombre").Index).Value = "Nombre *".ToUpper()
        spArticulos.ActiveSheet.ColumnHeader.Cells(1, spArticulos.ActiveSheet.Columns("nombreComercial").Index).Value = "Nombre Comercial".ToUpper()
        spArticulos.ActiveSheet.ColumnHeader.Cells(1, spArticulos.ActiveSheet.Columns("idUnidadMedida").Index).Value = "No. *".ToUpper()
        spArticulos.ActiveSheet.ColumnHeader.Cells(1, spArticulos.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = "Nombre Unidad Medida *".ToUpper()
        spArticulos.ActiveSheet.ColumnHeader.Cells(1, spArticulos.ActiveSheet.Columns("cantidadMinima").Index).Value = "Cantidad Mínima".ToUpper()
        spArticulos.ActiveSheet.ColumnHeader.Cells(1, spArticulos.ActiveSheet.Columns("cantidadMaxima").Index).Value = "Cantidad Máxima".ToUpper()
        spArticulos.ActiveSheet.ColumnHeader.Cells(1, spArticulos.ActiveSheet.Columns("precio").Index).Value = "Precio *".ToUpper()
        spArticulos.ActiveSheet.ColumnHeader.Cells(1, spArticulos.ActiveSheet.Columns("seccion").Index).Value = "Sección".ToUpper()
        spArticulos.ActiveSheet.ColumnHeader.Cells(1, spArticulos.ActiveSheet.Columns("estante").Index).Value = "Estante".ToUpper()
        spArticulos.ActiveSheet.ColumnHeader.Cells(1, spArticulos.ActiveSheet.Columns("nivel").Index).Value = "Nivel".ToUpper()
        If (Me.opcionSeleccionada = OpcionMenu.articulos) Then
            spArticulos.ActiveSheet.Rows.Count += 1
        End If
        Application.DoEvents()

    End Sub

    Private Sub GuardarEditarArticulos()

        Dim idAlmacen As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spAlmacenes.ActiveSheet.Cells(spAlmacenes.ActiveSheet.ActiveRowIndex, spAlmacenes.ActiveSheet.Columns("id").Index).Value)
        Dim idFamilia As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spFamilias.ActiveSheet.Cells(spFamilias.ActiveSheet.ActiveRowIndex, spFamilias.ActiveSheet.Columns("id").Index).Value)
        Dim idSubFamilia As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spSubFamilias.ActiveSheet.Cells(spSubFamilias.ActiveSheet.ActiveRowIndex, spSubFamilias.ActiveSheet.Columns("id").Index).Value)
        EliminarArticulos(False, idAlmacen, idFamilia, idSubFamilia)
        For fila As Integer = 0 To spArticulos.ActiveSheet.Rows.Count - 1
            Dim id As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spArticulos.ActiveSheet.Cells(fila, spArticulos.ActiveSheet.Columns("id").Index).Text)
            Dim nombre As String = spArticulos.ActiveSheet.Cells(fila, spArticulos.ActiveSheet.Columns("nombre").Index).Text
            Dim nombreComercial As String = spArticulos.ActiveSheet.Cells(fila, spArticulos.ActiveSheet.Columns("nombreComercial").Index).Text
            Dim idUnidadMedida As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spArticulos.ActiveSheet.Cells(fila, spArticulos.ActiveSheet.Columns("idUnidadMedida").Index).Text)
            Dim cantidadMinima As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spArticulos.ActiveSheet.Cells(fila, spArticulos.ActiveSheet.Columns("cantidadMinima").Index).Text)
            Dim cantidadMaxima As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spArticulos.ActiveSheet.Cells(fila, spArticulos.ActiveSheet.Columns("cantidadMaxima").Index).Text)
            Dim precio As Double = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spArticulos.ActiveSheet.Cells(fila, spArticulos.ActiveSheet.Columns("precio").Index).Text)
            Dim seccion As String = spArticulos.ActiveSheet.Cells(fila, spArticulos.ActiveSheet.Columns("seccion").Index).Text
            Dim estante As String = spArticulos.ActiveSheet.Cells(fila, spArticulos.ActiveSheet.Columns("estante").Index).Text
            Dim nivel As String = spArticulos.ActiveSheet.Cells(fila, spArticulos.ActiveSheet.Columns("nivel").Index).Text
            If (idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso id > 0 AndAlso Not String.IsNullOrEmpty(nombre) AndAlso idUnidadMedida > 0 AndAlso precio >= 0) Then
                articulos.EIdAlmacen = idAlmacen
                articulos.EIdFamilia = idFamilia
                articulos.EIdSubFamilia = idSubFamilia
                articulos.EId = id
                articulos.ENombre = nombre
                articulos.ENombreComercial = nombreComercial
                articulos.EIdUnidadMedida = idUnidadMedida
                articulos.ECantidadMinima = cantidadMinima
                articulos.ECantidadMaxima = cantidadMaxima
                articulos.EPrecio = precio
                articulos.ESeccion = seccion
                articulos.EEstante = estante
                articulos.ENivel = nivel
                articulos.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        CargarArticulos(idAlmacen, idFamilia, idSubFamilia)

    End Sub

    Private Sub EliminarArticulos(ByVal conMensaje As Boolean, ByVal idAlmacen As Integer, ByVal idFamilia As Integer, ByVal idSubFamilia As Integer)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            articulos.EIdAlmacen = idAlmacen
            articulos.EIdFamilia = idFamilia
            articulos.EIdSubFamilia = idSubFamilia
            articulos.EId = 0
            articulos.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            CargarArticulos(idAlmacen, idFamilia, idSubFamilia)
        End If

    End Sub

    Private Sub ReiniciarValoresIndices()

        Me.filaAlmacen = -1 : Me.filaFamilia = -1 : Me.filaSubFamilia = -1

    End Sub

    Private Sub SeleccionoProveedores()

        Me.Cursor = Cursors.WaitCursor
        Me.opcionSeleccionada = OpcionMenu.proveedores
        OcultarSpreads()
        LimpiarSpread(spVarios)
        CargarProveedores()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CargarProveedores()

        spVarios.Height = Me.altoTotal
        spVarios.Width = Me.anchoTotal
        spVarios.Top = Me.arriba
        spVarios.Left = Me.izquierda
        spVarios.Show()
        proveedores.EId = 0
        spVarios.ActiveSheet.DataSource = proveedores.ObtenerListadoReporte()
        FormatearSpreadProveedores()

    End Sub

    Private Sub FormatearSpreadProveedores()

        spVarios.ActiveSheet.ColumnHeader.RowCount = 2
        spVarios.ActiveSheet.ColumnHeader.Rows(0, spVarios.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spVarios.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spVarios.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spVarios.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        ControlarSpreadEnterASiguienteColumna(spVarios)
        Dim numeracion As Integer = 0
        spVarios.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "rfc" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "domicilio" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "municipio" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "estado" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "telefono" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "correo" : numeracion += 1
        spVarios.ActiveSheet.Columns("id").Width = 50
        spVarios.ActiveSheet.Columns("nombre").Width = 400
        spVarios.ActiveSheet.Columns("rfc").Width = 120
        spVarios.ActiveSheet.Columns("domicilio").Width = 300
        spVarios.ActiveSheet.Columns("municipio").Width = 200
        spVarios.ActiveSheet.Columns("estado").Width = 180
        spVarios.ActiveSheet.Columns("telefono").Width = 120
        spVarios.ActiveSheet.Columns("correo").Width = 150
        spVarios.ActiveSheet.Columns("id").CellType = tipoEntero
        spVarios.ActiveSheet.Columns("nombre").CellType = tipoTexto
        spVarios.ActiveSheet.Columns("rfc").CellType = tipoTexto
        spVarios.ActiveSheet.Columns("domicilio").CellType = tipoTexto
        spVarios.ActiveSheet.Columns("municipio").CellType = tipoTexto
        spVarios.ActiveSheet.Columns("estado").CellType = tipoTexto
        spVarios.ActiveSheet.Columns("telefono").CellType = tipoTexto
        spVarios.ActiveSheet.Columns("correo").CellType = tipoTexto
        spVarios.ActiveSheet.AddColumnHeaderSpanCell(0, 0, 1, spVarios.ActiveSheet.Columns.Count)
        spVarios.ActiveSheet.ColumnHeader.Cells(0, 0).Value = "P  r  o  v  e  d  o  r  e  s".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("id").Index).Value = "No. *".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("nombre").Index).Value = "Nombre *".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("rfc").Index).Value = "Rfc".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("domicilio").Index).Value = "Domicilio".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("municipio").Index).Value = "Municipio".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("estado").Index).Value = "Estado".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("telefono").Index).Value = "Teléfono".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("correo").Index).Value = "Correo".ToUpper()
        spVarios.ActiveSheet.Rows.Count += 1
        Application.DoEvents()

    End Sub

    Private Sub GuardarEditarProveedores()

        EliminarProveedores(False)
        For fila As Integer = 0 To spVarios.ActiveSheet.Rows.Count - 1
            Dim id As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("id").Index).Text)
            Dim nombre As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("nombre").Index).Text
            Dim rfc As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("rfc").Index).Text
            Dim domicilio As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("domicilio").Index).Text
            Dim municipio As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("municipio").Index).Text
            Dim estado As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("estado").Index).Text
            Dim telefono As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("telefono").Index).Text
            Dim correo As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("correo").Index).Text
            If (id > 0 AndAlso Not String.IsNullOrEmpty(nombre)) Then
                proveedores.EId = id
                proveedores.ENombre = nombre
                proveedores.Erfc = rfc
                proveedores.EDomicilio = domicilio
                proveedores.EMunicipio = municipio
                proveedores.EEstado = estado
                proveedores.ETelefono = telefono
                proveedores.ECorreo = correo
                proveedores.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        CargarProveedores()

    End Sub

    Private Sub EliminarProveedores(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            proveedores.EId = 0
            proveedores.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            CargarProveedores()
        End If

    End Sub

    Private Sub SeleccionoMonedas()

        Me.Cursor = Cursors.WaitCursor
        Me.opcionSeleccionada = OpcionMenu.monedas
        OcultarSpreads()
        LimpiarSpread(spVarios)
        CargarMonedas()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CargarMonedas()

        spVarios.Height = Me.altoTotal
        spVarios.Width = Me.anchoTotal
        spVarios.Top = Me.arriba
        spVarios.Left = Me.izquierda
        spVarios.Show()
        monedas.EId = 0
        spVarios.ActiveSheet.DataSource = monedas.ObtenerListadoReporte()
        FormatearSpreadMonedas()

    End Sub

    Private Sub FormatearSpreadMonedas()

        spVarios.ActiveSheet.ColumnHeader.RowCount = 2
        spVarios.ActiveSheet.ColumnHeader.Rows(0, spVarios.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spVarios.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spVarios.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spVarios.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        ControlarSpreadEnterASiguienteColumna(spVarios)
        Dim numeracion As Integer = 0
        spVarios.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spVarios.ActiveSheet.Columns("id").Width = 50
        spVarios.ActiveSheet.Columns("nombre").Width = 400
        spVarios.ActiveSheet.Columns("id").CellType = tipoEntero
        spVarios.ActiveSheet.Columns("nombre").CellType = tipoTexto
        spVarios.ActiveSheet.AddColumnHeaderSpanCell(0, 0, 1, spVarios.ActiveSheet.Columns.Count)
        spVarios.ActiveSheet.ColumnHeader.Cells(0, 0).Value = "M  o  n  e  d  a  s".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("id").Index).Value = "No. *".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("nombre").Index).Value = "Nombre *".ToUpper()
        spVarios.ActiveSheet.Rows.Count += 1
        Application.DoEvents()

    End Sub

    Private Sub GuardarEditarMonedas()

        EliminarMonedas(False)
        For fila As Integer = 0 To spVarios.ActiveSheet.Rows.Count - 1
            Dim id As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("id").Index).Text)
            Dim nombre As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("nombre").Index).Text
            If (id > 0 AndAlso Not String.IsNullOrEmpty(nombre)) Then
                monedas.EId = id
                monedas.ENombre = nombre
                monedas.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        CargarMonedas()

    End Sub

    Private Sub EliminarMonedas(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            monedas.EId = 0
            monedas.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            CargarMonedas()
        End If

    End Sub

    Private Sub SeleccionoTiposEntradas()

        Me.Cursor = Cursors.WaitCursor
        Me.opcionSeleccionada = OpcionMenu.tiposEntradas
        OcultarSpreads()
        LimpiarSpread(spVarios)
        CargarTiposEntradas()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CargarTiposEntradas()

        spVarios.Height = Me.altoTotal
        spVarios.Width = Me.anchoTotal
        spVarios.Top = Me.arriba
        spVarios.Left = Me.izquierda
        spVarios.Show()
        tiposEntradas.EId = 0
        spVarios.ActiveSheet.DataSource = tiposEntradas.ObtenerListadoReporte()
        FormatearSpreadTiposEntradas()

    End Sub

    Private Sub FormatearSpreadTiposEntradas()

        spVarios.ActiveSheet.ColumnHeader.RowCount = 2
        spVarios.ActiveSheet.ColumnHeader.Rows(0, spVarios.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spVarios.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spVarios.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spVarios.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        ControlarSpreadEnterASiguienteColumna(spVarios)
        Dim numeracion As Integer = 0
        spVarios.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spVarios.ActiveSheet.Columns("id").Width = 50
        spVarios.ActiveSheet.Columns("nombre").Width = 400
        spVarios.ActiveSheet.Columns("id").CellType = tipoEntero
        spVarios.ActiveSheet.Columns("nombre").CellType = tipoTexto
        spVarios.ActiveSheet.AddColumnHeaderSpanCell(0, 0, 1, spVarios.ActiveSheet.Columns.Count)
        spVarios.ActiveSheet.ColumnHeader.Cells(0, 0).Value = "T  i  p  o  s      d  e      E  n  t  r  a  d  a  s".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("id").Index).Value = "No. *".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("nombre").Index).Value = "Nombre *".ToUpper()
        spVarios.ActiveSheet.Rows.Count += 1
        Application.DoEvents()

    End Sub

    Private Sub GuardarEditarTiposEntradas()

        EliminarTiposEntradas(False)
        For fila As Integer = 0 To spVarios.ActiveSheet.Rows.Count - 1
            Dim id As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("id").Index).Text)
            Dim nombre As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("nombre").Index).Text
            If (id > 0 AndAlso Not String.IsNullOrEmpty(nombre)) Then
                tiposEntradas.EId = id
                tiposEntradas.ENombre = nombre
                tiposEntradas.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        CargarTiposEntradas()

    End Sub

    Private Sub EliminarTiposEntradas(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            tiposEntradas.EId = 0
            tiposEntradas.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            CargarTiposEntradas()
        End If

    End Sub

    Private Sub SeleccionoTiposSalidas()

        Me.Cursor = Cursors.WaitCursor
        Me.opcionSeleccionada = OpcionMenu.tiposSalidas
        OcultarSpreads()
        LimpiarSpread(spVarios)
        CargarTiposSalidas()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CargarTiposSalidas()

        spVarios.Height = Me.altoTotal
        spVarios.Width = Me.anchoTotal
        spVarios.Top = Me.arriba
        spVarios.Left = Me.izquierda
        spVarios.Show()
        tiposSalidas.EId = 0
        spVarios.ActiveSheet.DataSource = tiposSalidas.ObtenerListadoReporte()
        FormatearSpreadTiposSalidas()

    End Sub

    Private Sub FormatearSpreadTiposSalidas()

        spVarios.ActiveSheet.ColumnHeader.RowCount = 2
        spVarios.ActiveSheet.ColumnHeader.Rows(0, spVarios.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spVarios.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spVarios.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spVarios.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        ControlarSpreadEnterASiguienteColumna(spVarios)
        Dim numeracion As Integer = 0
        spVarios.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spVarios.ActiveSheet.Columns("id").Width = 50
        spVarios.ActiveSheet.Columns("nombre").Width = 400
        spVarios.ActiveSheet.Columns("id").CellType = tipoEntero
        spVarios.ActiveSheet.Columns("nombre").CellType = tipoTexto
        spVarios.ActiveSheet.AddColumnHeaderSpanCell(0, 0, 1, spVarios.ActiveSheet.Columns.Count)
        spVarios.ActiveSheet.ColumnHeader.Cells(0, 0).Value = "T  i  p  o  s      d  e      S  a  l  i  d  a  s".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("id").Index).Value = "No. *".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("nombre").Index).Value = "Nombre *".ToUpper()
        spVarios.ActiveSheet.Rows.Count += 1
        Application.DoEvents()

    End Sub

    Private Sub GuardarEditarTiposSalidas()

        EliminarTiposSalidas(False)
        For fila As Integer = 0 To spVarios.ActiveSheet.Rows.Count - 1
            Dim id As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("id").Index).Text)
            Dim nombre As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("nombre").Index).Text
            If (id > 0 AndAlso Not String.IsNullOrEmpty(nombre)) Then
                tiposSalidas.EId = id
                tiposSalidas.ENombre = nombre
                tiposSalidas.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        CargarTiposSalidas()

    End Sub

    Private Sub EliminarTiposSalidas(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            tiposSalidas.EId = 0
            tiposSalidas.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            CargarTiposSalidas()
        End If

    End Sub

    Private Sub SeleccionoUnidadesMedidas()

        Me.Cursor = Cursors.WaitCursor
        Me.opcionSeleccionada = OpcionMenu.unidadesMedidas
        OcultarSpreads()
        LimpiarSpread(spVarios)
        CargarUnidadesMedidas()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CargarUnidadesMedidas()

        spVarios.Height = Me.altoTotal
        spVarios.Width = Me.anchoTotal
        spVarios.Top = Me.arriba
        spVarios.Left = Me.izquierda
        spVarios.Show()
        unidadesMedidas.EId = 0
        spVarios.ActiveSheet.DataSource = unidadesMedidas.ObtenerListado()
        FormatearSpreadUnidadesMedidas()

    End Sub

    Private Sub FormatearSpreadUnidadesMedidas()

        spVarios.ActiveSheet.ColumnHeader.RowCount = 2
        spVarios.ActiveSheet.ColumnHeader.Rows(0, spVarios.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spVarios.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spVarios.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spVarios.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        ControlarSpreadEnterASiguienteColumna(spVarios)
        Dim numeracion As Integer = 0
        spVarios.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spVarios.ActiveSheet.Columns("id").Width = 50
        spVarios.ActiveSheet.Columns("nombre").Width = 400
        spVarios.ActiveSheet.Columns("id").CellType = tipoEntero
        spVarios.ActiveSheet.Columns("nombre").CellType = tipoTexto
        spVarios.ActiveSheet.AddColumnHeaderSpanCell(0, 0, 1, spVarios.ActiveSheet.Columns.Count)
        spVarios.ActiveSheet.ColumnHeader.Cells(0, 0).Value = "U  n  i  d  a  d  e  s      d  e      M  e  d  i  d  a  s ".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("id").Index).Value = "No. *".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("nombre").Index).Value = "Nombre *".ToUpper()
        spVarios.ActiveSheet.Rows.Count += 1
        Application.DoEvents()

    End Sub

    Private Sub GuardarEditarUnidadesMedidas()

        EliminarUnidadesMedidas(False)
        For fila As Integer = 0 To spVarios.ActiveSheet.Rows.Count - 1
            Dim id As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("id").Index).Text)
            Dim nombre As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("nombre").Index).Text
            If (id > 0 AndAlso Not String.IsNullOrEmpty(nombre)) Then
                unidadesMedidas.EId = id
                unidadesMedidas.ENombre = nombre
                unidadesMedidas.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        CargarUnidadesMedidas()

    End Sub

    Private Sub EliminarUnidadesMedidas(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            unidadesMedidas.EId = 0
            unidadesMedidas.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            CargarUnidadesMedidas()
        End If

    End Sub

    Private Sub SeleccionoTiposCambios()

        Me.Cursor = Cursors.WaitCursor
        Me.opcionSeleccionada = OpcionMenu.tiposCambios
        OcultarSpreads()
        LimpiarSpread(spVarios)
        CargarTiposCambios()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CargarTiposCambios()

        spVarios.Height = Me.altoTotal
        spVarios.Width = Me.anchoTotal
        spVarios.Top = Me.arriba
        spVarios.Left = Me.izquierda
        spVarios.Show()
        tiposCambios.EIdMoneda = 0
        'tiposCambios.EFecha = Convert.ToDateTime("01/01/1899")
        spVarios.ActiveSheet.DataSource = tiposCambios.ObtenerListadoReporte()
        FormatearSpreadTiposCambios()

    End Sub

    Private Sub FormatearSpreadTiposCambios()

        spVarios.ActiveSheet.ColumnHeader.RowCount = 2
        spVarios.ActiveSheet.ColumnHeader.Rows(0, spVarios.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spVarios.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spVarios.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spVarios.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        ControlarSpreadEnterASiguienteColumna(spVarios)
        Dim numeracion As Integer = 0
        spVarios.ActiveSheet.Columns(numeracion).Tag = "idMoneda" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "nombreMoneda" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "fecha" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "valor" : numeracion += 1
        spVarios.ActiveSheet.Columns("idMoneda").Width = 50
        spVarios.ActiveSheet.Columns("nombreMoneda").Width = 300
        spVarios.ActiveSheet.Columns("fecha").Width = 100
        spVarios.ActiveSheet.Columns("valor").Width = 100
        spVarios.ActiveSheet.Columns("idMoneda").CellType = tipoEntero
        spVarios.ActiveSheet.Columns("nombreMoneda").CellType = tipoTexto
        spVarios.ActiveSheet.Columns("fecha").CellType = tipoFecha
        spVarios.ActiveSheet.Columns("valor").CellType = tipoDoble
        spVarios.ActiveSheet.AddColumnHeaderSpanCell(0, 0, 1, spVarios.ActiveSheet.Columns.Count)
        spVarios.ActiveSheet.ColumnHeader.Cells(0, 0).Value = "T  i  p  o  s      d  e      C  a  m  b  i  o  s".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("idMoneda").Index).Value = "No. *".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("nombreMoneda").Index).Value = "Nombre Moneda *".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("fecha").Index).Value = "Fecha *".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("valor").Index).Value = "Valor *".ToUpper()
        spVarios.ActiveSheet.Rows.Count += 1
        Application.DoEvents()

    End Sub

    Private Sub GuardarEditarTiposCambios()

        Dim fechaActual As Date = Today
        EliminarTiposCambios(False, fechaActual)
        For fila As Integer = 0 To spVarios.ActiveSheet.Rows.Count - 1
            Dim idMoneda As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("idMoneda").Index).Text)
            Dim fecha As Date = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("fecha").Index).Value
            Dim valor As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("valor").Index).Text
            If (idMoneda > 0 AndAlso Not String.IsNullOrEmpty(fecha) AndAlso fecha = fechaActual AndAlso valor > 0) Then
                tiposCambios.EIdMoneda = idMoneda
                tiposCambios.EFecha = fecha
                tiposCambios.EValor = valor
                tiposCambios.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado, solo de la fecha actual.", "Finalizado.", MessageBoxButtons.OK)
        CargarTiposCambios()

    End Sub

    Private Sub EliminarTiposCambios(ByVal conMensaje As Boolean, ByVal fecha As Date)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar todo lo de la fecha actual?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            tiposCambios.EIdMoneda = 0
            tiposCambios.EFecha = fecha
            tiposCambios.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado, solo de la fecha actual.", "Finalizado.", MessageBoxButtons.OK)
            CargarTiposCambios()
        End If

    End Sub

    Private Sub SeleccionoClientes()

        Me.Cursor = Cursors.WaitCursor
        Me.opcionSeleccionada = OpcionMenu.clientes
        OcultarSpreads()
        LimpiarSpread(spVarios)
        CargarClientes()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CargarClientes()

        spVarios.Height = Me.altoTotal
        spVarios.Width = Me.anchoTotal
        spVarios.Top = Me.arriba
        spVarios.Left = Me.izquierda
        spVarios.Show()
        clientes.EId = 0
        spVarios.ActiveSheet.DataSource = clientes.ObtenerListadoReporte()
        FormatearSpreadClientes()

    End Sub

    Private Sub FormatearSpreadClientes()

        spVarios.ActiveSheet.ColumnHeader.RowCount = 2
        spVarios.ActiveSheet.ColumnHeader.Rows(0, spVarios.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spVarios.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spVarios.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spVarios.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        ControlarSpreadEnterASiguienteColumna(spVarios)
        Dim numeracion As Integer = 0
        spVarios.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "rfc" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "domicilio" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "municipio" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "estado" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "telefono" : numeracion += 1
        spVarios.ActiveSheet.Columns(numeracion).Tag = "correo" : numeracion += 1
        spVarios.ActiveSheet.Columns("id").Width = 50
        spVarios.ActiveSheet.Columns("nombre").Width = 400
        spVarios.ActiveSheet.Columns("rfc").Width = 120
        spVarios.ActiveSheet.Columns("domicilio").Width = 300
        spVarios.ActiveSheet.Columns("municipio").Width = 200
        spVarios.ActiveSheet.Columns("estado").Width = 180
        spVarios.ActiveSheet.Columns("telefono").Width = 120
        spVarios.ActiveSheet.Columns("correo").Width = 150
        spVarios.ActiveSheet.Columns("id").CellType = tipoEntero
        spVarios.ActiveSheet.Columns("nombre").CellType = tipoTexto
        spVarios.ActiveSheet.Columns("rfc").CellType = tipoTexto
        spVarios.ActiveSheet.Columns("domicilio").CellType = tipoTexto
        spVarios.ActiveSheet.Columns("municipio").CellType = tipoTexto
        spVarios.ActiveSheet.Columns("estado").CellType = tipoTexto
        spVarios.ActiveSheet.Columns("telefono").CellType = tipoTexto
        spVarios.ActiveSheet.Columns("correo").CellType = tipoTexto
        spVarios.ActiveSheet.AddColumnHeaderSpanCell(0, 0, 1, spVarios.ActiveSheet.Columns.Count)
        spVarios.ActiveSheet.ColumnHeader.Cells(0, 0).Value = "C  l  i  e  n  t  e  s".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("id").Index).Value = "No. *".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("nombre").Index).Value = "Nombre *".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("rfc").Index).Value = "Rfc".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("domicilio").Index).Value = "Domicilio".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("municipio").Index).Value = "Municipio".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("estado").Index).Value = "Estado".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("telefono").Index).Value = "Teléfono".ToUpper()
        spVarios.ActiveSheet.ColumnHeader.Cells(1, spVarios.ActiveSheet.Columns("correo").Index).Value = "Correo".ToUpper()
        spVarios.ActiveSheet.Rows.Count += 1
        Application.DoEvents()

    End Sub

    Private Sub GuardarEditarClientes()

        EliminarClientes(False)
        For fila As Integer = 0 To spVarios.ActiveSheet.Rows.Count - 1
            Dim id As Integer = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("id").Index).Text)
            Dim nombre As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("nombre").Index).Text
            Dim rfc As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("rfc").Index).Text
            Dim domicilio As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("domicilio").Index).Text
            Dim municipio As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("municipio").Index).Text
            Dim estado As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("estado").Index).Text
            Dim telefono As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("telefono").Index).Text
            Dim correo As String = spVarios.ActiveSheet.Cells(fila, spVarios.ActiveSheet.Columns("correo").Index).Text
            If (id > 0 AndAlso Not String.IsNullOrEmpty(nombre)) Then
                clientes.EId = id
                clientes.ENombre = nombre
                clientes.Erfc = rfc
                clientes.EDomicilio = domicilio
                clientes.EMunicipio = municipio
                clientes.EEstado = estado
                clientes.ETelefono = telefono
                clientes.ECorreo = correo
                clientes.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        CargarClientes()

    End Sub

    Private Sub EliminarClientes(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar todo?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            clientes.EId = 0
            clientes.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            CargarClientes()
        End If

    End Sub

#End Region

#End Region

#Region "Enumeraciones"

    Public Enum OpcionMenu

        almacenes = 1
        familias = 2
        subFamilias = 3
        articulos = 4
        proveedores = 5
        clientes = 6
        monedas = 7
        tiposCambios = 8
        tiposEntradas = 9
        tiposSalidas = 10
        unidadesMedidas = 11

    End Enum

#End Region

End Class