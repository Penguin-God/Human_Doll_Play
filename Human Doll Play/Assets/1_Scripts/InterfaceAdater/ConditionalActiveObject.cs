using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalActiveObject : MonoBehaviour
{
    [SerializeField] NudgeParameterController _envirmentController;

    public void SetEn(NudgeParameterController envirmentController) => _envirmentController = envirmentController;

    [SerializeField] string parameterName;

    public void Set()
    {
        print(_envirmentController.GetParameterValue("A"));
        print(_envirmentController.GetParameterValue(parameterName));
        gameObject.SetActive(_envirmentController.GetParameterValue("A") == 1 && _envirmentController.GetParameterValue(parameterName) == 1);
    }
}
