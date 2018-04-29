
using Alife.Agents;
using Alife.AlifeMaps;
using NailsLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsLib.Decorators
{
    /**
     * <summary>
     * Wraps the AlifeMap class from the Alife library. Provides Alife interface to NailsMap.
     * <author>1upD</author>
     * </summary>
      */
    public class NailsAlifeMap : AlifeMap
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<BaseAgent> Agents { get; set; }
        public NailsMap map;

        public NailsAlifeMap()
        {
            this.Agents = new List<BaseAgent>();
            map = new NailsMap();
        }

        public NailsAlifeMap(NailsMap map)
        {
            this.Agents = new List<BaseAgent>();
            this.map = map;
        }


        public string GetLocation(int x, int y, int z)
        {
            NailsCube cube = null;
            try
            {
                cube = this.map.GetCube(x, y, z);
            } catch(Exception e)
            {
                log.Error("NailsAlifeMap.GetLocation(): Caught exception: ", e);
            }

            if(cube == null)
            {
                return null;
            }

            return cube.StyleName;

        }

        public void MarkLocation(string aString, int x, int y, int z)
        {
            this.map.MarkLocation(aString, x, y, z);
        }
    }
}
