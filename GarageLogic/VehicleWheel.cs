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
        public float CurrentAirPressure { get; private set;}
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
        public virtual string PrintParameters()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Manufacturer,CurrentAirPressure,MaxAirPressure");
            return sb.ToString();
        }
    }
}
