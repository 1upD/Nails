using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NailsLib.Data
{
    /**
     * <summary>
     * The NailsMap class is a data structure provided to store information about a map. It should be accessible as though it were a three dimensional matrix, although the underlying data structure should be concise yet allow for boundless growth.
     * If the code for Nails ever needs to be recycled for another project, an interface should be supplied so that Alife agents can operate on maps other than Nail maps.
     * <author>1upD</author>
     * </summary>
     */
    public class NailsMap
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /** 
         * <summary>
         * Naive implementation of underlying data structure is simply a list of cube objects.
         * <author>1upD</author>
         * </summary>
         */
        public List<NailsCube> NailsCubes;

        /**
         * <summary>
         * Default constructor.
         * <author>1upD</author>
         * </summary>
         */
        public NailsMap()
        {
            this.NailsCubes = new List<NailsCube>();
            log.Debug("New NailsMap created.");
        }

        /**
         * <summary>
         * Gets the cube at a given location given by an x, y, or z coordinate. Coordinates can be negative.
         * <author>1upD</author>
         * </summary>
         */
        public NailsCube GetCube(int x, int y, int z)
        {
            // Return value
            NailsCube cube = null;
            
            // Use Linq to query the list
            var cubesList = NailsCubes.Where(i => i.X == x && i.Y == y && i.Z == z).ToList();

            // If there is a cube at this location, return it
            if (cubesList != null && cubesList.Count > 0)
            {
                cube = cubesList[0];
            }

            // If more than one cube is found, log an error
            if (cubesList.Count > 1)
            {
                log.Error(string.Format("NailsCube.GetCube(): Multiple cubes found at one location: x {0} y {1} z {2}", x.ToString(), y.ToString(), z.ToString()));
            }

            // If there is no cube at this location, return null
            return cube;
        }

        /**
         * <summary>
         * Applies a style to the location at the given x, y, and z coordinates.
         * <author>1upD</author>
         * </summary>
         */
        public void MarkLocation(string styleName, int x, int y, int z)
        {
            // TODO Does GetCube pass by reference or by value? What should it?
            var cube = GetCube(x, y, z);

            // If there is no cube at this location, create one.
            if(cube == null)
            {
                cube = new NailsCube(x, y, z);
                this.NailsCubes.Add(cube);
                cube.ApplyStyle(styleName);
                this.rebuildAdjacent(x, y, z);

            } else
            {
                // Apply the given style to the cube
                cube.ApplyStyle(styleName);
            }
        }

        /**
         * <summary>
         * Looks at a given cube and the cubes surrounding.
         * Rebuilds the faces for all of them.
         * <author>1upD</author>
         * </summary>
         */
        private void rebuildAdjacent(int x, int y, int z)
        {
            NailsCube cube;

            cube = GetCube(x, y, z);
            if(cube != null) cube.RebuildFaces(this);
            cube = GetCube(x, y, z - 1);
            if (cube != null) cube.RebuildFaces(this);
            cube = GetCube(x + 1, y, z);
            if(cube != null) cube.RebuildFaces(this);
            cube = GetCube(x, y + 1, z);
            if (cube != null) cube.RebuildFaces(this);
            cube = GetCube(x - 1, y, z);
            if (cube != null) cube.RebuildFaces(this);
            cube = GetCube(x, y - 1, z);
            if (cube != null) cube.RebuildFaces(this);
            cube = GetCube(x, y, z + 1);
            if (cube != null) cube.RebuildFaces(this);

        }
    }
}
