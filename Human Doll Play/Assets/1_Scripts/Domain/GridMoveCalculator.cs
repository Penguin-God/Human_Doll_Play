using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMoveCalculator
{
    readonly int TileSize;
    public GridMoveCalculator(int tileSize) => TileSize = tileSize;
    public Vector2 CalculateDestination(Vector2 currentPos, Direction direction) => currentPos + (DirToVector(direction) * TileSize);
    public Vector2 DirToVector(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up: return Vector2.up;
            case Direction.Down: return Vector2.down;
            case Direction.Left: return Vector2.left;
            case Direction.Right: return Vector2.right;
            default: return Vector2.zero;
        }
    }

    public Vector2 CalculateDestination(MoveEntity moveEntitiy, Vector2 currentPos) => currentPos + (DirToVector(moveEntitiy.Direction) * moveEntitiy.MoveCount * TileSize);
}
