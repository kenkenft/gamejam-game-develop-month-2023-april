using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    
    [HideInInspector]
    public AudioSource source;

    // [Range(0f, 1f)]
    public float volume;
    // [Range(.1f, 3f)]
    public float pitch;
}