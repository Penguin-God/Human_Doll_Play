using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnvirmentManagerTests
{
    IEnumerable<EnvirmentStateEntity> CreateEntity(int a, int b, int c, int state) => new EnvirmentStateEntity[] { new EnvirmentStateEntity(ParameterCreator.CreateCondition(a, b, c), state) };
    EnvirmentStateController CreateController(int state, IEnvirment envirment, int a = -1 , int b = -1, int c = -1)  => new (CreateEntity(a, b, c, state), envirment);

    [Test]
    public void 환경은_조건에_맞게_상태가_변화해야_함()
    {
        var envirment1 = EnvirmentTestHelper.CreateTestEnvirment();
        var envirment2 = EnvirmentTestHelper.CreateTestEnvirment();
        var envirment3 = EnvirmentTestHelper.CreateTestEnvirment();

        var controllers = new EnvirmentStateController[]
        {
            CreateController(1, envirment1, a: 1),
            CreateController(1, envirment2, a: 1, b: 1),
            CreateController(1, envirment3, a: 1, c: 1),
        };
        var sut = new EnvirmentManager(controllers, ParameterCreator.Create3Parms(0, 0, 0));
        AssertEnvirment(false, false, false);

        ChangeEnviremt("A", 1);
        AssertEnvirment(true, false, false);

        ChangeEnviremt("B", 1);
        AssertEnvirment(true, true, false);

        ChangeEnviremt("C", 1);
        AssertEnvirment(true, true, true);

        ChangeEnviremt("A", 0);
        AssertEnvirment(false, false, false);

        void AssertEnvirment(bool ep1, bool ep2, bool ep3)
        {
            Assert.AreEqual(ep1, envirment1.Flag);
            Assert.AreEqual(ep2, envirment2.Flag);
            Assert.AreEqual(ep3, envirment3.Flag);
        }

        void ChangeEnviremt(string name, int value) => sut.ChangeEnviremt(new NudgeParameter(name, value));
    }
}
