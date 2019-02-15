using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace YkzWorkHelper
{
    public partial class ConfigurarFiltros : Form
    {
        public ConfigurarFiltros()
        {
            InitializeComponent();
        }

        // Load filter configuration.
        private void ConfigurarFiltros_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            int counter = 0;
            foreach (var item in Vista.filtros)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[counter].Cells[0].Value = item.Key;
                dataGridView1.Rows[counter].Cells[1].Value = item.Value;
                counter++;
            }
        }

        // Save filter configuration.
        private void ConfigurarFiltros_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<Filtro> configuracion = new List<Filtro>();

            for (int j = 0; j < dataGridView1.Rows.Count - 1; ++j)
            {
                string key = (string)dataGridView1.Rows[j].Cells[0].Value;

                bool value = false;

                if (dataGridView1.Rows[j].Cells[1].Value != null)
                    value = (bool)dataGridView1.Rows[j].Cells[1].Value;

                configuracion.Add(new Filtro(key, value));
            }

            string json = JsonConvert.SerializeObject(configuracion, Formatting.Indented);

            using (StreamWriter escritor = new StreamWriter(Environment.CurrentDirectory + "\\filtros.json", false))
                escritor.Write(json);
        }
    }
}
