using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class ContactData
    {
        public Vehicle Vehicle { get; set; }
        public string VehicleOwner { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public eVehicleStatus VehicleStatus { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Contact Data:\n");
            stringBuilder.AppendLine($"Owner Name: {VehicleOwner}\n");
            stringBuilder.AppendLine($"Vehicle Status: {VehicleStatus}\n");
            stringBuilder.AppendLine(Vehicle.ToString());
            return stringBuilder.ToString();
        }
    }
    public enum eVehicleStatus
    {
        InProcess=1, Ready, Paid, None
    }
}
