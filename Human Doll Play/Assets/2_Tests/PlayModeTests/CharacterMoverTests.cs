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
    public IEnumerator 경로에_따라_움직여야_함()
    {
        // Arrange
        var sut = CreateSut();
        sut.transform.position = Vector2.zero;

        // Act
        Move(sut, new MoveEntity[] { CreateEntity(Direction.Right, 50, 1), CreateEntity(Direction.Up, 50, 2) });
        yield return new WaitForSeconds(0.1f); // 이동 시간 대기

        // Assert
        Assert.AreEqual(new Vector3(1, 2, 0), sut.transform.position);

        Object.Destroy(sut.gameObject);
    }

    [UnityTest]
    public IEnumerator 경로에_따라_애니메이션을_주어야_함()
    {
        yield return 경로에_따라_애니메이션을_주어야_함(Direction.Up, 0, 1);
        yield return 경로에_따라_애니메이션을_주어야_함(Direction.Down, 0, -1);
        yield return 경로에_따라_애니메이션을_주어야_함(Direction.Left, -1, 0);
        yield return 경로에_따라_애니메이션을_주어야_함(Direction.Right, 1, 0);
    }

    IEnumerator 경로에_따라_애니메이션을_주어야_함(Direction direction, float x, float y)
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
    public IEnumerator 경로에_따라_회전을_해야_함()
    {
        yield return 경로에_따라_회전을_해야_함(Direction.Up, 0, 1);
        yield return 경로에_따라_회전을_해야_함(Direction.Down, 0, -1);
        yield return 경로에_따라_회전을_해야_함(Direction.Left, -1, 0);
        yield return 경로에_따라_회전을_해야_함(Direction.Right, 1, 0);
    }

    public IEnumerator 경로에_따라_회전을_해야_함(Direction direction, float x, float y)
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
