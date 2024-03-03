using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class Motorcycle: Vehicle
    {
        private const int k_MaxPressureMotorcycle = 29;
        private const float k_MaxFuelMotorcycle = 5.8f;
        private const float k_MaxBatteryMotorcycle = 2.8f;
        public eLiceneceType LicenceType { get; set; }
        public int EngineCapacity { get; set; }

        public Motorcycle() : base(i_NumOfWheels:2) { }

        public override void SetWheels()
        {
            for (int i = 0; i < NumOfWheels; i++)
            {
                VehicleWheel Wheel = new VehicleWheel(k_MaxPressureMotorcycle);
                Wheels.Add(Wheel);
            }
        }
        public override void SetEngine(eEngineType i_EngineType)
        {
            switch (i_EngineType)
            {
                case eEngineType.Fuel:
                    FuelEngine FEngine = new FuelEngine();
                    FEngine.FuelType = eFuelType.Octan98;
                    FEngine.MaxAmount = k_MaxFuelMotorcycle;
                    this.Engine = FEngine;
                    break;
                case eEngineType.Elecric:
                    ElectricEngine EEngine = new ElectricEngine();
                    EEngine.MaxAmount = k_MaxBatteryMotorcycle;
                    this.Engine = EEngine;
                    break;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("======= Motorcycle Details =========");
            stringBuilder.Append($"Licence Type: {LicenceType}\n Engine Capacity: {EngineCapacity}\n");
            stringBuilder.Append(base.ToString());
            stringBuilder.AppendLine("====================================");
            return stringBuilder.ToString();
        }
        public override Dictionary<string, string> GenerateParamsOutputs()
        {
            Dictionary<string, string> dicParams = base.GenerateParamsOutputs();
            dicParams.Add("LicenceType", "Enter licence type\n1:A1\n2:A2\n3:AB\n4:B2");
            dicParams.Add("EngineCapacity", "Enter engine capacity");
            
            return dicParams;
        }
        public override Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues)
        {
            bool isValidType,isValidCapacity;
            eLiceneceType type;
            int capacity;
            Dictionary<string, string> errors = base.InitValues(i_DicValues);
            if (i_DicValues.ContainsKey("LicenceType"))
            {
                isValidType = Enum.TryParse(i_DicValues["LicenceType"], out type);
                if (!isValidType)
                {
                    errors.Add("LicenceType", "please enter a valid licence type");
                }
                else
                {
                    LicenceType = type;
                }
            }
            if (i_DicValues.ContainsKey("EngineCapacity"))
            {
                isValidCapacity = int.TryParse(i_DicValues["EngineCapacity"], out capacity);
                if (!isValidCapacity)
                {
                    errors.Add("EngineCapacity", "please enter a valid engine capacity");
                }
                else
                {
                    EngineCapacity = capacity;
                }
            }
            return errors;

        }
    }
     public enum eLiceneceType
    {
        A1=1,A2=2,AB=3,B2=4
    } 
}
