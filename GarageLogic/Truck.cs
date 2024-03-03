using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    internal class Truck : Vehicle
    {
        private const int k_MaxPressureTruck = 28;
        private const float k_MaxFuelTruck = 110f;

        public bool isDangerousMaterials { get; set; }
        public int CarriageCapacity { get; set; }
        public Truck() : base(i_NumOfWheels: 12) { }

        public override void SetWheels()
        {
            for (int i = 0; i < NumOfWheels; i++)
            {
                VehicleWheel Wheel = new VehicleWheel(k_MaxPressureTruck);
                Wheels.Add(Wheel);
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("=========== Truck Details ==========");
            stringBuilder.Append($"Carriage Capacity: {CarriageCapacity}\n is there Dangerous Materials : {isDangerousMaterials}\n");
            stringBuilder.Append(base.ToString());
            stringBuilder.AppendLine("====================================");
            return stringBuilder.ToString();
        }

        public override void SetEngine(eEngineType i_EngineType)
        {
            FuelEngine FEngine = new FuelEngine();
            FEngine.FuelType = eFuelType.Soler;
            FEngine.MaxAmount = k_MaxFuelTruck;
            this.Engine = FEngine;
        }
        
        public override Dictionary<string, string> GenerateParamsOutputs()
        {
            Dictionary<string, string> dicParams = base.GenerateParamsOutputs();
            dicParams.Add("isDangerousMaterials",
                    "is there dangerous materials?\n" +
                "1- yes\n" +
                "2- no");
            dicParams.Add("CarriageCapacity", "Enter carriage capacity");

            return dicParams;
        }

        public override Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues)
        {
            bool isValidDangerous, isValidCarriage, isDangerous;
            int capacity;

            Dictionary<string, string> errors = base.InitValues(i_DicValues);
            if (i_DicValues.ContainsKey("isDangerousMaterials"))
            {
                isValidDangerous = Enum.TryParse(i_DicValues["isDangerousMaterials"], out isDangerous);
                if (!isValidDangerous)
                {
                    errors.Add("isDangerousMaterials", "please enter a 1 or 2 for dangerous materials");
                }
                else
                {
                    isDangerousMaterials = isDangerous;
                }
            }
            if (i_DicValues.ContainsKey("CarriageCapacity"))
            {
                isValidCarriage = Enum.TryParse(i_DicValues["CarriageCapacity"], out capacity);
                if (!isValidCarriage)
                {
                    errors.Add("CarriageCapacity", "please enter a valid capcity");
                }
                else
                {
                    CarriageCapacity = capacity;
                }
            }
            return errors;
        }
    }
}

