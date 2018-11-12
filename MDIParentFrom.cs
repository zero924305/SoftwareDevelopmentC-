//File name:    MDIParentFrom.cs
//Author:        Su Hoi Chong A94729
//Date:           04/01/2016

using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ESP__Electricity_Simulation_Program_.Business;
using System.Collections.Generic;

namespace ESP__Electricity_Simulation_Program_.Presentation
{
    public partial class MDIParentFrom : Form
    {
        //CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the <see cref="MDIParentFrom"/> class.
        /// </summary>
        public MDIParentFrom()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Display childFrom<ESP>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowNewForm(object sender, EventArgs e)
        {
            ESP childForm = new ESP();
            childForm.MdiParent = this;
            childForm.Text = "ESP(Electricity Simulation Program)";
            childForm.Show();
        }

        /// <summary>
        /// Open a file and display in childfrom<ESP> devicelistView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            string FileName = openFileDialog.FileName;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Stream s = File.Open(openFileDialog.FileName, FileMode.Open);

                BinaryFormatter bf = new BinaryFormatter();
                {
                    ESP currentForm = this.ActiveMdiChild as ESP;

                    currentForm.Lod=(List<Devices>)bf.Deserialize(s);

                }
                s.Close();
            }

        }

        /// <summary>
        /// Display the Help information page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            HelpPage helpinformation = new HelpPage();
            helpinformation.Show();
        }


        /// <summary>
        /// save the information form childfrom<ESP>devicelistView as binary file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                Stream s = File.Open(saveFileDialog.FileName, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                {

                   ESP currentForm = this.ActiveMdiChild as ESP;
                   bf.Serialize(s,currentForm.Lod);
                   s.Close();
                }
            }
        }

    }
}
