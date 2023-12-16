using System.Collections;
using System.Collections.Generic;

public class EnvirmentStateController
{
    readonly EnvirmentStateEntity _envirmentStateEntity;
    readonly IEnvirment _envirment;

    public EnvirmentStateController(EnvirmentStateEntity envirmentStateEntity, IEnvirment envirment)
    {
        _envirmentStateEntity = envirmentStateEntity;
        _envirment = envirment;
    }

    public void UpdateState()
    {

    }
}
