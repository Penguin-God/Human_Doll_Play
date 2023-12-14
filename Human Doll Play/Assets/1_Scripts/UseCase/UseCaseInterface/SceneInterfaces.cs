using System.Collections;
using System.Collections.Generic;

public interface ISceneEnvirment
{
    public void ChangeEnvierment();
}

public interface IAct
{
    IEnumerator Execute();
}
