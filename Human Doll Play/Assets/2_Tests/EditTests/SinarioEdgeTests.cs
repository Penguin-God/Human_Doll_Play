using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SinarioEdgeTests
{
    IEnumerable<NudgeParameter> CreateParms(int a, int b) => new NudgeParameter[] {new NudgeParameter("A", a), new NudgeParameter("B", b)};
    SinarioEdge CreateSut(IEnumerable<NudgeParameter> parms) => new(parms);

    [Test]
    [TestCase(0, false)]
    [TestCase(1, true)]
    public void 조건이_하나면_그게_맞아야_참을_반환해야_함(int value, bool expected)
    {
        var sut = CreateSut(new NudgeParameter[] { new NudgeParameter("A", value) });
        var result = sut.CheckCondition(CreateParms(1, 0));

        Assert.AreEqual(expected, result);
    }

    [Test]
    [TestCase(0, 0, false)]
    [TestCase(0, 1, false)]
    [TestCase(1, 0, false)]
    [TestCase(1, 1, true)]
    public void 조건이_여러개면_그게_전부_옳아야_참을_봔환해야_함(int aValue, int bValue, bool expected)
    {
        var sut = CreateSut(CreateParms(aValue, bValue));

        var result = sut.CheckCondition(CreateParms(1, 1));

        Assert.AreEqual(expected, result);
    }
}
