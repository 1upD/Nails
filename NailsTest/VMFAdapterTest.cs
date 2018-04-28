using Microsoft.VisualStudio.TestTools.UnitTesting;
using NailsLib.Data;
using NailsLib.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsTest
{
    [TestClass]
    public class VMFAdapterTest
    {
        [TestMethod]
        public void TestVMFExportImport()
        {
            // Setup: Create a NailsMap
            NailsMap map = new NailsMap();

            // Mark a location
            map.MarkLocation("TestStyle", 0, 0, 0);
            map.MarkLocation("TestStyle", 0, 1, 0);
            map.MarkLocation("TestStyle", 1, 1, 0);

            // Create a new JSON adapter
            VMFAdapter adapter = new VMFAdapter("testoutput.vmf", 64, 64);

            // Export the map
            adapter.Export(map);

            // Import the map
            var newMap = adapter.Import();

            // Compare
            Assert.IsNotNull(newMap);
            NailsCube cube = newMap.GetCube(0, 0, 0);
            Assert.IsNotNull(cube);
            Assert.AreEqual("TestStyle", cube.StyleName);
            cube = newMap.GetCube(0, 1, 0);
            Assert.IsNotNull(cube);
            Assert.AreEqual("TestStyle", cube.StyleName);
            cube = newMap.GetCube(1, 1, 0);
            Assert.IsNotNull(cube);
            Assert.AreEqual("TestStyle", cube.StyleName);
        }

        [TestMethod]
        public void TestVMFImportExport()
        {
            // Create a new JSON adapter
            VMFAdapter adapter = new VMFAdapter("testinput.vmf", 64, 64);

            // Setup: Create a NailsMap
            NailsMap map = adapter.Import();

            // Export the map
            adapter.Export(map);

            // Import the map
            var newMap = adapter.Import();

            // Compare
            Assert.IsNotNull(newMap);
            NailsCube cube = newMap.GetCube(0, 0, 0);
            Assert.IsNotNull(cube);
            Assert.AreEqual("TestStyle", cube.StyleName);
            cube = newMap.GetCube(0, 1, 0);
            Assert.IsNotNull(cube);
            Assert.AreEqual("TestStyle", cube.StyleName);
            cube = newMap.GetCube(1, 1, 0);
            Assert.IsNotNull(cube);
            Assert.AreEqual("TestStyle", cube.StyleName);
        }

    }
}
