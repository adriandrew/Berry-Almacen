namespace Escritorio
{
    partial class Principal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.temporizador = new System.Windows.Forms.Timer(this.components);
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlIniciarSesion = new System.Windows.Forms.Panel();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.lblContraseña = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.picUsuario = new System.Windows.Forms.PictureBox();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.pnlPie = new System.Windows.Forms.Panel();
            this.btnRegresarMenu = new System.Windows.Forms.Button();
            this.btnAyuda = new System.Windows.Forms.Button();
            this.lblDescripcionTooltip = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnCambiarDirectorio = new System.Windows.Forms.Button();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblEncabezadoLicencia = new System.Windows.Forms.Label();
            this.lblEncabezadoUsuario = new System.Windows.Forms.Label();
            this.lblEncabezadoDirectorio = new System.Windows.Forms.Label();
            this.lblEncabezadoPrograma = new System.Windows.Forms.Label();
            this.pnlContenido.SuspendLayout();
            this.pnlIniciarSesion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUsuario)).BeginInit();
            this.pnlPie.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // temporizador
            // 
            this.temporizador.Interval = 1;
            this.temporizador.Tick += new System.EventHandler(this.temporizador_Tick);
            // 
            // pnlContenido
            // 
            this.pnlContenido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContenido.BackColor = System.Drawing.Color.DarkSlateGray;
            this.pnlContenido.BackgroundImage = global::PrincipalBerry.Properties.Resources.Logo3;
            this.pnlContenido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlContenido.Controls.Add(this.pnlIniciarSesion);
            this.pnlContenido.Controls.Add(this.pnlMenu);
            this.pnlContenido.Controls.Add(this.pnlPie);
            this.pnlContenido.Controls.Add(this.pnlEncabezado);
            this.pnlContenido.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlContenido.Location = new System.Drawing.Point(0, 0);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(1034, 630);
            this.pnlContenido.TabIndex = 1;
            this.pnlContenido.MouseEnter += new System.EventHandler(this.pnlContenido_MouseEnter);
            // 
            // pnlIniciarSesion
            // 
            this.pnlIniciarSesion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlIniciarSesion.BackColor = System.Drawing.Color.White;
            this.pnlIniciarSesion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlIniciarSesion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlIniciarSesion.Controls.Add(this.txtContraseña);
            this.pnlIniciarSesion.Controls.Add(this.txtUsuario);
            this.pnlIniciarSesion.Controls.Add(this.btnEntrar);
            this.pnlIniciarSesion.Controls.Add(this.lblContraseña);
            this.pnlIniciarSesion.Controls.Add(this.lblUsuario);
            this.pnlIniciarSesion.Controls.Add(this.picUsuario);
            this.pnlIniciarSesion.ForeColor = System.Drawing.Color.Black;
            this.pnlIniciarSesion.Location = new System.Drawing.Point(310, 224);
            this.pnlIniciarSesion.Name = "pnlIniciarSesion";
            this.pnlIniciarSesion.Size = new System.Drawing.Size(422, 206);
            this.pnlIniciarSesion.TabIndex = 6;
            this.pnlIniciarSesion.MouseEnter += new System.EventHandler(this.pnlIniciarSesion_MouseEnter);
            // 
            // txtContraseña
            // 
            this.txtContraseña.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtContraseña.BackColor = System.Drawing.Color.White;
            this.txtContraseña.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContraseña.Location = new System.Drawing.Point(151, 144);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.Size = new System.Drawing.Size(196, 31);
            this.txtContraseña.TabIndex = 22;
            this.txtContraseña.UseSystemPasswordChar = true;
            this.txtContraseña.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContraseña_KeyDown);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUsuario.BackColor = System.Drawing.Color.White;
            this.txtUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(151, 107);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(249, 31);
            this.txtUsuario.TabIndex = 21;
            this.txtUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsuario_KeyDown);
            // 
            // btnEntrar
            // 
            this.btnEntrar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEntrar.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnEntrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntrar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnEntrar.FlatAppearance.BorderSize = 3;
            this.btnEntrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine;
            this.btnEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEntrar.ForeColor = System.Drawing.Color.White;
            this.btnEntrar.Image = ((System.Drawing.Image)(resources.GetObject("btnEntrar.Image")));
            this.btnEntrar.Location = new System.Drawing.Point(353, 142);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(47, 33);
            this.btnEntrar.TabIndex = 23;
            this.btnEntrar.UseVisualStyleBackColor = false;
            this.btnEntrar.Click += new System.EventHandler(this.btnIniciarSesion_Click);
            this.btnEntrar.MouseEnter += new System.EventHandler(this.btnEntrar_MouseEnter);
            // 
            // lblContraseña
            // 
            this.lblContraseña.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblContraseña.AutoSize = true;
            this.lblContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContraseña.ForeColor = System.Drawing.Color.Black;
            this.lblContraseña.Location = new System.Drawing.Point(9, 146);
            this.lblContraseña.Name = "lblContraseña";
            this.lblContraseña.Size = new System.Drawing.Size(140, 25);
            this.lblContraseña.TabIndex = 20;
            this.lblContraseña.Text = "Contraseña:";
            this.lblContraseña.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.Black;
            this.lblUsuario.Location = new System.Drawing.Point(9, 110);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(100, 25);
            this.lblUsuario.TabIndex = 19;
            this.lblUsuario.Text = "Usuario:";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picUsuario
            // 
            this.picUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picUsuario.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picUsuario.BackgroundImage")));
            this.picUsuario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picUsuario.Location = new System.Drawing.Point(190, 22);
            this.picUsuario.Name = "picUsuario";
            this.picUsuario.Size = new System.Drawing.Size(70, 70);
            this.picUsuario.TabIndex = 0;
            this.picUsuario.TabStop = false;
            // 
            // pnlMenu
            // 
            this.pnlMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMenu.AutoScroll = true;
            this.pnlMenu.BackColor = System.Drawing.Color.Transparent;
            this.pnlMenu.Location = new System.Drawing.Point(3, 77);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(1028, 490);
            this.pnlMenu.TabIndex = 9;
            this.pnlMenu.Visible = false;
            this.pnlMenu.MouseEnter += new System.EventHandler(this.pnlMenu_MouseEnter);
            // 
            // pnlPie
            // 
            this.pnlPie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPie.BackColor = System.Drawing.Color.Black;
            this.pnlPie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPie.Controls.Add(this.btnRegresarMenu);
            this.pnlPie.Controls.Add(this.btnAyuda);
            this.pnlPie.Controls.Add(this.lblDescripcionTooltip);
            this.pnlPie.Controls.Add(this.btnSalir);
            this.pnlPie.Controls.Add(this.btnCambiarDirectorio);
            this.pnlPie.ForeColor = System.Drawing.Color.White;
            this.pnlPie.Location = new System.Drawing.Point(0, 570);
            this.pnlPie.Name = "pnlPie";
            this.pnlPie.Size = new System.Drawing.Size(1034, 60);
            this.pnlPie.TabIndex = 8;
            this.pnlPie.MouseEnter += new System.EventHandler(this.pnlPie_MouseEnter);
            // 
            // btnRegresarMenu
            // 
            this.btnRegresarMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegresarMenu.BackColor = System.Drawing.Color.White;
            this.btnRegresarMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresarMenu.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRegresarMenu.FlatAppearance.BorderSize = 3;
            this.btnRegresarMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine;
            this.btnRegresarMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresarMenu.ForeColor = System.Drawing.Color.Black;
            this.btnRegresarMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnRegresarMenu.Image")));
            this.btnRegresarMenu.Location = new System.Drawing.Point(912, 0);
            this.btnRegresarMenu.Margin = new System.Windows.Forms.Padding(0);
            this.btnRegresarMenu.Name = "btnRegresarMenu";
            this.btnRegresarMenu.Size = new System.Drawing.Size(60, 60);
            this.btnRegresarMenu.TabIndex = 6;
            this.btnRegresarMenu.UseVisualStyleBackColor = false;
            this.btnRegresarMenu.Visible = false;
            this.btnRegresarMenu.Click += new System.EventHandler(this.btnRegresarMenu_Click);
            this.btnRegresarMenu.MouseEnter += new System.EventHandler(this.btnRegresarMenu_MouseEnter);
            // 
            // btnAyuda
            // 
            this.btnAyuda.BackColor = System.Drawing.Color.White;
            this.btnAyuda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAyuda.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAyuda.FlatAppearance.BorderSize = 3;
            this.btnAyuda.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine;
            this.btnAyuda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAyuda.ForeColor = System.Drawing.Color.Black;
            this.btnAyuda.Image = ((System.Drawing.Image)(resources.GetObject("btnAyuda.Image")));
            this.btnAyuda.Location = new System.Drawing.Point(0, 0);
            this.btnAyuda.Margin = new System.Windows.Forms.Padding(0);
            this.btnAyuda.Name = "btnAyuda";
            this.btnAyuda.Size = new System.Drawing.Size(60, 60);
            this.btnAyuda.TabIndex = 5;
            this.btnAyuda.UseVisualStyleBackColor = false;
            this.btnAyuda.Click += new System.EventHandler(this.btnAyuda_Click);
            this.btnAyuda.MouseEnter += new System.EventHandler(this.btnAyuda_MouseEnter);
            // 
            // lblDescripcionTooltip
            // 
            this.lblDescripcionTooltip.AutoSize = true;
            this.lblDescripcionTooltip.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionTooltip.ForeColor = System.Drawing.Color.White;
            this.lblDescripcionTooltip.Location = new System.Drawing.Point(135, 17);
            this.lblDescripcionTooltip.Name = "lblDescripcionTooltip";
            this.lblDescripcionTooltip.Size = new System.Drawing.Size(0, 31);
            this.lblDescripcionTooltip.TabIndex = 4;
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
            this.btnSalir.Location = new System.Drawing.Point(972, 0);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(0);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(60, 60);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            this.btnSalir.MouseEnter += new System.EventHandler(this.btnSalir_MouseEnter);
            // 
            // btnCambiarDirectorio
            // 
            this.btnCambiarDirectorio.BackColor = System.Drawing.Color.White;
            this.btnCambiarDirectorio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCambiarDirectorio.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCambiarDirectorio.FlatAppearance.BorderSize = 3;
            this.btnCambiarDirectorio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine;
            this.btnCambiarDirectorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarDirectorio.ForeColor = System.Drawing.Color.Black;
            this.btnCambiarDirectorio.Image = ((System.Drawing.Image)(resources.GetObject("btnCambiarDirectorio.Image")));
            this.btnCambiarDirectorio.Location = new System.Drawing.Point(60, 0);
            this.btnCambiarDirectorio.Margin = new System.Windows.Forms.Padding(0);
            this.btnCambiarDirectorio.Name = "btnCambiarDirectorio";
            this.btnCambiarDirectorio.Size = new System.Drawing.Size(60, 60);
            this.btnCambiarDirectorio.TabIndex = 0;
            this.btnCambiarDirectorio.UseVisualStyleBackColor = false;
            this.btnCambiarDirectorio.Click += new System.EventHandler(this.btnCambiarDirectorio_Click);
            this.btnCambiarDirectorio.MouseEnter += new System.EventHandler(this.btnCambiarDirectorio_MouseEnter);
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlEncabezado.BackColor = System.Drawing.Color.Black;
            this.pnlEncabezado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEncabezado.Controls.Add(this.lblEncabezadoLicencia);
            this.pnlEncabezado.Controls.Add(this.lblEncabezadoUsuario);
            this.pnlEncabezado.Controls.Add(this.lblEncabezadoDirectorio);
            this.pnlEncabezado.Controls.Add(this.lblEncabezadoPrograma);
            this.pnlEncabezado.ForeColor = System.Drawing.Color.White;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(1034, 75);
            this.pnlEncabezado.TabIndex = 7;
            this.pnlEncabezado.MouseEnter += new System.EventHandler(this.pnlEncabezado_MouseEnter);
            // 
            // lblEncabezadoLicencia
            // 
            this.lblEncabezadoLicencia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEncabezadoLicencia.AutoSize = true;
            this.lblEncabezadoLicencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncabezadoLicencia.ForeColor = System.Drawing.Color.White;
            this.lblEncabezadoLicencia.Location = new System.Drawing.Point(600, 0);
            this.lblEncabezadoLicencia.Name = "lblEncabezadoLicencia";
            this.lblEncabezadoLicencia.Size = new System.Drawing.Size(0, 33);
            this.lblEncabezadoLicencia.TabIndex = 5;
            // 
            // lblEncabezadoUsuario
            // 
            this.lblEncabezadoUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEncabezadoUsuario.AutoSize = true;
            this.lblEncabezadoUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncabezadoUsuario.ForeColor = System.Drawing.Color.White;
            this.lblEncabezadoUsuario.Location = new System.Drawing.Point(600, 35);
            this.lblEncabezadoUsuario.Name = "lblEncabezadoUsuario";
            this.lblEncabezadoUsuario.Size = new System.Drawing.Size(0, 33);
            this.lblEncabezadoUsuario.TabIndex = 4;
            // 
            // lblEncabezadoDirectorio
            // 
            this.lblEncabezadoDirectorio.AutoSize = true;
            this.lblEncabezadoDirectorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncabezadoDirectorio.ForeColor = System.Drawing.Color.White;
            this.lblEncabezadoDirectorio.Location = new System.Drawing.Point(10, 35);
            this.lblEncabezadoDirectorio.Name = "lblEncabezadoDirectorio";
            this.lblEncabezadoDirectorio.Size = new System.Drawing.Size(0, 33);
            this.lblEncabezadoDirectorio.TabIndex = 1;
            // 
            // lblEncabezadoPrograma
            // 
            this.lblEncabezadoPrograma.AutoSize = true;
            this.lblEncabezadoPrograma.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncabezadoPrograma.ForeColor = System.Drawing.Color.White;
            this.lblEncabezadoPrograma.Location = new System.Drawing.Point(10, 0);
            this.lblEncabezadoPrograma.Name = "lblEncabezadoPrograma";
            this.lblEncabezadoPrograma.Size = new System.Drawing.Size(0, 33);
            this.lblEncabezadoPrograma.TabIndex = 0;
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1034, 631);
            this.Controls.Add(this.pnlContenido);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Principal";
            this.Text = "Iniciar Sesión y Menú";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Principal_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Principal_FormClosed);
            this.Load += new System.EventHandler(this.Principal_Load);
            this.Shown += new System.EventHandler(this.Principal_Shown);
            this.pnlContenido.ResumeLayout(false);
            this.pnlIniciarSesion.ResumeLayout(false);
            this.pnlIniciarSesion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUsuario)).EndInit();
            this.pnlPie.ResumeLayout(false);
            this.pnlPie.PerformLayout();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCambiarDirectorio;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlIniciarSesion;
        private System.Windows.Forms.PictureBox picUsuario;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.Label lblContraseña;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblEncabezadoPrograma;
        private System.Windows.Forms.Panel pnlPie;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Label lblEncabezadoUsuario;
        public System.Windows.Forms.Label lblEncabezadoDirectorio;
        internal System.Windows.Forms.Label lblDescripcionTooltip;
        internal System.Windows.Forms.Timer temporizador;
        private System.Windows.Forms.Button btnAyuda;
        private System.Windows.Forms.Button btnRegresarMenu;
        private System.Windows.Forms.Label lblEncabezadoLicencia;

    }
}