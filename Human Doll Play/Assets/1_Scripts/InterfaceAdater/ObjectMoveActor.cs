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
