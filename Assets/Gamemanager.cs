using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] UiInventory uiInventory;

    [SerializeField] float maxTime;
    float uiTimerRemaining;

    

    private void Start()
    {
        uiTimerRemaining = maxTime;
    }

    private void Update()
    {
        uiTimerRemaining -= Time.deltaTime;

        uiInventory.SetUiTimer(convertTime(uiTimerRemaining));

        
    }

    private string convertTime(float time)
    {
        float minutes=time/60;
        float seconds=time%60;

        return ($"{(int)minutes}:{(int)seconds}");
    }
}
