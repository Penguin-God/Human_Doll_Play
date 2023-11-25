using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridMoveUseCaseTests
{
    [Test]
    public void 유닛은_주어진_방향대로_움직여야_함()
    {
        var unit = new GridMover(Vector2.zero);
        var movements = new List<Vector2> { Vector2.right, Vector2.right, Vector2.up };
        var sut = new GridMoveUseCase(unit);

        sut.MoveUnit(movements);

        var expectedPosition = new Vector2(2, 1); // 2칸 오른쪽, 1칸 위로 이동
        Assert.AreEqual(expectedPosition, unit.Position);
    }
}
