using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SceneDiractorTests
{
    SceneDiractor CraeteSut(TestCount testCount)
    {
        var result = new GameObject().AddComponent<SceneDiractor>();
        result.SetNode(CreateNode(testCount));
        return result;
    }

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

    IEnumerable<IAct> CreateAct(TestCount count, int amount) => new IAct[] { new TestAct(count, amount) };

    [UnityTest]
    public IEnumerator 조건에_맞는_시나리오를_실행시켜야_함()
    {
        조건에_맞는_시나리오를_실행시켜야_함(new int[] { 0 }, 1);
        조건에_맞는_시나리오를_실행시켜야_함(new int[] { 0, 1, 1 }, 1);

        조건에_맞는_시나리오를_실행시켜야_함(new int[] { 1, 0 }, 3);
        조건에_맞는_시나리오를_실행시켜야_함(new int[] { 1, 0, 1 }, 3);

        조건에_맞는_시나리오를_실행시켜야_함(new int[] { 1, 1, 0 }, 8);
        조건에_맞는_시나리오를_실행시켜야_함(new int[] { 1, 1, 1 }, 9);
        조건에_맞는_시나리오를_실행시켜야_함(new int[] { 1, 1, 1, 1 }, 9);

        yield return null;
    }

    public IEnumerator 조건에_맞는_시나리오를_실행시켜야_함(int[] conditions, int expected)
    {
        TestCount testCount = new TestCount();
        var sut = CraeteSut(testCount);

        sut.Shooting(conditions);
        yield return null;
        Assert.AreEqual(expected, testCount.Count);
    }
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
