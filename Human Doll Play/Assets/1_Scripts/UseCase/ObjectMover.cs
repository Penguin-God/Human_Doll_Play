using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    GridMoveUseCase _gridMoveUseCase;
    public void DependencyInject(GridMoveUseCase gridMoveUseCase) => _gridMoveUseCase = gridMoveUseCase;

    public IEnumerator Co_Move(Direction direction, float speed) // speed : 초당 움직이는 거리
    {
        Vector2 destination = _gridMoveUseCase.CalculateDestination(transform.position, direction);
        while (Vector2.Distance(transform.position, destination) > Mathf.Epsilon)
        {
            // 선형 보간. 거리가 이동할 크기보다 작으면 목적지 반환.
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }
}
