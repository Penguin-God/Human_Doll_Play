using System.Collections;
using System.Collections.Generic;

public readonly struct MoveEntity
{
    public readonly Direction Direction;
    public readonly int MoveCount;

    public MoveEntity(Direction direction, int moveCount)
    {
        Direction = direction;
        MoveCount = moveCount;
    }
}
