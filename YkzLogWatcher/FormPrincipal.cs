using Newtonsoft.Json;
using Nosis.Framework.Diagnostico;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace YkzWorkHelper
{
    public partial class FormPrincipal : Form
    {
        FileSystemWatcher fileWatcher = new FileSystemWatcher();
        FileSystemWatcher fileWatcherEquipo = new FileSystemWatcher();

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
            CargarConfiguracion();

            AyudanteDeTrabajo.Visible = true;
            AyudanteDeTrabajo.BalloonTipClicked += new EventHandler(notifyIcon_BalloonTipClicked);

            fileWatcher.Path = @"D:\Logs";
            fileWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
            fileWatcher.Filter = "*.*";

            fileWatcher.Changed += new FileSystemEventHandler(ArchivoModificado);
            fileWatcher.Created += new FileSystemEventHandler(ArchivoCreado);

            fileWatcher.EnableRaisingEvents = true;

            if (Vista.RegistraEquipo)
            {
                fileWatcherEquipo.Path = Vista.RepositorioEquipo;
                fileWatcherEquipo.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
                fileWatcherEquipo.Filter = "*.*";
                fileWatcherEquipo.Changed += new FileSystemEventHandler(ArchivoModificado);
                fileWatcherEquipo.Created += new FileSystemEventHandler(ArchivoCreado);
                fileWatcherEquipo.EnableRaisingEvents = true;
            }

            accesosDirectosToolStripMenuItem.DropDownItemClicked += contextMenuStrip1_ItemClicked;

            ActualizarColeccionMenu();
            ActualizarAccesosDirectos();
            ActualizarFiltros();
        }

        private void ArchivoCreado(object sender, FileSystemEventArgs e)
        {
            bool repositorio = false;
            string file = e.FullPath;
            string strFileExt = Path.GetExtension(file);

            bool filtrado = false;
            foreach (var item in Vista.filtros)
            {
                if (file.Contains(item.Key))
                    if (item.Value)
                        filtrado = true;
            }

            if (Directory.Exists(Vista.RepositorioEquipo))
                if (file.Contains(Vista.RepositorioEquipo))
                    repositorio = true;

            if (!repositorio)
            {
                if (!filtrado)
                {
                    if (Vista.RegistrarErrores && strFileExt == ".err")
                        AyudanteDeTrabajo.ShowBalloonTip(2000, "Vaya, debo decirte algo", "Se creo un nuevo log con error.", ToolTipIcon.Error);

                    if (Vista.RegistrarLogs && strFileExt == ".log")
                        AyudanteDeTrabajo.ShowBalloonTip(2000, "Veo algo de movimiento", "Se creo un nuevo log de información.", ToolTipIcon.Info);
                }
            }
            else
            {
                if (!filtrado && Vista.RegistraEquipo)
                {
                    FileInfo temp = new FileInfo(e.FullPath);
                    AyudanteDeTrabajo.ShowBalloonTip(2000, "El equipo trabaja duro!", $"Parece que alguien agregó el archivo {e.Name} al repositorio del equipo.", ToolTipIcon.Info);
                }
            }
        }

        /// <summary>
        /// Método encargado de capturar el evento, filtrar el tipo de archivo modificado y realizar una acción.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArchivoModificado(object sender, FileSystemEventArgs e)
        {
            bool repositorio = false;
            string file = e.FullPath;
            string strFileExt = Path.GetExtension(file);

            bool filtrado = false;
            foreach (var item in Vista.filtros)
            {
                if (file.Contains(item.Key))
                    if (item.Value)
                        filtrado = true;
            }

            if (Directory.Exists(Vista.RepositorioEquipo))
                if (file.Contains(Vista.RepositorioEquipo))
                    repositorio = true;

            if (!repositorio)
            {
                if (!filtrado)
                {
                    if (Vista.RegistrarErrores && strFileExt == ".err")
                        AyudanteDeTrabajo.ShowBalloonTip(2000, "Vaya, debo decirte algo", "Fueron modificados logs con error.", ToolTipIcon.Error);

                    if (Vista.RegistrarLogs && strFileExt == ".log")
                        AyudanteDeTrabajo.ShowBalloonTip(2000, "Veo algo de movimiento", "Fueron modificados logs de información.", ToolTipIcon.Info);
                }
            }
            else
            {
                if (!filtrado && Vista.RegistraEquipo)
                {
                    FileInfo temp = new FileInfo(e.FullPath);
                    AyudanteDeTrabajo.ShowBalloonTip(2000, "El equipo trabaja duro!", $"Parece que alguien modificó el archivo {e.Name} en el repositorio del equipo.", ToolTipIcon.Info);
                }
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

        private void CargarConfiguracion()
        {
            Configuraciones config;

            if (!File.Exists(Environment.CurrentDirectory + "\\config.json"))
            {
                File.Create(Environment.CurrentDirectory + "\\config.json").Dispose();

                config = new Configuraciones();
                Vista.RepositorioEquipo = "";
                Vista.RegistrarErrores = true;
                Vista.RegistrarLogs = true;

                GuardarConfiguracion();
                return;
            }

            config = JsonConvert.DeserializeObject<Configuraciones>(File.ReadAllText(Environment.CurrentDirectory + "\\config.json"));

            Vista.RepositorioEquipo = config.RutaRepositorio;
            Vista.RegistrarLogs = config.NotificarLogs;
            Vista.RegistrarErrores = config.NotificarErrores;
        }

        private void GuardarConfiguracion()
        {
            Configuraciones config = new Configuraciones();
            config.NotificarLogs = Vista.RegistrarLogs;
            config.NotificarErrores = Vista.RegistrarErrores;
            config.RutaRepositorio = Vista.RepositorioEquipo;

            // Serializo la configuración.
            File.WriteAllText(Environment.CurrentDirectory + "\\config.json", JsonConvert.SerializeObject(config));
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
            GuardarConfiguracion();
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
                this.AyudanteDeTrabajo.ShowBalloonTip(2000, "Vaya, que pasó?", $"La ruta \"{ruta}\" no existe.", ToolTipIcon.Error);
                Logger.Default.Warn($"Parece que la ruta \"{ruta}\" no existe.");
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

            AyudanteDeTrabajo.ShowBalloonTip(2000, "Mi trabajo aquí terminó!", "Carpeta de logs limpiada satisfactoriamente.", ToolTipIcon.Info);
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