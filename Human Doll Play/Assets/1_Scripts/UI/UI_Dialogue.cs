using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Dialogue : MonoBehaviour, IDialoguer, IYieldNextLine
{
    [SerializeField] TextMeshProUGUI _dialogueText;
    [SerializeField] GameObject _textBackground;

    public void StartDialogue()
    {
        _dialogueText.text = "";
        _textBackground.SetActive(true);
    }

    public void DrawLine(string line) => _dialogueText.text = line;

    public IEnumerator YieldNextLine()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));
        yield return null;
    }

    public void EndDialogue()
    {
        _dialogueText.text = "";
        _textBackground.SetActive(false);
    }
}
