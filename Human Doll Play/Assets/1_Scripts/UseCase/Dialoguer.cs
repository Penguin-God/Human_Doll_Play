using System.Collections;
using System.Collections.Generic;

public interface IDialogueDrawer
{
    IEnumerator DrawLine(string line);
}

public interface IYieldNextLine
{
    IEnumerator YieldNextLine();
}

public class Dialoguer : IAct
{
    readonly IEnumerable<string> _lines;
    readonly IDialogueDrawer _dialogueDrawer;
    readonly IYieldNextLine _yieldNextLine;
    public Dialoguer(IEnumerable<string> lines, IDialogueDrawer dialogueDrawer, IYieldNextLine yieldNextLine)
    {
        _lines = lines;
        _dialogueDrawer = dialogueDrawer;
        _yieldNextLine = yieldNextLine;
    }

    public IEnumerator Execute()
    {
        foreach (var line in _lines)
        {
            _dialogueDrawer.DrawLine(line);
            yield return _yieldNextLine.YieldNextLine();
            yield return null;
        }
    }
}
