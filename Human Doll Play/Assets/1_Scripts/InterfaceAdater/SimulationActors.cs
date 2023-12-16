using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveActor : IAct
{
    readonly ObjectMover _mover;
    readonly IEnumerable<MoveEntity> _moves;
    public ObjectMoveActor(ObjectMover mover, IEnumerable<MoveEntity> dirs)
    {
        _mover = mover;
        _moves = dirs;
    }

    public IEnumerator Execute()
    {
        foreach (var move in _moves)
            yield return _mover.Co_Move(move);
    }
}


public class CharacterRotator : IAct
{
    readonly CharacterMover _rotator;
    readonly Direction _dir;
    public CharacterRotator(CharacterMover rotator, Direction dir)
    {
        _rotator = rotator;
        _dir = dir;
    }

    public IEnumerator Execute()
    {
        _rotator.RotateToDir(_dir);
        yield return null;
    }
}


public class EnvirmentChangeActor : IAct
{
    readonly ISceneEnvirment _envirment;
    readonly int Value;
    public EnvirmentChangeActor(ISceneEnvirment envirment, int value)
    {
        _envirment = envirment;
        Value = value;
    }

    public IEnumerator Execute()
    {
        _envirment.ChangeEnvierment(Value);
        yield return null;
    }
}


public class SoundActor : IAct
{
    AudioClip _clip;
    public SoundActor(AudioClip clip)
    {
        _clip = clip;
    }
    public IEnumerator Execute()
    {
        SoundManager.Instance.PlaySound(_clip);
        yield return new WaitForSeconds(_clip.length);
    }
}


public class DelayActor : IAct
{
    readonly float Delay;
    public DelayActor(float delay)
    {
        Delay = delay;
    }

    public IEnumerator Execute()
    {
        yield return new WaitForSeconds(Delay);
    }
}