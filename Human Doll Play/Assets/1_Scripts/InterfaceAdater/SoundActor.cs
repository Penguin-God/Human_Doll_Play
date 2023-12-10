using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundActor : IAct
{
    AudioClip _clip;
    public SoundActor(AudioClip clip)
    {
        _clip = clip;
    }
    public IEnumerator Execute()
    {
        SoundManager.Instance.PlaySound(_clip);
        yield return new WaitForSeconds(_clip.length);
    }
}
