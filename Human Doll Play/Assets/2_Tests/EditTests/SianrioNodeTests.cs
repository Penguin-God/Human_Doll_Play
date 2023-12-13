using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SianrioNodeTests
{
    IEnumerable<NudgeParameter> CraeteParms(int a = -1, int b = -1, int c = -1) => SinarioTestHelper.CreateParms(a, b, c);

    [Test]
    public void ���ǿ�_�����ϴ�_�ó�������_��ȯ�ؾ�_��()
    {
        // Arrange
        var nodeTree = SinarioTestHelper.CreateSixNodeTree();
        var startNode = nodeTree[0];

        //Act & Assert
        Assert.AreSame(nodeTree[1], startNode.GetNextScenario(CraeteParms(a: 0)));
        Assert.AreSame(nodeTree[1], startNode.GetNextScenario(CraeteParms(a: 0, b: 1)));
        Assert.AreSame(nodeTree[2], startNode.GetNextScenario(CraeteParms(a: 1, b: 0)));
        Assert.AreSame(nodeTree[3], startNode.GetNextScenario(CraeteParms(a: 1, b: 1)));

        Assert.AreSame(nodeTree[4], nodeTree[3].GetNextScenario(CraeteParms(c: 0)));
        Assert.AreSame(nodeTree[5], nodeTree[3].GetNextScenario(CraeteParms(c: 1)));
    }
}
