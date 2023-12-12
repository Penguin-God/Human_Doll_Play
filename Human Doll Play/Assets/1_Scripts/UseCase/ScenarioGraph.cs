using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SinarioNode
{
    public SinarioNode(IEnumerable<IAct> sinario)
    {
        Sinario = sinario;

    }

    public readonly IEnumerable<IAct> Sinario;
    Dictionary<SinarioEdge, SinarioNode> _edgeByTarget = new Dictionary<SinarioEdge, SinarioNode>();
    public void AddTranstion(SinarioEdge edge, SinarioNode target) => _edgeByTarget.Add(edge, target);

    public bool IsLast => _edgeByTarget == null || _edgeByTarget.Count() == 0;

    public SinarioNode GetNextScenario(int index)
    {
        if (IsLast) return null;

        return null;
    }

    public SinarioNode GetNextScenario(IEnumerable<NudgeParmeter> parmeters)
    {
        if (IsLast) return null;

        return _edgeByTarget.First(x => x.Key.CheckCondition(parmeters)).Value;
    }
}

public class SinarioEdge
{
    public SinarioEdge(IEnumerable<NudgeParmeter> parmeters) => _parmeters = parmeters;


    IEnumerable<NudgeParmeter> _parmeters = new List<NudgeParmeter>();
    public bool CheckCondition(IEnumerable<NudgeParmeter> conditions) => _parmeters.All(parm => conditions.Any(x => x.Name == parm.Name && x.Value == parm.Value));
}

public class SinarioGraph
{
    List<SinarioEdge> _startTranstion = new List<SinarioEdge>();
    SinarioNode _currentNode = null;
    public void AddStartTranstion(SinarioEdge sinarioTranstion) => _startTranstion.Add(sinarioTranstion);

    //public SinarioNode MoveNextNode(IEnumerable<NudgeParmeter> nudgeParmeters)
    //{
    //    return _currentNode.GetNextScenario(nudgeParmeters);
    //}
}