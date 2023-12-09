using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class GridMoveUseCase
{
    readonly int TileSize;
    public GridMoveUseCase(int tileSize) => TileSize = tileSize;
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
}
