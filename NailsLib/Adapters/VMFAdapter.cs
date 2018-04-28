using NailsLib.Data;
using System;
using System.Collections;
using System.Text;
using VMFParser;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NailsLib.Adapters
{
    /**
     * <summary>
     * Provides an adapter to read and write Nails maps to VMF format.
     * <author>1upD</author>
     * </summary>
     */
    public class VMFAdapter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int _horizontal_scale;
        private int _vertical_scale;
        private string _filename;
        private VMF _vmf;

        public VMFAdapter(string filename, int horizontal_scale = 64, int vertical_scale = 64)
        {
            this._filename = filename;
            this._horizontal_scale = horizontal_scale;
            this._vertical_scale = vertical_scale;
            this._vmf = new VMF(); // Blank VMF in case export is called before input
        }

        /**
         * Exports the given NailsMap to a VMF file. Uses the file name passed in when this adapter was constructed.
         * <author>1upD</author>
         */
        public void Export(NailsMap map)
        {
            try
            {

            log.Info(string.Format("Exporting Nails map data to VMF file: {0}", this._filename));

            // Make a deep copy of the stored VMF to add instances to
            VMF output_vmf = new VMFParser.VMF(this._vmf.ToVMFStrings());
            int i = 0;

            foreach(NailsCube cube in map.NailsCubes)
            {
                int x_pos = cube.X * this._horizontal_scale;
                int y_pos = cube.Y * this._horizontal_scale;
                int z_pos = cube.Z * this._vertical_scale;

                var faces = cube.GetFaces();

                if (faces.Contains(NailsCubeFace.Floor))
                {
                    instanceData instance = new instanceData { filename = "Nails/" + cube.StyleName + "/floor.vmf", rotation = 0 };
                    instance.xpos = x_pos;
                    instance.ypos = y_pos;
                    instance.zpos = z_pos;
                    instance.ent_id = i++;
                    insertInstanceIntoVMF(instance, output_vmf);
                }

                if (faces.Contains(NailsCubeFace.Front))
                {
                    instanceData instance = new instanceData { filename = "Nails/" + cube.StyleName + "/wall.vmf", rotation = 0 };
                    instance.xpos = x_pos;
                    instance.ypos = y_pos;
                    instance.zpos = z_pos;
                    instance.ent_id = i++;
                    insertInstanceIntoVMF(instance, output_vmf);
                }

                if (faces.Contains(NailsCubeFace.Left))
                {
                    instanceData instance = new instanceData { filename = "Nails/" + cube.StyleName + "/wall.vmf", rotation = 90 };
                    instance.xpos = x_pos;
                    instance.ypos = y_pos;
                    instance.zpos = z_pos;
                    instance.ent_id = i++;
                    insertInstanceIntoVMF(instance, output_vmf);
                }

                if (faces.Contains(NailsCubeFace.Back))
                {
                    instanceData instance = new instanceData { filename = "Nails/" + cube.StyleName + "/wall.vmf", rotation = 180 };
                    instance.xpos = x_pos;
                    instance.ypos = y_pos;
                    instance.zpos = z_pos;
                    instance.ent_id = i++;
                    insertInstanceIntoVMF(instance, output_vmf);
                }

                if (faces.Contains(NailsCubeFace.Right))
                {
                    instanceData instance = new instanceData { filename = "Nails/" + cube.StyleName + "/wall.vmf", rotation = 270 };
                    instance.xpos = x_pos;
                    instance.ypos = y_pos;
                    instance.zpos = z_pos;
                    instance.ent_id = i++;
                    insertInstanceIntoVMF(instance, output_vmf);
                }

                if (faces.Contains(NailsCubeFace.Ceiling))
                {
                    instanceData instance = new instanceData { filename = "Nails/" + cube.StyleName + "/ceiling.vmf", rotation = 0 };
                    instance.xpos = x_pos;
                    instance.ypos = y_pos;
                    instance.zpos = z_pos;
                    instance.ent_id = i++;
                    insertInstanceIntoVMF(instance, output_vmf);
                }
            }


            using (StreamWriter sw = new StreamWriter(new FileStream(this._filename, FileMode.Create)))
            {
                var vmfStrings = output_vmf.ToVMFStrings();
                foreach (string line in vmfStrings)
                {
                    sw.WriteLine(line);
                }
            }

            log.Info(string.Format("Wrote to VMF file: {0}", this._filename));

            } catch (Exception e)
            {
                log.Error("VMFAdapter.Export(): Caught exception: ", e);
                log.Info(string.Format("Failed to write to VMF file: {0}", this._filename));
            }


        }

        private static void insertInstanceIntoVMF(instanceData instance, VMF output_vmf)
        {
            try
            {
                IList<int> list = null;

                VBlock instance_block = new VBlock("entity");
                instance_block.Body.Add(new VProperty("id", instance.ent_id.ToString()));
                instance_block.Body.Add(new VProperty("classname", "func_instance"));
                instance_block.Body.Add(new VProperty("angles", "0 " + instance.rotation + " 0"));
                instance_block.Body.Add(new VProperty("origin", instance.xpos + " " + instance.ypos + " " + instance.zpos));
                instance_block.Body.Add(new VProperty("file", instance.filename));
                output_vmf.Body.Add(instance_block);
            }
            catch (Exception e)
            {
                log.Error("VMFAdapter.insertInstanceIntoVMF(): Caught exception: ", e);
                log.Info(string.Format("Failed to insert instance: {0}", instance.filename));
            }
        }

        private class instanceData
        {
            public string filename { get; set; }
            public int rotation { get; set; }

            public int ent_id;

            public int xpos;
            public int ypos;
            public int zpos;

            public instanceData Copy()
            {
                var newInstanceData = new instanceData();
                newInstanceData.filename = this.filename;
                newInstanceData.rotation = this.rotation;
                newInstanceData.ent_id = this.ent_id;
                newInstanceData.xpos = this.xpos;
                newInstanceData.ypos = this.ypos;
                newInstanceData.zpos = this.zpos;

                return newInstanceData;
            }
        }

        /**
         * Imports a VMF file to create a NailsMap. Uses the file name passed in when this adapter was constructed.
         * <author>1upD</author>
         * TODO Add more error checking
         */
        public NailsMap Import()
        {
            try {
            // Reset adapter VMF
            this._vmf = new VMF();


            NailsMap map = new NailsMap();

            List<string> lines = new List<string>();

            using (StreamReader sr = new StreamReader(new FileStream(this._filename, FileMode.OpenOrCreate)))
            {
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }
            }


            VMF input_vmf = new VMF(lines.ToArray());

            for (int blockIndex = 0; blockIndex < input_vmf.Body.Count; blockIndex++)
            {
                // Should this object be included when the VMF is output?
                bool includeThisBlock = true;
                // Get the next object from the VMF
                var obj  = input_vmf.Body[blockIndex];

                try
                {
                // Try to cast to block
                VMFParser.VBlock block = (VMFParser.VBlock)obj;
                

                // If this block is an entity
                if (block.Name == "entity")
                {
                    
                    var body = block.Body;
                    foreach (var innerObj in body) {
                        try
                        {
                            VMFParser.VProperty prop = (VMFParser.VProperty)innerObj;

                            // If this block is an instance
                            if (prop.Name == "classname" && prop.Value == "func_instance")
                            {
                                VProperty fileProperty = (VProperty)body.Where(p => p.Name == "file").ToList()[0];
                                var filePathParts = fileProperty.Value.Split('/');

                                // If this is a nails instance
                                if (filePathParts[0] == "Nails")
                                {
                                    // Get position
                                    VProperty originProperty = (VProperty)body.Where(p => p.Name == "origin").ToList()[0];
                                    var originParts = originProperty.Value.Split(' ');
                                    int x_pos = int.Parse(originParts[0]) / this._horizontal_scale;
                                    int y_pos = int.Parse(originParts[1]) / this._horizontal_scale;
                                    int z_pos = int.Parse(originParts[2]) / this._vertical_scale;
                                    string style = filePathParts[1];
                                    map.MarkLocation(style, x_pos, y_pos, z_pos);

                                        // Remove this block from the vmf
                                        includeThisBlock = false;

                                }

                                break;
                            }

                        } catch (InvalidCastException e)
                        {
                                    log.Error("Invalid cast exception. VMF Object is not a VProperty.", e);
                        }
                    }
                }
            } catch(InvalidCastException e)
                {
                        log.Error("Invalid cast exception. VMF object is not a VBlock.", e);
                }
                // If this object is not a Nails block, add it to the adapter's VMF to be output later
                if (includeThisBlock)
                {
                    this._vmf.Body.Add(obj);
                }

            }

            return map;

        } catch (Exception e)
            {
                log.Error("VMFAdapter.Import(): Caught exception: ", e);
                log.Info(string.Format("Failed to read from VMF file: {0}", this._filename));
            }

            return null;
}


    }
}
