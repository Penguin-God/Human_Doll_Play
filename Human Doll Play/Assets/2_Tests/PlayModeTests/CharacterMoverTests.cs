using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterMoverTests
{
    [UnityTest]
    public IEnumerator ��ο�_����_��������_��()
    {
        // Arrange
        var sut = new GameObject().AddComponent<CharacterMover>();
        sut.transform.position = Vector2.zero;

        // �׽�Ʈ�� ���� ������
        var directions = new List<Direction> { Direction.Right, Direction.Up, Direction.Up };
        sut.Move(directions);

        // Assert
        Assert.AreEqual(new Vector2(16, 32), sut.transform.position);

        // �׽�Ʈ�� �Ϸ�� �� ���� ������Ʈ�� �����մϴ�.
        Object.Destroy(sut.gameObject);
        yield return null;
    }
}
