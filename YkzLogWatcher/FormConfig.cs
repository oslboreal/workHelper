using Microsoft.Win32;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace YkzWorkHelper
{
    public partial class Configuracion : Form
    {
        DataTable registros;

        /// <summary>
        /// Método constructor de la clase Configuración.
        /// </summary>
        public Configuracion()
        {
            InitializeComponent();

            if (!File.Exists(Environment.CurrentDirectory + "\\registros.bin"))
                File.Create(Environment.CurrentDirectory + "\\registros.bin");

            dataGridView1.Rows.Clear();
            string file = Environment.CurrentDirectory + "\\registros.bin";
            using (BinaryReader bw = new BinaryReader(File.Open(file, FileMode.Open)))
            {
                var nextValue = bw.PeekChar();

                if (nextValue != -1)
                {
                    int n = bw.ReadInt32();
                    int m = bw.ReadInt32();
                    for (int i = 0; i < m; ++i)
                    {
                        dataGridView1.Rows.Add();
                        for (int j = 0; j < n; ++j)
                        {
                            if (bw.ReadBoolean())
                            {
                                dataGridView1.Rows[i].Cells[j].Value = bw.ReadString();
                            }
                            else bw.ReadBoolean();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Método Load de la clase Configuración.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Configuracion_Load(object sender, EventArgs e)
        {
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

            //registros.WriteXml("regs");
            this.Dispose();
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            // TODO: Validar input.
            if (e.ColumnIndex == 1)
                MessageBox.Show("Valido");
        }

        /// <summary>
        /// Saves the current state of the data grid view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Configuracion_FormClosing(object sender, FormClosingEventArgs e)
        {
            string file = Environment.CurrentDirectory + "\\registros.bin";
            using (BinaryWriter bw = new BinaryWriter(File.Open(file, FileMode.Create)))
            {
                bw.Write(dataGridView1.Columns.Count);
                bw.Write(dataGridView1.Rows.Count);
                foreach (DataGridViewRow dgvR in dataGridView1.Rows)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; ++j)
                    {
                        object val = dgvR.Cells[j].Value;

                        if (val == null)
                        {
                            bw.Write(false);
                            bw.Write(false);
                        }
                        else
                        {
                            bw.Write(true);
                            bw.Write(val.ToString());
                        }
                    }
                }
            }
        }
    }
}
