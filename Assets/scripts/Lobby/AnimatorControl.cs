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

        private void Update()
        {
            var currentPos = transform.position;
            if (currentPos != _lastPos)
            {
                if (_isAnimating == false)
                {
                    animator.SetFloat(Speed, 1.0f);
                    _isAnimating = true;
                }
            }
            else
            {
                if (_isAnimating)
                {
                    animator.SetFloat(Speed, 0);
                    _isAnimating = false;
                }
            }

            _lastPos = currentPos;
        }
    }
}