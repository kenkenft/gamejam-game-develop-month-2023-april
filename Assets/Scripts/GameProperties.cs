using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProperties
{
    private static Dictionary<string, AudioClip> _audioClipsDict = new Dictionary<string, AudioClip>(){
                                                                                                        {"test", Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-03")}
                                                                                                        };

    
    // private static Dictionary<int, CoinScriptable> _coinScriptableDict = new Dictionary<int, CoinScriptable>(){
    //                                                                                                             {0, Resources.Load<CoinScriptable>("Coin00")},
    //                                                                                                             {1, Resources.Load<CoinScriptable>("Coin01")},
    //                                                                                                             {2, Resources.Load<CoinScriptable>("Coin02")},
    //                                                                                                             {3, Resources.Load<CoinScriptable>("Coin03")},
    //                                                                                                             };

    private static List<CoinScriptable> _coinScriptableDict = new List<CoinScriptable>(){
                                                                                            Resources.Load<CoinScriptable>("Coin00"),
                                                                                            Resources.Load<CoinScriptable>("Coin01"),
                                                                                            Resources.Load<CoinScriptable>("Coin02"),
                                                                                            Resources.Load<CoinScriptable>("Coin03")
                                                                                        };
    
    public static AudioClip GetAudioClip(string clipName)
    {
        return _audioClipsDict[clipName];
    }

    public static List<CoinScriptable> GetCoinScriptables()
    {
        return _coinScriptableDict;
    }
}
