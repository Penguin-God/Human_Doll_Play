using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnvirmentStateEntityTests
{
    [Test]
    public void 조건에_맞는_환경_변수를_반환해야_함()
    {
        var sut = new EnvirmentStateEntity();

        int result = sut.GetState();

        Assert.AreEqual(1, result);
    }
}
