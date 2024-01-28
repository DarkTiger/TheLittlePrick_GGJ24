using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    [SerializeField] int funnyScoreToWin = 75;
    [SerializeField] UiInventory uiInventory;
    [SerializeField] List<PickableObject> missionItems;

    [SerializeField] List<PickableObject> missionItemDuplo;
    [SerializeField] List<PickableObject> powerUpItems;
    [SerializeField] float maxTime;
    [SerializeField] int maxMissionPickableDrops;
    [SerializeField] List<int> funnyLevelsObjectRateRandomize;

    [SerializeField] List<PickableObject> oggettiDialoghi;
      
    private List<PickableObject> funnyRatioObjectSequence;

    [SerializeField] List<GameObject> dialogueNpc;

    public float MissionPickableDropsChance = 0.5f;
    public float PowerUpPickableDropsChance = 1f;

    [SerializeField] JesterJokes jesterEvent;

    public Camera mainCamera1;
    public Camera mainCamera2;

    public bool billBoardEnabled = true;

    public bool timerStopped=true;

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

        funnyRatioObjectSequence = missionItems.ToList ();

        SetFunnyLevel();


    }

    private void Update()
    {
       

        if (!timerStopped)
        {
            uiTimerRemaining -= Time.deltaTime;
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

        if (type == pickableObjectType.Mission && maxMissionPickableDrops > 0)
        {
            //if (maxMissionPickableDrops <= 0) return null;

            pickable = missionItems[Random.Range(0, missionItems.Count)];

            if (pickable != null)
            {
                missionItems.Remove(pickable);
                maxMissionPickableDrops--;
            }
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

        

        funnyRatioObjectSequence.Sort((x,y)=>y.GetFunnyLevel().CompareTo(x.GetFunnyLevel()));

        oggettiDialoghi=funnyRatioObjectSequence.ToList();
        oggettiDialoghi.RemoveAt(Random.Range(1, oggettiDialoghi.Count));

        foreach(GameObject g in dialogueNpc)
        {

            int randomposition = Random.Range(0, oggettiDialoghi.Count);

            if (g.GetComponent<Gardener>() != null)
            {
                g.GetComponent<Gardener>().item = oggettiDialoghi[randomposition];
                oggettiDialoghi.RemoveAt(randomposition);


            }
            else if(g.GetComponent<Cook>() != null)
            {
                g.GetComponent<Cook>().item = oggettiDialoghi[randomposition];
                oggettiDialoghi.RemoveAt(randomposition);
            }
            else if(g.GetComponent<Latrina>() != null)
            {
                g.GetComponent<Latrina>().item = oggettiDialoghi[randomposition];
                oggettiDialoghi.RemoveAt(randomposition);
            }
            else
            {
                Debug.LogError("sbagliato inserimento dialogo");
            }


        }
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

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
        UnityEngine.Cursor.visible = true;
    }

    public List<PickableObject> GetFunnyOrderList()
    {
        return funnyRatioObjectSequence;
    }

    

    

    

    public delegate void OnFunnyScoreChanged(float value);
    public event OnFunnyScoreChanged onFunnyScoreChanged;  
}
