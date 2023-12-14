using System.Collections;
using System.Collections.Generic;

public readonly struct MoveEntity
{
    public readonly Direction Direction;
    public readonly float Speed;
    public readonly int MoveCount;

    public MoveEntity(Direction direction, float speed, int moveCount)
    {
        Direction = direction;
        Speed = speed;
        MoveCount = moveCount;
    }
}
