using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMoveUseCase
{
    readonly GridMover _gridMover;
    public GridMoveUseCase(GridMover gridMover) => _gridMover = gridMover;

    public void MoveUnit(IEnumerable<Vector2> movements)
    {
        foreach (var movement in movements)
            _gridMover.Move(movement);
    }
}
