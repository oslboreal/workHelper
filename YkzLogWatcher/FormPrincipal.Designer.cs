namespace YkzWorkHelper
{
    partial class FormPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.AyudanteDeTrabajo = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.logsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limpiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parámetrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sACToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accesosDirectosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AyudanteDeTrabajo
            // 
            this.AyudanteDeTrabajo.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.AyudanteDeTrabajo.ContextMenuStrip = this.contextMenuStrip1;
            this.AyudanteDeTrabajo.Icon = ((System.Drawing.Icon)(resources.GetObject("AyudanteDeTrabajo.Icon")));
            this.AyudanteDeTrabajo.Text = "Ayudante de trabajo";
            this.AyudanteDeTrabajo.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logsToolStripMenuItem,
            this.parámetrosToolStripMenuItem,
            this.accesosDirectosToolStripMenuItem,
            this.configuraciónToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 136);
            this.contextMenuStrip1.Text = "Menú";
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // logsToolStripMenuItem
            // 
            this.logsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.limpiarToolStripMenuItem});
            this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
            this.logsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.logsToolStripMenuItem.Text = "Logs";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.abrirToolStripMenuItem.Text = "Abrir carpeta";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click_1);
            // 
            // limpiarToolStripMenuItem
            // 
            this.limpiarToolStripMenuItem.Name = "limpiarToolStripMenuItem";
            this.limpiarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.limpiarToolStripMenuItem.Text = "Limpiar";
            this.limpiarToolStripMenuItem.Click += new System.EventHandler(this.limpiarToolStripMenuItem_Click_1);
            // 
            // parámetrosToolStripMenuItem
            // 
            this.parámetrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nSToolStripMenuItem,
            this.sACToolStripMenuItem});
            this.parámetrosToolStripMenuItem.Name = "parámetrosToolStripMenuItem";
            this.parámetrosToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.parámetrosToolStripMenuItem.Text = "Parámetros";
            // 
            // nSToolStripMenuItem
            // 
            this.nSToolStripMenuItem.Name = "nSToolStripMenuItem";
            this.nSToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nSToolStripMenuItem.Text = "NS";
            this.nSToolStripMenuItem.Click += new System.EventHandler(this.nSToolStripMenuItem_Click_1);
            // 
            // sACToolStripMenuItem
            // 
            this.sACToolStripMenuItem.Name = "sACToolStripMenuItem";
            this.sACToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sACToolStripMenuItem.Text = "SAC";
            this.sACToolStripMenuItem.Click += new System.EventHandler(this.sACToolStripMenuItem_Click_1);
            // 
            // accesosDirectosToolStripMenuItem
            // 
            this.accesosDirectosToolStripMenuItem.Name = "accesosDirectosToolStripMenuItem";
            this.accesosDirectosToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.accesosDirectosToolStripMenuItem.Text = "Accesos directos";
            // 
            // configuraciónToolStripMenuItem
            // 
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.configuraciónToolStripMenuItem.Text = "Configuracion";
            this.configuraciónToolStripMenuItem.Click += new System.EventHandler(this.configuraciónToolStripMenuItem_Click_1);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(147, 29);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPrincipal";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon AyudanteDeTrabajo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem logsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parámetrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sACToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem limpiarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accesosDirectosToolStripMenuItem;
    }
}

