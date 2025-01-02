using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TMP_Text CountDown;
    public AudioSource GetReady;
    public AudioSource GoAudio;
    public GameObject LapTimer;
    public GameObject CarControls;
    public AudioSource LevelMusic;
      

    void Start()
    {
        CountDown.gameObject.SetActive(false);
        StartCoroutine(CountStart());
    }

    IEnumerator CountStart()
    {
        yield return new WaitForSeconds(0.5f);
        CountDown.text = "3";
        GetReady.Play();
        CountDown.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.gameObject.SetActive(false);
        CountDown.text = "2";
        GetReady.Play();
        CountDown.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.gameObject.SetActive(false);
        CountDown.text = "1";
        GetReady.Play();
        CountDown.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.gameObject.SetActive(false);
        GoAudio.Play();
        LevelMusic.Play();
        LapTimer.SetActive(true);
        CarControls.SetActive(true);

       
    }



}
