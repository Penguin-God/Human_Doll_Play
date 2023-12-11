using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoTest : MonoBehaviour
{
    [SerializeField] CharacterMover characterMover;
    [SerializeField] UI_Dialogue dialogue;
    [SerializeField] ScriptPhotographer secnarioDirector;
    [SerializeField] ActDatas actDatas1;

    void Start()
    {
        characterMover.DependencyInject(new GridMoveUseCase(GameSettings.TileSize), 5);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            secnarioDirector.Shooting(CreateActs(actDatas1));
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            secnarioDirector.Shooting(CreateActs(actDatas1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            secnarioDirector.Shooting(CreateActs(actDatas1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            secnarioDirector.Shooting(CreateActs(actDatas1));
        }
    }

    IEnumerable<IAct> CreateActs(ActDatas actDatas) => actDatas.actDatas.Select(x => CreateAct(x));

    IAct CreateAct(ActData actData)
    {
        switch (actData.selectedAction)
        {
            case ActionEnum.Move: return new CharacterMoveActor(characterMover, actData.MoveEntities);
            case ActionEnum.Rotate: return new CharacterRotator(characterMover, actData.rotateDir);
            case ActionEnum.Dialogue: return new Dialoguer(actData.dialogue, dialogue, dialogue);
            case ActionEnum.Sound: return new SoundActor(actData.clip);
            default: return null;
        }
    }
}
