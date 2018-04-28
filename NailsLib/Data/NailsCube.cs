using System.Collections.Generic;

namespace NailsLib.Data
{
    /*
     * <summary>
     * The NailsCube class models a single data point in a Nails map. It contains information about the style being used in at that space in the map as well as what instances are to be inserted.
     * Walls, ceilings, and floors will be stored separately from other instances so that they can be more quickly rebuilt using convolution.
     * <author>1upD</author>
     * </summary>
     */
    public class NailsCube
    {
        public string StyleName { get { return this._styleName; } set { this._styleName = value; } }
        private string _styleName;
        private int _faces;
        private int _x;
        private int _y;
        private int _z;

        public int X { get { return this._x; } }
        public int Y { get { return this._y; } }
        public int Z { get { return this._z; } }

        /**
         * Constructor takes three arguments for the position.
         * <author>1upD</author>
         */
        public NailsCube(int x, int y, int z)
        {
            this._x = x;
            this._y = y;
            this._z = z;
        }



        /**
        * <summary>
        * Applies a style to this cube.
        * <author>1upD</author>
        * </summary>
        */
        public void ApplyStyle(string styleName)
        {
            this._styleName = styleName;
        }

        /**
         * <summary>
         * RebuildFaces takes a NailsMap and looks at the cubes adjacent to this cube
         * to determine which faces of the cube need to be 'filled in'.
         * <author>1upD</author>
         * </summary>
         */
        public void RebuildFaces(NailsMap nailsMap)
        {
            this._faces = 0;

            if(nailsMap.GetCube(this._x, this._y, this._z - 1) != null)
            {
                this._faces += (int)NailsCubeFace.Floor;   
            }

            if (nailsMap.GetCube(this._x + 1, this._y, this._z) != null)
            {
                this._faces += (int)NailsCubeFace.Front;
            }

            if (nailsMap.GetCube(this._x, this._y + 1, this._z) != null)
            {
                this._faces += (int)NailsCubeFace.Left;
            }

            if (nailsMap.GetCube(this._x - 1, this._y, this._z) != null)
            {
                this._faces += (int)NailsCubeFace.Back;
            }

            if (nailsMap.GetCube(this._x, this._y - 1, this._z) != null)
            {
                this._faces += (int)NailsCubeFace.Right;
            }

            if (nailsMap.GetCube(this._x, this._y, this._z + 1) != null)
            {
                this._faces += (int)NailsCubeFace.Ceiling;
            }

        }

        /**
         * <summary>
         * Return a list of enumerated values representing the faces of the cube that should be 'filled in'.
         * <author>1upD</author>
         * </summary>
         */
        public List<NailsCubeFace> GetFaces()
        {
            List<NailsCubeFace> newList = new List<NailsCubeFace>();
            for(int i = 1; i <= 32; i*=2) // Double from 1 to 32
            {
                if ((this._faces & i) == 0)
                {
                    newList.Add((NailsCubeFace)i);
                }
            }

            return newList;

        }

    }
}