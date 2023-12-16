using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ParametersCondition
{
    public ParametersCondition(IEnumerable<NudgeParameter> parmeters) => _transtionCondtions = parmeters.ToDictionary(x => x.Name, x => x);

    Dictionary<string, NudgeParameter> _transtionCondtions;
    public IEnumerable<NudgeParameter> Conditions => _transtionCondtions.Values;
    public bool CheckCondition(IEnumerable<NudgeParameter> conditions) => _transtionCondtions.All(x => conditions.Contains(x.Value));
    public bool HasParameter(string name) => _transtionCondtions.ContainsKey(name);
    public int GetValue(string name)
    {
        if (HasParameter(name))
            return _transtionCondtions[name].Value;
        return -1;
    }
    public void ChangeCondition(NudgeParameter nudgeParameter)
    {
        if (_transtionCondtions.ContainsKey(nudgeParameter.Name))
            _transtionCondtions[nudgeParameter.Name] = nudgeParameter;
    }
}
