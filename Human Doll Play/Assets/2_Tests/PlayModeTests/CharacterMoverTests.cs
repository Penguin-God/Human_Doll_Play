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
        sut.DependencyInject(new GridMoveUseCase(2), 100);
        sut.transform.position = Vector2.zero;
        var directions = new List<Direction> { Direction.Right, Direction.Up, Direction.Up };

        // Act
        sut.Move(directions);
        yield return new WaitForSeconds(0.1f); // �̵� �ð� ���

        // Assert
        Assert.AreEqual(new Vector3(2, 4, 0), sut.transform.position);

        Object.Destroy(sut.gameObject);
    }
}
