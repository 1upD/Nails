using Alife.Agents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Alife.Configurator
{
    /**
     * Static class to provide methods to read and write agent configurations.
     * <author>1upD</author>
     */
    public static class AlifeConfigurator
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /**
         * Static method to read configuration from an XML file and return a list of BaseAgent objects.
         * <author>1upD</author>
         */
        public static List<BaseAgent> ReadConfiguration(string filepath)
        {
            List<BaseAgent> agents = null;
            try
            {
                log.Info(string.Format("Loading artificial life configuration from file: {0}", filepath));
                XmlSerializer serializer = new XmlSerializer(typeof(List<BaseAgent>));

                StreamReader reader = new StreamReader(filepath);
                agents = (List<BaseAgent>)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception e)
            {
                log.Error(string.Format("Error reading artificial life configuration in file: {0}", filepath), e);
            }

            return agents;
        }

        /**
         * Static method to read configuration from an XML file and return a list of BaseAgent objects.
         * <author>1upD</author>
         */
        public static void WriteConfiguration(string filepath, List<BaseAgent> agents)
        {
            try
            {
                log.Info(string.Format("Writing artificial life configuration from file: {0}", filepath));

                XmlSerializer serializer = new XmlSerializer(typeof(List<BaseAgent>));

                StreamWriter writer = new StreamWriter(filepath);
                serializer.Serialize(writer, agents);
                writer.Close();
            }
            catch (Exception e)
            {
                log.Error(string.Format("Error writing artificial life configuration to file: {0}", filepath), e);
            }
        }



    }
}
