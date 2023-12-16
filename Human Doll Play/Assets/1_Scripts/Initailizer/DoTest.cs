using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoTest : MonoBehaviour
{
    [SerializeField] CharacterMover characterMover;
    
    [SerializeField] ShootingDiractor secnarioDirector;
    [SerializeField] ActDatas[] actDatas;
    NudgeEnvirmentController _envirmentController;

    [SerializeField] SpritePresenter curtain;
    [SerializeField] SpritePresenter curtain2;

    [SerializeField] UI_NudgeController uI_NudgeController;
    [SerializeField] SequentialFocusCamera sequentialFocusCamera;

    [SerializeField] ConditionalActiveObject _lightToBad;
    [SerializeField] ConditionalActiveObject _lightToMesroom;
    [SerializeField] GameObject mushroom;
    void Start()
    {
        characterMover.DependencyInject(new GridMoveCalculator(GameSettings.TileSize));
        var envirment1 = new NudgeEnvierment("A", null);
        var envirment2 = new NudgeEnvierment("B", curtain);
        var envirment3 = new NudgeEnvierment("C", curtain2);
        _envirmentController = new NudgeEnvirmentController(new NudgeEnvierment[] { envirment1, envirment2, envirment3 }, new ConditionalActiveObject[] { _lightToBad, _lightToMesroom });
        _lightToBad.SetEn(_envirmentController);
        _lightToMesroom.SetEn(_envirmentController);
        uI_NudgeController.StartNudgeSetting(_envirmentController);
        secnarioDirector.OnShootingDone += ReSettting;
    }

    void ReSettting(bool isSuccess)
    {
        if (isSuccess) return;

        _envirmentController.ChangeEnvirment("A", 0);
        _envirmentController.ChangeEnvirment("B", 0);
        _envirmentController.ChangeEnvirment("C", 0);
        uI_NudgeController.gameObject.SetActive(true);
        sequentialFocusCamera.MoveToTarget(0);
        mushroom.gameObject.SetActive(true);
        // 캐릭터 위치 복구
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
            sequentialFocusCamera.ResetPosition();
            uI_NudgeController.gameObject.SetActive(false);
            secnarioDirector.SetGrahp(CreateFiveSinarioGraph(CreateSinarioDatas()));
            secnarioDirector.Shooting(_envirmentController.NudgeParameters);
        }
    }

    IEnumerable<IAct>[] CreateSinarioDatas() => actDatas.Select(x => x.CreateSinarioData()).ToArray();

    public static ParameterConditionChecker CreateEdge(params NudgeParameter[] parameters) => new ParameterConditionChecker(parameters);

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
