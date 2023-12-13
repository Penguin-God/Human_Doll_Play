using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SinarioGraphTests
{
    SinarioEdge CraeteEdge(params NudgeParmeter[] parmeters) => new(parmeters);
    IEnumerable<NudgeParmeter> CraeteParms(int a = -1, int b = -1, int c = -1) => new NudgeParmeter[] { new NudgeParmeter("A", a), new NudgeParmeter("B", b), new NudgeParmeter("C", c) };
    IEnumerable<IAct> CreateSinario(int amount) => new IAct[] { new TestCount() { Count = amount } };

    [Test]
    public void 그래프는_순차적으로_조건에_맞는_노드를_반환해야_함()
    {
        // Arrange
        SinarioNode startNode = new(null);
        SinarioNode sinarioNode2 = new(null);
        SinarioNode sinarioNode3 = new(null);
        SinarioNode sinarioNode4 = new(null);
        SinarioNode sinarioNode5 = new(null);
        SinarioNode sinarioNode6 = new(null);

        var sinarioEdge1 = CraeteEdge(new NudgeParmeter("A", 0));
        var sinarioEdge2 = CraeteEdge(new NudgeParmeter("A", 1), new NudgeParmeter("B", 0));
        var sinarioEdge3 = CraeteEdge(new NudgeParmeter("A", 1), new NudgeParmeter("B", 1));
        var sinarioEdge4 = CraeteEdge(new NudgeParmeter("C", 0));
        var sinarioEdge5 = CraeteEdge(new NudgeParmeter("C", 1));

        startNode.AddTranstion(sinarioEdge1, sinarioNode2);
        startNode.AddTranstion(sinarioEdge2, sinarioNode3);
        startNode.AddTranstion(sinarioEdge3, sinarioNode4);

        sinarioNode4.AddTranstion(sinarioEdge4, sinarioNode5);
        sinarioNode4.AddTranstion(sinarioEdge5, sinarioNode6);

        var sut = new SinarioGraph(startNode);
        var sinario2 = CreateSinario(2);
        var sinario3 = CreateSinario(3);
        var sinario4 = CreateSinario(4);
        var sinario5 = CreateSinario(5);
        var sinario6 = CreateSinario(6);
        sut.AddSianrio(sinarioNode2, sinario2);
        sut.AddSianrio(sinarioNode3, sinario3);
        sut.AddSianrio(sinarioNode4, sinario4);
        sut.AddSianrio(sinarioNode5, sinario5);
        sut.AddSianrio(sinarioNode6, sinario6);

        //Act & Assert
        AssertMoveSinario(sut, true, sinario2, a: 0);
        sut.ResetSianrio();

        AssertMoveSinario(sut, true, sinario3, a: 1, b: 0);
        sut.ResetSianrio();

        AssertMoveSinario(sut, false, sinario4, a: 1, b: 1);
        AssertMoveSinario(sut, true, sinario5, c:0);
        sut.ResetSianrio();

        AssertMoveSinario(sut, false, sinario4, a: 1, b: 1);
        AssertMoveSinario(sut, true, sinario6, c: 1);
    }

    void AssertMoveSinario(SinarioGraph sut, bool expected, IEnumerable<IAct> sinario, int a = -1, int b = -1, int c = -1)
    {
        bool result = sut.MoveNextNode(CraeteParms(a, b, c), out var sinarioResult);
        Assert.AreEqual(expected, result);
        Assert.AreSame(sinario, sinarioResult);
    }

    public class TestCount : IAct
    {
        public int Count;

        public IEnumerator Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
