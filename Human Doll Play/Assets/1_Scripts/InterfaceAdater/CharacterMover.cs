using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : ObjectMover
{
    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void PlayWalkAnima(Direction direction)
    {
        PlayDirAnima(direction);
        _animator.SetBool("IsWalk", true);
    }

    void PlayIdleAnima(Direction direction)
    {
        PlayDirAnima(direction);
        _animator.SetBool("IsWalk", false);
    }

    void PlayDirAnima(Direction direction)
    {
        Vector2 dir = _gridMoveUseCase.DirToVector(direction);
        _animator.SetFloat("DirX", dir.x);
        _animator.SetFloat("DirY", dir.y);
    }

    public override IEnumerator Co_Move(MoveEntity moveEntity)
    {
        PlayWalkAnima(moveEntity.Direction);
        yield return base.Co_Move(moveEntity);
        PlayIdleAnima(moveEntity.Direction);
    }

    public void RotateToDir(Direction direction) => PlayIdleAnima(direction);

    void Update()
    {
        print(_gridMoveUseCase);
    }
}
