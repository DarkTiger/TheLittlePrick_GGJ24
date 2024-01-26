using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    
    [SerializeField] UiInventory uiInventory;

    [SerializeField] float maxTime;
    float uiTimerRemaining;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("trovato gia un Gamemanager nella scena");
        }
        else
        {
            instance = this;

        }
    }

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

        if (seconds < 10)
        {
            return ($"{(int)minutes}:0{(int)seconds}");
        }
        else
        {
            return ($"{(int)minutes}:{(int)seconds}");
        }

        
    }
}
