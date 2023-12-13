using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnvirmentController
{
     readonly Dictionary<ISceneEnvirment, NudgeParameter> _envirmentByParameter;
    public EnvirmentController(IEnumerable<ISceneEnvirment> envirments)
    {
        foreach (var envirment in envirments)
            _envirmentByParameter.Add(envirment, new NudgeParameter("", 0)); // 이름 정하기
    }

    public void ChangeEnvirment(ISceneEnvirment envirment)
    {
        _envirmentByParameter[envirment].Value = envirment.ChangeEnvierment();
    }
}
