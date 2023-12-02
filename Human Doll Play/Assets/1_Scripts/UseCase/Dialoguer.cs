using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialoguer : IAct
{
    readonly IEnumerable<string> _lines;
    readonly IDialoguer _dialogueDrawer;
    readonly IYieldNextLine _yieldNextLine;
    public Dialoguer(IEnumerable<string> lines, IDialoguer dialogueDrawer, IYieldNextLine yieldNextLine)
    {
        _lines = lines;
        _dialogueDrawer = dialogueDrawer;
        _yieldNextLine = yieldNextLine;
    }

    public IEnumerator Execute()
    {
        _dialogueDrawer.StartDialogue();
        foreach (var line in _lines)
        {
            _dialogueDrawer.DrawLine(line);
            yield return _yieldNextLine.YieldNextLine();
            yield return null;
        }
        _dialogueDrawer.EndDialogue();
    }
}
