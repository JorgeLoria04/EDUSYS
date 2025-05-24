namespace EDUSYS
{
    partial class frmUsuarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsuarios));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.rolesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eDUSYSDataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eDUSYSDataSet1 = new EDUSYS.EDUSYSDataSet1();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lstUsuarios = new System.Windows.Forms.ListView();
            this.Usuario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Rol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminarUS = new System.Windows.Forms.Button();
            this.btnEditarUS = new System.Windows.Forms.Button();
            this.btnAgregarUS = new System.Windows.Forms.Button();
            this.btnConsultarUS = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtNuevoUsuario = new System.Windows.Forms.TextBox();
            this.lblIdentificacion = new System.Windows.Forms.Label();
            this.lblPrimerNombre = new System.Windows.Forms.Label();
            this.lblEstudiantes = new System.Windows.Forms.Label();
            this.rolesTableAdapter = new EDUSYS.EDUSYSDataSet1TableAdapters.RolesTableAdapter();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rolesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eDUSYSDataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eDUSYSDataSet1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.panel1.Controls.Add(this.cmbRol);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.btnLimpiar);
            this.panel1.Controls.Add(this.btnEliminarUS);
            this.panel1.Controls.Add(this.btnEditarUS);
            this.panel1.Controls.Add(this.btnAgregarUS);
            this.panel1.Controls.Add(this.btnConsultarUS);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.txtNuevoUsuario);
            this.panel1.Controls.Add(this.lblIdentificacion);
            this.panel1.Controls.Add(this.lblPrimerNombre);
            this.panel1.Controls.Add(this.lblEstudiantes);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1080, 614);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cmbRol
            // 
            this.cmbRol.DataSource = this.rolesBindingSource;
            this.cmbRol.DisplayMember = "Nombre_Rol";
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Location = new System.Drawing.Point(219, 282);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(200, 21);
            this.cmbRol.TabIndex = 51;
            this.cmbRol.ValueMember = "Nombre_Rol";
            // 
            // rolesBindingSource
            // 
            this.rolesBindingSource.DataMember = "Roles";
            this.rolesBindingSource.DataSource = this.eDUSYSDataSet1BindingSource;
            // 
            // eDUSYSDataSet1BindingSource
            // 
            this.eDUSYSDataSet1BindingSource.DataSource = this.eDUSYSDataSet1;
            this.eDUSYSDataSet1BindingSource.Position = 0;
            // 
            // eDUSYSDataSet1
            // 
            this.eDUSYSDataSet1.DataSetName = "EDUSYSDataSet1";
            this.eDUSYSDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(123, 279);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 21);
            this.label2.TabIndex = 50;
            this.label2.Text = "ROL:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(204, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 21);
            this.label1.TabIndex = 49;
            this.label1.Text = "Información de usuario";
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel3.Controls.Add(this.lstUsuarios);
            this.panel3.Location = new System.Drawing.Point(621, 78);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(431, 509);
            this.panel3.TabIndex = 48;
            // 
            // lstUsuarios
            // 
            this.lstUsuarios.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lstUsuarios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Usuario,
            this.Rol});
            this.lstUsuarios.FullRowSelect = true;
            this.lstUsuarios.HideSelection = false;
            this.lstUsuarios.Location = new System.Drawing.Point(3, 3);
            this.lstUsuarios.Name = "lstUsuarios";
            this.lstUsuarios.Size = new System.Drawing.Size(428, 506);
            this.lstUsuarios.TabIndex = 47;
            this.lstUsuarios.UseCompatibleStateImageBehavior = false;
            this.lstUsuarios.View = System.Windows.Forms.View.Details;
            this.lstUsuarios.SelectedIndexChanged += new System.EventHandler(this.lstUsuarios_SelectedIndexChanged);
            // 
            // Usuario
            // 
            this.Usuario.Text = "Usuario";
            this.Usuario.Width = 214;
            // 
            // Rol
            // 
            this.Rol.Text = "Rol";
            this.Rol.Width = 209;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(80)))), ((int)(((byte)(205)))));
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.Image")));
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(307, 423);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(156, 33);
            this.btnLimpiar.TabIndex = 46;
            this.btnLimpiar.Text = "LIMPIAR";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnEliminarUS
            // 
            this.btnEliminarUS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(80)))), ((int)(((byte)(205)))));
            this.btnEliminarUS.FlatAppearance.BorderSize = 0;
            this.btnEliminarUS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarUS.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarUS.ForeColor = System.Drawing.Color.White;
            this.btnEliminarUS.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarUS.Image")));
            this.btnEliminarUS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminarUS.Location = new System.Drawing.Point(127, 423);
            this.btnEliminarUS.Name = "btnEliminarUS";
            this.btnEliminarUS.Size = new System.Drawing.Size(156, 33);
            this.btnEliminarUS.TabIndex = 45;
            this.btnEliminarUS.Text = "ELIMINAR";
            this.btnEliminarUS.UseVisualStyleBackColor = false;
            this.btnEliminarUS.Click += new System.EventHandler(this.btnEliminarUS_Click);
            // 
            // btnEditarUS
            // 
            this.btnEditarUS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(80)))), ((int)(((byte)(205)))));
            this.btnEditarUS.FlatAppearance.BorderSize = 0;
            this.btnEditarUS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarUS.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarUS.ForeColor = System.Drawing.Color.White;
            this.btnEditarUS.Image = ((System.Drawing.Image)(resources.GetObject("btnEditarUS.Image")));
            this.btnEditarUS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditarUS.Location = new System.Drawing.Point(381, 368);
            this.btnEditarUS.Name = "btnEditarUS";
            this.btnEditarUS.Size = new System.Drawing.Size(156, 33);
            this.btnEditarUS.TabIndex = 44;
            this.btnEditarUS.Text = "MODIFICAR";
            this.btnEditarUS.UseVisualStyleBackColor = false;
            this.btnEditarUS.Click += new System.EventHandler(this.btnEditarUS_Click);
            // 
            // btnAgregarUS
            // 
            this.btnAgregarUS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(80)))), ((int)(((byte)(205)))));
            this.btnAgregarUS.FlatAppearance.BorderSize = 0;
            this.btnAgregarUS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarUS.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarUS.ForeColor = System.Drawing.Color.White;
            this.btnAgregarUS.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarUS.Image")));
            this.btnAgregarUS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarUS.Location = new System.Drawing.Point(219, 368);
            this.btnAgregarUS.Name = "btnAgregarUS";
            this.btnAgregarUS.Size = new System.Drawing.Size(156, 33);
            this.btnAgregarUS.TabIndex = 43;
            this.btnAgregarUS.Text = "AGREGAR";
            this.btnAgregarUS.UseVisualStyleBackColor = false;
            this.btnAgregarUS.Click += new System.EventHandler(this.btnAgregarUS_Click);
            // 
            // btnConsultarUS
            // 
            this.btnConsultarUS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(80)))), ((int)(((byte)(205)))));
            this.btnConsultarUS.FlatAppearance.BorderSize = 0;
            this.btnConsultarUS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarUS.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultarUS.ForeColor = System.Drawing.Color.White;
            this.btnConsultarUS.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultarUS.Image")));
            this.btnConsultarUS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultarUS.Location = new System.Drawing.Point(57, 368);
            this.btnConsultarUS.Name = "btnConsultarUS";
            this.btnConsultarUS.Size = new System.Drawing.Size(156, 33);
            this.btnConsultarUS.TabIndex = 42;
            this.btnConsultarUS.Text = "CONSULTAR";
            this.btnConsultarUS.UseVisualStyleBackColor = false;
            this.btnConsultarUS.Click += new System.EventHandler(this.btnConsultarUS_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(219, 224);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 37;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtNuevoUsuario
            // 
            this.txtNuevoUsuario.Location = new System.Drawing.Point(219, 171);
            this.txtNuevoUsuario.Name = "txtNuevoUsuario";
            this.txtNuevoUsuario.Size = new System.Drawing.Size(200, 20);
            this.txtNuevoUsuario.TabIndex = 34;
            // 
            // lblIdentificacion
            // 
            this.lblIdentificacion.AutoSize = true;
            this.lblIdentificacion.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdentificacion.ForeColor = System.Drawing.Color.White;
            this.lblIdentificacion.Location = new System.Drawing.Point(86, 168);
            this.lblIdentificacion.Name = "lblIdentificacion";
            this.lblIdentificacion.Size = new System.Drawing.Size(80, 21);
            this.lblIdentificacion.TabIndex = 26;
            this.lblIdentificacion.Text = "USUARIO:";
            // 
            // lblPrimerNombre
            // 
            this.lblPrimerNombre.AutoSize = true;
            this.lblPrimerNombre.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrimerNombre.ForeColor = System.Drawing.Color.White;
            this.lblPrimerNombre.Location = new System.Drawing.Point(52, 221);
            this.lblPrimerNombre.Name = "lblPrimerNombre";
            this.lblPrimerNombre.Size = new System.Drawing.Size(114, 21);
            this.lblPrimerNombre.TabIndex = 30;
            this.lblPrimerNombre.Text = "CONTRASEÑA:";
            // 
            // lblEstudiantes
            // 
            this.lblEstudiantes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblEstudiantes.AutoSize = true;
            this.lblEstudiantes.Font = new System.Drawing.Font("Ebrima", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstudiantes.ForeColor = System.Drawing.Color.White;
            this.lblEstudiantes.Location = new System.Drawing.Point(360, 9);
            this.lblEstudiantes.Name = "lblEstudiantes";
            this.lblEstudiantes.Size = new System.Drawing.Size(310, 32);
            this.lblEstudiantes.TabIndex = 16;
            this.lblEstudiantes.Text = "Mantenimiento de Usuarios";
            // 
            // rolesTableAdapter
            // 
            this.rolesTableAdapter.ClearBeforeFill = true;
            // 
            // frmUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.ClientSize = new System.Drawing.Size(1080, 615);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUsuarios";
            this.Text = "EDUSYS";
            this.Load += new System.EventHandler(this.frmUsuarios_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rolesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eDUSYSDataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eDUSYSDataSet1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblEstudiantes;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtNuevoUsuario;
        private System.Windows.Forms.Label lblIdentificacion;
        private System.Windows.Forms.Label lblPrimerNombre;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminarUS;
        private System.Windows.Forms.Button btnEditarUS;
        private System.Windows.Forms.Button btnAgregarUS;
        private System.Windows.Forms.Button btnConsultarUS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstUsuarios;
        private System.Windows.Forms.ColumnHeader Usuario;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.ColumnHeader Rol;
        private System.Windows.Forms.BindingSource eDUSYSDataSet1BindingSource;
        private EDUSYSDataSet1 eDUSYSDataSet1;
        private System.Windows.Forms.BindingSource rolesBindingSource;
        private EDUSYSDataSet1TableAdapters.RolesTableAdapter rolesTableAdapter;
    }
}