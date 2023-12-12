using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;

public class SinarioNode
{
    public SinarioNode(IEnumerable<IAct> sinario)
    {
        Sinario = sinario;
    }

    public readonly IEnumerable<IAct> Sinario;
    List<SinarioEdge> _edges = new List<SinarioEdge>();
    public void AddTranstion(SinarioEdge edge) => _edges.Add(edge);

    public void AddNode(SinarioNode node)
    {
        // _edges.Add(node);
    }

    public bool IsLast => _edges == null || _edges.Count == 0;

    public SinarioNode GetNextScenario(int index)
    {
        if (IsLast) return null;

        return null;// _edges[index];
    }
}

public class SinarioEdge
{
    List<NudgeParmeter> _parmeters = new List<NudgeParmeter>();
    public void AddParmeters(NudgeParmeter nudgeParmeter) => _parmeters.Add(nudgeParmeter);
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