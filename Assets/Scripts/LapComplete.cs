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

    public GameObject RaceFinish;

    public GameOverPanelManager GameOverPanelManager;  // Reference to the GameOverPanelManager

    private float raceTime;

    void OnTriggerEnter()
    {
        LapsDone += 1;

        // Calculate the current lap time
        raceTime = LapTimeManager.MinuteCount * 60 + LapTimeManager.SecondCount + LapTimeManager.MiliCount / 10f;

        // Update best lap time
        LapTimeManager.UpdateBestLapTime(raceTime);

        // Update lap time displays
        UpdateLapDisplay();

        // Reset lap time for the next lap
        ResetLapTime();

        // Update lap counter
        LapCounter.text = " " + LapsDone;

        // Toggle triggers
        HalfLapTrig.SetActive(true);
        LapCompleteTrig.SetActive(false);

        // Check if race is complete
        if (LapsDone >= 3)
        {
            RaceFinish.SetActive(true);
            LapTimeManager.RaceFinished = true;
            GameOverPanelManager.ShowGameOverPanel(raceTime);
        }

        // Play finish audio
        AudioSource finishAudio = RaceFinish.GetComponent<AudioSource>();
        if (finishAudio != null)
        {
            finishAudio.Play();
        }
    }

    private void UpdateLapDisplay()
    {
        SecondDisplay.text = LapTimeManager.SecondCount <= 9 ? $"0{LapTimeManager.SecondCount}." : $"{LapTimeManager.SecondCount}.";
        MinuteDisplay.text = LapTimeManager.MinuteCount <= 9 ? $"0{LapTimeManager.MinuteCount}." : $"{LapTimeManager.MinuteCount}.";
        MiliDisplay.text = Mathf.Floor(LapTimeManager.MiliCount).ToString("0");
    }

    private void ResetLapTime()
    {
        LapTimeManager.MinuteCount = 0;
        LapTimeManager.SecondCount = 0;
        LapTimeManager.MiliCount = 0;
    }
}
