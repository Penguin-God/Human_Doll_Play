using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridMoveUseCaseTests
{
    const int TileSize = 10;
    readonly Vector2 StartPos = Vector2.zero;
    [Test]
    [TestCase(Direction.Up, 0, 10)]
    [TestCase(Direction.Down, 0, -10)]
    [TestCase(Direction.Right, 10, 0)]
    [TestCase(Direction.Left, -10, 0)]
    public void 유닛은_주어진_방향대로_움직여야_함(Direction dir, float x, float y)
    {
        var sut = new GridMoveUseCase(TileSize);

        var result = sut.CalculateDestination(StartPos, dir);

        Assert.AreEqual(new Vector2(x, y), result);
    }

    [Test]
    [TestCase(Direction.Up, 3, 0, 30)]
    [TestCase(Direction.Down, 1, 0, -10)]
    [TestCase(Direction.Right, 2, 20, 0)]
    [TestCase(Direction.Left, 7, -70, 0)]
    public void 유닛은_주어진_방향대로_카운트만큼_움직여야_함(Direction dir, int count, float x, float y)
    {
        var sut = new GridMoveUseCase(TileSize);

        var result = sut.CalculateDestination(new MoveEntity(dir, 0, count), StartPos);

        Assert.AreEqual(new Vector2(x, y), result);
    }
}
