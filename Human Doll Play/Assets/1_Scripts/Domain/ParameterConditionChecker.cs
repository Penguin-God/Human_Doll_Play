using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ParameterConditionChecker
{
    public ParameterConditionChecker(IEnumerable<NudgeParameter> parmeters) => _transtionCondtions = parmeters;

    IEnumerable<NudgeParameter> _transtionCondtions = new List<NudgeParameter>();
    public bool CheckCondition(IEnumerable<NudgeParameter> conditions) => _transtionCondtions.All(parm => conditions.Any(x => x.Name == parm.Name && x.Value == parm.Value));
}
