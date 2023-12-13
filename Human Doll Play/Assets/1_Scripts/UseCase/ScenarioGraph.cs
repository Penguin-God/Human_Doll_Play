using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SinarioNode
{
    Dictionary<SinarioEdge, SinarioNode> _edgeByTarget = new ();
    public void AddTranstion(SinarioEdge edge, SinarioNode target) => _edgeByTarget.Add(edge, target);

    public bool IsSuccess { get; private set; } = false;
    public bool IsLast => _edgeByTarget == null || _edgeByTarget.Count() == 0;

    public SinarioNode GetNextScenario(IEnumerable<NudgeParmeter> parmeters)
    {
        if (IsLast) return null;

        return _edgeByTarget.First(x => x.Key.CheckCondition(parmeters)).Value;
    }

    public static SinarioNode CreateSuccessNode() => new () { IsSuccess = true };
}

public class SinarioEdge
{
    public SinarioEdge(IEnumerable<NudgeParmeter> parmeters) => _transtionCondtions = parmeters;

    IEnumerable<NudgeParmeter> _transtionCondtions = new List<NudgeParmeter>();
    public bool CheckCondition(IEnumerable<NudgeParmeter> conditions) => _transtionCondtions.All(parm => conditions.Any(x => x.Name == parm.Name && x.Value == parm.Value));
}

public class SinarioGraph
{
    Dictionary<SinarioNode, IEnumerable<IAct>> _nodeBySinario = new();
    SinarioNode _startNode;
    SinarioNode _currentNode = null;
    public SinarioGraph(SinarioNode startSinario)
    {
        _startNode = startSinario;
        _currentNode = startSinario;
    }
    public void AddSianrio(SinarioNode node, IEnumerable<IAct> sinario) => _nodeBySinario.Add(node, sinario);

    public bool MoveNextSinario(IEnumerable<NudgeParmeter> nudgeParmeters, out IEnumerable<IAct> sinario)
    {
        _currentNode = _currentNode.GetNextScenario(nudgeParmeters);
        sinario = _nodeBySinario[_currentNode];
        return _currentNode.IsLast;
    }

    public void ResetSianrio() => _currentNode = _startNode;
}