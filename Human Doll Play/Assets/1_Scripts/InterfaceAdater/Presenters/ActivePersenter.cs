using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePersenter : MonoBehaviour, ISceneEnvirment
{
    public void ChangeEnvierment(int value)
    {
        if (value == 0) gameObject.SetActive(false);
        else if(value == 1 ) gameObject.SetActive(true);
    }
}
