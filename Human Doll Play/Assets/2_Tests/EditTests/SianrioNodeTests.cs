using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SianrioNodeTests
{
    SinarioEdge CraeteEdge(params NudgeParmeter[] parmeters) => new (parmeters);

    IEnumerable<NudgeParmeter> CraeteParms(int a = -1, int b = -1, int c = -1) => new NudgeParmeter[] { new NudgeParmeter("A", a), new NudgeParmeter("B", b), new NudgeParmeter("C", c) };

    [Test]
    public void 조건에_부합하는_시나리오를_반환해야_함()
    {
        // Arrange
        SinarioNode sinarioNode1 = new (null);
        SinarioNode sinarioNode2 = new (null);
        SinarioNode sinarioNode3 = new (null);
        SinarioNode sinarioNode4 = new (null);
        SinarioNode sinarioNode5 = new (null);
        SinarioNode sinarioNode6 = new (null);
        
        var sinarioEdge1 = CraeteEdge(new NudgeParmeter("A", 0));
        var sinarioEdge2 = CraeteEdge(new NudgeParmeter("A", 1), new NudgeParmeter("B", 0));
        var sinarioEdge3 = CraeteEdge(new NudgeParmeter("A", 1), new NudgeParmeter("B", 1));
        var sinarioEdge4 = CraeteEdge(new NudgeParmeter("C", 0));
        var sinarioEdge5 = CraeteEdge(new NudgeParmeter("C", 1));

        sinarioNode1.AddTranstion(sinarioEdge1, sinarioNode2);
        sinarioNode1.AddTranstion(sinarioEdge2, sinarioNode3);
        sinarioNode1.AddTranstion(sinarioEdge3, sinarioNode4);

        sinarioNode4.AddTranstion(sinarioEdge4, sinarioNode5);
        sinarioNode4.AddTranstion(sinarioEdge5, sinarioNode6);

        //Act & Assert
        Assert.AreSame(sinarioNode2, sinarioNode1.GetNextScenario(CraeteParms(a: 0)));
        Assert.AreSame(sinarioNode2, sinarioNode1.GetNextScenario(CraeteParms(a: 0, b: 1)));
        Assert.AreSame(sinarioNode3, sinarioNode1.GetNextScenario(CraeteParms(a: 1, b: 0)));
        Assert.AreSame(sinarioNode4, sinarioNode1.GetNextScenario(CraeteParms(a: 1, b: 1)));

        Assert.AreSame(sinarioNode5, sinarioNode4.GetNextScenario(CraeteParms(c: 0)));
        Assert.AreSame(sinarioNode6, sinarioNode4.GetNextScenario(CraeteParms(c: 1)));
    }
}
