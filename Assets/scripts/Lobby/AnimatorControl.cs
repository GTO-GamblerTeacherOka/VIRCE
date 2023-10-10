using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

namespace Lobby
{
/// <summary>
/// Class for controlling other player's Animator
/// </summary>
    public class AnimatorControl : MonoBehaviour
    {
        private static Dictionary<string, GameObject> _userObjects;
        private static Vector3 _lastPos = new();
        public Animator animator;
        private static readonly int Speed = Animator.StringToHash("speed");
        private bool _isAnimating;

        private void Update()
        {
            foreach (var keyValuePair in _userObjects)
            {
                var key = keyValuePair.Key;
                var obj = keyValuePair.Value;
                
                var currentPos = obj.transform.position;
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
                    if (_isAnimating == true)
                    {
                        animator.SetFloat(Speed, 0);
                        _isAnimating = false;
                    }
                }
                _lastPos = currentPos;
            }
        }
    }
}
