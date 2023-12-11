using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAct
{
    IEnumerator Execute();
}


public class ScriptPhotographer : MonoBehaviour
{
    IEnumerable<IAct> _scriptBook;
    public void SetScriptBook(IEnumerable<IAct> scriptBook) => _scriptBook = scriptBook;
    public void Shooting(IEnumerable<IAct> acts) => StartCoroutine(Co_Shooting(acts));

    IEnumerator Co_Shooting(IEnumerable<IAct> acts)
    {
        foreach (var act in acts)
            yield return StartCoroutine(act.Execute());
    }
}
