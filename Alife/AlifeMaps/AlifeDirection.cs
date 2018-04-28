using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alife.AlifeMaps
{
    public enum AlifeDirection
    {
        East = 0,
        North = 1,
        West = 2,
        South = 3,
        Down = 4,
        Up = 5
    }

    public class AlifeDirectionOperations
    {
        /**
         * <summary>
         * This silly static method rotates a direction 90 degrees by a given number of times i.
         * I had to write it to work around C# not supporting canonical modulus.
         * <author>1upD</author>
         * </summary>
         */
        public static AlifeDirection Add(AlifeDirection dir, int i)
        {
            AlifeDirection result;
            // Cast direction to int
            int dirInt = (int)dir;
            // Add the given integer
            int sum = dirInt + i;
            // Mod by four
            int mod = sum % 4;
            // If the result is negative, subtract from 4 for canonical modulus!
            if(mod < 0)
            {
                mod = 4 + mod;
            }

            // Cast back to direction
            result = (AlifeDirection)mod;
            return result;
        }
    }
    
}
