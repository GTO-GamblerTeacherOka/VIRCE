using IO;
using UnityEngine;
using Zenject;

namespace Lobby
{
    public class Camera : MonoBehaviour
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
            var moveVector = new Vector3();
            if ((moveDirection & IMoveProvider.MoveDirection.Forward) != 0)
            {
                moveVector += transform.forward;
            }
            if ((moveDirection & IMoveProvider.MoveDirection.Back) != 0)
            {
                moveVector -= transform.forward;
            }
            if ((moveDirection & IMoveProvider.MoveDirection.Left) != 0)
            {
                moveVector -= transform.right;
            }
            if ((moveDirection & IMoveProvider.MoveDirection.Right) != 0)
            {
                moveVector += transform.right;
            }
            moveVector.Normalize();
            transform.position += moveVector * (moveSpeed * Time.deltaTime);
            
            var horizontalViewPoint = _viewPointProvider.GetHorizontalViewPoint();
            var verticalViewPoint = _viewPointProvider.GetVerticalViewPoint();
            transform.Rotate(new Vector3(0.0f, horizontalViewPoint, 0.0f) * (viewPointSpeed * Time.deltaTime));
            transform.Rotate(new Vector3(-verticalViewPoint, 0.0f, 0.0f) * (viewPointSpeed * Time.deltaTime));
        }
    }
}
