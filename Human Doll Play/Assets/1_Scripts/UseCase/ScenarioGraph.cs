using System.Collections;
using System.Collections.Generic;

public readonly struct SinarioData
{
    public readonly IEnumerable<IAct> Sinario;
    public readonly bool IsLast;
    public readonly bool IsShootingSuccess;

    public SinarioData(IEnumerable<IAct> sinario, bool isLast, bool isShootingSuccess)
    {
        Sinario = sinario;
        IsLast = isLast;
        IsShootingSuccess = isShootingSuccess;
    }
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

    public SinarioData MoveNextSinario(IEnumerable<NudgeParameter> nudgeParmeters)
    {
        _currentNode = _currentNode.GetNextScenario(nudgeParmeters);
        return new SinarioData(_nodeBySinario[_currentNode], _currentNode.IsLast, _currentNode.IsSuccess);
    }

    public void ResetSianrio() => _currentNode = _startNode;
}