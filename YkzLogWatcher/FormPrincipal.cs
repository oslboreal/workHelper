using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

            this.nombre = new DataGridViewTextBoxColumn();
            this.ruta = new DataGridViewTextBoxColumn();

            this.dgv.Columns.AddRange(new DataGridViewColumn[] {
            this.nombre,
            this.ruta});
        }

        /// <summary>
        /// Método de carga del formulario principal.
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

            accesosDirectosToolStripMenuItem.DropDownItemClicked += contextMenuStrip1_ItemClicked;

            ActualizarColeccionMenu();
            ActualizarAccesosDirectos();
            ActualizarFiltros();
        }

        /// <summary>
        /// Método encargado de capturar el evento, filtrar el tipo de archivo modificado y realizar una acción.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            string file = e.FullPath;
            string strFileExt = Path.GetExtension(file);

            bool filtrado = false;
            foreach (var item in Vista.filtros)
            {
                if (file.Contains(item.Key))
                    if (item.Value)
                        filtrado = true;
            }

            if (!filtrado)
            {
                if (strFileExt == ".err")
                    AyudanteDeTrabajo.ShowBalloonTip(2000, "Ayudante de trabajo", "Se registraron nuevos logs con error.", ToolTipIcon.Error);

                if (strFileExt == ".log")
                    AyudanteDeTrabajo.ShowBalloonTip(2000, "Ayudante de trabajo", "Se registraron nuevos logs de información.", ToolTipIcon.Info);
            }
        }

        /// <summary>
        /// Método encargado de modificar el accesosDirectosToolStripMenuItem
        /// </summary>
        private void ActualizarAccesosDirectos()
        {
            accesosDirectosToolStripMenuItem.DropDownItems.Clear();

            foreach (var item in Vista.accesosDirectos)
                this.accesosDirectosToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(item.Key));
        }


        /// <summary>
        /// Método encargado de actualizar la vista.
        /// </summary>
        private void ActualizarColeccionMenu()
        {
            Vista.accesosDirectos.Clear();

            if (!File.Exists(Environment.CurrentDirectory + "\\registros.json"))
                File.Create(Environment.CurrentDirectory + "\\registros.json").Dispose();

            List<Registro> registros = JsonConvert.DeserializeObject<List<Registro>>(File.ReadAllText(Environment.CurrentDirectory + "\\registros.json"));

            foreach (var item in registros)
            {
                Vista.accesosDirectos.TryAdd(item.Nombre, item.Ruta);
            }
        }

        private void ActualizarFiltros()
        {
            Vista.filtros.Clear();

            if (!File.Exists(Environment.CurrentDirectory + "\\filtros.json"))
                File.Create(Environment.CurrentDirectory + "\\filtros.json").Dispose();

            List<Filtro> filtros = JsonConvert.DeserializeObject<List<Filtro>>(File.ReadAllText(Environment.CurrentDirectory + "\\filtros.json"));

            foreach (var item in filtros)
                Vista.filtros.TryAdd(item.Palabra, item.Filtrar);
        }

        #region Eventos UI
        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ActualizarColeccionMenu();
            ActualizarAccesosDirectos();
            ActualizarFiltros();
        }

        void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"D:\Logs");
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string ruta;
            bool obtenida = Vista.accesosDirectos.TryGetValue(e.ClickedItem.Text, out ruta);
            try
            {
                if (obtenida)
                    System.Diagnostics.Process.Start(ruta);
            }
            catch (Exception)
            {
                this.AyudanteDeTrabajo.ShowBalloonTip(2000, "Ayudante de trabajo", $"La ruta {ruta} no existe.", ToolTipIcon.Error);
            }

        }

        private void abrirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"D:\Logs");
        }

        private void limpiarToolStripMenuItem_Click_1(object sender, EventArgs e)
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

        private void sACToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\NS_Librerias\Librerias_SAC\SAC_Parametros.ini");
        }

        private void nSToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\NS_Librerias\Librerias_NS\NS_Parametros.ini");
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void configuraciónToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var configForm = new Configuracion();
            configForm.Show();
            configForm.FormClosed += ConfigForm_FormClosed;
        }
        #endregion
    }
}