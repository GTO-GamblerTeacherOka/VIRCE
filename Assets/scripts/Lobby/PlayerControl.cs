using System;
using IO;
using UnityEngine;
using Zenject;
// ReSharper disable Unity.InefficientPropertyAccess

namespace Lobby
{
    /// <summary>
    /// Class for controlling player
    /// </summary>
    public class PlayerControl : MonoBehaviour
    {
        [Inject]
        private IMoveProvider _moveProvider;
        [Inject]
        private IViewPointProvider _viewPointProvider;
        
        [SerializeField]
        private float moveSpeed = 1.0f;
        [SerializeField]
        private float viewPointSpeed = 1.0f;

        public Animator animator;

        private void Update()
        {
            var move = _moveProvider.GetMove().Normalize.ToVector3;
            #if UNITY_EDITOR
            Debug.Log(move.ToString());
            #endif

            transform.position += move * Math.Abs(moveSpeed * Time.deltaTime);
            
            if (move != Vector3.zero)
            {
                animator.SetFloat("speed", 1.0f);
            }
            else
            {
                animator.SetFloat("speed", 0);
            }
            
            var horizontalViewPoint = _viewPointProvider.GetHorizontalViewPoint();
            transform.RotateAround(transform.position, Vector3.up, horizontalViewPoint * (viewPointSpeed * Time.deltaTime));
        }
    }
}
