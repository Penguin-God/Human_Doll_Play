using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SinarioNode
{
    Dictionary<SinarioEdge, SinarioNode> _edgeByTarget = new();
    public void AddTranstion(SinarioEdge edge, SinarioNode target) => _edgeByTarget.Add(edge, target);

    public bool IsSuccess { get; private set; } = false;
    public bool IsLast => _edgeByTarget == null || _edgeByTarget.Count() == 0;

    public SinarioNode GetNextScenario(IEnumerable<NudgeParameter> parmeters)
    {
        if (IsLast) return null;

        return _edgeByTarget.First(x => x.Key.CheckCondition(parmeters)).Value;
    }

    public static SinarioNode CreateSuccessNode() => new() { IsSuccess = true };
}

public class SinarioEdge
{
    public SinarioEdge(IEnumerable<NudgeParameter> parmeters) => _transtionCondtions = parmeters;

    IEnumerable<NudgeParameter> _transtionCondtions = new List<NudgeParameter>();
    public bool CheckCondition(IEnumerable<NudgeParameter> conditions) => _transtionCondtions.All(parm => conditions.Any(x => x.Name == parm.Name && x.Value == parm.Value));
}