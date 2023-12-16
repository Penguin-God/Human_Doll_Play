using System;
using System.Collections;
using System.Collections.Generic;

public class EnvirmentStateEntity
{
    readonly public ParametersCondition Condition;
    readonly public int State;

    public EnvirmentStateEntity(ParametersCondition condition, int state)
    {
        Condition = condition;
        State = state;
    }
}
