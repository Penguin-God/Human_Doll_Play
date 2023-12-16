using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalActiveObject : MonoBehaviour
{
    [SerializeField] NudgeEnvirmentController _envirmentController;

    public void SetEn(NudgeEnvirmentController envirmentController) => _envirmentController = envirmentController;

    [SerializeField] string parameterName;

    public void Set()
    {
        print(_envirmentController.GetEnvirmentValue("A"));
        print(_envirmentController.GetEnvirmentValue(parameterName));
        gameObject.SetActive(_envirmentController.GetEnvirmentValue("A") == 1 && _envirmentController.GetEnvirmentValue(parameterName) == 1);
    }
}
