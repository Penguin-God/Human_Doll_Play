using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ParametersCondition
{
    public ParametersCondition(IEnumerable<NudgeParameter> parmeters) => _transtionCondtions = parmeters.ToDictionary(x => x.Name, x => x);

    Dictionary<string, NudgeParameter> _transtionCondtions;

    // public bool CheckCondition(IEnumerable<NudgeParameter> conditions) => _transtionCondtions.All(parm => conditions.Any(x => x.Name == parm.Name && x.Value == parm.Value));
    public bool CheckCondition(IEnumerable<NudgeParameter> conditions) => _transtionCondtions.All(x => conditions.Contains(x.Value));

    public void ChangeCondition(NudgeParameter nudgeParameter)
    {
        if (_transtionCondtions.ContainsKey(nudgeParameter.Name))
            _transtionCondtions[nudgeParameter.Name] = nudgeParameter;
    }
}
