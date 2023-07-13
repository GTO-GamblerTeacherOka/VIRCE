using UnityEngine;
using Util;

namespace IO
{
    /// <summary>
    /// Class for providing move direction from key
    /// </summary>
    public class MoveFromKey : IMoveProvider
    {
        public Complex GetMove()
        {
            var moveComplex = new Complex(0, 0);
            if (Input.GetKey(KeyCode.W))
            {
                moveComplex += new Complex(0, 1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveComplex += new Complex(0, -1);
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveComplex += new Complex(-1, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveComplex += new Complex(1, 0);
            }
            return moveComplex;
        }
    }
}
