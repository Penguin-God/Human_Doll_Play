using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    GridMoveUseCase _gridMoveUseCase;
    float _speed; // 초당 움직이는 거리
    Animator _animator;
    public void DependencyInject(GridMoveUseCase gridMoveUseCase, float speed)
    {
        _gridMoveUseCase = gridMoveUseCase;
        _speed = speed;
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

    public IEnumerator Co_Move(Direction direction)
    {
        PlayWalkAnima(direction);

        Vector2 destination = _gridMoveUseCase.CalculateDestination(transform.position, direction);
        while (Vector2.Distance(transform.position, destination) > Mathf.Epsilon)
        {
            // 선형 보간. 거리가 이동할 크기보다 작으면 목적지 반환.
            transform.position = Vector2.MoveTowards(transform.position, destination, _speed * Time.deltaTime);
            yield return null;
        }

        PlayIdleAnima(direction);
    }

    public void RotateToDir(Direction direction) => PlayIdleAnima(direction);
}
