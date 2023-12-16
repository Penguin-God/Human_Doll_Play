using System.Collections;
using System.Collections.Generic;

public interface IEnvirment
{
    public void ChangeEnvierment(int value);
}

public interface IAct
{
    IEnumerator Execute();
}
