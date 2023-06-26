using System;

namespace IO
{
    public interface IMoveProvider
    {
        [Flags]
        public enum MoveDirection
        {
            Forward = 1,
            Back = 2,
            Left = 4,
            Right = 8,
        }
        public MoveDirection GetMoveDirection();
    }
}
