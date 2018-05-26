using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsLib.Data
{
	public class NailsTheme
	{
		public string Name;

		private Dictionary<string, List<string>> _themeDictionary;

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
			if (this._themeDictionary.ContainsKey(aInstanceName))
			{
				return this._themeDictionary[aInstanceName];
			} else
			{
				return null;
			}
		}

		public void SetThemeDictionary(Dictionary<string, List<string>> themeDictionary)
		{
			this._themeDictionary = themeDictionary;
		}

	}


}
