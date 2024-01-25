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

    [SerializeField] inventory playerInventory;

    private void Start()
    {
        playerInventory.OnMissionObjectBool += OnBoolChangeMission;
        playerInventory.OnFunnyObjectBool += OnBoolChangeFunny;
    }

    public void SetMissionUI(PickableObject obj)
    {       
        missionObjectImage.sprite = obj.objectSprite;
        missionObjectText.text = obj.objectName;
    }

    public void SetFunnyUI(PickableObject obj)
    {
        
        funnyObjectImage.sprite = obj.objectSprite;
        funnyObjectText.name = obj.objectName;
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
            SetMissionUI(playerInventory.GetFunnyObject().GetComponent<PickableObject>());
        }
    }
   
    

}
