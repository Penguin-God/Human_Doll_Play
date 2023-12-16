using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NudgeParameterControllerTests
{
    IEnumerable<EnvirmentStateEntity> CreateEntity(int a, int b, int c, int state) => new EnvirmentStateEntity[] { new EnvirmentStateEntity(ParameterCreator.CreateCondition(a, b, c), state) };
    EnvirmentStateController CreateController(int state, IEnvirment envirment, int a = -1, int b = -1, int c = -1) => new(CreateEntity(a, b, c, state), envirment);
    [Test]
    public void 파라미터_변경_후_환경도_그에_맞게_세팅해야_함()
    {
        var envirment = EnvirmentTestHelper.CreateTestEnvirment();
        var parms = ParameterCreator.Create1Parms(0);
        var envirmentManager = new EnvirmentManager(new EnvirmentStateController[] { CreateController(1, envirment, 1) }, parms);
        var sut = new NudgeParameterController(parms, envirmentManager);

        sut.ChangeParameter("A", 1);

        Assert.AreEqual(sut.GetParameterValue("A"), 1);
        Assert.IsTrue(envirment.Flag);
    }
}
