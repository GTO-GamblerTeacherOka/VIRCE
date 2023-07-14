using UnityEngine;

namespace IO
{
    /// <summary>
    /// Class for providing view point from mouse
    /// </summary>
    public class ViewPointFromMouse : IViewPointProvider
    {
        public float GetHorizontalViewPoint()
        {
            return Input.GetAxis("Mouse X");
        }

        public float GetVerticalViewPoint()
        {
            return Input.GetAxis("Mouse Y");
        }
    }
}
