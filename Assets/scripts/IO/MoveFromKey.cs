using UnityEngine;

namespace IO
{
    public class MoveFromKey : IMoveProvider
    {
        public IMoveProvider.MoveDirection GetMoveDirection()
        {
            IMoveProvider.MoveDirection direction = 0;
            if (Input.GetKey(KeyCode.W))
            {
                direction |= IMoveProvider.MoveDirection.Forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction |= IMoveProvider.MoveDirection.Back;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction |= IMoveProvider.MoveDirection.Left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction |= IMoveProvider.MoveDirection.Right;
            }
            return direction;
        }
    }
}
