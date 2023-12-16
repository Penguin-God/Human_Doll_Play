using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoTest : MonoBehaviour
{
    [SerializeField] CharacterMover characterMover;
    
    [SerializeField] ShootingDiractor secnarioDirector;
    [SerializeField] ActDatas[] actDatas;
    NudgeParameterController _envirmentController;

    [SerializeField] SpritePresenter curtain;
    [SerializeField] SpritePresenter curtain2;
    [SerializeField] ActivePersenter _lightToBad;
    [SerializeField] ActivePersenter _lightToMesroom;

    [SerializeField] UI_NudgeController uI_NudgeController;
    [SerializeField] SequentialFocusCamera sequentialFocusCamera;

    public EnvirmentManager _enviremntManager;
    //[SerializeField] ConditionalActiveObject _lightToBad;
    //[SerializeField] ConditionalActiveObject _lightToMesroom;
    [SerializeField] GameObject mushroom;

    IEnumerable<EnvirmentStateEntity> CreateEntitys(int a = -1, int b = -1, int c = -1) => new EnvirmentStateEntity[] { new EnvirmentStateEntity(CreateCondition(a, b, c), 1) };
    void Start()
    {
        characterMover.DependencyInject(new GridMoveCalculator(GameSettings.TileSize));

        IEnumerable<NudgeParameter> nudgeParameters = CreateCondition(0, 0, 0).Conditions;
        var envirment1 = new EnvirmentStateController(CreateEntitys(b:1), curtain);
        var envirment2 = new EnvirmentStateController(CreateEntitys(c:1), curtain2);
        var envirment3 = new EnvirmentStateController(CreateEntitys(a:1, b:1), _lightToBad);
        var envirment4 = new EnvirmentStateController(CreateEntitys(a:1, c:1), _lightToMesroom);
        _enviremntManager = new EnvirmentManager(new EnvirmentStateController[] { envirment1, envirment2, envirment3, envirment4 }, nudgeParameters);
        _envirmentController = new NudgeParameterController(nudgeParameters, _enviremntManager);
        //_lightToBad.SetEn(_envirmentController);
        //_lightToMesroom.SetEn(_envirmentController);
        uI_NudgeController.StartNudgeSetting(_envirmentController);
        secnarioDirector.OnShootingDone += ReSettting;
    }

    void ReSettting(bool isSuccess)
    {
        if (isSuccess) return;

        _envirmentController.ChangeParameter("A", 0);
        _envirmentController.ChangeParameter("B", 0);
        _envirmentController.ChangeParameter("C", 0);
        uI_NudgeController.gameObject.SetActive(true);
        sequentialFocusCamera.MoveToTarget(0);
        mushroom.gameObject.SetActive(true);
        // 캐릭터 위치 복구
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1)) _envirmentController.ChangeEnvirment("A", 0);
        //if (Input.GetKeyDown(KeyCode.Alpha2)) _envirmentController.ChangeEnvirment("A", 1);
        //if (Input.GetKeyDown(KeyCode.Alpha3)) _envirmentController.ChangeEnvirment("B", 0);
        //if (Input.GetKeyDown(KeyCode.Alpha4)) _envirmentController.ChangeEnvirment("B", 1);
        //if (Input.GetKeyDown(KeyCode.Alpha5)) _envirmentController.ChangeEnvirment("C", 0);
        //if (Input.GetKeyDown(KeyCode.Alpha6)) _envirmentController.ChangeEnvirment("C", 1);

        if (Input.GetKeyDown(KeyCode.R))
        {
            sequentialFocusCamera.ResetPosition();
            uI_NudgeController.gameObject.SetActive(false);
            secnarioDirector.SetGrahp(CreateFiveSinarioGraph(CreateSinarioDatas()));
            secnarioDirector.Shooting(_envirmentController.Condition.Conditions);
        }
    }

    IEnumerable<IAct>[] CreateSinarioDatas() => actDatas.Select(x => x.CreateSinarioData(_enviremntManager)).ToArray();

    public static ParametersCondition CreateEdge(params NudgeParameter[] parameters) => new ParametersCondition(parameters);

    public static ParametersCondition CreateCondition(int a = -1, int b = -1, int c = -1) => new ParametersCondition(CreateParms(a, b, c));
    readonly static string[] Names = new string[] { "A", "B", "C" };
    static IEnumerable<NudgeParameter> CreateParms(params int[] values)
    {
        var result = new List<NudgeParameter>();
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] >= 0)
                result.Add(new NudgeParameter(Names[i], values[i]));
        }
        return result;
    }

    NudgeParameter CreateParameter(string name, int value) => new NudgeParameter(name, value);

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
