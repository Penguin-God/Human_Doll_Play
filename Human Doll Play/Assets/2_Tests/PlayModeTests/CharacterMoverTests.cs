using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterMoverTests
{
    CharacterMover CreateSut(float speed)
    {
        var result = new GameObject().AddComponent<CharacterMover>();
        result.gameObject.AddComponent<Animator>();
        result.DependencyInject(new GridMoveUseCase(1), speed);
        return result;
    }

    [UnityTest]
    public IEnumerator ��ο�_����_��������_��()
    {
        // Arrange
        var sut = CreateSut(50);
        sut.transform.position = Vector2.zero;
        var directions = new List<Direction> { Direction.Right, Direction.Up, Direction.Up };

        // Act
        sut.Move(directions);
        yield return new WaitForSeconds(0.1f); // �̵� �ð� ���

        // Assert
        Assert.AreEqual(new Vector3(1, 2, 0), sut.transform.position);

        Object.Destroy(sut.gameObject);
    }

    [UnityTest]
    public IEnumerator ��ο�_����_�ִϸ��̼���_�־��_��()
    {
        yield return ��ο�_����_�ִϸ��̼���_�־��_��(Direction.Up, 0, 1);
        yield return ��ο�_����_�ִϸ��̼���_�־��_��(Direction.Down, 0, -1);
        yield return ��ο�_����_�ִϸ��̼���_�־��_��(Direction.Left, -1, 0);
        yield return ��ο�_����_�ִϸ��̼���_�־��_��(Direction.Right, 1, 0);
    }

    IEnumerator ��ο�_����_�ִϸ��̼���_�־��_��(Direction direction, float x, float y)
    {
        // Arrange
        var sut = CreateSut(1000);
        var animator = sut.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Character/CharacterController");

        // Act
        sut.Move(new Direction[] { direction });

        // Assert
        Assert.IsTrue(animator.GetBool("IsWalk"));
        Assert.AreEqual(x, animator.GetFloat("DirX"));
        Assert.AreEqual(y, animator.GetFloat("DirY"));
        yield return null;
        yield return null;
        Assert.IsFalse(animator.GetBool("IsWalk"));

        Object.Destroy(sut.gameObject);
    }
}
