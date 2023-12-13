using System.Collections;
using System.Collections.Generic;

public interface ISceneEnvirment
{
    public NudgeParameter ChangeEnvierment();
}

public interface IAct
{
    IEnumerator Execute();
}
