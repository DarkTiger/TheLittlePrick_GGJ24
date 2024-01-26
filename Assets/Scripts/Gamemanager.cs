using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    
    [SerializeField] UiInventory uiInventory;
    [SerializeField] List<PickableObject> missionItems;
    [SerializeField] float maxTime;
    [SerializeField] int maxMissionPickableDrops;
    public float MissionPickableDropsChance = 0.1f;

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

        return ($"{(int)minutes}:{(int)seconds}");
    }

    public PickableObject GetRandomPickable()
    {
        if (maxMissionPickableDrops <= 0) return null;

        PickableObject pickable = missionItems[Random.Range(0, missionItems.Count - 1)];
        missionItems.Remove(pickable);
        maxMissionPickableDrops--;

        return pickable;
    }
}
