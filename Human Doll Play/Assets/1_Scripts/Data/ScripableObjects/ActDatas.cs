using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Linq;

public enum ActionEnum
{
    Move,
    Rotate,
    Dialogue,
    Sound,
    Envirment,
    Delay,
}

[Serializable]
public class MoveData
{
    // [SerializeField] GameObject _mover;
    [SerializeField] float _speed = 5;
    [SerializeField] MoveEntityData[] _moveDatas;

    public IAct CreateMoveActor() => new ObjectMoveActor(GameObject.FindObjectOfType<ObjectMover>(), _moveDatas.Select(x => new MoveEntity(x.moveDir, _speed, x.moveCount)));
}

[Serializable]
public class MoveEntityData
{
    [EnumToggleButtons, SerializeField]
    public Direction moveDir;
    [SerializeField] public int moveCount = 1;
}

[Serializable]
public class CharacterRotateData
{
    // [SerializeField] GameObject _character;
    [SerializeField, EnumToggleButtons] Direction _direction;

    public IAct CreateRotateActor() => new CharacterRotator(GameObject.FindObjectOfType<CharacterMover>(), _direction);
}

[Serializable]
public class DialogeData
{
    [SerializeField, TextArea] string[] dialogue;
    //[SerializeField] GameObject _dialoger;
    //[SerializeField] GameObject _waiter; 

    public IAct CreateDialoger() => new Dialoguer(dialogue, GameObject.Find("Canvas").GetComponent<IDialoguer>(), GameObject.Find("Canvas").GetComponent<IYieldNextLine>());
}

[Serializable]
public class EnvirmentInteractionData
{
    [SerializeField] string _objectName;
    [SerializeField] int value;

    public IAct CreateInteractionActor() => new EnvirmentChangeActor(GameObject.Find(_objectName).GetComponent<ISceneEnvirment>(), value);
}

[Serializable]
public class ActData
{
    [SerializeField, EnumToggleButtons]
    ActionEnum _selectedAction;

    [SerializeField, ShowIf(nameof(_selectedAction), ActionEnum.Move)]
    MoveData _moveData;

    [SerializeField, ShowIf(nameof(_selectedAction), ActionEnum.Rotate), EnumToggleButtons]
    CharacterRotateData _characterRotateData;

    [SerializeField, ShowIf(nameof(_selectedAction), ActionEnum.Dialogue)]
    DialogeData _dialogueData;

    [SerializeField, ShowIf(nameof(_selectedAction), ActionEnum.Sound)]
    AudioClip _clip;

    [SerializeField, ShowIf(nameof(_selectedAction), ActionEnum.Envirment)]
    EnvirmentInteractionData _envirmentInteractionData;

    [SerializeField, ShowIf(nameof(_selectedAction), ActionEnum.Delay)]
    float _delay;

    public IAct CreateAct()
    {
        switch (_selectedAction)
        {
            case ActionEnum.Move: return _moveData.CreateMoveActor();
            case ActionEnum.Rotate: return _characterRotateData.CreateRotateActor();
            case ActionEnum.Dialogue: return _dialogueData.CreateDialoger();
            case ActionEnum.Sound: return new SoundActor(_clip);
            case ActionEnum.Envirment: return _envirmentInteractionData.CreateInteractionActor();
            case ActionEnum.Delay: return new DelayActor(_delay);
            default: return null;
        }
    }
}

[CreateAssetMenu(fileName = "ActDatas", menuName = "ScripableOject/ActDatas")]
public class ActDatas : SerializedScriptableObject
{
    [SerializeField] ActData[] _actDatas;

    public IEnumerable<IAct> CreateSinarioData() => _actDatas.Select(x => x.CreateAct());
}