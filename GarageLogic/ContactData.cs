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
    }
    public enum eVehicleStatus
    {
        InProcess, Ready, Paid
    }
}
