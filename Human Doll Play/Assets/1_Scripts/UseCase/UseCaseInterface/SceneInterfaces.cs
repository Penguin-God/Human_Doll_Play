using System.Collections;
using System.Collections.Generic;

public interface ISceneEnvirment
{
    public int ChangeEnvierment();
}

public interface IAct
{
    IEnumerator Execute();
}
