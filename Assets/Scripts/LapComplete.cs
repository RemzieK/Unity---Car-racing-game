using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LapComplete : MonoBehaviour
{
    public GameObject LapCompleteTrig;
    public GameObject HalfLapTrig;

    public TMP_Text MinuteDisplay;
    public TMP_Text SecondDisplay;
    public TMP_Text MiliDisplay;

    public TMP_Text LapCounter;
    public int LapsDone;

    void OnTriggerEnter()
    {
        LapsDone += 1;
        if(LapTimeManager.SecondCount <= 9)
        {
            SecondDisplay.text = "0" + LapTimeManager.SecondCount + ".";
        }
        else
        {
            SecondDisplay.text = "" + LapTimeManager.SecondCount + ".";
        }

        if(LapTimeManager.MinuteCount <= 9)
        {
            MinuteDisplay.text = "0" + LapTimeManager.MinuteCount + ".";
        }
        else
        {
            MinuteDisplay.text = "" + LapTimeManager.MinuteCount + ".";
        }

        MiliDisplay.text = Mathf.Floor(LapTimeManager.MiliCount).ToString("0");

        LapTimeManager.MinuteCount = 0;
        LapTimeManager.SecondCount = 0;
        LapTimeManager.MiliCount = 0;
        LapCounter.text = " " + LapsDone;

        HalfLapTrig.SetActive(true);
        LapCompleteTrig.SetActive(false);
    }
}
