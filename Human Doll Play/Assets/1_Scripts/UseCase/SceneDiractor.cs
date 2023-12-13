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

    IEnumerator Co_Shooting(IEnumerable<IAct> sinario)
    {
        foreach (var act in sinario)
            yield return StartCoroutine(act.Execute());
    }
}
