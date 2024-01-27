using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    
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

    

    public delegate void OnFunnyScoreChanged(float value);
    public event OnFunnyScoreChanged onFunnyScoreChanged;  
}
