using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnvirmentController
{
    readonly public IEnumerable<NudgeParameter> NudgeParameters;

    public EnvirmentController(IEnumerable<NudgeParameter> initailParameters) => NudgeParameters = initailParameters;

    public void ChangeEnvirment(ISceneEnvirment envirment)
    {
        var newEnvirementParameter = envirment.ChangeEnvierment();
        NudgeParameters.First(x => newEnvirementParameter.Name == x.Name).Value = newEnvirementParameter.Value;
    }
}
