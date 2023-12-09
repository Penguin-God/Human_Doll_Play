using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    GridMoveUseCase _gridMoveUseCase;
    float _speed; // �ʴ� �����̴� �Ÿ�
    Animator _animator;
    public void DependencyInject(GridMoveUseCase gridMoveUseCase, float speed)
    {
        _gridMoveUseCase = gridMoveUseCase;
        _speed = speed;
        _animator = GetComponent<Animator>();
    }

    public IEnumerator Co_Move(Direction direction)
    {
        _animator.SetBool("IsWalk", true);
        Vector2 dir = _gridMoveUseCase.DirToVector(direction);
        _animator.SetFloat("DirX", dir.x);
        _animator.SetFloat("DirY", dir.y);

        Vector2 destination = _gridMoveUseCase.CalculateDestination(transform.position, direction);
        while (Vector2.Distance(transform.position, destination) > Mathf.Epsilon)
        {
            // ���� ����. �Ÿ��� �̵��� ũ�⺸�� ������ ������ ��ȯ.
            transform.position = Vector2.MoveTowards(transform.position, destination, _speed * Time.deltaTime);
            yield return null;
        }
        _animator.SetBool("IsWalk", false);
    }
}
