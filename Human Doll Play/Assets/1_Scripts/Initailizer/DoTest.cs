using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoTest : MonoBehaviour
{
    [SerializeField] CharacterMover characterMover;
    [SerializeField] UI_Dialogue dialogue;
    [SerializeField] ShootingDiractor secnarioDirector;
    [SerializeField] ActDatas actDatas1;
    [SerializeField] ActDatas[] actDatas;
    EnvirmentController _envirmentController;

    [SerializeField] SpritePresenter curtain;
    void Start()
    {
        characterMover.DependencyInject(new GridMoveUseCase(GameSettings.TileSize), 5);
        _envirmentController = new EnvirmentController(new ISceneEnvirment[] { curtain });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) _envirmentController.ChangeEnvirment(curtain);

        if (Input.GetKeyDown(KeyCode.R))
        {
            secnarioDirector.SetGrahp(CreateFiveSinarioGraph(CreateActss()));
            // secnarioDirector.Shooting(_envirmentController.NudgeParameters);
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
            default: return null;
        }
    }

    public static SinarioEdge CreateEdge(params NudgeParameter[] parameters) => new SinarioEdge(parameters);

    public static SinarioNode[] CreateSixNodeTree()
    {
        SinarioNode startNode = new();
        SinarioNode sinarioNode2 = new();
        SinarioNode sinarioNode3 = new();
        SinarioNode sinarioNode4 = new();
        SinarioNode sinarioNode5 = new();
        SinarioNode sinarioNode6 = SinarioNode.CreateSuccessNode();

        var sinarioEdge1 = CreateEdge(new NudgeParameter("A", 0));
        var sinarioEdge2 = CreateEdge(new NudgeParameter("A", 1), new NudgeParameter("B", 0));
        var sinarioEdge3 = CreateEdge(new NudgeParameter("A", 1), new NudgeParameter("B", 1));
        var sinarioEdge4 = CreateEdge(new NudgeParameter("C", 0));
        var sinarioEdge5 = CreateEdge(new NudgeParameter("C", 1));

        startNode.AddTranstion(sinarioEdge1, sinarioNode2);
        startNode.AddTranstion(sinarioEdge2, sinarioNode3);
        startNode.AddTranstion(sinarioEdge3, sinarioNode4);

        sinarioNode4.AddTranstion(sinarioEdge4, sinarioNode5);
        sinarioNode4.AddTranstion(sinarioEdge5, sinarioNode6);
        return new SinarioNode[] { startNode, sinarioNode2, sinarioNode3, sinarioNode4, sinarioNode5, sinarioNode6 };
    }

    public static SinarioGraph CreateFiveSinarioGraph(IEnumerable<IAct>[] sinarios)
    {
        var nodeTree = CreateSixNodeTree();
        var startNode = nodeTree[0];

        var result = new SinarioGraph(startNode);
        for (int i = 0; i < sinarios.Length; i++)
            result.AddSianrio(nodeTree[i + 1], sinarios[i]);
        return result;
    }
}
