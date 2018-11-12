//File name:    ESP.cs
//Author:       Su Hoi Chong A94729
//Date:         03/01/2016

using ESP__Electricity_Simulation_Program_.Business;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ESP__Electricity_Simulation_Program_
{
    public partial class ESP : Form
    {
        //Data Field
        Devices devices;

        /// <summary>
        /// valuable of each kWh cost of the company
        /// </summary>
        public double eon = 12.46214;
        public double BG = 14.37;
        public double npower = 17.367;


        /// <summary>
        /// get value to save and display the information form the file.
        /// </summary>
        ///<value>
        ///get the value Lod
        ///</value>
        public List<Devices> Lod
        {
            get
            {
                List<Devices> lod = new List<Devices>();
                foreach (ListViewItem lv in devicelistView.Items)
                {
                    lod.Add((Devices)lv.Tag);
                }
                return lod;
            }
            set
            {
                foreach (Devices d in value)
                {
                    ListViewItem lv = new ListViewItem(d.ToString());
                    lv.Tag = d;
                    devicelistView.Items.Add(lv);
                }
            }
        }
        //PROPERTIES
        /// <summary>
        /// Get the name for the device
        /// </summary>
        /// <value>
        /// The name of the device.
        /// </value>
        private string devicename
        {
            get
            {
                //if the comboboxDevice.Text = nothing, the error message will be display.
                if (comboBoxDevice.Text == "")
                {
                    //Error message
                    throw new FormatException("You must enter a value for the name of the device or you can select 'other' then on the other section type the name of your device.");
                }
                else
                    return comboBoxDevice.Text;
            }
        }

        /// <summary>
        /// Get the other name for the device.
        /// </summary>
        /// <value>
        /// The other name for the device.
        /// </value>
        private string otherdevicename
        {
            get
            {
                //if the otherTextBox.Text = nothing and comboboxDevice.Text = other, the error message will be display.
                if (otherTextBox.Text == "" && comboBoxDevice.Text == "other")
                {
                    //Error message
                    throw new FormatException("You must enter a value for the other name of the Device.");
                }
                else
                    return otherTextBox.Text;
            }
        }

        /// <summary>
        /// Get the watts for the device.
        /// </summary>
        /// <value>
        /// The watts for the device.
        /// </value>
        private double wattsdevice
        {
            get
            {

                //if the WattstextBox.Text  = nothing, the error message will be display.
                if (WattstextBox.Text == "")
                {
                    //Error message
                    throw new FormatException("You must enter a value for the watts of the Device.");
                }
                else
                    return Convert.ToDouble(WattstextBox.Text);
            }
        }


        /// <summary>
        /// Get the hours used for the device.
        /// </summary>
        /// <value>
        /// The hours used for the device.
        /// </value>
        private double hoursofdevice
        {
            get
            {
                //if the WattstextBox.Text  = nothing, the error message will be display.
                if (HourstextBox.Text == "")
                {
                    //Error message
                    throw new FormatException("You must enter a value for the hours of daily used of the device.");
                }
                else
                    return Convert.ToDouble(HourstextBox.Text);
            }
        }



        /// <summary>
        /// Get the value form Devices class
        /// </summary>
        /// <value>
        /// get value
        /// </value>
        public Devices value
        {
            get
            {
                return devices;
            }
            set
            {
                comboBoxDevice.Text = value.name.ToString();
                otherTextBox.Text = value.othername.ToString();
                WattstextBox.Text = value.watts.ToString();
                HourstextBox.Text = value.hours.ToString();
            }
        }

        //
        /// <summary>
        /// Add and display the informaiton devices to listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                ///devices = constructors in <see cref="Devices"/>.class
                devices = new Devices(devicename, otherdevicename, wattsdevice, hoursofdevice);
                ///display the devices in listviewitem
                ListViewItem lvitem = new ListViewItem(devices.ToString());
                lvitem.Tag = devices;
                devicelistView.Items.Add(lvitem);

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //allow to remove a wrong data or information for the selected device.
        /// <summary>
        /// remove the selected item in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeButton_Click(object sender, EventArgs e)
        {
            if (devicelistView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lvitem in devicelistView.SelectedItems)
                {
                    devicelistView.Items.Remove(lvitem);
                }
            }

            //if the items hasnt been selected, the error message will display.
            else
            {

                Console.WriteLine("Error: You must select a device to remove."); ///Error message
                MessageBox.Show("Error: You must select a device to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the <see cref="ESP"/> class.
        /// </summary>
        public ESP()
        {
            InitializeComponent();

            //set the width of listview
            foreach (ColumnHeader col in devicelistView.Columns)
                col.Width = devicelistView.Width;
        }


        /// <summary>
        /// To calculate the total hours and watts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <value>
        /// get the string of watts and hours from the 'devices'.
        /// </value>
        private void CalculateButton_Click(object sender, EventArgs e)
        {

            double totalWatts = 0;
            double totalHours = 0;

            ///convert string to double
            double other;
            if (OthercompanytextBox.Text == "")
                other = 0;
            else
                other = Convert.ToDouble(OthercompanytextBox.Text);


            ///foreach loop to sum up the total device watts and hours used.
            foreach (ListViewItem lvitem in devicelistView.Items)
            {
                Devices d = (Devices)lvitem.Tag;///get value

                totalWatts = totalWatts + d.watts; ///sum up the total watts.
                totalHours = totalHours + d.hours;///sum up the total hours.
            }

            ///if the eon radio button is selected and click calculate button calculate and display the total watts, hours,  the daily, monthy and year cost.
            if (eonradioButton.Checked == true)
            {
                ///Data Field
                double energy = totalWatts * totalHours / 1000;
                double eoncost = energy * eon / 100;
                double eonmonthcost = eoncost / 24 * 30;
                double eonyearcost = eoncost / 24 * 365;

                resultTextBox.Text =
                    "You have select EON. " + Environment.NewLine +
                    "Total Watts of all the devices = " + totalWatts + "(W)." + Environment.NewLine +
                    "Total Hours used =" + totalHours + "(h)." + Environment.NewLine +
                    "Total daily cos £" + Math.Round(eoncost, 2) + "." + Environment.NewLine +
                    "Total monthly cost = £" + Math.Round(eonmonthcost, 2) + Environment.NewLine +
                    "Total year cost = £" + Math.Round(eonyearcost, 2) + ".";
            }
            ///if the BG radio button is selected and click calculate button calculate and display the total watts, hours,  the daily, monthy and year cost
            if (BGradioButton.Checked == true)
            {
                double energy = totalWatts * totalHours / 1000;
                double BGcost = energy * BG / 100;
                double BGmonthcost = BGcost / 24 * 30;
                double BGyearcost = BGcost / 24 * 365;
                resultTextBox.Text =
                "You have select British Gas. " + Environment.NewLine +
                    "Total Watts of all the devices = " + totalWatts + "(W)." + Environment.NewLine +
                    "Total Hours used =" + totalHours + "(h)." + Environment.NewLine +
                    "Total daily cos £" + Math.Round(BGcost, 2) + "." + Environment.NewLine +
                    "Total monthly cost = £" + Math.Round(BGmonthcost, 2) + Environment.NewLine +
                    "Total year cost = £" + Math.Round(BGyearcost, 2) + ".";
            }
            ///if the npower radio button is selected and click calculate button calculate and display the total watts, hours,  the daily, monthy and year cost
            if (npowerradioButton.Checked == true)
            {
                double energy = totalWatts * totalHours / 1000;
                double npowercost = energy * npower / 100;
                double npowermonthcost = npowercost / 24 * 30;
                double npoweryearcost = npowercost / 24 * 365;
                resultTextBox.Text =
               "You have select npower. " + Environment.NewLine +
                    "Total Watts of all the devices = " + totalWatts + "(W)." + Environment.NewLine +
                    "Total Hours used =" + totalHours + "(h)." + Environment.NewLine +
                    "Total daily cos £" + Math.Round(npowercost, 2) + "." + Environment.NewLine +
                    "Total monthly cost = £" + Math.Round(npowermonthcost, 2) + Environment.NewLine +
                    "Total year cost = £" + Math.Round(npoweryearcost, 2) + ".";
            }
            ///if the other radio button is selected and click calculate button calculate and display the total watts, hours,  the daily, monthy and year cost
            if (OtherradioButton.Checked == true)
            {

                double energy = totalWatts * totalHours / 1000;
                double othercost = energy * other / 100;
                double othermonthcost = othercost / 24 * 30;
                double otheryearcost = othercost / 24 * 365;
                resultTextBox.Text =
                "You have select other company. " + Environment.NewLine +
                    "Total Watts of all the devices = " + totalWatts + "(W)." + Environment.NewLine +
                    "Total Hours used =" + totalHours + "(h)." + Environment.NewLine +
                    "Total daily cos £" + Math.Round(othercost, 2) + "." + Environment.NewLine +
                    "Total monthly cost = £" + Math.Round(othermonthcost, 2) + Environment.NewLine +
                    "Total year cost = £" + Math.Round(otheryearcost, 2) + ".";
            }

            //validation - if all the radionbutton hasn't been click, errow message will display.
            else if (eonradioButton.Checked == false && BGradioButton.Checked == false && npowerradioButton.Checked == false && OtherradioButton.Checked == false)
            {
                ///Error message
                Console.WriteLine("Error: You must select a company to calculate the cost.");
                MessageBox.Show("Error: You must select a company to calculate the cost.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// clear the information in the resulttextbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clearbutton_Click(object sender, EventArgs e)
        {
            resultTextBox.Clear();
        }

        /// <summary>
        /// clear the information in the devicelistview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clearbutton1_Click(object sender, EventArgs e)
        {
            devicelistView.Items.Clear();
        }

        /// <summary>
        /// when the OtherradioButton is clicked, other radiobutton cannot be click.
        /// incloude(npowerradioButton,BGradioButton,eonradioButton)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtherradioButton_CheckedChanged(object sender, EventArgs e)
        {
            npowerradioButton.Checked = false;
            BGradioButton.Checked = false;
            eonradioButton.Checked = false;
        }
        /// <summary>
        /// when the BGradionButton is clicked, OtherradioButton cannot be click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eonradioButton_CheckedChanged(object sender, EventArgs e)
        {
            OtherradioButton.Checked = false;
        }
        /// <summary>
        /// when the BGradionButton is clicked, OtherradioButton cannot be click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BGradioButton_CheckedChanged(object sender, EventArgs e)
        {
            OtherradioButton.Checked = false;
        }
        /// <summary>
        /// when the BGradionButton is clicked, OtherradioButton cannot be click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void npowerradioButton_CheckedChanged(object sender, EventArgs e)
        {
            OtherradioButton.Checked = false;
        }

    }
}
