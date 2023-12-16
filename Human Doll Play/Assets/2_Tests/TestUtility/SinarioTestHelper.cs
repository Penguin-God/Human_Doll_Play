using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SinarioTestHelper
{
    public static IEnumerable<NudgeParameter> CreateParms(int a = -1, int b = -1, int c = -1) => ParameterCreator.Create3Parms(a, b, c);

    public static SinarioNode[] CreateSixNodeTree()
    {
        SinarioNode startNode = new();
        SinarioNode sinarioNode2 = new ();
        SinarioNode sinarioNode3 = new ();
        SinarioNode sinarioNode4 = new ();
        SinarioNode sinarioNode5 = new ();
        SinarioNode sinarioNode6 = SinarioNode.CreateSuccessNode();

        var condition1 = ParameterCreator.CreateCondition(a: 0);
        var condition2 = ParameterCreator.CreateCondition(a: 1, b:0);
        var condition3 =  ParameterCreator.CreateCondition(a: 1, b: 1);
        var condition4 =  ParameterCreator.CreateCondition(c: 0);
        var condition5 =  ParameterCreator.CreateCondition(c: 1);

        startNode.AddTranstion(condition1, sinarioNode2);
        startNode.AddTranstion(condition2, sinarioNode3);
        startNode.AddTranstion(condition3, sinarioNode4);

        sinarioNode4.AddTranstion(condition4, sinarioNode5);
        sinarioNode4.AddTranstion(condition5, sinarioNode6);
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
