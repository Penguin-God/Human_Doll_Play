using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEmotionState
{
    int _happiness;
    const int MinHappiness = 0;
    const int MaxHappiness = 100;

    public CharacterEmotionState(int happiness) => _happiness = happiness;

    public int ChangeHappier(int amount)
    {
        _happiness += amount;
        ClampHappiness();
        return _happiness;
    }

    int ClampHappiness() => _happiness = Mathf.Clamp(_happiness, MinHappiness, MaxHappiness);
}
