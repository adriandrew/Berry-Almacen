namespace Escritorio
{
    partial class AdministrarDirectorios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdministrarDirectorios));
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer1 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer2 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblEncabezadoUsuario = new System.Windows.Forms.Label();
            this.lblEncabezadoDirectorio = new System.Windows.Forms.Label();
            this.lblEncabezadoPrograma = new System.Windows.Forms.Label();
            this.pnlPie = new System.Windows.Forms.Panel();
            this.lblDescripcionTooltip = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlCuerpo = new System.Windows.Forms.Panel();
            this.spDirectorios = new FarPoint.Win.Spread.FpSpread();
            this.spDirectorios_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnlEncabezado.SuspendLayout();
            this.pnlPie.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlCuerpo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spDirectorios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDirectorios_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlEncabezado.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pnlEncabezado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEncabezado.Controls.Add(this.lblEncabezadoUsuario);
            this.pnlEncabezado.Controls.Add(this.lblEncabezadoDirectorio);
            this.pnlEncabezado.Controls.Add(this.lblEncabezadoPrograma);
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(1034, 75);
            this.pnlEncabezado.TabIndex = 15;
            this.pnlEncabezado.MouseEnter += new System.EventHandler(this.pnlEncabezado_MouseEnter);
            // 
            // lblEncabezadoUsuario
            // 
            this.lblEncabezadoUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEncabezadoUsuario.AutoSize = true;
            this.lblEncabezadoUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncabezadoUsuario.ForeColor = System.Drawing.Color.White;
            this.lblEncabezadoUsuario.Location = new System.Drawing.Point(600, 37);
            this.lblEncabezadoUsuario.Name = "lblEncabezadoUsuario";
            this.lblEncabezadoUsuario.Size = new System.Drawing.Size(0, 33);
            this.lblEncabezadoUsuario.TabIndex = 6;
            // 
            // lblEncabezadoDirectorio
            // 
            this.lblEncabezadoDirectorio.AutoSize = true;
            this.lblEncabezadoDirectorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncabezadoDirectorio.ForeColor = System.Drawing.Color.White;
            this.lblEncabezadoDirectorio.Location = new System.Drawing.Point(12, 35);
            this.lblEncabezadoDirectorio.Name = "lblEncabezadoDirectorio";
            this.lblEncabezadoDirectorio.Size = new System.Drawing.Size(0, 33);
            this.lblEncabezadoDirectorio.TabIndex = 1;
            // 
            // lblEncabezadoPrograma
            // 
            this.lblEncabezadoPrograma.AutoSize = true;
            this.lblEncabezadoPrograma.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncabezadoPrograma.ForeColor = System.Drawing.Color.White;
            this.lblEncabezadoPrograma.Location = new System.Drawing.Point(12, 0);
            this.lblEncabezadoPrograma.Name = "lblEncabezadoPrograma";
            this.lblEncabezadoPrograma.Size = new System.Drawing.Size(0, 33);
            this.lblEncabezadoPrograma.TabIndex = 0;
            // 
            // pnlPie
            // 
            this.pnlPie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPie.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pnlPie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPie.Controls.Add(this.lblDescripcionTooltip);
            this.pnlPie.Controls.Add(this.btnSalir);
            this.pnlPie.Location = new System.Drawing.Point(0, 571);
            this.pnlPie.Name = "pnlPie";
            this.pnlPie.Size = new System.Drawing.Size(1034, 60);
            this.pnlPie.TabIndex = 14;
            this.pnlPie.MouseEnter += new System.EventHandler(this.pnlPie_MouseEnter);
            // 
            // lblDescripcionTooltip
            // 
            this.lblDescripcionTooltip.AutoSize = true;
            this.lblDescripcionTooltip.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionTooltip.ForeColor = System.Drawing.Color.White;
            this.lblDescripcionTooltip.Location = new System.Drawing.Point(101, 17);
            this.lblDescripcionTooltip.Name = "lblDescripcionTooltip";
            this.lblDescripcionTooltip.Size = new System.Drawing.Size(0, 31);
            this.lblDescripcionTooltip.TabIndex = 5;
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.Color.White;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSalir.FlatAppearance.BorderSize = 3;
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.Location = new System.Drawing.Point(972, -1);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(0);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(60, 60);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            this.btnSalir.MouseEnter += new System.EventHandler(this.btnSalir_MouseEnter);
            // 
            // pnlContenido
            // 
            this.pnlContenido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlContenido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlContenido.Controls.Add(this.pnlEncabezado);
            this.pnlContenido.Controls.Add(this.pnlCuerpo);
            this.pnlContenido.Controls.Add(this.pnlPie);
            this.pnlContenido.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlContenido.Location = new System.Drawing.Point(0, 0);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(1038, 634);
            this.pnlContenido.TabIndex = 3;
            this.pnlContenido.MouseEnter += new System.EventHandler(this.pnlContenido_MouseEnter);
            // 
            // pnlCuerpo
            // 
            this.pnlCuerpo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCuerpo.AutoScroll = true;
            this.pnlCuerpo.BackColor = System.Drawing.Color.Transparent;
            this.pnlCuerpo.Controls.Add(this.spDirectorios);
            this.pnlCuerpo.Location = new System.Drawing.Point(0, 78);
            this.pnlCuerpo.Name = "pnlCuerpo";
            this.pnlCuerpo.Size = new System.Drawing.Size(1034, 490);
            this.pnlCuerpo.TabIndex = 12;
            this.pnlCuerpo.MouseEnter += new System.EventHandler(this.pnlCuerpo_MouseEnter);
            // 
            // spDirectorios
            // 
            this.spDirectorios.AccessibleDescription = "";
            this.spDirectorios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spDirectorios.HorizontalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.spDirectorios.HorizontalScrollBar.Name = "";
            enhancedScrollBarRenderer1.ArrowColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ButtonBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.ButtonBorderColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer1.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer1.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer1.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer1.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray;
            this.spDirectorios.HorizontalScrollBar.Renderer = enhancedScrollBarRenderer1;
            this.spDirectorios.HorizontalScrollBar.TabIndex = 2;
            this.spDirectorios.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spDirectorios.Location = new System.Drawing.Point(5, 0);
            this.spDirectorios.Name = "spDirectorios";
            this.spDirectorios.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spDirectorios_Sheet1});
            this.spDirectorios.Size = new System.Drawing.Size(1024, 487);
            this.spDirectorios.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell;
            this.spDirectorios.TabIndex = 4;
            this.spDirectorios.VerticalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.spDirectorios.VerticalScrollBar.Name = "";
            enhancedScrollBarRenderer2.ArrowColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ButtonBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.ButtonBorderColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer2.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer2.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer2.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer2.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray;
            this.spDirectorios.VerticalScrollBar.Renderer = enhancedScrollBarRenderer2;
            this.spDirectorios.VerticalScrollBar.TabIndex = 3;
            this.spDirectorios.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spDirectorios.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.spDirectorios_CellClick);
            this.spDirectorios.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.spDirectorios_CellDoubleClick);
            this.spDirectorios.MouseEnter += new System.EventHandler(this.spDirectorios_MouseEnter);
            // 
            // spDirectorios_Sheet1
            // 
            this.spDirectorios_Sheet1.Reset();
            spDirectorios_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spDirectorios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spDirectorios_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.spDirectorios_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderSeashell";
            this.spDirectorios_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.spDirectorios_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerSeashell";
            this.spDirectorios_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.spDirectorios_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell";
            this.spDirectorios_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.spDirectorios_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell";
            this.spDirectorios_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.spDirectorios_Sheet1.SheetCornerStyle.Parent = "CornerSeashell";
            this.spDirectorios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // AdministrarDirectorios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1034, 631);
            this.Controls.Add(this.pnlContenido);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdministrarDirectorios";
            this.Text = "Directorios";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdministrarDirectorios_FormClosed);
            this.Load += new System.EventHandler(this.AdministrarDirectorios_Load);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlPie.ResumeLayout(false);
            this.pnlPie.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlCuerpo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spDirectorios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDirectorios_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        public System.Windows.Forms.Label lblEncabezadoDirectorio;
        private System.Windows.Forms.Label lblEncabezadoPrograma;
        private System.Windows.Forms.Panel pnlPie;
        internal System.Windows.Forms.Label lblDescripcionTooltip;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlCuerpo;
        private FarPoint.Win.Spread.FpSpread spDirectorios;
        private FarPoint.Win.Spread.SheetView spDirectorios_Sheet1;
        private System.Windows.Forms.Label lblEncabezadoUsuario;


    }
}