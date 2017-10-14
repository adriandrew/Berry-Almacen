<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Principal
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim EnhancedScrollBarRenderer7 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim NamedStyle13 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("ColumnHeaderMidnight")
        Dim NamedStyle14 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("RowHeaderMidnight")
        Dim NamedStyle15 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("CornerMidnight")
        Dim EnhancedCornerRenderer3 As FarPoint.Win.Spread.CellType.EnhancedCornerRenderer = New FarPoint.Win.Spread.CellType.EnhancedCornerRenderer()
        Dim NamedStyle16 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("DataAreaMidnght")
        Dim GeneralCellType6 As FarPoint.Win.Spread.CellType.GeneralCellType = New FarPoint.Win.Spread.CellType.GeneralCellType()
        Dim NamedStyle17 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("Style1")
        Dim GeneralCellType7 As FarPoint.Win.Spread.CellType.GeneralCellType = New FarPoint.Win.Spread.CellType.GeneralCellType()
        Dim NamedStyle18 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("DataAreaGrayscale")
        Dim GeneralCellType8 As FarPoint.Win.Spread.CellType.GeneralCellType = New FarPoint.Win.Spread.CellType.GeneralCellType()
        Dim EnhancedScrollBarRenderer8 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer1 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim NamedStyle1 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("Style1")
        Dim NamedStyle2 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("DataAreaGrayscale")
        Dim GeneralCellType1 As FarPoint.Win.Spread.CellType.GeneralCellType = New FarPoint.Win.Spread.CellType.GeneralCellType()
        Dim NamedStyle3 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("ColumnHeaderMidnight")
        Dim NamedStyle4 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("RowHeaderMidnight")
        Dim NamedStyle5 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("CornerMidnight")
        Dim EnhancedCornerRenderer1 As FarPoint.Win.Spread.CellType.EnhancedCornerRenderer = New FarPoint.Win.Spread.CellType.EnhancedCornerRenderer()
        Dim NamedStyle6 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("DataAreaMidnght")
        Dim GeneralCellType2 As FarPoint.Win.Spread.CellType.GeneralCellType = New FarPoint.Win.Spread.CellType.GeneralCellType()
        Dim EnhancedScrollBarRenderer2 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer3 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer4 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Principal))
        Me.pnlContenido = New System.Windows.Forms.Panel()
        Me.pnlCuerpo = New System.Windows.Forms.Panel()
        Me.spListados = New FarPoint.Win.Spread.FpSpread()
        Me.spListados_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.pnlCapturaSuperior = New System.Windows.Forms.Panel()
        Me.cbOrigenes = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbProveedores = New System.Windows.Forms.ComboBox()
        Me.btnMostrarOcultar = New System.Windows.Forms.Button()
        Me.btnIdSiguiente = New System.Windows.Forms.Button()
        Me.btnIdAnterior = New System.Windows.Forms.Button()
        Me.cbAlmacenes = New System.Windows.Forms.ComboBox()
        Me.chkMantenerDatos = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTipoCambio = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cbMonedas = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbTiposEntradas = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtIdExterno = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlCatalogos = New System.Windows.Forms.Panel()
        Me.txtBuscarCatalogo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.spCatalogos = New FarPoint.Win.Spread.FpSpread()
        Me.spCatalogos_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.spEntradas = New FarPoint.Win.Spread.FpSpread()
        Me.spEntradas_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.pnlPie = New System.Windows.Forms.Panel()
        Me.btnListados = New System.Windows.Forms.Button()
        Me.btnAyuda = New System.Windows.Forms.Button()
        Me.lblDescripcionTooltip = New System.Windows.Forms.Label()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.pnlEncabezado = New System.Windows.Forms.Panel()
        Me.lblEncabezadoArea = New System.Windows.Forms.Label()
        Me.lblEncabezadoUsuario = New System.Windows.Forms.Label()
        Me.lblEncabezadoEmpresa = New System.Windows.Forms.Label()
        Me.lblEncabezadoPrograma = New System.Windows.Forms.Label()
        Me.temporizador = New System.Windows.Forms.Timer(Me.components)
        Me.pnlContenido.SuspendLayout()
        Me.pnlCuerpo.SuspendLayout()
        CType(Me.spListados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spListados_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCapturaSuperior.SuspendLayout()
        Me.pnlCatalogos.SuspendLayout()
        CType(Me.spCatalogos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spCatalogos_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spEntradas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spEntradas_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPie.SuspendLayout()
        Me.pnlEncabezado.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlContenido
        '
        Me.pnlContenido.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlContenido.BackColor = System.Drawing.Color.DarkSlateGray
        Me.pnlContenido.BackgroundImage = Global.ALMEntradas.My.Resources.Resources.Logo3
        Me.pnlContenido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pnlContenido.Controls.Add(Me.pnlCuerpo)
        Me.pnlContenido.Controls.Add(Me.pnlPie)
        Me.pnlContenido.Controls.Add(Me.pnlEncabezado)
        Me.pnlContenido.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlContenido.Location = New System.Drawing.Point(0, 0)
        Me.pnlContenido.Name = "pnlContenido"
        Me.pnlContenido.Size = New System.Drawing.Size(1034, 630)
        Me.pnlContenido.TabIndex = 2
        '
        'pnlCuerpo
        '
        Me.pnlCuerpo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlCuerpo.AutoScroll = True
        Me.pnlCuerpo.BackColor = System.Drawing.Color.Transparent
        Me.pnlCuerpo.Controls.Add(Me.spListados)
        Me.pnlCuerpo.Controls.Add(Me.pnlCapturaSuperior)
        Me.pnlCuerpo.Controls.Add(Me.pnlCatalogos)
        Me.pnlCuerpo.Controls.Add(Me.spEntradas)
        Me.pnlCuerpo.Location = New System.Drawing.Point(3, 77)
        Me.pnlCuerpo.Name = "pnlCuerpo"
        Me.pnlCuerpo.Size = New System.Drawing.Size(1028, 490)
        Me.pnlCuerpo.TabIndex = 9
        '
        'spListados
        '
        Me.spListados.AccessibleDescription = ""
        Me.spListados.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.spListados.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.spListados.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spListados.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer7.ArrowColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer7.ArrowHoveredColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer7.ArrowSelectedColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer7.ButtonBackgroundColor = System.Drawing.Color.DimGray
        EnhancedScrollBarRenderer7.ButtonBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer7.ButtonHoveredBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer7.ButtonHoveredBorderColor = System.Drawing.Color.Silver
        EnhancedScrollBarRenderer7.ButtonSelectedBackgroundColor = System.Drawing.Color.Silver
        EnhancedScrollBarRenderer7.ButtonSelectedBorderColor = System.Drawing.Color.DimGray
        EnhancedScrollBarRenderer7.TrackBarBackgroundColor = System.Drawing.Color.LightGray
        EnhancedScrollBarRenderer7.TrackBarSelectedBackgroundColor = System.Drawing.Color.DarkGray
        Me.spListados.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer7
        Me.spListados.HorizontalScrollBar.TabIndex = 14
        Me.spListados.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.spListados.Location = New System.Drawing.Point(658, 0)
        Me.spListados.Name = "spListados"
        NamedStyle13.BackColor = System.Drawing.Color.DarkSlateBlue
        NamedStyle13.ForeColor = System.Drawing.SystemColors.ControlText
        NamedStyle13.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle13.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle13.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle14.BackColor = System.Drawing.Color.DarkSlateBlue
        NamedStyle14.ForeColor = System.Drawing.SystemColors.ControlText
        NamedStyle14.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle14.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle14.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle15.BackColor = System.Drawing.Color.MidnightBlue
        NamedStyle15.ForeColor = System.Drawing.SystemColors.ControlText
        NamedStyle15.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle15.NoteIndicatorColor = System.Drawing.Color.Red
        EnhancedCornerRenderer3.ActiveBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedCornerRenderer3.GridLineColor = System.Drawing.Color.Empty
        EnhancedCornerRenderer3.NormalBackgroundColor = System.Drawing.Color.MidnightBlue
        NamedStyle15.Renderer = EnhancedCornerRenderer3
        NamedStyle15.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle16.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer))
        NamedStyle16.CellType = GeneralCellType6
        NamedStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        NamedStyle16.Locked = False
        NamedStyle16.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle16.Renderer = GeneralCellType6
        NamedStyle17.BackColor = System.Drawing.SystemColors.HotTrack
        NamedStyle17.CellType = GeneralCellType7
        NamedStyle17.ForeColor = System.Drawing.SystemColors.WindowText
        NamedStyle17.Locked = False
        NamedStyle17.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle17.Renderer = GeneralCellType7
        NamedStyle18.BackColor = System.Drawing.Color.Gainsboro
        NamedStyle18.CellType = GeneralCellType8
        NamedStyle18.ForeColor = System.Drawing.SystemColors.WindowText
        NamedStyle18.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle18.Renderer = GeneralCellType8
        Me.spListados.NamedStyles.AddRange(New FarPoint.Win.Spread.NamedStyle() {NamedStyle13, NamedStyle14, NamedStyle15, NamedStyle16, NamedStyle17, NamedStyle18})
        Me.spListados.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spListados_Sheet1})
        Me.spListados.Size = New System.Drawing.Size(260, 148)
        Me.spListados.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Grayscale
        Me.spListados.TabIndex = 25
        Me.spListados.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spListados.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer8.ArrowColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer8.ArrowHoveredColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer8.ArrowSelectedColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer8.ButtonBackgroundColor = System.Drawing.Color.DimGray
        EnhancedScrollBarRenderer8.ButtonBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer8.ButtonHoveredBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer8.ButtonHoveredBorderColor = System.Drawing.Color.Silver
        EnhancedScrollBarRenderer8.ButtonSelectedBackgroundColor = System.Drawing.Color.Silver
        EnhancedScrollBarRenderer8.ButtonSelectedBorderColor = System.Drawing.Color.DimGray
        EnhancedScrollBarRenderer8.TrackBarBackgroundColor = System.Drawing.Color.LightGray
        EnhancedScrollBarRenderer8.TrackBarSelectedBackgroundColor = System.Drawing.Color.DarkGray
        Me.spListados.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer8
        Me.spListados.VerticalScrollBar.TabIndex = 15
        Me.spListados.Visible = False
        '
        'spListados_Sheet1
        '
        Me.spListados_Sheet1.Reset()
        spListados_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spListados_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spListados_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spListados_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderGrayscale"
        Me.spListados_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spListados_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerGrayscale"
        Me.spListados_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spListados_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderGrayscale"
        Me.spListados_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spListados_Sheet1.DefaultStyle.Parent = "DataAreaGrayscale"
        Me.spListados_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spListados_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderGrayscale"
        Me.spListados_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spListados_Sheet1.SheetCornerStyle.Parent = "CornerGrayscale"
        Me.spListados_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'pnlCapturaSuperior
        '
        Me.pnlCapturaSuperior.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlCapturaSuperior.AutoScroll = True
        Me.pnlCapturaSuperior.BackColor = System.Drawing.Color.White
        Me.pnlCapturaSuperior.Controls.Add(Me.cbOrigenes)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label9)
        Me.pnlCapturaSuperior.Controls.Add(Me.cbProveedores)
        Me.pnlCapturaSuperior.Controls.Add(Me.btnMostrarOcultar)
        Me.pnlCapturaSuperior.Controls.Add(Me.btnIdSiguiente)
        Me.pnlCapturaSuperior.Controls.Add(Me.btnIdAnterior)
        Me.pnlCapturaSuperior.Controls.Add(Me.cbAlmacenes)
        Me.pnlCapturaSuperior.Controls.Add(Me.chkMantenerDatos)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label8)
        Me.pnlCapturaSuperior.Controls.Add(Me.txtTipoCambio)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label7)
        Me.pnlCapturaSuperior.Controls.Add(Me.cbMonedas)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label6)
        Me.pnlCapturaSuperior.Controls.Add(Me.cbTiposEntradas)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label5)
        Me.pnlCapturaSuperior.Controls.Add(Me.txtIdExterno)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label4)
        Me.pnlCapturaSuperior.Controls.Add(Me.txtId)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label3)
        Me.pnlCapturaSuperior.Controls.Add(Me.dtpFecha)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label2)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label1)
        Me.pnlCapturaSuperior.Location = New System.Drawing.Point(0, 0)
        Me.pnlCapturaSuperior.Name = "pnlCapturaSuperior"
        Me.pnlCapturaSuperior.Size = New System.Drawing.Size(392, 490)
        Me.pnlCapturaSuperior.TabIndex = 23
        '
        'cbOrigenes
        '
        Me.cbOrigenes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbOrigenes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbOrigenes.BackColor = System.Drawing.Color.White
        Me.cbOrigenes.Enabled = False
        Me.cbOrigenes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbOrigenes.ForeColor = System.Drawing.Color.Black
        Me.cbOrigenes.FormattingEnabled = True
        Me.cbOrigenes.Location = New System.Drawing.Point(103, 8)
        Me.cbOrigenes.Name = "cbOrigenes"
        Me.cbOrigenes.Size = New System.Drawing.Size(245, 24)
        Me.cbOrigenes.TabIndex = 80
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(28, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 15)
        Me.Label9.TabIndex = 79
        Me.Label9.Text = "ORIGEN: *"
        '
        'cbProveedores
        '
        Me.cbProveedores.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbProveedores.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbProveedores.BackColor = System.Drawing.Color.White
        Me.cbProveedores.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProveedores.ForeColor = System.Drawing.Color.Black
        Me.cbProveedores.FormattingEnabled = True
        Me.cbProveedores.Location = New System.Drawing.Point(117, 246)
        Me.cbProveedores.Name = "cbProveedores"
        Me.cbProveedores.Size = New System.Drawing.Size(231, 24)
        Me.cbProveedores.TabIndex = 78
        '
        'btnMostrarOcultar
        '
        Me.btnMostrarOcultar.BackColor = System.Drawing.Color.Transparent
        Me.btnMostrarOcultar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnMostrarOcultar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMostrarOcultar.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnMostrarOcultar.FlatAppearance.BorderSize = 0
        Me.btnMostrarOcultar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnMostrarOcultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMostrarOcultar.ForeColor = System.Drawing.Color.Black
        Me.btnMostrarOcultar.Image = CType(resources.GetObject("btnMostrarOcultar.Image"), System.Drawing.Image)
        Me.btnMostrarOcultar.Location = New System.Drawing.Point(352, 0)
        Me.btnMostrarOcultar.Margin = New System.Windows.Forms.Padding(0)
        Me.btnMostrarOcultar.Name = "btnMostrarOcultar"
        Me.btnMostrarOcultar.Size = New System.Drawing.Size(40, 38)
        Me.btnMostrarOcultar.TabIndex = 77
        Me.btnMostrarOcultar.UseVisualStyleBackColor = False
        '
        'btnIdSiguiente
        '
        Me.btnIdSiguiente.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnIdSiguiente.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIdSiguiente.Location = New System.Drawing.Point(178, 68)
        Me.btnIdSiguiente.Name = "btnIdSiguiente"
        Me.btnIdSiguiente.Size = New System.Drawing.Size(25, 28)
        Me.btnIdSiguiente.TabIndex = 21
        Me.btnIdSiguiente.Text = ">"
        Me.btnIdSiguiente.UseVisualStyleBackColor = True
        '
        'btnIdAnterior
        '
        Me.btnIdAnterior.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnIdAnterior.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIdAnterior.Location = New System.Drawing.Point(154, 68)
        Me.btnIdAnterior.Name = "btnIdAnterior"
        Me.btnIdAnterior.Size = New System.Drawing.Size(25, 28)
        Me.btnIdAnterior.TabIndex = 20
        Me.btnIdAnterior.Text = "<"
        Me.btnIdAnterior.UseVisualStyleBackColor = True
        '
        'cbAlmacenes
        '
        Me.cbAlmacenes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbAlmacenes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbAlmacenes.BackColor = System.Drawing.Color.White
        Me.cbAlmacenes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAlmacenes.ForeColor = System.Drawing.Color.Black
        Me.cbAlmacenes.FormattingEnabled = True
        Me.cbAlmacenes.Location = New System.Drawing.Point(103, 38)
        Me.cbAlmacenes.Name = "cbAlmacenes"
        Me.cbAlmacenes.Size = New System.Drawing.Size(245, 24)
        Me.cbAlmacenes.TabIndex = 19
        '
        'chkMantenerDatos
        '
        Me.chkMantenerDatos.AutoSize = True
        Me.chkMantenerDatos.Checked = True
        Me.chkMantenerDatos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMantenerDatos.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkMantenerDatos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMantenerDatos.ForeColor = System.Drawing.Color.Black
        Me.chkMantenerDatos.Location = New System.Drawing.Point(117, 278)
        Me.chkMantenerDatos.Name = "chkMantenerDatos"
        Me.chkMantenerDatos.Size = New System.Drawing.Size(214, 20)
        Me.chkMantenerDatos.TabIndex = 18
        Me.chkMantenerDatos.Text = "Mantener Datos Al Guardar"
        Me.chkMantenerDatos.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(10, 250)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(106, 15)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "PROVEEDOR: *"
        '
        'txtTipoCambio
        '
        Me.txtTipoCambio.BackColor = System.Drawing.Color.White
        Me.txtTipoCambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTipoCambio.ForeColor = System.Drawing.Color.Black
        Me.txtTipoCambio.Location = New System.Drawing.Point(117, 188)
        Me.txtTipoCambio.MaxLength = 8
        Me.txtTipoCambio.Name = "txtTipoCambio"
        Me.txtTipoCambio.Size = New System.Drawing.Size(50, 22)
        Me.txtTipoCambio.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 192)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(108, 15)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "TIPO CAMBIO: *"
        '
        'cbMonedas
        '
        Me.cbMonedas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbMonedas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbMonedas.BackColor = System.Drawing.Color.White
        Me.cbMonedas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMonedas.ForeColor = System.Drawing.Color.Black
        Me.cbMonedas.FormattingEnabled = True
        Me.cbMonedas.Location = New System.Drawing.Point(117, 158)
        Me.cbMonedas.Name = "cbMonedas"
        Me.cbMonedas.Size = New System.Drawing.Size(231, 24)
        Me.cbMonedas.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(36, 163)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 15)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "MONEDA: *"
        '
        'cbTiposEntradas
        '
        Me.cbTiposEntradas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbTiposEntradas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbTiposEntradas.BackColor = System.Drawing.Color.White
        Me.cbTiposEntradas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTiposEntradas.ForeColor = System.Drawing.Color.Black
        Me.cbTiposEntradas.FormattingEnabled = True
        Me.cbTiposEntradas.Location = New System.Drawing.Point(117, 216)
        Me.cbTiposEntradas.Name = "cbTiposEntradas"
        Me.cbTiposEntradas.Size = New System.Drawing.Size(231, 24)
        Me.cbTiposEntradas.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(-3, 221)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(119, 15)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "TIPO ENTRADA: *"
        '
        'txtIdExterno
        '
        Me.txtIdExterno.BackColor = System.Drawing.Color.White
        Me.txtIdExterno.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdExterno.ForeColor = System.Drawing.Color.Black
        Me.txtIdExterno.Location = New System.Drawing.Point(117, 102)
        Me.txtIdExterno.Name = "txtIdExterno"
        Me.txtIdExterno.Size = New System.Drawing.Size(67, 22)
        Me.txtIdExterno.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 106)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 15)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "NO. EXTERNO:"
        '
        'txtId
        '
        Me.txtId.BackColor = System.Drawing.Color.White
        Me.txtId.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtId.ForeColor = System.Drawing.Color.Black
        Me.txtId.Location = New System.Drawing.Point(103, 71)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(50, 22)
        Me.txtId.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(61, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "NO: *"
        '
        'dtpFecha
        '
        Me.dtpFecha.CalendarForeColor = System.Drawing.Color.Black
        Me.dtpFecha.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFecha.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dtpFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFecha.Location = New System.Drawing.Point(103, 130)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(245, 22)
        Me.dtpFecha.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(37, 134)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "FECHA: *"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ALMACÉN: *"
        '
        'pnlCatalogos
        '
        Me.pnlCatalogos.BackColor = System.Drawing.Color.Indigo
        Me.pnlCatalogos.Controls.Add(Me.txtBuscarCatalogo)
        Me.pnlCatalogos.Controls.Add(Me.Label10)
        Me.pnlCatalogos.Controls.Add(Me.spCatalogos)
        Me.pnlCatalogos.Location = New System.Drawing.Point(392, 0)
        Me.pnlCatalogos.Name = "pnlCatalogos"
        Me.pnlCatalogos.Size = New System.Drawing.Size(260, 150)
        Me.pnlCatalogos.TabIndex = 24
        Me.pnlCatalogos.Visible = False
        '
        'txtBuscarCatalogo
        '
        Me.txtBuscarCatalogo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBuscarCatalogo.BackColor = System.Drawing.Color.White
        Me.txtBuscarCatalogo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuscarCatalogo.ForeColor = System.Drawing.Color.Black
        Me.txtBuscarCatalogo.Location = New System.Drawing.Point(79, 121)
        Me.txtBuscarCatalogo.MaxLength = 300
        Me.txtBuscarCatalogo.Name = "txtBuscarCatalogo"
        Me.txtBuscarCatalogo.Size = New System.Drawing.Size(178, 26)
        Me.txtBuscarCatalogo.TabIndex = 55
        '
        'Label10
        '
        Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(0, 125)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 18)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "BUSCAR:"
        '
        'spCatalogos
        '
        Me.spCatalogos.AccessibleDescription = ""
        Me.spCatalogos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spCatalogos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.spCatalogos.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spCatalogos.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer1.ArrowColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer1.ArrowHoveredColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer1.ArrowSelectedColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer1.ButtonBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer1.ButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer1.ButtonHoveredBackgroundColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer1.ButtonHoveredBorderColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer1.ButtonSelectedBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer1.ButtonSelectedBorderColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer1.TrackBarBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer1.TrackBarSelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue
        Me.spCatalogos.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer1
        Me.spCatalogos.HorizontalScrollBar.TabIndex = 0
        Me.spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.spCatalogos.Location = New System.Drawing.Point(0, 0)
        Me.spCatalogos.Name = "spCatalogos"
        NamedStyle1.ForeColor = System.Drawing.Color.White
        NamedStyle1.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle1.Locked = False
        NamedStyle1.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle1.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle2.BackColor = System.Drawing.Color.Gainsboro
        NamedStyle2.CellType = GeneralCellType1
        NamedStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        NamedStyle2.Locked = False
        NamedStyle2.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle2.Renderer = GeneralCellType1
        NamedStyle3.BackColor = System.Drawing.Color.DarkSlateBlue
        NamedStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        NamedStyle3.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle3.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle3.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle4.BackColor = System.Drawing.Color.DarkSlateBlue
        NamedStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        NamedStyle4.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle4.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle4.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle5.BackColor = System.Drawing.Color.MidnightBlue
        NamedStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        NamedStyle5.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle5.NoteIndicatorColor = System.Drawing.Color.Red
        EnhancedCornerRenderer1.ActiveBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedCornerRenderer1.GridLineColor = System.Drawing.Color.Empty
        EnhancedCornerRenderer1.NormalBackgroundColor = System.Drawing.Color.MidnightBlue
        NamedStyle5.Renderer = EnhancedCornerRenderer1
        NamedStyle5.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle6.BackColor = System.Drawing.Color.DarkGray
        NamedStyle6.CellType = GeneralCellType2
        NamedStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        NamedStyle6.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle6.Renderer = GeneralCellType2
        Me.spCatalogos.NamedStyles.AddRange(New FarPoint.Win.Spread.NamedStyle() {NamedStyle1, NamedStyle2, NamedStyle3, NamedStyle4, NamedStyle5, NamedStyle6})
        Me.spCatalogos.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spCatalogos_Sheet1})
        Me.spCatalogos.Size = New System.Drawing.Size(260, 120)
        Me.spCatalogos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Midnight
        Me.spCatalogos.TabIndex = 22
        Me.spCatalogos.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spCatalogos.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer2.ArrowColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer2.ArrowHoveredColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer2.ArrowSelectedColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer2.ButtonBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer2.ButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer2.ButtonHoveredBackgroundColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer2.ButtonHoveredBorderColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer2.ButtonSelectedBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer2.ButtonSelectedBorderColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer2.TrackBarBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer2.TrackBarSelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue
        Me.spCatalogos.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer2
        Me.spCatalogos.VerticalScrollBar.TabIndex = 11
        '
        'spCatalogos_Sheet1
        '
        Me.spCatalogos_Sheet1.Reset()
        spCatalogos_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spCatalogos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spCatalogos_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCatalogos_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderMidnight"
        Me.spCatalogos_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCatalogos_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerMidnight"
        Me.spCatalogos_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCatalogos_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderMidnight"
        Me.spCatalogos_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCatalogos_Sheet1.DefaultStyle.Parent = "DataAreaMidnght"
        Me.spCatalogos_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCatalogos_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderMidnight"
        Me.spCatalogos_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCatalogos_Sheet1.SheetCornerStyle.Parent = "CornerMidnight"
        Me.spCatalogos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'spEntradas
        '
        Me.spEntradas.AccessibleDescription = ""
        Me.spEntradas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spEntradas.BackColor = System.Drawing.Color.White
        Me.spEntradas.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spEntradas.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer3.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer3.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer3.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer3.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer3.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer3.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer3.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer3.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer3.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer3.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer3.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spEntradas.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer3
        Me.spEntradas.HorizontalScrollBar.TabIndex = 10
        Me.spEntradas.Location = New System.Drawing.Point(392, 0)
        Me.spEntradas.Name = "spEntradas"
        Me.spEntradas.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spEntradas_Sheet1})
        Me.spEntradas.Size = New System.Drawing.Size(636, 490)
        Me.spEntradas.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        Me.spEntradas.TabIndex = 0
        Me.spEntradas.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spEntradas.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer4.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer4.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer4.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer4.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer4.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer4.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer4.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer4.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer4.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer4.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer4.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spEntradas.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer4
        Me.spEntradas.VerticalScrollBar.TabIndex = 11
        '
        'spEntradas_Sheet1
        '
        Me.spEntradas_Sheet1.Reset()
        spEntradas_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spEntradas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spEntradas_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spEntradas_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spEntradas_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spEntradas_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerSeashell"
        Me.spEntradas_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spEntradas_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spEntradas_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spEntradas_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell"
        Me.spEntradas_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spEntradas_Sheet1.SheetCornerStyle.Parent = "CornerSeashell"
        Me.spEntradas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'pnlPie
        '
        Me.pnlPie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPie.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pnlPie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPie.Controls.Add(Me.btnListados)
        Me.pnlPie.Controls.Add(Me.btnAyuda)
        Me.pnlPie.Controls.Add(Me.lblDescripcionTooltip)
        Me.pnlPie.Controls.Add(Me.btnEliminar)
        Me.pnlPie.Controls.Add(Me.btnSalir)
        Me.pnlPie.Controls.Add(Me.btnGuardar)
        Me.pnlPie.ForeColor = System.Drawing.Color.White
        Me.pnlPie.Location = New System.Drawing.Point(0, 570)
        Me.pnlPie.Name = "pnlPie"
        Me.pnlPie.Size = New System.Drawing.Size(1034, 60)
        Me.pnlPie.TabIndex = 8
        '
        'btnListados
        '
        Me.btnListados.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnListados.BackColor = System.Drawing.Color.White
        Me.btnListados.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnListados.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnListados.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnListados.FlatAppearance.BorderSize = 3
        Me.btnListados.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnListados.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnListados.ForeColor = System.Drawing.Color.Black
        Me.btnListados.Image = CType(resources.GetObject("btnListados.Image"), System.Drawing.Image)
        Me.btnListados.Location = New System.Drawing.Point(64, 0)
        Me.btnListados.Margin = New System.Windows.Forms.Padding(0)
        Me.btnListados.Name = "btnListados"
        Me.btnListados.Size = New System.Drawing.Size(60, 60)
        Me.btnListados.TabIndex = 19
        Me.btnListados.UseVisualStyleBackColor = False
        '
        'btnAyuda
        '
        Me.btnAyuda.BackColor = System.Drawing.Color.White
        Me.btnAyuda.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnAyuda.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAyuda.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnAyuda.FlatAppearance.BorderSize = 3
        Me.btnAyuda.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnAyuda.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAyuda.ForeColor = System.Drawing.Color.Black
        Me.btnAyuda.Image = CType(resources.GetObject("btnAyuda.Image"), System.Drawing.Image)
        Me.btnAyuda.Location = New System.Drawing.Point(0, 0)
        Me.btnAyuda.Margin = New System.Windows.Forms.Padding(0)
        Me.btnAyuda.Name = "btnAyuda"
        Me.btnAyuda.Size = New System.Drawing.Size(60, 60)
        Me.btnAyuda.TabIndex = 5
        Me.btnAyuda.UseVisualStyleBackColor = False
        '
        'lblDescripcionTooltip
        '
        Me.lblDescripcionTooltip.AutoSize = True
        Me.lblDescripcionTooltip.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescripcionTooltip.ForeColor = System.Drawing.Color.White
        Me.lblDescripcionTooltip.Location = New System.Drawing.Point(150, 17)
        Me.lblDescripcionTooltip.Name = "lblDescripcionTooltip"
        Me.lblDescripcionTooltip.Size = New System.Drawing.Size(0, 31)
        Me.lblDescripcionTooltip.TabIndex = 4
        '
        'btnEliminar
        '
        Me.btnEliminar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEliminar.BackColor = System.Drawing.Color.White
        Me.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminar.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnEliminar.FlatAppearance.BorderSize = 3
        Me.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEliminar.ForeColor = System.Drawing.Color.Black
        Me.btnEliminar.Image = CType(resources.GetObject("btnEliminar.Image"), System.Drawing.Image)
        Me.btnEliminar.Location = New System.Drawing.Point(844, 0)
        Me.btnEliminar.Margin = New System.Windows.Forms.Padding(0)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(60, 60)
        Me.btnEliminar.TabIndex = 18
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnSalir
        '
        Me.btnSalir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSalir.BackColor = System.Drawing.Color.White
        Me.btnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnSalir.FlatAppearance.BorderSize = 3
        Me.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalir.ForeColor = System.Drawing.Color.Black
        Me.btnSalir.Image = CType(resources.GetObject("btnSalir.Image"), System.Drawing.Image)
        Me.btnSalir.Location = New System.Drawing.Point(972, 0)
        Me.btnSalir.Margin = New System.Windows.Forms.Padding(0)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(60, 60)
        Me.btnSalir.TabIndex = 2
        Me.btnSalir.UseVisualStyleBackColor = False
        '
        'btnGuardar
        '
        Me.btnGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuardar.BackColor = System.Drawing.Color.White
        Me.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnGuardar.FlatAppearance.BorderSize = 3
        Me.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGuardar.ForeColor = System.Drawing.Color.Black
        Me.btnGuardar.Image = CType(resources.GetObject("btnGuardar.Image"), System.Drawing.Image)
        Me.btnGuardar.Location = New System.Drawing.Point(907, 0)
        Me.btnGuardar.Margin = New System.Windows.Forms.Padding(0)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(60, 60)
        Me.btnGuardar.TabIndex = 17
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'pnlEncabezado
        '
        Me.pnlEncabezado.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pnlEncabezado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlEncabezado.Controls.Add(Me.lblEncabezadoArea)
        Me.pnlEncabezado.Controls.Add(Me.lblEncabezadoUsuario)
        Me.pnlEncabezado.Controls.Add(Me.lblEncabezadoEmpresa)
        Me.pnlEncabezado.Controls.Add(Me.lblEncabezadoPrograma)
        Me.pnlEncabezado.ForeColor = System.Drawing.Color.White
        Me.pnlEncabezado.Location = New System.Drawing.Point(0, 0)
        Me.pnlEncabezado.Name = "pnlEncabezado"
        Me.pnlEncabezado.Size = New System.Drawing.Size(1034, 75)
        Me.pnlEncabezado.TabIndex = 7
        '
        'lblEncabezadoArea
        '
        Me.lblEncabezadoArea.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEncabezadoArea.AutoSize = True
        Me.lblEncabezadoArea.BackColor = System.Drawing.Color.Transparent
        Me.lblEncabezadoArea.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEncabezadoArea.ForeColor = System.Drawing.Color.White
        Me.lblEncabezadoArea.Location = New System.Drawing.Point(599, 0)
        Me.lblEncabezadoArea.Name = "lblEncabezadoArea"
        Me.lblEncabezadoArea.Size = New System.Drawing.Size(0, 33)
        Me.lblEncabezadoArea.TabIndex = 5
        '
        'lblEncabezadoUsuario
        '
        Me.lblEncabezadoUsuario.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEncabezadoUsuario.AutoSize = True
        Me.lblEncabezadoUsuario.BackColor = System.Drawing.Color.Transparent
        Me.lblEncabezadoUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEncabezadoUsuario.ForeColor = System.Drawing.Color.White
        Me.lblEncabezadoUsuario.Location = New System.Drawing.Point(599, 35)
        Me.lblEncabezadoUsuario.Name = "lblEncabezadoUsuario"
        Me.lblEncabezadoUsuario.Size = New System.Drawing.Size(0, 33)
        Me.lblEncabezadoUsuario.TabIndex = 4
        '
        'lblEncabezadoEmpresa
        '
        Me.lblEncabezadoEmpresa.AutoSize = True
        Me.lblEncabezadoEmpresa.BackColor = System.Drawing.Color.Transparent
        Me.lblEncabezadoEmpresa.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEncabezadoEmpresa.ForeColor = System.Drawing.Color.White
        Me.lblEncabezadoEmpresa.Location = New System.Drawing.Point(12, 35)
        Me.lblEncabezadoEmpresa.Name = "lblEncabezadoEmpresa"
        Me.lblEncabezadoEmpresa.Size = New System.Drawing.Size(0, 33)
        Me.lblEncabezadoEmpresa.TabIndex = 1
        '
        'lblEncabezadoPrograma
        '
        Me.lblEncabezadoPrograma.AutoSize = True
        Me.lblEncabezadoPrograma.BackColor = System.Drawing.Color.Transparent
        Me.lblEncabezadoPrograma.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEncabezadoPrograma.ForeColor = System.Drawing.Color.White
        Me.lblEncabezadoPrograma.Location = New System.Drawing.Point(12, 0)
        Me.lblEncabezadoPrograma.Name = "lblEncabezadoPrograma"
        Me.lblEncabezadoPrograma.Size = New System.Drawing.Size(0, 33)
        Me.lblEncabezadoPrograma.TabIndex = 0
        '
        'temporizador
        '
        Me.temporizador.Interval = 1
        '
        'Principal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1034, 631)
        Me.Controls.Add(Me.pnlContenido)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Principal"
        Me.Text = "Almacén - Entradas"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlContenido.ResumeLayout(False)
        Me.pnlCuerpo.ResumeLayout(False)
        CType(Me.spListados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spListados_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCapturaSuperior.ResumeLayout(False)
        Me.pnlCapturaSuperior.PerformLayout()
        Me.pnlCatalogos.ResumeLayout(False)
        Me.pnlCatalogos.PerformLayout()
        CType(Me.spCatalogos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spCatalogos_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spEntradas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spEntradas_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPie.ResumeLayout(False)
        Me.pnlPie.PerformLayout()
        Me.pnlEncabezado.ResumeLayout(False)
        Me.pnlEncabezado.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnlContenido As System.Windows.Forms.Panel
    Private WithEvents pnlCuerpo As System.Windows.Forms.Panel
    Private WithEvents pnlPie As System.Windows.Forms.Panel
    Private WithEvents btnSalir As System.Windows.Forms.Button
    Private WithEvents pnlEncabezado As System.Windows.Forms.Panel
    Private WithEvents lblEncabezadoEmpresa As System.Windows.Forms.Label
    Private WithEvents lblEncabezadoPrograma As System.Windows.Forms.Label
    Friend WithEvents spEntradas As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spEntradas_Sheet1 As FarPoint.Win.Spread.SheetView
    Private WithEvents lblEncabezadoArea As System.Windows.Forms.Label
    Private WithEvents lblEncabezadoUsuario As System.Windows.Forms.Label
    Private WithEvents btnEliminar As System.Windows.Forms.Button
    Private WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents lblDescripcionTooltip As System.Windows.Forms.Label
    Private WithEvents spCatalogos As FarPoint.Win.Spread.FpSpread
    Private WithEvents spCatalogos_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents temporizador As System.Windows.Forms.Timer
    Private WithEvents btnAyuda As System.Windows.Forms.Button
    Friend WithEvents pnlCapturaSuperior As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtIdExterno As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbTiposEntradas As System.Windows.Forms.ComboBox
    Friend WithEvents cbMonedas As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTipoCambio As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkMantenerDatos As System.Windows.Forms.CheckBox
    Friend WithEvents cbAlmacenes As System.Windows.Forms.ComboBox
    Friend WithEvents pnlCatalogos As System.Windows.Forms.Panel
    Friend WithEvents btnIdAnterior As System.Windows.Forms.Button
    Friend WithEvents btnIdSiguiente As System.Windows.Forms.Button
    Private WithEvents btnMostrarOcultar As System.Windows.Forms.Button
    Friend WithEvents txtBuscarCatalogo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cbProveedores As System.Windows.Forms.ComboBox
    Friend WithEvents cbOrigenes As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents spListados As FarPoint.Win.Spread.FpSpread
    Private WithEvents spListados_Sheet1 As FarPoint.Win.Spread.SheetView
    Private WithEvents btnListados As System.Windows.Forms.Button
End Class
