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
    public class JSONAdapterTest
    {
        [TestMethod]
        public void TestImportExport()
        {
            // Setup: Create a NailsMap
            NailsMap map = new NailsMap();

            // Mark a location
            map.MarkLocation("TestStyle", 0, 1, 0);

            // Create a new JSON adapter
            JSONAdapter adapter = new JSONAdapter("testoutput.json");

            // Export the map
            adapter.Export(map);

            // Import the map
            var newMap = adapter.Import();

            // Compare
            Assert.IsNotNull(newMap);
            NailsCube cube = newMap.GetCube(0, 1, 0);
            Assert.IsNotNull(cube);
            Assert.AreEqual("TestStyle", cube.StyleName);

        }

    }
}
