using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PrinceNpc : MonoBehaviour
{
    private float funnyLevel;
    private float currentLevel;

    
    [SerializeField] float increasePerSecond;

    [SerializeField] Slider funnyBarSlider;

    [SerializeField] JesterJokes jesterJokes;

    private SpriteRenderer spriteRenderer;

    [SerializeField] Transform ExectionPoint;

    private Animator animator;

    private void Start()
    {
        funnyLevel = Gamemanager.instance.FunnyScore;

        funnyBarSlider.value = currentLevel;

        Gamemanager.instance.onFunnyScoreChanged += SetFunnyLevel;

        spriteRenderer = GetComponent<SpriteRenderer>();

        animator=GetComponent<Animator>();
    }
    public void AddFunnyLevel(int value)
    {
        funnyLevel=value;
    }

    private void Update()
    {
        if (currentLevel<funnyLevel)
        {
            currentLevel += increasePerSecond * Time.deltaTime;

            funnyBarSlider.value = currentLevel;

            
        }
        else if(currentLevel > funnyLevel)
        {
            currentLevel = funnyLevel;

            funnyBarSlider.value = currentLevel;

            
        }
    }

    private void SetFunnyLevel(float value)
    {
        funnyLevel = value;
    }

    public void AddFunnyScore(float value)
    {
        Gamemanager.instance.FunnyScore += value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerMovement>()!=null)
        {
            GameObject playerObject = inventory.Instance.GetMissionObject();

            if(playerObject==null)
            {
                return;
            }

            if(playerObject!=null && playerObject.GetComponent<PickableObject>().objectType== pickableObjectType.Mission)
            {
                //fai animazione

                jesterJokes.gameObject.SetActive(true);
                Debug.Log(jesterJokes.gameObject);

                jesterJokes.StartJokesAnimation(playerObject.GetComponent<PickableObject>().GetMissionType());

                gameObject.transform.eulerAngles = new Vector3(
    0,
       180,
    0
);

                jesterJokes.StartEvent();

                AddFunnyScore(playerObject.GetComponent<PickableObject>().GetFunnyLevel());

                //dopo
                
                inventory.Instance.SetMissionObject(null);
                Destroy(playerObject);
                
            }
        }
    }

    public void DisablePrince()
    {
        spriteRenderer.enabled = false;
    }

    public void SetExecutionPoint()
    {
        transform.position = ExectionPoint.position;

        gameObject.transform.eulerAngles = new Vector3(
    0,
       180,
    0);
    }

    public void PrinceLaugh()
    {
        jesterJokes.StartEvent();
        animator.SetTrigger("Laugh");
    }

    public void DisableJesterSprite()
    {
        jesterJokes.gameObject.GetComponent<SpriteRenderer>().enabled=false;
    }
}
