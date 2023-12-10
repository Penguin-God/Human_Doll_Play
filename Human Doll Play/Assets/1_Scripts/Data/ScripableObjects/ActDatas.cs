using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Linq;

public enum ActionEnum
{
    Move,
    Dialogue,
    Rotate,
    Sound,
}

[Serializable]
public class MoveData
{
    [EnumToggleButtons]
    public Direction moveDir;
    public int moveCount = 1;
}

[Serializable]
public class ActData
{
    [SerializeField, EnumToggleButtons]
    public ActionEnum selectedAction;

    [SerializeField, ShowIf("selectedAction", ActionEnum.Move)]
    MoveData[] moveDatas;

    public IEnumerable<MoveEntity> MoveEntities => moveDatas.Select(x => new MoveEntity(x.moveDir, x.moveCount));

    [SerializeField, ShowIf("selectedAction", ActionEnum.Dialogue), TextArea]
    public string[] dialogue;

    [SerializeField, ShowIf("selectedAction", ActionEnum.Rotate), EnumToggleButtons]
    public Direction rotateDir;

    [SerializeField, ShowIf("selectedAction", ActionEnum.Sound)]
    public AudioClip clip;
}

[CreateAssetMenu(fileName = "ActDatas", menuName = "ScripableOject/ActDatas")]
public class ActDatas : SerializedScriptableObject
{
    [SerializeField] public ActData[] actDatas;
}