using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMover
{
    public Vector2 Position { get; private set; } = Vector2.zero;
    public GridMover(Vector2 position) => Position = position;
    public void Move(Vector2 direction) => Position += direction;
}
