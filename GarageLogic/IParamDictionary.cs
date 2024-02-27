using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public interface IParamDictionary
    {
        /// <summary>
        /// Generate Dictionary for user output each param 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GenerateParamsOutputs();
        /// <summary>
        /// Get Dictionary of values and intialize it in the state of the object
        /// </summary>
        /// <param name="i_DicValues"> the dictionary with values from user</param>
        /// <returns>dictionary of errors for each param if needed</returns>
        Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues);
    }
}
