using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnvirmentStateController
{
    readonly IEnumerable<EnvirmentStateEntity> _envirmentStateEntitys;
    readonly IEnvirment _envirment;

    public EnvirmentStateController(IEnumerable<EnvirmentStateEntity> envirmentStateEntitys, IEnvirment envirment)
    {
        _envirmentStateEntitys = envirmentStateEntitys;
        _envirment = envirment;
    }

    const int DefaultState = 0;
    public void UpdateState(IEnumerable<NudgeParameter> condition)
    {
        var entity = _envirmentStateEntitys.FirstOrDefault(x => x.Condition.CheckCondition(condition));
        if (entity == null) _envirment.ChangeEnvierment(DefaultState);
        else _envirment.ChangeEnvierment(entity.State);
    }
}
