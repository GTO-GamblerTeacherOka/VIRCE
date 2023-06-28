using IO;
using UnityEngine;
using Zenject;
// ReSharper disable Unity.InefficientPropertyAccess

namespace Lobby
{
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

        private void Update()
        {
            var moveDirection = _moveProvider.GetMoveDirection();
            var dPos = Vector3.zero;
            if((moveDirection & IMoveProvider.MoveDirection.Forward)is not 0)
            {
                dPos += transform.forward;
            }
            if((moveDirection & IMoveProvider.MoveDirection.Back)is not 0)
            {
                dPos -= transform.forward;
            }
            if((moveDirection & IMoveProvider.MoveDirection.Left)is not 0)
            {
                dPos -= transform.right;
            }
            if((moveDirection & IMoveProvider.MoveDirection.Right)is not 0)
            {
                dPos += transform.right;
            }
            
            transform.position += dPos.normalized * (moveSpeed * Time.deltaTime);
            
            var horizontalViewPoint = _viewPointProvider.GetHorizontalViewPoint();
            transform.RotateAround(transform.position, Vector3.up, horizontalViewPoint * (viewPointSpeed * Time.deltaTime));
        }
    }
}
