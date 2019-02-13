using System;

using System.IO;
using System.Windows.Forms;

namespace YkzWorkHelper
{
    public partial class FormPrincipal : Form
    {
        FileSystemWatcher fileWatcher = new FileSystemWatcher();

        DataGridView dgv = new DataGridView();
        DataGridViewTextBoxColumn nombre;
        DataGridViewTextBoxColumn ruta;


        /// <summary>
        /// Método constructor que no toma ningún argumento.
        /// </summary>
        public FormPrincipal()
        {
            InitializeComponent();

            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ruta = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre,
            this.ruta});
        }

        private void event_ClickContextHandling(object sender, ToolStripItemClickedEventArgs args)
        {
            switch (args.ClickedItem.Text)
            {
                case "":
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Método Load de la clase FormPrincipal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            AyudanteDeTrabajo.Visible = true;
            AyudanteDeTrabajo.BalloonTipClicked += new EventHandler(notifyIcon_BalloonTipClicked);

            fileWatcher.Path = @"D:\Logs";
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Filter = "*.*";
            fileWatcher.Changed += new FileSystemEventHandler(OnChanged);
            fileWatcher.EnableRaisingEvents = true;

            if (!File.Exists(Environment.CurrentDirectory + "\\registros.bin"))
                File.Create(Environment.CurrentDirectory + "\\registros.bin");

            dgv.Rows.Clear();
            string file = Environment.CurrentDirectory + "\\registros.bin";

            using (BinaryReader bw = new BinaryReader(File.Open(file, FileMode.Open)))
            {
                int n = bw.ReadInt32();
                int m = bw.ReadInt32();
                int counter = 0;
                string titulo = string.Empty;
                string ruta = string.Empty;

                for (int i = 0; i < m; ++i)
                {
                    dgv.Rows.Add();

                    if (counter != 0)
                        Vista.accesosDirectos.TryAdd(titulo, ruta);

                    for (int j = 0; j < n; ++j)
                    {
                        if (bw.ReadBoolean())
                        {
                            counter++;
                            var value = bw.ReadString();
                            dgv.Rows[i].Cells[j].Value = value;

                            if (counter % 2 != 0)
                            {
                                titulo = value;
                            }
                            else
                            {
                                ruta = value;
                            }

                        }
                        else bw.ReadBoolean();
                    }
                }
            }
        }

        /// <summary>
        /// Método encargado de capturar el evento, filtrar el tipo de archivo modificado y realizar una acción.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            string strFileExt = Path.GetExtension(e.FullPath);

            if (!e.FullPath.Contains("carcasa"))
            {
                if (strFileExt == ".err")
                    AyudanteDeTrabajo.ShowBalloonTip(2000, "Ayudante de trabajo", "Se registraron nuevos logs con error.", ToolTipIcon.Error);

                if (strFileExt == ".log")
                    AyudanteDeTrabajo.ShowBalloonTip(2000, "Ayudante de trabajo", "Se registraron nuevos logs de información.", ToolTipIcon.Info);
            }
        }

        void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"D:\Logs");
        }

        #region Eventos del ToolStripMenu
        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var configForm = new Configuracion();
            configForm.Show();
            configForm.FormClosed += ConfigForm_FormClosed;
        }

        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Actualizo el menu.

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void nSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\NS_Librerias\Librerias_NS\NS_Parametros.ini");
        }

        private void sACToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\NS_Librerias\Librerias_SAC\SAC_Parametros.ini");
        }
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"D:\Logs");
        }

        private void limpiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(@"D:\Logs");

            // Files clean.
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            // Directories clean.
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            AyudanteDeTrabajo.ShowBalloonTip(2000, "Ayudante de trabajo", "Carpeta de logs limpiada satisfactoriamente.", ToolTipIcon.Info);
        }

        #endregion

        private void AyudanteDeTrabajo_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void AyudanteDeTrabajo_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}