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
        NudgeParameter.ChangeValue(value);
        SceneEnvirment.ChangeEnvierment();
    }

    public bool SameParameter(string name) => NudgeParameter.Name == name;
}

public class EnvirmentController
{
    HashSet<NudgeEnvierment> _nudgeEnvierments;
    public IEnumerable<NudgeParameter> NudgeParameters => _nudgeEnvierments.Select(x => x.NudgeParameter);
    public EnvirmentController(IEnumerable<NudgeEnvierment> nudgeEnvierments) => _nudgeEnvierments = new(nudgeEnvierments);

    public void ChangeEnvirment(string parameterName, int value)
    {
        if(FindEnvierment(parameterName, out var envierment))
            envierment.ChangeEnvirment(value);
    }

    bool FindEnvierment(string parameterName, out NudgeEnvierment nudgeEnvierment)
    {
        nudgeEnvierment = _nudgeEnvierments.FirstOrDefault(x => x.SameParameter(parameterName));
        return nudgeEnvierment != null;
    }
}
