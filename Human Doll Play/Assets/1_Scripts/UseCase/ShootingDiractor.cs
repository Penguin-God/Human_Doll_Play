using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDiractor : MonoBehaviour
{
    SinarioGraph _sinarioGraph;
    public void SetGrahp(SinarioGraph sinarioGraph) => _sinarioGraph = sinarioGraph;
    public event Action<bool> OnShootingDone = null;

    public void Shooting(IEnumerable<NudgeParameter> nudgeParmeters) => StartCoroutine(Co_Shooting(nudgeParmeters));
    IEnumerator Co_Shooting(IEnumerable<NudgeParameter> nudgeParmeters)
    {
        SinarioData sinarioData;
        do
        {
            sinarioData = _sinarioGraph.MoveNextSinario(nudgeParmeters);
            yield return StartCoroutine(Co_Shooting(sinarioData.Sinario));
        } while (sinarioData.IsLast == false);

        OnShootingDone?.Invoke(sinarioData.IsShootingSuccess);
    }

    IEnumerator Co_Shooting(IEnumerable<IAct> sinario)
    {
        foreach (var act in sinario)
            yield return StartCoroutine(act.Execute());
    }
}
