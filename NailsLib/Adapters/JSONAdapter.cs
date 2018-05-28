using NailsLib.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NailsLib.Adapters
{
    /**
    * <summary>
    * Provides an adapter to read and write Nails maps to JSON format.
    * <author>1upD</author>
    * </summary>
    */
    public class JSONAdapter : NailsMapAdapter
    {

        private String _filename;
        private JsonSerializer _serializer;

        /**
         * Adapter to read and write NailsMaps to JSON files.
         * Accepts on argument, the name of a file to read and write from.
         * <author>1upD</author>
         */
        public JSONAdapter(string filename)
        {
            this._filename = filename;
            this._serializer = new JsonSerializer();
        }


        /**
         * Exports the given NailsMap to a JSON file. Uses the file name passed in when this adapter was constructed.
         * <author>1upD</author>
         */
        public void Export(NailsMap map)
        {

            using (TextWriter w = new StreamWriter(new FileStream(this._filename, FileMode.OpenOrCreate)))
            using (JsonWriter writer = new JsonTextWriter(w))
            {
                this._serializer.Serialize(writer, map);
            }
        }

        /**
         * Imports a JSON file to create a NailsMap. Uses the file name passed in when this adapter was constructed.
         * <author>1upD</author>
         */
        public NailsMap Import()
        {
            using (TextReader r = new StreamReader(new FileStream(this._filename, FileMode.OpenOrCreate)))
            using (JsonReader reader = new JsonTextReader(r))
            {
                return this._serializer.Deserialize<NailsMap>(reader);
            }
        }

    }
}
