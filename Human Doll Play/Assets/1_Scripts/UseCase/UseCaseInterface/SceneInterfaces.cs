using System.Collections;
using System.Collections.Generic;

public interface ISceneEnvirment
{
    public void SetupEnvierment();
    public int GetState();
}

public interface IAct
{
    IEnumerator Execute();
}
