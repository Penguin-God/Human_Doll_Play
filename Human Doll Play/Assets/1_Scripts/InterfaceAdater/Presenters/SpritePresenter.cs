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
    int _spriteIndex;
    
    public void ChangeEnvierment()
    {
        _spriteIndex++;
        if (_spriteIndex >= _sprites.Length) _spriteIndex = 0;

        _spriteRenderer.sprite = _sprites[_spriteIndex];
    }
}
