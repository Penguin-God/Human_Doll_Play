using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterMoverTests
{
    [UnityTest]
    public IEnumerator 경로에_따라_움직여야_함()
    {
        // Arrange
        var sut = new GameObject().AddComponent<CharacterMover>();
        sut.DependencyInject(new GridMoveUseCase(2), 100);
        sut.transform.position = Vector2.zero;
        var directions = new List<Direction> { Direction.Right, Direction.Up, Direction.Up };

        // Act
        sut.Move(directions);
        yield return new WaitForSeconds(0.1f); // 이동 시간 대기

        // Assert
        Assert.AreEqual(new Vector3(2, 4, 0), sut.transform.position);

        Object.Destroy(sut.gameObject);
    }
}
