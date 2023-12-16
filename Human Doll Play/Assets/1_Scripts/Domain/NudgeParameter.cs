using System.Collections;
using System.Collections.Generic;

public struct NudgeParameter
{
    public readonly string Name;
    public int Value { get; private set; }
    public NudgeParameter(string name)
    {
        Name = name;
        Value = 0;
    }

    public void ChangeValue(int value) => Value = value;
}