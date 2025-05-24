namespace EDUSYS.Vistas
{
    partial class frmRolesyPermisos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRolesyPermisos));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRol = new System.Windows.Forms.TextBox();
            this.btnEliminarRol = new System.Windows.Forms.Button();
            this.btnAgregarRol = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lstPermisos = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPermiso = new System.Windows.Forms.TextBox();
            this.btnEliminarPermiso = new System.Windows.Forms.Button();
            this.btnAgregarPermiso = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.clbPermisos = new System.Windows.Forms.CheckedListBox();
            this.btnGuardarPermisos = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbRoles = new System.Windows.Forms.ComboBox();
            this.rolesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eDUSYSDataSet1 = new EDUSYS.EDUSYSDataSet1();
            this.label2 = new System.Windows.Forms.Label();
            this.eDUSYSDataSet = new EDUSYS.EDUSYSDataSet();
            this.eDUSYSDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rolesTableAdapter = new EDUSYS.EDUSYSDataSet1TableAdapters.RolesTableAdapter();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rolesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eDUSYSDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eDUSYSDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eDUSYSDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(80)))), ((int)(((byte)(205)))));
            this.panel1.Controls.Add(this.lstRoles);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtRol);
            this.panel1.Controls.Add(this.btnEliminarRol);
            this.panel1.Controls.Add(this.btnAgregarRol);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(17, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 539);
            this.panel1.TabIndex = 0;
            // 
            // lstRoles
            // 
            this.lstRoles.FormattingEnabled = true;
            this.lstRoles.Location = new System.Drawing.Point(14, 34);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(277, 303);
            this.lstRoles.TabIndex = 6;
            this.lstRoles.SelectedIndexChanged += new System.EventHandler(this.lstRoles_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(15, 377);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 21);
            this.label7.TabIndex = 5;
            this.label7.Text = "Nombre de Rol:";
            // 
            // txtRol
            // 
            this.txtRol.Location = new System.Drawing.Point(140, 378);
            this.txtRol.Name = "txtRol";
            this.txtRol.Size = new System.Drawing.Size(151, 20);
            this.txtRol.TabIndex = 4;
            // 
            // btnEliminarRol
            // 
            this.btnEliminarRol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.btnEliminarRol.FlatAppearance.BorderSize = 0;
            this.btnEliminarRol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarRol.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarRol.ForeColor = System.Drawing.Color.White;
            this.btnEliminarRol.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarRol.Image")));
            this.btnEliminarRol.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminarRol.Location = new System.Drawing.Point(74, 484);
            this.btnEliminarRol.Name = "btnEliminarRol";
            this.btnEliminarRol.Size = new System.Drawing.Size(151, 41);
            this.btnEliminarRol.TabIndex = 3;
            this.btnEliminarRol.Text = "Eliminar";
            this.btnEliminarRol.UseVisualStyleBackColor = false;
            this.btnEliminarRol.Click += new System.EventHandler(this.btnEliminarRol_Click);
            // 
            // btnAgregarRol
            // 
            this.btnAgregarRol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.btnAgregarRol.FlatAppearance.BorderSize = 0;
            this.btnAgregarRol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarRol.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarRol.ForeColor = System.Drawing.Color.White;
            this.btnAgregarRol.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarRol.Image")));
            this.btnAgregarRol.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarRol.Location = new System.Drawing.Point(74, 420);
            this.btnAgregarRol.Name = "btnAgregarRol";
            this.btnAgregarRol.Size = new System.Drawing.Size(151, 41);
            this.btnAgregarRol.TabIndex = 2;
            this.btnAgregarRol.Text = "Añadir";
            this.btnAgregarRol.UseVisualStyleBackColor = false;
            this.btnAgregarRol.Click += new System.EventHandler(this.btnAgregarRol_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(86, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gestión de Roles";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(80)))), ((int)(((byte)(205)))));
            this.panel2.Controls.Add(this.lstPermisos);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtPermiso);
            this.panel2.Controls.Add(this.btnEliminarPermiso);
            this.panel2.Controls.Add(this.btnAgregarPermiso);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(362, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(305, 539);
            this.panel2.TabIndex = 1;
            // 
            // lstPermisos
            // 
            this.lstPermisos.FormattingEnabled = true;
            this.lstPermisos.Location = new System.Drawing.Point(15, 34);
            this.lstPermisos.Name = "lstPermisos";
            this.lstPermisos.Size = new System.Drawing.Size(277, 303);
            this.lstPermisos.TabIndex = 9;
            this.lstPermisos.SelectedIndexChanged += new System.EventHandler(this.lstPermisos_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(5, 377);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Nombre de Permiso:";
            // 
            // txtPermiso
            // 
            this.txtPermiso.Location = new System.Drawing.Point(141, 374);
            this.txtPermiso.Name = "txtPermiso";
            this.txtPermiso.Size = new System.Drawing.Size(151, 20);
            this.txtPermiso.TabIndex = 7;
            // 
            // btnEliminarPermiso
            // 
            this.btnEliminarPermiso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.btnEliminarPermiso.FlatAppearance.BorderSize = 0;
            this.btnEliminarPermiso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarPermiso.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarPermiso.ForeColor = System.Drawing.Color.White;
            this.btnEliminarPermiso.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarPermiso.Image")));
            this.btnEliminarPermiso.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminarPermiso.Location = new System.Drawing.Point(74, 484);
            this.btnEliminarPermiso.Name = "btnEliminarPermiso";
            this.btnEliminarPermiso.Size = new System.Drawing.Size(151, 41);
            this.btnEliminarPermiso.TabIndex = 6;
            this.btnEliminarPermiso.Text = "Eliminar";
            this.btnEliminarPermiso.UseVisualStyleBackColor = false;
            this.btnEliminarPermiso.Click += new System.EventHandler(this.btnEliminarPermiso_Click);
            // 
            // btnAgregarPermiso
            // 
            this.btnAgregarPermiso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.btnAgregarPermiso.FlatAppearance.BorderSize = 0;
            this.btnAgregarPermiso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarPermiso.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarPermiso.ForeColor = System.Drawing.Color.White;
            this.btnAgregarPermiso.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarPermiso.Image")));
            this.btnAgregarPermiso.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarPermiso.Location = new System.Drawing.Point(74, 420);
            this.btnAgregarPermiso.Name = "btnAgregarPermiso";
            this.btnAgregarPermiso.Size = new System.Drawing.Size(151, 41);
            this.btnAgregarPermiso.TabIndex = 5;
            this.btnAgregarPermiso.Text = "Añadir";
            this.btnAgregarPermiso.UseVisualStyleBackColor = false;
            this.btnAgregarPermiso.Click += new System.EventHandler(this.btnAgregarPermiso_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(86, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Gestión de Permisos";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(80)))), ((int)(((byte)(205)))));
            this.panel3.Controls.Add(this.clbPermisos);
            this.panel3.Controls.Add(this.btnGuardarPermisos);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.cmbRoles);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(709, 18);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(305, 539);
            this.panel3.TabIndex = 2;
            // 
            // clbPermisos
            // 
            this.clbPermisos.FormattingEnabled = true;
            this.clbPermisos.Location = new System.Drawing.Point(21, 120);
            this.clbPermisos.Name = "clbPermisos";
            this.clbPermisos.Size = new System.Drawing.Size(267, 244);
            this.clbPermisos.TabIndex = 13;
            // 
            // btnGuardarPermisos
            // 
            this.btnGuardarPermisos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.btnGuardarPermisos.FlatAppearance.BorderSize = 0;
            this.btnGuardarPermisos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarPermisos.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarPermisos.ForeColor = System.Drawing.Color.White;
            this.btnGuardarPermisos.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarPermisos.Image")));
            this.btnGuardarPermisos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarPermisos.Location = new System.Drawing.Point(89, 420);
            this.btnGuardarPermisos.Name = "btnGuardarPermisos";
            this.btnGuardarPermisos.Size = new System.Drawing.Size(151, 41);
            this.btnGuardarPermisos.TabIndex = 11;
            this.btnGuardarPermisos.Text = "Guardar";
            this.btnGuardarPermisos.UseVisualStyleBackColor = false;
            this.btnGuardarPermisos.Click += new System.EventHandler(this.btnGuardarPermisos_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(53, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Rol:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbRoles
            // 
            this.cmbRoles.DataSource = this.rolesBindingSource;
            this.cmbRoles.DisplayMember = "Nombre_Rol";
            this.cmbRoles.FormattingEnabled = true;
            this.cmbRoles.Location = new System.Drawing.Point(95, 80);
            this.cmbRoles.Name = "cmbRoles";
            this.cmbRoles.Size = new System.Drawing.Size(151, 21);
            this.cmbRoles.TabIndex = 2;
            this.cmbRoles.SelectedIndexChanged += new System.EventHandler(this.cmbRoles_SelectedIndexChanged);
            // 
            // rolesBindingSource
            // 
            this.rolesBindingSource.DataMember = "Roles";
            this.rolesBindingSource.DataSource = this.eDUSYSDataSet1;
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
            this.label2.Location = new System.Drawing.Point(91, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Asignación de Roles";
            // 
            // eDUSYSDataSet
            // 
            this.eDUSYSDataSet.DataSetName = "EDUSYSDataSet";
            this.eDUSYSDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // eDUSYSDataSetBindingSource
            // 
            this.eDUSYSDataSetBindingSource.DataSource = this.eDUSYSDataSet;
            this.eDUSYSDataSetBindingSource.Position = 0;
            // 
            // rolesTableAdapter
            // 
            this.rolesTableAdapter.ClearBeforeFill = true;
            // 
            // frmRolesyPermisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(105)))));
            this.ClientSize = new System.Drawing.Size(1080, 615);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRolesyPermisos";
            this.Text = "frmRolesyPermisos";
            this.Load += new System.EventHandler(this.frmRolesyPermisos_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rolesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eDUSYSDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eDUSYSDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eDUSYSDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnEliminarRol;
        private System.Windows.Forms.Button btnAgregarRol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEliminarPermiso;
        private System.Windows.Forms.Button btnAgregarPermiso;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbRoles;
        private System.Windows.Forms.Button btnGuardarPermisos;
        private EDUSYSDataSet eDUSYSDataSet;
        private System.Windows.Forms.BindingSource eDUSYSDataSetBindingSource;
        private EDUSYSDataSet1 eDUSYSDataSet1;
        private System.Windows.Forms.BindingSource rolesBindingSource;
        private EDUSYSDataSet1TableAdapters.RolesTableAdapter rolesTableAdapter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRol;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPermiso;
        private System.Windows.Forms.CheckedListBox clbPermisos;
        private System.Windows.Forms.ListBox lstRoles;
        private System.Windows.Forms.ListBox lstPermisos;
    }
}