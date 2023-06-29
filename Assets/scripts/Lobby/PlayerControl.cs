using System;
using Util;
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
        private float viewPointSpeed = 10.0f;

        public Animator animator;

        private void Update()
        {
            var moveComplex = _moveProvider.GetMove();
            var rot = transform.eulerAngles;
            Debug.Log(rot.ToString());
            var rotationComplex = new Complex(transform.eulerAngles, true);
            var move = (moveComplex * rotationComplex.Conjugate).Normalize.ToVector3;

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
