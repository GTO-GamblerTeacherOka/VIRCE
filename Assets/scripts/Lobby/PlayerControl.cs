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
        private bool _isColliding;
        
        private Vector3 _move;
        private static readonly int Speed = Animator.StringToHash("speed");

        private void Update()
        {
            var moveComplex = _moveProvider.GetMove();
            var rotationComplex = new Complex(transform.eulerAngles, true);
            var move = (moveComplex * rotationComplex.Conjugate).Normalize.ToVector3 * Math.Abs(moveSpeed * Time.deltaTime);
            
            if (move.sqrMagnitude is not 0)
            {
                animator.SetFloat(Speed, 1.0f);
            }
            else
            {
                animator.SetFloat(Speed, 0);
            }

            if (_isColliding)
            {
                transform.position -= _move;
            }
            else
            {
                transform.position += _move = move;
            }

            var horizontalViewPoint = _viewPointProvider.GetHorizontalViewPoint();
            transform.RotateAround(transform.position, Vector3.up, horizontalViewPoint * (viewPointSpeed * Time.deltaTime));
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if(other.transform.gameObject.layer != 6)
                _isColliding = true;
        }
        
        private void OnCollisionExit(Collision other) 
        {
            if(other.transform.gameObject.layer != 6)
                _isColliding = false;
        }
    }
}
