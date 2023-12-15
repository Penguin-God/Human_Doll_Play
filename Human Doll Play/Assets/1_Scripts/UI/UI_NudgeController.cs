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

    [SerializeField] SequentialFocusCamera _camera;

    string[] _names = new string[] { "A", "B", "C" };
    int _currentIndex = 0;

    void Start()
    {
        _activeButton.onClick.AddListener(() => SetEnvirment(1));
        _inactiveButton.onClick.AddListener(() => SetEnvirment(0));

        _nextButton.onClick.AddListener(MoveToNextObject);
        _previousButton.onClick.AddListener(MoveToPreviousObject);
    }

    void SetEnvirment(int value)
    {
        _envirmentController.ChangeEnvirment(_names[_currentIndex], value);
    }

    public void StartNudgeSetting(EnvirmentController envirmentController)
    {
        _envirmentController = envirmentController;
    }

    void MoveToNextObject()
    {
        _currentIndex++;
        ClampIndex();
        _camera.MoveToTarget(_currentIndex);
    }

    void MoveToPreviousObject()
    {
        _currentIndex--;
        ClampIndex();
        _camera.MoveToTarget(_currentIndex);
    }

    void ClampIndex() => _currentIndex = Mathf.Clamp(_currentIndex, 0, _names.Length - 1);
}
