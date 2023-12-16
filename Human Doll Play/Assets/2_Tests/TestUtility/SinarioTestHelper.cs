using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SinarioTestHelper
{
    public static ParametersCondition CreateEdge(params NudgeParameter[] parameters) => new ParametersCondition(parameters);

    public static IEnumerable<NudgeParameter> CreateParms(int a = -1, int b = -1, int c = -1)
    {
        List<NudgeParameter> result = new ();
        if (a >= 0) result.Add(CreateParameter("A", a));
        if (b >= 0) result.Add(CreateParameter("B", b));
        if (c >= 0) result.Add(CreateParameter("C", c));
        return result;
    }

    static NudgeParameter CreateParameter(string name, int value)
    {
        NudgeParameter result = new(name);
        result.ChangeValue(value);
        return result;
    }

    public static SinarioNode[] CreateSixNodeTree()
    {
        SinarioNode startNode = new();
        SinarioNode sinarioNode2 = new ();
        SinarioNode sinarioNode3 = new ();
        SinarioNode sinarioNode4 = new ();
        SinarioNode sinarioNode5 = new ();
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

    public static SinarioGraph CreateFiveSinarioGraph(params IEnumerable<IAct>[] sinarios)
    {
        if (sinarios.Length != 5)
        {
            Debug.Log($"왜 5개가 아닌 것이지. {sinarios.Length}");
            return null;
        }

        var nodeTree = CreateSixNodeTree();
        var startNode = nodeTree[0];

        var result = new SinarioGraph(startNode);
        for (int i = 0; i < sinarios.Length; i++)
            result.AddSianrio(nodeTree[i + 1], sinarios[i]);
        return result;
    }
}
