//File name:    Devices.cs
//Author:        Su Hoi Chong A94729
//Date:           04/01/2016

using System;
namespace ESP__Electricity_Simulation_Program_.Business
{
    [Serializable]
    public class Devices
    {
        //Data Fields
        /// <summary>
        /// To hold the device data in a different valueable,
        /// String = text and number
        /// double = number
        /// </summary>
        public string name;
        public string othername;
        public double watts;
        public double hours;

        //CONSTRUCTORS
        /// <summary>
        /// initializes a new instance of the device class
        /// </summary>
        /// <param name="name">The device name.</param>
        /// <param name="othername">The device other name.</param>
        /// <param name="watts">The watts of the device,measured in watts.</param>
        /// <param name="hours">The hours used of the device. measured in hours.</param>
        public Devices(string name, string othername, double watts, double hours)
        {
            if (name == "")
                throw new System.FormatException("You must specify a value for the name of the device.");
            this.name = name;

            if (othername == "")
                throw new System.FormatException("You must specify a value for the other name of the device.");
            this.othername = othername;

            if (watts <= 0)
                throw new System.FormatException("You must enter a value for the watts of the Device.");
            this.watts = watts;

            if (hours <= 0|| hours > 24)
                throw new System.FormatException("You must enter a value for the hours of daily used of the device.");
            this.hours = hours;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>
        ///
        /// </returns>
        public override string ToString()
        {
            return
                "Device name: " + name +
                ", Other name:" + othername +
                ", Device watts(W)= " + watts + ", " +
                "Hours used in a day =" + hours + "(h).";
        }
    }
}
