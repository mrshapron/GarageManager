using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public abstract class Vehicle : IParamDictionary
    {
        public readonly int NumOfWheels;
        public string ModelName { get; set; }
        public string LicenceNumber { get; set; }
        //public float RemainingBatteryPrecent { get; set; }
        public List<VehicleWheel> Wheels { get; protected set; }
        public Engine Engine { get; set; }

        public abstract void SetWheels();
        public abstract void SetEngine(eEngineType i_EngineType);

        public Vehicle(int i_NumOfWheels)
        {
            Wheels = new List<VehicleWheel>();
            NumOfWheels = i_NumOfWheels;
        }

        public virtual Dictionary<string, string> GenerateParamsOutputs()
        {
            Dictionary<string, string> dicParams = new Dictionary<string, string>
            {
                { "ModelName", "Enter model of the vehicle" },
                { "WheelManufactur", "Enter Wheel Manufactur" },
                { "WheelCurrentAirPressure", "Please Enter Current air prresure in wheels" },
            };

            dicParams = dicParams.Union(Engine.GenerateParamsOutputs()).
                ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return dicParams;
        }
        public virtual Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues)
        {
            Dictionary<string, string> errors = Engine.InitValues(i_DicValues);
            if (i_DicValues.ContainsKey("ModelName"))
            {
                ModelName = i_DicValues["ModelName"];
            }
            foreach (VehicleWheel wheel in Wheels)
            {
                if (i_DicValues.ContainsKey("WheelManufactur"))
                {
                    wheel.Manufacturer = i_DicValues["WheelManufactur"];
                }
                if (i_DicValues.ContainsKey("WheelCurrentAirPressure"))
                {
                    if (float.TryParse(i_DicValues["WheelCurrentAirPressure"], out float airPressure))
                    {
                        if (airPressure <= wheel.MaxAirPressure) { wheel.CurrentAirPressure = airPressure; }
                        else 
                        { 
                            if(!errors.ContainsKey("WheelCurrentAirPressure"))
                                errors.Add("WheelCurrentAirPressure", $"Please enter air pressure less than Max : {wheel.MaxAirPressure}");
                        }
                    }
                    else
                    {
                        if (!errors.ContainsKey("WheelCurrentAirPressure"))
                            errors.Add("WheelCurrentAirPressure", "Please enter valid wheel Air Pressure");
                    }
                }
            }
            return errors;

        }
        public override string ToString()
        {
            int count = 1;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Licence Number: {LicenceNumber}\n Model Name: {ModelName}\n Num of wheels:{NumOfWheels}");
            foreach (VehicleWheel wheel in Wheels)
            {
                stringBuilder.AppendLine($"wheel {count++}:\n");
                stringBuilder.AppendLine($"{wheel}\n");
            }
            stringBuilder.Append("Engine: \n");
            stringBuilder.AppendLine(Engine.ToString());

            return stringBuilder.ToString();
        }

    }
    public enum eEngineType
    {
        Elecric, Fuel
    }

}
