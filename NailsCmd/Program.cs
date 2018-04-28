using Alife.Agents;
using Alife.AlifeMaps;
using Alife.Configurator;
using CommandLine;
using log4net;
using log4net.Config;
using NailsLib.Adapters;
using NailsLib.Data;
using NailsLib.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NailsCmd
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /**
         * Inner class for command line options. Used with CommandLineParser library.
         * <author>1upD</author>
         */
        class Options
        {
            [Option('f', "file", Required = false, Default = "alife.xml", HelpText = "XML containing agent configuration.")]
            public string InputFileName { get; set; }

            [Option('o', "output", Required = false, Default = "output.vmf", HelpText = "Filename to write out to.")]
            public string OutputFileName { get; set; }

            [Option('t', "lifetime", Required = false, Default = 40, HelpText = "Number of steps in the simulation.")]
            public int Lifetime { get; set; }
        }


        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            log.Info("Starting NailsCmd random map generator...");

            CommandLine.Parser.Default.ParseArguments<Options>(args)
             .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
             .WithNotParsed<Options>((errs) => HandleParseError(errs));

            log.Info("NailsCmd completed.");

        }

        /**
         * Main method given command line options.
         * <author>1upD</author>
         */
        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            try
            {
                // Read agents from XML file
                List<BaseAgent> agents = AlifeConfigurator.ReadConfiguration(opts.InputFileName);

                // Create a new nails map
                NailsMap nailsMap = new NailsMap();
                AlifeMap alifeMap = new NailsAlifeMap(nailsMap);
                alifeMap.Agents = agents;

                // Run the simulation
                AlifeSimulation.Simulate(ref alifeMap, opts.Lifetime);

                // Write out to a file
                VMFAdapter vmfAdapter = new VMFAdapter(opts.OutputFileName);
                vmfAdapter.Export(nailsMap);
            } catch(Exception e)
            {
                log.Error("NailsCmd caught fatal exception: ", e);
            }

        }

        /**
         * Handles incorrectly parsed command line options.
         * <author>1upD</author>
         */
        private static object HandleParseError(object errs)
        {
            log.Error(string.Format("Parsing error: {0}", errs.ToString()));
            return null;
        }
    }
}
