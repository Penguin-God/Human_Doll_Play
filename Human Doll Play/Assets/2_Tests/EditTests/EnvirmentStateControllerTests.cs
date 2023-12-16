using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnvirmentStateControllerTests
{
    EnvirmentStateEntity CreateEntity(int a, int b, int state) => new EnvirmentStateEntity(new ParametersCondition(Create2Parms(a, b)), state);
    IEnumerable<NudgeParameter> Create2Parms(int a, int b) => new NudgeParameter[] { new NudgeParameter("A", a), new NudgeParameter("B", b) };
    [Test]
    [TestCase(0, 0, false)]
    [TestCase(0, 1, false)]
    [TestCase(1, 0, false)]
    [TestCase(1, 1, true)]
    public void 환경은_조건에_맞는_상태를_적용해야_함(int a, int b, bool expected)
    {
        var entitys = CreateEntity(1, 1, 1);
        var envirment = new TestEnvirment();
        var sut = new EnvirmentStateController(new EnvirmentStateEntity[] { entitys }, envirment);

        sut.UpdateState(Create2Parms(a, b));

        Assert.AreEqual(expected, envirment.Flag);
    }

    class TestEnvirment : IEnvirment
    {
        public bool Flag;
        public void ChangeEnvierment(int value) => Flag = value == 1;
    }
}
