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
        sut.transform.position = Vector2.zero;

        // 테스트할 방향 시퀀스
        var directions = new List<Direction> { Direction.Right, Direction.Up, Direction.Up };
        sut.Move(directions);

        // Assert
        Assert.AreEqual(new Vector2(16, 32), sut.transform.position);

        // 테스트가 완료된 후 게임 오브젝트를 정리합니다.
        Object.Destroy(sut.gameObject);
        yield return null;
    }
}
