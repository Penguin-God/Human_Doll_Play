using System;
using System.Collections;
using System.Collections.Generic;

public class EnvirmentManager
{
    readonly IEnumerable<EnvirmentStateController> _controllers;
    readonly ParametersCondition _condition;
    public EnvirmentManager(IEnumerable<EnvirmentStateController> controllers, IEnumerable<NudgeParameter> condition)
    {
        _controllers = controllers;
        _condition = new ParametersCondition(condition);
    }

    public void ChangeEnviremt(NudgeParameter parameter)
    {
        _condition.ChangeCondition(parameter);
        foreach (var controller in _controllers)
            controller.UpdateState(_condition.Conditions);
    }
}
