using System;
using Util;
using IO;
using UnityEngine;
using Zenject;


namespace Lace
{
   public class PlayerControl : MonoBehaviour
   {
       [Inject]
       private IMoveProvider _moveProvider;
       
       [SerializeField]
       private float moveSpeed = 1.0f;

       private bool _isColliding;
       private Vector3 _move;

       private void Update()
       {
           var moveComplex = _moveProvider.GetMove();
           var rotationComplex = new Complex(transform.eulerAngles, true);
           var move = (moveComplex * rotationComplex.Conjugate).Normalize.ToVector3 * Math.Abs(moveSpeed * Time.deltaTime);
       }
   } 
}

