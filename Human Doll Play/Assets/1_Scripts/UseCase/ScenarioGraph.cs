using System.Collections;
using System.Collections.Generic;

public class SinarioNode
{
    public SinarioNode(IEnumerable<IAct> sinario)
    {
        Sinario = sinario;
    }

    public readonly IEnumerable<IAct> Sinario;
    List<SinarioNode> _linkNodes = new List<SinarioNode>();
    public void AddNode(SinarioNode node) => _linkNodes.Add(node);

    public bool IsLast => _linkNodes == null || _linkNodes.Count == 0;

    public SinarioNode GetNextScenario(int index)
    {
        if (IsLast) return null;

        return _linkNodes[index];
    }
}