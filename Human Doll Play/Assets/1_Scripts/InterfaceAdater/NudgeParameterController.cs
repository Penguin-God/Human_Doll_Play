using System.Collections;
using System.Collections.Generic;

public class NudgeParameterController
{
    readonly public ParametersCondition Condition;
    readonly EnvirmentManager _envirmentManager;
    public NudgeParameterController(IEnumerable<NudgeParameter> conditions, EnvirmentManager envirmentManager)
    {
        Condition = new ParametersCondition(conditions);
        _envirmentManager = envirmentManager;
    }

    public void ChangeParameter(string parameterName, int value)
    {
        var parameter = new NudgeParameter(parameterName, value);
        if (Condition.HasParameter(parameter.Name) == false) return;

        Condition.ChangeCondition(parameter);
        _envirmentManager.ChangeEnviremt(parameter);
    }

    public int GetParameterValue(string parameterName) => Condition.GetValue(parameterName);
}
