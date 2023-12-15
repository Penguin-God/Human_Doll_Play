using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_NudgeController : MonoBehaviour
{
    EnvirmentController _envirmentController;
    [SerializeField] Button _activeButton;
    [SerializeField] Button _inactiveButton;
    [SerializeField] Button _nextButton;
    [SerializeField] Button _previousButton;

    void Start()
    {
        _activeButton.onClick.AddListener(() => SetEnvirment(1));
        _inactiveButton.onClick.AddListener(() => SetEnvirment(0));
    }

    void SetEnvirment(int value)
    {
        _envirmentController.ChangeEnvirment("A", value);
    }

    public void StartNudgeSetting(EnvirmentController envirmentController)
    {
        _envirmentController = envirmentController;
    }
}
