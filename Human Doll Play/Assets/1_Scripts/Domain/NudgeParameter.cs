using System.Collections;
using System.Collections.Generic;

public class NudgeParameter
{
    public readonly string Name;
    public int Value { get; private set; } = 0;
    public NudgeParameter(string name) => Name = name;

    public void ChangeValue(int value) => Value = value;
}