using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public interface IElectricVehicle
    {
        float RemainingBatteryTime { get; set; }
        float MaxBatteryTime { get; set; }

        void ChargeBattery(float i_HoursCharge);
       
    }
}
