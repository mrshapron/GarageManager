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
            if(i_FuelType != FuelType)
            {
                throw new ArgumentException("You Entered Fuel from wrong Type!");
            }
            if (this.CurrentAmount + i_NumOfLitters > this.MaxAmount)
            {
                throw new ValueOutOfRangeException("Current num of Litters", 0, MaxAmount);
            }
            this.CurrentAmount += i_NumOfLitters;
        }


        public override Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues)
        {
            Dictionary<string, string> errors = base.InitValues(i_DicValues);
            return errors;

        }

        public override string ToString()
        {
            string str = $"Fuel Type: {FuelType}\n";
            StringBuilder stringBuilder = new StringBuilder(base.ToString());
            stringBuilder.Append(str);
            return stringBuilder.ToString();
        }

    }
    public enum eFuelType
    {
        Soler = 1, Octan95, Octan96, Octan98
    }
}