using IO;
using UnityEngine;
using Util;
using Zenject;

namespace Lobby
{
    public class PlayerControl : MonoBehaviour
    {
        [Inject]
        private IMoveProvider _moveProvider;
        [Inject]
        private IViewPointProvider _viewPointProvider;
        
        [SerializeField]
        private float moveSpeed = 20.0f;
        [SerializeField]
        private float viewPointSpeed = 20.0f;

        private void Update()
        {
            var moveDirection = _moveProvider.GetMoveDirection();
            var moveVector = new Complex();
            if ((moveDirection & IMoveProvider.MoveDirection.Forward) != 0)
            {
                moveVector += new Complex(0, 1);
            }
            if ((moveDirection & IMoveProvider.MoveDirection.Back) != 0)
            {
                moveVector += new Complex(0, -1);
            }
            if ((moveDirection & IMoveProvider.MoveDirection.Right) != 0)
            {
                moveVector += new Complex(1, 0);
            }
            if ((moveDirection & IMoveProvider.MoveDirection.Left) != 0)
            {
                moveVector += new Complex(-1, 0);
            }
            transform.position += moveVector.Normalize.ToVector3 * (moveSpeed * Time.deltaTime);
            
            var horizontalViewPoint = _viewPointProvider.GetHorizontalViewPoint();
            var verticalViewPoint = _viewPointProvider.GetVerticalViewPoint();
            transform.RotateAround(transform.position, Vector3.up, horizontalViewPoint * (viewPointSpeed * Time.deltaTime));
            transform.RotateAround(transform.position, transform.right, -verticalViewPoint * (viewPointSpeed * Time.deltaTime));
        }
    }
}
