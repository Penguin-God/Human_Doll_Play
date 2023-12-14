using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePersenter : MonoBehaviour, ISceneEnvirment
{
    public void ChangeEnvierment()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
