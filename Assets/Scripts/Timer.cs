using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    int timeLeft = 9;
    Text timerText;
    // GameManager gameManager;
    WaitForSecondsRealtime timerDelay = new WaitForSecondsRealtime(0.9f);
    WaitForSecondsRealtime addTimerDelay = new WaitForSecondsRealtime(0.4f);
    Color defaultColor = Color.white, addColor = Color.green, currColor;
    void Start()
    {
        timerText = GetComponentInChildren<Text>();
        timerText.text = "Time: " + timeLeft;
        currColor = timerText.color;
        // gameManager = GetComponentInParent<GameManager>();
    }

    public IEnumerator Countdown(int startAmount)
    {
        timeLeft = startAmount;
        while(timeLeft > 0)
        {
            yield return timerDelay;
            timeLeft--;
            timerText.text = "Time: " + timeLeft;
        }
        StopCoroutine("Countdown");
        // gameManager.TriggerEndgame();
        yield return null;
    }

    IEnumerator WaitAddTimer()
    {
        yield return addTimerDelay;
    }

}
