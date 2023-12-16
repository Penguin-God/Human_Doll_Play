using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    protected GridMoveCalculator _gridMoveUseCase;
    public void DependencyInject(GridMoveCalculator gridMoveUseCase)
    {
        _gridMoveUseCase = gridMoveUseCase;
    }

    public virtual IEnumerator Co_Move(MoveEntity moveEntity)
    {
        Vector2 destination = _gridMoveUseCase.CalculateDestination(moveEntity, transform.position);
        while (Vector2.Distance(transform.position, destination) > Mathf.Epsilon)
        {
            // 선형 보간. 거리가 이동할 크기보다 작으면 목적지 반환.
            transform.position = Vector2.MoveTowards(transform.position, destination, moveEntity.Speed * Time.deltaTime);
            yield return null;
        }
    }
}
