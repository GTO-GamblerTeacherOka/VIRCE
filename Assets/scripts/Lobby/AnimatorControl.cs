using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Lobby
{
/// <summary>
/// Class for controlling other player's Animator
/// </summary>
    public class AnimatorControl : MonoBehaviour
    {
        private static Dictionary<string, GameObject> _userObjects;
        private Vector3 _move;
        public Animator animator;
        private static readonly int Speed = Animator.StringToHash("speed");

        private void Update()
        {
            foreach (var keyValuePair in _userObjects)
            {
                var key = keyValuePair.Key;
                var obj = keyValuePair.Value;
                
                var currentPos = obj.transform.position;

            }
        }
    }
}
