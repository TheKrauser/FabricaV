using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlaySoundtrack : MonoBehaviour
{
    [SerializeField] private string soundName;


    void Start()
    {
        AutoPlay(soundName);
    }

    public void AutoPlay(string name)
    {
        AudioManager.Instance.PlaySoundtrack(name);
    } 
}
