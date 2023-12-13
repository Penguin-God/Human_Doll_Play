using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NudgeParmeter
{
    public readonly string Name;
    public int Value;

    public NudgeParmeter(string name, int value)
    {
        Name = name;
        Value = value;
    }
}

public class SceneDiractor : MonoBehaviour
{
    SinarioNode _startNode;
    public void SetNode(SinarioNode startNode)
    {
        _startNode = startNode;
    }

    SinarioGraph _sinarioGraph;
    public void SetGrahp(SinarioGraph sinarioGraph) => _sinarioGraph = sinarioGraph;

    public void Shooting(IEnumerable<IEnumerable<NudgeParmeter>> nudgeParmeters) => StartCoroutine(Co_Shooting(nudgeParmeters));
    IEnumerator Co_Shooting(IEnumerable<IEnumerable<NudgeParmeter>> nudgeParmeters)
    {
        foreach (var parmeters in nudgeParmeters)
        {
            bool isLast = _sinarioGraph.MoveNextSinario(parmeters, out var sinario);
            yield return StartCoroutine(Co_Shooting(sinario));
            if (isLast) break;
        }
    }

    public void Shooting(IEnumerable<int> indexs) => StartCoroutine(Co_Shooting(indexs));

    IEnumerator Co_Shooting(IEnumerable<int> indexs)
    {
        SinarioNode shootingNode = _startNode;
        foreach (var index in indexs)
        {
            yield return StartCoroutine(Co_Shooting(shootingNode.Sinario));
            shootingNode = shootingNode.GetNextScenario(index);
        }
    }

    IEnumerator Co_Shooting(IEnumerable<IAct> sinario)
    {
        foreach (var act in sinario)
            yield return StartCoroutine(act.Execute());
    }
}
