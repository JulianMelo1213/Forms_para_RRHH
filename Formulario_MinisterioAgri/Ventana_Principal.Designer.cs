namespace Formulario_MinisterioAgri
{
    partial class Ventana_Principal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ventana_Principal));
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.btn_Minimizar = new System.Windows.Forms.PictureBox();
            this.btn_Cerrar = new System.Windows.Forms.PictureBox();
            this.btnSolicitud_Nombramiento = new System.Windows.Forms.PictureBox();
            this.btn_CerrarSesion = new System.Windows.Forms.Button();
            this.btnSolicitud_Cambio = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReajuste = new System.Windows.Forms.PictureBox();
            this.panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Minimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Cerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSolicitud_Nombramiento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSolicitud_Cambio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReajuste)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelSuperior.Controls.Add(this.btn_Minimizar);
            this.panelSuperior.Controls.Add(this.btn_Cerrar);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1259, 37);
            this.panelSuperior.TabIndex = 0;
            this.panelSuperior.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelSuperior_MouseDown);
            // 
            // btn_Minimizar
            // 
            this.btn_Minimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Minimizar.Image = ((System.Drawing.Image)(resources.GetObject("btn_Minimizar.Image")));
            this.btn_Minimizar.Location = new System.Drawing.Point(1173, 2);
            this.btn_Minimizar.Name = "btn_Minimizar";
            this.btn_Minimizar.Size = new System.Drawing.Size(33, 35);
            this.btn_Minimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Minimizar.TabIndex = 21;
            this.btn_Minimizar.TabStop = false;
            this.btn_Minimizar.Click += new System.EventHandler(this.btn_Minimizar_Click);
            // 
            // btn_Cerrar
            // 
            this.btn_Cerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Cerrar.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cerrar.Image")));
            this.btn_Cerrar.Location = new System.Drawing.Point(1214, 2);
            this.btn_Cerrar.Name = "btn_Cerrar";
            this.btn_Cerrar.Size = new System.Drawing.Size(33, 35);
            this.btn_Cerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Cerrar.TabIndex = 20;
            this.btn_Cerrar.TabStop = false;
            this.btn_Cerrar.Click += new System.EventHandler(this.btn_Cerrar_Click);
            // 
            // btnSolicitud_Nombramiento
            // 
            this.btnSolicitud_Nombramiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSolicitud_Nombramiento.Image = ((System.Drawing.Image)(resources.GetObject("btnSolicitud_Nombramiento.Image")));
            this.btnSolicitud_Nombramiento.Location = new System.Drawing.Point(76, 113);
            this.btnSolicitud_Nombramiento.Name = "btnSolicitud_Nombramiento";
            this.btnSolicitud_Nombramiento.Size = new System.Drawing.Size(188, 201);
            this.btnSolicitud_Nombramiento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSolicitud_Nombramiento.TabIndex = 1;
            this.btnSolicitud_Nombramiento.TabStop = false;
            this.btnSolicitud_Nombramiento.Click += new System.EventHandler(this.btnSolicitud_Nombramiento_Click);
            // 
            // btn_CerrarSesion
            // 
            this.btn_CerrarSesion.BackColor = System.Drawing.Color.White;
            this.btn_CerrarSesion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_CerrarSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CerrarSesion.Location = new System.Drawing.Point(0, 379);
            this.btn_CerrarSesion.Name = "btn_CerrarSesion";
            this.btn_CerrarSesion.Size = new System.Drawing.Size(1259, 40);
            this.btn_CerrarSesion.TabIndex = 6;
            this.btn_CerrarSesion.Text = "Cerrar sesión";
            this.btn_CerrarSesion.UseVisualStyleBackColor = false;
            this.btn_CerrarSesion.Click += new System.EventHandler(this.btn_CerrarSesion_Click);
            // 
            // btnSolicitud_Cambio
            // 
            this.btnSolicitud_Cambio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSolicitud_Cambio.Image = ((System.Drawing.Image)(resources.GetObject("btnSolicitud_Cambio.Image")));
            this.btnSolicitud_Cambio.Location = new System.Drawing.Point(489, 56);
            this.btnSolicitud_Cambio.Name = "btnSolicitud_Cambio";
            this.btnSolicitud_Cambio.Size = new System.Drawing.Size(282, 288);
            this.btnSolicitud_Cambio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSolicitud_Cambio.TabIndex = 7;
            this.btnSolicitud_Cambio.TabStop = false;
            this.btnSolicitud_Cambio.Click += new System.EventHandler(this.btnSolicitud_Cambio_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(423, 313);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 29);
            this.label1.TabIndex = 43;
            this.label1.Text = "Solicitud de cambio de designación";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 315);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(301, 29);
            this.label2.TabIndex = 44;
            this.label2.Text = "Solicitud de nombramiento";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(907, 313);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 29);
            this.label3.TabIndex = 46;
            this.label3.Text = "Solicitud de reajuste";
            // 
            // btnReajuste
            // 
            this.btnReajuste.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReajuste.Image = ((System.Drawing.Image)(resources.GetObject("btnReajuste.Image")));
            this.btnReajuste.Location = new System.Drawing.Point(906, 84);
            this.btnReajuste.Name = "btnReajuste";
            this.btnReajuste.Size = new System.Drawing.Size(236, 217);
            this.btnReajuste.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnReajuste.TabIndex = 45;
            this.btnReajuste.TabStop = false;
            this.btnReajuste.Click += new System.EventHandler(this.btnReajuste_Click);
            // 
            // Ventana_Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(1259, 419);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnReajuste);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSolicitud_Cambio);
            this.Controls.Add(this.btn_CerrarSesion);
            this.Controls.Add(this.btnSolicitud_Nombramiento);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Ventana_Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ventana_Principal";
            this.panelSuperior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_Minimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Cerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSolicitud_Nombramiento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSolicitud_Cambio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReajuste)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.PictureBox btn_Minimizar;
        private System.Windows.Forms.PictureBox btn_Cerrar;
        private System.Windows.Forms.PictureBox btnSolicitud_Nombramiento;
        private System.Windows.Forms.Button btn_CerrarSesion;
        private System.Windows.Forms.PictureBox btnSolicitud_Cambio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox btnReajuste;
    }
}