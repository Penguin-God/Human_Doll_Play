using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NudgeEnvierment
{
    public readonly NudgeParameter NudgeParameter;
    readonly ISceneEnvirment SceneEnvirment;
    
    public NudgeEnvierment(string parameterName, ISceneEnvirment sceneEnvirment)
    {
        NudgeParameter = new NudgeParameter(parameterName);
        SceneEnvirment = sceneEnvirment;
    }

    public void ChangeEnvirment(int value)
    {
        NudgeParameter.ChangeValue(value); // 변수 설정이랑은 별개로 해야 재활용 가능할 듯.
        SceneEnvirment?.ChangeEnvierment(value);
    }

    public bool SameParameter(string name) => NudgeParameter.Name == name;
}

public class EnvirmentController
{
    HashSet<NudgeEnvierment> _nudgeEnvierments;
    public IEnumerable<NudgeParameter> NudgeParameters => _nudgeEnvierments.Select(x => x.NudgeParameter);
    public EnvirmentController(IEnumerable<NudgeEnvierment> nudgeEnvierments, IEnumerable<ConditionalActiveObject> conditionalActiveObjects)
    {
        _nudgeEnvierments = new(nudgeEnvierments);
        _conditionalActiveObjects = conditionalActiveObjects;
    }

    readonly IEnumerable<ConditionalActiveObject> _conditionalActiveObjects;

    public void ChangeEnvirment(string parameterName, int value)
    {
        if (FindEnvierment(parameterName, out var envierment) == false) return;

        envierment.ChangeEnvirment(value);
        foreach (var conditional in _conditionalActiveObjects)
            conditional.Set();
    }

    public int GetEnvirmentValue(string parameterName)
    {
        if (FindEnvierment(parameterName, out var envierment))
            return envierment.NudgeParameter.Value;
        return -1;
    }

    bool FindEnvierment(string parameterName, out NudgeEnvierment nudgeEnvierment)
    {
        nudgeEnvierment = _nudgeEnvierments.FirstOrDefault(x => x.SameParameter(parameterName));
        return nudgeEnvierment != null;
    }
}
