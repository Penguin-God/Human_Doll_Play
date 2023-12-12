using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SianrioNodeTests
{
    IEnumerable<IAct> CreateAct(TestCount count, int amount) => new IAct[] { new TestAct(count, amount) };

    [Test]
    public void 조건에_맞는_다음_시나리오를_반환해야_함()
    {
        TestCount testCount = new ();
        SinarioNode sinarioNode1 = new SinarioNode(CreateAct(testCount, 1));
        SinarioNode sinarioNode2 = new SinarioNode(CreateAct(testCount, 2));
        SinarioNode sinarioNode3 = new SinarioNode(CreateAct(testCount, 3));
        SinarioNode sinarioNode4 = new SinarioNode(CreateAct(testCount, 4));
        SinarioNode sinarioNode5 = new SinarioNode(CreateAct(testCount, 5));
        SinarioNode sinarioNode6 = new SinarioNode(CreateAct(testCount, 6));
        SinarioNode sinarioNode7 = new SinarioNode(CreateAct(testCount, 7));

        // 간선 넣고 테스트하기
        //sinarioNode1.AddNode(sinarioNode2);
        //sinarioNode1.AddNode(sinarioNode3);

        //sinarioNode3.AddNode(sinarioNode4);
        //sinarioNode3.AddNode(sinarioNode5);

        //sinarioNode5.AddNode(sinarioNode6);
        //sinarioNode5.AddNode(sinarioNode7);
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
