using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMoveUseCase
{
    public void MoveUnit(GridMover unit, IEnumerable<Vector2> movements)
    {
        foreach (var movement in movements)
            unit.Move(movement);
    }
}
