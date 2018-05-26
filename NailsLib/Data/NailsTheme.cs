using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NailsLib.Data
{
	public class NailsTheme
	{
		public string Name;

        public List<NailsInstance> Instances;

        /**
		 * <summary> 
		 * Returns a list of filepaths for a given instance name.
		 * If this theme does not have any instances with that name,
		 * returns null.
		 * <author>1upD</author>
		 * </summary>
		 */
        public List<string> GetInstancePaths(string aInstanceName)
        {
            // Unreadable linq expression gets the filepath list from an instance by instance name
            return Instances.Where(i => i.Name == aInstanceName).Select(i => i.Filepaths).ToList()[0];
        }
    }


}
