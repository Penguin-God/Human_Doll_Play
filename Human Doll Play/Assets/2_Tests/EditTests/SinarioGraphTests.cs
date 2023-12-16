using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SinarioGraphTests
{
    IEnumerable<IAct> CreateSinario(int amount) => new IAct[] { new TestCount() { Count = amount } };

    [Test]
    public void 그래프는_순차적으로_조건에_맞는_노드를_반환해야_함()
    {
        // Arrange
        var sinario2 = CreateSinario(2);
        var sinario3 = CreateSinario(3);
        var sinario4 = CreateSinario(4);
        var sinario5 = CreateSinario(5);
        var sinario6 = CreateSinario(6);
        var sut = SinarioTestHelper.CreateFiveSinarioGraph(sinario2, sinario3, sinario4, sinario5, sinario6);

        //Act & Assert
        AssertMoveSinario(sut, true, false, sinario2, a: 0);
        sut = SinarioTestHelper.CreateFiveSinarioGraph(sinario2, sinario3, sinario4, sinario5, sinario6);

        AssertMoveSinario(sut, true, false, sinario3, a: 1, b: 0);
        sut = SinarioTestHelper.CreateFiveSinarioGraph(sinario2, sinario3, sinario4, sinario5, sinario6);

        AssertMoveSinario(sut, false, false, sinario4, a: 1, b: 1);
        AssertMoveSinario(sut, true, false, sinario5, c: 0);
        sut = SinarioTestHelper.CreateFiveSinarioGraph(sinario2, sinario3, sinario4, sinario5, sinario6);

        AssertMoveSinario(sut, false, false, sinario4, a: 1, b: 1);
        AssertMoveSinario(sut, true, true, sinario6, c: 1);
    }

    void AssertMoveSinario(SinarioGraph sut, bool expectedLast, bool expectedSuccess, IEnumerable<IAct> sinario, int a = -1, int b = -1, int c = -1)
    {
        var result = sut.MoveNextSinario(SinarioTestHelper.CreateParms(a, b, c));
        Assert.AreSame(sinario, result.Sinario);
        Assert.AreEqual(expectedLast, result.IsLast);
        Assert.AreEqual(expectedSuccess, result.IsShootingSuccess);
    }

    public class TestCount : IAct
    {
        public int Count;

        public IEnumerator Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
