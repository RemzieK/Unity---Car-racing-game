using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCubeRotate : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0,2 , 0, Space.World);
    }
}
