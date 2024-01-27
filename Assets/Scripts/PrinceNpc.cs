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

    private void Start()
    {
        funnyLevel = Gamemanager.instance.FunnyScore;

        funnyBarSlider.value = currentLevel;

        Gamemanager.instance.onFunnyScoreChanged += SetFunnyLevel;
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

                AddFunnyScore(playerObject.GetComponent<PickableObject>().GetFunnyLevel());

                //dopo

                inventory.Instance.SetMissionObject(null);
                Destroy(playerObject);
                
            }
        }
    }
}
