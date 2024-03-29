﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class Engine : IParamDictionary
    {
        public float CurrentAmount { get; set; }
        public float MaxAmount { get; set; }

        public virtual Dictionary<string, string> GenerateParamsOutputs()
        {
            Dictionary<string, string> dicParams = new Dictionary<string, string>
            {
                { "CurrentAmount", "Enter current amount of energy" },
                { "MaxAmount", "Enter maximum amount of energy" }
            };
            return dicParams;
        }
        public override string ToString()
        {
            string str = $"Current amount: {CurrentAmount}\n Max amount: {MaxAmount}\n";
            return str;
        }
        public virtual Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues)
        {
            bool isValidamount;
            int amount;
            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (i_DicValues.ContainsKey("CurrentAmount"))
            {
                isValidamount = int.TryParse(i_DicValues["CurrentAmount"], out amount);
                if (!isValidamount)
                {
                    errors.Add("CurrentAmount", "please enter a valid amount");
                }
                else
                {
                    CurrentAmount = amount;
                }
            }
            if (i_DicValues.ContainsKey("MaxAmount"))
            {
                isValidamount = int.TryParse(i_DicValues["MaxAmount"], out amount);
                if (!isValidamount)
                {
                    errors.Add("MaxAmount", "please enter a valid amount");
                }
                else
                {
                    MaxAmount = amount;
                }
            }
            return errors;

        }
    }
}
