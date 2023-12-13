using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SceneDiractorTests
{
    SinarioNode CreateNode(TestCount testCount)
    {
        SinarioNode sinarioNode1 = new SinarioNode(CreateAct(testCount, 1));
        SinarioNode sinarioNode2 = new SinarioNode(CreateAct(testCount, 2));
        SinarioNode sinarioNode3 = new SinarioNode(CreateAct(testCount, 3));
        SinarioNode sinarioNode4 = new SinarioNode(CreateAct(testCount, 4));
        SinarioNode sinarioNode5 = new SinarioNode(CreateAct(testCount, 5));
        SinarioNode sinarioNode6 = new SinarioNode(CreateAct(testCount, 6));
        SinarioNode sinarioNode7 = new SinarioNode(CreateAct(testCount, 7));

        //sinarioNode1.AddNode(sinarioNode2);
        //sinarioNode1.AddNode(sinarioNode3);

        //sinarioNode3.AddNode(sinarioNode4);
        //sinarioNode3.AddNode(sinarioNode5);

        //sinarioNode5.AddNode(sinarioNode6);
        //sinarioNode5.AddNode(sinarioNode7);

        return sinarioNode1;
    }

    SinarioEdge CraeteEdge(params NudgeParmeter[] parmeters) => new(parmeters);
    IEnumerable<NudgeParmeter> CraeteParms(int a = -1, int b = -1, int c = -1) => new NudgeParmeter[] { new NudgeParmeter("A", a), new NudgeParmeter("B", b), new NudgeParmeter("C", c) };
    IEnumerable<IAct> CreateSinario(int amount, TestCount count) => new IAct[] { new TestAct(count, amount) };
    SceneDiractor CreateSut(TestCount count)
    {
        var result = new GameObject().AddComponent<SceneDiractor>();

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

        var grahp = new SinarioGraph(startNode);
        var sinario2 = CreateSinario(2, count);
        var sinario3 = CreateSinario(3, count);
        var sinario4 = CreateSinario(4, count);
        var sinario5 = CreateSinario(5, count);
        var sinario6 = CreateSinario(6, count);
        grahp.AddSianrio(sinarioNode2, sinario2);
        grahp.AddSianrio(sinarioNode3, sinario3);
        grahp.AddSianrio(sinarioNode4, sinario4);
        grahp.AddSianrio(sinarioNode5, sinario5);
        grahp.AddSianrio(sinarioNode6, sinario6);
        result.SetGrahp(grahp);
        return result;
    }

    IEnumerable<IAct> CreateAct(TestCount count, int amount) => new IAct[] { new TestAct(count, amount) };

    [UnityTest]
    public IEnumerator 조건에_맞는_시나리오를_실행시켜야_함()
    {
        //조건에_맞는_시나리오를_실행시켜야_함(new int[] { 0 }, 1);
        //조건에_맞는_시나리오를_실행시켜야_함(new int[] { 0, 1, 1 }, 1);

        //조건에_맞는_시나리오를_실행시켜야_함(new int[] { 1, 0 }, 3);
        //조건에_맞는_시나리오를_실행시켜야_함(new int[] { 1, 0, 1 }, 3);

        //조건에_맞는_시나리오를_실행시켜야_함(new int[] { 1, 1, 0 }, 8);
        //조건에_맞는_시나리오를_실행시켜야_함(new int[] { 1, 1, 1 }, 9);
        //조건에_맞는_시나리오를_실행시켜야_함(new int[] { 1, 1, 1, 1 }, 9);

        조건에_맞는_시나리오를_실행시켜야_함(2, a:0);
        조건에_맞는_시나리오를_실행시켜야_함(3, a:1, b:0);

        조건에_맞는_시나리오를_실행시켜야_함(9, a: 1, b: 1, c: 0);
        조건에_맞는_시나리오를_실행시켜야_함(10, a: 1, b: 1, c: 1);

        yield return null;
    }

    public IEnumerator 조건에_맞는_시나리오를_실행시켜야_함(int[] conditions, int expected)
    {
        TestCount testCount = new TestCount();
        var sut = CreateSut(testCount);

        sut.Shooting(conditions);
        yield return null;
        Assert.AreEqual(expected, testCount.Count);
    }

    public IEnumerator 조건에_맞는_시나리오를_실행시켜야_함(int expected, int a = -1, int b = -1, int c = -1)
    {
        TestCount testCount = new TestCount();
        var sut = CreateSut(testCount);

        sut.Shooting(new NudgeParmeter[][] {new NudgeParmeter[] {new NudgeParmeter("A", a), new NudgeParmeter("B", b) }, new NudgeParmeter[] { new NudgeParmeter("C", c) } });
        yield return null;
        yield return null;
        Assert.AreEqual(expected, testCount.Count);
    }

    public class TestCount
    {
        public int Count;
    }

    public class TestAct : IAct
    {
        TestCount _testCount;
        int _amount;
        public TestAct(TestCount testCount, int amount)
        {
            _testCount = testCount;
            _amount = amount;
        }

        public IEnumerator Execute()
        {
            _testCount.Count += _amount;
            yield return null;
        }
    }
}
