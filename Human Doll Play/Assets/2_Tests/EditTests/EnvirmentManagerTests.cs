using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnvirmentManagerTests
{
    EnvirmentManager CreateSut() => new();
    [Test]
    public void 환경은_조건에_맞게_상태가_변화해야_함()
    {
        var sut = CreateSut();
        var envirment = new TestEnvirment();

        sut.ChangeEnviremt("1", 1);

        Assert.AreEqual(true, envirment.Flag);
    }

    class TestEnvirment : IEnvirment
    {
        public bool Flag;
        public void ChangeEnvierment(int value) => Flag = value == 1;
    }
}
