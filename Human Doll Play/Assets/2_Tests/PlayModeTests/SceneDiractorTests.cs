using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SceneDiractorTests
{
    IEnumerable<IAct> CreateSinario(int amount, TestCount count) => new IAct[] { new TestAct(count, amount) };
    ShootingDiractor CreateSut(TestCount count)
    {
        var result = new GameObject().AddComponent<ShootingDiractor>();

        var sinario2 = CreateSinario(2, count);
        var sinario3 = CreateSinario(3, count);
        var sinario4 = CreateSinario(4, count);
        var sinario5 = CreateSinario(5, count);
        var sinario6 = CreateSinario(6, count);
        var graph = SinarioTestHelper.CreateFiveSinarioGraph(sinario2, sinario3, sinario4, sinario5, sinario6);
        result.SetGrahp(graph);
        return result;
    }

    [UnityTest]
    public IEnumerator 조건에_맞는_시나리오를_실행시켜야_함()
    {
        yield return 조건에_맞는_시나리오를_실행시켜야_함(2, a:0);
        yield return 조건에_맞는_시나리오를_실행시켜야_함(3, a:1, b:0);

        yield return 조건에_맞는_시나리오를_실행시켜야_함(9, a: 1, b: 1, c: 0);
        yield return 조건에_맞는_시나리오를_실행시켜야_함(10, a: 1, b: 1, c: 1);
    }

    public IEnumerator 조건에_맞는_시나리오를_실행시켜야_함(int expected, int a = -1, int b = -1, int c = -1)
    {
        TestCount testCount = new TestCount();
        var sut = CreateSut(testCount);

        sut.Shooting(CreateCondtions(a, b, c));
        yield return null;
        yield return null;
        Assert.AreEqual(expected, testCount.Count);
        Object.Destroy(sut.gameObject);
    }

    NudgeParameter[][] CreateCondtions(int a, int b, int c) 
        => new NudgeParameter[][] { new NudgeParameter[] { new NudgeParameter("A", a), new NudgeParameter("B", b) }, new NudgeParameter[] { new NudgeParameter("C", c) } };

    [UnityTest]
    public IEnumerator 성공_실패_여부를_이밴트로_호출해야_함()
    {
        yield return 성공_실패_여부를_이밴트로_호출해야_함(false, a: 0);
        yield return 성공_실패_여부를_이밴트로_호출해야_함(false, a: 1, b: 0);

        yield return 성공_실패_여부를_이밴트로_호출해야_함(false, a: 1, b: 1, c: 0);
        yield return 성공_실패_여부를_이밴트로_호출해야_함(true, a: 1, b: 1, c: 1);
    }

    public IEnumerator 성공_실패_여부를_이밴트로_호출해야_함(bool expected, int a = -1, int b = -1, int c = -1)
    {
        bool result = false;
        var sut = CreateSut(new TestCount());
        sut.OnShootingDone += isSuccess => result = isSuccess;

        sut.Shooting(CreateCondtions(a, b, c));
        yield return null;
        yield return null;
        Assert.AreEqual(expected, result);
        Object.Destroy(sut.gameObject);
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
