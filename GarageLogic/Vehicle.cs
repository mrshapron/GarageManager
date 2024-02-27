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

        public virtual string PrintParameters()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("NumOfWheels,ModelName,LicenceNumber,RemainingBatteryPrecent,Engine:");
            sb.AppendLine(Engine.PrintParameters());
            sb.AppendLine("Wheels:");
            // sb.AppendLine(VehicleWheel.PrintParameters());
            return sb.ToString();
        }
        public virtual Dictionary<string, string> GenerateParamsOutputs()
        {
            Dictionary<string, string> dicParams = new Dictionary<string, string>
            {
                { "ModelName", "Enter model of the vehicle" },

            };

            dicParams = dicParams.Union(Engine.GenerateParamsOutputs()).
                ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return dicParams;
        }
        public virtual Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues)
        {
            if (i_DicValues.ContainsKey("ModelName"))
            {
                ModelName = i_DicValues["ModelName"];
            }
            Dictionary<string, string> errors = Engine.InitValues(i_DicValues);
            return errors;
            
        }

    }
    public enum eEngineType
    {
        Elecric, Fuel
    }

}
