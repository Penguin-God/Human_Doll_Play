using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoTest : MonoBehaviour
{
    [SerializeField] CharacterMover characterMover;
    [SerializeField] UI_Dialogue dialogue;
    [SerializeField] ShootingDiractor secnarioDirector;
    [SerializeField] ActDatas[] actDatas;
    EnvirmentController _envirmentController;

    [SerializeField] SpritePresenter curtain;
    [SerializeField] ActivePersenter activePersenter1;
    [SerializeField] ActivePersenter activePersenter2;
    void Start()
    {
        characterMover.DependencyInject(new GridMoveUseCase(GameSettings.TileSize), 5);
        var envirment1 = new NudgeEnvierment("A", curtain);
        var envirment2 = new NudgeEnvierment("B", activePersenter1);
        var envirment3 = new NudgeEnvierment("C", activePersenter2);
        _envirmentController = new EnvirmentController(new NudgeEnvierment[] { envirment1, envirment2, envirment3 });

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) _envirmentController.ChangeEnvirment("A", 0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) _envirmentController.ChangeEnvirment("A", 1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) _envirmentController.ChangeEnvirment("B", 0);
        if (Input.GetKeyDown(KeyCode.Alpha4)) _envirmentController.ChangeEnvirment("B", 1);
        if (Input.GetKeyDown(KeyCode.Alpha5)) _envirmentController.ChangeEnvirment("C", 0);
        if (Input.GetKeyDown(KeyCode.Alpha6)) _envirmentController.ChangeEnvirment("C", 1);

        if (Input.GetKeyDown(KeyCode.R))
        {
            secnarioDirector.SetGrahp(CreateFiveSinarioGraph(CreateActss()));
            secnarioDirector.Shooting(_envirmentController.NudgeParameters);
        }
    }

    IEnumerable<IAct>[] CreateActss()
    {
        List<IEnumerable<IAct>> result = new();
        foreach (ActDatas data in actDatas)
            result.Add(CreateActs(data));
        return result.ToArray();
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
            case ActionEnum.Envirment: return actData._envirmentInteractionData.CreateInteractionActor();
            default: return null;
        }
    }

    public static SinarioEdge CreateEdge(params NudgeParameter[] parameters) => new SinarioEdge(parameters);

    NudgeParameter CreateParameter(string name, int value)
    {
        NudgeParameter result = new(name);
        result.ChangeValue(value);
        return result;
    }

    public SinarioNode[] CreateSixNodeTree()
    {
        SinarioNode startNode = new();
        SinarioNode sinarioNode2 = new();
        SinarioNode sinarioNode3 = new();
        SinarioNode sinarioNode4 = new();
        SinarioNode sinarioNode5 = new();
        SinarioNode sinarioNode6 = SinarioNode.CreateSuccessNode();

        var sinarioEdge1 = CreateEdge(CreateParameter("A", 0));
        var sinarioEdge2 = CreateEdge(CreateParameter("A", 1), CreateParameter("B", 0));
        var sinarioEdge3 = CreateEdge(CreateParameter("A", 1), CreateParameter("B", 1));
        var sinarioEdge4 = CreateEdge(CreateParameter("C", 0));
        var sinarioEdge5 = CreateEdge(CreateParameter("C", 1));

        startNode.AddTranstion(sinarioEdge1, sinarioNode2);
        startNode.AddTranstion(sinarioEdge2, sinarioNode3);
        startNode.AddTranstion(sinarioEdge3, sinarioNode4);

        sinarioNode4.AddTranstion(sinarioEdge4, sinarioNode5);
        sinarioNode4.AddTranstion(sinarioEdge5, sinarioNode6);
        return new SinarioNode[] { startNode, sinarioNode2, sinarioNode3, sinarioNode4, sinarioNode5, sinarioNode6 };
    }

    public SinarioGraph CreateFiveSinarioGraph(IEnumerable<IAct>[] sinarios)
    {
        var nodeTree = CreateSixNodeTree();
        var startNode = nodeTree[0];

        var result = new SinarioGraph(startNode);
        for (int i = 0; i < sinarios.Length; i++)
            result.AddSianrio(nodeTree[i + 1], sinarios[i]);
        return result;
    }
}
