using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SinarioEdgeTests
{
    IEnumerable<NudgeParmeter> CreateParms(int a, int b) => new NudgeParmeter[] {new NudgeParmeter("A", a), new NudgeParmeter("B", b)};
    SinarioEdge CreateSut(IEnumerable<NudgeParmeter> parms) => new(parms);

    [Test]
    [TestCase(0, false)]
    [TestCase(1, true)]
    public void ������_�ϳ���_�װ�_�¾ƾ�_����_��ȯ�ؾ�_��(int value, bool expected)
    {
        var sut = CreateSut(new NudgeParmeter[] { new NudgeParmeter("A", value) });
        var result = sut.CheckCondition(CreateParms(1, 0));

        Assert.AreEqual(expected, result);
    }

    [Test]
    [TestCase(0, 0, false)]
    [TestCase(0, 1, false)]
    [TestCase(1, 0, false)]
    [TestCase(1, 1, true)]
    public void ������_��������_�װ�_����_�Ǿƾ�_����_��ȯ�ؾ�_��(int aValue, int bValue, bool expected)
    {
        var sut = CreateSut(CreateParms(aValue, bValue));

        var result = sut.CheckCondition(CreateParms(1, 1));

        Assert.AreEqual(expected, result);
    }
}
