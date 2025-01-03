using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RaceFinish : MonoBehaviour
{
    public GameObject MyCar;
    public GameObject FinishCam;
    public GameObject ViewModes;
    public GameObject LevelMusic;
    public GameObject CompleteTrig;

   void OnTriggerEnter()
    {
        CarController carController = MyCar.GetComponent<CarController>();

        MyCar.SetActive(false);
        CompleteTrig.SetActive(false);
        carController.maximumMotorTorque = 0.0f;
        MyCar.GetComponent<CarController>().enabled=false;
        MyCar.SetActive(true);
        FinishCam.SetActive(true);
        LevelMusic.SetActive(false);
        ViewModes.SetActive(false);

    }
}
