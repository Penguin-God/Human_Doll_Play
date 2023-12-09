using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public enum ActionEnum
{
    Move,
    Dialogue,
    Interact,
}

[Serializable]
public class ActData
{
    [SerializeField, EnumToggleButtons]
    public ActionEnum selectedAction;

    [SerializeField, ShowIf("selectedAction", ActionEnum.Move), EnumToggleButtons]
    public Direction[] dirs;

    [SerializeField, ShowIf("selectedAction", ActionEnum.Dialogue), TextArea]
    public string[] dialogue;

    [SerializeField, ShowIf("selectedAction", ActionEnum.Interact)]
    public GameObject interactableObject;
}

[CreateAssetMenu(fileName = "ActDatas", menuName = "ScripableOject/ActDatas")]
public class ActDatas : SerializedScriptableObject
{
    [SerializeField] public ActData[] actDatas;
}