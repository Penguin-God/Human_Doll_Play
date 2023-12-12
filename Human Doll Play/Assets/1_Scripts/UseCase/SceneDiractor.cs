using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDiractor : MonoBehaviour
{
    SinarioNode _startNode;
    public void SetNode(SinarioNode startNode)
    {
        _startNode = startNode;
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
