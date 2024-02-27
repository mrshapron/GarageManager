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

        public void GetFuel(float NumOfLitters)
        {
            this.CurrentAmount = NumOfLitters;
            if (this.CurrentAmount > this.MaxAmount)
            {
                this.CurrentAmount = this.MaxAmount;
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

    }
    public enum eFuelType
    {
        Soler = 1, Octan95, Octan96, Octan98
    }
}
