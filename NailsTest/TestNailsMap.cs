using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NailsLib.Data;

namespace NailsTest
{
    [TestClass]
    public class TestNailsMap
    {
        [TestMethod]
        public void TestGetCube_NothingAtLocation()
        {
            // Setup: Create a NailsMap
            NailsMap map = new NailsMap();

            // If there is no cube at the location
            NailsCube cube = map.GetCube(0, 0, 0);

            Assert.IsNull(cube);
        }

        [TestMethod]
        public void TestGetCube()
        {
            // Setup: Create a NailsMap
            NailsMap map = new NailsMap();

            // Mark a location
            map.MarkLocation("TestStyle", 0, 0, 0);

            // If there is no cube at the location
            NailsCube cube = map.GetCube(0, 0, 0);

            Assert.IsNotNull(cube);
            Assert.AreEqual(cube.StyleName, "TestStyle");
        }

        [TestMethod]
        public void TestGetTwoCubes()
        {
            // Setup: Create a NailsMap
            NailsMap map = new NailsMap();

            // Mark a location
            map.MarkLocation("TestStyle", 0, 0, 0);
            map.MarkLocation("TestStyle", 0, 1, 0);

            // If there is no cube at the location
            NailsCube cube = map.GetCube(0, 0, 0);

            Assert.IsNotNull(cube);
            Assert.AreEqual(cube.StyleName, "TestStyle");

            // Check the second cube
            cube = map.GetCube(0, 1, 0);

            Assert.IsNotNull(cube);
            Assert.AreEqual(cube.StyleName, "TestStyle");

        }

        [TestMethod]
        public void TestGetTwoCubes_OtherLocation()
        {
            // Setup: Create a NailsMap
            NailsMap map = new NailsMap();

            // Mark a location
            map.MarkLocation("TestStyle", 0, 0, 0);
            map.MarkLocation("TestStyle", 0, 1, 0);

            // If there is no cube at the location
            NailsCube cube = map.GetCube(0, -1, 0);
            Assert.IsNull(cube);

            cube = map.GetCube(1, 0, 0);
            Assert.IsNull(cube);

            cube = map.GetCube(-1, 0, 0);
            Assert.IsNull(cube);

            cube = map.GetCube(0, -1, 0);
            Assert.IsNull(cube);

            cube = map.GetCube(0, 0, -1);
            Assert.IsNull(cube);

            cube = map.GetCube(0, 0, 1);
            Assert.IsNull(cube);
        }


        [TestMethod]
        public void TestCubeMarkedMultipleTimes()
        {
            // Setup: Create a NailsMap
            NailsMap map = new NailsMap();

            // Mark a location
            map.MarkLocation("TestStyle", 0, 0, 0);
            map.MarkLocation("TestStyle", 0, 0, 0);

            // Get the cube
            NailsCube cube = map.GetCube(0, 0, 0);

            // Should act as though there is only one cube
            Assert.IsNotNull(cube);
            Assert.AreEqual(cube.StyleName, "TestStyle");
        }

        [TestMethod]
        public void TestFacesTwoCubes()
        {
            // Setup: Create a NailsMap
            NailsMap map = new NailsMap();

            // Mark a location
            map.MarkLocation("TestStyle", 0, 0, 0);
            map.MarkLocation("TestStyle", 0, 1, 0);

            // If there is no cube at the location
            NailsCube cube = map.GetCube(0, 0, 0);

            Assert.IsNotNull(cube);
            Assert.AreEqual(cube.StyleName, "TestStyle");

            // Check the second cube
            cube = map.GetCube(0, 1, 0);

            // Look at faces
            var faces = cube.GetFaces();
            Assert.IsTrue(faces.Contains(NailsCubeFace.Floor));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Front));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Left));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Back));
            Assert.IsFalse(faces.Contains(NailsCubeFace.Right));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Ceiling));

            // Check the first cube
            cube = map.GetCube(0, 0, 0);

            // Look at faces
            faces = cube.GetFaces();
            Assert.IsTrue(faces.Contains(NailsCubeFace.Floor));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Front));
            Assert.IsFalse(faces.Contains(NailsCubeFace.Left));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Back));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Right));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Ceiling));

        }


        [TestMethod]
        public void TestFacesTwoCubes_FloorCeiling()
        {
            // Setup: Create a NailsMap
            NailsMap map = new NailsMap();

            // Mark a location
            map.MarkLocation("TestStyle", 0, 0, 0);
            map.MarkLocation("TestStyle", 0, 0, 1);

            // If there is no cube at the location
            NailsCube cube = map.GetCube(0, 0, 0);

            Assert.IsNotNull(cube);
            Assert.AreEqual(cube.StyleName, "TestStyle");

            // Check the second cube
            cube = map.GetCube(0, 0, 1);

            // Look at faces
            var faces = cube.GetFaces();
            Assert.IsFalse(faces.Contains(NailsCubeFace.Floor));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Front));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Left));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Back));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Right));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Ceiling));

            // Check the first cube
            cube = map.GetCube(0, 0, 0);

            // Look at faces
            faces = cube.GetFaces();
            Assert.IsTrue(faces.Contains(NailsCubeFace.Floor));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Front));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Left));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Back));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Right));
            Assert.IsFalse(faces.Contains(NailsCubeFace.Ceiling));

        }

        [TestMethod]
        public void TestFacesTwoCubes_FrontBack()
        {
            // Setup: Create a NailsMap
            NailsMap map = new NailsMap();

            // Mark a location
            map.MarkLocation("TestStyle", 0, 0, 0);
            map.MarkLocation("TestStyle", 1, 0, 0);

            // If there is no cube at the location
            NailsCube cube = map.GetCube(0, 0, 0);

            Assert.IsNotNull(cube);
            Assert.AreEqual(cube.StyleName, "TestStyle");

            // Check the second cube
            cube = map.GetCube(1, 0, 0);

            // Look at faces
            var faces = cube.GetFaces();
            Assert.IsTrue(faces.Contains(NailsCubeFace.Floor));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Front));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Left));
            Assert.IsFalse(faces.Contains(NailsCubeFace.Back));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Right));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Ceiling));

            // Check the first cube
            cube = map.GetCube(0, 0, 0);

            // Look at faces
            faces = cube.GetFaces();
            Assert.IsTrue(faces.Contains(NailsCubeFace.Floor));
            Assert.IsFalse(faces.Contains(NailsCubeFace.Front));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Left));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Back));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Right));
            Assert.IsTrue(faces.Contains(NailsCubeFace.Ceiling));

        }


    }
}
