using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class FuelEngine : Engine
    {
        public eFuelType FuelType { get; set; }

        public void GetFuel(float i_NumOfLitters, eFuelType i_FuelType)
        {
            this.CurrentAmount = i_NumOfLitters;
            if (this.CurrentAmount + i_NumOfLitters <= this.MaxAmount &&
                i_FuelType == FuelType)
            {
                this.CurrentAmount += i_NumOfLitters;
            }
        }
        public override string PrintParameters()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.PrintParameters());
            sb.AppendLine("FuelType");
            return sb.ToString();
        }
        public override Dictionary<string, string> GenerateParamsOutputs()
        {
            Dictionary<string, string> dicParams = base.GenerateParamsOutputs();
            dicParams.Add("FuelType", "Enter fuel type");
            return dicParams;
        }
        public override Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues)
        {
            bool isValidType;
            eFuelType type;
            Dictionary<string, string> errors = base.InitValues(i_DicValues);
            if (i_DicValues.ContainsKey("FuelType"))
            {
                isValidType = Enum.TryParse(i_DicValues["FuelType"], out type);
                if (!isValidType)
                {
                    errors.Add("FuelType", "please enter a valid fuel type");
                }
                else
                {
                    FuelType = type;
                }
            }
            return errors;

        }
        public override string ToString()
        {
            string str = $"Fuel Type: {FuelType}\n";
            StringBuilder stringBuilder = new StringBuilder(base.ToString());
            stringBuilder.Append(str);
            return str;
        }

    }
    public enum eFuelType
    {
        Soler = 1, Octan95, Octan96, Octan98
    }
}
