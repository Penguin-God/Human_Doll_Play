using System.Collections;
using System.Collections.Generic;

public class EnvirmentInteractionActor : IAct
{
    readonly ISceneEnvirment _envirment;
    readonly int Value;
    public EnvirmentInteractionActor(ISceneEnvirment envirment, int value)
    {
        _envirment = envirment;
        Value = value;
    }

    public IEnumerator Execute()
    {
        _envirment.ChangeEnvierment(Value);
        yield return null;
    }
}
