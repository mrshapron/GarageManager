using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class VehicleWheel
    {
        public string Manufacturer { get; set; }
        public float CurrentAirPressure { get; set; }
        public readonly float MaxAirPressure;

        public VehicleWheel(float i_MaxAirPressure)
        {
            CurrentAirPressure = 0;
            MaxAirPressure = i_MaxAirPressure;
        }
        public void Inflate(float i_AmountOfAir)
        {
            CurrentAirPressure += i_AmountOfAir;
            if(CurrentAirPressure > MaxAirPressure)
            {
                CurrentAirPressure = MaxAirPressure;
            }
        }

        public override string ToString()
        {
            string str = $"Manufacturer: {Manufacturer}\nCurrent Air Pressure: {CurrentAirPressure}\n"
                + $"Max Air Preesure: {MaxAirPressure}";
            return str;
        }
    }
}
