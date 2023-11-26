using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _dialogueText;
    [SerializeField] GameObject _textBackground;
    public void StartDialogue(IEnumerable<string> lines)
    {
        _textBackground.SetActive(true);
        _dialogueText.text = "";
        StartCoroutine(Co_DisplayDialogue(lines));
    }

    IEnumerator Co_DisplayDialogue(IEnumerable<string> lines)
    {
        foreach (string line in lines)
        {
            _dialogueText.text = line;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));
            yield return null;
        }
        EndDialogue();
    }

    void EndDialogue()
    {
        _dialogueText.text = "";
        _textBackground.SetActive(false);
    }
}
