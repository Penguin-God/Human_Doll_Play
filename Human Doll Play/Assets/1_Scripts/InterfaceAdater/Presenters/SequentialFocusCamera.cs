using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialFocusCamera : MonoBehaviour
{
    [SerializeField] Transform[] _focusTargets;
    public float transitionSpeed = 5f;

    void Start()
    {
        MoveToTarget(0); // 시작 시 첫 번째 대상으로 카메라 이동
    }

    public void MoveToTarget(int index)
    {
        if (index < 0 || index >= _focusTargets.Length) return;

        StopAllCoroutines();
        StartCoroutine(TransitionCamera(_focusTargets[index].position));
    }

    IEnumerator TransitionCamera(Vector2 destination)
    {
        while (Vector2.Distance(Camera.main.transform.position, destination) > 0.02f)
        {
            ChangePositoin(Vector2.Lerp(Camera.main.transform.position, destination, transitionSpeed * Time.deltaTime));
            yield return null;
        }
        ChangePositoin(destination);
    }

    void ChangePositoin(Vector2 destination) => transform.position = new Vector3(destination.x, destination.y, transform.position.z);
}
