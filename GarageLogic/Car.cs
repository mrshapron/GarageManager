using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_MaxPressureCar = 30;
        private const float k_MaxFuelCar = 58f;
        private const float k_MaxBatteryCar = 4.8f;
        public eCarColor CarColor { get; set; }
        public eNumOfDoors NumOfDoors { get; set; }

        public Car() : base(i_NumOfWheels: 4) { }

        public override void SetWheels()
        {
            for (int i = 0; i < NumOfWheels; i++)
            {
                VehicleWheel Wheel = new VehicleWheel(k_MaxPressureCar);
                Wheels.Add(Wheel);
            }
        }
        public override void SetEngine(eEngineType i_EngineType)
        {
            switch (i_EngineType)
            {
                case eEngineType.Fuel:
                    FuelEngine FEngine = new FuelEngine();
                    FEngine.FuelType = eFuelType.Octan95;
                    FEngine.MaxAmount = k_MaxFuelCar;
                    this.Engine = FEngine;
                    break;
                case eEngineType.Elecric:
                    ElectricEngine EEngine = new ElectricEngine();
                    EEngine.MaxAmount = k_MaxBatteryCar;
                    this.Engine = EEngine;
                    break;

            }
        }

        public override Dictionary<string, string> GenerateParamsOutputs()
        {
            Dictionary<string, string> dicParams = base.GenerateParamsOutputs();
            dicParams.Add("CarColor", "Enter color of the car\n" +
                 "1 - Blue\n2 - White\n3 - Red\n4 - Yellow"); 
            return dicParams;
        }
        public override Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues)
        {
            bool isValidColor;
            eCarColor color;
            Dictionary<string, string> errors = base.InitValues(i_DicValues);
            if (i_DicValues.ContainsKey("CarColor"))
            {
                isValidColor = Enum.TryParse(i_DicValues["CarColor"], out color);
                if (!isValidColor)
                {
                    errors.Add("CarColor", "car color out of range (1-4)");
                }
                else
                {
                    CarColor = color;
                }
            }
            return errors;

        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("=========== Car Details ==========");
            stringBuilder.Append($"Car color: {CarColor}\n Num of doors: {NumOfDoors}\n");
            stringBuilder.Append(base.ToString());
            stringBuilder.AppendLine("====================================");
            return stringBuilder.ToString();
        }
    }
    public enum eCarColor
    {
        Blue = 1, White = 2, Red = 3, Yellow =4
    }
    public enum eNumOfDoors
    {
        Two, Three, Four, Five
    }
}
