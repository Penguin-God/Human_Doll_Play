using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ParametersConditionTests
{
    IEnumerable<NudgeParameter> CreateParms(int a, int b) => SinarioTestHelper.CreateParms(a, b);
    ParametersCondition CreateSut(IEnumerable<NudgeParameter> parms) => new(parms);

    [Test]
    [TestCase(0, false)]
    [TestCase(1, true)]
    public void 조건이_하나면_그게_맞아야_참을_반환해야_함(int value, bool expected)
    {
        var sut = CreateSut(SinarioTestHelper.CreateParms(value));

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

    [Test]
    public void 조건이_바뀜에_따라_결과도_바뀌어야_함()
    {
        var sut = CreateSut(CreateParms(0, 0));

        var result = sut.CheckCondition(CreateParms(1, 0));
        Assert.IsFalse(result);

        sut.ChangeCondition(new NudgeParameter("A", 1));

        result = sut.CheckCondition(CreateParms(1, 0));
        Assert.IsTrue(result);
    }
}
