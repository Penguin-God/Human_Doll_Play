using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    GridMoveUseCase _gridMoveUseCase;
    public void DependencyInject(GridMoveUseCase gridMoveUseCase) => _gridMoveUseCase = gridMoveUseCase;

    public IEnumerator Co_Move(Direction direction, float speed) // speed : �ʴ� �����̴� �Ÿ�
    {
        Vector2 destination = _gridMoveUseCase.CalculateDestination(transform.position, direction);
        while (Vector2.Distance(transform.position, destination) > Mathf.Epsilon)
        {
            // ���� ����. �Ÿ��� �̵��� ũ�⺸�� ������ ������ ��ȯ.
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }
}
