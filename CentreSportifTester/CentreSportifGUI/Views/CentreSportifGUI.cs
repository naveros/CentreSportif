using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CentreSportifGUI.Views.menu;
using CentreSportifGUI.Views.formulaire;
using CentreSportifLib;
using CentreSportifLib.dto;

namespace CentreSportifGUI
{
    public partial class CentreSportifGUI : Form
    {
        public CentreSportifCreateur DbCreateur;
        public CentreSportifGUI()
        {
            InitializeComponent();
            DbCreateur = new CentreSportifCreateur();
            RefreshTableMembre();
            RefreshTableActivite();
            RefreshTableGroupe();
        }


        public void RefreshTableMembre()
        {
            this.dataGridView1.Rows.Clear();
            DbCreateur.ServicePersonne.getAll().ForEach(delegate(PersonneDTO p)
            {
                int i = this.dataGridView1.Rows.Add();
                //Console.WriteLine(p.ToString());
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
            DbCreateur.ServiceActivite.getAll().ForEach(delegate(ActiviteDTO a)
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

        public void RefreshTableGroupe()
        {
            this.dataGridView3.Rows.Clear();
            DbCreateur.ServiceGroupe.getAll().ForEach(delegate(GroupeDTO g)
            {
                int i = this.dataGridView3.Rows.Add();
               // Console.WriteLine(g.ToString());
                dataGridView3.Rows[i].Cells[0].Value = g.IdGroupe;
                dataGridView3.Rows[i].Cells[1].Value = g.IdActivite;
                dataGridView3.Rows[i].Cells[2].Value = g.NumeroGroupe;
                dataGridView3.Rows[i].Cells[3].Value = "Modifier";
                dataGridView3.Rows[i].Cells[3].Tag = g;
            });
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                PersonneDTO p = (PersonneDTO)dataGridView1.Rows[e.RowIndex].Cells[6].Tag;
                MenuMembre form = new MenuMembre(p);
                form.Owner = this;
                form.ShowDialog();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                ActiviteDTO a = (ActiviteDTO)dataGridView2.Rows[e.RowIndex].Cells[4].Tag;
                MenuActivite form = new MenuActivite(a);
                form.Owner = this;
                form.ShowDialog();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                GroupeDTO g = (GroupeDTO)dataGridView3.Rows[e.RowIndex].Cells[3].Tag;
                MenuGroupe form = new MenuGroupe(g);
                form.Owner = this;
                form.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e) //new activite
        {
            ActiviteDTO a = new ActiviteDTO();
            a.Nom = textBox2.Text;
            a.Duree = textBox3.Text;
            a.Description = richTextBox1.Text;
            DbCreateur.ServiceActivite.creer(a);
            MessageBox.Show("Activité crée avec succès.");
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            RefreshTableActivite();
        }

        private void button4_Click(object sender, EventArgs e) //new groupe
        {
            GroupeDTO g = new GroupeDTO();
            g.IdGroupe = textBox4.Text;
            g.IdActivite = textBox5.Text;
            g.NumeroGroupe = textBox6.Text;
            Console.WriteLine("GROUPE : " +g.ToString());
            DbCreateur.ServiceGroupe.creer(g);
            Console.WriteLine(g.ToString());
            MessageBox.Show("Groupe crée avec succès.");
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            RefreshTableGroupe();
        }

        private void button5_Click(object sender, EventArgs e) //Connexion d'un membre manuellement
        {
            FormulaireConnexion formConnexion = new FormulaireConnexion();
            formConnexion.Owner = this;
            formConnexion.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) //nouveau membre
        {
            FormulaireMembre form = new FormulaireMembre(null);
            form.Owner = this;
            form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e) //nouvelle activite
        {
            FormulaireActivite form = new FormulaireActivite(null);
            form.Owner = this;
            form.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e) //nouveau groupe
        {
            FormulaireGroupe form = new FormulaireGroupe(null);
            form.Owner = this;
            form.ShowDialog();
        }

        public void connexionAccueil(PersonneDTO personneDTO)
        {
            textBoxID.Text = personneDTO.IdPersonne;
            textBoxEmail.Text = personneDTO.Email;
            textBoxNom.Text = personneDTO.Nom;
            textBoxPrenom.Text = personneDTO.Prenom;
            textBoxRole.Text = personneDTO.Role;
            textBoxCodeBarre.Text = personneDTO.CodeBarre;
            try
            {
                pictureBox1.Image = Image.FromFile("../photos/" + personneDTO.IdPersonne + ".jpg");
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch {
                pictureBox1.Image = Image.FromFile("../photos/0.jpg");
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}