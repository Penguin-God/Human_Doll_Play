using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDiractor : MonoBehaviour
{
    SinarioGraph _sinarioGraph;
    public void SetGrahp(SinarioGraph sinarioGraph) => _sinarioGraph = sinarioGraph;
    public event Action<bool> OnShootingDone = null;

    public void Shooting(IEnumerable<IEnumerable<NudgeParameter>> nudgeParmeters) => StartCoroutine(Co_Shooting(nudgeParmeters));
    IEnumerator Co_Shooting(IEnumerable<IEnumerable<NudgeParameter>> nudgeParmeters)
    {
        SinarioData sinarioData = new();
        foreach (var parmeters in nudgeParmeters)
        {
            sinarioData = _sinarioGraph.MoveNextSinario(parmeters);
            yield return StartCoroutine(Co_Shooting(sinarioData.Sinario));
            if (sinarioData.IsLast) break;
        }

        OnShootingDone?.Invoke(sinarioData.IsShootingSuccess);
    }

    IEnumerator Co_Shooting(IEnumerable<IAct> sinario)
    {
        foreach (var act in sinario)
            yield return StartCoroutine(act.Execute());
    }
}
