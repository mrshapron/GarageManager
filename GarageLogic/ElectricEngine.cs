using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class ElectricEngine : Engine
    {
        public void ChargeBattery(float i_HoursCharge)
        {
            CurrentAmount = i_HoursCharge;
            if (CurrentAmount + i_HoursCharge > MaxAmount)
            {
                throw new ValueOutOfRangeException("Hours of Charge", 0, MaxAmount);
            }
            CurrentAmount += i_HoursCharge;
        }
    }
}
