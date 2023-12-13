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
    public IEnumerator ���ǿ�_�´�_�ó�������_������Ѿ�_��()
    {
        yield return ���ǿ�_�´�_�ó�������_������Ѿ�_��(2, a:0);
        yield return ���ǿ�_�´�_�ó�������_������Ѿ�_��(3, a:1, b:0);

        yield return ���ǿ�_�´�_�ó�������_������Ѿ�_��(9, a: 1, b: 1, c: 0);
        yield return ���ǿ�_�´�_�ó�������_������Ѿ�_��(10, a: 1, b: 1, c: 1);
    }

    public IEnumerator ���ǿ�_�´�_�ó�������_������Ѿ�_��(int expected, int a = -1, int b = -1, int c = -1)
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
    public IEnumerator ����_����_���θ�_�̹�Ʈ��_ȣ���ؾ�_��()
    {
        yield return ����_����_���θ�_�̹�Ʈ��_ȣ���ؾ�_��(false, a: 0);
        yield return ����_����_���θ�_�̹�Ʈ��_ȣ���ؾ�_��(false, a: 1, b: 0);

        yield return ����_����_���θ�_�̹�Ʈ��_ȣ���ؾ�_��(false, a: 1, b: 1, c: 0);
        yield return ����_����_���θ�_�̹�Ʈ��_ȣ���ؾ�_��(true, a: 1, b: 1, c: 1);
    }

    public IEnumerator ����_����_���θ�_�̹�Ʈ��_ȣ���ؾ�_��(bool expected, int a = -1, int b = -1, int c = -1)
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
