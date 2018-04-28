using Alife.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alife.AlifeMaps
{
    /**
     * <summary>
     * Interface supplied for alife agents.
     * Intended to be used in a decorator of whatever map class
     * so that this library can be used for any project.
     * <author>1upD</author>
     * </summary>
     */
    public interface AlifeMap
    {
        /**
         * <summary>
         * Gets the marked string at a location in 3D space.
         * </summary>
         */
        string GetLocation(int x, int y, int z);
        /**
         * <summary>
         * Marks a location in 3D space with the given string.
         * </summary>
         */
        void MarkLocation(string aString, int x, int y, int z);

        /**
         * <summary>
         * Every alife environment needs a list of agents
         * </summary>
         */
        List<BaseAgent> Agents { get; set; }
    }

}
