using System.Collections;
using System.Collections.Generic;

public static class ParameterCreator
{
    readonly static string[] Names = new string[] {"A",  "B", "C"};
    public static IEnumerable<NudgeParameter> Create1Parms(int a) => CreateParms(a);
    public static IEnumerable<NudgeParameter> Create2Parms(int a, int b) => CreateParms(a, b);
    public static IEnumerable<NudgeParameter> Create3Parms(int a, int b, int c) => CreateParms(a, b, c);

    public static ParametersCondition CreateCondition(int a = -1, int b = -1, int c = -1) => new ParametersCondition(CreateParms(a, b, c));

    static IEnumerable<NudgeParameter> CreateParms(params int[] values)
    {
        var result = new List<NudgeParameter>();
        for(int i = 0; i < values.Length; i++)
        {
            if (values[i] >= 0)
                result.Add(new NudgeParameter(Names[i], values[i]));
        }
        return result;
    }
}
