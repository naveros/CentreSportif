using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CentreSportifLib;
using CentreSportifLib.dto;
using CentreSportifGUI.Views.menu;
using CentreSportifGUI.Views.formulaire;
using CentreSportifGUI.Views.formulaire.formulairesMembre;

namespace CentreSportifGUI
{
    public partial class CentreSportifGUI : Form
    {
        public CentreSportifCreateur DbCreateur;
        PersonneDTO connectedPersonneDTO;
        public CentreSportifGUI()
        {
            InitializeComponent();
            DbCreateur = new CentreSportifCreateur();
            RefreshTableMembre();
            RefreshTableActivite();
            RefreshTableGroupe();
            labelMessageMembre.Text = "";
            labelMessageActivite.Text = "";
            labelMessageGroupe.Text = "";
        }

        public void RefreshTableMembre()
        {
            this.dataGridView1.Rows.Clear();
            DbCreateur.ServicePersonne.getAll().ForEach(delegate(PersonneDTO personneDTO)
            {
                int i = this.dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = personneDTO.IdPersonne;
                dataGridView1.Rows[i].Cells[1].Value = personneDTO.Prenom;
                dataGridView1.Rows[i].Cells[2].Value = personneDTO.Nom;
                dataGridView1.Rows[i].Cells[3].Value = personneDTO.Email;
                dataGridView1.Rows[i].Cells[4].Value = formatRole(personneDTO.Role);
                dataGridView1.Rows[i].Cells[5].Value = personneDTO.Sexe;
                dataGridView1.Rows[i].Cells[6].Value = personneDTO.CodeBarre;
                dataGridView1.Rows[i].Cells[7].Value = "Option";
                dataGridView1.Rows[i].Cells[7].Tag = personneDTO;
            });
        }

        public void RefreshTableActivite()
        {
            this.dataGridView2.Rows.Clear();
            DbCreateur.ServiceActivite.getAll().ForEach(delegate(ActiviteDTO activiteDTO)
            {
                int i = this.dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = activiteDTO.IdActivite;
                dataGridView2.Rows[i].Cells[1].Value = activiteDTO.Nom;
                dataGridView2.Rows[i].Cells[2].Value = activiteDTO.Duree;
                dataGridView2.Rows[i].Cells[3].Value = activiteDTO.Description;
                dataGridView2.Rows[i].Cells[4].Value = "Option";
                dataGridView2.Rows[i].Cells[4].Tag = activiteDTO;
            });
        }

