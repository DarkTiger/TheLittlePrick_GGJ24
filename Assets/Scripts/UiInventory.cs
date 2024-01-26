using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInventory : MonoBehaviour
{
    [Header("Mission Object")]
    [SerializeField] GameObject missionPanel;
    [SerializeField] Image missionObjectImage;
    [SerializeField] TextMeshProUGUI missionObjectText;

    [Header("Funny Object")]
    [SerializeField] GameObject funnyPanel;
    [SerializeField] Image funnyObjectImage;
    [SerializeField] TextMeshProUGUI funnyObjectText;

    [Header("Timer")]
    [SerializeField] TextMeshProUGUI timerText;
    

    [SerializeField] inventory playerInventory;

    private void Start()
    {
        playerInventory.OnMissionObjectBool += OnBoolChangeMission;
        playerInventory.OnFunnyObjectBool += OnBoolChangeFunny;
    }

    private void Update()
    {
        
    }

    public void SetMissionUI(PickableObject obj)
    {       
        missionObjectImage.sprite = obj.objectSprite;
        missionObjectText.text = obj.objectName;
    }

    public void SetFunnyUI(PickableObject obj)
    {
        Debug.Log("Ho inseriro funny");
        funnyObjectImage.sprite = obj.objectSprite;
        funnyObjectText.text = obj.objectName;
    }

    private void OnBoolChangeMission(bool value)
    {
       missionPanel.SetActive(value);

        if(value)
        {
            SetMissionUI(playerInventory.GetMissionObject().GetComponent<PickableObject>());
        }
        
    }

    private void OnBoolChangeFunny(bool value)
    {
        funnyPanel.SetActive(value);

        if (value)
        {
            SetFunnyUI(playerInventory.GetFunnyObject().GetComponent<PickableObject>());
        }
    }

    public void SetUiTimer(string time)
    {
        timerText.text = time;
    }

   



}
