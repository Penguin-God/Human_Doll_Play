using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridMoveUseCaseTests
{
    [Test]
    public void ������_�־���_������_��������_��()
    {
        var unit = new GridMover(Vector2.zero);
        var movements = new List<Vector2> { Vector2.right, Vector2.right, Vector2.up };
        var sut = new GridMoveUseCase(unit);

        sut.MoveUnit(movements);

        var expectedPosition = new Vector2(2, 1); // 2ĭ ������, 1ĭ ���� �̵�
        Assert.AreEqual(expectedPosition, unit.Position);
    }
}
