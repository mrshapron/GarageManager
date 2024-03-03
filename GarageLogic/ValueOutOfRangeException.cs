using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException() { }

        public ValueOutOfRangeException(string message) : base(message) { }

        public ValueOutOfRangeException(string message, Exception innerException) : base(message, innerException) { }

        public ValueOutOfRangeException(string paramName, float minValue, float maxValue)
            : base($"The value for {paramName} is outside the valid range [{minValue}, {maxValue}].") { }
    }
}
