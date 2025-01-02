using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapTimeManager : MonoBehaviour
{
    public static int MinuteCount;
    public static int SecondCount;
    public static float MiliCount;
    public static string MiliDisplay;

    public TMP_Text MinuteBox;
    public TMP_Text SecondBox;
    public TMP_Text MilliBox;

    void Update()
    {

        MiliCount += Time.deltaTime * 10f;
        MiliDisplay = Mathf.Floor(MiliCount).ToString("0");
        MilliBox.text = MiliDisplay;

        if(MiliCount >= 10)
        {
            MiliCount = 0;
            SecondCount += 1;
        }

        if(SecondCount <= 9)
        {
            SecondBox.text = "0" + SecondCount + ".";
        }
        else
        {
            SecondBox.text = "" + SecondCount + ".";
        }

        if(SecondCount >= 60)
        {
            SecondCount = 0;
            MinuteCount += 1;
        }

        if(MinuteCount <= 9)
        {
            MinuteBox.text = "0" + MinuteCount + ":";
        }
        else
        {
            MinuteBox.text = "" + MinuteCount + ":";
        }
    }
}
