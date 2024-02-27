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
            this.CurrentAmount = i_HoursCharge;
            if (this.CurrentAmount > this.MaxAmount)
            {
                this.CurrentAmount = this.MaxAmount;
            }
        }
    }
}
