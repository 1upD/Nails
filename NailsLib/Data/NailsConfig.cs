using Alife.Agents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NailsLib.Data
{
	[Serializable]
    public class NailsConfig : ISerializable

    {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public List<BaseAgent> Agents;
		public List<NailsTheme> Themes;

		public NailsConfig()
		{
		}

        public NailsTheme GetTheme(string aThemeName)
        {
            return this.Themes.Where(t => t.Name == aThemeName).ToList()[0];
        }

		/**
	     * Static method to read configuration from an XML file and return a NailsConfig object.
         * <author>1upD</author>
         */
		public static NailsConfig ReadConfiguration(string filepath)
		{
			NailsConfig config = null;
			try
			{
				log.Info(string.Format("Loading Nails configuration from file: {0}", filepath));
				XmlSerializer serializer = new XmlSerializer(typeof(NailsConfig));

				StreamReader reader = new StreamReader(filepath);
				config = (NailsConfig)serializer.Deserialize(reader);
				reader.Close();
			}
			catch (Exception e)
			{
				log.Error(string.Format("Error reading Nails configuration in file: {0}", filepath), e);
			}

			return config;
		}

		/**
         * Static method to write Nails configuration to a file
         * <author>1upD</author>
         */
		public static void WriteConfiguration(string filepath,  NailsConfig config)
		{
			try
			{
				log.Info(string.Format("Writing Nails configuration to file: {0}", filepath));

				XmlSerializer serializer = new XmlSerializer(typeof(NailsConfig));

				StreamWriter writer = new StreamWriter(filepath);
				serializer.Serialize(writer, config);
				writer.Close();
			}
			catch (Exception e)
			{
				log.Error(string.Format("Error writing Nails configuration to file: {0}", filepath), e);
			}
		}

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
