using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    GridMoveUseCase _gridMoveUseCase;
    float _speed; // 초당 움직이는 거리

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

            // 현재 위치에서 목표 위치까지 지정된 속도로 부드럽게 이동합니다.
            while (Vector2.Distance(transform.position, destination) > Mathf.Epsilon)
            {
                transform.position = Vector2.MoveTowards(transform.position, destination, _speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
