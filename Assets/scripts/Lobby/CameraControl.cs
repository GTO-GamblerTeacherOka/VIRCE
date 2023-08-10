using System;
using UnityEngine;
using Zenject;
using IO;

namespace Lobby
{
    /// <summary>
    /// Class for controlling camera vertical rotation
    /// </summary>
    public class CameraControl : MonoBehaviour
    {
        [Inject]
        private IViewPointProvider _viewPointProvider;
        
        private void Update()
        {
            var verticalViewPoint = _viewPointProvider.GetVerticalViewPoint();
            var rot = transform.localEulerAngles;
            rot.x -= verticalViewPoint;
            if(rot.x > 180)
            {
                rot.x -= 360;
            }
            rot.x = Math.Clamp(rot.x, -60, 60);
            if (rot.x < 0)
            {
                rot.x += 360;
            }
            transform.localEulerAngles = new Vector3(rot.x, 0, 0);
        }
    }
}
