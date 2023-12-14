using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEmotionState
{
    public int Happiness { get; private set; }
    const int MinHappiness = 0;
    const int MaxHappiness = 100;

    public CharacterEmotionState(int happiness) => Happiness = happiness;

    public void ChangeHappier(int amount) => ClampHappiness(Happiness + amount);

    void ClampHappiness(int newHappiness) => Happiness = Mathf.Clamp(newHappiness, MinHappiness, MaxHappiness);
}
