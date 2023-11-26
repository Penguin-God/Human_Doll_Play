using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    GridMoveUseCase _gridMoveUseCase;
    float _speed; // �ʴ� �����̴� �Ÿ�

    public void DependencyInject(GridMoveUseCase gridMoveUseCase, float speed)
    {
        _gridMoveUseCase = gridMoveUseCase;
        _speed = speed;
    }

    public void Move(IEnumerable<Direction> directions) => StartCoroutine(MoveCoroutine(directions));

    IEnumerator MoveCoroutine(IEnumerable<Direction> directions)
    {
        foreach (var direction in directions)
        {
            Vector2 destination = _gridMoveUseCase.CalculateDestination(transform.position, direction);

            // ���� ��ġ���� ��ǥ ��ġ���� ������ �ӵ��� �ε巴�� �̵��մϴ�.
            while (Vector2.Distance(transform.position, destination) > Mathf.Epsilon)
            {
                // ���� ����. �Ÿ��� �̵��� ũ�⺸�� ������ ������ ��ȯ.
                transform.position = Vector2.MoveTowards(transform.position, destination, _speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
