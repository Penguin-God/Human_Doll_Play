using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveActor : IAct
{
    readonly CharacterMover _mover;
    readonly IEnumerable<Direction> _dirs;
    public CharacterMoveActor(CharacterMover mover, IEnumerable<Direction> dirs)
    {
        _mover = mover;
        _dirs = dirs;
    }

    public IEnumerator Execute()
    {
        foreach (var dir in _dirs)
            yield return _mover.Co_Move(dir);
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
