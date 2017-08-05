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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Principal))
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
        Dim SpreadSkin1 As FarPoint.Win.Spread.SpreadSkin = New FarPoint.Win.Spread.SpreadSkin()
        Dim EnhancedFocusIndicatorRenderer1 As FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer = New FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer()
        Dim EnhancedInterfaceRenderer1 As FarPoint.Win.Spread.EnhancedInterfaceRenderer = New FarPoint.Win.Spread.EnhancedInterfaceRenderer()
        Dim EnhancedScrollBarRenderer2 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer3 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer4 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer5 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer6 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer7 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer8 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer9 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer10 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer11 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer12 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer13 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Me.pnlContenido = New System.Windows.Forms.Panel()
        Me.pnlCuerpo = New System.Windows.Forms.Panel()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.pnlMenu = New System.Windows.Forms.Panel()
        Me.rbtnClientes = New System.Windows.Forms.RadioButton()
        Me.rbtnTiposCambios = New System.Windows.Forms.RadioButton()
        Me.rbtnUnidadesMedidas = New System.Windows.Forms.RadioButton()
        Me.rbtnTiposSalidas = New System.Windows.Forms.RadioButton()
        Me.rbtnTiposEntradas = New System.Windows.Forms.RadioButton()
        Me.rbtnMonedas = New System.Windows.Forms.RadioButton()
        Me.rbtnProveedores = New System.Windows.Forms.RadioButton()
        Me.rbtnSubFamilias = New System.Windows.Forms.RadioButton()
        Me.rbtnAlmacenes = New System.Windows.Forms.RadioButton()
        Me.rbtnFamilias = New System.Windows.Forms.RadioButton()
        Me.rbtnArticulos = New System.Windows.Forms.RadioButton()
        Me.spCatalogos = New FarPoint.Win.Spread.FpSpread()
        Me.spCatalogos_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.spAlmacenes = New FarPoint.Win.Spread.FpSpread()
        Me.spAlmacenes_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.spFamilias = New FarPoint.Win.Spread.FpSpread()
        Me.spFamilias_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.spSubFamilias = New FarPoint.Win.Spread.FpSpread()
        Me.spSubFamilias_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.spArticulos = New FarPoint.Win.Spread.FpSpread()
        Me.spArticulos_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.spVarios = New FarPoint.Win.Spread.FpSpread()
        Me.spVarios_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.pnlPie = New System.Windows.Forms.Panel()
        Me.btnAyuda = New System.Windows.Forms.Button()
        Me.lblDescripcionTooltip = New System.Windows.Forms.Label()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.pnlEncabezado = New System.Windows.Forms.Panel()
        Me.lblEncabezadoArea = New System.Windows.Forms.Label()
        Me.lblEncabezadoUsuario = New System.Windows.Forms.Label()
        Me.lblEncabezadoEmpresa = New System.Windows.Forms.Label()
        Me.lblEncabezadoPrograma = New System.Windows.Forms.Label()
        Me.temporizador = New System.Windows.Forms.Timer(Me.components)
        Me.pnlContenido.SuspendLayout()
        Me.pnlCuerpo.SuspendLayout()
        Me.pnlMenu.SuspendLayout()
        CType(Me.spCatalogos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spCatalogos_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spAlmacenes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spAlmacenes_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spFamilias, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spFamilias_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spSubFamilias, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spSubFamilias_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spArticulos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spArticulos_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spVarios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spVarios_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlContenido.BackgroundImage = Global.Catalogos.My.Resources.Resources.Logo3
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
        Me.pnlCuerpo.Controls.Add(Me.btnEliminar)
        Me.pnlCuerpo.Controls.Add(Me.btnGuardar)
        Me.pnlCuerpo.Controls.Add(Me.pnlMenu)
        Me.pnlCuerpo.Controls.Add(Me.spCatalogos)
        Me.pnlCuerpo.Controls.Add(Me.spAlmacenes)
        Me.pnlCuerpo.Controls.Add(Me.spFamilias)
        Me.pnlCuerpo.Controls.Add(Me.spSubFamilias)
        Me.pnlCuerpo.Controls.Add(Me.spArticulos)
        Me.pnlCuerpo.Controls.Add(Me.spVarios)
        Me.pnlCuerpo.Location = New System.Drawing.Point(3, 77)
        Me.pnlCuerpo.Name = "pnlCuerpo"
        Me.pnlCuerpo.Size = New System.Drawing.Size(1028, 490)
        Me.pnlCuerpo.TabIndex = 9
        '
        'btnEliminar
        '
        Me.btnEliminar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEliminar.BackColor = System.Drawing.Color.White
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminar.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnEliminar.FlatAppearance.BorderSize = 3
        Me.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEliminar.ForeColor = System.Drawing.Color.Black
        Me.btnEliminar.Image = CType(resources.GetObject("btnEliminar.Image"), System.Drawing.Image)
        Me.btnEliminar.Location = New System.Drawing.Point(902, 428)
        Me.btnEliminar.Margin = New System.Windows.Forms.Padding(0)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(60, 60)
        Me.btnEliminar.TabIndex = 18
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnGuardar
        '
        Me.btnGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuardar.BackColor = System.Drawing.Color.White
        Me.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnGuardar.FlatAppearance.BorderSize = 3
        Me.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGuardar.ForeColor = System.Drawing.Color.Black
        Me.btnGuardar.Image = CType(resources.GetObject("btnGuardar.Image"), System.Drawing.Image)
        Me.btnGuardar.Location = New System.Drawing.Point(963, 428)
        Me.btnGuardar.Margin = New System.Windows.Forms.Padding(0)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(60, 60)
        Me.btnGuardar.TabIndex = 17
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'pnlMenu
        '
        Me.pnlMenu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMenu.AutoScroll = True
        Me.pnlMenu.BackColor = System.Drawing.Color.MintCream
        Me.pnlMenu.Controls.Add(Me.rbtnClientes)
        Me.pnlMenu.Controls.Add(Me.rbtnTiposCambios)
        Me.pnlMenu.Controls.Add(Me.rbtnUnidadesMedidas)
        Me.pnlMenu.Controls.Add(Me.rbtnTiposSalidas)
        Me.pnlMenu.Controls.Add(Me.rbtnTiposEntradas)
        Me.pnlMenu.Controls.Add(Me.rbtnMonedas)
        Me.pnlMenu.Controls.Add(Me.rbtnProveedores)
        Me.pnlMenu.Controls.Add(Me.rbtnSubFamilias)
        Me.pnlMenu.Controls.Add(Me.rbtnAlmacenes)
        Me.pnlMenu.Controls.Add(Me.rbtnFamilias)
        Me.pnlMenu.Controls.Add(Me.rbtnArticulos)
        Me.pnlMenu.Location = New System.Drawing.Point(0, 0)
        Me.pnlMenu.Name = "pnlMenu"
        Me.pnlMenu.Size = New System.Drawing.Size(1028, 40)
        Me.pnlMenu.TabIndex = 24
        '
        'rbtnClientes
        '
        Me.rbtnClientes.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnClientes.AutoSize = True
        Me.rbtnClientes.BackColor = System.Drawing.Color.Transparent
        Me.rbtnClientes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbtnClientes.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.rbtnClientes.FlatAppearance.BorderSize = 2
        Me.rbtnClientes.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine
        Me.rbtnClientes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.rbtnClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnClientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnClientes.ForeColor = System.Drawing.Color.Black
        Me.rbtnClientes.Location = New System.Drawing.Point(625, 3)
        Me.rbtnClientes.Name = "rbtnClientes"
        Me.rbtnClientes.Size = New System.Drawing.Size(102, 32)
        Me.rbtnClientes.TabIndex = 11
        Me.rbtnClientes.TabStop = True
        Me.rbtnClientes.Text = "CLIENTES"
        Me.rbtnClientes.UseVisualStyleBackColor = False
        '
        'rbtnTiposCambios
        '
        Me.rbtnTiposCambios.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnTiposCambios.AutoSize = True
        Me.rbtnTiposCambios.BackColor = System.Drawing.Color.Transparent
        Me.rbtnTiposCambios.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbtnTiposCambios.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.rbtnTiposCambios.FlatAppearance.BorderSize = 2
        Me.rbtnTiposCambios.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine
        Me.rbtnTiposCambios.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.rbtnTiposCambios.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnTiposCambios.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnTiposCambios.ForeColor = System.Drawing.Color.Black
        Me.rbtnTiposCambios.Location = New System.Drawing.Point(836, 3)
        Me.rbtnTiposCambios.Name = "rbtnTiposCambios"
        Me.rbtnTiposCambios.Size = New System.Drawing.Size(179, 32)
        Me.rbtnTiposCambios.TabIndex = 10
        Me.rbtnTiposCambios.TabStop = True
        Me.rbtnTiposCambios.Text = "TIPOS DE CAMBIOS"
        Me.rbtnTiposCambios.UseVisualStyleBackColor = False
        '
        'rbtnUnidadesMedidas
        '
        Me.rbtnUnidadesMedidas.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnUnidadesMedidas.AutoSize = True
        Me.rbtnUnidadesMedidas.BackColor = System.Drawing.Color.Transparent
        Me.rbtnUnidadesMedidas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbtnUnidadesMedidas.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.rbtnUnidadesMedidas.FlatAppearance.BorderSize = 2
        Me.rbtnUnidadesMedidas.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine
        Me.rbtnUnidadesMedidas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.rbtnUnidadesMedidas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnUnidadesMedidas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnUnidadesMedidas.ForeColor = System.Drawing.Color.Black
        Me.rbtnUnidadesMedidas.Location = New System.Drawing.Point(1384, 3)
        Me.rbtnUnidadesMedidas.Name = "rbtnUnidadesMedidas"
        Me.rbtnUnidadesMedidas.Size = New System.Drawing.Size(213, 32)
        Me.rbtnUnidadesMedidas.TabIndex = 9
        Me.rbtnUnidadesMedidas.TabStop = True
        Me.rbtnUnidadesMedidas.Text = "UNIDADES DE MEDIDAS"
        Me.rbtnUnidadesMedidas.UseVisualStyleBackColor = False
        '
        'rbtnTiposSalidas
        '
        Me.rbtnTiposSalidas.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnTiposSalidas.AutoSize = True
        Me.rbtnTiposSalidas.BackColor = System.Drawing.Color.Transparent
        Me.rbtnTiposSalidas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbtnTiposSalidas.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.rbtnTiposSalidas.FlatAppearance.BorderSize = 2
        Me.rbtnTiposSalidas.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine
        Me.rbtnTiposSalidas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.rbtnTiposSalidas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnTiposSalidas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnTiposSalidas.ForeColor = System.Drawing.Color.Black
        Me.rbtnTiposSalidas.Location = New System.Drawing.Point(1211, 3)
        Me.rbtnTiposSalidas.Name = "rbtnTiposSalidas"
        Me.rbtnTiposSalidas.Size = New System.Drawing.Size(171, 32)
        Me.rbtnTiposSalidas.TabIndex = 8
        Me.rbtnTiposSalidas.TabStop = True
        Me.rbtnTiposSalidas.Text = "TIPOS DE SALIDAS"
        Me.rbtnTiposSalidas.UseVisualStyleBackColor = False
        '
        'rbtnTiposEntradas
        '
        Me.rbtnTiposEntradas.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnTiposEntradas.AutoSize = True
        Me.rbtnTiposEntradas.BackColor = System.Drawing.Color.Transparent
        Me.rbtnTiposEntradas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbtnTiposEntradas.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.rbtnTiposEntradas.FlatAppearance.BorderSize = 2
        Me.rbtnTiposEntradas.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine
        Me.rbtnTiposEntradas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.rbtnTiposEntradas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnTiposEntradas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnTiposEntradas.ForeColor = System.Drawing.Color.Black
        Me.rbtnTiposEntradas.Location = New System.Drawing.Point(1017, 3)
        Me.rbtnTiposEntradas.Name = "rbtnTiposEntradas"
        Me.rbtnTiposEntradas.Size = New System.Drawing.Size(192, 32)
        Me.rbtnTiposEntradas.TabIndex = 7
        Me.rbtnTiposEntradas.TabStop = True
        Me.rbtnTiposEntradas.Text = "TIPOS DE ENTRADAS"
        Me.rbtnTiposEntradas.UseVisualStyleBackColor = False
        '
        'rbtnMonedas
        '
        Me.rbtnMonedas.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnMonedas.AutoSize = True
        Me.rbtnMonedas.BackColor = System.Drawing.Color.Transparent
        Me.rbtnMonedas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbtnMonedas.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.rbtnMonedas.FlatAppearance.BorderSize = 2
        Me.rbtnMonedas.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine
        Me.rbtnMonedas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.rbtnMonedas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnMonedas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnMonedas.ForeColor = System.Drawing.Color.Black
        Me.rbtnMonedas.Location = New System.Drawing.Point(729, 3)
        Me.rbtnMonedas.Name = "rbtnMonedas"
        Me.rbtnMonedas.Size = New System.Drawing.Size(105, 32)
        Me.rbtnMonedas.TabIndex = 6
        Me.rbtnMonedas.TabStop = True
        Me.rbtnMonedas.Text = "MONEDAS"
        Me.rbtnMonedas.UseVisualStyleBackColor = False
        '
        'rbtnProveedores
        '
        Me.rbtnProveedores.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnProveedores.AutoSize = True
        Me.rbtnProveedores.BackColor = System.Drawing.Color.Transparent
        Me.rbtnProveedores.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbtnProveedores.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.rbtnProveedores.FlatAppearance.BorderSize = 2
        Me.rbtnProveedores.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine
        Me.rbtnProveedores.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.rbtnProveedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnProveedores.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnProveedores.ForeColor = System.Drawing.Color.Black
        Me.rbtnProveedores.Location = New System.Drawing.Point(474, 3)
        Me.rbtnProveedores.Name = "rbtnProveedores"
        Me.rbtnProveedores.Size = New System.Drawing.Size(149, 32)
        Me.rbtnProveedores.TabIndex = 5
        Me.rbtnProveedores.TabStop = True
        Me.rbtnProveedores.Text = "PROVEEDORES"
        Me.rbtnProveedores.UseVisualStyleBackColor = False
        '
        'rbtnSubFamilias
        '
        Me.rbtnSubFamilias.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnSubFamilias.AutoSize = True
        Me.rbtnSubFamilias.BackColor = System.Drawing.Color.Transparent
        Me.rbtnSubFamilias.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbtnSubFamilias.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.rbtnSubFamilias.FlatAppearance.BorderSize = 2
        Me.rbtnSubFamilias.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine
        Me.rbtnSubFamilias.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.rbtnSubFamilias.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnSubFamilias.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSubFamilias.ForeColor = System.Drawing.Color.Black
        Me.rbtnSubFamilias.Location = New System.Drawing.Point(227, 3)
        Me.rbtnSubFamilias.Name = "rbtnSubFamilias"
        Me.rbtnSubFamilias.Size = New System.Drawing.Size(128, 32)
        Me.rbtnSubFamilias.TabIndex = 4
        Me.rbtnSubFamilias.TabStop = True
        Me.rbtnSubFamilias.Text = "SUBFAMILIAS"
        Me.rbtnSubFamilias.UseVisualStyleBackColor = False
        '
        'rbtnAlmacenes
        '
        Me.rbtnAlmacenes.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnAlmacenes.AutoSize = True
        Me.rbtnAlmacenes.BackColor = System.Drawing.Color.Transparent
        Me.rbtnAlmacenes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbtnAlmacenes.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.rbtnAlmacenes.FlatAppearance.BorderSize = 2
        Me.rbtnAlmacenes.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine
        Me.rbtnAlmacenes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.rbtnAlmacenes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnAlmacenes.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnAlmacenes.ForeColor = System.Drawing.Color.Black
        Me.rbtnAlmacenes.Location = New System.Drawing.Point(7, 3)
        Me.rbtnAlmacenes.Name = "rbtnAlmacenes"
        Me.rbtnAlmacenes.Size = New System.Drawing.Size(122, 32)
        Me.rbtnAlmacenes.TabIndex = 3
        Me.rbtnAlmacenes.TabStop = True
        Me.rbtnAlmacenes.Text = "ALMACENES"
        Me.rbtnAlmacenes.UseVisualStyleBackColor = False
        '
        'rbtnFamilias
        '
        Me.rbtnFamilias.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnFamilias.AutoSize = True
        Me.rbtnFamilias.BackColor = System.Drawing.Color.Transparent
        Me.rbtnFamilias.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbtnFamilias.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.rbtnFamilias.FlatAppearance.BorderSize = 2
        Me.rbtnFamilias.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine
        Me.rbtnFamilias.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.rbtnFamilias.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnFamilias.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnFamilias.ForeColor = System.Drawing.Color.Black
        Me.rbtnFamilias.Location = New System.Drawing.Point(131, 3)
        Me.rbtnFamilias.Name = "rbtnFamilias"
        Me.rbtnFamilias.Size = New System.Drawing.Size(94, 32)
        Me.rbtnFamilias.TabIndex = 2
        Me.rbtnFamilias.TabStop = True
        Me.rbtnFamilias.Text = "FAMILIAS"
        Me.rbtnFamilias.UseVisualStyleBackColor = False
        '
        'rbtnArticulos
        '
        Me.rbtnArticulos.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnArticulos.AutoSize = True
        Me.rbtnArticulos.BackColor = System.Drawing.Color.Transparent
        Me.rbtnArticulos.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbtnArticulos.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.rbtnArticulos.FlatAppearance.BorderSize = 2
        Me.rbtnArticulos.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine
        Me.rbtnArticulos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise
        Me.rbtnArticulos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbtnArticulos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnArticulos.ForeColor = System.Drawing.Color.Black
        Me.rbtnArticulos.Location = New System.Drawing.Point(357, 3)
        Me.rbtnArticulos.Name = "rbtnArticulos"
        Me.rbtnArticulos.Size = New System.Drawing.Size(115, 32)
        Me.rbtnArticulos.TabIndex = 0
        Me.rbtnArticulos.TabStop = True
        Me.rbtnArticulos.Text = "ARTÍCULOS"
        Me.rbtnArticulos.UseVisualStyleBackColor = False
        '
        'spCatalogos
        '
        Me.spCatalogos.AccessibleDescription = ""
        Me.spCatalogos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
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
        Me.spCatalogos.HorizontalScrollBar.TabIndex = 10
        Me.spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spCatalogos.Location = New System.Drawing.Point(7, 42)
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
        NamedStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer))
        NamedStyle6.CellType = GeneralCellType2
        NamedStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        NamedStyle6.Locked = False
        NamedStyle6.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle6.Renderer = GeneralCellType2
        Me.spCatalogos.NamedStyles.AddRange(New FarPoint.Win.Spread.NamedStyle() {NamedStyle1, NamedStyle2, NamedStyle3, NamedStyle4, NamedStyle5, NamedStyle6})
        Me.spCatalogos.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spCatalogos_Sheet1})
        Me.spCatalogos.Size = New System.Drawing.Size(221, 183)
        SpreadSkin1.ColumnFooterDefaultStyle = NamedStyle3
        SpreadSkin1.ColumnHeaderDefaultStyle = NamedStyle3
        SpreadSkin1.CornerDefaultStyle = NamedStyle5
        SpreadSkin1.DefaultStyle = NamedStyle6
        SpreadSkin1.FocusRenderer = EnhancedFocusIndicatorRenderer1
        EnhancedInterfaceRenderer1.ArrowColorEnabled = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.GrayAreaColor = System.Drawing.Color.LightSlateGray
        EnhancedInterfaceRenderer1.RangeGroupBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer1.RangeGroupButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.RangeGroupLineColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.ScrollBoxBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedInterfaceRenderer1.SheetTabBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.SheetTabLowerActiveColor = System.Drawing.Color.DarkGray
        EnhancedInterfaceRenderer1.SheetTabLowerNormalColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.SheetTabUpperActiveColor = System.Drawing.Color.LightGray
        EnhancedInterfaceRenderer1.SheetTabUpperNormalColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer1.SplitBarBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer1.SplitBarDarkColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.SplitBarLightColor = System.Drawing.Color.DarkGray
        EnhancedInterfaceRenderer1.SplitBoxBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer1.SplitBoxBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.TabStripBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedInterfaceRenderer1.TabStripButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.TabStripButtonFlatStyle = System.Windows.Forms.FlatStyle.Flat
        EnhancedInterfaceRenderer1.TabStripButtonLowerActiveColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer1.TabStripButtonLowerNormalColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.TabStripButtonLowerPressedColor = System.Drawing.Color.DimGray
        EnhancedInterfaceRenderer1.TabStripButtonUpperActiveColor = System.Drawing.Color.DarkGray
        EnhancedInterfaceRenderer1.TabStripButtonUpperNormalColor = System.Drawing.Color.SlateBlue
        EnhancedInterfaceRenderer1.TabStripButtonUpperPressedColor = System.Drawing.Color.DarkSlateBlue
        SpreadSkin1.InterfaceRenderer = EnhancedInterfaceRenderer1
        SpreadSkin1.Name = "MidnightPersonalizado"
        SpreadSkin1.RowHeaderDefaultStyle = NamedStyle4
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
        SpreadSkin1.ScrollBarRenderer = EnhancedScrollBarRenderer2
        SpreadSkin1.SelectionRenderer = New FarPoint.Win.Spread.GradientSelectionRenderer(System.Drawing.Color.MidnightBlue, System.Drawing.Color.MidnightBlue, System.Drawing.Drawing2D.LinearGradientMode.Horizontal, 80)
        Me.spCatalogos.Skin = SpreadSkin1
        Me.spCatalogos.TabIndex = 22
        Me.spCatalogos.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spCatalogos.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer3.ArrowColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer3.ArrowHoveredColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer3.ArrowSelectedColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer3.ButtonBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer3.ButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer3.ButtonHoveredBackgroundColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer3.ButtonHoveredBorderColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer3.ButtonSelectedBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer3.ButtonSelectedBorderColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer3.TrackBarBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer3.TrackBarSelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue
        Me.spCatalogos.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer3
        Me.spCatalogos.VerticalScrollBar.TabIndex = 11
        Me.spCatalogos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spCatalogos.Visible = False
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
        'spAlmacenes
        '
        Me.spAlmacenes.AccessibleDescription = ""
        Me.spAlmacenes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spAlmacenes.BackColor = System.Drawing.Color.White
        Me.spAlmacenes.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spAlmacenes.HorizontalScrollBar.Name = ""
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
        Me.spAlmacenes.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer4
        Me.spAlmacenes.HorizontalScrollBar.TabIndex = 10
        Me.spAlmacenes.Location = New System.Drawing.Point(5, 42)
        Me.spAlmacenes.Name = "spAlmacenes"
        Me.spAlmacenes.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spAlmacenes_Sheet1})
        Me.spAlmacenes.Size = New System.Drawing.Size(334, 212)
        Me.spAlmacenes.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        Me.spAlmacenes.TabIndex = 0
        Me.spAlmacenes.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spAlmacenes.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer5.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer5.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer5.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer5.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer5.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer5.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer5.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer5.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer5.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer5.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer5.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spAlmacenes.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer5
        Me.spAlmacenes.VerticalScrollBar.TabIndex = 11
        Me.spAlmacenes.Visible = False
        '
        'spAlmacenes_Sheet1
        '
        Me.spAlmacenes_Sheet1.Reset()
        spAlmacenes_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spAlmacenes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spAlmacenes_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spAlmacenes_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spAlmacenes_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spAlmacenes_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerSeashell"
        Me.spAlmacenes_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spAlmacenes_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spAlmacenes_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spAlmacenes_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell"
        Me.spAlmacenes_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spAlmacenes_Sheet1.SheetCornerStyle.Parent = "CornerSeashell"
        Me.spAlmacenes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'spFamilias
        '
        Me.spFamilias.AccessibleDescription = ""
        Me.spFamilias.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spFamilias.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spFamilias.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer6.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer6.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer6.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer6.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer6.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer6.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer6.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer6.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer6.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer6.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer6.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spFamilias.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer6
        Me.spFamilias.HorizontalScrollBar.TabIndex = 2
        Me.spFamilias.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spFamilias.Location = New System.Drawing.Point(345, 43)
        Me.spFamilias.Name = "spFamilias"
        Me.spFamilias.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spFamilias_Sheet1})
        Me.spFamilias.Size = New System.Drawing.Size(335, 211)
        Me.spFamilias.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        Me.spFamilias.TabIndex = 23
        Me.spFamilias.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spFamilias.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer7.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer7.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer7.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer7.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer7.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer7.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer7.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer7.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer7.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer7.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer7.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spFamilias.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer7
        Me.spFamilias.VerticalScrollBar.TabIndex = 3
        Me.spFamilias.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spFamilias.Visible = False
        '
        'spFamilias_Sheet1
        '
        Me.spFamilias_Sheet1.Reset()
        spFamilias_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spFamilias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spFamilias_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spFamilias_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spFamilias_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spFamilias_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerSeashell"
        Me.spFamilias_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spFamilias_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spFamilias_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spFamilias_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell"
        Me.spFamilias_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spFamilias_Sheet1.SheetCornerStyle.Parent = "CornerSeashell"
        Me.spFamilias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'spSubFamilias
        '
        Me.spSubFamilias.AccessibleDescription = ""
        Me.spSubFamilias.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spSubFamilias.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spSubFamilias.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer8.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer8.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer8.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer8.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer8.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer8.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer8.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer8.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer8.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer8.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer8.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spSubFamilias.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer8
        Me.spSubFamilias.HorizontalScrollBar.TabIndex = 2
        Me.spSubFamilias.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spSubFamilias.Location = New System.Drawing.Point(687, 43)
        Me.spSubFamilias.Name = "spSubFamilias"
        Me.spSubFamilias.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spSubFamilias_Sheet1})
        Me.spSubFamilias.Size = New System.Drawing.Size(335, 211)
        Me.spSubFamilias.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        Me.spSubFamilias.TabIndex = 25
        Me.spSubFamilias.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spSubFamilias.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer9.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer9.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer9.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer9.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer9.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer9.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer9.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer9.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer9.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer9.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer9.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spSubFamilias.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer9
        Me.spSubFamilias.VerticalScrollBar.TabIndex = 3
        Me.spSubFamilias.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spSubFamilias.Visible = False
        '
        'spSubFamilias_Sheet1
        '
        Me.spSubFamilias_Sheet1.Reset()
        spSubFamilias_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spSubFamilias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spSubFamilias_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spSubFamilias_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spSubFamilias_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spSubFamilias_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerSeashell"
        Me.spSubFamilias_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spSubFamilias_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spSubFamilias_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spSubFamilias_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell"
        Me.spSubFamilias_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spSubFamilias_Sheet1.SheetCornerStyle.Parent = "CornerSeashell"
        Me.spSubFamilias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'spArticulos
        '
        Me.spArticulos.AccessibleDescription = ""
        Me.spArticulos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spArticulos.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spArticulos.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer10.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer10.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer10.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer10.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer10.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer10.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer10.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer10.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer10.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer10.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer10.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spArticulos.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer10
        Me.spArticulos.HorizontalScrollBar.TabIndex = 2
        Me.spArticulos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spArticulos.Location = New System.Drawing.Point(5, 262)
        Me.spArticulos.Name = "spArticulos"
        Me.spArticulos.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spArticulos_Sheet1})
        Me.spArticulos.Size = New System.Drawing.Size(505, 224)
        Me.spArticulos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        Me.spArticulos.TabIndex = 26
        Me.spArticulos.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spArticulos.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer11.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer11.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer11.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer11.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer11.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer11.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer11.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer11.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer11.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer11.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer11.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spArticulos.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer11
        Me.spArticulos.VerticalScrollBar.TabIndex = 3
        Me.spArticulos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spArticulos.Visible = False
        '
        'spArticulos_Sheet1
        '
        Me.spArticulos_Sheet1.Reset()
        spArticulos_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spArticulos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spArticulos_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spArticulos_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spArticulos_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spArticulos_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerSeashell"
        Me.spArticulos_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spArticulos_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spArticulos_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spArticulos_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell"
        Me.spArticulos_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spArticulos_Sheet1.SheetCornerStyle.Parent = "CornerSeashell"
        Me.spArticulos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'spVarios
        '
        Me.spVarios.AccessibleDescription = ""
        Me.spVarios.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spVarios.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spVarios.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer12.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer12.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer12.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer12.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer12.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer12.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer12.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer12.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer12.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer12.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer12.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spVarios.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer12
        Me.spVarios.HorizontalScrollBar.TabIndex = 2
        Me.spVarios.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spVarios.Location = New System.Drawing.Point(517, 262)
        Me.spVarios.Name = "spVarios"
        Me.spVarios.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spVarios_Sheet1})
        Me.spVarios.Size = New System.Drawing.Size(505, 224)
        Me.spVarios.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        Me.spVarios.TabIndex = 27
        Me.spVarios.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spVarios.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer13.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer13.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer13.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer13.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer13.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer13.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer13.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer13.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer13.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer13.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer13.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spVarios.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer13
        Me.spVarios.VerticalScrollBar.TabIndex = 3
        Me.spVarios.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.spVarios.Visible = False
        '
        'spVarios_Sheet1
        '
        Me.spVarios_Sheet1.Reset()
        spVarios_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spVarios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spVarios_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVarios_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spVarios_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVarios_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerSeashell"
        Me.spVarios_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVarios_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spVarios_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVarios_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell"
        Me.spVarios_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVarios_Sheet1.SheetCornerStyle.Parent = "CornerSeashell"
        Me.spVarios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'pnlPie
        '
        Me.pnlPie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPie.BackColor = System.Drawing.Color.Black
        Me.pnlPie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPie.Controls.Add(Me.btnAyuda)
        Me.pnlPie.Controls.Add(Me.lblDescripcionTooltip)
        Me.pnlPie.Controls.Add(Me.btnSalir)
        Me.pnlPie.ForeColor = System.Drawing.Color.White
        Me.pnlPie.Location = New System.Drawing.Point(0, 570)
        Me.pnlPie.Name = "pnlPie"
        Me.pnlPie.Size = New System.Drawing.Size(1034, 60)
        Me.pnlPie.TabIndex = 8
        '
        'btnAyuda
        '
        Me.btnAyuda.BackColor = System.Drawing.Color.White
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
        Me.lblDescripcionTooltip.Location = New System.Drawing.Point(101, 17)
        Me.lblDescripcionTooltip.Name = "lblDescripcionTooltip"
        Me.lblDescripcionTooltip.Size = New System.Drawing.Size(0, 31)
        Me.lblDescripcionTooltip.TabIndex = 4
        '
        'btnSalir
        '
        Me.btnSalir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSalir.BackColor = System.Drawing.Color.White
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
        'pnlEncabezado
        '
        Me.pnlEncabezado.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlEncabezado.BackColor = System.Drawing.Color.Black
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
        Me.Text = "Almacén - Catálogos"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlContenido.ResumeLayout(False)
        Me.pnlCuerpo.ResumeLayout(False)
        Me.pnlMenu.ResumeLayout(False)
        Me.pnlMenu.PerformLayout()
        CType(Me.spCatalogos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spCatalogos_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spAlmacenes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spAlmacenes_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spFamilias, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spFamilias_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spSubFamilias, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spSubFamilias_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spArticulos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spArticulos_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spVarios, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spVarios_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents spAlmacenes As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spAlmacenes_Sheet1 As FarPoint.Win.Spread.SheetView
    Private WithEvents lblEncabezadoArea As System.Windows.Forms.Label
    Private WithEvents lblEncabezadoUsuario As System.Windows.Forms.Label
    Private WithEvents btnEliminar As System.Windows.Forms.Button
    Private WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents lblDescripcionTooltip As System.Windows.Forms.Label
    Private WithEvents spCatalogos As FarPoint.Win.Spread.FpSpread
    Private WithEvents spCatalogos_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents temporizador As System.Windows.Forms.Timer
    Private WithEvents btnAyuda As System.Windows.Forms.Button
    Private WithEvents spFamilias As FarPoint.Win.Spread.FpSpread
    Private WithEvents spFamilias_Sheet1 As FarPoint.Win.Spread.SheetView
    Private WithEvents pnlMenu As System.Windows.Forms.Panel
    Private WithEvents rbtnSubFamilias As System.Windows.Forms.RadioButton
    Private WithEvents rbtnAlmacenes As System.Windows.Forms.RadioButton
    Private WithEvents rbtnFamilias As System.Windows.Forms.RadioButton
    Private WithEvents rbtnArticulos As System.Windows.Forms.RadioButton
    Private WithEvents spSubFamilias As FarPoint.Win.Spread.FpSpread
    Private WithEvents spSubFamilias_Sheet1 As FarPoint.Win.Spread.SheetView
    Private WithEvents spArticulos As FarPoint.Win.Spread.FpSpread
    Private WithEvents spArticulos_Sheet1 As FarPoint.Win.Spread.SheetView
    Private WithEvents spVarios As FarPoint.Win.Spread.FpSpread
    Private WithEvents spVarios_Sheet1 As FarPoint.Win.Spread.SheetView
    Private WithEvents rbtnProveedores As System.Windows.Forms.RadioButton
    Private WithEvents rbtnMonedas As System.Windows.Forms.RadioButton
    Private WithEvents rbtnTiposEntradas As System.Windows.Forms.RadioButton
    Private WithEvents rbtnTiposSalidas As System.Windows.Forms.RadioButton
    Private WithEvents rbtnUnidadesMedidas As System.Windows.Forms.RadioButton
    Private WithEvents rbtnTiposCambios As System.Windows.Forms.RadioButton
    Private WithEvents rbtnClientes As System.Windows.Forms.RadioButton
End Class
