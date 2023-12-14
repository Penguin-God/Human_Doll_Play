using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePresenter : MonoBehaviour, ISceneEnvirment
{
    [SerializeField] Sprite[] _sprites;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    SpriteRenderer _spriteRenderer;
    public void ChangeEnvierment(int value)
    {
        if (value >= 0 && value < _sprites.Length) 
            _spriteRenderer.sprite = _sprites[value];
    }
}
