using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterMoverTests
{
    CharacterMover CreateSut()
    {
        var gameObj = new GameObject();
        var animator = gameObj.AddComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Character/CharacterController");

        var result = gameObj.AddComponent<CharacterMover>();
        result.DependencyInject(new GridMoveCalculator(1));
        return result;
    }

    void Move(CharacterMover mover, IEnumerable<MoveEntity> dirs)
    {
        mover.StartCoroutine(new ObjectMoveActor(mover, dirs).Execute());
    }

    MoveEntity CreateEntity(Direction dir, float speed, int count) => new MoveEntity(dir, speed, count);

    [UnityTest]
    public IEnumerator ��ο�_����_��������_��()
    {
        // Arrange
        var sut = CreateSut();
        sut.transform.position = Vector2.zero;

        // Act
        Move(sut, new MoveEntity[] { CreateEntity(Direction.Right, 50, 1), CreateEntity(Direction.Up, 50, 2) });
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
        var sut = CreateSut();
        var animator = sut.GetComponent<Animator>();


        // Act
        Move(sut, new MoveEntity[] { CreateEntity(direction, 1000, 1) });

        // Assert
        Assert.IsTrue(animator.GetBool("IsWalk"));
        Assert.AreEqual(x, animator.GetFloat("DirX"));
        Assert.AreEqual(y, animator.GetFloat("DirY"));
        yield return null;
        yield return null;
        Assert.IsFalse(animator.GetBool("IsWalk"));

        Object.Destroy(sut.gameObject);
    }

    [UnityTest]
    public IEnumerator ��ο�_����_ȸ����_�ؾ�_��()
    {
        yield return ��ο�_����_ȸ����_�ؾ�_��(Direction.Up, 0, 1);
        yield return ��ο�_����_ȸ����_�ؾ�_��(Direction.Down, 0, -1);
        yield return ��ο�_����_ȸ����_�ؾ�_��(Direction.Left, -1, 0);
        yield return ��ο�_����_ȸ����_�ؾ�_��(Direction.Right, 1, 0);
    }

    public IEnumerator ��ο�_����_ȸ����_�ؾ�_��(Direction direction, float x, float y)
    {
        var sut = CreateSut();
        yield return null;
        yield return null;
        var animator = sut.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Character/CharacterController");

        sut.RotateToDir(direction);
        yield return null;

        Assert.AreEqual(x, animator.GetFloat("DirX"));
        Assert.AreEqual(y, animator.GetFloat("DirY"));
        Assert.IsFalse(animator.GetBool("IsWalk"));
        
        Object.Destroy(sut.gameObject);
    }
}
