using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayActor : IAct
{
    readonly float Delay;
    public DelayActor(float delay)
    {
        Delay = delay;
    }

    public IEnumerator Execute()
    {
        yield return new WaitForSeconds(Delay);
    }
}
