using System.Collections;
using System.Collections.Generic;

public interface IDialoguer
{
    void StartDialogue();
    void DrawLine(string line);
    void EndDialogue();
}

public interface IYieldNextLine
{
    IEnumerator YieldNextLine();
}