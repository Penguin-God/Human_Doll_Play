using System.Collections;
using System.Collections.Generic;

public interface IAct
{
    IEnumerator Execute();
}

public class Secnario
{
    public IEnumerator Shooting(IEnumerable<IAct> acts)
    {
        foreach (var act in acts)
            yield return act.Execute();
    }
}