        public void RefreshTableGroupe()
        {
            this.dataGridView3.Rows.Clear();
            DbCreateur.ServiceGroupe.getAll().ForEach(delegate(GroupeDTO groupeDTO)
            {
                int i = this.dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = groupeDTO.IdGroupe;
                dataGridView3.Rows[i].Cells[1].Value = groupeDTO.IdActivite;
                dataGridView3.Rows[i].Cells[2].Value = DbCreateur.ServiceActivite.findById(groupeDTO.IdActivite).Nom;
                dataGridView3.Rows[i].Cells[3].Value = groupeDTO.NumeroGroupe;
                dataGridView3.Rows[i].Cells[4].Value = "Option";
                dataGridView3.Rows[i].Cells[4].Tag = groupeDTO;
            });
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                PersonneDTO personneDTO = (PersonneDTO)dataGridView1.Rows[e.RowIndex].Cells[7].Tag;
                if (personneDTO != null)
                {
                    MenuMembre form = new MenuMembre(personneDTO);
                    form.Owner = this;
                    form.ShowDialog();
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                ActiviteDTO activiteDTO = (ActiviteDTO)dataGridView2.Rows[e.RowIndex].Cells[4].Tag;
                if (activiteDTO != null)
                {
                    MenuActivite form = new MenuActivite(activiteDTO);
                    form.Owner = this;
                    form.ShowDialog();
                }
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                GroupeDTO groupeDTO = (GroupeDTO)dataGridView3.Rows[e.RowIndex].Cells[4].Tag;
                if (groupeDTO != null)
                {
                    MenuGroupe form = new MenuGroupe(groupeDTO);
                    form.Owner = this;
                    form.ShowDialog();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e) //Connexion d'un membre manuellement
        {
            FormulaireConnexion formConnexion = new FormulaireConnexion();
            formConnexion.Owner = this;
            formConnexion.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) //nouveau membre
        {
            FormulaireMembre formMembre = new FormulaireMembre(null);
            formMembre.Owner = this;
            formMembre.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e) //nouvelle activite
        {
            FormulaireActivite formActivite = new FormulaireActivite(null);
            formActivite.Owner = this;
            formActivite.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e) //nouveau groupe
        {
            FormulaireGroupe formGroupe = new FormulaireGroupe(null);
            formGroupe.Owner = this;
            formGroupe.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) //Chercher Membre par id
        {
            labelMessageMembre.Text = "";
            try
            {
                int idPersonne = int.Parse(textBox1.Text);
                PersonneDTO personneDTO = DbCreateur.ServicePersonne.findById(idPersonne);
                MenuMembre formMenuMembre = new MenuMembre(personneDTO);
                formMenuMembre.Owner = this;
                formMenuMembre.ShowDialog();
            }
            catch
            {
                labelMessageMembre.Text = "ID introuvable";
            }
        }

        private void button3_Click(object sender, EventArgs e) //Chercher Activite par id
        {
            labelMessageActivite.Text = "";
            String idActivite = textBox2.Text;
            try
            {
                ActiviteDTO activiteDTO = DbCreateur.ServiceActivite.findById(idActivite);
                MenuActivite form = new MenuActivite(activiteDTO);
                form.Owner = this;
                form.ShowDialog();
            }
            catch
            {
                labelMessageActivite.Text = "ID introuvable";
            }
        }

        private void button4_Click(object sender, EventArgs e)//Chercher Groupe par id
        {
            labelMessageGroupe.Text = "";
            String idGroupe = textBox4.Text;
            try
            {
                GroupeDTO groupeDTO = DbCreateur.ServiceGroupe.findById(idGroupe);
                MenuGroupe form = new MenuGroupe(groupeDTO);
                form.Owner = this;
                form.ShowDialog();
            }
            catch
            {
                labelMessageGroupe.Text = "ID introuvable";
            }
        }

        private void button9_Click(object sender, EventArgs e)//Menu membre
        {
            MenuMembre formMenuMembre = new MenuMembre(connectedPersonneDTO);
            formMenuMembre.Owner = this;
            formMenuMembre.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)//Ajouter message
        {
            FormulaireMessage formMessage = new FormulaireMessage(connectedPersonneDTO);
            formMessage.Owner = this;
            formMessage.ShowDialog();
        }

        public void connexionAccueil(PersonneDTO personneDTO) //Connexion Accueil
        {
            textBoxID.Text = personneDTO.IdPersonne;
            textBoxEmail.Text = personneDTO.Email;
            textBoxNom.Text = personneDTO.Nom;
            textBoxPrenom.Text = personneDTO.Prenom;
            textBoxRole.Text = formatRole(personneDTO.Role);
            textBoxCodeBarre.Text = personneDTO.CodeBarre;
            try
            {
                if (File.Exists("../photos/" + personneDTO.IdPersonne + ".jpg"))
                {
                    pictureBox1.Image = Image.FromFile("../photos/" + personneDTO.IdPersonne + ".jpg");
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    pictureBox1.Image = Image.FromFile("../photos/0.jpg");
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch
            {
                throw;
            }

            connectedPersonneDTO = personneDTO;
            button8.Visible = true;
            button9.Visible = true;
            button10.Visible = true;

            afficherAllMessages();

            listBox3.Items.Add(DateTime.Now.ToShortTimeString() + " || ID: " + personneDTO.IdPersonne + " | Prenom: " + personneDTO.Prenom + " | Nom:"
                + personneDTO.Nom + " | Role : " + personneDTO.Role);
        }

        private void button10_Click(object sender, EventArgs e) //Supprimer message
        {
            var confirmResult = MessageBox.Show("Êtes-vous certain de vouloir supprimer ce message ? ",
                                       "Confirmer la suppression d'un message",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    MessageDTO messageDTO = (MessageDTO)listBox2.SelectedItem;
                    DbCreateur.ServicePersonne.deleteMessage(messageDTO.IdMessage);
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                }
                catch (Exception ee)
                {
                    Console.WriteLine("Erreur dans la requete delete message");
                    Console.Write(ee.Message);
                }
            }
        }

        public void afficherAllMessages()
        {
            while (listBox2.Items.Count > 0)
            {
                listBox2.Items.RemoveAt(0);
            }
            DbCreateur.ServicePersonne.getAllMessages(connectedPersonneDTO.IdPersonne).ForEach(delegate(MessageDTO messageDTO)
            {
                listBox2.Items.Add(messageDTO);
            });
        }

        public String formatRole(String role)
        {
            String result;
            if (role == "membre")
            {
                result = "Membre";
            }
            else if (role == "prof")
            {
                result = "Professeur";
            }
            else
            {
                result = "Administrateur";
            }
            return result;
        }
    }
}