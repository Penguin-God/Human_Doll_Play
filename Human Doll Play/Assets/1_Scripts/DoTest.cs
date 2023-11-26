using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTest : MonoBehaviour
{
    [SerializeField] CharacterMover characterMover;
    [SerializeField] UI_Dialogue dialogue;
    void Start()
    {
        characterMover.DependencyInject(new GridMoveUseCase(GameSettings.TileSize), 5);
        characterMover.Move(new Direction[] { Direction.Up, Direction.Up, Direction.Left, Direction.Left, Direction.Down, Direction.Right});

        dialogue.StartDialogue(new string[] { "dd", "ss", "ff" });
    }
}
