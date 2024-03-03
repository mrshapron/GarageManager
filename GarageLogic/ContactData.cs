using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class ContactData : IParamDictionary
    {
        public Vehicle Vehicle { get; set; }
        public string VehicleOwner { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public eVehicleStatus VehicleStatus { get; set; }

        public Dictionary<string, string> GenerateParamsOutputs()
        {

            Dictionary<string, string> dicParams = new Dictionary<string, string>
            {
                { "VehicleOwner", "Enter Vehicle Owner" },
                {"OwnerPhoneNumber","Enter Phone Number" },
                { "VehicleStatus","Enter Vehicle Status \n1.InProcess \n2.Ready\n3.Paid\n4.None" }
            };
            return dicParams;
        }

        public Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues)
        {
            bool isValid;
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (i_DicValues.ContainsKey("VehicleOwner"))
            {
                VehicleOwner = i_DicValues["VehicleOwner"];
            }
            if (i_DicValues.ContainsKey("OwnerPhoneNumber"))
            {
                OwnerPhoneNumber = i_DicValues["OwnerPhoneNumber"];
            }
            if (i_DicValues.ContainsKey("VehicleStatus"))
            {
                eVehicleStatus status;
                isValid = Enum.TryParse(i_DicValues["VehicleStatus"],out status);
                if (!isValid)
                {
                    errors.Add("VehicleStatus", "Vehicle Status out of range (1-3)");
                }
                else
                {
                    VehicleStatus = status;
                }
            }
            return errors;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("*****Contact Data******");
            stringBuilder.AppendLine($"Owner Name: {VehicleOwner}");
            stringBuilder.AppendLine($"Vehicle Status: {VehicleStatus}");
            string vehicleDetails = Vehicle.ToString();
            stringBuilder.Append(vehicleDetails);
            return stringBuilder.ToString();
        }
    }
    public enum eVehicleStatus
    {
        InProcess = 1, Ready = 2, Paid = 3, None = 4 
    }
}
