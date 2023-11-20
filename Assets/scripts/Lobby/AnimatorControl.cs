using System.Collections.Generic;
using UnityEngine;

namespace Lobby
{
    /// <summary>
    ///     Class for controlling other player's Animator
    /// </summary>
    public class AnimatorControl : MonoBehaviour
    {
        private static Vector3 _lastPos;
        private static readonly int Speed = Animator.StringToHash("speed");
        public Animator animator;
        private bool _isAnimating;
        private static int _watchPosition = 0;

        private void Update()
        {
            var currentPos = transform.position;
            
            float currentX = currentPos.x*100;
            float currentZ = currentPos.z*100;
            float lastX = _lastPos.x*100;
            float lastZ = _lastPos.z*100;

            if ((int)currentX != (int)lastX || (int)currentZ != (int)lastZ)
            {
                _watchPosition = 0;
                if (_isAnimating == false)
                {
                    animator.SetFloat(Speed, 1.0f);
                    _isAnimating = true;
                }
            }
            else
            {
                _watchPosition++;
                if (_isAnimating && _watchPosition > 5)
                {
                    animator.SetFloat(Speed, 0);
                    _isAnimating = false;
                }
            }
            _lastPos = currentPos;
        }
    }
}