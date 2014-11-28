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

namespace CentreSportifTester
{
    public partial class Form1 : Form
    {
        CentreSportifCreateur sp;
        public Form1()
        {
            InitializeComponent();
            this.sp = new CentreSportifCreateur();
            RefreshTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        public void RefreshTable()
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
                dataGridView1.Rows[i].Cells[4].Value = p.CodeBarre;
                dataGridView1.Rows[i].Cells[5].Value = p.Sexe;
                dataGridView1.Rows[i].Cells[6].Value = p.MotDePasse;
            });
        }
    }
}
