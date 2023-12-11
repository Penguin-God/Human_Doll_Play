using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterStateTests
{
    CharacterEmotionState CreateSut() => new (50);

    [Test]
    [TestCase(30, 80)]
    [TestCase(70, 100)]
    [TestCase(-20, 30)]
    [TestCase(-200, 0)]
    public void �ູ����_����_������_���ؾ�_��(int amount, int expected)
    {
        int result = CreateSut().ChangeHappier(amount);
        Assert.AreEqual(expected, result);
    }
}
