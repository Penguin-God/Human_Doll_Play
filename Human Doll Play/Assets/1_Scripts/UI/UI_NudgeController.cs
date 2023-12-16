using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_NudgeController : MonoBehaviour
{
    NudgeEnvirmentController _envirmentController;
    [SerializeField] Button _activeButton;
    [SerializeField] Button _inactiveButton;
    [SerializeField] Button _nextButton;
    [SerializeField] Button _previousButton;
    [SerializeField] Button _sunButton;
    [SerializeField] Button _cloudButton;

    [SerializeField] SequentialFocusCamera _camera;

    string[] _names = new string[] { "A", "B", "C" };
    int _currentIndex = 0;

    void Start()
    {
        _activeButton.onClick.AddListener(() => SetEnvirment(1));
        _inactiveButton.onClick.AddListener(() => SetEnvirment(0));

        _nextButton.onClick.AddListener(MoveToNextObject);
        _previousButton.onClick.AddListener(MoveToPreviousObject);

        _sunButton.onClick.AddListener(() => SetEnvirment(1));
        _cloudButton.onClick.AddListener(() => SetEnvirment(0));
    }

    void SetEnvirment(int value)
    {
        _envirmentController.ChangeEnvirment(_names[_currentIndex], value);
    }

    public void StartNudgeSetting(NudgeEnvirmentController envirmentController)
    {
        _envirmentController = envirmentController;
    }

    void MoveToNextObject()
    {
        _currentIndex++;
        ClampIndex();
        _camera.MoveToTarget(_currentIndex);
        SetButton();
    }

    void MoveToPreviousObject()
    {
        _currentIndex--;
        ClampIndex();
        _camera.MoveToTarget(_currentIndex);
        SetButton();
    }

    void SetButton()
    {
        if(_currentIndex == 0)
        {
            _sunButton.gameObject.SetActive(true);
            _cloudButton.gameObject.SetActive(true);
            _activeButton.gameObject.SetActive(false);
            _inactiveButton.gameObject.SetActive(false);
        }
        else
        {
            _activeButton.gameObject.SetActive(true);
            _inactiveButton.gameObject.SetActive(true);
            _sunButton.gameObject.SetActive(false);
            _cloudButton.gameObject.SetActive(false);
        }
    }

    void ClampIndex() => _currentIndex = Mathf.Clamp(_currentIndex, 0, _names.Length - 1);
}
