using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnvirmentStateEntityTests
{
    [Test]
    public void ���ǿ�_�´�_ȯ��_������_��ȯ�ؾ�_��()
    {
        var sut = new EnvirmentStateEntity();

        int result = sut.GetState();

        Assert.AreEqual(1, result);
    }
}
