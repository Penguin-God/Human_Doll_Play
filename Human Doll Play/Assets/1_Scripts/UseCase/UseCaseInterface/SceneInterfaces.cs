using System.Collections;
using System.Collections.Generic;

public interface ISceneEnvirment
{
    public void ChangeEnvierment(int value);
}

public interface IAct
{
    IEnumerator Execute();
}
