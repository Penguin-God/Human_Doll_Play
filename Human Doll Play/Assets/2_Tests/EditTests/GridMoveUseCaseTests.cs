using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridMoveUseCaseTests
{
    const int tileSize = 16;
    [Test]
    [TestCase(Direction.Up, 0, 16)]
    [TestCase(Direction.Down, 0, -16)]
    [TestCase(Direction.Right, 16, 0)]
    [TestCase(Direction.Left, -16, 0)]
    public void 유닛은_주어진_방향대로_움직여야_함(Direction dir, float x, float y)
    {
        var sut = new GridMoveUseCase(tileSize);

        var result = sut.GridMove(Vector2.zero, dir);

        Assert.AreEqual(new Vector2(x, y), result);
    }
}
