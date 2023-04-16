using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProperties
{
    private static Dictionary<string, AudioClip> _audioClipsDict = new Dictionary<string, AudioClip>(){
                                                                                                        {"test", Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-03")}
                                                                                                        };

    public static AudioClip GetAudioClip(string clipName)
    {
        return _audioClipsDict[clipName];
    }
}
