using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace YkzWorkHelper
{
    public partial class Configuracion : Form
    {
        private bool mostrarLogs;
        private bool mostrarErrores;

        /// <summary>
        /// Método constructor de la clase Configuración.
        /// </summary>
        public Configuracion()
        {
            InitializeComponent();

            dataGridView1.Rows.Clear();

            int counter = 0;
            foreach (var item in Vista.accesosDirectos)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[counter].Cells[0].Value = item.Key;
                dataGridView1.Rows[counter].Cells[1].Value = item.Value;
                counter++;
            }
        }

        /// <summary>
        /// Método Load de la clase Configuración.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Configuracion_Load(object sender, EventArgs e)
        {
            ActualizarUI();
        }

        private void ActualizarUI()
        {
            // Registro del sistema.
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                var value = key.GetValue("YkzWorkHelper");

                if (value == null)
                {
                    toolStripStatusLabel1.BackColor = Color.Red;
                    checkBox1.Checked = false;
                }
                else
                {
                    toolStripStatusLabel1.BackColor = Color.Green;
                    checkBox1.Checked = true;
                }
            }

            // Configuraciones.
            checkboxLogs.Checked = Vista.RegistrarLogs;
            checkboxErrores.Checked = Vista.RegistrarErrores;
            textBox1.Text = Vista.RepositorioEquipo;
        }

        /// <summary>
        /// Método que se ejecuta cuando se modifica el estado de la check box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    var value = key.GetValue("YkzWorkHelper");

                    // If the value does not exist.
                    if (value == null)
                    {
                        key.SetValue("YkzWorkHelper", "\"" + Application.ExecutablePath + "\"");
                    }
                    else
                    {
                        key.DeleteValue("YkzWorkHelper");
                        key.SetValue("YkzWorkHelper", "\"" + Application.ExecutablePath + "\"");
                    }
                }
            }
            else
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    var value = key.GetValue("YkzWorkHelper");

                    // If the value does exist.
                    if (value != null)
                        key.DeleteValue("YkzWorkHelper");
                }
            }
        }

        /// <summary>
        /// Evento de timer encargado de actualizar la interfaz de usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                var value = key.GetValue("YkzWorkHelper");

                if (value == null)
                    toolStripStatusLabel1.BackColor = Color.Red;
                else
                    toolStripStatusLabel1.BackColor = Color.Green;
            }
        }

        /// <summary>
        /// Método encargado de almacenar los estados de la aplicación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Saves the current state of the data grid view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Configuracion_FormClosing(object sender, FormClosingEventArgs e)
        {
            // -- Almacenamiento de registros. 
            List<Registro> registros = new List<Registro>();

            for (int j = 0; j < dataGridView1.RowCount - 1; ++j)
            {
                string key = (string)dataGridView1.Rows[j].Cells[0].Value;
                string value = (string)dataGridView1.Rows[j].Cells[1].Value;
                registros.Add(new Registro(key, value));
            }

            string json = JsonConvert.SerializeObject(registros, Formatting.Indented);

            using (StreamWriter escritor = new StreamWriter("registros.json", false))
                escritor.Write(json);

            // -- Almacenamiento de settings.
            Vista.RegistrarLogs = checkboxLogs.Checked;
            Vista.RegistrarErrores = checkboxErrores.Checked;
            Vista.RepositorioEquipo = textBox1.Text;
        }

        /// <summary>
        /// Método de sobrecarga de apertura de formulario de filtros.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            var ventanaFiltro = new ConfigurarFiltros();
            ventanaFiltro.FormClosed += Filtros_FormClosed;
            ventanaFiltro.Show();
        }

        /// <summary>
        /// Evento producido al cerrar el formulario de filtros.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Filtros_FormClosed(object sender, FormClosedEventArgs e)
        {
            Vista.filtros.Clear();

            if (!File.Exists(Environment.CurrentDirectory + "\\filtros.json"))
                File.Create(Environment.CurrentDirectory + "\\filtros.json").Dispose();

            List<Filtro> filtros = JsonConvert.DeserializeObject<List<Filtro>>(File.ReadAllText(Environment.CurrentDirectory + "\\filtros.json"));

            foreach (var item in filtros)
                Vista.filtros.TryAdd(item.Palabra, item.Filtrar);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            mostrarLogs = checkboxLogs.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            mostrarErrores = checkboxErrores.Checked;
        }
    }
}
