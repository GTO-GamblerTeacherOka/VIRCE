using UnityEngine;

namespace IO
{
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
