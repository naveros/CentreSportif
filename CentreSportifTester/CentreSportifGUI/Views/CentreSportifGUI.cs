using CentreSportifGUI.Views.formulaire;
using CentreSportifLib;
using CentreSportifLib.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CentreSportifGUI
{
    public partial class CentreSportifGUI : Form
    {
        public CentreSportifCreateur sp { set; get; }
        public CentreSportifGUI()
        {
            InitializeComponent();
            sp = new CentreSportifCreateur();
            RefreshTableMembre();
            RefreshTableActivite();
        }
        public void RefreshTableMembre()
        {
            this.dataGridView1.Rows.Clear();
            sp.ServicePersonne.getAll().ForEach(delegate(PersonneDTO p)
            {
                int i = this.dataGridView1.Rows.Add();
                Console.WriteLine(p.ToString());
                dataGridView1.Rows[i].Cells[0].Value = p.IdPersonne;
                dataGridView1.Rows[i].Cells[1].Value = p.Prenom;
                dataGridView1.Rows[i].Cells[2].Value = p.Nom;
                dataGridView1.Rows[i].Cells[3].Value = p.Email;               
                dataGridView1.Rows[i].Cells[4].Value = p.Sexe;
                dataGridView1.Rows[i].Cells[5].Value = p.CodeBarre;
                dataGridView1.Rows[i].Cells[6].Value = "Modifier";
                dataGridView1.Rows[i].Cells[6].Tag = p;
            });
        }

        public void RefreshTableActivite()
        {
            this.dataGridView2.Rows.Clear();
            sp.ServiceActivite.getAll().ForEach(delegate(ActiviteDTO a)
            {
                int i = this.dataGridView2.Rows.Add();
                //Console.WriteLine(p.ToString());
                dataGridView2.Rows[i].Cells[0].Value = a.IdActivite;
                dataGridView2.Rows[i].Cells[1].Value = a.Nom;
                dataGridView2.Rows[i].Cells[2].Value = a.Duree;
                dataGridView2.Rows[i].Cells[3].Value = a.Description;
                dataGridView2.Rows[i].Cells[4].Value = "Modifier";
                dataGridView2.Rows[i].Cells[4].Tag = a;
            });
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                PersonneDTO p = (PersonneDTO)dataGridView1.Rows[e.RowIndex].Cells[6].Tag;
                FormulaireMembre form = new FormulaireMembre(p);
                form.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActiviteDTO a = new ActiviteDTO();
            a.Nom = textBox2.Text;
            a.Duree = textBox3.Text;
            a.Description = richTextBox1.Text;
            sp.ServiceActivite.creer(a);
            MessageBox.Show("Activité crée avec succès.");
            textBox2.Text = "";
            textBox3.Text = "";
            richTextBox1.Text = "";
            RefreshTableActivite();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormulaireMembre form = new FormulaireMembre(null);
            form.ShowDialog();
        }
    }
}
