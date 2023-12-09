using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTest : MonoBehaviour
{
    [SerializeField] CharacterMover characterMover;
    [SerializeField] UI_Dialogue dialogue;
    [SerializeField] SecnarioDirector secnarioDirector;

    void Start()
    {
        characterMover.DependencyInject(new GridMoveUseCase(GameSettings.TileSize), 5);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            var mover = new CharacterMoveActor(characterMover, new Direction[] { Direction.Up, Direction.Up, Direction.Up, Direction.Up });
            var dialoguer = new Dialoguer(new string[] { "¾È³ç", "µå·¡" }, dialogue, dialogue);
            var mover2 = new CharacterMoveActor(characterMover, new Direction[] { Direction.Down, Direction.Down, Direction.Down, Direction.Down});

            secnarioDirector.Shooting(new IAct[] { mover, dialoguer, mover2 });
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            characterMover.DependencyInject(new GridMoveUseCase(GameSettings.TileSize), 5);

            var mover = new CharacterMoveActor(characterMover, new Direction[] { Direction.Up, Direction.Up, Direction.Left, Direction.Left });
            var dialoguer = new Dialoguer(new string[] { "dd", "ss", "ff" }, dialogue, dialogue);
            var mover2 = new CharacterMoveActor(characterMover, new Direction[] { Direction.Down, Direction.Right, Direction.Left, Direction.Left, Direction.Left, Direction.Left });

            secnarioDirector.Shooting(new IAct[] { mover, dialoguer, mover2 });
        }
    }
}
