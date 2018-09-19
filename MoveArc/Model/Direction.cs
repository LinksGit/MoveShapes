using System;

namespace MoveArc.Model
{
    [Flags]
    public enum Direction
    {
        Left = 1,
        Right = 2,
        Up = 4,
        Down = 8,
        RightUp = Right | Up,
        RightDown = Right | Down,
        LeftUp = Left | Up,
        LeftDown = Left | Down
    }
}
