using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using CentreSportifLib.dto;

namespace CentreSportifGUI.Views.formulaire
{
    public partial class FormulaireMembre : Form
    {
        PersonneDTO p;
        AdresseDTO adresse;
        string mode;
        public CentreSportifGUI CentreView;
        Image photo;
        public FormulaireMembre(PersonneDTO p)
        {
            InitializeComponent();
            if (p != null)
            {

                this.mode = "Modifier";
                this.p = p;               
            }
            else
            {
                this.mode = "Créer";
                this.p = new PersonneDTO();
            }
            this.Text = mode;
        }

        private void button1_Click(object sender, EventArgs e) //enregistrer/modifier
        {
            String errorMessage = "";
            

                //TODO: Gestion des erreurs
            try
            {
                p.IdPersonne = textBox1.Text;
                p.Prenom = textBox2.Text;
                p.Nom = textBox3.Text;
                p.DateNaissance = dateTimePicker1.Value;
                p.Email = textBox4.Text;
                p.CodeBarre = textBox5.Text;


                adresse = new AdresseDTO();
                adresse.Numero = textBox8.Text;
                adresse.Rue = textBox9.Text;
                adresse.CodePostal = textBox10.Text;
                adresse.Ville = textBox11.Text;
                adresse.IdPersonne = textBox1.Text;
                adresse.Pays = textBox12.Text;
                p.Adresse = adresse;
                p.Role = "membre";


                if (radioButton2.Checked)
                {
                    p.Sexe = 'F';
                }
                else
                {
                    p.Sexe = 'M';
                }
                if (textBox6.Text.Equals(textBox7.Text))
                {
                    p.MotDePasse = textBox6.Text;

                }
                else 
                {
                    errorMessage = "Mot de passe incorrect";
                    Exception exeptionPWD = new Exception("Mot de passe incorrect");
                    throw exeptionPWD;
                }
                if (this.mode.Equals("Créer"))
                {

                  //  photo.Save("../photos/" + p.IdPersonne + ".jpg", ImageFormat.Jpeg); /////////
                    adresse.IdPersonne = this.CentreView.DbCreateur.ServicePersonne.register(p);
                    this.CentreView.DbCreateur.ServicePersonne.addAdresse(adresse);
                    errorMessage = "Ajout réussit !";
                }
                else if (this.mode.Equals("Modifier"))
                {
                    CentreView.DbCreateur.ServicePersonne.update(p);
                    CentreView.DbCreateur.ServicePersonne.updateAdresse(adresse);
                    errorMessage = "Modification réussit";
                }
            }
            catch (Exception exception)
            {
                if (errorMessage == "")
                {
                    errorMessage = "Informations incorrectes";
                }
                Console.WriteLine(exception);
            }
            finally
            {
                label13.Text = "Message : "+errorMessage;
            }
        }
        

        private void button2_Click(object sender, EventArgs e) //annulé
        {
            this.Dispose();
        }

        private void remplir()
        {
            try
            {
                
                textBox1.Text = p.IdPersonne;
                textBox2.Text = p.Prenom;
                textBox3.Text = p.Nom;
                textBox4.Text = p.Email;
                dateTimePicker1.Value = p.DateNaissance;

                textBox8.Text = adresse.Numero;
                textBox9.Text = adresse.Rue;
                textBox10.Text = adresse.CodePostal;
                textBox11.Text = adresse.Ville;
                textBox12.Text = adresse.Pays;
                try
                {
                    if (File.Exists("../photos/" + p.IdPersonne + ".jpg"))
                    {
                        pictureBox1.Image = Image.FromFile("../photos/" + p.IdPersonne + ".jpg");
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                catch
                {
                    throw;
                }


                if (p.Sexe.Equals('f'))
                {
                    radioButton2.Checked = true;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("Erreur dans la requete get adresse");
                Console.Write(ee.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e) //Changer 
        {

            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "%USERPROFILE%";
            openFileDialog1.Filter = "Jpeg files (*.jpg)|*.jpg";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {

                            photo = System.Drawing.Image.FromStream(myStream);
                            pictureBox1.Image = Image.FromStream(myStream);
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;


                            var i2 = new Bitmap(photo);
                            i2.Save("c:\test.jpg",ImageFormat.Jpeg);

                          /*  using (var m = new MemoryStream())
                            {
                                photo.Save(m, ImageFormat.Jpeg);

                                var img = Image.FromStream(m);

                                //TEST
                                img.Save("C:\\test.jpg", ImageFormat.Jpeg);

                            }*/

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e) //Captuer 
        {
            //TODO
        }

        private void FormulaireMembre_Load(object sender, EventArgs e)
        {
            this.CentreView = (CentreSportifGUI)this.Owner;
            if (this.mode.Equals("Modifier")) 
            {
                adresse = CentreView.DbCreateur.ServicePersonne.getAdresse(p);
                remplir();
            }
        }
    }
}
