using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    [SerializeField] int funnyScoreToWin = 75;
    [SerializeField] UiInventory uiInventory;
    [SerializeField] List<PickableObject> missionItems;
    [SerializeField] List<PickableObject> powerUpItems;
    [SerializeField] float maxTime;
    [SerializeField] int maxMissionPickableDrops;
    [SerializeField] List<int> funnyLevelsObjectRateRandomize;

    private List<PickableObject> funnyRatioObjectSequence;

    public float MissionPickableDropsChance = 0.5f;
    public float PowerUpPickableDropsChance = 1f;

    [SerializeField] JesterJokes jesterEvent;

    public Camera mainCamera1;
    public Camera mainCamera2;

    public bool billBoardEnabled = true;

    public bool timerStopped=false;

    [SerializeField] PlayableDirector WinDirector;
    [SerializeField] PlayableDirector LoseDirector;

    bool win = false;


    float _funnyScore = 0;

    public float FunnyScore
    {
        get => _funnyScore;

        set 
        {
            _funnyScore = value;
            onFunnyScoreChanged.Invoke(value);
        }
    }

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

        funnyRatioObjectSequence= new List<PickableObject> ();

        SetFunnyLevel();


    }

    private void Update()
    {
        uiTimerRemaining -= Time.deltaTime;

        if (!timerStopped)
        {
            uiInventory.SetUiTimer(convertTime(uiTimerRemaining));
        }
        

        if(uiTimerRemaining <0 ) 
        {
            
            LoseDirector.Play();
        }

        
    }

    private string convertTime(float time)
    {
        float minutes=time/60;
        float seconds=time%60;

        if (time <= 0)
        {
            return ($"0:00");
        }

        if (seconds < 10)
        {
            return ($"{(int)minutes}:0{(int)seconds}");
        }
        else
        {
            return ($"{(int)minutes}:{(int)seconds}");
        }

        
    }

    public PickableObject GetRandomPickable(pickableObjectType type)
    {
        PickableObject pickable = null;

        if (type == pickableObjectType.Mission)
        {
            if (maxMissionPickableDrops <= 0) return null;

            pickable = missionItems[Random.Range(0, missionItems.Count)];
            missionItems.Remove(pickable);
            maxMissionPickableDrops--;
        }
        else
        {
            pickable = powerUpItems[Random.Range(0, powerUpItems.Count)];
        }

        return pickable;
    }

    private void SetFunnyLevel()
    {
        foreach(PickableObject pickable in missionItems)
        {
            int index = Random.Range(0, funnyLevelsObjectRateRandomize.Count);

            int value = funnyLevelsObjectRateRandomize[index];

            funnyLevelsObjectRateRandomize.RemoveAt(index);

            pickable.SetFunnyLevel(value);
        }

        funnyRatioObjectSequence = missionItems.ToList();

        funnyRatioObjectSequence.Sort((x,y)=>y.GetFunnyLevel().CompareTo(x.GetFunnyLevel()));
    }

    public void DisableBilbordEvent()
    {
        billBoardEnabled = false;
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public bool checkForWin()
    {
        if (FunnyScore > 75 && !win)
        {
            win = true;
            jesterEvent.gameObject.SetActive(true);
            jesterEvent.StartEvent();
            jesterEvent.WinEvent();

            return true;
        }

        return false;
    }

    

    

    public delegate void OnFunnyScoreChanged(float value);
    public event OnFunnyScoreChanged onFunnyScoreChanged;  
}
